using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web; // Add reference to System.Web (for HttpUtility.HtmlEncode)
using System.Windows.Forms; // if using WinForms

/// <Copyright>
///     Copyright © 2025-2026 GTN Industries Ltd - Nagpur Unit 
///     All rights reserved.
///     IT Department (Anil K. Waghmare)
/// </Copyright>
/// 
/// <summary>
///    Redistribution and use in source and binary forms, with or without modification, 
///    are permitted provided that the following conditions are met:
///
///   - Redistributions of source code must retain the above copyright notice, 
///     this list of conditions and the following disclaimer.
///
///   - Redistributions in binary form must reproduce the above copyright notice, 
///     this list of conditions and the following disclaimer in the documentation 
///     and/or other materials provided with the distribution.
/// </summary>
/// 
/// <Email>
///     (anil@gtnindustries.com)
/// </Email>
/// 
/// <Module>
///    Title:          Helper class
///    Name:           HtmlExportHelper.cs
///    Created:        24th August 2025
///    Date Completed: 24th August 2025
/// </Module >
/// 
/// <ChangeLog>
///   Date Modified:   
/// 
/// </ChangeLog>
/// 
/// <databaseDetails>
///   Database Name:     
/// </databaseDetails>
/// 
/// <tablesUsed>
///   
/// </tablesUsed>
/// 
/// <commonControlsUsed>
///   
/// </commonControlsUsed>
/// 
/// <containersUsed>
///   
/// </containersUsed>
/// 
/// <menusToolBarsUsed>
///  
/// </menusToolBarsUsed>
/// 
/// <dataComponentsUsed>
///   
/// </dataComponentsUsed>
/// 
/// <componentsUsed>
///   
/// </componentsUsed>
/// 
/// <remarks>
///    
/// </remarks>
/// 

namespace TimeAttendanceManager.Helpers.Classes
{

    /// <summary>
    /// usage
    /// var file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Contractors.html");
    /// HtmlExportHelper.ExportDataTableToHtml(myDataTable, file, title: "Master Contractors", dateFormat: "dd-MMM-yyyy");
    /// </summary>
    public static class HtmlExportHelper
    {
        public static void ExportDataTableToHtml(
            DataTable table,
            string filePath,
            string title = "Data Export",
            string dateFormat = "dd-MMM-yyyy",
            Dictionary<string, string> customHeaders = null)
        {
            if (table == null) throw new ArgumentNullException(nameof(table));
            if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentException("filePath required.", nameof(filePath));

            var sb = new StringBuilder(1 << 20); // start with 1MB capacity to reduce reallocs

            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html lang=\"en\">");
            sb.AppendLine("<head>");
            sb.AppendLine("  <meta charset=\"utf-8\" />");
            sb.AppendLine("  <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\" />");
            sb.AppendLine($"  <title>{HttpUtility.HtmlEncode(title)}</title>");
            // Minimal CSS (responsive, clean)
            sb.AppendLine(@"  <style>
                :root{--fg:#222;--bg:#fff;--muted:#666;--border:#ddd}
                *{box-sizing:border-box} body{font-family:Segoe UI,Arial,Helvetica,sans-serif;margin:0;background:var(--bg);color:var(--fg)}
                header{padding:16px 20px;border-bottom:1px solid var(--border)}
                h1{margin:0;font-size:20px}
                .toolbar{display:flex;gap:12px;flex-wrap:wrap;align-items:center;padding:12px 20px;border-bottom:1px solid var(--border)}
                .toolbar input[type=search]{flex:1 1 280px;padding:8px 10px;border:1px solid var(--border);border-radius:8px}
                .toolbar .select{display:flex;align-items:center;gap:6px}
                .table-wrap{padding:10px 20px}
                .responsive{overflow:auto;border:1px solid var(--border);border-radius:10px}
                table{width:100%; border-collapse:collapse; min-width:640px}
                thead th{position:sticky;top:0;background:#fafafa;border-bottom:1px solid var(--border);text-align:left;font-weight:600;padding:10px;cursor:pointer}
                tbody td{border-top:1px solid var(--border);padding:10px;vertical-align:top}
                tbody tr:nth-child(even){background:#fcfcfc}
                .muted{color:var(--muted);font-size:12px}
                .sort-indicator{margin-left:6px;font-size:11px}
                .pager{display:flex;gap:6px;align-items:center;justify-content:flex-end;padding:10px 0}
                .pager button{padding:6px 10px;border:1px solid var(--border);background:#fff;border-radius:8px;cursor:pointer}
                .pager button[disabled]{opacity:.5;cursor:not-allowed}
                .count{margin-left:auto}
              </style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("  <header><h1>" + HttpUtility.HtmlEncode(title) + "</h1></header>");
            sb.AppendLine("  <div class=\"toolbar\">");
            sb.AppendLine("    <input id=\"globalSearch\" type=\"search\" placeholder=\"Search...\" />");
            sb.AppendLine("    <div class=\"select\"><label for=\"pageSize\" class=\"muted\">Rows per page:</label>");
            sb.AppendLine("      <select id=\"pageSize\">");
            sb.AppendLine("        <option>10</option><option selected>25</option><option>50</option><option>100</option><option>500</option>");
            sb.AppendLine("      </select>");
            sb.AppendLine("    </div>");
            sb.AppendLine("    <div class=\"muted count\" id=\"countInfo\"></div>");
            sb.AppendLine("  </div>");

            sb.AppendLine("  <div class=\"table-wrap\">");
            sb.AppendLine("    <div class=\"responsive\">");
            sb.AppendLine("      <table id=\"dataTable\">");
            sb.AppendLine("        <thead><tr>");

            // Render headers with data-type hints
            //foreach (DataColumn col in table.Columns)
            //{
            //    string dataType = GetTypeHint(col.DataType);
            //    sb.Append($"<th data-type=\"{dataType}\">{HttpUtility.HtmlEncode(col.ColumnName)}<span class=\"sort-indicator\"></span></th>");
            //}
            //sb.AppendLine("</tr></thead>");
            //sb.AppendLine("<tbody>");

            // Render headers with custom dictionary if available
            foreach (DataColumn col in table.Columns)
            {
                string dataType = GetTypeHint(col.DataType);

                string headerText = (customHeaders != null && customHeaders.ContainsKey(col.ColumnName))
                    ? customHeaders[col.ColumnName]
                    : col.ColumnName;

                sb.Append($"<th data-type=\"{dataType}\">{HttpUtility.HtmlEncode(headerText)}<span class=\"sort-indicator\"></span></th>");
            }
            sb.AppendLine("</tr></thead>");
            sb.AppendLine("<tbody>");


            // Render rows
            foreach (DataRow row in table.Rows)
            {
                sb.Append("<tr>");
                foreach (DataColumn col in table.Columns)
                {
                    string text = FormatCell(row[col], col.DataType, dateFormat);
                    sb.Append("<td>").Append(HttpUtility.HtmlEncode(text)).Append("</td>");
                }
                sb.AppendLine("</tr>");
            }

            sb.AppendLine("        </tbody>");
            sb.AppendLine("      </table>");
            sb.AppendLine("    </div>");

            // Pager
            sb.AppendLine(@"    <div class=""pager"">
                  <button id=""prevBtn"">&laquo; Prev</button>
                  <span id=""pageInfo"" class=""muted""></span>
                  <button id=""nextBtn"">Next &raquo;</button>
                </div>");

            sb.AppendLine("  </div>");

            // Tiny JS for search, sort, pagination
            sb.AppendLine(@"  <script>
            (function(){
              const table = document.getElementById('dataTable');
              const tbody = table.querySelector('tbody');
              const headers = Array.from(table.querySelectorAll('thead th'));
              const search = document.getElementById('globalSearch');
              const pageSizeSel = document.getElementById('pageSize');
              const prevBtn = document.getElementById('prevBtn');
              const nextBtn = document.getElementById('nextBtn');
              const pageInfo = document.getElementById('pageInfo');
              const countInfo = document.getElementById('countInfo');

              let rows = Array.from(tbody.querySelectorAll('tr'));
              let filtered = rows.slice();
              let sortCol = -1, sortDir = 1; // 1=asc, -1=desc
              let page = 1, pageSize = parseInt(pageSizeSel.value,10);

              function textOfRow(row){
                return row.textContent.toLowerCase();
              }

              function applySearch(){
                const q = search.value.trim().toLowerCase();
                filtered = rows.filter(r => !q || textOfRow(r).includes(q));
                page = 1;
                applySort();
                render();
              }

              function compare(a, b, type){
                if(type === 'number'){
                  const na = parseFloat(a.replace(/,/g,'')); const nb = parseFloat(b.replace(/,/g,''));
                  if(isNaN(na) && isNaN(nb)) return 0;
                  if(isNaN(na)) return -1;
                  if(isNaN(nb)) return 1;
                  return na - nb;
                } else if(type === 'date'){
                  const pa = Date.parse(a); const pb = Date.parse(b);
                  if(isNaN(pa) && isNaN(pb)) return 0;
                  if(isNaN(pa)) return -1;
                  if(isNaN(pb)) return 1;
                  return pa - pb;
                }
                return a.localeCompare(b);
              }

              function applySort(){
                if (sortCol < 0) return;
                const type = headers[sortCol].dataset.type || 'string';
                filtered.sort((r1,r2)=>{
                  const a = r1.children[sortCol].textContent.trim();
                  const b = r2.children[sortCol].textContent.trim();
                  const c = compare(a,b,type);
                  return c * sortDir;
                });
              }

              function clearIndicators(){
                headers.forEach(h=>{ h.querySelector('.sort-indicator').textContent=''; });
              }

              function render(){
                // Pagination
                pageSize = parseInt(pageSizeSel.value,10);
                const total = filtered.length;
                const pages = Math.max(1, Math.ceil(total / pageSize));
                page = Math.min(Math.max(1, page), pages);
                const start = (page-1)*pageSize;
                const end = start + pageSize;

                // Update DOM
                tbody.innerHTML = '';
                filtered.slice(start, end).forEach(r=>tbody.appendChild(r));
                prevBtn.disabled = (page === 1);
                nextBtn.disabled = (page === pages);
                pageInfo.textContent = `Page ${page} / ${pages}`;
                countInfo.textContent = `${total} row(s)`;
              }

              // Events
              search.addEventListener('input', ()=>{ applySearch(); });
              pageSizeSel.addEventListener('change', ()=>{ page = 1; render(); });
              prevBtn.addEventListener('click', ()=>{ page--; render(); });
              nextBtn.addEventListener('click', ()=>{ page++; render(); });
              headers.forEach((h, idx)=>{
                h.addEventListener('click', ()=>{
                  if (sortCol === idx){ sortDir = -sortDir; } else { sortCol = idx; sortDir = 1; }
                  clearIndicators();
                  h.querySelector('.sort-indicator').textContent = sortDir>0 ? '▲' : '▼';
                  applySort();
                  render();
                });
              });

              // Initial
              applySearch(); // also sorts + renders
            })();
  </script>");

            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            // Write file
            Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(filePath)) ?? ".");
            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);

            // Open in default browser
            try { Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true }); }
            catch { /* ignore */ }
        }

        private static string GetTypeHint(Type t)
        {
            if (t == typeof(byte) || t == typeof(short) || t == typeof(int) || t == typeof(long) ||
                t == typeof(float) || t == typeof(double) || t == typeof(decimal))
                return "number";
            if (t == typeof(DateTime)) return "date";
            return "string";
        }

        private static string FormatCell(object value, Type type, string dateFormat)
        {
            if (value == null || value == DBNull.Value) return string.Empty;

            if (type == typeof(DateTime))
            {
                var dt = (DateTime)value;
                return dt.ToString(dateFormat, CultureInfo.InvariantCulture);
            }
            // Optional: format decimals nicely (no trailing zeros if integer)
            if (type == typeof(decimal) || type == typeof(double) || type == typeof(float))
            {
                return Convert.ToDecimal(value).ToString(CultureInfo.InvariantCulture);
            }
            return Convert.ToString(value, CultureInfo.CurrentCulture) ?? string.Empty;
        }


    }
}

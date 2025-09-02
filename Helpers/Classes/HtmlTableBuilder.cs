using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web; // Add reference to System.Web (for HtmlEncode/AttributeEncode)
using System.Globalization;
using System.Windows;

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
///    Name:           HtmlTableBuilder.cs
///    Created:        27th August 2025
///    Date Completed: 27th August 2025
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


namespace TimeAttendanceManager.Helpers
{
    public static class HtmlTableBuilder
    {
        /// <summary>
        /// Build a sortable, filterable, paginated HTML table for WebView2.
        /// Right-aligns numeric types; formats DateTime as dd-MMM-yyyy; TimeSpan as hh:mm.
        /// </summary>
        public static string BuildWebViewHtml(
            DataTable dt,
            IDictionary<string, string> customHeaders = null)
        {
            try
            {
                if (dt == null) throw new ArgumentNullException(nameof(dt));
                var cols = dt.Columns.Cast<DataColumn>().ToList();
                var sb = new StringBuilder();

                sb.Append(@"
                <!DOCTYPE html>
                <html>
                <head>
                <meta charset='utf-8' />
                <title>Data</title>
                <style>
                    body{font-family:Segoe UI,Arial,sans-serif;margin:16px;}
                    .toolbar{display:flex;gap:12px;align-items:center;margin-bottom:10px;flex-wrap:wrap}
                    input[type='text']{padding:6px 8px;min-width:240px;border:1px solid #ccc;border-radius:6px}
                    select{padding:6px 8px;border:1px solid #ccc;border-radius:6px}
                    table{border-collapse:collapse;width:100%}
                    th, td{border:1px solid #e5e7eb;padding:8px 10px;font-size:13px;vertical-align:top}
                    th{background:#f3f4f6;cursor:pointer;position:sticky;top:0}
                    tr:nth-child(even){background:#fafafa}
                    .muted{color:#6b7280}
                    .pill{display:inline-block;padding:2px 8px;border-radius:999px;font-size:12px}
                    .pill-yes{background:#dcfce7;color:#166534}
                    .pill-no{background:#fee2e2;color:#991b1b}
                    .pager{display:flex;gap:6px;align-items:center;margin-top:10px;flex-wrap:wrap}
                    .btn{padding:6px 10px;border:1px solid #d1d5db;border-radius:6px;background:#fff;cursor:pointer}
                    .btn[disabled]{opacity:.5;cursor:not-allowed}
                    .count{margin-left:auto;color:#6b7280}
                    .th-sort-asc::after{content:' ▲';}
                    .th-sort-desc::after{content:' ▼';}
                    .nwrap{white-space:nowrap}
                    /* NEW: alignment helpers */
                    .num{ text-align:right; }
                    .date{ white-space:nowrap; }
                    .time{ white-space:nowrap; }
                </style>
                </head>
                <body>
                <div class='toolbar'>
                  <input id='searchBox' type='text' placeholder='Search...' />
                  <label class='muted'>Rows per page:</label>
                  <select id='rowsPerPage'>
                    <option value='10'>10</option>
                    <option value='25'>25</option>
                    <option value='50'>50</option>
                    <option value='100'>100</option>
                    <option value='-1'>All</option>
                  </select>
                  <span class='count' id='countInfo'></span>
                </div>
                <table id='grid'>
                  <thead>
                    <tr>");

                // headers
                foreach (var col in cols)
                {
                    var colName = col.ColumnName;
                    var headerText = (customHeaders != null && customHeaders.TryGetValue(colName, out var h)) ? h : colName;

                    sb.Append("<th class='nwrap' data-col='")
                      .Append(HttpUtility.HtmlEncode(colName))
                      .Append("'>")
                      .Append(HttpUtility.HtmlEncode(headerText))
                      .Append("</th>");
                }

                sb.Append(@"</tr>
                  </thead>
                  <tbody>
                ");

                // rows
                foreach (DataRow r in dt.Rows)
                {
                    sb.Append("<tr>");
                    foreach (var col in cols)
                    {
                        object v = r[col];

                        string css = GetCellCss(col, v);               // "num" / "date" / "time" / ""
                        string cellHtml = FormatDisplay(v, col);        // friendly display
                        string dataVal = RawSortableValue(v, col);     // for JS sort/filter

                        sb.Append("<td")
                          .Append(string.IsNullOrEmpty(css) ? "" : $" class='{css}'")
                          .Append(" data-val='").Append(HttpUtility.HtmlAttributeEncode(dataVal)).Append("'>")
                          .Append(cellHtml)
                          .Append("</td>");
                    }
                    sb.Append("</tr>\n");
                }

                sb.Append(@"  </tbody>
                </table>

                <div class='pager'>
                  <button class='btn' id='prevBtn'>&lt; Prev</button>
                  <span id='pageInfo' class='muted'></span>
                  <button class='btn' id='nextBtn'>Next &gt;</button>
                </div>

                <script>
                (function(){
                  const table = document.getElementById('grid');
                  const tbody = table.querySelector('tbody');
                  const headers = Array.from(table.querySelectorAll('thead th'));
                  const searchBox = document.getElementById('searchBox');
                  const rowsPerPageSel = document.getElementById('rowsPerPage');
                  const prevBtn = document.getElementById('prevBtn');
                  const nextBtn = document.getElementById('nextBtn');
                  const pageInfo = document.getElementById('pageInfo');
                  const countInfo = document.getElementById('countInfo');

                  let data = Array.from(tbody.querySelectorAll('tr'));
                  let filtered = data.slice();
                  let sortCol = -1;
                  let sortDir = 1; // 1 asc, -1 desc
                  let page = 1;

                  function textOfRow(tr){
                    return Array.from(tr.cells).map(td => (td.getAttribute('data-val')||td.textContent||'')).join(' ').toLowerCase();
                  }

                  function applyFilter(){
                    const q = (searchBox.value||'').trim().toLowerCase();
                    filtered = !q ? data.slice() : data.filter(tr => textOfRow(tr).indexOf(q) !== -1);
                    page = 1;
                    updateCount();
                    render();
                  }

                  function tryParse(val){
                    if(val === null || val === undefined) return {num:false, val:''};

                    // ISO date (yyyy-mm-dd) → numeric yyyymmdd for sort
                    if(/^\d{4}-\d{2}-\d{2}$/.test(val)){
                      return {num:true, val:Number(val.replace(/-/g,''))};
                    }

                    // time hh:mm(:ss) → seconds
                    if(/^\d{1,2}:\d{2}(:\d{2})?$/.test(val)){
                      const p = val.split(':').map(Number);
                      const s = p[0]*3600 + p[1]*60 + (p[2]||0);
                      return {num:true, val:s};
                    }

                    // numeric
                    const n = parseFloat(val);
                    if(!isNaN(n)) return {num:true, val:n};
                    return {num:false, val:val.toString().toLowerCase()};
                  }

                  function clearSortIndicators(){
                    headers.forEach(h=>h.classList.remove('th-sort-asc','th-sort-desc'));
                  }

                  function applySort(colIdx){
                    if(sortCol === colIdx){ sortDir *= -1; } else { sortCol = colIdx; sortDir = 1; }
                    clearSortIndicators();
                    if(sortCol >= 0){
                      headers[sortCol].classList.add(sortDir===1 ? 'th-sort-asc' : 'th-sort-desc');
                      filtered.sort((a,b)=>{
                        const av = a.cells[sortCol].getAttribute('data-val')||a.cells[sortCol].textContent;
                        const bv = b.cells[sortCol].getAttribute('data-val')||b.cells[sortCol].textContent;
                        const A = tryParse(av), B = tryParse(bv);
                        if(A.num && B.num) return (A.val - B.val)*sortDir;
                        return A.val.localeCompare(B.val)*sortDir;
                      });
                    }
                    page = 1;
                    render();
                  }

                  function updateCount(){ countInfo.textContent = filtered.length + ' row(s)'; }

                  function render(){
                    const pageSize = parseInt(rowsPerPageSel.value,10);
                    let total = filtered.length;
                    let totalPages = (pageSize === -1) ? 1 : Math.max(1, Math.ceil(total / pageSize));
                    if(page > totalPages) page = totalPages;

                    let start = (pageSize === -1) ? 0 : (page-1)*pageSize;
                    let end = (pageSize === -1) ? total : Math.min(total, start + pageSize);

                    const frag = document.createDocumentFragment();
                    for(let i=start;i<end;i++) frag.appendChild(filtered[i]);
                    tbody.innerHTML = '';
                    tbody.appendChild(frag);

                    pageInfo.textContent = (pageSize===-1) ? `All rows` : `Page ${page} of ${Math.max(1, totalPages)}`;
                    prevBtn.disabled = (page <= 1) || (pageSize===-1);
                    nextBtn.disabled = (page >= totalPages) || (pageSize===-1);
                  }

                  // events
                  headers.forEach((h,idx)=> h.addEventListener('click', ()=>applySort(idx)));
                  searchBox.addEventListener('input', applyFilter);
                  rowsPerPageSel.addEventListener('change', ()=>{ page=1; render(); });
                  prevBtn.addEventListener('click', ()=>{ if(page>1){ page--; render(); } });
                  nextBtn.addEventListener('click', ()=>{ page++; render(); });

                  // initial
                  updateCount();
                  render();
                })();
                </script>
                </body></html>");

                return sb.ToString();

                // ---------------- helpers ----------------
                string GetCellCss(DataColumn col, object v)
                {
                    if (v == null || v == DBNull.Value) return "";
                    var t = col.DataType;

                    if (IsNumericType(t)) return "num";
                    if (t == typeof(DateTime) || col.ColumnName.EndsWith("Date", StringComparison.OrdinalIgnoreCase)) return "date";
                    if (t == typeof(TimeSpan) || col.ColumnName.EndsWith("Time", StringComparison.OrdinalIgnoreCase)) return "time";
                    return "";
                }

                string FormatDisplay(object v, DataColumn col)
                {
                    if (v == null || v == DBNull.Value) return "<span class='muted'>&nbsp;</span>";

                    var t = col.DataType;

                    // TIME -> hh:mm
                    if (t == typeof(TimeSpan) || col.ColumnName.EndsWith("Time", StringComparison.OrdinalIgnoreCase))
                    {
                        if (v is TimeSpan ts) return HttpUtility.HtmlEncode(ts.ToString(@"hh\:mm"));
                        return HttpUtility.HtmlEncode(Convert.ToString(v));
                    }

                    // DATE -> dd-MMM-yyyy
                    if (t == typeof(DateTime) || col.ColumnName.EndsWith("Date", StringComparison.OrdinalIgnoreCase))
                    {
                        if (v is DateTime dtv) return HttpUtility.HtmlEncode(dtv.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture));
                        DateTime parsed;
                        if (DateTime.TryParse(Convert.ToString(v), out parsed))
                            return HttpUtility.HtmlEncode(parsed.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture));
                    }

                    // Boolean as pills (keeps your IsActive style)
                    if (t == typeof(bool) || string.Equals(col.ColumnName, "IsActive", StringComparison.OrdinalIgnoreCase))
                    {
                        bool b = ToBool(v);
                        return $"<span class='pill {(b ? "pill-yes" : "pill-no")}'>{(b ? "Yes" : "No")}</span>";
                    }

                    // Numeric -> plain text (right-aligned via CSS)
                    if (IsNumericType(t))
                    {
                        return HttpUtility.HtmlEncode(Convert.ToString(v, CultureInfo.InvariantCulture));
                    }

                    return HttpUtility.HtmlEncode(Convert.ToString(v));
                }

                // Value used for JS sorting/filtering
                string RawSortableValue(object v, DataColumn col)
                {
                    if (v == null || v == DBNull.Value) return "";

                    var t = col.DataType;

                    // For times: use hh:mm:ss sortable numeric via JS (we keep hh:mm:ss text)
                    if (t == typeof(TimeSpan) || col.ColumnName.EndsWith("Time", StringComparison.OrdinalIgnoreCase))
                    {
                        if (v is TimeSpan ts) return ts.ToString(@"hh\:mm\:ss");
                        return Convert.ToString(v);
                    }

                    // For dates: ISO yyyy-MM-dd to enable numeric-like sorting in JS
                    if (t == typeof(DateTime) || col.ColumnName.EndsWith("Date", StringComparison.OrdinalIgnoreCase))
                    {
                        if (v is DateTime dtv) return dtv.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        DateTime parsed;
                        if (DateTime.TryParse(Convert.ToString(v), out parsed))
                            return parsed.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        return Convert.ToString(v);
                    }

                    // Numeric: invariant culture to keep '.' decimal point
                    if (IsNumericType(t))
                        return Convert.ToString(v, CultureInfo.InvariantCulture);

                    return Convert.ToString(v);
                }

                bool ToBool(object v)
                {
                    if (v is bool b) return b;
                    if (v is byte by) return by != 0;
                    if (v is short s) return s != 0;
                    if (v is int i) return i != 0;
                    var s2 = Convert.ToString(v);
                    return s2 == "1" || s2.Equals("true", StringComparison.OrdinalIgnoreCase) || s2.Equals("yes", StringComparison.OrdinalIgnoreCase);
                }

                bool IsNumericType(Type t)
                {
                    return t == typeof(byte) || t == typeof(sbyte) ||
                           t == typeof(short) || t == typeof(ushort) ||
                           t == typeof(int) || t == typeof(uint) ||
                           t == typeof(long) || t == typeof(ulong) ||
                           t == typeof(float) || t == typeof(double) ||
                           t == typeof(decimal);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "BuildWebViewHtml",
                 MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        } //
    }
}

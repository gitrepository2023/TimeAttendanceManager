using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using ClosedXML.Excel;

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
///    Name:           ExcelExportHelpers.cs
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


namespace TimeAttendanceManager.Helpers.Classes
{
    public static class ExcelExportHelpers
    {
        /// <summary>
        /// Exports a DataTable to an .xlsx file with optional custom headers.
        /// - DateTime columns are formatted as dd-MMM-yyyy
        /// - TimeSpan (or column name ending with "Time") formatted as hh:mm (written as text to keep it simple)
        /// - Numeric types are written as numbers (right-aligned by Excel)
        /// - Header row is frozen, auto-filtered, and columns auto-fit
        /// ExcelExportHelpers.ExportDataTableToExcel(_shiftDt, @"D:\Exports\ShiftSchedules.xlsx", headers);
        /// </summary>
        public static void ExportDataTableToExcel(
            DataTable dt, 
            string path, 
            IDictionary<string, string> customHeaders = null)
        {
            if (dt == null) throw new ArgumentNullException(nameof(dt));
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException(nameof(path));

            // Ensure directory exists
            var dir = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            using (var wb = new XLWorkbook())
            {
                string sheetName = string.IsNullOrWhiteSpace(dt.TableName) ? "Sheet1" : SanitizeSheetName(dt.TableName);
                var ws = wb.Worksheets.Add(sheetName);

                int cols = dt.Columns.Count;
                int rows = dt.Rows.Count;

                // 1) Headers
                for (int c = 0; c < cols; c++)
                {
                    var col = dt.Columns[c];
                    string header = (customHeaders != null && customHeaders.TryGetValue(col.ColumnName, out var h))
                                    ? h : col.ColumnName;
                    ws.Cell(1, c + 1).Value = header;
                }

                // 2) Data
                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < cols; c++)
                    {
                        var col = dt.Columns[c];
                        object raw = dt.Rows[r][c];

                        var cell = ws.Cell(r + 2, c + 1);
                        WriteCell(cell, raw, col);
                    }
                }

                // 3) Table / styling / usability
                var range = ws.Range(1, 1, Math.Max(1, rows + 1), Math.Max(1, cols));
                var table = range.CreateTable();
                table.Theme = XLTableTheme.TableStyleMedium2;
                table.ShowAutoFilter = true;

                // Freeze header
                ws.SheetView.FreezeRows(1);

                // Auto fit columns
                ws.Columns().AdjustToContents();

                // Save
                wb.SaveAs(path);
            }
        }

        private static void WriteCell(ClosedXML.Excel.IXLCell cell, object value, DataColumn col)
        {
            if (value == null || value == DBNull.Value)
            {
                cell.Value = string.Empty;
                cell.Style.NumberFormat.Format = "@"; // treat as text
                return;
            }

            var t = col.DataType;

            // TIME: write as Excel time serial (fraction of day) and format "hh:mm"
            if (t == typeof(TimeSpan) || col.ColumnName.EndsWith("Time", StringComparison.OrdinalIgnoreCase))
            {
                if (value is TimeSpan ts)
                {
                    cell.Value = ts.TotalDays;                 // Excel time serial
                    cell.Style.NumberFormat.Format = "hh:mm";  // shows 00:00 - 23:59
                }
                else
                {
                    // fallback: write as text if not a TimeSpan
                    cell.Value = Convert.ToString(value, System.Globalization.CultureInfo.InvariantCulture);
                    cell.Style.NumberFormat.Format = "@";
                }
                return;
            }

            // DATE: format as dd-MMM-yyyy
            if (t == typeof(DateTime))
            {
                var dtv = (DateTime)value;
                cell.Value = dtv;
                cell.Style.DateFormat.Format = "dd-MMM-yyyy";
                return;
            }
            else if (col.ColumnName.EndsWith("Date", StringComparison.OrdinalIgnoreCase))
            {
                if (DateTime.TryParse(Convert.ToString(value, System.Globalization.CultureInfo.InvariantCulture), out var parsed))
                {
                    cell.Value = parsed;
                    cell.Style.DateFormat.Format = "dd-MMM-yyyy";
                    return;
                }
            }

            // NUMERIC: write as numbers
            if (IsNumericType(t))
            {
                // decimals get 2 dp, integers 0 dp
                if (t == typeof(decimal) || t == typeof(double) || t == typeof(float))
                {
                    cell.Value = Convert.ToDouble(value, System.Globalization.CultureInfo.InvariantCulture);
                    cell.Style.NumberFormat.Format = "#,##0.00";
                }
                else
                {
                    cell.Value = Convert.ToDouble(value, System.Globalization.CultureInfo.InvariantCulture);
                    cell.Style.NumberFormat.Format = "#,##0";
                }
                return;
            }

            // BOOL → Yes/No (as text)
            if (t == typeof(bool) || col.ColumnName.Equals("IsActive", StringComparison.OrdinalIgnoreCase))
            {
                bool b = ToBool(value);
                cell.Value = b ? "Yes" : "No";
                cell.Style.NumberFormat.Format = "@";
                return;
            }

            // Default: write as text
            cell.Value = Convert.ToString(value, System.Globalization.CultureInfo.InvariantCulture);
            cell.Style.NumberFormat.Format = "@";
        }

        private static bool IsNumericType(Type t)
        {
            return t == typeof(byte) || t == typeof(sbyte) ||
                   t == typeof(short) || t == typeof(ushort) ||
                   t == typeof(int) || t == typeof(uint) ||
                   t == typeof(long) || t == typeof(ulong) ||
                   t == typeof(float) || t == typeof(double) ||
                   t == typeof(decimal);
        }

        private static bool ToBool(object v)
        {
            if (v is bool b) return b;
            if (v is byte by) return by != 0;
            if (v is short s) return s != 0;
            if (v is int i) return i != 0;
            var s2 = Convert.ToString(v, System.Globalization.CultureInfo.InvariantCulture);
            return s2 == "1" || s2.Equals("true", StringComparison.OrdinalIgnoreCase) || s2.Equals("yes", StringComparison.OrdinalIgnoreCase);
        }

        private static string SanitizeSheetName(string name)
        {
            // Excel sheet name rules: max 31 chars, no : \ / ? * [ ]
            var invalid = new[] { ':', '\\', '/', '?', '*', '[', ']' };
            foreach (var ch in invalid) name = name.Replace(ch, ' ');
            if (name.Length > 31) name = name.Substring(0, 31);
            if (string.IsNullOrWhiteSpace(name)) name = "Sheet1";
            return name;
        }
    }
}

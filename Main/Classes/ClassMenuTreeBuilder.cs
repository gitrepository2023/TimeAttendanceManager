using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient; // For .NET 5+ consider Microsoft.Data.SqlClient
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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
///    Name:           ClassMenuTreeBuilder.cs
///    Created:        20th August 2025
///    Date Completed: 20th August 2025
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

namespace TimeAttendanceManager.Main.Classes
{
    public static class ClassMenuTreeBuilder
    {
        #region "LoadMenuTreeAsync"
        /// <summary>
        /// Load Menu Tree asynchronously into a TreeView control
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="unitCode"></param>
        /// <param name="empCatgId"></param>
        /// <param name="searchText"></param>
        /// <param name="treeViewFont"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public static async Task LoadMenuTreeAsync(
            TreeView treeView,
            string unitCode = null,          
            int? empCatgId = null,
            string searchText = null,
            Font treeViewFont = null,
            CancellationToken ct = default)
        {
            // 1) Fetch the data
            var dt = await FetchMenuDataAsync(
                userRowGuid: ClassGlobalVariables.pubLoginUserRowGuid,
                unitCode: unitCode,
                empCatgId: empCatgId,
                searchText: searchText,
                ct);

            // 2) Build the tree structure in memory
            var roots = BuildTreeNodes(dt, treeViewFont);

            // 3) Apply to UI
            if (treeView.InvokeRequired)
            {
                treeView.Invoke((MethodInvoker)(() => ApplyNodes(treeView, roots)));
            }
            else
            {
                ApplyNodes(treeView, roots);
            }
        }
        #endregion

        #region "FetchMenuDataAsync"
        /// <summary>
        /// Fetch menu data from database based on parameters     
        /// </summary>
        /// <param name="userRowGuid"></param>
        /// <param name="unitCode"></param>
        /// <param name="empCatgId"></param>
        /// <param name="searchText"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        private static async Task<DataTable> FetchMenuDataAsync(
            string userRowGuid = null,
            string unitCode = null,
            int? empCatgId = null,
            string searchText = null,
            CancellationToken ct = default)
        {
            var dt = new DataTable();

            const string tableName = "dbo.v_AdminMenuStructures";

            var sb = new StringBuilder();
            var parameters = new List<SqlParameter>();

            // Replace '*' with '%' for SQL LIKE query
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                searchText = searchText.Replace("*", "%");
            }
            else
            {
                searchText = null; // normalize empty to null
            }

            string connectionString = ClassGlobalFunctions.GetConnectionStringByUnitCode(unitCode);

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Connection string cannot be empty");
            }

            sb.AppendLine("SELECT *");
            sb.AppendLine($"FROM {tableName} AS m");
            sb.AppendLine("WHERE 1 = 1");
            sb.AppendLine("  AND m.MenuIsActive = 1");
            sb.AppendLine("  AND m.IsDeleted = 0");
            sb.AppendLine("  AND m.UnitCode = @UnitCode");

            // Only menus that the user can view
            sb.AppendLine("  AND EXISTS (");
            sb.AppendLine("        SELECT 1");
            sb.AppendLine("        FROM dbo.AdminMenuUserPerms AS p");
            sb.AppendLine("        WHERE p.UserRowGuid = @UserRowGuid");
            sb.AppendLine("          AND p.MenuId = m.Id");      // m.Id must be exposed by the view
            sb.AppendLine("          AND p.CanView = 1");
            sb.AppendLine("  )");

            // Optional filter by employee category
            sb.AppendLine("  AND (@MenuEmpCatgId IS NULL OR m.MenuEmpCatgId = @MenuEmpCatgId)");

            var serachColumns = new List<string> {
                    "EmpCatgName",
                    "ParentTitle",
                    "MenuFormTitle",
                    "MenuFormName"};

            // Add search conditions if search text is provided
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                searchText = searchText.Trim().ToLower().Replace("*", "%");

                if (serachColumns != null && serachColumns.Count > 0)
                {
                    sb.Append(" AND (");
                    var searchClauses = new List<string>();

                    for (int i = 0; i < serachColumns.Count; i++)
                    {
                        string paramName = $"@SearchText{i}";
                        searchClauses.Add($"{serachColumns[i]} LIKE {paramName}");
                        parameters.Add(new SqlParameter(paramName, $"%{searchText}%"));
                    }

                    sb.Append(string.Join(" OR ", searchClauses));
                    sb.Append(")");
                }
            }
            sb.Append("ORDER BY [EmpCatgSortOrder], [MenuSortOrder], [ParentTitle] ");

            // parameters
            parameters.Add(new SqlParameter("@UnitCode", SqlDbType.VarChar, 50) { Value = unitCode });
            parameters.Add(new SqlParameter("@MenuEmpCatgId", SqlDbType.Int) { Value = empCatgId.Value });
            parameters.Add(new SqlParameter("@UserRowGuid", SqlDbType.NVarChar) { Value = userRowGuid });

            var query = sb.ToString();

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                if (parameters.Count > 0)
                {
                    cmd.Parameters.AddRange(parameters.ToArray());
                }
                await conn.OpenAsync(ct).ConfigureAwait(false);
                using (var reader = await cmd.ExecuteReaderAsync(ct).ConfigureAwait(false))
                {
                    dt.Load(reader);
                }
            }

            return dt;
        }

        #endregion

        #region "BuildTreeNodes"
        /// <summary>
        /// Build TreeNodes from DataTable 
        /// var nodes = BuildTreeNodes(myDataTable, TreeViewMain.Font);
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="treeViewFont"></param>
        /// <returns></returns>
        private static List<TreeNode> BuildTreeNodes(DataTable dt, Font treeViewFont)
        {
            var roots = new List<TreeNode>();
            if (dt == null || dt.Rows.Count == 0) return roots;

            // create a single bold font derived from the tree's font and reuse it
            // Note: owner should dispose this font when nodes/tree are discarded (see notes).
            var boldFont = new Font(treeViewFont, FontStyle.Bold);

            try
            {
                var byUnit = dt.AsEnumerable()
                    .GroupBy(r => (r["UnitAliasName"]?.ToString() ?? string.Empty).Trim(), StringComparer.OrdinalIgnoreCase);

                foreach (var unitGroup in byUnit)
                {
                    var unitName = string.IsNullOrWhiteSpace(unitGroup.Key) ? "(Unknown Unit)" : unitGroup.Key;
                    var unitRoot = new TreeNode(unitName) { Name = unitName };
                    roots.Add(unitRoot);

                    unitRoot.ForeColor = Color.DarkRed;
                    unitRoot.ImageKey = "store-small.png";
                    unitRoot.SelectedImageKey = "store-small.png";

                    var categories = unitGroup
                        .Where(r => string.Equals(r["MenuNodeType"]?.ToString(), "Category", StringComparison.OrdinalIgnoreCase))
                        .OrderBy(r => SafeInt(r["MenuSortOrder"]))
                        .ThenBy(r => (r["MenuFormTitle"]?.ToString() ?? string.Empty));

                    var categoryMap = new Dictionary<string, TreeNode>(StringComparer.OrdinalIgnoreCase);

                    foreach (var catRow in categories)
                    {
                        var catTitle = (catRow["MenuFormTitle"]?.ToString() ?? string.Empty).Trim();
                        if (string.IsNullOrWhiteSpace(catTitle)) continue;

                        var key = (catRow["Id"]?.ToString() ?? catTitle).Trim();
                        if (categoryMap.ContainsKey(key)) continue;

                        var catNode = new TreeNode(catTitle)
                        {
                            Name = key,
                            Tag = null
                        };
                        categoryMap[key] = catNode;
                        unitRoot.Nodes.Add(catNode);

                        catNode.ForeColor = Color.DarkBlue;
                        catNode.ImageKey = "folders16x16.png";
                        catNode.SelectedImageKey = "folders16x16.png";

                        // Use the single boldFont we created
                        catNode.NodeFont = boldFont;
                    }

                    // commands logic (unchanged)...
                    var commands = unitGroup
                        .Where(r => string.Equals(r["MenuNodeType"]?.ToString(), "Command", StringComparison.OrdinalIgnoreCase))
                        .OrderBy(r => SafeInt(r["MenuSortOrder"]))
                        .ThenBy(r => (r["MenuFormTitle"]?.ToString() ?? string.Empty));

                    foreach (var cmdRow in commands)
                    {
                        var title = (cmdRow["MenuFormTitle"]?.ToString() ?? string.Empty).Trim();
                        var formName = (cmdRow["MenuFormName"]?.ToString() ?? string.Empty).Trim();
                        if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(formName)) continue;

                        var node = new TreeNode(title)
                        {
                            Name = (cmdRow["Id"]?.ToString() ?? title).Trim(),
                            Tag = formName
                        };

                        node.ForeColor = Color.Blue;
                        node.ImageKey = "notebook16x16.png";
                        node.SelectedImageKey = "tick_small16x16.png";

                        var parentTitle = (cmdRow["ParentTitle"]?.ToString() ?? string.Empty).Trim();
                        if (!string.IsNullOrWhiteSpace(parentTitle))
                        {
                            var parentIdKey = (cmdRow["ParentId"]?.ToString() ?? string.Empty).Trim();
                            TreeNode parentNode = null;

                            if (!string.IsNullOrWhiteSpace(parentIdKey) && categoryMap.TryGetValue(parentIdKey, out var byId))
                                parentNode = byId;
                            else
                                parentNode = categoryMap.Values.FirstOrDefault(n =>
                                    string.Equals(n.Text, parentTitle, StringComparison.OrdinalIgnoreCase));

                            if (parentNode != null)
                                parentNode.Nodes.Add(node);
                            else
                                unitRoot.Nodes.Add(node);
                        }
                        else
                        {
                            unitRoot.Nodes.Add(node);
                        }
                    }
                }

                // Important: do NOT dispose boldFont here, nodes reference it. Caller must dispose when done.
                return roots;
            }
            catch
            {
                // if exception, we should not leave an unmanaged font undisposed in some flows.
                // Better approach: make caller manage boldFont lifecycle; kept simple here.
                throw;
            }
        }

        #endregion

        #region "ApplyNodes"
        /// <summary>
        /// Apply the built nodes to the TreeView control     
        /// </summary>
        /// <param name="tv"></param>
        /// <param name="roots"></param>
        private static void ApplyNodes(TreeView tv, List<TreeNode> roots)
        {
            tv.BeginUpdate();
            try
            {
                tv.Nodes.Clear();
                tv.Nodes.AddRange(roots.ToArray());
                tv.ExpandAll(); // optional
            }
            finally
            {
                tv.EndUpdate();
            }
        }
        #endregion

        private static int SafeInt(object o)
            => int.TryParse(o?.ToString(), out var v) ? v : int.MaxValue;
    }
}

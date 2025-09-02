using TimeAttendanceManager.Main.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
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
/// <Class>
///    Title:          Create the Helper Class
///
///                    
///    Name:           UserTreeBuilder.cs
///    Created:        21st August 2025
///    Date Completed: 21st August 2025
/// </Class >
/// 
/// <classChangeLog>
///   Date Modified:   NA
/// 
/// </classChangeLog>
/// 
/// <databaseDetails>
///   Database Name:     
/// </databaseDetails>
/// 
/// <tablesUsed>
///  
/// 
/// </tablesUsed>
/// 
/// <remarks>
///    
/// </remarks>
/// 

namespace TimeAttendanceManager.Auth.Classes
{
    public static class UserTreeBuilder
    {

        #region "LoadUserTreeAsync"
        /// <summary>
        /// Builds a TreeView from dbo.v_UserLogins:
        /// Root = CompanyAliasName → DepartmentCode → UserName (Tag = UserRowGuid)
        /// Filters by @UnitCode and @RoleName. Includes checkboxes, images, and search support.
        /// </summary>
        public static async Task LoadUserTreeAsync(
            TreeView tree,
            string unitCode = null,
            string roleName = null,
            string searchText = null,
            CancellationToken ct = default)
        {
            if (tree == null) throw new ArgumentNullException(nameof(tree));
            if (string.IsNullOrWhiteSpace(unitCode)) throw new ArgumentException("UnitCode is required.");

            tree.CheckBoxes = false;
            tree.HideSelection = false;
            tree.FullRowSelect = true;
            tree.ShowNodeToolTips = true;
            tree.PathSeparator = " ▶ ";

            // Setup images once
            // Create ImageList: Makes a new 32-bit color image list with 16x16 pixel images
            if (tree.ImageList == null)
            {
                var imgs = new ImageList { ColorDepth = ColorDepth.Depth32Bit, ImageSize = new Size(16, 16) };
                imgs.Images.Add("company", SystemIcons.Application.ToBitmap());
                imgs.Images.Add("dept", SystemIcons.Information.ToBitmap());
                imgs.Images.Add("user", SystemIcons.Shield.ToBitmap());
                tree.ImageList = imgs;
            }

            // Fetch data
            var dt = await FetchUserLoginsAsync(
                unitCode: unitCode,
                roleName: roleName,
                searchText: searchText,
                ct);
            ct.ThrowIfCancellationRequested();

            // Build the tree
            tree.BeginUpdate();
            try
            {
                tree.Nodes.Clear();

                // Group: CompanyAliasName -> DepartmentCode -> Users
                var rows = dt.AsEnumerable()
                    .OrderBy(r => r.Field<string>("CompanyAliasName"))
                    .ThenBy(r => r.Field<string>("DepartmentCode"))
                    .ThenBy(r => r.Field<string>("UserName"));

                // Company level
                foreach (var companyGroup in rows.GroupBy(r => r.Field<string>("CompanyAliasName") ?? "(No Company)"))
                {
                    var companyNode = new TreeNode(companyGroup.Key)
                    {
                        ImageKey = "company",
                        SelectedImageKey = "company",
                        ToolTipText = companyGroup.First().Field<string>("CompanyName") ?? companyGroup.Key
                    };

                    // Department level
                    foreach (var deptGroup in companyGroup.GroupBy(r => r.Field<string>("DepartmentCode") ?? "(No Dept)"))
                    {
                        string deptName = deptGroup.First().Field<string>("DepartmentName") ?? deptGroup.Key;
                        var deptNode = new TreeNode($"{deptGroup.Key} - {deptName}")
                        {
                            ImageKey = "dept",
                            SelectedImageKey = "dept",
                            ToolTipText = deptName
                        };

                        // User level
                        foreach (var row in deptGroup)
                        {
                            string userName = row.Field<string>("UserName") ?? "(Unknown)";
                            string role = row.Field<string>("RoleName");
                            var userGuid = row.Field<Guid>("UserRowGuid");

                            var userNode = new TreeNode(userName)
                            {
                                ImageKey = "user",
                                SelectedImageKey = "user",
                                Tag = userGuid.ToString(),
                                ToolTipText = string.IsNullOrEmpty(role) ? userName : $"{userName} — {role}"
                            };

                            deptNode.Nodes.Add(userNode);
                        }

                        companyNode.Nodes.Add(deptNode);
                    }

                    tree.Nodes.Add(companyNode);
                }

                tree.ExpandAll();
            }
            finally
            {
                tree.EndUpdate();
            }
        }
        #endregion

        #region "FetchUserLoginsAsync"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitCode"></param>
        /// <param name="roleName"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        private static async Task<DataTable> FetchUserLoginsAsync(
            string unitCode = null, 
            string roleName = null,
            string searchText = null,
            CancellationToken ct = default)
        {
            var dt = new DataTable();

            var serachColumns = new List<string> {
                    "UnitCode",
                    "UserName",
                    "DepartmentCode"};

            // Build query with parameters @UnitCode and optional @RoleName
            var sb = new System.Text.StringBuilder();
            var parameters = new List<SqlParameter>();

            sb.AppendLine("SELECT ");
            sb.AppendLine("* ");
            sb.AppendLine("FROM [dbo].[v_UserLogins]");
            sb.AppendLine("WHERE 1 = 1");
            sb.AppendLine(" AND UnitCode = @UnitCode");
            sb.AppendLine(" AND ISNULL(IsDeleted,0) = 0");
            sb.AppendLine(" AND ISNULL(IsActive,0) = 1");

            parameters.Add(new SqlParameter("@UnitCode", SqlDbType.VarChar) { Value = unitCode });
            if (!string.IsNullOrWhiteSpace(roleName))
            {
                sb.AppendLine(" AND RoleName = @RoleName");
                parameters.Add(new SqlParameter("@RoleName", SqlDbType.VarChar) { Value = roleName });
            }

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

            sb.AppendLine("ORDER BY CompanyAliasName, DepartmentCode, UserName;");

            string connectionString = ClassGlobalFunctions.GetConnectionStringByUnitCode(unitCode);

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Connection string cannot be empty");
            }

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sb.ToString(), conn))
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


    }
}

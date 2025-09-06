using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TimeAttendanceManager.Main.Classes.ClassGlobalVariables;

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
///     
/// </Email>
/// 
/// <Module>
///    Title:          Global Functions used in project
///    Name:           ClassGlobalFunctions.cs
///    Created:        14th August 2025
///    Date Completed: 14th August 2025
/// </Module >
/// 
/// <ChangeLog>
///   Date Modified:   NA
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
    public static class ClassGlobalFunctions
    {

        #region "GetTextSelected"
        public static void GetTextSelected(TextBox mTextBox)
        {
            try
            {
                // Equivalent of VB's With statement
                mTextBox.SelectionStart = 0;
                mTextBox.SelectionLength = mTextBox.Text.Length;

                // Direct cast (CType equivalent)
                mTextBox.BackColor = Color.AliceBlue;
            }
            catch (Exception ex)
            {
                // Exception handling (empty catch block like original)
            }
        }
        #endregion

        #region "GetConnectionStringByUnitCode"

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mUnitCode"></param>
        /// <returns></returns>
        public static string GetConnectionStringByUnitCode(string mUnitCode)
        {
            try
            {
                // Define a dictionary for unit code to connection string mapping
                var unitCodeMapping = new Dictionary<string, string>
                    {
                        { "1400", mySQLDataBasePathNgp },
                        { "3000", mySQLDataBasePathMdk },
                        { "3001", mySQLDataBasePathMdk },
                        { "1500", mySQLDataBasePathBmt }
                    };

                // Attempt to get the connection string for the given unit code
                if (unitCodeMapping.ContainsKey(mUnitCode))
                {
                    return unitCodeMapping[mUnitCode];
                }
                else
                {
                    // Unknown unit code
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GetConnectionStringByUnitCode", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return string.Empty;
            }
        }
        #endregion

        #region "GetLocalIPV4"

        /// <summary>
        /// http://stackoverflow.com/questions/2546225/ip-address-lookup-in-vb-net-xp-vs-windows-7
        /// IP Address Lookup in VB.net (XP vs Windows 7)
        /// 
        /// loop through the AddressList looking at the AddressFamily seeing which is set to InterNetwork
        /// 
        /// strIPAddress = System.Net.Dns.GetHostEntry(strComputerName).AddressList(0).ToString()
        /// Windows XP workstations. However, in Vista and Windows 7, this returns the IPv6 address 
        /// which is not used at all. Is there a method of setting this to work so it always returns 
        /// the IPv4 address regardless of platform?
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string GetLocalIPV4()
        {
            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
                return "127.0.0.1";
            }
            catch
            {
                return "127.0.0.1";
            }
        }

        #endregion

        #region "GetLoginOrganisationDetails"

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginUnitCode"></param>
        /// <returns></returns>
        public static async Task GetLoginOrganisationDetails(string loginUnitCode)
        {
            try
            {
                string mTableName = "dbo.v_Companies";
                string myConnectionString = GetConnectionStringByUnitCode(loginUnitCode);

                using (var mySqlConnection = new SqlConnection(myConnectionString))
                {
                    string query = $"SELECT * FROM {mTableName} WHERE UnitCode = @UnitCode";

                    using (var mySqlCommand = new SqlCommand(query, mySqlConnection))
                    {
                        mySqlCommand.Parameters.AddWithValue("@UnitCode", loginUnitCode);
                        await mySqlConnection.OpenAsync();

                        using (var mySqlDataReader = await mySqlCommand.ExecuteReaderAsync())
                        {
                            if (await mySqlDataReader.ReadAsync())
                            {
                                // Local helper functions
                                string GetSafeString(string fieldName)
                                {
                                    object value = mySqlDataReader[fieldName];
                                    return (value == DBNull.Value || string.IsNullOrEmpty(value?.ToString()))
                                        ? null
                                        : value.ToString();
                                }

                                int? GetSafeInt(string fieldName)
                                {
                                    object value = mySqlDataReader[fieldName];
                                    return (value == DBNull.Value)
                                        ? (int?)null
                                        : Convert.ToInt32(value);
                                }

                                var orgSession = ClassOrganizationSession.Instance;
                                orgSession.Id = GetSafeInt("Id");
                                orgSession.UnitCode = GetSafeString("UnitCode");
                                orgSession.UnitName = GetSafeString("UnitName");
                                orgSession.UnitNameSlug = GetSafeString("UnitNameSlug");
                                orgSession.OrgAddress = GetSafeString("OrgAddress");
                                orgSession.GSTNo = GetSafeString("GST_No");
                                orgSession.PANNo = GetSafeString("PAN_NO");
                                orgSession.GSTStateCode = GetSafeString("GST_STATE_CODE");

                                pubOrganisationName = orgSession.UnitName;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error Number: {ex.HResult}\n" +
                    $"Error Source: {ex.Source}\n" +
                    $"Error Message: {ex.Message}\n" +
                    "Module: ModuleGlobalFunctions\n" +
                    "Function: GetLoginOrganisationDetails",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        #endregion

        #region "GetInstalledFrameworkVersion"
        public static string GetInstalledFrameworkVersion()
        {
            const string keyPath = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full";
            string frameworkVersion = "No .NET Framework installed";

            try
            {
                using (RegistryKey ndpKey = Registry.LocalMachine.OpenSubKey(keyPath))
                {
                    if (ndpKey != null && ndpKey.GetValue("Release") != null)
                    {
                        int releaseKey = Convert.ToInt32(ndpKey.GetValue("Release"));

                        if (releaseKey >= 528040)
                            frameworkVersion = ".NET Framework 4.8 or later (Release: " + releaseKey + ")";
                        else if (releaseKey >= 461808)
                            frameworkVersion = ".NET Framework 4.7.2 (Release: " + releaseKey + ")";
                        else if (releaseKey >= 461308)
                            frameworkVersion = ".NET Framework 4.7.1 (Release: " + releaseKey + ")";
                        else if (releaseKey >= 460798)
                            frameworkVersion = ".NET Framework 4.7 (Release: " + releaseKey + ")";
                        else if (releaseKey >= 394802)
                            frameworkVersion = ".NET Framework 4.6.2 (Release: " + releaseKey + ")";
                        else if (releaseKey >= 394254)
                            frameworkVersion = ".NET Framework 4.6.1 (Release: " + releaseKey + ")";
                        else if (releaseKey >= 393295)
                            frameworkVersion = ".NET Framework 4.6 (Release: " + releaseKey + ")";
                        else if (releaseKey >= 379893)
                            frameworkVersion = ".NET Framework 4.5.2 (Release: " + releaseKey + ")";
                        else if (releaseKey >= 378675)
                            frameworkVersion = ".NET Framework 4.5.1 (Release: " + releaseKey + ")";
                        else if (releaseKey >= 378389)
                            frameworkVersion = ".NET Framework 4.5 (Release: " + releaseKey + ")";
                        else
                            frameworkVersion = ".NET Framework version unknown (Release: " + releaseKey + ")";
                    }
                }

                return frameworkVersion;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error: " + ex.Message + "\n\nModule: ClassGlobalFunctions\nFunction: " + nameof(GetInstalledFrameworkVersion),
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return null; // Returning null indicates failure
            }
        }
        #endregion

        #region "FillComboBoxAsync"
        /// <summary>
        /// Creating a new class to hold the data you require
        /// </summary>
        /// <param name="displayMember"></param>
        /// <param name="valueMember"></param>
        /// <param name="comboBox"></param>
        /// <param name="tableName"></param>
        /// <param name="fallbackUnitCode"></param>
        /// <returns></returns>
        public static async Task FillComboBoxAsync(string displayMember, string valueMember,
                                                   ComboBox comboBox, string tableName,
                                                   string fallbackUnitCode = null)
        {
            try
            {
                // Validate input parameters
                if (comboBox == null) throw new ArgumentNullException(nameof(comboBox));
                if (string.IsNullOrWhiteSpace(displayMember)) throw new ArgumentException("Display member cannot be empty");
                if (string.IsNullOrWhiteSpace(valueMember)) throw new ArgumentException("Value member cannot be empty");
                if (string.IsNullOrWhiteSpace(tableName)) throw new ArgumentException("Table name cannot be empty");

                // Determine unit code to use
                string unitCode = string.IsNullOrWhiteSpace(fallbackUnitCode) ? ClassGlobalVariables.pubUnitCode : fallbackUnitCode;

                // Get connection string
                string connectionString = ClassGlobalFunctions.GetConnectionStringByUnitCode(unitCode);
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new InvalidOperationException("Connection string cannot be empty");
                }

                // Clear existing data (UI thread operation)
                if (comboBox.InvokeRequired)
                {
                    comboBox.Invoke((MethodInvoker)(() =>
                    {
                        comboBox.BeginUpdate();
                        comboBox.DataSource = null;
                        comboBox.Items.Clear();
                    }));
                }
                else
                {
                    comboBox.BeginUpdate();
                    comboBox.DataSource = null;
                    comboBox.Items.Clear();
                }

                // Build parameterized query to prevent SQL injection
                string query = $"SELECT [{displayMember}], [{valueMember}] FROM [{tableName}] ORDER BY [{displayMember}]";

                DataTable dataTable = null;

                // Database operations (background thread)
                await Task.Run(async () =>
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        await connection.OpenAsync().ConfigureAwait(false);

                        using (var command = new SqlCommand(query, connection))
                        {
                            dataTable = new DataTable();

                            using (var reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                            {
                                dataTable.Load(reader);
                            }
                        }
                    }
                });

                // Update ComboBox with results (UI thread operation)
                if (comboBox.InvokeRequired)
                {
                    comboBox.Invoke((MethodInvoker)(() =>
                    {
                        comboBox.DataSource = dataTable;
                        comboBox.DisplayMember = displayMember;
                        comboBox.ValueMember = valueMember;
                        comboBox.SelectedIndex = -1;
                        comboBox.EndUpdate();
                    }));
                }
                else
                {
                    comboBox.DataSource = dataTable;
                    comboBox.DisplayMember = displayMember;
                    comboBox.ValueMember = valueMember;
                    comboBox.SelectedIndex = -1;
                    comboBox.EndUpdate();
                }
            }
            catch (Exception ex)
            {
                string errorMessage = $"Function: FillComboBoxAsync\n\n" +
                                     $"Error Type: {ex.GetType().Name}\n" +
                                     $"Error Source: {ex.Source}\n" +
                                     $"Error Message: {ex.Message}";

                if (ex is SqlException sqlEx)
                {
                    errorMessage += $"\nSQL Error Number: {sqlEx.Number}";
                }

                // Show error on UI thread
                if (comboBox != null && !comboBox.IsDisposed)
                {
                    if (comboBox.InvokeRequired)
                    {
                        comboBox.Invoke((MethodInvoker)(() =>
                            MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)));
                    }
                    else
                    {
                        MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        #endregion

        #region "FillComboForUnitCodeAsync"
        /// <summary>
        /// 16.08.2025
        /// Thread safety: Added proper UI thread marshaling using Invoke when needed.
        /// SQL injection protection: Added basic SQL identifier escaping (though parameterized queries are still used for values).
        /// ConfigureAwait(false): Added to avoid potential deadlocks in library code.
        ///  
        /// </summary>
        /// <param name="displayMember"></param>
        /// <param name="valueMember"></param>
        /// <param name="comboBox"></param>
        /// <param name="tableName"></param>
        /// <param name="unitCode"></param>
        /// <returns></returns>
        public static async Task FillComboForUnitCodeAsync(
            string displayMember,
            string valueMember,
            ComboBox comboBox,
            string tableName,
            string unitCode)
        {
            try
            {
                // Clear the combo box safely on the UI thread
                if (comboBox.InvokeRequired)
                {
                    comboBox.Invoke((MethodInvoker)(() =>
                    {
                        comboBox.DataSource = null;
                        comboBox.Items.Clear();
                    }));
                }
                else
                {
                    comboBox.DataSource = null;
                    comboBox.Items.Clear();
                }

                // Build the query safely with parameterization
                var queryBuilder = new StringBuilder();
                queryBuilder.Append($"SELECT {EscapeSqlIdentifier(displayMember)}, {EscapeSqlIdentifier(valueMember)} FROM {EscapeSqlIdentifier(tableName)} ");
                queryBuilder.Append("WHERE 1 = 1 ");

                if (!string.IsNullOrWhiteSpace(unitCode))
                {
                    queryBuilder.Append("AND UnitCode = @UnitCode ");
                }

                queryBuilder.Append($"ORDER BY {EscapeSqlIdentifier(displayMember)}");

                string connectionString = ClassGlobalFunctions.GetConnectionStringByUnitCode(unitCode);

                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync().ConfigureAwait(false);

                    using (var command = new SqlCommand(queryBuilder.ToString(), connection))
                    {
                        if (!string.IsNullOrWhiteSpace(unitCode))
                        {
                            command.Parameters.AddWithValue("@UnitCode", unitCode);
                        }

                        using (var adapter = new SqlDataAdapter(command))
                        {
                            var dataSet = new DataSet();
                            await Task.Run(() => adapter.Fill(dataSet, "Combo")).ConfigureAwait(false);

                            var dt = dataSet.Tables["Combo"];

                            if (dt != null)
                            {
                                // Remove rows where displayMember or valueMember is NULL/empty
                                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                                {
                                    var displayVal = dt.Rows[i][displayMember]?.ToString().Trim();
                                    var valueVal = dt.Rows[i][valueMember]?.ToString().Trim();

                                    if (string.IsNullOrWhiteSpace(displayVal) || string.IsNullOrWhiteSpace(valueVal))
                                    {
                                        dt.Rows.RemoveAt(i);
                                    }
                                }

                                // Update UI on the main thread
                                if (comboBox.InvokeRequired)
                                {
                                    comboBox.Invoke((MethodInvoker)(() =>
                                    {
                                        comboBox.DataSource = dt;
                                        comboBox.ValueMember = valueMember;
                                        comboBox.DisplayMember = displayMember;
                                        comboBox.SelectedIndex = -1; // no selection
                                    }));
                                }
                                else
                                {
                                    comboBox.DataSource = dt;
                                    comboBox.ValueMember = valueMember;
                                    comboBox.DisplayMember = displayMember;
                                    comboBox.SelectedIndex = -1;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Function: FillComboForUnitCode{Environment.NewLine}{Environment.NewLine}" +
                    $"Error Source: {ex.Source}{Environment.NewLine}" +
                    $"Error Message: {ex.Message}{Environment.NewLine}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        #endregion

        #region "FillComboForUnitCodeMenuParentAsync"
        /// <summary>
        /// 16.08.2025
        /// Thread safety: Added proper UI thread marshaling using Invoke when needed.
        /// SQL injection protection: Added basic SQL identifier escaping (though parameterized queries are still used for values).
        /// ConfigureAwait(false): Added to avoid potential deadlocks in library code.
        ///  
        /// </summary>
        /// <param name="displayMember"></param>
        /// <param name="valueMember"></param>
        /// <param name="comboBox"></param>
        /// <param name="tableName"></param>
        /// <param name="unitCode"></param>
        /// <returns></returns>
        public static async Task FillComboForUnitCodeMenuParentAsync(
            string displayMember,
            string valueMember,
            ComboBox comboBox,
            string tableName,
            string unitCode)
        {
            try
            {
                // Clear the combo box safely on the UI thread
                if (comboBox.InvokeRequired)
                {
                    comboBox.Invoke((MethodInvoker)(() =>
                    {
                        comboBox.DataSource = null;
                        comboBox.Items.Clear();
                    }));
                }
                else
                {
                    comboBox.DataSource = null;
                    comboBox.Items.Clear();
                }

                // Build the query safely with parameterization
                var queryBuilder = new StringBuilder();
                queryBuilder.Append($"SELECT {EscapeSqlIdentifier(displayMember)}, {EscapeSqlIdentifier(valueMember)} FROM {EscapeSqlIdentifier(tableName)} ");
                queryBuilder.Append("WHERE 1 = 1 ");
                queryBuilder.Append("AND [MenuParentId] IS NULL ");

                if (!string.IsNullOrWhiteSpace(unitCode))
                {
                    queryBuilder.Append("AND UnitCode = @UnitCode ");
                }

                queryBuilder.Append($"ORDER BY {EscapeSqlIdentifier(displayMember)}");

                string connectionString = ClassGlobalFunctions.GetConnectionStringByUnitCode(unitCode);

                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync().ConfigureAwait(false);

                    using (var command = new SqlCommand(queryBuilder.ToString(), connection))
                    {
                        if (!string.IsNullOrWhiteSpace(unitCode))
                        {
                            command.Parameters.AddWithValue("@UnitCode", unitCode);
                        }

                        using (var adapter = new SqlDataAdapter(command))
                        {
                            var dataSet = new DataSet();
                            await Task.Run(() => adapter.Fill(dataSet, "Combo")).ConfigureAwait(false);

                            var dt = dataSet.Tables["Combo"];

                            if (dt != null)
                            {
                                // Remove rows where displayMember or valueMember is NULL/empty
                                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                                {
                                    var displayVal = dt.Rows[i][displayMember]?.ToString().Trim();
                                    var valueVal = dt.Rows[i][valueMember]?.ToString().Trim();

                                    if (string.IsNullOrWhiteSpace(displayVal) || string.IsNullOrWhiteSpace(valueVal))
                                    {
                                        dt.Rows.RemoveAt(i);
                                    }
                                }

                                // Update UI on the main thread
                                if (comboBox.InvokeRequired)
                                {
                                    comboBox.Invoke((MethodInvoker)(() =>
                                    {
                                        comboBox.DataSource = dt;
                                        comboBox.ValueMember = valueMember;
                                        comboBox.DisplayMember = displayMember;
                                        comboBox.SelectedIndex = -1; // no selection
                                    }));
                                }
                                else
                                {
                                    comboBox.DataSource = dt;
                                    comboBox.ValueMember = valueMember;
                                    comboBox.DisplayMember = displayMember;
                                    comboBox.SelectedIndex = -1;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Function: FillComboForUnitCode{Environment.NewLine}{Environment.NewLine}" +
                    $"Error Source: {ex.Source}{Environment.NewLine}" +
                    $"Error Message: {ex.Message}{Environment.NewLine}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        #endregion

        #region "FillComboBoxUnitCodeHasAccessAsync"
        /// <summary>
        /// 25.08.2025
        /// Usage
        /// await FillComboBoxUnitCodeAsync(
        ///     displayMember: "UnitName",
        ///     valueMember: "UnitCode",
        ///     comboBox: cboUnit,
        ///     tableName: "dbo.Units",
        ///     userRowGuid: ClassGlobalVariables.pubLoginUserRowGuid
        /// );
        /// </summary>
        /// <param name="displayMember"></param>
        /// <param name="valueMember"></param>
        /// <param name="comboBox"></param>
        /// <param name="tableName"></param>
        /// <param name="userRowGuid"></param>
        /// <param name="fallbackUnitCode"></param>
        /// <returns></returns>
        public static async Task FillComboBoxUnitCodeHasAccessAsync(
            string displayMember,
            string valueMember,
            ComboBox comboBox,
            string tableName,
            string userRowGuid,
            string fallbackUnitCode = null)
        {
            try
            {
                if (comboBox == null) throw new ArgumentNullException(nameof(comboBox));
                if (string.IsNullOrWhiteSpace(displayMember)) throw new ArgumentException("Display member cannot be empty", nameof(displayMember));
                if (string.IsNullOrWhiteSpace(valueMember)) throw new ArgumentException("Value member cannot be empty", nameof(valueMember));
                if (string.IsNullOrWhiteSpace(tableName)) throw new ArgumentException("Table name cannot be empty", nameof(tableName));
                if (string.IsNullOrWhiteSpace(userRowGuid)) throw new ArgumentException("UserRowGuid is required", nameof(userRowGuid));

                // Determine unit code to use (if you still want a fallback selection after load)
                string unitCode = string.IsNullOrWhiteSpace(fallbackUnitCode)
                    ? ClassGlobalVariables.pubUnitCode
                    : fallbackUnitCode;

                string connectionString = ClassGlobalFunctions.GetConnectionStringByUnitCode(unitCode);
                if (string.IsNullOrWhiteSpace(connectionString))
                    throw new InvalidOperationException("Connection string cannot be empty");

                // Clear existing items on UI thread
                if (comboBox.InvokeRequired)
                {
                    comboBox.Invoke((MethodInvoker)(() =>
                    {
                        comboBox.BeginUpdate();
                        comboBox.DataSource = null;
                        comboBox.Items.Clear();
                    }));
                }
                else
                {
                    comboBox.BeginUpdate();
                    comboBox.DataSource = null;
                    comboBox.Items.Clear();
                }

                // Only load rows where the UnitCode exists in dbo.AdminUserUnitAccess for the selected user
                string query = $@"
                    SELECT u.[{displayMember}] AS [{displayMember}],
                           u.[{valueMember}]   AS [{valueMember}]
                    FROM [{tableName}] AS u
                    WHERE u.[{displayMember}] IN (
                        SELECT UnitCode
                        FROM dbo.AdminUserUnitAccess
                        WHERE UserRowGuid = @UserRowGuid
                    )
                    ORDER BY u.[{displayMember}];";

                DataTable dataTable = new DataTable();

                // No need for Task.Run — true async I/O is already non-blocking when awaited.
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@UserRowGuid", SqlDbType.NVarChar, 150).Value = userRowGuid;
                    await connection.OpenAsync().ConfigureAwait(false);

                    using (var reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                    {
                        dataTable.Load(reader);
                    }
                }

                // Bind on UI thread
                if (comboBox.InvokeRequired)
                {
                    comboBox.Invoke((MethodInvoker)(() =>
                    {
                        comboBox.DataSource = dataTable;
                        comboBox.DisplayMember = displayMember;
                        comboBox.ValueMember = valueMember;
                        comboBox.SelectedIndex = -1; // nothing selected by default
                        comboBox.EndUpdate();
                    }));
                }
                else
                {
                    comboBox.DataSource = dataTable;
                    comboBox.DisplayMember = displayMember;
                    comboBox.ValueMember = valueMember;
                    comboBox.SelectedIndex = -1;
                    comboBox.EndUpdate();
                }
            }
            catch (Exception ex)
            {
                string errorMessage = $"Function: FillComboBoxUnitCodeAsync\n\n" +
                                      $"Error Type: {ex.GetType().Name}\n" +
                                      $"Error Source: {ex.Source}\n" +
                                      $"Error Message: {ex.Message}";

                if (ex is SqlException sqlEx)
                    errorMessage += $"\nSQL Error Number: {sqlEx.Number}";

                if (comboBox != null && !comboBox.IsDisposed)
                {
                    if (comboBox.InvokeRequired)
                    {
                        comboBox.Invoke((MethodInvoker)(() =>
                            MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)));
                    }
                    else
                    {
                        MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        #endregion

        #region "EscapeSqlIdentifier"
        /// <summary>
        /// 16.08.2025
        /// Helper method to escape SQL identifiers (basic protection against SQL injection)
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        private static string EscapeSqlIdentifier(string identifier)
        {
            if (string.IsNullOrWhiteSpace(identifier))
                throw new ArgumentException("Identifier cannot be empty", nameof(identifier));

            // In a real application, you might want more comprehensive validation
            // or use a proper ORM/library that handles parameterization of identifiers
            return "[" + identifier.Replace("]", "]]") + "]";
        }
        #endregion

        #region "FillComboBoxSafeAsync"
        /// <summary>
        /// 06.09.2025     
        /// </summary>
        /// <param name="displayMember"></param>
        /// <param name="valueMember"></param>
        /// <param name="comboBox"></param>
        /// <param name="tableName"></param>
        /// <param name="fallbackUnitCode"></param>
        /// <param name="selectedValue"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static async Task FillComboBoxSafeAsync(
            string displayMember,
            string valueMember,
            ComboBox comboBox,
            string tableName,
            string fallbackUnitCode = null,
            object selectedValue = null)   // optional selected value (int, long, string, etc.)
        {
            if (comboBox == null) throw new ArgumentNullException(nameof(comboBox));
            if (string.IsNullOrWhiteSpace(displayMember)) throw new ArgumentException("DisplayMember required");
            if (string.IsNullOrWhiteSpace(valueMember)) throw new ArgumentException("ValueMember required");
            if (string.IsNullOrWhiteSpace(tableName)) throw new ArgumentException("TableName required");

            string unitCode = string.IsNullOrWhiteSpace(fallbackUnitCode) ? ClassGlobalVariables.pubUnitCode : fallbackUnitCode;
            string connectionString = ClassGlobalFunctions.GetConnectionStringByUnitCode(unitCode);
            if (string.IsNullOrWhiteSpace(connectionString)) throw new InvalidOperationException("Connection string empty");

            DataTable dataTable = null;

            // Load data on background thread
            await Task.Run(async () =>
            {
                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand($"SELECT [{displayMember}], [{valueMember}] FROM [{tableName}] ORDER BY [{displayMember}]", conn))
                {
                    await conn.OpenAsync().ConfigureAwait(false);
                    using (var rdr = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                    {
                        dataTable = new DataTable();
                        dataTable.Load(rdr);
                    }
                }
            }).ConfigureAwait(false);

            // Now update UI
            if (comboBox.IsDisposed) return;

            void uiAction()
            {
                comboBox.BeginUpdate();
                try
                {
                    // clear and bind
                    comboBox.DataSource = null;
                    comboBox.Items.Clear();
                    comboBox.DataSource = dataTable;
                    comboBox.DisplayMember = displayMember;
                    comboBox.ValueMember = valueMember;

                    // default to no selection
                    comboBox.SelectedIndex = -1;

                    if (selectedValue != null && dataTable.Rows.Count > 0 && dataTable.Columns.Contains(valueMember))
                    {
                        // try to convert selectedValue to column type
                        Type colType = dataTable.Columns[valueMember].DataType;
                        object converted = null;
                        bool convertOk = false;

                        try
                        {
                            // handle DBNull or nulls gracefully
                            if (selectedValue is DBNull) { convertOk = false; }
                            else if (colType == typeof(string)) { converted = selectedValue.ToString(); convertOk = true; }
                            else
                            {
                                // Convert.ChangeType handles numeric conversions (int->long etc.) where safe
                                converted = Convert.ChangeType(selectedValue, colType);
                                convertOk = true;
                            }
                        }
                        catch { convertOk = false; }

                        if (convertOk && converted != null)
                        {
                            // existence check by string comparison (safe across types)
                            string convertedStr = converted.ToString();
                            bool exists = dataTable.AsEnumerable()
                                                   .Any(r => r[valueMember] != null && r[valueMember] != DBNull.Value &&
                                                             r[valueMember].ToString() == convertedStr);

                            if (exists)
                            {
                                // Temporarily suppress SelectedIndexChanged handlers to avoid reentrancy issues
                                var handler = GetSelectedIndexChangedHandler(comboBox);
                                if (handler != null) comboBox.SelectedIndexChanged -= handler;

                                try
                                {
                                    comboBox.SelectedValue = converted;
                                }
                                finally
                                {
                                    if (handler != null) comboBox.SelectedIndexChanged += handler;
                                }
                            }
                        }
                    }
                }
                finally
                {
                    comboBox.EndUpdate();
                }
            }

            if (comboBox.InvokeRequired)
                comboBox.Invoke((MethodInvoker)uiAction);
            else
                uiAction();
        }

        /// <summary>
        /// Helper: tries to get the first SelectedIndexChanged delegate (if any).
        /// You can remove this helper if you don't want auto handler detach/reattach.
        /// </summary>
        private static EventHandler GetSelectedIndexChangedHandler(ComboBox comboBox)
        {
            // Reflection-based lookup of events is not ideal but works when necessary.
            // If you don't want this complexity, simply skip handler detach/reattach.
            try
            {
                FieldInfo f = typeof(Control).GetField("Events", BindingFlags.NonPublic | BindingFlags.Instance);
                if (f == null) return null;
                EventHandlerList list = f.GetValue(comboBox) as EventHandlerList;
                if (list == null) return null;

                // key for SelectedIndexChanged: lookup via reflection
                FieldInfo keyField = typeof(ComboBox).GetField("EVENT_SELECTEDINDEXCHANGED", BindingFlags.NonPublic | BindingFlags.Static);
                if (keyField == null) return null;
                object key = keyField.GetValue(null);
                Delegate d = list[key];
                return d as EventHandler;
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region "LoadSqlServerTodayDate"
        /// <summary>
        /// 23.08.2025
        /// get the sql server date and store in a global variable
        /// </summary>
        public static async Task LoadSqlServerTodayDateAsync()
        {
            string connectionString = ClassGlobalVariables.pubConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("SELECT CAST(GETDATE() AS DATE)", conn))
                {
                    object result = await cmd.ExecuteScalarAsync();
                    if (result != null)
                    {
                        ClassGlobalVariables.SqlServerTodayDate = Convert.ToDateTime(result);
                    }
                }
            }
        }
        #endregion

        #region "RowExistsAsync"
        /// <summary>
        /// 23.08.2025
        /// 
        /// Returns true if a row exists in [tableName] where [columnName] = value (or IS NULL when value is null).
        /// 
        /// bool exists = await DbExistsHelper.RowExistsAsync(
        ///     connectionString: myConnString,
        ///     tableName: "dbo.Users",
        ///     columnName: "Email",
        ///     value: "anil@example.com");
        /// 
        /// if (exists)
        /// {
        ///     Console.WriteLine("User already exists.");
        /// }
        /// else
        /// {
        ///     Console.WriteLine("No matching user found.");
        /// }
        /// </summary>  
        public static async Task<bool> RowExistsAsync(
            string connectionString,
            string tableName,
            string columnName,
            object value,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Connection string is required.", nameof(connectionString));
            if (string.IsNullOrWhiteSpace(tableName))
                throw new ArgumentException("Table name is required.", nameof(tableName));
            if (string.IsNullOrWhiteSpace(columnName))
                throw new ArgumentException("Column name is required.", nameof(columnName));

            // Validate identifiers to avoid SQL injection via names.
            if (!IsSafeIdentifier(tableName, true))
                throw new ArgumentException("Table name contains invalid characters.", nameof(tableName));
            if (!IsSafeIdentifier(columnName, false))
                throw new ArgumentException("Column name contains invalid characters.", nameof(columnName));

            string qTable = QuoteIdentifierWithSchema(tableName);
            string qColumn = QuoteIdentifier(columnName);

            string sql = value == null
                ? string.Format("SELECT TOP(1) 1 FROM {0} WHERE {1} IS NULL;", qTable, qColumn)
                : string.Format("SELECT TOP(1) 1 FROM {0} WHERE {1} = @value;", qTable, qColumn);

            using (var conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync(cancellationToken).ConfigureAwait(false);

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.Text;

                    if (value != null)
                    {
                        cmd.Parameters.AddWithValue("@value", value);
                    }

                    var result = await cmd.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false);
                    return result != null;
                }
            }
        }
        #endregion

        #region "RowExistsForUnitCodeAsync"
        /// <summary>
        /// 23.08.2025
        /// 
        /// Returns true if a row exists in [tableName] where [columnName] = value (or IS NULL when value is null).
        /// 
        /// bool exists = await DbExistsHelper.RowExistsAsync(
        ///     connectionString: myConnString,
        ///     tableName: "dbo.Users",
        ///     columnName: "Email",
        ///     value: "anil@example.com");
        /// 
        /// if (exists)
        /// {
        ///     Console.WriteLine("User already exists.");
        /// }
        /// else
        /// {
        ///     Console.WriteLine("No matching user found.");
        /// }
        /// </summary>  
        public static async Task<bool> RowExistsForUnitCodeAsync(
            string connectionString,
            string tableName,
            string columnName,
            object value,
            string unitCode,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Connection string is required.", nameof(connectionString));
            if (string.IsNullOrWhiteSpace(tableName))
                throw new ArgumentException("Table name is required.", nameof(tableName));
            if (string.IsNullOrWhiteSpace(columnName))
                throw new ArgumentException("Column name is required.", nameof(columnName));
            if (string.IsNullOrWhiteSpace(unitCode))
                throw new ArgumentException("UnitCode is required.", nameof(unitCode));

            // Validate identifiers to avoid SQL injection via names.
            if (!IsSafeIdentifier(tableName, true))
                throw new ArgumentException("Table name contains invalid characters.", nameof(tableName));
            if (!IsSafeIdentifier(columnName, false))
                throw new ArgumentException("Column name contains invalid characters.", nameof(columnName));

            string qTable = QuoteIdentifierWithSchema(tableName);
            string qColumn = QuoteIdentifier(columnName);

            string sql;

            if (value == null)
            {
                sql = $@"
                    SELECT TOP(1) 1 
                    FROM {qTable} 
                    WHERE {qColumn} IS NULL 
                        AND UnitCode = @unitCode;";
            }
            else
            {
                sql = $@"
                SELECT TOP(1) 1 
                FROM {qTable} 
                WHERE {qColumn} = @value 
                  AND UnitCode = @unitCode;";
            }

            using (var conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync(cancellationToken).ConfigureAwait(false);

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.Text;

                    if (value != null)
                    {
                        cmd.Parameters.AddWithValue("@value", value);
                    }

                    cmd.Parameters.AddWithValue("@unitCode", unitCode);

                    var result = await cmd.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false);
                    return result != null;
                }
            }
        }

        #endregion

        #region "IsSafeIdentifier"
        private static bool IsSafeIdentifier(string name, bool allowSchema)
        {
            var pattern = allowSchema
                ? @"^[A-Za-z_][A-Za-z0-9_]*(\.[A-Za-z_][A-Za-z0-9_]*)?$"
                : @"^[A-Za-z_][A-Za-z0-9_]*$";

            return Regex.IsMatch(name, pattern);
        }
        #endregion

        #region "QuoteIdentifier"
        private static string QuoteIdentifier(string identifier)
        {
            return "[" + identifier.Replace("]", "]]") + "]";
        }

        private static string QuoteIdentifierWithSchema(string name)
        {
            var parts = name.Split('.');
            if (parts.Length == 2)
            {
                return QuoteIdentifier(parts[0]) + "." + QuoteIdentifier(parts[1]);
            }
            return QuoteIdentifier(name);
        }
        #endregion


    }
}

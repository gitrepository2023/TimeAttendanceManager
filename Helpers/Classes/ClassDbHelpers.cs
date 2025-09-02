using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using static TimeAttendanceManager.Main.Classes.ClassGlobalFunctions;
using static TimeAttendanceManager.Main.Classes.ClassGlobalVariables;
using TimeAttendanceManager.Auth.Classes;
using System.Diagnostics;
using TimeAttendanceManager.Main.Classes;
using System.Net;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;
using System.Linq;
using System.Threading;
using System.Text.RegularExpressions;
using TimeAttendanceManager.Auth.Forms;
using TimeAttendanceManager.Main.Forms;

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
///    Name:           ClassDbHelpers.cs
///    Created:        14th August 2025
///    Date Completed: 14th August 2025
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
    public static class ClassDbHelpers
    {

        #region "LocalVariables"
        // Allow only letters, numbers, and underscores; must not start with a digit.
        private static readonly Regex IdentifierRegex = new Regex(@"^[A-Za-z_][A-Za-z0-9_]*$", RegexOptions.Compiled);

        #endregion

        #region "ValidateCredentials"
        public static async Task<bool> ValidateCredentials(string username, string password, string unitCode)
        {
            try
            {
                string mTableName = "dbo.v_UserLogins";

                var sb = new StringBuilder();
                var parameters = new List<SqlParameter>();

                sb.AppendLine($"SELECT TOP 1 * FROM {mTableName} ");
                sb.AppendLine("WHERE UserName = @UserName ");
                sb.AppendLine("AND IsDeleted = 0 ");
                sb.AppendLine("AND UserLocked = 0 ");

                parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar)
                {
                    Value = username
                });
                parameters.Add(new SqlParameter("@UnitCode", SqlDbType.VarChar)
                {
                    Value = unitCode
                });

                string query = sb.ToString();
                string myConnectionString = GetConnectionStringByUnitCode(unitCode);

                if (string.IsNullOrWhiteSpace(myConnectionString))
                {
                    throw new Exception("Connection string is empty.");
                }

                using (var mySqlConnection = new SqlConnection(myConnectionString))
                {
                    await mySqlConnection.OpenAsync();

                    using (var mySqlCommand = new SqlCommand(query, mySqlConnection))
                    {
                        if (parameters.Count > 0)
                        {
                            mySqlCommand.Parameters.AddRange(parameters.ToArray());
                        }

                        using (var mySqlDataReader = await mySqlCommand.ExecuteReaderAsync())
                        {
                            if (await mySqlDataReader.ReadAsync())
                            {
                                byte[] storedHash = mySqlDataReader["PasswordHash"] as byte[];
                                byte[] saltBytes = mySqlDataReader["Salt"] as byte[];

                                if (storedHash == null || saltBytes == null)
                                {
                                    return false;
                                }

                                string salt = Encoding.UTF8.GetString(saltBytes);
                                byte[] hashedPasswordBytes = PasswordHelper.HashPasswordToBytes(password, salt);

                                Debug.WriteLine("StoredHash: " + Convert.ToBase64String(storedHash));
                                Debug.WriteLine("ComputedHash: " + Convert.ToBase64String(hashedPasswordBytes));
                                Debug.WriteLine("Salt Used: " + salt);

                                if (PasswordHelper.SecureCompare(hashedPasswordBytes, storedHash))
                                {
                                    // Local helper functions
                                    string GetSafeString(string fieldName)
                                    {
                                        object value = mySqlDataReader[fieldName];
                                        return (value == DBNull.Value || string.IsNullOrEmpty(value.ToString()))
                                            ? null
                                            : value.ToString();
                                    }

                                    int? GetSafeInt(string fieldName)
                                    {
                                        object value = mySqlDataReader[fieldName];
                                        return value == DBNull.Value
                                            ? (int?)null
                                            : Convert.ToInt32(value);
                                    }

                                    var session = ClassUserSession.Instance;
                                    session.LoginUserRowId = GetSafeInt("Id");
                                    session.LoginUserRowGuid = GetSafeString("UserRowGuid");
                                    session.LoginUserName = GetSafeString("UserName");
                                    session.LoginUserUnitCode = GetSafeString("UnitCode");
                                    session.LoginUserRoleId = GetSafeInt("RoleId");
                                    session.LoginUserRole = GetSafeString("RoleName");
                                    session.LoginDepartmentCode = GetSafeString("DepartmentCode");
                                    session.LoginUserDeptName = GetSafeString("DepartmentName");
                                    session.LoginUserEmail = GetSafeString("Email");
                                    session.LoginUserFirstName = GetSafeString("FirstName");
                                    session.LoginUserLastName = GetSafeString("LastName");
                                    session.LoginUserFirstLastName = GetSafeString("UserFirstLastName");

                                    pubUnitCode = session.LoginUserUnitCode;
                                    pubLoginUserRowId = session.LoginUserRowId;
                                    pubLoginUserRowGuid = session.LoginUserRowGuid;
                                    pubConnectionString = myConnectionString;

                                    pubUserDictionary["CurrentUser"] = session;

                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Error: " + ex.Message, "Validate Credentials",
                                System.Windows.MessageBoxButton.OK);
                return false;
            }
        }
        #endregion

        #region "UpsertUserLoginHits"
        public static async Task UpsertUserLoginHits()
        {
            try
            {
                string mSqlProcedureName = "p_AdminUserHitsUpsert";

                using (var myConnection = new SqlConnection(pubConnectionString))
                {
                    // Open the database connection asynchronously
                    if (myConnection.State == ConnectionState.Closed)
                    {
                        await myConnection.OpenAsync();
                    }

                    using (var mySqlCommand = new SqlCommand(mSqlProcedureName, myConnection))
                    {
                        mySqlCommand.CommandType = CommandType.StoredProcedure;

                        // Add parameters with null checks
                        mySqlCommand.Parameters.AddWithValue("@UserRowId",
                            pubLoginUserRowId ?? (object)DBNull.Value);

                        mySqlCommand.Parameters.AddWithValue("@UserRowGuid",
                            !string.IsNullOrEmpty(pubLoginUserRowGuid)
                            ? pubLoginUserRowGuid
                            : (object)DBNull.Value);

                        mySqlCommand.Parameters.AddWithValue("@IpAdds",
                            !string.IsNullOrEmpty(pubHostIPAddress)
                            ? pubHostIPAddress
                            : (object)DBNull.Value);

                        // Execute the command asynchronously
                        await mySqlCommand.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(
                      $"Function: UpsertUserLoginHits\n\n" +
                      $"Error Source: {ex.Source}\n" +
                      $"Error Message: {ex.Message}\n",
                      "Error",
                      System.Windows.MessageBoxButton.OK,
                      System.Windows.MessageBoxImage.Information);
            }
        }
        #endregion

        #region "GetUserLoginDetails"
        public static async Task GetUserLoginDetails(string loginName, string unitCode)
        {
            try
            {
                string mTableName = "dbo.v_UserLogins";
                var sb = new StringBuilder();
                var parameters = new List<SqlParameter>();

                sb.AppendLine($"SELECT * FROM {mTableName} ");
                sb.AppendLine("WHERE UserName = @UserName ");
                sb.AppendLine("AND IsDeleted = 0 ");
                sb.AppendLine("AND UserLocked = 0 ");

                string query = sb.ToString();

                parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar)
                {
                    Value = loginName
                });
                parameters.Add(new SqlParameter("@UnitCode", SqlDbType.VarChar)
                {
                    Value = unitCode
                });

                string myConnectionString = GetConnectionStringByUnitCode(unitCode);

                using (var mySqlConnection = new SqlConnection(myConnectionString))
                using (var mySqlCommand = new SqlCommand(query, mySqlConnection))
                {
                    if (parameters.Count > 0)
                    {
                        mySqlCommand.Parameters.AddRange(parameters.ToArray());
                    }

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

                            bool? GetSafeBoolean(string fieldName)
                            {
                                object value = mySqlDataReader[fieldName];
                                return (value == DBNull.Value)
                                    ? (bool?)null
                                    : Convert.ToBoolean(value);
                            }

                            var userSession = ClassUserSession.Instance;
                            userSession.LoginUserRowId = GetSafeInt("Id");
                            userSession.LoginUserRowGuid = GetSafeString("UserRowGuid");
                            userSession.LoginUserUnitCode = GetSafeString("UnitCode");
                            userSession.LoginUserName = GetSafeString("UserName");
                            userSession.LoginUserFullName = GetSafeString("UserFirstLastName");
                            userSession.LoginDepartmentCode = GetSafeString("DepartmentCode");
                            userSession.LoginUserDeptName = GetSafeString("DepartmentName");
                            userSession.LoginUserEmail = GetSafeString("Email");
                            userSession.LoginUserRoleId = GetSafeInt("RoleId");
                            userSession.LoginUserRole = GetSafeString("RoleName");
                            userSession.LoginUserRoleDesc = GetSafeString("RoleDescription");

                            pubUnitCode = unitCode;
                            pubUserLoginName = userSession.LoginUserName;
                            pubLoginUserRowId = userSession.LoginUserRowId;
                            pubLoginUserRowGuid = userSession.LoginUserRowGuid;
                            pubUserRole = userSession.LoginUserRole;

                            try
                            {
                                pubDNSHostName = Dns.GetHostName();
                            }
                            catch
                            {
                                pubDNSHostName = "UNKNOWN";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Get User Login Details",
                              System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            }
        }
        #endregion

        #region "GetRowsFromTableAsync"
        /// <summary>
        /// 15.08.2025
        /// 
        /// DataTable result = await DatabaseHelper.GetRowsFromTable(unitCode: "3000",tableName: "dbo.Employees");
        /// 
        /// </summary>
        /// <param name="unitCode"></param>
        /// <param name="tableName"></param>
        /// <param name="selectedColumns"></param>
        /// <param name="serachColumns"></param>
        /// <param name="additionalParameters"></param>
        /// <param name="searchText"></param>
        /// <param name="orderByClause"></param>
        /// <param name="limitRows"></param>
        /// <returns></returns>
        public static async Task<DataTable> GetRowsFromTableAsync(
            string unitCode,
            string tableName,
            List<string> selectedColumns = null,
            List<string> serachColumns = null,
            List<SqlParameter> additionalParameters = null,
            string searchText = null,
            string orderByClause = null,
            string isDeletedColumn = null,
            bool? isDeletedValue = null,
            string isActiveColumn = null,
            bool? isActiveValue = null,
            int limitRows = 0)
        {
            try
            {
                // Build the base query
                var query = new StringBuilder();
                var parameters = new List<SqlParameter>();

                /// Select columns with optional TOP clause
                query.Append("SELECT ");
                if (limitRows > 0)
                {
                    query.Append($"TOP {limitRows} ");
                }

                if (selectedColumns == null || selectedColumns.Count == 0)
                {
                    query.Append("*");
                }
                else
                {
                    query.Append(string.Join(", ", selectedColumns));
                }

                // From clause
                query.Append($" FROM {tableName} WHERE 1=1");

                // Add IsDeleted filter (if column and value specified)
                if (!string.IsNullOrEmpty(isDeletedColumn) && isDeletedValue.HasValue)
                {
                    query.Append($" AND {isDeletedColumn} = @IsDeleted");
                    parameters.Add(new SqlParameter("@IsDeleted", isDeletedValue.Value ? 1 : 0));
                }

                // Add IsActive filter (if column and value specified)
                if (!string.IsNullOrEmpty(isActiveColumn) && isActiveValue.HasValue)
                {
                    query.Append($" AND {isActiveColumn} = @IsActive");
                    parameters.Add(new SqlParameter("@IsActive", isActiveValue.Value ? 1 : 0));
                }

                // Add search conditions if search text is provided
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    searchText = searchText.Trim().ToLower().Replace("*", "%");

                    if (serachColumns != null && serachColumns.Count > 0)
                    {
                        query.Append(" AND (");
                        var searchClauses = new List<string>();

                        for (int i = 0; i < serachColumns.Count; i++)
                        {
                            string paramName = $"@SearchText{i}";
                            searchClauses.Add($"{serachColumns[i]} LIKE {paramName}");
                            parameters.Add(new SqlParameter(paramName, $"%{searchText}%"));
                        }

                        query.Append(string.Join(" OR ", searchClauses));
                        query.Append(")");
                    }
                }

                // Add additional parameters if provided
                if (additionalParameters != null && additionalParameters.Count > 0)
                {
                    foreach (var param in additionalParameters)
                    {
                        // Ensure parameter name starts with @
                        string paramName = param.ParameterName.StartsWith("@")
                                           ? param.ParameterName
                                           : "@" + param.ParameterName;

                        // Use the *column name* = parameter
                        // Example: if param.ParameterName = "RoleName", we add "AND RoleName = @RoleName"
                        string columnName = paramName.TrimStart('@');

                        query.Append($" AND {columnName} = {paramName}");

                        // Add to parameters list
                        parameters.Add(param);
                    }
                }
                
                // Add order by clause if provided
                if (!string.IsNullOrWhiteSpace(orderByClause))
                {
                    query.Append($" ORDER BY {orderByClause}");
                }

                // Get connection string based on unit code
                string connectionString = ClassGlobalFunctions.GetConnectionStringByUnitCode(unitCode);

                // Execute the query
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand(query.ToString(), connection))
                    {
                        if (parameters.Count > 0)
                        {
                            command.Parameters.AddRange(parameters.ToArray());
                        }

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            var resultTable = new DataTable();
                            resultTable.Load(reader);
                            return resultTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error (you should implement your own logging)
                Console.WriteLine($"Error in GetRowsFromTable: {ex.Message}");
                throw; // Re-throw to let caller handle
            }
        }
        #endregion

        #region "GetSelectedRowDataAsync"
        /// <summary>
        /// 15.08.2025
        /// 
        /// Usage
        /// DataTable userData = await DataHelper.GetSelectedRowDataAsync(123);
        /// 
        /// With custom table and status label:
        /// DataTable userData = await DataHelper.GetSelectedRowDataAsync(
        /// rowId: 123,
        /// tableName: "dbo.UsersExtended",
        /// statusLabel: TsLblInputStatus);
        /// 
        /// Reusable across all forms
        /// Thread-safe UI updates
        /// Better error handling
        /// More flexible with optional parameters
        /// 
        /// </summary>
        /// <param name="rowId"></param>
        /// <param name="tableName"></param>
        /// <param name="unitCode"></param>
        /// <returns></returns>
        public static async Task<DataTable> GetSelectedRowDataAsync(
            int rowId,
            string tableName = null,
            string unitCode = null)
        {
            try
            {
                
                // Build parameterized query
                var query = new StringBuilder();
                query.Append("SELECT * ");
                query.Append($"FROM {tableName} ");
                query.Append("WHERE [Id] = @RowId");

                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@RowId", SqlDbType.Int) { Value = rowId }
                };

                // Get connection string
                string connectionString = ClassGlobalFunctions.GetConnectionStringByUnitCode(unitCode ?? ClassGlobalVariables.pubUnitCode);
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new InvalidOperationException("Connection string cannot be empty");
                }

                var resultTable = new DataTable();

                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand(query.ToString(), connection))
                    {
                        command.Parameters.AddRange(parameters.ToArray());

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            resultTable.Load(reader);
                        }
                    }
                }

                return resultTable;
            }
            catch (Exception ex)
            {
               ShowErrorMessage("Get Selected Row Data", ex.Message);
                return null;
            }
        }
        #endregion

        #region "GetPasswordDataAsync"
        /// <summary>
        /// 16.08.2025
        /// 
        /// Usage
        /// var credentials = await GetPasswordDataAsync(
        ///     userId: 123,
        ///     passwordColumnName: "PasswordHash",
        ///     saltColumnName: "PasswordSalt",
        ///     unitCode: "3000");
        ///     
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="passwordColumnName"></param>
        /// <param name="saltColumnName"></param>
        /// <param name="unitCode"></param>
        /// <returns></returns>
        public static async Task<(byte[] passwordHash, byte[] salt)> GetPasswordDataAsync(
            int userId,
            string passwordColumnName,
            string saltColumnName,
            string unitCode = null)
        {
            

            // Parameterized query with safe column name insertion
            string query = $@"
                SELECT [{passwordColumnName}], [{saltColumnName}] 
                FROM dbo.UserLogins 
                WHERE Id = @UserId";

            try
            {
                string connectionString = ClassGlobalFunctions.GetConnectionStringByUnitCode(
                    unitCode ?? ClassGlobalVariables.pubUnitCode);

                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new InvalidOperationException("Connection string cannot be empty");
                }

                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                // Read using column names passed as parameters
                                byte[] passwordHash = (byte[])reader[passwordColumnName];
                                byte[] salt = (byte[])reader[saltColumnName];
                                return (passwordHash, salt);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Logger.Error("Failed to retrieve password data", ex);
                throw;
            }

            return (null, null);
        }
        #endregion

        #region "UpdateStatusLabel"

        /// <summary>
        /// 
        /// </summary>
        /// <param name="label"></param>
        /// <param name="text"></param>
        /// <param name="color"></param>
        private static void UpdateStatusLabel(Control label, string text, Color color)
        {
            if (label != null && !label.IsDisposed)
            {
                if (label.InvokeRequired)
                {
                    label.Invoke((MethodInvoker)(() =>
                    {
                        label.Text = text;
                        label.ForeColor = color;
                    }));
                }
                else
                {
                    label.Text = text;
                    label.ForeColor = color;
                }
            }
        }
        #endregion

        #region "ShowErrorMessage"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        private static void ShowErrorMessage(string title, string message)
        {
            System.Windows.MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        #endregion

        #region "ValidateMasterCredentialsAsync"
        /// <summary>
        /// 15.08.2025
        /// 
        /// Usage
        /// bool isValid = await ValidateMasterCredentialsAsync(txtUser.Text, txtPass.Text, cmbUnit.SelectedValue.ToString());
        /// if (isValid)
        /// {
        ///     // Success
        /// }
        /// else
        /// {
        ///     // Invalid credentials
        /// }
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="unitCode"></param>
        /// <returns></returns>
        public static async Task<bool> ValidateMasterCredentialsAsync(string username, 
            string password, 
            string unitCode)
        {
            try
            {
                const string tableName = "dbo.v_UserLogins";

                var sb = new StringBuilder();
                sb.AppendLine($"SELECT TOP 1 * FROM {tableName} ");
                sb.AppendLine("WHERE UserName = @UserName ");
                sb.AppendLine("AND UnitCode = @UnitCode ");
                sb.AppendLine("AND IsDeleted = 0 ");
                sb.AppendLine("AND UserLocked = 0 ");

                var parameters = new List<SqlParameter>

                {
                    new SqlParameter("@UserName", SqlDbType.VarChar) { Value = username },
                    new SqlParameter("@UnitCode", SqlDbType.VarChar) { Value = unitCode }
                };

                string query = sb.ToString();

                string connectionString = ClassGlobalFunctions.GetConnectionStringByUnitCode(unitCode);
                if (string.IsNullOrWhiteSpace(connectionString))
                    throw new Exception("Connection string is empty.");

                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddRange(parameters.ToArray());

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (!await reader.ReadAsync())
                                return false; // No matching user

                            byte[] storedHash = reader["MasterPasswordHash"] as byte[];
                            byte[] saltBytes = reader["MasterPasswordSalt"] as byte[];

                            if (storedHash == null || saltBytes == null)
                                return false;

                            // Convert salt bytes to string
                            string salt = Encoding.UTF8.GetString(saltBytes);

                            // Compute hash from input password
                            byte[] computedHash = PasswordHelper.HashPasswordToBytes(password, salt);

                            // Compare hashes securely
                            return PasswordHelper.SecureCompare(computedHash, storedHash);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error: {ex.Message}", "Validate Master Credentials",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        #endregion

        #region "CreateSqlParameter"
        /// <summary>
        /// 16.08.2025
        /// Helper methods for better code organization
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SqlParameter CreateSqlParameter(string name, SqlDbType type, object value)
        {
            return new SqlParameter(name, type)
            {
                Value = value ?? DBNull.Value,
                IsNullable = true
            };
        }
        #endregion

        #region "UpsertFormsToDatabase"
        /// <summary>
        /// 18.08.2025
        /// 
        /// Retrieve Form Names and Titles: The GetFormsStartingWith function returns a list of tuples, 
        /// each containing the form name and its title. It creates an instance of each form to access the 
        /// Text property, which represents the title of the form.
        /// 
        /// The MERGE command is used to insert or update records in the MM_UserForms table. 
        /// It checks if a form with the given name already exists:
        /// If it does (WHEN MATCHED), it updates the form's title.
        /// If it does not (WHEN NOT MATCHED), it inserts a new record.
        /// 
        /// Removes rows where FormName does not exist in formInfo.
        /// The DELETE query ensures only forms present in formInfo remain in MM_UserForms.
        /// If formInfo.Count = 0, the DELETE query will not execute to avoid errors.
        /// 
        /// Use a Single MERGE Statement for Multiple Rows
        /// String.Format("@FormName{0}", i) and String.Format("@FormTitle{0}", i) are used to generate unique parameter names for each row in formInfo.
        /// valueList.Add(String.Format("({0}, {1})", formNameParam, formTitleParam)) constructs each row's part of the VALUES clause.
        /// Parameters are added dynamically using String.Format to match the names used in the VALUES clause. 
        /// </summary>
        /// <returns></returns>
        public static async Task UpsertFormsToDatabaseAsync()
        {
            try
            {
                const string prefix = "Frm";
                var formInfo = GetFormsStartingWith(prefix);

                using (var myConnection = new SqlConnection(pubConnectionString))
                {
                    await myConnection.OpenAsync();

                    // Build MERGE command
                    var mergeCommand = new StringBuilder();
                    mergeCommand.Append(@"
                        MERGE dbo.AdminMenuFormNames AS target 
                        USING (VALUES ");

                    // Add parameterized values - .NET Framework 4.8 compatible version
                    var valueParams = formInfo.Select((info, i) =>
                        $"(@FormName{i}, @FormTitle{i})").ToArray();
                    mergeCommand.Append(string.Join(", ", valueParams));

                    mergeCommand.Append(@") AS source (FormName, FormTitle) 
                        ON target.FormName = source.FormName 
                        WHEN MATCHED THEN 
                            UPDATE SET 
                                FormTitle = source.FormTitle,
                                UserUpdatedId = @UserId,
                                UserUpdatedRowGuid = @UserRowGuid,
                                IpAddsUpdated = @IpAddsCreated,
                                UpdatedHostName = @HostName,
                                UpdatedAt = GETDATE()
                        WHEN NOT MATCHED THEN 
                            INSERT (FormName, FormTitle, UserId, UserRowGuid, IpAddsCreated, HostName) 
                            VALUES (source.FormName, source.FormTitle, @UserId, @UserRowGuid, @IpAddsCreated, @HostName);");

                    // Execute MERGE
                    using (var mySqlCommand = new SqlCommand(mergeCommand.ToString(), myConnection))
                    {
                        // Add form parameters
                        for (int i = 0; i < formInfo.Count; i++)
                        {
                            mySqlCommand.Parameters.AddWithValue($"@FormName{i}", formInfo[i].Item1);
                            mySqlCommand.Parameters.AddWithValue($"@FormTitle{i}", formInfo[i].Item2);
                        }

                        // Add common parameters
                        mySqlCommand.Parameters.AddWithValue("@UserId", pubLoginUserRowId);
                        mySqlCommand.Parameters.AddWithValue("@UserRowGuid", pubLoginUserRowGuid);
                        mySqlCommand.Parameters.AddWithValue("@IpAddsCreated", pubHostIPAddress);
                        mySqlCommand.Parameters.AddWithValue("@HostName", pubDNSHostName);

                        await mySqlCommand.ExecuteNonQueryAsync();
                    }

                    // Execute DELETE for forms not in current list
                    if (formInfo.Count > 0)
                    {
                        var deleteCommand = new StringBuilder(@"
                            DELETE FROM dbo.AdminMenuFormNames 
                            WHERE FormName NOT IN (");

                        var deleteParams = formInfo.Select((_, i) => $"@DelFormName{i}").ToArray();
                        deleteCommand.Append(string.Join(", ", deleteParams));
                        deleteCommand.Append(");");

                        using (var myDeleteCommand = new SqlCommand(deleteCommand.ToString(), myConnection))
                        {
                            for (int i = 0; i < formInfo.Count; i++)
                            {
                                myDeleteCommand.Parameters.AddWithValue($"@DelFormName{i}", formInfo[i].Item1);
                            }

                            await myDeleteCommand.ExecuteNonQueryAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Update Insert Forms To Database",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        #endregion

        #region "GetFormsStartingWith"
        /// <summary>
        /// 18.08.2025
        /// 
        /// To retrieve form names that start with "FrmMM" from a VB.NET project using reflection
        /// Assembly.GetExecutingAssembly(): Retrieves the assembly of the currently executing application.
        /// GetTypes(): Retrieves all the types (classes) defined in the assembly.
        /// IsSubclassOf(GetType(Form)): Checks if a type is a subclass of Form, which indicates it’s a form.
        /// Not type.IsAbstract: Ensures that the type is not an abstract class (which can’t be instantiated).
        /// This code will dynamically retrieve and display the names of all forms in your application whose names start with "FrmMM". 
        /// 
        /// Usage:
        /// const string prefix = "Frm";
        /// var formInfo = GetFormsStartingWith(prefix);
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static List<Tuple<string, string>> GetFormsStartingWith(string prefix)
        {
            var formInfo = new List<Tuple<string, string>>();

            try
            {
                // Get the assembly of the currently executing application
                var assembly = Assembly.GetExecutingAssembly();

                // Get all types in the assembly
                var types = assembly.GetTypes();

                foreach (var type in types)
                {
                    try
                    {
                        // Check if the type is a form and starts with the specified prefix
                        if (type.IsSubclassOf(typeof(Form)) &&
                            !type.IsAbstract &&
                            type.Name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                        {
                            // Create an instance of the form to get its title
                            if (Activator.CreateInstance(type) is Form formInstance)
                            {
                                formInfo.Add(Tuple.Create(type.Name, formInstance.Text));
                                formInstance.Dispose(); // Clean up the form instance
                            }
                        }
                    }
                    catch
                    {
                        // Silently handle individual type failures
                    }
                }

                return formInfo;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Get Form Names",
                   MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
        }
        #endregion

        #region "DeleteRowsAsync"
        /// <summary>
        /// 19.08.2025
        /// 
        /// Calling the Stored Procedure dbo.usp_AdminSoftHotDeleteRows
        /// to Soft or Hot Delete Rows from any SQL table
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="idColumnName"></param>
        /// <param name="ids"></param>
        /// <param name="softDelete"></param>
        /// <param name="deletedBy"></param>
        /// <param name="unitCode"></param>
        /// <returns></returns>
        public static async Task<bool> DeleteRowsAsync(
            string tableName,
            string idColumnName,
            List<int> ids,
            bool softDelete = true,
            string deletedBy = null,
            string unitCode = null
            )
        {
            if (ids == null || ids.Count == 0)
            {
                // _logger.LogWarning("No IDs provided for deletion.");
                System.Windows.MessageBox.Show("No IDs provided for deletion.", "Delete Rows",
                  MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            try
            {

                bool isValidMasterPassword = false;
                int maxAttempts = 3;

                for (int attempts = 0; attempts < maxAttempts && !isValidMasterPassword; attempts++)
                {
                    string password = ShowPasswordInputDialog();

                    if (string.IsNullOrEmpty(password))
                    {
                        System.Windows.MessageBox.Show("Password cannot be empty. Access Denied", "Information",MessageBoxButton.OK,MessageBoxImage.Stop);
                        continue;
                    }

                    isValidMasterPassword = await ClassDbHelpers.ValidateMasterCredentialsAsync(
                        ClassUserSession.Instance.LoginUserName,
                        password,
                        ClassUserSession.Instance.LoginUserUnitCode);

                    if (isValidMasterPassword)
                    {
                        break; // Exit loop immediately when password is valid
                    }
                    else
                    {
                        int remainingAttempts = maxAttempts - attempts - 1;
                        if (remainingAttempts > 0)
                        {
                            System.Windows.MessageBox.Show($"Wrong Password. You have {remainingAttempts} attempt(s) left.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }

                if (!isValidMasterPassword)
                {
                    System.Windows.MessageBox.Show("Access Denied. You have used all attempts.", "Information", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return false;
                }

                string connectionString = ClassGlobalFunctions.GetConnectionStringByUnitCode(
                    unitCode ?? ClassGlobalVariables.pubUnitCode);

                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new InvalidOperationException("Connection string cannot be empty");
                }

                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("usp_AdminSoftHotDeleteRows", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TableName", tableName);
                        command.Parameters.AddWithValue("@IdColumnName", idColumnName);
                        command.Parameters.AddWithValue("@Ids", string.Join(",", ids));
                        command.Parameters.AddWithValue("@SoftDelete", softDelete);
                        command.Parameters.AddWithValue("@DeletedBy", deletedBy ?? (object)DBNull.Value);

                        // Add return value parameter
                        var returnParameter = new SqlParameter("@ReturnVal", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.ReturnValue
                        };
                        command.Parameters.Add(returnParameter);

                        await command.ExecuteNonQueryAsync();

                        int result = (int)returnParameter.Value;
                        return result == 1;
                    }
                }
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, $"Failed to delete rows from {tableName}.");
                System.Windows.MessageBox.Show(ex.Message, "Delete Rows",
                 MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }
        }
        #endregion

        #region "ShowPasswordInputDialog"
        private static string ShowPasswordInputDialog()
        {
            using (var frm = new FrmAdminInputBox())
            {
                return frm.ShowDialog() == DialogResult.OK ? frm.TxtPassword.Text : null;
            }
        }
        #endregion

        #region "GetDefaultXmlAsync"
        /// <summary>
        /// 20.08.2025
        /// 
        /// Gets a single XML (or text) value from the given column in the given table by filtering on UserRowGuid.
        /// Returns empty string if no row/value is found. Catches exceptions and returns empty string by default.
        /// </summary>
        /// <param name="connectionString">SQL Server connection string.</param>
        /// <param name="schemaAndTable">Table name, optionally schema-qualified (e.g., "dbo.UserLoginDefaults" or "UserLoginDefaults").</param>
        /// <param name="columnName">Column name to select (e.g., "DefaultXml").</param>
        /// <param name="userRowGuid">UserRowGuid filter value (as string GUID).</param>
        /// <param name="showMessageBoxOnError">If true and WinForms is available, shows a MessageBox on error.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public static async Task<string> GetDefaultXmlAsync(
            string connectionString,
            string schemaAndTable,
            string columnName,
            string userRowGuid,
            bool showMessageBoxOnError = true,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Connection string is required.", nameof(connectionString));

            if (string.IsNullOrWhiteSpace(schemaAndTable))
                throw new ArgumentException("Table name is required.", nameof(schemaAndTable));

            if (string.IsNullOrWhiteSpace(columnName))
                throw new ArgumentException("Column name is required.", nameof(columnName));

            // Validate identifiers and build a quoted FROM target and SELECT column
            var fromTarget = BuildFromTarget(schemaAndTable);     // e.g., [dbo].[UserLoginDefaults] or [UserLoginDefaults]
            var safeColumn = QuoteIdentifier(columnName);         // e.g., [DefaultXml]

            string sql = $@"
                SELECT TOP (1) {safeColumn}
                FROM {fromTarget}
                WHERE [UserRowGuid] = @UserRowGuid;";

            try
            {
                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(sql, conn))
                {
                    // If it's a valid GUID, send as UNIQUEIDENTIFIER; otherwise as NVARCHAR
                    if (Guid.TryParse(userRowGuid, out var guidVal))
                    {
                        cmd.Parameters.Add("@UserRowGuid", SqlDbType.UniqueIdentifier).Value = guidVal;
                    }
                    else
                    {
                        // Fallback if caller passes a non-GUID format
                        cmd.Parameters.Add("@UserRowGuid", SqlDbType.NVarChar, 64).Value = userRowGuid ?? string.Empty;
                    }

                    await conn.OpenAsync(cancellationToken).ConfigureAwait(false);
                    var result = await cmd.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false);

                    return (result == null || result == DBNull.Value) ? string.Empty : Convert.ToString(result);
                }
            }
            catch (Exception ex)
            {
            #if WINDOWS
            if (showMessageBoxOnError)
            {
                try
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message, "Get Default XML", 
                        System.Windows.Forms.MessageBoxButtons.OK, 
                        System.Windows.Forms.MessageBoxIcon.Information);
                }
                catch { /* ignore if WinForms not available */ }
            }
            #endif
                // Match original behavior (return Nothing) by returning empty string on error.
                return string.Empty;
            }
        }
        #endregion

        #region "BuildFromTarget"
        private static string BuildFromTarget(string schemaAndTable)
        {
            // Accept "Table" or "Schema.Table"
            var parts = schemaAndTable.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 1)
            {
                return QuoteIdentifier(parts[0]); // [Table]
            }
            if (parts.Length == 2)
            {
                return $"{QuoteIdentifier(parts[0])}.{QuoteIdentifier(parts[1])}"; // [Schema].[Table]
            }

            throw new ArgumentException("Provide table as 'Table' or 'Schema.Table' (avoid database/server prefixes).", nameof(schemaAndTable));
        }
        #endregion

        #region "QuoteIdentifier"
        private static string QuoteIdentifier(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Identifier cannot be empty.");

            if (!IdentifierRegex.IsMatch(name))
                throw new ArgumentException($"Invalid identifier: '{name}'. Use letters, numbers, and underscores; cannot start with a digit.");

            return $"[{name}]";
        }
        #endregion

        #region "SaveUserLoginDefaultXmlAsync"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="userRowGuid"></param>
        /// <param name="tableName"></param>
        /// <param name="elements"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task SaveUserLoginDefaultXmlAsync(
            string connectionString,
            string userRowGuid,
            string tableName,
            IDictionary<string, string> elements,
            CancellationToken cancellationToken = default)
            {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Connection string is required.", nameof(connectionString));
            if (string.IsNullOrWhiteSpace(userRowGuid))
                throw new ArgumentException("UserRowGuid is required.", nameof(userRowGuid));

            string xml = ClassXmlHelper.BuildDefaultXml(elements);

            string sql = $@"
                MERGE INTO {tableName} AS target
                USING (SELECT @UserRowGuid AS UserRowGuid, @Xml AS DefaultXml) AS source
                ON target.[UserRowGuid] = source.UserRowGuid
                WHEN MATCHED THEN
                    UPDATE SET [DefaultXml] = source.DefaultXml
                WHEN NOT MATCHED THEN
                    INSERT ([UserRowGuid], [DefaultXml])
                    VALUES (source.UserRowGuid, source.DefaultXml);";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                // Match your schema exactly
                cmd.Parameters.Add("@UserRowGuid", SqlDbType.NVarChar, 150).Value = userRowGuid;
                cmd.Parameters.Add("@Xml", SqlDbType.Xml).Value = xml;

                await conn.OpenAsync(cancellationToken).ConfigureAwait(false);
                await cmd.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
            }
        }
        #endregion

        #region "GetUserLoginDefaultXmlAsync"
        /// <summary>
        /// Returns empty string if not found.
        /// 
        /// Usage
        /// string xml = await GetUserLoginDefaultXmlAsync(connectionString, ClassGlobalVariables.pubLoginUserRowGuid);
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="userRowGuid"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<string> GetUserLoginDefaultXmlAsync(
           string connectionString,
           string userRowGuid,
           string tableName,
           CancellationToken cancellationToken = default)
        {
            string sql = $@"
                SELECT TOP (1) [DefaultXml]
                FROM {tableName}
                WHERE [UserRowGuid] = @UserRowGuid;";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@UserRowGuid", SqlDbType.NVarChar, 150).Value = userRowGuid;
                await conn.OpenAsync(cancellationToken).ConfigureAwait(false);
                var result = await cmd.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false);
                return (result == null || result == DBNull.Value) ? string.Empty : Convert.ToString(result);
            }
        }
        #endregion

    }
}

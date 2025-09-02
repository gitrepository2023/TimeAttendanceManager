using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
///    Title:          Mappings from table dbo.AdminUserUnitAccess table into memory
///
///                    
///    Name:           UserAccessCache.cs
///    Created:        25th August 2025
///    Date Completed: 25th August 2025
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
    public static class UserAccessCache
    {
        // Key = UnitCode, Value = true (has access)
        public static Dictionary<string, bool> UnitAccess { get; private set; }
            = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);

        public static string CurrentUserRowGuid { get; private set; }

        private static readonly object _sync = new object();

    /// <summary>
    /// Loads the UnitCode access list for the given user into a process-wide cache.
    /// After successful login:
    /// string userGuidStr = ClassGlobalVariables.pubLoginUserRowGuid; // nvarchar(150)
    ///         string cs = ClassGlobalFunctions.GetConnectionStringByUnitCode(ClassGlobalVariables.pubUnitCode);
    ///         await UserAccessCache.LoadUserAccessAsync(userGuidStr, cs);
    /// 
    // Anywhere:
    /// if (!UserAccessCache.HasAccess(selectedUnitCode))
    /// {
    ///     MessageBox.Show("You do not have access to the selected UnitCode.");
    ///     return;
    /// }
    /// </summary>
    public static async Task LoadUserAccessAsync(
            string userRowGuid,
            string connectionString,
            CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(userRowGuid))
                throw new ArgumentException("userRowGuid is required.", nameof(userRowGuid));
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("connectionString is required.", nameof(connectionString));

            var newMap = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(@"
                SELECT DISTINCT UnitCode
                FROM dbo.AdminUserUnitAccess WITH (NOLOCK)
                WHERE UserRowGuid = @UserRowGuid;", conn))
            {
                var p = cmd.Parameters.Add("@UserRowGuid", SqlDbType.NVarChar, 150);
                p.Value = userRowGuid.Trim();

                await conn.OpenAsync(ct).ConfigureAwait(false);

                using (var rdr = await cmd.ExecuteReaderAsync(CommandBehavior.SequentialAccess, ct).ConfigureAwait(false))
                {
                    while (await rdr.ReadAsync(ct).ConfigureAwait(false))
                    {
                        if (!await rdr.IsDBNullAsync(0))
                        {
                            var unit = rdr.GetString(0)?.Trim();
                            if (!string.IsNullOrEmpty(unit))
                                newMap[unit] = true;
                        }
                    }
                }
            }

            // Swap in atomically to avoid partial reads from other threads.
            lock (_sync)
            {
                UnitAccess = newMap;
                CurrentUserRowGuid = userRowGuid.Trim();
            }
        }

        public static bool HasAccess(string unitCode)
        {
            if (string.IsNullOrWhiteSpace(unitCode)) return false;
            // local snapshot avoids race if another thread swaps the dictionary reference
            var map = UnitAccess;
            return map.ContainsKey(unitCode.Trim());
        }
    }

}

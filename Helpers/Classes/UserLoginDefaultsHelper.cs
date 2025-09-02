using TimeAttendanceManager.Main.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
/// <Module>
///    Title:          Helper class
///    Name:           UserLoginDefaultsHelper.cs
///    Created:        21st August 2025
///    Date Completed: 21st August 2025
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
    public static class UserLoginDefaultsHelper
    {
        /// <summary>
        /// Fetch specific columns from dbo.UserLoginDefaults as a case-insensitive dictionary.
        /// </summary>
        /// <param name="unitCode">Plant/Unit code used to resolve the connection string.</param>
        /// <param name="userRowGuid">Login user's RowGuid.</param>
        /// <param name="wantedColumns">Exact element names to extract from the XML.</param>
        /// <param name="tableName">Defaults to dbo.UserLoginDefaults.</param>
        public static async Task<IDictionary<string, string>> GetAsync(
            string unitCode,
            string userRowGuid,
            IEnumerable<string> wantedColumns,
            string tableName = "dbo.UserLoginDefaults")
        {
            if (wantedColumns == null) throw new ArgumentNullException(nameof(wantedColumns));

            // 1) Resolve connection
            var connectionString = ClassGlobalFunctions.GetConnectionStringByUnitCode(unitCode);

            // 2) Retrieve XML from your existing DB helper
            string xml = await ClassDbHelpers
                .GetUserLoginDefaultXmlAsync(connectionString, userRowGuid, tableName)
                .ConfigureAwait(false);

            // 3) Extract only the requested elements (uses your existing XML helper)
            //    Ensure case-insensitive keys in the returned dictionary.
            var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            // ClassXmlHelper.ExtractElements(xml, wanted) is assumed to return
            // Dictionary<string,string> of found elements; if not found, you can
            // fill them as empty strings to always return all requested keys.
            var wantedArray = wantedColumns as string[] ?? wantedColumns.ToArray();
            var extracted = ClassXmlHelper.ExtractElements(xml, wantedArray)
                            ?? new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            foreach (var key in wantedArray)
            {
                // Put an empty string if missing so callers can safely read every requested key.
                string value;
                if (!extracted.TryGetValue(key, out value))
                    value = string.Empty;

                result[key] = value ?? string.Empty;
            }

            return result;
        }

    }
}

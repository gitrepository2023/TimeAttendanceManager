using System;
using System.Collections.Generic;
using System.Xml.Linq;

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
///    Name:           ClassXmlHelper.cs
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

namespace TimeAttendanceManager.Helpers.Classes
{

    public class ClassXmlHelper
    {

        #region "ExtractElements"
        /// <summary>
        /// Parses the XML string and extracts values for the requested element names.
        /// If an element does not exist, its value will be an empty string.
        /// 
        /// Usage:
        /// var values = ClassXmlHelper.ExtractElements(xmlData, elementNames);
        /// 
        /// </summary>
        /// <param name="xmlData">The XML string to parse.</param>
        /// <param name="elementNames">List of element names to extract.</param>
        /// <returns>Dictionary of elementName -> value (empty string if missing).</returns>
        public static Dictionary<string, string> ExtractElements(string xmlData, IEnumerable<string> elementNames)
        {
            var results = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            if (string.IsNullOrWhiteSpace(xmlData))
            {
                // return all requested keys with empty values
                foreach (var name in elementNames)
                    results[name] = string.Empty;

                return results;
            }

            XElement root;
            try
            {
                root = XElement.Parse(xmlData);
            }
            catch
            {
                // invalid XML → return all empty
                foreach (var name in elementNames)
                    results[name] = string.Empty;

                return results;
            }

            foreach (var name in elementNames)
            {
                var value = root.Element(name)?.Value?.Trim() ?? string.Empty;
                results[name] = value;
            }

            return results;
        }
        #endregion

        #region "BuildDefaultXml"
        /// <summary>
        /// Builds XML like:
        /// <Defaults>
        ///   <PlantCode>1400</PlantCode>
        ///   <EmployeeCategory>PERMANENT STAFF</EmployeeCategory>
        ///   ... (any new elements you add later)
        /// </Defaults>
        /// </summary>
        public static string BuildDefaultXml(IDictionary<string, string> elements, string rootName = "Defaults")
        {
            var root = new XElement(rootName);
            if (elements != null)
            {
                foreach (var kvp in elements)
                {
                    // Write element even if value is null (stored as empty string)
                    root.Add(new XElement(kvp.Key, kvp.Value ?? string.Empty));
                }
            }
            return root.ToString(SaveOptions.DisableFormatting);
        }
        #endregion 

    }
}

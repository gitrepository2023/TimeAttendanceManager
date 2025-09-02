using System;
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
///    Title:          Global Helper
///    Name:           ClassSafeValueHelpers.cs
///    Created:        19th August 2025
///    Date Completed: 19th August 2025
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
    public class ClassSafeValueHelpers
    {
        public static string PubGetSafeValue(object value)
        {
            if (value != null && value != DBNull.Value)
            {
                string str = value.ToString();
                if (!string.IsNullOrWhiteSpace(str))
                    return str;
            }
            return string.Empty;
        }

        /// <summary>
        /// int? id = PubGetSafeInteger(row["Id"]);
        /// </summary>
        public static int? PubGetSafeInteger(object value)
        {
            if (value != null && value != DBNull.Value)
            {
                if (int.TryParse(value.ToString(), out int result))
                    return result;
            }
            return null;
        }
        public static byte? PubGetSafeTinyInt(object value)
        {
            if (value != null && value != DBNull.Value)
            {
                if (byte.TryParse(value.ToString(), out byte result))
                    return result;
            }
            return null;
        }

        /// <summary>
        /// decimal? price = PubGetSafeDecimal(row["Price"]);
        /// </summary>
        public static decimal? PubGetSafeDecimal(object value)
        {
            if (value != null && value != DBNull.Value)
            {
                if (decimal.TryParse(value.ToString(), out decimal result))
                    return result;
            }
            return null;
        }

        /// <summary>
        /// long? bigId = PubGetSafeLong(row["BigId"]);
        /// </summary>
        public static long? PubGetSafeLong(object value)
        {
            if (value != null && value != DBNull.Value)
            {
                if (long.TryParse(value.ToString(), out long result))
                    return result;
            }
            return null;
        }

        /// <summary>
        /// DateTime? orderDate = PubGetSafeDate(row["OrderDate"]);
        /// </summary>
        public static DateTime? PubGetSafeDate(object value)
        {
            if (value != null && value != DBNull.Value)
            {
                if (DateTime.TryParse(value.ToString(), out DateTime result))
                    return result;
            }
            return null;
        }

        /// <summary>
        /// object dbValue = row["ShiftStartTime"]
        /// TimeSpan? startTime = PubGetSafeTime(dbValue);
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TimeSpan? PubGetSafeTime(object value)
        {
            if (value != null && value != DBNull.Value)
            {
                if (TimeSpan.TryParse(value.ToString(), out TimeSpan result))
                    return result;
            }
            return null;
        }

        /// <summary>
        /// bool? isActive = PubGetSafeBoolean(row["IsActive"]);
        /// </summary>
        public static bool? PubGetSafeBoolean(object value)
        {
            if (value != null && value != DBNull.Value)
            {
                if (bool.TryParse(value.ToString(), out bool result))
                    return result;

                // Handle SQL bit values (0/1)
                if (int.TryParse(value.ToString(), out int intVal))
                    return intVal != 0;
            }
            return null;
        }

        #region "ConfigureDateTimePicker"
        /// <summary>
        /// 28.08.2025
        /// </summary>
        /// <param name="picker"></param>
        /// <param name="date"></param>
        public static void ConfigureDateTimePicker(DateTimePicker picker, DateTime date)
        {
            picker.Format = DateTimePickerFormat.Custom;
            picker.CustomFormat = "ddd, dd-MMM-yyyy";
            picker.Value = date.Date;
            picker.Checked = true;
        }
        #endregion

    }
}

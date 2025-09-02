using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;

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
///    Name:           TableNames.cs
///    Created:        22nd August 2025
///    Date Completed: 22nd August 2025
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
    [TypeConverter(typeof(TableNameConverter))]
    [Serializable]
    public class TableNames
    {
        private string _tableName1;
        private string _tableName2;
        private string _tableName3;
        private string _tableName4;
        private string _tableName5;
        private string _tableName6;
        private string _tableName7;
        private string _tableName8;
        private string _tableName9;

        public TableNames() { }

        /// <summary>
        /// CSV ctor: "t1,t2,t3,t4,t5,t6,t7,t8,t9"
        /// Missing items are treated as empty.
        /// </summary>
        public TableNames(string csv)
        {
            if (csv == null) csv = string.Empty;
            var fields = csv.Split(new[] { ',' }, StringSplitOptions.None)
                            .Select(s => s == null ? string.Empty : s.Trim())
                            .ToArray();

            _tableName1 = Get(fields, 0);
            _tableName2 = Get(fields, 1);
            _tableName3 = Get(fields, 2);
            _tableName4 = Get(fields, 3);
            _tableName5 = Get(fields, 4);
            _tableName6 = Get(fields, 5);
            _tableName7 = Get(fields, 6);
            _tableName8 = Get(fields, 7);
            _tableName9 = Get(fields, 8);
        }

        public TableNames(
            string table1, string table2, string table3,
            string table4, string table5, string table6,
            string table7, string table8, string table9)
        {
            _tableName1 = table1;
            _tableName2 = table2;
            _tableName3 = table3;
            _tableName4 = table4;
            _tableName5 = table5;
            _tableName6 = table6;
            _tableName7 = table7;
            _tableName8 = table8;
            _tableName9 = table9;
        }

        private static string Get(string[] arr, int i)
            => (arr != null && i >= 0 && i < arr.Length) ? arr[i] : string.Empty;

        public override string ToString()
        {
            // Always produce 9 comma-separated fields (can be empty)
            var sb = new StringBuilder(128);
            sb.Append(_tableName1 ?? string.Empty).Append(',')
              .Append(_tableName2 ?? string.Empty).Append(',')
              .Append(_tableName3 ?? string.Empty).Append(',')
              .Append(_tableName4 ?? string.Empty).Append(',')
              .Append(_tableName5 ?? string.Empty).Append(',')
              .Append(_tableName6 ?? string.Empty).Append(',')
              .Append(_tableName7 ?? string.Empty).Append(',')
              .Append(_tableName8 ?? string.Empty).Append(',')
              .Append(_tableName9 ?? string.Empty);
            return sb.ToString();
        }

        // Optional helpers if you want manual parsing
        public static TableNames Parse(string csv) => new TableNames(csv);

        public static bool TryParse(string csv, out TableNames result)
        {
            try
            {
                result = new TableNames(csv);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }

        #region Properties (PropertyGrid-friendly)

        [Category("Table Details"), DefaultValue(""),
         DisplayName("Table 1"),
         Description("Tables used in Form")]
        public string tableName1
        {
            get { return _tableName1; }
            set { _tableName1 = value; }
        }

        [Category("Table Details"), DefaultValue(""),
         DisplayName("Table 2"),
         Description("Tables used in Form")]
        public string tableName2
        {
            get { return _tableName2; }
            set { _tableName2 = value; }
        }

        [Category("Table Details"), DefaultValue(""),
         DisplayName("Table 3"),
         Description("Tables used in Form")]
        public string tableName3
        {
            get { return _tableName3; }
            set { _tableName3 = value; }
        }

        [Category("Table Details"), DefaultValue(""),
         DisplayName("Table 4"),
         Description("Tables used in Form")]
        public string tableName4
        {
            get { return _tableName4; }
            set { _tableName4 = value; }
        }

        [Category("Table Details"), DefaultValue(""),
         DisplayName("Table 5"),
         Description("Tables used in Form")]
        public string tableName5
        {
            get { return _tableName5; }
            set { _tableName5 = value; }
        }

        [Category("Table Details"), DefaultValue(""),
         DisplayName("Table 6"),
         Description("Tables used in Form")]
        public string tableName6
        {
            get { return _tableName6; }
            set { _tableName6 = value; }
        }

        [Category("Table Details"), DefaultValue(""),
         DisplayName("Table 7"),
         Description("Tables used in Form")]
        public string tableName7
        {
            get { return _tableName7; }
            set { _tableName7 = value; }
        }

        [Category("Table Details"), DefaultValue(""),
         DisplayName("Table 8"),
         Description("Tables used in Form")]
        public string tableName8
        {
            get { return _tableName8; }
            set { _tableName8 = value; }
        }

        [Category("Table Details"), DefaultValue(""),
         DisplayName("Table 9"),
         Description("Tables used in Form")]
        public string tableName9
        {
            get { return _tableName9; }
            set { _tableName9 = value; }
        }

        #endregion
    }

    /// <summary>
    /// Allows the PropertyGrid to treat 'tableNames' as a single CSV-editable string,
    /// while still exposing child properties if expanded.
    /// </summary>
    public class TableNameConverter : StringConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string)) return true;
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string s) return new TableNames(s);
            return base.ConvertFrom(context, culture, value);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string)) return true;
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is TableNames tn)
                return tn.ToString();

            return base.ConvertTo(context, culture, value, destinationType);
        }

        // Let PropertyGrid show sub-properties (tableName1..9) when expanded
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(typeof(TableNames), attributes);
        }
    }
}

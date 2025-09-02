using System;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.IO;
using TimeAttendanceManager.Main.Classes;
using System.Data.SqlClient;

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
///                    Implement the Singleton Pattern to ensure that only one instance of the class is used globally. 
///                    Encapsulation: All user-related information is encapsulated within the UserSession class.
///                    Controlled Access: By using the singleton pattern, you ensure only one instance of the class exists, 
///                    making it easy to control and modify globally.
///                    Flexibility: If needed, you can add methods to manage user session data within this class, 
///                    such as clearing session data on logout.
///                    This class-based approach is more maintainable and provides a cleaner structure than using a module, 
///                    especially if you have more complex requirements for handling user data globally.
///                    
///    Name:           ClassTechDetails.cs
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
    [Serializable]
    public class ClassTechDetails
    {
        #region Fields

        private string _applicationName;
        private string _applicationPath;
        private string _applicationVersion;

        private string _dbType;
        private string _dbName;
        private string _dbPath;
        private string _dbConnectionString;

        private ClassTableCollections _tableNames = new ClassTableCollections();

        private string _formName;
        private string _formTitle;
        private DateTime _dateOfCoding;
        private DateTime _lastUpdated;

        // Kept for compatibility with your VB code (unused in snippet)
        private TableNames _myTableNames;

        // Backing storage for logDetails
        private string[] _logDetails = new string[0];

        #endregion

        #region Constructors

        /// <summary>
        /// Parameterless: fills application details from assembly/application.
        /// You can set DB/Form details later via properties or use the overload below.
        /// </summary>
        public ClassTechDetails()
        {
            try
            {
                // Application Name & Path (WinForms-friendly)
                _applicationName = SafeGetAppName();
                _applicationPath = SafeGetAppPath();

                // Version: Major.Minor Build X Revision Y
                var ver = Assembly.GetEntryAssembly()?.GetName().Version
                          ?? Assembly.GetExecutingAssembly().GetName().Version
                          ?? new Version(1, 0, 0, 0);

                _applicationVersion = $"{ver.Major}.{ver.Minor} Build {ver.Build} Revision {ver.Revision}";

                // Defaults you can override
                _dbType = "Microsoft SQL Server 2019 (RTM) - 15.0.2000.5 (X64)";
                _dbName = Properties.Settings.Default.DatabaseName;
                _dbPath = Properties.Settings.Default.DatabaseServer;
                _dbConnectionString = ClassGlobalVariables.pubConnectionString;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error initializing ClassTechDetails\r\n\r\n" +
                    $"Message: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Overload to inject DB details up front.
        /// </summary>
        public ClassTechDetails(string dbType, string dbName, string dbPath, string dbConnectionString)
            : this()
        {
            _dbType = dbType ?? _dbType;
            _dbName = dbName ?? string.Empty;
            _dbPath = dbPath ?? string.Empty;
            _dbConnectionString = dbConnectionString ?? string.Empty;
        }

        #endregion

        #region Application Details

        [Category("1 Application Details")]
        [DefaultValue("")]
        [DisplayName("Name")]
        [ReadOnly(true)]
        [Description("Name of VB.NET/C# Project")]
        public string ApplicationName
        {
            get => _applicationName;
            set => _applicationName = value;
        }

        [Category("1 Application Details")]
        [DefaultValue("")]
        [DisplayName("Path")]
        [ReadOnly(true)]
        [Description("Path of the project executable")]
        public string ApplicationPath
        {
            get => _applicationPath;
            set => _applicationPath = value;
        }

        [Category("1 Application Details")]
        [DefaultValue("")]
        [DisplayName("Version")]
        [ReadOnly(true)]
        [Description("Version of the project")]
        public string ApplicationVersion
        {
            get => _applicationVersion;
            set => _applicationVersion = value;
        }

        #endregion

        #region Database Details

        [Category("2 Database Details")]
        [DefaultValue("")]
        [DisplayName("Type")]
        [Description("Type of Database")]
        public string DbType
        {
            get => _dbType;
            set => _dbType = value;
        }

        [Category("2 Database Details")]
        [DefaultValue("")]
        [DisplayName("Name")]
        [Description("Name of Database")]
        public string DbName
        {
            get => _dbName;
            set => _dbName = value;
        }

        [Category("2 Database Details")]
        [DefaultValue("")]
        [DisplayName("Connection")]
        [Description("Database Connection String")]
        public string DbConnectString
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_dbConnectionString))
                    return string.Empty;

                try
                {
                    var builder = new SqlConnectionStringBuilder(_dbConnectionString);
                    if (!string.IsNullOrEmpty(builder.Password))
                        builder.Password = "******";  // Mask password when displayed
                    return builder.ToString();
                }
                catch
                {
                    // If the string can't be parsed, just return masked
                    return "******";
                }
            }
            set
            {
                _dbConnectionString = value;
            }
        }

        [Category("2 Database Details")]
        [DefaultValue("")]
        [DisplayName("Path")]
        [Description("Path of Database")]
        public string DbPath
        {
            get => _dbPath;
            set => _dbPath = value;
        }

        #endregion

        #region Form Details

        [Category("3 Form Details")]
        [DefaultValue("")]
        [DisplayName("Name")]
        [Description("Name of the Form")]
        public string FormName
        {
            get => _formName;
            set => _formName = value;
        }

        [Category("3 Form Details")]
        [DefaultValue("")]
        [DisplayName("Title")]
        [Description("Description/Title of the Form")]
        public string FormTitle
        {
            get => _formTitle;
            set => _formTitle = value;
        }

        [Category("3 Form Details")]
        [DisplayName("Created")]
        [Description("Date the form was created")]
        public DateTime FormCreated
        {
            get => _dateOfCoding;
            set => _dateOfCoding = value;
        }

        [Category("3 Form Details")]
        [DisplayName("Modified")]
        [Description("Date the form was last modified")]
        public DateTime FormModified
        {
            get => _lastUpdated;
            set => _lastUpdated = value;
        }

        #endregion

        #region Change Log

        /// <summary>
        /// Array of text lines for your change log. 
        /// DesignerSerializationVisibility.Content allows PropertyGrid to persist contents.
        /// </summary>
        [Category("4 Change Log")]
        [Description("Click the ellipsis to open the editor (if provided).")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public string[] LogDetails
        {
            get => _logDetails ?? new string[0];
            set
            {
                _logDetails = value ?? new string[0];

                // If you want a combined string for some UI element, build it here:
                // (left intact from your VB logic, without writing to a TextBox)
                var arr = _logDetails;
                var sb = new StringBuilder(arr.Length * 32);
                for (int i = 0; i < arr.Length; i++)
                {
                    sb.Append(arr[i]);
                    sb.Append(Environment.NewLine);
                }
                // e.g., assign sb.ToString() to a viewer if needed
            }
        }

        #endregion

        #region Table Collections

        [Category("2 Database Details")]
        [DefaultValue("")]
        [DisplayName("Tables")]
        [Description("Tables used in the Form")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ClassTableCollections TableNames
        {
            get => _tableNames ?? (_tableNames = new ClassTableCollections());
            set => _tableNames = value ?? new ClassTableCollections();
        }

        #endregion

        #region Helpers

        private static string SafeGetAppName()
        {
            try
            {
                // Prefer WinForms Application info; fall back to assembly name
                var name = Application.ProductName;
                if (!string.IsNullOrWhiteSpace(name))
                    return name;

                return Assembly.GetEntryAssembly()?.GetName().Name
                       ?? Assembly.GetExecutingAssembly().GetName().Name
                       ?? "Application";
            }
            catch
            {
                return "Application";
            }
        }

        private static string SafeGetAppPath()
        {
            try
            {
                // WinForms-friendly executable path
                var path = Application.StartupPath;
                if (!string.IsNullOrWhiteSpace(path))
                    return path;

                // Fallback to assembly location
                var asm = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
                var loc = asm.Location;
                return string.IsNullOrWhiteSpace(loc) ? string.Empty : Path.GetDirectoryName(loc) ?? string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion
    }
}
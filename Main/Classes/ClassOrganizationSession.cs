
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
///    Title:          Create the OrganizationSession Class
///                    Implement the Singleton Pattern to ensure that only one instance of the class is used globally. 
///                    Encapsulation: All user-related information is encapsulated within the UserSession class.
///                    Controlled Access: By using the singleton pattern, you ensure only one instance of the class exists, 
///                    making it easy to control and modify globally.
///                    Flexibility: If needed, you can add methods to manage user session data within this class, 
///                    such as clearing session data on logout.
///                    This class-based approach is more maintainable and provides a cleaner structure than using a module, 
///                    especially if you have more complex requirements for handling user data globally.
///                    
///    Name:           ClassOrganizationSession.vb
///    Created:        14th August 2025
///    Date Completed: 14th August 2025
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


namespace TimeAttendanceManager.Main.Classes
{
    public sealed class ClassOrganizationSession
    {
        // Private static variable to hold the single instance
        private static ClassOrganizationSession _instance;
        private static readonly object _lock = new object();

        // Public properties
        public int? Id { get; set; }
        public string UnitCode { get; set; }
        public string UnitName { get; set; }
        public string UnitNameSlug { get; set; }
        public string OrgAddress { get; set; }
        public string GSTNo { get; set; }
        public string PANNo { get; set; }
        public string GSTStateCode { get; set; }

        // Private constructor
        private ClassOrganizationSession()
        {
            // Initialize string properties to empty strings if needed
            UnitCode = string.Empty;
            UnitName = string.Empty;
            UnitNameSlug = string.Empty;
            OrgAddress = string.Empty;
            GSTNo = string.Empty;
            PANNo = string.Empty;
            GSTStateCode = string.Empty;
        }

        // Public static property for singleton access
        public static ClassOrganizationSession Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ClassOrganizationSession();
                        }
                    }
                }
                return _instance;
            }
        }
    }
}


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
///    Title:          Create the UserSession Class
///                    Implement the Singleton Pattern to ensure that only one instance of the class is used globally. 
///                    Encapsulation: All user-related information is encapsulated within the UserSession class.
///                    Controlled Access: By using the singleton pattern, you ensure only one instance of the class exists, 
///                    making it easy to control and modify globally.
///                    Flexibility: If needed, you can add methods to manage user session data within this class, 
///                    such as clearing session data on logout.
///                    This class-based approach is more maintainable and provides a cleaner structure than using a module, 
///                    especially if you have more complex requirements for handling user data globally.
///                    
///    Name:           ClassUserSession.vb
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
    public sealed class ClassUserSession
    {
        // Private static variable to hold the single instance of the class
        private static ClassUserSession _instance;
        private static readonly object _lock = new object();

        // Public properties to store user details
        public int? LoginUserRowId { get; set; }
        public string LoginUserRowGuid { get; set; }
        public string LoginUserName { get; set; }
        public string LoginUserFullName { get; set; }
        public string LoginUserUnitCode { get; set; }
        public int? LoginUserRoleId { get; set; }
        public string LoginUserRole { get; set; }
        public string LoginUserRoleDesc { get; set; }
        public string LoginDepartmentCode { get; set; }
        public string LoginUserDeptName { get; set; }
        public string LoginUserEmail { get; set; }
        public string LoginUserFirstName { get; set; }
        public string LoginUserLastName { get; set; }
        public string LoginUserFirstLastName { get; set; }
        public bool LoginUserIsDeleted { get; set; }

        // Private constructor to prevent direct instantiation
        private ClassUserSession()
        {
            // Initialize properties if needed
            LoginUserRowGuid = string.Empty;
            LoginUserName = string.Empty;
           
        }

        // Public static property to provide access to the single instance of the class
        public static ClassUserSession Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ClassUserSession();
                        }
                    }
                }
                return _instance;
            }
        }
    }
}

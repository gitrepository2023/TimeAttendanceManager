using System;
using System.Text;
using System.Security.Cryptography;

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
///    Name:           PasswordHelper.cs
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

namespace TimeAttendanceManager.Auth.Classes
{
    public static class PasswordHelper
    {
        /// <summary>
        /// Generate a random salt
        /// </summary>
        public static string GenerateSalt()
        {
            byte[] salt = new byte[32]; // 32 bytes
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        /// <summary>
        /// Hash a password using SHA256 + salt
        /// </summary>
        public static string HashPassword(string password, string salt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                string saltedPassword = password + salt;
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                return Convert.ToBase64String(hashBytes);
            }
        }

        /// <summary>
        /// Hash a password using SHA256 and return as byte array
        /// </summary>
        public static byte[] HashPasswordToBytes(string password, string salt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password + salt);
                return sha256.ComputeHash(passwordBytes);
            }
        }

        /// <summary>
        /// Verify password against stored hash and salt
        /// </summary>
        public static bool VerifyPassword(string inputPassword, string storedHash, string salt)
        {
            string hashOfInput = HashPassword(inputPassword, salt);
            return string.Equals(hashOfInput, storedHash);
        }

        /// <summary>
        /// Constant-time comparison to prevent timing attacks
        /// </summary>
        public static bool SecureCompare(byte[] a, byte[] b)
        {
            uint diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < Math.Min(a.Length, b.Length); i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }
            return diff == 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TimeAttendanceManager.Helpers.Classes
{
    class ClassStringHelpers
    {
        #region "CleanAndUpperCase"
        /// <summary>
        /// Remove extra spaces and change the case to upper case
        /// Return If(value IsNot Nothing, value.Trim().ToUpper(), Nothing)
        /// 
        /// Remove extra spaces and change the case to upper case
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string CleanAndUpperCase(string value)
        {
            return value != null ?
                System.Text.RegularExpressions.Regex.Replace(value.Trim(), @"\s+", " ").ToUpper() :
                null;
        }
        #endregion

        #region "CleanAndLowerCase"
        /// <summary>
        /// Remove extra spaces and change the case to lower case
        /// Return If(value IsNot Nothing, value.Trim().ToLower(), Nothing)
        /// 
        /// Remove extra spaces and change the case to lower case
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string CleanAndLowerCase(string value)
        {
            return value != null ? System.Text.RegularExpressions.Regex.Replace(value.Trim(), @"\s+", " ").ToLower() : null;
        }
        #endregion

        #region "CleanAndNormalizeString"
        /// <summary>
        /// Takes an input string, removes leading and trailing spaces, and condenses 
        /// multiple spaces between words to a single space. It then returns the processed string:
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string CleanAndNormalizeString(string input)
        {
            return Regex.Replace(input.Trim(), @"\s+", " ");
        }
        #endregion

    }
}

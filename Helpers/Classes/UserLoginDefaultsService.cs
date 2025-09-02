using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace TimeAttendanceManager.Helpers.Classes
{
    /// <summary>
    /// 30.08.2025
    /// Create a Global Helper Class
    /// 
    /// var vals = await UserLoginDefaultsService.GetUserLoginDefaultsAsync(
    ///     ClassGlobalVariables.pubUnitCode,
    ///     ClassGlobalVariables.pubLoginUserRowGuid);
    /// 
    /// if (vals.TryGetValue("PlantCode", out string plantCode) && !string.IsNullOrEmpty(plantCode))
    /// {
    ///     CmbUnitCode.Text = plantCode;
    /// }
    /// 
    /// if (vals.TryGetValue("EmployeeCategory", out string empCategory))
    /// {
    ///     TxtEmployeeCategory.Text = empCategory; // for example
    /// }
    /// </summary>
    public static class UserLoginDefaultsService
    {

        public static async Task<Dictionary<string, string>> GetUserLoginDefaultsAsync(
            string unitCode,
            string userRowGuid)
        {
            try
            {
                var wanted = new[] { "PlantCode", "EmployeeCategory" };

                var vals = await UserLoginDefaultsHelper.GetAsync(
                    unitCode: unitCode,
                    userRowGuid: userRowGuid,
                    wantedColumns: wanted,
                    tableName: "dbo.UserLoginDefaults");

                return (Dictionary<string, string>)vals; // return dictionary
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Message: {ex.Message}\n\nStack Trace: {ex.StackTrace}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return new Dictionary<string, string>(); // return empty if error
            }
        }

    }
}

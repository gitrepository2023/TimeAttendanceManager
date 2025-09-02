using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
///     
/// Use non-static when:
/// 
///     The method needs to access or modify the form's/object's state
/// 
///     The behavior might vary based on instance properties
/// 
///     You're implementing object-oriented behavior
/// 
/// Use static when:
/// 
///     The method is a pure utility function
/// 
///     It doesn't need any instance state
/// 
///     You want to call it without creating an object
/// 
///     The operation is stateless (same inputs always produce same outputs)
/// </summary>
/// 
/// <Email>
///     
/// </Email>
/// 
/// <Module>
///    Title:          Global Functions used in project
///    Name:           ClassLayoutHelper.cs
///    Created:        15th August 2025
///    Date Completed: 15th August 2025
/// </Module >
/// 
/// <ChangeLog>
///   Date Modified:   NA
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
    class ClassLayoutHelper
    {

        #region "ConfigureTableLayout"

        /// <summary>
        /// Helper function to dock and set fore color of controls
        /// </summary>
        /// <param name="tableLayout"></param>
        public static void ConfigureTableLayout(TableLayoutPanel tableLayout)
        {
            tableLayout.Dock = DockStyle.Fill;
            foreach (Control myControl in tableLayout.Controls)
            {
                if (myControl is Button)
                    continue;
                myControl.Dock = DockStyle.Fill;
                if (myControl is TextBox || myControl is ComboBox || myControl is NumericUpDown)
                    myControl.ForeColor = Color.Blue;
            }
        }

        #endregion

        #region "ClearErrorsTableLayout"
        public static void ClearErrorsTableLayout(TableLayoutPanel myTableLayout, ErrorProvider myErrorProvider)
        {
            foreach (Control ctrl in myTableLayout.Controls)
                myErrorProvider.SetError(ctrl, "");
        }
        #endregion

        #region "ClearControlsTableLayout"
        /// <summary>
        /// Used switch pattern matching for cleaner type checks.
        /// Used string.Empty instead of null — safer for WinForms text controls.
        /// Used 0m for decimals so it’s explicit for NumericUpDown.
        /// </summary>
        /// <param name="myTableLayout"></param>
        public static void ClearControlsTableLayout(TableLayoutPanel myTableLayout)
        {
            foreach (Control ctrl in myTableLayout.Controls)
            {
                switch (ctrl)
                {
                    case TextBox _:
                    case ComboBox _:
                    case MaskedTextBox _:
                        ctrl.Text = string.Empty;
                        break;

                    case NumericUpDown numUpDown:
                        numUpDown.Value = 0m;
                        break;
                }
            }
        }

        #endregion

        #region "GetFormTypeByName"
        /// <summary>
        /// 15.08.2025
        /// helper method to look through all types in your project — even without knowing the namespace:
        /// </summary>
        /// <param name="formClassName"></param>
        /// <returns></returns>
        public Type GetFormTypeByName(string formClassName)
        {
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var t in asm.GetTypes())
                {
                    if (t.IsSubclassOf(typeof(Form)) &&
                        t.Name.Equals(formClassName, StringComparison.OrdinalIgnoreCase))
                    {
                        return t;
                    }
                }
            }
            return null;
        }
        #endregion

        #region "GetFormTypeByNameLinq"
        /// <summary>
        /// 15.08.2025
        /// helper method to look through all types in your project — even without knowing the namespace:
        /// Checks both Name (just class name) and FullName (namespace + class name).
        /// Will work even if multiple assemblies have forms.
        /// Still case-insensitive.
        /// more concise and faster version using LINQ
        /// Usage
        /// Type formType = GetFormTypeByName("MyForm"); // just class name
        /// or 
        /// Type formTypeFull = GetFormTypeByName("TimeAttendanceManager.Auth.Forms.MyForm"); // full name
        ///  if (formType != null)
        ///  {
        ///    Form formInstance = (Form)Activator.CreateInstance(formType);
        ///        formInstance.Show();
        ///  }
        /// </summary>
        /// <param name="formClassName"></param>
        /// <returns></returns>
        public static Type GetFormTypeByNameLinq(string formClassName)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(asm => asm.GetTypes())
            .FirstOrDefault(t => t.IsSubclassOf(typeof(Form)) &&
                (t.Name.Equals(formClassName, StringComparison.OrdinalIgnoreCase) ||
                t.FullName.Equals(formClassName, StringComparison.OrdinalIgnoreCase)));
        }

        #endregion

        #region "GetReportFolderPath"
        public static string GetReportFolderPath(string foldername)
        {
            // candidate drives in priority order
            string[] candidateDrives = { @"D:\", @"E:\", @"F:\" };
            string baseDrive = null;

            // pick the first available drive
            foreach (var drive in candidateDrives)
            {
                if (Directory.Exists(drive))
                {
                    baseDrive = drive;
                    break;
                }
            }

            // if none found, fallback to Desktop
            if (string.IsNullOrEmpty(baseDrive))
            {
                baseDrive = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\";
            }

            // build the report folder path
            string folderPath = Path.Combine(baseDrive, "Reports", Application.ProductName, foldername);

            // create folder if it doesn’t exist
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            return folderPath;
        }
        #endregion


    }
}

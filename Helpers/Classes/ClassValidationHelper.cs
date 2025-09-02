using System;
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
/// </summary>
/// 
/// <Email>
///     (anil@gtnindustries.com)
/// </Email>
/// 
/// <Module>
///    Title:          Global Helper
///    Name:           ClassValidationHelper.cs
///    Created:        15th August 2025
///    Date Completed: 15th August 2025
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
    public class ClassValidationHelper
    {

        #region "ValidateControl"

        /// <summary>
        /// 15.08.2025
        /// Usage
        /// ValidationHelper.ValidateControl(
        ///     myTextBox,
        ///     "Name cannot be empty.",
        ///     ErrorProvider1,
        ///     TsLblInputStatus
        /// );
        /// </summary>
        public static void ValidateControl(
                Control control,
                string errorMessage,
                ErrorProvider errorProvider,
                ToolStripStatusLabel statusLabel)
            {
                if (control == null)
                    throw new ArgumentNullException(nameof(control));

                if (errorProvider == null)
                    throw new ArgumentNullException(nameof(errorProvider));

                if (statusLabel == null)
                    throw new ArgumentNullException(nameof(statusLabel));

                if (string.IsNullOrWhiteSpace(control.Text))
                {
                    statusLabel.Text = errorMessage ?? "Invalid input.";
                    errorProvider.SetError(control, errorMessage ?? "Invalid input.");
                    throw new InvalidOperationException(errorMessage ?? "Invalid input.");
                }
                else
                {
                    // Clear previous error when valid
                    errorProvider.SetError(control, string.Empty);
                }
            }
       
        #endregion

        #region "ValidateCmbSelectedValue"
        /// <summary>
        /// 15.08.2025
        /// Usage
        /// int? selectedId = ValidationHelper.ValidateCmbSelectedValue(
        ///     myComboBox,
        ///     "Please select a valid item.",
        ///     ErrorProvider1,
        ///     TsLblInputStatus
        /// );
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="errorMessage"></param>
        /// <param name="errorProvider"></param>
        /// <param name="statusLabel"></param>
        /// <returns></returns>
        public static int? ValidateCmbSelectedValue(
            ComboBox comboBox,
            string errorMessage,
            ErrorProvider errorProvider,
            ToolStripStatusLabel statusLabel)
        {
            if (comboBox == null)
                throw new ArgumentNullException(nameof(comboBox));
            if (errorProvider == null)
                throw new ArgumentNullException(nameof(errorProvider));
            if (statusLabel == null)
                throw new ArgumentNullException(nameof(statusLabel));

            if (comboBox.SelectedValue != null &&
                int.TryParse(comboBox.SelectedValue.ToString(), out int selectedId))
            {
                errorProvider.SetError(comboBox, string.Empty);
                return selectedId;
            }
            else
            {
                statusLabel.Text = errorMessage ?? "Invalid selection.";
                errorProvider.SetError(comboBox, errorMessage ?? "Invalid selection.");
                throw new InvalidOperationException(errorMessage ?? "Invalid selection.");
            }
        }
        #endregion

        #region "ValidateTextBoxLength"
        /// <summary>
        /// 15.08.2025
        /// Usage
        /// ValidationHelper.ValidateTextBoxLength(
        ///     txtName,
        ///     50,
        ///     "Name",
        ///     ErrorProvider1,
        ///     TsLblInputStatus
        /// ); 
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="maxLength"></param>
        /// <param name="fieldDescription"></param>
        /// <param name="errorProvider"></param>
        /// <param name="statusLabel"></param>
        public static void ValidateTextBoxLength(
           TextBox textBox,
           int maxLength,
           string fieldDescription,
           ErrorProvider errorProvider,
           ToolStripStatusLabel statusLabel)
        {
            if (textBox == null) throw new ArgumentNullException(nameof(textBox));
            if (errorProvider == null) throw new ArgumentNullException(nameof(errorProvider));
            if (statusLabel == null) throw new ArgumentNullException(nameof(statusLabel));
            if (maxLength <= 0) throw new ArgumentOutOfRangeException(nameof(maxLength), "Max length must be greater than zero.");

            if (!string.IsNullOrEmpty(textBox.Text) && textBox.Text.Length > maxLength)
            {
                string errorMessage = $"{fieldDescription} - Please limit the input to a maximum of {maxLength} characters.";
                statusLabel.Text = errorMessage;
                errorProvider.SetError(textBox, errorMessage);
                throw new InvalidOperationException(errorMessage);
            }
            else
            {
                errorProvider.SetError(textBox, string.Empty);
            }
        }
        #endregion

        #region "IsValidEmail"
        /// <summary>
        /// 15.08.2025
        /// Used verbatim string literal (@) for the regex to avoid escaping \.
        /// Made the method static so it can be placed in your ValidationHelper class for global access.
        /// 
        /// Usage
        /// if (string.IsNullOrWhiteSpace(email))
        ///     return false; 
        ///     
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            var regex = new Regex(emailPattern);
            return regex.IsMatch(email);
        }
        #endregion


    }
}

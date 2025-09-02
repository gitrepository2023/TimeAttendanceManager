using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using static TimeAttendanceManager.Main.Classes.ClassGlobalFunctions;
using static TimeAttendanceManager.Helpers.Classes.ClassDbHelpers;
using TimeAttendanceManager.Main.Classes;
using TimeAttendanceManager.Auth.Classes;

/// <Copyright>
///      Copyright © 2025-2026 GTN Industries Ltd 
///     All rights reserved.
///     IT Department 
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
/// Generic About Box for use with my projects. Provides basic system information as well as
/// system up time and the current date and time. The header is a panel which my contain a
/// background image or color. A picture box (48x48 pixels) can display a custom image.
/// 
/// There are two buttons on the form, "Microsoft System Information," which starts msinfo32.exe
/// and "OK," which dismisses the form. Typing Escape on the keyboard will also dismiss the form.
/// 
/// There are two link labels. The first opens a file called "EULA.pdf" Imports the Adobe Reader that
/// is installed on the computer. The "EULA.pdf" file must reside in the application folder. The other
/// link label initiates an email to me. This is normally not visible for non-commercial applications.
/// the application folder.
/// The application will produce an error dialog if msinfo32.exe or EULA.pdf is not found.
/// </summary>
/// 
/// <Email>
///     (anil@gtnindustries.com)
/// </Email>
/// 
/// <form>
///    Title:          Code for User's Login
///                    
/// 
///    Name:           FrmAuthUsersLogin.cs
///    Created:        13th August 2025
///    Date Completed: 13th August 2025
/// </form >
/// 
/// <formChangeLog>
///   Date Modified:   NA
/// 
/// </formChangeLog>
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

namespace TimeAttendanceManager.Auth.Forms
{
    public partial class FrmAuthUsersLogin : Form
    {
        public FrmAuthUsersLogin()
        {
            InitializeComponent();

            // Wire up the event handler to each textbox
            TxtUserName.GotFocus += TextBox_GotFocus;
            TxtUserPassword.GotFocus += TextBox_GotFocus;
        }

        #region "FormEvents"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmAuthUsersLogin_Load(object sender, EventArgs e)
        {
            try
            {
                // Get the last save settings
                CmbPlantCode.Items.Clear();
                LoadUnitCodes();

                TxtUserName.Text = Properties.Settings.Default.UserLoginName;
                CmbPlantCode.Text = Properties.Settings.Default.PlantCode;

                TxtUserName.Text = Properties.Settings.Default.UserLoginName;

                // Set focus after form is shown
                this.BeginInvoke(new MethodInvoker(delegate
                {
                    TxtUserPassword.Focus();
                    TxtUserPassword.SelectAll(); // Optional: highlights existing text
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName,
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region "TextBoxEvents"

            private void TextBox_GotFocus(object sender, EventArgs e)
            {
                TextBox selectedTextBox = sender as TextBox;
                if (selectedTextBox != null)
                {
                  GetTextSelected(selectedTextBox);
                }
            }

        #endregion

        #region "ButtonEvents"

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnOkay_Click(object sender, EventArgs e)
        {
            try
            {
                // Input validation
                if (string.IsNullOrWhiteSpace(CmbPlantCode.Text))
                {
                    throw new Exception("Plant Code is required");
                }

                if (string.IsNullOrWhiteSpace(TxtUserName.Text))
                {
                    throw new Exception("User Login Name is required");
                }

                if (string.IsNullOrWhiteSpace(TxtUserPassword.Text))
                {
                    throw new Exception("User Login Password is required");
                }

                // Validate credentials asynchronously
                bool isValidUser = await ValidateCredentials(
                    TxtUserName.Text.Trim(),
                    TxtUserPassword.Text,
                    CmbPlantCode.Text.Trim());

                if (!isValidUser)
                {
                    TxtUserPassword.Focus();
                    this.DialogResult = DialogResult.None;
                    throw new Exception("Invalid User Login or Password, try again!");
                }
                else
                {
                    // Save settings
                    Properties.Settings.Default.UserLoginName = this.TxtUserName.Text;
                    Properties.Settings.Default.PlantCode = this.CmbPlantCode.Text;
                    Properties.Settings.Default.Save();
                   
                    await GetLoginOrganisationDetails(this.CmbPlantCode.Text);

                    await UpsertUserLoginHits();

                    await GetUserLoginDetails(
                        TxtUserName.Text.Trim(),
                        CmbPlantCode.Text.Trim());

                    // Load SQL date in ClassGlobalVariables.SqlServerTodayDate
                    await ClassGlobalFunctions.LoadSqlServerTodayDateAsync();

                    // After successful login:
                    string userGuidStr = ClassGlobalVariables.pubLoginUserRowGuid; // nvarchar(150)
                    string cs = ClassGlobalFunctions.GetConnectionStringByUnitCode(this.CmbPlantCode.Text);
                    await UserAccessCache.LoadUserAccessAsync(userGuidStr, cs);
                    if (!UserAccessCache.HasAccess(this.CmbPlantCode.Text))
                    {
                        MessageBox.Show("You do not have access to the selected Plant.","Access",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        return;
                    }

                    // Close the dialog
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                // Error handling
                TsLblInputStatus.Text = ex.Message;
                TsLblInputStatus.ForeColor = Color.Red;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
            {
                DialogResult = DialogResult.Cancel;
                Close();
                Application.Exit();
            }

        #endregion

        #region "LoadUnitCodes"
            /// <summary>
            /// During development: YourProject\Config\unitcodes.json
            /// After build: YourApp\bin\Debug\Config\unitcodes.json (or \Release\)
            /// </summary>
            private void LoadUnitCodes()
            {
                try
                {
                    // Get the path to the JSON file in the output directory
                    string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "unitcodes.json");

                    if (File.Exists(jsonPath))
                    {
                        string json = File.ReadAllText(jsonPath);
                        dynamic data = JsonConvert.DeserializeObject(json);

                        CmbPlantCode.Items.Clear();
                        foreach (var code in data.UnitCodes)
                        {
                            CmbPlantCode.Items.Add(code.ToString());
                        }
                    }
                    else
                    {
                        CmbPlantCode.Items.Add("3000");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading unit codes: {ex.Message}");
                }
            }
        #endregion

    }


}

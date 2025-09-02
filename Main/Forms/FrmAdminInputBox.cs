using System;
using System.Windows.Forms;

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
///    Name:           FrmAdminInputBox.cs
///    Created:        14th August 2025
///    Date Completed: 14th August 2025
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

namespace TimeAttendanceManager.Main.Forms
{
    public partial class FrmAdminInputBox : Form
    {
        public FrmAdminInputBox()
        {
            InitializeComponent();
            this.BtnOkay.Click += (s, e) => BtnOkay_Click(s, e);
            this.BtnCancel.Click += (s, e) => BtnCancel_Click(s, e);
            this.Load += FrmAdminInputBox_Load;  // Add this line to wire up Load event
            // Ensure buttons exist
            if (BtnOkay == null || BtnCancel == null)
                throw new InvalidOperationException("Buttons not initialized");
        }

        private void FrmAdminInputBox_Load(object sender, EventArgs e)
        {
            // Set focus after form is shown
            this.BeginInvoke(new MethodInvoker(delegate
            {
                TxtPassword.Focus();
                TxtPassword.SelectAll(); // Optional: highlights existing text
            }));
        }

        private void BtnOkay_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

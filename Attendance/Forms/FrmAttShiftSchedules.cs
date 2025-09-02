using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimeAttendanceManager.Main.Forms;
using TimeAttendanceManager.Contractors.Models;
using TimeAttendanceManager.Helpers.Classes;
using TimeAttendanceManager.Main.Classes;
using TimeAttendanceManager.Auth.Classes;
using System.Globalization;
using System.Data.SqlClient;
using System.IO;
using TimeAttendanceManager.Attendance.Models;
using System.Web;
using Microsoft.Web.WebView2.WinForms;
using TimeAttendanceManager.Helpers;
using System.Diagnostics;

/// <Copyright>
///     Copyright © 2025-2026 GTN Industries Ltd 
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
///    Title:          Code for UPSERT
///                    
/// 
///    Name:           FrmAttShiftSchedules.cs
///    Created:        26th August 2025
///    Date Completed: 26th August 2025
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

namespace TimeAttendanceManager.Attendance.Forms
{
    public partial class FrmAttShiftSchedules : Form
    {
        #region "Contructor"
        public FrmAttShiftSchedules()
        {
            InitializeComponent();

            // let the form sees keys early
            this.KeyPreview = true;

            // Remove events
            this.Load -= Form_Load;
            this.FormClosing -= FormClosing_FormClosing;
            this.TsBtnCloseForm.Click -= TsBtnCloseForm_Click;
            this.TsBtnClose.Click -= TsBtnCloseForm_Click;
            this.TabCtrlMain.Click -= TabCtrlMain_Click;
            this.TsBtnSidePanel.Click -= TsBtnSideMenu_Click;
            this.TsBtnHelp.Click -= TsBtnHelp_Click;
            this.TsBtnOptTech.Click -= TsBtnOptTech_Click;
            this.TsBtnFilterClose.Click -= TsBtnFilterClose_Click;
            this.TsMenuViewInActive.Click -= TsMenuViewInActive_Click;
            this.TsMenuViewDeleted.Click -= TsMenuViewDeleted_Click;
            this.DgvList.CellFormatting -= DgvList_CellFormatting;
            this.TsBtRefreshDgv.Click -= TsBtRefreshDgv_Click;
            this.TsBtnClearSearchDgv.Click -= TsBtRefreshDgv_Click;
            this.TsBtnSearchDgv.Click -= TsBtRefreshDgv_Click;
            this.TsTxtSearchDgv.KeyDown -= TsTxtSearchDgv_KeyDown;
            this.TsBtnDrpUnitCode.DropDownItemClicked -= TsBtnDrpBtn_DropDownItemClicked;
            this.ChkIsActive.CheckedChanged -= ChkIsActive_CheckedChanged;
            this.DgvList.CellClick -= DgvList_CellClick;
            this.TsBtnAddNew.Click -= TsBtnAddNew_Click;
            this.TsBtnSave.Click -= TsBtnSave_Click;
            this.TsBtnDelete.Click -= TsBtnDelete_Click;
            this.TsBtnRefreshWebView.Click -= TsBtnRefreshWebView_Click;
            this.TsBtnExport.Click -= TsBtnExport_Click;
            this.TsBtnExporotExcel.Click -= TsBtnExporotExcel_Click;

            this.NumShiftTotalHrs.Enter -= (s, e) => SelectNumericUpDownText(NumShiftTotalHrs);
            this.NumShiftGraceEarlyMin.Enter -= (s, e) => SelectNumericUpDownText(NumShiftGraceEarlyMin);
            this.NumShiftGraceLateMin.Enter -= (s, e) => SelectNumericUpDownText(NumShiftGraceLateMin);
            this.NumShiftLunchMinTime.Enter -= (s, e) => SelectNumericUpDownText(NumShiftLunchMinTime);
            this.NumShiftLunchGraceEarlyMin.Enter -= (s, e) => SelectNumericUpDownText(NumShiftLunchGraceEarlyMin);
            this.NumShiftLunchGraceLateMin.Enter -= (s, e) => SelectNumericUpDownText(NumShiftLunchGraceLateMin);
            this.NumShiftLunchHour.Enter -= (s, e) => SelectNumericUpDownText(NumShiftLunchHour);
            this.NumShiftOtMinMinutes.Enter -= (s, e) => SelectNumericUpDownText(NumShiftOtMinMinutes);
            this.NumShiftInLatePermitted.Enter -= (s, e) => SelectNumericUpDownText(NumShiftInLatePermitted);
            this.NumLunchLatePermitted.Enter -= (s, e) => SelectNumericUpDownText(NumLunchLatePermitted);

            // Add events
            this.Load += Form_Load;
            this.FormClosing += FormClosing_FormClosing;
            this.TsBtnCloseForm.Click += TsBtnCloseForm_Click;
            this.TsBtnClose.Click += TsBtnCloseForm_Click;
            this.TabCtrlMain.Click += TabCtrlMain_Click;
            this.TsBtnSidePanel.Click += TsBtnSideMenu_Click;
            this.TsBtnHelp.Click += TsBtnHelp_Click;
            this.TsBtnOptTech.Click += TsBtnOptTech_Click;
            this.TsBtnFilterClose.Click += TsBtnFilterClose_Click;
            this.TsMenuViewInActive.Click += TsMenuViewInActive_Click;
            this.TsMenuViewDeleted.Click += TsMenuViewDeleted_Click;
            this.DgvList.CellFormatting += DgvList_CellFormatting;
            this.TsBtRefreshDgv.Click += TsBtRefreshDgv_Click;
            this.TsBtnClearSearchDgv.Click += TsBtRefreshDgv_Click;
            this.TsBtnSearchDgv.Click += TsBtRefreshDgv_Click;
            this.TsTxtSearchDgv.KeyDown += TsTxtSearchDgv_KeyDown;
            this.TsBtnDrpUnitCode.DropDownItemClicked += TsBtnDrpBtn_DropDownItemClicked;
            this.ChkIsActive.CheckedChanged += ChkIsActive_CheckedChanged;
            this.DgvList.CellClick += DgvList_CellClick;
            this.TsBtnAddNew.Click += TsBtnAddNew_Click;
            this.TsBtnSave.Click += TsBtnSave_Click;
            this.TsBtnDelete.Click += TsBtnDelete_Click;
            this.TsBtnRefreshWebView.Click += TsBtnRefreshWebView_Click;
            this.TsBtnExport.Click += TsBtnExport_Click;
            this.TsBtnExporotExcel.Click += TsBtnExporotExcel_Click;

            this.NumShiftTotalHrs.Enter += (s, e) => SelectNumericUpDownText(NumShiftTotalHrs);
            this.NumShiftGraceEarlyMin.Enter += (s, e) => SelectNumericUpDownText(NumShiftGraceEarlyMin);
            this.NumShiftGraceLateMin.Enter += (s, e) => SelectNumericUpDownText(NumShiftGraceLateMin);
            this.NumShiftLunchMinTime.Enter += (s, e) => SelectNumericUpDownText(NumShiftLunchMinTime);
            this.NumShiftLunchGraceEarlyMin.Enter += (s, e) => SelectNumericUpDownText(NumShiftLunchGraceEarlyMin);
            this.NumShiftLunchGraceLateMin.Enter += (s, e) => SelectNumericUpDownText(NumShiftLunchGraceLateMin);
            this.NumShiftLunchHour.Enter += (s, e) => SelectNumericUpDownText(NumShiftLunchHour);
            this.NumShiftOtMinMinutes.Enter += (s, e) => SelectNumericUpDownText(NumShiftOtMinMinutes);
            this.NumShiftInLatePermitted.Enter += (s, e) => SelectNumericUpDownText(NumShiftInLatePermitted);
            this.NumLunchLatePermitted.Enter += (s, e) => SelectNumericUpDownText(NumLunchLatePermitted);

        }
        #endregion

        #region "LocalVariables"
        private AttShiftSchedule myDataTable = new AttShiftSchedule();
        private ContextMenuStrip _cmsDgvList;
        bool? isActiveRows = true;
        bool? isDeletedRows = false;
        #endregion

        #region "MyBase_Load"
        private async void Form_Load(object sender, EventArgs e)
        {
            // Show the wait/progress form
            using (var progress = new FrmAdminProgress())
            {
                progress.ProgressBar1.Visible = false;
                progress.Show();
                Application.DoEvents(); // Allow the UI to render the progress form

                try
                {
                    this.Hide();

                    // Set Splitter Control Panel Distance
                    SetSplitterDistance();

                    // Dock Controls
                    DockControls();

                    // Set Default Values
                    await SetDefaultValuesAsync();

                    // Select default tab
                    TabCtrlMain.SelectTab(0);
                    TabCtrlMain_Click(TabCtrlMain, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Form Load", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    this.Visible = true;
                    this.Cursor = Cursors.Default;
                    progress.Close();
                    progress.Dispose();
                }
            }
        }
        #endregion

        #region "FormClosing_FormClosing"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormClosing_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // 1. First check if the MDI parent form exists
                //if (MDIFrmDashBoard.Instance == null || MDIFrmDashBoard.Instance.TabCtrlMain == null)
                //{
                //    MessageBox.Show("Main dashboard not available", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}

                // If MDI main form not available, just allow closing without tab removal
                if (MDIFrmDashBoard.Instance?.TabCtrlMain == null)
                {
                    return;
                }

                // 2. Show confirmation dialog
                if (MessageBox.Show("Close this form?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }

                // 3. Safely remove tab
                TabPage selectedTab = MDIFrmDashBoard.Instance.TabCtrlMain.SelectedTab;
                if (selectedTab != null)
                {
                    MDIFrmDashBoard.Instance.TabCtrlMain.TabPages.Remove(selectedTab);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error closing form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region "CloseForm"
        private void TsBtnCloseForm_Click(object sender, EventArgs e)
        {

            this.Close();
        }
        #endregion

        #region "ProcessCmdKey"
        /// <summary>
        /// user presses Enter inside any control in your TableLayoutPanel 
        /// the focus should move to the next control (like Tab key behavior).
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                try
                {
                    var leaf = GetLeafActiveControl();
                    if (leaf != null && !leaf.IsDisposed)
                    {
                        this.SelectNextControl(
                            leaf,      // start from the leaf-focused control
                            true,      // forward
                            true,      // tabStopOnly
                            true,      // nested
                            true       // wrap
                        );
                        return true; // handled
                    }
                }
                catch (Exception ex)
                {
                    // Log if needed, or show in debug
                    System.Diagnostics.Debug.WriteLine("ProcessCmdKey error: " + ex.Message);
                    // Optional: MessageBox.Show("Unexpected navigation error: " + ex.Message);
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }


        private Control GetLeafActiveControl()
        {
            Control ctl = this.ActiveControl;
            // Drill down through nested containers to the focused leaf control
            while (ctl is ContainerControl cc && cc.ActiveControl != null)
                ctl = cc.ActiveControl;
            return ctl;
        }
        #endregion

        #region "SetSplitterDistance"
        private void SetSplitterDistance()
        {
            try
            {
                // Hide Panel
                SplitRight.Panel2Collapsed = true;

                // Set SplitterDistance to 30% and 40% of the total width
                SplitMain.SplitterDistance = (int)(this.SplitMain.Width * 0.3);
                SplitRight.SplitterDistance = (int)(this.SplitRight.Width * 0.6);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                              "Set Splitter Distance",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
            }
        }
        #endregion

        #region "DockControls"
        private void DockControls()
        {
            ClassLayoutHelper.ConfigureTableLayout(TableLayout1);
        }
        #endregion

        #region "TabCtrlClick"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void TabCtrlMain_Click(object sender, EventArgs e)
        {
            try
            {
                var tabCtrl = sender as TabControl;
                if (tabCtrl == null) return;

                tabCtrl.ImageList = ImageListTabCtrl;

                foreach (TabPage tab in tabCtrl.TabPages)
                {
                    tab.ImageKey = null;
                }

                tabCtrl.SelectedTab.ImageIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Tab Click", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region "TsBtnSideMenu_Click"
        private void TsBtnSideMenu_Click(object sender, EventArgs e)
        {

            // Hide Panel
            SplitMain.Panel1Collapsed = !SplitMain.Panel1Collapsed;
        }
        #endregion

        #region "SetDefaultValuesAsync"

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task SetDefaultValuesAsync()
        {
            try
            {

                // Load UnitCode where access is granted
                await ClassGlobalFunctions.FillComboBoxUnitCodeHasAccessAsync(
                    displayMember: "UnitCode",
                    valueMember: "Id",
                    comboBox: CmbUnitCode,
                    tableName: "Companies",
                    userRowGuid: ClassGlobalVariables.pubLoginUserRowGuid,
                    fallbackUnitCode: ClassGlobalVariables.pubUnitCode);

                await ClassGlobalFunctions.FillComboBoxAsync(
                    displayMember: "ShiftCode",
                    valueMember: "Id",
                    comboBox: CmbShiftCode,
                    tableName: "Att_ShiftSchedules",
                    fallbackUnitCode: ClassGlobalVariables.pubUnitCode);

                FillCmbShiftCode(CmbShiftType);

                // Load ComboBox
                await FillUnitCodeAsync();

                // Load dropdown
                LoadShiftTypeDropdown();

                // Set time format to HH:MM (24-hour)
                DtPickShiftStartTime.Format = DateTimePickerFormat.Custom;
                DtPickShiftStartTime.CustomFormat = "HH:mm";
                DtPickShiftStartTime.ShowUpDown = true;
                DtPickShiftStartTime.Value = DateTime.Today.AddHours(6);

                DtPickShiftEndTime.Format = DateTimePickerFormat.Custom;
                DtPickShiftEndTime.CustomFormat = "HH:mm";
                DtPickShiftEndTime.ShowUpDown = true;
                DtPickShiftEndTime.Value = DateTime.Today.AddHours(14);

                // Set to show 00:15 for 15 minutes duration
                //NumShiftGraceEarlyMin.Format = DateTimePickerFormat.Custom;
                //NumShiftGraceEarlyMin.CustomFormat = "mm':'ss"; // Shows "15:00" for 15 minutes
                //NumShiftGraceEarlyMin.ShowUpDown = true;
                //NumShiftGraceEarlyMin.Value = DateTime.Today.AddMinutes(15); // Sets to 00:15

                DtPickShiftLunchStartTime.Format = DateTimePickerFormat.Custom;
                DtPickShiftLunchStartTime.CustomFormat = "HH:mm";
                DtPickShiftLunchStartTime.ShowUpDown = true;
                DtPickShiftLunchStartTime.Value = DateTime.Today.AddHours(11);

                DtPickShiftLunchEndTime.Format = DateTimePickerFormat.Custom;
                DtPickShiftLunchEndTime.CustomFormat = "HH:mm";
                DtPickShiftLunchEndTime.ShowUpDown = true;
                DtPickShiftLunchEndTime.Value = DateTime.Today.AddHours(12);

                // Read user defaults
                await ReadUserLoginDefaultXmlAsync();

                // add context menu for DataGridView
                InitDgvListContextMenu();

                // Load DataGridView
                await LoadDataGridViewAsync();

                // Load help text
                AddHelpText();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Set Default Values",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Consider adding more detailed error logging here
                // Logger.LogError(ex, "Error in SetDefaultValuesAsync");
            }
        }
        #endregion

        #region "TsBtnHelp_Click"
        private void TsBtnHelp_Click(object sender, EventArgs e)
        {
            try
            {
                SplitRight.Panel2Collapsed = !SplitRight.Panel2Collapsed;
                TabRightPanel.SelectedTab = tabPageTips;
                // Remove tabPagePref if it exists in TabRightPanel
                if (tabPagePref != null && TabRightPanel.TabPages.Contains(tabPagePref))
                {
                    TabRightPanel.TabPages.Remove(tabPagePref);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Help Click", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region "AddHelpText"
        private void AddHelpText()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("MANAGE SHIFT SCHEDULE GUIDE");
            sb.AppendLine();
            sb.AppendLine("Follow these steps to create a SHIFT SCHEDULE:");
            sb.AppendLine();

            // 1. BASIC DATA TAB
            sb.AppendLine("1. BASIC DATA TAB");
            sb.AppendLine("   - Select your Plant Code from the dropdown list");
            sb.AppendLine("   - To add a new Shift, click on the 'Add New' button");
            sb.AppendLine("   - Fill in all required shift details:");
            sb.AppendLine("     • Shift Code (unique identifier)");
            sb.AppendLine("     • Shift Description");
            sb.AppendLine("     • Start Time and End Time");
            sb.AppendLine("     • Break/Lunch duration if applicable");
            sb.AppendLine();

            // 2. SAVING THE SHIFT
            sb.AppendLine("2. SAVING THE SHIFT");
            sb.AppendLine("   - Click the [Save] button after completing all fields");
            sb.AppendLine("   - System will validate all entries for:");
            sb.AppendLine("     • Required fields completeness");
            sb.AppendLine("     • Time format validity");
            sb.AppendLine("     • Shift code uniqueness");
            sb.AppendLine("   - Upon successful validation, data will be saved to database");
            sb.AppendLine("   - Success confirmation message will be displayed");
            sb.AppendLine();

            // 3. UPDATE THE SHIFT
            sb.AppendLine("3. UPDATE EXISTING SHIFT");
            sb.AppendLine("   - Select the shift row from the left Data Grid");
            sb.AppendLine("   - Selected shift data will be populated in the Form view");
            sb.AppendLine("   - Make necessary changes to the shift details");
            sb.AppendLine("   - Click the [Save] button to update the record");
            sb.AppendLine("   - System will validate changes before updating");
            sb.AppendLine();

            // 4. SOFT DELETE SHIFT
            sb.AppendLine("4. DELETE SHIFT (Soft Delete)");
            sb.AppendLine("   - Select the shift row from the left Data Grid");
            sb.AppendLine("   - Data will be populated in the Form view for confirmation");
            sb.AppendLine("   - Click the [Delete] button");
            sb.AppendLine("   - Confirm deletion in the prompt dialog");
            sb.AppendLine("   - Shift will be marked as inactive (not physically deleted)");
            sb.AppendLine("   - Deleted shifts can be restored if needed");
            sb.AppendLine();

            // 5. LIST ALL SHIFTS
            sb.AppendLine("5. VIEW AND FILTER SHIFTS");
            sb.AppendLine("   - Select the 'List View' tab");
            sb.AppendLine("   - Set filter parameters if needed:");
            sb.AppendLine("     • Plant Code");
            sb.AppendLine("     • Shift Status (Active/Inactive)");
            sb.AppendLine("     • Date range");
            sb.AppendLine("   - Click the [Refresh List] button");
            sb.AppendLine("   - All matching shifts will be displayed in the grid");
            sb.AppendLine("   - Use column sorting by clicking column headers");
            sb.AppendLine();

            // 6. ADDITIONAL FEATURES
            sb.AppendLine("6. ADDITIONAL FEATURES");
            sb.AppendLine("   - Export shifts to Excel/CSV format");
            sb.AppendLine("   - Print shift schedule reports");
            sb.AppendLine("   - Copy existing shift as template for new shift");
            sb.AppendLine("   - Bulk operations for multiple shifts");
            sb.AppendLine();

            // 7. TROUBLESHOOTING
            sb.AppendLine("7. TROUBLESHOOTING");
            sb.AppendLine("   - If save fails: Check all required fields are completed");
            sb.AppendLine("   - If time validation fails: Ensure end time is after start time");
            sb.AppendLine("   - If duplicate error: Shift code must be unique per plant");
            sb.AppendLine("   - Contact system administrator for persistent issues");
            sb.AppendLine();

            sb.AppendLine("NOTE: All changes are logged for audit purposes.");

            // Assign the help text to mHelpText
            string mHelpText = sb.ToString();

            // Set properties of RichTextBox and assign the text
            txtHelp.Multiline = true;
            txtHelp.BorderStyle = BorderStyle.None;
            txtHelp.Enabled = true;
            txtHelp.ReadOnly = true;
            txtHelp.ScrollBars = (RichTextBoxScrollBars)ScrollBars.Both;
            txtHelp.BackColor = Color.White;
            txtHelp.ForeColor = Color.DarkBlue;
            txtHelp.Text = mHelpText;
        }
        #endregion

        #region "TsBtnFilterClose_Click"
        private void TsBtnFilterClose_Click(object sender, EventArgs e)
        {

            SplitRight.Panel2Collapsed = true;
        }
        #endregion

        #region "TsBtnOptTech_Click"
        private void TsBtnOptTech_Click(object sender, EventArgs e)
        {
            try
            {
                // Only SUPER and ADMIN can toggle/show the tech panel
                var allowedRoles = new[] { "SUPER", "ADMIN" };
                if (!allowedRoles.Contains(ClassGlobalVariables.pubUserRole, StringComparer.OrdinalIgnoreCase))
                    return;

                // Toggle right panel visibility
                SplitRight.Panel2Collapsed = !SplitRight.Panel2Collapsed;

                // Build the tech details object
                var mTechDetails = new ClassTechDetails
                {
                    // Set form identity
                    FormName = this.Name,
                    FormTitle = this.Text,

                    // Set default "created" date (VB had FormatDateTime("09-MAY-25", ShortDate))
                    // Here we parse explicitly as dd-MMM-yy using invariant culture.
                    FormCreated = DateTime.ParseExact("26-AUG-2025", "dd-MMM-yyyy", CultureInfo.InvariantCulture)
                };

                // Add table names used in this form (9 fields as in your VB)
                mTechDetails.TableNames.Add(new TableNames(
                    "dbo.Att_ShiftSchedules",
                    "dbo.usp_Att_ShiftSchedules_Upsert",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    ""
                ));

                // Bind to PropertyGrid
                PgridOptions.SelectedObject = mTechDetails;

                // Select default tab (index 1)
                // Check if tabPagePref is not found in TabRightPanel, then add it
                if (!TabRightPanel.TabPages.Contains(tabPagePref))
                {
                    TabRightPanel.TabPages.Add(tabPagePref);
                }
                TabRightPanel.SelectedTab = tabPagePref;
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid date format for FormCreated (expected dd-MMM-yy).",
                    "Date Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                // Optional: log or show
                MessageBox.Show("An unexpected error occurred: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region "TsMenuViewInActive_Click"
        private void TsMenuViewInActive_Click(object sender, EventArgs e)
        {
            isActiveRows = !TsMenuViewInActive.Checked;
        }
        #endregion

        #region "TsMenuViewDeleted_Click"
        private void TsMenuViewDeleted_Click(object sender, EventArgs e)
        {
            isDeletedRows = TsMenuViewDeleted.Checked;
        }
        #endregion

        #region "InitDgvListContextMenu"
        private void InitDgvListContextMenu()
        {
            _cmsDgvList = new ContextMenuStrip();

            var miExtendShift = new ToolStripMenuItem("Extend Shift to Plant") { Name = "cmsExtendShift" };
            var miIsActive = new ToolStripMenuItem("Make Shift In-Active") { Name = "cmsIsActive" };
            var miCancel = new ToolStripMenuItem("Cancel") { Name = "cmsCancel" };

            miExtendShift.Click += ExtendShiftToPlant_Click;  // attach a handler
            miCancel.Click += (s, e) => { /* no-op */ };

            _cmsDgvList.Items.Add(miExtendShift);
            _cmsDgvList.Items.Add(miIsActive);
            _cmsDgvList.Items.Add(new ToolStripSeparator());
            _cmsDgvList.Items.Add(miCancel);

            DgvList.ContextMenuStrip = _cmsDgvList;
        }
        #endregion


        #region "ChkIsActive_CheckedChanged"
        private void ChkIsActive_CheckedChanged(object sender, EventArgs e)
        {
            // Update the text first
            ChkIsActive.Text = ChkIsActive.Checked ? "Yes" : "No";

            if (!ChkIsActive.Checked)
            {
                // Show confirmation dialog when unchecking
                DialogResult result = MessageBox.Show(
                "This action will remove shift from list and can only be set to active by authorised authority. Are you sure?",
                "Confirm Deactivation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2 // Default to "No"
                );

                if (result == DialogResult.No)
                {
                    // Re-check the checkbox if user clicks No
                    ChkIsActive.Checked = true;
                    return; // Exit without updating text
                }
            }

            // Update the text after confirmation handling
            ChkIsActive.Text = ChkIsActive.Checked ? "Yes" : "No";
        }
        #endregion

        #region "TsBtRefreshDgv_Click"
        private async void TsBtRefreshDgv_Click(System.Object sender, System.EventArgs e)
        {
            // clear search box if coming from Clear button
            if (sender == TsBtnClearSearchDgv)
                TsTxtSearchDgv.Text = null;

            await LoadDataGridViewAsync();

        }
        #endregion

        #region "TsTxtSearchDgv_KeyDown"
        private void TsTxtSearchDgv_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)

                // Update DataGridView
                TsBtRefreshDgv_Click(null, null);
        }

        #endregion

        #region "TsBtnDrpBtn_DropDownItemClicked"
        private async void TsBtnDrpBtn_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                var dropDown = (ToolStripDropDownButton)sender;
                if (dropDown.DropDownItems.Count == 0) return;

                // Update check states for all menu items
                foreach (ToolStripItem item in dropDown.DropDownItems)
                {
                    if (item is ToolStripMenuItem menuItem)
                    {
                        menuItem.Checked = (item == e.ClickedItem);
                    }
                }

                // Update displayed text
                dropDown.Text = e.ClickedItem.Text;
                // Update Tag of the button to match the clicked item’s Tag
                dropDown.Tag = e.ClickedItem.Tag;

                // Load DataGridView
                await LoadDataGridViewAsync();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "TsBtnDrpBtn_DropDownItemClicked",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        #endregion

        #region "LoadDataGridViewAsync"
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task LoadDataGridViewAsync()
        {
            var startTime = DateTime.Now;  // Storing Start Time
            var progress = new FrmAdminProgress();

            try
            {
                // Initialize progress form
                progress.LblStatus.Text = "Updating List.";
                progress.LblMoreStatus.Text = "Please Wait...";
                progress.ProgressBar1.Visible = false;
                progress.Show();

                // Update status label
                TsLblDgvListFooter.Text = "Fetching Rows. Please wait...";
                TsLblDgvListFooter.ForeColor = Color.DarkBlue;
                Application.DoEvents();

                // Prepare DataGridView
                DgvList.Invoke((MethodInvoker)(() =>
                {
                    DgvList.DataSource = null;
                    DgvList.Rows.Clear();
                    DgvList.Columns.Clear();
                    DgvList.Refresh();
                    DgvList.Visible = false;
                    DgvList.ContextMenuStrip = _cmsDgvList;
                }));

                if (TsBtnDrpUnitCode == null ||
                    string.IsNullOrWhiteSpace(TsBtnDrpUnitCode.Text) ||
                    TsBtnDrpUnitCode.Text.Equals("(All)", StringComparison.OrdinalIgnoreCase) ||
                    TsBtnDrpUnitCode.Text.Equals("Plant Code", StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException("Please Select Plant Code from drop down.");
                }

                string shiftType = null;

                if (TsBtnDrpShiftType != null
                    && !string.IsNullOrWhiteSpace(TsBtnDrpShiftType.Text)
                    && !TsBtnDrpShiftType.Text.Equals("(All)", StringComparison.OrdinalIgnoreCase))
                {
                    shiftType = TsBtnDrpShiftType.Tag?.ToString();
                }

                string defaultUnitCode = TsBtnDrpUnitCode.Text;

                string connectionString = ClassGlobalFunctions.GetConnectionStringByUnitCode(
                        defaultUnitCode ?? ClassGlobalVariables.pubUnitCode);

                if (string.IsNullOrWhiteSpace(connectionString))
                    throw new InvalidOperationException("Connection string cannot be empty.");

                string searchText = string.IsNullOrWhiteSpace(TsTxtSearchDgv.Text)
                ? null
                : TsTxtSearchDgv.Text.Trim().ToLower();

                // Replace '*' with '%' for SQL LIKE query
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    searchText = searchText.Replace("*", "%");
                }
                else
                {
                    searchText = null; // normalize empty to null
                }

                var serachColumns = new List<string> {
                    "UnitCode",
                    "ShiftCode",
                    "ShiftDescription",
                    "ShiftType"};

                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@UnitCode", SqlDbType.NVarChar) { Value = defaultUnitCode });

                if (shiftType != null) 
                {
                    parameters.Add(new SqlParameter("@ShiftType", SqlDbType.Char) { Value = shiftType });
                }

                // Determine the row limit based on TsCmbLimitRows input:
                // If the text is "(all)", set limit to 0
                // Otherwise, try parsing to int; if fails, default to 50
                // ?. ensures Text is not null before calling Trim()(null - conditional operator).
                int mLimitRows =
                    TsCmbLimitRows.Text?.Trim().ToLower() == "(all)" ? 0 :
                    int.TryParse(TsCmbLimitRows.Text, out var value) ? value : 50;

                string orderBy = "UnitCode, ShiftCode, ShiftStartTime";
                string mTableName = "Att_ShiftSchedules";

                // Get data asynchronously
                DataTable resultTable = await ClassDbHelpers.GetRowsFromTableAsync(
                    unitCode: defaultUnitCode,  // plant code
                    tableName: mTableName, // sql table name
                    selectedColumns: null,   // selectedColumns
                    serachColumns: serachColumns,  // search sql table columns
                    additionalParameters: parameters, // sql parameters
                    searchText: searchText, // search text
                    orderByClause: orderBy,    // sql order by columns
                    isDeletedColumn: "IsDeleted", // table column name for IsDeleted
                    isDeletedValue: isDeletedRows, // isDeletedValue
                    isActiveColumn: "IsActive", // table column name for IsActive
                    isActiveValue: isActiveRows, // isActiveValue
                    limitRows: mLimitRows);

                // Check for empty results
                if (resultTable == null || resultTable.Rows.Count == 0)
                {
                    TsLblDgvListFooter.Text = "No rows found for given parameters.";
                    return;
                }

                // Create a filtered DataTable with only the columns we want
                DataTable filteredTable = new DataTable();
                var columnsToInclude = new List<string> {
                    "Id",
                    "UnitCode",
                    "ShiftCode",
                    "ShiftDescription",
                    "ShiftType",
                    "ShiftStartTime",
                    "ShiftEndTime",
                    "ShiftDurationHours"};

                // Add columns to filtered table
                foreach (string colName in columnsToInclude)
                {
                    if (resultTable.Columns.Contains(colName))
                    {
                        filteredTable.Columns.Add(colName, resultTable.Columns[colName].DataType);
                    }
                }

                // Copy data for selected columns
                foreach (DataRow row in resultTable.Rows)
                {
                    DataRow newRow = filteredTable.NewRow();
                    foreach (string colName in columnsToInclude)
                    {
                        if (resultTable.Columns.Contains(colName))
                        {
                            newRow[colName] = row[colName];
                        }
                    }
                    filteredTable.Rows.Add(newRow);
                }

                // Prepare DataGridView
                DgvList.Invoke((MethodInvoker)(() =>
                {
                    DgvList.DataSource = null;
                    DgvList.Rows.Clear();
                    DgvList.Columns.Clear();
                    DgvList.ReadOnly = false;
                    DgvList.Refresh();
                    DgvList.Visible = false;
                }));

                // Set data source
                DgvList.Invoke((MethodInvoker)(() => DgvList.DataSource = filteredTable));

                // Define and apply custom headers
                var customHeaders = new Dictionary<string, string>
                    {
                        {"Id", "Id"},
                        {"UnitCode", "Plant"},
                        {"ShiftCode", "Shift Code"},
                        {"ShiftDescription", "Shift Description"},
                        {"ShiftType", "Shift Type"},
                        {"ShiftStartTime", "Start Time"},
                        {"ShiftEndTime", "End Time"},
                        {"ShiftDurationHours", "Shift Duration Hrs"}
                    };

                DgvList.Invoke((MethodInvoker)(() =>
                {
                    foreach (DataGridViewColumn column in DgvList.Columns)
                    {
                        if (customHeaders.ContainsKey(column.Name))
                        {
                            column.HeaderText = customHeaders[column.Name];
                        }
                    }

                    // Configure DataGridView appearance
                    DgvList.ColumnHeadersVisible = true;
                    DgvList.AllowUserToAddRows = false;
                    DgvList.AllowUserToDeleteRows = false;
                    DgvList.ReadOnly = false;
                    DgvList.RowHeadersWidth = 60;
                    DgvList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    DgvList.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;

                    // Set column header style
                    var columnHeaderStyle = new DataGridViewCellStyle
                    {
                        BackColor = Color.Beige
                    };
                    DgvList.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

                    // Auto-size columns
                    DgvList.AutoResizeColumnHeadersHeight();
                    DgvList.AutoResizeColumns();

                    // Hide ID column
                    DgvList.Columns["Id"].Visible = false;
                    DgvList.Columns["Id"].ReadOnly = true;

                    // Add row numbers
                    for (int i = 0, mRowId = 1; i < DgvList.Rows.Count; i++, mRowId++)
                    {
                        DgvList.Rows[i].HeaderCell.Value = mRowId.ToString();
                    }
                }));

                // Populate the dropdown 
                AddColumnVisibilityDgvItems();

                // Calculate and display elapsed time
                var elapsedTime = DateTime.Now.Subtract(startTime);
                TsLblDgvListFooter.Text = $"Fetch Rows [ {resultTable.Rows.Count} ] In: {elapsedTime:hh\\:mm\\:ss}";
                TsLblDgvListFooter.ForeColor = Color.DarkBlue;
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                TsLblDgvListFooter.Text = ex.Message;
                TsLblDgvListFooter.ForeColor = Color.Red;
                MessageBox.Show(ex.Message, "Load DataGridView", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                DgvList.Invoke((MethodInvoker)(() =>
                {
                    DgvList.ResumeLayout();
                    DgvList.Visible = true;
                }));

                progress.Close();
                progress.Dispose();
            }
        }
        #endregion

        #region "AddColumnVisibilityDgvItems"
        /// <summary>
        /// call AddColumnVisibilityMenuItems() when you need to populate the dropdown
        /// </summary>
        private void AddColumnVisibilityDgvItems()
        {
            // Check if we need to invoke on the UI thread
            if (TsBtnDrpDgvCols.GetCurrentParent()?.InvokeRequired ?? false)
            {
                TsBtnDrpDgvCols.GetCurrentParent().Invoke((MethodInvoker)AddColumnVisibilityDgvItems);
                return;
            }

            // Clear existing items (if any)
            TsBtnDrpDgvCols.DropDownItems.Clear();

            // Add menu items for each column
            for (int i = 0; i < DgvList.Columns.Count; i++)
            {
                DataGridViewColumn column = DgvList.Columns[i];

                // Create menu item
                var menuItem = new ToolStripMenuItem
                {
                    Text = column.HeaderText,
                    Checked = column.Visible,
                    CheckOnClick = true
                };

                // Store column reference in Tag
                menuItem.Tag = column;

                // Handle click event
                menuItem.Click += (sender, e) =>
                {
                    var item = (ToolStripMenuItem)sender;
                    var col = (DataGridViewColumn)item.Tag;
                    col.Visible = item.Checked;
                };

                // Add to dropdown
                TsBtnDrpDgvCols.DropDownItems.Add(menuItem);
            }
        }
        #endregion

        #region "DgvList_CellClick"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void DgvList_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            try
            {
                if (DgvList.Rows.Count == 0)
                    return;

                DataGridViewSelectedRowCollection selectedRows = DgvList.SelectedRows;

                if (selectedRows.Count > 1)
                    throw new Exception("Please select only one row at a time");

                await DisplaySelectedRowDataAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DataGrid Click", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region "DisplaySelectedRowDataAsync"
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task DisplaySelectedRowDataAsync()
        {
            try
            {
                // Update status
                TsLblInputStatus.Text = "Displaying data. Please wait...";
                TsLblInputStatus.ForeColor = Color.DarkBlue;
                Application.DoEvents();

                // Check if there are rows
                if (DgvList.Rows.Count == 0)
                    return;

                // Validate single selection
                var selectedRows = DgvList.SelectedRows;
                if (selectedRows.Count > 1)
                {
                    throw new Exception("Please select only one row at a time");
                }

                // Clear existing values
                ClearInputs();

                // Get selected row ID
                int rowId = 0;
                foreach (DataGridViewRow selectedRow in selectedRows)
                {
                    if (selectedRow.Cells["Id"].Value != null && selectedRow.Cells["Id"].Value != DBNull.Value)
                    {
                        if (!int.TryParse(selectedRow.Cells["Id"].Value.ToString(), out rowId))
                        {
                            MessageBox.Show("Invalid Item selected", "Select Item");
                            return;
                        }
                        break;
                    }
                }

                TsTxtRecordId.Text = rowId.ToString();
                TsTxtRecordId.Tag = null;

                // Get data for selected row

                TsLblInputStatus.Text = "Fetching row. Please wait...";
                TsLblInputStatus.ForeColor = Color.DarkBlue;

                string mTableName = "dbo.Att_ShiftSchedules";

                string mUnitCode = (CmbUnitCode == null || string.IsNullOrWhiteSpace(CmbUnitCode.Text))
                   ? ClassGlobalVariables.pubUnitCode
                   : CmbUnitCode.Text;

                // Fetch from sql table
                DataTable dt = await ClassDbHelpers.GetSelectedRowDataAsync(
                    rowId: rowId,
                    tableName: mTableName,
                    unitCode: mUnitCode);

                if (dt == null || dt.Rows.Count == 0)
                {
                    TsLblInputStatus.Text = "...";
                    return;
                }

                // Populate controls with data
                DataRow row = dt.Rows[0];
                TsTxtRecordId.Tag = ClassSafeValueHelpers.PubGetSafeInteger(row["RowVersion"]);
                CmbUnitCode.Text = ClassSafeValueHelpers.PubGetSafeValue(row["UnitCode"]);
                CmbShiftCode.Text = ClassSafeValueHelpers.PubGetSafeValue(row["ShiftCode"]);
                TxtShiftDesc.Text = ClassSafeValueHelpers.PubGetSafeValue(row["ShiftDescription"]);

                string shiftType = ClassSafeValueHelpers.PubGetSafeValue(row["ShiftType"]);
                if (!string.IsNullOrWhiteSpace(shiftType))
                {
                    // Normalize the input
                    string code = shiftType.Trim();

                    // If value is "Day"/"Night", map to their codes
                    if (code.Equals("Day", StringComparison.OrdinalIgnoreCase))
                        code = "D";
                    else if (code.Equals("Night", StringComparison.OrdinalIgnoreCase))
                        code = "N";

                    // Set by SelectedValue (because ComboBox is data-bound)
                    CmbShiftType.SelectedValue = code;
                }
                else
                {
                    // Nothing from DB → clear selection
                    CmbShiftType.SelectedIndex = -1;
                }

                TimeSpan? safeShiftStartTime = ClassSafeValueHelpers.PubGetSafeTime(row["ShiftStartTime"]);
                if (safeShiftStartTime.HasValue)
                {
                    DtPickShiftStartTime.Value = DateTime.Today.Add(safeShiftStartTime.Value);
                    DtPickShiftStartTime.Checked = true;
                }
                else
                {
                    DtPickShiftStartTime.Checked = false;
                }

                TimeSpan? safeShiftEndTime = ClassSafeValueHelpers.PubGetSafeTime(row["ShiftEndTime"]);
                if (safeShiftEndTime.HasValue)
                {
                    DtPickShiftEndTime.Value = DateTime.Today.Add(safeShiftEndTime.Value);
                    DtPickShiftEndTime.Checked = true;
                }
                else
                {
                    DtPickShiftEndTime.Checked = false;
                }

                byte? safeShiftDurationHours = ClassSafeValueHelpers.PubGetSafeTinyInt(row["ShiftDurationHours"]);
                NumShiftTotalHrs.Value = safeShiftDurationHours ?? 0;

                byte? safeShiftGraceEarlyMin = ClassSafeValueHelpers.PubGetSafeTinyInt(row["ShiftGraceEarlyMin"]);
                NumShiftGraceEarlyMin.Value = safeShiftGraceEarlyMin ?? 0;

                byte? safeShiftGraceLateMin = ClassSafeValueHelpers.PubGetSafeTinyInt(row["ShiftGraceLateMin"]);
                NumShiftGraceLateMin.Value = safeShiftGraceLateMin ?? 0;

                TimeSpan? safeShiftLunchStartTime = ClassSafeValueHelpers.PubGetSafeTime(row["ShiftLunchStartTime"]);
                if (safeShiftLunchStartTime.HasValue)
                {
                    DtPickShiftLunchStartTime.Value = DateTime.Today.Add(safeShiftLunchStartTime.Value);
                    DtPickShiftLunchStartTime.Checked = true;
                }
                else
                {
                    DtPickShiftLunchStartTime.Checked = false;
                }

                TimeSpan? safeShiftLunchEndTime = ClassSafeValueHelpers.PubGetSafeTime(row["ShiftLunchEndTime"]);
                if (safeShiftLunchEndTime.HasValue)
                {
                    DtPickShiftLunchEndTime.Value = DateTime.Today.Add(safeShiftLunchEndTime.Value);
                    DtPickShiftLunchEndTime.Checked = true;
                }
                else
                {
                    DtPickShiftLunchEndTime.Checked = false;
                }

                byte? safeShiftLunchGraceEarlyMin = ClassSafeValueHelpers.PubGetSafeTinyInt(row["ShiftLunchGraceEarlyMin"]);
                NumShiftLunchGraceEarlyMin.Value = safeShiftLunchGraceEarlyMin ?? 0;

                byte? safeShiftLunchGraceLateMin = ClassSafeValueHelpers.PubGetSafeTinyInt(row["ShiftLunchGraceLateMin"]);
                NumShiftLunchGraceLateMin.Value = safeShiftLunchGraceLateMin ?? 0;

                byte? safeShiftLunchMinTime = ClassSafeValueHelpers.PubGetSafeTinyInt(row["ShiftLunchMinTime"]);
                NumShiftLunchMinTime.Value = safeShiftLunchMinTime ?? 0;

                byte? safeShiftLunchHourTime = ClassSafeValueHelpers.PubGetSafeTinyInt(row["ShiftLunchHourTime"]);
                NumShiftLunchHour.Value = safeShiftLunchHourTime ?? 0;

                byte? safeShiftMinimumOtMin = ClassSafeValueHelpers.PubGetSafeTinyInt(row["ShiftMinimumOtMin"]);
                NumShiftOtMinMinutes.Value = safeShiftMinimumOtMin ?? 0;

                byte? safeMonthlyShiftInLatePermitted = ClassSafeValueHelpers.PubGetSafeTinyInt(row["MonthlyShiftInLatePermitted"]);
                NumShiftInLatePermitted.Value = safeMonthlyShiftInLatePermitted ?? 0;

                byte? safeMonthlyLunchLatePermitted = ClassSafeValueHelpers.PubGetSafeTinyInt(row["MonthlyLunchLatePermitted"]);
                NumLunchLatePermitted.Value = safeMonthlyLunchLatePermitted ?? 0;

                bool? isActive = ClassSafeValueHelpers.PubGetSafeBoolean(row["IsActive"]);
                if (isActive.HasValue && isActive.Value)
                {
                    ChkIsActive.Checked = true;
                    ChkIsActive.Text = "Yes";
                }
                else
                {
                    ChkIsActive.Checked = false;
                    ChkIsActive.Text = "No";
                }

                bool? isDeleted = ClassSafeValueHelpers.PubGetSafeBoolean(row["IsDeleted"]);
                if (isDeleted.HasValue && isDeleted.Value)
                {
                    TsBtnSave.Enabled = false;
                    TsBtnDelete.Enabled = false;
                }
                else
                {
                    TsBtnSave.Enabled = true;
                    TsBtnDelete.Enabled = true;
                }

                TsLblInputStatus.Text = "...";

            }
            catch (Exception ex)
            {
                TsLblInputStatus.Text = ex.Message;
                TsLblInputStatus.ForeColor = Color.Red;
                MessageBox.Show(ex.Message, "Display Selected Row Data",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region "DgvList_CellFormatting"
        /// <summary>
        /// Uses an array of column names you care about.
        /// Checks if the current column is in that list.
        /// Formats both TimeSpan and DateTime values correctly.
        /// Safely ignores other columns.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // columns you want to format
            string[] timeColumns = { "ShiftStartTime", "ShiftEndTime", "ShiftLunchStartTime", "ShiftLunchEndTime" };

            // check if the current column is one of them
            if (e.Value != null && e.ColumnIndex >= 0)
            {
                string colName = DgvList.Columns[e.ColumnIndex].Name;

                if (timeColumns.Contains(colName))
                {
                    // Handle TimeSpan (SQL TIME type)
                    if (e.Value is TimeSpan ts)
                    {
                        e.Value = ts.ToString(@"hh\:mm");
                        e.FormattingApplied = true;
                    }
                    // Handle DateTime (if column comes as DateTime instead of TimeSpan)
                    else if (e.Value is DateTime dt)
                    {
                        e.Value = dt.ToString("HH:mm");
                        e.FormattingApplied = true;
                    }
                }
            }
        }

        #endregion

        #region "TsBtnAddNew_Click"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsBtnAddNew_Click(object sender, EventArgs e)
        {
            ClearInputs();

            CmbUnitCode.Focus();

            TsLblInputFooter.Text = "Add all required information and click on Save.";
            Application.DoEvents();
        }

        #endregion

        #region "TsBtnSave_Click"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TsBtnSave_Click(System.Object sender, System.EventArgs e)
        {
            var startTime = DateTime.Now;  // Storing Start Time
            var progress = new FrmAdminProgress();

            try
            {

                // Call the ValidInput function and await its result
                bool isValidInput = await ValidateInputAsync();
                if (!isValidInput)
                    return;

                var sb = new StringBuilder("Are you sure you want to ");

                string isRowInsertUpdate = "INSERTED";

                if (string.IsNullOrWhiteSpace(TsTxtRecordId.Text))
                {
                    isRowInsertUpdate = "INSERTED";

                    sb.Append("INSERT a new shift")
                      .AppendLine()
                      .Append($"Shift Code: {CmbShiftCode.Text}, Type: {CmbShiftType.Text}");
                }
                else
                {
                    isRowInsertUpdate = "UPDATED";

                    sb.Append("UPDATE the existing shift")
                      .AppendLine()
                      .Append($"Shift Code: {CmbShiftCode.Text}, Type: {CmbShiftType.Text}");
                }

                var message = sb.ToString();

                var msgResponse = MessageBox.Show(message, "Confirm Save",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (msgResponse == DialogResult.No)
                    return;

                // Initialize progress form
                progress.LblStatus.Text = "Saving Data.";
                progress.LblMoreStatus.Text = "Please Wait...";
                progress.ProgressBar1.Visible = false;
                progress.Show();

                // Update status label
                TsLblInputStatus.Text = "Saving Data. Please wait...";
                TsLblInputStatus.ForeColor = Color.DarkBlue;
                Application.DoEvents();

                if (await UpsertRowsAsync())
                {
                    // Insert / Update was successful
                    MessageBox.Show($"Row {isRowInsertUpdate} successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearInputs();

                    await LoadDataGridViewAsync();

                    // Calculate and display elapsed time
                    var elapsedTime = DateTime.Now.Subtract(startTime);
                    TsLblInputStatus.Text = $"Done In: {elapsedTime:hh\\:mm\\:ss}";
                    TsLblInputStatus.ForeColor = Color.DarkBlue;
                    Application.DoEvents();

                }
                else
                    // Update failed
                    MessageBox.Show("Failed to update row.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                progress.Close();
                progress.Dispose();
            }
        }

        #endregion

        #region "UpsertRowsAsync"
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task<bool> UpsertRowsAsync()
        {
            try
            {
                UseWaitCursor = true;

                const string tableName = "dbo.Att_ShiftSchedules";
                const string sqlProcedureName = "dbo.usp_Att_ShiftSchedules_Upsert";

                string defaultUnitCode = CmbUnitCode.Text;

                string connectionString = ClassGlobalFunctions.GetConnectionStringByUnitCode(defaultUnitCode);
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new InvalidOperationException("Connection string cannot be empty");
                }

                var parameters = new List<SqlParameter>
                {
                    ClassDbHelpers.CreateSqlParameter("@UnitCode", SqlDbType.VarChar, myDataTable.UnitCode),
                    ClassDbHelpers.CreateSqlParameter("@ShiftCode", SqlDbType.NVarChar, myDataTable.ShiftCode),
                    ClassDbHelpers.CreateSqlParameter("@ShiftDescription", SqlDbType.NVarChar, myDataTable.ShiftDescription),
                    ClassDbHelpers.CreateSqlParameter("@ShiftType", SqlDbType.NVarChar, myDataTable.ShiftType),
                    ClassDbHelpers.CreateSqlParameter("@ShiftStartTime", SqlDbType.Time, myDataTable.ShiftStartTime),
                    ClassDbHelpers.CreateSqlParameter("@ShiftEndTime", SqlDbType.Time, myDataTable.ShiftEndTime),
                    ClassDbHelpers.CreateSqlParameter("@ShiftDurationHours", SqlDbType.TinyInt, myDataTable.ShiftDurationHours),
                    ClassDbHelpers.CreateSqlParameter("@ShiftGraceEarlyMin", SqlDbType.TinyInt, myDataTable.ShiftGraceEarlyMin),
                    ClassDbHelpers.CreateSqlParameter("@ShiftGraceLateMin", SqlDbType.TinyInt, myDataTable.ShiftGraceLateMin),

                    ClassDbHelpers.CreateSqlParameter("@ShiftLunchStartTime", SqlDbType.Time, myDataTable.ShiftLunchStartTime),
                    ClassDbHelpers.CreateSqlParameter("@ShiftLunchEndTime", SqlDbType.Time, myDataTable.ShiftLunchEndTime),
                    ClassDbHelpers.CreateSqlParameter("@ShiftLunchGraceEarlyMin", SqlDbType.TinyInt, myDataTable.ShiftLunchGraceEarlyMin),
                    ClassDbHelpers.CreateSqlParameter("@ShiftLunchGraceLateMin", SqlDbType.TinyInt, myDataTable.ShiftLunchGraceLateMin),
                    ClassDbHelpers.CreateSqlParameter("@ShiftLunchMinTime", SqlDbType.TinyInt, myDataTable.ShiftLunchMinTime),
                    ClassDbHelpers.CreateSqlParameter("@ShiftLunchHourTime", SqlDbType.TinyInt, myDataTable.ShiftLunchHourTime),

                    ClassDbHelpers.CreateSqlParameter("@ShiftMinimumOtMin", SqlDbType.TinyInt, myDataTable.ShiftMinimumOtMin),
                    ClassDbHelpers.CreateSqlParameter("@MonthlyShiftInLatePermitted", SqlDbType.TinyInt, myDataTable.MonthlyShiftInLatePermitted),
                    ClassDbHelpers.CreateSqlParameter("@MonthlyLunchLatePermitted", SqlDbType.TinyInt, myDataTable.MonthlyLunchLatePermitted),

                    ClassDbHelpers.CreateSqlParameter("@IsActive", SqlDbType.Bit, myDataTable.IsActive),
                    ClassDbHelpers.CreateSqlParameter("@RowVersion", SqlDbType.Int, myDataTable.RowVersion),
                    ClassDbHelpers.CreateSqlParameter("@UserId", SqlDbType.Int, myDataTable.UserId),
                    ClassDbHelpers.CreateSqlParameter("@UserRowGuid", SqlDbType.VarChar, myDataTable.UserRowGuid.ToString()),
                    ClassDbHelpers.CreateSqlParameter("@IpAddsCreated", SqlDbType.VarChar, myDataTable.IpAddsCreated),
                    ClassDbHelpers.CreateSqlParameter("@HostName", SqlDbType.VarChar, myDataTable.HostName),
                    ClassDbHelpers.CreateSqlParameter("@RowId", SqlDbType.Int, myDataTable.Id)
                };

                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync().ConfigureAwait(false);

                    using (var command = new SqlCommand(sqlProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddRange(parameters.ToArray());

                        // Add output parameter for success status
                        var successParam = new SqlParameter("@Success", SqlDbType.Bit)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(successParam);

                        // Execute the command
                        await command.ExecuteNonQueryAsync().ConfigureAwait(false);

                        bool success = Convert.ToBoolean(successParam.Value);

                        UpdateStatusLabel("Done...");
                        return success;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                string errorMessage = $"Database error {sqlEx.Number}: {sqlEx.Message}";
                UpdateStatusLabel($"Database error: {sqlEx.Number}");
                ShowErrorMessage(errorMessage, "Database error");
                return false;
            }
            catch (Exception ex)
            {
                UpdateStatusLabel(ex.Message);
                ShowErrorMessage(ex.Message, "Upsert Rows");
                return false;
            }
            finally
            {
                UseWaitCursor = false;
            }
        }
        #endregion


        #region "ExtendShiftToPlant_Click"
        /// <summary>
        /// Define the handler as async void
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ExtendShiftToPlant_Click(object sender, EventArgs e)
        {
            try
            {
                var row = GetSelectedRowOrNull();
                if (row == null) { MessageBox.Show("Please select a shift row."); return; }

                string defaultUnitCode = CmbUnitCode.Text;

                string connectionString = ClassGlobalFunctions.GetConnectionStringByUnitCode(defaultUnitCode);
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new InvalidOperationException("Connection string cannot be empty");
                }

                int? currentRowId = null;
                if (row.Cells["Id"].Value != null && row.Cells["Id"].Value != DBNull.Value)
                {
                    if (int.TryParse(row.Cells["Id"].Value.ToString(), out int parsedId))
                    {
                        currentRowId = parsedId;
                    }
                    else
                    {
                        currentRowId = null;
                        throw new Exception("Invalid row selected.");
                    }
                }
                var currentUnitCode = Convert.ToString(row.Cells["UnitCode"].Value);

                // Prompt for UnitCode
                using (var dlg = new UnitPickerForm(
                    connectionString,
                    currentUnitCode,
                    ClassGlobalVariables.pubLoginUserRowGuid))
                {
                    if (dlg.ShowDialog(this) != DialogResult.OK) return;
                    var targetUnitCode = dlg.SelectedUnitCode;
                    if (string.IsNullOrWhiteSpace(targetUnitCode))
                    {
                        MessageBox.Show("No UnitCode selected."); return;
                    }

                    bool success = await InsertExtendedUnitCodeAsync(currentRowId.Value, targetUnitCode);
                    if (success)
                    {
                        ClearInputs();
                        await LoadDataGridViewAsync();
                    }
                    else
                    {
                        MessageBox.Show("No rows extended. Please try again.", "Extend Unit Code", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ExtendShiftToPlant_Click", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private DataGridViewRow GetSelectedRowOrNull()
        {
            return DgvList.SelectedRows.Count > 0 ? DgvList.SelectedRows[0] :
                   (DgvList.CurrentRow != null ? DgvList.CurrentRow : null);
        }
        #endregion

        #region "InsertExtendedUnitCodeAsync"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowId"></param>
        /// <param name="extendedUnitCode"></param>
        /// <returns></returns>
        private async Task<bool> InsertExtendedUnitCodeAsync(int rowId, string extendedUnitCode)
        {
            try
            {
                UseWaitCursor = true;

                const string tableName = "dbo.Att_ShiftSchedules";
                const string sqlProcedureName = "dbo.usp_Att_ShiftSchedules_ToExtendedUnitCode";

                string defaultUnitCode = CmbUnitCode.Text;

                string connectionString = ClassGlobalFunctions.GetConnectionStringByUnitCode(defaultUnitCode);
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new InvalidOperationException("Connection string cannot be empty");
                }

                var parameters = new List<SqlParameter>
                {
                    ClassDbHelpers.CreateSqlParameter("@UnitCode", SqlDbType.VarChar, extendedUnitCode),

                    ClassDbHelpers.CreateSqlParameter("@UserId", SqlDbType.Int, ClassGlobalVariables.pubLoginUserRowId),
                    ClassDbHelpers.CreateSqlParameter("@UserRowGuid", SqlDbType.NVarChar, ClassGlobalVariables.pubLoginUserRowGuid),
                    ClassDbHelpers.CreateSqlParameter("@IpAddsCreated", SqlDbType.VarChar, ClassGlobalVariables.pubHostIPAddress),
                    ClassDbHelpers.CreateSqlParameter("@HostName", SqlDbType.NVarChar, ClassGlobalVariables.pubDNSHostName),
                    ClassDbHelpers.CreateSqlParameter("@RowId", SqlDbType.Int, rowId)
                };

                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync().ConfigureAwait(false);

                    using (var command = new SqlCommand(sqlProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddRange(parameters.ToArray());

                        // Add output parameter for success status
                        var successParam = new SqlParameter("@Success", SqlDbType.Bit)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(successParam);

                        // Execute the command
                        await command.ExecuteNonQueryAsync().ConfigureAwait(false);

                        bool success = Convert.ToBoolean(successParam.Value);

                        UpdateStatusLabel("Done...");
                        return success;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                string errorMessage = $"Database error {sqlEx.Number}: {sqlEx.Message}";
                UpdateStatusLabel($"Database error: {sqlEx.Number}");
                ShowErrorMessage(errorMessage, "Database error");
                return false;
            }
            catch (Exception ex)
            {
                UpdateStatusLabel(ex.Message);
                ShowErrorMessage(ex.Message, "Insert Rows");
                return false;
            }
            finally
            {
                UseWaitCursor = false;
            }
        }
        #endregion

        #region "UpdateStatusLabel"
        /// <summary>
        /// ToolStrip items are thread-safe for Text updates
        /// </summary>
        /// <param name="message"></param>
        private void UpdateStatusLabel(string message)
        {
            // Get the ToolStrip that contains our status label
            ToolStrip parentStrip = TsLblInputStatus.GetCurrentParent();

            // If we're on a background thread and have a parent to invoke on
            if (parentStrip != null && parentStrip.InvokeRequired)
            {
                parentStrip.Invoke((MethodInvoker)(() =>
                {
                    TsLblInputStatus.Text = message;
                    parentStrip.Refresh();
                }));
            }
            else
            {
                // We're on the UI thread
                TsLblInputStatus.Text = message;
                parentStrip?.Refresh();
            }
        }
        #endregion

        #region "TsBtnDelete_Click"
        private async void TsBtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // get the valid selected RowId
                int? rowId = null;
                if (!string.IsNullOrWhiteSpace(TsTxtRecordId.Text))
                {
                    if (!int.TryParse(TsTxtRecordId.Text.Trim(), out int parsedId))
                    {
                        string errorMessage = "Invalid Contractor selected.";
                        errorProvider1.SetError(CmbShiftCode, errorMessage);
                        throw new Exception(errorMessage);
                    }
                    rowId = parsedId; // Assign the parsed value to the outer variable
                }

                // 1. Get selected rows with valid "Id" column
                List<int> idsToDelete = new List<int>();

                idsToDelete.Add(rowId.Value); // Add ID to the list

                // If no valid IDs found, show a message
                if (idsToDelete.Count == 0)
                {
                    MessageBox.Show("No valid rows selected for deletion.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var invalidOptions = new[] { "(All)", "Plant Code" };
                if (TsBtnDrpUnitCode == null ||
                    string.IsNullOrWhiteSpace(TsBtnDrpUnitCode.Text) ||
                    invalidOptions.Contains(TsBtnDrpUnitCode.Text, StringComparer.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException("Please Select Plant Code from drop down.");
                }
                string defaultUnitCode = TsBtnDrpUnitCode.Text;

                // Ask for confirmation before deletion
                var confirmResult = MessageBox.Show(
                    $"Soft Delete {idsToDelete.Count} selected row(s)?",
                    "Confirm Deletion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmResult != DialogResult.Yes)
                    return;

                if (confirmResult == DialogResult.Yes)
                {
                    bool success = await DeleteSelectedRowsAsync(
                        idsToDelete,
                        defaultUnitCode);

                    if (success)
                    {
                        ClearInputs();
                        await LoadDataGridViewAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                TsLblInputStatus.Text = ex.Message;
                MessageBox.Show(ex.Message, "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }
        #endregion

        #region "DeleteSelectedRowsAsync"
        private async Task<bool> DeleteSelectedRowsAsync(
            List<int> idsToDelete,
            string unitCode = null)
        {
            // 1. Call database helper
            bool success = await ClassDbHelpers.DeleteRowsAsync(
                tableName: "Att_ShiftSchedules",
                idColumnName: "Id",
                ids: idsToDelete,
                softDelete: true,
                deletedBy: ClassGlobalVariables.pubLoginUserRowGuid,
                unitCode: unitCode
            );

            // 2. Show result message
            if (success)
            {
                MessageBox.Show(
                    "Deletion successful!",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                await LoadDataGridViewAsync(); // Refresh grid
            }
            else
            {
                MessageBox.Show(
                    "Deletion failed. Check logs for details.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

            return success;
        }
        #endregion

        #region "ValidateInputAsync"

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task<bool> ValidateInputAsync()
        {
            try
            {
                TsLblInputFooter.ForeColor = Color.Red;
                TsLblInputFooter.Text = "Validating data. Please wait...";

                await Task.Delay(TimeSpan.FromSeconds(0.5)); // 0.5 seconds

                // Remove existing errors
                ClassLayoutHelper.ClearErrorsTableLayout(TableLayout1, errorProvider1);

                // Plant code
                if (string.Equals(TsBtnDrpUnitCode.Text, "(All)", StringComparison.OrdinalIgnoreCase))
                {
                    string errorMessage = "Please select Plant Code from drop down.";
                    errorProvider1.SetError(CmbUnitCode, errorMessage);
                    throw new Exception(errorMessage);
                }

                // Required control validations
                ClassValidationHelper.ValidateControl(CmbUnitCode, "Plant Code is required.", errorProvider1, TsLblInputFooter);
                ClassValidationHelper.ValidateControl(CmbShiftCode, "Shift Code is required.", errorProvider1, TsLblInputFooter);
                ClassValidationHelper.ValidateControl(TxtShiftDesc, "Shift Description is required.", errorProvider1, TsLblInputFooter);
                ClassValidationHelper.ValidateControl(CmbShiftType, "Shift Type is required.", errorProvider1, TsLblInputFooter);

                // Check if the checkbox is unchecked(i.e., date is not provided)
                var requiredDateTimePickers = new[]
                {
                    (DtPickShiftStartTime, "Shift start time"),
                    (DtPickShiftEndTime, "Shift end time"),
                    (DtPickShiftLunchStartTime, "Shift lunch start time"),
                    (DtPickShiftLunchEndTime, "Shift lunch end time")
                };

                foreach (var (picker, fieldName) in requiredDateTimePickers)
                {
                    if (!picker.Checked)
                    {
                        throw new ArgumentException($"{fieldName} is required.");
                    }
                }

                if (!DtPickShiftStartTime.Checked)
                {
                    throw new ArgumentException("Shift start time is required.");
                }

                // validate NumericUpDown control
                var requiredNum = new[]
                {
                    (NumShiftGraceEarlyMin, "Shift grace early time"),
                    (NumShiftGraceLateMin, "Shift grace late time"),
                    (NumShiftLunchGraceEarlyMin, "Shift lunch grace early time"),
                    (NumShiftLunchGraceLateMin, "Shift lunch grace late time"),
                    (NumShiftLunchMinTime, "Shift lunch minimum gap time"),
                    (NumShiftLunchHour, "Shift total lunch hour time"),
                    (NumShiftOtMinMinutes, "Minimum Hours to Qualify for OT"),
                    (NumShiftInLatePermitted, "Shift IN late permission time"),
                    (NumLunchLatePermitted, "Shift lunch late permission time")
                };

                foreach (var (numUpDwn, fieldName) in requiredNum)
                {
                    if (numUpDwn.Value <= 0)
                    {
                        throw new ArgumentException($"{fieldName} is required.");
                    }
                }

                // get the valid selected RowId
                int? rowId = null;
                if (!string.IsNullOrWhiteSpace(TsTxtRecordId.Text))
                {
                    if (!int.TryParse(TsTxtRecordId.Text.Trim(), out int parsedId))
                    {
                        string errorMessage = "Invalid Shift selected.";
                        errorProvider1.SetError(CmbShiftCode, errorMessage);
                        throw new Exception(errorMessage);
                    }
                    rowId = parsedId; // Assign the parsed value to the outer variable
                }

                // get the valid selected RowId
                int? rowVersion = null;
                string tagValue = TsTxtRecordId.Tag as string;   // safe cast to string

                if (!string.IsNullOrWhiteSpace(tagValue))
                {
                    int parsedId;
                    if (!int.TryParse(tagValue, out parsedId))
                    {
                        string errorMessage = "Invalid Contractor selected.";
                        errorProvider1.SetError(CmbShiftCode, errorMessage);
                        throw new Exception(errorMessage);
                    }

                    rowVersion = parsedId; // Assign the parsed value
                }

                // Length validations
                ClassValidationHelper.ValidateTextBoxLength(TxtShiftDesc, 100, "Shift Description", errorProvider1, TsLblInputFooter);

                // Dropdown value validations
                int? selectedUnitId = ClassValidationHelper.ValidateCmbSelectedValue(CmbUnitCode, "Plant Code is required.", errorProvider1, TsLblInputFooter);

                bool? isActive = ChkIsActive.Checked;

                // Populate data table
                myDataTable = new AttShiftSchedule();

                // Id
                myDataTable.Id = rowId.HasValue ? rowId.Value : (int?)null;
                myDataTable.RowVersion = rowVersion.HasValue ? rowVersion.Value : (int?)null;

                // UnitCode
                myDataTable.UnitCode = !string.IsNullOrEmpty(CmbUnitCode.Text)
                    ? CmbUnitCode.Text
                    : null;

                // ShiftCode
                myDataTable.ShiftCode = !string.IsNullOrEmpty(CmbShiftCode.Text)
                    ? ClassStringHelpers.CleanAndUpperCase(CmbShiftCode.Text)
                    : null;

                // ShiftDescription
                myDataTable.ShiftDescription = !string.IsNullOrEmpty(TxtShiftDesc.Text)
                    ? ClassStringHelpers.CleanAndUpperCase(TxtShiftDesc.Text)
                    : null;

                // ShiftType
                myDataTable.ShiftType = CmbShiftType.SelectedValue != null
                    ? CmbShiftType.SelectedValue.ToString()
                    : null;

                // ShiftStartTime
                myDataTable.ShiftStartTime = DtPickShiftStartTime.Checked
                    ? DtPickShiftStartTime.Value.TimeOfDay
                    : (TimeSpan?)null;

                // ShiftEndTime
                myDataTable.ShiftEndTime = DtPickShiftEndTime.Checked
                    ? DtPickShiftEndTime.Value.TimeOfDay
                    : (TimeSpan?)null;

                // ShiftGraceEarlyMin
                myDataTable.ShiftGraceEarlyMin = NumShiftGraceEarlyMin.Value > 0
                    ? (byte?)NumShiftGraceEarlyMin.Value
                    : (byte?)null;

                // ShiftGraceLateMin
                myDataTable.ShiftGraceLateMin = NumShiftGraceLateMin.Value > 0
                    ? (byte?)NumShiftGraceEarlyMin.Value
                    : (byte?)null;

                // ShiftLunchStartTime
                myDataTable.ShiftLunchStartTime = DtPickShiftLunchStartTime.Checked
                    ? DtPickShiftLunchStartTime.Value.TimeOfDay
                    : (TimeSpan?)null;

                // ShiftLunchEndTime
                myDataTable.ShiftLunchEndTime = DtPickShiftLunchEndTime.Checked
                    ? DtPickShiftLunchEndTime.Value.TimeOfDay
                    : (TimeSpan?)null;

                // ShiftLunchGraceEarlyMin
                myDataTable.ShiftLunchGraceEarlyMin = NumShiftLunchGraceEarlyMin.Value > 0
                    ? (byte)NumShiftLunchGraceEarlyMin.Value
                    : (byte?)null;

                // ShiftLunchGraceLateMin
                myDataTable.ShiftLunchGraceLateMin = NumShiftLunchGraceLateMin.Value > 0
                    ? (byte)NumShiftLunchGraceLateMin.Value
                    : (byte?)null;

                // ShiftLunchMinTime
                myDataTable.ShiftLunchMinTime = NumShiftLunchMinTime.Value > 0
                    ? (byte)NumShiftLunchMinTime.Value
                    : (byte?)null;

                // ShiftLunchHourTime
                myDataTable.ShiftLunchHourTime = NumShiftLunchHour.Value > 0
                    ? (byte)NumShiftLunchHour.Value
                    : (byte?)null;

                // ShiftDurationHours
                myDataTable.ShiftDurationHours = NumShiftTotalHrs.Value > 0
                    ? (byte)NumShiftTotalHrs.Value
                    : (byte?)null;

                // MonthlyShiftInLatePermitted
                myDataTable.ShiftMinimumOtMin = NumShiftOtMinMinutes.Value > 0
                    ? (byte)NumShiftOtMinMinutes.Value
                    : (byte?)null;

                // MonthlyShiftInLatePermitted
                myDataTable.MonthlyShiftInLatePermitted = NumShiftInLatePermitted.Value > 0
                    ? (byte)NumShiftInLatePermitted.Value
                    : (byte?)null;

                // MonthlyLunchLatePermitted
                myDataTable.MonthlyLunchLatePermitted = NumLunchLatePermitted.Value > 0
                    ? (byte)NumLunchLatePermitted.Value
                    : (byte?)null;

                // IsActive
                myDataTable.IsActive = isActive.Value;

                // UserId
                myDataTable.UserId = ClassGlobalVariables.pubLoginUserRowId;

                // UserCreatedRowGuid
                myDataTable.UserRowGuid = ClassGlobalVariables.pubLoginUserRowGuid;

                // IPAddress
                myDataTable.IpAddsCreated = ClassGlobalVariables.pubHostIPAddress;

                // HostName
                myDataTable.HostName = ClassGlobalVariables.pubDNSHostName;

                TsLblInputFooter.ForeColor = Color.DarkBlue;
                TsLblInputFooter.Text = "Done...";

                return true;

            }
            catch (Exception ex)
            {
                TsLblInputFooter.Text = ex.Message;
                MessageBox.Show(ex.Message, "Validate Input", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        #endregion

        #region "ShowErrorMessage"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        private void ShowErrorMessage(string message, string title)
        {
            // For MessageBox, we still need to check for cross-thread calls
            if (TsLblInputStatus.GetCurrentParent()?.InvokeRequired ?? false)
            {
                TsLblInputStatus.GetCurrentParent().Invoke((MethodInvoker)(() =>
                    MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information)));
            }
            else
            {
                MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region "ShowMessage"
        /// <summary>
        /// Helper method for consistent message display
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="icon"></param>
        private void ShowMessage(string message, string title, MessageBoxIcon icon)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)(() =>
                    MessageBox.Show(message, title, MessageBoxButtons.OK, icon)));
            }
            else
            {
                MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
            }
        }
        #endregion

        #region "SelectNumericUpDownText"

        /// <summary>
        /// NumShiftTotalHrs.Enter += (s, e) => SelectNumericUpDownText(NumShiftTotalHrs);
        /// </summary>
        /// <param name="num"></param>
        private void SelectNumericUpDownText(NumericUpDown num)
        {
            if (num.Controls.Count > 0 && num.Controls[1] is TextBox tb)
            {
                tb.SelectAll();
            }
        }
        #endregion

        #region "ClearInputs"
        /// <summary>
        /// 
        /// </summary>
        private void ClearInputs()
        {
            try
            {

                // Remove Set Errors
                ClassLayoutHelper.ClearErrorsTableLayout(TableLayout1, errorProvider1);
                ClassLayoutHelper.ClearControlsTableLayout(TableLayout1);

                TsTxtRecordId.Text = string.Empty;
                TsTxtRecordId.Tag = string.Empty;
                TsLblInputStatus.Text = "...";

                // set focus
                CmbUnitCode.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ClearInputs",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        #endregion

        #region "ReadUserLoginDefaultXmlAsync"
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task ReadUserLoginDefaultXmlAsync()
        {
            try
            {
                string mUnitCode = ClassGlobalVariables.pubUnitCode;

                var wanted = new[] { "PlantCode", "EmployeeCategory" };

                var vals = await UserLoginDefaultsHelper.GetAsync(
                    unitCode: mUnitCode,
                    userRowGuid: ClassGlobalVariables.pubLoginUserRowGuid,
                    wantedColumns: wanted,
                    tableName: "dbo.UserLoginDefaults");

                string plantCode = vals["PlantCode"];

                // Assign only if PlantCode is not null or empty
                if (!string.IsNullOrEmpty(plantCode))
                {
                    CmbUnitCode.Text = plantCode;
                }

                // Example: set dropdown selection if it exists
                var selectedItem = TsBtnDrpUnitCode.DropDownItems
                    .OfType<ToolStripMenuItem>()
                    .FirstOrDefault(item => string.Equals(item.Text, plantCode, StringComparison.OrdinalIgnoreCase));

                if (selectedItem != null)
                {
                    TsBtnDrpUnitCode.Text = selectedItem.Text;
                    TsBtnDrpUnitCode.Tag = selectedItem.Tag; // keep Text and Tag in sync
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Message: {ex.Message}\n\nStack Trace: {ex.StackTrace}",
                          "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
        #endregion

        #region "FillCmbShiftCode"

        private void FillCmbShiftCode(ComboBox myComboBox)
        {
            // Create a small list of shift options
            var shifts = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("D", "Day"),
                new KeyValuePair<string, string>("N", "Night")
            };

            // Bind to ComboBox
            myComboBox.DataSource = shifts;
            myComboBox.DisplayMember = "Value";  // what user sees
            myComboBox.ValueMember = "Key";      // internal code
        }
        #endregion

        #region "FillUnitCodeAsync"
        /// <summary>
        /// Used verbatim string ($@) for cleaner SQL query formatting
        /// </summary>
        /// <returns></returns>
        private async Task FillUnitCodeAsync(string unitCode = null)
        {
            try
            {
                const string tableName = "dbo.Companies";

                // Clear existing items
                TsBtnDrpUnitCode.DropDownItems.Clear();
                // TsBtnDrpUnitCode.DropDownItems.Add("(All)").Tag = 0;

                // Resolve UnitCode once and use everywhere
                var resolvedUnitCode = unitCode ?? ClassGlobalVariables.pubUnitCode;

                // Get connection string
                string connectionString = ClassGlobalFunctions.GetConnectionStringByUnitCode(resolvedUnitCode);
                if (string.IsNullOrWhiteSpace(connectionString))
                    throw new InvalidOperationException("Connection string cannot be empty.");


                // Load categories from database
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    string query = $@"
                        SELECT u.UnitCode, u.Id 
                        FROM {tableName} AS u
                        WHERE u.IsActive = 1 AND u.IsDeleted = 0
                        AND u.[UnitCode] IN (
		                        SELECT UnitCode
		                        FROM dbo.AdminUserUnitAccess
		                        WHERE UserRowGuid = @UserRowGuid
		                        )
                        ORDER BY u.UnitCode";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UnitCode", resolvedUnitCode);
                        command.Parameters.Add("@UserRowGuid", SqlDbType.NVarChar, 150).Value = ClassGlobalVariables.pubLoginUserRowGuid;

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                string categoryName = reader["UnitCode"] as string ?? "Unnamed Unit";
                                int categoryId = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : -1;

                                var menuItem = new ToolStripMenuItem(categoryName)
                                {
                                    Tag = categoryId,
                                    CheckOnClick = true
                                };

                                // When clicked, set the button's Text and Tag to this item's values
                                menuItem.Click += (s, e) =>
                                {
                                    // uncheck others (single-select behavior)
                                    foreach (ToolStripItem it in TsBtnDrpUnitCode.DropDownItems)
                                        if (it is ToolStripMenuItem mi) mi.Checked = false;

                                    menuItem.Checked = true;

                                    TsBtnDrpUnitCode.Text = menuItem.Text;
                                    TsBtnDrpUnitCode.Tag = categoryId;
                                };

                                TsBtnDrpUnitCode.DropDownItems.Add(menuItem);
                            }
                        }
                    }
                }

                TsBtnDrpUnitCode.Text = ClassGlobalVariables.pubUnitCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fill Plant Code",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        #endregion

        #region "LoadShiftTypeDropdown"
        private void LoadShiftTypeDropdown()
        {
            TsBtnDrpShiftType.DropDownItems.Clear();

            // Add (All)
            var itemAll = new ToolStripMenuItem("(All)") { Tag = "A" };
            itemAll.Click += TsBtnDrpShiftType_Click;
            TsBtnDrpShiftType.DropDownItems.Add(itemAll);

            // Add Day
            var itemDay = new ToolStripMenuItem("Day") { Tag = "D" };
            itemDay.Click += TsBtnDrpShiftType_Click;
            TsBtnDrpShiftType.DropDownItems.Add(itemDay);

            // Add Night
            var itemNight = new ToolStripMenuItem("Night") { Tag = "N" };
            itemNight.Click += TsBtnDrpShiftType_Click;
            TsBtnDrpShiftType.DropDownItems.Add(itemNight);

            // set default display
            TsBtnDrpShiftType.Text = "(All)";
            TsBtnDrpShiftType.Tag = "A";
        }

        #endregion

        #region "TsBtnDrpShiftType_Click"
        private async void TsBtnDrpShiftType_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem menuItem)
            {
                // Update dropdown button text & tag
                TsBtnDrpShiftType.Text = menuItem.Text;
                TsBtnDrpShiftType.Tag = menuItem.Tag;

                // Example: use the selected value
                string selectedValue = TsBtnDrpShiftType.Tag.ToString(); // "A", "D", or "N"

                await LoadDataGridViewAsync();
                                                                         
            }
        }
        #endregion

        #region "TsBtnRefreshWebView_Click"
        private async void TsBtnRefreshWebView_Click(object sender, EventArgs e)
        {
            await LoadShiftSchedulesToWebView2Async(WebViewShiftList);
        }
        #endregion

        #region "LoadShiftSchedulesToWebView2Async"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="web"></param>
        /// <returns></returns>
        private async Task LoadShiftSchedulesToWebView2Async(WebView2 web)
        {

            var startTime = DateTime.Now;  // Storing Start Time
            var progress = new FrmAdminProgress();
            try
            {
                // Initialize progress form
                progress.LblStatus.Text = "Updating List.";
                progress.LblMoreStatus.Text = "Please Wait...";
                progress.ProgressBar1.Visible = false;
                progress.Show();

                // Update status label
                TsLblFooterWebView.Text = "Fetching data. Please wait...";
                TsLblFooterWebView.ForeColor = Color.DarkBlue;
                Application.DoEvents();

                string defaultUnitCode;
                if (TsBtnDrpUnitCode == null || string.IsNullOrWhiteSpace(TsBtnDrpUnitCode.Text) ||
                    string.Equals(TsBtnDrpUnitCode.Text, "(All)", StringComparison.Ordinal) ||
                    string.Equals(TsBtnDrpUnitCode.Text, "Plant Code", StringComparison.Ordinal))
                {
                    throw new InvalidOperationException("Please Select Plant Code from drop down.");
                }
                else
                {
                    defaultUnitCode = TsBtnDrpUnitCode.Text;
                }

                string connectionString = ClassGlobalFunctions.GetConnectionStringByUnitCode(
                        defaultUnitCode ?? ClassGlobalVariables.pubUnitCode);

                if (string.IsNullOrWhiteSpace(connectionString))
                    throw new InvalidOperationException("Connection string cannot be empty.");

                string searchText = string.IsNullOrWhiteSpace(TsTxtSearchWebView.Text)
                ? null
                : TsTxtSearchWebView.Text.Trim().ToLower();

                // Replace '*' with '%' for SQL LIKE query
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    searchText = searchText.Replace("*", "%");
                }
                else
                {
                    searchText = null; // normalize empty to null
                }

                var serachColumns = new List<string> {
                    "UnitCode",
                    "ShiftCode",
                    "ShiftDescription",
                    "ShiftType"};

                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@UnitCode", SqlDbType.NVarChar) { Value = defaultUnitCode });

                // Determine the row limit based on TsCmbLimitRows input:
                // If the text is "(all)", set limit to 0
                // Otherwise, try parsing to int; if fails, default to 50
                // ?. ensures Text is not null before calling Trim()(null - conditional operator).
                int mLimitRows =
                    TsCmbLimitRows.Text?.Trim().ToLower() == "(all)" ? 0 :
                    int.TryParse(TsCmbLimitRows.Text, out var value) ? value : 50;

                string orderBy = "UnitCode, ShiftCode, ShiftType";
                string mTableName = "Att_ShiftSchedules";

                // Get data asynchronously
                DataTable resultTable = await ClassDbHelpers.GetRowsFromTableAsync(
                    unitCode: defaultUnitCode,  // plant code
                    tableName: mTableName, // sql table name
                    selectedColumns: null,   // selectedColumns
                    serachColumns: serachColumns,  // search sql table columns
                    additionalParameters: parameters, // sql parameters
                    searchText: searchText, // search text
                    orderByClause: orderBy,    // sql order by columns
                    isDeletedColumn: "IsDeleted", // table column name for IsDeleted
                    isDeletedValue: isDeletedRows, // isDeletedValue
                    isActiveColumn: "IsActive", // table column name for IsActive
                    isActiveValue: isActiveRows, // isActiveValue
                    limitRows: mLimitRows);

                // Check for empty results
                if (resultTable == null || resultTable.Rows.Count == 0)
                {
                    TsLblFooterWebView.Text = "No rows found for given parameters.";
                    return;
                }

                // Create a filtered DataTable with only the columns we want
                DataTable filteredTable = new DataTable();
                var columnsToInclude = new List<string> {
                    "UnitCode",
                    "ShiftCode",
                    "ShiftDescription",
                    "ShiftType",
                    "ShiftStartTime",
                    "ShiftEndTime",
                    "ShiftDurationHours",
                    "ShiftLunchStartTime",
                    "ShiftLunchEndTime",
                    "ShiftLunchHourTime",
                    "ShiftMinimumOtMin"};

                // Add columns to filtered table
                foreach (string colName in columnsToInclude)
                {
                    if (resultTable.Columns.Contains(colName))
                    {
                        filteredTable.Columns.Add(colName, resultTable.Columns[colName].DataType);
                    }
                }

                // Copy data for selected columns
                foreach (DataRow row in resultTable.Rows)
                {
                    DataRow newRow = filteredTable.NewRow();
                    foreach (string colName in columnsToInclude)
                    {
                        if (resultTable.Columns.Contains(colName))
                        {
                            newRow[colName] = row[colName];
                        }
                    }
                    filteredTable.Rows.Add(newRow);
                }

                // Define and apply custom headers
                var customHeaders = new Dictionary<string, string>
                    {
                        {"UnitCode", "Plant"},
                        {"ShiftCode", "Shift Code"},
                        {"ShiftDescription", "Shift Description"},
                        {"ShiftType", "Shift Type"},
                        {"ShiftStartTime", "Shift Start Time"},
                        {"ShiftEndTime", "Shift End Time"},
                        {"ShiftDurationHours", "Shift Duration Hrs"},
                        {"ShiftLunchStartTime", "Lunch Start Time"},
                        {"ShiftLunchEndTime", "Lunch End Time"},
                        {"ShiftLunchHourTime", "Lunch Hour Minutes"},
                        {"ShiftMinimumOtMin", "Minimum OT Minutes"}
                    };

                // Build HTML and load into WebView2
                string html = HtmlTableBuilder.BuildWebViewHtml(
                    filteredTable,
                    customHeaders);

                // Build HTML
                // string html = BuildShiftSchedulesHtml(filteredTable);
                // Ensure WebView2 ready and navigate
                if (web.CoreWebView2 == null)
                    await web.EnsureCoreWebView2Async(null);

                web.CoreWebView2.NavigateToString(html);

                // Calculate and display elapsed time
                var elapsedTime = DateTime.Now.Subtract(startTime);
                TsLblFooterWebView.Text = $"Fetch Rows [ {resultTable.Rows.Count} ] In: {elapsedTime:hh\\:mm\\:ss}";
                TsLblFooterWebView.ForeColor = Color.DarkBlue;
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                TsLblFooterWebView.Text = ex.Message;
                TsLblFooterWebView.ForeColor = Color.Red;
                MessageBox.Show(ex.Message, "Export List To Html", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                progress.Close();
            }
        }

        #endregion

        #region "BuildShiftSchedulesHtml"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static string BuildShiftSchedulesHtml(DataTable dt)
        {

            var startTime = DateTime.Now;  // Storing Start Time
            var progress = new FrmAdminProgress();

            try
            {


                var sb = new StringBuilder();

                // Basic CSS
                sb.Append(@"
                <!DOCTYPE html>
                <html>
                <head>
                <meta charset='utf-8' />
                <title>Shift Schedules</title>
                <style>
                    body{font-family:Segoe UI,Arial,sans-serif;margin:16px;}
                    .toolbar{display:flex;gap:12px;align-items:center;margin-bottom:10px;flex-wrap:wrap}
                    input[type='text']{padding:6px 8px;min-width:240px;border:1px solid #ccc;border-radius:6px}
                    select{padding:6px 8px;border:1px solid #ccc;border-radius:6px}
                    table{border-collapse:collapse;width:100%}
                    th, td{border:1px solid #e5e7eb;padding:8px 10px;font-size:13px;vertical-align:top}
                    th{background:#f3f4f6;cursor:pointer;position:sticky;top:0}
                    tr:nth-child(even){background:#fafafa}
                    .muted{color:#6b7280}
                    .pill{display:inline-block;padding:2px 8px;border-radius:999px;font-size:12px}
                    .pill-yes{background:#dcfce7;color:#166534}
                    .pill-no{background:#fee2e2;color:#991b1b}
                    .pager{display:flex;gap:6px;align-items:center;margin-top:10px;flex-wrap:wrap}
                    .btn{padding:6px 10px;border:1px solid #d1d5db;border-radius:6px;background:#fff;cursor:pointer}
                    .btn[disabled]{opacity:.5;cursor:not-allowed}
                    .count{margin-left:auto;color:#6b7280}
                    .th-sort-asc::after{content:' ▲';}
                    .th-sort-desc::after{content:' ▼';}
                    .nwrap{white-space:nowrap}
                </style>
                </head>
                <body>
                <div class='toolbar'>
                  <input id='searchBox' type='text' placeholder='Search...' />
                  <label class='muted'>Rows per page:</label>
                  <select id='rowsPerPage'>
                    <option value='10'>10</option>
                    <option value='25'>25</option>
                    <option value='50'>50</option>
                    <option value='100'>100</option>
                    <option value='-1'>All</option>
                  </select>
                  <span class='count' id='countInfo'></span>
                </div>
                <table id='grid'>
                  <thead>
                    <tr>");

                // Columns header (clickable for sort)
                string[] columns = new[]
                {
                                "UnitCode","ShiftCode","ShiftDescription","ShiftType",
                                "ShiftStartTime","ShiftEndTime",
                                "ShiftLunchStartTime","ShiftLunchEndTime","ShiftLunchHourTime"
                            };

                foreach (var col in columns)
                    sb.Append("<th class='nwrap' data-col='").Append(HttpUtility.HtmlEncode(col)).Append("'>")
                      .Append(HttpUtility.HtmlEncode(col)).Append("</th>");

                sb.Append(@"</tr>
                  </thead>
                  <tbody>
                ");

                // Rows
                foreach (DataRow r in dt.Rows)
                {
                    sb.Append("<tr>");
                    foreach (var col in columns)
                    {
                        object v = r[col];

                        // Tidy up values
                        string cell;
                        if (v == DBNull.Value || v == null)
                        {
                            cell = "<span class='muted'>&nbsp;</span>";
                        }
                        else if (col.EndsWith("Time", StringComparison.OrdinalIgnoreCase) && v is TimeSpan ts)
                        {
                            // TIME columns arrive as TimeSpan (SqlDataAdapter maps TIME→TimeSpan)
                            cell = ts.ToString(@"hh\:mm");
                        }
                        else if (col.EndsWith("Time", StringComparison.OrdinalIgnoreCase) && v is string sTime)
                        {
                            // fallback if driver returns string
                            cell = sTime;
                        }
                        else if (col.Equals("IsActive", StringComparison.OrdinalIgnoreCase))
                        {
                            bool active = false;
                            if (v is bool b) active = b;
                            else if (v is byte by) active = by != 0;
                            else if (v is int ii) active = ii != 0;
                            else active = string.Equals(v.ToString(), "true", StringComparison.OrdinalIgnoreCase) ||
                                          v.ToString() == "1";
                            cell = $"<span class='pill {(active ? "pill-yes" : "pill-no")}'>{(active ? "Yes" : "No")}</span>";
                        }
                        else
                        {
                            cell = HttpUtility.HtmlEncode(Convert.ToString(v));
                        }

                        // Data attribute (used by JS sort/filter)
                        string dataAttr = HttpUtility.HtmlAttributeEncode(
                            v == null || v == DBNull.Value ? "" :
                            (v is TimeSpan tsv ? tsv.ToString(@"hh\:mm\:ss") : Convert.ToString(v)));

                        sb.Append("<td data-val='").Append(dataAttr).Append("'>").Append(cell).Append("</td>");
                    }
                    sb.Append("</tr>\n");
                }

                sb.Append(@"  </tbody>
            </table>

            <div class='pager'>
              <button class='btn' id='prevBtn'>&lt; Prev</button>
              <span id='pageInfo' class='muted'></span>
              <button class='btn' id='nextBtn'>Next &gt;</button>
            </div>

            <script>
            (function(){
              const table = document.getElementById('grid');
              const tbody = table.querySelector('tbody');
              const headers = Array.from(table.querySelectorAll('thead th'));
              const searchBox = document.getElementById('searchBox');
              const rowsPerPageSel = document.getElementById('rowsPerPage');
              const prevBtn = document.getElementById('prevBtn');
              const nextBtn = document.getElementById('nextBtn');
              const pageInfo = document.getElementById('pageInfo');
              const countInfo = document.getElementById('countInfo');

              let data = Array.from(tbody.querySelectorAll('tr'));
              let filtered = data.slice();
              let sortCol = -1;
              let sortDir = 1; // 1 asc, -1 desc
              let page = 1;
              let pageSize = parseInt(rowsPerPageSel.value,10);

              function textOfRow(tr){
                return Array.from(tr.cells).map(td => (td.getAttribute('data-val')||td.textContent||'')).join(' ').toLowerCase();
              }

              function applyFilter(){
                const q = (searchBox.value||'').trim().toLowerCase();
                if(!q){ filtered = data.slice(); }
                else { filtered = data.filter(tr => textOfRow(tr).indexOf(q) !== -1); }
                page = 1;
                updateCount();
                render();
              }

              function tryParse(val){
                if(val === null || val === undefined) return {num:false, val:val};
                // time hh:mm:ss → convert to seconds for numeric sort
                if(/^\d{1,2}:\d{2}(:\d{2})?$/.test(val)){
                  const parts = val.split(':').map(Number);
                  const s = parts[0]*3600 + parts[1]*60 + (parts[2]||0);
                  return {num:true, val:s};
                }
                const n = parseFloat(val);
                if(!isNaN(n)) return {num:true, val:n};
                return {num:false, val:val.toString().toLowerCase()};
              }

              function clearSortIndicators(){
                headers.forEach(h=>{h.classList.remove('th-sort-asc','th-sort-desc');});
              }

              function applySort(colIdx){
                if(sortCol === colIdx){ sortDir *= -1; } else { sortCol = colIdx; sortDir = 1; }
                clearSortIndicators();
                if(sortCol >= 0){
                  headers[sortCol].classList.add(sortDir===1 ? 'th-sort-asc' : 'th-sort-desc');
                  filtered.sort((a,b)=>{
                    const av = a.cells[sortCol].getAttribute('data-val')||a.cells[sortCol].textContent;
                    const bv = b.cells[sortCol].getAttribute('data-val')||b.cells[sortCol].textContent;
                    const A = tryParse(av), B = tryParse(bv);
                    if(A.num && B.num) return (A.val - B.val)*sortDir;
                    return A.val.localeCompare(B.val)*sortDir;
                  });
                }
                page = 1;
                render();
              }

              function updateCount(){
                countInfo.textContent = filtered.length + ' row(s)';
              }

              function render(){
                // pagination
                pageSize = parseInt(rowsPerPageSel.value,10);
                let total = filtered.length;
                let totalPages = (pageSize === -1) ? 1 : Math.max(1, Math.ceil(total / pageSize));
                if(page > totalPages) page = totalPages;

                // slice rows to show
                let start = (pageSize === -1) ? 0 : (page-1)*pageSize;
                let end = (pageSize === -1) ? total : Math.min(total, start + pageSize);

                // rebuild tbody
                const frag = document.createDocumentFragment();
                for(let i=start;i<end;i++) frag.appendChild(filtered[i]);
                tbody.innerHTML = '';
                tbody.appendChild(frag);

                // pager state
                pageInfo.textContent = (pageSize===-1) ? `All rows` : `Page ${page} of ${Math.max(1, totalPages)}`;
                prevBtn.disabled = (page <= 1) || (pageSize===-1);
                nextBtn.disabled = (page >= totalPages) || (pageSize===-1);
              }

              // events
              headers.forEach((h,idx)=> h.addEventListener('click', ()=>applySort(idx)));
              searchBox.addEventListener('input', applyFilter);
              rowsPerPageSel.addEventListener('change', ()=>{ page=1; render(); });
              prevBtn.addEventListener('click', ()=>{ if(page>1){ page--; render(); } });
              nextBtn.addEventListener('click', ()=>{ page++; render(); });

              // initial
              updateCount();
              render();
            })();
            </script>
            </body></html>");

                return sb.ToString();
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                progress.Close();
            }
        }

        #endregion

        #region "TsBtnExport_Click"
        private async void TsBtnExport_Click(object sender, EventArgs e)
        {
            await ExportListToHtmlAsync();
        }
        #endregion

        #region "ExportListToHtmlAsync"
        private async Task ExportListToHtmlAsync()
        {

            var startTime = DateTime.Now;  // Storing Start Time
            var progress = new FrmAdminProgress();

            try
            {
                // Initialize progress form
                progress.LblStatus.Text = "Updating List.";
                progress.LblMoreStatus.Text = "Please Wait...";
                progress.ProgressBar1.Visible = false;
                progress.Show();

                // Update status label
                TsLblDgvListFooter.Text = "Fetching data. Please wait...";
                TsLblDgvListFooter.ForeColor = Color.DarkBlue;
                Application.DoEvents();

                string defaultUnitCode;
                if (TsBtnDrpUnitCode == null || string.IsNullOrWhiteSpace(TsBtnDrpUnitCode.Text) ||
                    string.Equals(TsBtnDrpUnitCode.Text, "(All)", StringComparison.Ordinal) ||
                    string.Equals(TsBtnDrpUnitCode.Text, "Plant Code", StringComparison.Ordinal))
                {
                    throw new InvalidOperationException("Please Select Plant Code from drop down.");
                }
                else
                {
                    defaultUnitCode = TsBtnDrpUnitCode.Text;
                }

                string connectionString = ClassGlobalFunctions.GetConnectionStringByUnitCode(
                        defaultUnitCode ?? ClassGlobalVariables.pubUnitCode);

                if (string.IsNullOrWhiteSpace(connectionString))
                    throw new InvalidOperationException("Connection string cannot be empty.");

                string searchText = string.IsNullOrWhiteSpace(TsTxtSearchDgv.Text)
                ? null
                : TsTxtSearchDgv.Text.Trim().ToLower();

                // Replace '*' with '%' for SQL LIKE query
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    searchText = searchText.Replace("*", "%");
                }
                else
                {
                    searchText = null; // normalize empty to null
                }

                var serachColumns = new List<string> {
                    "ShiftCode",
                    "ShiftDescription",
                    "ShiftType"};

                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@UnitCode", SqlDbType.NVarChar) { Value = defaultUnitCode });

                // Determine the row limit based on TsCmbLimitRows input:
                // If the text is "(all)", set limit to 0
                // Otherwise, try parsing to int; if fails, default to 50
                // ?. ensures Text is not null before calling Trim()(null - conditional operator).
                int mLimitRows =
                    TsCmbLimitRows.Text?.Trim().ToLower() == "(all)" ? 0 :
                    int.TryParse(TsCmbLimitRows.Text, out var value) ? value : 50;

                string orderBy = "UnitCode, ShiftCode, ShiftType";
                string mTableName = "Att_ShiftSchedules";

                // Get data asynchronously
                DataTable resultTable = await ClassDbHelpers.GetRowsFromTableAsync(
                    unitCode: defaultUnitCode,  // plant code
                    tableName: mTableName, // sql table name
                    selectedColumns: null,   // selectedColumns
                    serachColumns: serachColumns,  // search sql table columns
                    additionalParameters: parameters, // sql parameters
                    searchText: searchText, // search text
                    orderByClause: orderBy,    // sql order by columns
                    isDeletedColumn: "IsDeleted", // table column name for IsDeleted
                    isDeletedValue: isDeletedRows, // isDeletedValue
                    isActiveColumn: "IsActive", // table column name for IsActive
                    isActiveValue: isActiveRows, // isActiveValue
                    limitRows: mLimitRows);

                // Check for empty results
                if (resultTable == null || resultTable.Rows.Count == 0)
                {
                    TsLblDgvListFooter.Text = "No rows found for given parameters.";
                    return;
                }

                // Create a filtered DataTable with only the columns we want
                DataTable filteredTable = new DataTable();
                var columnsToInclude = new List<string> {
                    "UnitCode",
                    "ShiftCode",
                    "ShiftDescription",
                    "ShiftType",
                    "ShiftStartTime",
                    "ShiftEndTime",
                    "ShiftDurationHours",
                    "ShiftLunchStartTime",
                    "ShiftLunchEndTime",
                    "ShiftLunchHourTime",
                    "ShiftMinimumOtMin"};

                // Add columns to filtered table
                foreach (string colName in columnsToInclude)
                {
                    if (resultTable.Columns.Contains(colName))
                    {
                        filteredTable.Columns.Add(colName, resultTable.Columns[colName].DataType);
                    }
                }

                // Copy data for selected columns
                foreach (DataRow row in resultTable.Rows)
                {
                    DataRow newRow = filteredTable.NewRow();
                    foreach (string colName in columnsToInclude)
                    {
                        if (resultTable.Columns.Contains(colName))
                        {
                            newRow[colName] = row[colName];
                        }
                    }
                    filteredTable.Rows.Add(newRow);
                }

                // Define and apply custom headers
                var customHeaders = new Dictionary<string, string>
                    {
                        {"UnitCode", "Plant"},
                        {"ShiftCode", "Shift Code"},
                        {"ShiftDescription", "Shift Description"},
                        {"ShiftType", "Shift Type"},
                        {"ShiftStartTime", "Shift Start Time"},
                        {"ShiftEndTime", "Shift End Time"},
                        {"ShiftDurationHours", "Shift Duration Hours"},
                        {"ShiftLunchStartTime", "Lunch Start Time"},
                        {"ShiftLunchEndTime", "Lunch End Time"},
                        {"ShiftLunchHourTime", "Lunch Hour Minutes"},
                        {"ShiftMinimumOtMin", "Minimum OT Minutes"}
                    };

                var folderPath = GetReportFolderPath();
                var file = Path.Combine(folderPath, "AttShifts.html");

                // var file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Contractors.html");

                HtmlExportHelper.ExportDataTableToHtml(
                    table: filteredTable,
                    filePath: file,
                    title: "Shift Schedules",
                    dateFormat: "dd-MMM-yyyy",
                    customHeaders: customHeaders);

                // Calculate and display elapsed time
                var elapsedTime = DateTime.Now.Subtract(startTime);
                TsLblDgvListFooter.Text = $"Fetch Rows [ {resultTable.Rows.Count} ] In: {elapsedTime:hh\\:mm\\:ss}";
                TsLblDgvListFooter.ForeColor = Color.DarkBlue;
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                TsLblDgvListFooter.Text = ex.Message;
                TsLblDgvListFooter.ForeColor = Color.Red;
                MessageBox.Show(ex.Message, "Export List To Html", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                progress.Close();
            }
        }

        #endregion

        #region "GetReportFolderPath"
        private static string GetReportFolderPath()
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
            string folderPath = Path.Combine(baseDrive, "Reports", Application.ProductName, "AttShiftSchedule");

            // create folder if it doesn’t exist
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            return folderPath;
        }
        #endregion

        #region "TsBtnExporotExcel"
        private async void TsBtnExporotExcel_Click(object sender, EventArgs e)
        {
            await ExportListToExcelAsync();
        }
        #endregion

        #region "ExportListToExcelAsync"
        private async Task ExportListToExcelAsync()
        {

            var startTime = DateTime.Now;  // Storing Start Time
            var progress = new FrmAdminProgress();

            try
            {
                // Initialize progress form
                progress.LblStatus.Text = "Updating List.";
                progress.LblMoreStatus.Text = "Please Wait...";
                progress.ProgressBar1.Visible = false;
                progress.Show();

                // Update status label
                TsLblDgvListFooter.Text = "Fetching data. Please wait...";
                TsLblDgvListFooter.ForeColor = Color.DarkBlue;
                Application.DoEvents();

                string defaultUnitCode;
                if (TsBtnDrpUnitCode == null || string.IsNullOrWhiteSpace(TsBtnDrpUnitCode.Text) ||
                    string.Equals(TsBtnDrpUnitCode.Text, "(All)", StringComparison.Ordinal) ||
                    string.Equals(TsBtnDrpUnitCode.Text, "Plant Code", StringComparison.Ordinal))
                {
                    throw new InvalidOperationException("Please Select Plant Code from drop down.");
                }
                else
                {
                    defaultUnitCode = TsBtnDrpUnitCode.Text;
                }

                string connectionString = ClassGlobalFunctions.GetConnectionStringByUnitCode(
                        defaultUnitCode ?? ClassGlobalVariables.pubUnitCode);

                if (string.IsNullOrWhiteSpace(connectionString))
                    throw new InvalidOperationException("Connection string cannot be empty.");

                string searchText = string.IsNullOrWhiteSpace(TsTxtSearchDgv.Text)
                ? null
                : TsTxtSearchDgv.Text.Trim().ToLower();

                // Replace '*' with '%' for SQL LIKE query
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    searchText = searchText.Replace("*", "%");
                }
                else
                {
                    searchText = null; // normalize empty to null
                }

                var serachColumns = new List<string> {
                    "ShiftCode",
                    "ShiftDescription",
                    "ShiftType"};

                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@UnitCode", SqlDbType.NVarChar) { Value = defaultUnitCode });

                // Determine the row limit based on TsCmbLimitRows input:
                // If the text is "(all)", set limit to 0
                // Otherwise, try parsing to int; if fails, default to 50
                // ?. ensures Text is not null before calling Trim()(null - conditional operator).
                int mLimitRows =
                    TsCmbLimitRows.Text?.Trim().ToLower() == "(all)" ? 0 :
                    int.TryParse(TsCmbLimitRows.Text, out var value) ? value : 50;

                string orderBy = "UnitCode, ShiftCode, ShiftType";
                string mTableName = "Att_ShiftSchedules";

                // Get data asynchronously
                DataTable resultTable = await ClassDbHelpers.GetRowsFromTableAsync(
                    unitCode: defaultUnitCode,  // plant code
                    tableName: mTableName, // sql table name
                    selectedColumns: null,   // selectedColumns
                    serachColumns: serachColumns,  // search sql table columns
                    additionalParameters: parameters, // sql parameters
                    searchText: searchText, // search text
                    orderByClause: orderBy,    // sql order by columns
                    isDeletedColumn: "IsDeleted", // table column name for IsDeleted
                    isDeletedValue: isDeletedRows, // isDeletedValue
                    isActiveColumn: "IsActive", // table column name for IsActive
                    isActiveValue: isActiveRows, // isActiveValue
                    limitRows: mLimitRows);

                // Check for empty results
                if (resultTable == null || resultTable.Rows.Count == 0)
                {
                    TsLblDgvListFooter.Text = "No rows found for given parameters.";
                    return;
                }

                // Create a filtered DataTable with only the columns we want
                DataTable filteredTable = new DataTable();
                var columnsToInclude = new List<string> {
                    "UnitCode",
                    "ShiftCode",
                    "ShiftDescription",
                    "ShiftType",
                    "ShiftStartTime",
                    "ShiftEndTime",
                    "ShiftDurationHours",
                    "ShiftLunchStartTime",
                    "ShiftLunchEndTime",
                    "ShiftLunchHourTime",
                    "ShiftMinimumOtMin"};

                // Add columns to filtered table
                foreach (string colName in columnsToInclude)
                {
                    if (resultTable.Columns.Contains(colName))
                    {
                        filteredTable.Columns.Add(colName, resultTable.Columns[colName].DataType);
                    }
                }

                // Copy data for selected columns
                foreach (DataRow row in resultTable.Rows)
                {
                    DataRow newRow = filteredTable.NewRow();
                    foreach (string colName in columnsToInclude)
                    {
                        if (resultTable.Columns.Contains(colName))
                        {
                            newRow[colName] = row[colName];
                        }
                    }
                    filteredTable.Rows.Add(newRow);
                }

                // Define and apply custom headers
                var customHeaders = new Dictionary<string, string>
                    {
                        {"UnitCode", "Plant"},
                        {"ShiftCode", "Shift Code"},
                        {"ShiftDescription", "Shift Description"},
                        {"ShiftType", "Shift Type"},
                        {"ShiftStartTime", "Shift Start Time"},
                        {"ShiftEndTime", "Shift End Time"},
                        {"ShiftDurationHours", "Shift Duration Hours"},
                        {"ShiftLunchStartTime", "Lunch Start Time"},
                        {"ShiftLunchEndTime", "Lunch End Time"},
                        {"ShiftLunchHourTime", "Lunch Hour Minutes"},
                        {"ShiftMinimumOtMin", "Minimum OT Minutes"}
                    };


                var folderPath = GetReportFolderPath();
                var file = Path.Combine(folderPath, "ShiftSchedules.xlsx");

                ExcelExportHelpers.ExportDataTableToExcel(
                    dt: filteredTable,
                    path: file,
                    customHeaders: customHeaders);

                // Confirm file exists and was just written
                if (File.Exists(file))
                {
                    // Optional: check file last write time is "recent" (within a few seconds)
                    var lastWrite = File.GetLastWriteTime(file);
                    if (lastWrite > DateTime.Now.AddSeconds(-10))
                    {
                        // Open the file
                        Process.Start(file);

                        // Also open folder and select the file
                        Process.Start("explorer.exe", $"/select,\"{file}\"");
                    }
                    else
                    {
                        MessageBox.Show("File was not updated with latest data. Last modified: " + lastWrite);
                    }
                }
                else
                {
                    MessageBox.Show("Export failed: file was not created.");
                }

                // Calculate and display elapsed time
                var elapsedTime = DateTime.Now.Subtract(startTime);
                TsLblDgvListFooter.Text = $"Fetch Rows [ {resultTable.Rows.Count} ] In: {elapsedTime:hh\\:mm\\:ss}";
                TsLblDgvListFooter.ForeColor = Color.DarkBlue;
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                TsLblDgvListFooter.Text = ex.Message;
                TsLblDgvListFooter.ForeColor = Color.Red;
                MessageBox.Show(ex.Message, "Export List To Html", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                progress.Close();
            }
        }

        #endregion

        #region "UnitPickerForm"
        public partial class UnitPickerForm : Form
        {
            private readonly string _conStr;
            private readonly string _excludeUnitCode;
            private readonly string _userRowGuid;

            public string SelectedUnitCode
            {
                get
                {
                    var it = cboUnits.SelectedItem as UnitItem;
                    return it != null ? it.UnitCode : null;
                }
            }

            public UnitPickerForm(
                string connectionString,
                string excludeUnitCode,
                string userRowGuid)
            {
                // InitializeComponent();
                _conStr = connectionString;
                _excludeUnitCode = excludeUnitCode;
                _userRowGuid = userRowGuid;

                this.Text = "Select UnitCode";
                this.StartPosition = FormStartPosition.CenterParent;
                this.Width = 380;
                this.Height = 140;

                cboUnits = new ComboBox { Left = 12, Top = 12, Width = 340, DropDownStyle = ComboBoxStyle.DropDownList };
                btnOk = new Button { Left = 188, Top = 50, Width = 75, Text = "OK", DialogResult = DialogResult.OK };
                btnCancel = new Button { Left = 277, Top = 50, Width = 75, Text = "Cancel", DialogResult = DialogResult.Cancel };

                this.Controls.Add(cboUnits);
                this.Controls.Add(btnOk);
                this.Controls.Add(btnCancel);
                this.AcceptButton = btnOk;
                this.CancelButton = btnCancel;

                LoadUnits();
            }

            private ComboBox cboUnits;
            private Button btnOk;
            private Button btnCancel;

            private sealed class UnitItem
            {
                public string UnitCode { get; set; }
                public string AliasName { get; set; }
                public override string ToString() { return string.IsNullOrEmpty(AliasName) ? UnitCode : (UnitCode + " - " + AliasName); }
            }

            private void LoadUnits()
            {
                var list = new System.Collections.Generic.List<UnitItem>();
                try
                {
                    using (var con = new SqlConnection(_conStr))
                    using (var cmd = new SqlCommand(@"
                        SELECT u.UnitCode, u.AliasName
                        FROM dbo.Companies AS u WITH (NOLOCK)
                        WHERE u.IsActive = 1 AND u.IsDeleted = 0
                          AND (@ExcludeUnitCode IS NULL OR u.UnitCode <> @ExcludeUnitCode)
                          AND u.UnitCode IN (
                                SELECT aua.UnitCode
                                FROM dbo.AdminUserUnitAccess AS aua WITH (NOLOCK)
                                WHERE aua.UserRowGuid = @UserRowGuid
                          )
                        ORDER BY u.UnitCode;", con))
                    {
                        cmd.Parameters.Add("@ExcludeUnitCode", SqlDbType.VarChar, 50)
                            .Value = string.IsNullOrWhiteSpace(_excludeUnitCode) ? (object)DBNull.Value : _excludeUnitCode;

                        cmd.Parameters.Add("@UserRowGuid", SqlDbType.NVarChar, 150)
                            .Value = string.IsNullOrWhiteSpace(_userRowGuid) ? (object)DBNull.Value : _userRowGuid;

                        con.Open();
                        using (var r = cmd.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                list.Add(new UnitItem
                                {
                                    UnitCode = r["UnitCode"] as string,
                                    AliasName = r["AliasName"] as string
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load UnitCodes:\n" + ex.Message, "Units", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                cboUnits.Items.Clear();
                foreach (var it in list) cboUnits.Items.Add(it);
                if (cboUnits.Items.Count > 0) cboUnits.SelectedIndex = 0;
            }
        }

        #endregion



    }
}

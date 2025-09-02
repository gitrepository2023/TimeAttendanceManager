using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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
///    Title:          Code for INSERT
///                    
/// 
///    Name:           FrmContractors.cs
///    Created:        23rd August 2025
///    Date Completed: 23rd August 2025
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

namespace TimeAttendanceManager.Contractors.Forms
{
    public partial class FrmContractors : Form
    {
        #region "Constructor"
        public FrmContractors()
        {
            InitializeComponent();

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
            this.TsBtnAddNew.Click -= TsBtnAddNew_Click;
            this.TsBtnSave.Click -= TsBtnSave_Click;

            this.TsMenuViewInActive.Click -= TsMenuViewInActive_Click;
            this.TsMenuViewDeleted.Click -= TsMenuViewDeleted_Click;
            this.TsBtRefreshDgv.Click -= TsBtRefreshDgv_Click;
            this.TsBtnClearSearchDgv.Click -= TsBtRefreshDgv_Click;
            this.TsBtnSearchDgv.Click -= TsBtRefreshDgv_Click;
            this.TsTxtSearchDgv.KeyDown -= TsTxtSearchDgv_KeyDown;
            this.TsBtnDrpUnitCode.DropDownItemClicked -= TsBtnDrpBtn_DropDownItemClicked;
            this.TsBtnExport.Click -= TsBtnExport_Click;

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
            this.TsBtnAddNew.Click += TsBtnAddNew_Click;
            this.TsBtnSave.Click += TsBtnSave_Click;

            this.TsMenuViewInActive.Click += TsMenuViewInActive_Click;
            this.TsMenuViewDeleted.Click += TsMenuViewDeleted_Click;
            this.TsBtRefreshDgv.Click += TsBtRefreshDgv_Click;
            this.TsBtnClearSearchDgv.Click += TsBtRefreshDgv_Click;
            this.TsBtnSearchDgv.Click += TsBtRefreshDgv_Click;
            this.TsTxtSearchDgv.KeyDown += TsTxtSearchDgv_KeyDown;
            this.TsBtnDrpUnitCode.DropDownItemClicked += TsBtnDrpBtn_DropDownItemClicked;
            this.TsBtnExport.Click += TsBtnExport_Click;
        }
        #endregion

        #region "LocalVariables"
        private MasterContractor myDataTable = new MasterContractor();
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

        #region "SetSplitterDistance"
        private void SetSplitterDistance()
        {
            try
            {
                // Hide Panel
                SplitMain.Panel2Collapsed = true;

                // Set SplitterDistance to 30% and 40% of the total width
                SplitMain.SplitterDistance = (int)(this.SplitMain.Width * 0.6);
                
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
            SplitMain.Panel2Collapsed = !SplitMain.Panel2Collapsed;
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

                await ClassGlobalFunctions.FillComboBoxAsync("StateName", "Id", CmbState, "AdminIndiaStates", ClassGlobalVariables.pubUnitCode);

                await FillUnitCodeAsync();

                // Get SQL Server Today's date
                DateTime safeDate = ClassGlobalVariables.SqlServerTodayDate.HasValue
                    ? ClassGlobalVariables.SqlServerTodayDate.Value.Date
                    : DateTime.Today;

                // Format DatePicker
                ClassSafeValueHelpers.ConfigureDateTimePicker(DtPickConctractStartDate, safeDate);
                ClassSafeValueHelpers.ConfigureDateTimePicker(DtPickConctractEndDate, safeDate);
                ClassSafeValueHelpers.ConfigureDateTimePicker(DtPickConctractLastReview, safeDate);
                ClassSafeValueHelpers.ConfigureDateTimePicker(DtPickConctractNextReview, safeDate);

                // Read user defaults
                await ReadUserLoginDefaultXmlAsync();
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
                SplitMain.Panel2Collapsed = !SplitMain.Panel2Collapsed;
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

        #region "TsBtnFilterClose_Click"
        private void TsBtnFilterClose_Click(object sender, EventArgs e)
        {

            SplitMain.Panel2Collapsed = true;
        }
        #endregion

        #region "TsBtnOptTech_Click"
        private void TsBtnOptTech_Click(object sender, EventArgs e)
        {
            try
            {
                // Only SUPER can toggle/show the tech panel
                if (!string.Equals(ClassGlobalVariables.pubUserRole, "SUPER", StringComparison.OrdinalIgnoreCase))
                    return;

                // Toggle right panel visibility
                SplitMain.Panel2Collapsed = !SplitMain.Panel2Collapsed;

                // Build the tech details object
                var mTechDetails = new ClassTechDetails
                {
                    // Set form identity
                    FormName = this.Name,
                    FormTitle = this.Text,

                    // Set default "created" date (VB had FormatDateTime("09-MAY-25", ShortDate))
                    // Here we parse explicitly as dd-MMM-yy using invariant culture.
                    FormCreated = DateTime.ParseExact("22-AUG-2025", "dd-MMM-yyyy", CultureInfo.InvariantCulture)
                };

                // Add table names used in this form (9 fields as in your VB)
                mTechDetails.TableNames.Add(new TableNames(
                    "dbo.Master_Contractors",
                    "dbo.usp_Master_Contractors_Upsert",
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

            TsLblInputStatus.Text = "Add all required information and click on Save.";
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

                var msgResponse = MessageBox.Show("Are you sure you want to Save changes ?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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

                if (await InsertRowsAsync())
                {
                    // Update was successful
                    MessageBox.Show("Row inserted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearInputs();

                    // Calculate and display elapsed time
                    var elapsedTime = DateTime.Now.Subtract(startTime);
                    TsLblInputStatus.Text = $"Done In: {elapsedTime:hh\\:mm\\:ss}";
                    TsLblInputStatus.ForeColor = Color.DarkBlue;
                    Application.DoEvents();

                }
                else
                    // Update failed
                    MessageBox.Show("Failed to insert row.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                progress.Close();
                progress.Dispose();
            }
        }

        #endregion

        #region "InsertRowsAsync"
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task<bool> InsertRowsAsync()
        {
            try
            {
                UseWaitCursor = true;

                const string tableName = "dbo.Master_Contractors";
                const string sqlProcedureName = "dbo.usp_Master_Contractors_Upsert";

                string defaultUnitCode = CmbUnitCode.Text;
                
                string connectionString = ClassGlobalFunctions.GetConnectionStringByUnitCode(defaultUnitCode);
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new InvalidOperationException("Connection string cannot be empty");
                }

                var parameters = new List<SqlParameter>
                {
                    ClassDbHelpers.CreateSqlParameter("@UnitCode", SqlDbType.VarChar, myDataTable.UnitCode),
                    ClassDbHelpers.CreateSqlParameter("@ContractorCode", SqlDbType.NVarChar, myDataTable.ContractorCode),
                    ClassDbHelpers.CreateSqlParameter("@ContractorName", SqlDbType.NVarChar, myDataTable.ContractorName),
                    ClassDbHelpers.CreateSqlParameter("@ContactPerson", SqlDbType.NVarChar, myDataTable.ContactPerson),
                    ClassDbHelpers.CreateSqlParameter("@PhoneNumber", SqlDbType.NVarChar, myDataTable.PhoneNumber),
                    ClassDbHelpers.CreateSqlParameter("@AlternatePhone", SqlDbType.NVarChar, myDataTable.AlternatePhone),
                    ClassDbHelpers.CreateSqlParameter("@Email", SqlDbType.NVarChar, myDataTable.Email),
                    ClassDbHelpers.CreateSqlParameter("@AddressLine1", SqlDbType.NVarChar, myDataTable.AddressLine1),
                    ClassDbHelpers.CreateSqlParameter("@AddressLine2", SqlDbType.NVarChar, myDataTable.AddressLine2),
                    ClassDbHelpers.CreateSqlParameter("@City", SqlDbType.NVarChar, myDataTable.City),
                    ClassDbHelpers.CreateSqlParameter("@State", SqlDbType.NVarChar, myDataTable.State),
                    ClassDbHelpers.CreateSqlParameter("@PostalCode", SqlDbType.NVarChar, myDataTable.PostalCode),

                    ClassDbHelpers.CreateSqlParameter("@ContractStartDate", SqlDbType.Date, myDataTable.ContractStartDate),
                    ClassDbHelpers.CreateSqlParameter("@ContractEndDate", SqlDbType.Date, myDataTable.ContractEndDate),
                    ClassDbHelpers.CreateSqlParameter("@LastReviewDate", SqlDbType.Date, myDataTable.LastReviewDate),
                    ClassDbHelpers.CreateSqlParameter("@NextReviewDate", SqlDbType.Date, myDataTable.NextReviewDate),
                    ClassDbHelpers.CreateSqlParameter("@ReviewFrequencyMonths", SqlDbType.Int, myDataTable.ReviewFrequencyMonths),

                    ClassDbHelpers.CreateSqlParameter("@RowVersion", SqlDbType.Int, myDataTable.RowVersion),
                    ClassDbHelpers.CreateSqlParameter("@UserId", SqlDbType.Int, myDataTable.UserId),
                    ClassDbHelpers.CreateSqlParameter("@UserRowGuid", SqlDbType.VarChar, myDataTable.UserCreatedRowGuid.ToString()),
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

        #region "ValidateInputAsync"

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task<bool> ValidateInputAsync()
        {
            try
            {
                TsLblInputStatus.ForeColor = Color.Red;
                TsLblInputStatus.Text = "Validating data. Please wait...";

                // Remove existing errors
                ClassLayoutHelper.ClearErrorsTableLayout(TableLayout1, errorProvider1);

                // Required control validations
                ClassValidationHelper.ValidateControl(CmbUnitCode, "Plant Code is required.", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateControl(TxtConcrtractorCode, "Contractor Code is required.", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateControl(TxtConctractorName, "Contractor Name is required.", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateControl(TxtContactPerson, "Contact Person is required.", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateControl(TxtPhoneNumber, "Phone Number is required.", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateControl(TxtCity, "City Name is required.", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateControl(CmbState, "State Name is required.", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateControl(TxtPostalCode, "Postal Code is required.", errorProvider1, TsLblInputStatus);
                
                // Check if the checkbox is unchecked(i.e., date is not provided)
                if (!DtPickConctractStartDate.Checked)
                {
                    throw new ArgumentException("Contract Start Date is required.");
                }

                // ensure date-only
                // equal is allowed; only future is disallowed
                DateTime safeDate = ClassGlobalVariables.SqlServerTodayDate.HasValue
                    ? ClassGlobalVariables.SqlServerTodayDate.Value.Date
                    : DateTime.Today;

                DateTime validDate = safeDate;
                DateTime contractStartDate = DtPickConctractStartDate.Value.Date;
                if (contractStartDate > validDate)
                {
                    string errorMessage = "Contract Start Date cannot be in the future.";
                    errorProvider1.SetError(DtPickConctractStartDate, errorMessage);
                    throw new Exception(errorMessage);
                }

                DateTime contractEndDate = DtPickConctractEndDate.Value.Date;
                if (DtPickConctractEndDate.Checked)
                {
                    if (contractStartDate > contractEndDate)
                    {
                        string errorMessage = "Contract Start Date must be less than Contract End Date.";
                        errorProvider1.SetError(DtPickConctractEndDate, errorMessage);
                        throw new Exception(errorMessage);
                    }
                }

                if (!string.IsNullOrWhiteSpace(TxtReviewFreq.Text))
                {
                    if (!int.TryParse(TxtReviewFreq.Text.Trim(), out int reviewFreq) || reviewFreq <= 0)
                    {
                        throw new ArgumentException("Review Frequency must be a valid positive integer.");
                    }
                }

                // Length validations
                ClassValidationHelper.ValidateTextBoxLength(TxtConcrtractorCode, 50, "Contractor Code", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateTextBoxLength(TxtConctractorName, 150, "Contractor Name", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateTextBoxLength(TxtContactPerson, 100, "Contact Person", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateTextBoxLength(TxtPhoneNumber, 20, "Phone Number", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateTextBoxLength(TxtAlternatePhone, 20, "Alternate Number", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateTextBoxLength(TxtEmail, 100, "Email", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateTextBoxLength(TxtAddsLine01, 200, "Address Line 01", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateTextBoxLength(TxtAddsLine02, 200, "Address Line 02", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateTextBoxLength(TxtCity, 50, "Address Line 02", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateTextBoxLength(TxtPostalCode, 20, "Address Line 02", errorProvider1, TsLblInputStatus);

                // Dropdown value validations
                int? selectedUnitId = ClassValidationHelper.ValidateCmbSelectedValue(CmbUnitCode, "Plant Code is required.", errorProvider1, TsLblInputStatus);

                // Email validation
                if (!string.IsNullOrWhiteSpace(TxtEmail.Text))
                {
                    string userEmail = TxtEmail.Text.Trim().ToLower();
                    if (!ClassValidationHelper.IsValidEmail(userEmail))
                    {
                        string errorMessage = "Invalid Email format.";
                        errorProvider1.SetError(TxtEmail, errorMessage);
                        throw new Exception(errorMessage);
                    }
                }

                // Phone number validation
                if (!string.IsNullOrWhiteSpace(TxtPhoneNumber.Text) && !long.TryParse(TxtPhoneNumber.Text, out _))
                {
                    string errorMessage = "Please enter a valid numeric value for contact number.";
                    errorProvider1.SetError(TxtPhoneNumber, errorMessage);
                    throw new Exception(errorMessage);
                }

                if (!string.IsNullOrWhiteSpace(TxtAlternatePhone.Text) && !long.TryParse(TxtAlternatePhone.Text, out _))
                {
                    string errorMessage = "Please enter a valid numeric value for alternate number.";
                    errorProvider1.SetError(TxtPhoneNumber, errorMessage);
                    throw new Exception(errorMessage);
                }

                string defaultUnitCode = CmbUnitCode.Text;

                string connectionString = ClassGlobalFunctions.GetConnectionStringByUnitCode(defaultUnitCode);
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new InvalidOperationException("Connection string cannot be empty");
                }

                // check for duplicate rows
                bool exists = await ClassGlobalFunctions.RowExistsForUnitCodeAsync(
                connectionString: connectionString,
                tableName: "dbo.Master_Contractors",
                columnName: "ContractorCode",
                unitCode: CmbUnitCode.Text,
                value: ClassStringHelpers.CleanAndUpperCase(TxtConcrtractorCode.Text));

                if (exists)
                {
                    string errorMessage = $"Contractor code already exists in Plant {CmbUnitCode.Text}.";
                    errorProvider1.SetError(TxtConcrtractorCode, errorMessage);
                    throw new Exception(errorMessage);
                }

                // check for duplicate rows
                bool existsConctractName = await ClassGlobalFunctions.RowExistsForUnitCodeAsync(
                connectionString: connectionString,
                tableName: "dbo.Master_Contractors",
                columnName: "ContractorName",
                unitCode: CmbUnitCode.Text,
                value: ClassStringHelpers.CleanAndUpperCase(TxtConctractorName.Text));

                if (existsConctractName)
                {
                    string errorMessage = $"Contractor name already exists in Plant {CmbUnitCode.Text}.";
                    errorProvider1.SetError(TxtConcrtractorCode, errorMessage);
                    throw new Exception(errorMessage);
                }

                // Populate data table
                myDataTable = new MasterContractor();

                // Id
                // Set null as we want to insert new row
                myDataTable.Id = null;

                // UnitCode
                myDataTable.UnitCode = !string.IsNullOrEmpty(CmbUnitCode.Text)
                    ? CmbUnitCode.Text
                    : null;

                // ContractorCode
                myDataTable.ContractorCode = !string.IsNullOrEmpty(TxtConcrtractorCode.Text)
                    ? ClassStringHelpers.CleanAndUpperCase(TxtConcrtractorCode.Text)
                    : null;

                // ContractorCode
                myDataTable.ContractorName = !string.IsNullOrEmpty(TxtConctractorName.Text)
                    ? ClassStringHelpers.CleanAndUpperCase(TxtConctractorName.Text)
                    : null;

                // ContactPerson
                myDataTable.ContactPerson = !string.IsNullOrEmpty(TxtContactPerson.Text)
                    ? ClassStringHelpers.CleanAndUpperCase(TxtContactPerson.Text)
                    : null;

                // PhoneNumber
                myDataTable.PhoneNumber = !string.IsNullOrEmpty(TxtPhoneNumber.Text)
                    ? ClassStringHelpers.CleanAndNormalizeString(TxtPhoneNumber.Text)
                    : null;

                // AlternatePhone
                myDataTable.AlternatePhone = !string.IsNullOrEmpty(TxtAlternatePhone.Text)
                    ? ClassStringHelpers.CleanAndNormalizeString(TxtAlternatePhone.Text)
                    : null;

                // Email
                myDataTable.Email = !string.IsNullOrEmpty(TxtEmail.Text)
                    ? ClassStringHelpers.CleanAndLowerCase(TxtEmail.Text)
                    : null;

                // AddressLine1
                myDataTable.AddressLine1 = !string.IsNullOrEmpty(TxtAddsLine01.Text)
                    ? ClassStringHelpers.CleanAndUpperCase(TxtAddsLine01.Text)
                    : null;

                // AddressLine2
                myDataTable.AddressLine2 = !string.IsNullOrEmpty(TxtAddsLine02.Text)
                    ? ClassStringHelpers.CleanAndUpperCase(TxtAddsLine02.Text)
                    : null;

                // City
                myDataTable.City = !string.IsNullOrEmpty(TxtCity.Text)
                    ? ClassStringHelpers.CleanAndUpperCase(TxtCity.Text)
                    : null;

                // State
                myDataTable.State = !string.IsNullOrEmpty(CmbState.Text)
                    ? ClassStringHelpers.CleanAndNormalizeString(CmbState.Text)
                    : null;

                // PostalCode
                myDataTable.PostalCode = !string.IsNullOrEmpty(TxtPostalCode.Text)
                    ? ClassStringHelpers.CleanAndNormalizeString(TxtPostalCode.Text)
                    : null;

                // ContractStartDate
                myDataTable.ContractStartDate = contractStartDate;

                // ContractEndDate
                myDataTable.ContractEndDate = DtPickConctractEndDate.Checked ? DtPickConctractEndDate.Value.Date : (DateTime?)null;

                // LastReviewDate
                myDataTable.LastReviewDate = DtPickConctractLastReview.Checked ? DtPickConctractLastReview.Value.Date : (DateTime?)null;

                // NextReviewDate
                myDataTable.NextReviewDate = DtPickConctractNextReview.Checked ? DtPickConctractNextReview.Value.Date : (DateTime?)null;

                // IPAddress
                myDataTable.IpAddsCreated = ClassGlobalVariables.pubHostIPAddress;

                // UserCreatedRowGuid
                myDataTable.UserCreatedRowGuid = ClassGlobalVariables.pubLoginUserRowGuid;

                // HostName
                myDataTable.HostName = ClassGlobalVariables.pubDNSHostName;

                TsLblInputStatus.ForeColor = Color.DarkBlue;
                TsLblInputStatus.Text = "Done...";

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Validate Input", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
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

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Message: {ex.Message}\n\nStack Trace: {ex.StackTrace}",
                          "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

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
                    "TsBtnDrpEmpCatg_DropDownItemClicked",
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
                }));

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
                    "UnitCode",
                    "ContractorCode",
                    "ContractorName",
                    "ContactPerson",
                    "PhoneNumber",
                    "Email",
                    "City",
                    "State"};

                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@UnitCode", SqlDbType.NVarChar) { Value = defaultUnitCode });

                // Determine the row limit based on TsCmbLimitRows input:
                // If the text is "(all)", set limit to 0
                // Otherwise, try parsing to int; if fails, default to 50
                // ?. ensures Text is not null before calling Trim()(null - conditional operator).
                int mLimitRows =
                    TsCmbLimitRows.Text?.Trim().ToLower() == "(all)" ? 0 :
                    int.TryParse(TsCmbLimitRows.Text, out var value) ? value : 50;

                string orderBy = "UnitCode, ContractorCode, ContractorName";
                string mTableName = "Master_Contractors";

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
                    "ContractorCode",
                    "ContractorName",
                    "ContactPerson",
                    "City",
                    "State",
                    "PhoneNumber",
                    "Email",
                    "ContractStartDate",
                    "ContractEndDate"};

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
                        {"ContractorCode", "Contractor Code"},
                        {"ContractorName", "Contractor Name"},
                        {"ContactPerson", "Contact Person"},
                        {"City", "City"},
                        {"State", "State"},
                        {"PhoneNumber", "Phone Number"},
                        {"Email", "Email"},
                        {"ContractStartDate", "Contract Start Date"},
                        {"ContractEndDate", "Contract End Date"}
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

                    // Make grid readonly except CanView
                    foreach (DataGridViewColumn col in DgvList.Columns)
                    {
                        col.ReadOnly = col.Name != "CanView";
                    }

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

                    DgvList.Columns["ContractStartDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
                    DgvList.Columns["ContractEndDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";

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
                        SELECT UnitCode, Id 
                        FROM {tableName} 
                        WHERE IsActive = 1 AND IsDeleted = 0 
                        ORDER BY UnitCode";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UnitCode", resolvedUnitCode);

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
                    "UnitCode",
                    "ContractorCode",
                    "ContractorName",
                    "ContactPerson",
                    "PhoneNumber",
                    "Email",
                    "City",
                    "State"};

                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@UnitCode", SqlDbType.NVarChar) { Value = defaultUnitCode });

                // Determine the row limit based on TsCmbLimitRows input:
                // If the text is "(all)", set limit to 0
                // Otherwise, try parsing to int; if fails, default to 50
                // ?. ensures Text is not null before calling Trim()(null - conditional operator).
                int mLimitRows =
                    TsCmbLimitRows.Text?.Trim().ToLower() == "(all)" ? 0 :
                    int.TryParse(TsCmbLimitRows.Text, out var value) ? value : 50;

                string orderBy = "UnitCode, ContractorCode, ContractorName";
                string mTableName = "Master_Contractors";

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
                    "ContractorCode",
                    "ContractorName",
                    "ContactPerson",
                    "City",
                    "State",
                    "PhoneNumber",
                    "Email",
                    "ContractStartDate",
                    "ContractEndDate"};

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
                        {"ContractorCode", "Contractor Code"},
                        {"ContractorName", "Contractor Name"},
                        {"ContactPerson", "Contact Person"},
                        {"City", "City"},
                        {"State", "State"},
                        {"PhoneNumber", "Phone Number"},
                        {"Email", "Email"},
                        {"ContractStartDate", "Contract Start Date"},
                        {"ContractEndDate", "Contract End Date"}
                    };

                var folderPath = ClassLayoutHelper.GetReportFolderPath("Conctractors");
                var file = Path.Combine(folderPath, "Contractors.html");

                // var file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Contractors.html");

                HtmlExportHelper.ExportDataTableToHtml(
                    table: filteredTable,
                    filePath: file, 
                    title: "Master Contractors", 
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



    }
}

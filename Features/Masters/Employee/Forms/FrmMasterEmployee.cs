using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimeAttendanceManager.Auth.Classes;
using TimeAttendanceManager.Contractors.Models;
using TimeAttendanceManager.Features.Masters.Employee.Classes;
using TimeAttendanceManager.Features.Masters.Employee.Models;
using TimeAttendanceManager.Helpers.Classes;
using TimeAttendanceManager.Main.Classes;
using TimeAttendanceManager.Main.Forms;

namespace TimeAttendanceManager.Features.Masters.Employee.Forms
{
    public partial class FrmMasterEmployee : Form
    {
        #region "Constructor"   
        public FrmMasterEmployee()
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
            this.TsBtnOptClose.Click += TsBtnOptClose_Click;
            this.TsBtnFilterClose.Click -= TsBtnFilterClose_Click;

            // Add events
            this.Load += Form_Load;
            this.FormClosing += FormClosing_FormClosing;
            this.TsBtnCloseForm.Click += TsBtnCloseForm_Click;
            this.TsBtnClose.Click += TsBtnCloseForm_Click;
            this.TabCtrlMain.Click += TabCtrlMain_Click;
            this.TsBtnSidePanel.Click += TsBtnSideMenu_Click;
            this.TsBtnHelp.Click += TsBtnHelp_Click;
            this.TsBtnOptTech.Click += TsBtnOptTech_Click;
            this.TsBtnOptClose.Click += TsBtnOptClose_Click;
            this.TsBtnFilterClose.Click += TsBtnFilterClose_Click;


        }
        #endregion

        #region "LocalVariables"
        private EmployeeMasterFilter mFilter = new EmployeeMasterFilter();
        private MasterEmployee myDataTable = new MasterEmployee();
        private bool _isFillingMenuGroup; // optional reentrancy guard
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
                SplitRight.Panel2Collapsed = true;

                // Set SplitterDistance to 30% and 40% of the total width
                SplitMain.SplitterDistance = (int)(this.SplitMain.Width * 0.3);
                SplitData.SplitterDistance = (int)(this.SplitData.Height * 0.3);
                SplitRight.SplitterDistance = (int)(this.SplitMain.Width * 0.6);

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
            ClassLayoutHelper.ConfigureTableLayout(TableLayout2);
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

                // Load combo boxes with data
                this.CmbUnitCode.SelectedIndexChanged -= CmbUnitCode_SelectedIndexChanged;
                await ClassGlobalFunctions.FillComboBoxUnitCodeHasAccessAsync(
                   displayMember: "UnitCode",
                   valueMember: "Id",
                   comboBox: CmbUnitCode,
                   tableName: "Companies",
                   userRowGuid: ClassGlobalVariables.pubLoginUserRowGuid,
                   fallbackUnitCode: ClassGlobalVariables.pubUnitCode);
                this.CmbUnitCode.SelectedIndexChanged += CmbUnitCode_SelectedIndexChanged;

                await FillUnitCodeAsync();

                // Employee Type
                await ClassGlobalFunctions.FillComboForUnitCodeAsync(
                    "CatgName",
                    "Id",
                    CmbEmploymentType,
                    "v_Master_EmploymentTypes",
                    ClassGlobalVariables.pubUnitCode);

                CmbUnitCode.Text = ClassGlobalVariables.pubUnitCode;

                // Get selected UnitCode globally
                // DataBound item text (not value) is stored globally for use in other forms
                string defaultUnitCode = ClassGlobalVariables.pubUnitCode;
                bool foundUnitCode = false;
                foreach (var item in CmbUnitCode.Items)
                {
                    if (CmbUnitCode.GetItemText(item) == defaultUnitCode)
                    {
                        CmbUnitCode.SelectedItem = item;
                        foundUnitCode = true;
                        break;
                    }
                }

                if (!foundUnitCode)
                {
                    CmbUnitCode.SelectedIndex = -1; // not found in list
                }
                CmbUnitCode.Enabled = false;// disable to prevent changes

                // Get selected employee category globally
                // DataBound item text (not value) is stored globally for use in other forms
                string empCategory = ClassGlobalVariables.pubSelectedEmpCategoryText;
                bool foundEmploymentType = false;
                foreach (var item in CmbEmploymentType.Items)
                {
                    if (CmbEmploymentType.GetItemText(item) == empCategory)
                    {
                        CmbEmploymentType.SelectedItem = item;
                        foundEmploymentType = true;
                        break;
                    }
                }

                if (!foundEmploymentType)
                {
                    CmbEmploymentType.SelectedIndex = -1; // not found in list
                }
                CmbEmploymentType.Enabled = false;// disable to prevent changes

                await ClassGlobalFunctions.FillComboBoxAsync("GenderCode", "Id", CmbEmpBloodGroup, "Master_Genders", ClassGlobalVariables.pubUnitCode);
                await ClassGlobalFunctions.FillComboBoxAsync("MaritalStatus", "Id", CmbEmpMaritalStatus, "Master_MaritalStatus", ClassGlobalVariables.pubUnitCode);
                await ClassGlobalFunctions.FillComboBoxAsync("Name", "Id", CmbEmpBloodGroup, "Master_BloodGroups", ClassGlobalVariables.pubUnitCode);
                await ClassGlobalFunctions.FillComboBoxAsync("FullName", "Id", CmbWeekNames, "Master_WeekNames", ClassGlobalVariables.pubUnitCode);

                // Get SQL Server Today's date
                DateTime safeDate = ClassGlobalVariables.SqlServerTodayDate.HasValue
                    ? ClassGlobalVariables.SqlServerTodayDate.Value.Date
                    : DateTime.Today;

                // Format DatePicker
                ClassSafeValueHelpers.ConfigureDateTimePicker(DtPickEmpDOB, safeDate.AddYears(-18));
                ClassSafeValueHelpers.ConfigureDateTimePicker(DtPickDOJ, safeDate.AddDays(-5));

                // Retrieve XML from your existing DB helper
                await ReadUserLoginDefaultXmlAsync();

                // Set PropertyGrid for Filter
                PgridFilter.PropertySort = PropertySort.Categorized;
                PgridFilter.SelectedObject = mFilter;
                PgridFilter.ToolbarVisible = false;
                PgridFilter.CommandsVisibleIfAvailable = true;

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

        #region "CmbUnitCode_SelectedIndexChanged"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void CmbUnitCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isFillingMenuGroup) return; // avoid overlapping calls if user clicks fast
            _isFillingMenuGroup = true;
            try
            {

                string mUnitCode = (CmbUnitCode == null || string.IsNullOrWhiteSpace(CmbUnitCode.Text))
                   ? ClassGlobalVariables.pubUnitCode
                   : CmbUnitCode.Text;

                // Duty Location
                await ClassGlobalFunctions.FillComboForUnitCodeAsync(
                    "Name",
                    "Id",
                    CmbDutyLocation,
                    "Master_DutyLocations",
                    mUnitCode);

                // Department
                await ClassGlobalFunctions.FillComboForUnitCodeAsync(
                    "DepartmentCode",
                    "Id",
                    CmbDept,
                    "Master_Departments",
                    mUnitCode);

                // Designation
                await ClassGlobalFunctions.FillComboForUnitCodeAsync(
                    "Name",
                    "Id",
                    CmbDesig,
                    "Master_Designations",
                    mUnitCode);

                // Job Category
                await ClassGlobalFunctions.FillComboForUnitCodeAsync(
                    "Name",
                    "Id",
                    CmbJobCatg,
                    "Master_JobCategories",
                    mUnitCode);

                // Job Grade
                await ClassGlobalFunctions.FillComboForUnitCodeAsync(
                    "Name",
                    "Id",
                    CmbJobGrade,
                    "Master_GradeCodes",
                    mUnitCode);

                // Batch Code
                await ClassGlobalFunctions.FillComboForUnitCodeAsync(
                    "Name",
                    "Id",
                    CmbBatchCode,
                    "Master_BatchCodes",
                    mUnitCode);

                // Card Color
                await ClassGlobalFunctions.FillComboForUnitCodeAsync(
                    "Name",
                    "Id",
                    CmbCardColor,
                    "Master_CardColors",
                    mUnitCode);

                // Contractor Name
                await ClassGlobalFunctions.FillComboForUnitCodeAsync(
                    "ContractorName",
                    "Id",
                    CmbContractorName,
                    "Master_Contractors",
                    mUnitCode);


            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "CmbUnitCode_SelectedIndexChanged");
            }
            finally
            {
                _isFillingMenuGroup = false;
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

        #region "TsBtnOptClose_Click"
        private void TsBtnOptClose_Click(object sender, EventArgs e)
        {

            SplitRight.Panel2Collapsed = true;
        }
        #endregion

        #region "TsBtnFilterClose_Click"
        private void TsBtnFilterClose_Click(object sender, EventArgs e)
        {

            SplitMain.Panel1Collapsed = true;
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
                    "dbo.Master_Employees",
                    "dbo.usp_Master_Employees_Upsert",
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

        #region "ReadUserLoginDefaultXmlAsync"
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task ReadUserLoginDefaultXmlAsync()
        {
            try
            {
                string mUnitCode;
                if (TsBtnDrpUnitCode == null || string.IsNullOrWhiteSpace(TsBtnDrpUnitCode.Text) ||
                    string.Equals(TsBtnDrpUnitCode.Text, "(All)", StringComparison.Ordinal) ||
                    string.Equals(TsBtnDrpUnitCode.Text, "Plant Code", StringComparison.Ordinal))
                {
                    mUnitCode = ClassGlobalVariables.pubUnitCode;
                }
                else
                {
                    mUnitCode = TsBtnDrpUnitCode.Text;
                }

                var wanted = new[] { "PlantCode", "EmployeeCategory" };

                var vals = await UserLoginDefaultsHelper.GetAsync(
                    unitCode: mUnitCode,
                    userRowGuid: mUnitCode,
                    wantedColumns: wanted,
                    tableName: "dbo.UserLoginDefaults");

                string plantCode = vals["PlantCode"];        // always present (empty if missing)
                string empCat = vals["EmployeeCategory"]; // always present (empty if missing)

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
                ClassLayoutHelper.ClearErrorsTableLayout(TableLayout2, errorProvider1);

                ClassLayoutHelper.ClearControlsTableLayout(TableLayout1);
                ClassLayoutHelper.ClearControlsTableLayout(TableLayout2);

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
                ClassValidationHelper.ValidateControl(TxtEmpName, "Name is required.", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateControl(TxtEmpFatherName, "Father Name is required.", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateControl(CmbEmpGender, "Gender is required.", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateControl(CmbEmpMaritalStatus, "Marital Status is required.", errorProvider1, TsLblInputStatus);

                ClassValidationHelper.ValidateControl(CmbEmploymentType, "Employment Type is required.", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateControl(CmbDutyLocation, "Duty Location is required.", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateControl(CmbDept, "Department is required.", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateControl(CmbDesig, "Designation is required.", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateControl(CmbJobCatg, "Job Categorg is required.", errorProvider1, TsLblInputStatus);

                // Check if the checkbox is unchecked(i.e., date is not provided)
                if (!DtPickEmpDOB.Checked)
                {
                    throw new ArgumentException("Date of Birth is required.");
                }

                // ensure date-only
                // equal is allowed; only future is disallowed
                DateTime safeDate = ClassGlobalVariables.SqlServerTodayDate.HasValue
                    ? ClassGlobalVariables.SqlServerTodayDate.Value.Date
                    : DateTime.Today;

                DateTime birthDate = DtPickEmpDOB.Value.Date;

                // Total days lived
                double totalDays = (safeDate - birthDate).TotalDays;

                // Convert to years (365.25 accounts for leap years)
                double exactAge = totalDays / 365.25;

                // Validate Age(must be >= 18)
                if (exactAge < 18.0)
                {
                    string errorMessage = $"Age must be 18 years or above. Current age is {exactAge:F1} years.";
                    errorProvider1.SetError(DtPickEmpDOB, errorMessage);
                    throw new Exception(errorMessage);
                }

                if (!DtPickDOJ.Checked)
                {
                    throw new ArgumentException("Date of Joining is required.");
                }

                DateTime validDate = safeDate;
                DateTime joiningDate = DtPickDOJ.Value.Date;
                if (joiningDate > validDate)
                {
                    string errorMessage = "Joining Date cannot be in the future.";
                    errorProvider1.SetError(DtPickDOJ, errorMessage);
                    throw new Exception(errorMessage);
                }

                // Length validations
                ClassValidationHelper.ValidateTextBoxLength(TxtEmpCode, 20, "Employee Code", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateTextBoxLength(TxtEmpName, 50, "Name", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateTextBoxLength(TxtEmpFatherName, 50, "Father Name", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateTextBoxLength(TxtEmpLastName, 50, "Last Name", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateTextBoxLength(TxtEmpDisplayName, 100, "Display Name", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateTextBoxLength(TxtReportingMgrCode, 20, "Reporting Manager", errorProvider1, TsLblInputStatus);

                // Dropdown value validations
                int? selectedUnitId = ClassValidationHelper.ValidateCmbSelectedValue(CmbUnitCode, "Plant Code is required.", errorProvider1, TsLblInputStatus);
                int? selectedGenderId = ClassValidationHelper.ValidateCmbSelectedValue(CmbEmpGender, "Gender is required.", errorProvider1, TsLblInputStatus);
                int? selectedMaritalStatusId = ClassValidationHelper.ValidateCmbSelectedValue(CmbEmpMaritalStatus, "Marital Status is required.", errorProvider1, TsLblInputStatus);

                int? selectedBloodGroupId = null;
                if (!string.IsNullOrWhiteSpace(CmbEmpBloodGroup.Text))
                {
                    selectedBloodGroupId = ClassValidationHelper.ValidateCmbSelectedValue(CmbEmpBloodGroup, "Please select a valid Blood Group from the list.", errorProvider1, TsLblInputStatus);
                }

                int? selectedEmploymentTypeId = ClassValidationHelper.ValidateCmbSelectedValue(CmbEmploymentType, "Employment Type is required.", errorProvider1, TsLblInputStatus);
                int? selectedDutyLocationId = ClassValidationHelper.ValidateCmbSelectedValue(CmbDutyLocation, "Duty Location is required.", errorProvider1, TsLblInputStatus);
                int? selectedDeptId = ClassValidationHelper.ValidateCmbSelectedValue(CmbDept, "Department is required.", errorProvider1, TsLblInputStatus);
                int? selectedDesigId = ClassValidationHelper.ValidateCmbSelectedValue(CmbDesig, "Designation is required.", errorProvider1, TsLblInputStatus);
                int? selectedJobCatgId = ClassValidationHelper.ValidateCmbSelectedValue(CmbJobCatg, "Job Category is required.", errorProvider1, TsLblInputStatus);

                int? selectedWeekNameId = null;
                if (!string.IsNullOrWhiteSpace(CmbWeekNames.Text))
                {
                    selectedWeekNameId = ClassValidationHelper.ValidateCmbSelectedValue(CmbWeekNames, "Please select a valid Weekly Off Day from the list.", errorProvider1, TsLblInputStatus);
                }

                int? selectedJobGradeId = null;
                if (!string.IsNullOrWhiteSpace(CmbJobGrade.Text))
                {
                    selectedJobGradeId = ClassValidationHelper.ValidateCmbSelectedValue(CmbJobGrade, "Please select a valid Job Grade from the list.", errorProvider1, TsLblInputStatus);
                }

                int? selectedBatchCodeId = null;
                if (!string.IsNullOrWhiteSpace(CmbBatchCode.Text))
                {
                    selectedBatchCodeId = ClassValidationHelper.ValidateCmbSelectedValue(CmbBatchCode, "Please select a valid Batch Code from the list.", errorProvider1, TsLblInputStatus);
                }

                int? selectedCardColorId = null;
                if (!string.IsNullOrWhiteSpace(CmbBatchCode.Text))
                {
                    selectedCardColorId = ClassValidationHelper.ValidateCmbSelectedValue(CmbCardColor, "Please select a valid Card Color from the list.", errorProvider1, TsLblInputStatus);
                }

                int? selectedContractorId = null;
                if (!string.IsNullOrWhiteSpace(CmbContractorName.Text))
                {
                    selectedContractorId = ClassValidationHelper.ValidateCmbSelectedValue(CmbContractorName, "Please select a valid Contractor Name from the list.", errorProvider1, TsLblInputStatus);
                }

                string defaultUnitCode = CmbUnitCode.Text;

                string connectionString = ClassGlobalFunctions.GetConnectionStringByUnitCode(defaultUnitCode);
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new InvalidOperationException("Connection string cannot be empty");
                }

                // Populate data table
                myDataTable = new MasterEmployee();

                // Id
                // Set null as we want to insert new row
                myDataTable.Id = null;

                // UnitCode
                myDataTable.UnitCode = !string.IsNullOrEmpty(CmbUnitCode.Text)
                    ? CmbUnitCode.Text
                    : null;

                // EmployeeName
                myDataTable.EmployeeName = !string.IsNullOrEmpty(TxtEmpName.Text)
                    ? ClassStringHelpers.CleanAndUpperCase(TxtEmpName.Text)
                    : null;

                // FathersName
                myDataTable.FathersName = !string.IsNullOrEmpty(TxtEmpFatherName.Text)
                    ? ClassStringHelpers.CleanAndUpperCase(TxtEmpFatherName.Text)
                    : null;

                // LastName
                myDataTable.LastName = !string.IsNullOrEmpty(TxtEmpLastName.Text)
                    ? ClassStringHelpers.CleanAndUpperCase(TxtEmpLastName.Text)
                    : null;

                // EmployeeDisplayName
                myDataTable.EmployeeDisplayName = !string.IsNullOrEmpty(TxtEmpDisplayName.Text)
                    ? ClassStringHelpers.CleanAndUpperCase(TxtEmpDisplayName.Text)
                    : null;

                // DateOfBirth
                myDataTable.DateOfBirth = DtPickEmpDOB.Checked ? DtPickEmpDOB.Value.Date : (DateTime?)null;

                // DateOfJoining
                myDataTable.DateOfJoining = DtPickDOJ.Checked ? DtPickDOJ.Value.Date : (DateTime?)null;

                // EmpGenderId
                myDataTable.EmpGenderId = selectedGenderId;

                // EmployeeTypeId
                myDataTable.EmployeeTypeId = selectedEmploymentTypeId;

                // DutyLocationId
                myDataTable.DutyLocationId = selectedDutyLocationId;

                // DesignationId
                myDataTable.DesignationId = selectedDesigId;

                // DepartmentId
                myDataTable.DepartmentId = selectedDeptId;

                // JobCategoryId
                myDataTable.JobCategoryId = selectedJobCatgId;

                // GradeCodeId
                myDataTable.GradeCodeId = selectedJobGradeId;

                // BatchCodeId
                myDataTable.BatchCodeId = selectedBatchCodeId;

                // ContractorId
                myDataTable.ContractorId = selectedContractorId;

                // CardColorId
                myDataTable.CardColorId = selectedCardColorId;

                // MaritalStatusId
                myDataTable.MaritalStatusId = selectedMaritalStatusId;

                // WeeklyOffDayId
                myDataTable.WeeklyOffDayId = selectedWeekNameId;

                // BloodGroupId
                myDataTable.BloodGroupId = selectedBloodGroupId;

                // ReportingManagerCode
                myDataTable.ReportingManagerCode = !string.IsNullOrEmpty(TxtReportingMgrCode.Text)
                    ? ClassStringHelpers.CleanAndUpperCase(TxtReportingMgrCode.Text)
                    : null;

                // IPAddress
                myDataTable.IpAddsCreated = ClassGlobalVariables.pubHostIPAddress;

                // UserCreatedRowGuid
                myDataTable.UserRowGuid = ClassGlobalVariables.pubLoginUserRowGuid;

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


    }
}

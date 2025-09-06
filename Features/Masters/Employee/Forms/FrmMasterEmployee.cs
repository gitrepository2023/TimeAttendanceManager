using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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
            this.TsMenuViewInActive.Click -= TsMenuViewInActive_Click;
            this.TsMenuViewDeleted.Click -= TsMenuViewDeleted_Click;
            this.TsBtnAddNew.Click -= TsBtnAddNew_Click;
            this.TsBtnSave.Click -= TsBtnSave_Click;

            this.TsMenuViewInActive.Click -= TsMenuViewInActive_Click;
            this.TsMenuViewDeleted.Click -= TsMenuViewDeleted_Click;
            this.TsBtRefreshDgv.Click -= TsBtRefreshDgv_Click;
            this.TsBtnClearSearchDgv.Click -= TsBtRefreshDgv_Click;
            this.TsBtnSearchDgv.Click -= TsBtRefreshDgv_Click;
            this.TsTxtSearchDgv.KeyDown -= TsTxtSearchDgv_KeyDown;
            this.TsBtnDrpUnitCode.DropDownItemClicked -= TsBtnDrpBtn_DropDownItemClicked;
            this.DgvList.CellClick -= DgvList_CellClick;

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
            this.TsMenuViewInActive.Click += TsMenuViewInActive_Click;
            this.TsMenuViewDeleted.Click += TsMenuViewDeleted_Click;
            this.TsBtnAddNew.Click += TsBtnAddNew_Click;
            this.TsBtnSave.Click += TsBtnSave_Click;

            this.TsMenuViewInActive.Click += TsMenuViewInActive_Click;
            this.TsMenuViewDeleted.Click += TsMenuViewDeleted_Click;
            this.TsBtRefreshDgv.Click += TsBtRefreshDgv_Click;
            this.TsBtnClearSearchDgv.Click += TsBtRefreshDgv_Click;
            this.TsBtnSearchDgv.Click += TsBtRefreshDgv_Click;
            this.TsTxtSearchDgv.KeyDown += TsTxtSearchDgv_KeyDown;
            this.TsBtnDrpUnitCode.DropDownItemClicked += TsBtnDrpBtn_DropDownItemClicked;
            this.DgvList.CellClick += DgvList_CellClick;

        }
        #endregion

        #region "LocalVariables"
        private EmployeeMasterFilter mFilter = new EmployeeMasterFilter();
        private MasterEmployee myDataTable = new MasterEmployee();
        private bool _isFillingMenuGroup; // optional reentrancy guard
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

                await ClassGlobalFunctions.FillComboBoxAsync("CodeAndName", "Id", CmbEmpGender, "v_Master_Genders", defaultUnitCode);
                await ClassGlobalFunctions.FillComboBoxAsync("MaritalStatus", "Id", CmbEmpMaritalStatus, "Master_MaritalStatus", defaultUnitCode);
                await ClassGlobalFunctions.FillComboBoxAsync("Name", "Id", CmbEmpBloodGroup, "Master_BloodGroups", defaultUnitCode);
                await ClassGlobalFunctions.FillComboBoxAsync("FullName", "Id", CmbWeekNames, "Master_WeekNames", defaultUnitCode);
               
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

                // Load DataGridView
                await LoadDataGridViewAsync();

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
                    "CodeAndName",
                    "Id",
                    CmbDutyLocation,
                    "v_Master_DutyLocations",
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
                    "CodeAndName",
                    "Id",
                    CmbJobCatg,
                    "v_Master_JobCategories",
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
                    DgvList.ReadOnly = true;
                    DgvList.Visible = false;
                }));

                var invalidOptions = new[] { "(All)", "Plant Code" };
                if (TsBtnDrpUnitCode == null ||
                    string.IsNullOrWhiteSpace(TsBtnDrpUnitCode.Text) ||
                    invalidOptions.Contains(TsBtnDrpUnitCode.Text, StringComparer.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException("Please Select Plant Code from drop down.");
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
                    "UnitCodeAndAliasName",
                    "EmpCodeAndDisplayName",
                    "Gender",
                    "EmpTypeCatgName",
                    "DutyLocCodeAndName",
                    "DesigCode",
                    "DepartmentCode",
                    "JobCategoryCodeAndName",
                    "JobGradeCodeLevel",
                    "ContractorCodeAndName",
                    "MaritalStatus",
                    "WeeklyOffFullName",
                    "BloodGroupDesc"};

                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@UnitCode", SqlDbType.NVarChar) { Value = defaultUnitCode });

                // Get selected employee category globally
                // DataBound item text (not value) is stored globally for use in other forms
                int? empCategory = ClassGlobalVariables.pubSelectedEmpCategoryId;
                if (empCategory.HasValue)
                {
                    parameters.Add(new SqlParameter("@EmployeeTypeId", SqlDbType.Int) { Value = empCategory.Value });
                }

                // Determine the row limit based on TsCmbLimitRows input:
                // If the text is "(all)", set limit to 0
                // Otherwise, try parsing to int; if fails, default to 50
                // ?. ensures Text is not null before calling Trim()(null - conditional operator).
                int mLimitRows =
                    TsCmbLimitRows.Text?.Trim().ToLower() == "(all)" ? 0 :
                    int.TryParse(TsCmbLimitRows.Text, out var value) ? value : 50;

                string orderBy = "UnitCode, EmployeeCode, EmployeeName";
                string mTableName = "v_Master_Employees";

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
                    "UnitCodeAndAliasName",
                    "EmpTypeCatgName",
                    "EmployeeCode",
                    "EmployeeDisplayName",
                    "GenderCodeAndName",
                    "DepartmentCode",
                    "DesigCode",
                    "JobCategoryCode"};

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
                    DgvList.ReadOnly = true;
                    DgvList.Refresh();
                    DgvList.Visible = false;
                }));

                // Set data source
                DgvList.Invoke((MethodInvoker)(() => DgvList.DataSource = filteredTable));

                // Define and apply custom headers
                var customHeaders = new Dictionary<string, string>
                    {
                        {"Id", "Id"},
                        {"UnitCodeAndAliasName", "Plant"},
                        {"EmpTypeCatgName", "Employment Type"},
                        {"EmployeeCode", "Employee Code"},
                        {"EmployeeDisplayName", "Employee Name"},
                        {"GenderCodeAndName", "Gender"},
                        {"DepartmentCode", "Department"},
                        {"DesigCode", "Designation"}
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
                        DgvList.ReadOnly = true;
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

                string mTableName = "dbo.v_Master_Employees";

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
                TxtEmpCode.Text = ClassSafeValueHelpers.PubGetSafeValue(row["EmployeeCode"]);
                TxtEmpName.Text = ClassSafeValueHelpers.PubGetSafeValue(row["EmployeeName"]);
                TxtEmpFatherName.Text = ClassSafeValueHelpers.PubGetSafeValue(row["FathersName"]);
                TxtEmpLastName.Text = ClassSafeValueHelpers.PubGetSafeValue(row["LastName"]);
                TxtEmpDisplayName.Text = ClassSafeValueHelpers.PubGetSafeValue(row["EmployeeDisplayName"]);

                int? empGenderId = ClassSafeValueHelpers.PubGetSafeInteger(row["EmpGenderId"]);
                string genderCodeAndName = ClassSafeValueHelpers.PubGetSafeValue(row["GenderCodeAndName"]);
                if (empGenderId.HasValue)
                {
                    ClassSafeValueHelpers.SafeComboSelection(CmbEmpGender, empGenderId.Value, genderCodeAndName);
                }
                else
                {
                    CmbEmpGender.SelectedIndex = -1;
                    CmbEmpGender.Text = string.Empty;
                }

                int? maritalStatusId = ClassSafeValueHelpers.PubGetSafeInteger(row["MaritalStatusId"]);
                string maritalStatus = ClassSafeValueHelpers.PubGetSafeValue(row["MaritalStatus"]);
                if (maritalStatusId.HasValue)
                {
                    ClassSafeValueHelpers.SafeComboSelection(CmbEmpMaritalStatus, maritalStatusId.Value, maritalStatus);
                }
                else
                {
                    CmbEmpMaritalStatus.SelectedIndex = -1;
                    CmbEmpMaritalStatus.Text = string.Empty;
                }
               
                int? bloodGroupId = ClassSafeValueHelpers.PubGetSafeInteger(row["BloodGroupId"]);
                string bloodGroupName = ClassSafeValueHelpers.PubGetSafeValue(row["BloodGroupName"]);
                if (bloodGroupId.HasValue)
                {
                    ClassSafeValueHelpers.SafeComboSelection(CmbEmpBloodGroup, bloodGroupId.Value, bloodGroupName);
                }
                else
                {
                    CmbEmpBloodGroup.SelectedIndex = -1;
                    CmbEmpBloodGroup.Text = string.Empty;
                }
                
                int? employeeTypeId = ClassSafeValueHelpers.PubGetSafeInteger(row["EmployeeTypeId"]);
                string empTypeCatgName = ClassSafeValueHelpers.PubGetSafeValue(row["EmpTypeCatgName"]);
                if (bloodGroupId.HasValue)
                {
                    ClassSafeValueHelpers.SafeComboSelection(CmbEmploymentType, employeeTypeId.Value, empTypeCatgName);
                }
                else
                {
                    CmbEmploymentType.SelectedIndex = -1;
                    CmbEmploymentType.Text = string.Empty;
                }

                int? dutyLocationId = ClassSafeValueHelpers.PubGetSafeInteger(row["DutyLocationId"]);
                string dutyLocCodeAndName = ClassSafeValueHelpers.PubGetSafeValue(row["DutyLocCodeAndName"]);
                if (dutyLocationId.HasValue)
                {
                    ClassSafeValueHelpers.SafeComboSelection(CmbDutyLocation, dutyLocationId.Value, dutyLocCodeAndName);
                }
                else
                {
                    CmbDutyLocation.SelectedIndex = -1;
                    CmbDutyLocation.Text = string.Empty;
                }

                int? designationId = ClassSafeValueHelpers.PubGetSafeInteger(row["DesignationId"]);
                string desigCode = ClassSafeValueHelpers.PubGetSafeValue(row["DesigName"]);
                if (designationId.HasValue)
                {
                    ClassSafeValueHelpers.SafeComboSelection(CmbDesig, designationId.Value, desigCode);
                }
                else
                {
                    CmbDesig.SelectedIndex = -1;
                    CmbDesig.Text = string.Empty;
                }

                int? departmentId = ClassSafeValueHelpers.PubGetSafeInteger(row["DepartmentId"]);
                string departmentCode = ClassSafeValueHelpers.PubGetSafeValue(row["DepartmentCode"]);
                if (departmentId.HasValue)
                {
                    ClassSafeValueHelpers.SafeComboSelection(CmbDept, departmentId.Value, departmentCode);
                }
                else
                {
                    CmbDept.SelectedIndex = -1; // nothing selected
                    CmbDept.Text = string.Empty;
                }

                int? jobCategoryId = ClassSafeValueHelpers.PubGetSafeInteger(row["JobCategoryId"]);
                string jobCategoryCodeAndName = ClassSafeValueHelpers.PubGetSafeValue(row["JobCategoryCodeAndName"]);
                if (jobCategoryId.HasValue)
                {
                    ClassSafeValueHelpers.SafeComboSelection(CmbJobCatg, jobCategoryId.Value, jobCategoryCodeAndName);
                }
                else
                {
                    CmbJobCatg.SelectedIndex = -1; // nothing selected
                    CmbJobCatg.Text = string.Empty;
                }

                bool? isOffApplicable = ClassSafeValueHelpers.PubGetSafeBoolean(row["IsActive"]);
                if (isOffApplicable.HasValue && isOffApplicable.Value)
                {
                    ChkWoff.Checked = true;
                    ChkWoff.Text = "Yes";
                }
                else
                {
                    ChkWoff.Checked = false;
                    ChkWoff.Text = "No";
                }

                int? weeklyOffDayId = ClassSafeValueHelpers.PubGetSafeInteger(row["WeeklyOffDayId"]);
                string weeklyOffDay = ClassSafeValueHelpers.PubGetSafeValue(row["WeeklyOffFullName"]);
                if (weeklyOffDayId.HasValue)
                {
                   ClassSafeValueHelpers.SafeComboSelection(CmbWeekNames, weeklyOffDayId.Value, weeklyOffDay);
                }
                else
                {
                    CmbWeekNames.SelectedIndex = -1;
                    CmbWeekNames.Text = string.Empty;
                }

                int? gradeCodeId = ClassSafeValueHelpers.PubGetSafeInteger(row["GradeCodeId"]);
                string jobGradeCode = ClassSafeValueHelpers.PubGetSafeValue(row["JobGradeCode"]);
                if (gradeCodeId.HasValue)
                {
                    ClassSafeValueHelpers.SafeComboSelection(CmbJobGrade, gradeCodeId.Value, jobGradeCode);
                }
                else
                {
                    CmbJobGrade.SelectedIndex = -1; // nothing selected
                    CmbJobGrade.Text = string.Empty;
                }
                
                int? batchCodeId = ClassSafeValueHelpers.PubGetSafeInteger(row["BatchCodeId"]);
                string batchName = ClassSafeValueHelpers.PubGetSafeValue(row["BatchName"]);
                if (batchCodeId.HasValue)
                {
                    ClassSafeValueHelpers.SafeComboSelection(CmbBatchCode, batchCodeId.Value, batchName);
                }
                else
                {
                    CmbBatchCode.SelectedIndex = -1; // nothing selected
                    CmbBatchCode.Text = string.Empty;
                }
                
                int? cardColorId = ClassSafeValueHelpers.PubGetSafeInteger(row["CardColorId"]);
                string cardColorCode = ClassSafeValueHelpers.PubGetSafeValue(row["CardColorCode"]);
                if (cardColorId.HasValue)
                {
                    ClassSafeValueHelpers.SafeComboSelection(CmbCardColor, cardColorId.Value, cardColorCode);
                }
                else
                {
                    CmbCardColor.SelectedIndex = -1; // nothing selected
                    CmbCardColor.Text = string.Empty;
                }
                
                int? contractorId = ClassSafeValueHelpers.PubGetSafeInteger(row["ContractorId"]);
                string contractorName = ClassSafeValueHelpers.PubGetSafeValue(row["ContractorName"]);
                if (contractorId.HasValue)
                {
                    ClassSafeValueHelpers.SafeComboSelection(CmbContractorName, contractorId.Value, contractorName);
                }
                else
                {
                    CmbContractorName.SelectedIndex = -1; // nothing selected
                    CmbContractorName.Text = string.Empty;
                }
                
                                
                TxtReportingMgrCode.Text = ClassSafeValueHelpers.PubGetSafeValue(row["ReportingManagerCode"]);
                
                // Nullable DateTime (because our helper returns DateTime?)
                DateTime? dateOfBirth = ClassSafeValueHelpers.PubGetSafeDate(row["DateOfBirth"]);
                if (dateOfBirth.HasValue)
                {
                    DtPickEmpDOB.Checked = true;
                    DtPickEmpDOB.Value = dateOfBirth.Value;
                }
                else
                {
                    DtPickEmpDOB.Checked = false;
                }

                DateTime? dateOfJoining = ClassSafeValueHelpers.PubGetSafeDate(row["DateOfJoining"]);
                if (dateOfJoining.HasValue)
                {
                    DtPickDOJ.Checked = true;
                    DtPickDOJ.Value = dateOfJoining.Value;
                }
                else
                {
                    DtPickDOJ.Checked = false;
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

        #region "TsBtnAddNew_Click"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsBtnAddNew_Click(object sender, EventArgs e)
        {
            ClearInputs();

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

                const string tableName = "dbo.Master_Employees";
                const string sqlProcedureName = "dbo.usp_Master_Employees_Insert";

                string defaultUnitCode = CmbUnitCode.Text;

                string connectionString = ClassGlobalFunctions.GetConnectionStringByUnitCode(defaultUnitCode);
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new InvalidOperationException("Connection string cannot be empty");
                }

                var parameters = new List<SqlParameter>
                {
                    ClassDbHelpers.CreateSqlParameter("@UnitCode", SqlDbType.VarChar, myDataTable.UnitCode),
                    ClassDbHelpers.CreateSqlParameter("@EmployeeName", SqlDbType.NVarChar, myDataTable.EmployeeName),
                    ClassDbHelpers.CreateSqlParameter("@FathersName", SqlDbType.NVarChar, myDataTable.FathersName),
                    ClassDbHelpers.CreateSqlParameter("@LastName", SqlDbType.NVarChar, myDataTable.LastName),
                    ClassDbHelpers.CreateSqlParameter("@EmployeeDisplayName", SqlDbType.NVarChar, myDataTable.EmployeeDisplayName),

                    ClassDbHelpers.CreateSqlParameter("@DateOfBirth", SqlDbType.Date, myDataTable.DateOfBirth),
                    ClassDbHelpers.CreateSqlParameter("@DateOfJoining", SqlDbType.Date, myDataTable.DateOfJoining),

                    ClassDbHelpers.CreateSqlParameter("@EmpGenderId", SqlDbType.Int, myDataTable.EmpGenderId),
                    ClassDbHelpers.CreateSqlParameter("@EmployeeTypeId", SqlDbType.Int, myDataTable.EmployeeTypeId),
                    ClassDbHelpers.CreateSqlParameter("@DutyLocationId", SqlDbType.Int, myDataTable.DutyLocationId),
                    ClassDbHelpers.CreateSqlParameter("@DesignationId", SqlDbType.Int, myDataTable.DesignationId),
                    ClassDbHelpers.CreateSqlParameter("@DepartmentId", SqlDbType.Int, myDataTable.DepartmentId),
                    ClassDbHelpers.CreateSqlParameter("@JobCategoryId", SqlDbType.Int, myDataTable.JobCategoryId),
                    ClassDbHelpers.CreateSqlParameter("@GradeCodeId", SqlDbType.Int, myDataTable.GradeCodeId),
                    ClassDbHelpers.CreateSqlParameter("@BatchCodeId", SqlDbType.Int, myDataTable.BatchCodeId),
                    ClassDbHelpers.CreateSqlParameter("@ContractorId", SqlDbType.Int, myDataTable.ContractorId),
                    ClassDbHelpers.CreateSqlParameter("@CardColorId", SqlDbType.Int, myDataTable.CardColorId),
                    ClassDbHelpers.CreateSqlParameter("@MaritalStatusId", SqlDbType.Int, myDataTable.MaritalStatusId),
                    ClassDbHelpers.CreateSqlParameter("@IsWeeklyOffApplicable", SqlDbType.Bit, myDataTable.IsWeeklyOffApplicable),
                    ClassDbHelpers.CreateSqlParameter("@WeeklyOffDayId", SqlDbType.Int, myDataTable.WeeklyOffDayId),
                    ClassDbHelpers.CreateSqlParameter("@BloodGroupId", SqlDbType.Int, myDataTable.BloodGroupId),

                    ClassDbHelpers.CreateSqlParameter("@ReportingManagerCode", SqlDbType.NVarChar, myDataTable.ReportingManagerCode),

                    ClassDbHelpers.CreateSqlParameter("@RowId", SqlDbType.Int, myDataTable.Id),
                    ClassDbHelpers.CreateSqlParameter("@RowVersion", SqlDbType.Int, myDataTable.RowVersion),
                   
                    ClassDbHelpers.CreateSqlParameter("@UserId", SqlDbType.Int, myDataTable.UserId),
                    ClassDbHelpers.CreateSqlParameter("@UserRowGuid", SqlDbType.NVarChar, myDataTable.UserRowGuid.ToString()),
                    ClassDbHelpers.CreateSqlParameter("@IpAddsCreated", SqlDbType.VarChar, myDataTable.IpAddsCreated),
                    ClassDbHelpers.CreateSqlParameter("@HostName", SqlDbType.VarChar, myDataTable.HostName),
                   
                };

                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync().ConfigureAwait(false);

                    using (var command = new SqlCommand(sqlProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddRange(parameters.ToArray());

                        // Add output parameter for NewId
                        var newIdParam = new SqlParameter("@NewId", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(newIdParam);

                        // Add output parameter for NewTicketNo
                        var newTicketNoParam = new SqlParameter("@NewTicketNo", SqlDbType.NVarChar, 20)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(newTicketNoParam);

                        // Add output parameter for success status
                        var successParam = new SqlParameter("@Success", SqlDbType.Bit)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(successParam);

                        // Execute the command
                        await command.ExecuteNonQueryAsync().ConfigureAwait(false);

                        string newTicketNo = newTicketNoParam.Value as string;

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

                TsTxtRecordId.Text = string.Empty;
                TsTxtRecordId.Tag = null;

                TsLblInputStatus.Text = "...";

                // set focus
                TxtEmpName.Focus();
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

                await Task.Delay(TimeSpan.FromSeconds(0.5)); // 0.5 seconds

                // Remove existing errors
                ClassLayoutHelper.ClearErrorsTableLayout(TableLayout1, errorProvider1);
                ClassLayoutHelper.ClearErrorsTableLayout(TableLayout2, errorProvider1);

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
                //if (exactAge < 18.0)
                //{
                //    string errorMessage = $"Age must be 18 years or above. Current age is {exactAge:F1} years.";
                //    errorProvider1.SetError(DtPickEmpDOB, errorMessage);
                //    throw new Exception(errorMessage);
                //}

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

                // Joining date must be after birth date
                if (joiningDate <= birthDate)
                {
                    string errorMessage = "Joining Date must be after Date of Birth.";
                    errorProvider1.SetError(DtPickDOJ, errorMessage);
                    throw new Exception(errorMessage);
                }

                // Employee must be at least 18 years old at joining
                //int ageAtJoining = joiningDate.Year - birthDate.Year;
                //if (joiningDate < birthDate.AddYears(ageAtJoining))
                //{
                //    ageAtJoining--; // Adjust if birthday hasn’t occurred yet in joining year
                //}

                //if (ageAtJoining < 18)
                //{
                //    string errorMessage = "Employee must be at least 18 years old at the time of joining.";
                //    errorProvider1.SetError(DtPickDOJ, errorMessage);
                //    throw new Exception(errorMessage);
                //}

                // Length validations
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

                // Store weekly off applicability
                bool isWeeklyOffApplicable = ChkWoff.Checked;
                int? selectedWeekNameId = null;
                if (isWeeklyOffApplicable)
                {
                    if (CmbWeekNames.SelectedIndex >= 0 && CmbWeekNames.SelectedValue != null)
                    {
                        selectedWeekNameId = ClassValidationHelper.ValidateCmbSelectedValue(
                            CmbWeekNames,
                            "Please select a valid Weekly Off Day from the list.",
                            errorProvider1,
                            TsLblInputStatus
                        );
                    }
                    else
                    {
                        string errorMessage = "Weekly Off is applicable, but no day has been selected.";
                        errorProvider1.SetError(CmbWeekNames, errorMessage);
                        throw new Exception(errorMessage);
                    }
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
                if (!string.IsNullOrWhiteSpace(CmbCardColor.Text))
                {
                    selectedCardColorId = ClassValidationHelper.ValidateCmbSelectedValue(CmbCardColor, "Please select a valid Card Color from the list.", errorProvider1, TsLblInputStatus);
                }

                int? selectedContractorId = null;
                if (!string.IsNullOrWhiteSpace(CmbContractorName.Text))
                {
                    selectedContractorId = ClassValidationHelper.ValidateCmbSelectedValue(CmbContractorName, "Please select a valid Contractor Name from the list.", errorProvider1, TsLblInputStatus);
                }

                // get the valid selected RowId
                int? rowId = null;
                if (!string.IsNullOrWhiteSpace(TsTxtRecordId.Text))
                {
                    if (!int.TryParse(TsTxtRecordId.Text.Trim(), out int parsedId))
                    {
                        string errorMessage = "Invalid Employee selected.";
                        errorProvider1.SetError(TxtEmpCode, errorMessage);
                        throw new ArgumentException(errorMessage);
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
                        string errorMessage = "Invalid Employee selected.";
                        errorProvider1.SetError(TxtEmpCode, errorMessage);
                        throw new ArgumentException(errorMessage);
                    }

                    rowVersion = parsedId; // Assign the parsed value
                }

                // Validation complete - prepare data object
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
                myDataTable.Id = rowId;
                myDataTable.RowVersion = rowVersion;

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
                myDataTable.DateOfBirth = DtPickEmpDOB.Checked ? birthDate.Date : (DateTime?)null;

                // DateOfJoining
                myDataTable.DateOfJoining = DtPickDOJ.Checked ? joiningDate.Date : (DateTime?)null;

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

                // IsWeeklyOffApplicable
                myDataTable.IsWeeklyOffApplicable = isWeeklyOffApplicable;

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
                TsLblInputStatus.ForeColor = Color.Red;
                MessageBox.Show(ex.Message, "Validate Input", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        #endregion


    }
}

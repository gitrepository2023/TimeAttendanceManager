using TimeAttendanceManager.Auth.Classes;
using TimeAttendanceManager.Features.Masters.Departments.Models;
using TimeAttendanceManager.Helpers.Classes;
using TimeAttendanceManager.Main.Classes;
using TimeAttendanceManager.Main.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeAttendanceManager.Features.Masters.Departments.Forms
{
    public partial class FrmDepartmentList : Form
    {

        #region "Constructor"
        public FrmDepartmentList()
        {
            InitializeComponent();

            // Remove events
            this.Load -= Form_Load;
            this.FormClosing -= FormClosing_FormClosing;
            this.TsBtnCloseForm.Click -= TsBtnCloseForm_Click;
            this.TsBtnClose.Click -= TsBtnCloseForm_Click;
            this.TabCtrlMain.Click -= TabCtrlMain_Click;
            this.TsBtnHelp.Click -= TsBtnHelp_Click;
            this.TsBtnOptTech.Click -= TsBtnOptTech_Click;
            this.TsBtnFilterClose.Click -= TsBtnFilterClose_Click;
            this.TsMenuViewInActive.Click -= TsMenuViewInActive_Click;
            this.TsBtRefreshDgv.Click -= TsBtRefreshDgv_Click;
            this.TsBtnClearSearchDgv.Click -= TsBtRefreshDgv_Click;
            this.TsBtnSearchDgv.Click -= TsBtRefreshDgv_Click;
            this.TsTxtSearchDgv.KeyDown -= TsTxtSearchDgv_KeyDown;
            this.TsBtnDrpUnitCode.DropDownItemClicked -= TsBtnDrpBtn_DropDownItemClicked;
            this.DgvList.CellClick -= DgvList_CellClick;
            this.DgvList.CellDoubleClick -= DgvList_CellDoubleClick;
            this.TsBtnAddNew.Click -= TsBtnAddNew_Click;
            this.TsBtnSave.Click -= TsBtnSave_Click;
            this.TsBtnDelete.Click -= TsBtnDelete_Click;
            this.TsBtnExport.Click -= TsBtnExport_Click;

            // Add events
            this.Load += Form_Load;
            this.FormClosing += FormClosing_FormClosing;
            this.TsBtnCloseForm.Click += TsBtnCloseForm_Click;
            this.TsBtnClose.Click += TsBtnCloseForm_Click;
            this.TabCtrlMain.Click += TabCtrlMain_Click;
            this.TsBtnHelp.Click += TsBtnHelp_Click;
            this.TsBtnOptTech.Click += TsBtnOptTech_Click;
            this.TsBtnFilterClose.Click += TsBtnFilterClose_Click;
            this.TsMenuViewInActive.Click += TsMenuViewInActive_Click;
            this.TsBtRefreshDgv.Click += TsBtRefreshDgv_Click;
            this.TsBtnClearSearchDgv.Click += TsBtRefreshDgv_Click;
            this.TsBtnSearchDgv.Click += TsBtRefreshDgv_Click;
            this.TsTxtSearchDgv.KeyDown += TsTxtSearchDgv_KeyDown;
            this.TsBtnDrpUnitCode.DropDownItemClicked += TsBtnDrpBtn_DropDownItemClicked;
            this.DgvList.CellClick += DgvList_CellClick;
            this.DgvList.CellDoubleClick += DgvList_CellDoubleClick;
            this.TsBtnAddNew.Click += TsBtnAddNew_Click;
            this.TsBtnSave.Click += TsBtnSave_Click;
            this.TsBtnDelete.Click += TsBtnDelete_Click;
            this.TsBtnExport.Click += TsBtnExport_Click;

        }
        #endregion

        #region "LocalVariables"
        private Department myDataTable = new Department();
        private ContextMenuStrip _cmsDgvList;
        bool? isActiveRows = true;
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
            // ClassLayoutHelper.ConfigureTableLayout(TableLayout1);
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

        #region "SetDefaultValuesAsync"

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task SetDefaultValuesAsync()
        {
            try
            {
                // Load ComboBox
                await FillUnitCodeAsync();

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

        #region "AddHelpText"
        private void AddHelpText()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("MANAGE DEPARTMENTS");
            sb.AppendLine();
            sb.AppendLine("Follow these steps to create a SHIFT SCHEDULE:");
            sb.AppendLine();

            // 1. BASIC DATA TAB
            sb.AppendLine("1. BASIC DATA TAB");
            sb.AppendLine("   - Select your Plant Code from the dropdown list");
            sb.AppendLine("   - To add a new Department, click on the 'Add New' button");
            sb.AppendLine("   - Fill in all required details:");
            sb.AppendLine("     • Plant Code (unique identifier)");
            sb.AppendLine("     • Department Code");
            sb.AppendLine("     • Department Name");
            sb.AppendLine();

            // 2. SAVING THE SHIFT
            sb.AppendLine("2. SAVING THE ENTRY");
            sb.AppendLine("   - Click the [Save] button after completing all fields");
            sb.AppendLine("   - System will validate all entries for:");
            sb.AppendLine("     • Required fields completeness");
            sb.AppendLine("   - Upon successful validation, data will be saved to database");
            sb.AppendLine("   - Success confirmation message will be displayed");
            sb.AppendLine();

            // 3. UPDATE THE SHIFT
            sb.AppendLine("3. UPDATE EXISTING ROW");
            sb.AppendLine("   - Select the row from the Data Grid by double clicking");
            sb.AppendLine("   - Selected data will be populated in the Form view");
            sb.AppendLine("   - Make necessary changes");
            sb.AppendLine("   - Click the [Save] button to update the record");
            sb.AppendLine("   - System will validate changes before updating");
            sb.AppendLine();

            // 4. SOFT DELETE SHIFT
            sb.AppendLine("4. DELETE (Soft Delete)");
            sb.AppendLine("   - Select the row from the Data Grid");
            sb.AppendLine("   - Data will be populated in the Form view for confirmation");
            sb.AppendLine("   - Click the [Delete] button");
            sb.AppendLine("   - Confirm deletion in the prompt dialog");
            sb.AppendLine("   - Shift will be marked as inactive (not physically deleted)");
            sb.AppendLine("   - Deleted rows can be restored if needed");
            sb.AppendLine();

            // 5. LIST ALL SHIFTS
            sb.AppendLine("5. VIEW AND FILTER");
            sb.AppendLine("   - Select the 'List View' tab");
            sb.AppendLine("   - Set filter parameters if needed:");
            sb.AppendLine("     • Plant Code");
            sb.AppendLine("     • Status (Active/Inactive)");
            sb.AppendLine("     • Date range");
            sb.AppendLine("   - Click the [Refresh List] button");
            sb.AppendLine("   - All matching shifts will be displayed in the grid");
            sb.AppendLine("   - Use column sorting by clicking column headers");
            sb.AppendLine();

            // 6. ADDITIONAL FEATURES
            sb.AppendLine("6. ADDITIONAL FEATURES");
            sb.AppendLine("   - Export list to Excel/CSV format");
            sb.AppendLine("   - Print reports");
            sb.AppendLine("   - Copy existing row as template for new plant code");
            sb.AppendLine("   - Bulk operations for multiple rows");
            sb.AppendLine();

            // 7. TROUBLESHOOTING
            sb.AppendLine("7. TROUBLESHOOTING");
            sb.AppendLine("   - If save fails: Check all required fields are completed");
            sb.AppendLine("   - If validation fails: Ensure all required fields have a value");
            sb.AppendLine("   - If duplicate error: Code must be unique per plant");
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

            SplitMain.Panel2Collapsed = true;
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
                SplitMain.Panel2Collapsed = !SplitMain.Panel2Collapsed;

                // Build the tech details object
                var mTechDetails = new ClassTechDetails
                {
                    // Set form identity
                    FormName = this.Name,
                    FormTitle = this.Text,

                    // Set default "created" date (VB had FormatDateTime("09-MAY-25", ShortDate))
                    // Here we parse explicitly as dd-MMM-yy using invariant culture.
                    FormCreated = DateTime.ParseExact("29-AUG-2025", "dd-MMM-yyyy", CultureInfo.InvariantCulture)
                };

                // Add table names used in this form (9 fields as in your VB)
                mTechDetails.TableNames.Add(new TableNames(
                    "dbo.Master_Departments",
                    "dbo.usp_Master_Departments_Upsert",
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

        #region "InitDgvListContextMenu"
        private void InitDgvListContextMenu()
        {
            _cmsDgvList = new ContextMenuStrip();

            var miEditSelectedRow = new ToolStripMenuItem("Edit Selected Row") { Name = "cmsEditSelectedRow" };
            var miAddNewRow = new ToolStripMenuItem("Add New Row") { Name = "cmsAddNewRow" };
            var miPasteRows = new ToolStripMenuItem("Paste Rows From Excel") { Name = "cmsPasteRows" };
            var miIsActive = new ToolStripMenuItem("Make Row In-Active") { Name = "cmsIsActive" };
            var miCancel = new ToolStripMenuItem("Cancel") { Name = "cmsCancel" };

            miEditSelectedRow.Click += CmsEditSelectedRow_Click;  // attach a handler
            miAddNewRow.Click += CmsAddNewRow_Click;
            miPasteRows.Click += CmsPasteRows_Click;
            miIsActive.Click += CmsIsActiveRows_Click;
            miCancel.Click += (s, e) => { /* no-op */ };

            _cmsDgvList.Items.Add(miEditSelectedRow);
            _cmsDgvList.Items.Add(miAddNewRow);
            _cmsDgvList.Items.Add(miPasteRows);
            _cmsDgvList.Items.Add(miIsActive);
            _cmsDgvList.Items.Add(new ToolStripSeparator());
            _cmsDgvList.Items.Add(miCancel);

            DgvList.ContextMenuStrip = _cmsDgvList;
        }
        #endregion

        #region "CmsEditSelectedRow_Click"
        private async void CmsEditSelectedRow_Click(object sender, EventArgs e)
        {
            try
            {
                var row = GetSelectedRowOrNull();
                if (row == null) { MessageBox.Show("Please select a row."); return; }

                var invalidOptions = new[] { "(All)", "Plant Code" };
                if (TsBtnDrpUnitCode == null ||
                    string.IsNullOrWhiteSpace(TsBtnDrpUnitCode.Text) ||
                    invalidOptions.Contains(TsBtnDrpUnitCode.Text, StringComparer.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException("Please Select Plant Code from drop down.");
                }
                string defaultUnitCode = TsBtnDrpUnitCode.Text;

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

                // open form
                OpenDepartmentEditForm(currentRowId.Value);
            }
            catch (Exception ex)
            {
                TsLblInputStatus.Text = ex.Message;
                TsLblInputStatus.ForeColor = Color.Red;
                MessageBox.Show(ex.Message, "Edit Row", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }
        private DataGridViewRow GetSelectedRowOrNull()
        {
            return DgvList.SelectedRows.Count > 0 ? DgvList.SelectedRows[0] :
                   (DgvList.CurrentRow != null ? DgvList.CurrentRow : null);
        }
        #endregion

        #region "CmsAddNewRow_Click"
        private void CmsAddNewRow_Click(object sender, EventArgs e)
        {
            TsBtnAddNew_Click(null, null);
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
                TsLblInputStatus.Text = "Fetching Rows. Please wait...";
                TsLblInputStatus.ForeColor = Color.DarkBlue;
                Application.DoEvents();

                // Prepare DataGridView
                DgvList.Invoke((MethodInvoker)(() =>
                {
                    DgvList.DataSource = null;
                    DgvList.Rows.Clear();
                    DgvList.Columns.Clear();
                    DgvList.Refresh();
                    DgvList.Visible = false;
                    DgvList.ReadOnly = true;
                    DgvList.ContextMenuStrip = _cmsDgvList;
                }));

                if (TsBtnDrpUnitCode == null ||
                    string.IsNullOrWhiteSpace(TsBtnDrpUnitCode.Text) ||
                    TsBtnDrpUnitCode.Text.Equals("(All)", StringComparison.OrdinalIgnoreCase) ||
                    TsBtnDrpUnitCode.Text.Equals("Plant Code", StringComparison.OrdinalIgnoreCase))
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
                    "DepartmentCode",
                    "DepartmentName"};

                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@UnitCode", SqlDbType.NVarChar) { Value = defaultUnitCode });

                // Determine the row limit based on TsCmbLimitRows input:
                // If the text is "(all)", set limit to 0
                // Otherwise, try parsing to int; if fails, default to 50
                // ?. ensures Text is not null before calling Trim()(null - conditional operator).
                int mLimitRows =
                    TsCmbLimitRows.Text?.Trim().ToLower() == "(all)" ? 0 :
                    int.TryParse(TsCmbLimitRows.Text, out var value) ? value : 50;

                string orderBy = "UnitCode, DepartmentCode, DepartmentName";
                string mTableName = "Master_Departments";

                // Get data asynchronously
                DataTable resultTable = await ClassDbHelpers.GetRowsFromTableAsync(
                    unitCode: defaultUnitCode,  // plant code
                    tableName: mTableName, // sql table name
                    selectedColumns: null,   // selectedColumns
                    serachColumns: serachColumns,  // search sql table columns
                    additionalParameters: parameters, // sql parameters
                    searchText: searchText, // search text
                    orderByClause: orderBy,    // sql order by columns
                    isDeletedColumn: null, // table column name for IsDeleted
                    isDeletedValue: null, // isDeletedValue
                    isActiveColumn: "IsActive", // table column name for IsActive
                    isActiveValue: isActiveRows, // isActiveValue
                    limitRows: mLimitRows);

                // Check for empty results
                if (resultTable == null || resultTable.Rows.Count == 0)
                {
                    TsLblInputStatus.Text = "No rows found for given parameters.";
                    return;
                }

                // Create a filtered DataTable with only the columns we want
                DataTable filteredTable = new DataTable();
                var columnsToInclude = new List<string> {
                    "Id",
                    "UnitCode",
                    "DepartmentCode",
                    "DepartmentName",
                    "IsActive"};

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
                    DgvList.Refresh();
                    DgvList.ReadOnly = true;
                    DgvList.Visible = false;
                }));

                // Set data source
                DgvList.Invoke((MethodInvoker)(() => DgvList.DataSource = filteredTable));

                // Define and apply custom headers
                var customHeaders = new Dictionary<string, string>
                    {
                        {"Id", "Id"},
                        {"UnitCode", "Plant"},
                        {"DepartmentCode", "Code"},
                        {"DepartmentName", "Department Name"},
                        {"IsActive", "Is Active"}
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
                TsLblInputStatus.Text = $"Fetch Rows [ {resultTable.Rows.Count} ] In: {elapsedTime:hh\\:mm\\:ss}";
                TsLblInputStatus.ForeColor = Color.DarkBlue;
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                TsLblInputStatus.Text = ex.Message;
                TsLblInputStatus.ForeColor = Color.Red;
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


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DataGrid Click", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region "DgvList_CellDoubleClick"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // make sure it's not header
            {
                OpenDepartmentEditForm(e.RowIndex);
            }
        }

        #endregion

        #region "OpenDepartmentEditForm"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowIndex"></param>
        private async void OpenDepartmentEditForm(int rowIndex)
        {
            // Get the cell values first
            var idCell = DgvList.Rows[rowIndex].Cells["Id"];
            var unitCodeCell = DgvList.Rows[rowIndex].Cells["UnitCode"];

            // Handle int? with proper null checking
            int? rowId = null;
            if (idCell.Value != null && int.TryParse(idCell.Value.ToString(), out int parsedId))
            {
                rowId = parsedId;
            }

            // Handle string with null coalescing
            string unitCode = unitCodeCell.Value?.ToString() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(unitCode))
            {
                throw new InvalidOperationException("Invalid Plant Code.");
            }

            using (var frm = new FrmDepartmentEditForm(unitCode, rowId))
            {
                var result = frm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    await LoadDataGridViewAsync(); // refresh after save
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

            try
            {
                if (TsBtnDrpUnitCode == null ||
                                string.IsNullOrWhiteSpace(TsBtnDrpUnitCode.Text) ||
                                TsBtnDrpUnitCode.Text.Equals("(All)", StringComparison.OrdinalIgnoreCase) ||
                                TsBtnDrpUnitCode.Text.Equals("Plant Code", StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException("Please Select Plant Code from drop down.");
                }

                string defaultUnitCode = TsBtnDrpUnitCode.Text;

                using (var frm = new FrmDepartmentEditForm(defaultUnitCode, null))
                {
                    var result = frm.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        LoadDataGridViewAsync(); // refresh after save
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        // optional: do nothing
                    }
                }
            }
            catch (Exception ex)
            {
                TsLblInputStatus.Text = ex.Message;
                TsLblInputStatus.ForeColor = Color.Red;
                MessageBox.Show(ex.Message, "Add New", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                tableName: "Master_Departments",
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


        #region "CmsPasteRows_Click"
        private void CmsPasteRows_Click(object sender, EventArgs e)
        {
            try
            {
                string clipboardText = Clipboard.GetText();

                if (string.IsNullOrWhiteSpace(clipboardText))
                {
                    TsLblInputStatus.Text = "Clipboard is empty.";
                    TsLblInputStatus.ForeColor = Color.Red;
                    return;
                }

                var sb = new StringBuilder("PASTE ROWS FROM EXCEL");
                sb.AppendLine("");
                sb.AppendLine(" - Please ensure columns are in order");
                sb.AppendLine(" - Plant Code, Department Code, Department Name");
                sb.AppendLine(" - Do not paste duplicate rows");
                sb.AppendLine(" - No rows will be commited");
                sb.AppendLine(" - If duplicate rows are found in pasted list.");
                sb.AppendLine(" - If duplicate rows exists in table.");

                var message = sb.ToString();

                var msgResponse = MessageBox.Show(message, "Confirm Paste",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (msgResponse == DialogResult.No)
                    return;

                // Prepare DataGridView
                PrepareDataGridView();

                // Split lines (rows)
                string[] lines = clipboardText.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                int startRow = DgvList.CurrentCell != null ? DgvList.CurrentCell.RowIndex : 0;

                foreach (string line in lines)
                {
                    string[] values = line.Split('\t'); // Excel cells are tab-separated

                    // Ensure DataGridView has enough rows
                    if (startRow >= DgvList.Rows.Count - 1)
                        DgvList.Rows.Add();

                    DataGridViewRow row = DgvList.Rows[startRow];

                    // 🔹 Map Excel columns → SQL table columns
                    // Assuming Excel order: UnitCode | DepartmentCode | DepartmentName
                    if (values.Length > 0) row.Cells["UnitCode"].Value = values[0].Trim();
                    if (values.Length > 1) row.Cells["DepartmentCode"].Value = values[1].Trim();
                    if (values.Length > 2) row.Cells["DepartmentName"].Value = values[2].Trim();

                    startRow++;
                }

                TsBtnSave.Enabled = true;

                TsLblInputStatus.Text = $"Pasted {lines.Length} rows from Excel.";
                TsLblInputStatus.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                TsLblInputStatus.Text = ex.Message;
                TsLblInputStatus.ForeColor = Color.Red;
                MessageBox.Show(ex.Message, "Paste Rows", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region "PrepareDataGridView"
        private void PrepareDataGridView()
        {

            // Prepare DataGridView
            DgvList.Invoke((MethodInvoker)(() =>
            {
                DgvList.DataSource = null;
                DgvList.Rows.Clear();
                DgvList.Columns.Clear();
                DgvList.Refresh();
                DgvList.Visible = true;
                DgvList.ReadOnly = false;
                DgvList.AllowUserToAddRows = true;
                DgvList.AllowUserToDeleteRows = true;
                DgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }));

            // Add UnitCode column
            DataGridViewTextBoxColumn colUnit = new DataGridViewTextBoxColumn
            {
                Name = "UnitCode",
                HeaderText = "Plant Code",
                MaxInputLength = 50
            };
            DgvList.Columns.Add(colUnit);

            // Add DepartmentCode column
            DataGridViewTextBoxColumn colDeptCode = new DataGridViewTextBoxColumn
            {
                Name = "DepartmentCode",
                HeaderText = "Department Code",
                MaxInputLength = 50
            };
            DgvList.Columns.Add(colDeptCode);

            // Add DepartmentName column
            DataGridViewTextBoxColumn colDeptName = new DataGridViewTextBoxColumn
            {
                Name = "DepartmentName",
                HeaderText = "Department Name",
                MaxInputLength = 100
            };
            DgvList.Columns.Add(colDeptName);

        }

        #endregion

        #region "GetDepartmentsTableFromGrid"
        private DataTable GetDepartmentsTableFromGrid()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("UnitCode", typeof(string));
            dt.Columns.Add("DepartmentCode", typeof(string));
            dt.Columns.Add("DepartmentName", typeof(string));

            foreach (DataGridViewRow row in DgvList.Rows)
            {
                if (row.IsNewRow) continue;

                string unitCode = row.Cells["UnitCode"].Value?.ToString();
                string deptCode = row.Cells["DepartmentCode"].Value?.ToString();
                string deptName = row.Cells["DepartmentName"].Value?.ToString();

                if (!string.IsNullOrWhiteSpace(unitCode) && !string.IsNullOrWhiteSpace(deptCode))
                {
                    dt.Rows.Add(unitCode, deptCode, deptName);
                }
            }
            return dt;
        }

        #endregion

        #region "TsBtnSave_Click"
        private async void TsBtnSave_Click(object sender, EventArgs e)
        {

            var startTime = DateTime.Now;  // Storing Start Time
            var progress = new FrmAdminProgress();

            try
            {
                DataTable dt = GetDepartmentsTableFromGrid();

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No rows to insert.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var msgResponse = MessageBox.Show("Do you wish to save. Please confirm.", "Confirm Save",
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

                if (await InsertRowsAsync())
                {
                    // Insert / Update was successful
                    MessageBox.Show($"Row INSERTED successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Disable Save button
                    TsBtnSave.Enabled = false;

                    // Calculate and display elapsed time
                    var elapsedTime = DateTime.Now.Subtract(startTime);
                    TsLblInputStatus.Text = $"Done In: {elapsedTime:hh\\:mm\\:ss}";
                    TsLblInputStatus.ForeColor = Color.DarkBlue;
                    Application.DoEvents();

                }
                else
                    // Update failed
                    MessageBox.Show("Failed to Insert row(s).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Insert Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                const string tableName = "dbo.Master_Departments";
                const string sqlProcedureName = "dbo.usp_Master_Departments_Bulk";

                if (TsBtnDrpUnitCode == null ||
                    string.IsNullOrWhiteSpace(TsBtnDrpUnitCode.Text) ||
                    TsBtnDrpUnitCode.Text.Equals("(All)", StringComparison.OrdinalIgnoreCase) ||
                    TsBtnDrpUnitCode.Text.Equals("Plant Code", StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException("Please Select Plant Code from drop down.");
                }

                string defaultUnitCode = TsBtnDrpUnitCode.Text;

                string connectionString = ClassGlobalFunctions.GetConnectionStringByUnitCode(defaultUnitCode);
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new InvalidOperationException("Connection string cannot be empty");
                }

                DataTable deptTable = BuildDataTableFromGrid();
                if (deptTable.Rows.Count == 0)
                {
                    MessageBox.Show("No rows to insert.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    throw new InvalidOperationException("No rows to insert");
                }

                var parameters = new List<SqlParameter>
                {
                   ClassDbHelpers.CreateSqlParameter("@UnitCode", SqlDbType.VarChar, defaultUnitCode),
                   ClassDbHelpers.CreateSqlParameter("@UserRowGuid", SqlDbType.NVarChar, ClassGlobalVariables.pubLoginUserRowGuid),
                };

                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync().ConfigureAwait(false);

                    using (var command = new SqlCommand(sqlProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // TVP parameter
                        SqlParameter tvpParam = command.Parameters.AddWithValue("@Departments", deptTable);
                        tvpParam.SqlDbType = SqlDbType.Structured;
                        tvpParam.TypeName = "dbo.UDT_Master_Departments";

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

        #region "BuildDataTableFromGrid"
        private DataTable BuildDataTableFromGrid()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("UnitCode", typeof(string));
            dt.Columns.Add("DepartmentCode", typeof(string));
            dt.Columns.Add("DepartmentName", typeof(string));
            dt.Columns.Add("IsActive", typeof(bool));
            dt.Columns.Add("RowVersion", typeof(int));

            foreach (DataGridViewRow row in DgvList.Rows)
            {
                if (row.IsNewRow) continue; // skip last empty row

                string unitCode = row.Cells["UnitCode"].Value?.ToString().Trim();
                string deptCode = row.Cells["DepartmentCode"].Value?.ToString().Trim();
                string deptName = row.Cells["DepartmentName"].Value?.ToString().Trim();

                // 🔹 Validation
                if (string.IsNullOrWhiteSpace(unitCode))
                    throw new Exception("UnitCode cannot be empty.");
                if (string.IsNullOrWhiteSpace(deptCode))
                    throw new Exception("DepartmentCode cannot be empty.");
                if (string.IsNullOrWhiteSpace(deptName))
                    throw new Exception("DepartmentName cannot be empty.");

                // Add to DataTable with defaults
                dt.Rows.Add(unitCode, deptCode, deptName, true, 1);
            }

            return dt;
        }

        #endregion


        #region "CmsIsActiveRows_Click"
        private async void CmsIsActiveRows_Click(object sender, EventArgs e)
        {

            var startTime = DateTime.Now;  // Storing Start Time
            var progress = new FrmAdminProgress();

            try
            {
                
                var sb = new StringBuilder("SET SELECTED DEPARTMENT AS IN-ACTIVE");
                sb.AppendLine("");
                sb.AppendLine(" - This will mark all slected rows");
                sb.AppendLine(" - In-Active");
                sb.AppendLine(" - This action cannot be recalled ");
                sb.AppendLine(" - Only authorised user can change the status to Active");

                var message = sb.ToString();

                var msgResponse = MessageBox.Show(message, "Confirm Paste",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (msgResponse == DialogResult.No)
                    return;

                DataTable dt = BuildIsActiveTableFromSelection();

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No rows selected.", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (await BulkUpdateIsActiveAsync(dt))
                {
                    // Insert / Update was successful
                    MessageBox.Show($"Row INSERTED successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Disable Save button
                    TsBtnSave.Enabled = false;

                    // Load DataGridView
                    await LoadDataGridViewAsync();

                    // Calculate and display elapsed time
                    var elapsedTime = DateTime.Now.Subtract(startTime);
                    TsLblInputStatus.Text = $"Done In: {elapsedTime:hh\\:mm\\:ss}";
                    TsLblInputStatus.ForeColor = Color.DarkBlue;
                    Application.DoEvents();

                }
                else
                    // Update failed
                    MessageBox.Show("Failed to Insert row(s).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                TsLblInputStatus.Text = ex.Message;
                TsLblInputStatus.ForeColor = Color.Red;
                MessageBox.Show(ex.Message, "Paste Rows", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region "BuildIsActiveTableFromSelection"
        private DataTable BuildIsActiveTableFromSelection()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("IsActive", typeof(bool));

            foreach (DataGridViewRow row in DgvList.SelectedRows)
            {
                if (row.Cells["Id"].Value == null)
                    continue;

                int id = Convert.ToInt32(row.Cells["Id"].Value);
                bool isActive = false;

                dt.Rows.Add(id, isActive);
            }

            return dt;
        }

        #endregion

        #region "BulkUpdateIsActiveAsync"
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task<bool> BulkUpdateIsActiveAsync(DataTable dt)
        {
            try
            {
                UseWaitCursor = true;

                const string tableName = "dbo.Master_Departments";
                const string sqlProcedureName = "dbo.usp_Master_Departments_Bulk_Active";

                if (TsBtnDrpUnitCode == null ||
                    string.IsNullOrWhiteSpace(TsBtnDrpUnitCode.Text) ||
                    TsBtnDrpUnitCode.Text.Equals("(All)", StringComparison.OrdinalIgnoreCase) ||
                    TsBtnDrpUnitCode.Text.Equals("Plant Code", StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException("Please Select Plant Code from drop down.");
                }

                string defaultUnitCode = TsBtnDrpUnitCode.Text;

                string connectionString = ClassGlobalFunctions.GetConnectionStringByUnitCode(defaultUnitCode);
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new InvalidOperationException("Connection string cannot be empty");
                }

                DataTable deptTable = BuildIsActiveTableFromSelection();
                if (deptTable.Rows.Count == 0)
                {
                    MessageBox.Show("No rows to insert.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    throw new InvalidOperationException("No rows to insert");
                }

                var parameters = new List<SqlParameter>
                {
                   ClassDbHelpers.CreateSqlParameter("@UnitCode", SqlDbType.VarChar, defaultUnitCode),
                   ClassDbHelpers.CreateSqlParameter("@UserRowGuid", SqlDbType.NVarChar, ClassGlobalVariables.pubLoginUserRowGuid),
                };

                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync().ConfigureAwait(false);

                    using (var command = new SqlCommand(sqlProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // TVP parameter
                        SqlParameter tvpParam = command.Parameters.AddWithValue("@Departments", deptTable);
                        tvpParam.SqlDbType = SqlDbType.Structured;
                        tvpParam.TypeName = "dbo.UDT_IsActive";

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
                TsLblInputStatus.Text = "Fetching data. Please wait...";
                TsLblInputStatus.ForeColor = Color.DarkBlue;
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
                    "DepartmentCode",
                    "DepartmentName",
                    "ContactPerson"};

                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@UnitCode", SqlDbType.VarChar) { Value = defaultUnitCode });

                // Determine the row limit based on TsCmbLimitRows input:
                // If the text is "(all)", set limit to 0
                // Otherwise, try parsing to int; if fails, default to 50
                // ?. ensures Text is not null before calling Trim()(null - conditional operator).
                int mLimitRows =
                    TsCmbLimitRows.Text?.Trim().ToLower() == "(all)" ? 0 :
                    int.TryParse(TsCmbLimitRows.Text, out var value) ? value : 50;

                string orderBy = "UnitCode, DepartmentCode, DepartmentName";
                string mTableName = "Master_Departments";

                // Get data asynchronously
                DataTable resultTable = await ClassDbHelpers.GetRowsFromTableAsync(
                    unitCode: defaultUnitCode,  // plant code
                    tableName: mTableName, // sql table name
                    selectedColumns: null,   // selectedColumns
                    serachColumns: serachColumns,  // search sql table columns
                    additionalParameters: parameters, // sql parameters
                    searchText: searchText, // search text
                    orderByClause: orderBy,    // sql order by columns
                    isDeletedColumn: null, // table column name for IsDeleted
                    isDeletedValue: null, // isDeletedValue
                    isActiveColumn: "IsActive", // table column name for IsActive
                    isActiveValue: isActiveRows, // isActiveValue
                    limitRows: mLimitRows);

                // Check for empty results
                if (resultTable == null || resultTable.Rows.Count == 0)
                {
                    TsLblInputStatus.Text = "No rows found for given parameters.";
                    return;
                }

                // Create a filtered DataTable with only the columns we want
                DataTable filteredTable = new DataTable();
                var columnsToInclude = new List<string> {
                    "UnitCode",
                    "DepartmentCode",
                    "DepartmentName"};

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
                        {"DepartmentCode", "Code"},
                        {"DepartmentName", "Name"}
                    };

                var folderPath = ClassLayoutHelper.GetReportFolderPath("Department");
                var file = Path.Combine(folderPath, "Departments.html");

                HtmlExportHelper.ExportDataTableToHtml(
                    table: filteredTable,
                    filePath: file,
                    title: "Department Master List",
                    dateFormat: "dd-MMM-yyyy",
                    customHeaders: customHeaders);

                // Calculate and display elapsed time
                var elapsedTime = DateTime.Now.Subtract(startTime);
                TsLblInputStatus.Text = $"Fetch Rows [ {resultTable.Rows.Count} ] In: {elapsedTime:hh\\:mm\\:ss}";
                TsLblInputStatus.ForeColor = Color.DarkBlue;
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                TsLblInputStatus.Text = ex.Message;
                TsLblInputStatus.ForeColor = Color.Red;
                MessageBox.Show(ex.Message, "Export List To Html", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                progress.Close();
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

    }
}

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
using TimeAttendanceManager.Auth.Classes;
using TimeAttendanceManager.Features.Masters.Jobcategory.Models;
using TimeAttendanceManager.Helpers.Classes;
using TimeAttendanceManager.Main.Classes;
using TimeAttendanceManager.Main.Forms;

namespace TimeAttendanceManager.Features.Masters.Jobcategory.Forms
{
    public partial class FrmMasterJobCategories : Form
    {
        #region "Constructor"
        public FrmMasterJobCategories()
        {
            InitializeComponent();

            // Remove events
            this.Load -= Form_Load;
            this.FormClosing -= FormClosing_FormClosing;
            this.TsBtnCloseForm.Click -= TsBtnCloseForm_Click;
            this.TsBtnClose.Click -= TsBtnCloseForm_Click;
            this.TsBtnHelp.Click -= TsBtnHelp_Click;
            this.TsBtnOptTech.Click -= TsBtnOptTech_Click;
            this.TsBtnFilterClose.Click -= TsBtnFilterClose_Click;
            this.TsBtnAddNew.Click -= TsBtnAddNew_Click;
            this.TsBtnSave.Click -= TsBtnSave_Click;
            this.TsBtnExport.Click -= TsBtnExport_Click;
            this.TsBtnPaste.Click -= TsBtnPaste_Click;

            // Event to update row numbers when rows are added/removed
            DgvList.RowsAdded -= DgvList_RowsChanged;
            DgvList.RowsRemoved -= DgvList_RowsChanged;
            DgvList.Sorted -= DgvList_RowsChanged;
            DgvList.RowValidated -= DgvList_RowValidated;
            DgvList.DefaultValuesNeeded -= DgvList_DefaultValuesNeeded;
            DgvList.CurrentCellDirtyStateChanged -= DgvList_CurrentCellDirtyStateChanged;
            DgvList.CellValidated -= DgvList_CellValidated;

            this.TsMenuViewInActive.Click -= TsMenuViewInActive_Click;
            this.TsMenuViewDeleted.Click -= TsMenuViewDeleted_Click;
            this.TsBtRefreshDgv.Click -= TsBtRefreshDgv_Click;
            this.TsBtnClearSearchDgv.Click -= TsBtRefreshDgv_Click;
            this.TsBtnSearchDgv.Click -= TsBtRefreshDgv_Click;
            this.TsTxtSearchDgv.KeyDown -= TsTxtSearchDgv_KeyDown;
            this.TsBtnDrpUnitCode.DropDownItemClicked -= TsBtnDrpBtn_DropDownItemClicked;

            // Add events
            this.Load += Form_Load;
            this.FormClosing += FormClosing_FormClosing;
            this.TsBtnCloseForm.Click += TsBtnCloseForm_Click;
            this.TsBtnClose.Click += TsBtnCloseForm_Click;
            this.TsBtnHelp.Click += TsBtnHelp_Click;
            this.TsBtnOptTech.Click += TsBtnOptTech_Click;
            this.TsBtnFilterClose.Click += TsBtnFilterClose_Click;
            this.TsBtnAddNew.Click += TsBtnAddNew_Click;
            this.TsBtnSave.Click += TsBtnSave_Click;
            this.TsBtnExport.Click += TsBtnExport_Click;
            this.TsBtnPaste.Click += TsBtnPaste_Click;

            // Event to update row numbers when rows are added/removed
            DgvList.RowsAdded += DgvList_RowsChanged;
            DgvList.RowsRemoved += DgvList_RowsChanged;
            DgvList.Sorted += DgvList_RowsChanged;
            DgvList.RowValidated += DgvList_RowValidated;
            DgvList.DefaultValuesNeeded += DgvList_DefaultValuesNeeded;
            DgvList.CurrentCellDirtyStateChanged += DgvList_CurrentCellDirtyStateChanged;
            DgvList.CellValidated += DgvList_CellValidated;

            this.TsMenuViewInActive.Click += TsMenuViewInActive_Click;
            this.TsMenuViewDeleted.Click += TsMenuViewDeleted_Click;
            this.TsBtRefreshDgv.Click += TsBtRefreshDgv_Click;
            this.TsBtnClearSearchDgv.Click += TsBtRefreshDgv_Click;
            this.TsBtnSearchDgv.Click += TsBtRefreshDgv_Click;
            this.TsTxtSearchDgv.KeyDown += TsTxtSearchDgv_KeyDown;
            this.TsBtnDrpUnitCode.DropDownItemClicked += TsBtnDrpBtn_DropDownItemClicked;

        }
        #endregion

        #region "LocalVariables"
        private MasterJobCategorie myDataTable = new MasterJobCategorie();
        private DataTable _sqlTable = new DataTable();
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

                    // Load help text
                    AddHelpText();
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

                await FillUnitCodeAsync();

                // Read user defaults
                await ReadUserLoginDefaultXmlAsync();

                // add context menu for DataGridView
                InitDgvListContextMenu();

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

            sb.AppendLine("MANAGE JOB CATEGORIES");
            sb.AppendLine();
            sb.AppendLine("Follow these steps to add or update JOB CATEGORIES:");
            sb.AppendLine();

            // 1. BASIC DATA
            sb.AppendLine("1. ADDING NEW JOB CATEGORIE");
            sb.AppendLine("   - Select your Plant Code from the dropdown list.");
            sb.AppendLine("   - Click the [Add New] button to insert a new row in the grid.");
            sb.AppendLine("   - Enter the required details:");
            sb.AppendLine("     • Category Code (unique per Plant)");
            sb.AppendLine("     • Category Name (description of the Category)");
            sb.AppendLine("   - 'Is Active' will be checked by default for new batches.");
            sb.AppendLine();

            // 2. SAVING DATA
            sb.AppendLine("2. SAVING DATA");
            sb.AppendLine("   - After adding or editing rows in the grid, click the [Save] button.");
            sb.AppendLine("   - The system will validate entries for:");
            sb.AppendLine("     • Required fields (Code and Name must not be blank)");
            sb.AppendLine("     • Duplicate Category Code per Plant.");
            sb.AppendLine("   - If validation passes, changes will be saved to the database.");
            sb.AppendLine("   - A confirmation message will appear after successful save.");
            sb.AppendLine();

            // 3. UPDATING EXISTING ROWS
            sb.AppendLine("3. UPDATING EXISTING JOB CATEGORIES");
            sb.AppendLine("   - Edit the Category Code or Category Name directly in the grid.");
            sb.AppendLine("   - Click the [Save] button to commit the changes.");
            sb.AppendLine("   - The system will update only the modified rows.");
            sb.AppendLine();

            // 4. DEACTIVATING A BATCH
            sb.AppendLine("4. DEACTIVATE A JOB CATEGORIES");
            sb.AppendLine("   - To make a batch inactive, uncheck the 'Is Active' column in the grid.");
            sb.AppendLine("   - Click [Save] to apply changes.");
            sb.AppendLine("   - Inactive Category will remain in the system but cannot be used in transactions.");
            sb.AppendLine();

            // 5. VIEWING AND FILTERING
            sb.AppendLine("5. VIEW AND FILTER JOB CATEGORIES");
            sb.AppendLine("   - Use the search box to filter by Code or Name.");
            sb.AppendLine("   - Change 'Rows per page' to adjust how many rows are displayed.");
            sb.AppendLine("   - Use column headers to sort ascending or descending.");
            sb.AppendLine();

            // 6. ADDITIONAL FEATURES
            sb.AppendLine("6. ADDITIONAL FEATURES");
            sb.AppendLine("   - Export the list of batches to Excel/CSV.");
            sb.AppendLine("   - Print the batch list for review.");
            sb.AppendLine("   - Refresh the grid to reload latest data from the database.");
            sb.AppendLine();

            // 7. TROUBLESHOOTING
            sb.AppendLine("7. TROUBLESHOOTING");
            sb.AppendLine("   - If Save fails: Ensure Plant Code, Code, and Name are filled in.");
            sb.AppendLine("   - If duplicate error: Code must be unique within the same Plant.");
            sb.AppendLine("   - Contact the system administrator if problems persist.");
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
            txtHelp.BackColor = System.Drawing.Color.White;
            txtHelp.ForeColor = System.Drawing.Color.DarkBlue;
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
                    FormCreated = DateTime.ParseExact("04-Sep-2025", "dd-MMM-yyyy", CultureInfo.InvariantCulture)
                };

                // Add table names used in this form (9 fields as in your VB)
                mTechDetails.TableNames.Add(new TableNames(
                    "dbo.Master_JobCategories",
                    "dbo.usp_Master_JobCategories_Insert",
                    "dbo.usp_Master_JobCategories_Update",
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

        #region "TsBtRefreshDgv_Click"
        private async void TsBtRefreshDgv_Click(System.Object sender, System.EventArgs e)
        {
            // clear search box if coming from Clear button
            if (sender == TsBtnClearSearchDgv)
                TsTxtSearchDgv.Text = null;

            await LoadDataGridViewAsync();

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

        #region "TsTxtSearchDgv_KeyDown"
        private void TsTxtSearchDgv_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)

                // Update DataGridView
                TsBtRefreshDgv_Click(null, null);
        }

        #endregion

        #region "InitDgvListContextMenu"
        private void InitDgvListContextMenu()
        {
            _cmsDgvList = new ContextMenuStrip();

            var miEditSelectedRow = new ToolStripMenuItem("Edit Selected Row") { Name = "cmsEditSelectedRow" };
            var miAddNewRow = new ToolStripMenuItem("Add New Row") { Name = "cmsAddNewRow" };
            var miPasteRows = new ToolStripMenuItem("Paste Rows From Excel") { Name = "cmsPasteRows" };
            var miCancel = new ToolStripMenuItem("Cancel") { Name = "cmsCancel" };

            miEditSelectedRow.Click += CmsEditSelectedRow_Click;  // attach a handler
            miAddNewRow.Click += CmsAddNewRow_Click;
            miPasteRows.Click += CmsPasteRows_Click;
            miCancel.Click += (s, e) => { /* no-op */ };

            _cmsDgvList.Items.Add(miEditSelectedRow);
            _cmsDgvList.Items.Add(miAddNewRow);
            _cmsDgvList.Items.Add(miPasteRows);
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
            var startTime = DateTime.Now;
            var progress = new FrmAdminProgress();

            try
            {
                progress.LblStatus.Text = "Loading Batch Codes...";
                progress.LblMoreStatus.Text = "Please Wait...";
                progress.ProgressBar1.Visible = false;
                progress.Show();

                TsLblInputStatus.Text = "Fetching Rows. Please wait...";
                TsLblInputStatus.ForeColor = System.Drawing.Color.DarkBlue;
                Application.DoEvents();

                // Validate Plant Code
                if (TsBtnDrpUnitCode == null || string.IsNullOrWhiteSpace(TsBtnDrpUnitCode.Text) ||
                    TsBtnDrpUnitCode.Text == "(All)" || TsBtnDrpUnitCode.Text == "Plant Code")
                {
                    throw new InvalidOperationException("Please Select Plant Code from drop down.");
                }
                string defaultUnitCode = TsBtnDrpUnitCode.Text;

                // Build connection
                string connectionString = ClassGlobalFunctions.GetConnectionStringByUnitCode(defaultUnitCode);
                if (string.IsNullOrWhiteSpace(connectionString))
                    throw new InvalidOperationException("Connection string cannot be empty.");

                // Build search
                string searchText = string.IsNullOrWhiteSpace(TsTxtSearchDgv.Text) ? null : TsTxtSearchDgv.Text.Trim();
                if (!string.IsNullOrEmpty(searchText))
                    searchText = searchText.Replace("*", "%");

                var searchColumns = new List<string> { "UnitCode", "Code", "Name" };
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@UnitCode", SqlDbType.NVarChar) { Value = defaultUnitCode }
                };

                int mLimitRows = TsCmbLimitRows.Text?.Trim().ToLower() == "(all)" ? 0 :
                                 int.TryParse(TsCmbLimitRows.Text, out var value) ? value : 50;

                // Get rows
                DataTable resultTable = await ClassDbHelpers.GetRowsFromTableAsync(
                    unitCode: defaultUnitCode,
                    tableName: "Master_JobCategories",
                    selectedColumns: null,
                    serachColumns: searchColumns,
                    additionalParameters: parameters,
                    searchText: searchText,
                    orderByClause: "UnitCode, CodeLevel, Code, Name",
                    isDeletedColumn: null,
                    isDeletedValue: null,
                    isActiveColumn: "IsActive",
                    isActiveValue: isActiveRows,
                    limitRows: mLimitRows);

                // Always create schema even if no rows
                // Add columns if you got an empty table with no schema
                _sqlTable = resultTable ?? new DataTable();
                if (_sqlTable.Columns.Count == 0)
                {
                    // Only add schema if table has no columns at all
                    _sqlTable.Columns.Add("Id", typeof(int));
                    _sqlTable.Columns.Add("RowVersion", typeof(int));
                    _sqlTable.Columns.Add("UnitCode", typeof(string));
                    _sqlTable.Columns.Add("Code", typeof(string));
                    _sqlTable.Columns.Add("Name", typeof(string));
                    _sqlTable.Columns.Add("CodeLevel", typeof(int));
                    _sqlTable.Columns.Add("IsActive", typeof(bool));
                }

                // Bind DataGridView
                DgvList.DataSource = _sqlTable;
                DgvList.ReadOnly = false;
                DgvList.AllowUserToAddRows = true;  // allow insert
                DgvList.AllowUserToDeleteRows = true; // disable delete here

                // Hide system columns
                DgvList.Columns["Id"].Visible = false;
                DgvList.Columns["Id"].ReadOnly = true;

                DgvList.Columns["RowVersion"].Visible = false;
                DgvList.Columns["RowVersion"].ReadOnly = true;

                // Set nice headers
                DgvList.Columns["UnitCode"].HeaderText = "Plant";
                DgvList.Columns["Code"].HeaderText = "Code";
                DgvList.Columns["Name"].HeaderText = "Name";
                DgvList.Columns["CodeLevel"].HeaderText = "Code Level";
                DgvList.Columns["IsActive"].HeaderText = "Is Active";

                // Set column readonly
                DgvList.Columns["UnitCode"].ReadOnly = true;

                // Set column alignments
                DgvList.Columns["Code"].Width = 500;   // pixels

                // Disable global auto-sizing
                DgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                // Code column fixed width
                DgvList.Columns["Code"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                DgvList.Columns["Code"].Width = 300;

                // Name column fills remaining space
                DgvList.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                // Row numbers
                for (int i = 0; i < DgvList.Rows.Count; i++)
                    DgvList.Rows[i].HeaderCell.Value = (i + 1).ToString();

                // Populate the dropdown 
                AddColumnVisibilityDgvItems();

                // Accept changes to reset row states
                _sqlTable.AcceptChanges();

                // Status
                var elapsedTime = DateTime.Now.Subtract(startTime);
                TsLblInputStatus.Text = $"Fetched Rows [ {_sqlTable.Rows.Count} ] in: {elapsedTime:hh\\:mm\\:ss}";
                TsLblInputStatus.ForeColor = System.Drawing.Color.DarkBlue;
            }
            catch (Exception ex)
            {
                TsLblInputStatus.Text = ex.Message;
                TsLblInputStatus.ForeColor = System.Drawing.Color.Red;
                MessageBox.Show(ex.Message, "Load DataGridView", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
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

        #region "DgvList_RowsChanged"
        private void DgvList_RowsChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DgvList.Rows.Count; i++)
            {
                if (!DgvList.Rows[i].IsNewRow) // skip the "new row" placeholder
                {
                    DgvList.Rows[i].HeaderCell.Value = (i + 1).ToString();
                }
            }
        }

        #endregion

        #region "DgvList_RowValidated"
        private void DgvList_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < DgvList.Rows.Count; i++)
            {
                if (!DgvList.Rows[i].IsNewRow)
                {
                    DgvList.Rows[i].HeaderCell.Value = (i + 1).ToString();
                }
            }
        }

        #endregion

        #region "DgvList_DefaultValuesNeeded"
        /// <summary>
        /// Auto-fill default value when new row is created
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvList_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {

            if (TsBtnDrpUnitCode == null || string.IsNullOrWhiteSpace(TsBtnDrpUnitCode.Text) ||
                TsBtnDrpUnitCode.Text == "(All)" || TsBtnDrpUnitCode.Text == "Plant Code")
            {
                MessageBox.Show("Please select a Plant Code from the drop down before adding rows.",
                    "Plant Code Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string defaultUnitCode = TsBtnDrpUnitCode.Text;

            int newValue = 1; // default if table is empty
            if (_sqlTable.Rows.Count > 0)
            {
                // Use LINQ to find max CodeLevel (ignore DBNull values)
                var maxValue = _sqlTable.AsEnumerable()
                                        .Where(r => r.RowState != DataRowState.Deleted && r["CodeLevel"] != DBNull.Value)
                                        .Select(r => r.Field<int>("CodeLevel"))
                                        .DefaultIfEmpty(0)   // if all are DBNull or no rows
                                        .Max();

                newValue = maxValue + 1;
            }

            e.Row.Cells["UnitCode"].Value = defaultUnitCode;
            e.Row.Cells["CodeLevel"].Value = newValue;
            e.Row.Cells["IsActive"].Value = true;
        }

        #endregion

        #region "DgvList_CurrentCellDirtyStateChanged"
        /// <summary>
        /// Force immediate commit when the user edits a cell:
        /// This ensures CellValueChanged event fires right away.
        /// Useful for checkbox columns.
        /// CommitEdit immediately pushes the change into the bound DataTable.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvList_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (DgvList.IsCurrentCellDirty)
            {
                DgvList.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        #endregion

        #region "DgvList_CellValueChanged"
        /// <summary>
        /// Be careful with AcceptChanges() — it resets row states (Added/Modified),
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvList_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            // Value is already committed to DataTable here
            // You can log or trigger save if you want
        }

        #endregion

        #region "TsBtnPaste_Click"
        private void TsBtnPaste_Click(object sender, EventArgs e)
        {
            CmsPasteRows_Click(null, null);
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
                sb.AppendLine();
                sb.AppendLine(" - Ensure columns are in order:");
                sb.AppendLine("   Unit Code\tCode\tName\tCode Level");
                sb.AppendLine(" - Rows with empty Code will be skipped.");
                sb.AppendLine(" - Duplicates by (UnitCode, Code) will be skipped.");
                var msgResponse = MessageBox.Show(sb.ToString(), "Confirm Paste",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgResponse == DialogResult.No) return;

                // Ensure grid + table ready
                PrepareDataGridView();

                // Build a fast duplicate check set from existing rows
                var existingKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (DataRow r in _sqlTable.Rows)
                {
                    if (r.RowState != DataRowState.Deleted && !IsNullOrWhiteSpace(r["UnitCode"]) && !IsNullOrWhiteSpace(r["Code"]))
                    {
                        string key = Convert.ToString(r["UnitCode"]).Trim() + "||" + Convert.ToString(r["Code"]).Trim();
                        existingKeys.Add(key);
                    }
                }

                // Track duplicates within this paste
                var pastedKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                // Split lines
                string[] lines = clipboardText.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

                int pasted = 0, skippedEmptyCode = 0, skippedDuplicate = 0;
                foreach (string line in lines)
                {
                    var values = line.Split('\t');

                    string unitCode = values.Length > 0 ? (values[0] ?? string.Empty).Trim() : string.Empty;
                    string code = values.Length > 1 ? (values[1] ?? string.Empty).Trim() : string.Empty;
                    string name = values.Length > 2 ? (values[2] ?? string.Empty).Trim() : string.Empty;
                    string codeLvl = values.Length > 3 ? (values[3] ?? string.Empty).Trim() : string.Empty;

                    if (string.IsNullOrWhiteSpace(code))
                    {
                        skippedEmptyCode++;
                        continue;
                    }

                    string key = unitCode + "||" + code;
                    if (existingKeys.Contains(key) || pastedKeys.Contains(key))
                    {
                        skippedDuplicate++;
                        continue;
                    }

                    // Build row
                    DataRow newRow = _sqlTable.NewRow();
                    newRow["Id"] = DBNull.Value;
                    newRow["UnitCode"] = unitCode;
                    newRow["Code"] = code;
                    newRow["Name"] = name;
                    newRow["RowVersion"] = DBNull.Value;
                    // newRow["CodeLevel"] = codeLvl;

                    // CodeLevel: use pasted int if valid, else auto = max+1
                    object parsed = ParseIntOrDbNull(codeLvl);
                    if (parsed == DBNull.Value)
                        newRow["CodeLevel"] = GetNextCodeLevel();
                    else
                        newRow["CodeLevel"] = parsed;

                    newRow["IsActive"] = true;

                    _sqlTable.Rows.Add(newRow);
                    pasted++;

                    // mark key as used to prevent duplicates within this paste batch
                    pastedKeys.Add(key);
                }

                // Rebind is not necessary since _sqlTable is the DataSource, but refresh UI
                DgvList.Refresh();

                TsBtnSave.Enabled = (_sqlTable.GetChanges(DataRowState.Added) != null) ||
                                    (_sqlTable.GetChanges(DataRowState.Modified) != null);

                TsLblInputStatus.Text = $"Pasted {pasted} row(s). Skipped: empty Code={skippedEmptyCode}, duplicates={skippedDuplicate}.";
                TsLblInputStatus.ForeColor = pasted > 0 ? Color.Green : Color.DarkOrange;
            }
            catch (Exception ex)
            {
                TsLblInputStatus.Text = ex.Message;
                TsLblInputStatus.ForeColor = Color.Red;
                MessageBox.Show(ex.Message, "Paste Rows", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            // Build table schema if needed
            _sqlTable = new DataTable("Categories");

            var colId = _sqlTable.Columns.Add("Id", typeof(int));
            colId.AllowDBNull = true;

            _sqlTable.Columns.Add("UnitCode", typeof(string));
            _sqlTable.Columns.Add("Code", typeof(string));
            _sqlTable.Columns.Add("Name", typeof(string));

            var colCodeLevel = _sqlTable.Columns.Add("CodeLevel", typeof(int));
            colCodeLevel.AllowDBNull = true;

            _sqlTable.Columns.Add("IsActive", typeof(bool));

            var colRowVersion = _sqlTable.Columns.Add("RowVersion", typeof(int));
            colRowVersion.AllowDBNull = true;

            // Bind DataGridView
            DgvList.DataSource = _sqlTable;
            DgvList.ReadOnly = false;
            DgvList.AllowUserToAddRows = true;  // allow insert
            DgvList.AllowUserToDeleteRows = true; // disable delete here

            DgvList.Columns["Id"].Visible = false;
            DgvList.Columns["Id"].ReadOnly = true;

            DgvList.Columns["RowVersion"].Visible = false;
            DgvList.Columns["RowVersion"].ReadOnly = true;

        }

        #endregion

        #region "ParseIntOrDbNull"
        private static object ParseIntOrDbNull(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return DBNull.Value;
            int v;
            return int.TryParse(s.Trim(), out v) ? (object)v : DBNull.Value;
        }
        #endregion

        #region "GetNextCodeLevel"
        private int GetNextCodeLevel()
        {
            if (_sqlTable == null || _sqlTable.Rows.Count == 0) return 1;

            var max = 0;
            foreach (DataRow r in _sqlTable.Rows)
            {
                if (r.RowState != DataRowState.Deleted && r["CodeLevel"] != DBNull.Value)
                {
                    var v = (int)r["CodeLevel"];
                    if (v > max) max = v;
                }
            }
            return max + 1;
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

            if (_sqlTable == null)
            {
                MessageBox.Show("Data not loaded.");
                return;
            }

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


            // Create a new row
            DataRow newRow = _sqlTable.NewRow();

            int newValue = 1; // default if table is empty
            if (_sqlTable.Rows.Count > 0)
            {
                // Use LINQ to find max CodeLevel (ignore DBNull values)
                var maxValue = _sqlTable.AsEnumerable()
                                        .Where(r => r.RowState != DataRowState.Deleted && r["CodeLevel"] != DBNull.Value)
                                        .Select(r => r.Field<int>("CodeLevel"))
                                        .DefaultIfEmpty(0)   // if all are DBNull or no rows
                                        .Max();

                newValue = maxValue + 1;
            }

            // Set defaults (Id must be null for insert)
            _sqlTable.Columns["Id"].AllowDBNull = true;
            newRow["Id"] = DBNull.Value;
            newRow["RowVersion"] = DBNull.Value;
            newRow["UnitCode"] = defaultUnitCode;
            newRow["Code"] = string.Empty;
            newRow["Name"] = string.Empty;
            newRow["CodeLevel"] = newValue;
            newRow["IsActive"] = true;

            // Add row to DataTable
            _sqlTable.Rows.Add(newRow);

            // Move focus to the new row in the DataGridView
            DgvList.CurrentCell = DgvList.Rows[DgvList.Rows.Count - 1].Cells["Code"];
            DgvList.BeginEdit(true);

            DgvList.Focus();

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
                TsLblInputStatus.ForeColor = System.Drawing.Color.DarkBlue;
                Application.DoEvents();

                // DgvList.EndEdit();            // commit current cell

                // Format data rows before saving
                FormatDatTableRows();

                // Separate new rows vs modified rows
                DataTable insertTable = _sqlTable.GetChanges(DataRowState.Added);
                DataTable updateTable = _sqlTable.GetChanges(DataRowState.Modified);

                // keep only rows with a non-empty Code
                insertTable = FilterNonEmptyCode(insertTable);
                updateTable = FilterNonEmptyCode(updateTable);

                // Track changes to save
                bool hasChanges = false;

                if (insertTable != null && insertTable.Rows.Count > 0)
                {
                    if (await UpsertRowsAsync("dbo.usp_Master_JobCategories_Insert", insertTable))
                    {
                        // Insert was successful
                        hasChanges = true;
                        MessageBox.Show("Row inserted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                if (updateTable != null)
                {
                    // keep only rows that actually changed values
                    var changed = updateTable.GetChanges(DataRowState.Modified);
                    if (changed != null && changed.Rows.Count > 0)
                    {
                        if (await UpsertRowsAsync("dbo.usp_Master_JobCategories_Update", changed))
                        {
                            hasChanges = true;
                            MessageBox.Show("Row updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

                // Refresh grid after successful insert or update
                if (hasChanges)
                {
                    await LoadDataGridViewAsync();
                }

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

        #region "FormatDatTableRows"

        private void FormatDatTableRows()
        {
            foreach (DataRow row in _sqlTable.Rows)
            {
                if (row.RowState == DataRowState.Deleted) continue;

                if (row["Code"] != DBNull.Value)
                {
                    string original = row["Code"].ToString();
                    string cleaned = ClassStringHelpers.CleanAndUpperCase(original);
                    if (cleaned != original)   // only assign if different
                        row["Code"] = cleaned;
                }

                if (row["Name"] != DBNull.Value)
                {
                    string original = row["Name"].ToString();
                    string cleaned = ClassStringHelpers.CleanAndUpperCase(original);
                    if (cleaned != original)   // only assign if different
                        row["Name"] = cleaned;
                }
            }
        }


        #endregion

        #region "FilterNonEmptyCode"
        /// <summary>
        /// helper: treat DBNull/NULL/whitespace as empty 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static bool IsNullOrWhiteSpace(object value)
        {
            if (value == null || value == DBNull.Value) return true;
            return string.IsNullOrWhiteSpace(Convert.ToString(value));
        }

        /// <summary>
        /// helper: clone structure and import only rows with non-empty Code
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private static DataTable FilterNonEmptyCode(DataTable source)
        {
            if (source == null) return null;

            var filtered = source.Clone(); // keeps columns, constraints, types
            foreach (DataRow r in source.Rows)
            {
                if (!IsNullOrWhiteSpace(r["Code"]))
                    filtered.ImportRow(r);   // preserves RowState & versions
            }
            return filtered.Rows.Count > 0 ? filtered : null;
        }
        #endregion

        #region "UpsertRowsAsync"
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task<bool> UpsertRowsAsync(
            string sqlProcedureName,
            DataTable changes)
        {
            try
            {
                UseWaitCursor = true;

                const string tableName = "dbo.Master_JobCategories";

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

                string connectionString = ClassGlobalFunctions.GetConnectionStringByUnitCode(defaultUnitCode);
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new InvalidOperationException("Connection string cannot be empty");
                }

                var parameters = new List<SqlParameter>
                {
                    ClassDbHelpers.CreateSqlParameter("@UnitCode", SqlDbType.VarChar, defaultUnitCode),
                    ClassDbHelpers.CreateSqlParameter("@UserRowGuid", SqlDbType.VarChar, ClassGlobalVariables.pubLoginUserRowGuid),
                };

                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync().ConfigureAwait(false);

                    using (var command = new SqlCommand(sqlProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddRange(parameters.ToArray());

                        // Add table-valued parameter
                        var tvpParam = command.Parameters.AddWithValue("@TableType", changes);
                        tvpParam.SqlDbType = SqlDbType.Structured;
                        tvpParam.TypeName = "dbo.UDT_Master_JobCategories";

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

        #region "ValidateInputAsync"

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task<bool> ValidateInputAsync()
        {
            try
            {
                TsLblInputStatus.ForeColor = System.Drawing.Color.Red;
                TsLblInputStatus.Text = "Validating data. Please wait...";

                // Remove existing errors

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

                // Validate DataTable column Name
                foreach (DataRow row in _sqlTable.Rows)
                {
                    if (row.RowState == DataRowState.Deleted) continue;

                    string code = row["Code"] == DBNull.Value ? null : row["Code"].ToString().Trim();
                    string name = row["Name"] == DBNull.Value ? null : row["Name"].ToString().Trim();

                    if (!string.IsNullOrEmpty(code) && string.IsNullOrWhiteSpace(name))
                    {
                        MessageBox.Show(
                            $"Row {_sqlTable.Rows.IndexOf(row) + 1}: 'Name' is required when 'Code' is filled.",
                            "Validation Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return false; // stop validation
                    }
                }

                // Populate data table
                myDataTable = new MasterJobCategorie();

                TsLblInputStatus.ForeColor = System.Drawing.Color.DarkBlue;
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

        #region "ClearInputs"
        /// <summary>
        /// 
        /// </summary>
        private void ClearInputs()
        {
            try
            {

                TsLblInputStatus.Text = "...";

                // set focus
                DgvList.Focus();
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
                    TsBtnDrpUnitCode.Text = plantCode;
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
                TsLblInputStatus.ForeColor = System.Drawing.Color.DarkBlue;
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
                    "Code",
                    "Name"};

                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@UnitCode", SqlDbType.NVarChar) { Value = defaultUnitCode });

                // Determine the row limit based on TsCmbLimitRows input:
                // If the text is "(all)", set limit to 0
                // Otherwise, try parsing to int; if fails, default to 50
                // ?. ensures Text is not null before calling Trim()(null - conditional operator).
                int mLimitRows =
                    TsCmbLimitRows.Text?.Trim().ToLower() == "(all)" ? 0 :
                    int.TryParse(TsCmbLimitRows.Text, out var value) ? value : 50;

                string orderBy = "UnitCode, CodeLevel, Code, Name";
                string mTableName = "Master_JobCategories";

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
                    "Code",
                    "Name"};

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
                        {"Code", "Code"},
                        {"Name", "Name"}
                    };

                var folderPath = ClassLayoutHelper.GetReportFolderPath("MasterData");
                var file = Path.Combine(folderPath, "JobCategories.html");

                HtmlExportHelper.ExportDataTableToHtml(
                    table: filteredTable,
                    filePath: file,
                    title: "Master Job Categorires Codes",
                    dateFormat: "dd-MMM-yyyy",
                    customHeaders: customHeaders);

                // Calculate and display elapsed time
                var elapsedTime = DateTime.Now.Subtract(startTime);
                TsLblInputStatus.Text = $"Fetch Rows [ {resultTable.Rows.Count} ] In: {elapsedTime:hh\\:mm\\:ss}";
                TsLblInputStatus.ForeColor = System.Drawing.Color.DarkBlue;
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                TsLblInputStatus.Text = ex.Message;
                TsLblInputStatus.ForeColor = System.Drawing.Color.Red;
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

using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimeAttendanceManager.Auth.Models;
using TimeAttendanceManager.Auth.Classes;
using TimeAttendanceManager.Main.Forms;
using TimeAttendanceManager.Main.Classes;
using TimeAttendanceManager.Helpers.Classes;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

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
///    Title:          Code for User's Registration
///                    
/// 
///    Name:           FrmAuthManageUsers.cs
///    Created:        15th August 2025
///    Date Completed: 15th August 2025
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
    public partial class FrmAuthManageUsers : Form
    {

        #region "Constructor"
        public FrmAuthManageUsers()
        {
            InitializeComponent();
            this.Load += Form_Load;
            this.FormClosing += FormClosing_FormClosing;
            this.TsBtnCloseForm.Click += TsBtnCloseForm_Click;
            this.TsBtnClose.Click += TsBtnCloseForm_Click;
            this.TabCtrlMain.Click += TabCtrlMain_Click;
            this.TsBtnSidePanel.Click += TsBtnSideMenu_Click;
            this.TsMenuViewLockedUsers.Click += TsMenuViewLockedUsers_Click;
            this.TsMenuViewDeletedUsers.Click += TsMenuViewLockedUsers_Click;
            this.DgvListUsers.CellClick += DgvListUsers_CellClick;
            this.TsBtnAddNew.Click += TsBtnAddNew_Click;
            this.TsBtnSave.Click += TblBtnSave_Click;
            this.TsBtnUpdatePwd.Click += TsBtnUpdatePwd_Click;
            this.TsBtnUpdateMstPwd.Click += TsBtnUpdateMstPwd_Click;
            this.TsBtRefreshDgv.Click += TsBtRefreshDgv_Click;
            this.TsBtnClearSearchDgv.Click += TsBtRefreshDgv_Click;
            this.TsBtnSearchDgv.Click += TsBtRefreshDgv_Click;
            this.TsTxtSearchDgv.KeyDown += TsTxtSearchDgv_KeyDown;
            this.TsBtnDrpDgvCols.DropDownItemClicked += TsBtnDrpDgvCols_DropDownItemClicked;
            this.TsBtnDrpUnitCode.DropDownItemClicked += TsBtnDrpBtn_DropDownItemClicked;
        }
        #endregion

        #region "LocalVariables"
        private ContextMenuStrip myContextMenu = new ContextMenuStrip();   // Storing Context Menu

        private bool pubLockedOutUsers = false;
        private bool pubDeletedUsers = false;

        private UserLogin myDataTable = new UserLogin();
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

                    // Update DataGridView
                    await LoadDataGridViewAsync();

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

        #region "InitializeComponents"
        private async Task InitializeComponentsAsync(FrmAdminProgress progress)
        {
            try
            {
                // Thread-safe invocations for UI updates
                await Task.Run(() => this.Invoke((MethodInvoker)(() =>
                {
                    SetSplitterDistance();
                    DockControls();
                })));

                // Need to handle async method separately
                await SetDefaultValuesAsync();

                // Report progress
                progress.Invoke((MethodInvoker)(() =>
                {
                    progress.ProgressBar1.Visible = true;
                    progress.ProgressBar1.Value = 50;
                    progress.LblStatus.Text = "Loading data...";
                }));

                // If LoadDataGridView touches UI, wrap in Invoke
                await LoadDataGridViewAsync();

                progress.Invoke((MethodInvoker)(() =>
                    progress.ProgressBar1.Value = 100));
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                MessageBox.Show($"Initialization failed: {ex.Message}");
                throw; // Re-throw if you want calling code to handle it
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
                SplitRight.SplitterDistance = (int)(this.SplitRight.Width * 0.4);
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
            ClassLayoutHelper.ConfigureTableLayout(TableLayout3);
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

        #region "TsMenuViewLockedUsers_Click"
        private async void TsMenuViewLockedUsers_Click(object sender, EventArgs e)
        {
            pubLockedOutUsers = false;
            pubDeletedUsers = false;

            if (sender == TsMenuViewLockedUsers)
                pubLockedOutUsers = true;
            if (sender == TsMenuViewDeletedUsers)
                pubDeletedUsers = true;

            // Update DataGridView
           await LoadDataGridViewAsync();
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

                // Load UnitCode where access is granted
                await ClassGlobalFunctions.FillComboBoxUnitCodeHasAccessAsync(
                    displayMember: "UnitCode",
                    valueMember: "Id",
                    comboBox: CmbUnitCode,
                    tableName: "Companies",
                    userRowGuid: ClassGlobalVariables.pubLoginUserRowGuid,
                    fallbackUnitCode: ClassGlobalVariables.pubUnitCode);

                await FillUnitCodeAsync();

                await ClassGlobalFunctions.FillComboBoxAsync("RoleName", "Id", CmbUserRole, "UserRoles", ClassGlobalVariables.pubUnitCode);
                await ClassGlobalFunctions.FillComboBoxAsync("DepartmentCode", "Id", CmbDept, "Master_Departments", ClassGlobalVariables.pubUnitCode);

                // Set up enum-based combo boxes
                CmbUserLocked.DataSource = Enum.GetValues(typeof(ClassGlobalVariables.PubYeNoStatus));
                CmbUserLocked.SelectedItem = ClassGlobalVariables.PubYeNoStatus.No;

                CmbUserDeleted.DataSource = Enum.GetValues(typeof(ClassGlobalVariables.PubYeNoStatus));
                CmbUserDeleted.SelectedItem = ClassGlobalVariables.PubYeNoStatus.No;
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

        #region "TsBtRefreshDgv_Click"
        private async void TsBtRefreshDgv_Click(System.Object sender, System.EventArgs e)
        {
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

        #region "TsBtnDrpDgvCols_DropDownItemClicked"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsBtnDrpDgvCols_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                if (e.ClickedItem is ToolStripMenuItem menuItem && menuItem.Tag is string colName)
                {
                    menuItem.Checked = !menuItem.Checked;

                    if (DgvListUsers.Columns.Contains(colName))
                    {
                        DgvListUsers.Columns[colName].Visible = menuItem.Checked;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error Number: {ex.HResult}\n" +
                    $"Error Source: {ex.Source}\n" +
                    $"Error Message: {ex.Message}\n" +
                    $"Form: {this.Name}\n" +
                    $"Function: {nameof(TsBtnDrpDgvCols_DropDownItemClicked)}",
                    "Error",
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
            var progress = new  FrmAdminProgress();

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
                DgvListUsers.Invoke((MethodInvoker)(() =>
                {
                    DgvListUsers.DataSource = null;
                    DgvListUsers.Rows.Clear();
                    DgvListUsers.Columns.Clear();
                    DgvListUsers.Refresh();
                    DgvListUsers.Visible = false;
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

                string mUnitCode = (CmbUnitCode == null || string.IsNullOrWhiteSpace(CmbUnitCode.Text))
                   ? ClassGlobalVariables.pubUnitCode
                   : CmbUnitCode.Text;

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
                    "UserName",
                    "DepartmentCode", 
                    "RoleName" };

                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@UnitCode", SqlDbType.VarChar) { Value = defaultUnitCode });
                parameters.Add(new SqlParameter("@IsLockedOut", SqlDbType.Bit) { Value = pubLockedOutUsers });
                parameters.Add(new SqlParameter("@IsDeleted", SqlDbType.Bit) { Value = pubDeletedUsers });

                string orderBy = "UnitCode, RoleName, UserName";

                // Get data asynchronously
                DataTable resultTable = await ClassDbHelpers.GetRowsFromTableAsync(
                    defaultUnitCode,
                    "v_UserLogins",
                    null,
                    serachColumns,
                    parameters,
                    searchText,
                    orderBy);

                // Check for empty results
                if (resultTable == null || resultTable.Rows.Count == 0)
                {
                    TsLblDgvListFooter.Text = "No rows found for given parameters.";
                    return;
                }

                // Create a filtered DataTable with only the columns we want
                DataTable filteredTable = new DataTable();
                var columnsToInclude = new List<string> { "Id", "UnitCode", "UserName", "DepartmentCode", "RoleName" };
               
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
                DgvListUsers.Invoke((MethodInvoker)(() =>
                {
                    DgvListUsers.DataSource = null;
                    DgvListUsers.Rows.Clear();
                    DgvListUsers.Columns.Clear();
                    DgvListUsers.Refresh();
                    DgvListUsers.Visible = false;
                }));

                // Set data source
                DgvListUsers.Invoke((MethodInvoker)(() => DgvListUsers.DataSource = filteredTable));

                // Define and apply custom headers
                var customHeaders = new Dictionary<string, string>
                    {
                        {"Id", "Id"},
                        {"UnitCode", "Plant"},
                        {"UserName", "Login Name"},
                        {"DepartmentCode", "User Dept"},
                        {"RoleName", "User Role"}
                    };

                DgvListUsers.Invoke((MethodInvoker)(() =>
                {
                    foreach (DataGridViewColumn column in DgvListUsers.Columns)
                    {
                        if (customHeaders.ContainsKey(column.Name))
                        {
                            column.HeaderText = customHeaders[column.Name];
                        }
                    }

                    // Configure DataGridView appearance
                    DgvListUsers.ColumnHeadersVisible = true;
                    DgvListUsers.AllowUserToAddRows = false;
                    DgvListUsers.AllowUserToDeleteRows = false;
                    DgvListUsers.ReadOnly = true;
                    DgvListUsers.RowHeadersWidth = 60;
                    DgvListUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    DgvListUsers.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;

                    // Set column header style
                    var columnHeaderStyle = new DataGridViewCellStyle
                    {
                        BackColor = Color.Beige
                    };
                    DgvListUsers.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

                    // Auto-size columns
                    DgvListUsers.AutoResizeColumnHeadersHeight();
                    DgvListUsers.AutoResizeColumns();

                    // Hide ID column
                    DgvListUsers.Columns["Id"].Visible = false;
                    DgvListUsers.Columns["Id"].ReadOnly = true;

                    // Add row numbers
                    for (int i = 0, mRowId = 1; i < DgvListUsers.Rows.Count; i++, mRowId++)
                    {
                        DgvListUsers.Rows[i].HeaderCell.Value = mRowId.ToString();
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
                DgvListUsers.Invoke((MethodInvoker)(() =>
                {
                    DgvListUsers.ResumeLayout();
                    DgvListUsers.Visible = true;
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
            for (int i = 0; i < DgvListUsers.Columns.Count; i++)
            {
                DataGridViewColumn column = DgvListUsers.Columns[i];

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

        #region "DgvListUsers_CellClick"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void DgvListUsers_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            try
            {
                if (DgvListUsers.Rows.Count == 0)
                    return;

                DataGridViewSelectedRowCollection selectedRows = DgvListUsers.SelectedRows;

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
                if (DgvListUsers.Rows.Count == 0)
                    return;

                // Validate single selection
                var selectedRows = DgvListUsers.SelectedRows;
                if (selectedRows.Count > 1)
                {
                    throw new Exception("Please select only one row at a time");
                }

                // Helper methods for safe value conversion
                string GetSafeValue(object value) =>
                    value != null && value != DBNull.Value && !string.IsNullOrWhiteSpace(value.ToString())
                        ? value.ToString()
                        : string.Empty;

                int GetSafeInteger(object value) =>
                    value != null && value != DBNull.Value && int.TryParse(value.ToString(), out int result)
                        ? result
                        : 0;

                decimal GetSafeDecimal(object value) =>
                    value != null && value != DBNull.Value && decimal.TryParse(value.ToString(), out decimal result)
                        ? result
                        : 0m;

                long GetSafeLong(object value) =>
                    value != null && value != DBNull.Value && long.TryParse(value.ToString(), out long result)
                        ? result
                        : 0L;

                DateTime GetSafeDate(object value) =>
                    value != null && value != DBNull.Value && DateTime.TryParse(value.ToString(), out DateTime result)
                        ? result
                        : DateTime.MinValue;

                bool GetSafeBoolean(object value) =>
                    value != null && value != DBNull.Value && bool.TryParse(value.ToString(), out bool result)
                        && result;

                // Clear existing values
                CmbUnitCode.Text = string.Empty;
                TxtUserLoginName.Text = string.Empty;
                TxtUserFirstName.Text = string.Empty;
                TxtUserLastName.Text = string.Empty;
                CmbDept.Text = string.Empty;
                TxtUserEmail.Text = string.Empty;
                TxtUserContact.Text = string.Empty;
                CmbUserRole.Text = string.Empty;
                CmbUserLocked.Text = "No";
                CmbUserDeleted.Text = "No";

                TxtEnterPwd.Text = string.Empty;
                TxtConfirmPwd.Text = string.Empty;
                TxtEnterMstPwd.Text = string.Empty;
                TxtConfirmMstPwd.Text = string.Empty;

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

                TxtRecordId.Text = rowId.ToString();
                TxtRecordId.Tag = null;

                // Get data for selected row

                TsLblInputStatus.Text = "Fetching row. Please wait...";
                TsLblInputStatus.ForeColor = Color.DarkBlue;

                string mTableName = "dbo.v_UserLogins";

                string mUnitCode = (CmbUnitCode == null || string.IsNullOrWhiteSpace(CmbUnitCode.Text))
                   ? ClassGlobalVariables.pubUnitCode
                   : CmbUnitCode.Text;

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
                TxtRecordId.Tag = GetSafeInteger(row["RowVersion"]);
                CmbUnitCode.Text = GetSafeValue(row["UnitCode"]);
                TxtUserLoginName.Text = GetSafeValue(row["UserName"]);
                TxtUserFirstName.Text = GetSafeValue(row["FirstName"]);
                TxtUserLastName.Text = GetSafeValue(row["LastName"]);
                CmbDept.Text = GetSafeValue(row["DepartmentCode"]);
                TxtUserEmail.Text = GetSafeValue(row["Email"]);
                TxtUserContact.Text = GetSafeValue(row["PhoneNumber"]);
                CmbUserRole.Text = GetSafeValue(row["RoleName"]);
                CmbUserLocked.Text = GetSafeBoolean(row["IsLockedOut"]) ? "Yes" : "No";
                CmbUserDeleted.Text = GetSafeBoolean(row["IsDeleted"]) ? "Yes" : "No";

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

            CmbUnitCode.Focus();

            TsLblInputFooter.Text = "Add all required information and click on Save.";
            Application.DoEvents();
        }

        #endregion

        #region "TblBtnSave_Click"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TblBtnSave_Click(System.Object sender, System.EventArgs e)
        {
            try
            {

                // Call the ValidInput function and await its result
                bool isValidInput = ValidateInput();
                if (!isValidInput)
                    return;

                var msgResponse = MessageBox.Show("Are you sure you want to Save changes ?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (msgResponse == DialogResult.No)
                    return;

                if (await UpsertRowsAsync())
                {
                    // Update was successful
                    MessageBox.Show("Row inserted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearInputs();

                    // Update DataGridView
                  await  LoadDataGridViewAsync();
                }
                else
                    // Update failed
                    MessageBox.Show("Failed to insert row.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region "UpsertRowsAsync"
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UpsertRowsAsync()
        {
            try
            {
                const string tableName = "dbo.UserLogins";
                const string sqlProcedureName = "dbo.usp_UserLogins_Upsert";

                string unitCode = (CmbUnitCode == null || string.IsNullOrWhiteSpace(CmbUnitCode.Text))
                    ? ClassGlobalVariables.pubUnitCode
                    : CmbUnitCode.Text;

                var parameters = new List<SqlParameter>
                {
                    ClassDbHelpers.CreateSqlParameter("@UnitCode", SqlDbType.VarChar, myDataTable.UnitCode),
                    ClassDbHelpers.CreateSqlParameter("@UserName", SqlDbType.VarChar, myDataTable.UserName),
                    ClassDbHelpers.CreateSqlParameter("@DeptId", SqlDbType.Int, myDataTable.DeptId),
                    ClassDbHelpers.CreateSqlParameter("@Email", SqlDbType.VarChar, myDataTable.Email),
                    ClassDbHelpers.CreateSqlParameter("@PhoneNumber", SqlDbType.NVarChar, myDataTable.PhoneNumber),
                    ClassDbHelpers.CreateSqlParameter("@RoleId", SqlDbType.Int, myDataTable.RoleId),
                    ClassDbHelpers.CreateSqlParameter("@CompanyId", SqlDbType.Int, myDataTable.CompanyId),
                    ClassDbHelpers.CreateSqlParameter("@FirstName", SqlDbType.VarChar, myDataTable.FirstName),
                    ClassDbHelpers.CreateSqlParameter("@LastName", SqlDbType.VarChar, myDataTable.LastName),
                    ClassDbHelpers.CreateSqlParameter("@IsLockedOut", SqlDbType.Bit, myDataTable.IsLockedOut),
                    ClassDbHelpers.CreateSqlParameter("@IsDeleted", SqlDbType.Bit, myDataTable.IsDeleted),
                    ClassDbHelpers.CreateSqlParameter("@IpAddsCreated", SqlDbType.VarChar, myDataTable.IpAddsCreated),
                    ClassDbHelpers.CreateSqlParameter("@UserRowGuid", SqlDbType.VarChar, myDataTable.UserRowGuid.ToString()),
                    ClassDbHelpers.CreateSqlParameter("@RowVersion", SqlDbType.Int, myDataTable.RowVersion),
                    ClassDbHelpers.CreateSqlParameter("@RowId", SqlDbType.Int, myDataTable.Id)
                };

                string connectionString = ClassGlobalFunctions.GetConnectionStringByUnitCode(unitCode);

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
        }
        #endregion

        #region "TsBtnUpdatePwd_Click"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TsBtnUpdatePwd_Click(object sender, EventArgs e)
        {
            try
            {

                // Call the ValidInput function and await its result
                bool isValidInput = ValidateInputPassword();
                if (!isValidInput)
                    return;

                bool success = await UpdatePasswordAsync();

                if (success)
                    await LoadDataGridViewAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update Password Click", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region "UpdatePasswordAsync"
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UpdatePasswordAsync()
        {
            try
            {
                string unitCode = (CmbUnitCode == null || string.IsNullOrWhiteSpace(CmbUnitCode.Text))
                    ? ClassGlobalVariables.pubUnitCode
                    : CmbUnitCode.Text;

                string connectionString = ClassGlobalFunctions.GetConnectionStringByUnitCode(unitCode);
                const string tableName = "dbo.UserLogins";

                // Generate salt and hash
                string salt = PasswordHelper.GenerateSalt();
                byte[] saltBytes = Encoding.UTF8.GetBytes(salt);
                byte[] hashedPasswordBytes = PasswordHelper.HashPasswordToBytes(TxtEnterPwd.Text, salt);

                string query = $@"UPDATE {tableName} SET 
                        PasswordHash = @PasswordHash, 
                        Salt = @Salt, 
                        RowVersion = RowVersion + 1 
                        WHERE [Id] = @RowId 
                        AND RowVersion = @RowVersion";

                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync().ConfigureAwait(false);

                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            using (var command = new SqlCommand(query, connection, transaction))
                            {
                                command.Parameters.Add("@PasswordHash", SqlDbType.VarBinary).Value = hashedPasswordBytes;
                                command.Parameters.Add("@Salt", SqlDbType.VarBinary).Value = saltBytes;
                                command.Parameters.Add("@RowId", SqlDbType.Int).Value = Convert.ToInt32(TxtRecordId.Text);
                                command.Parameters.Add("@RowVersion", SqlDbType.Int).Value = Convert.ToInt32(TxtRecordId.Tag);

                                int rowsAffected = await command.ExecuteNonQueryAsync().ConfigureAwait(false);

                                if (rowsAffected > 0)
                                {
                                    transaction.Commit();  // Synchronous in .NET Framework 4.8
                                    return true;
                                }
                                else
                                {
                                    transaction.Rollback();  // Synchronous in .NET Framework 4.8
                                    ShowMessage("Row version conflict. Try again later.", "Update Password", MessageBoxIcon.Information);
                                    return false;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();  // Synchronous in .NET Framework 4.8
                            ShowMessage(ex.Message, "Update Error", MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, "Update Password", MessageBoxIcon.Information);
                return false;
            }
        }
        #endregion

        #region "TsBtnUpdateMstPwd_Click"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TsBtnUpdateMstPwd_Click(object sender, EventArgs e)
        {
            try
            {

                // Call the ValidInput function and await its result
                bool isValidInput = ValidateInputMasterPassword();
                if (!isValidInput)
                    return;

                bool success = await UpdateMasterPasswordAsync();

                if (success)
                    await LoadDataGridViewAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update Password Click", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region "UpdateMasterPasswordAsync"
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UpdateMasterPasswordAsync()
        {
            try
            {
                string unitCode = (CmbUnitCode == null || string.IsNullOrWhiteSpace(CmbUnitCode.Text))
                    ? ClassGlobalVariables.pubUnitCode
                    : CmbUnitCode.Text;

                string connectionString = ClassGlobalFunctions.GetConnectionStringByUnitCode(unitCode);
                const string tableName = "dbo.UserLogins";

                // Generate salt and hash
                string salt = PasswordHelper.GenerateSalt();
                byte[] saltBytes = Encoding.UTF8.GetBytes(salt);
                byte[] hashedPasswordBytes = PasswordHelper.HashPasswordToBytes(TxtEnterMstPwd.Text, salt);

                string query = $@"UPDATE {tableName} SET 
                        MasterPasswordHash = @PasswordHash, 
                        MasterPasswordSalt = @Salt, 
                        RowVersion = RowVersion + 1 
                        WHERE [Id] = @RowId 
                        AND RowVersion = @RowVersion";

                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync().ConfigureAwait(false);

                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            using (var command = new SqlCommand(query, connection, transaction))
                            {
                                command.Parameters.Add("@PasswordHash", SqlDbType.VarBinary).Value = hashedPasswordBytes;
                                command.Parameters.Add("@Salt", SqlDbType.VarBinary).Value = saltBytes;
                                command.Parameters.Add("@RowId", SqlDbType.Int).Value = Convert.ToInt32(TxtRecordId.Text);
                                command.Parameters.Add("@RowVersion", SqlDbType.Int).Value = Convert.ToInt32(TxtRecordId.Tag);

                                int rowsAffected = await command.ExecuteNonQueryAsync().ConfigureAwait(false);

                                if (rowsAffected > 0)
                                {
                                    transaction.Commit();  // Synchronous in .NET Framework 4.8
                                    return true;
                                }
                                else
                                {
                                    transaction.Rollback();  // Synchronous in .NET Framework 4.8
                                    ShowMessage("Row version conflict. Try again later.", "Update Password", MessageBoxIcon.Information);
                                    return false;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();  // Synchronous in .NET Framework 4.8
                            ShowMessage(ex.Message, "Update Error", MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, "Update Password", MessageBoxIcon.Information);
                return false;
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

                ClassLayoutHelper.ClearErrorsTableLayout(TableLayout2, errorProvider1);
                ClassLayoutHelper.ClearControlsTableLayout(TableLayout2);

                ClassLayoutHelper.ClearErrorsTableLayout(TableLayout3, errorProvider1);
                ClassLayoutHelper.ClearControlsTableLayout(TableLayout3);

                TxtRecordId.Text = null;
                TxtRecordId.Tag = null;
                TsLblInputFooter.Text = "...";
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

        #region "ValidateInput"

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool ValidateInput()
        {
            try
            {
                TsLblInputStatus.ForeColor = Color.Red;
                TsLblInputStatus.Text = "Validating data. Please wait...";

                // Remove existing errors
                ClassLayoutHelper.ClearErrorsTableLayout(TableLayout1, errorProvider1);

                // Required control validations
                ClassValidationHelper.ValidateControl(CmbUnitCode, "Plant Code is required.", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateControl(TxtUserLoginName, "User Login Name is required.", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateControl(TxtUserFirstName, "First Name is required.", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateControl(TxtUserLastName, "Last Name is required.", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateControl(CmbDept, "Department is required.", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateControl(TxtUserEmail, "Email is required.", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateControl(CmbUserRole, "User Role is required.", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateControl(CmbUserLocked, "User Locked Status is required.", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateControl(CmbUserDeleted, "User Delete Status is required.", errorProvider1, TsLblInputStatus);

                // Length validations
                ClassValidationHelper.ValidateTextBoxLength(TxtUserLoginName, 50, "User Login Name", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateTextBoxLength(TxtUserEmail, 100, "User Email", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateTextBoxLength(TxtUserContact, 50, "User Contact Number", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateTextBoxLength(TxtUserFirstName, 50, "User First Name", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateTextBoxLength(TxtUserLastName, 50, "User Last Name", errorProvider1, TsLblInputStatus);

                // Dropdown value validations
                int? selectedDeptId = ClassValidationHelper.ValidateCmbSelectedValue(CmbDept, "Department is required.", errorProvider1, TsLblInputStatus);
                int? selectedUserRoleId = ClassValidationHelper.ValidateCmbSelectedValue(CmbUserRole, "User Role is required.", errorProvider1, TsLblInputStatus);
                int? selectedUnitId = ClassValidationHelper.ValidateCmbSelectedValue(CmbUnitCode, "Plant Code is required.", errorProvider1, TsLblInputStatus);

                // Email validation
                string userEmail = TxtUserEmail.Text.Trim().ToLower();
                if (!ClassValidationHelper.IsValidEmail(userEmail))
                {
                    string errorMessage = "Invalid Email.";
                    errorProvider1.SetError(TxtUserEmail, errorMessage);
                    throw new Exception(errorMessage);
                }

                // Boolean selections
                bool isLockedOut = string.Equals(CmbUserLocked.Text, "Yes", StringComparison.OrdinalIgnoreCase);
                bool isDeleted = string.Equals(CmbUserDeleted.Text, "Yes", StringComparison.OrdinalIgnoreCase);

                // Phone number validation
                if (!string.IsNullOrWhiteSpace(TxtUserContact.Text) && !long.TryParse(TxtUserContact.Text, out _))
                {
                    string errorMessage = "Please enter a valid numeric value for contact number.";
                    errorProvider1.SetError(TxtUserContact, errorMessage);
                    throw new Exception(errorMessage);
                }

                // Populate data table
                myDataTable = new UserLogin();

                // Id
                if (!string.IsNullOrEmpty(TxtRecordId.Text) && int.TryParse(TxtRecordId.Text, out int id))
                    myDataTable.Id = id;
                else
                    myDataTable.Id = null;

                // UnitCode
                myDataTable.UnitCode = !string.IsNullOrEmpty(CmbUnitCode.Text)
                    ? CmbUnitCode.Text
                    : null;

                // UserName
                myDataTable.UserName = !string.IsNullOrEmpty(TxtUserLoginName.Text)
                    ? ClassStringHelpers.CleanAndUpperCase(TxtUserLoginName.Text)
                    : null;

                // DeptId
                myDataTable.DeptId = selectedDeptId;

                // Email
                myDataTable.Email = userEmail;

                // PhoneNumber
                myDataTable.PhoneNumber = !string.IsNullOrEmpty(TxtUserContact.Text)
                    ? ClassStringHelpers.CleanAndUpperCase(TxtUserContact.Text)
                    : null;

                // RoleId
                myDataTable.RoleId = selectedUserRoleId;

                // CompanyId
                myDataTable.CompanyId = selectedUnitId;

                // FirstName
                myDataTable.FirstName = !string.IsNullOrEmpty(TxtUserFirstName.Text)
                    ? ClassStringHelpers.CleanAndUpperCase(TxtUserFirstName.Text)
                    : null;

                // LastName
                myDataTable.LastName = !string.IsNullOrEmpty(TxtUserLastName.Text)
                    ? ClassStringHelpers.CleanAndUpperCase(TxtUserLastName.Text)
                    : null;

                // IsLockedOut
                myDataTable.IsLockedOut = isLockedOut;

                // IsDeleted
                myDataTable.IsDeleted = isDeleted;

                // IPAddress
                myDataTable.IpAddsCreated = ClassGlobalVariables.pubHostIPAddress;

                // UserRowGuid
                if (Guid.TryParse(ClassGlobalVariables.pubLoginUserRowGuid, out Guid guidValue))
                    myDataTable.UserRowGuid = guidValue;
                else
                    myDataTable.UserRowGuid = null;

                // RowVersion
                if (TxtRecordId.Tag != null && int.TryParse(TxtRecordId.Tag.ToString(), out int version))
                    myDataTable.RowVersion = version;
                else
                    myDataTable.RowVersion = null;


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

        #region "ValidateInputPassword"

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool ValidateInputPassword()
        {
            try
            {
                TsLblSetPwdFooter.ForeColor = Color.Red;
                TsLblSetPwdFooter.Text = "Validating data. Please wait...";

                // Remove Set Errors
                ClassLayoutHelper.ClearErrorsTableLayout(TableLayout2, errorProvider1);

                if (string.IsNullOrWhiteSpace(TxtRecordId.Text))
                    throw new Exception("No user selected");

                ClassValidationHelper.ValidateControl(TxtEnterPwd, "User Password is required.", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateControl(TxtConfirmPwd, "Confirm Password is required.", errorProvider1, TsLblInputStatus);

                // Case-Sensitive Version
                // StringComparison.Ordinal ensures byte-level, case-sensitive comparison.
                if (string.Compare(TxtEnterPwd.Text, TxtConfirmPwd.Text, StringComparison.Ordinal) != 0)
                    throw new Exception("Password is not matching");

                TsLblSetPwdFooter.ForeColor = Color.DarkBlue;
                TsLblSetPwdFooter.Text = "Done...";

                return true;
            }
            catch (Exception ex)
            {
                TsLblSetPwdFooter.ForeColor = Color.Red;
                TsLblSetPwdFooter.Text = ex.Message;

                MessageBox.Show(ex.Message, "Validate Input Password", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return false;
            }
        }


        #endregion

        #region "ValidateInputMasterPassword"

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool ValidateInputMasterPassword()
        {
            try
            {
                TsLblFooterMstPwd.ForeColor = Color.Red;
                TsLblFooterMstPwd.Text = "Validating data. Please wait...";

                // Remove Set Errors
                ClassLayoutHelper.ClearErrorsTableLayout(TableLayout3, errorProvider1);

                if (string.IsNullOrWhiteSpace(TxtRecordId.Text))
                    throw new Exception("No user selected");

                ClassValidationHelper.ValidateControl(TxtEnterMstPwd, "User Password is required.", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateControl(TxtConfirmMstPwd, "Confirm Password is required.", errorProvider1, TsLblInputStatus);

                // Case-Sensitive Version
                // StringComparison.Ordinal ensures byte-level, case-sensitive comparison.
                if (string.Compare(TxtEnterMstPwd.Text, TxtConfirmMstPwd.Text, StringComparison.Ordinal) != 0)
                    throw new Exception("Password is not matching");

                TsLblFooterMstPwd.ForeColor = Color.DarkBlue;
                TsLblFooterMstPwd.Text = "Done...";

                return true;
            }
            catch (Exception ex)
            {
                TsLblFooterMstPwd.ForeColor = Color.Red;
                TsLblFooterMstPwd.Text = ex.Message;

                MessageBox.Show(ex.Message, "Validate Input Master Password", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return false;
            }
        }


        #endregion
 

    }
}

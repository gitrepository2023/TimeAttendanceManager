using TimeAttendanceManager.Helpers.Classes;
using TimeAttendanceManager.Main.Classes;
using TimeAttendanceManager.Main.Forms;
using TimeAttendanceManager.MenuDesign.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
///    Title:          Code for Main Menu Structure
///                    
/// 
///    Name:           FrmAdminMenuStructure.cs
///    Created:        18th August 2025
///    Date Completed: 18th August 2025
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

namespace TimeAttendanceManager.MenuDesign.Forms
{
    public partial class FrmAdminMenuStructure : Form
    {

        #region "Constructor"
        public FrmAdminMenuStructure()
        {
            InitializeComponent();

            this.Load += Form_Load;

            // remove events
            this.FormClosing -= FormClosing_FormClosing;
            this.TsBtnCloseForm.Click -= TsBtnCloseForm_Click;
            this.TsBtnClose.Click -= TsBtnCloseForm_Click;
            this.TabCtrlMain.Click -= TabCtrlMain_Click;
            this.TsBtnSidePanel.Click -= TsBtnSideMenu_Click;
            this.TsBtRefreshDgv.Click -= TsBtRefreshDgv_Click;
            this.TsBtnClearSearchDgv.Click -= TsBtRefreshDgv_Click;
            this.TsBtnSearchDgv.Click -= TsBtRefreshDgv_Click;
            this.TsTxtSearchDgv.KeyDown -= TsTxtSearchDgv_KeyDown;
            this.TsBtnDrpColsDgvTblCols.DropDownItemClicked -= TsBtnDrpColsDgvTblCols_DropDownItemClicked;
            this.DgvList.CellClick -= DgvList_CellClick;
            this.TsBtnAddNew.Click -= TsBtnAddNew_Click;
            this.TsBtnSave.Click -= TblBtnSave_Click;
            this.TsBtnDelRow.Click -= TsBtnDelRow_Click;
            this.TsBtnUpsert.Click -= TsBtnUpsert_Click;
            this.TsBtnUpsertUserMenu.Click -= TsBtnUpsertUserMenu_Click;
            this.BtnPopFormNames.Click -= BtnPopFormNames_Click;
            this.CmbUnitCode.SelectedIndexChanged -= CmbUnitCode_SelectedIndexChanged;

            // add events
            this.FormClosing += FormClosing_FormClosing;
            this.TsBtnCloseForm.Click += TsBtnCloseForm_Click;
            this.TsBtnClose.Click += TsBtnCloseForm_Click;
            this.TabCtrlMain.Click += TabCtrlMain_Click;
            this.TsBtnSidePanel.Click += TsBtnSideMenu_Click;
            this.TsBtRefreshDgv.Click += TsBtRefreshDgv_Click;
            this.TsBtnClearSearchDgv.Click += TsBtRefreshDgv_Click;
            this.TsBtnSearchDgv.Click += TsBtRefreshDgv_Click;
            this.TsTxtSearchDgv.KeyDown += TsTxtSearchDgv_KeyDown;
            this.TsBtnDrpColsDgvTblCols.DropDownItemClicked += TsBtnDrpColsDgvTblCols_DropDownItemClicked;
            this.DgvList.CellClick += DgvList_CellClick;
            this.TsBtnAddNew.Click += TsBtnAddNew_Click;
            this.TsBtnSave.Click += TblBtnSave_Click;
            this.TsBtnDelRow.Click += TsBtnDelRow_Click;
            this.TsBtnUpsert.Click += TsBtnUpsert_Click;
            this.TsBtnUpsertUserMenu.Click += TsBtnUpsertUserMenu_Click;
            this.BtnPopFormNames.Click += BtnPopFormNames_Click;
            this.CmbUnitCode.SelectedIndexChanged += CmbUnitCode_SelectedIndexChanged;
        }
        #endregion

        #region "LocalVariables"
        private ContextMenuStrip myContextMenu = new ContextMenuStrip();   // Storing Context Menu
        private AdminMenuStructure myDataTable = new AdminMenuStructure();
        private DataTable myDt = new DataTable();

        private bool pubIsActiveMenuItems = true;
        private bool pubIsDeletedMenuItems = false;
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

                    // Load default values
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
                SplitData.SplitterDistance = (int)(this.SplitData.Height * 0.5);
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

                string mUnitCode = (CmbUnitCode == null || string.IsNullOrWhiteSpace(CmbUnitCode.Text))
                   ? ClassGlobalVariables.pubUnitCode
                   : CmbUnitCode.Text;

                // Load combo boxes with data
                this.CmbUnitCode.SelectedIndexChanged -= CmbUnitCode_SelectedIndexChanged;
                await ClassGlobalFunctions.FillComboBoxAsync("UnitCode", "Id", CmbUnitCode, "Companies", mUnitCode);
                this.CmbUnitCode.SelectedIndexChanged += CmbUnitCode_SelectedIndexChanged;

                await ClassGlobalFunctions.FillComboBoxAsync("MenuNodeType", "Id", CmbNodeType, "AdminMenuNodeTypes", mUnitCode);

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

                await ClassGlobalFunctions.FillComboForUnitCodeAsync(
                    "CatgName", 
                    "Id", 
                    CmbEmpCategory,
                    "v_Master_EmploymentTypes", 
                    mUnitCode);

                await ClassGlobalFunctions.FillComboForUnitCodeMenuParentAsync(
                    "MenuFormTitle", 
                    "Id", 
                    CmbMenuGroup, 
                    "v_AdminMenuStructures", 
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

        #region "TsBtnDrpColsDgvTblCols_DropDownItemClicked"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsBtnDrpColsDgvTblCols_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                if (e.ClickedItem is ToolStripMenuItem menuItem && menuItem.Tag is string colName)
                {
                    menuItem.Checked = !menuItem.Checked;

                    if (DgvList.Columns.Contains(colName))
                    {
                        DgvList.Columns[colName].Visible = menuItem.Checked;
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
                    $"Function: {nameof(TsBtnDrpColsDgvTblCols_DropDownItemClicked)}",
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

                var serachColumns = new List<string> { "UnitCode",
                    "EmpCatgName",
                    "ParentTitle",
                    "MenuNodeType",
                    "MenuFormTitle",
                    "MenuFormName" };

                var parameters = new List<SqlParameter>();
                // parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit) { Value = pubIsActiveMenuItems });
                // parameters.Add(new SqlParameter("@IsDeleted", SqlDbType.Bit) { Value = pubIsDeletedMenuItems });

                // Determine the row limit based on TsCmbLimitRows input:
                // If the text is "(all)", set limit to 0
                // Otherwise, try parsing to int; if fails, default to 50
                // ?. ensures Text is not null before calling Trim()(null - conditional operator).
                int mLimitRows =
                    TsCmbLimitRows.Text?.Trim().ToLower() == "(all)" ? 0 :
                    int.TryParse(TsCmbLimitRows.Text, out var value) ? value : 50;

                string orderBy = "UnitCode, EmpCatgSortOrder, MenuSortOrder";
                string mTableName = "v_AdminMenuStructures";

                // Get data asynchronously
                DataTable resultTable = await ClassDbHelpers.GetRowsFromTableAsync(
                    mUnitCode,  // plant code
                    mTableName, // sql table name
                    null,   // selectedColumns
                    serachColumns,  // search sql table columns
                    parameters, // sql parameters
                    searchText, // search text
                    orderBy,    // sql order by columns
                    "IsDeleted", // table column name for IsDeleted
                    pubIsDeletedMenuItems, // isDeletedValue
                    "MenuIsActive", // table column name for IsActive
                    pubIsActiveMenuItems, // isActiveValue
                    mLimitRows);

                // Check for empty results
                if (resultTable == null || resultTable.Rows.Count == 0)
                {
                    TsLblDgvListFooter.Text = "No rows found for given parameters.";
                    return;
                }

                // Create a filtered DataTable with only the columns we want
                DataTable filteredTable = new DataTable();
                var columnsToInclude = new List<string> { "Id", 
                    "UnitCode",
                    "EmpCatgName",
                    "ParentTitle",
                    "MenuNodeType",
                    "MenuFormTitle",
                    "MenuFormName",
                    "MenuSortOrder" };

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
                    DgvList.Visible = false;
                }));

                // Set data source
                DgvList.Invoke((MethodInvoker)(() => DgvList.DataSource = filteredTable));

                // Define and apply custom headers
                var customHeaders = new Dictionary<string, string>
                    {
                        {"Id", "Id"},
                        {"UnitCode", "Plant"},
                        {"EmpCatgName", "Employee Category"},
                        {"ParentTitle", "Parent Title"},
                        {"MenuNodeType", "Menu Node Type"},
                        {"MenuFormTitle", "Menu Form Title"},
                        {"MenuFormName", "Menu Form Name"},
                        {"MenuSortOrder", "Sort Order"}
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
            if (TsBtnDrpColsDgvTblCols.GetCurrentParent()?.InvokeRequired ?? false)
            {
                TsBtnDrpColsDgvTblCols.GetCurrentParent().Invoke((MethodInvoker)AddColumnVisibilityDgvItems);
                return;
            }

            // Clear existing items (if any)
            TsBtnDrpColsDgvTblCols.DropDownItems.Clear();

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
                TsBtnDrpColsDgvTblCols.DropDownItems.Add(menuItem);
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

                TsTxtRowId.Text = rowId.ToString();
                TsTxtRowId.Tag = null;

                // Get data for selected row

                TsLblInputStatus.Text = "Fetching row. Please wait...";
                TsLblInputStatus.ForeColor = Color.DarkBlue;

                string mTableName = "dbo.v_AdminMenuStructures";

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
                TsTxtRowId.Tag = GetSafeInteger(row["RowVersion"]);
                CmbUnitCode.Text = GetSafeValue(row["UnitCode"]);
                CmbEmpCategory.Text = GetSafeValue(row["EmpCatgName"]);
                CmbMenuGroup.Text = GetSafeValue(row["ParentTitle"]);
                CmbNodeType.Text = GetSafeValue(row["MenuNodeType"]);
                TxtFormName.Text = GetSafeValue(row["MenuFormName"]);
                TxtFormTitle.Text = GetSafeValue(row["MenuFormTitle"]);
                
                // NumSortOrder.Text = (row["MenuSortOrder"] is int i ? i : 0).ToString();
                // Handle all cases: null, DBNull, string, and other numeric types
                NumSortOrder.Text = Convert.ToInt32(row["MenuSortOrder"] ?? 0).ToString();

                CmbIconName.Text = GetSafeValue(row["MenuIconName"]);

                bool isAdmin = GetSafeBoolean(row["MenuIsAdmin"]);
                bool isProtected = GetSafeBoolean(row["MenuIsProtected"]);
                bool isActive = GetSafeBoolean(row["MenuIsActive"]);

                ChkIsAdmin.Checked = isAdmin;
                ChkIsProtected.Checked = isProtected;
                ChkIsActive.Checked = isActive;

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

        #region "BtnPopFormNames_Click"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnPopFormNames_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate plant code
                if (string.IsNullOrWhiteSpace(CmbUnitCode.Text))
                {
                    throw new Exception("Please select Plant code");
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

                string mUnitCode = CmbUnitCode.Text;
                string mTableName = "AdminMenuFormNames";

                // Open selection dialog
                int? mRowId = null;
                using (var myDialog = new FrmModalFormNames(mUnitCode))
                {
                    if (myDialog.ShowDialog() == DialogResult.OK)
                    {
                        mRowId = myDialog.SelectedRowId;
                        TxtFormName.Tag = mRowId;
                    }
                }

                // Handle no selection case
                if (!mRowId.HasValue)
                {
                    TxtFormName.Text = null;
                    TxtFormName.Tag = null;
                    MessageBox.Show("No suitable document selected",
                                 "Material Selection",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Information);
                    return;
                }

                // Retrieve and display data

                DataTable myDt = await ClassDbHelpers.GetSelectedRowDataAsync(
                    rowId: mRowId.Value,
                    tableName: mTableName,
                    unitCode: mUnitCode);
               
                if (myDt?.Rows.Count > 0)  // Null-conditional operator
                {
                    foreach (DataRow row in myDt.Rows)
                    {
                        int? rowId = GetSafeInteger(row["Id"]);
                        string formName = GetSafeValue(row["FormName"]?.ToString());
                        string formTitle = GetSafeValue(row["FormTitle"]?.ToString());

                        TxtFormName.Text = formName;

                        TxtFormTitle.Text = formTitle;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                              "Get Form Details",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
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
                    await LoadDataGridViewAsync();
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

        #region "TsBtnDelRow_Click"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TsBtnDelRow_Click(object sender, EventArgs e)
        {
            // 1. Get selected rows with valid "Id" column
            List<int> idsToDelete = new List<int>();

            foreach (DataGridViewRow row in DgvList.SelectedRows)
            {
                // Skip if the row is a header or new row
                if (row.IsNewRow || row.Index < 0)
                    continue;

                // Check if "Id" column exists
                if (DgvList.Columns["Id"] != null && row.Cells["Id"].Value != null)
                {
                    if (int.TryParse(row.Cells["Id"].Value.ToString(), out int id))
                    {
                        idsToDelete.Add(id); // Add ID to the list
                    }
                    else
                    {
                        TsLblInputStatus.Text = $"Invalid ID in row {row.Index}: {row.Cells["Id"].Value}";
                    }
                }
                else
                {
                    TsLblInputStatus.Text = $"Row {row.Index} has no 'Id' column or value is null.";
                }
            }

            // If no valid IDs found, show a message
            if (idsToDelete.Count == 0)
            {
                MessageBox.Show("No valid rows selected for deletion.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
                await DeleteSelectedRowsAsync(idsToDelete);
            }
        }
        #endregion

        #region "DeleteSelectedRowsAsync"
        private async Task<bool> DeleteSelectedRowsAsync(List<int> idsToDelete)
        {
            // 1. Call database helper
            bool success = await ClassDbHelpers.DeleteRowsAsync(
                tableName: "AdminMenuStructures",
                idColumnName: "Id",
                ids: idsToDelete,
                softDelete: true,
                deletedBy: ClassGlobalVariables.pubLoginUserRowGuid
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

        #region "UpsertRowsAsync"
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task<bool> UpsertRowsAsync()
        {
            try
            {
                const string tableName = "dbo.AdminMenuStructures";
                const string sqlProcedureName = "dbo.usp_AdminMenuStructures_Upsert";

                string unitCode = (CmbUnitCode == null || string.IsNullOrWhiteSpace(CmbUnitCode.Text))
                    ? ClassGlobalVariables.pubUnitCode
                    : CmbUnitCode.Text;

                var parameters = new List<SqlParameter>
                {
                    ClassDbHelpers.CreateSqlParameter("@UnitCode", SqlDbType.VarChar, myDataTable.UnitCode),
                    ClassDbHelpers.CreateSqlParameter("@MenuEmpCatgId", SqlDbType.Int, myDataTable.MenuEmpCatgId),
                    ClassDbHelpers.CreateSqlParameter("@MenuParentId", SqlDbType.Int, myDataTable.MenuParentId),
                    ClassDbHelpers.CreateSqlParameter("@MenuNodeType", SqlDbType.VarChar, myDataTable.MenuNodeType),
                    ClassDbHelpers.CreateSqlParameter("@MenuFormTitle", SqlDbType.NVarChar, myDataTable.MenuFormTitle),
                    ClassDbHelpers.CreateSqlParameter("@MenuFormName", SqlDbType.NVarChar, myDataTable.MenuFormName),
                    ClassDbHelpers.CreateSqlParameter("@MenuSortOrder", SqlDbType.Int, myDataTable.MenuSortOrder),
                    ClassDbHelpers.CreateSqlParameter("@MenuIconName", SqlDbType.NVarChar, myDataTable.MenuIconName),
                    ClassDbHelpers.CreateSqlParameter("@MenuIsAdmin", SqlDbType.Bit, myDataTable.MenuIsAdmin),
                    ClassDbHelpers.CreateSqlParameter("@MenuIsProtected", SqlDbType.Bit, myDataTable.MenuIsProtected),
                    ClassDbHelpers.CreateSqlParameter("@MenuIsActive", SqlDbType.Bit, myDataTable.MenuIsActive),
                    ClassDbHelpers.CreateSqlParameter("@RowVersion", SqlDbType.Int, myDataTable.RowVersion),

                    ClassDbHelpers.CreateSqlParameter("@UserId", SqlDbType.Int, myDataTable.UserId),
                    ClassDbHelpers.CreateSqlParameter("@UserRowGuid", SqlDbType.VarChar, myDataTable.UserRowGuid.ToString()),
                    ClassDbHelpers.CreateSqlParameter("@IpAddsCreated", SqlDbType.VarChar, myDataTable.IpAddsCreated),
                    ClassDbHelpers.CreateSqlParameter("@HostName", SqlDbType.VarChar, myDataTable.HostName),
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

        #region "TsBtnUpsert_Click"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TsBtnUpsert_Click(object sender, EventArgs e)
        {
            // Show the wait form
            var progress = new FrmAdminProgress();

            try
            {
                // Construct the event log message
                var sb = new StringBuilder();
                sb.AppendLine("Please read before continuing.");
                sb.AppendLine();
                sb.AppendLine("Note: This will update table [dbo.Menu_FormNames]");
                sb.AppendLine("New Form Names will be INSERTED with a prefix of FrmMM.");
                sb.AppendLine();
                sb.AppendLine("Existing Form Names and titles will be UPDATED.");
                sb.AppendLine();
                sb.AppendLine("Please update the form group after executing this function.");
                sb.AppendLine();
                sb.AppendLine();
                sb.AppendLine("Only [SUPER] and [ADMIN] users can perform this operation.");

                // Show the message box
                var msgResponse = MessageBox.Show(
                    "Are you sure you want to continue?\n\n" + sb.ToString(),
                    "Update Menu Items",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                // Exit if user chooses 'No'
                if (msgResponse == DialogResult.No) return;

                // Check if the user role is "SUPER"
                if (ClassUserSession.Instance.LoginUserRole.Equals("SUPER", StringComparison.OrdinalIgnoreCase) ||
                    ClassUserSession.Instance.LoginUserRole.Equals("ADMIN", StringComparison.OrdinalIgnoreCase))
                {
                    progress.LblStatus.Text = "Updating form details.";
                    progress.LblMoreStatus.Text = "Please wait...";
                    progress.ProgressBar1.Visible = false;
                    progress.Show();
                    Application.DoEvents();

                    await ClassDbHelpers.UpsertFormsToDatabaseAsync();
                    return;
                }

                string mPassword = null;
                bool isValidMasterPassword = false;
                int attempts = 0;

                while (attempts < 3 && !isValidMasterPassword)
                {
                    attempts++;

                    // Show the password input dialog
                    using (var f = new FrmAdminInputBox())
                    {
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            mPassword = f.TxtPassword.Text;
                        }
                    }

                    if (!string.IsNullOrEmpty(mPassword))
                    {
                        // Verify the password
                        isValidMasterPassword = await ClassDbHelpers.ValidateMasterCredentialsAsync(
                            ClassUserSession.Instance.LoginUserName,
                            mPassword,
                            ClassUserSession.Instance.LoginUserUnitCode
                        );

                        if (isValidMasterPassword)
                        {
                            progress.LblStatus.Text = "Updating form details.";
                            progress.LblMoreStatus.Text = "Please wait...";
                            progress.ProgressBar1.Visible = false;
                            progress.Show();
                            Application.DoEvents();

                            await ClassDbHelpers.UpsertFormsToDatabaseAsync();
                        }
                        else
                        {
                            // Incorrect password, show message
                            MessageBox.Show(
                                $"Wrong Password. You have {3 - attempts} attempt(s) left.",
                                "Information",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );
                        }
                    }
                    else
                    {
                        MessageBox.Show(
                            "Password cannot be empty. Access Denied",
                            "Information",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                }

                // If all attempts are used and password is incorrect
                if (!isValidMasterPassword)
                {
                    MessageBox.Show(
                        "Access Denied. You have used all attempts.",
                        "Access Denied",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"{ex.Message}\nFunction: {System.Reflection.MethodBase.GetCurrentMethod().Name}",
                    "Button Upsert Click",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            finally
            {
                progress.Close();
                progress.Dispose();
            }
        }
        #endregion

        #region "TsBtnUpsertUserMenu_Click"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TsBtnUpsertUserMenu_Click(object sender, EventArgs e)
        {

            var message = new StringBuilder();
            message.AppendLine("This will MERGE rows in table dbo.AdminMenuUserPerms for:");
            message.AppendLine("• Each active UserId in table dbo.UserLogins");
            message.AppendLine("• Each active menu item in table dbo.AdminMenuStructures");
            message.AppendLine("");
            message.AppendLine("The operation will:");
            message.AppendLine("✓ Grant permissions for active menus to active users");
            message.AppendLine("✓ Remove permissions for menus that no longer exist");
            message.AppendLine("✓ Remove permissions for users that are orphaned, inactive, or soft-deleted");
            message.AppendLine("");
            message.AppendLine("This ensures user menu permissions stay synchronized with current users and menus.");

            var msgResponse = MessageBox.Show(message.ToString(), "Synchronize User Menu Permissions",
                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (msgResponse == DialogResult.No)
                return;

            if (await UpserUserMenuItemsAsync())
            {
                // Update was successful
                MessageBox.Show("User menu permissions synchronized successfully.\n" +
                               "Permissions updated for all active users and menus.",
                               "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to synchronize user menu permissions.",
                               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region "UpserUserMenuItemsAsync"
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task<bool> UpserUserMenuItemsAsync() 
        {
            try
            {
                UseWaitCursor = true;

                const string tableName = "dbo.AdminMenuStructures";
                const string sqlProcedureName = "dbo.usp_AdminMenuUserPerms";

                string unitCode = (CmbUnitCode == null || string.IsNullOrWhiteSpace(CmbUnitCode.Text))
                    ? ClassGlobalVariables.pubUnitCode
                    : CmbUnitCode.Text;

                
                string connectionString = ClassGlobalFunctions.GetConnectionStringByUnitCode(unitCode);
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new InvalidOperationException("Connection string cannot be empty");
                }

                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync().ConfigureAwait(false);

                    using (var command = new SqlCommand(sqlProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                       
                        command.Parameters.Add("@GrantedBy", SqlDbType.NVarChar, 150).Value = ClassGlobalVariables.pubLoginUserRowGuid;
                        command.Parameters.Add("@DefaultCanView", SqlDbType.Bit).Value = false;

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

                ChkIsAdmin.CheckState = CheckState.Unchecked;
                ChkIsProtected.CheckState = CheckState.Unchecked;
                ChkIsActive.CheckState = CheckState.Checked;

                TsTxtRowId.Text = null;
                TsTxtRowId.Tag = null;
                TsLblInputFooter.Text = "...";
                TsLblInputStatus.Text = "...";
                TsLblDgvListFooter.Text = "...";
                NumSortOrder.Text = "0";

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
                TsLblInputFooter.ForeColor = Color.Red;
                TsLblInputFooter.Text = "Validating data. Please wait...";

                // Remove existing errors
                ClassLayoutHelper.ClearErrorsTableLayout(TableLayout1, errorProvider1);

                // Required control validations
                ClassValidationHelper.ValidateControl(CmbUnitCode, "Plant Code is required.", errorProvider1, TsLblInputFooter);
                ClassValidationHelper.ValidateControl(CmbEmpCategory, "Employee Category is required.", errorProvider1, TsLblInputFooter);
                ClassValidationHelper.ValidateControl(CmbNodeType, "Menu Node Type is required.", errorProvider1, TsLblInputFooter);
                
                // Length validations
                ClassValidationHelper.ValidateTextBoxLength(TxtFormName, 50, "Form Name", errorProvider1, TsLblInputFooter);
                ClassValidationHelper.ValidateTextBoxLength(TxtFormTitle, 150, "Form Title", errorProvider1, TsLblInputFooter);
                
                // Dropdown value validations
                int? selectedEmpCategoryId = ClassValidationHelper.ValidateCmbSelectedValue(CmbEmpCategory, "Employee Category is required.", errorProvider1, TsLblInputFooter);
                int? selectedNodeTypeId = ClassValidationHelper.ValidateCmbSelectedValue(CmbUnitCode, "Menu Node Type is required.", errorProvider1, TsLblInputFooter);

                // Boolean selections
                // Safely get the checkbox state (handles null check if needed)
                bool isAdmin = ChkIsAdmin?.Checked ?? false;
                bool isProtected = ChkIsProtected?.Checked ?? false;
                bool isActive = ChkIsActive?.Checked ?? false;

                // Populate data table
                myDataTable = new AdminMenuStructure();

                // Id
                if (!string.IsNullOrEmpty(TsTxtRowId.Text) && int.TryParse(TsTxtRowId.Text, out int id))
                    myDataTable.Id = id;
                else
                    myDataTable.Id = null;

                // UnitCode
                myDataTable.UnitCode = !string.IsNullOrEmpty(CmbUnitCode.Text)
                    ? CmbUnitCode.Text
                    : null;

                // MenuEmpCatgId
                myDataTable.MenuEmpCatgId = selectedEmpCategoryId;

                // MenuParentId
                int? selectedMenuParentId = null;
                if (!string.IsNullOrWhiteSpace(CmbMenuGroup.Text))
                {
                    selectedMenuParentId = ClassValidationHelper.ValidateCmbSelectedValue(CmbMenuGroup, "Menu Parent Id is required.", errorProvider1, TsLblInputFooter);
                    myDataTable.MenuParentId = selectedMenuParentId;
                }
                else 
                {
                    myDataTable.MenuParentId = null;
                }

                // MenuNodeType
                myDataTable.MenuNodeType = !string.IsNullOrEmpty(CmbNodeType.Text)
                    ? CmbNodeType.Text
                    : null;

                // MenuFormTitle
                myDataTable.MenuFormTitle = !string.IsNullOrEmpty(TxtFormTitle.Text)
                    ? ClassStringHelpers.CleanAndNormalizeString(TxtFormTitle.Text)
                    : null;

                // MenuFormName
                myDataTable.MenuFormName = !string.IsNullOrEmpty(TxtFormName.Text)
                    ? ClassStringHelpers.CleanAndNormalizeString(TxtFormName.Text)
                    : null;

                // MenuSortOrder
                myDataTable.MenuSortOrder = int.TryParse(NumSortOrder.Text, out int sortOrder)
                    ? sortOrder
                    : 0; // Default value when parsing fails

                // MenuIconName
                myDataTable.MenuIconName = !string.IsNullOrEmpty(CmbIconName.Text)
                    ? ClassStringHelpers.CleanAndNormalizeString(CmbIconName.Text)
                    : null;

                // MenuIsAdmin
                myDataTable.MenuIsAdmin = isAdmin;

                // MenuIsProtected
                myDataTable.MenuIsProtected = isProtected;

                // MenuIsActive
                myDataTable.MenuIsActive = isActive;

                // RowVersion
                if (TsTxtRowId.Tag != null && int.TryParse(TsTxtRowId.Tag.ToString(), out int version))
                    myDataTable.RowVersion = version;
                else
                    myDataTable.RowVersion = null;

                // UserId
                myDataTable.UserId = ClassGlobalVariables.pubLoginUserRowId;

                // UserRowGuid
                myDataTable.UserRowGuid = !string.IsNullOrEmpty(ClassGlobalVariables.pubLoginUserRowGuid)
                    ? ClassGlobalVariables.pubLoginUserRowGuid
                    : null;

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
                MessageBox.Show(ex.Message, "Validate Input", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }


        #endregion
    }
}

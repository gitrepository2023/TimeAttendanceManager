using TimeAttendanceManager.Main.Forms;
using TimeAttendanceManager.MenuDesign.Models;
using TimeAttendanceManager.Main.Classes;
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
///    Title:          Code for Main Menu Groups
///                    
/// 
///    Name:           FrmMenuSetGroups.cs
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

namespace TimeAttendanceManager.MenuDesign.Forms
{
    public partial class FrmMenuSetGroups : Form
    {

        #region "Constructor"
        public FrmMenuSetGroups()
        {
            InitializeComponent();
            this.Load += Form_Load;
            this.FormClosing += FormClosing_FormClosing;
            this.TsBtnCloseForm.Click += TsBtnCloseForm_Click;
            this.TsBtnClose.Click += TsBtnCloseForm_Click;
            this.TsBtnAddNew.Click += TsBtnAddNew_Click;
            this.TsBtnSave.Click += TsBtnSave_Click;
            this.TsBtnDiscard.Click += TsBtnDiscard_Click;
            this.DgvList.CellClick += DgvList_CellClick;
            this.DgvList.UserDeletingRow += DgvList_UserDeletingRow;

            // Using an array and foreach loop
            foreach (var button in new[] { TsBtnSearchDgv, TsBtnClearSearchDgv, TsBtRefreshDgv })
            {
                button.Click += TsBtRefreshDgv_Click;
            }
            this.TsTxtSearchDgv.KeyDown += TsTxtSearchDgv_KeyDown;
            this.TsBtnDrpUnitCode.DropDownItemClicked += TsBtnDrpUnitCode_DropDownItemClicked;
            this.TsBtnDrpColsDgvTblCols.DropDownItemClicked += TsBtnDrpColsDgvTblCols_DropDownItemClicked;
        }
        #endregion

        #region "LocalVariables"
        private ContextMenuStrip myContextMenu = new ContextMenuStrip();   // Storing Context Menu
        private AdminMenuGroup myDataTable = new AdminMenuGroup();
        private SqlDataAdapter mySqlDataAdapter;
        private DataTable myDt = new DataTable();
        private bool changesMade = false;
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

                    // Load default values
                    await SetDefaultValuesAsync();

                    // Update DataGridView
                    await LoadDataGridViewAsync();

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

        #region "TsBtnAddNew_Click"
        private void TsBtnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate if DataTable exists
                if (myDt == null)
                {
                    MessageBox.Show("Data table is not initialized.", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Create new row with default values
                DataRow newRow = myDt.NewRow();

                // Set default values - using null-coalescing for column existence checks
                // newRow["Id"] = myDt.Columns.Contains("Id") ? DBNull.Value : (object)DBNull.Value;
                newRow["MenuGroupName"] = myDt.Columns.Contains("MenuGroupName") ? null : (object)DBNull.Value;
                newRow["SortOrder"] = myDt.Columns.Contains("SortOrder") ? 1 : (object)DBNull.Value;

                // Add row to DataTable
                myDt.Rows.Add(newRow);

                // Optional: Scroll to and select the new row in DataGridView
                if (DgvList != null && DgvList.DataSource == myDt)
                {
                    DgvList.FirstDisplayedScrollingRowIndex = DgvList.RowCount - 1;
                    DgvList.Rows[DgvList.RowCount - 1].Selected = true;
                }
            }
            catch (Exception ex)
            {
                // Enhanced error handling
                MessageBox.Show($"Error adding new row:\n{ex.Message}", "Add Row",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Consider logging the error
                // Logger.Error(ex, "Failed to add new row");
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
                // Load 
                await FillPlantCodeAsync();
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

        #region "TsBtnDrpUnitCode_DropDownItemClicked"
        private void TsBtnDrpUnitCode_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
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
                
                // Check plant access if needed
                if (sender == TsBtnDrpUnitCode)
                {
                    if (!string.IsNullOrWhiteSpace(TsBtnDrpUnitCode.Text) &&
                        !TsBtnDrpUnitCode.Text.Equals("(all)", StringComparison.OrdinalIgnoreCase))
                    {
                        // Uncomment if needed:
                        // string selectedUnit = e.ClickedItem.Text;
                        // bool hasPlantAccess = MMValidateUserPlantAccess(mLoginUserRowGuiID, selectedUnit);
                        // if (!hasPlantAccess)
                        // {
                        //     MessageBox.Show($"You don't have permission for selected plant {selectedUnit}", 
                        //         "Access denied", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //     return;
                        // }
                    }
                }

                // Refresh the data grid view
                TsBtRefreshDgv_Click(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error Number: {ex.HResult}\n" +
                    $"Error Source: {ex.Source}\n" +
                    $"Error Message: {ex.Message}\n" +
                    $"Form: {this.Name}\n" +
                    $"Function: {nameof(TsBtnDrpUnitCode_DropDownItemClicked)}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        #endregion

        #region "TsBtnDrpColsDgvTblCols_DropDownItemClicked"
        /// <summary>
        /// Use col.Name (from DataGridViewColumn) for matching — this is guaranteed to exist and not change.
        /// HeaderText can be shown to the user, but don’t rely on it for column lookups.
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

        #region "DataChanged"
        private void DataChanged(object sender, EventArgs e)
        {
            changesMade = true;
            HighlightChanges();
        }

        #endregion

        #region "HighlightChanges"
        private void HighlightChanges()
        {
            if (DgvList?.Rows == null || DgvList.DataSource == null)
                return;

            foreach (DataGridViewRow row in DgvList.Rows)
            {
                // Skip the new row placeholder
                if (row.IsNewRow)
                    continue;

                // Safely get the bound data item
                if (row.DataBoundItem is DataRowView dataRowView)
                {
                    // Apply highlighting based on row state
                    switch (dataRowView.Row.RowState)
                    {
                        case DataRowState.Added:
                            row.DefaultCellStyle.BackColor = Color.LightGreen;
                            row.DefaultCellStyle.SelectionBackColor = Color.Green;
                            break;

                        case DataRowState.Modified:
                            row.DefaultCellStyle.BackColor = Color.LightYellow;
                            row.DefaultCellStyle.SelectionBackColor = Color.Goldenrod;
                            break;

                        case DataRowState.Unchanged:
                        case DataRowState.Deleted:
                        case DataRowState.Detached:
                        default:
                            // Reset to default colors if needed
                            row.DefaultCellStyle.BackColor = DgvList.DefaultCellStyle.BackColor;
                            row.DefaultCellStyle.SelectionBackColor = DgvList.DefaultCellStyle.SelectionBackColor;
                            break;
                    }
                }
            }
        }
        #endregion

        #region "InitializeContextMenu"
        /// <summary>
        /// 
        /// </summary>
        private void InitializeContextMenu()
        {
            // Create the context menu
            var contextMenu = new ContextMenuStrip();

            // Add "Select Row" option
            var selectRowItem = new ToolStripMenuItem("Select Row");
            selectRowItem.Click += SelectRow_Click;
            selectRowItem.ShortcutKeys = Keys.Control | Keys.S; // Added shortcut key
            // selectRowItem.Image = Properties.Resources.SelectIcon; // Optional icon
            contextMenu.Items.Add(selectRowItem);

            // Add separator
            contextMenu.Items.Add(new ToolStripSeparator());

            // Add copy menu item
            var copyItem = new ToolStripMenuItem("Copy");
            copyItem.Click += CopyCellValue_Click;
            copyItem.ShortcutKeys = Keys.Control | Keys.C;
            // copyItem.Image = Properties.Resources.CopyIcon;
            contextMenu.Items.Add(copyItem);

            // Add more menu items as needed
            var exportItem = new ToolStripMenuItem("Export to Excel");
            exportItem.Click += ExportToExcel_Click;
            // exportItem.Image = Properties.Resources.ExcelIcon;
            contextMenu.Items.Add(exportItem);

            // Add another separator
            contextMenu.Items.Add(new ToolStripSeparator());

            // Add row coloring options
            var colorMenu = new ToolStripMenuItem("Highlight Row");
            // colorMenu.Image = Properties.Resources.ColorIcon;
            colorMenu.DropDownItems.Add("Green", null, (s, e) => HighlightRow(Color.LightGreen));
            colorMenu.DropDownItems.Add("Yellow", null, (s, e) => HighlightRow(Color.LightYellow));
            colorMenu.DropDownItems.Add("Reset", null, (s, e) => ResetRowColor());
            contextMenu.Items.Add(colorMenu);

            // Assign to DataGridView with null check
            if (DgvList != null)
            {
                DgvList.ContextMenuStrip = contextMenu;

                // Enable opening menu on right-click anywhere in the grid
                DgvList.MouseDown += (sender, e) =>
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        var hitTest = DgvList.HitTest(e.X, e.Y);
                        if (hitTest.Type == DataGridViewHitTestType.None)
                        {
                            // Show menu at clicked position
                            contextMenu.Show(DgvList, e.Location);
                        }
                    }
                };
            }
        }

        // Example event handlers
        private void SelectRow_Click(object sender, EventArgs e)
        {
            if (DgvList?.CurrentRow != null && !DgvList.CurrentRow.IsNewRow)
            {
                DgvList.CurrentRow.Selected = true;
            }
        }

        private void CopyCellValue_Click(object sender, EventArgs e)
        {
            if (DgvList?.CurrentCell != null)
            {
                Clipboard.SetText(DgvList.CurrentCell.Value?.ToString() ?? "");
            }
        }

        private void HighlightRow(Color color)
        {
            if (DgvList?.CurrentRow != null && !DgvList.CurrentRow.IsNewRow)
            {
                DgvList.CurrentRow.DefaultCellStyle.BackColor = color;
            }
        }

        private void ResetRowColor()
        {
            if (DgvList?.CurrentRow != null && !DgvList.CurrentRow.IsNewRow)
            {
                DgvList.CurrentRow.DefaultCellStyle.BackColor = DgvList.DefaultCellStyle.BackColor;
            }
        }

        private void ExportToExcel_Click(object sender, EventArgs e)
        {
            // Implement Excel export logic
        }
        #endregion

        #region "LoadDataGridViewAsync"
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task LoadDataGridViewAsync()
        {
            var startTime = DateTime.UtcNow;
            try
            {
                TsLblInputStatus.Text = "Fetching rows. Please wait...";
                TsLblInputStatus.ForeColor = Color.Blue;
                await Task.Yield(); // allow UI refresh instead of Application.DoEvents()

                // Clear DataGridView
                DgvList.DataSource = null;
                DgvList.Rows.Clear();
                DgvList.Columns.Clear();
                DgvList.Refresh();
                DgvList.SuspendLayout();

                myDt = await LoadTableDataAsync();

                if (myDt == null)
                {
                    TsLblInputStatus.Text = "No data exists for selected parameters";
                    return;
                }

                DgvList.DataSource = myDt;

                // Define and apply custom headers
                var customHeaders = new Dictionary<string, string>
                    {
                        {"Id", "Id"},
                        {"MenuGroupName", "Menu Group Name"},
                        {"SortOrder", "Sort Order"}
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
                }));

                // Remove old handlers first
                myDt.RowChanged -= DataChanged;
                myDt.RowDeleted -= DataChanged;

                // Reattach event handlers
                myDt.RowChanged += DataChanged;
                myDt.RowDeleted += DataChanged;

                InitializeContextMenu();

                ConfigureDataGridView(DgvList);

                // Add row numbers
                for (int i = 0; i < DgvList.Rows.Count; i++)
                {
                    DgvList.Rows[i].HeaderCell.Value = (i + 1).ToString();
                }

                // Populate the dropdown 
                AddColumnVisibilityDgvItems();

                var elapsedTime = DateTime.UtcNow - startTime;
                TsLblInputStatus.Text = $"Fetch Rows [ {DgvList.RowCount} ] " +
                                        $"In : {elapsedTime:hh\\:mm\\:ss}";
                await Task.Yield();
            }
            catch (Exception ex)
            {
                TsLblInputStatus.Text = ex.Message;
                TsLblInputStatus.ForeColor = Color.Red;
                await Task.Yield();

                MessageBox.Show(ex.Message, "Load DataGridView", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                DgvList.ResumeLayout();
            }
        }
        #endregion

        #region "ConfigureDataGridView"
        /// <summary>
        /// helper method
        /// </summary>
        /// <param name="dgv"></param>
        private static void ConfigureDataGridView(DataGridView dgv)
        {
            dgv.ColumnHeadersVisible = true;
            dgv.AllowUserToAddRows = true;
            dgv.AllowUserToDeleteRows = true;
            dgv.ReadOnly = false;
            dgv.RowHeadersWidth = 60;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;

            // Style column headers
            var columnHeaderStyle = new DataGridViewCellStyle
            {
                BackColor = Color.Beige
            };
            dgv.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            // Hide and lock Id column if it exists
            if (dgv.Columns.Contains("Id"))
            {
                dgv.Columns["Id"].Visible = false;
                dgv.Columns["Id"].ReadOnly = true;
            }

            dgv.AutoResizeColumnHeadersHeight();
            dgv.AutoResizeColumns();
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

        #region "LoadTableDataAsync"
        /// <summary>
        /// Loads menu group data from the database.
        /// </summary>
        /// <returns></returns>
        private async Task<DataTable> LoadTableDataAsync()
        {
            try
            {

                // This line assigns a value to mUnitCode using the ternary operator (? :)
                // Check if the TsBtnDrpUnitCode control is null 
                // OR if its Text property is null, empty, or whitespace
                // If the condition above is TRUE, then use the default unit code from global variables
                // Otherwise (if TsBtnDrpUnitCode is not null and has valid text), 
                // use the value entered/selected in TsBtnDrpUnitCode.Text
                string mUnitCode = (TsBtnDrpUnitCode == null || string.IsNullOrWhiteSpace(TsBtnDrpUnitCode.Text))
                   ? ClassGlobalVariables.pubUnitCode
                   : TsBtnDrpUnitCode.Text;

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

                string connectionString = ClassGlobalFunctions.GetConnectionStringByUnitCode(mUnitCode);

                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new InvalidOperationException("Connection string cannot be empty");
                }

                var serachColumns = new List<string> {"MenuGroupName"};

                const string tableName = "dbo.AdminMenuGroups";

                var sb = new StringBuilder();
                var parameters = new List<SqlParameter>();

                int mLimitRows = int.TryParse(TsCmbLimitRows.Text, out int parsedLimit) ? parsedLimit : 50;

                if (!string.IsNullOrWhiteSpace(TsCmbLimitRows.Text) &&
                    TsCmbLimitRows.Text.Trim().ToLower() != "(all)")
                {
                    sb.Append($"SELECT TOP {mLimitRows} ");
                }
                else
                {
                    sb.Append("SELECT ");
                }

                sb.Append($"Id, MenuGroupName, SortOrder FROM {tableName} WHERE 1=1 ");

                // Add search conditions if search text is provided
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    searchText = searchText.Trim().ToLower().Replace("*", "%");

                    if (serachColumns != null && serachColumns.Count > 0)
                    {
                        sb.Append(" AND (");
                        var searchClauses = new List<string>();

                        for (int i = 0; i < serachColumns.Count; i++)
                        {
                            string paramName = $"@SearchText{i}";
                            searchClauses.Add($"{serachColumns[i]} LIKE {paramName}");
                            parameters.Add(new SqlParameter(paramName, $"%{searchText}%"));
                        }

                        sb.Append(string.Join(" OR ", searchClauses));
                        sb.Append(")");
                    }
                }

                sb.Append("ORDER BY SortOrder, MenuGroupName ");

                var query = sb.ToString();

                var dt = new DataTable();

                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    if (mySqlDataAdapter != null)
                    {
                        mySqlDataAdapter.Dispose();
                        mySqlDataAdapter = null; // Not strictly necessary in most cases
                    }

                    using (var mySQLcmd = new SqlCommand(query, connection))
                    {
                        if (parameters.Count > 0)
                        {
                            mySQLcmd.Parameters.AddRange(parameters.ToArray());
                        }

                        // Assign the command to SqlDataAdapter
                        mySqlDataAdapter = new SqlDataAdapter(mySQLcmd);

                        // Optional: Auto-generate insert/update/delete commands
                        using (var builder = new SqlCommandBuilder(mySqlDataAdapter))
                        {
                            // Fill schema to get table structure even if no rows
                            mySqlDataAdapter.FillSchema(dt, SchemaType.Source);
                            // Fill data
                            mySqlDataAdapter.Fill(dt);
                        }
                    }
                }

                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Table Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        #endregion

        #region "TsBtnSave_Click"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TsBtnSave_Click(object sender, EventArgs e)
        {
            if (!changesMade)
            {
                ShowInformationMessage("No changes to save.", "Info");
                return;
            }

            const string tableName = "dbo.AdminMenuGroups";

            try
            {
                // Prepare queries with parameterized SQL
                string insertQuery = $@"
                    INSERT INTO {tableName} 
                    (MenuGroupName, SortOrder, UserId, UserRowGuid, IpAddsCreated, HostName) 
                    VALUES (@MenuGroupName, 
                        @SortOrder, 
                        @UserId, 
                        @UserRowGuid, 
                        @IpAddsCreated, 
                        @HostName)";

                string updateQuery = $@"
                    UPDATE {tableName} 
                    SET MenuGroupName = @MenuGroupName, 
                        SortOrder = @SortOrder,
                        UserUpdatedId = @UserUpdatedId,
                        UserUpdatedRowGuid = @UserUpdatedRowGuid,
                        IpAddsUpdated = @IpAddsUpdated,
                        UpdatedHostName = @UpdatedHostName,
                        UpdatedAt = GETDATE()
                    WHERE Id = @Id";

                string deleteQuery = $@"
                    DELETE FROM {tableName} 
                    WHERE Id = @Id";

                string unitCode = ClassGlobalVariables.pubUnitCode;

                string connectionString = ClassGlobalFunctions.GetConnectionStringByUnitCode(unitCode);

                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Configure data adapter commands
                            mySqlDataAdapter.SelectCommand.Connection = connection;
                            mySqlDataAdapter.SelectCommand.Transaction = transaction;

                            // Configure INSERT command
                            mySqlDataAdapter.InsertCommand = new SqlCommand(insertQuery, connection, transaction);
                            mySqlDataAdapter.InsertCommand.Parameters.Add("@MenuGroupName", SqlDbType.VarChar, 50, "MenuGroupName");
                            mySqlDataAdapter.InsertCommand.Parameters.Add("@SortOrder", SqlDbType.Int, 0, "SortOrder");

                            mySqlDataAdapter.InsertCommand.Parameters.Add("@UserId", SqlDbType.Int).Value =
                                ClassGlobalVariables.pubLoginUserRowId.HasValue
                                    ? (object)ClassGlobalVariables.pubLoginUserRowId.Value
                                    : DBNull.Value;

                            mySqlDataAdapter.InsertCommand.Parameters.Add("@UserRowGuid", SqlDbType.VarChar, 150).Value =
                                ClassGlobalVariables.pubLoginUserRowGuid ?? (object)DBNull.Value;

                            mySqlDataAdapter.InsertCommand.Parameters.Add("@IpAddsCreated", SqlDbType.VarChar, 50).Value =
                                ClassGlobalVariables.pubHostIPAddress ?? (object)DBNull.Value;

                            mySqlDataAdapter.InsertCommand.Parameters.Add("@HostName", SqlDbType.VarChar, 50).Value =
                                ClassGlobalVariables.pubDNSHostName ?? (object)DBNull.Value;

                            // Configure UPDATE command
                            mySqlDataAdapter.UpdateCommand = new SqlCommand(updateQuery, connection, transaction);
                            mySqlDataAdapter.UpdateCommand.Parameters.Add("@MenuGroupName", SqlDbType.VarChar, 150, "MenuGroupName");
                            mySqlDataAdapter.UpdateCommand.Parameters.Add("@SortOrder", SqlDbType.Int, 0, "SortOrder");
                            mySqlDataAdapter.UpdateCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id").SourceVersion = DataRowVersion.Original;

                            mySqlDataAdapter.UpdateCommand.Parameters.Add("@UserUpdatedId", SqlDbType.Int).Value =
                                ClassGlobalVariables.pubLoginUserRowId.HasValue
                                    ? (object)ClassGlobalVariables.pubLoginUserRowId.Value
                                    : DBNull.Value;

                            mySqlDataAdapter.UpdateCommand.Parameters.Add("@UserUpdatedRowGuid", SqlDbType.VarChar, 150).Value =
                                ClassGlobalVariables.pubLoginUserRowGuid ?? (object)DBNull.Value;

                            mySqlDataAdapter.UpdateCommand.Parameters.Add("@IpAddsUpdated", SqlDbType.VarChar, 50).Value =
                                ClassGlobalVariables.pubHostIPAddress ?? (object)DBNull.Value;

                            mySqlDataAdapter.UpdateCommand.Parameters.Add("@UpdatedHostName", SqlDbType.VarChar, 50).Value =
                                ClassGlobalVariables.pubDNSHostName ?? (object)DBNull.Value;

                            // Configure DELETE command
                            mySqlDataAdapter.DeleteCommand = new SqlCommand(deleteQuery, connection, transaction);
                            mySqlDataAdapter.DeleteCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id").SourceVersion = DataRowVersion.Original;

                            // Execute update
                            int rowsAffected = mySqlDataAdapter.Update(myDt);

                            transaction.Commit();

                            ShowSuccessMessage($"Successfully saved {rowsAffected} changes.", "Success");
                            changesMade = false;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            LogError(ex, "Error saving menu groups");
                            ShowErrorMessage($"Error saving data: {ex.Message}", "Error");
                            throw; // Re-throw for global error handling
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                ShowErrorMessage($"Database error: {sqlEx.Number} - {sqlEx.Message}", "Database Error");
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Unexpected error: {ex.Message}", "Error");
            }
        }
        #endregion

        #region "DgvList_CellClick"
        private void DgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure it's not a header click
            if (e.RowIndex >= 0)
            {
                DgvList.Rows[e.RowIndex].Selected = true;
            }
        }
        #endregion

        #region "DgvList_UserDeletingRow"
        private void DgvList_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to delete this row?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        #endregion

        #region "TsBtnDiscard_Click"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsBtnDiscard_Click(object sender, EventArgs e)
        {
            if (!changesMade) return;

            var result = MessageBox.Show(
                "Are you sure you want to discard all changes?",
                "Reset Changes",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2); // Default to "No" for safety

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Suspend layout for better performance
                    if (DgvList != null)
                    {
                        DgvList.SuspendLayout();
                    }

                    // Reject changes and update state
                    myDt?.RejectChanges();
                    changesMade = false;

                    // Refresh highlighting
                    HighlightChanges();
                  
                }
                catch (Exception ex)
                {
                    // Log error and notify user
                    // Logger.Error("Error discarding changes", ex);
                    MessageBox.Show(
                        ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                finally
                {
                    // Always resume layout
                    if (DgvList != null)
                    {
                        DgvList.ResumeLayout();
                    }
                }
            }
        }
        #endregion

        #region "ShowMessages"
        /// <summary>
        /// Helper methods for consistent messaging
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        private void ShowInformationMessage(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowSuccessMessage(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowErrorMessage(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void LogError(Exception ex, string message)
        {
            // Implement your logging mechanism here
            // Example: Logger.Error(message, ex);
        }

        #endregion

        #region "TsBtRefreshDgv_Click"
        private async void TsBtRefreshDgv_Click(object sender, EventArgs e)
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

        #region "FillPlantCodeAsync"
        private async Task FillPlantCodeAsync()
        {
            try
            {
                string mUnitCode = ClassGlobalVariables.pubUnitCode;

                string connectionString = ClassGlobalFunctions.GetConnectionStringByUnitCode(
                   mUnitCode ?? ClassGlobalVariables.pubUnitCode);

                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new InvalidOperationException("Connection string cannot be empty");
                }

                const string mTableName = "dbo.Companies";

                // Clear existing items
                TsBtnDrpUnitCode.DropDownItems.Clear();

                using (var myConnection = new SqlConnection(connectionString))
                {
                    await myConnection.OpenAsync();

                    string query = $"SELECT UnitCode, AliasName FROM {mTableName} WHERE IsActive = 1 AND IsDeleted = 0 ORDER BY UnitCode";

                    using (var mySQLcmd = new SqlCommand(query, myConnection))
                    using (var reader = await mySQLcmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            string unitCode = reader["UnitCode"].ToString();
                            string aliasName = reader["AliasName"].ToString();

                            var menuItem = new ToolStripMenuItem(unitCode)
                            {
                                Tag = aliasName
                            };

                            TsBtnDrpUnitCode.DropDownItems.Add(menuItem);
                        }
                    }
                }

                // Set default values
                TsBtnDrpUnitCode.Text = ClassGlobalVariables.pubUnitCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FillPlantCodeAsync",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion


    }
}

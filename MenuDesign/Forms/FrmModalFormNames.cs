using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimeAttendanceManager.Helpers.Classes;
using TimeAttendanceManager.Main.Classes;
using TimeAttendanceManager.Main.Forms;

namespace TimeAttendanceManager.MenuDesign.Forms
{
    public partial class FrmModalFormNames : Form
    {

        #region "LocalVariables"
        private string mUnitCode;

        private ContextMenuStrip myContextMenu = new ContextMenuStrip();   // Storing Context Menu
        public int SelectedRowId { get; set; }
        #endregion

        #region "Constructor"
        // Constructor with unit code parameter
        public FrmModalFormNames(string myUnitCode)
        {
            InitializeComponent();

            if (string.IsNullOrWhiteSpace(myUnitCode))
            {
                throw new ArgumentException("Unit code cannot be null or empty", nameof(myUnitCode));
            }

            // Consider adding this if you want the global default as fallback:
            mUnitCode = myUnitCode ?? ClassGlobalVariables.pubUnitCode;

            this.KeyPreview = true; // Important for form to receive key events

            // Remove existing event handlers to prevent duplicates
            this.Load -= Form_Load;
            this.KeyDown -= FrmMMMenuModalFormNames_KeyDown;
            this.TsBtnClose.Click -= TsBtnClose_Click;
            this.TsBtRefreshDgv.Click -= TsBtRefreshDgv_Click;
            this.TsBtnClearSearchDgv.Click -= TsBtRefreshDgv_Click;
            this.TsBtnSearchDgv.Click -= TsBtRefreshDgv_Click;
            this.TsTxtSearchDgv.KeyDown -= TsTxtSearchDgv_KeyDown;
            this.TsBtnDrpColsDgvTblCols.DropDownItemClicked -= TsBtnDrpColsDgvTblCols_DropDownItemClicked;
            this.TsBtnSelectRow.Click -= TsBtnSelectRow_Click;
            this.DgvList.CellDoubleClick -= DgvList_CellDoubleClick;
            this.TsBtnNotAssign.Click -= TsBtnNotAssign_Click;

            // Add event handlers
            this.Load += Form_Load;
            this.KeyDown += FrmMMMenuModalFormNames_KeyDown;
            this.TsBtnClose.Click += TsBtnClose_Click;
            this.TsBtRefreshDgv.Click += TsBtRefreshDgv_Click;
            this.TsBtnClearSearchDgv.Click += TsBtRefreshDgv_Click;
            this.TsBtnSearchDgv.Click += TsBtRefreshDgv_Click;
            this.TsTxtSearchDgv.KeyDown += TsTxtSearchDgv_KeyDown;
            this.TsBtnDrpColsDgvTblCols.DropDownItemClicked += TsBtnDrpColsDgvTblCols_DropDownItemClicked;
            this.TsBtnSelectRow.Click += TsBtnSelectRow_Click;
            this.DgvList.CellDoubleClick += DgvList_CellDoubleClick;
            this.TsBtnNotAssign.Click += TsBtnNotAssign_Click;

        }
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

                    // Update DataGridView
                    await LoadDataGridViewAsync();

                    // Set Focus
                    TsTxtSearchDgv.Focus();

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

        #region "FrmMMMenuModalFormNames_KeyDown"
        private void FrmMMMenuModalFormNames_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close(); // Close the form
            }
        }
        #endregion

        #region "TsBtnClose_Click"
        private void TsBtnClose_Click(System.Object sender, System.EventArgs e)
        {

            // Close the form
            this.DialogResult = DialogResult.Cancel;
            this.Close();
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

        #region "DgvContextMenu"
        /// <summary>
        /// 
        /// </summary>
        private void DgvContextMenu()
        {
            try
            {
                // Create and configure ContextMenu
                myContextMenu = new ContextMenuStrip
                {
                    Name = "myDgvContextMenu"
                };

                myContextMenu.Items.Clear();

                // Add menu items
                var selectRowItem = new ToolStripMenuItem("Select Row");
                var cancelItem = new ToolStripMenuItem("Cancel");

                // Wire up event handler
                selectRowItem.Click += ContextMenuHandler;

                // Add items to menu
                myContextMenu.Items.Add(selectRowItem);
                myContextMenu.Items.Add(cancelItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Menu Creation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region "ContextMenuHandler"
        // <summary>
        /// Handles context menu item clicks
        /// </summary>
        private void ContextMenuHandler(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (sender is ToolStripItem menuItem)
                {
                    switch (menuItem.Text)
                    {
                        case "Select Row":
                            if (DgvList.Rows.Count > 0)
                            {
                                // Add your row selection logic here
                            }
                            break;

                            // Add other cases as needed
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Menu Handler Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                this.Cursor = Cursors.Default;
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

                var serachColumns = new List<string> { "FormName",
                    "FormTitle" };

                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit) { Value = 1 });

                string orderBy = "FormName, FormTitle ";

                // Get data asynchronously
                DataTable resultTable = await ClassDbHelpers.GetRowsFromTableAsync(
                    mUnitCode,
                    "AdminMenuFormNames",
                    null,   // selectedColumns
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
                var columnsToInclude = new List<string> { "Id", "FormName", "FormTitle" };

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
                        {"FormName", "Form Name"},
                        {"FormTitle", "Form Title"}
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
                    DgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

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

        #region "DgvList_CellDoubleClick"
        private void DgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure a valid row is selected (not header row)
            if (e.RowIndex < 0) return;

            try
            {
                var cellValue = DgvList.Rows[e.RowIndex].Cells["Id"].Value;

                if (cellValue != null && cellValue != DBNull.Value && IsNumeric(cellValue))
                {
                    SelectedRowId = Convert.ToInt32(cellValue);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid or missing Id in the selected row.",
                                  "Error",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing selection: {ex.Message}",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Helper method to check if value is numeric
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool IsNumeric(object value)
        {
            if (value == null) return false;

            return int.TryParse(value.ToString(), out _);
        }
        #endregion

        #region "TsBtnSelectRow_Click"
        private void TsBtnSelectRow_Click(object sender, EventArgs e)
        {
            try
            {
                var dgvSelectedRows = DgvList.SelectedRows;

                // Validate single row selection
                if (dgvSelectedRows.Count > 1)
                {
                    throw new Exception("Please select one row at a time");
                }

                // Find first valid row with ID
                int? rowId = null;
                foreach (DataGridViewRow selectedRow in dgvSelectedRows)
                {
                    var idCell = selectedRow.Cells["Id"];
                    if (idCell.Value != null && idCell.Value != DBNull.Value)
                    {
                        if (int.TryParse(idCell.Value.ToString(), out int parsedId))
                        {
                            rowId = parsedId;
                            break; // Exit after first valid ID found
                        }
                    }
                }

                if (rowId.HasValue)
                {
                    SelectedRowId = rowId.Value;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No valid row with ID was selected",
                                  "Selection Error",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                               "Row Selection Error",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
            }
        }
        #endregion

        #region "TsBtnNotAssign_Click"
        /// <summary>
        /// Load FormNames not assigned to any MenuStructure
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TsBtnNotAssign_Click(object sender, EventArgs e)
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

                var serachColumns = new List<string> { "FormName",
                    "FormTitle" };

                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit) { Value = 1 });

                string orderBy = "FormName, FormTitle ";

                // Get connection string based on unit code
                string connectionString = ClassGlobalFunctions.GetConnectionStringByUnitCode(mUnitCode);

                // Get data asynchronously
                DataTable resultTable = await GetUnreferencedFormNamesAsync(connectionString);

                // Check for empty results
                if (resultTable == null || resultTable.Rows.Count == 0)
                {
                    TsLblDgvListFooter.Text = "No rows found for given parameters.";
                    return;
                }

                // Create a filtered DataTable with only the columns we want
                DataTable filteredTable = new DataTable();
                var columnsToInclude = new List<string> { "Id", "FormName", "FormTitle" };

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
                        {"FormName", "Form Name"},
                        {"FormTitle", "Form Title"}
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

        #region "GetUnreferencedFormNamesAsync"
        /// <summary>
        /// Returns rows from AdminMenuFormNames whose FormName is not referenced by AdminMenuStructures.MenuFormName.
        /// </summary>
        /// <param name="connectionString">SQL Server connection string</param>
        /// <param name="cancellationToken">optional cancellation token</param>
        /// <returns>DataTable with the resulting rows</returns>
        public static async Task<DataTable> GetUnreferencedFormNamesAsync(
            string connectionString,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("connectionString is required", nameof(connectionString));

            // Use LEFT JOIN + IS NULL to correctly handle NULLs and avoid NOT IN pitfalls
            const string sql = @"
                SELECT f.[Id], f.[FormName], f.[FormTitle], f.[IsActive]
                FROM [dbo].[AdminMenuFormNames] AS f
                LEFT JOIN [dbo].[AdminMenuStructures] AS s
                    ON f.[FormName] = s.[MenuFormName]
                WHERE s.[MenuFormName] IS NULL;
                ";

            var dt = new DataTable();

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                await conn.OpenAsync(cancellationToken).ConfigureAwait(false);

                // ExecuteReaderAsync returns a reader we can iterate with ReadAsync
                using (var reader = await cmd.ExecuteReaderAsync(CommandBehavior.SequentialAccess, cancellationToken).ConfigureAwait(false))
                {
                    // Build DataTable schema from reader
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        // Create column with same name & type (handle nullable types)
                        var fieldType = reader.GetFieldType(i);
                        var columnType = Nullable.GetUnderlyingType(fieldType) ?? fieldType;
                        dt.Columns.Add(reader.GetName(i), columnType);
                    }

                    // Fill DataTable asynchronously, row-by-row
                    while (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
                    {
                        var values = new object[reader.FieldCount];
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            values[i] = await GetFieldValueSafeAsync(reader, i, cancellationToken).ConfigureAwait(false);
                        }
                        dt.Rows.Add(values);
                    }
                }
            }

            return dt;
        }

        // Helper: retrieve a value safely (reader.GetValue is synchronous but cheap for the current row)
        private static Task<object> GetFieldValueSafeAsync(SqlDataReader reader, int ordinal, CancellationToken cancellationToken)
        {
            // We keep this synchronous as GetValue for the current row is immediate.
            // Wrap in Task.FromResult so the call-site can remain async-friendly.
            object val = reader.IsDBNull(ordinal) ? (object)DBNull.Value : reader.GetValue(ordinal);
            return Task.FromResult(val);
        }
        #endregion

    }
}

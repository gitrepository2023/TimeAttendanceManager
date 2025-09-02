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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeAttendanceManager.Features.Masters.Departments.Forms
{
    public partial class FrmDepartmentEditForm : Form
    {

        #region "Constructor"
        public FrmDepartmentEditForm(string unitCode, int? rowId = null)
        {
            InitializeComponent();

            if (string.IsNullOrWhiteSpace(unitCode))
                throw new ArgumentException("UnitCode must have a value.", nameof(unitCode));

            _rowId = rowId.HasValue ? rowId.Value : (int?)null;
            _unitCode = unitCode;

            // Remove events
            this.Load -= Form_Load;
            // this.FormClosing -= FormClosing_FormClosing;
            this.TsBtnCancel.Click -= TsBtnCancel_Click;
            this.TsBtnSave.Click -= TsBtnSave_Click;

            // Add events
            this.Load += Form_Load;
            // this.FormClosing += FormClosing_FormClosing;
            this.TsBtnCancel.Click += TsBtnCancel_Click;
            this.TsBtnSave.Click += TsBtnSave_Click;
        }
        #endregion

        #region "LocalVariables"
        private int? _rowId;         // nullable for insert/update
        private string _unitCode;   // always required
        private Department myDataTable = new Department();
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

                    // Dock Controls
                    DockControls();

                    // Set Default Values
                    await SetDefaultValuesAsync();

                    if (_rowId.HasValue)   // Edit Mode
                    {
                        await DisplaySelectedRowDataAsync(_rowId.Value);
                    }
                    else
                    {
                        TsTxtRecordId.Text = string.Empty;
                        CmbUnitCode.Text = _unitCode;
                        CmbUnitCode.Enabled = false;
                        TxtCode.Text = string.Empty;
                        TxtName.Text = string.Empty;
                    }

                    // Set focus
                    TxtCode.Focus();

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

                // Show confirmation dialog
                if (MessageBox.Show("Close this form?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error closing form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region "CloseForm"
        private void TsBtnCancel_Click(object sender, EventArgs e)
        {

            // Return Cancel to parent
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion

        #region "DockControls"
        private void DockControls()
        {
            ClassLayoutHelper.ConfigureTableLayout(TableLayout1);
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

                // Read user defaults
                var vals = await UserLoginDefaultsService.GetUserLoginDefaultsAsync(
                    ClassGlobalVariables.pubUnitCode,
                    ClassGlobalVariables.pubLoginUserRowGuid);
                if (vals.TryGetValue("PlantCode", out string plantCode))
                {
                    CmbUnitCode.Text = plantCode;
                }
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

        #region "DisplaySelectedRowDataAsync"
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task DisplaySelectedRowDataAsync(int? rowId)
        {
            try
            {
                // Update status
                TsLblInputStatus.Text = "Displaying data. Please wait...";
                TsLblInputStatus.ForeColor = Color.DarkBlue;
                Application.DoEvents();

                TsTxtRecordId.Text = _rowId.ToString();
                TsTxtRecordId.Tag = null;

                // Get data for selected row

                TsLblInputStatus.Text = "Fetching row. Please wait...";
                TsLblInputStatus.ForeColor = Color.DarkBlue;

                string mTableName = "dbo.Master_Departments";

                string mUnitCode = (CmbUnitCode == null || string.IsNullOrWhiteSpace(CmbUnitCode.Text))
                   ? ClassGlobalVariables.pubUnitCode
                   : CmbUnitCode.Text;

                // Fetch from sql table
                DataTable dt = await ClassDbHelpers.GetSelectedRowDataAsync(
                    rowId: rowId.Value,
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
                TxtCode.Text = ClassSafeValueHelpers.PubGetSafeValue(row["DepartmentCode"]);
                TxtName.Text = ClassSafeValueHelpers.PubGetSafeValue(row["DepartmentName"]);

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

                    sb.Append("INSERT a new row")
                      .AppendLine()
                      .Append($"Code: {TxtCode.Text}, Name: {TxtName.Text}");
                }
                else
                {
                    isRowInsertUpdate = "UPDATED";

                    sb.Append("UPDATE the existing shift")
                      .AppendLine()
                      .Append($"Shift Code: {TxtCode.Text}, Type: {TxtName.Text}");
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

                    // Return OK to parent
                    this.DialogResult = DialogResult.OK;
                    this.Close();

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

                const string tableName = "dbo.Master_Departments";
                const string sqlProcedureName = "dbo.usp_Master_Departments_Upsert";

                string defaultUnitCode = _unitCode;

                string connectionString = ClassGlobalFunctions.GetConnectionStringByUnitCode(defaultUnitCode);
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new InvalidOperationException("Connection string cannot be empty");
                }

                var parameters = new List<SqlParameter>
                {
                    ClassDbHelpers.CreateSqlParameter("@RowId", SqlDbType.Int, myDataTable.Id),
                    ClassDbHelpers.CreateSqlParameter("@UnitCode", SqlDbType.VarChar, myDataTable.UnitCode),
                    ClassDbHelpers.CreateSqlParameter("@DepartmentCode", SqlDbType.NVarChar, myDataTable.DepartmentCode),
                    ClassDbHelpers.CreateSqlParameter("@DepartmentName", SqlDbType.NVarChar, myDataTable.DepartmentName),
                    ClassDbHelpers.CreateSqlParameter("@IsActive", SqlDbType.Bit, myDataTable.IsActive),

                    ClassDbHelpers.CreateSqlParameter("@RowVersion", SqlDbType.Int, myDataTable.RowVersion),
                    ClassDbHelpers.CreateSqlParameter("@UserRowGuid", SqlDbType.NVarChar, ClassGlobalVariables.pubLoginUserRowGuid),
                };

                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync().ConfigureAwait(false);

                    using (var command = new SqlCommand(sqlProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddRange(parameters.ToArray());

                        // Add output parameter for success status
                        var newRowIdParam = new SqlParameter("@NewId", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(newRowIdParam);

                        // Add output parameter for success status
                        var successParam = new SqlParameter("@Success", SqlDbType.Bit)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(successParam);

                        // Execute the command
                        await command.ExecuteNonQueryAsync().ConfigureAwait(false);

                        int newRowId = Convert.ToInt32(newRowIdParam.Value);
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

                // Required control validations
                ClassValidationHelper.ValidateControl(CmbUnitCode, "Plant Code is required.", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateControl(TxtCode, "Code is required.", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateControl(TxtName, "Name is required.", errorProvider1, TsLblInputStatus);
               
                // get the valid selected RowId
                int? rowVersion = null;
                string tagValue = TsTxtRecordId.Tag as string;   // safe cast to string

                if (!string.IsNullOrWhiteSpace(tagValue))
                {
                    int parsedId;
                    if (!int.TryParse(tagValue, out parsedId))
                    {
                        string errorMessage = "Invalid row selected.";
                        errorProvider1.SetError(CmbUnitCode, errorMessage);
                        throw new ArgumentException(errorMessage);
                    }

                    rowVersion = parsedId; // Assign the parsed value
                }

                // Length validations
                ClassValidationHelper.ValidateTextBoxLength(TxtCode, 50, "Code", errorProvider1, TsLblInputStatus);
                ClassValidationHelper.ValidateTextBoxLength(TxtName, 100, "Name", errorProvider1, TsLblInputStatus);
               
                // Dropdown value validations
                int? selectedUnitId = ClassValidationHelper.ValidateCmbSelectedValue(CmbUnitCode, "Plant Code is required.", errorProvider1, TsLblInputStatus);

                bool? isActive = ChkIsActive.Checked;

                // Populate data table
                myDataTable = new Department();

                // Id
                myDataTable.Id = _rowId.HasValue ? _rowId.Value : (int?)null;
                myDataTable.RowVersion = rowVersion.HasValue ? rowVersion.Value : (int?)null;
               
                // UnitCode
                myDataTable.UnitCode = !string.IsNullOrEmpty(CmbUnitCode.Text)
                    ? CmbUnitCode.Text
                    : null;

                // DepartmentCode
                myDataTable.DepartmentCode = !string.IsNullOrEmpty(TxtCode.Text)
                    ? ClassStringHelpers.CleanAndUpperCase(TxtCode.Text)
                    : null;

                // DepartmentName
                myDataTable.DepartmentName = !string.IsNullOrEmpty(TxtName.Text)
                    ? ClassStringHelpers.CleanAndUpperCase(TxtName.Text)
                    : null;

                // IsActive
                myDataTable.IsActive = isActive.Value;

                TsLblInputStatus.ForeColor = Color.DarkBlue;
                TsLblInputStatus.Text = "Done...";

                return true;

            }
            catch (Exception ex)
            {
                TsLblInputStatus.Text = ex.Message;
                MessageBox.Show(ex.Message, "Validate Input", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        #endregion

    }
}

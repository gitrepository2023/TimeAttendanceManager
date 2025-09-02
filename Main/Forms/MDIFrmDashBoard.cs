using TimeAttendanceManager.Auth.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimeAttendanceManager.Helpers.Classes;
using TimeAttendanceManager.Main.Classes;
using System.Reflection;
using System.Diagnostics;
using System.Deployment.Application;
using System.IO;
using System.Data.SqlClient;
using TimeAttendanceManager.MenuDesign.Forms;

namespace TimeAttendanceManager.Main.Forms
{
    public partial class MDIFrmDashBoard : Form
    {

        #region "Constructor"
        public MDIFrmDashBoard()
        {
            InitializeComponent();

            Instance = this; // Make sure this is set

            // Subscribe to events
            this.Shown += FrmMainMdi_Shown;

            // Remove if already subscribed

            this.TsBtnDrpEmpCatg.DropDownItemClicked -= TsBtnDrpEmpCatg_DropDownItemClicked;
            this.TsMenuAdminManageUsers.Click -= TsMenuAdminManageUsers_Click;
            this.TsMenuAdminSetMenuGrp.Click -= TsMenuAdminSetMenuGrp_Click;
            this.TsMenuAdminSetMenuStructure.Click -= TsMenuAdminSetMenuStructure_Click;
            this.TsMenuAdminSetUserPlantAccess.Click -= TsMenuAdminSetUserPlantAccess_Click;

            this.TabCtrlMain.MouseDown -= TabCtrlMain_MouseDown;
            this.TsBtnExit.Click -= TsBtnExit_Click;
            this.TsBtnExpandAppInfo.Click -= TsBtnExpandAppInfo_Click;
            this.TsBtnExpandTview.Click -= TsBtnExpandAppInfo_Click;
            this.TsMenuSideNav.Click -= TsMenuSideNav_Click;
            this.TsBtnMenuClose.Click -= TsMenuSideNav_Click;
            this.TsBtnLogOut.Click -= TsBtnLogOut_Click;
            this.TsBtnRefresh.Click -= TsBtnRefresh_Click;
            this.TsBtnExpandCollapse.Click -= TsBtnExpandCollapse_Click;
            this.TreeViewMain.BeforeCollapse -= TreeViewMain_BeforeCollapse;
            this.TreeViewMain.BeforeExpand -= TreeViewMain_BeforeExpand;
            this.TreeViewMain.NodeMouseDoubleClick -= TreeViewMain_NodeMouseDoubleClick;
                       
            this.TsTxtTviewSearch.KeyDown -= TsTxtTviewSearch_KeyDown;
            this.TsTxtTviewSearch.TextChanged -= TsTxtTviewSearch_TextChanged;
            this.TsBtnTviewSearch.Click -= TsBtnTviewSearch_Click;
            this.TsBtnTviewSearchClear.Click -= TsBtnTviewSearch_Click;

            // Add again
            this.TsBtnDrpEmpCatg.DropDownItemClicked += TsBtnDrpEmpCatg_DropDownItemClicked;
            this.TsMenuAdminManageUsers.Click += TsMenuAdminManageUsers_Click;
            this.TsMenuAdminSetMenuGrp.Click += TsMenuAdminSetMenuGrp_Click;
            this.TsMenuAdminSetMenuStructure.Click += TsMenuAdminSetMenuStructure_Click;
            this.TsMenuAdminSetUserPlantAccess.Click += TsMenuAdminSetUserPlantAccess_Click;

            this.TabCtrlMain.MouseDown += TabCtrlMain_MouseDown;
            this.TsBtnExit.Click += TsBtnExit_Click;
            this.TsBtnExpandAppInfo.Click += TsBtnExpandAppInfo_Click;
            this.TsBtnExpandTview.Click += TsBtnExpandAppInfo_Click;
            this.TsMenuSideNav.Click += TsMenuSideNav_Click;
            this.TsBtnMenuClose.Click += TsMenuSideNav_Click;
            this.TsBtnLogOut.Click += TsBtnLogOut_Click;
            this.TsBtnRefresh.Click += TsBtnRefresh_Click;
            this.TsBtnExpandCollapse.Click += TsBtnExpandCollapse_Click;
            this.TreeViewMain.BeforeCollapse += TreeViewMain_BeforeCollapse;
            this.TreeViewMain.BeforeExpand += TreeViewMain_BeforeExpand;
            this.TreeViewMain.NodeMouseDoubleClick += TreeViewMain_NodeMouseDoubleClick;

            // Set up event handlers
            this.TsTxtTviewSearch.KeyDown += TsTxtTviewSearch_KeyDown;
            this.TsTxtTviewSearch.TextChanged += TsTxtTviewSearch_TextChanged;
            this.TsBtnTviewSearch.Click += TsBtnTviewSearch_Click;
            this.TsBtnTviewSearchClear.Click += TsBtnTviewSearch_Click; 


        }
        #endregion

        #region " Private Members "

        private List<TreeNode> _searchResults = new List<TreeNode>();
        private int _currentSearchIndex = -1;
        private string _lastSearchText = string.Empty;
        private Color _originalBackColor = Color.Empty;

        #endregion

        //add a static instance:
        public static MDIFrmDashBoard Instance { get; private set; }

        #region "FrmMainMdi_Shown"
        private async void FrmMainMdi_Shown(object sender, EventArgs e)
        {
            try
            {
                UseWaitCursor = true;
                // Set Splitter Control Panel Distance
                SetSplitterDistance();

                // Set application title with version info
                var assembly = Assembly.GetExecutingAssembly();
                var fileInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
                var buildDate = File.GetLastWriteTime(assembly.Location);

                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    var clickOnceVersion = ApplicationDeployment.CurrentDeployment.CurrentVersion;
                    this.Text = $"{fileInfo.ProductName} {clickOnceVersion}";
                }
                else
                {
                    var version = assembly.GetName().Version;
                    this.Text = $"{fileInfo.ProductName} Version {version}";
                }

                // Set company information
                LabelCompany.Text = $"{fileInfo.ProductName} is a product of {fileInfo.CompanyName}";
                LabelSupportAvailable.Text = $"Support is available from: {ClassGlobalVariables.pubProgrammer}";

                // Set build date
                LblBuildDate.Text = $"[ Build Date: {buildDate} ]";

                // Set email link
                LinkLabelEmail.Text = $"Email: {ClassGlobalVariables.pubProgrammerEmails.Replace("mailto:", "")}";

                // Set database path
                TsLblDbPath.Text = ClassGlobalVariables.pubDatabaseName;

                // Set application version info
                string mApplicationVersion;
                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    var clickOnceVersion = ApplicationDeployment.CurrentDeployment.CurrentVersion;
                    mApplicationVersion = $"{fileInfo.ProductName} Version {clickOnceVersion.Major}.{clickOnceVersion.Minor} " +
                                         $"Build {clickOnceVersion.Build} Revision {clickOnceVersion.Revision}";
                }
                else
                {
                    var version = assembly.GetName().Version;
                    mApplicationVersion = $"{fileInfo.ProductName} Version {version.Major}.{version.Minor} " +
                                         $"Build {version.Build} Revision {version.Revision}";
                }

                TsLblBuildDate.Text = $"{mApplicationVersion} [ Build Date: {buildDate} ]";
                TsLblUserLogin.Text = $"{ClassGlobalVariables.pubUserLoginName} | {ClassGlobalVariables.pubUserRole}";

                // Set form title
                this.Text = fileInfo.ProductName;

                // Show welcome notification
               this.notifyIcon1.ShowBalloonTip(
                    3000,
                    fileInfo.ProductName,
                    $"Welcome {ClassGlobalVariables.pubUserLoginName}\n\nToday Is {DateTime.Now.ToLongDateString()}",
                    ToolTipIcon.Info
                );

                // Set up tab control
                TabCtrlMain.ImageList = ImageListMdi;
                TabCtrlMain.SelectTab(0);
                TabCtrlMain.SelectedTab.ImageIndex = 0;

                // Initialize default values
                await SetDefaultValuesAsync();

                // Read user defaults
                await ReadUserLoginDefaultXmlAsync();

                // Load Tree View
                await LoadTreeViewAsync();

                // Set focus
                TsCmbTcodes.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Form Shown Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }
        #endregion

        #region "TsMenuSideNav_Click"
        private void TsMenuSideNav_Click(object sender, EventArgs e)
        {
            SplitMain.Panel1Collapsed = !SplitMain.Panel1Collapsed;
        }
        #endregion

        #region "SetSplitterDistance"
        private void SetSplitterDistance()
        {
            try
            {
                
                // Set SplitterDistance to 30% and 40% of the total width
                SplitMain.SplitterDistance = (int)(this.SplitMain.Width * 0.2);
                SplitLeft.SplitterDistance = (int)(this.SplitLeft.Width * 0.2);
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

        #region "SetDefaultValuesAsync"

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task SetDefaultValuesAsync()
        {
            try
            {
                if (ClassGlobalVariables.pubUserRole.Equals("SUPER", StringComparison.OrdinalIgnoreCase) ||
                    ClassGlobalVariables.pubUserRole.Equals("ADMIN", StringComparison.OrdinalIgnoreCase))
                {
                    TsMenuAdmin.Visible = true;
                }
                else
                {
                    TsMenuAdmin.Visible = false;
                }

                await FillEmployeeTypesAsync(ClassGlobalVariables.pubUnitCode);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Dock Controls",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Consider adding more detailed error logging here
                // Logger.LogError(ex, "Error in SetDefaultValuesAsync");
            }
        }
        #endregion

        #region "OpenFormInTab"
        public void OpenFormInTab(string formClassName, string tabTitle)
        {
            try
            {
                // Detailed validation
                var validation = ValidateFormClass(formClassName);
                if (!validation.isValid)
                {
                    ShowError("Invalid Form", validation.errorMessage);
                    return;
                }

                // Check if tab already exists
                if (ActivateExistingTab(formClassName))
                    return;

                // Create form instance
                var form = CreateFormInstance(formClassName);
                if (form == null)
                {
                    ShowError("Creation Failed", $"Failed to create instance of '{formClassName}'");
                    return;
                }

                // Configure form for embedding
                ConfigureFormForTab(form);

                // Create and setup new tab
                var tab = CreateTabPage(formClassName, tabTitle, form);

                // Add to TabControl and show
                TabCtrlMain.TabPages.Add(tab);
                TabCtrlMain.SelectedTab = tab;
                form.Show();
            }
            catch (Exception ex)
            {
                ShowError("Open Form In Tab", ex.Message);
            }
        }

        private bool ActivateExistingTab(string formClassName)
        {
            foreach (TabPage tab in TabCtrlMain.TabPages)
            {
                if (tab.Name.Equals(formClassName, StringComparison.OrdinalIgnoreCase))
                {
                    TabCtrlMain.SelectedTab = tab;
                    return true;
                }
            }
            return false;
        }

        private Form CreateFormInstance(string formClassName)
        {
            Type formType = ClassLayoutHelper.GetFormTypeByNameLinq(formClassName);
            if (formType == null)
            {
                MessageBox.Show($"Form class not found: {formClassName}",
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            return Activator.CreateInstance(formType) as Form;
        }

        private void ConfigureFormForTab(Form form)
        {
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
        }

        private TabPage CreateTabPage(string formClassName, string tabTitle, Form form)
        {
            const string CLOSE_SYMBOL = "✖";
            var tab = new TabPage($"  {tabTitle}   {CLOSE_SYMBOL}")
            {
                Name = formClassName,
                Padding = new Padding(3),
                BackColor = SystemColors.Control
            };

            tab.Controls.Add(form);
            return tab;
        }

        private void ShowError(string caption, string message)
        {
            MessageBox.Show(message, caption,
                          MessageBoxButtons.OK, MessageBoxIcon.Error);

            // Consider adding logging here
            // Logger.Error(message);
        }
        #endregion

        #region "IsFormClassValid"
        /// <summary>
        /// 19.08.2025
        /// alidates that the form class exists and can be instantiated
        /// </summary>
        /// <param name="formClassName"></param>
        /// <returns></returns>
        private bool IsFormClassValid(string formClassName)
        {
            if (string.IsNullOrWhiteSpace(formClassName))
                return false;

            try
            {
                // Get the type from the current assembly
                Type formType = Type.GetType(formClassName, false, true);

                // If not found in current assembly, try all loaded assemblies
                if (formType == null)
                {
                    formType = AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(assembly => assembly.GetTypes())
                        .FirstOrDefault(type => type.FullName == formClassName || type.Name == formClassName);
                }

                // Check if it's a Form class
                return formType != null && typeof(Form).IsAssignableFrom(formType);
            }
            catch
            {
                return false; // Any error means the class is not valid
            }
        }
        #endregion

        #region "ShowPasswordInputDialog"
        private string ShowPasswordInputDialog()
        {
            using (var frm = new FrmAdminInputBox())
            {
                return frm.ShowDialog() == DialogResult.OK ? frm.TxtPassword.Text : null;
            }
        }
        #endregion

        #region "ShowMessage"
        private void ShowMessage(string message, string title, MessageBoxIcon icon)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }
        #endregion

        #region "ShowErrorMessage"
        private void ShowErrorMessage(Exception ex)
        {
            MessageBox.Show($"Message: {ex.Message}\n\nStack Trace: {ex.StackTrace}",
                          "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            // Consider logging the error
            // Logger.Error(ex, "Error in TsMenuAdminManageUsers_Click");
        }
        #endregion

        #region "TsBtnExit_Click"
        /// <summary>
        /// await SaveUserLoginDefaultXmlAsync(), the method will await until the save finishes (or throws).
        /// Only after the await is done will this.Close() run.
        /// the save will complete before the form closes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TsBtnExit_Click(object sender, EventArgs e)
        {
            try
            {
                // save default values to sql table
                UseWaitCursor = true;
                await SaveUserLoginDefaultXmlAsync();
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex); // log or show message
            }
            finally
            {
                UseWaitCursor = false;
                this.Close();
            }
        }
        #endregion

        #region "TsBtnLogOut_Click"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsBtnLogOut_Click(object sender, EventArgs e)
        {

            using (var loginForm = new FrmAuthUsersLogin())
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    // Reset dashboard without creating new instance
                    // First collect tabs to remove
                    var tabsToRemove = new List<TabPage>();

                    foreach (TabPage tab in TabCtrlMain.TabPages)
                    {
                        if (tab != TbPgDashBoard)
                            tabsToRemove.Add(tab);
                    }

                    // Now remove them
                    foreach (TabPage tab in tabsToRemove)
                    {
                        TabCtrlMain.TabPages.Remove(tab);
                    }

                    // Call MDIFrmDashBoard_Shown (like in VB)
                    FrmMainMdi_Shown(null, null);
                }
                else
                {
                    // Exit if login failed or cancelled
                    Application.Exit();
                }
            }
        }
        #endregion

        #region "TsBtnExpandAppInfo_Click"
        private void TsBtnExpandAppInfo_Click(object sender, EventArgs e)
        {
            // Toggle panel collapse state
            SplitLeft.Panel1Collapsed = !SplitLeft.Panel1Collapsed;

            // Set splitter distance to 20% of container height
            SplitLeft.SplitterDistance = (int)(SplitLeft.Height * 0.2);

            // Optional: Update button appearance based on state
            // UpdateExpandButtonAppearance();
        }

        #endregion

        #region "TabCtrlMain_MouseDown"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabCtrlMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                for (int i = 0; i < TabCtrlMain.TabPages.Count; i++)
                {
                    Rectangle tabRect = TabCtrlMain.GetTabRect(i);

                    // Approximate area of ✖ (right side of tab)
                    Rectangle closeButtonRect = new Rectangle(tabRect.Right - 20, tabRect.Top + 4, 16, 16);

                    if (closeButtonRect.Contains(e.Location))
                    {
                        TabCtrlMain.TabPages.RemoveAt(i);
                        return;
                    }
                }
            }
        }
        #endregion

        #region "TsBtnRefresh_Click"
        private async void TsBtnRefresh_Click(object sender, EventArgs e)
        {

            // Load Tree View
            await LoadTreeViewAsync();

        }
        #endregion

        #region "TsBtnDrpEmpCatg_DropDownItemClicked"
        private async void TsBtnDrpEmpCatg_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
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

                // Load TreeView
                // await LoadTreeViewMenuAsync();
                await LoadTreeViewAsync();

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

        #region "aboutToolStripMenuItem_Click"
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var f = new FrmAboutDialog())
            {
                f.ShowDialog();
            }
        }
        #endregion

        #region "AdminMenu"
        /// <summary>
        /// 15.08.2025
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        #region "ManageUsers"
        private async void TsMenuAdminManageUsers_Click(object sender, EventArgs e)
        {
            try
            {
                // Check for user role
                var allowedRoles = new[] { "SUPER", "ADMIN" };
                if (allowedRoles.Contains(ClassGlobalVariables.pubUserRole, StringComparer.OrdinalIgnoreCase))
                {
                    var manageUsersForm = new FrmAuthManageUsers();
                    OpenFormInTab("FrmAuthManageUsers", manageUsersForm.Text);
                    return;
                }

                bool isValidMasterPassword = false;
                int maxAttempts = 3;

                for (int attempts = 0; attempts < maxAttempts && !isValidMasterPassword; attempts++)
                {
                    string password = ShowPasswordInputDialog();

                    if (string.IsNullOrEmpty(password))
                    {
                        ShowMessage("Password cannot be empty. Access Denied", "Information", MessageBoxIcon.Information);
                        continue;
                    }

                    isValidMasterPassword = await ClassDbHelpers.ValidateMasterCredentialsAsync(
                        ClassUserSession.Instance.LoginUserName,
                        password,
                        ClassUserSession.Instance.LoginUserUnitCode);

                    if (isValidMasterPassword)
                    {
                        var manageUsersForm = new FrmAuthManageUsers();
                        OpenFormInTab("FrmAuthManageUsers", manageUsersForm.Text);
                        return;
                    }
                    else
                    {
                        int remainingAttempts = maxAttempts - attempts - 1;
                        if (remainingAttempts > 0)
                        {
                            ShowMessage($"Wrong Password. You have {remainingAttempts} attempt(s) left.",
                                      "Information", MessageBoxIcon.Information);
                        }
                    }
                }

                if (!isValidMasterPassword)
                {
                    ShowMessage("Access Denied. You have used all attempts.",
                              "Access Denied", MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex);
            }
        }
        #endregion 

        #region "DesignMenu"
        /// <summary>
        /// 16.08.2025
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private async void TsMenuAdminSetMenuGrp_Click(object sender, EventArgs e)
        {
            try
            {
                // Check for SUPER user role
                var allowedRoles = new[] { "SUPER", "ADMIN" };
                if (allowedRoles.Contains(ClassGlobalVariables.pubUserRole, StringComparer.OrdinalIgnoreCase))
                {
                    var manageForm = new FrmMenuSetGroups();
                    OpenFormInTab("FrmMenuSetGroups", manageForm.Text);
                    return;
                }

                bool isValidMasterPassword = false;
                int maxAttempts = 3;

                for (int attempts = 0; attempts < maxAttempts && !isValidMasterPassword; attempts++)
                {
                    string password = ShowPasswordInputDialog();

                    if (string.IsNullOrEmpty(password))
                    {
                        ShowMessage("Password cannot be empty. Access Denied", "Information", MessageBoxIcon.Information);
                        continue;
                    }

                    isValidMasterPassword = await ClassDbHelpers.ValidateMasterCredentialsAsync(
                        ClassUserSession.Instance.LoginUserName,
                        password,
                        ClassUserSession.Instance.LoginUserUnitCode);

                    if (isValidMasterPassword)
                    {
                        var manageForm = new FrmMenuSetGroups();
                        OpenFormInTab("FrmMenuSetGroups", manageForm.Text);
                        return;
                    }
                    else
                    {
                        int remainingAttempts = maxAttempts - attempts - 1;
                        if (remainingAttempts > 0)
                        {
                            ShowMessage($"Wrong Password. You have {remainingAttempts} attempt(s) left.",
                                      "Information", MessageBoxIcon.Information);
                        }
                    }
                }

                if (!isValidMasterPassword)
                {
                    ShowMessage("Access Denied. You have used all attempts.",
                              "Access Denied", MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex);
            }
        }


        /// <summary>
        /// 16.08.2025
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private async void TsMenuAdminSetMenuStructure_Click(object sender, EventArgs e)
        {
            try
            {
                // Check for user role
                var allowedRoles = new[] { "SUPER", "ADMIN" };
                if (allowedRoles.Contains(ClassGlobalVariables.pubUserRole, StringComparer.OrdinalIgnoreCase))
                {
                    var manageUsersForm = new FrmAdminMenuStructure();
                    OpenFormInTab("FrmAdminMenuStructure", manageUsersForm.Text);
                    return;
                }

                bool isValidMasterPassword = false;
                int maxAttempts = 3;

                for (int attempts = 0; attempts < maxAttempts && !isValidMasterPassword; attempts++)
                {
                    string password = ShowPasswordInputDialog();

                    if (string.IsNullOrEmpty(password))
                    {
                        ShowMessage("Password cannot be empty. Access Denied", "Information", MessageBoxIcon.Information);
                        continue;
                    }

                    isValidMasterPassword = await ClassDbHelpers.ValidateMasterCredentialsAsync(
                        ClassUserSession.Instance.LoginUserName,
                        password,
                        ClassUserSession.Instance.LoginUserUnitCode);

                    if (isValidMasterPassword)
                    {
                        var manageForm = new FrmAdminMenuStructure();
                        OpenFormInTab("FrmAdminMenuStructure", manageForm.Text);
                        return;
                    }
                    else
                    {
                        int remainingAttempts = maxAttempts - attempts - 1;
                        if (remainingAttempts > 0)
                        {
                            ShowMessage($"Wrong Password. You have {remainingAttempts} attempt(s) left.",
                                      "Information", MessageBoxIcon.Information);
                        }
                    }
                }

                if (!isValidMasterPassword)
                {
                    ShowMessage("Access Denied. You have used all attempts.",
                              "Access Denied", MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex);
            }
        }


        #endregion

        #region "UserPlantAccess"

        /// <summary>
        /// 25.08.2025
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private async void TsMenuAdminSetUserPlantAccess_Click(object sender, EventArgs e)
        {
            try
            {

                var allowedRoles = new[] { "SUPER", "ADMIN" };
                if (allowedRoles.Contains(ClassGlobalVariables.pubUserRole, StringComparer.OrdinalIgnoreCase))
                {
                    var manageUsersForm = new FrmAuthUserPlantAccess();
                    OpenFormInTab("FrmAuthUserPlantAccess", manageUsersForm.Text);
                    return;
                }

                bool isValidMasterPassword = false;
                int maxAttempts = 3;

                for (int attempts = 0; attempts < maxAttempts && !isValidMasterPassword; attempts++)
                {
                    string password = ShowPasswordInputDialog();

                    if (string.IsNullOrEmpty(password))
                    {
                        ShowMessage("Password cannot be empty. Access Denied", "Information", MessageBoxIcon.Information);
                        continue;
                    }

                    isValidMasterPassword = await ClassDbHelpers.ValidateMasterCredentialsAsync(
                        ClassUserSession.Instance.LoginUserName,
                        password,
                        ClassUserSession.Instance.LoginUserUnitCode);

                    if (isValidMasterPassword)
                    {
                        var manageForm = new FrmAuthUserPlantAccess();
                        OpenFormInTab("FrmAuthUserPlantAccess", manageForm.Text);
                        return;
                    }
                    else
                    {
                        int remainingAttempts = maxAttempts - attempts - 1;
                        if (remainingAttempts > 0)
                        {
                            ShowMessage($"Wrong Password. You have {remainingAttempts} attempt(s) left.",
                                      "Information", MessageBoxIcon.Information);
                        }
                    }
                }

                if (!isValidMasterPassword)
                {
                    ShowMessage("Access Denied. You have used all attempts.",
                              "Access Denied", MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex);
            }
        }
        #endregion

        #endregion

        #region "TsBtnTviewSearch_Click"
        private async void TsBtnTviewSearch_Click(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (sender == TsBtnTviewSearchClear)
                    TsTxtTviewSearch.Text = null;
               
                await LoadTreeViewAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "TsBtnTviewSearch_Click", MessageBoxButtons.OK,MessageBoxIcon.Exclamation );
            }
        }

        #endregion 

        #region "FillEmployeeTypesAsync"
        /// <summary>
        /// Used verbatim string ($@) for cleaner SQL query formatting
        /// Simplified the dropdown item search using LINQ
        /// </summary>
        /// <returns></returns>
        private async Task FillEmployeeTypesAsync(string unitCode = null)
        {
            try
            {
                const string tableName = "dbo.Master_EmploymentTypes";

                // Clear existing items
                TsBtnDrpEmpCatg.DropDownItems.Clear();

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
                        SELECT CatgName, Id 
                        FROM {tableName} 
                        WHERE IsActive = 1 AND IsDeleted = 0 
                        AND UnitCode = @UnitCode
                        ORDER BY SortOrder";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UnitCode", resolvedUnitCode);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                string categoryName = reader["CatgName"] as string ?? "Unnamed Category";
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
                                    foreach (ToolStripItem it in TsBtnDrpEmpCatg.DropDownItems)
                                        if (it is ToolStripMenuItem mi) mi.Checked = false;

                                    menuItem.Checked = true;

                                    TsBtnDrpEmpCatg.Text = menuItem.Text;
                                    TsBtnDrpEmpCatg.Tag = categoryId;
                                };

                                TsBtnDrpEmpCatg.DropDownItems.Add(menuItem);
                            }
                        }
                    }
                }

                // Set default selection from settings
                //if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.SettingEmployeeCategory))
                //{
                //    var foundItem = TsBtnDrpEmpCatg.DropDownItems
                //        .OfType<ToolStripMenuItem>()
                //        .FirstOrDefault(item => item.Text.Equals(
                //            Properties.Settings.Default.SettingEmployeeCategory,
                //            StringComparison.OrdinalIgnoreCase));

                //    if (foundItem != null)
                //    {
                //        TsBtnDrpEmpCatg.Text = Properties.Settings.Default.SettingEmployeeCategory;
                //    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fill Employee Types",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Consider logging the error
                // Logger.Error(ex, "Error in FillEmployeeTypesAsync");
            }
        }

        #endregion

        #region "LoadTreeViewAsync"

        /// <summary>
        /// 18.08.2025
        /// </summary>
        /// <returns></returns>
        private async Task LoadTreeViewAsync()
        {
            try
            {
                UseWaitCursor = true;
                var unitCode = ClassGlobalVariables.pubUnitCode;
                
                int? employeeCatgId = TsBtnDrpEmpCatg.Tag is int id ? id : (int?)null;
                // or, if Tag might be string:
                if (employeeCatgId is null && TsBtnDrpEmpCatg.Tag is string s && int.TryParse(s, out var parsed))
                    employeeCatgId = parsed;

                string searchText = string.IsNullOrWhiteSpace(TsTxtTviewSearch.Text)
                ? null
                : TsTxtTviewSearch.Text.Trim().ToLower();

                // Set ImageList
                TreeViewMain.ImageList = imageListTreeView;

                // exit if employeeCatgId is not found
                if (employeeCatgId == null) return;

                await ClassMenuTreeBuilder.LoadMenuTreeAsync(
                    treeView: TreeViewMain,
                    unitCode: unitCode,
                    empCatgId: employeeCatgId,
                    searchText: searchText
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Menu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        #endregion

        #region "LoadTreeViewMenuAsync"
        /// <summary>
        /// 18.08.2025
        /// </summary>
        /// <returns></returns>
        public async Task LoadTreeViewMenuAsync()
        {
            try
            {

                string unitCode = ClassGlobalVariables.pubUnitCode;
                string defaultCategory = "PERMANENT STAFF";
                string employeeCategory = (TsBtnDrpEmpCatg == null || string.IsNullOrWhiteSpace(TsBtnDrpEmpCatg.Text))
                   ? defaultCategory
                   : TsBtnDrpEmpCatg.Text;

                TreeViewMain.Nodes.Clear();
              
                const string tableName = "dbo.v_AdminMenuStructures";

                var sb = new StringBuilder();
                var parameters = new List<SqlParameter>();

                string searchText = string.IsNullOrWhiteSpace(TsTxtTviewSearch.Text)
                ? null
                : TsTxtTviewSearch.Text.Trim().ToLower();

                // Replace '*' with '%' for SQL LIKE query
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    searchText = searchText.Replace("*", "%");
                }
                else
                {
                    searchText = null; // normalize empty to null
                }

                string connectionString = ClassGlobalFunctions.GetConnectionStringByUnitCode(unitCode);

                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new InvalidOperationException("Connection string cannot be empty");
                }

                sb.Append("SELECT ");
                sb.Append("* ");
                sb.Append($"FROM {tableName} ");

                sb.Append("WHERE 1=1 ");
                sb.Append("AND MenuIsActive = 1 ");
                sb.Append("AND IsDeleted = 0 ");
                sb.Append("AND UnitCode = @UnitCode ");

                sb.Append("AND (@EmpCatgName IS NULL OR EmpCatgName = @EmpCatgName) ");
              
                parameters.Add(new SqlParameter("@UnitCode", SqlDbType.VarChar) { Value = unitCode });
                parameters.Add(new SqlParameter("@EmpCatgName", SqlDbType.VarChar) { Value = employeeCategory });

                var serachColumns = new List<string> {
                    "EmpCatgName",
                    "ParentTitle",
                    "MenuFormTitle",
                    "MenuFormName"};

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
                sb.Append("ORDER BY [EmpCatgSortOrder], [MenuSortOrder] ");

                var query = sb.ToString();

                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(query, conn))
                {
                    if (parameters.Count > 0)
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                    }
                    
                    await conn.OpenAsync();
                    var reader = await cmd.ExecuteReaderAsync();

                    // Build hierarchy
                    while (await reader.ReadAsync())
                    {
                        string unitAliasName = ClassSafeValueHelpers.PubGetSafeValue(reader["UnitAliasName"].ToString());
                        string catg = ClassSafeValueHelpers.PubGetSafeValue(reader["EmpCatgName"].ToString());
                        string group = ClassSafeValueHelpers.PubGetSafeValue(reader["ParentTitle"].ToString());
                        string formTitle = ClassSafeValueHelpers.PubGetSafeValue(reader["MenuFormTitle"].ToString());
                        string formName = ClassSafeValueHelpers.PubGetSafeValue(reader["MenuFormName"].ToString());
                        string menuNodeType = ClassSafeValueHelpers.PubGetSafeValue(reader["MenuNodeType"].ToString());
                        string menuIconName = ClassSafeValueHelpers.PubGetSafeValue(reader["MenuIconName"].ToString());

                        // Set ImageList
                        TreeViewMain.ImageList = imageListTreeView;

                        // Root Node (UnitName)
                        TreeNode unitNode = FindOrCreateNode(TreeViewMain.Nodes, unitAliasName);
                        unitNode.NodeFont = new Font(TreeViewMain.Font, FontStyle.Bold);
                        unitNode.ForeColor = Color.DarkRed;
                        unitNode.ImageKey = "store-small.png";
                        unitNode.SelectedImageKey = "store-small.png";
                        unitNode.Tag = "ROOT_NODE"; // Identify as root node

                        // Category Node (EmpCatgName)
                        TreeNode catgNode = FindOrCreateNode(unitNode.Nodes, catg);
                        catgNode.NodeFont = new Font(TreeViewMain.Font, FontStyle.Bold);
                        catgNode.ForeColor = Color.DarkRed;
                        catgNode.ImageKey = "agt_family16x16.png";
                        catgNode.SelectedImageKey = "agt_family16x16.png";
                        catgNode.Tag = "CATEGORY_NODE"; // Identify as category node

                        // Group Node (ParentTitle)
                        TreeNode groupNode = FindOrCreateNode(catgNode.Nodes, group);
                        groupNode.NodeFont = new Font(TreeViewMain.Font, FontStyle.Bold);
                        groupNode.ForeColor = Color.DarkBlue;
                        groupNode.ImageKey = "pinned.ico";
                        groupNode.SelectedImageKey = "pinned.ico";
                        groupNode.Tag = "GROUP_NODE"; // Identify as group node

                        // Command Node (MenuFormTitle → Tag = MenuFormName)
                        if (!string.IsNullOrWhiteSpace(formTitle))
                        {
                            TreeNode cmdNode = FindOrCreateNode(groupNode.Nodes, formTitle);
                            cmdNode.Tag = string.IsNullOrWhiteSpace(formName) ? null : formName;
                            cmdNode.NodeFont = new Font(TreeViewMain.Font, FontStyle.Regular);
                            cmdNode.ForeColor = Color.Blue;
                            cmdNode.ImageKey = "notebook16x16.png";
                            cmdNode.SelectedImageKey = "tick_small16x16.png";
                        }

                        // Ensure width is sufficient
                        TreeViewMain.Width = 500;
                        TreeViewMain.ExpandAll();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading menu:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region "TsBtnExpandCollapse_Click"
        private void TsBtnExpandCollapse_Click(object sender, EventArgs e)
        {
            var button = sender as ToolStripButton;
            if (button == null || TreeViewMain.Nodes.Count == 0) return;

            bool currentlyExpanded = TreeViewMain.Nodes[0].IsExpanded;

            if (currentlyExpanded)
            {
                TreeViewMain.CollapseAll();
                button.Text = "Expand All";
                button.ToolTipText = "Expand all nodes";
            }
            else
            {
                TreeViewMain.ExpandAll();
                button.Text = "Collapse All";
                button.ToolTipText = "Collapse all nodes";
            }
        }
        #endregion

        #region "FindOrCreateNode"
        private TreeNode FindOrCreateNode(TreeNodeCollection nodes, string text)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Text == text)
                    return node;
            }
            var newNode = new TreeNode(text);
            nodes.Add(newNode);
            return newNode;
        }
        #endregion

        #region "TreeViewMain_BeforeCollapse"
        private void TreeViewMain_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            // Only affect nodes marked as group nodes
            if (e.Node.Tag?.ToString() == "GROUP_NODE")
            {
                e.Node.ImageKey = "unpinned.ico";
                e.Node.SelectedImageKey = "unpinned.ico";
            }
        }
        #endregion

        #region "TreeViewMain_BeforeExpand"
        private void TreeViewMain_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            // Only affect nodes marked as group nodes
            if (e.Node.Tag?.ToString() == "GROUP_NODE")
            {
                e.Node.ImageKey = "pinned.ico";
                e.Node.SelectedImageKey = "pinned.ico";
            }
        }
        #endregion

        #region "TreeViewMain_NodeMouseDoubleClick"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeViewMain_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // Only process left mouse button double-clicks
            if (e.Button != MouseButtons.Left) return;

            if (e.Node?.Tag != null)
            {
                string tagValue = e.Node.Tag.ToString();

                // Check if it's NOT a group node and NOT a root node
                if (tagValue != "GROUP_NODE" && tagValue != "ROOT_NODE")
                {
                    string formClassName = tagValue;
                    string tabTitle = e.Node.Text;

                    // Additional validation to ensure it's a valid form class name
                    if (!string.IsNullOrWhiteSpace(formClassName) && IsFormClassValid(formClassName))
                    {
                        OpenFormInTab(formClassName, tabTitle);
                    }
                }
            }
        }
        #endregion

        #region "ValidateFormClass"
        /// <summary>
        /// 19.08.2025
        /// More detailed validation method with error information
        /// </summary>
        /// <param name="formClassName"></param>
        /// <returns></returns>
        private (bool isValid, string errorMessage) ValidateFormClass(string formClassName)
        {
            if (string.IsNullOrWhiteSpace(formClassName))
                return (false, "Form class name is empty");

            try
            {
                Type formType = Type.GetType(formClassName, false, true);

                if (formType == null)
                {
                    // Search in all assemblies
                    formType = AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(assembly => assembly.GetTypes())
                        .FirstOrDefault(type => type.FullName == formClassName || type.Name == formClassName);

                    if (formType == null)
                        return (false, $"Form class '{formClassName}' not found in any loaded assembly");
                }

                if (!typeof(Form).IsAssignableFrom(formType))
                    return (false, $"Class '{formClassName}' is not a Form type");

                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, $"Error validating form class: {ex.Message}");
            }
        }
        #endregion


        #region "TsTxtTviewSearch_KeyDown"
        /// <summary>
        /// Search when user presses Enter in textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsTxtTviewSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformSearch();
                e.Handled = e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.F3)
            {
                FindNext();
                e.Handled = e.SuppressKeyPress = true;
            }
        }
        #endregion

        #region "TsTxtTviewSearch_TextChanged"
        private void TsTxtTviewSearch_TextChanged(object sender, EventArgs e)
        {
            // Optional: Perform search as user types (add delay to avoid performance issues)
            // You might want to use a Timer for delayed search
        }
        #endregion

        #region "PerformSearch"
        private void PerformSearch()
        {
            string searchText = TsTxtTviewSearch.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                ClearSearchHighlight();
                return;
            }

            // If search text changed, start new search
            if (searchText != _lastSearchText)
            {
                ClearSearchHighlight();
                _searchResults = FindNodesWithText(TreeViewMain.Nodes, searchText, StringComparison.OrdinalIgnoreCase);
                _currentSearchIndex = -1;
                _lastSearchText = searchText;
            }

            if (_searchResults.Count > 0)
            {
                FindNext();
            }
            else
            {
                MessageBox.Show($"No results found for '{searchText}'", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region "FindNext"
        private void FindNext()
        {
            if (_searchResults.Count == 0) return;

            _currentSearchIndex = (_currentSearchIndex + 1) % _searchResults.Count;
            TreeNode foundNode = _searchResults[_currentSearchIndex];

            // Clear previous highlight
            ClearSearchHighlight();

            // Highlight current node
            foundNode.BackColor = Color.Yellow;
            foundNode.ForeColor = Color.Black;
            _originalBackColor = foundNode.BackColor; // Store for clearing later

            // Ensure node is visible and selected
            foundNode.EnsureVisible();
            TreeViewMain.SelectedNode = foundNode;
            TreeViewMain.Focus();

            // Update status (optional)
            UpdateSearchStatus();
        }
        #endregion

        #region "FindNodesWithText"
        /// <summary>
        /// Recursive method to find nodes containing text
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="searchText"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        private List<TreeNode> FindNodesWithText(TreeNodeCollection nodes, string searchText, StringComparison comparison)
        {
            var results = new List<TreeNode>();

            foreach (TreeNode node in nodes)
            {
                // Check if node text contains search text
                if (node.Text.IndexOf(searchText, comparison) >= 0)
                {
                    results.Add(node);
                }

                // Recursively search child nodes
                results.AddRange(FindNodesWithText(node.Nodes, searchText, comparison));
            }

            return results;
        }
        #endregion

        #region "ClearSearchHighlight"
        /// <summary>
        /// Clear all search highlights
        /// </summary>
        private void ClearSearchHighlight()
        {
            foreach (TreeNode node in _searchResults)
            {
                node.BackColor = TreeViewMain.BackColor;
                node.ForeColor = TreeViewMain.ForeColor;
            }

            _searchResults.Clear();
            _currentSearchIndex = -1;
            _lastSearchText = string.Empty;
        }
        #endregion

        #region "UpdateSearchStatus"
        private void UpdateSearchStatus()
        {
            //if (_searchResults.Count > 0)
            //{
            //    TsLblSearchStatus.Text = $"{_currentSearchIndex + 1} of {_searchResults.Count}";
            //}
            //else
            //{
            //    TsLblSearchStatus.Text = "No results";
            //}
        }
        #endregion

        #region "PerformAdvancedSearch"
        /// <summary>
        /// Advanced search with options
        /// </summary>
        private void PerformAdvancedSearch()
        {
            string searchText = TsTxtTviewSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchText)) return;

            var searchOptions = new SearchOptions
            {
                //MatchCase = ChkMatchCase.Checked,
                //WholeWord = ChkWholeWord.Checked,
                //SearchInTags = ChkSearchInTags.Checked
                MatchCase = false,
                WholeWord = false,
                SearchInTags = true
            };

            _searchResults = FindNodes(TreeViewMain.Nodes, searchText, searchOptions);
            _currentSearchIndex = -1;
            _lastSearchText = searchText;

            if (_searchResults.Count > 0)
            {
                FindNext();
            }
        }
        #endregion

        #region "FindNodes"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="searchText"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        private List<TreeNode> FindNodes(TreeNodeCollection nodes, string searchText, SearchOptions options)
        {
            var results = new List<TreeNode>();
            var comparison = options.MatchCase ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

            foreach (TreeNode node in nodes)
            {
                bool matches = false;

                // Search in text
                if (options.WholeWord)
                {
                    matches = node.Text.Equals(searchText, comparison);
                }
                else
                {
                    matches = node.Text.IndexOf(searchText, comparison) >= 0;
                }

                // Search in tag (optional)
                if (!matches && options.SearchInTags && node.Tag != null)
                {
                    string tagText = node.Tag.ToString();
                    if (options.WholeWord)
                    {
                        matches = tagText.Equals(searchText, comparison);
                    }
                    else
                    {
                        matches = tagText.IndexOf(searchText, comparison) >= 0;
                    }
                }

                if (matches)
                {
                    results.Add(node);
                }

                results.AddRange(FindNodes(node.Nodes, searchText, options));
            }

            return results;
        }
        #endregion

        #region "SaveUserLoginDefaultXmlAsync"
        /// <summary>
        /// 
        /// </summary>

        private async Task SaveUserLoginDefaultXmlAsync() 
        {
            try 
            {
                var defaults = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    ["PlantCode"] = ClassGlobalVariables.pubUnitCode,
                    ["EmployeeCategory"] = TsBtnDrpEmpCatg.Text
                };

                string connectionString = ClassGlobalFunctions.GetConnectionStringByUnitCode(ClassGlobalVariables.pubUnitCode);
                const string tableName = "dbo.UserLoginDefaults";

                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new Exception("Connection string is empty.");
                }

                // Save (upsert) to dbo.UserLoginDefaults
                await ClassDbHelpers.SaveUserLoginDefaultXmlAsync(
                    connectionString: connectionString,
                    userRowGuid: ClassGlobalVariables.pubLoginUserRowGuid,
                    tableName,
                    elements: defaults
                );

            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex);
            }

        }
        #endregion

        #region "ReadUserLoginDefaultXmlAsync"
        /// <summary>
        /// async void ➝ only for event handlers (e.g., button_Click).
        /// Everywhere else ➝ use async Task (or async Task<T> for returning results).
        /// That way you can await the method properly.
        /// </summary>
        /// <returns></returns>
        private async Task ReadUserLoginDefaultXmlAsync()
        {
            try 
            {

                string connectionString = ClassGlobalFunctions.GetConnectionStringByUnitCode(
                    ClassGlobalVariables.pubUnitCode);

                const string tableName = "dbo.UserLoginDefaults";

                string xml = await ClassDbHelpers.GetUserLoginDefaultXmlAsync(
                    connectionString,
                    ClassGlobalVariables.pubLoginUserRowGuid,
                    tableName);

                var wanted = new[] { "PlantCode", "EmployeeCategory" };
                var vals = ClassXmlHelper.ExtractElements(xml, wanted);

                // use the values
                string plantCode = vals.ContainsKey("PlantCode")
                ? vals["PlantCode"]
                : string.Empty;

                string empCat = vals.ContainsKey("EmployeeCategory")
                ? vals["EmployeeCategory"]
                : string.Empty;

                // Only assign if TsBtnDrpEmpCatg has this empCat in its dropdown
                var selectedItem = TsBtnDrpEmpCatg.DropDownItems
                    .OfType<ToolStripMenuItem>()
                    .FirstOrDefault(item => string.Equals(item.Text, empCat, StringComparison.OrdinalIgnoreCase));

                if (selectedItem != null)
                {
                    TsBtnDrpEmpCatg.Text = selectedItem.Text;
                    TsBtnDrpEmpCatg.Tag = selectedItem.Tag; // keep Text and Tag in sync
                }


            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex);
            }
        }


        #endregion

    }
}

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
using TimeAttendanceManager.Auth.Models;
using TimeAttendanceManager.Helpers.Classes;
using TimeAttendanceManager.Main.Classes;
using TimeAttendanceManager.Main.Forms;

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
///    Title:          Code for User's Plant Permissions
///                    
/// 
///    Name:           FrmAuthUserPlantAccess.cs
///    Created:        25th August 2025
///    Date Completed: 25th August 2025
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
    public partial class FrmAuthUserPlantAccess : Form
    {
        #region "Constructor"
        public FrmAuthUserPlantAccess()
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
            this.TsBtnFilterClose.Click -= TsBtnFilterClose_Click;
            this.TreeViewMain.AfterCheck -= Tree_AfterCheck_Propagate;
            this.TsBtnDrpUnitCode.DropDownItemClicked -= TsBtnDrpBtn_DropDownItemClicked;
            this.TsBtnDrpUserRole.DropDownItemClicked -= TsBtnDrpBtn_DropDownItemClicked;
            this.TsBtRefreshTview.Click -= TsBtRefreshTview_Click;
            this.TsBtnClearSearchTview.Click -= TsBtRefreshTview_Click;
            this.TsBtnSearchTview.Click -= TsBtRefreshTview_Click;
            this.TsTxtSearchTview.KeyDown -= TsTxtSearchTview_KeyDown;
            this.TsBtRefreshDgv.Click -= TsBtRefreshDgv_Click;
            this.TsBtnClearSearchDgv.Click -= TsBtRefreshDgv_Click;
            this.TsTxtSearchDgv.KeyDown -= TsTxtSearchDgv_KeyDown;
            this.TreeViewMain.AfterSelect -= TreeViewMain_AfterSelect;
            this.TsBtnSave.Click -= TsBtnSave_Click;
            
            // Add events
            this.Load += Form_Load;
            this.FormClosing += FormClosing_FormClosing;
            this.TsBtnCloseForm.Click += TsBtnCloseForm_Click;
            this.TsBtnClose.Click += TsBtnCloseForm_Click;
            this.TabCtrlMain.Click += TabCtrlMain_Click;
            this.TsBtnSidePanel.Click += TsBtnSideMenu_Click;
            this.TsBtnHelp.Click += TsBtnHelp_Click;
            this.TsBtnOptTech.Click += TsBtnOptTech_Click;
            this.TsBtnFilterClose.Click += TsBtnFilterClose_Click;
            this.TreeViewMain.AfterCheck += Tree_AfterCheck_Propagate;
            this.TsBtnDrpUnitCode.DropDownItemClicked += TsBtnDrpBtn_DropDownItemClicked;
            this.TsBtnDrpUserRole.DropDownItemClicked += TsBtnDrpBtn_DropDownItemClicked;
            this.TsBtRefreshTview.Click += TsBtRefreshTview_Click;
            this.TsBtnClearSearchTview.Click += TsBtRefreshTview_Click;
            this.TsBtnSearchTview.Click += TsBtRefreshTview_Click;
            this.TsTxtSearchTview.KeyDown += TsTxtSearchTview_KeyDown;
            this.TsBtRefreshDgv.Click += TsBtRefreshDgv_Click;
            this.TsBtnClearSearchDgv.Click += TsBtRefreshDgv_Click;
            this.TsTxtSearchDgv.KeyDown += TsTxtSearchDgv_KeyDown;
            this.TreeViewMain.AfterSelect += TreeViewMain_AfterSelect;
            this.TsBtnSave.Click += TsBtnSave_Click;
            
        }
        #endregion

        #region "LocalVariables"
        private ContextMenuStrip _cmsDgvList;
        private const string CanViewColumnName = "HasAccess";

        private AdminUserUnitAccess myDataTable = new AdminUserUnitAccess();
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

                    // Update TreeView
                    await LoadTreeViewAsync();

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
                SplitMain.SplitterDistance = (int)(this.SplitMain.Width * 0.2);
                SplitRight.SplitterDistance = (int)(this.SplitRight.Width * 0.6);
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
                await FillUnitCodeAsync();
                await FillUserRolesAsync();
                await ReadUserLoginDefaultXmlAsync();

                // add context menu for DataGridView
                InitDgvListContextMenu();
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

        #region "TsBtnFilterClose_Click"
        private void TsBtnFilterClose_Click(object sender, EventArgs e)
        {

            SplitRight.Panel2Collapsed = true;
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
                SplitRight.Panel2Collapsed = !SplitRight.Panel2Collapsed;

                // Build the tech details object
                var mTechDetails = new ClassTechDetails
                {
                    // Set form identity
                    FormName = this.Name,
                    FormTitle = this.Text,

                    // Set default "created" date (VB had FormatDateTime("09-MAY-25", ShortDate))
                    // Here we parse explicitly as dd-MMM-yy using invariant culture.
                    FormCreated = DateTime.ParseExact("25-AUG-2025", "dd-MMM-yyyy", CultureInfo.InvariantCulture)
                };

                // Add table names used in this form (9 fields as in your VB)
                mTechDetails.TableNames.Add(new TableNames(
                    "dbo.AdminUserUnitAccess",
                    "dbo.v_AdminUserUnitAccess",
                    "dbo.UserLogins",
                    "dbo.usp_AdminUserUnitAccess_Upsert",
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

        #region "TsBtRefreshTview_Click"
        private async void TsBtRefreshTview_Click(System.Object sender, System.EventArgs e)
        {
            if (sender == TsBtnClearSearchTview)
                TsTxtSearchTview.Text = null;

            await LoadTreeViewAsync();
        }
        #endregion

        #region "TsTxtSearchTview_KeyDown"
        private void TsTxtSearchTview_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)

                // Update DataGridView
                TsBtRefreshTview_Click(null, null);
        }

        #endregion

        #region "TreeViewMain_AfterCheck"
        private async void TreeViewMain_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null && e.Node.Tag != null)
            {
                string userRowGuid = e.Node.Tag.ToString();
                await LoadDataGridViewAsync(userRowGuid);
            }
        }
        #endregion

        #region "LoadTreeViewAsync"
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task LoadTreeViewAsync()
        {
            try
            {


                string defaultUnitCode = null;
                string unitCode = (TsBtnDrpUnitCode == null ||
                    string.Equals(TsBtnDrpUnitCode.Text, "(All)", StringComparison.Ordinal) ||
                    string.Equals(TsBtnDrpUnitCode.Text, "Plant Code", StringComparison.Ordinal))
                   ? defaultUnitCode
                   : TsBtnDrpUnitCode.Text;

                string searchText = string.IsNullOrWhiteSpace(TsTxtSearchTview.Text)
                ? null
                : TsTxtSearchTview.Text.Trim().ToLower();

                var defaultRoleName = (string)null;
                string userRole = (TsBtnDrpUserRole == null ||
                    string.Equals(TsBtnDrpUserRole.Text, "(All)", StringComparison.Ordinal) ||
                    string.Equals(TsBtnDrpUserRole.Text, "User Role", StringComparison.Ordinal))
                   ? defaultRoleName
                   : TsBtnDrpUserRole.Text;

                await UserTreeBuilder.LoadUserTreeAsync(
                    tree: TreeViewMain,
                    unitCode: unitCode,
                    roleName: userRole,
                    searchText: searchText,
                    ct: default);

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

                // Load TreeView
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

        #region "Tree_AfterCheck_Propagate"
        /// <summary>
        /// Propagates check/uncheck to children and updates parent state.
        /// (Simplified: standard two-state propagation; extend to tri-state if needed.)
        /// </summary>
        private static void Tree_AfterCheck_Propagate(object sender, TreeViewEventArgs e)
        {
            var tree = sender as TreeView;
            if (tree == null) return;


            tree.AfterCheck -= Tree_AfterCheck_Propagate; // prevent recursion storms
            try
            {
                // Push state to children
                foreach (TreeNode n in e.Node.Nodes)
                {
                    n.Checked = e.Node.Checked;
                }


                // Pull state up to parents
                UpdateParentChecks(e.Node.Parent);
            }
            finally
            {
                tree.AfterCheck += Tree_AfterCheck_Propagate;
            }
        }
        #endregion

        #region "UpdateParentChecks"
        private static void UpdateParentChecks(TreeNode node)
        {
            if (node == null) return;
            bool anyChecked = false;
            bool allChecked = true;
            foreach (TreeNode child in node.Nodes)
            {
                if (child.Checked) anyChecked = true; else allChecked = false;
            }
            // Two-state: parent is checked only if all children checked
            node.Checked = (node.Nodes.Count > 0) ? allChecked : node.Checked;
            UpdateParentChecks(node.Parent);
        }
        #endregion

        #region "PerformTreeSearch"
        /// <summary>
        /// Simple incremental search: expands and selects the first node whose Text or ToolTip contains the query (case-insensitive).
        /// </summary>
        public static void PerformTreeSearch(TreeView tree, string query)
        {
            if (tree == null) return;
            if (string.IsNullOrWhiteSpace(query)) return;


            string q = query.Trim();
            TreeNode match = FindNode(tree.Nodes, q, StringComparison.CurrentCultureIgnoreCase);
            if (match != null)
            {
                ExpandTo(match);
                tree.SelectedNode = match;
                match.EnsureVisible();
            }
        }
        #endregion

        #region "FindNode"
        private static TreeNode FindNode(TreeNodeCollection nodes, string q, StringComparison cmp)
        {
            foreach (TreeNode n in nodes)
            {
                if ((n.Text != null && n.Text.IndexOf(q, cmp) >= 0) ||
                (n.ToolTipText != null && n.ToolTipText.IndexOf(q, cmp) >= 0))
                    return n;
                TreeNode hit = FindNode(n.Nodes, q, cmp);
                if (hit != null) return hit;
            }
            return null;
        }
        #endregion

        #region "ExpandTo"
        private static void ExpandTo(TreeNode node)
        {
            var p = node.Parent;
            while (p != null)
            {
                p.Expand();
                p = p.Parent;
            }
        }
        #endregion

        #region "TsBtRefreshDgv_Click"
        private async void TsBtRefreshDgv_Click(System.Object sender, System.EventArgs e)
        {
            // clear search box if coming from Clear button
            if (sender == TsBtnClearSearchDgv)
                TsTxtSearchDgv.Text = null;

            // get currently selected node from TreeView
            if (TreeViewMain.SelectedNode != null && TreeViewMain.SelectedNode.Tag != null)
            {
                string userRowGuid = TreeViewMain.SelectedNode.Tag.ToString();
                await LoadDataGridViewAsync(userRowGuid);
            }
            else
            {
                MessageBox.Show("Please select a user node in the TreeView.");
            }
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

            var miCheckAll = new ToolStripMenuItem("Check All Rows") { Name = "cmsCheckAll" };
            var miUncheckAll = new ToolStripMenuItem("Uncheck All Rows") { Name = "cmsUncheckAll" };
            var miCheckSelected = new ToolStripMenuItem("Check Selected Rows") { Name = "cmsCheckSelected" };
            var miUncheckSelected = new ToolStripMenuItem("Uncheck Selected Rows") { Name = "cmsUncheckSelected" };
            var miUpdatePerms = new ToolStripMenuItem("Update Permissions") { Name = "cmsUpdatePerms" };
            var miCancel = new ToolStripMenuItem("Cancel") { Name = "cmsCancel" };

            miCheckAll.Click += (s, e) => SetCanViewForAllRows(true);
            miUncheckAll.Click += (s, e) => SetCanViewForAllRows(false);
            miCheckSelected.Click += (s, e) => SetCanViewForSelectedRows(true);
            miUncheckSelected.Click += (s, e) => SetCanViewForSelectedRows(false);

            // In InitDgvListContextMenu
            miUpdatePerms.Click += MiUpdatePerms_Click;  // attach a handler

            miCancel.Click += (s, e) => { /* no-op */ };

            _cmsDgvList.Items.Add(miCheckAll);
            _cmsDgvList.Items.Add(miUncheckAll);
            _cmsDgvList.Items.Add(new ToolStripSeparator());
            _cmsDgvList.Items.Add(miCheckSelected);
            _cmsDgvList.Items.Add(miUncheckSelected);
            _cmsDgvList.Items.Add(new ToolStripSeparator());
            _cmsDgvList.Items.Add(miUpdatePerms);
            _cmsDgvList.Items.Add(new ToolStripSeparator());
            _cmsDgvList.Items.Add(miCancel);

            DgvList.ContextMenuStrip = _cmsDgvList;
        }
        #endregion

        #region "MiUpdatePerms_Click"
        /// <summary>
        /// Define the handler as async void
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void MiUpdatePerms_Click(object sender, EventArgs e)
        {
            await UpdatePermissionsFromContextAsync();
        }
        #endregion

        #region "EnsureCanViewColumnEditable"
        private bool EnsureCanViewColumnEditable()
        {
            if (DgvList.Columns[CanViewColumnName] == null)
            {
                MessageBox.Show($"Column '{CanViewColumnName}' not found.");
                return false;
            }

            // Make only HasAccess editable, everything else read-only
            foreach (DataGridViewColumn col in DgvList.Columns)
                col.ReadOnly = !string.Equals(col.Name, CanViewColumnName, StringComparison.OrdinalIgnoreCase);

            // If not a checkbox column visually, force it (optional—depends on your binding)
            if (!(DgvList.Columns[CanViewColumnName] is DataGridViewCheckBoxColumn))
            {
                // If you want to convert it visually, you can replace the column (optional)
                // Usually your DataSource with bool will auto-generate a checkbox.
            }
            return true;
        }
        #endregion

        #region "SetCanViewForAllRows"
        private void SetCanViewForAllRows(bool value)
        {
            if (!EnsureCanViewColumnEditable()) return;
            if (DgvList.Rows.Count == 0) return;

            DgvList.EndEdit();
            foreach (DataGridViewRow row in DgvList.Rows)
            {
                if (!row.IsNewRow)
                    row.Cells[CanViewColumnName].Value = value;
            }
            DgvList.EndEdit();
        }
        #endregion

        #region "SetCanViewForSelectedRows"
        private void SetCanViewForSelectedRows(bool value)
        {
            if (!EnsureCanViewColumnEditable()) return;
            if (DgvList.Rows.Count == 0) return;

            var indices = GetSelectedRowIndices();
            if (indices.Count == 0)
            {
                MessageBox.Show("Please select one or more rows (or cells) first.");
                return;
            }

            DgvList.EndEdit();
            for (int i = 0; i < indices.Count; i++)
            {
                int r = indices[i];
                if (r >= 0 && r < DgvList.Rows.Count && !DgvList.Rows[r].IsNewRow)
                    DgvList.Rows[r].Cells[CanViewColumnName].Value = value;
            }
            DgvList.EndEdit();
        }
        #endregion

        #region "GetSelectedRowIndices"
        private List<int> GetSelectedRowIndices()
        {
            var set = new System.Collections.Generic.HashSet<int>();

            // If FullRowSelect, SelectedRows is enough
            if (DgvList.SelectedRows != null && DgvList.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow r in DgvList.SelectedRows)
                    if (!r.IsNewRow) set.Add(r.Index);
            }

            // If CellSelect or mixed, also consider selected cells
            if (DgvList.SelectedCells != null && DgvList.SelectedCells.Count > 0)
            {
                foreach (DataGridViewCell c in DgvList.SelectedCells)
                    if (c.RowIndex >= 0) set.Add(c.RowIndex);
            }

            return new List<int>(set);
        }
        #endregion

        #region "LoadDataGridViewAsync"
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task LoadDataGridViewAsync(string userRowGuid)
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
                TsLblInputFooter.Text = "Fetching Rows. Please wait...";
                TsLblInputFooter.ForeColor = Color.DarkBlue;
                Application.DoEvents();

                // Prepare DataGridView
                DgvList.Invoke((MethodInvoker)(() =>
                {
                    DgvList.DataSource = null;
                    DgvList.Rows.Clear();
                    DgvList.Columns.Clear();
                    DgvList.Refresh();
                    DgvList.Visible = false;
                    DgvList.ContextMenuStrip = _cmsDgvList;
                }));

                string defaultUnitCode;
                if (TsBtnDrpUnitCode == null || string.IsNullOrWhiteSpace(TsBtnDrpUnitCode.Text) ||
                    string.Equals(TsBtnDrpUnitCode.Text, "(All)", StringComparison.Ordinal) ||
                    string.Equals(TsBtnDrpUnitCode.Text, "Plant Code", StringComparison.Ordinal))
                {
                    defaultUnitCode = ClassGlobalVariables.pubUnitCode;
                }
                else
                {
                    defaultUnitCode = TsBtnDrpUnitCode.Text;
                }

                string defaultUserRole = null;
                if (TsBtnDrpUserRole == null || string.IsNullOrWhiteSpace(TsBtnDrpUserRole.Text) ||
                    string.Equals(TsBtnDrpUserRole.Text, "(All)", StringComparison.Ordinal) ||
                    string.Equals(TsBtnDrpUserRole.Text, "User Role", StringComparison.Ordinal))
                {
                    defaultUserRole = null;
                }
                else
                {
                    defaultUserRole = TsBtnDrpUserRole.Text;
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
                    "UserName",
                    "RoleName",
                    "UnitName"};

                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@UserRowGuid", SqlDbType.NVarChar) { Value = userRowGuid });

                if (defaultUserRole != null)
                {
                    parameters.Add(new SqlParameter("@RoleName", SqlDbType.VarChar) { Value = defaultUserRole });
                }

                // Determine the row limit based on TsCmbLimitRows input:
                // If the text is "(all)", set limit to 0
                // Otherwise, try parsing to int; if fails, default to 50
                // ?. ensures Text is not null before calling Trim()(null - conditional operator).
                int mLimitRows =
                    TsCmbLimitRows.Text?.Trim().ToLower() == "(all)" ? 0 :
                    int.TryParse(TsCmbLimitRows.Text, out var value) ? value : 50;

                string orderBy = "UnitCode";
                string mTableName = "v_AdminUserUnitAccess";

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
                    isDeletedValue: false, // isDeletedValue
                    isActiveColumn: "IsActive", // table column name for IsActive
                    isActiveValue: true, // isActiveValue
                    limitRows: mLimitRows);

                // Check for empty results
                if (resultTable == null || resultTable.Rows.Count == 0)
                {
                    TsLblInputFooter.Text = "No rows found for given parameters.";
                    return;
                }

                // Create a filtered DataTable with only the columns we want
                DataTable filteredTable = new DataTable();
                var columnsToInclude = new List<string> {
                    "UserRowGuid",
                    "UnitCode",
                    "UnitAliasName",
                    "UserName",
                    "RoleName",
                    "HasAccess"};

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
                    DgvList.ReadOnly = false;
                    DgvList.Refresh();
                    DgvList.Visible = false;
                }));

                // Set data source
                DgvList.Invoke((MethodInvoker)(() => DgvList.DataSource = filteredTable));

                // Define and apply custom headers
                var customHeaders = new Dictionary<string, string>
                    {
                        {"UserRowGuid", "UserRowGuid"},
                        {"UnitCode", "Plant"},
                        {"UnitAliasName", "Plant Name"},
                        {"UserName", "User Login"},
                        {"RoleName", "User Role"},
                        {"HasAccess", "Can View"}
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
                    DgvList.ReadOnly = false;
                    DgvList.RowHeadersWidth = 60;
                    DgvList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    DgvList.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;

                    // Make grid readonly except HasAccess
                    foreach (DataGridViewColumn col in DgvList.Columns)
                    {
                        col.ReadOnly = col.Name != "HasAccess";
                    }

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
                    DgvList.Columns["UserRowGuid"].Visible = false;
                    DgvList.Columns["UserRowGuid"].ReadOnly = true;

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
                TsLblInputFooter.Text = $"Fetch Rows [ {resultTable.Rows.Count} ] In: {elapsedTime:hh\\:mm\\:ss}";
                TsLblInputFooter.ForeColor = Color.DarkBlue;
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                TsLblInputFooter.Text = ex.Message;
                TsLblInputFooter.ForeColor = Color.Red;
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

        #region "TsBtnSave_Click"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TsBtnSave_Click(object sender, EventArgs e)
        {
            // Validate selection
            if (TreeViewMain.SelectedNode == null || TreeViewMain.SelectedNode.Tag == null)
            {
                MessageBox.Show("Please select a user node in TreeView.");
                return;
            }

            // userRowGuid is nvarchar, so just use it as string
            string userRowGuid = (string)TreeViewMain.SelectedNode.Tag;

            try
            {

                var message = new StringBuilder();
                message.AppendLine("This will MERGE rows in table dbo.AdminUserUnitAccess for:");
                message.AppendLine("• Each selected user");
                message.AppendLine("");
                message.AppendLine("The operation will:");
                message.AppendLine("✓ Grant permissions for selected users");
                message.AppendLine("✓ Remove permissions for selected user");
                message.AppendLine("✓ Delete for users that are orphaned, inactive, or Remove permissions");
                message.AppendLine("");

                var msgResponse = MessageBox.Show(message.ToString(), "Synchronize User Menu Permissions",
                                                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (msgResponse == DialogResult.No)
                    return;

                if (await SavePermissionsAsync(userRowGuid.ToString()))
                {
                    // Update was successful
                    MessageBox.Show("User menu permissions synchronized successfully.\n" +
                                   "Permissions updated for selected user.",
                                   "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to synchronize user permissions.",
                                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (SqlException ex)
            {
                if (!string.IsNullOrEmpty(ex.Message) &&
                    ex.Message.IndexOf("MenuAssigner", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    MessageBox.Show("User not found. Please run MenuAssigner.");
                }
                else
                {
                    MessageBox.Show("SQL error: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error: " + ex.Message);
            }
        }
        #endregion

        #region "UpdatePermissionsFromContextAsync"
        private async Task UpdatePermissionsFromContextAsync()
        {
            // Get currently selected userRowGuid from the TreeView
            if (TreeViewMain.SelectedNode == null || TreeViewMain.SelectedNode.Tag == null)
            {
                MessageBox.Show("Please select a user node in the TreeView.");
                return;
            }

            string userRowGuid = (string)TreeViewMain.SelectedNode.Tag;

            try
            {
                // Commit any pending edits before save
                DgvList.EndEdit();
                var cm = (CurrencyManager)BindingContext[DgvList.DataSource];
                if (cm != null) cm.EndCurrentEdit();

                await SavePermissionsAsync(userRowGuid.ToString());
            }
            catch (SqlException ex)
            {
                if (!string.IsNullOrEmpty(ex.Message) &&
                    ex.Message.IndexOf("MenuAssigner", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    MessageBox.Show("User not found. Please run MenuAssigner.");
                }
                else
                {
                    MessageBox.Show("SQL error: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error: " + ex.Message);
            }
        }
        #endregion

        #region "SavePermissionsAsync"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userRowGuid"></param>
        /// <returns></returns>
        private async Task<bool> SavePermissionsAsync(string userRowGuid)
        {
            // Make sure any checkbox edits are committed before we read the table
            DgvList.EndEdit(); // commits the current cell edit
            var cm = (CurrencyManager)BindingContext[DgvList.DataSource];
            if (cm != null) cm.EndCurrentEdit();

            var sourceTable = DgvList.DataSource as DataTable;
            if (sourceTable == null)
            {
                MessageBox.Show("No data to save.");
                return false;
            }

            // Build the TVP DataTable expected by dbo.UDT_AdminMenuUserPerms
            var tvpTable = new DataTable();
            tvpTable.Columns.Add("UserRowGuid", typeof(string));
            tvpTable.Columns.Add("UnitCode", typeof(string));
            tvpTable.Columns.Add("HasAccess", typeof(bool));

            foreach (DataRow row in sourceTable.Rows)
            {
                // Skip deleted rows (shouldn't happen for a read-mostly grid, but safe)
                if (row.RowState == DataRowState.Deleted) continue;

                // Read defensively with DBNull checks
                object guidObj = row["UserRowGuid"];
                object unitCodeObj = row["UnitCode"];
                object hasAccessObj = row["HasAccess"];

                if (guidObj == DBNull.Value || unitCodeObj == DBNull.Value)
                    continue; // invalid row; skip (or you can throw)

                // Handle Guid or string seamlessly
                string rowGuid = guidObj is Guid g ? g.ToString("D") : Convert.ToString(guidObj);
                if (!string.Equals(rowGuid, userRowGuid, StringComparison.OrdinalIgnoreCase))
                    continue;

                // UnitCode is a string in the TVP schema
                string unitCode = Convert.ToString(unitCodeObj);

                bool hasAccess = false;
                if (hasAccessObj != DBNull.Value)
                    hasAccess = Convert.ToBoolean(hasAccessObj);

                tvpTable.Rows.Add(rowGuid, unitCode, hasAccess);
            }

            string defaultUnitCode;
            if (TsBtnDrpUnitCode == null || string.IsNullOrWhiteSpace(TsBtnDrpUnitCode.Text) ||
                string.Equals(TsBtnDrpUnitCode.Text, "(All)", StringComparison.Ordinal) ||
                string.Equals(TsBtnDrpUnitCode.Text, "Plant Code", StringComparison.Ordinal))
            {
                defaultUnitCode = ClassGlobalVariables.pubUnitCode;
            }
            else
            {
                defaultUnitCode = TsBtnDrpUnitCode.Text;
            }

            string connectionString = ClassGlobalFunctions.GetConnectionStringByUnitCode(
                    defaultUnitCode ?? ClassGlobalVariables.pubUnitCode);

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("Connection string cannot be empty.");

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("dbo.usp_AdminUserUnitAccess_Upsert", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // @GrantedBy
                var pGrantedBy = new SqlParameter("@GrantedBy", SqlDbType.NVarChar) { Value = ClassGlobalVariables.pubLoginUserRowGuid };
                cmd.Parameters.Add(pGrantedBy);

                // @Perms TVP
                var pPerms = new SqlParameter("@TypeUserPlantAccess", SqlDbType.Structured)
                {
                    TypeName = "dbo.UDT_AdminUserUnitAccess",
                    Value = tvpTable
                };
                cmd.Parameters.Add(pPerms);

                // Add output parameter for success status
                var successParam = new SqlParameter("@Success", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(successParam);

                try
                {
                    await conn.OpenAsync();

                    // Execute the command
                    await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);

                    bool success = Convert.ToBoolean(successParam.Value);

                    return success;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("SQL error: " + ex.Message);
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unexpected error: " + ex.Message);
                    return false;
                }
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
                    userRowGuid: ClassGlobalVariables.pubLoginUserRowGuid,
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
                TsBtnDrpUnitCode.DropDownItems.Add("(All)").Tag = 0;

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


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fill Plant Code",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region "FillUserRolesAsync"
        /// <summary>
        /// Used verbatim string ($@) for cleaner SQL query formatting
        /// </summary>
        /// <returns></returns>
        private async Task FillUserRolesAsync(string unitCode = null)
        {
            try
            {
                const string tableName = "dbo.UserRoles";

                // Clear existing items
                TsBtnDrpUserRole.DropDownItems.Clear();
                TsBtnDrpUserRole.DropDownItems.Add("(All)").Tag = 0;

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
                        SELECT RoleName, Id
                        FROM {tableName} 
                        WHERE 1 = 1 
                        ORDER BY SortOrder";

                    using (var command = new SqlCommand(query, connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                string categoryName = reader["RoleName"] as string ?? "Unnamed RoleName";
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
                                    foreach (ToolStripItem it in TsBtnDrpUserRole.DropDownItems)
                                        if (it is ToolStripMenuItem mi) mi.Checked = false;

                                    menuItem.Checked = true;

                                    TsBtnDrpUserRole.Text = menuItem.Text;
                                    TsBtnDrpUserRole.Tag = categoryId;
                                };

                                TsBtnDrpUserRole.DropDownItems.Add(menuItem);
                            }
                        }
                    }
                }

                // Set Default Text
                TsBtnDrpUserRole.Text = "(All)";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fill User Roles",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
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





    }
}

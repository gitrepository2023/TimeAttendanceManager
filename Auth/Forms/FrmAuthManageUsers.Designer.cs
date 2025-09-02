
namespace TimeAttendanceManager.Auth.Forms
{
    partial class FrmAuthManageUsers
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAuthManageUsers));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.CmbUnitCode = new System.Windows.Forms.ComboBox();
            this.TxtUserLoginName = new System.Windows.Forms.TextBox();
            this.TxtUserFirstName = new System.Windows.Forms.TextBox();
            this.TxtUserLastName = new System.Windows.Forms.TextBox();
            this.CmbDept = new System.Windows.Forms.ComboBox();
            this.TxtUserEmail = new System.Windows.Forms.TextBox();
            this.TxtUserContact = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.ToolStripCaption = new System.Windows.Forms.ToolStrip();
            this.TsLblCaption = new System.Windows.Forms.ToolStripLabel();
            this.TsBtnClose = new FontAwesome.Sharp.IconToolStripButton();
            this.StripInputFooter = new System.Windows.Forms.StatusStrip();
            this.TsLblInputStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.TsProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.TsBtnSidePanel = new FontAwesome.Sharp.IconToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.TsBtnOptions = new FontAwesome.Sharp.IconDropDownButton();
            this.TsBtnOptTech = new System.Windows.Forms.ToolStripMenuItem();
            this.TsMenuViewLockedUsers = new System.Windows.Forms.ToolStripMenuItem();
            this.TsMenuViewDeletedUsers = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.TsBtnDrpUnitCode = new FontAwesome.Sharp.IconDropDownButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.TsBtnCloseForm = new FontAwesome.Sharp.IconToolStripButton();
            this.SplitMain = new System.Windows.Forms.SplitContainer();
            this.DgvListUsers = new System.Windows.Forms.DataGridView();
            this.StatusStrip1 = new System.Windows.Forms.StatusStrip();
            this.TsLblDgvListFooter = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStripShiftSchedule = new System.Windows.Forms.ToolStrip();
            this.TsLblShiftPat = new System.Windows.Forms.ToolStripLabel();
            this.ToolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.TsBtnDrpDgvCols = new FontAwesome.Sharp.IconDropDownButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.TsBtRefreshDgv = new FontAwesome.Sharp.IconToolStripButton();
            this.TsTxtSearchDgv = new System.Windows.Forms.ToolStripTextBox();
            this.TsBtnClearSearchDgv = new FontAwesome.Sharp.IconToolStripButton();
            this.TsBtnSearchDgv = new FontAwesome.Sharp.IconToolStripButton();
            this.SplitRight = new System.Windows.Forms.SplitContainer();
            this.TabCtrlMain = new System.Windows.Forms.TabControl();
            this.TbPgFormView = new System.Windows.Forms.TabPage();
            this.TableLayout1 = new System.Windows.Forms.TableLayoutPanel();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.CmbUserLocked = new System.Windows.Forms.ComboBox();
            this.CmbUserDeleted = new System.Windows.Forms.ComboBox();
            this.Label12 = new System.Windows.Forms.Label();
            this.CmbUserRole = new System.Windows.Forms.ComboBox();
            this.StatusStrip2 = new System.Windows.Forms.StatusStrip();
            this.TsLblInputFooter = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStrip5 = new System.Windows.Forms.ToolStrip();
            this.TsBtnAddNew = new FontAwesome.Sharp.IconToolStripButton();
            this.ToolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.TsBtnSave = new FontAwesome.Sharp.IconToolStripButton();
            this.ToolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.TxtRecordId = new System.Windows.Forms.ToolStripTextBox();
            this.TbPgSetPwd = new System.Windows.Forms.TabPage();
            this.TableLayout2 = new System.Windows.Forms.TableLayoutPanel();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.TxtEnterPwd = new System.Windows.Forms.TextBox();
            this.TxtConfirmPwd = new System.Windows.Forms.TextBox();
            this.StatusStrip3 = new System.Windows.Forms.StatusStrip();
            this.TsLblSetPwdFooter = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStrip2 = new System.Windows.Forms.ToolStrip();
            this.TsBtnUpdatePwd = new FontAwesome.Sharp.IconToolStripButton();
            this.ToolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.TbPgSetMstPwd = new System.Windows.Forms.TabPage();
            this.TableLayout3 = new System.Windows.Forms.TableLayoutPanel();
            this.Label13 = new System.Windows.Forms.Label();
            this.Label14 = new System.Windows.Forms.Label();
            this.TxtEnterMstPwd = new System.Windows.Forms.TextBox();
            this.TxtConfirmMstPwd = new System.Windows.Forms.TextBox();
            this.StatusStrip4 = new System.Windows.Forms.StatusStrip();
            this.TsLblFooterMstPwd = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStrip3 = new System.Windows.Forms.ToolStrip();
            this.TsBtnUpdateMstPwd = new FontAwesome.Sharp.IconToolStripButton();
            this.ToolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.TabRightPanel = new System.Windows.Forms.TabControl();
            this.tabPagePref = new System.Windows.Forms.TabPage();
            this.PgridOptions = new System.Windows.Forms.PropertyGrid();
            this.tabPageTips = new System.Windows.Forms.TabPage();
            this.txtHelp = new System.Windows.Forms.RichTextBox();
            this.ImageListTabCtrl = new System.Windows.Forms.ImageList(this.components);
            this.ToolStrip4 = new System.Windows.Forms.ToolStrip();
            this.IconToolStripButton2 = new FontAwesome.Sharp.IconToolStripButton();
            this.TsBtnFilterClose = new FontAwesome.Sharp.IconToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.ToolStripCaption.SuspendLayout();
            this.StripInputFooter.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitMain)).BeginInit();
            this.SplitMain.Panel1.SuspendLayout();
            this.SplitMain.Panel2.SuspendLayout();
            this.SplitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvListUsers)).BeginInit();
            this.StatusStrip1.SuspendLayout();
            this.ToolStripShiftSchedule.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitRight)).BeginInit();
            this.SplitRight.Panel1.SuspendLayout();
            this.SplitRight.Panel2.SuspendLayout();
            this.SplitRight.SuspendLayout();
            this.TabCtrlMain.SuspendLayout();
            this.TbPgFormView.SuspendLayout();
            this.TableLayout1.SuspendLayout();
            this.StatusStrip2.SuspendLayout();
            this.ToolStrip5.SuspendLayout();
            this.TbPgSetPwd.SuspendLayout();
            this.TableLayout2.SuspendLayout();
            this.StatusStrip3.SuspendLayout();
            this.ToolStrip2.SuspendLayout();
            this.TbPgSetMstPwd.SuspendLayout();
            this.TableLayout3.SuspendLayout();
            this.StatusStrip4.SuspendLayout();
            this.ToolStrip3.SuspendLayout();
            this.TabRightPanel.SuspendLayout();
            this.tabPagePref.SuspendLayout();
            this.tabPageTips.SuspendLayout();
            this.ToolStrip4.SuspendLayout();
            this.SuspendLayout();
            // 
            // CmbUnitCode
            // 
            this.CmbUnitCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CmbUnitCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CmbUnitCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbUnitCode.FormattingEnabled = true;
            this.CmbUnitCode.Location = new System.Drawing.Point(147, 33);
            this.CmbUnitCode.Name = "CmbUnitCode";
            this.CmbUnitCode.Size = new System.Drawing.Size(121, 25);
            this.CmbUnitCode.TabIndex = 0;
            this.toolTip1.SetToolTip(this.CmbUnitCode, "Select plant code");
            // 
            // TxtUserLoginName
            // 
            this.TxtUserLoginName.Location = new System.Drawing.Point(147, 63);
            this.TxtUserLoginName.Name = "TxtUserLoginName";
            this.TxtUserLoginName.Size = new System.Drawing.Size(100, 25);
            this.TxtUserLoginName.TabIndex = 1;
            this.toolTip1.SetToolTip(this.TxtUserLoginName, "Please enter unique user login name");
            // 
            // TxtUserFirstName
            // 
            this.TxtUserFirstName.Location = new System.Drawing.Point(147, 93);
            this.TxtUserFirstName.Name = "TxtUserFirstName";
            this.TxtUserFirstName.Size = new System.Drawing.Size(100, 25);
            this.TxtUserFirstName.TabIndex = 2;
            this.toolTip1.SetToolTip(this.TxtUserFirstName, "Enter your first name");
            // 
            // TxtUserLastName
            // 
            this.TxtUserLastName.Location = new System.Drawing.Point(147, 123);
            this.TxtUserLastName.Name = "TxtUserLastName";
            this.TxtUserLastName.Size = new System.Drawing.Size(100, 25);
            this.TxtUserLastName.TabIndex = 3;
            this.toolTip1.SetToolTip(this.TxtUserLastName, "Enter your last name");
            // 
            // CmbDept
            // 
            this.CmbDept.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CmbDept.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CmbDept.FormattingEnabled = true;
            this.CmbDept.Location = new System.Drawing.Point(147, 153);
            this.CmbDept.Name = "CmbDept";
            this.CmbDept.Size = new System.Drawing.Size(121, 25);
            this.CmbDept.TabIndex = 4;
            this.toolTip1.SetToolTip(this.CmbDept, "Select your department");
            // 
            // TxtUserEmail
            // 
            this.TxtUserEmail.Location = new System.Drawing.Point(147, 183);
            this.TxtUserEmail.Name = "TxtUserEmail";
            this.TxtUserEmail.Size = new System.Drawing.Size(100, 25);
            this.TxtUserEmail.TabIndex = 5;
            this.toolTip1.SetToolTip(this.TxtUserEmail, "Enter your valid email address");
            // 
            // TxtUserContact
            // 
            this.TxtUserContact.Location = new System.Drawing.Point(147, 213);
            this.TxtUserContact.Name = "TxtUserContact";
            this.TxtUserContact.Size = new System.Drawing.Size(100, 25);
            this.TxtUserContact.TabIndex = 6;
            this.toolTip1.SetToolTip(this.TxtUserContact, "Enter your mobile number in numeric format");
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // ToolStripCaption
            // 
            this.ToolStripCaption.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ToolStripCaption.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsLblCaption,
            this.TsBtnClose});
            this.ToolStripCaption.Location = new System.Drawing.Point(0, 0);
            this.ToolStripCaption.Name = "ToolStripCaption";
            this.ToolStripCaption.Size = new System.Drawing.Size(933, 31);
            this.ToolStripCaption.TabIndex = 0;
            this.ToolStripCaption.Text = "toolStrip1";
            // 
            // TsLblCaption
            // 
            this.TsLblCaption.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.TsLblCaption.ForeColor = System.Drawing.Color.Green;
            this.TsLblCaption.Image = ((System.Drawing.Image)(resources.GetObject("TsLblCaption.Image")));
            this.TsLblCaption.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TsLblCaption.Name = "TsLblCaption";
            this.TsLblCaption.Size = new System.Drawing.Size(161, 28);
            this.TsLblCaption.Text = "Manage Users";
            // 
            // TsBtnClose
            // 
            this.TsBtnClose.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.TsBtnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsBtnClose.IconChar = FontAwesome.Sharp.IconChar.Remove;
            this.TsBtnClose.IconColor = System.Drawing.Color.DarkGray;
            this.TsBtnClose.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.TsBtnClose.IconSize = 24;
            this.TsBtnClose.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TsBtnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsBtnClose.Name = "TsBtnClose";
            this.TsBtnClose.Size = new System.Drawing.Size(28, 28);
            this.TsBtnClose.Text = "Close Form";
            // 
            // StripInputFooter
            // 
            this.StripInputFooter.BackColor = System.Drawing.Color.LemonChiffon;
            this.StripInputFooter.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.StripInputFooter.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsLblInputStatus,
            this.TsProgressBar});
            this.StripInputFooter.Location = new System.Drawing.Point(0, 566);
            this.StripInputFooter.Name = "StripInputFooter";
            this.StripInputFooter.Size = new System.Drawing.Size(933, 22);
            this.StripInputFooter.TabIndex = 1;
            this.StripInputFooter.Text = "statusStrip1";
            // 
            // TsLblInputStatus
            // 
            this.TsLblInputStatus.ForeColor = System.Drawing.Color.DarkBlue;
            this.TsLblInputStatus.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TsLblInputStatus.Name = "TsLblInputStatus";
            this.TsLblInputStatus.Size = new System.Drawing.Size(918, 17);
            this.TsLblInputStatus.Spring = true;
            this.TsLblInputStatus.Text = "...";
            this.TsLblInputStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TsProgressBar
            // 
            this.TsProgressBar.Name = "TsProgressBar";
            this.TsProgressBar.Size = new System.Drawing.Size(100, 18);
            this.TsProgressBar.Visible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsBtnSidePanel,
            this.toolStripSeparator1,
            this.TsBtnOptions,
            this.toolStripSeparator2,
            this.TsBtnDrpUnitCode,
            this.toolStripSeparator3,
            this.TsBtnCloseForm});
            this.toolStrip1.Location = new System.Drawing.Point(0, 31);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(933, 31);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // TsBtnSidePanel
            // 
            this.TsBtnSidePanel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsBtnSidePanel.IconChar = FontAwesome.Sharp.IconChar.Columns;
            this.TsBtnSidePanel.IconColor = System.Drawing.Color.DarkBlue;
            this.TsBtnSidePanel.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.TsBtnSidePanel.IconSize = 24;
            this.TsBtnSidePanel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TsBtnSidePanel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsBtnSidePanel.Name = "TsBtnSidePanel";
            this.TsBtnSidePanel.Size = new System.Drawing.Size(28, 28);
            this.TsBtnSidePanel.Text = "Hide Panel";
            this.TsBtnSidePanel.ToolTipText = "Show / Hide Panel";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // TsBtnOptions
            // 
            this.TsBtnOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsBtnOptTech,
            this.TsMenuViewLockedUsers,
            this.TsMenuViewDeletedUsers});
            this.TsBtnOptions.IconChar = FontAwesome.Sharp.IconChar.List;
            this.TsBtnOptions.IconColor = System.Drawing.Color.DarkBlue;
            this.TsBtnOptions.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.TsBtnOptions.IconSize = 24;
            this.TsBtnOptions.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TsBtnOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsBtnOptions.Name = "TsBtnOptions";
            this.TsBtnOptions.Size = new System.Drawing.Size(86, 28);
            this.TsBtnOptions.Text = "Options";
            // 
            // TsBtnOptTech
            // 
            this.TsBtnOptTech.Name = "TsBtnOptTech";
            this.TsBtnOptTech.Size = new System.Drawing.Size(173, 22);
            this.TsBtnOptTech.Text = "Technical Details";
            // 
            // TsMenuViewLockedUsers
            // 
            this.TsMenuViewLockedUsers.Name = "TsMenuViewLockedUsers";
            this.TsMenuViewLockedUsers.Size = new System.Drawing.Size(173, 22);
            this.TsMenuViewLockedUsers.Text = "View Locked Users";
            // 
            // TsMenuViewDeletedUsers
            // 
            this.TsMenuViewDeletedUsers.Name = "TsMenuViewDeletedUsers";
            this.TsMenuViewDeletedUsers.Size = new System.Drawing.Size(173, 22);
            this.TsMenuViewDeletedUsers.Text = "View Deleted Users";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // TsBtnDrpUnitCode
            // 
            this.TsBtnDrpUnitCode.ForeColor = System.Drawing.Color.Blue;
            this.TsBtnDrpUnitCode.IconChar = FontAwesome.Sharp.IconChar.HomeUser;
            this.TsBtnDrpUnitCode.IconColor = System.Drawing.Color.DarkBlue;
            this.TsBtnDrpUnitCode.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.TsBtnDrpUnitCode.IconSize = 24;
            this.TsBtnDrpUnitCode.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TsBtnDrpUnitCode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsBtnDrpUnitCode.Name = "TsBtnDrpUnitCode";
            this.TsBtnDrpUnitCode.Size = new System.Drawing.Size(102, 28);
            this.TsBtnDrpUnitCode.Text = "Plant Code";
            this.TsBtnDrpUnitCode.ToolTipText = "Select Plant Code";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
            // 
            // TsBtnCloseForm
            // 
            this.TsBtnCloseForm.IconChar = FontAwesome.Sharp.IconChar.Remove;
            this.TsBtnCloseForm.IconColor = System.Drawing.Color.Black;
            this.TsBtnCloseForm.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.TsBtnCloseForm.IconSize = 24;
            this.TsBtnCloseForm.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TsBtnCloseForm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsBtnCloseForm.Name = "TsBtnCloseForm";
            this.TsBtnCloseForm.Size = new System.Drawing.Size(95, 28);
            this.TsBtnCloseForm.Text = "Close Form";
            this.TsBtnCloseForm.Click += new System.EventHandler(this.TsBtnCloseForm_Click);
            // 
            // SplitMain
            // 
            this.SplitMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SplitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitMain.Location = new System.Drawing.Point(0, 62);
            this.SplitMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SplitMain.Name = "SplitMain";
            // 
            // SplitMain.Panel1
            // 
            this.SplitMain.Panel1.Controls.Add(this.DgvListUsers);
            this.SplitMain.Panel1.Controls.Add(this.StatusStrip1);
            this.SplitMain.Panel1.Controls.Add(this.ToolStripShiftSchedule);
            // 
            // SplitMain.Panel2
            // 
            this.SplitMain.Panel2.Controls.Add(this.SplitRight);
            this.SplitMain.Size = new System.Drawing.Size(933, 504);
            this.SplitMain.SplitterDistance = 389;
            this.SplitMain.SplitterWidth = 5;
            this.SplitMain.TabIndex = 11;
            // 
            // DgvListUsers
            // 
            this.DgvListUsers.AllowUserToAddRows = false;
            this.DgvListUsers.AllowUserToDeleteRows = false;
            this.DgvListUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DgvListUsers.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.DgvListUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvListUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgvListUsers.Location = new System.Drawing.Point(0, 25);
            this.DgvListUsers.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DgvListUsers.Name = "DgvListUsers";
            this.DgvListUsers.ReadOnly = true;
            this.DgvListUsers.RowHeadersWidth = 51;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.DarkBlue;
            this.DgvListUsers.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DgvListUsers.Size = new System.Drawing.Size(387, 455);
            this.DgvListUsers.TabIndex = 27;
            // 
            // StatusStrip1
            // 
            this.StatusStrip1.BackColor = System.Drawing.Color.LemonChiffon;
            this.StatusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.StatusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsLblDgvListFooter});
            this.StatusStrip1.Location = new System.Drawing.Point(0, 480);
            this.StatusStrip1.Name = "StatusStrip1";
            this.StatusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.StatusStrip1.Size = new System.Drawing.Size(387, 22);
            this.StatusStrip1.TabIndex = 26;
            this.StatusStrip1.Text = "StatusStrip1";
            // 
            // TsLblDgvListFooter
            // 
            this.TsLblDgvListFooter.ForeColor = System.Drawing.Color.DarkBlue;
            this.TsLblDgvListFooter.Name = "TsLblDgvListFooter";
            this.TsLblDgvListFooter.Size = new System.Drawing.Size(16, 17);
            this.TsLblDgvListFooter.Text = "...";
            // 
            // ToolStripShiftSchedule
            // 
            this.ToolStripShiftSchedule.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ToolStripShiftSchedule.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsLblShiftPat,
            this.ToolStripSeparator12,
            this.TsBtnDrpDgvCols,
            this.toolStripSeparator4,
            this.TsBtRefreshDgv,
            this.TsTxtSearchDgv,
            this.TsBtnClearSearchDgv,
            this.TsBtnSearchDgv});
            this.ToolStripShiftSchedule.Location = new System.Drawing.Point(0, 0);
            this.ToolStripShiftSchedule.Name = "ToolStripShiftSchedule";
            this.ToolStripShiftSchedule.Size = new System.Drawing.Size(387, 25);
            this.ToolStripShiftSchedule.TabIndex = 25;
            this.ToolStripShiftSchedule.Text = "ToolStrip1";
            // 
            // TsLblShiftPat
            // 
            this.TsLblShiftPat.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TsLblShiftPat.ForeColor = System.Drawing.Color.DarkMagenta;
            this.TsLblShiftPat.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TsLblShiftPat.Name = "TsLblShiftPat";
            this.TsLblShiftPat.Size = new System.Drawing.Size(51, 22);
            this.TsLblShiftPat.Text = "User List";
            // 
            // ToolStripSeparator12
            // 
            this.ToolStripSeparator12.Name = "ToolStripSeparator12";
            this.ToolStripSeparator12.Size = new System.Drawing.Size(6, 25);
            // 
            // TsBtnDrpDgvCols
            // 
            this.TsBtnDrpDgvCols.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsBtnDrpDgvCols.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.TsBtnDrpDgvCols.IconColor = System.Drawing.Color.DarkSlateGray;
            this.TsBtnDrpDgvCols.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.TsBtnDrpDgvCols.IconSize = 18;
            this.TsBtnDrpDgvCols.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TsBtnDrpDgvCols.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsBtnDrpDgvCols.Name = "TsBtnDrpDgvCols";
            this.TsBtnDrpDgvCols.Size = new System.Drawing.Size(31, 22);
            this.TsBtnDrpDgvCols.Text = "Show / Hide Columns";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // TsBtRefreshDgv
            // 
            this.TsBtRefreshDgv.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsBtRefreshDgv.IconChar = FontAwesome.Sharp.IconChar.Sync;
            this.TsBtRefreshDgv.IconColor = System.Drawing.Color.RoyalBlue;
            this.TsBtRefreshDgv.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.TsBtRefreshDgv.IconSize = 18;
            this.TsBtRefreshDgv.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TsBtRefreshDgv.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsBtRefreshDgv.Name = "TsBtRefreshDgv";
            this.TsBtRefreshDgv.Size = new System.Drawing.Size(23, 22);
            this.TsBtRefreshDgv.Text = "Refresh";
            this.TsBtRefreshDgv.ToolTipText = "Refresh List";
            // 
            // TsTxtSearchDgv
            // 
            this.TsTxtSearchDgv.BackColor = System.Drawing.Color.LemonChiffon;
            this.TsTxtSearchDgv.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TsTxtSearchDgv.Name = "TsTxtSearchDgv";
            this.TsTxtSearchDgv.Size = new System.Drawing.Size(116, 25);
            this.TsTxtSearchDgv.ToolTipText = "Enter Search Text";
            // 
            // TsBtnClearSearchDgv
            // 
            this.TsBtnClearSearchDgv.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsBtnClearSearchDgv.ForeColor = System.Drawing.Color.DarkRed;
            this.TsBtnClearSearchDgv.IconChar = FontAwesome.Sharp.IconChar.Remove;
            this.TsBtnClearSearchDgv.IconColor = System.Drawing.Color.DarkRed;
            this.TsBtnClearSearchDgv.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.TsBtnClearSearchDgv.IconSize = 16;
            this.TsBtnClearSearchDgv.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TsBtnClearSearchDgv.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsBtnClearSearchDgv.Name = "TsBtnClearSearchDgv";
            this.TsBtnClearSearchDgv.Size = new System.Drawing.Size(23, 22);
            this.TsBtnClearSearchDgv.Text = "Clear";
            this.TsBtnClearSearchDgv.ToolTipText = "Clear search text";
            // 
            // TsBtnSearchDgv
            // 
            this.TsBtnSearchDgv.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsBtnSearchDgv.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.TsBtnSearchDgv.IconColor = System.Drawing.Color.Blue;
            this.TsBtnSearchDgv.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.TsBtnSearchDgv.IconSize = 16;
            this.TsBtnSearchDgv.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TsBtnSearchDgv.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsBtnSearchDgv.Name = "TsBtnSearchDgv";
            this.TsBtnSearchDgv.Size = new System.Drawing.Size(23, 22);
            this.TsBtnSearchDgv.Text = "Search";
            this.TsBtnSearchDgv.ToolTipText = "Search in DataGrid";
            // 
            // SplitRight
            // 
            this.SplitRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SplitRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitRight.Location = new System.Drawing.Point(0, 0);
            this.SplitRight.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SplitRight.Name = "SplitRight";
            // 
            // SplitRight.Panel1
            // 
            this.SplitRight.Panel1.Controls.Add(this.TabCtrlMain);
            // 
            // SplitRight.Panel2
            // 
            this.SplitRight.Panel2.Controls.Add(this.TabRightPanel);
            this.SplitRight.Panel2.Controls.Add(this.ToolStrip4);
            this.SplitRight.Size = new System.Drawing.Size(539, 504);
            this.SplitRight.SplitterDistance = 384;
            this.SplitRight.SplitterWidth = 5;
            this.SplitRight.TabIndex = 0;
            // 
            // TabCtrlMain
            // 
            this.TabCtrlMain.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.TabCtrlMain.Controls.Add(this.TbPgFormView);
            this.TabCtrlMain.Controls.Add(this.TbPgSetPwd);
            this.TabCtrlMain.Controls.Add(this.TbPgSetMstPwd);
            this.TabCtrlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabCtrlMain.Location = new System.Drawing.Point(0, 0);
            this.TabCtrlMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TabCtrlMain.Name = "TabCtrlMain";
            this.TabCtrlMain.SelectedIndex = 0;
            this.TabCtrlMain.Size = new System.Drawing.Size(382, 502);
            this.TabCtrlMain.TabIndex = 0;
            // 
            // TbPgFormView
            // 
            this.TbPgFormView.Controls.Add(this.TableLayout1);
            this.TbPgFormView.Controls.Add(this.StatusStrip2);
            this.TbPgFormView.Controls.Add(this.ToolStrip5);
            this.TbPgFormView.Location = new System.Drawing.Point(4, 29);
            this.TbPgFormView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TbPgFormView.Name = "TbPgFormView";
            this.TbPgFormView.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TbPgFormView.Size = new System.Drawing.Size(374, 469);
            this.TbPgFormView.TabIndex = 0;
            this.TbPgFormView.Text = "Form View";
            this.TbPgFormView.UseVisualStyleBackColor = true;
            // 
            // TableLayout1
            // 
            this.TableLayout1.AutoScroll = true;
            this.TableLayout1.ColumnCount = 3;
            this.TableLayout1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.TableLayout1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.TableLayout1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.TableLayout1.Controls.Add(this.Label1, 0, 1);
            this.TableLayout1.Controls.Add(this.Label2, 0, 2);
            this.TableLayout1.Controls.Add(this.Label3, 0, 3);
            this.TableLayout1.Controls.Add(this.Label4, 0, 4);
            this.TableLayout1.Controls.Add(this.Label5, 0, 5);
            this.TableLayout1.Controls.Add(this.Label6, 0, 6);
            this.TableLayout1.Controls.Add(this.Label7, 0, 7);
            this.TableLayout1.Controls.Add(this.CmbUnitCode, 1, 1);
            this.TableLayout1.Controls.Add(this.TxtUserLoginName, 1, 2);
            this.TableLayout1.Controls.Add(this.TxtUserFirstName, 1, 3);
            this.TableLayout1.Controls.Add(this.TxtUserLastName, 1, 4);
            this.TableLayout1.Controls.Add(this.CmbDept, 1, 5);
            this.TableLayout1.Controls.Add(this.TxtUserEmail, 1, 6);
            this.TableLayout1.Controls.Add(this.TxtUserContact, 1, 7);
            this.TableLayout1.Controls.Add(this.Label10, 0, 9);
            this.TableLayout1.Controls.Add(this.Label11, 0, 10);
            this.TableLayout1.Controls.Add(this.CmbUserLocked, 1, 9);
            this.TableLayout1.Controls.Add(this.CmbUserDeleted, 1, 10);
            this.TableLayout1.Controls.Add(this.Label12, 0, 8);
            this.TableLayout1.Controls.Add(this.CmbUserRole, 1, 8);
            this.TableLayout1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableLayout1.ForeColor = System.Drawing.Color.DarkMagenta;
            this.TableLayout1.Location = new System.Drawing.Point(3, 31);
            this.TableLayout1.Name = "TableLayout1";
            this.TableLayout1.RowCount = 13;
            this.TableLayout1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayout1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayout1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayout1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayout1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayout1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayout1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayout1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayout1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayout1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayout1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayout1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayout1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TableLayout1.Size = new System.Drawing.Size(368, 412);
            this.TableLayout1.TabIndex = 5;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(3, 30);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(74, 17);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Plant Code:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(3, 60);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(113, 17);
            this.Label2.TabIndex = 1;
            this.Label2.Text = "User Login Name:";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(3, 90);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(105, 17);
            this.Label3.TabIndex = 2;
            this.Label3.Text = "User First Name:";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(3, 120);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(104, 17);
            this.Label4.TabIndex = 3;
            this.Label4.Text = "User Last Name:";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(3, 150);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(111, 17);
            this.Label5.TabIndex = 4;
            this.Label5.Text = "User Department:";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(3, 180);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(73, 17);
            this.Label6.TabIndex = 5;
            this.Label6.Text = "User Email:";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(3, 210);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(138, 17);
            this.Label7.TabIndex = 6;
            this.Label7.Text = "User Contact Number:";
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(3, 270);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(96, 17);
            this.Label10.TabIndex = 14;
            this.Label10.Text = "Is User Locked:";
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(3, 300);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(100, 17);
            this.Label11.TabIndex = 15;
            this.Label11.Text = "Is User Deleted:";
            // 
            // CmbUserLocked
            // 
            this.CmbUserLocked.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbUserLocked.FormattingEnabled = true;
            this.CmbUserLocked.Location = new System.Drawing.Point(147, 273);
            this.CmbUserLocked.Name = "CmbUserLocked";
            this.CmbUserLocked.Size = new System.Drawing.Size(121, 25);
            this.CmbUserLocked.TabIndex = 8;
            // 
            // CmbUserDeleted
            // 
            this.CmbUserDeleted.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbUserDeleted.FormattingEnabled = true;
            this.CmbUserDeleted.Location = new System.Drawing.Point(147, 303);
            this.CmbUserDeleted.Name = "CmbUserDeleted";
            this.CmbUserDeleted.Size = new System.Drawing.Size(121, 25);
            this.CmbUserDeleted.TabIndex = 9;
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Location = new System.Drawing.Point(3, 240);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(68, 17);
            this.Label12.TabIndex = 18;
            this.Label12.Text = "User Role:";
            // 
            // CmbUserRole
            // 
            this.CmbUserRole.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CmbUserRole.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CmbUserRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbUserRole.FormattingEnabled = true;
            this.CmbUserRole.Location = new System.Drawing.Point(147, 243);
            this.CmbUserRole.Name = "CmbUserRole";
            this.CmbUserRole.Size = new System.Drawing.Size(121, 25);
            this.CmbUserRole.TabIndex = 7;
            // 
            // StatusStrip2
            // 
            this.StatusStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.StatusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsLblInputFooter});
            this.StatusStrip2.Location = new System.Drawing.Point(3, 443);
            this.StatusStrip2.Name = "StatusStrip2";
            this.StatusStrip2.Size = new System.Drawing.Size(368, 22);
            this.StatusStrip2.TabIndex = 4;
            this.StatusStrip2.Text = "StatusStrip2";
            // 
            // TsLblInputFooter
            // 
            this.TsLblInputFooter.ForeColor = System.Drawing.Color.DarkBlue;
            this.TsLblInputFooter.Name = "TsLblInputFooter";
            this.TsLblInputFooter.Size = new System.Drawing.Size(16, 17);
            this.TsLblInputFooter.Text = "...";
            // 
            // ToolStrip5
            // 
            this.ToolStrip5.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ToolStrip5.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsBtnAddNew,
            this.ToolStripSeparator5,
            this.TsBtnSave,
            this.ToolStripSeparator6,
            this.TxtRecordId});
            this.ToolStrip5.Location = new System.Drawing.Point(3, 4);
            this.ToolStrip5.Name = "ToolStrip5";
            this.ToolStrip5.Size = new System.Drawing.Size(368, 27);
            this.ToolStrip5.TabIndex = 3;
            this.ToolStrip5.Text = "ToolStrip5";
            // 
            // TsBtnAddNew
            // 
            this.TsBtnAddNew.ForeColor = System.Drawing.Color.Blue;
            this.TsBtnAddNew.IconChar = FontAwesome.Sharp.IconChar.UserEdit;
            this.TsBtnAddNew.IconColor = System.Drawing.Color.RoyalBlue;
            this.TsBtnAddNew.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.TsBtnAddNew.IconSize = 20;
            this.TsBtnAddNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TsBtnAddNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsBtnAddNew.Name = "TsBtnAddNew";
            this.TsBtnAddNew.Size = new System.Drawing.Size(80, 24);
            this.TsBtnAddNew.Text = "Add New";
            // 
            // ToolStripSeparator5
            // 
            this.ToolStripSeparator5.Name = "ToolStripSeparator5";
            this.ToolStripSeparator5.Size = new System.Drawing.Size(6, 27);
            // 
            // TsBtnSave
            // 
            this.TsBtnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.TsBtnSave.ForeColor = System.Drawing.Color.DarkGreen;
            this.TsBtnSave.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            this.TsBtnSave.IconColor = System.Drawing.Color.Green;
            this.TsBtnSave.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.TsBtnSave.IconSize = 20;
            this.TsBtnSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TsBtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsBtnSave.Name = "TsBtnSave";
            this.TsBtnSave.Size = new System.Drawing.Size(58, 24);
            this.TsBtnSave.Text = "Save";
            // 
            // ToolStripSeparator6
            // 
            this.ToolStripSeparator6.Name = "ToolStripSeparator6";
            this.ToolStripSeparator6.Size = new System.Drawing.Size(6, 27);
            // 
            // TxtRecordId
            // 
            this.TxtRecordId.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.TxtRecordId.Enabled = false;
            this.TxtRecordId.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TxtRecordId.Name = "TxtRecordId";
            this.TxtRecordId.ReadOnly = true;
            this.TxtRecordId.Size = new System.Drawing.Size(100, 27);
            this.TxtRecordId.Visible = false;
            // 
            // TbPgSetPwd
            // 
            this.TbPgSetPwd.Controls.Add(this.TableLayout2);
            this.TbPgSetPwd.Controls.Add(this.StatusStrip3);
            this.TbPgSetPwd.Controls.Add(this.ToolStrip2);
            this.TbPgSetPwd.Location = new System.Drawing.Point(4, 25);
            this.TbPgSetPwd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TbPgSetPwd.Name = "TbPgSetPwd";
            this.TbPgSetPwd.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TbPgSetPwd.Size = new System.Drawing.Size(372, 471);
            this.TbPgSetPwd.TabIndex = 1;
            this.TbPgSetPwd.Text = "Set Password";
            this.TbPgSetPwd.UseVisualStyleBackColor = true;
            // 
            // TableLayout2
            // 
            this.TableLayout2.AutoScroll = true;
            this.TableLayout2.ColumnCount = 3;
            this.TableLayout2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.TableLayout2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.TableLayout2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.TableLayout2.Controls.Add(this.Label8, 0, 1);
            this.TableLayout2.Controls.Add(this.Label9, 0, 2);
            this.TableLayout2.Controls.Add(this.TxtEnterPwd, 1, 1);
            this.TableLayout2.Controls.Add(this.TxtConfirmPwd, 1, 2);
            this.TableLayout2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableLayout2.ForeColor = System.Drawing.Color.DarkMagenta;
            this.TableLayout2.Location = new System.Drawing.Point(3, 31);
            this.TableLayout2.Name = "TableLayout2";
            this.TableLayout2.RowCount = 7;
            this.TableLayout2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayout2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayout2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayout2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayout2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayout2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayout2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TableLayout2.Size = new System.Drawing.Size(366, 414);
            this.TableLayout2.TabIndex = 6;
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(3, 30);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(101, 17);
            this.Label8.TabIndex = 0;
            this.Label8.Text = "Enter Password:";
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(3, 60);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(117, 17);
            this.Label9.TabIndex = 1;
            this.Label9.Text = "Confirm Password:";
            // 
            // TxtEnterPwd
            // 
            this.TxtEnterPwd.Location = new System.Drawing.Point(126, 33);
            this.TxtEnterPwd.Name = "TxtEnterPwd";
            this.TxtEnterPwd.PasswordChar = 'X';
            this.TxtEnterPwd.Size = new System.Drawing.Size(100, 25);
            this.TxtEnterPwd.TabIndex = 2;
            // 
            // TxtConfirmPwd
            // 
            this.TxtConfirmPwd.Location = new System.Drawing.Point(126, 63);
            this.TxtConfirmPwd.Name = "TxtConfirmPwd";
            this.TxtConfirmPwd.Size = new System.Drawing.Size(100, 25);
            this.TxtConfirmPwd.TabIndex = 3;
            // 
            // StatusStrip3
            // 
            this.StatusStrip3.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.StatusStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsLblSetPwdFooter});
            this.StatusStrip3.Location = new System.Drawing.Point(3, 445);
            this.StatusStrip3.Name = "StatusStrip3";
            this.StatusStrip3.Size = new System.Drawing.Size(366, 22);
            this.StatusStrip3.TabIndex = 5;
            this.StatusStrip3.Text = "StatusStrip3";
            // 
            // TsLblSetPwdFooter
            // 
            this.TsLblSetPwdFooter.ForeColor = System.Drawing.Color.DarkBlue;
            this.TsLblSetPwdFooter.Name = "TsLblSetPwdFooter";
            this.TsLblSetPwdFooter.Size = new System.Drawing.Size(16, 17);
            this.TsLblSetPwdFooter.Text = "...";
            // 
            // ToolStrip2
            // 
            this.ToolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ToolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsBtnUpdatePwd,
            this.ToolStripSeparator8});
            this.ToolStrip2.Location = new System.Drawing.Point(3, 4);
            this.ToolStrip2.Name = "ToolStrip2";
            this.ToolStrip2.Size = new System.Drawing.Size(366, 27);
            this.ToolStrip2.TabIndex = 4;
            this.ToolStrip2.Text = "ToolStrip2";
            // 
            // TsBtnUpdatePwd
            // 
            this.TsBtnUpdatePwd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.TsBtnUpdatePwd.ForeColor = System.Drawing.Color.Brown;
            this.TsBtnUpdatePwd.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            this.TsBtnUpdatePwd.IconColor = System.Drawing.Color.Indigo;
            this.TsBtnUpdatePwd.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.TsBtnUpdatePwd.IconSize = 20;
            this.TsBtnUpdatePwd.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TsBtnUpdatePwd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsBtnUpdatePwd.Name = "TsBtnUpdatePwd";
            this.TsBtnUpdatePwd.Size = new System.Drawing.Size(156, 24);
            this.TsBtnUpdatePwd.Text = "Set Password and save";
            this.TsBtnUpdatePwd.ToolTipText = "Set Password and save\r\nThis will re-set the old password with new.";
            // 
            // ToolStripSeparator8
            // 
            this.ToolStripSeparator8.Name = "ToolStripSeparator8";
            this.ToolStripSeparator8.Size = new System.Drawing.Size(6, 27);
            // 
            // TbPgSetMstPwd
            // 
            this.TbPgSetMstPwd.Controls.Add(this.TableLayout3);
            this.TbPgSetMstPwd.Controls.Add(this.StatusStrip4);
            this.TbPgSetMstPwd.Controls.Add(this.ToolStrip3);
            this.TbPgSetMstPwd.Location = new System.Drawing.Point(4, 25);
            this.TbPgSetMstPwd.Name = "TbPgSetMstPwd";
            this.TbPgSetMstPwd.Size = new System.Drawing.Size(372, 471);
            this.TbPgSetMstPwd.TabIndex = 2;
            this.TbPgSetMstPwd.Text = "Set Master Password";
            this.TbPgSetMstPwd.UseVisualStyleBackColor = true;
            // 
            // TableLayout3
            // 
            this.TableLayout3.AutoScroll = true;
            this.TableLayout3.ColumnCount = 3;
            this.TableLayout3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.TableLayout3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.TableLayout3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.TableLayout3.Controls.Add(this.Label13, 0, 1);
            this.TableLayout3.Controls.Add(this.Label14, 0, 2);
            this.TableLayout3.Controls.Add(this.TxtEnterMstPwd, 1, 1);
            this.TableLayout3.Controls.Add(this.TxtConfirmMstPwd, 1, 2);
            this.TableLayout3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableLayout3.ForeColor = System.Drawing.Color.DarkMagenta;
            this.TableLayout3.Location = new System.Drawing.Point(0, 27);
            this.TableLayout3.Name = "TableLayout3";
            this.TableLayout3.RowCount = 7;
            this.TableLayout3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayout3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayout3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayout3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayout3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayout3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayout3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TableLayout3.Size = new System.Drawing.Size(372, 422);
            this.TableLayout3.TabIndex = 7;
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(3, 30);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(146, 17);
            this.Label13.TabIndex = 0;
            this.Label13.Text = "Enter Master Password:";
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.Location = new System.Drawing.Point(3, 60);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(162, 17);
            this.Label14.TabIndex = 1;
            this.Label14.Text = "Confirm Master Password:";
            // 
            // TxtEnterMstPwd
            // 
            this.TxtEnterMstPwd.Location = new System.Drawing.Point(171, 33);
            this.TxtEnterMstPwd.Name = "TxtEnterMstPwd";
            this.TxtEnterMstPwd.PasswordChar = 'X';
            this.TxtEnterMstPwd.Size = new System.Drawing.Size(100, 25);
            this.TxtEnterMstPwd.TabIndex = 2;
            // 
            // TxtConfirmMstPwd
            // 
            this.TxtConfirmMstPwd.Location = new System.Drawing.Point(171, 63);
            this.TxtConfirmMstPwd.Name = "TxtConfirmMstPwd";
            this.TxtConfirmMstPwd.Size = new System.Drawing.Size(100, 25);
            this.TxtConfirmMstPwd.TabIndex = 3;
            // 
            // StatusStrip4
            // 
            this.StatusStrip4.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.StatusStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsLblFooterMstPwd});
            this.StatusStrip4.Location = new System.Drawing.Point(0, 449);
            this.StatusStrip4.Name = "StatusStrip4";
            this.StatusStrip4.Size = new System.Drawing.Size(372, 22);
            this.StatusStrip4.TabIndex = 6;
            this.StatusStrip4.Text = "StatusStrip4";
            // 
            // TsLblFooterMstPwd
            // 
            this.TsLblFooterMstPwd.ForeColor = System.Drawing.Color.DarkBlue;
            this.TsLblFooterMstPwd.Name = "TsLblFooterMstPwd";
            this.TsLblFooterMstPwd.Size = new System.Drawing.Size(16, 17);
            this.TsLblFooterMstPwd.Text = "...";
            // 
            // ToolStrip3
            // 
            this.ToolStrip3.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ToolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsBtnUpdateMstPwd,
            this.ToolStripSeparator9});
            this.ToolStrip3.Location = new System.Drawing.Point(0, 0);
            this.ToolStrip3.Name = "ToolStrip3";
            this.ToolStrip3.Size = new System.Drawing.Size(372, 27);
            this.ToolStrip3.TabIndex = 5;
            this.ToolStrip3.Text = "ToolStrip3";
            // 
            // TsBtnUpdateMstPwd
            // 
            this.TsBtnUpdateMstPwd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.TsBtnUpdateMstPwd.ForeColor = System.Drawing.Color.Brown;
            this.TsBtnUpdateMstPwd.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            this.TsBtnUpdateMstPwd.IconColor = System.Drawing.Color.Indigo;
            this.TsBtnUpdateMstPwd.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.TsBtnUpdateMstPwd.IconSize = 20;
            this.TsBtnUpdateMstPwd.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TsBtnUpdateMstPwd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsBtnUpdateMstPwd.Name = "TsBtnUpdateMstPwd";
            this.TsBtnUpdateMstPwd.Size = new System.Drawing.Size(198, 24);
            this.TsBtnUpdateMstPwd.Text = "Set Master Password and save";
            this.TsBtnUpdateMstPwd.ToolTipText = "Set Password and save\r\nThis will re-set the old password with new.";
            // 
            // ToolStripSeparator9
            // 
            this.ToolStripSeparator9.Name = "ToolStripSeparator9";
            this.ToolStripSeparator9.Size = new System.Drawing.Size(6, 27);
            // 
            // TabRightPanel
            // 
            this.TabRightPanel.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.TabRightPanel.Controls.Add(this.tabPagePref);
            this.TabRightPanel.Controls.Add(this.tabPageTips);
            this.TabRightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabRightPanel.ImageList = this.ImageListTabCtrl;
            this.TabRightPanel.Location = new System.Drawing.Point(0, 25);
            this.TabRightPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TabRightPanel.Name = "TabRightPanel";
            this.TabRightPanel.SelectedIndex = 0;
            this.TabRightPanel.Size = new System.Drawing.Size(148, 477);
            this.TabRightPanel.TabIndex = 37;
            // 
            // tabPagePref
            // 
            this.tabPagePref.Controls.Add(this.PgridOptions);
            this.tabPagePref.ImageKey = "notebook16x16.png";
            this.tabPagePref.Location = new System.Drawing.Point(4, 4);
            this.tabPagePref.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPagePref.Name = "tabPagePref";
            this.tabPagePref.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPagePref.Size = new System.Drawing.Size(140, 447);
            this.tabPagePref.TabIndex = 0;
            this.tabPagePref.Text = "Preferences";
            this.tabPagePref.UseVisualStyleBackColor = true;
            // 
            // PgridOptions
            // 
            this.PgridOptions.BackColor = System.Drawing.Color.AliceBlue;
            this.PgridOptions.CategoryForeColor = System.Drawing.Color.DarkBlue;
            this.PgridOptions.CommandsForeColor = System.Drawing.Color.Indigo;
            this.PgridOptions.DisabledItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(75)))), ((int)(((byte)(0)))), ((int)(((byte)(130)))));
            this.PgridOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PgridOptions.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PgridOptions.HelpBackColor = System.Drawing.Color.AliceBlue;
            this.PgridOptions.HelpForeColor = System.Drawing.Color.RoyalBlue;
            this.PgridOptions.LineColor = System.Drawing.Color.AliceBlue;
            this.PgridOptions.Location = new System.Drawing.Point(3, 4);
            this.PgridOptions.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.PgridOptions.Name = "PgridOptions";
            this.PgridOptions.Size = new System.Drawing.Size(134, 439);
            this.PgridOptions.TabIndex = 29;
            this.PgridOptions.ViewForeColor = System.Drawing.Color.Indigo;
            // 
            // tabPageTips
            // 
            this.tabPageTips.Controls.Add(this.txtHelp);
            this.tabPageTips.ImageKey = "note_text16x16.png";
            this.tabPageTips.Location = new System.Drawing.Point(4, 4);
            this.tabPageTips.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPageTips.Name = "tabPageTips";
            this.tabPageTips.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPageTips.Size = new System.Drawing.Size(140, 448);
            this.tabPageTips.TabIndex = 1;
            this.tabPageTips.Text = "Tips";
            this.tabPageTips.UseVisualStyleBackColor = true;
            // 
            // txtHelp
            // 
            this.txtHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtHelp.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHelp.Location = new System.Drawing.Point(3, 4);
            this.txtHelp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtHelp.Name = "txtHelp";
            this.txtHelp.Size = new System.Drawing.Size(134, 440);
            this.txtHelp.TabIndex = 1;
            this.txtHelp.Text = "";
            // 
            // ImageListTabCtrl
            // 
            this.ImageListTabCtrl.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageListTabCtrl.ImageStream")));
            this.ImageListTabCtrl.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageListTabCtrl.Images.SetKeyName(0, "pinned.ico");
            this.ImageListTabCtrl.Images.SetKeyName(1, "unpinned.ico");
            this.ImageListTabCtrl.Images.SetKeyName(2, "note_text16x16.png");
            this.ImageListTabCtrl.Images.SetKeyName(3, "notebook16x16.png");
            // 
            // ToolStrip4
            // 
            this.ToolStrip4.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ToolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.IconToolStripButton2,
            this.TsBtnFilterClose});
            this.ToolStrip4.Location = new System.Drawing.Point(0, 0);
            this.ToolStrip4.Name = "ToolStrip4";
            this.ToolStrip4.Size = new System.Drawing.Size(148, 25);
            this.ToolStrip4.TabIndex = 3;
            this.ToolStrip4.Text = "ToolStrip4";
            // 
            // IconToolStripButton2
            // 
            this.IconToolStripButton2.IconChar = FontAwesome.Sharp.IconChar.DiceSix;
            this.IconToolStripButton2.IconColor = System.Drawing.Color.Blue;
            this.IconToolStripButton2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconToolStripButton2.IconSize = 16;
            this.IconToolStripButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.IconToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.IconToolStripButton2.Name = "IconToolStripButton2";
            this.IconToolStripButton2.Size = new System.Drawing.Size(69, 22);
            this.IconToolStripButton2.Text = "Options";
            // 
            // TsBtnFilterClose
            // 
            this.TsBtnFilterClose.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.TsBtnFilterClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsBtnFilterClose.IconChar = FontAwesome.Sharp.IconChar.Remove;
            this.TsBtnFilterClose.IconColor = System.Drawing.Color.DarkRed;
            this.TsBtnFilterClose.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.TsBtnFilterClose.IconSize = 16;
            this.TsBtnFilterClose.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TsBtnFilterClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsBtnFilterClose.Name = "TsBtnFilterClose";
            this.TsBtnFilterClose.Size = new System.Drawing.Size(23, 22);
            this.TsBtnFilterClose.Text = "Close";
            // 
            // FrmAuthManageUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(933, 588);
            this.Controls.Add(this.SplitMain);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.StripInputFooter);
            this.Controls.Add(this.ToolStripCaption);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmAuthManageUsers";
            this.ShowIcon = false;
            this.Text = "Manage Users";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ToolStripCaption.ResumeLayout(false);
            this.ToolStripCaption.PerformLayout();
            this.StripInputFooter.ResumeLayout(false);
            this.StripInputFooter.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.SplitMain.Panel1.ResumeLayout(false);
            this.SplitMain.Panel1.PerformLayout();
            this.SplitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitMain)).EndInit();
            this.SplitMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgvListUsers)).EndInit();
            this.StatusStrip1.ResumeLayout(false);
            this.StatusStrip1.PerformLayout();
            this.ToolStripShiftSchedule.ResumeLayout(false);
            this.ToolStripShiftSchedule.PerformLayout();
            this.SplitRight.Panel1.ResumeLayout(false);
            this.SplitRight.Panel2.ResumeLayout(false);
            this.SplitRight.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitRight)).EndInit();
            this.SplitRight.ResumeLayout(false);
            this.TabCtrlMain.ResumeLayout(false);
            this.TbPgFormView.ResumeLayout(false);
            this.TbPgFormView.PerformLayout();
            this.TableLayout1.ResumeLayout(false);
            this.TableLayout1.PerformLayout();
            this.StatusStrip2.ResumeLayout(false);
            this.StatusStrip2.PerformLayout();
            this.ToolStrip5.ResumeLayout(false);
            this.ToolStrip5.PerformLayout();
            this.TbPgSetPwd.ResumeLayout(false);
            this.TbPgSetPwd.PerformLayout();
            this.TableLayout2.ResumeLayout(false);
            this.TableLayout2.PerformLayout();
            this.StatusStrip3.ResumeLayout(false);
            this.StatusStrip3.PerformLayout();
            this.ToolStrip2.ResumeLayout(false);
            this.ToolStrip2.PerformLayout();
            this.TbPgSetMstPwd.ResumeLayout(false);
            this.TbPgSetMstPwd.PerformLayout();
            this.TableLayout3.ResumeLayout(false);
            this.TableLayout3.PerformLayout();
            this.StatusStrip4.ResumeLayout(false);
            this.StatusStrip4.PerformLayout();
            this.ToolStrip3.ResumeLayout(false);
            this.ToolStrip3.PerformLayout();
            this.TabRightPanel.ResumeLayout(false);
            this.tabPagePref.ResumeLayout(false);
            this.tabPageTips.ResumeLayout(false);
            this.ToolStrip4.ResumeLayout(false);
            this.ToolStrip4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ToolStrip ToolStripCaption;
        private System.Windows.Forms.ToolStripLabel TsLblCaption;
        private FontAwesome.Sharp.IconToolStripButton TsBtnClose;
        private System.Windows.Forms.StatusStrip StripInputFooter;
        private System.Windows.Forms.ToolStripStatusLabel TsLblInputStatus;
        private System.Windows.Forms.ToolStripProgressBar TsProgressBar;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private FontAwesome.Sharp.IconToolStripButton TsBtnSidePanel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        internal FontAwesome.Sharp.IconDropDownButton TsBtnOptions;
        internal System.Windows.Forms.ToolStripMenuItem TsBtnOptTech;
        internal System.Windows.Forms.ToolStripMenuItem TsMenuViewLockedUsers;
        internal System.Windows.Forms.ToolStripMenuItem TsMenuViewDeletedUsers;
        internal FontAwesome.Sharp.IconDropDownButton TsBtnDrpUnitCode;
        internal FontAwesome.Sharp.IconToolStripButton TsBtnCloseForm;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        internal System.Windows.Forms.SplitContainer SplitMain;
        internal System.Windows.Forms.DataGridView DgvListUsers;
        internal System.Windows.Forms.StatusStrip StatusStrip1;
        internal System.Windows.Forms.ToolStripStatusLabel TsLblDgvListFooter;
        internal System.Windows.Forms.ToolStrip ToolStripShiftSchedule;
        internal System.Windows.Forms.ToolStripLabel TsLblShiftPat;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator12;
        internal FontAwesome.Sharp.IconDropDownButton TsBtnDrpDgvCols;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        internal FontAwesome.Sharp.IconToolStripButton TsBtRefreshDgv;
        internal System.Windows.Forms.ToolStripTextBox TsTxtSearchDgv;
        internal FontAwesome.Sharp.IconToolStripButton TsBtnClearSearchDgv;
        internal FontAwesome.Sharp.IconToolStripButton TsBtnSearchDgv;
        internal System.Windows.Forms.SplitContainer SplitRight;
        internal System.Windows.Forms.TabControl TabCtrlMain;
        internal System.Windows.Forms.TabPage TbPgFormView;
        internal System.Windows.Forms.TableLayoutPanel TableLayout1;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.ComboBox CmbUnitCode;
        internal System.Windows.Forms.TextBox TxtUserLoginName;
        internal System.Windows.Forms.TextBox TxtUserFirstName;
        internal System.Windows.Forms.TextBox TxtUserLastName;
        internal System.Windows.Forms.ComboBox CmbDept;
        internal System.Windows.Forms.TextBox TxtUserEmail;
        internal System.Windows.Forms.TextBox TxtUserContact;
        internal System.Windows.Forms.Label Label10;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.ComboBox CmbUserLocked;
        internal System.Windows.Forms.ComboBox CmbUserDeleted;
        internal System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.ComboBox CmbUserRole;
        internal System.Windows.Forms.StatusStrip StatusStrip2;
        internal System.Windows.Forms.ToolStripStatusLabel TsLblInputFooter;
        internal System.Windows.Forms.ToolStrip ToolStrip5;
        internal FontAwesome.Sharp.IconToolStripButton TsBtnAddNew;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator5;
        internal FontAwesome.Sharp.IconToolStripButton TsBtnSave;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator6;
        internal System.Windows.Forms.ToolStripTextBox TxtRecordId;
        internal System.Windows.Forms.TabPage TbPgSetPwd;
        internal System.Windows.Forms.TableLayoutPanel TableLayout2;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.TextBox TxtEnterPwd;
        internal System.Windows.Forms.TextBox TxtConfirmPwd;
        internal System.Windows.Forms.StatusStrip StatusStrip3;
        internal System.Windows.Forms.ToolStripStatusLabel TsLblSetPwdFooter;
        internal System.Windows.Forms.ToolStrip ToolStrip2;
        internal FontAwesome.Sharp.IconToolStripButton TsBtnUpdatePwd;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator8;
        internal System.Windows.Forms.TabPage TbPgSetMstPwd;
        internal System.Windows.Forms.TableLayoutPanel TableLayout3;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.Label Label14;
        internal System.Windows.Forms.TextBox TxtEnterMstPwd;
        internal System.Windows.Forms.TextBox TxtConfirmMstPwd;
        internal System.Windows.Forms.StatusStrip StatusStrip4;
        internal System.Windows.Forms.ToolStripStatusLabel TsLblFooterMstPwd;
        internal System.Windows.Forms.ToolStrip ToolStrip3;
        internal FontAwesome.Sharp.IconToolStripButton TsBtnUpdateMstPwd;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator9;
        internal System.Windows.Forms.TabControl TabRightPanel;
        internal System.Windows.Forms.TabPage tabPagePref;
        internal System.Windows.Forms.PropertyGrid PgridOptions;
        internal System.Windows.Forms.TabPage tabPageTips;
        internal System.Windows.Forms.RichTextBox txtHelp;
        internal System.Windows.Forms.ToolStrip ToolStrip4;
        internal FontAwesome.Sharp.IconToolStripButton IconToolStripButton2;
        internal FontAwesome.Sharp.IconToolStripButton TsBtnFilterClose;
        private System.Windows.Forms.ImageList ImageListTabCtrl;
    }
}
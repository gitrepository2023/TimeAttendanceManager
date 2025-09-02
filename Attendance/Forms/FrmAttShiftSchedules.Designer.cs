
namespace TimeAttendanceManager.Attendance.Forms
{
    partial class FrmAttShiftSchedules
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAttShiftSchedules));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label12 = new System.Windows.Forms.Label();
            this.CmbShiftCode = new System.Windows.Forms.ComboBox();
            this.TxtShiftDesc = new System.Windows.Forms.TextBox();
            this.CmbShiftType = new System.Windows.Forms.ComboBox();
            this.DtPickShiftStartTime = new System.Windows.Forms.DateTimePicker();
            this.DtPickShiftEndTime = new System.Windows.Forms.DateTimePicker();
            this.DtPickShiftLunchStartTime = new System.Windows.Forms.DateTimePicker();
            this.DtPickShiftLunchEndTime = new System.Windows.Forms.DateTimePicker();
            this.ChkIsActive = new System.Windows.Forms.CheckBox();
            this.NumShiftTotalHrs = new System.Windows.Forms.NumericUpDown();
            this.NumShiftGraceEarlyMin = new System.Windows.Forms.NumericUpDown();
            this.NumShiftGraceLateMin = new System.Windows.Forms.NumericUpDown();
            this.NumShiftLunchGraceEarlyMin = new System.Windows.Forms.NumericUpDown();
            this.NumShiftLunchGraceLateMin = new System.Windows.Forms.NumericUpDown();
            this.NumShiftLunchMinTime = new System.Windows.Forms.NumericUpDown();
            this.NumShiftLunchHour = new System.Windows.Forms.NumericUpDown();
            this.NumShiftInLatePermitted = new System.Windows.Forms.NumericUpDown();
            this.NumLunchLatePermitted = new System.Windows.Forms.NumericUpDown();
            this.NumShiftOtMinMinutes = new System.Windows.Forms.NumericUpDown();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.ToolStripCaption = new System.Windows.Forms.ToolStrip();
            this.TsLblCaption = new System.Windows.Forms.ToolStripLabel();
            this.TsBtnClose = new FontAwesome.Sharp.IconToolStripButton();
            this.ImageListTabCtrl = new System.Windows.Forms.ImageList(this.components);
            this.StripInputFooter = new System.Windows.Forms.StatusStrip();
            this.TsLblInputStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.TsProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.iconSplitButton1 = new FontAwesome.Sharp.IconSplitButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.TsBtnSidePanel = new FontAwesome.Sharp.IconToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.TsBtnDrpUnitCode = new FontAwesome.Sharp.IconDropDownButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.TsBtnOptions = new FontAwesome.Sharp.IconDropDownButton();
            this.TsBtnOptTech = new System.Windows.Forms.ToolStripMenuItem();
            this.TsMenuViewInActive = new System.Windows.Forms.ToolStripMenuItem();
            this.TsMenuViewDeleted = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.TsBtnCloseForm = new FontAwesome.Sharp.IconToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.TsBtnHelp = new FontAwesome.Sharp.IconToolStripButton();
            this.SplitMain = new System.Windows.Forms.SplitContainer();
            this.DgvList = new System.Windows.Forms.DataGridView();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.TsBtnDrpShiftType = new FontAwesome.Sharp.IconDropDownButton();
            this.TsCmbLimitRows = new System.Windows.Forms.ToolStripComboBox();
            this.StatusStrip1 = new System.Windows.Forms.StatusStrip();
            this.TsLblDgvListFooter = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStripShiftSchedule = new System.Windows.Forms.ToolStrip();
            this.TsLblShiftPat = new System.Windows.Forms.ToolStripLabel();
            this.ToolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.TsBtnDrpDgvCols = new FontAwesome.Sharp.IconDropDownButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.TsBtRefreshDgv = new FontAwesome.Sharp.IconToolStripButton();
            this.TsTxtSearchDgv = new System.Windows.Forms.ToolStripTextBox();
            this.TsBtnClearSearchDgv = new FontAwesome.Sharp.IconToolStripButton();
            this.TsBtnSearchDgv = new FontAwesome.Sharp.IconToolStripButton();
            this.SplitRight = new System.Windows.Forms.SplitContainer();
            this.TabCtrlMain = new System.Windows.Forms.TabControl();
            this.TbPgInput = new System.Windows.Forms.TabPage();
            this.TableLayout1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.CmbUnitCode = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.StatusStrip2 = new System.Windows.Forms.StatusStrip();
            this.TsLblInputFooter = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStrip5 = new System.Windows.Forms.ToolStrip();
            this.TsBtnAddNew = new FontAwesome.Sharp.IconToolStripButton();
            this.ToolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.TsBtnSave = new FontAwesome.Sharp.IconToolStripButton();
            this.ToolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.TsBtnDelete = new FontAwesome.Sharp.IconToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.TsTxtRecordId = new System.Windows.Forms.ToolStripTextBox();
            this.TbPgList = new System.Windows.Forms.TabPage();
            this.WebViewShiftList = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.statusStrip3 = new System.Windows.Forms.StatusStrip();
            this.TsLblFooterWebView = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.TsBtnRefreshWebView = new FontAwesome.Sharp.IconToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.TsBtnDrpColsWebView = new FontAwesome.Sharp.IconDropDownButton();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.TsBtnExport = new FontAwesome.Sharp.IconToolStripButton();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.TsBtnExporotExcel = new FontAwesome.Sharp.IconToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.TsTxtSearchWebView = new System.Windows.Forms.ToolStripTextBox();
            this.TsBtnClearWebView = new FontAwesome.Sharp.IconToolStripButton();
            this.TsBtnSearchWebView = new FontAwesome.Sharp.IconToolStripButton();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripTextBox2 = new System.Windows.Forms.ToolStripTextBox();
            this.TabRightPanel = new System.Windows.Forms.TabControl();
            this.tabPagePref = new System.Windows.Forms.TabPage();
            this.PgridOptions = new System.Windows.Forms.PropertyGrid();
            this.tabPageTips = new System.Windows.Forms.TabPage();
            this.txtHelp = new System.Windows.Forms.RichTextBox();
            this.ToolStrip4 = new System.Windows.Forms.ToolStrip();
            this.IconToolStripButton2 = new FontAwesome.Sharp.IconToolStripButton();
            this.TsBtnFilterClose = new FontAwesome.Sharp.IconToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.NumShiftTotalHrs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumShiftGraceEarlyMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumShiftGraceLateMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumShiftLunchGraceEarlyMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumShiftLunchGraceLateMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumShiftLunchMinTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumShiftLunchHour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumShiftInLatePermitted)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumLunchLatePermitted)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumShiftOtMinMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.ToolStripCaption.SuspendLayout();
            this.StripInputFooter.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitMain)).BeginInit();
            this.SplitMain.Panel1.SuspendLayout();
            this.SplitMain.Panel2.SuspendLayout();
            this.SplitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvList)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.StatusStrip1.SuspendLayout();
            this.ToolStripShiftSchedule.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitRight)).BeginInit();
            this.SplitRight.Panel1.SuspendLayout();
            this.SplitRight.Panel2.SuspendLayout();
            this.SplitRight.SuspendLayout();
            this.TabCtrlMain.SuspendLayout();
            this.TbPgInput.SuspendLayout();
            this.TableLayout1.SuspendLayout();
            this.StatusStrip2.SuspendLayout();
            this.ToolStrip5.SuspendLayout();
            this.TbPgList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WebViewShiftList)).BeginInit();
            this.statusStrip3.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.TabRightPanel.SuspendLayout();
            this.tabPagePref.SuspendLayout();
            this.tabPageTips.SuspendLayout();
            this.ToolStrip4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.MediumBlue;
            this.label12.Location = new System.Drawing.Point(3, 430);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(189, 17);
            this.label12.TabIndex = 11;
            this.label12.Text = "Shift Lunch Grace Late Minutes:";
            this.toolTip1.SetToolTip(this.label12, "Set shift lunch grace late minutes");
            // 
            // CmbShiftCode
            // 
            this.CmbShiftCode.FormattingEnabled = true;
            this.CmbShiftCode.Location = new System.Drawing.Point(252, 63);
            this.CmbShiftCode.Name = "CmbShiftCode";
            this.CmbShiftCode.Size = new System.Drawing.Size(121, 25);
            this.CmbShiftCode.TabIndex = 1;
            this.toolTip1.SetToolTip(this.CmbShiftCode, "Set shift codes like A, B or C ...");
            // 
            // TxtShiftDesc
            // 
            this.TxtShiftDesc.Location = new System.Drawing.Point(252, 93);
            this.TxtShiftDesc.Name = "TxtShiftDesc";
            this.TxtShiftDesc.Size = new System.Drawing.Size(100, 25);
            this.TxtShiftDesc.TabIndex = 2;
            this.toolTip1.SetToolTip(this.TxtShiftDesc, "Enter shift description like First Shift, Second Shift");
            // 
            // CmbShiftType
            // 
            this.CmbShiftType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbShiftType.FormattingEnabled = true;
            this.CmbShiftType.Location = new System.Drawing.Point(252, 123);
            this.CmbShiftType.Name = "CmbShiftType";
            this.CmbShiftType.Size = new System.Drawing.Size(121, 25);
            this.CmbShiftType.TabIndex = 3;
            this.toolTip1.SetToolTip(this.CmbShiftType, "Shift type Day or Night");
            // 
            // DtPickShiftStartTime
            // 
            this.DtPickShiftStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.DtPickShiftStartTime.Location = new System.Drawing.Point(252, 153);
            this.DtPickShiftStartTime.Name = "DtPickShiftStartTime";
            this.DtPickShiftStartTime.ShowCheckBox = true;
            this.DtPickShiftStartTime.Size = new System.Drawing.Size(134, 25);
            this.DtPickShiftStartTime.TabIndex = 4;
            this.toolTip1.SetToolTip(this.DtPickShiftStartTime, "Set shift start time");
            // 
            // DtPickShiftEndTime
            // 
            this.DtPickShiftEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.DtPickShiftEndTime.Location = new System.Drawing.Point(252, 183);
            this.DtPickShiftEndTime.Name = "DtPickShiftEndTime";
            this.DtPickShiftEndTime.ShowCheckBox = true;
            this.DtPickShiftEndTime.Size = new System.Drawing.Size(134, 25);
            this.DtPickShiftEndTime.TabIndex = 5;
            this.toolTip1.SetToolTip(this.DtPickShiftEndTime, "Set shift end time");
            // 
            // DtPickShiftLunchStartTime
            // 
            this.DtPickShiftLunchStartTime.CalendarForeColor = System.Drawing.Color.MediumBlue;
            this.DtPickShiftLunchStartTime.CalendarTitleForeColor = System.Drawing.Color.MediumBlue;
            this.DtPickShiftLunchStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.DtPickShiftLunchStartTime.Location = new System.Drawing.Point(252, 343);
            this.DtPickShiftLunchStartTime.Name = "DtPickShiftLunchStartTime";
            this.DtPickShiftLunchStartTime.ShowCheckBox = true;
            this.DtPickShiftLunchStartTime.Size = new System.Drawing.Size(134, 25);
            this.DtPickShiftLunchStartTime.TabIndex = 9;
            this.toolTip1.SetToolTip(this.DtPickShiftLunchStartTime, "Set shift lunch start time");
            // 
            // DtPickShiftLunchEndTime
            // 
            this.DtPickShiftLunchEndTime.CalendarForeColor = System.Drawing.Color.MediumBlue;
            this.DtPickShiftLunchEndTime.CalendarTitleForeColor = System.Drawing.Color.MediumBlue;
            this.DtPickShiftLunchEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.DtPickShiftLunchEndTime.Location = new System.Drawing.Point(252, 373);
            this.DtPickShiftLunchEndTime.Name = "DtPickShiftLunchEndTime";
            this.DtPickShiftLunchEndTime.ShowCheckBox = true;
            this.DtPickShiftLunchEndTime.Size = new System.Drawing.Size(134, 25);
            this.DtPickShiftLunchEndTime.TabIndex = 10;
            this.toolTip1.SetToolTip(this.DtPickShiftLunchEndTime, "Set shift lunch end time");
            // 
            // ChkIsActive
            // 
            this.ChkIsActive.AutoSize = true;
            this.ChkIsActive.Checked = true;
            this.ChkIsActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkIsActive.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ChkIsActive.Location = new System.Drawing.Point(252, 643);
            this.ChkIsActive.Name = "ChkIsActive";
            this.ChkIsActive.Size = new System.Drawing.Size(46, 21);
            this.ChkIsActive.TabIndex = 18;
            this.ChkIsActive.Text = "Yes";
            this.toolTip1.SetToolTip(this.ChkIsActive, "Set Shift Active or In-Active");
            this.ChkIsActive.UseVisualStyleBackColor = true;
            // 
            // NumShiftTotalHrs
            // 
            this.NumShiftTotalHrs.Location = new System.Drawing.Point(252, 213);
            this.NumShiftTotalHrs.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.NumShiftTotalHrs.Name = "NumShiftTotalHrs";
            this.NumShiftTotalHrs.Size = new System.Drawing.Size(120, 25);
            this.NumShiftTotalHrs.TabIndex = 6;
            this.toolTip1.SetToolTip(this.NumShiftTotalHrs, "Total shift hours (8 or 12)");
            // 
            // NumShiftGraceEarlyMin
            // 
            this.NumShiftGraceEarlyMin.Location = new System.Drawing.Point(252, 263);
            this.NumShiftGraceEarlyMin.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.NumShiftGraceEarlyMin.Name = "NumShiftGraceEarlyMin";
            this.NumShiftGraceEarlyMin.Size = new System.Drawing.Size(120, 25);
            this.NumShiftGraceEarlyMin.TabIndex = 7;
            this.toolTip1.SetToolTip(this.NumShiftGraceEarlyMin, "Allow shift IN early minutes");
            // 
            // NumShiftGraceLateMin
            // 
            this.NumShiftGraceLateMin.Location = new System.Drawing.Point(252, 293);
            this.NumShiftGraceLateMin.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.NumShiftGraceLateMin.Name = "NumShiftGraceLateMin";
            this.NumShiftGraceLateMin.Size = new System.Drawing.Size(120, 25);
            this.NumShiftGraceLateMin.TabIndex = 8;
            this.toolTip1.SetToolTip(this.NumShiftGraceLateMin, "Allow shift IN grace late minutes");
            // 
            // NumShiftLunchGraceEarlyMin
            // 
            this.NumShiftLunchGraceEarlyMin.Location = new System.Drawing.Point(252, 493);
            this.NumShiftLunchGraceEarlyMin.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.NumShiftLunchGraceEarlyMin.Name = "NumShiftLunchGraceEarlyMin";
            this.NumShiftLunchGraceEarlyMin.Size = new System.Drawing.Size(120, 25);
            this.NumShiftLunchGraceEarlyMin.TabIndex = 14;
            this.toolTip1.SetToolTip(this.NumShiftLunchGraceEarlyMin, "Allow shift lunch grace early minutes");
            // 
            // NumShiftLunchGraceLateMin
            // 
            this.NumShiftLunchGraceLateMin.Location = new System.Drawing.Point(252, 433);
            this.NumShiftLunchGraceLateMin.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.NumShiftLunchGraceLateMin.Name = "NumShiftLunchGraceLateMin";
            this.NumShiftLunchGraceLateMin.Size = new System.Drawing.Size(120, 25);
            this.NumShiftLunchGraceLateMin.TabIndex = 12;
            this.toolTip1.SetToolTip(this.NumShiftLunchGraceLateMin, "Allow shift lunch grace late minutes");
            // 
            // NumShiftLunchMinTime
            // 
            this.NumShiftLunchMinTime.Location = new System.Drawing.Point(252, 463);
            this.NumShiftLunchMinTime.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.NumShiftLunchMinTime.Name = "NumShiftLunchMinTime";
            this.NumShiftLunchMinTime.Size = new System.Drawing.Size(120, 25);
            this.NumShiftLunchMinTime.TabIndex = 13;
            this.toolTip1.SetToolTip(this.NumShiftLunchMinTime, "Set minimum lunch period like 10 minutes");
            // 
            // NumShiftLunchHour
            // 
            this.NumShiftLunchHour.ForeColor = System.Drawing.Color.MidnightBlue;
            this.NumShiftLunchHour.Location = new System.Drawing.Point(252, 403);
            this.NumShiftLunchHour.Name = "NumShiftLunchHour";
            this.NumShiftLunchHour.Size = new System.Drawing.Size(120, 25);
            this.NumShiftLunchHour.TabIndex = 11;
            this.toolTip1.SetToolTip(this.NumShiftLunchHour, "Enter total lunch hour in minutes\r\nfor half hour enter 30 \r\nfor one hour enter 60" +
        "\r\n");
            // 
            // NumShiftInLatePermitted
            // 
            this.NumShiftInLatePermitted.Location = new System.Drawing.Point(252, 583);
            this.NumShiftInLatePermitted.Name = "NumShiftInLatePermitted";
            this.NumShiftInLatePermitted.Size = new System.Drawing.Size(120, 25);
            this.NumShiftInLatePermitted.TabIndex = 16;
            this.toolTip1.SetToolTip(this.NumShiftInLatePermitted, "Set monthly shift IN late permission like 26 minutes per month");
            // 
            // NumLunchLatePermitted
            // 
            this.NumLunchLatePermitted.Location = new System.Drawing.Point(252, 613);
            this.NumLunchLatePermitted.Name = "NumLunchLatePermitted";
            this.NumLunchLatePermitted.Size = new System.Drawing.Size(120, 25);
            this.NumLunchLatePermitted.TabIndex = 17;
            this.toolTip1.SetToolTip(this.NumLunchLatePermitted, "Set monthly lunch late permission like 26 minutes per month");
            // 
            // NumShiftOtMinMinutes
            // 
            this.NumShiftOtMinMinutes.Location = new System.Drawing.Point(252, 553);
            this.NumShiftOtMinMinutes.Maximum = new decimal(new int[] {
            240,
            0,
            0,
            0});
            this.NumShiftOtMinMinutes.Name = "NumShiftOtMinMinutes";
            this.NumShiftOtMinMinutes.Size = new System.Drawing.Size(120, 25);
            this.NumShiftOtMinMinutes.TabIndex = 15;
            this.toolTip1.SetToolTip(this.NumShiftOtMinMinutes, "Enter Minimum Minutes to Qualify for OT\r\nfor half hour enter 30\r\nfor one hour ent" +
        "er 1\r\nfor four hour enter 240");
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
            this.ToolStripCaption.TabIndex = 4;
            this.ToolStripCaption.Text = "toolStrip1";
            // 
            // TsLblCaption
            // 
            this.TsLblCaption.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.TsLblCaption.ForeColor = System.Drawing.Color.Green;
            this.TsLblCaption.Image = ((System.Drawing.Image)(resources.GetObject("TsLblCaption.Image")));
            this.TsLblCaption.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TsLblCaption.Name = "TsLblCaption";
            this.TsLblCaption.Size = new System.Drawing.Size(239, 28);
            this.TsLblCaption.Text = "Manage Shift Schedule";
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
            // ImageListTabCtrl
            // 
            this.ImageListTabCtrl.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageListTabCtrl.ImageStream")));
            this.ImageListTabCtrl.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageListTabCtrl.Images.SetKeyName(0, "pinned.ico");
            this.ImageListTabCtrl.Images.SetKeyName(1, "unpinned.ico");
            this.ImageListTabCtrl.Images.SetKeyName(2, "note_text16x16.png");
            this.ImageListTabCtrl.Images.SetKeyName(3, "notebook16x16.png");
            // 
            // StripInputFooter
            // 
            this.StripInputFooter.BackColor = System.Drawing.Color.LemonChiffon;
            this.StripInputFooter.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.StripInputFooter.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsLblInputStatus,
            this.TsProgressBar});
            this.StripInputFooter.Location = new System.Drawing.Point(0, 539);
            this.StripInputFooter.Name = "StripInputFooter";
            this.StripInputFooter.Size = new System.Drawing.Size(933, 22);
            this.StripInputFooter.TabIndex = 5;
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
            // iconSplitButton1
            // 
            this.iconSplitButton1.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.iconSplitButton1.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconSplitButton1.IconColor = System.Drawing.Color.Black;
            this.iconSplitButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconSplitButton1.IconSize = 48;
            this.iconSplitButton1.Name = "iconSplitButton1";
            this.iconSplitButton1.Rotation = 0D;
            this.iconSplitButton1.Size = new System.Drawing.Size(23, 23);
            this.iconSplitButton1.Text = "iconSplitButton1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsBtnSidePanel,
            this.toolStripSeparator1,
            this.TsBtnDrpUnitCode,
            this.toolStripSeparator2,
            this.TsBtnOptions,
            this.toolStripSeparator4,
            this.TsBtnCloseForm,
            this.toolStripSeparator7,
            this.TsBtnHelp});
            this.toolStrip1.Location = new System.Drawing.Point(0, 31);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(933, 31);
            this.toolStrip1.TabIndex = 7;
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
            this.TsBtnDrpUnitCode.Size = new System.Drawing.Size(66, 28);
            this.TsBtnDrpUnitCode.Text = "(All)";
            this.TsBtnDrpUnitCode.ToolTipText = "Select Plant Code";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // TsBtnOptions
            // 
            this.TsBtnOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsBtnOptTech,
            this.TsMenuViewInActive,
            this.TsMenuViewDeleted});
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
            this.TsBtnOptTech.Size = new System.Drawing.Size(177, 22);
            this.TsBtnOptTech.Text = "Technical Details";
            // 
            // TsMenuViewInActive
            // 
            this.TsMenuViewInActive.Name = "TsMenuViewInActive";
            this.TsMenuViewInActive.Size = new System.Drawing.Size(177, 22);
            this.TsMenuViewInActive.Text = "View InActive Shifts";
            // 
            // TsMenuViewDeleted
            // 
            this.TsMenuViewDeleted.Name = "TsMenuViewDeleted";
            this.TsMenuViewDeleted.Size = new System.Drawing.Size(177, 22);
            this.TsMenuViewDeleted.Text = "View Deleted Shifts";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
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
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 31);
            // 
            // TsBtnHelp
            // 
            this.TsBtnHelp.ForeColor = System.Drawing.Color.DarkGreen;
            this.TsBtnHelp.IconChar = FontAwesome.Sharp.IconChar.Question;
            this.TsBtnHelp.IconColor = System.Drawing.Color.DarkGreen;
            this.TsBtnHelp.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.TsBtnHelp.IconSize = 22;
            this.TsBtnHelp.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TsBtnHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsBtnHelp.Name = "TsBtnHelp";
            this.TsBtnHelp.Size = new System.Drawing.Size(58, 28);
            this.TsBtnHelp.Text = "Help";
            // 
            // SplitMain
            // 
            this.SplitMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SplitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitMain.Location = new System.Drawing.Point(0, 62);
            this.SplitMain.Name = "SplitMain";
            // 
            // SplitMain.Panel1
            // 
            this.SplitMain.Panel1.Controls.Add(this.DgvList);
            this.SplitMain.Panel1.Controls.Add(this.toolStrip2);
            this.SplitMain.Panel1.Controls.Add(this.StatusStrip1);
            this.SplitMain.Panel1.Controls.Add(this.ToolStripShiftSchedule);
            // 
            // SplitMain.Panel2
            // 
            this.SplitMain.Panel2.Controls.Add(this.SplitRight);
            this.SplitMain.Size = new System.Drawing.Size(933, 477);
            this.SplitMain.SplitterDistance = 311;
            this.SplitMain.TabIndex = 8;
            // 
            // DgvList
            // 
            this.DgvList.AllowUserToAddRows = false;
            this.DgvList.AllowUserToDeleteRows = false;
            this.DgvList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DgvList.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.DgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgvList.Location = new System.Drawing.Point(0, 50);
            this.DgvList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DgvList.Name = "DgvList";
            this.DgvList.ReadOnly = true;
            this.DgvList.RowHeadersWidth = 51;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.DarkBlue;
            this.DgvList.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DgvList.Size = new System.Drawing.Size(309, 403);
            this.DgvList.TabIndex = 32;
            // 
            // toolStrip2
            // 
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsBtnDrpShiftType,
            this.TsCmbLimitRows});
            this.toolStrip2.Location = new System.Drawing.Point(0, 25);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(309, 25);
            this.toolStrip2.TabIndex = 31;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // TsBtnDrpShiftType
            // 
            this.TsBtnDrpShiftType.ForeColor = System.Drawing.Color.DarkOrchid;
            this.TsBtnDrpShiftType.IconChar = FontAwesome.Sharp.IconChar.ClockFour;
            this.TsBtnDrpShiftType.IconColor = System.Drawing.Color.ForestGreen;
            this.TsBtnDrpShiftType.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.TsBtnDrpShiftType.IconSize = 18;
            this.TsBtnDrpShiftType.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TsBtnDrpShiftType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsBtnDrpShiftType.Name = "TsBtnDrpShiftType";
            this.TsBtnDrpShiftType.Size = new System.Drawing.Size(90, 22);
            this.TsBtnDrpShiftType.Text = "Shift Type";
            this.TsBtnDrpShiftType.ToolTipText = "Set filter by User Role";
            // 
            // TsCmbLimitRows
            // 
            this.TsCmbLimitRows.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.TsCmbLimitRows.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TsCmbLimitRows.ForeColor = System.Drawing.Color.Blue;
            this.TsCmbLimitRows.Items.AddRange(new object[] {
            "(All)",
            "20",
            "50",
            "100",
            ""});
            this.TsCmbLimitRows.Name = "TsCmbLimitRows";
            this.TsCmbLimitRows.Size = new System.Drawing.Size(75, 25);
            this.TsCmbLimitRows.Text = "(All)";
            this.TsCmbLimitRows.ToolTipText = "Limit Rows";
            // 
            // StatusStrip1
            // 
            this.StatusStrip1.BackColor = System.Drawing.Color.LemonChiffon;
            this.StatusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.StatusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsLblDgvListFooter});
            this.StatusStrip1.Location = new System.Drawing.Point(0, 453);
            this.StatusStrip1.Name = "StatusStrip1";
            this.StatusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.StatusStrip1.Size = new System.Drawing.Size(309, 22);
            this.StatusStrip1.TabIndex = 27;
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
            this.toolStripSeparator3,
            this.TsBtRefreshDgv,
            this.TsTxtSearchDgv,
            this.TsBtnClearSearchDgv,
            this.TsBtnSearchDgv});
            this.ToolStripShiftSchedule.Location = new System.Drawing.Point(0, 0);
            this.ToolStripShiftSchedule.Name = "ToolStripShiftSchedule";
            this.ToolStripShiftSchedule.Size = new System.Drawing.Size(309, 25);
            this.ToolStripShiftSchedule.TabIndex = 26;
            this.ToolStripShiftSchedule.Text = "ToolStrip1";
            // 
            // TsLblShiftPat
            // 
            this.TsLblShiftPat.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TsLblShiftPat.ForeColor = System.Drawing.Color.DarkMagenta;
            this.TsLblShiftPat.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TsLblShiftPat.Name = "TsLblShiftPat";
            this.TsLblShiftPat.Size = new System.Drawing.Size(25, 22);
            this.TsLblShiftPat.Text = "List";
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
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
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
            this.SplitRight.Size = new System.Drawing.Size(618, 477);
            this.SplitRight.SplitterDistance = 458;
            this.SplitRight.TabIndex = 0;
            // 
            // TabCtrlMain
            // 
            this.TabCtrlMain.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.TabCtrlMain.Controls.Add(this.TbPgInput);
            this.TabCtrlMain.Controls.Add(this.TbPgList);
            this.TabCtrlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabCtrlMain.Location = new System.Drawing.Point(0, 0);
            this.TabCtrlMain.Name = "TabCtrlMain";
            this.TabCtrlMain.SelectedIndex = 0;
            this.TabCtrlMain.Size = new System.Drawing.Size(456, 475);
            this.TabCtrlMain.TabIndex = 0;
            // 
            // TbPgInput
            // 
            this.TbPgInput.Controls.Add(this.TableLayout1);
            this.TbPgInput.Controls.Add(this.StatusStrip2);
            this.TbPgInput.Controls.Add(this.ToolStrip5);
            this.TbPgInput.Location = new System.Drawing.Point(4, 29);
            this.TbPgInput.Name = "TbPgInput";
            this.TbPgInput.Padding = new System.Windows.Forms.Padding(3);
            this.TbPgInput.Size = new System.Drawing.Size(448, 442);
            this.TbPgInput.TabIndex = 0;
            this.TbPgInput.Text = "Form View";
            this.TbPgInput.UseVisualStyleBackColor = true;
            // 
            // TableLayout1
            // 
            this.TableLayout1.AutoScroll = true;
            this.TableLayout1.ColumnCount = 3;
            this.TableLayout1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.TableLayout1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.TableLayout1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.TableLayout1.Controls.Add(this.label1, 0, 1);
            this.TableLayout1.Controls.Add(this.label2, 0, 2);
            this.TableLayout1.Controls.Add(this.label3, 0, 3);
            this.TableLayout1.Controls.Add(this.label4, 0, 4);
            this.TableLayout1.Controls.Add(this.label5, 0, 5);
            this.TableLayout1.Controls.Add(this.label6, 0, 6);
            this.TableLayout1.Controls.Add(this.label7, 0, 9);
            this.TableLayout1.Controls.Add(this.label8, 0, 10);
            this.TableLayout1.Controls.Add(this.label9, 0, 12);
            this.TableLayout1.Controls.Add(this.label10, 0, 13);
            this.TableLayout1.Controls.Add(this.label12, 0, 15);
            this.TableLayout1.Controls.Add(this.label13, 0, 16);
            this.TableLayout1.Controls.Add(this.CmbUnitCode, 1, 1);
            this.TableLayout1.Controls.Add(this.CmbShiftCode, 1, 2);
            this.TableLayout1.Controls.Add(this.TxtShiftDesc, 1, 3);
            this.TableLayout1.Controls.Add(this.CmbShiftType, 1, 4);
            this.TableLayout1.Controls.Add(this.DtPickShiftStartTime, 1, 5);
            this.TableLayout1.Controls.Add(this.DtPickShiftEndTime, 1, 6);
            this.TableLayout1.Controls.Add(this.DtPickShiftLunchStartTime, 1, 12);
            this.TableLayout1.Controls.Add(this.DtPickShiftLunchEndTime, 1, 13);
            this.TableLayout1.Controls.Add(this.label17, 0, 20);
            this.TableLayout1.Controls.Add(this.label18, 0, 21);
            this.TableLayout1.Controls.Add(this.label19, 0, 22);
            this.TableLayout1.Controls.Add(this.ChkIsActive, 1, 22);
            this.TableLayout1.Controls.Add(this.label15, 0, 7);
            this.TableLayout1.Controls.Add(this.NumShiftTotalHrs, 1, 7);
            this.TableLayout1.Controls.Add(this.NumShiftGraceEarlyMin, 1, 9);
            this.TableLayout1.Controls.Add(this.NumShiftGraceLateMin, 1, 10);
            this.TableLayout1.Controls.Add(this.NumShiftLunchGraceLateMin, 1, 15);
            this.TableLayout1.Controls.Add(this.NumShiftLunchMinTime, 1, 16);
            this.TableLayout1.Controls.Add(this.NumShiftInLatePermitted, 1, 20);
            this.TableLayout1.Controls.Add(this.NumLunchLatePermitted, 1, 21);
            this.TableLayout1.Controls.Add(this.NumShiftLunchHour, 1, 14);
            this.TableLayout1.Controls.Add(this.NumShiftLunchGraceEarlyMin, 1, 17);
            this.TableLayout1.Controls.Add(this.label14, 0, 14);
            this.TableLayout1.Controls.Add(this.label11, 0, 17);
            this.TableLayout1.Controls.Add(this.label16, 0, 19);
            this.TableLayout1.Controls.Add(this.NumShiftOtMinMinutes, 1, 19);
            this.TableLayout1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableLayout1.ForeColor = System.Drawing.Color.DarkMagenta;
            this.TableLayout1.Location = new System.Drawing.Point(3, 30);
            this.TableLayout1.Name = "TableLayout1";
            this.TableLayout1.RowCount = 24;
            this.TableLayout1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayout1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayout1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayout1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayout1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayout1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayout1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayout1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayout1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TableLayout1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayout1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayout1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
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
            this.TableLayout1.Size = new System.Drawing.Size(442, 387);
            this.TableLayout1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Plant Code:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Shift Code:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Shift Description:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Shift Type:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 150);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "Shift Start Time:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 180);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 17);
            this.label6.TabIndex = 5;
            this.label6.Text = "Shift End Time:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 260);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(156, 17);
            this.label7.TabIndex = 6;
            this.label7.Text = "Shift Grace Early Minutes:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 290);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(152, 17);
            this.label8.TabIndex = 7;
            this.label8.Text = "Shift Grace Late Minutes:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.MediumBlue;
            this.label9.Location = new System.Drawing.Point(3, 340);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(136, 17);
            this.label9.TabIndex = 8;
            this.label9.Text = "Shift Lunch Start Time:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.MediumBlue;
            this.label10.Location = new System.Drawing.Point(3, 370);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(131, 17);
            this.label10.TabIndex = 9;
            this.label10.Text = "Shift Lunch End Time:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.MediumBlue;
            this.label13.Location = new System.Drawing.Point(3, 460);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(163, 17);
            this.label13.TabIndex = 12;
            this.label13.Text = "Shift Lunch Minimum Time:";
            // 
            // CmbUnitCode
            // 
            this.CmbUnitCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbUnitCode.FormattingEnabled = true;
            this.CmbUnitCode.Location = new System.Drawing.Point(252, 33);
            this.CmbUnitCode.Name = "CmbUnitCode";
            this.CmbUnitCode.Size = new System.Drawing.Size(121, 25);
            this.CmbUnitCode.TabIndex = 0;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.DarkRed;
            this.label17.Location = new System.Drawing.Point(3, 580);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(242, 17);
            this.label17.TabIndex = 34;
            this.label17.Text = "Monthly Shift IN Late Minutes Permitted:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.Color.DarkRed;
            this.label18.Location = new System.Drawing.Point(3, 610);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(243, 17);
            this.label18.TabIndex = 35;
            this.label18.Text = "Monthly LUNCH Late Minutes permitted:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(3, 640);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(58, 17);
            this.label19.TabIndex = 38;
            this.label19.Text = "Is Active:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(3, 210);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(161, 17);
            this.label15.TabIndex = 14;
            this.label15.Text = "Total Shift Duration Hours:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label14.Location = new System.Drawing.Point(3, 400);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(188, 17);
            this.label14.TabIndex = 13;
            this.label14.Text = "Shift Total Lunch Hour Minutes:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.MediumBlue;
            this.label11.Location = new System.Drawing.Point(3, 490);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(193, 17);
            this.label11.TabIndex = 10;
            this.label11.Text = "Shift Lunch Grace Early Minutes:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(3, 550);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(216, 17);
            this.label16.TabIndex = 39;
            this.label16.Text = "Minimum Minutes to Qualify for OT:";
            // 
            // StatusStrip2
            // 
            this.StatusStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.StatusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsLblInputFooter});
            this.StatusStrip2.Location = new System.Drawing.Point(3, 417);
            this.StatusStrip2.Name = "StatusStrip2";
            this.StatusStrip2.Size = new System.Drawing.Size(442, 22);
            this.StatusStrip2.TabIndex = 5;
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
            this.TsBtnDelete,
            this.toolStripSeparator8,
            this.TsTxtRecordId});
            this.ToolStrip5.Location = new System.Drawing.Point(3, 3);
            this.ToolStrip5.Name = "ToolStrip5";
            this.ToolStrip5.Size = new System.Drawing.Size(442, 27);
            this.ToolStrip5.TabIndex = 4;
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
            // TsBtnDelete
            // 
            this.TsBtnDelete.ForeColor = System.Drawing.Color.DarkRed;
            this.TsBtnDelete.IconChar = FontAwesome.Sharp.IconChar.TrashAlt;
            this.TsBtnDelete.IconColor = System.Drawing.Color.Red;
            this.TsBtnDelete.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.TsBtnDelete.IconSize = 20;
            this.TsBtnDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TsBtnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsBtnDelete.Name = "TsBtnDelete";
            this.TsBtnDelete.Size = new System.Drawing.Size(64, 24);
            this.TsBtnDelete.Text = "Delete";
            this.TsBtnDelete.ToolTipText = "Delete selected shift";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 27);
            // 
            // TsTxtRecordId
            // 
            this.TsTxtRecordId.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.TsTxtRecordId.Enabled = false;
            this.TsTxtRecordId.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TsTxtRecordId.Name = "TsTxtRecordId";
            this.TsTxtRecordId.ReadOnly = true;
            this.TsTxtRecordId.Size = new System.Drawing.Size(100, 27);
            this.TsTxtRecordId.Visible = false;
            // 
            // TbPgList
            // 
            this.TbPgList.Controls.Add(this.WebViewShiftList);
            this.TbPgList.Controls.Add(this.statusStrip3);
            this.TbPgList.Controls.Add(this.toolStrip3);
            this.TbPgList.Location = new System.Drawing.Point(4, 25);
            this.TbPgList.Name = "TbPgList";
            this.TbPgList.Padding = new System.Windows.Forms.Padding(3);
            this.TbPgList.Size = new System.Drawing.Size(448, 446);
            this.TbPgList.TabIndex = 1;
            this.TbPgList.Text = "List View";
            this.TbPgList.UseVisualStyleBackColor = true;
            // 
            // WebViewShiftList
            // 
            this.WebViewShiftList.AllowExternalDrop = true;
            this.WebViewShiftList.CreationProperties = null;
            this.WebViewShiftList.DefaultBackgroundColor = System.Drawing.Color.White;
            this.WebViewShiftList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WebViewShiftList.Location = new System.Drawing.Point(3, 32);
            this.WebViewShiftList.Name = "WebViewShiftList";
            this.WebViewShiftList.Size = new System.Drawing.Size(442, 389);
            this.WebViewShiftList.TabIndex = 31;
            this.WebViewShiftList.ZoomFactor = 1D;
            // 
            // statusStrip3
            // 
            this.statusStrip3.BackColor = System.Drawing.Color.LemonChiffon;
            this.statusStrip3.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsLblFooterWebView});
            this.statusStrip3.Location = new System.Drawing.Point(3, 421);
            this.statusStrip3.Name = "statusStrip3";
            this.statusStrip3.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip3.Size = new System.Drawing.Size(442, 22);
            this.statusStrip3.TabIndex = 30;
            this.statusStrip3.Text = "statusStrip3";
            // 
            // TsLblFooterWebView
            // 
            this.TsLblFooterWebView.ForeColor = System.Drawing.Color.DarkBlue;
            this.TsLblFooterWebView.Name = "TsLblFooterWebView";
            this.TsLblFooterWebView.Size = new System.Drawing.Size(16, 17);
            this.TsLblFooterWebView.Text = "...";
            // 
            // toolStrip3
            // 
            this.toolStrip3.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsBtnRefreshWebView,
            this.toolStripSeparator10,
            this.TsBtnDrpColsWebView,
            this.toolStripSeparator11,
            this.TsBtnExport,
            this.toolStripSeparator13,
            this.TsBtnExporotExcel,
            this.toolStripSeparator9,
            this.TsTxtSearchWebView,
            this.TsBtnClearWebView,
            this.TsBtnSearchWebView,
            this.toolStripComboBox1,
            this.toolStripTextBox2});
            this.toolStrip3.Location = new System.Drawing.Point(3, 3);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(442, 29);
            this.toolStrip3.TabIndex = 7;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // TsBtnRefreshWebView
            // 
            this.TsBtnRefreshWebView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsBtnRefreshWebView.IconChar = FontAwesome.Sharp.IconChar.Sync;
            this.TsBtnRefreshWebView.IconColor = System.Drawing.Color.RoyalBlue;
            this.TsBtnRefreshWebView.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.TsBtnRefreshWebView.IconSize = 18;
            this.TsBtnRefreshWebView.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TsBtnRefreshWebView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsBtnRefreshWebView.Name = "TsBtnRefreshWebView";
            this.TsBtnRefreshWebView.Size = new System.Drawing.Size(23, 26);
            this.TsBtnRefreshWebView.Text = "Refresh";
            this.TsBtnRefreshWebView.ToolTipText = "Refresh List";
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 29);
            // 
            // TsBtnDrpColsWebView
            // 
            this.TsBtnDrpColsWebView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsBtnDrpColsWebView.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.TsBtnDrpColsWebView.IconColor = System.Drawing.Color.DarkSlateGray;
            this.TsBtnDrpColsWebView.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.TsBtnDrpColsWebView.IconSize = 18;
            this.TsBtnDrpColsWebView.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TsBtnDrpColsWebView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsBtnDrpColsWebView.Name = "TsBtnDrpColsWebView";
            this.TsBtnDrpColsWebView.Size = new System.Drawing.Size(31, 26);
            this.TsBtnDrpColsWebView.Text = "Show / Hide Columns";
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 29);
            // 
            // TsBtnExport
            // 
            this.TsBtnExport.ForeColor = System.Drawing.Color.Maroon;
            this.TsBtnExport.IconChar = FontAwesome.Sharp.IconChar.FileCode;
            this.TsBtnExport.IconColor = System.Drawing.Color.Maroon;
            this.TsBtnExport.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.TsBtnExport.IconSize = 22;
            this.TsBtnExport.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TsBtnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsBtnExport.Name = "TsBtnExport";
            this.TsBtnExport.Size = new System.Drawing.Size(87, 26);
            this.TsBtnExport.Text = "Export List";
            this.TsBtnExport.ToolTipText = "Export List to HTML";
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(6, 29);
            // 
            // TsBtnExporotExcel
            // 
            this.TsBtnExporotExcel.IconChar = FontAwesome.Sharp.IconChar.FileExcel;
            this.TsBtnExporotExcel.IconColor = System.Drawing.Color.DarkGreen;
            this.TsBtnExporotExcel.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.TsBtnExporotExcel.IconSize = 22;
            this.TsBtnExporotExcel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TsBtnExporotExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsBtnExporotExcel.Name = "TsBtnExporotExcel";
            this.TsBtnExporotExcel.Size = new System.Drawing.Size(95, 26);
            this.TsBtnExporotExcel.Text = "Export Excel";
            this.TsBtnExporotExcel.ToolTipText = "Export list to Excel";
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 29);
            // 
            // TsTxtSearchWebView
            // 
            this.TsTxtSearchWebView.BackColor = System.Drawing.Color.LemonChiffon;
            this.TsTxtSearchWebView.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TsTxtSearchWebView.Name = "TsTxtSearchWebView";
            this.TsTxtSearchWebView.Size = new System.Drawing.Size(150, 29);
            this.TsTxtSearchWebView.ToolTipText = "Enter Search Text";
            // 
            // TsBtnClearWebView
            // 
            this.TsBtnClearWebView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsBtnClearWebView.ForeColor = System.Drawing.Color.DarkRed;
            this.TsBtnClearWebView.IconChar = FontAwesome.Sharp.IconChar.Remove;
            this.TsBtnClearWebView.IconColor = System.Drawing.Color.DarkRed;
            this.TsBtnClearWebView.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.TsBtnClearWebView.IconSize = 16;
            this.TsBtnClearWebView.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TsBtnClearWebView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsBtnClearWebView.Name = "TsBtnClearWebView";
            this.TsBtnClearWebView.Size = new System.Drawing.Size(23, 20);
            this.TsBtnClearWebView.Text = "Clear";
            this.TsBtnClearWebView.ToolTipText = "Clear search text";
            // 
            // TsBtnSearchWebView
            // 
            this.TsBtnSearchWebView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsBtnSearchWebView.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.TsBtnSearchWebView.IconColor = System.Drawing.Color.Blue;
            this.TsBtnSearchWebView.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.TsBtnSearchWebView.IconSize = 16;
            this.TsBtnSearchWebView.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TsBtnSearchWebView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsBtnSearchWebView.Name = "TsBtnSearchWebView";
            this.TsBtnSearchWebView.Size = new System.Drawing.Size(23, 20);
            this.TsBtnSearchWebView.Text = "Search";
            this.TsBtnSearchWebView.ToolTipText = "Search in DataGrid";
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripComboBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripComboBox1.ForeColor = System.Drawing.Color.Blue;
            this.toolStripComboBox1.Items.AddRange(new object[] {
            "(All)",
            "20",
            "50",
            "100",
            ""});
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(75, 23);
            this.toolStripComboBox1.Text = "(All)";
            this.toolStripComboBox1.ToolTipText = "Limit Rows";
            // 
            // toolStripTextBox2
            // 
            this.toolStripTextBox2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripTextBox2.Enabled = false;
            this.toolStripTextBox2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox2.Name = "toolStripTextBox2";
            this.toolStripTextBox2.ReadOnly = true;
            this.toolStripTextBox2.Size = new System.Drawing.Size(75, 23);
            this.toolStripTextBox2.Visible = false;
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
            this.TabRightPanel.Size = new System.Drawing.Size(154, 450);
            this.TabRightPanel.TabIndex = 42;
            // 
            // tabPagePref
            // 
            this.tabPagePref.Controls.Add(this.PgridOptions);
            this.tabPagePref.ImageKey = "notebook16x16.png";
            this.tabPagePref.Location = new System.Drawing.Point(4, 4);
            this.tabPagePref.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPagePref.Name = "tabPagePref";
            this.tabPagePref.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPagePref.Size = new System.Drawing.Size(146, 420);
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
            this.PgridOptions.Size = new System.Drawing.Size(140, 412);
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
            this.tabPageTips.Size = new System.Drawing.Size(146, 423);
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
            this.txtHelp.Size = new System.Drawing.Size(140, 415);
            this.txtHelp.TabIndex = 1;
            this.txtHelp.Text = "";
            // 
            // ToolStrip4
            // 
            this.ToolStrip4.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ToolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.IconToolStripButton2,
            this.TsBtnFilterClose});
            this.ToolStrip4.Location = new System.Drawing.Point(0, 0);
            this.ToolStrip4.Name = "ToolStrip4";
            this.ToolStrip4.Size = new System.Drawing.Size(154, 25);
            this.ToolStrip4.TabIndex = 8;
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
            // FrmAttShiftSchedules
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(933, 561);
            this.Controls.Add(this.SplitMain);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.StripInputFooter);
            this.Controls.Add(this.ToolStripCaption);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmAttShiftSchedules";
            this.ShowIcon = false;
            this.Text = "Manage Shift Schedule";
            ((System.ComponentModel.ISupportInitialize)(this.NumShiftTotalHrs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumShiftGraceEarlyMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumShiftGraceLateMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumShiftLunchGraceEarlyMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumShiftLunchGraceLateMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumShiftLunchMinTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumShiftLunchHour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumShiftInLatePermitted)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumLunchLatePermitted)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumShiftOtMinMinutes)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.DgvList)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
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
            this.TbPgInput.ResumeLayout(false);
            this.TbPgInput.PerformLayout();
            this.TableLayout1.ResumeLayout(false);
            this.TableLayout1.PerformLayout();
            this.StatusStrip2.ResumeLayout(false);
            this.StatusStrip2.PerformLayout();
            this.ToolStrip5.ResumeLayout(false);
            this.ToolStrip5.PerformLayout();
            this.TbPgList.ResumeLayout(false);
            this.TbPgList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WebViewShiftList)).EndInit();
            this.statusStrip3.ResumeLayout(false);
            this.statusStrip3.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
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
        private System.Windows.Forms.ImageList ImageListTabCtrl;
        private System.Windows.Forms.StatusStrip StripInputFooter;
        private System.Windows.Forms.ToolStripStatusLabel TsLblInputStatus;
        private System.Windows.Forms.ToolStripProgressBar TsProgressBar;
        private FontAwesome.Sharp.IconSplitButton iconSplitButton1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private FontAwesome.Sharp.IconToolStripButton TsBtnSidePanel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        internal FontAwesome.Sharp.IconDropDownButton TsBtnDrpUnitCode;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        internal FontAwesome.Sharp.IconDropDownButton TsBtnOptions;
        internal System.Windows.Forms.ToolStripMenuItem TsBtnOptTech;
        internal System.Windows.Forms.ToolStripMenuItem TsMenuViewInActive;
        internal System.Windows.Forms.ToolStripMenuItem TsMenuViewDeleted;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        internal FontAwesome.Sharp.IconToolStripButton TsBtnCloseForm;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private FontAwesome.Sharp.IconToolStripButton TsBtnHelp;
        private System.Windows.Forms.SplitContainer SplitMain;
        private System.Windows.Forms.SplitContainer SplitRight;
        internal System.Windows.Forms.ToolStrip ToolStrip4;
        internal FontAwesome.Sharp.IconToolStripButton IconToolStripButton2;
        internal FontAwesome.Sharp.IconToolStripButton TsBtnFilterClose;
        internal System.Windows.Forms.TabControl TabRightPanel;
        internal System.Windows.Forms.TabPage tabPagePref;
        internal System.Windows.Forms.PropertyGrid PgridOptions;
        internal System.Windows.Forms.TabPage tabPageTips;
        internal System.Windows.Forms.RichTextBox txtHelp;
        internal System.Windows.Forms.ToolStrip ToolStripShiftSchedule;
        internal System.Windows.Forms.ToolStripLabel TsLblShiftPat;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator12;
        internal FontAwesome.Sharp.IconDropDownButton TsBtnDrpDgvCols;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        internal FontAwesome.Sharp.IconToolStripButton TsBtRefreshDgv;
        internal System.Windows.Forms.ToolStripTextBox TsTxtSearchDgv;
        internal FontAwesome.Sharp.IconToolStripButton TsBtnClearSearchDgv;
        internal FontAwesome.Sharp.IconToolStripButton TsBtnSearchDgv;
        internal System.Windows.Forms.StatusStrip StatusStrip1;
        internal System.Windows.Forms.ToolStripStatusLabel TsLblDgvListFooter;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private FontAwesome.Sharp.IconDropDownButton TsBtnDrpShiftType;
        private System.Windows.Forms.ToolStripComboBox TsCmbLimitRows;
        internal System.Windows.Forms.DataGridView DgvList;
        private System.Windows.Forms.TabControl TabCtrlMain;
        private System.Windows.Forms.TabPage TbPgInput;
        private System.Windows.Forms.TabPage TbPgList;
        internal System.Windows.Forms.ToolStrip ToolStrip5;
        internal FontAwesome.Sharp.IconToolStripButton TsBtnAddNew;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator5;
        internal FontAwesome.Sharp.IconToolStripButton TsBtnSave;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator6;
        internal System.Windows.Forms.ToolStripTextBox TsTxtRecordId;
        internal System.Windows.Forms.StatusStrip StatusStrip2;
        internal System.Windows.Forms.ToolStripStatusLabel TsLblInputFooter;
        private System.Windows.Forms.TableLayoutPanel TableLayout1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox CmbUnitCode;
        private System.Windows.Forms.ComboBox CmbShiftCode;
        private System.Windows.Forms.TextBox TxtShiftDesc;
        private System.Windows.Forms.ComboBox CmbShiftType;
        private System.Windows.Forms.DateTimePicker DtPickShiftStartTime;
        private System.Windows.Forms.DateTimePicker DtPickShiftEndTime;
        private System.Windows.Forms.DateTimePicker DtPickShiftLunchStartTime;
        private System.Windows.Forms.DateTimePicker DtPickShiftLunchEndTime;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.CheckBox ChkIsActive;
        private FontAwesome.Sharp.IconToolStripButton TsBtnDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.NumericUpDown NumShiftTotalHrs;
        private System.Windows.Forms.NumericUpDown NumShiftGraceEarlyMin;
        private System.Windows.Forms.NumericUpDown NumShiftGraceLateMin;
        private System.Windows.Forms.NumericUpDown NumShiftLunchGraceEarlyMin;
        private System.Windows.Forms.NumericUpDown NumShiftLunchGraceLateMin;
        private System.Windows.Forms.NumericUpDown NumShiftLunchMinTime;
        private System.Windows.Forms.NumericUpDown NumShiftLunchHour;
        private System.Windows.Forms.NumericUpDown NumShiftInLatePermitted;
        private System.Windows.Forms.NumericUpDown NumLunchLatePermitted;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.NumericUpDown NumShiftOtMinMinutes;
        internal System.Windows.Forms.ToolStrip toolStrip3;
        internal FontAwesome.Sharp.IconToolStripButton TsBtnRefreshWebView;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        internal FontAwesome.Sharp.IconDropDownButton TsBtnDrpColsWebView;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private FontAwesome.Sharp.IconToolStripButton TsBtnExport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        internal System.Windows.Forms.ToolStripTextBox TsTxtSearchWebView;
        internal FontAwesome.Sharp.IconToolStripButton TsBtnClearWebView;
        internal FontAwesome.Sharp.IconToolStripButton TsBtnSearchWebView;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        internal System.Windows.Forms.ToolStripTextBox toolStripTextBox2;
        internal System.Windows.Forms.StatusStrip statusStrip3;
        internal System.Windows.Forms.ToolStripStatusLabel TsLblFooterWebView;
        private Microsoft.Web.WebView2.WinForms.WebView2 WebViewShiftList;
        private FontAwesome.Sharp.IconToolStripButton TsBtnExporotExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
    }
}
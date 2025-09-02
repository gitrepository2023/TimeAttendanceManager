
namespace TimeAttendanceManager.MenuDesign.Forms
{
    partial class FrmMenuSetGroups
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMenuSetGroups));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.ImageListTabCtrl = new System.Windows.Forms.ImageList(this.components);
            this.ToolStripCaption = new System.Windows.Forms.ToolStrip();
            this.TsLblCaption = new System.Windows.Forms.ToolStripLabel();
            this.TsBtnClose = new FontAwesome.Sharp.IconToolStripButton();
            this.StripInputFooter = new System.Windows.Forms.StatusStrip();
            this.TsLblInputStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.TsProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.ToolStrip5 = new System.Windows.Forms.ToolStrip();
            this.TsDetails = new FontAwesome.Sharp.IconToolStripButton();
            this.ToolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.TsBtnDrpUnitCode = new FontAwesome.Sharp.IconDropDownButton();
            this.ToolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.TsBtnAddNew = new FontAwesome.Sharp.IconToolStripButton();
            this.ToolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.TsBtnSave = new FontAwesome.Sharp.IconToolStripButton();
            this.ToolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.TsBtnDelRow = new FontAwesome.Sharp.IconToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.TsBtnDiscard = new FontAwesome.Sharp.IconToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.TsBtnCloseForm = new FontAwesome.Sharp.IconToolStripButton();
            this.TsTxtRowId = new System.Windows.Forms.ToolStripTextBox();
            this.ToolStripShiftSchedule = new System.Windows.Forms.ToolStrip();
            this.TsBtRefreshDgv = new FontAwesome.Sharp.IconToolStripButton();
            this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.TsBtnDrpColsDgvTblCols = new FontAwesome.Sharp.IconDropDownButton();
            this.ToolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.TsBtnExpandCollapse = new FontAwesome.Sharp.IconToolStripButton();
            this.ToolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.TsTxtSearchDgv = new System.Windows.Forms.ToolStripTextBox();
            this.TsBtnClearSearchDgv = new FontAwesome.Sharp.IconToolStripButton();
            this.TsBtnSearchDgv = new FontAwesome.Sharp.IconToolStripButton();
            this.TsCmbLimitRows = new System.Windows.Forms.ToolStripComboBox();
            this.DgvList = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.ToolStripCaption.SuspendLayout();
            this.StripInputFooter.SuspendLayout();
            this.ToolStrip5.SuspendLayout();
            this.ToolStripShiftSchedule.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvList)).BeginInit();
            this.SuspendLayout();
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
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
            // ToolStripCaption
            // 
            this.ToolStripCaption.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ToolStripCaption.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsLblCaption,
            this.TsBtnClose});
            this.ToolStripCaption.Location = new System.Drawing.Point(0, 0);
            this.ToolStripCaption.Name = "ToolStripCaption";
            this.ToolStripCaption.Size = new System.Drawing.Size(933, 31);
            this.ToolStripCaption.TabIndex = 1;
            this.ToolStripCaption.Text = "toolStrip1";
            // 
            // TsLblCaption
            // 
            this.TsLblCaption.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.TsLblCaption.ForeColor = System.Drawing.Color.Green;
            this.TsLblCaption.Image = ((System.Drawing.Image)(resources.GetObject("TsLblCaption.Image")));
            this.TsLblCaption.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TsLblCaption.Name = "TsLblCaption";
            this.TsLblCaption.Size = new System.Drawing.Size(192, 28);
            this.TsLblCaption.Text = "Set Menu Groups";
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
            this.StripInputFooter.TabIndex = 2;
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
            // ToolStrip5
            // 
            this.ToolStrip5.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsDetails,
            this.ToolStripSeparator5,
            this.TsBtnDrpUnitCode,
            this.ToolStripSeparator6,
            this.TsBtnAddNew,
            this.ToolStripSeparator10,
            this.TsBtnSave,
            this.ToolStripSeparator11,
            this.TsBtnDelRow,
            this.toolStripSeparator1,
            this.TsBtnDiscard,
            this.toolStripSeparator2,
            this.TsBtnCloseForm,
            this.TsTxtRowId});
            this.ToolStrip5.Location = new System.Drawing.Point(0, 31);
            this.ToolStrip5.Name = "ToolStrip5";
            this.ToolStrip5.Size = new System.Drawing.Size(933, 31);
            this.ToolStrip5.TabIndex = 6;
            this.ToolStrip5.Text = "ToolStrip5";
            // 
            // TsDetails
            // 
            this.TsDetails.IconChar = FontAwesome.Sharp.IconChar.ArrowDownAZ;
            this.TsDetails.IconColor = System.Drawing.Color.Maroon;
            this.TsDetails.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.TsDetails.IconSize = 16;
            this.TsDetails.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TsDetails.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsDetails.Name = "TsDetails";
            this.TsDetails.Size = new System.Drawing.Size(36, 28);
            this.TsDetails.Text = "...";
            // 
            // ToolStripSeparator5
            // 
            this.ToolStripSeparator5.Name = "ToolStripSeparator5";
            this.ToolStripSeparator5.Size = new System.Drawing.Size(6, 31);
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
            // ToolStripSeparator6
            // 
            this.ToolStripSeparator6.Name = "ToolStripSeparator6";
            this.ToolStripSeparator6.Size = new System.Drawing.Size(6, 31);
            // 
            // TsBtnAddNew
            // 
            this.TsBtnAddNew.ForeColor = System.Drawing.Color.Blue;
            this.TsBtnAddNew.IconChar = FontAwesome.Sharp.IconChar.PenAlt;
            this.TsBtnAddNew.IconColor = System.Drawing.Color.RoyalBlue;
            this.TsBtnAddNew.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.TsBtnAddNew.IconSize = 20;
            this.TsBtnAddNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TsBtnAddNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsBtnAddNew.Name = "TsBtnAddNew";
            this.TsBtnAddNew.Size = new System.Drawing.Size(80, 28);
            this.TsBtnAddNew.Text = "Add New";
            // 
            // ToolStripSeparator10
            // 
            this.ToolStripSeparator10.Name = "ToolStripSeparator10";
            this.ToolStripSeparator10.Size = new System.Drawing.Size(6, 31);
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
            this.TsBtnSave.Size = new System.Drawing.Size(58, 28);
            this.TsBtnSave.Text = "Save";
            this.TsBtnSave.ToolTipText = "Save changes";
            // 
            // ToolStripSeparator11
            // 
            this.ToolStripSeparator11.Name = "ToolStripSeparator11";
            this.ToolStripSeparator11.Size = new System.Drawing.Size(6, 31);
            // 
            // TsBtnDelRow
            // 
            this.TsBtnDelRow.ForeColor = System.Drawing.Color.DarkRed;
            this.TsBtnDelRow.IconChar = FontAwesome.Sharp.IconChar.TrashAlt;
            this.TsBtnDelRow.IconColor = System.Drawing.Color.DarkRed;
            this.TsBtnDelRow.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.TsBtnDelRow.IconSize = 20;
            this.TsBtnDelRow.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TsBtnDelRow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsBtnDelRow.Name = "TsBtnDelRow";
            this.TsBtnDelRow.Size = new System.Drawing.Size(90, 28);
            this.TsBtnDelRow.Text = "Delete Row";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // TsBtnDiscard
            // 
            this.TsBtnDiscard.IconChar = FontAwesome.Sharp.IconChar.Ban;
            this.TsBtnDiscard.IconColor = System.Drawing.Color.DarkRed;
            this.TsBtnDiscard.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.TsBtnDiscard.IconSize = 18;
            this.TsBtnDiscard.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TsBtnDiscard.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsBtnDiscard.Name = "TsBtnDiscard";
            this.TsBtnDiscard.Size = new System.Drawing.Size(68, 28);
            this.TsBtnDiscard.Text = "Discard";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // TsBtnCloseForm
            // 
            this.TsBtnCloseForm.IconChar = FontAwesome.Sharp.IconChar.Remove;
            this.TsBtnCloseForm.IconColor = System.Drawing.Color.Black;
            this.TsBtnCloseForm.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.TsBtnCloseForm.IconSize = 18;
            this.TsBtnCloseForm.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TsBtnCloseForm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsBtnCloseForm.Name = "TsBtnCloseForm";
            this.TsBtnCloseForm.Size = new System.Drawing.Size(89, 28);
            this.TsBtnCloseForm.Text = "Close Form";
            // 
            // TsTxtRowId
            // 
            this.TsTxtRowId.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.TsTxtRowId.Enabled = false;
            this.TsTxtRowId.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TsTxtRowId.Name = "TsTxtRowId";
            this.TsTxtRowId.ReadOnly = true;
            this.TsTxtRowId.Size = new System.Drawing.Size(100, 31);
            this.TsTxtRowId.Visible = false;
            // 
            // ToolStripShiftSchedule
            // 
            this.ToolStripShiftSchedule.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ToolStripShiftSchedule.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsBtRefreshDgv,
            this.ToolStripSeparator3,
            this.TsBtnDrpColsDgvTblCols,
            this.ToolStripSeparator12,
            this.TsBtnExpandCollapse,
            this.ToolStripSeparator8,
            this.TsTxtSearchDgv,
            this.TsBtnClearSearchDgv,
            this.TsBtnSearchDgv,
            this.TsCmbLimitRows});
            this.ToolStripShiftSchedule.Location = new System.Drawing.Point(0, 62);
            this.ToolStripShiftSchedule.Name = "ToolStripShiftSchedule";
            this.ToolStripShiftSchedule.Size = new System.Drawing.Size(933, 25);
            this.ToolStripShiftSchedule.TabIndex = 30;
            this.ToolStripShiftSchedule.Text = "ToolStrip1";
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
            // ToolStripSeparator3
            // 
            this.ToolStripSeparator3.Name = "ToolStripSeparator3";
            this.ToolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // TsBtnDrpColsDgvTblCols
            // 
            this.TsBtnDrpColsDgvTblCols.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsBtnDrpColsDgvTblCols.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.TsBtnDrpColsDgvTblCols.IconColor = System.Drawing.Color.Black;
            this.TsBtnDrpColsDgvTblCols.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.TsBtnDrpColsDgvTblCols.IconSize = 16;
            this.TsBtnDrpColsDgvTblCols.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TsBtnDrpColsDgvTblCols.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsBtnDrpColsDgvTblCols.Name = "TsBtnDrpColsDgvTblCols";
            this.TsBtnDrpColsDgvTblCols.Size = new System.Drawing.Size(29, 22);
            this.TsBtnDrpColsDgvTblCols.Text = "Show / Hide Columns";
            // 
            // ToolStripSeparator12
            // 
            this.ToolStripSeparator12.Name = "ToolStripSeparator12";
            this.ToolStripSeparator12.Size = new System.Drawing.Size(6, 25);
            // 
            // TsBtnExpandCollapse
            // 
            this.TsBtnExpandCollapse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsBtnExpandCollapse.IconChar = FontAwesome.Sharp.IconChar.List;
            this.TsBtnExpandCollapse.IconColor = System.Drawing.Color.SteelBlue;
            this.TsBtnExpandCollapse.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.TsBtnExpandCollapse.IconSize = 16;
            this.TsBtnExpandCollapse.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TsBtnExpandCollapse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsBtnExpandCollapse.Name = "TsBtnExpandCollapse";
            this.TsBtnExpandCollapse.Size = new System.Drawing.Size(23, 22);
            this.TsBtnExpandCollapse.Text = "Expand / Collpase";
            // 
            // ToolStripSeparator8
            // 
            this.ToolStripSeparator8.Name = "ToolStripSeparator8";
            this.ToolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // TsTxtSearchDgv
            // 
            this.TsTxtSearchDgv.BackColor = System.Drawing.Color.LemonChiffon;
            this.TsTxtSearchDgv.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TsTxtSearchDgv.ForeColor = System.Drawing.Color.Blue;
            this.TsTxtSearchDgv.Name = "TsTxtSearchDgv";
            this.TsTxtSearchDgv.Size = new System.Drawing.Size(220, 25);
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
            // TsCmbLimitRows
            // 
            this.TsCmbLimitRows.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.TsCmbLimitRows.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.TsCmbLimitRows.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.TsCmbLimitRows.ForeColor = System.Drawing.Color.Blue;
            this.TsCmbLimitRows.Items.AddRange(new object[] {
            "10",
            "50",
            "70",
            "100",
            "(All)"});
            this.TsCmbLimitRows.Name = "TsCmbLimitRows";
            this.TsCmbLimitRows.Size = new System.Drawing.Size(75, 25);
            this.TsCmbLimitRows.Text = "(All)";
            this.TsCmbLimitRows.ToolTipText = "Limit Rows";
            // 
            // DgvList
            // 
            this.DgvList.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.DgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgvList.Location = new System.Drawing.Point(0, 87);
            this.DgvList.Name = "DgvList";
            this.DgvList.Size = new System.Drawing.Size(933, 479);
            this.DgvList.TabIndex = 31;
            // 
            // FrmMenuSetGroups
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(933, 588);
            this.Controls.Add(this.DgvList);
            this.Controls.Add(this.ToolStripShiftSchedule);
            this.Controls.Add(this.ToolStrip5);
            this.Controls.Add(this.StripInputFooter);
            this.Controls.Add(this.ToolStripCaption);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmMenuSetGroups";
            this.ShowIcon = false;
            this.Text = "Set Menu Groups";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ToolStripCaption.ResumeLayout(false);
            this.ToolStripCaption.PerformLayout();
            this.StripInputFooter.ResumeLayout(false);
            this.StripInputFooter.PerformLayout();
            this.ToolStrip5.ResumeLayout(false);
            this.ToolStrip5.PerformLayout();
            this.ToolStripShiftSchedule.ResumeLayout(false);
            this.ToolStripShiftSchedule.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ImageList ImageListTabCtrl;
        private System.Windows.Forms.ToolStrip ToolStripCaption;
        private System.Windows.Forms.ToolStripLabel TsLblCaption;
        private FontAwesome.Sharp.IconToolStripButton TsBtnClose;
        private System.Windows.Forms.StatusStrip StripInputFooter;
        private System.Windows.Forms.ToolStripStatusLabel TsLblInputStatus;
        private System.Windows.Forms.ToolStripProgressBar TsProgressBar;
        internal System.Windows.Forms.ToolStrip ToolStrip5;
        internal FontAwesome.Sharp.IconToolStripButton TsDetails;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator5;
        internal FontAwesome.Sharp.IconToolStripButton TsBtnAddNew;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator6;
        internal FontAwesome.Sharp.IconToolStripButton TsBtnSave;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator10;
        internal FontAwesome.Sharp.IconToolStripButton TsBtnDelRow;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator11;
        internal FontAwesome.Sharp.IconToolStripButton TsBtnDiscard;
        internal System.Windows.Forms.ToolStripTextBox TsTxtRowId;
        internal System.Windows.Forms.ToolStrip ToolStripShiftSchedule;
        internal FontAwesome.Sharp.IconToolStripButton TsBtRefreshDgv;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator3;
        internal FontAwesome.Sharp.IconDropDownButton TsBtnDrpColsDgvTblCols;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator12;
        internal FontAwesome.Sharp.IconToolStripButton TsBtnExpandCollapse;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator8;
        internal System.Windows.Forms.ToolStripTextBox TsTxtSearchDgv;
        internal FontAwesome.Sharp.IconToolStripButton TsBtnClearSearchDgv;
        internal FontAwesome.Sharp.IconToolStripButton TsBtnSearchDgv;
        internal System.Windows.Forms.ToolStripComboBox TsCmbLimitRows;
        private System.Windows.Forms.DataGridView DgvList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        internal FontAwesome.Sharp.IconToolStripButton TsBtnCloseForm;
        internal FontAwesome.Sharp.IconDropDownButton TsBtnDrpUnitCode;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}
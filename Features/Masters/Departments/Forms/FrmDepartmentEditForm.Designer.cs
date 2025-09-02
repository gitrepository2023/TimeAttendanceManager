
namespace TimeAttendanceManager.Features.Masters.Departments.Forms
{
    partial class FrmDepartmentEditForm
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
            this.ToolStrip5 = new System.Windows.Forms.ToolStrip();
            this.TsBtnSave = new FontAwesome.Sharp.IconToolStripButton();
            this.TsTxtRecordId = new System.Windows.Forms.ToolStripTextBox();
            this.StripInputFooter = new System.Windows.Forms.StatusStrip();
            this.TsLblInputStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.TsProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.TableLayout1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CmbUnitCode = new System.Windows.Forms.ComboBox();
            this.TxtCode = new System.Windows.Forms.TextBox();
            this.TxtName = new System.Windows.Forms.TextBox();
            this.ChkIsActive = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.TsBtnCancel = new FontAwesome.Sharp.IconToolStripButton();
            this.ToolStrip5.SuspendLayout();
            this.StripInputFooter.SuspendLayout();
            this.TableLayout1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // ToolStrip5
            // 
            this.ToolStrip5.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ToolStrip5.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsBtnSave,
            this.toolStripSeparator1,
            this.TsBtnCancel,
            this.TsTxtRecordId});
            this.ToolStrip5.Location = new System.Drawing.Point(0, 0);
            this.ToolStrip5.Name = "ToolStrip5";
            this.ToolStrip5.Size = new System.Drawing.Size(536, 27);
            this.ToolStrip5.TabIndex = 29;
            this.ToolStrip5.Text = "ToolStrip5";
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
            this.TsBtnSave.Click += new System.EventHandler(this.TsBtnSave_Click);
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
            // StripInputFooter
            // 
            this.StripInputFooter.BackColor = System.Drawing.Color.LemonChiffon;
            this.StripInputFooter.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.StripInputFooter.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsLblInputStatus,
            this.TsProgressBar});
            this.StripInputFooter.Location = new System.Drawing.Point(0, 219);
            this.StripInputFooter.Name = "StripInputFooter";
            this.StripInputFooter.Size = new System.Drawing.Size(536, 22);
            this.StripInputFooter.TabIndex = 30;
            this.StripInputFooter.Text = "statusStrip1";
            // 
            // TsLblInputStatus
            // 
            this.TsLblInputStatus.ForeColor = System.Drawing.Color.DarkBlue;
            this.TsLblInputStatus.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TsLblInputStatus.Name = "TsLblInputStatus";
            this.TsLblInputStatus.Size = new System.Drawing.Size(521, 17);
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
            this.TableLayout1.Controls.Add(this.CmbUnitCode, 1, 1);
            this.TableLayout1.Controls.Add(this.TxtCode, 1, 2);
            this.TableLayout1.Controls.Add(this.TxtName, 1, 3);
            this.TableLayout1.Controls.Add(this.ChkIsActive, 1, 4);
            this.TableLayout1.Controls.Add(this.label4, 0, 4);
            this.TableLayout1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableLayout1.ForeColor = System.Drawing.Color.DarkMagenta;
            this.TableLayout1.Location = new System.Drawing.Point(0, 27);
            this.TableLayout1.Name = "TableLayout1";
            this.TableLayout1.RowCount = 6;
            this.TableLayout1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayout1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayout1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayout1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayout1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayout1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayout1.Size = new System.Drawing.Size(536, 192);
            this.TableLayout1.TabIndex = 31;
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
            this.label2.Size = new System.Drawing.Size(115, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Department Code:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Department Name:";
            // 
            // CmbUnitCode
            // 
            this.CmbUnitCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbUnitCode.FormattingEnabled = true;
            this.CmbUnitCode.Location = new System.Drawing.Point(128, 33);
            this.CmbUnitCode.Name = "CmbUnitCode";
            this.CmbUnitCode.Size = new System.Drawing.Size(121, 25);
            this.CmbUnitCode.TabIndex = 3;
            // 
            // TxtCode
            // 
            this.TxtCode.Location = new System.Drawing.Point(128, 63);
            this.TxtCode.Name = "TxtCode";
            this.TxtCode.Size = new System.Drawing.Size(100, 25);
            this.TxtCode.TabIndex = 4;
            // 
            // TxtName
            // 
            this.TxtName.Location = new System.Drawing.Point(128, 93);
            this.TxtName.Name = "TxtName";
            this.TxtName.Size = new System.Drawing.Size(100, 25);
            this.TxtName.TabIndex = 5;
            // 
            // ChkIsActive
            // 
            this.ChkIsActive.AutoSize = true;
            this.ChkIsActive.Checked = true;
            this.ChkIsActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkIsActive.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ChkIsActive.Location = new System.Drawing.Point(128, 123);
            this.ChkIsActive.Name = "ChkIsActive";
            this.ChkIsActive.Size = new System.Drawing.Size(46, 21);
            this.ChkIsActive.TabIndex = 36;
            this.ChkIsActive.Text = "Yes";
            this.ChkIsActive.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 17);
            this.label4.TabIndex = 37;
            this.label4.Text = "Is Active:";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // TsBtnCancel
            // 
            this.TsBtnCancel.ForeColor = System.Drawing.Color.DimGray;
            this.TsBtnCancel.IconChar = FontAwesome.Sharp.IconChar.Remove;
            this.TsBtnCancel.IconColor = System.Drawing.Color.DimGray;
            this.TsBtnCancel.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.TsBtnCancel.IconSize = 20;
            this.TsBtnCancel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TsBtnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsBtnCancel.Name = "TsBtnCancel";
            this.TsBtnCancel.Size = new System.Drawing.Size(70, 24);
            this.TsBtnCancel.Text = "Discard";
            this.TsBtnCancel.ToolTipText = "Discard close form";
            // 
            // FrmDepartmentEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(536, 241);
            this.Controls.Add(this.TableLayout1);
            this.Controls.Add(this.StripInputFooter);
            this.Controls.Add(this.ToolStrip5);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmDepartmentEditForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update Department Details";
            this.ToolStrip5.ResumeLayout(false);
            this.ToolStrip5.PerformLayout();
            this.StripInputFooter.ResumeLayout(false);
            this.StripInputFooter.PerformLayout();
            this.TableLayout1.ResumeLayout(false);
            this.TableLayout1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ToolStrip ToolStrip5;
        internal FontAwesome.Sharp.IconToolStripButton TsBtnSave;
        internal System.Windows.Forms.ToolStripTextBox TsTxtRecordId;
        private System.Windows.Forms.StatusStrip StripInputFooter;
        private System.Windows.Forms.ToolStripStatusLabel TsLblInputStatus;
        private System.Windows.Forms.ToolStripProgressBar TsProgressBar;
        private System.Windows.Forms.TableLayoutPanel TableLayout1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox CmbUnitCode;
        private System.Windows.Forms.TextBox TxtCode;
        private System.Windows.Forms.TextBox TxtName;
        private System.Windows.Forms.CheckBox ChkIsActive;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private FontAwesome.Sharp.IconToolStripButton TsBtnCancel;
    }
}
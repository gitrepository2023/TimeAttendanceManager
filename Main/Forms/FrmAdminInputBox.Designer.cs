
namespace TimeAttendanceManager.Main.Forms
{
    partial class FrmAdminInputBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAdminInputBox));
            this.label1 = new System.Windows.Forms.Label();
            this.TxtPassword = new System.Windows.Forms.TextBox();
            this.BtnCancel = new FontAwesome.Sharp.IconButton();
            this.BtnOkay = new FontAwesome.Sharp.IconButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label1.Location = new System.Drawing.Point(132, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(232, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter Your Master Password";
            // 
            // TxtPassword
            // 
            this.TxtPassword.ForeColor = System.Drawing.Color.DarkBlue;
            this.TxtPassword.Location = new System.Drawing.Point(135, 50);
            this.TxtPassword.Name = "TxtPassword";
            this.TxtPassword.PasswordChar = 'x';
            this.TxtPassword.Size = new System.Drawing.Size(213, 29);
            this.TxtPassword.TabIndex = 1;
            // 
            // BtnCancel
            // 
            this.BtnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(196)))), ((int)(((byte)(182)))));
            this.BtnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.ForeColor = System.Drawing.Color.Maroon;
            this.BtnCancel.IconChar = FontAwesome.Sharp.IconChar.Remove;
            this.BtnCancel.IconColor = System.Drawing.Color.DarkRed;
            this.BtnCancel.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.BtnCancel.IconSize = 24;
            this.BtnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnCancel.Location = new System.Drawing.Point(238, 89);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(110, 30);
            this.BtnCancel.TabIndex = 11;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = false;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnOkay
            // 
            this.BtnOkay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.BtnOkay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnOkay.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnOkay.ForeColor = System.Drawing.Color.White;
            this.BtnOkay.IconChar = FontAwesome.Sharp.IconChar.UserCheck;
            this.BtnOkay.IconColor = System.Drawing.Color.White;
            this.BtnOkay.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.BtnOkay.IconSize = 24;
            this.BtnOkay.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnOkay.Location = new System.Drawing.Point(124, 89);
            this.BtnOkay.Name = "BtnOkay";
            this.BtnOkay.Size = new System.Drawing.Size(110, 30);
            this.BtnOkay.TabIndex = 10;
            this.BtnOkay.Text = "Login";
            this.BtnOkay.UseVisualStyleBackColor = false;
            this.BtnOkay.Click += new System.EventHandler(this.BtnOkay_Click);
            // 
            // FrmAdminInputBox
            // 
            this.AcceptButton = this.BtnOkay;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CancelButton = this.BtnCancel;
            this.ClientSize = new System.Drawing.Size(386, 130);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOkay);
            this.Controls.Add(this.TxtPassword);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmAdminInputBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmAdminInputBox";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox TxtPassword;
        public FontAwesome.Sharp.IconButton BtnCancel;
        public FontAwesome.Sharp.IconButton BtnOkay;
    }
}
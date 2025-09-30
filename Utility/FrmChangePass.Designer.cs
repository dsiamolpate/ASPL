namespace ASPL.Utility
{
    partial class FrmChangePass
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblNmPass = new System.Windows.Forms.Label();
            this.txtnewPass = new System.Windows.Forms.TextBox();
            this.lblRetypPass = new System.Windows.Forms.Label();
            this.txtRetypPass = new System.Windows.Forms.TextBox();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 14);
            this.label1.TabIndex = 38;
            this.label1.Text = "Current Password :";
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.White;
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(174, 13);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(171, 22);
            this.txtPassword.TabIndex = 42;
            this.txtPassword.Enter += new System.EventHandler(this.txtPassword_Enter);
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            // 
            // lblNmPass
            // 
            this.lblNmPass.AutoSize = true;
            this.lblNmPass.BackColor = System.Drawing.Color.Transparent;
            this.lblNmPass.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNmPass.Location = new System.Drawing.Point(39, 57);
            this.lblNmPass.Name = "lblNmPass";
            this.lblNmPass.Size = new System.Drawing.Size(115, 14);
            this.lblNmPass.TabIndex = 43;
            this.lblNmPass.Text = "New Password :";
            // 
            // txtnewPass
            // 
            this.txtnewPass.BackColor = System.Drawing.SystemColors.Window;
            this.txtnewPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtnewPass.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtnewPass.Location = new System.Drawing.Point(174, 54);
            this.txtnewPass.Name = "txtnewPass";
            this.txtnewPass.PasswordChar = '*';
            this.txtnewPass.Size = new System.Drawing.Size(171, 22);
            this.txtnewPass.TabIndex = 44;
            this.txtnewPass.Enter += new System.EventHandler(this.txtnewPass_Enter);
            this.txtnewPass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtnewPass_KeyDown);
            this.txtnewPass.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtnewPass_KeyPress);
            // 
            // lblRetypPass
            // 
            this.lblRetypPass.AutoSize = true;
            this.lblRetypPass.BackColor = System.Drawing.Color.Transparent;
            this.lblRetypPass.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRetypPass.Location = new System.Drawing.Point(23, 98);
            this.lblRetypPass.Name = "lblRetypPass";
            this.lblRetypPass.Size = new System.Drawing.Size(131, 14);
            this.lblRetypPass.TabIndex = 45;
            this.lblRetypPass.Text = "Retype Password :";
            // 
            // txtRetypPass
            // 
            this.txtRetypPass.BackColor = System.Drawing.SystemColors.Window;
            this.txtRetypPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRetypPass.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRetypPass.Location = new System.Drawing.Point(174, 95);
            this.txtRetypPass.Name = "txtRetypPass";
            this.txtRetypPass.PasswordChar = '*';
            this.txtRetypPass.Size = new System.Drawing.Size(171, 22);
            this.txtRetypPass.TabIndex = 46;
            this.txtRetypPass.Enter += new System.EventHandler(this.txtRetypPass_Enter);
            this.txtRetypPass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRetypPass_KeyDown);
            this.txtRetypPass.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRetypPass_KeyPress);
            // 
            // BtnSave
            // 
            this.BtnSave.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BtnSave.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Location = new System.Drawing.Point(140, 138);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(98, 25);
            this.BtnSave.TabIndex = 47;
            this.BtnSave.Text = "SAVE";
            this.BtnSave.UseVisualStyleBackColor = false;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BtnExit.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Location = new System.Drawing.Point(248, 138);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(98, 25);
            this.BtnExit.TabIndex = 48;
            this.BtnExit.Text = "EXIT";
            this.BtnExit.UseVisualStyleBackColor = false;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // FrmChangePass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(366, 198);
            this.ControlBox = false;
            this.Controls.Add(this.BtnExit);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.txtRetypPass);
            this.Controls.Add(this.lblRetypPass);
            this.Controls.Add(this.txtnewPass);
            this.Controls.Add(this.lblNmPass);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmChangePass";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Change Password";
            this.Load += new System.EventHandler(this.FrmChangePass_Load);
            this.Shown += new System.EventHandler(this.FrmChangePass_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblNmPass;
        private System.Windows.Forms.TextBox txtnewPass;
        private System.Windows.Forms.Label lblRetypPass;
        private System.Windows.Forms.TextBox txtRetypPass;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnExit;
    }
}
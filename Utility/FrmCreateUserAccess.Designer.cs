namespace ASPL.Utility
{
    partial class FrmCreateUserAccess
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
            this.lblUserNm = new System.Windows.Forms.Label();
            this.combUserlist = new System.Windows.Forms.ComboBox();
            this.TxtUsercd = new System.Windows.Forms.TextBox();
            this.lblPass = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblRetypPass = new System.Windows.Forms.Label();
            this.txtRepassword = new System.Windows.Forms.TextBox();
            this.BtnAddNew = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.BtnExit = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblUserNm
            // 
            this.lblUserNm.AutoSize = true;
            this.lblUserNm.BackColor = System.Drawing.Color.Transparent;
            this.lblUserNm.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserNm.Location = new System.Drawing.Point(48, 19);
            this.lblUserNm.Name = "lblUserNm";
            this.lblUserNm.Size = new System.Drawing.Size(89, 14);
            this.lblUserNm.TabIndex = 37;
            this.lblUserNm.Text = "User Name :";
            // 
            // combUserlist
            // 
            this.combUserlist.BackColor = System.Drawing.Color.White;
            this.combUserlist.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combUserlist.FormattingEnabled = true;
            this.combUserlist.Location = new System.Drawing.Point(144, 16);
            this.combUserlist.Name = "combUserlist";
            this.combUserlist.Size = new System.Drawing.Size(158, 22);
            this.combUserlist.TabIndex = 38;
            this.combUserlist.SelectedIndexChanged += new System.EventHandler(this.combUserlist_SelectedIndexChanged);
            this.combUserlist.Enter += new System.EventHandler(this.combUserlist_Enter);
            this.combUserlist.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.combUserlist_KeyPress);
            // 
            // TxtUsercd
            // 
            this.TxtUsercd.BackColor = System.Drawing.Color.White;
            this.TxtUsercd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtUsercd.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtUsercd.Location = new System.Drawing.Point(306, 16);
            this.TxtUsercd.Multiline = true;
            this.TxtUsercd.Name = "TxtUsercd";
            this.TxtUsercd.ReadOnly = true;
            this.TxtUsercd.Size = new System.Drawing.Size(106, 22);
            this.TxtUsercd.TabIndex = 39;
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.BackColor = System.Drawing.Color.Transparent;
            this.lblPass.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPass.Location = new System.Drawing.Point(55, 60);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(81, 14);
            this.lblPass.TabIndex = 40;
            this.lblPass.Text = "Password :";
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.White;
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(144, 56);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(158, 22);
            this.txtPassword.TabIndex = 41;
            this.txtPassword.Enter += new System.EventHandler(this.txtPassword_Enter);
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            this.txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPassword_KeyPress);
            // 
            // lblRetypPass
            // 
            this.lblRetypPass.AutoSize = true;
            this.lblRetypPass.BackColor = System.Drawing.Color.Transparent;
            this.lblRetypPass.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRetypPass.Location = new System.Drawing.Point(5, 98);
            this.lblRetypPass.Name = "lblRetypPass";
            this.lblRetypPass.Size = new System.Drawing.Size(131, 14);
            this.lblRetypPass.TabIndex = 42;
            this.lblRetypPass.Text = "Retype Password :";
            // 
            // txtRepassword
            // 
            this.txtRepassword.BackColor = System.Drawing.Color.White;
            this.txtRepassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRepassword.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepassword.Location = new System.Drawing.Point(144, 95);
            this.txtRepassword.Name = "txtRepassword";
            this.txtRepassword.PasswordChar = '*';
            this.txtRepassword.Size = new System.Drawing.Size(158, 22);
            this.txtRepassword.TabIndex = 43;
            this.txtRepassword.Enter += new System.EventHandler(this.txtRepassword_Enter);
            this.txtRepassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRepassword_KeyPress);
            // 
            // BtnAddNew
            // 
            this.BtnAddNew.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BtnAddNew.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.BtnAddNew.Location = new System.Drawing.Point(5, 126);
            this.BtnAddNew.Name = "BtnAddNew";
            this.BtnAddNew.Size = new System.Drawing.Size(90, 25);
            this.BtnAddNew.TabIndex = 44;
            this.BtnAddNew.Text = "ADD NEW";
            this.BtnAddNew.UseVisualStyleBackColor = false;
            this.BtnAddNew.Click += new System.EventHandler(this.BtnAddNew_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BtnSave.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.BtnSave.Location = new System.Drawing.Point(109, 127);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(90, 25);
            this.BtnSave.TabIndex = 45;
            this.BtnSave.Text = "SAVE";
            this.BtnSave.UseVisualStyleBackColor = false;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BtnDelete.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.BtnDelete.Location = new System.Drawing.Point(213, 127);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(90, 25);
            this.BtnDelete.TabIndex = 46;
            this.BtnDelete.Text = "DELETE";
            this.BtnDelete.UseVisualStyleBackColor = false;
            // 
            // BtnExit
            // 
            this.BtnExit.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BtnExit.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.BtnExit.Location = new System.Drawing.Point(315, 127);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(90, 25);
            this.BtnExit.TabIndex = 47;
            this.BtnExit.Text = "EXIT";
            this.BtnExit.UseVisualStyleBackColor = false;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(109, 167);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 25);
            this.button1.TabIndex = 489;
            this.button1.Text = "SET ACCESS";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmCreateUserAccess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(418, 233);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.BtnExit);
            this.Controls.Add(this.BtnDelete);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.BtnAddNew);
            this.Controls.Add(this.txtRepassword);
            this.Controls.Add(this.lblRetypPass);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPass);
            this.Controls.Add(this.TxtUsercd);
            this.Controls.Add(this.combUserlist);
            this.Controls.Add(this.lblUserNm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "FrmCreateUserAccess";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create New User";
            this.Load += new System.EventHandler(this.FrmCreateUserAccess_Load);
            this.Shown += new System.EventHandler(this.FrmCreateUserAccess_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUserNm;
        private System.Windows.Forms.ComboBox combUserlist;
        private System.Windows.Forms.TextBox TxtUsercd;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblRetypPass;
        private System.Windows.Forms.TextBox txtRepassword;
        private System.Windows.Forms.Button BtnAddNew;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnDelete;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.Button button1;
    }
}
namespace ASPL.Utility
{
    partial class FrmCreateNewDataBase
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
            this.txtOlddatabase = new System.Windows.Forms.TextBox();
            this.txtNewDatabase = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.BtnExit = new System.Windows.Forms.Button();
            this.BtnCreareDB = new System.Windows.Forms.Button();
            this.txtusernm = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtOlddatabase
            // 
            this.txtOlddatabase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOlddatabase.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOlddatabase.Location = new System.Drawing.Point(180, 9);
            this.txtOlddatabase.Name = "txtOlddatabase";
            this.txtOlddatabase.Size = new System.Drawing.Size(179, 22);
            this.txtOlddatabase.TabIndex = 0;
            this.txtOlddatabase.Enter += new System.EventHandler(this.txtOlddatabase_Enter);
            this.txtOlddatabase.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOlddatabase_KeyPress);
            // 
            // txtNewDatabase
            // 
            this.txtNewDatabase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNewDatabase.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNewDatabase.Location = new System.Drawing.Point(180, 45);
            this.txtNewDatabase.MaxLength = 50;
            this.txtNewDatabase.Name = "txtNewDatabase";
            this.txtNewDatabase.Size = new System.Drawing.Size(179, 22);
            this.txtNewDatabase.TabIndex = 0;
            this.txtNewDatabase.Enter += new System.EventHandler(this.txtNewDatabase_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(23, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 14);
            this.label3.TabIndex = 407;
            this.label3.Text = "New Database Name :";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(22, 13);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(155, 14);
            this.label21.TabIndex = 406;
            this.label21.Text = "Old Databasae Name :";
            // 
            // BtnExit
            // 
            this.BtnExit.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.BtnExit.Location = new System.Drawing.Point(220, 157);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(100, 25);
            this.BtnExit.TabIndex = 410;
            this.BtnExit.Text = "EXIT";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnCreareDB
            // 
            this.BtnCreareDB.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.BtnCreareDB.Location = new System.Drawing.Point(91, 157);
            this.BtnCreareDB.Name = "BtnCreareDB";
            this.BtnCreareDB.Size = new System.Drawing.Size(100, 25);
            this.BtnCreareDB.TabIndex = 409;
            this.BtnCreareDB.Text = "CREATE DB";
            this.BtnCreareDB.UseVisualStyleBackColor = true;
            this.BtnCreareDB.Click += new System.EventHandler(this.BtnCreareDB_Click);
            // 
            // txtusernm
            // 
            this.txtusernm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtusernm.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtusernm.Location = new System.Drawing.Point(180, 81);
            this.txtusernm.Name = "txtusernm";
            this.txtusernm.Size = new System.Drawing.Size(179, 22);
            this.txtusernm.TabIndex = 411;
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(180, 117);
            this.txtPassword.MaxLength = 50;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(179, 22);
            this.txtPassword.TabIndex = 412;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(23, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 14);
            this.label1.TabIndex = 414;
            this.label1.Text = "Password :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(22, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 14);
            this.label2.TabIndex = 413;
            this.label2.Text = "User Name :";
            // 
            // FrmCreateNewDataBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(439, 210);
            this.ControlBox = false;
            this.Controls.Add(this.txtusernm);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BtnExit);
            this.Controls.Add(this.BtnCreareDB);
            this.Controls.Add(this.txtOlddatabase);
            this.Controls.Add(this.txtNewDatabase);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label21);
            this.Name = "FrmCreateNewDataBase";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.FrmCreateNewDataBase_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtOlddatabase;
        private System.Windows.Forms.TextBox txtNewDatabase;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.Button BtnCreareDB;
        private System.Windows.Forms.TextBox txtusernm;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
namespace ASPL.Utility
{
    partial class FrmCompanySelect
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
            this.lstCompaName = new System.Windows.Forms.ListBox();
            this.lstFinanYear = new System.Windows.Forms.ListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnBack = new System.Windows.Forms.Button();
            this.BtnConnect = new System.Windows.Forms.Button();
            this.BtnNewUser = new System.Windows.Forms.Button();
            this.BtnDetail = new System.Windows.Forms.Button();
            this.txtpath = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lstCompaName
            // 
            this.lstCompaName.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstCompaName.FormattingEnabled = true;
            this.lstCompaName.ItemHeight = 16;
            this.lstCompaName.Location = new System.Drawing.Point(4, 81);
            this.lstCompaName.Name = "lstCompaName";
            this.lstCompaName.Size = new System.Drawing.Size(323, 164);
            this.lstCompaName.TabIndex = 0;
            this.lstCompaName.SelectedIndexChanged += new System.EventHandler(this.lstCompaName_SelectedIndexChanged);
            // 
            // lstFinanYear
            // 
            this.lstFinanYear.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstFinanYear.FormattingEnabled = true;
            this.lstFinanYear.ItemHeight = 16;
            this.lstFinanYear.Location = new System.Drawing.Point(333, 83);
            this.lstFinanYear.Name = "lstFinanYear";
            this.lstFinanYear.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lstFinanYear.Size = new System.Drawing.Size(171, 164);
            this.lstFinanYear.TabIndex = 1;
            this.lstFinanYear.SelectedIndexChanged += new System.EventHandler(this.lstFinanYear_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(12, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(183, 26);
            this.label8.TabIndex = 448;
            this.label8.Text = "Company Selection";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 16);
            this.label2.TabIndex = 449;
            this.label2.Text = "Company Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(336, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 16);
            this.label1.TabIndex = 450;
            this.label1.Text = "Financial Year";
            // 
            // BtnBack
            // 
            this.BtnBack.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.BtnBack.Location = new System.Drawing.Point(378, 272);
            this.BtnBack.Name = "BtnBack";
            this.BtnBack.Size = new System.Drawing.Size(106, 25);
            this.BtnBack.TabIndex = 454;
            this.BtnBack.Text = "BACK";
            this.BtnBack.UseVisualStyleBackColor = true;
            this.BtnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // BtnConnect
            // 
            this.BtnConnect.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.BtnConnect.Location = new System.Drawing.Point(3, 272);
            this.BtnConnect.Name = "BtnConnect";
            this.BtnConnect.Size = new System.Drawing.Size(106, 25);
            this.BtnConnect.TabIndex = 453;
            this.BtnConnect.Text = "CONNECT";
            this.BtnConnect.UseVisualStyleBackColor = true;
            this.BtnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // BtnNewUser
            // 
            this.BtnNewUser.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.BtnNewUser.Location = new System.Drawing.Point(221, 272);
            this.BtnNewUser.Name = "BtnNewUser";
            this.BtnNewUser.Size = new System.Drawing.Size(106, 25);
            this.BtnNewUser.TabIndex = 455;
            this.BtnNewUser.Text = "NEW USER";
            this.BtnNewUser.UseVisualStyleBackColor = true;
            this.BtnNewUser.Click += new System.EventHandler(this.BtnNewUser_Click);
            // 
            // BtnDetail
            // 
            this.BtnDetail.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.BtnDetail.Location = new System.Drawing.Point(112, 272);
            this.BtnDetail.Name = "BtnDetail";
            this.BtnDetail.Size = new System.Drawing.Size(106, 25);
            this.BtnDetail.TabIndex = 456;
            this.BtnDetail.Text = "DETAILS";
            this.BtnDetail.UseVisualStyleBackColor = true;
            this.BtnDetail.Click += new System.EventHandler(this.BtnDetail_Click);
            // 
            // txtpath
            // 
            this.txtpath.Location = new System.Drawing.Point(314, 9);
            this.txtpath.Name = "txtpath";
            this.txtpath.Size = new System.Drawing.Size(170, 20);
            this.txtpath.TabIndex = 457;
            // 
            // FrmCompanySelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(509, 335);
            this.ControlBox = false;
            this.Controls.Add(this.txtpath);
            this.Controls.Add(this.BtnDetail);
            this.Controls.Add(this.BtnNewUser);
            this.Controls.Add(this.BtnBack);
            this.Controls.Add(this.BtnConnect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lstFinanYear);
            this.Controls.Add(this.lstCompaName);
            this.MaximizeBox = false;
            this.Name = "FrmCompanySelect";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FrmCompanySelect_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstCompaName;
        private System.Windows.Forms.ListBox lstFinanYear;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnBack;
        private System.Windows.Forms.Button BtnConnect;
        private System.Windows.Forms.Button BtnNewUser;
        private System.Windows.Forms.Button BtnDetail;
        private System.Windows.Forms.TextBox txtpath;
    }
}
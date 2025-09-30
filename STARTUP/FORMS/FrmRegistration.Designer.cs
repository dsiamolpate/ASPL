namespace ASPL.STARTUP.FORMS
{
    partial class FrmRegistration
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
            this.LblApplicationName = new System.Windows.Forms.Label();
            this.LblAuthorName = new System.Windows.Forms.Label();
            this.LblCopyRights = new System.Windows.Forms.Label();
            this.LblCopyDate = new System.Windows.Forms.Label();
            this.txtDealerName = new System.Windows.Forms.TextBox();
            this.TxtCopyRights = new System.Windows.Forms.TextBox();
            this.DTPickerCopyDate = new System.Windows.Forms.DateTimePicker();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnExit = new System.Windows.Forms.Button();
            this.LblStar1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listboxAppName = new System.Windows.Forms.CheckedListBox();
            this.txtContactNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LblApplicationName
            // 
            this.LblApplicationName.AutoSize = true;
            this.LblApplicationName.BackColor = System.Drawing.Color.Transparent;
            this.LblApplicationName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblApplicationName.Location = new System.Drawing.Point(23, 68);
            this.LblApplicationName.Name = "LblApplicationName";
            this.LblApplicationName.Size = new System.Drawing.Size(131, 14);
            this.LblApplicationName.TabIndex = 0;
            this.LblApplicationName.Text = "Application Name :";
            // 
            // LblAuthorName
            // 
            this.LblAuthorName.AutoSize = true;
            this.LblAuthorName.BackColor = System.Drawing.Color.Transparent;
            this.LblAuthorName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblAuthorName.Location = new System.Drawing.Point(50, 181);
            this.LblAuthorName.Name = "LblAuthorName";
            this.LblAuthorName.Size = new System.Drawing.Size(102, 14);
            this.LblAuthorName.TabIndex = 1;
            this.LblAuthorName.Text = "Dealer Name :";
            // 
            // LblCopyRights
            // 
            this.LblCopyRights.AutoSize = true;
            this.LblCopyRights.BackColor = System.Drawing.Color.Transparent;
            this.LblCopyRights.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCopyRights.Location = new System.Drawing.Point(58, 142);
            this.LblCopyRights.Name = "LblCopyRights";
            this.LblCopyRights.Size = new System.Drawing.Size(94, 14);
            this.LblCopyRights.TabIndex = 3;
            this.LblCopyRights.Text = "Copy Rights :";
            // 
            // LblCopyDate
            // 
            this.LblCopyDate.AutoSize = true;
            this.LblCopyDate.BackColor = System.Drawing.Color.Transparent;
            this.LblCopyDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCopyDate.Location = new System.Drawing.Point(71, 25);
            this.LblCopyDate.Name = "LblCopyDate";
            this.LblCopyDate.Size = new System.Drawing.Size(84, 14);
            this.LblCopyDate.TabIndex = 6;
            this.LblCopyDate.Text = "Copy Date :";
            // 
            // txtDealerName
            // 
            this.txtDealerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDealerName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDealerName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDealerName.Location = new System.Drawing.Point(170, 178);
            this.txtDealerName.Name = "txtDealerName";
            this.txtDealerName.Size = new System.Drawing.Size(332, 22);
            this.txtDealerName.TabIndex = 2;
            this.txtDealerName.Enter += new System.EventHandler(this.txtDealerName_Enter);
            this.txtDealerName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDealerName_KeyPress);
            this.txtDealerName.Leave += new System.EventHandler(this.txtDealerName_Leave);
            // 
            // TxtCopyRights
            // 
            this.TxtCopyRights.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCopyRights.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtCopyRights.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCopyRights.Location = new System.Drawing.Point(170, 140);
            this.TxtCopyRights.Name = "TxtCopyRights";
            this.TxtCopyRights.Size = new System.Drawing.Size(332, 22);
            this.TxtCopyRights.TabIndex = 1;
            this.TxtCopyRights.Enter += new System.EventHandler(this.TxtCopyRights_Enter);
            this.TxtCopyRights.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtCopyRights_KeyPress);
            this.TxtCopyRights.Leave += new System.EventHandler(this.TxtCopyRights_Leave);
            // 
            // DTPickerCopyDate
            // 
            this.DTPickerCopyDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPickerCopyDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTPickerCopyDate.Location = new System.Drawing.Point(171, 20);
            this.DTPickerCopyDate.Name = "DTPickerCopyDate";
            this.DTPickerCopyDate.Size = new System.Drawing.Size(106, 22);
            this.DTPickerCopyDate.TabIndex = 17;
            // 
            // BtnSave
            // 
            this.BtnSave.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BtnSave.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Location = new System.Drawing.Point(318, 276);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(85, 25);
            this.BtnSave.TabIndex = 18;
            this.BtnSave.Text = "&Save";
            this.BtnSave.UseVisualStyleBackColor = false;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BtnExit.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Location = new System.Drawing.Point(422, 276);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(85, 25);
            this.BtnExit.TabIndex = 19;
            this.BtnExit.Text = "&Exit";
            this.BtnExit.UseVisualStyleBackColor = false;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // LblStar1
            // 
            this.LblStar1.AutoSize = true;
            this.LblStar1.BackColor = System.Drawing.Color.Transparent;
            this.LblStar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblStar1.ForeColor = System.Drawing.Color.Red;
            this.LblStar1.Location = new System.Drawing.Point(504, 138);
            this.LblStar1.Name = "LblStar1";
            this.LblStar1.Size = new System.Drawing.Size(13, 15);
            this.LblStar1.TabIndex = 22;
            this.LblStar1.Text = "*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(504, 177);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 15);
            this.label1.TabIndex = 23;
            this.label1.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(504, 212);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 15);
            this.label2.TabIndex = 24;
            this.label2.Text = "*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(504, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 15);
            this.label3.TabIndex = 25;
            this.label3.Text = "*";
            // 
            // listboxAppName
            // 
            this.listboxAppName.FormattingEnabled = true;
            this.listboxAppName.Items.AddRange(new object[] {
            "LODGE",
            "RESTAURANT",
            "MATERIAL MANAGEMENT",
            "ACCOUNTS"});
            this.listboxAppName.Location = new System.Drawing.Point(170, 59);
            this.listboxAppName.Name = "listboxAppName";
            this.listboxAppName.Size = new System.Drawing.Size(156, 64);
            this.listboxAppName.TabIndex = 0;
            this.listboxAppName.Enter += new System.EventHandler(this.listboxAppName_Enter);
            this.listboxAppName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.listboxAppName_KeyPress);
            this.listboxAppName.Leave += new System.EventHandler(this.listboxAppName_Leave);
            // 
            // txtContactNo
            // 
            this.txtContactNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtContactNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtContactNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContactNo.Location = new System.Drawing.Point(170, 215);
            this.txtContactNo.Name = "txtContactNo";
            this.txtContactNo.Size = new System.Drawing.Size(170, 22);
            this.txtContactNo.TabIndex = 3;
            this.txtContactNo.Enter += new System.EventHandler(this.txtContactNo_Enter);
            this.txtContactNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtContactNo_KeyPress);
            this.txtContactNo.Leave += new System.EventHandler(this.txtContactNo_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(60, 218);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 14);
            this.label4.TabIndex = 26;
            this.label4.Text = "Contact No. :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(504, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 15);
            this.label5.TabIndex = 28;
            this.label5.Text = "*";
            // 
            // FrmRegistration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(527, 341);
            this.ControlBox = false;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtContactNo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.listboxAppName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LblStar1);
            this.Controls.Add(this.BtnExit);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.DTPickerCopyDate);
            this.Controls.Add(this.TxtCopyRights);
            this.Controls.Add(this.txtDealerName);
            this.Controls.Add(this.LblCopyDate);
            this.Controls.Add(this.LblCopyRights);
            this.Controls.Add(this.LblAuthorName);
            this.Controls.Add(this.LblApplicationName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(20, 20);
            this.MaximizeBox = false;
            this.Name = "FrmRegistration";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registration";
            this.Load += new System.EventHandler(this.FrmRegistration_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmRegistration_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LblApplicationName;
        private System.Windows.Forms.Label LblAuthorName;
        private System.Windows.Forms.Label LblCopyRights;
        private System.Windows.Forms.Label LblCopyDate;
        private System.Windows.Forms.TextBox txtDealerName;
        private System.Windows.Forms.TextBox TxtCopyRights;
        private System.Windows.Forms.DateTimePicker DTPickerCopyDate;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.Label LblStar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckedListBox listboxAppName;
        private System.Windows.Forms.TextBox txtContactNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}
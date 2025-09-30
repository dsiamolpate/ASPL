namespace ASPL.STARTUP.FORMS
{
    partial class FrmUpdateRegister
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
            this.txtotpNo = new System.Windows.Forms.TextBox();
            this.LblRegistrationNo = new System.Windows.Forms.Label();
            this.BtnUpdate = new System.Windows.Forms.Button();
            this.BtnExit = new System.Windows.Forms.Button();
            this.linklabelRegenekey = new System.Windows.Forms.LinkLabel();
            this.txtValidityDays = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtotpNo
            // 
            this.txtotpNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtotpNo.Location = new System.Drawing.Point(154, 19);
            this.txtotpNo.MaxLength = 10;
            this.txtotpNo.Name = "txtotpNo";
            this.txtotpNo.Size = new System.Drawing.Size(117, 22);
            this.txtotpNo.TabIndex = 0;
            this.txtotpNo.Enter += new System.EventHandler(this.TxtRegistrationKey_Enter);
            this.txtotpNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtotpNo_KeyPress);
            this.txtotpNo.Leave += new System.EventHandler(this.txtotpNo_Leave);
            // 
            // LblRegistrationNo
            // 
            this.LblRegistrationNo.AutoSize = true;
            this.LblRegistrationNo.BackColor = System.Drawing.Color.Transparent;
            this.LblRegistrationNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblRegistrationNo.Location = new System.Drawing.Point(34, 22);
            this.LblRegistrationNo.Name = "LblRegistrationNo";
            this.LblRegistrationNo.Size = new System.Drawing.Size(105, 14);
            this.LblRegistrationNo.TabIndex = 0;
            this.LblRegistrationNo.Text = "Enter OTP No :";
            // 
            // BtnUpdate
            // 
            this.BtnUpdate.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BtnUpdate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnUpdate.Location = new System.Drawing.Point(53, 101);
            this.BtnUpdate.Name = "BtnUpdate";
            this.BtnUpdate.Size = new System.Drawing.Size(82, 25);
            this.BtnUpdate.TabIndex = 4;
            this.BtnUpdate.Text = "Save";
            this.BtnUpdate.UseVisualStyleBackColor = false;
            this.BtnUpdate.Click += new System.EventHandler(this.BtnUpdate_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BtnExit.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Location = new System.Drawing.Point(154, 101);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(82, 25);
            this.BtnExit.TabIndex = 5;
            this.BtnExit.Text = "E&xit";
            this.BtnExit.UseVisualStyleBackColor = false;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // linklabelRegenekey
            // 
            this.linklabelRegenekey.ActiveLinkColor = System.Drawing.Color.Maroon;
            this.linklabelRegenekey.AutoSize = true;
            this.linklabelRegenekey.BackColor = System.Drawing.Color.Transparent;
            this.linklabelRegenekey.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linklabelRegenekey.LinkColor = System.Drawing.Color.OrangeRed;
            this.linklabelRegenekey.Location = new System.Drawing.Point(160, 79);
            this.linklabelRegenekey.Name = "linklabelRegenekey";
            this.linklabelRegenekey.Size = new System.Drawing.Size(112, 14);
            this.linklabelRegenekey.TabIndex = 125;
            this.linklabelRegenekey.TabStop = true;
            this.linklabelRegenekey.Text = "Regenerate Key";
            this.linklabelRegenekey.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklabelRegenekey_LinkClicked);
            // 
            // txtValidityDays
            // 
            this.txtValidityDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtValidityDays.Location = new System.Drawing.Point(154, 50);
            this.txtValidityDays.MaxLength = 10;
            this.txtValidityDays.Name = "txtValidityDays";
            this.txtValidityDays.Size = new System.Drawing.Size(117, 22);
            this.txtValidityDays.TabIndex = 126;
            this.txtValidityDays.Enter += new System.EventHandler(this.txtValidityDays_Enter);
            this.txtValidityDays.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValidityDays_KeyPress);
            this.txtValidityDays.Leave += new System.EventHandler(this.txtValidityDays_Leave);
            // 
            // FrmUpdateRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(303, 142);
            this.ControlBox = false;
            this.Controls.Add(this.txtValidityDays);
            this.Controls.Add(this.linklabelRegenekey);
            this.Controls.Add(this.BtnExit);
            this.Controls.Add(this.BtnUpdate);
            this.Controls.Add(this.LblRegistrationNo);
            this.Controls.Add(this.txtotpNo);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "FrmUpdateRegister";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update Now";
            this.Load += new System.EventHandler(this.FrmUpdateRegister_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmUpdateRegister_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtotpNo;
        private System.Windows.Forms.Label LblRegistrationNo;
        private System.Windows.Forms.Button BtnUpdate;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.LinkLabel linklabelRegenekey;
        private System.Windows.Forms.TextBox txtValidityDays;
    }
}
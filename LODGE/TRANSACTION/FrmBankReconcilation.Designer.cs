namespace ASPL.LODGE.TRANSCTION
{
    partial class FrmBankReconcilation
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.BtnReceipt = new System.Windows.Forms.Button();
            this.BtnAll = new System.Windows.Forms.Button();
            this.BtnPayment = new System.Windows.Forms.Button();
            this.txtBookCodeNM = new System.Windows.Forms.TextBox();
            this.txtBookCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.label20 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtgridBankReconcilation = new System.Windows.Forms.DataGridView();
            this.CheckIssued = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCheckDeposited = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBalAsPerBankBook = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBalanceAsPerStatement = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNarration = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnUnclear = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dtgridBankReconcilation)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnReceipt
            // 
            this.BtnReceipt.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.BtnReceipt.Location = new System.Drawing.Point(466, 61);
            this.BtnReceipt.Name = "BtnReceipt";
            this.BtnReceipt.Size = new System.Drawing.Size(86, 22);
            this.BtnReceipt.TabIndex = 1;
            this.BtnReceipt.Text = "Receipt";
            this.BtnReceipt.UseVisualStyleBackColor = true;
            this.BtnReceipt.Click += new System.EventHandler(this.BtnReceipt_Click);
            // 
            // BtnAll
            // 
            this.BtnAll.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.BtnAll.Location = new System.Drawing.Point(650, 61);
            this.BtnAll.Name = "BtnAll";
            this.BtnAll.Size = new System.Drawing.Size(54, 22);
            this.BtnAll.TabIndex = 3;
            this.BtnAll.Text = "All";
            this.BtnAll.UseVisualStyleBackColor = true;
            this.BtnAll.Click += new System.EventHandler(this.BtnAll_Click);
            // 
            // BtnPayment
            // 
            this.BtnPayment.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.BtnPayment.Location = new System.Drawing.Point(558, 61);
            this.BtnPayment.Name = "BtnPayment";
            this.BtnPayment.Size = new System.Drawing.Size(86, 22);
            this.BtnPayment.TabIndex = 2;
            this.BtnPayment.Text = "Payment";
            this.BtnPayment.UseVisualStyleBackColor = true;
            this.BtnPayment.Click += new System.EventHandler(this.BtnPayment_Click);
            // 
            // txtBookCodeNM
            // 
            this.txtBookCodeNM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBookCodeNM.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBookCodeNM.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBookCodeNM.Location = new System.Drawing.Point(212, 61);
            this.txtBookCodeNM.Name = "txtBookCodeNM";
            this.txtBookCodeNM.ReadOnly = true;
            this.txtBookCodeNM.Size = new System.Drawing.Size(241, 22);
            this.txtBookCodeNM.TabIndex = 598;
            // 
            // txtBookCode
            // 
            this.txtBookCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBookCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBookCode.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBookCode.Location = new System.Drawing.Point(149, 61);
            this.txtBookCode.Name = "txtBookCode";
            this.txtBookCode.Size = new System.Drawing.Size(57, 22);
            this.txtBookCode.TabIndex = 0;
            this.txtBookCode.Enter += new System.EventHandler(this.txtBookCode_Enter);
            this.txtBookCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBookCode_KeyDown);
            this.txtBookCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBookCode_KeyPress);
            this.txtBookCode.Leave += new System.EventHandler(this.txtBookCode_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(59, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 14);
            this.label2.TabIndex = 597;
            this.label2.Text = "Book Code :";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(148, 7);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(115, 20);
            this.dtpFromDate.TabIndex = 594;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(59, 9);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(85, 14);
            this.label20.TabIndex = 596;
            this.label20.Text = "From Date :";
            // 
            // dtpToDate
            // 
            this.dtpToDate.CustomFormat = "dd/MM/yyyy";
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(148, 34);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(115, 20);
            this.dtpToDate.TabIndex = 602;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(77, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 14);
            this.label1.TabIndex = 603;
            this.label1.Text = "To Date :";
            // 
            // dtgridBankReconcilation
            // 
            this.dtgridBankReconcilation.AllowUserToAddRows = false;
            this.dtgridBankReconcilation.AllowUserToResizeColumns = false;
            this.dtgridBankReconcilation.AllowUserToResizeRows = false;
            this.dtgridBankReconcilation.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgridBankReconcilation.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgridBankReconcilation.DefaultCellStyle = dataGridViewCellStyle6;
            this.dtgridBankReconcilation.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dtgridBankReconcilation.EnableHeadersVisualStyles = false;
            this.dtgridBankReconcilation.Location = new System.Drawing.Point(23, 89);
            this.dtgridBankReconcilation.Margin = new System.Windows.Forms.Padding(2);
            this.dtgridBankReconcilation.Name = "dtgridBankReconcilation";
            this.dtgridBankReconcilation.RowHeadersVisible = false;
            this.dtgridBankReconcilation.RowTemplate.Height = 24;
            this.dtgridBankReconcilation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dtgridBankReconcilation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dtgridBankReconcilation.Size = new System.Drawing.Size(684, 263);
            this.dtgridBankReconcilation.TabIndex = 614;
            // 
            // CheckIssued
            // 
            this.CheckIssued.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CheckIssued.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.CheckIssued.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckIssued.Location = new System.Drawing.Point(214, 403);
            this.CheckIssued.Name = "CheckIssued";
            this.CheckIssued.Size = new System.Drawing.Size(133, 22);
            this.CheckIssued.TabIndex = 617;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(75, 405);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 14);
            this.label4.TabIndex = 618;
            this.label4.Text = "Add Check Issued :";
            // 
            // txtCheckDeposited
            // 
            this.txtCheckDeposited.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCheckDeposited.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCheckDeposited.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCheckDeposited.Location = new System.Drawing.Point(568, 375);
            this.txtCheckDeposited.Name = "txtCheckDeposited";
            this.txtCheckDeposited.Size = new System.Drawing.Size(133, 22);
            this.txtCheckDeposited.TabIndex = 619;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(403, 378);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(160, 14);
            this.label5.TabIndex = 620;
            this.label5.Text = "Less Check Deposited :";
            // 
            // txtBalAsPerBankBook
            // 
            this.txtBalAsPerBankBook.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBalAsPerBankBook.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBalAsPerBankBook.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBalAsPerBankBook.Location = new System.Drawing.Point(214, 373);
            this.txtBalAsPerBankBook.Name = "txtBalAsPerBankBook";
            this.txtBalAsPerBankBook.Size = new System.Drawing.Size(133, 22);
            this.txtBalAsPerBankBook.TabIndex = 621;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(21, 377);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(189, 14);
            this.label6.TabIndex = 622;
            this.label6.Text = "Balance As Per BankBook  :";
            // 
            // txtBalanceAsPerStatement
            // 
            this.txtBalanceAsPerStatement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBalanceAsPerStatement.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBalanceAsPerStatement.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBalanceAsPerStatement.Location = new System.Drawing.Point(568, 404);
            this.txtBalanceAsPerStatement.Name = "txtBalanceAsPerStatement";
            this.txtBalanceAsPerStatement.Size = new System.Drawing.Size(133, 22);
            this.txtBalanceAsPerStatement.TabIndex = 623;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(376, 406);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(187, 14);
            this.label7.TabIndex = 624;
            this.label7.Text = "Balance As Per Statement :";
            // 
            // txtNarration
            // 
            this.txtNarration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNarration.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNarration.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNarration.Location = new System.Drawing.Point(21, 446);
            this.txtNarration.Name = "txtNarration";
            this.txtNarration.Size = new System.Drawing.Size(683, 22);
            this.txtNarration.TabIndex = 625;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.DarkRed;
            this.label9.Location = new System.Drawing.Point(24, 425);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 14);
            this.label9.TabIndex = 626;
            this.label9.Text = "Narration :";
            // 
            // BtnCancel
            // 
            this.BtnCancel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Location = new System.Drawing.Point(614, 474);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(90, 25);
            this.BtnCancel.TabIndex = 630;
            this.BtnCancel.Text = "CANCEL";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(131, 474);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(90, 25);
            this.btnClear.TabIndex = 628;
            this.btnClear.Text = "CLEAR";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(513, 474);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 25);
            this.btnSave.TabIndex = 627;
            this.btnSave.Text = "SAVE";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnUnclear
            // 
            this.btnUnclear.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUnclear.Location = new System.Drawing.Point(25, 474);
            this.btnUnclear.Name = "btnUnclear";
            this.btnUnclear.Size = new System.Drawing.Size(90, 25);
            this.btnUnclear.TabIndex = 631;
            this.btnUnclear.Text = "UNCLEAR";
            this.btnUnclear.UseVisualStyleBackColor = true;
            this.btnUnclear.Click += new System.EventHandler(this.btnUnclear_Click);
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.Red;
            this.label22.Location = new System.Drawing.Point(455, 59);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(10, 10);
            this.label22.TabIndex = 632;
            this.label22.Text = "*";
            this.label22.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.label22);
            this.pnlMain.Controls.Add(this.btnUnclear);
            this.pnlMain.Controls.Add(this.BtnCancel);
            this.pnlMain.Controls.Add(this.btnClear);
            this.pnlMain.Controls.Add(this.btnSave);
            this.pnlMain.Controls.Add(this.txtNarration);
            this.pnlMain.Controls.Add(this.label9);
            this.pnlMain.Controls.Add(this.txtBalanceAsPerStatement);
            this.pnlMain.Controls.Add(this.label7);
            this.pnlMain.Controls.Add(this.txtBalAsPerBankBook);
            this.pnlMain.Controls.Add(this.label6);
            this.pnlMain.Controls.Add(this.txtCheckDeposited);
            this.pnlMain.Controls.Add(this.label5);
            this.pnlMain.Controls.Add(this.CheckIssued);
            this.pnlMain.Controls.Add(this.label4);
            this.pnlMain.Controls.Add(this.dtgridBankReconcilation);
            this.pnlMain.Controls.Add(this.dtpToDate);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.BtnReceipt);
            this.pnlMain.Controls.Add(this.BtnAll);
            this.pnlMain.Controls.Add(this.BtnPayment);
            this.pnlMain.Controls.Add(this.txtBookCodeNM);
            this.pnlMain.Controls.Add(this.txtBookCode);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.dtpFromDate);
            this.pnlMain.Controls.Add(this.label20);
            this.pnlMain.Location = new System.Drawing.Point(12, 12);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(735, 516);
            this.pnlMain.TabIndex = 633;
            // 
            // FrmBankReconcilation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(764, 545);
            this.ControlBox = false;
            this.Controls.Add(this.pnlMain);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmBankReconcilation";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bank Reconciliation";
            this.Load += new System.EventHandler(this.FrmBankReconcilation_Load);
            this.Shown += new System.EventHandler(this.FrmBankReconcilation_Shown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmBankReconcilation_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.dtgridBankReconcilation)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnReceipt;
        private System.Windows.Forms.Button BtnAll;
        private System.Windows.Forms.Button BtnPayment;
        private System.Windows.Forms.TextBox txtBookCodeNM;
        private System.Windows.Forms.TextBox txtBookCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dtgridBankReconcilation;
        private System.Windows.Forms.TextBox CheckIssued;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCheckDeposited;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBalAsPerBankBook;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBalanceAsPerStatement;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNarration;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnUnclear;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Panel pnlMain;
    }
}
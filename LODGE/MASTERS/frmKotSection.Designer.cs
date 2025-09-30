namespace ASPL.LODGE.MASTERS
{
    partial class frmKotSection
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label7 = new System.Windows.Forms.Label();
            this.txtsearch = new System.Windows.Forms.TextBox();
            this.txtcode = new System.Windows.Forms.TextBox();
            this.BtnExit = new System.Windows.Forms.Button();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.txtkotsection = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.DtgridKotSection = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.Chkshowdelet = new System.Windows.Forms.CheckBox();
            this.cmbPrintertype = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbPrint = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbprinternm = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSucessMsg = new System.Windows.Forms.Label();
            this.btnGreaterID = new System.Windows.Forms.Button();
            this.btnLessID = new System.Windows.Forms.Button();
            this.pnlMain = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.DtgridKotSection)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(336, 195);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 14);
            this.label7.TabIndex = 497;
            this.label7.Text = "Printer Name :";
            // 
            // txtsearch
            // 
            this.txtsearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtsearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtsearch.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsearch.Location = new System.Drawing.Point(147, 33);
            this.txtsearch.Name = "txtsearch";
            this.txtsearch.Size = new System.Drawing.Size(185, 22);
            this.txtsearch.TabIndex = 0;
            this.txtsearch.TextChanged += new System.EventHandler(this.txtsearch_TextChanged);
            this.txtsearch.Enter += new System.EventHandler(this.txtsearch_Enter);
            this.txtsearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtsearch_KeyDown);
            this.txtsearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtsearch_KeyPress);
            this.txtsearch.Leave += new System.EventHandler(this.txtsearch_Leave);
            // 
            // txtcode
            // 
            this.txtcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtcode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtcode.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcode.Location = new System.Drawing.Point(443, 94);
            this.txtcode.Name = "txtcode";
            this.txtcode.Size = new System.Drawing.Size(87, 22);
            this.txtcode.TabIndex = 495;
            // 
            // BtnExit
            // 
            this.BtnExit.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Location = new System.Drawing.Point(719, 484);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(75, 25);
            this.BtnExit.TabIndex = 494;
            this.BtnExit.Text = "EXIT";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Location = new System.Drawing.Point(637, 484);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(75, 25);
            this.BtnDelete.TabIndex = 493;
            this.BtnDelete.Text = "DELETE";
            this.BtnDelete.UseVisualStyleBackColor = true;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Location = new System.Drawing.Point(556, 484);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(75, 25);
            this.BtnSave.TabIndex = 492;
            this.BtnSave.Text = "SAVE";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnAdd
            // 
            this.BtnAdd.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BtnAdd.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAdd.Location = new System.Drawing.Point(471, 484);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(75, 25);
            this.BtnAdd.TabIndex = 491;
            this.BtnAdd.Text = "&ADD";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // txtkotsection
            // 
            this.txtkotsection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtkotsection.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtkotsection.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtkotsection.Location = new System.Drawing.Point(442, 127);
            this.txtkotsection.MaxLength = 50;
            this.txtkotsection.Name = "txtkotsection";
            this.txtkotsection.Size = new System.Drawing.Size(258, 22);
            this.txtkotsection.TabIndex = 1;
            this.txtkotsection.Enter += new System.EventHandler(this.txtkotsection_Enter);
            this.txtkotsection.Leave += new System.EventHandler(this.txtkotsection_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(348, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 14);
            this.label3.TabIndex = 490;
            this.label3.Text = " KotSection :";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(386, 97);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(53, 14);
            this.label21.TabIndex = 489;
            this.label21.Text = " Code :";
            // 
            // DtgridKotSection
            // 
            this.DtgridKotSection.AllowUserToAddRows = false;
            this.DtgridKotSection.AllowUserToDeleteRows = false;
            this.DtgridKotSection.AllowUserToResizeColumns = false;
            this.DtgridKotSection.AllowUserToResizeRows = false;
            this.DtgridKotSection.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DtgridKotSection.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.DtgridKotSection.ColumnHeadersHeight = 25;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DtgridKotSection.DefaultCellStyle = dataGridViewCellStyle10;
            this.DtgridKotSection.EnableHeadersVisualStyles = false;
            this.DtgridKotSection.Location = new System.Drawing.Point(17, 59);
            this.DtgridKotSection.MultiSelect = false;
            this.DtgridKotSection.Name = "DtgridKotSection";
            this.DtgridKotSection.ReadOnly = true;
            this.DtgridKotSection.RowHeadersVisible = false;
            this.DtgridKotSection.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DtgridKotSection.Size = new System.Drawing.Size(313, 450);
            this.DtgridKotSection.TabIndex = 488;
            this.DtgridKotSection.TabStop = false;
            this.DtgridKotSection.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DtgridKotSection_CellClick);
            this.DtgridKotSection.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DtgridKotSection_CellDoubleClick);
            this.DtgridKotSection.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DtgridKotSection_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(23, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 14);
            this.label1.TabIndex = 487;
            this.label1.Text = "Search by Name :";
            // 
            // Chkshowdelet
            // 
            this.Chkshowdelet.AutoSize = true;
            this.Chkshowdelet.BackColor = System.Drawing.Color.Transparent;
            this.Chkshowdelet.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Chkshowdelet.ForeColor = System.Drawing.Color.Black;
            this.Chkshowdelet.Location = new System.Drawing.Point(150, 13);
            this.Chkshowdelet.Name = "Chkshowdelet";
            this.Chkshowdelet.Size = new System.Drawing.Size(118, 18);
            this.Chkshowdelet.TabIndex = 486;
            this.Chkshowdelet.Text = "Show Deleted";
            this.Chkshowdelet.UseVisualStyleBackColor = false;
            this.Chkshowdelet.CheckedChanged += new System.EventHandler(this.Chkshowdelet_CheckedChanged);
            // 
            // cmbPrintertype
            // 
            this.cmbPrintertype.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPrintertype.FormattingEnabled = true;
            this.cmbPrintertype.Location = new System.Drawing.Point(442, 230);
            this.cmbPrintertype.Name = "cmbPrintertype";
            this.cmbPrintertype.Size = new System.Drawing.Size(131, 21);
            this.cmbPrintertype.TabIndex = 4;
            this.cmbPrintertype.Enter += new System.EventHandler(this.cmbPrintertype_Enter);
            this.cmbPrintertype.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbPrintertype_KeyPress);
            this.cmbPrintertype.Leave += new System.EventHandler(this.cmbPrintertype_Leave);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(342, 232);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 19);
            this.label5.TabIndex = 501;
            this.label5.Text = "Printer Type :";
            // 
            // cmbPrint
            // 
            this.cmbPrint.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPrint.FormattingEnabled = true;
            this.cmbPrint.Location = new System.Drawing.Point(442, 159);
            this.cmbPrint.Name = "cmbPrint";
            this.cmbPrint.Size = new System.Drawing.Size(133, 21);
            this.cmbPrint.TabIndex = 2;
            this.cmbPrint.Enter += new System.EventHandler(this.cmbPrint_Enter);
            this.cmbPrint.Leave += new System.EventHandler(this.cmbPrint_Leave);
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(390, 162);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(50, 19);
            this.label11.TabIndex = 500;
            this.label11.Text = "Print :";
            // 
            // cmbprinternm
            // 
            this.cmbprinternm.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbprinternm.FormattingEnabled = true;
            this.cmbprinternm.Location = new System.Drawing.Point(442, 193);
            this.cmbprinternm.Name = "cmbprinternm";
            this.cmbprinternm.Size = new System.Drawing.Size(202, 21);
            this.cmbprinternm.TabIndex = 3;
            this.cmbprinternm.Enter += new System.EventHandler(this.cmbprinternm_Enter);
            this.cmbprinternm.Leave += new System.EventHandler(this.cmbprinternm_Leave);
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.Red;
            this.label22.Location = new System.Drawing.Point(704, 125);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(10, 10);
            this.label22.TabIndex = 502;
            this.label22.Text = "*";
            this.label22.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(650, 193);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(10, 10);
            this.label2.TabIndex = 503;
            this.label2.Text = "*";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // lblSucessMsg
            // 
            this.lblSucessMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblSucessMsg.Font = new System.Drawing.Font("Verdana", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSucessMsg.ForeColor = System.Drawing.Color.Black;
            this.lblSucessMsg.Location = new System.Drawing.Point(22, 512);
            this.lblSucessMsg.Name = "lblSucessMsg";
            this.lblSucessMsg.Size = new System.Drawing.Size(260, 24);
            this.lblSucessMsg.TabIndex = 506;
            // 
            // btnGreaterID
            // 
            this.btnGreaterID.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnGreaterID.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGreaterID.Location = new System.Drawing.Point(401, 484);
            this.btnGreaterID.Name = "btnGreaterID";
            this.btnGreaterID.Size = new System.Drawing.Size(50, 25);
            this.btnGreaterID.TabIndex = 657;
            this.btnGreaterID.Text = ">>";
            this.btnGreaterID.UseVisualStyleBackColor = true;
            this.btnGreaterID.Click += new System.EventHandler(this.btnGreaterID_Click);
            // 
            // btnLessID
            // 
            this.btnLessID.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnLessID.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLessID.Location = new System.Drawing.Point(336, 484);
            this.btnLessID.Name = "btnLessID";
            this.btnLessID.Size = new System.Drawing.Size(50, 25);
            this.btnLessID.TabIndex = 656;
            this.btnLessID.Text = "<<";
            this.btnLessID.UseVisualStyleBackColor = true;
            this.btnLessID.Click += new System.EventHandler(this.btnLessID_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.btnGreaterID);
            this.pnlMain.Controls.Add(this.btnLessID);
            this.pnlMain.Controls.Add(this.lblSucessMsg);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.label22);
            this.pnlMain.Controls.Add(this.cmbprinternm);
            this.pnlMain.Controls.Add(this.cmbPrintertype);
            this.pnlMain.Controls.Add(this.label5);
            this.pnlMain.Controls.Add(this.cmbPrint);
            this.pnlMain.Controls.Add(this.label11);
            this.pnlMain.Controls.Add(this.label7);
            this.pnlMain.Controls.Add(this.txtsearch);
            this.pnlMain.Controls.Add(this.txtcode);
            this.pnlMain.Controls.Add(this.BtnExit);
            this.pnlMain.Controls.Add(this.BtnDelete);
            this.pnlMain.Controls.Add(this.BtnSave);
            this.pnlMain.Controls.Add(this.BtnAdd);
            this.pnlMain.Controls.Add(this.txtkotsection);
            this.pnlMain.Controls.Add(this.label3);
            this.pnlMain.Controls.Add(this.label21);
            this.pnlMain.Controls.Add(this.DtgridKotSection);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.Chkshowdelet);
            this.pnlMain.Location = new System.Drawing.Point(12, 12);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(835, 553);
            this.pnlMain.TabIndex = 658;
            // 
            // frmKotSection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(881, 584);
            this.ControlBox = false;
            this.Controls.Add(this.pnlMain);
            this.KeyPreview = true;
            this.Name = "frmKotSection";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Kitchen Section";
            this.Load += new System.EventHandler(this.frmKotSection_Load);
            this.Shown += new System.EventHandler(this.frmKotSection_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmKotSection_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmKotSection_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.DtgridKotSection)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtsearch;
        private System.Windows.Forms.TextBox txtcode;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.Button BtnDelete;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.TextBox txtkotsection;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.DataGridView DtgridKotSection;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox Chkshowdelet;
        private System.Windows.Forms.ComboBox cmbPrintertype;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbPrint;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbprinternm;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSucessMsg;
        private System.Windows.Forms.Button btnGreaterID;
        private System.Windows.Forms.Button btnLessID;
        private System.Windows.Forms.Panel pnlMain;
    }
}
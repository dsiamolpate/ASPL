namespace ASPL.LODGE.MASTERS
{
    partial class DayBook
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ChkDeleted = new System.Windows.Forms.CheckBox();
            this.txtsearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.txtdaybookcod = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGenLegnm = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtgroupnm = new System.Windows.Forms.TextBox();
            this.txtglcode = new System.Windows.Forms.TextBox();
            this.txtgrpcod = new System.Windows.Forms.TextBox();
            this.BtnExit = new System.Windows.Forms.Button();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            this.DtGridDayBook = new System.Windows.Forms.DataGridView();
            this.lblSucessMsg = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.btnLessID = new System.Windows.Forms.Button();
            this.btnGreaterID = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DtGridDayBook)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChkDeleted
            // 
            this.ChkDeleted.AutoSize = true;
            this.ChkDeleted.BackColor = System.Drawing.Color.Transparent;
            this.ChkDeleted.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDeleted.ForeColor = System.Drawing.Color.Black;
            this.ChkDeleted.Location = new System.Drawing.Point(144, 6);
            this.ChkDeleted.Name = "ChkDeleted";
            this.ChkDeleted.Size = new System.Drawing.Size(118, 18);
            this.ChkDeleted.TabIndex = 390;
            this.ChkDeleted.Text = "Show Deleted";
            this.ChkDeleted.UseVisualStyleBackColor = false;
            this.ChkDeleted.CheckedChanged += new System.EventHandler(this.ChkDeleted_CheckedChanged);
            // 
            // txtsearch
            // 
            this.txtsearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtsearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtsearch.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsearch.Location = new System.Drawing.Point(140, 25);
            this.txtsearch.Name = "txtsearch";
            this.txtsearch.Size = new System.Drawing.Size(185, 22);
            this.txtsearch.TabIndex = 0;
            this.txtsearch.TextChanged += new System.EventHandler(this.txtsearch_TextChanged);
            this.txtsearch.Enter += new System.EventHandler(this.txtsearch_Enter);
            this.txtsearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtsearch_KeyDown);
            this.txtsearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtsearch_KeyPress);
            this.txtsearch.Leave += new System.EventHandler(this.txtsearch_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(16, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 14);
            this.label1.TabIndex = 392;
            this.label1.Text = "Search by Name :";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(348, 60);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(116, 14);
            this.label21.TabIndex = 411;
            this.label21.Text = "Day Book Code :";
            // 
            // txtdaybookcod
            // 
            this.txtdaybookcod.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtdaybookcod.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtdaybookcod.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtdaybookcod.Location = new System.Drawing.Point(467, 57);
            this.txtdaybookcod.Name = "txtdaybookcod";
            this.txtdaybookcod.Size = new System.Drawing.Size(117, 22);
            this.txtdaybookcod.TabIndex = 412;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(328, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 14);
            this.label2.TabIndex = 413;
            this.label2.Text = " Gen-Ledger Name:";
            // 
            // txtGenLegnm
            // 
            this.txtGenLegnm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGenLegnm.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGenLegnm.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGenLegnm.Location = new System.Drawing.Point(467, 101);
            this.txtGenLegnm.MaxLength = 50;
            this.txtGenLegnm.Name = "txtGenLegnm";
            this.txtGenLegnm.Size = new System.Drawing.Size(225, 22);
            this.txtGenLegnm.TabIndex = 1;
            this.txtGenLegnm.Enter += new System.EventHandler(this.txtGenLegnm_Enter);
            this.txtGenLegnm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGenLegnm_KeyDown);
            this.txtGenLegnm.Leave += new System.EventHandler(this.txtGenLegnm_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(367, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 14);
            this.label3.TabIndex = 415;
            this.label3.Text = " Group Name:";
            // 
            // txtgroupnm
            // 
            this.txtgroupnm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtgroupnm.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtgroupnm.Enabled = false;
            this.txtgroupnm.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtgroupnm.Location = new System.Drawing.Point(467, 142);
            this.txtgroupnm.Name = "txtgroupnm";
            this.txtgroupnm.Size = new System.Drawing.Size(225, 22);
            this.txtgroupnm.TabIndex = 352;
            // 
            // txtglcode
            // 
            this.txtglcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtglcode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtglcode.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtglcode.Location = new System.Drawing.Point(467, 218);
            this.txtglcode.Name = "txtglcode";
            this.txtglcode.Size = new System.Drawing.Size(117, 22);
            this.txtglcode.TabIndex = 417;
            this.txtglcode.Visible = false;
            // 
            // txtgrpcod
            // 
            this.txtgrpcod.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtgrpcod.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtgrpcod.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtgrpcod.Location = new System.Drawing.Point(467, 179);
            this.txtgrpcod.Name = "txtgrpcod";
            this.txtgrpcod.Size = new System.Drawing.Size(117, 22);
            this.txtgrpcod.TabIndex = 418;
            this.txtgrpcod.Visible = false;
            // 
            // BtnExit
            // 
            this.BtnExit.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Location = new System.Drawing.Point(694, 477);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(78, 26);
            this.BtnExit.TabIndex = 422;
            this.BtnExit.Text = "EXIT";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Location = new System.Drawing.Point(605, 477);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(78, 26);
            this.BtnDelete.TabIndex = 421;
            this.BtnDelete.Text = "DELETE";
            this.BtnDelete.UseVisualStyleBackColor = true;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Location = new System.Drawing.Point(516, 477);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(78, 26);
            this.BtnSave.TabIndex = 2;
            this.BtnSave.Text = "SAVE";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnAdd
            // 
            this.BtnAdd.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BtnAdd.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAdd.Location = new System.Drawing.Point(427, 477);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(78, 26);
            this.BtnAdd.TabIndex = 419;
            this.BtnAdd.Text = "&ADD";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.Red;
            this.label22.Location = new System.Drawing.Point(696, 102);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(10, 10);
            this.label22.TabIndex = 423;
            this.label22.Text = "*";
            this.label22.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // DtGridDayBook
            // 
            this.DtGridDayBook.AllowUserToAddRows = false;
            this.DtGridDayBook.AllowUserToDeleteRows = false;
            this.DtGridDayBook.AllowUserToResizeColumns = false;
            this.DtGridDayBook.AllowUserToResizeRows = false;
            this.DtGridDayBook.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DtGridDayBook.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DtGridDayBook.ColumnHeadersHeight = 25;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DtGridDayBook.DefaultCellStyle = dataGridViewCellStyle2;
            this.DtGridDayBook.EnableHeadersVisualStyles = false;
            this.DtGridDayBook.Location = new System.Drawing.Point(13, 53);
            this.DtGridDayBook.MultiSelect = false;
            this.DtGridDayBook.Name = "DtGridDayBook";
            this.DtGridDayBook.ReadOnly = true;
            this.DtGridDayBook.RowHeadersVisible = false;
            this.DtGridDayBook.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DtGridDayBook.Size = new System.Drawing.Size(313, 450);
            this.DtGridDayBook.TabIndex = 1;
            this.DtGridDayBook.TabStop = false;
            this.DtGridDayBook.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DtGridDayBook_CellClick);
            this.DtGridDayBook.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DtGridDayBook_CellDoubleClick);
            this.DtGridDayBook.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DtGridDayBook_KeyDown_1);
            // 
            // lblSucessMsg
            // 
            this.lblSucessMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblSucessMsg.Font = new System.Drawing.Font("Verdana", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSucessMsg.ForeColor = System.Drawing.Color.Black;
            this.lblSucessMsg.Location = new System.Drawing.Point(17, 506);
            this.lblSucessMsg.Name = "lblSucessMsg";
            this.lblSucessMsg.Size = new System.Drawing.Size(260, 20);
            this.lblSucessMsg.TabIndex = 456;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.btnLessID);
            this.pnlMain.Controls.Add(this.btnGreaterID);
            this.pnlMain.Controls.Add(this.lblSucessMsg);
            this.pnlMain.Controls.Add(this.DtGridDayBook);
            this.pnlMain.Controls.Add(this.label22);
            this.pnlMain.Controls.Add(this.BtnExit);
            this.pnlMain.Controls.Add(this.BtnDelete);
            this.pnlMain.Controls.Add(this.BtnSave);
            this.pnlMain.Controls.Add(this.BtnAdd);
            this.pnlMain.Controls.Add(this.txtgrpcod);
            this.pnlMain.Controls.Add(this.txtglcode);
            this.pnlMain.Controls.Add(this.txtgroupnm);
            this.pnlMain.Controls.Add(this.label3);
            this.pnlMain.Controls.Add(this.txtGenLegnm);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.txtdaybookcod);
            this.pnlMain.Controls.Add(this.label21);
            this.pnlMain.Controls.Add(this.txtsearch);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.ChkDeleted);
            this.pnlMain.Location = new System.Drawing.Point(10, 13);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(785, 543);
            this.pnlMain.TabIndex = 457;
            // 
            // btnLessID
            // 
            this.btnLessID.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnLessID.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLessID.Location = new System.Drawing.Point(333, 478);
            this.btnLessID.Name = "btnLessID";
            this.btnLessID.Size = new System.Drawing.Size(36, 25);
            this.btnLessID.TabIndex = 677;
            this.btnLessID.Text = "<<";
            this.btnLessID.UseVisualStyleBackColor = true;
            this.btnLessID.Click += new System.EventHandler(this.btnLessID_Click);
            // 
            // btnGreaterID
            // 
            this.btnGreaterID.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnGreaterID.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGreaterID.Location = new System.Drawing.Point(380, 478);
            this.btnGreaterID.Name = "btnGreaterID";
            this.btnGreaterID.Size = new System.Drawing.Size(36, 25);
            this.btnGreaterID.TabIndex = 676;
            this.btnGreaterID.Text = ">>";
            this.btnGreaterID.UseVisualStyleBackColor = true;
            this.btnGreaterID.Click += new System.EventHandler(this.btnGreaterID_Click);
            // 
            // DayBook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(816, 568);
            this.ControlBox = false;
            this.Controls.Add(this.pnlMain);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "DayBook";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Day Book";
            this.Load += new System.EventHandler(this.DayBook_Load);
            this.Shown += new System.EventHandler(this.DayBook_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DayBook_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DayBook_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.DtGridDayBook)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox ChkDeleted;
        private System.Windows.Forms.TextBox txtsearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtdaybookcod;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtGenLegnm;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtgroupnm;
        private System.Windows.Forms.TextBox txtglcode;
        private System.Windows.Forms.TextBox txtgrpcod;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.Button BtnDelete;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.DataGridView DtGridDayBook;
        private System.Windows.Forms.Label lblSucessMsg;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Button btnLessID;
        private System.Windows.Forms.Button btnGreaterID;
    }
}
namespace ASPL.ACCOUNT.TRANSACTION
{
    partial class FrmPurchase
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabPurchase = new System.Windows.Forms.TabControl();
            this.tabList = new System.Windows.Forms.TabPage();
            this.txtsearch = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.BtnExit = new System.Windows.Forms.Button();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.dtgridPurchase = new System.Windows.Forms.DataGridView();
            this.tabDetail = new System.Windows.Forms.TabPage();
            this.lblSupplierName = new System.Windows.Forms.Label();
            this.BtnExcel = new System.Windows.Forms.Button();
            this.BtnExport = new System.Windows.Forms.Button();
            this.txtNetAmt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDiscAmt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDisc = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtGstAmt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGrossAmt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtgridItem = new System.Windows.Forms.DataGridView();
            this.SrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GrossWt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
          //  this.Tax = new System.Windows.Forms.DataGridViewTextBoxColumn()
            this.Disc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaxAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtSupplierID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnPrint = new System.Windows.Forms.Button();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.dtpBillDate = new System.Windows.Forms.DateTimePicker();
            this.label20 = new System.Windows.Forms.Label();
            this.txtBillNo = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabPurchase.SuspendLayout();
            this.tabList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgridPurchase)).BeginInit();
            this.tabDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgridItem)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPurchase
            // 
            this.tabPurchase.Controls.Add(this.tabList);
            this.tabPurchase.Controls.Add(this.tabDetail);
            this.tabPurchase.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPurchase.ItemSize = new System.Drawing.Size(100, 21);
            this.tabPurchase.Location = new System.Drawing.Point(6, 4);
            this.tabPurchase.Name = "tabPurchase";
            this.tabPurchase.SelectedIndex = 0;
            this.tabPurchase.Size = new System.Drawing.Size(811, 533);
            this.tabPurchase.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabPurchase.TabIndex = 4;
            this.tabPurchase.TabStop = false;
            this.tabPurchase.SelectedIndexChanged += new System.EventHandler(this.tabPurchase_SelectedIndexChanged);
            this.tabPurchase.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabPurchase_Selecting);
            // 
            // tabList
            // 
            this.tabList.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tabList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabList.Controls.Add(this.txtsearch);
            this.tabList.Controls.Add(this.label22);
            this.tabList.Controls.Add(this.BtnExit);
            this.tabList.Controls.Add(this.BtnAdd);
            this.tabList.Controls.Add(this.dtgridPurchase);
            this.tabList.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabList.Location = new System.Drawing.Point(4, 25);
            this.tabList.Name = "tabList";
            this.tabList.Padding = new System.Windows.Forms.Padding(3);
            this.tabList.Size = new System.Drawing.Size(803, 504);
            this.tabList.TabIndex = 0;
            this.tabList.Text = "List";
            // 
            // txtsearch
            // 
            this.txtsearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtsearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtsearch.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsearch.Location = new System.Drawing.Point(178, 41);
            this.txtsearch.Name = "txtsearch";
            this.txtsearch.Size = new System.Drawing.Size(307, 22);
            this.txtsearch.TabIndex = 0;
            this.txtsearch.TextChanged += new System.EventHandler(this.txtsearch_TextChanged);
            this.txtsearch.Enter += new System.EventHandler(this.txtsearch_Enter);
            this.txtsearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtsearch_KeyDown);
            this.txtsearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtsearch_KeyPress);
            this.txtsearch.Leave += new System.EventHandler(this.txtsearch_Leave);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label22.Location = new System.Drawing.Point(36, 41);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(124, 14);
            this.label22.TabIndex = 594;
            this.label22.Text = "Search by Name :";
            // 
            // BtnExit
            // 
            this.BtnExit.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.BtnExit.Location = new System.Drawing.Point(587, 475);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(90, 25);
            this.BtnExit.TabIndex = 592;
            this.BtnExit.Text = "EXIT";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnAdd
            // 
            this.BtnAdd.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BtnAdd.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.BtnAdd.Location = new System.Drawing.Point(493, 475);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(90, 25);
            this.BtnAdd.TabIndex = 1;
            this.BtnAdd.Text = "&ADD";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // dtgridPurchase
            // 
            this.dtgridPurchase.AllowUserToAddRows = false;
            this.dtgridPurchase.AllowUserToDeleteRows = false;
            this.dtgridPurchase.AllowUserToResizeColumns = false;
            this.dtgridPurchase.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dtgridPurchase.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgridPurchase.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgridPurchase.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dtgridPurchase.ColumnHeadersHeight = 25;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgridPurchase.DefaultCellStyle = dataGridViewCellStyle3;
            this.dtgridPurchase.EnableHeadersVisualStyles = false;
            this.dtgridPurchase.Location = new System.Drawing.Point(6, 80);
            this.dtgridPurchase.Name = "dtgridPurchase";
            this.dtgridPurchase.RowHeadersVisible = false;
            this.dtgridPurchase.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgridPurchase.Size = new System.Drawing.Size(759, 438);
            this.dtgridPurchase.TabIndex = 576;
            this.dtgridPurchase.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgridPurchase_CellDoubleClick);
            this.dtgridPurchase.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtgridPurchase_KeyDown);
            // 
            // tabDetail
            // 
            this.tabDetail.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tabDetail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabDetail.Controls.Add(this.textBox1);
            this.tabDetail.Controls.Add(this.label7);
            this.tabDetail.Controls.Add(this.lblSupplierName);
            this.tabDetail.Controls.Add(this.BtnExcel);
            this.tabDetail.Controls.Add(this.BtnExport);
            this.tabDetail.Controls.Add(this.txtNetAmt);
            this.tabDetail.Controls.Add(this.label6);
            this.tabDetail.Controls.Add(this.txtDiscAmt);
            this.tabDetail.Controls.Add(this.label4);
            this.tabDetail.Controls.Add(this.txtDisc);
            this.tabDetail.Controls.Add(this.label3);
            this.tabDetail.Controls.Add(this.txtGstAmt);
            this.tabDetail.Controls.Add(this.label2);
            this.tabDetail.Controls.Add(this.txtGrossAmt);
            this.tabDetail.Controls.Add(this.label1);
            this.tabDetail.Controls.Add(this.dtgridItem);
            this.tabDetail.Controls.Add(this.txtSupplierID);
            this.tabDetail.Controls.Add(this.label5);
            this.tabDetail.Controls.Add(this.BtnCancel);
            this.tabDetail.Controls.Add(this.BtnPrint);
            this.tabDetail.Controls.Add(this.BtnDelete);
            this.tabDetail.Controls.Add(this.BtnSave);
            this.tabDetail.Controls.Add(this.dtpBillDate);
            this.tabDetail.Controls.Add(this.label20);
            this.tabDetail.Controls.Add(this.txtBillNo);
            this.tabDetail.Controls.Add(this.label21);
            this.tabDetail.Location = new System.Drawing.Point(4, 25);
            this.tabDetail.Name = "tabDetail";
            this.tabDetail.Padding = new System.Windows.Forms.Padding(3);
            this.tabDetail.Size = new System.Drawing.Size(803, 504);
            this.tabDetail.TabIndex = 1;
            this.tabDetail.Text = "Details";
            // 
            // lblSupplierName
            // 
            this.lblSupplierName.BackColor = System.Drawing.Color.White;
            this.lblSupplierName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSupplierName.Font = new System.Drawing.Font("Verdana", 9F);
            this.lblSupplierName.Location = new System.Drawing.Point(240, 41);
            this.lblSupplierName.Name = "lblSupplierName";
            this.lblSupplierName.Size = new System.Drawing.Size(298, 22);
            this.lblSupplierName.TabIndex = 658;
            this.lblSupplierName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BtnExcel
            // 
            this.BtnExcel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExcel.Location = new System.Drawing.Point(132, 473);
            this.BtnExcel.Name = "BtnExcel";
            this.BtnExcel.Size = new System.Drawing.Size(86, 25);
            this.BtnExcel.TabIndex = 632;
            this.BtnExcel.Text = "EXCEL";
            this.BtnExcel.UseVisualStyleBackColor = true;
            this.BtnExcel.Click += new System.EventHandler(this.BtnExcel_Click);
            // 
            // BtnExport
            // 
            this.BtnExport.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExport.Location = new System.Drawing.Point(224, 473);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(86, 25);
            this.BtnExport.TabIndex = 631;
            this.BtnExport.Text = "EXPORT";
            this.BtnExport.UseVisualStyleBackColor = true;
            // 
            // txtNetAmt
            // 
            this.txtNetAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNetAmt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNetAmt.Enabled = false;
            this.txtNetAmt.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNetAmt.Location = new System.Drawing.Point(562, 440);
            this.txtNetAmt.Name = "txtNetAmt";
            this.txtNetAmt.Size = new System.Drawing.Size(119, 22);
            this.txtNetAmt.TabIndex = 629;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkRed;
            this.label6.Location = new System.Drawing.Point(471, 441);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 14);
            this.label6.TabIndex = 630;
            this.label6.Text = "Net Amount:";
            // 
            // txtDiscAmt
            // 
            this.txtDiscAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDiscAmt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDiscAmt.Enabled = false;
            this.txtDiscAmt.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiscAmt.Location = new System.Drawing.Point(389, 436);
            this.txtDiscAmt.Name = "txtDiscAmt";
            this.txtDiscAmt.Size = new System.Drawing.Size(67, 22);
            this.txtDiscAmt.TabIndex = 627;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(286, 440);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 14);
            this.label4.TabIndex = 628;
            this.label4.Text = " Disc Amount :";
            // 
            // txtDisc
            // 
            this.txtDisc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDisc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDisc.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDisc.Location = new System.Drawing.Point(235, 438);
            this.txtDisc.Name = "txtDisc";
            this.txtDisc.Size = new System.Drawing.Size(49, 22);
            this.txtDisc.TabIndex = 625;
            this.txtDisc.Enter += new System.EventHandler(this.txtDisc_Enter);
            this.txtDisc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDisc_KeyPress);
            this.txtDisc.Leave += new System.EventHandler(this.txtDisc_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(171, 441);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 14);
            this.label3.TabIndex = 626;
            this.label3.Text = "Disc.% :";
            // 
            // txtGstAmt
            // 
            this.txtGstAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGstAmt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGstAmt.Enabled = false;
            this.txtGstAmt.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGstAmt.Location = new System.Drawing.Point(101, 438);
            this.txtGstAmt.Name = "txtGstAmt";
            this.txtGstAmt.Size = new System.Drawing.Size(65, 22);
            this.txtGstAmt.TabIndex = 623;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 442);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 14);
            this.label2.TabIndex = 624;
            this.label2.Text = "GST Amount :";
            // 
            // txtGrossAmt
            // 
            this.txtGrossAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGrossAmt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGrossAmt.Enabled = false;
            this.txtGrossAmt.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGrossAmt.Location = new System.Drawing.Point(561, 411);
            this.txtGrossAmt.Name = "txtGrossAmt";
            this.txtGrossAmt.Size = new System.Drawing.Size(119, 22);
            this.txtGrossAmt.TabIndex = 621;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(456, 413);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 14);
            this.label1.TabIndex = 622;
            this.label1.Text = "Gross Amount:";
            // 
            // dtgridItem
            // 
            this.dtgridItem.AllowUserToAddRows = false;
            this.dtgridItem.AllowUserToResizeColumns = false;
            this.dtgridItem.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            this.dtgridItem.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dtgridItem.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgridItem.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dtgridItem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SrNo,
            this.ItemNo,
            this.ItemName,
            this.GrossWt,
            this.Qty,
            this.Rate,
            this.Disc,
            this.TaxAmt,
            this.TotalAmt});
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgridItem.DefaultCellStyle = dataGridViewCellStyle10;
            this.dtgridItem.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dtgridItem.EnableHeadersVisualStyles = false;
            this.dtgridItem.Location = new System.Drawing.Point(0, 73);
            this.dtgridItem.Margin = new System.Windows.Forms.Padding(2);
            this.dtgridItem.Name = "dtgridItem";
            this.dtgridItem.RowHeadersVisible = false;
            this.dtgridItem.RowTemplate.Height = 24;
            this.dtgridItem.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dtgridItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dtgridItem.Size = new System.Drawing.Size(798, 331);
            this.dtgridItem.TabIndex = 620;
            this.dtgridItem.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgridItem_CellEnter);
            this.dtgridItem.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgridItem_CellLeave);
            this.dtgridItem.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dtgridItem_EditingControlShowing);
            this.dtgridItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtgridItem_KeyDown);
            // 
            // SrNo
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.SrNo.DefaultCellStyle = dataGridViewCellStyle6;
            this.SrNo.HeaderText = "Sr No.";
            this.SrNo.Name = "SrNo";
            this.SrNo.ReadOnly = true;
            this.SrNo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SrNo.Width = 56;
            // 
            // ItemNo
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ItemNo.DefaultCellStyle = dataGridViewCellStyle7;
            this.ItemNo.HeaderText = "Item No.";
            this.ItemNo.Name = "ItemNo";
            this.ItemNo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ItemNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ItemNo.Width = 80;
            // 
            // ItemName
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ItemName.DefaultCellStyle = dataGridViewCellStyle8;
            this.ItemName.HeaderText = "Item Name";
            this.ItemName.Name = "ItemName";
            this.ItemName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ItemName.Width = 182;
            // 
            // GrossWt
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.GrossWt.DefaultCellStyle = dataGridViewCellStyle9;
            this.GrossWt.HeaderText = "Gross Wt.(kg)";
            this.GrossWt.Name = "GrossWt";
            this.GrossWt.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.GrossWt.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.GrossWt.Width = 120;
            // 
            // Qty
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Qty.DefaultCellStyle = dataGridViewCellStyle10;
            this.Qty.HeaderText = "Qty.";
            this.Qty.Name = "Qty";
            this.Qty.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Qty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Qty.Width = 40;
            // 
            // Rate
            // 
            this.Rate.HeaderText = "Rate";
            this.Rate.Name = "Rate";
            this.Rate.Width = 65;
            //// 
            //// Tax
            //// 
            //dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            //this.TaxAmt.DefaultCellStyle = dataGridViewCellStyle12;
            //this.TaxAmt.HeaderText = "Tax%";
            //this.TaxAmt.Name = "Tax";
            //this.TaxAmt.Width = 85;
            // 
            // TaxAmt
            // 
            this.TaxAmt.HeaderText = "Tax Amt";
            this.TaxAmt.Name = "TaxAmt";
            this.TaxAmt.Width = 80;
            // 
            // Disc
            // 
            this.Disc.FillWeight = 75F;
            this.Disc.HeaderText = "Disc%";
            this.Disc.Name = "Disc";
            this.Disc.Width = 75;
            // 
            // TotalAmt
            // 
            this.TotalAmt.HeaderText = "Total Amt.";
            this.TotalAmt.Name = "TotalAmt";
            this.TotalAmt.Width = 92;
            // 
            // txtSupplierID
            // 
            this.txtSupplierID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSupplierID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSupplierID.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSupplierID.Location = new System.Drawing.Point(132, 41);
            this.txtSupplierID.Name = "txtSupplierID";
            this.txtSupplierID.Size = new System.Drawing.Size(102, 22);
            this.txtSupplierID.TabIndex = 1;
            this.txtSupplierID.Enter += new System.EventHandler(this.txtSupplierID_Enter);
            this.txtSupplierID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSupplierID_KeyDown);
            this.txtSupplierID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSupplierID_KeyPress);
            this.txtSupplierID.Leave += new System.EventHandler(this.txtSupplierID_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(57, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 14);
            this.label5.TabIndex = 618;
            this.label5.Text = "Supplier :";
            // 
            // BtnCancel
            // 
            this.BtnCancel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Location = new System.Drawing.Point(592, 473);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(86, 25);
            this.BtnCancel.TabIndex = 612;
            this.BtnCancel.Text = "CANCEL";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnPrint
            // 
            this.BtnPrint.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPrint.Location = new System.Drawing.Point(316, 473);
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.Size = new System.Drawing.Size(86, 25);
            this.BtnPrint.TabIndex = 593;
            this.BtnPrint.Text = "PRINT";
            this.BtnPrint.UseVisualStyleBackColor = true;
            // 
            // BtnDelete
            // 
            this.BtnDelete.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Location = new System.Drawing.Point(500, 473);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(86, 25);
            this.BtnDelete.TabIndex = 589;
            this.BtnDelete.Text = "DELETE";
            this.BtnDelete.UseVisualStyleBackColor = true;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Location = new System.Drawing.Point(408, 473);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(86, 25);
            this.BtnSave.TabIndex = 588;
            this.BtnSave.Text = "SAVE";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // dtpBillDate
            // 
            this.dtpBillDate.CustomFormat = "dd/MM/yyyy";
            this.dtpBillDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBillDate.Location = new System.Drawing.Point(654, 10);
            this.dtpBillDate.Name = "dtpBillDate";
            this.dtpBillDate.Size = new System.Drawing.Size(143, 23);
            this.dtpBillDate.TabIndex = 0;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(559, 12);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(72, 14);
            this.label20.TabIndex = 576;
            this.label20.Text = "Bill Date :";
            // 
            // txtBillNo
            // 
            this.txtBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBillNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBillNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBillNo.Location = new System.Drawing.Point(131, 10);
            this.txtBillNo.Name = "txtBillNo";
            this.txtBillNo.Size = new System.Drawing.Size(103, 22);
            this.txtBillNo.TabIndex = 567;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(68, 14);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(59, 14);
            this.label21.TabIndex = 575;
            this.label21.Text = "Bill No :";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.tabPurchase);
            this.pnlMain.Location = new System.Drawing.Point(12, 12);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(820, 547);
            this.pnlMain.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(559, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 14);
            this.label7.TabIndex = 659;
            this.label7.Text = "Bill Ref No. :";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(654, 39);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(143, 23);
            this.textBox1.TabIndex = 660;
            // 
            // FrmPurchase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(844, 569);
            this.Controls.Add(this.pnlMain);
            this.KeyPreview = true;
            this.Name = "FrmPurchase";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Purchase";
            this.Load += new System.EventHandler(this.FrmPurchase_Load);
            this.Shown += new System.EventHandler(this.FrmPurchase_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmPurchase_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmPurchase_KeyPress);
            this.tabPurchase.ResumeLayout(false);
            this.tabList.ResumeLayout(false);
            this.tabList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgridPurchase)).EndInit();
            this.tabDetail.ResumeLayout(false);
            this.tabDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgridItem)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabPurchase;
        private System.Windows.Forms.TabPage tabList;
        private System.Windows.Forms.TextBox txtsearch;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.DataGridView dtgridPurchase;
        private System.Windows.Forms.TabPage tabDetail;
        private System.Windows.Forms.TextBox txtSupplierID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Button BtnPrint;
        private System.Windows.Forms.Button BtnDelete;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.DateTimePicker dtpBillDate;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtBillNo;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtGrossAmt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNetAmt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDiscAmt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDisc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtGstAmt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BtnExcel;
        private System.Windows.Forms.Button BtnExport;
        private System.Windows.Forms.DataGridView dtgridItem;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblSupplierName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn GrossWt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Disc;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaxAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalAmt;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label7;
    }
}
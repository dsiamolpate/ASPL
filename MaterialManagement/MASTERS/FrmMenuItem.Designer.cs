namespace ASPL.Material_Management.MASTERS
{
    partial class FrmMenuItem
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
            this.cmbCategry = new System.Windows.Forms.ComboBox();
            this.txtsearch = new System.Windows.Forms.TextBox();
            this.txtcode = new System.Windows.Forms.TextBox();
            this.BtnExit = new System.Windows.Forms.Button();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.txtItemname = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.DtgridMenuItem = new System.Windows.Forms.DataGridView();
            this.Chkshowdelet = new System.Windows.Forms.CheckBox();
            this.txtHSNno = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btngrpBrws = new System.Windows.Forms.Button();
            this.txtGropId = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSaleRate = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPurchaseRate = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtOpeningStock = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.combOpenStockCrDr = new System.Windows.Forms.ComboBox();
            this.combClosingStockCrDr = new System.Windows.Forms.ComboBox();
            this.txtClosingStock = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtMinQty = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtReorderQty = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtMaxQty = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.lblSucessMsg = new System.Windows.Forms.Label();
            this.btnLessID = new System.Windows.Forms.Button();
            this.btnGreaterID = new System.Windows.Forms.Button();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.lblGroupName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DtgridMenuItem)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbCategry
            // 
            this.cmbCategry.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCategry.FormattingEnabled = true;
            this.cmbCategry.Location = new System.Drawing.Point(436, 214);
            this.cmbCategry.Name = "cmbCategry";
            this.cmbCategry.Size = new System.Drawing.Size(112, 21);
            this.cmbCategry.TabIndex = 6;
            this.cmbCategry.Enter += new System.EventHandler(this.cmbCategry_Enter);
            this.cmbCategry.Leave += new System.EventHandler(this.cmbCategry_Leave);
            // 
            // txtsearch
            // 
            this.txtsearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtsearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtsearch.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsearch.Location = new System.Drawing.Point(131, 28);
            this.txtsearch.Name = "txtsearch";
            this.txtsearch.Size = new System.Drawing.Size(187, 22);
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
            this.txtcode.Location = new System.Drawing.Point(437, 62);
            this.txtcode.Name = "txtcode";
            this.txtcode.Size = new System.Drawing.Size(87, 22);
            this.txtcode.TabIndex = 484;
            // 
            // BtnExit
            // 
            this.BtnExit.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Location = new System.Drawing.Point(625, 486);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(72, 25);
            this.BtnExit.TabIndex = 483;
            this.BtnExit.Text = "EXIT";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Location = new System.Drawing.Point(549, 486);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(72, 25);
            this.BtnDelete.TabIndex = 482;
            this.BtnDelete.Text = "DELETE";
            this.BtnDelete.UseVisualStyleBackColor = true;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Location = new System.Drawing.Point(473, 486);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(72, 25);
            this.BtnSave.TabIndex = 481;
            this.BtnSave.Text = "SAVE";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnAdd
            // 
            this.BtnAdd.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BtnAdd.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAdd.Location = new System.Drawing.Point(397, 486);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(72, 25);
            this.BtnAdd.TabIndex = 480;
            this.BtnAdd.Text = "&ADD";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(358, 217);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(76, 14);
            this.label11.TabIndex = 479;
            this.label11.Text = "Category :";
            // 
            // txtItemname
            // 
            this.txtItemname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtItemname.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtItemname.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtItemname.Location = new System.Drawing.Point(437, 149);
            this.txtItemname.MaxLength = 50;
            this.txtItemname.Name = "txtItemname";
            this.txtItemname.Size = new System.Drawing.Size(260, 22);
            this.txtItemname.TabIndex = 3;
            this.txtItemname.Enter += new System.EventHandler(this.txtItemname_Enter);
            this.txtItemname.Leave += new System.EventHandler(this.txtItemname_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(380, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 14);
            this.label3.TabIndex = 478;
            this.label3.Text = "Name :";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(386, 66);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(49, 14);
            this.label21.TabIndex = 477;
            this.label21.Text = "Code :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(6, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 14);
            this.label1.TabIndex = 475;
            this.label1.Text = "Search by Name :";
            // 
            // DtgridMenuItem
            // 
            this.DtgridMenuItem.AllowUserToAddRows = false;
            this.DtgridMenuItem.AllowUserToDeleteRows = false;
            this.DtgridMenuItem.AllowUserToResizeColumns = false;
            this.DtgridMenuItem.AllowUserToResizeRows = false;
            this.DtgridMenuItem.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DtgridMenuItem.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DtgridMenuItem.ColumnHeadersHeight = 25;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DtgridMenuItem.DefaultCellStyle = dataGridViewCellStyle2;
            this.DtgridMenuItem.EnableHeadersVisualStyles = false;
            this.DtgridMenuItem.Location = new System.Drawing.Point(5, 53);
            this.DtgridMenuItem.MultiSelect = false;
            this.DtgridMenuItem.Name = "DtgridMenuItem";
            this.DtgridMenuItem.ReadOnly = true;
            this.DtgridMenuItem.RowHeadersVisible = false;
            this.DtgridMenuItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DtgridMenuItem.Size = new System.Drawing.Size(313, 450);
            this.DtgridMenuItem.TabIndex = 476;
            this.DtgridMenuItem.TabStop = false;
            this.DtgridMenuItem.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DtgridMenuItem_CellClick);
            this.DtgridMenuItem.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DtgridMenuItem_CellDoubleClick);
            this.DtgridMenuItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DtgridMenuItem_KeyDown);
            // 
            // Chkshowdelet
            // 
            this.Chkshowdelet.AutoSize = true;
            this.Chkshowdelet.BackColor = System.Drawing.Color.Transparent;
            this.Chkshowdelet.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Chkshowdelet.ForeColor = System.Drawing.Color.Black;
            this.Chkshowdelet.Location = new System.Drawing.Point(134, 8);
            this.Chkshowdelet.Name = "Chkshowdelet";
            this.Chkshowdelet.Size = new System.Drawing.Size(118, 18);
            this.Chkshowdelet.TabIndex = 474;
            this.Chkshowdelet.Text = "Show Deleted";
            this.Chkshowdelet.UseVisualStyleBackColor = false;
            this.Chkshowdelet.CheckedChanged += new System.EventHandler(this.Chkshowdelet_CheckedChanged);
            // 
            // txtHSNno
            // 
            this.txtHSNno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHSNno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtHSNno.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHSNno.Location = new System.Drawing.Point(437, 90);
            this.txtHSNno.Name = "txtHSNno";
            this.txtHSNno.Size = new System.Drawing.Size(176, 22);
            this.txtHSNno.TabIndex = 1;
            this.txtHSNno.Enter += new System.EventHandler(this.txtHSNno_Enter);
            this.txtHSNno.Leave += new System.EventHandler(this.txtHSNno_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(363, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 14);
            this.label2.TabIndex = 485;
            this.label2.Text = "HSN No. :";
            // 
            // btngrpBrws
            // 
            this.btngrpBrws.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btngrpBrws.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.btngrpBrws.Location = new System.Drawing.Point(494, 118);
            this.btngrpBrws.Name = "btngrpBrws";
            this.btngrpBrws.Size = new System.Drawing.Size(27, 22);
            this.btngrpBrws.TabIndex = 490;
            this.btngrpBrws.Text = "...";
            this.btngrpBrws.UseVisualStyleBackColor = true;
            this.btngrpBrws.Click += new System.EventHandler(this.btngrpBrws_Click);
            // 
            // txtGropId
            // 
            this.txtGropId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGropId.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGropId.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGropId.Location = new System.Drawing.Point(437, 118);
            this.txtGropId.Name = "txtGropId";
            this.txtGropId.Size = new System.Drawing.Size(56, 22);
            this.txtGropId.TabIndex = 2;
            this.txtGropId.Enter += new System.EventHandler(this.txtGropId_Enter);
            this.txtGropId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGropId_KeyDown);
            this.txtGropId.Leave += new System.EventHandler(this.txtGropId_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(379, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 14);
            this.label4.TabIndex = 489;
            this.label4.Text = "Group :";
            // 
            // txtSaleRate
            // 
            this.txtSaleRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSaleRate.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSaleRate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSaleRate.Location = new System.Drawing.Point(607, 180);
            this.txtSaleRate.Name = "txtSaleRate";
            this.txtSaleRate.Size = new System.Drawing.Size(89, 22);
            this.txtSaleRate.TabIndex = 5;
            this.txtSaleRate.Enter += new System.EventHandler(this.txtSaleRate_Enter);
            this.txtSaleRate.Leave += new System.EventHandler(this.txtSaleRate_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(529, 184);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 14);
            this.label6.TabIndex = 494;
            this.label6.Text = "Sale Rate :";
            // 
            // txtPurchaseRate
            // 
            this.txtPurchaseRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPurchaseRate.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPurchaseRate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPurchaseRate.Location = new System.Drawing.Point(437, 180);
            this.txtPurchaseRate.Name = "txtPurchaseRate";
            this.txtPurchaseRate.Size = new System.Drawing.Size(89, 22);
            this.txtPurchaseRate.TabIndex = 4;
            this.txtPurchaseRate.TextChanged += new System.EventHandler(this.txtPurchaseRate_TextChanged);
            this.txtPurchaseRate.Enter += new System.EventHandler(this.txtPurchaseRate_Enter);
            this.txtPurchaseRate.Leave += new System.EventHandler(this.txtPurchaseRate_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(319, 183);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 14);
            this.label5.TabIndex = 493;
            this.label5.Text = " Purchase Rate :";
            // 
            // txtOpeningStock
            // 
            this.txtOpeningStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOpeningStock.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtOpeningStock.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOpeningStock.Location = new System.Drawing.Point(437, 244);
            this.txtOpeningStock.Name = "txtOpeningStock";
            this.txtOpeningStock.Size = new System.Drawing.Size(113, 22);
            this.txtOpeningStock.TabIndex = 7;
            this.txtOpeningStock.Enter += new System.EventHandler(this.txtOpeningStock_Enter);
            this.txtOpeningStock.Leave += new System.EventHandler(this.txtOpeningStock_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(322, 248);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 14);
            this.label7.TabIndex = 495;
            this.label7.Text = "Opening Stock :";
            // 
            // combOpenStockCrDr
            // 
            this.combOpenStockCrDr.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combOpenStockCrDr.FormattingEnabled = true;
            this.combOpenStockCrDr.Location = new System.Drawing.Point(556, 245);
            this.combOpenStockCrDr.Name = "combOpenStockCrDr";
            this.combOpenStockCrDr.Size = new System.Drawing.Size(83, 21);
            this.combOpenStockCrDr.TabIndex = 8;
            this.combOpenStockCrDr.Enter += new System.EventHandler(this.combOpenStockCrDr_Enter);
            this.combOpenStockCrDr.Leave += new System.EventHandler(this.combOpenStockCrDr_Leave);
            // 
            // combClosingStockCrDr
            // 
            this.combClosingStockCrDr.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combClosingStockCrDr.FormattingEnabled = true;
            this.combClosingStockCrDr.Location = new System.Drawing.Point(556, 274);
            this.combClosingStockCrDr.Name = "combClosingStockCrDr";
            this.combClosingStockCrDr.Size = new System.Drawing.Size(83, 21);
            this.combClosingStockCrDr.TabIndex = 499;
            this.combClosingStockCrDr.Enter += new System.EventHandler(this.combClosingStockCrDr_Enter);
            this.combClosingStockCrDr.Leave += new System.EventHandler(this.combClosingStockCrDr_Leave);
            // 
            // txtClosingStock
            // 
            this.txtClosingStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtClosingStock.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtClosingStock.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClosingStock.Location = new System.Drawing.Point(437, 275);
            this.txtClosingStock.Name = "txtClosingStock";
            this.txtClosingStock.Size = new System.Drawing.Size(113, 22);
            this.txtClosingStock.TabIndex = 498;
            this.txtClosingStock.Enter += new System.EventHandler(this.txtClosingStock_Enter);
            this.txtClosingStock.Leave += new System.EventHandler(this.txtClosingStock_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(329, 277);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 14);
            this.label8.TabIndex = 497;
            this.label8.Text = "Closing Stock :";
            // 
            // txtMinQty
            // 
            this.txtMinQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMinQty.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMinQty.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMinQty.Location = new System.Drawing.Point(436, 338);
            this.txtMinQty.Name = "txtMinQty";
            this.txtMinQty.Size = new System.Drawing.Size(116, 22);
            this.txtMinQty.TabIndex = 10;
            this.txtMinQty.Enter += new System.EventHandler(this.txtMinQty_Enter);
            this.txtMinQty.Leave += new System.EventHandler(this.txtMinQty_Leave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(364, 340);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 14);
            this.label9.TabIndex = 503;
            this.label9.Text = "Min. Qty :";
            // 
            // txtReorderQty
            // 
            this.txtReorderQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReorderQty.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtReorderQty.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReorderQty.Location = new System.Drawing.Point(437, 306);
            this.txtReorderQty.Name = "txtReorderQty";
            this.txtReorderQty.Size = new System.Drawing.Size(113, 22);
            this.txtReorderQty.TabIndex = 9;
            this.txtReorderQty.Enter += new System.EventHandler(this.txtReorderQty_Enter);
            this.txtReorderQty.Leave += new System.EventHandler(this.txtReorderQty_Leave);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(329, 310);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(106, 14);
            this.label10.TabIndex = 501;
            this.label10.Text = "Re-Order Qty :";
            // 
            // txtMaxQty
            // 
            this.txtMaxQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaxQty.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMaxQty.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxQty.Location = new System.Drawing.Point(436, 367);
            this.txtMaxQty.Name = "txtMaxQty";
            this.txtMaxQty.Size = new System.Drawing.Size(116, 22);
            this.txtMaxQty.TabIndex = 11;
            this.txtMaxQty.Enter += new System.EventHandler(this.txtMaxQty_Enter);
            this.txtMaxQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMaxQty_KeyPress);
            this.txtMaxQty.Leave += new System.EventHandler(this.txtMaxQty_Leave);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(360, 369);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(75, 14);
            this.label12.TabIndex = 505;
            this.label12.Text = "Max. Qty :";
            // 
            // lblSucessMsg
            // 
            this.lblSucessMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblSucessMsg.Font = new System.Drawing.Font("Verdana", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSucessMsg.ForeColor = System.Drawing.Color.Black;
            this.lblSucessMsg.Location = new System.Drawing.Point(7, 508);
            this.lblSucessMsg.Name = "lblSucessMsg";
            this.lblSucessMsg.Size = new System.Drawing.Size(260, 24);
            this.lblSucessMsg.TabIndex = 507;
            // 
            // btnLessID
            // 
            this.btnLessID.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnLessID.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLessID.Location = new System.Drawing.Point(321, 486);
            this.btnLessID.Name = "btnLessID";
            this.btnLessID.Size = new System.Drawing.Size(36, 25);
            this.btnLessID.TabIndex = 550;
            this.btnLessID.Text = "<<";
            this.btnLessID.UseVisualStyleBackColor = true;
            this.btnLessID.Click += new System.EventHandler(this.btnLessID_Click);
            // 
            // btnGreaterID
            // 
            this.btnGreaterID.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnGreaterID.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGreaterID.Location = new System.Drawing.Point(358, 486);
            this.btnGreaterID.Name = "btnGreaterID";
            this.btnGreaterID.Size = new System.Drawing.Size(36, 25);
            this.btnGreaterID.TabIndex = 549;
            this.btnGreaterID.Text = ">>";
            this.btnGreaterID.UseVisualStyleBackColor = true;
            this.btnGreaterID.Click += new System.EventHandler(this.btnGreaterID_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.lblGroupName);
            this.pnlMain.Controls.Add(this.btnLessID);
            this.pnlMain.Controls.Add(this.btnGreaterID);
            this.pnlMain.Controls.Add(this.lblSucessMsg);
            this.pnlMain.Controls.Add(this.txtMaxQty);
            this.pnlMain.Controls.Add(this.label12);
            this.pnlMain.Controls.Add(this.txtMinQty);
            this.pnlMain.Controls.Add(this.label9);
            this.pnlMain.Controls.Add(this.txtReorderQty);
            this.pnlMain.Controls.Add(this.label10);
            this.pnlMain.Controls.Add(this.combOpenStockCrDr);
            this.pnlMain.Controls.Add(this.combClosingStockCrDr);
            this.pnlMain.Controls.Add(this.txtClosingStock);
            this.pnlMain.Controls.Add(this.label8);
            this.pnlMain.Controls.Add(this.txtOpeningStock);
            this.pnlMain.Controls.Add(this.label7);
            this.pnlMain.Controls.Add(this.txtSaleRate);
            this.pnlMain.Controls.Add(this.label6);
            this.pnlMain.Controls.Add(this.txtPurchaseRate);
            this.pnlMain.Controls.Add(this.label5);
            this.pnlMain.Controls.Add(this.btngrpBrws);
            this.pnlMain.Controls.Add(this.txtGropId);
            this.pnlMain.Controls.Add(this.label4);
            this.pnlMain.Controls.Add(this.txtHSNno);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.cmbCategry);
            this.pnlMain.Controls.Add(this.txtsearch);
            this.pnlMain.Controls.Add(this.txtcode);
            this.pnlMain.Controls.Add(this.BtnExit);
            this.pnlMain.Controls.Add(this.BtnDelete);
            this.pnlMain.Controls.Add(this.BtnSave);
            this.pnlMain.Controls.Add(this.BtnAdd);
            this.pnlMain.Controls.Add(this.label11);
            this.pnlMain.Controls.Add(this.txtItemname);
            this.pnlMain.Controls.Add(this.label3);
            this.pnlMain.Controls.Add(this.label21);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.DtgridMenuItem);
            this.pnlMain.Controls.Add(this.Chkshowdelet);
            this.pnlMain.Location = new System.Drawing.Point(6, 4);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(711, 545);
            this.pnlMain.TabIndex = 551;
            // 
            // lblGroupName
            // 
            this.lblGroupName.BackColor = System.Drawing.Color.White;
            this.lblGroupName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblGroupName.Location = new System.Drawing.Point(527, 118);
            this.lblGroupName.Name = "lblGroupName";
            this.lblGroupName.Size = new System.Drawing.Size(169, 22);
            this.lblGroupName.TabIndex = 661;
            this.lblGroupName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmMenuItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(734, 556);
            this.Controls.Add(this.pnlMain);
            this.Name = "FrmMenuItem";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu Item";
            this.Load += new System.EventHandler(this.FrmMenuItem_Load);
            this.Shown += new System.EventHandler(this.FrmMenuItem_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMenuItem_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmMenuItem_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.DtgridMenuItem)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbCategry;
        private System.Windows.Forms.TextBox txtsearch;
        private System.Windows.Forms.TextBox txtcode;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.Button BtnDelete;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtItemname;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView DtgridMenuItem;
        private System.Windows.Forms.CheckBox Chkshowdelet;
        private System.Windows.Forms.TextBox txtHSNno;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btngrpBrws;
        private System.Windows.Forms.TextBox txtGropId;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSaleRate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPurchaseRate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtOpeningStock;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox combOpenStockCrDr;
        private System.Windows.Forms.ComboBox combClosingStockCrDr;
        private System.Windows.Forms.TextBox txtClosingStock;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtMinQty;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtReorderQty;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtMaxQty;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblSucessMsg;
        private System.Windows.Forms.Button btnLessID;
        private System.Windows.Forms.Button btnGreaterID;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblGroupName;
    }
}
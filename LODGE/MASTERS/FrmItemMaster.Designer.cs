namespace ASPL.LODGE.MASTERS
{
    partial class FrmItemMaster
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmbPayable = new System.Windows.Forms.ComboBox();
            this.txtsearch = new System.Windows.Forms.TextBox();
            this.txtcode = new System.Windows.Forms.TextBox();
            this.BtnExit = new System.Windows.Forms.Button();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.txtname = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.DtgridItemcard = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.Chkshowdelet = new System.Windows.Forms.CheckBox();
            this.txtTallyCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtItemRate = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbservicetax = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbActive = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dtgridtax = new System.Windows.Forms.DataGridView();
            this.chkSaleMode = new System.Windows.Forms.CheckBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblSucessMsg = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.btnLessID = new System.Windows.Forms.Button();
            this.btnGreaterID = new System.Windows.Forms.Button();
            this.lblTallyName = new System.Windows.Forms.Label();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drpDes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tax_Percentage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DtgridItemcard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgridtax)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbPayable
            // 
            this.cmbPayable.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPayable.FormattingEnabled = true;
            this.cmbPayable.Location = new System.Drawing.Point(493, 123);
            this.cmbPayable.Name = "cmbPayable";
            this.cmbPayable.Size = new System.Drawing.Size(117, 21);
            this.cmbPayable.TabIndex = 2;
            this.cmbPayable.SelectedIndexChanged += new System.EventHandler(this.cmbPayable_SelectedIndexChanged);
            this.cmbPayable.Enter += new System.EventHandler(this.cmbPayable_Enter);
            this.cmbPayable.Leave += new System.EventHandler(this.cmbPayable_Leave);
            // 
            // txtsearch
            // 
            this.txtsearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtsearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtsearch.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsearch.Location = new System.Drawing.Point(141, 31);
            this.txtsearch.Name = "txtsearch";
            this.txtsearch.Size = new System.Drawing.Size(183, 22);
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
            this.txtcode.Location = new System.Drawing.Point(493, 57);
            this.txtcode.Name = "txtcode";
            this.txtcode.Size = new System.Drawing.Size(87, 22);
            this.txtcode.TabIndex = 404;
            // 
            // BtnExit
            // 
            this.BtnExit.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Location = new System.Drawing.Point(667, 482);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(75, 25);
            this.BtnExit.TabIndex = 403;
            this.BtnExit.Text = "EXIT";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Location = new System.Drawing.Point(584, 482);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(75, 25);
            this.BtnDelete.TabIndex = 402;
            this.BtnDelete.Text = "DELETE";
            this.BtnDelete.UseVisualStyleBackColor = true;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Location = new System.Drawing.Point(501, 482);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(75, 25);
            this.BtnSave.TabIndex = 401;
            this.BtnSave.Text = "SAVE";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnAdd
            // 
            this.BtnAdd.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BtnAdd.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAdd.Location = new System.Drawing.Point(418, 482);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(75, 25);
            this.BtnAdd.TabIndex = 400;
            this.BtnAdd.Text = "&ADD";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(430, 126);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 19);
            this.label11.TabIndex = 399;
            this.label11.Text = "Payble :";
            // 
            // txtname
            // 
            this.txtname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtname.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtname.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtname.Location = new System.Drawing.Point(493, 90);
            this.txtname.MaxLength = 50;
            this.txtname.Name = "txtname";
            this.txtname.Size = new System.Drawing.Size(201, 22);
            this.txtname.TabIndex = 1;
            this.txtname.Enter += new System.EventHandler(this.txtname_Enter);
            this.txtname.Leave += new System.EventHandler(this.txtname_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(429, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 14);
            this.label3.TabIndex = 398;
            this.label3.Text = "  Name :";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(438, 61);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(53, 14);
            this.label21.TabIndex = 397;
            this.label21.Text = " Code :";
            // 
            // DtgridItemcard
            // 
            this.DtgridItemcard.AllowUserToAddRows = false;
            this.DtgridItemcard.AllowUserToDeleteRows = false;
            this.DtgridItemcard.AllowUserToResizeColumns = false;
            this.DtgridItemcard.AllowUserToResizeRows = false;
            this.DtgridItemcard.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DtgridItemcard.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DtgridItemcard.ColumnHeadersHeight = 25;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DtgridItemcard.DefaultCellStyle = dataGridViewCellStyle2;
            this.DtgridItemcard.EnableHeadersVisualStyles = false;
            this.DtgridItemcard.Location = new System.Drawing.Point(11, 57);
            this.DtgridItemcard.MultiSelect = false;
            this.DtgridItemcard.Name = "DtgridItemcard";
            this.DtgridItemcard.ReadOnly = true;
            this.DtgridItemcard.RowHeadersVisible = false;
            this.DtgridItemcard.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DtgridItemcard.Size = new System.Drawing.Size(313, 450);
            this.DtgridItemcard.TabIndex = 396;
            this.DtgridItemcard.TabStop = false;
            this.DtgridItemcard.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DtgridItemcard_CellClick);
            this.DtgridItemcard.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DtgridItemcard_CellDoubleClick);
            this.DtgridItemcard.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DtgridItemcard_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 14);
            this.label1.TabIndex = 395;
            this.label1.Text = "Search by Name :";
            // 
            // Chkshowdelet
            // 
            this.Chkshowdelet.AutoSize = true;
            this.Chkshowdelet.BackColor = System.Drawing.Color.Transparent;
            this.Chkshowdelet.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Chkshowdelet.ForeColor = System.Drawing.Color.Black;
            this.Chkshowdelet.Location = new System.Drawing.Point(140, 10);
            this.Chkshowdelet.Name = "Chkshowdelet";
            this.Chkshowdelet.Size = new System.Drawing.Size(118, 18);
            this.Chkshowdelet.TabIndex = 394;
            this.Chkshowdelet.Text = "Show Deleted";
            this.Chkshowdelet.UseVisualStyleBackColor = false;
            this.Chkshowdelet.CheckedChanged += new System.EventHandler(this.Chkshowdelet_CheckedChanged);
            // 
            // txtTallyCode
            // 
            this.txtTallyCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTallyCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTallyCode.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTallyCode.Location = new System.Drawing.Point(494, 156);
            this.txtTallyCode.Name = "txtTallyCode";
            this.txtTallyCode.Size = new System.Drawing.Size(56, 22);
            this.txtTallyCode.TabIndex = 3;
            this.txtTallyCode.Enter += new System.EventHandler(this.txtTallyCode_Enter);
            this.txtTallyCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTallyCode_KeyDown);
            this.txtTallyCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTallyCode_KeyPress);
            this.txtTallyCode.Leave += new System.EventHandler(this.txtTallyCode_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(407, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 14);
            this.label2.TabIndex = 455;
            this.label2.Text = "Tally Code :";
            // 
            // txtItemRate
            // 
            this.txtItemRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtItemRate.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtItemRate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtItemRate.Location = new System.Drawing.Point(494, 189);
            this.txtItemRate.Name = "txtItemRate";
            this.txtItemRate.Size = new System.Drawing.Size(116, 22);
            this.txtItemRate.TabIndex = 4;
            this.txtItemRate.Enter += new System.EventHandler(this.txtItemRate_Enter);
            this.txtItemRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtItemRate_KeyPress);
            this.txtItemRate.Leave += new System.EventHandler(this.txtItemRate_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(407, 192);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 14);
            this.label4.TabIndex = 457;
            this.label4.Text = " Item Rate :";
            // 
            // cmbCategory
            // 
            this.cmbCategory.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(494, 221);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(116, 21);
            this.cmbCategory.TabIndex = 5;
            this.cmbCategory.Enter += new System.EventHandler(this.cmbCategory_Enter);
            this.cmbCategory.Leave += new System.EventHandler(this.cmbCategory_Leave);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(416, 224);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 20);
            this.label5.TabIndex = 458;
            this.label5.Text = "Category :";
            // 
            // cmbservicetax
            // 
            this.cmbservicetax.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbservicetax.FormattingEnabled = true;
            this.cmbservicetax.Location = new System.Drawing.Point(494, 251);
            this.cmbservicetax.Name = "cmbservicetax";
            this.cmbservicetax.Size = new System.Drawing.Size(116, 21);
            this.cmbservicetax.TabIndex = 6;
            this.cmbservicetax.SelectedIndexChanged += new System.EventHandler(this.cmbservicetax_SelectedIndexChanged);
            this.cmbservicetax.Enter += new System.EventHandler(this.cmbservicetax_Enter);
            this.cmbservicetax.Leave += new System.EventHandler(this.cmbservicetax_Leave);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(327, 253);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(166, 21);
            this.label6.TabIndex = 460;
            this.label6.Text = "Service Tax Applicable :";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(332, 277);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(162, 19);
            this.label7.TabIndex = 462;
            this.label7.Text = "Restaurant Sale Mode :";
            // 
            // cmbActive
            // 
            this.cmbActive.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbActive.FormattingEnabled = true;
            this.cmbActive.Location = new System.Drawing.Point(494, 302);
            this.cmbActive.Name = "cmbActive";
            this.cmbActive.Size = new System.Drawing.Size(116, 21);
            this.cmbActive.TabIndex = 8;
            this.cmbActive.Enter += new System.EventHandler(this.cmbActive_Enter);
            this.cmbActive.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbActive_KeyPress);
            this.cmbActive.Leave += new System.EventHandler(this.cmbActive_Leave);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(435, 305);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 19);
            this.label8.TabIndex = 463;
            this.label8.Text = "Active :";
            // 
            // dtgridtax
            // 
            this.dtgridtax.AllowUserToAddRows = false;
            this.dtgridtax.AllowUserToResizeColumns = false;
            this.dtgridtax.AllowUserToResizeRows = false;
            this.dtgridtax.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgridtax.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dtgridtax.ColumnHeadersHeight = 25;
            this.dtgridtax.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.dataGridViewTextBoxColumn3,
            this.drpDes,
            this.Tax_Percentage});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgridtax.DefaultCellStyle = dataGridViewCellStyle7;
            this.dtgridtax.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dtgridtax.Enabled = false;
            this.dtgridtax.EnableHeadersVisualStyles = false;
            this.dtgridtax.Location = new System.Drawing.Point(329, 329);
            this.dtgridtax.Margin = new System.Windows.Forms.Padding(2);
            this.dtgridtax.Name = "dtgridtax";
            this.dtgridtax.RowHeadersVisible = false;
            this.dtgridtax.RowTemplate.Height = 24;
            this.dtgridtax.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dtgridtax.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dtgridtax.Size = new System.Drawing.Size(413, 148);
            this.dtgridtax.TabIndex = 9;
            this.dtgridtax.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgridtax_CellEnter);
            this.dtgridtax.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgridtax_CellLeave);
            this.dtgridtax.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dtgridtax_EditingControlShowing);
            // 
            // chkSaleMode
            // 
            this.chkSaleMode.AutoSize = true;
            this.chkSaleMode.BackColor = System.Drawing.Color.Transparent;
            this.chkSaleMode.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSaleMode.ForeColor = System.Drawing.Color.Black;
            this.chkSaleMode.Location = new System.Drawing.Point(496, 279);
            this.chkSaleMode.Name = "chkSaleMode";
            this.chkSaleMode.Size = new System.Drawing.Size(15, 14);
            this.chkSaleMode.TabIndex = 7;
            this.chkSaleMode.UseVisualStyleBackColor = false;
            this.chkSaleMode.CheckedChanged += new System.EventHandler(this.chkSaleMode_CheckedChanged);
            this.chkSaleMode.Enter += new System.EventHandler(this.chkSaleMode_Enter);
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.Red;
            this.label22.Location = new System.Drawing.Point(696, 88);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(10, 10);
            this.label22.TabIndex = 464;
            this.label22.Text = "*";
            this.label22.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(616, 189);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(10, 10);
            this.label9.TabIndex = 465;
            this.label9.Text = "*";
            this.label9.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // lblSucessMsg
            // 
            this.lblSucessMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblSucessMsg.Font = new System.Drawing.Font("Verdana", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSucessMsg.ForeColor = System.Drawing.Color.Black;
            this.lblSucessMsg.Location = new System.Drawing.Point(16, 510);
            this.lblSucessMsg.Name = "lblSucessMsg";
            this.lblSucessMsg.Size = new System.Drawing.Size(260, 24);
            this.lblSucessMsg.TabIndex = 505;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.btnLessID);
            this.pnlMain.Controls.Add(this.btnGreaterID);
            this.pnlMain.Controls.Add(this.lblTallyName);
            this.pnlMain.Controls.Add(this.lblSucessMsg);
            this.pnlMain.Controls.Add(this.label9);
            this.pnlMain.Controls.Add(this.label22);
            this.pnlMain.Controls.Add(this.chkSaleMode);
            this.pnlMain.Controls.Add(this.dtgridtax);
            this.pnlMain.Controls.Add(this.cmbActive);
            this.pnlMain.Controls.Add(this.label8);
            this.pnlMain.Controls.Add(this.label7);
            this.pnlMain.Controls.Add(this.cmbservicetax);
            this.pnlMain.Controls.Add(this.label6);
            this.pnlMain.Controls.Add(this.cmbCategory);
            this.pnlMain.Controls.Add(this.label5);
            this.pnlMain.Controls.Add(this.txtItemRate);
            this.pnlMain.Controls.Add(this.label4);
            this.pnlMain.Controls.Add(this.txtTallyCode);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.cmbPayable);
            this.pnlMain.Controls.Add(this.txtsearch);
            this.pnlMain.Controls.Add(this.txtcode);
            this.pnlMain.Controls.Add(this.BtnExit);
            this.pnlMain.Controls.Add(this.BtnDelete);
            this.pnlMain.Controls.Add(this.BtnSave);
            this.pnlMain.Controls.Add(this.BtnAdd);
            this.pnlMain.Controls.Add(this.label11);
            this.pnlMain.Controls.Add(this.txtname);
            this.pnlMain.Controls.Add(this.label3);
            this.pnlMain.Controls.Add(this.label21);
            this.pnlMain.Controls.Add(this.DtgridItemcard);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.Chkshowdelet);
            this.pnlMain.Location = new System.Drawing.Point(12, 12);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(750, 548);
            this.pnlMain.TabIndex = 506;
            this.pnlMain.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlMain_Paint);
            // 
            // btnLessID
            // 
            this.btnLessID.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnLessID.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLessID.Location = new System.Drawing.Point(330, 482);
            this.btnLessID.Name = "btnLessID";
            this.btnLessID.Size = new System.Drawing.Size(36, 25);
            this.btnLessID.TabIndex = 665;
            this.btnLessID.Text = "<<";
            this.btnLessID.UseVisualStyleBackColor = true;
            this.btnLessID.Click += new System.EventHandler(this.btnLessID_Click);
            // 
            // btnGreaterID
            // 
            this.btnGreaterID.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnGreaterID.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGreaterID.Location = new System.Drawing.Point(374, 482);
            this.btnGreaterID.Name = "btnGreaterID";
            this.btnGreaterID.Size = new System.Drawing.Size(36, 25);
            this.btnGreaterID.TabIndex = 664;
            this.btnGreaterID.Text = ">>";
            this.btnGreaterID.UseVisualStyleBackColor = true;
            this.btnGreaterID.Click += new System.EventHandler(this.btnGreaterID_Click);
            // 
            // lblTallyName
            // 
            this.lblTallyName.BackColor = System.Drawing.Color.White;
            this.lblTallyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTallyName.Location = new System.Drawing.Point(556, 156);
            this.lblTallyName.Name = "lblTallyName";
            this.lblTallyName.Size = new System.Drawing.Size(138, 22);
            this.lblTallyName.TabIndex = 661;
            this.lblTallyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Column3
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column3.FillWeight = 60F;
            this.Column3.HeaderText = "Sr. No.";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 60;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTextBoxColumn3.FillWeight = 80F;
            this.dataGridViewTextBoxColumn3.HeaderText = "Tax Code";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn3.Width = 80;
            // 
            // drpDes
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.drpDes.DefaultCellStyle = dataGridViewCellStyle6;
            this.drpDes.FillWeight = 150F;
            this.drpDes.HeaderText = "Tax Name";
            this.drpDes.Name = "drpDes";
            this.drpDes.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.drpDes.Width = 150;
            // 
            // Tax_Percentage
            // 
            this.Tax_Percentage.HeaderText = "Tax %";
            this.Tax_Percentage.Name = "Tax_Percentage";
            // 
            // FrmItemMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(775, 573);
            this.ControlBox = false;
            this.Controls.Add(this.pnlMain);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmItemMaster";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Menu Group";
            this.Load += new System.EventHandler(this.FrmItemMaster_Load);
            this.Shown += new System.EventHandler(this.FrmItemMaster_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmItemMaster_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmItemMaster_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.DtgridItemcard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgridtax)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbPayable;
        private System.Windows.Forms.TextBox txtsearch;
        private System.Windows.Forms.TextBox txtcode;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.Button BtnDelete;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtname;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.DataGridView DtgridItemcard;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox Chkshowdelet;
        private System.Windows.Forms.TextBox txtTallyCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtItemRate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbservicetax;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbActive;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dtgridtax;
        private System.Windows.Forms.CheckBox chkSaleMode;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblSucessMsg;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblTallyName;
        private System.Windows.Forms.Button btnLessID;
        private System.Windows.Forms.Button btnGreaterID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn drpDes;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tax_Percentage;
    }
}
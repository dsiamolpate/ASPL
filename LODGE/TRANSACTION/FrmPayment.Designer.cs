namespace ASPL.LODGE.TRANSCTION
{
    partial class FrmPayment
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabPayment = new System.Windows.Forms.TabControl();
            this.tabList = new System.Windows.Forms.TabPage();
            this.txtsearch = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.BtnExit = new System.Windows.Forms.Button();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.dtgridPayment = new System.Windows.Forms.DataGridView();
            this.tabDetail = new System.Windows.Forms.TabPage();
            this.txtBranchCode = new System.Windows.Forms.TextBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.txtBranch = new System.Windows.Forms.TextBox();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtTranstypeNM = new System.Windows.Forms.TextBox();
            this.txtTranstype = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbCreditDebit = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnLessID = new System.Windows.Forms.Button();
            this.btnGreaterID = new System.Windows.Forms.Button();
            this.BtnPrint = new System.Windows.Forms.Button();
            this.txtNarration = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.dtgridGLcode = new System.Windows.Forms.DataGridView();
            this.colmGlCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colmdescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colmAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCheckNo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBookCodeNM = new System.Windows.Forms.TextBox();
            this.txtBookCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpVoucherDate = new System.Windows.Forms.DateTimePicker();
            this.label20 = new System.Windows.Forms.Label();
            this.txtVoucherNo = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.tabPayment.SuspendLayout();
            this.tabList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgridPayment)).BeginInit();
            this.tabDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgridGLcode)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPayment
            // 
            this.tabPayment.Controls.Add(this.tabList);
            this.tabPayment.Controls.Add(this.tabDetail);
            this.tabPayment.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPayment.ItemSize = new System.Drawing.Size(100, 21);
            this.tabPayment.Location = new System.Drawing.Point(11, 10);
            this.tabPayment.Name = "tabPayment";
            this.tabPayment.SelectedIndex = 0;
            this.tabPayment.Size = new System.Drawing.Size(695, 534);
            this.tabPayment.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabPayment.TabIndex = 2;
            this.tabPayment.TabStop = false;
            this.tabPayment.SelectedIndexChanged += new System.EventHandler(this.tabPayment_SelectedIndexChanged);
            this.tabPayment.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabPayment_Selecting);
            // 
            // tabList
            // 
            this.tabList.BackColor = System.Drawing.Color.White;
            this.tabList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabList.Controls.Add(this.txtsearch);
            this.tabList.Controls.Add(this.label22);
            this.tabList.Controls.Add(this.BtnExit);
            this.tabList.Controls.Add(this.BtnAdd);
            this.tabList.Controls.Add(this.dtgridPayment);
            this.tabList.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabList.Location = new System.Drawing.Point(4, 25);
            this.tabList.Name = "tabList";
            this.tabList.Padding = new System.Windows.Forms.Padding(3);
            this.tabList.Size = new System.Drawing.Size(687, 505);
            this.tabList.TabIndex = 0;
            this.tabList.Text = "List";
            // 
            // txtsearch
            // 
            this.txtsearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtsearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtsearch.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsearch.Location = new System.Drawing.Point(137, 8);
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
            this.label22.Location = new System.Drawing.Point(9, 11);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(124, 14);
            this.label22.TabIndex = 594;
            this.label22.Text = "Search by Name :";
            // 
            // BtnExit
            // 
            this.BtnExit.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Location = new System.Drawing.Point(591, 461);
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
            this.BtnAdd.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAdd.Location = new System.Drawing.Point(482, 461);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(90, 25);
            this.BtnAdd.TabIndex = 1;
            this.BtnAdd.Text = "&ADD";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // dtgridPayment
            // 
            this.dtgridPayment.AllowUserToAddRows = false;
            this.dtgridPayment.AllowUserToDeleteRows = false;
            this.dtgridPayment.AllowUserToResizeColumns = false;
            this.dtgridPayment.AllowUserToResizeRows = false;
            this.dtgridPayment.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgridPayment.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgridPayment.ColumnHeadersHeight = 25;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgridPayment.DefaultCellStyle = dataGridViewCellStyle2;
            this.dtgridPayment.EnableHeadersVisualStyles = false;
            this.dtgridPayment.Location = new System.Drawing.Point(4, 32);
            this.dtgridPayment.Name = "dtgridPayment";
            this.dtgridPayment.RowHeadersVisible = false;
            this.dtgridPayment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgridPayment.Size = new System.Drawing.Size(680, 422);
            this.dtgridPayment.TabIndex = 576;
            this.dtgridPayment.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgridPayment_CellDoubleClick);
            this.dtgridPayment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtgridPayment_KeyDown);
            // 
            // tabDetail
            // 
            this.tabDetail.BackColor = System.Drawing.Color.White;
            this.tabDetail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabDetail.Controls.Add(this.txtBranchCode);
            this.tabDetail.Controls.Add(this.txtAmount);
            this.tabDetail.Controls.Add(this.txtBranch);
            this.tabDetail.Controls.Add(this.BtnCancel);
            this.tabDetail.Controls.Add(this.label18);
            this.tabDetail.Controls.Add(this.label16);
            this.tabDetail.Controls.Add(this.textBox5);
            this.tabDetail.Controls.Add(this.textBox6);
            this.tabDetail.Controls.Add(this.label17);
            this.tabDetail.Controls.Add(this.label14);
            this.tabDetail.Controls.Add(this.textBox3);
            this.tabDetail.Controls.Add(this.textBox4);
            this.tabDetail.Controls.Add(this.label15);
            this.tabDetail.Controls.Add(this.label12);
            this.tabDetail.Controls.Add(this.txtTranstypeNM);
            this.tabDetail.Controls.Add(this.txtTranstype);
            this.tabDetail.Controls.Add(this.label13);
            this.tabDetail.Controls.Add(this.label10);
            this.tabDetail.Controls.Add(this.cmbCreditDebit);
            this.tabDetail.Controls.Add(this.label11);
            this.tabDetail.Controls.Add(this.btnLessID);
            this.tabDetail.Controls.Add(this.btnGreaterID);
            this.tabDetail.Controls.Add(this.BtnPrint);
            this.tabDetail.Controls.Add(this.txtNarration);
            this.tabDetail.Controls.Add(this.label9);
            this.tabDetail.Controls.Add(this.BtnDelete);
            this.tabDetail.Controls.Add(this.BtnSave);
            this.tabDetail.Controls.Add(this.dtgridGLcode);
            this.tabDetail.Controls.Add(this.label8);
            this.tabDetail.Controls.Add(this.label4);
            this.tabDetail.Controls.Add(this.txtCheckNo);
            this.tabDetail.Controls.Add(this.label6);
            this.tabDetail.Controls.Add(this.label3);
            this.tabDetail.Controls.Add(this.label1);
            this.tabDetail.Controls.Add(this.label5);
            this.tabDetail.Controls.Add(this.txtBookCodeNM);
            this.tabDetail.Controls.Add(this.txtBookCode);
            this.tabDetail.Controls.Add(this.label2);
            this.tabDetail.Controls.Add(this.dtpVoucherDate);
            this.tabDetail.Controls.Add(this.label20);
            this.tabDetail.Controls.Add(this.txtVoucherNo);
            this.tabDetail.Controls.Add(this.label21);
            this.tabDetail.Location = new System.Drawing.Point(4, 25);
            this.tabDetail.Name = "tabDetail";
            this.tabDetail.Padding = new System.Windows.Forms.Padding(3);
            this.tabDetail.Size = new System.Drawing.Size(687, 505);
            this.tabDetail.TabIndex = 1;
            this.tabDetail.Text = "Details";
            // 
            // txtBranchCode
            // 
            this.txtBranchCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBranchCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBranchCode.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBranchCode.Location = new System.Drawing.Point(110, 83);
            this.txtBranchCode.Name = "txtBranchCode";
            this.txtBranchCode.Size = new System.Drawing.Size(57, 22);
            this.txtBranchCode.TabIndex = 2;
            this.txtBranchCode.Enter += new System.EventHandler(this.txtBranchCode_Enter);
            this.txtBranchCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBranchCode_KeyDown);
            this.txtBranchCode.Leave += new System.EventHandler(this.txtBranchCode_Leave);
            // 
            // txtAmount
            // 
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAmount.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Location = new System.Drawing.Point(110, 119);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(133, 22);
            this.txtAmount.TabIndex = 2;
            this.txtAmount.Enter += new System.EventHandler(this.txtAmount_Enter);
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmount_KeyPress);
            this.txtAmount.Leave += new System.EventHandler(this.txtAmount_Leave);
            // 
            // txtBranch
            // 
            this.txtBranch.BackColor = System.Drawing.SystemColors.Control;
            this.txtBranch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBranch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBranch.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBranch.Location = new System.Drawing.Point(173, 83);
            this.txtBranch.Name = "txtBranch";
            this.txtBranch.ReadOnly = true;
            this.txtBranch.Size = new System.Drawing.Size(268, 22);
            this.txtBranch.TabIndex = 5;
            this.txtBranch.Enter += new System.EventHandler(this.txtBranch_Enter);
            this.txtBranch.Leave += new System.EventHandler(this.txtBranch_Leave);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Location = new System.Drawing.Point(575, 472);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(75, 28);
            this.BtnCancel.TabIndex = 612;
            this.BtnCancel.Text = "CANCEL";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.DarkRed;
            this.label18.Location = new System.Drawing.Point(456, 224);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(86, 14);
            this.label18.TabIndex = 611;
            this.label18.Text = "Bal. Amount";
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Red;
            this.label16.Location = new System.Drawing.Point(442, 253);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(10, 10);
            this.label16.TabIndex = 610;
            this.label16.Text = "*";
            this.label16.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // textBox5
            // 
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox5.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox5.Location = new System.Drawing.Point(175, 254);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(264, 22);
            this.textBox5.TabIndex = 609;
            // 
            // textBox6
            // 
            this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox6.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox6.Location = new System.Drawing.Point(112, 254);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(57, 22);
            this.textBox6.TabIndex = 607;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(23, 258);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(85, 14);
            this.label17.TabIndex = 608;
            this.label17.Text = "PRN Code  :";
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(445, 217);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(10, 10);
            this.label14.TabIndex = 606;
            this.label14.Text = "*";
            this.label14.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // textBox3
            // 
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.Location = new System.Drawing.Point(176, 219);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(264, 22);
            this.textBox3.TabIndex = 605;
            // 
            // textBox4
            // 
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox4.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.Location = new System.Drawing.Point(113, 219);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(57, 22);
            this.textBox4.TabIndex = 603;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(14, 223);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(95, 14);
            this.label15.TabIndex = 604;
            this.label15.Text = "Agent Code  :";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(445, 182);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(10, 10);
            this.label12.TabIndex = 602;
            this.label12.Text = "*";
            this.label12.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // txtTranstypeNM
            // 
            this.txtTranstypeNM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTranstypeNM.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTranstypeNM.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTranstypeNM.Location = new System.Drawing.Point(176, 185);
            this.txtTranstypeNM.Name = "txtTranstypeNM";
            this.txtTranstypeNM.ReadOnly = true;
            this.txtTranstypeNM.Size = new System.Drawing.Size(264, 22);
            this.txtTranstypeNM.TabIndex = 601;
            // 
            // txtTranstype
            // 
            this.txtTranstype.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTranstype.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTranstype.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTranstype.Location = new System.Drawing.Point(112, 185);
            this.txtTranstype.Name = "txtTranstype";
            this.txtTranstype.Size = new System.Drawing.Size(57, 22);
            this.txtTranstype.TabIndex = 599;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(15, 188);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(93, 14);
            this.label13.TabIndex = 600;
            this.label13.Text = "Trans Type  :";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(251, 151);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(10, 10);
            this.label10.TabIndex = 598;
            this.label10.Text = "*";
            this.label10.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // cmbCreditDebit
            // 
            this.cmbCreditDebit.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCreditDebit.FormattingEnabled = true;
            this.cmbCreditDebit.Location = new System.Drawing.Point(111, 152);
            this.cmbCreditDebit.Name = "cmbCreditDebit";
            this.cmbCreditDebit.Size = new System.Drawing.Size(134, 21);
            this.cmbCreditDebit.TabIndex = 4;
            this.cmbCreditDebit.Enter += new System.EventHandler(this.cmbCreditDebit_Enter);
            this.cmbCreditDebit.Leave += new System.EventHandler(this.cmbCreditDebit_Leave);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(41, 155);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 14);
            this.label11.TabIndex = 597;
            this.label11.Text = "C/D/O  :";
            // 
            // btnLessID
            // 
            this.btnLessID.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnLessID.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLessID.Location = new System.Drawing.Point(160, 472);
            this.btnLessID.Name = "btnLessID";
            this.btnLessID.Size = new System.Drawing.Size(75, 28);
            this.btnLessID.TabIndex = 595;
            this.btnLessID.Text = "<<";
            this.btnLessID.UseVisualStyleBackColor = true;
            this.btnLessID.Click += new System.EventHandler(this.btnLessID_Click);
            // 
            // btnGreaterID
            // 
            this.btnGreaterID.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnGreaterID.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGreaterID.Location = new System.Drawing.Point(244, 472);
            this.btnGreaterID.Name = "btnGreaterID";
            this.btnGreaterID.Size = new System.Drawing.Size(75, 28);
            this.btnGreaterID.TabIndex = 594;
            this.btnGreaterID.Text = ">>";
            this.btnGreaterID.UseVisualStyleBackColor = true;
            this.btnGreaterID.Click += new System.EventHandler(this.btnGreaterID_Click);
            // 
            // BtnPrint
            // 
            this.BtnPrint.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPrint.Location = new System.Drawing.Point(326, 472);
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.Size = new System.Drawing.Size(75, 28);
            this.BtnPrint.TabIndex = 593;
            this.BtnPrint.Text = "PRINT";
            this.BtnPrint.UseVisualStyleBackColor = true;
            // 
            // txtNarration
            // 
            this.txtNarration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNarration.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNarration.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNarration.Location = new System.Drawing.Point(16, 445);
            this.txtNarration.Name = "txtNarration";
            this.txtNarration.Size = new System.Drawing.Size(632, 22);
            this.txtNarration.TabIndex = 591;
            this.txtNarration.Enter += new System.EventHandler(this.txtNarration_Enter);
            this.txtNarration.Leave += new System.EventHandler(this.txtNarration_Leave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.DarkRed;
            this.label9.Location = new System.Drawing.Point(16, 422);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 14);
            this.label9.TabIndex = 592;
            this.label9.Text = "Narration :";
            // 
            // BtnDelete
            // 
            this.BtnDelete.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Location = new System.Drawing.Point(494, 472);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(75, 28);
            this.BtnDelete.TabIndex = 589;
            this.BtnDelete.Text = "DELETE";
            this.BtnDelete.UseVisualStyleBackColor = true;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Location = new System.Drawing.Point(411, 472);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(75, 28);
            this.BtnSave.TabIndex = 588;
            this.BtnSave.Text = "SAVE";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // dtgridGLcode
            // 
            this.dtgridGLcode.AllowUserToAddRows = false;
            this.dtgridGLcode.AllowUserToResizeColumns = false;
            this.dtgridGLcode.AllowUserToResizeRows = false;
            this.dtgridGLcode.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgridGLcode.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dtgridGLcode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgridGLcode.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colmGlCode,
            this.colmdescription,
            this.colmAmount});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgridGLcode.DefaultCellStyle = dataGridViewCellStyle4;
            this.dtgridGLcode.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dtgridGLcode.EnableHeadersVisualStyles = false;
            this.dtgridGLcode.Location = new System.Drawing.Point(16, 292);
            this.dtgridGLcode.Margin = new System.Windows.Forms.Padding(2);
            this.dtgridGLcode.Name = "dtgridGLcode";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgridGLcode.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dtgridGLcode.RowHeadersVisible = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dtgridGLcode.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dtgridGLcode.RowTemplate.Height = 24;
            this.dtgridGLcode.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dtgridGLcode.Size = new System.Drawing.Size(632, 123);
            this.dtgridGLcode.TabIndex = 586;
            this.dtgridGLcode.TabStop = false;
            // 
            // colmGlCode
            // 
            this.colmGlCode.HeaderText = "GL Code";
            this.colmGlCode.Name = "colmGlCode";
            this.colmGlCode.ReadOnly = true;
            // 
            // colmdescription
            // 
            this.colmdescription.HeaderText = "Description";
            this.colmdescription.Name = "colmdescription";
            this.colmdescription.Width = 380;
            // 
            // colmAmount
            // 
            this.colmAmount.HeaderText = "Amount";
            this.colmAmount.Name = "colmAmount";
            this.colmAmount.Width = 150;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(44, 87);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 14);
            this.label8.TabIndex = 585;
            this.label8.Text = "Branch :";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(669, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(10, 10);
            this.label4.TabIndex = 583;
            this.label4.Text = "*";
            this.label4.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // txtCheckNo
            // 
            this.txtCheckNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCheckNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCheckNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCheckNo.Location = new System.Drawing.Point(421, 118);
            this.txtCheckNo.Name = "txtCheckNo";
            this.txtCheckNo.Size = new System.Drawing.Size(242, 22);
            this.txtCheckNo.TabIndex = 3;
            this.txtCheckNo.Enter += new System.EventHandler(this.txtCheckNo_Enter);
            this.txtCheckNo.Leave += new System.EventHandler(this.txtCheckNo_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(331, 121);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 14);
            this.label6.TabIndex = 582;
            this.label6.Text = "Cheque No :";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(249, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(10, 10);
            this.label3.TabIndex = 581;
            this.label3.Text = "*";
            this.label3.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(40, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 14);
            this.label1.TabIndex = 580;
            this.label1.Text = "Amount :";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(447, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(10, 10);
            this.label5.TabIndex = 579;
            this.label5.Text = "*";
            this.label5.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // txtBookCodeNM
            // 
            this.txtBookCodeNM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBookCodeNM.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBookCodeNM.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBookCodeNM.Location = new System.Drawing.Point(173, 48);
            this.txtBookCodeNM.Name = "txtBookCodeNM";
            this.txtBookCodeNM.ReadOnly = true;
            this.txtBookCodeNM.Size = new System.Drawing.Size(267, 22);
            this.txtBookCodeNM.TabIndex = 578;
            // 
            // txtBookCode
            // 
            this.txtBookCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBookCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBookCode.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBookCode.Location = new System.Drawing.Point(110, 48);
            this.txtBookCode.Name = "txtBookCode";
            this.txtBookCode.Size = new System.Drawing.Size(57, 22);
            this.txtBookCode.TabIndex = 1;
            this.txtBookCode.Enter += new System.EventHandler(this.txtBookCode_Enter);
            this.txtBookCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBookCode_KeyDown);
            this.txtBookCode.Leave += new System.EventHandler(this.txtBookCode_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 14);
            this.label2.TabIndex = 577;
            this.label2.Text = "Book Code :";
            // 
            // dtpVoucherDate
            // 
            this.dtpVoucherDate.CustomFormat = "dd/MM/yyyy";
            this.dtpVoucherDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpVoucherDate.Location = new System.Drawing.Point(326, 11);
            this.dtpVoucherDate.Name = "dtpVoucherDate";
            this.dtpVoucherDate.Size = new System.Drawing.Size(115, 23);
            this.dtpVoucherDate.TabIndex = 0;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(223, 13);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(105, 14);
            this.label20.TabIndex = 576;
            this.label20.Text = "Voucher Date :";
            // 
            // txtVoucherNo
            // 
            this.txtVoucherNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVoucherNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtVoucherNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVoucherNo.Location = new System.Drawing.Point(111, 11);
            this.txtVoucherNo.Name = "txtVoucherNo";
            this.txtVoucherNo.Size = new System.Drawing.Size(87, 22);
            this.txtVoucherNo.TabIndex = 567;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(15, 13);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(92, 14);
            this.label21.TabIndex = 575;
            this.label21.Text = "Voucher No :";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.tabPayment);
            this.pnlMain.Location = new System.Drawing.Point(19, 12);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(716, 548);
            this.pnlMain.TabIndex = 3;
            // 
            // FrmPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(752, 570);
            this.ControlBox = false;
            this.Controls.Add(this.pnlMain);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmPayment";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Payment";
            this.Load += new System.EventHandler(this.FrmPayment_Load);
            this.Shown += new System.EventHandler(this.FrmPayment_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmPayment_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmPayment_KeyPress);
            this.tabPayment.ResumeLayout(false);
            this.tabList.ResumeLayout(false);
            this.tabList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgridPayment)).EndInit();
            this.tabDetail.ResumeLayout(false);
            this.tabDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgridGLcode)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabPayment;
        private System.Windows.Forms.TabPage tabList;
        private System.Windows.Forms.TextBox txtsearch;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.DataGridView dtgridPayment;
        private System.Windows.Forms.TabPage tabDetail;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.TextBox txtBranch;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtTranstypeNM;
        private System.Windows.Forms.TextBox txtTranstype;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbCreditDebit;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnLessID;
        private System.Windows.Forms.Button btnGreaterID;
        private System.Windows.Forms.Button BtnPrint;
        private System.Windows.Forms.TextBox txtNarration;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button BtnDelete;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.DataGridView dtgridGLcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colmGlCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colmdescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn colmAmount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCheckNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBookCodeNM;
        private System.Windows.Forms.TextBox txtBookCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpVoucherDate;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtVoucherNo;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtBranchCode;
        private System.Windows.Forms.Panel pnlMain;
    }
}
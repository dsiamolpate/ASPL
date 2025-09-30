namespace ASPL.LODGE.MASTERS
{
    partial class FrmGuestInformation
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ChkDeleted = new System.Windows.Forms.CheckBox();
            this.txtsearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DtgridGuestInfo = new System.Windows.Forms.DataGridView();
            this.txtcustcode = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbPrefix = new System.Windows.Forms.ComboBox();
            this.txtCustName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLegID = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbNationality = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtAddress2 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtAddress3 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPlace = new System.Windows.Forms.TextBox();
            this.txtPin = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtMobile1 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtMobile2 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.BtnExit = new System.Windows.Forms.Button();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.picImage = new System.Windows.Forms.PictureBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtPancard = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtPassportNo = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtVisaNo = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.dtpVisa = new System.Windows.Forms.DateTimePicker();
            this.dtpVisaExp = new System.Windows.Forms.DateTimePicker();
            this.dtpPassExp = new System.Windows.Forms.DateTimePicker();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.dtpDateofBirth = new System.Windows.Forms.DateTimePicker();
            this.dtpPass = new System.Windows.Forms.DateTimePicker();
            this.txtEmailId = new System.Windows.Forms.TextBox();
            this.lblSucessMsg = new System.Windows.Forms.Label();
            this.txtState = new System.Windows.Forms.TextBox();
            this.btnPlaceAdd = new System.Windows.Forms.Button();
            this.btnStateAdd = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtImageName = new System.Windows.Forms.TextBox();
            this.txtAddress1 = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.btnGreaterID = new System.Windows.Forms.Button();
            this.btnLessID = new System.Windows.Forms.Button();
            this.lblLegName = new System.Windows.Forms.Label();
            this.btnRemoveDocument = new System.Windows.Forms.Button();
            this.lvViews = new System.Windows.Forms.ListView();
            this.btnAddMore = new System.Windows.Forms.Button();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DtgridGuestInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChkDeleted
            // 
            this.ChkDeleted.AutoSize = true;
            this.ChkDeleted.BackColor = System.Drawing.Color.Transparent;
            this.ChkDeleted.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDeleted.ForeColor = System.Drawing.Color.Black;
            this.ChkDeleted.Location = new System.Drawing.Point(144, 11);
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
            this.txtsearch.Location = new System.Drawing.Point(142, 30);
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
            this.label1.Location = new System.Drawing.Point(19, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 14);
            this.label1.TabIndex = 392;
            this.label1.Text = "Search by Name :";
            // 
            // DtgridGuestInfo
            // 
            this.DtgridGuestInfo.AllowUserToAddRows = false;
            this.DtgridGuestInfo.AllowUserToDeleteRows = false;
            this.DtgridGuestInfo.AllowUserToResizeColumns = false;
            this.DtgridGuestInfo.AllowUserToResizeRows = false;
            this.DtgridGuestInfo.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DtgridGuestInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DtgridGuestInfo.ColumnHeadersHeight = 25;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DtgridGuestInfo.DefaultCellStyle = dataGridViewCellStyle2;
            this.DtgridGuestInfo.EnableHeadersVisualStyles = false;
            this.DtgridGuestInfo.Location = new System.Drawing.Point(15, 54);
            this.DtgridGuestInfo.MultiSelect = false;
            this.DtgridGuestInfo.Name = "DtgridGuestInfo";
            this.DtgridGuestInfo.ReadOnly = true;
            this.DtgridGuestInfo.RowHeadersVisible = false;
            this.DtgridGuestInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DtgridGuestInfo.Size = new System.Drawing.Size(313, 450);
            this.DtgridGuestInfo.TabIndex = 1;
            this.DtgridGuestInfo.TabStop = false;
            this.DtgridGuestInfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DtgridGuestInfo_CellClick);
            this.DtgridGuestInfo.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DtgridGuestInfo_CellDoubleClick);
            this.DtgridGuestInfo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DtgridGuestInfo_KeyDown);
            // 
            // txtcustcode
            // 
            this.txtcustcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtcustcode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtcustcode.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcustcode.Location = new System.Drawing.Point(434, 29);
            this.txtcustcode.Name = "txtcustcode";
            this.txtcustcode.Size = new System.Drawing.Size(68, 22);
            this.txtcustcode.TabIndex = 398;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(333, 33);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(99, 14);
            this.label21.TabIndex = 397;
            this.label21.Text = "Customer ID :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(345, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 14);
            this.label2.TabIndex = 399;
            this.label2.Text = "Cust Name :";
            // 
            // cmbPrefix
            // 
            this.cmbPrefix.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrefix.FormattingEnabled = true;
            this.cmbPrefix.Location = new System.Drawing.Point(434, 55);
            this.cmbPrefix.Name = "cmbPrefix";
            this.cmbPrefix.Size = new System.Drawing.Size(67, 21);
            this.cmbPrefix.TabIndex = 2;
            this.cmbPrefix.Enter += new System.EventHandler(this.cmbPrefix_Enter);
            this.cmbPrefix.Leave += new System.EventHandler(this.cmbPrefix_Leave);
            // 
            // txtCustName
            // 
            this.txtCustName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCustName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCustName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustName.Location = new System.Drawing.Point(502, 54);
            this.txtCustName.MaxLength = 50;
            this.txtCustName.Name = "txtCustName";
            this.txtCustName.Size = new System.Drawing.Size(319, 22);
            this.txtCustName.TabIndex = 3;
            this.txtCustName.Enter += new System.EventHandler(this.txtCustName_Enter);
            this.txtCustName.Leave += new System.EventHandler(this.txtCustName_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(329, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 14);
            this.label3.TabIndex = 402;
            this.label3.Text = "Ledger Name :";
            // 
            // txtLegID
            // 
            this.txtLegID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLegID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLegID.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLegID.Location = new System.Drawing.Point(435, 82);
            this.txtLegID.Name = "txtLegID";
            this.txtLegID.Size = new System.Drawing.Size(66, 22);
            this.txtLegID.TabIndex = 4;
            this.txtLegID.Enter += new System.EventHandler(this.txtLegID_Enter);
            this.txtLegID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLegID_KeyDown);
            this.txtLegID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLegID_KeyPress);
            this.txtLegID.Leave += new System.EventHandler(this.txtLegID_Leave);
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.Red;
            this.label22.Location = new System.Drawing.Point(823, 85);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(10, 12);
            this.label22.TabIndex = 405;
            this.label22.Text = "*";
            this.label22.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(629, 113);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 14);
            this.label6.TabIndex = 409;
            this.label6.Text = "Nationality :";
            // 
            // cmbNationality
            // 
            this.cmbNationality.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbNationality.FormattingEnabled = true;
            this.cmbNationality.Location = new System.Drawing.Point(719, 109);
            this.cmbNationality.Name = "cmbNationality";
            this.cmbNationality.Size = new System.Drawing.Size(102, 21);
            this.cmbNationality.TabIndex = 6;
            this.cmbNationality.Enter += new System.EventHandler(this.cmbNationality_Enter);
            this.cmbNationality.Leave += new System.EventHandler(this.cmbNationality_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(355, 139);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 14);
            this.label7.TabIndex = 411;
            this.label7.Text = "Address1 :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(355, 166);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 14);
            this.label8.TabIndex = 413;
            this.label8.Text = "Address2 :";
            // 
            // txtAddress2
            // 
            this.txtAddress2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAddress2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress2.Location = new System.Drawing.Point(435, 163);
            this.txtAddress2.MaxLength = 100;
            this.txtAddress2.Name = "txtAddress2";
            this.txtAddress2.Size = new System.Drawing.Size(387, 22);
            this.txtAddress2.TabIndex = 8;
            this.txtAddress2.Enter += new System.EventHandler(this.txtAddress2_Enter);
            this.txtAddress2.Leave += new System.EventHandler(this.txtAddress2_Leave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(355, 194);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 14);
            this.label9.TabIndex = 415;
            this.label9.Text = "Address3 :";
            // 
            // txtAddress3
            // 
            this.txtAddress3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAddress3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress3.Location = new System.Drawing.Point(435, 190);
            this.txtAddress3.MaxLength = 100;
            this.txtAddress3.Name = "txtAddress3";
            this.txtAddress3.Size = new System.Drawing.Size(387, 22);
            this.txtAddress3.TabIndex = 9;
            this.txtAddress3.Enter += new System.EventHandler(this.txtAddress3_Enter);
            this.txtAddress3.Leave += new System.EventHandler(this.txtAddress3_Leave);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(546, 220);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 14);
            this.label10.TabIndex = 417;
            this.label10.Text = "Place:";
            // 
            // txtPlace
            // 
            this.txtPlace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPlace.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlace.Location = new System.Drawing.Point(593, 217);
            this.txtPlace.MaxLength = 30;
            this.txtPlace.Name = "txtPlace";
            this.txtPlace.Size = new System.Drawing.Size(85, 22);
            this.txtPlace.TabIndex = 11;
            this.txtPlace.Enter += new System.EventHandler(this.txtCity_Enter);
            this.txtPlace.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPlace_KeyDown);
            this.txtPlace.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCity_KeyPress);
            this.txtPlace.Leave += new System.EventHandler(this.txtCity_Leave);
            // 
            // txtPin
            // 
            this.txtPin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPin.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPin.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPin.Location = new System.Drawing.Point(744, 217);
            this.txtPin.MaxLength = 20;
            this.txtPin.Name = "txtPin";
            this.txtPin.Size = new System.Drawing.Size(78, 22);
            this.txtPin.TabIndex = 12;
            this.txtPin.Enter += new System.EventHandler(this.txtPin_Enter);
            this.txtPin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPin_KeyPress);
            this.txtPin.Leave += new System.EventHandler(this.txtPin_Leave);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(705, 221);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(37, 14);
            this.label11.TabIndex = 419;
            this.label11.Text = "PIN:";
            // 
            // txtMobile1
            // 
            this.txtMobile1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMobile1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMobile1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMobile1.Location = new System.Drawing.Point(435, 243);
            this.txtMobile1.Name = "txtMobile1";
            this.txtMobile1.Size = new System.Drawing.Size(86, 22);
            this.txtMobile1.TabIndex = 13;
            this.txtMobile1.Enter += new System.EventHandler(this.txtPhonNo_Enter);
            this.txtMobile1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPhonNo_KeyPress);
            this.txtMobile1.Leave += new System.EventHandler(this.txtPhonNo_Leave);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(339, 247);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(94, 14);
            this.label12.TabIndex = 421;
            this.label12.Text = "Mobile No.1 :";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(340, 275);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(94, 14);
            this.label13.TabIndex = 423;
            this.label13.Text = "Mobile No.2 :";
            // 
            // txtMobile2
            // 
            this.txtMobile2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMobile2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMobile2.Location = new System.Drawing.Point(435, 271);
            this.txtMobile2.MaxLength = 10;
            this.txtMobile2.Name = "txtMobile2";
            this.txtMobile2.Size = new System.Drawing.Size(85, 22);
            this.txtMobile2.TabIndex = 14;
            this.txtMobile2.Enter += new System.EventHandler(this.txtMobileNo_Enter);
            this.txtMobile2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMobileNo_KeyPress);
            this.txtMobile2.Leave += new System.EventHandler(this.txtMobileNo_Leave);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(544, 247);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(48, 14);
            this.label14.TabIndex = 425;
            this.label14.Text = "Email:";
            // 
            // BtnExit
            // 
            this.BtnExit.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Location = new System.Drawing.Point(741, 505);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(75, 25);
            this.BtnExit.TabIndex = 430;
            this.BtnExit.Text = "EXIT";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Location = new System.Drawing.Point(654, 505);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(75, 25);
            this.BtnDelete.TabIndex = 429;
            this.BtnDelete.Text = "DELETE";
            this.BtnDelete.UseVisualStyleBackColor = true;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Location = new System.Drawing.Point(569, 505);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(75, 25);
            this.BtnSave.TabIndex = 428;
            this.BtnSave.Text = "SAVE";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnAdd
            // 
            this.BtnAdd.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BtnAdd.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAdd.Location = new System.Drawing.Point(479, 505);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(75, 25);
            this.BtnAdd.TabIndex = 427;
            this.BtnAdd.Text = "&ADD";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // picImage
            // 
            this.picImage.BackColor = System.Drawing.Color.White;
            this.picImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picImage.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.picImage.ErrorImage = null;
            this.picImage.InitialImage = null;
            this.picImage.Location = new System.Drawing.Point(725, 355);
            this.picImage.Name = "picImage";
            this.picImage.Size = new System.Drawing.Size(120, 113);
            this.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picImage.TabIndex = 433;
            this.picImage.TabStop = false;
            this.picImage.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(521, 275);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(72, 14);
            this.label16.TabIndex = 439;
            this.label16.Text = "Pan Card:";
            // 
            // txtPancard
            // 
            this.txtPancard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPancard.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPancard.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPancard.Location = new System.Drawing.Point(593, 271);
            this.txtPancard.Name = "txtPancard";
            this.txtPancard.Size = new System.Drawing.Size(231, 22);
            this.txtPancard.TabIndex = 16;
            this.txtPancard.Enter += new System.EventHandler(this.txtPancard_Enter);
            this.txtPancard.Leave += new System.EventHandler(this.txtPancard_Leave);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(337, 302);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(96, 14);
            this.label17.TabIndex = 441;
            this.label17.Text = "Passport No :";
            // 
            // txtPassportNo
            // 
            this.txtPassportNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassportNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPassportNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassportNo.Location = new System.Drawing.Point(435, 298);
            this.txtPassportNo.Name = "txtPassportNo";
            this.txtPassportNo.Size = new System.Drawing.Size(86, 22);
            this.txtPassportNo.TabIndex = 17;
            this.txtPassportNo.Enter += new System.EventHandler(this.txtPassportNo_Enter);
            this.txtPassportNo.Leave += new System.EventHandler(this.txtPassportNo_Leave);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(367, 329);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(66, 14);
            this.label18.TabIndex = 443;
            this.label18.Text = "Visa No :";
            // 
            // txtVisaNo
            // 
            this.txtVisaNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVisaNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtVisaNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVisaNo.Location = new System.Drawing.Point(435, 326);
            this.txtVisaNo.Name = "txtVisaNo";
            this.txtVisaNo.Size = new System.Drawing.Size(86, 22);
            this.txtVisaNo.TabIndex = 20;
            this.txtVisaNo.Enter += new System.EventHandler(this.txtVisaNo_Enter);
            this.txtVisaNo.Leave += new System.EventHandler(this.txtVisaNo_Leave);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(521, 302);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(83, 14);
            this.label20.TabIndex = 444;
            this.label20.Text = "Issue Date:";
            // 
            // dtpVisa
            // 
            this.dtpVisa.CustomFormat = "dd/MM/yyyy";
            this.dtpVisa.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpVisa.Location = new System.Drawing.Point(603, 329);
            this.dtpVisa.Name = "dtpVisa";
            this.dtpVisa.Size = new System.Drawing.Size(78, 20);
            this.dtpVisa.TabIndex = 21;
            this.dtpVisa.Enter += new System.EventHandler(this.dtpVisa_Enter);
            // 
            // dtpVisaExp
            // 
            this.dtpVisaExp.CustomFormat = "dd/MM/yyyy";
            this.dtpVisaExp.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpVisaExp.Location = new System.Drawing.Point(769, 326);
            this.dtpVisaExp.Name = "dtpVisaExp";
            this.dtpVisaExp.Size = new System.Drawing.Size(78, 20);
            this.dtpVisaExp.TabIndex = 22;
            this.dtpVisaExp.Enter += new System.EventHandler(this.dtpVisaExp_Enter);
            this.dtpVisaExp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpVisaExp_KeyPress);
            // 
            // dtpPassExp
            // 
            this.dtpPassExp.CustomFormat = "dd/MM/yyyy";
            this.dtpPassExp.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPassExp.Location = new System.Drawing.Point(768, 299);
            this.dtpPassExp.Name = "dtpPassExp";
            this.dtpPassExp.Size = new System.Drawing.Size(78, 20);
            this.dtpPassExp.TabIndex = 19;
            this.dtpPassExp.Enter += new System.EventHandler(this.dtpPassExp_Enter);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.BackColor = System.Drawing.Color.Transparent;
            this.label24.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(680, 303);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(89, 14);
            this.label24.TabIndex = 448;
            this.label24.Text = "Expiry Date:";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.Color.Transparent;
            this.label23.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(681, 329);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(89, 14);
            this.label23.TabIndex = 452;
            this.label23.Text = "Expiry Date:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(521, 331);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(83, 14);
            this.label19.TabIndex = 453;
            this.label19.Text = "Issue Date:";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.BackColor = System.Drawing.Color.Transparent;
            this.label25.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(388, 113);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(46, 14);
            this.label25.TabIndex = 455;
            this.label25.Text = "DOB :";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(823, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(10, 10);
            this.label5.TabIndex = 456;
            this.label5.Text = "*";
            this.label5.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.Transparent;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.Red;
            this.label26.Location = new System.Drawing.Point(522, 243);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(10, 10);
            this.label26.TabIndex = 457;
            this.label26.Text = "*";
            this.label26.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // dtpDateofBirth
            // 
            this.dtpDateofBirth.CustomFormat = "dd/MM/yyyy";
            this.dtpDateofBirth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateofBirth.Location = new System.Drawing.Point(435, 110);
            this.dtpDateofBirth.Name = "dtpDateofBirth";
            this.dtpDateofBirth.Size = new System.Drawing.Size(98, 20);
            this.dtpDateofBirth.TabIndex = 5;
            this.dtpDateofBirth.Enter += new System.EventHandler(this.dtpDateofBirth_Enter);
            this.dtpDateofBirth.Leave += new System.EventHandler(this.dtpDateofBirth_Leave);
            // 
            // dtpPass
            // 
            this.dtpPass.CustomFormat = "dd/MM/yyyy";
            this.dtpPass.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPass.Location = new System.Drawing.Point(602, 300);
            this.dtpPass.Name = "dtpPass";
            this.dtpPass.Size = new System.Drawing.Size(78, 20);
            this.dtpPass.TabIndex = 18;
            this.dtpPass.Enter += new System.EventHandler(this.dtpPass_Enter_1);
            // 
            // txtEmailId
            // 
            this.txtEmailId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmailId.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmailId.Location = new System.Drawing.Point(592, 243);
            this.txtEmailId.MaxLength = 50;
            this.txtEmailId.Name = "txtEmailId";
            this.txtEmailId.Size = new System.Drawing.Size(232, 22);
            this.txtEmailId.TabIndex = 15;
            this.txtEmailId.Text = " ";
            this.txtEmailId.Enter += new System.EventHandler(this.txtEmailId_Enter_1);
            this.txtEmailId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmailId_KeyPress_1);
            this.txtEmailId.Leave += new System.EventHandler(this.txtEmailId_Leave);
            this.txtEmailId.Validating += new System.ComponentModel.CancelEventHandler(this.txtEmailId_Validating);
            // 
            // lblSucessMsg
            // 
            this.lblSucessMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblSucessMsg.Font = new System.Drawing.Font("Verdana", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSucessMsg.ForeColor = System.Drawing.Color.Black;
            this.lblSucessMsg.Location = new System.Drawing.Point(21, 507);
            this.lblSucessMsg.Name = "lblSucessMsg";
            this.lblSucessMsg.Size = new System.Drawing.Size(260, 24);
            this.lblSucessMsg.TabIndex = 503;
            // 
            // txtState
            // 
            this.txtState.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtState.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtState.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtState.Location = new System.Drawing.Point(435, 216);
            this.txtState.MaxLength = 15;
            this.txtState.Name = "txtState";
            this.txtState.Size = new System.Drawing.Size(86, 22);
            this.txtState.TabIndex = 10;
            this.txtState.Enter += new System.EventHandler(this.txtState_Enter);
            this.txtState.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtState_KeyDown);
            this.txtState.Leave += new System.EventHandler(this.txtState_Leave);
            // 
            // btnPlaceAdd
            // 
            this.btnPlaceAdd.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnPlaceAdd.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.btnPlaceAdd.Location = new System.Drawing.Point(679, 216);
            this.btnPlaceAdd.Name = "btnPlaceAdd";
            this.btnPlaceAdd.Size = new System.Drawing.Size(24, 22);
            this.btnPlaceAdd.TabIndex = 551;
            this.btnPlaceAdd.Text = "+";
            this.btnPlaceAdd.UseVisualStyleBackColor = true;
            this.btnPlaceAdd.Click += new System.EventHandler(this.btnPlaceAdd_Click);
            // 
            // btnStateAdd
            // 
            this.btnStateAdd.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnStateAdd.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.btnStateAdd.Location = new System.Drawing.Point(522, 216);
            this.btnStateAdd.Name = "btnStateAdd";
            this.btnStateAdd.Size = new System.Drawing.Size(24, 22);
            this.btnStateAdd.TabIndex = 550;
            this.btnStateAdd.Text = "+";
            this.btnStateAdd.UseVisualStyleBackColor = true;
            this.btnStateAdd.Click += new System.EventHandler(this.btnStateAdd_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(382, 219);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 14);
            this.label4.TabIndex = 549;
            this.label4.Text = "State :";
            // 
            // txtImageName
            // 
            this.txtImageName.Location = new System.Drawing.Point(502, 478);
            this.txtImageName.Name = "txtImageName";
            this.txtImageName.Size = new System.Drawing.Size(103, 20);
            this.txtImageName.TabIndex = 553;
            this.txtImageName.Visible = false;
            // 
            // txtAddress1
            // 
            this.txtAddress1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAddress1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress1.Location = new System.Drawing.Point(435, 137);
            this.txtAddress1.MaxLength = 100;
            this.txtAddress1.Name = "txtAddress1";
            this.txtAddress1.Size = new System.Drawing.Size(387, 22);
            this.txtAddress1.TabIndex = 7;
            this.txtAddress1.Enter += new System.EventHandler(this.txtAddress1_Enter);
            this.txtAddress1.Leave += new System.EventHandler(this.txtAddress1_Leave);
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.Transparent;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.Red;
            this.label27.Location = new System.Drawing.Point(823, 54);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(10, 10);
            this.label27.TabIndex = 555;
            this.label27.Text = "*";
            this.label27.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.Transparent;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.ForeColor = System.Drawing.Color.Red;
            this.label28.Location = new System.Drawing.Point(824, 137);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(10, 10);
            this.label28.TabIndex = 556;
            this.label28.Text = "*";
            this.label28.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.btnGreaterID);
            this.pnlMain.Controls.Add(this.btnLessID);
            this.pnlMain.Controls.Add(this.lblLegName);
            this.pnlMain.Controls.Add(this.btnRemoveDocument);
            this.pnlMain.Controls.Add(this.lvViews);
            this.pnlMain.Controls.Add(this.btnAddMore);
            this.pnlMain.Controls.Add(this.label28);
            this.pnlMain.Controls.Add(this.label27);
            this.pnlMain.Controls.Add(this.txtAddress1);
            this.pnlMain.Controls.Add(this.txtState);
            this.pnlMain.Controls.Add(this.btnPlaceAdd);
            this.pnlMain.Controls.Add(this.btnStateAdd);
            this.pnlMain.Controls.Add(this.label4);
            this.pnlMain.Controls.Add(this.lblSucessMsg);
            this.pnlMain.Controls.Add(this.txtEmailId);
            this.pnlMain.Controls.Add(this.dtpPass);
            this.pnlMain.Controls.Add(this.dtpDateofBirth);
            this.pnlMain.Controls.Add(this.label26);
            this.pnlMain.Controls.Add(this.label5);
            this.pnlMain.Controls.Add(this.label25);
            this.pnlMain.Controls.Add(this.label19);
            this.pnlMain.Controls.Add(this.label23);
            this.pnlMain.Controls.Add(this.dtpVisaExp);
            this.pnlMain.Controls.Add(this.dtpPassExp);
            this.pnlMain.Controls.Add(this.label24);
            this.pnlMain.Controls.Add(this.dtpVisa);
            this.pnlMain.Controls.Add(this.label20);
            this.pnlMain.Controls.Add(this.label18);
            this.pnlMain.Controls.Add(this.txtVisaNo);
            this.pnlMain.Controls.Add(this.label17);
            this.pnlMain.Controls.Add(this.txtPassportNo);
            this.pnlMain.Controls.Add(this.label16);
            this.pnlMain.Controls.Add(this.txtPancard);
            this.pnlMain.Controls.Add(this.picImage);
            this.pnlMain.Controls.Add(this.BtnExit);
            this.pnlMain.Controls.Add(this.BtnDelete);
            this.pnlMain.Controls.Add(this.BtnSave);
            this.pnlMain.Controls.Add(this.BtnAdd);
            this.pnlMain.Controls.Add(this.label14);
            this.pnlMain.Controls.Add(this.txtMobile2);
            this.pnlMain.Controls.Add(this.label13);
            this.pnlMain.Controls.Add(this.txtMobile1);
            this.pnlMain.Controls.Add(this.label12);
            this.pnlMain.Controls.Add(this.txtPin);
            this.pnlMain.Controls.Add(this.label11);
            this.pnlMain.Controls.Add(this.txtPlace);
            this.pnlMain.Controls.Add(this.label10);
            this.pnlMain.Controls.Add(this.txtAddress3);
            this.pnlMain.Controls.Add(this.label9);
            this.pnlMain.Controls.Add(this.txtAddress2);
            this.pnlMain.Controls.Add(this.label8);
            this.pnlMain.Controls.Add(this.label7);
            this.pnlMain.Controls.Add(this.cmbNationality);
            this.pnlMain.Controls.Add(this.label6);
            this.pnlMain.Controls.Add(this.label22);
            this.pnlMain.Controls.Add(this.txtLegID);
            this.pnlMain.Controls.Add(this.label3);
            this.pnlMain.Controls.Add(this.txtCustName);
            this.pnlMain.Controls.Add(this.cmbPrefix);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.txtcustcode);
            this.pnlMain.Controls.Add(this.label21);
            this.pnlMain.Controls.Add(this.DtgridGuestInfo);
            this.pnlMain.Controls.Add(this.txtsearch);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.ChkDeleted);
            this.pnlMain.Controls.Add(this.txtImageName);
            this.pnlMain.Location = new System.Drawing.Point(6, 11);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(870, 540);
            this.pnlMain.TabIndex = 557;
            // 
            // btnGreaterID
            // 
            this.btnGreaterID.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnGreaterID.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGreaterID.Location = new System.Drawing.Point(413, 505);
            this.btnGreaterID.Name = "btnGreaterID";
            this.btnGreaterID.Size = new System.Drawing.Size(50, 25);
            this.btnGreaterID.TabIndex = 660;
            this.btnGreaterID.Text = ">>";
            this.btnGreaterID.UseVisualStyleBackColor = true;
            this.btnGreaterID.Click += new System.EventHandler(this.btnGreaterID_Click);
            // 
            // btnLessID
            // 
            this.btnLessID.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnLessID.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLessID.Location = new System.Drawing.Point(348, 505);
            this.btnLessID.Name = "btnLessID";
            this.btnLessID.Size = new System.Drawing.Size(50, 25);
            this.btnLessID.TabIndex = 659;
            this.btnLessID.Text = "<<";
            this.btnLessID.UseVisualStyleBackColor = true;
            this.btnLessID.Click += new System.EventHandler(this.btnLessID_Click);
            // 
            // lblLegName
            // 
            this.lblLegName.BackColor = System.Drawing.Color.White;
            this.lblLegName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLegName.Location = new System.Drawing.Point(502, 82);
            this.lblLegName.Name = "lblLegName";
            this.lblLegName.Size = new System.Drawing.Size(319, 22);
            this.lblLegName.TabIndex = 658;
            this.lblLegName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnRemoveDocument
            // 
            this.btnRemoveDocument.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnRemoveDocument.Location = new System.Drawing.Point(332, 473);
            this.btnRemoveDocument.Name = "btnRemoveDocument";
            this.btnRemoveDocument.Size = new System.Drawing.Size(122, 20);
            this.btnRemoveDocument.TabIndex = 559;
            this.btnRemoveDocument.Text = "Remove Document";
            this.btnRemoveDocument.UseVisualStyleBackColor = true;
            this.btnRemoveDocument.Click += new System.EventHandler(this.btnRemoveDocument_Click);
            // 
            // lvViews
            // 
            this.lvViews.HideSelection = false;
            this.lvViews.Location = new System.Drawing.Point(333, 354);
            this.lvViews.MultiSelect = false;
            this.lvViews.Name = "lvViews";
            this.lvViews.Size = new System.Drawing.Size(386, 118);
            this.lvViews.TabIndex = 558;
            this.lvViews.UseCompatibleStateImageBehavior = false;
            this.lvViews.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvViews_ItemSelectionChanged);
            // 
            // btnAddMore
            // 
            this.btnAddMore.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnAddMore.Location = new System.Drawing.Point(725, 473);
            this.btnAddMore.Name = "btnAddMore";
            this.btnAddMore.Size = new System.Drawing.Size(122, 20);
            this.btnAddMore.TabIndex = 557;
            this.btnAddMore.Text = "Load Document";
            this.btnAddMore.UseVisualStyleBackColor = true;
            this.btnAddMore.Click += new System.EventHandler(this.btnAddMore_Click);
            // 
            // imgList
            // 
            this.imgList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imgList.ImageSize = new System.Drawing.Size(75, 75);
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // FrmGuestInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(890, 556);
            this.ControlBox = false;
            this.Controls.Add(this.pnlMain);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmGuestInformation";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Guest Information";
            this.Load += new System.EventHandler(this.FrmGuestInformation_Load);
            this.Shown += new System.EventHandler(this.FrmGuestInformation_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmGuestInformation_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmGuestInformation_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.DtgridGuestInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox ChkDeleted;
        private System.Windows.Forms.TextBox txtsearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView DtgridGuestInfo;
        private System.Windows.Forms.TextBox txtcustcode;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbPrefix;
        private System.Windows.Forms.TextBox txtCustName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLegID;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbNationality;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtAddress2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtAddress3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtPlace;
        private System.Windows.Forms.TextBox txtPin;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtMobile1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtMobile2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.Button BtnDelete;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.PictureBox picImage;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtPancard;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtPassportNo;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtVisaNo;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.DateTimePicker dtpVisa;
        private System.Windows.Forms.DateTimePicker dtpVisaExp;
        private System.Windows.Forms.DateTimePicker dtpPassExp;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.DateTimePicker dtpDateofBirth;
        private System.Windows.Forms.DateTimePicker dtpPass;
        private System.Windows.Forms.TextBox txtEmailId;
        private System.Windows.Forms.Label lblSucessMsg;
        private System.Windows.Forms.TextBox txtState;
        private System.Windows.Forms.Button btnPlaceAdd;
        private System.Windows.Forms.Button btnStateAdd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtImageName;
        private System.Windows.Forms.TextBox txtAddress1;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Button btnAddMore;
        private System.Windows.Forms.ListView lvViews;
        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.Button btnRemoveDocument;
        private System.Windows.Forms.Label lblLegName;
        private System.Windows.Forms.Button btnGreaterID;
        private System.Windows.Forms.Button btnLessID;
    }
}
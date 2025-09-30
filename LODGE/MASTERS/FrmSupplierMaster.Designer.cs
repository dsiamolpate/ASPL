namespace ASPL.LODGE.MASTERS
{
    partial class FrmSupplierMaster
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtsearch = new System.Windows.Forms.TextBox();
            this.DtgridSupMaster = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.ChkDeleted = new System.Windows.Forms.CheckBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtcode = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.txtAddress2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtAddress1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMobileNo2 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtMobileNo1 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtclosebal = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtopenbal = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtCrDr = new System.Windows.Forms.TextBox();
            this.txtCrDr1 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtTallyCode = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtEmailId = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.BtnExit = new System.Windows.Forms.Button();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.cmbActive = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbmarketSeg = new System.Windows.Forms.ComboBox();
            this.txtGST = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.lblSucessMsg = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.btnLessID = new System.Windows.Forms.Button();
            this.btnGreaterID = new System.Windows.Forms.Button();
            this.lblTallyName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DtgridSupMaster)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtsearch
            // 
            this.txtsearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtsearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtsearch.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsearch.Location = new System.Drawing.Point(142, 31);
            this.txtsearch.Name = "txtsearch";
            this.txtsearch.Size = new System.Drawing.Size(185, 22);
            this.txtsearch.TabIndex = 0;
            this.txtsearch.TextChanged += new System.EventHandler(this.txtsearch_TextChanged);
            this.txtsearch.Enter += new System.EventHandler(this.txtsearch_Enter);
            this.txtsearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtsearch_KeyDown);
            this.txtsearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtsearch_KeyPress);
            this.txtsearch.Leave += new System.EventHandler(this.txtsearch_Leave);
            // 
            // DtgridSupMaster
            // 
            this.DtgridSupMaster.AllowUserToAddRows = false;
            this.DtgridSupMaster.AllowUserToDeleteRows = false;
            this.DtgridSupMaster.AllowUserToResizeColumns = false;
            this.DtgridSupMaster.AllowUserToResizeRows = false;
            this.DtgridSupMaster.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DtgridSupMaster.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.DtgridSupMaster.ColumnHeadersHeight = 25;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DtgridSupMaster.DefaultCellStyle = dataGridViewCellStyle8;
            this.DtgridSupMaster.EnableHeadersVisualStyles = false;
            this.DtgridSupMaster.Location = new System.Drawing.Point(15, 55);
            this.DtgridSupMaster.MultiSelect = false;
            this.DtgridSupMaster.Name = "DtgridSupMaster";
            this.DtgridSupMaster.ReadOnly = true;
            this.DtgridSupMaster.RowHeadersVisible = false;
            this.DtgridSupMaster.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DtgridSupMaster.Size = new System.Drawing.Size(313, 450);
            this.DtgridSupMaster.TabIndex = 145;
            this.DtgridSupMaster.TabStop = false;
            this.DtgridSupMaster.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DtgridSupMaster_CellClick);
            this.DtgridSupMaster.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DtgridSupMaster_CellDoubleClick);
            this.DtgridSupMaster.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DtgridSupMaster_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(13, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 14);
            this.label1.TabIndex = 401;
            this.label1.Text = "Search by Name :";
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
            this.ChkDeleted.TabIndex = 400;
            this.ChkDeleted.Text = "Show Deleted";
            this.ChkDeleted.UseVisualStyleBackColor = false;
            this.ChkDeleted.CheckedChanged += new System.EventHandler(this.ChkDeleted_CheckedChanged);
            // 
            // txtName
            // 
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(458, 95);
            this.txtName.MaxLength = 50;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(241, 22);
            this.txtName.TabIndex = 1;
            this.txtName.Enter += new System.EventHandler(this.txtName_Enter);
            this.txtName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtName_KeyPress);
            this.txtName.Leave += new System.EventHandler(this.txtName_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(345, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 14);
            this.label2.TabIndex = 406;
            this.label2.Text = "Supplier Name :";
            // 
            // txtcode
            // 
            this.txtcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtcode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtcode.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcode.Location = new System.Drawing.Point(458, 62);
            this.txtcode.Name = "txtcode";
            this.txtcode.Size = new System.Drawing.Size(83, 22);
            this.txtcode.TabIndex = 405;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(409, 65);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(49, 14);
            this.label21.TabIndex = 404;
            this.label21.Text = "Code :";
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.Red;
            this.label22.Location = new System.Drawing.Point(701, 95);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(10, 10);
            this.label22.TabIndex = 407;
            this.label22.Text = "*";
            this.label22.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // txtAddress2
            // 
            this.txtAddress2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAddress2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress2.Location = new System.Drawing.Point(458, 156);
            this.txtAddress2.MaxLength = 150;
            this.txtAddress2.Name = "txtAddress2";
            this.txtAddress2.Size = new System.Drawing.Size(241, 22);
            this.txtAddress2.TabIndex = 3;
            this.txtAddress2.Enter += new System.EventHandler(this.txtAddress2_Enter);
            this.txtAddress2.Leave += new System.EventHandler(this.txtAddress2_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(379, 160);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 14);
            this.label8.TabIndex = 436;
            this.label8.Text = "Address2 :";
            // 
            // txtAddress1
            // 
            this.txtAddress1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAddress1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress1.Location = new System.Drawing.Point(458, 125);
            this.txtAddress1.MaxLength = 150;
            this.txtAddress1.Name = "txtAddress1";
            this.txtAddress1.Size = new System.Drawing.Size(241, 22);
            this.txtAddress1.TabIndex = 2;
            this.txtAddress1.Enter += new System.EventHandler(this.txtAddress1_Enter);
            this.txtAddress1.Leave += new System.EventHandler(this.txtAddress1_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(379, 128);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 14);
            this.label7.TabIndex = 435;
            this.label7.Text = "Address1 :";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(700, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(10, 13);
            this.label3.TabIndex = 434;
            this.label3.Text = "*";
            this.label3.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // txtMobileNo2
            // 
            this.txtMobileNo2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMobileNo2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMobileNo2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMobileNo2.Location = new System.Drawing.Point(459, 217);
            this.txtMobileNo2.MaxLength = 10;
            this.txtMobileNo2.Name = "txtMobileNo2";
            this.txtMobileNo2.Size = new System.Drawing.Size(136, 22);
            this.txtMobileNo2.TabIndex = 5;
            this.txtMobileNo2.Enter += new System.EventHandler(this.txtMobileNo_Enter);
            this.txtMobileNo2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMobileNo_KeyPress);
            this.txtMobileNo2.Leave += new System.EventHandler(this.txtMobileNo_Leave);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(363, 220);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(94, 14);
            this.label13.TabIndex = 441;
            this.label13.Text = "Mobile No.2 :";
            // 
            // txtMobileNo1
            // 
            this.txtMobileNo1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMobileNo1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMobileNo1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMobileNo1.Location = new System.Drawing.Point(459, 187);
            this.txtMobileNo1.Name = "txtMobileNo1";
            this.txtMobileNo1.Size = new System.Drawing.Size(136, 22);
            this.txtMobileNo1.TabIndex = 4;
            this.txtMobileNo1.Enter += new System.EventHandler(this.txtPhonNo_Enter);
            this.txtMobileNo1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPhonNo_KeyPress);
            this.txtMobileNo1.Leave += new System.EventHandler(this.txtPhonNo_Leave);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(364, 191);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(94, 14);
            this.label12.TabIndex = 440;
            this.label12.Text = "Mobile No.1 :";
            // 
            // txtclosebal
            // 
            this.txtclosebal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtclosebal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtclosebal.Enabled = false;
            this.txtclosebal.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtclosebal.Location = new System.Drawing.Point(458, 309);
            this.txtclosebal.Name = "txtclosebal";
            this.txtclosebal.Size = new System.Drawing.Size(137, 22);
            this.txtclosebal.TabIndex = 125;
            this.txtclosebal.Enter += new System.EventHandler(this.txtclosebal_Enter);
            this.txtclosebal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtclosebal_KeyPress);
            this.txtclosebal.Leave += new System.EventHandler(this.txtclosebal_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(336, 312);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 14);
            this.label6.TabIndex = 444;
            this.label6.Text = "Closing Balance :";
            // 
            // txtopenbal
            // 
            this.txtopenbal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtopenbal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtopenbal.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtopenbal.Location = new System.Drawing.Point(458, 277);
            this.txtopenbal.Name = "txtopenbal";
            this.txtopenbal.Size = new System.Drawing.Size(137, 22);
            this.txtopenbal.TabIndex = 7;
            this.txtopenbal.Enter += new System.EventHandler(this.txtopenbal_Enter);
            this.txtopenbal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtopenbal_KeyPress);
            this.txtopenbal.Leave += new System.EventHandler(this.txtopenbal_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(330, 280);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 14);
            this.label4.TabIndex = 442;
            this.label4.Text = "Opening Balance :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(598, 282);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 14);
            this.label5.TabIndex = 446;
            this.label5.Text = " CR/DR:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(598, 313);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 14);
            this.label9.TabIndex = 447;
            this.label9.Text = " CR/DR:";
            // 
            // txtCrDr
            // 
            this.txtCrDr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCrDr.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCrDr.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCrDr.Location = new System.Drawing.Point(660, 279);
            this.txtCrDr.Name = "txtCrDr";
            this.txtCrDr.Size = new System.Drawing.Size(34, 22);
            this.txtCrDr.TabIndex = 8;
            this.txtCrDr.Enter += new System.EventHandler(this.txtCrDr_Enter);
            this.txtCrDr.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCrDr_KeyDown);
            this.txtCrDr.Leave += new System.EventHandler(this.txtCrDr_Leave);
            // 
            // txtCrDr1
            // 
            this.txtCrDr1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCrDr1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCrDr1.Enabled = false;
            this.txtCrDr1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCrDr1.Location = new System.Drawing.Point(661, 310);
            this.txtCrDr1.Name = "txtCrDr1";
            this.txtCrDr1.Size = new System.Drawing.Size(34, 22);
            this.txtCrDr1.TabIndex = 165;
            this.txtCrDr1.Enter += new System.EventHandler(this.txtCrDr1_Enter);
            this.txtCrDr1.Leave += new System.EventHandler(this.txtCrDr1_Leave);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(372, 343);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(85, 14);
            this.label11.TabIndex = 452;
            this.label11.Text = "Tally Code :";
            // 
            // txtTallyCode
            // 
            this.txtTallyCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTallyCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTallyCode.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTallyCode.Location = new System.Drawing.Point(458, 340);
            this.txtTallyCode.Name = "txtTallyCode";
            this.txtTallyCode.Size = new System.Drawing.Size(60, 22);
            this.txtTallyCode.TabIndex = 9;
            this.txtTallyCode.Enter += new System.EventHandler(this.txtLbtno_Enter);
            this.txtTallyCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTallyCode_KeyDown);
            this.txtTallyCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTallyCode_KeyPress);
            this.txtTallyCode.Leave += new System.EventHandler(this.txtTallyCode_Leave);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(400, 375);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(57, 14);
            this.label14.TabIndex = 454;
            this.label14.Text = "Active :";
            // 
            // txtEmailId
            // 
            this.txtEmailId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmailId.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmailId.Location = new System.Drawing.Point(458, 247);
            this.txtEmailId.MaxLength = 50;
            this.txtEmailId.Name = "txtEmailId";
            this.txtEmailId.Size = new System.Drawing.Size(238, 22);
            this.txtEmailId.TabIndex = 6;
            this.txtEmailId.Enter += new System.EventHandler(this.txtEmailId_Enter);
            this.txtEmailId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmailId_KeyPress);
            this.txtEmailId.Leave += new System.EventHandler(this.txtEmailId_Leave);
            this.txtEmailId.Validating += new System.ComponentModel.CancelEventHandler(this.txtEmailId_Validating);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(385, 251);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(72, 14);
            this.label16.TabIndex = 457;
            this.label16.Text = "Email ID :";
            // 
            // BtnExit
            // 
            this.BtnExit.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Location = new System.Drawing.Point(666, 480);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(75, 25);
            this.BtnExit.TabIndex = 16;
            this.BtnExit.Text = "EXIT";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Location = new System.Drawing.Point(584, 480);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(75, 25);
            this.BtnDelete.TabIndex = 15;
            this.BtnDelete.Text = "DELETE";
            this.BtnDelete.UseVisualStyleBackColor = true;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Location = new System.Drawing.Point(502, 480);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(75, 25);
            this.BtnSave.TabIndex = 145;
            this.BtnSave.Text = "SAVE";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnAdd
            // 
            this.BtnAdd.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BtnAdd.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAdd.Location = new System.Drawing.Point(420, 480);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(75, 25);
            this.BtnAdd.TabIndex = 17;
            this.BtnAdd.Text = "&ADD";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // cmbActive
            // 
            this.cmbActive.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbActive.FormattingEnabled = true;
            this.cmbActive.Location = new System.Drawing.Point(458, 371);
            this.cmbActive.Name = "cmbActive";
            this.cmbActive.Size = new System.Drawing.Size(137, 21);
            this.cmbActive.TabIndex = 10;
            this.cmbActive.Enter += new System.EventHandler(this.cmbActive_Enter);
            this.cmbActive.Leave += new System.EventHandler(this.cmbActive_Leave);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(333, 404);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(124, 14);
            this.label10.TabIndex = 459;
            this.label10.Text = "Market Segment :";
            // 
            // cmbmarketSeg
            // 
            this.cmbmarketSeg.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbmarketSeg.FormattingEnabled = true;
            this.cmbmarketSeg.Location = new System.Drawing.Point(459, 401);
            this.cmbmarketSeg.Name = "cmbmarketSeg";
            this.cmbmarketSeg.Size = new System.Drawing.Size(136, 21);
            this.cmbmarketSeg.TabIndex = 11;
            this.cmbmarketSeg.Enter += new System.EventHandler(this.cmbmarketSeg_Enter);
            this.cmbmarketSeg.Leave += new System.EventHandler(this.cmbmarketSeg_Leave);
            // 
            // txtGST
            // 
            this.txtGST.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGST.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGST.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGST.Location = new System.Drawing.Point(459, 432);
            this.txtGST.Name = "txtGST";
            this.txtGST.Size = new System.Drawing.Size(236, 22);
            this.txtGST.TabIndex = 12;
            this.txtGST.Enter += new System.EventHandler(this.txtGST_Enter);
            this.txtGST.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGST_KeyPress);
            this.txtGST.Leave += new System.EventHandler(this.txtGST_Leave);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(414, 434);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(43, 14);
            this.label17.TabIndex = 462;
            this.label17.Text = "GST :";
            // 
            // lblSucessMsg
            // 
            this.lblSucessMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblSucessMsg.Font = new System.Drawing.Font("Verdana", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSucessMsg.ForeColor = System.Drawing.Color.Black;
            this.lblSucessMsg.Location = new System.Drawing.Point(18, 508);
            this.lblSucessMsg.Name = "lblSucessMsg";
            this.lblSucessMsg.Size = new System.Drawing.Size(260, 24);
            this.lblSucessMsg.TabIndex = 547;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.lblTallyName);
            this.pnlMain.Controls.Add(this.btnLessID);
            this.pnlMain.Controls.Add(this.lblSucessMsg);
            this.pnlMain.Controls.Add(this.btnGreaterID);
            this.pnlMain.Controls.Add(this.txtGST);
            this.pnlMain.Controls.Add(this.label17);
            this.pnlMain.Controls.Add(this.cmbmarketSeg);
            this.pnlMain.Controls.Add(this.label10);
            this.pnlMain.Controls.Add(this.cmbActive);
            this.pnlMain.Controls.Add(this.BtnExit);
            this.pnlMain.Controls.Add(this.BtnDelete);
            this.pnlMain.Controls.Add(this.BtnSave);
            this.pnlMain.Controls.Add(this.BtnAdd);
            this.pnlMain.Controls.Add(this.txtEmailId);
            this.pnlMain.Controls.Add(this.label16);
            this.pnlMain.Controls.Add(this.label14);
            this.pnlMain.Controls.Add(this.txtTallyCode);
            this.pnlMain.Controls.Add(this.label11);
            this.pnlMain.Controls.Add(this.txtCrDr1);
            this.pnlMain.Controls.Add(this.txtCrDr);
            this.pnlMain.Controls.Add(this.label9);
            this.pnlMain.Controls.Add(this.label5);
            this.pnlMain.Controls.Add(this.txtclosebal);
            this.pnlMain.Controls.Add(this.label6);
            this.pnlMain.Controls.Add(this.txtopenbal);
            this.pnlMain.Controls.Add(this.label4);
            this.pnlMain.Controls.Add(this.txtMobileNo2);
            this.pnlMain.Controls.Add(this.label13);
            this.pnlMain.Controls.Add(this.txtMobileNo1);
            this.pnlMain.Controls.Add(this.label12);
            this.pnlMain.Controls.Add(this.txtAddress2);
            this.pnlMain.Controls.Add(this.label8);
            this.pnlMain.Controls.Add(this.txtAddress1);
            this.pnlMain.Controls.Add(this.label7);
            this.pnlMain.Controls.Add(this.label3);
            this.pnlMain.Controls.Add(this.label22);
            this.pnlMain.Controls.Add(this.txtName);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.txtcode);
            this.pnlMain.Controls.Add(this.label21);
            this.pnlMain.Controls.Add(this.txtsearch);
            this.pnlMain.Controls.Add(this.DtgridSupMaster);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.ChkDeleted);
            this.pnlMain.Location = new System.Drawing.Point(6, 10);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(754, 542);
            this.pnlMain.TabIndex = 548;
            // 
            // btnLessID
            // 
            this.btnLessID.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnLessID.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLessID.Location = new System.Drawing.Point(334, 480);
            this.btnLessID.Name = "btnLessID";
            this.btnLessID.Size = new System.Drawing.Size(36, 25);
            this.btnLessID.TabIndex = 683;
            this.btnLessID.Text = "<<";
            this.btnLessID.UseVisualStyleBackColor = true;
            this.btnLessID.Click += new System.EventHandler(this.btnLessID_Click);
            // 
            // btnGreaterID
            // 
            this.btnGreaterID.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnGreaterID.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGreaterID.Location = new System.Drawing.Point(377, 480);
            this.btnGreaterID.Name = "btnGreaterID";
            this.btnGreaterID.Size = new System.Drawing.Size(36, 25);
            this.btnGreaterID.TabIndex = 682;
            this.btnGreaterID.Text = ">>";
            this.btnGreaterID.UseVisualStyleBackColor = true;
            this.btnGreaterID.Click += new System.EventHandler(this.btnGreaterID_Click);
            // 
            // lblTallyName
            // 
            this.lblTallyName.BackColor = System.Drawing.Color.White;
            this.lblTallyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTallyName.Location = new System.Drawing.Point(524, 340);
            this.lblTallyName.Name = "lblTallyName";
            this.lblTallyName.Size = new System.Drawing.Size(170, 22);
            this.lblTallyName.TabIndex = 684;
            this.lblTallyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmSupplierMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(774, 556);
            this.ControlBox = false;
            this.Controls.Add(this.pnlMain);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmSupplierMaster";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Supplier";
            this.Load += new System.EventHandler(this.FrmSupplierMaster_Load);
            this.Shown += new System.EventHandler(this.FrmSupplierMaster_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmSupplierMaster_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmSupplierMaster_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.DtgridSupMaster)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtsearch;
        private System.Windows.Forms.DataGridView DtgridSupMaster;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ChkDeleted;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtcode;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtAddress2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtAddress1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMobileNo2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtMobileNo1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtclosebal;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtopenbal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtCrDr;
        private System.Windows.Forms.TextBox txtCrDr1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtTallyCode;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtEmailId;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.Button BtnDelete;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.ComboBox cmbActive;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbmarketSeg;
        private System.Windows.Forms.TextBox txtGST;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblSucessMsg;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Button btnLessID;
        private System.Windows.Forms.Button btnGreaterID;
        private System.Windows.Forms.Label lblTallyName;
    }
}
namespace ASPL.LODGE.MASTERS
{
    partial class TaxMaster
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ChkDeleted = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtsearch = new System.Windows.Forms.TextBox();
            this.DtgridTaxMast = new System.Windows.Forms.DataGridView();
            this.label21 = new System.Windows.Forms.Label();
            this.txtcode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtGL_Code_1 = new System.Windows.Forms.TextBox();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.BtnExit = new System.Windows.Forms.Button();
            this.dtGrdPurdtl = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTallyCode = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblSucessMsg = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.btnLessID = new System.Windows.Forms.Button();
            this.btnGreaterID = new System.Windows.Forms.Button();
            this.lblGLName = new System.Windows.Forms.Label();
            this.lblTallyName = new System.Windows.Forms.Label();
            this.txtSrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drpDes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtUnits = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DtgridTaxMast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtGrdPurdtl)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChkDeleted
            // 
            this.ChkDeleted.AutoSize = true;
            this.ChkDeleted.BackColor = System.Drawing.Color.Transparent;
            this.ChkDeleted.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDeleted.ForeColor = System.Drawing.Color.Black;
            this.ChkDeleted.Location = new System.Drawing.Point(148, 7);
            this.ChkDeleted.Name = "ChkDeleted";
            this.ChkDeleted.Size = new System.Drawing.Size(118, 18);
            this.ChkDeleted.TabIndex = 389;
            this.ChkDeleted.Text = "Show Deleted";
            this.ChkDeleted.UseVisualStyleBackColor = false;
            this.ChkDeleted.CheckedChanged += new System.EventHandler(this.ChkDeleted_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(17, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 14);
            this.label1.TabIndex = 390;
            this.label1.Text = "Search by Name :";
            // 
            // txtsearch
            // 
            this.txtsearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtsearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtsearch.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsearch.Location = new System.Drawing.Point(144, 29);
            this.txtsearch.Name = "txtsearch";
            this.txtsearch.Size = new System.Drawing.Size(185, 22);
            this.txtsearch.TabIndex = 0;
            this.txtsearch.TextChanged += new System.EventHandler(this.txtsearch_TextChanged);
            this.txtsearch.Enter += new System.EventHandler(this.txtsearch_Enter);
            this.txtsearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtsearch_KeyDown);
            this.txtsearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtsearch_KeyPress);
            this.txtsearch.Leave += new System.EventHandler(this.txtsearch_Leave);
            // 
            // DtgridTaxMast
            // 
            this.DtgridTaxMast.AllowUserToAddRows = false;
            this.DtgridTaxMast.AllowUserToDeleteRows = false;
            this.DtgridTaxMast.AllowUserToResizeColumns = false;
            this.DtgridTaxMast.AllowUserToResizeRows = false;
            this.DtgridTaxMast.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DtgridTaxMast.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DtgridTaxMast.ColumnHeadersHeight = 25;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DtgridTaxMast.DefaultCellStyle = dataGridViewCellStyle2;
            this.DtgridTaxMast.EnableHeadersVisualStyles = false;
            this.DtgridTaxMast.Location = new System.Drawing.Point(17, 55);
            this.DtgridTaxMast.MultiSelect = false;
            this.DtgridTaxMast.Name = "DtgridTaxMast";
            this.DtgridTaxMast.ReadOnly = true;
            this.DtgridTaxMast.RowHeadersVisible = false;
            this.DtgridTaxMast.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DtgridTaxMast.Size = new System.Drawing.Size(313, 450);
            this.DtgridTaxMast.TabIndex = 392;
            this.DtgridTaxMast.TabStop = false;
            this.DtgridTaxMast.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DtgridTaxMast_CellClick);
            this.DtgridTaxMast.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DtgridTaxMast_CellDoubleClick);
            this.DtgridTaxMast.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DtgridTaxMast_KeyDown);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(379, 61);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(49, 14);
            this.label21.TabIndex = 394;
            this.label21.Text = "Code :";
            // 
            // txtcode
            // 
            this.txtcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtcode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtcode.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcode.Location = new System.Drawing.Point(434, 58);
            this.txtcode.Name = "txtcode";
            this.txtcode.Size = new System.Drawing.Size(82, 22);
            this.txtcode.TabIndex = 395;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(374, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 14);
            this.label2.TabIndex = 396;
            this.label2.Text = "Name :";
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.Red;
            this.label22.Location = new System.Drawing.Point(703, 88);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(10, 10);
            this.label22.TabIndex = 402;
            this.label22.Text = "*";
            this.label22.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.label22.Click += new System.EventHandler(this.label22_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(353, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 14);
            this.label5.TabIndex = 406;
            this.label5.Text = " GL Code :";
            // 
            // txtGL_Code_1
            // 
            this.txtGL_Code_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGL_Code_1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGL_Code_1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGL_Code_1.Location = new System.Drawing.Point(433, 157);
            this.txtGL_Code_1.Name = "txtGL_Code_1";
            this.txtGL_Code_1.Size = new System.Drawing.Size(84, 22);
            this.txtGL_Code_1.TabIndex = 4;
            this.txtGL_Code_1.Enter += new System.EventHandler(this.txtGL_Code_1_Enter);
            this.txtGL_Code_1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGL_Code_1_KeyDown);
            this.txtGL_Code_1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGL_Code_1_KeyPress);
            this.txtGL_Code_1.Leave += new System.EventHandler(this.txtGL_Code_1_Leave);
            // 
            // BtnAdd
            // 
            this.BtnAdd.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BtnAdd.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAdd.Location = new System.Drawing.Point(422, 478);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(75, 25);
            this.BtnAdd.TabIndex = 409;
            this.BtnAdd.Text = "&ADD";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Location = new System.Drawing.Point(506, 478);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(75, 25);
            this.BtnSave.TabIndex = 410;
            this.BtnSave.Text = "SAVE";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Location = new System.Drawing.Point(590, 478);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(75, 25);
            this.BtnDelete.TabIndex = 411;
            this.BtnDelete.Text = "DELETE";
            this.BtnDelete.UseVisualStyleBackColor = true;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Location = new System.Drawing.Point(674, 478);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(75, 25);
            this.BtnExit.TabIndex = 412;
            this.BtnExit.Text = "EXIT";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // dtGrdPurdtl
            // 
            this.dtGrdPurdtl.AccessibleDescription = " ";
            this.dtGrdPurdtl.AllowUserToAddRows = false;
            this.dtGrdPurdtl.AllowUserToResizeColumns = false;
            this.dtGrdPurdtl.AllowUserToResizeRows = false;
            this.dtGrdPurdtl.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtGrdPurdtl.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dtGrdPurdtl.ColumnHeadersHeight = 25;
            this.dtGrdPurdtl.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.txtSrNo,
            this.dataGridViewTextBoxColumn1,
            this.drpDes,
            this.txtUnits});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtGrdPurdtl.DefaultCellStyle = dataGridViewCellStyle8;
            this.dtGrdPurdtl.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dtGrdPurdtl.EnableHeadersVisualStyles = false;
            this.dtGrdPurdtl.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dtGrdPurdtl.Location = new System.Drawing.Point(347, 201);
            this.dtGrdPurdtl.Margin = new System.Windows.Forms.Padding(2);
            this.dtGrdPurdtl.Name = "dtGrdPurdtl";
            this.dtGrdPurdtl.RowHeadersVisible = false;
            this.dtGrdPurdtl.RowTemplate.Height = 24;
            this.dtGrdPurdtl.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dtGrdPurdtl.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dtGrdPurdtl.Size = new System.Drawing.Size(374, 170);
            this.dtGrdPurdtl.TabIndex = 5;
            this.dtGrdPurdtl.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtGrdPurdtl_CellEnter_1);
            this.dtGrdPurdtl.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtGrdPurdtl_CellLeave_1);
            this.dtGrdPurdtl.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dtGrdPurdtl_EditingControlShowing);
            this.dtGrdPurdtl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtGrdPurdtl_KeyDown_1);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(702, 121);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(10, 10);
            this.label7.TabIndex = 415;
            this.label7.Text = "*";
            this.label7.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // txtTallyCode
            // 
            this.txtTallyCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTallyCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTallyCode.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTallyCode.Location = new System.Drawing.Point(434, 124);
            this.txtTallyCode.Name = "txtTallyCode";
            this.txtTallyCode.Size = new System.Drawing.Size(82, 22);
            this.txtTallyCode.TabIndex = 3;
            this.txtTallyCode.Enter += new System.EventHandler(this.txtTallyCode_Enter);
            this.txtTallyCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTallyCode_KeyDown);
            this.txtTallyCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTallyCode_KeyPress);
            this.txtTallyCode.Leave += new System.EventHandler(this.txtTallyCode_Leave);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(344, 125);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(85, 14);
            this.label11.TabIndex = 455;
            this.label11.Text = "Tally Code :";
            // 
            // txtName
            // 
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(434, 90);
            this.txtName.MaxLength = 50;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(266, 22);
            this.txtName.TabIndex = 1;
            this.txtName.Enter += new System.EventHandler(this.txtName_Enter);
            this.txtName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtName_KeyPress);
            this.txtName.Leave += new System.EventHandler(this.txtName_Leave);
            // 
            // lblSucessMsg
            // 
            this.lblSucessMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblSucessMsg.Font = new System.Drawing.Font("Verdana", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSucessMsg.ForeColor = System.Drawing.Color.Black;
            this.lblSucessMsg.Location = new System.Drawing.Point(23, 508);
            this.lblSucessMsg.Name = "lblSucessMsg";
            this.lblSucessMsg.Size = new System.Drawing.Size(260, 24);
            this.lblSucessMsg.TabIndex = 506;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.btnLessID);
            this.pnlMain.Controls.Add(this.btnGreaterID);
            this.pnlMain.Controls.Add(this.lblGLName);
            this.pnlMain.Controls.Add(this.lblTallyName);
            this.pnlMain.Controls.Add(this.lblSucessMsg);
            this.pnlMain.Controls.Add(this.txtTallyCode);
            this.pnlMain.Controls.Add(this.label11);
            this.pnlMain.Controls.Add(this.label7);
            this.pnlMain.Controls.Add(this.dtGrdPurdtl);
            this.pnlMain.Controls.Add(this.BtnExit);
            this.pnlMain.Controls.Add(this.BtnDelete);
            this.pnlMain.Controls.Add(this.BtnSave);
            this.pnlMain.Controls.Add(this.BtnAdd);
            this.pnlMain.Controls.Add(this.txtGL_Code_1);
            this.pnlMain.Controls.Add(this.label5);
            this.pnlMain.Controls.Add(this.label22);
            this.pnlMain.Controls.Add(this.txtName);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.txtcode);
            this.pnlMain.Controls.Add(this.label21);
            this.pnlMain.Controls.Add(this.DtgridTaxMast);
            this.pnlMain.Controls.Add(this.txtsearch);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.ChkDeleted);
            this.pnlMain.Location = new System.Drawing.Point(1, 5);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(760, 551);
            this.pnlMain.TabIndex = 507;
            this.pnlMain.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlMain_Paint);
            // 
            // btnLessID
            // 
            this.btnLessID.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnLessID.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLessID.Location = new System.Drawing.Point(332, 478);
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
            this.btnGreaterID.Location = new System.Drawing.Point(377, 478);
            this.btnGreaterID.Name = "btnGreaterID";
            this.btnGreaterID.Size = new System.Drawing.Size(36, 25);
            this.btnGreaterID.TabIndex = 664;
            this.btnGreaterID.Text = ">>";
            this.btnGreaterID.UseVisualStyleBackColor = true;
            this.btnGreaterID.Click += new System.EventHandler(this.btnGreaterID_Click);
            // 
            // lblGLName
            // 
            this.lblGLName.BackColor = System.Drawing.Color.White;
            this.lblGLName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblGLName.Location = new System.Drawing.Point(523, 157);
            this.lblGLName.Name = "lblGLName";
            this.lblGLName.Size = new System.Drawing.Size(177, 22);
            this.lblGLName.TabIndex = 663;
            this.lblGLName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTallyName
            // 
            this.lblTallyName.BackColor = System.Drawing.Color.White;
            this.lblTallyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTallyName.Location = new System.Drawing.Point(523, 124);
            this.lblTallyName.Name = "lblTallyName";
            this.lblTallyName.Size = new System.Drawing.Size(177, 22);
            this.lblTallyName.TabIndex = 662;
            this.lblTallyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSrNo
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            this.txtSrNo.DefaultCellStyle = dataGridViewCellStyle4;
            this.txtSrNo.HeaderText = "Sr No.";
            this.txtSrNo.MinimumWidth = 10;
            this.txtSrNo.Name = "txtSrNo";
            this.txtSrNo.ReadOnly = true;
            this.txtSrNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.txtSrNo.Width = 52;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTextBoxColumn1.HeaderText = "Slab Amt. From";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 113;
            // 
            // drpDes
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.drpDes.DefaultCellStyle = dataGridViewCellStyle6;
            this.drpDes.HeaderText = "Slab Amt. To";
            this.drpDes.Name = "drpDes";
            this.drpDes.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.drpDes.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.drpDes.Width = 110;
            // 
            // txtUnits
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "N2";
            dataGridViewCellStyle7.NullValue = null;
            this.txtUnits.DefaultCellStyle = dataGridViewCellStyle7;
            this.txtUnits.HeaderText = "Tax Amt %";
            this.txtUnits.Name = "txtUnits";
            this.txtUnits.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.txtUnits.Width = 96;
            // 
            // TaxMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(785, 565);
            this.ControlBox = false;
            this.Controls.Add(this.pnlMain);
            this.KeyPreview = true;
            this.Name = "TaxMaster";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tax Master";
            this.Load += new System.EventHandler(this.TaxMaster_Load);
            this.Shown += new System.EventHandler(this.TaxMaster_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TaxMaster_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TaxMaster_KeyPress_1);
            ((System.ComponentModel.ISupportInitialize)(this.DtgridTaxMast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtGrdPurdtl)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox ChkDeleted;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtsearch;
        private System.Windows.Forms.DataGridView DtgridTaxMast;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtcode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtGL_Code_1;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnDelete;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.DataGridView dtGrdPurdtl;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTallyCode;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblSucessMsg;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblTallyName;
        private System.Windows.Forms.Label lblGLName;
        private System.Windows.Forms.Button btnLessID;
        private System.Windows.Forms.Button btnGreaterID;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtSrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn drpDes;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtUnits;
    }
}
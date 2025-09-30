namespace ASPL.LODGE.MASTERS
{
    partial class GeneralLedger
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
            this.label1 = new System.Windows.Forms.Label();
            this.DtgridGenralLedger = new System.Windows.Forms.DataGridView();
            this.label21 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtopenbal = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtperyearbal = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtclosebal = new System.Windows.Forms.TextBox();
            this.txtclbalcrdr = new System.Windows.Forms.TextBox();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.BtnExit = new System.Windows.Forms.Button();
            this.combPreyarbal = new System.Windows.Forms.ComboBox();
            this.combOpenBal = new System.Windows.Forms.ComboBox();
            this.txtcode = new System.Windows.Forms.TextBox();
            this.txtgroup1 = new System.Windows.Forms.TextBox();
            this.txtsearch = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblSucessMsg = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.btnLessID = new System.Windows.Forms.Button();
            this.btnGreaterID = new System.Windows.Forms.Button();
            this.lblGroupName = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbSubLedYN = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.DtgridGenralLedger)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChkDeleted
            // 
            this.ChkDeleted.AutoSize = true;
            this.ChkDeleted.BackColor = System.Drawing.Color.Transparent;
            this.ChkDeleted.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDeleted.ForeColor = System.Drawing.Color.Black;
            this.ChkDeleted.Location = new System.Drawing.Point(149, 9);
            this.ChkDeleted.Name = "ChkDeleted";
            this.ChkDeleted.Size = new System.Drawing.Size(118, 18);
            this.ChkDeleted.TabIndex = 395;
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
            this.label1.Location = new System.Drawing.Point(22, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 14);
            this.label1.TabIndex = 396;
            this.label1.Text = "Search by Name :";
            // 
            // DtgridGenralLedger
            // 
            this.DtgridGenralLedger.AllowUserToAddRows = false;
            this.DtgridGenralLedger.AllowUserToDeleteRows = false;
            this.DtgridGenralLedger.AllowUserToResizeColumns = false;
            this.DtgridGenralLedger.AllowUserToResizeRows = false;
            this.DtgridGenralLedger.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DtgridGenralLedger.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DtgridGenralLedger.ColumnHeadersHeight = 25;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DtgridGenralLedger.DefaultCellStyle = dataGridViewCellStyle2;
            this.DtgridGenralLedger.EnableHeadersVisualStyles = false;
            this.DtgridGenralLedger.Location = new System.Drawing.Point(18, 56);
            this.DtgridGenralLedger.MultiSelect = false;
            this.DtgridGenralLedger.Name = "DtgridGenralLedger";
            this.DtgridGenralLedger.ReadOnly = true;
            this.DtgridGenralLedger.RowHeadersVisible = false;
            this.DtgridGenralLedger.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DtgridGenralLedger.Size = new System.Drawing.Size(313, 450);
            this.DtgridGenralLedger.TabIndex = 398;
            this.DtgridGenralLedger.TabStop = false;
            this.DtgridGenralLedger.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DtgridGenralLedger_CellClick);
            this.DtgridGenralLedger.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DtgridGenralLedger_CellDoubleClick);
            this.DtgridGenralLedger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DtgridGenralLedger_KeyDown);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(373, 63);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(99, 14);
            this.label21.TabIndex = 399;
            this.label21.Text = "Ledger Code :";
            this.label21.Click += new System.EventHandler(this.label21_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(369, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 14);
            this.label2.TabIndex = 401;
            this.label2.Text = "Ledger Name :";
            // 
            // txtName
            // 
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(473, 93);
            this.txtName.MaxLength = 50;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(228, 22);
            this.txtName.TabIndex = 1;
            this.txtName.Enter += new System.EventHandler(this.txtName_Enter);
            this.txtName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtName_KeyPress);
            this.txtName.Leave += new System.EventHandler(this.txtName_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(416, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 14);
            this.label3.TabIndex = 403;
            this.label3.Text = "Group :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(345, 192);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 14);
            this.label4.TabIndex = 407;
            this.label4.Text = "Opening Balance :";
            // 
            // txtopenbal
            // 
            this.txtopenbal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtopenbal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtopenbal.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtopenbal.Location = new System.Drawing.Point(473, 189);
            this.txtopenbal.Name = "txtopenbal";
            this.txtopenbal.Size = new System.Drawing.Size(116, 22);
            this.txtopenbal.TabIndex = 4;
            this.txtopenbal.Enter += new System.EventHandler(this.txtopenbal_Enter);
            this.txtopenbal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtopenbal_KeyPress);
            this.txtopenbal.Leave += new System.EventHandler(this.txtopenbal_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(337, 226);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(135, 14);
            this.label5.TabIndex = 411;
            this.label5.Text = "Previous Year Bal :";
            // 
            // txtperyearbal
            // 
            this.txtperyearbal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtperyearbal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtperyearbal.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtperyearbal.Location = new System.Drawing.Point(473, 224);
            this.txtperyearbal.Name = "txtperyearbal";
            this.txtperyearbal.Size = new System.Drawing.Size(116, 22);
            this.txtperyearbal.TabIndex = 6;
            this.txtperyearbal.Enter += new System.EventHandler(this.txtperyearbal_Enter);
            this.txtperyearbal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtperyearbal_KeyPress);
            this.txtperyearbal.Leave += new System.EventHandler(this.txtperyearbal_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(352, 262);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 14);
            this.label6.TabIndex = 415;
            this.label6.Text = "Closing Balance :";
            // 
            // txtclosebal
            // 
            this.txtclosebal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtclosebal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtclosebal.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtclosebal.Location = new System.Drawing.Point(473, 260);
            this.txtclosebal.Name = "txtclosebal";
            this.txtclosebal.Size = new System.Drawing.Size(116, 22);
            this.txtclosebal.TabIndex = 8;
            this.txtclosebal.Enter += new System.EventHandler(this.txtclosebal_Enter);
            this.txtclosebal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtclosebal_KeyPress);
            this.txtclosebal.Leave += new System.EventHandler(this.txtclosebal_Leave);
            // 
            // txtclbalcrdr
            // 
            this.txtclbalcrdr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtclbalcrdr.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtclbalcrdr.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtclbalcrdr.Location = new System.Drawing.Point(603, 260);
            this.txtclbalcrdr.Name = "txtclbalcrdr";
            this.txtclbalcrdr.Size = new System.Drawing.Size(48, 22);
            this.txtclbalcrdr.TabIndex = 9;
            // 
            // BtnAdd
            // 
            this.BtnAdd.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BtnAdd.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAdd.Location = new System.Drawing.Point(431, 481);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(75, 25);
            this.BtnAdd.TabIndex = 418;
            this.BtnAdd.Text = "&ADD";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Location = new System.Drawing.Point(517, 481);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(75, 25);
            this.BtnSave.TabIndex = 10;
            this.BtnSave.Text = "SAVE";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Location = new System.Drawing.Point(603, 481);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(75, 25);
            this.BtnDelete.TabIndex = 420;
            this.BtnDelete.Text = "DELETE";
            this.BtnDelete.UseVisualStyleBackColor = true;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Location = new System.Drawing.Point(689, 481);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(75, 25);
            this.BtnExit.TabIndex = 421;
            this.BtnExit.Text = "EXIT";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // combPreyarbal
            // 
            this.combPreyarbal.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combPreyarbal.FormattingEnabled = true;
            this.combPreyarbal.Location = new System.Drawing.Point(602, 224);
            this.combPreyarbal.Name = "combPreyarbal";
            this.combPreyarbal.Size = new System.Drawing.Size(83, 21);
            this.combPreyarbal.TabIndex = 7;
            this.combPreyarbal.Enter += new System.EventHandler(this.combPreyarbal_Enter);
            this.combPreyarbal.Leave += new System.EventHandler(this.combPreyarbal_Leave);
            // 
            // combOpenBal
            // 
            this.combOpenBal.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combOpenBal.FormattingEnabled = true;
            this.combOpenBal.Location = new System.Drawing.Point(601, 190);
            this.combOpenBal.Name = "combOpenBal";
            this.combOpenBal.Size = new System.Drawing.Size(83, 21);
            this.combOpenBal.TabIndex = 5;
            this.combOpenBal.Enter += new System.EventHandler(this.combOpenBal_Enter);
            this.combOpenBal.Leave += new System.EventHandler(this.combOpenBal_Leave);
            // 
            // txtcode
            // 
            this.txtcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtcode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtcode.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcode.Location = new System.Drawing.Point(474, 61);
            this.txtcode.Name = "txtcode";
            this.txtcode.ReadOnly = true;
            this.txtcode.Size = new System.Drawing.Size(62, 22);
            this.txtcode.TabIndex = 400;
            // 
            // txtgroup1
            // 
            this.txtgroup1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtgroup1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtgroup1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtgroup1.Location = new System.Drawing.Point(474, 124);
            this.txtgroup1.Name = "txtgroup1";
            this.txtgroup1.Size = new System.Drawing.Size(62, 22);
            this.txtgroup1.TabIndex = 2;
            this.txtgroup1.Enter += new System.EventHandler(this.txtgroup1_Enter);
            this.txtgroup1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtgroup1_KeyDown_1);
            this.txtgroup1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtgroup1_KeyPress_1);
            this.txtgroup1.Leave += new System.EventHandler(this.txtgroup1_Leave);
            // 
            // txtsearch
            // 
            this.txtsearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtsearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtsearch.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsearch.Location = new System.Drawing.Point(146, 31);
            this.txtsearch.Name = "txtsearch";
            this.txtsearch.Size = new System.Drawing.Size(185, 22);
            this.txtsearch.TabIndex = 0;
            this.txtsearch.TextChanged += new System.EventHandler(this.txtsearch_TextChanged_1);
            this.txtsearch.Enter += new System.EventHandler(this.txtsearch_Enter_1);
            this.txtsearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtsearch_KeyDown_1);
            this.txtsearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtsearch_KeyPress);
            this.txtsearch.Leave += new System.EventHandler(this.txtsearch_Leave);
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.Red;
            this.label22.Location = new System.Drawing.Point(706, 93);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(10, 10);
            this.label22.TabIndex = 437;
            this.label22.Text = "*";
            this.label22.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(704, 125);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(10, 10);
            this.label7.TabIndex = 438;
            this.label7.Text = "*";
            this.label7.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // lblSucessMsg
            // 
            this.lblSucessMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblSucessMsg.Font = new System.Drawing.Font("Verdana", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSucessMsg.ForeColor = System.Drawing.Color.Black;
            this.lblSucessMsg.Location = new System.Drawing.Point(23, 509);
            this.lblSucessMsg.Name = "lblSucessMsg";
            this.lblSucessMsg.Size = new System.Drawing.Size(260, 24);
            this.lblSucessMsg.TabIndex = 505;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.btnLessID);
            this.pnlMain.Controls.Add(this.btnGreaterID);
            this.pnlMain.Controls.Add(this.lblGroupName);
            this.pnlMain.Controls.Add(this.label8);
            this.pnlMain.Controls.Add(this.cmbSubLedYN);
            this.pnlMain.Controls.Add(this.lblSucessMsg);
            this.pnlMain.Controls.Add(this.label7);
            this.pnlMain.Controls.Add(this.label22);
            this.pnlMain.Controls.Add(this.label21);
            this.pnlMain.Controls.Add(this.txtcode);
            this.pnlMain.Controls.Add(this.txtsearch);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.txtName);
            this.pnlMain.Controls.Add(this.txtgroup1);
            this.pnlMain.Controls.Add(this.combOpenBal);
            this.pnlMain.Controls.Add(this.BtnExit);
            this.pnlMain.Controls.Add(this.label3);
            this.pnlMain.Controls.Add(this.DtgridGenralLedger);
            this.pnlMain.Controls.Add(this.combPreyarbal);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.BtnDelete);
            this.pnlMain.Controls.Add(this.ChkDeleted);
            this.pnlMain.Controls.Add(this.BtnSave);
            this.pnlMain.Controls.Add(this.BtnAdd);
            this.pnlMain.Controls.Add(this.txtclbalcrdr);
            this.pnlMain.Controls.Add(this.label4);
            this.pnlMain.Controls.Add(this.txtperyearbal);
            this.pnlMain.Controls.Add(this.txtclosebal);
            this.pnlMain.Controls.Add(this.label5);
            this.pnlMain.Controls.Add(this.txtopenbal);
            this.pnlMain.Controls.Add(this.label6);
            this.pnlMain.Location = new System.Drawing.Point(4, 3);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(774, 543);
            this.pnlMain.TabIndex = 506;
            // 
            // btnLessID
            // 
            this.btnLessID.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnLessID.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLessID.Location = new System.Drawing.Point(337, 481);
            this.btnLessID.Name = "btnLessID";
            this.btnLessID.Size = new System.Drawing.Size(36, 25);
            this.btnLessID.TabIndex = 675;
            this.btnLessID.Text = "<<";
            this.btnLessID.UseVisualStyleBackColor = true;
            this.btnLessID.Click += new System.EventHandler(this.btnLessID_Click);
            // 
            // btnGreaterID
            // 
            this.btnGreaterID.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnGreaterID.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGreaterID.Location = new System.Drawing.Point(384, 481);
            this.btnGreaterID.Name = "btnGreaterID";
            this.btnGreaterID.Size = new System.Drawing.Size(36, 25);
            this.btnGreaterID.TabIndex = 674;
            this.btnGreaterID.Text = ">>";
            this.btnGreaterID.UseVisualStyleBackColor = true;
            this.btnGreaterID.Click += new System.EventHandler(this.btnGreaterID_Click);
            // 
            // lblGroupName
            // 
            this.lblGroupName.BackColor = System.Drawing.Color.White;
            this.lblGroupName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblGroupName.Location = new System.Drawing.Point(542, 124);
            this.lblGroupName.Name = "lblGroupName";
            this.lblGroupName.Size = new System.Drawing.Size(159, 22);
            this.lblGroupName.TabIndex = 658;
            this.lblGroupName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(381, 158);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 14);
            this.label8.TabIndex = 507;
            this.label8.Text = "Sub Ledger :";
            // 
            // cmbSubLedYN
            // 
            this.cmbSubLedYN.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSubLedYN.FormattingEnabled = true;
            this.cmbSubLedYN.Location = new System.Drawing.Point(474, 158);
            this.cmbSubLedYN.Name = "cmbSubLedYN";
            this.cmbSubLedYN.Size = new System.Drawing.Size(83, 21);
            this.cmbSubLedYN.TabIndex = 3;
            this.cmbSubLedYN.SelectedIndexChanged += new System.EventHandler(this.cmbSubLedYN_SelectedIndexChanged);
            // 
            // GeneralLedger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(795, 557);
            this.ControlBox = false;
            this.Controls.Add(this.pnlMain);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "GeneralLedger";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "General Ledger";
            this.Load += new System.EventHandler(this.GeneralLedger_Load);
            this.Shown += new System.EventHandler(this.GeneralLedger_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GeneralLedger_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GeneralLedger_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.DtgridGenralLedger)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox ChkDeleted;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView DtgridGenralLedger;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtopenbal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtperyearbal;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtclosebal;
        private System.Windows.Forms.TextBox txtclbalcrdr;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnDelete;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.ComboBox combPreyarbal;
        private System.Windows.Forms.ComboBox combOpenBal;
        private System.Windows.Forms.TextBox txtgroup1;
        private System.Windows.Forms.TextBox txtsearch;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblSucessMsg;
        private System.Windows.Forms.TextBox txtcode;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.ComboBox cmbSubLedYN;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblGroupName;
        private System.Windows.Forms.Button btnLessID;
        private System.Windows.Forms.Button btnGreaterID;
    }
}
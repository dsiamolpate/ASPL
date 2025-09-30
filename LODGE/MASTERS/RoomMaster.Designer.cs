namespace ASPL.LODGE.MASTERS
{
    partial class RoomMaster
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
            this.DtgridRoomCat = new System.Windows.Forms.DataGridView();
            this.label22 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDoubleRate = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSingleRate = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRomCategry = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.BtnExit = new System.Windows.Forms.Button();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.txtsearch = new System.Windows.Forms.TextBox();
            this.txtRoomNo = new System.Windows.Forms.TextBox();
            this.lblSucessMsg = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.lblRoomCatName = new System.Windows.Forms.Label();
            this.btnLessID = new System.Windows.Forms.Button();
            this.btnGreaterID = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DtgridRoomCat)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChkDeleted
            // 
            this.ChkDeleted.AutoSize = true;
            this.ChkDeleted.BackColor = System.Drawing.Color.Transparent;
            this.ChkDeleted.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDeleted.ForeColor = System.Drawing.Color.Black;
            this.ChkDeleted.Location = new System.Drawing.Point(141, 4);
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
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 14);
            this.label1.TabIndex = 390;
            this.label1.Text = "Search by Name :";
            // 
            // DtgridRoomCat
            // 
            this.DtgridRoomCat.AllowUserToAddRows = false;
            this.DtgridRoomCat.AllowUserToDeleteRows = false;
            this.DtgridRoomCat.AllowUserToResizeColumns = false;
            this.DtgridRoomCat.AllowUserToResizeRows = false;
            this.DtgridRoomCat.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DtgridRoomCat.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DtgridRoomCat.ColumnHeadersHeight = 25;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DtgridRoomCat.DefaultCellStyle = dataGridViewCellStyle2;
            this.DtgridRoomCat.EnableHeadersVisualStyles = false;
            this.DtgridRoomCat.Location = new System.Drawing.Point(13, 51);
            this.DtgridRoomCat.MultiSelect = false;
            this.DtgridRoomCat.Name = "DtgridRoomCat";
            this.DtgridRoomCat.ReadOnly = true;
            this.DtgridRoomCat.RowHeadersVisible = false;
            this.DtgridRoomCat.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DtgridRoomCat.Size = new System.Drawing.Size(313, 450);
            this.DtgridRoomCat.TabIndex = 452;
            this.DtgridRoomCat.TabStop = false;
            this.DtgridRoomCat.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DtgridRoomCat_CellClick);
            this.DtgridRoomCat.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DtgridRoomCat_CellDoubleClick);
            this.DtgridRoomCat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DtgridRoomCat_KeyDown);
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.Red;
            this.label22.Location = new System.Drawing.Point(696, 92);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(10, 10);
            this.label22.TabIndex = 421;
            this.label22.Text = "*";
            this.label22.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(524, 162);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(10, 10);
            this.label6.TabIndex = 420;
            this.label6.Text = "*";
            this.label6.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(524, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(10, 10);
            this.label5.TabIndex = 419;
            this.label5.Text = "*";
            this.label5.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // txtDoubleRate
            // 
            this.txtDoubleRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDoubleRate.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDoubleRate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDoubleRate.Location = new System.Drawing.Point(445, 162);
            this.txtDoubleRate.Name = "txtDoubleRate";
            this.txtDoubleRate.Size = new System.Drawing.Size(73, 22);
            this.txtDoubleRate.TabIndex = 4;
            this.txtDoubleRate.Enter += new System.EventHandler(this.txtDoubleRate_Enter);
            this.txtDoubleRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDoubleRate_KeyPress_1);
            this.txtDoubleRate.Leave += new System.EventHandler(this.txtDoubleRate_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(348, 166);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 14);
            this.label4.TabIndex = 417;
            this.label4.Text = "Double Rate :";
            // 
            // txtSingleRate
            // 
            this.txtSingleRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSingleRate.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSingleRate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSingleRate.Location = new System.Drawing.Point(446, 129);
            this.txtSingleRate.Name = "txtSingleRate";
            this.txtSingleRate.Size = new System.Drawing.Size(73, 22);
            this.txtSingleRate.TabIndex = 3;
            this.txtSingleRate.Enter += new System.EventHandler(this.txtSingleRate_Enter);
            this.txtSingleRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSingleRate_KeyPress);
            this.txtSingleRate.Leave += new System.EventHandler(this.txtSingleRate_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(354, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 14);
            this.label3.TabIndex = 415;
            this.label3.Text = "Single Rate :";
            // 
            // txtRomCategry
            // 
            this.txtRomCategry.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRomCategry.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRomCategry.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRomCategry.Location = new System.Drawing.Point(446, 92);
            this.txtRomCategry.Name = "txtRomCategry";
            this.txtRomCategry.Size = new System.Drawing.Size(73, 22);
            this.txtRomCategry.TabIndex = 2;
            this.txtRomCategry.Enter += new System.EventHandler(this.txtRomCategry_Enter);
            this.txtRomCategry.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRomCategry_KeyDown);
            this.txtRomCategry.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRomCategry_KeyPress);
            this.txtRomCategry.Leave += new System.EventHandler(this.txtRomCategry_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(329, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 14);
            this.label2.TabIndex = 412;
            this.label2.Text = "Room Category :";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(370, 59);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(75, 14);
            this.label21.TabIndex = 410;
            this.label21.Text = "Room No :";
            // 
            // BtnExit
            // 
            this.BtnExit.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Location = new System.Drawing.Point(679, 476);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(75, 25);
            this.BtnExit.TabIndex = 426;
            this.BtnExit.Text = "EXIT";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Location = new System.Drawing.Point(594, 476);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(75, 25);
            this.BtnDelete.TabIndex = 425;
            this.BtnDelete.Text = "DELETE";
            this.BtnDelete.UseVisualStyleBackColor = true;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Location = new System.Drawing.Point(509, 476);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(75, 25);
            this.BtnSave.TabIndex = 424;
            this.BtnSave.Text = "SAVE";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnAdd
            // 
            this.BtnAdd.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BtnAdd.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAdd.Location = new System.Drawing.Point(424, 476);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(75, 25);
            this.BtnAdd.TabIndex = 423;
            this.BtnAdd.Text = "&ADD";
            this.BtnAdd.UseVisualStyleBackColor = false;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // txtsearch
            // 
            this.txtsearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtsearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtsearch.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsearch.Location = new System.Drawing.Point(141, 25);
            this.txtsearch.Name = "txtsearch";
            this.txtsearch.Size = new System.Drawing.Size(185, 22);
            this.txtsearch.TabIndex = 0;
            this.txtsearch.TextChanged += new System.EventHandler(this.txtsearch_TextChanged_1);
            this.txtsearch.Enter += new System.EventHandler(this.txtsearch_Enter_1);
            this.txtsearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtsearch_KeyDown_1);
            this.txtsearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtsearch_KeyPress);
            this.txtsearch.Leave += new System.EventHandler(this.txtsearch_Leave);
            // 
            // txtRoomNo
            // 
            this.txtRoomNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRoomNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRoomNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRoomNo.Location = new System.Drawing.Point(446, 55);
            this.txtRoomNo.Name = "txtRoomNo";
            this.txtRoomNo.Size = new System.Drawing.Size(73, 22);
            this.txtRoomNo.TabIndex = 1;
            this.txtRoomNo.Enter += new System.EventHandler(this.txtRoomNo_Enter_1);
            this.txtRoomNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRoomNo_KeyDown_1);
            this.txtRoomNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRoomNo_KeyPress);
            this.txtRoomNo.Leave += new System.EventHandler(this.txtRoomNo_Leave);
            // 
            // lblSucessMsg
            // 
            this.lblSucessMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblSucessMsg.Font = new System.Drawing.Font("Verdana", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSucessMsg.ForeColor = System.Drawing.Color.Black;
            this.lblSucessMsg.Location = new System.Drawing.Point(16, 504);
            this.lblSucessMsg.Name = "lblSucessMsg";
            this.lblSucessMsg.Size = new System.Drawing.Size(260, 24);
            this.lblSucessMsg.TabIndex = 454;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.btnLessID);
            this.pnlMain.Controls.Add(this.btnGreaterID);
            this.pnlMain.Controls.Add(this.lblRoomCatName);
            this.pnlMain.Controls.Add(this.lblSucessMsg);
            this.pnlMain.Controls.Add(this.txtRoomNo);
            this.pnlMain.Controls.Add(this.txtsearch);
            this.pnlMain.Controls.Add(this.BtnExit);
            this.pnlMain.Controls.Add(this.BtnDelete);
            this.pnlMain.Controls.Add(this.BtnSave);
            this.pnlMain.Controls.Add(this.BtnAdd);
            this.pnlMain.Controls.Add(this.label22);
            this.pnlMain.Controls.Add(this.label6);
            this.pnlMain.Controls.Add(this.label5);
            this.pnlMain.Controls.Add(this.txtDoubleRate);
            this.pnlMain.Controls.Add(this.label4);
            this.pnlMain.Controls.Add(this.txtSingleRate);
            this.pnlMain.Controls.Add(this.label3);
            this.pnlMain.Controls.Add(this.txtRomCategry);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.label21);
            this.pnlMain.Controls.Add(this.DtgridRoomCat);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.ChkDeleted);
            this.pnlMain.Location = new System.Drawing.Point(9, 8);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(765, 536);
            this.pnlMain.TabIndex = 455;
            // 
            // lblRoomCatName
            // 
            this.lblRoomCatName.BackColor = System.Drawing.Color.White;
            this.lblRoomCatName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRoomCatName.Location = new System.Drawing.Point(528, 92);
            this.lblRoomCatName.Name = "lblRoomCatName";
            this.lblRoomCatName.Size = new System.Drawing.Size(164, 22);
            this.lblRoomCatName.TabIndex = 659;
            this.lblRoomCatName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnLessID
            // 
            this.btnLessID.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnLessID.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLessID.Location = new System.Drawing.Point(332, 476);
            this.btnLessID.Name = "btnLessID";
            this.btnLessID.Size = new System.Drawing.Size(36, 25);
            this.btnLessID.TabIndex = 661;
            this.btnLessID.Text = "<<";
            this.btnLessID.UseVisualStyleBackColor = true;
            this.btnLessID.Click += new System.EventHandler(this.btnLessID_Click);
            // 
            // btnGreaterID
            // 
            this.btnGreaterID.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnGreaterID.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGreaterID.Location = new System.Drawing.Point(378, 476);
            this.btnGreaterID.Name = "btnGreaterID";
            this.btnGreaterID.Size = new System.Drawing.Size(36, 25);
            this.btnGreaterID.TabIndex = 660;
            this.btnGreaterID.Text = ">>";
            this.btnGreaterID.UseVisualStyleBackColor = true;
            // 
            // RoomMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(790, 556);
            this.ControlBox = false;
            this.Controls.Add(this.pnlMain);
            this.ForeColor = System.Drawing.Color.Black;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "RoomMaster";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create Room";
            this.Load += new System.EventHandler(this.RoomMaster_Load);
            this.Shown += new System.EventHandler(this.RoomMaster_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RoomMaster_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RoomMaster_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.DtgridRoomCat)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox ChkDeleted;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView DtgridRoomCat;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDoubleRate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSingleRate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRomCategry;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.Button BtnDelete;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.TextBox txtsearch;
        private System.Windows.Forms.TextBox txtRoomNo;
        private System.Windows.Forms.Label lblSucessMsg;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblRoomCatName;
        private System.Windows.Forms.Button btnLessID;
        private System.Windows.Forms.Button btnGreaterID;
    }
}
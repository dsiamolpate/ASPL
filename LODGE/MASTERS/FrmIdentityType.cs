using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;
using System.Net;
using System.IO;
using ASPL.CLASSES;
using ASPL.STARTUP.FORMS;

namespace ASPL.LODGE.MASTERS
{
    public partial class FrmIdentityType : Form
    {
        Design ObjDesign = new Design();
        public FrmIdentityType()
        {
            InitializeComponent();
        }
        Connection con = new Connection();
        Module mod = new Module();
        SqlDataReader dr;
        string firstCol = "Id_Code";
        string secondCol = "Id_type";
        string tableName = "tbIdentityType";
        string Deleted = "Deleted";
        string str, strUnderCd, StrUpdate;
        int underCd, selectrow;
        #region HIDE CLOSE BUTTON
        //Hide Close Button
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }
        #endregion

        private void FrmIdentityType_Load(object sender, EventArgs e)
        {
            LoadEvent();
        }
        public void LoadEvent()
        {
            try
            {
                ObjDesign.FormDesign(this, DtgridIdentyType);
                mod.FormLoad(this, tableName, firstCol, secondCol, Deleted, "N", "N", DtgridIdentyType, txtsearch, ChkDeleted, "Description", "Code");
                //  mod.FormLoad(this, firstCol, secondCol, tableName, DtgridIdentyType, txtsearch, ChkDeleted);    
                txtsearch.Focus();
                txtcode.Enabled = false;
                FillDetails();
                mod.UserAccessibillityMaster("Identity Master", BtnAdd, BtnSave, BtnDelete);
                Globalvariable.SearchChangeVariable = "SrchByName";
            }

            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }


        public void FillDetails()
        {
            try
            {
                if (ChkDeleted.Checked == true)
                {
                    mod.chkDeletedTrue(this, BtnAdd, BtnSave, BtnDelete, DtgridIdentyType, txtsearch);
                    txtsearch.Focus();
                }
                else
                {
                    mod.chkDeletedFalse("Identity Master", this, BtnAdd, BtnSave, BtnDelete, DtgridIdentyType, txtsearch);
                    ChkDeleted.Enabled = true;
                }

                if (DtgridIdentyType.Rows.Count > 0)
                {
                    int i;
                    if (StrUpdate == "U")
                    {
                        i = selectrow;
                    }
                    else
                    {
                        i = DtgridIdentyType.SelectedCells[0].RowIndex;
                    }
                    if (DtgridIdentyType.Rows[i].Cells[1].Value != null)
                    {
                        string ID = mod.Isnull(DtgridIdentyType.Rows[i].Cells[1].Value.ToString(), "");
                        //SqlDataReader dr = mod.GetRecord("SELECT * FROM " + tableName + " WHERE " +
                        //                     firstCol + " ='" + s + "' AND Branchcode='" + Globalvariable.bcode + "'");
                        SqlDataReader dr = mod.GetSelectAllField(tableName, firstCol, ID, "");
                        while (dr.Read())
                        {
                            txtcode.Text = dr["Id_Code"].ToString();
                            txtcode.Enabled = false;
                            txtIDtype.Text = dr["Id_type"].ToString();
                        }
                    }
                }
                else
                {
                    mod.txtclear(this.Controls);
                    BtnDelete.Text = "RESET";

                }
                //   lblSucessMsg.Text = "";
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Add();
        }

        public void Add()
        {
            try
            {
                mod.addBtnClick(this, txtcode, tableName, firstCol, BtnSave, BtnDelete, txtIDtype);
                mod.UserAccessibillityMaster("Identity Master", BtnAdd, BtnSave, BtnDelete);
                // Max_ID();
                txtcode.Text = "" + mod.GetMaxNum(tableName, firstCol);
                lblSucessMsg.Text = "";
                label1.Text = "Search by Name :";
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        public void Max_ID()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT (ISNULL(MAX(Id_Code),0)+1) FROM tbIdentityType", con.connect());
                cmd.ExecuteNonQuery();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                txtcode.Text = "" + count;
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        public void white_Textbox()
        {
            txtsearch.BackColor = Color.White;
            txtsearch.BackColor = Color.White;
            txtsearch.BackColor = Color.White;

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            txtIDtype.BackColor = Color.White;
            Save();
        }

        private void Save()
        {
            try
            {
                if (DtgridIdentyType.Rows.Count > 0)
                {
                    selectrow = DtgridIdentyType.SelectedCells[0].RowIndex;
                }
                int i;
                if (txtIDtype.Text == "")
                {
                    txtIDtype.Focus();
                }
                else
                {

                    if (BtnSave.Text == "SAVE")
                        str = "SELECT * FROM tbIdentityType WHERE Id_type='" + txtIDtype.Text.Replace("'", "''") + "'";
                    else if (BtnSave.Text == "UPDATE")
                        str = "SELECT * FROM tbIdentityType WHERE Id_type='" + txtIDtype.Text.Replace("'", "''") + "'AND Id_Code !=" + txtcode.Text + "";
                    dr = mod.GetRecord(str);
                    if (dr.HasRows)
                    {
                        MessageBox.Show("Name Already Exist!!");
                        txtIDtype.Focus();
                    }
                    else
                    {
                        BtnSave.Enabled = false;
                        if (BtnSave.Text == "UPDATE")
                        {
                            dr = mod.GetSelectAllField(tableName, firstCol, txtcode.Text.ToString(), "");
                            while (dr.Read())
                            {
                                mod.compare(txtIDtype.Text, dr["Id_type"].ToString(), "Identity Master");
                            }
                        }
                        else
                        {
                            txtcode.Text = "" + mod.GetMaxNum(tableName, firstCol);
                        }
                        SqlCommand cmd = new SqlCommand("SP_INSERTIdentiType", con.connect());
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (BtnSave.Text == "SAVE")
                        {
                            cmd.Parameters.AddWithValue("@variable", "INSERT");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@variable", "UPDATE");
                        }
                        cmd.Parameters.AddWithValue("@code", txtcode.Text);
                        cmd.Parameters.AddWithValue("@name", txtIDtype.Text.Replace("'", "''"));
                        cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                        cmd.ExecuteNonQuery();
                        if (BtnSave.Text == "SAVE")
                        {
                            lblSucessMsg.Text = "Save Successfully !!!";
                        }
                        else
                        {
                            lblSucessMsg.Text = "Update Successfully !!!";
                            StrUpdate = "U";
                        }
                        //str = "SELECT Id_type,Id_Code FROM tbIdentityType WHERE deleted='N' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY Id_Code";
                        //    mod.fillgrid(str, DtgridIdentyType);
                        //fillGrid();
                        mod.DataGridBind(DtgridIdentyType, tableName, firstCol, secondCol, "N", "Identity Type", "Code");
                        txtsearch.Focus();
                        txtsearch.Text = "";
                        string txt = BtnSave.Text;
                        FillDetails();
                    }
                    int s = selectrow;
                    if (DtgridIdentyType.Rows.Count > 0)
                    {
                        DtgridIdentyType.CurrentCell = DtgridIdentyType[0, s];
                    }
                    StrUpdate = "";
                    BtnSave.Enabled = false;

                }
            }

            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void txtsearch_Enter(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.Yellow;
        }

        private void txtIDtype_Enter(object sender, EventArgs e)
        {
            txtIDtype.BackColor = Color.Yellow;
        }

        private void FrmIdentityType_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Control nextTab;
                if (e.KeyChar == (char)Keys.Enter)
                {
                    nextTab = ((Control)sender);
                    if (ActiveControl.Name == "txtsearch" && DtgridIdentyType.Rows.Count == 0 && e.KeyChar == (char)Keys.Enter)
                    {
                        txtsearch.Focus();
                    }
                    else
                    {
                        if (ActiveControl.Name == "txtsearch" && DtgridIdentyType.Rows.Count == 0)
                            nextTab = BtnAdd;
                        else
                            nextTab = GetNextControl(ActiveControl, true);

                        nextTab.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                string strMsg;
                strMsg = BtnDelete.Text;
                if (BtnDelete.Text == "DELETE" || BtnDelete.Text == "RECALL")
                {
                    i = mod.deleteButtonClick(tableName, firstCol, txtcode.Text,
                         txtsearch, BtnDelete.Text.Substring(0, 1));
                    if (i == 1)
                    {
                        FrmIdentityType_Load(sender, EventArgs.Empty);
                        if (strMsg == "DELETE")
                            lblSucessMsg.Text = "Deleted Successfully !!!";
                        else
                            lblSucessMsg.Text = "Recalled Successfully !!!";
                    }
                }

                else if (BtnDelete.Text == "RESET")
                {
                    Add();
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void ChkDeleted_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //mod.chkDeletedChange(ChkDeleted, firstCol, secondCol, tableName, DtgridIdentyType, txtsearch);
                mod.chkDeletedChange(ChkDeleted, firstCol, secondCol, tableName, Deleted, "N", DtgridIdentyType, txtsearch, "Identity Type", "Code");
                FillDetails();
                lblSucessMsg.Text = "";
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void DtgridIdentyType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                mod.gridKeyDown(e, BtnDelete, txtsearch, DtgridIdentyType, this);
                FillDetails();
                if (e.KeyCode == Keys.Enter && ChkDeleted.Checked == true && DtgridIdentyType.Rows.Count > 0)
                    BtnDelete.Focus();
                else if (e.KeyCode == Keys.Enter && ChkDeleted.Checked == true && DtgridIdentyType.Rows.Count == 0)
                    BtnExit.Focus();
                lblSucessMsg.Text = "";

            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                mod.txtSearchChange(ChkDeleted, firstCol, secondCol, tableName, DtgridIdentyType, txtsearch, "Identity Type", "Code");
                FillDetails();
                lblSucessMsg.Text = "";
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void txtsearch_KeyDown(object sender, KeyEventArgs e)
        {
            DtgridIdentyType_KeyDown(sender, e);
        }

        private void DtgridIdentyType_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            FillDetails();
            mod.gridDoubleClick(ChkDeleted, BtnDelete, txtIDtype);
            lblSucessMsg.Text = "";
            BtnSave.Enabled = true;
        }

        private void txtsearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && txtsearch.Text == "")
            {
                BtnAdd.Focus();
            }
            lblSucessMsg.Text = "";
        }

        private void txtsearch_Leave(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.White;
        }

        private void txtIDtype_Leave(object sender, EventArgs e)
        {
            txtIDtype.BackColor = Color.White;
        }

        private void btnLessID_Click(object sender, EventArgs e)
        {

            Models.Common.FormManagement.Instance.SelectRecordFromGrid(DtgridIdentyType, Models.Common.RecordMove.Previous, FillDetails);

        }

        private void btnGreaterID_Click(object sender, EventArgs e)
        {

            Models.Common.FormManagement.Instance.SelectRecordFromGrid(DtgridIdentyType, Models.Common.RecordMove.Next, FillDetails);

        }

        private void FrmIdentityType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true && e.KeyCode == Keys.S && BtnSave.Text == "SAVE")
            {
                BtnSave.PerformClick();
            }
            if (e.Control == true && e.KeyCode == Keys.U && BtnSave.Text == "UPDATE")
            {
                BtnSave.PerformClick();
            }
            if (e.Control == true && e.KeyCode == Keys.D && BtnDelete.Text == "DELETE" && BtnDelete.Enabled == true)
            {
                BtnDelete.PerformClick();
            }
            if (e.Control == true && e.KeyCode == Keys.R && BtnDelete.Text == "RESET")
            {
                BtnDelete.PerformClick();
            }
            if (e.Control == true && e.KeyCode == Keys.X)
            {
                BtnExit.PerformClick();
            }
            if (e.Control == true && e.KeyCode == Keys.L && BtnDelete.Text == "RECALL")
            {
                BtnDelete.PerformClick();
            }
        }

        private void DtgridIdentyType_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1 && e.RowIndex == -1)
                {
                    label1.Text = "Search by Code :";
                    Globalvariable.SearchChangeVariable = "SrchByCode";
                }
                else
                {
                    label1.Text = "Search by Name :";
                    Globalvariable.SearchChangeVariable = "SrchByName";
                }
                txtsearch.Focus();
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void FrmIdentityType_Shown(object sender, EventArgs e)
        {
            txtsearch.Focus();
        }


    }
}

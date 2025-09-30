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
using ASPL.CLASSES;
using System.IO;
using ASPL.STARTUP.FORMS;


namespace ASPL.LODGE.MASTERS
{
    public partial class FrmTallyMaster : Form
    {
        Design ObjDesign = new Design();
        public FrmTallyMaster()
        {
            InitializeComponent();
        }
        Module mod = new Module();
        Connection con = new Connection();
        int selectrow;
        string str, StrUpdate;
        string firstCol = "Tally_Code";
        string secondCol = "Tally_Name";
        string tableName = "tbTallyMaster";
        string Deleted = "Deleted";
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
        private void FrmTallyMaster_Load(object sender, EventArgs e)
        {
            try
            {
                ObjDesign.FormDesign(this, DtgridTallyMast);
               // mod.FormLoad(this, firstCol, secondCol, tableName, DtgridTallyMast, txtsearch, ChkDeleted);
                mod.FormLoad(this, tableName, firstCol, secondCol, Deleted, "N", "N", DtgridTallyMast, txtsearch, ChkDeleted, "Description", "Code");
                FillDetails();
                txtTallyCode.Enabled = false;
                mod.UserAccessibillityMaster("Tally Master", BtnAdd, BtnSave, BtnDelete);
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
                    mod.chkDeletedTrue(this, BtnAdd, BtnSave, BtnDelete, DtgridTallyMast, txtsearch);
                    txtsearch.Focus();
                }
                else
                {
                    mod.chkDeletedFalse("Tally Master",this, BtnAdd, BtnSave, BtnDelete, DtgridTallyMast, txtsearch);
                }
                if (DtgridTallyMast.Rows.Count > 0)
                {
                    int i;
                    if (StrUpdate == "U")
                    {
                        i = selectrow;
                    }
                    else
                    {
                        i = DtgridTallyMast.SelectedCells[0].RowIndex;
                    }
                    if (DtgridTallyMast.Rows[i].Cells[1].Value != null)
                    {
                        string ID = mod.Isnull(DtgridTallyMast.Rows[i].Cells[1].Value.ToString(), "");
                        //SqlDataReader dr = mod.GetRecord("SELECT * FROM " + tableName + " WHERE " +
                        //                     firstCol + " ='" + s + "' AND Branchcode='" + Globalvariable.bcode + "'");
                        SqlDataReader dr = mod.GetSelectAllField(tableName, firstCol, ID,"");
                        while (dr.Read())
                        {
                           txtTallyCode.Text = dr["Tally_Code"].ToString();
                           txtTallyCode.Enabled = false;
                           txtTallyName.Text = dr["Tally_Name"].ToString();
                        }
                    }
                }
                else
                {
                    mod.txtclear(this.Controls);
                    BtnDelete.Text = "RESET";
                }
              //  lblSucessMsg.Text = "";
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

        private void txtTallyName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                BtnSave.Focus();
            }
        }

        private void txtsearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && txtsearch.Text == "")
            {
                BtnAdd.Focus();
            }
            lblSucessMsg.Text = "";

        }

        private void txtTallyName_Enter(object sender, EventArgs e)
        {
            txtTallyName.BackColor = Color.Yellow;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Add();
        }
        public void Add()
        {
            try
            {
                mod.addBtnClick(this, txtTallyCode, tableName, firstCol, BtnSave, BtnDelete, txtTallyName);
                mod.UserAccessibillityMaster("Tally Master", BtnAdd, BtnSave, BtnDelete);
               // Max_ID();
                txtTallyCode.Text = "" + mod.GetMaxNum(tableName, firstCol);
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
                SqlCommand cmd = new SqlCommand("SELECT (ISNULL(MAX(Tally_Code),0)+1) FROM tbTallyMaster WHERE Branchcode='" + Globalvariable.bcode + "'", con.connect());
                cmd.ExecuteNonQuery();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                txtTallyCode.Text = "" + count;
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (DtgridTallyMast.Rows.Count > 0)
                {
                    selectrow = DtgridTallyMast.SelectedCells[0].RowIndex;
                }
                int i;
                if (txtTallyName.Text == "")
                {
                    txtTallyName.Focus();
                }
                else
                {
                    if (BtnSave.Text == "SAVE")
                        str = "SELECT * FROM " + tableName + " WHERE " + secondCol + "='" + txtTallyName.Text.Replace("'", "''") +
                            "' AND Branchcode='" + Globalvariable.bcode + "'";
                    else if (BtnSave.Text == "UPDATE")
                        str = "SELECT * FROM " + tableName + " WHERE " + secondCol + "='" + txtTallyName.Text.Replace("'", "''") +
                            "' AND Branchcode='" + Globalvariable.bcode + "' AND " + firstCol + " !=" + txtTallyCode.Text + "";
                    SqlDataReader dr = mod.GetRecord(str);
                    if (dr.HasRows)
                    {
                        MessageBox.Show("Name Already Exist!!");
                        txtTallyName.Focus();
                    }
                    else
                    {
                        BtnSave.Enabled = false;
                        
                            //str = "INSERT INTO tbTallyMaster(Tally_Code,Tally_Name,User_id,Deleted,Branchcode) VALUES(" + txtTallyCode.Text +
                            //         ",'" + txtTallyName.Text.Replace("'", "''") + "','" + Globalvariable.usercd +
                            //        "','N','" + Globalvariable.bcode + "' )";
                            // i = mod.SaveMaster(str);
                            SqlCommand cmd = new SqlCommand("SP_INSERTTallyMast", con.connect());
                            cmd.CommandType = CommandType.StoredProcedure;
                            if (BtnSave.Text == "SAVE")
                            {
                                txtTallyCode.Text = "" + mod.GetMaxNum(tableName, firstCol);
                            cmd.Parameters.AddWithValue("@variable", "INSERT");
                            }
                              else
                            {
                               cmd.Parameters.AddWithValue("@variable", "UPDATE");
                            }
                            cmd.Parameters.AddWithValue("@code", txtTallyCode.Text);
                            cmd.Parameters.AddWithValue("@name", txtTallyName.Text.Replace("'", "''"));
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
                       
                           // fillDisplayGrid();
                        mod.DataGridBind(DtgridTallyMast, tableName, firstCol, secondCol, "N", "Group Name", "Code");                    
                            txtsearch.Focus();
                            txtsearch.Text = "";
                            string txt = BtnSave.Text;
                            FillDetails();                       
                        int s = selectrow;
                        if (DtgridTallyMast.Rows.Count > 0)
                        {
                            DtgridTallyMast.CurrentCell = DtgridTallyMast[0, s];
                        }
                        StrUpdate = "";
                        BtnSave.Enabled = false;
                    }
                }
                }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }


        private void DtgridTallyMast_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                FillDetails();
                mod.gridDoubleClick(ChkDeleted, BtnDelete, txtTallyName);
                lblSucessMsg.Text = "";
                BtnSave.Enabled = true;

            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void DtgridTallyMast_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                mod.gridKeyDown(e, BtnDelete, txtsearch, DtgridTallyMast, this);
                FillDetails();
                if (e.KeyCode == Keys.Enter && ChkDeleted.Checked == true && DtgridTallyMast.Rows.Count > 0)
                    BtnDelete.Focus();
                else if (e.KeyCode == Keys.Enter && ChkDeleted.Checked == true && DtgridTallyMast.Rows.Count == 0)
                    BtnExit.Focus();
                lblSucessMsg.Text = "";

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
                //mod.chkDeletedChange(ChkDeleted, firstCol, secondCol, tableName, DtgridTallyMast, txtsearch);
                mod.chkDeletedChange(ChkDeleted, firstCol, secondCol, tableName, Deleted, "N", DtgridTallyMast, txtsearch, "Group Name", "Code");
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
            DtgridTallyMast_KeyDown(sender, e);
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                mod.txtSearchChange(ChkDeleted, firstCol, secondCol, tableName, DtgridTallyMast, txtsearch, "Group Name", "Code");
                FillDetails();
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
                    i = mod.deleteButtonClick(tableName, firstCol, txtTallyCode.Text,
                         txtsearch, BtnDelete.Text.Substring(0, 1));
                    if (i == 1)
                    {
                        FrmTallyMaster_Load(sender, EventArgs.Empty);
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

        private void FrmTallyMaster_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (ActiveControl.Name == "txtsearch" && DtgridTallyMast.Rows.Count == 0 && e.KeyChar == (char)Keys.Enter)
            {
                txtsearch.Focus();
            }
        }

        private void txtsearch_Leave(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.White;
        }

        private void txtTallyName_Leave(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.White;
        }

        private void FrmTallyMaster_KeyDown(object sender, KeyEventArgs e)
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

        private void DtgridTallyMast_CellClick(object sender, DataGridViewCellEventArgs e)
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

        private void btnLessID_Click(object sender, EventArgs e)
        {

            Models.Common.FormManagement.Instance.SelectRecordFromGrid(DtgridTallyMast, Models.Common.RecordMove.Previous, FillDetails);

        }

        private void btnGreaterID_Click(object sender, EventArgs e)
        {

            Models.Common.FormManagement.Instance.SelectRecordFromGrid(DtgridTallyMast, Models.Common.RecordMove.Next, FillDetails);

        }

        private void FrmTallyMaster_Shown(object sender, EventArgs e)
        {
            txtsearch.Focus();
        }
    }
}

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
    public partial class FrmPartyBank : Form
    {
        Design ObjDesign = new Design();
        public FrmPartyBank()
        {
            InitializeComponent();
        }

        Module mod = new Module();
        Connection con = new Connection();
        int underCd, selectrow;
        string firstCol = "bank_code";
        string secondCol = "bank_desc";
        string tableName = "tbPartyBank";
        string Deleted = "deleted";
        string str, StrUpdate;
        string strUnderCd;

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
        private void FrmPartyBank_Load(object sender, EventArgs e)
        {
            try
            {
               // mod.FormLoad(this, firstCol, secondCol, tableName, DtgridPartyBank, txtsearch, Chkshowdelet);
                //    txtsearch.Focus();
                //    fillDisplayGrid();
                ObjDesign.FormDesign(this, DtgridPartyBank);
                mod.FormLoad(this, tableName, firstCol, secondCol, Deleted, "N", "N", DtgridPartyBank, txtsearch, Chkshowdelet, "Description", "Code");
                FillDetails();
               txtbankcod.Enabled = false;
                mod.UserAccessibillityMaster("Party Bank", BtnAdd, BtnSave, BtnDelete);
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
                if (Chkshowdelet.Checked == true)
                {
                    mod.chkDeletedTrue(this, BtnAdd, BtnSave, BtnDelete, DtgridPartyBank, txtsearch);
                    txtsearch.Focus();
                }
                else
                {
                    mod.chkDeletedFalse("Party Bank Name",this, BtnAdd, BtnSave, BtnDelete, DtgridPartyBank, txtsearch);
                }

                if (DtgridPartyBank.Rows.Count > 0)
                {
                    int i;
                    if (StrUpdate == "U")
                    {
                        i = selectrow;
                    }
                    else
                    {
                        i = DtgridPartyBank.SelectedCells[0].RowIndex;
                    }
                    
                    if (DtgridPartyBank.Rows[i].Cells[1].Value != null)
                    {
                        string ID = mod.Isnull(DtgridPartyBank.Rows[i].Cells[1].Value.ToString(), "");
                       //SqlDataReader dr = mod.GetRecord("SELECT * FROM " + tableName + " WHERE " +
                       //                     firstCol + " ='" + s + "' AND Branchcode='" + Globalvariable.bcode + "'");
                        SqlDataReader dr = mod.GetSelectAllField(tableName, firstCol, ID,"");
                        while (dr.Read())
                        {
                            txtbankcod.Text = dr["bank_code"].ToString();
                            txtbankcod.Enabled = false;
                            txtbankname.Text = dr["bank_desc"].ToString();
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


        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            add();
        }

        public void add()
        {
            try
            {
                mod.addBtnClick(this, txtbankcod, tableName, firstCol, BtnSave, BtnDelete, txtbankname);
                mod.UserAccessibillityMaster("Party Bank", BtnAdd, BtnSave, BtnDelete);
               // max_value();
                txtbankcod.Text = "" + mod.GetMaxNum(tableName, firstCol);
                lblSucessMsg.Text = "";
                label1.Text = "Search by Name :";
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        public void max_value()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT (ISNULL(MAX(bank_code),0)+1) FROM tbPartyBank WHERE Branchcode='" + Globalvariable.bcode + "'", con.connect());
                cmd.ExecuteNonQuery();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
               txtbankcod.Text = "" + count;
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

        private void txtbankname_Enter(object sender, EventArgs e)
        {
             txtbankname.BackColor = Color.Yellow;
        }

        private void txtbankname_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {                 
            try
            {
                int i;
                if (DtgridPartyBank.Rows.Count > 0)
                {
                    selectrow = DtgridPartyBank.SelectedCells[0].RowIndex;
                }
                if (txtbankname.Text == "")
                {
                    txtbankname.Focus();
                }
                else
                {
                  
                    if (BtnSave.Text == "SAVE")
                        str = "SELECT * FROM " + tableName + " WHERE " + secondCol + "='" + txtbankname.Text.Replace("'", "''") +
                            "' AND Branchcode='" + Globalvariable.bcode + "'";
                    else if (BtnSave.Text == "UPDATE")
                        str = "SELECT * FROM " + tableName + " WHERE " + secondCol + "='" + txtbankname.Text.Replace("'", "''") +
                            "' AND Branchcode='" + Globalvariable.bcode + "' AND " + firstCol + " !=" + txtbankcod.Text + "";
                    SqlDataReader dr = mod.GetRecord(str);
                    if (dr.HasRows)
                    {
                        MessageBox.Show("Name Already Exist!!");
                        txtbankname.Focus();
                    }
                    else
                    {
                        BtnSave.Enabled = false;
                       
                            //str = "INSERT INTO tbPartyBank(bank_code,bank_desc,user_id,deleted,Branchcode) VALUES(" + txtbankcod.Text +
                            //         ",'" + txtbankname.Text.Replace("'", "''") + "','" + Globalvariable.usercd +
                            //        "','N','" + Globalvariable.bcode + "' )";
                            //i = mod.SaveMaster(str);
                            SqlCommand cmd = new SqlCommand("SP_INSERTPartyBank", con.connect());
                            cmd.CommandType = CommandType.StoredProcedure;
                            if (BtnSave.Text == "SAVE")
                            {
                                txtbankcod.Text = "" + mod.GetMaxNum(tableName, firstCol);
                            cmd.Parameters.AddWithValue("@variable", "INSERT");
                            }
                             else
                            {
                              cmd.Parameters.AddWithValue("@variable", "UPDATE");
                            }
                            cmd.Parameters.AddWithValue("@code", txtbankcod.Text);
                            cmd.Parameters.AddWithValue("@name", txtbankname.Text.Replace("'", "''"));
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
                            //fillDisplayGrid();
                            mod.DataGridBind(DtgridPartyBank, tableName, firstCol, secondCol, "N", "Name", "Code");                    
                            txtsearch.Focus();
                            txtsearch.Text = "";
                            string txt = BtnSave.Text;
                            FillDetails();
                            int s = selectrow;
                            if (DtgridPartyBank.Rows.Count > 0)
                            {
                                DtgridPartyBank.CurrentCell = DtgridPartyBank[0, s];
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

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                string strMsg;
                strMsg = BtnDelete.Text;
                if (BtnDelete.Text == "DELETE" || BtnDelete.Text == "RECALL")
                {
                    i = mod.deleteButtonClick(tableName, firstCol, txtbankcod.Text,
                         txtsearch, BtnDelete.Text.Substring(0, 1));
                    if (i == 1)
                    {
                        FrmPartyBank_Load(sender, EventArgs.Empty);
                        if (strMsg == "DELETE")
                     lblSucessMsg.Text = "Deleted Successfully !!!";
                        else
                            lblSucessMsg.Text = "Recalled Successfully !!!";
                    }
                }

                else if (BtnDelete.Text == "RESET")
                {
                    add();
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void Chkshowdelet_CheckedChanged(object sender, EventArgs e)
        {
            //mod.chkDeletedChange(Chkshowdelet, firstCol, secondCol, tableName, DtgridPartyBank, txtsearch);
            mod.chkDeletedChange(Chkshowdelet, firstCol, secondCol, tableName, Deleted, "N", DtgridPartyBank, txtsearch, "Name", "Code");
            FillDetails();
            lblSucessMsg.Text = "";
        }

        private void DtgridPartyBank_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                mod.gridKeyDown(e, BtnDelete, txtsearch, DtgridPartyBank, this);
                FillDetails();
                if (e.KeyCode == Keys.Enter && Chkshowdelet.Checked == true && DtgridPartyBank.Rows.Count > 0)
                    BtnDelete.Focus();
                else if (e.KeyCode == Keys.Enter && Chkshowdelet.Checked == true && DtgridPartyBank.Rows.Count == 0)
                    BtnExit.Focus();
                lblSucessMsg.Text = "";

            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void DtgridPartyBank_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            FillDetails();
            mod.gridDoubleClick(Chkshowdelet, BtnDelete, txtbankname);
            lblSucessMsg.Text = "";
            BtnSave.Enabled = true;

        }

        private void txtsearch_KeyDown(object sender, KeyEventArgs e)
        {
            DtgridPartyBank_KeyDown(sender, e);
        }

        private void txtsearch_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                mod.txtSearchChange(Chkshowdelet, firstCol, secondCol, tableName, DtgridPartyBank, txtsearch, "Bank Name", "Code");
                FillDetails();
                lblSucessMsg.Text = "";
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void FrmPartyBank_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Control nextTab;
                if (e.KeyChar == (char)Keys.Enter)
                {
                    nextTab = ((Control)sender);
                    if (ActiveControl.Name == "txtsearch" && DtgridPartyBank.Rows.Count == 0 && e.KeyChar == (char)Keys.Enter)
                    {
                        txtsearch.Focus();
                    }
                    else
                    {
                        if (ActiveControl.Name == "txtsearch" && DtgridPartyBank.Rows.Count == 0)
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

        private void txtsearch_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && txtsearch.Text == "")
            {
                BtnAdd.Focus();
            }
            lblSucessMsg.Text = "";
        }

        private void txtbankname_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtbankname.BackColor = Color.White;
                BtnSave.Focus();
            }
        }

        private void txtsearch_Leave(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.White;
        }

        private void txtbankname_Leave(object sender, EventArgs e)
        {
            txtbankname.BackColor = Color.White;
        }

        private void FrmPartyBank_KeyDown(object sender, KeyEventArgs e)
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

        private void btnLessID_Click(object sender, EventArgs e)
        {

            Models.Common.FormManagement.Instance.SelectRecordFromGrid(DtgridPartyBank, Models.Common.RecordMove.Previous, FillDetails);

        }

        private void btnGreaterID_Click(object sender, EventArgs e)
        {

            Models.Common.FormManagement.Instance.SelectRecordFromGrid(DtgridPartyBank, Models.Common.RecordMove.Next, FillDetails);

        }

        private void DtgridPartyBank_CellClick(object sender, DataGridViewCellEventArgs e)
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

        private void FrmPartyBank_Shown(object sender, EventArgs e)
        {
            txtsearch.Focus();
        }       
    }
}

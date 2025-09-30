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
    public partial class FrmPlan : Form
    {
        Design ObjDesign = new Design();
        public FrmPlan()
        {
            InitializeComponent();
        }

        public static int searchNo;
        Connection con = new Connection();
        Module mod = new Module();
        string firstCol = "Plan_Code";
        string secondCol = "Plan_Name";
        string tableName = "tbPlan";
        string Deleted = "Deleted";
        string str, StrUpdate;
        int selectrow;
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
        private void ChkDeleted_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //mod.chkDeletedChange(ChkDeleted, firstCol, secondCol, tableName, DtgridPlan, txtsearch);
                mod.chkDeletedChange(ChkDeleted, firstCol, secondCol, tableName, Deleted, "N", DtgridPlan, txtsearch, "Plan Name", "Code");
                FillDetails();
                lblSucessMsg.Text = "";
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }
        private void FrmPlan_Load(object sender, EventArgs e)
        {
            txtTariff.KeyPress += Models.Common.FormManagement.Instance.NumericKeyTextBox_KeyPress;
            LoadEvent();

        }
        public void LoadEvent()
        {
            try
            {
                ObjDesign.FormDesign(this, DtgridPlan);
               // mod.FormLoad(this, firstCol, secondCol, tableName, DtgridPlan, txtsearch, ChkDeleted);
                mod.FormLoad(this, tableName, firstCol, secondCol, Deleted, "N", "N", DtgridPlan, txtsearch, ChkDeleted, "Description", "Code");
                FillDetails();
                mod.UserAccessibillityMaster("Plan", BtnAdd, BtnSave, BtnDelete);
                BtnSave.Enabled = false;
                txtcode.Enabled = false;
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
                    mod.chkDeletedTrue(this, BtnAdd, BtnSave, BtnDelete, DtgridPlan, txtsearch);
                    txtsearch.Focus();
                }
                else
                {
                    mod.chkDeletedFalse("Plan",this, BtnAdd, BtnSave, BtnDelete, DtgridPlan, txtsearch);
                }
                if (DtgridPlan.Rows.Count > 0)
                {
                     int i;
                     if (StrUpdate == "U")
                     {
                         i = selectrow;
                     }
                     else
                     {
                          i = DtgridPlan.SelectedCells[0].RowIndex;
                     }
                    if (DtgridPlan.Rows[i].Cells[1].Value != null)
                    {
                        string ID = mod.Isnull(DtgridPlan.Rows[i].Cells[1].Value.ToString(), "");
                       // SqlDataReader dr = mod.GetRecord("SELECT * FROM tbPlan WHERE Plan_Code='" + s + "' AND Branchcode='" + Globalvariable.bcode + "'");
                        SqlDataReader dr = mod.GetSelectAllField(tableName, firstCol, ID,"");
                        while (dr.Read())
                        {
                            txtcode.Text = "";
                            txtcode.Text = dr["Plan_Code"].ToString();
                            txtcode.Enabled = false;
                            txtPlanName.Text = dr["Plan_Name"].ToString();
                            txtTariff.Text = mod.Isnull(dr["Tariff"].ToString(), "0");
                            txtDetail.Text = mod.Isnull(dr["Detail"].ToString(), "-");
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
        private void txtPlanName_Enter(object sender, EventArgs e)
        {
            txtPlanName.BackColor = Color.Yellow;
        }
        private void txtTariff_Enter(object sender, EventArgs e)
        {
            txtTariff.BackColor = Color.Yellow;
        }
        private void txtDetail_Enter(object sender, EventArgs e)
        {
            txtDetail.BackColor = Color.Yellow;
        }
        private void FrmPlan_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Control nextTab;
                if (e.KeyChar == (char)Keys.Enter)
                {
                    nextTab = ((Control)sender);
                    if (ActiveControl.Name == "txtsearch" && DtgridPlan.Rows.Count == 0 && e.KeyChar == (char)Keys.Enter)
                    {
                        txtsearch.Focus();
                    }
                    else
                    {
                        if (ActiveControl.Name == "txtsearch" && DtgridPlan.Rows.Count == 0)
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
        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                mod.txtSearchChange(ChkDeleted, firstCol, secondCol, tableName, DtgridPlan, txtsearch, "Plan Name", "Code");
                FillDetails();
                lblSucessMsg.Text = "";
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
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
        private void txtsearch_KeyDown(object sender, KeyEventArgs e)
        {
            DtgridPlan_KeyDown(sender, e);
        }
        private void DtgridPlan_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                mod.gridKeyDown(e, BtnDelete, txtsearch, DtgridPlan, this);
                FillDetails();
                if (e.KeyCode == Keys.Enter && ChkDeleted.Checked == true && DtgridPlan.Rows.Count > 0)
                    BtnDelete.Focus();
                else if (e.KeyCode == Keys.Enter && ChkDeleted.Checked == true && DtgridPlan.Rows.Count == 0)
                    BtnExit.Focus();
                else if (e.KeyCode == Keys.Enter && ChkDeleted.Checked == false && DtgridPlan.Rows.Count == 0)
                    BtnExit.Focus();
                lblSucessMsg.Text = "";

            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }
        private void DtgridPlan_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                FillDetails();
                mod.gridDoubleClick(ChkDeleted, BtnDelete, txtPlanName);
                lblSucessMsg.Text = "";
                BtnSave.Enabled = true;

            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }
        public void Add()
        {
            try
            {
                mod.addBtnClick(this, txtcode, tableName, firstCol, BtnSave, BtnDelete, txtPlanName);
                mod.UserAccessibillityMaster("Plan", BtnAdd, BtnSave, BtnDelete);
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
                SqlCommand cmd = new SqlCommand("SELECT (ISNULL(MAX(Plan_Code),0)+1) FROM tbPlan WHERE Branchcode='" + Globalvariable.bcode + "'", con.connect());
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
                        FrmPlan_Load(sender, EventArgs.Empty);
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
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (DtgridPlan.Rows.Count > 0)
                {
                    selectrow = DtgridPlan.SelectedCells[0].RowIndex;
                }
                int i;
             
                if (txtPlanName.Text == "")
                {
                    txtPlanName.Focus();
                }
               else if (txtTariff.Text == "")
                {
                    txtTariff.Focus();
                }
                else
                {
                    if (BtnSave.Text == "SAVE")
                        str = "SELECT * FROM " + tableName + " WHERE " + secondCol + "='" + txtPlanName.Text.Replace("'", "''") +
                            "' AND Branchcode='" + Globalvariable.bcode + "'";
                    else if (BtnSave.Text == "UPDATE")
                        str = "SELECT * FROM " + tableName + " WHERE " + secondCol + "='" + txtPlanName.Text.Replace("'", "''") +
                            "' AND Branchcode='" + Globalvariable.bcode + "' AND " + firstCol + " !=" + txtcode.Text + "";
                    SqlDataReader dr = mod.GetRecord(str);
                    if (dr.HasRows)
                    {
                        MessageBox.Show("Name Already Exist!!");
                        txtPlanName.Focus();
                    }
                    else
                    {
                        BtnSave.Enabled = false;
                        
                            //str = "INSERT INTO tbPlan(Plan_Code,Plan_Name,Tariff,Detail,Deleted,Branchcode) VALUES(" + txtcode.Text +
                            //         ",'" + txtPlanName.Text.Replace("'", "''") + "','" + txtTariff.Text+ "','" +mod.Isnull(txtDetail.Text.Replace("'", "''"),"") + "','N','" + Globalvariable.bcode + "' )";
                            // i = mod.SaveMaster(str);
                            SqlCommand cmd = new SqlCommand("SP_INSERTPLAN", con.connect());
                            cmd.CommandType = CommandType.StoredProcedure;
                            if (BtnSave.Text == "SAVE")
                            {
                                txtcode.Text = "" + mod.GetMaxNum(tableName, firstCol);
                            cmd.Parameters.AddWithValue("@variable", "INSERT");
                            }
                             else
                            {
                              cmd.Parameters.AddWithValue("@variable", "UPDATE");
                            }
                            cmd.Parameters.AddWithValue("@code", txtcode.Text);
                            cmd.Parameters.AddWithValue("@name", txtPlanName.Text.Replace("'", "''"));
                            cmd.Parameters.AddWithValue("@tariff", txtTariff.Text);
                            cmd.Parameters.AddWithValue("@detail", txtDetail.Text.Replace("'", "''"));
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
                        mod.DataGridBind(DtgridPlan, tableName, firstCol, secondCol, "N", "Plan Name", "Code");                    
                            txtsearch.Focus();
                            txtsearch.Text = "";
                            string txt = BtnSave.Text;
                            FillDetails();
                        int s = selectrow;
                        if (DtgridPlan.Rows.Count > 0)
                        {
                            DtgridPlan.CurrentCell = DtgridPlan[0, s];
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

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Add();
        }

        private void txtDetail_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == '.' || e.KeyChar == (char)Keys.Space);
            if (e.KeyChar == (char)Keys.Enter)
            {
               txtDetail.BackColor = Color.White;
                BtnSave.Focus();
            }
        }

        private void txtPlanName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == '.' || e.KeyChar == (char)Keys.Space);
        }

        //private void txtTariff_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == '.');
        //}

        private void txtsearch_Leave(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.White;
        }

        private void txtPlanName_Leave(object sender, EventArgs e)
        {
           txtPlanName.BackColor = Color.White;
        }

        private void txtTariff_Leave(object sender, EventArgs e)
        {
            txtTariff.BackColor = Color.White;
        }

        private void txtDetail_Leave(object sender, EventArgs e)
        {
           txtDetail.BackColor = Color.White;
        }

        private void FrmPlan_KeyDown(object sender, KeyEventArgs e)
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

        private void DtgridPlan_CellClick(object sender, DataGridViewCellEventArgs e)
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

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnLessID_Click(object sender, EventArgs e)
        {

            Models.Common.FormManagement.Instance.SelectRecordFromGrid(DtgridPlan, Models.Common.RecordMove.Previous, FillDetails);

        }

        private void btnGreaterID_Click(object sender, EventArgs e)
        {

            Models.Common.FormManagement.Instance.SelectRecordFromGrid(DtgridPlan, Models.Common.RecordMove.Next, FillDetails);

        }

        private void FrmPlan_Shown(object sender, EventArgs e)
        {
            txtsearch.Focus();
        }
    }
}

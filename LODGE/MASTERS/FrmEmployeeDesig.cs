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
    public partial class FrmEmployeeDesig : Form
    {
        Design ObjDesign = new Design();
        public FrmEmployeeDesig()
        {
            InitializeComponent();
        }
        Module mod = new Module();
        Connection con = new Connection();
        string str, StrUpdate;
        string firstCol = "Desig_Code";
        string secondCol = "Desig_Name";
        string tableName = "tbEmployeeDesignation";
        string Deleted= "Deleted";
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
        private void FrmEmployeeDesig_Load(object sender, EventArgs e)
        {
            load();
        }
        public void load()
        {
            try
            {
                ObjDesign.FormDesign(this, DtgridEmpDesig);
                mod.FormLoad(this, tableName, firstCol, secondCol, Deleted, "N", "N", DtgridEmpDesig, txtsearch, ChkDeleted, "Description", "Code");
                FillDetails();
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
                    mod.chkDeletedTrue(this, BtnAdd, BtnSave, BtnDelete, DtgridEmpDesig, txtsearch);
                    txtsearch.Focus();
                }
                else
                {
                    mod.chkDeletedFalse("Designation",this, BtnAdd, BtnSave, BtnDelete, DtgridEmpDesig, txtsearch);
                }

                if (DtgridEmpDesig.Rows.Count > 0)
                {
                    int i;
                    if (StrUpdate == "U")
                    {
                        i = selectrow;
                    }
                    else
                    {
                        i = DtgridEmpDesig.SelectedCells[0].RowIndex;
                    }
                    if (DtgridEmpDesig.Rows[i].Cells[1].Value != null)
                    {
                        string ID = mod.Isnull(DtgridEmpDesig.Rows[i].Cells[1].Value.ToString(), "");
                         SqlDataReader dr = mod.GetSelectAllField(tableName, firstCol, ID,"");
                        while (dr.Read())
                        {
                            txtDesgCode.Text = dr["Desig_Code"].ToString();
                            txtDesgCode.Enabled = false;
                           txtDesigName.Text = dr["Desig_Name"].ToString();
                           txtbasicsal.Text = dr["Salary"].ToString();
                        }
                    }
                    mod.UserAccessibillityMaster("Designation", BtnAdd, BtnSave, BtnDelete);
                    if (BtnDelete.Enabled == true)
                    {
                        str = "SELECT * FROM tbEmployeeDesignation WHERE Desig_Code ='"+txtDesgCode.Text+"' AND Desig_Code in (SELECT Desig_Id FROM tbEmployeeInformation)";
                        mod.CheckUse(str, BtnDelete);
                    }
                }
                else
                {
                    mod.txtclear(this.Controls);
                    BtnDelete.Text = "RESET";

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
        public void Add()
        {
            try
            {
                mod.addBtnClick(this, txtDesgCode, tableName, firstCol, BtnSave, BtnDelete, txtDesigName);
                mod.UserAccessibillityMaster("Designation", BtnAdd, BtnSave, BtnDelete);
                //  Max_ID();
                txtDesgCode.Text = "" + mod.GetMaxNum(tableName, firstCol);
                lblSucessMsg.Text = "";
                label1.Text = "Search by Name :";
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
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int i;
                SqlCommand cmd;
                if (DtgridEmpDesig.Rows.Count > 0)
                {
                    selectrow = DtgridEmpDesig.SelectedCells[0].RowIndex;
                }
                if (txtDesigName.Text == "")
                {
                    txtDesigName.Focus();
                }
                else
                {
                   
                    if (BtnSave.Text == "SAVE")
                        str = "SELECT * FROM " + tableName + " WHERE " + secondCol + "='" + txtDesigName.Text.Replace("'", "''") +
                            "' AND Branchcode='" + Globalvariable.bcode + "'";
                    else if (BtnSave.Text == "UPDATE")
                        str = "SELECT * FROM " + tableName + " WHERE " + secondCol + "='" + txtDesigName.Text.Replace("'", "''") +
                            "' AND Branchcode='" + Globalvariable.bcode + "' AND " + firstCol + " !=" + txtDesgCode.Text + "";
                    SqlDataReader dr = mod.GetRecord(str);
                    if (dr.HasRows)
                    {
                        MessageBox.Show("Name Already Exist!!");
                        txtDesigName.Focus();
                    }
                    else
                    {
                        BtnSave.Enabled = false;

                        if (BtnSave.Text == "UPDATE")
                        {
                            dr = mod.GetSelectAllField(tableName, firstCol, txtDesgCode.Text.ToString(), "");
                            while (dr.Read())
                            {
                                mod.compare(txtDesigName.Text, dr["Desig_Name"].ToString(), "Employee Designation");
                                mod.compare(txtbasicsal.Text, dr["Salary"].ToString(), "Employee Designation");                                
                            }
                        }
                            cmd = new SqlCommand("SP_INSERTEmpDesig", con.connect());
                            cmd.CommandType = CommandType.StoredProcedure;
                            if (BtnSave.Text == "SAVE")
                            {
                            txtDesgCode.Text = "" + mod.GetMaxNum(tableName, firstCol);
                            cmd.Parameters.AddWithValue("@variable", "INSERT");
                            }
                            else
                            {
                              cmd.Parameters.AddWithValue("@variable", "UPDATE");
                            }
                            cmd.Parameters.AddWithValue("@code", txtDesgCode.Text);
                            cmd.Parameters.AddWithValue("@name", txtDesigName.Text.Replace("'", "''"));
                            cmd.Parameters.AddWithValue("@salary", txtbasicsal.Text);
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
                        mod.DataGridBind(DtgridEmpDesig, tableName, firstCol, secondCol, "N", "Designation","Code");                    
                        txtsearch.Focus();
                        txtsearch.Text = "";
                        string txt = BtnSave.Text;
                        FillDetails();               
                        int s = selectrow;
                        if (DtgridEmpDesig.Rows.Count > 0)
                        {
                            DtgridEmpDesig.CurrentCell = DtgridEmpDesig[0, s];
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

        private void ChkDeleted_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                mod.chkDeletedChange(ChkDeleted, firstCol, secondCol, tableName, Deleted, "N", DtgridEmpDesig, txtsearch, "Designation", "Code");
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
            try
            {
                DtgridEmpDesig_KeyDown(sender, e);
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

        private void txtDesigName_Enter(object sender, EventArgs e)
        {
            txtDesigName.BackColor = Color.Yellow;
        }

        private void txtbasicsal_Enter(object sender, EventArgs e)
        {
            txtbasicsal.BackColor = Color.Yellow;
        }

        private void txtsearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            BtnAdd.Focus();
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
                   i = mod.deleteButtonClick(tableName, firstCol,txtDesgCode.Text,
                        txtsearch, BtnDelete.Text.Substring(0, 1));
                   if (i == 1)
                   {
                       FrmEmployeeDesig_Load(sender, EventArgs.Empty);
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

        private void FrmEmployeeDesig_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Control nextTab;
                if (e.KeyChar == (char)Keys.Enter)
                {
                    nextTab = ((Control)sender);
                    if (ActiveControl.Name == "txtsearch" && DtgridEmpDesig.Rows.Count == 0 && e.KeyChar == (char)Keys.Enter)
                    {
                        txtsearch.Focus();
                    }
                    else
                    {
                        if (ActiveControl.Name == "txtsearch" && DtgridEmpDesig.Rows.Count == 0)
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

        private void DtgridEmpDesig_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                mod.gridKeyDown(e, BtnDelete, txtsearch, DtgridEmpDesig, this);

                FillDetails();
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
                {
                    DtgridEmpDesig.Focus();
                }
                if (e.KeyCode == Keys.Enter && ChkDeleted.Checked == true && DtgridEmpDesig.Rows.Count > 0)
                    BtnDelete.Focus();
                else if (e.KeyCode == Keys.Enter && ChkDeleted.Checked == true && DtgridEmpDesig.Rows.Count == 0)
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
                mod.txtSearchChange(ChkDeleted, firstCol, secondCol, tableName, DtgridEmpDesig, txtsearch, "Group Name", "Group Code");
                FillDetails();
                lblSucessMsg.Text = "";
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

        private void txtbasicsal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
               txtbasicsal.BackColor = Color.White;
                BtnSave.Focus();
            }
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == (char)Keys.Back);
        }

        private void txtsearch_Leave(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.White;
        }

        private void txtDesigName_Leave(object sender, EventArgs e)
        {
           txtDesigName.BackColor = Color.White;
        }

        private void txtbasicsal_Leave(object sender, EventArgs e)
        {
            txtbasicsal.BackColor = Color.White;
        }

        private void FrmEmployeeDesig_KeyDown(object sender, KeyEventArgs e)
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

        private void DtgridEmpDesig_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            FillDetails();
            mod.gridDoubleClick(ChkDeleted, BtnDelete, txtDesigName);
            lblSucessMsg.Text = "";
            BtnSave.Enabled = true;

        }

        private void DtgridEmpDesig_CellClick_1(object sender, DataGridViewCellEventArgs e)
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

            Models.Common.FormManagement.Instance.SelectRecordFromGrid(DtgridEmpDesig, Models.Common.RecordMove.Previous, FillDetails);

        }

        private void btnGreaterID_Click(object sender, EventArgs e)
        {

            Models.Common.FormManagement.Instance.SelectRecordFromGrid(DtgridEmpDesig, Models.Common.RecordMove.Next, FillDetails);

        }

        private void FrmEmployeeDesig_Shown(object sender, EventArgs e)
        {
            txtsearch.Focus();
        }
    }
}

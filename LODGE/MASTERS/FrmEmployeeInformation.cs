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
    public partial class FrmEmployeeInformation : Form
    {
        Design ObjDesign = new Design();
        public FrmEmployeeInformation()
        {
            InitializeComponent();
        }
        private System.Drawing.Color m_tbcolorenter = System.Drawing.Color.Yellow;
        private System.Drawing.Color m_tbcolorleave = System.Drawing.Color.White;
        public static SqlDataReader drsrch;
        public static SqlDataAdapter dasrch;
        Module mod = new Module();
        Connection con = new Connection();
        SqlCommand command;
        DataSet ds = new DataSet();
        string str, strsrch, StrUpdate;
        string firstCol = "EmpNo";
        string secondCol = "EmpName";
        string tableName = "tbEmployeeInformation";
        string Deleted = "Deleted";
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
        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        public string searchChange(string searchStr)
        {
            try
            {
                if (Globalvariable.searchNo == 1)
                {
                    strsrch = "SELECT Desig_Name,Desig_Code FROM tbEmployeeDesignation WHERE deleted='N' OR Desig_Name LIKE '" +
                           searchStr + "%' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY Desig_Code";

                }
                else if (Globalvariable.searchNo == 2)
                {
                    strsrch = "SELECT Desig_Name,Desig_Code FROM tbEmployeeDesignation WHERE deleted='N' AND Desig_Name LIKE '" +
                           searchStr + "%' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY Desig_Code";
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
            return strsrch;
        }

        private void FrmEmployeeInformation_Load(object sender, EventArgs e)
        {
            load();
        }

        public void load()
        {
            try
            {
                ObjDesign.FormDesign(this, DtgridEmpInfo);
                mod.FormLoad(this, tableName, firstCol, secondCol, Deleted, "N", "N", DtgridEmpInfo, txtsearch, ChkDeleted, "Description", "Code");
                FillDetails();
                mod.UserAccessibillityMaster("Employee Information", BtnAdd, BtnSave, BtnDelete);
                txtEmpNo.Enabled = false;
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
                    mod.chkDeletedTrue(this, BtnAdd, BtnSave, BtnDelete, DtgridEmpInfo, txtsearch);
                    txtsearch.Focus();
                }
                else
                {
                    mod.chkDeletedFalse("Employee Information",this, BtnAdd, BtnSave, BtnDelete, DtgridEmpInfo, txtsearch);
                }

                if (DtgridEmpInfo.Rows.Count > 0)
                {
                     int i;
                     if (StrUpdate == "U")
                     {
                         i = selectrow;
                     }
                     else
                     {
                         i = DtgridEmpInfo.SelectedCells[0].RowIndex;
                     }
                    if (DtgridEmpInfo.Rows[i].Cells[1].Value != null)
                    {
                        string ID = mod.Isnull(DtgridEmpInfo.Rows[i].Cells[1].Value.ToString(), "");
                        SqlDataReader dr = mod.GetSelectAllField(tableName, firstCol, ID,"");
                        while (dr.Read())
                        {
                            txtEmpNo.Text = dr["EmpNo"].ToString();
                            txtEmpNo.Enabled = false;
                            txtEmpName.Text = dr["EmpName"].ToString();
                            txtAddress1.Text = mod.Isnull(dr["Add1"].ToString(),"-");
                            txtAddress2.Text = mod.Isnull(dr["Add2"].ToString(),"-");
                            txtAddress3.Text = mod.Isnull(dr["Add3"].ToString(), "-");
                            txtDesigID.Text = mod.Isnull(dr["Desig_Id"].ToString(), "0");
                            if (txtDesigID.Text != "0")
                            {
                                fill_EmpDesignation();
                            }
                            else if (txtDesigID.Text == "0")
                            {
                                lblEmployeeDesignation.Text = "";
                            }
                            txtDepartment.Text = mod.Isnull(dr["Department"].ToString(), "-");
                            txtESIno.Text = mod.Isnull(dr["ESI_No"].ToString(), "0");
                            txtPFno.Text = mod.Isnull(dr["PF_No"].ToString(), "0");
                           dtpDateOfJoining.Value = Convert.ToDateTime(dr["DOJ"].ToString());
                           dtpDateofLeaving.Value = Convert.ToDateTime(dr["DOL"].ToString());
                           txtSikLevGiv.Text = mod.Isnull(dr["SL_Total"].ToString(), "0");
                           txtSickLevBalance.Text = mod.Isnull(dr["SL_Bal"].ToString(), "0");
                          txtCasualLevGiv.Text = mod.Isnull(dr["CL_Total"].ToString(), "0");
                          txtCasuallevBalan.Text = mod.Isnull(dr["CL_Bal"].ToString(), "0");
                          txtSpeciallevGiv.Text = mod.Isnull(dr["SPL_Total"].ToString(), "0");
                          txtSpecialLevBal.Text = mod.Isnull(dr["SPL_Bal"].ToString(), "0");
                          txtContactPerson.Text = mod.Isnull(dr["Con_PersonNo"].ToString(), "");
                         txtEmergencyPhon.Text = mod.Isnull(dr["EmerPh_No"].ToString(), "");
                         txtResiPhNo.Text = mod.Isnull(dr["ResiPh_No"].ToString(), "");
                         txtAdvanceTaken.Text = mod.Isnull(dr["Advance_Tot"].ToString(), "0");
                         txtTaegetGiven.Text = mod.Isnull(dr["Target"].ToString(), "0");

                        }
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
            add();
        }
        public void add()
        {
            try
            {
                mod.addBtnClick(this, txtEmpNo, tableName, firstCol, BtnSave, BtnDelete, txtEmpName);
                mod.UserAccessibillityMaster("Employee Information", BtnAdd, BtnSave, BtnDelete);
                txtEmpNo.Text = "" + mod.GetMaxNum(tableName, firstCol);
                lblSucessMsg.Text = "";
                label1.Text = "Search by Name :";
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

        private void txtEmpName_Enter(object sender, EventArgs e)
        {
        
            txtEmpName.BackColor = Color.Yellow;
        }

        private void txtAddress1_Enter(object sender, EventArgs e)
        {
         
            txtAddress1.BackColor = Color.Yellow;
        }

        private void txtAddress2_Enter(object sender, EventArgs e)
        {
         
            txtAddress2.BackColor = Color.Yellow;
        }

        private void txtAddress3_Enter(object sender, EventArgs e)
        {
            txtAddress3.BackColor = Color.Yellow;
        }

        private void txtDesigID_Enter(object sender, EventArgs e)
        {
      
            txtDesigID.BackColor = Color.Yellow;
        }

        private void txtDepartment_Enter(object sender, EventArgs e)
        {
       
            txtDepartment.BackColor = Color.Yellow;
        }

        private void txtPFno_Enter(object sender, EventArgs e)
        {
          
            txtPFno.BackColor = Color.Yellow;
        }

        private void txtSikLevGiv_Enter(object sender, EventArgs e)
        {
            
            txtSikLevGiv.BackColor = Color.Yellow;
        }

        private void txtSickLevBalance_Enter(object sender, EventArgs e)
        {
         
            txtSickLevBalance.BackColor = Color.Yellow;
        }

        private void txtCasualLevGiv_Enter(object sender, EventArgs e)
        {
          
            txtCasualLevGiv.BackColor = Color.Yellow;
        }

        private void txtCasuallevBalan_Enter(object sender, EventArgs e)
        {
          
            txtCasuallevBalan.BackColor = Color.Yellow;
        }

        private void txtSpeciallevGiv_Enter(object sender, EventArgs e)
        {
            
            txtSpeciallevGiv.BackColor = Color.Yellow;
        }

        private void txtSpecialLevBal_Enter(object sender, EventArgs e)
        {
         
            txtSpecialLevBal.BackColor = Color.Yellow;
        }

        private void txtEmergencyPhon_Enter(object sender, EventArgs e)
        {
            
            txtEmergencyPhon.BackColor = Color.Yellow;
        }

        private void txtContactPerson_Enter(object sender, EventArgs e)
        {
           
            txtContactPerson.BackColor = Color.Yellow;
        }

        private void txtResiPhNo_Enter(object sender, EventArgs e)
        {
         
            txtResiPhNo.BackColor = Color.Yellow;
        }

        private void txtAdvanceTaken_Enter(object sender, EventArgs e)
        {
           
            txtAdvanceTaken.BackColor = Color.Yellow;
        }

        private void txtTaegetGiven_Enter(object sender, EventArgs e)
        {
            
            txtTaegetGiven.BackColor = Color.Yellow;
        }

        private void FrmEmployeeInformation_KeyPress(object sender, KeyPressEventArgs e)
        {
             try
            {
                Control nextTab;
                if (e.KeyChar == (char)Keys.Enter)
                {
                    nextTab = ((Control)sender);
                    if (ActiveControl.Name == "txtsearch" && DtgridEmpInfo.Rows.Count == 0 && e.KeyChar == (char)Keys.Enter)
                    {
                        txtsearch.Focus();
                    }
                    else
                    {
                        if (ActiveControl.Name == "txtsearch" && DtgridEmpInfo.Rows.Count == 0)
                        {
                            nextTab = BtnAdd;
                        }
                        else if (ActiveControl.Name == "txtDesigID")
                        {
                            if (lblEmployeeDesignation.Text == "" && txtDesigID.Text != "0")
                            {
                                txtDesigID.Focus();
                            }
                            else
                            {
                                nextTab = GetNextControl(ActiveControl, true);
                            }
                        }
                        else
                        {
                            if (BtnDelete.Text != "RECALL")
                            {
                                nextTab = GetNextControl(ActiveControl, true);
                            }
                        }

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

        private void txtESIno_Enter(object sender, EventArgs e)
        {
           
            txtESIno.BackColor = Color.Yellow;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            save();
        }
        public void save()
        {
            try
             {
                 if (DtgridEmpInfo.Rows.Count > 0)
                 {
                     selectrow = DtgridEmpInfo.SelectedCells[0].RowIndex;
                 }
                 if (txtEmpName.Text == "")
                 {
                     txtEmpName.Focus();
                 
                 }
                 else if (txtAddress1.Text == "")
                 {
                     txtAddress1.Focus();
                 }                 
                 else
                 {
                        BtnSave.Enabled = false;
                        if (BtnSave.Text == "UPDATE")
                        {
                            drsrch = mod.GetSelectAllField(tableName, firstCol, txtEmpNo.Text.ToString(), "");
                            while (drsrch.Read())
                            {
                                mod.compare(txtEmpName.Text, drsrch["EmpName"].ToString(), "Employee Information");
                                mod.compare(string.Concat(txtAddress1.Text,txtAddress2.Text,txtAddress3.Text), string.Concat(drsrch["Add1"].ToString(), drsrch["Add2"].ToString(), drsrch["Add3"].ToString()), "Employee Information");
                                mod.compare(txtDepartment.Text, drsrch["Department"].ToString(), "Employee Information");
                                mod.compare(txtESIno.Text, drsrch["ESI_No"].ToString(), "Employee Information");
                                mod.compare(txtPFno.Text, drsrch["PF_No"].ToString(), "Employee Information");
                                mod.compare(dtpDateOfJoining.Value.ToShortDateString(), Convert.ToString(drsrch["DOJ"].ToString()), "Employee Information");
                                mod.compare(dtpDateofLeaving.Value.ToShortDateString(), Convert.ToString(drsrch["DOL"].ToString()), "Employee Information");
                                mod.compare(txtPFno.Text, drsrch["PF_No"].ToString(), "Employee Information");
                            }
                        }
                         command = new SqlCommand("SP_INSERTEmpInfo", con.connect());
                         command.CommandType = CommandType.StoredProcedure;
                         if (BtnSave.Text == "SAVE")
                         {
                             txtEmpNo.Text = "" + mod.GetMaxNum(tableName, firstCol);
                         command.Parameters.AddWithValue("@variable", "INSERT");
                         }
                        else
                         {
                            command.Parameters.AddWithValue("@variable", "UPDATE");
                         }
                         command.Parameters.AddWithValue("@code", txtEmpNo.Text);
                         command.Parameters.AddWithValue("@name", txtEmpName.Text.Replace("'", "''"));
                         command.Parameters.AddWithValue("@add1", mod.Isnull(txtAddress1.Text.Replace("'", "''"), "-"));
                         command.Parameters.AddWithValue("@add2", mod.Isnull(txtAddress2.Text.Replace("'", "''"), "-"));
                         command.Parameters.AddWithValue("@add3", mod.Isnull(txtAddress3.Text.Replace("'", "''"), "-"));
                         command.Parameters.AddWithValue("@desigID", mod.Isnull(txtDesigID.Text, "0"));
                         command.Parameters.AddWithValue("@department", mod.Isnull(txtDepartment.Text.Replace("'", "''"), "-"));
                         command.Parameters.AddWithValue("@EsiNo", mod.Isnull(txtESIno.Text, "0"));
                         command.Parameters.AddWithValue("@PfNo", mod.Isnull(txtPFno.Text, "0"));
                         command.Parameters.AddWithValue("@joindate", dtpDateOfJoining.Value.ToShortDateString());
                         command.Parameters.AddWithValue("@dateofleaving", dtpDateofLeaving.Value.ToShortDateString());                     
                         command.Parameters.AddWithValue("@sickleave", mod.Isnull(txtSikLevGiv.Text, "0"));
                         command.Parameters.AddWithValue("@sickbalance", mod.Isnull(txtSickLevBalance.Text, "0"));
                         command.Parameters.AddWithValue("@casualeave", mod.Isnull(txtCasualLevGiv.Text, "0"));
                         command.Parameters.AddWithValue("@casualbalance", mod.Isnull(txtCasuallevBalan.Text, "0"));
                         command.Parameters.AddWithValue("@specleave", mod.Isnull(txtSpeciallevGiv.Text, "0"));
                         command.Parameters.AddWithValue("@speclbalance", mod.Isnull(txtSpecialLevBal.Text, "0"));
                         command.Parameters.AddWithValue("@contactperson", mod.Isnull(txtContactPerson.Text, "0"));
                         command.Parameters.AddWithValue("@emergenPhon", mod.Isnull(txtEmergencyPhon.Text, "0"));
                         command.Parameters.AddWithValue("@RegistPhon", mod.Isnull(txtResiPhNo.Text, "0"));
                         command.Parameters.AddWithValue("@advancetaken", mod.Isnull(txtAdvanceTaken.Text, "0"));
                         command.Parameters.AddWithValue("@targetgiven", mod.Isnull(txtTaegetGiven.Text, "0"));
                         command.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                         command.ExecuteNonQuery();
                         if (BtnSave.Text == "SAVE")
                         {
                         lblSucessMsg.Text = "Save Successfully !!!";
                          }
                         else
                         {
                         lblSucessMsg.Text = "Update Successfully !!!";
                         StrUpdate = "U";
                         }                                                                                  
                         mod.DataGridBind(DtgridEmpInfo, tableName, firstCol, secondCol, "N", "Employee Name", "Code");                    
                         txtsearch.Focus();
                         txtsearch.Text = "";
                         string txt = BtnSave.Text;
                         FillDetails();
                         int s = selectrow;
                         if (DtgridEmpInfo.Rows.Count > 0)
                         {
                             DtgridEmpInfo.CurrentCell = DtgridEmpInfo[0, s];
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

       


        private void txtDesigID_KeyDown(object sender, KeyEventArgs e)
        {
            var returnForm = Models.Common.FormManagement.Instance.SearchFormOnKeyDown(ref txtDesigID, e, "Common_EmployeeDesignation", true);
            if (returnForm == null)
            {
                return;
            }
            txtDesigID.Text = returnForm.codeselected;
            lblEmployeeDesignation.Text = returnForm.descselected;
            return;

            //try
            //{
            //    if (txtDesigID.Text == "" && e.KeyCode == Keys.Enter)
            //    {
            //       Globalvariable.searchNo = 1;
            //        KeysConverter kc = new KeysConverter();
            //        string key = kc.ConvertToString(e.KeyCode);
            //        FrmSearch srch = new FrmSearch();
            //        dasrch = mod.GetselectQuery("tbEmployeeDesignation", "Desig_Code", "Desig_Name", "N", "N");
            //        srch.val = key;
            //        srch.fillbackgridSrch(dasrch, "FrmEmployeeInformation");
            //        ds.Clear();
            //        dasrch.Fill(ds);
            //        if (ds.Tables[0].Rows.Count > 0)
            //            srch.ShowDialog();
            //       txtDesigID.Text = srch.codeselected;
            //        if (srch.codeselected == null)
            //        {
            //            txtDesigID.Focus();
            //        }
            //        else
            //        {
            //            fill_EmpDesignation();                       
            //        }
            //        Globalvariable.searchNo = 0;
            //        if (lblEmployeeDesignation.Text != "")
            //        {
            //            txtDesigID.BackColor = Color.White;
            //            txtDepartment.Focus();
            //        }
            //        else
            //        {
            //            txtDesigID.Focus();
            //        }
            //    }
            //    else if (e.KeyCode == Keys.Enter && txtDesigID.Text != "" && txtDesigID.Text!="0")
            //    {
            //        fill_EmpDesignation();
            //    }

            //    else if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            //    {
            //        txtDesigID.Text = "";
            //       lblEmployeeDesignation.Text = "";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    string str = "Message:" + ex.Message;
            //    MessageBox.Show(str, "Error Message");
            //}
        }

        private void fill_EmpDesignation()
        {
            try
            {
                if (txtDesigID.Text.Trim().Equals(""))
                {
                    lblEmployeeDesignation.Text = "";
                }
                else
                {
                    SqlDataReader dr = mod.GetRecord("SELECT Desig_Name FROM tbEmployeeDesignation WHERE deleted='N' AND Branchcode='" + Globalvariable.bcode +
                            "' AND Desig_Code='" + txtDesigID.Text + "'");
                    if (dr.HasRows)
                    {
                        dr.Read();
                        lblEmployeeDesignation.Text = dr["Desig_Name"].ToString();
                    }
                    else
                    {
                        txtDesigID.Text = "";
                        lblEmployeeDesignation.Text = "";
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
                    i = mod.deleteButtonClick(tableName, firstCol, txtEmpNo.Text,
                         txtsearch, BtnDelete.Text.Substring(0, 1));
                    if (i == 1)
                    {
                        FrmEmployeeInformation_Load(sender, EventArgs.Empty);
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

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                mod.txtSearchChange(ChkDeleted, firstCol, secondCol, tableName, DtgridEmpInfo, txtsearch, "Employee Name", "Code");
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
            try
            {
               DtgridEmpInfo_KeyDown(sender, e);
               lblSucessMsg.Text = "";

            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void DtgridEmpInfo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                mod.gridKeyDown(e, BtnDelete, txtsearch, DtgridEmpInfo, this);
                lblSucessMsg.Text = "";
                FillDetails();
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
                {
                    DtgridEmpInfo.Focus();
                }
                if (e.KeyCode == Keys.Enter && ChkDeleted.Checked == true && DtgridEmpInfo.Rows.Count > 0)
                    BtnDelete.Focus();
                else if (e.KeyCode == Keys.Enter && ChkDeleted.Checked == true && DtgridEmpInfo.Rows.Count == 0)
                    BtnExit.Focus();
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void DtgridEmpInfo_CellClick(object sender, DataGridViewCellEventArgs e)
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

        private void DtgridEmpInfo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                FillDetails();
                mod.gridDoubleClick(ChkDeleted, BtnDelete, txtEmpName);
                if (ChkDeleted.Checked == true)
                    BtnDelete.Focus();
                else
                    txtEmpName.Focus();
                lblSucessMsg.Text = "";
                BtnSave.Enabled = true;

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
                mod.chkDeletedChange(ChkDeleted, firstCol, secondCol, tableName, Deleted, "N", DtgridEmpInfo, txtsearch, "Customer Name", "Code");
                FillDetails();
                lblSucessMsg.Text = "";
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void txtTaegetGiven_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == (char)Keys.Back);
            if (e.KeyChar == (char)Keys.Enter)
            {
               txtTaegetGiven.BackColor = Color.White;
                BtnSave.Focus();
            }

        }

        private void txtEmpName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space || e.KeyChar == '.');
        }

        private void txtDesigID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void txtSikLevGiv_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == (char)Keys.Back);
        }

        private void txtSickLevBalance_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == (char)Keys.Back);
        }

        private void txtCasualLevGiv_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == (char)Keys.Back);
        }

        private void txtCasuallevBalan_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == (char)Keys.Back);
        }

        private void txtSpeciallevGiv_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == (char)Keys.Back);
        }

        private void txtSpecialLevBal_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == (char)Keys.Back);
        }

        private void txtContactPerson_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space || e.KeyChar == '.');
        }

        private void txtEmergencyPhon_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void txtResiPhNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void txtAdvanceTaken_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == (char)Keys.Back);
        }

        private void txtsearch_Leave(object sender, EventArgs e)
        {
            txtsearch.BackColor = m_tbcolorleave;
        }

        private void txtEmpName_Leave(object sender, EventArgs e)
        {
            txtEmpName.BackColor = m_tbcolorleave;
        }

        private void txtAddress1_Leave(object sender, EventArgs e)
        {
            txtAddress1.BackColor = m_tbcolorleave;
        }

        private void txtAddress2_Leave(object sender, EventArgs e)
        {
            txtAddress2.BackColor = m_tbcolorleave;
        }

        private void txtAddress3_Leave(object sender, EventArgs e)
        {
            txtAddress3.BackColor = m_tbcolorleave;
        }

        private void txtDesigID_Leave(object sender, EventArgs e)
        {
            txtDesigID.BackColor = m_tbcolorleave;
        }

        private void txtDepartment_Leave(object sender, EventArgs e)
        {
            txtDepartment.BackColor = m_tbcolorleave;
        }

        private void txtESIno_Leave(object sender, EventArgs e)
        {
            txtESIno.BackColor = m_tbcolorleave;
        }

        private void txtPFno_Leave(object sender, EventArgs e)
        {
            txtPFno.BackColor = m_tbcolorleave;
        }

        private void dtpDateOfJoining_Leave(object sender, EventArgs e)
        {
            dtpDateOfJoining.BackColor = m_tbcolorleave;
        }

        private void dtpDateofLeaving_Leave(object sender, EventArgs e)
        {
            dtpDateofLeaving.BackColor = m_tbcolorleave;
        }

        private void txtSikLevGiv_Leave(object sender, EventArgs e)
        {
            txtSikLevGiv.BackColor = m_tbcolorleave;
        }

        private void txtSickLevBalance_Leave(object sender, EventArgs e)
        {
            txtSickLevBalance.BackColor = m_tbcolorleave;
        }

        private void txtCasualLevGiv_Leave(object sender, EventArgs e)
        {
            txtCasualLevGiv.BackColor = m_tbcolorleave;
        }

        private void txtCasuallevBalan_Leave(object sender, EventArgs e)
        {
            txtCasuallevBalan.BackColor = m_tbcolorleave;
        }

        private void txtSpeciallevGiv_Leave(object sender, EventArgs e)
        {
            txtSpeciallevGiv.BackColor = m_tbcolorleave;
        }

        private void txtSpecialLevBal_Leave(object sender, EventArgs e)
        {
            txtSpecialLevBal.BackColor = m_tbcolorleave;
        }

        private void txtContactPerson_Leave(object sender, EventArgs e)
        {
            txtContactPerson.BackColor = m_tbcolorleave;
        }

        private void txtEmergencyPhon_Leave(object sender, EventArgs e)
        {
            txtEmergencyPhon.BackColor = m_tbcolorleave;
        }

        private void txtResiPhNo_Leave(object sender, EventArgs e)
        {
            txtResiPhNo.BackColor = m_tbcolorleave;
        }

        private void txtAdvanceTaken_Leave(object sender, EventArgs e)
        {
            txtAdvanceTaken.BackColor = m_tbcolorleave;
        }

        private void txtTaegetGiven_Leave(object sender, EventArgs e)
        {
            txtTaegetGiven.BackColor = m_tbcolorleave;
        }

        private void btnLessID_Click(object sender, EventArgs e)
        {

            Models.Common.FormManagement.Instance.SelectRecordFromGrid(DtgridEmpInfo, Models.Common.RecordMove.Previous, FillDetails);

        }

        private void btnGreaterID_Click(object sender, EventArgs e)
        {

            Models.Common.FormManagement.Instance.SelectRecordFromGrid(DtgridEmpInfo, Models.Common.RecordMove.Next, FillDetails);

        }

        private void FrmEmployeeInformation_KeyDown(object sender, KeyEventArgs e)
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

        private void FrmEmployeeInformation_Shown(object sender, EventArgs e)
        {
            txtsearch.Focus();
        }
    }
}

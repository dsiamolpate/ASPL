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

namespace ASPL.LODGE.TRANSCTION
{
    public partial class FrmReceipt : Form
    {
        Design ObjDesign=new Design();
        public FrmReceipt()
        {
            InitializeComponent();
        }
        string voch_dt,fillCDO="";
        int detail_id = 0, grdchange, escGrid, m, mnth, flag = 0;
        string firstCol = "Voucher_No";
        string tableName = "tbTransReceipt";
        public string m_bkcode;
        SqlDataReader srchdr;
        SqlDataAdapter srchda;
        DataSet ds = new DataSet();
        Connection con = new Connection();
        SqlCommand cmd;
        Module mod = new Module();
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

        private void fillPartyBank()
        {
            try
            {
                if (txtPartyBank.Text.Trim().Equals(""))
                {
                    txtBankCode.Text = "";
                }
                else
                {
                    SqlDataReader dr = mod.GetRecord("SELECT bank_desc,bank_code FROM tbPartyBank WHERE deleted='N' AND Branchcode='" + Globalvariable.bcode +
                        "' AND bank_code='" + txtBankCode.Text + "'");
                    if (dr.HasRows)
                    {
                        dr.Read();
                        txtPartyBank.Text = dr["bank_desc"].ToString();
                        txtBankCode.Text = dr["bank_code"].ToString();
                    }
                    else
                    {
                        txtBankCode.Text = "";
                        txtPartyBank.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void fillBankBranch()
        {
            try
            {
                if (txtBranch.Text.Trim().Equals(""))
                {
                    txtBranchcode.Text = "";
                }
                else
                {
                    SqlDataReader dr = mod.GetRecord("SELECT Bank_Location,Bank_Code FROM tbBankLocation WHERE deleted='N' AND Branchcode='" + Globalvariable.bcode +
                        "' AND Bank_Code='" + txtBranchcode.Text + "'");
                    if (dr.HasRows)
                    {
                        dr.Read();
                        txtBranch.Text = dr["Bank_Location"].ToString();
                        txtBranchcode.Text = dr["Bank_Code"].ToString();
                    }
                    else
                    {
                        txtBranchcode.Text = "";
                        txtBranch.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void fillBookCode()
        {
            try
            {
                if (txtBookCode.Text.Trim().Equals(""))
                {
                    txtBookCodeNM.Text = "";
                }
                else
                {
                    SqlDataReader dr = mod.GetRecord("SELECT book_description FROM tbBook WHERE deleted='N' AND Branchcode='" + Globalvariable.bcode +
                        "' AND book_code='" + txtBookCode.Text + "'");
                    if (dr.HasRows)
                    {
                        dr.Read();
                        txtBookCodeNM.Text = dr["book_description"].ToString();
                        //   txtSingleRate.Focus();
                    }
                    else
                    {
                        txtBookCode.Text = "";
                        txtBookCodeNM.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        public void FillCDO()
        {
            cmbCreditDebit.Items.Clear();
            cmbCreditDebit.Items.Insert(0, "CREDITOR");
            cmbCreditDebit.Items.Insert(1, "DEBITOR");
            cmbCreditDebit.Items.Insert(2, "OTHER");
            if (detail_id != 1)
            {
                cmbCreditDebit.SelectedIndex = 0;
            }
        }

        private void FrmReceipt_KeyDown(object sender, KeyEventArgs e)
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

        private void FrmReceipt_Load(object sender, EventArgs e)
        {
            ObjDesign.FormDesign(this, dtgridReceipt);
            ObjDesign.FormDesign(this, dtgridGLcode);
            flag = 1;
            txtsearch.Focus();
            tabReceipt.SelectedTab = tabList;
            FillGrid("tbTransReceipt");
            txtBookCodeNM.Enabled = false;
            txtVoucherNo.Enabled = false;
            mod.UserAccessibillityMaster("Receipt", BtnAdd, BtnSave, BtnDelete);
            Globalvariable.SearchChangeVariable = "SrchByName";
        }

        private void txtBookCode_Enter(object sender, EventArgs e)
        {
            txtBookCode.BackColor = Color.Yellow;
        }

        private void txtAmount_Enter(object sender, EventArgs e)
        {
            txtAmount.BackColor = Color.Yellow;
        }

        private void txtBookCode_Leave(object sender, EventArgs e)
        {
            txtBookCode.BackColor = Color.White;
        }

        private void txtAmount_Leave(object sender, EventArgs e)
        {
            txtAmount.BackColor = Color.White;
        }

        private void txtCheckNortgs_Enter(object sender, EventArgs e)
        {
            txtCheckNo.BackColor = Color.Yellow;
        }

        private void txtCheckNortgs_Leave(object sender, EventArgs e)
        {
            txtCheckNo.BackColor = Color.White;
        }

        private void txtPartyBank_Enter(object sender, EventArgs e)
        {
            txtPartyBank.BackColor = Color.Yellow;
        }

        private void txtPartyBank_Leave(object sender, EventArgs e)
        {
            txtPartyBank.BackColor = Color.White;
        }

        private void txtBranch_Enter(object sender, EventArgs e)
        {
           txtBranch.BackColor = Color.Yellow;
        }

        private void txtBranch_Leave(object sender, EventArgs e)
        {
            txtBranch.BackColor = Color.White;
        }

        private void cmbNationality_Enter(object sender, EventArgs e)
        {
            cmbCreditDebit.BackColor = Color.Yellow;
        }

        private void cmbNationality_Leave(object sender, EventArgs e)
        {
            cmbCreditDebit.BackColor = Color.White;
        }

        private void txtTranstype_Enter(object sender, EventArgs e)
        {
            txtTranstype.BackColor = Color.Yellow;
        }

        private void txtTranstype_Leave(object sender, EventArgs e)
        {
            txtTranstype.BackColor = Color.White;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
           // Close();
            if (grdchange == 3)
            {
                grdchange = 1;
                Fillgriddetails();
            }
            else if (grdchange == 2)
            {
                grdchange = 0;
                Fillgriddetails();
            }
            else if (grdchange == 1)
            {
                grdchange = 0;
                txtsearch.Focus();
                tabReceipt.SelectedTab = tabList;
                FillGrid("tbTransReceipt");
                BtnAdd.Visible = true;
            }
            else if (grdchange == 0)
            {
                Close();
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            flag = 1;
            tabReceipt.SelectedTab = tabList;
            grdchange = 0;
            FillGrid("tbTransReceipt");
            BtnAdd.Visible = true;
        }

        private void txtsearch_Enter(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.Yellow;
        }

        private void txtsearch_Leave(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.White;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            add();
        }

        public void add()
        {
            flag = 0;
            grdchange = 0;
            tabReceipt.SelectedTab = tabDetail;
            mod.addBtnClick(this, txtVoucherNo, tableName, firstCol, BtnSave, BtnDelete, txtBookCode);
            mod.txtclear(tabReceipt.TabPages[1].Controls);
            mod.UserAccessibillityMaster("Receipt", BtnAdd, BtnSave, BtnDelete);
            txtVoucherNo.Text = "" + mod.GetMaxNum(tableName, firstCol);
            FillCDO();
            BtnPrint.Enabled = false;
            btnGreaterID.Visible = false;
            btnLessID.Visible = false;
            dtpVoucherDate.Focus();
            BtnDelete.Text = "RESET";           
        }

        private void txtBookCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Globalvariable.SearchString = "DayBook";
                if (e.KeyCode == Keys.Enter && txtBookCode.Text != "")
                {
                    fillBookCode();
                }
                else if (txtBookCode.Text == "" && e.KeyCode == Keys.Enter)
                {
                    Globalvariable.searchNo = 1;
                    KeysConverter kc = new KeysConverter();
                    string key = kc.ConvertToString(e.KeyCode);
                    FrmSearch srch = new FrmSearch();
                    // str = "SELECT catgry_name,catgry_code FROM RoomCategoryMaster WHERE deleted='N'AND Branchcode='" + Globalvariable.bcode + "'";
                    srchda = mod.GetselectQuery("tbBook", "book_code", "book_description", "N", "N");
                    srch.val = key;
                    srch.fillbackgridSrch(srchda, "FrmReceipt");
                    ds.Clear();
                    srchda.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)         
                        srch.ShowDialog();
                    txtBookCode.Text = srch.codeselected;
                    if (srch.codeselected == null)
                    {
                        txtBookCode.Focus();
                    }
                    else
                    {
                        fillBookCode();
                    }
                    Globalvariable.searchNo = 0;
                    if (txtBookCodeNM.Text != "")
                    {
                        txtAmount.Focus();
                    }
                    else
                    {
                        txtBookCode.Focus();
                    }
                }
                else if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
                {
                    txtBookCode.Text = "";
                    txtBookCodeNM.Text = "";
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void txtPartyBank_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Globalvariable.SearchString = "Party Bank";
                if (e.KeyCode == Keys.Enter && txtBankCode.Text != "")
                {
                    fillPartyBank();
                }
                else if (txtPartyBank.Text == "" && e.KeyCode == Keys.Enter)
                {
                    Globalvariable.searchNo = 1;
                    KeysConverter kc = new KeysConverter();
                    string key = kc.ConvertToString(e.KeyCode);
                    FrmSearch srch = new FrmSearch();
                    srchda = mod.GetselectQuery("tbPartyBank", "bank_code", "bank_desc", "N", "N");
                    srch.val = key;
                    srch.fillbackgridSrch(srchda, "FrmReceipt");
                    ds.Clear();
                    srchda.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                        srch.ShowDialog();
                        txtBankCode.Text = srch.codeselected;
                        txtPartyBank.Text = srch.descselected;
                        if (srch.codeselected == null)
                        {
                            txtPartyBank.Focus();
                        }
                        else
                        {
                            fillPartyBank();
                        }
                        Globalvariable.searchNo = 0;
                        if (txtPartyBank.Text != "")
                        {
                            txtBranch.Focus();
                        }
                        else
                        {
                            txtPartyBank.Focus();
                        }
                    }
                }
                else if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
                {
                    txtBankCode.Text = "";
                    txtPartyBank.Text = "";
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void txtBranch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Globalvariable.SearchString = "Bank Location";
                if (e.KeyCode == Keys.Enter && txtBranch.Text != "")
                {
                    fillBankBranch();
                }
                else if (txtBranch.Text == "" && e.KeyCode == Keys.Enter)
                {
                    Globalvariable.searchNo = 1;
                    KeysConverter kc = new KeysConverter();
                    string key = kc.ConvertToString(e.KeyCode);
                    FrmSearch srch = new FrmSearch();
                    srchda = mod.GetselectQuery("tbBankLocation", "Bank_Code", "Bank_Location", "N", "N");
                    srch.val = key;
                    srch.fillbackgridSrch(srchda, "FrmReceipt");
                    ds.Clear();
                    srchda.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        srch.ShowDialog();
                        txtBranchcode.Text = srch.codeselected;
                        txtBranch.Text = srch.descselected;
                        if (srch.codeselected == null)
                        {
                            txtBranch.Focus();
                        }
                        else
                        {
                            fillBankBranch();
                        }
                        Globalvariable.searchNo = 0;
                        if (txtPartyBank.Text != "")
                        {
                            cmbCreditDebit.Focus();
                        }
                        else
                        {
                            txtBranch.Focus();
                        }
                    }
                }
                else if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
                {
                    txtBranchcode.Text = "";
                    txtBranch.Text = "";
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void cmbCreditDebit_Enter(object sender, EventArgs e)
        {
            cmbCreditDebit.BackColor = Color.Yellow;
        }

        private void cmbCreditDebit_Leave(object sender, EventArgs e)
        {
            cmbCreditDebit.BackColor = Color.White;
        }

        private void txtsearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && txtsearch.Text == "")
            {
                BtnAdd.Focus();
            }
        }

        private void FrmReceipt_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Control nextTab;
                if (e.KeyChar == (char)Keys.Enter)
                {
                    nextTab = ((Control)sender);
                    if (ActiveControl.Name == "txtsearch" && dtgridReceipt.Rows.Count == 0 && e.KeyChar == (char)Keys.Enter)
                    {
                        txtsearch.Focus();
                    }
                    else
                    {
                        if (ActiveControl.Name == "txtsearch" && dtgridReceipt.Rows.Count == 0)
                            nextTab = BtnAdd;
                        else if(ActiveControl.Name=="txtAmount" && txtBookCode.Text=="1")
                           {
                               txtCheckNo.Text = "";
                               txtCheckNo.Enabled = false;
                               txtBankCode.Text = "";
                               txtPartyBank.Text = "";
                               txtPartyBank.Enabled = false;
                               txtBranchcode.Text = "";
                               txtBranch.Text = "";
                               txtBranch.Enabled = false;
                               cmbCreditDebit.Focus();
                           }
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


        public void FillGrid(string tableNM)
        {
            try
            {
                SqlCommand cmd;
                SqlDataReader sqldr1, sqldr2, sqldr3;
                int cd = 1;
                DataTable dt = new DataTable();
                DataRow drw;
                string str;
                double samt;
                // Btnadd.Visible = true;
                dtgridReceipt.Columns.Clear();
                // m = gv.StDate.Month;
                // int months = gv.EndDate.Subtract(gv.StDate).Days / 30;
                dt.Columns.Add("Code", typeof(string));
                dt.Columns.Add("Description", typeof(string));
                dt.Columns.Add("Amount", typeof(string));

                cmd = new SqlCommand("SP_FrmReceipt", con.connect());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@variable", "SELECT");
              //  cmd.Parameters.AddWithValue("@gridchangeValue","0");
                cmd.Parameters.AddWithValue("@tablename", "tbTransReceipt");
                cmd.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                sqldr1 = cmd.ExecuteReader();
               // str = "select distinct(BK_CODE) from " + tablenm + " where Company_Sr='" + gv.companysr + "' and Branchcode='" + gv.Brncode + "'" + gv.advRecCriteria + "";
                while (sqldr1.Read())
                {
                    //strsql = "select * from book where bk_code='" + dr["BK_CODE"].ToString() + "' and Branchcode='" + gv.Brncode + "'";
                    cmd = new SqlCommand("SP_FrmReceipt", con.connect());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@variable", "SELECT");
                   // cmd.Parameters.AddWithValue("@gridchangeValue", "0");
                    cmd.Parameters.AddWithValue("@tablename", "tbBook");
                    cmd.Parameters.AddWithValue("@daybookcode", sqldr1["DayBook_Code"].ToString());
                    cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                    sqldr2 = cmd.ExecuteReader();
                   
                    if (sqldr2.Read())
                    {
                        drw = dt.NewRow();
                        drw["Code"] = mod.Isnull(sqldr2["book_code"].ToString(), "0");
                        drw["Description"] = mod.Isnull(sqldr2["book_description"].ToString(), "");
                      //  strsql = "select sum(TOT_AMT) from " + tablenm + " where BK_CODE='" + dr["BK_CODE"].ToString() + "' and Company_Sr='" + gv.companysr + "' and Branchcode='" + gv.Brncode + "'" + gv.advRecCriteria + "";
                        cmd = new SqlCommand("SP_FrmReceipt", con.connect());
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@variable", "SELECT");
                     //   cmd.Parameters.AddWithValue("@gridchangeValue", "0");
                        cmd.Parameters.AddWithValue("@tablename", "tbTransReceipt");
                        cmd.Parameters.AddWithValue("@checkstring","SUM");
                        cmd.Parameters.AddWithValue("@daybookcode", sqldr1["DayBook_Code"].ToString());
                        cmd.Parameters.AddWithValue("@compasrNo",Globalvariable.company_Srno);
                        cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                        sqldr3 = cmd.ExecuteReader();
                        if (sqldr3.Read())
                        {
                            drw["Amount"] = Convert.ToDouble(mod.Isnull(sqldr3[0].ToString(), "")).ToString("#0.00");
                        }
                        dt.Rows.Add(drw);
                    }
                }
                dtgridReceipt.DataSource = dt;
                dtgridReceipt.Columns[0].Width = 100;
                dtgridReceipt.Columns[1].Width = 401;
                dtgridReceipt.Columns[2].Width = 183;
                dtgridReceipt.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                if (dtgridReceipt.Rows.Count > 0)
                {
                    txtsearch.Text = dtgridReceipt.Rows[0].Cells[0].Value.ToString();
                }
                //Txtsearch.Text = DtgrdBarPrch.Rows[0].Cells[0].Value.ToString();
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void txtAmount_Enter_1(object sender, EventArgs e)
        {
            txtAmount.BackColor = Color.Yellow;
        }

        private void txtAmount_Leave_1(object sender, EventArgs e)
        {
            txtAmount.BackColor = Color.White;
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == '.');
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int i;
                string str="";
                string strCustType="";
                SqlCommand cmd;
                SqlCommand command=new SqlCommand();
                DateTime ST_date=Convert.ToDateTime(Globalvariable.StartDate);
                DateTime END_date=Convert.ToDateTime(Globalvariable.EndDate);
                if(dtpVoucherDate.Value < ST_date || dtpVoucherDate.Value>END_date)
                {
                    MessageBox.Show("Voch Date Does Not Come In Financial Year.");
                    dtpVoucherDate.Focus();
                }
                else if (txtBookCode.Text == "")
                {
                    txtBookCode.Focus();
                }
                    else if(txtBookCodeNM.Text=="")
                {
                    txtBookCode.Text = "";
                    txtBookCode.Focus();
                  }
                else if (txtAmount.Text == "")
                {
                    txtAmount.Focus();
                }
                else
                {
                   
                    if (BtnSave.Text == "SAVE")
                        str = "SELECT * FROM " + tableName + " WHERE DayBook_Code='" + txtBookCode.Text + "' AND Cheque_No='" + txtCheckNo.Text + "'";
                    else if (BtnSave.Text == "UPDATE")
                        str = "SELECT * FROM " + tableName + " WHERE DayBook_Code='" + txtBookCode.Text +
                            "' AND Cheque_No='" + txtCheckNo.Text + "' AND " + firstCol + " !=" + txtVoucherNo.Text + "";
                    SqlDataReader dr = mod.GetRecord(str);
                    if (dr.HasRows)
                    {
                         txtCheckNo.Focus();
                    }
                    else
                    {
                        //BtnSave.Enabled = false;
                       
                        if (cmbCreditDebit.SelectedItem == "CREDITOR")
                        {
                            strCustType = "C";
                        }
                        else if (cmbCreditDebit.SelectedItem == "DEBITOR")
                        {
                            strCustType = "D";
                        }
                        else if (cmbCreditDebit.SelectedItem == "OTHER")
                        {
                            strCustType = "O";
                        }

                            cmd = new SqlCommand("SP_FrmReceipt", con.connect());
                            cmd.CommandType = CommandType.StoredProcedure;

                            if (BtnSave.Text == "SAVE")
                            {
                                txtVoucherNo.Text = "" + mod.GetMaxNum(tableName, firstCol);
                            cmd.Parameters.AddWithValue("@variable", "INSERT");
                            }
                            else
                            {
                                str = "DELETE FROM tbTransReceiptDetail WHERE Voucher_No=" + txtVoucherNo.Text + " AND Company_Srno='" + Globalvariable.company_Srno + "'";
                                //command.CommandText = str;
                                command = new SqlCommand(str, con.connect());
                                command.ExecuteNonQuery();
                             cmd.Parameters.AddWithValue("@variable", "UPDATE");
                            }
                            cmd.Parameters.AddWithValue("@code", txtVoucherNo.Text);
                            cmd.Parameters.AddWithValue("@Voucherdate", dtpVoucherDate.Value.ToShortDateString());
                            cmd.Parameters.AddWithValue("@daybookcode", txtBookCode.Text);
                            cmd.Parameters.AddWithValue("@checkno", mod.Isnull(txtCheckNo.Text, "0"));
                            cmd.Parameters.AddWithValue("@TotalAmount", mod.Isnull(txtAmount.Text, "0"));
                            cmd.Parameters.AddWithValue("@custtype",strCustType);
                            cmd.Parameters.AddWithValue("@partyBankno", mod.Isnull(txtBankCode.Text, "0"));
                            cmd.Parameters.AddWithValue("@branchNo", mod.Isnull(txtBranchcode.Text, "0"));
                            cmd.Parameters.AddWithValue("@narration", mod.Isnull(txtNarration.Text, "-"));
                          // cmd.Parameters.AddWithValue("@glCode", mod.Isnull(txtgl Text, "0"));
                            cmd.Parameters.AddWithValue("@userid",Globalvariable.usercd);
                            cmd.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                            cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                            cmd.ExecuteNonQuery();
                            mod.txtclear(tabReceipt.TabPages[1].Controls);
                            txtVoucherNo.Text = "" + mod.GetMaxNum(tableName, firstCol);
                            FillCDO();
                            btnGreaterID.Visible = false;
                            btnLessID.Visible = false;
                            dtpVoucherDate.Focus();
                            BtnPrint.Enabled = true;
                           
                        }
                                               
                    }
                }
            
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void dtgridReceipt_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Fillgriddetails();
        }

        void Fillgriddetails()
        {
            try
            {
                if (dtgridReceipt.Rows.Count > 0)
                {
                    int mntotal, dayTot;
                    string strsql;
                    double totlamt;
                    DateTime dtm;
                    DataTable dt = new DataTable();
                    DataRow drw;
                    if (grdchange == 0)
                    {
                        int cd = 1;
                        double samt;
                        int i = dtgridReceipt.SelectedCells[0].RowIndex;
                        if (escGrid == 0)
                        {
                            m_bkcode = dtgridReceipt.Rows[i].Cells[0].Value.ToString();
                        }
                        dtgridReceipt.Columns.Clear();
                        // m = Globalvariable.StartDate;
                        DateTime expDateconv = Convert.ToDateTime(Globalvariable.EndDate);
                        DateTime startdate = Convert.ToDateTime(Globalvariable.StartDate);
                        string d = Globalvariable.EndDate;
                        m = startdate.Month;
                        // TimeSpan span = expDateconv.Subtract(startdate);
                        int months = expDateconv.Subtract(startdate).Days / 30;
                        dt.Columns.Add("Month", typeof(string));
                        dt.Columns.Add("Amount", typeof(string));
                        while (cd <= months)
                        {
                            drw = dt.NewRow();

                            if (m > 12)
                                m = 1;

                            drw["Month"] = mod.Getmonth(m);
                            //str = "select sum(TOT_AMT) from " + strHeadTable + " where BK_CODE='" + m_bkcode + "' and Company_Sr='" + gv.companysr +
                            //       "' and month(VOCH_DT)='" + m + "' and VOCH_DT  between '" + gv.StDate.ToString("MM/dd/yyyy") + "' and '" +
                            //       gv.EndDate.ToString("MM/dd/yyyy") + "' and Branchcode='" + gv.Brncode + "'" + gv.advRecCriteria + "";

                            cmd = new SqlCommand("SP_FrmReceipt", con.connect());
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@variable", "SELECT");
                            cmd.Parameters.AddWithValue("@gridchangeValue", "0");
                            cmd.Parameters.AddWithValue("@daybookcode", m_bkcode);
                            cmd.Parameters.AddWithValue("@month", m);
                            cmd.Parameters.AddWithValue("@startDate", Globalvariable.StartDate);
                            cmd.Parameters.AddWithValue("@endDate", Globalvariable.EndDate);
                            cmd.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                            cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                            samt = Convert.ToDouble(mod.Isnull(cmd.ExecuteScalar().ToString(), "0"));
                            drw["Amount"] = samt.ToString("#0.00");
                            // Convert.ToDouble(mod.Isnull(sqldr3[0].ToString(), "")).ToString("#0.00")
                            dt.Rows.Add(drw);
                            m = m + 1;
                            cd = cd + 1;
                        }
                        dtgridReceipt.DataSource = dt;

                        dtgridReceipt.Columns[0].Width = 496;
                        dtgridReceipt.Columns[1].Width = 183;
                        dtgridReceipt.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                        //Txtsearch.Text = DtgrdBarPrch.Rows[0].Cells[0].Value.ToString();
                        grdchange = 1;
                        escGrid = 0;
                        txtsearch.Text = dtgridReceipt.Rows[0].Cells[0].Value.ToString();
                        BtnAdd.Visible = false;
                    }
                    else if (grdchange == 1)
                    {
                        int i = dtgridReceipt.SelectedCells[0].RowIndex;
                        string cd = dtgridReceipt.Rows[i].Cells[1].Value.ToString();
                        if (dtgridReceipt.Rows[i].Cells[1].Value.ToString() != "0.00")
                        {
                            if (escGrid == 0 || escGrid == 1)
                            {
                                mnth = mod.Getmonthno(dtgridReceipt.Rows[i].Cells[0].Value.ToString());
                            }
                            dtgridReceipt.Columns.Clear();
                            dt.Columns.Add("Voucher Date", typeof(string));
                            dt.Columns.Add("Year", typeof(string));
                            dt.Columns.Add("Amount", typeof(string));

                            mntotal = 0;
                            //strsql = "select distinct(VOCH_DT) from " + strHeadTable + " where Company_Sr='" + gv.companysr + "' and month(VOCH_DT)='" +
                            //          mnth + "' and BK_CODE='" + m_bkcode + "' and Branchcode='" + gv.Brncode + "'" + gv.advRecCriteria + " order by VOCH_DT";
                            //cmd = new OdbcCommand(strsql, con.connect());
                            cmd = new SqlCommand("SP_FrmReceipt", con.connect());
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@variable", "SELECT");
                            cmd.Parameters.AddWithValue("@gridchangeValue", "1");
                            cmd.Parameters.AddWithValue("@month", mnth);
                            cmd.Parameters.AddWithValue("@daybookcode", m_bkcode);
                            cmd.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                            cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                            SqlDataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                dayTot = 0;
                                DateTime pdate = Convert.ToDateTime(dr[0].ToString());
                                //strsql = "select sum(tot_amt) from " + strHeadTable + " where Company_Sr='" + gv.companysr + "' and voch_dt='" +
                                //         pdate.ToString("MM/dd/yyyy") + "' and BK_CODE='" + m_bkcode + "' and Branchcode='" + gv.Brncode + "'" + gv.advRecCriteria + "";
                                //cmd = new OdbcCommand(strsql, con.connect());
                                cmd = new SqlCommand("SP_FrmReceipt", con.connect());
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@variable", "SELECT");
                                cmd.Parameters.AddWithValue("@gridchangeValue", "1");
                                cmd.Parameters.AddWithValue("@checkstring", "SUM");
                                cmd.Parameters.AddWithValue("@vouchDate", pdate.ToString("MM/dd/yyyy"));
                                cmd.Parameters.AddWithValue("@daybookcode", m_bkcode);
                                cmd.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                                cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                                totlamt = Convert.ToDouble(mod.Isnull(cmd.ExecuteScalar().ToString(), "0"));
                                drw = dt.NewRow();
                                dtm = Convert.ToDateTime(dr[0].ToString());
                                drw["Voucher Date"] = dtm.Day;
                                drw["Year"] = dtm.Year;
                                drw["Amount"] = totlamt.ToString("#0.00");
                                dt.Rows.Add(drw);
                            }
                            grdchange = 2;
                            escGrid = 1;
                            dr.Close();
                            dtgridReceipt.DataSource = dt;
                            dtgridReceipt.Columns[0].Width = 110;
                            dtgridReceipt.Columns[1].Width = 401;
                            dtgridReceipt.Columns[2].Width = 185;
                            dtgridReceipt.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                            txtsearch.Text = dtgridReceipt.Rows[0].Cells[0].Value.ToString();
                            BtnAdd.Visible = false;
                        }
                    }
                    else if (grdchange == 2)
                    {
                        int i = dtgridReceipt.SelectedCells[0].RowIndex;
                        if (Convert.ToString(dtgridReceipt.Rows[i].Cells[2].Value) != "0.00")
                        {
                            voch_dt = mnth + "/" + dtgridReceipt.Rows[i].Cells[0].Value + "/" + dtgridReceipt.Rows[i].Cells[1].Value;

                            dtgridReceipt.Columns.Clear();
                            dt.Columns.Add("Voucher No", typeof(string));
                            dt.Columns.Add("Book Code", typeof(string));
                            dt.Columns.Add("Description", typeof(string));
                            dt.Columns.Add("Amount", typeof(string));

                            //strsql = "select * from " + strHeadTable + " where BK_CODE='" + m_bkcode + "' and Company_Sr='" + gv.companysr +
                            //         "' and voch_dt='" + voch_dt + "' and Branchcode='" + gv.Brncode + "'" + gv.advRecCriteria + "";
                            cmd = new SqlCommand("SP_FrmReceipt", con.connect());
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@variable", "SELECT");
                            cmd.Parameters.AddWithValue("@gridchangeValue", "2");
                            cmd.Parameters.AddWithValue("@tablename", "tbTransReceipt");
                            cmd.Parameters.AddWithValue("@daybookcode", m_bkcode);
                            cmd.Parameters.AddWithValue("@vouchDate", voch_dt);
                            cmd.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                            cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                            SqlDataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                drw = dt.NewRow();
                                drw["Voucher No"] = dr["Voucher_No"].ToString();
                                drw["Book Code"] = dr["DayBook_Code"].ToString();
                                // strsql = "select * from book where bk_code='" + dr["BK_CODE"].ToString() + "' and Branchcode='" + gv.Brncode + "'";
                                cmd = new SqlCommand("SP_FrmReceipt", con.connect());
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@variable", "SELECT");
                                cmd.Parameters.AddWithValue("@gridchangeValue", "2");
                                cmd.Parameters.AddWithValue("@tablename", "tbBook");
                                cmd.Parameters.AddWithValue("@daybookcode", dr["DayBook_Code"].ToString());
                                cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                                SqlDataReader rs = cmd.ExecuteReader();
                                if (rs.Read())
                                {
                                    drw["Description"] = rs["book_description"].ToString();
                                }
                                drw["Amount"] = Convert.ToDouble(dr["Total_Amount"].ToString()).ToString("#0.00");
                                dt.Rows.Add(drw);
                            }
                            grdchange = 3;
                            escGrid = 2;
                            dr.Close();
                            dtgridReceipt.DataSource = dt;
                            dtgridReceipt.Columns[0].Width = 107;
                            dtgridReceipt.Columns[1].Width = 107;
                            dtgridReceipt.Columns[2].Width = 318;
                            dtgridReceipt.Columns[3].Width = 151;
                            dtgridReceipt.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                            txtsearch.Text = dtgridReceipt.Rows[0].Cells[0].Value.ToString();
                            BtnAdd.Visible = false;
                        }
                    }
                    else if (grdchange == 3)
                    {
                        flag = 1;
                        tabReceipt.SelectedTab = tabDetail;
                        btnLessID.Visible = true;
                        btnGreaterID.Visible = true;
                        BtnDelete.Text = "DELETE";
                        BtnPrint.Enabled = true ;
                    }
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }    

        private void tabReceipt_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
               SqlDataReader dr, dr1;
                string str, Pdate, stmess;
                int i = 0, j = 1, sup = 0, k;
                DataTable dt = new DataTable();
                DataRow drw;

                if (tabReceipt.SelectedTab == tabDetail)
                {
                    if (flag == 1)
                    {                   
                        if(dtgridReceipt.RowCount>0)
                        {
                        k = dtgridReceipt.SelectedCells[0].RowIndex;
                        if (dtgridReceipt.Rows[k].Cells[1].Value.ToString() != "0.00" && grdchange != 1)
                        {
                            detail_id = 1;
                            i = dtgridReceipt.SelectedCells[0].RowIndex;
                            txtVoucherNo.Text = dtgridReceipt.Rows[i].Cells[0].Value.ToString();
                            string ID = mod.Isnull(dtgridReceipt.Rows[i].Cells[0].Value.ToString(),"");
                            SqlDataReader dareader = mod.GetSelectAllField(tableName, firstCol, ID, "SrNo");
                           FillDetails(dareader,"null");
                            BtnDelete.Text = "DELETE";
                            BtnSave.Text="UPDATE";
                        }
                        else
                        {
                            MessageBox.Show("No record to display");
                            this.tabReceipt.SelectedTab = tabList;
                        }
                        }                        
                   }
                }
                detail_id = 0;
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }




        private void dtgridReceipt_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
            int i = 0;
            if (e.KeyCode == Keys.Enter)
            {
                Fillgriddetails();
            }
            else
            {
                if (e.KeyCode == Keys.Down)
                    i = dtgridReceipt.SelectedCells[0].RowIndex + 1;
                else if (e.KeyCode == Keys.Up)
                    i = dtgridReceipt.SelectedCells[0].RowIndex - 1;

                if (((e.KeyCode == Keys.Down) && (i < dtgridReceipt.RowCount)) || ((e.KeyCode == Keys.Up) && (i >= 0)))
                {
                    dtgridReceipt.Refresh();
                    dtgridReceipt.CurrentCell = dtgridReceipt.Rows[i].Cells[0];
                    dtgridReceipt.Rows[i].Selected = true;
                    if (dtgridReceipt.Rows.Count > 0)
                    {
                        txtsearch.Text = dtgridReceipt.Rows[i].Cells[0].Value.ToString();
                    }
                }
                txtsearch.Focus();             
                e.Handled = true;
            }
           
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }


            //try
            //{
            //    if (e.KeyChar == (char)Keys.Escape)
            //    {
            //        if (grdchange == 2)
            //        {
            //            escGrid = 1;
            //            grdchange = 0;
            //            Fillgriddetails();
            //        }
            //        else if (grdchange == 1)
            //        {
            //            FrmReceipt_Load(sender, EventArgs.Empty);
            //        }
            //        else if (grdchange == 0)
            //            Close();

            //    }
            //    else if (e.KeyChar == (char)Keys.Enter)
            //    {
            //        Fillgriddetails();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    string str = "Message:" + ex.Message;
            //    MessageBox.Show(str, "Error Message");
            //}






        }

        private void txtsearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                dtgridReceipt_KeyDown(sender, e);
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void btnLessID_Click(object sender, EventArgs e)
        {
            try
            {
                string daybookCode,voucherID ;
                 daybookCode=txtBookCode.Text.ToString();
                voucherID=txtVoucherNo.Text.ToString();
                SqlDataReader dr = SelectLessGreaterID("LESS_ID",daybookCode,voucherID);             
                FillDetails(dr, "LESS_ID");              
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        public void FillDetails(SqlDataReader RschallanHD,string strvariable)
        {
            try
            {
                if (RschallanHD.Read())
                {
                    //OdbcDataReader name = mod.GetRecord("select * from book where bk_code='" + RschallanHD["BK_CODE"].ToString() + "' and Branchcode='" + gv.Brncode + "'");
                    txtVoucherNo.Text = "";
                    txtVoucherNo.Text = RschallanHD["Voucher_No"].ToString();
                    SqlDataReader dareader = mod.GetSelectAllField("tbBook", "book_code", RschallanHD["DayBook_Code"].ToString(), "");
                    txtBookCode.Text = RschallanHD["DayBook_Code"].ToString();
                    if (dareader.Read())
                    {
                        txtBookCodeNM.Text = dareader["book_description"].ToString();
                    }
                    dtpVoucherDate.Text = RschallanHD["Voucher_Date"].ToString();
                    txtCheckNo.Text = RschallanHD["Cheque_No"].ToString();
                    txtAmount.Text = Convert.ToDouble(RschallanHD["Total_Amount"].ToString()).ToString("#0.00");
                    txtNarration.Text = RschallanHD["Narration"].ToString();
                    //str = "select * from partybank where bk_code='" + RschallanHD["P_BANK"].ToString() + "' and Branchcode='" + gv.Brncode + "'";
                    dareader = mod.GetSelectAllField("tbPartyBank", "bank_code", mod.Isnull(RschallanHD["PartyBank"].ToString(), "0"), "");

                    txtBankCode.Text = RschallanHD["PartyBank"].ToString();
                    if (dareader.Read())
                    {
                        txtPartyBank.Text = dareader["bank_desc"].ToString();
                    }
                    else
                    {
                        txtPartyBank.Text = "";
                    }
                    // str = "select * from Location where bk_code='" + RschallanHD["P_BRANCH"].ToString() + "' and Branchcode='" + gv.Brncode + "'";
                    dareader = mod.GetSelectAllField("tbBankLocation", "Bank_Code", RschallanHD["Branch"].ToString(), "");
                    txtBranchcode.Text = RschallanHD["Branch"].ToString();
                    if (dareader.Read())
                    {
                        txtBranch.Text = dareader["Bank_Location"].ToString();
                    }
                    else
                    {
                        txtBranch.Text = "";
                    }
                    string voucherNo = txtVoucherNo.Text.ToString();
                    SqlDataReader CDOreader = mod.GetSelectAllField("tbTransReceiptDetail", "Voucher_No", voucherNo, "SrNo");
                    if (CDOreader.Read())
                    {
                        fillCDO = Convert.ToString(CDOreader["Cust_Type"]);
                    }
                    if (fillCDO != "")
                    {
                        if (fillCDO == "C")
                            cmbCreditDebit.Items.Insert(0, "CREDITOR");
                        else if (fillCDO == "D")
                            cmbCreditDebit.Items.Insert(0, "DEBITOR");
                        else if (fillCDO == "O")
                            cmbCreditDebit.Items.Insert(0, "OTHER");
                    }
                    cmbCreditDebit.SelectedIndex = 0;
                    FillCDO();
                   
                }
                else
                {
                    if (strvariable == "LESS_ID")
                    {
                          MessageBox.Show("First Record!!!");
                    }
                    else if (strvariable == "GREATER_ID")
                    {
                          MessageBox.Show("Last Record!!!");
                    }
                  
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }


        public SqlDataReader SelectLessGreaterID(string Variable, string daybokCode, string id)
        {
            cmd = new SqlCommand("SP_FrmReceipt", con.connect());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@variable", Variable);
            cmd.Parameters.AddWithValue("@daybookcode", daybokCode);
            cmd.Parameters.AddWithValue("@code", id);
            cmd.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
            cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
            SqlDataReader sqldr = cmd.ExecuteReader();
            return sqldr;
            //sqldr.Close();    
        }

        private void btnGreaterID_Click(object sender, EventArgs e)
        {
            try
            {
                string daybookCode, voucherID;
                daybookCode = txtBookCode.Text.ToString();
                voucherID = txtVoucherNo.Text.ToString();
                SqlDataReader dr = SelectLessGreaterID("GREATER_ID", daybookCode, voucherID);
                FillDetails(dr,"GREATER_ID");
                
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
                if (BtnDelete.Text == "DELETE")
                {
                    DialogResult mydialog;
                    mydialog = MessageBox.Show("Are you Sure you want to delete this record", "", MessageBoxButtons.YesNo);
                    if (mydialog == DialogResult.Yes)
                    {
                        cmd = new SqlCommand("SP_FrmReceipt", con.connect());
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@variable", "DELETE");
                        cmd.Parameters.AddWithValue("@code", txtVoucherNo.Text);
                        cmd.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                        cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                        cmd.ExecuteNonQuery();
                        txtsearch.Focus();
                        tabReceipt.SelectedTab = tabList;
                        //FillGrid("tbTransReceipt");
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

        private void txtsearch_TextChanged_1(object sender, EventArgs e)
        {
           mod.transSearch(txtsearch, dtgridReceipt, 0);
        }

        private void tabReceipt_Selecting(object sender, TabControlCancelEventArgs e)
        {
            try
            {
                if (e.TabPage.Name == "tabDetail" && grdchange != 3 && flag != 0)
                    e.Cancel = true;
                else if (e.TabPage.Name == "tabList")
                    // BtnCancel_Click(sender, e);
                    BtnAdd.Enabled = true;
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void FrmReceipt_Shown(object sender, EventArgs e)
        {
            txtsearch.Focus();
        }
    }
}

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
    public partial class FrmPayment : Form
    {
        Design ObjDesign = new Design();
        public FrmPayment()
        {
            InitializeComponent();
        }

        string firstCol = "Voucher_No";
        string tableName = "tbTransPayment";
        string voch_dt, fillCDO = "";
        int detail_id = 0, grdchange, escGrid, m, mnth, flag = 0;
      //  string firstCol = "Voucher_No";
       // string tableName = "tbTransReceipt";
        public string m_bkcode;
        SqlDataReader srchdr;
        SqlDataAdapter srchda;
        Connection con = new Connection();
        DataSet ds = new DataSet();
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
        private void FrmPayment_Load(object sender, EventArgs e)
        {
            ObjDesign.FormDesign(this, dtgridGLcode);
            ObjDesign.FormDesign(this, dtgridPayment);
            flag = 1;
            txtsearch.Focus();
            tabPayment.SelectedTab = tabList;
            FillGrid("tbTransPayment");
            txtBookCodeNM.Enabled = false;
            txtVoucherNo.Enabled = false;
            txtBranch.Enabled = false;
            mod.UserAccessibillityMaster("Payment", BtnAdd, BtnSave, BtnDelete);
            Globalvariable.SearchChangeVariable = "SrchByName";
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

        private void txtsearch_Enter(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.Yellow;
        }

        private void txtsearch_Leave(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.White;
        }

        private void txtBookCode_Enter(object sender, EventArgs e)
        {
            txtBookCode.BackColor = Color.Yellow;
         }

        private void txtBookCode_Leave(object sender, EventArgs e)
        {
            txtBookCode.BackColor = Color.White;
        }

        private void txtAmount_Enter(object sender, EventArgs e)
        {
            txtAmount.BackColor = Color.Yellow;
        }

        private void txtAmount_Leave(object sender, EventArgs e)
        {
            txtAmount.BackColor = Color.White;
        }

        private void txtCheckNo_Enter(object sender, EventArgs e)
        {
            txtCheckNo.BackColor = Color.Yellow;
        }

        private void txtCheckNo_Leave(object sender, EventArgs e)
        {
            txtCheckNo.BackColor = Color.White;
        }
     
        private void txtBranch_Enter(object sender, EventArgs e)
        {
            txtBranch.BackColor = Color.Yellow;
        }

        private void txtBranch_Leave(object sender, EventArgs e)
        {
            txtBranch.BackColor = Color.White;
        }

        private void cmbCreditDebit_Enter(object sender, EventArgs e)
        {
            cmbCreditDebit.BackColor = Color.Yellow;
        }

        private void cmbCreditDebit_Leave(object sender, EventArgs e)
        {
            cmbCreditDebit.BackColor = Color.White;
        }

        private void txtNarration_Enter(object sender, EventArgs e)
        {
            txtNarration.BackColor = Color.Yellow;
        }

        private void txtNarration_Leave(object sender, EventArgs e)
        {
            txtNarration.BackColor = Color.White;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
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
                tabPayment.SelectedTab = tabList;
                FillGrid("tbTransPayment");
                BtnAdd.Visible = true;
            }
            else if (grdchange == 0)
            {
                Close();
            }
        }

        void Fillgriddetails()
        {
            try
            {
                if (dtgridPayment.Rows.Count > 0)
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
                        int i = dtgridPayment.SelectedCells[0].RowIndex;
                        if (escGrid == 0)
                        {
                            m_bkcode = dtgridPayment.Rows[i].Cells[0].Value.ToString();
                        }
                        dtgridPayment.Columns.Clear();
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

                            cmd = new SqlCommand("SP_FrmTransPayment", con.connect());
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
                        dtgridPayment.DataSource = dt;
                        dtgridPayment.Columns[0].Width = 555;
                        dtgridPayment.Columns[1].Width = 192;
                        dtgridPayment.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                        //Txtsearch.Text = DtgrdBarPrch.Rows[0].Cells[0].Value.ToString();
                        grdchange = 1;
                        escGrid = 0;
                        txtsearch.Text = dtgridPayment.Rows[0].Cells[0].Value.ToString();
                        BtnAdd.Visible = false;
                    }
                    else if (grdchange == 1)
                    {
                        int i = dtgridPayment.SelectedCells[0].RowIndex;
                        string cd = dtgridPayment.Rows[i].Cells[1].Value.ToString();
                        if (dtgridPayment.Rows[i].Cells[1].Value.ToString() != "0.00")
                        {
                            if (escGrid == 0 || escGrid == 1)
                            {
                                mnth = mod.Getmonthno(dtgridPayment.Rows[i].Cells[0].Value.ToString());
                            }
                            dtgridPayment.Columns.Clear();
                            dt.Columns.Add("Voucher Date", typeof(string));
                            dt.Columns.Add("Year", typeof(string));
                            dt.Columns.Add("Amount", typeof(string));

                            mntotal = 0;
                            //strsql = "select distinct(VOCH_DT) from " + strHeadTable + " where Company_Sr='" + gv.companysr + "' and month(VOCH_DT)='" +
                            //          mnth + "' and BK_CODE='" + m_bkcode + "' and Branchcode='" + gv.Brncode + "'" + gv.advRecCriteria + " order by VOCH_DT";
                            //cmd = new OdbcCommand(strsql, con.connect());
                            cmd = new SqlCommand("SP_FrmTransPayment", con.connect());
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
                                cmd = new SqlCommand("SP_FrmTransPayment", con.connect());
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
                            dtgridPayment.DataSource = dt;
                            dtgridPayment.Columns[0].Width = 120;
                            dtgridPayment.Columns[1].Width = 435;
                            dtgridPayment.Columns[2].Width = 195;
                            dtgridPayment.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                            txtsearch.Text = dtgridPayment.Rows[0].Cells[0].Value.ToString();
                            BtnAdd.Visible = false;
                        }
                    }
                    else if (grdchange == 2)
                    {
                        int i = dtgridPayment.SelectedCells[0].RowIndex;
                        if (Convert.ToString(dtgridPayment.Rows[i].Cells[2].Value) != "0.00")
                        {
                            voch_dt = mnth + "/" + dtgridPayment.Rows[i].Cells[0].Value + "/" + dtgridPayment.Rows[i].Cells[1].Value;

                            dtgridPayment.Columns.Clear();
                            dt.Columns.Add("Voucher No", typeof(string));
                            dt.Columns.Add("Book Code", typeof(string));
                            dt.Columns.Add("Description", typeof(string));
                            dt.Columns.Add("Amount", typeof(string));

                            //strsql = "select * from " + strHeadTable + " where BK_CODE='" + m_bkcode + "' and Company_Sr='" + gv.companysr +
                            //         "' and voch_dt='" + voch_dt + "' and Branchcode='" + gv.Brncode + "'" + gv.advRecCriteria + "";
                            cmd = new SqlCommand("SP_FrmTransPayment", con.connect());
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@variable", "SELECT");
                            cmd.Parameters.AddWithValue("@gridchangeValue", "2");
                            cmd.Parameters.AddWithValue("@tablename", "tbTransPayment");
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
                                cmd = new SqlCommand("SP_FrmTransPayment", con.connect());
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
                                drw["Amount"] = Convert.ToDouble(dr["Amount"].ToString()).ToString("#0.00");
                                dt.Rows.Add(drw);
                            }
                            grdchange = 3;
                            escGrid = 2;
                            dr.Close();
                            dtgridPayment.DataSource = dt;
                            dtgridPayment.Columns[0].Width = 117;
                            dtgridPayment.Columns[1].Width = 117;
                            dtgridPayment.Columns[2].Width = 352;
                            dtgridPayment.Columns[3].Width = 160;
                            dtgridPayment.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                            txtsearch.Text = dtgridPayment.Rows[0].Cells[0].Value.ToString();
                            BtnAdd.Visible = false;
                        }
                    }
                    else if (grdchange == 3)
                    {
                        flag = 1;
                        tabPayment.SelectedTab = tabDetail;
                        btnLessID.Visible = true;
                        btnGreaterID.Visible = true;
                        BtnDelete.Text = "DELETE";
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

        private void FrmPayment_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Control nextTab;
                if (e.KeyChar == (char)Keys.Enter)
                {
                    nextTab = ((Control)sender);
                    if (ActiveControl.Name == "txtsearch" && dtgridPayment.Rows.Count == 0 && e.KeyChar == (char)Keys.Enter)
                    {
                        txtsearch.Focus();
                    }
                    else
                    {
                        if (ActiveControl.Name == "txtsearch" && dtgridPayment.Rows.Count == 0)
                            nextTab = BtnAdd;
                        else if (ActiveControl.Name == "txtBookCode" && txtBookCode.Text == "1")
                        {
                            txtCheckNo.Text = "";
                            txtCheckNo.Enabled = false;
                           txtBranchCode.Text = "";
                            txtBranch.Text = "";
                            txtBranchCode.Enabled = false;
                            txtAmount.Focus();
                        }
                        else if (ActiveControl.Name == "txtAmount" && txtCheckNo.Enabled==false)
                        {
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

        private void FrmPayment_KeyDown(object sender, KeyEventArgs e)
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
                    srch.fillbackgridSrch(srchda, "FrmPayment");
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
                    if (txtBookCodeNM.Text != "" )
                    {
                        if(txtBookCode.Text!="1")
                        {
                        txtBranchCode.Focus();
                        }
                        else if(txtBookCode.Text=="1")
                        {
                            txtBranchCode.Enabled = false;
                            txtBranch.Enabled = false;
                            txtAmount.Focus();
                        }
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

        private void fillBranchCode()
        {
            try
            {
                if (txtBranchCode.Text.Trim().Equals(""))
                {
                    txtBranch.Text = "";
                }
                else
                {
                    SqlDataReader dr = mod.GetRecord("SELECT Bank_Location,Bank_Code FROM tbBankLocation WHERE deleted='N' AND Branchcode='" + Globalvariable.bcode +
                        "' AND Bank_Code='" + txtBranchCode.Text + "'");
                    if (dr.HasRows)
                    {
                        dr.Read();
                        txtBranch.Text = dr["Bank_Location"].ToString();
                        //   txtSingleRate.Focus();
                    }
                    else
                    {
                        txtBranchCode.Text = "";
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

        private void txtBranchCode_Enter(object sender, EventArgs e)
        {
            txtBranchCode.BackColor = Color.Yellow;
        }

        private void txtBranchCode_Leave(object sender, EventArgs e)
        {
            txtBranchCode.BackColor = Color.White;
        }

        private void txtBranchCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Globalvariable.SearchString = "Bank Location";
                if (e.KeyCode == Keys.Enter && txtBranchCode.Text != "")
                {
                    fillBranchCode();
                }
                else if (txtBranchCode.Text == "" && e.KeyCode == Keys.Enter)
                {
                    Globalvariable.searchNo = 1;
                    KeysConverter kc = new KeysConverter();
                    string key = kc.ConvertToString(e.KeyCode);
                    FrmSearch srch = new FrmSearch();
                    srchda = mod.GetselectQuery("tbBankLocation", "Bank_Code", "Bank_Location", "N", "N");
                    srch.val = key;
                    srch.fillbackgridSrch(srchda, "FrmPayment");
                    ds.Clear();
                    srchda.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    srch.ShowDialog();
                    txtBranchCode.Text = srch.codeselected;
                    if (srch.codeselected == null)
                    {
                        txtBranchCode.Focus();
                    }
                    else
                    {
                        fillBranchCode();
                    }
                    Globalvariable.searchNo = 0;
                    if (txtBranch.Text != "")
                    {
                        txtAmount.Focus();
                    }
                    else
                    {
                        txtBranchCode.Focus();
                    }
                }
                else if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
                {
                    txtBranchCode.Text = "";
                    txtBranch.Text = "";
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
            flag = 0;
            grdchange = 0;
            tabPayment.SelectedTab = tabDetail;
            mod.addBtnClick(this, txtVoucherNo, tableName, firstCol, BtnSave, BtnDelete, txtBookCode);
            mod.txtclear(tabPayment.TabPages[1].Controls);
            mod.UserAccessibillityMaster("Payment", BtnAdd, BtnSave, BtnDelete);
            txtVoucherNo.Text = "" + mod.GetMaxNum(tableName, firstCol);
            FillCDO();
            BtnPrint.Enabled = false;
            btnGreaterID.Visible = false;
            btnLessID.Visible = false;
            txtBranch.Enabled = false;
            dtpVoucherDate.Focus();
            BtnDelete.Text = "RESET";
        }

        public void FillDetails(SqlDataReader RschallanHD, string strvariable)
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
                    txtCheckNo.Text = RschallanHD["Check_No"].ToString();
                    txtAmount.Text = Convert.ToDouble(RschallanHD["Amount"].ToString()).ToString("#0.00");
                    txtNarration.Text = RschallanHD["Narration"].ToString();
                    dareader = mod.GetSelectAllField("tbBankLocation", "Bank_Code", RschallanHD["Branch_Code"].ToString(), "");
                    txtBranchCode.Text = RschallanHD["Branch_Code"].ToString();
                    if (dareader.Read())
                    {
                        txtBranch.Text = dareader["Bank_Location"].ToString();
                    }
                    else
                    {
                        txtBranch.Text = "";
                    }
                    string voucherNo = txtVoucherNo.Text.ToString();
                    SqlDataReader CDOreader = mod.GetSelectAllField("tbTransPaymentDetail", "Voucher_No", voucherNo, "SrNo");
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

        private void tabPayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SqlDataReader dr, dr1;
                string str, Pdate, stmess;
                int i = 0, j = 1, sup = 0, k;
                DataTable dt = new DataTable();
                DataRow drw;

                if (tabPayment.SelectedTab == tabDetail)
                {
                    if (flag == 1)
                    {
                        if (dtgridPayment.RowCount > 0)
                        {
                            k = dtgridPayment.SelectedCells[0].RowIndex;
                            if (dtgridPayment.Rows[k].Cells[1].Value.ToString() != "0.00" && grdchange != 1)
                            {
                                detail_id = 1;
                                i = dtgridPayment.SelectedCells[0].RowIndex;
                                txtVoucherNo.Text = dtgridPayment.Rows[i].Cells[0].Value.ToString();
                                string ID = mod.Isnull(dtgridPayment.Rows[i].Cells[0].Value.ToString(), "");
                                SqlDataReader dareader = mod.GetSelectAllField(tableName, firstCol, ID, "SrNo");
                                FillDetails(dareader, "null");
                                BtnDelete.Text = "DELETE";
                                BtnSave.Text = "UPDATE";
                            }
                            else
                            {
                                MessageBox.Show("No record to display");
                                this.tabPayment.SelectedTab = tabList;
                            }
                        }
                    }
                }
                else if(tabPayment.SelectedTab==tabList)
                {
                    FillGrid("tbTransPayment");
                }
                detail_id = 0;
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
                dtgridPayment.Columns.Clear();
                dt.Columns.Add("Code", typeof(string));
                dt.Columns.Add("Description", typeof(string));
                dt.Columns.Add("Amount", typeof(string));
                cmd = new SqlCommand("SP_FrmTransPayment", con.connect());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@variable", "SELECT");
                cmd.Parameters.AddWithValue("@tablename", "tbTransPayment");
                cmd.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                sqldr1 = cmd.ExecuteReader();
                while (sqldr1.Read())
                {
                    cmd = new SqlCommand("SP_FrmTransPayment", con.connect());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@variable", "SELECT");
                    cmd.Parameters.AddWithValue("@tablename", "tbBook");
                    cmd.Parameters.AddWithValue("@daybookcode", sqldr1["DayBook_Code"].ToString());
                    cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                    sqldr2 = cmd.ExecuteReader();

                    if (sqldr2.Read())
                    {
                        drw = dt.NewRow();
                        drw["Code"] = mod.Isnull(sqldr2["book_code"].ToString(), "0");
                        drw["Description"] = mod.Isnull(sqldr2["book_description"].ToString(), "");
                        cmd = new SqlCommand("SP_FrmTransPayment", con.connect());
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@variable", "SELECT");
                        cmd.Parameters.AddWithValue("@tablename", "tbTransPayment");
                        cmd.Parameters.AddWithValue("@checkstring", "SUM");
                        cmd.Parameters.AddWithValue("@daybookcode", sqldr1["DayBook_Code"].ToString());
                        cmd.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                        cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                        sqldr3 = cmd.ExecuteReader();
                        if (sqldr3.Read())
                        {
                            drw["Amount"] = Convert.ToDouble(mod.Isnull(sqldr3[0].ToString(), "")).ToString("#0.00");
                        }
                        dt.Rows.Add(drw);
                    }
                }
                dtgridPayment.DataSource = dt;
                dtgridPayment.Columns[0].Width = 120;
                dtgridPayment.Columns[1].Width = 435;
                dtgridPayment.Columns[2].Width = 192;
                dtgridPayment.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                if (dtgridPayment.Rows.Count > 0)
                {
                    txtsearch.Text = dtgridPayment.Rows[0].Cells[0].Value.ToString();
                }
                //Txtsearch.Text = DtgrdBarPrch.Rows[0].Cells[0].Value.ToString();
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
                string daybookCode, voucherID;
                daybookCode = txtBookCode.Text.ToString();
                voucherID = txtVoucherNo.Text.ToString();
                SqlDataReader dr = SelectLessGreaterID("LESS_ID", daybookCode, voucherID);
                FillDetails(dr, "LESS_ID");
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        public SqlDataReader SelectLessGreaterID(string Variable, string daybokCode, string id)
        {
            cmd = new SqlCommand("SP_FrmTransPayment", con.connect());
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
                FillDetails(dr, "GREATER_ID");

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
                        cmd = new SqlCommand("SP_FrmTransPayment", con.connect());
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@variable", "DELETE");
                        cmd.Parameters.AddWithValue("@code", txtVoucherNo.Text);
                        cmd.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                        cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                        cmd.ExecuteNonQuery();
                        //txtsearch.Focus();
                        tabPayment.SelectedTab = tabList;
                        //FillGrid("tbTransPayment");
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

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            flag = 1;
            tabPayment.SelectedTab = tabList;       
            grdchange = 0;
            FillGrid("tbTransPayment");
            BtnAdd.Visible = true;
        }

        private void txtsearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                dtgridPayment_KeyDown(sender, e);
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
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            mod.transSearch(txtsearch, dtgridPayment, 0);
        }

        private void dtgridPayment_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Fillgriddetails();
        }

        private void dtgridPayment_KeyDown(object sender, KeyEventArgs e)
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
                        i = dtgridPayment.SelectedCells[0].RowIndex + 1;
                    else if (e.KeyCode == Keys.Up)
                        i = dtgridPayment.SelectedCells[0].RowIndex - 1;

                    if (((e.KeyCode == Keys.Down) && (i < dtgridPayment.RowCount)) || ((e.KeyCode == Keys.Up) && (i >= 0)))
                    {
                        dtgridPayment.Refresh();
                        dtgridPayment.CurrentCell = dtgridPayment.Rows[i].Cells[0];
                        dtgridPayment.Rows[i].Selected = true;
                        if (dtgridPayment.Rows.Count > 0)
                        {
                            txtsearch.Text = dtgridPayment.Rows[i].Cells[0].Value.ToString();
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

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int i;
                string str = "";
                string strCustType = "";
                SqlCommand cmd;
                SqlCommand command = new SqlCommand();
                DateTime ST_date = Convert.ToDateTime(Globalvariable.StartDate);
                DateTime END_date = Convert.ToDateTime(Globalvariable.EndDate);
                if (dtpVoucherDate.Value < ST_date || dtpVoucherDate.Value > END_date)
                {
                    MessageBox.Show("Voch Date Does Not Come In Financial Year.");
                    dtpVoucherDate.Focus();
                }
                else if (txtBookCode.Text == "")
                {
                    txtBookCode.Focus();
                }
                else if (txtBookCodeNM.Text == "")
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
                        str = "SELECT * FROM " + tableName + " WHERE DayBook_Code='" + txtBookCode.Text + "' AND Check_No='" + txtCheckNo.Text + "'";
                    else if (BtnSave.Text == "UPDATE")
                        str = "SELECT * FROM " + tableName + " WHERE DayBook_Code='" + txtBookCode.Text +
                            "' AND Check_No='" + txtCheckNo.Text + "' AND " + firstCol + " !=" + txtVoucherNo.Text + "";
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

                        cmd = new SqlCommand("SP_FrmTransPayment", con.connect());
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (BtnSave.Text == "SAVE")
                        {
                            txtVoucherNo.Text = "" + mod.GetMaxNum(tableName, firstCol);
                            cmd.Parameters.AddWithValue("@variable", "INSERT");
                        }
                        else
                        {
                            str = "DELETE FROM tbTransPaymentDetail WHERE Voucher_No=" + txtVoucherNo.Text + " AND Company_Srno='" + Globalvariable.company_Srno + "'";
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
                        cmd.Parameters.AddWithValue("@custtype", strCustType);
                        cmd.Parameters.AddWithValue("@branchNo", mod.Isnull(txtBranchCode.Text, "0"));
                        cmd.Parameters.AddWithValue("@narration", mod.Isnull(txtNarration.Text, "-"));
                        // cmd.Parameters.AddWithValue("@glCode", mod.Isnull(txtgl Text, "0"));
                        cmd.Parameters.AddWithValue("@userid", Globalvariable.usercd);
                        cmd.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                        cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                        cmd.ExecuteNonQuery();
                        mod.txtclear(tabPayment.TabPages[1].Controls);
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

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == '.');
        }

        private void tabPayment_Selecting(object sender, TabControlCancelEventArgs e)
        {
            try
            {
                if (e.TabPage.Name == "tabDetail" && grdchange != 3 && flag != 0)
                    e.Cancel = true;
                else if (e.TabPage.Name == "tabList")
                    BtnAdd.Visible = true;
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void FrmPayment_Shown(object sender, EventArgs e)
        {
            txtsearch.Focus();
        }
    } 
}

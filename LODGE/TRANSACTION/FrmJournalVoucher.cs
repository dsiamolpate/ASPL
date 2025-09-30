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
    public partial class FrmJournalVoucher : Form
    {
        Design ObjDesign = new Design();
        public FrmJournalVoucher()
        {
            InitializeComponent();
        }
        int rowNo, colNo;
        string ExitCode;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        SqlDataAdapter da;
        public Boolean plusKeyPress;
        double amount;
        public static SqlDataReader srchdr;
        public static SqlDataAdapter srchda;
      // public static SqlDataReader drsrch;
        string firstCol = "Voucher_No";
        string tableName = "tbTransJournalVoucher";
        Connection con = new Connection();
        SqlCommand cmd;
        Module mod = new Module();
        string voch_dt, code = "";
        bool chkDupFlag;
        string str;
        DataGridViewTextBoxEditingControl tb;
        int grdchange, m, escGrid, mnth, flag;
        public string DupCode1 = "0";
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

        public void FillGrid(string tableNM)
        {
            try
            {
                // int mntotal, dayTot;
                string strsql;
                // double totlamt;
                // DateTime dtm;
                DataTable dt = new DataTable();
                DataRow drw;
                int cd = 1;
                double samt;
                dtgridJournalVoucher.Columns.Clear();
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

                    cmd = new SqlCommand("SP_FrmTransJournalVoucher", con.connect());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@variable", "SELECT");
                    cmd.Parameters.AddWithValue("@tablename", "tbTransJournalVoucher");
                    cmd.Parameters.AddWithValue("@checkstring", "SUM");
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
                dtgridJournalVoucher.DataSource = dt;
                dtgridJournalVoucher.Columns[0].Width = 538;
                dtgridJournalVoucher.Columns[1].Width = 193;
                dtgridJournalVoucher.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                //Txtsearch.Text = DtgrdBarPrch.Rows[0].Cells[0].Value.ToString();
                grdchange = 0;
                escGrid = 0;
                if (dtgridJournalVoucher.Rows.Count > 0)
                {
                    txtsearch.Text = dtgridJournalVoucher.Rows[0].Cells[0].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void FrmJournalVoucher_Load(object sender, EventArgs e)
        {
            ObjDesign.FormDesign(this, dtgridcustcode);
            ObjDesign.FormDesign(this, dtgridJournalVoucher);
            flag = 1;
            txtsearch.Focus();
            tabJournalVoucher.SelectedTab = tabList;
            FillGrid("tbTransJournalVoucher");
            txtVoucherNo.Enabled = false;
            mod.UserAccessibillityMaster("Journal Voucher", BtnAdd, BtnSave, BtnDelete);
            Globalvariable.SearchChangeVariable = "SrchByName";
        }

        private void txtAmount_Enter(object sender, EventArgs e)
        {
            txtAmount.BackColor = Color.Yellow;
        }

        private void txtAmount_Leave(object sender, EventArgs e)
        {
            txtAmount.BackColor = Color.White;
        }

        private void txtsearch_Enter(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.Yellow;
        }

        private void txtsearch_Leave(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.White;
        }

        private void FrmJournalVoucher_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Control nextTab;
                if (e.KeyChar == (char)Keys.Enter)
                {
                    nextTab = ((Control)sender);
                    if (ActiveControl.Name == "txtsearch" && dtgridJournalVoucher.Rows.Count == 0 && e.KeyChar == (char)Keys.Enter)
                    {
                        txtsearch.Focus();
                    }
                    else
                    {
                        if (ActiveControl.Name == "txtsearch" && dtgridJournalVoucher.Rows.Count == 0)
                        {
                            nextTab = BtnAdd;
                        }
                        else if (ActiveControl.Name == "txtNarration")
                        {
                            txtNarration.Focus();
                        }
                        else
                        {
                            nextTab = GetNextControl(ActiveControl, true);
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

        private void txtNarration_Enter(object sender, EventArgs e)
        {
            txtNarration.BackColor = Color.Yellow;
            string st = "SELECT DISTINCT(Narration),Voucher_No FROM tbTransJournalVoucherDetail WHERE Company_Srno='"+Globalvariable.company_Srno+"' ORDER BY Voucher_No";
           cmd=new SqlCommand(st, con.connect());
           SqlDataReader dr = cmd.ExecuteReader();
           if (dr.Read())
           {
               txtNarration.Text = dr["Narration"].ToString();
           }
           else
           {
               txtNarration.Text = "BEING AMOUNT TRANSFER TO";
           }
          
        }

        private void txtNarration_Leave(object sender, EventArgs e)
        {
            txtNarration.BackColor = Color.White;
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
           // e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == '.');
            try
            {
                e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == '.');
                if (txtAmount.Text == "" || txtAmount.Text == "0" && e.KeyChar == (char)Keys.Enter)
                {
                    txtAmount.Focus();
                }
                else if (e.KeyChar == (char)Keys.Enter && dtgridcustcode.Rows.Count == 0)
                {
                    dtgridcustcode.Enabled = true;
                    dtgridcustcode.Rows.Clear();
                    amount = Convert.ToDouble(txtAmount.Text);
                    txtAmount.Text = amount.ToString("#0.00");
                    dtgridcustcode.Rows.Add();
                    dtgridcustcode.Rows[0].Cells[0].Value = "Dr";
                    dtgridcustcode.Rows[0].Cells[6].Value = Convert.ToDouble(txtAmount.Text).ToString("#0.00");
                    dtgridcustcode.CurrentCell = dtgridcustcode[1, 0];
                    dtgridcustcode.BeginEdit(true);
                    e.Handled = true;
                    txtNarration.Text = "BEING";
                }
                else if (e.KeyChar == (char)Keys.Enter && dtgridcustcode.Rows.Count > 0)
                {
                    dtgridcustcode.Enabled = true;
                    dtgridcustcode.CurrentCell = dtgridcustcode[1, 0];
                    dtgridcustcode.BeginEdit(true);
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void tabJournalVoucher_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SqlDataReader dr, dr1;
                string str, Pdate, stmess;
                int i = 0, j = 1, sup = 0, k;
                DataTable dt = new DataTable();
                DataRow drw;

                if (tabJournalVoucher.SelectedTab == tabDetail)
                {
                    if (flag == 1)
                    {
                        if (dtgridJournalVoucher.RowCount > 0)
                        {
                            k = dtgridJournalVoucher.SelectedCells[0].RowIndex;
                            if (dtgridJournalVoucher.Rows[k].Cells[1].Value.ToString() != "0.00" && grdchange != 1)
                            {
                                i = dtgridJournalVoucher.SelectedCells[0].RowIndex;
                                dtgridJournalVoucher.Text = dtgridJournalVoucher.Rows[i].Cells[0].Value.ToString();
                                string ID = mod.Isnull(dtgridJournalVoucher.Rows[i].Cells[0].Value.ToString(), "");
                                SqlDataReader dareader = mod.GetSelectAllField(tableName, firstCol, ID, "SrNo");
                                FillDetails(dareader, "null");
                                BtnDelete.Text = "DELETE";
                                BtnSave.Text = "UPDATE";
                            }
                            else
                            {
                                MessageBox.Show("No record to display");
                                this.tabJournalVoucher.SelectedTab = tabList;
                            }
                        }
                    }
                }
                else
                {
                    FillGrid("tbTransJournalVoucher");
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        public void FillDetails(SqlDataReader dr, string strvariable)
        {
            try
            {
                int i = 0;
                if (dr.Read())
                {
                   txtVoucherNo.Text = "";
                   txtVoucherNo.Text = dr["Voucher_No"].ToString();
                    dtpVoucherDate.Text = dr["Voucher_Date"].ToString();
                    txtAmount.Text = Convert.ToDouble(dr["Amount"].ToString()).ToString("#0.00");
                    SqlDataReader dr1 = mod.GetSelectAllField("tbTransJournalVoucherDetail", "Voucher_No", txtVoucherNo.Text.ToString(), "SrNo");
                    dtgridcustcode.Rows.Clear();
                    i = 0;
                    while (dr1.Read())
                    {
                        dtgridcustcode.Rows.Add();
                        dtgridcustcode.Rows[i].Cells[0].Value = dr1["Transction_Type"].ToString();
                        dtgridcustcode.Rows[i].Cells[1].Value = dr1["Voucher_Type"].ToString();
                        dtgridcustcode.Rows[i].Cells[2].Value = dr1["Customer_Code"].ToString();
                        string[] rstr;
                        if (dtgridcustcode.Rows[i].Cells[1].Value.ToString() == "C" || dtgridcustcode.Rows[i].Cells[1].Value.ToString() == "D")
                        {
                            rstr = GetCustName(dr1["Customer_Code"].ToString(), "tbSupplierMaster");
                            dtgridcustcode.Rows[i].Cells[3].Value = rstr[0];
                        }
                        else if (dtgridcustcode.Rows[i].Cells[1].Value.ToString() == "O")
                        {
                            rstr = GetCustName(dr1["Customer_Code"].ToString(), "tbGeneralLedger");
                            dtgridcustcode.Rows[i].Cells[3].Value = rstr[0];
                        }
                        dtgridcustcode.Rows[i].Cells[4].Value = dr1["Sled_Code"].ToString();
                        string[] sled_code;
                        sled_code = GetCustName(dr1["Sled_Code"].ToString(), "tbGuestInformation");
                        dtgridcustcode.Rows[i].Cells[5].Value = sled_code[0];
                        dtgridcustcode.Rows[i].Cells[6].Value = Convert.ToDouble(dr1["Amount"].ToString()).ToString("#0.00"); 
                        txtNarration.Text = dr1["Narration"].ToString();
                        i = i + 1;
                    }
                    // txtTransNo.Focus();
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

        private string[] GetCustName(string tno,string tablename)
        {
            string[] rtstr = new string[2];
            SqlDataReader dr1 = null;
            if (tablename == "tbSupplierMaster")
            {
            dr1 = mod.GetRecord("SELECT supplier_name FROM tbSupplierMaster WHERE supplier_id='" + tno + "' AND deleted='N' AND Branchcode='" + Globalvariable.bcode + "'");
             
            }
            else if (tablename == "tbGeneralLedger")
            {
                dr1 = mod.GetRecord("SELECT genledg_desc FROM tbGeneralLedger WHERE genledg_code='" + tno + "' AND deleted='N' AND Branchcode='" + Globalvariable.bcode + "'");
            }
            else if (tablename == "tbGuestInformation")
            {
                dr1 = mod.GetRecord("SELECT Customer_name FROM tbGuestInformation WHERE Customer_id='" + tno + "' AND Deleted='N' AND Branchcode='" + Globalvariable.bcode + "'");
            }

                if (dr1.HasRows)
                {
                                dr1.Read();
                            if(tablename=="tbSupplierMaster")
                            {
                                rtstr[0] = dr1["supplier_name"].ToString();
                            }
                            else if(tablename == "tbGeneralLedger")
                            {
                                rtstr[0] = dr1["genledg_desc"].ToString();
                            }
                            else if (tablename == "tbGuestInformation")
                            {
                                rtstr[0] = dr1["Customer_name"].ToString();
                            }
                                return (rtstr);
                }
                else
                {
                    rtstr[0] = "";
                    return (rtstr);
                }
            }

        private void btnLessID_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new SqlCommand("SP_FrmTransJournalVoucher", con.connect());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@variable", "LESS_ID");
                cmd.Parameters.AddWithValue("@code", txtVoucherNo.Text);
                cmd.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                SqlDataReader sqldr = cmd.ExecuteReader();
                FillDetails(sqldr, "LESS_ID");
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void btnGreaterID_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new SqlCommand("SP_FrmTransJournalVoucher", con.connect());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@variable", "GREATER_ID");
                cmd.Parameters.AddWithValue("@code", txtVoucherNo.Text);
                cmd.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                SqlDataReader sqldr = cmd.ExecuteReader();
                FillDetails(sqldr, "GREATER_ID");
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            ExitCode = "ExitClick";
            if (grdchange == 3)
            {
                grdchange = 1;
              //  ExitGridFill();
                Fillgriddetails();
            }
            else if (grdchange == 2)
            {
                grdchange = 0;
               // Fillgriddetails();
                ExitGridFill();
            }
            else if (grdchange == 1)
            {
                grdchange = 0;
                txtsearch.Focus();
                tabJournalVoucher.SelectedTab = tabList;
                FillGrid("tbTransJournalVoucher");
                BtnAdd.Visible = true;
            }
            else if (grdchange == 0)
            {
                Close();
            }
            ExitCode = "";
        }

        void Fillgriddetails()
        {
            try
            {
                if (dtgridJournalVoucher.Rows.Count > 0)
                {
                    int mntotal, dayTot;
                    string strsql;
                    double totlamt;
                    DateTime dtm;
                    DataTable dt = new DataTable();
                    DataRow drw;
                    if (grdchange == 0)
                    {
                        int i = dtgridJournalVoucher.SelectedCells[0].RowIndex;
                      //  string cd = dtgridJournalVoucher.Rows[i].Cells[0].Value.ToString();
                        if (dtgridJournalVoucher.Rows[i].Cells[1].Value.ToString() != "0.00")
                        {
                            if (escGrid == 0 || escGrid == 1)
                            {
                                mnth = mod.Getmonthno(dtgridJournalVoucher.Rows[i].Cells[0].Value.ToString());
                            }
                            dtgridJournalVoucher.Columns.Clear();
                            dt.Columns.Add("Voucher Date", typeof(string));
                            dt.Columns.Add("Year", typeof(string));
                            dt.Columns.Add("Amount", typeof(string));
                            mntotal = 0;                           
                            cmd = new SqlCommand("SP_FrmTransJournalVoucher", con.connect());
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@variable", "SELECT");
                            cmd.Parameters.AddWithValue("@gridchangeValue", "0");
                            cmd.Parameters.AddWithValue("@month", mnth);
                            cmd.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                            cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);                      
                            SqlDataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                dayTot = 0;
                                DateTime pdate = Convert.ToDateTime(dr[0].ToString());
                                cmd = new SqlCommand("SP_FrmTransJournalVoucher", con.connect());
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@variable", "SELECT");
                                cmd.Parameters.AddWithValue("@gridchangeValue", "0");
                                cmd.Parameters.AddWithValue("@checkstring", "SUM");
                                cmd.Parameters.AddWithValue("@vouchDate", pdate.ToString("MM/dd/yyyy"));
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
                            grdchange = 1;
                            escGrid = 0;
                            dr.Close();
                            dtgridJournalVoucher.DataSource = dt;
                            dtgridJournalVoucher.Columns[0].Width = 120;
                            dtgridJournalVoucher.Columns[1].Width = 416;
                            dtgridJournalVoucher.Columns[2].Width = 195;
                            dtgridJournalVoucher.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                            if (dtgridJournalVoucher.Rows.Count > 0)
                            {
                                txtsearch.Text = dtgridJournalVoucher.Rows[0].Cells[0].Value.ToString();
                            }
                            BtnAdd.Visible = false;
                        }
                    }
                    else if (grdchange == 1)
                    {
                        int i = dtgridJournalVoucher.SelectedCells[0].RowIndex;
                        if (Convert.ToString(dtgridJournalVoucher.Rows[i].Cells[2].Value) != "0.00")
                        {
                            voch_dt = mnth + "/" + dtgridJournalVoucher.Rows[i].Cells[0].Value + "/" + dtgridJournalVoucher.Rows[i].Cells[1].Value;

                            dtgridJournalVoucher.Columns.Clear();
                            dt.Columns.Add("Voucher No", typeof(string));
                            dt.Columns.Add("Amount", typeof(string));

                            //strsql = "select * from " + strHeadTable + " where BK_CODE='" + m_bkcode + "' and Company_Sr='" + gv.companysr +
                            //         "' and voch_dt='" + voch_dt + "' and Branchcode='" + gv.Brncode + "'" + gv.advRecCriteria + "";
                            cmd = new SqlCommand("SP_FrmTransJournalVoucher", con.connect());
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@variable", "SELECT");
                            cmd.Parameters.AddWithValue("@gridchangeValue", "1");
                            cmd.Parameters.AddWithValue("@vouchDate", voch_dt);
                            cmd.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                            cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                            SqlDataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                drw = dt.NewRow();
                                drw["Voucher No"] = dr["Voucher_No"].ToString();
                                drw["Amount"] = Convert.ToDouble(dr["Amount"].ToString()).ToString("#0.00");
                                dt.Rows.Add(drw);
                            }
                            grdchange = 2;
                            escGrid = 1;
                            dr.Close();
                            dtgridJournalVoucher.DataSource = dt;
                            dtgridJournalVoucher.Columns[0].Width = 365;
                            dtgridJournalVoucher.Columns[1].Width = 367;
                            dtgridJournalVoucher.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                            if (dtgridJournalVoucher.Rows.Count > 0)
                            {
                                txtsearch.Text = dtgridJournalVoucher.Rows[0].Cells[0].Value.ToString();
                            }
                            BtnAdd.Visible = false;
                        }
                    }
                    else if (grdchange == 2)
                    {
                        flag = 1;
                        tabJournalVoucher.SelectedTab = tabDetail;
                        // dtpVoucherDate.Focus();
                        btnLessID.Visible = true;
                        btnGreaterID.Visible = true;
                        BtnDelete.Text = "DELETE";
                        BtnPrint.Enabled = true;
                    }
                    txtsearch.Focus();
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
            try
            {
                flag = 1;
                tabJournalVoucher.SelectedTab = tabList;
                grdchange = 0;
                FillGrid("tbTransJournalVoucher");
                BtnAdd.Visible = true;
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

        public void ExitGridFill()
        {
            try
            {
                if (dtgridJournalVoucher.Rows.Count > 0)
                {
                    int mntotal, dayTot;
                    string strsql;
                    double totlamt;
                    DateTime dtm;
                    DataTable dt = new DataTable();
                    DataRow drw;
                    if (grdchange == 0)
                    {
                        int i = dtgridJournalVoucher.SelectedCells[0].RowIndex;
                        string cd = dtgridJournalVoucher.Rows[i].Cells[0].Value.ToString();
                        if (dtgridJournalVoucher.Rows[i].Cells[1].Value.ToString() != "0.00")
                        {
                            //if (escGrid == 0 || escGrid == 1)
                            //{
                            //    mnth = mod.Getmonthno(dtgridJournalVoucher.Rows[i].Cells[0].Value.ToString());
                            //}
                            dtgridJournalVoucher.Columns.Clear();
                            dt.Columns.Add("Voucher Date", typeof(string));
                            dt.Columns.Add("Year", typeof(string));
                            dt.Columns.Add("Amount", typeof(string));
                            mntotal = 0;
                            //SELECT Voucher_Date FROM tbTransJournalVoucher WHERE Company_Srno='' AND Voucher_No='' AND Branchcode='';
                            cmd = new SqlCommand("SP_FrmTransJournalVoucher", con.connect());
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@variable", "SELECT");
                            cmd.Parameters.AddWithValue("@gridchangeValue", "0");
                            cmd.Parameters.AddWithValue("@exitcode", "ExitCode");
                            cmd.Parameters.AddWithValue("@code", cd);
                            cmd.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                            cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);                                             
                            SqlDataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                dayTot = 0;
                                DateTime pdate = Convert.ToDateTime(dr[0].ToString());
                                cmd = new SqlCommand("SP_FrmTransJournalVoucher", con.connect());
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@variable", "SELECT");
                                cmd.Parameters.AddWithValue("@gridchangeValue", "0");
                                cmd.Parameters.AddWithValue("@checkstring", "SUM");
                                cmd.Parameters.AddWithValue("@vouchDate", pdate.ToString("MM/dd/yyyy"));
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
                            grdchange = 1;
                            escGrid = 0;
                            dr.Close();
                            dtgridJournalVoucher.DataSource = dt;
                            dtgridJournalVoucher.Columns[0].Width = 120;
                            dtgridJournalVoucher.Columns[1].Width = 416;
                            dtgridJournalVoucher.Columns[2].Width = 195;
                            dtgridJournalVoucher.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                            if (dtgridJournalVoucher.Rows.Count > 0)
                            {
                                txtsearch.Text = dtgridJournalVoucher.Rows[0].Cells[0].Value.ToString();
                            }
                            BtnAdd.Visible = false;
                        }
                    }                                    
                    txtsearch.Focus();
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }


        public void add()
        {
            grdchange = 0;
            flag = 0;
            tabJournalVoucher.SelectedTab = tabDetail;
            mod.addBtnClick(this, txtVoucherNo, tableName, firstCol, BtnSave, BtnDelete, txtAmount);
            mod.txtclear(tabJournalVoucher.TabPages[1].Controls);
            dtgridcustcode.Rows.Clear();
            mod.UserAccessibillityMaster("Journal Voucher", BtnAdd, BtnSave, BtnDelete);
            txtVoucherNo.Text = "" + mod.GetMaxNum(tableName, firstCol);
            BtnPrint.Enabled = false;
            btnGreaterID.Visible = false;
            btnLessID.Visible = false;
            dtpVoucherDate.Focus();
            BtnDelete.Text = "RESET";
            //  flag = 0;
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            mod.transSearch(txtsearch, dtgridJournalVoucher, 0);
        }

        private void txtsearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && txtsearch.Text == "")
            {
                BtnAdd.Focus();
            }
        }

        private void txtsearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                dtgridJournalVoucher_KeyDown(sender, e);
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void dtgridJournalVoucher_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Fillgriddetails();
        }

        private void dtgridJournalVoucher_KeyDown(object sender, KeyEventArgs e)
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
                        i = dtgridJournalVoucher.SelectedCells[0].RowIndex + 1;
                    else if (e.KeyCode == Keys.Up)
                        i = dtgridJournalVoucher.SelectedCells[0].RowIndex - 1;

                    if (((e.KeyCode == Keys.Down) && (i < dtgridJournalVoucher.RowCount)) || ((e.KeyCode == Keys.Up) && (i >= 0)))
                    {
                        dtgridJournalVoucher.Refresh();
                        dtgridJournalVoucher.CurrentCell = dtgridJournalVoucher.Rows[i].Cells[0];
                        dtgridJournalVoucher.Rows[i].Selected = true;
                        if (dtgridJournalVoucher.Rows.Count > 0)
                        {
                            txtsearch.Text = dtgridJournalVoucher.Rows[i].Cells[0].Value.ToString();
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

        private void dtgridcustcode_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            dtgridcustcode[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.Yellow;
        }

        private void dtgridcustcode_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            dtgridcustcode[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.White;
        }

        private void dtgridcustcode_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                e.Control.PreviewKeyDown -= Control_PreviewKeyDown;
                e.Control.PreviewKeyDown += new PreviewKeyDownEventHandler(Control_PreviewKeyDown);

                e.Control.GotFocus -= dataGridViewTextBox_GotFocus;
                e.Control.GotFocus += new EventHandler(dataGridViewTextBox_GotFocus);

                tb = (DataGridViewTextBoxEditingControl)e.Control;
                tb.KeyPress += new KeyPressEventHandler(dataGridViewTextBox_KeyPress);
                e.Control.KeyPress += new KeyPressEventHandler(dataGridViewTextBox_KeyPress);
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void Control_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (dtgridcustcode.Rows.Count < 3)
                {

                    string str = "";
                    BtnSave.Enabled = false;
                    DupCode1 = "0";
                    rowNo = dtgridcustcode.CurrentCell.RowIndex;
                    colNo = dtgridcustcode.CurrentCell.ColumnIndex;
                    if (colNo == 1)
                    {
                        if (e.KeyCode == Keys.Enter && tb.Text != string.Empty)
                        {
                            dtgridcustcode.CurrentCell = dtgridcustcode[2, rowNo];
                            e.IsInputKey = true;
                            if (dtgridcustcode.Rows[rowNo].Cells[1].Value.ToString() == "c")
                            {
                                dtgridcustcode.Rows[rowNo].Cells[1].Value = "C";
                            }
                            else if (dtgridcustcode.Rows[rowNo].Cells[1].Value.ToString() == "d")
                            {
                                dtgridcustcode.Rows[rowNo].Cells[1].Value = "D";
                            }
                            else if (dtgridcustcode.Rows[rowNo].Cells[1].Value.ToString() == "o")
                            {
                                dtgridcustcode.Rows[rowNo].Cells[1].Value = "O";
                            }
                        }
                        else if (e.KeyCode == Keys.Enter && tb.Text == string.Empty)
                        {
                            dtgridcustcode.CurrentCell = dtgridcustcode[1, rowNo];
                           // dtgridcustcode.BeginEdit(true);
                        }
                    }
                    else if (colNo == 2)
                    {
                        // string str;
                        if (e.KeyCode == Keys.Back)
                        {
                            dtgridcustcode.Rows[rowNo].Cells[2].Value = null;
                        }
                        else if (dtgridcustcode.Rows[rowNo].Cells[1].Value != null)
                        {
                            #region Enter Id in datagridText
                            if (e.KeyCode == Keys.Enter && tb.Text != string.Empty)
                            {                              
                                    DupCode1 = mod.DuplicateRecord(dtgridcustcode, 2,rowNo);
                                    Globalvariable.DuplicateValue = DupCode1;
                                    dtgridcustcode.Rows[rowNo].Cells[2].Value = tb.Text;
                                    if (tb.Text != string.Empty && dtgridcustcode.Rows[rowNo].Cells[2].Value != null)
                                    {
                                        int i;
                                        int itemn = Convert.ToInt32(dtgridcustcode.Rows[rowNo].Cells[2].Value);
                                        chkDupFlag = false;
                                        i = mod.getDuplicateRowNo(itemn, dtgridcustcode, 2, rowNo);
                                        if (i == -1)
                                        {
                                            code = dtgridcustcode.Rows[rowNo].Cells[2].Value.ToString();
                                            if (dtgridcustcode.Rows[rowNo].Cells[1].Value.ToString() == "C")
                                            {
                                                str = "SELECT supplier_name,supplier_id FROM tbSupplierMaster WHERE supplier_id='" + code + "' AND supplier_id NOT IN(" + DupCode1 + ") AND deleted='N' AND cust_type='C'  AND Branchcode='" + Globalvariable.bcode + "'";
                                            }
                                            else if (dtgridcustcode.Rows[rowNo].Cells[1].Value.ToString() == "D")
                                            {
                                                str = "SELECT supplier_name,supplier_id FROM tbSupplierMaster WHERE supplier_id='" + code + "' AND supplier_id NOT IN(" + DupCode1 + ") AND deleted='N' AND cust_type='D'  AND Branchcode='" + Globalvariable.bcode + "'";
                                            }
                                            else if (dtgridcustcode.Rows[rowNo].Cells[1].Value.ToString() == "O")
                                            {
                                                str = "SELECT genledg_desc,genledg_code FROM tbGeneralLedger WHERE sub_led='N' AND genledg_code NOT IN (select gen_leg_code from tbBook where deleted='N')AND deleted = 'N' AND genledg_code='" + code + "'";
                                            }
                                            SqlDataReader dr1 = mod.GetRecord(str);
                                            if (dr1.Read())
                                            {
                                                tb.Text = "";
                                                dtgridcustcode.ClearSelection();
                                                dtgridcustcode.CurrentCell = dtgridcustcode[2, rowNo];
                                               // dtgridcustcode.BeginEdit(true);
                                                if (dtgridcustcode.Rows[rowNo].Cells[1].Value.ToString() == "C")
                                                {
                                                    dtgridcustcode.Rows[rowNo].Cells[2].Value = dr1["supplier_id"].ToString();
                                                    dtgridcustcode.Rows[rowNo].Cells[3].Value = dr1["supplier_name"].ToString();
                                                    tb.Text = dr1["supplier_id"].ToString();
                                                }
                                                else if (dtgridcustcode.Rows[rowNo].Cells[1].Value.ToString() == "D")
                                                {
                                                    dtgridcustcode.Rows[rowNo].Cells[2].Value = dr1["supplier_id"].ToString();
                                                    dtgridcustcode.Rows[rowNo].Cells[3].Value = dr1["supplier_name"].ToString();
                                                    tb.Text = dr1["supplier_id"].ToString();
                                                }
                                                else if (dtgridcustcode.Rows[rowNo].Cells[1].Value.ToString() == "O")
                                                {
                                                    dtgridcustcode.Rows[rowNo].Cells[2].Value = dr1["genledg_code"].ToString();
                                                    dtgridcustcode.Rows[rowNo].Cells[3].Value = dr1["genledg_desc"].ToString();
                                                    tb.Text = dr1["genledg_code"].ToString();
                                                }
                                                //Add row in grid
                                                if (dtgridcustcode.Rows[rowNo].Cells[1].Value.ToString() == "D" || dtgridcustcode.Rows[rowNo].Cells[1].Value.ToString() == "C")
                                                {
                                                    dtgridcustcode.Enabled = true;
                                                    dtgridcustcode.CurrentCell = dtgridcustcode[4, rowNo];
                                                  //  dtgridcustcode.BeginEdit(true);
                                                }
                                                else
                                                {
                                                    dtgridcustcode.Enabled = true;
                                                    dtgridcustcode.CurrentCell = dtgridcustcode[6, rowNo];
                                                    dtgridcustcode.BeginEdit(true);
                                                }
                                            }
                                            else
                                            {
                                                chkDupFlag = true;
                                                dtgridcustcode.Rows[rowNo].Cells[2].Value = "";
                                                tb.Text = "";
                                                //dtgridcustcode.ClearSelection();
                                                dtgridcustcode.CurrentCell = dtgridcustcode[2, rowNo];
                                              //  dtgridcustcode.BeginEdit(true);
                                            }
                                        }
                                        else
                                        {
                                            chkDupFlag = true;
                                            dtgridcustcode.Rows[rowNo].Cells[2].Value = "";
                                            tb.Text = "";
                                            dtgridcustcode.CurrentCell = dtgridcustcode[2, rowNo];
                                        }
                                    }                               
                            }
                            #endregion

                            #region Enter Key Press And Show Search from
                            else if (tb.Text == string.Empty && e.KeyCode == Keys.Enter)
                            {
                                Globalvariable.searchNo = 1;
                                KeysConverter kc = new KeysConverter();
                                string key = kc.ConvertToString(e.KeyCode);
                                FrmSearch srch = new FrmSearch();
                                //  DupCode1 = "0";
                                DupCode1 = mod.DuplicateRecord(dtgridcustcode, 2,rowNo);
                                Globalvariable.DuplicateValue = DupCode1;
                                if (dtgridcustcode.Rows[rowNo].Cells[1].Value.ToString() == "C")
                                {
                                    Globalvariable.SearchString = "SupplierMaster";
                                    Globalvariable.Cust_Type = "C";
                                    // str = "SELECT supplier_name,supplier_id FROM tbSupplierMaster WHERE supplier_id NOT IN(" + DupCode1 + ") AND deleted='N' AND cust_type='C'  AND Branchcode='" + Globalvariable.bcode + "' ORDER BY supplier_id";
                                    srchda = SP_FillSearchGrid("tbSupplierMaster", DupCode1, "C");
                                }
                                else if (dtgridcustcode.Rows[rowNo].Cells[1].Value.ToString() == "D")
                                {
                                    Globalvariable.SearchString = "SupplierMaster";
                                    Globalvariable.Cust_Type = "D";
                                    //  str = "SELECT supplier_name,supplier_id FROM tbSupplierMaster WHERE supplier_id NOT IN(" + DupCode1 + ") AND deleted='N' AND cust_type='D'  AND Branchcode='" + Globalvariable.bcode + "' ORDER BY supplier_id";
                                    srchda = SP_FillSearchGrid("tbSupplierMaster", DupCode1, "D");
                                }
                                else if (dtgridcustcode.Rows[rowNo].Cells[1].Value.ToString() == "O")
                                {
                                    Globalvariable.SearchString = "GeneralLedger";
                                    // str = "SELECT genledg_desc,genledg_code FROM tbGeneralLedger WHERE sub_led='N' AND genledg_code NOT IN (select gen_leg_code from tbBook where deleted='N')AND deleted = 'N' ORDER BY genledg_code";
                                    srchda = mod.SearchFrmQuery("tbGeneralLedger", "genledg_code", "genledg_desc");
                                }
                                srch.val = key;
                                srch.fillbackgridSrch(srchda, "FrmJournalVoucher");
                                ds.Clear();
                                srchda.Fill(ds);
                                if (ds.Tables[0].Rows.Count > 0)
                                    srch.ShowDialog();
                                if (srch.codeselected != null)
                                {
                                    tb.Text = "";
                                    dtgridcustcode.ClearSelection();
                                    dtgridcustcode.CurrentCell = dtgridcustcode[2, rowNo];
                                    dtgridcustcode.Rows[rowNo].Cells[2].Value = mod.Isnull(srch.codeselected, "");
                                    dtgridcustcode.Rows[rowNo].Cells[3].Value = mod.Isnull(srch.descselected, "");
                                    if (dtgridcustcode.Rows[rowNo].Cells[1].Value.ToString() == "D" || dtgridcustcode.Rows[rowNo].Cells[1].Value.ToString() == "C")
                                    {
                                        dtgridcustcode.Enabled = true;
                                        dtgridcustcode.CurrentCell = dtgridcustcode[4, rowNo];
                                       // dtgridcustcode.BeginEdit(true);
                                    }
                                    else
                                    {
                                        dtgridcustcode.Enabled = true;
                                        dtgridcustcode.CurrentCell = dtgridcustcode[6, rowNo];
                                        dtgridcustcode.BeginEdit(true);
                                    }
                                }
                                else
                                {
                                    dtgridcustcode.CurrentCell = dtgridcustcode[2, rowNo];
                                    //dtgridcustcode.BeginEdit(true);
                                    tb.Focus();
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            dtgridcustcode.Enabled = true;
                            dtgridcustcode.CurrentCell = dtgridcustcode[1, rowNo];
                            dtgridcustcode.BeginEdit(true);
                        }
                    }
                    else if (colNo == 4)
                    {
                        // string str;
                        if (e.KeyCode == Keys.Back)
                        {
                            dtgridcustcode.Rows[rowNo].Cells[4].Value = null;
                        }
                        else if (dtgridcustcode.Rows[rowNo].Cells[1].Value.ToString() == "D" || dtgridcustcode.Rows[rowNo].Cells[1].Value.ToString() == "C")
                        {
                            #region Enter Id in datagridText
                            if (e.KeyCode == Keys.Enter && tb.Text != string.Empty)
                            {
                                {
                                    DupCode1 = mod.DuplicateRecord(dtgridcustcode, 4,rowNo);
                                    Globalvariable.DuplicateValue = DupCode1;
                                    dtgridcustcode.Rows[rowNo].Cells[4].Value = tb.Text;
                                    if (tb.Text != string.Empty && dtgridcustcode.Rows[rowNo].Cells[4].Value != null)
                                    {
                                        int i;
                                        int itemn = Convert.ToInt32(dtgridcustcode.Rows[rowNo].Cells[4].Value);
                                        chkDupFlag = false;
                                        i = mod.getDuplicateRowNo(itemn, dtgridcustcode, 4, rowNo);
                                        if (i == -1)
                                        {
                                            code = dtgridcustcode.Rows[rowNo].Cells[4].Value.ToString();

                                            str = "SELECT Customer_name,Customer_id FROM tbGuestInformation WHERE Customer_id='" + code + "' AND Leger_id='" + dtgridcustcode.Rows[rowNo].Cells[2].Value+ "' AND deleted='N' AND Branchcode='" + Globalvariable.bcode + "'";                                            
                                            SqlDataReader dr1 = mod.GetRecord(str);
                                            if (dr1.Read())
                                            {
                                                tb.Text = "";
                                                dtgridcustcode.ClearSelection();
                                                dtgridcustcode.CurrentCell = dtgridcustcode[4, rowNo];
                                              //  dtgridcustcode.BeginEdit(true);
                                                dtgridcustcode.Rows[rowNo].Cells[4].Value = dr1["Customer_id"].ToString();
                                                dtgridcustcode.Rows[rowNo].Cells[5].Value = dr1["Customer_name"].ToString();
                                                tb.Text = dr1["Customer_id"].ToString();                                                                                   
                                                //Add row in grid                                               
                                                    dtgridcustcode.Enabled = true;
                                                    dtgridcustcode.CurrentCell = dtgridcustcode[6, rowNo];                                                
                                            }
                                            else
                                            {
                                                chkDupFlag = true;
                                                dtgridcustcode.Rows[rowNo].Cells[4].Value = "";
                                                tb.Text = "";
                                                //dtgridcustcode.ClearSelection();
                                                dtgridcustcode.CurrentCell = dtgridcustcode[4, rowNo];
                                                //dtgridcustcode.BeginEdit(true);
                                            }
                                        }
                                        else
                                        {
                                            chkDupFlag = true;
                                            dtgridcustcode.Rows[rowNo].Cells[4].Value = "";
                                            tb.Text = "";
                                            //dtgridcustcode.ClearSelection();
                                            dtgridcustcode.CurrentCell = dtgridcustcode[4, rowNo];
                                           // dtgridcustcode.BeginEdit(true);
                                        }
                                    }
                                }
                            }
                            #endregion

                            #region Enter Key Press And Show Search from
                            else if (tb.Text == string.Empty && e.KeyCode == Keys.Enter)
                            {
                                Globalvariable.SearchString = "GuestInformation";
                                KeysConverter kc = new KeysConverter();
                                string key = kc.ConvertToString(e.KeyCode);
                                FrmSearch srch = new FrmSearch();
                                    // DupCode1=ledger id in dtgridcustcode                              
                                   DupCode1= dtgridcustcode.Rows[rowNo].Cells[2].Value.ToString();
                                    srchda = SP_FillSearchGrid("tbGuestInformation", DupCode1, "0");                                  
                                srch.val = key;
                                srch.fillbackgridSrch(srchda, "FrmJournalVoucher");
                                ds.Clear();
                                srchda.Fill(ds);
                                if (ds.Tables[0].Rows.Count > 0)
                                    srch.ShowDialog();
                                if (srch.codeselected != null)
                                {
                                    tb.Text = "";
                                    dtgridcustcode.ClearSelection();
                                    dtgridcustcode.CurrentCell = dtgridcustcode[4, rowNo];
                                    dtgridcustcode.Rows[rowNo].Cells[4].Value = mod.Isnull(srch.codeselected, "");
                                    dtgridcustcode.Rows[rowNo].Cells[5].Value = mod.Isnull(srch.descselected, "");
                                    dtgridcustcode.Enabled = true;
                                    dtgridcustcode.CurrentCell = dtgridcustcode[6, rowNo];
                                   
                                }
                                else
                                {
                                    dtgridcustcode.CurrentCell = dtgridcustcode[4, rowNo];
                                  //  dtgridcustcode.BeginEdit(true);
                                    tb.Focus();
                                }
                            }
                            #endregion
                        }
                        else
                        {                        
                                dtgridcustcode.Enabled = true;
                                dtgridcustcode.CurrentCell = dtgridcustcode[6, rowNo];
                                dtgridcustcode.BeginEdit(true);
                        }                   
                    }
                    else if(colNo==6)
                    {
                        if (e.KeyCode == Keys.Enter)
                        {
                            if (rowNo == 1)
                            {
                                txtNarration.Focus();
                            }
                            else if (rowNo == 0 && dtgridcustcode.Rows.Count < 2)
                            {
                                dtgridcustcode.Rows.Add();
                                dtgridcustcode.Rows[0].Cells[0].Value = "Cr";
                                dtgridcustcode.Rows[0].Cells[6].Value = Convert.ToDouble(txtAmount.Text).ToString("#0.00");
                                dtgridcustcode.CurrentCell = dtgridcustcode[1, 0];
                                dtgridcustcode.BeginEdit(true);
                            }
                            else if (dtgridcustcode.Rows.Count == 2)
                            {
                                dtgridcustcode.CurrentCell = dtgridcustcode[1, 1];
                                dtgridcustcode.BeginEdit(true);
                            }
                        }
                    }
                 //   e.IsInputKey = true;
                }
                else
                {
                    txtNarration.Focus();
                }
             }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }

        }

        public SqlDataAdapter SP_FillSearchGrid(string tablename, string duplicatecode,string CustType)
        {
            cmd = new SqlCommand("SP_FillSearchGrid", con.connect());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@tableName", tablename);
            cmd.Parameters.AddWithValue("@duplicateCode", duplicatecode);
            cmd.Parameters.AddWithValue("@Custtype", CustType);
            cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
            SqlDataAdapter da =new SqlDataAdapter(cmd);
            return da;
        }

        private void dataGridViewTextBox_GotFocus(object sender, EventArgs e)
        {
            TextBox obj = sender as TextBox;
            obj.BackColor = Color.Yellow;
            obj.Focus();
        }

        private void dataGridViewTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                TextBox obj = sender as TextBox;
                colNo = dtgridcustcode.CurrentCell.ColumnIndex;
                rowNo = dtgridcustcode.CurrentCell.RowIndex;
                                                                                                                                                                                                                                                   
                if (colNo == 1)
                {
                    if ((e.KeyChar == 67 || e.KeyChar == 68 || e.KeyChar == 79) || (e.KeyChar == 99 || e.KeyChar == 100 || e.KeyChar == 111 || e.KeyChar == 8))
                    {
                        if (obj.Text.ToString().Length >= 1 && (Keys)e.KeyChar != Keys.Back)
                        {
                            e.Handled = true;
                        }
                        else
                        {
                            e.Handled = false;
                        }
                    }
                    else
                    {
                        if (!(e.KeyChar == 8 || e.KeyChar == 13))
                        {
                            e.Handled = true;
                        }
                    }
                }
                else if (colNo == 2 || colNo == 3 || colNo == 6)
                {
                    e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == '.');               
                }           
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void txtNarration_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                BtnSave.Enabled = true;
                BtnSave.Focus();
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (BtnDelete.Text == "DELETE")
                {
                    DialogResult mydialog;
                    mydialog = MessageBox.Show("Are you Sure you want to delete this record", "", MessageBoxButtons.YesNo);
                    if (mydialog == DialogResult.Yes)
                    {
                        cmd = new SqlCommand("SP_FrmTransJournalVoucher", con.connect());
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@variable", "DELETE");
                        cmd.Parameters.AddWithValue("@code", txtVoucherNo.Text);
                        cmd.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                        cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                        cmd.ExecuteNonQuery();
                        txtsearch.Focus();
                        tabJournalVoucher.SelectedTab = tabList;
                        //FillGrid("tbTransJournalVoucher");
                        //BtnAdd.Visible = true;
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

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string str;
                SqlCommand cmd;
                SqlCommand command = new SqlCommand();
                DateTime ST_date = Convert.ToDateTime(Globalvariable.StartDate);
                DateTime END_date = Convert.ToDateTime(Globalvariable.EndDate);
                if (dtpVoucherDate.Value < ST_date || dtpVoucherDate.Value > END_date)
                {
                    MessageBox.Show("Voch Date Does Not Come In Financial Year.");
                    dtpVoucherDate.Focus();
                }
                else if (txtAmount.Text == "")
                {
                    txtAmount.Focus();
                }
                else if (dtgridcustcode.Rows.Count == 2)
                {
                    if (dtgridcustcode.Rows[0].Cells[6].Value.ToString() != txtAmount.Text)
                    {
                        txtAmount.Focus();
                    }
                    else if (dtgridcustcode.Rows[1].Cells[6].Value.ToString() != txtAmount.Text)
                    {
                        txtAmount.Focus();
                    }
                    else
                    {
                        cmd = new SqlCommand("SP_FrmTransJournalVoucher", con.connect());
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (BtnSave.Text == "SAVE")
                        {
                            txtVoucherNo.Text = "" + mod.GetMaxNum(tableName, firstCol);
                            cmd.Parameters.AddWithValue("@variable", "INSERT");
                        }
                        else
                        {
                            str = "DELETE FROM tbTransJournalVoucherDetail WHERE Voucher_No=" + txtVoucherNo.Text + " AND Company_Srno='" + Globalvariable.company_Srno + "'AND Branchcode='" + Globalvariable.bcode + "'";
                            //command.CommandText = str;
                            command = new SqlCommand(str, con.connect());
                            command.ExecuteNonQuery();
                            cmd.Parameters.AddWithValue("@variable", "UPDATE");
                        }
                        cmd.Parameters.AddWithValue("@code", txtVoucherNo.Text);
                        cmd.Parameters.AddWithValue("@Voucherdate", dtpVoucherDate.Value.ToShortDateString());
                        cmd.Parameters.AddWithValue("@TotalAmount", mod.Isnull(txtAmount.Text, "0"));
                        cmd.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                        cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                        cmd.ExecuteNonQuery();

                        for (int cnt = 0; cnt < dtgridcustcode.Rows.Count; cnt++)
                        {
                            if (dtgridcustcode.Rows[cnt].Cells[0].Value == null)
                            {
                                dtgridcustcode.Rows.RemoveAt(cnt);
                            }
                            else
                            {
                                SqlCommand cmdGlcode = new SqlCommand("SP_FrmTransJournalVoucher", con.connect());
                                cmdGlcode.CommandType = CommandType.StoredProcedure;
                                cmdGlcode.Parameters.AddWithValue("@variable", "INSERTJournalVoucherDetail");
                                cmdGlcode.Parameters.AddWithValue("@code", txtVoucherNo.Text);
                                cmdGlcode.Parameters.AddWithValue("@Voucherdate", dtpVoucherDate.Value.ToShortDateString());
                                cmdGlcode.Parameters.AddWithValue("@transtype", dtgridcustcode.Rows[cnt].Cells[0].Value.ToString());
                                cmdGlcode.Parameters.AddWithValue("@vouchertype", dtgridcustcode.Rows[cnt].Cells[1].Value);
                                cmdGlcode.Parameters.AddWithValue("@custCode", dtgridcustcode.Rows[cnt].Cells[2].Value);
                                cmdGlcode.Parameters.AddWithValue("@narration", mod.Isnull(txtNarration.Text, "-"));
                                cmdGlcode.Parameters.AddWithValue("@TotalAmount", dtgridcustcode.Rows[cnt].Cells[6].Value);
                                if (dtgridcustcode.Rows[cnt].Cells[4].Value == null)
                                {
                                    dtgridcustcode.Rows[cnt].Cells[4].Value = 0;
                                    cmdGlcode.Parameters.AddWithValue("@legerCode", dtgridcustcode.Rows[cnt].Cells[4].Value);
                                }
                                else
                                {
                                    cmdGlcode.Parameters.AddWithValue("@legerCode", dtgridcustcode.Rows[cnt].Cells[4].Value);
                                }
                                cmdGlcode.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                                cmdGlcode.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                                cmdGlcode.ExecuteNonQuery();
                            }
                        }
                        mod.txtclear(tabJournalVoucher.TabPages[1].Controls);
                        BtnSave.Text = "SAVE";
                        dtgridcustcode.Rows.Clear();
                        txtVoucherNo.Text = "" + mod.GetMaxNum(tableName, firstCol);
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

        private void dtgridcustcode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                
                if(dtgridcustcode.Rows.Count<3)
                {
                if (e.KeyCode == Keys.Enter)
                {
                    rowNo = dtgridcustcode.CurrentCell.RowIndex;
                    colNo = dtgridcustcode.CurrentCell.ColumnIndex;
                    //if (dtgridcustcode.Rows[rowNo].Cells[colNo].Value != null && dtgridcustcode.Rows[rowNo].Cells[colNo].Value.ToString() != "")
                    //{
                        #region Colm1
                        if (colNo == 1)
                        {
                            if (tb.Text != string.Empty)
                            {
                                if (dtgridcustcode.Rows[rowNo].Cells[1].Value.ToString() == "c")
                                {
                                    dtgridcustcode.Rows[rowNo].Cells[1].Value = "C";
                                }
                                else if (dtgridcustcode.Rows[rowNo].Cells[1].Value.ToString() == "d")
                                {
                                    dtgridcustcode.Rows[rowNo].Cells[1].Value = "D";
                                }
                                else if (dtgridcustcode.Rows[rowNo].Cells[1].Value.ToString() == "o")
                                {
                                    dtgridcustcode.Rows[rowNo].Cells[1].Value = "O";
                                }
                                dtgridcustcode.CurrentCell = dtgridcustcode[2, rowNo];
                                //dtgridcustcode.BeginEdit(true);
                                e.Handled = true;
                            }
                            else
                            {
                                dtgridcustcode.CurrentCell = dtgridcustcode[1, rowNo];
                                //  dtgridcustcode.BeginEdit(true);
                                e.Handled = true;
                            }
                        }
                        #endregion
                        #region colm2
                        else if (colNo == 2)
                        {
                           // string str = "";
                            if (e.KeyCode == Keys.Back)
                            {
                                dtgridcustcode.Rows[rowNo].Cells[2].Value = null;
                            }
                            #region select data using id
                            else if (dtgridcustcode.Rows[rowNo].Cells[2].Value != null && dtgridcustcode.Rows[rowNo].Cells[2].Value.ToString()!= "")
                            {
                                code = dtgridcustcode.Rows[rowNo].Cells[2].Value.ToString();
                                //    dtgridcustcode.CurrentCell = dtgridcustcode[4, rowNo];
                                if (dtgridcustcode.Rows[rowNo].Cells[1].Value.ToString() == "C")
                                {
                                    str = "SELECT supplier_name,supplier_id FROM tbSupplierMaster WHERE supplier_id='" + code + "' AND supplier_id NOT IN(" + DupCode1 + ") AND deleted='N' AND cust_type='C'  AND Branchcode='" + Globalvariable.bcode + "'";
                                }
                                else if (dtgridcustcode.Rows[rowNo].Cells[1].Value.ToString() == "D")
                                {
                                    str = "SELECT supplier_name,supplier_id FROM tbSupplierMaster WHERE supplier_id='" + code + "' AND supplier_id NOT IN(" + DupCode1 + ") AND deleted='N' AND cust_type='D'  AND Branchcode='" + Globalvariable.bcode + "'";
                                }
                                else if (dtgridcustcode.Rows[rowNo].Cells[1].Value.ToString() == "O")
                                {
                                    str = "SELECT genledg_desc,genledg_code FROM tbGeneralLedger WHERE sub_led='N' AND genledg_code NOT IN (select gen_leg_code from tbBook where deleted='N')AND deleted = 'N' AND genledg_code='" + code + "'";
                                }
                                SqlDataReader dr1 = mod.GetRecord(str);
                                if (dr1.Read())
                                {
                                    if (dtgridcustcode.Rows[rowNo].Cells[1].Value.ToString() == "C")
                                    {
                                        dtgridcustcode.Rows[rowNo].Cells[2].Value = dr1["supplier_id"].ToString();
                                        dtgridcustcode.Rows[rowNo].Cells[3].Value = dr1["supplier_name"].ToString();
                                        tb.Text = dr1["supplier_id"].ToString();
                                    }
                                    else if (dtgridcustcode.Rows[rowNo].Cells[1].Value.ToString() == "D")
                                    {
                                        dtgridcustcode.Rows[rowNo].Cells[2].Value = dr1["supplier_id"].ToString();
                                        dtgridcustcode.Rows[rowNo].Cells[3].Value = dr1["supplier_name"].ToString();
                                        tb.Text = dr1["supplier_id"].ToString();
                                    }
                                    else if (dtgridcustcode.Rows[rowNo].Cells[1].Value.ToString() == "O")
                                    {
                                        dtgridcustcode.Rows[rowNo].Cells[2].Value = dr1["genledg_code"].ToString();
                                        dtgridcustcode.Rows[rowNo].Cells[3].Value = dr1["genledg_desc"].ToString();
                                        tb.Text = dr1["genledg_code"].ToString();
                                    }
                                    if (tb.Text != string.Empty && dtgridcustcode.Rows[rowNo].Cells[4].Value.ToString() != "0")
                                    {
                                        dtgridcustcode.CurrentCell = dtgridcustcode[4, rowNo];
                                        //  dtgridcustcode.BeginEdit(true);
                                        e.Handled = true;
                                    }
                                    else
                                    {
                                        dtgridcustcode.CurrentCell = dtgridcustcode[6, rowNo];
                                        //  dtgridcustcode.BeginEdit(true);
                                        e.Handled = true;
                                    }
                                }
                                else
                                {
                                    dtgridcustcode.Rows[rowNo].Cells[2].Value = "";
                                    dtgridcustcode.Rows[rowNo].Cells[3].Value = "";
                                    dtgridcustcode.CurrentCell = dtgridcustcode[2, rowNo];
                                    e.Handled = true;
                                }
                            }
                            #endregion
                            #region Show Search From

                            else if (dtgridcustcode.Rows[rowNo].Cells[2].Value == null && e.KeyCode == Keys.Enter)
                            {
                                Globalvariable.searchNo = 1;
                                KeysConverter kc = new KeysConverter();
                                string key = kc.ConvertToString(e.KeyCode);
                                FrmSearch srch = new FrmSearch();
                                //  DupCode1 = "0";
                                DupCode1 = mod.DuplicateRecord(dtgridcustcode, 2,rowNo);
                                Globalvariable.DuplicateValue = DupCode1;
                                if (dtgridcustcode.Rows[rowNo].Cells[1].Value.ToString() == "C")
                                {
                                    Globalvariable.SearchString = "SupplierMaster";
                                    Globalvariable.Cust_Type = "C";
                                    // str = "SELECT supplier_name,supplier_id FROM tbSupplierMaster WHERE supplier_id NOT IN(" + DupCode1 + ") AND deleted='N' AND cust_type='C'  AND Branchcode='" + Globalvariable.bcode + "' ORDER BY supplier_id";
                                    srchda = SP_FillSearchGrid("tbSupplierMaster", DupCode1, "C");
                                }
                                else if (dtgridcustcode.Rows[rowNo].Cells[1].Value.ToString() == "D")
                                {
                                    Globalvariable.SearchString = "SupplierMaster";
                                    Globalvariable.Cust_Type = "D";
                                    //  str = "SELECT supplier_name,supplier_id FROM tbSupplierMaster WHERE supplier_id NOT IN(" + DupCode1 + ") AND deleted='N' AND cust_type='D'  AND Branchcode='" + Globalvariable.bcode + "' ORDER BY supplier_id";
                                    srchda = SP_FillSearchGrid("tbSupplierMaster", DupCode1, "D");
                                }
                                else if (dtgridcustcode.Rows[rowNo].Cells[1].Value.ToString() == "O")
                                {
                                    Globalvariable.SearchString = "GeneralLedger";
                                    // str = "SELECT genledg_desc,genledg_code FROM tbGeneralLedger WHERE sub_led='N' AND genledg_code NOT IN (select gen_leg_code from tbBook where deleted='N')AND deleted = 'N' ORDER BY genledg_code";
                                    srchda = mod.SearchFrmQuery("tbGeneralLedger", "genledg_code", "genledg_desc");
                                }
                                srch.val = key;
                                srch.fillbackgridSrch(srchda, "FrmJournalVoucher");
                                ds.Clear();
                                srchda.Fill(ds);
                                if (ds.Tables[0].Rows.Count > 0)
                                    srch.ShowDialog();
                                if (srch.codeselected != null)
                                {
                                    tb.Text = "";
                                    dtgridcustcode.ClearSelection();
                                    dtgridcustcode.CurrentCell = dtgridcustcode[2, rowNo];
                                    dtgridcustcode.Rows[rowNo].Cells[2].Value = mod.Isnull(srch.codeselected, "");
                                    dtgridcustcode.Rows[rowNo].Cells[3].Value = mod.Isnull(srch.descselected, "");
                                    if (dtgridcustcode.Rows[rowNo].Cells[1].Value.ToString() == "D" || dtgridcustcode.Rows[rowNo].Cells[1].Value.ToString() == "C")
                                    {
                                        dtgridcustcode.Enabled = true;
                                        dtgridcustcode.CurrentCell = dtgridcustcode[4, rowNo];
                                        // dtgridcustcode.BeginEdit(true);
                                    }
                                    else
                                    {
                                        dtgridcustcode.Enabled = true;
                                        dtgridcustcode.CurrentCell = dtgridcustcode[6, rowNo];
                                        dtgridcustcode.BeginEdit(true);
                                    }
                                }
                                else
                                {
                                    dtgridcustcode.CurrentCell = dtgridcustcode[2, rowNo];
                                    //dtgridcustcode.BeginEdit(true);
                                    tb.Focus();
                                }
                            }


                            #endregion
                            else
                            {
                                dtgridcustcode.CurrentCell = dtgridcustcode[2, rowNo];
                                dtgridcustcode.BeginEdit(true);
                                e.Handled = true;
                            }
                        }
                        #endregion
                        #region colm4
                        else if (colNo == 4)
                        {

                            if (e.KeyCode == Keys.Back)
                            {
                                dtgridcustcode.Rows[rowNo].Cells[4].Value = null;
                            }
                            else if (dtgridcustcode.Rows[rowNo].Cells[1].Value.ToString() == "D" || dtgridcustcode.Rows[rowNo].Cells[1].Value.ToString() == "C")
                            {
                                #region Enter Id in datagridText
                                if (e.KeyCode == Keys.Enter && dtgridcustcode.Rows[rowNo].Cells[4].Value != null)
                                {
                                    {
                                       // DupCode1 = mod.DuplicateRecord(dtgridcustcode, 4);
                                        Globalvariable.DuplicateValue = DupCode1;
                                      //  dtgridcustcode.Rows[rowNo].Cells[4].Value = tb.Text;
                                        if (tb.Text != string.Empty && dtgridcustcode.Rows[rowNo].Cells[4].Value != null)
                                        {
                                            int i;
                                            int itemn = Convert.ToInt32(dtgridcustcode.Rows[rowNo].Cells[4].Value);
                                            chkDupFlag = false;
                                            i = mod.getDuplicateRowNo(itemn, dtgridcustcode, 4, rowNo);
                                            if (i == -1)
                                            {
                                                code = dtgridcustcode.Rows[rowNo].Cells[4].Value.ToString();

                                                str = "SELECT Customer_name,Customer_id FROM tbGuestInformation WHERE Customer_id='" + code + "' AND Leger_id='" + dtgridcustcode.Rows[rowNo].Cells[2].Value + "' AND deleted='N' AND Branchcode='" + Globalvariable.bcode + "'";
                                                SqlDataReader dr1 = mod.GetRecord(str);
                                                if (dr1.Read())
                                                {
                                                    tb.Text = "";
                                                    dtgridcustcode.ClearSelection();
                                                   // dtgridcustcode.CurrentCell = dtgridcustcode[4, rowNo];
                                                    //  dtgridcustcode.BeginEdit(true);
                                                    dtgridcustcode.Rows[rowNo].Cells[4].Value = dr1["Customer_id"].ToString();
                                                    dtgridcustcode.Rows[rowNo].Cells[5].Value = dr1["Customer_name"].ToString();
                                                    tb.Text = dr1["Customer_id"].ToString();
                                                    //Add row in grid                                               
                                                    dtgridcustcode.Enabled = true;
                                                    dtgridcustcode.CurrentCell = dtgridcustcode[6, rowNo];
                                                    e.Handled = true;
                                                }
                                                else
                                                {
                                                    chkDupFlag = true;
                                                    dtgridcustcode.Rows[rowNo].Cells[4].Value = "";
                                                    tb.Text = "";
                                                    //dtgridcustcode.ClearSelection();
                                                    dtgridcustcode.CurrentCell = dtgridcustcode[4, rowNo];
                                                    //dtgridcustcode.BeginEdit(true);
                                                }
                                            }
                                            else
                                            {
                                                chkDupFlag = true;
                                                dtgridcustcode.Rows[rowNo].Cells[4].Value = "";
                                                tb.Text = "";
                                                //dtgridcustcode.ClearSelection();
                                                dtgridcustcode.CurrentCell = dtgridcustcode[4, rowNo];
                                                // dtgridcustcode.BeginEdit(true);
                                            }
                                        }
                                    }
                                }
                                #endregion

                                #region Enter Key Press And Show Search from
                                else if (dtgridcustcode.Rows[rowNo].Cells[4].Value == null && e.KeyCode == Keys.Enter)
                                {
                                    Globalvariable.SearchString = "GuestInformation";
                                    KeysConverter kc = new KeysConverter();
                                    string key = kc.ConvertToString(e.KeyCode);
                                    FrmSearch srch = new FrmSearch();
                                    // DupCode1=ledger id in dtgridcustcode                              
                                    DupCode1 = dtgridcustcode.Rows[rowNo].Cells[2].Value.ToString();
                                    srchda = SP_FillSearchGrid("tbGuestInformation", DupCode1, "0");
                                    srch.val = key;
                                    srch.fillbackgridSrch(srchda, "FrmJournalVoucher");
                                    ds.Clear();
                                    srchda.Fill(ds);
                                    if (ds.Tables[0].Rows.Count > 0)
                                        srch.ShowDialog();
                                    if (srch.codeselected != null)
                                    {
                                        tb.Text = "";
                                        dtgridcustcode.ClearSelection();
                                        dtgridcustcode.CurrentCell = dtgridcustcode[4, rowNo];
                                        dtgridcustcode.Rows[rowNo].Cells[4].Value = mod.Isnull(srch.codeselected, "");
                                        dtgridcustcode.Rows[rowNo].Cells[5].Value = mod.Isnull(srch.descselected, "");
                                        dtgridcustcode.Enabled = true;
                                        dtgridcustcode.CurrentCell = dtgridcustcode[6, rowNo];

                                    }
                                    else
                                    {
                                        dtgridcustcode.CurrentCell = dtgridcustcode[4, rowNo];
                                        //  dtgridcustcode.BeginEdit(true);
                                        tb.Focus();
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                dtgridcustcode.Enabled = true;
                                dtgridcustcode.CurrentCell = dtgridcustcode[6, rowNo];
                                dtgridcustcode.BeginEdit(true);
                            }     




                            //if (tb.Text != string.Empty)
                            //{
                            //    dtgridcustcode.CurrentCell = dtgridcustcode[6, rowNo];
                            //   // dtgridcustcode.BeginEdit(true);
                            //    e.Handled = true;
                            //}
                            //else
                            //{
                            //    dtgridcustcode.CurrentCell = dtgridcustcode[4, rowNo];
                            //    dtgridcustcode.BeginEdit(true);
                            //    e.Handled = true;
                            //}
                        }
                        #endregion
                        #region colm6
                        else if (colNo == 6)
                        {
                            if (rowNo == 1 && e.KeyCode == Keys.Enter)
                            {
                                txtNarration.Focus();
                            }
                            else if (rowNo == 0 && dtgridcustcode.Rows.Count < 2)
                            {
                                dtgridcustcode.Rows.Add();
                                dtgridcustcode.Rows[1].Cells[0].Value = "Cr";
                                dtgridcustcode.Rows[1].Cells[6].Value = Convert.ToDouble(txtAmount.Text).ToString("#0.00");
                                dtgridcustcode.CurrentCell = dtgridcustcode[1, 1];
                               // dtgridcustcode.BeginEdit(true);
                                e.Handled = true;
                            }
                            else if (dtgridcustcode.Rows.Count == 2)
                            {
                                dtgridcustcode.CurrentCell = dtgridcustcode[1, 1];
                               // dtgridcustcode.BeginEdit(true);
                            }
                        }
                        #endregion
                    }
                    //else
                    //{
                    //    dtgridcustcode.CurrentCell = dtgridcustcode[colNo, rowNo];
                    //    dtgridcustcode.BeginEdit(true);
                    //}
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void tabJournalVoucher_Selecting(object sender, TabControlCancelEventArgs e)
        {
            try
            {
                if (e.TabPage.Name == "tabDetail" && grdchange != 2 && flag != 0)
                    e.Cancel = true;
                else if (e.TabPage.Name == "tabList")
                    //BtnCancel_Click(sender, e);      
                    BtnAdd.Enabled = true;
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void FrmJournalVoucher_Shown(object sender, EventArgs e)
        {
            txtsearch.Focus();
        }
           
         }
}

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
    public partial class FrmContra : Form
    {
        Design ObjDesign = new Design();
        public FrmContra()
        {
            InitializeComponent();
        }
         int rowNo, colNo;
        
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        SqlDataAdapter da;
        SqlDataReader srchdr;
        SqlDataAdapter srchda;
        public Boolean plusKeyPress;
        double amount;
        string firstCol = "Voucher_No";
        string tableName = "tbTransContra";
        Connection con = new Connection();
        SqlCommand cmd;
        Module mod = new Module();
        string voch_dt,code = "";
         bool chkDupFlag;
        DataGridViewTextBoxEditingControl tb;
        int grdchange, m, escGrid, mnth,flag;
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

        private void txtsearch_Enter(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.Yellow;
        }

        private void txtsearch_Leave(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.White;
        }

        private void txtAmount_Enter(object sender, EventArgs e)
        {
            txtAmount.BackColor = Color.Yellow;
        }

        private void txtAmount_Leave(object sender, EventArgs e)
        {
            txtAmount.BackColor = Color.White;
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                    e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == '.');
                    if (txtAmount.Text == "" || txtAmount.Text == "0" && e.KeyChar == (char)Keys.Enter)
                    {
                        txtAmount.Focus();
                    }
                    else  if (e.KeyChar == (char)Keys.Enter && dtgridBookcode.Rows.Count==0)
                    {                     
                            dtgridBookcode.Enabled = true;
                            dtgridBookcode.Rows.Clear();
                            amount = Convert.ToDouble(txtAmount.Text);
                            txtAmount.Text = amount.ToString("#0.00");
                            dtgridBookcode.Rows.Add();
                            dtgridBookcode.Rows[0].Cells[0].Value = "Dr";
                            dtgridBookcode.Rows[0].Cells[3].Value = Convert.ToDouble(txtAmount.Text).ToString("#0.00");
                            dtgridBookcode.CurrentCell = dtgridBookcode[1, 0];
                            dtgridBookcode.BeginEdit(true);
                            e.Handled = true;                                           
                    }
                    else if (e.KeyChar == (char)Keys.Enter && dtgridBookcode.Rows.Count > 0)
                    {
                        dtgridBookcode.Enabled = true;                                                                                 
                        dtgridBookcode.CurrentCell = dtgridBookcode[1, 0];
                        dtgridBookcode.BeginEdit(true);
                        e.Handled = true;
                    }  
                }
                catch (Exception ex)
                {
                    string str = "Message:" + ex.Message;
                    MessageBox.Show(str, "Error Message");
                }
        }

        private void FrmContra_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Control nextTab;
                if (e.KeyChar == (char)Keys.Enter)
                {
                    nextTab = ((Control)sender);
                    if (ActiveControl.Name == "dtgridBookcode" && dtgridBookcode.RowCount > 0)
                    {
                        rowNo = dtgridBookcode.CurrentCell.RowIndex;
                        colNo = dtgridBookcode.CurrentCell.ColumnIndex;
                        dtgridBookcode.CurrentCell = dtgridBookcode[colNo, rowNo];
                    }
                    else if (ActiveControl.Name == "txtsearch" && dtgridContra.Rows.Count == 0 && e.KeyChar == (char)Keys.Enter)
                    {
                        txtsearch.Focus();
                    }
                    else if (ActiveControl.Name == "txtNarration")
                    {
                        txtNarration.Focus();
                    }
                    else
                    {
                        if (ActiveControl.Name == "txtsearch" && dtgridContra.Rows.Count == 0)
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

        public void TallyAmount()
        {
            rowNo = dtgridBookcode.CurrentCell.RowIndex;
            colNo = dtgridBookcode.CurrentCell.ColumnIndex;
            double DrAmount = 0, CrAmount = 0, TotalAmount = 0;
            TotalAmount = Convert.ToDouble(txtAmount.Text);
           
                for (int cnt = 0; cnt < dtgridBookcode.Rows.Count; cnt++)
                {
                    if (dtgridBookcode.Rows[cnt].Cells[0].Value.ToString() == "Dr")
                    {
                        DrAmount = DrAmount + Convert.ToDouble(dtgridBookcode.Rows[cnt].Cells[3].Value);
                    }
                    else if (dtgridBookcode.Rows[cnt].Cells[0].Value.ToString() == "Cr")
                    {
                        CrAmount = CrAmount + Convert.ToDouble(dtgridBookcode.Rows[cnt].Cells[3].Value);
                    }
                }

                if (DrAmount <= TotalAmount && CrAmount <= TotalAmount)
                {
                    if (TotalAmount != DrAmount)
                    {
                        dtgridBookcode.Rows.Add();
                        dtgridBookcode.Rows[dtgridBookcode.Rows.Count - 1].Cells[0].Value = "Dr";
                        dtgridBookcode.Rows[dtgridBookcode.Rows.Count - 1].Cells[3].Value = Convert.ToDouble(TotalAmount - DrAmount).ToString("#0.00");
                        dtgridBookcode.CurrentCell = dtgridBookcode[1, dtgridBookcode.Rows.Count - 1];
                        dtgridBookcode.BeginEdit(true);
                    }
                    else if (TotalAmount == DrAmount && TotalAmount != CrAmount)
                    {
                        dtgridBookcode.Rows.Add();
                        dtgridBookcode.Rows[dtgridBookcode.Rows.Count - 1].Cells[0].Value = "Cr";
                        dtgridBookcode.Rows[dtgridBookcode.Rows.Count - 1].Cells[3].Value = Convert.ToDouble(TotalAmount - CrAmount).ToString("#0.00");
                        dtgridBookcode.CurrentCell = dtgridBookcode[1, dtgridBookcode.Rows.Count - 1];
                        dtgridBookcode.BeginEdit(true);
                    }
                    else if (TotalAmount == DrAmount && TotalAmount == CrAmount)
                    {
                        txtNarration.Text = "BEING AMOUNT";
                        txtNarration.Focus();
                    }
                }
           
        }

         
        private void FrmContra_KeyDown(object sender, KeyEventArgs e)
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

        private void FrmContra_Load(object sender, EventArgs e)
        {
            ObjDesign.FormDesign(this, dtgridBookcode);
            ObjDesign.FormDesign(this, dtgridContra);
            flag = 1;           
            tabReceipt.SelectedTab = tabList;
            FillGrid("tbTransContra");
            txtTransNo.Enabled = false;
            mod.UserAccessibillityMaster("Contra", BtnAdd, BtnSave, BtnDelete);
            Globalvariable.SearchChangeVariable = "SrchByName";
            txtsearch.Focus();
        }

        public void FillGrid(string tableNM)
        {
            try
            {
                    string strsql;
                     DataTable dt = new DataTable();
                    DataRow drw;                 
                        int cd = 1;
                        double samt;
                        dtgridContra.Columns.Clear();
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

                            cmd = new SqlCommand("SP_FrmTransContra", con.connect());
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@variable", "SELECT");
                            cmd.Parameters.AddWithValue("@tablename", "tbTransContra");
                            cmd.Parameters.AddWithValue("@checkstring", "SUM");
                            cmd.Parameters.AddWithValue("@month", m);
                            cmd.Parameters.AddWithValue("@startDate", Globalvariable.StartDate);
                            cmd.Parameters.AddWithValue("@endDate", Globalvariable.EndDate);
                            cmd.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                            cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                            samt = Convert.ToDouble(mod.Isnull(cmd.ExecuteScalar().ToString(), "0"));
                            drw["Amount"] = samt.ToString("#0.00");
                            dt.Rows.Add(drw);
                            m = m + 1;
                            cd = cd + 1;
                        }
                        dtgridContra.DataSource = dt;
                        dtgridContra.Columns[0].Width = 501;
                        dtgridContra.Columns[1].Width = 183;
                        dtgridContra.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                        grdchange = 0;
                        escGrid = 0;
                        if (dtgridContra.Rows.Count > 0)
                        {
                            txtsearch.Text = dtgridContra.Rows[0].Cells[0].Value.ToString();
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
            grdchange = 0;
            flag = 0;
            tabReceipt.SelectedTab = tabDetail;
            mod.addBtnClick(this,txtTransNo,tableName, firstCol, BtnSave, BtnDelete, txtAmount);
            mod.txtclear(tabReceipt.TabPages[1].Controls);
            dtgridBookcode.Rows.Clear();
            mod.UserAccessibillityMaster("Contra", BtnAdd, BtnSave, BtnDelete);
           txtTransNo.Text = "" + mod.GetMaxNum(tableName, firstCol);
            BtnPrint.Enabled = false;
            btnGreaterID.Visible = false;
            btnLessID.Visible = false;
            dtpVoucherDate.Focus();
            BtnDelete.Text = "RESET";
          //  flag = 0;
        }

        public void ExitGridFill()
        {
            try
            {
                if (dtgridContra.Rows.Count > 0)
                {
                    int mntotal, dayTot;
                    string strsql;
                    double totlamt;
                    DateTime dtm;
                    DataTable dt = new DataTable();
                    DataRow drw;
                    if (grdchange == 0)
                    {
                        int i = dtgridContra.SelectedCells[0].RowIndex;
                        string cd = dtgridContra.Rows[i].Cells[0].Value.ToString();
                        if (dtgridContra.Rows[i].Cells[1].Value.ToString() != "0.00")
                        {
                            //if (escGrid == 0 || escGrid == 1)
                            //{
                            //    mnth = mod.Getmonthno(dtgridJournalVoucher.Rows[i].Cells[0].Value.ToString());
                            //}
                            dtgridContra.Columns.Clear();
                            dt.Columns.Add("Voucher Date", typeof(string));
                            dt.Columns.Add("Year", typeof(string));
                            dt.Columns.Add("Amount", typeof(string));
                            mntotal = 0;
                            //SELECT Voucher_Date FROM tbTransJournalVoucher WHERE Company_Srno='' AND Voucher_No='' AND Branchcode='';
                            cmd = new SqlCommand("SP_FrmTransContra", con.connect());
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
                                cmd = new SqlCommand("SP_FrmTransContra", con.connect());
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
                            dtgridContra.DataSource = dt;
                            dtgridContra.Columns[0].Width = 110;
                            dtgridContra.Columns[1].Width = 390;
                            dtgridContra.Columns[2].Width = 184;
                            dtgridContra.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                            if (dtgridContra.Rows.Count > 0)
                            {
                                txtsearch.Text = dtgridContra.Rows[0].Cells[0].Value.ToString();
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
                //Fillgriddetails();
                ExitGridFill();
            }
            else if (grdchange == 1)
            {
                grdchange = 0;
                txtsearch.Focus();
                tabReceipt.SelectedTab = tabList;
                FillGrid("tbTransContra");
                BtnAdd.Visible = true;
            }
            else if (grdchange == 0)
            {
                Close();
            }
        }

        private void btnLessID_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new SqlCommand("SP_FrmTransContra", con.connect());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@variable", "LESS_ID");
                cmd.Parameters.AddWithValue("@code", txtTransNo.Text);
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

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                tabReceipt.SelectedTab = tabList;
                grdchange = 0;
                FillGrid("tbTransContra");
                BtnAdd.Visible = true;
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        void Fillgriddetails()
        {
            try
            {
                if (dtgridContra.Rows.Count > 0)
                {
                    int mntotal, dayTot;
                    string strsql;
                    double totlamt;
                    DateTime dtm;
                    DataTable dt = new DataTable();
                    DataRow drw;
                    if (grdchange == 0)
                    {
                        int i = dtgridContra.SelectedCells[0].RowIndex;
                        string cd = dtgridContra.Rows[i].Cells[1].Value.ToString();
                        if (dtgridContra.Rows[i].Cells[1].Value.ToString() != "0.00")
                        {
                            if (escGrid == 0 || escGrid == 1)
                            {
                                mnth = mod.Getmonthno(dtgridContra.Rows[i].Cells[0].Value.ToString());
                            }
                            dtgridContra.Columns.Clear();
                            dt.Columns.Add("Voucher Date", typeof(string));
                            dt.Columns.Add("Year", typeof(string));
                            dt.Columns.Add("Amount", typeof(string));

                            mntotal = 0;
                           
                            cmd = new SqlCommand("SP_FrmTransContra", con.connect());
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
                                cmd = new SqlCommand("SP_FrmTransContra", con.connect());
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
                            dtgridContra.DataSource = dt;
                            dtgridContra.Columns[0].Width = 100;
                            dtgridContra.Columns[1].Width = 396;
                            dtgridContra.Columns[2].Width = 188;
                            dtgridContra.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                            if (dtgridContra.Rows.Count > 0)
                            {
                                txtsearch.Text = dtgridContra.Rows[0].Cells[0].Value.ToString();
                            }
                            BtnAdd.Visible = false;
                        }
                    }
                    else if (grdchange == 1)
                    {
                        int i = dtgridContra.SelectedCells[0].RowIndex;
                        if (Convert.ToString(dtgridContra.Rows[i].Cells[2].Value) != "0.00")
                        {
                            voch_dt = mnth + "/" + dtgridContra.Rows[i].Cells[0].Value + "/" + dtgridContra.Rows[i].Cells[1].Value;

                            dtgridContra.Columns.Clear();
                            dt.Columns.Add("Voucher No", typeof(string));                    
                            dt.Columns.Add("Amount", typeof(string));

                            //strsql = "select * from " + strHeadTable + " where BK_CODE='" + m_bkcode + "' and Company_Sr='" + gv.companysr +
                            //         "' and voch_dt='" + voch_dt + "' and Branchcode='" + gv.Brncode + "'" + gv.advRecCriteria + "";
                            cmd = new SqlCommand("SP_FrmTransContra", con.connect());
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
                                drw["Amount"] = Convert.ToDouble(dr["Total_Amount"].ToString()).ToString("#0.00");
                                dt.Rows.Add(drw);
                            }
                            grdchange = 2;
                            escGrid = 1;
                            dr.Close();
                            dtgridContra.DataSource = dt;
                            dtgridContra.Columns[0].Width = 345;
                            dtgridContra.Columns[1].Width = 339;                            
                            dtgridContra.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                            if (dtgridContra.Rows.Count > 0)
                            {
                                txtsearch.Text = dtgridContra.Rows[0].Cells[0].Value.ToString();
                            }
                            BtnAdd.Visible = false;
                        }
                    }
                    else if (grdchange == 2)
                    {
                        flag = 1;
                        tabReceipt.SelectedTab = tabDetail;
                       // dtpVoucherDate.Focus();
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

        

        private void dtgridContra_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Fillgriddetails();
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
                        if (dtgridContra.RowCount > 0)
                        {
                            k = dtgridContra.SelectedCells[0].RowIndex;
                            if (dtgridContra.Rows[k].Cells[1].Value.ToString() != "0.00" && grdchange != 1)
                            {
                                i = dtgridContra.SelectedCells[0].RowIndex;
                                dtgridContra.Text = dtgridContra.Rows[i].Cells[0].Value.ToString();
                                string ID = mod.Isnull(dtgridContra.Rows[i].Cells[0].Value.ToString(), "");
                                SqlDataReader dareader = mod.GetSelectAllField(tableName, firstCol, ID, "SrNo");
                                FillDetails(dareader, "null");
                                BtnDelete.Text = "DELETE";
                                BtnSave.Text = "UPDATE";
                            }
                            else
                            {
                                MessageBox.Show("No record to display");
                                this.tabReceipt.SelectedTab = tabList;
                            }
                        }
                    }
                }
                else
                {
                      FillGrid("tbTransContra");
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
                    txtTransNo.Text = "";
                    txtTransNo.Text = dr["Voucher_No"].ToString();
                    dtpVoucherDate.Text = dr["Voucher_Date"].ToString();
                    txtAmount.Text = Convert.ToDouble(dr["Total_Amount"].ToString()).ToString("#0.00");
                    SqlDataReader dr1 = mod.GetSelectAllField("tbTransContraDetail", "Voucher_No", txtTransNo.Text.ToString(), "SrNo");
                    dtgridBookcode.Rows.Clear();
                    i = 0;                   
                        while (dr1.Read())
                        {
                            dtgridBookcode.Rows.Add();
                            dtgridBookcode.Rows[i].Cells[0].Value = dr1["Transction_Type"].ToString();
                            dtgridBookcode.Rows[i].Cells[1].Value = dr1["GL_Code"].ToString();
                            string[] rstr;
                            rstr = GetGLName(dr1["GL_Code"].ToString());
                            dtgridBookcode.Rows[i].Cells[2].Value = rstr[0];
                            dtgridBookcode.Rows[i].Cells[3].Value = Convert.ToDouble(dr1["Total_Amount"].ToString()).ToString("#0.00"); ;
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

        private string[] GetGLName(string tno)
        {
            string[] rtstr = new string[2];
            SqlDataReader dr1 = mod.GetRecord("SELECT book_description,book_code FROM tbBook WHERE book_code='" + tno + "' AND deleted='N' AND Branchcode='" + Globalvariable.bcode +"'");
            if (dr1.HasRows)
            {
                dr1.Read();
                rtstr[0] = dr1["book_description"].ToString();
                return (rtstr);
            }
            else
            {
                rtstr[0] = "";
                return (rtstr);
            }
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            mod.transSearch(txtsearch, dtgridContra, 0);
        }

        private void txtsearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                dtgridContra_KeyDown(sender, e);
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void dtgridContra_KeyDown(object sender, KeyEventArgs e)
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
                        i = dtgridContra.SelectedCells[0].RowIndex + 1;
                    else if (e.KeyCode == Keys.Up)
                        i = dtgridContra.SelectedCells[0].RowIndex - 1;

                    if (((e.KeyCode == Keys.Down) && (i < dtgridContra.RowCount)) || ((e.KeyCode == Keys.Up) && (i >= 0)))
                    {
                        dtgridContra.Refresh();
                        dtgridContra.CurrentCell = dtgridContra.Rows[i].Cells[0];
                        dtgridContra.Rows[i].Selected = true;
                        if(dtgridContra.Rows.Count>0)
                        {
                        txtsearch.Text = dtgridContra.Rows[i].Cells[0].Value.ToString();
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

        private void txtsearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && txtsearch.Text == "")
            {
                BtnAdd.Focus();
            }
        }

        private void Control_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                string str;
                Globalvariable.SearchString = "DayBook";
                BtnSave.Enabled = false;
                
                DupCode1 = "0";
                rowNo = dtgridBookcode.CurrentCell.RowIndex;
                colNo = dtgridBookcode.CurrentCell.ColumnIndex;
                e.IsInputKey = true;
                if (colNo == 1)
                {
                    if (e.KeyCode == Keys.Back)
                    {
                        dtgridBookcode.Rows[rowNo].Cells[1].Value = null;
                    }
                    else if (e.KeyCode == Keys.Enter && tb.Text != string.Empty)
                    {
                        DupCode1 =mod.DuplicateRecord(dtgridBookcode, 1,rowNo);
                        Globalvariable.DuplicateValue = DupCode1;
                        dtgridBookcode.Rows[rowNo].Cells[1].Value = tb.Text;
                        if (dtgridBookcode.Rows[rowNo].Cells[1].Value != null && dtgridBookcode.Rows[rowNo].Cells[1].Value.ToString() != "")
                        {
                            int i;
                            int itemn = Convert.ToInt32(dtgridBookcode.Rows[rowNo].Cells[1].Value);
                            chkDupFlag = false;
                            i = mod.getDuplicateRowNo(itemn, dtgridBookcode, 1, rowNo);
                            if (i == -1)
                            {
                                code = dtgridBookcode.Rows[rowNo].Cells[1].Value.ToString();
                                str = "SELECT book_description,book_code FROM tbBook WHERE book_code='" + code + "' AND book_code NOT IN(" + DupCode1 + ") AND deleted='N' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY book_code";
                                SqlDataReader dr1 = mod.GetRecord(str);
                                if (dr1.Read())
                                {
                                    tb.Text = "";
                                    dtgridBookcode.ClearSelection();
                                    dtgridBookcode.CurrentCell = dtgridBookcode[1, rowNo];
                                    dtgridBookcode.BeginEdit(true);
                                    dtgridBookcode.Rows[rowNo].Cells[1].Value = dr1["book_code"].ToString();
                                    dtgridBookcode.Rows[rowNo].Cells[2].Value = dr1["book_description"].ToString();
                                    tb.Text = dr1["book_code"].ToString();
                                    //Add row in grid
                                    dtgridBookcode.Enabled = true;
                                    dtgridBookcode.CurrentCell = dtgridBookcode[3, rowNo];
                                    if (dtgridBookcode.Rows[rowNo].Cells[1].Value.ToString() == "1")
                                    {
                                        txtNarration.Text = "BEING CASH WITHDRAWAL FROM BANK";
                                    }
                                    else
                                    {
                                        txtNarration.Text = "BEING CASH DEPOSITED TO BANK";
                                    }
                                }
                                else
                                {
                                    chkDupFlag = true;
                                    dtgridBookcode.Rows[rowNo].Cells[1].Value = "";
                                    tb.Text = "";
                                   // dtgridBookcode.ClearSelection();
                                   dtgridBookcode.CurrentCell = dtgridBookcode[1, rowNo];
                                    dtgridBookcode.BeginEdit(true);                    
                                }
                            }
                            else
                            {
                                chkDupFlag = true;
                                dtgridBookcode.Rows[rowNo].Cells[1].Value = "";
                                tb.Text = "";
                              //  dtgridBookcode.ClearSelection();
                                dtgridBookcode.CurrentCell = dtgridBookcode[1, rowNo];
                                dtgridBookcode.BeginEdit(true);
                            }
                        }
                        else if (tb.Text != string.Empty && dtgridBookcode.Rows[rowNo].Cells[2].Value.ToString() != "")
                        {
                            if (dtgridBookcode.Rows[rowNo].Cells[1].Value.ToString() == "1")
                            {
                                txtNarration.Text = "BEING CASH WITHDRAWAL FROM BANK";
                            }
                            else
                            {
                                txtNarration.Text = "BEING CASH DEPOSITED TO BANK";
                            }
                            dtgridBookcode.CurrentCell = dtgridBookcode[3, rowNo];
                           // dtgridBookcode.BeginEdit(false);                     
                        }
                    }
                    else if (tb.Text == string.Empty && e.KeyCode == Keys.Enter)
                    {
                        Globalvariable.searchNo = 1;
                        KeysConverter kc = new KeysConverter();
                        string key = kc.ConvertToString(e.KeyCode);
                        FrmSearch srch = new FrmSearch();
                        //  DupCode1 = "0";
                        DupCode1 =mod.DuplicateRecord(dtgridBookcode, 1,rowNo);
                        Globalvariable.DuplicateValue = DupCode1;
                       // str = "SELECT book_description,book_code FROM tbBook WHERE book_code NOT IN(" + DupCode1 + ") AND deleted='N' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY book_code";
                        srch.val = key;
                        srchda = mod.SP_FillSearchGrid("tbBook", "book_description","book_code",DupCode1);
                        srch.val = key;
                        srch.fillbackgridSrch(srchda, "FrmContra");
                        ds.Clear();
                        srchda.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                            srch.ShowDialog();
                        if (srch.codeselected != null)
                        {
                            tb.Text = "";
                            dtgridBookcode.ClearSelection();
                            dtgridBookcode.CurrentCell = dtgridBookcode[1, rowNo];
                            dtgridBookcode.Rows[rowNo].Cells[1].Value = mod.Isnull(srch.codeselected, "");
                            dtgridBookcode.Rows[rowNo].Cells[2].Value = mod.Isnull(srch.descselected, "");
                            //Add row in grid
                           //Add row in grid
                            dtgridBookcode.Enabled = true;
                            dtgridBookcode.CurrentCell = dtgridBookcode[3, rowNo];
                            if (dtgridBookcode.Rows[rowNo].Cells[1].Value == "1")
                            {
                                txtNarration.Text = "BEING CASH WITHDRAWAL FROM BANK";
                            }
                            else
                            {
                                txtNarration.Text = "BEING CASH DEPOSITED TO BANK";
                            }
                        }
                        else
                        {
                            dtgridBookcode.CurrentCell = dtgridBookcode[1, rowNo];
                            dtgridBookcode.BeginEdit(true);
                            tb.Focus();
                        }
                    }
                    else if (e.KeyCode == Keys.Enter && tb.Text != string.Empty && dtgridBookcode.Rows[rowNo].Cells[2].Value != null)
                    {
                        // string k = itemMastGrid.Rows[rowNo].Cells[1].Value.ToString();
                        int rowcount = dtgridBookcode.Rows.Count;
                        if (rowNo == rowcount - 1)
                        {
                            // int val;                           
                            rowNo = dtgridBookcode.RowCount - 1;
                            if (dtgridBookcode.Rows[rowNo].Cells[1].Value != null)
                            {
                                dtgridBookcode.Rows.Add();
                                rowNo = dtgridBookcode.RowCount - 1;
                                dtgridBookcode.Rows[rowNo].Cells[0].Value = dtgridBookcode.Rows.Count;
                                dtgridBookcode.CurrentCell = dtgridBookcode[1, rowNo];
                                dtgridBookcode.BeginEdit(true);
                            }
                            else
                            {
                                dtgridBookcode.Rows[rowNo].Cells[0].Value = dtgridBookcode.Rows.Count;
                                dtgridBookcode.CurrentCell = dtgridBookcode[1, rowNo];
                                dtgridBookcode.BeginEdit(true);
                            }                                  
                        }
                    }                  
                }
                else if (colNo == 3 && e.KeyCode == Keys.Enter)
                {
                   double DrAmount = 0, CrAmount = 0, TotalAmount = 0;
                            TotalAmount = Convert.ToDouble(txtAmount.Text);
                            dtgridBookcode.Rows[rowNo].Cells[3].Value = tb.Text;
                                for (int cnt = 0; cnt < dtgridBookcode.Rows.Count; cnt++)
                                {
                                    if (dtgridBookcode.Rows[cnt].Cells[0].Value.ToString() == "Dr")
                                    {
                                        DrAmount = DrAmount + Convert.ToDouble(dtgridBookcode.Rows[cnt].Cells[3].Value);
                                    }
                                    else if (dtgridBookcode.Rows[cnt].Cells[0].Value.ToString() == "Cr")
                                    {
                                        CrAmount = CrAmount + Convert.ToDouble(dtgridBookcode.Rows[cnt].Cells[3].Value);
                                    }
                                }

                                if (DrAmount <= TotalAmount && CrAmount <= TotalAmount)
                                {
                                    if (TotalAmount != DrAmount)
                                    {
                                        dtgridBookcode.Rows.Add();
                                        dtgridBookcode.Rows[dtgridBookcode.Rows.Count - 1].Cells[0].Value = "Dr";
                                        dtgridBookcode.Rows[dtgridBookcode.Rows.Count - 1].Cells[3].Value = Convert.ToDouble(TotalAmount - DrAmount).ToString("#0.00");
                                        dtgridBookcode.CurrentCell = dtgridBookcode[1, dtgridBookcode.Rows.Count - 1];
                                        dtgridBookcode.BeginEdit(true);
                                    }
                                    else if (TotalAmount == DrAmount && TotalAmount != CrAmount)
                                    {
                                        dtgridBookcode.Rows.Add();
                                        dtgridBookcode.Rows[dtgridBookcode.Rows.Count - 1].Cells[0].Value = "Cr";
                                        dtgridBookcode.Rows[dtgridBookcode.Rows.Count - 1].Cells[3].Value = Convert.ToDouble(TotalAmount - CrAmount).ToString("#0.00");
                                        dtgridBookcode.CurrentCell = dtgridBookcode[1, dtgridBookcode.Rows.Count - 1];
                                        dtgridBookcode.BeginEdit(true);
                                    }
                                    else if (TotalAmount == DrAmount && TotalAmount == CrAmount )
                                    {
                                        if(rowNo==dtgridBookcode.Rows.Count-1)
                                        {
                                        txtNarration.Text = "BEING AMOUNT";
                                        txtNarration.Focus();
                                        }
                                        else if(rowNo<dtgridBookcode.Rows.Count-1)
                                        {
                                            dtgridBookcode.CurrentCell = dtgridBookcode[1, rowNo];
                                            dtgridBookcode.BeginEdit(true);
                                        }
                                    }
                    }
               }
                                       
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

            private void clearCol()
            {
                for (int cnt = 1; cnt < dtgridBookcode.ColumnCount; cnt++)
                {
                    dtgridBookcode.Rows[dtgridBookcode.CurrentCell.RowIndex].Cells[cnt].Value = "";
                }
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
             colNo = dtgridBookcode.CurrentCell.ColumnIndex;
             rowNo = dtgridBookcode.CurrentCell.RowIndex;

             if (colNo == 1 || colNo == 3)
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

        private void dtgridBookcode_CellEnter_1(object sender, DataGridViewCellEventArgs e)
        {
            dtgridBookcode[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.Yellow;
            //Cursor.Show();
        }

        private void dtgridBookcode_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            dtgridBookcode[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.White;
            //Cursor.Hide();
        }

        private void dtgridBookcode_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
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

        private void txtNarration_Enter(object sender, EventArgs e)
        {
            txtNarration.BackColor = Color.Yellow;
        }

        private void txtNarration_Leave(object sender, EventArgs e)
        {
            txtNarration.BackColor = Color.White;
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
            if (BtnDelete.Text == "DELETE")
            {
                DialogResult mydialog;
                mydialog = MessageBox.Show("Are you Sure you want to delete this record", "", MessageBoxButtons.YesNo);
                if (mydialog == DialogResult.Yes)
                {
                    cmd = new SqlCommand("SP_FrmTransContra", con.connect());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@variable", "DELETE");
                    cmd.Parameters.AddWithValue("@code", txtTransNo.Text);
                    cmd.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                    cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                    cmd.ExecuteNonQuery();
                    txtsearch.Focus();
                    tabReceipt.SelectedTab = tabList;
                    //FillGrid("tbTransContra");
                    //BtnAdd.Visible = true;
                }
            }
            else if (BtnDelete.Text == "RESET")
            {
                add();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
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
            else
            {
                cmd = new SqlCommand("SP_FrmTransContra", con.connect());
                cmd.CommandType = CommandType.StoredProcedure;

                if (BtnSave.Text == "SAVE")
                {
                    txtTransNo.Text = "" + mod.GetMaxNum(tableName, firstCol);
                    cmd.Parameters.AddWithValue("@variable", "INSERT");
                }
                else
                {
                    str = "DELETE FROM tbTransContraDetail WHERE Voucher_No=" + txtTransNo.Text + " AND Company_Srno='" + Globalvariable.company_Srno + "' AND Branchcode='"+Globalvariable.bcode+"'";
                    //command.CommandText = str;
                    command = new SqlCommand(str, con.connect());
                    command.ExecuteNonQuery();
                    cmd.Parameters.AddWithValue("@variable", "UPDATE");
                }
                cmd.Parameters.AddWithValue("@code", txtTransNo.Text);
                cmd.Parameters.AddWithValue("@Voucherdate", dtpVoucherDate.Value.ToShortDateString());
                cmd.Parameters.AddWithValue("@TotalAmount", mod.Isnull(txtAmount.Text, "0"));
                cmd.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                cmd.ExecuteNonQuery();

                for (int cnt = 0; cnt < dtgridBookcode.Rows.Count; cnt++)
                {
                    if (dtgridBookcode.Rows[cnt].Cells[0].Value == null)
                    {
                        dtgridBookcode.Rows.RemoveAt(cnt);
                    }
                    else
                    {
                        SqlCommand cmdGlcode = new SqlCommand("SP_FrmTransContra", con.connect());
                        cmdGlcode.CommandType = CommandType.StoredProcedure;
                        cmdGlcode.Parameters.AddWithValue("@variable", "INSERTContraDetail");
                        cmdGlcode.Parameters.AddWithValue("@code", txtTransNo.Text);
                        cmdGlcode.Parameters.AddWithValue("@Voucherdate", dtpVoucherDate.Value.ToShortDateString());
                        cmdGlcode.Parameters.AddWithValue("@custtype",dtgridBookcode.Rows[cnt].Cells[0].Value.ToString());
                        cmdGlcode.Parameters.AddWithValue("@glCode", dtgridBookcode.Rows[cnt].Cells[1].Value);
                        cmdGlcode.Parameters.AddWithValue("@narration", mod.Isnull(txtNarration.Text, "-"));
                        cmdGlcode.Parameters.AddWithValue("@TotalAmount", dtgridBookcode.Rows[cnt].Cells[3].Value);
                        cmdGlcode.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                        cmdGlcode.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                        cmdGlcode.ExecuteNonQuery();
                    }
                }               
                mod.txtclear(tabReceipt.TabPages[1].Controls);
                BtnSave.Text = "SAVE";
                dtgridBookcode.Rows.Clear();
                txtTransNo.Text = "" + mod.GetMaxNum(tableName, firstCol);
                btnGreaterID.Visible = false;
                btnLessID.Visible = false;
                dtpVoucherDate.Focus();
                BtnPrint.Enabled = true;
            }
        }

        private void btnGreaterID_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new SqlCommand("SP_FrmTransContra", con.connect());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@variable", "GREATER_ID");
                cmd.Parameters.AddWithValue("@code", txtTransNo.Text);
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

        private void dtgridBookcode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                string str;
                Globalvariable.SearchString = "DayBook";
                BtnSave.Enabled = false;
                DupCode1 = "0";
                rowNo = dtgridBookcode.CurrentCell.RowIndex;
                colNo = dtgridBookcode.CurrentCell.ColumnIndex;
                
                if (colNo == 1)
                {
                    if (e.KeyCode == Keys.Back)
                    {
                        dtgridBookcode.Rows[rowNo].Cells[1].Value = null;
                    }
                    else if (e.KeyCode == Keys.Enter && tb.Text != string.Empty)
                    {
                        DupCode1 =mod.DuplicateRecord(dtgridBookcode, 1,rowNo);
                        Globalvariable.DuplicateValue = DupCode1;
                       // dtgridBookcode.Rows[rowNo].Cells[1].Value = tb.Text;
                        if (dtgridBookcode.Rows[rowNo].Cells[1].Value != null && dtgridBookcode.Rows[rowNo].Cells[1].Value.ToString()!="")
                        {
                            int i;
                            int itemn = Convert.ToInt32(dtgridBookcode.Rows[rowNo].Cells[1].Value);
                            chkDupFlag = false;
                            i = mod.getDuplicateRowNo(itemn, dtgridBookcode, 1, rowNo);
                            if (i == -1)
                            {
                                code = dtgridBookcode.Rows[rowNo].Cells[1].Value.ToString();
                                str = "SELECT book_description,book_code FROM tbBook WHERE book_code='" + code + "' AND book_code NOT IN(" + DupCode1 + ") AND deleted='N' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY book_code";
                                SqlDataReader dr1 = mod.GetRecord(str);
                                if (dr1.Read())
                                {
                                    tb.Text = "";
                                    dtgridBookcode.ClearSelection();
                                    dtgridBookcode.CurrentCell = dtgridBookcode[1, rowNo];
                                    dtgridBookcode.BeginEdit(true);
                                    dtgridBookcode.Rows[rowNo].Cells[1].Value = dr1["book_code"].ToString();
                                    dtgridBookcode.Rows[rowNo].Cells[2].Value = dr1["book_description"].ToString();
                                    tb.Text = dr1["book_code"].ToString();
                                    //Add row in grid
                                    dtgridBookcode.Enabled = true;
                                    dtgridBookcode.CurrentCell = dtgridBookcode[3, rowNo];
                                    if (dtgridBookcode.Rows[rowNo].Cells[1].Value.ToString() == "1")
                                    {
                                        txtNarration.Text = "BEING CASH WITHDRAWAL FROM BANK";
                                    }
                                    else
                                    {
                                        txtNarration.Text = "BEING CASH DEPOSITED TO BANK";
                                    }
                                }
                                else
                                {
                                    chkDupFlag = true;
                                    dtgridBookcode.Rows[rowNo].Cells[1].Value = "";
                                    tb.Text = "";
                                    // dtgridBookcode.ClearSelection();
                                    dtgridBookcode.CurrentCell = dtgridBookcode[1, rowNo];
                                    dtgridBookcode.BeginEdit(true);
                                }
                            }
                            else
                            {
                                chkDupFlag = true;
                                dtgridBookcode.Rows[rowNo].Cells[1].Value = "";
                                tb.Text = "";
                                //  dtgridBookcode.ClearSelection();
                                dtgridBookcode.CurrentCell = dtgridBookcode[1, rowNo];
                                dtgridBookcode.BeginEdit(true);
                            }
                        }
                        else if (tb.Text != string.Empty && dtgridBookcode.Rows[rowNo].Cells[2].Value.ToString() != "")
                        {
                            if (dtgridBookcode.Rows[rowNo].Cells[1].Value.ToString() == "1")
                            {
                                txtNarration.Text = "BEING CASH WITHDRAWAL FROM BANK";
                            }
                            else
                            {
                                txtNarration.Text = "BEING CASH DEPOSITED TO BANK";
                            }
                            dtgridBookcode.CurrentCell = dtgridBookcode[3, rowNo];
                            // dtgridBookcode.BeginEdit(false);

                        }
                    }
                    else if (tb.Text == string.Empty && e.KeyCode == Keys.Enter)
                    {
                        Globalvariable.searchNo = 1;
                        KeysConverter kc = new KeysConverter();
                        string key = kc.ConvertToString(e.KeyCode);
                        FrmSearch srch = new FrmSearch();
                        //  DupCode1 = "0";
                        DupCode1 =mod.DuplicateRecord(dtgridBookcode, 1,rowNo);
                        Globalvariable.DuplicateValue = DupCode1;
                        // str = "SELECT book_description,book_code FROM tbBook WHERE book_code NOT IN(" + DupCode1 + ") AND deleted='N' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY book_code";
                        srch.val = key;
                        srchda = mod.SP_FillSearchGrid("tbBook", "book_description", "book_code", DupCode1);
                        srch.val = key;
                        srch.fillbackgridSrch(srchda, "FrmItemMaster");
                        ds.Clear();
                        srchda.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                            srch.ShowDialog();
                        if (srch.codeselected != null)
                        {
                            tb.Text = "";
                            dtgridBookcode.ClearSelection();
                            dtgridBookcode.CurrentCell = dtgridBookcode[1, rowNo];
                            dtgridBookcode.Rows[rowNo].Cells[1].Value = mod.Isnull(srch.codeselected, "");
                            dtgridBookcode.Rows[rowNo].Cells[2].Value = mod.Isnull(srch.descselected, "");
                            //Add row in grid
                            //Add row in grid
                            dtgridBookcode.Enabled = true;
                            dtgridBookcode.CurrentCell = dtgridBookcode[3, rowNo];
                            if (dtgridBookcode.Rows[rowNo].Cells[1].Value == "1")
                            {
                                txtNarration.Text = "BEING CASH WITHDRAWAL FROM BANK";
                            }
                            else
                            {
                                txtNarration.Text = "BEING CASH DEPOSITED TO BANK";
                            }
                        }
                        else
                        {
                            dtgridBookcode.CurrentCell = dtgridBookcode[1, rowNo];
                            dtgridBookcode.BeginEdit(true);
                            tb.Focus();
                        }
                    }
                    else if (e.KeyCode == Keys.Enter && tb.Text != string.Empty && dtgridBookcode.Rows[rowNo].Cells[2].Value != null)
                    {
                        // string k = itemMastGrid.Rows[rowNo].Cells[1].Value.ToString();
                        int rowcount = dtgridBookcode.Rows.Count;
                        if (rowNo == rowcount - 1)
                        {
                            // int val;                           
                            rowNo = dtgridBookcode.RowCount - 1;
                            if (dtgridBookcode.Rows[rowNo].Cells[1].Value != null)
                            {
                                dtgridBookcode.Rows.Add();
                                rowNo = dtgridBookcode.RowCount - 1;
                                dtgridBookcode.Rows[rowNo].Cells[0].Value = dtgridBookcode.Rows.Count;
                                dtgridBookcode.CurrentCell = dtgridBookcode[1, rowNo];
                                dtgridBookcode.BeginEdit(true);
                            }
                            else
                            {
                                dtgridBookcode.Rows[rowNo].Cells[0].Value = dtgridBookcode.Rows.Count;
                                dtgridBookcode.CurrentCell = dtgridBookcode[1, rowNo];
                                dtgridBookcode.BeginEdit(true);
                            }
                        }
                    }
                }
                else if (colNo == 3 && e.KeyCode == Keys.Enter)
                {
                    double DrAmount = 0, CrAmount = 0, TotalAmount = 0;
                    TotalAmount = Convert.ToDouble(txtAmount.Text);
                   // dtgridBookcode.Rows[rowNo].Cells[3].Value = tb.Text;
                    for (int cnt = 0; cnt < dtgridBookcode.Rows.Count; cnt++)
                    {
                        if (dtgridBookcode.Rows[cnt].Cells[0].Value.ToString() == "Dr")
                        {
                          DrAmount = DrAmount + Convert.ToDouble(dtgridBookcode.Rows[cnt].Cells[3].Value);
                        }
                        else if (dtgridBookcode.Rows[cnt].Cells[0].Value.ToString() == "Cr")
                        {
                            CrAmount = CrAmount + Convert.ToDouble(dtgridBookcode.Rows[cnt].Cells[3].Value);
                        }
                    }

                    if (DrAmount <= TotalAmount && CrAmount <= TotalAmount)
                    {
                        if (TotalAmount != DrAmount)
                        {
                            dtgridBookcode.Rows.Add();
                            dtgridBookcode.Rows[dtgridBookcode.Rows.Count - 1].Cells[0].Value = "Dr";
                            dtgridBookcode.Rows[dtgridBookcode.Rows.Count - 1].Cells[3].Value = Convert.ToDouble(TotalAmount - DrAmount).ToString("#0.00");
                            dtgridBookcode.CurrentCell = dtgridBookcode[1, dtgridBookcode.Rows.Count - 1];
                            dtgridBookcode.BeginEdit(true);
                        }
                        else if (TotalAmount == DrAmount && TotalAmount != CrAmount)
                        {
                            dtgridBookcode.Rows.Add();
                            dtgridBookcode.Rows[dtgridBookcode.Rows.Count - 1].Cells[0].Value = "Cr";
                            dtgridBookcode.Rows[dtgridBookcode.Rows.Count - 1].Cells[3].Value = Convert.ToDouble(TotalAmount - CrAmount).ToString("#0.00");
                            dtgridBookcode.CurrentCell = dtgridBookcode[1, dtgridBookcode.Rows.Count - 1];
                            dtgridBookcode.BeginEdit(true);
                        }
                        else if (TotalAmount == DrAmount && TotalAmount == CrAmount)
                        {
                            if (rowNo == dtgridBookcode.Rows.Count - 1)
                            {
                                txtNarration.Text = "BEING AMOUNT";
                                txtNarration.Focus();
                            }
                            else if (rowNo < dtgridBookcode.Rows.Count - 1)
                            {
                                dtgridBookcode.CurrentCell = dtgridBookcode[1, rowNo+1];
                               // dtgridBookcode.BeginEdit(true);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void tabReceipt_Selecting(object sender, TabControlCancelEventArgs e)
        {
            try
            {
                if (e.TabPage.Name == "tabDetail" && grdchange != 2 && flag != 0)
                    e.Cancel = true;
                else if (e.TabPage.Name == "tabList")
                    BtnAdd.Enabled = true;
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void FrmContra_Shown(object sender, EventArgs e)
        {
            txtsearch.Focus();
        }
    }
}

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
using ASPL.LODGE.MASTERS;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Core;

namespace ASPL.ACCOUNT.TRANSACTION
{
    public partial class FrmPurchase : Form
    {
        Design ObjDesign = new Design();
        public FrmPurchase()
        {
            InitializeComponent();
        }
        int rowNo, colNo;
        string BILL_dt, fillCDO = "";
        int detail_id = 0, grdchange, escGrid, m, mnth, flag = 0;
        public static double slabPerVal = 0;
        public string m_bkcode, tableNM, table_Tax_detail, str;
        string val_colm5 = "0", val_colm1 = "0", val_colm4 = "0", val_colm3 = "0", val_colm6 = "0";
        double TotalAmount = 0, ItmTotal_Tax = 0, Total_Tax = 0;
        double TaxPerTotal = 0;
        SqlDataReader srchdr;
        SqlDataAdapter srchda;
        SqlDataReader dr;
        DataSet ds = new DataSet();
        Connection con = new Connection();
        SqlCommand cmd;
        Module mod = new Module();

        DataGridViewTextBoxEditingControl tb;
        public string DupCode1 = "0";
        string voch_dt, code = "", table_detail;
        bool chkDupFlag;
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

        //SELECT supplier_name,supplier_id FROM tbSupplierMaster WHERE deleted='N' AND cust_type='' AND Branchcode='' ORDER BY supplier_id
        #region Page Load
        private void FrmPurchase_Load(object sender, EventArgs e)
        {
            ObjDesign.FormDesign(this, dtgridItem);
            ObjDesign.FormDesign(this, dtgridPurchase);
            if (Globalvariable.frmName == "PurchaseReturn")
            {
                this.Text = "Purchase Return";
                FillGrid("tbTransPurchaseReturn");
                tableNM = "tbTransPurchaseReturn";
                table_detail = "tbTransPurchaseReturnDetail";
                table_Tax_detail = "tbTransPurchaseTaxDetail";
                mod.UserAccessibillityMaster("Purchase Return", BtnAdd, BtnSave, BtnDelete);
            }
            else if (Globalvariable.frmName == "CashPurchase")
            {
                this.Text = "Cash Purchase";
                FillGrid("tbCashPurchase");
                tableNM = "tbCashPurchase";
                table_detail = "tbCashPurchaseDetail";
                table_Tax_detail = "tbCashPurchaseTaxDetail";
                mod.UserAccessibillityMaster("Cash Purchase", BtnAdd, BtnSave, BtnDelete);
            }
            else if (Globalvariable.frmName == "CreditPurchase")
            {
                this.Text = "Credit Purchase";
                FillGrid("tbCreditPurchase");
                tableNM = "tbCreditPurchase";
                table_detail = "tbCreditPurchaseDetail";
                table_Tax_detail = "tbCreditPurchaseTaxDetail";
                mod.UserAccessibillityMaster("Credit Purchase", BtnAdd, BtnSave, BtnDelete);
            }
            else if (Globalvariable.frmName == "SaleReturn")
            {
                this.Text = "Sale Return";
                FillGrid("tbSaleReturn");
                tableNM = "tbSaleReturn";
                table_detail = "tbSaleReturnDetail";
                table_Tax_detail = "tbSaleReturnTaxDetail";
                mod.UserAccessibillityMaster("Sale Return", BtnAdd, BtnSave, BtnDelete);
            }
            else if (Globalvariable.frmName == "CashSale")
            {
                this.Text = "Cash Sale";
                FillGrid("tbCashSale");
                tableNM = "tbCashSale";
                table_detail = "tbCashSaleDetail";
                table_Tax_detail = "tbCashSaleTaxDetail";
                mod.UserAccessibillityMaster("Cash Sale", BtnAdd, BtnSave, BtnDelete);
            }
            else if (Globalvariable.frmName == "CreditSale")
            {
                this.Text = "Credit Sale";
                FillGrid("tbCreditSale");
                tableNM = "tbCreditSale";
                table_detail = "tbCreditSaleDetail";
                table_Tax_detail = "tbCreditSaleTaxDetail";
                mod.UserAccessibillityMaster("Credit Sale", BtnAdd, BtnSave, BtnDelete);
            }
            flag = 1;
            txtsearch.Focus();
            tabPurchase.SelectedTab = tabList;
            txtBillNo.Enabled = false;
            Globalvariable.SearchChangeVariable = "SrchByName";
        }
        #endregion
        #region Fill DataGrid
        public void FillGrid(string tableNM)
        {
            try
            {
                string strsql;
                DataTable dt = new DataTable();
                DataRow drw;
                int cd = 1;
                double samt;
                dtgridPurchase.Columns.Clear();
                // m = Globalvariable.StartDate;
                DateTime expDateconv = Convert.ToDateTime(Globalvariable.EndDate);
                DateTime startdate = Convert.ToDateTime(Globalvariable.StartDate);
                string d = Globalvariable.EndDate;
                m = startdate.Month;
                // TimeSpan span = expDateconv.Subtract(startdate);
                int months = expDateconv.Subtract(startdate).Days / 30;
                dt.Columns.Add("Month");
                dt.Columns.Add("Amount");
                while (cd <= months)
                {
                    drw = dt.NewRow();
                    if (m > 12)
                        m = 1;
                    drw["Month"] = mod.Getmonth(m);
                    //SELECT SUM(No_Of_Rooms) FROM tbReservation WHERE MONTH(FromDate)='4' AND Company_SrNo='' AND Branchcode=''
                    cmd = new SqlCommand("SP_FrmTransPurchaseSale", con.connect());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@variable", "SELECT");
                    cmd.Parameters.AddWithValue("@tablename", tableNM);
                    cmd.Parameters.AddWithValue("@checkstring", "SUM");
                    cmd.Parameters.AddWithValue("@month", m);
                    cmd.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                    cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                    samt = Convert.ToDouble(mod.Isnull(cmd.ExecuteScalar().ToString(), "0"));
                    drw["Amount"] = samt.ToString("#0.00");
                    dt.Rows.Add(drw);
                    m = m + 1;
                    cd = cd + 1;
                }
                dtgridPurchase.DataSource = dt;
                dtgridPurchase.Columns[0].Width = 461;
                dtgridPurchase.Columns[1].Width = 220;
                dtgridPurchase.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                grdchange = 0;
                escGrid = 0;
                if (dtgridPurchase.Rows.Count > 0)
                {
                    txtsearch.Text = dtgridPurchase.Rows[0].Cells[0].Value.ToString();
                }
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
                if (dtgridPurchase.Rows.Count > 0)
                {
                    int mntotal, dayTot;
                    string strsql;
                    double totlamt;
                    DateTime dtm;
                    DataTable dt = new DataTable();
                    DataRow drw;
                    if (grdchange == 0)
                    {
                        int i = dtgridPurchase.SelectedCells[0].RowIndex;
                        string cd = dtgridPurchase.Rows[i].Cells[1].Value.ToString();
                        if (dtgridPurchase.Rows[i].Cells[1].Value.ToString() != "0.00")
                        {
                            if (escGrid == 0 || escGrid == 1)
                            {
                                mnth = mod.Getmonthno(dtgridPurchase.Rows[i].Cells[0].Value.ToString());
                            }
                            dtgridPurchase.Columns.Clear();
                            dt.Columns.Add("Billing Date", typeof(string));
                            dt.Columns.Add("Year", typeof(string));
                            dt.Columns.Add("Amount", typeof(string));
                            mntotal = 0;
                            cmd = new SqlCommand("SP_FrmTransPurchaseSale", con.connect());
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@variable", "SELECT");
                            cmd.Parameters.AddWithValue("@gridchangeValue", "0");
                            // cmd.Parameters.AddWithValue("@frmname", Globalvariable.frmName);
                            cmd.Parameters.AddWithValue("@tablename", tableNM);
                            cmd.Parameters.AddWithValue("@month", mnth);
                            cmd.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                            cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                            SqlDataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                dayTot = 0;
                                DateTime pdate = Convert.ToDateTime(dr[0].ToString());
                                cmd = new SqlCommand("SP_FrmTransPurchaseSale", con.connect());
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@variable", "SELECT");
                                // cmd.Parameters.AddWithValue("@frmname", Globalvariable.frmName);
                                cmd.Parameters.AddWithValue("@gridchangeValue", "0");
                                cmd.Parameters.AddWithValue("@tablename", tableNM);
                                cmd.Parameters.AddWithValue("@checkstring", "SUM");
                                cmd.Parameters.AddWithValue("@BillDate", pdate.ToString("MM/dd/yyyy"));
                                cmd.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                                cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                                totlamt = Convert.ToDouble(mod.Isnull(cmd.ExecuteScalar().ToString(), "0"));
                                drw = dt.NewRow();
                                dtm = Convert.ToDateTime(dr[0].ToString());
                                drw["Billing Date"] = dtm.Day;
                                drw["Year"] = dtm.Year;
                                drw["Amount"] = totlamt.ToString("#0.00");
                                dt.Rows.Add(drw);
                            }
                            grdchange = 1;
                            escGrid = 0;
                            dr.Close();
                            dtgridPurchase.DataSource = dt;
                            dtgridPurchase.Columns[0].Width = 192;
                            dtgridPurchase.Columns[1].Width = 232;
                            dtgridPurchase.Columns[2].Width = 257;
                            dtgridPurchase.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                            if (dtgridPurchase.Rows.Count > 0)
                            {
                                txtsearch.Text = dtgridPurchase.Rows[0].Cells[0].Value.ToString();
                            }
                            BtnAdd.Visible = false;
                        }
                    }
                    else if (grdchange == 1)
                    {
                        int i = dtgridPurchase.SelectedCells[0].RowIndex;
                        if (Convert.ToString(dtgridPurchase.Rows[i].Cells[2].Value) != "0.00")
                        {
                            BILL_dt = mnth + "/" + dtgridPurchase.Rows[i].Cells[0].Value + "/" + dtgridPurchase.Rows[i].Cells[1].Value;

                            dtgridPurchase.Columns.Clear();
                            dt.Columns.Add("Billing Number", typeof(string));
                            dt.Columns.Add("Customer Name", typeof(string));
                            dt.Columns.Add("Amount", typeof(string));
                            //strsql = "select * from " + strHeadTable + " where BK_CODE='" + m_bkcode + "' and Company_Sr='" + gv.companysr +
                            //         "' and voch_dt='" + voch_dt + "' and Branchcode='" + gv.Brncode + "'" + gv.advRecCriteria + "";
                            cmd = new SqlCommand("SP_FrmTransPurchaseSale", con.connect());
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@variable", "SELECT");
                            //cmd.Parameters.AddWithValue("@frmname", Globalvariable.frmName);
                            cmd.Parameters.AddWithValue("@tablename", tableNM);
                            cmd.Parameters.AddWithValue("@gridchangeValue", "1");
                            cmd.Parameters.AddWithValue("@BillDate", BILL_dt);
                            cmd.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                            cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                            SqlDataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                drw = dt.NewRow();
                                drw["Billing Number"] = dr["Bill_No"].ToString();
                                drw["Amount"] = Convert.ToDouble(dr["Net_Amount"].ToString()).ToString("#0.00");
                                dt.Rows.Add(drw);
                                cmd = new SqlCommand("SP_FrmTransPurchaseSale", con.connect());
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@variable", "SELECT");
                                //cmd.Parameters.AddWithValue("@frmname", Globalvariable.frmName);
                                cmd.Parameters.AddWithValue("@gridchangeValue", "1");
                                cmd.Parameters.AddWithValue("@tablename", tableNM);
                                cmd.Parameters.AddWithValue("@checkstring", "SELECT_SupplierName");
                                cmd.Parameters.AddWithValue("@code", mod.Isnull(dr["Bill_No"].ToString(), "0"));
                                cmd.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                                cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                                SqlDataReader dr1 = cmd.ExecuteReader();
                                while (dr1.Read())
                                {
                                    drw["Customer Name"] = dr1[0].ToString();
                                }
                            }
                            grdchange = 2;
                            escGrid = 1;
                            dr.Close();
                            dtgridPurchase.DataSource = dt;
                            dtgridPurchase.Columns[0].Width = 192;
                            dtgridPurchase.Columns[1].Width = 232;
                            dtgridPurchase.Columns[2].Width = 257;
                            dtgridPurchase.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                            if (dtgridPurchase.Rows.Count > 0)
                            {
                                txtsearch.Text = dtgridPurchase.Rows[0].Cells[0].Value.ToString();
                            }
                            BtnAdd.Visible = false;
                        }
                    }
                    else if (grdchange == 2)
                    {
                        flag = 1;
                        tabPurchase.SelectedTab = tabDetail;
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
        #endregion
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
                tabPurchase.SelectedTab = tabList;
                FillGrid(tableNM);
                BtnAdd.Visible = true;
            }
            else if (grdchange == 0)
            {
                Close();
            }
        }
        #region btn Add
        public void add()
        {
            grdchange = 0;
            flag = 0;
            tabPurchase.SelectedTab = tabDetail;
            mod.txtclear(tabPurchase.TabPages[1].Controls);
            if (Globalvariable.frmName == "PurchaseReturn")
            {
                mod.addBtnClick(this, txtBillNo, "tbTransPurchaseReturn", "Bill_No", BtnSave, BtnDelete, txtSupplierID);
                mod.UserAccessibillityMaster("Purchase Return", BtnAdd, BtnSave, BtnDelete);
                //txtBillNo.Text = "" + mod.GetMaxNum("tbTransPurchaseReturn", "Bill_No");
            }
            else if (Globalvariable.frmName == "CashSale" || Globalvariable.frmName == "CashPurchase")
            {
                if (Globalvariable.frmName == "CashSale")
                {
                    mod.addBtnClick(this, txtBillNo, "tbCashCreditSale", "Bill_No", BtnSave, BtnDelete, txtSupplierID);
                    mod.UserAccessibillityMaster("Cash Sale", BtnAdd, BtnSave, BtnDelete);
                    //txtBillNo.Text = "" + mod.GetMaxNum("tbCashCreditSale", "Bill_No");
                }
                else if (Globalvariable.frmName == "CashPurchase")
                {
                    mod.addBtnClick(this, txtBillNo, "tbCashCreditPurchase", "Bill_No", BtnSave, BtnDelete, txtSupplierID);
                    mod.UserAccessibillityMaster("Cash Purchase", BtnAdd, BtnSave, BtnDelete);
                    //txtBillNo.Text = "" + mod.GetMaxNum("tbCashCreditPurchase", "Bill_No");
                }
                dr = mod.GetRecord(" SELECT supplier_name FROM tbSupplierMaster WHERE deleted='N' AND Branchcode='" + Globalvariable.bcode + "' AND supplier_id=1 ORDER BY supplier_name");
                if (dr.HasRows)
                {
                    dr.Read();
                    lblSupplierName.Text = dr["supplier_name"].ToString();
                }
                else
                {
                    txtSupplierID.Enabled = false;
                }
            }
            else if (Globalvariable.frmName == "CreditSale" || Globalvariable.frmName == "CreditPurchase")
            {
                if (Globalvariable.frmName == "CreditSale")
                {
                    mod.addBtnClick(this, txtBillNo, "tbCashCreditSale", "Bill_No", BtnSave, BtnDelete, txtSupplierID);
                    mod.UserAccessibillityMaster("Credit Sale", BtnAdd, BtnSave, BtnDelete);
                    //txtBillNo.Text = "" + mod.GetMaxNum("tbCashCreditSale", "Bill_No");
                }
                else if (Globalvariable.frmName == "CreditPurchase")
                {
                    mod.addBtnClick(this, txtBillNo, "tbCashCreditPurchase", "Bill_No", BtnSave, BtnDelete, txtSupplierID);
                    mod.UserAccessibillityMaster("Credit Purchase", BtnAdd, BtnSave, BtnDelete);
                    //txtBillNo.Text = "" + mod.GetMaxNum("tbCashCreditPurchase", "Bill_No");
                }
            }
            else if (Globalvariable.frmName == "SaleReturn")
            {
                mod.addBtnClick(this, txtBillNo, "tbSaleReturn", "Bill_No", BtnSave, BtnDelete, txtSupplierID);
                mod.UserAccessibillityMaster("Sale Return", BtnAdd, BtnSave, BtnDelete);
                //txtBillNo.Text = "" + mod.GetMaxNum("tbSaleReturn", "Bill_No");
            }
            txtBillNo.Text = "" + mod.GetMaxNum(tableNM, "Bill_No");
            dtgridItem.Rows.Clear();
            BtnPrint.Enabled = false;
            //txtSupplierName.Enabled = false;
            txtGrossAmt.Enabled = false;
            txtGstAmt.Enabled = false;
            txtDiscAmt.Enabled = false;
            txtNetAmt.Enabled = false;
            txtDisc.Enabled = false;
            dtpBillDate.Focus();
            BtnDelete.Text = "RESET";
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            add();
        }

        #endregion
        #region textbox back color
        private void txtsearch_Enter(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.Yellow;
        }

        private void txtsearch_Leave(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.White;
        }

        private void txtSupplierID_Enter(object sender, EventArgs e)
        {
            txtSupplierID.BackColor = Color.Yellow;
        }

        private void txtSupplierID_Leave(object sender, EventArgs e)
        {
            txtSupplierID.BackColor = Color.White;
        }

        private void txtDisc_Enter(object sender, EventArgs e)
        {
            txtDisc.BackColor = Color.Yellow;
        }

        private void txtDisc_Leave(object sender, EventArgs e)
        {
            txtDisc.BackColor = Color.White;
        }

        #endregion

        #region tab events
        private void tabPurchase_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SqlDataReader dr, dr1;
                string str, Pdate, stmess;
                int i = 0, j = 1, sup = 0, k;
                DataTable dt = new DataTable();
                DataRow drw;

                if (tabPurchase.SelectedTab == tabDetail)
                {
                    if (flag == 1)
                    {
                        if (dtgridPurchase.RowCount > 0)
                        {
                            k = dtgridPurchase.SelectedCells[0].RowIndex;
                            if (dtgridPurchase.Rows[k].Cells[0].Value.ToString() != "0.00" && grdchange != 1)
                            {
                                i = dtgridPurchase.SelectedCells[0].RowIndex;
                                dtgridPurchase.Text = dtgridPurchase.Rows[i].Cells[0].Value.ToString();
                                string ID = mod.Isnull(dtgridPurchase.Rows[i].Cells[0].Value.ToString(), "");
                                SqlDataReader dareader = mod.GetSelectAllField(tableNM, "Bill_No", ID, "SrNo");
                                FillDetails(dareader, "null");
                                BtnDelete.Text = "DELETE";
                                BtnSave.Text = "UPDATE";
                            }
                            else
                            {
                                MessageBox.Show("No record to display");
                                this.tabPurchase.SelectedTab = tabList;
                            }
                        }
                    }
                }
                else
                {
                    FillGrid(tableNM);
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void tabPurchase_Selecting(object sender, TabControlCancelEventArgs e)
        {
            try
            {
                if (e.TabPage.Name == "tabDetail" && grdchange != 2 && flag != 0)
                    e.Cancel = true;
                else if (e.TabPage.Name == "tabList")
                {
                    //  FillGrid(tableNM);
                    BtnAdd.Visible = true;
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }
        #endregion

        public void FillDetails(SqlDataReader dr, string strvariable)
        {
            try
            {
                int i = 0;
                string[] rstr;
                if (dr.Read())
                {
                    txtBillNo.Text = "";
                    txtBillNo.Text = dr["Bill_No"].ToString();
                    dtpBillDate.Text = dr["Bill_Date"].ToString();
                    txtDisc.Text = mod.Isnull(dr["Discount"].ToString(), "0");
                    txtSupplierID.Text = mod.Isnull(dr["Supplier_ID"].ToString(), "0");
                    rstr = GetGLName(dr["Supplier_ID"].ToString(), "tbSupplierMaster");
                    lblSupplierName.Text = rstr[0];
                    SqlDataReader dr1 = mod.GetSelectAllField(table_detail, "Bill_No", txtBillNo.Text.ToString(), "SrNo");
                    dtgridItem.Rows.Clear();
                    i = 0;
                    while (dr1.Read())
                    {
                        dtgridItem.Rows.Add();
                        dtgridItem.Rows[i].Cells[0].Value = dtgridItem.Rows.Count;
                        dtgridItem.Rows[i].Cells[1].Value = dr1["Item_ID"].ToString();
                        rstr = GetGLName(dr1["Item_ID"].ToString(), "tbItemName");
                        dtgridItem.Rows[i].Cells[2].Value = rstr[0];
                        dtgridItem.Rows[i].Cells[3].Value = mod.Isnull(dr1["Gross_Wt"].ToString(), "0");
                        dtgridItem.Rows[i].Cells[4].Value = mod.Isnull(dr1["Item_Qty"].ToString(), "0");
                        dtgridItem.Rows[i].Cells[5].Value = Convert.ToDouble(dr1["Rate"].ToString()).ToString("#0.00");

                        SqlCommand TAX_CMD = new SqlCommand("SELECT SUM(Tax_Amount) FROM tbTransPurchaseTaxDetail WHERE Bill_No='" + txtBillNo.Text + "' AND " +
                            "Item_ID='" + dr1["Item_ID"].ToString() + "' AND Company_SrNo='" + Globalvariable.company_Srno + "' AND Branchcode='" + Globalvariable.bcode + "'", con.connect());
                        SqlDataReader TAX_dr = TAX_CMD.ExecuteReader();
                        while (TAX_dr.Read())
                        {
                            dtgridItem.Rows[i].Cells[6].Value = mod.Isnull(TAX_dr[0].ToString(), "0");
                        }
                        double TotalItemAmt = Convert.ToDouble(dr1["Net_Amount"].ToString()) + Convert.ToDouble(dtgridItem.Rows[i].Cells[6].Value);
                        dtgridItem.Rows[i].Cells[7].Value = TotalItemAmt.ToString("#0.00");
                        i = i + 1;
                    }
                    dtpBillDate.Focus();
                    double TotalAmount = 0, TotalTaxAmt = 0;
                    for (int j = 0; j <= dtgridItem.Rows.Count - 1; j++)
                    {
                        TotalAmount = TotalAmount + Convert.ToDouble(mod.Isnull(dtgridItem.Rows[j].Cells[7].Value.ToString(), "0"));
                        TotalTaxAmt = TotalTaxAmt + Convert.ToDouble(mod.Isnull(dtgridItem.Rows[j].Cells[6].Value.ToString(), "0"));
                    }
                    txtGstAmt.Text = Math.Round(TotalTaxAmt, 2).ToString("#0.00");
                    double GrossAmount = Math.Round(TotalAmount, 2);
                    txtGrossAmt.Text = GrossAmount.ToString("#0.00");
                    double discAmount = Math.Round(GrossAmount * (Convert.ToDouble(mod.Isnull(txtDisc.Text, "0")) / 100), 2);
                    txtDiscAmt.Text = discAmount.ToString("#0.00");
                    double NetAmount = Math.Round(GrossAmount - Convert.ToDouble(txtDiscAmt.Text), 2);
                    txtNetAmt.Text = NetAmount.ToString("#0.00");
                }

            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private string[] GetGLName(string tno, string tableName)
        {
            string[] rtstr = new string[2];
            string[] Itemstr = new string[5];
            SqlDataReader dr1;
            if (tableName == "tbSupplierMaster")
            {
                dr1 = mod.GetRecord("SELECT supplier_name,supplier_id FROM tbSupplierMaster WHERE supplier_id='" + tno + "' AND deleted='N' AND Branchcode='" + Globalvariable.bcode + "'");
                if (dr1.HasRows)
                {
                    dr1.Read();
                    rtstr[0] = dr1["supplier_name"].ToString();
                }
                else
                {
                    rtstr[0] = "";
                }
            }
            else if (tableName == "tbItemName")
            {
                dr1 = mod.GetRecord("SELECT * FROM tbItemName WHERE Item_Code='" + tno + "' AND deleted='N' AND Branchcode='" + Globalvariable.bcode + "'");
                if (dr1.HasRows)
                {
                    dr1.Read();
                    rtstr[0] = dr1["Item_Name"].ToString();
                }
                else
                {
                    rtstr[0] = "";
                }
            }
            return (rtstr);
        }
        private void dtgridPurchase_KeyDown(object sender, KeyEventArgs e)
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
                        i = dtgridPurchase.SelectedCells[0].RowIndex + 1;
                    else if (e.KeyCode == Keys.Up)
                        i = dtgridPurchase.SelectedCells[0].RowIndex - 1;

                    if (((e.KeyCode == Keys.Down) && (i < dtgridPurchase.RowCount)) || ((e.KeyCode == Keys.Up) && (i >= 0)))
                    {
                        dtgridPurchase.Refresh();
                        dtgridPurchase.CurrentCell = dtgridPurchase.Rows[i].Cells[0];
                        dtgridPurchase.Rows[i].Selected = true;
                        if (dtgridPurchase.Rows.Count > 0)
                        {
                            txtsearch.Text = dtgridPurchase.Rows[i].Cells[0].Value.ToString();
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
        #region txtsearch Events
        private void txtsearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                dtgridPurchase_KeyDown(sender, e);
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
            mod.transSearch(txtsearch, dtgridPurchase, 0);
        }
        #endregion

        #region Frm Events
        private void FrmPurchase_KeyDown(object sender, KeyEventArgs e)
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

        private void FrmPurchase_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Control nextTab;
                if (e.KeyChar == (char)Keys.Enter)
                {
                    nextTab = ((Control)sender);
                    if (ActiveControl.Name == "txtsearch" && dtgridPurchase.Rows.Count == 0 && e.KeyChar == (char)Keys.Enter)
                    {
                        txtsearch.Focus();
                    }
                    else
                    {
                        if (ActiveControl.Name == "txtsearch" && dtgridPurchase.Rows.Count == 0)
                        {
                            nextTab = BtnAdd;
                        }
                        else if (ActiveControl.Name == "txtSupplierID")
                        {
                            if (txtSupplierID.Text == "")
                            {
                                txtSupplierID.Focus();
                            }
                            else
                            {
                                nextTab = GetNextControl(ActiveControl, true);
                            }
                            nextTab.Focus();
                        }
                        else
                        {
                            nextTab = GetNextControl(ActiveControl, true);
                            nextTab.Focus();
                        }
                        // nextTab.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

   




        #endregion

        #region Supplier ID Event
        private void txtSupplierID_KeyDown(object sender, KeyEventArgs e)
        {

            lblSupplierName.Text = "";

            var cType = Globalvariable.frmName == "CashSale" || Globalvariable.frmName == "CreditSale" ? "D" : "C";
            Globalvariable.Cust_Type = cType;
            Globalvariable.SearchString = "SupplierMaster";

            var returnForm = Models.Common.FormManagement.Instance.SearchFormOnKeyDown(ref txtSupplierID, e, "FrmPurchase", true);
            if (returnForm == null)
            {
                return;
            }


            if (returnForm.codeselected != null && returnForm.codeselected.Length > 0)
            {
                txtSupplierID.Text = returnForm.codeselected;
                lblSupplierName.Text = returnForm.descselected;
            }


            return;

            try
            {
                string custType;
                if (Globalvariable.frmName == "CashSale" || Globalvariable.frmName == "CreditSale")
                {
                    custType = "D";
                }
                else
                {
                    custType = "C";
                }
                if (txtSupplierID.Text == "" && e.KeyCode == Keys.Enter)
                {
                    Globalvariable.SearchString = "SupplierMaster";
                    KeysConverter kc = new KeysConverter();
                    string key = kc.ConvertToString(e.KeyCode);
                    FrmSearch srch = new FrmSearch();

                    if (Globalvariable.frmName == "CashSale" || Globalvariable.frmName == "CashPurchase")
                    {
                        //select code,name from supp_master where deleted='N' AND code=1 order by name
                        srchda = mod.FillSearchGrid_FrmName(Globalvariable.frmName, "tbSupplierMaster");
                    }
                    else if (Globalvariable.frmName == "CreditSale" || Globalvariable.frmName == "CreditPurchase")
                    {
                        //select code,name from supp_master where deleted='N'  and CustType=''  and code<>1 order by name
                        srchda = mod.FillSearchGrid_FrmName(Globalvariable.frmName, "tbSupplierMaster");
                    }
                    else if (Globalvariable.frmName == "PurchaseReturn" || Globalvariable.frmName == "SaleReturn")
                    {
                        // select code,name from supp_master where deleted='N'  and CustType='C'  order by name
                        srchda = mod.FillSearchGrid_FrmName(Globalvariable.frmName, "tbSupplierMaster");
                    }
                    srch.val = key;
                    srch.fillbackgridSrch(srchda, "FrmPurchase");
                    ds.Clear();
                    srchda.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                        srch.ShowDialog();
                    txtSupplierID.Text = srch.codeselected;
                    lblSupplierName.Text = srch.descselected;
                    if (srch.codeselected == null)
                    {
                        txtSupplierID.Focus();
                    }
                    else
                    {
                        LegID(custType);
                    }
                    Globalvariable.searchNo = 0;
                    if (lblSupplierName.Text != "")
                    {
                        // dtpDateofBirth.Focus();
                    }
                    else
                    {
                        txtSupplierID.Focus();
                    }
                }
                else if (e.KeyCode == Keys.Enter && txtSupplierID.Text != "" && txtSupplierID.Text != "0")
                {
                    LegID(custType);
                }
                else if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete || txtSupplierID.Text == "0")
                {
                    if (txtSupplierID.Text != "0")
                    {
                        txtSupplierID.Text = "";
                        lblSupplierName.Text = "";
                    }

                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        public void LegID(string custtype)
        {
            try
            {

                if (txtSupplierID.Text.Trim().Equals(""))
                {
                    lblSupplierName.Text = "";
                }
                else
                {

                    if (Globalvariable.frmName == "CashSale" || Globalvariable.frmName == "CashPurchase")
                    {
                        dr = mod.GetRecord(" SELECT supplier_name FROM tbSupplierMaster WHERE supplier_id='" + txtSupplierID.Text + "' AND deleted='N' AND Branchcode='" + Globalvariable.bcode + "' AND supplier_id=1 ORDER BY supplier_name");
                    }
                    else if (Globalvariable.frmName == "CreditSale" || Globalvariable.frmName == "CreditPurchase")
                    {
                        dr = mod.GetRecord(" SELECT supplier_name FROM tbSupplierMaster WHERE supplier_id='" + txtSupplierID.Text + "' AND deleted='N' AND cust_type='" + custtype + "' AND Branchcode='" + Globalvariable.bcode + "' AND supplier_id!=1 ORDER BY supplier_id");
                    }
                    else if (Globalvariable.frmName == "PurchaseReturn" || Globalvariable.frmName == "SaleReturn")
                    {
                        dr = mod.GetRecord(" SELECT supplier_name,supplier_id FROM tbSupplierMaster WHERE supplier_id='" + txtSupplierID.Text + "' AND deleted='N' AND cust_type='" + custtype + "' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY supplier_id");

                    }
                    if (dr.HasRows)
                    {
                        dr.Read();
                        lblSupplierName.Text = dr["supplier_name"].ToString();
                    }
                    else if (txtSupplierID.Text != "0")
                    {
                        txtSupplierID.Text = "";
                        lblSupplierName.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void txtSupplierID_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == '.');
                if (txtSupplierID.Enabled == true)
                {
                    if (txtSupplierID.Text == "" || txtSupplierID.Text == "0" && e.KeyChar == (char)Keys.Enter)
                    {
                        txtSupplierID.Focus();
                        // MessageBox.Show("Code Not Found In Master File,Please Check Then Proceed");
                    }
                    else if (e.KeyChar == (char)Keys.Enter && dtgridItem.Rows.Count == 0)
                    {
                        dtgridItem.Enabled = true;
                        dtgridItem.Rows.Clear();
                        dtgridItem.Rows.Add();
                        dtgridItem.Rows[0].Cells[0].Value = dtgridItem.Rows.Count;
                        dtgridItem.CurrentCell = dtgridItem[1, 0];
                        dtgridItem.BeginEdit(true);
                        e.Handled = true;
                    }
                    else if (e.KeyChar == (char)Keys.Enter && dtgridItem.Rows.Count > 0)
                    {
                        dtgridItem.Enabled = true;
                        dtgridItem.CurrentCell = dtgridItem[1, 0];
                        dtgridItem.BeginEdit(true);
                        e.Handled = true;
                    }
                }
                else
                {
                    if (e.KeyChar == (char)Keys.Enter && dtgridItem.Rows.Count == 0)
                    {
                        dtgridItem.Enabled = true;
                        dtgridItem.Rows.Clear();
                        dtgridItem.Rows.Add();
                        dtgridItem.Rows[0].Cells[0].Value = dtgridItem.Rows.Count;
                        dtgridItem.CurrentCell = dtgridItem[1, 0];
                        dtgridItem.BeginEdit(true);
                        e.Handled = true;
                    }
                    else if (e.KeyChar == (char)Keys.Enter && dtgridItem.Rows.Count > 0)
                    {
                        dtgridItem.Enabled = true;
                        dtgridItem.CurrentCell = dtgridItem[1, 0];
                        dtgridItem.BeginEdit(true);
                        e.Handled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }
        #endregion     

        #region Item Datagrid Fill
        private void dtgridItem_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            dtgridItem[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.Yellow;
        }

        private void dtgridItem_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            dtgridItem[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.White;
        }

        private void dtgridItem_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
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
                
                colNo = dtgridItem.CurrentCell.ColumnIndex;
                rowNo = dtgridItem.CurrentCell.RowIndex;

                if (colNo == 1 || colNo == 3 || colNo == 4 || colNo == 5)
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

        #endregion

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                tabPurchase.SelectedTab = tabList;
                grdchange = 0;
                FillGrid(tableNM);
                BtnAdd.Visible = true;
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
                string str, dtgrid_txtvalue;
                BtnSave.Enabled = false;
                DupCode1 = "0";
                rowNo = dtgridItem.CurrentCell.RowIndex;
                colNo = dtgridItem.CurrentCell.ColumnIndex;
                #region colm1
                if (e.KeyCode == Keys.Enter && colNo == 1)
                {
                    Globalvariable.SearchString = "MenuItem";
                    var returnForm = Models.Common.FormManagement.Instance.SearchFormOnKeyDown(ref tb, e, "FrmPurchase", true);
                    if (returnForm == null)
                    {
                        return;
                    }
                    dtgridItem.Rows[rowNo].Cells[1].Value = returnForm.codeselected;
                    dtgridItem.Rows[rowNo].Cells[2].Value = returnForm.descselected;

                    #region FillGrid
                    {
                        var dr = Models.Common.DbConnectivity.Instance.GetItemPurchaseSaleRatesWithCategoryType(returnForm.codeselected);
                        if (dr != null)
                        {
                            int i;
                            int itemn = Convert.ToInt32(dtgridItem.Rows[rowNo].Cells[1].Value);
                            chkDupFlag = false;
                            i = mod.getDuplicateRowNo(itemn, dtgridItem, 1, rowNo);
                            if (i == -1)
                            {

                                if (dr["CategoryType"].ToString().Equals("B"))
                                {

                                    dtgridItem.Rows[rowNo].Cells[4].ReadOnly = true;
                                    dtgridItem.CurrentCell = dtgridItem[3, rowNo];
                                }
                                else if (dr["CategoryType"].ToString().Equals("F"))
                                {
                                    dtgridItem.Rows[rowNo].Cells[3].ReadOnly = true;
                                    dtgridItem.CurrentCell = dtgridItem[4, rowNo];
                                }

                                double rate = 0;
                                if (Globalvariable.frmName == "CreditPurchase" || Globalvariable.frmName == "CashPurchase" || Globalvariable.frmName == "PurchaseReturn")
                                {
                                    double.TryParse(dr["Purchase_Rate"].ToString(), out rate);
                                    dtgridItem.Rows[rowNo].Cells[5].Value = rate.ToString("#0.00");
                                }
                                else if (Globalvariable.frmName == "CreditSale" || Globalvariable.frmName == "CashSale" || Globalvariable.frmName == "SaleReturn")
                                {
                                    double.TryParse(dr["Sale_Rate"].ToString(), out rate);

                                    dtgridItem.Rows[rowNo].Cells[5].Value = Convert.ToDouble(dr["Sale_Rate"]).ToString("#0.00");
                                }
                            }
                            else
                            {
                                chkDupFlag = true;
                                dtgridItem.Rows[rowNo].Cells[1].Value = "";
                                dtgridItem.Rows[rowNo].Cells[2].Value = "";
                                tb.Text = "";
                                dtgridItem.ClearSelection();
                                if (dr["CategoryType"].ToString() == "B")
                                {
                                    dtgridItem.CurrentCell = dtgridItem[3, i];
                                }
                                else if (dr["CategoryType"].ToString() == "F")
                                {
                                    dtgridItem.CurrentCell = dtgridItem[4, i];
                                }

                            }
                        }
                        else
                        {
                            chkDupFlag = true;
                            dtgridItem.Rows[rowNo].Cells[1].Value = "";
                            tb.Text = "";
                            dtgridItem.CurrentCell = dtgridItem[1, rowNo];
                            dtgridItem.BeginEdit(true);
                        }
                    }
                    #endregion
                    return;
                    Globalvariable.SearchString = "MenuItem";
                    //  dtgrid_txtvalue =mod.Isnull(dtgridItem.Rows[rowNo].Cells[1].Value.ToString(),"");
                    if (e.KeyCode == Keys.Back)
                    {
                        dtgridItem.Rows[rowNo].Cells[1].Value = null;
                    }

                    #region Select item using code
                    else if (tb.Text != string.Empty || dtgridItem.Rows[rowNo].Cells[1].Value != null)
                    {
                        dtgridItem.Rows[rowNo].Cells[1].Value = tb.Text;
                        if (e.KeyCode == Keys.Enter && tb.Text != string.Empty)
                        {
                            int i;
                            int itemn = Convert.ToInt32(dtgridItem.Rows[rowNo].Cells[1].Value);
                            chkDupFlag = false;
                            i = mod.getDuplicateRowNo(itemn, dtgridItem, 1, rowNo);
                            code = dtgridItem.Rows[rowNo].Cells[1].Value.ToString();
                            str = "SELECT * FROM tbItemName WHERE Item_Code='" + code + "' AND deleted='N' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY Item_Code";
                            SqlDataReader dr1 = mod.GetRecord(str);
                            if (i == -1)
                            {
                                if (dr1.Read())
                                {
                                    tb.Text = "";
                                    dtgridItem.ClearSelection();
                                    dtgridItem.CurrentCell = dtgridItem[1, rowNo];
                                    dtgridItem.Rows[rowNo].Cells[1].Value = dr1["Item_Code"].ToString();
                                    dtgridItem.Rows[rowNo].Cells[2].Value = dr1["Item_Name"].ToString();
                                    tb.Text = dr1["Item_Code"].ToString();
                                    if (Globalvariable.frmName == "CreditPurchase" || Globalvariable.frmName == "CashPurchase" || Globalvariable.frmName == "PurchaseReturn")
                                    {
                                        dtgridItem.Rows[rowNo].Cells[5].Value = Convert.ToDouble(dr1["Purchase_Rate"]).ToString("#0.00");
                                    }
                                    else if (Globalvariable.frmName == "CreditSale" || Globalvariable.frmName == "CashSale" || Globalvariable.frmName == "SaleReturn")
                                    {
                                        dtgridItem.Rows[rowNo].Cells[5].Value = Convert.ToDouble(dr1["Sale_Rate"]).ToString("#0.00");
                                    }
                                    if (dr1["Categry"].ToString() == "2")
                                    {
                                        dtgridItem.Rows[rowNo].Cells[4].ReadOnly = true;
                                        dtgridItem.CurrentCell = dtgridItem[3, rowNo];
                                    }
                                    else if (dr1["Categry"].ToString() == "1")
                                    {
                                        dtgridItem.Rows[rowNo].Cells[3].ReadOnly = true;
                                        dtgridItem.CurrentCell = dtgridItem[4, rowNo];
                                    }
                                }
                                else
                                {
                                    chkDupFlag = true;
                                    dtgridItem.Rows[rowNo].Cells[1].Value = "";
                                    tb.Text = "";
                                    dtgridItem.CurrentCell = dtgridItem[1, rowNo];
                                    dtgridItem.BeginEdit(true);
                                }
                            }
                            else
                            {
                                dtgridItem.Rows[rowNo].Cells[1].Value = "";
                                dtgridItem.Rows[rowNo].Cells[2].Value = "";
                                tb.Text = "";
                                if (dr1.HasRows)
                                {
                                    dtgridItem.ClearSelection();
                                    dr1.Read();
                                    if (dr1["Categry"].ToString() == "2")
                                    {
                                        dtgridItem.CurrentCell = dtgridItem[3, i];
                                    }
                                    else if (dr1["Categry"].ToString() == "1")
                                    {
                                        dtgridItem.CurrentCell = dtgridItem[4, i];
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    #region select Item Using Search Frm
                    else if (dtgridItem.Rows[rowNo].Cells[1].Value == null || dtgridItem.Rows[rowNo].Cells[1].Value.ToString().Trim() == "")
                    {
                        Globalvariable.searchNo = 1;
                        KeysConverter kc = new KeysConverter();
                        FrmSearch srch = new FrmSearch();
                        string key = kc.ConvertToString(e.KeyCode);
                        srch.val = key;
                        srchda = mod.SP_FillSearchGrid("tbItemName", "Item_Name", "Item_Code", "0");
                        srch.val = key;
                        srch.fillbackgridSrch(srchda, "FrmPurchase");
                        ds.Clear();
                        srchda.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                            srch.ShowDialog();
                        if (srch.codeselected != null)
                        {
                            tb.Text = "";
                            dtgridItem.ClearSelection();
                            dtgridItem.Rows[rowNo].Cells[1].Value = mod.Isnull(srch.codeselected, "");
                            dtgridItem.Rows[rowNo].Cells[2].Value = mod.Isnull(srch.descselected, "");
                            int i;
                            int itemn = Convert.ToInt32(dtgridItem.Rows[rowNo].Cells[1].Value);
                            chkDupFlag = false;
                            i = mod.getDuplicateRowNo(itemn, dtgridItem, 1, rowNo);
                            code = dtgridItem.Rows[rowNo].Cells[1].Value.ToString();
                            str = "SELECT * FROM tbItemName WHERE Item_Code='" + code + "' AND deleted='N' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY Item_Code";
                            SqlDataReader dr1 = mod.GetRecord(str);
                            if (i == -1)
                            {
                                if (dr1.Read())
                                {
                                    tb.Text = "";
                                    dtgridItem.ClearSelection();
                                    dtgridItem.Rows[rowNo].Cells[1].Value = dr1["Item_Code"].ToString();
                                    dtgridItem.Rows[rowNo].Cells[2].Value = dr1["Item_Name"].ToString();
                                    if (Globalvariable.frmName == "CreditPurchase" || Globalvariable.frmName == "CashPurchase" || Globalvariable.frmName == "PurchaseReturn")
                                    {
                                        dtgridItem.Rows[rowNo].Cells[5].Value = Convert.ToDouble(dr1["Purchase_Rate"]).ToString("#0.00");
                                    }
                                    else if (Globalvariable.frmName == "CreditSale" || Globalvariable.frmName == "CashSale" || Globalvariable.frmName == "SaleReturn")
                                    {
                                        dtgridItem.Rows[rowNo].Cells[5].Value = Convert.ToDouble(dr1["Sale_Rate"]).ToString("#0.00");
                                    }
                                    if (dr1["Categry"].ToString() == "2")
                                    {
                                        dtgridItem.Rows[rowNo].Cells[4].ReadOnly = true;
                                        dtgridItem.CurrentCell = dtgridItem[3, rowNo];
                                    }
                                    else if (dr1["Categry"].ToString() == "1")
                                    {
                                        dtgridItem.Rows[rowNo].Cells[3].ReadOnly = true;
                                        dtgridItem.CurrentCell = dtgridItem[4, rowNo];
                                    }
                                }
                                else
                                {
                                    chkDupFlag = true;
                                    dtgridItem.Rows[rowNo].Cells[1].Value = "";
                                    tb.Text = "";
                                    dtgridItem.CurrentCell = dtgridItem[1, rowNo];
                                    dtgridItem.BeginEdit(true);
                                }
                            }
                            else
                            {
                                chkDupFlag = true;
                                dtgridItem.Rows[rowNo].Cells[1].Value = "";
                                dtgridItem.Rows[rowNo].Cells[2].Value = "";
                                tb.Text = "";
                                if (dr1.HasRows)
                                {
                                    dtgridItem.ClearSelection();
                                    dr1.Read();
                                    if (dr1["Categry"].ToString() == "2")
                                    {
                                        dtgridItem.CurrentCell = dtgridItem[3, i];
                                    }
                                    else if (dr1["Categry"].ToString() == "1")
                                    {
                                        dtgridItem.CurrentCell = dtgridItem[4, i];
                                    }
                                }
                            }

                        }
                        else
                        {
                            dtgridItem.CurrentCell = dtgridItem[1, rowNo];
                            dtgridItem.BeginEdit(true);
                            tb.Focus();
                        }
                    }
                    #endregion

                }
                #endregion
                #region col3
                else if (e.KeyCode == Keys.Enter && colNo == 3)
                {
                    if (e.KeyCode == Keys.Enter && tb.Text != string.Empty || dtgridItem.Rows[rowNo].Cells[3].Value != null)
                    {
                        dtgridItem.CurrentCell = dtgridItem[5, rowNo];
                        double val = Convert.ToDouble(dtgridItem.Rows[rowNo].Cells[3].Value);
                        dtgridItem.Rows[rowNo].Cells[3].Value = val.ToString("#0.000");
                    }
                    else
                    {
                        dtgridItem.CurrentCell = dtgridItem[3, rowNo];
                        // dtgridItem.Rows[rowNo].Cells[3].Value
                    }
                }
                #endregion
                #region colm4
                else if (e.KeyCode == Keys.Enter && colNo == 4)
                {
                    if (e.KeyCode == Keys.Enter && tb.Text != string.Empty)
                    {
                        dtgridItem.CurrentCell = dtgridItem[5, rowNo];
                        double val = Convert.ToDouble(dtgridItem.Rows[rowNo].Cells[4].Value);
                        dtgridItem.Rows[rowNo].Cells[4].Value = val.ToString("#0.000");
                    }
                    else
                    {
                        dtgridItem.CurrentCell = dtgridItem[4, rowNo];
                        dtgridItem.BeginEdit(true);
                    }
                }
                #endregion
                #region colm5
                else if (e.KeyCode == Keys.Enter && colNo == 5)
                {
                    if (e.KeyCode == Keys.Enter && tb.Text != string.Empty)
                    {
                        dtgridItem.CurrentCell = dtgridItem[6, rowNo];
                        double val = Convert.ToDouble(dtgridItem.Rows[rowNo].Cells[5].Value);
                        dtgridItem.Rows[rowNo].Cells[4].Value = val.ToString("#0.000");
                    }
                    else
                    {
                        dtgridItem.CurrentCell = dtgridItem[5, rowNo];
                        dtgridItem.BeginEdit(true);
                    }
                }
                #endregion
                #region colm6
                else if (e.KeyCode == Keys.Enter && colNo == 6)
                {
                    //string val_colm5 = "0", val_colm1 = "0", val_colm4 = "0", val_colm3 = "0", val_colm6 = "0";
                    //double TotalAmount = 0;
                    //if (e.KeyCode == Keys.Enter && dtgridItem.Rows[rowNo].Cells[6].Value != null)
                    //{
                    //    val_colm5 = dtgridItem.Rows[rowNo].Cells[6].Value.ToString();

                    //    if (Convert.ToDouble(val_colm5) > 0)
                    //    {
                    //        //check sale return,cash sale,credit sale
                    //        //if(saletaxDetail,purchasetaxdetail)
                    //        //{
                    //        //}
                    //        /*  if(stringtaxdetai!="")
                    //          {
                    //          }*/
                    //        /* else*/
                    //        {
                    //            #region check colm3 and colm4 value
                    //            if (dtgridItem.Rows[rowNo].Cells[3].Value != null && dtgridItem.Rows[rowNo].Cells[3].Value != "0")
                    //            {
                    //                val_colm3 = dtgridItem.Rows[rowNo].Cells[3].Value.ToString();
                    //                val_colm5 = dtgridItem.Rows[rowNo].Cells[5].Value.ToString();
                    //                TotalAmount = Convert.ToDouble(val_colm3) * Convert.ToDouble(val_colm5);
                    //            }
                    //            else if (dtgridItem.Rows[rowNo].Cells[4].Value != null && dtgridItem.Rows[rowNo].Cells[4].Value != "0")
                    //            {
                    //                val_colm4 = dtgridItem.Rows[rowNo].Cells[4].Value.ToString();
                    //                val_colm5 = dtgridItem.Rows[rowNo].Cells[5].Value.ToString();
                    //                TotalAmount = Convert.ToDouble(val_colm4) * Convert.ToDouble(val_colm5);
                    //            }
                    //            #endregion
                    //            TaxPerTotal = 0;
                    //            #region Calculate Tax and show in com6
                    //            //SELECT * FROM tbKitchStockItemGroupTaxdetail INNER JOIN tbItemGroup ON tbKitchStockItemGroupTaxdetail.Group_Code=tbItemGroup.Group_Code AND tbItemGroup.ApplyTax='y' AND tbItemGroup.Group_Code IN(SELECT Group_Code FROM tbItemName WHERE Item_Code='1' AND Branchcode='')  
                    //            //string st = "SELECT tax_code FROM tbKitchStockItemGroupTaxdetail INNER JOIN tbItemGroup ON tbKitchStockItemGroupTaxdetail.Group_Code=tbItemGroup.Group_Code AND tbItemGroup.ApplyTax='Y''" +
                    //            //    "AND tbKitchStockItemGroupTaxdetail.Branchcode=tbItemGroup.Branchcode AND tbItemGroup.Group_Code IN(SELECT Group_Code FROM tbItemName WHERE Item_Code='" + dtgridItem.Rows[rowNo].Cells[1].Value + "' AND Branchcode='" + Globalvariable.bcode + "')";
                    //            SqlCommand cmdSELECT = new SqlCommand("SELECT tax_code FROM tbKitchStockItemGroupTaxdetail INNER JOIN tbItemGroup ON tbKitchStockItemGroupTaxdetail.Group_Code=tbItemGroup.Group_Code AND tbItemGroup.ApplyTax='Y'" +
                    //                "AND tbKitchStockItemGroupTaxdetail.Branchcode=tbItemGroup.Branchcode AND tbItemGroup.Group_Code IN(SELECT Group_Code FROM tbItemName WHERE Item_Code='" + dtgridItem.Rows[rowNo].Cells[1].Value + "' AND Branchcode='" + Globalvariable.bcode + "')", con.connect());
                    //            SqlDataReader dr1 = cmdSELECT.ExecuteReader();
                    //            while (dr1.Read())
                    //            {
                    //                string taxcode = mod.Isnull(dr1["tax_code"].ToString(), "0");
                    //                double slabPerVal = mod.CheckSlab(taxcode, TotalAmount);
                    //                if (slabPerVal > 0)
                    //                {
                    //                    //  TaxPerTotal = TaxPerTotal + slabPerVal;
                    //                    TaxPerTotal = TaxPerTotal + (TotalAmount * (slabPerVal / 100));
                    //                }
                    //            }
                    //            dtgridItem.Rows[rowNo].Cells[6].Value = Math.Round(TaxPerTotal, 2);
                    //            #endregion

                    //            #region Calculate Amount and Tax
                    //            if (Convert.ToDouble(val_colm4) != 0)
                    //            {
                    //                TotalAmount = Convert.ToDouble(val_colm4) * Convert.ToDouble(val_colm5);
                    //                if (dtgridItem.Rows[rowNo].Cells[6].Value != "0" || dtgridItem.Rows[rowNo].Cells[6].Value != null)
                    //                {
                    //                    TotalAmount = TotalAmount + Convert.ToDouble(dtgridItem.Rows[rowNo].Cells[6].Value);
                    //                }
                    //            }
                    //            else if (Convert.ToDouble(val_colm3) != 0)
                    //            {
                    //                TotalAmount = Convert.ToDouble(val_colm3) * Convert.ToDouble(val_colm5);
                    //                if (dtgridItem.Rows[rowNo].Cells[6].Value != "0" || dtgridItem.Rows[rowNo].Cells[6].Value != null)
                    //                {
                    //                    TotalAmount = TotalAmount + Convert.ToDouble(dtgridItem.Rows[rowNo].Cells[6].Value);
                    //                }
                    //            }
                    //            dtgridItem.Rows[rowNo].Cells[7].Value = Math.Round(TotalAmount, 2);
                    //            #endregion
                    //            TotalAmount = 0;
                    //            Total_Tax = 0;
                    //            #region Calculate All Item Amount and tax
                    //            for (int i = 0; i <= dtgridItem.Rows.Count - 1; i++)
                    //            {
                    //                if (dtgridItem.Rows[i].Cells[7].Value != null)
                    //                {
                    //                    TotalAmount = TotalAmount + Convert.ToDouble(dtgridItem.Rows[i].Cells[7].Value);
                    //                }
                    //                if (dtgridItem.Rows[i].Cells[6].Value != null)
                    //                {
                    //                    Total_Tax = Total_Tax + Convert.ToDouble(dtgridItem.Rows[i].Cells[6].Value);
                    //                }
                    //            }
                    //            #endregion

                    //            #region calculate value show in textbox
                    //            double Txt_GST = Math.Round(Total_Tax, 2);
                    //            txtGstAmt.Text = Txt_GST.ToString("#0.00");
                    //            txtDisc.Enabled = true;
                    //            double GrossAmount = Math.Round(TotalAmount, 2);
                    //            txtGrossAmt.Text = GrossAmount.ToString("#0.00");
                    //            double discAmount = Math.Round(GrossAmount * (Convert.ToDouble(mod.Isnull(txtDisc.Text, "0")) / 100), 2);
                    //            txtDiscAmt.Text = discAmount.ToString("#0.00");
                    //            double NetAmount = Math.Round(GrossAmount - Convert.ToDouble(txtDiscAmt.Text), 2);
                    //            txtNetAmt.Text = NetAmount.ToString("#0.00");
                    //            #endregion
                    //            val_colm1 = dtgridItem.Rows[rowNo].Cells[1].Value.ToString();
                    //            if (Globalvariable.frmName == "CreditPurchase" || Globalvariable.frmName == "CashPurchase" || Globalvariable.frmName == "PurchaseReturn")
                    //            {
                    //                if (val_colm1 != "0")
                    //                {
                    //                    str = "UPDATE tbItemName SET Purchase_Rate='" + dtgridItem.Rows[rowNo].Cells[5].Value + "' WHERE Item_Code=" + val_colm1 + " AND Branchcode='" + Globalvariable.bcode + "' AND Company_SrNo='" + Globalvariable.company_Srno + "'";
                    //                    SqlCommand cmd = new SqlCommand(str, con.connect());
                    //                    cmd.ExecuteNonQuery();
                    //                }
                    //            }
                    //            else if (Globalvariable.frmName == "CreditSale" || Globalvariable.frmName == "CashSale" || Globalvariable.frmName == "SaleReturn")
                    //            {
                    //                if (val_colm1 != "0")
                    //                {
                    //                    str = "UPDATE tbItemName SET Sale_Rate='" + dtgridItem.Rows[rowNo].Cells[5].Value + "' WHERE Item_Code=" + val_colm1 + " AND Branchcode='" + Globalvariable.bcode + "' AND Company_SrNo='" + Globalvariable.company_Srno + "'";
                    //                    SqlCommand cmd = new SqlCommand(str, con.connect());
                    //                    cmd.ExecuteNonQuery();
                    //                }
                    //            }
                    //            int rowcount = dtgridItem.RowCount;
                    //            if (rowcount - 1 > rowNo)
                    //            {
                    //                dtgridItem.CurrentCell = dtgridItem[1, rowNo + 1];
                    //            }
                    //            else
                    //            {
                    //                dtgridItem.Enabled = true;
                    //                dtgridItem.Rows.Add();
                    //                dtgridItem.Rows[rowNo + 1].Cells[0].Value = dtgridItem.Rows.Count;
                    //                dtgridItem.CurrentCell = dtgridItem[1, rowNo + 1];
                    //                //  dtgridItem.BeginEdit(true);                          
                    //            }
                    //        }
                    //    }
                    //}
                }
                #endregion
                //if (e.KeyCode == Keys.Add)
                //{
                //    if (dtgridItem.RowCount > 0)
                //    {
                //        //plusKeyPress = true;
                //        int i = dtgridItem.CurrentCell.RowIndex;
                //        if (Convert.ToString(dtgridItem.Rows[i].Cells[1].Value) == string.Empty)
                //        {
                //            rowNo = rowNo - 1;
                //            dtgridItem.EndEdit();
                //            dtgridItem.Rows.RemoveAt(i);
                //        }
                //        if (Convert.ToString(dtgridItem.Rows[rowNo].Cells[7].Value) != string.Empty)
                //        {
                //            //BtnSave.Enabled = true;
                //            txtDisc.Focus();
                //        }
                //        else
                //        {
                //            dtgridItem.CurrentCell = dtgridItem[1, rowNo];
                //            dtgridItem.BeginEdit(true);
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void dtgridItem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                string str, dtgrid_txtvalue = "0";
                Globalvariable.SearchString = "MenuItem";
                BtnSave.Enabled = false;
                DupCode1 = "0";
                rowNo = dtgridItem.CurrentCell.RowIndex;
                colNo = dtgridItem.CurrentCell.ColumnIndex;

                //if (e.KeyCode == Keys.Add)
                //{
                //    if (dtgridItem.RowCount > 0)
                //    {
                //        //plusKeyPress = true;
                //        int i = dtgridItem.CurrentCell.RowIndex;
                //        if (Convert.ToString(dtgridItem.Rows[i].Cells[1].Value) == string.Empty)
                //        {
                //            rowNo = rowNo - 1;
                //            dtgridItem.EndEdit();
                //            dtgridItem.Rows.RemoveAt(i);
                //        }
                //        else if (Convert.ToString(dtgridItem.Rows[rowNo].Cells[3].Value) != string.Empty)
                //        {
                //            txtDisc.Focus();
                //        }
                //        else
                //        {
                //            dtgridItem.CurrentCell = dtgridItem[3, rowNo];
                //            dtgridItem.BeginEdit(true);
                //        }
                //    }
                //    return;
                //}

                #region colm1
                if (e.KeyCode == Keys.Enter && colNo == 1)
                {

                    Globalvariable.SearchString = "MenuItem";
                    var returnForm = Models.Common.FormManagement.Instance.SearchFormOnKeyDown(ref dtgridItem, rowNo, 1, e, "FrmPurchase", true);
                    if (returnForm == null)
                    {
                        return;
                    }
                    dtgridItem.Rows[rowNo].Cells[1].Value = returnForm.codeselected;
                    dtgridItem.Rows[rowNo].Cells[2].Value = returnForm.descselected;


                    #region FillGrid
                    {
                        var dr = Models.Common.DbConnectivity.Instance.GetItemPurchaseSaleRatesWithCategoryType(returnForm.codeselected);
                        if (dr != null)
                        {
                            int i;
                            int itemn = Convert.ToInt32(dtgridItem.Rows[rowNo].Cells[1].Value);
                            chkDupFlag = false;
                            i = mod.getDuplicateRowNo(itemn, dtgridItem, 1, rowNo);
                            if (i == -1)
                            {

                                if (dr["CategoryType"].ToString().Equals("B"))
                                {

                                    dtgridItem.Rows[rowNo].Cells[4].ReadOnly = true;
                                    dtgridItem.CurrentCell = dtgridItem[3, rowNo];
                                    e.Handled = true;
                                }
                                else if (dr["CategoryType"].ToString().Equals("F"))
                                {
                                    dtgridItem.Rows[rowNo].Cells[3].ReadOnly = true;
                                    dtgridItem.CurrentCell = dtgridItem[4, rowNo];
                                    e.Handled = true;
                                }

                                double rate = 0;

                                if (Globalvariable.frmName == "CreditPurchase" || Globalvariable.frmName == "CashPurchase" || Globalvariable.frmName == "PurchaseReturn")
                                {
                                    double.TryParse(dr["Purchase_Rate"].ToString(), out rate);

                                    dtgridItem.Rows[rowNo].Cells[5].Value = rate.ToString("#0.00");
                                }
                                else if (Globalvariable.frmName == "CreditSale" || Globalvariable.frmName == "CashSale" || Globalvariable.frmName == "SaleReturn")
                                {
                                    double.TryParse(dr["Sale_Rate"].ToString(), out rate);

                                    dtgridItem.Rows[rowNo].Cells[5].Value = rate.ToString("#0.00");
                                }
                            }
                            else
                            {
                                chkDupFlag = true;
                                dtgridItem.Rows[rowNo].Cells[1].Value = "";
                                dtgridItem.Rows[rowNo].Cells[2].Value = "";
                                tb.Text = "";
                                dtgridItem.ClearSelection();
                                if (dr["CategoryType"].ToString() == "B")
                                {
                                    dtgridItem.CurrentCell = dtgridItem[3, i];
                                }
                                else if (dr["CategoryType"].ToString() == "F")
                                {
                                    dtgridItem.CurrentCell = dtgridItem[4, i];
                                }

                                e.Handled = true;
                            }
                        }
                        else
                        {
                            chkDupFlag = true;
                            dtgridItem.Rows[rowNo].Cells[1].Value = "";
                            tb.Text = "";
                            dtgridItem.CurrentCell = dtgridItem[1, rowNo];
                            e.Handled = true;
                            dtgridItem.BeginEdit(true);
                        }
                    }
                    #endregion
                    return;
                    //dtgrid_txtvalue =mod.Isnull(dtgridItem.Rows[rowNo].Cells[1].Value,"");
                    if (e.KeyCode == Keys.Back)
                    {
                        dtgridItem.Rows[rowNo].Cells[1].Value = null;
                    }
                    #region select Item Using Search Frm
                    else if (dtgridItem.Rows[rowNo].Cells[1].Value == null || dtgridItem.Rows[rowNo].Cells[1].Value.ToString().Trim() == "")
                    {
                        Globalvariable.searchNo = 1;
                        FrmSearch srch = new FrmSearch();
                        KeysConverter kc = new KeysConverter();
                        string key = kc.ConvertToString(e.KeyCode);
                        srch.val = key;
                        srchda = mod.SP_FillSearchGrid("tbItemName", "Item_Name", "Item_Code", "0");
                        srch.val = key;
                        srch.fillbackgridSrch(srchda, "FrmPurchase");
                        ds.Clear();
                        srchda.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                            srch.ShowDialog();
                        if (srch.codeselected != null)
                        {
                            tb.Text = "";
                            dtgridItem.ClearSelection();
                            dtgridItem.Rows[rowNo].Cells[1].Value = mod.Isnull(srch.codeselected, "");
                            dtgridItem.Rows[rowNo].Cells[2].Value = mod.Isnull(srch.descselected, "");
                            int i;
                            int itemn = Convert.ToInt32(dtgridItem.Rows[rowNo].Cells[1].Value);
                            chkDupFlag = false;
                            i = mod.getDuplicateRowNo(itemn, dtgridItem, 1, rowNo);
                            code = dtgridItem.Rows[rowNo].Cells[1].Value.ToString();
                            str = "SELECT * FROM tbItemName WHERE Item_Code='" + code + "' AND deleted='N' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY Item_Code";
                            SqlDataReader dr1 = mod.GetRecord(str);
                            if (i == -1)
                            {
                                if (dr1.Read())
                                {
                                    tb.Text = "";
                                    dtgridItem.ClearSelection();
                                    dtgridItem.Rows[rowNo].Cells[1].Value = dr1["Item_Code"].ToString();
                                    dtgridItem.Rows[rowNo].Cells[2].Value = dr1["Item_Name"].ToString();
                                    if (Globalvariable.frmName == "CreditPurchase" || Globalvariable.frmName == "CashPurchase" || Globalvariable.frmName == "PurchaseReturn")
                                    {
                                        dtgridItem.Rows[rowNo].Cells[5].Value = Convert.ToDouble(dr1["Purchase_Rate"]).ToString("#0.00");
                                    }
                                    else if (Globalvariable.frmName == "CreditSale" || Globalvariable.frmName == "CashSale" || Globalvariable.frmName == "SaleReturn")
                                    {
                                        dtgridItem.Rows[rowNo].Cells[5].Value = Convert.ToDouble(dr1["Sale_Rate"]).ToString("#0.00");
                                    }
                                    if (dr1["Categry"].ToString() == "2")
                                    {
                                        dtgridItem.Rows[rowNo].Cells[4].ReadOnly = true;
                                        dtgridItem.CurrentCell = dtgridItem[3, rowNo];
                                        e.Handled = true;
                                    }
                                    else if (dr1["Categry"].ToString() == "1")
                                    {
                                        dtgridItem.Rows[rowNo].Cells[3].ReadOnly = true;
                                        dtgridItem.CurrentCell = dtgridItem[4, rowNo];
                                        e.Handled = true;
                                    }
                                }
                                else
                                {
                                    chkDupFlag = true;
                                    dtgridItem.Rows[rowNo].Cells[1].Value = "";
                                    tb.Text = "";
                                    dtgridItem.CurrentCell = dtgridItem[1, rowNo];
                                    e.Handled = true;
                                    dtgridItem.BeginEdit(true);
                                }
                            }
                            else
                            {
                                chkDupFlag = true;
                                dtgridItem.Rows[rowNo].Cells[1].Value = "";
                                dtgridItem.Rows[rowNo].Cells[2].Value = "";
                                tb.Text = "";
                                if (dr1.HasRows)
                                {
                                    dtgridItem.ClearSelection();
                                    dr1.Read();
                                    if (dr1["Categry"].ToString() == "2")
                                    {
                                        dtgridItem.CurrentCell = dtgridItem[3, i];
                                    }
                                    else if (dr1["Categry"].ToString() == "1")
                                    {
                                        dtgridItem.CurrentCell = dtgridItem[4, i];
                                    }
                                }
                            }
                        }
                        else
                        {
                            dtgridItem.CurrentCell = dtgridItem[1, rowNo];
                            e.Handled = true;
                            dtgridItem.BeginEdit(true);
                            tb.Focus();
                        }
                    }
                    #endregion
                    #region Select item using code
                    else if (dtgridItem.Rows[rowNo].Cells[2].Value != null && dtgridItem.Rows[rowNo].Cells[2].Value.ToString().Trim() != "")
                    {
                        // dtgridItem.Rows[rowNo].Cells[1].Value = tb.Text;
                        if (e.KeyCode == Keys.Enter && dtgridItem.Rows[rowNo].Cells[1].Value != null)
                        {
                            int i;
                            int itemn = Convert.ToInt32(dtgridItem.Rows[rowNo].Cells[1].Value);
                            chkDupFlag = false;
                            i = mod.getDuplicateRowNo(itemn, dtgridItem, 1, rowNo);
                            if (i == -1)
                            {
                                code = dtgridItem.Rows[rowNo].Cells[1].Value.ToString();
                                str = "SELECT * FROM tbItemName WHERE Item_Code='" + code + "' AND deleted='N' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY Item_Code";
                                SqlDataReader dr1 = mod.GetRecord(str);
                                if (dr1.Read())
                                {
                                    tb.Text = "";
                                    dtgridItem.ClearSelection();
                                    dtgridItem.CurrentCell = dtgridItem[1, rowNo];
                                    dtgridItem.BeginEdit(true);
                                    dtgridItem.Rows[rowNo].Cells[1].Value = dr1["Item_Code"].ToString();
                                    dtgridItem.Rows[rowNo].Cells[2].Value = dr1["Item_Name"].ToString();
                                    tb.Text = dr1["Item_Code"].ToString();
                                    if (Globalvariable.frmName == "CreditPurchase" || Globalvariable.frmName == "CashPurchase" || Globalvariable.frmName == "PurchaseReturn")
                                    {
                                        dtgridItem.Rows[rowNo].Cells[5].Value = Convert.ToDouble(dr1["Purchase_Rate"]).ToString("#0.00");
                                    }
                                    else if (Globalvariable.frmName == "CreditSale" || Globalvariable.frmName == "CashSale" || Globalvariable.frmName == "SaleReturn")
                                    {
                                        dtgridItem.Rows[rowNo].Cells[5].Value = Convert.ToDouble(dr1["Sale_Rate"]).ToString("#0.00");
                                    }
                                    if (dr1["Categry"].ToString() == "2")
                                    {
                                        dtgridItem.Rows[rowNo].Cells[4].ReadOnly = true;
                                        dtgridItem.CurrentCell = dtgridItem[3, rowNo];
                                        e.Handled = true;
                                    }
                                    else if (dr1["Categry"].ToString() == "1")
                                    {
                                        dtgridItem.Rows[rowNo].Cells[3].ReadOnly = true;
                                        dtgridItem.CurrentCell = dtgridItem[4, rowNo];
                                        e.Handled = true;
                                    }
                                }
                                else
                                {
                                    chkDupFlag = true;
                                    dtgridItem.Rows[rowNo].Cells[1].Value = "";
                                    tb.Text = "";
                                    dtgridItem.CurrentCell = dtgridItem[1, rowNo];
                                    e.Handled = true;
                                    dtgridItem.BeginEdit(true);
                                }
                            }
                            else
                            {
                                chkDupFlag = true;
                                dtgridItem.Rows[rowNo].Cells[1].Value = "";
                                tb.Text = "";
                                dtgridItem.CurrentCell = dtgridItem[1, i];
                                e.Handled = true;
                                dtgridItem.BeginEdit(true);
                            }
                        }
                    }
                    #endregion

                }
                #endregion
                #region colm3
                else if (e.KeyCode == Keys.Enter && colNo == 3)
                {
                    if (e.KeyCode == Keys.Enter && tb.Text != string.Empty || dtgridItem.Rows[rowNo].Cells[3].Value != null)
                    {
                        dtgridItem.CurrentCell = dtgridItem[5, rowNo];
                        e.Handled = true;
                        // dtgridItem.BeginEdit(true);
                        double val = Convert.ToDouble(dtgridItem.Rows[rowNo].Cells[3].Value);
                        dtgridItem.Rows[rowNo].Cells[3].Value = val.ToString("#0.000");
                    }
                    else
                    {
                        if (dtgridItem.Rows[rowNo].Cells[3].ReadOnly == false)
                        {
                            dtgridItem.CurrentCell = dtgridItem[3, rowNo];
                            dtgridItem.BeginEdit(true);
                        }
                        else
                        {
                            dtgridItem.CurrentCell = dtgridItem[4, rowNo];
                            e.Handled = true;
                            // dtgridItem.BeginEdit(true);
                        }
                    }
                }
                #endregion
                #region colm4
                else if (e.KeyCode == Keys.Enter && colNo == 4)
                {
                    if (e.KeyCode == Keys.Enter && dtgridItem.Rows[rowNo].Cells[4].Value != null)
                    {
                        dtgridItem.CurrentCell = dtgridItem[5, rowNo];
                        e.Handled = true;
                    }
                    else
                    {
                        dtgridItem.CurrentCell = dtgridItem[4, rowNo];
                        e.Handled = true;
                    }
                }
                #endregion
                #region colm5
                else if (e.KeyCode == Keys.Enter && colNo == 5)
                {
                    if (e.KeyCode == Keys.Enter && dtgridItem.Rows[rowNo].Cells[5].Value != null)
                    {
                        dtgridItem.CurrentCell = dtgridItem[6, rowNo];
                        e.Handled = true;
                    }
                    else
                    {
                        dtgridItem.CurrentCell = dtgridItem[5, rowNo];
                        e.Handled = true;
                    }
                }
                #endregion
                #region colm6
                else if (e.KeyCode == Keys.Enter && colNo == 6)
                {

                    if (e.KeyCode == Keys.Enter && dtgridItem.Rows[rowNo].Cells[5].Value != null)
                    {
                        val_colm5 = dtgridItem.Rows[rowNo].Cells[5].Value.ToString();

                        if (Convert.ToDouble(val_colm5) > 0)
                        {
                            //check sale return,cash sale,credit sale
                            //if(saletaxDetail,purchasetaxdetail)
                            //{
                            //}
                            /*  if(stringtaxdetai!="")
                              {
                              }*/
                            /* else*/
                            {
                                #region check colm3 and colm4 value
                                if (dtgridItem.Rows[rowNo].Cells[3].Value != null && dtgridItem.Rows[rowNo].Cells[3].Value != "0")
                                {
                                    val_colm3 = dtgridItem.Rows[rowNo].Cells[3].Value.ToString();
                                    val_colm5 = dtgridItem.Rows[rowNo].Cells[5].Value.ToString();
                                    TotalAmount = Convert.ToDouble(val_colm3) * Convert.ToDouble(val_colm5);
                                }
                                else if (dtgridItem.Rows[rowNo].Cells[4].Value != null && dtgridItem.Rows[rowNo].Cells[4].Value != "0")
                                {
                                    val_colm4 = dtgridItem.Rows[rowNo].Cells[4].Value.ToString();
                                    val_colm5 = dtgridItem.Rows[rowNo].Cells[5].Value.ToString();
                                    TotalAmount = Convert.ToDouble(val_colm4) * Convert.ToDouble(val_colm5);
                                }
                                #endregion
                                TaxPerTotal = 0;
                                #region Calculate Tax and show in com6
                                //SELECT * FROM tbKitchStockItemGroupTaxdetail INNER JOIN tbItemGroup ON tbKitchStockItemGroupTaxdetail.Group_Code=tbItemGroup.Group_Code AND tbItemGroup.ApplyTax='y' AND tbItemGroup.Group_Code IN(SELECT Group_Code FROM tbItemName WHERE Item_Code='1' AND Branchcode='')  
                                //string st = "SELECT tax_code FROM tbKitchStockItemGroupTaxdetail INNER JOIN tbItemGroup ON tbKitchStockItemGroupTaxdetail.Group_Code=tbItemGroup.Group_Code AND tbItemGroup.ApplyTax='Y''" +
                                //    "AND tbKitchStockItemGroupTaxdetail.Branchcode=tbItemGroup.Branchcode AND tbItemGroup.Group_Code IN(SELECT Group_Code FROM tbItemName WHERE Item_Code='" + dtgridItem.Rows[rowNo].Cells[1].Value + "' AND Branchcode='" + Globalvariable.bcode + "')";
                                SqlCommand cmdSELECT = new SqlCommand("SELECT tax_code FROM tbKitchStockItemGroupTaxdetail INNER JOIN tbItemGroup ON tbKitchStockItemGroupTaxdetail.Group_Code=tbItemGroup.Group_Code AND tbItemGroup.ApplyTax='Y'" +
                                    "AND tbKitchStockItemGroupTaxdetail.Branchcode=tbItemGroup.Branchcode AND tbItemGroup.Group_Code IN(SELECT Group_Code FROM tbItemName WHERE Item_Code='" + dtgridItem.Rows[rowNo].Cells[1].Value + "' AND Branchcode='" + Globalvariable.bcode + "')", con.connect());
                                SqlDataReader dr1 = cmdSELECT.ExecuteReader();
                                while (dr1.Read())
                                {
                                    string taxcode = mod.Isnull(dr1["tax_code"].ToString(), "0");
                                    double slabPerVal = mod.CheckSlab(taxcode, TotalAmount);
                                    if (slabPerVal > 0)
                                    {
                                        //  TaxPerTotal = TaxPerTotal + slabPerVal;
                                        TaxPerTotal = TaxPerTotal + (TotalAmount * (slabPerVal / 100));
                                    }
                                }
                                dtgridItem.Rows[rowNo].Cells[6].Value = Math.Round(TaxPerTotal, 2);
                                #endregion

                                #region Calculate Amount and Tax
                                if (Convert.ToDouble(val_colm4) != 0)
                                {
                                    TotalAmount = Convert.ToDouble(val_colm4) * Convert.ToDouble(val_colm5);
                                    if (dtgridItem.Rows[rowNo].Cells[6].Value != "0" || dtgridItem.Rows[rowNo].Cells[6].Value != null)
                                    {
                                        TotalAmount = TotalAmount + Convert.ToDouble(dtgridItem.Rows[rowNo].Cells[6].Value);
                                    }
                                }
                                else if (Convert.ToDouble(val_colm3) != 0)
                                {
                                    TotalAmount = Convert.ToDouble(val_colm3) * Convert.ToDouble(val_colm5);
                                    if (dtgridItem.Rows[rowNo].Cells[6].Value != "0" || dtgridItem.Rows[rowNo].Cells[6].Value != null)
                                    {
                                        TotalAmount = TotalAmount + Convert.ToDouble(dtgridItem.Rows[rowNo].Cells[6].Value);
                                    }
                                }
                                dtgridItem.Rows[rowNo].Cells[7].Value = Math.Round(TotalAmount, 2);
                                #endregion
                                TotalAmount = 0;
                                Total_Tax = 0;
                                #region Calculate All Item Amount and tax
                                for (int i = 0; i <= dtgridItem.Rows.Count - 1; i++)
                                {
                                    if (dtgridItem.Rows[i].Cells[7].Value != null)
                                    {
                                        TotalAmount = TotalAmount + Convert.ToDouble(dtgridItem.Rows[i].Cells[7].Value);
                                    }
                                    if (dtgridItem.Rows[i].Cells[6].Value != null)
                                    {
                                        Total_Tax = Total_Tax + Convert.ToDouble(dtgridItem.Rows[i].Cells[6].Value);
                                    }
                                }
                                #endregion

                                #region calculate value show in textbox
                                double Txt_GST = Math.Round(Total_Tax, 2);
                                txtGstAmt.Text = Txt_GST.ToString("#0.00");
                                txtDisc.Enabled = true;
                                double GrossAmount = Math.Round(TotalAmount, 2);
                                txtGrossAmt.Text = GrossAmount.ToString("#0.00");
                                double discAmount = Math.Round(GrossAmount * (Convert.ToDouble(mod.Isnull(txtDisc.Text, "0")) / 100), 2);
                                txtDiscAmt.Text = discAmount.ToString("#0.00");
                                double NetAmount = Math.Round(GrossAmount - Convert.ToDouble(txtDiscAmt.Text), 2);
                                txtNetAmt.Text = NetAmount.ToString("#0.00");
                                #endregion
                                val_colm1 = dtgridItem.Rows[rowNo].Cells[1].Value.ToString();
                                if (Globalvariable.frmName == "CreditPurchase" || Globalvariable.frmName == "CashPurchase" || Globalvariable.frmName == "PurchaseReturn")
                                {
                                    if (val_colm1 != "0")
                                    {
                                        str = "UPDATE tbItemName SET Purchase_Rate='" + dtgridItem.Rows[rowNo].Cells[5].Value + "' WHERE Item_Code=" + val_colm1 + " AND Branchcode='" + Globalvariable.bcode + "' AND Company_SrNo='" + Globalvariable.company_Srno + "'";
                                        SqlCommand cmd = new SqlCommand(str, con.connect());
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                else if (Globalvariable.frmName == "CreditSale" || Globalvariable.frmName == "CashSale" || Globalvariable.frmName == "SaleReturn")
                                {
                                    if (val_colm1 != "0")
                                    {
                                        str = "UPDATE tbItemName SET Sale_Rate='" + dtgridItem.Rows[rowNo].Cells[5].Value + "' WHERE Item_Code=" + val_colm1 + " AND Branchcode='" + Globalvariable.bcode + "' AND Company_SrNo='" + Globalvariable.company_Srno + "'";
                                        SqlCommand cmd = new SqlCommand(str, con.connect());
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                int rowcount = dtgridItem.RowCount;
                                if (rowcount - 1 > rowNo)
                                {
                                    dtgridItem.CurrentCell = dtgridItem[1, rowNo + 1];
                                    e.Handled = true;
                                    // dtgridItem.BeginEdit(true);
                                }
                                else
                                {
                                    dtgridItem.Enabled = true;
                                    dtgridItem.Rows.Add();
                                    dtgridItem.Rows[rowNo + 1].Cells[0].Value = dtgridItem.Rows.Count;
                                    dtgridItem.CurrentCell = dtgridItem[1, rowNo + 1];
                                    //dtgridItem.BeginEdit(true);
                                    e.Handled = true;
                                }
                            }
                        }
                    }
                    #endregion

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
                if (BtnDelete.Text == "DELETE")
                {
                    DialogResult mydialog;
                    mydialog = MessageBox.Show("Are you Sure you want to delete this record", "", MessageBoxButtons.YesNo);
                    if (mydialog == DialogResult.Yes)
                    {
                        cmd = new SqlCommand("SP_FrmTransPurchaseSale", con.connect());
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@variable", "DELETE");
                        cmd.Parameters.AddWithValue("@code", txtBillNo.Text);
                        cmd.Parameters.AddWithValue("@tablename", tableNM);
                        cmd.Parameters.AddWithValue("@tablename_detail", table_detail);
                        cmd.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                        cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                        cmd.ExecuteNonQuery();
                        // txtsearch.Focus();
                        tabPurchase.SelectedTab = tabList;
                        //  FillGrid("FrmPurchase");
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

        private void txtDisc_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back);
                if (e.KeyChar == (char)Keys.Enter && txtDisc.Text != "")
                {
                    double TotalAmount = 0;
                    for (int i = 0; i <= dtgridItem.Rows.Count - 1; i++)
                    {
                        var amt = dtgridItem.Rows[i].Cells[7].Value == null || dtgridItem.Rows[i].Cells[7].Value.ToString().Trim().Length == 0 ? "0" : dtgridItem.Rows[i].Cells[7].Value.ToString();
                        TotalAmount = TotalAmount + Convert.ToDouble(amt);
                    }
                    double GrossAmount = Math.Round(TotalAmount, 2);
                    txtGrossAmt.Text = GrossAmount.ToString("#0.00");
                    double discAmount = Math.Round(GrossAmount * (Convert.ToDouble(mod.Isnull(txtDisc.Text, "0")) / 100), 2);
                    txtDiscAmt.Text = discAmount.ToString("#0.00");
                    double NetAmount = Math.Round(GrossAmount - Convert.ToDouble(txtDiscAmt.Text), 2);
                    txtNetAmt.Text = NetAmount.ToString("#0.00");
                    BtnSave.Enabled = true;
                    BtnSave.Focus();
                }
                else if (e.KeyChar == (char)Keys.Enter)
                {
                    BtnSave.Enabled = true;
                    BtnSave.Focus();
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
                if (txtSupplierID.Text == "")
                {
                    txtSupplierID.Focus();
                }
                else if (dtgridItem.Rows.Count == 0)
                {
                    dtgridItem.Enabled = true;
                    dtgridItem.Rows.Clear();
                    dtgridItem.Rows.Add();
                    dtgridItem.Rows[0].Cells[0].Value = dtgridItem.Rows.Count;
                    dtgridItem.CurrentCell = dtgridItem[1, 0];
                    dtgridItem.BeginEdit(true);
                }
                else
                {
                    cmd = new SqlCommand("SP_FrmTransPurchaseSale", con.connect());
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (BtnSave.Text == "SAVE")
                    {
                        txtBillNo.Text = "" + mod.GetMaxNum(tableNM, "Bill_No");
                        cmd.Parameters.AddWithValue("@variable", "INSERT");
                    }
                    else
                    {
                        str = "DELETE FROM " + table_detail + " WHERE Bill_No=" + txtBillNo.Text + " AND Company_Srno='" + Globalvariable.company_Srno + "'AND Branchcode='" + Globalvariable.bcode + "'";
                        //command.CommandText = str;
                        SqlCommand command = new SqlCommand(str, con.connect());
                        command.ExecuteNonQuery();
                        cmd.Parameters.AddWithValue("@variable", "UPDATE");
                    }

                    // cmd.Parameters.AddWithValue("@frmname", Globalvariable.frmName);
                    cmd.Parameters.AddWithValue("@tablename", tableNM);
                    cmd.Parameters.AddWithValue("@code", txtBillNo.Text);
                    cmd.Parameters.AddWithValue("@BillDate", dtpBillDate.Value.ToShortDateString());
                    cmd.Parameters.AddWithValue("@SupplierID", mod.Isnull(txtSupplierID.Text, "0"));
                    cmd.Parameters.AddWithValue("@discount", mod.Isnull(txtDisc.Text, "0"));
                    cmd.Parameters.AddWithValue("@Netamount", mod.Isnull(txtNetAmt.Text, "0"));
                    cmd.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                    cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                    cmd.ExecuteNonQuery();

                    for (int cnt = 0; cnt < dtgridItem.Rows.Count; cnt++)
                    {
                        if (dtgridItem.Rows[cnt].Cells[1].Value == null || dtgridItem.Rows[cnt].Cells[1].Value.ToString().Length == 0)
                        {
                            dtgridItem.Rows.RemoveAt(cnt);
                        }
                        else if (dtgridItem.Rows[cnt].Cells[7].Value == null || dtgridItem.Rows[cnt].Cells[7].Value.ToString().Length == 0)
                        {
                            dtgridItem.Rows.RemoveAt(cnt);
                        }
                        else
                        {
                            SqlCommand cmdDetail = new SqlCommand("SP_FrmTransPurchaseSale", con.connect());
                            cmdDetail.CommandType = CommandType.StoredProcedure;
                            // INSERT INTO tbTransPurchaseReturnDetail(Bill_No,Bill_Date,Item_ID,Gross_Wt,Item_Qty,Rate,Net_Amount,Company_SrNo,Branchcode) VALUES('2','02/18/2019','1','2.000','0','40.55','81.1','4','1001')
                            cmdDetail.Parameters.AddWithValue("@variable", "INSERT");
                            // cmdDetail.Parameters.AddWithValue("@frmname", Globalvariable.frmName);
                            cmdDetail.Parameters.AddWithValue("@tablename_detail", table_detail);
                            cmdDetail.Parameters.AddWithValue("@code", txtBillNo.Text);
                            cmdDetail.Parameters.AddWithValue("@BillDate", dtpBillDate.Value.ToShortDateString());
                            cmdDetail.Parameters.AddWithValue("@ItemID", mod.Isnull(dtgridItem.Rows[cnt].Cells[1].Value.ToString(), "0"));
                            if (dtgridItem.Rows[cnt].Cells[3].Value != null)
                            {
                                cmdDetail.Parameters.AddWithValue("@GrossWt", mod.Isnull(dtgridItem.Rows[cnt].Cells[3].Value.ToString(), "0"));
                            }
                            else
                            {
                                cmdDetail.Parameters.AddWithValue("@GrossWt", "0");
                            }
                            if (dtgridItem.Rows[cnt].Cells[4].Value != null)
                            {
                                cmdDetail.Parameters.AddWithValue("@ItemQty", mod.Isnull(dtgridItem.Rows[cnt].Cells[4].Value.ToString(), "0"));
                            }
                            else
                            {
                                cmdDetail.Parameters.AddWithValue("@ItemQty", "0");
                            }
                            cmdDetail.Parameters.AddWithValue("@rate", mod.Isnull(dtgridItem.Rows[cnt].Cells[5].Value.ToString(), "0"));
                            cmdDetail.Parameters.AddWithValue("@TotalAmount", mod.Isnull(dtgridItem.Rows[cnt].Cells[7].Value.ToString(), "0"));
                            cmdDetail.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                            cmdDetail.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                            cmdDetail.ExecuteNonQuery();

                            #region check colm3 and colm4 value
                            if (dtgridItem.Rows[rowNo].Cells[3].Value != null && dtgridItem.Rows[rowNo].Cells[3].Value != "0")
                            {
                                val_colm3 = dtgridItem.Rows[rowNo].Cells[3].Value.ToString();
                                val_colm5 = dtgridItem.Rows[rowNo].Cells[5].Value.ToString();
                                TotalAmount = Convert.ToDouble(val_colm3) * Convert.ToDouble(val_colm5);
                            }
                            else if (dtgridItem.Rows[rowNo].Cells[4].Value != null && dtgridItem.Rows[rowNo].Cells[4].Value != "0")
                            {
                                val_colm4 = dtgridItem.Rows[rowNo].Cells[4].Value.ToString();
                                val_colm5 = dtgridItem.Rows[rowNo].Cells[5].Value.ToString();
                                TotalAmount = Convert.ToDouble(val_colm4) * Convert.ToDouble(val_colm5);
                            }
                            #endregion
                            //Fill Tax Detail
                            SqlCommand cmdSELECT = new SqlCommand("SELECT tax_code FROM tbKitchStockItemGroupTaxdetail INNER JOIN tbItemGroup ON tbKitchStockItemGroupTaxdetail.Group_Code=tbItemGroup.Group_Code AND tbItemGroup.ApplyTax='Y'" +
                                      "AND tbKitchStockItemGroupTaxdetail.Branchcode=tbItemGroup.Branchcode AND tbItemGroup.Group_Code IN(SELECT Group_Code FROM tbItemName WHERE Item_Code='" + dtgridItem.Rows[rowNo].Cells[1].Value + "' AND Branchcode='" + Globalvariable.bcode + "')", con.connect());
                            SqlDataReader dr1 = cmdSELECT.ExecuteReader();
                            while (dr1.Read())
                            {
                                TaxPerTotal = 0;
                                string taxcode = mod.Isnull(dr1["tax_code"].ToString(), "0");
                                double slabPerVal = mod.CheckSlab(taxcode, TotalAmount);
                                if (slabPerVal > 0)
                                {
                                    //  TaxPerTotal = TaxPerTotal + slabPerVal;
                                    TaxPerTotal = TaxPerTotal + (TotalAmount * (slabPerVal / 100));
                                }
                                SqlCommand cmdTaxDetail = new SqlCommand("SP_FrmTransPurchaseSale", con.connect());
                                cmdTaxDetail.CommandType = CommandType.StoredProcedure;
                                cmdTaxDetail.Parameters.AddWithValue("@variable", "INSERT");
                                cmdTaxDetail.Parameters.AddWithValue("@tablename_Tax", table_Tax_detail);
                                cmdTaxDetail.Parameters.AddWithValue("@code", txtBillNo.Text);
                                cmdTaxDetail.Parameters.AddWithValue("@BillDate", dtpBillDate.Value.ToShortDateString());
                                cmdTaxDetail.Parameters.AddWithValue("@ItemID", mod.Isnull(dtgridItem.Rows[cnt].Cells[1].Value.ToString(), "0"));
                                cmdTaxDetail.Parameters.AddWithValue("@taxcode", taxcode);
                                cmdTaxDetail.Parameters.AddWithValue("@TaxPer", slabPerVal);
                                cmdTaxDetail.Parameters.AddWithValue("@TaxAmount", TaxPerTotal);
                                cmdTaxDetail.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                                cmdTaxDetail.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                                cmdTaxDetail.ExecuteNonQuery();
                            }
                        }
                    }
                    mod.txtclear(tabPurchase.TabPages[1].Controls);
                    BtnSave.Text = "SAVE";
                    dtgridItem.Rows.Clear();
                    txtBillNo.Text = "" + mod.GetMaxNum(tableNM, "Bill_No");
                    dtpBillDate.Focus();
                    BtnPrint.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void dtgridPurchase_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Fillgriddetails();
        }

        private void copyAlltoClipboard()
        {
            dtgridItem.SelectAll();
            DataObject dataObj = dtgridItem.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }
        private void BtnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                //if (dtgridItem.Rows.Count > 0)
                //{
                //    Microsoft.Office.Interop.Excel.ApplicationClass XcelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
                //    Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet = new Microsoft.Office.Interop.Excel.Worksheet();
                //    XcelApp.Application.Workbooks.Add(Type.Missing);

                //    for (int i = 1; i < dtgridItem.Columns.Count + 1; i++)
                //    {
                //        XcelApp.Cells[1, i] = dtgridItem.Columns[i - 1].HeaderText;
                //    }

                //    for (int i = 0; i < dtgridItem.Rows.Count; i++)
                //    {
                //        for (int j = 0; j < dtgridItem.Columns.Count; j++)
                //        {
                //            XcelApp.Cells[i + 2, j + 1] = dtgridItem.Rows[i].Cells[j].Value.ToString();
                //        }
                //    }
                //    XcelApp.Columns.AutoFit();
                //    XcelApp.Visible = true;
                //}
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void FrmPurchase_Shown(object sender, EventArgs e)
        {
            txtsearch.Focus();
        }
    }
}

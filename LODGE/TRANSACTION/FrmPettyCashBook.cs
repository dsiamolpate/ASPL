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
    public partial class FrmPettyCashBook : Form
    {
        Design ObjDesign = new Design();
        public FrmPettyCashBook()
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
        string tableName = "tbTransPettyCash";
        Connection con = new Connection();
        SqlCommand cmd;
        Module mod = new Module();
        string voch_dt, code = "";
        bool chkDupFlag;
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

        private void FrmPettyCashBook_Load(object sender, EventArgs e)
        {
            ObjDesign.FormDesign(this, dtgridGenLegCode);
            ObjDesign.FormDesign(this, dtgridPettyCash);
            flag = 1;
            txtsearch.Focus();
            tabPettyCashbook.SelectedTab = tabList;
            FillGrid("tbTransPettyCash");
            txtTransNo.Enabled = false;
            txtAmount.Enabled = false;
            mod.UserAccessibillityMaster("Petty Cash Book", BtnAdd, BtnSave, BtnDelete);
            Globalvariable.SearchChangeVariable = "SrchByName";
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            add();
        }

        public void add()
        {
            grdchange = 0;
            flag = 0;
            tabPettyCashbook.SelectedTab = tabDetail;
            mod.addBtnClick(this, txtTransNo, tableName, firstCol, BtnSave, BtnDelete, txtAmount);
            mod.txtclear(tabPettyCashbook.TabPages[1].Controls);
            dtgridGenLegCode.Rows.Clear();
            mod.UserAccessibillityMaster("Contra", BtnAdd, BtnSave, BtnDelete);
            txtTransNo.Text = "" + mod.GetMaxNum(tableName, firstCol);
            BtnPrint.Enabled = false;
            btnGreaterID.Visible = false;
            btnLessID.Visible = false;
            dtpVoucherDate.Focus();
            BtnDelete.Text = "RESET";
            //  flag = 0;
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
                dtgridPettyCash.Columns.Clear();
                DateTime expDateconv = Convert.ToDateTime(Globalvariable.EndDate);
                DateTime startdate = Convert.ToDateTime(Globalvariable.StartDate);
                string d = Globalvariable.EndDate;
                m = startdate.Month;
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
                    cmd = new SqlCommand("SP_FrmTransPettyCashBook", con.connect());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@variable", "SELECT");
                    cmd.Parameters.AddWithValue("@tablename", "tbTransPettyCash");
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
                dtgridPettyCash.DataSource = dt;
                dtgridPettyCash.Columns[0].Width = 538;
                dtgridPettyCash.Columns[1].Width = 193;
                dtgridPettyCash.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                grdchange = 0;
                escGrid = 0;
                if (dtgridPettyCash.Rows.Count > 0)
                {
                    txtsearch.Text = dtgridPettyCash.Rows[0].Cells[0].Value.ToString();
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
                tabPettyCashbook.SelectedTab = tabList;
                grdchange = 0;
                FillGrid("tbTransPettyCash");
                BtnAdd.Visible = true;
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
                cmd = new SqlCommand("SP_FrmTransPettyCashBook", con.connect());
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

        private void btnGreaterID_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new SqlCommand("SP_FrmTransPettyCashBook", con.connect());
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

        void Fillgriddetails()
        {
            try
            {
                if (dtgridPettyCash.Rows.Count > 0)
                {
                    int mntotal, dayTot;
                    string strsql;
                    double totlamt;
                    DateTime dtm;
                    DataTable dt = new DataTable();
                    DataRow drw;
                    if (grdchange == 0)
                    {
                        int i = dtgridPettyCash.SelectedCells[0].RowIndex;
                        string cd = dtgridPettyCash.Rows[i].Cells[1].Value.ToString();
                        if (dtgridPettyCash.Rows[i].Cells[1].Value.ToString() != "0.00")
                        {
                            if (escGrid == 0 || escGrid == 1)
                            {
                                mnth = mod.Getmonthno(dtgridPettyCash.Rows[i].Cells[0].Value.ToString());
                            }
                            dtgridPettyCash.Columns.Clear();
                            dt.Columns.Add("Voucher Date", typeof(string));
                            dt.Columns.Add("Year", typeof(string));
                            dt.Columns.Add("Amount", typeof(string));
                            mntotal = 0;
                            cmd = new SqlCommand("SP_FrmTransPettyCashBook", con.connect());
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
                                cmd = new SqlCommand("SP_FrmTransPettyCashBook", con.connect());
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
                            dtgridPettyCash.DataSource = dt;
                            dtgridPettyCash.Columns[0].Width = 120;
                            dtgridPettyCash.Columns[1].Width = 416;
                            dtgridPettyCash.Columns[2].Width = 195;
                            dtgridPettyCash.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                            if (dtgridPettyCash.Rows.Count > 0)
                            {
                                txtsearch.Text = dtgridPettyCash.Rows[0].Cells[0].Value.ToString();
                            }
                            BtnAdd.Visible = false;
                        }
                    }
                    else if (grdchange == 1)
                    {
                        int i = dtgridPettyCash.SelectedCells[0].RowIndex;
                        if (Convert.ToString(dtgridPettyCash.Rows[i].Cells[2].Value) != "0.00")
                        {
                            voch_dt = mnth + "/" + dtgridPettyCash.Rows[i].Cells[0].Value + "/" + dtgridPettyCash.Rows[i].Cells[1].Value;

                            dtgridPettyCash.Columns.Clear();
                            dt.Columns.Add("Voucher No", typeof(string));
                            dt.Columns.Add("Amount", typeof(string));

                            //strsql = "select * from " + strHeadTable + " where BK_CODE='" + m_bkcode + "' and Company_Sr='" + gv.companysr +
                            //         "' and voch_dt='" + voch_dt + "' and Branchcode='" + gv.Brncode + "'" + gv.advRecCriteria + "";
                            cmd = new SqlCommand("SP_FrmTransPettyCashBook", con.connect());
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
                            dtgridPettyCash.DataSource = dt;
                            dtgridPettyCash.Columns[0].Width = 365;
                            dtgridPettyCash.Columns[1].Width = 367;
                            dtgridPettyCash.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                            if (dtgridPettyCash.Rows.Count > 0)
                            {
                                txtsearch.Text = dtgridPettyCash.Rows[0].Cells[0].Value.ToString();
                            }
                            BtnAdd.Visible = false;
                        }
                    }
                    else if (grdchange == 2)
                    {
                        flag = 1;
                        tabPettyCashbook.SelectedTab = tabDetail;
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
                    txtAmount.Text = Convert.ToDouble(dr["Amount"].ToString()).ToString("#0.00");
                    SqlDataReader dr1 = mod.GetSelectAllField("tbTransPettyCashDetail", "Voucher_No", txtTransNo.Text.ToString(), "SrNo");
                    dtgridGenLegCode.Rows.Clear();
                    i = 0;
                    while (dr1.Read())
                    {
                        dtgridGenLegCode.Rows.Add();
                        dtgridGenLegCode.Rows[i].Cells[0].Value = dr1["GenLeg_Code"].ToString();
                        string[] rstr;
                        rstr = GetGLName(dr1["GenLeg_Code"].ToString());
                        dtgridGenLegCode.Rows[i].Cells[1].Value = rstr[0];
                        dtgridGenLegCode.Rows[i].Cells[2].Value = dr1["Narration"].ToString();
                        dtgridGenLegCode.Rows[i].Cells[3].Value = Convert.ToDouble(dr1["Amount"].ToString()).ToString("#0.00"); ;        
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
          //  SELECT genledg_desc,genledg_code FROM tbGeneralLedger WHERE deleted='N' AND Branchcode='"+Globalvariable.bcode+"' AND grp_code IN(SELECT grp_code FROM tbGroup WHERE under_code IN (1,2)) AND genledg_code='"+code+"'";
            SqlDataReader dr1 = mod.GetRecord("SELECT genledg_desc,genledg_code FROM tbGeneralLedger WHERE genledg_code='" + tno + "' AND deleted='N' AND Branchcode='" + Globalvariable.bcode + "'");
            if (dr1.HasRows)
            {
                dr1.Read();
                rtstr[0] = dr1["genledg_desc"].ToString();
                return (rtstr);
            }
            else
            {
                rtstr[0] = "";
                return (rtstr);
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
                Fillgriddetails();
            }
            else if (grdchange == 1)
            {
                grdchange = 0;
                txtsearch.Focus();
                tabPettyCashbook.SelectedTab = tabList;
                FillGrid("tbTransPettyCash");
                BtnAdd.Visible = true;
            }
            else if (grdchange == 0)
            {
                Close();
            }
        }

        private void dtgridGenLegCode_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            dtgridGenLegCode[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.Yellow;
        }

        private void dtgridGenLegCode_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            dtgridGenLegCode[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.White;
        }

        private void dtpVoucherDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && dtgridGenLegCode.Rows.Count == 0)
            {

                dtgridGenLegCode.Rows.Add();
                dtgridGenLegCode.CurrentCell = dtgridGenLegCode[0, 0];
                dtgridGenLegCode.BeginEdit(true);
                e.Handled = true;
            }
            else if (e.KeyChar == (char)Keys.Enter && dtgridGenLegCode.Rows.Count > 0)
            {
                dtgridGenLegCode.Enabled = true;
                dtgridGenLegCode.CurrentCell = dtgridGenLegCode[0, 0];
                dtgridGenLegCode.BeginEdit(true);
                e.Handled = true;
            }  
           

        }

        private void dtgridGenLegCode_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
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

        private void dataGridViewTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                TextBox obj = sender as TextBox;
                colNo = dtgridGenLegCode.CurrentCell.ColumnIndex;
                rowNo = dtgridGenLegCode.CurrentCell.RowIndex;
                if (colNo == 0 || colNo == 3 )
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

        private void dataGridViewTextBox_GotFocus(object sender, EventArgs e)
        {
            TextBox obj = sender as TextBox;
            obj.BackColor = Color.Yellow;
            obj.Focus();
        }

        private void Control_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                string str;
                BtnSave.Enabled = false;
                rowNo = dtgridGenLegCode.CurrentCell.RowIndex;
                colNo = dtgridGenLegCode.CurrentCell.ColumnIndex;
                e.IsInputKey = true;
                if (colNo == 0)
                {
                    if (e.KeyCode == Keys.Back)
                    {
                        dtgridGenLegCode.Rows[rowNo].Cells[0].Value = null;
                    }
                    else if (e.KeyCode == Keys.Enter && tb.Text != string.Empty)
                    {
                        dtgridGenLegCode.Rows[rowNo].Cells[0].Value = tb.Text;
                        if (dtgridGenLegCode.Rows[rowNo].Cells[0].Value != null && dtgridGenLegCode.Rows[rowNo].Cells[0].Value.ToString() != "")
                        {
                            int i;
                            int itemn = Convert.ToInt32(dtgridGenLegCode.Rows[rowNo].Cells[0].Value);
                            chkDupFlag = false;
                            i = mod.getDuplicateRowNo(itemn, dtgridGenLegCode, 0, rowNo);
                            if (i == -1)
                            {
                                code = dtgridGenLegCode.Rows[rowNo].Cells[0].Value.ToString();
                                str = "SELECT genledg_desc,genledg_code FROM tbGeneralLedger WHERE deleted='N' AND Branchcode='"+Globalvariable.bcode+"' AND grp_code IN(SELECT grp_code FROM tbGroup WHERE under_code IN (1,2)) AND genledg_code='"+code+"'";
                                SqlDataReader dr1 = mod.GetRecord(str);
                                if (dr1.Read())
                                {
                                    tb.Text = "";
                                    dtgridGenLegCode.ClearSelection();
                                    dtgridGenLegCode.CurrentCell = dtgridGenLegCode[0, rowNo];
                                    dtgridGenLegCode.BeginEdit(true);
                                    dtgridGenLegCode.Rows[rowNo].Cells[0].Value = dr1["genledg_code"].ToString();
                                    dtgridGenLegCode.Rows[rowNo].Cells[1].Value = dr1["genledg_desc"].ToString();
                                    tb.Text = dr1["genledg_code"].ToString();
                                    //Add row in grid
                                    dtgridGenLegCode.Enabled = true;
                                    dtgridGenLegCode.CurrentCell = dtgridGenLegCode[2, rowNo];
                                   
                                }
                                else
                                {
                                    chkDupFlag = true;
                                    dtgridGenLegCode.Rows[rowNo].Cells[0].Value = "";
                                    tb.Text = "";
                                    // dtgridBookcode.ClearSelection();
                                    dtgridGenLegCode.CurrentCell = dtgridGenLegCode[0, rowNo];
                                    dtgridGenLegCode.BeginEdit(true);
                                }
                            }
                            else
                            {
                                chkDupFlag = true;
                                dtgridGenLegCode.Rows[rowNo].Cells[0].Value = "";
                                tb.Text = "";
                                //  dtgridBookcode.ClearSelection();
                                dtgridGenLegCode.CurrentCell = dtgridGenLegCode[0, rowNo];
                                dtgridGenLegCode.BeginEdit(true);
                            }
                        }
                        else if (tb.Text != string.Empty && dtgridGenLegCode.Rows[rowNo].Cells[1].Value.ToString() != "")
                        {
                            dtgridGenLegCode.CurrentCell = dtgridGenLegCode[2, rowNo];
                            // dtgridBookcode.BeginEdit(false);                     
                        }
                    }
                    else if (tb.Text == string.Empty && e.KeyCode == Keys.Enter)
                    {                     
                        KeysConverter kc = new KeysConverter();
                        string key = kc.ConvertToString(e.KeyCode);
                        FrmSearch srch = new FrmSearch();
                       //SELECT genledg_desc,genledg_code FROM tbGeneralLedger WHERE deleted='N' AND Branchcode='1001' AND grp_code IN(SELECT grp_code FROM tbGroup WHERE under_code IN (1,2))ORDER BY genledg_code
                        srch.val = key;
                        cmd = new SqlCommand("SP_SEARCHFORM", con.connect());
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@frmName", "FrmPettyCash");
                        cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                        SqlDataAdapter srchda = new SqlDataAdapter(cmd);
                        srch.val = key;
                        srch.fillbackgridSrch(srchda,"FrmPettyCashBook");
                        ds.Clear();
                        srchda.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                            srch.ShowDialog();
                        if (srch.codeselected != null)
                        {
                            tb.Text = "";
                            dtgridGenLegCode.ClearSelection();
                            dtgridGenLegCode.CurrentCell = dtgridGenLegCode[0, rowNo];
                            dtgridGenLegCode.Rows[rowNo].Cells[0].Value = mod.Isnull(srch.codeselected, "");
                            dtgridGenLegCode.Rows[rowNo].Cells[1].Value = mod.Isnull(srch.descselected, "");
                            //Add row in grid
                            dtgridGenLegCode.Enabled = true;
                            dtgridGenLegCode.CurrentCell = dtgridGenLegCode[2, rowNo];
                          
                        }
                        else
                        {
                            dtgridGenLegCode.CurrentCell = dtgridGenLegCode[0, rowNo];
                            dtgridGenLegCode.BeginEdit(true);
                            tb.Focus();
                        }
                    }
                    else if (e.KeyCode == Keys.Enter && tb.Text != string.Empty && dtgridGenLegCode.Rows[rowNo].Cells[1].Value != null)
                    {
                        // string k = itemMastGrid.Rows[rowNo].Cells[1].Value.ToString();
                        int rowcount = dtgridGenLegCode.Rows.Count;
                        if (rowNo == rowcount - 1)
                        {
                            // int val;                           
                            rowNo = dtgridGenLegCode.RowCount - 1;
                            if (dtgridGenLegCode.Rows[rowNo].Cells[0].Value != null)
                            {
                                dtgridGenLegCode.Rows.Add();
                                rowNo = dtgridGenLegCode.RowCount - 1;
                                dtgridGenLegCode.CurrentCell = dtgridGenLegCode[0, rowNo];
                                dtgridGenLegCode.BeginEdit(true);
                            }
                            else
                            {
                                dtgridGenLegCode.CurrentCell = dtgridGenLegCode[0, rowNo];
                                dtgridGenLegCode.BeginEdit(true);
                            }
                        }
                    }
                }
                else if (colNo == 2 && e.KeyCode == Keys.Enter)
                {
                    if (dtgridGenLegCode.Rows[rowNo].Cells[2].Value != null && dtgridGenLegCode.Rows[rowNo].Cells[2].Value.ToString() != "")
                    {
                        dtgridGenLegCode.CurrentCell = dtgridGenLegCode[3, rowNo];
                        dtgridGenLegCode.BeginEdit(true);
                    }
                    else
                    {
                        dtgridGenLegCode.CurrentCell = dtgridGenLegCode[2, rowNo];
                    }
                }
                else if (colNo == 3 && e.KeyCode == Keys.Enter)
                {
                    if (dtgridGenLegCode.Rows[rowNo].Cells[3].Value != null && dtgridGenLegCode.Rows[rowNo].Cells[3].Value.ToString() != "")
                    {
                        dtgridGenLegCode.Rows.Add();
                        rowNo = dtgridGenLegCode.RowCount - 1;
                        dtgridGenLegCode.CurrentCell = dtgridGenLegCode[0, rowNo];
                        dtgridGenLegCode.BeginEdit(true);
                    }
                    else
                    {
                        dtgridGenLegCode.CurrentCell = dtgridGenLegCode[3, rowNo];
                    }
                }
                if (e.KeyCode == Keys.Add)
                {
                    if (dtgridGenLegCode.RowCount > 0)
                    {
                        //plusKeyPress = true;
                        int i = dtgridGenLegCode.CurrentCell.RowIndex;
                        if (Convert.ToString(dtgridGenLegCode.Rows[i].Cells[1].Value) == string.Empty)
                        {
                            rowNo = rowNo - 1;
                            dtgridGenLegCode.Rows.RemoveAt(i);
                        }
                        if (Convert.ToString(dtgridGenLegCode.Rows[rowNo].Cells[3].Value) != string.Empty)
                        {
                            BtnSave.Enabled = true;
                            BtnSave.Focus();
                        }
                        else
                        {
                            dtgridGenLegCode.CurrentCell = dtgridGenLegCode[3, rowNo];
                            dtgridGenLegCode.BeginEdit(true);
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

        private void dtgridGenLegCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                string str;
                BtnSave.Enabled = false;
                rowNo = dtgridGenLegCode.CurrentCell.RowIndex;
                colNo = dtgridGenLegCode.CurrentCell.ColumnIndex;
               if(colNo == 0)
                {
                    if (e.KeyCode == Keys.Back)
                    {
                        dtgridGenLegCode.Rows[rowNo].Cells[0].Value = null;
                    }
                    else if (e.KeyCode == Keys.Enter && tb.Text != string.Empty)
                    {
                       // dtgridGenLegCode.Rows[rowNo].Cells[0].Value = tb.Text;
                        if (dtgridGenLegCode.Rows[rowNo].Cells[0].Value != null && dtgridGenLegCode.Rows[rowNo].Cells[0].Value.ToString() != "")
                        {
                            int i;
                            int itemn = Convert.ToInt32(dtgridGenLegCode.Rows[rowNo].Cells[0].Value);
                            chkDupFlag = false;
                            i = mod.getDuplicateRowNo(itemn, dtgridGenLegCode, 0, rowNo);
                            if (i == -1)
                            {
                                code = dtgridGenLegCode.Rows[rowNo].Cells[0].Value.ToString();
                                str = "SELECT genledg_desc,genledg_code FROM tbGeneralLedger WHERE deleted='N' AND Branchcode='" + Globalvariable.bcode + "' AND grp_code IN(SELECT grp_code FROM tbGroup WHERE under_code IN (1,2)) AND genledg_code='" + code + "'";
                                SqlDataReader dr1 = mod.GetRecord(str);
                                if (dr1.Read())
                                {
                                    tb.Text = "";
                                    dtgridGenLegCode.ClearSelection();
                                    dtgridGenLegCode.CurrentCell = dtgridGenLegCode[0, rowNo];
                                    dtgridGenLegCode.BeginEdit(true);
                                    dtgridGenLegCode.Rows[rowNo].Cells[0].Value = dr1["genledg_code"].ToString();
                                    dtgridGenLegCode.Rows[rowNo].Cells[1].Value = dr1["genledg_desc"].ToString();
                                    tb.Text = dr1["genledg_code"].ToString();
                                    //Add row in grid
                                    dtgridGenLegCode.Enabled = true;
                                    dtgridGenLegCode.CurrentCell = dtgridGenLegCode[2, rowNo];
                                    e.Handled = true;
                                }
                                else
                                {
                                    chkDupFlag = true;
                                    dtgridGenLegCode.Rows[rowNo].Cells[0].Value = "";
                                    tb.Text = "";
                                    dtgridGenLegCode.CurrentCell = dtgridGenLegCode[0, rowNo];
                                    dtgridGenLegCode.BeginEdit(true);
                                    e.Handled = true;
                                }
                            }
                            else
                            {
                                chkDupFlag = true;
                                dtgridGenLegCode.Rows[rowNo].Cells[0].Value = "";
                                tb.Text = "";
                                dtgridGenLegCode.CurrentCell = dtgridGenLegCode[0, rowNo];
                                dtgridGenLegCode.BeginEdit(true);
                                e.Handled = true;
                            }
                        }
                        else if (tb.Text != string.Empty && dtgridGenLegCode.Rows[rowNo].Cells[1].Value.ToString() != "")
                        {
                            dtgridGenLegCode.CurrentCell = dtgridGenLegCode[2, rowNo];
                        }
                    }
                    else if (tb.Text == string.Empty && e.KeyCode == Keys.Enter)
                    {
                        KeysConverter kc = new KeysConverter();
                        string key = kc.ConvertToString(e.KeyCode);
                        FrmSearch srch = new FrmSearch();
                        srch.val = key;                     
                        SqlDataAdapter srchda = mod.FillSearchGrid_FrmName("FrmPettyCash", "tbGeneralLedger");
                        srch.val = key;
                        srch.fillbackgridSrch(srchda, "FrmPettyCashBook");
                        ds.Clear();
                        srchda.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                            srch.ShowDialog();
                        if (srch.codeselected != null)
                        {
                            tb.Text = "";
                            dtgridGenLegCode.ClearSelection();
                            dtgridGenLegCode.CurrentCell = dtgridGenLegCode[0, rowNo];
                            dtgridGenLegCode.Rows[rowNo].Cells[0].Value = mod.Isnull(srch.codeselected, "");
                            dtgridGenLegCode.Rows[rowNo].Cells[1].Value = mod.Isnull(srch.descselected, "");
                            //Add row in grid
                            dtgridGenLegCode.Enabled = true;
                            dtgridGenLegCode.CurrentCell = dtgridGenLegCode[2, rowNo];
                            e.Handled = true;
                        }
                        else
                        {
                            dtgridGenLegCode.CurrentCell = dtgridGenLegCode[0, rowNo];
                            dtgridGenLegCode.BeginEdit(true);
                            tb.Focus();
                        }
                    }
                    else if (e.KeyCode == Keys.Enter && tb.Text != string.Empty && dtgridGenLegCode.Rows[rowNo].Cells[1].Value != null)
                    {
                        int rowcount = dtgridGenLegCode.Rows.Count;
                        if (rowNo == rowcount - 1)
                        {
                            rowNo = dtgridGenLegCode.RowCount - 1;
                            if (dtgridGenLegCode.Rows[rowNo].Cells[0].Value != null)
                            {
                                dtgridGenLegCode.Rows.Add();
                                rowNo = dtgridGenLegCode.RowCount - 1;
                                dtgridGenLegCode.CurrentCell = dtgridGenLegCode[0, rowNo];
                                dtgridGenLegCode.BeginEdit(true);
                                e.Handled = true;
                            }
                            else
                            {
                                dtgridGenLegCode.CurrentCell = dtgridGenLegCode[0, rowNo];
                                dtgridGenLegCode.BeginEdit(true);
                                e.Handled = true;
                            }
                        }
                    }
                }
                else if (colNo == 2 && e.KeyCode == Keys.Enter)
                {
                    if (dtgridGenLegCode.Rows[rowNo].Cells[2].Value != null && dtgridGenLegCode.Rows[rowNo].Cells[2].Value.ToString() != "")
                    {
                        dtgridGenLegCode.CurrentCell = dtgridGenLegCode[3, rowNo];
                        //dtgridGenLegCode.BeginEdit(true);
                        e.Handled = true;
                    }
                    else
                    {
                        dtgridGenLegCode.CurrentCell = dtgridGenLegCode[2, rowNo];
                        e.Handled = true;
                    }
                }
               else if (colNo == 3 && e.KeyCode == Keys.Enter)
               {
                   if (dtgridGenLegCode.Rows[rowNo].Cells[3].Value != null && dtgridGenLegCode.Rows[rowNo].Cells[3].Value.ToString() != "")
                   {
                       int rowcount = dtgridGenLegCode.RowCount;
                       if (rowcount - 1 > rowNo)
                       {
                           dtgridGenLegCode.CurrentCell = dtgridGenLegCode[0, rowNo + 1];
                           e.Handled = true;
                           //dtGrdPurdtl.BeginEdit(true);
                       }
                       else
                       {
                           dtgridGenLegCode.Rows.Add();
                           rowNo = dtgridGenLegCode.RowCount - 1;
                           dtgridGenLegCode.CurrentCell = dtgridGenLegCode[0, rowNo];
                           dtgridGenLegCode.BeginEdit(true);
                       }
                   }
                   else
                   {
                       dtgridGenLegCode.CurrentCell = dtgridGenLegCode[3, rowNo];
                       e.Handled = true;
                   }
               }
               if (e.KeyCode == Keys.Add)
               {
                   if (dtgridGenLegCode.RowCount > 0)
                   {
                       //plusKeyPress = true;
                       int i = dtgridGenLegCode.CurrentCell.RowIndex;
                       if (Convert.ToString(dtgridGenLegCode.Rows[i].Cells[1].Value) == string.Empty)
                       {
                           rowNo = rowNo - 1;
                           dtgridGenLegCode.Rows.RemoveAt(i);
                       }
                       if (Convert.ToString(dtgridGenLegCode.Rows[rowNo].Cells[3].Value) != string.Empty)
                       {
                           BtnSave.Enabled = true;
                           BtnSave.Focus();
                       }
                       else
                       {
                           dtgridGenLegCode.CurrentCell = dtgridGenLegCode[3, rowNo];
                           dtgridGenLegCode.BeginEdit(true);
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

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                double TotalAmount = 0;           
                SqlCommand cmd;
                string str;
                SqlCommand command = new SqlCommand();
                DateTime ST_date = Convert.ToDateTime(Globalvariable.StartDate);
                DateTime END_date = Convert.ToDateTime(Globalvariable.EndDate);
                 if (dtpVoucherDate.Value < ST_date || dtpVoucherDate.Value > END_date)
                {
                    MessageBox.Show("Voch Date Does Not Come In Financial Year.");
                    dtpVoucherDate.Focus();
                }
                else if (dtgridGenLegCode.Rows.Count >0)
                {
                    cmd = new SqlCommand("SP_FrmTransPettyCashBook", con.connect());
                    cmd.CommandType = CommandType.StoredProcedure;
                    for (int cnt = 0; cnt < dtgridGenLegCode.Rows.Count; cnt++)
                    {
                        TotalAmount = TotalAmount + Convert.ToDouble(dtgridGenLegCode.Rows[cnt].Cells[3].Value);
                    }
                    if (BtnSave.Text == "SAVE")
                    {
                        txtTransNo.Text = "" + mod.GetMaxNum(tableName, firstCol);
                        cmd.Parameters.AddWithValue("@variable", "INSERT");
                    }
                    else
                    {
                        str = "DELETE FROM tbTransPettyCashDetail WHERE Voucher_No=" + txtTransNo.Text + " AND Company_Srno='" + Globalvariable.company_Srno + "'AND Branchcode='" + Globalvariable.bcode + "'";
                        //command.CommandText = str;
                        command = new SqlCommand(str, con.connect());
                        command.ExecuteNonQuery();
                        cmd.Parameters.AddWithValue("@variable", "UPDATE");
                    }
                    cmd.Parameters.AddWithValue("@code", txtTransNo.Text);
                    cmd.Parameters.AddWithValue("@Voucherdate", dtpVoucherDate.Value.ToShortDateString());
                    cmd.Parameters.AddWithValue("@TotalAmount", mod.Isnull(Convert.ToString(TotalAmount), "0"));
                        cmd.Parameters.AddWithValue("@userID", Globalvariable.usercd);
                    cmd.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                    cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                    cmd.ExecuteNonQuery();

                    for (int cnt = 0; cnt < dtgridGenLegCode.Rows.Count; cnt++)
                    {
                        if (dtgridGenLegCode.Rows[cnt].Cells[0].Value == null)
                        {
                            dtgridGenLegCode.Rows.RemoveAt(cnt);
                        }
                        else
                        {
                            SqlCommand cmdGlcode = new SqlCommand("SP_FrmTransPettyCashBook", con.connect());
                            cmdGlcode.CommandType = CommandType.StoredProcedure;
                            cmdGlcode.Parameters.AddWithValue("@variable", "INSERTPetty");
                            cmdGlcode.Parameters.AddWithValue("@code", txtTransNo.Text);
                            cmdGlcode.Parameters.AddWithValue("@Voucherdate", dtpVoucherDate.Value.ToShortDateString());
                            cmdGlcode.Parameters.AddWithValue("@glCode", dtgridGenLegCode.Rows[cnt].Cells[0].Value.ToString());
                            cmdGlcode.Parameters.AddWithValue("@narration", dtgridGenLegCode.Rows[cnt].Cells[2].Value);
                            cmdGlcode.Parameters.AddWithValue("@TotalAmount", dtgridGenLegCode.Rows[cnt].Cells[3].Value);
                            cmdGlcode.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                            cmdGlcode.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                            cmdGlcode.ExecuteNonQuery();
                        }
                    }
                    mod.txtclear(tabPettyCashbook.TabPages[1].Controls);
                    BtnSave.Text = "SAVE";
                    dtgridGenLegCode.Rows.Clear();
                    txtTransNo.Text = "" + mod.GetMaxNum(tableName, firstCol);
                    btnGreaterID.Visible = false;
                    btnLessID.Visible = false;
                    dtpVoucherDate.Focus();
                    BtnPrint.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }    

        private void dtgridPettyCash_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Fillgriddetails();
        }

        private void txtsearch_Enter(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.Yellow;
        }

        private void txtsearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && txtsearch.Text == "")
            {
                BtnAdd.Focus();
            }
        }

        private void txtsearch_Leave(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.White;
        }

        private void txtsearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                dtgridPettyCash_KeyDown_1(sender, e);
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            mod.transSearch(txtsearch, dtgridPettyCash, 0);
        }

        private void FrmPettyCashBook_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Control nextTab;
                if (e.KeyChar == (char)Keys.Enter)
                {
                    nextTab = ((Control)sender);
                    if (ActiveControl.Name == "dtgridGenLegCode" && dtgridGenLegCode.RowCount > 0)
                    {
                        rowNo = dtgridGenLegCode.CurrentCell.RowIndex;
                        colNo = dtgridGenLegCode.CurrentCell.ColumnIndex;
                        dtgridGenLegCode.CurrentCell = dtgridGenLegCode[colNo, rowNo];
                    }
                    else if (ActiveControl.Name == "txtsearch" && dtgridPettyCash.Rows.Count == 0 && e.KeyChar == (char)Keys.Enter)
                    {
                        txtsearch.Focus();
                    }
                    else
                    {
                        if (ActiveControl.Name == "txtsearch" && dtgridPettyCash.Rows.Count == 0)
                        {
                            nextTab = BtnAdd;
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

        private void FrmPettyCashBook_KeyDown(object sender, KeyEventArgs e)
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

        private void dtgridPettyCash_KeyDown_1(object sender, KeyEventArgs e)
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
                        i = dtgridPettyCash.SelectedCells[0].RowIndex + 1;
                    else if (e.KeyCode == Keys.Up)
                        i = dtgridPettyCash.SelectedCells[0].RowIndex - 1;

                    if (((e.KeyCode == Keys.Down) && (i < dtgridPettyCash.RowCount)) || ((e.KeyCode == Keys.Up) && (i >= 0)))
                    {
                        dtgridPettyCash.Refresh();
                        dtgridPettyCash.CurrentCell = dtgridPettyCash.Rows[i].Cells[0];
                        dtgridPettyCash.Rows[i].Selected = true;
                        if (dtgridPettyCash.Rows.Count > 0)
                        {
                            txtsearch.Text = dtgridPettyCash.Rows[i].Cells[0].Value.ToString();
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

        private void dtgridPettyCash_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            Fillgriddetails();
        }

        private void tabPettyCashbook_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SqlDataReader dr, dr1;
                string str, Pdate, stmess;
                int i = 0, j = 1, sup = 0, k;
                DataTable dt = new DataTable();
                DataRow drw;

                if (tabPettyCashbook.SelectedTab == tabDetail)
                {
                    if (flag == 1)
                    {
                        if (dtgridPettyCash.RowCount > 0)
                        {
                            k = dtgridPettyCash.SelectedCells[0].RowIndex;
                            if (dtgridPettyCash.Rows[k].Cells[1].Value.ToString() != "0.00" && grdchange != 1)
                            {
                                i = dtgridPettyCash.SelectedCells[0].RowIndex;
                               // dtgridPettyCash.Text = dtgridPettyCash.Rows[i].Cells[0].Value.ToString();
                                string ID = mod.Isnull(dtgridPettyCash.Rows[i].Cells[0].Value.ToString(), "");
                                SqlDataReader dareader = mod.GetSelectAllField(tableName, firstCol, ID, "SrNo");
                                FillDetails(dareader, "null");
                                BtnDelete.Text = "DELETE";
                                BtnSave.Text = "UPDATE";
                            }
                            else
                            {
                                MessageBox.Show("No record to display");
                                this.tabPettyCashbook.SelectedTab = tabList;                               
                            }
                        }
                    }
                }
                else
                {
                    FillGrid("tbTransPettyCash");
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
            if (BtnDelete.Text == "DELETE")
            {
                DialogResult mydialog;
                mydialog = MessageBox.Show("Are you Sure you want to delete this record", "", MessageBoxButtons.YesNo);
                if (mydialog == DialogResult.Yes)
                {
                    cmd = new SqlCommand("SP_FrmTransPettyCashBook", con.connect());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@variable", "DELETE");
                    cmd.Parameters.AddWithValue("@code", txtTransNo.Text);
                    cmd.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                    cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                    cmd.ExecuteNonQuery();
                    txtsearch.Focus();
                    tabPettyCashbook.SelectedTab = tabList;
                    //FillGrid("tbTransPettyCash");
                    //BtnAdd.Visible = true;
                }
            }
            else if (BtnDelete.Text == "RESET")
            {
                add();
            }
        }

        private void tabPettyCashbook_Selecting(object sender, TabControlCancelEventArgs e)
        {
             try
            {
                if (e.TabPage.Name == "tabDetail" && grdchange != 2 && flag != 0)
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

        private void FrmPettyCashBook_Shown(object sender, EventArgs e)
        {
            txtsearch.Focus();
        }

    }
}

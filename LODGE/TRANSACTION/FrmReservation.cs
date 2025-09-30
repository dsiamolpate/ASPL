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

namespace ASPL.LODGE.TRANSCTION
{
    public partial class FrmReservation : Form
    {
        Design ObjDesign = new Design();
        public FrmReservation()
        {
            InitializeComponent();
        }
        AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();
        AutoCompleteStringCollection CitynamesCollection = new AutoCompleteStringCollection();

        string voch_dt, fillCDO = "";
        int detail_id = 0, grdchange, escGrid, m, mnth, flag = 0;
        string firstCol = "Reserve_No";
        string tableName = "tbReservation";
        public static double slabPerVal = 0;
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

        private void FrmReservation_Load(object sender, EventArgs e)
        {
            ObjDesign.FormDesign(this, dtgridReservation);
            flag = 1;
            txtsearch.Focus();
            tabReservation.SelectedTab = tabList;
            FillGrid("tbReservation");
            txtReservNo.Enabled = false;
            mod.UserAccessibillityMaster("Reservation", BtnAdd, BtnSave, BtnDelete);
            Globalvariable.SearchChangeVariable = "SrchByName";
        }

        void Fillgriddetails()
        {

            try
            {
                if (dtgridReservation.Rows.Count > 0)
                {
                    int mntotal, dayTot;
                    string strsql;
                    double totlamt;
                    DateTime dtm;
                    DataTable dt = new DataTable();
                    DataRow drw;
                    if (grdchange == 0)
                    {
                        int i = dtgridReservation.SelectedCells[0].RowIndex;
                        string cd = dtgridReservation.Rows[i].Cells[1].Value.ToString();
                        var countOfRooms = 0;
                        Int32.TryParse(dtgridReservation.Rows[i].Cells[1].Value.ToString(), out countOfRooms);
                        if (countOfRooms>0)
                        {
                            if (escGrid == 0 || escGrid == 1)
                            {
                                mnth = mod.Getmonthno(dtgridReservation.Rows[i].Cells[0].Value.ToString());
                            }
                            dtgridReservation.Columns.Clear();
                            dt.Columns.Add("From Date", typeof(string));
                            dt.Columns.Add("Company ID", typeof(string));
                            dt.Columns.Add("Company Name", typeof(string));
                            dt.Columns.Add("From_Date", typeof(string));
                            mntotal = 0;
                            //SELECT DISTINCT(FromDate) FROM tbReservation WHERE MONTH(FromDate)='' AND Company_SrNo='' AND Branchcode=''
                            cmd = new SqlCommand("SP_FrmTransReservation", con.connect());
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@variable", "SELECT");
                            cmd.Parameters.AddWithValue("@checkstring", "SELECT_DATE");
                            cmd.Parameters.AddWithValue("@gridchangeValue", "0");
                            cmd.Parameters.AddWithValue("@month", mnth);
                            cmd.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                            cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                            SqlDataReader dr = cmd.ExecuteReader();
                            while(dr.Read())
                            {
                                drw = dt.NewRow();

                                string date_String = dr["FromDate"].ToString();
                                DateTime dq = Convert.ToDateTime(date_String);
                                drw["From Date"] = dq.ToLongDateString();
                                drw["Company ID"] = dr["Company_ID"].ToString();
                                drw["Company Name"] = dr["Company_Name"].ToString();
                                drw["From_Date"] = dq.ToShortDateString();
                                dt.Rows.Add(drw);
                            }
                            //while (dr.Read())
                            //{
                            //    drw = dt.NewRow();
                            //    string date_String = dr["FromDate"].ToString();
                            //    DateTime dq = Convert.ToDateTime(date_String);
                            //    drw["From Date"] = dq.ToShortDateString();
                            //    dt.Rows.Add(drw);
                            //    dayTot = 0;
                            //    DateTime pdate = Convert.ToDateTime(dr[0].ToString());
                            //    //--SELECT Customer_ID FROM tbReservation WHERE MONTH(FromDate)='' AND Company_SrNo='' AND Branchcode='' ORDER BY FromDate
                            //    cmd = new SqlCommand("SP_FrmTransReservation", con.connect());
                            //    cmd.CommandType = CommandType.StoredProcedure;
                            //    cmd.Parameters.AddWithValue("@variable", "SELECT");
                            //    cmd.Parameters.AddWithValue("@gridchangeValue", "0");
                            //    cmd.Parameters.AddWithValue("@checkstring", "SELECT_CUSTID");
                            //    cmd.Parameters.AddWithValue("@month", mnth);
                            //    cmd.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                            //    cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                            //    SqlDataReader dr1 = cmd.ExecuteReader();
                            //    while (dr1.Read())
                            //    {
                            //        //  drw = dt.NewRow();
                            //        drw["Reser.No."] = dr1["Reserve_No"].ToString();
                            //        // dt.Rows.Add(drw);
                            //        //SELECT supplier_name,supplier_id FROM tbSupplierMaster WHERE supplier_id='' AND Branchcode=''
                            //        cmd = new SqlCommand("SP_FrmTransReservation", con.connect());
                            //        cmd.CommandType = CommandType.StoredProcedure;
                            //        cmd.Parameters.AddWithValue("@variable", "SELECT");
                            //        cmd.Parameters.AddWithValue("@gridchangeValue", "0");
                            //        cmd.Parameters.AddWithValue("@checkstring", "SELECT_SUPPLIER");
                            //        cmd.Parameters.AddWithValue("@CustCode", dr1[1].ToString());
                            //        cmd.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                            //        cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                            //        SqlDataReader dr2 = cmd.ExecuteReader();
                            //        while (dr2.Read())
                            //        {
                            //            drw["Customer ID"] = dr2["supplier_id"].ToString();
                            //            drw["Customer Name"] = dr2["supplier_name"].ToString();

                            //        }
                            //    }
                            //}
                            grdchange = 1;
                            escGrid = 0;
                            dr.Close();
                            dtgridReservation.DataSource = dt;
                            dtgridReservation.Columns[1].Visible = false;
                            dtgridReservation.Columns[3].Visible = false;
                            dtgridReservation.Columns[0].Width = 320;
                            //dtgridReservation.Columns[1].Width = 120;
                            dtgridReservation.Columns[2].Width = 320;
                            //dtgridReservation.Columns[3].Width = 320;
                            //if (dtgridReservation.Rows.Count > 0)
                            //{
                            //    txtsearch.Text = dtgridReservation.Rows[0].Cells[2].Value.ToString();
                            //}
                            BtnAdd.Visible = false;
                        }
                    }
                    else if (grdchange == 1)
                    {
                        int i = dtgridReservation.SelectedCells[1].RowIndex;
                        string FromDate = dtgridReservation.Rows[i].Cells[3].Value.ToString();
                        string CompanyID = dtgridReservation.Rows[i].Cells[1].Value.ToString();
                        dtgridReservation.Columns.Clear();
                        dt.Columns.Add("Guest Name", typeof(string));
                        dt.Columns.Add("Reservation No.", typeof(string));
                        dt.Columns.Add("Guest_ID", typeof(string));
                        //select Guest_ID from tbReservation WHERE Reserve_No='' AND Customer_ID='' AND Company_SrNo='' AND Branchcode ='' ORDER BY Reserve_No
                        cmd = new SqlCommand("SP_FrmTransReservation", con.connect());
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@variable", "SELECT");
                        cmd.Parameters.AddWithValue("@checkstring", "SELECT_GUSTID");
                        cmd.Parameters.AddWithValue("@gridchangeValue", "1");
                        cmd.Parameters.AddWithValue("@FromDate", FromDate);
                        cmd.Parameters.AddWithValue("@CustCode", CompanyID);
                        cmd.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                        cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            drw = dt.NewRow();
                            drw["Reservation No."] = dr["Reserve_No"].ToString();
                            drw["Guest_ID"] = dr["Guest_ID"].ToString();
                            drw["Guest Name"] = dr["Guest_Name"].ToString();
                            dt.Rows.Add(drw);
                            //cmd = new SqlCommand("SP_FrmTransReservation", con.connect());
                            //cmd.CommandType = CommandType.StoredProcedure;
                            //cmd.Parameters.AddWithValue("@variable", "SELECT");
                            //cmd.Parameters.AddWithValue("@gridchangeValue", "1");
                            //cmd.Parameters.AddWithValue("@checkstring", "SELECT_GUESTNAME");
                            //cmd.Parameters.AddWithValue("@guetID", dr[1].ToString());
                            //cmd.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                            //cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                            //SqlDataReader dr2 = cmd.ExecuteReader();
                            //while (dr2.Read())
                            //{
                            //    drw["Guest Name"] = dr2["Customer_name"].ToString();
                            //}
                        }
                        grdchange = 2;
                        escGrid = 1;
                        dr.Close();
                        dtgridReservation.DataSource = dt;
                        dtgridReservation.Columns[0].Width = 440;
                        dtgridReservation.Columns[1].Width = 248;
                        dtgridReservation.Columns[2].Visible = false;
                        //  dtgridReservation.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                        //if (dtgridReservation.Rows.Count > 0)
                        //{
                        //    txtsearch.Text = dtgridReservation.Rows[0].Cells[0].Value.ToString();
                        //}
                        BtnAdd.Visible = false;
                    }
                    else if (grdchange == 2)
                    {
                        flag = 1;
                        tabReservation.SelectedTab = tabDetail;
                        ChkCancelReservation.Visible = true;
                        BtnSave.Enabled = false;
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

        public void FillGrid(string tableNM)
        {
            try
            {
                string strsql;
                DataTable dt = new DataTable();
                DataRow drw;
                int cd = 1;
                double samt;
                dtgridReservation.Columns.Clear();
                // m = Globalvariable.StartDate;
                DateTime expDateconv = Convert.ToDateTime(Globalvariable.EndDate);
                DateTime startdate = Convert.ToDateTime(Globalvariable.StartDate);
                string d = Globalvariable.EndDate;
                m = startdate.Month;
                // TimeSpan span = expDateconv.Subtract(startdate);
                int months = expDateconv.Subtract(startdate).Days / 30;
                dt.Columns.Add("Month", typeof(string));
                dt.Columns.Add("Total Rooms", typeof(string));
                while (cd <= months)
                {
                    drw = dt.NewRow();
                    if (m > 12)
                        m = 1;
                    drw["Month"] = mod.Getmonth(m);
                    //SELECT SUM(No_Of_Rooms) FROM tbReservation WHERE MONTH(FromDate)='4' AND Company_SrNo='' AND Branchcode=''
                    cmd = new SqlCommand("SP_FrmTransReservation", con.connect());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@variable", "SELECT");
                    cmd.Parameters.AddWithValue("@tablename", "tbReservation");
                    cmd.Parameters.AddWithValue("@checkstring", "SUM");
                    cmd.Parameters.AddWithValue("@month", m);
                    cmd.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                    cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                    samt = Convert.ToDouble(mod.Isnull(cmd.ExecuteScalar().ToString(), "0"));
                    drw["Total Rooms"] = samt.ToString();
                    dt.Rows.Add(drw);
                    m = m + 1;
                    cd = cd + 1;
                }
                dtgridReservation.DataSource = dt;
                dtgridReservation.Columns[0].Width = 474;
                dtgridReservation.Columns[1].Width = 210;
                dtgridReservation.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                grdchange = 0;
                escGrid = 0;
                if (dtgridReservation.Rows.Count > 0)
                {
                    txtsearch.Text = dtgridReservation.Rows[0].Cells[0].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        #region Set TextBox Back Color
        private void txtCustId_Enter(object sender, EventArgs e)
        {
            txtCustId.BackColor = Color.Yellow;
        }

        private void txtCustId_Leave(object sender, EventArgs e)
        {
            txtCustId.BackColor = Color.White;
        }

        private void txtBookCode_Enter(object sender, EventArgs e)
        {
            txtGuestID.BackColor = Color.Yellow;
        }

        private void txtBookCode_Leave(object sender, EventArgs e)
        {
            txtGuestID.BackColor = Color.White;
        }

        private void txtRoomNo_Enter(object sender, EventArgs e)
        {
            txtRoomCategry.BackColor = Color.Yellow;
        }

        private void txtRoomNo_Leave(object sender, EventArgs e)
        {
            txtRoomCategry.BackColor = Color.White;
        }

        private void txtNoOfAdult_Enter(object sender, EventArgs e)
        {
            txtNoOfAdult.BackColor = Color.Yellow;
        }

        private void txtNoOfAdult_Leave(object sender, EventArgs e)
        {
            txtNoOfAdult.BackColor = Color.White;
        }

        private void txtChild_Enter(object sender, EventArgs e)
        {
            txtChild.BackColor = Color.Yellow;
        }

        private void txtChild_Leave(object sender, EventArgs e)
        {
            txtChild.BackColor = Color.White;
        }

        private void txtReserveRoom_Enter(object sender, EventArgs e)
        {
            txtReserveRoom.BackColor = Color.Yellow;
        }

        private void txtReserveRoom_Leave(object sender, EventArgs e)
        {
            txtReserveRoom.BackColor = Color.White;
        }

        private void cmbPurpose_Enter(object sender, EventArgs e)
        {
            cmbPurpose.BackColor = Color.Yellow;
        }

        private void cmbPurpose_Leave(object sender, EventArgs e)
        {
            cmbPurpose.BackColor = Color.White;
        }

        private void cmbApplyTax_Enter(object sender, EventArgs e)
        {
            cmbApplyTax.BackColor = Color.Yellow;
        }

        private void cmbApplyTax_Leave(object sender, EventArgs e)
        {
            cmbApplyTax.BackColor = Color.White;
        }

        private void txtApplyTax_Enter(object sender, EventArgs e)
        {
            txtApplyTax.BackColor = Color.Yellow;
        }

        private void txtApplyTax_Leave(object sender, EventArgs e)
        {
            txtApplyTax.BackColor = Color.White;
        }

        private void txtAdvance1_Enter(object sender, EventArgs e)
        {
            txtAdvance1.BackColor = Color.Yellow;
        }

        private void txtAdvance1_Leave(object sender, EventArgs e)
        {
            txtAdvance1.BackColor = Color.White;
        }

        private void txtBookedBy_Enter(object sender, EventArgs e)
        {
            txtBookedBy.BackColor = Color.Yellow;
        }

        private void txtBookedBy_Leave(object sender, EventArgs e)
        {
            txtBookedBy.BackColor = Color.White;
        }

        private void txtRemarks_Enter(object sender, EventArgs e)
        {
            txtRemarks.BackColor = Color.Yellow;
        }

        private void txtRemarks_Leave(object sender, EventArgs e)
        {
            txtRemarks.BackColor = Color.White;
        }

        private void txtPlan_Enter(object sender, EventArgs e)
        {
            txtPlan.BackColor = Color.Yellow;
        }

        private void txtPlan_Leave(object sender, EventArgs e)
        {
            txtPlan.BackColor = Color.White;
        }

        private void txtReason_Enter(object sender, EventArgs e)
        {
            txtReason.BackColor = Color.Yellow;
        }

        private void txtReason_Leave(object sender, EventArgs e)
        {
            txtReason.BackColor = Color.White;
        }

        private void txtsearch_Enter(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.Yellow;
        }

        private void txtsearch_Leave(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.White;
        }

        private void txtState_Enter(object sender, EventArgs e)
        {
            txtState.BackColor = Color.Yellow;
        }

        private void txtState_Leave(object sender, EventArgs e)
        {
            txtState.BackColor = Color.White;
        }

        private void txtPlace_Enter(object sender, EventArgs e)
        {
            txtPlace.BackColor = Color.Yellow;
        }

        private void txtPlace_Leave(object sender, EventArgs e)
        {
            txtPlace.BackColor = Color.White;
        }
        #endregion

        public void FillApplyTax()
        {
            cmbApplyTax.Items.Clear();
            cmbApplyTax.Items.Insert(0, "Including");
            cmbApplyTax.Items.Insert(1, "Excluding");
            // cmbPayable.SelectedIndex = 0;
            if (detail_id != 1)
            {
                cmbApplyTax.SelectedIndex = 0;
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            flag = 0;
            grdchange = 0;
            tabReservation.SelectedTab = tabDetail;
            mod.addBtnClick(this, txtReservNo, tableName, firstCol, BtnSave, BtnDelete, txtGuestID);
            mod.txtclear(tabReservation.TabPages[1].Controls);
            mod.UserAccessibillityMaster("Reservation", BtnAdd, BtnSave, BtnDelete);
            txtReservNo.Text = "" + mod.GetMaxNum(tableName, firstCol);
            BtnPrint.Enabled = false;
            dtpFromDate.Focus();
            BtnDelete.Text = "RESET";
            FillApplyTax();
            dtpInTime.Value = DateTime.Now;
            txtNoOfAdult.Text = "1";
            txtChild.Text = "0";
            txtReserveRoom.Text = "1";
            ChkCancelReservation.Visible = false;
            txtRoomtariff1.Enabled = false;
            txtRoomTariffTax.Enabled = false;
            txtTotalRoomtariff.Enabled = false;
        }

        private void tabReservation_Selecting(object sender, TabControlCancelEventArgs e)
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

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                tabReservation.SelectedTab = tabList;
                grdchange = 0;
                BtnAdd.Visible = true;
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

        private void FrmReservation_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Control nextTab;
                if (e.KeyChar == (char)Keys.Enter)
                {
                    nextTab = ((Control)sender);
                    if (ActiveControl.Name == "txtsearch" && dtgridReservation.Rows.Count == 0 && e.KeyChar == (char)Keys.Enter)
                    {
                        txtsearch.Focus();
                    }
                    else
                    {
                        if (ActiveControl.Name == "txtsearch" && dtgridReservation.Rows.Count == 0)
                        {
                            nextTab = BtnAdd;
                        }
                        else if (ActiveControl.Name == "txtCustId")
                        {
                            if (txtCustName.Text == "")
                            {
                                txtCustId.Focus();
                            }
                            else
                            {
                                nextTab = GetNextControl(ActiveControl, true);
                            }
                            nextTab.Focus();
                        }
                        //else if (ActiveControl.Name == "txtGuestID")
                        //{
                        //    if (txtGuestname.Text == "")
                        //    {
                        //        txtGuestID.Focus();
                        //    }
                        //    else
                        //    {
                        //        nextTab = GetNextControl(ActiveControl, true);
                        //    }
                        //    nextTab.Focus();
                        //}
                        else if (ActiveControl.Name == "txtRoomCategry")
                        {
                            if (txtRoomName.Text == "")
                            {
                                txtRoomCategry.Focus();
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

        private void dtpFromDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                dtpToDate.Value = DateTime.Now.AddDays(1);
            }
        }

        private void txtCustId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void txtCustId_KeyDown(object sender, KeyEventArgs e)
        {
            Globalvariable.SearchString = "CompanyInfo";
            Globalvariable.Cust_Type = "D";
            var returnForm = Models.Common.FormManagement.Instance.SearchFormOnKeyDown(ref txtCustId, e, "FrmReservation", true);
            if (returnForm == null)
            {
                return;
            }

            txtCustId.Text = returnForm.codeselected;
            txtCustName.Text = returnForm.descselected;

            if (returnForm.codeselected != null && returnForm.codeselected.Trim().Length > 0)
            {
                SelectNextControl(txtCustId, true, true, true, true);
            }

            //try
            //{
            //    Globalvariable.SearchString = "CustName";
            //    if (e.KeyCode == Keys.Enter && txtCustId.Text != "")
            //    {
            //        fillCustID();
            //    }
            //    else if (txtCustId.Text == "" && e.KeyCode == Keys.Enter)
            //    {
            //        KeysConverter kc = new KeysConverter();
            //        string key = kc.ConvertToString(e.KeyCode);
            //        FrmSearch srch = new FrmSearch();
            //        Globalvariable.SearchString = "SupplierMaster";
            //        srchda = mod.FillSearchGrid_FrmName("FrmReservation", "tbSupplierMaster");
            //        srch.val = key;
            //        srch.fillbackgridSrch(srchda, "FrmReservation");
            //        ds.Clear();
            //        srchda.Fill(ds);
            //        if (ds.Tables[0].Rows.Count > 0)
            //            srch.ShowDialog();
            //        txtCustId.Text = srch.codeselected;
            //        txtCustName.Text = srch.descselected;
            //        if (srch.codeselected == null)
            //        {
            //            txtCustId.Focus();
            //        }
            //        //else
            //        //{
            //        //    fillRoomCat();
            //        //}
            //        Globalvariable.searchNo = 0;
            //        if (txtCustName.Text != "")
            //        {
            //            txtGuestID.Focus();
            //        }
            //        else
            //        {
            //            txtCustId.Focus();
            //        }
            //    }
            //    else if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            //    {
            //        txtCustId.Text = "";
            //        txtCustName.Text = "";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    string str = "Message:" + ex.Message;
            //    MessageBox.Show(str, "Error Message");
            //}
        }

        private void fillEmpID()
        {
            try
            {
                if (txtBookedBy.Text.Trim().Equals(""))
                {
                    TxtBookedName.Text = "";
                }
                else
                {
                    SqlDataReader dr = mod.GetRecord("SELECT EmpName,EmpNo FROM tbEmployeeInformation WHERE EmpNo='" + txtBookedBy.Text + "' AND Branchcode='" + Globalvariable.bcode + "' AND Deleted='N'");
                    // SqlDataReader dr = mod.GetRecord("SELECT supplier_name,supplier_id FROM tbSupplierMaster WHERE(active='Y' OR active IS NULL) AND deleted='N' AND Branchcode='" + Globalvariable.bcode + "' AND cust_type='D' AND supplier_id='" + txtCustId.Text + "'");
                    if (dr.HasRows)
                    {
                        dr.Read();
                        txtBookedBy.Text = dr["EmpNo"].ToString();
                        TxtBookedName.Text = dr["EmpName"].ToString();
                    }
                    else
                    {
                        txtBookedBy.Text = "";
                        TxtBookedName.Text = "";
                        txtBookedBy.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void fillCustID()
        {
            try
            {
                if (txtCustId.Text.Trim().Equals(""))
                {
                    txtCustName.Text = "";
                }
                else
                {
                    SqlDataReader dr = mod.GetRecord("SELECT supplier_name,supplier_id FROM tbSupplierMaster WHERE(active='Y' OR active IS NULL) AND deleted='N' AND Branchcode='" + Globalvariable.bcode + "' AND cust_type='D' AND supplier_id='" + txtCustId.Text + "'");
                    if (dr.HasRows)
                    {
                        dr.Read();
                        txtCustId.Text = dr["supplier_id"].ToString();
                        txtCustName.Text = dr["supplier_name"].ToString();
                        //   txtSingleRate.Focus();
                    }
                    else
                    {
                        txtCustId.Text = "";
                        txtCustName.Text = "";
                        txtCustId.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void fillGuestID()
        {

            txtAddress.Text = "";
            txtContactNo.Text = "";
            txtState.Text = "";
            txtPlace.Text = "";
            try
            {
                if (txtGuestID.Text.Trim().Equals(""))
                {
                    txtGuestname.Text = "";
                }
                else
                {
                    SqlDataReader dr = mod.GetRecord("SELECT Customer_name,Customer_id  FROM tbGuestInformation WHERE deleted='N' AND Branchcode='" + Globalvariable.bcode + "' AND Customer_id='" + txtGuestID.Text + "'");
                    if (dr.HasRows)
                    {
                        dr.Read();
                        txtGuestID.Text = dr["Customer_id"].ToString();
                        txtGuestname.Text = dr["Customer_name"].ToString();
                        //   txtSingleRate.Focus();
                        DataRow drGuest = Models.Common.DbConnectivity.Instance.GetCustomerInformation(txtGuestID.Text);
                        try
                        {
                            txtAddress.Text = drGuest["Address"].ToString();
                            txtContactNo.Text = drGuest["Contact"].ToString();
                            txtState.Text = drGuest["State"].ToString();
                            txtPlace.Text = drGuest["Place"].ToString();
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    else
                    {
                        txtCustId.Text = "";
                        txtCustName.Text = "";
                        txtCustId.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }
        public void autoAddState()
        {

            SqlDataReader dReader;

            SqlCommand cmd = new SqlCommand("Select distinct StateName from tbStateMaster order by StateName asc", con.connect());

            dReader = cmd.ExecuteReader();
            if (dReader.HasRows == true)
            {
                while (dReader.Read())
                    namesCollection.Add(dReader["StateName"].ToString());
            }
            else
            {
                MessageBox.Show("Data not found");
            }
            txtState.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtState.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtState.AutoCompleteCustomSource = namesCollection;
        }

        public void autoAddCity()
        {

            SqlDataReader dReader;

            SqlCommand cmd = new SqlCommand("Select distinct CityName from tbCityMaster order by CityName asc", con.connect());

            dReader = cmd.ExecuteReader();
            if (dReader.HasRows == true)
            {
                while (dReader.Read())
                    CitynamesCollection.Add(dReader["CityName"].ToString());
            }
            else
            {
                MessageBox.Show("Data not found");
            }
            dReader.Close();

            txtPlace.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtPlace.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtPlace.AutoCompleteCustomSource = CitynamesCollection;
        }

        private void txtGuestID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void txtRoomNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void txtRoomCategry_KeyDown(object sender, KeyEventArgs e)
        {
            Globalvariable.SearchString = "RoomType";
            var returnForm = Models.Common.FormManagement.Instance.SearchFormOnKeyDown(ref txtRoomCategry, e, "FrmReservation", true);
            if (returnForm == null)
            {
                return;
            }

            txtRoomCategry.Text = returnForm.codeselected;
            txtRoomName.Text = returnForm.descselected;

            if (returnForm.codeselected != null && returnForm.codeselected.Trim().Length > 0)
            {
                SelectNextControl(txtRoomCategry, true, true, true, true);
            }
            //try
            //{
            //    Globalvariable.SearchString = "RoomCatgry";
            //    if (e.KeyCode == Keys.Enter && txtRoomCategry.Text != "")
            //    {
            //        fillRoomCat();
            //    }
            //    else if (txtRoomCategry.Text == "" && e.KeyCode == Keys.Enter)
            //    {
            //        Globalvariable.searchNo = 1;
            //        KeysConverter kc = new KeysConverter();
            //        string key = kc.ConvertToString(e.KeyCode);
            //        FrmSearch srch = new FrmSearch();
            //        // str = "SELECT catgry_name,catgry_code FROM RoomCategoryMaster WHERE deleted='N'AND Branchcode='" + Globalvariable.bcode + "'";
            //        srchda = mod.GetselectQuery("RoomCategoryMaster", "catgry_code", "catgry_name", "N", "N");
            //        srch.val = key;
            //        srch.fillbackgridSrch(srchda, "FrmReservation");
            //        ds.Clear();
            //        srchda.Fill(ds);
            //        if (ds.Tables[0].Rows.Count > 0)
            //            srch.ShowDialog();
            //        txtRoomCategry.Text = srch.codeselected;
            //        if (srch.codeselected == null)
            //        {
            //            txtRoomCategry.Focus();
            //        }
            //        else
            //        {
            //            fillRoomCat();
            //        }
            //        Globalvariable.searchNo = 0;
            //        if (txtRoomName.Text != "")
            //        {
            //            txtNoOfAdult.Focus();
            //        }
            //        else
            //        {
            //            txtRoomCategry.Focus();
            //        }
            //    }
            //    else if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            //    {
            //        txtRoomCategry.Text = "";
            //        txtRoomName.Text = "";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    string str = "Message:" + ex.Message;
            //    MessageBox.Show(str, "Error Message");
            //}
        }

        private void fillRoomCat()
        {
            try
            {
                if (txtRoomCategry.Text.Trim().Equals(""))
                {
                    txtRoomName.Text = "";
                }
                else
                {
                    SqlDataReader dr = mod.GetRecord("SELECT catgry_name FROM RoomCategoryMaster WHERE deleted='N' AND Branchcode='" + Globalvariable.bcode +
                        "' AND catgry_code='" + txtRoomCategry.Text + "'");
                    if (dr.HasRows)
                    {
                        dr.Read();
                        txtRoomName.Text = dr["catgry_name"].ToString();
                        //   txtSingleRate.Focus();
                    }
                    else
                    {
                        txtRoomCategry.Text = "";
                        txtRoomName.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void FrmReservation_KeyDown(object sender, KeyEventArgs e)
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

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (BtnDelete.Text == "DELETE")
            {
                DialogResult mydialog;
                mydialog = MessageBox.Show("Are you Sure you want to delete this record", "", MessageBoxButtons.YesNo);
                if (mydialog == DialogResult.Yes)
                {
                    cmd = new SqlCommand("SP_FrmTransReservation", con.connect());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@variable", "DELETE");
                    cmd.Parameters.AddWithValue("@code", txtReserveRoom.Text);
                    cmd.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                    cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                    cmd.ExecuteNonQuery();
                    txtsearch.Focus();
                    tabReservation.SelectedTab = tabList;
                    FillGrid("tbReservation");
                    BtnAdd.Visible = true;
                }
            }
            else if (BtnDelete.Text == "RESET")
            {
                BtnAdd_Click(sender, e);
            }
        }

        private void btnAddCustName_Click(object sender, EventArgs e)
        {
            Globalvariable.PartyLedgertype = "4";
            FrmSupplierMaster frm = new FrmSupplierMaster();
            frm.ShowDialog();
            txtCustId.Text = Globalvariable.SupplierID;
            txtCustName.Text = Globalvariable.SupplierName;
        }

        private void btnAddGuestName_Click(object sender, EventArgs e)
        {
            FrmGuestInformation frm = new FrmGuestInformation();
            frm.ShowDialog();
            txtGuestID.Text = Globalvariable.GuestID;
            txtGuestname.Text = Globalvariable.GuestName;
        }

        private void txtBookedBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void txtBookedBy_KeyDown(object sender, KeyEventArgs e)
        {
            Globalvariable.SearchString = "EmployeeInfo";
            var returnForm = Models.Common.FormManagement.Instance.SearchFormOnKeyDown(ref txtBookedBy, e, "FrmReservation", true);
            if (returnForm == null)
            {
                return;
            }

            txtBookedBy.Text = returnForm.codeselected;
            TxtBookedName.Text = returnForm.descselected;


            //try
            //{
            //    Globalvariable.SearchString = "EmpName";
            //    if (e.KeyCode == Keys.Enter && txtBookedBy.Text != "")
            //    {
            //        fillEmpID();
            //    }
            //    else if (txtBookedBy.Text == "" && e.KeyCode == Keys.Enter)
            //    {
            //        KeysConverter kc = new KeysConverter();
            //        string key = kc.ConvertToString(e.KeyCode);
            //        FrmSearch srch = new FrmSearch();
            //       //SELECT EmpName,EmpNo FROM tbEmployeeInformation WHERE Branchcode='' AND Deleted='N' 
            //        srchda = mod.FillSearchGrid_FrmName("FrmReservation", "tbEmployeeInformation");
            //        srch.val = key;
            //        srch.fillbackgridSrch(srchda, "FrmReservation");
            //        ds.Clear();
            //        srchda.Fill(ds);
            //        if (ds.Tables[0].Rows.Count > 0)
            //            srch.ShowDialog();
            //        txtBookedBy.Text = srch.codeselected;
            //        TxtBookedName.Text = srch.descselected;
            //        if (srch.codeselected == null)
            //        {
            //            txtBookedBy.Focus();
            //        }
            //        if (TxtBookedName.Text != "")
            //        {
            //            txtRemarks.Focus();
            //        }
            //        else
            //        {
            //            txtBookedBy.Focus();
            //        }
            //    }
            //    else if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            //    {
            //        txtBookedBy.Text = "";
            //        TxtBookedName.Text = "";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    string str = "Message:" + ex.Message;
            //    MessageBox.Show(str, "Error Message");
            //}
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string cancelReservation = "N", WithTaxYN = "", AdvanceRecpt = "", VouchNo = "";
            string CancelDate, CancelTime;
            // Check Receipt


            if (txtRoomtariff1.Text == "0" || txtRoomtariff1.Text == "")
            {
                txtApplyTax.Focus();
            }
            //else if(txtGuestname.Text=="")
            //{
            //    txtGuestID.Focus();
            //}
            else if (txtCustName.Text == "")
            {
                txtCustId.Focus();
            }
            //else if (txtAddress.Text == "")
            //{
            //    txtAddress.Focus();
            //}
            //else if (txtContactNo.Text == "")
            //{
            //    txtContactNo.Focus();
            //}
            else if (txtBookedBy.Text == "")
            {
                txtBookedBy.Focus();
            }
            else
            {
                if (ChkCancelReservation.Checked == true)
                {
                    CancelDate = DateTime.Now.ToShortDateString();
                    CancelTime = DateTime.Now.ToShortTimeString();
                }
                else if (ChkCancelReservation.Checked == false)
                {
                    CancelDate = "NULL";
                    CancelTime = "NULL";
                }
                if (txtAdvance1.Text != "")
                {
                    AdvanceRecpt = mod.Isnull(txtAdvance1.Text, "0");
                    VouchNo = mod.Isnull(txtAdvance2.Text, "0");
                }
                if (cmbApplyTax.SelectedItem == "Excluding")
                {
                    WithTaxYN = "N";
                }
                else if (cmbApplyTax.SelectedItem == "Including")
                {
                    WithTaxYN = "Y";
                }

                if (BtnSave.Text == "SAVE")
                {
                    cmd = new SqlCommand("SP_FrmTransReservation", con.connect());

                    cmd.CommandType = CommandType.StoredProcedure;

                    if (BtnSave.Text == "SAVE")
                    {
                        txtReservNo.Text = "" + mod.GetMaxNum(tableName, firstCol);
                        cmd.Parameters.AddWithValue("@variable", "INSERT");
                        cmd.Parameters.AddWithValue("@code", txtReservNo.Text);
                        cmd.Parameters.AddWithValue("@preIndate", dtpFromDate.Value.ToShortDateString());
                        cmd.Parameters.AddWithValue("@preOutDate", dtpToDate.Value.ToShortDateString());
                        string currentDate = DateTime.Now.ToShortDateString();
                        cmd.Parameters.AddWithValue("@reserveDate", currentDate);
                    }
                }
                else if (BtnSave.Text == "UPDATE")
                {
                    cmd = new SqlCommand("SP_FrmTransReservation", con.connect());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@variable", "UPDATE");
                    cmd.Parameters.AddWithValue("@code", txtReservNo.Text);

                    SqlCommand command = new SqlCommand("SELECT FromDate,ToDate FROM tbReservation WHERE Reserve_No='" + txtReservNo.Text + "' AND Company_SrNo='" + Globalvariable.company_Srno + "' AND Branchcode='" + Globalvariable.bcode + "'", con.connect());
                    SqlDataReader DR = command.ExecuteReader();
                    while (DR.Read())
                    {
                        if ((DR["FromDate"].ToString() != dtpFromDate.Value.ToShortDateString()) || (DR["ToDate"].ToString() != dtpToDate.Value.ToShortDateString()))
                        {
                            cmd.Parameters.AddWithValue("@UpdateDate_Variable", "UPDATE_DATE");
                            cmd.Parameters.AddWithValue("@preIndate", dtpFromDate.Value.ToShortDateString());
                            cmd.Parameters.AddWithValue("@preOutDate", dtpToDate.Value.ToShortDateString());
                            cmd.Parameters.AddWithValue("@UpdateDate", DateTime.Now.ToShortDateString());
                        }
                    }

                    if (ChkCancelReservation.Checked == true)
                    {
                        cancelReservation = "Y";
                        //  cmd.Parameters.AddWithValue("@cancelReservation", cancelReservation);
                        cmd.Parameters.AddWithValue("@CancelDate", DateTime.Now.ToShortDateString());
                        cmd.Parameters.AddWithValue("@CancelTime", DateTime.Now.ToShortTimeString());
                    }
                    else if (ChkCancelReservation.Checked == false)
                    {
                        cancelReservation = "N";
                        // cmd.Parameters.AddWithValue("@cancelReservation", cancelReservation);
                    }
                }
                cmd.Parameters.AddWithValue("@FromDate", dtpFromDate.Value.ToShortDateString());
                cmd.Parameters.AddWithValue("@InTime", dtpInTime.Value.ToShortTimeString());
                cmd.Parameters.AddWithValue("@ToDate", dtpToDate.Value.ToShortDateString());
                cmd.Parameters.AddWithValue("@CustCode", mod.Isnull(txtCustId.Text, "0"));
                cmd.Parameters.AddWithValue("@guetID", mod.Isnull(txtGuestID.Text, "0"));
                cmd.Parameters.AddWithValue("@noOfAdult", mod.Isnull(txtNoOfAdult.Text, "0"));
                cmd.Parameters.AddWithValue("@RoomType", mod.Isnull(txtRoomCategry.Text, "0"));
                cmd.Parameters.AddWithValue("@child", mod.Isnull(txtChild.Text, "0"));
                cmd.Parameters.AddWithValue("@NumberOfRooms", mod.Isnull(txtReserveRoom.Text, "0"));
                cmd.Parameters.AddWithValue("@RoomTariff", mod.Isnull(txtRoomtariff1.Text, "0"));
                cmd.Parameters.AddWithValue("@TotalRoomTariff", mod.Isnull(txtTotalRoomtariff.Text, "0"));
                cmd.Parameters.AddWithValue("@BookedBy", mod.Isnull(txtBookedBy.Text, "0"));
                cmd.Parameters.AddWithValue("@cancelReservation", cancelReservation);
                cmd.Parameters.AddWithValue("@remark", mod.Isnull(txtRemarks.Text, "-"));
                cmd.Parameters.AddWithValue("@Plan", mod.Isnull(txtPlan.Text, "-"));
                cmd.Parameters.AddWithValue("@reason", mod.Isnull(txtReason.Text, "-"));
                cmd.Parameters.AddWithValue("@compasrNo", Globalvariable.company_Srno);
                cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                cmd.ExecuteNonQuery();
                mod.txtclear(tabReservation.TabPages[1].Controls);
                txtReservNo.Text = "" + mod.GetMaxNum(tableName, firstCol);
                FillApplyTax();
                dtpInTime.Value = DateTime.Now;
                txtNoOfAdult.Text = "1";
                txtChild.Text = "0";
                txtReserveRoom.Text = "1";
                FillApplyTax();
                dtpFromDate.Focus();
                BtnSave.Enabled = false;
                BtnPrint.Enabled = true;
            }
        }

        public void CalculateRoomTerrif()
        {
            try
            {
                double TerifAmount = 0;
                double TaxAmount = 0;
                double TaxPerTotal = 0;
                string strTaxPerTotal = "";
                SqlCommand cmd;
                cmd = new SqlCommand("SELECT DISTINCT tax_code FROM tbRoomTaxDetail WHERE Branchcode='" + Globalvariable.bcode + "'", con.connect());
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        //cmd = new SqlCommand("SELECT * FROM tbTaxMaster WHERE tax_code='" + mod.Isnull(dr["tax_code"].ToString(), "0") + "' AND Branchcode='" + Globalvariable.bcode + "'", con.connect());
                        //SqlDataReader dr1 = cmd.ExecuteReader();
                        cmd = new SqlCommand("SP_FrmTransReservation", con.connect());
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@variable", "SELECT");
                        cmd.Parameters.AddWithValue("@tablename", "tbTaxMaster");
                        cmd.Parameters.AddWithValue("@code", mod.Isnull(dr["tax_code"].ToString(), "0"));
                        cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                        SqlDataReader dr1 = cmd.ExecuteReader();
                        while (dr1.Read())
                        {
                            string taxcode = mod.Isnull(dr1["tax_code"].ToString(), "0");
                            CheckSlab(taxcode, txtApplyTax.Text);
                            if (slabPerVal > 0)
                            {
                                TaxPerTotal = TaxPerTotal + slabPerVal;
                            }
                        }
                    }
                }

                cmd = new SqlCommand("SP_FrmTransReservation", con.connect());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@variable", "CALCULATION");
                cmd.Parameters.AddWithValue("@RoomTarif_AmOunt", txtApplyTax.Text);
                if (cmbApplyTax.SelectedItem == "Excluding")
                {
                    cmd.Parameters.AddWithValue("@variable2", "Excluding");
                    cmd.Parameters.AddWithValue("@TaxPerTotal", TaxPerTotal);
                }
                else if (cmbApplyTax.SelectedItem == "Including")
                {
                    if (TaxPerTotal < 10)
                    {
                        strTaxPerTotal = TaxPerTotal.ToString("#00.0000000");
                    }
                    else
                    {
                        strTaxPerTotal = Convert.ToString(TaxPerTotal);
                    }
                    strTaxPerTotal = strTaxPerTotal.Replace(".", "");
                    strTaxPerTotal = "1." + strTaxPerTotal;
                    cmd.Parameters.AddWithValue("@variable2", "Including");
                    cmd.Parameters.AddWithValue("@TaxPerTotal", strTaxPerTotal);
                }
                SqlDataReader Cal_dr = cmd.ExecuteReader();
                if (Cal_dr.HasRows)
                {
                    Cal_dr.Read();
                    if (cmbApplyTax.SelectedItem == "Excluding")
                    {
                        txtRoomtariff1.Text = txtApplyTax.Text;
                        txtRoomTariffTax.Text = Cal_dr[0].ToString();
                        txtTotalRoomtariff.Text = Cal_dr[1].ToString();
                    }
                    else if (cmbApplyTax.SelectedItem == "Including")
                    {
                        txtRoomtariff1.Text = Cal_dr[0].ToString();
                        txtRoomTariffTax.Text = Cal_dr[1].ToString();
                        txtTotalRoomtariff.Text = Cal_dr[2].ToString();
                    }
                }

                #region Calculate RoomTarif Inline query
                //if (cmbApplyTax.SelectedItem == "Excluding")
                // {
                //     //strTaxPerTotal = TaxPerTotal;
                //     txtRoomtariff1.Text = txtApplyTax.Text;
                //    double txtRoomTariff = (((Convert.ToDouble(txtRoomtariff1.Text)) * TaxPerTotal) / 100);
                //    double roomteriff_decimal = Math.Round(txtRoomTariff);
                //    txtRoomTariffTax.Text = Convert.ToString(roomteriff_decimal);
                //     double TotalVal=Convert.ToDouble(txtRoomtariff1.Text)+Convert.ToDouble(txtRoomTariffTax.Text);
                //     double totalval_decimal = Math.Round(TotalVal, 2);
                //     txtTotalRoomtariff.Text = Convert.ToString(Math.Round(totalval_decimal));
                // }
                // else if (cmbApplyTax.SelectedItem == "Including")
                // {
                //     if (TaxPerTotal < 10)
                //     {
                //         strTaxPerTotal = TaxPerTotal.ToString("#00.0000000");
                //     }
                //     else
                //     {
                //         strTaxPerTotal = Convert.ToString(TaxPerTotal);
                //     }
                //     strTaxPerTotal = strTaxPerTotal.Replace(".", "");
                //     strTaxPerTotal = "1." + strTaxPerTotal;
                //     TerifAmount = (Convert.ToDouble(txtApplyTax.Text) / Convert.ToDouble(strTaxPerTotal));
                //     double TerifAmt_decimal = Math.Round(TerifAmount, 2);
                //     txtRoomtariff1.Text = Convert.ToString(TerifAmt_decimal);
                //     TaxAmount = (Convert.ToDouble(txtApplyTax.Text) - Convert.ToDouble(txtRoomtariff1.Text));
                //     double TaxAmt_Decimal = Math.Round(TaxAmount,2);
                //     txtRoomTariffTax.Text = Convert.ToString(TaxAmt_Decimal);
                //     double TotalAmount = (Convert.ToDouble(txtRoomtariff1.Text) + Convert.ToDouble(txtRoomTariffTax.Text));
                //     double totalAmount_decimal = Math.Round(TotalAmount, 2);
                //     txtTotalRoomtariff.Text = Convert.ToString(totalAmount_decimal);
                //}
                #endregion

            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        public void CheckSlab(string CODE, string RoomTarif)
        {
            try
            {
                //cmd = new SqlCommand("SELECT * FROM tbTaxSlab WHERE code='" + CODE + "' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY tax_per", con.connect());
                cmd = new SqlCommand("SP_FrmTransReservation", con.connect());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@variable", "SELECT");
                cmd.Parameters.AddWithValue("@tablename", "tbTaxSlab");
                cmd.Parameters.AddWithValue("@code", CODE);
                cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                SqlDataReader slab_dr = cmd.ExecuteReader();
                while (slab_dr.Read())
                {
                    if (Convert.ToDouble(RoomTarif) >= Convert.ToDouble(slab_dr["from_range"].ToString()) && Convert.ToDouble(slab_dr["to_range"].ToString()) >= Convert.ToDouble(RoomTarif))
                    {
                        slabPerVal = Convert.ToDouble(slab_dr["tax_per"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void txtApplyTax_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //  e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back);
                if (txtApplyTax.Text != "")
                {
                    if (cmbApplyTax.SelectedItem == "Excluding")
                    {
                        txtRoomtariff1.Text = txtApplyTax.Text;
                        CalculateRoomTerrif();
                    }
                    else if (cmbApplyTax.SelectedItem == "Including")
                    {
                        CalculateRoomTerrif();
                    }
                }
                else
                {
                    txtRoomtariff1.Text = "0";
                    txtRoomTariffTax.Text = "0";
                    txtTotalRoomtariff.Text = "0";
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void cmbApplyTax_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtApplyTax_TextChanged(sender, e);
        }

        private void txtReason_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                BtnSave.Enabled = true;
                BtnSave.Focus();
            }
        }

        private void dtgridReservation_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Fillgriddetails();
        }

        private void dtgridReservation_KeyDown(object sender, KeyEventArgs e)
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
                        i = dtgridReservation.SelectedCells[0].RowIndex + 1;
                    else if (e.KeyCode == Keys.Up)
                        i = dtgridReservation.SelectedCells[0].RowIndex - 1;

                    if (((e.KeyCode == Keys.Down) && (i < dtgridReservation.RowCount)) || ((e.KeyCode == Keys.Up) && (i >= 0)))
                    {
                        dtgridReservation.Refresh();
                        dtgridReservation.CurrentCell = dtgridReservation.Rows[i].Cells[0];
                        dtgridReservation.Rows[i].Selected = true;
                        //if (dtgridReservation.Rows.Count > 0)
                        //{
                        //    txtsearch.Text = dtgridReservation.Rows[i].Cells[0].Value.ToString();
                        //}
                    }
                    txtsearch.Text = "";
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

        private void txtsearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                dtgridReservation_KeyDown(sender, e);
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            mod.transSearch(txtsearch, dtgridReservation, 0);
        }

        private void txtsearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && txtsearch.Text == "")
            {
                BtnAdd.Focus();
            }
        }

        private void tabReservation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SqlDataReader dr, dr1;
                string str, Pdate, stmess;
                int i = 0, j = 1, sup = 0, k;
                DataTable dt = new DataTable();
                DataRow drw;

                if (tabReservation.SelectedTab == tabDetail)
                {
                    if (flag == 1)
                    {
                        if (dtgridReservation.RowCount > 0)
                        {
                            k = dtgridReservation.SelectedCells[0].RowIndex;
                            if (dtgridReservation.Rows[k].Cells[1].Value.ToString() != "" && grdchange != 1)
                            {
                                i = dtgridReservation.SelectedCells[1].RowIndex;
                                dtgridReservation.Text = dtgridReservation.Rows[i].Cells[1].Value.ToString();
                                string ID = mod.Isnull(dtgridReservation.Rows[i].Cells[1].Value.ToString(), "");
                                SqlDataReader dareader = mod.GetSelectAllField(tableName, firstCol, ID, "SrNo");
                                FillDetails(dareader, "null");
                                BtnDelete.Text = "DELETE";
                                BtnSave.Text = "UPDATE";
                            }
                            else
                            {
                                MessageBox.Show("No record to display");
                                this.tabReservation.SelectedTab = tabList;
                            }
                        }
                    }
                }
                else
                {
                    FillGrid("tbReservation");
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
                    txtReservNo.Text = "";
                    txtReservNo.Text = dr["Reserve_No"].ToString();
                    dtpFromDate.Text = dr["FromDate"].ToString();
                    dtpInTime.Text = dr["InTime"].ToString();
                    dtpToDate.Text = dr["ToDate"].ToString();
                    txtCustId.Text = mod.Isnull(dr["Customer_ID"].ToString(), "0");
                    fillCustID();
                    txtGuestID.Text = mod.Isnull(dr["Guest_ID"].ToString(), "0");
                    fillGuestID();
                    txtNoOfAdult.Text = mod.Isnull(dr["No_Of_Adult"].ToString(), "0");
                    txtChild.Text = mod.Isnull(dr["Child"].ToString(), "0");
                    txtRoomCategry.Text = mod.Isnull(dr["Room_Type"].ToString(), "0");
                    fillRoomCat();
                    txtReserveRoom.Text = mod.Isnull(dr["No_Of_Rooms"].ToString(), "0");
                    string filc = Convert.ToString(dr["ApplyTax_YN"]);
                    if (filc != "")
                    {
                        if (filc == "Y")
                        {
                            cmbApplyTax.Items.Insert(0, "Including");
                        }
                        else if (filc == "N")
                        {
                            cmbApplyTax.Items.Insert(0, "Excluding");
                        }
                        cmbApplyTax.SelectedIndex = 0;
                    }
                    FillApplyTax();
                    txtTotalRoomtariff.Text = mod.Isnull(dr["TotalRoom_Tariff"].ToString(), "0");
                    txtRoomtariff1.Text = mod.Isnull(dr["Room_Tariff"].ToString(), "0");

                    txtRoomtariff1.Enabled = false;
                    txtRoomTariffTax.Enabled = false;
                    txtTotalRoomtariff.Enabled = false;

                    if (cmbApplyTax.SelectedItem == "Excluding")
                    {
                        txtApplyTax.Text = mod.Isnull(dr["Room_Tariff"].ToString(), "0");
                        double TaxAmount = (Convert.ToDouble(txtTotalRoomtariff.Text) - Convert.ToDouble(txtRoomtariff1.Text));
                        double TaxAmt_Decimal = Math.Round(TaxAmount, 2);
                        txtRoomTariffTax.Text = Convert.ToString(TaxAmt_Decimal);
                    }
                    else if (cmbApplyTax.SelectedItem == "Including")
                    {
                        txtApplyTax.Text = mod.Isnull(dr["TotalRoom_Tariff"].ToString(), "0");
                        double TaxAmount = (Convert.ToDouble(txtTotalRoomtariff.Text) - Convert.ToDouble(txtRoomtariff1.Text));
                        double TaxAmt_Decimal = Math.Round(TaxAmount, 2);
                        txtRoomTariffTax.Text = Convert.ToString(TaxAmt_Decimal);
                    }
                    txtchildRoomRate.Text = mod.Isnull(dr["RoomRate_Child"].ToString(), "0");
                    txtAdvance1.Text = mod.Isnull(dr["Advance_Amount"].ToString(), "0");
                    txtBookedBy.Text = mod.Isnull(dr["Booked_By"].ToString(), "0");
                    fillEmpID();
                    txtRemarks.Text = mod.Isnull(dr["Remark"].ToString(), "");
                    txtReason.Text = mod.Isnull(dr["Reason"].ToString(), "");
                    txtPlan.Text = mod.Isnull(dr["Plan_Reserv"].ToString(), "");

                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtAdvance1.Text != "" || txtAdvance1.Text != "0")
            {
                FrmReceipt frmreceipt = new FrmReceipt();
                frmreceipt.Show();
            }
            else if (txtAdvance1.Text == "" || txtAdvance1.Text == "0")
            {
                txtBookedBy.Focus();
            }
        }


        private void FrmReservation_Shown(object sender, EventArgs e)
        {
            txtsearch.Focus();
        }

        private void txtGuestID_KeyDown(object sender, KeyEventArgs e)
        {
            txtAddress.Text = String.Empty;
            txtContactNo.Text = String.Empty;
            txtState.Text = String.Empty;
            txtPlace.Text = String.Empty;

            Globalvariable.SearchString = "GuestInfo";
            var returnForm = Models.Common.FormManagement.Instance.SearchFormOnKeyDown(ref txtGuestID, e, "FrmReservation", true);
            if (returnForm == null)
            {
                return;
            }

            txtGuestID.Text = returnForm.codeselected;
            txtGuestname.Text = returnForm.descselected;

            if (returnForm.codeselected != null && returnForm.codeselected.Trim().Length > 0)
            {
                fillGuestID();

                SelectNextControl(txtGuestID, true, true, true, true);
            }
            //try
            //{
            //    Globalvariable.SearchString = "CustName";
            //    if (e.KeyCode == Keys.Enter && txtCustId.Text != "")
            //    {
            //        fillCustID();
            //    }
            //    else if (txtCustId.Text == "" && e.KeyCode == Keys.Enter)
            //    {
            //        KeysConverter kc = new KeysConverter();
            //        string key = kc.ConvertToString(e.KeyCode);
            //        FrmSearch srch = new FrmSearch();
            //        Globalvariable.SearchString = "SupplierMaster";
            //        srchda = mod.FillSearchGrid_FrmName("FrmReservation", "tbSupplierMaster");
            //        srch.val = key;
            //        srch.fillbackgridSrch(srchda, "FrmReservation");
            //        ds.Clear();
            //        srchda.Fill(ds);
            //        if (ds.Tables[0].Rows.Count > 0)
            //            srch.ShowDialog();
            //        txtCustId.Text = srch.codeselected;
            //        txtCustName.Text = srch.descselected;
            //        if (srch.codeselected == null)
            //        {
            //            txtCustId.Focus();
            //        }
            //        //else
            //        //{
            //        //    fillRoomCat();
            //        //}
            //        Globalvariable.searchNo = 0;
            //        if (txtCustName.Text != "")
            //        {
            //            txtGuestID.Focus();
            //        }
            //        else
            //        {
            //            txtCustId.Focus();
            //        }
            //    }
            //    else if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            //    {
            //        txtCustId.Text = "";
            //        txtCustName.Text = "";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    string str = "Message:" + ex.Message;
            //    MessageBox.Show(str, "Error Message");
            //}
        }
    }
}

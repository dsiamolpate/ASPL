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
    public partial class FrmSupplierMaster : Form
    {
        Design ObjDesign=new Design();
        public FrmSupplierMaster()
        {
            InitializeComponent();
        }
       // FrmSupplierMaster frmSupp = new FrmSupplierMaster();
        int detail_id, selectrow,opmode;
        Module mod = new Module();
        Connection con = new Connection();
        DataSet ds = new DataSet();
        public static SqlDataReader srchdr;
        public static SqlDataAdapter srchda;
        string str, filc, ActiveYes="N", wrd, filmsid, marketseg, strsrch,StrUpdate;
        public static int searchNo;
        string firstCol = "supplier_id";
        string secondCol = "supplier_name";
        string tableName = "tbSupplierMaster";
        string Deleted = "deleted";
        int flagSave = 0;
      //  FrmSupplierMaster frmSupp = new FrmSupplierMaster();

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
        private void FrmSupplierMaster_Load(object sender, EventArgs e)
        {
            LoadEvent();
        }

        public void LoadEvent()
        {
            try
            {
               // mod.centerme(this);
                ObjDesign.FormDesign(this, DtgridSupMaster);
                txtsearch.Focus();
               // ChkDeleted.Checked = false;
                //txtTallyName.Enabled = false;
                txtcode.Enabled = false;
                txtCrDr1.Enabled = false;
                if (Globalvariable.PartyLedgertype == "3")
                {
                   // str = "SELECT supplier_name,supplier_id FROM tbSupplierMaster WHERE deleted='N' AND cust_type='C'  AND Branchcode='" + Globalvariable.bcode + "' ORDER BY supplier_id";
                    mod.FormLoad(this, tableName, firstCol, secondCol, Deleted, "N", "C", DtgridSupMaster, txtsearch, ChkDeleted, "Description", "Code");
                  //  lblsupp.Text = "Creditor";
                    this.Text = "Creditor";
                   
                }
                else if (Globalvariable.PartyLedgertype == "4")
                    {
                       // str = "SELECT supplier_name,supplier_id FROM tbSupplierMaster WHERE deleted='N' AND cust_type='D' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY supplier_id";
                        mod.FormLoad(this, tableName, firstCol, secondCol, Deleted, "N", "D", DtgridSupMaster, txtsearch, ChkDeleted, "Description", "Code");
                   // lblsupp.Text = "Debitor";
                        this.Text = "Debitor";
                    }
               // mod.fillgrid(str, DtgridSupMaster);
                txtsearch.Text = "";           
                FillDetails();
                if (Globalvariable.PartyLedgertype == "3")
                {
                    mod.UserAccessibillityMaster("Creditor", BtnAdd, BtnSave, BtnDelete);
                }
                else if (Globalvariable.PartyLedgertype == "4")
                    {
                        mod.UserAccessibillityMaster("Debitor", BtnAdd, BtnSave, BtnDelete);
                    }
                Globalvariable.SearchChangeVariable = "SrchByName";
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        public void FillActive()
        {
           cmbActive.Items.Clear();
           cmbActive.Items.Insert(0, "Active");
           cmbActive.Items.Insert(1, "InActive");
           cmbActive.SelectedIndex = 0;

        }

        public void fillindexcom()
        {
            cmbActive.Items.Clear();
            cmbActive.Items.Insert(0, "Active");
            cmbActive.Items.Insert(1, "InActive");
            //   comballowdesc.SelectedIndex = 1;
        }

        private void getTallyInfo()
        {
            try
            {
                if (txtTallyCode.Text.Trim().Equals(""))
                {
                    lblTallyName.Text = "";
                }

                str = "SELECT * FROM tbTallyMaster WHERE Branchcode='" + Globalvariable.bcode + "' AND Tally_Code=" +
                   txtTallyCode.Text + "";
                SqlDataReader dr1 = mod.GetRecord(str);
                if (dr1.HasRows)
                {
                    dr1.Read();
                    lblTallyName.Text = dr1["Tally_Name"].ToString();
                }

            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        public void SeleMarSeg( string val )
        {
         //   SqlDataReader dr = mod.GetRecord("SELECT * FROM tbMarketSegment WHERE Deleted='N' AND Branchcode='" + Globalvariable.bcode + "' AND Market_Code='" + val + "'");
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tbMarketSegment WHERE Deleted='N' AND Branchcode='" + Globalvariable.bcode + "' AND Market_Code='" + val + "'", con.connect());
                DataSet ds = new DataSet();
                da.Fill(ds, "tbMarketSegment");
                cmbmarketSeg.DisplayMember = "Description";
                cmbmarketSeg.ValueMember = "Market_Code";
                cmbmarketSeg.DataSource = ds.Tables["tbMarketSegment"];
                cmbmarketSeg.SelectedIndex = 0;
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
                detail_id = 1;
                if (ChkDeleted.Checked == true)
                {
                    mod.chkDeletedTrue(this, BtnAdd, BtnSave, BtnDelete, DtgridSupMaster, txtsearch);
                    txtsearch.Focus();
                   txtTallyCode.Enabled = false;
                }
                else
                {
                    if (Globalvariable.PartyLedgertype == "3")
                    {
                        mod.chkDeletedFalse("Creditor", this, BtnAdd, BtnSave, BtnDelete, DtgridSupMaster, txtsearch);
                    }
                    if (Globalvariable.PartyLedgertype == "4")
                    {
                        mod.chkDeletedFalse("Debitor", this, BtnAdd, BtnSave, BtnDelete, DtgridSupMaster, txtsearch);
                    }
                    txtTallyCode.Enabled = true;
                }


                if (DtgridSupMaster.Rows.Count > 0)
                {
                    int i;
                    if (StrUpdate == "U")
                    {
                        i = selectrow;
                    }
                    else
                    {
                        i = DtgridSupMaster.SelectedCells[0].RowIndex;
                    }
                    if (DtgridSupMaster.Rows[i].Cells[1].Value != null)
                    {
                        string ID = mod.Isnull(DtgridSupMaster.Rows[i].Cells[1].Value.ToString(), "");
                       // SqlDataReader dr = mod.GetRecord("SELECT * FROM tbSupplierMaster WHERE supplier_id='" + s + "' AND Branchcode='" + Globalvariable.bcode + "'");
                        SqlDataReader dr = mod.GetSelectAllField(tableName, firstCol, ID, "");
                        while (dr.Read())
                        {
                            txtcode.Text = "";
                            txtcode.Text = dr["supplier_id"].ToString();
                            txtcode.Enabled = false;
                            txtName.Text = mod.Isnull(dr["supplier_name"].ToString(), "");
                            txtAddress1.Text = mod.Isnull(dr["addresss1"].ToString(), "");
                            txtAddress2.Text = mod.Isnull(dr["address2"].ToString(), "-");
                            txtMobileNo1.Text = mod.Isnull(dr["mobile_no1"].ToString(), "0");
                            txtMobileNo2.Text = mod.Isnull(dr["mobile_no2"].ToString(), "0");
                            txtopenbal.Text = mod.Isnull(dr["opening_bal"].ToString(), "0");
                            txtCrDr.Text = dr["opening_crdr"].ToString();
                            txtclosebal.Text = mod.Isnull(dr["closing_bal"].ToString(), "0");
                            txtclosebal.Enabled = false;
                            txtCrDr1.Text = mod.Isnull(dr["closing_crdr"].ToString(), "");
                            filmsid = Convert.ToString(dr["market_code"]);
                            if(filmsid!="0")
                            {
                            SeleMarSeg(filmsid);
                            }
                            txtTallyCode.Text = mod.Isnull(dr["tally_id"].ToString(), "0");
                            //txtTallyName.Enabled = false;
                            if (txtTallyCode.Text != "0")
                            {
                                getTallyInfo();
                            }
                            else if (txtTallyCode.Text == "0")
                            {
                                lblTallyName.Text = "";
                            }
                            txtEmailId.Text = mod.Isnull(dr["email_id"].ToString(), "");
                            filc = Convert.ToString(dr["active"]);
                            if (filc != " ")
                            {
                                if (filc == "Y")
                                    cmbActive.Items.Insert(0, "Active");

                                else if (filc == "N")
                                    cmbActive.Items.Insert(0, "InActive");
                            }
                            cmbActive.SelectedIndex = 0;
                            fillindexcom();
                            txtGST.Text = mod.Isnull(dr["comp_GST"].ToString(), "0");
                        }
                    }
                    txtsearch.Focus();
                }
                else
                {
                    mod.txtclear(this.Controls);
                    FillActive();
                    fillMarketSeg();
                    BtnDelete.Text = "RESET";

                }
           //    lblSucessMsg.Text = "";
                detail_id = 0;
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

        public void add()
        {
            try
            {
                opmode = 1;
                mod.addBtnClick(this, txtcode, tableName, firstCol, BtnSave, BtnDelete, txtName);
                if (Globalvariable.PartyLedgertype == "3")
                {
                    mod.UserAccessibillityMaster("Creditor", BtnAdd, BtnSave, BtnDelete);
                }
                else if (Globalvariable.PartyLedgertype == "4")
                {
                    mod.UserAccessibillityMaster("Debitor", BtnAdd, BtnSave, BtnDelete);
                }
               // maxID();
                txtcode.Text = "" + mod.GetMaxNum(tableName, firstCol);
                txtclosebal.Enabled = false;
                FillActive();
                fillMarketSeg();
                txtCrDr.Text = "C";
                txtCrDr1.Text = "C";
                lblSucessMsg.Text = "";
                label1.Text = "Search by Name :";
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

        public void maxID()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT (ISNULL(MAX(supplier_id),0)+1) FROM tbSupplierMaster WHERE Branchcode='" + Globalvariable.bcode + "'", con.connect());
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

        private void txtsearch_Enter(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.Yellow;
          }

        public void fillMarketSeg()
        {
            try
            {
                string query = "select Description, Market_Code from tbMarketSegment";
                SqlDataAdapter da = new SqlDataAdapter(query, con.connect());
                DataSet ds = new DataSet();
                da.Fill(ds, "tbMarketSegment");
               cmbmarketSeg.DisplayMember = "Description";
               cmbmarketSeg.ValueMember = "Market_Code";
               cmbmarketSeg.DataSource = ds.Tables["tbMarketSegment"];
                if(detail_id!=1 && ds.Tables[0].Rows.Count!=0)
                {
                    cmbmarketSeg.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }




        private void txtName_Enter(object sender, EventArgs e)
        {
            txtName.BackColor = Color.Yellow;
            txtName.SelectionStart = txtName.Text.Length;
        }

        private void txtAddress1_Enter(object sender, EventArgs e)
        {      
            txtAddress1.BackColor = Color.Yellow;
           txtAddress1.SelectionStart = txtAddress2.Text.Length;
        }

        private void txtAddress2_Enter(object sender, EventArgs e)
        {
            txtAddress2.BackColor = Color.Yellow;
           txtAddress2.SelectionStart = txtAddress2.Text.Length;
        }

        private void txtPhonNo_Enter(object sender, EventArgs e)
        {
            txtMobileNo1.BackColor = Color.Yellow;
           txtMobileNo1.SelectionStart =txtMobileNo1.Text.Length;
        }

        private void txtMobileNo_Enter(object sender, EventArgs e)
        {
            txtMobileNo2.BackColor = Color.Yellow;
           txtMobileNo2.SelectionStart = txtMobileNo2.Text.Length;
        }

        private void txtopenbal_Enter(object sender, EventArgs e)
        {
            txtopenbal.BackColor = Color.Yellow;
           txtopenbal.SelectionStart = txtopenbal.Text.Length;
        }

        private void txtCrDr_Enter(object sender, EventArgs e)
        {
            txtCrDr.BackColor = Color.Yellow;
           txtCrDr.SelectionStart =txtCrDr.Text.Length;
        }

        private void txtclosebal_Enter(object sender, EventArgs e)
        {
            txtclosebal.BackColor = Color.Yellow;
        }

        private void txtCrDr1_Enter(object sender, EventArgs e)
        {
            txtCrDr1.BackColor = Color.Yellow;
        }
        private void txtLbtno_Enter(object sender, EventArgs e)
        {
            txtTallyCode.BackColor = Color.Yellow;
        }
        private void txtEmailId_Enter(object sender, EventArgs e)
        {
            txtEmailId.BackColor = Color.Yellow;
            txtEmailId.SelectionStart = txtEmailId.Text.Length;
        }

        private void FrmSupplierMaster_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Control nextTab;
                if (e.KeyChar == (char)Keys.Enter)
                {
                    nextTab = ((Control)sender);
                    if (ActiveControl.Name == "txtsearch" && DtgridSupMaster.Rows.Count == 0 && e.KeyChar == (char)Keys.Enter)
                    {
                        txtsearch.Focus();
                    }
                    else
                    {
                        if (ActiveControl.Name == "txtsearch" && DtgridSupMaster.Rows.Count == 0)
                        {
                            nextTab = BtnAdd;
                        }
                        else if (ActiveControl.Name == "txtTallyCode")
                        {
                            if (lblTallyName.Text == "" && txtTallyCode.Text != "0")
                            {
                                txtTallyCode.Focus();
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
        private void txtEmailId_KeyPress(object sender, KeyPressEventArgs e)
        {
             try
            {
                string email;
                e.Handled = !(e.KeyChar != (char)Keys.Space);
                if (e.KeyChar == (char)Keys.Enter)
                {
                    email =txtEmailId.Text;
                    if (email != "" && (email.IndexOf('@') < 0 || email.IndexOf('.') < 0))
                    {
                        int s = email.IndexOf('@');
                        txtEmailId.Text = "";
                        txtEmailId.Focus();

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
                if (DtgridSupMaster.Rows.Count > 0)
                {
                    selectrow = DtgridSupMaster.SelectedCells[0].RowIndex;
                }
                if (txtName.Text == "")
                {
                    txtName.Focus();
                }
                else if (txtAddress1.Text == "")
                {
                    txtAddress1.Focus();
                }
                else if (txtMobileNo1.Text == "")
                {
                    txtMobileNo1.Focus();
                }                       
               else
                {
                    if (txtGST.Text != "")
                    {
                        if (opmode == 1)
                        {
                            string strSQL = "SELECT supplier_id FROM tbSupplierMaster WHERE comp_GST= '" + txtGST.Text + "'";
                            SqlDataReader dr = mod.GetRecord(strSQL);
                            if (dr.HasRows)
                            {
                                txtGST.Text = "";
                            }
                        }
                        else
                        {
                            string strSQL = "SELECT * FROM tbSupplierMaster WHERE supplier_id='"+txtcode.Text +"' AND comp_GST= '" + txtGST.Text + "'";
                            SqlDataReader dr = mod.GetRecord(strSQL);
                            if (dr.HasRows)
                            {
                                txtGST.Text = "";
                            }
                        }
                    }
                    if (BtnSave.Text == "SAVE")
                    {                      
                            InsertUpdate();                    
                    }
                    else if (BtnSave.Text == "UPDATE")
                    {                      
                            InsertUpdate();
                    }
                }       
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        public void InsertUpdate()
        {
            try
            {
                string tally_code, gst_no, email;
                string custype = "", strInsert = "";
               
                if (cmbActive.SelectedItem == "Active")
                {
                    ActiveYes = "Y";
                }
                else if (cmbActive.SelectedItem == "InActive")
                {
                    ActiveYes = "N";
                }
                else
                {
                    ActiveYes = "Y";
                }
                tally_code = mod.Isnull(txtTallyCode.Text, "0");
                gst_no = mod.Isnull(txtGST.Text, "0");
                email = mod.Isnull(txtEmailId.Text, "");        
                    if (Globalvariable.PartyLedgertype == "3")
                        custype = "C";
                    else if (Globalvariable.PartyLedgertype == "4")
                        custype = "D";
                        if(cmbmarketSeg.SelectedIndex==0)
                        {
                            filmsid = cmbmarketSeg.SelectedValue.ToString();
                        }
                   
                        //strInsert = "INSERT INTO tbSupplierMaster(supplier_id,supplier_name,addresss1,address2,phone_no,mobile_no,opening_bal,opening_crdr,closing_bal,closing_crdr,deleted,cust_type,Branchcode,tally_id,active,comp_GST,email_id,market_code)"
                        //+ "VALUES(" + txtcode.Text + ",'" + mod.Isnull(txtName.Text.Replace("'", "''"), "") + "','" + mod.Isnull(txtAddress1.Text.Replace("'", "''"), "") + "','" + mod.Isnull(txtAddress2.Text.Replace("'", "''"), "") + "'," +
                        //"'" + mod.Isnull(txtMobileNo1.Text, "") + "','" + mod.Isnull(txtMobileNo2.Text, "") + "','" + mod.Isnull(txtopenbal.Text, "0") + "','" + mod.Isnull(txtCrDr.Text, "") + "'," +
                        //"'" + mod.Isnull(txtclosebal.Text, "0") + "','" + mod.Isnull(txtCrDr1.Text, "") + "','N','" + custype + "','" + Globalvariable.bcode + "','" + tally_code + "','" + ActiveYes + "','" + gst_no + "','" + email + "','"+filmsid+"')";
                        //SqlCommand cmd = new SqlCommand(strInsert, con.connect());  
                        SqlCommand cmd = new SqlCommand("SP_INSERTSupplerMast", con.connect());
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (BtnSave.Text == "SAVE")
                        {
                        txtcode.Text = "" + mod.GetMaxNum(tableName, firstCol);
                        cmd.Parameters.AddWithValue("@variable", "INSERT");
                        }
                        else if (BtnSave.Text == "UPDATE")
                        {
                         cmd.Parameters.AddWithValue("@variable", "UPDATE");
                        }
                        cmd.Parameters.AddWithValue("@code", txtcode.Text);
                        cmd.Parameters.AddWithValue("@name", mod.Isnull(txtName.Text.Replace("'", "''"), ""));
                        cmd.Parameters.AddWithValue("@add1", mod.Isnull(txtAddress1.Text.Replace("'", "''"), ""));
                        cmd.Parameters.AddWithValue("@add2", mod.Isnull(txtAddress2.Text.Replace("'", "''"), ""));
                        cmd.Parameters.AddWithValue("@mobile1",txtMobileNo1.Text);
                        cmd.Parameters.AddWithValue("@mobile2", mod.Isnull(txtMobileNo2.Text, "0"));
                        cmd.Parameters.AddWithValue("@openingBal", mod.Isnull(txtopenbal.Text, "0"));
                        cmd.Parameters.AddWithValue("@openingCrDr", mod.Isnull(txtCrDr.Text, ""));
                        cmd.Parameters.AddWithValue("@closeningBal", mod.Isnull(txtclosebal.Text, "0"));
                        cmd.Parameters.AddWithValue("@closeningCrDr", mod.Isnull(txtCrDr1.Text, ""));
                        cmd.Parameters.AddWithValue("@CustType", custype);
                        cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                        cmd.Parameters.AddWithValue("@tallyId", tally_code);
                        cmd.Parameters.AddWithValue("@active", ActiveYes);
                        cmd.Parameters.AddWithValue("@companyGst", gst_no);
                        cmd.Parameters.AddWithValue("@emailID", email);
                        cmd.Parameters.AddWithValue("@marketCode", mod.Isnull(filmsid,"0"));
                        cmd.ExecuteNonQuery();                                
                      if (BtnSave.Text == "SAVE")
                    {
                        lblSucessMsg.Text = "Save Successfully !!!";
                    }
                    else if (BtnSave.Text == "UPDATE")
                    {
                        lblSucessMsg.Text = "Update Successfully !!!";
                         StrUpdate = "U";
                    }
                       
                    
                    //i = mod.Executequery(strInsert);
                    

                    if (Globalvariable.PartyLedgertype == "3")
                    {
                        // str = "SELECT supplier_name,supplier_id FROM tbSupplierMaster WHERE deleted='N' AND cust_type='C' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY supplier_id";
                        fillDisplayGrid("C");
                    }
                    else if (Globalvariable.PartyLedgertype == "4")
                    {                      
                        fillDisplayGrid("D");
                    }
                        FillDetails();                  
                       txtsearch.Focus();
                       txtsearch.Text = "";
                       if (BtnSave.Text == "SAVE")
                       {
                           if (Globalvariable.frmName == "Reservation")
                           {
                               Globalvariable.SupplierID= txtcode.Text;
                               Globalvariable.SupplierName = txtName.Text;
                               this.Close();
                           }
                           else
                           {
                               int s = selectrow;
                               if (DtgridSupMaster.Rows.Count > 0)
                               {
                                   DtgridSupMaster.CurrentCell = DtgridSupMaster[0, s];
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

        private void fillDisplayGrid(string custType)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                DtgridSupMaster.AutoGenerateColumns = true;
                ds = mod.GetFillGrid(tableName, firstCol, secondCol, "N", custType, "Party Ledger Name", "Code");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DtgridSupMaster.DataSource = ds.Tables[0];
                    DtgridSupMaster.Columns[0].Width = 234;
                    DtgridSupMaster.Columns[1].Width = 76;
                }
                else
                {
                    DtgridSupMaster.DataSource = null;
                    dt.Columns.Add("Party Ledger Name", typeof(string));
                    dt.Columns.Add("Code", typeof(string));
                    DtgridSupMaster.DataSource = dt;
                    DtgridSupMaster.Columns[0].Width = 234;
                    DtgridSupMaster.Columns[1].Width = 76;
                }
               
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }


        private void txtopenbal_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == '.');

              
                // mod.rateValidation(sender, e, TxtRt1, 4, 2);
                if (e.KeyChar == (char)Keys.Enter)
                {
                        txtclosebal.Text = txtopenbal.Text;                 
                }
              //  mod.rateValidation(sender, e, txtopenbal, 2, 2);               


                //if (e.KeyChar == (char)Keys.Enter)
                //{
                //    if (mod.checkRate(txtopenbal) == false)
                //    {
                //        txtopenbal.Text = "";
                //        txtopenbal.Focus();
                //    }
                //}
                //else
                //    if ((txtopenbal.Text.Length == 0) && (e.KeyChar == '.'))
                //        e.Handled = true;
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void DtgridSupMaster_CellClick(object sender, DataGridViewCellEventArgs e)
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

        private void txtCrDr_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !(e.KeyCode == Keys.D || e.KeyCode == Keys.C || e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete);
        }

        private void DtgridSupMaster_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
             try
            {
                FillDetails();
                if (ChkDeleted.Checked == true)
                    BtnDelete.Focus();
                else
                    txtName.Focus();
                lblSucessMsg.Text = "";
                BtnSave.Enabled = true;

            }
             catch (Exception ex)
             {
                 string str = "Message:" + ex.Message;
                 MessageBox.Show(str, "Error Message");
             }
        }

        private void DtgridSupMaster_KeyDown(object sender, KeyEventArgs e)
        {
             try
            {
                mod.gridKeyDown(e, BtnDelete, txtsearch, DtgridSupMaster, this);
                FillDetails();
                if (e.KeyCode == Keys.Enter && ChkDeleted.Checked == true && DtgridSupMaster.Rows.Count > 0)
                    BtnDelete.Focus();
                else if (e.KeyCode == Keys.Enter && ChkDeleted.Checked == true && DtgridSupMaster.Rows.Count == 0)
                    BtnExit.Focus();
                else if (e.KeyCode == Keys.Enter && ChkDeleted.Checked == false && DtgridSupMaster.Rows.Count == 0)
                    BtnExit.Focus();
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
                DtgridSupMaster_KeyDown(sender, e);
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
                SqlCommand cmd;
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                    wrd = mod.SrchChange(txtsearch);
                    //if (ChkDeleted.Checked == true)
                    //    str = "SELECT supplier_name,supplier_id FROM tbSupplierMaster WHERE deleted='Y' AND cust_type='C' AND Branchcode='" + Globalvariable.bcode + "'  AND supplier_name LIKE '" + wrd + "%' ORDER BY supplier_id";
                    //else
                    //    str = "SELECT supplier_name,supplier_id FROM tbSupplierMaster WHERE deleted='N' and cust_type='C' AND Branchcode='" + Globalvariable.bcode + "'  AND supplier_name LIKE '" + wrd + "%' ORDER BY supplier_id";
                    cmd = new SqlCommand("SP_SearchText", con.connect());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tableName", tableName);
                    cmd.Parameters.AddWithValue("@code", firstCol);
                    cmd.Parameters.AddWithValue("@name", secondCol);
                    cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                    cmd.Parameters.AddWithValue("@colm_name", "Party Ledger Name");
                    cmd.Parameters.AddWithValue("@colm_code", "Code");
                    cmd.Parameters.AddWithValue("@SearchChangeVariable", Globalvariable.SearchChangeVariable);
                    cmd.Parameters.AddWithValue("@text", wrd);
                    if (ChkDeleted.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@deletYN", "Y");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@deletYN", "N");
                    }
                    if (Globalvariable.PartyLedgertype == "3")
                    {
                    cmd.Parameters.AddWithValue("@custtype", "C");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@custtype", "D");
                    }
                    //SqlDataReader sqldr = cmd.ExecuteReader();
                    //DtgridSupMaster.Rows.Clear();
                    //while (sqldr.Read())
                    //{
                    //    string[] row = new string[] { sqldr[0].ToString(), sqldr[1].ToString() };
                    //    DtgridSupMaster.Rows.Add(row);
                    //}
                    DtgridSupMaster.AutoGenerateColumns = true;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    //ds.Clear();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DtgridSupMaster.DataSource = ds.Tables[0];
                        DtgridSupMaster.Columns[0].Width = 234;
                        DtgridSupMaster.Columns[1].Width = 76;
                    }
                    else
                    {
                        DtgridSupMaster.DataSource = null;
                        dt.Columns.Add("Party Ledger Name", typeof(string));
                        dt.Columns.Add("Code", typeof(string));
                        DtgridSupMaster.DataSource = dt;
                        DtgridSupMaster.Columns[0].Width = 234;
                        DtgridSupMaster.Columns[1].Width = 76;
                    }
                if (DtgridSupMaster.Rows.Count <= 0)
                {
                    //DtgridSupMaster.Rows.Clear();
                    mod.txtclear(this.Controls);
                    //DtgridSupMaster.Rows.Clear();
                    mod.CntrlEnable(this.Controls);
                    BtnSave.Enabled = false;
                    BtnDelete.Enabled = false;
                   txtsearch.Enabled = true;
                   txtsearch.Focus();              
                }
                else
                {                   
                    FillDetails();
                }
                lblSucessMsg.Text = "";

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
              
                     if (Globalvariable.PartyLedgertype == "3")
                {
                    mod.chkDeletedChange(ChkDeleted, firstCol, secondCol, tableName, Deleted, "C", DtgridSupMaster, txtsearch, "Ledger Desc", "Code");
                  //  str = "SELECT supplier_name,supplier_id FROM tbSupplierMaster WHERE deleted='Y' AND cust_type='C'  AND Branchcode='" + Globalvariable.bcode + "' ORDER BY supplier_id";
                   // lblsupp.Text = "Creditor";
                    this.Text = "Creditor";
                }
                     else if (Globalvariable.PartyLedgertype == "4")
                     {
                         mod.chkDeletedChange(ChkDeleted, firstCol, secondCol, tableName, Deleted, "D", DtgridSupMaster, txtsearch, "Ledger Desc", "Code");
                         //  str = "SELECT supplier_name,supplier_id FROM tbSupplierMaster WHERE deleted='Y' AND cust_type='D' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY supplier_id";
                         //  lblsupp.Text = "Debitor";
                         this.Text = "Debitor";

                     }
                txtsearch.Text = "";
                txtsearch.Focus();
                FillDetails();
                lblSucessMsg.Text = "";
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
                        FrmSupplierMaster_Load(sender, EventArgs.Empty);
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

        //private void fillDisplayGrid()
        //{
        //    try
        //    {
        //        string str;
        //        DtgridSupMaster.Rows.Clear();
        //        str = "SELECT supplier_name,supplier_id FROM tbSupplierMaster WHERE deleted='N' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY supplier_id";
        //        SqlCommand cmd = new SqlCommand(str, con.connect());
        //        SqlDataReader dr = cmd.ExecuteReader();
        //        while (dr.Read())
        //        {
        //            string[] row = new string[] { dr[0].ToString(), dr[1].ToString() };
        //            DtgridSupMaster.Rows.Add(row);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string str = "Message:" + ex.Message;
        //        MessageBox.Show(str, "Error Message");
        //    }
        //}

        private void cmbActive_Enter(object sender, EventArgs e)
        {
            cmbActive.BackColor = Color.Yellow;
        }

        private void txtGST_Enter(object sender, EventArgs e)
        {
            txtGST.BackColor = Color.Yellow;
        }

        private void txtsearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && txtsearch.Text=="")
            {
                BtnAdd.Focus();
            }
            lblSucessMsg.Text = "";

        }

        private void filltallycode()
        {
            try
            {
                if (txtTallyCode.Text.Trim().Equals(""))
                {
                    txtTallyCode.Text = "";
                }
                else
                {
                    SqlDataReader dr = mod.GetRecord("SELECT Tally_Name FROM tbTallyMaster WHERE Deleted='N' AND Branchcode='" + Globalvariable.bcode +
                            "' AND Tally_Code='" + txtTallyCode.Text + "'");
                    if (dr.HasRows)
                    {
                        dr.Read();
                        lblTallyName.Text = dr["Tally_Name"].ToString();
                    }
                    else 
                    {
                        txtTallyCode.Text = "";
                        lblTallyName.Text = "";
                       // txtTallyCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }


        public string searchChange(string searchStr)
        {
            try
            {
                if (Globalvariable.searchNo == 1)
                {
                    strsrch = "SELECT Tally_Name,Tally_Code FROM tbTallyMaster WHERE deleted='N' OR Tally_Name LIKE '" +
                           searchStr + "%' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY Tally_Code";
                }
                else if (Globalvariable.searchNo == 2)
                {
                    strsrch = "SELECT Tally_Name,Tally_Code FROM tbTallyMaster WHERE deleted='N' AND Tally_Name LIKE '" +
                           searchStr + "%' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY Tally_Code";
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
            return strsrch;
        }

        private void txtTallyCode_KeyDown(object sender, KeyEventArgs e)
        {
            var returnForm = Models.Common.FormManagement.Instance.SearchFormOnKeyDown(ref txtTallyCode, e, "Common_TallyMaster", true);
            if (returnForm == null)
            {
                return;
            }
            txtTallyCode.Text = returnForm.codeselected;
            lblTallyName.Text = returnForm.descselected;
            return;


            //try
            //{
            //    if (txtTallyCode.Text == "" && e.KeyCode == Keys.Enter)
            //    {
            //       Globalvariable.searchNo = 1;
            //        KeysConverter kc = new KeysConverter();
            //        string key = kc.ConvertToString(e.KeyCode);
            //        FrmSearch srch = new FrmSearch();
            //       // str = "SELECT Tally_Name,Tally_Code FROM tbTallyMaster WHERE Deleted='N' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY Tally_Code";
            //        srchda = mod.GetselectQuery("tbTallyMaster", "Tally_Code", "Tally_Name","N", "N");
            //        srch.val = key;
            //        srch.fillbackgridSrch(srchda, "FrmSupplierMaster");
            //        ds.Clear();
            //      srchda.Fill(ds);
            //     if (ds.Tables[0].Rows.Count > 0)
            //         srch.ShowDialog();
            //       txtTallyCode.Text = srch.codeselected;
            //        if (srch.codeselected == null)
            //        {
            //            txtTallyCode.Focus();
            //        }
            //        else
            //        {
            //            filltallycode();
            //        }
            //        Globalvariable.searchNo = 0;
            //        if (lblTallyName.Text != "")
            //        {
            //            cmbActive.Focus();
            //        }
            //        else
            //        {
            //            txtTallyCode.Focus();
            //        }
            //    }
            //    else if (e.KeyCode == Keys.Enter && txtTallyCode.Text != "" && txtTallyCode.Text != "0")
            //    {
            //        filltallycode();
            //    }
            //    else if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete && txtTallyCode.Text != "0")
            //    {
            //        txtTallyCode.Text = "";
            //       lblTallyName.Text = "";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    string str = "Message:" + ex.Message;
            //    MessageBox.Show(str, "Error Message");
            //}
        }

        private void cmbmarketSeg_Enter(object sender, EventArgs e)
        {
            cmbmarketSeg.BackColor = Color.Yellow;
        }

        private void txtGST_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                BtnSave.Focus();
            }
        }

        private void txtEmailId_Validating(object sender, CancelEventArgs e)
        {
           System.Text.RegularExpressions.Regex rEmail = new System.Text.RegularExpressions.Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            if (txtEmailId.Text.Length > 0)
            {
                if (!rEmail.IsMatch(txtEmailId.Text))
                {
                    MessageBox.Show("Please enter valid email address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // txtEmailId.SelectAll();
                    txtEmailId.Focus();
                }
            }
        }

        private void txtMobileNo_Validating(object sender, CancelEventArgs e)
        {
            System.Text.RegularExpressions.Regex rMobil = new System.Text.RegularExpressions.Regex(@"[0-9]{10}");
            if (txtMobileNo2.Text.Length > 0)
            {
                if (!rMobil.IsMatch(txtMobileNo2.Text))
                {
                    MessageBox.Show("Please enter valid mobile no", "Error", MessageBoxButtons.OK);
                    // txtEmailId.SelectAll();
                    txtMobileNo2.Focus();
                }
            }
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space || e.KeyChar == '.' || e.KeyChar == (char)Keys.Space);
        }

        private void txtPhonNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void txtMobileNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void txtclosebal_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == '.');
        }

        private void txtTallyCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void txtsearch_Leave(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.White;
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
           txtName.BackColor = Color.White;
        }

        private void txtAddress1_Leave(object sender, EventArgs e)
        {
            txtAddress1.BackColor = Color.White;
        }

        private void txtAddress2_Leave(object sender, EventArgs e)
        {
            txtAddress2.BackColor = Color.White;
        }

        private void txtPhonNo_Leave(object sender, EventArgs e)
        {
            txtMobileNo1.BackColor = Color.White;
        }

        private void txtMobileNo_Leave(object sender, EventArgs e)
        {
            txtMobileNo2.BackColor = Color.White;
        }

        private void txtEmailId_Leave(object sender, EventArgs e)
        {
            txtEmailId.BackColor = Color.White;
        }

        private void txtopenbal_Leave(object sender, EventArgs e)
        {
           txtopenbal.BackColor = Color.White;
        }

        private void btnLessID_Click(object sender, EventArgs e)
        {

            Models.Common.FormManagement.Instance.SelectRecordFromGrid(DtgridSupMaster, Models.Common.RecordMove.Previous, FillDetails);

        }

        private void btnGreaterID_Click(object sender, EventArgs e)
        {

            Models.Common.FormManagement.Instance.SelectRecordFromGrid(DtgridSupMaster, Models.Common.RecordMove.Next, FillDetails);

        }

        private void txtCrDr_Leave(object sender, EventArgs e)
        {
            txtCrDr.BackColor = Color.White;
        }

        private void txtclosebal_Leave(object sender, EventArgs e)
        {
            txtclosebal.BackColor = Color.White;
        }

        private void txtCrDr1_Leave(object sender, EventArgs e)
        {
            txtCrDr1.BackColor = Color.White;
        }

        private void txtTallyCode_Leave(object sender, EventArgs e)
        {
           txtTallyCode.BackColor = Color.White;
        }

        private void cmbActive_Leave(object sender, EventArgs e)
        {
            cmbActive.BackColor = Color.White;
        }

        private void cmbmarketSeg_Leave(object sender, EventArgs e)
        {
           cmbmarketSeg.BackColor = Color.White;
        }

        private void txtGST_Leave(object sender, EventArgs e)
        {
           txtGST.BackColor = Color.White;
        }

        private void FrmSupplierMaster_KeyDown(object sender, KeyEventArgs e)
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

        private void FrmSupplierMaster_Shown(object sender, EventArgs e)
        {
            txtsearch.Focus();
        }
    }
}

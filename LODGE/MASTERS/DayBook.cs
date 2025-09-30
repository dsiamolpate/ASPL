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
    public partial class DayBook : Form
    {
        Design ObjDesign = new Design();
        public DayBook()
        {
            InitializeComponent();
        }
        int selectrow;
        string firstCol = "book_code";
        string secondCol = "book_description";
        string tableName = "tbBook";
        string Deleted = "deleted";
        string str, strsrch, StrUpdate;
        public static SqlDataReader srchdr;
        SqlDataAdapter srchda;
        DataSet ds = new DataSet();
        SqlCommand cmd;
      //  public static int searchNo;
        Module mod = new Module();
        Connection con = new Connection();
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
        private void DayBook_Load(object sender, EventArgs e)
        {
            LoadEvent();
        }
        public void LoadEvent()
        {
            try
            {
                ObjDesign.FormDesign(this, DtGridDayBook);
                mod.FormLoad(this, tableName, firstCol, secondCol, Deleted,"N","N",DtGridDayBook, txtsearch, ChkDeleted,"Description","Code");
               txtsearch.Focus();
               txtgrpcod.Enabled = false;
               txtgroupnm.Enabled = false;
                FillDetails();
                mod.UserAccessibillityMaster("Day Book", BtnAdd, BtnSave, BtnDelete);
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
                    mod.chkDeletedTrue(this, BtnAdd, BtnSave, BtnDelete, DtGridDayBook, txtsearch);
                    txtsearch.Focus();
                }
                else
                {
                    mod.chkDeletedFalse("Day Book", this, BtnAdd, BtnSave, BtnDelete, DtGridDayBook, txtsearch);
                }

                if (DtGridDayBook.Rows.Count > 0)
                {
                     int i;
                     if (StrUpdate == "U")
                     {
                         i = selectrow;
                     }
                     else
                     {
                         i = DtGridDayBook.SelectedCells[0].RowIndex;
                     }
                    if (DtGridDayBook.Rows[i].Cells[1].Value != null)
                    {
                        string ID = mod.Isnull(DtGridDayBook.Rows[i].Cells[1].Value.ToString(), "");
                       //SqlDataReader dr = mod.GetRecord("SELECT * FROM " + tableName + " WHERE " +
                       //                     firstCol + " ='" + s + "' AND Branchcode='" + Globalvariable.bcode + "'");
                        SqlDataReader dr = mod.GetSelectAllField(tableName, firstCol,ID,"");
                        if (dr.Read())
                        {
                            txtdaybookcod.Text = dr["book_code"].ToString();
                            txtdaybookcod.Enabled = false;
                            txtGenLegnm.Text = dr["book_description"].ToString();
                            txtglcode.Text = dr["gen_leg_code"].ToString();
                            str = "SELECT grp_code,description FROM tbGroup WHERE grp_code='" + dr["group_code"].ToString() + "' AND Branchcode='" + Globalvariable.bcode + "'";
                           SqlDataReader dr1 = mod.GetRecord(str);
                            if (dr1.Read())
                            {
                                txtgroupnm.Text = dr1["description"].ToString();
                                txtgroupnm.Enabled = false;
                                txtgrpcod.Text = dr1["grp_code"].ToString();
                            }
                        }
                    }
                    mod.UserAccessibillityMaster("Designation", BtnAdd, BtnSave, BtnDelete);
                    if (BtnDelete.Enabled == true)
                    {
                        if(txtdaybookcod.Text=="1")
                        {
                            //select * from book where bk_code ='1'and bk_code in (select BK_CODE from REC_HEAD union select BK_CODE from PAY_HEAD union select BK_CODE from FIX_HD union select month from ddc union select month from dlc union select month from savacc union select bk_code from book where gl_code in(select CUST_CODE from CON_DTL))
                        }
                        else
                        {
                           // select * from book where bk_code = '4' and bk_code in (select BK_CODE from REC_HEAD union select BK_CODE from PAY_HEAD union select BK_CODE from FIX_HD union select bk_code from book where gl_code in(select CUST_CODE from CON_DTL)) 
                            //SELECT * FROM tbBook WHERE book_code='' AND book_code IN (SELECT DayBook_Code FROM tbTransReceipt UNION SELECT book_code FROM tbBook)
                            str = "SELECT * FROM tbBook WHERE book_code='"+txtdaybookcod.Text+"' AND book_code IN (SELECT DayBook_Code FROM tbTransReceipt UNION SELECT book_code FROM tbBook)";
                        }
                        mod.CheckUse(str, BtnDelete);
                    }
                }
                else
                {
                    mod.txtclear(this.Controls);
                    BtnDelete.Text = "RESET";
                }
               // lblSucessMsg.Text = "";
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

        private void txtsearch_Enter(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.Yellow;
          
        }

        private void txtGenLegnm_Enter(object sender, EventArgs e)
        {
             txtGenLegnm.BackColor = Color.Yellow;
           
        }


        private void DayBook_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Control nextTab;
                if (e.KeyChar == (char)Keys.Enter)
                {
                    nextTab = ((Control)sender);
                    if (ActiveControl.Name == "txtsearch" && DtGridDayBook.Rows.Count == 0 && e.KeyChar == (char)Keys.Enter)
                    {
                        txtsearch.Focus();
                    }
                    else
                    {
                        if (ActiveControl.Name == "txtsearch" && DtGridDayBook.Rows.Count == 0)
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

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            ADD();
        }
        public void ADD()
        {
            try
            {
                mod.addBtnClick(this, txtdaybookcod, tableName, firstCol, BtnSave, BtnDelete, txtGenLegnm);
                mod.UserAccessibillityMaster("Day Book", BtnAdd, BtnSave, BtnDelete);
                // maxID();
                txtdaybookcod.Text=""+mod.GetMaxNum(tableName, firstCol);
                txtgroupnm.Enabled = false;
                lblSucessMsg.Text = "";
                label1.Text = "Search by Name :";
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }


        private void txtGenLegnm_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (txtGenLegnm.Text == "" && e.KeyCode == Keys.Enter)
                {
                    Globalvariable.searchNo = 1;
                    KeysConverter kc = new KeysConverter();
                    string key = kc.ConvertToString(e.KeyCode);
                    FrmSearch srch = new FrmSearch();
                    //str = "SELECT * FROM tbGeneralLedger WHERE deleted = 'N' AND sub_led='N' AND genledg_code NOT IN (select gen_leg_code from tbBook where deleted='N') ORDER BY genledg_code";
                   srchda = mod.SearchFrmQuery("tbGeneralLedger", "genledg_code", "genledg_desc");
                    srch.val = key;
                    srch.fillbackgridSrch(srchda, "DayBook");
                    ds.Clear();
                    srchda.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                        srch.ShowDialog();
                    txtglcode.Text = srch.codeselected;
                    // getGlInfo();
                    if (srch.codeselected == null)
                        txtGenLegnm.Focus();
                    else
                    {
                        str = "SELECT * FROM tbGeneralLedger WHERE Branchcode='" + Globalvariable.bcode + "' AND genledg_code='" + txtglcode.Text + "'";
                        getGlInfo(str);
                    }
                    Globalvariable.searchNo = 0;
                    if (txtgroupnm.Text != "")
                    {
                        BtnSave.Focus();
                        txtGenLegnm.BackColor = Color.White;
                    }
                    else
                    {
                        txtGenLegnm.Focus();
                    }
                }
                else if (e.KeyCode == Keys.Enter && txtGenLegnm.Text != "")
                {
                   str = "SELECT * FROM tbGeneralLedger WHERE deleted = 'N' AND sub_led='N' AND genledg_desc='" + txtGenLegnm.Text + "'AND genledg_code NOT IN (select gen_leg_code from tbBook where deleted='N')";
                    getGlInfo(str);
                }
                else if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
                {
                    txtGenLegnm.Text = "";
                    txtgroupnm.Text = "";
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void getGlInfo(string selectQuery)
        {
            try
            {
               // string selectQuery = "SELECT genledg_desc,grp_code FROM tbGeneralLedger WHERE deleted = 'N' AND sub_led='N' AND genledg_desc='" + txtGenLegnm.Text + "'AND genledg_code NOT IN (select gen_leg_code from tbBook where deleted='N')";
                SqlDataReader dr1 = mod.GetRecord(selectQuery);
                if (dr1.HasRows)
                {
                    dr1.Read();
                    txtGenLegnm.Text = dr1["genledg_desc"].ToString();
                    txtgrpcod.Text = dr1["grp_code"].ToString();

                    str = "SELECT description FROM tbGroup WHERE deleted='N' AND grp_code='" + dr1["grp_code"] + "'";
                    SqlDataReader dr2 = mod.GetRecord(str);
                    if (dr2.HasRows)
                    {
                        dr2.Read();
                        txtgroupnm.Text = dr2["description"].ToString();
                    }
                }
                else
                    txtGenLegnm.Focus();
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
        private void Save()
        {
            try
            {
                if (DtGridDayBook.Rows.Count > 0)
                {
                    selectrow = DtGridDayBook.SelectedCells[0].RowIndex;
                }
                int i;
                if (txtGenLegnm.Text == "" || txtgroupnm.Text=="")
                {
                    txtGenLegnm.Text = "";
                    txtGenLegnm.Focus();                   
                }
                else
                {                    
                    if (BtnSave.Text == "SAVE")
                        str = "SELECT * FROM " + tableName + " WHERE " + secondCol + "='" + txtGenLegnm.Text.Replace("'", "''") +
                            "' AND Branchcode='" + Globalvariable.bcode + "'";
                    else if (BtnSave.Text == "UPDATE")
                        str = "SELECT * FROM " + tableName + " WHERE " + secondCol + "='" + txtGenLegnm.Text.Replace("'", "''") +
                            "' AND Branchcode='" + Globalvariable.bcode + "' AND " + firstCol + " !=" + txtdaybookcod.Text + "";
                    SqlDataReader dr = mod.GetRecord(str);
                    if (dr.HasRows)
                    {
                        MessageBox.Show("Name Already Exist!!");
                        txtGenLegnm.Focus();
                        txtGenLegnm.Text = "";
                        txtgroupnm.Text = "";
                    }
                    else
                    {
                        BtnSave.Enabled = false;
                        if (BtnSave.Text == "UPDATE")
                        {
                            dr = mod.GetSelectAllField(tableName, firstCol, txtgrpcod.Text, txtdaybookcod.Text);
                            while (dr.Read())
                            {
                                SqlDataReader dataStr, changeData;                        
                                dataStr = mod.GetSelectAllField("tbGroup","grp_code",dr["group_code"].ToString(),"");
                                changeData = mod.GetSelectAllField("tbGeneralLedger", "genledg_code", dr["gen_leg_code"].ToString(), "");
                                if (dataStr.Read())
                                {
                                    mod.compare(txtgroupnm.Text, dataStr["description"].ToString(), "Day Book");
                                }
                                if (changeData.Read())
                                {
                                    mod.compare(txtGenLegnm.Text, changeData["genledg_desc"].ToString(), "Day Book");
                                }
                            }
                        }
                            cmd = new SqlCommand("SP_INSERTDayBook", con.connect());
                            cmd.CommandType = CommandType.StoredProcedure;
                            if (BtnSave.Text == "SAVE")
                            {
                                txtdaybookcod.Text = "" + mod.GetMaxNum(tableName, firstCol);
                                cmd.Parameters.AddWithValue("@variable","INSERT");
                            }
                            else
                            {
                             cmd.Parameters.AddWithValue("@variable", "UPDATE");
                            }
                            cmd.Parameters.AddWithValue("@code", txtdaybookcod.Text);
                            cmd.Parameters.AddWithValue("@name", txtGenLegnm.Text.Replace("'", "''"));
                            cmd.Parameters.AddWithValue("@grpcode", txtgrpcod.Text);
                            cmd.Parameters.AddWithValue("@genlegcode", txtglcode.Text);
                            cmd.Parameters.AddWithValue("@userid",Globalvariable.usercd);
                            cmd.Parameters.AddWithValue("@branchcode",Globalvariable.bcode);
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
                        }
                        
                            mod.DataGridBind(DtGridDayBook, tableName, firstCol, secondCol, "N", "Book Desc", "Code");                    
                            txtsearch.Focus();
                            txtsearch.Text = "";
                            string txt = BtnSave.Text;
                            FillDetails();
                            int s = selectrow;
                            if (DtGridDayBook.Rows.Count > 0)
                            {
                                DtGridDayBook.CurrentCell = DtGridDayBook[0, s];
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

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                string strMsg;
                strMsg = BtnDelete.Text;
                if (BtnDelete.Text == "DELETE" || BtnDelete.Text == "RECALL")
                {
                    i = mod.deleteButtonClick(tableName, firstCol, txtdaybookcod.Text,
                         txtsearch, BtnDelete.Text.Substring(0, 1));
                    if (i == 1)
                    {
                        DayBook_Load(sender, EventArgs.Empty);
                        if (strMsg == "DELETE")
                             lblSucessMsg.Text = "Deleted Successfully !!!";
                        else
                            lblSucessMsg.Text = "Recalled Successfully !!!";
                    }
                }

                else if (BtnDelete.Text == "RESET")
                {
                    ADD();
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void txtgroupnm_KeyPress(object sender, KeyPressEventArgs e)
        {
            BtnSave.Focus();
        }


        private void ChkDeleted_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                mod.chkDeletedChange(ChkDeleted, firstCol, secondCol, tableName, Deleted, "N", DtGridDayBook, txtsearch, "Book Desc", "Code");
                FillDetails();
                lblSucessMsg.Text = "";
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void DtGridDayBook_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
            FillDetails();
            mod.gridDoubleClick(ChkDeleted, BtnDelete, txtGenLegnm);
            lblSucessMsg.Text = "";
            BtnSave.Enabled = true;
        }

        private void DtGridDayBook_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
                mod.gridKeyDown(e, BtnDelete, txtsearch, DtGridDayBook, this);
                FillDetails();
                if (e.KeyCode == Keys.Enter && ChkDeleted.Checked == true && DtGridDayBook.Rows.Count > 0)
                    BtnDelete.Focus();
                else if (e.KeyCode == Keys.Enter && ChkDeleted.Checked == true && DtGridDayBook.Rows.Count == 0)
                    BtnExit.Focus();
                                lblSucessMsg.Text = "";

            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        #region txtSearch Event
        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                mod.txtSearchChange(ChkDeleted, firstCol, secondCol, tableName, DtGridDayBook, txtsearch, "Book Desc", "Code");
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
            if (e.KeyChar == (char)Keys.Enter && txtsearch.Text=="")
            {
               BtnAdd.Focus();
            }
            lblSucessMsg.Text = "";
        }

        private void txtsearch_KeyDown(object sender, KeyEventArgs e)
        {
            DtGridDayBook_KeyDown_1(sender, e);
        }
        #endregion

        private void txtGenLegnm_Leave(object sender, EventArgs e)
        {
            txtGenLegnm.BackColor = Color.White;
        }

        private void txtsearch_Leave(object sender, EventArgs e)
        {
           txtsearch.BackColor = Color.White;
        }

        private void DayBook_KeyDown(object sender, KeyEventArgs e)
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

        private void btnLessID_Click(object sender, EventArgs e)
        {

            Models.Common.FormManagement.Instance.SelectRecordFromGrid(DtGridDayBook, Models.Common.RecordMove.Previous, FillDetails);

        }

        private void btnGreaterID_Click(object sender, EventArgs e)
        {

            Models.Common.FormManagement.Instance.SelectRecordFromGrid(DtGridDayBook, Models.Common.RecordMove.Next, FillDetails);

        }

        private void DtGridDayBook_CellClick(object sender, DataGridViewCellEventArgs e)
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

        private void DayBook_Shown(object sender, EventArgs e)
        {
            txtsearch.Focus();
        }     

    }
}

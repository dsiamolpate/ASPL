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
    public partial class FrmDiscount : Form
    {
        Design ObjDesign = new Design();
        public FrmDiscount()
        {
            InitializeComponent();
        }
        Module mod = new Module();
        Connection con = new Connection();
        string firstCol = "discount_code";
        string secondCol = "name";
        string tableName = "tbDiscountDetail";
        string Deleted = "Deleted";
        string str, filname, strsrch, StrUpdate;
        int flagSave = 0, selectrow;
        public static SqlDataReader srchdr;
        public static SqlDataAdapter srchda;
        DataSet ds = new DataSet();
        //     public static int searchNo;
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
        private void FrmDiscount_Load(object sender, EventArgs e)
        {
            this.txtDiscount.KeyPress += Models.Common.FormManagement.Instance.PercentageTextBox_KeyPress;
            LoadEvent();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            ADD();
        }

        public string searchChange(string searchStr)
        {
            if (Globalvariable.searchNo == 1)
            {
                strsrch = "SELECT genledg_desc,genledg_code FROM tbGeneralLedger WHERE deleted='N' AND genledg_code NOT IN(SELECT gen_leg_code FROM tbBook) OR genledg_desc LIKE '" +
                       searchStr + "%' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY genledg_code";
            }
            else if (Globalvariable.searchNo == 2)
            {
                strsrch = "SELECT genledg_desc,genledg_code FROM tbGeneralLedger WHERE deleted='N' AND genledg_code NOT IN(SELECT gen_leg_code FROM tbBook) AND genledg_desc LIKE '" +
                       searchStr + "%' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY genledg_code";
            }
            return strsrch;
        }


        public void ADD()
        {
            try
            {
                mod.addBtnClick(this, txtDiscCode, tableName, firstCol, BtnSave, BtnDelete, txtname);
                mod.UserAccessibillityMaster("Discount Information", BtnAdd, BtnSave, BtnDelete);
                // maxID();
                txtDiscCode.Text = "" + mod.GetMaxNum(tableName, firstCol);
                lblSucessMsg.Text = "";
                label1.Text = "Search by Name :";
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        public void LoadEvent()
        {
            ObjDesign.FormDesign(this, DtgridDiscount);
            //  SELECT [name]+'  '+ CAST(disc as nvarchar(20)) + '%' AS [Name],[discount_code] AS [Code]  FROM dbo.[tbDiscountDetail] WHERE DELETED IN ('N') AND Branchcode='1001' ORDER BY [discount_code]
            txtDiscCode.Enabled = false;
            //txtGLname.Enabled = false;
            mod.FormLoad(this, tableName, firstCol, secondCol, Deleted, "N", "N", DtgridDiscount, txtsearch, Chkshowdelet, "Description", "Code");
            FillDetails();
            mod.UserAccessibillityMaster("Discount Information", BtnAdd, BtnSave, BtnDelete);
            Globalvariable.SearchChangeVariable = "SrchByName";
        }

        public void FillDetails()
        {
            try
            {
                if (Chkshowdelet.Checked == true)
                {
                    mod.chkDeletedTrue(this, BtnAdd, BtnSave, BtnDelete, DtgridDiscount, txtsearch);
                    txtsearch.Focus();
                }
                else
                {
                    mod.chkDeletedFalse("Discount Information", this, BtnAdd, BtnSave, BtnDelete, DtgridDiscount, txtsearch);
                }
                if (DtgridDiscount.Rows.Count > 0)
                {
                    int i;
                    if (StrUpdate == "U")
                    {
                        i = selectrow;
                    }
                    else
                    {
                        i = DtgridDiscount.SelectedCells[0].RowIndex;
                    }

                    if (DtgridDiscount.Rows[i].Cells[1].Value != null)
                    {
                        string ID = mod.Isnull(DtgridDiscount.Rows[i].Cells[1].Value.ToString(), "");
                        //SqlDataReader dr = mod.GetRecord("SELECT * FROM tbDiscountDetail WHERE discount_code='" + s + "' AND Branchcode='" + Globalvariable.bcode + "'");
                        SqlDataReader dr = mod.GetSelectAllField(tableName, firstCol, ID, "");
                        while (dr.Read())
                        {
                            txtDiscCode.Text = "";
                            txtDiscCode.Text = dr["discount_code"].ToString();
                            txtDiscCode.Enabled = false;
                            txtDiscount.Text = mod.Isnull(dr["disc"].ToString(), "0");
                            txtname.Text = mod.Isnull(dr["name"].ToString(), "-");
                            txtGLcod.Text = mod.Isnull(dr["genleg_code"].ToString(), "0");
                            if (txtGLcod.Text != "0")
                            {
                                getGLInfo();
                            }
                            else
                            {
                                lblGLName.Text = "";
                            }
                            filname = txtname.Text;
                        }
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

        private void getGLInfo()
        {
            try
            {
                if (txtGLcod.Text.Trim().Equals(""))
                {
                    lblGLName.Text = "";
                }
                else
                {
                    str = "SELECT * FROM tbGeneralLedger WHERE  deleted='N' AND Branchcode='" + Globalvariable.bcode + "' AND genledg_code=" +
                       txtGLcod.Text + " AND genledg_code NOT IN(SELECT gen_leg_code FROM tbBook) ";
                    SqlDataReader dr1 = mod.GetRecord(str);
                    if (dr1.HasRows)
                    {
                        dr1.Read();
                        lblGLName.Text = dr1["genledg_desc"].ToString();
                    }
                    else
                    {
                        txtGLcod.Text = "";
                        lblGLName.Text = "";

                    }
                }
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

        private void txtname_Enter(object sender, EventArgs e)
        {
            txtname.BackColor = Color.Yellow;

        }

        private void txtDiscount_Enter(object sender, EventArgs e)
        {
            txtDiscount.BackColor = Color.Yellow;

        }

        private void txtGLcod_Enter(object sender, EventArgs e)
        {
            txtGLcod.BackColor = Color.Yellow;
            txtGLcod.SelectionStart = txtGLcod.Text.Length;
        }

        private void FrmDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Control nextTab;
                if (e.KeyChar == (char)Keys.Enter)
                {
                    nextTab = ((Control)sender);
                    if (ActiveControl.Name == "txtsearch" && DtgridDiscount.Rows.Count == 0 && e.KeyChar == (char)Keys.Enter)
                    {
                        txtsearch.Focus();
                    }
                    else
                    {
                        if (ActiveControl.Name == "txtsearch" && DtgridDiscount.Rows.Count == 0)
                        {
                            nextTab = BtnAdd;
                        }
                        else if (ActiveControl.Name == "txtGLcod")
                        {
                            if (lblGLName.Text == "" && txtGLcod.Text != "0")
                            {
                                txtGLcod.Focus();
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

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtGLcod_KeyDown(object sender, KeyEventArgs e)
        {
            var returnForm = Models.Common.FormManagement.Instance.SearchFormOnKeyDown(ref txtGLcod, e, "FrmDiscount", true);
            if (returnForm == null)
            {
                return;
            }

            txtGLcod.Text = returnForm.codeselected;
            lblGLName.Text = returnForm.descselected;

            //return;
            //try
            //{
            //     if (e.KeyCode == Keys.Enter && txtGLcod.Text != ""&& txtGLcod.Text!="0")
            //        {
            //            getGLInfo();
            //        }
            //          else if (txtGLcod.Text == "" && e.KeyCode == Keys.Enter)
            //       {
            //       Globalvariable.searchNo = 1;
            //        txtGLname.Text = "";
            //        KeysConverter kc = new KeysConverter();
            //        string key = kc.ConvertToString(e.KeyCode);
            //        FrmSearch srch = new FrmSearch();
            //       //str = "SELECT genledg_desc,genledg_code FROM tbGeneralLedger WHERE deleted='N' AND sub_led='N' And Branchcode='" + Globalvariable.bcode + "' AND genledg_code NOT IN(SELECT gen_leg_code FROM tbBook) ORDER BY genledg_code";
            //        srchda = mod.SearchFrmQuery("tbGeneralLedger", "genledg_code", "genledg_desc");
            //         srch.val = key;
            //         srch.fillbackgridSrch(srchda, "FrmDiscount");
            //         ds.Clear();
            //       srchda.Fill(ds);
            //          if (ds.Tables[0].Rows.Count > 0)         
            //            srch.ShowDialog();
            //        txtGLcod.Text = srch.codeselected;
            //        if (srch.codeselected == null)
            //        {
            //            txtGLcod.Focus();
            //        }
            //        else
            //        {
            //            getGLInfo();
            //        }
            //        Globalvariable.searchNo = 0;
            //        if (txtGLname.Text  != "")
            //        {
            //            BtnSave.Focus();
            //            txtGLcod.BackColor = Color.White;
            //        }
            //        else
            //        {
            //            txtGLcod.Focus();
            //        }
            //    }
            //     else if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            //    {
            //        txtGLcod.Text = "";
            //        txtGLname.Text = "";
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
            try
            {
                if (DtgridDiscount.Rows.Count > 0)
                {
                    selectrow = DtgridDiscount.SelectedCells[0].RowIndex;
                }
                txtGLcod.BackColor = Color.White;

                if (txtname.Text == "")
                {
                    txtname.Focus();
                }
                else if (txtDiscount.Text == "" || Convert.ToDouble(txtDiscount.Text) >= 100)
                {
                    txtDiscount.Focus();
                }
                else if (txtGLcod.Text.Trim() == "")
                {
                    txtGLcod.Focus();
                }
                else
                {
                    if (BtnSave.Text == "SAVE")
                        str = "SELECT * FROM tbDiscountDetail WHERE disc='" + txtDiscount.Text +
                            "' AND Branchcode='" + Globalvariable.bcode + "'";
                    else if (BtnSave.Text == "UPDATE")
                        str = "SELECT * FROM tbDiscountDetail WHERE disc='" + txtDiscount.Text +
                               "' AND Branchcode='" + Globalvariable.bcode + "' AND discount_code !=" + txtDiscCode.Text + "";
                    SqlDataReader dr = mod.GetRecord(str);
                    if (dr.HasRows)
                    {
                        MessageBox.Show("Discount Already Exist!!");
                        txtDiscount.Text = "";
                        txtDiscount.Focus();
                    }
                    else
                    {
                        if (BtnSave.Text == "UPDATE")
                        {
                            dr = mod.GetSelectAllField("tbDiscountDetail", "discount_code", txtDiscCode.Text, "");
                            while (dr.Read())
                            {
                                SqlDataReader dataStr, changeData;
                                mod.compare(txtname.Text, dr["name"].ToString(), "Discount Info");
                                mod.compare(txtDiscount.Text, dr["disc"].ToString(), "Discount Info");
                                changeData = mod.GetSelectAllField("tbGeneralLedger", "genledg_code", dr["genleg_code"].ToString(), "");
                                if (changeData.Read())
                                {
                                    dataStr = mod.GetSelectAllField("tbGeneralLedger", "genledg_code", changeData["genledg_code"].ToString(), "");
                                    if (dataStr.Read())
                                    {
                                        mod.compare(dataStr["genledg_desc"].ToString(), changeData["genledg_desc"].ToString(), "Discount Info");
                                    }
                                }
                            }
                        }
                        BtnSave.Enabled = false;
                        string strInsert = ""; ;
                        int i;
                        SqlCommand cmd = new SqlCommand("SP_INSERTDiscountMast", con.connect());
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (BtnSave.Text == "SAVE")
                        {
                            txtDiscCode.Text = "" + mod.GetMaxNum(tableName, firstCol);
                            cmd.Parameters.AddWithValue("@variable", "INSERT");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@variable", "UPDATE");
                        }
                        cmd.Parameters.AddWithValue("@code", txtDiscCode.Text);
                        cmd.Parameters.AddWithValue("@name", txtname.Text.Replace("'", "''"));
                        cmd.Parameters.AddWithValue("@discount", mod.Isnull(txtDiscount.Text, "0"));
                        cmd.Parameters.AddWithValue("@genelegcod", mod.Isnull(txtGLcod.Text, "0"));
                        cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
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
                        mod.DataGridBind(DtgridDiscount, tableName, firstCol, secondCol, "N", "Name", "Code");
                        txtsearch.Focus();
                        txtsearch.Text = "";
                        FillDetails();
                        int s = selectrow;
                        if (DtgridDiscount.Rows.Count > 0)
                        {
                            DtgridDiscount.CurrentCell = DtgridDiscount[0, s];
                        }
                        StrUpdate = "";
                        BtnSave.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void DtgridDiscount_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DtgridDiscount_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            FillDetails();
            mod.gridDoubleClick(Chkshowdelet, BtnDelete, txtname);
            lblSucessMsg.Text = "";
            BtnSave.Enabled = true;
        }

        private void DtgridDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                mod.gridKeyDown(e, BtnDelete, txtsearch, DtgridDiscount, this);
                FillDetails();
                if (e.KeyCode == Keys.Enter && Chkshowdelet.Checked == true && DtgridDiscount.Rows.Count > 0)
                    BtnDelete.Focus();
                else if (e.KeyCode == Keys.Enter && Chkshowdelet.Checked == true && DtgridDiscount.Rows.Count == 0)
                    BtnExit.Focus();
                else if (e.KeyCode == Keys.Enter && Chkshowdelet.Checked == false && DtgridDiscount.Rows.Count == 0)
                    BtnExit.Focus();
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
                    i = mod.deleteButtonClick(tableName, firstCol, txtDiscCode.Text,
                         txtsearch, BtnDelete.Text.Substring(0, 1));
                    if (i == 1)
                    {
                        FrmDiscount_Load(sender, EventArgs.Empty);
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

        private void Chkshowdelet_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                mod.chkDeletedChange(Chkshowdelet, firstCol, secondCol, tableName, Deleted, "N", DtgridDiscount, txtsearch, "Name", "Code");
                FillDetails();
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
            DtgridDiscount_KeyDown(sender, e);
        }

        private void txtsearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && txtsearch.Text == "")
            {
                BtnAdd.Focus();
            }
            lblSucessMsg.Text = "";
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                mod.txtSearchChange(Chkshowdelet, firstCol, secondCol, tableName, DtgridDiscount, txtsearch, "Room Category Name", "Room No.");
                FillDetails();
                lblSucessMsg.Text = "";

            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void txtname_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space || e.KeyChar == '.');
        }

        //private void txtDiscount_KeyPress_1(object sender, KeyPressEventArgs e)
        //{
        //    e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == (char)Keys.Back);
        //}

        private void txtGLcod_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void txtsearch_Leave(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.White;
        }

        private void txtname_Leave(object sender, EventArgs e)
        {
            txtname.BackColor = Color.White;
        }

        private void txtDiscount_Leave(object sender, EventArgs e)
        {
            txtDiscount.BackColor = Color.White;

        }

        private void btnLessID_Click(object sender, EventArgs e)
        {

            Models.Common.FormManagement.Instance.SelectRecordFromGrid(DtgridDiscount, Models.Common.RecordMove.Previous, FillDetails);

        }

        private void btnGreaterID_Click(object sender, EventArgs e)
        {

            Models.Common.FormManagement.Instance.SelectRecordFromGrid(DtgridDiscount, Models.Common.RecordMove.Next, FillDetails);

        }

        private void txtGLcod_Leave(object sender, EventArgs e)
        {
            txtGLcod.BackColor = Color.White;
        }

        private void FrmDiscount_KeyDown(object sender, KeyEventArgs e)
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

        private void DtgridDiscount_CellClick_1(object sender, DataGridViewCellEventArgs e)
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

        private void FrmDiscount_Shown(object sender, EventArgs e)
        {
            txtsearch.Focus();
        }

    }
}

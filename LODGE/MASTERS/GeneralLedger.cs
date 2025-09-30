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
    public partial class GeneralLedger : Form
    {
        Design ObjDesign = new Design();
        public GeneralLedger()
        {
            InitializeComponent();
        }
        Module mod = new Module();
        Connection con = new Connection();
        public static SqlDataReader srchdr;
        public static SqlDataAdapter srchda;

        string firstCol = "genledg_code";
        string secondCol = "genledg_desc";
        string tableName = "tbGeneralLedger";
        string Deleted = "deleted";
        int under_gc, selectrow;
        string strsrch, chksub, strOpCD, strPreCD, str, fillOpCr, fillPreyrCr, StrUpdate;
        public static int searchNo;
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
        private void GeneralLedger_Load(object sender, EventArgs e)
        {
            LoadEvent();
        }

        public string searchChange(string searchStr)
        {
            try
            {
                if (Globalvariable.searchNo == 1)
                {
                    strsrch = "SELECT description,grp_code FROM tbGroup WHERE deleted='N' OR description LIKE '" +
                           searchStr + "%' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY grp_code";

                }
                else if (Globalvariable.searchNo == 2)
                {
                    strsrch = "SELECT description,grp_code FROM tbGroup WHERE deleted='N' AND description LIKE '" +
                           searchStr + "%' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY grp_code";
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
            return strsrch;
        }


        public void FillDetails()
        {
            try
            {
                mod.txtclear(this.Controls);
                if (ChkDeleted.Checked == true)
                {
                    mod.chkDeletedTrue(this, BtnAdd, BtnSave, BtnDelete, DtgridGenralLedger, txtsearch);
                    txtsearch.Focus();
                }
                else
                {
                    mod.chkDeletedFalse("General Ledger", this, BtnAdd, BtnSave, BtnDelete, DtgridGenralLedger, txtsearch);
                }
                if (DtgridGenralLedger.Rows.Count > 0)
                {
                    int i;
                    if (StrUpdate == "U")
                    {
                        i = selectrow;
                    }
                    else
                    {
                        i = DtgridGenralLedger.SelectedCells[0].RowIndex;
                    }
                    if (DtgridGenralLedger.Rows[i].Cells[1].Value != null)
                    {
                        string ID = mod.Isnull(DtgridGenralLedger.Rows[i].Cells[1].Value.ToString(), "");
                        SqlDataReader dr = mod.GetSelectAllField(tableName, firstCol, ID, "");
                        //SqlDataReader dr = mod.GetRecord("SELECT * FROM " + tableName + " WHERE " +
                        //                     firstCol + " ='" + s + "' AND Branchcode='" + Globalvariable.bcode + "'");
                        if (dr.Read())
                        {
                            txtcode.Text = dr["genledg_code"].ToString();
                            txtcode.Enabled = false;
                            txtName.Text = dr["genledg_desc"].ToString();
                            txtgroup1.Text = dr["grp_code"].ToString();
                            cmbSubLedYN.SelectedValue = dr["sub_led"].ToString();
                            getGroupInfo();
                            //if (dr["sub_led"].ToString() == "Y")
                            //    chkSubLedYN.Checked = true;
                            //else
                            //    chkSubLedYN.Checked = false;
                        }
                        string str = "SELECT * FROM tbAcctOpeClosBal WHERE cust_type='O' AND gl_code='" + txtcode.Text +
                                            "' AND Branchcode='" + Globalvariable.bcode + "'";
                        dr = mod.GetRecord(str);
                        if (dr.HasRows)
                        {
                            dr.Read();
                            txtopenbal.Text = dr["open_bal"].ToString();
                            fillOpCr = Convert.ToString(dr["open_crdr"]);
                            fillPreyrCr = Convert.ToString(dr["pre_year_crdr"]);
                            combOpenBal.SelectedValue = fillOpCr;
                            combPreyarbal.SelectedValue = fillPreyrCr;
                            //if (fillOpCr != "")
                            //{
                            //    if (fillOpCr == "C")
                            //        combOpenBal.Items.Insert(0, "Credit");
                            //    else if (fillOpCr == "D")
                            //        combOpenBal.Items.Insert(0, "Debit");
                            //}
                            //combOpenBal.SelectedIndex = 0;
                            //fillindexcomOpenBacrdr();
                            //fillPreyrCr = Convert.ToString(dr["pre_year_crdr"]);
                            //if (fillPreyrCr != "")
                            //{
                            //    if (fillPreyrCr == "C")
                            //        combPreyarbal.Items.Insert(0, "Credit");

                            //    else if (fillPreyrCr == "D")
                            //        combPreyarbal.Items.Insert(0, "Debit");
                            //}
                            //combPreyarbal.SelectedIndex = 0;
                            //fillindexPrecrdr();
                        }
                    }
                    mod.UserAccessibillityMaster("General Ledger", BtnAdd, BtnSave, BtnDelete);
                    if (BtnDelete.Enabled == true)
                    {
                        string strUSE = "SELECT * FROM tbGeneralLedger WHERE deleted='N' AND genledg_code = '1' AND genledg_code IN (SELECT genledg_code FROM tbBook WHERE deleted='N' UNION SELECT gl_code from tbTaxMaster UNION SELECT genledg_code from tbDiscountDetail)";
                        mod.CheckUse(strUSE, BtnDelete);
                    }
                }
                else
                {
                    mod.txtclear(this.Controls);
                    //OpenBalCRDR();
                    //PreYearCRDR();
                    mod.UserAccessibillityMaster("General Ledger", BtnAdd, BtnSave, BtnDelete);
                    BtnDelete.Text = "RESET";
                }
                //   lblSucessMsg.Text = "";
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void getGroupInfo()
        {
            try
            {
                if (txtgroup1.Text.Trim().Equals(""))
                {
                    lblGroupName.Text = "";
                    return;
                }
                string str = "SELECT * FROM tbGroup WHERE Branchcode='" + Globalvariable.bcode + "' AND grp_code=" +
                   txtgroup1.Text + "";
                SqlDataReader dr1 = mod.GetRecord(str);
                if (dr1.HasRows)
                {
                    dr1.Read();
                    lblGroupName.Text = dr1["description"].ToString();
                    //txtgroupname.Enabled = false;
                    under_gc = Convert.ToInt32(dr1["under_code"]);
                    txtgroup1.Text = dr1["grp_code"].ToString();
                    if (under_gc == 1 || under_gc == 2)
                    {
                        txtopenbal.Enabled = false;
                        combOpenBal.Enabled = false;
                        combPreyarbal.Enabled = true;
                        txtperyearbal.Enabled = true;
                        txtopenbal.Text = "";
                        //combOpenBal.Items.Insert(0, "Credit");
                        //txtPreYearBal.Focus();
                    }
                    else
                    {
                        txtopenbal.Enabled = true;
                        txtperyearbal.Enabled = false;
                        combPreyarbal.Enabled = false;
                        combOpenBal.Enabled = true;
                        txtperyearbal.Text = "";
                        //  rndPreOpBalC.Checked = true;
                    }
                }
                else
                {
                    txtgroup1.Text = "";
                    lblGroupName.Text = "";
                    //  txtgroup1.Focus();
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }


        public void LoadEvent()
        {
            try
            {
                ObjDesign.FormDesign(this, DtgridGenralLedger);
                //mod.FormLoad(this, firstCol, secondCol, tableName, DtgridGenralLedger, txtsearch, ChkDeleted);
                mod.FormLoad(this, tableName, firstCol, secondCol, Deleted, "N", "N", DtgridGenralLedger, txtsearch, ChkDeleted, "Description", "Code");
                //chkSubLedYN.Enabled = true;
                Models.Common.FormManagement.Instance.FillCombo(ref cmbSubLedYN, Models.Common.ComboType.YesNo, "N");
                Models.Common.FormManagement.Instance.FillCombo(ref combOpenBal, "DebitCredit", "D");
                Models.Common.FormManagement.Instance.FillCombo(ref combPreyarbal, "DebitCredit", "D");
                txtcode.Enabled = false;
                FillDetails();
                Globalvariable.SearchChangeVariable = "SrchByName";
                //mod.UserAccessibillityMaster("General Ledger", BtnAdd, BtnSave, BtnDelete);
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        //public void OpenBalCRDR()
        //{
        //    combOpenBal.Items.Clear();
        //    combOpenBal.Items.Insert(0, "SELECT");
        //    combOpenBal.Items.Insert(1, "Credit");
        //    combOpenBal.Items.Insert(2, "Debit");
        //    combOpenBal.SelectedIndex = 0;

        //}
        //public void fillindexcomOpenBacrdr()
        //{
        //    combOpenBal.Items.Clear();
        //    combOpenBal.Items.Insert(0, "SELECT");
        //    combOpenBal.Items.Insert(1, "Credit");
        //    combOpenBal.Items.Insert(2, "Debit");
        //}


        //public void PreYearCRDR()
        //{
        //    combPreyarbal.Items.Clear();
        //    combPreyarbal.Items.Insert(0, "SELECT");
        //    combPreyarbal.Items.Insert(1, "Credit");
        //    combPreyarbal.Items.Insert(2, "Debit");
        //    combPreyarbal.SelectedIndex = 0;

        //}
        //public void fillindexPrecrdr()
        //{
        //    combPreyarbal.Items.Clear();
        //    combPreyarbal.Items.Insert(0, "SELECT");
        //    combPreyarbal.Items.Insert(1, "Credit");
        //    combPreyarbal.Items.Insert(2, "Debit");
        //}
        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void txtName_Enter(object sender, EventArgs e)
        {
            txtName.BackColor = Color.Yellow;
        }



        private void txtopenbal_Enter(object sender, EventArgs e)
        {
            txtopenbal.BackColor = Color.Yellow;
        }

        private void txtperyearbal_Enter(object sender, EventArgs e)
        {
            txtperyearbal.BackColor = Color.Yellow;
        }

        private void txtclosebal_Enter(object sender, EventArgs e)
        {
            txtclosebal.BackColor = Color.Yellow;
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtgroup1.Focus();
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            ADD();
        }

        public void max_value()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT (ISNULL(MAX(genledg_code),0)+1) FROM tbGeneralLedger WHERE Branchcode='" + Globalvariable.bcode + "'", con.connect());
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


        public void ADD()
        {
            try
            {
                mod.addBtnClick(this, txtcode, tableName, firstCol, BtnSave, BtnDelete, txtName);
                mod.UserAccessibillityMaster("General Ledger", BtnAdd, BtnSave, BtnDelete);
                // max_value();
                txtcode.Text = "" + mod.GetMaxNum(tableName, firstCol);
                txtclbalcrdr.Text = "C";
                //chkSubLedYN.Enabled = true;
                //chkSubLedYN.Checked = false;

                //cmbSubLedYN.Enabled = false;
                cmbSubLedYN.SelectedValue = "Y";
                //OpenBalCRDR();
                //PreYearCRDR();
                lblSucessMsg.Text = "";
                label1.Text = "Search by Name :";
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void txtgroup1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void GeneralLedger_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Control nextTab;
                if (e.KeyChar == (char)Keys.Enter)
                {
                    nextTab = ((Control)sender);
                    if (ActiveControl.Name == "txtsearch" && DtgridGenralLedger.Rows.Count == 0 && e.KeyChar == (char)Keys.Enter)
                    {
                        txtsearch.Focus();
                    }
                    else
                    {
                        if (ActiveControl.Name == "txtsearch" && DtgridGenralLedger.Rows.Count == 0)
                        {
                            nextTab = BtnAdd;
                        }
                        else if (ActiveControl.Name == "txtgroup1" && txtgroup1.Text == "")
                        {
                            nextTab = txtgroup1;
                        }
                        else if (ActiveControl.Name == "txtgroup1")
                        {
                            if (lblGroupName.Text == "")
                            {
                                txtgroup1.Focus();
                            }
                            else
                            {
                                nextTab = GetNextControl(ActiveControl, true);
                            }
                        }
                        else if (ActiveControl.Name == "cmbSubLedYN" && txtopenbal.Enabled == true)
                            nextTab = txtopenbal;
                        else if (ActiveControl.Name == "cmbSubLedYN" && txtperyearbal.Enabled == true)
                            nextTab = txtperyearbal;
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

        //private void chkSubLedYN_CheckedChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (chkSubLedYN.Checked == true)
        //        {
        //            chksub = "Y";
        //            txtopenbal.Enabled = false;
        //            combOpenBal.Enabled = false;
        //        }
        //        else
        //        {
        //            chksub = "N";
        //            if (under_gc == 3 || under_gc == 4)
        //            {
        //                txtopenbal.Enabled = true;
        //                combOpenBal.Enabled = true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string str = "Message:" + ex.Message;
        //        MessageBox.Show(str, "Error Message");
        //    }
        //}

        private void txtopenbal_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == '.');
                // mod.rateValidation(sender, e, txtopenbal, 5, 2);
                if (e.KeyChar == (char)Keys.Enter)
                {
                    if (txtopenbal.Text == "" || txtopenbal.Text == "0")
                    {
                        combOpenBal.Enabled = false;
                        //combPreyarbal.Enabled = true;
                        combPreyarbal.Focus();
                    }
                    else
                    {
                        combOpenBal.Enabled = true;
                        combOpenBal.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void txtperyearbal_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == '.');
                //  mod.rateValidation(sender, e, txtperyearbal, 5, 2);
                if (e.KeyChar == (char)Keys.Enter)
                {
                    if (txtperyearbal.Text == "" || txtperyearbal.Text == "0")
                        combPreyarbal.Enabled = false;
                    else
                        combPreyarbal.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void txtclosebal_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == '.');
            //mod.rateValidation(sender, e, txtclosebal, 5, 2);
        }

        private void txtgroup1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            try
            {
                string subled;
                SqlDataReader dr1;
                if (DtgridGenralLedger.Rows.Count > 0)
                {
                    selectrow = DtgridGenralLedger.SelectedCells[0].RowIndex;
                }
                int i;
                if (txtName.Text == "")
                    txtName.Focus();
                else
                {
                    //  string str;
                    if (BtnSave.Text == "SAVE")
                    {
                        str = "SELECT * FROM " + tableName + " WHERE " + secondCol + "='" + txtName.Text.Replace("'", "''") +
                            "' AND Branchcode='" + Globalvariable.bcode + "'";
                    }
                    else if (BtnSave.Text == "UPDATE")
                    {
                        str = "SELECT * FROM " + tableName + " WHERE " + secondCol + "='" + txtName.Text.Replace("'", "''") +
                            "' AND Branchcode='" + Globalvariable.bcode + "' AND " + firstCol + " !=" + txtcode.Text + "";
                    }
                    SqlDataReader dr = mod.GetRecord(str);
                    if (dr.HasRows)
                    {
                        MessageBox.Show("Name Already Exist!!");
                        txtName.Focus();
                    }
                    else
                    {
                        subled = cmbSubLedYN.SelectedValue.ToString();
                        //if (chkSubLedYN.Checked == true)
                        //    subled = "Y";
                        //else
                        //    subled = "N";
                        if (txtgroup1.Text != string.Empty)
                        {
                            str = "SELECT description FROM tbGroup WHERE grp_code='" + txtgroup1.Text + "' AND deleted='N'";
                            SqlDataReader dr2 = mod.GetRecord(str);
                            if (!dr2.HasRows)
                            {
                                txtgroup1.Text = "";
                                lblGroupName.Text = "";
                                txtgroup1.Focus();
                            }
                        }

                        //str = "INSERT INTO tbGeneralLedger(genledg_code,genledg_desc,grp_code,sub_led,deleted,Branchcode,user_id) VALUES(" + txtcode.Text +
                        //         ",'" + txtName.Text.Replace("'", "''") + "','" + txtgroup1.Text + "','" + subled + "','N','" + Globalvariable.bcode + "','" + Globalvariable.usercd + "' )";
                        // i = mod.SaveMaster(str);
                        SqlCommand cmd = new SqlCommand("SP_INSERTGeneralLeg", con.connect());
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (BtnSave.Text == "SAVE")
                        {
                            txtcode.Text = "" + mod.GetMaxNum(tableName, firstCol);
                            cmd.Parameters.AddWithValue("@variable", "INSERT");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@variable", "UPDATE");
                        }
                        cmd.Parameters.AddWithValue("@genlegcode", txtcode.Text);
                        cmd.Parameters.AddWithValue("@genelegname", txtName.Text.Replace("'", "''"));
                        cmd.Parameters.AddWithValue("@grpcode", txtgroup1.Text);
                        cmd.Parameters.AddWithValue("@subled", subled);
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
                        str = "SELECT * from tbAcctOpeClosBal WHERE cust_type='O' and gl_code='" + txtcode.Text +
                                "' AND  Branchcode='" + Globalvariable.bcode + "'";
                        dr1 = mod.GetRecord(str);
                        strOpCD = combOpenBal.SelectedValue.ToString();
                        strPreCD = combPreyarbal.SelectedValue.ToString();
                        //if (combOpenBal.SelectedItem == "Credit")
                        //    strOpCD = "C";
                        //else
                        //    strOpCD = "D";
                        //if (combPreyarbal.SelectedItem == "Credit")
                        //    strPreCD = "C";
                        //else
                        //    strPreCD = "D";

                        //str = "INSERT INTO tbAcctOpeClosBal(cust_type,gl_code,open_bal,open_crdr,pre_year_bal,pre_year_crdr,Branchcode,company_srno) VALUES('O'," + txtcode.Text +
                        //     "," + mod.Isnull(txtopenbal.Text, "0") + ",'" + strOpCD + "'," + mod.Isnull(txtperyearbal.Text, "0") + ",'" + strPreCD +
                        //     "','" + Globalvariable.bcode + "','"+ Globalvariable.company_Srno +"' )";
                        //i = mod.SaveMaster(str);
                        SqlCommand cmdAcct = new SqlCommand("SP_INSERTAcctOpenCloBal", con.connect());
                        cmdAcct.CommandType = CommandType.StoredProcedure;
                        if (!dr1.HasRows)
                        {
                            cmdAcct.Parameters.AddWithValue("@variable", "INSERT");
                        }
                        else
                        {
                            cmdAcct.Parameters.AddWithValue("@variable", "UPDATE");
                        }
                        cmdAcct.Parameters.AddWithValue("@glcode", txtcode.Text);
                        cmdAcct.Parameters.AddWithValue("@openingBal", mod.Isnull(txtopenbal.Text, "0"));
                        cmdAcct.Parameters.AddWithValue("@opencrdr", strOpCD);
                        cmdAcct.Parameters.AddWithValue("@preyearbal", mod.Isnull(txtperyearbal.Text, "0"));
                        cmdAcct.Parameters.AddWithValue("@preyearcrdr", strPreCD);
                        cmdAcct.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                        cmdAcct.Parameters.AddWithValue("@companySrNo", Globalvariable.usercd);
                        cmdAcct.ExecuteNonQuery();
                    }

                    if (BtnSave.Text != "SAVE")
                    {
                        str = "UPDATE tbBook SET book_description='" + txtName.Text.Replace("'", "''") + "' WHERE gen_leg_code= '" + txtcode.Text + "'";
                        i = mod.SaveMaster(str);
                    }
                    //str = "SELECT " + secondCol + "," + firstCol + " FROM " + tableName + " WHERE deleted='N'  AND Branchcode='" + Globalvariable.bcode + "' ORDER BY " + firstCol + "";
                    //mod.fillgrid(str, DtgridGenralLedger);
                    mod.DataGridBind(DtgridGenralLedger, tableName, firstCol, secondCol, "N", "Ledger Desc", "Code");
                    txtsearch.Focus();
                    txtsearch.Text = "";
                    string txt = BtnSave.Text;
                    FillDetails();
                    int s = selectrow;
                    if (DtgridGenralLedger.Rows.Count > 0)
                    {
                        DtgridGenralLedger.CurrentCell = DtgridGenralLedger[0, s];
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

        private void DtgridGenralLedger_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            FillDetails();
            mod.gridDoubleClick(ChkDeleted, BtnDelete, txtName);
            lblSucessMsg.Text = "";
            BtnSave.Enabled = true;

        }

        private void DtgridGenralLedger_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                mod.gridKeyDown(e, BtnDelete, txtsearch, DtgridGenralLedger, this);
                FillDetails();
                if (e.KeyCode == Keys.Enter && ChkDeleted.Checked == true && DtgridGenralLedger.Rows.Count > 0)
                    BtnDelete.Focus();
                else if (e.KeyCode == Keys.Enter && ChkDeleted.Checked == true && DtgridGenralLedger.Rows.Count == 0)
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
                    i = mod.deleteButtonClick(tableName, firstCol, txtcode.Text,
                         txtsearch, BtnDelete.Text.Substring(0, 1));
                    if (i == 1)
                    {
                        GeneralLedger_Load(sender, EventArgs.Empty);
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


        private void txtgroup1_Enter_1(object sender, EventArgs e)
        {

        }

        private void txtgroup1_KeyDown_1(object sender, KeyEventArgs e)
        {

            var returnForm = Models.Common.FormManagement.Instance.SearchFormOnKeyDown(ref txtgroup1, e, "GeneralLedger", true);
            if (returnForm == null)
            {
                return;
            }
            txtgroup1.Text = returnForm.codeselected;
            lblGroupName.Text = returnForm.descselected;
            getGroupInfo();
            return;
            //try
            //{
            //    DataSet ds = new DataSet();
            //    if (txtgroup1.Text == "" && e.KeyCode == Keys.Enter)
            //    {
            //        Globalvariable.searchNo = 1;
            //        KeysConverter kc = new KeysConverter();
            //        string key = kc.ConvertToString(e.KeyCode);
            //        FrmSearch srch = new FrmSearch();
            //        // string str = "SELECT description,grp_code FROM tbGroup WHERE deleted='N' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY grp_code";
            //        srchda = mod.GetselectQuery("tbGroup", "grp_code", "description", "N", "N");
            //        srch.val = key;
            //        srch.fillbackgridSrch(srchda, "tbGroup");
            //        ds.Clear();
            //        srchda.Fill(ds);
            //        if (ds.Tables[0].Rows.Count > 0)
            //            //if (srchdr.HasRows)
            //            srch.ShowDialog();
            //        txtgroup1.Text = srch.codeselected;
            //        if (srch.codeselected == null)
            //        {
            //            txtgroup1.Focus();
            //        }
            //        else
            //        {
            //            getGroupInfo();
            //        }
            //        Globalvariable.searchNo = 0;
            //        if (lblGroupName.Text != "")
            //        {
            //            cmbSubLedYN.Focus();
            //        }
            //        else
            //        {
            //            txtgroup1.Focus();
            //        }
            //    }
            //    else if (e.KeyCode == Keys.Enter && txtgroup1.Text != "")
            //    {
            //        getGroupInfo();

            //    }
            //    else if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            //    {
            //        txtgroup1.Text = "";
            //        lblGroupName.Text = "";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    string str = "Message:" + ex.Message;
            //    MessageBox.Show(str, "Error Message");
            //}
        }

        private void chkSubLedYN_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*   if (e.KeyChar == (char)Keys.Enter)
               {
                   txtgroup1.Focus();
               }*/
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtsearch_KeyDown(object sender, KeyEventArgs e)
        {
            DtgridGenralLedger_KeyDown(sender, e);
        }

        private void txtsearch_Enter_1(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.Yellow;
        }

        private void txtsearch_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                mod.txtSearchChange(ChkDeleted, firstCol, secondCol, tableName, DtgridGenralLedger, txtsearch, "Ledger Desc", "Code");
                FillDetails();
                lblSucessMsg.Text = "";

            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void txtsearch_KeyDown_1(object sender, KeyEventArgs e)
        {
            DtgridGenralLedger_KeyDown(sender, e);
        }

        private void txtgroup1_Enter(object sender, EventArgs e)
        {
            txtgroup1.BackColor = Color.Yellow;
        }

        private void ChkDeleted_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //mod.chkDeletedChange(ChkDeleted, firstCol, secondCol, tableName, DtgridGenralLedger, txtsearch);
                mod.chkDeletedChange(ChkDeleted, firstCol, secondCol, tableName, Deleted, "N", DtgridGenralLedger, txtsearch, "Ledger Desc", "Code");
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
            if (e.KeyChar == (char)Keys.Enter && txtsearch.Text == "")
            {
                BtnAdd.Focus();
            }
            lblSucessMsg.Text = "";

        }

        //private void chkSubLedYN_Enter(object sender, EventArgs e)
        //{
        //    chkSubLedYN.BackColor = Color.FromArgb(60, Color.Black);
        //}

        private void txtgroup1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void combPreyarbal_Enter(object sender, EventArgs e)
        {
            combPreyarbal.BackColor = Color.Yellow;
        }

        private void combOpenBal_Enter(object sender, EventArgs e)
        {
            combOpenBal.BackColor = Color.Yellow;
        }

        private void txtsearch_Leave(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.White;
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            txtName.BackColor = Color.White;
        }

        private void txtgroup1_Leave(object sender, EventArgs e)
        {
            txtgroup1.BackColor = Color.White;
        }

        private void cmbSubLedYN_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbSubLedYN.SelectedValue.ToString().ToUpper().Equals("Y"))
                {
                    chksub = "Y";
                    txtopenbal.Enabled = false;
                    combOpenBal.Enabled = false;
                }
                else
                {
                    chksub = "N";
                    if (under_gc == 3 || under_gc == 4)
                    {
                        txtopenbal.Enabled = true;
                        combOpenBal.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }

        }

        private void btnLessID_Click(object sender, EventArgs e)
        {

            Models.Common.FormManagement.Instance.SelectRecordFromGrid(DtgridGenralLedger, Models.Common.RecordMove.Previous, FillDetails);

        }

        private void btnGreaterID_Click(object sender, EventArgs e)
        {

            Models.Common.FormManagement.Instance.SelectRecordFromGrid(DtgridGenralLedger, Models.Common.RecordMove.Next, FillDetails);

        }

        private void txtopenbal_Leave(object sender, EventArgs e)
        {
            txtopenbal.BackColor = Color.White;
        }

        private void combOpenBal_Leave(object sender, EventArgs e)
        {
            combOpenBal.BackColor = Color.White;
        }

        private void txtperyearbal_Leave(object sender, EventArgs e)
        {
            txtperyearbal.BackColor = Color.White;
        }

        private void combPreyarbal_Leave(object sender, EventArgs e)
        {
            combPreyarbal.BackColor = Color.White;
        }

        private void txtclosebal_Leave(object sender, EventArgs e)
        {
            txtclosebal.BackColor = Color.White;
        }

        private void GeneralLedger_KeyDown(object sender, KeyEventArgs e)
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

        private void DtgridGenralLedger_CellClick(object sender, DataGridViewCellEventArgs e)
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

        private void GeneralLedger_Shown(object sender, EventArgs e)
        {
            txtsearch.Focus();
        }


    }
}

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
    public partial class FrmItemMaster : Form
    {
        Design ObjDesign = new Design();
        public string ItemGrpid, ItemGrpnm;
        public Color gridBackColor;
        public FrmItemMaster()
        {
            InitializeComponent();
        }
        public static SqlDataReader srchdr;
        Connection con = new Connection();
        SqlDataAdapter srchda;
        DataSet ds = new DataSet();
        DataGridViewTextBoxEditingControl tb;
        Module mod = new Module();
        //  Connection con = new Connection();
        //  SqlConnection connection = con.connect();
        SqlCommand command = new SqlCommand();
        string firstCol = "Item_Code";
        string secondCol = "Item_name";
        string tableName = "tbMenuGroup";
        string Deleted = "Deleted";
        public Boolean plusKeyPress;
        string filpayble, filcatgry, filserviTaxApp, filActiv, code = "", str, RestSaleMode, StrUpdate;
        int detail_id = 0, rowNo, colNo, dupRowNo, flag, selectrow;
        public string DupCode1, strsrch;
        public static int searchNo;
        bool chkDupFlag;
        SqlDataAdapter dr;
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
        private void FrmItemMaster_Load(object sender, EventArgs e)
        {
            load();
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


        public void load()
        {
            ObjDesign.FormDesign(this, DtgridItemcard);
            ObjDesign.FormDesign(this, dtgridtax);
            gridBackColor = dtgridtax.BackgroundColor;
            dtgridtax.Enabled = false;
            dtgridtax.BackgroundColor = Color.DarkGray;
            flag = 1;
            mod.FormLoad(this, tableName, firstCol, secondCol, Deleted, "N", "N", DtgridItemcard, txtsearch, Chkshowdelet, "Description", "Code");
            txtsearch.Focus();
            txtcode.Enabled = false;
            //txtTallyName.Enabled = false;

            Models.Common.FormManagement.Instance.FillCombo(ref cmbCategory, "Food_Units");
            Models.Common.FormManagement.Instance.FillCombo(ref cmbPayable, Models.Common.ComboType.YesNo);
            Models.Common.FormManagement.Instance.FillCombo(ref cmbservicetax, Models.Common.ComboType.YesNo);
            Models.Common.FormManagement.Instance.FillCombo(ref cmbActive, Models.Common.ComboType.YesNo);

            FillDetails();
            Globalvariable.SearchChangeVariable = "SrchByName";
        }

        public void maxID()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT (ISNULL(MAX(Item_Code),0)+1) FROM tbMenuGroup WHERE Branchcode='" + Globalvariable.bcode + "'", con.connect());
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
        public void add()
        {
            try
            {
                flag = 0;
                mod.addBtnClick(this, txtcode, tableName, firstCol, BtnSave, BtnDelete, txtname);
                mod.UserAccessibillityMaster("Menu Group", BtnAdd, BtnSave, BtnDelete);
                dtgridtax.Rows.Clear();
                //txtTallyName.Enabled = false;
                dtgridtax.Enabled = true;
                dtgridtax.BackgroundColor = gridBackColor;
                // maxID();
                txtcode.Text = "" + mod.GetMaxNum(tableName, firstCol);
                //FillPayable();
                //FillServiceTaxApplica();
                //FillCategory();
                //FillActive();
                label1.Text = "Search by Name :";
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        //public void FillPayable()
        //{
        //    cmbPayable.Items.Clear();
        //    cmbPayable.Items.Insert(0, "Yes");
        //    cmbPayable.Items.Insert(1, "No");
        //    // cmbPayable.SelectedIndex = 0;
        //    if (detail_id != 1)
        //    {
        //        cmbPayable.SelectedIndex = 0;
        //    }
        //}

        //public void FillActive()
        //{
        //    cmbActive.Items.Clear();
        //    cmbActive.Items.Insert(0, "Yes");
        //    cmbActive.Items.Insert(1, "No");
        //    if (detail_id != 1)
        //    {
        //        cmbActive.SelectedIndex = 0;
        //    }
        //}

        //public void FillServiceTaxApplica()
        //{
        //    cmbservicetax.Items.Clear();
        //    cmbservicetax.Items.Insert(0, "SELECT");
        //    cmbservicetax.Items.Insert(1, "Yes");
        //    cmbservicetax.Items.Insert(2, "No");
        //    if (detail_id != 1)
        //    {
        //        cmbservicetax.SelectedIndex = 0;
        //    }
        //}

        ////public void FillCategory()
        //{
        //    cmbCategory.Items.Clear();
        //    cmbCategory.Items.Insert(0, "Per Piece");
        //    cmbCategory.Items.Insert(1, "Kg/ML");
        //    if (detail_id != 1)
        //    {
        //        cmbCategory.SelectedIndex = 0;
        //    }
        //}

        public void white_textbox()
        {
            txtsearch.BackColor = Color.White;
            txtname.BackColor = Color.White;
            cmbPayable.BackColor = Color.White;
            txtTallyCode.BackColor = Color.White;
            txtItemRate.BackColor = Color.White;
            cmbCategory.BackColor = Color.White;
            cmbservicetax.BackColor = Color.White;
            cmbActive.BackColor = Color.White;
        }

        public void chkDeletedTrue()
        {
            try
            {
                mod.CntrlEnable(this.Controls);
                BtnAdd.Visible = false;
                BtnSave.Visible = false;
                BtnDelete.Text = "RECALL";
                if (DtgridItemcard.Rows.Count == 0)
                    BtnDelete.Enabled = false;
                else
                    BtnDelete.Enabled = true;
                txtsearch.Enabled = true;
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void getTallyInfo()
        {
            try
            {

                str = "SELECT Tally_Name FROM tbTallyMaster WHERE Branchcode='" + Globalvariable.bcode + "' AND Tally_Code=" +
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

        private string[] GetTaxName(string tno)
        {

            string[] rtstr = new string[2];
            // string que = "SELECT * FROM tbTaxMaster WHERE deleted='N' AND Branchcode='" + Globalvariable.bcode +
            //                    "' AND tax_code='" + tno + "'";
            SqlDataReader dr1 = mod.GetRecord("SELECT * FROM tbTaxMaster WHERE deleted='N' AND Branchcode='" + Globalvariable.bcode +
                            "' AND tax_code='" + tno + "'");
            if (dr1.HasRows)
            {
                dr1.Read();
                rtstr[0] = dr1["name"].ToString();
                rtstr[1] = dr1["gl_code"].ToString();
                return (rtstr);
            }
            else
            {
                rtstr[0] = "";
                rtstr[1] = "";
                return (rtstr);
            }

        }


        public void FillDetails()
        {
            try
            {

                dtgridtax.Enabled = true;
                dtgridtax.BackgroundColor = gridBackColor;
                if (Chkshowdelet.Checked == true)
                {
                    chkDeletedTrue();
                    dtgridtax.Enabled = false;
                    dtgridtax.BackgroundColor = Color.DarkGray;
                    txtsearch.Focus();
                }
                else
                {
                    mod.chkDeletedFalse("Menu Group", this, BtnAdd, BtnSave, BtnDelete, DtgridItemcard, txtsearch);
                    //txtTallyName.Enabled = false;
                    dtgridtax.Enabled = true;
                    dtgridtax.BackgroundColor = gridBackColor;
                }
                if (DtgridItemcard.Rows.Count > 0)
                {
                    detail_id = 1;
                    int i;
                    if (StrUpdate == "U")
                    {
                        i = selectrow;
                        StrUpdate = "";
                    }
                    else
                    {
                        i = DtgridItemcard.SelectedCells[0].RowIndex;
                    }
                    if (DtgridItemcard.Rows[i].Cells[1].Value != null)
                    {
                        string ID = mod.Isnull(DtgridItemcard.Rows[i].Cells[1].Value.ToString(), "");
                        //SqlDataReader dr = mod.GetRecord("SELECT * FROM tbMenuGroup WHERE Item_Code ='" + s +
                        //    "' AND Branchcode='" + Globalvariable.bcode + "' ");
                        {
                            var dtData = Models.Common.DbConnectivity.Instance.ManageItemMaster("SELECT", ID);
                            //SqlDataReader dr = mod.GetSelectAllField(tableName, firstCol, ID, "");
                            if (dtData.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dtData.Rows)
                                {
                                    txtcode.Text = dr["Item_Code"].ToString();
                                    txtname.Text = dr["Item_name"].ToString();
                                    cmbPayable.SelectedValue = dr["Payble"].ToString();
                                    txtTallyCode.Text = dr["Tally_Code"].ToString();
                                    lblTallyName.Text = dr["Tally_Name"].ToString();
                                    txtItemRate.Text = dr["Item_Rate"].ToString();
                                    cmbCategory.SelectedValue = dr["Category"].ToString();
                                    cmbservicetax.SelectedValue = dr["Appli_Service_Tax"].ToString();
                                    chkSaleMode.Checked = dr["Res_Sale_Mode"].ToString().Equals("Y", StringComparison.InvariantCultureIgnoreCase);
                                    cmbActive.SelectedValue = dr["Active"].ToString();

                                }
                            }
                        }

                        {
                            dtgridtax.Rows.Clear();

                            var dtData = Models.Common.DbConnectivity.Instance.ManageItemGroupTaxDetail("SELECT", ID);
                            if (dtData.Rows.Count > 0)
                            {
                                for (i = 0; i < dtData.Rows.Count; i++)
                                {
                                    var dr = dtData.Rows[i];
                                    dtgridtax.Rows.Add();
                                    dtgridtax.Rows[i].Cells[0].Value = i + 1;//dr["Item_code"].ToString();                            
                                    dtgridtax.Rows[i].Cells[1].Value = dr["Tax_Code"].ToString();
                                    dtgridtax.Rows[i].Cells[2].Value = dr["Tax_Name"].ToString();
                                    dtgridtax.Rows[i].Cells[3].Value = Convert.ToDecimal(dr["Tax_Percentage"]).ToString("0.00");
                                }
                            }
                        }
                        //while (dr.Read())
                        //{
                        //    txtcode.Text = dr["Item_Code"].ToString();
                        //    txtname.Text = dr["Item_name"].ToString();
                        //    filpayble = Convert.ToString(dr["Payble"]);

                        //    //if (filpayble != "")
                        //    //{
                        //    //    if (filpayble == "Y")
                        //    //        cmbPayable.Items.Insert(0, "Yes");
                        //    //    else if (filpayble == "N")
                        //    //        cmbPayable.Items.Insert(0, "No");
                        //    //}
                        //    //cmbPayable.SelectedIndex = 0;
                        //    //FillPayable();

                        //    txtTallyCode.Text = mod.Isnull(dr["Tally_Code"].ToString(), "0");
                        //    if (txtTallyCode.Text != "0")
                        //    {
                        //        getTallyInfo();
                        //    }
                        //    else if (txtTallyCode.Text == "0")
                        //    {
                        //        lblTallyName.Text = "";
                        //    }
                        //    txtItemRate.Text = dr["Item_Rate"].ToString();
                        //    filcatgry = Convert.ToString(dr["Category"]);
                        //    //if (filcatgry != "")
                        //    //{
                        //    //    if (filcatgry == "2")
                        //    //        cmbCategory.Items.Insert(0, "Kg/ML");
                        //    //    else if (filcatgry == "1")
                        //    //        cmbCategory.Items.Insert(0, "Per Piece");
                        //    //}
                        //    //cmbCategory.SelectedIndex = 0;
                        //    //FillCategory();
                        //    filserviTaxApp = Convert.ToString(dr["Appli_Service_Tax"]);
                        //    //if (filserviTaxApp != "")
                        //    //{
                        //    //    if (filserviTaxApp == "Y")
                        //    //        cmbservicetax.Items.Insert(0, "Yes");
                        //    //    else if (filserviTaxApp == "N")
                        //    //        cmbservicetax.Items.Insert(0, "No");
                        //    //    dtgridtax.Enabled = false;
                        //    //    dtgridtax.BackgroundColor = Color.DarkGray;
                        //    //}
                        //    //cmbservicetax.SelectedIndex = 0;
                        //    //FillServiceTaxApplica();

                        //    RestSaleMode = Convert.ToString(dr["Res_Sale_Mode"]);
                        //    //if (RestSaleMode != "")
                        //    //{
                        //    //    if (RestSaleMode == "Y")
                        //    //        chkSaleMode.Checked = true;
                        //    //    else if (RestSaleMode == "N")
                        //    //        chkSaleMode.Checked = false;
                        //    //}

                        //    filActiv = Convert.ToString(dr["Active"]);
                        //    //if (filActiv != "")
                        //    //{
                        //    //    if (filActiv == "Y")
                        //    //        cmbActive.Items.Insert(0, "Yes");
                        //    //    else if (filActiv == "N")
                        //    //        cmbActive.Items.Insert(0, "No");
                        //    //}
                        //    //cmbActive.SelectedIndex = 0;
                        //    //FillActive();
                        //}
                        //string qstr;
                        //SqlDataReader dr1 = mod.GetSelectAllField("tbItemGroupTaxDetail", "Item_Code", txtcode.Text.ToString(), "");
                        ////qstr = "SELECT * FROM tbItemGroupTaxDetail WHERE Item_Code='" + txtcode.Text + "' AND Branchcode='" + Globalvariable.bcode + "'";
                        ////dr = mod.GetRecord(qstr);
                        //dtgridtax.Rows.Clear();
                        //i = 0;
                        //if (dr1.HasRows)
                        //{
                        //    while (dr1.Read())
                        //    {
                        //        dtgridtax.Rows.Add();
                        //        dtgridtax.Rows[i].Cells[0].Value = i + 1;//dr["Item_code"].ToString();                            
                        //        dtgridtax.Rows[i].Cells[1].Value = dr1["Tax_Code"].ToString();
                        //        string[] rstr;
                        //        rstr = GetTaxName(dr1["Tax_Code"].ToString());
                        //        dtgridtax.Rows[i].Cells[2].Value = rstr[0];
                        //        //dtgridtax.Rows[i].Cells[3].Value = rstr[1];
                        //        i = i + 1;
                        //    }
                        //}
                        txtcode.Enabled = false;
                    }
                    mod.UserAccessibillityMaster("Menu Group", BtnAdd, BtnSave, BtnDelete);
                    if (BtnDelete.Enabled == true)
                    {
                        //SELECT Item_Code FROM tbMenuGroup WHERE Item_Code=4 AND Item_Code IN(select i_code from Sales_dtl where company_sr=19 union select i_code from Sales_dtl_ret where company_sr=19 union select i_code from purchase_dtl where company_sr=19 union select CODE from ITEM  union select i_code from purchase_dtl_ret where company_sr=19)
                        str = "SELECT Item_Code FROM tbMenuGroup WHERE Item_Code='" + txtcode.Text + "' AND Item_Code IN(SELECT Item_Code FROM tbSubItemMaster)";
                        mod.CheckUse(str, BtnDelete);
                    }
                }
                else
                {
                    mod.txtclear(this.Controls);
                    dtgridtax.Rows.Clear();
                    //FillPayable();
                    //FillServiceTaxApplica();
                    //FillCategory();
                    //FillActive();
                    BtnDelete.Text = "RESET";

                }
                detail_id = 0;
                //  lblSucessMsg.Text = "";
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        public void FillTaxGrid()
        {
            //int i;
            //string qstr;
            //qstr = "SELECT * FROM tbItemGroupTaxDetail WHERE Item_Code='" + txtcode.Text + "' AND Branchcode='" + Globalvariable.bcode + "'";
            //SqlDataReader dr = mod.GetRecord(qstr);
            var dtData = Models.Common.DbConnectivity.Instance.ManageItemGroupTaxDetail("SELECT", txtcode.Text);
            dtgridtax.Rows.Clear();
            //i = 0;
            if (dtData.Rows.Count > 0)
            {
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    var dr = dtData.Rows[i];
                    dtgridtax.Rows.Add();
                    dtgridtax.Rows[i].Cells[0].Value = i + 1;//dr["Item_code"].ToString();                            
                    dtgridtax.Rows[i].Cells[1].Value = dr["Tax_Code"].ToString();
                    dtgridtax.Rows[i].Cells[2].Value = dr["Tax_Name"].ToString();
                    dtgridtax.Rows[i].Cells[3].Value = dr["Tax_Percentage"].ToString();
                }

            }
            //if (dr.HasRows)
            //{
            //    while (dr.Read())
            //    {
            //        dtgridtax.Rows.Add();
            //        dtgridtax.Rows[i].Cells[0].Value = i + 1;//dr["Item_code"].ToString();                            
            //        dtgridtax.Rows[i].Cells[1].Value = dr["Tax_Code"].ToString();
            //        string[] rstr;
            //        rstr = GetTaxName(dr["Tax_Code"].ToString());
            //        dtgridtax.Rows[i].Cells[2].Value = rstr[0];
            //        //dtgridtax.Rows[i].Cells[3].Value = rstr[1];
            //        i = i + 1;
            //    }
            //}
        }
        private void txtsearch_Enter(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.Yellow;
        }

        private void txtname_Enter(object sender, EventArgs e)
        {
            txtname.BackColor = Color.Yellow;
        }

        private void cmbPayable_Enter(object sender, EventArgs e)
        {
            cmbPayable.BackColor = Color.Yellow;
        }

        private void txtTallyCode_Enter(object sender, EventArgs e)
        {
            txtTallyCode.BackColor = Color.Yellow;
        }

        private void txtItemRate_Enter(object sender, EventArgs e)
        {
            txtItemRate.BackColor = Color.Yellow;
        }

        private void cmbCategory_Enter(object sender, EventArgs e)
        {
            cmbCategory.BackColor = Color.Yellow;
        }

        private void cmbservicetax_Enter(object sender, EventArgs e)
        {
            cmbservicetax.BackColor = Color.Yellow;
        }

        private void cmbActive_Enter(object sender, EventArgs e)
        {
            cmbActive.BackColor = Color.Yellow;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                add();
                chkSaleMode.Checked = false;
                lblSucessMsg.Text = "";
                label1.Text = "Search by Name :";
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
                //   mod.chkDeletedChange(Chkshowdelet, firstCol, secondCol, tableName, DtgridItemcard, txtsearch);
                mod.chkDeletedChange(Chkshowdelet, firstCol, secondCol, tableName, Deleted, "N", DtgridItemcard, txtsearch, "Name", "Item No.");
                //if (Chkshowdelet.Checked == true)
                //{
                //    str = "SELECT " + secondCol + "," + firstCol + " FROM " + tableName + " WHERE deleted='Y' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY " + firstCol + "";
                //    mod.fillgrid(str, DtgridItemcard);
                //    FillPayable();
                //    FillCategory();
                //    FillServiceTaxApplica();
                //    chkSaleMode.Checked = false;
                //    FillActive();
                //    dtgridtax.Enabled = false;
                //}
                //else
                //{
                //    str = "SELECT " + secondCol + "," + firstCol + " FROM " + tableName + " WHERE deleted='N' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY " + firstCol + "";
                //    mod.fillgrid(str, DtgridItemcard);
                //}

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

        private void DtgridItemcard_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                mod.gridKeyDown(e, BtnDelete, txtsearch, DtgridItemcard, this);
                FillDetails();
                if (e.KeyCode == Keys.Enter && Chkshowdelet.Checked == true && DtgridItemcard.Rows.Count > 0)
                    BtnDelete.Focus();
                else if (e.KeyCode == Keys.Enter && Chkshowdelet.Checked == true && DtgridItemcard.Rows.Count == 0)
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
            DtgridItemcard_KeyDown(sender, e);
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
            mod.txtSearchChange(Chkshowdelet, firstCol, secondCol, tableName, DtgridItemcard, txtsearch, "Item Group Name", "Item Group No.");
            FillDetails();
            lblSucessMsg.Text = "";
        }

        private void DtgridItemcard_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                FillDetails();
                mod.gridDoubleClick(Chkshowdelet, BtnDelete, txtname);
                flag = 1;
                lblSucessMsg.Text = "";
                BtnSave.Enabled = true;

            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void dtgridtax_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
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
            try
            {
                TextBox obj = sender as TextBox;
                obj.BackColor = Color.Yellow;
                obj.Focus();
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
                if (dtgridtax.CurrentCell != null && dtgridtax.CurrentCell.ColumnIndex == 3)
                {
                    if (e.KeyChar.ToString() == ".")
                    {
                        if (obj.Text.Trim().Length == 0 || obj.Text.Contains("."))
                        {
                            e.Handled = true;
                            return;
                        }



                    }
                    else if (!Char.IsNumber(e.KeyChar) && (Keys)e.KeyChar != Keys.Back)  // check if backspace is pressed
                    {
                        e.Handled = true;
                    }

                    if ((Char.IsNumber(e.KeyChar) || e.KeyChar.ToString() == ".") && Convert.ToDecimal(obj.Text + e.KeyChar.ToString()) >= 100)
                    {
                        e.Handled = true;
                    }

                    return;
                }

                if (!Char.IsNumber(e.KeyChar)
                      && (Keys)e.KeyChar != Keys.Back)  // check if backspace is pressed
                {
                    e.Handled = true;
                }
                else if (obj.Text.ToString().Length > 3)
                    e.Handled = true;
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        public string DuplicateRecord(DataGridView dtgv, int colNo)
        {
            string DupCode = "0";
            for (int cnt = 0; cnt < dtgv.Rows.Count; cnt++)
            {
                if (dtgv.Rows[cnt].Cells[colNo].Value != null)
                {
                    DupCode = DupCode + "," + mod.Isnull(dtgv.Rows[cnt].Cells[colNo].Value.ToString(), "0");
                }
            }
            return DupCode;
        }

        private void clearCol()
        {
            try
            {
                for (int cnt = 1; cnt < dtgridtax.ColumnCount; cnt++)
                {
                    dtgridtax.Rows[dtgridtax.CurrentCell.RowIndex].Cells[cnt].Value = "";
                }
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
                cmbActive.BackColor = Color.White;
                Globalvariable.SearchString = "ItemCardTaxID";
                BtnSave.Enabled = false;
                DupCode1 = "0";
                rowNo = dtgridtax.CurrentCell.RowIndex;
                colNo = dtgridtax.CurrentCell.ColumnIndex;
                if (colNo == 1)
                {
                    if (e.KeyCode == Keys.Back)
                    {
                        dtgridtax.Rows[rowNo].Cells[1].Value = null;
                    }
                    if (e.KeyCode == Keys.Enter)
                    {
                        dtgridtax.Rows[rowNo].Cells[2].Value = "";

                        Globalvariable.searchNo = 1;
                        DupCode1 = mod.DuplicateRecord(dtgridtax, 1, rowNo);
                        Globalvariable.DuplicateValue = DupCode1;
                        Globalvariable.searchNo = 1;

                        var returnForm = Models.Common.FormManagement.Instance.SearchFormOnKeyDown(ref tb, e, "GST", true);
                        if (returnForm == null)
                        {
                            return;
                        }

                        if (returnForm.codeselected == null)
                        {
                            return;
                        }

                        dtgridtax.ClearSelection();
                        dtgridtax.CurrentCell = dtgridtax[2, rowNo];
                        dtgridtax.Rows[rowNo].Cells[1].Value = mod.Isnull(returnForm.codeselected, "");
                        dtgridtax.Rows[rowNo].Cells[2].Value = mod.Isnull(returnForm.descselected, "");

                        dtgridtax.CurrentCell = dtgridtax[3, rowNo];
                        return;
                    }
                    //else if (e.KeyCode == Keys.Enter && dtgridtax.Rows[rowNo].Cells[1].Value == null)
                    //{
                    //    //  DupCode1 = "0";
                    //    DupCode1 = DuplicateRecord(dtgridtax, 1);
                    //    Globalvariable.DuplicateValue = DupCode1;
                    //    dtgridtax.Rows[rowNo].Cells[1].Value = tb.Text;
                    //    if (tb.Text != string.Empty)
                    //    {
                    //        int i;
                    //        int itemn = Convert.ToInt32(dtgridtax.Rows[rowNo].Cells[1].Value);
                    //        chkDupFlag = false;
                    //        i = mod.getDuplicateRowNo(itemn, dtgridtax, 1, rowNo);
                    //        if (i == -1)
                    //        {
                    //            code = dtgridtax.Rows[rowNo].Cells[1].Value.ToString();
                    //            str = "SELECT * FROM tbTaxMaster WHERE tax_code='" + code + "' AND tax_code not IN(" + DupCode1 + ") AND deleted='N' AND Branchcode='" + Globalvariable.bcode + "'";
                    //            SqlDataReader dr1 = mod.GetRecord(str);
                    //            if (dr1.Read())
                    //            {
                    //                tb.Text = "";
                    //                dtgridtax.ClearSelection();
                    //                dtgridtax.CurrentCell = dtgridtax[1, rowNo];
                    //                dtgridtax.BeginEdit(true);
                    //                dtgridtax.Rows[rowNo].Cells[1].Value = dr1["tax_code"].ToString();
                    //                dtgridtax.Rows[rowNo].Cells[2].Value = dr1["name"].ToString();
                    //                //dtgridtax.Rows[rowNo].Cells[3].Value = dr1["gl_code"].ToString();
                    //                tb.Text = dr1["tax_code"].ToString();
                    //                //Add row in grid
                    //                dtgridtax.Enabled = true;
                    //                dtgridtax.BackgroundColor = gridBackColor;
                    //                int rowcount = dtgridtax.Rows.Count;
                    //                if (rowNo == rowcount - 1)
                    //                {
                    //                    dtgridtax.Rows.Add();
                    //                    rowNo = dtgridtax.RowCount - 1;
                    //                    dtgridtax.Rows[rowNo].Cells[0].Value = dtgridtax.Rows.Count;
                    //                    dtgridtax.CurrentCell = dtgridtax[1, rowNo];
                    //                    dtgridtax.BeginEdit(true);
                    //                }
                    //                else
                    //                {
                    //                    dtgridtax.CurrentCell = dtgridtax[1, rowNo];
                    //                    dtgridtax.BeginEdit(true);
                    //                }
                    //            }
                    //            else
                    //            {
                    //                clearCol();
                    //                tb.Text = "";
                    //                dtgridtax.ClearSelection();
                    //                dtgridtax.CurrentCell = dtgridtax[1, rowNo];
                    //                dtgridtax.BeginEdit(true);
                    //            }
                    //        }
                    //        else
                    //        {
                    //            chkDupFlag = true;
                    //            dtgridtax.Rows[rowNo].Cells[1].Value = "";
                    //            tb.Text = "";
                    //            dtgridtax.ClearSelection();
                    //            dtgridtax.CurrentCell = dtgridtax[1, rowNo];
                    //            dtgridtax.BeginEdit(true);
                    //        }
                    //    }
                    //    else if (tb.Text == string.Empty)
                    //    {
                    //        Globalvariable.searchNo = 1;
                    //        DupCode1 = mod.DuplicateRecord(dtgridtax, 1, rowNo);
                    //        Globalvariable.DuplicateValue = DupCode1;
                    //        Globalvariable.searchNo = 1;

                    //        var returnForm = Models.Common.FormManagement.Instance.SearchFormOnKeyDown(ref tb, e, "GST", true);
                    //        if (returnForm == null)
                    //        {
                    //            return;
                    //        }


                    //        dtgridtax.ClearSelection();
                    //        dtgridtax.CurrentCell = dtgridtax[3, rowNo];
                    //        dtgridtax.Rows[rowNo].Cells[1].Value = mod.Isnull(returnForm.codeselected, "");
                    //        dtgridtax.Rows[rowNo].Cells[2].Value = mod.Isnull(returnForm.descselected, "");
                    //        return;

                    //        Globalvariable.searchNo = 1;
                    //        KeysConverter kc = new KeysConverter();
                    //        string key = kc.ConvertToString(e.KeyCode);
                    //        FrmSearch srch = new FrmSearch();
                    //        //  DupCode1 = "0";
                    //        DupCode1 = DuplicateRecord(dtgridtax, 1);
                    //        Globalvariable.DuplicateValue = DupCode1;
                    //        // str = "SELECT name,tax_code FROM tbTaxMaster WHERE tax_code NOT IN(" + DupCode1 + ") AND deleted='N' AND Branchcode='" + Globalvariable.bcode + "'";
                    //        srch.val = key;
                    //        srchda = mod.SP_FillSearchGrid("tbTaxMaster", "name", "tax_code", DupCode1);
                    //        srch.val = key;
                    //        srch.fillbackgridSrch(srchda, "FrmItemMaster");
                    //        ds.Clear();
                    //        srchda.Fill(ds);
                    //        if (ds.Tables[0].Rows.Count > 0)
                    //            srch.ShowDialog();
                    //        if (srch.codeselected != null)
                    //        {
                    //            tb.Text = "";
                    //            dtgridtax.ClearSelection();
                    //            dtgridtax.CurrentCell = dtgridtax[2, rowNo];
                    //            dtgridtax.Rows[rowNo].Cells[1].Value = mod.Isnull(srch.codeselected, "");
                    //            dtgridtax.Rows[rowNo].Cells[2].Value = mod.Isnull(srch.descselected, "");
                    //            str = "select * from tbTaxMaster WHERE tax_code ='" + dtgridtax.Rows[rowNo].Cells[1].Value + "' AND deleted='N' and Branchcode='" + Globalvariable.bcode + "'";
                    //            SqlDataReader dr2 = mod.GetRecord(str);
                    //            if (dr2.HasRows)
                    //            {
                    //                dr2.Read();
                    //                dtgridtax.Rows[rowNo].Cells[3].Value = dr2["gl_code"].ToString();
                    //            }
                    //            //Add row in grid
                    //            dtgridtax.Enabled = true;
                    //            dtgridtax.BackgroundColor = gridBackColor;
                    //            int rowcount = dtgridtax.Rows.Count;
                    //            if (rowNo == rowcount - 1)
                    //            {
                    //                dtgridtax.Rows.Add();
                    //                rowNo = dtgridtax.RowCount - 1;
                    //                dtgridtax.Rows[rowNo].Cells[0].Value = dtgridtax.Rows.Count;
                    //                dtgridtax.CurrentCell = dtgridtax[1, rowNo];
                    //                dtgridtax.BeginEdit(true);
                    //            }
                    //            else
                    //            {
                    //                dtgridtax.CurrentCell = dtgridtax[1, rowNo];
                    //                dtgridtax.BeginEdit(true);
                    //            }
                    //        }
                    //        else
                    //        {
                    //            dtgridtax.CurrentCell = dtgridtax[1, rowNo];
                    //            dtgridtax.BeginEdit(true);
                    //            tb.Focus();
                    //        }
                    //    }
                    //}
                    //else if (e.KeyCode == Keys.Enter && tb.Text != string.Empty)
                    //{
                    //    string k = dtgridtax.Rows[rowNo].Cells[1].Value.ToString();
                    //    int rowcount = dtgridtax.Rows.Count;
                    //    if (rowNo == rowcount - 1)
                    //    {
                    //        // int val;
                    //        dtgridtax.Rows.Add();
                    //        rowNo = dtgridtax.RowCount - 1;

                    //        if (dtgridtax.Rows[rowNo].Cells[1].Value != null)
                    //        {
                    //            dtgridtax.Rows[rowNo].Cells[0].Value = dtgridtax.Rows.Count;
                    //            dtgridtax.CurrentCell = dtgridtax[1, rowNo];
                    //            dtgridtax.BeginEdit(true);
                    //        }
                    //        else
                    //        {
                    //            /*if (rowNo > val)
                    //            {
                    //                itemMastGrid.Rows.Remove(itemMastGrid.Rows[rowNo]);
                    //            }*/
                    //            dtgridtax.Rows[rowNo].Cells[0].Value = dtgridtax.Rows.Count;
                    //            dtgridtax.CurrentCell = dtgridtax[1, rowNo];
                    //            dtgridtax.BeginEdit(true);
                    //        }
                    //    }
                    //}
                    //else if (e.KeyCode == Keys.Enter && tb.Text == string.Empty)
                    //{
                    //    dtgridtax.CurrentCell = dtgridtax[1, rowNo];
                    //    dtgridtax.BeginEdit(true);
                    //    tb.Focus();
                    //}
                }
                else if (colNo == 3)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        if (rowNo == dtgridtax.Rows.Count - 1)
                        {
                            dtgridtax.Rows.Add();
                            rowNo = dtgridtax.RowCount - 1;
                            dtgridtax.Rows[rowNo].Cells[0].Value = dtgridtax.Rows.Count;
                            dtgridtax.CurrentCell = dtgridtax[1, rowNo];
                        }
                        else
                        {
                            rowNo++;
                            dtgridtax.CurrentCell = dtgridtax[1, rowNo];
                        }

                    }
                }
                //if (e.KeyCode == Keys.Add && colNo == 1)
                //{
                //    if (dtgridtax.RowCount > 0)
                //    {
                //        //plusKeyPress = true;
                //        int i = dtgridtax.CurrentCell.RowIndex;
                //        if (Convert.ToString(dtgridtax.Rows[i].Cells[1].Value) == string.Empty)
                //        {
                //            rowNo = rowNo - 1;
                //            dtgridtax.Rows.RemoveAt(i);
                //        }
                //        BtnSave.Enabled = true;
                //        BtnSave.Focus();
                //    }
                //}
                Globalvariable.searchNo = 0;
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void dtgridtax_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            dtgridtax[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.Yellow;
        }

        private void dtgridtax_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            dtgridtax[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.White;
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
            //    try
            //    {
            //        Globalvariable.SearchString = "TallyID";
            //        if (txtTallyCode.Text == "" && e.KeyCode == Keys.Enter)
            //        {
            //           Globalvariable.searchNo = 1;
            //            KeysConverter kc = new KeysConverter();
            //            string key = kc.ConvertToString(e.KeyCode);
            //            FrmSearch srch = new FrmSearch();
            //          //  str = "SELECT Tally_Name,Tally_Code FROM tbTallyMaster WHERE Deleted='N' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY Tally_Code";
            //            srchda = mod.GetselectQuery("tbTallyMaster", "Tally_Code", "Tally_Name","N", "N");
            //            srch.val = key;
            //            srch.fillbackgridSrch(srchda, "FrmItemMaster");
            //            ds.Clear();
            //            srchda.Fill(ds);
            //            if (ds.Tables[0].Rows.Count > 0)
            //                srch.ShowDialog();
            //            txtTallyCode.Text = srch.codeselected;
            //            if (srch.codeselected == null)
            //            {
            //                txtTallyCode.Focus();
            //            }
            //            else
            //            {
            //                filltallycode();
            //            }
            //            Globalvariable.searchNo = 0;
            //            if (lblTallyName.Text != "")
            //            {
            //               txtItemRate.Focus();
            //            }
            //            else
            //            {
            //                txtTallyCode.Focus();
            //            }
            //        }
            //        else if (e.KeyCode == Keys.Enter && txtTallyCode.Text != "" && txtTallyCode.Text != "0")
            //        {
            //            filltallycode();
            //        }
            //        else if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            //        {
            //           txtTallyCode.Text = "";
            //           lblTallyName.Text = "";
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        string str = "Message:" + ex.Message;
            //        MessageBox.Show(str, "Error Message");
            //    }
        }

        private void filltallycode()
        {
            try
            {
                if (txtTallyCode.Text.Trim().Equals(""))
                {
                    lblTallyName.Text = "";
                }
                else
                {
                    SqlDataReader dr = mod.GetRecord("SELECT Tally_Name FROM tbTallyMaster WHERE Deleted='N' AND Branchcode='" + Globalvariable.bcode + "' AND Tally_Code='" + txtTallyCode.Text + "'");
                    if (dr.HasRows)
                    {
                        dr.Read();
                        lblTallyName.Text = dr["Tally_Name"].ToString();
                        // txtItemRate.Focus();
                    }
                    else
                    {

                        txtTallyCode.Text = "";
                        lblTallyName.Text = "";
                        txtTallyCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void FrmItemMaster_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 43)
                {
                    if (ActiveControl.AccessibleName == "Editing Control")
                    {
                        if (dtgridtax.Rows[dtgridtax.CurrentCell.RowIndex].Cells[1].Value == null || dtgridtax.Rows[dtgridtax.CurrentCell.RowIndex].Cells[1].Value.ToString() == "")
                        {
                            dtgridtax.Rows.RemoveAt(dtgridtax.CurrentCell.RowIndex);
                            BtnSave.Enabled = true;
                            BtnSave.Focus();
                            return;
                        }
                    }
                }
                Control nextTab;
                if (e.KeyChar == (char)Keys.Enter)
                {
                    /*   if ( txtItemRate.Enabled == false)
                       {                      
                               cmbCategory.Focus();         
                       }
                       else
                       {*/
                    nextTab = ((Control)sender);

                    if (ActiveControl.Name == "txtsearch" && DtgridItemcard.Rows.Count == 0 && e.KeyChar == (char)Keys.Enter)
                    {
                        txtsearch.Focus();
                    }
                    else
                    {
                        if (ActiveControl.Name == "txtsearch" && DtgridItemcard.Rows.Count == 0)
                        {
                            nextTab = BtnAdd;
                        }
                        else if (ActiveControl.Name == "cmbActive")
                        {
                            if (ActiveControl.Name != "dtgridtax")
                            {
                                BtnSave.Focus();
                            }
                        }
                        else if (ActiveControl.Name == "txtTallyCode")
                        {
                            if (lblTallyName.Text == "" && txtTallyCode.Text != "0")
                            {
                                txtTallyCode.Focus();
                            }
                            else if (ActiveControl.Name == "txtTallyCode" && txtItemRate.Enabled == false)
                            {
                                cmbCategory.Focus();
                            }
                            else
                            {
                                nextTab = GetNextControl(ActiveControl, true);
                            }

                        }
                        /*   else if (ActiveControl.Name == "txtTallyCode"&&txtItemRate.Enabled == false && e.KeyChar == (char)Keys.Enter)
                           {
                              cmbCategory.Focus();
                           }*/
                        else
                        {
                            if (BtnDelete.Text != "RECALL")
                                nextTab = GetNextControl(ActiveControl, true);
                        }
                        nextTab.Focus();
                        if (ActiveControl.Name == "dtgridtax" && dtgridtax.RowCount > 0)
                        {
                            rowNo = dtgridtax.CurrentCell.RowIndex;
                            colNo = dtgridtax.CurrentCell.ColumnIndex;
                            if (dtgridtax.Rows[rowNo].Cells[1].Value != null)
                            {
                                dtgridtax.CurrentCell = dtgridtax[3, rowNo];
                                dtgridtax.BeginEdit(true);
                            }
                            else
                            {

                                dtgridtax.CurrentCell = dtgridtax[1, rowNo];
                                dtgridtax.BeginEdit(true);
                            }
                            return;
                            //rowNo = dtgridtax.CurrentCell.RowIndex;
                            //colNo = dtgridtax.CurrentCell.ColumnIndex;
                            //if (dtgridtax.Rows[rowNo].Cells[colNo].Value != null && dtgridtax.Rows[rowNo].Cells[colNo].Value.ToString() != "")
                            //{
                            //    if (colNo == 0)
                            //    {
                            //        if (plusKeyPress == true)
                            //        {
                            //            plusKeyPress = false;
                            //        }
                            //        else if (dtgridtax.Rows[rowNo].Cells[0].Value != null)
                            //        {
                            //            dtgridtax.CurrentCell = dtgridtax[1, rowNo];
                            //            dtgridtax.BeginEdit(true);
                            //        }
                            //    }
                            //    else if (colNo == 1)
                            //    {
                            //        if (dtgridtax.Rows[rowNo].Cells[1].Value != null)
                            //        {
                            //            dtgridtax.CurrentCell = dtgridtax[1, rowNo];
                            //            dtgridtax.BeginEdit(true);
                            //        }
                            //    }
                            //    //else if (colNo == 2)
                            //    //{
                            //    //    dtgridtax.CurrentCell = dtgridtax[3, rowNo];
                            //    //    dtgridtax.BeginEdit(true);
                            //    //}
                            //}
                            //else
                            //{
                            //    dtgridtax.CurrentCell = dtgridtax[colNo, rowNo];
                            //    dtgridtax.BeginEdit(true);
                            //}
                        }
                    }
                }
            }
            //   }
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

        private void cmbActive_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                cmbActive.BackColor = Color.White;

                if (e.KeyCode == Keys.Enter && cmbActive.SelectedIndex != 0)
                {

                    if (flag == 0)
                    {
                        if (cmbActive.SelectedIndex != 0)
                        {
                            dtgridtax.Rows.Clear();
                            dtgridtax.Enabled = true;
                            dtgridtax.BackgroundColor = gridBackColor;
                            dtgridtax.Rows.Add();
                            dtgridtax.Rows[0].Cells[0].Value = dtgridtax.Rows.Count;
                            dtgridtax.CurrentCell = dtgridtax[1, 0];
                            dtgridtax.BeginEdit(true);
                            e.Handled = true;
                        }
                    }
                    else if (flag == 1)
                    {
                        dtgridtax.Enabled = true;
                        dtgridtax.BackgroundColor = gridBackColor;
                        if (dtgridtax.RowCount < 1)
                        {
                            dtgridtax.Rows.Add();
                            dtgridtax.Rows[0].Cells[0].Value = dtgridtax.Rows.Count;
                        }
                        dtgridtax.CurrentCell = dtgridtax[1, 0];
                        dtgridtax.BeginEdit(true);
                    }
                }
                else if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
                {
                    cmbActive.SelectedIndex = 0;
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
            Save();
        }

        public string SearchTax(string searchStr)
        {
            //   DupCode1 = DuplicateRecord(dtgridtax, 1);
            try
            {
                if (Globalvariable.searchNo == 1)
                {
                    strsrch = "SELECT name,tax_code FROM tbTaxMaster WHERE deleted='N' OR name LIKE '" +
                           searchStr + "%' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY tax_code";
                }
                else if (Globalvariable.searchNo == 2)
                {
                    strsrch = "SELECT name,tax_code FROM tbTaxMaster WHERE deleted='N' AND name LIKE '" +
                           searchStr + "%' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY tax_code";
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
            return strsrch;
        }


        private void Save()
        {
            try
            {
                if (DtgridItemcard.Rows.Count > 0)
                {
                    selectrow = DtgridItemcard.SelectedCells[0].RowIndex;
                }
                if (txtname.Text == "")
                {
                    txtname.Focus();
                }
                else if (txtItemRate.Text == "" || txtItemRate.Text == "0")
                {
                    txtItemRate.Focus();
                }
                else
                {
                    if (BtnSave.Text == "SAVE")
                    {
                        str = "SELECT * FROM tbMenuGroup WHERE Item_name='" + txtname.Text.Replace("'", "''") +
                        "' AND Branchcode='" + Globalvariable.bcode + "'";
                    }
                    else if (BtnSave.Text == "UPDATE")
                    {
                        str = "SELECT * FROM tbMenuGroup WHERE Item_name='" + txtname.Text.Replace("'", "''") +
                           "' AND Branchcode='" + Globalvariable.bcode + "' AND Item_Code !=" + txtcode.Text + "";
                    }
                    SqlDataReader dr = mod.GetRecord(str);
                    if (dr.HasRows)
                    {
                        MessageBox.Show("Name Already Exist!!");
                        txtname.Focus();
                    }
                    else
                    {

                        for (int cnt = 0; cnt < dtgridtax.Rows.Count; cnt++)
                        {
                            if (dtgridtax.Rows[cnt].Cells[1].Value == null)
                            {
                                dtgridtax.Rows.RemoveAt(cnt);
                            }
                        }

                        filpayble = cmbPayable.SelectedValue.ToString();
                        filcatgry = cmbCategory.SelectedValue.ToString();
                        filserviTaxApp = cmbservicetax.SelectedValue.ToString();
                        filActiv = cmbActive.SelectedValue.ToString();
                        //if (cmbPayable.SelectedItem == "Yes")
                        //{
                        //    filpayble = "Y";
                        //}
                        //else
                        //{
                        //    filpayble = "N";
                        //}

                        //if (cmbCategory.SelectedItem == "Kg/ML")
                        //{
                        //    filcatgry = "2";
                        //}
                        //else if (cmbCategory.SelectedItem == "Per Piece")
                        //{
                        //    filcatgry = "1";
                        //}
                        //if (cmbservicetax.SelectedItem == "Yes")
                        //{
                        //    filserviTaxApp = "Y";
                        //}
                        //else if (cmbservicetax.SelectedItem == "No")
                        //{
                        //    filserviTaxApp = "N";
                        //}
                        //if (cmbActive.SelectedItem == "Yes")
                        //{
                        //    filActiv = "Y";
                        //}
                        //else
                        //{
                        //    filActiv = "N";
                        //}
                        if (chkSaleMode.Checked == true)
                        {
                            RestSaleMode = "Y";
                        }
                        else if (chkSaleMode.Checked == false)
                        {
                            RestSaleMode = "N";
                        }


                        BtnSave.Enabled = false;

                        string ProcessName = BtnSave.Text == "SAVE" ? "INSERT" : "UPDATE";
                        Models.Common.DbConnectivity.Instance.ManageItemMaster(ProcessName, txtcode.Text, txtname.Text, filpayble,
                            mod.Isnull(txtTallyCode.Text, "0"), txtItemRate.Text, filcatgry, filserviTaxApp, RestSaleMode, filActiv);

                        Models.Common.DbConnectivity.Instance.ManageItemGroupTaxDetail("DELETE", txtcode.Text);
                        for (int cnt = 0; cnt < dtgridtax.Rows.Count; cnt++)
                        {
                            if (dtgridtax.Rows[cnt].Cells[0].Value == null)
                                dtgridtax.Rows.RemoveAt(cnt);
                            else
                            {
                                Models.Common.DbConnectivity.Instance.ManageItemGroupTaxDetail("INSERT", txtcode.Text, dtgridtax.Rows[cnt].Cells[1].Value.ToString(), dtgridtax.Rows[cnt].Cells[3].Value.ToString());
                            }
                        }
                        //str = "INSERT INTO tbMenuGroup(Item_Code,Item_name,Payble," +
                        // "Tally_Code,Item_Rate,Category,Appli_Service_Tax,Res_Sale_Mode,Active,deleted,Branchcode) VALUES(" + txtcode.Text +
                        //     ",'" + txtname.Text.Replace("'", "''") + "','" + filpayble + "','" + mod.Isnull(txtTallyCode.Text, "0") + "','" + txtItemRate.Text + "','" + filcatgry + "','" + filserviTaxApp + "','" + RestSaleMode + "','" + filActiv + "','N','" + Globalvariable.bcode + "' )";
                        //command.CommandText = str;
                        //command.ExecuteNonQuery();
                        //command = new SqlCommand("SP_INSERTItemMaster", con.connect());
                        //command.CommandType = CommandType.StoredProcedure;
                        //if (BtnSave.Text == "SAVE")
                        //{
                        //    txtcode.Text = "" + mod.GetMaxNum(tableName, firstCol);
                        //    command.Parameters.AddWithValue("@variable", "INSERT");
                        //}
                        //else
                        //{
                        //    command.Parameters.AddWithValue("@variable", "UPDATE");
                        //}
                        //command.Parameters.AddWithValue("@code", txtcode.Text);
                        //command.Parameters.AddWithValue("@name", txtname.Text.Replace("'", "''"));
                        //command.Parameters.AddWithValue("@payble", filpayble);
                        //command.Parameters.AddWithValue("@tallycode", mod.Isnull(txtTallyCode.Text, "0"));
                        //command.Parameters.AddWithValue("@itemrate", txtItemRate.Text);
                        //command.Parameters.AddWithValue("@categry", filcatgry);
                        //command.Parameters.AddWithValue("@servicetax", filserviTaxApp);
                        //command.Parameters.AddWithValue("@restasalemode", RestSaleMode);
                        //command.Parameters.AddWithValue("@active", filActiv);
                        //command.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                        //command.ExecuteNonQuery();
                        if (BtnSave.Text == "SAVE")
                        {
                            lblSucessMsg.Text = "Save Successfully !!!";
                        }
                        else
                        {
                            lblSucessMsg.Text = "Update Successfully !!!";
                            StrUpdate = "U";
                        }
                        //SqlCommand cmd;
                        //str = "DELETE FROM tbItemGroupTaxDetail WHERE Item_Code=" + txtcode.Text + " AND Branchcode='" + Globalvariable.bcode + "'";
                        //cmd = new SqlCommand(str, con.connect());
                        //cmd.ExecuteNonQuery();
                        //for (int cnt = 0; cnt < dtgridtax.Rows.Count; cnt++)
                        //{
                        //    if (dtgridtax.Rows[cnt].Cells[0].Value == null)
                        //        dtgridtax.Rows.RemoveAt(cnt);
                        //    else
                        //    {
                        //        str = "INSERT INTO tbItemGroupTaxDetail(Item_Code,Tax_Code,Branchcode) VALUES(" + txtcode.Text +
                        //            "," + dtgridtax.Rows[cnt].Cells[1].Value +
                        //            ",'" + Globalvariable.bcode + "' )";
                        //        cmd.CommandText = str;
                        //        cmd.ExecuteNonQuery();
                        //    }
                        //}
                        mod.DataGridBind(DtgridItemcard, tableName, firstCol, secondCol, "N", "Name", "Item No.");
                        txtsearch.Focus();
                        txtsearch.Text = "";
                        if (BtnSave.Text == "SAVE")
                        {
                            if (Globalvariable.frmName == "MenuItem")
                            {
                                Globalvariable.ItemGrpid = txtcode.Text;
                                Globalvariable.ItemGrpnm = txtname.Text;
                                this.Close();
                            }
                            else
                            {
                                string txt = BtnSave.Text;
                                FillDetails();
                                int s = selectrow;
                                if (DtgridItemcard.Rows.Count > 0)
                                {
                                    DtgridItemcard.CurrentCell = DtgridItemcard[0, s];
                                }
                                StrUpdate = "";
                                BtnSave.Enabled = false;
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

        private void cmbservicetax_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbservicetax.SelectedItem == "Yes")
            {
                dtgridtax.Enabled = true;
                dtgridtax.BackgroundColor = gridBackColor;
            }
            else
            {
                dtgridtax.Enabled = false;
                dtgridtax.BackgroundColor = Color.DarkGray;
            }
        }

        private void chkSaleMode_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSaleMode.Checked == true)
            {
                white_textbox();
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
                        FrmItemMaster_Load(sender, EventArgs.Empty);
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

        private void cmbActive_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                cmbActive.BackColor = Color.White;
                if (e.KeyChar == (char)Keys.Enter)
                {
                    if (cmbservicetax.Text == "Yes")
                    {
                        if (flag == 0)
                        {
                            cmbActive.BackColor = Color.White;
                            dtgridtax.Rows.Clear();
                            dtgridtax.Enabled = true;
                            dtgridtax.BackgroundColor = gridBackColor;
                            dtgridtax.Rows.Add();
                            rowNo = dtgridtax.RowCount - 1;
                            dtgridtax.Rows[rowNo].Cells[0].Value = dtgridtax.Rows.Count;
                            dtgridtax.CurrentCell = dtgridtax[1, 0];
                            dtgridtax.BeginEdit(true);
                        }
                        else if (flag == 1)
                        {
                            int val;
                            cmbActive.BackColor = Color.White;
                            dtgridtax.Enabled = true;
                            dtgridtax.BackgroundColor = gridBackColor;
                            if (dtgridtax.Rows.Count == 0)
                            {
                                dtgridtax.Rows.Add();
                                rowNo = dtgridtax.RowCount - 1;
                                if (rowNo == 0)
                                {
                                    val = rowNo;
                                }
                                else
                                {
                                    val = rowNo - 1;
                                }
                                if (dtgridtax.Rows[val].Cells[1].Value != null)
                                {
                                    dtgridtax.Rows[rowNo].Cells[0].Value = dtgridtax.Rows.Count;
                                    dtgridtax.CurrentCell = dtgridtax[1, rowNo];
                                    dtgridtax.BeginEdit(true);
                                }
                                else
                                {
                                    if (rowNo > val)
                                    {
                                        dtgridtax.Rows.Remove(dtgridtax.Rows[rowNo]);
                                    }
                                    dtgridtax.Rows[rowNo].Cells[0].Value = dtgridtax.Rows.Count;
                                    dtgridtax.CurrentCell = dtgridtax[1, val];
                                    dtgridtax.BeginEdit(true);
                                }
                            }
                            else
                            {
                                dtgridtax.CurrentCell = dtgridtax[1, 0];
                                dtgridtax.BeginEdit(true);
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

        private void chkSaleMode_Enter(object sender, EventArgs e)
        {
            white_textbox();
        }

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnLessID_Click(object sender, EventArgs e)
        {

            Models.Common.FormManagement.Instance.SelectRecordFromGrid(DtgridItemcard, Models.Common.RecordMove.Previous, FillDetails);

        }

        private void btnGreaterID_Click(object sender, EventArgs e)
        {

            Models.Common.FormManagement.Instance.SelectRecordFromGrid(DtgridItemcard, Models.Common.RecordMove.Next, FillDetails);

        }

        private void txtTallyCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void txtItemRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == '.');
        }

        private void cmbPayable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPayable.SelectedItem.ToString() == "No")
            {
                txtItemRate.Enabled = false;
                txtItemRate.Text = "0";
            }
            else if (cmbPayable.SelectedItem.ToString() == "Yes")
            {
                txtItemRate.Enabled = true;
            }
        }

        private void txtsearch_Leave(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.White;
        }

        private void txtname_Leave(object sender, EventArgs e)
        {
            txtname.BackColor = Color.White;
        }

        private void cmbPayable_Leave(object sender, EventArgs e)
        {
            cmbPayable.BackColor = Color.White;
        }

        private void txtTallyCode_Leave(object sender, EventArgs e)
        {
            txtTallyCode.BackColor = Color.White;
        }

        private void txtItemRate_Leave(object sender, EventArgs e)
        {
            txtItemRate.BackColor = Color.White;
        }

        private void cmbCategory_Leave(object sender, EventArgs e)
        {
            cmbCategory.BackColor = Color.White;
        }

        private void cmbservicetax_Leave(object sender, EventArgs e)
        {
            cmbservicetax.BackColor = Color.White;
        }

        private void cmbActive_Leave(object sender, EventArgs e)
        {
            cmbActive.BackColor = Color.White;
        }

        private void FrmItemMaster_KeyDown(object sender, KeyEventArgs e)
        {

            try
            {
                if (ActiveControl.Name.Equals("dtgridtax"))
                {
                    if (e.KeyCode == Keys.Add)
                    {
                        if (dtgridtax.Rows.Count > 0)
                        {

                            if (Convert.ToString(dtgridtax.Rows[dtgridtax.CurrentCell.RowIndex].Cells[1].Value) == string.Empty)
                            {
                                if (rowNo >= dtgridtax.CurrentCell.RowIndex)
                                {
                                    rowNo--;
                                }

                                dtgridtax.Rows.RemoveAt(dtgridtax.CurrentCell.RowIndex);

                                BtnSave.Enabled = true;
                                BtnSave.Focus();

                                e.Handled = true;
                                return;
                            }
                        }
                    }

                }
            }
            catch
            {

            }
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

        private void DtgridItemcard_CellClick(object sender, DataGridViewCellEventArgs e)
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

        private void FrmItemMaster_Shown(object sender, EventArgs e)
        {
            txtsearch.Focus();
        }


    }
}

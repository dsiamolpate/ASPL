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
    public partial class FrmSubItemMaster : Form
    {
        Design ObjDesign = new Design();
        public FrmSubItemMaster()
        {
            InitializeComponent();          
        }
        public static int searchNo;
        Module mod = new Module();
        Connection con = new Connection();
        public static SqlDataReader srchdr;
        public static SqlDataAdapter srchda;
        DataSet ds = new DataSet();
        string firstCol = "Sub_Item_Code";
        string secondCol = "Item_Name";
        string tableName = "tbSubItemMaster";
        string Deleted = "Deleted";
        string str, strsrch, StrUpdate;
        int selectrow;
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

        private void FrmSubItemMaster_Load(object sender, EventArgs e)
        {
            LoadEvent();
        }

        public void LoadEvent()
        {
            try
            {
                ObjDesign.FormDesign(this, DtgridMenucard);
               // this.Left = 210;
               // mod.FormLoad(this, firstCol, secondCol, tableName, DtgridMenucard, txtsearch, Chkshowdelet);
                mod.FormLoad(this, tableName, firstCol, secondCol, Deleted, "N", "N", DtgridMenucard, txtsearch, Chkshowdelet, "Description", "Code");
                FillDetails();
                txtcode.Enabled = false;
                //txtItemGrpname.Enabled = false;
                //txtkichsenam.Enabled = false;
                mod.UserAccessibillityMaster("Menu Item", BtnAdd, BtnSave, BtnDelete);
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
                if (Chkshowdelet.Checked == true)
                {
                    mod.chkDeletedTrue(this, BtnAdd, BtnSave, BtnDelete, DtgridMenucard, txtsearch);
                    if (DtgridMenucard.Rows.Count == 0)
                    {
                        btnGreaterID.Enabled = false;
                        btnLessID.Enabled = false;
                    }
                    else
                    {
                        btnGreaterID.Enabled = true;
                        btnLessID.Enabled = true;
                    }
                    txtsearch.Focus();
                }
                else
                {
                    mod.chkDeletedFalse("Menu Item",this, BtnAdd, BtnSave, BtnDelete, DtgridMenucard, txtsearch);
                    if (DtgridMenucard.Rows.Count == 0)
                    {
                        btnGreaterID.Enabled = false;
                        btnLessID.Enabled = false;
                    }
                    else
                    {
                        btnGreaterID.Enabled = true;
                        btnLessID.Enabled = true;
                    }
                }
                if (DtgridMenucard.Rows.Count > 0)
                {
                    int i;
                    if (StrUpdate == "U")
                    {
                        i = selectrow;
                    }
                    else
                    {
                        i = DtgridMenucard.SelectedCells[0].RowIndex;
                    }
                    if (DtgridMenucard.Rows[i].Cells[1].Value != null)
                    {
                        string ID = mod.Isnull(DtgridMenucard.Rows[i].Cells[1].Value.ToString(), "");
                        //SqlDataReader dr = mod.GetRecord("SELECT * FROM tbSubItemMaster WHERE Sub_Item_Code='" + s + "' AND Branchcode='" + Globalvariable.bcode + "'");
                        SqlDataReader dr = mod.GetSelectAllField(tableName, firstCol, ID,"");
                        while (dr.Read())
                        {
                            txtcode.Text = "";
                            txtcode.Text = dr["Sub_Item_Code"].ToString();
                            txtcode.Enabled = false;
                            txtname.Text = mod.Isnull(dr["Item_Name"].ToString(), "");
                            txtItemRate1.Text = mod.Isnull(dr["Item_Rate1"].ToString(), "");
                            txtRate2.Text = mod.Isnull(dr["Item_Rate2"].ToString(), "");
                            txtRate3.Text = mod.Isnull(dr["Item_Rate3"].ToString(), "");
                            txtItemGropId.Text = mod.Isnull(dr["Item_Grp_Code"].ToString(), "");
                            fillItmGrpID();
                            txtkichsectid.Text = mod.Isnull(dr["KitchenSec_Code"].ToString(), "0");
                            if (txtkichsectid.Text != "0")
                            {
                                fillKotsection();
                            }
                            else if (txtkichsectid.Text == "0")
                            {
                                lblKitSectName.Text= "";
                            }
                        }
                    }
                }
                else
                {
                    mod.txtclear(this.Controls);
                    BtnDelete.Text = "RESET";

                }
              //  lblSucessMsg.Text = "";
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

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            add();
           Chkshowdelet.Checked = false;
           lblSucessMsg.Text = "";
        }

        public void maxID()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT (ISNULL(MAX(Sub_Item_Code),0)+1) FROM tbSubItemMaster WHERE Branchcode='" + Globalvariable.bcode + "'", con.connect());
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
                mod.addBtnClick(this, txtcode, tableName, firstCol, BtnSave, BtnDelete, txtItemGropId);
                mod.UserAccessibillityMaster("Menu Item", BtnAdd, BtnSave, BtnDelete);
               // maxID();
                txtcode.Text = "" + mod.GetMaxNum(tableName, firstCol);
                label1.Text = "Search by Name :";
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void txtItemGropId_Enter(object sender, EventArgs e)
        {
            txtItemGropId.BackColor = Color.Yellow;
        }

        private void txtname_Enter(object sender, EventArgs e)
        {
           txtname.BackColor = Color.Yellow;
        }

        private void txtItemRate1_Enter(object sender, EventArgs e)
        {
           txtItemRate1.BackColor = Color.Yellow;
        }

        private void txtRate2_Enter(object sender, EventArgs e)
        {
           txtRate2.BackColor = Color.Yellow;
        }

        private void txtRate3_Enter(object sender, EventArgs e)
        {
           txtRate3.BackColor = Color.Yellow;
        }

        private void txtkichsectid_Enter(object sender, EventArgs e)
        {
           txtkichsectid.BackColor = Color.Yellow;
        }

        private void Chkshowdelet_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //mod.chkDeletedChange(Chkshowdelet, firstCol, secondCol, tableName, DtgridMenucard, txtsearch);
                mod.chkDeletedChange(Chkshowdelet, firstCol, secondCol, tableName, Deleted, "N", DtgridMenucard, txtsearch, "Item Name", "Code");
                FillDetails();
                lblSucessMsg.Text = "";
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void DtgridMenucard_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                mod.gridKeyDown(e, BtnDelete, txtsearch, DtgridMenucard, this);
                FillDetails();
                if (e.KeyCode == Keys.Enter && Chkshowdelet.Checked == true && DtgridMenucard.Rows.Count > 0)
                    BtnDelete.Focus();
                else if (e.KeyCode == Keys.Enter && Chkshowdelet.Checked == true && DtgridMenucard.Rows.Count == 0)
                    BtnExit.Focus();
                else if (e.KeyCode == Keys.Enter && Chkshowdelet.Checked == false && DtgridMenucard.Rows.Count == 0)
                    BtnExit.Focus();
                lblSucessMsg.Text = "";

            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void DtgridMenucard_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                FillDetails();
                mod.gridDoubleClick(Chkshowdelet, BtnDelete, txtname);
                lblSucessMsg.Text = "";
                BtnSave.Enabled = true;

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
                mod.txtSearchChange(Chkshowdelet, firstCol, secondCol, tableName, DtgridMenucard, txtsearch, "Item Name", "Code");
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

        private void txtsearch_KeyDown(object sender, KeyEventArgs e)
        {
            DtgridMenucard_KeyDown(sender, e);
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
                        FrmSubItemMaster_Load(sender, EventArgs.Empty);
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

        private void FrmSubItemMaster_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Control nextTab;
                if (e.KeyChar == (char)Keys.Enter)
                {
                    nextTab = ((Control)sender);
                    if (ActiveControl.Name == "txtsearch" && DtgridMenucard.Rows.Count == 0 && e.KeyChar == (char)Keys.Enter)
                    {
                        txtsearch.Focus();
                    }
                    else
                    {
                        if (ActiveControl.Name == "txtsearch" && DtgridMenucard.Rows.Count == 0)
                        {
                            nextTab = BtnAdd;
                        }
                        else if (ActiveControl.Name == "txtItemGropId")
                        {
                            if (lblItemGroupName.Text == "")
                            {
                                txtItemGropId.Focus();
                            }
                            else
                            {
                                nextTab = GetNextControl(ActiveControl, true);
                            }
                        }
                        else if (ActiveControl.Name == "txtkichsectid")
                        {
                            if (lblKitSectName.Text== "" && txtkichsectid.Text != "0")
                            {
                                txtkichsectid.Focus();
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

        private void txtsearch_Enter(object sender, EventArgs e)
        {
           txtsearch.BackColor = Color.Yellow;
        }

        private void btnkitBrw_Click(object sender, EventArgs e)
        {
            frmKotSection frm = new frmKotSection();
            frm.ShowDialog();
           txtkichsectid.Text = Globalvariable.kot_id;
           lblKitSectName.Text= Globalvariable.kot_name;
        }


        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (DtgridMenucard.Rows.Count > 0)
                {
                    selectrow = DtgridMenucard.SelectedCells[0].RowIndex;
                }
                int i;
                if (txtItemGropId.Text == "")
                {
                    txtItemGropId.Focus();
                }             
                else if (txtItemRate1.Text == "")
                {
                    txtItemRate1.Focus();
                }               
               else if (txtname.Text == "")
                {
                    txtname.Focus();
                }
                else
                {
                    if (BtnSave.Text == "SAVE")
                        str = "SELECT * FROM " + tableName + " WHERE " + secondCol + "='" +  txtname.Text.Replace("'", "''") +
                            "' AND Branchcode='" + Globalvariable.bcode + "'";
                    else if (BtnSave.Text == "UPDATE")
                        str = "SELECT * FROM " + tableName + " WHERE " + secondCol + "='" + txtname.Text.Replace("'", "''") +
                            "' AND Branchcode='" + Globalvariable.bcode + "' AND " + firstCol + " !=" + txtcode.Text + "";
                    SqlDataReader dr = mod.GetRecord(str);
                    if (dr.HasRows)
                    {
                        MessageBox.Show("Name Already Exist!!");
                        txtname.Focus();
                    }
                    else
                    {
                        BtnSave.Enabled = false;
                      
                            //str = "INSERT INTO tbSubItemMaster(Sub_Item_Code,Item_Name,Item_Grp_Code,Item_Rate1,Item_Rate2,Item_Rate3,KitchenSec_Code,Deleted,Branchcode) VALUES(" + txtcode.Text +
                            //         ",'" + txtname.Text.Replace("'", "''") + "','" + txtItemGropId.Text +
                            //        "','" + txtItemRate1.Text + "','" + txtRate2.Text + "','" + txtRate3.Text + "','"+mod.Isnull(txtkichsectid.Text,"0")+"','N','" + Globalvariable.bcode + "' )";
                            // i = mod.SaveMaster(str);
                           SqlCommand comm = new SqlCommand("SP_INSERTSubItemMaster", con.connect());
                           comm.CommandType = CommandType.StoredProcedure;
                           if (BtnSave.Text == "SAVE")
                           {
                               txtcode.Text = "" + mod.GetMaxNum(tableName, firstCol);
                           comm.Parameters.AddWithValue("@variable", "INSERT");
                           }
                             else
                            {
                             comm.Parameters.AddWithValue("@variable", "UPDATE");
                           }
                           comm.Parameters.AddWithValue("@code", txtcode.Text);
                           comm.Parameters.AddWithValue("@name", txtname.Text.Replace("'", "''"));
                           comm.Parameters.AddWithValue("@itemgrpcode", txtItemGropId.Text);
                           comm.Parameters.AddWithValue("@itemrate1", txtItemRate1.Text);
                           comm.Parameters.AddWithValue("@itemrate2", txtRate2.Text);
                           comm.Parameters.AddWithValue("@itemrate3", txtRate3.Text);
                           comm.Parameters.AddWithValue("@kitchensectcod", mod.Isnull(txtkichsectid.Text, "0"));
                           comm.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                           comm.ExecuteNonQuery();
                        if (BtnSave.Text == "SAVE")
                           {
                             lblSucessMsg.Text = "Save Successfully !!!";
                        }
                        else
                        {
                            lblSucessMsg.Text = "Update Successfully !!!";
                            StrUpdate = "U";
                        }           
                        mod.DataGridBind(DtgridMenucard, tableName, firstCol, secondCol, "N", "Item Name", "Code");                    
                            txtsearch.Focus();
                            txtsearch.Text = "";
                            string txt = BtnSave.Text;
                            FillDetails();
                        int s = selectrow;
                        if (DtgridMenucard.Rows.Count > 0)
                        {
                            DtgridMenucard.CurrentCell = DtgridMenucard[0, s];
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

        public string searchChange(string searchStr)
        {
            try
            {
                if (Globalvariable.searchNo == 1)
                {
                    strsrch = "SELECT Item_name,Item_Code FROM tbMenuGroup WHERE deleted='N' OR Item_name LIKE '" +
                           searchStr + "%' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY Item_Code";

                }
                else if (Globalvariable.searchNo == 2)
                {
                    strsrch = "SELECT Item_name,Item_Code FROM tbMenuGroup WHERE deleted='N' AND Item_name LIKE '" +
                           searchStr + "%' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY Item_Code";
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
            return strsrch;
        }

        private void txtItemGropId_KeyDown(object sender, KeyEventArgs e)
        {
            Globalvariable.MenuCardVarb = "GroupId";
            var returnForm = Models.Common.FormManagement.Instance.SearchFormOnKeyDown(ref txtItemGropId, e, "FrmSubItemMaster", true);
            if (returnForm == null)
            {
                return;
            }
            txtItemGropId.Text = returnForm.codeselected;
            lblItemGroupName.Text = returnForm.descselected;
            return;
            //try
            //{
            //    Globalvariable.MenuCardVarb = "GroupId";
            //    if (txtItemGropId.Text == "" && e.KeyCode == Keys.Enter)
            //    {
            //       Globalvariable.searchNo = 1;
            //        KeysConverter kc = new KeysConverter();
            //        string key = kc.ConvertToString(e.KeyCode);
            //        FrmSearch srch = new FrmSearch();
            //       // str = "SELECT Item_name,Item_Code FROM tbMenuGroup WHERE Deleted='N' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY Item_Code ";
            //        srchda = mod.GetselectQuery("tbMenuGroup", "Item_Code", "Item_name","N", "N");
            //        srch.val = key;
            //        srch.fillbackgridSrch(srchda, "FrmSubItemMaster");
            //        ds.Clear();
            //        srchda.Fill(ds);
            //        if (ds.Tables[0].Rows.Count > 0)
            //            srch.ShowDialog();
            //      txtItemGropId.Text = srch.codeselected;
            //        if (srch.codeselected == null)
            //        {
            //            txtItemGropId.Focus();
            //        }
            //        else
            //        {
            //            fillItmGrpID();
            //        }
            //        Globalvariable.searchNo = 0;
            //        if (txtItemGrpname.Text != "")
            //        {
            //            txtname.Focus();
            //        }
            //        else
            //        {
            //            txtItemGropId.Focus();
            //        }
            //    }
            //    else if (e.KeyCode == Keys.Enter && txtItemGropId.Text != "")
            //    {
            //        fillItmGrpID();
            //    }
            //    else if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            //    {
            //        txtItemGropId.Text = "";
            //       txtItemGrpname.Text = "";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    string str = "Message:" + ex.Message;
            //    MessageBox.Show(str, "Error Message");
            //}
        }
        private void fillItmGrpID()
        {
            try
            {
                if (txtItemGropId.Text.Trim().Equals(""))
                {
                    txtItemGropId.Text = "";
                }
                else
                {
                    SqlDataReader dr = mod.GetRecord("SELECT Item_name FROM tbMenuGroup WHERE Deleted='N' AND Branchcode='" + Globalvariable.bcode + "' AND Item_Code='" + txtItemGropId.Text + "'");
                    if (dr.HasRows)
                    {
                        dr.Read();
                        lblItemGroupName.Text = dr["Item_name"].ToString();
                    }
                    else
                    {
                        txtItemGropId.Text = "";
                        lblItemGroupName.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        public string searchChangeKOTSect(string searchStr)
        {
            try
            {
                if (Globalvariable.searchNo == 1)
                {
                    strsrch = "SELECT Discription,KotSec_Code FROM tbKotSection WHERE deleted='N' OR Discription LIKE '" +
                           searchStr + "%' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY KotSec_Code";

                }
                else if (Globalvariable.searchNo == 2)
                {
                    strsrch = "SELECT Discription,KotSec_Code FROM tbKotSection WHERE deleted='N' AND Discription LIKE '" +
                           searchStr + "%' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY KotSec_Code";
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
            return strsrch;
        }


        private void txtkichsectid_KeyDown(object sender, KeyEventArgs e)
        {
            Globalvariable.MenuCardVarb = "KOTSect";
            var returnForm = Models.Common.FormManagement.Instance.SearchFormOnKeyDown(ref txtkichsectid, e, "FrmSubItemMaster", true);
            if (returnForm == null)
            {
                return;
            }
            txtkichsectid.Text = returnForm.codeselected;
            lblKitSectName.Text= returnForm.descselected;
            return;
            try
            {
                Globalvariable.MenuCardVarb = "KOTSect";
                if (txtkichsectid.Text == "" && e.KeyCode == Keys.Enter)
                {
                   Globalvariable.searchNo = 1;
                    KeysConverter kc = new KeysConverter();
                    string key = kc.ConvertToString(e.KeyCode);
                    FrmSearch srch = new FrmSearch();
                   // str = "SELECT Discription,KotSec_Code FROM tbKotSection WHERE Deleted='N' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY KotSec_Code";
                    srchda = mod.GetselectQuery("tbKotSection", "KotSec_Code", "Discription","N", "N");
                    srch.val = key;
                    srch.fillbackgridSrch(srchda, "FrmSubItemMaster");
                    ds.Clear();
                    srchda.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                        srch.ShowDialog();
                   txtkichsectid.Text = srch.codeselected;
                    if (srch.codeselected == null)
                    {
                        txtkichsectid.Focus();
                    }
                    else
                    {
                        fillKotsection();
                    }
                    Globalvariable.searchNo = 0;
                    if (lblKitSectName.Text!= "")
                    {
                      BtnSave.Focus();
                      txtkichsectid.BackColor = Color.White;
                    }
                    else
                    {
                        txtkichsectid.Focus();
                    }
                }
                else if (e.KeyCode == Keys.Enter && txtkichsectid.Text != "" && txtkichsectid.Text != "0")
                {
                    fillKotsection();
                }
                else if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
                {
                    txtkichsectid.Text = "";
                   lblKitSectName.Text= "";
                }
             
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void fillKotsection()
        {
            try
            {
                if (txtkichsectid.Text.Trim().Equals(""))
                {
                    txtkichsectid.Text = "";
                }
                else
                {
                    SqlDataReader dr = mod.GetRecord("SELECT Discription FROM tbKotSection WHERE Deleted='N' AND Branchcode='" + Globalvariable.bcode + "' AND KotSec_Code='" + txtkichsectid.Text + "'");
                    if (dr.HasRows)
                    {
                        dr.Read();
                       lblKitSectName.Text= dr["Discription"].ToString();
                    }
                    else
                    {
                        txtkichsectid.Text = "";
                        lblKitSectName.Text= "";
                      //  txtkichsectid.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void txtItemRate1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == '.');
                // mod.rateValidation(sender, e, TxtRt1, 4, 2);
                if ((e.KeyChar == (char)Keys.Enter) && (BtnSave.Text == "SAVE"))
                {
                    if ((txtRate2.Text == string.Empty) || (txtRate2.Text == "0"))
                        txtRate2.Text = txtItemRate1.Text;
                    if ((txtRate3.Text == string.Empty) || (txtRate3.Text == "0"))
                        txtRate3.Text = txtItemRate1.Text;
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void txtItemGropId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void txtkichsectid_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void btnLessID_Click(object sender, EventArgs e)
        {
            try
            {
                str = "SELECT * FROM tbSubItemMaster WHERE deleted='N' AND Sub_Item_Code<'" + txtcode.Text + "' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY Sub_Item_Code DESC";
                SqlDataReader dr = mod.GetRecord(str);
                //if (dr1.HasRows)
                //{
                    //getInfo(str);
                if (dr.Read())
                {
                    //getInfo(str);
                    txtcode.Text = "";
                    txtcode.Text = dr["Sub_Item_Code"].ToString();
                    txtcode.Enabled = false;
                    txtname.Text = mod.Isnull(dr["Item_Name"].ToString(), "");
                    txtItemRate1.Text = mod.Isnull(dr["Item_Rate1"].ToString(), "");
                    txtRate2.Text = mod.Isnull(dr["Item_Rate2"].ToString(), "");
                    txtRate3.Text = mod.Isnull(dr["Item_Rate3"].ToString(), "");
                    txtItemGropId.Text = mod.Isnull(dr["Item_Grp_Code"].ToString(), "");
                    fillItmGrpID();
                    txtkichsectid.Text = mod.Isnull(dr["KitchenSec_Code"].ToString(), "0");
                    if (txtkichsectid.Text != "0")
                    {
                        fillKotsection();
                    }
                    else if (txtkichsectid.Text == "0")
                    {
                        lblKitSectName.Text= "";
                    }
                }
                //else
                //{
                //    MessageBox.Show("First Record!!!");
                //}
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }
        public void getInfo(string str)
        {
            try
            {
                SqlDataReader dr = mod.GetRecord(str);
                if (dr.Read())
                {
                    txtcode.Text = "";
                    txtcode.Text = dr["Sub_Item_Code"].ToString();
                    txtcode.Enabled = false;
                    txtname.Text = mod.Isnull(dr["Item_Name"].ToString(), "");
                    txtItemRate1.Text = mod.Isnull(dr["Item_Rate1"].ToString(), "");
                    txtRate2.Text = mod.Isnull(dr["Item_Rate2"].ToString(), "");
                    txtRate3.Text = mod.Isnull(dr["Item_Rate3"].ToString(), "");
                    txtItemGropId.Text = mod.Isnull(dr["Item_Grp_Code"].ToString(), "");
                    fillItmGrpID();
                    txtkichsectid.Text = mod.Isnull(dr["KitchenSec_Code"].ToString(), "0");
                    if (txtkichsectid.Text != "0")
                    {
                        fillKotsection();
                    }
                    else if (txtkichsectid.Text == "0")
                    {
                        lblKitSectName.Text= "";
                    }
                }
            }
            catch (Exception ex)
            {
                string str1 = "Message:" + ex.Message;
                MessageBox.Show(str1, "Error Message");
            }
            
        }

        private void btnGreaterID_Click(object sender, EventArgs e)
        {
            try
            {
                str = "SELECT * FROM tbSubItemMaster WHERE deleted='N' AND Sub_Item_Code>'" + txtcode.Text + "' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY Sub_Item_Code";
                SqlDataReader dr = mod.GetRecord(str);
                //if (dr.HasRows)
                if (dr.Read())
                {
                    //getInfo(str);
                    txtcode.Text = "";
                    txtcode.Text = dr["Sub_Item_Code"].ToString();
                    txtcode.Enabled = false;
                    txtname.Text = mod.Isnull(dr["Item_Name"].ToString(), "");
                    txtItemRate1.Text = mod.Isnull(dr["Item_Rate1"].ToString(), "");
                    txtRate2.Text = mod.Isnull(dr["Item_Rate2"].ToString(), "");
                    txtRate3.Text = mod.Isnull(dr["Item_Rate3"].ToString(), "");
                    txtItemGropId.Text = mod.Isnull(dr["Item_Grp_Code"].ToString(), "");
                    fillItmGrpID();
                    txtkichsectid.Text = mod.Isnull(dr["KitchenSec_Code"].ToString(), "0");
                    if (txtkichsectid.Text != "0")
                    {
                        fillKotsection();
                    }
                    else if (txtkichsectid.Text == "0")
                    {
                        lblKitSectName.Text= "";
                    }
                }
                //else
                //    MessageBox.Show("Last Record!!!");
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void txtsearch_Leave(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.White;
        }

        private void txtItemGropId_Leave(object sender, EventArgs e)
        {
            txtItemGropId.BackColor = Color.White;
        }

        private void txtname_Leave(object sender, EventArgs e)
        {
            txtname.BackColor = Color.White;
        }

        private void txtItemRate1_Leave(object sender, EventArgs e)
        {
           txtItemRate1.BackColor = Color.White;
        }

        private void txtRate2_Leave(object sender, EventArgs e)
        {
           txtRate2.BackColor = Color.White;
        }

        private void txtRate3_Leave(object sender, EventArgs e)
        {
            txtRate3.BackColor = Color.White;
        }

        private void txtkichsectid_Leave(object sender, EventArgs e)
        {
           txtkichsectid.BackColor = Color.White;
        }

        private void FrmSubItemMaster_KeyDown(object sender, KeyEventArgs e)
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

        private void btngrpBrws_Click(object sender, EventArgs e)
        {
            FrmItemMaster frm = new FrmItemMaster();
            frm.ShowDialog();
            txtItemGropId.Text = Globalvariable.ItemGrpid;
            lblItemGroupName.Text = Globalvariable.ItemGrpnm;
        }

        private void DtgridMenucard_CellClick(object sender, DataGridViewCellEventArgs e)
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

        private void FrmSubItemMaster_Shown(object sender, EventArgs e)
        {
            txtsearch.Focus();
        }

        private void txtRate2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == '.');
        }

        private void txtRate3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == '.');
        }

    }
}

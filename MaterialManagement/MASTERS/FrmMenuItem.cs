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

namespace ASPL.Material_Management.MASTERS
{
    public partial class FrmMenuItem : Form
    {
        Design ObjDesign = new Design();
        public FrmMenuItem()
        {
            InitializeComponent();
        }
        private BindingSource bindgrid = new BindingSource();
        Module mod = new Module();
        Connection con = new Connection();
        DataGridViewTextBoxEditingControl tb;
        SqlDataAdapter sqlda;
        DataSet ds = new DataSet();  
        int flag,detail_id = 0;
        bool chkDupFlag;
        public Boolean plusKeyPress;
        public string DupCode1 = "0";
        string code = "";
        // public static SqlDataReader srchdr;
        int rowNo, colNo;
        int selectrow;
        string str, StrUpdate, filcrdr,filc;
        string firstCol = "Item_Code";
        string secondCol = "Item_Name";
        string tableName = "tbItemName";
        string Deleted = "Deleted";
        SqlDataReader dr;
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

        #region Frmevents
        private void FrmMenuItem_Load(object sender, EventArgs e)
        {
            try
            {
                ObjDesign.FormDesign(this, DtgridMenuItem);
                mod.FormLoad(this, tableName, firstCol, secondCol, Deleted, "N", "N", DtgridMenuItem, txtsearch, Chkshowdelet, "Description", "Code");
                FillDetails();
                txtcode.Enabled = false;
                this.KeyPreview = true;
                flag = 1;
                Globalvariable.SearchChangeVariable = "SrchByName";
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void FrmMenuItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Control nextTab;
                if (e.KeyChar == (char)Keys.Enter)
                {
                    nextTab = ((Control)sender);
                    if (ActiveControl.Name == "txtsearch" && DtgridMenuItem.Rows.Count == 0 && e.KeyChar == (char)Keys.Enter)
                    {
                        txtsearch.Focus();
                    }
                    else
                    {
                        if (ActiveControl.Name == "txtsearch" && DtgridMenuItem.Rows.Count == 0)
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

        private void FrmMenuItem_KeyDown(object sender, KeyEventArgs e)
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
        #endregion

        #region txtbox back color
        private void txtsearch_Enter(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.Yellow;          
        }

        private void txtsearch_Leave(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.White;          
        }

        private void txtHSNno_Enter(object sender, EventArgs e)
        {
            txtHSNno.BackColor = Color.Yellow;          
        }

        private void txtHSNno_Leave(object sender, EventArgs e)
        {
            txtHSNno.BackColor = Color.White;          
        }

        private void txtGropId_Enter(object sender, EventArgs e)
        {
            txtGropId.BackColor = Color.Yellow;          
        }

        private void txtGropId_Leave(object sender, EventArgs e)
        {
            txtGropId.BackColor = Color.White;          
        }

        private void txtItemname_Enter(object sender, EventArgs e)
        {
            txtItemname.BackColor = Color.Yellow;          
        }

        private void txtPurchaseRate_Enter(object sender, EventArgs e)
        {
            txtPurchaseRate.BackColor = Color.Yellow;          
        }

        private void txtPurchaseRate_Leave(object sender, EventArgs e)
        {
            txtPurchaseRate.BackColor = Color.White;          
        }

        private void txtItemname_Leave(object sender, EventArgs e)
        {
            txtItemname.BackColor = Color.White;          
        }

        private void txtSaleRate_Enter(object sender, EventArgs e)
        {
            txtSaleRate.BackColor = Color.Yellow;          
        }

        private void txtSaleRate_Leave(object sender, EventArgs e)
        {
            txtSaleRate.BackColor = Color.White;          
        }

        private void cmbCategry_Enter(object sender, EventArgs e)
        {
            cmbCategry.BackColor = Color.Yellow;          
        }

        private void cmbCategry_Leave(object sender, EventArgs e)
        {
            cmbCategry.BackColor = Color.White;          
        }

        private void txtOpeningStock_Enter(object sender, EventArgs e)
        {
            txtOpeningStock.BackColor = Color.Yellow;          
        }

        private void txtOpeningStock_Leave(object sender, EventArgs e)
        {
            txtOpeningStock.BackColor = Color.White;          
        }

        private void combOpenStockCrDr_Enter(object sender, EventArgs e)
        {
            combOpenStockCrDr.BackColor = Color.Yellow;          
        }

        private void combOpenStockCrDr_Leave(object sender, EventArgs e)
        {
            combOpenStockCrDr.BackColor = Color.White;          
        }

        private void txtClosingStock_Enter(object sender, EventArgs e)
        {
            txtClosingStock.BackColor = Color.Yellow;          
        }

        private void txtClosingStock_Leave(object sender, EventArgs e)
        {
            txtClosingStock.BackColor = Color.White;          
        }

        private void combClosingStockCrDr_Enter(object sender, EventArgs e)
        {
            combClosingStockCrDr.BackColor = Color.Yellow;          
        }

        private void combClosingStockCrDr_Leave(object sender, EventArgs e)
        {
            combClosingStockCrDr.BackColor = Color.White;          
        }

        private void txtReorderQty_Enter(object sender, EventArgs e)
        {
            txtReorderQty.BackColor = Color.Yellow;          
        }

        private void txtReorderQty_Leave(object sender, EventArgs e)
        {
            txtReorderQty.BackColor = Color.White;          
        }

        private void txtMinQty_Enter(object sender, EventArgs e)
        {
            txtMinQty.BackColor = Color.Yellow;          
        }

        private void txtMinQty_Leave(object sender, EventArgs e)
        {
            txtMinQty.BackColor = Color.White;          
        }

        private void txtMaxQty_Enter(object sender, EventArgs e)
        {
            txtMaxQty.BackColor = Color.Yellow;          
        }

        private void txtMaxQty_Leave(object sender, EventArgs e)
        {
            txtMaxQty.BackColor = Color.White;
        }
        #endregion

        #region txtsearch Events
        private void txtsearch_KeyDown(object sender, KeyEventArgs e)
        {
            DtgridMenuItem_KeyDown(sender, e);
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
                mod.txtSearchChange(Chkshowdelet, firstCol, secondCol, tableName, DtgridMenuItem, txtsearch, "Item Name", "Code");
                FillDetails();
                lblSucessMsg.Text = "";

            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        #endregion

        #region Datagrid Events
        private void DtgridMenuItem_CellClick(object sender, DataGridViewCellEventArgs e)
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

        private void DtgridMenuItem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                FillDetails();
                mod.gridDoubleClick(Chkshowdelet, BtnDelete, txtItemname);
                lblSucessMsg.Text = "";
                BtnSave.Enabled = true;

            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void DtgridMenuItem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                mod.gridKeyDown(e, BtnDelete, txtsearch, DtgridMenuItem, this);
                FillDetails();
                if (e.KeyCode == Keys.Enter && Chkshowdelet.Checked == true && DtgridMenuItem.Rows.Count > 0)
                    BtnDelete.Focus();
                else if (e.KeyCode == Keys.Enter && Chkshowdelet.Checked == true && DtgridMenuItem.Rows.Count == 0)
                    BtnExit.Focus();
                else if (e.KeyCode == Keys.Enter && Chkshowdelet.Checked == false && DtgridMenuItem.Rows.Count == 0)
                    BtnExit.Focus();
                lblSucessMsg.Text = "";

            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        #endregion

        public void FillCategory()
        {
            cmbCategry.Items.Clear();
            cmbCategry.Items.Insert(0, "Per Piece");
            cmbCategry.Items.Insert(1, "Kg/ML");
            if (detail_id != 1)
            {
                cmbCategry.SelectedIndex = 0;
            }
        }

        public void FillOpencrdr()
        {
            combOpenStockCrDr.Items.Clear();
            combOpenStockCrDr.Items.Insert(0, "Cr");
            combOpenStockCrDr.Items.Insert(1, "Dr");
            if (detail_id != 1)
            {
                combOpenStockCrDr.SelectedIndex = 0;
            }
        }

        public void FillClosecrdr()
        {
            combClosingStockCrDr.Items.Clear();
            combClosingStockCrDr.Items.Insert(0, "Cr");
            combClosingStockCrDr.Items.Insert(1, "Dr");
            if (detail_id != 1)
            {
                combClosingStockCrDr.SelectedIndex = 0;
            }
        }

        public void FillDetails()
        {
            try
            {
                if (Chkshowdelet.Checked == true)
                {
                    mod.chkDeletedTrue(this, BtnAdd, BtnSave, BtnDelete, DtgridMenuItem, txtsearch);
                    if (DtgridMenuItem.Rows.Count == 0)
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
                    mod.chkDeletedFalse("Item Name", this, BtnAdd, BtnSave, BtnDelete, DtgridMenuItem, txtsearch);
                    if (DtgridMenuItem.Rows.Count == 0)
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
                if (DtgridMenuItem.Rows.Count > 0)
                {
                    int i;
                    detail_id = 1;
                    if (StrUpdate == "U")
                    {
                        i = selectrow;
                    }
                    else
                    {
                        i = DtgridMenuItem.SelectedCells[0].RowIndex;
                    }
                    if (DtgridMenuItem.Rows[i].Cells[1].Value != null)
                    {
                        string ID = mod.Isnull(DtgridMenuItem.Rows[i].Cells[1].Value.ToString(), "");
                        //SqlDataReader dr = mod.GetRecord("SELECT * FROM tbSubItemMaster WHERE Sub_Item_Code='" + s + "' AND Branchcode='" + Globalvariable.bcode + "'");
                        SqlDataReader dr = mod.GetSelectAllField(tableName, firstCol, ID, Globalvariable.company_Srno.ToString());
                        while (dr.Read())
                        {
                            txtcode.Text = "";
                            txtcode.Text = dr["Item_Code"].ToString();
                            txtcode.Enabled = false;
                            txtHSNno.Text = dr["HSN_No"].ToString();
                            txtItemname.Text = mod.Isnull(dr["Item_Name"].ToString(), "");
                            txtGropId.Text = mod.Isnull(dr["Group_Code"].ToString(), "0");
                            SqlDataReader dr1 = mod.SELECT_NAME("tbItemGroup", "tbItemName", txtGropId.Text.ToString(), "Group_Name", "Group_Code", "Branchcode");
                            if (dr1.Read())
                            {
                                lblGroupName.Text = mod.Isnull(dr1["Group_Name"].ToString(), "");
                            }
                            txtPurchaseRate.Text = mod.Isnull(dr["Purchase_Rate"].ToString(), "0");
                            txtSaleRate.Text = mod.Isnull(dr["Sale_Rate"].ToString(), "0");
                            filc = Convert.ToString(dr["Categry"]);
                            if (filc != "")
                            {
                                if (filc == "2")
                                    cmbCategry.Items.Insert(0, "Kg/ML");
                                else if (filc == "1")
                                    cmbCategry.Items.Insert(0, "Per Piece");
                            }
                            cmbCategry.SelectedIndex = 0;
                            FillCategory();                         
                            txtReorderQty.Text = mod.Isnull(dr["Re_OrderQty"].ToString(), "0");
                            txtMinQty.Text = mod.Isnull(dr["Min_Qty"].ToString(), "0");
                            txtMaxQty.Text = mod.Isnull(dr["Max_Qty"].ToString(), "0");
                            SqlDataReader dr_stock = mod.GetSelectAllField("tbStockOpeningCloseBal", "Item_Code", ID, "SrNo");
                            if(dr_stock.Read())
                            {
                                txtOpeningStock.Text = mod.Isnull(dr_stock["Opening_Stock"].ToString(), "");
                               // txtClosingStock.Text = mod.Isnull(dr["Closing_Stock"].ToString(), "");
                                filcrdr = Convert.ToString(dr_stock["OpeningCrDr"]);
                               if (filcrdr != "")
                                {
                                    if (filcrdr == "C")
                                        combOpenStockCrDr.Items.Insert(0, "Cr");
                                    else if (filcrdr == "D")
                                        combOpenStockCrDr.Items.Insert(0, "Dr");
                                }
                               combOpenStockCrDr.SelectedIndex = 0;      
                            }
                        }
                    }
                }
                else
                {
                    mod.txtclear(this.Controls);
                    BtnDelete.Text = "RESET";
                    FillCategory();
                    FillOpencrdr();
                    FillClosecrdr();
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

        #region Add 
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            ADD();
        }

        public void ADD()
        {
            try
            {
                mod.addBtnClick(this, txtcode, tableName, firstCol, BtnSave, BtnDelete, txtHSNno);
                mod.UserAccessibillityMaster("Item Name", BtnAdd, BtnSave, BtnDelete);
                txtcode.Text = "" + mod.GetMaxNum(tableName, firstCol);
                FillCategory();
                FillOpencrdr();
                FillClosecrdr();
                lblSucessMsg.Text = "";
                label1.Text = "Search by Name :";
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        #endregion

        private void btnLessID_Click(object sender, EventArgs e)
        {
            //Less_greaterID("<");

            Models.Common.FormManagement.Instance.SelectRecordFromGrid(DtgridMenuItem, Models.Common.RecordMove.Previous, FillDetails);

        }

        private void btnGreaterID_Click(object sender, EventArgs e)
        {
            //Less_greaterID(">");

            Models.Common.FormManagement.Instance.SelectRecordFromGrid(DtgridMenuItem, Models.Common.RecordMove.Next, FillDetails);

        }

        public void Less_greaterID(string st)
        {
            try
            {
               // string st = "<";
                str = "SELECT * FROM tbItemName WHERE deleted='N' AND Item_Code" + st + "'" + txtcode.Text + "' AND Branchcode='" + Globalvariable.bcode + "'";
                SqlDataReader dr = mod.GetRecord(str);
                //if (dr1.HasRows)
                //{
                //getInfo(str);
                if (dr.Read())
                {
                    //getInfo(str);
                    txtcode.Text = "";
                    txtcode.Text = dr["Item_Code"].ToString();
                    txtcode.Enabled = false;
                    txtHSNno.Text = dr["HSN_No"].ToString();
                    txtItemname.Text = mod.Isnull(dr["Item_Name"].ToString(), "");
                    txtGropId.Text = mod.Isnull(dr["Group_Code"].ToString(), "0");
                    SqlDataReader dr1 = mod.SELECT_NAME("tbItemGroup", "tbItemName", txtcode.Text.ToString(), "Group_Name", "Group_Code", "Branchcode");
                    if (dr1.Read())
                    {
                        lblGroupName.Text = mod.Isnull(dr1["Group_Name"].ToString(), "");
                    }
                    txtPurchaseRate.Text = mod.Isnull(dr["Purchase_Rate"].ToString(), "0");
                    txtSaleRate.Text = mod.Isnull(dr["Sale_Rate"].ToString(), "0");
                    filc = Convert.ToString(dr["Categry"]);
                    if (filc != "")
                    {
                        if (filc == "2")
                            cmbCategry.Items.Insert(0, "Kg/ML");
                        else if (filc == "1")
                            cmbCategry.Items.Insert(0, "Per Piece");
                    }
                    cmbCategry.SelectedIndex = 0;
                    FillCategory();
                    txtReorderQty.Text = mod.Isnull(dr["Re_OrderQty"].ToString(), "0");
                    txtMinQty.Text = mod.Isnull(dr["Min_Qty"].ToString(), "0");
                    txtMaxQty.Text = mod.Isnull(dr["Max_Qty"].ToString(), "0");
                    SqlDataReader dr_stock = mod.GetSelectAllField("tbStockOpeningCloseBal", "Item_Code", txtcode.Text, "SrNo");
                    if (dr_stock.Read())
                    {
                        txtOpeningStock.Text = mod.Isnull(dr_stock["Opening_Stock"].ToString(), "");
                    }
                    // txtClosingStock.Text = mod.Isnull(dr["Closing_Stock"].ToString(), "");
                    string filcrdr = Convert.ToString(dr_stock["OpeningCrDr"]);
                    if (filcrdr != "")
                    {
                        if (filcrdr == "C")
                            combOpenStockCrDr.Items.Insert(0, "Cr");
                        else if (filcrdr == "D")
                            combOpenStockCrDr.Items.Insert(0, "Dr");
                    }
                    combOpenStockCrDr.SelectedIndex = 0;
                }
                else
                {
                    if (st == "<")
                    {
                        MessageBox.Show("First Record!!!");
                    }
                    else if (st == ">")
                    {
                        MessageBox.Show("Last Record!!!");
                    }
                }
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
                        FrmMenuItem_Load(sender, EventArgs.Empty);
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

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            try
            {
                if (DtgridMenuItem.Rows.Count > 0)
                {
                    selectrow = DtgridMenuItem.SelectedCells[0].RowIndex;
                }
                if (txtItemname.Text == "")
                {
                    txtItemname.Focus();
                }
                else if (txtGropId.Text == "")
                {
                    txtGropId.Focus();
                }
                else if (txtPurchaseRate.Text == "0")
                {
                    txtPurchaseRate.Focus();
                }
                else if (txtSaleRate.Text == "0")
                {
                    txtSaleRate.Focus();
                }
                else
                {
                    if (BtnSave.Text == "SAVE")
                        str = "SELECT * FROM tbItemName WHERE Item_Name='" + txtItemname.Text.Replace("'", "''") +
                            "' AND Branchcode='" + Globalvariable.bcode + "'";
                    else if (BtnSave.Text == "UPDATE")
                        str = "SELECT * FROM tbItemName WHERE Item_Name='" + txtItemname.Text.Replace("'", "''") +
                               "' AND Branchcode='" + Globalvariable.bcode + "' AND Item_Code !=" + txtcode.Text + "";
                    dr = mod.GetRecord(str);
                    if (dr.HasRows)
                    {
                        MessageBox.Show("Name Already Exist!!");
                        txtItemname.Text = "";
                        txtItemname.Focus();
                    }
                    else
                    {
                        int i;
                        if (cmbCategry.SelectedItem == "Per Piece")
                            filc = "1";
                        else if (cmbCategry.SelectedItem == "Kg/ML")
                            filc = "2";
                        if (combClosingStockCrDr.SelectedItem == "Cr")
                            filcrdr = "C";
                        else if (combClosingStockCrDr.SelectedItem == "Dr")
                            filcrdr = "D";
                        BtnSave.Enabled = false;
                        SqlCommand cmd = new SqlCommand("SP_FrmMenuItem", con.connect());
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
                        cmd.Parameters.AddWithValue("@code", txtcode.Text);
                        cmd.Parameters.AddWithValue("@HSNno",mod.Isnull(txtHSNno.Text,"0"));
                        cmd.Parameters.AddWithValue("@name", txtItemname.Text.Replace("'", "''"));
                        cmd.Parameters.AddWithValue("@group_Code", mod.Isnull(txtGropId.Text, "0"));
                        cmd.Parameters.AddWithValue("@purchase_rate", mod.Isnull(txtPurchaseRate.Text, "0"));
                        cmd.Parameters.AddWithValue("@sale_rate", mod.Isnull(txtSaleRate.Text, "0"));
                        cmd.Parameters.AddWithValue("@categry", filc);
                        cmd.Parameters.AddWithValue("@reorder_QTY", mod.Isnull(txtReorderQty.Text, "0"));
                        cmd.Parameters.AddWithValue("@min_QTY", mod.Isnull(txtMinQty.Text, "0"));
                        cmd.Parameters.AddWithValue("@max_QTY", mod.Isnull(txtMaxQty.Text, "0"));
                        cmd.Parameters.AddWithValue("@OpeningStock", mod.Isnull(txtOpeningStock.Text, "0"));
                        cmd.Parameters.AddWithValue("@OpeningCrDr", filcrdr);
                        cmd.Parameters.AddWithValue("@companySrno", Globalvariable.company_Srno);
                        cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                        cmd.ExecuteNonQuery();
                        if (BtnSave.Text == "SAVE")
                        {
                            lblSucessMsg.Text = "Save Successfully.....!!";                       
                        }
                        else
                        {
                            lblSucessMsg.Text = "Update Successfully.....!!";                       
                            StrUpdate = "U";
                        }
                       
                        mod.DataGridBind(DtgridMenuItem, tableName, firstCol, secondCol, "N", "Item Name", "Code");
                        // string txt = BtnSave.Text;
                        FillDetails();

                        if (DtgridMenuItem.Rows.Count > 0)
                        {
                            DtgridMenuItem.CurrentCell = DtgridMenuItem[0, selectrow];
                        }
                        StrUpdate = "";
                        txtsearch.Focus();
                       // BtnSave.Enabled = false;
                    }
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
                //mod.chkDeletedChange(Chkshowdelet, firstCol, secondCol, tableName, DtgridRoomCat, txtsearch);
                mod.chkDeletedChange(Chkshowdelet, firstCol, secondCol, tableName, Deleted, "N", DtgridMenuItem, txtsearch, "Item Name", "Code");
                FillDetails();
                mod.UserAccessibillityMaster("Item Name", BtnAdd, BtnSave, BtnDelete);
                lblSucessMsg.Text = "";
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void txtGropId_KeyDown(object sender, KeyEventArgs e)
        {
            Globalvariable.SearchString = "MenuItem";
            var returnForm = Models.Common.FormManagement.Instance.SearchFormOnKeyDown(ref txtGropId, e, "FrmMenuItem", true);
            if (returnForm == null)
            {
                return;
            }
            txtGropId.Text = returnForm.codeselected;
            lblGroupName.Text = returnForm.descselected;
            return;
            try
            {
                Globalvariable.SearchString = "MenuItem";
                if (e.KeyCode == Keys.Enter && txtGropId.Text != "")
                {
                    fillGropID();
                }
                else if (txtGropId.Text == "" && e.KeyCode == Keys.Enter)
                {
                    Globalvariable.searchNo = 1;
                    KeysConverter kc = new KeysConverter();
                    string key = kc.ConvertToString(e.KeyCode);
                    FrmSearch srch = new FrmSearch();
                    sqlda = mod.GetselectQuery("tbItemGroup", "Group_Code", "Group_Name", "N", "N");
                    srch.val = key;
                    srch.fillbackgridSrch(sqlda, "FrmMenuItem");
                    ds.Clear();
                    sqlda.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                        srch.ShowDialog();
                    txtGropId.Text = srch.codeselected;
                    if (srch.codeselected == null)
                    {
                        txtGropId.Focus();
                    }
                    else
                    {
                        fillGropID();
                    }
                    Globalvariable.searchNo = 0;
                    if (lblGroupName.Text != "")
                    {
                        txtItemname.Focus();
                    }
                    else
                    {
                        txtGropId.Focus();
                    }
                }
                else if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
                {
                    txtGropId.Text = "";
                    lblGroupName.Text = "";
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void fillGropID()
        {
            try
            {
                if (txtGropId.Text.Trim().Equals(""))
                {
                    lblGroupName.Text = "";
                }
                else
                {
                    SqlDataReader dr = mod.GetRecord("SELECT Group_Name FROM tbItemGroup WHERE deleted='N' AND Branchcode='" + Globalvariable.bcode +
                        "' AND Group_Code='" + txtGropId.Text + "'");
                    if (dr.HasRows)
                    {
                        dr.Read();
                        lblGroupName.Text = dr["Group_Name"].ToString();
                    }
                    else
                    {
                        txtGropId.Text = "";
                        lblGroupName.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void txtPurchaseRate_TextChanged(object sender, EventArgs e)
        {
            if(txtPurchaseRate.Text!="")
            {
                txtSaleRate.Text = txtPurchaseRate.Text;
            }
        }

        private void txtMaxQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                //BtnSave.Enabled=true;
                BtnSave.Focus();
            }
        }

        private void btngrpBrws_Click(object sender, EventArgs e)
        {
            FrmItemGroup frm = new FrmItemGroup();
            frm.ShowDialog();
            txtGropId.Text = Globalvariable.ItemGrpid;
            lblGroupName.Text = Globalvariable.ItemGrpnm;
        }

        private void FrmMenuItem_Shown(object sender, EventArgs e)
        {
            txtsearch.Focus();
        }
   
    }
}

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
    public partial class FrmItemGroup : Form
    {
        Design ObjDesign = new Design();
        public FrmItemGroup()
        {
            InitializeComponent();
        }
        private BindingSource bindgrid = new BindingSource();
        Module mod = new Module();
        Connection con = new Connection();
        DataGridViewTextBoxEditingControl tb;
        SqlDataAdapter sqlda;
        DataSet ds = new DataSet();
        int flag;
        bool chkDupFlag;
        public Boolean plusKeyPress;
        public string DupCode1 = "0";
        string code = "";
        // public static SqlDataReader srchdr;
        int rowNo, colNo;
        int selectrow;
        string str, StrUpdate;
        string filc;
        string firstCol = "Group_Code";
        string secondCol = "Group_Name";
        string tableName = "tbItemGroup";
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
        private void FrmItemGroup_Load(object sender, EventArgs e)
        {
            try
            {
                ObjDesign.FormDesign(this, DtgridItemGroup);
                ObjDesign.FormDesign(this, TaxDatagrid);
                mod.FormLoad(this, tableName, firstCol, secondCol, Deleted, "N", "N", DtgridItemGroup, txtsearch, Chkshowdelet, "Description", "Code");
                Models.Common.FormManagement.Instance.FillCombo(ref cmbApplyTax, Models.Common.ComboType.YesNo, "N");
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

        public void FillDetails()
        {
            try
            {
                    if (Chkshowdelet.Checked == true)
                {
                    mod.chkDeletedTrue(this, BtnAdd, BtnSave, BtnDelete, DtgridItemGroup, txtsearch);
                    txtsearch.Focus();
                }
                else
                {
                    mod.chkDeletedFalse("Item Group", this, BtnAdd, BtnSave, BtnDelete, DtgridItemGroup, txtsearch);
                }
                if (DtgridItemGroup.Rows.Count > 0)
                {
                    int i;
                    //   FillAllowDesc();
                    //cmbApplyTax.Items.Clear();
                    if (StrUpdate == "U")
                    {
                        i = selectrow;
                    }
                    else
                    {
                        i = DtgridItemGroup.SelectedCells[0].RowIndex;
                    }
                    if (DtgridItemGroup.Rows[i].Cells[1].Value != null)
                    {
                        string ID = mod.Isnull(DtgridItemGroup.Rows[i].Cells[1].Value.ToString(), "");
                        //SqlDataReader dr = mod.GetSelectAllField(tableName, firstCol, ID, "");
                        //string v = "SELECT * FROM RoomCategoryMaster WHERE catgry_code ='" + s + "' and Branchcode='" + Globalvariable.bcode + "'";
                        //SqlCommand cmd = new SqlCommand("SELECT * FROM RoomCategoryMaster WHERE catgry_code ='" + s + "' and Branchcode='" + Globalvariable.bcode + "'", con.connect());
                        //SqlDataReader dr = cmd.ExecuteReader();

                        var dtGroupData = Models.Common.DbConnectivity.Instance.ManageItemGroup("SELECT", ID);
                        if (dtGroupData.Rows.Count > 0)
                        {
                            var dr = dtGroupData.Rows[0];
                            txtcode.Text = dr["Group_Code"].ToString();
                            txtcode.Enabled = false;
                            txtname.Text = dr["Group_Name"].ToString();
                            cmbApplyTax.SelectedValue = dr["ApplyTax"].ToString();
                            //filc = Convert.ToString(dr["ApplyTax"]);
                            //if (filc != "")
                            //{
                            //    if (filc == "Y")
                            //        cmbApplyTax.Items.Insert(0, "Yes");

                            //    else if (filc == "N")
                            //        cmbApplyTax.Items.Insert(0, "No");
                            //}
                            //cmbApplyTax.SelectedIndex = 0;
                            //FillApplytaxYN();
                        }
                        //SELECT name FROM tbTaxMaster WHERE Branchcode='1001' AND tbTaxMaster INNER JOIN tbKitchStockItemGroupTaxdetail ON tbTaxMaster.tax_code=tbKitchStockItemGroupTaxdetail.tax_code WHERE tbKitchStockItemGroupTaxdetail.tax_code=1
                        // SqlDataReader dr1 = mod.SELECT_NAME("tbTaxMaster", "tbKitchStockItemGroupTaxdetail", txtcode.Text.ToString(), "name", "tax_code", "Branchcode",firstCol);
                        // SqlDataReader dr1 = mod.SELECT_NAME("tbTaxMaster", "tbKitchStockItemGroupTaxdetail", txtcode.Text.ToString(), "name", "tax_code", "Branchcode");                              
                        //SqlCommand cmd = new SqlCommand("SP_FrmItemGroup", con.connect());
                        //    cmd.CommandType = CommandType.StoredProcedure;
                        //    cmd.Parameters.AddWithValue("@variable", "SELECT_Tax_Name");
                        //    cmd.Parameters.AddWithValue("@code", txtcode.Text);                  
                        //    cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                        //    SqlDataReader dr1 = cmd.ExecuteReader();       
                        //TaxDatagrid.Rows.Clear();
                        //i = 0;
                        //    while (dr1.Read())
                        //    {
                        //        TaxDatagrid.Rows.Add();
                        //        TaxDatagrid.Rows[i].Cells[0].Value = i + 1;//dr["Item_code"].ToString();                            
                        //        TaxDatagrid.Rows[i].Cells[1].Value = dr1["tax_code"].ToString();
                        //        string[] rstr;
                        //        TaxDatagrid.Rows[i].Cells[2].Value = dr1["name"].ToString();
                        //        i = i + 1;
                        //    }
                        //    

                        TaxDatagrid.Rows.Clear();
                        var dtTaxData = Models.Common.DbConnectivity.Instance.ManageItemGroup("SELECT_Tax_Name", txtcode.Text);
                        for (int ti = 0; ti < dtTaxData.Rows.Count; ti++)
                        {
                            var drTax = dtTaxData.Rows[ti];
                            TaxDatagrid.Rows.Add();
                            TaxDatagrid.Rows[ti].Cells[0].Value = ti + 1;//dr["Item_code"].ToString();                            
                            TaxDatagrid.Rows[ti].Cells[1].Value = drTax["tax_code"].ToString();
                            TaxDatagrid.Rows[ti].Cells[2].Value = drTax["name"].ToString();
                            TaxDatagrid.Rows[ti].Cells[3].Value = Convert.ToDecimal(drTax["Tax_Percentage"]).ToString("#0.00");

                        }
                    }
                    mod.UserAccessibillityMaster("Item Group", BtnAdd, BtnSave, BtnDelete);
                }
                else
                {
                    mod.txtclear(this.Controls);
                    TaxDatagrid.Rows.Clear();
                    //FillApplytaxYN();
                    cmbApplyTax.SelectedValue = "N";
                    mod.UserAccessibillityMaster("Item Group", BtnAdd, BtnSave, BtnDelete);
                    BtnDelete.Text = "RESET";
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        #region TextBox Back Color
        private void txtsearch_Enter(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.Yellow;
        }

        private void txtsearch_Leave(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.White;
        }
        private void txtname_Enter(object sender, EventArgs e)
        {
            txtname.BackColor = Color.Yellow;
        }
        private void txtname_Leave(object sender, EventArgs e)
        {
            txtname.BackColor = Color.White;
        }

        private void comballowdesc_Enter(object sender, EventArgs e)
        {
            cmbApplyTax.BackColor = Color.Yellow;
        }

        private void comballowdesc_Leave(object sender, EventArgs e)
        {
            cmbApplyTax.BackColor = Color.White;
        }

        #endregion

        private void FrmItemGroup_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (ActiveControl.Name.Equals("TaxDatagrid"))
                {
                    if (e.KeyCode == Keys.Add)
                    {
                        if (TaxDatagrid.Rows.Count > 0)
                        {

                            if (Convert.ToString(TaxDatagrid.Rows[TaxDatagrid.CurrentCell.RowIndex].Cells[1].Value) == string.Empty)
                            {
                                if (rowNo >= TaxDatagrid.CurrentCell.RowIndex)
                                {
                                    rowNo--;
                                }

                                TaxDatagrid.Rows.RemoveAt(TaxDatagrid.CurrentCell.RowIndex);

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

        private void FrmItemGroup_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Control nextTab;
                if (e.KeyChar == (char)Keys.Enter)
                {
                    nextTab = ((Control)sender);
                    //  FillDetails();
                    //   int i = DtgridRoomCat.SelectedCells[0].RowIndex;
                    if (ActiveControl.Name == "TaxDatagrid" && TaxDatagrid.RowCount > 0)
                    {
                        rowNo = TaxDatagrid.CurrentCell.RowIndex;
                        colNo = TaxDatagrid.CurrentCell.ColumnIndex;
                        if (TaxDatagrid.Rows[rowNo].Cells[colNo].Value != null && TaxDatagrid.Rows[rowNo].Cells[colNo].Value.ToString() != "")
                        {
                            if (colNo == 0)
                            {
                                if (plusKeyPress == true)
                                {
                                    plusKeyPress = false;
                                }
                                else if (TaxDatagrid.Rows[rowNo].Cells[0].Value != null)
                                {
                                    TaxDatagrid.CurrentCell = TaxDatagrid[1, rowNo];
                                    TaxDatagrid.BeginEdit(true);
                                }
                            }
                            else if (colNo == 1)
                            {
                                if (TaxDatagrid.Rows[rowNo].Cells[1].Value != null)
                                {
                                    TaxDatagrid.CurrentCell = TaxDatagrid[1, rowNo];
                                    TaxDatagrid.BeginEdit(true);
                                }
                            }
                            else if (colNo == 2)
                            {
                                TaxDatagrid.CurrentCell = TaxDatagrid[3, rowNo];
                                TaxDatagrid.BeginEdit(true);
                            }
                        }
                        else
                        {
                            TaxDatagrid.CurrentCell = TaxDatagrid[colNo, rowNo];
                            TaxDatagrid.BeginEdit(true);
                        }
                    }
                    else if (ActiveControl.Name == "txtsearch" && DtgridItemGroup.Rows.Count == 0 && e.KeyChar == (char)Keys.Enter)
                    {
                        txtsearch.Focus();
                    }
                    else
                    {
                        if (ActiveControl.Name == "txtsearch" && DtgridItemGroup.Rows.Count == 0 && txtsearch.Text == "")
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

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            ADD();
        }

        public void ADD()
        {
            try
            {
                mod.addBtnClick(this, txtcode, tableName, firstCol, BtnSave, BtnDelete, txtname);
                mod.UserAccessibillityMaster("Item Group", BtnAdd, BtnSave, BtnDelete);
                flag = 0;
                txtcode.Text = "" + mod.GetMaxNum(tableName, firstCol);
                TaxDatagrid.Rows.Clear();
                //FillApplytaxYN();
                lblSucessMsg.Text = "";
                label1.Text = "Search by Name :";
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        //public void FillApplytaxYN()
        //{
        //    try
        //    {
        //        cmbApplyTax.Items.Clear();
        //        cmbApplyTax.Items.Insert(0, "SELECT");
        //        cmbApplyTax.Items.Insert(1, "Yes");
        //        cmbApplyTax.Items.Insert(2, "No");
        //        //  cmbApplyTax.SelectedIndex = 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        string str = "Message:" + ex.Message;
        //        MessageBox.Show(str, "Error Message");
        //    }
        //}

        #region txtsearch events
        private void txtsearch_KeyDown(object sender, KeyEventArgs e)
        {
            DtgridItemGroup_KeyDown(sender, e);
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
                mod.txtSearchChange(Chkshowdelet, firstCol, secondCol, tableName, DtgridItemGroup, txtsearch, "Item Group Name", "Code");
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

        private void DtgridItemGroup_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                mod.gridKeyDown(e, BtnDelete, txtsearch, DtgridItemGroup, this);
                FillDetails();
                if (e.KeyCode == Keys.Enter && Chkshowdelet.Checked == true && DtgridItemGroup.Rows.Count > 0)
                    BtnDelete.Focus();
                else if (e.KeyCode == Keys.Enter && Chkshowdelet.Checked == true && DtgridItemGroup.Rows.Count == 0)
                    BtnExit.Focus();
                lblSucessMsg.Text = "";

            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void DtgridItemGroup_CellClick(object sender, DataGridViewCellEventArgs e)
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

        private void DtgridItemGroup_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                FillDetails();
                //   comballowdesc.Items.Clear();
                mod.gridDoubleClick(Chkshowdelet, BtnDelete, txtname);
                lblSucessMsg.Text = "";
                BtnSave.Enabled = true;
                flag = 1;
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
                        FrmItemGroup_Load(sender, EventArgs.Empty);
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

        private void cmbApplyTax_TextChanged(object sender, EventArgs e)
        {
            if (cmbApplyTax.SelectedValue.ToString() == "Y")
            {
                TaxDatagrid.Enabled = true;
            }
            else
            {
                TaxDatagrid.Enabled = false;
            }
        }

        private void cmbApplyTax_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (cmbApplyTax.SelectedValue.ToString() == "Y")
                {
                    TaxDatagrid.Enabled = true;

                    if ((e.KeyChar == (char)Keys.Enter))
                    {
                        if (flag == 0)
                        {
                            TaxDatagrid.Rows.Clear();
                            TaxDatagrid.Enabled = true;
                            TaxDatagrid.Rows.Add();
                            rowNo = TaxDatagrid.RowCount - 1;
                            TaxDatagrid.Rows[rowNo].Cells[0].Value = TaxDatagrid.Rows.Count;
                            TaxDatagrid.CurrentCell = TaxDatagrid[1, 0];
                            TaxDatagrid.BeginEdit(true);
                        }
                        else if (flag == 1)
                        {
                            int val;
                            TaxDatagrid.Enabled = true;
                            if (TaxDatagrid.Rows.Count == 0)
                            {
                                TaxDatagrid.Rows.Add();
                                rowNo = TaxDatagrid.RowCount - 1;
                                if (rowNo == 0)
                                {
                                    val = rowNo;
                                }
                                else
                                {
                                    val = rowNo - 1;
                                }
                                if (TaxDatagrid.Rows[val].Cells[1].Value != null)
                                {
                                    TaxDatagrid.Rows[rowNo].Cells[0].Value = TaxDatagrid.Rows.Count;
                                    TaxDatagrid.CurrentCell = TaxDatagrid[1, rowNo];
                                    TaxDatagrid.BeginEdit(true);
                                }
                                else
                                {
                                    if (rowNo > val)
                                    {
                                        TaxDatagrid.Rows.Remove(TaxDatagrid.Rows[rowNo]);
                                    }
                                    TaxDatagrid.Rows[rowNo].Cells[0].Value = TaxDatagrid.Rows.Count;
                                    TaxDatagrid.CurrentCell = TaxDatagrid[1, val];
                                    TaxDatagrid.BeginEdit(true);
                                }
                            }
                            else if (TaxDatagrid.Rows.Count > 0)
                            {
                                TaxDatagrid.CurrentCell = TaxDatagrid[1, 0];
                                TaxDatagrid.BeginEdit(true);
                            }
                        }
                    }
                }
                else if (cmbApplyTax.SelectedValue.ToString() == "N")
                {
                    TaxDatagrid.Enabled = false;
                    BtnSave.Focus();
                }
                else
                {
                    cmbApplyTax.Focus();
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
        private void Save()
        {
            try
            {
                if (DtgridItemGroup.Rows.Count > 0)
                {
                    selectrow = DtgridItemGroup.SelectedCells[0].RowIndex;
                }
                if (txtname.Text == "")
                {
                    txtname.Focus();
                }
                //else if (cmbApplyTax.SelectedValue == 0)
                //{
                //    cmbApplyTax.Focus();
                //}
                else
                {
                    if (BtnSave.Text == "SAVE")
                        str = "SELECT * FROM tbItemGroup WHERE Group_Name='" + txtname.Text.Replace("'", "''") +
                            "' AND Branchcode='" + Globalvariable.bcode + "'";
                    else if (BtnSave.Text == "UPDATE")
                        str = "SELECT * FROM tbItemGroup WHERE Group_Name='" + txtname.Text.Replace("'", "''") +
                               "' AND Branchcode='" + Globalvariable.bcode + "' AND Group_Code !=" + txtcode.Text + "";
                    dr = mod.GetRecord(str);
                    if (dr.HasRows)
                    {
                        MessageBox.Show("Name Already Exist!!");
                        txtname.Text = "";
                        txtname.Focus();
                    }
                    else
                    {
                        //string ApplyTaxYN = cmbApplyTax.SelectedValue.ToString();
                        //int i;
                        //if (cmbApplyTax.SelectedItem == "Yes")
                        //    ApplyTaxYN = "Y";
                        BtnSave.Enabled = false;
                        SqlCommand cmd = new SqlCommand("SP_FrmItemGroup", con.connect());
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
                        cmd.Parameters.AddWithValue("@name", txtname.Text.Replace("'", "''"));
                        cmd.Parameters.AddWithValue("@ApplyTax", cmbApplyTax.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                        cmd.ExecuteNonQuery();
                        if (BtnSave.Text == "SAVE")
                        {
                            lblSucessMsg.Text = "Save Successfully.....!!";
                        }
                        else
                        {
                            lblSucessMsg.Text = "Update Successfully.....!!";
                            str = "DELETE FROM tbKitchStockItemGroupTaxdetail WHERE Group_Code=" + txtcode.Text + " AND Branchcode='" + Globalvariable.bcode + "'";
                            mod.Executequery(str);
                            StrUpdate = "U";
                        }
                        for (int cnt = 0; cnt < TaxDatagrid.Rows.Count; cnt++)
                        {
                            if (TaxDatagrid.Rows[cnt].Cells[0].Value == null)
                                TaxDatagrid.Rows.RemoveAt(cnt);

                            else
                            {
                                SqlCommand cmdRoomTax = new SqlCommand("SP_FrmItemGroup", con.connect());
                                cmdRoomTax.CommandType = CommandType.StoredProcedure;
                                cmdRoomTax.Parameters.AddWithValue("@variable", "INSERT_Tax");
                                cmdRoomTax.Parameters.AddWithValue("@code", txtcode.Text);
                                cmdRoomTax.Parameters.AddWithValue("@TaxCode", TaxDatagrid.Rows[cnt].Cells[1].Value);
                                cmdRoomTax.Parameters.AddWithValue("@TaxPercentage", TaxDatagrid.Rows[cnt].Cells[3].Value);
                                cmdRoomTax.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                                cmdRoomTax.ExecuteNonQuery();
                            }
                        }
                        mod.DataGridBind(DtgridItemGroup, tableName, firstCol, secondCol, "N", "Item Group Name", "Code");
                        // string txt = BtnSave.Text;
                        FillDetails();

                        if (DtgridItemGroup.Rows.Count > 0)
                        {
                            DtgridItemGroup.CurrentCell = DtgridItemGroup[0, selectrow];
                        }
                        StrUpdate = "";
                        txtsearch.Focus();
                        BtnSave.Enabled = false;
                        if (BtnSave.Text == "SAVE")
                        {
                            if (Globalvariable.frmName == "ItemGroup")
                            {
                                Globalvariable.ItemGrpid = txtcode.Text;
                                Globalvariable.ItemGrpnm = txtname.Text;
                                this.Close();
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

        private void Chkshowdelet_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //mod.chkDeletedChange(Chkshowdelet, firstCol, secondCol, tableName, DtgridRoomCat, txtsearch);
                mod.chkDeletedChange(Chkshowdelet, firstCol, secondCol, tableName, Deleted, "N", DtgridItemGroup, txtsearch, "Item Group Name", "Code");
                FillDetails();
                mod.UserAccessibillityMaster("Item Group", BtnAdd, BtnSave, BtnDelete);
                lblSucessMsg.Text = "";
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void TaxDatagrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            TaxDatagrid[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.Yellow;
        }

        private void TaxDatagrid_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            TaxDatagrid[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.White;
        }

        private void TaxDatagrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
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

        private void Control_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                lblSucessMsg.Text = "";
                BtnSave.Enabled = false;
                DupCode1 = "0";
                rowNo = TaxDatagrid.CurrentCell.RowIndex;
                colNo = TaxDatagrid.CurrentCell.ColumnIndex;
                if (colNo == 1)
                {
                    if (e.KeyCode == Keys.Back)
                    {
                        TaxDatagrid.Rows[rowNo].Cells[1].Value = null;
                    }
                    else if (e.KeyCode == Keys.Add)
                    {
                        if (TaxDatagrid.RowCount > 0)
                        {
                            //plusKeyPress = true;
                            int i = TaxDatagrid.CurrentCell.RowIndex;
                            if (Convert.ToString(TaxDatagrid.Rows[i].Cells[1].Value) == string.Empty)
                            {
                                if (rowNo >= TaxDatagrid.CurrentCell.RowIndex)
                                {
                                    rowNo--;
                                }
                                TaxDatagrid.Rows.RemoveAt(i);
                            }
                            BtnSave.Enabled = true;
                            BtnSave.Focus();
                        }
                    }
                    else if (e.KeyCode == Keys.Enter)
                    {
                        //  DupCode1 = "0";
                        DupCode1 = mod.DuplicateRecord(TaxDatagrid, 1, rowNo);
                        Globalvariable.DuplicateValue = DupCode1;

                        TaxDatagrid.Rows[rowNo].Cells[1].Value = tb.Text;

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

                        TaxDatagrid.ClearSelection();

                        TaxDatagrid.Rows[rowNo].Cells[1].Value = mod.Isnull(returnForm.codeselected, "");
                        TaxDatagrid.Rows[rowNo].Cells[2].Value = mod.Isnull(returnForm.descselected, "");

                        TaxDatagrid.CurrentCell = TaxDatagrid[3, rowNo];

                    }
                }

                else if (colNo == 3)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        var taxGridRow = TaxDatagrid.Rows[rowNo];
                        if (tb.Text != string.Empty)
                        {
                            if (taxGridRow.Cells[1].Value == null || taxGridRow.Cells[1].Value == string.Empty || taxGridRow.Cells[2].Value == string.Empty)
                            {
                                TaxDatagrid.CurrentCell = TaxDatagrid[1, rowNo];
                                Globalvariable.searchNo = 0;
                                return;
                            }
                            if (TaxDatagrid.Rows[rowNo].Cells[3].Value != null || tb.Text != string.Empty)
                            {
                                int rowcount = TaxDatagrid.Rows.Count;
                                if (rowNo == rowcount - 1)
                                {
                                    // int val;                           
                                    rowNo = TaxDatagrid.RowCount - 1;
                                    if (TaxDatagrid.Rows[rowNo].Cells[1].Value != null)
                                    {
                                        TaxDatagrid.Rows.Add();
                                        rowNo = TaxDatagrid.RowCount - 1;
                                        TaxDatagrid.Rows[rowNo].Cells[0].Value = TaxDatagrid.Rows.Count;
                                        TaxDatagrid.CurrentCell = TaxDatagrid[1, rowNo];
                                    }
                                    else
                                    {
                                        TaxDatagrid.Rows[rowNo].Cells[0].Value = TaxDatagrid.Rows.Count;
                                        TaxDatagrid.CurrentCell = TaxDatagrid[1, rowNo];
                                    }
                                }
                                else
                                {
                                    rowNo++;
                                    TaxDatagrid.CurrentCell = TaxDatagrid[1, rowNo];

                                }
                            }
                        }
                        else
                        {
                            TaxDatagrid.CurrentCell = TaxDatagrid[1, rowNo];

                        }
                    }

                }
                Globalvariable.searchNo = 0;
                //  e.IsInputKey = true;
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
                if(TaxDatagrid.CurrentCell != null && TaxDatagrid.CurrentCell.ColumnIndex==3)
                {
                    if(e.KeyChar.ToString()=="." )
                    {
                        if(obj.Text.Trim().Length == 0 || obj.Text.Contains("."))
                        {
                            e.Handled = true;
                            return;
                        }

                        

                    }
                    else if (!Char.IsNumber(e.KeyChar) && (Keys)e.KeyChar != Keys.Back)  // check if backspace is pressed
                    {
                        e.Handled = true;
                    }

                    if ((Char.IsNumber(e.KeyChar) || e.KeyChar.ToString()==".") && Convert.ToDecimal(obj.Text + e.KeyChar.ToString()) >= 100)
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

        private void btnLessID_Click(object sender, EventArgs e)
        {

            Models.Common.FormManagement.Instance.SelectRecordFromGrid(DtgridItemGroup, Models.Common.RecordMove.Previous, FillDetails);

        }

        private void btnGreaterID_Click(object sender, EventArgs e)
        {

            Models.Common.FormManagement.Instance.SelectRecordFromGrid(DtgridItemGroup, Models.Common.RecordMove.Next, FillDetails);

        }

        private void clearCol()
        {
            for (int cnt = 1; cnt < TaxDatagrid.ColumnCount; cnt++)
            {
                TaxDatagrid.Rows[TaxDatagrid.CurrentCell.RowIndex].Cells[cnt].Value = "";
            }
        }

        private void FrmItemGroup_Shown(object sender, EventArgs e)
        {
            txtsearch.Focus();
        }


    }
}

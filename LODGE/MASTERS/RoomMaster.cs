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
    public partial class RoomMaster : Form
    {
        Design ObjDesign = new Design();
        public RoomMaster()
        {
            InitializeComponent();
        }

        Connection con = new Connection();

        SqlCommand cmd;
        SqlDataReader sqldr;
        SqlDataAdapter sqlda;
        DataSet ds = new DataSet();
        // static int rowNum = 0, colNum = 0;
        int flag, selectrow;
        public static SqlDataReader srchdr;
        int rowNo, colNo;
        //  int dupRowNo;
        //    public static int searchNo;
        string code = "", str, StrUpdate;
        public string DupCode1 = "0";
        Module mod = new Module();
        string firstCol = "room_no";
        string secondCol = "room_cat_name";
        string tableName = "tbRoomMaster";
        string Deleted = "deleted";
        DataGridViewTextBoxEditingControl tb;
        bool chkDupFlag;
        public Boolean plusKeyPress;
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

        private void RoomMaster_Load(object sender, EventArgs e)
        {
            try
            {
                ObjDesign.FormDesign(this, DtgridRoomCat);
                flag = 1;
                LoadEvent();
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }



        public SqlDataAdapter SearchTax(string searchStr)
        {
            SqlCommand cmd;
            cmd = new SqlCommand("SP_SEARCHFRMTextChanged", con.connect());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@frmname", "RoomMaster");
            cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
            cmd.Parameters.AddWithValue("@tableName", "tbTaxMaster");
            cmd.Parameters.AddWithValue("@text", searchStr);
            cmd.Parameters.AddWithValue("@dupcode", DupCode1);
            cmd.Parameters.AddWithValue("@SearchChangeVariable", Globalvariable.SearchChangeVariable);
            // cmd.Parameters.AddWithValue("@searchno", Globalvariable.searchNo);
            //   sqlda = cmd.ExecuteReader();
            sqlda = new SqlDataAdapter(cmd);
            return sqlda;
        }

        public void ADD()
        {
            try
            {
                mod.addBtnClick(this, txtRoomNo, tableName, firstCol, BtnSave, BtnDelete, txtRomCategry);
                mod.UserAccessibillityMaster("Create Room", BtnAdd, BtnSave, BtnDelete);
                txtSingleRate.BackColor = Color.White;
                txtDoubleRate.BackColor = Color.White;
                txtRoomNo.Enabled = true;
                txtRoomNo.Focus();
                //  maxID();
                txtRoomNo.Text = "" + mod.GetMaxNum(tableName, firstCol);
                lblSucessMsg.Text = "";
                label1.Text = "Search by Name :";
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }
        public void maxID()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT (ISNULL(MAX(room_no),0)+1) FROM tbRoomMaster WHERE Branchcode='" + Globalvariable.bcode + "'", con.connect());
                cmd.ExecuteNonQuery();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                txtRoomNo.Text = "" + count;
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
                mod.FormLoad(this, tableName, firstCol, secondCol, Deleted, "N", "N", DtgridRoomCat, txtsearch, ChkDeleted, "Description", "Code");
                txtsearch.Focus();
                FillDetails();
                mod.UserAccessibillityMaster("Create Room", BtnAdd, BtnSave, BtnDelete);
                Globalvariable.SearchChangeVariable = "SrchByName";
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private string GetCatName(string cno)
        {
            SqlDataReader dr1 = mod.GetRecord("SELECT * FROM RoomCategoryMaster WHERE deleted='N' AND Branchcode='" + Globalvariable.bcode +
                                   "' AND catgry_code='" + cno + "'");
            if (dr1.HasRows)
            {
                dr1.Read();
                return (dr1["catgry_name"].ToString());
            }
            else
            {
                return ("");
            }
        }

        public void chkDeletedTrue()
        {
            mod.CntrlEnable(this.Controls);
            BtnAdd.Visible = false;
            BtnSave.Visible = false;
            BtnDelete.Text = "RECALL";
            if (DtgridRoomCat.Rows.Count == 0)
                BtnDelete.Enabled = false;
            else
                BtnDelete.Enabled = true;
            txtsearch.Enabled = true;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                flag = 0;
                ADD();
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void txtsearch_Enter(object sender, EventArgs e)
        {

        }

        private void txtRomCategry_Enter(object sender, EventArgs e)
        {
            txtRomCategry.BackColor = Color.Yellow;

        }

        private void txtSingleRate_Enter(object sender, EventArgs e)
        {
            txtSingleRate.BackColor = Color.Yellow;
        }

        private void txtDoubleRate_Enter(object sender, EventArgs e)
        {
            txtDoubleRate.BackColor = Color.Yellow;

        }
        public void FillDetails()
        {
            try
            {
                if (ChkDeleted.Checked == true)
                {
                    chkDeletedTrue();
                    txtsearch.Focus();
                }
                else
                {
                    mod.chkDeletedFalse("Create Room", this, BtnAdd, BtnSave, BtnDelete, DtgridRoomCat, txtsearch);
                }
                if (DtgridRoomCat.Rows.Count > 0)
                {
                    int i;
                    if (StrUpdate == "U")
                    {
                        i = selectrow;
                    }
                    else
                    {
                        i = DtgridRoomCat.SelectedCells[0].RowIndex;
                    }
                    if (DtgridRoomCat.Rows[i].Cells[1].Value != null)
                    {
                        string ID = mod.Isnull(DtgridRoomCat.Rows[i].Cells[1].Value.ToString(), "");
                        SqlDataReader dr = mod.GetSelectAllField(tableName, firstCol, ID, "");
                        while (dr.Read())
                        {
                            txtRoomNo.Text = dr["room_no"].ToString();
                            txtRomCategry.Text = dr["room_cat_no"].ToString();
                            lblRoomCatName.Text = GetCatName(txtRomCategry.Text.ToString());
                            txtSingleRate.Text = dr["nrate_1"].ToString();
                            txtDoubleRate.Text = dr["nrate_2"].ToString();
                        }
                        txtRoomNo.Enabled = false;
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


        private string[] GetTaxName(string tno)
        {
            string[] rtstr = new string[2];
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


        public void CloseIt()
        {
            System.Threading.Thread.Sleep(300);
            Microsoft.VisualBasic.Interaction.AppActivate(
                 System.Diagnostics.Process.GetCurrentProcess().Id);
            System.Windows.Forms.SendKeys.SendWait(" ");
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
        private void Save()
        {
            try
            {
                if (DtgridRoomCat.Rows.Count > 0)
                {
                    selectrow = DtgridRoomCat.SelectedCells[0].RowIndex;
                }
                if (txtRoomNo.Text == "")
                    txtRoomNo.Focus();
                else if (txtRomCategry.Text == "")
                    txtRomCategry.Focus();
                else
                {
                    SqlDataReader dr = mod.GetRecord("SELECT * FROM RoomCategoryMaster WHERE catgry_code='" + txtRomCategry.Text +
                        "' AND Branchcode='" + Globalvariable.bcode + "'");
                    if (!dr.HasRows)
                    {
                        txtRomCategry.Text = "";
                        lblRoomCatName.Text = "";
                        txtRomCategry.Focus();
                    }
                    else
                    {

                        str = "SELECT * FROM " + tableName + " WHERE " + firstCol + "='" + txtRoomNo.Text +
                                  "' AND Branchcode='" + Globalvariable.bcode + "'";
                        dr = mod.GetRecord(str);
                        if (dr.HasRows && BtnSave.Text == "SAVE")
                        {
                            MessageBox.Show("Room No Already Exist!!");
                            txtRoomNo.Text = "";
                            txtRoomNo.Focus();
                        }
                        else
                        {
                            if (BtnSave.Text == "UPDATE")
                            {
                                DataSet ds = new DataSet();
                                str = "SELECT * FROM " + tableName + " WHERE " + firstCol + "='" + txtRoomNo.Text +
                                                                     "' AND Branchcode='" + Globalvariable.bcode + "'";
                                dr = mod.GetRecord(str);
                                while (dr.Read())
                                {
                                    mod.compare(txtSingleRate.Text, dr["nrate_1"].ToString(), "Create Room");
                                    mod.compare(txtDoubleRate.Text, dr["nrate_2"].ToString(), "Create Room");
                                    mod.compare(txtRomCategry.Text, dr["room_cat_no"].ToString(), "Create Room");
                                }
                            }
                            SqlCommand cmd = new SqlCommand("SP_INSERTRoomMaster", con.connect());
                            cmd.CommandType = CommandType.StoredProcedure;
                            if (BtnSave.Text == "SAVE")
                            {
                                txtRoomNo.Text = "" + mod.GetMaxNum(tableName, firstCol);
                                cmd.Parameters.AddWithValue("@variable", "INSERT");
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@variable", "UPDATE");
                            }
                            cmd.Parameters.AddWithValue("@code", txtRoomNo.Text);
                            cmd.Parameters.AddWithValue("@catgryNo", txtRomCategry.Text);
                            cmd.Parameters.AddWithValue("@name", lblRoomCatName.Text.Replace("'", "''"));
                            cmd.Parameters.AddWithValue("@rate1", mod.Isnull(txtSingleRate.Text, "0"));
                            cmd.Parameters.AddWithValue("@rate2", mod.Isnull(txtDoubleRate.Text, "0"));
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
                            mod.DataGridBind(DtgridRoomCat, tableName, firstCol, secondCol, "N", "Room Category Name", "Room No.");
                            string txt = BtnSave.Text;
                            FillDetails();
                            int s = selectrow;
                            if (DtgridRoomCat.Rows.Count > 0)
                            {
                                DtgridRoomCat.CurrentCell = DtgridRoomCat[0, s];
                            }
                            StrUpdate = "";
                            BtnSave.Enabled = false;
                            txtsearch.Focus();
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

        private void itemMastGrid_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                /*  e.SuppressKeyPress = true;
                  if (e.KeyCode == Keys.Enter)
                      e.Handled = true;*/
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
                // mod.chkDeletedChange(ChkDeleted, firstCol, secondCol, tableName, DtgridRoomCat, txtsearch);
                if (ChkDeleted.Checked == true)
                {
                    str = "SELECT RoomCategoryMaster.catgry_name,tbRoomMaster.room_no FROM tbRoomMaster,RoomCategoryMaster WHERE  RoomCategoryMaster.catgry_code=tbRoomMaster.room_cat_no AND tbRoomMaster.deleted='Y' AND tbRoomMaster.Branchcode='" + Globalvariable.bcode + "' ORDER BY tbRoomMaster.room_no";
                }
                else if (ChkDeleted.Checked == false)
                {
                    str = "SELECT RoomCategoryMaster.catgry_name,tbRoomMaster.room_no FROM tbRoomMaster,RoomCategoryMaster WHERE  RoomCategoryMaster.catgry_code=tbRoomMaster.room_cat_no AND tbRoomMaster.deleted='N' AND tbRoomMaster.Branchcode='" + Globalvariable.bcode + "' ORDER BY tbRoomMaster.room_no";

                    //str = "SELECT RoomCategoryMaster.catgry_name,tbRoomMaster.room_no FROM tbRoomMaster,RoomCategoryMaster WHERE tbRoomMaster.room_cat_no=RoomCategoryMaster.catgry_code AND tbRoomMaster.deleted='N' AND RoomCategoryMaster.deleted='N' AND tbRoomMaster.Branchcode='" + Globalvariable.bcode + "' ORDER BY tbRoomMaster.room_no";
                }
                mod.fillgrid(str, DtgridRoomCat, "Room Category Name", "Room No.");
                FillDetails();
                txtsearch.Text = "";
                txtsearch.Focus();
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }




        private void txtsearch_KeyDown(object sender, KeyEventArgs e)
        {
            DtgridRoomCat_KeyDown(sender, e);
        }

        private void DtgridRoomCat_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                mod.gridKeyDown(e, BtnDelete, txtsearch, DtgridRoomCat, this);
                FillDetails();
                if (e.KeyCode == Keys.Enter && ChkDeleted.Checked == true && DtgridRoomCat.Rows.Count > 0)
                    BtnDelete.Focus();
                else if (e.KeyCode == Keys.Enter && ChkDeleted.Checked == true && DtgridRoomCat.Rows.Count == 0)
                    BtnExit.Focus();
                lblSucessMsg.Text = "";

            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtRomCategry_KeyDown(object sender, KeyEventArgs e)
        {
            Globalvariable.SearchString = "RoomCategory";
            var returnForm = Models.Common.FormManagement.Instance.SearchFormOnKeyDown(ref txtRomCategry, e, "RoomMaster", true);
            if (returnForm == null)
            {
                return;
            }
            txtRomCategry.Text = returnForm.codeselected;
            lblRoomCatName.Text = returnForm.descselected;
           

            return;
            try
            {
                Globalvariable.SearchString = "RoomCatgry";
                if (e.KeyCode == Keys.Enter && txtRomCategry.Text != "")
                {
                    fillRoomCat();
                }
                else if (txtRomCategry.Text == "" && e.KeyCode == Keys.Enter)
                {
                    Globalvariable.searchNo = 1;
                    KeysConverter kc = new KeysConverter();
                    string key = kc.ConvertToString(e.KeyCode);
                    FrmSearch srch = new FrmSearch();
                    // str = "SELECT catgry_name,catgry_code FROM RoomCategoryMaster WHERE deleted='N'AND Branchcode='" + Globalvariable.bcode + "'";
                    sqlda = mod.GetselectQuery("RoomCategoryMaster", "catgry_code", "catgry_name", "N", "N");
                    srch.val = key;
                    srch.fillbackgridSrch(sqlda, "RoomMaster");
                    ds.Clear();
                    sqlda.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                        srch.ShowDialog();
                    txtRomCategry.Text = srch.codeselected;
                    if (srch.codeselected == null)
                    {
                        txtRomCategry.Focus();
                    }
                    else
                    {
                        fillRoomCat();
                    }
                    Globalvariable.searchNo = 0;
                    if (lblRoomCatName.Text != "")
                    {
                        txtSingleRate.Focus();
                    }
                    else
                    {
                        txtRomCategry.Focus();
                    }
                }
                else if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
                {
                    txtRomCategry.Text = "";
                    lblRoomCatName.Text = "";
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void fillRoomCat()
        {
            try
            {
                if (txtRomCategry.Text.Trim().Equals(""))
                {
                    lblRoomCatName.Text = "";
                }
                else
                {
                    SqlDataReader dr = mod.GetRecord("SELECT catgry_name FROM RoomCategoryMaster WHERE deleted='N' AND Branchcode='" + Globalvariable.bcode +
                        "' AND catgry_code='" + txtRomCategry.Text + "'");
                    if (dr.HasRows)
                    {
                        dr.Read();
                        lblRoomCatName.Text = dr["catgry_name"].ToString();
                        //   txtSingleRate.Focus();
                    }
                    else
                    {
                        txtRomCategry.Text = "";
                        lblRoomCatName.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void textGotFocus(System.Windows.Forms.TextBox txtTemp)
        {
            txtTemp.SelectionStart = 0;
            txtTemp.SelectionLength = txtTemp.Text.Length;
        }



        private void txtSingleRate_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void DtgridRoomCat_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                FillDetails();
                mod.gridDoubleClick(ChkDeleted, BtnDelete, txtRomCategry);
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

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                string strMsg;
                strMsg = BtnDelete.Text;
                if (BtnDelete.Text == "DELETE" || BtnDelete.Text == "RECALL")
                {
                    i = mod.deleteButtonClick(tableName, firstCol, txtRoomNo.Text,
                         txtsearch, BtnDelete.Text.Substring(0, 1));
                    if (i == 1)
                    {
                        RoomMaster_Load(sender, EventArgs.Empty);
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
                //  lblSucessMsg.Text = "";
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }



        private void ChkCode()
        {
            try
            {
                str = "SELECT * FROM " + tableName + " WHERE " + firstCol + "='" + txtRoomNo.Text +
                               "' AND Branchcode='" + Globalvariable.bcode + "'";
                SqlDataReader dr = mod.GetRecord(str);
                if (dr.HasRows)
                {
                    MessageBox.Show("Table No Already Exist!!");
                    txtRoomNo.Text = "";
                    txtRoomNo.Focus();
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void txtsearch_Enter_1(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.Yellow;
        }

        private void txtsearch_KeyDown_1(object sender, KeyEventArgs e)
        {
            DtgridRoomCat_KeyDown(sender, e);
        }

        private void txtsearch_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                mod.txtSearchChange(ChkDeleted, firstCol, secondCol, tableName, DtgridRoomCat, txtsearch, "Room Category Name", "Room No.");
                FillDetails();
                lblSucessMsg.Text = "";

            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void txtRoomNo_Enter_1(object sender, EventArgs e)
        {
            txtRoomNo.BackColor = Color.Yellow;

        }

        private void txtRoomNo_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!txtRoomNo.Text.Trim().Equals(""))
                {
                    ChkCode();
                }
                else
                {
                    txtRoomNo.Focus();
                }
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


        private void txtDoubleRate_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void RoomMaster_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Control nextTab;

                if (e.KeyChar == (char)Keys.Enter)
                {
                    nextTab = ((Control)sender);
                    if (ActiveControl.Name == "txtsearch" && DtgridRoomCat.Rows.Count == 0)
                    {
                        nextTab = BtnAdd;
                    }
                    else if (ActiveControl.Name == "txtSingleRate")
                    {
                        if (txtSingleRate.Text.Trim().Equals(""))
                        {
                            txtSingleRate.Focus();
                        }
                        else
                        {
                            txtDoubleRate.Focus();
                        }
                    }
                    else if (ActiveControl.Name == "txtDoubleRate")
                    {
                        if (txtDoubleRate.Text.Trim().Equals(""))
                        {
                            txtDoubleRate.Focus();
                        }
                    }
                    else if (ActiveControl.Name == "txtRomCategry")
                    {
                        if (lblRoomCatName.Text == "")
                        {
                            txtRomCategry.Focus();
                        }
                        else
                        {
                            nextTab = GetNextControl(ActiveControl, true);
                        }
                        nextTab.Focus();
                    }
                    else
                    {
                        if (BtnDelete.Text != "RECALL")
                            nextTab = GetNextControl(ActiveControl, true);
                    }
                    //    nextTab.Focus();
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void txtDoubleRate_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            try
            {
                e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == '.');
                if (e.KeyChar == (char)Keys.Enter && txtDoubleRate.Text != "")
                {
                    BtnSave.Focus();
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void txtRomCategry_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void txtSingleRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == '.');
        }

        private void txtRoomNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtRomCategry.Focus();
            }
        }

        private void txtDoubleRate_Leave(object sender, EventArgs e)
        {
            txtDoubleRate.BackColor = Color.White;
        }

        private void txtsearch_Leave(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.White;
        }

        private void txtRoomNo_Leave(object sender, EventArgs e)
        {
            txtRoomNo.BackColor = Color.White;
        }

        private void txtRomCategry_Leave(object sender, EventArgs e)
        {
            txtRomCategry.BackColor = Color.White;
        }

        private void btnLessID_Click(object sender, EventArgs e)
        {

            Models.Common.FormManagement.Instance.SelectRecordFromGrid(DtgridRoomCat, Models.Common.RecordMove.Previous, FillDetails);

        }

        private void btnGreaterID_Click(object sender, EventArgs e)
        {

            Models.Common.FormManagement.Instance.SelectRecordFromGrid(DtgridRoomCat, Models.Common.RecordMove.Next, FillDetails);

        }

        private void txtSingleRate_Leave(object sender, EventArgs e)
        {
            txtSingleRate.BackColor = Color.White;
        }

        private void RoomMaster_KeyDown(object sender, KeyEventArgs e)
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

        private void DtgridRoomCat_CellClick(object sender, DataGridViewCellEventArgs e)
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

        private void RoomMaster_Shown(object sender, EventArgs e)
        {
            txtsearch.Focus();
        }


    }
}

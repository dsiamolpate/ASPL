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
    public partial class RoomCategory : Form
    {
        Design ObjDesign = new Design();
        public RoomCategory()
        {
            InitializeComponent();
        }
        public bool checkValue = false;
        private BindingSource bindgrid = new BindingSource();
        Module mod = new Module();
        Connection con = new Connection();
        DataGridViewTextBoxEditingControl tb;
        SqlDataAdapter sqlda;
        SqlCommand cmd;
        DataSet ds = new DataSet();
        int flag;
        bool chkDupFlag;
        public Boolean plusKeyPress;
        public string DupCode1 = "0";
        string code = "";
        // public static SqlDataReader srchdr;
        int rowNo, colNo;
        private void myControl1_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.comballowdesc.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
        }
        int selectrow;
        string str, StrUpdate;
        string filc;
        string firstCol = "catgry_code";
        string secondCol = "catgry_name";
        string tableName = "RoomCategoryMaster";
        string Deleted = "deleted";
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
        private void RoomCategory_Load(object sender, EventArgs e)
        {
            try
            {
                Models.Common.FormManagement.Instance.FillCombo(ref comballowdesc, Models.Common.ComboType.YesNo, "No");
                //Globalvariable.frmName = "RoomCategry";         
                ObjDesign.FormDesign(this, DtgridRoomCat);
                ObjDesign.FormDesign(this, itemMastGrid);
                mod.FormLoad(this, tableName, firstCol, secondCol, Deleted, "N", "N", DtgridRoomCat, txtsearch, Chkshowdelet, "Description", "Code");
                FillDetails();
                txtcode.Enabled = false;
                this.KeyPreview = true;
                flag = 1;
                this.ActiveControl = txtsearch;
                // txtsearch.Focus();
                Globalvariable.SearchChangeVariable = "SrchByName";

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
                mod.chkDeletedChange(Chkshowdelet, firstCol, secondCol, tableName, Deleted, "N", DtgridRoomCat, txtsearch, "Room Category Name", "Room No.");
                FillDetails();
                mod.UserAccessibillityMaster("Room Category", BtnAdd, BtnSave, BtnDelete);
                lblSucessMsg.Text = "";
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        //public void FillAllowDesc()
        //{
        //    try
        //    {
        //        comballowdesc.Items.Clear();
        //        comballowdesc.Items.Insert(0, "SELECT");
        //        comballowdesc.Items.Insert(1, "Yes");
        //        comballowdesc.Items.Insert(2, "No");
        //        comballowdesc.SelectedIndex = 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        string str = "Message:" + ex.Message;
        //        MessageBox.Show(str, "Error Message");
        //    }
        //}

        //public void fillindexcom()
        //{
        //    try
        //    {
        //        comballowdesc.Items.Clear();
        //        comballowdesc.Items.Insert(0, "SELECT");
        //        comballowdesc.Items.Insert(1, "Yes");
        //        comballowdesc.Items.Insert(2, "No");
        //        //   comballowdesc.SelectedIndex = 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        string str = "Message:" + ex.Message;
        //        MessageBox.Show(str, "Error Message");
        //    }
        //}

        public void FillDetails()
        {
            try
            {
                if (Chkshowdelet.Checked == true)
                {
                    mod.chkDeletedTrue(this, BtnAdd, BtnSave, BtnDelete, DtgridRoomCat, txtsearch);
                    txtsearch.Focus();
                }
                else
                {
                    mod.chkDeletedFalse("Room Category", this, BtnAdd, BtnSave, BtnDelete, DtgridRoomCat, txtsearch);
                }
                if (DtgridRoomCat.Rows.Count > 0)
                {
                    int i;
                    //   FillAllowDesc();
                    //comballowdesc.Items.Clear();
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
                        //string v = "SELECT * FROM RoomCategoryMaster WHERE catgry_code ='" + s + "' and Branchcode='" + Globalvariable.bcode + "'";
                        //SqlCommand cmd = new SqlCommand("SELECT * FROM RoomCategoryMaster WHERE catgry_code ='" + s + "' and Branchcode='" + Globalvariable.bcode + "'", con.connect());
                        //SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            txtcode.Text = dr["catgry_code"].ToString();
                            txtcode.Enabled = false;
                            txtname.Text = dr["catgry_name"].ToString();

                            comballowdesc.SelectedValue = Convert.ToString(dr["discount"]);
                            //if (filc != "")
                            //{
                            //    if (filc == "Y")
                            //        comballowdesc.Items.Insert(0, "Yes");

                            //    else if (filc == "N")
                            //        comballowdesc.Items.Insert(0, "No");
                            //}
                            //comballowdesc.SelectedIndex = 0;
                            //fillindexcom();
                        }
                        SqlDataReader dr1 = mod.GetSelectAllField("tbRoomTaxDetail", "room_cat_no", txtcode.Text.ToString(), "");
                        itemMastGrid.Rows.Clear();
                        i = 0;
                        if (dr1.HasRows)
                        {
                            while (dr1.Read())
                            {
                                itemMastGrid.Rows.Add();
                                itemMastGrid.Rows[i].Cells[0].Value = i + 1;//dr["Item_code"].ToString();                            
                                itemMastGrid.Rows[i].Cells[1].Value = dr1["tax_code"].ToString();
                                string[] rstr;
                                rstr = GetTaxName(dr1["tax_code"].ToString());
                                itemMastGrid.Rows[i].Cells[2].Value = rstr[0];
                                //itemMastGrid.Rows[i].Cells[3].Value = rstr[1];
                                i = i + 1;
                            }
                        }
                    }
                    mod.UserAccessibillityMaster("Room Category", BtnAdd, BtnSave, BtnDelete);
                    if (BtnDelete.Enabled == true)
                    {
                        str = "SELECT catgry_code FROM RoomCategoryMaster WHERE catgry_code='" + txtcode.Text + "' AND catgry_code IN(SELECT room_cat_no FROM tbRoomMaster)";
                        mod.CheckUse(str, BtnDelete);
                    }
                }
                else
                {
                    mod.txtclear(this.Controls);
                    itemMastGrid.Rows.Clear();
                    //FillAllowDesc();
                    mod.UserAccessibillityMaster("Room Category", BtnAdd, BtnSave, BtnDelete);
                    BtnDelete.Text = "RESET";

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

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
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
                if (e.KeyCode == Keys.Enter && Chkshowdelet.Checked == true && DtgridRoomCat.Rows.Count > 0)
                    BtnDelete.Focus();
                else if (e.KeyCode == Keys.Enter && Chkshowdelet.Checked == true && DtgridRoomCat.Rows.Count == 0)
                    BtnExit.Focus();
                lblSucessMsg.Text = "";

            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void RoomCategory_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                bool nextTabPrcess = false;
                if (e.KeyChar == 43)
                {
                    if (ActiveControl.AccessibleName == "Editing Control")
                    {
                        if (itemMastGrid.Rows[itemMastGrid.CurrentCell.RowIndex].Cells[1].Value == null || itemMastGrid.Rows[itemMastGrid.CurrentCell.RowIndex].Cells[1].Value.ToString() == "")
                        {
                            itemMastGrid.Rows.RemoveAt(itemMastGrid.CurrentCell.RowIndex);
                            BtnSave.Enabled = true;
                            BtnSave.Focus();
                            return;
                        }
                    }
                }
                Control nextTab;
                if (e.KeyChar == (char)Keys.Enter)
                {
                    nextTab = ((Control)sender);
                    //  FillDetails();
                    //   int i = DtgridRoomCat.SelectedCells[0].RowIndex;

                    if (ActiveControl.Name == "itemMastGrid" && itemMastGrid.RowCount > 0)
                    {
                        rowNo = itemMastGrid.CurrentCell.RowIndex;
                        colNo = itemMastGrid.CurrentCell.ColumnIndex;
                        if (itemMastGrid.Rows[rowNo].Cells[1].Value != null)
                        {
                            itemMastGrid.CurrentCell = itemMastGrid[1, rowNo];
                            itemMastGrid.BeginEdit(true);
                        }
                        else
                        {

                            itemMastGrid.CurrentCell = itemMastGrid[1, rowNo];
                            itemMastGrid.BeginEdit(true);
                        }
                        //if (itemMastGrid.Rows[rowNo].Cells[colNo].Value != null && itemMastGrid.Rows[rowNo].Cells[colNo].Value.ToString() != "")
                        //{
                        //    if (colNo == 0)
                        //    {
                        //        if (plusKeyPress == true)
                        //        {
                        //            plusKeyPress = false;
                        //        }
                        //        else if (itemMastGrid.Rows[rowNo].Cells[0].Value != null)
                        //        {
                        //            itemMastGrid.CurrentCell = itemMastGrid[1, rowNo];
                        //            itemMastGrid.BeginEdit(true);
                        //        }
                        //    }
                        //    else if (colNo == 1)
                        //    {
                        //        if (itemMastGrid.Rows[rowNo].Cells[1].Value != null)
                        //        {
                        //            itemMastGrid.CurrentCell = itemMastGrid[1, rowNo];
                        //            itemMastGrid.BeginEdit(true);
                        //        }
                        //    }
                        //    else if (colNo == 2)
                        //    {
                        //        itemMastGrid.CurrentCell = itemMastGrid[3, rowNo];
                        //        itemMastGrid.BeginEdit(true);
                        //    }
                        //}
                        //else
                        //{
                        //    itemMastGrid.CurrentCell = itemMastGrid[colNo, rowNo];
                        //    itemMastGrid.BeginEdit(true);
                        //}
                    }
                    else if (ActiveControl.Name == "txtsearch" && DtgridRoomCat.Rows.Count == 0 && e.KeyChar == (char)Keys.Enter)
                    {
                        txtsearch.Focus();
                    }
                    else
                    {
                        if (ActiveControl.Name == "txtsearch" && DtgridRoomCat.Rows.Count == 0 && txtsearch.Text == "")
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

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                mod.txtSearchChange(Chkshowdelet, firstCol, secondCol, tableName, DtgridRoomCat, txtsearch, "Room Category Name", "Room No.");
                FillDetails();
                lblSucessMsg.Text = "";

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
                mod.UserAccessibillityMaster("Room Category", BtnAdd, BtnSave, BtnDelete);
                flag = 0;
                txtcode.Text = "" + mod.GetMaxNum(tableName, firstCol);
                itemMastGrid.Rows.Clear();
                //FillAllowDesc();
                lblSucessMsg.Text = "";
                label1.Text = "Search by Name :";

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
                if (DtgridRoomCat.Rows.Count > 0)
                {
                    selectrow = DtgridRoomCat.SelectedCells[0].RowIndex;
                }
                if (txtname.Text == "")
                {
                    txtname.Focus();
                }
                //else if (comballowdesc.SelectedIndex == 0)
                //{
                //    comballowdesc.Focus();
                //}
                else
                {
                    if (BtnSave.Text == "SAVE")
                        str = "SELECT * FROM RoomCategoryMaster WHERE catgry_name='" + txtname.Text.Replace("'", "''") +
                            "' AND Branchcode='" + Globalvariable.bcode + "'";
                    else if (BtnSave.Text == "UPDATE")
                        str = "SELECT * FROM RoomCategoryMaster WHERE catgry_name='" + txtname.Text.Replace("'", "''") +
                               "' AND Branchcode='" + Globalvariable.bcode + "' AND catgry_code !=" + txtcode.Text + "";
                    dr = mod.GetRecord(str);
                    if (dr.HasRows)
                    {
                        MessageBox.Show("Name Already Exist!!");
                        txtname.Text = "";
                        txtname.Focus();
                    }
                    else
                    {
                        string discYN = comballowdesc.SelectedValue.ToString();
                        int i;
                        //if (comballowdesc.SelectedItem == "Yes")
                        //    discYN = "Y";
                        BtnSave.Enabled = false;
                        if (BtnSave.Text == "UPDATE")
                        {
                            dr = mod.GetSelectAllField("RoomCategoryMaster", "catgry_code", txtcode.Text.ToString(), "");
                            while (dr.Read())
                            {
                                mod.compare(txtname.Text, dr["catgry_name"].ToString(), "Room Category");
                                mod.compare(discYN, dr["discount"].ToString(), "Room Category");
                                SqlDataReader dr1 = mod.GetSelectAllField("tbRoomTaxDetail", "room_cat_no", txtcode.Text.ToString(), "");
                                while (dr1.Read())
                                {
                                    for (int cnt = 0; cnt < itemMastGrid.Rows.Count; cnt++)
                                    {
                                        string[] dataStr, changeData;
                                        dataStr = GetTaxName(dr1["tax_code"].ToString());
                                        changeData = GetTaxName(Convert.ToString(itemMastGrid.Rows[cnt].Cells[1].Value));
                                        mod.compare(changeData[0], dataStr[0], "Room Category");
                                    }
                                }
                            }
                        }
                        cmd = new SqlCommand("SP_INSERTRoomcategry", con.connect());
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
                        cmd.Parameters.AddWithValue("@discount", discYN);
                        cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                        cmd.ExecuteNonQuery();
                        if (BtnSave.Text == "SAVE")
                        {
                            lblSucessMsg.Text = "Save Successfully.....!!";
                        }
                        else
                        {
                            lblSucessMsg.Text = "Update Successfully.....!!";
                            str = "DELETE FROM tbRoomTaxDetail WHERE room_cat_no=" + txtcode.Text + " AND Branchcode='" + Globalvariable.bcode + "'";
                            mod.Executequery(str);
                            StrUpdate = "U";
                        }
                        for (int cnt = 0; cnt < itemMastGrid.Rows.Count; cnt++)
                        {
                            if (itemMastGrid.Rows[cnt].Cells[0].Value == null)
                                itemMastGrid.Rows.RemoveAt(cnt);
                            else
                            {
                                //str = "INSERT INTO tbRoomTaxDetail(room_no,room_cat_no,tax_code,Branchcode) VALUES(" + txtRoomNo.Text +
                                //    "," + txtRomCategry.Text+ "," + itemMastGrid.Rows[cnt].Cells[1].Value +
                                //    ",'" + Globalvariable.bcode + "' )";
                                SqlCommand cmdRoomTax = new SqlCommand("SP_INSERTRoomTax", con.connect());
                                cmdRoomTax.CommandType = CommandType.StoredProcedure;
                                // cmdRoomTax.Parameters.AddWithValue("@roomno", txtRoomNo.Text);
                                cmdRoomTax.Parameters.AddWithValue("@catgryNo", txtcode.Text);
                                cmdRoomTax.Parameters.AddWithValue("@taxcode", itemMastGrid.Rows[cnt].Cells[1].Value);
                                cmdRoomTax.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                                cmdRoomTax.ExecuteNonQuery();
                            }
                        }
                        mod.DataGridBind(DtgridRoomCat, tableName, firstCol, secondCol, "N", "Room Category Name", "Room No.");
                        // string txt = BtnSave.Text;
                        FillDetails();

                        if (DtgridRoomCat.Rows.Count > 0)
                        {
                            DtgridRoomCat.CurrentCell = DtgridRoomCat[0, selectrow];
                        }
                        StrUpdate = "";
                        txtsearch.Focus();
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

        //public void compare(string val1,string val2,string frmname)
        //{  
        //    bool equal= string.Equals(val1,val2);
        //    if (equal == false)
        //    {
        //       string  str = "INSERT INTO tbMasterModifyDetail(FrmName,Orginal_Data,Change_Data,Modify_Date,Modify_Time,User_id)VALUES('" + frmname + "','" + mod.Isnull(val2, "") + "','" + mod.Isnull(val1, "") + "','" + DateTime.Now.ToShortDateString() + "','" + DateTime.Now.ToShortTimeString() + "','" + Globalvariable.usercd + "')";
        //       SqlCommand cmd = new SqlCommand(str, con.connect());
        //       cmd.ExecuteNonQuery();
        //    }
        //}


        public void BindGridView()
        {
            DataSet ds = new DataSet();
            {
                str = "SELECT catgry_name,catgry_code FROM RoomCategoryMaster WHERE deleted='N' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY catgry_code";
                SqlCommand cmd = new SqlCommand(str, con.connect());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DtgridRoomCat.Columns[0].DataPropertyName = "catgry_name";
                DtgridRoomCat.Columns[1].DataPropertyName = "catgry_code";
                DtgridRoomCat.DataSource = dt;
                DtgridRoomCat.DataSource = bindgrid;
            }
        }

        private void txtname_Enter(object sender, EventArgs e)
        {
            txtname.BackColor = Color.Yellow;
        }

        private void DtgridRoomCat_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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

        private void comballowdesc_Enter(object sender, EventArgs e)
        {
            comballowdesc.BackColor = Color.Yellow;
        }

        private void txtname_KeyPress(object sender, KeyPressEventArgs e)
        {
            // e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space || e.KeyChar == '.');
            if (e.KeyChar == (char)Keys.Enter)
            {
                comballowdesc.Focus();
            }
        }

        private void comballowdesc_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((e.KeyChar == (char)Keys.Enter) && (comballowdesc.SelectedIndex != 0))
                {
                    if (flag == 0)
                    {
                        itemMastGrid.Rows.Clear();
                        itemMastGrid.Enabled = true;
                        itemMastGrid.Rows.Add();
                        rowNo = itemMastGrid.RowCount - 1;
                        itemMastGrid.Rows[rowNo].Cells[0].Value = itemMastGrid.Rows.Count;
                        itemMastGrid.CurrentCell = itemMastGrid[1, 0];
                        itemMastGrid.BeginEdit(true);
                    }
                    else if (flag == 1)
                    {
                        int val;
                        itemMastGrid.Enabled = true;
                        if (itemMastGrid.Rows.Count == 0)
                        {
                            itemMastGrid.Rows.Add();
                            rowNo = itemMastGrid.RowCount - 1;
                            if (rowNo == 0)
                            {
                                val = rowNo;
                            }
                            else
                            {
                                val = rowNo - 1;
                            }
                            if (itemMastGrid.Rows[val].Cells[1].Value != null)
                            {
                                itemMastGrid.Rows[rowNo].Cells[0].Value = itemMastGrid.Rows.Count;
                                itemMastGrid.CurrentCell = itemMastGrid[1, rowNo];
                                itemMastGrid.BeginEdit(true);
                            }
                            else
                            {
                                if (rowNo > val)
                                {
                                    itemMastGrid.Rows.Remove(itemMastGrid.Rows[rowNo]);
                                }
                                itemMastGrid.Rows[rowNo].Cells[0].Value = itemMastGrid.Rows.Count;
                                itemMastGrid.CurrentCell = itemMastGrid[1, val];
                                itemMastGrid.BeginEdit(true);
                            }
                        }
                        else if (itemMastGrid.Rows.Count > 0)
                        {
                            rowNo = 0;
                            itemMastGrid.CurrentCell = itemMastGrid[1, 0];
                            itemMastGrid.BeginEdit(true);
                        }
                    }
                }
                else
                {
                    comballowdesc.Focus();
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
                        RoomCategory_Load(sender, EventArgs.Empty);
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

        private void txtsearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            //  if (e.KeyChar == (char)Keys.Enter && txtsearch.Text=="")
            if (e.KeyChar == (char)Keys.Enter && txtsearch.Text == "")
            {
                BtnAdd.Focus();
            }
            lblSucessMsg.Text = "";

        }

        private void txtsearch_Leave(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.White;
        }

        private void txtname_Leave(object sender, EventArgs e)
        {
            txtname.BackColor = Color.White;
        }

        private void comballowdesc_Leave(object sender, EventArgs e)
        {
            comballowdesc.BackColor = Color.White;
        }

        private void RoomCategory_KeyDown_1(object sender, KeyEventArgs e)
        {
            //if (e.Control == true && e.KeyCode == Keys.A)
            //{
            //    BtnAdd.PerformClick();
            //}
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
            //  e.Handled = false;
        }

        private void itemMastGrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
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
                Globalvariable.SearchString = "TaxID";
                BtnSave.Enabled = false;
                DupCode1 = "0";
                rowNo = itemMastGrid.CurrentCell.RowIndex;
                colNo = itemMastGrid.CurrentCell.ColumnIndex;




                if (e.KeyCode == Keys.Add)
                {
                    if (itemMastGrid.RowCount > 0)
                    {

                        itemMastGrid.Rows[rowNo].Cells[2].Value = "";
                        //plusKeyPress = true;
                        int i = itemMastGrid.CurrentCell.RowIndex;
                        if (Convert.ToString(itemMastGrid.Rows[i].Cells[2].Value) == string.Empty)
                        {
                            rowNo = rowNo - 1;
                            itemMastGrid.Rows.RemoveAt(i);
                        }
                        BtnSave.Enabled = true;
                        BtnSave.Focus();
                    }
                    Globalvariable.searchNo = 0;
                    return;
                }

                if (colNo == 1)
                {


                    if (e.KeyCode == Keys.Enter)
                    {
                        itemMastGrid.Rows[rowNo].Cells[2].Value = "";

                        Globalvariable.searchNo = 1;
                        DupCode1 = mod.DuplicateRecord(itemMastGrid, 1, rowNo);
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

                        itemMastGrid.ClearSelection();
                        itemMastGrid.CurrentCell = itemMastGrid[2, rowNo];
                        itemMastGrid.Rows[rowNo].Cells[1].Value = mod.Isnull(returnForm.codeselected, "");
                        itemMastGrid.Rows[rowNo].Cells[2].Value = mod.Isnull(returnForm.descselected, "");

                        if (rowNo == itemMastGrid.Rows.Count - 1)
                        {
                            itemMastGrid.Rows.Add();
                            rowNo = itemMastGrid.RowCount - 1;
                            itemMastGrid.Rows[rowNo].Cells[0].Value = itemMastGrid.Rows.Count;
                            itemMastGrid.CurrentCell = itemMastGrid[1, rowNo];
                            itemMastGrid.BeginEdit(true);
                        }
                        else
                        {
                            itemMastGrid.CurrentCell = itemMastGrid[1, rowNo];
                        }
                        return;
                    }
                }

                return;

                if (colNo == 1)
                {
                    if (e.KeyCode == Keys.Back)
                    {
                        itemMastGrid.Rows[rowNo].Cells[1].Value = null;
                    }
                    else if (e.KeyCode == Keys.Enter && itemMastGrid.Rows[rowNo].Cells[1].Value == null)
                    {
                        //  DupCode1 = "0";
                        DupCode1 = mod.DuplicateRecord(itemMastGrid, 1, rowNo);
                        Globalvariable.DuplicateValue = DupCode1;
                        itemMastGrid.Rows[rowNo].Cells[1].Value = tb.Text;
                        if (tb.Text != string.Empty)
                        {
                            int i;
                            int itemn = Convert.ToInt32(itemMastGrid.Rows[rowNo].Cells[1].Value);
                            chkDupFlag = false;
                            i = mod.getDuplicateRowNo(itemn, itemMastGrid, 1, rowNo);
                            if (i == -1)
                            {
                                code = itemMastGrid.Rows[rowNo].Cells[1].Value.ToString();
                                str = "SELECT * FROM tbTaxMaster WHERE tax_code='" + code + "' AND tax_code not IN(" + DupCode1 + ") AND deleted='N' AND Branchcode='" + Globalvariable.bcode + "'";
                                SqlDataReader dr1 = mod.GetRecord(str);
                                if (dr1.Read())
                                {
                                    tb.Text = "";
                                    itemMastGrid.ClearSelection();
                                    itemMastGrid.CurrentCell = itemMastGrid[1, rowNo];
                                    itemMastGrid.BeginEdit(true);
                                    itemMastGrid.Rows[rowNo].Cells[1].Value = dr1["tax_code"].ToString();
                                    itemMastGrid.Rows[rowNo].Cells[2].Value = dr1["name"].ToString();
                                    //itemMastGrid.Rows[rowNo].Cells[3].Value = dr1["gl_code"].ToString();
                                    tb.Text = dr1["tax_code"].ToString();
                                    //Add row in grid
                                    itemMastGrid.Enabled = true;
                                    int rowcount = itemMastGrid.Rows.Count;
                                    if (rowNo == rowcount - 1)
                                    {
                                        itemMastGrid.Rows.Add();
                                        rowNo = itemMastGrid.RowCount - 1;
                                        itemMastGrid.Rows[rowNo].Cells[0].Value = itemMastGrid.Rows.Count;
                                        itemMastGrid.CurrentCell = itemMastGrid[1, rowNo];
                                        itemMastGrid.BeginEdit(true);
                                    }
                                    else
                                    {
                                        itemMastGrid.CurrentCell = itemMastGrid[1, rowNo];
                                        itemMastGrid.BeginEdit(true);
                                    }
                                }
                                else
                                {
                                    clearCol();
                                    tb.Text = "";
                                    // itemMastGrid.ClearSelection();
                                    itemMastGrid.CurrentCell = itemMastGrid[1, rowNo];
                                    itemMastGrid.BeginEdit(true);
                                }
                            }
                            else
                            {
                                chkDupFlag = true;
                                itemMastGrid.Rows[rowNo].Cells[1].Value = "";
                                tb.Text = "";
                                // itemMastGrid.ClearSelection();
                                itemMastGrid.CurrentCell = itemMastGrid[1, rowNo];
                                itemMastGrid.BeginEdit(true);
                            }
                        }
                        else if (tb.Text == string.Empty)
                        {
                            Globalvariable.searchNo = 1;
                            DupCode1 = mod.DuplicateRecord(itemMastGrid, 1, rowNo);
                            Globalvariable.DuplicateValue = DupCode1;
                            Globalvariable.searchNo = 1;

                            var returnForm = Models.Common.FormManagement.Instance.SearchFormOnKeyDown(ref tb, e, "GST", true);
                            if (returnForm == null)
                            {
                                return;
                            }


                            itemMastGrid.ClearSelection();
                            itemMastGrid.CurrentCell = itemMastGrid[2, rowNo];
                            itemMastGrid.Rows[rowNo].Cells[1].Value = mod.Isnull(returnForm.codeselected, "");
                            itemMastGrid.Rows[rowNo].Cells[2].Value = mod.Isnull(returnForm.descselected, "");
                            return;

                            //KeysConverter kc = new KeysConverter();
                            //string key = kc.ConvertToString(e.KeyCode);
                            //FrmSearch srch = new FrmSearch();
                            ////  DupCode1 = "0";
                            //DupCode1 =mod.DuplicateRecord(itemMastGrid, 1,rowNo);
                            //Globalvariable.DuplicateValue = DupCode1;
                            //// str = "SELECT name,tax_code FROM tbTaxMaster WHERE tax_code NOT IN(" + DupCode1 + ") AND deleted='N' AND Branchcode='" + Globalvariable.bcode + "'";
                            ////sqlda = mod.GetselectQuery("tbTaxMaster", "tax_code", "name", "N", "N");
                            //sqlda = mod.SP_FillSearchGrid("tbTaxMaster", "name", "tax_code", DupCode1);
                            //srch.val = key;
                            //srch.fillbackgridSrch(sqlda, "RoomCategory");
                            //ds.Clear();
                            //sqlda.Fill(ds);
                            //if (ds.Tables[0].Rows.Count > 0)
                            //    srch.ShowDialog();
                            if (returnForm.codeselected != null)
                            {
                                tb.Text = "";
                                itemMastGrid.ClearSelection();
                                itemMastGrid.CurrentCell = itemMastGrid[2, rowNo];
                                itemMastGrid.Rows[rowNo].Cells[1].Value = mod.Isnull(returnForm.codeselected, "");
                                itemMastGrid.Rows[rowNo].Cells[2].Value = mod.Isnull(returnForm.descselected, "");
                                str = "select * from tbTaxMaster WHERE tax_code ='" + itemMastGrid.Rows[rowNo].Cells[1].Value + "' AND deleted='N' and Branchcode='" + Globalvariable.bcode + "'";
                                SqlDataReader dr2 = mod.GetRecord(str);
                                if (dr2.HasRows)
                                {
                                    dr2.Read();
                                    itemMastGrid.Rows[rowNo].Cells[3].Value = dr2["gl_code"].ToString();
                                }
                                //Add row in grid
                                itemMastGrid.Enabled = true;
                                int rowcount = itemMastGrid.Rows.Count;
                                if (rowNo == rowcount - 1)
                                {
                                    itemMastGrid.Rows.Add();
                                    rowNo = itemMastGrid.RowCount - 1;
                                    itemMastGrid.Rows[rowNo].Cells[0].Value = itemMastGrid.Rows.Count;
                                    itemMastGrid.CurrentCell = itemMastGrid[1, rowNo];
                                    itemMastGrid.BeginEdit(true);
                                }
                                else
                                {
                                    itemMastGrid.CurrentCell = itemMastGrid[1, rowNo];
                                    itemMastGrid.BeginEdit(true);
                                }
                            }
                            else
                            {
                                itemMastGrid.CurrentCell = itemMastGrid[1, rowNo];
                                itemMastGrid.BeginEdit(true);
                                tb.Focus();
                            }
                        }
                    }
                    else if (e.KeyCode == Keys.Enter && tb.Text != string.Empty && itemMastGrid.Rows[rowNo].Cells[2].Value != null)
                    {
                        // string k = itemMastGrid.Rows[rowNo].Cells[1].Value.ToString();
                        int rowcount = itemMastGrid.Rows.Count;
                        if (rowNo == rowcount - 1)
                        {
                            // int val;                           
                            rowNo = itemMastGrid.RowCount - 1;
                            if (itemMastGrid.Rows[rowNo].Cells[1].Value != null)
                            {
                                itemMastGrid.Rows.Add();
                                rowNo = itemMastGrid.RowCount - 1;
                                itemMastGrid.Rows[rowNo].Cells[0].Value = itemMastGrid.Rows.Count;
                                itemMastGrid.CurrentCell = itemMastGrid[1, rowNo];
                                itemMastGrid.BeginEdit(true);
                            }
                            else
                            {
                                itemMastGrid.Rows[rowNo].Cells[0].Value = itemMastGrid.Rows.Count;
                                itemMastGrid.CurrentCell = itemMastGrid[1, rowNo];
                                itemMastGrid.BeginEdit(true);
                            }
                        }
                    }
                    else if (e.KeyCode == Keys.Enter && tb.Text == string.Empty)
                    {
                        itemMastGrid.CurrentCell = itemMastGrid[1, rowNo];
                        itemMastGrid.BeginEdit(true);
                        tb.Focus();
                    }
                }

                if (e.KeyCode == Keys.Add && colNo == 1)
                {
                    if (itemMastGrid.RowCount > 0)
                    {
                        //plusKeyPress = true;
                        int i = itemMastGrid.CurrentCell.RowIndex;
                        if (Convert.ToString(itemMastGrid.Rows[i].Cells[1].Value) == string.Empty)
                        {
                            rowNo = rowNo - 1;
                            itemMastGrid.Rows.RemoveAt(i);
                        }
                        BtnSave.Enabled = true;
                        BtnSave.Focus();
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

        private void clearCol()
        {
            for (int cnt = 1; cnt < itemMastGrid.ColumnCount; cnt++)
            {
                itemMastGrid.Rows[itemMastGrid.CurrentCell.RowIndex].Cells[cnt].Value = "";
            }
        }

        private void itemMastGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            itemMastGrid[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.Yellow;
        }

        private void itemMastGrid_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            itemMastGrid[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.White;
        }

        private void itemMastGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void itemMastGrid_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void itemMastGrid_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void btnLessID_Click(object sender, EventArgs e)
        {

            Models.Common.FormManagement.Instance.SelectRecordFromGrid(DtgridRoomCat, Models.Common.RecordMove.Previous, FillDetails);

        }

        private void btnGreaterID_Click(object sender, EventArgs e)
        {

            Models.Common.FormManagement.Instance.SelectRecordFromGrid(DtgridRoomCat, Models.Common.RecordMove.Next, FillDetails);

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

        private void RoomCategory_Shown(object sender, EventArgs e)
        {
            txtsearch.Focus();
        }
    }
}

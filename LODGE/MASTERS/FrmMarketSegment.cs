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
    public partial class FrmMarketSegment : Form
    {
        Design ObjDesign = new Design();
        public FrmMarketSegment()
        {
            InitializeComponent();
        }

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

        public static int searchNo, selectrow;
        Module mod = new Module();
        string firstCol = "Market_Code";
        string secondCol = "Description";
        string tableName = "tbMarketSegment";
        string Deleted = "Deleted";
        string str, StrUpdate;
        Connection con = new Connection();
        private void FrmMarketSegment_Load(object sender, EventArgs e)
        {
            LoadEvent();
        }

        public void LoadEvent()
        {
            try
            {
                ObjDesign.FormDesign(this, DtgridMarketSegm);
                //  mod.FormLoad(this, firstCol, secondCol, tableName, DtgridMarketSegm, txtsearch, Chkshowdelet);
                mod.FormLoad(this, tableName, firstCol, secondCol, Deleted, "N", "N", DtgridMarketSegm, txtsearch, Chkshowdelet, "Description", "Code");
                FillDetails();
                txtcode.Enabled = false;
                mod.UserAccessibillityMaster("Market Segment", BtnAdd, BtnSave, BtnDelete);
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
                    mod.chkDeletedTrue(this, BtnAdd, BtnSave, BtnDelete, DtgridMarketSegm, txtsearch);
                    txtsearch.Focus();
                }
                else
                {
                    mod.chkDeletedFalse("Market Segment", this, BtnAdd, BtnSave, BtnDelete, DtgridMarketSegm, txtsearch);
                }
                if (DtgridMarketSegm.Rows.Count > 0)
                {
                    int i;
                    if (StrUpdate == "U")
                    {
                        i = selectrow;
                    }
                    else
                    {
                        i = DtgridMarketSegm.SelectedCells[0].RowIndex;
                    }
                    if (DtgridMarketSegm.Rows[i].Cells[1].Value != null)
                    {
                        string ID = mod.Isnull(DtgridMarketSegm.Rows[i].Cells[1].Value.ToString(), "");
                        //SqlDataReader dr = mod.GetRecord("SELECT * FROM tbMarketSegment WHERE Market_Code='" + s + "' AND Branchcode='" + Globalvariable.bcode + "'");
                        SqlDataReader dr = mod.GetSelectAllField(tableName, firstCol, ID, "");
                        while (dr.Read())
                        {
                            txtcode.Text = "";
                            txtcode.Text = dr["Market_Code"].ToString();
                            txtcode.Enabled = false;
                            txtname.Text = mod.Isnull(dr["Description"].ToString(), "");
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

        private void FrmMarketSegment_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Control nextTab;
                if (e.KeyChar == (char)Keys.Enter)
                {
                    nextTab = ((Control)sender);
                    if (ActiveControl.Name == "txtsearch" && DtgridMarketSegm.Rows.Count == 0 && e.KeyChar == (char)Keys.Enter)
                    {
                        txtsearch.Focus();
                    }
                    else
                    {
                        if (ActiveControl.Name == "txtsearch" && DtgridMarketSegm.Rows.Count == 0)
                            //  if (ActiveControl.Name == "txtsearch")
                            nextTab = BtnAdd;
                        else
                            nextTab = GetNextControl(ActiveControl, true);

                        // if (ActiveControl.Name != "")
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

        private void Chkshowdelet_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                mod.chkDeletedChange(Chkshowdelet, firstCol, secondCol, tableName, Deleted, "N", DtgridMarketSegm, txtsearch, "Plan Name", "Code");
                FillDetails();
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
            try
            {
                mod.txtSearchChange(Chkshowdelet, firstCol, secondCol, tableName, DtgridMarketSegm, txtsearch, "Plan Name", "Code");
                FillDetails();
                lblSucessMsg.Text = "";

            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void DtgridMarketSegm_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                mod.gridKeyDown(e, BtnDelete, txtsearch, DtgridMarketSegm, this);
                FillDetails();
                if (e.KeyCode == Keys.Enter && Chkshowdelet.Checked == true && DtgridMarketSegm.Rows.Count > 0)
                    BtnDelete.Focus();
                else if (e.KeyCode == Keys.Enter && Chkshowdelet.Checked == true && DtgridMarketSegm.Rows.Count == 0)
                    BtnExit.Focus();
                else if (e.KeyCode == Keys.Enter && Chkshowdelet.Checked == false && DtgridMarketSegm.Rows.Count == 0)
                    BtnExit.Focus();
                lblSucessMsg.Text = "";

            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void DtgridMarketSegm_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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

        private void txtsearch_Enter(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.Yellow;
        }

        private void txtname_Enter(object sender, EventArgs e)
        {
            txtname.BackColor = Color.Yellow;
        }

        private void txtsearch_KeyDown(object sender, KeyEventArgs e)
        {
            DtgridMarketSegm_KeyDown(sender, e);

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Add();
        }

        public void Add()
        {
            try
            {
                mod.addBtnClick(this, txtcode, tableName, firstCol, BtnSave, BtnDelete, txtname);
                mod.UserAccessibillityMaster("Market Segment", BtnAdd, BtnSave, BtnDelete);
                // Max_ID();
                txtcode.Text = "" + mod.GetMaxNum(tableName, firstCol);
                lblSucessMsg.Text = "";
                label1.Text = "Search by Name :";
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }

        }
        public void Max_ID()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT (ISNULL(MAX(Market_Code),0)+1) FROM tbMarketSegment WHERE Branchcode='" + Globalvariable.bcode + "'", con.connect());
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

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (DtgridMarketSegm.Rows.Count > 0)
                {
                    selectrow = DtgridMarketSegm.SelectedCells[0].RowIndex;
                }
                int i;
                if (txtname.Text == "")
                {
                    txtname.Focus();
                }
                else
                {
                    if (BtnSave.Text == "SAVE")
                        str = "SELECT * FROM " + tableName + " WHERE " + secondCol + "='" + txtname.Text.Replace("'", "''") +
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

                        var isSuccess = Models.Common.DbConnectivity.Instance.SaveMarketSegment(BtnSave.Text, txtcode.Text, txtname.Text);
                        Models.Common.FormManagement.Instance.ShowMessage(ref lblSucessMsg, BtnSave.Text == "SAVE" ? Models.Common.MessageType.Save : Models.Common.MessageType.Update, isSuccess);
                        //str = "INSERT INTO tbMarketSegment(Market_Code,Description,Deleted,Branchcode) VALUES(" + txtcode.Text +
                        //         ",'" + txtname.Text.Replace("'", "''") + "','N','" + Globalvariable.bcode + "' )";
                        //i = mod.SaveMaster(str);
                        //SqlCommand cmd = new SqlCommand("SP_INSERTMarketseg", con.connect());
                        //cmd.CommandType = CommandType.StoredProcedure;
                        //if (BtnSave.Text == "SAVE")
                        //{
                        //    txtcode.Text = "" + mod.GetMaxNum(tableName, firstCol);
                        //    cmd.Parameters.AddWithValue("@variable", "INSERT");
                        //}
                        // else
                        //{
                        //    cmd.Parameters.AddWithValue("@variable", "UPDATE");
                        //}
                        //cmd.Parameters.AddWithValue("@code", txtcode.Text);
                        //cmd.Parameters.AddWithValue("@name", txtname.Text.Replace("'", "''"));
                        //cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                        //cmd.ExecuteNonQuery();
                        //   if (BtnSave.Text == "SAVE")
                        //     {
                        //     lblSucessMsg.Text = "Save Successfully !!!";
                        //     }
                        //else
                        if (BtnSave.Text != "SAVE")
                        {
                            //lblSucessMsg.Text = "Update Successfully !!!";
                            StrUpdate = "U";
                        }
                        mod.DataGridBind(DtgridMarketSegm, tableName, firstCol, secondCol, "N", "Plan Name", "Code");
                        txtsearch.Focus();
                        txtsearch.Text = "";
                        string txt = BtnSave.Text;
                        FillDetails();
                        int s = selectrow;
                        if (DtgridMarketSegm.Rows.Count > 0)
                        {
                            DtgridMarketSegm.CurrentCell = DtgridMarketSegm[0, s];
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
                        FrmMarketSegment_Load(sender, EventArgs.Empty);
                        if (strMsg == "DELETE")
                            lblSucessMsg.Text = "Deleted Successfully !!!";
                        else
                            lblSucessMsg.Text = "Recalled Successfully !!!";
                    }
                }
                else if (BtnDelete.Text == "RESET")
                {
                    Add();
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

        private void txtsearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && txtsearch.Text == "")
            {
                BtnAdd.Focus();
            }
            lblSucessMsg.Text = "";
        }

        private void txtname_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == '.' || e.KeyChar == (char)Keys.Space);
        }

        private void txtsearch_Leave(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.White;
        }

        private void txtname_Leave(object sender, EventArgs e)
        {
            txtname.BackColor = Color.White;
        }

        private void FrmMarketSegment_KeyDown(object sender, KeyEventArgs e)
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

            Models.Common.FormManagement.Instance.SelectRecordFromGrid(DtgridMarketSegm, Models.Common.RecordMove.Previous, FillDetails);

        }

        private void btnGreaterID_Click(object sender, EventArgs e)
        {

            Models.Common.FormManagement.Instance.SelectRecordFromGrid(DtgridMarketSegm, Models.Common.RecordMove.Next, FillDetails);

        }

        private void DtgridMarketSegm_CellClick(object sender, DataGridViewCellEventArgs e)
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

        private void FrmMarketSegment_Shown(object sender, EventArgs e)
        {
            txtsearch.Focus();
        }
    }
}

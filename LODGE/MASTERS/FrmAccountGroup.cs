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

namespace ASPL.LODGE.MASTERS
{
    public partial class FrmAccountGroup : Form
    {
        Module mod = new Module();
       Design  ObjDesign=new Design();
        public FrmAccountGroup()
        {
            InitializeComponent();
        }
        //Connection Objcon = new Connection();
        string firstCol = "grp_code";
        string secondCol = "description";
        string tableName = "tbGroup";
        string Deleted = "Deleted";
        Connection con = new Connection();
        string str, strUnderCd, StrUpdate;
        int underCd, selectrow; 
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
        private void FrmAccountGroup_Load(object sender, EventArgs e)
        {
            LoadEvent();
        }

        public void LoadEvent()
        {
            try
            {
                ObjDesign.FormDesign(this, DtgridGroup);
                mod.FormLoad(this, tableName, firstCol, secondCol, Deleted, "N", "N", DtgridGroup, txtsearch, ChkDeleted, "Description", "Code");
                txtsearch.Focus();
                txtcode.Enabled = false;
                mod.FillCombo(comboUndercod, "SELECT under_code_id,under_cod_descri FROM tbUnderCode", "under_cod_descri", "under_code_id");
               FillDetails();
               Globalvariable.SearchChangeVariable = "SrchByName";
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }


        


        private void fillGridSearch(string strsrch)
        {
            try
            {
                DtgridGroup.Rows.Clear();
                SqlCommand cmd = new SqlCommand(strsrch, con.connect());
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string[] row = new string[] { dr[0].ToString(), dr[1].ToString() };
                    DtgridGroup.Rows.Add(row);
                }
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
                    mod.chkDeletedTrue(this, BtnAdd, BtnSave, BtnDelete, DtgridGroup, txtsearch);
                    txtsearch.Focus();
                }
                else
                {
                    mod.chkDeletedFalse("Groups",this, BtnAdd, BtnSave, BtnDelete, DtgridGroup, txtsearch);
                    ChkDeleted.Enabled = true;
                }

                if (DtgridGroup.Rows.Count > 0)
                {
                 int i;
                 if (StrUpdate == "U")
                 {
                     i = selectrow;
                 }
                 else
                 {
                     i = DtgridGroup.SelectedCells[0].RowIndex;
                 }
                    if (DtgridGroup.Rows[i].Cells[1].Value != null)
                    {
                        string ID = mod.Isnull(DtgridGroup.Rows[i].Cells[1].Value.ToString(), "");
                         SqlDataReader dr = mod.GetSelectAllField(tableName, firstCol, ID,"");
                        while (dr.Read())
                        {
                            txtcode.Text = dr["grp_code"].ToString();
                            txtcode.Enabled = false;
                            txtName.Text = dr["description"].ToString();
                            underCd = Convert.ToInt32(dr["under_code"].ToString());
                            switch (underCd)
                            {
                                case 1:
                                   comboUndercod.SelectedIndex = 0;
                                    break;
                                case 2:
                                    comboUndercod.SelectedIndex = 1;
                                    break;
                                case 3:
                                    comboUndercod.SelectedIndex = 2;
                                    break;
                                case 4:
                                    comboUndercod.SelectedIndex = 3;
                                    break;
                            }
                        }
                    }
                    mod.UserAccessibillityMaster("Groups", BtnAdd, BtnSave, BtnDelete);
                    if (BtnDelete.Enabled == true)
                    {
                        str = "select * from tbGeneralLedger where grp_code='" + txtcode.Text + "'";
                        mod.CheckUse(str,BtnDelete);
                       
                    }
                }
                else
                {
                    mod.txtclear(this.Controls);
                    comboUndercod.SelectedIndex = 0;
                    mod.UserAccessibillityMaster("Groups", BtnAdd, BtnSave, BtnDelete);
                    BtnDelete.Text = "RESET";

                }
           
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        public void txtSearchChange(System.Windows.Forms.CheckBox chkTemp, string firstCol, string secondCol, string thirdCol, string tableName,
           System.Windows.Forms.DataGridView dtgridTemp, System.Windows.Forms.TextBox txtsearch)
        {
            try
            {
                string wrd;
                wrd = mod.SrchChange(txtsearch);
                if (chkTemp.Checked == true)
                    str = "SELECT " + firstCol + "," + secondCol + "," + thirdCol + " FROM " + tableName + " WHERE deleted='Y' AND " + secondCol +
                            " LIKE '" + wrd + "%' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY " + firstCol + "";
                else
                    str = "SELECT " + firstCol + "," + secondCol + "," + thirdCol + " FROM " + tableName + " WHERE deleted='N' AND " + secondCol +
                            " LIKE '" + wrd + "%' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY " + firstCol + "";
                fillGridSearch(str);
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }



        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comballowdesc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtsearch_Enter(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.Yellow;        
        }

        private void txtName_Enter(object sender, EventArgs e)
        {
            txtName.BackColor = Color.Yellow;
            
        }

        private void comboUndercod_Enter(object sender, EventArgs e)
        {
            comboUndercod.BackColor = Color.Yellow;
            
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

       private void ChkDeleted_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                mod.chkDeletedChange(ChkDeleted, firstCol, secondCol, tableName, Deleted, "N", DtgridGroup, txtsearch, "Group Name", "Code");
                FillDetails();
                lblSucessMsg.Text = "";
            }
            catch (Exception ex)
            {
                string filepath = Application.StartupPath + "/Error";
                using (StreamWriter writer = new StreamWriter(filepath, true))
                {
                    writer.WriteLine("Message:" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace:" + ex.StackTrace +
                        "" + Environment.NewLine + "Date:" + DateTime.Now.ToString());
                    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------" + Environment.NewLine);
                }
            }
        }

        private void txtsearch_KeyDown(object sender, KeyEventArgs e)
        {
            DtgridIDType_KeyDown(sender, e);
        }
        private void DtgridIDType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
               mod.gridKeyDown(e, BtnDelete, txtsearch, DtgridGroup, this);
                FillDetails();
                if (e.KeyCode == Keys.Enter && ChkDeleted.Checked == true && DtgridGroup.Rows.Count > 0)
                    BtnDelete.Focus();
                else if (e.KeyCode == Keys.Enter && ChkDeleted.Checked == true && DtgridGroup.Rows.Count == 0)
                    BtnExit.Focus();
                lblSucessMsg.Text = "";

            }
            catch (Exception ex)
            {
                string filepath = Application.StartupPath + "/Error";
                using (StreamWriter writer = new StreamWriter(filepath, true))
                {
                    writer.WriteLine("Message:" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace:" + ex.StackTrace +
                        "" + Environment.NewLine + "Date:" + DateTime.Now.ToString());
                    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------" + Environment.NewLine);
                }
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            add();
        }

        public void add()
        {
            try
            {
                mod.addBtnClick(this, txtcode, tableName, firstCol, BtnSave, BtnDelete, txtName);
                mod.UserAccessibillityMaster("Groups", BtnAdd, BtnSave, BtnDelete);
                txtcode.Text = "" + mod.GetMaxNum(tableName, firstCol);
                comboUndercod.SelectedIndex = 0;
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
            try
            {
                if (DtgridGroup.Rows.Count > 0)
                {
                    selectrow = DtgridGroup.SelectedCells[0].RowIndex;
                }
                int i;
                if (txtName.Text == "")
                {
                    txtName.Focus();
                }
                else
                {
                  
                    if (BtnSave.Text == "SAVE")
                        str = "SELECT * FROM tbGroup WHERE description='" + txtName.Text.Replace("'", "''") +
                            "' AND Branchcode='" + Globalvariable.bcode + "'";
                    
                    else if (BtnSave.Text == "UPDATE")
                        str = "SELECT * FROM tbGroup  WHERE description='" + txtName.Text.Replace("'", "''") +
                            "' AND Branchcode='" + Globalvariable.bcode + "' AND grp_code !=" + txtcode.Text + "";
                    SqlDataReader dr = mod.GetRecord(str);
                    if (dr.HasRows)
                    {
                        MessageBox.Show("Name Already Exist!!");
                        txtName.Focus();
                    }
                    else
                    {
                        switch (comboUndercod.SelectedIndex)
                        {
                            case 0:
                                underCd = 1;
                                break;
                            case 1:
                                underCd = 2;
                                break;
                            case 2:
                                underCd = 3;
                                break;
                            case 3:
                                underCd = 4;
                                break;
                        }
                        BtnSave.Enabled = false;
                                                  
                            SqlCommand cmd = new SqlCommand("SP_INSERTGroup", con.connect());
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
                            cmd.Parameters.AddWithValue("@name", txtName.Text.Replace("'", "''"));
                            cmd.Parameters.AddWithValue("@undercode", underCd);
                            cmd.Parameters.AddWithValue("@userid", Globalvariable.usercd);
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
                            mod.DataGridBind(DtgridGroup, tableName, firstCol, secondCol, "N", "Group Name", "Code");                    
                            txtsearch.Focus();
                            txtsearch.Text = "";
                            string txt = BtnSave.Text;
                            FillDetails();
                        int s = selectrow;
                        if (DtgridGroup.Rows.Count > 0)
                        {
                            DtgridGroup.CurrentCell = DtgridGroup[0, s];
                        }
                        StrUpdate = "";
                        BtnSave.Enabled = false;

                    }
                }
            }
            catch (Exception ex)
            {
                string filepath = Application.StartupPath + "/Error";
                using (StreamWriter writer = new StreamWriter(filepath, true))
                {
                    writer.WriteLine("Message:" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace:" + ex.StackTrace +
                        "" + Environment.NewLine + "Date:" + DateTime.Now.ToString());
                    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------" + Environment.NewLine);
                }
            }
        
        }

        private void FrmAccountGroup_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Control nextTab;

                if (e.KeyChar == (char)Keys.Enter)
                {
                    nextTab = ((Control)sender);
                    if (ActiveControl.Name == "txtsearch" && DtgridGroup.Rows.Count == 0 && e.KeyChar == (char)Keys.Enter)
                    {
                        txtsearch.Focus();
                    }
                    else
                    {
                        if (ActiveControl.Name == "txtsearch" && DtgridGroup.Rows.Count == 0)
                            nextTab = BtnAdd;
                        else
                            nextTab = GetNextControl(ActiveControl, true);

                        nextTab.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                string filepath = Application.StartupPath + "/Error";
                using (StreamWriter writer = new StreamWriter(filepath, true))
                {
                    writer.WriteLine("Message:" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace:" + ex.StackTrace +
                        "" + Environment.NewLine + "Date:" + DateTime.Now.ToString());
                    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------" + Environment.NewLine);
                }
            }
        }

        private void DtgridGroup_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            FillDetails();
            mod.gridDoubleClick(ChkDeleted, BtnDelete, txtName);
            lblSucessMsg.Text = "";
            BtnSave.Enabled = true;

        }

        private void DtgridGroup_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                mod.gridKeyDown(e, BtnDelete, txtsearch, DtgridGroup, this);
                FillDetails();
                if (e.KeyCode == Keys.Enter && ChkDeleted.Checked == true && DtgridGroup.Rows.Count > 0)
                    BtnDelete.Focus();
                else if (e.KeyCode == Keys.Enter && ChkDeleted.Checked == true && DtgridGroup.Rows.Count == 0)
                    BtnExit.Focus();
                lblSucessMsg.Text = "";

            }
            catch (Exception ex)
            {
                string filepath = Application.StartupPath + "/Error";
                using (StreamWriter writer = new StreamWriter(filepath, true))
                {
                    writer.WriteLine("Message:" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace:" + ex.StackTrace +
                        "" + Environment.NewLine + "Date:" + DateTime.Now.ToString());
                    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------" + Environment.NewLine);
                }
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
                        FrmAccountGroup_Load(sender, EventArgs.Empty);
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
                string filepath = Application.StartupPath + "/Error";
                using (StreamWriter writer = new StreamWriter(filepath, true))
                {
                    writer.WriteLine("Message:" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace:" + ex.StackTrace +
                        "" + Environment.NewLine + "Date:" + DateTime.Now.ToString());
                    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------" + Environment.NewLine);
                }
            }
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                mod.txtSearchChange(ChkDeleted, firstCol, secondCol, tableName, DtgridGroup, txtsearch, "Group Name", "Code");
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
            if (e.KeyChar == (char)Keys.Enter&& txtsearch.Text=="")
            {
                BtnAdd.Focus();
            }
            lblSucessMsg.Text = "";

        }

        private void txtsearch_Leave(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.White;
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            txtName.BackColor = Color.White;
        }

        private void comboUndercod_Leave(object sender, EventArgs e)
        {
           comboUndercod.BackColor = Color.White;
        }

        private void btnLessID_Click(object sender, EventArgs e)
        {

            Models.Common.FormManagement.Instance.SelectRecordFromGrid(DtgridGroup, Models.Common.RecordMove.Previous, FillDetails);

        }

        private void btnGreaterID_Click(object sender, EventArgs e)
        {

            Models.Common.FormManagement.Instance.SelectRecordFromGrid(DtgridGroup, Models.Common.RecordMove.Next, FillDetails);

        }

        private void FrmAccountGroup_KeyDown(object sender, KeyEventArgs e)
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

        private void DtgridGroup_CellClick(object sender, DataGridViewCellEventArgs e)
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

        private void FrmAccountGroup_Shown(object sender, EventArgs e)
        {
            txtsearch.Focus();
        }

    }
}

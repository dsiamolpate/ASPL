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
    public partial class frmKotSection : Form
    {
        Design ObjDesign = new Design();
        public frmKotSection()
        {
            InitializeComponent();
        }
        int detail_id = 0, selectrow;
        string str, StrUpdate;
        string filprintYN, filprintnm, filprintTyp;
        Module mod = new Module();
        Connection con = new Connection();
        string firstCol = "KotSec_Code";
        string secondCol = "Discription";
        string tableName = "tbKotSection";
        string Deleted = "Deleted";

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

        private void frmKotSection_Load(object sender, EventArgs e)
        {
            load();
        }

        public void load()
        {

            Models.Common.FormManagement.Instance.FillCombo(ref cmbPrint, Models.Common.ComboType.YesNo);
            Models.Common.FormManagement.Instance.FillCombo(ref cmbPrintertype, "Printers");
            PrinterList();
            ObjDesign.FormDesign(this, DtgridKotSection);
            // mod.FormLoad(this, firstCol, secondCol, tableName,DtgridKotSection, txtsearch, Chkshowdelet);
            mod.FormLoad(this, tableName, firstCol, secondCol, Deleted, "N", "N", DtgridKotSection, txtsearch, Chkshowdelet, "Description", "Code");
            FillDetails();
            txtcode.Enabled = false;
            mod.UserAccessibillityMaster("Kitchen Section", BtnAdd, BtnSave, BtnDelete);
            Globalvariable.SearchChangeVariable = "SrchByName";
        }

        public void FillDetails()
        {
            try
            {

                if (Chkshowdelet.Checked == true)
                {
                    mod.chkDeletedTrue(this, BtnAdd, BtnSave, BtnDelete, DtgridKotSection, txtsearch);
                    txtsearch.Focus();
                }
                else
                {
                    mod.chkDeletedFalse("Kitchen Section", this, BtnAdd, BtnSave, BtnDelete, DtgridKotSection, txtsearch);
                }

                if (DtgridKotSection.Rows.Count > 0)
                {
                    detail_id = 1;
                    int i;
                    if (StrUpdate == "U")
                    {
                        i = selectrow;
                    }
                    else
                    {
                        i = DtgridKotSection.SelectedCells[0].RowIndex;
                    }
                    if (DtgridKotSection.Rows[i].Cells[1].Value != null)
                    {
                        string ID = mod.Isnull(DtgridKotSection.Rows[i].Cells[1].Value.ToString(), "");
                        //SqlDataReader dr = mod.GetRecord("SELECT * FROM " + tableName + " WHERE " +
                        //                     firstCol + " ='" + s + "' AND Branchcode='" + Globalvariable.bcode + "'");
                        SqlDataReader dr = mod.GetSelectAllField(tableName, firstCol, ID, "");
                        while (dr.Read())
                        {
                            txtcode.Text = dr["KotSec_Code"].ToString();
                            txtcode.Enabled = false;
                            txtkotsection.Text = dr["Discription"].ToString();
                            filprintYN = Convert.ToString(dr["PrintYN"]);
                            cmbPrint.SelectedValue = filprintYN;
                            //if (filprintYN != "")
                            //{
                            //    if (filprintYN == "Y")
                            //      cmbPrint.Items.Insert(0, "Yes");
                            //    else if (filprintYN == "N")
                            //        cmbPrint.Items.Insert(0, "No");
                            //}
                            //cmbPrint.SelectedIndex = 0;
                            //FillPrintYN();
                            filprintnm = Convert.ToString(dr["PrinterNm"]);


                            //cmbprinternm.Items.Insert(0, filprintnm);
                            //cmbprinternm.SelectedIndex = 0;
                            //PrinterList();
                            filprintTyp = Convert.ToString(dr["Printer_Type"]);
                            if(cmbprinternm.Items.IndexOf(filprintnm) <0)
                            {
                                cmbprinternm.Items.Insert(0, filprintnm);
                            }
                            cmbprinternm.Text = filprintnm;
                            cmbPrintertype.SelectedValue = filprintTyp;
                            //if (filprintTyp != "")
                            //{
                            //    if (filprintTyp == "DM")
                            //       cmbPrintertype.Items.Insert(0, "Dot Matrix");
                            //    else if (filprintTyp == "TH")
                            //        cmbPrintertype.Items.Insert(0, "Thermal");
                            //}
                            //cmbPrintertype.SelectedIndex = 0;
                            //FillPrintType();                               
                        }
                    }

                }
                else
                {
                    mod.txtclear(this.Controls);
                    //FillPrintYN();
                    //FillPrintType();
                    //PrinterList();
                    BtnDelete.Text = "RESET";

                }
                detail_id = 0;
                // lblSucessMsg.Text = "";
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


        private void PrinterList()
        {
            try
            {
                cmbprinternm.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbprinternm.Items.Clear();
                //  cmbprinternm.Items.Insert(0, "SELECT");
                foreach (string sPrinters in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                {
                    cmbprinternm.Items.Add(sPrinters);
                }

                if (detail_id != 1)
                {
                    cmbprinternm.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }



        //public void FillPrintYN()
        //{
        //    try
        //    {
        //        cmbPrint.Items.Clear();
        //        cmbPrint.Items.Insert(0, "SELECT");
        //        cmbPrint.Items.Insert(1, "Yes");
        //        cmbPrint.Items.Insert(2, "No");
        //        // cmbPayable.SelectedIndex = 0;
        //        if (detail_id != 1)
        //        {
        //            cmbPrint.SelectedIndex = 0;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string str = "Message:" + ex.Message;
        //        MessageBox.Show(str, "Error Message");
        //    }
        //}

        //public void FillPrintType()
        //{
        //    try
        //    {
        //        cmbPrintertype.Items.Clear();
        //        cmbPrintertype.Items.Insert(0, "SELECT");
        //        cmbPrintertype.Items.Insert(1, "Dot Matrix");
        //        cmbPrintertype.Items.Insert(2, "Thermal");
        //        if (detail_id != 1)
        //        {
        //            cmbPrintertype.SelectedIndex = 0;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string str = "Message:" + ex.Message;
        //        MessageBox.Show(str, "Error Message");
        //    }
        //}

        private void txtsearch_Enter(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.Yellow;
        }

        private void txtkotsection_Enter(object sender, EventArgs e)
        {

            txtkotsection.BackColor = Color.Yellow;
        }

        private void cmbPrint_Enter(object sender, EventArgs e)
        {
            cmbPrint.BackColor = Color.Yellow;
        }

        private void cmbprinternm_Enter(object sender, EventArgs e)
        {
            cmbprinternm.BackColor = Color.Yellow;
        }

        private void cmbPrintertype_Enter(object sender, EventArgs e)
        {
            cmbPrintertype.BackColor = Color.Yellow;
        }

        private void Chkshowdelet_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //mod.chkDeletedChange(Chkshowdelet, firstCol, secondCol, tableName, DtgridKotSection, txtsearch);
                mod.chkDeletedChange(Chkshowdelet, firstCol, secondCol, tableName, Deleted, "N", DtgridKotSection, txtsearch, "Name", "Code");
                FillDetails();
                lblSucessMsg.Text = "";
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void DtgridKotSection_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                mod.gridKeyDown(e, BtnDelete, txtsearch, DtgridKotSection, this);
                FillDetails();
                if (e.KeyCode == Keys.Enter && Chkshowdelet.Checked == true && DtgridKotSection.Rows.Count > 0)
                    BtnDelete.Focus();
                else if (e.KeyCode == Keys.Enter && Chkshowdelet.Checked == true && DtgridKotSection.Rows.Count == 0)
                    BtnExit.Focus();
                lblSucessMsg.Text = "";

            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void DtgridKotSection_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                FillDetails();
                mod.gridDoubleClick(Chkshowdelet, BtnDelete, txtkotsection);
                lblSucessMsg.Text = "";
                BtnSave.Enabled = true;

            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void txtsearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                DtgridKotSection_KeyDown(sender, e);
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

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Add();
        }

        public void Add()
        {
            try
            {
                mod.addBtnClick(this, txtcode, tableName, firstCol, BtnSave, BtnDelete, txtkotsection);
                mod.UserAccessibillityMaster("Kitchen Section", BtnAdd, BtnSave, BtnDelete);
                // Max_ID();
                txtcode.Text = "" + mod.GetMaxNum(tableName, firstCol);
                //FillPrintYN();
                //FillPrintType();
                //PrinterList();
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
                SqlCommand cmd = new SqlCommand("SELECT (ISNULL(MAX(KotSec_Code),0)+1) FROM tbKotSection WHERE Branchcode='" + Globalvariable.bcode + "'", con.connect());
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
                        frmKotSection_Load(sender, EventArgs.Empty);
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

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (DtgridKotSection.Rows.Count > 0)
                {
                    selectrow = DtgridKotSection.SelectedCells[0].RowIndex;
                }
                int i;

                if (txtkotsection.Text == "")
                {
                    txtkotsection.Focus();
                }
                else if (cmbPrint.SelectedIndex == 0)
                {
                    cmbPrint.Focus();
                }
                else if (cmbprinternm.SelectedIndex == 0)
                {
                    cmbprinternm.Focus();
                }
                else
                {

                    if (BtnSave.Text == "SAVE")
                        str = "SELECT * FROM " + tableName + " WHERE " + secondCol + "='" + txtkotsection.Text.Replace("'", "''") +
                            "' AND Branchcode='" + Globalvariable.bcode + "'";
                    else if (BtnSave.Text == "UPDATE")
                        str = "SELECT * FROM " + tableName + " WHERE " + secondCol + "='" + txtkotsection.Text.Replace("'", "''") +
                            "' AND Branchcode='" + Globalvariable.bcode + "' AND " + firstCol + " !=" + txtcode.Text + "";
                    SqlDataReader dr = mod.GetRecord(str);
                    if (dr.HasRows)
                    {
                        MessageBox.Show("Name Already Exist!!");
                        txtkotsection.Focus();
                    }
                    else
                    {
                        filprintYN = cmbPrint.SelectedValue.ToString();
                        filprintTyp = cmbPrintertype.SelectedValue.ToString();
                        //if (cmbPrint.SelectedItem == "Yes")
                        //{
                        //    filprintYN = "Y";
                        //}
                        //else if (cmbPrint.SelectedItem == "No")
                        //{
                        //    filprintYN = "N";
                        //}
                        //if (cmbPrintertype.SelectedItem == "Dot Matrix")
                        //{
                        //    filprintTyp = "DM";
                        //}
                        //else if (cmbPrintertype.SelectedItem == "Thermal")
                        //{
                        //    filprintTyp = "TH";
                        //}
                        filprintnm = cmbprinternm.Text;
                        BtnSave.Enabled = false;

                        BtnSave.Enabled = false;
                        if (BtnSave.Text == "UPDATE")
                        {
                            dr = mod.GetSelectAllField(tableName, firstCol, txtcode.Text.ToString(), "");
                            while (dr.Read())
                            {
                                mod.compare(txtkotsection.Text, dr["Discription"].ToString(), "Kitchen Section");
                                mod.compare(filprintYN, dr["PrintYN"].ToString(), "Kitchen Section");
                                mod.compare(filprintTyp, dr["Printer_Type"].ToString(), "Kitchen Section");
                                mod.compare(filprintnm, dr["PrinterNm"].ToString(), "Kitchen Section");
                            }
                        }
                        SqlCommand cmd = new SqlCommand("SP_INSERTKotSection", con.connect());
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
                        cmd.Parameters.AddWithValue("@name", txtkotsection.Text.Replace("'", "''"));
                        cmd.Parameters.AddWithValue("@printYN", filprintYN);
                        cmd.Parameters.AddWithValue("@printername", filprintnm);
                        cmd.Parameters.AddWithValue("@printertype", filprintTyp);
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
                        mod.DataGridBind(DtgridKotSection, tableName, firstCol, secondCol, "N", "Name", "Code");
                        txtsearch.Focus();
                        txtsearch.Text = "";
                        if (BtnSave.Text == "SAVE")
                        {
                            if (Globalvariable.frmName == "MenuItem")
                            {
                                Globalvariable.kot_id = txtcode.Text;
                                Globalvariable.kot_name = txtkotsection.Text;
                                this.Close();
                            }
                            else
                            {
                                string txt = BtnSave.Text;
                                FillDetails();
                                int s = selectrow;
                                if (DtgridKotSection.Rows.Count > 0)
                                {
                                    DtgridKotSection.CurrentCell = DtgridKotSection[0, s];
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


        private void frmKotSection_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {

                Control nextTab;
                if (e.KeyChar == (char)Keys.Enter)
                {
                    nextTab = ((Control)sender);
                    if (ActiveControl.Name == "txtsearch" && DtgridKotSection.Rows.Count == 0 && e.KeyChar == (char)Keys.Enter)
                    {
                        txtsearch.Focus();
                    }
                    else
                    {
                        if (ActiveControl.Name == "txtsearch" && DtgridKotSection.Rows.Count == 0)
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

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                mod.txtSearchChange(Chkshowdelet, firstCol, secondCol, tableName, DtgridKotSection, txtsearch, "Name", "Code");
                FillDetails();
                lblSucessMsg.Text = "";

            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void cmbPrintertype_KeyPress(object sender, KeyPressEventArgs e)
        {
            BtnSave.Focus();
        }

        private void txtkotsection_Leave(object sender, EventArgs e)
        {
            txtkotsection.BackColor = Color.White;
        }

        private void cmbPrint_Leave(object sender, EventArgs e)
        {
            cmbPrint.BackColor = Color.White;
        }

        private void cmbprinternm_Leave(object sender, EventArgs e)
        {
            cmbprinternm.BackColor = Color.White;
        }

        private void cmbPrintertype_Leave(object sender, EventArgs e)
        {
            cmbPrintertype.BackColor = Color.White;
        }

        private void btnLessID_Click(object sender, EventArgs e)
        {

            Models.Common.FormManagement.Instance.SelectRecordFromGrid(DtgridKotSection, Models.Common.RecordMove.Previous, FillDetails);

        }

        private void btnGreaterID_Click(object sender, EventArgs e)
        {

            Models.Common.FormManagement.Instance.SelectRecordFromGrid(DtgridKotSection, Models.Common.RecordMove.Next, FillDetails);

        }

        private void frmKotSection_KeyDown(object sender, KeyEventArgs e)
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

        private void txtsearch_Leave(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.White;
        }

        private void DtgridKotSection_CellClick(object sender, DataGridViewCellEventArgs e)
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

        private void frmKotSection_Shown(object sender, EventArgs e)
        {
            txtsearch.Focus();
        }
    }
}

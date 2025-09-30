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
    public partial class TaxMaster : Form
    {
        Design ObjDesign = new Design();
        public TaxMaster()
        {
            InitializeComponent();
        }
        Module mod = new Module();
        Connection con = new Connection();
        SqlDataReader dr;
        DataGridViewTextBoxEditingControl tb;
        public static SqlDataReader srchdr;
        public static SqlDataAdapter srchda;
        int flag, entervalue;
        DataSet ds = new DataSet();
        string filname, str, strsrch, StrUpdate;
        string firstCol = "tax_code";
        string secondCol = "name";
        string tableName = "tbTaxMaster";
        string Deleted = "deleted";
        int rowNo, colNo, selectrow;
        public Boolean Escpress, plusKeyPress;
        public static int searchNo;
        Boolean BlankCellFlag;
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
        private void TaxMaster_Load(object sender, EventArgs e)
        {
            try
            {
                flag = 1;
                LoadEvent();
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        public void LoadEvent()
        {
            ObjDesign.FormDesign(this, dtGrdPurdtl);
            ObjDesign.FormDesign(this, DtgridTaxMast);
            mod.FormLoad(this, tableName, firstCol, secondCol, Deleted, "N", "N", DtgridTaxMast, txtsearch, ChkDeleted, "Description", "Code");
            // mod.FormLoad(this, firstCol, secondCol, tableName, DtgridTaxMast, txtsearch, ChkDeleted);
            FillDetails();
            //txtTallyName.Enabled = false;
            //txtGL_Code_2.Enabled = false;
            mod.UserAccessibillityMaster("Tax Master", BtnAdd, BtnSave, BtnDelete);
            Globalvariable.SearchChangeVariable = "SrchByName";
        }
        private void getTallyInfo()
        {
            try
            {
                if (txtTallyCode.Text.Trim().Equals(""))
                {
                    lblTallyName.Text = "";
                }
                str = "SELECT * FROM tbTallyMaster WHERE Branchcode='" + Globalvariable.bcode + "' AND Tally_Code=" +
                   txtTallyCode.Text + "";
                SqlDataReader dr1 = mod.GetRecord(str);
                if (dr1.HasRows)
                {
                    dr1.Read();
                    lblTallyName.Text = dr1["Tally_Name"].ToString();
                }
                else
                {
                    lblTallyName.Text = "";
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
                    mod.chkDeletedTrue(this, BtnAdd, BtnSave, BtnDelete, DtgridTaxMast, txtsearch);
                    txtsearch.Focus();
                }
                else
                {
                    mod.chkDeletedFalse("Tax Master", this, BtnAdd, BtnSave, BtnDelete, DtgridTaxMast, txtsearch);
                }
                if (DtgridTaxMast.Rows.Count > 0)
                {
                    int i;
                    if (StrUpdate == "U")
                    {
                        i = selectrow;
                    }
                    else
                    {
                        i = DtgridTaxMast.SelectedCells[0].RowIndex;
                    }
                    if (DtgridTaxMast.Rows[i].Cells[1].Value != null)
                    {
                        string ID = mod.Isnull(DtgridTaxMast.Rows[i].Cells[1].Value.ToString(), "");
                        SqlDataReader dr = mod.GetSelectAllField(tableName, firstCol, ID, "");
                        // SqlCommand cmd = new SqlCommand("SELECT * FROM tbTaxMaster WHERE tax_code='" + s + "' AND Branchcode='" + Globalvariable.bcode + "'", con.connect());
                        //   and company_sr = '" + gv.companysr + "'
                        // SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            txtcode.Text = "";
                            txtcode.Text = dr["tax_code"].ToString();
                            txtcode.Enabled = false;
                            txtName.Text = mod.Isnull(dr["name"].ToString(), "-");
                            //txtTaxAmt.Text = mod.Isnull(dr["tax_amount"].ToString(), "0");
                            txtTallyCode.Text = mod.Isnull(dr["TallyCode"].ToString(), "0");
                            getTallyInfo();
                            txtGL_Code_1.Text = mod.Isnull(dr["gl_code"].ToString(), "0");
                            if (txtGL_Code_1.Text != "0")
                            {
                                getGLInfo();
                            }
                            else
                            {
                                lblGLName.Text = "";
                            }
                            filname = txtName.Text;
                        }
                        //To display record from TaxSlab
                        // SqlDataReader dr1 = mod.GetSelectAllField("tbTaxSlab", "code", txtcode.Text.ToString());
                        SqlCommand cmd1 = new SqlCommand("SELECT * FROM tbTaxSlab WHERE code='" + txtcode.Text + "' and Branchcode='" + Globalvariable.bcode + "' ORDER BY from_range ", con.connect());
                        SqlDataReader dr1 = cmd1.ExecuteReader();
                        dtGrdPurdtl.Rows.Clear();
                        int sr = 1;
                        while (dr1.Read())
                        {
                            dtGrdPurdtl.Rows.Add(sr, dr1["from_range"].ToString(), dr1["to_range"].ToString(), dr1["tax_per"].ToString());
                            sr = sr + 1;
                        }
                    }
                    //SELECT * FROM tbTaxMaster WHERE tax_code='1' AND tax_code IN(SELECT TaxCode from BillRoomTaxDtl  union select taxCode from Sales_dtl union select taxCode from Sales_dtl_ret  union select taxCode from purchase_dtl union select taxCode from purchase_dtl_ret)

                }
                else
                {
                    mod.txtclear(this.Controls);
                    dtGrdPurdtl.Rows.Clear();
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

        private void getGLInfo()
        {
            try
            {
                if (txtGL_Code_1.Text.Trim().Equals(""))
                {
                    lblGLName.Text = "";
                }
                else
                {
                    str = "SELECT genledg_desc FROM tbGeneralLedger WHERE Branchcode='" + Globalvariable.bcode + "' AND genledg_code=" +
                        txtGL_Code_1.Text + " AND genledg_code NOT IN(SELECT gen_leg_code FROM tbBook) ";
                    SqlDataReader dr1 = mod.GetRecord(str);
                    if (dr1.HasRows)
                    {
                        dr1.Read();
                        lblGLName.Text = dr1["genledg_desc"].ToString();
                    }
                    else if (txtGL_Code_1.Text != "0")
                    {
                        txtGL_Code_1.Text = "";
                        //  txtGL_Code_1.Focus();
                        lblGLName.Text = "";
                        //  txtGL_Code_1.Text = "0";
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

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space || e.KeyChar == '.' || e.KeyChar == (char)Keys.Space);
            //if (e.KeyChar == (char)Keys.Enter)
            //{
            //    txtTaxAmt.Focus();  
            //}
            //e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == '.');
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space || e.KeyChar == '.' || e.KeyChar == (char)Keys.Space);
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtTallyCode.Focus();
            }
        }

        private void txtName_Enter(object sender, EventArgs e)
        {
            txtName.BackColor = Color.Yellow;
        }

        //private void txtTaxAmt_Enter(object sender, EventArgs e)
        //{
        //    txtTaxAmt.BackColor = Color.Yellow;       
        //}

        private void txtGL_Code_1_Enter(object sender, EventArgs e)
        {
            txtGL_Code_1.BackColor = Color.Yellow;
        }


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        //private void txtTaxAmt_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == '.');
        //    if (e.KeyChar == (char)Keys.Enter)
        //    {
        //       txtTallyCode.Focus();
        //    }
        //    mod.rateValidation(sender, e, txtTaxAmt, 2, 2);
        //}

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            flag = 0;
            dtGrdPurdtl.Rows.Clear();
            Add();
        }
        public void Add()
        {
            try
            {
                mod.addBtnClick(this, txtcode, tableName, firstCol, BtnSave, BtnDelete, txtName);
                mod.UserAccessibillityMaster("Tax Master", BtnAdd, BtnSave, BtnDelete);
                // maxID();
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

        public void maxID()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT (ISNULL(MAX(tax_code),0)+1) FROM tbTaxMaster WHERE Branchcode='" + Globalvariable.bcode + "'", con.connect());
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

        private void TaxMaster_KeyPress(object sender, KeyPressEventArgs e)
        {
            //try
            //{
            //    Control nextTab;
            //    if (e.KeyChar == (char)Keys.Enter)
            //    {
            //        nextTab = ((Control)sender);
            //        if (ActiveControl.Name == "txtsearch" && DtgridTaxMast.Rows.Count == 0 && e.KeyChar == (char)Keys.Enter)
            //        {
            //            txtsearch.Focus();
            //        }
            //        else
            //        {
            //            if (ActiveControl.Name == "dtGrdPurdtl" && dtGrdPurdtl.RowCount > 0)
            //            {
            //                rowNo = dtGrdPurdtl.CurrentCell.RowIndex;
            //                colNo = dtGrdPurdtl.CurrentCell.ColumnIndex;
            //                   if (colNo == 1)
            //                      {
            //                          if (e.KeyChar == (char)Keys.Enter)
            //                          {
            //                              dtGrdPurdtl.Rows[rowNo].Cells[1].Value = tb.Text;
            //                              if (tb.Text != string.Empty)
            //                              {
            //                                  dtGrdPurdtl.CurrentCell = dtGrdPurdtl[2, rowNo];
            //                                  dtGrdPurdtl.BeginEdit(true);
            //                              }
            //                              else
            //                              {
            //                                  rowNo = dtGrdPurdtl.RowCount - 1;
            //                                  dtGrdPurdtl.CurrentCell = dtGrdPurdtl[1, rowNo];
            //                                  dtGrdPurdtl.BeginEdit(true);
            //                              }
            //                          }
            //                      }
            //                   else if (colNo == 2)
            //                   {
            //                       if (e.KeyChar == (char)Keys.Enter)
            //                       {
            //                           dtGrdPurdtl.Rows[rowNo].Cells[2].Value = tb.Text;
            //                           if (tb.Text != string.Empty)
            //                           {
            //                               dtGrdPurdtl.CurrentCell = dtGrdPurdtl[3, rowNo];
            //                               dtGrdPurdtl.BeginEdit(true);

            //                           }
            //                           else
            //                           {
            //                               rowNo = dtGrdPurdtl.RowCount - 1;
            //                               dtGrdPurdtl.CurrentCell = dtGrdPurdtl[2, rowNo];
            //                               dtGrdPurdtl.BeginEdit(true);
            //                           }
            //                       }
            //                   }
            //                   else if (colNo == 3)
            //                   {
            //                       if (e.KeyChar == (char)Keys.Enter)
            //                       {
            //                           dtGrdPurdtl.Rows[rowNo].Cells[3].Value = tb.Text;
            //                           if (tb.Text != string.Empty)
            //                           {
            //                               int rowcount = dtGrdPurdtl.RowCount;
            //                               if (rowcount - 1 > rowNo)
            //                               {
            //                                   dtGrdPurdtl.CurrentCell = dtGrdPurdtl[1, rowcount - 1];
            //                                   dtGrdPurdtl.BeginEdit(true);
            //                               }
            //                               else
            //                               {
            //                                   dtGrdPurdtl.Rows.Add();
            //                                   rowNo = dtGrdPurdtl.RowCount - 1;
            //                                   dtGrdPurdtl.Rows[rowNo].Cells[0].Value = dtGrdPurdtl.Rows.Count;
            //                                   dtGrdPurdtl.CurrentCell = dtGrdPurdtl[1, rowNo];
            //                                   dtGrdPurdtl.BeginEdit(true);
            //                               }
            //                           }
            //                           else
            //                           {
            //                               rowNo = dtGrdPurdtl.RowCount - 1;
            //                               dtGrdPurdtl.CurrentCell = dtGrdPurdtl[3, rowNo];
            //                               dtGrdPurdtl.BeginEdit(true);
            //                           }
            //                       }
            //                   }
            //                }  
            //            }

            //            if (ActiveControl.Name == "txtsearch" && DtgridTaxMast.Rows.Count == 0)
            //            {
            //                nextTab = BtnAdd;
            //            }
            //            else if (ActiveControl.Name == "txtTallyCode")
            //            {
            //                if (txtTallyCode.Text.Trim().Equals(""))
            //                {
            //                    txtTallyCode.Focus();
            //                }
            //                else
            //                {
            //                    txtGL_Code_1.Focus();
            //                }
            //            }
            //            else if (ActiveControl.Name == "txtGL_Code_1")
            //            {
            //                if (txtGL_Code_1.Text.Trim().Equals(""))
            //                {
            //                    txtGL_Code_1.Focus();
            //                }
            //            }
            //            else
            //            {
            //                if (BtnDelete.Text != "RECALL")
            //                    nextTab = GetNextControl(ActiveControl, true);
            //            }
            //            //   nextTab.Focus();
            //        }
            //    }

            //catch (Exception ex)
            //{
            //    string str = "Message:" + ex.Message;
            //    MessageBox.Show(str, "Error Message");
            //}                     
        }

        private void ChkDeleted_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //mod.chkDeletedChange(ChkDeleted, firstCol, secondCol, tableName, DtgridTaxMast, txtsearch); 
                mod.chkDeletedChange(ChkDeleted, firstCol, secondCol, tableName, Deleted, "N", DtgridTaxMast, txtsearch, "Tax Name", "Tax Code");
                FillDetails();
                if (ChkDeleted.Checked == true)
                {
                    dtGrdPurdtl.Enabled = false;
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
                mod.txtSearchChange(ChkDeleted, firstCol, secondCol, tableName, DtgridTaxMast, txtsearch, "Name", "Code");
                FillDetails();
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
            DtgridTaxMast_KeyDown(sender, e);
        }

        private void DtgridTaxMast_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                mod.gridKeyDown(e, BtnDelete, txtsearch, DtgridTaxMast, this);
                FillDetails();
                if (e.KeyCode == Keys.Enter && ChkDeleted.Checked == true && DtgridTaxMast.Rows.Count > 0)
                    BtnDelete.Focus();
                else if (e.KeyCode == Keys.Enter && ChkDeleted.Checked == true && DtgridTaxMast.Rows.Count == 0)
                    BtnExit.Focus();
                else if (e.KeyCode == Keys.Enter && ChkDeleted.Checked == false && DtgridTaxMast.Rows.Count == 0)
                    BtnExit.Focus();
                lblSucessMsg.Text = "";

            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        //public bool CheckPer()
        //{
        //    bool ret = false;
        //    try
        //    {
        //        int amt = Convert.ToInt32(txtTaxAmt.Text);
        //        if (amt > 0 && amt < 100)
        //            ret = true;
        //        else
        //            ret = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        string str = "Message:" + ex.Message;
        //        MessageBox.Show(str, "Error Message");
        //    }
        //    return ret;
        //}

        private void FillBlankCell()
        {
            try
            {
                BlankCellFlag = false;
                if (txtName.Text.Trim().Equals(""))
                {
                    BtnSave.Enabled = false;
                    BlankCellFlag = true;
                    return;
                }
                else if (txtGL_Code_1.Text.Trim().Equals(""))
                {
                    BtnSave.Enabled = false;
                    BlankCellFlag = true;
                    return;
                }
                else
                {
                    for (int i = 0; i < dtGrdPurdtl.Rows.Count; i++)
                    {
                        for (int j = 0; j < dtGrdPurdtl.ColumnCount; j++)
                        {
                            if (dtGrdPurdtl.Rows[i].Cells[j].Value.ToString() == "")
                            {
                                BtnSave.Enabled = false;
                                BlankCellFlag = true;
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
                BtnSave.Enabled = false;
                BlankCellFlag = true;
                return;
            }
        }


        private void BtnSave_Click(object sender, EventArgs e)
        {
            string tally_code;
            if (DtgridTaxMast.Rows.Count > 0)
            {
                selectrow = DtgridTaxMast.SelectedCells[0].RowIndex;
            }
            try
            {
                if (txtName.Text == "")
                {
                    txtName.Focus();
                    return;
                }
                //else if (txtTaxAmt.Text == "")
                //{
                //    txtTaxAmt.Focus();
                //    return;
                //}
                else
                {
                    int i, j, k;
                    i = 0;
                    //   lblSuccess.Text = "";                                           
                    if (BtnSave.Text == "SAVE")
                    {
                        str = "SELECT * FROM " + tableName + " WHERE " + secondCol + "='" + txtName.Text.Replace("'", "''") +
                              "' AND Branchcode='" + Globalvariable.bcode + "'";
                    }
                    else if (BtnSave.Text == "UPDATE")
                    {
                        str = "SELECT * FROM " + tableName + " WHERE " + secondCol + "='" + txtName.Text.Replace("'", "''") +
                              "' AND Branchcode='" + Globalvariable.bcode + "'AND " + firstCol + " !=" + txtcode.Text + "";
                    }
                    dr = mod.GetRecord(str);
                    if (dr.HasRows)
                    {
                        MessageBox.Show("Name Already Exist!!");
                        if (BtnSave.Text == "UPDATE")
                            txtName.Text = filname;
                        else
                            txtName.Text = "";
                        txtName.Focus();
                    }
                    else
                    {
                        tally_code = mod.Isnull(txtTallyCode.Text, "0");
                        BtnSave.Enabled = false;

                        //    str = "INSERT INTO tbTaxMaster(tax_code,name,tax_amount,deleted,Branchcode,gl_code,TallyCode) values(" + txtcode.Text + ",'" + mod.Isnull(txtName.Text.Replace("'", "''"), "") + "','" + mod.Isnull(txtTaxAmt.Text, "") + "','N','" + Globalvariable.bcode + "'," + mod.Isnull(txtGL_Code_1.Text,"0") + ",'"+tally_code+"')";                       
                        //i = mod.Executequery(str);
                        SqlCommand cmd = new SqlCommand("SP_INSERTTaxMast", con.connect());
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (BtnSave.Text == "SAVE")
                        {
                            txtcode.Text = "" + mod.GetMaxNum(tableName, firstCol);
                            cmd.Parameters.AddWithValue("@variable", "INSERT");
                        }
                        else if (BtnSave.Text == "UPDATE")
                        {
                            cmd.Parameters.AddWithValue("@variable", "UPDATE");
                        }
                        cmd.Parameters.AddWithValue("@taxcode", txtcode.Text);
                        cmd.Parameters.AddWithValue("@taxname", txtName.Text.Replace("'", "''"));
                        //cmd.Parameters.AddWithValue("@taxamount", mod.Isnull(txtTaxAmt.Text, ""));
                        cmd.Parameters.AddWithValue("@glcode", mod.Isnull(txtGL_Code_1.Text, "0"));
                        cmd.Parameters.AddWithValue("@tallycode", tally_code);
                        cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                        cmd.ExecuteNonQuery();
                        if (BtnSave.Text == "SAVE")
                        {
                            lblSucessMsg.Text = "Save Successfully !!!";
                        }
                        else if (BtnSave.Text == "UPDATE")
                        {
                            lblSucessMsg.Text = "Update Successfully !!!";
                            str = "DELETE FROM tbTaxSlab WHERE code=" + txtcode.Text + " AND Branchcode='" + Globalvariable.bcode + "'";
                            j = mod.Executequery(str);
                            StrUpdate = "U";
                        }
                        //Table:TaxSlab
                        for (j = 0; j < dtGrdPurdtl.Rows.Count; j++)
                        {
                            dtGrdPurdtl.Rows[j].Cells[1].Value = dtGrdPurdtl.Rows[j].Cells[1].Value == null ?"0": dtGrdPurdtl.Rows[j].Cells[1].Value;
                            dtGrdPurdtl.Rows[j].Cells[2].Value = dtGrdPurdtl.Rows[j].Cells[2].Value == null ? "0" : dtGrdPurdtl.Rows[j].Cells[2].Value;
                            dtGrdPurdtl.Rows[j].Cells[3].Value = dtGrdPurdtl.Rows[j].Cells[3].Value == null ? "0" : dtGrdPurdtl.Rows[j].Cells[3].Value;

                            double fromAmount = 0;
                            double toAmount = 0;
                            double taxPercentage = 0;

                            double.TryParse(dtGrdPurdtl.Rows[j].Cells[1].Value.ToString(), out fromAmount);
                            double.TryParse(dtGrdPurdtl.Rows[j].Cells[2].Value.ToString(), out toAmount);
                            double.TryParse(dtGrdPurdtl.Rows[j].Cells[3].Value.ToString(), out taxPercentage);

                            if (fromAmount + toAmount <= 0)
                            {
                                continue;
                            }

                            SqlCommand cmdTaxslab = new SqlCommand("SP_INSERTTaxMast", con.connect());
                            cmdTaxslab.CommandType = CommandType.StoredProcedure;
                            cmdTaxslab.Parameters.AddWithValue("@tablename", "tbTaxSlab");
                            cmdTaxslab.Parameters.AddWithValue("@taxcode", txtcode.Text);
                            cmdTaxslab.Parameters.AddWithValue("@fromrange", dtGrdPurdtl.Rows[j].Cells[1].Value);
                            cmdTaxslab.Parameters.AddWithValue("@torange", dtGrdPurdtl.Rows[j].Cells[2].Value);
                            cmdTaxslab.Parameters.AddWithValue("@taxper", dtGrdPurdtl.Rows[j].Cells[3].Value);
                            cmdTaxslab.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                            cmdTaxslab.ExecuteNonQuery();
                            //str = "INSERT INTO tbTaxSlab (code,from_range,to_range,tax_per,Branchcode)" +
                            //    "VALUES(" + txtcode.Text + "," + dtGrdPurdtl.Rows[j].Cells[1].Value + "," + dtGrdPurdtl.Rows[j].Cells[2].Value + "," +
                            //    "" + dtGrdPurdtl.Rows[j].Cells[3].Value + ",'" + Globalvariable.bcode + "')";
                            //mod.Executequery(str);
                        }
                        mod.DataGridBind(DtgridTaxMast, tableName, firstCol, secondCol, "N", "Name", "Code");
                        txtsearch.Focus();
                        txtsearch.Text = "";
                        FillDetails();
                        int s = selectrow;
                        if (DtgridTaxMast.Rows.Count > 0)
                        {
                            DtgridTaxMast.CurrentCell = DtgridTaxMast[0, s];
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

        public string searchChangeGLID(string searchStr)
        {
            try
            {
                if (Globalvariable.searchNo == 1)
                {
                    strsrch = "SELECT genledg_desc,genledg_code FROM tbGeneralLedger WHERE deleted='N' AND genledg_code NOT IN(SELECT gen_leg_code FROM tbBook) OR genledg_desc LIKE '" +
                           searchStr + "%' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY genledg_code";

                }
                else if (Globalvariable.searchNo == 2)
                {
                    strsrch = "SELECT genledg_desc,genledg_code FROM tbGeneralLedger WHERE deleted='N' AND genledg_code NOT IN(SELECT gen_leg_code FROM tbBook) AND genledg_desc LIKE '" +
                           searchStr + "%' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY genledg_code";
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
            return strsrch;
        }

        private void txtGL_Code_1_KeyDown(object sender, KeyEventArgs e)
        {
            var returnForm = Models.Common.FormManagement.Instance.SearchFormOnKeyDown(ref txtGL_Code_1, e, "Common_GeneralLedger", true);
            if (returnForm == null)
            {
                return;
            }
            txtGL_Code_1.Text = returnForm.codeselected;
            lblGLName.Text = returnForm.descselected;

            if (dtGrdPurdtl.Rows.Count == 0)
            {
                dtGrdPurdtl.Rows.Add();
                rowNo = dtGrdPurdtl.RowCount - 1;
                dtGrdPurdtl.Rows[rowNo].Cells[0].Value = dtGrdPurdtl.Rows.Count;
                dtGrdPurdtl.CurrentCell = dtGrdPurdtl[1, rowNo];
                dtGrdPurdtl.BeginEdit(true);
                dtGrdPurdtl.CurrentCell.Selected = true;
                return;
            }
            else
            {
                dtGrdPurdtl.CurrentCell = dtGrdPurdtl.Rows[0].Cells[1]; ;
                dtGrdPurdtl.BeginEdit(true);
                dtGrdPurdtl.CurrentCell.Selected = true;
            }
            return;
            //try
            //{
            //    Globalvariable.TaxMastVarb = "GLId";
            //    if (txtGL_Code_1.Text == "" && e.KeyCode == Keys.Enter)
            //    {
            //      Globalvariable. searchNo = 1;
            //       lblGLName.Text = "";
            //        KeysConverter kc = new KeysConverter();
            //        string key = kc.ConvertToString(e.KeyCode);
            //        FrmSearch srch = new FrmSearch();
            //        srchda = mod.SearchFrmQuery("tbGeneralLedger", "genledg_code", "genledg_desc");
            //        srch.val = key;
            //        srch.fillbackgridSrch(srchda, "FrmDiscount");
            //        ds.Clear();
            //        srchda.Fill(ds);
            //        if (ds.Tables[0].Rows.Count > 0)
            //           srch.ShowDialog();
            //       txtGL_Code_1.Text = srch.codeselected;
            //        if (srch.codeselected == null)
            //            txtGL_Code_1.Focus();
            //        else
            //        {
            //            getGLInfo();
            //        }
            //        Globalvariable.searchNo = 0;
            //        if (lblGLName.Text == "")
            //        {
            //            txtGL_Code_1.Focus();
            //        }
            //    }
            //    else if (e.KeyCode == Keys.Enter && txtGL_Code_1.Text != "" || txtGL_Code_1.Text == "0" && e.KeyCode != Keys.Back)
            //    {
            //        getGLInfo();
            //        txtGL_Code_1.BackColor = Color.White;
            //        if (flag == 0)
            //        {
            //            if ((txtGL_Code_1.Text != ""))
            //            {
            //                dtGrdPurdtl.Rows.Clear();
            //                dtGrdPurdtl.Enabled = true;
            //                dtGrdPurdtl.Rows.Add();
            //                dtGrdPurdtl.Rows[0].Cells[0].Value = dtGrdPurdtl.Rows.Count;
            //                dtGrdPurdtl.CurrentCell = dtGrdPurdtl[1, 0];
            //                dtGrdPurdtl.BeginEdit(true);
            //                e.Handled = true;
            //            }
            //        }
            //        else if (flag == 1)
            //        {
            //             int val;
            //            dtGrdPurdtl.Enabled = true;
            //            if (dtGrdPurdtl.Rows.Count == 0)
            //            {
            //                dtGrdPurdtl.Rows.Add();
            //           rowNo = dtGrdPurdtl.RowCount - 1;
            //                    if (rowNo == 0)
            //                    {
            //                        val = rowNo;
            //                    }
            //                    else
            //                    {
            //                        val = rowNo - 1;
            //                    }
            //                    if (dtGrdPurdtl.Rows[val].Cells[1].Value != null)
            //                    {
            //                        dtGrdPurdtl.Rows[rowNo].Cells[0].Value = dtGrdPurdtl.Rows.Count;
            //                        dtGrdPurdtl.CurrentCell = dtGrdPurdtl[1, rowNo];
            //                        dtGrdPurdtl.BeginEdit(true);
            //                    }
            //                    else
            //                    {
            //                        if (rowNo > val)
            //                        {
            //                            dtGrdPurdtl.Rows.Remove(dtGrdPurdtl.Rows[rowNo]);
            //                        }
            //                        dtGrdPurdtl.Rows[rowNo].Cells[0].Value = dtGrdPurdtl.Rows.Count;
            //                        dtGrdPurdtl.CurrentCell = dtGrdPurdtl[1, val];
            //                        dtGrdPurdtl.BeginEdit(true);
            //                    }
            //                }
            //                else if (dtGrdPurdtl.Rows.Count > 0)
            //                {
            //                    dtGrdPurdtl.CurrentCell = dtGrdPurdtl[1, 0];
            //                    dtGrdPurdtl.BeginEdit(true);
            //                }                           
            //        }
            //    }
            //    else if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete||txtGL_Code_1.Text=="0")
            //    {
            //       lblGLName.Text = "";
            //    }
            //}
            // catch (Exception ex)
            // {
            //     string str = "Message:" + ex.Message;
            //     MessageBox.Show(str, "Error Message");
            // }
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
                        TaxMaster_Load(sender, EventArgs.Empty);
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


        private void DtgridTaxMast_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                FillDetails();
                flag = 1;
                mod.gridDoubleClick(ChkDeleted, BtnDelete, txtName);
                lblSucessMsg.Text = "";
                BtnSave.Enabled = true;

            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }


        //private void dtGrdPurdtl_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        //{
        //    try
        //    {
        //        //if (e. == rowNo)
        //        {
        //            e.Control.PreviewKeyDown -= Control_PreviewKeyDown;
        //            e.Control.PreviewKeyDown += new PreviewKeyDownEventHandler(Control_PreviewKeyDown);

        //            e.Control.GotFocus -= dataGridViewTextBox_GotFocus;
        //            e.Control.GotFocus += new EventHandler(dataGridViewTextBox_GotFocus);

        //            tb = (DataGridViewTextBoxEditingControl)e.Control;
        //            tb.KeyPress += new KeyPressEventHandler(dataGridViewTextBox_KeyPress);
        //          //  e.Control.KeyPress += new KeyPressEventHandler(dataGridViewTextBox_KeyPress);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string str = "Message:" + ex.Message;
        //        MessageBox.Show(str, "Error Message");
        //    }
        //}


        private void dataGridViewTextBox_GotFocus(object sender, EventArgs e)
        {

            TextBox obj = sender as TextBox;
            obj.BackColor = Color.Yellow;
            //  obj.Focus();

        }

        private void dataGridViewTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {

                //input numeric value 
                TextBox obj = sender as TextBox;
                if (!(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == '.'))  // check if backspace is pressed
                {
                    e.Handled = true;
                }

                if (e.KeyChar == 43)
                {

                    if (dtGrdPurdtl.RowCount > 0)
                    {
                        //plusKeyPress = true;
                        int i = dtGrdPurdtl.CurrentCell.RowIndex;
                        if (Convert.ToString(dtGrdPurdtl.Rows[i].Cells[1].Value) == string.Empty)
                        {
                            rowNo = rowNo - 1;
                            dtGrdPurdtl.Rows.RemoveAt(i);
                        }
                        if (Convert.ToString(dtGrdPurdtl.Rows[rowNo].Cells[3].Value) != string.Empty)
                        {
                            BtnSave.Enabled = true;
                            BtnSave.Focus();
                        }
                        else
                        {
                            dtGrdPurdtl.CurrentCell = dtGrdPurdtl[3, rowNo];
                            dtGrdPurdtl.BeginEdit(true);
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



        private void txtsearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && txtsearch.Text == "")
            {
                BtnAdd.Focus();
            }
            lblSucessMsg.Text = "";

        }

        private void txtTallyCode_Enter(object sender, EventArgs e)
        {
            txtTallyCode.BackColor = Color.Yellow;
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

            try
            {
                Globalvariable.TaxMastVarb = "TallyID";
                if (txtTallyCode.Text == "" && e.KeyCode == Keys.Enter)
                {
                    Globalvariable.searchNo = 1;
                    KeysConverter kc = new KeysConverter();
                    string key = kc.ConvertToString(e.KeyCode);
                    FrmSearch srch = new FrmSearch();
                    // str = "SELECT Tally_Name,Tally_Code FROM tbTallyMaster WHERE Deleted='N' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY Tally_Code";
                    srchda = mod.GetselectQuery("tbTallyMaster", "Tally_Code", "Tally_Name", "N", "N");
                    srch.val = key;
                    //  srch.fillbackgrid(str, "FrmSupplierMaster");
                    //  SqlDataReader dr1 = mod.GetRecord(str);
                    srch.fillbackgridSrch(srchda, "FrmSupplierMaster");
                    ds.Clear();
                    srchda.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                        srch.ShowDialog();
                    txtTallyCode.Text = srch.codeselected;
                    if (srch.codeselected == null)
                    {
                        txtTallyCode.Focus();
                    }
                    else
                    {
                        filltallycode();
                    }
                    Globalvariable.searchNo = 0;
                    if (lblTallyName.Text != "")
                    {
                        txtGL_Code_1.Focus();
                    }
                    else
                    {
                        txtTallyCode.Focus();
                    }
                }
                else if (e.KeyCode == Keys.Enter && txtTallyCode.Text != "" && txtTallyCode.Text != "0")
                {
                    filltallycode();
                }
                else if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete || txtTallyCode.Text == "0")
                {
                    if (txtTallyCode.Text != "0")
                    {
                        txtTallyCode.Text = "";
                        lblTallyName.Text = "";
                    }
                    else if (e.KeyCode != Keys.Back && txtTallyCode.Text == "0")
                    {
                        txtGL_Code_1.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
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
                    SqlDataReader dr = mod.GetRecord("SELECT Tally_Name FROM tbTallyMaster WHERE Deleted='N' AND Branchcode='" + Globalvariable.bcode +
                            "' AND Tally_Code='" + txtTallyCode.Text + "'");
                    if (dr.HasRows)
                    {
                        dr.Read();
                        lblTallyName.Text = dr["Tally_Name"].ToString();
                        //   txtGL_Code_1.Focus();
                    }
                    else
                    {
                        txtTallyCode.Text = "";
                        lblTallyName.Text = "";
                        //  txtTallyCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void txtTallyCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void txtGL_Code_1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }




        private void Control_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                BtnSave.Enabled = false;
                //  lblSuccess.Text = "";
                //DupCode1 = "0";
                if (dtGrdPurdtl.Rows.Count == 0)
                {
                    dtGrdPurdtl.Rows.Add();
                    dtGrdPurdtl.Rows.Add();
                    rowNo = dtGrdPurdtl.RowCount - 1;
                    dtGrdPurdtl.Rows[rowNo].Cells[0].Value = dtGrdPurdtl.Rows.Count;
                    dtGrdPurdtl.CurrentCell = dtGrdPurdtl[1, rowNo];
                    dtGrdPurdtl.BeginEdit(true);
                    return;
                }

                rowNo = dtGrdPurdtl.CurrentCell.RowIndex;
                colNo = dtGrdPurdtl.CurrentCell.ColumnIndex;
                if (colNo == 1)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        dtGrdPurdtl.Rows[rowNo].Cells[1].Value = tb.Text;
                        if (tb.Text != string.Empty)
                        {
                            dtGrdPurdtl.CurrentCell = dtGrdPurdtl[2, rowNo];
                            //  dtGrdPurdtl.BeginEdit(true);
                        }
                        else
                        {
                            rowNo = dtGrdPurdtl.RowCount - 1;
                            dtGrdPurdtl.CurrentCell = dtGrdPurdtl[1, rowNo];
                            dtGrdPurdtl.BeginEdit(true);
                        }
                    }
                }
                else if (colNo == 2)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        dtGrdPurdtl.Rows[rowNo].Cells[2].Value = tb.Text;
                        if (tb.Text != string.Empty)
                        {
                            dtGrdPurdtl.CurrentCell = dtGrdPurdtl[3, rowNo];
                            // dtGrdPurdtl.BeginEdit(true);
                        }
                        else
                        {
                            rowNo = dtGrdPurdtl.RowCount - 1;
                            dtGrdPurdtl.CurrentCell = dtGrdPurdtl[2, rowNo];
                            dtGrdPurdtl.BeginEdit(true);
                        }
                    }
                }
                else if (colNo == 3)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        dtGrdPurdtl.Rows[rowNo].Cells[3].Value = tb.Text;
                        if (tb.Text != string.Empty)
                        {
                            int rowcount = dtGrdPurdtl.RowCount;
                            if (rowcount - 1 > rowNo)
                            {
                                dtGrdPurdtl.CurrentCell = dtGrdPurdtl[1, rowNo + 1];
                                //dtGrdPurdtl.BeginEdit(true);
                            }
                            else
                            {
                                dtGrdPurdtl.Rows.Add();
                                rowNo = dtGrdPurdtl.RowCount - 1;
                                dtGrdPurdtl.Rows[rowNo].Cells[0].Value = dtGrdPurdtl.Rows.Count;
                                dtGrdPurdtl.CurrentCell = dtGrdPurdtl[1, rowNo];
                                dtGrdPurdtl.BeginEdit(true);
                            }
                        }
                        else
                        {
                            rowNo = dtGrdPurdtl.RowCount - 1;
                            dtGrdPurdtl.CurrentCell = dtGrdPurdtl[3, rowNo];
                            dtGrdPurdtl.BeginEdit(true);
                        }
                    }
                }
                if (e.KeyCode == Keys.Add)
                {

                    if (dtGrdPurdtl.RowCount > 0)
                    {
                        //plusKeyPress = true;
                        int i = dtGrdPurdtl.CurrentCell.RowIndex;
                        if (Convert.ToString(dtGrdPurdtl.Rows[i].Cells[1].Value) == string.Empty)
                        {
                            rowNo = rowNo - 1;
                            dtGrdPurdtl.Rows.RemoveAt(i);
                        }
                        if (Convert.ToString(dtGrdPurdtl.Rows[rowNo].Cells[3].Value) != string.Empty)
                        {
                            BtnSave.Enabled = true;
                            BtnSave.Focus();
                        }
                        else
                        {
                            dtGrdPurdtl.CurrentCell = dtGrdPurdtl[3, rowNo];
                            dtGrdPurdtl.BeginEdit(true);
                        }
                    }
                }
                e.IsInputKey = true;
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void dtGrdPurdtl_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
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



        private void txtsearch_Leave(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.White;
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            txtName.BackColor = Color.White;
        }

        //private void txtTaxAmt_Leave(object sender, EventArgs e)
        //{
        //   txtTaxAmt.BackColor = Color.White;
        //}

        private void txtTallyCode_Leave(object sender, EventArgs e)
        {
            txtTallyCode.BackColor = Color.White;
        }

        private void txtGL_Code_1_Leave(object sender, EventArgs e)
        {
            txtGL_Code_1.BackColor = Color.White;
        }

        private void btnLessID_Click(object sender, EventArgs e)
        {

            Models.Common.FormManagement.Instance.SelectRecordFromGrid(DtgridTaxMast, Models.Common.RecordMove.Previous, FillDetails);

        }

        private void btnGreaterID_Click(object sender, EventArgs e)
        {

            Models.Common.FormManagement.Instance.SelectRecordFromGrid(DtgridTaxMast, Models.Common.RecordMove.Next, FillDetails);

        }

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TaxMaster_KeyDown(object sender, KeyEventArgs e)
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

        private void dtGrdPurdtl_CellEnter_1(object sender, DataGridViewCellEventArgs e)
        {
            dtGrdPurdtl[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.Yellow;

        }

        private void dtGrdPurdtl_CellLeave_1(object sender, DataGridViewCellEventArgs e)
        {
            dtGrdPurdtl[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.Empty;
        }

        private void TaxMaster_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            try
            {
                Control nextTab;
                if (e.KeyChar == (char)Keys.Enter)
                {
                    nextTab = ((Control)sender);
                    if (ActiveControl.Name == "txtsearch" && DtgridTaxMast.Rows.Count == 0 && e.KeyChar == (char)Keys.Enter)
                    {
                        txtsearch.Focus();
                    }

                    else if (ActiveControl.Name == "dtGrdPurdtl" && dtGrdPurdtl.RowCount > 0)
                    {
                        rowNo = dtGrdPurdtl.CurrentCell.RowIndex;
                        colNo = dtGrdPurdtl.CurrentCell.ColumnIndex;
                        dtGrdPurdtl.CurrentCell = dtGrdPurdtl[colNo, rowNo];
                    }

                    else if (ActiveControl.Name == "txtsearch" && DtgridTaxMast.Rows.Count == 0)
                    {
                        nextTab = BtnAdd;
                    }
                    else if (ActiveControl.Name == "txtTallyCode")
                    {
                        if (txtTallyCode.Text.Trim().Equals(""))
                        {
                            txtTallyCode.Focus();
                        }
                        else
                        {
                            txtGL_Code_1.Focus();
                        }
                    }
                    else if (ActiveControl.Name == "txtGL_Code_1")
                    {
                        if (txtGL_Code_1.Text.Trim().Equals(""))
                        {
                            txtGL_Code_1.Focus();
                        }
                        else
                        {
                            dtGrdPurdtl.Focus();
                            ActiveControl = dtGrdPurdtl;
                        }
                    }
                    else
                    {
                        if (BtnDelete.Text != "RECALL")
                            nextTab = GetNextControl(ActiveControl, true);
                    }
                    //   nextTab.Focus();
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void dtGrdPurdtl_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
                BtnSave.Enabled = false;
                //  lblSuccess.Text = "";
                //DupCode1 = "0";
                if (dtGrdPurdtl.Rows.Count == 0)
                {
                    dtGrdPurdtl.Rows.Add();
                    rowNo = dtGrdPurdtl.RowCount - 1;
                    dtGrdPurdtl.Rows[rowNo].Cells[0].Value = dtGrdPurdtl.Rows.Count;
                    dtGrdPurdtl.CurrentCell = dtGrdPurdtl[1, rowNo];
                    dtGrdPurdtl.BeginEdit(true);
                    return;
                }


                rowNo = dtGrdPurdtl.CurrentCell.RowIndex;
                colNo = dtGrdPurdtl.CurrentCell.ColumnIndex;
                if (colNo == 1)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        // dtGrdPurdtl.Rows[rowNo].Cells[1].Value = tb.Text;
                        if (tb!=null && tb.Text != string.Empty)
                        {
                            dtGrdPurdtl.CurrentCell = dtGrdPurdtl[2, rowNo];
                            dtGrdPurdtl.BeginEdit(true);
                            e.Handled = true;
                        }
                        else
                        {
                            rowNo = dtGrdPurdtl.RowCount - 1;
                            dtGrdPurdtl.CurrentCell = dtGrdPurdtl[1, rowNo];
                            dtGrdPurdtl.BeginEdit(true);
                            e.Handled = true;
                        }
                    }
                }
                else if (colNo == 2)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        // dtGrdPurdtl.Rows[rowNo].Cells[2].Value = tb.Text;
                        if (tb.Text != string.Empty)
                        {
                            dtGrdPurdtl.CurrentCell = dtGrdPurdtl[3, rowNo];
                            dtGrdPurdtl.BeginEdit(true);
                            e.Handled = true;
                        }
                        else
                        {
                            rowNo = dtGrdPurdtl.RowCount - 1;
                            dtGrdPurdtl.CurrentCell = dtGrdPurdtl[2, rowNo];
                            dtGrdPurdtl.BeginEdit(true);
                        }
                    }
                }
                else if (colNo == 3)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        //  dtGrdPurdtl.Rows[rowNo].Cells[3].Value = tb.Text;
                        int perVal = 0;
                        int.TryParse(tb.Text, out perVal);
                        if (tb.Text != string.Empty && perVal > 0)
                        {
                            int rowcount = dtGrdPurdtl.RowCount;
                            if (rowcount - 1 > rowNo)
                            {
                                dtGrdPurdtl.CurrentCell = dtGrdPurdtl[1, rowNo + 1];
                                // dtGrdPurdtl.BeginEdit(true);
                                e.Handled = true;
                            }
                            else
                            {
                                dtGrdPurdtl.Rows.Add();
                                rowNo = dtGrdPurdtl.RowCount - 1;
                                dtGrdPurdtl.Rows[rowNo].Cells[0].Value = dtGrdPurdtl.Rows.Count;
                                dtGrdPurdtl.CurrentCell = dtGrdPurdtl[1, rowNo];
                                dtGrdPurdtl.BeginEdit(true);
                            }
                        }
                        else
                        {
                            rowNo = dtGrdPurdtl.RowCount - 1;
                            dtGrdPurdtl.CurrentCell = dtGrdPurdtl[3, rowNo];
                            dtGrdPurdtl.BeginEdit(true);
                        }
                    }
                }
                if (e.KeyCode == Keys.Add)
                {

                    if (dtGrdPurdtl.RowCount > 0)
                    {
                        //plusKeyPress = true;
                        int i = dtGrdPurdtl.CurrentCell.RowIndex;
                        if (Convert.ToString(dtGrdPurdtl.Rows[i].Cells[1].Value) == string.Empty)
                        {
                            rowNo = rowNo - 1;
                            dtGrdPurdtl.Rows.RemoveAt(i);
                        }
                        if (Convert.ToString(dtGrdPurdtl.Rows[rowNo].Cells[3].Value) != string.Empty)
                        {
                            BtnSave.Enabled = true;
                            BtnSave.Focus();
                        }
                        else
                        {
                            dtGrdPurdtl.CurrentCell = dtGrdPurdtl[3, rowNo];
                            dtGrdPurdtl.BeginEdit(true);
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

        private void DtgridTaxMast_CellClick(object sender, DataGridViewCellEventArgs e)
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

        private void TaxMaster_Shown(object sender, EventArgs e)
        {
            txtsearch.Focus();
        }




    }
}




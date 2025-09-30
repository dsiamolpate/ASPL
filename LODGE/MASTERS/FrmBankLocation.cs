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
    public partial class FrmBankLocation : Form
    {
        Design ObjDesign = new Design();
        public FrmBankLocation()
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
        Module mod = new Module();
        Connection con = new Connection();
        string firstCol = "Bank_Code";
        string secondCol = "Bank_Location";
        string tableName = "tbBankLocation";
        string Deleted = "Deleted";
         string strsrch, chksub, str, StrUpdate;
        int selectrow;
        private void FrmBankLocation_Load(object sender, EventArgs e)
        {
            load();
        }

        private void txtLocation_Enter(object sender, EventArgs e)
        {
            txtLocation.BackColor = Color.Yellow;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtsearch_Enter(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.Yellow;
        }

       public void load()
        {
            try
            {
                ObjDesign.FormDesign(this, DtgridbankLocation);
                mod.FormLoad(this, tableName, firstCol, secondCol, Deleted, "N", "N", DtgridbankLocation, txtsearch, ChkDeleted, "Description", "Code");
                txtsearch.Focus();
               FillDetails();
               mod.UserAccessibillityMaster("Bank Location", BtnAdd, BtnSave, BtnDelete);
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
               if (ChkDeleted.Checked == true)
               {
                   mod.chkDeletedTrue(this, BtnAdd, BtnSave, BtnDelete, DtgridbankLocation, txtsearch);
                   txtsearch.Focus();
               }
               else
               {
                   mod.chkDeletedFalse("Bank Location",this, BtnAdd, BtnSave, BtnDelete, DtgridbankLocation, txtsearch);
                   ChkDeleted.Enabled = true;
               }

               if (DtgridbankLocation.Rows.Count > 0)
               {
                   int i;
                   if (StrUpdate == "U")
                   {
                       i = selectrow;
                   }
                   else
                   {
                       i = DtgridbankLocation.SelectedCells[0].RowIndex;
                   }
                   if (DtgridbankLocation.Rows[i].Cells[1].Value != null)
                   {
                       string ID = mod.Isnull(DtgridbankLocation.Rows[i].Cells[1].Value.ToString(), "");
                       //SqlDataReader dr = mod.GetRecord("SELECT * FROM " + tableName + " WHERE " +
                       //                     firstCol + " ='" + s + "' AND Branchcode='" + Globalvariable.bcode + "'");
                       SqlDataReader dr = mod.GetSelectAllField(tableName, firstCol, ID,"");
                       while (dr.Read())
                       {
                           txtBankCode.Text = dr["Bank_Code"].ToString();
                           txtBankCode.Enabled = false;
                           txtLocation.Text = dr["Bank_Location"].ToString();
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

       private void txtsearch_KeyDown(object sender, KeyEventArgs e)
       {
           DtgridbankLocation_KeyDown(sender, e);
       }

       private void DtgridbankLocation_KeyDown(object sender, KeyEventArgs e)
       {
           try
           {
               mod.gridKeyDown(e, BtnDelete, txtsearch, DtgridbankLocation, this);
               FillDetails();
               if (e.KeyCode == Keys.Enter && ChkDeleted.Checked == true && DtgridbankLocation.Rows.Count > 0)
               {
                   BtnDelete.Focus();
               }
               else if (e.KeyCode == Keys.Enter && ChkDeleted.Checked == true && DtgridbankLocation.Rows.Count == 0)
               {
                   BtnExit.Focus();
               }
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

       private void txtsearch_TextChanged(object sender, EventArgs e)
       {
           try
           {
               mod.txtSearchChange(ChkDeleted, firstCol, secondCol, tableName, DtgridbankLocation, txtsearch, "Bank Location", "Code");
               FillDetails();
               lblSucessMsg.Text = "";
           }
           catch (Exception ex)
           {
               string str = "Message:" + ex.Message;
               MessageBox.Show(str, "Error Message");
           }
       }

       private void DtgridbankLocation_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
       {
           FillDetails();
           mod.gridDoubleClick(ChkDeleted, BtnDelete, txtLocation);
           lblSucessMsg.Text = "";
           BtnSave.Enabled = true;

       }

       private void BtnAdd_Click(object sender, EventArgs e)
       {
           add();
       }

       public void add()
       {
           try
           {
               mod.addBtnClick(this, txtBankCode, tableName, firstCol, BtnSave, BtnDelete, txtLocation);
               mod.UserAccessibillityMaster("Bank Location", BtnAdd, BtnSave, BtnDelete);
               txtBankCode.Text = "" + mod.GetMaxNum(tableName, firstCol);
               lblSucessMsg.Text = "";
               label1.Text = "Search by Name :";
           }
           catch (Exception ex)
           {
               string str = "Message:" + ex.Message;
               MessageBox.Show(str, "Error Message");
           }
       }

       public void max_value()
       {
           try
           {
               SqlCommand cmd = new SqlCommand("SELECT (ISNULL(MAX(Bank_Code),0)+1) FROM tbBankLocation WHERE Branchcode='" + Globalvariable.bcode + "'", con.connect());
               cmd.ExecuteNonQuery();
               int count = Convert.ToInt32(cmd.ExecuteScalar());
               txtBankCode.Text = "" + count;
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
                   i = mod.deleteButtonClick(tableName, firstCol, txtBankCode.Text,
                        txtsearch, BtnDelete.Text.Substring(0, 1));
                   if (i == 1)
                   {
                       FrmBankLocation_Load(sender, EventArgs.Empty);
                       if (strMsg == "DELETE")
                          lblSucessMsg.Text= "Deleted Successfully !!!";
                       else
                          lblSucessMsg.Text= "Recalled Successfully !!!";
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

       private void BtnSave_Click(object sender, EventArgs e)
       {
           try
           {
               if (DtgridbankLocation.Rows.Count > 0)
               {
                   selectrow = DtgridbankLocation.SelectedCells[0].RowIndex;
               }
               int i;
               if (txtLocation.Text == "")
               {
                   txtLocation.Focus();
               }
               else
               {
                 
                   if (BtnSave.Text == "SAVE")
                       str = "SELECT * FROM tbBankLocation WHERE Bank_Location='" + txtLocation.Text.Replace("'", "''") +
                           "' AND Branchcode='" + Globalvariable.bcode + "'";
                   //  str =" SELECT * FROM tbGroup  WHERE description='"+txtName.Text+"' AND Branchcode='"+Globalvariable.bcode+"'";
                   else if (BtnSave.Text == "UPDATE")
                       str = "SELECT * FROM tbBankLocation  WHERE Bank_Location='" + txtLocation.Text.Replace("'", "''") +
                           "' AND Branchcode='" + Globalvariable.bcode + "' AND Bank_Code !=" + txtBankCode.Text + "";
                   SqlDataReader dr = mod.GetRecord(str);
                   if (dr.HasRows)
                   {
                       MessageBox.Show("Name Already Exist!!");
                       txtLocation.Focus();
                   }
                   else
                   {

                           if (BtnSave.Text == "UPDATE")
                           {
                               DataSet ds = new DataSet();
                               str = "SELECT * FROM tbBankLocation  WHERE Branchcode='" + Globalvariable.bcode + "' AND Bank_Code =" + txtBankCode.Text + "";
                               dr = mod.GetRecord(str);
                               while (dr.Read())
                               {
                                   mod.compare(txtLocation.Text, dr["Bank_Location"].ToString(), "Bank Location");                             
                               }
                           }
                            BtnSave.Enabled = false;                  
                           //str = "INSERT INTO tbBankLocation(Bank_Code,Bank_Location,deleted,Branchcode) VALUES(" + txtBankCode.Text +
                           //         ",'" + txtLocation.Text.Replace("'", "''") + "','N','" + Globalvariable.bcode + "' )";
                           //i = mod.SaveMaster(str);
                           SqlCommand cmd = new SqlCommand("SP_INSERTBankLocation", con.connect());
                           cmd.CommandType = CommandType.StoredProcedure;
                           if (BtnSave.Text == "SAVE")
                           {
                           txtBankCode.Text = "" + mod.GetMaxNum(tableName, firstCol);
                           cmd.Parameters.AddWithValue("@variable", "INSERT");
                           }
                           else
                           { 
                               cmd.Parameters.AddWithValue("@variable", "UPDATE");
                           }
                           cmd.Parameters.AddWithValue("@code", txtBankCode.Text);
                           cmd.Parameters.AddWithValue("@name", txtLocation.Text.Replace("'", "''"));
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
                            //  fillGrid();
                           mod.DataGridBind(DtgridbankLocation, tableName, firstCol, secondCol, "N", "Location", "Code");                    
                           txtsearch.Focus();
                           txtsearch.Text = "";
                           string txt = BtnSave.Text;
                           FillDetails();
                           int s = selectrow;
                           if (DtgridbankLocation.Rows.Count > 0)
                           {
                               DtgridbankLocation.CurrentCell = DtgridbankLocation[0, s];
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
        
       private void FrmBankLocation_KeyPress(object sender, KeyPressEventArgs e)
       {
           try
           {
               Control nextTab;
               if (e.KeyChar == (char)Keys.Enter)
               {
                   nextTab = ((Control)sender);
                   if (ActiveControl.Name == "txtsearch" && DtgridbankLocation.Rows.Count == 0 && e.KeyChar == (char)Keys.Enter)
                   {
                       txtsearch.Focus();
                   }
                   else
                   {
                       if (ActiveControl.Name == "txtsearch" && DtgridbankLocation.Rows.Count == 0)
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

       private void txtLocation_KeyPress(object sender, KeyPressEventArgs e)
       {
           if (e.KeyChar == (char)Keys.Enter)
           {
               BtnSave.Focus();
           }
       }

       private void ChkDeleted_CheckedChanged(object sender, EventArgs e)
       {
           try
           {
               mod.chkDeletedChange(ChkDeleted, firstCol, secondCol, tableName, Deleted, "N", DtgridbankLocation, txtsearch, "Location", "Code");
               FillDetails();
               lblSucessMsg.Text = "";
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

       private void txtLocation_Leave(object sender, EventArgs e)
       {
           txtsearch.BackColor = Color.White;
       }

       private void FrmBankLocation_KeyDown(object sender, KeyEventArgs e)
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

            Models.Common.FormManagement.Instance.SelectRecordFromGrid(DtgridbankLocation, Models.Common.RecordMove.Previous, FillDetails);

        }

        private void btnGreaterID_Click(object sender, EventArgs e)
        {

            Models.Common.FormManagement.Instance.SelectRecordFromGrid(DtgridbankLocation, Models.Common.RecordMove.Next, FillDetails);

        }

        private void FrmBankLocation_KeyPress_1(object sender, KeyPressEventArgs e)
       {
           try
           {
               Control nextTab;
               if (e.KeyChar ==(char)Keys.Enter)
               {
                   nextTab = ((Control)sender);
                   if (ActiveControl.Name == "txtsearch" && DtgridbankLocation.Rows.Count == 0 && e.KeyChar == (char)Keys.Enter)
                   {
                       txtsearch.Focus();
                   }
                   else
                   {
                       if (ActiveControl.Name == "txtsearch" && DtgridbankLocation.Rows.Count == 0)
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

       private void DtgridbankLocation_CellClick(object sender, DataGridViewCellEventArgs e)
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

       private void FrmBankLocation_Shown(object sender, EventArgs e)
       {
           txtsearch.Focus();
       }

    }
}

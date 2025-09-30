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

namespace ASPL.LODGE.TRANSCTION
{
    public partial class FrmBankReconcilation : Form
    {
        Design ObjDesign = new Design();
        DataGridViewTextBoxEditingControl tb;
        public FrmBankReconcilation()
        {
            InitializeComponent();
        }

        string voch_dt, fillCDO = "";
        int detail_id = 0, grdchange, escGrid, m, mnth, flag = 0;
        public string m_bkcode;
        public static string TABLENAME;
        SqlDataReader srchdr;
        SqlDataAdapter srchda;
        DataSet ds = new DataSet();
        Connection con = new Connection();
        SqlCommand cmd;
        Module mod = new Module();
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
        private void dtgridBankReconcilation_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void dtgridBankReconcilation_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            dtgridBankReconcilation[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.Yellow;
        }

        private void dtgridBankReconcilation_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            dtgridBankReconcilation[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.White;
        }

        private void FrmBankReconcilation_KeyPress(object sender, KeyPressEventArgs e)
        {

         
        }

        private void dataGridViewTextBox_GotFocus(object sender, EventArgs e)
        {
            TextBox obj = sender as TextBox;
            obj.BackColor = Color.Yellow;
            obj.Focus();
        }
  
        private void Control_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

            e.IsInputKey = true;
            if(dtgridBankReconcilation.Rows.Count>0)
            {
            int iColumn = dtgridBankReconcilation.CurrentCell.ColumnIndex;
            int iRow = dtgridBankReconcilation.CurrentCell.RowIndex;
            if (iColumn == dtgridBankReconcilation.ColumnCount - 1)
            {
                if (dtgridBankReconcilation.RowCount > (iRow + 1))
                {
                    dtgridBankReconcilation.CurrentCell = dtgridBankReconcilation[1, iRow + 1];
                   // dtgridBankReconcilation.BeginEdit(true);
                 
                }
                else if (iColumn == 5)
                {

                    dtgridBankReconcilation.Rows.Add();
                    dtgridBankReconcilation.Rows[iRow].Cells[0].Value = "Cr";
                    dtgridBankReconcilation.CurrentCell = dtgridBankReconcilation[1, iRow + 1];
                    dtgridBankReconcilation.BeginEdit(true);
                   
                }
            }
            else
                dtgridBankReconcilation.CurrentCell = dtgridBankReconcilation[iColumn + 1, iRow];
          //  dtgridBankReconcilation.BeginEdit(true);
            }
            
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtBookCode_Enter(object sender, EventArgs e)
        {
            txtBookCode.BackColor = Color.Yellow;
        }

        private void txtBookCode_Leave(object sender, EventArgs e)
        {
            txtBookCode.BackColor = Color.White;
        }

        private void FrmBankReconcilation_Load(object sender, EventArgs e)
        {
            ObjDesign.FormDesign(this, dtgridBankReconcilation);
            txtBookCodeNM.Enabled = false;
            txtBookCode.Focus();
            //set maximum date
            dtpFromDate.MaxDate =Convert.ToDateTime(Globalvariable.EndDate);
            //set minimum date
            dtpFromDate.MinDate = Convert.ToDateTime(Globalvariable.StartDate);
            //set maximum date
            dtpToDate.MaxDate = Convert.ToDateTime(Globalvariable.EndDate);
            //set minimum date
            dtpToDate.MinDate = Convert.ToDateTime(Globalvariable.StartDate);
            //set start date
            dtpFromDate.Value = Convert.ToDateTime(Globalvariable.StartDate);
            //set end date
            dtpToDate.MinDate = Convert.ToDateTime(Globalvariable.EndDate);
            btnUnclear.Enabled = false;
            btnClear.Enabled = false;
            txtBalanceAsPerStatement.Enabled = false;
            txtCheckDeposited.Enabled = false;
            txtBalanceAsPerStatement.Enabled = false;
            txtBalAsPerBankBook.Enabled = false;
            FillGrid();
        }

        public void FillGrid()
        {
            DataTable dt = new DataTable();
            dtgridBankReconcilation.Columns.Clear();
            dtgridBankReconcilation.AutoGenerateColumns = true;
            dt.Columns.Add("VouchNo", typeof(string));
            dt.Columns.Add("Cheque Date", typeof(string));
            dt.Columns.Add("Amount Rs.", typeof(string));
            dt.Columns.Add("Clearance Date", typeof(string));
            dt.Columns.Add("Voucher Type", typeof(string));
            dt.Columns.Add("Cheque No.", typeof(string));
            dtgridBankReconcilation.DataSource = dt;
            dtgridBankReconcilation.Columns[0].Width = 90;
            dtgridBankReconcilation.Columns[1].Width = 140;
            dtgridBankReconcilation.Columns[2].Width = 153;
            dtgridBankReconcilation.Columns[3].Width = 150;
            dtgridBankReconcilation.Columns[4].Width = 110;
            dtgridBankReconcilation.Columns[5].Width = 120;    
        }
        private void fillBookCode()
        {
            try
            {
                if (txtBookCode.Text.Trim().Equals(""))
                {
                    txtBookCodeNM.Text = "";
                }
                else
                {
                    SqlDataReader dr = mod.GetRecord("SELECT book_description FROM tbBook WHERE deleted='N' AND Branchcode='" + Globalvariable.bcode +
                        "' AND book_code!=1");
                    if (dr.HasRows)
                    {
                        dr.Read();
                        txtBookCodeNM.Text = dr["book_description"].ToString();
                        //   txtSingleRate.Focus();
                    }
                    else
                    {
                        txtBookCode.Text = "";
                        txtBookCodeNM.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void txtBookCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && txtBookCode.Text != "")
                {
                    fillBookCode();
                }
                else if (txtBookCode.Text == "" && e.KeyCode == Keys.Enter)
                {
                    Globalvariable.searchNo = 1;
                    KeysConverter kc = new KeysConverter();
                    string key = kc.ConvertToString(e.KeyCode);
                    FrmSearch srch = new FrmSearch();
                    // str = "SELECT catgry_name,catgry_code FROM RoomCategoryMaster WHERE deleted='N'AND Branchcode='" + Globalvariable.bcode + "'";
                    cmd = new SqlCommand("SP_TransBankReconcillation", con.connect());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@variable", "SELECT");
                    cmd.Parameters.AddWithValue("@code", "book_code");
                    cmd.Parameters.AddWithValue("@name", "book_description");
                    cmd.Parameters.AddWithValue("@tableName", "tbBook");
                    cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                    srchda = new SqlDataAdapter(cmd);
                    srch.val = key;
                    srch.fillbackgridSrch(srchda, "FrmBankReconcilation");
                    ds.Clear();
                    srchda.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                        srch.ShowDialog();
                    txtBookCode.Text = srch.codeselected;
                    if (srch.codeselected == null)
                    {
                        txtBookCode.Focus();
                    }
                    else
                    {
                        fillBookCode();
                    }
                    Globalvariable.searchNo = 0;
                    if (txtBookCodeNM.Text != "")
                    {
                        BtnReceipt.Focus();
                    }
                    else
                    {
                        txtBookCode.Focus();
                    }
                }
                else if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
                {
                    txtBookCode.Text = "";
                    txtBookCodeNM.Text = "";
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        public void clickButtons(string btnName)
        {
           
//dtgridBankReconcilation.Rows.Clear();
                DataSet ds1 = new DataSet();
                DataTable dt = new DataTable();
                dtgridBankReconcilation.Columns.Clear();
                dtgridBankReconcilation.DataSource = null;
                cmd = new SqlCommand("SP_TransBankReconcillation", con.connect());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@variable", "SELECT");
                cmd.Parameters.AddWithValue("@CheckString", "Fillgrid");
                cmd.Parameters.AddWithValue("@tableName", TABLENAME);
                cmd.Parameters.AddWithValue("@DayBookCode", txtBookCode.Text);
                cmd.Parameters.AddWithValue("@BtnClick", btnName);
                cmd.Parameters.AddWithValue("@Start_Date", Globalvariable.StartDate);
                cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                SqlDataAdapter da = new SqlDataAdapter(cmd);          
                dt.Clear();
                da.Fill(dt);
                    dtgridBankReconcilation.DataSource = dt;
                    dtgridBankReconcilation.Columns[0].Width = 90;
                    dtgridBankReconcilation.Columns[1].Width = 140;
                    dtgridBankReconcilation.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
                    dtgridBankReconcilation.Columns[2].Width = 153;
                    dtgridBankReconcilation.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                    dtgridBankReconcilation.Columns[3].Width = 150;
                    dtgridBankReconcilation.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
                    dtgridBankReconcilation.Columns[4].Width = 110;
                    dtgridBankReconcilation.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
                    dtgridBankReconcilation.Columns[5].Width = 120;
                    if (dtgridBankReconcilation.Rows.Count>0)
                    {
                        SqlDataReader dr1;
                        if (TABLENAME=="ALL")
                        {
                            dr1 = mod.GetRecord("SELECT Narration FROM tbTransReceipt WHERE Voucher_No='" + dtgridBankReconcilation.Rows[0].Cells[0].Value + "'");
                        }
                        else
                        {
                        dr1 = mod.GetRecord("SELECT Narration FROM " + TABLENAME + " WHERE Voucher_No='" + dtgridBankReconcilation.Rows[0].Cells[0].Value + "'");
                        }
                       while( dr1.Read())
                       {
                           txtNarration.Text=dr1["Narration"].ToString();
                       }
                    }                  
        }

        private void BtnReceipt_Click(object sender, EventArgs e)
        {
            if (txtBookCode.Text == "")
            {
                txtBookCode.Focus();
            }
            else
            {
                TABLENAME = "tbTransReceipt";
                clickButtons(BtnReceipt.Text);
                BtnReceipt.Enabled = false;
                BtnPayment.Enabled = true;
                BtnAll.Enabled = true;
                btnUnclear.Enabled = true;
                btnClear.Enabled = true;

            }
        }

        private void BtnPayment_Click(object sender, EventArgs e)
        {
            if (txtBookCode.Text == "")
            {
                txtBookCode.Focus();
            }
            else
            {
                TABLENAME = "tbTransPayment";
                clickButtons(BtnPayment.Text);
                BtnReceipt.Enabled = true;
                BtnPayment.Enabled = false;
                BtnAll.Enabled = true;
                btnUnclear.Enabled = true;
                btnClear.Enabled = true;
            }
        }

        private void BtnAll_Click(object sender, EventArgs e)
        {
            if (txtBookCode.Text == "")
            {
                txtBookCode.Focus();
            }
            else
            {
                TABLENAME = "ALL";
                clickButtons(BtnAll.Text);
                BtnReceipt.Enabled = true;
                BtnPayment.Enabled = true;
                BtnAll.Enabled = false;
                btnUnclear.Enabled = true;
                btnClear.Enabled = true;
            }
        }

        private void btnUnclear_Click(object sender, EventArgs e)
        {           
            clickButtons(BtnReceipt.Text);
            btnUnclear.Enabled = false;
            btnClear.Enabled = true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clickButtons(BtnReceipt.Text);
            btnUnclear.Enabled = true;
            btnClear.Enabled = false;
        }

        private void txtBookCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void FrmBankReconcilation_Shown(object sender, EventArgs e)
        {
            txtBookCode.Focus();
        }


    }
}

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
using ASPL.Utility;
using Excel = Microsoft.Office.Interop.Excel; 

namespace ASPL.LODGE.REPORTS
{
    public partial class FrmReport : Form
    {
        Design ObjDesign = new Design();
        public FrmReport()
        {
            InitializeComponent();
        }
       
        Module mod = new Module();
        Connection con = new Connection();
        DataSet ds = new DataSet();
        public static SqlDataReader srchdr;
        public static SqlDataAdapter sqlda;
        DataTable dt=new DataTable();
        SqlCommand CMD;
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

        private void FrmReport_Load(object sender, EventArgs e)
        {
            ObjDesign.FormDesign(this, dtgridFrmReport);
            if (Globalvariable.frmName == "Room List" || Globalvariable.frmName == "Tax List" || Globalvariable.frmName == "Discount List" || Globalvariable.frmName == "Groups Rpt List" || Globalvariable.frmName == "General Ledger Rpt List")
            {
                if (Globalvariable.frmName == "Room List")
                {
                    this.Text = "Room List";
                    CMD = new SqlCommand("SP_INSERTRoomMaster", con.connect());
                    CMD.CommandType = CommandType.StoredProcedure;
                    CMD.Parameters.AddWithValue("@variable", "SELECT_PRINT");
                    CMD.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                }
                else if (Globalvariable.frmName == "Tax List")
                {
                    this.Text = "Tax List";
                    CMD = new SqlCommand("SP_INSERTTaxMast", con.connect());
                    CMD.CommandType = CommandType.StoredProcedure;
                    CMD.Parameters.AddWithValue("@variable", "SELECT_PRINT");
                    CMD.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                }
                else if (Globalvariable.frmName == "Discount List")
                {
                    this.Text = "Discount List";
                    CMD = new SqlCommand("SP_INSERTDiscountMast", con.connect());
                    CMD.CommandType = CommandType.StoredProcedure;
                    CMD.Parameters.AddWithValue("@variable", "SELECT_PRINT");
                    CMD.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                }
                else if (Globalvariable.frmName == "Groups Rpt List")
                {
                    this.Text = "Groups List";
                    CMD = new SqlCommand("SP_INSERTGroup", con.connect());
                    CMD.CommandType = CommandType.StoredProcedure;
                    CMD.Parameters.AddWithValue("@variable", "SELECT_PRINT");
                    CMD.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                }
                else if (Globalvariable.frmName == "General Ledger Rpt List")
                {
                    this.Text = "General Ledger List";
                    CMD = new SqlCommand("SP_INSERTGeneralLeg", con.connect());
                    CMD.CommandType = CommandType.StoredProcedure;
                    CMD.Parameters.AddWithValue("@variable", "SELECT_PRINT");
                    CMD.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                }
                sqlda = new SqlDataAdapter(CMD);
               dt.Clear();
                sqlda.Fill(dt);
                dtgridFrmReport.DataSource = dt;
                if (Globalvariable.frmName == "Room List")
                {
                    dtgridFrmReport.Columns[0].Width = 110;
                    dtgridFrmReport.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                    dtgridFrmReport.Columns[1].Width = 120;
                    dtgridFrmReport.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                    dtgridFrmReport.Columns[2].Width = 238;
                    dtgridFrmReport.Columns[3].Width = 220;
                    dtgridFrmReport.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                }
                else if (Globalvariable.frmName == "Groups Rpt List" || Globalvariable.frmName == "General Ledger Rpt List")
                {
                    dtgridFrmReport.Columns[0].Width = 100;
                    dtgridFrmReport.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                    dtgridFrmReport.Columns[1].Width = 120;
                    dtgridFrmReport.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                    dtgridFrmReport.Columns[2].Width = 250;
                    dtgridFrmReport.Columns[3].Width = 223;
                    dtgridFrmReport.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                }
                else
                {
                    dtgridFrmReport.Columns[0].Width = 100;
                    dtgridFrmReport.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                    dtgridFrmReport.Columns[1].Width = 120;
                    dtgridFrmReport.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                    dtgridFrmReport.Columns[2].Width = 250;
                    dtgridFrmReport.Columns[3].Width = 223;
                    dtgridFrmReport.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                }
            }
            else if (Globalvariable.frmName == "Party List" || Globalvariable.frmName == "Item Coding List" )
            {
                if (Globalvariable.frmName == "Party List")
                {
                    this.Text = "Party List";
                    CMD = new SqlCommand("SP_INSERTSupplerMast", con.connect());
                    CMD.CommandType = CommandType.StoredProcedure;
                    CMD.Parameters.AddWithValue("@variable", "SELECT_PRINT");
                    CMD.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                }
                else if (Globalvariable.frmName == "Item Coding List")
                {
                    this.Text = "Item Coding List";
                    CMD = new SqlCommand("SP_INSERTItemMaster", con.connect());
                    CMD.CommandType = CommandType.StoredProcedure;
                    CMD.Parameters.AddWithValue("@variable", "SELECT_PRINT");
                    CMD.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                }            
                sqlda = new SqlDataAdapter(CMD);
                dt.Clear();
                sqlda.Fill(dt);
                dtgridFrmReport.DataSource = dt;
                dtgridFrmReport.Columns[0].Width = 200;
                dtgridFrmReport.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                dtgridFrmReport.Columns[1].Width = 200;
                dtgridFrmReport.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                dtgridFrmReport.Columns[2].Width = 297;
                //dtgridFrmReport.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                //dtgridFrmReport.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                //dtgridFrmReport.Columns[2].DefaultCellStyle.WrapMode = DataGridViewTriState.True;          
             }
            else if (Globalvariable.frmName == "Guest List")
            {
                this.Text = "Guest List";
                CMD = new SqlCommand("SP_INSERTGuestInfo", con.connect());
                CMD.CommandType = CommandType.StoredProcedure;
                CMD.Parameters.AddWithValue("@variable", "SELECT_PRINT");
                CMD.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                sqlda = new SqlDataAdapter(CMD);
                dt.Clear();
                sqlda.Fill(dt);
                dtgridFrmReport.DataSource = dt;
                dtgridFrmReport.Columns[0].Width = 65;
                dtgridFrmReport.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                dtgridFrmReport.Columns[1].Width = 65;
                dtgridFrmReport.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                dtgridFrmReport.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dtgridFrmReport.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dtgridFrmReport.Columns[2].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dtgridFrmReport.Columns[2].Width = 210;
                dtgridFrmReport.Columns[3].Width = 97;
                dtgridFrmReport.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dtgridFrmReport.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dtgridFrmReport.Columns[4].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dtgridFrmReport.Columns[4].Width = 270;
                //dtgridFrmReport.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;    
            }
            else if (Globalvariable.frmName == "Employee List" || Globalvariable.frmName == "Day Book Rpt List")
            {
                if (Globalvariable.frmName == "Employee List")
                {
                    this.Text = "Employee List";
                    CMD = new SqlCommand("SP_INSERTEmpInfo", con.connect());
                    CMD.CommandType = CommandType.StoredProcedure;
                    CMD.Parameters.AddWithValue("@variable", "SELECT_PRINT");
                    CMD.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                }
                else if (Globalvariable.frmName == "Day Book Rpt List")
                {
                    this.Text = "Day Book List";
                    CMD = new SqlCommand("SP_INSERTDayBook", con.connect());
                    CMD.CommandType = CommandType.StoredProcedure;
                    CMD.Parameters.AddWithValue("@variable", "SELECT_PRINT");
                    CMD.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                }

                sqlda = new SqlDataAdapter(CMD);
                dt.Clear();
                sqlda.Fill(dt);
                dtgridFrmReport.DataSource = dt;
                dtgridFrmReport.Columns[0].Width = 65;
                dtgridFrmReport.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                dtgridFrmReport.Columns[1].Width = 65;
                dtgridFrmReport.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                dtgridFrmReport.Columns[2].Width = 273;
                dtgridFrmReport.Columns[3].Width = 120;
                dtgridFrmReport.Columns[4].Width = 170;              
            }
                        
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                //    char ch = 'A';
                //    if (dtgridFrmReport.Rows.Count > 0)
                //    {
                //        Microsoft.Office.Interop.Excel.ApplicationClass XcelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
                //        Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet = new Microsoft.Office.Interop.Excel.Worksheet();
                //        Excel.Range chartRange;
                //        XcelApp.Application.Workbooks.Add(Type.Missing);

                //        #region Title
                //        ////Add a title
                //        int count = dtgridFrmReport.Columns.Count - 1;
                //        ch = Convert.ToChar(ch + count);
                //        string charcolumnNo = ch.ToString();
                //        XcelApp.Cells[1, 1] = Globalvariable.frmName;

                //        //Span the title across columns A through H
                //        Microsoft.Office.Interop.Excel.Range titleRange = XcelApp.get_Range(XcelApp.Cells[1, "A"], XcelApp.Cells[1, charcolumnNo]);
                //        titleRange.Merge(Type.Missing);

                //        //Center the title horizontally then vertically at the above defined range
                //        titleRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                //        titleRange.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                //        //Increase the font-size of the title
                //        titleRange.Font.Size = 12;
                //        //Make the title bold
                //        titleRange.Font.Bold = true;
                //        //Set the title row height
                //        titleRange.RowHeight = 20;
                //        //Give the title background color
                //        //titleRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                //        #endregion


                //        for (int i = 1; i < dtgridFrmReport.Columns.Count + 1; i++)
                //        {
                //            XcelApp.Cells[2, i] = dtgridFrmReport.Columns[i - 1].HeaderText;
                //        }

                //        for (int i = 0; i < dtgridFrmReport.Rows.Count; i++)
                //        {
                //            for (int j = 0; j < dtgridFrmReport.Columns.Count; j++)
                //            {
                //                XcelApp.Cells[i + 3, j + 1] = dtgridFrmReport.Rows[i].Cells[j].Value.ToString();
                //            }
                //        }

                //        XcelApp.Columns.AutoFit();
                //        XcelApp.Visible = true;
                //    }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            FrmReportShow frmshow = new FrmReportShow();
            frmshow.Show();
            //FrmConnectionString connect = new FrmConnectionString();
            //connect.Show();
        }
    }
}

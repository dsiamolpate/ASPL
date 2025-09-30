using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Windows;
using System.Drawing.Imaging;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;
using System.Net;
using System.IO;
using ASPL.CLASSES;
using ASPL.STARTUP.FORMS;
using ASPL.LODGE.STARTUP_FORM;
using System.Threading;

namespace ASPL.Utility
{
    public partial class FrmChangeFinanYear : Form
    {
        public FrmChangeFinanYear()
        {
            InitializeComponent();
        }

        Module mod = new Module();
        Connection Objcon = new Connection();
        SqlCommand cmd;
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
        private void FrmChangeFinanYear_Load(object sender, EventArgs e)
        {
            cmbCahngYear.Focus();
            fillCombo();
        }

        private void cmbCahngYear_Enter(object sender, EventArgs e)
        {
            cmbCahngYear.BackColor = Color.Yellow;
        }

        private void cmbCahngYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            BtnConnect.Focus();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void fillCombo()
        {
            try
            {
                DataSet ds = new DataSet();
                cmbCahngYear.Items.Clear();
                // concatenate two integer columns
                cmd = new SqlCommand("SELECT CONVERT(NVARCHAR(8),Financial_Start_Year) +'-'+CONVERT(NVARCHAR(8),Financial_End_Year) as 'StartEndYear',Sr_No FROM tbYear WHERE Company_Code='" + Globalvariable.bcode + "' ORDER BY Sr_No desc", Objcon.connect());
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds.Clear();
                da.Fill(ds);
                cmbCahngYear.DisplayMember = "StartEndYear";
                cmbCahngYear.ValueMember = "Sr_No";          
                cmbCahngYear.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }


        private void BtnConnect_Click(object sender, EventArgs e)
        {
            Btnconnect();
        }


        public void CloseIt()
        {
            System.Threading.Thread.Sleep(1000);
            Microsoft.VisualBasic.Interaction.AppActivate(
                 System.Diagnostics.Process.GetCurrentProcess().Id);
            System.Windows.Forms.SendKeys.SendWait(" ");
          /*  (new System.Threading.Thread(CloseIt)).Start();
            MessageBox.Show("Save Successfully......");*/
        }
        public void Btnconnect()
        {
            try
            {              
                cmd = new SqlCommand("SELECT * FROM tbYear WHERE Sr_No='"+cmbCahngYear.SelectedValue+"' AND  Company_Code='" + Globalvariable.bcode + "' ORDER BY Sr_No desc", Objcon.connect());
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Globalvariable.StartYear =Convert.ToInt32(dr["Financial_Start_Year"].ToString());
                    Globalvariable.EndYear = Convert.ToInt32(dr["Financial_End_Year"].ToString());
                    DateTime startDate = Convert.ToDateTime(dr["Start_Date"]);
                    Globalvariable.StartDate = startDate.ToString("MM/dd/yyyy");
                    DateTime endDate = Convert.ToDateTime(dr["End_Date"]);
                    Globalvariable.EndDate = endDate.ToString("MM/dd/yyyy");
                    Globalvariable.company_Srno = Convert.ToInt32(dr["Sr_No"].ToString());                  
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
             this.Close();
        }

        private void FrmChangeFinanYear_Shown(object sender, EventArgs e)
        {
            cmbCahngYear.Focus();
        }
    }
}

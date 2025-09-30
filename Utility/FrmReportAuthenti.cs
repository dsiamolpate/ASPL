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
using Microsoft.Win32;
using ASPL.CLASSES;
using ASPL.STARTUP.FORMS;

namespace ASPL.Utility
{
    public partial class FrmReportAuthenti : Form
    {
        public bool IsValid { get; set; }
        public FrmReportAuthenti()
        {
            InitializeComponent();
        }

        Connection ObjCon = new Connection();
        Module mod = new Module();
        Globalvariable gv = new Globalvariable();
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
        private void FrmReportAuthenti_Load(object sender, EventArgs e)
        {
            txtUsernm.Text = Globalvariable.username;
            txtPass.Focus();
            IsValid = false;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnProceed_Click(object sender, EventArgs e)
        {
            Proceed();
        }

        public void Proceed()
        {
            try
            {
                if (txtPass.Text == "" && txtPass.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Password");
                txtPass.Focus();
            }
                SqlCommand cmdBranch = new SqlCommand("SELECT * FROM tbLoginDetails WHERE UserName='" + txtUsernm.Text + "'", ObjCon.connect());
                cmdBranch.CommandType = CommandType.Text;
                SqlDataReader dr = cmdBranch.ExecuteReader();
                if (dr.Read())
                {
                    if (mod.decrypt(dr["Password"].ToString()) == txtPass.Text)
                    {
                         if(Globalvariable.frmName == "UtilityComDetail")
                         {
                            //FrmCompanyDetails frmcomdetail=new FrmCompanyDetails();
                            // this.Hide();
                            // frmcomdetail.ShowDialog();
                            IsValid = true;
                            this.Close();
                         }
                    }
                    else
                    {
                      MessageBox.Show("You Are Not A Authorised Person.");
                    }
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void txtPass_Enter(object sender, EventArgs e)
        {
            txtPass.BackColor=Color.Yellow;
        }

        private void txtPass_Leave(object sender, EventArgs e)
        {
            txtPass.BackColor=Color.White;
        }

        private void txtPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                BtnProceed.Focus();
            }
        }

        private void FrmReportAuthenti_Shown(object sender, EventArgs e)
        {
            txtUsernm.Focus();
        }
    }
}

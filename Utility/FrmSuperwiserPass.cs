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
namespace ASPL.Utility
{
    public partial class FrmSuperwiserPass : Form
    {
        public bool IsValid { get; set; }
        public FrmSuperwiserPass()
        {
            InitializeComponent();
            IsValid = false;
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
        string SupPassword;

        private void FrmSuperwiserPass_Load(object sender, EventArgs e)
        {
            txtPass.Focus();
        }

        private void BtnProceed_Click(object sender, EventArgs e)
        {
            SupPassword = "AS151009111976";
            if (txtPass.Text == "")
            {
                txtPass.Focus();
            }
            else if (txtPass.Text == SupPassword)
            {
                IsValid = true;
                //this.Hide();
                //FrmCreateUserAccess creatuser = new FrmCreateUserAccess();
                //creatuser.ShowDialog();
                //

                this.Close();
            }
            else
            {
                IsValid = false;
                MessageBox.Show("You Are Not Autherised Person..!! Contact Your Program Vender...!!!");
                txtPass.Text = "";
                txtPass.Focus();
            }
        }

        private void txtPass_Enter(object sender, EventArgs e)
        {
            txtPass.BackColor = Color.Yellow;
        }

        private void txtPass_Leave(object sender, EventArgs e)
        {
            txtPass.BackColor = Color.White;
        }

        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnProceed.Focus();
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

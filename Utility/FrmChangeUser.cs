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
using ASPL.LODGE.STARTUP_FORM;
using ASPL.STARTUP.FORMS;


namespace ASPL.Utility
{
    public partial class FrmChangeUser : Form
    {
        public FrmChangeUser()
        {
            InitializeComponent();
        }
        string str;
        Module mod = new Module();
        Connection ObjCon = new Connection();
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
        private void FrmChangeUser_Load(object sender, EventArgs e)
        {
            FillUserNameCombo();
            combUserlist.Focus();
        }

        public void FillUserNameCombo()
        {
            try
            {
                combUserlist.Items.Clear();
                combUserlist.Items.Insert(0, "SELECT");
                #region Check Whether the UserNames Are Available or Not SELECT
                DataSet ds = new DataSet();
                combUserlist.SelectedIndex = 0;
                SqlCommand cmd = new SqlCommand("SELECT UserName FROM tbLoginDetails", ObjCon.connect());
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds.Clear();
                da.Fill(ds);
                combUserlist.DisplayMember = "UserName";
                combUserlist.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
                #endregion
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void combUserlist_Enter(object sender, EventArgs e)
        {
            txtPassword.BackColor = Color.White;
            combUserlist.BackColor = Color.Yellow;
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
           combUserlist.BackColor = Color.White;
           txtPassword.BackColor = Color.Yellow;
        }

        private void combUserlist_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtPassword.Focus();
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
               BtnSave.Focus();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                txtPassword.BackColor = Color.White;
               // string pass;
                if (combUserlist.Text == "")
                    combUserlist.Focus();
                else
                {
                    if (txtPassword.Text == "")
                        txtPassword.Focus();
                    else
                    {
                        SqlCommand cmdPass = new SqlCommand("SELECT * FROM tbLoginDetails WHERE UserName='" + combUserlist.Text + "'", ObjCon.connect());
                        cmdPass.CommandType = CommandType.Text;
                        SqlDataReader dr = cmdPass.ExecuteReader();
                        if (dr.Read())
                        {
                            if (mod.decrypt(dr["Password"].ToString()) == txtPassword.Text)
                            {
                                Globalvariable.usercd = dr["User_ID"].ToString();
                                Globalvariable.username = dr["UserName"].ToString();
                                FrmLodgeTreeview frmtree = new FrmLodgeTreeview();
                                frmtree.ShowDialog();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Please Enter Valid User Name and Password !!");
                                txtPassword.Text = "";
                                txtPassword.Focus();
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

        private void FrmChangeUser_Shown(object sender, EventArgs e)
        {
            combUserlist.Focus();
        }
    }
}

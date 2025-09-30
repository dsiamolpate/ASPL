using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using System.IO;

using ASPL.CLASSES;
using System.Data.Sql;
using System.Net;
using ASPL.STARTUP.FORMS;
using ASPL.LODGE.STARTUP_FORM;
using ASPL.Utility;

namespace ASPL.Utility
{
    public partial class FrmCreateUserAccess : Form
    {
       Globalvariable gv = new Globalvariable();
       Connection con = new Connection();
       Module mod = new Module();
       string str,opmode;
       SqlDataReader dr;
        public FrmCreateUserAccess()
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
        private void FrmCreateUserAccess_Load(object sender, EventArgs e)
        {
            BtnAddNew.Enabled = true;
            BtnDelete.Enabled = true;
            BtnSave.Enabled = false;
            BtnExit.Enabled = true;
            txtRepassword.Visible = false;
            lblRetypPass.Visible = false;
            TxtUsercd.Enabled = false;
            FillUserName();
        }
        public void FillUserName()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT UserName,User_ID FROM tbLoginDetails WHERE Branchcode='" + Globalvariable.bcode + "'", con.connect());
                DataSet ds = new DataSet();
                da.Fill(ds, "tbLoginDetails");
                combUserlist.DisplayMember = "UserName";
                combUserlist.ValueMember = "User_ID";
                combUserlist.DataSource = ds.Tables["tbLoginDetails"];
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
            
        }

        private void BtnAddNew_Click(object sender, EventArgs e)
        {
            string old_id;
            opmode = "A";
            combUserlist.Text = "";
            txtPassword.Text = "";
            txtRepassword.Visible = true;
            lblRetypPass.Visible = true;           
            BtnAddNew.Enabled = false;
            BtnDelete.Enabled = false;
            BtnSave.Enabled = true;
            BtnExit.Enabled = true;

            try
            {
            SqlCommand cmd =new SqlCommand("SELECT max(User_ID) FROM tbLoginDetails WHERE Branchcode='"+Globalvariable.bcode+"'",con.connect());
            cmd.CommandType = CommandType.Text;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    old_id= dr[0].ToString();
                    TxtUsercd.Text = GenerateCode(old_id);
                }
                else
                {
                    TxtUsercd.Text = "USER_10001";
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
            combUserlist.Focus();  
        }

        public string GenerateCode(string old_id)
        {
            double cd;
            string Scd;
            cd = Convert.ToDouble(old_id.Substring(old_id.IndexOf("_") + 1, 5));
            return Scd = "USER_" + (cd + 1);
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void combUserlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxtUsercd.Text = combUserlist.SelectedValue.ToString();
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
                if (txtRepassword.Visible == true)
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    txtRepassword.Focus();
                }
            }
            else
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                   BtnAddNew.Focus();
                }
            }
        }

        private void txtRepassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
               BtnSave.Focus();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (combUserlist.Text == "")
            {
                MessageBox.Show("Enter User Name");
                combUserlist.Focus();
                return;
            }
            if (txtPassword.Text != txtRepassword.Text)
            {
                MessageBox.Show("Both Password Are Same");
                txtPassword.Text = "";
                txtRepassword.Text = "";
                txtPassword.Focus();
                return;
            }
            if (txtPassword.Text=="" || txtRepassword.Text=="")
            {
                txtPassword.Focus();
                return;
            }
            try
            {
                if (opmode == "A")
                {
                    SqlCommand str1 = new SqlCommand("SELECT * FROM tbLoginDetails where UserName='" + combUserlist.Text + "' and Branchcode='" + Globalvariable.bcode + "'", con.connect());
                    SqlDataReader Rstemp = str1.ExecuteReader();
                    if (Rstemp.Read())
                    {
                        MessageBox.Show("User Name Already Exist ...");
                        txtPassword.Text = "";
                        txtRepassword.Text = "";
                        txtPassword.Focus();
                        return;
                    }
                }
                if (opmode == "A")
                {
                    SqlCommand str2 = new SqlCommand("INSERT INTO tbLoginDetails(User_ID,UserName,Password,Remote,Branchcode)VALUES('" + TxtUsercd.Text + "','" + combUserlist.Text.ToUpper() + "','" + mod.encrypt(txtPassword.Text.Trim()) + "','0','" + Globalvariable.bcode + "')", con.connect());
                    str2.ExecuteNonQuery();
                    MessageBox.Show("New Record has been added Sucessfully", "HMS");
                }
                else
                {
                    SqlCommand str3 = new SqlCommand("UPDATE tbLoginDetails SET User_ID='" + TxtUsercd.Text + "',UserName='" + combUserlist.Text.ToUpper() + "',Password='" + mod.encrypt(txtPassword.Text.Trim()) + "',Remote='0',Branchcode='" + Globalvariable.bcode + "' WHERE User_ID='" + TxtUsercd.Text + "' and Branchcode='" + Globalvariable.bcode + "'", con.connect());
                    str3.ExecuteNonQuery();
                    MessageBox.Show("Record has been Updated Sucessfully", "HMS");
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
            txtPassword.Text = "";
           txtRepassword.Text = "";
           txtRepassword.Visible = false;
           lblRetypPass.Visible = false;

            FillUserName();
            BtnAddNew.Enabled = true;
            BtnDelete.Enabled = true;
            BtnSave.Enabled = false;
            BtnExit.Enabled = true;

            FillUserName();
            opmode = "V";

        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (opmode == "A")
                    {
                       txtRepassword.Visible = true;
                       lblRetypPass.Visible = true;
                        txtRepassword.Focus();
                    }
                    else
                    {
                      SqlCommand str =new SqlCommand("SELECT Password FROM tbLoginDetails WHERE UserName='" + combUserlist.Text + "' and Branchcode='" + Globalvariable.bcode + "'",con.connect());
                       SqlDataReader dr =str.ExecuteReader();
                        if (dr.Read())
                        {
                            if (txtPassword.Text != (mod.decrypt(dr[0].ToString())))
                            {
                                MessageBox.Show("Invalid Password", "HMS");
                                txtPassword.Text = "";
                                txtPassword.Focus();
                            }
                        }
                    }
                }
            }
                catch(Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
             }
            }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            txtPassword.BackColor = Color.Yellow;
            combUserlist.BackColor = Color.White;
            txtRepassword.BackColor = Color.White;
        }

        private void txtRepassword_Enter(object sender, EventArgs e)
        {
            txtPassword.BackColor = Color.White;
            txtRepassword.BackColor = Color.Yellow;
        }

        private void combUserlist_Enter(object sender, EventArgs e)
        {
            combUserlist.BackColor = Color.Yellow;
            txtPassword.BackColor = Color.White;
            txtRepassword.BackColor = Color.White;
        }

        private void lbl_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmUserAccess FrmUserAcc = new FrmUserAccess();
            FrmUserAcc.ShowDialog();
        }

        private void FrmCreateUserAccess_Shown(object sender, EventArgs e)
        {
            combUserlist.Focus();
        }
        }
}

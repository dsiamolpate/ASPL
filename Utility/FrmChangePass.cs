using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using ASPL.CLASSES;

//Import The FOLDERS
using ASPL.Utility;

namespace ASPL.Utility
{
    public partial class FrmChangePass : Form
    {
        Globalvariable gv = new Globalvariable();
        Design ObjDes = new Design();
        SqlCommand cmd;
        Connection con = new Connection();
        Module mod = new Module();
        int grp_no,seq_no;
        string user_id,strpass;
        public FrmChangePass()
        {
            InitializeComponent();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void OnGotFocusHandler(object sender, KeyEventArgs e)
        {
            TextBox txt = new TextBox();
        }
        private void FrmChangePass_Load(object sender, EventArgs e)
        {
            BtnSave.Enabled = false;
            lblNmPass.Visible = false;
            lblRetypPass.Visible = false;
            txtnewPass.Visible = false;
            txtRetypPass.Visible = false;       
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
            string pass;           
            if (e.KeyCode == Keys.Enter)
            {
                if (txtPassword.Text == "")
                {
                    MessageBox.Show("Please Enter Password");
                    txtPassword.Focus();
                }
                else
                {
                    string strsql = "select * FROM tbLoginDetails where User_ID='" + Globalvariable.usercd + "' and Branchcode='" + Globalvariable.bcode + "'";
                    cmd = new SqlCommand(strsql, con.connect());
                    SqlDataReader RstUserName = cmd.ExecuteReader();
                    if (RstUserName.Read())
                    {
                        pass = mod.decrypt(RstUserName["Password"].ToString());
                        if (txtPassword.Text != pass)
                        {
                            MessageBox.Show("Invalid Password");
                            txtPassword.Text = "";
                            txtPassword.Focus();
                            return;
                        }
                        BtnSave.Enabled = true;
                        lblNmPass.Visible = true;
                        lblRetypPass.Visible = true;
                        txtnewPass.Visible = true;
                        txtRetypPass.Visible = true;
                        txtnewPass.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Invalid User Id", "HMS");
                        txtPassword.Text = "";
                        txtPassword.Focus();
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

        private void txtnewPass_KeyDown(object sender, KeyEventArgs e)
        {
           /* if (e.KeyCode == Keys.Enter)
            {
                txtRetypPass.Focus();
                txtnewPass.BackColor = Color.White;
                txtRetypPass.BackColor = Color.Yellow;
            }*/
        }

        private void txtRetypPass_KeyDown(object sender, KeyEventArgs e)
        {
          /*  if (e.KeyCode == Keys.Enter)
            {
                BtnSave.Focus();
                txtRetypPass.BackColor = Color.White;
            }*/
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == "")
            {
                txtPassword.Focus();
           
                return;
            }
            else if (txtnewPass.Text == "")
            {
                txtnewPass.Focus();
             
                return;
            }
            else if (txtRetypPass.Text == "")
            {
                txtRetypPass.Focus();
              
                return;
            }
            else if (txtnewPass.Text.Trim() != txtRetypPass.Text.Trim())
            {
                MessageBox.Show("Both Password Should Be Same", "HMS");
                txtnewPass.Text = "";
                txtRetypPass.Text = "";
                txtnewPass.Focus();
                return;
            }
            else
            {
                try
                {
                    //  int no;
                    //  string cha;
                    Random rno = new Random();
                    cmd = new SqlCommand("SELECT * FROM tbLoginDetails where User_ID='" + Globalvariable.usercd + "' and Branchcode='" + Globalvariable.bcode + "'", con.connect());
                    SqlDataReader Rstemp = cmd.ExecuteReader();
                    Rstemp.Read();
                    cmd = new SqlCommand("SELECT * FROM tbUGroup where User_Id='" + Globalvariable.usercd + "'", con.connect());
                    SqlDataReader dr = cmd.ExecuteReader();
                    /* for (int i = 0; i <= (txtnewPass.Text.Length) - 1; i++)
                       {
                           //Generate random no 
                           no = ((100*rno.Next(0,10))+20);
                           cha = txtnewPass.Text.Substring(i, 1);
                           grp_no = no;
                           user_id = Globalvariable.usercd;
                           seq_no = i + 1;
                           int val=Convert.ToInt16(cha)+no;
                           strpass=strpass +(char)val;
                       }*/
                    cmd = new SqlCommand("Update tbLoginDetails set User_ID='" + Globalvariable.usercd + "',username='" + Rstemp["UserName"].ToString() + "',Password='" + mod.encrypt(txtnewPass.Text.Trim()) + "',Remote='0',Branchcode='" + Globalvariable.bcode + "' where User_ID='" + Globalvariable.usercd + "'", con.connect());
                    //  cmd = new SqlCommand("Update tbLoginDetails set User_ID='" + Globalvariable.usercd + "',username='" + Rstemp["UserName"].ToString() + "',Password='" + mod.encrypt(strpass) + "',Remote='0',Branchcode='" + Globalvariable.bcode + "' where User_ID='" + Globalvariable.usercd + "'", con.connect());
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("Update tbUGroup set Group_No='" + grp_no + "',Sequ_No='" + seq_no + "' WHERE User_Id='" + Globalvariable.usercd + "' AND Branchcode='" + Globalvariable.bcode + "'", con.connect());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Your Password has been Successfully Updated!!!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    string str = "Message:" + ex.Message;
                    MessageBox.Show(str, "Error Message");
                }
            }
        }

        private void txtnewPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtRetypPass.Focus();             
            }
        }

        private void txtRetypPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
               BtnSave.Focus();
              txtRetypPass.BackColor = Color.White;
            }
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            txtPassword.BackColor = Color.Yellow;
        }

        private void textGotFocus(System.Windows.Forms.TextBox txtTemp)
        {

          txtTemp.SelectionStart = 0;
            txtTemp.SelectionLength = txtTemp.Text.Length;
        }

        private void txtnewPass_Enter(object sender, EventArgs e)
        {
            txtnewPass.BackColor = Color.Yellow;
            txtRetypPass.BackColor = Color.White;
            txtPassword.BackColor = Color.White;
        }

        private void txtRetypPass_Enter(object sender, EventArgs e)
        {
            txtRetypPass.BackColor = Color.Yellow;
            txtnewPass.BackColor = Color.White;
            txtPassword.BackColor = Color.White;
        }

        private void FrmChangePass_Shown(object sender, EventArgs e)
        {
            txtPassword.Focus();
        }
        }
    }


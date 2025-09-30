using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//Import The FOLDERS
using ASPL.CLASSES;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Net;
using ASPL.STARTUP.FORMS;
using System.IO;
namespace ASPL.STARTUP.FORMS
{
    public partial class FrmUpdateRegister : Form
    {

        #region VARIABLE & CLASS OBJECT DECLARATION
        Design ObjDes = new Design();
        Connection ObjCon = new Connection();
        Module mod = new Module();
        SqlCommand cmdstr;
        SqlDataReader dr;
        String StrSQL,str;
        int ObjUPDATE = 0, RegisterAccessCount = 0,count;
        #endregion

        #region INITIALIZATION
        public FrmUpdateRegister()
        {
            InitializeComponent();
        }
        #endregion

        #region EVENTS
        #region FORMLOAD
        private void FrmUpdateRegister_Load(object sender, EventArgs e)
        {
            count = 0;
        }
        #endregion

        #region UPDATE CLICK EVENT
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            UPDATE();
        }
        #endregion

        #region EXIT CLICK EVENT
        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion
        
        #region For Set Focus On "ENTER" Key Depend On "TABINDEX".So "KEYPREVIEW" Property Of Form Should Be "TRUE".
        private void FrmUpdateRegister_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Control nextTab;
                if (e.KeyChar == (char)Keys.Enter)
                {
                    nextTab = ((Control)sender);
                    nextTab = GetNextControl(ActiveControl, true);

                    if (nextTab.Name == "BtnExit")
                    {
                    }
                    else
                    {
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
        #endregion
        
        #endregion

        #region UPDATE
        public void UPDATE()
        {
            try
            {
                string MotherBoardNo, stringOTP = "", strSQL;
                #region Validation
                if (txtotpNo.Text.Trim() == String.Empty)
                {
                    txtotpNo.Focus();
                }
                else if (txtValidityDays.Text.Trim() == String.Empty || txtValidityDays.Text=="0" || Convert.ToInt32(txtValidityDays.Text) > 365)
                {
                    txtValidityDays.Focus();
                }
                else
                {
                 
                    if (Globalvariable.MacAddress != "")
                    {
                       // cmdstr = new SqlCommand("SELECT * FROM tbRegister WHERE Motherbord_No='" + MotherBoardNo + "'", ObjCon.connect());
                        cmdstr = new SqlCommand("SP_FrmRegister", ObjCon.connect());
                        cmdstr.CommandType = CommandType.StoredProcedure;
                        cmdstr.Parameters.AddWithValue("@variable", "SELECT");
                        cmdstr.Parameters.AddWithValue("@MacAddress", Globalvariable.MacAddress);                      
                    }
                     dr = cmdstr.ExecuteReader();
                    if (dr.Read())
                    {
                        stringOTP = dr["Register_NO"].ToString();
                        stringOTP = mod.Isnull(mod.decrypt(stringOTP), "");
                    }
                    if (txtotpNo.Text != stringOTP)
                    {
                        MessageBox.Show("Invalid Registration Number");
                        txtotpNo.Focus();
                    }
                    else
                    {
                        string Currentdt =Convert.ToString( DateTime.Now);
                        string expdate = mod.Isnull(dr["Expiry_DT"].ToString(), Currentdt);
                        DateTime ObjDateTime = Convert.ToDateTime(expdate).AddDays(Convert.ToInt16(txtValidityDays.Text));
                        if (Globalvariable.MacAddress != "")
                        {
                            //strSQL = "UPDATE tbRegister SET Validity='365',EXPIRY_DT= '" + ObjDateTime + "',Software_LOCK='0' WHERE Motherbord_No='" + MotherBoardNo + "'";
                            //ObjCon.GetConnectionInsert(strSQL);
                            cmdstr = new SqlCommand("SP_FrmRegister", ObjCon.connect());
                            cmdstr.CommandType = CommandType.StoredProcedure;
                            cmdstr.Parameters.AddWithValue("@Frmname", "UpdateRegistration");
                            cmdstr.Parameters.AddWithValue("@Validdays",txtValidityDays.Text);
                            cmdstr.Parameters.AddWithValue("@ExpDate", ObjDateTime.ToShortDateString());
                            cmdstr.Parameters.AddWithValue("@MacAddress", Globalvariable.MacAddress);
                            cmdstr.ExecuteNonQuery();                           
                        }
                     
                    cmdstr = new SqlCommand("SP_FrmRegister", ObjCon.connect());
                    cmdstr.CommandType = CommandType.StoredProcedure;
                    cmdstr.Parameters.AddWithValue("@variable", "SELECT");
                    cmdstr.Parameters.AddWithValue("@MacAddress", Globalvariable.MacAddress);
                    SqlDataReader datareader = cmdstr.ExecuteReader();
                    if (datareader.Read())
                    {
                        if (datareader["Software_LOCK"].ToString() == "1" || (Convert.ToDateTime(datareader["Expiry_DT"].ToString()) < DateTime.Now))
                        {
                            MessageBox.Show("Your Software Has Been Expired Please Renew It.");
                            FrmUpdateRegister frmupdate = new FrmUpdateRegister();
                            this.Hide();
                            frmupdate.Show();
                        }
                        else
                        {
                            MAINFORM mainfrm = new MAINFORM();
                           // this.Close();
                            this.Hide();
                            mainfrm.ShowDialog();
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
            #endregion

            

        }
        #endregion

        #region EXIT/CANCEL FUNCTION
        public void Exit()
        {
            this.Close();
        }
        #endregion

        

        private void TxtRegistrationKey_Enter(object sender, EventArgs e)
        {
            txtotpNo.BackColor = Color.Yellow;
         
        }
              
        private void txtotpNo_Leave(object sender, EventArgs e)
        {
            txtotpNo.BackColor = Color.White;
        }

        private void txtotpNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtValidityDays.Focus();
            }
        }

        private void linklabelRegenekey_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (count > 2)
                {
                    MessageBox.Show("You Access Is Denied.");
                }
                SqlCommand cmdBranch = new SqlCommand("SELECT * FROM tbRegister WHERE MAC_Address='" + Globalvariable.MacAddress + "'", ObjCon.connect());
                SqlDataReader dr = cmdBranch.ExecuteReader();
                if (dr.Read())
                {
                    string stringOTP = dr["Register_NO"].ToString();
                    stringOTP = mod.Isnull(mod.decrypt(stringOTP), "");
                    string strmsg = "OTP for Dealer Name:" + mod.Isnull(dr["Dealer_Name"].ToString(), "") + " Mobile No.:" + mod.Isnull(dr["Contact_No"].ToString(), "") + " OTP is " + stringOTP + "";
                    // mod.SendSms("9890276751", strmsg);
                    MessageBox.Show(stringOTP);
                }
                count = count + 1;
                txtotpNo.Focus();
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void txtValidityDays_Enter(object sender, EventArgs e)
        {
            txtValidityDays.BackColor = Color.Yellow;
        }

        private void txtValidityDays_Leave(object sender, EventArgs e)
        {
            txtValidityDays.BackColor = Color.White;
        }

        private void txtValidityDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                BtnUpdate.Focus();
            }
        }        
    }
}

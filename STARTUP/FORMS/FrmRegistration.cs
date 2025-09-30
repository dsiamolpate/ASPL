using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Management;
using System.Text;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Management;
using System.Management.Instrumentation;
//Import The FOLDERS
using ASPL.CLASSES;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Net;
using ASPL.STARTUP.FORMS;
using System.IO;
using ASPL.CLASSES;
using ASPL.LODGE.STARTUP_FORM;
using ASPL.STARTUP.FORMS;
using ASPL.Utility;


namespace ASPL.STARTUP.FORMS
{
    public partial class FrmRegistration : Form
    {
        #region VARIABLE & CLASS OBJECT DECLARATION
        Design ObjDes = new Design();
        Connection ObjCon = new Connection();
        Module mod = new Module();
        int ObjUPDATE;
        string str, OTPStr, NewComp_Reg, StrSQL;
        SqlCommand cmd;
        SqlDataReader dataReader;
        public static bool ConnectionStrFlag = false;

        //public bool ConnectionStrFlag
        //{
        //    get
        //    {
        //        return ConnectionStrFlag;
        //    }
        //    set
        //    {
        //        ConnectionStrFlag = value;
        //    }
        //}
        #endregion

        #region INITIALIZATION
        public FrmRegistration()
        {
            InitializeComponent();
            while(ConnectionStrFlag == false)
            {
              ConnectionStrFlag=checkRegistered();
            }
        }
        #endregion

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

        public Boolean checkRegistered()
        {
            try
            {
                //FrmConnectionString frmconn = new FrmConnectionString();
                //frmconn.ShowDialog();
                GetMACAddress();
                #region Check Whether the PC is registered or not SELECT

                //ConnectionStrFlag=
                cmd = new SqlCommand("SP_FrmRegister", ObjCon.Registrationconnect());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@variable", "SELECT");
                cmd.Parameters.AddWithValue("@MacAddress", Globalvariable.MacAddress);
                dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    string SoftwareLock = dataReader["Software_LOCK"].ToString();
                    string Currentdt = Convert.ToString(DateTime.Now);
                  //  string expdate = mod.Isnull(dataReader["Expiry_DT"].ToString(), "");
                    DateTime expdate = Convert.ToDateTime(mod.Isnull(dataReader["Expiry_DT"].ToString(), Currentdt));
                    if (SoftwareLock == "1" || expdate < DateTime.Now)
                    {
                        MessageBox.Show("Your Software Has Been Expired Please Renew It.", "ASPL");
                        cmd = new SqlCommand("SP_FrmRegister", ObjCon.connect());
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@variable", "UPDATE");
                        cmd.Parameters.AddWithValue("@MacAddress", Globalvariable.MacAddress);
                        cmd.ExecuteNonQuery();
                        FrmUpdateRegister objFrmUpdateRegistercs = new FrmUpdateRegister();
                        this.Hide();
                        objFrmUpdateRegistercs.ShowDialog();
                        WindowState = FormWindowState.Minimized;
                        ShowInTaskbar = false;
                        this.Visible = false;
                    }
                    else
                    {
                        cmd = new SqlCommand("SELECT * FROM tbCompanyDetailsLodge",ObjCon.connect());
                        dataReader = cmd.ExecuteReader();
                        if (!dataReader.HasRows)
                        {
                            FrmCompanyDetails frmcomp = new FrmCompanyDetails();
                            this.Hide();
                            frmcomp.Show();
                            WindowState = FormWindowState.Minimized;
                            ShowInTaskbar = false;
                            this.Visible = false;
                        }
                        else
                        {
                        MAINFORM mainfrm = new MAINFORM();
                        this.Hide();
                        mainfrm.Show();
                        WindowState = FormWindowState.Minimized;
                        ShowInTaskbar = false;
                        this.Visible = false;
                            //Amol Pate - 22/3 Set connection string flag to true explicitly
                            ConnectionStrFlag = true;
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

            return ConnectionStrFlag;
        }

        #region EVENTS
        #region FORMLOAD
        private void FrmRegistration_Load(object sender, EventArgs e)
        {
           // checkRegistered();
        }
        #endregion

        public void GetMACAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card  
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            //return sMacAddress;
            Globalvariable.MacAddress = sMacAddress;
        }

        

        public void CheckListBoxText()
        {
            try
            {
                for (int i = 0; i < listboxAppName.Items.Count; i++)
                {
                    if (listboxAppName.GetItemChecked(i))
                    {
                        string str = (string)listboxAppName.Items[i];
                        MessageBox.Show(str);
                    }
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        #region SAVE CLICK EVENT
        private void BtnSave_Click(object sender, EventArgs e)
        {
            Save();
            
        }
        #endregion

        #region EXIT CLICK EVENT
        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        #endregion

        #region For Set Focus On "ENTER" Key Depend On "TABINDEX".So "KEYPREVIEW" Property Of Form Should Be "TRUE".
        private void FrmRegistration_KeyPress(object sender, KeyPressEventArgs e)
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
                        Application.Exit();
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


        public void OTPRandomString(int val)
        {
            try
            {
                string rgch;
                rgch = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
                var stringChars1 = new char[val];
                var random1 = new Random();

                for (int i = 0; i < val; i++)
                {
                    stringChars1[i] = rgch[random1.Next(rgch.Length)];
                }

                OTPStr = new String(stringChars1);
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }


        #region SAVE FUNCTION
        public void Save()
        {
            try
            {
                #region VALIDATION

                if( listboxAppName.Items.Count==0) 
                {
                    listboxAppName.Focus();
                }
                else if (txtDealerName.Text.Trim() == String.Empty)
                {
                    txtDealerName.Focus();
                    return;
                }
                else if (TxtCopyRights.Text.Trim() == String.Empty)
                {
                    TxtCopyRights.Focus();
                    return;
                }
                else if (txtContactNo.Text == String.Empty)
               {
                   txtContactNo.Focus();
                   return;
               }
                #endregion
                else
                {
                    #region INSERT Into Registration Table For Registered This Software With This  PC

                    for (int i = 0; i < listboxAppName.Items.Count; i++)
                    {
                        if (listboxAppName.GetItemChecked(i))
                        {
                            string str = (string)listboxAppName.Items[i];
                           if(str=="LODGE")
                           {
                               Globalvariable.flagLodge="L";
                           }
                           else if (str == "RESTAURANT")
                           {
                               Globalvariable.flagResta = "R";
                           }
                           else if (str == "MATERIAL MANAGEMENT")
                           {
                               Globalvariable.flagMateriMagmnt = "M";
                           }
                           else if (str == "ACCOUNTS")
                           {
                               Globalvariable.flagAcct = "A";
                           }
                        }
                    }

                    //Generate OTP String
                    OTPRandomString(4);

                    //Validity, Expiry_DT
                    //string strQuery = "INSERT INTO tbRegister(Application_Name_Lodge,Application_Name_Restaurant,Application_Name_MaterialMgmnt,Application_Name_Accounts,CopyRight, CopyRight_DT,Dealer_Name,Contact_No,Register_NO,Software_LOCK, Motherbord_No,HardDisk_No,Company_Regist) VALUES" +
                    //           "('" + mod.Isnull(Globalvariable.flagLodge, "N") + "','" + mod.Isnull(Globalvariable.flagResta, "N") + "','" + mod.Isnull(Globalvariable.flagMateriMagmnt, "N") + "','" + mod.Isnull(Globalvariable.flagAcct, "N") + "','" + TxtCopyRights.Text + "','" + DateTime.Now.ToShortDateString() + "','" + txtDealerName.Text + "','" + txtContactNo.Text + "','" + mod.encrypt(OTPStr) + "','1','" + MachineID.GetMotherBoardID() + "','" + MachineID.GetVolumeSerial("C") + "','2')";
                    //int InsertResult = ObjCon.GetConnectionInsert(strQuery);
                    cmd = new SqlCommand("SP_FrmRegister", ObjCon.connect());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@variable", "INSERT");
                    cmd.Parameters.AddWithValue("@Lodgeapp_Name", mod.Isnull(Globalvariable.flagLodge, "N"));
                    cmd.Parameters.AddWithValue("@Restaapp_Name", mod.Isnull(Globalvariable.flagResta, "N"));
                    cmd.Parameters.AddWithValue("@Accountapp_Name", mod.Isnull(Globalvariable.flagAcct, "N"));
                    cmd.Parameters.AddWithValue("@Materialapp_Name", mod.Isnull(Globalvariable.flagMateriMagmnt, "N"));
                    cmd.Parameters.AddWithValue("@copyRight", TxtCopyRights.Text);
                    cmd.Parameters.AddWithValue("@CopyDate", DateTime.Now.ToShortDateString());
                    cmd.Parameters.AddWithValue("@Dealername", txtDealerName.Text);
                    cmd.Parameters.AddWithValue("@ContactNo", txtContactNo.Text);
                    cmd.Parameters.AddWithValue("@OTP", mod.encrypt(OTPStr));
                    cmd.Parameters.AddWithValue("@MacAddress", Globalvariable.MacAddress);
                    cmd.ExecuteNonQuery();      
                  
                    MessageBox.Show(OTPStr);
                    //Send OTP
                     //SqlCommand cmdBranch = new SqlCommand("SELECT * FROM tbRegister WHERE Company_Regist='2'", ObjCon.connect());               
                    cmd = new SqlCommand("SP_FrmRegister", ObjCon.connect());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@variable", "SELECT");
                    cmd.Parameters.AddWithValue("@MacAddress", Globalvariable.MacAddress);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        string strmsg = "OTP for Dealer Name:" + mod.Isnull(dr["Dealer_Name"].ToString(), "") + " Mobile No.:" + mod.Isnull(dr["Contact_No"].ToString(), "") + " OTP is " + OTPStr + "";
                       // mod.SendSms("9890276751", strmsg);
                    }

                        FrmUpdateRegister updateRegister = new FrmUpdateRegister();                     
                        this.Hide();
                        updateRegister.ShowDialog();                                      
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


        private void TxtCopyRights_Enter(object sender, EventArgs e)
        {
            TxtCopyRights.BackColor = Color.Yellow;
        }

        
        private void TxtCopyRights_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
               txtDealerName.Focus();
            }
        }

      
        private void CmbSupport_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
               BtnSave.Focus();
            }
        }

        private void TxtCopyRights_Leave(object sender, EventArgs e)
        {
            TxtCopyRights.BackColor = Color.White;
        }

        private void txtDealerName_Enter(object sender, EventArgs e)
        {
           txtDealerName.BackColor = Color.Yellow;
        }

        private void txtDealerName_Leave(object sender, EventArgs e)
        {
            txtDealerName.BackColor = Color.White;
        }

        private void txtContactNo_Enter(object sender, EventArgs e)
        {
           txtContactNo.BackColor = Color.Yellow;
        }

        private void txtContactNo_Leave(object sender, EventArgs e)
        {
            txtContactNo.BackColor = Color.White;
        }

        private void txtDealerName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
               txtContactNo.Focus();
            }
        }

        private void txtContactNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back);

            if (e.KeyChar == (char)Keys.Enter)
            {
                BtnSave.Focus();
            }
        }

        private void listboxAppName_Enter(object sender, EventArgs e)
        {
            listboxAppName.BackColor = Color.Yellow;
        }

        private void listboxAppName_Leave(object sender, EventArgs e)
        {
            listboxAppName.BackColor = Color.White;
        }

        private void listboxAppName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                TxtCopyRights.Focus();
            }
        }
    }
}

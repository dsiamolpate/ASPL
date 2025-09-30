using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Management;
using System.Windows.Forms;
using System.Configuration;
using System.Security.Cryptography;
using System.Globalization;
using System.IO;
//Import The FOLDERS
using ASPL.CLASSES;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Net;
using ASPL.STARTUP.FORMS;
using ASPL.LODGE.STARTUP_FORM;
using ASPL.Utility;
using ASPL.LODGE;

namespace ASPL
{
    public partial class MAINFORM : Form
    {
        #region VARIABLE & CLASS OBJECT DECLARATION
        Image[] ImgObj;
        Design ObjDes = new Design();
        Connection ObjCon = new Connection();
        Module mod = new Module();
        Validation validation = new Validation();
        ASPL.CLASSES.Globalvariable gv = new ASPL.CLASSES.Globalvariable();
        String StrSQL;
        int ObjUPDATE = 0;
        SqlCommand cmd;
        string str;
        #endregion
        public static string connstr = ConfigurationManager.ConnectionStrings["ASPLDBCoonection"].ToString();
        SqlConnection con = new SqlConnection(connstr);
        #region INITIALIZATION
        public MAINFORM()
        {
            InitializeComponent();
        }
        #endregion
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern long GetVolumeInformation(string PathName, StringBuilder VolumeNameBuffer, UInt32 VolumeNameSize, ref UInt32 VolumeSerialNumber, ref UInt32 MaximumComponentLength, ref UInt32 FileSystemFlags, StringBuilder FileSystemNameBuffer, UInt32 FileSystemNameSize);
       
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
        #region FORMLOAD       
        private void MAINFORM_Load(object sender, EventArgs e)
        {         
            try
            {
              
               CmbUserName.Focus();
                //Set Size Depending On Screen Resolution 
                #region Set Size Depending On Screen Resolution
                ObjDes.SetScreenResolution(this);
                #endregion
                //Bitmap bm = new Bitmap(Properties.Resources.Icon);
                //// Convert to an icon and use for the form's icon.
                //this.Icon = Icon.FromHandle(bm.GetHicon());
                //Set The Images To PictureBox On Load And Panel
                #region Set Images And Array Of Images And Start Timer
                PicBox_ASPLLogo.Image = ASPL.Properties.Resources.Logo;
                PicBox_ExitLogo.Image = ASPL.Properties.Resources.exit_orange_icon_stock_photograph_csp25895792;
                //PanelFooter.BackgroundImage = ASPL.Properties.Resources.Background_Image;
                //PanelHeader.BackgroundImage = ASPL.Properties.Resources.Background_Image;
                //PanelLoginDetails.BackgroundImage = ASPL.Properties.Resources.BackgroundImage;
                //BtnLogin.BackgroundImage = ASPL.Properties.Resources.BackgroundImage;
                //BtnLoginCancel.BackgroundImage = ASPL.Properties.Resources.BackgroundImage;
                //Start Timer
                TimerFooterMarquee.Start();
                //Array Of Images Shows on Front Page  
                ImgObj = new Image[6];
                ImgObj[0] = ASPL.Properties.Resources.MM;
                ImgObj[1] = ASPL.Properties.Resources.LODGEBLUE;
                ImgObj[2] = ASPL.Properties.Resources.PAYROLL;
                ImgObj[3] = ASPL.Properties.Resources.RESTBLUE;
                ImgObj[4] = ASPL.Properties.Resources.PAYROLLBLUE;
                ImgObj[5] = ASPL.Properties.Resources.LODGE2BLUE;
                #endregion           

                #region Call All Fill Combo Box
                //Fill User Combo Box
                FillUserNameCombo();
                //Fill Application Combo Box
               // FillApplicationNameCombo();
                //Fill Company Combo Box
                FillCompanyNameCombo();
                //Fill Branch Combo Box
                FillBranchNameCombo();
                #endregion
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }
        #endregion
        #region LOGIN CLICK EVENT
        private void BtnLogin_Click(object sender, EventArgs e)
        {       
            CheckLogin();    
        }
        #endregion
        #region CANCEL CLICK EVENT
        private void BtnLoginCancel_Click(object sender, EventArgs e)
        {
            Exit();
        }
        #endregion
        #region EXIT PICBOX_EXITLOGO_CLICK
        private void PicBox_ExitLogo_Click(object sender, EventArgs e)
        {
            Exit();
        }
        #endregion
        public static string RightText(string param, int length)
        {
            string result = param.Substring(param.Length - length, length);
            return result;
        }
        public static string LeftText(string param, int length)
        {
            string result = param.Substring(0, length);
            return result;
        }

        #region TIMER FOOTER MARQUEE TICK EVENT
        private void TimerFooterMarquee_Tick(object sender, EventArgs e)
        {
            lblFooterMarquee.Text = RightText(lblFooterMarquee.Text, (lblFooterMarquee.Text).Length - 1) + LeftText(lblFooterMarquee.Text, 1);
            TimerFooterMarquee.Tag = Convert.ToInt32(TimerFooterMarquee.Tag) + 200;
            /* lblFooterMarquee.Left = lblFooterMarquee.Left - 5;
             if (lblFooterMarquee.Left + lblFooterMarquee.Width <= 0)
             {
                 lblFooterMarquee.Left = this.Width;
             }*/
            Random random = new Random();
            PicBoxImage.Image = ImgObj[random.Next(ImgObj.Length)];
        }

        #endregion

        #region For Set Focus On "ENTER" Key Depend On "TABINDEX".So "KEYPREVIEW" Property Of Form Should Be "TRUE".
        private void MAINFORM_KeyPress(object sender, KeyPressEventArgs e)
        {
            Control nextTab;
            if (e.KeyChar == (char)Keys.Enter)
            {
                nextTab = ((Control)sender);
                nextTab = GetNextControl(ActiveControl, true);

                if (nextTab.Name == "BtnLoginCancel")
                {
                }
                else
                {
                    nextTab.Focus();
                }
            }
        }
        #endregion

        #region LOGIN FUNCTION
        public void CheckLogin()
        {
            try
            {
                string selected = this.CmbApplicationName.GetItemText(this.CmbApplicationName.SelectedItem);
                if (TxtPassword.Text == String.Empty && TxtPassword.Text == "")
            {
                TxtPassword.Focus();
                MessageBox.Show("Please Enter Password");               
            }
            else if (selected == "SELECT" || selected=="")
            {
                CmbApplicationName.Focus();
                MessageBox.Show("Select Application");
            }
            else if (CmbCompanyName.Text == "")
            {
                CmbCompanyName.Focus();
            }
            else if (CmbBranchName.Text == "")
            {
                CmbBranchName.Focus();
            }
            else
            {
                UserAccess();
            }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
           
        }
        #endregion

        #region USERACCESS FUNCTION
        public void UserAccess()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_LoginDetails", ObjCon.connect());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tableName", "tbLoginDetails");
                cmd.Parameters.AddWithValue("@Username", CmbUserName.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (mod.decrypt(dr["Password"].ToString()) == TxtPassword.Text)
                    {
                        BtnLogin.Enabled = false;
                                Globalvariable.usercd = dr["User_ID"].ToString();
                                Globalvariable.username = dr["UserName"].ToString();
                                Globalvariable.bcode = dr["Branchcode"].ToString();
                                Globalvariable.cmb_usenm = CmbUserName.Text;
                                Globalvariable.company_nm = CmbCompanyName.Text;
                                Globalvariable.branch_nm = CmbBranchName.Text;
                               FrmCompanySelect frmcomSelect = new FrmCompanySelect();                              
                             frmcomSelect.ListCompname();                              
                                 frmcomSelect.ListFinanceYear();
                            //   frmcomSelect.ConnectData();
                              if (CmbApplicationName.Text == "Lodge Management System")
                              {
                                 mod.DataBaseBackup("Login");
                                  Globalvariable.ModuleNm = "Lodge Management System";
                                  Globalvariable.flagchar = "L";
                                  Globalvariable.OnApplicationLodge = "ON-L";
                                  FrmLodgeTreeview frmLodg = new FrmLodgeTreeview();
                                  frmLodg.Show();
                                  this.Hide();
                              }
                              else if (CmbApplicationName.Text == "Restaurant Management System")
                              {
                                 mod.DataBaseBackup("Login");
                                  Globalvariable.ModuleNm = "Restaurant Management System";
                                  Globalvariable.flagchar = "R";
                                  Globalvariable.OnApplicationRestaurant = "ON-R";
                                  FrmLodgeTreeview frmLodg = new FrmLodgeTreeview();
                                  frmLodg.Show();                                
                                  this.Hide();
                              }
                              else if (CmbApplicationName.Text == "Material Management System")
                              {
                                  mod.DataBaseBackup("Login");
                                  Globalvariable.ModuleNm = "Material Management System";
                                  Globalvariable.flagchar = "M";
                                  Globalvariable.OnApplicationMaterialManage = "ON-M";
                                  FrmLodgeTreeview frmLodg = new FrmLodgeTreeview();
                                  frmLodg.Show();
                                  this.Hide();
                              }
                              else if (CmbApplicationName.Text == "Accounts Management System")
                              {
                                  mod.DataBaseBackup("Login");
                                  Globalvariable.ModuleNm = "Accounts Management System";
                                  Globalvariable.flagchar = "A";
                                  Globalvariable.OnApplicationAccount = "ON-A";
                                  FrmLodgeTreeview frmLodg = new FrmLodgeTreeview();
                                  frmLodg.Show();
                                  this.Hide();
                              }                      
                    }
                    else
                    {
                        MessageBox.Show("Please Enter Correct Password !!");
                        TxtPassword.Text = "";
                        TxtPassword.Focus();
                    }
                }
          
            Globalvariable.MachSrl = GetVolumeSerial("C");
            SqlCommand cmd1 = new SqlCommand("SELECT * FROM tbPresent_users WHERE MachineUnique_ID='" + Globalvariable.MachSrl + "'", ObjCon.connect());
            SqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.Read())
            {
                cmd1 = new SqlCommand("DELETE FROM tbPresent_users WHERE MachineUnique_ID='" + Globalvariable.MachSrl + "'", ObjCon.connect());
                cmd1.ExecuteNonQuery();
            }
            SqlCommand str1 = new SqlCommand("INSERT INTO tbPresent_users (User_ID,Login_Time,MachineUnique_ID) VALUES ('" + Globalvariable.usercd + "','" + DateTime.Today.Date.ToString("yyyy/MM/dd") + "','" + Globalvariable.MachSrl + "' )", ASPLCSS.connect());
            str1.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }

        }          
        
        #endregion

        #region EXIT/CANCEL FUNCTION
        public void Exit()
        {
            Application.Exit();
        }
        #endregion

        #region Fill All Combo Box
        #region Fill User Name Combo Box
        public void FillUserNameCombo()
        {
            try
            {
                //CmbUserName.Items.Clear();
                CmbUserName.DataSource = null;
                CmbUserName.Items.Insert(0, "SELECT");
                #region Check Whether the UserNames Are Available or Not SELECT
                DataSet ds = new DataSet();            
                //CmbUserName.SelectedIndex = 0;
               // SqlCommand cmd = new SqlCommand("SELECT UserName FROM tbLoginDetails", ASPLCSS.connect());
                SqlCommand cmd = new SqlCommand("SP_MainFrmFillCombo", ObjCon.connect());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tableName", "tbLoginDetails");
                cmd.Parameters.AddWithValue("@field", "0");                           
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds.Clear();
                da.Fill(ds);
                CmbUserName.DisplayMember = "UserName";
                CmbUserName.ValueMember = "User_ID";
                if (ds.Tables[0].Rows.Count > 0)
                {
                    CmbUserName.DataSource = ds.Tables[0];
                }
                CmbApplicationName.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
            #endregion
        }
        #endregion

        #region Fill Application Name Combo Box
        public void FillApplicationNameCombo()
        {
            try
            {
                DataSet ds = new DataSet();
                CmbApplicationName.Items.Clear();
                //CmbApplicationName.DataSource = null;
                CmbApplicationName.Items.Insert(0, "SELECT");
                cmd = new SqlCommand("SP_FrmRegister", ObjCon.connect());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@variable", "SELECT");
                cmd.Parameters.AddWithValue("@MacAddress", Globalvariable.MacAddress);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Globalvariable.flagLodge = dr["Application_Name_Lodge"].ToString();
                    Globalvariable.flagResta = dr["Application_Name_Restaurant"].ToString();
                    Globalvariable.flagMateriMagmnt = dr["Application_Name_MaterialMgmnt"].ToString();
                    Globalvariable.flagAcct = dr["Application_Name_Accounts"].ToString();
                }
                string selected = this.CmbUserName.GetItemText(this.CmbUserName.SelectedItem);
                //string Usercnd = "SELECT module,Application_id FROM tbUserAccess WHERE Application_id!='' AND user_id='" + CmbUserName.SelectedValue + "'";
                if (selected != "ADMIN")
                {
                    SqlCommand Usercnd = new SqlCommand("SELECT module,Application_id FROM tbUserAccess WHERE Application_id!='' AND user_id='" + CmbUserName.SelectedValue + "'", ObjCon.connect());
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = Usercnd;
                    ds.Clear();
                    da.Fill(ds);
                    CmbCompanyName.DisplayMember = "module";
                    CmbCompanyName.ValueMember = "Application_id";
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        CmbCompanyName.DataSource = ds.Tables[0];
                    }
                }
                else
                {
                    if (Globalvariable.flagLodge == "L")
                    {
                        CmbApplicationName.Items.Add("Lodge Management System");
                    }
                    if (Globalvariable.flagResta == "R")
                    {
                        CmbApplicationName.Items.Add("Restaurant Management System");
                    }
                    if (Globalvariable.flagMateriMagmnt == "M")
                    {
                        CmbApplicationName.Items.Add("Material Management System");
                    }            
                    if (Globalvariable.flagAcct == "A")
                    {
                        CmbApplicationName.Items.Add("Accounts Management System");
                    }
                }
                CmbApplicationName.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }
        #endregion

        #region Fill Company Name Combo Box
        public void FillCompanyNameCombo()
        {
            try
            {
                DataSet ds = new DataSet();
                CmbCompanyName.DataSource = null;
                //CmbCompanyName.Items.Clear();
                CmbCompanyName.Items.Insert(0, "SELECT");
                //SqlCommand cmd = new SqlCommand("SELECT Company_Code,Company_Name FROM tbCompanyDetailsLodge", ASPLCSS.connect());
                SqlCommand cmd = new SqlCommand("SP_MainFrmFillCombo", ObjCon.connect());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tableName", "tbCompanyDetailsLodge");
                cmd.Parameters.AddWithValue("@field", "Company_Name");
               
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds.Clear();
                da.Fill(ds);
                CmbCompanyName.DisplayMember = "Company_Name";
                CmbCompanyName.ValueMember = "Company_Code";
                if (ds.Tables[0].Rows.Count > 0)
                {
                    CmbCompanyName.DataSource = ds.Tables[0];
                }
                CmbApplicationName.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }
        #endregion

        #region Fill Branch Name Combo Box
        public void FillBranchNameCombo()
        {
            try
            {     
                DataSet ds = new DataSet();
               CmbBranchName.DataSource = null;
                CmbBranchName.Items.Insert(0, "SELECT");
                SqlCommand cmd = new SqlCommand("SP_MainFrmFillCombo", ObjCon.connect());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tableName", "tbCompanyDetailsLodge");
                cmd.Parameters.AddWithValue("@field", "Branch_Name");    
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds.Clear();
                da.Fill(ds);
                CmbBranchName.DisplayMember = "Branch_Name";
                if (ds.Tables[0].Rows.Count > 0)
                {
                    CmbBranchName.DataSource = ds.Tables[0];
                }
                CmbBranchName.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }
        #endregion

        #endregion

        public string GetVolumeSerial(string strDriveLetter)
        {      
                uint serNum = 0;
                uint maxCompLen = 0;
                StringBuilder VolLabel = new StringBuilder(256); // Label
                UInt32 VolFlags = new UInt32();
                StringBuilder FSName = new StringBuilder(256); // File System Name
                strDriveLetter += ":\\"; // fix up the passed-in drive letter for the API call
                long Ret = GetVolumeInformation(strDriveLetter, VolLabel, (UInt32)VolLabel.Capacity, ref serNum, ref maxCompLen, ref VolFlags, FSName, (UInt32)FSName.Capacity);
                return Convert.ToString(serNum);            
        }

        #region Set Control Backcolor
        private void CmbApplicationName_Enter(object sender, EventArgs e)
        {
            CmbApplicationName.BackColor = Color.Yellow;
        }

        private void CmbUserName_Enter(object sender, EventArgs e)
        {
            CmbUserName.BackColor = Color.Yellow;
        }

        private void CmbCompanyName_Enter(object sender, EventArgs e)
        {
            CmbCompanyName.BackColor = Color.Yellow;
        }

        private void CmbBranchName_Enter(object sender, EventArgs e)
        {
            CmbBranchName.BackColor = Color.Yellow;
        }

        private void TxtPassword_Enter(object sender, EventArgs e)
        {
            TxtPassword.BackColor = Color.Yellow;
        }

        private void TxtPassword_Leave(object sender, EventArgs e)
        {
            TxtPassword.BackColor = Color.White;
        }

        private void CmbUserName_Leave(object sender, EventArgs e)
        {
            CmbUserName.BackColor = Color.White;
        }

        private void CmbApplicationName_Leave(object sender, EventArgs e)
        {
            CmbApplicationName.BackColor = Color.White;
        }

        private void CmbCompanyName_Leave(object sender, EventArgs e)
        {
            CmbApplicationName.BackColor = Color.White;
        }

        private void CmbBranchName_Leave(object sender, EventArgs e)
        {
            CmbBranchName.BackColor = Color.White;
        }
        #endregion
        private void CmbApplicationName_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(CmbApplicationName.BackColor), e.Bounds);
        }

        private void CmbUserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                CmbApplicationName.DataSource = null;
                cmd = new SqlCommand("SP_FrmRegister", ObjCon.connect());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@variable", "SELECT");
                cmd.Parameters.AddWithValue("@MacAddress", Globalvariable.MacAddress);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Globalvariable.flagLodge = dr["Application_Name_Lodge"].ToString();
                    Globalvariable.flagResta = dr["Application_Name_Restaurant"].ToString();
                    Globalvariable.flagMateriMagmnt = dr["Application_Name_MaterialMgmnt"].ToString();
                    Globalvariable.flagAcct = dr["Application_Name_Accounts"].ToString();
                }
                //string Usercnd = "SELECT module,Application_id FROM tbUserAccess WHERE Application_id!='' AND user_id='" + CmbUserName.SelectedValue + "'";
                string selected = this.CmbUserName.GetItemText(this.CmbUserName.SelectedItem);
                if (selected != "ADMIN")
                {
                    SqlCommand Usercnd = new SqlCommand("SELECT module,Application_id FROM tbUserAccess WHERE Application_id!='' AND user_id='" + CmbUserName.SelectedValue + "' AND FormAccess='Y'", ObjCon.connect());
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = Usercnd;
                    ds.Clear();
                    da.Fill(ds);
                    CmbApplicationName.DisplayMember = "module";
                    CmbApplicationName.ValueMember = "Application_id";
                    if(ds.Tables[0].Rows.Count>0)
                    {
                    CmbApplicationName.DataSource = ds.Tables[0];
                    }
                }
                else
                {
                    CmbApplicationName.DataSource=null;
                    if (Globalvariable.flagLodge == "L")
                    {
                        CmbApplicationName.Items.Add("Lodge Management System");
                    }
                    if (Globalvariable.flagResta == "R")
                    {
                        CmbApplicationName.Items.Add("Restaurant Management System");
                    }
                    if (Globalvariable.flagMateriMagmnt == "M")
                    {
                        CmbApplicationName.Items.Add("Material Management System");
                    }
                    
                    if (Globalvariable.flagAcct == "A")
                    {
                        CmbApplicationName.Items.Add("Accounts Management System");
                    }
                }
                CmbApplicationName.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }

        }

        private void LblAddressDetail_Click(object sender, EventArgs e)
        {

        }

        private void LblFooterCopyRights_Click(object sender, EventArgs e)
        {

        }
    }
}

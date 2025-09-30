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
using System.Collections;

namespace ASPL.Utility
{
    public partial class FrmCompanySelect : Form
    {
        public FrmCompanySelect()
        {
            InitializeComponent();
        }
        SqlDataReader compdetail_dr, finanYear_dr;
        Module mod = new Module();
        Connection Objcon = new Connection();
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
        private void FrmCompanySelect_Load(object sender, EventArgs e)
        {
            LOAD();
        }
        public void LOAD()
        {
            try
            {
                DataTable datatable = new DataTable();
                SqlDataAdapter da_comdetail = new SqlDataAdapter("SELECT * FROM tbCompanyDetailsLodge ORDER BY Company_Code", Objcon.connect());
                da_comdetail.Fill(datatable);
                lstCompaName.DisplayMember = "Company_Name";
                //   lstCompaName.ValueMember = "";
                lstCompaName.DataSource = datatable;
                //  lstCompaName.SelectedIndex = 0;
                ListCompname();
                BtnConnect.Enabled = false;
                BtnDetail.Enabled = false;
                if (Globalvariable.username == "ADMIN")
                {
                    BtnNewUser.Visible = true;
                    BtnNewUser.Enabled = false;
                }
                else
                {
                    BtnNewUser.Enabled = false;
                }
                txtpath.Visible = false;
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        public void financialyear()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbYear WHERE Company_Code='" + Globalvariable.bcode + "' ORDER BY Sr_No desc", Objcon.connect());
                finanYear_dr = cmd.ExecuteReader();
                if (finanYear_dr.Read())
                {
                    string year = finanYear_dr["Financial_Start_Year"].ToString();
                    Globalvariable.StartYear = Convert.ToInt32(year);
                    year = finanYear_dr["Financial_End_Year"].ToString();
                    Globalvariable.EndYear = Convert.ToInt32(year);
                    DateTime startDate = Convert.ToDateTime(finanYear_dr["Start_Date"]);
                    Globalvariable.StartDate = startDate.ToString("MM/dd/yyyy");
                    DateTime endDate = Convert.ToDateTime(finanYear_dr["End_Date"]);
                    Globalvariable.EndDate = endDate.ToString("MM/dd/yyyy");
                    Globalvariable.company_Srno = Convert.ToInt32(finanYear_dr["Sr_No"]);
                }
                finanYear_dr.Close();
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        public void ConnectData()
        {
            try
            {
                FrmCompanyDetails compDetail = new FrmCompanyDetails();
                Globalvariable.SPath = txtpath.Text;
                FrmCompanySelect comselect = new FrmCompanySelect();
                comselect.Visible = false;
                // FrmcomanyDetails Function call DataBaseBackup
                compDetail.DataBaseBackup();
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        public void UpdateData()
        {
            try
            {
                Globalvariable.SPath = txtpath.Text;
                Globalvariable.FrmCompSelec_ComNam = lstCompaName.SelectedItem.ToString();
                this.Close();
                FrmCompanyDetails compDetail = new FrmCompanyDetails();
                compDetail.ShowDialog();
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }

        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            ConnectData();
        }

        private void BtnDetail_Click(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnNewUser_Click(object sender, EventArgs e)
        {
            NewUser();
        }

        public void NewUser()
        {
        }

        private void lstCompaName_Click(object sender, EventArgs e)
        {


        }

        public void ListCompname()
        {
            try
            {
                // Fill Table tbCompanyDetailLodge value in Globalvariable
                string st = "SELECT * FROM tbCompanyDetailsLodge WHERE Company_Code='" + Globalvariable.bcode + "'";
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbCompanyDetailsLodge WHERE Company_Code='"+Globalvariable.bcode +"' ", Objcon.connect());
                compdetail_dr = cmd.ExecuteReader();
                if (compdetail_dr.Read())
                {
                    Globalvariable.company_nm = lstCompaName.Text;
                    Globalvariable.bcode = compdetail_dr["Company_Code"].ToString();
                    Globalvariable.company_nm = compdetail_dr["Company_Name"].ToString();
                    Globalvariable.PrintCompany_logoYN=compdetail_dr["PrintCompanyLogoYN"].ToString();
                    if (Globalvariable.PrintCompany_logoYN == "Y")
                    {
                        if (compdetail_dr["LogoName"].ToString() != "" && compdetail_dr["Company_Logo"].ToString() != "")
                        {
                            Globalvariable.company_logo_byte = (byte[])compdetail_dr["Company_Logo"];
                            Globalvariable.company_logoname = compdetail_dr["LogoName"].ToString();
                        }
                    }
                    Globalvariable.company_Add1 = mod.Isnull(compdetail_dr["Address1"].ToString(), "");
                    Globalvariable.company_Add2 = mod.Isnull(compdetail_dr["Address2"].ToString(), "");
                    Globalvariable.company_Add3 = mod.Isnull(compdetail_dr["Address3"].ToString(), "");
                    Globalvariable.company_phno1 = mod.Isnull(compdetail_dr["PhoneNo1"].ToString(), "");
                    Globalvariable.company_phno2 = mod.Isnull(compdetail_dr["PhoneNo2"].ToString(), "");
                    Globalvariable.company_EmailID1 = mod.Isnull(compdetail_dr["EMailId"].ToString(), "");
                    Globalvariable.company_VATtinNo = mod.Isnull(compdetail_dr["VATtinNo"].ToString(), "");
                    Globalvariable.company_Servicetax = mod.Isnull(compdetail_dr["ServiceTaxNo"].ToString(), "");
                    Globalvariable.company_CST_No = mod.Isnull(compdetail_dr["CST_No"].ToString(), "");
                    Globalvariable.company_LuxtaxNo = mod.Isnull(compdetail_dr["LuxuryTaxNo"].ToString(), "");
                    Globalvariable.company_PanNo = mod.Isnull(compdetail_dr["PanNo"].ToString(), "");
                    Globalvariable.company_DatabasNam = mod.Isnull(compdetail_dr["DataBase_Nm"].ToString(), "");
                    Globalvariable.company_GSTNo = mod.Isnull(compdetail_dr["GSTNo"].ToString(), "");
                    Globalvariable.company_SACNo = mod.Isnull(compdetail_dr["SACNo"].ToString(), "");
                    Globalvariable.company_BilHeading = mod.Isnull(compdetail_dr["BilHeading"].ToString(), "");
                    Globalvariable.company_SpecialMsg = mod.Isnull(compdetail_dr["SpecialMsg"].ToString(), "");
                    Globalvariable.company_PrintComNameBil = mod.Isnull(compdetail_dr["PrintComNameBil"].ToString(), "N");
                    Globalvariable.company_PrintAddressYN = mod.Isnull(compdetail_dr["PrintAddressYN"].ToString(), "N");
                    Globalvariable.company_PrintAftrSaviKOT = mod.Isnull(compdetail_dr["PrintAftrSaviKOT"].ToString(), "N");
                  
                    Globalvariable.company_PrintBilYN = mod.Isnull(compdetail_dr["PrintBilYN"].ToString(), "N");
                    Globalvariable.company_PrintNoOfPersBil = mod.Isnull(compdetail_dr["PrintNoOfPersBil"].ToString(), "N");
                    Globalvariable.company_TimeOnBilYN = mod.Isnull(compdetail_dr["TimeOnBilYN"].ToString(), "N");
                    Globalvariable.company_AllowDelay = mod.Isnull(compdetail_dr["AllowDelay"].ToString(), "N");
                    Globalvariable.company_MiniMumPageLengthofBill = mod.Isnull(compdetail_dr["PageLengthBil"].ToString(), "");
                    Globalvariable.company_NumberOfLinesForKOT = mod.Isnull(compdetail_dr["NoOfLinKOT"].ToString(), "");
                    Globalvariable.company_AllowSharingYN = mod.Isnull(compdetail_dr["AllowSharingYN"].ToString(), "N");
                    Globalvariable.company_Servicetax = mod.Isnull(compdetail_dr["ServiceTaxPer"].ToString(), "");
                    Globalvariable.company_ReprintYN = mod.Isnull(compdetail_dr["ReprintYN"].ToString(), "N");
                    Globalvariable.company_KOTCancel = mod.Isnull(compdetail_dr["KOTCancel"].ToString(), "");
                    Globalvariable.company_NoOfLineForward = mod.Isnull(compdetail_dr["NoOfLineForward"].ToString(), "");
                    Globalvariable.company_NoOfLineBackWard = mod.Isnull(compdetail_dr["NoOfLineBackWard"].ToString(), "");
                    Globalvariable.company_MailActYN = mod.Isnull(compdetail_dr["MailActYN"].ToString(), "N");
                    //mailID,Mailhead

                    Globalvariable.company_SendMailGuestYN = mod.Isnull(compdetail_dr["SendMailGuestYN"].ToString(), "N");
                    Globalvariable.company_SendMailComYN = mod.Isnull(compdetail_dr["SendMailComYN"].ToString(), "N");
                    Globalvariable.company_SaleRefNoYN = mod.Isnull(compdetail_dr["SaleRefNoYN"].ToString(), "N");
                    Globalvariable.company_PrintRoomTerfYN = mod.Isnull(compdetail_dr["PrintRomTerifBilYN"].ToString(), "N");
                    Globalvariable.company_PerHeadTeriff = mod.Isnull(compdetail_dr["PerHeadTerifBilYN"].ToString(), "N");
                    Globalvariable.company_multiRoomTerif = mod.Isnull(compdetail_dr["MultiRomTerifTotaPax"].ToString(), "N");
                    Globalvariable.company_TaxAfterDisc = mod.Isnull(compdetail_dr["TaxAfterDiscYN"].ToString(), "N");
                    Globalvariable.company_Mobile1 = mod.Isnull(compdetail_dr["MobileNo1"].ToString(), "");
                    Globalvariable.company_Mobile2 = mod.Isnull(compdetail_dr["MobileNo2"].ToString(), "");
                    Globalvariable.company_Mobile3 = mod.Isnull(compdetail_dr["MobileNo3"].ToString(), "");
                    Globalvariable.company_TotalDiscGLCode = mod.Isnull(compdetail_dr["TotalDiscGLCode"].ToString(), "0");
                    Globalvariable.company_ExtraPerson = mod.Isnull(compdetail_dr["ExtraPerson"].ToString(), "0");
                    Globalvariable.company_Roomservice = mod.Isnull(compdetail_dr["RoomService"].ToString(), "0");
                    Globalvariable.company_CheckOutTim = (mod.Isnull(compdetail_dr["CheckOutTim"].ToString(), "12:00:00 PM"));
                    Globalvariable.company_CheckOut12_24YN = mod.Isnull(compdetail_dr["CheckOut12_24"].ToString(), "Y");
                    Globalvariable.company_CheckInTime = (mod.Isnull(compdetail_dr["CheckInTime"].ToString(), "10:00:00 AM"));
                    Globalvariable.company_Roomservice = mod.Isnull(compdetail_dr["RoomService"].ToString(), "0");
                    Globalvariable.company_Restaurantsale = mod.Isnull(compdetail_dr["Restaurantsale"].ToString(), "0");
                    Globalvariable.company_TallyVoucherTyp = mod.Isnull(compdetail_dr["TallyVoucherTyp"].ToString(), "0");
                    Globalvariable.company_LuxtaxMastcode = mod.Isnull(compdetail_dr["LuxTaxmastCod"].ToString(), "0");
                    Globalvariable.company_ServtaxMastcode = mod.Isnull(compdetail_dr["ServiceTaxmastCode"].ToString(), "0");
                    Globalvariable.company_CallRecordFilepath = mod.Isnull(compdetail_dr["CallRecordFilepath"].ToString(), "");
                    Globalvariable.company_PhonCalGrpID = mod.Isnull(compdetail_dr["PhonCalGrpID"].ToString(), "0");
                    Globalvariable.company_PhonCalItemID = mod.Isnull(compdetail_dr["PhonCalItemID"].ToString(), "0");
                    Globalvariable.company_TotalDiscTalyCod = mod.Isnull(compdetail_dr["TotalDiscTalyCod"].ToString(), "0");
                    Globalvariable.company_Roomservice = mod.Isnull(compdetail_dr["TotalDiscTalyCod"].ToString(), "0");
                    Globalvariable.company_RoomServiceDirectPer = mod.Isnull(compdetail_dr["RoomServiDirectPer"].ToString(), "0");
                    Globalvariable.company_LockingAuthorization = mod.Isnull(compdetail_dr["LockingAuthorization"].ToString(), "");
                    Globalvariable.company_DefaultGuestID = mod.Isnull(compdetail_dr["DefaultGuestID"].ToString(), "");
                    Globalvariable.company_BillSetleme = mod.Isnull(compdetail_dr["BillSettleMentYN"].ToString(), "N");
                    Globalvariable.company_PrintSaleCrystYN = mod.Isnull(compdetail_dr["PrintsaleCrystReportYN"].ToString(), "N");
                    Globalvariable.company_AutoRefnoYN = mod.Isnull(compdetail_dr["AutoRefNoYN"].ToString(), "N");
                    Globalvariable.company_DetaiRepopYN = mod.Isnull(compdetail_dr["DetailBilreportYN"].ToString(), "N");
                    Globalvariable.company_SeperatBilYN = mod.Isnull(compdetail_dr["SeperateBilNoYN"].ToString(), "N");//
                    Globalvariable.company_CapilryFilYN = mod.Isnull(compdetail_dr["CapillaryfileYN"].ToString(), "N");
                    Globalvariable.company_LockingSystemYN = mod.Isnull(compdetail_dr["LockingSystemYN"].ToString(), "N");
                    Globalvariable.company_DefaultLegID = mod.Isnull(compdetail_dr["DefaultLegID"].ToString(), "N");




                    Globalvariable.company_SmsUser = mod.Isnull(compdetail_dr["SmsUserName"].ToString(), "");
                    Globalvariable.company_SmsPasswor = mod.Isnull(compdetail_dr["SmsPassword"].ToString(), "");
                    Globalvariable.company_Smssender = mod.Isnull(compdetail_dr["SmsSender"].ToString(), "");
                    Globalvariable.company_ReceiverEmail1 = mod.Isnull(compdetail_dr["ReceiverEmail1"].ToString(), "0");
                    Globalvariable.company_ReceiverEmail2 = mod.Isnull(compdetail_dr["ReceiverEmail2"].ToString(), "0");
                    Globalvariable.company_RegistrationKey = mod.Isnull(compdetail_dr["RegistrationKey"].ToString(), "0");
                    Globalvariable.company_DatabasPath = mod.Isnull(compdetail_dr["DataBasePath"].ToString(), "");
                    Globalvariable.company_BankDetail = mod.Isnull(compdetail_dr["BankDetail"].ToString(), "");
                    txtpath.Text = Globalvariable.company_DatabasPath;
                    Globalvariable.company_Mailtocust = mod.Isnull(compdetail_dr["MailID"].ToString(), "");
                    Globalvariable.company_MailPass = mod.Isnull(compdetail_dr["MailPassword"].ToString(), "");
                    Globalvariable.company_MailHead = mod.Isnull(compdetail_dr["MailHead"].ToString(), "");
                    Globalvariable.company_MailBody = mod.Isnull(compdetail_dr["MailBody"].ToString(), "");

                    Globalvariable.company_BackupPath = mod.Isnull(compdetail_dr["BackUpPathName"].ToString(), "");








                    // Fill Year listbox
                    DataTable dt = new DataTable();
                    SqlDataAdapter tbyer_da = new SqlDataAdapter("SELECT CONVERT(NVARCHAR(8),Financial_Start_Year) +'-'+CONVERT(NVARCHAR(8),Financial_End_Year) as 'StartEndYear',Sr_No FROM tbYear WHERE Company_Code='" + Globalvariable.bcode + "' ORDER BY Sr_No desc", Objcon.connect());
                    tbyer_da.Fill(dt);
                    lstFinanYear.DisplayMember = "StartEndYear";
                    lstFinanYear.ValueMember = "Sr_No";
                    lstFinanYear.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void lstCompaName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListCompname();
        }

        private void lstFinanYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            // SqlCommand cmd = new SqlCommand("SELECT CONVERT(NVARCHAR(8),Financial_Start_Year) +'-'+CONVERT(NVARCHAR(8),Financial_End_Year) as 'StartEndYear' FROM tbYear WHERE Company_Code='" + Globalvariable.bcode + "' ORDER BY Sr_No desc", Objcon.connect());
            ListFinanceYear();
        }

        public void ListFinanceYear()
        {
            try
            {
                // SqlCommand cmd = new SqlCommand("SELECT * FROM tbYear WHERE Company_Code='" + Globalvariable.bcode + "' ORDER BY Sr_No desc", Objcon.connect());
                SqlCommand cmd = new SqlCommand("SP_CompanyDetails", Objcon.connect());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tableName", "tbYear");
                cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                finanYear_dr = cmd.ExecuteReader();
                if (finanYear_dr.Read())
                {
                    string year = finanYear_dr["Financial_Start_Year"].ToString();
                    Globalvariable.StartYear = Convert.ToInt32(year);
                    year = finanYear_dr["Financial_End_Year"].ToString();
                    Globalvariable.EndYear = Convert.ToInt32(year);
                    DateTime startDate = Convert.ToDateTime(finanYear_dr["Start_Date"]);
                    Globalvariable.StartDate = startDate.ToString("MM/dd/yyyy");
                    DateTime endDate = Convert.ToDateTime(finanYear_dr["End_Date"]);
                    Globalvariable.EndDate = endDate.ToString("MM/dd/yyyy");
                    Globalvariable.company_Srno = Convert.ToInt32(finanYear_dr["Sr_No"]);
                }
                finanYear_dr.Close();
                // SqlCommand Companydetailcmd = new SqlCommand("SELECT * FROM tbCompanyDetailsLodge ORDER BY Company_Code", Objcon.connect());
                SqlCommand Companydetailcmd = new SqlCommand("SP_CompanyDetails", Objcon.connect());
                Companydetailcmd.CommandType = CommandType.StoredProcedure;
                Companydetailcmd.Parameters.AddWithValue("@tableName", "tbCompanyDetailsLodge");
                Companydetailcmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                compdetail_dr = Companydetailcmd.ExecuteReader();
                if (compdetail_dr.Read())
                {
                    Globalvariable.company_PrintAftrSaviKOT = mod.Isnull(compdetail_dr["PrintAftrSaviKOT"].ToString(), "N");
                    Globalvariable.company_PrintComNameBil = mod.Isnull(compdetail_dr["PrintComNameBil"].ToString(), "N");
                    Globalvariable.company_PrintAddressYN = mod.Isnull(compdetail_dr["PrintAddressYN"].ToString(), "N");
                    Globalvariable.company_PrintBilYN = mod.Isnull(compdetail_dr["PrintBilYN"].ToString(), "N");
                    Globalvariable.company_PrintNoOfPersBil = mod.Isnull(compdetail_dr["PrintNoOfPersBil"].ToString(), "N");
                    Globalvariable.company_PrintDetailBil = mod.Isnull(compdetail_dr["PrintDetailBil"].ToString(), "N");
                    Globalvariable.company_TimeOnBilYN = mod.Isnull(compdetail_dr["TimeOnBilYN"].ToString(), "N");
                    Globalvariable.company_AllowSharingYN = mod.Isnull(compdetail_dr["AllowSharingYN"].ToString(), "N");
                    Globalvariable.company_KOTCancel = mod.Isnull(compdetail_dr["KOTCancel"].ToString(), "N");
                    Globalvariable.company_AllowDelay = mod.Isnull(compdetail_dr["AllowDelay"].ToString(), "N");
                    Globalvariable.company_ReprintYN = mod.Isnull(compdetail_dr["ReprintYN"].ToString(), "N");
                    Globalvariable.company_MultiUser = mod.Isnull(compdetail_dr["MultiUser"].ToString(), "N");
                    Globalvariable.company_SMSActiv = mod.Isnull(compdetail_dr["SMSActiv"].ToString(), "N");
                    Globalvariable.company_MailActYN = mod.Isnull(compdetail_dr["MailActYN"].ToString(), "N");
                    Globalvariable.company_SendMailGuestYN = mod.Isnull(compdetail_dr["SendMailGuestYN"].ToString(), "N");
                    Globalvariable.company_SendMailComYN = mod.Isnull(compdetail_dr["SendMailComYN"].ToString(), "N");
                    Globalvariable.company_XPVersionYN = mod.Isnull(compdetail_dr["XPVersionYN"].ToString(), "Y");
                    Globalvariable.company_SmsUser = mod.Isnull(compdetail_dr["SmsUserName"].ToString(), "");
                    Globalvariable.company_SmsPasswor = mod.Isnull(compdetail_dr["SmsPassword"].ToString(), "");
                    Globalvariable.company_Smssender = mod.Isnull(compdetail_dr["SmsSender"].ToString(), "");
                    Globalvariable.company_Mailtocust = mod.Isnull(compdetail_dr["MailID"].ToString(), "");
                    Globalvariable.company_MailPass = mod.Isnull(compdetail_dr["MailPassword"].ToString(), "");
                    Globalvariable.company_MailHead = mod.Isnull(compdetail_dr["MailHead"].ToString(), "");
                    Globalvariable.company_MailBody = mod.Isnull(compdetail_dr["MailBody"].ToString(), "");
                    Globalvariable.company_VATtinNo = mod.Isnull(compdetail_dr["VATtinNo"].ToString(), "");
                    Globalvariable.company_CST_No = mod.Isnull(compdetail_dr["CST_No"].ToString(), "");
                    Globalvariable.company_PanNo = mod.Isnull(compdetail_dr["PanNo"].ToString(), "");
                    Globalvariable.company_LuxtaxNo = mod.Isnull(compdetail_dr["LuxuryTaxNo"].ToString(), "");
                    Globalvariable.company_SpecialMsg = mod.Isnull(compdetail_dr["SpecialMsg"].ToString(), "");
                    Globalvariable.company_BilHeading = mod.Isnull(compdetail_dr["BilHeading"].ToString(), "");
                    Globalvariable.branch_nm = mod.Isnull(compdetail_dr["Branch_Name"].ToString(), "");
                    Globalvariable.company_NoOfLineBackWard = mod.Isnull(compdetail_dr["NoOfLineBackWard"].ToString(), "0");
                    Globalvariable.company_NoOfLineForward = mod.Isnull(compdetail_dr["NoOfLineForward"].ToString(), "0");
                    Globalvariable.company_ServiceTaxPer = mod.Isnull(compdetail_dr["ServiceTaxPer"].ToString(), "0");
                    Globalvariable.company_NoOfLinKOT = mod.Isnull(compdetail_dr["NoOfLinKOT"].ToString(), "0");
                    //DateTime chekTim = Convert.ToDateTime(compdetail_dr["CheckOutTim"]);
                    //Globalvariable.company_CheckOutTim = chekTim.ToString("HH:mm:ss tt");
                    Globalvariable.company_CheckOutTim = (mod.Isnull(compdetail_dr["CheckOutTim"].ToString(), "12:00:00 PM"));
                    Globalvariable.company_CheckOut12_24YN = mod.Isnull(compdetail_dr["CheckOut12_24"].ToString(), "Y");
                    Globalvariable.company_CheckInTime = (mod.Isnull(compdetail_dr["CheckInTime"].ToString(), "10:00:00 AM"));
                    Globalvariable.company_PanNo = mod.Isnull(compdetail_dr["PanNo"].ToString(), "");
                    Globalvariable.company_SaleRefNoYN = mod.Isnull(compdetail_dr["SaleRefNoYN"].ToString(), "N");                  
                    Globalvariable.company_PrintRoomTerfYN = mod.Isnull(compdetail_dr["PrintRomTerifBilYN"].ToString(), "N");
                    Globalvariable.company_multiRoomTerif = mod.Isnull(compdetail_dr["MultiRomTerifTotaPax"].ToString(), "N");
                    string year = Convert.ToString(Globalvariable.StartYear);
                    if (year != "")
                    {
                        BtnConnect.Enabled = true;
                        BtnDetail.Enabled = true;
                        BtnNewUser.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }
    }
}

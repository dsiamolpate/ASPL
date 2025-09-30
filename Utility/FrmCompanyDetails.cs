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
    public partial class FrmCompanyDetails : Form
    {
        public FrmCompanyDetails()
        {
            InitializeComponent();
        }
        //   Connection ObjCon = new Connection();
        #region varibles
        Connection con = new Connection();
        static int val;
        SqlCommand cmd;
        Module mod = new Module();
        int detail_id = 0;
        string FilCom="N", opmode, ImageName;
        string Insert, smsAct, MailAct , PrintComNmBil, PrintAdd , PrintAfteSavKOT, prinBilYN, PrintNoPerBil, lockingsystem ;
        string PriDetaiBil, TimONbil, allowDelay, allowshaYN, ReprintYN, KotCancel, SendMailGuest, SendmailCom , saleRefNo , printGSTserialYN , seperateRestDirectYN ;
        string PrintRoomTerif , PerHeadTerif , multiromterif , taxAfterdic , BillSetle , Printsalecry , AutoRefNo , DetailBilrep, seperatebilNo, capilaryFile , printcompanylogoYN;
        private System.Drawing.Color m_tbcolorenter = System.Drawing.Color.Yellow;
        private System.Drawing.Color m_tbcolorleave = System.Drawing.Color.White;
        bool NFY;
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
                MessageBox.Show("hi");
            }
        }
        #endregion

        #region control enable true or false
        public void controlEnablefalse(Control.ControlCollection ctrls)
        {
            try
            {
                //selected tab control enabled false    
                foreach (Control cntrl in ctrls)
                {
                    if (cntrl is ComboBox || cntrl is TextBox || cntrl is DateTimePicker)
                    {
                        ((Control)cntrl).Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        public void controlEnableTrue(Control.ControlCollection ctrls)
        {
            try
            {
                //selected tab control enabled true
                foreach (Control cntrl in ctrls)
                {
                    if (cntrl is ComboBox || cntrl is TextBox || cntrl is DateTimePicker)
                    {
                        ((Control)cntrl).Enabled = true;
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

        #region Frm event
        private void FrmCompanyDetails_Load(object sender, EventArgs e)
        {
            try
            {
                opmode = "L";
                controlEnablefalse(tabCompanydetail.TabPages[0].Controls);
                controlEnablefalse(tabCompanydetail.TabPages[1].Controls);
                grpboxSupPass.Visible = false;
                tabCompanydetail.SelectTab(tabDetail);
                mod.CmbFill(this.tabCompanydetail.TabPages[0].Controls);
                txtCompNm.Focus();
                if ( Globalvariable.usercd !=null)
                {
                    FillDetails();
                    BtnSave.Enabled = false;
                }
                else
                {
                    opmode = "A";
                    AddCompany();
                    BtnSave.Enabled = true;
                    BtnUpdate.Enabled = false;
                    btnStartBackup.Enabled = false;
                   
                    controlEnableTrue(tabCompanydetail.TabPages[0].Controls);
                    controlEnableTrue(tabCompanydetail.TabPages[1].Controls);
                    if (cmbSmsAct.SelectedItem == "Yes")
                    {
                        txtDetailUseNm.Enabled = true;
                        txtDetailPass.Enabled = true;
                        txtSender.Enabled = true;
                    }
                    if (cmbMailAct.SelectedItem == "Yes")
                    {
                        txtmailToCust.Enabled = true;
                        txtCustPass.Enabled = true;
                        txtMailhead.Enabled = true;
                        txtMailbody.Enabled = true;
                        cmbMailGust.Enabled = true;
                        cmbMailGust.Enabled = true;
                    }
                    btnNewComp.Enabled = false;                 
                    BtnExit.Enabled = true;
                }
                if (Globalvariable.frmName == "UtilityComDetail")
                {
                    BtnNewFinYear.Enabled = false;
                }
                else
                {
                    BtnNewFinYear.Enabled = true;
                }
               
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void FrmCompanyDetails_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Control nextTab;
                if (e.KeyChar == (char)Keys.Enter)
                {
                    nextTab = ((Control)sender);
                    if (ActiveControl.Name == "cmbSmsAct")
                    {
                        if (cmbSmsAct.SelectedItem == "Yes")
                        {
                            txtDetailUseNm.Enabled = true;
                            txtDetailPass.Enabled = true;
                            txtSender.Enabled = true;
                            txtDetailUseNm.Focus();
                        }
                        else if (cmbSmsAct.SelectedItem == "No")
                        {
                            txtDetailUseNm.Enabled = false;
                            txtDetailPass.Enabled = false;
                            txtSender.Enabled = false;
                            txtRecivEmail1.Focus();
                        }
                    }
                    else
                    {
                        nextTab = GetNextControl(ActiveControl, true);
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

        #region set control back color

        private void txtDefaltLegId_Enter_1(object sender, EventArgs e)
        {
            txtDefaltLegId.BackColor = Color.Yellow;
        }
        private void cmbPrintGSTSerialNoYN_Enter_1(object sender, EventArgs e)
        {
            cmbPrintGSTSerialNoYN.BackColor = Color.Yellow;
        }

        private void cmbPrintGSTSerialNoYN_Leave_1(object sender, EventArgs e)
        {
            cmbPrintGSTSerialNoYN.BackColor = Color.White;
        }

        private void cmbSepeRestadirYN_Enter_1(object sender, EventArgs e)
        {
            cmbSepeRestadirYN.BackColor = Color.Yellow;
        }

        private void cmbSepeRestadirYN_Leave_1(object sender, EventArgs e)
        {
            cmbSepeRestadirYN.BackColor = Color.White;
        }
        private void txtRecivEmail1_Enter(object sender, EventArgs e)
        {
            txtRecivEmail1.BackColor = m_tbcolorenter;
        }

        private void txtRecivEmail1_Leave(object sender, EventArgs e)
        {
            txtRecivEmail1.BackColor = m_tbcolorleave;
        }

        private void txtReceivEmailId2_Enter(object sender, EventArgs e)
        {
            txtReceivEmailId2.BackColor = m_tbcolorenter;
        }

        private void txtReceivEmailId2_Leave(object sender, EventArgs e)
        {
            txtReceivEmailId2.BackColor = m_tbcolorleave;
        }
        private void dateTimePicker2_Enter(object sender, EventArgs e)
        {
            dateTimePicker2.BackColor = m_tbcolorenter;
        }

        private void dateTimePicker2_Leave(object sender, EventArgs e)
        {
            dateTimePicker2.BackColor = m_tbcolorleave;
        }

        private void txtRestSale_Enter(object sender, EventArgs e)
        {
            txtRestSale.BackColor = m_tbcolorenter;
        }

        private void txtRestSale_Leave(object sender, EventArgs e)
        {
            txtRestSale.BackColor = m_tbcolorleave;
        }

        private void txtTallyVoucherTyp_Enter(object sender, EventArgs e)
        {
            txtTallyVoucherTyp.BackColor = m_tbcolorenter;
        }

        private void txtLuxTaxMastCod_Enter(object sender, EventArgs e)
        {
            txtLuxTaxMastCod.BackColor = m_tbcolorenter;
        }

        private void txtTallyVoucherTyp_Leave(object sender, EventArgs e)
        {
            txtTallyVoucherTyp.BackColor = m_tbcolorleave;
        }

        private void txtLuxTaxMastCod_Leave(object sender, EventArgs e)
        {
            txtLuxTaxMastCod.BackColor = m_tbcolorleave;
        }

        private void cmbBilSetle_Enter(object sender, EventArgs e)
        {
            cmbBilSetle.BackColor = m_tbcolorenter;
        }

        private void cmbBilSetle_Leave(object sender, EventArgs e)
        {
            cmbBilSetle.BackColor = m_tbcolorleave;
        }

        private void txtServTaxMastCod_Enter(object sender, EventArgs e)
        {
            txtServTaxMastCod.BackColor = m_tbcolorenter;
        }

        private void txtServTaxMastCod_Leave(object sender, EventArgs e)
        {
            txtServTaxMastCod.BackColor = m_tbcolorleave;
        }

        private void cmbPrintsaleCrystreport_Enter(object sender, EventArgs e)
        {
            cmbPrintsaleCrystreport.BackColor = m_tbcolorenter;
        }

        private void cmbPrintsaleCrystreport_Leave(object sender, EventArgs e)
        {
            cmbPrintsaleCrystreport.BackColor = m_tbcolorleave;
        }

        private void txtCallRecordFilPath_Enter(object sender, EventArgs e)
        {
            txtCallRecordFilPath.BackColor = m_tbcolorenter;
        }

        private void txtCallRecordFilPath_Leave(object sender, EventArgs e)
        {
            txtCallRecordFilPath.BackColor = m_tbcolorleave;
        }

        private void cmbautoRefNo_Enter(object sender, EventArgs e)
        {
            cmbautoRefNo.BackColor = m_tbcolorenter;
        }

        private void cmbautoRefNo_Leave(object sender, EventArgs e)
        {
            cmbautoRefNo.BackColor = m_tbcolorleave;
        }

        private void txtPhonCalGropID_Enter(object sender, EventArgs e)
        {
            txtPhonCalGropID.BackColor = m_tbcolorenter;
        }

        private void txtPhonCalGropID_Leave(object sender, EventArgs e)
        {
            txtPhonCalGropID.BackColor = m_tbcolorleave;
        }

        private void cmbDetailBilReport_Enter(object sender, EventArgs e)
        {
            cmbDetailBilReport.BackColor = m_tbcolorenter;
        }

        private void cmbDetailBilReport_Leave(object sender, EventArgs e)
        {
            cmbDetailBilReport.BackColor = m_tbcolorleave;
        }

        private void txtPhonCalItmId_Enter(object sender, EventArgs e)
        {
            txtPhonCalItmId.BackColor = m_tbcolorenter;
        }

        private void txtPhonCalItmId_Leave(object sender, EventArgs e)
        {
            txtPhonCalItmId.BackColor = m_tbcolorleave;
        }

        private void cmbSeprteBilNo_Enter(object sender, EventArgs e)
        {
            cmbSeprteBilNo.BackColor = m_tbcolorenter;
        }

        private void cmbSeprteBilNo_Leave(object sender, EventArgs e)
        {
            cmbSeprteBilNo.BackColor = m_tbcolorleave;
        }

        private void txtDisTalCod_Enter(object sender, EventArgs e)
        {
            txtDisTalCod.BackColor = m_tbcolorenter;
        }

        private void txtDisTalCod_Leave(object sender, EventArgs e)
        {
            txtDisTalCod.BackColor = m_tbcolorleave;
        }

        private void cmbCapilFile_Enter(object sender, EventArgs e)
        {
            cmbCapilFile.BackColor = m_tbcolorenter;
        }

        private void cmbCapilFile_Leave(object sender, EventArgs e)
        {
            cmbCapilFile.BackColor = m_tbcolorleave;
        }

        private void txtRomservDirPer_Enter(object sender, EventArgs e)
        {
            txtRomservDirPer.BackColor = m_tbcolorenter;
        }

        private void txtRomservDirPer_Leave(object sender, EventArgs e)
        {
            txtRomservDirPer.BackColor = m_tbcolorleave;
        }

        private void cmbLockSystm_Enter(object sender, EventArgs e)
        {
            cmbLockSystm.BackColor = m_tbcolorenter;
        }

        private void cmbLockSystm_Leave(object sender, EventArgs e)
        {
            cmbLockSystm.BackColor = m_tbcolorleave;
        }

        private void txtLockAuto_Enter(object sender, EventArgs e)
        {
            txtLockAuto.BackColor = m_tbcolorenter;
        }

        private void txtLockAuto_Leave(object sender, EventArgs e)
        {
            txtLockAuto.BackColor = m_tbcolorleave;
        }

        

        private void txtDefaltLegId_Leave(object sender, EventArgs e)
        {
            txtDefaltLegId.BackColor = m_tbcolorleave;
        }

        private void txtGuestID_Enter(object sender, EventArgs e)
        {
            txtGuestID.BackColor = m_tbcolorenter;
        }

        private void txtGuestID_Leave(object sender, EventArgs e)
        {
            txtGuestID.BackColor = m_tbcolorleave;
        }
        private void txtCompNm_Enter(object sender, EventArgs e)
        {
            txtCompNm.BackColor = m_tbcolorenter;
        }
        private void txtCompNm_Leave(object sender, EventArgs e)
        {
            txtCompNm.BackColor = m_tbcolorleave;
        }
        private void textCompNm2_Enter(object sender, EventArgs e)
        {
            textCompNm2.BackColor = m_tbcolorenter;
        }
        private void textCompNm2_Leave(object sender, EventArgs e)
        {
            textCompNm2.BackColor = m_tbcolorleave;
        }

        private void dtpStartdate_Enter(object sender, EventArgs e)
        {
            dtpStartdate.BackColor = m_tbcolorenter;
        }
        private void dtpStartdate_Leave(object sender, EventArgs e)
        {
            dtpStartdate.BackColor = m_tbcolorleave;
        }

        private void dtpEnddate_Enter(object sender, EventArgs e)
        {
            dtpEnddate.BackColor = m_tbcolorenter;
        }

        private void dtpEnddate_Leave(object sender, EventArgs e)
        {
            dtpEnddate.BackColor = m_tbcolorleave;
        }

        private void txtaddress1_Enter(object sender, EventArgs e)
        {
            txtaddress1.BackColor = m_tbcolorenter;
        }

        private void txtaddress1_Leave(object sender, EventArgs e)
        {
            txtaddress1.BackColor = m_tbcolorleave;
        }

        private void txtaddress2_Leave(object sender, EventArgs e)
        {
            txtaddress2.BackColor = m_tbcolorleave;
        }

        private void txtaddress2_Enter(object sender, EventArgs e)
        {
            txtaddress2.BackColor = m_tbcolorenter;
        }

        private void txtaddress3_Enter(object sender, EventArgs e)
        {
            txtaddress3.BackColor = m_tbcolorenter;
        }

        private void txtaddress3_Leave(object sender, EventArgs e)
        {
            txtaddress3.BackColor = m_tbcolorleave;
        }

        private void txtphno1_Enter(object sender, EventArgs e)
        {
            txtphno1.BackColor = m_tbcolorenter;
        }

        private void txtphno1_Leave(object sender, EventArgs e)
        {
            txtphno1.BackColor = m_tbcolorleave;
        }

        private void txtPhNo2_Enter(object sender, EventArgs e)
        {
            txtPhNo2.BackColor = m_tbcolorenter;
        }

        private void txtPhNo2_Leave(object sender, EventArgs e)
        {
            txtPhNo2.BackColor = m_tbcolorleave;
        }

        private void txtFaxNo_Leave(object sender, EventArgs e)
        {
            txtFaxNo.BackColor = m_tbcolorleave;
        }

        private void txtFaxNo_Enter(object sender, EventArgs e)
        {
            txtFaxNo.BackColor = m_tbcolorenter;
        }

        private void txtCompEmail_Enter(object sender, EventArgs e)
        {
            txtCompEmail.BackColor = m_tbcolorenter;
        }

        private void txtCompEmail_Leave(object sender, EventArgs e)
        {
            txtCompEmail.BackColor = m_tbcolorleave;
        }

        private void cmbSmsAct_Leave(object sender, EventArgs e)
        {
            cmbSmsAct.BackColor = m_tbcolorleave;
        }

        private void cmbSmsAct_Enter(object sender, EventArgs e)
        {
            cmbSmsAct.BackColor = m_tbcolorenter;
        }

        private void txtDetailUseNm_Enter(object sender, EventArgs e)
        {
            txtDetailUseNm.BackColor = m_tbcolorenter;
        }

        private void txtDetailUseNm_Leave(object sender, EventArgs e)
        {
            txtDetailUseNm.BackColor = m_tbcolorleave;
        }

        private void txtDetailPass_Enter(object sender, EventArgs e)
        {
            txtDetailPass.BackColor = m_tbcolorenter;
        }

        private void txtDetailPass_Leave(object sender, EventArgs e)
        {
            txtDetailPass.BackColor = m_tbcolorleave;
        }

        private void txtSender_Enter(object sender, EventArgs e)
        {
            txtSender.BackColor = m_tbcolorenter;
        }

        private void txtSender_Leave(object sender, EventArgs e)
        {
            txtSender.BackColor = m_tbcolorleave;
        }
        private void cmbMailAct_Leave(object sender, EventArgs e)
        {
            cmbMailAct.BackColor = m_tbcolorleave;
        }

        private void textBox16_Enter(object sender, EventArgs e)
        {
            txtmailToCust.BackColor = m_tbcolorenter;
        }

        private void textBox16_Leave(object sender, EventArgs e)
        {
            txtmailToCust.BackColor = m_tbcolorleave;
        }
        private void txtLicenses_Leave(object sender, EventArgs e)
        {
            txtLicenses.BackColor = m_tbcolorleave;
        }

        private void txtLicenDate_Enter(object sender, EventArgs e)
        {
            txtLicenDate.BackColor = m_tbcolorenter;
        }

        private void txtLicenDate_Leave(object sender, EventArgs e)
        {
            txtLicenDate.BackColor = m_tbcolorleave;
        }

        private void txtRegistrKey_Enter(object sender, EventArgs e)
        {
            txtRegistrKey.BackColor = Color.Yellow;
        }

        private void txtRegistrKey_Leave(object sender, EventArgs e)
        {
            txtRegistrKey.BackColor = m_tbcolorleave;
        }

        private void txtsupPass_Enter(object sender, EventArgs e)
        {
            txtsupPass.BackColor = Color.Yellow;
        }

        private void txtsupPass_Leave(object sender, EventArgs e)
        {
            txtsupPass.BackColor = m_tbcolorleave;
        }

        private void txtTimeForWifi_Enter(object sender, EventArgs e)
        {
            txtTimeForWifi.BackColor = Color.Yellow;
        }

        private void txtTimeForWifi_Leave(object sender, EventArgs e)
        {
            txtTimeForWifi.BackColor = Color.White;
        }

        private void txtPortNumberForWifi_Enter(object sender, EventArgs e)
        {
            txtPortNumberForWifi.BackColor = Color.Yellow;
        }

        private void txtPortNumberForWifi_Leave(object sender, EventArgs e)
        {
            txtPortNumberForWifi.BackColor = Color.White;
        }

        private void txtPasswordForWifi_Enter(object sender, EventArgs e)
        {
            txtPasswordForWifi.BackColor = Color.Yellow;
        }

        private void txtPasswordForWifi_Leave(object sender, EventArgs e)
        {
            txtPasswordForWifi.BackColor = Color.White;
        }

        private void cmbPrintGSTSerialNoYN_Enter(object sender, EventArgs e)
        {
            cmbPrintGSTSerialNoYN.BackColor = Color.Yellow;
        }

        private void cmbPrintGSTSerialNoYN_Leave(object sender, EventArgs e)
        {
            cmbPrintGSTSerialNoYN.BackColor = Color.White;
        }

        private void cmbSepeRestadirYN_Enter(object sender, EventArgs e)
        {
            cmbSepeRestadirYN.BackColor = Color.Yellow;
        }

        private void cmbSepeRestadirYN_Leave(object sender, EventArgs e)
        {
            cmbSepeRestadirYN.BackColor = Color.White;
        }

        private void txtIPAddressForWifi_Enter(object sender, EventArgs e)
        {
            txtIPAddressForWifi.BackColor = Color.Yellow;
        }

        private void txtIPAddressForWifi_Leave(object sender, EventArgs e)
        {
            txtIPAddressForWifi.BackColor = Color.White;
        }

        private void txtUserName_Enter(object sender, EventArgs e)
        {
            txtUserNameforwifi.BackColor = Color.Yellow;
        }

        private void txtUserName_Leave(object sender, EventArgs e)
        {
            txtUserNameforwifi.BackColor = Color.White;
        }

        private void txtExtraBedGrpID_Enter(object sender, EventArgs e)
        {
            txtExtraBedGrpID.BackColor = Color.Yellow;
        }

        private void txtExtraBedGrpID_Leave(object sender, EventArgs e)
        {
            txtExtraBedGrpID.BackColor = Color.White;
        }

        private void txtBackupPathName_Enter(object sender, EventArgs e)
        {
            txtBackupPathName.BackColor = Color.Yellow;
        }

        private void txtBackupPathName_Leave(object sender, EventArgs e)
        {
            txtBackupPathName.BackColor = Color.White;
        }

        private void txtServicePlace_Enter(object sender, EventArgs e)
        {
            txtServicePlace.BackColor = Color.Yellow;
        }

        private void txtServicePlace_Leave(object sender, EventArgs e)
        {
            txtServicePlace.BackColor = Color.White;
        }

        private void txtLicenses_Enter(object sender, EventArgs e)
        {
            txtLicenses.BackColor = Color.Yellow;
        }
        private void txtFooter_Enter(object sender, EventArgs e)
        {
            txtFooter.BackColor = m_tbcolorenter;
        }

        private void txtFooter_Leave(object sender, EventArgs e)
        {
            txtFooter.BackColor = m_tbcolorleave;
        }

        private void txtVattinNo_Enter(object sender, EventArgs e)
        {
            txtVattinNo.BackColor = m_tbcolorenter;
        }

        private void txtVattinNo_Leave(object sender, EventArgs e)
        {
            txtVattinNo.BackColor = m_tbcolorleave;
        }

        private void txtserviceTaxNo_Enter(object sender, EventArgs e)
        {
            txtserviceTaxNo.BackColor = m_tbcolorenter;
        }

        private void txtserviceTaxNo_Leave(object sender, EventArgs e)
        {
            txtserviceTaxNo.BackColor = m_tbcolorleave;
        }

        private void txtCstNo_Enter(object sender, EventArgs e)
        {
            txtCstNo.BackColor = m_tbcolorenter;
        }

        private void txtCstNo_Leave(object sender, EventArgs e)
        {
            txtCstNo.BackColor = m_tbcolorleave;
        }

        private void txtLuxTaxNo_Enter(object sender, EventArgs e)
        {
            txtLuxTaxNo.BackColor = m_tbcolorenter;
        }

        private void txtLuxTaxNo_Leave(object sender, EventArgs e)
        {
            txtLuxTaxNo.BackColor = m_tbcolorleave;
        }

        private void txtPanNo_Enter(object sender, EventArgs e)
        {
            txtPanNo.BackColor = m_tbcolorenter;
        }

        private void txtPanNo_Leave(object sender, EventArgs e)
        {
            txtPanNo.BackColor = m_tbcolorleave;
        }

        private void txtDataBasNam_Enter(object sender, EventArgs e)
        {
            txtDataBasNam.BackColor = m_tbcolorenter;
        }

        private void txtDataBasNam_Leave(object sender, EventArgs e)
        {
            txtDataBasNam.BackColor = m_tbcolorleave;
        }
        private void txtGstSeialNo_Enter(object sender, EventArgs e)
        {
            txtGstSeialNo.BackColor = Color.Yellow;
        }

        private void txtGstSeialNo_Leave(object sender, EventArgs e)
        {
            txtGstSeialNo.BackColor = Color.White;
        }
        private void txtGstNo_Enter(object sender, EventArgs e)
        {
            txtGstNo.BackColor = m_tbcolorenter;
        }

        private void txtGstNo_Leave(object sender, EventArgs e)
        {
            txtGstNo.BackColor = m_tbcolorleave;
        }

        private void txtsacNo_Enter(object sender, EventArgs e)
        {
            txtsacNo.BackColor = m_tbcolorenter;
        }

        private void txtsacNo_Leave(object sender, EventArgs e)
        {
            txtsacNo.BackColor = m_tbcolorleave;
        }

        private void txtBankDetail_Enter(object sender, EventArgs e)
        {
            txtBankDetail.BackColor = m_tbcolorenter;
        }

        private void txtBankDetail_Leave(object sender, EventArgs e)
        {
            txtBankDetail.BackColor = m_tbcolorleave;
        }

        private void txtBillHeading_Enter(object sender, EventArgs e)
        {
            txtBillHeading.BackColor = m_tbcolorenter;
        }

        private void txtBillHeading_Leave(object sender, EventArgs e)
        {
            txtBillHeading.BackColor = m_tbcolorleave;
        }

        private void txtSpecMsg_Enter(object sender, EventArgs e)
        {
            txtSpecMsg.BackColor = m_tbcolorenter;
        }

        private void txtSpecMsg_Leave(object sender, EventArgs e)
        {
            txtSpecMsg.BackColor = m_tbcolorleave;
        }

        private void cmbComNmOnBil_Enter(object sender, EventArgs e)
        {
            cmbComNmOnBil.BackColor = m_tbcolorenter;
        }

        private void cmbComNmOnBil_Leave(object sender, EventArgs e)
        {
            cmbComNmOnBil.BackColor = m_tbcolorleave;
        }

        private void cmbAddres_Enter(object sender, EventArgs e)
        {
            cmbAddres.BackColor = m_tbcolorenter;
        }

        private void cmbAddres_Leave(object sender, EventArgs e)
        {
            cmbAddres.BackColor = m_tbcolorleave;
        }

        private void cmbActPrinAfterSaving_Enter(object sender, EventArgs e)
        {
            cmbActPrinAfterSaving.BackColor = m_tbcolorenter;
        }

        private void cmbActPrinAfterSaving_Leave(object sender, EventArgs e)
        {
            cmbActPrinAfterSaving.BackColor = m_tbcolorleave;
        }

        private void cmbPrintBil_Enter(object sender, EventArgs e)
        {
            cmbPrintBil.BackColor = m_tbcolorenter;
        }

        private void cmbPrintBil_Leave(object sender, EventArgs e)
        {
            cmbPrintBil.BackColor = m_tbcolorleave;
        }

        private void cmbPrintNoPersOnBil_Enter(object sender, EventArgs e)
        {
            cmbPrintNoPersOnBil.BackColor = m_tbcolorenter;
        }

        private void cmbPrintNoPersOnBil_Leave(object sender, EventArgs e)
        {
            cmbPrintNoPersOnBil.BackColor = m_tbcolorleave;
        }

        private void cmbPrintDetailbil_Enter(object sender, EventArgs e)
        {
            cmbPrintDetailbil.BackColor = m_tbcolorenter;
        }

        private void cmbTimOnBil_Enter(object sender, EventArgs e)
        {
            cmbTimOnBil.BackColor = m_tbcolorenter;
        }

        private void cmbPrintDetailbil_Leave(object sender, EventArgs e)
        {
            cmbPrintDetailbil.BackColor = m_tbcolorleave;
        }

        private void cmbTimOnBil_Leave(object sender, EventArgs e)
        {
            cmbTimOnBil.BackColor = m_tbcolorleave;
        }

        private void cmbAllowDelay_Enter(object sender, EventArgs e)
        {
            cmbAllowDelay.BackColor = m_tbcolorenter;
        }

        private void cmbAllowDelay_Leave(object sender, EventArgs e)
        {
            cmbAllowDelay.BackColor = m_tbcolorleave;
        }

        private void txtMinLengOfbil_Enter(object sender, EventArgs e)
        {
            txtMinLengOfbil.BackColor = m_tbcolorenter;
        }

        private void txtMinLengOfbil_Leave(object sender, EventArgs e)
        {
            txtMinLengOfbil.BackColor = m_tbcolorleave;
        }

        private void NoOfLinesForKot_Enter(object sender, EventArgs e)
        {
            NoOfLinesForKot.BackColor = m_tbcolorenter;
        }

        private void NoOfLinesForKot_Leave(object sender, EventArgs e)
        {
            NoOfLinesForKot.BackColor = m_tbcolorleave;
        }

        private void cmbAllowShar_Enter(object sender, EventArgs e)
        {
            cmbAllowShar.BackColor = m_tbcolorenter;
        }

        private void cmbAllowShar_Leave(object sender, EventArgs e)
        {
            cmbAllowShar.BackColor = m_tbcolorleave;
        }

        private void txtServTax_Enter(object sender, EventArgs e)
        {
            txtServTax.BackColor = m_tbcolorenter;
        }

        private void txtServTax_Leave(object sender, EventArgs e)
        {
            txtServTax.BackColor = m_tbcolorleave;
        }

        private void cmbReprint_Enter(object sender, EventArgs e)
        {
            cmbReprint.BackColor = m_tbcolorenter;
        }

        private void cmbReprint_Leave(object sender, EventArgs e)
        {
            cmbReprint.BackColor = m_tbcolorleave;
        }

        private void cmbKOTCanc_Enter(object sender, EventArgs e)
        {
            cmbKOTCanc.BackColor = m_tbcolorenter;
        }

        private void cmbKOTCanc_Leave(object sender, EventArgs e)
        {
            cmbKOTCanc.BackColor = m_tbcolorleave;
        }

        private void txtNumLinForward_Enter(object sender, EventArgs e)
        {
            txtNumLinForward.BackColor = m_tbcolorenter;
        }

        private void txtNumLinForward_Leave(object sender, EventArgs e)
        {
            txtNumLinForward.BackColor = m_tbcolorleave;
        }

        private void txtNumOfLinBackwrd_Enter(object sender, EventArgs e)
        {
            txtNumOfLinBackwrd.BackColor = m_tbcolorenter;
        }

        private void txtNumOfLinBackwrd_Leave(object sender, EventArgs e)
        {
            txtNumOfLinBackwrd.BackColor = m_tbcolorleave;
        }

        private void cmbMailAct_Enter(object sender, EventArgs e)
        {
            cmbMailAct.BackColor = m_tbcolorenter;
        }

        private void cmbMailAct_Leave_1(object sender, EventArgs e)
        {
            cmbMailAct.BackColor = m_tbcolorleave;
        }

        private void txtmailToCust_Enter(object sender, EventArgs e)
        {
            txtmailToCust.BackColor = m_tbcolorenter;
        }

        private void txtmailToCust_Leave(object sender, EventArgs e)
        {
            txtmailToCust.BackColor = m_tbcolorleave;
        }

        private void txtCustPass_Enter(object sender, EventArgs e)
        {
            txtCustPass.BackColor = m_tbcolorenter;
        }

        private void txtCustPass_Leave(object sender, EventArgs e)
        {
            txtCustPass.BackColor = m_tbcolorleave;
        }

        private void txtMailhead_Enter(object sender, EventArgs e)
        {
            txtMailhead.BackColor = m_tbcolorenter;
        }

        private void txtMailhead_Leave(object sender, EventArgs e)
        {
            txtMailhead.BackColor = m_tbcolorleave;
        }

        private void txtMailbody_Enter(object sender, EventArgs e)
        {
            txtMailbody.BackColor = m_tbcolorenter;
        }

        private void txtMailbody_Leave(object sender, EventArgs e)
        {
            txtMailbody.BackColor = m_tbcolorleave;
        }

        private void cmbMailGust_Enter_1(object sender, EventArgs e)
        {
            cmbMailGust.BackColor = m_tbcolorenter;
        }


        private void cmbMailGust_Leave(object sender, EventArgs e)
        {
            cmbMailGust.BackColor = m_tbcolorleave;
        }

        private void cmbsendMailComp_Enter(object sender, EventArgs e)
        {
            cmbsendMailComp.BackColor = m_tbcolorenter;
        }

        private void cmbsendMailComp_Leave(object sender, EventArgs e)
        {
            cmbsendMailComp.BackColor = m_tbcolorleave;
        }

        private void cmbCompanyLogoYN_Enter(object sender, EventArgs e)
        {
            cmbCompanyLogoYN.BackColor = Color.Yellow;
        }

        private void cmbCompanyLogoYN_Leave(object sender, EventArgs e)
        {
            cmbCompanyLogoYN.BackColor = Color.White;
        }
        private void cmbSaleRefNum_Enter(object sender, EventArgs e)
        {
            cmbSaleRefNum.BackColor = m_tbcolorenter;
        }

        private void cmbSaleRefNum_Leave(object sender, EventArgs e)
        {
            cmbSaleRefNum.BackColor = m_tbcolorleave;
        }

        private void txtmobile1_Enter(object sender, EventArgs e)
        {
            txtmobile1.BackColor = m_tbcolorenter;
        }

        private void txtmobile1_Leave(object sender, EventArgs e)
        {
            txtmobile1.BackColor = m_tbcolorleave;
        }

        private void cmbPrintRoomTarif_Enter(object sender, EventArgs e)
        {
            cmbPrintRoomTarif.BackColor = m_tbcolorenter;
        }

        private void cmbPrintRoomTarif_Leave(object sender, EventArgs e)
        {
            cmbPrintRoomTarif.BackColor = m_tbcolorleave;
        }

        private void txtMobile2_Enter(object sender, EventArgs e)
        {
            txtMobile2.BackColor = m_tbcolorenter;
        }

        private void txtMobile2_Leave(object sender, EventArgs e)
        {
            txtMobile2.BackColor = m_tbcolorleave;
        }

        private void cmbPerHedTarif_Enter(object sender, EventArgs e)
        {
            cmbPerHedTarif.BackColor = m_tbcolorenter;
        }

        private void cmbPerHedTarif_Leave(object sender, EventArgs e)
        {
            cmbPerHedTarif.BackColor = m_tbcolorleave;
        }

        private void txtMobile3_Enter(object sender, EventArgs e)
        {
            txtMobile3.BackColor = m_tbcolorenter;
        }

        private void txtMobile3_Leave(object sender, EventArgs e)
        {
            txtMobile3.BackColor = m_tbcolorleave;
        }

        private void cmbMultRomTerf_Enter(object sender, EventArgs e)
        {
            cmbMultRomTerf.BackColor = m_tbcolorenter;
        }

        private void cmbMultRomTerf_Leave(object sender, EventArgs e)
        {
            cmbMultRomTerf.BackColor = m_tbcolorleave;
        }

        private void txtTotlGlcod_Enter(object sender, EventArgs e)
        {
            txtTotlGlcod.BackColor = m_tbcolorenter;
        }

        private void txtTotlGlcod_Leave(object sender, EventArgs e)
        {
            txtTotlGlcod.BackColor = m_tbcolorleave;
        }

        private void cmbTaxAftrDisc_Enter(object sender, EventArgs e)
        {
            cmbTaxAftrDisc.BackColor = m_tbcolorenter;
        }

        private void cmbTaxAftrDisc_Leave(object sender, EventArgs e)
        {
            cmbTaxAftrDisc.BackColor = m_tbcolorleave;
        }

        private void txtExtraPersn_Enter(object sender, EventArgs e)
        {
            txtExtraPersn.BackColor = m_tbcolorenter;
        }

        private void txtExtraPersn_Leave(object sender, EventArgs e)
        {
            txtExtraPersn.BackColor = m_tbcolorleave;
        }

        private void txtCheckOuttim_Enter(object sender, EventArgs e)
        {
            txtCheckOuttim.BackColor = m_tbcolorenter;
        }

        private void txtCheckOuttim_Leave(object sender, EventArgs e)
        {
            txtCheckOuttim.BackColor = m_tbcolorleave;
        }

        private void txtRoomService_Enter(object sender, EventArgs e)
        {
            txtRoomService.BackColor = m_tbcolorenter;
        }

        private void txtRoomService_Leave(object sender, EventArgs e)
        {
            txtRoomService.BackColor = m_tbcolorleave;
        }
        #endregion

        #region add company
        private void btnNewComp_Click(object sender, EventArgs e)
        {
            AddCompany();
        }
        public void AddCompany()
        {
            try
            {
                opmode = "A";
                BtnSave.Enabled = true;
                btnNewComp.Enabled = false;
                BtnNewFinYear.Enabled = false;
                BtnExit.Enabled = true;
                btnStartBackup.Enabled = false;
                grpboxSupPass.Visible = true;
                //Find Max Company Code
                NFY = false;
                txtDataBasNam.Enabled = true;
                controlEnableTrue(tabCompanydetail.TabPages[0].Controls);
                controlEnableTrue(tabCompanydetail.TabPages[1].Controls);
                string StartDate = "04/01/" + DateTime.Now.ToString("yy");
                dtpStartdate.Value = Convert.ToDateTime(StartDate);
                DateTime ADDYer = Convert.ToDateTime(StartDate).AddYears(1);
                //Substract 1 in days
                ADDYer = ADDYer.AddDays(-1);
                dtpEnddate.Value = ADDYer;
                //Focus Detail tab
                tabCompanydetail.SelectTab(tabDetail);
                //Fill Combo Select Tab
                CmbFill(tabCompanydetail.TabPages[0].Controls);
                txtCompNm.Focus();
                //Clear Tab Control
                mod.txtclear(tabCompanydetail.TabPages[0].Controls);
                mod.txtclear(tabCompanydetail.TabPages[1].Controls);
                mod.txtclear(tabCompanydetail.TabPages[2].Controls);
                mod.txtclear(tabCompanydetail.TabPages[3].Controls);
                mod.txtclear(tabCompanydetail.TabPages[4].Controls);
                mod.txtclear(tabCompanydetail.TabPages[5].Controls);
                MaxID();
               
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }
        public void MaxID()
        {
            try
            {
                cmd = new SqlCommand("SELECT (ISNULL(MAX(Company_Code),0)) FROM tbCompanyDetailsLodge", ASPLCSS.connect());
                cmd.ExecuteNonQuery();
                string maxCode = Convert.ToString(cmd.ExecuteScalar());
                if (maxCode != "0")
                {
                     int c=(Convert.ToInt32(maxCode)) + 1;
                    txtCompCode.Text =c.ToString();
                       
                }
                else
                {
                    txtCompCode.Text = "1000";
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }
        #endregion

        #region fill conbobox
        public void CmbFill(Control.ControlCollection ctrls)
        {
            try
            {
                foreach (Control cntrl in ctrls)
                {
                    if (cntrl is ComboBox)
                    {
                        ((ComboBox)cntrl).Items.Clear();
                         ((ComboBox)cntrl).Items.Insert(0, "SELECT");
                        ((ComboBox)cntrl).Items.Insert(1, "Yes");
                        ((ComboBox)cntrl).Items.Insert(2, "No");
                        if (detail_id != 1)
                        {
                            ((ComboBox)cntrl).SelectedIndex = 0;
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
        #endregion

        #region financial year
        public void NewFinYear()
        {
            try
            {
                NFY = true;
                string EndDate = "0", StartDate;
                btnStartBackup.Enabled = false;
                BtnNewFinYear.Enabled = false;
                grpboxSupPass.Visible = true;
                BtnSave.Enabled = false;
                SetButtonBefSave();
                controlEnableTrue(tabCompanydetail.TabPages[0].Controls);
                controlEnableTrue(tabCompanydetail.TabPages[1].Controls);
                cmd = new SqlCommand("SELECT * FROM tbYear WHERE Financial_End_Year=(SELECT MAX(Financial_End_Year)FROM tbYear WHERE Company_Code='" + Globalvariable.bcode + "')", con.connect());
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    EndDate = dr["Financial_End_Year"].ToString();
                }
                dr.Close();
                //Show Start Date
                StartDate = "04/01/" + EndDate;
                dtpStartdate.Value = Convert.ToDateTime(StartDate);
                //Show End Date
                DateTime ADDYer = Convert.ToDateTime(StartDate);
                //Add 1 in Year
                ADDYer = ADDYer.AddYears(1);
                //Substract 1 in days
                ADDYer = ADDYer.AddDays(-1);
                dtpEnddate.Value = ADDYer;
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }
        #endregion

        private Image GetCopyImage(string path)
        {
            using (Image im = Image.FromFile(path))
            {
                Bitmap bm = new Bitmap(im);
                return bm;
            }
        }

        #region Fill details
        public void FillDetails()
        {
            try
            {
                cmd = new SqlCommand("SELECT * FROM tbCompanyDetailsLodge WHERE Company_Code=" + Globalvariable.bcode + "", con.connect());
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        detail_id = 1;
                        //Fill Detail tab
                        txtCompCode.Text = dr["Company_Code"].ToString();
                        txtCompNm.Text = mod.Isnull(dr["Company_Name"].ToString(), "-");
                        textCompNm2.Text = mod.Isnull(dr["Branch_Name"].ToString(), "-");
                        // Fill Logo in picturebox 
                        PicBoxLogo.Image = null;
                        PicBoxLogo.Invalidate();
                        string path = Application.StartupPath + @"\images";

                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        foreach (string file in Directory.GetFiles(path))
                        {
                            File.Delete(file);
                        }
                        if (dr["LogoName"].ToString() != "" && dr["Company_Logo"].ToString() != "")
                        {
                            string sPathToSaveFileTo = path + "\\" + dr["LogoName"];
                            ImageName = dr["LogoName"].ToString();
                            // read in using GetValue and cast to byte array                     
                            byte[] fileData = (byte[])dr["Company_Logo"];
                            Image im;
                            using (FileStream fs = new FileStream(sPathToSaveFileTo, FileMode.Create, FileAccess.Write))
                            {
                                fs.Write(fileData, 0, fileData.Length);
                                fs.Flush();
                                fs.Close();
                                fs.Dispose();
                                im = GetCopyImage(sPathToSaveFileTo);
                                PicBoxLogo.Image = im;
                                PicBoxLogo.Refresh();
                            }
                        }
                        txtLogoPath.Text = mod.Isnull(dr["LogoFileName"].ToString(), "");
                        //Fill textbox
                        txtaddress1.Text = mod.Isnull(dr["Address1"].ToString(), "-");
                        txtaddress2.Text = mod.Isnull(dr["Address2"].ToString(), "-");
                        txtaddress3.Text = mod.Isnull(dr["Address3"].ToString(), "-");
                        txtphno1.Text = mod.Isnull(dr["PhoneNo1"].ToString(), "0");
                        txtPhNo2.Text = mod.Isnull(dr["PhoneNo2"].ToString(), "0");
                        txtFaxNo.Text = mod.Isnull(dr["FaxNo"].ToString(), "0");
                        txtCompEmail.Text = mod.Isnull(dr["EMailId"].ToString(), "");
                        txtDetailUseNm.Text = mod.Isnull(dr["SmsUserName"].ToString(), "-");
                        txtDetailPass.Text = mod.Isnull(dr["SmsPassword"].ToString(), "-");
                        txtSender.Text = mod.Isnull(dr["SmsSender"].ToString(), "-");
                        txtRecivEmail1.Text = mod.Isnull(dr["ReceiverEmail1"].ToString(), "");
                        txtReceivEmailId2.Text = mod.Isnull(dr["ReceiverEmail2"].ToString(), "");
                        txtLicenses.Text = mod.Isnull(dr["Licenses_No"].ToString(), "-");
                        DateTime licendate = Convert.ToDateTime(dr["Licenses_DT"]);
                        txtLicenDate.Text = licendate.ToString("dd/MM/yyyy");
                        txtRegistrKey.Text = mod.Isnull(dr["RegistrationKey"].ToString(), "0");
                        //Fill Excize/Other Info tab
                        txtVattinNo.Text = mod.Isnull(dr["VATtinNo"].ToString(), "0");
                        txtserviceTaxNo.Text = mod.Isnull(dr["ServiceTaxNo"].ToString(), "0");
                        txtCstNo.Text = mod.Isnull(dr["CST_No"].ToString(), "0");
                        txtLuxTaxNo.Text = mod.Isnull(dr["LuxuryTaxNo"].ToString(), "0");
                        txtPanNo.Text = mod.Isnull(dr["PanNo"].ToString(), "0");
                        txtDataBasNam.Text = mod.Isnull(dr["DataBase_Nm"].ToString(), "-");
                        if (dr["DataBase_Nm"] != "")
                        {
                            txtDataBasNam.Enabled = false;
                        }
                        else
                        {
                            txtDataBasNam.Enabled = true;
                        }
                        txtGstNo.Text = mod.Isnull(dr["GSTNo"].ToString(), "0");
                        txtsacNo.Text = mod.Isnull(dr["SACNo"].ToString(), "0");
                        txtBankDetail.Text = mod.Isnull(dr["BankDetail"].ToString(), "-");
                        //Fill Special Info tab
                        txtBillHeading.Text = mod.Isnull(dr["BilHeading"].ToString(), "-");
                        txtSpecMsg.Text = mod.Isnull(dr["SpecialMsg"].ToString(), "-");
                        txtMinLengOfbil.Text = mod.Isnull(dr["PageLengthBil"].ToString(), "0");
                        NoOfLinesForKot.Text = mod.Isnull(dr["NoOfLinKOT"].ToString(), "0");
                        txtserviceTaxNo.Text = mod.Isnull(dr["ServiceTaxPer"].ToString(), "0");
                        txtNumLinForward.Text = mod.Isnull(dr["NoOfLineForward"].ToString(), "0");
                        txtNumOfLinBackwrd.Text = mod.Isnull(dr["NoOfLineBackWard"].ToString(), "0");
                        //Fill Email tab
                        txtmailToCust.Text = mod.Isnull(dr["MailID"].ToString(), "");
                        txtCustPass.Text = mod.Isnull(dr["MailPassword"].ToString(), "");
                        txtMailhead.Text = mod.Isnull(dr["MailHead"].ToString(), "-");
                        txtMailbody.Text = mod.Isnull(dr["MailBody"].ToString(), "-");
                        //Fill Parameter tab
                        txtmobile1.Text = mod.Isnull(dr["MobileNo1"].ToString(), "0");
                        txtMobile2.Text = mod.Isnull(dr["MobileNo2"].ToString(), "0");
                        txtMobile3.Text = mod.Isnull(dr["MobileNo3"].ToString(), "0");
                        txtTotlGlcod.Text = mod.Isnull(dr["TotalDiscGLCode"].ToString(), "0");
                        txtExtraPersn.Text = mod.Isnull(dr["ExtraPerson"].ToString(), "0");
                        txtRoomService.Text = mod.Isnull(dr["RoomService"].ToString(), "0");
                        txtRestSale.Text = mod.Isnull(dr["Restaurantsale"].ToString(), "0");
                        txtTallyVoucherTyp.Text = mod.Isnull(dr["TallyVoucherTyp"].ToString(), "");
                        txtLuxTaxMastCod.Text = mod.Isnull(dr["LuxTaxmastCod"].ToString(), "0");
                        txtServTaxMastCod.Text = mod.Isnull(dr["ServiceTaxmastCode"].ToString(), "0");
                        txtCallRecordFilPath.Text = mod.Isnull(dr["CallRecordFilepath"].ToString(), "0");
                        txtPhonCalGropID.Text = mod.Isnull(dr["PhonCalGrpID"].ToString(), "0");
                        txtPhonCalItmId.Text = mod.Isnull(dr["PhonCalItemID"].ToString(), "0");
                        txtDisTalCod.Text = mod.Isnull(dr["TotalDiscTalyCod"].ToString(), "0");
                        txtCheckOuttim.Text = (mod.Isnull(dr["CheckOutTim"].ToString(), "12:00:00 PM"));
                        txtRomservDirPer.Text = mod.Isnull(dr["RoomServiDirectPer"].ToString(), "0");
                        txtDefaltLegId.Text = mod.Isnull(dr["DefaultLegID"].ToString(), "0");
                        txtGuestID.Text = mod.Isnull(dr["DefaultGuestID"].ToString(), "0");
                        dateTimePicker2.Value = Convert.ToDateTime(mod.Isnull(dr["CheckInTime"].ToString(), "10:00:00 AM"));
                        txtTimeForWifi.Text = mod.Isnull(dr["TimeForWifi"].ToString(), "");
                        txtIPAddressForWifi.Text = mod.Isnull(dr["IPAddressForWifi"].ToString(), "");
                        txtPortNumberForWifi.Text = mod.Isnull(dr["PortNoForWiFi"].ToString(), "");
                        txtExtraBedGrpID.Text = mod.Isnull(dr["ExtraBedGrpID"].ToString(), "");
                        txtBackupPathName.Text = mod.Isnull(dr["BackUpPathName"].ToString(), "");
                        txtServicePlace.Text = mod.Isnull(dr["ServicePlace"].ToString(), "");
                        //Fill Combobox Detail tab
                        FilCom = Convert.ToString(dr["SMSActiv"]);
                        FillDetailCombo(cmbSmsAct, FilCom);
                        //Fill Combobox Special Info tab     
                        if (tabCompanydetail.SelectedIndex == 2)
                        {
                            PrintComNmBil = Convert.ToString(dr["PrintComNameBil"]);
                            FillDetailCombo(cmbComNmOnBil, PrintComNmBil); //Fill Combobox   
                            PrintAdd = Convert.ToString(dr["PrintAddressYN"]);
                            FillDetailCombo(cmbAddres, PrintAdd);//Fill Combobox
                            PrintAfteSavKOT = Convert.ToString(dr["PrintAftrSaviKOT"]);
                            FillDetailCombo(cmbActPrinAfterSaving, PrintAfteSavKOT);//Fill Combobox
                            prinBilYN = Convert.ToString(dr["PrintBilYN"]);
                            FillDetailCombo(cmbPrintBil, prinBilYN); //Fill Combobox
                            PrintNoPerBil = Convert.ToString(dr["PrintNoOfPersBil"]);
                            FillDetailCombo(cmbPrintNoPersOnBil, PrintNoPerBil);//Fill Combobox 
                            PriDetaiBil = Convert.ToString(dr["PrintDetailBil"]);
                            FillDetailCombo(cmbPrintDetailbil, PriDetaiBil);//Fill Combobox 
                            TimONbil = Convert.ToString(dr["TimeOnBilYN"]);
                            FillDetailCombo(cmbTimOnBil, TimONbil);//Fill Combobox 
                            allowDelay = Convert.ToString(dr["AllowDelay"]);
                            FillDetailCombo(cmbAllowDelay, allowDelay);//Fill Combobox 
                            allowshaYN = Convert.ToString(dr["AllowSharingYN"]);
                            FillDetailCombo(cmbAllowShar, allowshaYN);//Fill Combobox
                            ReprintYN = Convert.ToString(dr["ReprintYN"]);
                            FillDetailCombo(cmbReprint, ReprintYN);//Fill Combobox
                            KotCancel = Convert.ToString(dr["KOTCancel"]);
                            FillDetailCombo(cmbKOTCanc, KotCancel);//Fill Combobox
                        }
                        else if (tabCompanydetail.SelectedIndex == 3)
                        {
                            //Fill Combobox Email tab       
                            MailAct = Convert.ToString(dr["MailActYN"]);
                            FillDetailCombo(cmbMailAct, MailAct); //Fill Combobox   
                            SendMailGuest = Convert.ToString(dr["SendMailGuestYN"]);
                            FillDetailCombo(cmbMailGust, SendMailGuest);//Fill Combobox
                            SendmailCom = Convert.ToString(dr["SendMailComYN"]);
                            FillDetailCombo(cmbsendMailComp, SendmailCom);//Fill Combobox
                        }
                        else if (tabCompanydetail.SelectedIndex == 4)
                        {
                            //Fill Combobox Parameters tab      
                            saleRefNo = Convert.ToString(dr["SaleRefNoYN"]);
                            FillDetailCombo(cmbSaleRefNum, saleRefNo); //Fill Combobox
                            PrintRoomTerif = Convert.ToString(dr["PrintRomTerifBilYN"]);
                            FillDetailCombo(cmbPrintRoomTarif, PrintRoomTerif);//Fill Combobox
                            PerHeadTerif = Convert.ToString(dr["PerHeadTerifBilYN"]);
                            FillDetailCombo(cmbPerHedTarif, PerHeadTerif); //Fill Combobox
                            multiromterif = Convert.ToString(dr["MultiRomTerifTotaPax"]);
                            FillDetailCombo(cmbMultRomTerf, multiromterif);//Fill Combobox 
                            taxAfterdic = Convert.ToString(dr["TaxAfterDiscYN"]);
                            FillDetailCombo(cmbTaxAftrDisc, taxAfterdic);//Fill Combobox 
                            BillSetle = Convert.ToString(dr["BillSettleMentYN"]);
                            FillDetailCombo(cmbBilSetle, BillSetle);//Fill Combobox 
                            Printsalecry = Convert.ToString(dr["PrintsaleCrystReportYN"]);
                            FillDetailCombo(cmbPrintsaleCrystreport, Printsalecry);//Fill Combobox 
                            AutoRefNo = Convert.ToString(dr["AutoRefNoYN"]);
                            FillDetailCombo(cmbautoRefNo, AutoRefNo);//Fill Combobox
                            DetailBilrep = Convert.ToString(dr["DetailBilreportYN"]);
                            FillDetailCombo(cmbDetailBilReport, DetailBilrep);//Fill Combobox
                            seperatebilNo = Convert.ToString(dr["SeperateBilNoYN"]);
                            FillDetailCombo(cmbSeprteBilNo, seperatebilNo);//Fill Combobox
                            capilaryFile = Convert.ToString(dr["CapillaryfileYN"]);
                            FillDetailCombo(cmbCapilFile, capilaryFile);//Fill Combobox
                            lockingsystem = Convert.ToString(dr["LockingSystemYN"]);
                            FillDetailCombo(cmbLockSystm, lockingsystem);//Fill Combobox
                        }
                        else if (tabCompanydetail.SelectedIndex == 5)
                        {
                            //Fill Combobox Parameters tab      
                            printGSTserialYN = Convert.ToString(dr["PrintGSTSerialNoYN"]);
                            FillDetailCombo(cmbPrintGSTSerialNoYN, printGSTserialYN); //Fill Combobox
                            seperateRestDirectYN = Convert.ToString(dr["SeperRestDirectYN"]);
                            FillDetailCombo(cmbSepeRestadirYN, seperateRestDirectYN);//Fill Combobox
                            printcompanylogoYN = Convert.ToString(dr["PrintCompanyLogoYN"]);
                            FillDetailCombo(cmbCompanyLogoYN, printcompanylogoYN);//Fill Combobox
                        }
                    }
                    detail_id = 0;
                    string EndDate = "0", StartDate;
                    cmd = new SqlCommand("SELECT * FROM tbYear WHERE Company_Code='" + Globalvariable.bcode + "'", con.connect());
                    SqlDataReader dr1 = cmd.ExecuteReader();
                    //   string st = "SELECT * FROM tbYear WHERE Financial_End_Year=(SELECT MAX(Financial_End_Year)FROM tbYear WHERE Company_Code='" + Globalvariable.bcode + "')";
                    while (dr1.Read())
                    {
                        EndDate = dr1["Financial_End_Year"].ToString();
                    }
                    StartDate = "04/01/" + EndDate;
                    dtpStartdate.Value = Convert.ToDateTime(StartDate);
                    //Show End Date
                    DateTime ADDYer = Convert.ToDateTime(StartDate);
                    //Add 1 in Year
                    ADDYer = ADDYer.AddYears(1);
                    //Substract 1 in days
                    ADDYer = ADDYer.AddDays(-1);
                    dtpEnddate.Value = ADDYer;
                }
                else
                {
                    BtnNewFinYear.Enabled = false;
                    BtnSave.Enabled = false;
                    BtnUpdate.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }
        #endregion

        #region Fill combobox after select query
        public void FillDetailCombo(Control cntrl, string CharYN)
        {
            try
            {
                if (cntrl is ComboBox)
                {
                    if (CharYN != " ")
                    {
                        if (CharYN == "Y")
                        {
                            ((ComboBox)cntrl).Items.Insert(0, "Yes");
                        }
                        else if (CharYN == "N")
                        {
                            ((ComboBox)cntrl).Items.Insert(0, "No");
                        }
                         ((ComboBox)cntrl).SelectedIndex = 0;
                    }
                    ((ComboBox)cntrl).Items.Clear();
                     ((ComboBox)cntrl).Items.Insert(0, "SELECT");
                    ((ComboBox)cntrl).Items.Insert(1, "Yes");
                    ((ComboBox)cntrl).Items.Insert(2, "No");
                    if (detail_id != 1)
                    {
                        ((ComboBox)cntrl).SelectedIndex = 0;
                    }
                    else if (detail_id == 1 && CharYN == "")
                    //else if (detail_id == 1)
                    {
                        ((ComboBox)cntrl).SelectedIndex = 0;
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

        #region tab event and functions
        private void tabCompanydetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (opmode == "A")
                {
                    if (tabCompanydetail.SelectedIndex == 0)
                    {
                        txtCompNm.Focus();
                        CmbFill(this.tabCompanydetail.TabPages[0].Controls);
                    }
                    else if (tabCompanydetail.SelectedIndex == 1)
                    {
                        txtVattinNo.Focus();
                        CmbFill(this.tabCompanydetail.TabPages[1].Controls);
                    }
                    else if (tabCompanydetail.SelectedIndex == 2)
                    {
                            txtBillHeading.Focus();
                            CmbFill(this.tabCompanydetail.TabPages[2].Controls);                     
                    }
                    else if (tabCompanydetail.SelectedIndex == 3)
                    {
                            cmbMailAct.Focus();
                            CmbFill(this.tabCompanydetail.TabPages[3].Controls);                       
                    }
                    else if (tabCompanydetail.SelectedIndex == 4)
                    {                        
                            cmbSaleRefNum.Focus();
                            CmbFill(this.tabCompanydetail.TabPages[4].Controls);
                     }
                    else if (tabCompanydetail.SelectedIndex == 5)
                    {
                            txtTimeForWifi.Focus();
                           CmbFill(this.tabCompanydetail.TabPages[5].Controls);                                      
                    }
                }
                else
                {
                    cmd = new SqlCommand("SELECT * FROM tbCompanyDetailsLodge WHERE Company_Code=" + Globalvariable.bcode + "", con.connect());
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (tabCompanydetail.SelectedIndex == 0)
                    {
                        txtCompNm.Focus();
                        FillDetails();
                    }
                    else if (tabCompanydetail.SelectedIndex == 1)
                    {
                        txtVattinNo.Focus();
                        FillDetails();                      
                    }
                    else if (tabCompanydetail.SelectedIndex == 2)
                    {
                        if (dr.HasRows)
                        {
                            txtBillHeading.Focus();
                            FillDetails();                          
                        }
                        else
                        {
                            txtBillHeading.Focus();                         
                        }
                    }
                    else if (tabCompanydetail.SelectedIndex == 3)
                    {
                        if (dr.HasRows)
                        {
                            cmbMailAct.Focus();
                            FillDetails();                           
                        }
                        else
                        {
                            cmbMailAct.Focus();                           
                        }
                    }
                    else if (tabCompanydetail.SelectedIndex == 4)
                    {
                        if (dr.HasRows)
                        {
                            cmbSaleRefNum.Focus();
                            FillDetails();                            
                        }
                        else
                        {
                            cmbSaleRefNum.Focus();                           
                        }
                    }
                    else if (tabCompanydetail.SelectedIndex == 5)
                    {
                        if (dr.HasRows)
                        {
                            cmbSaleRefNum.Focus();
                            FillDetails();                         
                        }
                        else
                        {
                            cmbSaleRefNum.Focus();                       
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

    
        #endregion

        #region buttons event and functions
        private void BtnExit_Click(object sender, EventArgs e)
        {
            try
            {
                if (BtnExit.Visible == true)
                {
                    if (Globalvariable.frmName != "UtilityComDetail")
                    {
                        //Load Frm Company Select
                        FrmCompanySelect frmcomSelect = new FrmCompanySelect();
                        frmcomSelect.LOAD();
                        if(Globalvariable.usercd==null)
                        {
                            MAINFORM frmmain = new MAINFORM();
                            this.Close();
                            frmmain.Show();
                        }
                        this.Close();
                    }
                    
                }
                else if (btnCancel.Visible == true)
                {
                    opmode = "L";
                    BtnNewFinYear.Enabled = true;
                    btnStartBackup.Enabled = true;
                    btnNewComp.Enabled = true;
                    BtnUpdate.Enabled = true;
                    BtnSave.Enabled = false;
                    BtnExit.Enabled = true;
                    FillDetails();
                    grpboxSupPass.Visible = false;
                }
                Close();
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {

            try
            {
                using (OpenFileDialog dlg = new OpenFileDialog())
                {
                    dlg.Title = "";
                    dlg.Filter = "bmp files (*.bmp)|*.bmp|JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif|PNG Image|*.png";
                    //(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif;
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        // Create a new Bitmap object from the picture file on disk,
                        // and assign that to the PictureBox.Image property
                        PicBoxLogo.Image = new Bitmap(dlg.FileName);
                        PicBoxLogo.SizeMode = PictureBoxSizeMode.StretchImage;
                        txtLogoPath.Text = dlg.FileName;
                        ImageName = dlg.SafeFileName;
                        dtpStartdate.Focus();
                    }
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
        }
        public void SetButtonBefSave()
        {
            btnNewComp.Enabled = false;
            BtnSave.Enabled = true;
            BtnExit.Enabled = true;
        }

        private void BtnNewFinYear_Click(object sender, EventArgs e)
        {
            NewFinYear();
        }
        private void btnStartBackup_Click(object sender, EventArgs e)
        {
            DataBaseBackup();
        }

        public void DataBaseBackup()
        {
            //Metioned here your database name
            string dbname = "ASPLDB";
            SqlCommand sqlcmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            //Enter destination directory where backup file stored
            string destdir = "D:\\PMSBackup\\Data";
            //Check that directory already there otherwise create
            try
            {
                if (!System.IO.Directory.Exists(destdir))
                {
                    System.IO.Directory.CreateDirectory("D:\\PMSBackup\\Data");
                }
                //query to take backup database
                // day to day backup
                sqlcmd = new SqlCommand("BACKUP database ASPLDB to disk='" + destdir + "\\" + dbname + DateTime.Now.ToString("ddMMMyy") + ".Bak'", ASPLCSS.connect());
                sqlcmd.ExecuteNonQuery();

                // Complete backup to server
                string dir = "D:\\PMSBackup\\Comp_Data";
                //Check that directory already there otherwise create 
                if (!System.IO.Directory.Exists(dir))
                {
                    System.IO.Directory.CreateDirectory("D:\\PMSBackup\\Comp_Data");
                }
                sqlcmd = new SqlCommand("BACKUP database ASPLDB to disk='" + dir + "\\" + dbname + ".Bak'", ASPLCSS.connect());
                sqlcmd.ExecuteNonQuery();

                //    sqlcmd = new SqlCommand("SELECT ");
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            OKbtn();
        }
        public void OKbtn()
        {
            try
            {
                SqlCommand cmdBranch = new SqlCommand("SELECT Password FROM tbLoginDetails WHERE UserName='ADMIN'", con.connect());
                cmdBranch.CommandType = CommandType.Text;
                SqlDataReader dr = cmdBranch.ExecuteReader();
                if (dr.Read())
                {
                    if (mod.decrypt(dr["Password"].ToString()) == txtsupPass.Text)
                    {
                        btnStartBackup.Enabled = true;
                        BtnSave.Enabled = true;
                        grpboxSupPass.Visible = false;
                        txtDataBasNam.Enabled = true;
                        //  txtDsnName.Enabled = true;
                        if (opmode == "A")
                        {
                            txtCompNm.Focus();
                        }
                        if (NFY == true)
                        {
                            txtaddress1.Focus();
                        }
                        controlEnableTrue(tabCompanydetail.TabPages[0].Controls);
                    }
                    else
                    {
                        MessageBox.Show("Invalid Password");
                        txtsupPass.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            btnStartBackup.Enabled = false;
            Save();
            btnStartBackup.Enabled = true;
            if(Globalvariable.usercd=="")
            {
                MAINFORM mainfrm = new MAINFORM();
                this.Hide();
                mainfrm.Show();
                WindowState = FormWindowState.Minimized;
                ShowInTaskbar = false;
                this.Visible = false;
            }
        }

        public void Save()
        {
            try
            {
                //string Insert, smsAct = "N", MailAct = "N", PrintComNmBil = "N", PrintAdd = "N", PrintAfteSavKOT = "N", prinBilYN = "N", PrintNoPerBil = "N", lockingsystem = "N";
                //string PriDetaiBil = "N", TimONbil = "N", allowDelay = "N", allowshaYN = "N", ReprintYN = "N", KotCancel = "N", SendMailGuest = "N", SendmailCom = "N", saleRefNo = "N", printGSTserialYN = "N", seperateRestDirectYN = "N";
                //string PrintRoomTerif = "N", PerHeadTerif = "N", multiromterif = "N", taxAfterdic = "N", BillSetle = "N", Printsalecry = "N", AutoRefNo = "N", DetailBilrep = "N", seperatebilNo = "N", capilaryFile = "N",printcompanylogoYN="N";
                if (txtCompNm.Text == "")
                {
                    tabCompanydetail.SelectedIndex = 0;
                    txtCompNm.Focus();
                }
                else if (txtaddress1.Text == "")
                {
                    tabCompanydetail.SelectedIndex = 0;
                    txtaddress1.Focus();
                }              
                else if (dtpStartdate.Value > dtpEnddate.Value)
                {
                    MessageBox.Show("Invalid Date Range");
                    tabCompanydetail.SelectedIndex = 0;
                    dtpStartdate.Focus();
                }
                else
                {
                    if (cmbSmsAct.SelectedItem == "Yes")
                    {
                        if (txtDetailUseNm.Text == "")
                        {
                            txtDetailUseNm.Focus();
                        }
                        else if (txtDetailPass.Text == "")
                        {
                            txtDetailPass.Focus();
                        }
                        else if (txtSender.Text == "")
                        {
                            txtSender.Focus();
                        }
                    }
                    if (cmbMailAct.SelectedItem == "Yes")
                    {
                        if (txtmailToCust.Text == "")
                        {
                            txtmailToCust.Focus();
                        }
                        else if (txtCustPass.Text == "")
                        {
                            txtCustPass.Focus();
                        }
                        else if (txtMailhead.Text == "")
                        {
                            txtMailhead.Focus();
                        }
                        else if (txtMailbody.Text == "")
                        {
                            txtMailbody.Focus();
                        }
                    }
                    if (NFY == false)
                    {
                        if (Globalvariable.opmode == "A")
                        {
                            Insert = "Y";
                        }
                        else if (Globalvariable.opmode == "U")
                        {
                            Insert = "N";
                        }
                        //  string st=cmbSmsAct.SelectedItem.ToString();
                        if (cmbSmsAct.SelectedItem == "Yes")
                        {
                            smsAct = "Y";
                        }
                        else if (cmbSmsAct.SelectedItem == "No")
                        {
                            smsAct = "N";
                        }
                        if (cmbMailAct.SelectedItem == "Yes")
                        {
                            MailAct = "Y";
                        }
                        else if (cmbMailAct.SelectedItem == "No")
                        {
                            MailAct = "N";
                        }
                        if (cmbComNmOnBil.SelectedItem == "Yes")
                        {
                            PrintComNmBil = "Y";
                        }
                        else if (cmbComNmOnBil.SelectedItem == "No")
                        {
                            PrintComNmBil = "N";
                        }
                        if (cmbAddres.SelectedItem == "Yes")
                        {
                            PrintAdd = "Y";
                        }
                        else if (cmbAddres.SelectedItem == "No")
                        {
                            PrintAdd = "N";
                        }
                        if (cmbActPrinAfterSaving.SelectedItem == "Yes")
                        {
                            PrintAfteSavKOT = "Y";
                        }
                        else if (cmbActPrinAfterSaving.SelectedItem == "No")
                        {
                            PrintAfteSavKOT = "N";
                        }
                        if (cmbPrintBil.SelectedItem == "Yes")
                        {
                            prinBilYN = "Y";
                        }
                        else if (cmbPrintBil.SelectedItem == "No")
                        {
                            prinBilYN = "N";
                        }
                        if (cmbPrintNoPersOnBil.SelectedItem == "Yes")
                        {
                            PrintNoPerBil = "Y";
                        }
                        else if (cmbPrintNoPersOnBil.SelectedItem == "No")
                        {
                            PrintNoPerBil = "N";
                        }
                        if (cmbPrintDetailbil.SelectedItem == "Yes")
                        {
                            PriDetaiBil = "Y";
                        }
                        else if (cmbPrintDetailbil.SelectedItem == "No")
                        {
                            PriDetaiBil = "N";
                        }
                        if (cmbAllowDelay.SelectedItem == "Yes")
                        {
                            allowDelay = "Y";
                        }
                        else if (cmbAllowDelay.SelectedItem == "No")
                        {
                            allowDelay = "N";
                        }
                        if (cmbAllowShar.SelectedItem == "Yes")
                        {
                            allowshaYN = "Y";
                        }
                        else if (cmbAllowShar.SelectedItem == "No")
                        {
                            allowshaYN = "N";
                        }
                        if (cmbReprint.SelectedItem == "Yes")
                        {
                            ReprintYN = "Y";
                        }
                        else if (cmbReprint.SelectedItem == "No")
                        {
                            ReprintYN = "N";
                        }
                        if (cmbKOTCanc.SelectedItem == "Yes")
                        {
                            KotCancel = "Y";
                        }
                        else if (cmbKOTCanc.SelectedItem == "No")
                        {
                            KotCancel = "N";
                        }
                        if (cmbMailGust.SelectedItem == "Yes")
                        {
                            SendMailGuest = "Y";
                        }
                        else if (cmbMailGust.SelectedItem == "No")
                        {
                            SendMailGuest = "N";
                        }
                        if (cmbsendMailComp.SelectedItem == "Yes")
                        {
                            SendmailCom = "Y";
                        }
                        else if (cmbsendMailComp.SelectedItem == "No")
                        {
                            SendmailCom = "N";
                        }
                        if (cmbSaleRefNum.SelectedItem == "Yes")
                        {
                            saleRefNo = "Y";
                        }
                        else if (cmbSaleRefNum.SelectedItem == "No")
                        {
                            saleRefNo = "N";
                        }
                        if (cmbPrintRoomTarif.SelectedItem == "Yes")
                        {
                            PrintRoomTerif = "Y";
                        }
                        else if (cmbPrintRoomTarif.SelectedItem == "No")
                        {
                            PrintRoomTerif = "N";
                        }
                        if (cmbPerHedTarif.SelectedItem == "Yes")
                        {
                            PerHeadTerif = "Y";
                        }
                        else if (cmbPerHedTarif.SelectedItem == "No")
                        {
                            PerHeadTerif = "N";
                        }
                        if (cmbMultRomTerf.SelectedItem == "Yes")
                        {
                            multiromterif = "Y";
                        }
                        else if (cmbMultRomTerf.SelectedItem == "No")
                        {
                            multiromterif = "N";
                        }
                        if (cmbTaxAftrDisc.SelectedItem == "Yes")
                        {
                            taxAfterdic = "Y";
                        }
                        else if (cmbTaxAftrDisc.SelectedItem == "No")
                        {
                            taxAfterdic = "N";
                        }
                        if (cmbBilSetle.SelectedItem == "Yes")
                        {
                            BillSetle = "Y";
                        }
                        else if (cmbBilSetle.SelectedItem == "No")
                        {
                            BillSetle = "N";
                        }
                        if (cmbDetailBilReport.SelectedItem == "Yes")
                        {
                            DetailBilrep = "Y";
                        }
                        else if (cmbDetailBilReport.SelectedItem == "No")
                        {
                            DetailBilrep = "N";
                        }
                        if (cmbSeprteBilNo.SelectedItem == "Yes")
                        {
                            seperatebilNo = "Y";
                        }
                        else if (cmbSeprteBilNo.SelectedItem == "No")
                        {
                            seperatebilNo = "N";
                        }
                        if (cmbCapilFile.SelectedItem == "Yes")
                        {
                            capilaryFile = "Y";
                        }
                        else if (cmbCapilFile.SelectedItem == "No")
                        {
                            capilaryFile = "N";
                        }
                        if (cmbLockSystm.SelectedItem == "Yes")
                        {
                            lockingsystem = "Y";
                        }
                        else if (cmbLockSystm.SelectedItem == "No")
                        {
                            lockingsystem = "N";
                        }
                        if (cmbPrintGSTSerialNoYN.SelectedItem == "Yes")
                        {
                            printGSTserialYN = "Y";
                        }
                        else if (cmbPrintGSTSerialNoYN.SelectedItem == "No")
                        {
                            printGSTserialYN = "N";
                        }
                        if (cmbSepeRestadirYN.SelectedItem == "Yes")
                        {
                            seperateRestDirectYN = "Y";
                        }
                        else if (cmbSepeRestadirYN.SelectedItem == "No")
                        {
                            seperateRestDirectYN = "N";
                        }
                        if (cmbCompanyLogoYN.SelectedItem == "Yes")
                        {
                            printcompanylogoYN = "Y";
                        }
                        else if (cmbCompanyLogoYN.SelectedItem == "No")
                        {
                            printcompanylogoYN = "N";
                        }
                        // Save company logo

                        string sPathToFileToSave;
                        string fileName = "";
                        string str_docID = "";
                        byte[] fileData = null;
                        sPathToFileToSave = txtLogoPath.Text;
                        str_docID = DateTime.Now.ToString("MMddHHmmssyy") + fileName;
                        //use the file stream object to read the file from disk
                        //if (sPathToFileToSave.Trim() != "")
                        //{
                        //    using (System.IO.FileStream fs = new System.IO.FileStream(sPathToFileToSave, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                        //    {
                        //        // store the file in a byte array that is the length of the file
                        //        byte[] fileData = new byte[fs.Length];

                        //        // read in the file stream to the byte array
                        //        fs.Read(fileData, 0, System.Convert.ToInt32(fs.Length));

                        //      //  string dt = dateTimePicker2.Value.ToLongTimeString();

                        if (PicBoxLogo.Image != null)
                        {
                            MemoryStream stream = new MemoryStream();
                            PicBoxLogo.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                            fileData = stream.ToArray();
                            //System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            //PicBoxLogo.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            //byte[] fileData = new byte[ms.Length];
                            //ms.Read(fileData, 0, System.Convert.ToInt32(ms.Length));                     
                        }
                        string dt = dateTimePicker2.Value.ToLongTimeString();
                        if (opmode == "A")
                        {
                            cmd = new SqlCommand("INSERT INTO tbCompanyDetailsLodge(Company_Code,Company_Name,Branch_Name,Company_Logo,LogoFileName,LogoName,Address1,Address2,Address3,PhoneNo1,PhoneNo2,FaxNo,EMailId,SMSActiv,SmsUserName,SmsPassword,SmsSender,Licenses_No,Licenses_DT,ReceiverEmail1,ReceiverEmail2,RegistrationKey,GSTserialNo,"
                            + "VATtinNo,ServiceTaxNo,CST_No,LuxuryTaxNo,PanNo,DataBase_Nm,GSTNo,SACNo,BankDetail,BilHeading,SpecialMsg,PrintComNameBil,PrintAddressYN,PrintAftrSaviKOT,PrintBilYN,PrintNoOfPersBil,PrintDetailBil,TimeOnBilYN,AllowDelay,PageLengthBil,NoOfLinKOT,AllowSharingYN,ServiceTaxPer,ReprintYN,KOTCancel,NoOfLineForward,"
                            + "NoOfLineBackWard,MailActYN,MailID,MailPassword,MailHead,MailBody,SendMailGuestYN,SendMailComYN,SaleRefNoYN,PrintRomTerifBilYN,PerHeadTerifBilYN,MultiRomTerifTotaPax,TaxAfterDiscYN,MobileNo1,MobileNo2,MobileNo3,TotalDiscGLCode,ExtraPerson,CheckOutTim,RoomService,CheckInTime,Restaurantsale,TallyVoucherTyp,"
                            + "LuxTaxmastCod,BillSettleMentYN,ServiceTaxmastCode,PrintsaleCrystReportYN,CallRecordFilepath,AutoRefNoYN,PhonCalGrpID,DetailBilreportYN,PhonCalItemID,SeperateBilNoYN,TotalDiscTalyCod,CapillaryfileYN,RoomServiDirectPer,LockingSystemYN,LockingAuthorization,DefaultLegID,DefaultGuestID,TimeForWifi,IPAddressForWifi,"
                            + "PortNoForWiFi,UserNameForWifi,PasswordForwifi,ExtraBedGrpID,PrintGSTSerialNoYN,SeperRestDirectYN,BackUpPathName,ServicePlace,PrintCompanyLogoYN)"
                            + "VALUES('" + txtCompCode.Text + "','" + txtCompNm.Text + "','" + mod.Isnull(textCompNm2.Text, "-") + "',@Logo,'" + sPathToFileToSave + "','" + mod.Isnull(ImageName, "") + "','" + mod.Isnull(txtaddress1.Text, "-") + "','" + mod.Isnull(txtaddress2.Text, "-") + "','" + mod.Isnull(txtaddress3.Text, "-") + "',"
                            + "'" + mod.Isnull(txtphno1.Text, "0") + "','" + mod.Isnull(txtPhNo2.Text, "0") + "','" + mod.Isnull(txtFaxNo.Text, "0") + "','" + mod.Isnull(txtCompEmail.Text, "") + "','" + smsAct + "','" + mod.Isnull(txtDetailUseNm.Text, "") + "','" + mod.Isnull(txtDetailPass.Text, "") + "','" + mod.Isnull(txtSender.Text, "") + "',"
                            + "'" + mod.Isnull(txtLicenses.Text, "0") + "','" + mod.Isnull(txtLicenDate.Text, "") + "','" + mod.Isnull(txtRecivEmail1.Text, "") + "','" + mod.Isnull(txtReceivEmailId2.Text, "") + "','" + mod.Isnull(txtRegistrKey.Text, "0") + "','" + mod.Isnull(txtGstSeialNo.Text, "") + "','" + mod.Isnull(txtVattinNo.Text, "0") + "','" + mod.Isnull(txtserviceTaxNo.Text, "0") + "',"
                            + "'" + mod.Isnull(txtCstNo.Text, "0") + "','" + mod.Isnull(txtLuxTaxNo.Text, "0") + "','" + mod.Isnull(txtPanNo.Text, "0") + "','" + txtDataBasNam.Text + "','" + mod.Isnull(txtGstNo.Text, "0") + "','" + mod.Isnull(txtsacNo.Text, "0") + "','" + mod.Isnull(txtBankDetail.Text, "-") + "','" + mod.Isnull(txtBillHeading.Text, "-") + "',"
                            + "'" + mod.Isnull(txtSpecMsg.Text, "-") + "','" + PrintComNmBil + "','" + PrintAdd + "','" + PrintAfteSavKOT + "','" + prinBilYN + "','" + PrintNoPerBil + "','" + PriDetaiBil + "','" + TimONbil + "','" + allowDelay + "','" + mod.Isnull(txtMinLengOfbil.Text, "0") + "','" + mod.Isnull(NoOfLinesForKot.Text, "0") + "','" + allowshaYN + "',"
                            + "'" + mod.Isnull(txtServTax.Text, "0") + "','" + ReprintYN + "','" + KotCancel + "','" + mod.Isnull(txtNumLinForward.Text, "") + "','" + txtNumOfLinBackwrd.Text + "','" + MailAct + "','" + txtmailToCust.Text + "','" + txtCustPass.Text + "','" + txtMailhead.Text + "','" + txtMailbody.Text + "','" + SendMailGuest + "','" + SendmailCom + "',"
                            + "'" + saleRefNo + "','" + PrintRoomTerif + "','" + PerHeadTerif + "','" + multiromterif + "','" + taxAfterdic + "','" + mod.Isnull(txtmobile1.Text, "0") + "','" + mod.Isnull(txtMobile2.Text, "0") + "','" + mod.Isnull(txtMobile3.Text, "0") + "','" + mod.Isnull(txtTotlGlcod.Text, "0") + "','" + mod.Isnull(txtExtraPersn.Text, "0") + "',"
                            + "'" + mod.Isnull(txtCheckOuttim.Text, "") + "','" + mod.Isnull(txtRomservDirPer.Text, "") + "','" + dt + "','" + mod.Isnull(txtRestSale.Text, "0") + "','" + mod.Isnull(txtTallyVoucherTyp.Text, "0") + "','" + mod.Isnull(txtLuxTaxMastCod.Text, "0") + "','" + BillSetle + "','" + mod.Isnull(txtServTaxMastCod.Text, "0") + "','" + Printsalecry + "',"
                            + "'" + mod.Isnull(txtCallRecordFilPath.Text, "0") + "','" + AutoRefNo + "','" + mod.Isnull(txtPhonCalGropID.Text, "0") + "','" + DetailBilrep + "','" + mod.Isnull(txtPhonCalItmId.Text, "0") + "','" + seperatebilNo + "','" + mod.Isnull(txtDisTalCod.Text, "0") + "','" + capilaryFile + "','" + mod.Isnull(txtRoomService.Text, "0") + "',"
                            + "'" + lockingsystem + "','" + mod.Isnull(txtLockAuto.Text, "0") + "','" + mod.Isnull(txtDefaltLegId.Text, "0") + "','" + mod.Isnull(txtGuestID.Text, "0") + "','" + mod.Isnull(txtTimeForWifi.Text, "") + "','" + mod.Isnull(txtIPAddressForWifi.Text, "") + "','" + mod.Isnull(txtPortNumberForWifi.Text, "") + "',"
                            + "'" + mod.Isnull(txtUserNameforwifi.Text, "") + "','" + mod.Isnull(txtPasswordForWifi.Text, "") + "','" + mod.Isnull(txtExtraBedGrpID.Text, "") + "','" + printGSTserialYN + "','" + seperateRestDirectYN + "','" + mod.Isnull(txtBackupPathName.Text, "") + "','" + mod.Isnull(txtServicePlace.Text, "") + "','" + printcompanylogoYN + "')", con.connect());
                        }
                        else if (opmode == "U")
                        {
                            cmd = new SqlCommand("UPDATE tbCompanyDetailsLodge SET Company_Name='" + mod.Isnull(txtCompNm.Text, "") + "',Branch_Name='" + mod.Isnull(textCompNm2.Text, "-") + "',Company_Logo=@Logo,LogoFileName='" + sPathToFileToSave + "',LogoName='" + mod.Isnull(ImageName, "") + "',Address1='" + mod.Isnull(txtaddress1.Text, "") + "',Address2='" + mod.Isnull(txtaddress2.Text, "-") + "',Address3='" + mod.Isnull(txtaddress3.Text, "-") + "',"
                            + "PhoneNo1='" + mod.Isnull(txtphno1.Text, "0") + "',PhoneNo2='" + mod.Isnull(txtPhNo2.Text, "0") + "',FaxNo='" + mod.Isnull(txtFaxNo.Text, "0") + "',EMailId='" + mod.Isnull(txtCompEmail.Text, "") + "',SMSActiv='" + smsAct + "',SmsUserName='" + mod.Isnull(txtDetailUseNm.Text, "") + "',SmsPassword='" + mod.Isnull(txtDetailPass.Text, "") + "',SmsSender='" + mod.Isnull(txtSender.Text, "") + "',ReceiverEmail1='" + mod.Isnull(txtRecivEmail1.Text, "") + "',"
                            + "ReceiverEmail2='" + mod.Isnull(txtReceivEmailId2.Text, "") + "',Licenses_No='" + mod.Isnull(txtLicenses.Text, "0") + "',Licenses_DT='" + mod.Isnull(txtLicenDate.Text, "") + "',RegistrationKey='" + mod.Isnull(txtRegistrKey.Text, "0") + "',GSTserialNo='" + mod.Isnull(txtGstSeialNo.Text, "") + "',VATtinNo='" + mod.Isnull(txtVattinNo.Text, "0") + "',ServiceTaxNo='" + mod.Isnull(txtserviceTaxNo.Text, "0") + "',CST_No='" + mod.Isnull(txtCstNo.Text, "0") + "',LuxuryTaxNo='" + mod.Isnull(txtLuxTaxNo.Text, "0") + "',"
                            + "PanNo='" + mod.Isnull(txtPanNo.Text, "0") + "',DataBase_Nm='" + txtDataBasNam.Text + "',GSTNo='" + mod.Isnull(txtGstNo.Text, "0") + "',SACNo='" + mod.Isnull(txtsacNo.Text, "0") + "',BankDetail='" + mod.Isnull(txtBankDetail.Text, "-") + "',BilHeading='" + mod.Isnull(txtBillHeading.Text, "-") + "',SpecialMsg= '" + mod.Isnull(txtSpecMsg.Text, "-") + "',PrintComNameBil='" + PrintComNmBil + "',PrintAddressYN='" + PrintAdd + "',PrintAftrSaviKOT='" + PrintAfteSavKOT + "',"
                            + "PrintBilYN='" + prinBilYN + "',PrintNoOfPersBil='" + PrintNoPerBil + "',PrintDetailBil='" + PriDetaiBil + "',TimeOnBilYN='" + TimONbil + "',AllowDelay='" + allowDelay + "',PageLengthBil='" + mod.Isnull(txtMinLengOfbil.Text, "0") + "',NoOfLinKOT='" + mod.Isnull(NoOfLinesForKot.Text, "0") + "',AllowSharingYN='" + allowshaYN + "',ServiceTaxPer='" + txtServTax.Text + "',ReprintYN='" + ReprintYN + "',KOTCancel='" + KotCancel + "',NoOfLineForward='" + mod.Isnull(txtNumLinForward.Text, "") + "',"
                            + "NoOfLineBackWard='" + mod.Isnull(txtNumOfLinBackwrd.Text, "") + "',MailActYN='" + MailAct + "',MailID='" + mod.Isnull(txtmailToCust.Text, "") + "',MailPassword='" + mod.Isnull(txtCustPass.Text, "") + "',MailHead='" + mod.Isnull(txtMailhead.Text, "") + "',MailBody='" + mod.Isnull(txtMailbody.Text, "") + "',SendMailGuestYN='" + SendMailGuest + "',SendMailComYN='" + SendmailCom + "',SaleRefNoYN='" + saleRefNo + "',PrintRomTerifBilYN='" + PrintRoomTerif + "',PerHeadTerifBilYN='" + PerHeadTerif + "',"
                            + "MultiRomTerifTotaPax='" + multiromterif + "',TaxAfterDiscYN='" + taxAfterdic + "',MobileNo1='" + mod.Isnull(txtmobile1.Text, "0") + "',MobileNo2='" + mod.Isnull(txtMobile2.Text, "0") + "',MobileNo3='" + mod.Isnull(txtMobile3.Text, "0") + "',TotalDiscGLCode='" + mod.Isnull(txtTotlGlcod.Text, "0") + "',ExtraPerson='" + mod.Isnull(txtExtraPersn.Text, "0") + "',CheckOutTim='" + mod.Isnull(txtCheckOuttim.Text, "") + "',RoomService='" + mod.Isnull(txtRomservDirPer.Text, "") + "',"
                            + "CheckInTime='" + dt + "',Restaurantsale='" + mod.Isnull(txtRestSale.Text, "0") + "',TallyVoucherTyp='" + mod.Isnull(txtTallyVoucherTyp.Text, "0") + "',LuxTaxmastCod='" + mod.Isnull(txtLuxTaxMastCod.Text, "0") + "',BillSettleMentYN='" + BillSetle + "',ServiceTaxmastCode='" + mod.Isnull(txtServTaxMastCod.Text, "0") + "',PrintsaleCrystReportYN='" + Printsalecry + "',CallRecordFilepath='" + mod.Isnull(txtCallRecordFilPath.Text, "0") + "',AutoRefNoYN='" + AutoRefNo + "',"
                            + "PhonCalGrpID='" + mod.Isnull(txtPhonCalGropID.Text, "0") + "',SeperateBilNoYN='" + seperatebilNo + "',TotalDiscTalyCod='" + mod.Isnull(txtDisTalCod.Text, "0") + "',CapillaryfileYN='" + capilaryFile + "',RoomServiDirectPer='" + mod.Isnull(txtRoomService.Text, "0") + "',LockingSystemYN='" + lockingsystem + "',LockingAuthorization='" + mod.Isnull(txtLockAuto.Text, "0") + "',DefaultLegID='" + mod.Isnull(txtDefaltLegId.Text, "0") + "',DefaultGuestID='" + mod.Isnull(txtGuestID.Text, "0") + "',"
                            + "TimeForWifi='" + mod.Isnull(txtTimeForWifi.Text, "") + "',IPAddressForWifi='" + mod.Isnull(txtIPAddressForWifi.Text, "") + "',PortNoForWiFi='" + mod.Isnull(txtPortNumberForWifi.Text, "") + "',UserNameForWifi='" + mod.Isnull(txtUserNameforwifi.Text, "") + "',PasswordForwifi='" + mod.Isnull(txtPasswordForWifi.Text, "") + "',ExtraBedGrpID='" + mod.Isnull(txtExtraBedGrpID.Text, "0") + "',PrintGSTSerialNoYN='" + printGSTserialYN + "',SeperRestDirectYN='" + seperateRestDirectYN + "',BackUpPathName='" + mod.Isnull(txtBackupPathName.Text, "") + "',ServicePlace='" + mod.Isnull(txtServicePlace.Text, "") + "',"
                            + "PrintCompanyLogoYN='" + printcompanylogoYN + "' WHERE Company_Code='" + txtCompCode.Text + "'", con.connect());

                        }
                        //SqlParameter param1 = new SqlParameter("@Logo", SqlDbType.Image);
                        //param1.Value = fileData;

                        if (PicBoxLogo.Image == null)
                        {
                            SqlParameter param1 = new SqlParameter("@Logo", SqlDbType.VarChar);
                            param1.Value = fileData;
                            cmd.Parameters.Add(param1).Value = "NULL";
                        }
                        else
                        {
                            SqlParameter param1 = new SqlParameter("@Logo", SqlDbType.Image);
                            param1.Value = fileData;
                            cmd.Parameters.Add(param1);
                        }
                        cmd.ExecuteNonQuery();

                    }

                    //Select FrmCompany Select
                    FrmCompanySelect frmComselect = new FrmCompanySelect();
                    frmComselect.ListFinanceYear();
                    if (opmode != "U")
                    {
                        //SqlDataAdapter tbyer_da = new SqlDataAdapter("SELECT * FROM tbYear", con.connect());
                        //Globalvariable.company_Srno = 0;
                        //DataTable dtnew = new DataTable();
                        //tbyer_da.Fill(dtnew);
                        //if (dtnew.Rows.Count != 0)
                        //{
                        //    SqlCommand max_srno = new SqlCommand("SELECT (MAX(Sr_No)) from tbYear ", con.connect());
                        //    int count = Convert.ToInt32(max_srno.ExecuteScalar());
                        //    string val = Convert.ToString(count);
                        //    val = mod.Isnull(val, "0");
                        //    Globalvariable.company_Srno = Convert.ToInt32(val);
                        //}
                        //Create new Year
                        string Startyear = DateTime.Parse(dtpStartdate.Value.ToString()).Year.ToString();
                        string Endyear = DateTime.Parse(dtpEnddate.Value.ToString()).Year.ToString();

                        string st = "INSERT INTO tbYear(Company_Code,Financial_Start_Year,Financial_End_Year,Start_Date,End_Date)VALUES(" + txtCompCode.Text + ",'" + Startyear + "','" + Endyear + "','" + dtpStartdate.Value + "','" + dtpEnddate.Value + "')";
                        cmd = new SqlCommand("INSERT INTO tbYear(Company_Code,Financial_Start_Year,Financial_End_Year,Start_Date,End_Date)VALUES(" + txtCompCode.Text + ",'" + Startyear + "','" + Endyear + "','" + dtpStartdate.Value + "','" + dtpEnddate.Value + "')", con.connect());
                        cmd.ExecuteNonQuery();
                    }
                    if (opmode == "A")
                    {
                        MessageBox.Show("New Company Created Sucessfully ...");
                    }
                    else if (opmode == "U")
                    {
                        MessageBox.Show("Information Updated Sucessfully ...");
                        frmComselect.ListCompname();
                    }
                    else if (NFY == true)
                    {
                        MessageBox.Show("New Financial Year Created sucessfully ...");
                    }
                    BtnNewFinYear.Enabled = true;
                    tabCompanydetail.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            UpdateCompany();
        }

        public void UpdateCompany()
        {
            try
            {
                btnStartBackup.Enabled = false;
                opmode = "U";
                controlEnableTrue(tabCompanydetail.TabPages[0].Controls);
                controlEnableTrue(tabCompanydetail.TabPages[1].Controls);
                if (cmbSmsAct.SelectedItem == "Yes")
                {
                    txtDetailUseNm.Enabled = true;
                    txtDetailPass.Enabled = true;
                    txtSender.Enabled = true;
                }
                if (cmbMailAct.SelectedItem == "Yes")
                {
                    txtmailToCust.Enabled = true;
                    txtCustPass.Enabled = true;
                    txtMailhead.Enabled = true;
                    txtMailbody.Enabled = true;
                    cmbMailGust.Enabled = true;
                    cmbMailGust.Enabled = true;
                }
                btnNewComp.Enabled = false;
                BtnSave.Enabled = true;
                BtnExit.Enabled = true;
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        #endregion

        #region combobox event
        private void cmbMailAct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMailAct.SelectedItem == "Yes")
            {
                txtmailToCust.Enabled = true;
                txtCustPass.Enabled = true;
                txtMailhead.Enabled = true;
                txtMailbody.Enabled = true;
                cmbMailGust.Enabled = true;
                cmbsendMailComp.Enabled = true;
            }
            if (cmbMailAct.SelectedItem == "No")
            {
                txtmailToCust.Enabled = false;
                txtCustPass.Enabled = false;
                txtMailhead.Enabled = false;
                txtMailbody.Enabled = false;
                cmbMailGust.Enabled = false;
                cmbsendMailComp.Enabled = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                lblHrs.Text = "12 HRS";
            }
            else if (checkBox2.Checked == false)
            {
                lblHrs.Text = "24 HRS";
            }
        }

       
        #endregion

    }
}

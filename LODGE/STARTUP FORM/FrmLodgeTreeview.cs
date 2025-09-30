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
using ASPL.CLASSES;
using System.IO;
using System.Net;
using System.Globalization;
//using UnityEngine;
using System.Collections;
//using System.Reflection;
//Import The FOLDERS
using ASPL.Utility;
using ASPL.LODGE.MASTERS;
using ASPL.LODGE.TRANSCTION;
using ASPL.ACCOUNT.TRANSACTION;
using ASPL.CLASSES;
using ASPL.STARTUP.FORMS;
using ASPL.Material_Management.MASTERS;
using ASPL.LODGE.REPORTS;
using ASPL.HOTEL.MASTERS;
using ASPL.HOTEL.Transactions;

namespace ASPL.LODGE.STARTUP_FORM
{
    public partial class FrmLodgeTreeview : Form
    {
        //  public static string connstr = ConfigurationManager.ConnectionStrings["ASPLDBCoonection"].ToString();

        // SqlConnection con = new SqlConnection(connstr);
        Connection con = new Connection();
        Image[] ImgObj;
        SqlCommand cmd;
        Module mod = new Module();
        DataSet ds = new DataSet();
        SqlDataAdapter da;
        Globalvariable gv = new Globalvariable();
        Design ObjDes = new Design();
        string Treetxt, flagChar, str, strExpdate;
        int tree;
        Connection ObjCon = new Connection();
        bool nodedoucli;
        TreeNode treenode = new TreeNode();
        public FrmLodgeTreeview()
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
        private void FrmLodgeTreeview_Load(object sender, EventArgs e)
        {
            //this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            //this.Location = new Point(0, 0);
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.WindowState = FormWindowState.Maximized;
            try
            {

                //Bitmap bm = new Bitmap(Properties.Resources.Icon);
                //this.Icon = Icon.FromHandle(bm.GetHicon());
                //this.ShowIcon = true;
                Loadfun();
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }
        public void Loadfun()
        {
            treeView1.Nodes.Clear();
            lblappname.Text = Globalvariable.ModuleNm;
            lblusernm.Text = Globalvariable.cmb_usenm;
            lblBrnchnm.Text = Globalvariable.branch_nm;
            AccessApplication();
            companyName();
            FillCompanyLogo();
            FillTreeview();
            ExpiryDate();
        }

        public void AccessApplication()
        {
            DataSet ds = new DataSet();
            string S = "SELECT module FROM tbUserAccess WHERE user_id='" + Globalvariable.usercd + "' AND FormAccess='Y'";
            SqlCommand Usercnd = new SqlCommand("SELECT module FROM tbUserAccess WHERE user_id='" + Globalvariable.usercd + "' AND FormAccess='Y' ORDER BY Application_id", ObjCon.connect());
            //SqlDataReader dr = Usercnd.ExecuteReader();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Usercnd;
            ds.Clear();
            da.Fill(ds);
            int rowsCount = 0;

            #region Allication Access
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (row["module"].ToString() == "Lodge Management System")
                {
                    if (lblappname.Text == "Lodge Management System")
                    {
                        linkLabelLodge.Visible = false;
                    }
                    else
                    {
                        linkLabelLodge.Visible = true;
                    }
                }
                else if (row["module"].ToString() == "Material Management System")
                {
                    if (lblappname.Text == "Material Management System")
                    {
                        linkLblMaterialmanagement.Visible = false;
                    }
                    else
                    {
                        linkLblMaterialmanagement.Visible = true;
                    }
                }
                else if (row["module"].ToString() == "Accounts Management System")
                {
                    if (lblappname.Text == "Accounts Management System")
                    {
                        linklblAcctManagement.Visible = false;
                    }
                    else
                    {
                        linklblAcctManagement.Visible = true;
                    }
                }
                else if (row["module"].ToString() == "Restaurant Management System")
                {
                    if (lblappname.Text == "Restaurant Management System")
                    {
                        lblRestaurant.Visible = false;
                    }
                    else
                    {
                        lblRestaurant.Visible = true;
                    }
                }
                else
                {
                    rowsCount++;
                }
            }
            #endregion
            #region Access
            //if (Globalvariable.flagchar == "L")
            //{
            //    linkLabelLodge.Visible = false;
            //}
            //else if (Globalvariable.flagchar == "M")
            //{
            //    linkLblMaterialmanagement.Visible = false;
            //}
            //else if (Globalvariable.flagchar == "A")
            //{
            //    linklblAcctManagement.Visible = false;
            //}

            //if (dr.Read())
            //{
            //    while (dr.Read())
            //    {
            //        if (dr["module"].ToString() == "Lodge Management System")
            //        {
            //            linkLabelLodge.Visible = true;
            //        }
            //        if (dr["module"].ToString() == "Material Management System")
            //        {
            //            linkLblMaterialmanagement.Visible = true;
            //        }
            //        if (dr["module"].ToString() == "Accounts Management System")
            //        {
            //            linklblAcctManagement.Visible = true;
            //        }
            //    }
            //}
            #endregion

        }

        public void FillCompanyLogo()
        {
            try
            {
                string path = Application.StartupPath + @"\images";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                foreach (string file in Directory.GetFiles(path))
                {
                    File.Delete(file);
                }
                str = "";
                str = "SELECT Company_Logo,LogoName FROM tbCompanyDetailsLodge WHERE Company_Code=" + Globalvariable.bcode + "";
                SqlDataReader dr = mod.GetRecord(str);
                if (dr.Read())
                {
                    if (dr["LogoName"].ToString() != "")
                    {
                        string sPathToSaveFileTo = path + "\\" + dr.GetValue(1);
                        // read in using GetValue and cast to byte array                     
                        byte[] fileData = (byte[])dr.GetValue(0);
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
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private Image GetCopyImage(string path)
        {
            using (Image im = Image.FromFile(path))
            {
                Bitmap bm = new Bitmap(im);
                return bm;
            }
        }


        public void companyName()
        {
            lblComNm.Text = Globalvariable.company_nm + " " + "(" + (Globalvariable.StartYear + "-" + Globalvariable.EndYear) + ")";
        }
        public void FillTreeview()
        {
            try
            {

                string S;
                #region BIND LEVEL_1 TREEVIEW
                string adminUser = "ADMIN";
                if (Globalvariable.username == adminUser)
                {
                    // cmd = new SqlCommand("SELECT * FROM tbLavel_1 WHERE Branchcode='" + Globalvariable.bcode + "' ORDER BY lev1_id", con.connect());
                    cmd = new SqlCommand("SP_FillTreeview", con.connect());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tableName", "tbLavel_1");
                    cmd.Parameters.AddWithValue("@userNm", "ADMIN");
                    cmd.Parameters.AddWithValue("@userId", Globalvariable.usercd);
                    cmd.Parameters.AddWithValue("@flag", Globalvariable.flagchar);
                    cmd.Parameters.AddWithValue("@id", "0");

                }
                else
                {
                    //cmd = new SqlCommand("SELECT * from tbLavel_1 where lev1_id in(SELECT lev1_id from tbUserAccess where user_id='" +
                    //             Globalvariable.usercd + "' and branch_code='" + Globalvariable.bcode + "' and FormAccess='Y' )and Branchcode='" +
                    //            Globalvariable.bcode + "' order by lev1_id", con.connect());
                    cmd = new SqlCommand("SP_FillTreeview", con.connect());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tableName", "tbLavel_1");
                    cmd.Parameters.AddWithValue("@userNm", "0");
                    cmd.Parameters.AddWithValue("@userId", Globalvariable.usercd);
                    cmd.Parameters.AddWithValue("@flag", Globalvariable.flagchar);
                    cmd.Parameters.AddWithValue("@id", "0");
                }
                SqlDataReader dr = cmd.ExecuteReader();
                //   treeView1.Nodes.Clear();
                while (dr.Read())
                {
                    TreeNode treenode = new TreeNode();
                    treenode.Text = dr["Field_name"].ToString();
                    treenode.Tag = dr["lev1_id"];
                    treeView1.Nodes.Add(treenode);
                    if (Globalvariable.username == adminUser)
                    {
                        //cmd = new SqlCommand("SELECT * FROM tbLevel_2 WHERE lev1_id='" + dr["lev1_id"].ToString() + "' and (flag=('" +
                        //            Globalvariable.flagchar + "') OR flag=('AL')) and Branchcode='" + Globalvariable.bcode + "'  ORDER BY lev2_id", con.connect());
                        cmd = new SqlCommand("SP_FillTreeview", con.connect());
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@tableName", "tbLevel_2");
                        cmd.Parameters.AddWithValue("@userNm", "ADMIN");
                        cmd.Parameters.AddWithValue("@userId", Globalvariable.usercd);
                        cmd.Parameters.AddWithValue("@flag", Globalvariable.flagchar);
                        cmd.Parameters.AddWithValue("@id", dr["lev1_id"].ToString());
                    }
                    else
                    {
                        //cmd = new SqlCommand("SELECT * FROM tbLevel_2 WHERE lev1_id='" + dr["lev1_id"].ToString() +
                        //   "' AND lev2_id IN(SELECT lev2_id FROM tbUserAccess WHERE user_id='" +
                        //  Globalvariable.usercd + "' AND (flag=('" + Globalvariable.flagchar + "') OR flag=('AL')) AND branch_code='" + Globalvariable.bcode +
                        //   "' AND FormAccess='Y') AND Branchcode='" + Globalvariable.bcode + "' ORDER BY lev2_fieldname", con.connect());
                        cmd = new SqlCommand("SP_FillTreeview", con.connect());
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@tableName", "tbLevel_2");
                        cmd.Parameters.AddWithValue("@userNm", "0");
                        cmd.Parameters.AddWithValue("@userId", Globalvariable.usercd);
                        cmd.Parameters.AddWithValue("@flag", Globalvariable.flagchar);
                        cmd.Parameters.AddWithValue("@id", dr["lev1_id"]);
                    }
                    SqlDataReader dr1 = cmd.ExecuteReader();
                    while (dr1.Read())
                    {
                        TreeNode childNode = new TreeNode();
                        childNode.Text = dr1["lev2_fieldname"].ToString();
                        childNode.Tag = dr1["lev2_id"];
                        treenode.Nodes.Add(childNode);
                        if (Globalvariable.username == adminUser)
                        {
                            //cmd = new SqlCommand("SELECT * FROM tbLevel_3 WHERE lev2_id='" + dr1["lev2_id"].ToString() +
                            //        "' AND Branchcode='" + Globalvariable.bcode + "' AND (flag=('" + Globalvariable.flagchar + "') OR flag=('AL')) ORDER BY lev3_id", con.connect());
                            cmd = new SqlCommand("SP_FillTreeview", con.connect());
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@tableName", "tbLevel_3");
                            cmd.Parameters.AddWithValue("@userNm", "ADMIN");
                            cmd.Parameters.AddWithValue("@userId", Globalvariable.usercd);
                            cmd.Parameters.AddWithValue("@flag", Globalvariable.flagchar);
                            cmd.Parameters.AddWithValue("@id", dr1["lev2_id"]);
                        }
                        else
                        {
                            //cmd = new SqlCommand("SELECT * FROM tbLevel_3 WHERE lev2_id='" + dr1["lev2_id"].ToString() +
                            //        "' AND Branchcode='" + Globalvariable.bcode + "' AND lev3_id IN (SELECT lev3_id FROM tbUserAccess WHERE user_id='" +
                            //Globalvariable.usercd + "' AND (flag=('" + Globalvariable.flagchar + "') OR flag=('AL')) AND Branchcode='" + Globalvariable.bcode + "' AND FormAccess='Y')ORDER BY lev3_fieldname", con.connect());
                            cmd = new SqlCommand("SP_FillTreeview", con.connect());
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@tableName", "tbLevel_3");
                            cmd.Parameters.AddWithValue("@userNm", "0");
                            cmd.Parameters.AddWithValue("@userId", Globalvariable.usercd);
                            cmd.Parameters.AddWithValue("@flag", Globalvariable.flagchar);
                            cmd.Parameters.AddWithValue("@id", dr1["lev2_id"]);
                        }
                        SqlDataReader dr2 = cmd.ExecuteReader();
                        while (dr2.Read())
                        {
                            TreeNode child = new TreeNode();
                            child.Text = dr2["lev3_fieldname"].ToString();
                            child.Tag = dr2["lev3_id"];
                            childNode.Nodes.Add(child);
                            if (Globalvariable.username == adminUser)
                            {
                                //cmd = new SqlCommand("SELECT * FROM tbLevel_4 WHERE lev3_id='" + dr2["lev3_id"].ToString() +
                                //    "' AND Branchcode='" + Globalvariable.bcode + "' AND (flag=('" + Globalvariable.flagchar + "') OR flag=('AL')) ORDER BY lev4_id", con.connect());
                                cmd = new SqlCommand("SP_FillTreeview", con.connect());
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@tableName", "tbLevel_4");
                                cmd.Parameters.AddWithValue("@userNm", "ADMIN");
                                cmd.Parameters.AddWithValue("@userId", Globalvariable.usercd);
                                cmd.Parameters.AddWithValue("@flag", Globalvariable.flagchar);
                                cmd.Parameters.AddWithValue("@id", dr2["lev3_id"]);
                            }
                            else
                            {
                                //     cmd = new SqlCommand("SELECT * FROM tbLevel_4 WHERE lev3_id='" + dr2["lev3_id"].ToString() +
                                //           "' AND Branchcode='" + Globalvariable.bcode + "' AND lev4_id IN(SELECT lev4_id FROM tbUserAccess WHERE user_id='" +
                                //Globalvariable.usercd + "' AND (flag=('" + Globalvariable.flagchar + "') OR flag=('AL')) AND Branchcode='" + Globalvariable.bcode + "' AND FormAccess='Y') ORDER BY lev4_id", con.connect());

                                cmd = new SqlCommand("SP_FillTreeview", con.connect());
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@tableName", "tbLevel_4");
                                cmd.Parameters.AddWithValue("@userNm", "0");
                                cmd.Parameters.AddWithValue("@userId", Globalvariable.usercd);
                                cmd.Parameters.AddWithValue("@flag", Globalvariable.flagchar);
                                cmd.Parameters.AddWithValue("@id", dr2["lev3_id"]);
                            }
                            SqlDataReader dr3 = cmd.ExecuteReader();
                            while (dr3.Read())
                            {
                                TreeNode subchild = new TreeNode();
                                subchild.Text = dr3["lev4_fieldname"].ToString();
                                subchild.Tag = dr3["lev4_id"];
                                child.Nodes.Add(subchild);
                            }
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }
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


        public void ExpiryDate()
        {
            try
            {
                SqlCommand cmdExpdate = new SqlCommand("SELECT Expiry_DT FROM tbRegister", ObjCon.connect());
                SqlDataReader dr = cmdExpdate.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    // string Expdate =((DateTime)dr["Expiry_DT"]).ToString("dd/MM/yyyy");
                    strExpdate = dr["Expiry_DT"].ToString();
                }
                dr.Close();
                DateTime todayDate = DateTime.Now;
                if (strExpdate != "")
                {
                    DateTime expDateconv = Convert.ToDateTime(strExpdate);
                    TimeSpan span = expDateconv.Subtract(todayDate);
                    double Totaldays = span.TotalDays;
                    lblExpirydate.Text = "Due On:" + expDateconv.ToString("dd/MM/yyyy") + " " + Convert.ToInt16(Totaldays) + " Days.";
                }
                else
                {
                    lblExpirydate.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }
        private void TreeNodeFill(string Treetxt)
        {
            try
            {
                #region Level1
                if (Globalvariable.nodeLevel == 1)
                {

                    switch (Treetxt)
                    {
                        //case "44":
                        //    Globalvariable.frmName = "Measurement";
                        //    Models.Common.FormManagement.Instance.ProcessFormOpening<FrmMeasurement>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                        //    break;

                        //FrmMeasurement frmmeasurement = new FrmMeasurement();
                        //Globalvariable.frmName = "Measurement";
                        //frmmeasurement.TopLevel = false;
                        //frmmeasurement.Visible = true;
                        //frmmeasurement.Dock = DockStyle.None;
                        //this.panel7.Controls.Add(frmmeasurement);
                        //panel8.SendToBack();
                        //break;
                        case "44":
                            Globalvariable.frmName = "KOT Billing";
                            //MessageBox.Show("opening KOTBilling");
                            Models.Common.FormManagement.Instance.ProcessFormOpening<KOTBilling>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;


                        case "37":
                            Globalvariable.frmName = "Cal Insentive";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmCalInsentive>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;
                            //FrmCalInsentive frmcalinsen = new FrmCalInsentive();
                            //Globalvariable.frmName = "Cal Insentive";
                            //frmcalinsen.TopLevel = false;
                            //frmcalinsen.Visible = true;
                            //frmcalinsen.Dock = DockStyle.None;
                            //this.panel7.Controls.Add(frmcalinsen);
                            //panel8.SendToBack();
                            //break;
                        case "36":

                            Globalvariable.frmName = "Change Kitchen Section";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmChanKitSection>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;
                            //FrmChanKitSection frmchakitsec = new FrmChanKitSection();
                            //Globalvariable.frmName = "Change Kitchen Section";
                            //frmchakitsec.TopLevel = false;
                            //frmchakitsec.Visible = true;
                            //frmchakitsec.Dock = DockStyle.None;
                            //this.panel7.Controls.Add(frmchakitsec);
                            //panel8.SendToBack();
                            //break;
                        case "35":
                            Globalvariable.frmName = "Change Item Rates";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmChangeItemRate>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                        //FrmChangeItemRate frmchangeitmrate = new FrmChangeItemRate();
                        //Globalvariable.frmName = "Change Item Rates";
                        //frmchangeitmrate.TopLevel = false;
                        //frmchangeitmrate.Visible = true;
                        //frmchangeitmrate.Dock = DockStyle.None;
                        //this.panel7.Controls.Add(frmchangeitmrate);
                        //panel8.SendToBack();
                        //break;
                        case "29":
                            Globalvariable.frmName = "Reservation";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmReservation>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                            //FrmReservation Reservation = new FrmReservation();
                            //Globalvariable.frmName = "Reservation";
                            //Reservation.TopLevel = false;
                            //Reservation.Visible = true;
                            //Reservation.Dock = DockStyle.None;
                            //this.panel7.Controls.Add(Reservation);
                            //panel8.SendToBack();
                            //break;
                        case "28":
                            Globalvariable.frmName = "Petty Cash Book";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmPettyCashBook>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                            //FrmPettyCashBook PettyCash = new FrmPettyCashBook();
                            //Globalvariable.frmName = "Petty Cash Book";
                            //PettyCash.TopLevel = false;
                            //PettyCash.Visible = true;
                            //PettyCash.Dock = DockStyle.None;
                            //this.panel7.Controls.Add(PettyCash);
                            //panel8.SendToBack();
                            //break;
                        case "27":
                            Globalvariable.frmName = "Bank Reconciliation";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmBankReconcilation>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                            //FrmBankReconcilation Bankrecon = new FrmBankReconcilation();
                            //Globalvariable.frmName = "Bank Reconciliation";
                            //Bankrecon.TopLevel = false;
                            //Bankrecon.Visible = true;
                            //Bankrecon.Dock = DockStyle.None;
                            //this.panel7.Controls.Add(Bankrecon);
                            //panel8.SendToBack();
                            //break;
                        case "26":
                            Globalvariable.frmName = "Journal Voucher";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmJournalVoucher>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                        //    FrmJournalVoucher JV = new FrmJournalVoucher();
                        //    Globalvariable.frmName = "Journal Voucher";
                        //    JV.TopLevel = false;
                        //    JV.Visible = true;
                        //    JV.Dock = DockStyle.None;
                        //    this.panel7.Controls.Add(JV);
                        //    panel8.SendToBack();
                        //    break;
                        case "25":
                            Globalvariable.frmName = "Payment";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmPayment>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                            //FrmPayment Payment = new FrmPayment();
                            //Globalvariable.frmName = "Payment";
                            //Payment.TopLevel = false;
                            //Payment.Visible = true;
                            //Payment.Dock = DockStyle.None;
                            //this.panel7.Controls.Add(Payment);
                            //panel8.SendToBack();
                            //break;
                        case "24":
                            Globalvariable.frmName = "Contra";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmContra>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                            //FrmContra Contra = new FrmContra();
                            //Globalvariable.frmName = "Contra";
                            //Contra.TopLevel = false;
                            //Contra.Visible = true;
                            //Contra.Dock = DockStyle.None;
                            //this.panel7.Controls.Add(Contra);
                            //panel8.SendToBack();
                            //break;
                        case "23":
                            Globalvariable.frmName = "Receipt";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmReceipt>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                            //FrmReceipt receipt = new FrmReceipt();
                            //Globalvariable.frmName = "Receipt";
                            //receipt.TopLevel = false;
                            //receipt.Visible = true;
                            //receipt.Dock = DockStyle.None;
                            //this.panel7.Controls.Add(receipt);
                            //panel8.SendToBack();
                            //break;
                        case "21":
                            Globalvariable.frmName = "Day Book";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<DayBook>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                            //DayBook daybook = new DayBook();
                            //Globalvariable.frmName = "DayBook";
                            //daybook.TopLevel = false;
                            //daybook.Visible = true;
                            //daybook.Dock = DockStyle.None;
                            //this.panel7.Controls.Add(daybook);
                            //panel8.SendToBack();
                            //break;
                        case "20":
                            Globalvariable.frmName = "GeneralLedger";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<GeneralLedger>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                            //GeneralLedger generallegr = new GeneralLedger();
                            //Globalvariable.frmName = "GeneralLedger";
                            //generallegr.TopLevel = false;
                            //generallegr.Visible = true;
                            //generallegr.Dock = DockStyle.None;
                            //this.panel7.Controls.Add(generallegr);
                            //panel8.SendToBack();
                            //break;
                        case "19":
                            Globalvariable.frmName = "AccountGroup";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmAccountGroup>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                            //FrmGroup group = new FrmGroup();
                            //Globalvariable.frmName = "Group";
                            //group.TopLevel = false;
                            //group.Visible = true;
                            //group.Dock = DockStyle.None;
                            //this.panel7.Controls.Add(group);
                            //panel8.SendToBack();
                            //break;
                        case "18":
                            Globalvariable.frmName = "Companyselect";
                            Models.Common.FormManagement.Instance.ProcessModelFormOpening<FrmCompanySelect>(ref panel7);
                            break;

                            //FrmCompanySelect frmComsel = new FrmCompanySelect();
                            //Globalvariable.frmName = "Companyselect";
                            //frmComsel.TopLevel = false;
                            //frmComsel.Visible = true;
                            //frmComsel.Dock = DockStyle.None;
                            //this.panel7.Controls.Add(frmComsel);
                            //panel8.SendToBack();
                            //break;
                        case "17":
                            {
                                Globalvariable.frmName = "UtilityComDetail";
                                var objForm = Models.Common.FormManagement.Instance.ProcessModelFormOpening<FrmReportAuthenti>(ref panel7);
                                if (objForm != null)
                                {
                                    if (objForm.IsValid)
                                    {
                                        objForm.Dispose();
                                        Models.Common.FormManagement.Instance.ProcessFormOpening<FrmCompanyDetails>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                                    }
                                }
                                break;
                            }
                            //FrmCompanyDetails frmComDetail = new FrmCompanyDetails();              // frmComDetail.ShowDialog();
                            //FrmReportAuthenti frmreportAuthe = new FrmReportAuthenti();
                            //Globalvariable.frmName = "UtilityComDetail";
                            //frmreportAuthe.TopLevel = false;
                            //this.panel7.Controls.Add(frmreportAuthe);
                            //frmreportAuthe.Dock = DockStyle.None;
                            //panel8.SendToBack();
                            //break;
                        case "16":
                            Globalvariable.frmName = "Plan";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmPlan>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                            //FrmPlan frmplan = new FrmPlan();
                            //Globalvariable.frmName = "Plan";
                            //frmplan.TopLevel = false;
                            //frmplan.Visible = true;
                            //frmplan.Dock = DockStyle.None;
                            //this.panel7.Controls.Add(frmplan);
                            //panel8.SendToBack();
                            //break;
                        case "15":
                            Globalvariable.frmName = "Marketsegment";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmMarketSegment>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                            //FrmMarketSegment marketseg = new FrmMarketSegment();
                            //Globalvariable.frmName = "Marketsegment";
                            //marketseg.TopLevel = false;
                            //marketseg.Visible = true;
                            //marketseg.Dock = DockStyle.None;
                            //this.panel7.Controls.Add(marketseg);
                            //panel8.SendToBack();
                            //break;
                        //case "14":
                        //    Globalvariable.frmName = "Kitchen Section";
                        //    Models.Common.FormManagement.Instance.ProcessFormOpening<frmKotSection>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                        //    break;

                        //frmKotSection kotsec = new frmKotSection();
                        //Globalvariable.frmName = "KotSection";
                        //kotsec.TopLevel = false;
                        //kotsec.Visible = true;
                        //kotsec.Dock = DockStyle.None;
                        //this.panel7.Controls.Add(kotsec);
                        //panel8.SendToBack();
                        //break;
                        case "13":
                            Globalvariable.frmName = "MenuItem";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmSubItemMaster>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                            //FrmSubItemMaster menuItm = new FrmSubItemMaster();
                            //Globalvariable.frmName = "MenuItem";
                            //menuItm.TopLevel = false;
                            //menuItm.Visible = true;
                            //menuItm.Dock = DockStyle.None;
                            //this.panel7.Controls.Add(menuItm);
                            //panel8.SendToBack();
                            //break;
                        case "12":
                            Globalvariable.frmName = "MenuGroup";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmItemMaster>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                            //FrmItemMaster menugrp = new FrmItemMaster();
                            //Globalvariable.frmName = "MenuGroup";
                            //menugrp.TopLevel = false;
                            //menugrp.Visible = true;
                            //menugrp.Dock = DockStyle.None;
                            //this.panel7.Controls.Add(menugrp);
                            //panel8.SendToBack();
                            //break;
                        case "11":
                            Globalvariable.frmName = "IdentityType";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmIdentityType>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                            //FrmIdentityType identitytyp = new FrmIdentityType();
                            //Globalvariable.frmName = "IdentityType";
                            //identitytyp.TopLevel = false;
                            //identitytyp.Visible = true;
                            //identitytyp.Dock = DockStyle.None;
                            //this.panel7.Controls.Add(identitytyp);
                            //panel8.SendToBack();
                            //break;
                        case "9":
                            Globalvariable.frmName = "TallyMaster";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmTallyMaster>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                            //FrmTallyMaster Tallymast = new FrmTallyMaster();
                            //Globalvariable.frmName = "TallyMaster";
                            //Tallymast.TopLevel = false;
                            //Tallymast.Visible = true;
                            //Tallymast.Dock = DockStyle.None;
                            //this.panel7.Controls.Add(Tallymast);
                            //panel8.SendToBack();
                            //break;
                        case "7":
                            Globalvariable.frmName = "GuestInfo";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmGuestInformation>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                            //FrmGuestInformation guestinfo = new FrmGuestInformation();
                            //Globalvariable.frmName = "GuestInfo";
                            //guestinfo.TopLevel = false;
                            //guestinfo.Visible = true;
                            //guestinfo.Dock = DockStyle.None;
                            //this.panel7.Controls.Add(guestinfo);
                            //panel8.SendToBack();
                            //break;
                        case "6":
                            Globalvariable.frmName = "Discount";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmDiscount>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                            //FrmDiscount discount = new FrmDiscount();
                            //Globalvariable.frmName = "Discount";
                            //discount.TopLevel = false;
                            //discount.Visible = true;
                            //discount.Dock = DockStyle.None;
                            //this.panel7.Controls.Add(discount);
                            //panel8.SendToBack();
                            //break;
                        case "5":
                            Globalvariable.frmName = "TaxMaster";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<TaxMaster>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                            //TaxMaster taxmast = new TaxMaster();
                            //Globalvariable.frmName = "TaxMaster";
                            //taxmast.TopLevel = false;
                            //taxmast.Visible = true;
                            //taxmast.Dock = DockStyle.None;
                            //this.panel7.Controls.Add(taxmast);
                            //panel8.SendToBack();
                            //break;
                        case "4":
                            Models.Common.FormManagement.Instance.ProcessModelFormOpening<FrmChangePass>(ref panel7); ;
                            break;

                            //FrmChangePass change = new FrmChangePass();
                            //Globalvariable.frmName = "ChangePass";
                            //change.TopLevel = false;
                            //change.Visible = true;
                            //change.Dock = DockStyle.None;
                            //this.panel7.Controls.Add(change);
                            //panel8.SendToBack();
                            //break;
                        case "3":
                            {
                                var objForm = Models.Common.FormManagement.Instance.ProcessModelFormOpening<FrmSuperwiserPass>(ref panel7);
                                if (objForm != null)
                                {
                                    if (objForm.IsValid)
                                    {
                                        objForm.Dispose();
                                        Models.Common.FormManagement.Instance.ProcessModelFormOpening<FrmCreateUserAccess>(ref panel7);

                                    }
                                }
                                break;
                            }

                            //FrmSuperwiserPass supPass = new FrmSuperwiserPass();
                            //// FrmCreateUserAccess newuser = new FrmCreateUserAccess();
                            //supPass.TopLevel = false;
                            //supPass.Visible = true;
                            //supPass.Dock = DockStyle.None;
                            //this.panel7.Controls.Add(supPass);
                            //panel8.SendToBack();
                            //break;
                        case "1":
                            Globalvariable.frmName = "UserAccess";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmUserAccess>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                            //FrmUserAccess UserAccess = new FrmUserAccess();
                            //Globalvariable.frmName = "UserAccess";
                            //UserAccess.TopLevel = false;
                            //UserAccess.Visible = true;
                            //UserAccess.Dock = DockStyle.None;
                            //this.panel7.Controls.Add(UserAccess);
                            //panel8.SendToBack();
                            //break;
                    }

                }
                #endregion
                #region level2
                else if (Globalvariable.nodeLevel == 2)
                {
                    switch (Treetxt)
                    {
                        case "41":
                            Globalvariable.frmName = "GuestInfo";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmGuestInformation>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;
                        case "38":
                            Globalvariable.frmName = "Kot Message";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmKotMsg>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                        //FrmKotMsg frmkotmsg = new FrmKotMsg();
                        //Globalvariable.frmName = "Kot Message";
                        //frmkotmsg.TopLevel = false;
                        //frmkotmsg.Visible = true;
                        //frmkotmsg.Dock = DockStyle.None;
                        //this.panel7.Controls.Add(frmkotmsg);
                        //panel8.SendToBack();
                        //break;
                        case "37":
                            Globalvariable.frmName = "Bill Message";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmBillMsg>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                        //FrmBillMsg frmbillmsg = new FrmBillMsg();
                        //Globalvariable.frmName = "Bill Message";
                        //frmbillmsg.TopLevel = false;
                        //frmbillmsg.Visible = true;
                        //frmbillmsg.Dock = DockStyle.None;
                        //this.panel7.Controls.Add(frmbillmsg);
                        //panel8.SendToBack();
                        //break;
                        case "36":
                            Globalvariable.frmName = "Table Master";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmTableMaster>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                        //FrmTableMaster frmtablemast = new FrmTableMaster();
                        //Globalvariable.frmName = "Table Master";
                        //frmtablemast.TopLevel = false;
                        //frmtablemast.Visible = true;
                        //frmtablemast.Dock = DockStyle.None;
                        //this.panel7.Controls.Add(frmtablemast);
                        //panel8.SendToBack();
                        //break;
                        case "35":
                            Globalvariable.frmName = "Table Group";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmTableGroup>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                        //FrmTableGroup frmtablegrp = new FrmTableGroup();
                        //Globalvariable.frmName = "Table Group";
                        //frmtablegrp.TopLevel = false;
                        //frmtablegrp.Visible = true;
                        //frmtablegrp.Dock = DockStyle.None;
                        //this.panel7.Controls.Add(frmtablegrp);
                        //panel8.SendToBack();
                        //break;
                        case "34":
                            Globalvariable.frmName = "Liq. Barnd";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmLiqGrp>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                        //FrmLiqGrp frmliqgrp = new FrmLiqGrp();
                        //Globalvariable.frmName = "Liq. Barnd";
                        //frmliqgrp.TopLevel = false;
                        //frmliqgrp.Visible = true;
                        //frmliqgrp.Dock = DockStyle.None;
                        //this.panel7.Controls.Add(frmliqgrp);
                        //panel8.SendToBack();
                        //break;
                        case "33":
                            Globalvariable.frmName = "Liq. Group";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmLiqGrpName>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                        //FrmLiqGrpName frmliqgrpnm = new FrmLiqGrpName();
                        //Globalvariable.frmName = "Liq. Group";
                        //frmliqgrpnm.TopLevel = false;
                        //frmliqgrpnm.Visible = true;
                        //frmliqgrpnm.Dock = DockStyle.None;
                        //this.panel7.Controls.Add(frmliqgrpnm);
                        //panel8.SendToBack();
                        //break;
                        case "44":
                            Globalvariable.frmName = "Measurement";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmMeasurement>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                        case "32":
                            Globalvariable.frmName = "Food Type";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmFoodType>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;
                        //FrmFoodType frmcfoodtype = new FrmFoodType();
                        //Globalvariable.frmName = "Food Type";
                        //frmcfoodtype.TopLevel = false;
                        //frmcfoodtype.Visible = true;
                        //frmcfoodtype.Dock = DockStyle.None;
                        //frmcfoodtype.ControlBox = false;
                        //this.panel7.Controls.Add(frmcfoodtype);
                        //panel8.SendToBack();
                        //break;
                        case "45":
                            Globalvariable.frmName = "Kitchen Section";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<frmKotSection>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                        case "31":
                            Globalvariable.frmName = "Menu Card";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmMenuCard>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                        //FrmMenuCard frmmenucard = new FrmMenuCard();
                        //Globalvariable.frmName = "Menu Card";
                        //frmmenucard.TopLevel = false;
                        //frmmenucard.Visible = true;
                        //frmmenucard.Dock = DockStyle.None;
                        //this.panel7.Controls.Add(frmmenucard);
                        //panel8.SendToBack();
                        //break;
                        case "30":
                            Globalvariable.frmName = "Menu Group";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmMenuGroup>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                        //FrmMenuGroup frmmenugrp = new FrmMenuGroup();
                        //Globalvariable.frmName = "Menu Group";
                        //frmmenugrp.TopLevel = false;
                        //frmmenugrp.Visible = true;
                        //frmmenugrp.Dock = DockStyle.None;
                        //this.panel7.Controls.Add(frmmenugrp);
                        //panel8.SendToBack();
                        //break;
                        case "29":
                            Globalvariable.frmName = "Menu Category";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmMenuCategory>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                        //FrmMenuCategory frmcategory = new FrmMenuCategory();
                        //Globalvariable.frmName = "Menu Category";
                        //frmcategory.TopLevel = false;
                        //frmcategory.Visible = true;
                        //frmcategory.Dock = DockStyle.None;
                        //this.panel7.Controls.Add(frmcategory);
                        //panel8.SendToBack();
                        //break;
                        case "27":
                            FrmReport booklist = new FrmReport();
                            Globalvariable.frmName = "Day Book Rpt List";
                            booklist.TopLevel = false;
                            booklist.Visible = true;
                            booklist.Dock = DockStyle.None;
                            this.panel7.Controls.Add(booklist);
                            panel8.SendToBack();
                            break;
                        case "26":
                            FrmReport GeneralLeglist = new FrmReport();
                            Globalvariable.frmName = "General Ledger Rpt List";
                            GeneralLeglist.TopLevel = false;
                            GeneralLeglist.Visible = true;
                            GeneralLeglist.Dock = DockStyle.None;
                            this.panel7.Controls.Add(GeneralLeglist);
                            panel8.SendToBack();
                            break;
                        case "25":
                            FrmReport GroupList = new FrmReport();
                            Globalvariable.frmName = "Groups Rpt List";
                            GroupList.TopLevel = false;
                            GroupList.Visible = true;
                            GroupList.Dock = DockStyle.None;
                            this.panel7.Controls.Add(GroupList);
                            panel8.SendToBack();
                            break;
                        case "24":
                            FrmReport DiscountList = new FrmReport();
                            Globalvariable.frmName = "Discount List";
                            DiscountList.TopLevel = false;
                            DiscountList.Visible = true;
                            DiscountList.Dock = DockStyle.None;
                            this.panel7.Controls.Add(DiscountList);
                            panel8.SendToBack();
                            break;
                        case "23":
                            FrmReport TaxList = new FrmReport();
                            Globalvariable.frmName = "Tax List";
                            TaxList.TopLevel = false;
                            TaxList.Visible = true;
                            TaxList.Dock = DockStyle.None;
                            this.panel7.Controls.Add(TaxList);
                            panel8.SendToBack();
                            break;
                        case "22":
                            FrmReport EmpList = new FrmReport();
                            Globalvariable.frmName = "Employee List";
                            EmpList.TopLevel = false;
                            EmpList.Visible = true;
                            EmpList.Dock = DockStyle.None;
                            this.panel7.Controls.Add(EmpList);
                            panel8.SendToBack();
                            break;
                        case "21":
                            FrmReport GuestList = new FrmReport();
                            Globalvariable.frmName = "Guest List";
                            GuestList.TopLevel = false;
                            GuestList.Visible = true;
                            GuestList.Dock = DockStyle.None;
                            this.panel7.Controls.Add(GuestList);
                            panel8.SendToBack();
                            break;
                        case "20":
                            FrmReport ItemcodingListReport = new FrmReport();
                            Globalvariable.frmName = "Item Coding List";
                            ItemcodingListReport.TopLevel = false;
                            ItemcodingListReport.Visible = true;
                            ItemcodingListReport.Dock = DockStyle.None;
                            this.panel7.Controls.Add(ItemcodingListReport);
                            panel8.SendToBack();
                            break;
                        case "19":
                            FrmReport PartyListReport = new FrmReport();
                            Globalvariable.frmName = "Party List";
                            PartyListReport.TopLevel = false;
                            PartyListReport.Visible = true;
                            PartyListReport.Dock = DockStyle.None;
                            this.panel7.Controls.Add(PartyListReport);
                            panel8.SendToBack();
                            break;
                        case "18":
                            FrmReport RoomListReport = new FrmReport();
                            Globalvariable.frmName = "Room List";
                            RoomListReport.TopLevel = false;
                            RoomListReport.Visible = true;
                            RoomListReport.Dock = DockStyle.None;
                            this.panel7.Controls.Add(RoomListReport);
                            panel8.SendToBack();
                            break;
                        case "17":
                            Globalvariable.frmName = "MenuItem";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmSubItemMaster>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            //Globalvariable.frmName = "ItemName";
                            //Models.Common.FormManagement.Instance.ProcessFormOpening<FrmMenuItem>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                            //FrmMenuItem itmname = new FrmMenuItem();
                            //Globalvariable.frmName = "ItemName";
                            //itmname.TopLevel = false;
                            //itmname.Visible = true;
                            //itmname.Dock = DockStyle.None;
                            //this.panel7.Controls.Add(itmname);
                            //panel8.SendToBack();
                            //break;
                        case "16":

                            Globalvariable.frmName = "MenuGroup";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmItemMaster>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            //Globalvariable.frmName = "ItemGroup";
                            //Models.Common.FormManagement.Instance.ProcessFormOpening<FrmItemGroup>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                            //FrmItemGroup itmgroup = new FrmItemGroup();
                            //Globalvariable.frmName = "ItemGroup";
                            //itmgroup.TopLevel = false;
                            //itmgroup.Visible = true;
                            //itmgroup.Dock = DockStyle.None;
                            //this.panel7.Controls.Add(itmgroup);
                            //panel8.SendToBack();
                            //break;
                        case "15":
                            Globalvariable.frmName = "CreditSale";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmPurchase>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                            //FrmPurchase creditsale = new FrmPurchase();
                            //Globalvariable.frmName = "CreditSale";
                            //creditsale.TopLevel = false;
                            //creditsale.Visible = true;
                            //creditsale.Dock = DockStyle.None;
                            //this.panel7.Controls.Add(creditsale);
                            //panel8.SendToBack();
                            //break;
                        case "14":
                            Globalvariable.frmName = "CashSale";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmPurchase>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                            //FrmPurchase cashsale = new FrmPurchase();
                            //Globalvariable.frmName = "CashSale";
                            //cashsale.TopLevel = false;
                            //cashsale.Visible = true;
                            //cashsale.Dock = DockStyle.None;
                            //this.panel7.Controls.Add(cashsale);
                            //panel8.SendToBack();
                            //break;
                        case "13":
                            Globalvariable.frmName = "SaleReturn";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmPurchase>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                            //FrmPurchase salereturn = new FrmPurchase();
                            //Globalvariable.frmName = "SaleReturn";
                            //salereturn.TopLevel = false;
                            //salereturn.Visible = true;
                            //salereturn.Dock = DockStyle.None;
                            //this.panel7.Controls.Add(salereturn);
                            //panel8.SendToBack();
                            //break;
                        case "12":
                            Globalvariable.frmName = "CreditPurchase";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmPurchase>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                            //FrmPurchase creditpurchase = new FrmPurchase();
                            //Globalvariable.frmName = "CreditPurchase";
                            //creditpurchase.TopLevel = false;
                            //creditpurchase.Visible = true;
                            //creditpurchase.Dock = DockStyle.None;
                            //this.panel7.Controls.Add(creditpurchase);
                            //panel8.SendToBack();
                            //break;
                        case "11":
                            Globalvariable.frmName = "CashPurchase";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmPurchase>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                            //FrmPurchase cashpurchase = new FrmPurchase();
                            //Globalvariable.frmName = "CashPurchase";
                            //cashpurchase.TopLevel = false;
                            //cashpurchase.Visible = true;
                            //cashpurchase.Dock = DockStyle.None;
                            //this.panel7.Controls.Add(cashpurchase);
                            //panel8.SendToBack();
                            //break;
                        case "10":
                            Globalvariable.frmName = "PurchaseReturn";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmPurchase>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                            //FrmPurchase purchasereturn = new FrmPurchase();
                            //Globalvariable.frmName = "PurchaseReturn";
                            //purchasereturn.TopLevel = false;
                            //purchasereturn.Visible = true;
                            //purchasereturn.Dock = DockStyle.None;
                            //this.panel7.Controls.Add(purchasereturn);
                            //panel8.SendToBack();
                            //break;
                        case "8":
                            Globalvariable.frmName = "BankLocation";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmBankLocation>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                            //FrmBankLocation bankLocat = new FrmBankLocation();
                            //Globalvariable.frmName = "BankLocation";
                            //bankLocat.TopLevel = false;
                            //bankLocat.Visible = true;
                            //bankLocat.Dock = DockStyle.None;
                            //this.panel7.Controls.Add(bankLocat);
                            //panel8.SendToBack();
                            //break;
                        case "7":
                            Globalvariable.frmName = "PartyBank";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmPartyBank>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                            //FrmPartyBank partyBank = new FrmPartyBank();
                            //Globalvariable.frmName = "PartyBank";
                            //partyBank.TopLevel = false;
                            //partyBank.Visible = true;
                            //partyBank.Dock = DockStyle.None;
                            //this.panel7.Controls.Add(partyBank);
                            //panel8.SendToBack();
                            //break;
                        case "6":
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmEmployeeInformation>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                            //FrmEmployeeInformation frmempInfo = new FrmEmployeeInformation();
                            //frmempInfo.TopLevel = false;
                            //frmempInfo.Visible = true;
                            //frmempInfo.Dock = DockStyle.None;
                            //this.panel7.Controls.Add(frmempInfo);
                            //panel8.SendToBack();
                            //break;
                        case "5":
                            Globalvariable.frmName = "EmployeeDesig";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmEmployeeDesig>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                            //FrmEmployeeDesig frmEmpDesig = new FrmEmployeeDesig();
                            //Globalvariable.frmName = "EmployeeDesig";
                            //frmEmpDesig.TopLevel = false;
                            //frmEmpDesig.Visible = true;
                            //frmEmpDesig.Dock = DockStyle.None;
                            //this.panel7.Controls.Add(frmEmpDesig);
                            //panel8.SendToBack();
                            //break;
                        case "4":
                            Globalvariable.frmName = "SupplierMaster";
                            Globalvariable.PartyLedgertype = "4";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmSupplierMaster>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                            //FrmSupplierMaster frmsuppdeb = new FrmSupplierMaster();
                            //Globalvariable.frmName = "SupplierMaster";
                            //Globalvariable.PartyLedgertype = "4";
                            //frmsuppdeb.TopLevel = false;
                            //frmsuppdeb.Visible = true;
                            //frmsuppdeb.Dock = DockStyle.None;
                            //this.panel7.Controls.Add(frmsuppdeb);
                            //panel8.SendToBack();
                            //break;
                        case "3":
                            Globalvariable.frmName = "SupplierMaster";
                            Globalvariable.PartyLedgertype = "3";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<FrmSupplierMaster>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                            //FrmSupplierMaster frmsuppcre = new FrmSupplierMaster();
                            //Globalvariable.frmName = "SupplierMaster";
                            //Globalvariable.PartyLedgertype = "3";
                            //frmsuppcre.TopLevel = false;
                            //frmsuppcre.Visible = true;
                            //frmsuppcre.Dock = DockStyle.None;
                            //this.panel7.Controls.Add(frmsuppcre);
                            //panel8.SendToBack();
                            //break;
                        case "2":

                            Globalvariable.frmName = "RoomMaster";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<RoomMaster>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                            //RoomMaster roommast = new RoomMaster();
                            //Globalvariable.frmName = "RoomMaster";
                            //roommast.TopLevel = false;
                            //roommast.Visible = true;
                            //roommast.Dock = DockStyle.None;
                            //this.panel7.Controls.Add(roommast);
                            //panel8.SendToBack();

                            //break;
                        case "1":
                            Globalvariable.frmName = "RoomCategry";
                            Models.Common.FormManagement.Instance.ProcessFormOpening<RoomCategory>(ref panel7, ref panel8, treeView1.SelectedNode.Text);
                            break;

                            //RoomCategory roomcat = new RoomCategory();
                            //Globalvariable.frmName = "RoomCategry";
                            //roomcat.TopLevel = false;
                            //roomcat.Visible = true;
                            //roomcat.Dock = DockStyle.None;
                            //this.panel7.Controls.Add(roomcat);
                            //panel8.SendToBack();

                            //break;
                    }
                }
                #endregion
                //else if (Globalvariable.nodeLevel == 3)
                //{
                //    switch (Treetxt)
                //    {


                //    }
                //}
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private bool CheckForm(Form form)
        {
            form = Application.OpenForms[form.Text];
            if (form != null)
                return true;
            else
                return false;
        }

        private void PicBox_ExitLogo_Click(object sender, EventArgs e)

        {
            MAINFORM frm = new MAINFORM();
            this.Hide();
            frm.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MAINFORM frm = new MAINFORM();
            this.Hide();
            frm.Show();

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // FrmRoomBooking frmRoomBook = new FrmRoomBooking();
            // frmRoomBook.Show();
        }

        private void picLogout_Click(object sender, EventArgs e)
        {

        }

        private void picLogout_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MAINFORM frm = new MAINFORM();
            this.Hide();
            frm.Show();
        }

        private void TimerFooterMarquee_Tick(object sender, EventArgs e)
        {
            lblFooterMarquee.Text = RightText(lblFooterMarquee.Text, (lblFooterMarquee.Text).Length - 1) + LeftText(lblFooterMarquee.Text, 1);
            if (TimerFooterMarquee.Tag.ToString() == "100")
            {
                lblStatus.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
            }
            else
            {
                TimerFooterMarquee.Tag = Convert.ToInt32(TimerFooterMarquee.Tag) + 100;
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //try
            //{
            //    Treetxt = e.Node.Tag.ToString();
            //    Globalvariable.treeTextContain = Treetxt;
            //    Globalvariable.nodeLevel = e.Node.Level;
            //    TreeNodeFill(Treetxt);
            //}
            //catch (Exception ex)
            //{
            //    string str = "Message:" + ex.Message;
            //    MessageBox.Show(str, "Error Message");
            //}
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                Treetxt = e.Node.Tag.ToString();
                Globalvariable.treeTextContain = Treetxt;
                Globalvariable.nodeLevel = e.Node.Level;
                TreeNodeFill(Treetxt);
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeNode node = treeView1.SelectedNode;
        }

        private void treeView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {

                Treetxt = treeView1.SelectedNode.Tag.ToString();
                Globalvariable.treeTextContain = Treetxt;
                Globalvariable.nodeLevel = treeView1.SelectedNode.Level;
                if (e.KeyChar == (char)Keys.Enter)
                {
                    TreeNodeFill(Treetxt);
                }
                else if (e.KeyChar == (char)Keys.S)
                {
                    //Stock.MDIForm stm = new Stock.MDIForm();
                    //stm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void PicBox_Logout_Click(object sender, EventArgs e)
        {
            mod.DataBaseBackup("Logout");
            //MAINFORM frm = new MAINFORM();
            //this.Hide();
            //frm.Show();
            // this.Close();   
            Application.Exit();
        }

        private void linkLabelLodge_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //FrmLodgeTreeview frmLodg = new FrmLodgeTreeview();
            Globalvariable.ModuleNm = "Lodge Management System";
            Globalvariable.flagchar = "L";
            Loadfun();
            // Globalvariable.OnApplicationLodge = "ON-L";
            //this.Hide();
            //frmLodg.Show();
        }

        private void linkLblMaterialmanagement_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // FrmLodgeTreeview frmLodg = new FrmLodgeTreeview();
            Globalvariable.ModuleNm = "Material Management System";
            Globalvariable.flagchar = "M";
            Loadfun();
            // Globalvariable.OnApplicationMaterialManage= "ON-M";
            //this.Hide();
            //frmLodg.Show();      
        }

        private void linklblAcctManagement_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // FrmLodgeTreeview frmLodg = new FrmLodgeTreeview();
            Globalvariable.ModuleNm = "Accounts Management System";
            Globalvariable.flagchar = "A";
            Loadfun();
            // Globalvariable.OnApplicationAccount = "ON-A";
            //this.Hide();
            //frmLodg.Show();
        }

        private void lblFinanYear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FrmChangeFinanYear finanYear = new FrmChangeFinanYear();
                finanYear.ShowDialog();
                lblComNm.Text = "";
                finanYear.Hide();
                companyName();
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void linkLablChngUser_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Models.Common.FormManagement.Instance.ProcessModelFormOpening<FrmChangeUser>(ref panel7);
                //FrmChangeUser changeuser = new FrmChangeUser();
                //changeuser.TopLevel = false;
                //changeuser.Visible = true;
                //changeuser.Dock = DockStyle.None;
                //this.panel7.Controls.Add(changeuser);
                //changeuser.ShowDialog();
                treeView1.Nodes.Clear();
                this.FrmLodgeTreeview_Load(sender, e);
                //FillTreeview();
                //lblusernm.Text = Globalvariable.username;
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            TreeViewHitTestInfo info = treeView1.HitTest(e.X, e.Y);

            // Ensure that the user actually clicked on a node (there are lots of areas
            // over which they could potentially click that do not contain a node)
            if ((info.Node != null) && (info.Node == treeView1.SelectedNode))
            {
                // The user clicked on the currently-selected node,
                // so refresh the TreeView
                // . . . 
            }
        }

        private void lblRestaurant_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Globalvariable.ModuleNm = "Restaurant Management System";
            Globalvariable.flagchar = "R";
            Loadfun();
        }

        private void KOTBilling_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Globalvariable.ModuleNm = "Restaurant Management System";
            Globalvariable.flagchar = "R";
            Loadfun();
            //KOTBilling frmkot = new KOTBilling();
            //this.Hide();
            //frmkot.ShowDialog();

        }

    }
}

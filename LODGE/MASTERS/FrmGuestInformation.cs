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
using AForge.Video.DirectShow;

namespace ASPL.LODGE.MASTERS
{
    public partial class FrmGuestInformation : Form
    {
        Timer tmrNew;
        bool isDeviceStarted;
        List<Image> lstImages;
        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCaptureDevice;

        AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();
        AutoCompleteStringCollection CitynamesCollection = new AutoCompleteStringCollection();

        Design ObjDesign = new Design();
        public FrmGuestInformation()
        {
            InitializeComponent();
        }
        public static SqlDataReader srchdr;
        public static SqlDataAdapter srchda;
        DataSet ds = new DataSet();
        Module mod = new Module();
        Connection con = new Connection();
        SqlCommand command = new SqlCommand();
        DataGridViewTextBoxEditingControl tb;
        SqlDataReader dr, dr1;
        string fileName, filc, strsrch, StrUpdate;
        int rowNo, colNo, selectrow;
        string firstCol = "customer_id";
        string secondCol = "customer_name";
        string tableName = "tbGuestInformation";
        string Deleted = "Deleted";
        string str, str_docID;
        public static int searchNo;
        Boolean stretchImage = false;
        string StateIDVar = "0", CityIDVar = "0";
        int pic_left;
        int pic_top;
        int pic_height;
        int pic_width;

        public string searchChange(string searchStr)
        {
            try
            {
                if (Globalvariable.searchNo == 1)
                {
                    strsrch = "SELECT supplier_name,supplier_id FROM tbSupplierMaster WHERE deleted='N' AND cust_type='D' OR supplier_name LIKE '" +
                           searchStr + "%' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY supplier_id";
                }
                else if (Globalvariable.searchNo == 2)
                {
                    strsrch = "SELECT supplier_name,supplier_id FROM tbSupplierMaster WHERE deleted='N' AND cust_type='D' AND supplier_name LIKE '" +
                           searchStr + "%' AND Branchcode='" + Globalvariable.bcode + "' ORDER BY supplier_id";
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
            return strsrch;
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
        private void FrmGuestInformation_Load(object sender, EventArgs e)
        {
            LoadEvent();
        }


        public void autoAddState()
        {

            SqlDataReader dReader;

            SqlCommand cmd = new SqlCommand("Select distinct StateName from tbStateMaster order by StateName asc", con.connect());

            dReader = cmd.ExecuteReader();
            if (dReader.HasRows == true)
            {
                while (dReader.Read())
                    namesCollection.Add(dReader["StateName"].ToString());
            }
            else
            {
                MessageBox.Show("Data not found");
            }
            txtState.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtState.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtState.AutoCompleteCustomSource = namesCollection;
        }

        public void autoAddCity()
        {

            SqlDataReader dReader;

            SqlCommand cmd = new SqlCommand("Select distinct CityName from tbCityMaster order by CityName asc", con.connect());

            dReader = cmd.ExecuteReader();
            if (dReader.HasRows == true)
            {
                while (dReader.Read())
                    CitynamesCollection.Add(dReader["CityName"].ToString());
            }
            else
            {
                MessageBox.Show("Data not found");
            }
            dReader.Close();

            txtPlace.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtPlace.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtPlace.AutoCompleteCustomSource = CitynamesCollection;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + @"\images";

            DirectoryInfo thisFolder = new DirectoryInfo(path);

            if (thisFolder.Exists)
            {
                foreach (string file in Directory.GetFiles(path))
                {
                    File.Delete(file);
                }
                thisFolder.Delete();
            }

            Close();
        }
        public void ADD()
        {
            try
            {
                mod.addBtnClick(this, txtcustcode, tableName, firstCol, BtnSave, BtnDelete, txtCustName);
                mod.UserAccessibillityMaster("Guest Information", BtnAdd, BtnSave, BtnDelete);
                //itemMastGrid.Rows.Clear();
                //itemMastGrid.Enabled = true;
                Models.Common.FormManagement.Instance.ClearAllTextBox(ref pnlMain);
                foreach (Control c in pnlMain.Controls)
                {
                    if (c is DateTimePicker)
                    {
                        DateTimePicker d = (DateTimePicker)c;
                        d.Value = DateTime.Today;
                    }
                }
                lblLegName.Text = "";
                ClearImages();
                LoadListView();
                txtcustcode.Text = "" + mod.GetMaxNum(tableName, firstCol);
                Nationality();
                cmbPrefix.Focus();
                picImage.Image = null;
                lblSucessMsg.Text = "";
                txtCustName.Focus();


                label1.Text = "Search by Name :";
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }
        public void LoadEvent()
        {
            try
            {

                btnRemoveDocument.Enabled = false;

                tmrNew = new Timer();
                tmrNew.Interval = 500;
                tmrNew.Tick += TmrNew_Tick;

                lstImages = new List<Image>();


                #region Camera_Settings
                isDeviceStarted = false;
                filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[0].MonikerString);
                videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
                #endregion
            }
            catch
            {

            }

            try
            {
                ObjDesign.FormDesign(this, DtgridGuestInfo);
                //ObjDesign.FormDesign(this, itemMastGrid);
                autoAddCity();
                autoAddState();
                mod.FormLoad(this, tableName, firstCol, secondCol, Deleted, "N", "N", DtgridGuestInfo, txtsearch, ChkDeleted, "Description", "Code");
                txtcustcode.Enabled = false;
                Prefix();
                FillDetails();
                mod.UserAccessibillityMaster("Guest Information", BtnAdd, BtnSave, BtnDelete);
                //if (itemMastGrid.Rows.Count > 0)
                //{
                //    lblRemoveImage.Visible = true;
                //}
                //else
                //{
                //    lblRemoveImage.Visible = false;
                //}
                Globalvariable.SearchChangeVariable = "SrchByName";
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void TmrNew_Tick(object sender, EventArgs e)
        {
            if (isDeviceStarted)
            {
                tmrNew.Enabled = false;
                btnAddMore.Text = "Capture";
                btnAddMore.Enabled = true;

            }
        }

        private void VideoCaptureDevice_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {

            if (!isDeviceStarted)
            {
                isDeviceStarted = true;
            }

            picImage.Image = (Bitmap)eventArgs.Frame.Clone();
        }



        public void fillindexcom()
        {
            cmbNationality.Items.Clear();
            cmbNationality.Items.Insert(0, "INDIAN");
            cmbNationality.Items.Insert(1, "NRI");
        }
        public void FillDetails()
        {
            try
            {
                txtImageName.Text = "";
                if (ChkDeleted.Checked == true)
                {
                    mod.chkDeletedTrue(this, BtnAdd, BtnSave, BtnDelete, DtgridGuestInfo, txtsearch);
                    txtsearch.Focus();
                }
                else
                {
                    mod.chkDeletedFalse("Guest Information", this, BtnAdd, BtnSave, BtnDelete, DtgridGuestInfo, txtsearch);
                }
                if (DtgridGuestInfo.Rows.Count > 0)
                {
                    int i;
                    //if (StrUpdate == "U")
                    //{
                    //    i = selectrow;
                    //}
                    //else
                    //{
                    //    i = DtgridGuestInfo.SelectedCells[0].RowIndex;
                    //}

                    i = DtgridGuestInfo.SelectedCells[0].RowIndex;

                    if (DtgridGuestInfo.Rows[i].Cells[1].Value != null)
                    {
                        string ID = mod.Isnull(DtgridGuestInfo.Rows[i].Cells[1].Value.ToString(), "");
                        SqlDataReader dr = mod.GetSelectAllField(tableName, firstCol, ID, "");
                        while (dr.Read())
                        {
                            txtcustcode.Text = dr["Customer_id"].ToString();
                            txtcustcode.Enabled = false;
                            str = mod.Isnull(dr["Prefix"].ToString(), "Mr.").Trim();
                            switch (str)
                            {
                                case "Mr.":
                                    cmbPrefix.SelectedIndex = 0;
                                    break;
                                case "Mrs.":
                                    cmbPrefix.SelectedIndex = 1;
                                    break;
                                case "M/S.":
                                    cmbPrefix.SelectedIndex = 2;
                                    break;
                            }

                            txtCustName.Text = dr["Customer_name"].ToString();
                            txtLegID.Text = mod.Isnull(dr["Leger_id"].ToString(), "0");
                            str = "";
                            str = "SELECT * FROM tbSupplierMaster WHERE deleted='N' AND cust_type='D' AND " +
                            "supplier_id='" + txtLegID.Text + "'";
                            dr1 = mod.GetRecord(str);
                            if (dr1.HasRows)
                            {
                                dr1.Read();
                                lblLegName.Text = mod.Isnull(dr1["supplier_name"].ToString(), "");
                            }
                            else
                            {
                                lblLegName.Text = "";
                            }
                            dtpDateofBirth.Value = Convert.ToDateTime(dr["DOB"].ToString());
                            filc = Convert.ToString(dr["Nationality"]);
                            if (filc != "")
                            {
                                if (filc == "IND")
                                    cmbNationality.Items.Insert(0, "INDIAN");

                                else if (filc == "NRI")
                                    cmbNationality.Items.Insert(0, "NRI");
                            }
                            cmbNationality.SelectedIndex = 0;
                            fillindexcom();
                            txtAddress1.Text = mod.Isnull(dr["Address1"].ToString(), "-");
                            txtAddress2.Text = mod.Isnull(dr["Address2"].ToString(), "-");
                            txtAddress3.Text = mod.Isnull(dr["Address3"].ToString(), "-");
                            txtPin.Text = mod.Isnull(dr["Zip_id"].ToString(), "0");
                            txtMobile1.Text = mod.Isnull(dr["Mobile_no1"].ToString(), "0");
                            txtMobile2.Text = mod.Isnull(dr["Mobile_no2"].ToString(), "0");
                            txtEmailId.Text = mod.Isnull(dr["Email_id"].ToString(), "");
                            txtPancard.Text = mod.Isnull(dr["PanCard"].ToString(), "0");
                            txtPassportNo.Text = mod.Isnull(dr["PassportNo"].ToString(), "0");
                            dtpPass.Value = Convert.ToDateTime(dr["PassportIssue"].ToString());
                            dtpPassExp.Value = Convert.ToDateTime(dr["PassportExp"].ToString());
                            txtVisaNo.Text = mod.Isnull(dr["VisaNo"].ToString(), "0");
                            dtpVisa.Value = Convert.ToDateTime(dr["VisaIssue"].ToString());
                            dtpVisaExp.Value = Convert.ToDateTime(dr["VisaExp"].ToString());
                            string state_id = mod.Isnull(dr["State_id"].ToString(), "0");
                            if (state_id == "0")
                            {
                                txtState.Text = "";
                            }
                            else
                            {
                                str = "";
                                str = "SELECT StateName FROM tbStateMaster WHERE StateID='" + state_id + "'";
                                SqlDataReader drState = mod.GetRecord(str);
                                if (drState.HasRows)
                                {
                                    drState.Read();
                                    txtState.Text = mod.Isnull(drState["StateName"].ToString(), "");
                                }
                            }
                            string city_id = mod.Isnull(dr["City_id"].ToString(), "0");
                            if (city_id == "0")
                            {
                                txtPlace.Text = "";
                            }
                            else
                            {
                                str = "SELECT CityName FROM tbCityMaster WHERE CityID='" + city_id + "'";
                                SqlDataReader drCity = mod.GetRecord(str);
                                if (drCity.HasRows)
                                {
                                    drCity.Read();
                                    txtPlace.Text = mod.Isnull(drCity["CityName"].ToString(), "");
                                }
                            }
                            ReadFromDatabase();//for itemdatagrid fill                           
                        }
                    }
                    txtsearch.Focus();
                }
                else
                {
                    mod.txtclear(this.Controls);
                    Prefix();
                    Nationality();
                    BtnDelete.Text = "RESET";
                }

            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }

        }

        //private Bitmap GetBitmap(object dbImageObject)
        //{
        //    MemoryStream ms = new MemoryStream((byte[])dbImageObject);
        //    return new Bitmap(ms);
        //}

        public void ReadFromDatabase()
        {
            try
            {
                picImage.Image = null;
                picImage.Invalidate();
                picImage.ResetText();
                ClearImages();
                var images = Models.Common.DbConnectivity.Instance.GetCustomerDocuments(txtcustcode.Text);
                lstImages.AddRange(images.ToArray());
                LoadListView();
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
            //    str = "SELECT cust_id, document_detail FROM SaveImageTest";
            //    dr = mod.GetRecord(str);
            //    if (dr.HasRows)
            //    {
            //        while (dr.Read())
            //        {
            //            // read in using GetValue and cast to byte array                     
            //            itemMastGrid.Rows.Add();
            //            itemMastGrid.Rows[0].Cells[1].Value = dr.GetValue(0);
            //            itemMastGrid.Rows[0].Cells[2].Value = GetBitmap(dr.GetValue(1));
            //            itemMastGrid.Rows[0].Cells[5].Value = "Browse";
            //        }
            //    }
            //    else
            //    {
            //        itemMastGrid.Rows.Clear();
            //        itemMastGrid.Enabled = true;
            //    }
            //    return;
            //try
            //{
            //    picImage.Image = null;
            //    picImage.Invalidate();
            //    picImage.ResetText();
            //    //itemMastGrid.Rows.Clear();
            //    //itemMastGrid.Enabled = true;
            //    ClearImages();

            //    //int i = 0;
            //    //string path = Application.StartupPath + @"\images";

            //    //if (!Directory.Exists(path))
            //    //{
            //    //    Directory.CreateDirectory(path);
            //    //}
            //    //foreach (string file in Directory.GetFiles(path))
            //    //{
            //    //    File.Delete(file);
            //    //}
            //    ClearImages();

            //    //str = "";
            //    //str = "SELECT document_detail FROM tbCustomerIdentityDetail WHERE cust_id = " + mod.Isnull(txtcustcode.Text, "0") + " AND Branchcode='" + Globalvariable.bcode + "'";
            //    //dr = mod.GetRecord(str);
            //    var images = Models.Common.DbConnectivity.Instance.GetCustomerDocuments(txtcustcode.Text);
            //    lstImages.AddRange(images.ToArray());
            //    LoadListView(lstImages.ToArray());
            //    //if (dr.HasRows)
            //    //{
            //    //    while (dr.Read())
            //    //    {
            //    //        lstImages.Add(GetBitmap(dr.GetValue(0)));
            //    //    }
            //    //}
            //    //else
            //    //{
            //    //    ClearImages();
            //    //}
            //}
            //catch (Exception ex)
            //{
            //    string str = "Message:" + ex.Message;
            //    MessageBox.Show(str, "Error Message");
            //}
        }
        private Image GetCopyImage(string path)
        {
            using (Image im = Image.FromFile(path))
            {
                Bitmap bm = new Bitmap(im);
                return bm;
            }
        }
        private void txtsearch_Enter(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.Yellow;
        }
        private void cmbPrefix_Enter(object sender, EventArgs e)
        {

            cmbPrefix.BackColor = Color.Yellow;
        }
        public void Prefix()
        {
            cmbPrefix.Items.Clear();
            cmbPrefix.Items.Insert(0, "Mr.");
            cmbPrefix.Items.Insert(1, "Mrs.");
            cmbPrefix.Items.Insert(2, "M/S");
            cmbPrefix.SelectedIndex = 0;
        }
        public void Nationality()
        {
            cmbNationality.Items.Clear();
            cmbNationality.Items.Insert(0, "INDIAN");
            cmbNationality.Items.Insert(1, "NRI");
            cmbNationality.SelectedIndex = 0;
        }
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            ADD();
        }
        private void FrmGuestInformation_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Control nextTab;
                if (e.KeyChar == (char)Keys.Enter)
                {
                    nextTab = ((Control)sender);
                    if (ActiveControl.Name == "txtsearch" && DtgridGuestInfo.Rows.Count == 0 && e.KeyChar == (char)Keys.Enter)
                    {
                        txtsearch.Focus();
                    }
                    else
                    {
                        if (ActiveControl.Name == "txtsearch" && DtgridGuestInfo.Rows.Count == 0)
                        {
                            nextTab = BtnAdd;
                        }
                        else if (ActiveControl.Name == "txtLegID")
                        {
                            if (lblLegName.Text == "" && txtLegID.Text != "0")
                            {
                                txtLegID.Focus();
                            }
                            else
                            {
                                nextTab = GetNextControl(ActiveControl, true);
                            }
                        }
                        else
                        {
                            if (BtnDelete.Text != "RECALL")
                            {
                                nextTab = GetNextControl(ActiveControl, true);

                            }
                        }
                        nextTab.Focus();
                        //if (ActiveControl.Name == "itemMastGrid" && DtgridGuestInfo.RowCount > 0)
                        //{
                        //    if (itemMastGrid.Rows.Count > 0)
                        //    {
                        //        //itemMastGrid.Rows.Clear();
                        //        itemMastGrid.Enabled = true;
                        //        rowNo = itemMastGrid.Rows.Count - 1;
                        //        if (itemMastGrid.Rows[rowNo].Cells[3].Value != null)
                        //        {
                        //            foreach (DataGridViewRow row in itemMastGrid.Rows)
                        //            {
                        //                DataGridViewButtonCell btn = (DataGridViewButtonCell)row.Cells[5];
                        //                btn.ReadOnly = false;
                        //            }
                        //            itemMastGrid.Rows.Add();
                        //            rowNo = itemMastGrid.Rows.Count - 1;
                        //            itemMastGrid.Rows[rowNo].Cells[0].Value = itemMastGrid.Rows.Count;
                        //            itemMastGrid.Rows[rowNo].Cells[5].Value = "Start Webcam";
                        //            itemMastGrid.CurrentCell = itemMastGrid[5, rowNo];
                        //            itemMastGrid.BeginEdit(true);

                        //        }
                        //        else
                        //        {

                        //            itemMastGrid.Rows[rowNo].Cells[0].Value = itemMastGrid.Rows.Count;
                        //            itemMastGrid.Rows[rowNo].Cells[5].Value = "Start Webcam";
                        //            itemMastGrid.CurrentCell = itemMastGrid[5, rowNo];
                        //            itemMastGrid.BeginEdit(true);
                        //        }

                        //    }
                        //    else if (itemMastGrid.Rows.Count == 0)
                        //    {
                        //        itemMastGrid.Enabled = true;
                        //        itemMastGrid.Rows.Clear();
                        //        itemMastGrid.Rows.Add();
                        //        rowNo = itemMastGrid.Rows.Count - 1;
                        //        itemMastGrid.Rows[rowNo].Cells[0].Value = itemMastGrid.Rows.Count;
                        //        itemMastGrid.Rows[rowNo].Cells[5].Value = "Start Webcam";
                        //        itemMastGrid.CurrentCell = itemMastGrid[5, rowNo];
                        //        itemMastGrid.BeginEdit(true);

                        //    }
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }
        private void DtgridGuestInfo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                mod.gridKeyDown(e, BtnDelete, txtsearch, DtgridGuestInfo, this);
                FillDetails();

                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
                {
                    DtgridGuestInfo.Focus();
                }
                if (e.KeyCode == Keys.Enter && ChkDeleted.Checked == true && DtgridGuestInfo.Rows.Count > 0)
                    BtnDelete.Focus();
                else if (e.KeyCode == Keys.Enter && ChkDeleted.Checked == true && DtgridGuestInfo.Rows.Count == 0)
                    BtnExit.Focus();
                lblSucessMsg.Text = "";

            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }
        private void itemMastGrid_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                e.SuppressKeyPress = true;
                if (e.KeyCode == Keys.Enter)
                    e.Handled = true;

            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }
        private void itemMastGrid_GotFocus(object sender, EventArgs e)
        {
            TextBox obj = sender as TextBox;
            obj.BackColor = Color.Yellow;
        }
        private void itemMastGrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

            e.Control.PreviewKeyDown -= Control_PreviewKeyDown;
            e.Control.PreviewKeyDown += new PreviewKeyDownEventHandler(Control_PreviewKeyDown);

            e.Control.GotFocus -= itemMastGrid_GotFocus;
            e.Control.GotFocus += new EventHandler(itemMastGrid_GotFocus);

            tb = (DataGridViewTextBoxEditingControl)e.Control;
        }
        private void Control_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //try
            //{
            //    if (e.KeyCode == Keys.Enter)
            //    {
            //        if (itemMastGrid.Rows.Count > 0)
            //        {

            //            itemMastGrid.Enabled = true;
            //            rowNo = itemMastGrid.Rows.Count - 1;
            //            if (itemMastGrid.Rows[rowNo].Cells[3].Value != null)
            //            {
            //                itemMastGrid.Rows.Add();
            //                rowNo = itemMastGrid.Rows.Count - 1;
            //                itemMastGrid.Focus();
            //                ActiveControl = itemMastGrid;
            //                DataGridViewButtonColumn btncolmn = new DataGridViewButtonColumn();
            //                itemMastGrid.Rows[rowNo].Cells[5].Value = "Start Webcam";

            //                itemMastGrid.CurrentCell = itemMastGrid[5, rowNo];
            //                itemMastGrid.BeginEdit(true);
            //            }
            //            else
            //            {
            //                itemMastGrid.CurrentCell = itemMastGrid[5, rowNo];
            //                itemMastGrid.BeginEdit(true);
            //            }

            //        }
            //        else if (itemMastGrid.Rows.Count == 0)
            //        {
            //            itemMastGrid.Rows.Add();
            //            rowNo = itemMastGrid.Rows.Count - 1;
            //            itemMastGrid.Rows[rowNo].Cells[5].Value = "Start Webcam";
            //            itemMastGrid.CurrentCell = itemMastGrid[5, rowNo];
            //            itemMastGrid.BeginEdit(true);

            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    string str = "Message:" + ex.Message;
            //    MessageBox.Show(str, "Error Message");
            //}
        }

        private void ChkDeleted_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                mod.chkDeletedChange(ChkDeleted, firstCol, secondCol, tableName, Deleted, "N", DtgridGuestInfo, txtsearch, "Customer Name", "Code");
                FillDetails();
                lblSucessMsg.Text = "";
                if (ChkDeleted.Checked == true)
                {
                    //itemMastGrid.Enabled = false;
                    picImage.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }


        private void DtgridGuestInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1 && e.RowIndex == -1)
                {
                    label1.Text = "Search by Code :";
                    Globalvariable.SearchChangeVariable = "SrchByCode";
                }
                else
                {
                    label1.Text = "Search by Name :";
                    Globalvariable.SearchChangeVariable = "SrchByName";
                }
                txtsearch.Focus();
                // FillDetails();
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                if (stretchImage == false)
                {
                    if (picImage.Size != this.Size)
                    {
                        pic_left = picImage.Left;
                        pic_top = picImage.Top;
                        pic_height = picImage.Height;
                        pic_width = picImage.Width;

                        picImage.BringToFront();
                        picImage.Top = 0;
                        picImage.Left = 0;
                        picImage.Height = pnlMain.Height;
                        picImage.Width = pnlMain.Width;
                        stretchImage = true;
                        //btnSaveImage.Visible = true;
                        //btnSaveImage.BringToFront();
                    }
                }
                else if (stretchImage == true)
                {
                    picImage.Left = pic_left;
                    picImage.Top = pic_top;
                    picImage.Height = pic_height;
                    picImage.Width = pic_width;
                    stretchImage = false;
                    // btnSaveImage.Visible = false;
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }
        //private void lblAddImage_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //    if (itemMastGrid.Rows.Count > 0)
        //    {
        //        itemMastGrid.Enabled = true;
        //        rowNo = itemMastGrid.Rows.Count - 1;
        //        if (itemMastGrid.Rows[rowNo].Cells[3].Value != null)
        //        {
        //            foreach (DataGridViewRow row in itemMastGrid.Rows)
        //            {
        //                DataGridViewButtonCell btn = (DataGridViewButtonCell)row.Cells[5];
        //                btn.ReadOnly = false;
        //            }
        //            itemMastGrid.Rows.Add();
        //            rowNo = itemMastGrid.Rows.Count - 1;
        //            itemMastGrid.Rows[rowNo].Cells[0].Value = itemMastGrid.Rows.Count;
        //            itemMastGrid.Rows[rowNo].Cells[5].Value = "Browse";
        //            itemMastGrid.CurrentCell = itemMastGrid[5, rowNo];
        //            itemMastGrid.BeginEdit(true);

        //        }
        //        else
        //        {

        //            itemMastGrid.Rows[rowNo].Cells[0].Value = itemMastGrid.Rows.Count;
        //            itemMastGrid.Rows[rowNo].Cells[5].Value = "Browse";
        //            itemMastGrid.CurrentCell = itemMastGrid[5, rowNo];
        //            itemMastGrid.BeginEdit(true);
        //        }

        //    }
        //    else if (itemMastGrid.Rows.Count == 0)
        //    {
        //        itemMastGrid.Enabled = true;
        //        itemMastGrid.Rows.Clear();
        //        itemMastGrid.Rows.Add();
        //        rowNo = itemMastGrid.Rows.Count - 1;
        //        itemMastGrid.Rows[rowNo].Cells[0].Value = itemMastGrid.Rows.Count;
        //        itemMastGrid.Rows[rowNo].Cells[5].Value = "Browse";
        //        itemMastGrid.CurrentCell = itemMastGrid[5, rowNo];
        //        itemMastGrid.BeginEdit(true);
        //    }
        //    }
        //    catch (Exception ex)
        //    {
        //        string str = "Message:" + ex.Message;
        //        MessageBox.Show(str, "Error Message");
        //    }
        //}
        private void lblRemoveImage_Click(object sender, EventArgs e)
        {
            //string strid, docid;
            //    strid = Convert.ToString(itemMastGrid.Rows[rowNo].Cells[1].Value);
            //    docid = Convert.ToString(itemMastGrid.Rows[rowNo].Cells[4].Value);
            //    RemoveFromDatabase(strid, docid);

            //    if (itemMastGrid.RowCount > 1)
            //    {
            //        rowNo = itemMastGrid.RowCount - 1;
            //    }
            //    else
            //    {
            //        BtnSave.Focus();
            //    }
        }

        public void RemoveFromDatabase(string id, string docid)
        {
            try
            {
                Connection con = new Connection();
                SqlConnection connection = con.connect();
                SqlCommand command = new SqlCommand();
                str = "";
                str = "DELETE FROM tbCustomerIdentityDetail WHERE cust_id='" + txtcustcode.Text +
                    "' AND file_name='" + id + "' AND document_id='" + docid + "' AND Branchcode='" + Globalvariable.bcode +
                    "' AND  user_id='" + Globalvariable.usercd + "'";
                command.CommandText = str;
                command.ExecuteNonQuery();
                ReadFromDatabase();
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }
        private void btnSaveImage_Click(object sender, EventArgs e)
        {
            picImage.Image.Save(@"D:\\" + mod.Isnull(txtImageName.Text, "Image1.bmp").ToString() + "", System.Drawing.Imaging.ImageFormat.Bmp);
            MessageBox.Show("Image saved to D:\\" + mod.Isnull(txtImageName.Text, "Image1.bmp").ToString() + "", "Message box", MessageBoxButtons.OK);
        }
        private void txtCustName_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        private void txtCustName_Enter(object sender, EventArgs e)
        {
            txtCustName.BackColor = Color.Yellow;
        }
        private void txtLegID_Enter(object sender, EventArgs e)
        {
            txtLegID.BackColor = Color.Yellow;
        }



        private void cmbNationality_Enter(object sender, EventArgs e)
        {
            cmbNationality.BackColor = Color.Yellow;
        }

        private void txtAddress2_Enter(object sender, EventArgs e)
        {
            txtAddress2.BackColor = Color.Yellow;
        }

        private void txtAddress3_Enter(object sender, EventArgs e)
        {
            txtAddress3.BackColor = Color.Yellow;
        }

        private void txtCity_Enter(object sender, EventArgs e)
        {
            txtPlace.BackColor = Color.Yellow;
        }

        private void txtPin_Enter(object sender, EventArgs e)
        {
            txtPin.BackColor = Color.Yellow;
        }

        private void txtPhonNo_Enter(object sender, EventArgs e)
        {
            txtMobile1.BackColor = Color.Yellow;
        }

        private void txtMobileNo_Enter(object sender, EventArgs e)
        {
            txtMobile2.BackColor = Color.Yellow;
        }

        private void txtEmailId_Enter(object sender, EventArgs e)
        {
            txtEmailId.BackColor = Color.Yellow;
        }


        public void LegID()
        {
            try
            {
                if (txtLegID.Text.Trim().Equals(""))
                {
                    lblLegName.Text = "";
                }
                else
                {
                    dr = mod.GetRecord("SELECT supplier_name FROM tbSupplierMaster WHERE cust_type='D' AND deleted='N' AND Branchcode='" + Globalvariable.bcode +
                          "' AND supplier_id='" + txtLegID.Text + "'");
                    if (dr.HasRows)
                    {
                        dr.Read();
                        lblLegName.Text = dr["supplier_name"].ToString();
                    }
                    else if (txtLegID.Text != "0")
                    {
                        txtLegID.Text = "";
                        lblLegName.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }


        private void txtLegID_KeyDown(object sender, KeyEventArgs e)
        {

            Globalvariable.searchNo = 1;
            var returnForm = Models.Common.FormManagement.Instance.SearchFormOnKeyDown(ref txtLegID, e, "FrmGuestInformation", true);
            if (returnForm == null)
            {
                return;
            }

            txtLegID.Text = returnForm.codeselected;
            lblLegName.Text = returnForm.descselected;

            return;
            //try
            //{
            //    if (txtLegID.Text == "" && e.KeyCode == Keys.Enter)
            //    {
            //        Globalvariable.searchNo = 1;
            //        KeysConverter kc = new KeysConverter();
            //        string key = kc.ConvertToString(e.KeyCode);
            //        FrmSearch srch = new FrmSearch();
            //        //str = "SELECT supplier_name,supplier_id FROM tbSupplierMaster WHERE deleted='N' AND Branchcode='" + Globalvariable.bcode + "' and cust_type='D' ORDER BY supplier_id";
            //        srchda = mod.SearchFrmQuery("tbSupplierMaster", "supplier_id", "supplier_name");
            //        srch.val = key;
            //        srch.fillbackgridSrch(srchda, "FrmGuestInformation");
            //        ds.Clear();
            //        srchda.Fill(ds);
            //        if (ds.Tables[0].Rows.Count > 0)
            //            srch.ShowDialog();
            //        txtLegID.Text = srch.codeselected;
            //        lblLegName.Text = srch.descselected;
            //        if (srch.codeselected == null)
            //        {
            //            txtLegID.Focus();
            //        }
            //        else
            //        {
            //            LegID();
            //        }
            //        Globalvariable.searchNo = 0;
            //        if (lblLegName.Text != "")
            //        {
            //            dtpDateofBirth.Focus();
            //        }
            //        else
            //        {
            //            txtLegID.Focus();
            //        }
            //    }
            //    else if (e.KeyCode == Keys.Enter && txtLegID.Text != "" && txtLegID.Text != "0")
            //    {
            //        LegID();
            //    }
            //    else if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete || txtLegID.Text == "0")
            //    {
            //        if (txtLegID.Text != "0")
            //        {
            //            txtLegID.Text = "";
            //            lblLegName.Text = "";
            //        }

            //    }
            //}
            //catch (Exception ex)
            //{
            //    string str = "Message:" + ex.Message;
            //    MessageBox.Show(str, "Error Message");
            //}
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {

            try
            {
                if (DtgridGuestInfo.Rows.Count > 0)
                {
                    selectrow = DtgridGuestInfo.SelectedCells[0].RowIndex;
                }
                if (txtCustName.Text.Trim() == "")
                {
                    txtCustName.Focus();
                    return;
                }
                else if (lblLegName.Text.Trim() == "")
                {
                    txtLegID.Focus();
                    txtLegID.SelectAll();
                    return;
                }
                else if (txtAddress1.Text.Trim() == "")
                {
                    txtAddress1.Focus();
                    return;
                }
                else if (txtMobile1.Text.Trim() == "")
                {
                    txtMobile1.Focus();
                    return;
                }
                else
                {

                    string strNationality;
                    strNationality = "IND";
                    if (cmbNationality.SelectedItem == "INDIAN")
                    {
                        strNationality = "IND";
                    }
                    else if (cmbNationality.SelectedItem == "NRI")
                    {
                        strNationality = "NRI";
                    }

                    var variable = "UPDATE";
                    if (BtnSave.Text == "SAVE")
                    {
                        txtcustcode.Text = "" + mod.GetMaxNum(tableName, firstCol);
                        variable = "INSERT";
                    }
                    DataTable dtData = new DataTable();
                    dtData.Columns.Add(new DataColumn("cust_id"));
                    dtData.Columns.Add(new DataColumn("document_detail", typeof(Image)));
                    for (int i = 0; i < lstImages.Count; i++)
                    {
                        var cust_id = i;
                        DataRow dr = dtData.NewRow();
                        dr[0] = txtcustcode.Text;
                        dr[1] = lstImages[i];
                        dtData.Rows.Add(dr);
                    }

                    Models.Common.DbConnectivity.Instance.SaveCustomerInfo(variable, txtcustcode.Text, cmbPrefix.Text, txtCustName.Text,
                        txtAddress1.Text, txtAddress2.Text, txtAddress3.Text, txtMobile1.Text, txtMobile2.Text, txtEmailId.Text, dtpDateofBirth.Value,
                        txtState.Text, txtPlace.Text, txtPin.Text, "N", mod.Isnull(txtLegID.Text, "0"), strNationality, txtPancard.Text, txtPassportNo.Text,
                        dtpPass.Value, dtpPassExp.Value, txtVisaNo.Text, dtpVisa.Value, dtpVisaExp.Value, dtData);

                    //select State ID
                    //str = "SELECT StateID FROM tbStateMaster WHERE StateName='" + txtState.Text + "'";
                    //SqlDataReader dr = mod.GetRecord(str);
                    //if (dr.HasRows)
                    //{
                    //    dr.Read();
                    //    StateIDVar = dr["StateID"].ToString();
                    //}
                    ////select City ID
                    //str = "SELECT CityID FROM tbCityMaster WHERE CityName='" + txtPlace.Text + "'";
                    //SqlDataReader dr1 = mod.GetRecord(str);
                    //if (dr1.HasRows)
                    //{
                    //    dr1.Read();
                    //    CityIDVar = dr1["CityID"].ToString();
                    //}

                    //BtnSave.Enabled = false;

                    //command = new SqlCommand("SP_INSERTGuestInfo", con.connect());
                    //command.CommandType = CommandType.StoredProcedure;
                    //if (BtnSave.Text == "SAVE")
                    //{
                    //    txtcustcode.Text = "" + mod.GetMaxNum(tableName, firstCol);
                    //    command.Parameters.AddWithValue("@variable", "INSERT");
                    //}
                    //else
                    //{
                    //    command.Parameters.AddWithValue("@variable", "UPDATE");
                    //}
                    //command.Parameters.AddWithValue("@code", txtcustcode.Text);
                    //command.Parameters.AddWithValue("@prefix", cmbPrefix.Text);
                    //command.Parameters.AddWithValue("@name", txtCustName.Text.Replace("'", "''"));
                    //command.Parameters.AddWithValue("@legID", mod.Isnull(txtLegID.Text, "0"));
                    //command.Parameters.AddWithValue("@dateofbirth", dtpDateofBirth.Value.ToShortDateString());
                    //command.Parameters.AddWithValue("@natonality", strNationality);
                    //command.Parameters.AddWithValue("@add1", mod.Isnull(txtAddress1.Text.Replace("'", "''"), "-"));
                    //command.Parameters.AddWithValue("@add2", mod.Isnull(txtAddress2.Text.Replace("'", "''"), "-"));
                    //command.Parameters.AddWithValue("@add3", mod.Isnull(txtAddress3.Text.Replace("'", "''"), "-"));
                    //command.Parameters.AddWithValue("@stateid", mod.Isnull(StateIDVar, "0"));
                    //command.Parameters.AddWithValue("@cityid", mod.Isnull(CityIDVar, "0"));
                    //command.Parameters.AddWithValue("@pin", mod.Isnull(txtPin.Text, "0"));
                    //command.Parameters.AddWithValue("@mobile1", mod.Isnull(txtMobile1.Text, "0"));
                    //command.Parameters.AddWithValue("@mobile2", mod.Isnull(txtMobile2.Text, "0"));
                    //command.Parameters.AddWithValue("@email", mod.Isnull(txtEmailId.Text, " "));
                    //command.Parameters.AddWithValue("@pancard", mod.Isnull(txtPancard.Text, "0"));
                    //command.Parameters.AddWithValue("@passportno", mod.Isnull(txtPassportNo.Text, "0"));
                    //command.Parameters.AddWithValue("@Passdateofissue", dtpPass.Value);
                    //command.Parameters.AddWithValue("@Passexpirydate", dtpPassExp.Value);
                    //command.Parameters.AddWithValue("@visano", mod.Isnull(txtVisaNo.Text, "0"));
                    //command.Parameters.AddWithValue("@Visadateofissue", dtpVisa.Value);
                    //command.Parameters.AddWithValue("@Visaexpirydate", dtpVisaExp.Value);
                    //command.Parameters.AddWithValue("@userid", Globalvariable.usercd);
                    //command.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                    //command.ExecuteNonQuery();
                    if (BtnSave.Text == "SAVE")
                    {
                        lblSucessMsg.Text = "Save Successfully !!!";
                    }
                    else
                    {
                        lblSucessMsg.Text = "Update Successfully !!!";
                        StrUpdate = "U";
                    }

                    //str = "";
                    //str = "DELETE FROM tbCustomerIdentityDetail WHERE cust_id='" + txtcustcode.Text + "' AND Branchcode='" + Globalvariable.bcode + "' AND user_id='" + Globalvariable.usercd + "'";
                    //SqlCommand cmddel = new SqlCommand(str, con.connect());
                    //cmddel.ExecuteNonQuery();

                    //for (int cnt = 0; cnt < itemMastGrid.Rows.Count; cnt++)
                    //{
                    //    string sPathToFileToSave;
                    //    fileName = "";
                    //    str_docID = "";

                    //    sPathToFileToSave = Convert.ToString(itemMastGrid.Rows[cnt].Cells[3].Value);
                    //    fileName = Convert.ToString(itemMastGrid.Rows[cnt].Cells[1].Value);
                    //    str_docID = DateTime.Now.ToString("MMddHHmmssyy") + fileName;
                    //    // use the file stream object to read the file from disk
                    //    if (sPathToFileToSave.Trim() != "")
                    //    {
                    //        using (System.IO.FileStream fs = new System.IO.FileStream(sPathToFileToSave, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    //        {
                    //            // store the file in a byte array that is the length of the file
                    //            byte[] fileData = new byte[fs.Length];

                    //            // read in the file stream to the byte array
                    //            fs.Read(fileData, 0, System.Convert.ToInt32(fs.Length));

                    //            //Construct a SQL string and a connection object and insert this file in the database                   
                    //            str = "";

                    //            str = "INSERT INTO tbCustomerIdentityDetail(document_detail,cust_id,file_name,content_type,size,document_id,Branchcode,user_id)" +
                    //               "VALUES(@doc_content," + txtcustcode.Text + ",'" + fileName + "','" + fs.GetType() + "','" + fs.Length + "','" + str_docID + "','" + Globalvariable.bcode + "','" + Globalvariable.usercd + "' )";

                    //            SqlCommand cmd = new SqlCommand(str, con.connect());
                    //            SqlParameter param1 = new SqlParameter("@doc_content", SqlDbType.Image);
                    //            param1.Value = fileData;
                    //            cmd.Parameters.Add(param1);
                    //            cmd.ExecuteNonQuery();
                    //            fs.Close();
                    //        }
                    //    }
                    //}

                    if (BtnSave.Text == "SAVE")
                    {
                        if (Globalvariable.frmName == "Reservation")
                        {
                            Globalvariable.GuestID = txtcustcode.Text;
                            Globalvariable.GuestName = txtCustName.Text;
                            this.Close();
                        }
                        else
                        {
                            mod.DataGridBind(DtgridGuestInfo, tableName, firstCol, secondCol, "N", "Customer Name", "Code");
                            txtsearch.Focus();
                            txtsearch.Text = "";
                            string txt = BtnSave.Text;
                            FillDetails();
                            int s = selectrow;
                            if (DtgridGuestInfo.Rows.Count > 0)
                            {
                                DtgridGuestInfo.CurrentCell = DtgridGuestInfo[0, s];
                            }
                            StrUpdate = "";
                            BtnSave.Enabled = false;
                        }
                    }
                    else
                    {
                        mod.DataGridBind(DtgridGuestInfo, tableName, firstCol, secondCol, "N", "Customer Name", "Code");
                        DtgridGuestInfo.Rows[selectrow].Selected = true;
                    }
                }

            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }


        private void txtsearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                DtgridGuestInfo_KeyDown(sender, e);
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                mod.txtSearchChange(ChkDeleted, firstCol, secondCol, tableName, DtgridGuestInfo, txtsearch, "Customer Name", "Code");
                FillDetails();
                lblSucessMsg.Text = "";
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void txtsearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && txtsearch.Text == "")
            {
                BtnAdd.Focus();
            }
            lblSucessMsg.Text = "";

        }

        public void DeleteLoad()
        {
            ChkDeleted.Checked = false;
            txtsearch.Focus();
            txtState.Visible = true;
            txtPlace.Visible = true;
            FillDetails();
        }


        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                string strMsg;
                strMsg = BtnDelete.Text;
                if (BtnDelete.Text == "DELETE" || BtnDelete.Text == "RECALL")
                {
                    i = mod.deleteButtonClick(tableName, firstCol, txtcustcode.Text,
                         txtsearch, BtnDelete.Text.Substring(0, 1));
                    if (i == 1)
                    {
                        DeleteLoad();
                        if (strMsg == "DELETE")
                            lblSucessMsg.Text = "Deleted Successfully !!!";
                        else
                            lblSucessMsg.Text = "Recalled Successfully !!!";
                    }
                }

                else if (BtnDelete.Text == "RESET")
                {
                    ADD();
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void txtPancard_Enter(object sender, EventArgs e)
        {
            txtPancard.BackColor = Color.Yellow;
        }

        private void txtPassportNo_Enter(object sender, EventArgs e)
        {
            txtPassportNo.BackColor = Color.Yellow;
        }

        private void dtpPass_Enter(object sender, EventArgs e)
        {
            dtpPass.BackColor = Color.Yellow;
        }

        private void dtpPassExp_Enter(object sender, EventArgs e)
        {
            dtpPassExp.BackColor = Color.Yellow;
        }

        private void txtVisaNo_Enter(object sender, EventArgs e)
        {
            txtVisaNo.BackColor = Color.Yellow;
        }

        private void dtpVisa_Enter(object sender, EventArgs e)
        {
            dtpVisa.BackColor = Color.Yellow;
        }

        private void dtpVisaExp_Enter(object sender, EventArgs e)
        {
            dtpVisaExp.BackColor = Color.Yellow;
        }

        private void DtgridGuestInfo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectRecord();
            //try
            //{
            //    FillDetails();
            //    mod.gridDoubleClick(ChkDeleted, BtnDelete, txtCustName);
            //    if (ChkDeleted.Checked == true)
            //        BtnDelete.Focus();
            //    else
            //        txtCustName.Focus();
            //    lblSucessMsg.Text = "";
            //    BtnSave.Enabled = true;

            //}
            //catch (Exception ex)
            //{
            //    string str = "Message:" + ex.Message;
            //    MessageBox.Show(str, "Error Message");
            //}
        }

        private void txtEmailId_Validating(object sender, CancelEventArgs e)
        {
            //System.Text.RegularExpressions.Regex rEmail = new System.Text.RegularExpressions.Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            // if (txtEmailId.Text.Length > 0)
            // {
            //     if (!rEmail.IsMatch(txtEmailId.Text))
            //     {
            //         MessageBox.Show("Please enter valid email address", "Error", MessageBoxButtons.OK);
            //         txtEmailId.Focus();
            //     }
            // }
        }

        private void txtEmailId_Enter_1(object sender, EventArgs e)
        {
            txtEmailId.BackColor = Color.Yellow;
        }


        private void dtpPass_Enter_1(object sender, EventArgs e)
        {

            dtpPass.BackColor = Color.Yellow;
        }

        private void dtpDateofBirth_Enter(object sender, EventArgs e)
        {

            dtpDateofBirth.BackColor = Color.Yellow;
        }

        //private void itemMastGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        //{
        //    itemMastGrid[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.Yellow;
        //}

        //private void itemMastGrid_CellLeave(object sender, DataGridViewCellEventArgs e)
        //{
        //    itemMastGrid[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.Empty;
        //}

        private void txtLegID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void txtAge_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void txtCity_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space || e.KeyChar == '.');
        }

        private void txtPin_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void txtPhonNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void txtMobileNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void txtEmailId_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(e.KeyChar != (char)Keys.Space);
            // try
            //{
            //    string email;
            //    if (e.KeyChar == (char)Keys.Enter)
            //    {
            //        email = txtEmailId.Text;
            //        if (email != "" && (email.IndexOf('@') < 0 || email.IndexOf('.') < 0))
            //        {
            //            int s = email.IndexOf('@');
            //            txtEmailId.Text = "";
            //            txtEmailId.Focus();
            //        }
            //    }
            //}
            // catch (Exception ex)
            // {
            //     string str = "Message:" + ex.Message;
            //     MessageBox.Show(str, "Error Message");
            // }
        }

        private void txtsearch_Leave(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.White;
        }

        private void txtCustName_Leave(object sender, EventArgs e)
        {
            txtCustName.BackColor = Color.White;
        }

        private void txtLegID_Leave(object sender, EventArgs e)
        {
            txtLegID.BackColor = Color.White;
        }

        private void dtpDateofBirth_Leave(object sender, EventArgs e)
        {
            dtpDateofBirth.BackColor = Color.White;
        }

        private void cmbNationality_Leave(object sender, EventArgs e)
        {
            cmbNationality.BackColor = Color.White;
        }

        private void txtAddress2_Leave(object sender, EventArgs e)
        {
            txtAddress2.BackColor = Color.White;
        }

        private void txtAddress3_Leave(object sender, EventArgs e)
        {
            txtAddress3.BackColor = Color.White;
        }

        private void txtCity_Leave(object sender, EventArgs e)
        {
            txtPlace.BackColor = Color.White;
        }

        private void txtPin_Leave(object sender, EventArgs e)
        {
            txtPin.BackColor = Color.White;
        }

        private void txtPhonNo_Leave(object sender, EventArgs e)
        {
            txtMobile1.BackColor = Color.White;
        }

        private void txtMobileNo_Leave(object sender, EventArgs e)
        {
            txtMobile2.BackColor = Color.White;
        }

        private void txtEmailId_Leave(object sender, EventArgs e)
        {
            txtEmailId.BackColor = Color.White;
        }

        private void txtPancard_Leave(object sender, EventArgs e)
        {
            txtPancard.BackColor = Color.White;
        }

        private void txtPassportNo_Leave(object sender, EventArgs e)
        {
            txtPassportNo.BackColor = Color.White;
        }

        private void txtVisaNo_Leave(object sender, EventArgs e)
        {
            txtVisaNo.BackColor = Color.White;
        }

        private void cmbPrefix_Leave(object sender, EventArgs e)
        {
            cmbPrefix.BackColor = Color.White;
        }

        private void dtpVisaExp_KeyPress(object sender, KeyPressEventArgs e)
        {
            //try
            //{
            //    if (e.KeyChar == (char)Keys.Enter)
            //    {
            //        if (itemMastGrid.Rows.Count > 0)
            //        {
            //            //itemMastGrid.Rows.Clear();
            //            itemMastGrid.Enabled = true;
            //            rowNo = itemMastGrid.Rows.Count - 1;
            //            if (itemMastGrid.Rows[rowNo].Cells[3].Value != null)
            //            {
            //                foreach (DataGridViewRow row in itemMastGrid.Rows)
            //                {
            //                    DataGridViewButtonCell btn = (DataGridViewButtonCell)row.Cells[5];
            //                    btn.ReadOnly = false;
            //                }
            //                itemMastGrid.Rows.Add();
            //                rowNo = itemMastGrid.Rows.Count - 1;
            //                itemMastGrid.Rows[rowNo].Cells[0].Value = itemMastGrid.Rows.Count;
            //                itemMastGrid.Rows[rowNo].Cells[5].Value = "Start Webcam";
            //                itemMastGrid.CurrentCell = itemMastGrid[5, rowNo];
            //                itemMastGrid.BeginEdit(true);

            //            }
            //            else
            //            {
            //                foreach (DataGridViewRow row in itemMastGrid.Rows)
            //                {
            //                    DataGridViewButtonCell btn = (DataGridViewButtonCell)row.Cells[5];
            //                    btn.ReadOnly = false;
            //                }
            //                itemMastGrid.Rows[rowNo].Cells[0].Value = itemMastGrid.Rows.Count;
            //                itemMastGrid.Rows[rowNo].Cells[5].Value = "Start Webcam";
            //                itemMastGrid.CurrentCell = itemMastGrid[5, rowNo];
            //                itemMastGrid.BeginEdit(true);
            //            }

            //        }
            //        else if (itemMastGrid.Rows.Count == 0)
            //        {
            //            itemMastGrid.Enabled = true;
            //            itemMastGrid.Rows.Clear();
            //            itemMastGrid.Rows.Add();
            //            rowNo = itemMastGrid.Rows.Count - 1;
            //            itemMastGrid.Rows[rowNo].Cells[0].Value = itemMastGrid.Rows.Count;
            //            itemMastGrid.Rows[rowNo].Cells[5].Value = "Start Webcam";
            //            itemMastGrid.CurrentCell = itemMastGrid[5, rowNo];
            //            itemMastGrid.BeginEdit(true);

            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    string str = "Message:" + ex.Message;
            //    MessageBox.Show(str, "Error Message");
            //}
        }

        private void itemMastGrid_EditingControlShowing_1(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                e.Control.PreviewKeyDown -= Control_PreviewKeyDown;
                e.Control.PreviewKeyDown += new PreviewKeyDownEventHandler(Control_PreviewKeyDown);

                e.Control.GotFocus -= dataGridViewTextBox_GotFocus;
                e.Control.GotFocus += new EventHandler(dataGridViewTextBox_GotFocus);

                tb = (DataGridViewTextBoxEditingControl)e.Control;
                tb.KeyPress += new KeyPressEventHandler(dataGridViewTextBox_KeyPress);
                e.Control.KeyPress += new KeyPressEventHandler(dataGridViewTextBox_KeyPress);
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void dataGridViewTextBox_GotFocus(object sender, EventArgs e)
        {
            try
            {
                TextBox obj = sender as TextBox;
                obj.BackColor = Color.Yellow;
                obj.Focus();
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }
        private void dataGridViewTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                TextBox obj = sender as TextBox;
                if (!Char.IsNumber(e.KeyChar)
                      && (Keys)e.KeyChar != Keys.Back)  // check if backspace is pressed
                {
                    e.Handled = true;
                }
                else if (obj.Text.ToString().Length > 3)
                    e.Handled = true;
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        //private void itemMastGrid_CellEnter_1(object sender, DataGridViewCellEventArgs e)
        //{
        //    itemMastGrid[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.Yellow;
        //}

        //private void itemMastGrid_CellLeave_1(object sender, DataGridViewCellEventArgs e)
        //{
        //    itemMastGrid[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.White;
        //}

        //private void itemMastGrid_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    try
        //    {
        //        colNo = itemMastGrid.CurrentCell.ColumnIndex;
        //        if (e.KeyChar == (char)Keys.Enter && colNo == 5)
        //        {
        //            if (itemMastGrid.Rows.Count > 0)
        //            {
        //                //itemMastGrid.Rows.Clear();
        //                itemMastGrid.Enabled = true;
        //                rowNo = itemMastGrid.Rows.Count - 1;
        //                if (itemMastGrid.Rows[rowNo].Cells[3].Value != null)
        //                {
        //                    itemMastGrid.Rows.Add();
        //                    rowNo = itemMastGrid.Rows.Count - 1;
        //                    itemMastGrid.Focus();
        //                    ActiveControl = itemMastGrid;
        //                    DataGridViewButtonColumn btncolmn = new DataGridViewButtonColumn();
        //                    //   btncolmn = itemMastGrid.Rows[rowNo].Cells[5];
        //                    itemMastGrid.Rows[rowNo].Cells[5].Value = "Start Webcam";

        //                    itemMastGrid.CurrentCell = itemMastGrid[5, rowNo];
        //                    itemMastGrid.BeginEdit(true);
        //                    //  itemMastGrid.CurrentCell.Selected = true;
        //                }
        //                else
        //                {
        //                    itemMastGrid.CurrentCell = itemMastGrid[5, rowNo];
        //                    itemMastGrid.BeginEdit(true);
        //                }

        //            }
        //            else if (itemMastGrid.Rows.Count == 0)
        //            {
        //                itemMastGrid.Rows.Add();
        //                rowNo = itemMastGrid.Rows.Count - 1;
        //                //if (itemMastGrid.Rows[rowNo].Cells[1].Value != null)
        //                //{                          
        //                // rowNo = itemMastGrid.RowCount;
        //                itemMastGrid.Rows[rowNo].Cells[5].Value = "Start Webcam";
        //                itemMastGrid.CurrentCell = itemMastGrid[5, rowNo];
        //                itemMastGrid.BeginEdit(true);
        //                //}

        //            }
        //        }
        //        if (e.KeyChar == (char)43 && colNo == 5)
        //        {
        //            if (itemMastGrid.RowCount > 0)
        //            {
        //                //plusKeyPress = true;
        //                int i = itemMastGrid.CurrentCell.RowIndex;
        //                if (Convert.ToString(itemMastGrid.Rows[i].Cells[1].Value) == string.Empty)
        //                {
        //                    rowNo = rowNo - 1;
        //                    itemMastGrid.Rows.RemoveAt(i);
        //                }
        //                BtnSave.Enabled = true;
        //                BtnSave.Focus();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string str = "Message:" + ex.Message;
        //        MessageBox.Show(str, "Error Message");
        //    }
        //}

        //private void itemMastGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    try
        //    {
        //        if (e.RowIndex >= 0)
        //        {
        //            if (e.ColumnIndex == 5)
        //            {
        //                rowNo = e.RowIndex;
        //                if (isDeviceStarted)
        //                {
        //                    videoCaptureDevice.Stop();
        //                    isDeviceStarted = false;
        //                    itemMastGrid.Rows[rowNo].Cells[0].Value = itemMastGrid.RowCount;
        //                    itemMastGrid.Rows[rowNo].Cells[2].Value = (Bitmap)picImage.Image.Clone();
        //                    picImage.Image = null;
        //                    itemMastGrid.Rows[rowNo].Cells[5].Value = "Start Webcam";

        //                }
        //                else
        //                {
        //                    itemMastGrid.Rows[rowNo].Cells[5].Value = "Starting";
        //                    videoCaptureDevice.Start();
        //                }

        //                //OpenFileDialog openFileDialog1 = new OpenFileDialog();
        //                //using (OpenFileDialog dlg = new OpenFileDialog())
        //                //{
        //                //    dlg.Title = "";
        //                //    dlg.Filter = "bmp files (*.bmp)|*.bmp|JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
        //                //    //(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif;
        //                //    if (dlg.ShowDialog() == DialogResult.OK)
        //                //    {
        //                //        // Create a new Bitmap object from the picture file on disk,
        //                //        // and assign that to the PictureBox.Image property
        //                //        picImage.Image = new Bitmap(dlg.FileName);
        //                //        itemMastGrid.Rows[rowNo].Cells[0].Value = itemMastGrid.RowCount;
        //                //        itemMastGrid.Rows[rowNo].Cells[1].Value = dlg.SafeFileName;
        //                //        itemMastGrid.Rows[rowNo].Cells[2].Value = new Bitmap(dlg.FileName);
        //                //        itemMastGrid.Rows[rowNo].Cells[3].Value = dlg.FileName;
        //                //        // DataGridViewButtonColumn buttonCell =itemMastGrid.Rows[e.RowIndex].Cells["Buttons"];

        //                //        txtImageName.Text = dlg.SafeFileName;
        //                //    }
        //                //}
        //            }
        //            else
        //            {
        //                rowNo = e.RowIndex;
        //                DataGridViewRow row = this.itemMastGrid.Rows[e.RowIndex];
        //                picImage.Image = (Image)itemMastGrid.Rows[e.RowIndex].Cells[2].Value;
        //                //txtImageName.Text = itemMastGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string str = "Message:" + ex.Message;
        //        MessageBox.Show(str, "Error Message");
        //    }
        //}

        private void FrmGuestInformation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true && e.KeyCode == Keys.S && BtnSave.Text == "SAVE")
            {
                BtnSave.PerformClick();
            }
            if (e.Control == true && e.KeyCode == Keys.U && BtnSave.Text == "UPDATE")
            {
                BtnSave.PerformClick();
            }
            if (e.Control == true && e.KeyCode == Keys.D && BtnDelete.Text == "DELETE" && BtnDelete.Enabled == true)
            {
                BtnDelete.PerformClick();
            }
            if (e.Control == true && e.KeyCode == Keys.R && BtnDelete.Text == "RESET")
            {
                BtnDelete.PerformClick();
            }
            if (e.Control == true && e.KeyCode == Keys.X)
            {
                BtnExit.PerformClick();
            }
            if (e.Control == true && e.KeyCode == Keys.L && BtnDelete.Text == "RECALL")
            {
                BtnDelete.PerformClick();
            }
        }

        private void txtState_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPlace.Focus();
            }
        }

        private void btnAddMore_Click(object sender, EventArgs e)
        {
            if (isDeviceStarted)
            {
                videoCaptureDevice.Stop();
                isDeviceStarted = false;
                LoadImageToListView((Image)picImage.Image.Clone());
                picImage.Image = null;
                btnAddMore.Text = "Load Document";

            }
            else
            {
                tmrNew.Enabled = true;
                btnAddMore.Text = "Starting Webcam";
                btnAddMore.Enabled = false;
                videoCaptureDevice.Start();
            }
            //try
            //{
            //    if (itemMastGrid.Rows.Count > 0)
            //    {
            //        itemMastGrid.Enabled = true;
            //        rowNo = itemMastGrid.Rows.Count - 1;
            //        if (itemMastGrid.Rows[rowNo].Cells[2].Value != null)
            //        {
            //            foreach (DataGridViewRow row in itemMastGrid.Rows)
            //            {
            //                DataGridViewButtonCell btn = (DataGridViewButtonCell)row.Cells[5];
            //                btn.ReadOnly = false;
            //            }
            //            itemMastGrid.Rows.Add();
            //            rowNo = itemMastGrid.Rows.Count - 1;
            //            itemMastGrid.Rows[rowNo].Cells[0].Value = itemMastGrid.Rows.Count;
            //            itemMastGrid.Rows[rowNo].Cells[5].Value = "Start Webcam";
            //            itemMastGrid.CurrentCell = itemMastGrid[5, rowNo];
            //            itemMastGrid.BeginEdit(true);

            //        }
            //        else
            //        {

            //            itemMastGrid.Rows[rowNo].Cells[0].Value = itemMastGrid.Rows.Count;
            //            itemMastGrid.Rows[rowNo].Cells[5].Value = "Start Webcam";
            //            itemMastGrid.CurrentCell = itemMastGrid[5, rowNo];
            //            itemMastGrid.BeginEdit(true);
            //        }

            //    }
            //    else if (itemMastGrid.Rows.Count == 0)
            //    {
            //        itemMastGrid.Enabled = true;
            //        itemMastGrid.Rows.Clear();
            //        itemMastGrid.Rows.Add();
            //        rowNo = itemMastGrid.Rows.Count - 1;
            //        itemMastGrid.Rows[rowNo].Cells[0].Value = itemMastGrid.Rows.Count;
            //        itemMastGrid.Rows[rowNo].Cells[5].Value = "Start Webcam";
            //        itemMastGrid.CurrentCell = itemMastGrid[5, rowNo];
            //        itemMastGrid.BeginEdit(true);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    string str = "Message:" + ex.Message;
            //    MessageBox.Show(str, "Error Message");
            //}
        }

        private void lvViews_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                picImage.Image = lstImages[e.Item.ImageIndex];
                btnRemoveDocument.Enabled = true;
            }
            else
            {
                picImage.Image = null;
                btnRemoveDocument.Enabled = false;
            }
        }

        private void txtState_Enter(object sender, EventArgs e)
        {
            txtState.BackColor = Color.Yellow;
        }

        private void txtState_Leave(object sender, EventArgs e)
        {
            txtState.BackColor = Color.White;
        }

        private void txtPlace_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPin.Focus();
            }
        }

        private void btnStateAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtState.Text == "")
                {
                    txtState.Focus();
                }
                else if (txtState.Text != "")
                {
                    str = "SELECT * FROM tbStateMaster WHERE StateName='" + txtState.Text.Replace("'", "''") + "'";
                    SqlDataReader dr = mod.GetRecord(str);
                    if (dr.HasRows)
                    {
                        MessageBox.Show("Name Already Exist!!");
                        txtState.Focus();
                    }
                    else
                    {
                        str = "INSERT INTO tbStateMaster(StateName) VALUES('" + txtState.Text.Replace("'", "''") + "')";
                        SqlCommand cmd = new SqlCommand(str, con.connect());
                        cmd.ExecuteNonQuery();
                        lblSucessMsg.Text = "Save Successfully !!!";
                        txtState.Visible = false;
                        autoAddState();
                        txtState.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void btnRemoveDocument_Click(object sender, EventArgs e)
        {
            if (lvViews.SelectedIndices.Count > 0)
            {
                RemoveImageFromListView(lvViews.SelectedIndices[0]);
            }
        }

        private void btnGreaterID_Click(object sender, EventArgs e)
        {
            Models.Common.FormManagement.Instance.SelectRecordFromGrid(DtgridGuestInfo, Models.Common.RecordMove.Next, SelectRecord);

        }


        private void SelectRecord()
        {

            mod.gridDoubleClick(ChkDeleted, BtnDelete, txtcustcode);
            FillDetails();

            lblSucessMsg.Text = "";
            BtnSave.Enabled = true;
            mod.flag = 1;
        }

        private void btnLessID_Click(object sender, EventArgs e)
        {
            Models.Common.FormManagement.Instance.SelectRecordFromGrid(DtgridGuestInfo, Models.Common.RecordMove.Previous, SelectRecord);

        }

        private void btnPlaceAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPlace.Text == "")
                {
                    txtPlace.Focus();
                }
                else if (txtPlace.Text != "")
                {
                    str = "SELECT * FROM tbCityMaster WHERE CityName='" + txtPlace.Text.Replace("'", "''") + "'";
                    SqlDataReader dr = mod.GetRecord(str);
                    if (dr.HasRows)
                    {
                        MessageBox.Show("Name Already Exist!!");
                        txtPlace.Focus();
                    }
                    else
                    {
                        str = "INSERT INTO tbCityMaster(CityName) VALUES('" + txtPlace.Text.Replace("'", "''") + "')";
                        SqlCommand cmd = new SqlCommand(str, con.connect());
                        cmd.ExecuteNonQuery();
                        lblSucessMsg.Text = "Save Successfully !!!";
                        txtPlace.Visible = false;
                        autoAddCity();
                        txtPlace.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void txtAddress1_Enter(object sender, EventArgs e)
        {
            txtAddress1.BackColor = Color.Yellow;
        }

        private void txtAddress1_Leave(object sender, EventArgs e)
        {
            txtAddress1.BackColor = Color.White;
        }

        private void FrmGuestInformation_Shown(object sender, EventArgs e)
        {
            txtsearch.Focus();
        }

        private void LoadListView()
        {
            Image[] images = lstImages.ToArray();
            imgList.Images.Clear();
            imgList.ImageSize = new Size(75, 75);
            imgList.Images.AddRange(images);

            lvViews.Clear();
            lvViews.LargeImageList = imgList;
            lvViews.View = View.LargeIcon;
            lvViews.Alignment = ListViewAlignment.Left;

            for (int i = 0; i < imgList.Images.Count; i++)
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.ImageIndex = i;
                lvViews.Items.Add(listViewItem);
            }


        }

        private void LoadImageToListView(Image image)
        {
            lstImages.Add(image);
            imgList.Images.Add(image);

            ListViewItem listViewItem = new ListViewItem();
            listViewItem.ImageIndex = lstImages.Count - 1;
            lvViews.Items.Add(listViewItem);


        }

        private void RemoveImageFromListView(int index)
        {
            lstImages.RemoveAt(index);

            if (lstImages.Count == 0)
            {
                btnRemoveDocument.Enabled = false;
            }

            LoadListView();

            picImage.Image = null;

        }


        private void ClearImages()
        {
            lstImages.Clear();
            imgList.Images.Clear();
            lvViews.Clear();
        }


    }
}

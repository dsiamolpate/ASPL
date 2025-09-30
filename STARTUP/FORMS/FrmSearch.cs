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
using System.IO;
using ASPL.CLASSES;
using ASPL.STARTUP;
using ASPL.LODGE.MASTERS;
using ASPL.LODGE.TRANSCTION;
using ASPL.Material_Management;
using ASPL.ACCOUNT;

namespace ASPL.STARTUP.FORMS
{
    public partial class FrmSearch : Form
    {
        Design ObjDesign = new Design();
        public FrmSearch()
        {
            InitializeComponent();
            AutoSearchDecision = false;
        }

        public FrmSearch(string TableKey, bool autoSearchDecision = false) : this()
        {
            AutoSearchDecision = autoSearchDecision;
            frmnm = TableKey;
        }

        private void SetInitialDetails()
        {
            IsInitialized = true;
            NameColumnDisplayName = "Name";
            CodeColumnDisplayName = "Code";
            CustomerType = "0";
            CustomerTypeColumn = "cust_type";

            switch (frmnm)
            {
                case "FrmMenuItem":
                    CodeColumnName = "Group_Code";
                    NameColumnName = "Group_Name";
                    TableName = "tbItemGroup";
                    break;
                case "RoomMaster":
                    // Search Tax
                    CodeColumnName = "catgry_code";
                    NameColumnName = "catgry_name";
                    TableName = "RoomCategoryMaster";
                    break;
                case "RoomCategory":
                    // Search Tax
                    CodeColumnName = "tax_code";
                    NameColumnName = "name";
                    TableName = "tbTaxMaster";

                    CodeColumnDisplayName = "tax_code";
                    NameColumnDisplayName = "name";
                    break;
                case "GST":
                    // Search Tax
                    CodeColumnName = "tax_code";
                    NameColumnName = "name";
                    TableName = "tbTaxMaster";

                    CodeColumnDisplayName = "tax_code";
                    NameColumnDisplayName = "name";
                    NotIn = Globalvariable.DuplicateValue;
                    break;
                case "Common_EmployeeDesignation":
                    //Search Employee Designation
                    CodeColumnName = "Desig_Code";
                    NameColumnName = "Desig_Name";
                    TableName = "tbEmployeeDesignation";
                    break;
                case "FrmGuestInformation":
                    //Search Ledger Name
                    CodeColumnName = "supplier_id";
                    NameColumnName = "supplier_name";
                    TableName = "tbSupplierMaster";
                    CustomerType = "D";
                    break;
                case "Common_TallyMaster":
                    CodeColumnName = "Tally_Code";
                    NameColumnName = "Tally_Name";
                    TableName = "tbTallyMaster";
                    break;
                case "Common_GeneralLedger":
                    CodeColumnName = "Code";
                    NameColumnName = "Description";
                    TableName = "tbGeneralLedger";

                    NameColumnDisplayName = "Description";
                    break;
                case "FrmItemMaster":
                    //Search Tally code
                    if (Globalvariable.SearchString == "TallyID")
                    {
                        CodeColumnName = "Tally_Code";
                        NameColumnName = "Tally_Name";
                        TableName = "tbTallyMaster";

                        CodeColumnDisplayName = "Tally_Code";
                        NameColumnDisplayName = "Tally_Name";
                    }
                    //Search Tax
                    else if (Globalvariable.SearchString == "ItemCardTaxID")
                    {
                        CodeColumnName = "tax_code";
                        NameColumnName = "name";
                        TableName = "tbTaxMaster";

                    }
                    break;
                case "FrmSubItemMaster":
                    //Search Menu Group
                    if (Globalvariable.MenuCardVarb == "GroupId")
                    {
                        CodeColumnName = "Item_Code";
                        NameColumnName = "Item_name";
                        TableName = "tbMenuGroup";

                    }
                    //Search Kitchen Section
                    else if (Globalvariable.MenuCardVarb == "KOTSect")
                    {
                        CodeColumnName = "KotSec_Code";
                        NameColumnName = "Discription";
                        TableName = "tbKotSection";
                        NameColumnDisplayName = "Discription";
                    }
                    break;
                case "FrmSupplierMaster":
                    //Search Tally code
                    CodeColumnName = "Tally_Code";
                    NameColumnName = "Tally_Name";
                    TableName = "tbTallyMaster";

                    break;
                case "GeneralLedger":
                    // search Group
                    CodeColumnName = "grp_code";
                    NameColumnName = "description";
                    TableName = "tbGroup";
                    NameColumnDisplayName= "Description";

                    break;
                case "FrmChanKitSection":
                    //Search Kitchen Section
                    if (Globalvariable.SearchString == "KitchenSection")
                    {
                        CodeColumnDisplayName = "KotSec_Code";
                        NameColumnDisplayName = "Discription";
                        TableName = "tbKotSection";

                    }
                    //Search Menu Card
                    else if (Globalvariable.SearchString == "MenuCard")
                    {
                        CodeColumnDisplayName = "Menu_Code";
                        NameColumnDisplayName = "Menu_Name";
                        TableName = "tbRMenuCard";
                    }
                    break;
                case "FrmChangeItemRate":

                    //Search Menu Card
                    if (Globalvariable.SearchString == "Menucard")
                    {
                        CodeColumnName = "Menu_Code";
                        NameColumnName = "Menu_Name";
                        TableName = "tbRMenuCard";

                    }
                    break;
                case "FrmLiqGrp":
                    //Search Liq Group
                    if (Globalvariable.MenuCardVarb == "LiqId")
                    {
                        CodeColumnName = "Liq_Code";
                        NameColumnName = "Liq_Name";
                        TableName = "tbRLiqGrpName";
                    }

                    break;
                case "FrmMenuGroup":
                    //Search Menu Group
                    if (Globalvariable.MenuCardVarb == "CategoryId")
                    {
                        CodeColumnName = "Category_Code";
                        NameColumnName = "Category_Name";
                        TableName = "tbMenuCategory";

                    }

                    break;
                case "FrmMenuCard":
                    //Search Menu Card

                    if (Globalvariable.MenuCardVarb == "MenuGroupId")
                    {
                        CodeColumnName = "Item_Code";
                        NameColumnName = "Item_name";
                        TableName = "tbRMenuGroup";
                    }
                    //Search Kitchen Section
                    else if (Globalvariable.MenuCardVarb == "KOTSect")
                    {
                        CodeColumnName = "KotSec_Code";
                        NameColumnName = "Discription";
                        CodeColumnDisplayName = "Code";
                        NameColumnDisplayName = "Discription";
                        TableName = "tbKotSection";
                    }
                    else if (Globalvariable.MenuCardVarb == "LiqCode")
                    {
                        CodeColumnName = "Code";
                        NameColumnName = "Liq_Name";
                        NameColumnDisplayName = "Liq_Name";
                        TableName = "tbRLiq_Grp";
                    }

                    break;

                case "FrmTableMaster":
                    CodeColumnName = "Table_no";
                    NameColumnName = "TableGrpName";
                    TableName = "tbRTableGroup";
                    break;

                case "FrmTablenumberMaster":
                    CodeColumnName = "Table_no";  
                    NameColumnName = "Name"; 
                    TableName = "tbRTable_Mast";
                    break;

                case "FrmEmpinfoMaster":
                    CodeColumnName = "EmpNo";
                    NameColumnName = "EmpName";
                    TableName = "tbEmployeeInformation";
                    break;

                case "FrmCalInsentive":
                    CodeColumnName = "Menu_Code";
                    NameColumnName = "Menu_Name";
                    TableName = "tbRMenuCard";
                    break;
                case "FrmDiscount":
                    CodeColumnName = "Code";
                    NameColumnName = "Description";
                    TableName = "tbGeneralLedger";
                    NameColumnDisplayName = "Description";
                    CodeColumnDisplayName = "Code";
                    break;
                case "FrmCustomerMaster":
                    CodeColumnName = "Customer_id";
                    NameColumnName = "Customer_name";
                    TableName = "tbGuestInformation";
                    break;

                case "FrmPurchase":
                    if (Globalvariable.SearchString == "MenuItem")
                    {
                        CodeColumnName = "item_code";
                        NameColumnName = "item_name";
                        TableName = "tbItemName";
                    }
                    else if (Globalvariable.SearchString == "SupplierMaster")
                    {
                        CodeColumnName = "supplier_id";
                        NameColumnName = "supplier_name";
                        TableName = "tbSupplierMaster";
                        CustomerType = Globalvariable.Cust_Type;
                    }
                    break;
                case "FrmReservation":
                    if (Globalvariable.SearchString == "CompanyInfo")
                    {
                        CodeColumnName = "supplier_id";
                        NameColumnName = "supplier_name";
                        TableName = "tbSupplierMaster";
                        CustomerType = Globalvariable.Cust_Type;
                    }
                    else if (Globalvariable.SearchString == "GuestInfo")
                    {
                        CodeColumnName = "Customer_id";
                        NameColumnName = "Customer_name";
                        TableName = "tbGuestInformation";
                    }
                    else if(Globalvariable.SearchString=="RoomType")
                    {
                        CodeColumnName = "catgry_code";
                        NameColumnName = "catgry_name";
                        TableName = "RoomCategoryMaster";
                    }
                    else if (Globalvariable.SearchString == "EmployeeInfo")
                    {
                        CodeColumnName = "EmpNo";
                        NameColumnName = "EmpName";
                        TableName = "tbEmployeeInformation";
                    }
                    break;
                default:
                    IsInitialized = false;
                    break;
            }

        }

        public bool IsValuefound()
        {

            if (IsInitialized)
            {

                var strSQL = $"EXEC [dbo].[SP_SearchText] @tableName = '{TableName}', @code = '{CodeColumnName}', @name = '{NameColumnName}', @colm_code = '{CodeColumnDisplayName}', @colm_name = '{NameColumnDisplayName}', @branchcode = '{Globalvariable.bcode}', @custtype = '{CustomerType}',  @deletYN = 'N', @SearchChangeVariable = 'SrchByCode', @text = '{val}', @NotIn = '{NotIn}'";
                var dr = mod.GetRecord(strSQL);
                if (dr.HasRows)
                {
                    dr.Read();
                    codeselected = dr[CodeColumnDisplayName].ToString();
                    descselected = dr[NameColumnDisplayName].ToString();
                    return true;
                }
            }

            return false;
        }

        public string val
        {
            get { return srchtxt; }
            set { srchtxt = value; }
        }

        private string TableName { get; set; }
        private string CodeColumnName { get; set; }
        private string CodeColumnDisplayName { get; set; }
        private string NameColumnName { get; set; }
        private string NameColumnDisplayName { get; set; }
        private string CustomerType { get; set; }
        private string CustomerTypeColumn { get; set; }
        private string NotIn { get; set; }

        private string FormKey;

        private bool IsInitialized;
        public bool AutoSearchDecision { get; set; }

        public static int searchNo;
        public string frmnm
        {
            get
            {
                return FormKey;
            }
            set
            {
                FormKey = value;
                SetInitialDetails();

            }
        }
        public string codeselected, descselected, descselectedfromgrid;
        string srchtxt;
        private string pastSelection;

        Module mod = new Module();
        SqlCommand cmd;
        SqlDataReader sqldr;
        SqlDataAdapter da;
        DataSet ds = new DataSet();

        DataTable dt = new DataTable();
        Connection con = new Connection();
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
        private void FrmSearch_Load(object sender, EventArgs e)
        {
            try
            {

                ObjDesign.FormDesign(this, SearchGrid);
                Globalvariable.SearchChangeVariable = "SrchByName";
                txtsearch.Text = srchtxt;/* == null ? "" : srchtxt;*/
                if (txtsearch.Text.Trim().Length == 0)
                {
                    txtsearch_TextChanged(null, null);
                }


            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        public void fillsearchgrid(SqlDataAdapter da)
        {

            try
            {
                string wrd;
                wrd = mod.SrchChange(txtsearch);
                DataSet ds1 = new DataSet();
                DataTable dt = new DataTable();
                ds1.Clear();
                da.Fill(ds1);
                SearchGrid.AutoGenerateColumns = true;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    SearchGrid.DataSource = ds1.Tables[0];
                    SearchGrid.Columns[0].Width = 357;
                    SearchGrid.Columns[1].Width = 142;
                }
                else
                {
                    SearchGrid.DataSource = null;
                    dt.Columns.Add("Name", typeof(string));
                    dt.Columns.Add("Code", typeof(string));
                    SearchGrid.DataSource = dt;
                    SearchGrid.Columns[0].Width = 357;
                    SearchGrid.Columns[1].Width = 142;
                }

            }
            catch (Exception ex)
            {
                string str1 = "Message:" + ex.Message;
                MessageBox.Show(str1, "Error Message");
            }
        }



        public void fillbackgridSrch(SqlDataAdapter da, string frm)
        {
            try
            {
                frmnm = frm;
                SearchGrid.Rows.Clear();// SearchGrid.Rows.Clear();       
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                SearchGrid.AutoGenerateColumns = true;
                ds.Clear();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    SearchGrid.DataSource = ds.Tables[0];
                    SearchGrid.Columns[0].Width = 357;
                    SearchGrid.Columns[1].Width = 143;
                    SearchGrid.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                }
                else
                {
                    SearchGrid.DataSource = null;
                    dt.Columns.Add("Name", typeof(string));
                    dt.Columns.Add("Code", typeof(string));
                    SearchGrid.DataSource = dt;
                    SearchGrid.Columns[0].Width = 357;
                    SearchGrid.Columns[1].Width = 143;
                    MessageBox.Show("There Is No Record To Display");
                    Close();
                    return;
                }
                SearchGrid.Refresh();// SearchGrid.Refresh();
            }
            catch (Exception ex)
            {
                string stri = "Message:" + ex.Message;
                MessageBox.Show(stri, "Error Message");
            }
        }

        public void srchval(string val, string frm)
        {
            frmnm = frm;
            txtsearch.Text = val;
            txtsearch.Focus();
        }

        public void searchChangeText(string frmNM, string tablename, string searchStr, string Dupcode)
        {
            try
            {
                string wrd;
                wrd = mod.SrchChange(txtsearch);
                DataSet ds1 = new DataSet();
                DataTable dt = new DataTable();
                cmd = new SqlCommand("SP_SEARCHFRMTextChanged", con.connect());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@frmname", frmNM);
                cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                cmd.Parameters.AddWithValue("@tableName", tablename);
                cmd.Parameters.AddWithValue("@text", searchStr);
                cmd.Parameters.AddWithValue("@dupcode", Dupcode);
                cmd.Parameters.AddWithValue("@SearchChangeVariable", Globalvariable.SearchChangeVariable);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                ds1.Clear();
                da.Fill(ds1);
                SearchGrid.AutoGenerateColumns = true;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    SearchGrid.DataSource = ds1.Tables[0];
                    SearchGrid.Columns[0].Width = 357;
                    SearchGrid.Columns[1].Width = 143;
                }
                else
                {
                    SearchGrid.DataSource = null;
                    dt.Columns.Add("Name", typeof(string));
                    dt.Columns.Add("Code", typeof(string));
                    SearchGrid.DataSource = dt;
                    SearchGrid.Columns[0].Width = 357;
                    SearchGrid.Columns[1].Width = 143;
                }

            }
            catch (Exception ex)
            {
                string str1 = "Message:" + ex.Message;
                MessageBox.Show(str1, "Error Message");
            }
        }


        public void txtSearchChange(string firstCol, string secondCol, string tableName, System.Windows.Forms.TextBox txtsearch, string CustType)
        {
            try
            {
                string wrd;
                wrd = mod.SrchChange(txtsearch);
                DataSet ds1 = new DataSet();
                DataTable dt = new DataTable();
                cmd = new SqlCommand("SP_SearchText", con.connect());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tableName", tableName);
                cmd.Parameters.AddWithValue("@code", firstCol);
                cmd.Parameters.AddWithValue("@name", secondCol);
                cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                cmd.Parameters.AddWithValue("@deletYN", "N");
                cmd.Parameters.AddWithValue("@colm_name", "Name");
                cmd.Parameters.AddWithValue("@colm_code", "Code");
                cmd.Parameters.AddWithValue("@text", wrd);
                cmd.Parameters.AddWithValue("@custtype", CustType);
                cmd.Parameters.AddWithValue("@NotIn", NotIn);
                cmd.Parameters.AddWithValue("@SearchChangeVariable", Globalvariable.SearchChangeVariable);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                ds1.Clear();
                da.Fill(ds1);
                SearchGrid.AutoGenerateColumns = true;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    SearchGrid.DataSource = ds1.Tables[0];
                    SearchGrid.Columns[0].Width = 357;
                    SearchGrid.Columns[1].Width = 143;
                }
                else
                {
                    SearchGrid.DataSource = null;
                    dt.Columns.Add("Name", typeof(string));
                    dt.Columns.Add("Code", typeof(string));
                    SearchGrid.DataSource = dt;
                    SearchGrid.Columns[0].Width = 357;
                    SearchGrid.Columns[1].Width = 143;
                }

            }
            catch (Exception ex)
            {
                string str1 = "Message:" + ex.Message;
                MessageBox.Show(str1, "Error Message");
            }
        }

        public void FillSearchGrid_ChangeText(string firstCol, string secondCol, string tableName, System.Windows.Forms.TextBox txtsearch, string custtype, string NameCol = "Name", string CodeCol = "Code")
        {
            try
            {
                string wrd;
                wrd = mod.SrchChange(txtsearch);
                DataSet ds1 = new DataSet();
                DataTable dt = new DataTable();
                cmd = new SqlCommand("SP_SearchText", con.connect());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tableName", tableName);
                cmd.Parameters.AddWithValue("@code", firstCol);
                cmd.Parameters.AddWithValue("@name", secondCol);
                cmd.Parameters.AddWithValue("@branchcode", Globalvariable.bcode);
                cmd.Parameters.AddWithValue("@deletYN", "N");
                cmd.Parameters.AddWithValue("@colm_name", NameCol);
                cmd.Parameters.AddWithValue("@colm_code", CodeCol);
                cmd.Parameters.AddWithValue("@text", wrd);
                cmd.Parameters.AddWithValue("@custtype", custtype);
                cmd.Parameters.AddWithValue("@NotIn", NotIn);

                if (AutoSearchDecision)
                {
                    int intCode = -1;
                    int.TryParse(txtsearch.Text, out intCode);
                    if (intCode > 0)
                    {
                        cmd.Parameters.AddWithValue("@SearchChangeVariable", "SrchByCode");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@SearchChangeVariable", "SrchByName");
                    }

                }
                else
                {
                    cmd.Parameters.AddWithValue("@SearchChangeVariable", Globalvariable.SearchChangeVariable);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                ds1.Clear();
                da.Fill(ds1);
                SearchGrid.AutoGenerateColumns = true;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    SearchGrid.DataSource = ds1.Tables[0];
                    SearchGrid.Columns[0].Width = 357;
                    SearchGrid.Columns[1].Width = 143;
                }
                else
                {
                    SearchGrid.DataSource = null;
                    dt.Columns.Add("Name", typeof(string));
                    dt.Columns.Add("Code", typeof(string));
                    SearchGrid.DataSource = dt;
                    SearchGrid.Columns[0].Width = 357;
                    SearchGrid.Columns[1].Width = 143;
                }

            }
            catch (Exception ex)
            {
                string str1 = "Message:" + ex.Message;
                MessageBox.Show(str1, "Error Message");
            }
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SqlDataReader dr;
                string txt;
                txt = mod.SrchChange(txtsearch);
                if (IsInitialized)
                {
                    FillSearchGrid_ChangeText(CodeColumnName, NameColumnName, TableName, txtsearch, CustomerType, NameColumnDisplayName, CodeColumnDisplayName);
                }
                else
                {
                    switch (frmnm)
                    {
                        case "FrmPurchase":
                            if (Globalvariable.SearchString == "SupplierMaster")
                            {
                                //Search Supplier
                                searchChangeText(Globalvariable.frmName, "tbSupplierMaster", txt, "0");
                            }
                            else if (Globalvariable.SearchString == "MenuItem")
                            {
                                //Search Menu
                                FillSearchGrid_ChangeText("Item_Code", "Item_Name", "tbItemName", txtsearch, "0");
                            }
                            break;
                        case "FrmBankReconcilation":
                            // Search Book Code
                            searchChangeText(frmnm, "tbBook", txt, "0");
                            break;
                        case "RoomMaster":
                            if (Globalvariable.SearchString == "RoomCatgry")
                            {
                                // Search Room Category
                                searchChangeText(frmnm, "RoomCategoryMaster", txt, "0");
                            }
                            else if (Globalvariable.SearchString == "TaxID")
                            {
                                // Search Tax
                                RoomMaster RoomMast = new RoomMaster();
                                da = RoomMast.SearchTax(txt);
                                fillsearchgrid(da);
                            }
                            break;
                        case "DayBook":
                            //Search GeneralLedger
                            searchChangeText(frmnm, "tbGeneralLedger", txt, "0");
                            break;
                        case "FrmReceipt":
                            if (Globalvariable.SearchString == "DayBook")
                            {
                                //Search Daybook
                                searchChangeText(frmnm, "tbBook", txt, "0");
                            }
                            else if (Globalvariable.SearchString == "Party Bank")
                            {
                                //Search Party Bank
                                searchChangeText(frmnm, "tbPartyBank", txt, "0");
                            }
                            else if (Globalvariable.SearchString == "Bank Location")
                            {
                                // Search Bank Location
                                searchChangeText(frmnm, "tbBankLocation", txt, "0");
                            }
                            break;
                        case "FrmPayment":
                            if (Globalvariable.SearchString == "DayBook")
                            {
                                //Search Daybook
                                searchChangeText(frmnm, "tbBook", txt, "0");
                            }
                            else if (Globalvariable.SearchString == "Bank Location")
                            {
                                // Search Bank Location
                                searchChangeText(frmnm, "tbBankLocation", txt, "0");
                            }
                            break;
                        case "FrmJournalVoucher":
                            if (Globalvariable.SearchString == "SupplierMaster")
                            {
                                //Search Ledger Name
                                if (Globalvariable.Cust_Type == "C")
                                {
                                    FillSearchGrid_ChangeText("supplier_id", "supplier_name", "tbSupplierMaster", txtsearch, "C");
                                }
                                else if (Globalvariable.Cust_Type == "D")
                                {
                                    FillSearchGrid_ChangeText("supplier_id", "supplier_name", "tbSupplierMaster", txtsearch, "D");
                                }
                            }
                            else if (Globalvariable.SearchString == "GeneralLedger")
                            {
                                //Search GeneralLedger
                                searchChangeText(frmnm, "tbGeneralLedger", txt, "0");
                            }
                            break;
                        case "FrmPettyCash":
                            //Search GeneralLedger
                            searchChangeText(frmnm, "tbGeneralLedger", txt, "0");
                            break;
                        case "FrmContra":
                            //Search Daybook
                            searchChangeText(frmnm, "tbBook", txt, "0");
                            break;
                        case "TaxMaster":
                            if (Globalvariable.TaxMastVarb == "TallyID")
                            {
                                //Search Tally code               
                                FillSearchGrid_ChangeText("Tally_Code", "Tally_Name", "tbTallyMaster", txtsearch, "0");
                            }
                            else if (Globalvariable.TaxMastVarb == "GLId")
                            {
                                //search GeneralLedger
                                searchChangeText(frmnm, "tbGeneralLedger", txt, "0");
                            }
                            break;
                        //case "FrmTablenumberMaster":
                        //    if (Globalvariable.SearchString == "DayBook")
                        //    {
                        //        //Search Daybook
                        //        searchChangeText(frmnm, "Table_no", "Name", txt, "0");
                        //    }
                        //    else if (Globalvariable.SearchString == "Bank Location")
                        //    {
                        //        // Search Bank Location
                        //        searchChangeText(frmnm, "tbBankLocation", txt, "0");
                        //    }
                        //    break;

                    }
                }
                txtsearch.SelectionStart = txtsearch.Text.Length;

            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }

        }

        private void backgrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void SearchGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (SearchGrid.CurrentCell != null)
                {
                    int i = SearchGrid.CurrentCell.RowIndex;
                    codeselected = SearchGrid.Rows[i].Cells[1].Value.ToString();
                    descselected = SearchGrid.Rows[i].Cells[0].Value.ToString();
                    descselectedfromgrid = descselected;
                    Close();
                }
            }
        }

        public void searchchange(string strinfo)
        {
            try
            {
                this.SearchGrid.Rows.Clear();// backgrid.Rows.Clear();
                this.SearchGrid.Refresh();// backgrid.Refresh();
                SqlCommand cmd = new SqlCommand(strinfo, con.connect());
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        string[] row = new string[] { dr[1].ToString(), dr[0].ToString() };
                        SearchGrid.Rows.Add(row);
                    }
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        public void setText(string txt)
        {
            txtsearch.Text = txt;
        }

        private void txtsearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                int i = SearchGrid.SelectedCells[0].RowIndex + 1;
                if (i < SearchGrid.RowCount)
                {
                    SearchGrid.CurrentCell = SearchGrid.Rows[i].Cells[0];
                    SearchGrid.Rows[i].Selected = true;
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                int i = SearchGrid.SelectedCells[0].RowIndex - 1;
                if (i >= 0)
                {
                    SearchGrid.CurrentCell = SearchGrid.Rows[i].Cells[0];
                    SearchGrid.Rows[i].Selected = true;
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                SearchGrid_KeyDown(sender, e);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void SearchGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = SearchGrid.CurrentCell.RowIndex;
            codeselected = SearchGrid.Rows[i].Cells[1].Value.ToString();
            descselected = SearchGrid.Rows[i].Cells[0].Value.ToString();
            descselectedfromgrid = descselected;
            Close();
        }

        private void txtsearch_Enter(object sender, EventArgs e)
        {
            txtsearch.BackColor = Color.Yellow;
        }

        private void SearchGrid_CellClick(object sender, DataGridViewCellEventArgs e)
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
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void FrmSearch_Shown(object sender, EventArgs e)
        {
            txtsearch.Focus();
        }

    }
}

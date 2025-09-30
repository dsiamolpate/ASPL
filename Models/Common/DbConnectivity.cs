using ASPL.CLASSES;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPL.Models.Common
{
    public sealed class DbConnectivity : DBConnect
    {
        private static DbConnectivity instance = null;
        private static readonly object o = new object();


        private DbConnectivity()
        {

        }

        public static DbConnectivity Instance
        {
            get
            {
                lock (o)
                {
                    if (instance == null)
                    {
                        instance = new DbConnectivity();
                    }
                    return instance;
                }
            }
        }


        public List<CommonOption> GetOptions(string OptionType = null, bool IncludeBranchCode = true)
        {

            Dictionary<string, object> parameters = new Dictionary<string, object>();

            if (OptionType != null)
            {
                parameters.Add("@optiontype", OptionType);
            }

            if (IncludeBranchCode)
            {
                parameters.Add("@branchcode", Globalvariable.bcode);
            }

            var dt = GetDataTableByProcedure("GetCommonOptions", parameters);

            var commonOptions = new List<CommonOption>();

            foreach (DataRow dr in dt.Rows)
            {
                commonOptions.Add(new CommonOption()
                {
                    ID = (int)dr["ID"],
                    OptType = dr["OptionType"].ToString(),
                    OptName = dr["OptionName"].ToString(),
                    OptValue = dr["OptionValue"].ToString()

                });
            }

            return commonOptions;

        }

        public string GetValuFromDB(string TableName, string ReturnColumnName, string CompareColumnName, string ValueToCompare)
        {
            var dr = GetDataRowByQuery($"Select {ReturnColumnName} From {TableName} Where {CompareColumnName} = '{ValueToCompare}'");
            return dr[0].ToString();
        }

        public DataRow GetLiquorGroupDetails(string LiquorGroupCode)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@branchcode", Globalvariable.bcode);
            parameters.Add("@code", LiquorGroupCode);

            return DbConnectivity.Instance.GetDataRowByProcedure("GetLiquorGroupDetails", parameters);

        }

        public List<CommonOption> GetMeasurementForLiqBrand(string code, string currentCode)
        {

            Dictionary<string, object> parameters = new Dictionary<string, object>();

            if (code != null)
            {
                parameters.Add("@code", code);
            }

            if (currentCode != null)
            {
                parameters.Add("@currcode", currentCode);
            }

            parameters.Add("@branchcode", Globalvariable.bcode);

            var dt = GetDataTableByProcedure("GetMeasurementForLiqBrand", parameters);

            var commonOptions = new List<CommonOption>();


            foreach (DataRow dr in dt.Rows)
            {
                commonOptions.Add(new CommonOption()
                {
                    ID = (int)dr["ID"],
                    OptType = dr["OptionType"].ToString(),
                    OptName = dr["OptionName"].ToString(),
                    OptValue = dr["OptionValue"].ToString()

                });
            }

            return commonOptions;

        }

        public DataRow GetGroupDetails(string GroupCode)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Branch_Code", Globalvariable.bcode);
            parameters.Add("@Code", GroupCode);

            return DbConnectivity.Instance.GetDataRowByProcedure("GetGroupDetails", parameters);

        }

        public DataRow GetMenuCard(string MenuCode)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Branch_Code", Globalvariable.bcode);
            parameters.Add("@Code", MenuCode);

            return DbConnectivity.Instance.GetDataRowByProcedure("GetMenuCard", parameters);

        }

        public DataRow GetKOTSectionWithMenuCard(string CodeType, string Code)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Branch_Code", Globalvariable.bcode);
            parameters.Add("@CodeType", CodeType);
            parameters.Add("@Code", Code);

            return DbConnectivity.Instance.GetDataRowByProcedure("GetKOTSectionWithMenuCard", parameters);

        }

        public bool ManageCalInsentive(int Menu_Code, decimal Sunday_Inc, decimal Monday_Inc, decimal Tuesday_Inc,
            decimal Wednesday_Inc, decimal Thursday_Inc, decimal Friday_Inc, decimal Saturday_Inc)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Branch_Code", Globalvariable.bcode);
            parameters.Add("@Edited_By", Globalvariable.usercd);
            parameters.Add("@Menu_Code", Menu_Code);
            parameters.Add("@V_Sunday_Inc", Sunday_Inc);
            parameters.Add("@V_Monday_Inc", Monday_Inc);
            parameters.Add("@V_Tuesday_Inc", Tuesday_Inc);
            parameters.Add("@V_Wednesday_Inc", Wednesday_Inc);
            parameters.Add("@V_Thursday_Inc", Thursday_Inc);
            parameters.Add("@V_Friday_Inc", Friday_Inc);
            parameters.Add("@V_Saturday_Inc", Saturday_Inc);

            return SetDataByProcedure("ManageCalInsentive", parameters);

        }

        public bool DeleteCalInsentive(string Menu_Code)
        {

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Branch_Code", Globalvariable.bcode);
            parameters.Add("@Edited_By", Globalvariable.usercd);
            parameters.Add("@Menu_Code", Menu_Code);
            parameters.Add("@DeleteYN", "Y");

            return SetDataByProcedure("ManageCalInsentive", parameters);

        }

        public DataRow GetCalInsentiveDetails(int Menu_Code, string DeletedYN)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Branch_Code", Globalvariable.bcode);
            parameters.Add("@Menu_Code", Menu_Code);
            parameters.Add("@DeletedYN", DeletedYN);

            return GetDataRowByProcedure("GetCalInsentives", parameters);
        }

        public bool SaveMarketSegment(string operationName, string Code, string SegName)
        {

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@branchcode", Globalvariable.bcode);
            parameters.Add("@variable", operationName.ToUpper() == "SAVE" ? "INSERT" : "UPDATE");
            parameters.Add("@code", Code);
            parameters.Add("@name", SegName.Replace("'", "''"));

            return SetDataByProcedure("SP_INSERTMarketseg", parameters);
        }

        public bool SaveCustomerInfo(string variable, string Customer_id, string Prefix, string Customer_Name, string Address1, string Address2, string Address3,
            string Mobile_no1, string Mobile_no2, string Email_id, DateTime DOB, string StateName, string CityName, string Zip_id, string Deleted,
            string Leger_id, string Nationality, string PanCard, string PassportNo, DateTime PassportIssue, DateTime PassportExp, string VisaNo,
            DateTime VisaIssue, DateTime VisaExp, DataTable dtDocuments)
        {
            //Main Table
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@variable", variable);
                parameters.Add("@Customer_id", Customer_id);
                parameters.Add("@Prefix", Prefix);
                parameters.Add("@Customer_name", Customer_Name.Replace("'", "''"));
                parameters.Add("@Address1", Address1.Replace("'", "''"));
                parameters.Add("@Address2", Address2.Replace("'", "''"));
                parameters.Add("@Address3", Address3.Replace("'", "''"));
                parameters.Add("@Mobile_no1", Mobile_no1);
                parameters.Add("@Mobile_no2", Mobile_no2);
                parameters.Add("@Email_id", Email_id);
                parameters.Add("@DOB", DOB);
                parameters.Add("@StateName", StateName.Replace("'", "''"));
                parameters.Add("@CityName", CityName.Replace("'", "''"));
                parameters.Add("@Zip_id", Zip_id);
                parameters.Add("@User_id", Globalvariable.usercd);
                parameters.Add("@Deleted", Deleted);
                parameters.Add("@Branchcode", Globalvariable.bcode);
                parameters.Add("@Leger_id", Leger_id);
                parameters.Add("@Nationality", Nationality);
                parameters.Add("@PanCard", PanCard);
                parameters.Add("@PassportNo", PassportNo);
                parameters.Add("@PassportIssue", PassportIssue);
                parameters.Add("@PassportExp", PassportExp);
                parameters.Add("@VisaNo", VisaNo);
                parameters.Add("@VisaIssue", VisaIssue);
                parameters.Add("@VisaExp", VisaExp);

                SetDataByProcedure("SaveCustomerInfo", parameters);

            }

            //Delete Documents
            GetDataSetByQuery($"Delete From tbCustomerIdentityDetail Where cust_id = '{Customer_id}' AND Branchcode = '{Globalvariable.bcode}'");

            //Add Documents
            foreach (DataRow drDocument in dtDocuments.Rows)
            {

                List<SqlParameter> parameters = new List<SqlParameter>();
                var imageParam = new SqlParameter("@Document_Detail", SqlDbType.Image);
                imageParam.Value = ((Image)drDocument["document_detail"]).ConvertToByte();

                parameters.Add(imageParam);
                parameters.Add(new SqlParameter("@Cust_ID", Customer_id));
                parameters.Add(new SqlParameter("@Branch_Code", Globalvariable.bcode));
                parameters.Add(new SqlParameter("@User_ID", Globalvariable.usercd));

                GetDataSetByProcedure("SaveCustomerDocument", parameters.ToArray());

            }

            return true;
        }

        public List<Image> GetCustomerDocuments(string Cust_ID)
        {
            var dtData = GetDataTableByQuery($"SELECT document_detail FROM tbCustomerIdentityDetail WHERE cust_id = '{Cust_ID}' AND Branchcode='{Globalvariable.bcode}'");
            List<Image> lstImages = new List<Image>();
            if (dtData.Rows.Count > 0)
            {
                foreach (DataRow dr in dtData.Rows)
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream((byte[])dr[0]);
                    lstImages.Add(new Bitmap(ms));
                }
            }

            return lstImages;
        }

        public DataRow GetMenuCategoryDetails(string Category_Code)
        {

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Category_Code", Category_Code);
            parameters.Add("@Branch_Code", Globalvariable.bcode);

            return GetDataRowByProcedure("GetMenuCategoryDetails", parameters);

        }

        public DataRow GetItemPurchaseSaleRatesWithCategoryType(string Item_Code)
        {
            Item_Code = Item_Code == null || Item_Code.Trim().Length == 0 ? "0" : Item_Code;
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Item_Code", Item_Code);
            parameters.Add("@Branch_Code", Globalvariable.bcode);
            parameters.Add("@Company_SrNo", Globalvariable.company_Srno);

            return GetDataRowByProcedure("GetItemPurchaseSaleRatesWithCategoryType", parameters);

        }

        public DataTable ManageItemGroup(string ProcessName, string Code, string Name = null, string ApplyTax = null, string TaxCode = null, string TaxPercentage = null)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@variable", ProcessName);
            parameters.Add("@code", Code);
            if (Name != null)
            {
                parameters.Add("@name", Name);
            }

            if (ApplyTax != null)
            {
                parameters.Add("@ApplyTax", ApplyTax);
            }

            if (TaxCode != null)
            {
                parameters.Add("@TaxCode", TaxCode);
            }

            if (TaxPercentage != null)
            {
                parameters.Add("@TaxPercentage", TaxPercentage);
            }

            parameters.Add("@branchcode", Globalvariable.bcode);

            return GetDataTableByProcedure("SP_FrmItemGroup", parameters);
        }

        public DataTable ManageMenuCategory(string ProcessName, string Code, string Name = null, string TallyCode = null, string cType= null, 
                                                string AllowDiscount = null, string MaxDiscount = null, string AutoDiscVal = null,
                                                string UnitID = null, string TaxCode = null, string TaxPercentage = null)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@variable", ProcessName);
            parameters.Add("@code", Code);
            if (Name != null)
            {
                parameters.Add("@name", Name);
            }

            if (TallyCode != null)
            {
                parameters.Add("@tallycode", TallyCode);
            }

            if (cType != null)
            {
                parameters.Add("@type", cType);
            }

            if (AllowDiscount != null)
            {
                parameters.Add("@Allow_Discount", AllowDiscount);
            }

            if (MaxDiscount != null)
            {
                parameters.Add("@Max_Discount", MaxDiscount);
            }

            if (AutoDiscVal != null)
            {
                parameters.Add("@Auto_Disc_val", AutoDiscVal);
            }

            if (UnitID != null)
            {
                parameters.Add("@UnitID", UnitID);
            }

            if (TaxCode != null)
            {
                parameters.Add("@TaxCode", TaxCode);
            }

            if (TaxPercentage != null)
            {
                parameters.Add("@TaxPercentage", TaxPercentage);
            }

            parameters.Add("@branchcode", Globalvariable.bcode);

            return GetDataTableByProcedure("SP_RESTMENUCAT", parameters);
        }

        public DataTable ManageItemMaster(string ProcessName, string Code = null, string Name = null, string Payable = null, string TallyCode = null,
            string ItemRate = null, string Category = null, string ServiceTax = null, string ResSaleMode = null, string IsActive = null)
        {

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@variable", ProcessName);
            parameters.Add("@code", Code);

            if (Name != null)
            {
                parameters.Add("@name", Name.Replace("'","''"));
            }

            if (Payable != null)
            {
                parameters.Add("@payble", Payable);
            }

            if (TallyCode != null)
            {
                parameters.Add("@tallycode", TallyCode);
            }

            if (ItemRate != null)
            {
                parameters.Add("@itemrate", ItemRate);
            }

            if (Category != null)
            {
                parameters.Add("@categry", Category);
            }
            if (ServiceTax != null)
            {
                parameters.Add("@servicetax", ServiceTax);
            }

            if (ResSaleMode != null)
            {
                parameters.Add("@restasalemode", ResSaleMode);
            }

            if (IsActive != null)
            {
                parameters.Add("@active", IsActive);
            }

            parameters.Add("@branchcode", Globalvariable.bcode);

            return GetDataTableByProcedure("SP_INSERTItemMaster", parameters);

        }

        public DataTable ManageItemGroupTaxDetail(string ProcessName, string Item_Code =  null, string Tax_Code = null, string Tax_Percentage = null)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@variable", ProcessName);

            if (Item_Code != null)
            {
                parameters.Add("@Item_Code", Item_Code);
            }

            if (Tax_Code != null)
            {
                parameters.Add("@Tax_Code", Tax_Code);
            }

            if (Tax_Percentage != null)
            {
                parameters.Add("@Tax_Percentage", Tax_Percentage);
            }
            
            parameters.Add("@Branchcode", Globalvariable.bcode);

            return GetDataTableByProcedure("proc_ManageItemGroupTaxDetail", parameters);

        }

        public DataRow GetCustomerInformation(string code)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            parameters.Add("@code", code);
            parameters.Add("@Branchcode", Globalvariable.bcode);

            return  GetDataRowByProcedure("proc_GetCustomerInformation", parameters);

        }

    }
}

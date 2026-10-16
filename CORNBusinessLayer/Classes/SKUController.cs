using System;
using System.Data;
using System.IO;

using CORNCommon.Classes;
using CORNDataAccessLayer.Classes;
using CORNDatabaseLayer.Classes;

namespace CORNBusinessLayer.Classes
{
    /// <summary>
    /// Class For SKU Related Tasks
    /// <example>
    /// <list type="bullet">
    /// <item>
    /// Insert SKU
    /// </item>
    /// <term>
    /// Update SKU
    /// </term>
    /// <item>
    /// Get SKU
    /// </item>
    /// </list>
    /// </example>
    /// </summary>
	public class SkuController
	{	
		#region Constructors

        /// <summary>
        /// Constructor For SkuController
        /// </summary>
		public SkuController()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
        #endregion
				
		#region public Methods

        #region Select

        /// <summary>
        /// Gets SKUS Data
        /// </summary>
        /// <remarks>
        /// Returns SKUS Data as Datatable
        /// </remarks>
        /// <param name="p_company_id">Principal</param>
        /// <param name="p_division_id">Dicision</param>
        /// <param name="p_category_id">Category</param>
        /// <param name="p_brand_id">Brand</param>
        /// <param name="Companyid">Company</param>
        /// <returns>SKUS Data as Datatable</returns>
        public DataTable SelectSkuInfo(int p_company_id, int p_division_id, int p_category_id, int p_brand_id, int Companyid)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspSelectSkuInfo mspSelectSkuInfo = new uspSelectSkuInfo();
                mspSelectSkuInfo.Connection = mConnection;
                mspSelectSkuInfo.brand_id = p_brand_id;
                mspSelectSkuInfo.category_id = p_category_id;
                mspSelectSkuInfo.Principal_id = p_company_id;
                
                mspSelectSkuInfo.division_id = p_division_id;
                mspSelectSkuInfo.Company_id = Companyid;

                DataTable dt = mspSelectSkuInfo.ExecuteTable();

                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public DataTable SelectSkuPrice(int p_Distributor_ID,int p_Principal_id, int p_gender_ID, int p_category_id, int p_brand_id, int Companyid)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspSelectSkuPrice mspSelectSkuInfo = new uspSelectSkuPrice();
                mspSelectSkuInfo.Connection = mConnection;
                mspSelectSkuInfo.brand_id = p_brand_id;
                mspSelectSkuInfo.category_id = p_category_id;
                mspSelectSkuInfo.Principal_id = p_Principal_id;

                mspSelectSkuInfo.division_id = p_gender_ID;
                mspSelectSkuInfo.Company_id = Companyid;
                mspSelectSkuInfo.Distributor_id = p_Distributor_ID;

                DataTable dt = mspSelectSkuInfo.ExecuteTable();

                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public DataTable SelectSkuInfo2(int pCompanyId, int pDivisionId, int pCategoryId, int pBrandId, int companyid,int pTagid)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspSelectSkuInfo2 mspSelectSkuInfo = new uspSelectSkuInfo2();
                mspSelectSkuInfo.brand_id = pBrandId;
                mspSelectSkuInfo.category_id = pCategoryId;
                mspSelectSkuInfo.Principal_id = pCompanyId;
                mspSelectSkuInfo.Connection = mConnection;
                mspSelectSkuInfo.division_id = pDivisionId;
                mspSelectSkuInfo.Company_id = companyid;
                mspSelectSkuInfo.TAG_ID = pTagid;

                DataTable dt = mspSelectSkuInfo.ExecuteTable();

                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
 
        /// <summary>
        /// Gets SKU Data
        /// </summary>
        /// <remarks>
        /// Returns SKUS Data as Datatable
        /// </remarks>
        /// <param name="p_SKU_Id">SKU</param>
        /// <param name="Companyid">Company</param>
        /// <returns>SKUS Data as Datatable</returns>
        public DataTable SelectSkuData(int p_SKU_Id, int Companyid)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectSKUS mSkuInfo = new spSelectSKUS();
                mSkuInfo.Connection = mConnection;
                mSkuInfo.SKU_ID = p_SKU_Id;
                mSkuInfo.COMPANY_ID = Companyid;
                DataTable dt = mSkuInfo.ExecuteTable();
                return dt;

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        public DataTable GetSkuData(int p_CategoryID, int p_PrincipalID, int p_CountryID, int p_GenderID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectSKUS mSkuInfo = new spSelectSKUS();
                mSkuInfo.Connection = mConnection;
                mSkuInfo.CATEGORY_ID = p_CategoryID;
                mSkuInfo.PRINCIPAL_ID = p_PrincipalID;
                mSkuInfo.Country_ID = p_CountryID;
                mSkuInfo.Gender_id = p_GenderID;
                DataTable dt = mSkuInfo.ExecuteTable();
                return dt;

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        /// <summary>
        /// Gets SKU UOM
        /// </summary>
        /// <param name="p_UOM_Id">UOM</param>
        /// <param name="p_UOM_Desc">Description</param>
        /// <returns>SKU UOM</returns>
        public DataTable SelectUOMs(int p_UOM_Id, string p_UOM_Desc)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spSelectUOMS mUOMs = new spSelectUOMS();
                mUOMs.Connection = mConnection;

                mUOMs.UOM_ID = p_UOM_Id;
                mUOMs.UOM_DESC = p_UOM_Desc;
                mUOMs.TIME_STAMP = Constants.DateNullValue;
                mUOMs.STATUS = Constants.IntNullValue;

                DataTable dt = mUOMs.ExecuteTable();
                return dt;

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public DataTable SelectCountry(int pType)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spSelectCountrySku mUoMs = new spSelectCountrySku();
                mUoMs.Connection = mConnection;
                mUoMs.TYPE = pType;
                 DataTable dt = mUoMs.ExecuteTable();
                return dt;

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        #endregion
        public DataTable SearchProduct(string pSearchText)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspSearchProduct mspSelectSkuInfo = new uspSearchProduct();
                mspSelectSkuInfo.Connection = mConnection;
                mspSelectSkuInfo.SEARCH_TEXT = pSearchText;
                DataTable dt = mspSelectSkuInfo.ExecuteTable();

                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        public DataTable SearchProduct(string pSearchText,int pDistributorId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspSearchProduct mspSelectSkuInfo = new uspSearchProduct();
                mspSelectSkuInfo.Connection = mConnection;
                mspSelectSkuInfo.SEARCH_TEXT = pSearchText;
                mspSelectSkuInfo.DistributorId = pDistributorId;
                DataTable dt = mspSelectSkuInfo.ExecuteTable();

                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        #region Insert, Update

        /// <summary>
        /// Inserts Or Updates SKU Price From Excel File
        /// </summary>
        /// Returns True On Success And False On Failure
        /// <param name="p_DistributorId">Location</param>
        /// <param name="pFileName">ExcelFile</param>
        /// <param name="p_Principal_Id">Principal</param>
        /// <returns>True On Success And False On Failure</returns>
        public bool ImportSKUS(int p_DistributorId, string pFileName, int p_Principal_Id, int p_Company_ID, int p_UserId)
        {
            IDbConnection mConnection = null;
            FileStream Sourcefile = null;
            StreamReader ReadSourceFile = null;
            IDbTransaction mTransaction = null;
            DataControl DC = new DataControl();

            mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
            mConnection.Open();
            mTransaction = ProviderFactory.GetTransaction(mConnection);

            Sourcefile = new FileStream(pFileName, FileMode.Open);
            ReadSourceFile = new StreamReader(Sourcefile);
            string FileContents = "";
            try
            {
                while ((FileContents = ReadSourceFile.ReadLine()) != null)
                {

                    string[] ParametersArr = FileContents.Split(Constants.File_Delimiter);
                    uspImportSKUS mSKUS = new uspImportSKUS();
                    mSKUS.Connection = mConnection;
                    mSKUS.Transaction = mTransaction;
                    mSKUS.PRINCIPAL_ID = p_Principal_Id;
                    mSKUS.ISEXEMPTED = true;
                    mSKUS.ISACTIVE = true;
                    mSKUS.GST_ON = 'T';
                    mSKUS.COMPANY_ID = p_Company_ID;
                    mSKUS.SKU_CODE = ParametersArr[0].ToString();
                    mSKUS.SKU_NAME = ParametersArr[1].ToString();
                    mSKUS.BAR_CODE = ParametersArr[2].ToString();
                    mSKUS.DIVISION_ID = Convert.ToInt32(DC.chkNull_0(ParametersArr[3]));
                    mSKUS.CATEGORY_ID = Convert.ToInt32(DC.chkNull_0(ParametersArr[4]));
                    mSKUS.BRAND_ID = Convert.ToInt32(DC.chkNull_0(ParametersArr[5]));
                    mSKUS.SKU_TAG_ID = int.Parse(DC.chkNull_0(ParametersArr[6].ToString()));
                    mSKUS.COLOR = ParametersArr[7].ToString();                    
                    mSKUS.PACKSIZE = ParametersArr[8].ToString();
                    mSKUS.SKU_SEASON = ParametersArr[9].ToString();
                    mSKUS.SKU_COUNTRY = ParametersArr[10].ToString();
                    mSKUS.GST_RATE_REG = 0;
                    mSKUS.GST_RATE_UNREG = 0;                    
                    mSKUS.TIME_STAMP = System.DateTime.Now;
                    mSKUS.LASTUPDATE_DATE = System.DateTime.Now;
                    mSKUS.IP_ADDRESS = null;                    
                    mSKUS.USER_ID = p_UserId;

                    mSKUS.ExecuteQuery();
                }
                mTransaction.Commit();
                return true;
                

            }

            catch (Exception excp)
            {
                mTransaction.Rollback();
                ReadSourceFile.Close();
                mConnection.Close();
                //ExceptionPublisher.PublishException(excp);
               // throw;
                return false;

            }
            finally
            {
                ReadSourceFile.Close();
                mConnection.Close();

            }
        }

        public bool ImportSKUS_ZB(int p_DistributorId, string pFileName, int p_Principal_Id, int p_Company_ID, int p_UserId)
        {
            IDbConnection mConnection = null;
            FileStream Sourcefile = null;
            StreamReader ReadSourceFile = null;
            IDbTransaction mTransaction = null;
            DataControl DC = new DataControl();

            mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
            mConnection.Open();
            mTransaction = ProviderFactory.GetTransaction(mConnection);

            Sourcefile = new FileStream(pFileName, FileMode.Open);
            ReadSourceFile = new StreamReader(Sourcefile);
            string FileContents = "";
            try
            {
                SETTINGS_TABLE_Controller mAutoCode = new SETTINGS_TABLE_Controller();
                var CountID = "";
                var Tempcount =0;
                int UserId =  int.Parse(mAutoCode.GetAutoCustomerCode("SKU", 0, Constants.LongNullValue).ToString());
                    
                while ((FileContents = ReadSourceFile.ReadLine()) != null)
                {

                    string[] ParametersArr = FileContents.Split(Constants.File_Delimiter);
                    spInsertSKUS mSKUS = new spInsertSKUS();
                    mSKUS.Connection = mConnection;
                    mSKUS.Transaction = mTransaction;
                   
                    
                    mSKUS.ISACTIVE = true;
                   
                    mSKUS.COMPANY_ID = p_Company_ID;
                    mSKUS.SKU_CODE = ParametersArr[0].ToString();//ok
                    mSKUS.PRINCIPAL_ID = Convert.ToInt32(DC.chkNull_0(ParametersArr[1]));//ok
                    mSKUS.Gender_ID =Convert.ToInt32(DC.chkNull_0(ParametersArr[2]));//ok
                    mSKUS.CATEGORY_ID = Convert.ToInt32(DC.chkNull_0(ParametersArr[3]));//ok
                    mSKUS.BRAND_Name = (DC.chkNull_0(ParametersArr[4]));//ok
                    mSKUS.SKU_NAME = ParametersArr[5].ToString();//ok
                    mSKUS.TYPE_ID = int.Parse(DC.chkNull_0(ParametersArr[6].ToString()));//ok
                    mSKUS.SKU_SEASON = ParametersArr[7].ToString();//ok
                    mSKUS.COLOR_ID = Convert.ToInt32(DC.chkNull_0(ParametersArr[8]));//ok
                    mSKUS.SIZE_ID = Convert.ToInt32(DC.chkNull_0(ParametersArr[9]));//ok
                    mSKUS.Country_ID =Convert.ToInt32(DC.chkNull_0(ParametersArr[10]));//ok
                    mSKUS.UNITS_IN_CASE =Convert.ToInt16(DC.chkNull_0(ParametersArr[11]));//ok
                    mSKUS.REMARKS = ParametersArr[12].ToString();//ok
                    mSKUS.PicUrl = ParametersArr[13].ToString();//ok
                    mSKUS.TIME_STAMP = System.DateTime.Now;
                    mSKUS.LASTUPDATE_DATE = System.DateTime.Now;
                    mSKUS.IP_ADDRESS = null;
                    mSKUS.USER_ID = p_UserId;
                    #region Bar Code Generate
                    #region Max Sku Id
                   

                    #region user Id

                    if (ParametersArr[14].ToString() != "")
                    { // only assign value when column not empty
                        Tempcount = Convert.ToInt32(DC.chkNull_0(ParametersArr[14]));
                        UserId = int.Parse(mAutoCode.GetAutoCustomerCode("SKU", 0, Constants.LongNullValue).ToString());
                        mAutoCode.GetAutoCustomerCode("SKU", 1, long.Parse(UserId.ToString()));
                    }
                    
                    
                    string strcode = "";
                    // long UserId = long.Parse(mAutoCode.GetAutoSkuCode().ToString());
                  //   UserId = int.Parse(mAutoCode.GetAutoCustomerCode("SKU", 0, Constants.LongNullValue).ToString());
                    if (UserId.ToString().Length == 1)
                    {
                        strcode = "00000" + UserId.ToString();
                    }
                    else if (UserId.ToString().Length == 2)
                    {
                        strcode = "0000" + UserId.ToString();
                    }
                    else if (UserId.ToString().Length == 3)
                    {
                        strcode = "000" + UserId.ToString();
                    }
                    else if (UserId.ToString().Length == 4)
                    {
                        strcode = "00" + UserId.ToString();
                    }
                    else if (UserId.ToString().Length == 5)
                    {
                        strcode = "0" + UserId.ToString();
                    }
                    else
                    {
                        strcode = UserId.ToString();
                    }

                    #endregion
                    #region Country ID
                    //2 Digit of Country ID
                    var Tempcountid = mSKUS.Country_ID.ToString();
                    var country = Tempcountid.Substring(Math.Max(0, Tempcountid.Length - 2));
                    if (country.Length == 1)
                    {
                        country = "0" + country;
                    }
                    //
                    #endregion

                   
                    if (Tempcount.ToString().Length == 1)
                    {
                        CountID = "0" + Tempcount.ToString();
                    }
                    else
                    {
                        CountID = Tempcount.ToString();
                    }
                    #endregion

                    var Color = "";
                    var ColorID = mSKUS.COLOR_ID.ToString();
                    Color = ColorID.Substring(Math.Max(0, ColorID.Length - 2));
                    if (Color.Length == 1)
                    {
                        Color = "0" + Color;
                    }

                    var BarCode  = country + CountID + Color + strcode;
                    #endregion
                    mSKUS.BAR_CODE =BarCode;
                    mSKUS.ExecuteQuery();
                }
                mTransaction.Commit();
                return true;


            }

            catch (Exception excp)
            {
                mTransaction.Rollback();
                ReadSourceFile.Close();
                mConnection.Close();
                //ExceptionPublisher.PublishException(excp);
                // throw;
                return false;

            }
            finally
            {
                ReadSourceFile.Close();
                mConnection.Close();

            }
        }

        public string InsertSKUS2(bool pIsActive, int pCompanyId, int pDivisionId, int pCategoryId, int pSubCategoryId, string pCategoryType, int pBrandId, int pVariantId, short pUnitsInCase,
            short pUnitsInCase2, string pSkuCode, string pSkuName,string pPerPc, string pPackSize, int pUserId, int companyid, string pBarCode, string pColor
            , int pSkuTagId, string pSkuCountry, string pSkuSeason, string pPc1, string pPc2, string pPc3, string pPicUrl, bool pDisplay)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertSKUS2 mSkus = new spInsertSKUS2();

                mSkus.Connection = mConnection;
                mSkus.PRINCIPAL_ID = pCompanyId;               
                mSkus.ISACTIVE = pIsActive;                
                mSkus.COMPANY_ID = companyid;
                mSkus.DIVISION_ID = pDivisionId;
                mSkus.BRAND_ID = pBrandId;
                mSkus.CATEGORY_ID = pCategoryId;
                mSkus.SUBCATEGORY_ID = pSubCategoryId;
                mSkus.CATEGORY_TYPE = pCategoryType;
                mSkus.COLOR = pColor;
                mSkus.BAR_CODE = pBarCode;               
                mSkus.SKU_TAG_ID = pSkuTagId;
                mSkus.SKU_SEASON = pSkuSeason;
                mSkus.SKU_COUNTRY = pSkuCountry;
                mSkus.UNITS_IN_CASE = pUnitsInCase;
                mSkus.UNITS_IN_CASE2 = pUnitsInCase2;
                mSkus.SKU_NAME = pSkuName;
                mSkus.SKU_CODE = pSkuCode;
                mSkus.TIME_STAMP = System.DateTime.Now;
                mSkus.LASTUPDATE_DATE = System.DateTime.Now;
                mSkus.IP_ADDRESS = pPerPc;
                mSkus.PACKSIZE = pPackSize;
                mSkus.USER_ID = pUserId;
                mSkus.PC1 = pPc1;
                mSkus.PC2 = pPc2;
                mSkus.PC3 = pPc3;
                mSkus.PicUrl = pPicUrl;
                mSkus.Display = pDisplay;
                mSkus.ExecuteQuery();
                return mSkus.SKU_ID.ToString();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return exp.Message;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }


        public string InsertSKUSZB(bool pIsActive, int pPrincipalId, int pGenderId, int pCountryId, int pCategoryId, int pTypeID, string pBarCode,
            string pStyleCode ,string pIteamName,
            int pColorID, int pFabricId,int pSize,string pBrandName,int pSeasonID, short pUnitsInCase,
           int pUserId, int companyid,string pPicUrl, bool pDisplay, string pRemarks, string pColorName)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertSKUS mSkus = new spInsertSKUS();

                mSkus.Connection = mConnection;
                mSkus.PRINCIPAL_ID = pPrincipalId;
                mSkus.Gender_ID = pGenderId;
                mSkus.Country_ID = pCountryId;
                mSkus.CATEGORY_ID = pCategoryId;
                mSkus.TYPE_ID = pTypeID;
                mSkus.BAR_CODE = pBarCode;
                mSkus.SKU_CODE = pStyleCode;
                mSkus.SKU_NAME = pIteamName;
                mSkus.COLOR_ID = pColorID;
                mSkus.COLOR = pColorName;
                mSkus.FABRIC_ID = pFabricId;
                mSkus.SIZE_ID = pSize;
                mSkus.BRAND_Name = pBrandName;
                mSkus.SKU_SEASON = pSeasonID.ToString();
                mSkus.UNITS_IN_CASE = pUnitsInCase;
                mSkus.USER_ID = pUserId;
                mSkus.COMPANY_ID = companyid;
                mSkus.PicUrl = pPicUrl;
                mSkus.Display = pDisplay;
                mSkus.REMARKS = pRemarks;

                mSkus.ISACTIVE = pIsActive;
                
                mSkus.TIME_STAMP = System.DateTime.Now;
                mSkus.LASTUPDATE_DATE = System.DateTime.Now;
                
                mSkus.ExecuteQuery();
                return mSkus.SKU_ID.ToString();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return exp.Message;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }




        public string UpdateSKUSZB( int pSkuID, bool pIsActive, int pPrincipalId, int pGenderId, int pCountryId, int pCategoryId, int pTypeID, string pBarCode,
           string pStyleCode, string pIteamName,
           int pColorID, int pFabricId, int pSize, string pBrandName, int pSeasonID, short pUnitsInCase,
          int pUserId, int companyid, string pPicUrl, bool pDisplay, string pRemarks, string pColorName)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateSKUS2 mSkus = new spUpdateSKUS2();

                mSkus.Connection = mConnection;
                mSkus.SKU_ID = pSkuID;
                    
                mSkus.PRINCIPAL_ID = pPrincipalId;
                mSkus.Gender_id = pGenderId;
                mSkus.Country_ID = pCountryId;
                mSkus.CATEGORY_ID = pCategoryId;
                mSkus.TYPE_ID = pTypeID;
                mSkus.BAR_CODE = pBarCode;
                mSkus.SKU_CODE = pStyleCode;
                mSkus.SKU_NAME = pIteamName;
                mSkus.COLOR_ID = pColorID;
                mSkus.COLOR = pColorName;
                mSkus.FABRIC_ID = pFabricId;
                mSkus.SIZE_ID = pSize;
                mSkus.BRAND_NAME = pBrandName;
                mSkus.SKU_SEASON = pSeasonID.ToString();
                mSkus.UNITS_IN_CASE = pUnitsInCase;
                mSkus.USER_ID = pUserId;
                mSkus.COMPANY_ID = companyid;
                mSkus.PicUrl = pPicUrl;
                mSkus.Display = pDisplay;
                mSkus.REMARKS = pRemarks;

                mSkus.ISACTIVE = pIsActive;

                mSkus.TIME_STAMP = System.DateTime.Now;
                mSkus.LASTUPDATE_DATE = System.DateTime.Now;

                mSkus.ExecuteQuery();
                return mSkus.SKU_ID.ToString();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return exp.Message;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }




        public string InsertSKUS(bool p_IsExempted, bool p_IsActive, char p_Gst_On, int p_Company_Id, int p_Division_Id, int p_Category_Id, int p_Brand_Id, int p_Variant_Id, decimal p_GST_Rate_Reg, decimal p_GST_Rate_Unreg, short p_Units_In_Case, string p_Sku_Code, string p_Sku_Name, string p_Ip_Address, string p_packSize, int p_UserId, int Companyid)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertSKUS mSkus = new spInsertSKUS();

                mSkus.Connection = mConnection;
               // mSkus.PRINCIPAL_ID = p_Company_Id;
               // mSkus.ISEXEMPTED = p_IsExempted;
               // mSkus.ISACTIVE = p_IsActive;
               //// mSkus.GST_ON = p_Gst_On;
               // mSkus.COMPANY_ID = Companyid;
               // mSkus.DIVISION_ID = p_Division_Id;
               // mSkus.BRAND_ID = p_Brand_Id;
               // mSkus.CATEGORY_ID = p_Category_Id;
            

               // if (!p_IsExempted)
               // {
               //     mSkus.GST_RATE_REG = p_GST_Rate_Reg;
               //     mSkus.GST_RATE_UNREG = p_GST_Rate_Unreg;
               // }
               // else
               // {
               //     mSkus.GST_RATE_REG = 0;
               //     mSkus.GST_RATE_UNREG = 0;
               // }
               // mSkus.UNITS_IN_CASE = p_Units_In_Case;
               // mSkus.SKU_NAME = p_Sku_Name;
               // mSkus.SKU_CODE = p_Sku_Code;
               // mSkus.TIME_STAMP = System.DateTime.Now;
               // mSkus.LASTUPDATE_DATE = System.DateTime.Now;
               // mSkus.IP_ADDRESS = p_Ip_Address;
               // mSkus.PACKSIZE = p_packSize;
               // mSkus.USER_ID = p_UserId;

                mSkus.ExecuteQuery();

                return mSkus.SKU_ID.ToString();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return exp.Message;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        
        public string UpdateSKUS(bool p_IsActive, int p_Sku_Id, int p_UserId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateSKUS mSkus = new spUpdateSKUS();

                mSkus.Connection = mConnection;
                
                mSkus.ISACTIVE = p_IsActive;
                mSkus.SKU_ID = p_Sku_Id;
                mSkus.USER_ID = p_UserId;

                mSkus.ExecuteQuery();
                return "Record Updated";

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }


        public string UpdateSKUS2(bool pIsActive, int pCompanyId, int pDivisionId, int pCategoryId, int pSubCategoryId, string pCategoryType, int pBrandId, int pVariantId, short pUnitsInCase, short pUnitsInCase2
            , int pSkuId, string pSkuCode, string pSkuName, string pPerPc, string pPackSize, int pUserId, int companyId, string pBarCode, string pColor,
            int pSkuTagId, string pSkuCountry, string pSkuSeason, string pPc1, string pPc2, string pPc3, string pPicUrl, bool pDisplay)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateSKUS2 mSkus = new spUpdateSKUS2();

                mSkus.Connection = mConnection;
               
                mSkus.ISACTIVE = pIsActive;
                
                mSkus.COMPANY_ID = companyId;
                mSkus.PRINCIPAL_ID = pCompanyId;
                mSkus.DIVISION_ID = pDivisionId;
                mSkus.BRAND_ID = pBrandId;
                mSkus.CATEGORY_ID = pCategoryId;
                mSkus.SUBCATEGORY_ID = pSubCategoryId;
                mSkus.CATEGORY_TYPE = pCategoryType;
                mSkus.BAR_CODE = pBarCode;
                mSkus.COLOR = pColor;
                mSkus.SKU_TAG_ID = pSkuTagId;
                mSkus.SKU_SEASON = pSkuSeason;
                mSkus.SKU_COUNTRY =int.Parse(pSkuCountry);
                mSkus.PicUrl = pPicUrl;
                mSkus.Display = pDisplay;
                mSkus.UNITS_IN_CASE = pUnitsInCase;
                mSkus.UNITS_IN_CASE2 = pUnitsInCase2;
                mSkus.SKU_ID = pSkuId;
                mSkus.SKU_NAME = pSkuName;
                mSkus.SKU_CODE = pSkuCode;
                mSkus.TIME_STAMP = System.DateTime.Now;
                mSkus.LASTUPDATE_DATE = System.DateTime.Now;
                mSkus.IP_ADDRESS = pPerPc;
                mSkus.PACKSIZE = pPackSize;
                mSkus.USER_ID = pUserId;

                //mSkus.PC1 = pPc1;
                //mSkus.PC2 = pPc2;
                //mSkus.PC3 = pPc3;

                mSkus.ExecuteQuery();
                return "Record Updated";

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        #endregion
        public bool InsertBarcode(string p_Company_Name, byte[] p_image, DataTable dt)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertBARCODE mSkus = new spInsertBARCODE();

                mSkus.Connection = mConnection;
                foreach (DataRow dr in dt.Rows)
                {
                   int q = int.Parse(dr["Quantity"].ToString());

                    for(int i=0; i < q; i++)
                    {
                        mSkus.COMPANY_NAME = p_Company_Name;
                        mSkus.PRODUCT_id = int.Parse(dr["SKU_ID"].ToString());
                        mSkus.QUANTITY = int.Parse(dr["Quantity"].ToString());
                        mSkus.PRICE = decimal.Parse(dr["PRICE"].ToString());
                        mSkus.BARCODE_IMAGE = p_image;
                       // mSkus.BARCODE_IMAGE = dr["Code"].ToString();

                        mSkus.ExecuteQuery();
                    }
                }
                return true;

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                //return exp.Message;
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public bool InsertBarcodeSupplier(string p_Company_Name, byte[] p_image, DataTable dt)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertBARCODESupplier mSkus = new spInsertBARCODESupplier();

                mSkus.Connection = mConnection;
                foreach (DataRow dr in dt.Rows)
                {
                    int q = int.Parse(dr["Quantity"].ToString());

                    for (int i = 0; i < q; i++)
                    {
                        mSkus.COMPANY_NAME = p_Company_Name;//supplier name
                        mSkus.skuId= int.Parse(dr["Company_Id"].ToString());//suplier id
                        mSkus.quantity = int.Parse(dr["Quantity"].ToString());
                        mSkus.product_name = (dr["SIZE"].ToString());//size
                        mSkus.price = dr["Code"].ToString();
                        mSkus.BARCODE_IMAGE = p_image;
                     

                        mSkus.ExecuteQuery();
                    }
                }
                return true;

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                //return exp.Message;
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public bool InsertBarcodeSupplier2(string p_Company_Name, DataTable dt)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertBARCODESupplier mSkus = new spInsertBARCODESupplier();

                mSkus.Connection = mConnection;
                foreach (DataRow dr in dt.Rows)
                {
                    int q = int.Parse(dr["Quantity"].ToString());

                    for (int i = 0; i < q; i++)
                    {
                        mSkus.COMPANY_NAME = p_Company_Name;//supplier name
                        mSkus.skuId = int.Parse(dr["Company_Id"].ToString());//suplier id
                        mSkus.quantity = int.Parse(dr["Quantity"].ToString());
                        mSkus.product_name = (dr["SIZE"].ToString());//size
                        mSkus.price = dr["Code"].ToString();
                        mSkus.BARCODE_IMAGE =(byte [])(dr["BARCODE_IMAGE"]);


                        mSkus.ExecuteQuery();
                    }
                }
                return true;

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                //return exp.Message;
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        public int InsertBarcodeSupplierMaster(int company_id,string p_Company_Name , DataTable dt,int type_id)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
               // mTransaction = ProviderFactory.GetTransaction(mConnection);
                uspInsertSupplierBrcdMaster mSkus = new uspInsertSupplierBrcdMaster();
                mSkus.Connection = mConnection;
                mSkus.supplier_name = p_Company_Name;
                mSkus.SUPPLIER_ID = company_id;
                mSkus.TIME_STAMP = DateTime.Now;
                mSkus.USER_ID = 0;
                mSkus.MODIFY_DATE = DateTime.Now;
                mSkus.IS_DELETED = 1;
                mSkus.ExecuteQuery();
                int masterId = Convert.ToInt32(mSkus.supplier_bc_master_id);
                uspInsertSupplierBCDetail supBcDt = new uspInsertSupplierBCDetail();
                supBcDt.Connection = mConnection;

                foreach (DataRow dr in dt.Rows)
                {
                    for (int i = 0; i < int.Parse(dr["Quantity"].ToString()); i++)
                    {
                        supBcDt.SUPP_BARCD_MASTER_ID = masterId;
                        supBcDt.SUPPLIER_ID = company_id;//suplier id
                        supBcDt.SIZE = (dr["SIZE"].ToString());//size
                        supBcDt.CODE = dr["Code"].ToString();
                        supBcDt.TYPE_ID = type_id;
                        supBcDt.IS_DELETED = 1;
                        supBcDt.quantity = int.Parse(dr["Quantity"].ToString());
                        supBcDt.ExecuteQuery();
                    }
                }
                return masterId;

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                //return exp.Message;
                return 0;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
    

        public DataTable SelectSkuBarcode()
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectBARCODE mBarcode = new spSelectBARCODE();
                mBarcode.Connection = mConnection;
                DataTable dt = mBarcode.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        public DataTable SelectSkuBarcodeSupplier(int SupplierID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectBarcodeSupplier mBarcode = new spSelectBarcodeSupplier();
                mBarcode.Connection = mConnection;
                mBarcode.SUPLLIER_ID = SupplierID;
                DataTable dt = mBarcode.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        #endregion
    }
}

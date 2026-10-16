using System;
using System.Data;
using CORNCommon.Classes;
using CORNDataAccessLayer.Classes;
using CORNDatabaseLayer.Classes;
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;

namespace CORNBusinessLayer.Classes
{
    /// <summary>
    /// Class For SKU Price Related Tasks
    /// <example>
    /// <list type="bullet">
    /// <item>
    /// Insert SKU Prices
    /// </item>
    /// <item>
    /// Get SKU Prices
    /// </item>
    /// </list>
    /// </example>
    /// </summary>
   public class SKUPriceDetailController
   {
       #region Constructors

        /// <summary>
       /// Constructor For SKUPriceDetailController
        /// </summary>
       public SKUPriceDetailController()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
        #endregion

       #region Public Methods

       #region Select

       public DataTable SelectSKUDetail(int pskuId, int pdistributorId, int puserId, string pbatchNo, DateTime pstockDate)
       {
           IDbConnection mConnection = null;
           try
           {
               mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
               mConnection.Open();

               uspSelectSKUDetail ObjSelect = new uspSelectSKUDetail();
               ObjSelect.Connection = mConnection;
               ObjSelect.skuId = pskuId;
               ObjSelect.distributorId = pdistributorId;
               ObjSelect.userId = puserId;
               ObjSelect.batchNo = pbatchNo;
               ObjSelect.stockDate = pstockDate;

               DataTable dt = ObjSelect.ExecuteTable();
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
       public DataTable SelectSKUDetail(int pskuId, int pdistributorId, int puserId, string pbatchNo)
       {
           IDbConnection mConnection = null;
           try
           {
               mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
               mConnection.Open();

               uspSelectSKUDetail ObjSelect = new uspSelectSKUDetail();
               ObjSelect.Connection = mConnection;
               ObjSelect.skuId = pskuId;
               ObjSelect.distributorId = pdistributorId;
               ObjSelect.userId = puserId;
               ObjSelect.batchNo = pbatchNo;

               DataTable dt = ObjSelect.ExecuteTable();
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
       /// Gets SKU Price Data
       /// </summary>
       /// <remarks>
        /// Returns SKU Price Data as Datatable
       /// </remarks>
       /// <param name="p_Principal_Id">Principal</param>
       /// <param name="p_Division_ID">Division</param>
       /// <param name="p_Category_ID">Category</param>
       /// <param name="p_Brand_id">Brand</param>
       /// <param name="p_Var_Id">Variant</param>
       /// <param name="p_User_Id">InsertedBy</param>
       /// <param name="p_sku_id">SKU</param>
       /// <param name="p_Type_id">Type</param>
       /// <param name="p_ClosedDate">CloseDate</param>
        /// <returns>SKU Price Data as Datatable</returns>
       public DataTable SelectDataPrice(int p_Principal_Id, int p_Division_ID, int p_Category_ID, int p_Brand_id, int p_Var_Id, int p_User_Id,int p_sku_id,int p_Type_id,DateTime p_ClosedDate)
       {
           IDbConnection mConnection = null;
           try
           {
               mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
               mConnection.Open();

               UspSelectSKUPriceList  ObjSelect = new UspSelectSKUPriceList();
               ObjSelect.Connection = mConnection;
               ObjSelect.COMPANY_ID = p_Principal_Id;
               ObjSelect.DIVISION_ID = p_Division_ID;
               ObjSelect.CATEGORY_ID = p_Category_ID;
               ObjSelect.BRAND_ID = p_Brand_id;
               ObjSelect.VAR_ID = p_Var_Id;
               ObjSelect.SKU_ID = p_sku_id;
               ObjSelect.TYPEID = p_Type_id;
               ObjSelect.USER_ID = p_User_Id;
               ObjSelect.DAYCLOSED = p_ClosedDate; 
               DataTable dt = ObjSelect.ExecuteTable();
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
        public DataTable SelectDataPrice4(string p_CountryIds, int p_userId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spSelctSuplierCOuntRywise ObjSelect = new spSelctSuplierCOuntRywise();
                ObjSelect.Connection = mConnection;
                ObjSelect.COUNRTRY_IDS = p_CountryIds;
                ObjSelect.USER_ID = p_userId;
                DataTable dt = ObjSelect.ExecuteTable();
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
        public DataTable SelectDataPrice_stockregister(int p_Principal_Id, int p_Division_ID, int p_Category_ID, int p_Brand_id, int p_Var_Id, int p_User_Id, int p_sku_id, int p_Type_id, DateTime p_ClosedDate)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                UspSelectSKUPriceList_purchase ObjSelect = new UspSelectSKUPriceList_purchase();
                ObjSelect.Connection = mConnection;
                ObjSelect.COMPANY_ID = p_Principal_Id;
                ObjSelect.DIVISION_ID = p_Division_ID;
                ObjSelect.CATEGORY_ID = p_Category_ID;
                ObjSelect.BRAND_ID = p_Brand_id;
                ObjSelect.VAR_ID = p_Var_Id;
                ObjSelect.SKU_ID = p_sku_id;
                ObjSelect.TYPEID = p_Type_id;
                ObjSelect.USER_ID = p_User_Id;
                ObjSelect.DAYCLOSED = p_ClosedDate;
                DataTable dt = ObjSelect.ExecuteTable();
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
        public DataTable SelectSupplierDetailBarcode(int p_supplierId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                uspSelectSUpplierBarcodeDoc ObjSelect = new uspSelectSUpplierBarcodeDoc();
                ObjSelect.Connection = mConnection;
                ObjSelect.SUPPLIER_ID = p_supplierId;
               
                DataTable dt = ObjSelect.ExecuteTable();
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


        public DataTable SelectDataPrice(int p_Principal_Id, int p_Division_ID, int p_Category_ID, int p_Brand_id, int p_Var_Id, int p_User_Id, int p_sku_id, int p_Type_id, DateTime p_ClosedDate, int p_Gender_Id)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                UspSelectSKUPriceList ObjSelect = new UspSelectSKUPriceList();
                ObjSelect.Connection = mConnection;
                ObjSelect.COMPANY_ID = p_Principal_Id;
                ObjSelect.DIVISION_ID = p_Division_ID;
                ObjSelect.CATEGORY_ID = p_Category_ID;
                ObjSelect.BRAND_ID = p_Brand_id;
                ObjSelect.VAR_ID = p_Var_Id;
                ObjSelect.SKU_ID = p_sku_id;
                ObjSelect.TYPEID = p_Type_id;
                ObjSelect.USER_ID = p_User_Id;
                ObjSelect.GENDER_ID = p_Gender_Id;
                ObjSelect.DAYCLOSED = p_ClosedDate;
                DataTable dt = ObjSelect.ExecuteTable();
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

        public DataTable SelectDataPricePOS(int p_Principal_Id, int p_Division_ID, int p_Category_ID, int p_Brand_id, int p_Var_Id, int p_User_Id, int p_sku_id, int p_Type_id,string pSearch)
       {
           IDbConnection mConnection = null;
           try
           {
               mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
               mConnection.Open();

               UspSelectSKUPriceList ObjSelect = new UspSelectSKUPriceList();
               ObjSelect.Connection = mConnection;
               ObjSelect.COMPANY_ID = p_Principal_Id;
               ObjSelect.DIVISION_ID = p_Division_ID;
               ObjSelect.CATEGORY_ID = p_Category_ID;
               ObjSelect.BRAND_ID = p_Brand_id;
               ObjSelect.VAR_ID = p_Var_Id;
               ObjSelect.SKU_ID = p_sku_id;
               ObjSelect.TYPEID = p_Type_id;
               ObjSelect.USER_ID = p_User_Id;
               ObjSelect.DAYCLOSED = Constants.DateNullValue;
               ObjSelect.SEARCH_TEXT = pSearch;
               DataTable dt = ObjSelect.ExecuteTable();
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
       public DataTable SelectDataPrice2(int p_Principal_Id, int p_Division_ID, int p_Category_ID, int p_Brand_id, int p_Var_Id, int p_User_Id, int p_sku_id, int p_Type_id, DateTime p_ClosedDate)
       {
           IDbConnection mConnection = null;
           try
           {
               mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
               mConnection.Open();

               UspSelectSKUPriceList2 ObjSelect = new UspSelectSKUPriceList2();
               ObjSelect.Connection = mConnection;
               ObjSelect.COMPANY_ID = p_Principal_Id;
               ObjSelect.DIVISION_ID = p_Division_ID;
               ObjSelect.CATEGORY_ID = p_Category_ID;
               ObjSelect.BRAND_ID = p_Brand_id;
               ObjSelect.VAR_ID = p_Var_Id;
               ObjSelect.SKU_ID = p_sku_id;
               ObjSelect.TYPEID = p_Type_id;
               ObjSelect.USER_ID = p_User_Id;
               ObjSelect.DAYCLOSED = p_ClosedDate;
               DataTable dt = ObjSelect.ExecuteTable();
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

       public DataTable SelectAllSKUS(string p_SKU_CODE, int p_Type_id, int p_USER_ID)
       {
           IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                UspSelectAllSKUS ObjSelect = new UspSelectAllSKUS();
                ObjSelect.Connection = mConnection;
                ObjSelect.SKU_CODE = p_SKU_CODE;
                ObjSelect.TYPEID = p_Type_id;
                ObjSelect.USER_ID = p_USER_ID;
                DataTable dt = ObjSelect.ExecuteTable();
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
        public DataTable SelectSkusImages(int p_distributor_id,string p_Supplier_IDs,string p_Session_IDs,string p_Gendor_IDs,string p_Category_IDs, DateTime p_From_date,int p_Type_id, int p_USER_ID,int p_Min_value)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                UspSelectAllSKUS_Images ObjSelect = new UspSelectAllSKUS_Images();
                ObjSelect.Connection = mConnection;
                ObjSelect.DISTRIBUTOR_ID = p_distributor_id;
                ObjSelect.SUPPLIER_IDs = p_Supplier_IDs;
                ObjSelect.SESSION_IDs = p_Session_IDs;
                ObjSelect.CATEGORY_IDs = p_Category_IDs;
                ObjSelect.GENDER_IDs = p_Gendor_IDs;
                ObjSelect.FROM_DATE = p_From_date;
                ObjSelect.TYPEID = p_Type_id;
                ObjSelect.MIN_VALUE = p_Min_value;
                ObjSelect.USER_ID = p_USER_ID;
                DataTable dt = ObjSelect.ExecuteTable();
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
        /// Gets SKU Current Price Data
        /// </summary>
        /// <remarks>
        /// Returns SKU Current Price Data as Datatable
        /// </remarks>
        /// <param name="p_Uom_id">UOM</param>
        /// <param name="p_Company_id">Principal</param>
        /// <param name="p_Division_Id">Division</param>
        /// <param name="p_Brand_Id">Brand</param>
        /// <param name="p_Category_Id">Category</param>
        /// <param name="variant_Id">Variant</param>
        /// <param name="p_Iscurrent">IsCurrent</param>
        /// <param name="p_sku_id">SKU</param>
        /// <returns>SKU Current Price Data as Datatable</returns>
        public DataTable SelectSKuCurrentPrice(int p_Uom_id, int p_Company_id, int p_Division_Id, int p_Brand_Id, int p_Category_Id, int variant_Id, int p_Iscurrent, int p_sku_id)
       {
           IDbConnection mConnection = null;
           try
           {
               mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
               mConnection.Open();
               spSelectskuPriceInfo mSkuInfo = new spSelectskuPriceInfo();
               mSkuInfo.Connection = mConnection;

               mSkuInfo.UOM_ID = p_Uom_id;
               mSkuInfo.COMPANY_ID = p_Company_id;
               mSkuInfo.DIVISION_ID = p_Division_Id;
               mSkuInfo.BRAND_ID = p_Brand_Id;
               mSkuInfo.CATEGORY_ID = p_Category_Id;
               mSkuInfo.VARIANT_ID = variant_Id;
               mSkuInfo.ISACTIVE = true;
               mSkuInfo.ISCURRENT = p_Iscurrent;
               mSkuInfo.SKU_Id = p_sku_id;

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

        public DataTable GetSKUDetail(int p_DISTRIBUTOR_ID, int p_SKU_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                uspGetSKUDetail ObjSelect = new uspGetSKUDetail();
                ObjSelect.Connection = mConnection;
                ObjSelect.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                ObjSelect.SKU_ID = p_SKU_ID;
                DataTable dt = ObjSelect.ExecuteTable();
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

        public DataTable GetSKUDetail(int p_DISTRIBUTOR_ID, string p_BAR_CODE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                uspGetSKUDetail ObjSelect = new uspGetSKUDetail();
                ObjSelect.Connection = mConnection;
                ObjSelect.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                ObjSelect.BAR_CODE = p_BAR_CODE;
                DataTable dt = ObjSelect.ExecuteTable();
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

        #region Insert

        /// <summary>
        /// Insert SKU Price
        /// </summary>
        /// <remarks>
        /// Returns "Record Inserted" On Success And Null On Failure
        /// </remarks>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="p_Sku_Id">SKU</param>
        /// <param name="p_Uom_Id">UOM</param>
        /// <param name="p_Distributor_Discount">Discount</param>
        /// <param name="p_Tax_Price">Tax</param>
        /// <param name="p_Distributor_Price">DP</param>
        /// <param name="p_Trade_Price">TP</param>
        /// <param name="p_Retail_Price">RP</param>
        /// <param name="p_date_effected">DateFrom</param>
        /// <param name="p_SED_Tax">SED</param>
        /// <returns>"Record Inserted" On Success And Null On Failure</returns>
        public bool InsertSKU_PRICES(int p_Distributor_Id, int p_Sku_Id, int p_Uom_Id, decimal p_Distributor_Discount, decimal p_Tax_Price, decimal p_Distributor_Price, decimal p_Trade_Price, decimal p_Retail_Price, DateTime p_date_effected, decimal p_SED_Tax)
       {
           IDbConnection mConnection = null;
           try
           {
               mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
               mConnection.Open();
               UspProcessSKU_Prices mSkuPrices = new UspProcessSKU_Prices();
               mSkuPrices.Connection = mConnection;
               mSkuPrices.DISTRIBUTOR_ID = p_Distributor_Id;
               mSkuPrices.DISTRIBUTOR_PRICE = p_Distributor_Price;
               mSkuPrices.SKU_ID = p_Sku_Id;
               mSkuPrices.RETAIL_PRICE = p_Retail_Price;
               mSkuPrices.TAX_PRICE = p_Tax_Price;
               mSkuPrices.TRADE_PRICE =p_Trade_Price;
               mSkuPrices.DISTRIBUTOR_DISCOUNT = p_Distributor_Discount;
               mSkuPrices.TIME_STAMP = System.DateTime.Now;
               mSkuPrices.LASTUPDATE_DATE = System.DateTime.Now;
               mSkuPrices.DATE_EFFECTED = p_date_effected;
               mSkuPrices.SED_TAX = p_SED_Tax;
               mSkuPrices.ExecuteQuery();
                return true;

           }
           catch (Exception exp)
           {
               ExceptionPublisher.PublishException(exp);
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

        public bool InsertSKU_PRICES(int p_Distributor_Id, int p_Sku_Id, int p_Uom_Id, decimal p_Distributor_Discount, decimal p_Tax_Price, decimal p_Distributor_Price, decimal p_Trade_Price, decimal p_Retail_Price, DateTime p_date_effected, decimal p_SED_Tax,decimal p_Purchase_Price)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspProcessSKU_Prices mSkuPrices = new UspProcessSKU_Prices();
                mSkuPrices.Connection = mConnection;
                mSkuPrices.DISTRIBUTOR_ID = p_Distributor_Id;
                mSkuPrices.DISTRIBUTOR_PRICE = p_Distributor_Price;
                mSkuPrices.SKU_ID = p_Sku_Id;
                mSkuPrices.RETAIL_PRICE = p_Retail_Price;
                mSkuPrices.TAX_PRICE = p_Tax_Price;
                mSkuPrices.TRADE_PRICE = p_Trade_Price;
                mSkuPrices.DISTRIBUTOR_DISCOUNT = p_Distributor_Discount;
                mSkuPrices.TIME_STAMP = System.DateTime.Now;
                mSkuPrices.LASTUPDATE_DATE = System.DateTime.Now;
                mSkuPrices.DATE_EFFECTED = p_date_effected;
                mSkuPrices.SED_TAX = p_SED_Tax;
                mSkuPrices.PURCHASE_PRICE = p_Purchase_Price;
                mSkuPrices.ExecuteQuery();
                return true;

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
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

        /// <summary>
        /// Inserts Or Updates SKU Price From Excel File
        /// </summary>
        /// Returns True On Success And False On Failure
        /// <param name="p_DistributorId">Location</param>
        /// <param name="pFileName">ExcelFile</param>
        /// <param name="p_Principal_Id">Principal</param>
        /// <returns>True On Success And False On Failure</returns>
        public bool ImportSKUPrices(int p_DistributorId, string pFileName, int p_Principal_Id)
       {
           IDbConnection mConnection = null;
           FileStream Sourcefile = null;
           StreamReader ReadSourceFile = null;
           IDbTransaction mTransaction = null;

           try
           {
               mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
               mConnection.Open();
               mTransaction = ProviderFactory.GetTransaction(mConnection);

               Sourcefile = new FileStream(pFileName, FileMode.Open);
               ReadSourceFile = new StreamReader(Sourcefile);
               string FileContents = "";
               while ((FileContents = ReadSourceFile.ReadLine()) != null)
               {

                   string[] ParametersArr = FileContents.Split(Constants.File_Delimiter);
                   spSelectSKUS mSKUS = new spSelectSKUS();
                   mSKUS.Connection = mConnection;
                   mSKUS.Transaction = mTransaction;
                   mSKUS.SKU_CODE = ParametersArr[0].ToString();
                   mSKUS.ISACTIVE = true;
                   DataTable dt = mSKUS.ExecuteTable();

                   if (dt.Rows.Count > 0)
                   {
                       foreach (DataRow dr in dt.Rows)
                       {
                           
                           UspProcessSKU_Prices mSKUPrice = new UspProcessSKU_Prices();
                           mSKUPrice.Connection = mConnection;
                           mSKUPrice.Transaction = mTransaction;
                           mSKUPrice.SKU_ID = int.Parse(dr["SKU_Id"].ToString());
                           mSKUPrice.DISTRIBUTOR_ID = p_DistributorId;
                           mSKUPrice.PRINCIPAL_ID = int.Parse(dr["Principal_Id"].ToString());
                           mSKUPrice.DISTRIBUTOR_PRICE = decimal.Parse(ParametersArr[1]);
                           mSKUPrice.TRADE_PRICE = decimal.Parse(ParametersArr[2].ToString());
                           mSKUPrice.RETAIL_PRICE = decimal.Parse(ParametersArr[3].ToString());
                           mSKUPrice.DISTRIBUTOR_DISCOUNT = decimal.Parse(ParametersArr[4].ToString());
                           mSKUPrice.TAX_PRICE = decimal.Parse(ParametersArr[5].ToString());
                           mSKUPrice.SED_TAX = decimal.Parse(ParametersArr[6].ToString());
                           mSKUPrice.DATE_EFFECTED = DateTime.Parse(ParametersArr[7].ToString());
                           mSKUPrice.LASTUPDATE_DATE = DateTime.Now;
                           mSKUPrice.TIME_STAMP = DateTime.Now;
                           mSKUPrice.ExecuteQuery();
                       }
                   }
               }

               mTransaction.Commit();
               return true;
           }
           catch (Exception excp)
           {
               mTransaction.Rollback();
               ReadSourceFile.Close();
               mConnection.Close();
               return false;
               //  ExceptionPublisher.PublishException(excp);
               // throw;

           }
           finally
           {
               ReadSourceFile.Close();
               mConnection.Close();
           }
       }		

       #endregion

       #endregion
   }
}

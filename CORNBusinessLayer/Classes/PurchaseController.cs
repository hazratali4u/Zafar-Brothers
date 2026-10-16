using System;
using System.Data;
using CORNCommon.Classes;
using CORNDataAccessLayer.Classes;
using CORNDatabaseLayer.Classes;
using System.IO;

namespace CORNBusinessLayer.Classes
{
    /// <summary>
    /// Class For Purchase, TranferOut, Purchase Return, TranferIn And Damage Related Tasks
    /// <example>
    /// <list type="bullet">
    /// <item>
    /// Insert Purchase, TranferOut, Purchase Return, TranferIn And Damage
    /// </item>
    /// <term>
    /// Update Purchase, TranferOut, Purchase Return, TranferIn And Damage
    /// </term>
    /// <item>
    /// Get Purchase, TranferOut, Purchase Return, TranferIn And Damage
    /// </item>
    /// </list>
    /// </example>
    /// </summary>
    public class PurchaseController
    {
        #region Constructor

        /// <summary>
        /// Constructor for PurchaseController
        /// </summary>
        public PurchaseController()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#endregion

        #region Private Variables
        
        IDbTransaction mTransaction;
        IDbConnection mConnection;

        #endregion

        #region Public Methods

        #region Select

        /// <summary>
        /// Get Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail
        /// </summary>
        /// <remarks>
        /// Returns Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail as Datatable
        /// </remarks>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <param name="PConnection">Connection</param>
        /// <param name="PTransaction">Transaction</param>
        /// <returns>Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail as Datatable</returns>
        public DataTable SelectPrivousePurchaseDetail(int p_DISTRIBUTOR_ID, long p_PURCHASE_MASTER_ID, IDbConnection PConnection, IDbTransaction PTransaction)
        {
            try
            {
                spSelectPURCHASE_DETAIL mPurchaseDetail = new spSelectPURCHASE_DETAIL();
                mPurchaseDetail.Connection = PConnection;
                mPurchaseDetail.Transaction = PTransaction;
                mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseDetail.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                DataTable dt = mPurchaseDetail.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
        }

        /// <summary>
        /// Gets Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No
        /// </summary>
        /// <remarks>
        /// Returns Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No as Datatable
        /// </remarks>
        /// <param name="p_TYPE_ID">Type</param>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <param name="p_User_Id">InsertedBy</param>
        /// <param name="p_Posting">Posting</param>
        /// <returns>Purchase, TranferOut, Purchase Return, TranferIn And Damage  Document No as Datatable</returns>
        public DataTable SelectPurchaseDocumentNo(int p_TYPE_ID, int p_DISTRIBUTOR_ID, long p_PURCHASE_MASTER_ID, int p_User_Id, int p_Posting)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_MASTER mPurchaseMaster = new spSelectPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.USER_ID = p_User_Id;
                mPurchaseMaster.POSTING = p_Posting;
                DataTable dt = mPurchaseMaster.ExecuteTable();
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
        /// Gets Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No
        /// </summary>
        /// <remarks>
        /// Returns Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No as Datatable
        /// </remarks>
        /// <param name="p_TYPE_ID">Type</param>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="P_DocumentDate">Date</param>
        /// <returns>Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No as Datatable</returns>
        public DataTable SelectPurchaseDocumentNo(int p_TYPE_ID, int p_DISTRIBUTOR_ID, DateTime P_DocumentDate)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_MASTER mPurchaseMaster = new spSelectPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.DOCUMENT_DATE = P_DocumentDate;
                DataTable dt = mPurchaseMaster.ExecuteTable();
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
        /// Gets Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No
        /// </summary>
        /// <remarks>
        /// Returns Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No as Datatable
        /// </remarks>
        /// <param name="p_TYPE_ID">Type</param>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <param name="p_User_Id">InsertedBy</param>
        /// <param name="p_Posting">Posting</param>
        /// <param name="p_SOLD_TO">SoldTo</param>
        /// <returns>Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No as Datatable</returns>
        public DataTable SelectPurchaseDocumentNo(int p_TYPE_ID, int p_DISTRIBUTOR_ID, long p_PURCHASE_MASTER_ID, int p_User_Id, int p_Posting, int p_SOLD_TO)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_MASTER mPurchaseMaster = new spSelectPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.USER_ID = Constants.IntNullValue;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                DataTable dt = mPurchaseMaster.ExecuteTable();
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
        public DataTable SelectTransferDocumentNo(int p_TYPE_ID, int p_DISTRIBUTOR_ID, long p_PURCHASE_MASTER_ID, int p_User_Id, int p_Posting,  int p_SOLD_TO, int pIsClosed, DateTime pDocumentDate)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_MASTER mPurchaseMaster = new spSelectPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.USER_ID = Constants.IntNullValue;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.isClosed = pIsClosed;
                mPurchaseMaster.DOCUMENT_DATE = pDocumentDate;
                DataTable dt = mPurchaseMaster.ExecuteTable();
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
        /// Get Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail
        /// </summary>
        /// <remarks>
        /// Returns Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail as Datatable
        /// </remarks>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <returns>Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail as Datatable</returns>
        public DataTable SelectPurchaseDetail(int p_DISTRIBUTOR_ID, long p_PURCHASE_MASTER_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_DETAIL mPurchaseDetail = new spSelectPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseDetail.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                DataTable dt = mPurchaseDetail.ExecuteTable();
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

        public DataTable GetPurchaseOrder(long pPurchaseMasterId, int pDistributorId, int pUserId, DateTime pDocumentDate)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                GetPurchaseOrder mReserve = new GetPurchaseOrder();
                mReserve.Connection = mConnection;
                mReserve.DISTRIBUTOR_ID = pDistributorId;
               
                mReserve.PURCHASE_MASTER_ID = pPurchaseMasterId;
                mReserve.USER_ID = pUserId;
                mReserve.DOCUMENT_DATE = pDocumentDate;
                DataTable dt = mReserve.ExecuteTable();
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
        public DataSet GetGrn(long pPurchaseOrderId, int pDistributorId, int pTypeId, DateTime pDocumentDate)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                var mPurchaseMaster = new spSelectPURCHASE_ORDER
                {
                    Connection = mConnection,
                    PURCHASE_ORDER_ID = pPurchaseOrderId
                    ,
                    TYPE_ID = pTypeId
                    ,
                    document_date = pDocumentDate
                };

                return mPurchaseMaster.ExecuteDataSet();

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
        public DataSet SelectPurchaseOrderNo(long pPurchaseOrderId,int pTypeId, DateTime pDocumentDate)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                var mPurchaseMaster = new spSelectPURCHASE_ORDER
                {
                    Connection = mConnection,
                    PURCHASE_ORDER_ID = pPurchaseOrderId
                    ,TYPE_ID = pTypeId
                    ,document_date = pDocumentDate
                    
                };

                return mPurchaseMaster.ExecuteDataSet();
                
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

        #region Insert, Update

        /// <summary>
        /// Inserts Purchase, TranferOut, Purchase Return, TranferIn And Damage Document
        /// </summary>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_ORDER_NUMBER">DocumentNo</param>
        /// <param name="p_TYPE_ID">Type</param>
        /// <param name="p_DOCUMENT_DATE">Date</param>
        /// <param name="p_SOLD_TO">SoldTo</param>
        /// <param name="p_SOLD_FROM">SoldFrom</param>
        /// <param name="p_TOTAL_AMOUNT">Amount</param>
        /// <param name="p_IS_DELETE">IsDeleted</param>
        /// <param name="dtPurchaseDetail">PurchaseDetailDatatable</param>
        /// <param name="p_Posting">Posting</param>
        /// <param name="p_BuiltyNo">Builty</param>
        /// <param name="p_UserId">InsertedBy</param>
        /// <param name="p_PrincipalId">Principal</param>
        /// <returns>True On Success And False On Failure</returns>
        public bool InsertPurchaseDocument(int p_DISTRIBUTOR_ID,string p_ORDER_NUMBER,int p_TYPE_ID,DateTime p_DOCUMENT_DATE,int p_SOLD_TO,int p_SOLD_FROM,decimal p_TOTAL_AMOUNT,bool p_IS_DELETE,DataTable dtPurchaseDetail,int p_Posting,string p_BuiltyNo,int p_UserId,int p_PrincipalId) 
		{
			try
			{                 
				mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
				mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertPURCHASE_MASTER mPurchaseMaster = new spInsertPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
               
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;  
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.TIME_STAMP = DateTime.Now;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;   
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.PRINCIPAL_ID = p_PrincipalId; 
                mPurchaseMaster.ExecuteQuery();

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;
                decimal LedgerAmount = 0;
                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.RACK_NO = dr["RACK_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = decimal.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = 0;// dr["FREE_SKU"].ToString();
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.PURCHASE_PRICE = Convert.ToDecimal(dr["PURCHASE_PRICE"]);
                    mPurchaseDetail.ExecuteQuery();
                    LedgerAmount += mPurchaseDetail.QUANTITY * mPurchaseDetail.PURCHASE_PRICE;
                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_PrincipalId;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.RACK_NO;
                    mStockUpdate.ExecuteQuery();   
                }

                DataTable dtVoucher = new DataTable();
                dtVoucher.Columns.Add("LEDGER_ID", typeof(long));
                dtVoucher.Columns.Add("ACCOUNT_HEAD_ID", typeof(long));
                dtVoucher.Columns.Add("DEBIT", typeof(decimal));
                dtVoucher.Columns.Add("CREDIT", typeof(decimal));
                dtVoucher.Columns.Add("REMARKS", typeof(string));
                dtVoucher.Columns.Add("Principal_Id", typeof(string));
                //   dtVoucher.Columns.Add("VoucherNo",typeof(string));
                DataRow dr3 = dtVoucher.NewRow();
                dr3["LEDGER_ID"] = Constants.LongNullValue.ToString();
                dr3["ACCOUNT_HEAD_ID"] = 88;
                // dr["ACCOUNT_HEAD_ID"] = 352;//B3002120004           Cash in hand - accounts
                dr3["REMARKS"] = "Default Purchase Return Voucher";
                dr3["DEBIT"] = LedgerAmount;
                dr3["CREDIT"] = 0;
                dr3["Principal_Id"] = p_PrincipalId;
                dtVoucher.Rows.Add(dr3);
                ///Credit Side Entry
                DataRow dr1 = dtVoucher.NewRow();
                dr1["LEDGER_ID"] = Constants.LongNullValue.ToString();
                dr1["ACCOUNT_HEAD_ID"] = 141;
                dr1["DEBIT"] = 0;
                dr1["CREDIT"] = LedgerAmount;
                dr1["Principal_Id"] = p_PrincipalId;
                dr1["REMARKS"] = "Default Purchase Return Voucher";
                dtVoucher.Rows.Add(dr1);
                LedgerController LController = new LedgerController();
                string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, mPurchaseMaster.DISTRIBUTOR_ID, mPurchaseMaster.DOCUMENT_DATE);
                LController.Add_Voucher(Convert.ToInt32(mPurchaseMaster.DISTRIBUTOR_ID), p_PrincipalId, MaxDocumentId, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, 19, p_PrincipalId.ToString(), "Default Purchase Return  Voucher | " , mPurchaseMaster.DOCUMENT_DATE, null, dtVoucher, p_UserId, null, Constants.DateNullValue);

                mTransaction.Commit();
                return true; 
			}
			catch(Exception exp)
			{
				ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();  
				return false;
			}
			finally
			{
				if(mConnection != null && mConnection.State == ConnectionState.Open)
				{
					mConnection.Close();
				}
			}
			
		}
        public bool InsertTransferIn(long p_PURCHASE_MASTER_ID, int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID,
            DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE,
            DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_PrincipalId,int pIsClosed) 
		{
			try
			{                 
				mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
				mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertPURCHASE_MASTER mPurchaseMaster = new spInsertPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;  
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.TIME_STAMP = DateTime.Now;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;   
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.PRINCIPAL_ID = p_PrincipalId; 
                mPurchaseMaster.ExecuteQuery();

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;
                
                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.RACK_NO = dr["RACK_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = decimal.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = 0;// dr["FREE_SKU"].ToString();
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.PURCHASE_PRICE = Convert.ToDecimal(dr["PURCHASE_PRICE"]);
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_PrincipalId;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.RACK_NO;
                    mStockUpdate.ExecuteQuery();   
                }

               // bool flag = UpdateTransferOut(p_PURCHASE_MASTER_ID, p_DISTRIBUTOR_ID, pIsClosed);

               
                spUpdatePURCHASE_MASTER mPurchaseMaster2 = new spUpdatePURCHASE_MASTER();
                mPurchaseMaster2.Connection = mConnection;
                mPurchaseMaster2.Transaction = mTransaction;
                mPurchaseMaster2.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster2.DISTRIBUTOR_ID = p_SOLD_FROM;
                mPurchaseMaster2.IsClosed = pIsClosed;
               // mPurchaseMaster2.TYPE_ID = 5;
                mPurchaseMaster2.ExecuteQuery();

                mTransaction.Commit();
                return true; 
			}
			catch(Exception exp)
			{
				ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();  
				return false;
			}
			finally
			{
				if(mConnection != null && mConnection.State == ConnectionState.Open)
				{
					mConnection.Close();
				}
			}
			
		}
       
       
        public long InsertPurchaseDocument2(int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_PrincipalId)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertPURCHASE_MASTER mPurchaseMaster = new spInsertPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.TIME_STAMP = DateTime.Now;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.PRINCIPAL_ID = p_PrincipalId;
                mPurchaseMaster.ExecuteQuery();

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.RACK_NO = dr["RACK_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = decimal.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = 0;// dr["FREE_SKU"].ToString();
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.PURCHASE_PRICE = Convert.ToDecimal(dr["PURCHASE_PRICE"]);
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_PrincipalId;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.RACK_NO;
                    mStockUpdate.ExecuteQuery();
                }
                mTransaction.Commit();
                return mPurchaseMaster.PURCHASE_MASTER_ID;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return Constants.LongNullValue;
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
        /// Updates Purchase, TranferOut, Purchase Return, TranferIn And Damage Document
        /// </summary>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_ORDER_NUMBER">DocumentNo</param>
        /// <param name="p_TYPE_ID">Type</param>
        /// <param name="p_DOCUMENT_DATE">Date</param>
        /// <param name="p_SOLD_TO">SoldTo</param>
        /// <param name="p_SOLD_FROM">SoldFrom</param>
        /// <param name="p_TOTAL_AMOUNT">Amount</param>
        /// <param name="p_IS_DELETE">IsDeleted</param>
        /// <param name="dtPurchaseDetail">PurchaseDetailDatatable</param>
        /// <param name="p_Posting">Posting</param>
        /// <param name="p_BuiltyNo">Builty</param>
        /// <param name="p_UserId">InsertedBy</param>
        /// <param name="p_Principal">Principal</param>
        /// <returns>True On Success And False On Failure</returns>
        public bool UpdatePurchaseDocument(long p_PURCHASE_MASTER_ID, int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId,int p_Principal)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spUpdatePURCHASE_MASTER mPurchaseMaster = new spUpdatePURCHASE_MASTER(); 
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;  
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;   
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;  
                mPurchaseMaster.ExecuteQuery();
               
                //Get Privouse Update Purchase Detail and Rollback
                //LedgerController LController = new LedgerController();

                //string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID);

                DataTable dt = SelectPrivousePurchaseDetail(p_DISTRIBUTOR_ID, p_PURCHASE_MASTER_ID,mConnection,mTransaction);
                
                foreach (DataRow dr in dt.Rows)
                {
                    UspUpdatePurchaseDetailStock mPurchaseStock = new UspUpdatePurchaseDetailStock();
                    mPurchaseStock.Connection = mConnection;
                    mPurchaseStock.Transaction = mTransaction;
                    mPurchaseStock.TYPEID = p_TYPE_ID;
                    mPurchaseStock.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseStock.PURCHASE_DETAIL_ID = long.Parse(dr["PURCHASE_DETAIL_ID"].ToString());
                    mPurchaseStock.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID; 
                   // mPurchaseStock.R = dr["BATCH_NO"].ToString().Trim();
                    mPurchaseStock.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseStock.ExecuteQuery();
                }
                                
                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    
                   //update stock;
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.RACK_NO = dr["RACK_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = decimal.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.PURCHASE_PRICE = Convert.ToDecimal(dr["PURCHASE_PRICE"]);
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_Principal;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.RACK_NO;
                    mStockUpdate.ExecuteQuery();
                }
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
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
       
        //On Transfer In Close Transfer Out Document
        public bool UpdateTransferOut(long p_PURCHASE_MASTER_ID, int p_DISTRIBUTOR_ID, int pIsClosed)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spUpdatePURCHASE_MASTER mPurchaseMaster = new spUpdatePURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.IsClosed = pIsClosed;
                mPurchaseMaster.ExecuteQuery();

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
        /// Posts Pending Purchase, TranferOut, Purchase Return, TranferIn And Damage Document
        /// </summary>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <param name="p_Type_Id">Type</param>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="p_Posting">Posting</param>
        /// <returns>True On Success And False On Failure</returns>
        public bool PostPendingDocument(long p_PURCHASE_MASTER_ID,int p_Type_Id,int p_Distributor_Id,int p_Posting)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdatePURCHASE_MASTER mPurchaseMaster = new spUpdatePURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_Distributor_Id;
                mPurchaseMaster.TYPE_ID = p_Type_Id;  
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.ExecuteQuery();
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
        public bool ImportOpeningStock(int p_DISTRIBUTOR_ID, string pFileName, int p_PRINCIPAL_ID, DateTime p_DOCUMENT_DATE, int p_USER_ID)
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

                spInsertPURCHASE_MASTER mPurchaseMaster = new spInsertPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = 7;//Opening Stock
                mPurchaseMaster.ORDER_NUMBER = "";
                mPurchaseMaster.SOLD_FROM = p_PRINCIPAL_ID;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TOTAL_AMOUNT = 0;
                mPurchaseMaster.USER_ID = p_USER_ID;
                mPurchaseMaster.TIME_STAMP = DateTime.Now;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = 0;
                mPurchaseMaster.BUILTY_NO = "";
                mPurchaseMaster.PRINCIPAL_ID = p_PRINCIPAL_ID;
                mPurchaseMaster.ExecuteQuery();


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
                        spInsertPURCHASE_DETAIL mStockDetail = new spInsertPURCHASE_DETAIL();
                        mStockDetail.Connection = mConnection;
                        mStockDetail.Transaction = mTransaction;
                        mStockDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                        mStockDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                        mStockDetail.SKU_ID = int.Parse(dt.Rows[0]["SKU_ID"].ToString());
                        mStockDetail.RACK_NO = "";
                        mStockDetail.PRICE = decimal.Parse(ParametersArr[2].ToString());
                        mStockDetail.QUANTITY = int.Parse(ParametersArr[1].ToString());
                        mStockDetail.FREE_SKU = 0;
                        mStockDetail.AMOUNT = decimal.Parse(ParametersArr[3].ToString());
                        mStockDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                        mStockDetail.TIME_STAMP = p_DOCUMENT_DATE;
                        mStockDetail.ExecuteQuery();
                        
                        UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                        mStockUpdate.Connection = mConnection;
                        mStockUpdate.Transaction = mTransaction;
                        mStockUpdate.PRINCIPAL_ID = p_PRINCIPAL_ID;
                        mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                        mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                        mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                        mStockUpdate.SKU_ID = mStockDetail.SKU_ID;
                        mStockUpdate.STOCK_QTY = mStockDetail.QUANTITY;
                        mStockUpdate.FREE_QTY = mStockDetail.FREE_SKU;
                        mStockUpdate.BATCHNO = mStockDetail.RACK_NO;
                        mStockUpdate.ExecuteQuery();   
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
            //    ExceptionPublisher.PublishException(excp);
            //    throw;
                return false;
            }
            finally
            {
                ReadSourceFile.Close();
                mConnection.Close();
            }
        }

        public bool ImportPurchaseStock(int p_DISTRIBUTOR_ID, string pFileName, int p_PRINCIPAL_ID, DateTime p_DOCUMENT_DATE, int p_USER_ID)
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

                spInsertPURCHASE_MASTER mPurchaseMaster = new spInsertPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = 2;//Purchase Stock
                mPurchaseMaster.ORDER_NUMBER = "";
                mPurchaseMaster.SOLD_FROM = p_PRINCIPAL_ID;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TOTAL_AMOUNT = 0;
                mPurchaseMaster.USER_ID = p_USER_ID;
                mPurchaseMaster.TIME_STAMP = DateTime.Now;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = 0;
                mPurchaseMaster.BUILTY_NO = "";
                mPurchaseMaster.PRINCIPAL_ID = p_PRINCIPAL_ID;
                mPurchaseMaster.ExecuteQuery();


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
                        spInsertPURCHASE_DETAIL mStockDetail = new spInsertPURCHASE_DETAIL();
                        mStockDetail.Connection = mConnection;
                        mStockDetail.Transaction = mTransaction;
                        mStockDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                        mStockDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                        mStockDetail.SKU_ID = int.Parse(dt.Rows[0]["SKU_ID"].ToString());
                        mStockDetail.RACK_NO = "";
                        mStockDetail.PRICE = decimal.Parse(ParametersArr[2].ToString());
                        mStockDetail.QUANTITY = int.Parse(ParametersArr[1].ToString());
                        mStockDetail.FREE_SKU = 0;
                        mStockDetail.AMOUNT = decimal.Parse(ParametersArr[3].ToString());
                        mStockDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                        mStockDetail.TIME_STAMP = p_DOCUMENT_DATE;
                        mStockDetail.ExecuteQuery();

                        UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                        mStockUpdate.Connection = mConnection;
                        mStockUpdate.Transaction = mTransaction;
                        mStockUpdate.PRINCIPAL_ID = p_PRINCIPAL_ID;
                        mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                        mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                        mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                        mStockUpdate.SKU_ID = mStockDetail.SKU_ID;
                        mStockUpdate.STOCK_QTY = mStockDetail.QUANTITY;
                        mStockUpdate.FREE_QTY = mStockDetail.FREE_SKU;
                        mStockUpdate.BATCHNO = mStockDetail.RACK_NO;
                        mStockUpdate.ExecuteQuery();
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
               // ExceptionPublisher.PublishException(excp);
               // throw;
                return false;
            }
            finally
            {
                ReadSourceFile.Close();
                mConnection.Close();
            }
        }

        #region PurchaseOrder

        public long InsertPurchaseOrder(int pDistributorId, int pPrincipalId, string pContainerNo, string pRemarks, DateTime pArrivalDate, DateTime pDocumentDate, int pUserId,
           DataTable dtPurchaseDetail, DateTime pTimeStamp,DateTime pInvoiceDate,string p_BAG_NO)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertPURCHASE_ORDER mPurchaseMaster = new spInsertPURCHASE_ORDER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.DISTRIBUTOR_ID = pDistributorId;
                mPurchaseMaster.PRINCIPAL_ID = pPrincipalId;
                mPurchaseMaster.CONTAINER_NO = pContainerNo;
                mPurchaseMaster.REMARKS = pRemarks;
                mPurchaseMaster.ARRIVAL_DATE = pArrivalDate;
                mPurchaseMaster.DOCUMENT_DATE = pDocumentDate;
                mPurchaseMaster.USER_ID = pUserId;
                mPurchaseMaster.TIME_STAMP = pTimeStamp;  // as document date
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.INVOICE_DATE = pInvoiceDate;
                mPurchaseMaster.BAG_NO = p_BAG_NO;
                mPurchaseMaster.ExecuteQuery();

                spInsertPURCHASE_ORDER_DETAIL mPurchaseDetail = new spInsertPURCHASE_ORDER_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    if (decimal.Parse(dr["QUANTITY"].ToString())>0)
                    {
                        mPurchaseDetail.PURCHASE_ORDER_ID = mPurchaseMaster.PURCHASE_ORDER_ID;
                        mPurchaseDetail.DISTRIBUTOR_ID = pDistributorId;
                        mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                        mPurchaseDetail.QUANTITY = decimal.Parse(dr["QUANTITY"].ToString());
                        // mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                        //  mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                        mPurchaseDetail.TIME_STAMP = DateTime.Now;
                        mPurchaseDetail.ExecuteQuery();
                    }
                }
                mTransaction.Commit();
                return mPurchaseMaster.PURCHASE_ORDER_ID;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return -2;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public long UpdatePurchaseOrderDetail(long pPurchaseOrderId, int pDistributorId, string pContainerNo, string pRemarks, DateTime pArrivalDate, int pUserId, DataTable dtPurchaseDetail,DateTime pInvoiceDate,string p_BAG_NO)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spDeletePURCHASE_ORDER_DETAIL mDelete = new spDeletePURCHASE_ORDER_DETAIL();
                mDelete.Connection = mConnection;
                mDelete.Transaction = mTransaction;
                mDelete.PURCHASE_ORDER_ID = pPurchaseOrderId;
                mDelete.DISTRIBUTOR_ID = pDistributorId;                
                mDelete.ExecuteQuery();


                spUpdatePURCHASE_ORDER mPurchaseMaster = new spUpdatePURCHASE_ORDER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.PURCHASE_ORDER_ID = pPurchaseOrderId;
                mPurchaseMaster.DISTRIBUTOR_ID = pDistributorId;
                mPurchaseMaster.REMARKS = pRemarks;
                mPurchaseMaster.CONTAINER_NO = pContainerNo;
                mPurchaseMaster.ARRIVAL_DATE = pArrivalDate;
                mPurchaseMaster.USER_ID = pUserId;
                mPurchaseMaster.INVOICE_DATE = pInvoiceDate;
                mPurchaseMaster.BAG_NO = p_BAG_NO;
                mPurchaseMaster.ExecuteQuery();

                spInsertPURCHASE_ORDER_DETAIL mPurchaseDetail = new spInsertPURCHASE_ORDER_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    if (decimal.Parse(dr["QUANTITY"].ToString()) > 0)
                    {
                        mPurchaseDetail.PURCHASE_ORDER_ID = mPurchaseMaster.PURCHASE_ORDER_ID;
                        mPurchaseDetail.DISTRIBUTOR_ID = pDistributorId;
                        mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                        mPurchaseDetail.QUANTITY = decimal.Parse(dr["QUANTITY"].ToString());
                        //  mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                        // mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                        mPurchaseDetail.TIME_STAMP = DateTime.Now;
                        mPurchaseDetail.ExecuteQuery();
                    }
                }
                mTransaction.Commit();
                return mPurchaseMaster.PURCHASE_ORDER_ID;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return -2;
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

        #region GRN
        public long UpdatePurchaseDocument2(long pPurchaseMasterId, int pDistributorId, int pPrincipalId, Decimal pTotalAmount, int pTypeId, DateTime pDocumentDate, DataTable dtPurchaseDetail, int pPosting, int pUserId,string pREMARKS)
        {
            try
            {
               

                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                spUpdatePURCHASE_MASTER mPurchaseMaster = new spUpdatePURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.PURCHASE_MASTER_ID = pPurchaseMasterId;
                mPurchaseMaster.DISTRIBUTOR_ID = pDistributorId;
                mPurchaseMaster.TYPE_ID = pTypeId; 
                mPurchaseMaster.ORDER_NUMBER = "0";
                mPurchaseMaster.DOCUMENT_DATE = pDocumentDate;
                mPurchaseMaster.SOLD_FROM = pPrincipalId;
                mPurchaseMaster.TOTAL_AMOUNT = pTotalAmount;
                mPurchaseMaster.SOLD_TO = 0;
                mPurchaseMaster.USER_ID = pUserId;
                mPurchaseMaster.TIME_STAMP = DateTime.Now;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = pPosting;
                mPurchaseMaster.BUILTY_NO = "N/A";
                mPurchaseMaster.POSTING = pPosting;
                mPurchaseMaster.REMARKS = pREMARKS;
                mPurchaseMaster.ExecuteQuery();
                DataTable dt = SelectPrivousePurchaseDetail(pDistributorId, pPurchaseMasterId, mConnection, mTransaction);

                foreach (DataRow dr in dt.Rows)
                {
                    UspUpdatePurchaseDetailStock mPurchaseStock = new UspUpdatePurchaseDetailStock();
                    mPurchaseStock.Connection = mConnection;
                    mPurchaseStock.Transaction = mTransaction;
                    mPurchaseStock.TYPEID = pTypeId;
                    mPurchaseStock.DISTRIBUTOR_ID = pDistributorId;
                    mPurchaseStock.PURCHASE_DETAIL_ID = long.Parse(dr["PURCHASE_DETAIL_ID"].ToString());
                    mPurchaseStock.PURCHASE_MASTER_ID = pPurchaseMasterId;
                    mPurchaseStock.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseStock.document_date = pDocumentDate;
                    mPurchaseStock.ExecuteQuery();
                }

                #region insert Purchase Detail
                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = pDistributorId;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.RACK_NO = dr["RACK_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.TRADE_PRICE = decimal.Parse(dr["TRADE_PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = decimal.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.ORDER_QUANTITY = decimal.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = 0;
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;                  
                    mPurchaseDetail.DATE_EFFECTED = Convert.ToDateTime(dr["DATE_EFFECTED"]);
                    mPurchaseDetail.TIME_STAMP = pDocumentDate;
                    mPurchaseDetail.PURCHASE_PRICE = Convert.ToDecimal(dr["PURCHASE_PRICE"]);
                    mPurchaseDetail.ExecuteQuery();



                #endregion

                    UspProcessSKU_Prices mSkuPrices = new UspProcessSKU_Prices();

                    mSkuPrices.Connection = mConnection;
                    mSkuPrices.Transaction = mTransaction;
                    mSkuPrices.DISTRIBUTOR_ID = pDistributorId;
                    mSkuPrices.DISTRIBUTOR_PRICE = mPurchaseDetail.PRICE;
                    mSkuPrices.SKU_ID = mPurchaseDetail.SKU_ID;
                    mSkuPrices.RETAIL_PRICE = 0;
                    mSkuPrices.PURCHASE_PRICE = mPurchaseDetail.PURCHASE_PRICE;
                    mSkuPrices.TRADE_PRICE = mPurchaseDetail.TRADE_PRICE;
                    mSkuPrices.DISTRIBUTOR_DISCOUNT = 0;
                    mSkuPrices.TIME_STAMP = System.DateTime.Now;
                    mSkuPrices.LASTUPDATE_DATE = System.DateTime.Now;
                    mSkuPrices.DATE_EFFECTED = mPurchaseDetail.DATE_EFFECTED;
                    
                    mSkuPrices.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = pPrincipalId;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = 0;
                    mStockUpdate.BATCHNO =  mPurchaseDetail.RACK_NO;
                    mStockUpdate.ExecuteQuery();


                }

               
                mTransaction.Commit();
                return mPurchaseMaster.PURCHASE_MASTER_ID;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
               
                return -2;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public long InsertPurchaseDocument2(long pPoId,int pDistributorId, int pPrincipalId, Decimal pTotalAmount, int pTypeId, DateTime pDocumentDate, bool pIsDelete, DataTable dtPurchaseDetail, int pPosting, int pUserId,string pREMARKS)
        {
            try
            {
                

                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertPURCHASE_MASTER mPurchaseMaster = new spInsertPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.DISTRIBUTOR_ID = pDistributorId;
                mPurchaseMaster.TYPE_ID = pTypeId;
                mPurchaseMaster.ORDER_NUMBER = "0";
                mPurchaseMaster.DOCUMENT_DATE = pDocumentDate;
                mPurchaseMaster.SOLD_FROM = pPrincipalId;
                mPurchaseMaster.TOTAL_AMOUNT = pTotalAmount;
                mPurchaseMaster.SOLD_TO = 0;
                mPurchaseMaster.USER_ID = pUserId;
                mPurchaseMaster.TIME_STAMP = DateTime.Now;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = pPosting;
                mPurchaseMaster.PRINCIPAL_ID = pPrincipalId;
                mPurchaseMaster.BUILTY_NO = "N/A";
                mPurchaseMaster.PO_ID = pPoId;
                mPurchaseMaster.REMARKS = pREMARKS;                     
                mPurchaseMaster.ExecuteQuery();
                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;
                decimal totalamoutnWithPurchasePrice = 0;
                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = pDistributorId;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.RACK_NO = dr["RACK_NO"].ToString();
                    mPurchaseDetail.TRADE_PRICE = decimal.Parse(dr["TRADE_PRICE"].ToString());
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = decimal.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.ORDER_QUANTITY = decimal.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = 0;
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;                    
                    mPurchaseDetail.DATE_EFFECTED = Convert.ToDateTime(dr["DATE_EFFECTED"]);
                    mPurchaseDetail.TIME_STAMP = pDocumentDate;
                    mPurchaseDetail.PURCHASE_PRICE = Convert.ToDecimal(dr["PURCHASE_PRICE"]);
                    totalamoutnWithPurchasePrice = totalamoutnWithPurchasePrice + (decimal.Parse(dr["QUANTITY"].ToString()) * Convert.ToDecimal(dr["PURCHASE_PRICE"]));
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessSKU_Prices mSkuPrices = new UspProcessSKU_Prices();

                    mSkuPrices.Connection = mConnection;
                    mSkuPrices.Transaction = mTransaction;
                    mSkuPrices.DISTRIBUTOR_ID = pDistributorId;
                    mSkuPrices.DISTRIBUTOR_PRICE = mPurchaseDetail.PRICE;
                    mSkuPrices.SKU_ID = mPurchaseDetail.SKU_ID;
                    mSkuPrices.RETAIL_PRICE = 0;
                    mSkuPrices.PURCHASE_PRICE = mPurchaseDetail.PURCHASE_PRICE;
                    mSkuPrices.TRADE_PRICE = mPurchaseDetail.TRADE_PRICE;
                    mSkuPrices.DISTRIBUTOR_DISCOUNT = 0;
                    mSkuPrices.TIME_STAMP = System.DateTime.Now;
                    mSkuPrices.LASTUPDATE_DATE = System.DateTime.Now;
                    mSkuPrices.DATE_EFFECTED = mPurchaseDetail.DATE_EFFECTED;
                    mSkuPrices.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = pPrincipalId;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = 0;
                    mStockUpdate.BATCHNO = mPurchaseDetail.RACK_NO;
                    mStockUpdate.ExecuteQuery();

                }
                DataTable dtVoucher = new DataTable();
                dtVoucher.Columns.Add("LEDGER_ID", typeof(long));
                dtVoucher.Columns.Add("ACCOUNT_HEAD_ID", typeof(long));
                dtVoucher.Columns.Add("DEBIT", typeof(decimal));
                dtVoucher.Columns.Add("CREDIT", typeof(decimal));
                dtVoucher.Columns.Add("REMARKS", typeof(string));
                dtVoucher.Columns.Add("Principal_Id", typeof(string));
                //   dtVoucher.Columns.Add("VoucherNo",typeof(string));
                DataRow dr3 = dtVoucher.NewRow();
                dr3["LEDGER_ID"] = Constants.LongNullValue.ToString();
                dr3["ACCOUNT_HEAD_ID"] = 141;
                // dr["ACCOUNT_HEAD_ID"] = 352;//B3002120004           Cash in hand - accounts
                dr3["REMARKS"] = "Default Purchase Voucher" ;
                dr3["DEBIT"] =totalamoutnWithPurchasePrice;
                dr3["CREDIT"] = 0;
                dr3["Principal_Id"] = pPrincipalId;
                dtVoucher.Rows.Add(dr3);
                ///Credit Side Entry
                DataRow dr1 = dtVoucher.NewRow();
                dr1["LEDGER_ID"] = Constants.LongNullValue.ToString();
                dr1["ACCOUNT_HEAD_ID"] = 88;
                dr1["DEBIT"] = 0;
                dr1["CREDIT"] = totalamoutnWithPurchasePrice;
                dr1["Principal_Id"] = pPrincipalId;
                dr1["REMARKS"] ="Default Purchase Voucher";
                dtVoucher.Rows.Add(dr1);
                LedgerController LController = new LedgerController();
                string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, mPurchaseMaster.DISTRIBUTOR_ID, mPurchaseMaster.DOCUMENT_DATE);
                LController.Add_Voucher(Convert.ToInt32(mPurchaseMaster.DISTRIBUTOR_ID), pPrincipalId, MaxDocumentId, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE,19 , pPrincipalId.ToString(), "Default Purchase  Voucher", Constants.DateNullValue, null, dtVoucher, pUserId, null, Constants.DateNullValue);

                mTransaction.Commit();
                return mPurchaseMaster.PURCHASE_MASTER_ID;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                
                return -2;
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
        #endregion
        public DataTable SelectSKUClosingStock(int p_DISTRIBUTOR_ID, int p_SKU_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                mStockUpdate.Connection = mConnection;
                mStockUpdate.TYPE_ID = 15;
                mStockUpdate.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mStockUpdate.SKU_ID = p_SKU_ID;
                
                DataTable dt = mStockUpdate.ExecuteTable();
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
        
        public DataTable SelectSKUClosingStock(int p_DISTRIBUTOR_ID, int p_SKU_ID, DateTime pClosingdate)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                mStockUpdate.Connection = mConnection;
                mStockUpdate.TYPE_ID = 12;
                mStockUpdate.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mStockUpdate.SKU_ID = p_SKU_ID;
                mStockUpdate.STOCK_DATE = pClosingdate;
                DataTable dt = mStockUpdate.ExecuteTable();
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

        public DataTable GetSKUClosingStockAndroid(int p_DISTRIBUTOR_ID, int p_SKU_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetSKUStockAndroid mStockUpdate = new uspGetSKUStockAndroid();
                mStockUpdate.Connection = mConnection;
                mStockUpdate.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mStockUpdate.SKU_ID = p_SKU_ID;
                DataTable dt = mStockUpdate.ExecuteTable();
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

        public DataTable GetSKUClosingStockWeb(int p_DISTRIBUTOR_ID, int p_SKU_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetSKUStockWEB mStockUpdate = new uspGetSKUStockWEB();
                mStockUpdate.Connection = mConnection;
                mStockUpdate.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mStockUpdate.SKU_ID = p_SKU_ID;
                DataTable dt = mStockUpdate.ExecuteTable();
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
        public DataTable GetSKUClosingStockWebNew(int p_DISTRIBUTOR_ID,int p_ASSIGNED_DISTRIBUTOR_ID, int p_SKU_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetSKUStockWEBNew mStockUpdate = new uspGetSKUStockWEBNew();
                mStockUpdate.Connection = mConnection;
                mStockUpdate.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mStockUpdate.ASSIGNED_DISTRIBUTOR_ID = p_ASSIGNED_DISTRIBUTOR_ID;
                mStockUpdate.SKU_ID = p_SKU_ID;
                DataTable dt = mStockUpdate.ExecuteTable();
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

        public bool ImportPhysicalStock(int p_DISTRIBUTOR_ID, string pFileName, DateTime p_DOCUMENT_DATE, int p_USER_ID)
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
                DataControl _dc = new DataControl();
                while ((FileContents = ReadSourceFile.ReadLine()) != null)
                {
                    string[] ParametersArr = FileContents.Split(Constants.File_Delimiter);
                    uspGetSKUDetail mSKUS = new uspGetSKUDetail();
                    mSKUS.Connection = mConnection;
                    mSKUS.Transaction = mTransaction;
                    mSKUS.BAR_CODE = ParametersArr[0].ToString().Trim().Substring(0, 12);
                    mSKUS.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    DataTable dt = mSKUS.ExecuteTable();
                    if (dt.Rows.Count > 0)
                    {
                        spInsertPAYSICAL_STOCK mStockDetail = new spInsertPAYSICAL_STOCK();
                        mStockDetail.Connection = mConnection;
                        mStockDetail.Transaction = mTransaction;
                        mStockDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                        mStockDetail.SKU_ID = int.Parse(dt.Rows[0]["SKU_ID"].ToString());
                        mStockDetail.STOCK_DATE = p_DOCUMENT_DATE;
                        mStockDetail.UNIT_RATE = decimal.Parse(dt.Rows[0]["TRADE_PRICE"].ToString());
                        mStockDetail.SALEABLE_QUANTITY = int.Parse(_dc.chkNull_0(ParametersArr[1].ToString()));
                        mStockDetail.UNSALEABLE_QUANTITY = 0;
                        mStockDetail.POST = 0;
                        mStockDetail.Value_Type = 1;// Unit wise
                        mStockDetail.USER_ID = p_USER_ID;
                        mStockDetail.TIME_STAMP = DateTime.Now;
                        mStockDetail.IS_DELETED = false;
                        mStockDetail.ExecuteQuery();
                    }

                }
                mTransaction.Commit();
                return true;
            }
            catch (Exception excp)
            {
                ExceptionPublisher.PublishException(excp);
                mTransaction.Rollback();
                ReadSourceFile.Close();
                mConnection.Close();
                return false;
            }
            finally
            {
                ReadSourceFile.Close();
                mConnection.Close();
            }
        }

        public bool ImportStock(int p_DISTRIBUTOR_ID, string pFileName, DateTime p_DOCUMENT_DATE, int p_USER_ID,int p_SOLD_TO,int p_TypeID)
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
                DataControl _dc = new DataControl();

                DataTable dtSKUS = new DataTable();
                dtSKUS.Columns.Add("SKU_ID", typeof(int));
                dtSKUS.Columns.Add("PRICE", typeof(decimal));
                dtSKUS.Columns.Add("QUANTITY", typeof(decimal));
                dtSKUS.Columns.Add("AMOUNT", typeof(decimal));
                dtSKUS.Columns.Add("RACK_NO", typeof(string));

                DataRow dr = dtSKUS.NewRow();
                decimal Amount = 0;
                while ((FileContents = ReadSourceFile.ReadLine()) != null)
                {
                    string[] ParametersArr = FileContents.Split(Constants.File_Delimiter);
                    uspGetSKUDetail mSKUS = new uspGetSKUDetail();
                    mSKUS.Connection = mConnection;
                    mSKUS.Transaction = mTransaction;
                    mSKUS.BAR_CODE = ParametersArr[0].ToString().Trim().Substring(0, 12);
                    mSKUS.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    DataTable dt = mSKUS.ExecuteTable();
                    if (dt.Rows.Count > 0)
                    {
                        dr = dtSKUS.NewRow();
                        dr["SKU_ID"] = dt.Rows[0]["SKU_ID"];
                        dr["PRICE"] = decimal.Parse(dt.Rows[0]["TRADE_PRICE"].ToString());
                        dr["QUANTITY"] = int.Parse(_dc.chkNull_0(ParametersArr[1].ToString()));
                        dr["AMOUNT"] = int.Parse(_dc.chkNull_0(ParametersArr[1].ToString())) * decimal.Parse(dt.Rows[0]["TRADE_PRICE"].ToString());
                        dr["RACK_NO"] = dt.Rows[0]["RACK_NO"].ToString();
                        Amount += int.Parse(_dc.chkNull_0(ParametersArr[1].ToString())) * decimal.Parse(dt.Rows[0]["TRADE_PRICE"].ToString());
                        dtSKUS.Rows.Add(dr);
                    }
                }

                spInsertPURCHASE_MASTER mPurchaseMaster = new spInsertPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TypeID;
                mPurchaseMaster.ORDER_NUMBER = "";
                if (p_TypeID == 5)
                {
                    mPurchaseMaster.SOLD_FROM = p_DISTRIBUTOR_ID;
                    mPurchaseMaster.PRINCIPAL_ID = 1;
                }
                else
                {
                    mPurchaseMaster.SOLD_FROM = 0;
                    mPurchaseMaster.PRINCIPAL_ID = 0;
                }
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = Amount;
                mPurchaseMaster.USER_ID = p_USER_ID;
                mPurchaseMaster.TIME_STAMP = DateTime.Now;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = 0;
                mPurchaseMaster.BUILTY_NO = "";
                mPurchaseMaster.ExecuteQuery();

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow drDetail in dtSKUS.Rows)
                {
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(drDetail["SKU_ID"].ToString());
                    mPurchaseDetail.RACK_NO = drDetail["RACK_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(drDetail["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = decimal.Parse(drDetail["QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = 0;
                    mPurchaseDetail.AMOUNT = decimal.Parse(drDetail["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = 1;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.RACK_NO;
                    mStockUpdate.ExecuteQuery();
                }

                mTransaction.Commit();
                return true;
            }
            catch (Exception excp)
            {
                ExceptionPublisher.PublishException(excp);
                mTransaction.Rollback();
                ReadSourceFile.Close();
                mConnection.Close();
                return false;
            }
            finally
            {
                ReadSourceFile.Close();
                mConnection.Close();
            }
        }

        #endregion
    }
}

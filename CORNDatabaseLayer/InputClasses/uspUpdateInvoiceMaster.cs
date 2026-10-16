using System;
using System.Data;
using CORNCommon.Classes;
using CORNDataAccessLayer.Classes;

namespace CORNDatabaseLayer.Classes
{
    public class uspUpdateInvoiceMaster
    {
        #region Private Members
        private string sp_Name = "uspUpdateInvoiceMaster";
        private IDbConnection m_connection;
        private IDbTransaction m_transaction;
        private int m_DISTRIBUTOR_ID;
        private int m_ASSIGNED_DISTRIBUTOR_ID;
        private int m_DELIVERYMAN_ID;
        private int m_USER_ID;
        private int m_PAYMENT_MODE;
        private decimal m_TOTAL_AMOUNT;
        private decimal m_TOTAL_NET_AMOUNT;
        private decimal m_EXTRA_DISCOUNT_AMOUNT;
        private decimal m_STANDARD_DISCOUNT_AMOUNT;
        private decimal m_GST_AMOUNT;
        private decimal m_SCHEME_AMOUNT;
        private decimal m_CURRENT_CREDIT_AMOUNT;
        private decimal m_CREDIT_AMOUNT;
        private decimal m_TST_AMOUNT;
        private decimal m_SED_AMOUNT;
        private decimal m_SCHEME_AMOUNT_AGE;
        private decimal m_freightAmount;
        private long m_SALE_INVOICE_ID;
        private long m_SOLD_TO;
        private string m_MANUAL_INVOICE_ID;
        private string m_AUTHORISED_PERSON;
        private string m_REMARKS;
        private int m_Delivery_Method_ID;
        private int m_Total_Qty;
        private int m_isClosed;
        private int m_SALE_EXECUTIVE_ID;
        #endregion
        #region Public Properties


        public int Sale_executive
        {
            set
            {
                m_SALE_EXECUTIVE_ID = value;
            }
            get
            {
                return m_SALE_EXECUTIVE_ID;
            }
        }
        public int isClosed
        {
            set
            {
                m_isClosed = value;
            }
            get
            {
                return m_isClosed;
            }
        }
        public int Total_Qty
        {
            set
            {
                m_Total_Qty = value;
            }
            get
            {
                return m_Total_Qty;
            }
        }

        public int Delivery_Method_ID
        {
            set
            {
                m_Delivery_Method_ID = value;
            }
            get
            {
                return m_Delivery_Method_ID;
            }
        }
        
        public int DISTRIBUTOR_ID
        {
            set
            {
                m_DISTRIBUTOR_ID = value;
            }
            get
            {
                return m_DISTRIBUTOR_ID;
            }
        }


        public int ASSIGNED_DISTRIBUTOR_ID
        {
            set
            {
                m_ASSIGNED_DISTRIBUTOR_ID = value;
            }
            get
            {
                return m_ASSIGNED_DISTRIBUTOR_ID;
            }
        }
        public int DELIVERYMAN_ID
        {
            set
            {
                m_DELIVERYMAN_ID = value;
            }
            get
            {
                return m_DELIVERYMAN_ID;
            }
        }
        public int USER_ID
        {
            set
            {
                m_USER_ID = value;
            }
            get
            {
                return m_USER_ID;
            }
        }
        public int PAYMENT_MODE
        {
            set
            {
                m_PAYMENT_MODE = value;
            }
            get
            {
                return m_PAYMENT_MODE;
            }
        }
        public decimal TOTAL_AMOUNT
        {
            set
            {
                m_TOTAL_AMOUNT = value;
            }
            get
            {
                return m_TOTAL_AMOUNT;
            }
        }
        public decimal TOTAL_NET_AMOUNT
        {
            set
            {
                m_TOTAL_NET_AMOUNT = value;
            }
            get
            {
                return m_TOTAL_NET_AMOUNT;
            }
        }
        public decimal EXTRA_DISCOUNT_AMOUNT
        {
            set
            {
                m_EXTRA_DISCOUNT_AMOUNT = value;
            }
            get
            {
                return m_EXTRA_DISCOUNT_AMOUNT;
            }
        }
        public decimal STANDARD_DISCOUNT_AMOUNT
        {
            set
            {
                m_STANDARD_DISCOUNT_AMOUNT = value;
            }
            get
            {
                return m_STANDARD_DISCOUNT_AMOUNT;
            }
        }
        public decimal GST_AMOUNT
        {
            set
            {
                m_GST_AMOUNT = value;
            }
            get
            {
                return m_GST_AMOUNT;
            }
        }
        public decimal SCHEME_AMOUNT
        {
            set
            {
                m_SCHEME_AMOUNT = value;
            }
            get
            {
                return m_SCHEME_AMOUNT;
            }
        }
        public decimal CURRENT_CREDIT_AMOUNT
        {
            set
            {
                m_CURRENT_CREDIT_AMOUNT = value;
            }
            get
            {
                return m_CURRENT_CREDIT_AMOUNT;
            }
        }
        public decimal CREDIT_AMOUNT
        {
            set
            {
                m_CREDIT_AMOUNT = value;
            }
            get
            {
                return m_CREDIT_AMOUNT;
            }
        }
        public decimal TST_AMOUNT
        {
            set
            {
                m_TST_AMOUNT = value;
            }
            get
            {
                return m_TST_AMOUNT;
            }
        }
        public decimal SED_AMOUNT
        {
            set
            {
                m_SED_AMOUNT = value;
            }
            get
            {
                return m_SED_AMOUNT;
            }
        }
        public decimal SCHEME_AMOUNT_AGE
        {
            set
            {
                m_SCHEME_AMOUNT_AGE = value;
            }
            get
            {
                return m_SCHEME_AMOUNT_AGE;
            }
        }
        public decimal freightAmount
        {
            set
            {
                m_freightAmount = value;
            }
            get
            {
                return m_freightAmount;
            }
        }
        public long SALE_INVOICE_ID
        {
            set
            {
                m_SALE_INVOICE_ID = value;
            }
            get
            {
                return m_SALE_INVOICE_ID;
            }
        }
        public long SOLD_TO
        {
            set
            {
                m_SOLD_TO = value;
            }
            get
            {
                return m_SOLD_TO;
            }
        }
        public string MANUAL_INVOICE_ID
        {
            set
            {
                m_MANUAL_INVOICE_ID = value;
            }
            get
            {
                return m_MANUAL_INVOICE_ID;
            }
        }
        public string AUTHORISED_PERSON
        {
            set
            {
                m_AUTHORISED_PERSON = value;
            }
            get
            {
                return m_AUTHORISED_PERSON;
            }
        }
        public string REMARKS
        {
            set
            {
                m_REMARKS = value;
            }
            get
            {
                return m_REMARKS;
            }
        }


        public IDbConnection Connection
        {
            set
            {
                m_connection = value;
            }
            get
            {
                return m_connection;
            }
        }
        public IDbTransaction Transaction
        {
            set
            {
                m_transaction = value;
            }
            get
            {
                return m_transaction;
            }
        }
        #endregion
        #region Constructor
        public uspUpdateInvoiceMaster()
        {
            m_Delivery_Method_ID = Constants.IntNullValue;
            m_Total_Qty = Constants.IntNullValue;
            m_SALE_EXECUTIVE_ID = Constants.IntNullValue;
            
        }
        #endregion
        #region public Methods
        public bool ExecuteQuery()
        {
            try
            {
                IDbCommand cmd = ProviderFactory.GetCommand(EnumProviders.SQLClient);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sp_Name;
                cmd.Connection = m_connection;
                if (m_transaction != null)
                {
                    cmd.Transaction = m_transaction;
                }
                GetParameterCollection(ref cmd);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
            }
        }
        public IDataReader ExecuteReader()
        {
            try
            {
                IDbCommand command = ProviderFactory.GetCommand(EnumProviders.SQLClient);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = sp_Name;
                command.Connection = m_connection;
                if (m_transaction != null)
                {
                    command.Transaction = m_transaction;
                }
                GetParameterCollection(ref command);
                IDataReader dr = command.ExecuteReader();
                return dr;
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
            }
        }
        public DataTable ExecuteTable()
        {
            try
            {
                IDbCommand command = ProviderFactory.GetCommand(EnumProviders.SQLClient);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = sp_Name;
                command.Connection = m_connection;
                if (m_transaction != null)
                {
                    command.Transaction = m_transaction;
                }
                GetParameterCollection(ref command);
                IDbDataAdapter da = ProviderFactory.GetAdapter(EnumProviders.SQLClient);
                da.SelectCommand = command;
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
            }
        }
        public string ExecuteScalar()
        {
            try
            {
                IDbCommand command = ProviderFactory.GetCommand(EnumProviders.SQLClient);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = sp_Name;
                command.Connection = m_connection;
                if (m_transaction != null)
                {
                    command.Transaction = m_transaction;
                }
                GetParameterCollection(ref command);
                object o;
                o = command.ExecuteScalar();


                return o.ToString();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
            }
        }
        public void FirstReader(IDataReader dr)
        {
            if (dr.Read())
            {
                m_DISTRIBUTOR_ID = Convert.ToInt32(dr["DISTRIBUTOR_ID"]);
                m_DELIVERYMAN_ID = Convert.ToInt32(dr["DELIVERYMAN_ID"]);
                m_USER_ID = Convert.ToInt32(dr["USER_ID"]);
                m_PAYMENT_MODE = Convert.ToInt32(dr["PAYMENT_MODE"]);
                m_TOTAL_AMOUNT = Convert.ToDecimal(dr["TOTAL_AMOUNT"]);
                m_TOTAL_NET_AMOUNT = Convert.ToDecimal(dr["TOTAL_NET_AMOUNT"]);
                m_EXTRA_DISCOUNT_AMOUNT = Convert.ToDecimal(dr["EXTRA_DISCOUNT_AMOUNT"]);
                m_STANDARD_DISCOUNT_AMOUNT = Convert.ToDecimal(dr["STANDARD_DISCOUNT_AMOUNT"]);
                m_GST_AMOUNT = Convert.ToDecimal(dr["GST_AMOUNT"]);
                m_SCHEME_AMOUNT = Convert.ToDecimal(dr["SCHEME_AMOUNT"]);
                m_CURRENT_CREDIT_AMOUNT = Convert.ToDecimal(dr["CURRENT_CREDIT_AMOUNT"]);
                m_CREDIT_AMOUNT = Convert.ToDecimal(dr["CREDIT_AMOUNT"]);
                m_TST_AMOUNT = Convert.ToDecimal(dr["TST_AMOUNT"]);
                m_SED_AMOUNT = Convert.ToDecimal(dr["SED_AMOUNT"]);
                m_SCHEME_AMOUNT_AGE = Convert.ToDecimal(dr["SCHEME_AMOUNT_AGE"]);
                m_freightAmount = Convert.ToDecimal(dr["freightAmount"]);
                m_SALE_INVOICE_ID = Convert.ToInt64(dr["SALE_INVOICE_ID"]);
                m_SOLD_TO = Convert.ToInt64(dr["SOLD_TO"]);
                m_MANUAL_INVOICE_ID = Convert.ToString(dr["MANUAL_INVOICE_ID"]);
                m_AUTHORISED_PERSON = Convert.ToString(dr["AUTHORISED_PERSON"]);
                m_REMARKS = Convert.ToString(dr["REMARKS"]);
            }
        }
        public void GetParameterCollection(ref IDbCommand cmd)
        {
            IDataParameterCollection pparams = cmd.Parameters;
            IDataParameter parameter;
            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@DISTRIBUTOR_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_DISTRIBUTOR_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_DISTRIBUTOR_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@ASSIGNED_DISTRIBUTOR_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_ASSIGNED_DISTRIBUTOR_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_ASSIGNED_DISTRIBUTOR_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@DELIVERYMAN_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_DELIVERYMAN_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_DELIVERYMAN_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@USER_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_USER_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_USER_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@PAYMENT_MODE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_PAYMENT_MODE == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_PAYMENT_MODE;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@TOTAL_AMOUNT";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Money);
            if (m_TOTAL_AMOUNT == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_TOTAL_AMOUNT;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@TOTAL_NET_AMOUNT";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Money);
            if (m_TOTAL_NET_AMOUNT == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_TOTAL_NET_AMOUNT;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EXTRA_DISCOUNT_AMOUNT";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Money);
            if (m_EXTRA_DISCOUNT_AMOUNT == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EXTRA_DISCOUNT_AMOUNT;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@STANDARD_DISCOUNT_AMOUNT";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Money);
            if (m_STANDARD_DISCOUNT_AMOUNT == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_STANDARD_DISCOUNT_AMOUNT;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@GST_AMOUNT";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Money);
            if (m_GST_AMOUNT == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_GST_AMOUNT;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@SCHEME_AMOUNT";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Money);
            if (m_SCHEME_AMOUNT == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_SCHEME_AMOUNT;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@CURRENT_CREDIT_AMOUNT";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Money);
            if (m_CURRENT_CREDIT_AMOUNT == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_CURRENT_CREDIT_AMOUNT;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@CREDIT_AMOUNT";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Money);
            if (m_CREDIT_AMOUNT == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_CREDIT_AMOUNT;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@TST_AMOUNT";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Money);
            if (m_TST_AMOUNT == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_TST_AMOUNT;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@SED_AMOUNT";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Money);
            if (m_SED_AMOUNT == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_SED_AMOUNT;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@SCHEME_AMOUNT_AGE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
            if (m_SCHEME_AMOUNT_AGE == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_SCHEME_AMOUNT_AGE;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@freightAmount";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
            if (m_freightAmount == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_freightAmount;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@SALE_INVOICE_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
            if (m_SALE_INVOICE_ID == Constants.LongNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_SALE_INVOICE_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@SOLD_TO";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
            if (m_SOLD_TO == Constants.LongNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_SOLD_TO;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@MANUAL_INVOICE_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_MANUAL_INVOICE_ID == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_MANUAL_INVOICE_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@AUTHORISED_PERSON";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_AUTHORISED_PERSON == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_AUTHORISED_PERSON;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@REMARKS";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_REMARKS == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_REMARKS;
            }
            pparams.Add(parameter);
            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@Delivery_Method_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_Delivery_Method_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_Delivery_Method_ID;
            }
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@Total_Qty";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_Total_Qty == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_Total_Qty;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@isClosed";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_isClosed == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_isClosed;
            }
            pparams.Add(parameter);




            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@SALE_EXECUTIVE_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_SALE_EXECUTIVE_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_SALE_EXECUTIVE_ID;
            }
            pparams.Add(parameter);

        }
        #endregion
    }
}

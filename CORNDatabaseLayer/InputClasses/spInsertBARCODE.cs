using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CORNDataAccessLayer.Classes;
using CORNCommon.Classes;

namespace CORNDatabaseLayer.Classes
{
    public class spInsertBARCODE
    {
        #region Private Members
        private string sp_Name = "spInsertBARCODE";
		private IDbConnection m_connection;
		private IDbTransaction m_transaction;


		
		private string m_COMPANY_NAME;
		private int m_PRODUCT_id;
        private int m_QUANTITY;
		private byte[] m_BARCODE_IMAGE;
        private decimal m_PRICE;
	
		
		#endregion


		#region Public Properties
		public string COMPANY_NAME
		{
			set
			{
				m_COMPANY_NAME = value ;
			}
			get
			{
				return m_COMPANY_NAME;
			}
		}
        public int PRODUCT_id
		{
			set
			{
                m_PRODUCT_id = value;
			}
			get
			{
                return m_PRODUCT_id;
			}
		}
        public byte[] BARCODE_IMAGE
        {
            set
            {
                m_BARCODE_IMAGE = value;
            }
            get
            {
                return m_BARCODE_IMAGE;
            }
        }
        public int QUANTITY
		{
			set
			{
                m_QUANTITY = value;
			}
			get
			{
                return m_QUANTITY;
			}
		}
        public decimal PRICE
        {
            set
            {
                m_PRICE = value;
            }
            get
            {
                return m_PRICE;
            }
        }


		public IDbConnection  Connection
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
		public IDbTransaction  Transaction
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
        public spInsertBARCODE()
		{


		}
		#endregion

		#region public Methods
		public bool ExecuteQuery()
		{
			try
			{
			    IDbCommand cmd = ProviderFactory.GetCommand(EnumProviders.SQLClient);
				cmd.CommandType =  CommandType.StoredProcedure;
                cmd.CommandText = sp_Name;
				cmd.Connection =   m_connection;
				if(m_transaction!=null)
				{
					cmd.Transaction = m_transaction;
				}
				GetParameterCollection(ref cmd);
				cmd.ExecuteNonQuery();
				//m_BASKET_DETAIL_ID = (long)((IDataParameter)(cmd.Parameters["@BASKET_DETAIL_ID"])).Value;
				return true;
			}
			catch(Exception e)
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
				if(m_transaction!=null)
				{
					command.Transaction = m_transaction;
				}
				GetParameterCollection(ref command);
				IDataReader dr = command.ExecuteReader();
				return dr;
			}
			catch(Exception exp)
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
				if(m_transaction!=null)
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
			catch(Exception exp)
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
				if(m_transaction!=null)
				{
					command.Transaction = m_transaction;
				}
				GetParameterCollection(ref command);
				object o;
				o = command.ExecuteScalar();


				return o.ToString();
			}
			catch(Exception exp)
			{
				throw exp;
			}
			finally
			{
			}
		}


			public void FirstReader(IDataReader dr)
			{
				if(dr.Read())
				{
                    
				}
			}


		    public void GetParameterCollection(ref IDbCommand cmd)
		{
			IDataParameterCollection pparams = cmd.Parameters;
			IDataParameter parameter ;

			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@COMPANY_NAME"; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_COMPANY_NAME==null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_COMPANY_NAME;
			}
			pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@skuId";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_PRODUCT_id == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_PRODUCT_id;
            }
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@BARCODE_IMAGE";
            parameter.DbType = DbType.Binary;
           
            parameter.Value = m_BARCODE_IMAGE;
            pparams.Add(parameter);

			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@quantity"; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_QUANTITY==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
                parameter.Value = m_QUANTITY;
			}
			pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@price";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
            if (m_PRICE == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_PRICE;
            }
            pparams.Add(parameter);


		}
		#endregion
    }
}

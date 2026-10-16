using System;
using System.Data;
using CORNCommon.Classes;
using CORNDataAccessLayer.Classes;

namespace CORNDatabaseLayer.Classes
{
    public class spInsertSKUS2
    {
        #region Private Members
        private string sp_Name = "spInsertSKUS2";
        private IDbConnection m_connection;
        private IDbTransaction m_transaction;
        private short m_UNITS_IN_CASE;
        private short m_UNITS_IN_CASE2;
        private int m_PRINCIPAL_ID;
        private int m_DIVISION_ID;
        private int m_BRAND_ID;
        private int m_CATEGORY_ID;
        private int m_SUBCATEGORY_ID;
        private string m_CATEGORY_TYPE;
        private string m_PicUrl;
        private int m_COMPANY_ID;
        private int m_USER_ID;
        private int m_SKU_ID;
        private int m_SKU_TAG_ID;
       
        private DateTime m_TIME_STAMP;
        private DateTime m_LASTUPDATE_DATE;
        
        private bool m_ISACTIVE;
        private bool m_Display;
        private string m_SKU_CODE;
        private string m_SKU_NAME;
        private string m_COLOR;
        private string m_PACKSIZE;
        private string m_IP_ADDRESS;
        private string m_BAR_CODE;
        private string m_SKU_COUNTRY;
        private string m_SKU_SEASON;
        private string m_PC1;
        private string m_PC2;
        private string m_PC3;

        #endregion
        #region Public Properties
        public short UNITS_IN_CASE
        {
            set
            {
                m_UNITS_IN_CASE = value;
            }
            get
            {
                return m_UNITS_IN_CASE;
            }
        }
        public short UNITS_IN_CASE2
        {
            set
            {
                m_UNITS_IN_CASE2 = value;
            }
            get
            {
                return m_UNITS_IN_CASE2;
            }
        }

        public int PRINCIPAL_ID
        {
            set
            {
                m_PRINCIPAL_ID = value;
            }
            get
            {
                return m_PRINCIPAL_ID;
            }
        }
        public int DIVISION_ID
        {
            set
            {
                m_DIVISION_ID = value;
            }
            get
            {
                return m_DIVISION_ID;
            }
        }
        public int BRAND_ID
        {
            set
            {
                m_BRAND_ID = value;
            }
            get
            {
                return m_BRAND_ID;
            }
        }
        public int CATEGORY_ID
        {
            set
            {
                m_CATEGORY_ID = value;
            }
            get
            {
                return m_CATEGORY_ID;
            }
        }
        public int SUBCATEGORY_ID 
        {
            set
            {
                m_SUBCATEGORY_ID = value;
            }
            get
            {
                return m_SUBCATEGORY_ID;
            }
        }
        public string CATEGORY_TYPE
        {
            set
            {
                m_CATEGORY_TYPE = value;
            }
            get
            {
                return m_CATEGORY_TYPE;
            }
        }
        public string PicUrl
        {
            set { m_PicUrl = value; }
            get { return m_PicUrl; }
        }
        public int COMPANY_ID
        {
            set
            {
                m_COMPANY_ID = value;
            }
            get
            {
                return m_COMPANY_ID;
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
        public int SKU_ID
        {
            get
            {
                return m_SKU_ID;
            }
        }
        public int SKU_TAG_ID
        {
            set
            {
                m_SKU_TAG_ID = value;
            }
            get
            {
                return m_SKU_TAG_ID;
            }
        }
        
        public DateTime TIME_STAMP
        {
            set
            {
                m_TIME_STAMP = value;
            }
            get
            {
                return m_TIME_STAMP;
            }
        }
        public DateTime LASTUPDATE_DATE
        {
            set
            {
                m_LASTUPDATE_DATE = value;
            }
            get
            {
                return m_LASTUPDATE_DATE;
            }
        }
       
        public bool ISACTIVE
        {
            set
            {
                m_ISACTIVE = value;
            }
            get
            {
                return m_ISACTIVE;
            }
        }
        public bool Display
        {
            set { m_Display = value; }
            get { return m_Display; }
        }
        public string SKU_CODE
        {
            set
            {
                m_SKU_CODE = value;
            }
            get
            {
                return m_SKU_CODE;
            }
        }
        public string SKU_NAME
        {
            set
            {
                m_SKU_NAME = value;
            }
            get
            {
                return m_SKU_NAME;
            }
        }
        public string COLOR
        {
            set
            {
                m_COLOR = value;
            }
            get
            {
                return m_COLOR;
            }
        }
        public string PACKSIZE
        {
            set
            {
                m_PACKSIZE = value;
            }
            get
            {
                return m_PACKSIZE;
            }
        }
        public string IP_ADDRESS
        {
            set
            {
                m_IP_ADDRESS = value;
            }
            get
            {
                return m_IP_ADDRESS;
            }
        }
        public string BAR_CODE
        {
            set
            {
                m_BAR_CODE = value;
            }
            get
            {
                return m_BAR_CODE;
            }
        }
        public string SKU_COUNTRY
        {
            set
            {
                m_SKU_COUNTRY = value;
            }
            get
            {
                return m_SKU_COUNTRY;
            }
        }
        public string SKU_SEASON
        {
            set
            {
                m_SKU_SEASON = value;
            }
            get
            {
                return m_SKU_SEASON;
            }
        }
        public string PC1
        {
            set
            {
                m_PC1 = value;
            }
            get
            {
                return m_PC1;
            }
        }
        public string PC2
        {
            set
            {
                m_PC2 = value;
            }
            get
            {
                return m_PC2;
            }
        }
        public string PC3
        {
            set
            {
                m_PC3 = value;
            }
            get
            {
                return m_PC3;
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
        public spInsertSKUS2()
        {
            
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
                m_SKU_ID = (int)((IDataParameter)(cmd.Parameters["@SKU_ID"])).Value;
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
                m_UNITS_IN_CASE = Convert.ToInt16(dr["UNITS_IN_CASE"]);
                m_UNITS_IN_CASE2 = Convert.ToInt16(dr["UNITS_IN_CASE2"]);
                m_PRINCIPAL_ID = Convert.ToInt32(dr["PRINCIPAL_ID"]);
                m_DIVISION_ID = Convert.ToInt32(dr["DIVISION_ID"]);
                m_BRAND_ID = Convert.ToInt32(dr["BRAND_ID"]);
                m_CATEGORY_ID = Convert.ToInt32(dr["CATEGORY_ID"]);
                m_SUBCATEGORY_ID = Convert.ToInt32(dr["SUBCATEGORY_ID"]);
                m_CATEGORY_TYPE = Convert.ToString(dr["CATEGORY_TYPE"]);

                m_COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]);
                m_USER_ID = Convert.ToInt32(dr["USER_ID"]);
                m_SKU_ID = Convert.ToInt32(dr["SKU_ID"]);
                m_SKU_TAG_ID = Convert.ToInt32(dr["SKU_TAG_ID"]);
               
                m_TIME_STAMP = Convert.ToDateTime(dr["TIME_STAMP"]);
                m_LASTUPDATE_DATE = Convert.ToDateTime(dr["LASTUPDATE_DATE"]);
               
                m_ISACTIVE = Convert.ToBoolean(dr["ISACTIVE"]);
                m_SKU_CODE = Convert.ToString(dr["SKU_CODE"]);
                m_SKU_NAME = Convert.ToString(dr["SKU_NAME"]);
                m_COLOR = Convert.ToString(dr["COLOR"]);
                m_PACKSIZE = Convert.ToString(dr["PACKSIZE"]);
                m_IP_ADDRESS = Convert.ToString(dr["IP_ADDRESS"]);
                m_BAR_CODE = Convert.ToString(dr["BAR_CODE"]);
                m_SKU_COUNTRY = Convert.ToString(dr["SKU_COUNTRY"]);
                m_SKU_SEASON = Convert.ToString(dr["SKU_SEASON"]);
                m_PC1 = Convert.ToString(dr["PC1"]);
                m_PC2 = Convert.ToString(dr["PC2"]);
                m_PC3 = Convert.ToString(dr["PC3"]);
              
                
            }
        }
        public void GetParameterCollection(ref IDbCommand cmd)
        {
            IDataParameterCollection pparams = cmd.Parameters;
            IDataParameter parameter;

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@UNITS_IN_CASE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.SmallInt);
            if (m_UNITS_IN_CASE == Constants.ShortNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_UNITS_IN_CASE;
            }
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@UNITS_IN_CASE2";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.SmallInt);
            if (m_UNITS_IN_CASE2 == Constants.ShortNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_UNITS_IN_CASE2;
            }
            pparams.Add(parameter);



            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@PRINCIPAL_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_PRINCIPAL_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_PRINCIPAL_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@DIVISION_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_DIVISION_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_DIVISION_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@BRAND_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_BRAND_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_BRAND_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@CATEGORY_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_CATEGORY_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_CATEGORY_ID;
            }
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@SUBCATEGORY_ID ";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_SUBCATEGORY_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_SUBCATEGORY_ID;
            }
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@CATEGORY_TYPE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_CATEGORY_TYPE == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_CATEGORY_TYPE;
            }
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@PicUrl";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_PicUrl == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_PicUrl;
            }
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@COMPANY_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_COMPANY_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_COMPANY_ID;
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
            parameter.ParameterName = "@SKU_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            parameter.Direction = ParameterDirection.Output;
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@SKU_TAG_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_SKU_TAG_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_SKU_TAG_ID;
            }
            pparams.Add(parameter);


           


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@TIME_STAMP";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
            if (m_TIME_STAMP == Constants.DateNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_TIME_STAMP;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@LASTUPDATE_DATE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
            if (m_LASTUPDATE_DATE == Constants.DateNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_LASTUPDATE_DATE;
            }
            pparams.Add(parameter);


          


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@ISACTIVE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
            parameter.Value = m_ISACTIVE;
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@Display";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
            parameter.Value = m_Display;
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@SKU_CODE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_SKU_CODE == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_SKU_CODE;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@SKU_NAME";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_SKU_NAME == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_SKU_NAME;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@COLOR";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_COLOR == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_COLOR;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@PACKSIZE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_PACKSIZE == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_PACKSIZE;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@IP_ADDRESS";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_IP_ADDRESS == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_IP_ADDRESS;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@BAR_CODE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_BAR_CODE == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_BAR_CODE;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@SKU_COUNTRY";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_SKU_COUNTRY == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_SKU_COUNTRY;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@SKU_SEASON";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_SKU_SEASON == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_SKU_SEASON;
            }
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@PC1";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_PC1 == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_PC1;
            }
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@PC2";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_PC2 == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_PC2;
            }
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@PC3";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_PC3 == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_PC3;
            }
            pparams.Add(parameter);

        }
        #endregion
    }
}
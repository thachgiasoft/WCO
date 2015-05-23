using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Text;
using System.Collections.Generic;

namespace t
{
    public class clsDalADO
    {
        #region Dynamic SQL
        public static DataSet GetDataSet(string TSQL)
        {
            return GetDataSet(TSQL, null);
        }

        public static DataSet GetDataSet(string TSQL, IDbConnection myConn)
        {
            return GetDataSet(TSQL, myConn, null);
        }

        public static DataSet GetDataSet(string TSQL, IDbConnection myConn, IDbTransaction myTrans)
        {
            bool mustClose = false;
            if (myConn == null)
            {
                mustClose = true;
                myConn = clsConn.getConnADO();
            }
            DataSet myDS = new DataSet();
            SqlCommand myCMD = new SqlCommand(TSQL, myConn as SqlConnection);
            SqlDataAdapter myAD = new SqlDataAdapter(myCMD);
            // 
            myCMD.CommandType = CommandType.Text;
            myCMD.CommandTimeout = 180000;//3 phut             
            if (myTrans != null)
                myCMD.Transaction = myTrans as SqlTransaction;
            // 
            myAD.Fill(myDS);
            //
            if (mustClose) myConn.Close();
            return myDS;
        }

        public static IDataReader GetDataReader(string TSQL, IDbConnection myConn)
        {
            IDataReader myRD;
            IDbCommand myCMD = myConn.CreateCommand();
            myCMD.Connection = myConn;
            myCMD.CommandText = TSQL;
            // 
            myCMD.CommandType = CommandType.Text;
            myCMD.CommandTimeout = 180000;
            // 
            myRD = myCMD.ExecuteReader();
            // 
            return myRD;
        }

        public static bool CheckExist(string TSQL)
        {
            //Hàm checkexist này còn dùng cho cả đối với trường hợp check duplicate 
            //Nếu là duplicate sẽ có dạng: SELECT COUNT(*) FROM TABLE WHERE ID=@ID AND xID<>@xID 
            //Nếu là exist sẽ có dạng: SELECT COUNT(*) FROM TABLE WHERE ID=@ID 
            return CheckExist(TSQL, null);
        }

        public static bool CheckExist(string TSQL, IDbConnection myConn)
        {
            //Hàm checkexist này còn dùng cho cả đối với trường hợp check duplicate 
            //Nếu là duplicate sẽ có dạng: SELECT COUNT(*) FROM TABLE WHERE ID=@ID AND xID<>@xID 
            //Nếu là exist sẽ có dạng: SELECT COUNT(*) FROM TABLE WHERE ID=@ID 
            return CheckExist(TSQL, myConn, null);
        }

        public static bool CheckExist(string TSQL, IDbConnection myConn, IDbTransaction myTrans)
        {
            //Hàm checkexist này còn dùng cho cả đối với trường hợp check duplicate 
            //Nếu là duplicate sẽ có dạng: SELECT COUNT(*) FROM TABLE WHERE ID=@ID AND xID<>@xID 
            //Nếu là exist sẽ có dạng: SELECT COUNT(*) FROM TABLE WHERE ID=@ID 
            DataSet myDS = GetDataSet(TSQL, myConn, myTrans);
            if ((int)myDS.Tables[0].Rows[0][0] == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static int AffectData(string TSQL)
        {
            return AffectData(TSQL, null);
        }

        public static int AffectData(string TSQL, IDbConnection myConn)
        {
            return AffectData(TSQL, myConn, null);
        }

        public static int AffectData(string TSQL, IDbConnection myConn, IDbTransaction myTrans)
        {
            return AffectData(TSQL, myConn, myTrans, null);
        }

        public static int AffectData(string TSQL, IDbConnection myConn, IDbTransaction myTrans, List<IDbDataParameter> myParams)
        {
            bool mustClose = false;
            if (myConn == null)
            {
                mustClose = true;
                myConn = clsConn.getConnADO();
            }
            SqlCommand myCMD = new SqlCommand();
            // 
            myCMD.Connection = myConn as SqlConnection;
            if (myTrans != null)
                myCMD.Transaction = myTrans as SqlTransaction;
            //
            myCMD.CommandType = CommandType.Text;
            myCMD.CommandText = TSQL;
            myCMD.CommandTimeout = 180000;//3 phut 
            //
            if (myParams != null)
                AttachParameters(myCMD, myParams);

            int CMDResult = myCMD.ExecuteNonQuery();
            //
            if (mustClose) myConn.Close();
            return CMDResult;
        }

        public static string GetFValue(string TSQL)
        {
            return GetFValue(TSQL, null);
        }

        public static string GetFValue(string TSQL, IDbConnection myConn)
        {
            return GetFValue(TSQL, myConn, null);
        }

        public static string GetFValue(string TSQL, IDbConnection myConn, IDbTransaction myTrans)
        {
            bool mustClose = false;
            if (myConn == null)
            {
                mustClose = true;
                myConn = clsConn.getConnADO();
            }

            DataSet myDS = GetDataSet(TSQL, myConn, myTrans);
            try
            {
                return myDS.Tables[0].Rows[0][0].ToString();
            }
            catch
            {
                return string.Empty;
            }
            finally
            {
                if (mustClose) myConn.Close();
            }
        }

        public static DataTable GetTableSchema(string tableName)
        {
            return GetTableSchema(tableName, null);
        }

        public static DataTable GetTableSchema(string tableName, IDbConnection myConn)
        {
            bool mustClose = false;
            if (myConn == null)
            {
                mustClose = true;
                myConn = clsConn.getConnADO();
            }
            StringBuilder sbSql = new StringBuilder();

            sbSql.AppendLine("SELECT");
            sbSql.AppendLine("	C.*,");
            sbSql.AppendLine("	CASE ");
            sbSql.AppendLine("		WHEN C.COLUMN_DEFAULT IS NOT NULL THEN 1");
            sbSql.AppendLine("	    ELSE 0");
            sbSql.AppendLine("	END AS COLUMN_HASDEFAULT,");
            sbSql.AppendLine("	'' AS [DESCRIPTION],");
            sbSql.AppendLine("	COLUMNPROPERTY( Object_ID(C.TABLE_NAME) ,C.COLUMN_NAME, 'IsIdentity') AS ISIDENTITY,");
            sbSql.AppendLine("	COLUMNPROPERTY( Object_ID(C.TABLE_NAME) ,C.COLUMN_NAME, 'IsComputed') AS ISCOMPUTED,");
            sbSql.AppendLine("	CASE K.CONSTRAINT_TYPE");
            sbSql.AppendLine("	    WHEN 'PRIMARY KEY' THEN 1");
            sbSql.AppendLine("	    ELSE 0");
            sbSql.AppendLine("	END AS ISPK");
            sbSql.AppendLine("FROM INFORMATION_SCHEMA.COLUMNS C (nolock)");
            sbSql.AppendLine("LEFT JOIN ");
            sbSql.AppendLine("( ");
            sbSql.AppendLine("  SELECT CCU.TABLE_SCHEMA, CCU.TABLE_CATALOG, CCU.TABLE_NAME, CCU.COLUMN_NAME, TC.CONSTRAINT_TYPE");
            sbSql.AppendLine("  FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE CCU (nolock)");
            sbSql.AppendLine("  LEFT JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS TC (nolock)");
            sbSql.AppendLine("      ON TC.CONSTRAINT_NAME = CCU.CONSTRAINT_NAME");
            sbSql.AppendLine("	    AND TC.TABLE_NAME = CCU.TABLE_NAME");
            sbSql.AppendLine("      WHERE TC.CONSTRAINT_TYPE LIKE 'PRIMARY KEY'");
            sbSql.AppendLine(") K");
            sbSql.AppendLine("  ON K.TABLE_SCHEMA = C.TABLE_SCHEMA");
            sbSql.AppendLine("  AND K.TABLE_CATALOG = C.TABLE_CATALOG");
            sbSql.AppendLine("  AND K.TABLE_NAME = C.TABLE_NAME");
            sbSql.AppendLine("  AND K.COLUMN_NAME = C.COLUMN_NAME");
            sbSql.AppendFormat("WHERE C.TABLE_NAME LIKE '{0}'", tableName);

            DataSet myDS = GetDataSet(sbSql.ToString(), myConn);
            try
            {
                return myDS.Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                if (mustClose) myConn.Close();
            }
        }

        public static DataTable GetSPParams()
        {
            return GetSPParams(null);
        }

        public static DataTable GetSPParams(IDbConnection myConn)
        {
            bool mustClose = false;
            if (myConn == null)
            {
                mustClose = true;
                myConn = clsConn.getConnADO();
            }
            StringBuilder sbSql = new StringBuilder();

            sbSql.AppendLine("SELECT");
            sbSql.AppendLine("	pa.SPECIFIC_NAME, pa.PARAMETER_NAME, pa.ORDINAL_POSITION, pa.DATA_TYPE, pa.PARAMETER_MODE, pa.CHARACTER_MAXIMUM_LENGTH");
            sbSql.AppendLine("FROM information_schema.parameters pa");
            sbSql.AppendLine("INNER JOIN information_schema.routines  ro ON ro.SPECIFIC_NAME=pa.SPECIFIC_NAME");
            sbSql.AppendLine("WHERE ro.routine_type='PROCEDURE'");
            sbSql.AppendLine("ORDER BY pa.SPECIFIC_NAME, pa.ORDINAL_POSITION");
            
            DataSet myDS = GetDataSet(sbSql.ToString(), myConn);
            try
            {
                return myDS.Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                if (mustClose) myConn.Close();
            }
        }

        public static DataTable GetSPSchema(string SPName, List<IDbDataParameter> myParams)
        {
            return GetSPSchema(SPName, null, myParams);
        }

        public static DataTable GetSPSchema(string SPName, IDbConnection myConn, List<IDbDataParameter> myParams)
        {
            bool mustClose = false;
            if (myConn == null)
            {
                mustClose = true;
                myConn = clsConn.getConnADO();
            }
            IDbTransaction myTrans = myConn.BeginTransaction();
            IDataReader drdr = null;
            try
            {
                AffectData("SET FMTONLY ON;", myConn, myTrans); 
                SqlCommand myCMD = new SqlCommand();
                // 
                myCMD.Connection = myConn as SqlConnection;
                if (myTrans != null)
                    myCMD.Transaction = myTrans as SqlTransaction;
                //
                myCMD.CommandType = CommandType.StoredProcedure;
                myCMD.CommandText = SPName;
                myCMD.CommandTimeout = 180000;//3 phut 
                //
                if (myParams != null)
                    AttachParameters(myCMD, myParams);

                drdr = myCMD.ExecuteReader();
                return drdr.GetSchemaTable();
            }
            catch
            {
                AffectData("SET FMTONLY OFF;", myConn, myTrans);
                return null;
            }
            finally
            {
                if (drdr != null && !drdr.IsClosed)
                    drdr.Close();
                myTrans.Rollback();
                if (mustClose) myConn.Close();
            }
        }
        #endregion

        #region Parameter
        private static SqlDbType ConvertToSqlDbType(string type)
        {
            switch (type.ToLower())
            {
                case "integer":
                case "bigint":
                case "int32":
                case "int64":
                case "uint32":
                case "uint64":
                    return SqlDbType.BigInt;
                case "bit":
                case "boolean":
                    return SqlDbType.Bit;
                case "char":
                    return SqlDbType.Char;
                case "datetime":
                case "date":
                    return SqlDbType.DateTime;
                case "decimal":
                case "numeric":
                case "varnumeric":
                    return SqlDbType.Decimal;
                case "float":
                case "double":
                    return SqlDbType.Float;
                case "image":
                case "binary":
                case "object":
                    return SqlDbType.Image;
                case "int":
                case "int16":
                case "uint16":
                    return SqlDbType.Int;
                case "money":
                case "currency":
                    return SqlDbType.Money;
                case "nchar":
                    return SqlDbType.NChar;
                case "stringfixedlength":
                case "ntext":
                    return SqlDbType.NText;
                case "nvarchar":
                case "string":
                case "guid":
                case "wchar":
                    return SqlDbType.NVarChar;
                case "real":
                case "single":
                    return SqlDbType.Real;
                case "smalldatetime":
                    return SqlDbType.SmallDateTime;
                case "smallint":
                    return SqlDbType.SmallInt;
                case "smallmoney":
                    return SqlDbType.SmallMoney;
                case "text":
                    return SqlDbType.Text;
                case "timestamp":
                    return SqlDbType.Timestamp;
                case "tinyint":
                case "unsignedtinyint":
                case "byte":
                case "sbyte":
                    return SqlDbType.TinyInt;
                case "varbinary":
                    return SqlDbType.VarBinary;
                case "varchar":
                case "ansistring":
                    return SqlDbType.VarChar;
                case "xml":
                    return SqlDbType.Xml;
                case "time":
                    return SqlDbType.Time;
                case "datetime2":
                    return SqlDbType.DateTime2;
                case "datetimeoffset":
                    return SqlDbType.DateTimeOffset;
                case "variant":
                default:
                    return SqlDbType.Variant;
            }
        }

        public static SqlDbType GetSqlDbType(string type)
        {
            int intType = 0;
            if (int.TryParse(type, out intType))
                return (SqlDbType)intType;
            else
                return ConvertToSqlDbType(type);
        }

        private static void AttachParameters(IDbCommand command, List<IDbDataParameter> commandParameters)
        {
            if ((command == null)) throw new ArgumentNullException("command");
            if (((commandParameters != null)))
            {
                foreach (SqlParameter p in commandParameters)
                {
                    if (((p != null)))
                    {
                        // Check for derived output value with no value assigned
                        if ((p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Input) && p.Value == null)
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
            }
        }

        public static IDbDataParameter CreateParameter(string parameterName, string type, object value)
        {
            return CreateParameter(parameterName, type, value, ParameterDirection.Input);
        }

        public static IDbDataParameter CreateParameter(string parameterName, string type, object value, ParameterDirection direction)
        {
            SqlParameter param = new SqlParameter(parameterName, GetSqlDbType(type));
            param.Value = value;
            param.Direction = direction;
            return param;
        }

        public static IDbDataParameter CreateParameter(string parameterName, string type, int size, object value, ParameterDirection direction)
        {
            IDbDataParameter param = CreateParameter(parameterName, type, value, direction);
            param.Size = size;
            return param;
        }
        #endregion
    }
}

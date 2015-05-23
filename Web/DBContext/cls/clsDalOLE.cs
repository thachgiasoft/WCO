using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

namespace t
{
    public class clsDalOLE
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
                myConn = clsConn.getConnOLE();
            }
            if (myConn.State != ConnectionState.Open)
                myConn.Open();
            DataSet myDS = new DataSet();
            OleDbCommand myCMD = new OleDbCommand(TSQL, myConn as OleDbConnection);
            OleDbDataAdapter myAD = new OleDbDataAdapter(myCMD);
            // 
            myCMD.CommandType = CommandType.Text;
            myCMD.CommandTimeout = 180000;//3 phut             
            if (myTrans != null)
                myCMD.Transaction = myTrans as OleDbTransaction;
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
                myConn = clsConn.getConnOLE();
            }
            if (myConn.State != ConnectionState.Open)
                myConn.Open();
            OleDbCommand myCMD = new OleDbCommand();
            // 
            myCMD.Connection = myConn as OleDbConnection;
            if (myTrans != null)
                myCMD.Transaction = myTrans as OleDbTransaction;
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
                myConn = clsConn.getConnOLE();
            }
            if (myConn.State != ConnectionState.Open)
                myConn.Open();

            DataSet myDS = GetDataSet(TSQL, myConn, myTrans);
            try
            {
                if (myDS.Tables[0].Rows.Count > 0)
                    return myDS.Tables[0].Rows[0][0].ToString();
                else
                    return string.Empty;
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
            return null;
        }

        //public static BindingSource GetBindBox(string TSQL)
        //{
        //    return GetBindBox(TSQL, null);
        //}

        //public static BindingSource GetBindBox(string TSQL, IDbConnection myConn)
        //{
        //    bool mustClose = false;
        //    if (myConn == null)
        //    {
        //        mustClose = true;
        //        myConn = clsConn.getConnOLE();
        //    }
        //    if (myConn.State != ConnectionState.Open)
        //        myConn.Open();

        //    DataSet myDS = GetDataSet(TSQL, myConn as OleDbConnection);
        //    if (mustClose) myConn.Close();
        //    BindingSource myBind = new BindingSource();
        //    // 
        //    myBind.DataSource = myDS;
        //    myBind.DataMember = myDS.Tables[0].ToString();
        //    myBind.AddNew();
        //    //Thêm 1 dòng trắng cuối cùng của list 
        //    // 
        //    return myBind;
        //}

        //public static BindingSource GetBindView(string TSQL)
        //{
        //    return GetBindView(TSQL, null);
        //}

        //public static BindingSource GetBindView(string TSQL, IDbConnection myConn)
        //{
        //    bool mustClose = false;
        //    if (myConn == null)
        //    {
        //        mustClose = true;
        //        myConn = clsConn.getConnOLE();
        //    }
        //    if (myConn.State != ConnectionState.Open)
        //        myConn.Open();

        //    DataSet myDS = GetDataSet(TSQL, myConn as OleDbConnection);
        //    if (mustClose) myConn.Close();
        //    BindingSource myBind = new BindingSource();
        //    // 
        //    myBind.DataSource = myDS;
        //    myBind.DataMember = myDS.Tables[0].ToString();
        //    // 
        //    return myBind;
        //}
        #endregion

        #region Parameter
        private static OleDbType ConvertToOleDbType(string type)
        {
            switch (type.ToLower())
            {
                case "integer":
                case "bigint":
                case "int":
                case "int16":
                case "int32":
                case "int64":
                case "uint16":
                case "uint32":
                case "uint64":
                    return OleDbType.Integer;
                case "boolean":
                case "bit":
                    return OleDbType.Boolean;
                case "currency":
                case "money":
                case "smallmoney":
                    return OleDbType.Currency;
                case "date":
                case "datetime":
                case "smalldatetime":
                case "timestamp":
                case "time":
                case "datetime2":
                case "datetimeoffset":
                    return OleDbType.Date;
                case "unsignedtinyint":
                case "tinyint":
                case "byte":
                case "sbyte":
                    return OleDbType.UnsignedTinyInt;
                case "numeric":
                case "decimal":
                case "varnumeric":
                    return OleDbType.Numeric;
                case "double":
                case "float":
                    return OleDbType.Double;
                case "smallint":
                    return OleDbType.SmallInt;
                case "guid":
                    return OleDbType.Guid;
                case "binary":
                case "image":
                case "object":
                    return OleDbType.Binary;
                case "single":
                case "real":
                    return OleDbType.Single;
                case "wchar":
                case "char":
                case "nchar":
                case "ntext":
                case "nvarchar":
                case "text":
                case "varchar":
                case "ansistring":
                case "string":
                case "stringfixedlength":
                default:
                    return OleDbType.WChar;
            }
        }

        public static OleDbType GetOleDbType(string type)
        {
            int intType = 0;
            if (int.TryParse(type, out intType))
                return (OleDbType)intType;
            else
                return ConvertToOleDbType(type);
        }

        private static void AttachParameters(IDbCommand command, List<IDbDataParameter> commandParameters)
        {
            //TuyenHM
            //Chú ý:
            //Đối với access parameter cho SQL query không hành xử theo tên para mà hành xử theo thứ tự add para
            //vào command, đo đó cần xử lý lại thứ tự add para cho comman
            if ((command == null)) throw new ArgumentNullException("command");
            if (((commandParameters != null)))
            {
                Dictionary<int,IDbDataParameter> dicParams=new Dictionary<int,IDbDataParameter>();
                int[] arrParams = new int[commandParameters.Count];
                int i = 0;
                string strTempCommandText = command.CommandText;
                foreach (OleDbParameter p in commandParameters)
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
            OleDbParameter param = new OleDbParameter(parameterName, GetOleDbType(type));
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

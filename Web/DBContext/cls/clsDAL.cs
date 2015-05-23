using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Text;
using System.Data.OleDb;
using System.Collections.Generic;

namespace t
{
    public enum DBManagement : int { Access = 0, SQL = 1, SQLLite = 2,Oracle=3 }
    public enum ProType : int { STRING = 0, VNDATESTRING = 1, USDATESTRING = 2, BOOL = 3, DATETIME = 4, OTHER = 5, NUMBER = 6 }

    public class clsDAL
    {
        public static DBManagement defaultDBMan = DBManagement.SQL;

        #region Utilities
        public static string SelectField(string FieldName, ProType typ)
        {
            return SelectField(FieldName, typ, defaultDBMan);
        }

        public static string SelectField(string FieldName, ProType typ, DBManagement DBMan)
        {
            if (typ == ProType.VNDATESTRING)
            {
                switch (DBMan)
                {
                    case DBManagement.Access:
                        return string.Format("Format$({0},'dd/MM/yyyy') AS {0}", FieldName);
                    default:
                        return string.Format("Convert(varchar, {0}, 103) AS {0}", FieldName);
                }
            }
            else
                return FieldName;
        }

        public static string IsDBNULL(object source, ProType typ)
        {
            return IsDBNULL(source, typ, defaultDBMan);
        }

        public static string IsDBNULL(object source, ProType typ, DBManagement DBMan)
        {
            if (source != null && !string.IsNullOrEmpty(source.ToString()))
            {
                if (typ == ProType.DATETIME || source is DateTime || source is DateTime?)
                {
                    if (DBMan == DBManagement.Oracle)
                    {
                        return clsAll.Date2Oracle((DateTime)source);
                    }
                    else
                    {
                        return clsAll.Date2SQL((DateTime)source);
                    }
                }
                else
                {
                    switch (typ)
                    {
                        case ProType.STRING:
                            if (DBMan == DBManagement.Access || DBMan == DBManagement.Oracle)
                                return string.Format("'{0}'", source);
                            else
                                return string.Format("N'{0}'", source);
                        case ProType.USDATESTRING:
                            return clsAll.DateEN2SQL(source.ToString());
                        case ProType.VNDATESTRING:
                            return clsAll.DateVN2SQL(source.ToString());
                        case ProType.BOOL:
                            if (DBMan == DBManagement.Access)
                                return clsAll.ConvertToBool(source.ToString()).ToString();
                            else
                                return clsAll.ConvertToBit(source.ToString()).ToString();
                        default:
                            if (!string.IsNullOrEmpty(source.ToString()))
                                return string.Format("{0:0.###############}", source);
                            else
                                return "NULL";
                    }
                }
            }
            else
            {
                return "NULL";
            }
        }

        public static object ToDBParam(object source, ProType typ)
        {
            return ToDBParam(source, typ, defaultDBMan);
        }

        public static object ToDBParam(object source, ProType typ, DBManagement DBMan)
        {
            if (source != null && !string.IsNullOrEmpty(source.ToString()))
            {
                //TuyenHM su dung parameter nen ko can convert nua
                //if (typ == ProType.DATETIME || source is DateTime || source is DateTime?)
                //{
                //    return clsAll.Date2SQL((DateTime)source);
                //}
                //else
                //{
                    switch (typ)
                    {
                        case ProType.STRING:
                            return source;
                        case ProType.USDATESTRING:
                            DateTime? dtUS = clsAll.StringUSToDate(source.ToString());
                            if (dtUS == null)
                                return DBNull.Value;
                            else
                                return dtUS;
                        case ProType.VNDATESTRING:
                            DateTime? dtVN = clsAll.StringVNToDate(source.ToString());
                            if (dtVN == null)
                                return DBNull.Value;
                            else
                                return dtVN;
                        case ProType.DATETIME:
                            return (DateTime)source;
                        case ProType.BOOL:
                            if (DBMan == DBManagement.Access)
                                return clsAll.ConvertToBool(source.ToString());
                            else
                                return clsAll.ConvertToBit(source.ToString());
                        case ProType.NUMBER:
                        //TuyenHM    
                        //return string.Format("{0:0.###############}", source);
                            return source;
                        default:
                            return source;
                    }
                //}
            }
            else
            {
                return DBNull.Value;
            }
        }

        public static string DeleteString(string TableName)
        {
            return DeleteString(TableName, defaultDBMan);
        }

        public static string DeleteString(string TableName, DBManagement DBMan)
        {
            switch (DBMan)
            {
                case DBManagement.Access:
                    return string.Format("DELETE * FROM {0}", TableName);
                default:
                    return string.Format("DELETE FROM {0}", TableName);
            }
        }

        public static string ConvertFuntion(DBManagement toDB, string func)
        {
            switch (toDB)
            {
                case DBManagement.SQL:
                    return func.Replace("MID(", "SUBSTRING(");
                case DBManagement.Access:
                    return func.Replace("SUBSTRING(", "MID(");
            }

            return func;
        }

        #endregion

        #region Dynamic SQL
        public static DataSet GetDataSet(string TSQL)
        {
            return GetDataSet(TSQL, defaultDBMan);
        }

        public static DataSet GetDataSet(string TSQL, DBManagement dbtyp)
        {
            return GetDataSet(TSQL, null, dbtyp);
        }

        public static DataSet GetDataSet(string TSQL, IDbConnection myConn)
        {
            return GetDataSet(TSQL, myConn, defaultDBMan);
        }

        public static DataSet GetDataSet(string TSQL, IDbConnection myConn, DBManagement dbtyp)
        {
            return GetDataSet(TSQL, myConn, null, dbtyp);
        }

        public static DataSet GetDataSet(string TSQL, IDbConnection myConn, IDbTransaction myTrans)
        {
            return GetDataSet(TSQL, myConn, myTrans, defaultDBMan);
        }

        public static DataSet GetDataSet(string TSQL, IDbConnection myConn, IDbTransaction myTrans, DBManagement dbtyp)
        {
            switch (dbtyp)
            {
                case DBManagement.Access:
                    return clsDalOLE.GetDataSet(TSQL, myConn, myTrans);
                case DBManagement.Oracle:
                   // return clsDalORACLE.GetDataSet(TSQL, myConn, myTrans);
                default:
                    return clsDalADO.GetDataSet(TSQL, myConn, myTrans);
            }
        }

        public static IDataReader GetDataReader(string TSQL, SqlConnection myConn, DBManagement dbtyp)
        {
            switch (dbtyp)
            {
                case DBManagement.Access:
                    return clsDalOLE.GetDataReader(TSQL, myConn);
                case DBManagement.Oracle:
                   // return clsDalORACLE.GetDataReader(TSQL, myConn);
                default:
                    return clsDalADO.GetDataReader(TSQL, myConn);
            }
        }

        public static bool CheckExist(string TSQL)
        {
            //Hàm checkexist này còn dùng cho cả đối với trường hợp check duplicate 
            //Nếu là duplicate sẽ có dạng: SELECT COUNT(*) FROM TABLE WHERE ID=@ID AND xID<>@xID 
            //Nếu là exist sẽ có dạng: SELECT COUNT(*) FROM TABLE WHERE ID=@ID 
            return CheckExist(TSQL, defaultDBMan);
        }

        public static bool CheckExist(string TSQL, DBManagement dbtyp)
        {
            //Hàm checkexist này còn dùng cho cả đối với trường hợp check duplicate 
            //Nếu là duplicate sẽ có dạng: SELECT COUNT(*) FROM TABLE WHERE ID=@ID AND xID<>@xID 
            //Nếu là exist sẽ có dạng: SELECT COUNT(*) FROM TABLE WHERE ID=@ID 
            return CheckExist(TSQL, null, dbtyp);
        }

        public static bool CheckExist(string TSQL, IDbConnection myConn)
        {
            //Hàm checkexist này còn dùng cho cả đối với trường hợp check duplicate 
            //Nếu là duplicate sẽ có dạng: SELECT COUNT(*) FROM TABLE WHERE ID=@ID AND xID<>@xID 
            //Nếu là exist sẽ có dạng: SELECT COUNT(*) FROM TABLE WHERE ID=@ID 
            return CheckExist(TSQL, myConn, defaultDBMan);
        }

        public static bool CheckExist(string TSQL, IDbConnection myConn, DBManagement dbtyp)
        {
            //Hàm checkexist này còn dùng cho cả đối với trường hợp check duplicate 
            //Nếu là duplicate sẽ có dạng: SELECT COUNT(*) FROM TABLE WHERE ID=@ID AND xID<>@xID 
            //Nếu là exist sẽ có dạng: SELECT COUNT(*) FROM TABLE WHERE ID=@ID 
            return CheckExist(TSQL, myConn, null, dbtyp);
        }

        public static bool CheckExist(string TSQL, IDbConnection myConn, IDbTransaction myTrans)
        {
            return CheckExist(TSQL, myConn, myTrans, defaultDBMan);
        }

        public static bool CheckExist(string TSQL, IDbConnection myConn, IDbTransaction myTrans, DBManagement dbtyp)
        {
            //Hàm checkexist này còn dùng cho cả đối với trường hợp check duplicate 
            //Nếu là duplicate sẽ có dạng: SELECT COUNT(*) FROM TABLE WHERE ID=@ID AND xID<>@xID 
            //Nếu là exist sẽ có dạng: SELECT COUNT(*) FROM TABLE WHERE ID=@ID 
            DataSet myDS = GetDataSet(TSQL, myConn, myTrans, dbtyp);
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
            return AffectData(TSQL, defaultDBMan);
        }

        public static int AffectData(string TSQL, DBManagement dbtyp)
        {
            return AffectData(TSQL, null, dbtyp);
        }

        public static int AffectData(string TSQL, IDbConnection myConn)
        {
            return AffectData(TSQL, myConn, defaultDBMan);
        }

        public static int AffectData(string TSQL, IDbConnection myConn, DBManagement dbtyp)
        {
            return AffectData(TSQL, myConn, null, dbtyp);
        }

        public static int AffectData(string TSQL, IDbConnection myConn, IDbTransaction myTrans, DBManagement dbtyp)
        {
            return AffectData(TSQL, myConn, myTrans, dbtyp, null);
        }
        public static int AffectData(string TSQL, IDbConnection myConn, IDbTransaction myTrans, params IDbDataParameter[] myParams)
        {
            return AffectData(TSQL, myConn, myTrans, defaultDBMan, null);
        }

        public static int AffectData(string TSQL, IDbConnection myConn, IDbTransaction myTrans, DBManagement dbtyp, List<IDbDataParameter> myParams)
        {
            switch (dbtyp)
            {
                case DBManagement.Access:
                    return clsDalOLE.AffectData(TSQL, myConn, myTrans, myParams);
                case DBManagement.Oracle:
                   // return clsDalORACLE.AffectData(TSQL, myConn, myTrans, myParams);
                default:
                    return clsDalADO.AffectData(TSQL, myConn, myTrans, myParams);
            }
        }

        public static int AffectData(string TSQL, IDbConnection myConn, IDbTransaction myTrans,  List<IDbDataParameter> myParams)
        {
            return AffectData(TSQL, myConn, myTrans, defaultDBMan, myParams);
        }

        public static string GetFValue(string TSQL)
        {
            return GetFValue(TSQL, defaultDBMan);
        }

        public static string GetFValue(string TSQL, DBManagement dbtyp)
        {
            return GetFValue(TSQL, null, dbtyp);
        }

        public static string GetFValue(string TSQL, IDbConnection myConn)
        {
            return GetFValue(TSQL, myConn, defaultDBMan);
        }

        public static string GetFValue(string TSQL, IDbConnection myConn, DBManagement dbtyp)
        {
            return GetFValue(TSQL, myConn, null, dbtyp);
        }

        public static string GetFValue(string TSQL, IDbConnection myConn, IDbTransaction myTrans, DBManagement dbtyp)
        {
            switch (dbtyp)
            {
                case DBManagement.Access:
                    return clsDalOLE.GetFValue(TSQL, myConn, myTrans);
                case DBManagement.Oracle:
                    //return clsDalORACLE.GetFValue(TSQL, myConn, myTrans);
                default:
                    return clsDalADO.GetFValue(TSQL, myConn, myTrans);
            }
        }

        public static DataTable GetTableSchema(string tableName)
        {
            return GetTableSchema(tableName, defaultDBMan);
        }

        public static DataTable GetTableSchema(string tableName, DBManagement dbtyp)
        {
            return GetTableSchema(tableName, null, dbtyp);
        }

        public static DataTable GetTableSchema(string tableName, IDbConnection myConn)
        {
            return GetTableSchema(tableName, myConn, defaultDBMan);
        }

        public static DataTable GetTableSchema(string tableName, IDbConnection myConn, DBManagement dbtyp)
        {
            return GetTableSchema(tableName, myConn, null, dbtyp);
        }

        public static DataTable GetTableSchema(string tableName, IDbConnection myConn, IDbTransaction myTrans, DBManagement dbtyp)
        {
            switch (dbtyp)
            {
                case DBManagement.Access:
                    return clsDalOLE.GetTableSchema(tableName, myConn);
                default:
                    return clsDalADO.GetTableSchema(tableName, myConn);
            }
        }
        #endregion

        #region Parameter

        private static void AttachParameters(IDbCommand command, IDbDataParameter[] commandParameters)
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
            return CreateParameter(parameterName, type, value, defaultDBMan);
        }

        public static IDbDataParameter CreateParameter(string parameterName, string type, object value, DBManagement dbtyp)
        {
            switch (dbtyp)
            {
                case DBManagement.Access:
                    return clsDalOLE.CreateParameter(parameterName, type, value);
                case DBManagement.Oracle:
                    //return clsDalORACLE.CreateParameter(parameterName, type, value);
                default:
                    return clsDalADO.CreateParameter(parameterName, type, value);
            }
        }

        public static IDbDataParameter CreateParameter(string parameterName, string type, object value, ParameterDirection direction)
        {
            return CreateParameter(parameterName, type, value, direction, defaultDBMan);
        }

        public static IDbDataParameter CreateParameter(string parameterName, string type, object value, ParameterDirection direction, DBManagement dbtyp)
        {
            switch (dbtyp)
            {
                case DBManagement.Access:
                    return clsDalOLE.CreateParameter(parameterName, type, value, direction);
                case DBManagement.Oracle:
                   // return clsDalORACLE.CreateParameter(parameterName, type, value, direction);
                default:
                    return clsDalADO.CreateParameter(parameterName, type, value, direction);
            }
        }

        public static IDbDataParameter CreateParameter(string parameterName, string type, int size, object value, ParameterDirection direction)
        {
            return CreateParameter(parameterName, type, size, value, direction, defaultDBMan);
        }

        public static IDbDataParameter CreateParameter(string parameterName, string type, int size, object value, ParameterDirection direction, DBManagement dbtyp)
        {
            switch (dbtyp)
            {
                case DBManagement.Access:
                    return clsDalOLE.CreateParameter(parameterName, type, size, value, direction);
                case DBManagement.Oracle:
                    //return clsDalORACLE.CreateParameter(parameterName, type, size, value, direction);
                default:
                    return clsDalADO.CreateParameter(parameterName, type, size, value, direction);
            }
        }
        #endregion
    }
}

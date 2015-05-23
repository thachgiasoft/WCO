using System;
using System.Data.OleDb;
using System.Data.SqlClient;
//using System.Data.SQLite;
using System.Data;
//using System.Data.OracleClient;

namespace t
{
    public class clsConn
    {
        public static IDbConnection getConnection(DBManagement dbtyp)
        {
            switch (dbtyp)
            {
                case DBManagement.Access:
                    return getConnOLE();
                case DBManagement.Oracle:
                 //   return getConnOracle();
                default:
                    return getConnADO();
                //default:
                //    return getConnSQLite();
            }
        }

        public static IDbConnection getConnection(string connString, DBManagement dbtyp)
        {
            switch (dbtyp)
            {
                case DBManagement.Access:
                    return getConnOLE(connString);
                case DBManagement.Oracle:
                  //  return getConnOracle(connString);
                default:
                    return getConnADO(connString);
                //default:
                //    return getConnSQLite(connString);
            }
        }

        public static OleDbConnection getConnOLE()
        {
            string connString = st.sqlSTRINGOLE;
            return getConnOLE(connString);
        }

        public static OleDbConnection getConnOLE(string connString)
        {
            OleDbConnection myConn = new OleDbConnection(connString);
            try
            {
                myConn.Open();
                return myConn;
            }
            catch (Exception ex)
            {
                throw new Exception("Hệ thống hiện tại bị mất kết nối tới máy chủ chứa cơ sở dữ liệu. Xin vui lòng kiểm tra lại!" + ex.Message);
                //return null;
            }
        }

        public static OleDbConnection getConnXLS(string FILEPATH)
        {
            string strConnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|" + FILEPATH + ";Extended Properties='Excel 8.0;HDR=YES'";
            OleDbConnection myConn = new OleDbConnection(strConnString);
            try
            {
                myConn.Open();
                return myConn;
            }
            catch (Exception ex)
            {
                throw new Exception("Hệ thống hiện tại bị mất kết nối tới máy chủ chứa cơ sở dữ liệu. Xin vui lòng kiểm tra lại!" + ex.Message);
                //return null;
            }
        }

        public static SqlConnection getConnADO()
        {
            string connString = st.sqlSTRINGADO;
            return getConnADO(connString);
        }

        public static SqlConnection getConnADO(string strConnString)
        {
            SqlConnection myConn = new SqlConnection(strConnString);
            try
            {
                myConn.Open();
                return myConn;
            }
            catch (Exception ex)
            {
                throw new Exception("Hệ thống hiện tại bị mất kết nối tới máy chủ chứa cơ sở dữ liệu. Xin vui lòng kiểm tra lại!" + ex.Message);
                //return null;
            }
        }

        //public static SQLiteConnection getConnSQLite()
        //{
        //    string connString = st.sqlSTRINGLITE;
        //    return getConnSQLite(connString);
        //}

        //public static SQLiteConnection getConnSQLite(string connString)
        //{
        //    SQLiteConnection myConn = new SQLiteConnection(connString);
        //    try
        //    {
        //        myConn.Open();
        //        return myConn;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Hệ thống hiện tại bị mất kết nối tới máy chủ chứa cơ sở dữ liệu. Xin vui lòng kiểm tra lại!" + ex.Message);
        //        //return null;
        //    }
        //}

        //public static OracleConnection getConnOracle()
        //{
        //    string connString = st.sqlSTRINGORACLE;
        //    return getConnOracle(connString);
        //}

        //public static OracleConnection getConnOracle(string strConnString)
        //{
        //    OracleConnection myConn = new OracleConnection(strConnString);
        //    try
        //    {
        //        myConn.Open();
        //        return myConn;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Hệ thống hiện tại bị mất kết nối tới máy chủ chứa cơ sở dữ liệu. Xin vui lòng kiểm tra lại!" + ex.Message);
        //        //return null;
        //    }
        //}
    }
}

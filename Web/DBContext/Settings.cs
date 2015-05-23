using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace t
{
    public class st
    {
        //Liên quan tới CSDL
        public static string AccessDBDir = "";
        public static string sqlSTRINGADO = "Data Source=.\\SQLEXPRESS;Initial Catalog=mojotest1;User ID=sa;pwd=1";
        public static string sqlSTRINGOLE = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\NACCS.VN.mdb;Persist Security Info=True;Jet OLEDB:Database Password=tuyenchim";
        public static string sqlSTRINGLITE = "Data Source= |DataDirectory|NACCS.VN.db;Version=3;New=False;Compress=True;";
        public static string sqlSTRINGORACLE = "Data Source=//{0}:{1}/{2};User ID={3};pwd={4}";
    }
}

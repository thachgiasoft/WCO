//using DevComponents.DotNetBar.Controls;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using mojoPortal.Web.UI;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace t
{
    public class clsAll
    {
        public static void BindData(Control c, object o)
        {
            mojoLabel lbl = c as mojoLabel;
            if (lbl != null)
            {
                PropertyInfo p = o.GetType().GetProperty(lbl.ID);
                if (p != null)
                {
                    lbl.Text = string.Format("{0}", p.GetValue(o));
                }
                return;
            }

            foreach (Control c1 in c.Controls)
            {
                BindData(c1, o);
            }
        }

        public static void CopyData(HtmlTable tblDetail, object outObj)
        {
            foreach (PropertyInfo i in outObj.GetType().GetProperties())
            {
                if (i.PropertyType.FullName.Equals(typeof(string).FullName) == true)
                {
                    TextBox txt = tblDetail.FindControl(i.Name) as TextBox;
                    if (txt != null)
                    {
                        i.SetValue(outObj, txt.Text, null);
                        continue;
                    }

                    //Thử với HidenField
                    HiddenField hd = tblDetail.FindControl(i.Name) as HiddenField;
                    if (hd != null)
                    {
                        i.SetValue(outObj, hd.Value, null);
                        continue;
                    }

                    //Thử với mojoDropDownList
                    mojoDropDownList dr = tblDetail.FindControl(i.Name) as mojoDropDownList;
                    if (dr != null)
                    {
                        i.SetValue(outObj, dr.SelectedValue, null);
                        continue;
                    }

                    //Không thuộc trường hợp nào
                    continue;
                }
                if (i.PropertyType.FullName.Equals(typeof(int).FullName) == true)
                {
                    //Thử với HidenField
                    HiddenField hd = tblDetail.FindControl(i.Name) as HiddenField;
                    if (hd == null) continue;
                    if (hd.Value == null) continue;
                    if (string.IsNullOrEmpty(hd.Value) == true) continue;
                    int intValue = -1;
                    bool tryP = int.TryParse(hd.Value, out intValue);
                    if (tryP == false) continue;
                    i.SetValue(outObj, intValue, null);
                    continue;
                }
                if (i.PropertyType.FullName.Equals(typeof(decimal).FullName) == true)
                {
                    TextBox txt = tblDetail.FindControl(i.Name) as TextBox;
                    if (txt == null) continue;
                    decimal dm = 0;
                    if (decimal.TryParse(txt.Text, out dm) == true)
                        i.SetValue(outObj, dm, null);
                }
                if (i.PropertyType.FullName.Equals(typeof(DateTime).FullName) == true)
                {
                    jDatePicker txt = tblDetail.FindControl(i.Name) as jDatePicker;
                    if (txt == null) continue;
                    DateTime dm = default(DateTime);
                    if (DateTime.TryParseExact(txt.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out dm) == true)
                        i.SetValue(outObj, dm, null);
                }
                if (i.PropertyType.FullName.Equals(typeof(DateTime?).FullName) == true)
                {
                    jDatePicker txt = tblDetail.FindControl(i.Name) as jDatePicker;
                    if (txt == null) continue;
                    DateTime dm = default(DateTime);
                    if (txt.Text.Length == 10)
                    {
                        if (DateTime.TryParseExact(txt.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out dm) == true)
                            i.SetValue(outObj, dm, null);
                    }
                    if (txt.Text.Length == 21)
                    {
                        /*string[] formats = {"M/d/yyyy h:mm:ss tt", "M/d/yyyy h:mm tt", 
                       "MM/dd/yyyy hh:mm:ss", "M/d/yyyy h:mm:ss", 
                       "M/d/yyyy hh:mm tt", "M/d/yyyy hh tt", 
                       "M/d/yyyy h:mm", "M/d/yyyy h:mm", 
                       "MM/dd/yyyy hh:mm", "M/dd/yyyy hh:mm"};
                       string[] dateStrings = {"5/1/2009 6:32 PM", "05/01/2009 6:32:05 PM", 
                        "5/1/2009 6:32:00", "05/01/2009 06:32", 
                        "05/01/2009 06:32:00 PM", "05/01/2009 06:32:00"};
                        if (DateTime.TryParseExact(txt.Text, formats,
                             new CultureInfo("en-US"),
                             DateTimeStyles.None,
                             out dm))
                            i.SetValue(outObj, dm, null);*/

                        if (DateTime.TryParseExact(txt.Text, "dd/MM/yyyy 0:HH-mm-ss", null, DateTimeStyles.None, out dm) == true)
                            i.SetValue(outObj, dm, null);
                    }

                    else
                    {
                        if (DateTime.TryParse(txt.Text, CultureInfo.CreateSpecificCulture("vi-VN"), DateTimeStyles.None, out dm) == true)
                        {
                            i.SetValue(outObj, dm, null);
                        }
                    }
                }
                if (i.PropertyType.FullName.Equals(typeof(bool).FullName) == true)
                {
                    CheckBox txt = tblDetail.FindControl(i.Name) as CheckBox;
                    if (txt == null) continue;
                    i.SetValue(outObj, txt.Checked);
                }
            }
        }

        public static void BindData(object outObj, HtmlTable tblDetail)
        {
            foreach (PropertyInfo i in outObj.GetType().GetProperties())
            {
                if (i.PropertyType.FullName.Equals(typeof(string).FullName) == true)
                {
                    TextBox txt = tblDetail.FindControl(i.Name) as TextBox;
                    if (txt != null)
                    {
                        txt.Text = string.Format("{0}", i.GetValue(outObj, null));
                        continue;
                    }

                    //Thử với HidenField
                    HiddenField hd = tblDetail.FindControl(i.Name) as HiddenField;
                    if (hd != null)
                    {
                        hd.Value = string.Format("{0}", i.GetValue(outObj, null));
                        continue;
                    }

                    mojoDropDownList dr = tblDetail.FindControl(i.Name) as mojoDropDownList;
                    if (dr != null)
                    {
                        dr.SelectedValue = string.Format("{0}", i.GetValue(outObj, null));
                        continue;
                    }

                    continue;
                }
                if (i.PropertyType.FullName.Equals(typeof(int).FullName) == true)
                {
                    //Thử với HidenField
                    HiddenField hd = tblDetail.FindControl(i.Name) as HiddenField;
                    if (hd == null) continue;
                    hd.Value = string.Format("{0}", i.GetValue(outObj, null));
                    continue;
                }
                if (i.PropertyType.FullName.Equals(typeof(decimal).FullName) == true)
                {
                    TextBox txt = tblDetail.FindControl(i.Name) as TextBox;
                    if (txt == null) continue;
                    decimal dm = 0;
                    if (decimal.TryParse(txt.Text, out dm) == true)
                        // i.SetValue(outObj, dm, null);
                        txt.Text = string.Format("{0}", i.GetValue(outObj, null));
                }
                if (i.PropertyType.FullName.Equals(typeof(DateTime).FullName) == true ||
                    i.PropertyType.FullName.Equals(typeof(DateTime?).FullName) == true)
                {
                    jDatePicker txt = tblDetail.FindControl(i.Name) as jDatePicker;
                    if (txt == null) continue;
                    object objValue = i.GetValue(outObj, null);
                    if (objValue == null)
                    {
                        txt.Text = "";
                        continue;
                    }
                    DateTime? dt = objValue as DateTime?;
                    if (dt.HasValue == false)
                    {
                        txt.Text = "";
                        continue;
                    }
                    txt.Text = string.Format("{0:dd/MM/yyyy}", dt);

                }
                if (i.PropertyType.FullName.Equals(typeof(bool).FullName) == true)
                {
                    CheckBox txt = tblDetail.FindControl(i.Name) as CheckBox;
                    if (txt == null) continue;
                    object obj = i.GetValue(outObj);
                    if (obj == null)
                    {
                        txt.Checked = false;
                        continue;
                    }
                    txt.Checked = (bool)obj;
                }
            }
        }

        public static DataTable AddFirstRowEmpty(DataTable DATA)
        {
            if (DATA == null) return null;
            if (DATA == default(DataTable)) return default(DataTable);
            DataRow rFirst = DATA.NewRow();
            foreach (DataColumn c in DATA.Columns)
            {
                if (c.DataType.Equals(typeof(string)) == false) continue;
                rFirst[c.ColumnName] = string.Empty;
            }
            DATA.Rows.InsertAt(rFirst, 0);
            return DATA;
        }

  
        public static object[,] DataTable2ArrayObjects(DataTable source)
        {
            object[,] objData = new object[source.Rows.Count, source.Columns.Count];
            if (source != null)
            {
                for (int rowNumber = 0; rowNumber < source.Rows.Count; rowNumber++)
                {
                    for (int columnNumber = 0; columnNumber < source.Columns.Count; columnNumber++)
                    {
                        objData[rowNumber, columnNumber] = source.Rows[rowNumber][columnNumber];
                    }
                }
            }
            return objData;
        }

        public static object[,] DataTable2ArrayObjectsAndRemoveZeroValue(DataTable source)
        {
            object[,] objData = new object[source.Rows.Count, source.Columns.Count];
            if (source != null)
            {
                for (int rowNumber = 0; rowNumber < source.Rows.Count; rowNumber++)
                {
                    for (int columnNumber = 0; columnNumber < source.Columns.Count; columnNumber++)
                    {
                        if (source.Rows[rowNumber][columnNumber] != null && source.Rows[rowNumber][columnNumber].ToString().Equals("0"))
                        {
                            objData[rowNumber, columnNumber] = string.Empty;
                            continue;
                        }
                        objData[rowNumber, columnNumber] = source.Rows[rowNumber][columnNumber];
                    }
                }
            }
            return objData;
        }

        public static object[,] DataRowArray2ObjectArray(DataRow[] source)
        {
            if (source == null) return null;
            if (source.Length < 1) return null;
            int columnCount = source[0].ItemArray.Length;
            object[,] objData = new object[source.Length, columnCount];
            for (int rowNumber = 0; rowNumber < source.Length; rowNumber++)
            {
                for (int columnNumber = 0; columnNumber < columnCount; columnNumber++)
                {
                    objData[rowNumber, columnNumber] = source[rowNumber][columnNumber];
                }
            }
            return objData;
        }

       

        public static void ExtractXLS(string xXLSNAME, string xTEMPTNAME)
        {
            //try
            //{
            //    if (!System.IO.Directory.Exists(st.TEMP_DIR))
            //    {
            //        System.IO.Directory.CreateDirectory(st.TEMP_DIR);
            //    }
            //    //                
            //    string tmpLibFile = Path.Combine(st.TEMP_DIR, ct.RAR_FILENAME);
            //    string outPutDir = st.TEMP_DIR;
              
            //    if (!Directory.Exists(outPutDir))
            //        Directory.CreateDirectory(outPutDir);
            //    string tmpSourceFile = Path.Combine(outPutDir, xXLSNAME);
            //    if (!File.Exists(tmpSourceFile))
            //    {
            //        //
            //        //'Export file LIB------------------- 
            //        try
            //        {
            //            File.WriteAllBytes(tmpLibFile, Properties.Resources.NACCS);
            //        }
            //        catch { }
            //        //---------------------- 
            //        string tmpZipper = clsAll.GetExtractor();
            //        string strEX = tmpZipper + " e -ep \"" + tmpLibFile + "\" " + xXLSNAME + " \"" + outPutDir + "\" /o+";
            //        Interaction.Shell(strEX, AppWinStyle.Hide, true, 100000);
            //        File.Delete(tmpLibFile);
            //        clsAll.DelExtractor();
            //    }
            //    System.IO.File.Copy(tmpSourceFile, xTEMPTNAME);                
            //}
            //catch (Exception ex)
            //{
            //    string s = ex.Message;
            //   // clsAll.ShowGlobalError("clsPrivate", ex);
            //}
        }

        public static DataTable DataTableOriginal(DataTable source)
        {
            source.RejectChanges();
            return source;
        }

        public static List<T> DataTable2ListObjects<T>(DataTable source)
        {
            List<T> list = new List<T>();
            if (source != null)
            {
                foreach (DataRow dr in source.Rows)
                {
                    T obj = Activator.CreateInstance<T>();
                    clsAll.DataRow2Object(dr, obj);
                    list.Add(obj);
                }
            }
            return list;
        }

        public static void ExtractFile(string strDir,string strFileName)
        {
            //string strExtractor = clsAll.GetExtractor();  
            //string strExract = string.Format("{0} e -ep \"{1}\" {2} \"{3}\" /o+", strExtractor, "{0}", "{1}", strDir);
            //string strFilePath = Path.Combine(strDir, strFileName);
            //if (File.Exists(strFilePath) == false)
            //{
            //    string strRar = Path.Combine(strDir, ct.RAR_FILENAME);
            //    if(File.Exists(strRar)==false)
            //        File.WriteAllBytes(strRar, Properties.Resources.NACCS);

            //    //---------------------- 
            //    string strEX = string.Format(strExract, strRar, strFileName);
            //    Interaction.Shell(strEX, AppWinStyle.Hide, true, 100000);
            //    System.IO.File.Delete(strRar);
            //    Application.DoEvents();
            //}
        }

        public static void ExtractJobFile(string strDir, string strFileName)
        {            
            //if (Directory.Exists(strDir) == false) Directory.CreateDirectory(strDir);
            //string strFilePath = Path.Combine(strDir, strFileName);
            //if (File.Exists(strFilePath) == false)
            //{
            //    string strExtractor = clsAll.GetExtractor();
            //    string strExract = string.Format("{0} e -ep \"{1}\" \"{2}\" \"{3}\" /o+", strExtractor, "{0}", "{1}", strDir);
            //    string strRar = Path.Combine(Application.StartupPath, ct.APP_LOCALRESOURCES);
            //    if (File.Exists(strRar) == false)
            //        File.WriteAllBytes(strRar, Properties.Resources.App_LocalResources);

            //    //---------------------- 
            //    string strEX = string.Format(strExract, strRar, strFilePath.Replace(Path.Combine(Application.StartupPath,@"App_LocalResources\"),""));
            //    Interaction.Shell(strEX, AppWinStyle.Hide, true, 100000);
            //    System.IO.File.Delete(strRar);
            //    Application.DoEvents();
            //}
        }

        //public static string GetExtractor()
        //{
        //    string tmpZipper = Path.Combine(Application.StartupPath, ct.EXTRACTOR_FILENAME);
        //    try
        //    {            
        //        if(File.Exists(tmpZipper)==false)
        //            File.WriteAllBytes(tmpZipper, Properties.Resources.compress);
        //    }
        //    catch { }
        //    return tmpZipper;
        //}

        //public static void DelExtractor()
        //{
        //    string tmpZipper = Path.Combine(Application.StartupPath, ct.EXTRACTOR_FILENAME);
        //    try
        //    {
        //        if (File.Exists(tmpZipper) == true)
        //            File.Delete(tmpZipper);
        //    }
        //    catch { }
        //}

        //public static bool SetRegValue(string key, string val)
        //{
        //    try
        //    {
        //        Microsoft.Win32.Registry.SetValue(string.Format(@"HKEY_CURRENT_USER\SOFTWARE\{0}", ct.APP_NAME), key, val);
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        //public static string GetRegValue(string key, string dft)
        //{
        //    try
        //    {
        //        string str = Microsoft.Win32.Registry.GetValue(string.Format(@"HKEY_CURRENT_USER\SOFTWARE\{0}", ct.APP_NAME), key, dft).ToString();
        //        if (!string.IsNullOrEmpty(str))
        //            return str;
        //    }
        //    catch { }

        //    return dft;
        //}

        public static string MakeMeSecure(string value)
        {
            if (string.IsNullOrEmpty(value)) return value;

            string myResult;
            myResult = value + "";
            myResult = myResult.Replace("'", "`");
            myResult = myResult.Trim();
            return myResult;
        }

        public static string Date2SQL(DateTime xdate)
        {
            try
            {
                CultureInfo enCult = new CultureInfo("en-US");
                return string.Format("'{0} {1} {2} {3}:{4}:{5}'", enCult.DateTimeFormat.GetMonthName(xdate.Month), xdate.Day, xdate.Year, xdate.Hour, xdate.Minute, xdate.Second);
            }
            catch
            {
                return "NULL";
            }
        }

        public static string Date2Oracle(DateTime xdate)
        {
            try
            {
                CultureInfo enCult = new CultureInfo("en-US");
                return string.Format("TO_DATE('{0:yyyy/MM/dd HH:mm:ss}','yyyy/mm/dd hh24:mi:ss')", xdate);
            }
            catch
            {
                return "NULL";
            }
        }

        public static string DateVN2SQL(string xdate)
        {
            try
            {
                DateTime vnDate = DateTime.Parse(xdate, new CultureInfo("vi-VN"));
                return Date2SQL(vnDate);
            }
            catch
            {
                if (string.IsNullOrEmpty(xdate))
                    return "NULL";
                else
                    return DateEN2SQL(xdate);
            }
        }

        public static string DateEN2SQL(string xdate)
        {
            try
            {
                DateTime enDate = DateTime.Parse(xdate, new CultureInfo("en-US"));
                return Date2SQL(enDate);//enDate.ToString();
            }
            catch
            {
                //clsMx.Show("Hàm DateEN2SQL phát hiện dữ liệu kiểu ngày '" + xdate + "' chuyển đổi không phù hợp!", eAlertType.CanhBao);
                return "NULL";
            }
        }

        public static string DateUS2VN(DateTime xdate)
        {
            try
            {
                return xdate.ToString("dd/MM/yyyy HH:mm:ss");
            }
            catch
            {
                return "";
            }
        }

        public static string DateUS2VN(string xdate)
        {
            try
            {
                DateTime vnDate = DateTime.Parse(xdate);
                return vnDate.ToString("dd/MM/yyyy");
            }
            catch
            {
                return "";
            }
        }

        public static string DateUS2VN(string xdate, string strFormat)
        {
            try
            {
                DateTime vnDate = DateTime.Parse(xdate);
                return vnDate.ToString(strFormat);
            }
            catch
            {
                return "";
            }
        }

        public static string DateVN2US(string xdate)
        {
            try
            {
                DateTime vnDate = DateTime.Parse(xdate, new CultureInfo("vi-VN"));
                CultureInfo enCult = new CultureInfo("en-US");
                //Xử lý nếu nhập năm bị lỗi
                if (vnDate.Year < 1900 || vnDate.Year > 9999)
                {
                    return "NULL";
                }
                else
                {
                    return string.Format("{0} {1} {2} {3}:{4}:{5}", enCult.DateTimeFormat.GetMonthName(vnDate.Month), vnDate.Day, vnDate.Year, vnDate.Hour, vnDate.Minute, vnDate.Second);
                }
            }
            catch
            {
                return "NULL";
            }
        }

        public static string DateVN2US(string xdate, string strFormat)
        {
            try
            {
                DateTime vnDate = DateTime.Parse(xdate, new CultureInfo("vi-VN"));
                CultureInfo enCult = new CultureInfo("en-US");
                //Xử lý nếu nhập năm bị lỗi
                if (vnDate.Year < 1900 || vnDate.Year > 9999)
                {
                    return string.Empty;
                }
                else
                {
                    return vnDate.ToString(strFormat);
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        public static DateTime? StringVNToDate(string xdate)
        {
            try
            {
                DateTime vnDate;
                if (!DateTime.TryParse(xdate, new CultureInfo("vi-VN"), DateTimeStyles.NoCurrentDateDefault, out vnDate))
                {
                    if (!DateTime.TryParse(xdate, new CultureInfo("en-US"), DateTimeStyles.NoCurrentDateDefault, out vnDate))
                        return null;
                }

                return vnDate;
            }
            catch
            {
                return null;
            }
        }

        public static DateTime? StringUSToDate(string xdate)
        {
            try
            {
                DateTime usDate;
                if (!DateTime.TryParse(xdate, new CultureInfo("en-US"), DateTimeStyles.NoCurrentDateDefault, out usDate))
                {
                    if (!DateTime.TryParse(xdate, new CultureInfo("vi-VN"), DateTimeStyles.NoCurrentDateDefault, out usDate))
                        return null;
                }

                return usDate;
            }
            catch
            {
                return null;
            }
        }

        public static string IsDBNULL(object source, int typ)
        {
            if (source != null)
            {
                if (source is DateTime || source is DateTime?)
                {
                    return Date2SQL((DateTime)source);
                }
                else
                {
                    switch (typ)
                    {
                        case 1:
                            return string.Format("'{0}'", source);
                        case 2:
                            return Date2SQL((DateTime)source);
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

        public static object ConvertData(object source, Type typ)
        {
            return ConvertData(source, typ, new CultureInfo("en-US"));
        }

        public static object ConvertData(object source, Type typ, CultureInfo cul)
        {
            if (source == null || source == DBNull.Value)
                return null;
            switch (typ.Name.ToLower())
            {
                case "int?":
                case "int":
                case "int32":
                case "int16":
                case "int32?":
                case "int16?":
                    return int.Parse(source.ToString());
                case "double":
                case "double?":
                    return double.Parse(source.ToString());
                case "decimal":
                case "decimal?":
                    return decimal.Parse(source.ToString());
                case "bool":
                case "bool?":
                case "boolean":
                case "boolean?":
                    return bool.Parse(source.ToString());
                case "datetime":
                case "datetime?":
                    //return DateTime.Parse(source.ToString(), cul);
                    return source;
                case "char":
                case "char?":
                    return char.Parse(source.ToString());
                case "string":
                    return source.ToString();
                case "object":
                    return source;
                case "byte[]":
                    return (byte[])source;
                case "nullable`1":
                    typ = Nullable.GetUnderlyingType(typ);
                    return ConvertData(source, typ);
                default:
                    switch (typ.BaseType.Name.ToLower())
                    {
                        case "enum":
                            return Enum.Parse(typ, source.ToString(), true);
                    }
                    break;
            }
            return source;
        }

        public static object ConvertData2SQLType(object source, Type typ)
        {
            if (source == null || string.IsNullOrEmpty(source.ToString()))
                return DBNull.Value;

            return ConvertData(source, typ, new CultureInfo("vi-VN"));
        }

        public static void DataRow2Object(DataRow sourde, object objBO)
        {
            if ((sourde != null))
            {
                foreach (DataColumn colum in sourde.Table.Columns)
                {
                    PropertyInfo proInfor = objBO.GetType().GetProperty(colum.ColumnName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                    if (proInfor != null)
                    {
                        if (proInfor.PropertyType is System.Type && proInfor.CanWrite)
                        {
                            proInfor.SetValue(objBO, ConvertData(sourde[proInfor.Name], proInfor.PropertyType), null);
                        }
                    }
                }
            }
        }

        public static void DataRow2DataRow(DataRow source, DataRow dr)
        {
            if ((dr != null))
            {
                foreach (DataColumn colum in source.Table.Columns)
                {
                    if (dr.Table.Columns.Contains(colum.ColumnName))
                        dr[colum.ColumnName] = source[colum.ColumnName];
                }
            }
        }

        public static byte[] StringToByteArray(string source)
        {
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            return enc.GetBytes(source);
        }

        public static string ByteArrayToString(byte[] source)
        {
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            return enc.GetString(source);
        }

        public static bool SpecialInside(string content, string mySpecial)
        {
            bool myStatus = false;
            for (int i = 0; i < content.Length; i++)
            {
                if (mySpecial.Contains(content.Substring(i, 1)))
                {
                    myStatus = true;
                    break;
                }
            }

            if (myStatus)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool SpaceInside(string content)
        {
            return content.Trim().Contains(" ");
        }

        public static string RTFErrorSkip(string inputx, bool inx)
        {
            string resultx;
            //Input 
            if (inx)
            {
                resultx = inputx.Replace("'", "@+@");
            }
            //output 
            else
            {
                resultx = inputx.Replace("@+@", "'");
            }
            return resultx;
        }

        public static string MakeRealHS(string InputHS)
        {
            if (InputHS.Length > 4)
            {
                switch (InputHS.Length)
                {
                    case 5:
                        InputHS = InputHS.Substring(0, 4) + "." + InputHS.Substring(5, InputHS.Length - 4);
                        break;
                    case 6:
                        InputHS = InputHS.Substring(0, 4) + "." + InputHS.Substring(5, InputHS.Length - 4);
                        break;
                    case 7:
                        InputHS = InputHS.Substring(0, 4) + "." + InputHS.Substring(4, 2) + "." + InputHS.Substring(7, InputHS.Length - 6);
                        break;
                    case 8:
                        InputHS = InputHS.Substring(0, 4) + "." + InputHS.Substring(4, 2) + "." + InputHS.Substring(7, InputHS.Length - 6);
                        break;
                    case 9:
                        InputHS = InputHS.Substring(0, 4) + "." + InputHS.Substring(4, 2) + "." + InputHS.Substring(7, InputHS.Length - 6);
                        break;
                    case 10:
                        InputHS = InputHS.Substring(0, 4) + "." + InputHS.Substring(4, 2) + "." + InputHS.Substring(7, InputHS.Length - 6);
                        break;
                }
                return InputHS;
            }
            else
            {
                if (InputHS.Length == 0)
                {
                    InputHS = "không xác định được cụ thể";
                }
                return InputHS;
            }
        }

        public static string MakeFileName(string input)
        {
            input = input.Replace("\\", "");
            input = input.Replace("/", "");
            input = input.Replace(":", "");
            input = input.Replace("*", "");
            input = input.Replace("?", "");
            input = input.Replace(Convert.ToChar(34).ToString(), "");
            input = input.Replace("<", "");
            input = input.Replace(">", "");
            input = input.Replace("|", "");
            return input + "_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + "_" + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second;
        }

        public static int GetColName(DataTable objDT, string NameToFind)
        {
            int i = -1;
            foreach (DataColumn myCol in objDT.Columns)
            {
                i += 1;
                if (myCol.ColumnName == NameToFind)
                {
                    return i;
                }
            }
            return 0;
        }

        //Hàm xử lý cắt ra đoạn thông báo lỗi liên kết: giá trị VIP
        public static string GetExactError(string ErrMess)
        {
            string myExErr = "Bạn phải xoá hết các dữ liệu phát sinh từ dữ liệu này trước!";
            try
            {
                int mot = ErrMess.IndexOf("'");
                int hai = ErrMess.IndexOf("'", mot + 1);
                myExErr = ErrMess.Substring(mot + 1, hai - mot - 1);
                myExErr = clsDalOLE.GetFValue("SELECT CONST_MESS FROM SMESSAGE WHERE CONST_NAME='" + myExErr + "'");
            }
            catch { }
            return myExErr;

        }

        public static string CatSo0(decimal xVal)
        {
            return CatSo0(xVal.ToString());
        }

        public static string CatSo0(double xVal)
        {
            return string.Format("{0:0.###############}", xVal);
        }

        public static string CatSo0(decimal? xVal)
        {
            return xVal.HasValue ?
                string.Format("{0:0.###############}", xVal.Value) : string.Empty;
        }

        public static string CatSo0(double? xVal)
        {
            return xVal.HasValue ?
                string.Format("{0:0.###############}", xVal.Value) : string.Empty;
        }

        public static string CatSo0(string xVal)
        {
            double dValue = 0;
            if (double.TryParse(xVal, out dValue))
            {
                return string.Format("{0:0.###############}", dValue);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string ChongLoiNull(string xVal)
        {
            if (String.IsNullOrEmpty(xVal))
            {
                return "0";
            }
            else
            {
                return xVal;
            }
        }

        //public static string U2T_NOXML(string xINPUT)
        //{
        //    if (string.IsNullOrEmpty(xINPUT)) return xINPUT;

        //    string xOUPUT = U2T(xINPUT);
        //    //Xử lý chống lỗi XML luôn
        //    return XMLAnTiError(xOUPUT);
        //}

        //public static string U2T(string xINPUT)
        //{
        //    if (string.IsNullOrEmpty(xINPUT)) return xINPUT;

        //    string xOUPUT = xINPUT;
        //    ConvertDB.ConvertFont cv = new ConvertDB.ConvertFont();

        //    xOUPUT = xOUPUT.Normalize(NormalizationForm.FormD);
        //    xOUPUT = xOUPUT.Normalize(NormalizationForm.FormC);
        //    cv.Convert(ref xOUPUT, ConvertDB.FontIndex.iNotKnown, ConvertDB.FontIndex.iTCV);
        //    return xOUPUT;

        //}

        //public static string UC2U(string xINPUT)
        //{
        //    if (string.IsNullOrEmpty(xINPUT)) return xINPUT;

        //    string xOUPUT = xINPUT;
        //    ConvertDB.ConvertFont cv = new ConvertDB.ConvertFont();
        //    ConvertDB.FontIndex fid = ConvertDB.FontIndex.iUNI;
        //    cv.isVietnamese(xOUPUT, ref fid);
        //    if (fid != ConvertDB.FontIndex.iUNI && fid != ConvertDB.FontIndex.iUTH)
        //        xOUPUT = T2U(xOUPUT);

        //    xOUPUT = xOUPUT.Normalize(NormalizationForm.FormD);
        //    xOUPUT = xOUPUT.Normalize(NormalizationForm.FormC);
        //    return xOUPUT;

        //}

        //public static void U2TObject(object xINPUT)
        //{
        //    try
        //    {
        //        if (xINPUT != null)
        //        {
        //            PropertyInfo[] proSourceInfors = xINPUT.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
        //            foreach (PropertyInfo proSourceInfor in proSourceInfors)
        //            {
        //                if (proSourceInfor.CanRead && proSourceInfor.CanWrite
        //                    && proSourceInfor.PropertyType.Name.Equals("string", StringComparison.CurrentCultureIgnoreCase))
        //                {
        //                    try
        //                    {
        //                        proSourceInfor.SetValue(xINPUT, MakeMeSecure(U2T(proSourceInfor.GetValue(xINPUT, null) as string)), null);
        //                    }
        //                    catch { }
        //                }
        //            }
        //        }
        //    }
        //    catch { }
        //}

        //public static void U2TListObject<T>(List<T> list)
        //{
        //    try
        //    {
        //        if (list != null)
        //            foreach (T xINPUT in list)
        //            {
        //                U2TObject(xINPUT);
        //            }
        //    }
        //    catch { }
        //}

        //public static string T2U(string xINPUT)
        //{
        //    if (string.IsNullOrEmpty(xINPUT)) return xINPUT;

        //    string xOUPUT = xINPUT;
        //    ConvertDB.ConvertFont cv = new ConvertDB.ConvertFont();
        //    cv.Convert(ref xOUPUT, ConvertDB.FontIndex.iNotKnown, ConvertDB.FontIndex.iUNI);
        //    return xOUPUT;

        //}

        //public static void T2UObject(object xINPUT)
        //{
        //    try
        //    {
        //        if (xINPUT != null)
        //        {
        //            PropertyInfo[] proSourceInfors = xINPUT.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
        //            foreach (PropertyInfo proSourceInfor in proSourceInfors)
        //            {
        //                if (proSourceInfor.CanRead && proSourceInfor.CanWrite
        //                    && proSourceInfor.PropertyType.Name.Equals("string", StringComparison.CurrentCultureIgnoreCase))
        //                {
        //                    try
        //                    {
        //                        proSourceInfor.SetValue(xINPUT, MakeMeSecure(T2U(proSourceInfor.GetValue(xINPUT, null) as string)), null);
        //                    }
        //                    catch { }
        //                }
        //            }
        //        }
        //    }
        //    catch { }
        //}

        //public static void T2UListObject<T>(List<T> list)
        //{
        //    try
        //    {
        //        if (list != null)
        //            foreach (T xINPUT in list)
        //            {
        //                T2UObject(xINPUT);
        //            }
        //    }
        //    catch { }
        //}

        //public static string VN2UN(string xINPUT)
        //{
        //    if (string.IsNullOrEmpty(xINPUT)) return xINPUT;
        //    ConvertDB.ConvertFont cv = new ConvertDB.ConvertFont();
        //    string[] strs = xINPUT.Split(' ');
        //    StringBuilder xOUPUT = new StringBuilder();
        //    if (strs.Length > 0)
        //    {
        //        for (int i = 0; i < strs.Length; i++)
        //        {
        //            string x = strs[i];
        //            if (!string.IsNullOrEmpty(x))
        //                cv.Convert(ref x, ConvertDB.FontIndex.iNotKnown, ConvertDB.FontIndex.iUNI);
        //            if (i == 0)
        //                xOUPUT.Append(x);
        //            else
        //                xOUPUT.AppendFormat(" {0}", x);

        //        }
        //    }
        //    return xOUPUT.ToString();
        //}

        //public static void VN2UNObject(object xINPUT)
        //{
        //    try
        //    {
        //        if (xINPUT != null)
        //        {
        //            PropertyInfo[] proSourceInfors = xINPUT.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
        //            foreach (PropertyInfo proSourceInfor in proSourceInfors)
        //            {
        //                if (proSourceInfor.CanRead && proSourceInfor.CanWrite
        //                    && proSourceInfor.PropertyType.Name.Equals("string", StringComparison.CurrentCultureIgnoreCase))
        //                {
        //                    try
        //                    {
        //                        proSourceInfor.SetValue(xINPUT, MakeMeSecure(VN2UN(proSourceInfor.GetValue(xINPUT, null) as string)), null);
        //                    }
        //                    catch { }
        //                }
        //            }
        //        }
        //    }
        //    catch { }
        //}

        //public static void VN2UNListObject<T>(List<T> list)
        //{
        //    try
        //    {
        //        if (list != null)
        //            foreach (T xINPUT in list)
        //            {
        //                VN2UNObject(xINPUT);
        //            }
        //    }
        //    catch { }
        //}

        public static string XMLAnTiError(string xINPUT)
        {
            if (!string.IsNullOrEmpty(xINPUT))
            {
                xINPUT = System.Security.SecurityElement.Escape(xINPUT);
            }
            return xINPUT;
        }

        public static void XMLAnTiError4Object(object xINPUT)
        {
            try
            {
                if (xINPUT != null)
                {
                    PropertyInfo[] proSourceInfors = xINPUT.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                    foreach (PropertyInfo proSourceInfor in proSourceInfors)
                    {
                        if (proSourceInfor.CanRead && proSourceInfor.CanWrite
                            && proSourceInfor.PropertyType.Name.Equals("string", StringComparison.CurrentCultureIgnoreCase))
                        {
                            try
                            {
                                proSourceInfor.SetValue(xINPUT, XMLAnTiError(proSourceInfor.GetValue(xINPUT, null) as string), null);
                            }
                            catch { }
                        }
                    }
                }
            }
            catch { }
        }

        public static void XMLAnTiError4ListObject<T>(List<T> list)
        {
            try
            {
                if (list != null)
                    foreach (T xINPUT in list)
                    {
                        XMLAnTiError4Object(xINPUT);
                    }
            }
            catch { }
        }

        public static string AnTiError(string ID)
        {
            if (String.IsNullOrEmpty(ID))
            {
                return "0";//chong loi
            }
            else
            {
                return ID;
            }
        }

        public static string GetChecksum(string filePath)
        {
            if (File.Exists(filePath))
            {
                byte[] data;
                data = File.ReadAllBytes(filePath);
                return GetChecksum(data);
            }

            return string.Empty;
        }

        public static string GetChecksum(byte[] data)
        {
            string chkSum = string.Empty;
            if (data != null && data.Length > 0)
            {
                MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
                byte[] btys = md5Hasher.ComputeHash(data);
                for (int i = 0; i <= btys.Length - 1; i++)
                {
                    chkSum += btys[i];
                }
            }
            return chkSum;
        }

        public static string AddSpace(string source)
        {
            StringBuilder strRturn = new StringBuilder();
            if (!string.IsNullOrEmpty(source))
            {
                source = source.Replace(" ", "");
                for (int i = 0; i < source.Length; i++)
                {
                    if (i == 0)
                        strRturn.Append(source[i]);
                    else
                        strRturn.AppendFormat(" {0}", source[i]);
                }
            }

            return strRturn.ToString();
        }

        public static string ConvertToBase64(string strSource)
        {
            Byte[] arrEncodetext = Encoding.Unicode.GetBytes(strSource);
            return Convert.ToBase64String(arrEncodetext);
        }

        public static string ConvertFromBase64(string strSource)
        {
            Byte[] arrPlaintext = Convert.FromBase64String(strSource);
            return Encoding.Unicode.GetString(arrPlaintext);
        }

        #region "Say Money"

        public static string SayMoney(string xTIEN)
        {
            int j = 0;//j=group(3)
            int k = 0;//1=nghin;2=trieu;3=ty;4=nghin;5=trieu;6=ty
            string xGroup = "";
            string sResult = "";
            bool xNot0 = false;//khóa kiểm tra xem đã từng có dãy nào khác 0 hay chưa
            for (int i = xTIEN.Length - 1; i >= 0; i--)
            {
                j++;
                xGroup = xTIEN.Substring(i, 1) + xGroup;
                if (j == 3)
                {
                    k++;
                    //Nếu nhóm hiện tại toàn 0 thì bỏ qua, ngược lại thì đọc
                    if (xGroup != "000")
                    {
                        if (k == 1) xNot0 = true;//Nếu ngay nhóm đầu tiên mà đã có giá trị khác thì đọc luôn lẻ
                        sResult = Say3Number(xGroup, xNot0) + SayGroup(k) + sResult;
                        xNot0 = true;
                    }
                    else //Nếu đến hàng tỷ thì ko quan tâm có 000 hay không cũng phải thêm chữ TỶ vào
                    {
                        if (k == 4)
                        {
                            sResult = SayGroup(k) + sResult;
                        }
                    }
                    xGroup = "";
                    j = 0;
                }
            }
            if (!String.IsNullOrEmpty(xGroup))
            {
                sResult = Say3Number(xGroup, xNot0) + SayGroup(++k) + sResult;
            }
            string xFirstCase = sResult.Substring(0, 1).ToUpper();
            sResult = xFirstCase + sResult.Substring(1);
            return sResult;
        }

        public static string SayGroup(int k)
        {
            string sResult = "";
            switch (k)
            {
                case 1: sResult = ""; break;
                case 2: sResult = "nghìn "; break;
                case 3: sResult = "triệu "; break;
                case 4: sResult = "tỷ "; break;
                case 5: sResult = "nghìn "; break;
                case 6: sResult = "triệu "; break;
                case 7: sResult = "tỷ "; break;
                case 8: sResult = "nghìn "; break;
                case 9: sResult = "triệu "; break;
                case 10: sResult = "tỷ "; break;
            }
            return sResult;
        }

        public static string Say3Number(string xTIEN, bool xNot0)
        {
            string sResult = "";
            string xChuc = "";
            if (xTIEN.Length == 3)
            {
                string xTram = Say1Number(xTIEN.Substring(0, 1)) + "trăm ";
                if (xNot0)
                {
                    xChuc = Say1Number(xTIEN.Substring(1, 1)) + "mươi "; if (xChuc == "không mươi ") xChuc = "lẻ "; if (xChuc == "một mươi ") xChuc = "mười ";
                }
                else
                {
                    xChuc = Say1Number(xTIEN.Substring(1, 1)) + "mươi "; if (xChuc == "không mươi ") xChuc = ""; if (xChuc == "một mươi ") xChuc = "mười ";
                }
                string xDonv = Say1Number(xTIEN.Substring(2, 1)); if (xDonv == "một " && xChuc != "lẻ ") xDonv = "mốt "; if (xDonv == "năm " && xChuc != "lẻ ") xDonv = "lăm "; if (xDonv == "bốn " && xChuc != "mười ") xDonv = "tư "; if (xDonv == "không ") { xDonv = ""; if (xChuc == "lẻ ") xChuc = ""; }
                sResult = xTram + xChuc + xDonv;
            }
            else if (xTIEN.Length == 2)
            {
                xChuc = Say1Number(xTIEN.Substring(0, 1)) + "mươi "; if (xChuc == "không mươi ") xChuc = ""; if (xChuc == "một mươi ") xChuc = "mười ";
                string xDonv = Say1Number(xTIEN.Substring(1, 1)); if (xDonv == "một ") xDonv = "mốt "; if (xDonv == "năm ") xDonv = "lăm "; if (xDonv == "bốn " && xChuc != "mười ") xDonv = "tư "; if (xDonv == "không ") xDonv = "";
                sResult = xChuc + xDonv;
            }
            else
            {
                string xDonv = Say1Number(xTIEN.Substring(0, 1)); if (xDonv == "không ") xDonv = "";
                sResult = xDonv;
            }
            return sResult;
        }

        public static string Say1Number(string xNum)
        {
            string sResult = "";
            switch (xNum)
            {
                case "0": sResult = "không "; break;
                case "1": sResult = "một "; break;
                case "2": sResult = "hai "; break;
                case "3": sResult = "ba "; break;
                case "4": sResult = "bốn "; break;
                case "5": sResult = "năm "; break;
                case "6": sResult = "sáu "; break;
                case "7": sResult = "bảy "; break;
                case "8": sResult = "tám "; break;
                case "9": sResult = "chín "; break;
                default: sResult = "lỗi số "; break;
            }
            return sResult;
        }
        #endregion

        //public static void ExtractResources()
        //{
        //    //Giai phong Access db file            
        //    clsAll.ExtractFile(st.AccessDBDir, ct.ACCESS_DB_FILENAME);

        //    //Giai phong DotNetBarcode.dll
        //    clsAll.ExtractFile(Application.StartupPath, "DotNetBarcode.dll");

        //    //"System.Data.SQLite.dll"
        //    clsAll.ExtractFile(Application.StartupPath, "System.Data.SQLite.dll");

        //    //"Interop.Excel.dll"
        //    clsAll.ExtractFile(Application.StartupPath, "Interop.Excel.dll");

        //    //"vnConvert.dll"
        //    clsAll.ExtractFile(Application.StartupPath, "vnConvert.dll");

        //    clsAll.DelExtractor();
        //}

        //public static void ExtractJobResources()
        //{
        //    clsAll.ExtractJobFile(Path.Combine(Application.StartupPath, @"App_LocalResources\JobTrees"), "jobtree.xml");

        //    clsAll.ExtractJobFile(Path.Combine(Application.StartupPath, @"App_LocalResources\Template\Send\IDC"), "IDC00000D0.xml");
                        
        //    clsAll.ExtractJobFile(Path.Combine(Application.StartupPath, @"App_LocalResources\Help\gym_err"), "SYS_err.xml");
            
        //    clsAll.DelExtractor();
        //}

        public static bool ConvertToBool(string val)
        {
            switch (val.ToLower())
            {
                case "1":
                case "-1":
                case "yes":
                case "true":
                    return true;
                default:
                    return false;
            }
        }

        public static int ConvertToBit(object val)
        {
            switch (val.ToString().ToLower())
            {
                case "1":
                case "-1":
                case "yes":
                case "true":
                    return 1;
                default:
                    return 0;
            }
        }

        public static byte[] Compress2Byte(string data)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            using (GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true))
                zip.Write(buffer, 0, buffer.Length);
            ms.Position = 0;
            MemoryStream outStream = new MemoryStream();
            byte[] compressed = new byte[ms.Length];
            ms.Read(compressed, 0, compressed.Length);
            byte[] gzBuffer = new byte[compressed.Length + 4];
            System.Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length);
            System.Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gzBuffer, 0, 4);
            return gzBuffer;
        }

        public static string Compress(string data)
        {
            return Convert.ToBase64String(Compress2Byte(data));
        }

        public static string Decompress(string cmpData)
        {
            return Decompress(Convert.FromBase64String(cmpData));
        }

        public static string Decompress(byte[] cmpData)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                int msgLength = BitConverter.ToInt32(cmpData, 0);
                ms.Write(cmpData, 4, cmpData.Length - 4);
                byte[] buffer = new byte[msgLength];
                ms.Position = 0;
                using (GZipStream zip = new GZipStream(ms, CompressionMode.Decompress))
                    zip.Read(buffer, 0, buffer.Length);
                return Encoding.UTF8.GetString(buffer);
            }
        }

        public static string EncryptByPublicKey(string xmlKey, string inputString, int dwKeySize)
        {
            // TODO: Add Proper Exception Handlers
            RSACryptoServiceProvider rsaCryptoServiceProvider = new RSACryptoServiceProvider(dwKeySize);
            rsaCryptoServiceProvider.FromXmlString(xmlKey);
            int keySize = dwKeySize / 8;
            byte[] bytes = Compress2Byte(inputString);
            // The hash function in use by the .NET RSACryptoServiceProvider here is SHA1
            // int maxLength = ( keySize ) - 2 - ( 2 * SHA1.Create().ComputeHash( rawBytes ).Length );
            int maxLength = keySize - 42;
            int dataLength = bytes.Length;
            int iterations = dataLength / maxLength;
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i <= iterations; i++)
            {
                byte[] tempBytes = new byte[(dataLength - maxLength * i > maxLength) ? maxLength : dataLength - maxLength * i];
                Buffer.BlockCopy(bytes, maxLength * i, tempBytes, 0, tempBytes.Length);
                byte[] encryptedBytes = rsaCryptoServiceProvider.Encrypt(tempBytes, true);
                // Be aware the RSACryptoServiceProvider reverses the order of encrypted bytes after encryption and before decryption.
                // If you do not require compatibility with Microsoft Cryptographic API (CAPI) and/or other vendors.
                // Comment out the next line and the corresponding one in the DecryptString function.
                Array.Reverse(encryptedBytes);
                // Why convert to base 64?
                // Because it is the largest power-of-two base printable using only ASCII characters
                stringBuilder.Append(Convert.ToBase64String(encryptedBytes));
            }
            return stringBuilder.ToString();
        }

        public static string DecryptByPrivateKey(string xmlKey, string inputString, int dwKeySize)
        {
            // TODO: Add Proper Exception Handlers
            RSACryptoServiceProvider rsaCryptoServiceProvider = new RSACryptoServiceProvider(dwKeySize);
            rsaCryptoServiceProvider.FromXmlString(xmlKey);
            int base64BlockSize = ((dwKeySize / 8) % 3 != 0) ? (((dwKeySize / 8) / 3) * 4) + 4 : ((dwKeySize / 8) / 3) * 4;
            int iterations = inputString.Length / base64BlockSize;
            ArrayList arrayList = new ArrayList();
            for (int i = 0; i < iterations; i++)
            {
                byte[] encryptedBytes = Convert.FromBase64String(inputString.Substring(base64BlockSize * i, base64BlockSize));
                // Be aware the RSACryptoServiceProvider reverses the order of encrypted bytes after encryption and before decryption.
                // If you do not require compatibility with Microsoft Cryptographic API (CAPI) and/or other vendors.
                // Comment out the next line and the corresponding one in the EncryptString function.
                Array.Reverse(encryptedBytes);
                arrayList.AddRange(rsaCryptoServiceProvider.Decrypt(encryptedBytes, true));
            }
            return Decompress(arrayList.ToArray(typeof(byte)) as byte[]);
        }/// <summary>

        /// Cách sử dụng:
        ///    string passPhase = "Hứa Kiến Giang";
        ///    string saltValue = "Bùi Thu Hương";
        ///    string ivt = "HứMạTyển";//phải là 16 byte (get theo unicode nen 1 ky tu = 2 byte)
        ///    string s1 = EncryptRijndael(content, passPhase, saltValue, "SHA1", 5, ivt, 256);
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="passPhrase"></param>
        /// <param name="saltValue"></param>
        /// <param name="hashAlgorithm"></param>
        /// <param name="passwordIterations"></param>
        /// <param name="initVector"></param>
        /// <param name="keySize"></param>
        /// <returns></returns>
        public static string EncryptRijndael(string plainText)
        {
            string passPhrase = "d7d4ec84ớ6cc04ị7e0áa40aóe";
            string saltValue = "fbbvãi96fbaốa7a";
            string initVector = "ứKiếnGig"; //16 byte
            int passwordIterations = 5;
            int keySize = 256;

            byte[] initVectorBytes = Encoding.Unicode.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.Unicode.GetBytes(saltValue);

            byte[] plainTextBytes = Encoding.Unicode.GetBytes(plainText);

            Rfc2898DeriveBytes password = new Rfc2898DeriveBytes(
                                                            passPhrase,
                                                            saltValueBytes,
                                                            passwordIterations);
            byte[] keyBytes = password.GetBytes(keySize / 8);

            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(
                                                             keyBytes,
                                                             initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                         encryptor,
                                                         CryptoStreamMode.Write);
            // Start encrypting.
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            // Finish encrypting.
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            string cipherText = Convert.ToBase64String(cipherTextBytes);
            return cipherText;
        }

        /// <summary>
        /// Cách sử dụng
        ///   Dim passPhrase As String = "Hứa Kiến Giang"
        ///   Dim saltValue As String = "Bùi Thu Hương"
        ///   Dim initVector As String = "HứMạTyển"
        ///   Dim passwordIterations As Integer = 5
        ///   Dim keySize As Integer = 256
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="passPhrase"></param>
        /// <param name="saltValue"></param>
        /// <param name="hashAlgorithm"></param>
        /// <param name="passwordIterations"></param>
        /// <param name="initVector"></param>
        /// <param name="keySize"></param>
        /// <returns></returns>
        public static string DecryptRijndael(string cipherText)
        {

            string passPhrase = "d7d4ec84ớ6cc04ị7e0áa40aóe";
            string saltValue = "fbbvãi96fbaốa7a";
            string initVector = "ứKiếnGig"; //16 byte
            int passwordIterations = 5;
            int keySize = 256;

            byte[] initVectorBytes = Encoding.Unicode.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.Unicode.GetBytes(saltValue);
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            Rfc2898DeriveBytes password = new Rfc2898DeriveBytes(
                                                            passPhrase,
                                                            saltValueBytes,
                                                            passwordIterations);
            byte[] keyBytes = password.GetBytes(keySize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(
                                                             keyBytes,
                                                             initVectorBytes);
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                          decryptor,
                                                          CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes,
                                                       0,
                                                       plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            string plainText = Encoding.Unicode.GetString(plainTextBytes,
                                                       0,
                                                       decryptedByteCount);
            return plainText;
        }

        public static string GetExcelColumnLabel(int STT)
        {
            STT = STT % 26;
            switch (STT)
            {
                case 1:
                    return "A";
                case 2:
                    return "B";
                case 3:
                    return "C";
                case 4:
                    return "D";
                case 5:
                    return "E";
                case 6:
                    return "F";
                case 7:
                    return "G";
                case 8:
                    return "H";
                case 9:
                    return "I";
                case 10:
                    return "J";
                case 11:
                    return "K";
                case 12:
                    return "L";
                case 13:
                    return "M";
                case 14:
                    return "N";
                case 15:
                    return "O";
                case 16:
                    return "P";
                case 17:
                    return "Q";
                case 18:
                    return "R";
                case 19:
                    return "S";
                case 20:
                    return "T";
                case 21:
                    return "U";
                case 22:
                    return "V";
                case 23:
                    return "W";
                case 24:
                    return "X";
                case 25:
                    return "Y";
                case 26:
                    return "Z";
            }
            return "Z";
        }
    }
}

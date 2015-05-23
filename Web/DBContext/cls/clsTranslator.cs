using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;

namespace t
{
    public class clsTranslator
    {
        public static object TranslateObject(Type typ, object value)
        {
            if (typ.IsSealed)
                return value;
            else
                return TranslateObject(typ, System.Activator.CreateInstance(typ), value);
        }

        public static object TranslateObject(Type typ, object objRtn, object value)
        {
            if (value == null) return null;
            if (objRtn == null)
                TranslateObject(typ, value);

            PropertyInfo[] proSourceInfors = value.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            foreach (PropertyInfo proSourceInfor in proSourceInfors)
            {
                if (proSourceInfor.CanRead)
                {
                    PropertyInfo proInfor = objRtn.GetType().GetProperty(proSourceInfor.Name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                    if (proInfor != null && proInfor.CanRead)
                    {
                        if (proInfor.CanWrite)
                        {
                            if (proInfor.PropertyType.IsSealed)
                            {
                                try
                                {
                                    proInfor.SetValue(objRtn, proSourceInfor.GetValue(value, null), null);
                                }
                                catch { }
                            }
                            else if (proInfor.PropertyType.IsGenericType || proInfor.PropertyType.IsArray)
                            {
                                proInfor.SetValue(objRtn, TranslateListObject(proInfor.PropertyType, proSourceInfor.GetValue(value, null)), null);
                            }
                            else
                            {
                                proInfor.SetValue(objRtn, TranslateObject(proInfor.PropertyType, proSourceInfor.GetValue(value, null)), null);
                            }
                        }
                    }
                }
            }

            return objRtn;
        }

        public static T TranslateObject<T>(object value)
        {
            return (T)TranslateObject(typeof(T), value);
        }

        public static List<T> TranslateListObject<T>(List<object> list)
        {
            return TranslateListObject(typeof(List<T>), list) as List<T>;
        }

        public static List<T> TranslateListObject<T>(IEnumerable list)
        {
            return TranslateListObject(typeof(List<T>), list) as List<T>;
        }

        public static object TranslateListObject(Type typ, object Source)
        {
            object list = null;
            if (typ.IsGenericType)
            {
                list = System.Activator.CreateInstance(typ);
            }
            else if (typ.IsArray)
            {
                list = new ArrayList();
            }

            return TranslateListObject(typ, list, Source);
        }

        public static object TranslateListObject(Type TType, object Source, object Destination)
        {
            if (Source == null) return null;
            if (Destination == null)
                return TranslateListObject(TType, Source);

            Type FType = Source.GetType();

            if (TType.IsGenericType || FType.BaseType.IsGenericType)
            {
                Type ItemType = TType.GetGenericArguments()[0];
                IList TList = Destination as IList;
                if (FType.IsGenericType || FType.BaseType.IsGenericType)
                {
                    if (FType.GetInterface("IList", true) != null)
                    {
                        foreach (object item in (Source as IList))
                        {
                            TList.Add(TranslateObject(ItemType, item));
                        }
                    }
                    else if (FType.GetInterface("IEnumerable", true) != null)
                    {
                        foreach (object item in (Source as IEnumerable))
                        {
                            TList.Add(TranslateObject(ItemType, item));
                        }
                    }
                }
                else if (FType.IsArray)
                {
                    foreach (object item in (Source as Array))
                    {
                        TList.Add(TranslateObject(ItemType, item));
                    }
                }

                return TList;
            }
            else if (FType.IsArray)
            {
                Type ItemType = TType.GetElementType();
                ArrayList TList = Destination as ArrayList;
                if (FType.IsGenericType || TType.BaseType.IsGenericType)
                {
                    if (FType.GetInterface("IList", true) != null)
                    {
                        foreach (object item in (Source as IList))
                        {
                            TList.Add(TranslateObject(ItemType, item));
                        }
                    }
                    else if (FType.GetInterface("IEnumerable", true) != null)
                    {
                        foreach (object item in (Source as IEnumerable))
                        {
                            TList.Add(TranslateObject(ItemType, item));
                        }
                    }
                }
                else if (FType.IsArray)
                {
                    Array FList = Source as Array;
                    foreach (object item in (Source as IList))
                    {
                        TList.Add(TranslateObject(ItemType, item));
                    }
                }

                return TList.ToArray(ItemType);
            }

            return null;
        }

        public static void CopyData(object source, object destination)
        {
            TranslateObject(destination.GetType(), source, destination);
        }
    }
}

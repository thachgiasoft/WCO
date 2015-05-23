using System;
using System.Collections.Generic;
using System.Text;

namespace t
{
    public class clsService
    {
        public static bool IsInternetAvailable()
        {
            try
            {
                //Call url 
                System.Uri url = new System.Uri("http://www.google.com.vn");
                System.Net.WebRequest req = default(System.Net.WebRequest);
                req = System.Net.WebRequest.Create(url);
                req.Timeout = 15000;//15s
                System.Net.WebResponse resp = default(System.Net.WebResponse);
                //------Proxy
                //if (st.fProxyOption == "2")//2-Use IE proxy
                //{
                //    System.Net.WebProxy pr = System.Net.WebProxy.GetDefaultProxy();
                //    pr.Credentials = System.Net.CredentialCache.DefaultCredentials;
                //    req.Proxy = pr;
                //}
                //else if (st.fProxyOption == "3")//3-Manual proxy
                //{
                //    string strProxyIP = st.fProxyIP;
                //    string strProxyPort = st.fProxyPort;
                //    string strProxyUser = st.fProxyUser;
                //    string strProxyPass =st.fProxyPass;
                //    if (!String.IsNullOrEmpty(strProxyIP))
                //    {
                //        System.Net.WebProxy pr = new System.Net.WebProxy(strProxyIP, Convert.ToInt32(strProxyPort));
                //        if (!string.IsNullOrEmpty(strProxyUser))
                //        {
                //            System.Net.NetworkCredential cr = new System.Net.NetworkCredential(strProxyUser, strProxyPass);
                //            pr.Credentials = cr;
                //        }
                //        req.Proxy = pr;
                //    }
                //}
                //else//1-No proxy (là 1 hoặc giá trị khác thì là no proxy
                //{
                //    //Do nothing for proxy
                //}
                //------End

                resp = req.GetResponse();
                resp.Close();
                req = null;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

/*
 * Created by SharpDevelop.
 * User: spocke
 * Date: 2007-11-23
 * Time: 13:23
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
//using System.Xml;

namespace mojoPortal.Web.Editor
{
    // 2013-10-09 the old implementation was no longer working, updated to a new api per this thread
    //http://www.tinymce.com/forum/viewtopic.php?id=30779
    // hopefully it will keep working

    public class GoogleSpellChecker : ISpellChecker
    {


        public class GoogleResponse
        {
            public GoogleResponseResult result { get; set; }
        }
        public class GoogleResponseResult
        {
            public GoogleResponseSpellingCheckResponse spellingCheckResponse { get; set; }
        }
        public class GoogleResponseSpellingCheckResponse
        {
            public List<GoogleResponseMisspelling> misspellings { get; set; }
        }
        public class GoogleResponseMisspelling
        {
            public int charStart { get; set; }
            public int charLength { get; set; }
            public List<GoogleResponseSuggestion> suggestions { get; set; }
        }

        public class GoogleMisspellingResult
        {
            public GoogleResponseMisspelledWord word { get; set; }
            //public int charLength { get; set; }
            public List<GoogleResponseSuggestion> suggestions { get; set; }

            public void ToJsonFragment(StringBuilder stringBuilder)
            {
                if (stringBuilder == null) { return; }
                if (word == null) { return ; }
                if (suggestions == null) { return ; }

                stringBuilder.Append("\"" + word.word + "\"");
                stringBuilder.Append(":[");
                string comma = string.Empty;
                foreach(GoogleResponseSuggestion s in suggestions)
                {
                    stringBuilder.Append(comma);
                    stringBuilder.Append("\"" + s.suggestion + "\"");
                    comma = ",";

                }

                stringBuilder.Append("]");
            }
        }
        public class GoogleResponseSuggestion
        {
            public string suggestion { get; set; }
        }
        public class GoogleResponseMisspelledWord
        {
            public string word { get; set; }
        }


        /// <summary>
        /// Description of GoogleSpellChecker.
        /// </summary>
        public GoogleSpellChecker()
        {
        }

        public string SpellCheck(string requestId, string lang, string[] words)
        {
            //ArrayList misspelledWords = new ArrayList();
            StringBuilder json = new StringBuilder();
            json.Append("{");
            json.Append("\"id\":\"" + requestId + "\",\"result\":{");

            string result, wordsStr;

            // Send request to google
            wordsStr = String.Join(" ", words);
            result = this.SendRequest(lang, wordsStr);


            // Parse json response
            var js = new JavaScriptSerializer();
            var resultData = js.Deserialize<GoogleResponse>(result);

            // Get list of misspelled words
            if (resultData.result != null && resultData.result.spellingCheckResponse != null)
            {
                string comma = string.Empty;
                foreach (GoogleResponseMisspelling misspelling in resultData.result.spellingCheckResponse.misspellings)
                {
                    json.Append(comma);
                    GoogleMisspellingResult resultItem = new GoogleMisspellingResult();
                    GoogleResponseMisspelledWord w = new GoogleResponseMisspelledWord();
                    w.word = wordsStr.Substring(misspelling.charStart, misspelling.charLength);
                    resultItem.word = w;
                    resultItem.suggestions = misspelling.suggestions;
                    //misspelledWords.Add(wordsStr.Substring(misspelling.charStart, misspelling.charLength));
                    //misspelledWords.Add(resultItem);
                    resultItem.ToJsonFragment(json);
                    comma = ",";
                }
            }

            json.Append("}}");

            return json.ToString();
            //return js.Serialize(misspelledWords);
            //return (string[])misspelledWords.ToArray(typeof(string));
        }


        /// <summary>
        /// example request
        ///  {
        /// "method" : "checkWords",
        /// "params" : [ "en-us", ["usa","cawboy", "apple"] ]
        /// }
        /// 
        /// example response
        /// {"id":null,"result":["cawboy"], "error":null}
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="words"></param>
        /// <returns></returns>
        public string[] CheckWords(string lang, string[] words)
        {
            ArrayList misspelledWords = new ArrayList();
            //XmlDocument doc = new XmlDocument();
            string result, wordsStr;

            // Send request to google
            wordsStr = String.Join(" ", words);
            result = this.SendRequest(lang, wordsStr);
            

            // Parse json response
            var js = new JavaScriptSerializer();
            var resultData = js.Deserialize<GoogleResponse>(result);

            // Get list of misspelled words
            if (resultData.result != null && resultData.result.spellingCheckResponse != null)
            {
                foreach (GoogleResponseMisspelling misspelling in resultData.result.spellingCheckResponse.misspellings)
                {
                    misspelledWords.Add(wordsStr.Substring(misspelling.charStart, misspelling.charLength));
                }
            }

            return (string[])misspelledWords.ToArray(typeof(string));

            
        }

        /// <summary>
        /// example request 
        /// {
        /// "method" : "getWordSuggestions",
        /// "params" : [ "en-us", "Guugli" ]
        ///  }
        ///  
        /// example response
        /// {"id":null,"result":["Google"],"error":null}
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        public string[] GetSuggestions(string lang, string word)
        {
            ArrayList suggestions = new ArrayList();
            //XmlDocument doc = new XmlDocument();
            string result;

            // Send request to google
            result = this.SendRequest(lang, word);

            // Parse json response
            var js = new JavaScriptSerializer();
            var resultData = js.Deserialize<GoogleResponse>(result);

            // Get list of suggestions
            if (resultData.result != null && resultData.result.spellingCheckResponse != null)
            {
                foreach (GoogleResponseMisspelling misspelling in resultData.result.spellingCheckResponse.misspellings)
                {
                    foreach (GoogleResponseSuggestion suggestion in misspelling.suggestions)
                    {
                        suggestions.Add(suggestion.suggestion);
                    }
                }
            }

            return (string[])suggestions.ToArray(typeof(string));
        }

        #region private

        private string SendRequest(string lang, string data)
        {
            string server = "www.googleapis.com";
            string port = "443";
            string path = "/rpc";
            string url = "https://" + server + ":" + port + path;
            var requestData = new Dictionary<string, object>();
            var requestParams = new Dictionary<string, object>();
            requestParams.Add("language", lang);
            requestParams.Add("text", data);
            requestParams.Add("key", "AIzaSyCLlKc60a3z7lo8deV-hAyDU7rHYgL4HZg");
            requestData.Add("method", "spelling.check");
            requestData.Add("apiVersion", "v2");
            requestData.Add("params", requestParams);
            var js = new JavaScriptSerializer();
            string json = js.Serialize(requestData);
            string outData;
            StreamReader resStream = null;
            HttpWebResponse res = null;
            Stream reqStream = null;

            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.KeepAlive = true;
                req.Method = "POST";
                req.ContentType = "application/json";
                req.ContentLength = json.Length;

                // Google-specific headers
                WebHeaderCollection reqHeaders = req.Headers;
                reqStream = req.GetRequestStream();

                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] jsonData = encoding.GetBytes(json);
                reqStream.Write(jsonData, 0, jsonData.Length);

                res = (HttpWebResponse)req.GetResponse();
                resStream = new StreamReader(res.GetResponseStream());
                outData = resStream.ReadToEnd();
            }
            finally
            {
                if (reqStream != null)
                    reqStream.Close();

                if (resStream != null)
                    resStream.Close();

                if (res != null)
                    res.Close();
            }

            return outData;
        }

        private string EntityEncode(string str)
        {
            StringBuilder buff = new StringBuilder();

            for (int i = 0; i < str.Length; i++)
            {
                char chr = str[i];

                if (chr > 127)
                {
                    buff.Append("&#");
                    buff.Append((int)chr);
                    buff.Append(';');
                }
                else
                    buff.Append(chr);
            }

            return buff.ToString();
        }

        #endregion
    }
    
    
    
    //old implementation below

    ///// <summary>
    ///// Uses the google public spell checker service.
    ///// </summary>
    //public class GoogleSpellChecker : ISpellChecker
    //{
        
    //    public GoogleSpellChecker()
    //    {
    //    }

    //    public string[] CheckWords(string lang, string[] words)
    //    {
    //        ArrayList misspelledWords = new ArrayList();
    //        XmlDocument doc = new XmlDocument();
    //        string result, wordsStr;

    //        // Send request to google
    //        wordsStr = String.Join(" ", words);
    //        result = this.SendRequest(lang, wordsStr);

    //        // Parse XML result
    //        doc.LoadXml(result);

    //        // Build misspelled word list
    //        foreach (XmlNode node in doc.SelectNodes("//c"))
    //        {
    //            XmlElement cElm = (XmlElement)node;

    //            misspelledWords.Add(wordsStr.Substring(Convert.ToInt32(cElm.GetAttribute("o")), Convert.ToInt32(cElm.GetAttribute("l"))));
    //        }

    //        return (string[])misspelledWords.ToArray(typeof(string));
    //    }

       
    //    public string[] GetSuggestions(string lang, string word)
    //    {
    //        ArrayList suggestions = new ArrayList();
    //        XmlDocument doc = new XmlDocument();
    //        string result;

    //        // Send request to google
    //        result = this.SendRequest(lang, word);

    //        // Parse XML result
    //        doc.LoadXml(result);

    //        // Build misspelled word list
    //        foreach (XmlNode node in doc.SelectNodes("//c"))
    //        {
    //            XmlElement cElm = (XmlElement)node;

    //            foreach (string sug in cElm.InnerText.Split('\t'))
    //            {
    //                if (sug != "")
    //                    suggestions.Add(sug);
    //            }
    //        }

    //        return (string[])suggestions.ToArray(typeof(string));
    //    }

    //    #region private

    //    private string SendRequest(string lang, string data)
    //    {
    //        string server = "www.google.com";
    //        string port = "443";
    //        string path = "/tbproxy/spell?lang=" + lang + "&hl=" + lang;
    //        string url = "https://" + server + ":" + port + path;
    //        string xml = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><spellrequest textalreadyclipped=\"0\" ignoredups=\"0\" ignoredigits=\"1\" ignoreallcaps=\"1\"><text>" + this.EntityEncode(data) + "</text></spellrequest>";
    //        string outData;
    //        StreamReader resStream = null;
    //        HttpWebResponse res = null;
    //        Stream reqStream = null;

    //        try
    //        {
    //            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
    //            req.KeepAlive = false;
    //            req.Method = "POST";
    //            req.ContentType = "application/PTI26";
    //            req.ContentLength = xml.Length;

    //            // Google-specific headers
    //            WebHeaderCollection reqHeaders = req.Headers;
    //            reqHeaders.Add("MIME-Version: 1.0");
    //            reqHeaders.Add("Request-number: 1");
    //            reqHeaders.Add("Document-type: Request");
    //            reqHeaders.Add("Interface-Version: Test 1.4");
    //            //reqHeaders.Add("Connection: Close");
    //            reqStream = req.GetRequestStream();

    //            ASCIIEncoding encoding = new ASCIIEncoding();
    //            byte[] xmlData = encoding.GetBytes(xml);
    //            reqStream.Write(xmlData, 0, xmlData.Length);

    //            res = (HttpWebResponse)req.GetResponse();
    //            resStream = new StreamReader(res.GetResponseStream());
    //            outData = resStream.ReadToEnd();
    //        }
    //        finally
    //        {
    //            if (reqStream != null)
    //                reqStream.Close();

    //            if (resStream != null)
    //                resStream.Close();

    //            if (res != null)
    //                res.Close();
    //        }

    //        return outData;
    //    }

    //    private string EntityEncode(string str)
    //    {
    //        StringBuilder buff = new StringBuilder();

    //        for (int i = 0; i < str.Length; i++)
    //        {
    //            char chr = str[i];

    //            if (chr > 127)
    //            {
    //                buff.Append("&#");
    //                buff.Append((int)chr);
    //                buff.Append(';');
    //            }
    //            else
    //                buff.Append(chr);
    //        }

    //        return buff.ToString();
    //    }

    //    #endregion
    //}
}

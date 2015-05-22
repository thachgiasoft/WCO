/*
 * $Id: SpellCheckerModule.cs 439 2007-11-26 13:26:10Z spocke $
 *
 * @author Moxiecode
 * @copyright Copyright © 2004-2007, Moxiecode Systems AB, All rights reserved.
 * modified by Joe Audette
 */

using System;
using System.Web;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;
using mojoPortal.Web;

namespace mojoPortal.Web.Editor
{
    public class SpellCheckerRequestProcessor : IProcessRequest
    {
        // this article describes the TinyMCE 4 changes vs TinyMCE 3
        //http://achorniy.wordpress.com/2013/05/27/tinymce-4-spellchecker-integration/

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            JSONRpcCall call = JSON.ParseRPC(new System.IO.StreamReader(request.InputStream));
            object result = null;
            GoogleSpellChecker spellchecker = new GoogleSpellChecker();

            switch (call.Method)
            {
                case "checkWords":
                    result = spellchecker.CheckWords((string)call.Args[0], (string[])((ArrayList)call.Args[1]).ToArray(typeof(string)));
                    break;

                case "getSuggestions":
                    result = spellchecker.GetSuggestions((string)call.Args[0], (string)call.Args[1]);
                    break;

                    //TinyMCE 4 version is like combination
                    // it passes words and expects suggestions in the response
                case "spellcheck":
                    string[] words = (string[])call.Args.ToArray(typeof(string));
                    result = spellchecker.SpellCheck(call.Id, call.Lang, words);

                    response.ContentType = "application/json";
                    response.Write(result);

                    return;
            }

            // Serialize RPC output
            JSON.SerializeRPC(
                call.Id,
                null,
                result,
                response.OutputStream
            );
        }
    }
}

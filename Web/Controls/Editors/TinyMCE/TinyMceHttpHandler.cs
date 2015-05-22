
using System;
using System.Web;
//using Moxiecode.TinyMCE.Compression;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web;

namespace mojoPortal.Web.Editor
{
    /// <summary>
    /// Handles callback for TinyMCE editor
    /// </summary>
    public class TinyMceHttpHandler : IHttpHandler
    {
       
        public TinyMceHttpHandler()
        {
        }

       
        
        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            IProcessRequest requestProcessor = null;

            switch (request.QueryString["rp"])
            {
                //case "GzipModule":
                //    requestProcessor = new Moxiecode.TinyMCE.Compression.GzipModule();
                //    break;

                case "spellchecker":
                    // we don't want anonymous requests to initiate server side web requests
                    // lots of requests could generate significant load
                    if(WebUser.IsInRoles(WebConfigSettings.RolesAllowedToUseTinyMCESpellChecker)) // "Authenticated Users" by default
                    {
                        requestProcessor = new SpellCheckerRequestProcessor();
                    }
                    
                    break;
            }

            if (requestProcessor != null)
                requestProcessor.ProcessRequest(context);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}

// Author:					Joe Audette
// Created:				2007-10-27
// Last Modified:			2007-12-08
//		
// The use and distribution terms for this software are covered by the 
// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)
// which can be found in the file CPL.TXT at the root of this distribution.
// By using this software in any fashion, you are agreeing to be bound by 
// the terms of this license.
//
// You must not remove this notice, or any other, from this software.	
// 

using System;
using System.Text;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace mojoPortal.Web.Controls.ExtJs
{
    /// <summary>
    /// to use this you need to include the css
    /// ext/resources/css/ext-all.css
    /// optionally include this after the above:
    /// ext/resources/css/xtheme-gray.css
    /// 
    /// 
    /// </summary>
    [Obsolete("These ExtJs controls are no longer used in mojoPortal and no longer being maintained, we have standardized on jQuery")]
    public class Viewport : Panel
    {
        private string extJsBasePath = "~/ClientScript/ext-2.0/";
        private bool debugMode = false;
        private bool useFormViewport = false;


        public bool UseFormViewport
        {
            get { return useFormViewport; }
            set { useFormViewport = value; }
        }

        /// <summary>
        /// This should be the path to the root of ext. i.e ~/ClientScript/ext/
        /// </summary>
        public string ExtJsBasePath
        {
            get { return extJsBasePath; }
            set { extJsBasePath = value; }
        }

        /// <summary>
        /// When true the ext-all-debug.js file will be included instead of ext-core.
        /// This makes it possible to use the ext debug console by control+shift+home
        /// </summary>
        public bool DebugMode
        {
            get { return debugMode; }
            set { debugMode = value; }
        }




        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (ConfigurationManager.AppSettings["ExtJsBasePath"] != null)
            {
                ExtJsBasePath = ConfigurationManager.AppSettings["ExtJsBasePath"];
            }


        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            SetupScripts();

        }

        private void SetupScripts()
        {
            ExtScriptManager.IncludeBase(Page, ExtJsBasePath);
         
            if (DebugMode)
            {
                ExtScriptManager.IncludeAllDebug(Page, ExtJsBasePath);
            }
            else
            {
                ExtScriptManager.IncludeAll(Page, ExtJsBasePath);
            }

            StateManager.Require(Page);

            if(useFormViewport)
                Page.ClientScript.RegisterClientScriptBlock(
                typeof(ExtScriptManager),
                "ext-formviewport", "\n<script type=\"text/javascript\" src=\""
                + Page.ResolveUrl(extJsBasePath + "ext-mojo/FormViewport.js") + "\" ></script>");

            
            DoSetup();



        }

        private void DoSetup()
        {
            
            StringBuilder script = new StringBuilder();
            script.Append("<script type=\"text/javascript\"> ");
            script.Append("\n<!-- \n");

            script.Append("var Viewport" + this.ClientID + " = {   ");
            script.Append("\n init : function(){  ");

            //script.Append("Ext.state.Manager.setProvider(new Ext.state.CookieProvider());");

            if(useFormViewport)
                script.Append("\n var viewPort = new Ext.FormViewport({ ");
            else
            script.Append("\n var viewPort = new Ext.Viewport({ ");
            
            script.Append(" layout:'border'  ");

            script.Append("\n , items:[");

            AddItems(script);

            script.Append("] ");

            script.Append(" }); ");

            script.Append(" } ");
            script.Append("} ");

            script.Append("\n Ext.EventManager.onDocumentReady(Viewport"
                + this.ClientID + ".init, Viewport" + this.ClientID + ", true);  ");


            script.Append("\n//--> ");
            script.Append(" </script>");


            Page.ClientScript.RegisterClientScriptBlock(
                this.GetType(),
                "setup" + this.ClientID,
                script.ToString());



        }

        private void AddItems(StringBuilder script)
        {
            string comma = string.Empty;
            foreach (Control c in this.Controls)
            {
                if ((c is ExtBasePanel) && (c.Visible))
                {
                    ExtBasePanel p = (ExtBasePanel)c;

                    script.Append("\n" + comma);

                    p.RenderControlToScript(script);

                    comma = ",";

                }

            }

        }

        


    }
}

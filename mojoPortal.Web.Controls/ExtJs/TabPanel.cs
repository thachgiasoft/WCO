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

using System;
using System.Text;
using System.Configuration;
using System.Drawing;
using System.Web.UI;

namespace mojoPortal.Web.Controls.ExtJs
{
    /// <summary>
    /// 
    /// to use this you need to include the css
    /// ext/resources/css/ext-all.css
    /// optionally include this after the above:
    /// ext/resources/css/xtheme-gray.css
    /// 
    /// 
    /// </summary>
    [Obsolete("These ExtJs controls are no longer used in mojoPortal and no longer being maintained, we have standardized on jQuery")]
    public class TabPanel : SplitPanel
    {
        private string selectedTabID = string.Empty;
        private string extJsBasePath = "~/ClientScript/ext-2.0.2/";
        private bool debugMode = false;
        private bool deferredRender = false;
        private bool plain = false;
        private bool enableTabScroll = false;

        


        /// <summary>
        /// The Server side id of the Tab to select when the page is rendered.
        /// </summary>
        public string SelectedTabID
        {
            get { return selectedTabID;}
            set{ selectedTabID = value; }
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

        /// <summary>
        /// True to enable scrolling to tabs that may be invisible due to overflowing the overall TabPanel width. Only available with tabs on top.
        /// Defaults to false
        /// </summary>
        public bool EnableTabScroll
        {
            get { return enableTabScroll; }
            set { enableTabScroll = value; }
        }

        /// <summary>
        /// Internally, the TabPanel uses a Ext.layout.CardLayout to manage its tabs. This property will be passed on to the layout as its Ext.layout.CardLayout.deferredRender config value, determining whether or not each tab is rendered only when first accessed 
        /// (defaults to true in ExtJs)(defaults to false in this control).
        /// </summary>
        public bool DeferredRender
        {
            get { return deferredRender; }
            set { deferredRender = value; }
        }

        /// <summary>
        /// True to render the tab strip without a background container image (defaults to false).
        /// </summary>
        public bool Plain
        {
            get { return plain; }
            set { plain = value; }
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
            if (
                (ConfigurationSettings.AppSettings["DisableExtJsTabs"] != null)
                && (ConfigurationSettings.AppSettings["DisableExtJsTabs"] == "true")
                )
            {
                return;
            }

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

            
            DoSetup();

            

        }

        

        private void DoSetup()
        {
           
            if (DidRenderScript) return;

            StringBuilder script = new StringBuilder();
            script.Append("<script type=\"text/javascript\"> ");
            script.Append("\n<!-- \n");

            script.Append("var Tabs" + this.ClientID + " = {");

            if (UnMinify) script.Append("\n ");
            script.Append("init : function(){");
            if (UnMinify) script.Append("\n ");
            script.Append("var tabs = ");


            bool useApplyTo = true;

            RenderControlToScript(script, useApplyTo);

            

            script.Append(";");
            script.Append("\n ");
            script.Append("}");
            if (UnMinify) script.Append("\n ");
            script.Append("} ; ");

            if (UnMinify) script.Append("\n ");
            script.Append("Ext.EventManager.onDocumentReady(Tabs"
                + this.ClientID + ".init, Tabs" + this.ClientID + ", true);  ");


            script.Append("\n//--> ");
            script.Append(" </script>");


            Page.ClientScript.RegisterClientScriptBlock(
                this.GetType(),
                "setup" + this.ClientID,
                script.ToString());



        }

        public override void RenderControlToScript(StringBuilder script)
        {
            bool useApplyTo = false;
            RenderControlToScript(script, useApplyTo);
        }

        public override void RenderControlToScript(StringBuilder script, bool useApplyTo)
        {
            
            if (DidRenderScript) return;

            
            script.Append("new Ext.TabPanel(");
            
            script.Append("{");

            

            if (useApplyTo)
            {
                if (UnMinify) script.Append("\n ");
                script.Append("applyTo:'" + this.ClientID + "'");
            }
            else
            {
                if (UnMinify) script.Append("\n ");
                script.Append("contentEl:'" + this.ClientID + "'");
            }

            if (UnMinify) script.Append("\n ");
            script.Append(",id:'tp" + this.ClientID + "'");

            if (Title.Length > 0)
            {
                if (UnMinify) script.Append("\n ");
                script.Append(",title:'" + this.Title + "'");
            }

            if (Region.Length > 0)
            {
                if (UnMinify) script.Append("\n ");
                script.Append(",region:'" + this.Region + "'");
            }


            if ((AutoHeight) && (FixedPixelHeight == 0))
            {
                if (UnMinify) script.Append("\n ");
                script.Append(",autoHeight:true");
            }

            if (FixedPixelHeight > 0)
            {
                if (UnMinify) script.Append("\n ");
                script.Append(",height:'" + FixedPixelHeight.ToString() + "'");
            }

            if ((AutoWidth) && (FixedPixelWidth == 0))
            {
                if (UnMinify) script.Append("\n ");
                script.Append(",autoWidth:true");
            }

            if (FixedPixelWidth > 0)
            {
                if (UnMinify) script.Append("\n ");
                script.Append(",width: " + FixedPixelHeight.ToString());
            }

            if (!Header)
            {
                //if (UnMinify) script.Append("\n ");
                //script.Append(", header:false  ");
                
            }

            if (Plain)
            {
                if (UnMinify) script.Append("\n ");
                script.Append(",plain:true");
            }

            if (enableTabScroll)
            {
                if (UnMinify) script.Append("\n ");
                script.Append(",enableTabScroll:true");
            }


            if (Frame)
            {
                if (UnMinify) script.Append("\n ");
                script.Append(",frame:true");
            }
            


            if (!DeferredRender)
            {
                if (UnMinify) script.Append("\n ");
                script.Append(",deferredRender:false");
            }

            if (Split)
            {
                if (UnMinify) script.Append("\n ");
                script.Append(", split: true ");

                if (MinSize > 0)
                {
                    if (UnMinify) script.Append("\n ");
                    script.Append(", minSize: " + MinSize.ToString());

                }

                if (MaxSize > 0)
                {
                    if (UnMinify) script.Append("\n ");
                    script.Append(", maxSize: " + MaxSize.ToString());

                }

            }

            SelectTab(script);

            if (UnMinify) script.Append("\n ");
            script.Append(",items:[");

            AddTabs(script);

            if (UnMinify) script.Append("\n ");
            script.Append("]");

            if (UnMinify) script.Append("\n ");
            script.Append("}");


            if (UnMinify) script.Append("\n ");
            script.Append(")");
            

            DidRenderScript = true;



        }


        private void AddTabs(StringBuilder script)
        {
            string comma = string.Empty;
            foreach (Control c in this.Controls)
            {
                if ((c is Tab) && (c.Visible))
                {
                    Tab t = (Tab)c;

                    if (UnMinify) script.Append("\n ");

                    script.Append(comma);

                    t.RenderControlToScript(script);
                    
                    if(comma.Length == 0)comma = ",";

                }

            }

        }


        private void SelectTab(StringBuilder script)
        {
            int i = 0;
            foreach (Control c in this.Controls)
            {
                if ((c is Tab) && (c.Visible) )
                {
                    if (c.ID == SelectedTabID)
                    {
                    
                        if (UnMinify) script.Append("\n ");
                        script.Append(",activeTab:" + i.ToString());

                        return;

                    }

                    i += 1;
                }

            }

        }

        


    }
}

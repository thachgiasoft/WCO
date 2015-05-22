// Author:					Joe Audette
// Created:				2008-03-29
// Last Modified:			2007-03-29
//		
// The use and distribution terms for this software are covered by the 
// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)
// which can be found in the file CPL.TXT at the root of this distribution.
// By using this software in any fashion, you are agreeing to be bound by 
// the terms of this license.
//
// You must not remove this notice, or any other, from this software.	 

using System;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI;

namespace mojoPortal.Web.Controls.ExtJs
{
    /// <summary>
    /// These ExtJs controls are no longer used in mojoPortal and no longer being maintained, we have standardized on jQuery
    /// </summary>
    [Obsolete("These ExtJs controls are no longer used in mojoPortal and no longer being maintained, we have standardized on jQuery")]
    public class ExtWindow : ExtPanel
    {
        #region Private Properties

        private string extJsBasePath = "~/ClientScript/ext-2.0/";
        private bool debugMode = false;

        // window specific props
        private string animateTarget = string.Empty;
        private bool closable = true;
        private string baseCls = "x-window";
        private string closeAction = "close";
        // constrain to viewport
        private bool constrain = false;
        private string defaultButton = string.Empty;
        private bool draggable = true;
        private bool expandOnShow = true;
        private bool maximizable = false;
        private bool minimizable = false;
        private int minHeight = 100;
        private int minWidth = 200;
        private bool modal = false;
        private bool plain = false;
        private bool resizable = true;
        private string resizeHandles = "all";

        #endregion

        #region Public Properties


        public string AnimateTarget
        {
            get { return animateTarget; }
            set { animateTarget = value; }
        }

        public bool Closable
        {
            get { return closable; }
            set { closable = value; }
        }

        public string BaseCls
        {
            get { return baseCls; }
            set { baseCls = value; }
        }

        public string CloseAction
        {
            get { return closeAction; }
            set { closeAction = value; }
        }

        public bool Constrain
        {
            get { return constrain; }
            set { constrain = value; }
        }

        public override string DefaultButton
        {
            get { return defaultButton; }
            set { defaultButton = value; }
        }

        public bool Draggable
        {
            get { return draggable; }
            set { draggable = value; }
        }

        public bool ExpandOnShow
        {
            get { return expandOnShow; }
            set { expandOnShow = value; }
        }

        public bool Maximizable
        {
            get { return maximizable; }
            set { maximizable = value; }
        }

        public bool Minimizable
        {
            get { return minimizable; }
            set { minimizable = value; }
        }

        public int MinHeight
        {
            get { return minHeight; }
            set { minHeight = value; }
        }

        public int MinWidth
        {
            get { return minWidth; }
            set { minWidth = value; }
        }

        public bool Modal
        {
            get { return modal; }
            set { modal = value; }
        }

        public bool Plain
        {
            get { return plain; }
            set { plain = value; }
        }

        public bool Resizable
        {
            get { return resizable; }
            set { resizable = value; }
        }

        public string ResizeHandles
        {
            get { return resizeHandles; }
            set { resizeHandles = value; }
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

        #endregion


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


            DoSetup();



        }



        private void DoSetup()
        {

            if (DidRenderScript) return;

            StringBuilder script = new StringBuilder();
            script.Append("<script type=\"text/javascript\"> ");
            script.Append("\n<!-- \n");

            script.Append("var win" + this.ClientID + " = {");

            if (UnMinify) script.Append("\n ");
            script.Append("init : function(){");
            if (UnMinify) script.Append("\n ");
            script.Append("var win = ");


            bool useApplyTo = false;

            RenderControlToScript(script, useApplyTo);


            script.Append(";");

            
            script.Append("win.show();");
            script.Append("win.doLayout();");
            //script.Append("win.center();");
            

            script.Append("\n ");
            script.Append("}");
            if (UnMinify) script.Append("\n ");
            script.Append("} ; ");

            if (UnMinify) script.Append("\n ");
            script.Append("Ext.EventManager.onDocumentReady(win"
                + this.ClientID + ".init, win" + this.ClientID + ", true);  ");


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

            
            script.Append("new Ext.Window(");

            script.Append("{");

            //if (useApplyTo)
            //{
            //    if (UnMinify) script.Append("\n ");
            script.Append("applyTo:'" + this.ClientID + "'");
            //}
            //else
            //{
            //    if (UnMinify) script.Append("\n ");
            //    script.Append("contentEl:'" + this.ClientID + "'");
            //}

            if (UnMinify) script.Append("\n ");
            script.Append(",id:'tp" + this.ClientID + "'");

            if (Title.Length > 0)
            {
                if (UnMinify) script.Append("\n ");
                script.Append(",title:'" + this.Title + "'");
            }

            //if (Region.Length > 0)
            //{
            //    if (UnMinify) script.Append("\n ");
            //    script.Append(",region:'" + this.Region + "'");
            //}

            //script.Append(",deferredRender:false");

            if (Layout.Length > 0)
            {
                script.Append(", layout: '" + Layout + "'");
            }

            if ((AutoHeight) && (FixedPixelHeight == 0))
            {
                if (UnMinify) script.Append("\n ");
                script.Append(",autoHeight:true");
            }

            

            if ((AutoWidth) && (FixedPixelWidth == 0))
            {
                if (UnMinify) script.Append("\n ");
                script.Append(",autoWidth:true");
            }

            if (FixedPixelHeight > 0)
            {
                if (UnMinify) script.Append("\n ");
                script.Append(",height:'" + FixedPixelHeight.ToString() + "'");
            }

            if (FixedPixelWidth > 0)
            {
                if (UnMinify) script.Append("\n ");
                script.Append(",width: " + FixedPixelWidth.ToString());
            }

            //script.Append(",height:'100%'");
            //script.Append(",width:'100%'");


            if (UsePosition)
            {
                script.Append(",x:" + Left.ToString(CultureInfo.InvariantCulture));
                script.Append(",y:" + Top.ToString(CultureInfo.InvariantCulture));
            }

            if(closable)
                script.Append(",closable:true");
            else
                script.Append(",closable:false");

            if(animateTarget.Length > 0)
                script.Append(",animateTarget:'" + this.animateTarget + "'");

            script.Append(",baseCls:'" + this.baseCls + "'");
            script.Append(",closeAction:'" + this.closeAction + "'");

            if (constrain)
                script.Append(",constrain:true");
            else
                script.Append(",constrain:false");

            if (defaultButton.Length > 0)
                script.Append(",defaultButton:'" + this.defaultButton + "'");

            if (draggable)
                script.Append(",draggable:true");
            else
                script.Append(",draggable:false");

            if (expandOnShow)
                script.Append(",expandOnShow:true");
            else
                script.Append(",expandOnShow:false");

            if (maximizable)
                script.Append(",maximizable:true");
            else
                script.Append(",maximizable:false");

            if (plain)
                script.Append(",plain:true");
            else
                script.Append(",plain:false");

            if (resizable)
            {
                script.Append(",resizable:true");
                script.Append(",resizeHandles:'" + this.resizeHandles + "'");
            }
            else
                script.Append(",resizable:false");



            script.Append(",minHeight: " + minHeight.ToString());
            script.Append(",minWidth: " + minWidth.ToString());

            if (modal)
                script.Append(",modal:true");
            else
                script.Append(",modal:false");

            if (minimizable)
                script.Append(",minimizable:true");
            else
                script.Append(",minimizable:false");

            

            script.Append(",id:'" + this.ClientID + "'");

            bool hasChildren = HasVisibleExtChildren();

            if (hasChildren)
            {
                script.Append("\n ,items:[");

                AddExtItems(script);

                script.Append(" ]  ");

            }

            if (UnMinify) script.Append("\n ");
            script.Append("}");


            if (UnMinify) script.Append("\n ");

            
            script.Append(")");


            DidRenderScript = true;



        }

        protected void AddExtItems(StringBuilder script)
        {
            string comma = string.Empty;
            foreach (Control c in this.Controls)
            {
                if ((c is ExtBasePanel) && (c.Visible))
                {
                    ExtBasePanel p = (ExtBasePanel)c;

                    script.Append("\n" + comma);

                    //p.RenderConstructor = true;
                    p.RenderControlToScript(script);

                    comma = ",";

                }

            }

        }


    }
}

/// Author:					Joe Audette
/// Created:				2008-12-17
/// Last Modified:			2009-11-11
///		
/// The use and distribution terms for this software are covered by the 
/// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)
/// which can be found in the file CPL.TXT at the root of this distribution.
/// By using this software in any fashion, you are agreeing to be bound by 
/// the terms of this license.
///
/// You must not remove this notice, or any other, from this software.	 
 
using System;
using System.Configuration;
using System.Text;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mojoPortal.Web.UI
{
    /// <summary>
    /// These YUI controls are no longer used in mojoPortal and no longer being maintained, we have standardized on jQuery
    /// Extends the GridView by attaching YUI DataTable javascript
    /// http://developer.yahoo.com/yui/datatable/
    /// http://developer.yahoo.com/yui/docs/YAHOO.widget.DataTable.html
    /// http://developer.yahoo.com/yui/docs/module_datatable.html
    /// 
    /// </summary>
    [Obsolete("These YUI controls are no longer used in mojoPortal and no longer being maintained, we have standardized on jQuery")]
    public class YuiGridView : GridView
    {

        #region Private/Protected Properties

        private string caption = string.Empty;
        private string summary = string.Empty;
        //private string currencyOptions = "{prefix: $, decimalPlaces:2, decimalSeparator:\".\", thousandsSeparator:\",\"} ";
        //private string dateOptions = "{format:\"%m/%d/%Y\", locale:\"en\"} ";
        private bool draggableColumns = false;
        private string selectionMode = "standard";
        private bool disableYui = false;


        #endregion

        #region Public Properties

        /// <summary>
        /// Value for the CAPTION element. NB: Not supported in ScrollingDataTable. 
        /// </summary>
        public override string Caption
        {
            get { return caption; }
            set { caption = value; }
        }

        /// <summary>
        /// Value for the SUMMARY attribute. 
        /// </summary>
        public string Summary
        {
            get { return summary; }
            set { summary = value; }
        }

        /// <summary>
        /// "standard"
        ///Standard row selection with support for modifier keys to enable multiple selections.
        ///"single"
        ///Row selection with modifier keys disabled to not allow multiple selections.
        ///"singlecell"
        ///Cell selection with modifier keys disabled to not allow multiple selections.
        ///"cellblock"
        ///Cell selection with support for modifier keys to enable multiple selections in a block-fashion, like a spreadsheet.
        ///"cellrange"
        ///Cell selection with support for modifier keys to enable multiple selections in a range-fashion, like a calendar.
        ///
        ///Default Value: "standard" 
        /// </summary>
        public string SelectionMode
        {
            get { return selectionMode; }
            set { selectionMode = value; }
        }


        /// <summary>
        /// True if Columns are draggable to reorder, false otherwise. The Drag & Drop Utility is required to enable this feature. 
        /// Only top-level and non-nested Columns are draggable. Write once. 
        /// </summary>
        public bool DraggableColumns
        {
            get { return draggableColumns; }
            set { draggableColumns = value; }
        }

        public bool DisableYui
        {
            get { return disableYui; }
            set { disableYui = value; }
        }


        #endregion


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (disableYui) { return; }
            if (Page is mojoBasePage)
            {
                mojoBasePage p = Page as mojoBasePage;
                p.ScriptConfig.IncludeYuiDataTable = true;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (disableYui) { return; }
            if (WebConfigSettings.DisableYUI) { return; }
            SetupCss();
            SetupScript();

        }



        protected void SetupScript()
        {
            
            StringBuilder script = new StringBuilder();
            script.Append("<script type=\"text/javascript\"> ");
            script.Append("\n<!-- \n");

            script.Append("function setup" + this.ClientID + " () {   ");
            script.Append("var container = document.getElementById('" + this.ClientID + "');");
            script.Append("var tbl = document.getElementById('tbl" + this.ClientID + "');");
            script.Append("var myDataSource = new YAHOO.util.DataSource(YAHOO.util.Dom.get(tbl.id)); ");
            script.Append("myDataSource.responseType = YAHOO.util.DataSource.TYPE_HTMLTABLE; ");
            script.Append("myDataSource.responseSchema = {fields:[  ");
            RenderSchema(script);
            script.Append(" ]}; ");
            script.Append("var myColumnDefs = [ ");
            RenderColumnDefs(script);
            script.Append("]; ");
            script.Append("var config = { ");
            script.Append("caption:\"" + caption + "\"");
            script.Append(",summary:\"" + summary + "\"");
            script.Append(",selectionmode:\"" + selectionMode + "\"");
            if (draggableColumns)
            {
                script.Append(",draggableColumns: true");
            }
            else
            {
                script.Append(",draggableColumns: false");
            }

            script.Append("};");

            script.Append("var myDataTable = new YAHOO.widget.DataTable(container, myColumnDefs, myDataSource, config); ");
            script.Append("} ");


            //script.Append("\n Sys.Application.add_load(setup" + this.ClientID + ");");

            script.Append("\n//--> ");
            script.Append(" </script>");

            Page.ClientScript.RegisterClientScriptBlock(
                this.GetType(),
                "setup" + this.ClientID,
                script.ToString(), false);

            // TODO: better solution, this can cause problems with other UpdatePanels on the page
            Page.ClientScript.RegisterStartupScript(
                this.GetType(),
                "init" + this.ClientID,
                "\n Sys.Application.add_load(setup" + this.ClientID + ");", true);

            //Page.ClientScript.RegisterStartupScript(
            //    this.GetType(),
            //    "init" + this.ClientID,
            //    "\n setup" + this.ClientID + "();", true);

        }

        public void RenderColumnDefs(StringBuilder script)
        {
            string comma = string.Empty;

            foreach (DataControlField df in this.Columns)
            {
                if (!df.Visible) { continue; }
                if (!(df is YuiTemplateField)) { continue; }

                YuiTemplateField tf = df as YuiTemplateField;
                script.Append(comma);
                script.Append("{");
                script.Append("key:\"" + tf.Key + "\"");
                if (tf.HeaderText.Length > 0)
                {
                    script.Append(",label:\"" + tf.HeaderText + "\"");
                }

                if (tf.Abbr.Length > 0)
                {
                    script.Append(",abbr:\"" + tf.Abbr + "\"");
                }

                if (tf.Formatter.Length > 0)
                {
                    script.Append(",formatter:\"" + tf.Formatter + "\"");
                }

                if (tf.Sortable)
                {
                    script.Append(",sortable: true");
                }
                else
                {
                    script.Append(",sortable: false");
                }

                if (tf.Hidden)
                {
                    script.Append(",hidden: true");

                }
                else
                {
                    script.Append(",'hidden': false");
                }

                if (tf.Resizable)
                {
                    script.Append(",resizable: true");
                }
                else
                {
                    script.Append(",resizable: false");
                }

                script.Append(",width:" + tf.ColumnWidth.ToString(CultureInfo.InvariantCulture));
                script.Append(",maxautowidth:" + tf.MaxAutoWidth.ToString(CultureInfo.InvariantCulture));
                script.Append(",minwidth:" + tf.MinWidth.ToString(CultureInfo.InvariantCulture));

                script.Append("}");
                comma = ",";

            }

        }

        public void RenderSchema(StringBuilder script)
        {
            string comma = string.Empty;

            foreach (DataControlField df in this.Columns)
            {
                if (!df.Visible) { continue; }
                if (!(df is YuiTemplateField)) { continue; }

                YuiTemplateField tf = df as YuiTemplateField;

                script.Append(comma);
                script.Append("{");
                script.Append("key:\"" + tf.Key + "\"");

                if (tf.Parser.Length > 0)
                {
                    script.Append(",parser:\"" + tf.Parser + "\"");
                }

                script.Append("}");

                comma = ",";

            }

        }


        



        private void SetupCss()
        {
            if (HttpContext.Current == null) { return; }
            if (HttpContext.Current.Request == null) { return; }

            //string protocol = "http";
            //if (SiteUtils.IsSecureRequest()) { protocol = "https"; }

            //http://ajax.googleapis.com/ajax/libs/yui/2.6.0/build/datatable/assets/skins/sam/datatable.css

            Control head = Page.Master.FindControl("Head1");
            if (head != null)
            {
                try
                {
                    if (head.FindControl("yui-datatable") == null)
                    {
                        Literal cssLink = new Literal();
                        cssLink.ID = "yui-datatable";
                        cssLink.Text = "\n<link rel='stylesheet' type='text/css' href='"
                            + GetYuiBasePath() + "datatable/assets/skins/sam/datatable.css"
                             + "' />";



                        head.Controls.Add(cssLink);
                    }
                }
                catch (HttpException) { }

            }


        }

        private string GetYuiBasePath()
        {
            string yuiVersion = "2.6.0";
            string protocol = "http";
            if (SiteUtils.IsSecureRequest()) { protocol = "https"; }

            if (ConfigurationManager.AppSettings["GoogleCDNYUIVersion"] != null)
            {
                yuiVersion = ConfigurationManager.AppSettings["GoogleCDNYUIVersion"];
            }

            if (WebConfigSettings.UseGoogleCDN)
            {
                return protocol + "://ajax.googleapis.com/ajax/libs/yui/" + yuiVersion + "/build/";
            }

            if (ConfigurationManager.AppSettings["YUIBasePath"] != null)
            {
                string yuiBasePath = ConfigurationManager.AppSettings["YUIBasePath"];
                return Page.ResolveUrl(yuiBasePath);
            }

            return string.Empty;
        }

        //private void SetupBaseScripts()
        //{
        //    if (HttpContext.Current == null) { return; }
        //    if (HttpContext.Current.Request == null) { return; }

        //    string protocol = "http";
        //    if (SiteUtils.IsSecureRequest()) { protocol = "https"; }

        //    string scriptBaseUrl = protocol + "://ajax.googleapis.com/ajax/libs/yui/2.6.0/build/";

        //    Page.ClientScript.RegisterClientScriptBlock(typeof(Page),
        //        "yui-dom-event", "\n<script type=\"text/javascript\" src=\""
        //        + scriptBaseUrl + "yahoo-dom-event/yahoo-dom-event.js" + "\" ></script>");

        //    Page.ClientScript.RegisterClientScriptBlock(typeof(Page),
        //        "yui-element", "\n<script type=\"text/javascript\" src=\""
        //        + scriptBaseUrl + "element/element-beta-min.js" + "\" ></script>");

        //    Page.ClientScript.RegisterClientScriptBlock(typeof(Page),
        //        "yui-datasource", "\n<script type=\"text/javascript\" src=\""
        //        + scriptBaseUrl + "datasource/datasource-min.js" + "\" ></script>");

        //    Page.ClientScript.RegisterClientScriptBlock(typeof(Page),
        //        "yui-json", "\n<script type=\"text/javascript\" src=\""
        //        + scriptBaseUrl + "json/json-min.js" + "\" ></script>");

        //    Page.ClientScript.RegisterClientScriptBlock(typeof(Page),
        //        "yui-connection", "\n<script type=\"text/javascript\" src=\""
        //        + scriptBaseUrl + "connection/connection-min.js" + "\" ></script>");

        //    Page.ClientScript.RegisterClientScriptBlock(typeof(Page),
        //        "yui-get", "\n<script type=\"text/javascript\" src=\""
        //        + scriptBaseUrl + "get/get-min.js" + "\" ></script>");

        //    Page.ClientScript.RegisterClientScriptBlock(typeof(Page),
        //        "yui-dragdrop", "\n<script type=\"text/javascript\" src=\""
        //        + scriptBaseUrl + "dragdrop/dragdrop-min.js" + "\" ></script>");

        //    Page.ClientScript.RegisterClientScriptBlock(typeof(Page),
        //        "yui-datatable", "\n<script type=\"text/javascript\" src=\""
        //        + scriptBaseUrl + "datatable/datatable-min.js" + "\" ></script>");

        //}


    }
}

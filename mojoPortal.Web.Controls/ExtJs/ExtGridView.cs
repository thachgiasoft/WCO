// Author:					Joe Audette
// Created:				2007-11-04
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
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mojoPortal.Web.Controls.ExtJs
{
    /// <summary>
    /// These ExtJs controls are no longer used in mojoPortal and no longer being maintained, we have standardized on jQuery
    /// Extends the GridView by attaching ExtJs javascript
    /// </summary>
    [Obsolete("These ExtJs controls are no longer used in mojoPortal and no longer being maintained, we have standardized on jQuery")]
    public class ExtGridView : GridView
    {

        #region Private/Protected Properties

        protected bool DidRenderScript = false;

        private string extJsBasePath = "~/ClientScript/ext-2.0/";
        private bool debugMode = false;

        //Ext Container Properties
        private bool autoDestroy = true;
        private bool autoHeight = false;
        private bool autoWidth = false;
        private bool autoShow = false;
        private string cls = string.Empty;
        private string disabledClass = string.Empty;
        private int fixedPixelHeight = 0;
        private int fixedPixelWidth = 0;
        private bool hideBorders = false;

        //Ext Panel properties
        private bool animCollapse = true;
        private string autoLoad = string.Empty;
        private bool autoScroll = false;
        private bool border = true;
        private bool collapseFirst = true;
        private bool collapsed = false;
        private bool collapsible = false;
        private bool header = false;
        private bool headerAsText = true;
        private bool footer = false;
        private string title = string.Empty;
        private string layout = string.Empty;
        private string region = string.Empty;
        private bool frame = false;
        private string iconCls = string.Empty;

        // Ext GridPanel properties
        private string autoExpandColumn = string.Empty;
        private bool disableSelection = false;
        private bool enableColumnHide = true;
        private bool enableColumnMove = true;
        private bool enableColumnResize = true;
        private bool enableDragDrop = false;
        private bool enableHdMenu = true;
        private bool enableRowHeightSync = false;
        private bool loadMask = false;
        private int minColumnWidth = 25;
        private bool monitorWindowResize = true;
        private bool stripeRows = false;
        private bool trackMouseOver = true;

        

        //private string cm = "";
        // colModel

        
        

        #endregion

        #region Public Properties

        /// <summary>
        /// True to create the header element explicitly, false to skip creating it. By default, when header is not 
        /// specified, if a title is set the header will be created automatically, otherwise it will not. If a title 
        /// is set but header is explicitly set to false, the header will not be rendered.
        /// </summary>
        public bool Header
        {
            get { return header; }
            set { header = value; }
        }

        /// <summary>
        /// True to create the footer element explicitly, false to skip creating it. By default, when footer is not specified, 
        /// if one or more buttons have been added to the panel the footer will be created automatically, otherwise it 
        /// will not.
        /// </summary>
        public bool Footer
        {
            get { return footer; }
            set { footer = value; }
        }

        /// <summary>
        /// The id of a column in this grid that should expand to fill unused space. This id can not be 0.
        /// </summary>
        public string AutoExpandColumn
        {
            get { return autoExpandColumn; }
            set { autoExpandColumn = value; }
        }

        /// <summary>
        /// True to disable selections in the grid (defaults to false).
        /// </summary>
        public bool DisableSelection
        {
            get { return disableSelection; }
            set { disableSelection = value; }
        }

        /// <summary>
        /// True to enable hiding of columns with the header context menu.
        /// </summary>
        public bool EnableColumnHide
        {
            get { return enableColumnHide; }
            set { enableColumnHide = value; }
        }

        /// <summary>
        /// True to enable drag and drop reorder of columns.
        /// </summary>
        public bool EnableColumnMove
        {
            get { return enableColumnMove; }
            set { enableColumnMove = value; }
        }

        /// <summary>
        /// False to turn off column resizing for the whole grid (defaults to true).
        /// </summary>
        public bool EnableColumnResize
        {
            get { return enableColumnResize; }
            set { enableColumnResize = value; }
        }

        /// <summary>
        /// True to enable drag and drop of rows.
        /// </summary>
        public bool EnableDragDrop
        {
            get { return enableDragDrop; }
            set { enableDragDrop = value; }
        }

        /// <summary>
        /// False to disable the drop down button for menu in the headers.
        /// </summary>
        public bool EnableHdMenu
        {
            get { return enableHdMenu; }
            set { enableHdMenu = value; }
        }


        /// <summary>
        /// True to manually sync row heights across locked and not locked rows.
        /// </summary>
        public bool EnableRowHeightSync
        {
            get { return enableRowHeightSync; }
            set { enableRowHeightSync = value; }
        }

        /// <summary>
        /// true to mask the grid while loading (defaults to false).
        /// </summary>
        public bool LoadMask
        {
            get { return loadMask; }
            set { loadMask = value; }
        }

        /// <summary>
        /// The minimum width a column can be resized to. Defaults to 25.
        /// </summary>
        public int MinColumnWidth
        {
            get { return minColumnWidth; }
            set { minColumnWidth = value; }
        }

        /// <summary>
        /// True to autoSize the grid when the window resizes. Defaults to true.
        /// </summary>
        public bool MonitorWindowResize
        {
            get { return monitorWindowResize; }
            set { monitorWindowResize = value; }
        }


        /// <summary>
        /// True to stripe the rows. Default is false.
        /// </summary>
        public bool StripeRows
        {
            get { return stripeRows; }
            set { stripeRows = value; }
        }

        /// <summary>
        /// True to highlight rows when the mouse is over. Default is true.
        /// </summary>
        public bool TrackMouseOver
        {
            get { return trackMouseOver; }
            set { trackMouseOver = value; }
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
        /// if true the container will automatically destroy any contained component that is removed from it, else destruction must be handled manually (defaults to true).
        /// </summary>
        public bool AutoDestroy
        {
            get { return autoDestroy; }
            set { autoDestroy = value; }
        }

        /// <summary>
        /// True to use height:'auto', false to use fixed height (defaults to false).
        /// </summary>
        public bool AutoHeight
        {
            get { return autoHeight; }
            set { autoHeight = value; }
        }

        /// <summary>
        /// True to use width:'auto', false to use fixed width (defaults to false).
        /// </summary>
        public bool AutoWidth
        {
            get { return autoWidth; }
            set { autoWidth = value; }
        }

        /// <summary>
        /// height : Number The height of this component in pixels.
        /// If AutoHeight is false and FixedPixelHeight > 0 then FixedPixelHeight will be used.
        /// Defaults to 0
        /// </summary>
        public int FixedPixelHeight
        {
            get { return fixedPixelHeight; }
            set { fixedPixelHeight = value; }
        }

        /// <summary>
        /// height : Number The width of this component in pixels.
        /// If AutoWidth is false and FixedPixelWidth > 0 then FixedPixelWidth will be used.
        /// Defaults to 0
        /// </summary> 
        public int FixedPixelWidth
        {
            get { return fixedPixelWidth; }
            set { fixedPixelWidth = value; }
        }

        /// <summary>
        /// True if the component should check for hidden classes (e.g. 'x-hidden' or 'x-hide-display') and remove them on render (defaults to false).
        /// </summary>
        public bool AutoShow
        {
            get { return autoShow; }
            set { autoShow = value; }
        }

        /// <summary>
        /// An optional extra CSS class that will be added to this component's Element (defaults to ''). This can be useful for adding customized styles to the component or any of its children using standard CSS rules.
        /// </summary>
        public string ExtraCssClass
        {
            get { return cls; }
            set { cls = value; }
        }

        /// <summary>
        /// CSS class added to the component when it is disabled (defaults to "x-item-disabled").
        /// </summary>
        public string DisabledClass
        {
            get { return disabledClass; }
            set { disabledClass = value; }
        }

        /// <summary>
        /// True to hide the borders of each contained component, false to defer to the component's existing border settings (defaults to false).
        /// </summary>
        public bool HideBorders
        {
            get { return hideBorders; }
            set { hideBorders = value; }
        }

        /// <summary>
        /// The title text to display in the panel header (defaults to ''). When a title is specified the header element will automatically be created and displayed unless header is explicitly set to false.
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        /// <summary>
        /// The layout type to be used in this container. If not specified, a default Ext.layout.ContainerLayout will be created and used. Valid values are: accordion, anchor, border, card, column, fit, form and table. Specific config values for the chosen layout type can be specified using layoutConfig.
        /// </summary>
        public string Layout
        {
            get { return layout; }
            set { layout = value; }
        }

        public string Region
        {
            get { return region; }
            set { region = value; }
        }

        /// <summary>
        /// True to animate the transition when the panel is collapsed, false to skip the animation (defaults to true if the Ext.Fx class is available, otherwise false).
        /// </summary>
        public bool AnimCollapse
        {
            get { return animCollapse; }
            set { animCollapse = value; }
        }

        /// <summary>
        /// A valid url spec according to the Updater Ext.Updater.update method. If autoLoad is not null, the panel will attempt to load its contents immediately upon render.
        /// </summary>
        public string AutoLoad
        {
            get { return autoLoad; }
            set { autoLoad = value; }
        }

        /// <summary>
        /// True to use overflow:'auto' on the panel's body element and show scroll bars automatically when necessary, false to clip any overflowing content (defaults to false).
        /// </summary>
        public bool AutoScroll
        {
            get { return autoScroll; }
            set { autoScroll = value; }
        }

        /// <summary>
        /// True to make sure the collapse/expand toggle button always renders first (to the left of) any other tools in the panel's title bar, false to render it last (defaults to true).
        /// </summary>
        public bool CollapseFirst
        {
            get { return collapseFirst; }
            set { collapseFirst = value; }
        }

        /// <summary>
        /// True to render the panel collapsed, false to render it expanded (defaults to false).
        /// </summary>
        public bool Collapsed
        {
            get { return collapsed; }
            set { collapsed = value; }
        }

        /// <summary>
        /// True to make the panel collapsible and have the expand/collapse toggle button automatically rendered into the header tool button area, false to keep the panel statically sized with no button (defaults to false).
        /// </summary>
        public bool Collapsible
        {
            get { return collapsible; }
            set { collapsible = value; }
        }

        


        /// <summary>
        /// True to display the panel title in the header, false to hide it (defaults to true).
        /// </summary>
        public bool HeaderAsText
        {
            get { return headerAsText; }
            set { headerAsText = value; }
        }

        /// <summary>
        /// True to render the panel with custom rounded borders, false to render with plain 1px square borders (defaults to false).
        /// </summary>
        public bool Frame
        {
            get { return frame; }
            set { frame = value; }
        }

        /// <summary>
        /// A CSS class that will provide a background image to be used as the panel header icon (defaults to '').
        /// </summary>
        public string IconCls
        {
            get { return iconCls; }
            set { iconCls = value; }
        }

        /// <summary>
        /// True to display the borders of the panel's body element, false to hide them (defaults to true). By default, the border is a 2px wide inset border, but this can be further altered by setting bodyBorder to false.
        /// </summary>
        public bool Border
        {
            get { return border; }
            set { border = value; }
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

            ExtScriptManager.IncludeTableGrid(Page, ExtJsBasePath);

            //StateManager.Require(Page);

            DoSetup();



        }

        private void DoSetup()
        {

            StringBuilder script = new StringBuilder();
            script.Append("<script type=\"text/javascript\"> ");
            script.Append("\n<!-- \n");

            script.Append("var gridview" + this.ClientID + " = {   ");
            script.Append("\n init : function(){  ");

            script.Append("var config = ");
            RenderControlToScript(script);
            script.Append("; ");

            
            
            script.Append("var grid = new Ext.grid.TableGrid('tbl" + this.ClientID + "', config");

            script.Append("); ");

            //script.Append("{} ");
            script.Append("Ext.apply(grid,config");
            //RenderControlToScript(script);
            script.Append("); ");

            //script.Append("grid.enableColumnHide = false; ");

            //script.Append("); ");

            script.Append(" grid.render();  ");

            //script.Append(" alert('grid rendered');");

            script.Append(" } ");
            script.Append("} ");

            script.Append("\n Ext.EventManager.onDocumentReady(gridview"
                + this.ClientID + ".init, gridview" + this.ClientID + ", true);  ");


            script.Append("\n//--> ");
            script.Append(" </script>");


            Page.ClientScript.RegisterClientScriptBlock(
                this.GetType(),
                "setup" + this.ClientID,
                script.ToString());



        }

        public void RenderColumnModels(StringBuilder script)
        {
            script.Append(",columns:[");

            string comma = string.Empty;

            foreach (DataControlField df in this.Columns)
            {
                if ((df.Visible)&&(df is ExtTemplateField))
                {
                    script.Append(comma);
                    script.Append("{");
                    ExtTemplateField tf = (ExtTemplateField)df;

                    if (tf.IsClientSortable)
                    {
                        script.Append("'sortable': true");
                    }
                    else
                    {
                        script.Append("'sortable': false");
                    }

                    if (tf.IsClientHideable)
                    {
                        script.Append(",'hideable': true");

                    }
                    else
                    {
                        script.Append(",'hideable': false");
                    }

                    if (tf.ColumnWidth > 0)
                    {
                        script.Append(",width:" + tf.ColumnWidth.ToString(CultureInfo.InvariantCulture));
                    }

                    script.Append("}");
                    comma = ",";
                }


            }

            script.Append("]");


        }

        public void RenderControlToScript(StringBuilder script)
        {

            if (DidRenderScript) return;

            script.Append(" {  ");
            //script.Append("contentEl:'" + this.ClientID + "'  ");

            //if (Title.Length > 0)
            //{
                script.Append("title:'" + this.Title + "'");
            //}

            if (region.Length > 0)
            {
                script.Append(",region:'" + region + "'");
            }

            if (layout.Length > 0)
            {
                script.Append(",layout:'" + layout + "'");
            }

            if ((AutoHeight) && (FixedPixelHeight == 0))
            {
                script.Append(",autoHeight:true");
            }

            if (FixedPixelHeight > 0)
            {
                script.Append(",height:'" + FixedPixelHeight.ToString() + "'");
            }

            if ((AutoWidth) && (FixedPixelWidth == 0))
            {
                script.Append(",autoWidth:true  ");
            }

            if (FixedPixelWidth > 0)
            {
                script.Append(",width: " + FixedPixelWidth.ToString());
            }

            if (collapsible)
            {
                script.Append(",collapsible:true ");

            }

            if (AutoScroll)
            {
                script.Append(",autoScroll:true ");
            }

            if (IconCls.Length > 0)
            {
                script.Append(",iconCls:'" + IconCls + "' ");
            }

            if (!Border)
            {
                script.Append(",border:false");
            }


            //private string autoExpandColumn = string.Empty;

            if (AutoExpandColumn.Length > 0)
            {
                script.Append(",autoExpandColumn:'" + AutoExpandColumn + "' ");
            }

            //private bool disableSelection = false;

            if (DisableSelection)
            {
                script.Append(",disableSelection:true");
            }

            //private bool enableColumnHide = true;

            if (EnableColumnHide)
            {
                script.Append(",enableColumnHide:true");
            }
            else
            {
                script.Append(",enableColumnHide:false");
            }

            //private bool enableColumnMove = true;

            if (!EnableColumnMove)
            {
                script.Append(",enableColumnMove:false ");
            }

            //private bool enableColumnResize = true;
            if (!EnableColumnResize)
            {
                script.Append(",enableColumnResize:false ");
            }

            //private bool enableDragDrop = false;
            if (EnableDragDrop)
            {
                script.Append(",enableDragDrop:true");
            }

            if (header)
            {
                script.Append(",header:true");
            }

            if (footer)
            {
                script.Append(",footer:true");
            }

            //private bool enableHdMenu = true;
            if (EnableHdMenu)
            {
                script.Append(",enableHdMenu:true");
            }
            else
            {
                script.Append(",enableHdMenu:false");
            }

            //private bool enableRowHeightSync = false;
            if (EnableRowHeightSync)
            {
                script.Append(",enableRowHeightSync:true");
            }


            //private bool loadMask = false;
            if (LoadMask)
            {
                script.Append(",loadMask:true");
            }

            //private int minColumnWidth = 25;
            if (MinColumnWidth != 25)
            {
                script.Append(",minColumnWidth:" + MinColumnWidth.ToString());
            }


            //private bool monitorWindowResize = true;
            if (!MonitorWindowResize)
            {
                script.Append(",monitorWindowResize:false");
            }


            //private bool stripeRows = false;
            if (StripeRows)
            {
                script.Append(",stripeRows:true");
            }

            //private bool trackMouseOver = true;
            if (!TrackMouseOver)
            {
                script.Append(",trackMouseOver:false");
            }


            if (HasVisibleExtTemplateFields())
            {
                RenderColumnModels(script);
            }



            script.Append(" } ");

            DidRenderScript = true;


        }

        public bool HasVisibleExtTemplateFields()
        {
            foreach (DataControlField df in this.Columns)
            {
                if ((df.Visible) && (df is ExtTemplateField)) return true;
            }

            return false;

        }



    }
}

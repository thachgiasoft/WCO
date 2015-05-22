///	Created:			    2009-04-12
///	Last Modified:		    2009-04-13
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
using System.Globalization;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using mojoPortal.Web.Framework;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using Resources;

namespace mojoPortal.Web.UI
{
    /// <summary>
    /// These YUI controls are no longer used in mojoPortal and no longer being maintained, we have standardized on jQuery
    /// </summary>
    [Obsolete("These YUI controls are no longer used in mojoPortal and no longer being maintained, we have standardized on jQuery")]
    public class YuiPageLayout : WebControl
    {
        #region Private Properties

        private string topControlId = string.Empty;
        private string topCaption = string.Empty;
        private string topFooter = string.Empty;
        private int topHeight = 100;
        private int topMaxHeight = 0;
        private int topMinHeight = 0;
        private int topGutterPixels = 5;
        private int topCollapseSize = 50;
        private bool topCollapse = true;
        private bool topScroll = false;
        private bool topResize = true;
        private bool topAnimate = false;
        private bool topClose = false;

        private string bottomControlId = string.Empty;
        private string bottomCaption = string.Empty;
        private string bottomFooter = string.Empty;
        private int bottomHeight = 100;
        private int bottomMaxHeight = 0;
        private int bottomMinHeight = 0;
        private int bottomGutterPixels = 5;
        private int bottomCollapseSize = 50;
        private bool bottomCollapse = true;
        private bool bottomScroll = false;
        private bool bottomResize = true;
        private bool bottomAnimate = false;
        private bool bottomClose = false;

        private string leftControlId = string.Empty;
        private string leftCaption = string.Empty;
        private string leftFooter = string.Empty;
        private int leftWidth = 200;
        private int leftMinWidth = 0;
        private int leftMaxWidth = 0;
        private int leftCollapseSize = 0;
        private int leftGutterPixels = 5;
        private bool leftCollapse = true;
        private bool leftResize = true;
        private bool leftScroll = true;
        private bool leftAnimate = true;
        private bool leftClose = true;

        private string rightControlId = string.Empty;
        private string rightCaption = string.Empty;
        private string rightFooter = string.Empty;
        private int rightWidth = 300;
        private int rightMinWidth = 0;
        private int rightMaxWidth = 0;
        private int rightCollapseSize = 0;
        private int rightGutterPixels = 5;
        private bool rightCollapse = true;
        private bool rightResize = true;
        private bool rightScroll = true;
        private bool rightAnimate = true;
        private bool rightClose = true;

        private string centerControlId = string.Empty;
        private string centerCaption = string.Empty;
        private string centerFooter = string.Empty;
        private int centerGutterPixels = 5;
        private bool centerScroll = true;

        private string closeText = string.Empty;
        private string collapseText = string.Empty;
        private string expandText = string.Empty;

        
        #endregion

        #region Public Properties

        public string CloseText
        {
            get { return closeText; }
            set { closeText = value; }
        }

        public string CollapseText
        {
            get { return collapseText; }
            set { collapseText = value; }
        }

        public string ExpandText
        {
            get { return expandText; }
            set { expandText = value; }
        }


        #region Top Panel

        public string TopControlId
        {
            get { return topControlId; }
            set { topControlId = value; }
        }

        public string TopCaption
        {
            get { return topCaption; }
            set { topCaption = value; }
        }

        public string TopFooter
        {
            get { return topFooter; }
            set { topFooter = value; }
        }

        public int TopHeight
        {
            get { return topHeight; }
            set { topHeight = value; }
        }

        public int TopMaxHeight
        {
            get { return topMaxHeight; }
            set { topMaxHeight = value; }
        }

        public int TopMinHeight
        {
            get { return topMinHeight; }
            set { topMinHeight = value; }
        }

        public int TopCollapseSize
        {
            get { return topCollapseSize; }
            set { topCollapseSize = value; }
        }

        public int TopGutterPixels
        {
            get { return topGutterPixels; }
            set { topGutterPixels = value; }
        }

        public bool TopCollapse
        {
            get { return topCollapse; }
            set { topCollapse = value; }
        }

        public bool TopResize
        {
            get { return topResize; }
            set { topResize = value; }
        }

        public bool TopAnimate
        {
            get { return topAnimate; }
            set { topAnimate = value; }
        }

        public bool TopClose
        {
            get { return topClose; }
            set { topClose = value; }
        }

        public bool TopScroll
        {
            get { return topScroll; }
            set { topScroll = value; }
        }

        #endregion

        #region Bottom Panel

        public string BottomControlId
        {
            get { return bottomControlId; }
            set { bottomControlId = value; }
        }

        public string BottomCaption
        {
            get { return bottomCaption; }
            set { bottomCaption = value; }
        }

        public string BottomFooter
        {
            get { return bottomFooter; }
            set { bottomFooter = value; }
        }

        public int BottomHeight
        {
            get { return bottomHeight; }
            set { bottomHeight = value; }
        }

        public int BottomMaxHeight
        {
            get { return bottomMaxHeight; }
            set { bottomMaxHeight = value; }
        }

        public int BottomMinHeight
        {
            get { return bottomMinHeight; }
            set { bottomMinHeight = value; }
        }

        public int BottomCollapseSize
        {
            get { return bottomCollapseSize; }
            set { bottomCollapseSize = value; }
        }

        public int BottomGutterPixels
        {
            get { return bottomGutterPixels; }
            set { bottomGutterPixels = value; }
        }

        public bool BottomCollapse
        {
            get { return bottomCollapse; }
            set { bottomCollapse = value; }
        }

        public bool BottomResize
        {
            get { return bottomResize; }
            set { bottomResize = value; }
        }

        public bool BottomAnimate
        {
            get { return bottomAnimate; }
            set { bottomAnimate = value; }
        }

        public bool BottomClose
        {
            get { return bottomClose; }
            set { bottomClose = value; }
        }

        public bool BottomScroll
        {
            get { return bottomScroll; }
            set { bottomScroll = value; }
        }

        #endregion

        #region Left Panel

        public string LeftControlId
        {
            get { return leftControlId; }
            set { leftControlId = value; }
        }

        public string LeftCaption
        {
            get { return leftCaption; }
            set { leftCaption = value; }
        }

        public string LeftFooter
        {
            get { return leftFooter; }
            set { leftFooter = value; }
        }

        public int LeftWidth
        {
            get { return leftWidth; }
            set { leftWidth = value; }
        }

        public int LeftMinWidth
        {
            get { return leftMinWidth; }
            set { leftMinWidth = value; }
        }

        public int LeftMaxWidth
        {
            get { return leftMaxWidth; }
            set { leftMaxWidth = value; }
        }

        public int LeftCollapseSize
        {
            get { return leftCollapseSize; }
            set { leftCollapseSize = value; }
        }

        public int LeftGutterPixels
        {
            get { return leftGutterPixels; }
            set { leftGutterPixels = value; }
        }

        public bool LeftCollapse
        {
            get { return leftCollapse; }
            set { leftCollapse = value; }
        }

        public bool LeftResize
        {
            get { return leftResize; }
            set { leftResize = value; }
        }

        public bool LeftScroll
        {
            get { return leftScroll; }
            set { leftScroll = value; }
        }

        public bool LeftAnimate
        {
            get { return leftAnimate; }
            set { leftAnimate = value; }
        }

        public bool LeftClose
        {
            get { return leftClose; }
            set { leftClose = value; }
        }

        #endregion

        #region Right Panel

        public string RightControlId
        {
            get { return rightControlId; }
            set { rightControlId = value; }
        }

        public string RightCaption
        {
            get { return rightCaption; }
            set { rightCaption = value; }
        }

        public string RightFooter
        {
            get { return rightFooter; }
            set { rightFooter = value; }
        }

        public int RightWidth
        {
            get { return rightWidth; }
            set { rightWidth = value; }
        }

        public int RightMinWidth
        {
            get { return rightMinWidth; }
            set { rightMinWidth = value; }
        }

        public int RightMaxWidth
        {
            get { return rightMaxWidth; }
            set { rightMaxWidth = value; }
        }

        public int RightCollapseSize
        {
            get { return rightCollapseSize; }
            set { rightCollapseSize = value; }
        }

        public int RightGutterPixels
        {
            get { return rightGutterPixels; }
            set { rightGutterPixels = value; }
        }

        public bool RightCollapse
        {
            get { return rightCollapse; }
            set { rightCollapse = value; }
        }

        public bool RightResize
        {
            get { return rightResize; }
            set { rightResize = value; }
        }

        public bool RightScroll
        {
            get { return rightScroll; }
            set { rightScroll = value; }
        }

        public bool RightAnimate
        {
            get { return rightAnimate; }
            set { rightAnimate = value; }
        }

        public bool RightClose
        {
            get { return rightClose; }
            set { rightClose = value; }
        }

        #endregion

        #region Center Panel

        public string CenterControlId
        {
            get { return centerControlId; }
            set { centerControlId = value; }
        }

        public string CenterCaption
        {
            get { return centerCaption; }
            set { centerCaption = value; }
        }

        public string CenterFooter
        {
            get { return centerFooter; }
            set { centerFooter = value; }
        }

        public int CenterGutterPixels
        {
            get { return centerGutterPixels; }
            set { centerGutterPixels = value; }
        }
        

        public bool CenterScroll
        {
            get { return centerScroll; }
            set { centerScroll = value; }
        }

        #endregion

        #endregion


        private void SetupScripts()
        {
            // center is required
            if (centerControlId.Length == 0) { return; }

            StringBuilder script = new StringBuilder();

            script.Append("\n<script type='text/javascript'>");

            if (collapseText.Length > 0)
            {
                script.Append("YAHOO.widget.LayoutUnit.prototype.STR_COLLAPSE = '" + Page.Server.HtmlEncode(collapseText) + "';");
            }
            else
            {
                script.Append("YAHOO.widget.LayoutUnit.prototype.STR_COLLAPSE = '" + Page.Server.HtmlEncode(Resource.YuiPanelCollapse) + "';");
            }

            if (closeText.Length > 0)
            {
                script.Append("YAHOO.widget.LayoutUnit.prototype.STR_CLOSE = '" + Page.Server.HtmlEncode(closeText) + "';");
            }
            else
            {
                script.Append("YAHOO.widget.LayoutUnit.prototype.STR_CLOSE = '" + Page.Server.HtmlEncode(Resource.YuiPanelClose) + "';");
            }

            if (expandText.Length > 0)
            {
                script.Append("YAHOO.widget.LayoutUnit.prototype.STR_EXPAND = '" + Page.Server.HtmlEncode(expandText) + "';");
            }
            else
            {
                script.Append("YAHOO.widget.LayoutUnit.prototype.STR_EXPAND = '" + Page.Server.HtmlEncode(Resource.YuiPanelExpand) + "';");
            }

            script.Append("(function() {");
            script.Append("var Dom = YAHOO.util.Dom,");
            script.Append("Event = YAHOO.util.Event;");

            script.Append("Event.onDOMReady(function() {");
            script.Append("var layout = new YAHOO.widget.Layout({");
            script.Append("units: [");

            SetupTop(script);
            SetupRight(script);
            SetupBottom(script);
            SetupLeft(script);
            SetupCenter(script);

            script.Append("]");
            script.Append("});");

            script.Append("layout.on('render', function() {");
            script.Append("layout.getUnitByPosition('left').on('close', function() {");
            script.Append("closeLeft();");
            script.Append("});");
            script.Append("});");

            script.Append("layout.render();");

            //script.Append("Event.on('tLeft', 'click', function(ev) {");
            //script.Append("Event.stopEvent(ev);");
            //script.Append("layout.getUnitByPosition('left').toggle();");
            //script.Append("});");

            //script.Append("Event.on('tRight', 'click', function(ev) {");
            //script.Append("Event.stopEvent(ev);");
            //script.Append("layout.getUnitByPosition('right').toggle();");
            //script.Append("});");

            //script.Append("Event.on('padRight', 'click', function(ev) {");
            //script.Append("Event.stopEvent(ev);");
            //script.Append("var pad = prompt('CSS gutter to apply: (\"2px\" or \"2px 4px\" or any combination of the 4 sides)', layout.getUnitByPosition('right').get('gutter'));");
            //script.Append("layout.getUnitByPosition('right').set('gutter', pad);");
            //script.Append("});");

            //script.Append("var closeLeft = function() {");
            //script.Append("var a = document.createElement('a');");
            //script.Append("a.href = '#';");
            //script.Append("a.innerHTML = 'Add Left Unit';");
            //script.Append("Dom.get('closeLeft').parentNode.appendChild(a);");
            //script.Append("Dom.setStyle('tLeft', 'display', 'none');");
            //script.Append("Dom.setStyle('closeLeft', 'display', 'none');");

            //script.Append("Event.on(a, 'click', function(ev) {");
            //script.Append("Event.stopEvent(ev);");
            //script.Append("Dom.setStyle('tLeft', 'display', 'inline');");
            //script.Append("Dom.setStyle('closeLeft', 'display', 'inline');");
            //script.Append("a.parentNode.removeChild(a);");
            //script.Append("layout.addUnit(layout.get('units')[3]);");
            //script.Append("layout.getUnitByPosition('left').on('close', function() {");
            //script.Append("closeLeft();");
            //script.Append("});");

            //script.Append("});");
            //script.Append("};");
            //script.Append("Event.on('closeLeft', 'click', function(ev) {");
            //script.Append("Event.stopEvent(ev);");
            //script.Append("layout.getUnitByPosition('left').close();");
            //script.Append("});");


            script.Append("});");
            script.Append("})();");



            script.Append("</script>");


            Page.ClientScript.RegisterStartupScript(typeof(Page), "yui-layout-setup", script.ToString());

        }

        private void SetupTop(StringBuilder script)
        {
            if (topControlId.Length == 0) { return; }
            Control c = Page.FindControl(topControlId);
            if (c == null) { return; }

            script.Append("{position:'top',body:'" + c.ClientID + "'");

            script.Append(",height:" + topHeight.ToString(CultureInfo.InvariantCulture));

            if (topCaption.Length > 0)
            {
                script.Append(",header:'" + topCaption + "'");
            }

            if (topFooter.Length > 0)
            {
                script.Append(",footer:'" + topFooter + "'");
            }

            

            script.Append(",gutter: '" + topGutterPixels.ToString(CultureInfo.InvariantCulture) + "px'");

            if (topCollapse)
            {
                script.Append(",collapse:true");

                if (topCollapseSize > 0)
                {
                    script.Append(",collapseSize:" + topCollapseSize.ToString(CultureInfo.InvariantCulture));
                }
            }
           
            if (topResize)
            {
                script.Append(",resize:true");
                if (topMinHeight > 0)
                {
                    script.Append(",minHeight:" + topMinHeight.ToString(CultureInfo.InvariantCulture));
                }

                if (topMaxHeight > 0)
                {
                    script.Append(",maxHeight:" + topMaxHeight.ToString(CultureInfo.InvariantCulture));
                }
            }

            if (topClose)
            {
                script.Append(",close:true");
            }

            if (topScroll)
            {
                script.Append(",scroll:true");
            }

            if (topAnimate)
            {
                script.Append(",animate:true");
            }

            script.Append("},");
        }

        private void SetupRight(StringBuilder script)
        {
            if (topControlId.Length == 0) { return; }
            Control c = Page.FindControl(rightControlId);
            if (c == null) { return; }

            script.Append("{position:'right',body:'" + c.ClientID + "'");

            if (rightCaption.Length > 0)
            {
                script.Append(",header:'" + rightCaption + "'");
            }

            if (rightFooter.Length > 0)
            {
                script.Append(",footer:'" + rightFooter + "'");
            }

            

            script.Append(",width:" + rightWidth.ToString(CultureInfo.InvariantCulture));
            script.Append(",gutter:'" + rightGutterPixels.ToString(CultureInfo.InvariantCulture) + "px'");

            if (rightCollapse)
            {
                script.Append(",collapse:true");

                if (rightCollapseSize > 0)
                {
                    script.Append(",collapseSize:" + rightCollapseSize.ToString(CultureInfo.InvariantCulture));
                }
            }

            if (rightScroll)
            {
                script.Append(",scroll:true");
            }

            if (rightAnimate)
            {
                script.Append(",animate:true");
            }

            if (rightResize)
            {
                script.Append(",resize:true");
                if (rightMinWidth > 0)
                {
                    script.Append(",minWidth:" + rightMinWidth.ToString(CultureInfo.InvariantCulture));
                }

                if (rightMaxWidth > 0)
                {
                    script.Append(",maxWidth:" + rightMaxWidth.ToString(CultureInfo.InvariantCulture));
                }
            }

            if (rightClose)
            {
                script.Append(",close:true");
            }

            script.Append("},");
        }

        private void SetupBottom(StringBuilder script)
        {
            if (topControlId.Length == 0) { return; }
            Control c = Page.FindControl(bottomControlId);
            if (c == null) { return; }

            script.Append("{position:'bottom',body:'" + c.ClientID + "'");

            if (bottomCaption.Length > 0)
            {
                script.Append(",header:'" + bottomCaption + "'");
            }

            if (bottomFooter.Length > 0)
            {
                script.Append(",footer:'" + bottomFooter + "'");
            }

            script.Append(",height:" + bottomHeight.ToString(CultureInfo.InvariantCulture));
            script.Append(",gutter:'" + bottomGutterPixels.ToString(CultureInfo.InvariantCulture) + "px'");

            if (bottomResize)
            {
                script.Append(",resize: true");
                if (bottomMinHeight > 0)
                {
                    script.Append(",minHeight:" + bottomMinHeight.ToString(CultureInfo.InvariantCulture));
                }

                if (bottomMaxHeight > 0)
                {
                    script.Append(",maxHeight:" + bottomMaxHeight.ToString(CultureInfo.InvariantCulture));
                }
            }
            else
            {
                script.Append(",resize: false");
            }

            if (bottomCollapse)
            {
                script.Append(",collapse:true");

                if (bottomCollapseSize > 0)
                {
                    script.Append(",collapseSize:" + bottomCollapseSize.ToString(CultureInfo.InvariantCulture));
                }
            }
            else
            {
                script.Append(",collapse:false");
            }

            if (bottomClose)
            {
                script.Append(",close:true");
            }

            if (bottomScroll)
            {
                script.Append(",scroll:true");
            }

            if (bottomAnimate)
            {
                script.Append(",animate:true");
            }


            script.Append("},");

        }

        private void SetupLeft(StringBuilder script)
        {
            if (topControlId.Length == 0) { return; }
            Control c = Page.FindControl(leftControlId);
            if (c == null) { return; }

            script.Append("{position:'left',body:'" + c.ClientID + "'");

            if (leftCaption.Length > 0)
            {
                script.Append(",header:'" + leftCaption + "'");
            }

            if (leftFooter.Length > 0)
            {
                script.Append(",footer:'" + leftFooter + "'");
            }

            script.Append(",width:" + leftWidth.ToString(CultureInfo.InvariantCulture));
            
            script.Append(",gutter:'" + leftGutterPixels.ToString(CultureInfo.InvariantCulture) + "px'");
            if (leftCollapse)
            {
                script.Append(",collapse:true");

                if (leftCollapseSize > 0)
                {
                    script.Append(",collapseSize:" + leftCollapseSize.ToString(CultureInfo.InvariantCulture));
                }
            }
           
            if (leftClose)
            {
                script.Append(",close:true");
            }
           
            if (leftResize)
            {
                script.Append(",resize:true");
                if (leftMinWidth > 0)
                {
                    script.Append(",minWidth:" + leftMinWidth.ToString(CultureInfo.InvariantCulture));
                }

                if (leftMaxWidth > 0)
                {
                    script.Append(",maxWidth:" + leftMaxWidth.ToString(CultureInfo.InvariantCulture));
                }
            }

            if (leftScroll)
            {
                script.Append(",scroll:true");
            }

            if (leftAnimate)
            {
                script.Append(",animate:true");
            }


            script.Append("},");

        }

        private void SetupCenter(StringBuilder script)
        {
            if (topControlId.Length == 0) { return; }
            Control c = Page.FindControl(centerControlId);
            if (c == null) { return; }

            script.Append("{ position:'center',body:'" + c.ClientID + "'");
            script.Append(",gutter:'" + centerGutterPixels.ToString(CultureInfo.InvariantCulture) + "px'");

            if (centerCaption.Length > 0)
            {
                script.Append(",header:'" + centerCaption + "'");
            }

            if (centerFooter.Length > 0)
            {
                script.Append(",footer:'" + centerFooter + "'");
            }

            if (centerScroll)
            {
                script.Append(",scroll:true");
            }

            script.Append("}");

        }


        protected override void OnPreRender(System.EventArgs e)
        {
            base.OnPreRender(e);

            SetupScripts();
        }


        protected override void Render(HtmlTextWriter writer)
        {
            if (this.Site != null && this.Site.DesignMode)
            {
                // TODO: show a bmp or some other design time thing?
                writer.Write("[" + this.ID + "]");
            }
            else
            {
                // don't render anything, just setup the scripts
            }

        }

    }
}

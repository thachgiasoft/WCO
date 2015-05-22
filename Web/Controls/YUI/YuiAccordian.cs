//	Created:			    2009-04-21
//	Last Modified:		    2009-04-22
// 
// The use and distribution terms for this software are covered by the 
// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)
// which can be found in the file CPL.TXT at the root of this distribution.
// By using this software in any fashion, you are agreeing to be bound by 
// the terms of this license.
//
// You must not remove this notice, or any other, from this software.	

using System;
using System.Globalization;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mojoPortal.Web.UI
{
    /// <summary>
    /// These YUI controls are no longer used in mojoPortal and no longer being maintained, we have standardized on jQuery
    /// 
    /// Requires YUI Utilities
    /// Requires reset-fonts.css
    /// Requires accordianview.css
    /// Requires accordianview-min.js
    /// </summary>
    [Obsolete("These YUI controls are no longer used in mojoPortal and no longer being maintained, we have standardized on jQuery")]
    public class YuiAccordian : WebControl
    {
        //References
        //http://gallery.yahoo.com/apps/21293
        //http://www.i-marco.nl/weblog/yui-accordion/


        private string controlId = string.Empty;
        private int expandItemIndex = -1;
        private string animationSpeed = "0.7";
        private string effect = "YAHOO.util.Easing.easeBothStrong"; //http://developer.yahoo.com/yui/docs/YAHOO.util.Easing.html
        private bool animate = true;
        private bool collapsible = true; //whether the whole thing can be collapsed
        private bool expandable = false; //whether the whole thing can expand 
        private bool hoverActivated = false; //when set to true, the items are activated on hover. Note that this activates on click ALSO in order to keep keyboard navigation working
        private string hoverTimeout = "500ms";

        #region Public Properties

        /// <summary>
        /// This should be a<ul runat="server"
        /// </summary>
        public string ControlId
        {
            get { return controlId; }
            set { controlId = value; }
        }

        public int ExpandItemIndex
        {
            get { return expandItemIndex; }
            set { expandItemIndex = value; }
        }

        public string AnimationSpeed
        {
            get { return animationSpeed; }
            set { animationSpeed = value; }
        }

        public string Effect
        {
            get { return effect; }
            set { effect = value; }
        }

        public bool Animate
        {
            get { return animate; }
            set { animate = value; }
        }

        public bool Collapsible
        {
            get { return collapsible; }
            set { collapsible = value; }
        }

        public bool Expandable
        {
            get { return expandable; }
            set { expandable = value; }
        }

        //public bool HoverActivated
        //{
        //    get { return hoverActivated; }
        //    set { hoverActivated = value; }
        //}

        #endregion


        private void SetupScripts()
        {
            if (controlId.Length == 0) { return; }

            StringBuilder script = new StringBuilder();

            script.Append("\n<script type='text/javascript'>");

            script.Append("\nvar acm" + controlId + " = new YAHOO.widget.AccordionView('" + controlId + "', ");
            script.Append("{");
            if (collapsible)
            {
                script.Append("collapsible:true");
            }
            else
            {
                script.Append("collapsible:false");
            }

            script.Append(",width:'" + Width.ToString() +"'");

            if (expandItemIndex > -1)
            {
                script.Append(",expandItem:" + expandItemIndex.ToString(CultureInfo.InvariantCulture));
            }

            if (animate)
            {
                script.Append(",animate: true");
                script.Append(",animationSpeed:" + animationSpeed);
                script.Append(",effect:" + effect);
            }

            if (hoverActivated)
            {
                script.Append(",hoverActivated:true");
                script.Append(",hoverTimeout:" + hoverTimeout);
            }

            if (expandable)
            {
                script.Append(",expandable: true ");
            }

            script.Append("}");
            script.Append(");");

            //script.Append("acm" + controlId + ".");

            script.Append("\n</script>");


            Page.ClientScript.RegisterStartupScript(typeof(Page), "yui-accordion" + this.ClientID, script.ToString());

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

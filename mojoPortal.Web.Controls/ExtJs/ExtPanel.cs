// Author:					Joe Audette
// Created:				2007-11-01
// Last Modified:			2007-11-02
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

namespace mojoPortal.Web.Controls.ExtJs
{
    /// <summary>
    /// These ExtJs controls are no longer used in mojoPortal and no longer being maintained, we have standardized on jQuery
    /// </summary>
    [Obsolete("These ExtJs controls are no longer used in mojoPortal and no longer being maintained, we have standardized on jQuery")]
    public class ExtPanel : Container
    {
        private bool animCollapse = true;
        private string autoLoad = string.Empty;
        private bool autoScroll = false;
        private bool border = true;
        private bool collapseFirst = true;
        private bool collapsed = false;
        private bool collapsible = false;
        private bool header = false;
        private bool headerAsText = true;
        private string title = string.Empty;
        private string layout = string.Empty;
        private string region = string.Empty;
        private bool frame = false;
        private string iconCls = string.Empty;
        private string ctCls = string.Empty;

        private bool usePosition = false;
        private int left = 0;
        private int top = 100;


        
      
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
        /// 
        /// </summary>
        public bool Header
        {
            get { return header; }
            set { header = value; }
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
        /// An optional extra CSS class that will be added to this component's container (defaults to ''). 
        /// This can be useful for adding customized styles to the container or any of its children 
        /// using standard CSS rules.
        /// </summary>
        public string CtCls
        {
            get { return ctCls; }
            set { ctCls = value; }
        }

        /// <summary>
        /// True to display the borders of the panel's body element, false to hide them (defaults to true). By default, the border is a 2px wide inset border, but this can be further altered by setting bodyBorder to false.
        /// </summary>
        public bool Border
        {
            get { return border; }
            set { border = value; }
        }

        public bool UsePosition
        {
            get { return usePosition; }
            set { usePosition = value; }
        }

        public int Left
        {
            get { return left; }
            set { left = value; }
        }

        public int Top
        {
            get { return top; }
            set { top = value; }
        }


        public override void RenderControlToScript(StringBuilder script)
        {
           
            if (DidRenderScript) return;

            script.Append(" {  ");
            script.Append("contentEl:'" + this.ClientID + "'  ");
            script.Append(", title: '" + this.Title + "' ");

            if (region.Length > 0)
            {
                script.Append(", region: '" + region + "'");
            }

            if (layout.Length > 0)
            {
                script.Append(", layout: '" + layout + "'");
            }

            if (ctCls.Length > 0)
            {
                script.Append(", ctCls: '" + ctCls + "'");
            }
            
            if ((AutoHeight) && (FixedPixelHeight == 0))
            {
                script.Append(", autoHeight:true  ");
            }

            if (FixedPixelHeight > 0)
            {
                script.Append(", height: '" + FixedPixelHeight.ToString() + "'");
            }

            if ((AutoWidth) && (FixedPixelWidth == 0))
            {
                script.Append(", autoWidth:true  ");
            }

            if (FixedPixelWidth > 0)
            {
                script.Append(", width: " + FixedPixelWidth.ToString());
            }

            if (collapsible)
            {
                script.Append(", collapsible: true ");

            }

            if (AutoScroll)
            {
                script.Append(", autoScroll: true ");
            }

            if (IconCls.Length > 0)
            {
                script.Append(", iconCls: '" + IconCls + "' ");
            }

            if (!Border)
            {
                script.Append(", border: false ");
            }

            script.Append(" } ");

            DidRenderScript = true;


        }
        


    }
}

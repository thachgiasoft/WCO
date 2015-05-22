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
    public class Container : ExtBasePanel
    {

        private bool autoDestroy = true;
        private bool autoHeight = false;
        private bool autoWidth = false;
        private bool autoShow = false;
        private string cls = string.Empty;
        private string disabledClass = string.Empty;
        private int fixedPixelHeight = 0;
        private int fixedPixelWidth = 0;
        private bool hideBorders = false;
        

        
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


        public override void RenderControlToScript(StringBuilder script)
        {
            

        }
        

    }
}

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
    ///	 These ExtJs controls are no longer used in mojoPortal and no longer being maintained, we have standardized on jQuery
    /// </summary>
    [Obsolete("These ExtJs controls are no longer used in mojoPortal and no longer being maintained, we have standardized on jQuery")]
    public class SplitPanel : ExtPanel
    {
        private bool split = false;
        private int minSize = 0;
        private int maxSize = 0;
        private bool animateLayout = false;


        public bool AnimateLayout
        {
            get { return animateLayout; }
            set { animateLayout = value; }
        }

        public bool Split
        {
            get { return split; }
            set { split = value; }
        }

        public int MinSize
        {
            get { return minSize; }
            set { minSize = value; }
        }

        public int MaxSize
        {
            get { return maxSize; }
            set { maxSize = value; }
        }

        public override void RenderControlToScript(StringBuilder script)
        {
            bool useApplyTo = false;
            RenderControlToScript(script, useApplyTo);
        }

        public override void RenderControlToScript(StringBuilder script, bool useApplyTo)
        {
           
            if (DidRenderScript) return;

            if (RenderConstructor)
                script.Append("new Ext.Panel(");

            script.Append(" {  ");

            if (useApplyTo)
                script.Append("applyTo:'" + this.ClientID + "'");
            else
            script.Append("contentEl:'" + this.ClientID + "'  ");


            script.Append(", title: '" + this.Title + "' ");

            if (Region.Length > 0)
            {
                script.Append(", region: '" + Region + "'");
            }

            if (Layout.Length > 0)
            {
                script.Append(", layout: '" + Layout + "'");
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

            if (Collapsible)
            {
                script.Append(", collapsible: true ");

            }

            if (AutoScroll)
            {
                script.Append(", autoScroll: true ");
            }

            if (split)
            {
                script.Append(", split: true ");

                if (minSize > 0)
                {
                    script.Append(", minSize: " + minSize.ToString());

                }

                if (maxSize > 0)
                {
                    script.Append(", maxSize: " + maxSize.ToString());

                }

            }

            if (AnimateLayout)
            {
                script.Append(", layoutConfig:{ animate:true }");

            }

            bool hasChildren = HasVisibleExtChildren();

            if (hasChildren)
            {
                script.Append("\n ,items:[");

                AddItems(script);

                script.Append(" ]  ");

            }

            script.Append(" } ");

            if (RenderConstructor)
                script.Append(")");

            DidRenderScript = true;


        }

    }
}

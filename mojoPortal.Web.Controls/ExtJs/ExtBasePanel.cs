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
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mojoPortal.Web.Controls.ExtJs
{
    /// <summary>	
    /// These ExtJs controls are no longer used in mojoPortal and no longer being maintained, we have standardized on jQuery
    /// This is just a base class for other panel based controls, it is not meant to be used directly.
    /// </summary>
    [Obsolete("These ExtJs controls are no longer used in mojoPortal and no longer being maintained, we have standardized on jQuery")]
    public class ExtBasePanel : Panel
    {
        protected bool DidRenderScript = false;
        protected bool UnMinify = false;
        public bool RenderConstructor = false;

        

        virtual public void RenderControlToScript(StringBuilder script)
        {


        }

        virtual public void RenderControlToScript(StringBuilder script, bool useApplyTo)
        {


        }

        protected bool HasVisibleExtChildren()
        {
           
            foreach (Control c in this.Controls)
            {
                if ((c is ExtBasePanel) && (c.Visible))
                {
                    return true;
                }

            }

            return false;

        }

        protected void AddItems(StringBuilder script)
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

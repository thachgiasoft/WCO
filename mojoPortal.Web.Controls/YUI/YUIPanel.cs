//	Created:			    2008-09-10
//	Last Modified:		    2008-09-10
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
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mojoPortal.Web.Controls
{
    /// <summary>
    /// These YUI controls are no longer used in mojoPortal and no longer being maintained, we have standardized on jQuery
    /// The purpose of this control is just to provide a convenient panel that adds the css class to the YUI skin
    /// </summary>
    [Obsolete("These YUI controls are no longer used in mojoPortal and no longer being maintained, we have standardized on jQuery")]
    public class YUIPanel : Panel
    {
        private string skinName = "yui-skin-sam"; 

        public string SkinName
        {
            get { return skinName; }
            set { skinName = value; }
        }

        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);

            

        }

        protected override void OnPreRender(System.EventArgs e)
        {
            base.OnPreRender(e);

            if (this.CssClass.Length > 0)
            {
                this.CssClass += " " + skinName;
            }
            else
            {
                this.CssClass = skinName;
            }


        }
    }
}

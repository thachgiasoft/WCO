// Author:					Joe Audette
// Created:					2010-12-11
// Last Modified:			2013-09-17
// 
// The use and distribution terms for this software are covered by the 
// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)  
// which can be found in the file CPL.TXT at the root of this distribution.
// By using this software in any fashion, you are agreeing to be bound by 
// the terms of this license.
//
// You must not remove this notice, or any other, from this software.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mojoPortal.Web.Modules
{
    public partial class TuyenModule1 : SiteModuleControl
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadSettings();
            PopulateLabels();
            if (!Page.IsPostBack)
            {
                PopulateControls();
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
        }
        private void PopulateControls()
        {
        }
        private void PopulateLabels()
        {
        }
        private void LoadSettings()
        {
            // TODO: if your feature has an edit page link to it here
            //Title1.EditUrl = SiteRoot + "/MyClassEdit.aspx";
            //Title1.EditText = 
            Title1.Visible = !this.RenderInWebPartMode;
            if (this.ModuleConfiguration != null)
            {
                this.Title = this.ModuleConfiguration.ModuleTitle;
                this.Description = this.ModuleConfiguration.FeatureName;
            }
        }
    }
}

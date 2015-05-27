using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

    namespace mojoPortal.Web.Modules
{
    public partial class NewCar : SiteModuleControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadSettings();
        }

        private void LoadSettings()
        {

            // TODO: if your feature has an edit page link to it here
            //Title1.EditUrl = SiteRoot + "/MyClassEdit.aspx";
            //Title1.EditText = 

            //Title1.Visible = !this.RenderInWebPartMode;
            if (this.ModuleConfiguration != null)
            {
                this.Title = this.ModuleConfiguration.ModuleTitle;
                this.Description = this.ModuleConfiguration.FeatureName;
            }

        }
    }
}
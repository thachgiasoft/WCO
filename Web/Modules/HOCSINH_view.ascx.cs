using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mojoPortal.Web.Framework;
using t;

namespace mojoPortal.Web.Modules	
	{	
	    public partial class HOCSINH_VIEW : SiteModuleControl	
	    {	
	        //protected override void OnInit(EventArgs e)	
	        //{	
	        //    base.OnInit(e);	
	        //}	
		
	        protected void Page_Load(object sender, EventArgs e)	
	        {
	            LoadSettings();	
	            PopulateLabels();	
	            if (!Page.IsPostBack)	
	            {	
	                PopulateControls();	
	            }

                if(Page.IsPostBack==false) LoadData();
            }


        private void LoadData()
        {
            int intHS_ID = WebUtils.ParseInt32FromQueryString("HS_ID", -1);          
            if (intHS_ID < 0) return;
            HOCSINH o = default(HOCSINH);
            using (tDBContext mainDB = new tDBContext())
            {
                string strWhere = string.Format("HS_ID={0}", intHS_ID);
                o = mainDB.HOCSINHs.GetObject(strWhere);
            }
            if (o == null) return;
            clsAll.BindData(pnlInnerBody, o);
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
	            Title1.Visible = !this.RenderInWebPartMode;	
	            if (this.ModuleConfiguration != null)	
	            {	
	                this.Title = this.ModuleConfiguration.ModuleTitle;	
	                this.Description = this.ModuleConfiguration.FeatureName;	
	            }	
	        }	
	    }	
	}	

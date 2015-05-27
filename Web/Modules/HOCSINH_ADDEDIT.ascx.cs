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
	    public partial class HOCSINH_ADDEDIT : SiteModuleControl	
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
	             if(Page.IsPostBack==false) LoadData();	
	        }	
	        private void LoadData()	
	        {	
	            int intKEY = WebUtils.ParseInt32FromQueryString("HS_ID", -1);
	            if (intKEY < 0) return;	
	            HOCSINH o = default(HOCSINH);	
	            using (tDBContext mainDB = new tDBContext())	
	            {	
	                string strWhere = string.Format("HS_ID ={0}", intKEY);	
	                o = mainDB.HOCSINHs.GetObject(strWhere);	
	            }	
	            if (o == null) return;	
	            clsAll.BindData(pnlInnerBody, o);	
	        }    	
	  protected void btnGhi_Click(object sender, EventArgs e)	 
	            {	 
	                Page.Validate("GHIDULIEU");	 
	                if (Page.IsValid == false)	 
	                {	 
	                    lblMSG.Text = "Có một số dữ liệu chưa đúng. Vui lòng xem lại dữ liệu.";	 
	                    return;	 
	                }	 
	                try	 
	                {	 
	                    HOCSINH o = new HOCSINH();	 
	                    clsAll.CopyData(pnlInnerBody, o);	 
	                    using (tDBContext mainDB = new tDBContext())	 
	                    {	 
	                        if (o.HS_ID < 0)//them moi	 
	                        {	 
	                            string strMaxKey = mainDB.HOCSINHs.Max("HS_ID");	 
	                            if (string.IsNullOrEmpty(strMaxKey) == true) strMaxKey = "0";	 
	                            int intMaxKey = Convert.ToInt32(strMaxKey) + 1;	 
	                            o.HS_ID = intMaxKey;	 
	                            o.DataStatus = DBStatus.Inserted;	 
	                            mainDB.HOCSINHs.InsertOnSubmit(o);	 
	                        }	 
	                        else//o.HS_ID >= 0 update	 
	                        {	 
	                            o.DataStatus = DBStatus.Updated;	 
	                            mainDB.HOCSINHs.UpdateOnSubmit(o);	 
	                        }	 
	                        mainDB.SubmitAllChange();	 
	                    }	 
	                    HS_ID.Text = string.Format("{0}",o.HS_ID);	 
	                    lblMSG.Text = string.Format("Ghi thành công lúc: {0}h{1} {2}giây",DateTime.Now.Hour,DateTime.Now.Minute,DateTime.Now.Second);	 
	                }	 
	                catch (Exception ex)	 
	                {	 
	                    lblMSG.Text = string.Format("Xảy ra lỗi: {0}",ex.Message);	 
	                }	 
	            }	 
	        private void PopulateControls()	
	        {	
	        }	
	        private void PopulateLabels()	
	        {	
	        lblMSG.Text = "";	
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


<%@ Control Language="C#" ClassName="HelloMojoModule.ascx" Inherits="mojoPortal.Web.SiteModuleControl"   %>
<%@ Register Assembly="mojoPortal.Web.Controls" Namespace="mojoPortal.Web.Controls" TagPrefix="cc2" %>
<%@ Register Assembly="mojoPortal.Web" Namespace="mojoPortal.Web.UI" TagPrefix="cc1" %>
<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="mojoPortal.Business" %>
<%@ Import Namespace="mojoPortal.Business.WebHelpers" %>
<%@ Import Namespace="mojoPortal.Web.Framework" %>
<%@ Import Namespace="mojoPortal.Web" %>
<%@ Import Namespace="mojoPortal.Web.Controls" %>
<%@ Import Namespace="mojoPortal.Web.UI" %>
<%@ Import Namespace="mojoPortal.Web.Editor" %>
<%@ Import Namespace="mojoPortal.Net" %>

<script runat="server">
   
	
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
       
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      
        LoadSettings();
        PopulateLabels();
        if(!Page.IsPostBack)
        {
        	PopulateControls();
        }
        
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        TextBox1.Text = "Hello Web I'm a SiteModule and my ModuleID is " + this.ModuleId.ToString(CultureInfo.InvariantCulture);
    }
	

    private void PopulateControls()
    {
		TextBox1.Text = "Click the button";

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

</script>

<cc1:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
<cc2:CornerRounderTop id="ctop1" runat="server" />
<cc1:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper mymodule">
<asp:UpdatePanel ID="upGallery" UpdateMode="Conditional" runat="server">
<ContentTemplate>
<cc1:ModuleTitleControl id="Title1" runat="server" />
<cc1:OuterBodyPanel ID="pnlOuterBody" runat="server">
<cc1:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">

Your custom form goes here.
<asp:TextBox ID="TextBox1" runat="server" CssClass="widetextbox"></asp:TextBox>
<cc1:mojoButton ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
    
</cc1:InnerBodyPanel>
</cc1:OuterBodyPanel>
</ContentTemplate>
</asp:UpdatePanel>
<cc1:EmptyPanel id="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></cc1:EmptyPanel>
</cc1:InnerWrapperPanel>
<cc2:CornerRounderBottom id="cbottom1" runat="server" />
</cc1:OuterWrapperPanel>

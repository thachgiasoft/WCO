
<%@ Control Language="C#" ClassName="HelloMojoModule.ascx" Inherits="mojoPortal.Web.SiteModuleControl"   %>
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

<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
<mp:CornerRounderTop id="ctop1" runat="server" />
<portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper mymodule">
<asp:UpdatePanel ID="upGallery" UpdateMode="Conditional" runat="server">
<ContentTemplate>
<portal:ModuleTitleControl id="Title1" runat="server" />
<portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
<portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">

Your custom form goes here.
<asp:TextBox ID="TextBox1" runat="server" CssClass="widetextbox"></asp:TextBox>
<portal:mojoButton ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />

</portal:InnerBodyPanel>
</portal:OuterBodyPanel>
</ContentTemplate>
</asp:UpdatePanel>
<portal:EmptyPanel id="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
</portal:InnerWrapperPanel>
<mp:CornerRounderBottom id="cbottom1" runat="server" />
</portal:OuterWrapperPanel>

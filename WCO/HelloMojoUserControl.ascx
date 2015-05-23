
<%@ Control Language="C#" AutoEventWireup="true" ClassName="HelloMojoModule.ascx" Inherits="System.Web.UI.UserControl"   %>
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
        TextBox1.Text = "Hello Web I'm a plain old UserControl ";
    }

    void chkAutoPostBackTest_CheckedChanged(object sender, EventArgs e)
    {
        
        lblCheckboxResult.Text = "You posted back.";
        
        
    }

    void ddColors_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblDropDownResult.Text = ddColors.SelectedValue;
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

		

    }

</script>

<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
<mp:CornerRounderTop id="ctop1" runat="server" />
<portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper mymodule">
<asp:UpdatePanel ID="upGallery" UpdateMode="Conditional" runat="server">
<ContentTemplate>
<portal:HeadingControl ID="heading" runat="server" Text="Your Title" />
<portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
<portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">

<div class="settingrow">
<asp:CheckBox ID="chkAutoPostBackTest" runat="server" Text="Auto Postback" AutoPostBack="true" OnCheckedChanged="chkAutoPostBackTest_CheckedChanged"  />
<asp:Label ID="lblCheckboxResult" runat="server" />
</div>
<div class="settingrow">
AutoPostBack Dropdown
<asp:DropDownList ID="ddColors" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddColors_SelectedIndexChanged">
<asp:ListItem Text="Blue" Value="Blue"></asp:ListItem>
<asp:ListItem Text="Green" Value="Green"></asp:ListItem>
<asp:ListItem Text="Red" Value="Red"></asp:ListItem>
<asp:ListItem Text="Yellow" Value="Yellow"></asp:ListItem>
</asp:DropDownList>
<asp:Label ID="lblDropDownResult" runat="server" />
</div>
<div class="settingrow">
<asp:TextBox ID="TextBox1" runat="server" CssClass="widetextbox"></asp:TextBox>
<portal:mojoButton ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
</div>


</portal:InnerBodyPanel>
</portal:OuterBodyPanel>
</ContentTemplate>
</asp:UpdatePanel>
<portal:EmptyPanel id="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
</portal:InnerWrapperPanel>
<mp:CornerRounderBottom id="cbottom1" runat="server" />
</portal:OuterWrapperPanel>


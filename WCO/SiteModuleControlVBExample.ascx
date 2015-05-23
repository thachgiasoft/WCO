
<%@ Page Language="VB" ClassName="MyCustomVBSiteModule.ascx" Inherits="mojoPortal.Web.SiteModuleControl"   %>

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
    
Sub Page_Load()

LoadSettings()
PopulateLabels()
PopulateControls()

End Sub  

    
Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs)
    
        TextBox1.Text = "Hello Web"
End Sub
	
Sub PopulateControls()
    
TextBox1.Text = "Click the button"

End Sub


Sub PopulateLabels()
    

End Sub

Sub LoadSettings()
    
        Title1.Visible = !this.RenderInWebPartMode
        If this.ModuleConfiguration != null Then
            this.Title = this.ModuleConfiguration.ModuleTitle
            this.Description = this.ModuleConfiguration.FeatureName
        End If

End Sub

</script>

<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
<mp:CornerRounderTop id="ctop1" runat="server" />
<portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper mymodule">
<portal:ModuleTitleControl id="Title1" runat="server" />
<portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
<portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">

Your custom form goes here.
<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />

</portal:InnerBodyPanel>
</portal:OuterBodyPanel>
<portal:EmptyPanel id="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
</portal:InnerWrapperPanel>
<mp:CornerRounderBottom id="cbottom1" runat="server" />
</portal:OuterWrapperPanel>

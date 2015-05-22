<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="TuyenModule1.ascx.cs" Inherits="mojoPortal.Web.Modules.TuyenModule1" %>
<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="mojoPortal.Business" %>
<%@ Import Namespace="mojoPortal.Business.WebHelpers" %>
<%@ Import Namespace="mojoPortal.Web.Framework" %>
<%@ Import Namespace="mojoPortal.Web" %>
<%@ Import Namespace="mojoPortal.Web.Controls" %>
<%@ Import Namespace="mojoPortal.Web.UI" %>
<%@ Import Namespace="mojoPortal.Web.Editor" %>
<%@ Import Namespace="mojoPortal.Net" %>

<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
<mp:CornerRounderTop id="ctop1" runat="server" />
<portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper mymodule">
<asp:UpdatePanel ID="upGallery" UpdateMode="Conditional" runat="server">
<ContentTemplate>
<portal:ModuleTitleControl id="Title1" runat="server" />
<portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
<portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">

<h2>Chức năng abc</h2>

    
    <portal:mojoLabel ID="MojoLabel1" runat="server" Text="Trường 1:"></portal:mojoLabel>
    <portal:mojoLabel ID="MojoLabel2" runat="server" Text="Giá trị 1"></portal:mojoLabel>
    <br />
    <portal:mojoLabel ID="MojoLabel3" runat="server" Text="Trường 2:"></portal:mojoLabel>
    <portal:mojoLabel ID="MojoLabel4" runat="server" Text="Giá trị 2"></portal:mojoLabel>

    <br /><br />
    <asp:HyperLink ID="linkTroLaiDanhSach" runat="server" NavigateUrl="~/Modules/TuyenModule1.ascx">Trở lại</asp:HyperLink>



</portal:InnerBodyPanel>
</portal:OuterBodyPanel>
</ContentTemplate>
</asp:UpdatePanel>
<portal:EmptyPanel id="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
</portal:InnerWrapperPanel>
<mp:CornerRounderBottom id="cbottom1" runat="server" />
</portal:OuterWrapperPanel>


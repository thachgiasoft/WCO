﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HOCSINH_ADDEDIT.ascx.cs" Inherits="mojoPortal.Web.Modules.HOCSINH_ADDEDIT" %>
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
<ContentTemplate >
<portal:ModuleTitleControl id="Title1" runat="server" />
<portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
<portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
<h2>Chi tiết HOCSINH</h2>
<portal:mojoLabel ID="lblHS_ID" runat="server" Text="ID học sinh:" ></portal:mojoLabel>
<portal:mojoLabel ID="HS_ID" runat="server" Text="-1" ></portal:mojoLabel> <br />
<portal:mojoLabel ID="lblHS_HOTEN" runat="server" Text="Họ và tên:" ></portal:mojoLabel>
<mp:WatermarkTextBox ID="HS_HOTEN" runat="server" Watermark="Họ và tên:"></mp:WatermarkTextBox> <br />
<portal:mojoLabel ID="lblHS_TUOI" runat="server" Text="Tuổi:" ></portal:mojoLabel>
<mp:WatermarkTextBox ID="HS_TUOI" runat="server" Watermark="Tuổi:"></mp:WatermarkTextBox> <br />
<ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True" TargetControlID="HS_TUOI" ID="HS_TUOI_FilteredTextBoxExtender" FilterType="Numbers, Custom" ValidChars=".,"></ajaxToolkit:FilteredTextBoxExtender> <br />
<portal:mojoLabel ID="lblHS_NGAYSINH" runat="server" Text="Ngày sinh:" ></portal:mojoLabel>
<portal:jDatePicker ID="HS_NGAYSINH" runat="server"></portal:jDatePicker><br/>
<br /><br /><asp:HyperLink ID="linkTroLaiDanhSach" runat="server" NavigateUrl="~/HOCSINH_LIST.aspx">Trở lại</asp:HyperLink>
</portal:InnerBodyPanel>
</portal:OuterBodyPanel>
</ContentTemplate>
</asp:UpdatePanel>
<portal:EmptyPanel id="divCleared" runat="server" CssClass="cleared" SkinID="cleared" ></portal:EmptyPanel>
</portal:InnerWrapperPanel>
<mp:CornerRounderBottom id="cbottom1" runat="server" />
</portal:OuterWrapperPanel>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HOCSINH_ADDEDIT.ascx.cs" Inherits="mojoPortal.Web.Modules.HOCSINH_ADDEDIT" %>
<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="mojoPortal.Business" %>
<%@ Import Namespace="mojoPortal.Business.WebHelpers" %>
<%@ Import Namespace="mojoPortal.Web.Framework" %>
<%@ Import Namespace="mojoPortal.Web" %>
<%@ Import Namespace="mojoPortal.Web.Controls" %>
<%@ Import Namespace="mojoPortal.Web.UI" %>
<%@ Import Namespace="mojoPortal.Web.Editor" %>
<%@ Import Namespace="mojoPortal.Net" %>
<portal:OuterWrapperPanel ID="pnlOuterWrap"   runat="server">
<mp:CornerRounderTop id="ctop1" runat="server" />
<portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper mymodule">
<asp:UpdatePanel ID="upGallery" UpdateMode="Conditional" runat="server">
<ContentTemplate >
<portal:ModuleTitleControl id="Title1" runat="server" />
<portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
<portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
<h2>Chi tiết HOCSINH</h2>
<portal:mojoLabel ID="lblHS_ID" runat="server" Text="HS ID:" ></portal:mojoLabel>
<portal:mojoLabel ID="HS_ID" runat="server" Text="-1" ></portal:mojoLabel> <br />
<portal:mojoLabel ID="lblHS_HOTEN" runat="server" Text="Họ tên:" ></portal:mojoLabel>
<mp:WatermarkTextBox ID="HS_HOTEN" runat="server" Watermark="Họ tên:" UseWatermark="False" ValidationGroup="GHIDULIEU" Width="200px"></mp:WatermarkTextBox> 
<asp:RequiredFieldValidator ID="rHS_HOTEN" ControlToValidate="HS_HOTEN" ErrorMessage="Họ tên: bắt buộc nhập" Font-Bold="True" ForeColor="Red" ToolTip="Dữ liệu bắt buộc nhập" ValidationGroup="GHIDULIEU" runat="server" Display="Dynamic" >*</asp:RequiredFieldValidator>
<br />
    <portal:mojoLabel ID="lblHS_TUOI" runat="server" Text="Tuổi:"></portal:mojoLabel>
<mp:WatermarkTextBox ID="HS_TUOI" runat="server" Watermark="Tuổi:" UseWatermark="False" ValidationGroup="GHIDULIEU" Width="200px"></mp:WatermarkTextBox> 
<ajaxToolkit:FilteredTextBoxExtender ValidChars=".," runat="server" Enabled="True" TargetControlID="HS_TUOI" ID="HS_TUOI_FilteredTextBoxExtender" FilterType="Numbers, Custom"></ajaxToolkit:FilteredTextBoxExtender> 
<asp:RequiredFieldValidator ID="rHS_TUOI" ControlToValidate="HS_TUOI" ErrorMessage="Tuổi: bắt buộc nhập" Font-Bold="True" ForeColor="Red" ToolTip="Dữ liệu bắt buộc nhập" ValidationGroup="GHIDULIEU" runat="server" Display="Dynamic" >*</asp:RequiredFieldValidator>
<br />
<portal:mojoLabel ID="lblHS_NGAYSINH" runat="server" Text="Ngày sinh:" ></portal:mojoLabel>
<portal:jDatePicker ID="HS_NGAYSINH" runat="server"></portal:jDatePicker>
<asp:RequiredFieldValidator ID="rHS_NGAYSINH" ControlToValidate="HS_NGAYSINH" ErrorMessage="Ngày sinh: bắt buộc nhập" Font-Bold="True" ForeColor="Red" ToolTip="Dữ liệu bắt buộc nhập" ValidationGroup="GHIDULIEU" runat="server" Display="Dynamic" >*</asp:RequiredFieldValidator>
<br />
<br /><portal:mojoButton ID="btnGhi" Text="Ghi dữ liệu" OnClick="btnGhi_Click" runat="server" ValidationGroup="GHIDULIEU" />
<br /><br /><asp:HyperLink ID="linkTroLaiDanhSach" runat="server" NavigateUrl="~/HOCSINH_LIST.aspx">Trở lại</asp:HyperLink>
<br /><asp:Label ID="lblMSG" runat="server" Text="" BackColor="Yellow"></asp:Label>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="GHIDULIEU" />
</portal:InnerBodyPanel>
</portal:OuterBodyPanel>
</ContentTemplate>
</asp:UpdatePanel>
<portal:EmptyPanel id="divCleared" runat="server" CssClass="cleared" SkinID="cleared" ></portal:EmptyPanel>
</portal:InnerWrapperPanel>
<mp:CornerRounderBottom id="cbottom1" runat="server" />
</portal:OuterWrapperPanel>

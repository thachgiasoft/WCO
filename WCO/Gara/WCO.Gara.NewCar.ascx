﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WCO.Gara.NewCar.ascx.cs" Inherits="WCO.Gara.NewCar" %>
<%@ Register Assembly="mojoPortal.Web" Namespace="mojoPortal.Web.UI" TagPrefix="cc1" %>
<%@ Register Assembly="mojoPortal.Web.Controls" Namespace="mojoPortal.Web.Controls.ExtJs" TagPrefix="cc2" %>
<%@ Register Assembly="mojoPortal.Web.Controls" Namespace="mojoPortal.Web.Controls" TagPrefix="cc3" %>

<cc1:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
    <cc3:CornerRounderTop ID="ctop1" runat="server" />
    <cc1:InnerWrapperPanel ID="pnlInnerWrap" runat="server">
        <asp:UpdatePanel ID="upNewCar" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <table style="width: 90%;">
                    <tr>
                        <td style="width: 17%">Biển số</td>
                        <td style="width: 15%">
                            <cc1:mojoTextArea ID="txtBienSo" runat="server" Width="90%"></cc1:mojoTextArea></td>
                        <td colspan="4"></td>
                    </tr>
                    <tr>
                        <td>Mã kiểu xe</td>
                        <td>
                            <cc1:mojoTextArea ID="MojoTextArea7" runat="server" Width="90%"></cc1:mojoTextArea></td>
                        <td colspan="4">
                            <cc1:mojoTextArea ID="MojoTextArea8" runat="server" Width="90%"></cc1:mojoTextArea></td>
                    </tr>
                    <tr>
                        <td>Năm sản xuất</td>
                        <td>
                            <cc1:mojoTextArea ID="txtNamSX" runat="server" Width="90%"></cc1:mojoTextArea></td>
                        <td style="width: 15%">Số khung</td>
                        <td style="width: 19%">
                            <cc1:mojoTextArea ID="MojoTextArea1" runat="server" Width="90%"></cc1:mojoTextArea></td>
                        <td style="width: 15%">Số máy</td>
                        <td style="width: 19%">
                            <cc1:mojoTextArea ID="MojoTextArea4" runat="server" Width="90%"></cc1:mojoTextArea></td>
                    </tr>
                    <tr>
                        <td>Ngày mua xe</td>
                        <td>
                            <cc1:mojoTextArea ID="MojoTextArea2" runat="server" Width="90%"></cc1:mojoTextArea></td>
                        <td style="width: 15%">Màu xe</td>
                        <td style="width: 19%">
                            <cc1:mojoTextArea ID="MojoTextArea5" runat="server" Width="90%"></cc1:mojoTextArea></td>
                        <td style="width: 15%">Mã nội thất</td>
                        <td style="width: 19%">
                            <cc1:mojoTextArea ID="MojoTextArea6" runat="server" Width="90%"></cc1:mojoTextArea></td>
                    </tr>
                    <tr>
                        <td>Mã khách</td>
                        <td>
                            <cc1:mojoTextArea ID="MojoTextArea3" runat="server" Width="90%"></cc1:mojoTextArea></td>
                        <td colspan="4"></td>
                    </tr>
                    <tr>
                        <td>Tên khách</td>
                        <td colspan="4">
                            <cc1:mojoTextArea ID="MojoTextArea9" runat="server" Width="90%"></cc1:mojoTextArea></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>Điện thoại </td>
                        <td colspan="2">
                            <cc1:mojoTextArea ID="MojoTextArea10" runat="server" Width="90%"></cc1:mojoTextArea></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>Địa chỉ</td>
                        <td colspan="4">
                            <cc1:mojoTextArea ID="MojoTextArea14" runat="server" Width="90%"></cc1:mojoTextArea></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>Lái xe </td>
                        <td colspan="2">
                            <cc1:mojoTextArea ID="MojoTextArea11" runat="server" Width="90%"></cc1:mojoTextArea></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>Điện thoại </td>
                        <td colspan="2">
                            <cc1:mojoTextArea ID="MojoTextArea12" runat="server" Width="90%"></cc1:mojoTextArea></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>Mã số thuế </td>
                        <td colspan="2">
                            <cc1:mojoTextArea ID="MojoTextArea13" runat="server" Width="90%"></cc1:mojoTextArea></td>
                        <td></td>
                    </tr>
                    <tr style="text-align: center">
                        <td colspan="6">
                            <asp:Button ID="Button1" runat="server" Text="Thêm xe" /></td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <cc1:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></cc1:EmptyPanel>
    </cc1:InnerWrapperPanel>
    <cc3:CornerRounderBottom ID="cbottom1" runat="server" />
</cc1:OuterWrapperPanel>

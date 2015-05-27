<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WCO.PriceAnnounce.ascx.cs" Inherits="WCO.Gara.NewCar" %>
<%@ Register Assembly="mojoPortal.Web" Namespace="mojoPortal.Web.UI" TagPrefix="cc1" %>
<%@ Register Assembly="mojoPortal.Web.Controls" Namespace="mojoPortal.Web.Controls.ExtJs" TagPrefix="cc2" %>
<%@ Register Assembly="mojoPortal.Web.Controls" Namespace="mojoPortal.Web.Controls" TagPrefix="cc3" %>

<cc1:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
    <cc3:CornerRounderTop ID="ctop1" runat="server" />
    <cc1:InnerWrapperPanel ID="pnlInnerWrap" runat="server">
        <asp:UpdatePanel ID="upNewCar" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div>
                    <div style="text-align: center">
                        <h2>BÁO GIÁ</h2>
                    </div>
                    <div style="text-align: center;">
                        <asp:Label ID="lblNgay" runat="server">10/05/2015</asp:Label>
                    </div>
                    <div>
                        <table style="width: 100%;">
                            <tbody>
                                <tr>
                                    <td style="width: 20%;"></td>
                                    <td style="width: 20%;"></td>
                                    <td style="width: 5%;"></td>
                                    <td style="width: 25%;"></td>
                                    <td style="width: 8%;"></td>
                                    <td style="width: 3%;"></td>
                                    <td style="width: 10%;"></td>
                                    <td style="width: 3%;"></td>
                                    <td style="width: 8%;"></td>
                                    <td style="width: 3%;"></td>
                                </tr>
                                <tr>
                                    <td>Số báo giá:
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox Width="100%" ID="txtsobaogia" Enabled="false" runat="server">SBG-09292929</asp:TextBox>
                                    </td>
                                    <td colspan="6" style="text-align: center;">Hình Thức Sửa Chữa
                                    </td>

                                </tr>
                                <tr>
                                    <td>Khách hàng:
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox Width="100%" ID="txtkhachhang" runat="server">Anh Cường</asp:TextBox>
                                    </td>
                                    <td>Kiểm tra</td>
                                    <td style="text-align: center;">
                                        <asp:CheckBox runat="server" ID="chkkt" Checked="true" />
                                    </td>
                                    <td>Sữa chữa chung</td>
                                    <td style="text-align: center;">
                                        <asp:CheckBox runat="server" ID="chkscc" Checked="true" />
                                    </td>
                                    <td>Sữa chữa nhanh</td>
                                    <td style="text-align: center;">
                                        <asp:CheckBox runat="server" ID="chkscn" Checked="false" />
                                    </td>
                                </tr>

                                <tr>
                                    <td>Địa chỉ:</td>

                                    <td colspan="3">
                                        <asp:TextBox ID="txtdiachi" runat="server" Width="100%">Lô 1, Lê Quang Đạo cạnh cây xăng </asp:TextBox>
                                    </td>

                                    <td>Sửa chữa thân/Sơn</td>
                                    <td style="text-align: center;">
                                        <asp:CheckBox ID="chkscs" runat="server" Checked="false" />
                                    </td>
                                    <td>Bảo hành</td>
                                    <td style="text-align: center;">
                                        <asp:CheckBox ID="chkbh" runat="server" Checked="false" />
                                    </td>
                                    <td>Khác</td>
                                    <td style="text-align: center;">
                                        <asp:CheckBox ID="chkk" runat="server" Checked="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Điện thoại:
                                    </td>
                                    <td>
                                        <asp:TextBox Width="100%" ID="txtdienthoai" runat="server">0986829725 </asp:TextBox>
                                    </td>
                                    <td>MST:
                                    </td>
                                    <td>
                                        <asp:TextBox Width="100%" ID="txtmasothue" runat="server">01010101010 </asp:TextBox>
                                    </td>
                                    <td>Bảo dưỡng định kỳ</td>
                                    <td style="text-align: center;">
                                        <asp:CheckBox runat="server" ID="chkbd" Checked="false" />
                                    </td>
                                    <td>Sữa chữa lại</td>
                                    <td style="text-align: center;">
                                        <asp:CheckBox runat="server" ID="chkscl" Checked="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Biển số xe:</td>
                                    <td>
                                        <asp:TextBox Width="100%" ID="txtbiensoxe" runat="server">29B1.513.16 </asp:TextBox></td>
                                    <td>Số vin:</td>
                                    <td>
                                        <asp:TextBox Width="100%" ID="txtsovin" runat="server">99513.16 </asp:TextBox></td>

                                </tr>
                                <tr>
                                    <td>Loại xe:</td>
                                    <td>
                                        <asp:TextBox Width="100%" ID="txtloaixe" runat="server">VIOS </asp:TextBox></td>
                                    <td>Số máy:</td>
                                    <td>
                                        <asp:TextBox Width="100%" ID="txtsomay" runat="server">99513.16 </asp:TextBox></td>
                                    <td></td>
                                    <td></td>
                                    <td colspan="2">Ngày đến:</td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtngayden" Width="100%" runat="server">10/05/2015</asp:TextBox>
                                    </td>

                                </tr>
                                <tr>
                                    <td>Hãng xe:</td>
                                    <td>
                                        <asp:TextBox Width="100%" ID="txthangxe" runat="server">TOYOTA </asp:TextBox></td>

                                    <td>Số km:</td>
                                    <td>
                                        <asp:TextBox Width="100%" ID="txtsokm" runat="server">101010 </asp:TextBox></td>
                                    <td></td>
                                    <td></td>
                                    <td colspan="2">Ngày dự định bàn giao xe:</td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtngaydudinhgiaoxe" Width="100%" runat="server">10/06/2015</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="10"></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div>
                        <table style="width: 100%;">
                            <tbody>
                                <tr>
                                    <td style="width: 3%; text-align: center">STT</td>
                                    <td style="width: 47%%; text-align: center">Nội Dung</td>
                                    <td style="width: 10%; text-align: center">Hạng mục</td>
                                    <td style="width: 5%; text-align: center">SL</td>
                                    <td style="width: 5%; text-align: center">Đvt</td>
                                    <td style="width: 10%; text-align: center">Đơn giá</td>
                                    <td style="width: 10%; text-align: center">Giảm Giá</td>
                                    <td style="width: 10%; text-align: center">Thành tiền</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox Style="text-align: center;" Width="100%" ID="txtstt" runat="server">1</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox Style="text-align: left;" ID="TextBox5" Width="100%" runat="server">Hệ thống điều hòa</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox Style="text-align: left;" ID="TextBox8" Width="100%" runat="server">Xử lý</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox Style="text-align: center;" ID="TextBox1" Width="100%" runat="server">1</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox Style="text-align: left;" ID="TextBox2" Width="100%" runat="server">Công</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox Style="text-align: center;" ID="TextBox3" Width="100%" runat="server">250 000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox Style="text-align: center;" ID="TextBox4" Width="100%" runat="server">  10 000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox Style="text-align: center;" ID="TextBox7" Width="100%" runat="server">240 000</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox Style="text-align: center;" Width="100%" ID="TextBox6" runat="server">1</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox Style="text-align: left;" ID="TextBox9" Width="100%" runat="server">Hệ thống điều hòa</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox Style="text-align: left;" ID="TextBox10" Width="100%" runat="server">Xử lý</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox Style="text-align: center;" ID="TextBox11" Width="100%" runat="server">1</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox Style="text-align: left;" ID="TextBox12" Width="100%" runat="server">Công</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox Style="text-align: center;" ID="TextBox13" Width="100%" runat="server">250 000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox Style="text-align: center;" ID="TextBox14" Width="100%" runat="server">  10 000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox Style="text-align: center;" ID="TextBox15" Width="100%" runat="server">240 000</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox Style="text-align: center;" Width="100%" ID="TextBox16" runat="server">2</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox Style="text-align: left;" ID="TextBox17" Width="100%" runat="server">Phớt láp</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox Style="text-align: left;" ID="TextBox18" Width="100%" runat="server">Thay thế</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox Style="text-align: center;" ID="TextBox19" Width="100%" runat="server">1</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox Style="text-align: left;" ID="TextBox20" Width="100%" runat="server">Cái</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox Style="text-align: center;" ID="TextBox21" Width="100%" runat="server">250 000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox Style="text-align: center;" ID="TextBox22" Width="100%" runat="server">  10 000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox Style="text-align: center;" ID="TextBox23" Width="100%" runat="server">240 000</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox Style="text-align: center;" Width="100%" ID="TextBox24" runat="server">3</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox Style="text-align: left;" ID="TextBox25" Width="100%" runat="server">Các chỗ xước</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox Style="text-align: left;" ID="TextBox26" Width="100%" runat="server">Sơn</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox Style="text-align: center;" ID="TextBox27" Width="100%" runat="server">1</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox Style="text-align: left;" ID="TextBox28" Width="100%" runat="server">Lần</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox Style="text-align: center;" ID="TextBox29" Width="100%" runat="server">600 000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox Style="text-align: center;" ID="TextBox30" Width="100%" runat="server">  10 000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox Style="text-align: center;" ID="TextBox31" Width="100%" runat="server">590 000</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>

                                    <td>Ghi chú:</td>
                                    <td colspan="3">
                                        <asp:TextBox TextMode="MultiLine" Rows="4" Width="100%" ID="txtghichu" runat="server"></asp:TextBox>
                                    </td>
                                    <td></td>
                                    <td colspan="2">
                                        <asp:Label ID="lblcong" runat="server">Cộng:</asp:Label>
                                        <br />
                                        <asp:Label ID="Label1" runat="server">Giảm giá:</asp:Label>
                                        <br />
                                        <asp:Label ID="Label2" runat="server">Thuế GTGT(10%):</asp:Label>
                                        <br />
                                        <asp:Label ID="Label3" runat="server">Tổng tiền:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox Style="text-align: center;" ID="TextBox32" Width="100%" runat="server">590 000</asp:TextBox><br />
                                        <asp:TextBox Style="text-align: center;" ID="TextBox33" Width="100%" runat="server">590 000</asp:TextBox><br />
                                        <asp:TextBox Style="text-align: center;" ID="TextBox34" Width="100%" runat="server">590 000</asp:TextBox><br />
                                        <asp:TextBox Style="text-align: center;" ID="TextBox35" Width="100%" runat="server">590 000</asp:TextBox>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <cc1:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></cc1:EmptyPanel>
    </cc1:InnerWrapperPanel>
    <cc3:CornerRounderBottom ID="cbottom1" runat="server" />
</cc1:OuterWrapperPanel>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WCO.Gara.RepairCommand.ascx.cs" Inherits="WCO.Gara.RepairCommand" %>
<%@ Register Assembly="mojoPortal.Web" Namespace="mojoPortal.Web.UI" TagPrefix="cc1" %>
<%@ Register Assembly="mojoPortal.Web.Controls" Namespace="mojoPortal.Web.Controls.ExtJs" TagPrefix="cc2" %>
<%@ Register Assembly="mojoPortal.Web.Controls" Namespace="mojoPortal.Web.Controls" TagPrefix="cc3" %>
<link href="style.css" rel="stylesheet" type="text/css">
<script src="tabs_old.js"></script>
<script type="text/javascript">

    var _gaq = _gaq || [];
    _gaq.push(['_setAccount', 'UA-1332079-8']);
    _gaq.push(['_trackPageview']);

    (function () {
        var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
        ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
        var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
    })();

</script>

<cc1:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
    <cc3:CornerRounderTop ID="ctop1" runat="server" />
    <cc1:InnerWrapperPanel ID="pnlInnerWrap" runat="server">
        <asp:UpdatePanel ID="upNewCar" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <table style="width: 100%;" border="1">
                    <tr>
                        <td width="50%">
                            <table style="width: 90%;">
                                <tr>
                                    <td style="width: 25%">Biển số</td>
                                    <td style="width: 25%">
                                        <cc1:mojoTextArea ID="txtBienSo" runat="server" Width="90%"></cc1:mojoTextArea></td>
                                    <td style="width: 25%">Cố vấn DV</td>
                                    <td style="width: 25%">
                                        <cc1:mojoTextArea ID="MojoTextArea5" runat="server" Width="90%"></cc1:mojoTextArea></td>
                                </tr>
                                <tr>
                                    <td>Số lệnh sửa chữa</td>
                                    <td>
                                        <cc1:mojoTextArea ID="MojoTextArea3" runat="server" Width="90%"></cc1:mojoTextArea></td>
                                    <td>Ngày vào</td>
                                    <td>
                                        <cc1:mojoTextArea ID="MojoTextArea4" runat="server" Width="90%"></cc1:mojoTextArea></td>
                                </tr>
                                <tr>
                                    <td>Số km cũ</td>
                                    <td>
                                        <cc1:mojoTextArea ID="MojoTextArea1" runat="server" Width="90%"></cc1:mojoTextArea></td>
                                    <td>Số km mới</td>
                                    <td>
                                        <cc1:mojoTextArea ID="MojoTextArea2" runat="server" Width="90%"></cc1:mojoTextArea></td>
                                </tr>
                            </table>
                        </td>
                        <td colspan="2">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 25%">Mã kiểu xe</td>
                                    <td style="width: 25%">
                                        <cc1:mojoTextArea ID="MojoTextArea16" runat="server" Width="90%"></cc1:mojoTextArea></td>
                                    <td style="width: 25%">Kiểu xe</td>
                                    <td style="width: 25%">
                                        <cc1:mojoTextArea ID="MojoTextArea17" runat="server" Width="90%"></cc1:mojoTextArea></td>
                                </tr>
                                <tr>
                                    <td>Số khung</td>
                                    <td>
                                        <cc1:mojoTextArea ID="MojoTextArea13" runat="server" Width="90%"></cc1:mojoTextArea></td>
                                    <td>Mã màu</td>
                                    <td>
                                        <cc1:mojoTextArea ID="MojoTextArea15" runat="server" Width="90%"></cc1:mojoTextArea></td>
                                </tr>
                                <tr>
                                    <td>Số máy</td>
                                    <td>
                                        <cc1:mojoTextArea ID="MojoTextArea18" runat="server" Width="90%"></cc1:mojoTextArea></td>
                                    <td>Mã nội thất</td>
                                    <td>
                                        <cc1:mojoTextArea ID="MojoTextArea19" runat="server" Width="90%"></cc1:mojoTextArea></td>
                                </tr>

                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table style="width: 100%;">
                                <tr>
                                    <td width="17%">Mã khách hàng</td>
                                    <td width="50%">
                                        <cc1:mojoTextArea ID="MojoTextArea40" runat="server" Width="98%"></cc1:mojoTextArea></td>
                                    <td width="17%">Điện thoại KH</td>
                                    <td width="17%">
                                        <cc1:mojoTextArea ID="MojoTextArea6" runat="server" Width="90%"></cc1:mojoTextArea></td>
                                </tr>
                                <tr>
                                    <td>Tên khách hàng</td>
                                    <td>
                                        <cc1:mojoTextArea ID="MojoTextArea41" runat="server" Width="98%"></cc1:mojoTextArea></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>Địa chỉ</td>
                                    <td colspan="2">
                                        <cc1:mojoTextArea ID="MojoTextArea43" runat="server" Width="99%"></cc1:mojoTextArea></td>
                                </tr>
                                <tr>
                                    <td>Tên lái xe </td>
                                    <td>
                                        <cc1:mojoTextArea ID="MojoTextArea44" runat="server" Width="98%"></cc1:mojoTextArea></td>
                                    <td>Điện thoại LX</td>
                                    <td>
                                        <cc1:mojoTextArea ID="MojoTextArea7" runat="server" Width="90%"></cc1:mojoTextArea></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>Yêu cầu </td>
                                    <td>
                                        <cc1:mojoTextArea ID="MojoTextArea8" runat="server" Width="98%"></cc1:mojoTextArea></td>
                                    <td>Gói sửa chữa</td>
                                    <td>
                                        <cc1:mojoTextArea ID="MojoTextArea9" runat="server" Width="90%"></cc1:mojoTextArea></td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 22%">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 50%">Giờ xe vào</td>
                                    <td style="width: 50%">
                                        <cc1:mojoTextArea ID="MojoTextArea10" runat="server" Width="90%"></cc1:mojoTextArea></td>

                                </tr>
                                <tr>
                                    <td>Giờ sửa xong</td>
                                    <td>
                                        <cc1:mojoTextArea ID="MojoTextArea12" runat="server" Width="90%"></cc1:mojoTextArea></td>
                                </tr>
                                <tr>
                                    <td>Giờ giao xe</td>
                                    <td>
                                        <cc1:mojoTextArea ID="MojoTextArea20" runat="server" Width="90%"></cc1:mojoTextArea></td>
                                </tr>
                                <tr>
                                    <td>Ngày giao xe</td>
                                    <td>
                                        <cc1:mojoTextArea ID="MojoTextArea21" runat="server" Width="90%"></cc1:mojoTextArea></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <div id="tabContainer">
                                <div id="tabs">
                                  <ul>
                                    <li id="tabHeader_1">Nội dung sửa chữa</li>
                                    <li id="tabHeader_2">Vật tư phụ tùng thay thế</li>
                                  </ul>
                                </div>
                                <div id="tabscontent">
                                  <div class="tabpage" id="tabpage_1">
                                      <cc2:ExtGridView ID="ExtGridView1" runat="server" width="100%"></cc2:ExtGridView>
                                  </div>
                                  <div class="tabpage" id="tabpage_2">
                                      <cc2:ExtGridView ID="ExtGridView2" runat="server" width="100%"></cc2:ExtGridView>
                                  </div>
                                </div>
                            </div>
                        </td>                        
                    </tr>
                    <tr>
                        <td >
                            <table style="width: 98%;">
                                <tr>
                                    <td style="width: 25%">Hẹn lần sau</td>
                                    <td style="width: 25%">
                                        <cc1:mojoTextArea ID="MojoTextArea14" runat="server" Width="90%"></cc1:mojoTextArea></td>
                                    <td style="width: 25%;text-align:center">hoặc</td>
                                    <td style="width: 25%">
                                        <cc1:mojoTextArea ID="MojoTextArea22" runat="server" Width="100%"></cc1:mojoTextArea></td>
                                </tr>
                                <tr>
                                    <td>Nội dung hẹn</td>
                                    <td colspan="3">
                                        <cc1:mojoTextArea ID="MojoTextArea23" runat="server" Width="100%"></cc1:mojoTextArea></td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-decoration:underline">Theo dõi sau sửa chữa</td>
                                </tr>
                                <tr>
                                    <td style="text-align:right">ĐT. thời gian</td>
                                    <td style="text-align:right">
                                        <asp:CheckBox ID="chkDT" runat="server" Text=" "/><cc1:mojoTextArea ID="MojoTextArea30" runat="server" Width="75%"></cc1:mojoTextArea>
                                    </td>
                                    <td>Thư. thời gian</td>
                                    <td style="text-align:right;margin:initial"><asp:CheckBox ID="chkThu" runat="server" Text=" "/>
                                        <asp:Label ID="Label1" runat="server" Text=" Không cần " ></asp:Label>
                                        <asp:CheckBox ID="chkKocan" runat="server" Text=" "/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td colspan="2">
                            <table style="width: 100%; height:100%">
                                <tr>
                                    <td style="width: 50%">
                                        <table style="width: 100%; height:100%">
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Button ID="btnLenhBoSung" runat="server" Text="Lập lệnh bổ sung" Width="100%"/></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%">
                                                    <asp:Button ID="Button1" runat="server" Text="Nhập Nd MG" Width="100%"/></td>
                                                <td style="width: 50%">
                                                    <asp:Button ID="Button2" runat="server" Text="Quyết toán lệnh" Width="100%"/></td>
                                            </tr>
                                            <tr>
                                                <td>Ngày Q.Toán</td>
                                                <td>
                                                    <cc1:mojoTextArea ID="MojoTextArea11" runat="server" Width="90%"></cc1:mojoTextArea>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 50%">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 50%">Tiền công</td>
                                                <td style="width: 50%">
                                                    <cc1:mojoTextArea ID="MojoTextArea24" runat="server" Width="90%"></cc1:mojoTextArea></td>
                                            </tr>
                                            <tr>
                                                <td>Tiền vật tư</td>
                                                <td>
                                                    <cc1:mojoTextArea ID="MojoTextArea25" runat="server" Width="90%"></cc1:mojoTextArea></td>
                                            </tr>
                                            <tr>
                                                <td>Tiền thuế</td>
                                                <td>
                                                    <cc1:mojoTextArea ID="MojoTextArea26" runat="server" Width="90%"></cc1:mojoTextArea></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%;">                                
                                <tr>
                                    <td style="width: 30%">Tiền môi giới</td>
                                    <td style="width: 40%">
                                        <cc1:mojoTextArea ID="MojoTextArea36" runat="server" Width="90%"></cc1:mojoTextArea></td>
                                    <td style="width: 30%">Nội dung môi giới</td>
                                </tr>                                
                            </table>
                        </td>
                        <td colspan="2">
                            <cc1:mojoTextArea ID="MojoTextArea27" runat="server" Width="100%"></cc1:mojoTextArea></td>
                        </td>
                    </tr>
                    <tr >
                        <td colspan="3">
                            <table style="width: 100%;">                                
                                <tr>
                                    <td style="width: 15%">Trạng thái lệnh</td>
                                    <td style="width: 12%"><cc1:mojoTextArea ID="MojoTextArea28" runat="server" Width="100%"></cc1:mojoTextArea></td>
                                    <td style="width: 30%"><cc1:mojoTextArea ID="MojoTextArea29" runat="server" Width="100%"></cc1:mojoTextArea></td>
                                    <td style="width: 43%"></td>
                                </tr>                                
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%;">                                
                                <tr>
                                    <td style="width: 15%"><asp:Button ID="Button3" runat="server" Text="Thêm" width="80%"/></td>
                                    <td style="width: 15%"><asp:Button ID="Button4" runat="server" Text="Hủy" width="80%"/></td>
                                    <td style="width: 15%"><asp:Button ID="Button5" runat="server" Text="Copy" width="80%"/></td>
                                    <td style="width: 43%"></td>
                                </tr>                                
                            </table>
                        </td>                            
                        <td style="width: 28%">
                            <table style="width: 100%;">                                
                                <tr>
                                    <td style="width: 15%"><asp:Button ID="Button6" runat="server" Text="Thoát" width="80%"/></td>
                                    <td style="width: 43%"></td>
                                </tr>                                
                            </table>
                        </td>
                        <td style="width: 22%;text-align:right">
                            <table style="width: 100%;">                                
                                <tr>
                                    <td style="width: 13%"></td>
                                    <td ><asp:Button ID="Button9" runat="server" Text="Lịch sử" width="80%"/></td>
                                    <td ><asp:Button ID="Button10" runat="server" Text="Đặt lịch" width="80%"/></td>
                                    <td ><asp:Button ID="Button11" runat="server" Text="Tìm kiếm" width="80%"/></td>                                    
                                </tr>                                
                            </table>
                        </td>
                    </tr>

                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <cc1:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></cc1:EmptyPanel>
    </cc1:InnerWrapperPanel>
    <cc3:CornerRounderBottom ID="cbottom1" runat="server" />
</cc1:OuterWrapperPanel>

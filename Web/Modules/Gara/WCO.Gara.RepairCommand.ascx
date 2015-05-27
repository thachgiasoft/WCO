<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WCO.Gara.RepairCommand.ascx.cs" Inherits="mojoPortal.Web.Modules.RepairCommand" %>
<%@ Register Assembly="mojoPortal.Web" Namespace="mojoPortal.Web.UI" TagPrefix="cc1" %>
<%@ Register Assembly="mojoPortal.Web.Controls" Namespace="mojoPortal.Web.Controls.ExtJs" TagPrefix="cc2" %>
<%@ Register Assembly="mojoPortal.Web.Controls" Namespace="mojoPortal.Web.Controls" TagPrefix="cc3" %>
<script type="text/javascript">
    var myTabs = new YAHOO.widget.TabView("demo");
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
                                        <asp:TextBox ID="txtBienSo" runat="server" Width="90%"></asp:TextBox></td>
                                    <td style="width: 25%">Cố vấn DV</td>
                                    <td style="width: 25%">
                                        <asp:TextBox ID="MojoTextArea5" runat="server" Width="90%"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Số lệnh sửa chữa</td>
                                    <td>
                                        <asp:TextBox ID="MojoTextArea3" runat="server" Width="90%"></asp:TextBox></td>
                                    <td>Ngày vào</td>
                                    <td>
                                        <asp:TextBox ID="MojoTextArea4" runat="server" Width="90%"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Số km cũ</td>
                                    <td>
                                        <asp:TextBox ID="MojoTextArea1" runat="server" Width="90%"></asp:TextBox></td>
                                    <td>Số km mới</td>
                                    <td>
                                        <asp:TextBox ID="MojoTextArea2" runat="server" Width="90%"></asp:TextBox></td>
                                </tr>
                            </table>
                        </td>
                        <td colspan="2">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 25%">Mã kiểu xe</td>
                                    <td style="width: 25%">
                                        <asp:TextBox ID="MojoTextArea16" runat="server" Width="90%"></asp:TextBox></td>
                                    <td style="width: 25%">Kiểu xe</td>
                                    <td style="width: 25%">
                                        <asp:TextBox ID="MojoTextArea17" runat="server" Width="90%"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Số khung</td>
                                    <td>
                                        <asp:TextBox ID="MojoTextArea13" runat="server" Width="90%"></asp:TextBox></td>
                                    <td>Mã màu</td>
                                    <td>
                                        <asp:TextBox ID="MojoTextArea15" runat="server" Width="90%"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Số máy</td>
                                    <td>
                                        <asp:TextBox ID="MojoTextArea18" runat="server" Width="90%"></asp:TextBox></td>
                                    <td>Mã nội thất</td>
                                    <td>
                                        <asp:TextBox ID="MojoTextArea19" runat="server" Width="90%"></asp:TextBox></td>
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
                                        <asp:TextBox ID="MojoTextArea40" runat="server" Width="98%"></asp:TextBox></td>
                                    <td width="17%">Điện thoại KH</td>
                                    <td width="17%">
                                        <asp:TextBox ID="MojoTextArea6" runat="server" Width="90%"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Tên khách hàng</td>
                                    <td>
                                        <asp:TextBox ID="MojoTextArea41" runat="server" Width="98%"></asp:TextBox></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>Địa chỉ</td>
                                    <td colspan="2">
                                        <asp:TextBox ID="MojoTextArea43" runat="server" Width="99%"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Tên lái xe </td>
                                    <td>
                                        <asp:TextBox ID="MojoTextArea44" runat="server" Width="98%"></asp:TextBox></td>
                                    <td>Điện thoại LX</td>
                                    <td>
                                        <asp:TextBox ID="MojoTextArea7" runat="server" Width="90%"></asp:TextBox></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>Yêu cầu </td>
                                    <td>
                                        <asp:TextBox ID="MojoTextArea8" runat="server" Width="98%"></asp:TextBox></td>
                                    <td>Gói sửa chữa</td>
                                    <td>
                                        <asp:TextBox ID="MojoTextArea9" runat="server" Width="90%"></asp:TextBox></td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 22%">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 50%">Giờ xe vào</td>
                                    <td style="width: 50%">
                                        <asp:TextBox ID="MojoTextArea10" runat="server" Width="90%"></asp:TextBox></td>

                                </tr>
                                <tr>
                                    <td>Giờ sửa xong</td>
                                    <td>
                                        <asp:TextBox ID="MojoTextArea12" runat="server" Width="90%"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Giờ giao xe</td>
                                    <td>
                                        <asp:TextBox ID="MojoTextArea20" runat="server" Width="90%"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Ngày giao xe</td>
                                    <td>
                                        <asp:TextBox ID="MojoTextArea21" runat="server" Width="90%"></asp:TextBox></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Panel ID="pnlTabs" runat="server" CssClass="modulecontent" width="99%">
                                <script type="text/javascript">
                                    $(document).ready(function () {
                                        $(".selector").tabs();
                                    });
                                    function function1() {

                                    }
                                    function function2() {

                                    }
                                    function CallPageMethod() {
                                        PageMethods.MyFirstPageMethod(onSucceeded, onFailed);
                                    }
                                    function onSuccess(result) {
                                        var lbl = document.getElementById('lbl');
                                        lbl.innerHTML = result;
                                        alert(result);
                                    }
                                    function onFailure(error) {
                                        alert(error);
                                    }
                                </script>
                                <div class="settingrow">                                    
                                    <div class="mojo-tabs">
                                        <ul>
                                            <li><a href="#tab1" onclick="function1();">Nội dung sửa chữa</a></li>
                                            <li><a href="#tab2" onclick="function2();">Vật tư phụ tùng thay thế</a></li>
                                        </ul>
                                        <div id="tab1">                                
                                            <cc2:ExtGridView ID="ExtGridView1" runat="server" Width="100%" AnimCollapse="True" AutoDestroy="True" AutoExpandColumn="" AutoGenerateColumns="False" AutoHeight="False" AutoLoad="" AutoScroll="False" AutoShow="False" AutoWidth="False" Border="True" Collapsed="False" CollapseFirst="True" Collapsible="False" DebugMode="False" DisabledClass="" DisableSelection="False" EnableColumnHide="True" EnableColumnMove="True" EnableColumnResize="True" EnableDragDrop="False" EnableHdMenu="True" EnableRowHeightSync="False" ExtJsBasePath="~/ClientScript/ext-2.0/" ExtraCssClass="" FixedPixelHeight="0" FixedPixelWidth="0" Footer="False" Frame="False" Header="False" HeaderAsText="True" HideBorders="False" IconCls="" Layout="" LoadMask="False" MinColumnWidth="25" MonitorWindowResize="True" Region="" StripeRows="False" Title="" TrackMouseOver="True">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Mã hàng" DataField="MAHANG" />
                                                    <asp:BoundField HeaderText="Loại" DataField="LOAI" />
                                                    <asp:BoundField HeaderText="Tên hàng" DataField="TENHANG" />
                                                    <asp:BoundField HeaderText="Tên vật tư tiếng Việt" DataField="TENTV" />
                                                    <asp:BoundField HeaderText="ĐVT" DataField="DVT" />
                                                    <asp:BoundField HeaderText="Tồn kho" DataField="TONKHO" />
                                                    <asp:BoundField HeaderText="SL yêu cầu" DataField="SL_YEUCAU" />
                                                    <asp:BoundField HeaderText="SL t.xuất" DataField="SL_TXUAT" />
                                                    <asp:BoundField HeaderText="Giá bán" DataField="GIABAN" />
                                                    <asp:BoundField HeaderText="%Giảm giá" DataField="GIAMGIA" />
                                                    <asp:BoundField HeaderText="Tiền bán" DataField="TIENBAN" />
                                                    <asp:BoundField HeaderText="% Thuế" DataField="THUE" />
                                                    <asp:BoundField HeaderText="Lần" DataField="LAN" />
                                                </Columns>
                                            </cc2:ExtGridView>                                       
                                        </div>
                                        <div id="tab2">
                                            <cc2:ExtGridView ID="ExtGridView2" runat="server" Width="100%" AnimCollapse="True" AutoDestroy="True" AutoExpandColumn="" AutoGenerateColumns="False" AutoHeight="False" AutoLoad="" AutoScroll="False" AutoShow="False" AutoWidth="False" Border="True" Collapsed="False" CollapseFirst="True" Collapsible="False" DebugMode="False" DisabledClass="" DisableSelection="False" EnableColumnHide="True" EnableColumnMove="True" EnableColumnResize="True" EnableDragDrop="False" EnableHdMenu="True" EnableRowHeightSync="False" ExtJsBasePath="~/ClientScript/ext-2.0/" ExtraCssClass="" FixedPixelHeight="0" FixedPixelWidth="0" Footer="False" Frame="False" Header="False" HeaderAsText="True" HideBorders="False" IconCls="" Layout="" LoadMask="False" MinColumnWidth="25" MonitorWindowResize="True" Region="" StripeRows="False" Title="" TrackMouseOver="True">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Tên loại hình công việc" DataField="TENLHCV" />
                                                    <asp:BoundField HeaderText="Loại" DataField="LOAI" />
                                                    <asp:BoundField HeaderText="Mã ctcv" DataField="MACTCV" />
                                                    <asp:BoundField HeaderText="Nội dung chi tiết" DataField="NOIDUNG" />
                                                    <asp:BoundField HeaderText="Tiền công" DataField="TIENCONG" />
                                                    <asp:BoundField HeaderText="% Giảm giá" DataField="GIAMGIA" />
                                                    <asp:BoundField HeaderText="VAT (%)" DataField="VAT" />
                                                    <asp:BoundField HeaderText="KTV" DataField="KTV" />
                                                    <asp:BoundField HeaderText="Lần" DataField="LAN" />
                                                </Columns>
                                            </cc2:ExtGridView>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 98%;">
                                <tr>
                                    <td style="width: 25%">Hẹn lần sau</td>
                                    <td style="width: 25%">
                                        <asp:TextBox ID="MojoTextArea14" runat="server" Width="90%"></asp:TextBox></td>
                                    <td style="width: 25%; text-align: center">hoặc</td>
                                    <td style="width: 25%">
                                        <asp:TextBox ID="MojoTextArea22" runat="server" Width="100%"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Nội dung hẹn</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="MojoTextArea23" runat="server" Width="100%"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-decoration: underline">Theo dõi sau sửa chữa</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">ĐT. thời gian</td>
                                    <td style="text-align: right">
                                        <asp:CheckBox ID="chkDT" runat="server" Text=" " /><asp:TextBox ID="MojoTextArea30" runat="server" Width="75%"></asp:TextBox>
                                    </td>
                                    <td>Thư. thời gian</td>
                                    <td style="text-align: right; margin: initial">
                                        <asp:CheckBox ID="chkThu" runat="server" Text=" " />
                                        <asp:Label ID="Label1" runat="server" Text=" Không cần "></asp:Label>
                                        <asp:CheckBox ID="chkKocan" runat="server" Text=" " />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td colspan="2">
                            <table style="width: 100%; height: 100%">
                                <tr>
                                    <td style="width: 50%">
                                        <table style="width: 100%; height: 100%">
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Button ID="btnLenhBoSung" runat="server" Text="Lập lệnh bổ sung" Width="100%" /></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%">
                                                    <asp:Button ID="Button1" runat="server" Text="Nhập Nd MG" Width="100%" /></td>
                                                <td style="width: 50%">
                                                    <asp:Button ID="Button2" runat="server" Text="Quyết toán lệnh" Width="100%" /></td>
                                            </tr>
                                            <tr>
                                                <td>Ngày Q.Toán</td>
                                                <td>
                                                    <asp:TextBox ID="MojoTextArea11" runat="server" Width="90%"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 50%">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 50%">Tiền công</td>
                                                <td style="width: 50%">
                                                    <asp:TextBox Height="30px" ID="MojoTextArea24" runat="server" Width="90%"></asp:TextBox></td>

                                            </tr>
                                            <tr>
                                                <td>Tiền vật tư</td>
                                                <td>
                                                    <asp:TextBox ID="MojoTextArea25" runat="server" Width="90%"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>Tiền thuế</td>
                                                <td>
                                                    <asp:TextBox ID="MojoTextArea26" runat="server" Width="90%"></asp:TextBox></td>
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
                                        <asp:TextBox ID="MojoTextArea36" runat="server" Width="90%"></asp:TextBox>
                                    </td>
                                    <td style="width: 30%">Nội dung môi giới</td>
                                </tr>
                            </table>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="MojoTextArea27" runat="server" Width="100%"></asp:TextBox></td>
                        </td>                   
                    </tr>
                    <tr>
                        <td colspan="3">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 15%">Trạng thái lệnh</td>
                                    <td style="width: 12%">
                                        <asp:TextBox ID="MojoTextArea28" runat="server" Width="100%"></asp:TextBox></td>
                                    <td style="width: 30%">
                                        <asp:TextBox ID="MojoTextArea29" runat="server" Width="100%"></asp:TextBox></td>
                                    <td style="width: 43%"></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 15%">
                                        <asp:Button ID="Button3" runat="server" Text="Thêm" Width="80%" /></td>
                                    <td style="width: 15%">
                                        <asp:Button ID="Button4" runat="server" Text="Hủy" Width="80%" /></td>
                                    <td style="width: 15%">
                                        <asp:Button ID="Button5" runat="server" Text="Copy" Width="80%" /></td>
                                    <td style="width: 43%"></td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 28%">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 15%">
                                        <asp:Button ID="Button6" runat="server" Text="Thoát" Width="80%" /></td>
                                    <td style="width: 43%"></td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 22%; text-align: right">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <asp:Button ID="Button9" runat="server" Text="Lịch sử" Width="90%" /></td>
                                    <td>
                                        <asp:Button ID="Button10" runat="server" Text="Đặt lịch" Width="90%" /></td>
                                    <td>
                                        <asp:Button ID="Button11" runat="server" Text="Tìm kiếm" Width="90%" /></td>
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

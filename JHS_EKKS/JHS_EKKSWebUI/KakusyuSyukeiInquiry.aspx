<%@ Page Language="VB" MasterPageFile="~/EKKSMaster.master" AutoEventWireup="false"
    CodeFile="KakusyuSyukeiInquiry.aspx.vb" Inherits="KakusyuSyukeiInquiry" Title="äeéÌ èWåv" %>

<%@ Register Src="CommonControl/KakusyuSyukeiInquiryData.ascx" TagName="KakusyuSyukeiInquiryData"
    TagPrefix="uc2" %>
<%@ Register Src="CommonControl/KakusyuSyukeiInquiryInfo.ascx" TagName="KakusyuSyukeiInquiryInfo"
    TagPrefix="uc1" %>
<%@ Register Src="CommonControl/CommonButton.ascx" TagName="CommonButton" TagPrefix="uc3" %>
<%@ MasterType VirtualPath="~/EKKSMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        //onload
//        window.onload = function(){
//            window.name = "<%=CommonConstBC.uriageYojituKanri%>"
//            setMenuBgColor();
//        }

    function wheel(event)
    {
        var delta = 0;
        if(!event)
            event = window.event;
        if(event.wheelDelta)
        {
            delta = event.wheelDelta/120;
        if(window.opera)
            delta = -delta;
        }
        else if(event.detail)
        {
            delta = -event.detail/3;
        }
        if(delta)
            handle(delta);
    }
    function handle(delta)
    {
        if (delta < 0)
        {
            divBodyRight.scrollTop = divBodyRight.scrollTop + 50;
        }else{
            divBodyRight.scrollTop = divBodyRight.scrollTop - 50;
        }
    }
    </script>

    <table>
        <tr>
            <td>
                <asp:Label ID="lblKakusyuSyukei" runat="server" Text="äeéÌ èWåv" CssClass="Title_fontBold">
                </asp:Label></td>
        </tr>
    </table>
    <div>
        <table class="mainTable2" style="margin-bottom: 5px; margin-top: 3px; width: 929px;">
            <tr>
                <td class="koumokuMei" style="width: 99px; text-align: center;">
                    èWåv ä˙ä‘ëIë
                </td>
                <td style="width: 815px;">
                    <asp:Button ID="btnNendo" runat="server" Text="îNìx" Style="width: 50px; height: 23px;
                        margin-left: 20px; cursor: hand;" TabIndex="1" />
                    <asp:DropDownList ID="ddlNendo" runat="server" Style="width: 100px;" TabIndex="2">
                    </asp:DropDownList>
                    <asp:Button ID="btnKikan" runat="server" Text="ä˙ä‘" Style="width: 50px; height: 23px;
                        margin-left: 20px; cursor: hand;" TabIndex="3" />
                    <asp:DropDownList ID="ddlKikanFrom" runat="server" Style="width: 100px;" TabIndex="4">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlKikanTo" runat="server" Style="width: 140px;" TabIndex="5">
                    </asp:DropDownList>
                    <asp:Button ID="btnTukinami" runat="server" Text="åééü" Style="width: 50px; height: 23px;
                        margin-left: 20px; cursor: hand;" TabIndex="6" />
                    <asp:DropDownList ID="ddlTukinamiFrom" runat="server" Style="width: 100px;" TabIndex="7">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlTukinamiTo" runat="server" Style="width: 52px;" TabIndex="8"
                        AutoPostBack="true">
                    </asp:DropDownList>
                    Å`
                    <asp:DropDownList ID="ddlTukinamiTo2" runat="server" Style="width: 52px;" TabIndex="9"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <table cellpadding="2" class="mainTable2" style="width: 929px; margin-bottom: 5px;
            height: 50px;">
            <tr style="height: 24px;">
                <td class="koumokuMei" style="width: 99px; text-align: center;" rowspan="2" valign="top">
                    èWåv ì‡óeëIë</td>
                <td class="koumokuMei" style="text-align: center; width: 73px;">
                    éxìX</td>
                <td>
                    <asp:TextBox ID="tbxSiten" runat="server" Width="135px" Height="18px" TabIndex="10"
                        MaxLength="40"></asp:TextBox>
                    <asp:HiddenField ID="hidSitenCd" runat="server" />
                    <asp:Button ID="btnSiten" runat="server" Text="åüçı" Style="cursor: hand;" Height="24px"
                        TabIndex="11" /></td>
                <td class="koumokuMei" style="text-align: center; width: 73px;">
                    ìsìπï{åß</td>
                <td>
                    <asp:TextBox ID="tbxKameiten" runat="server" Width="135px" TabIndex="12" MaxLength="10"></asp:TextBox>
                    <asp:HiddenField ID="hidKameitenCd" runat="server" />
                    <asp:Button ID="btnKameiten" runat="server" Text="åüçı" Style="cursor: hand;" Height="24px"
                        TabIndex="13" /></td>
                <td class="koumokuMei" style="text-align: center; width: 73px;">
                    âcã∆èä</td>
                <td>
                    <asp:TextBox ID="tbxEigyou" runat="server" Width="135px" TabIndex="14" MaxLength="40"></asp:TextBox>
                    <asp:HiddenField ID="hidEigyouCd" runat="server" />
                    <asp:Button ID="btnEigyou" runat="server" Text="åüçı" Style="cursor: hand;" Height="24px"
                        TabIndex="15" /></td>
            </tr>
            <tr>
                <td class="koumokuMei" style="text-align: center; width: 73px;">
                    ånóÒñº</td>
                <td>
                    <asp:TextBox ID="tbxKeiretu" runat="server" Width="135px" TabIndex="16" MaxLength="40"></asp:TextBox>
                    <asp:HiddenField ID="hidKeiretuMei" runat="server" />
                    <asp:Button ID="btnKeiretu" runat="server" Text="åüçı" Style="cursor: hand;" Height="24px"
                        TabIndex="17" /></td>
                <td class="koumokuMei" style="text-align: center; width: 73px;">
                    âcã∆É}Éì</td>
                <td>
                    <asp:TextBox ID="tbxUser" runat="server" Width="135px" TabIndex="18" MaxLength="128"></asp:TextBox>
                    <asp:HiddenField ID="hidUserCd" runat="server" />
                    <asp:Button ID="btnUser" runat="server" Text="åüçı" Style="cursor: hand;" Height="24px"
                        TabIndex="19" /></td>
                <td class="koumokuMei" style="text-align: center; width: 73px;">
                    ìoò^éñã∆é“</td>
                <td>
                    <asp:TextBox ID="tbxTourokuJgousya" runat="server" Width="135px" TabIndex="20" MaxLength="40"></asp:TextBox>
                    <asp:HiddenField ID="hidTourokuJgousya" runat="server" />
                    <asp:Button ID="btnTourokuJgousya" runat="server" Text="åüçı" Style="cursor: hand;"
                        Height="24px" TabIndex="21" /></td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td>
                    <table class="mainTable2" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td class="koumokuMei" style="border-top-style: none; width: 99px; text-align: center;">
                                âcã∆ãÊï™çiçûÇ›</td>
                            <td style="border-top-style: none; width: 330px;" colspan="2" class="U009_td_font_bold">
                                <asp:CheckBox ID="chkEigyou" runat="server" Text="âcã∆" Width="65px" Style="margin-left: 10px"
                                    TabIndex="22" />
                                <asp:CheckBox ID="chkTokuhan" runat="server" Text="ì¡îÃ" Width="65px" Style="margin-left: 10px"
                                    TabIndex="23" />
                                <asp:CheckBox ID="chkSinki" runat="server" Text="êVãK" Width="65px" Style="margin-left: 10px"
                                    TabIndex="24" />
                                <asp:CheckBox ID="chkFC" runat="server" Text="FC" Width="65px" Style="margin-left: 10px"
                                    TabIndex="25" /></td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table cellpadding="0" cellspacing="0" border="0" style="margin-left: 90px;">
                        <tr>
                            <td>
                                <uc3:CommonButton ID="btnAllSave" ButtonKegen="ZensyaKeikakuKengen" Text="åãâ ï\é¶" runat="server"
                                    Cssclass="btnColor_Pink" TabIndex="26" />
                                <%--   <asp:Button ID="btnKeltukaHyouji" runat="server" Text="åãâ ï\é¶" CssClass="btnColor_Pink"
                                    Style="cursor: hand;" TabIndex="26" />--%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="divHead" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="font-size: 12px; margin-top: 10px">
            <tr>
                <td style="width: 482px;">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 482px; text-align: center;
                        border: solid 1px black; background-color: #99CCFE;">
                        <tr style="height: 20px;">
                            <td rowspan="2" style="width: 80px; border-right: solid 1px black; background-color: #FECC99;">
                                <asp:Label ID="lblSyukeiSentaku" runat="server" Text=""></asp:Label>
                            </td>
                            <td colspan="2" style="border-right: solid 1px black; border-bottom: solid 1px black;">
                                ëOîNÉfÅ[É^
                            </td>
                            <td style="border-bottom: solid 1px black;">
                                &nbsp;
                            </td>
                        </tr>
                        <tr style="height: 41px;">
                            <td style="width: 70px; border-right: solid 1px black;">
                                çHéñî‰ó¶
                            </td>
                            <td style="width: 240px; border-right: solid 1px black;">
                                è§ïi
                            </td>
                            <td>
                                ïΩãœíPâø
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="vertical-align: bottom;">
                    <div id="divHeadRight" style="width: 470px; overflow: hidden;">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 990px; text-align: center;
                            border-bottom: solid 1px black;">
                            <tr style="height: 20px;">
                                <td colspan="9" style="border-left: solid 1px black; border-bottom: solid 1px black;">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr style="height: 20px; background-color: #D6FEFE;">
                                <td colspan="3" style="border-left: solid 1px black; border-right: solid 1px black;
                                    border-bottom: solid 1px black;">
                                    åèêî
                                </td>
                                <td colspan="3" style="border-right: solid 1px black; border-bottom: solid 1px black;">
                                    îÑè„ã‡äz
                                </td>
                                <td colspan="3" style="border-right: solid 1px black; border-bottom: solid 1px black;">
                                    ëeóò
                                </td>
                            </tr>
                            <tr style="height: 20px; background-color: #D6FEFE;">
                                <td style="width: 131px; border-left: solid 1px black; border-right: solid 1px black;">
                                    åvâÊ
                                </td>
                                <td style="width: 131px; border-right: solid 1px black;">
                                    é¿ê—
                                </td>
                                <td style="width: 66px; border-right: solid 1px black;">
                                    íBê¨ó¶
                                </td>
                                <td style="width: 130px; border-right: solid 1px black;">
                                    åvâÊ
                                </td>
                                <td style="width: 130px; border-right: solid 1px black;">
                                    é¿ê—
                                </td>
                                <td style="width: 66px; border-right: solid 1px black;">
                                    íBê¨ó¶
                                </td>
                                <td style="width: 130px; border-right: solid 1px black;">
                                    åvâÊ
                                </td>
                                <td style="width: 130px; border-right: solid 1px black;">
                                    é¿ê—
                                </td>
                                <td style="border-right: solid 1px black;">
                                    íBê¨ó¶
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top;">
                    <div id="divBodyLeft" style="height: 287px; overflow: hidden; width: 482px;" onmousewheel="wheel();">
                        <asp:GridView ID="grdBodyLeft" runat="server" AutoGenerateColumns="False" ShowHeader="False"
                            BorderStyle="None" CellPadding="0" CellSpacing="0">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hidTitle" runat="server" Value='<%#Eval("title")%>' />
                                        <uc1:KakusyuSyukeiInquiryInfo ID="SyouhinInfo1" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle BorderStyle="None" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </td>
                <td>
                    <div id="divBodyRight" onscroll="divBodyLeft.scrollTop = this.scrollTop;divHeadRight.scrollLeft = this.scrollLeft;"
                        style="width: 487px; height: 304px; overflow: scroll;">
                        <asp:GridView ID="grdBodyRight" runat="server" AutoGenerateColumns="False" ShowHeader="False"
                            BorderStyle="None" CellPadding="0" CellSpacing="0">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hidTitle" runat="server" Value='<%#Eval("title")%>' />
                                        <uc2:KakusyuSyukeiInquiryData ID="SyouhinData1" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle BorderStyle="None" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <asp:Button ID="btnPopup" runat="server" Text="Button" Style="display: none;" />
    <asp:HiddenField ID="hidModouru" runat="server" />
</asp:Content>

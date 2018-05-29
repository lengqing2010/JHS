<%@ Page Language="VB" MasterPageFile="~/EKKSMaster.master" AutoEventWireup="false"
    CodeFile="KeikakuKameitenJyouhouInquiry.aspx.vb" Inherits="KeikakuKameitenJyouhouInquiry"
    Title="計画_加盟店情報照会" %>

<%@ Register Src="CommonControl/KeikakuKanriKameitenInquiry.ascx" TagName="KeikakuKanriKameitenInquiry"
    TagPrefix="uc1" %>
<%@ Register Src="CommonControl/KeikaikuKanriKameitenBikouInquiry.ascx" TagName="KeikaikuKanriKameitenBikouInquiry"
    TagPrefix="uc2" %>
<%@ MasterType VirtualPath="~/EKKSMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        window.onload = function()
        {
            window.name = "<%=CommonConstBC.keikakuKanriKameitenKensakuSyoukai%>"
            setMenuBgColor();
            
            fncSetAllPageScroll();
            
            fncBikouLoad();
        }    
    </script>

    <table>
        <tr>
            <td>
                <asp:Label ID="lblKeikakuKameitenJyouhouSyoukai" runat="server" Text="計画_加盟店情報照会"
                    CssClass="Title_fontBold">
                </asp:Label>
                <asp:Button ID="btnModoru" runat="server" Text="戻る" Style="width: 40px; height: 23px;
                    margin-left: 20px; cursor: hand;" TabIndex="1" /></td>
            <td style="width: 500px; text-align: right; vertical-align: top;">
                <asp:Button ID="btnClose" runat="server" Text="閉じる" OnClientClick="window.close();return false;"
                    TabIndex="2" Style="cursor: hand; width: 50px; height: 23px;" />
            </td>
            <td style="text-align: left; width: 95px;">
                <asp:Label ID="lblSaisinKousisya" runat="server" Text="最新更新者：">
                </asp:Label>
            </td>
            <td>
                <asp:Label ID="lblSaisinKousisyaValue" runat="server">
                </asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblKeikakuNendo" runat="server" Text="計画年度：" CssClass="TitleColor_Blue">
                </asp:Label>
                <asp:Label ID="lblKeikakuNendoValue" runat="server" CssClass="TitleColor_Blue">
                </asp:Label>
            </td>
            <td style="width: 500px;">
            </td>
            <td style="width: 95px; text-align: left;">
                <asp:Label ID="lblSaisinKousinNitiji" runat="server" Text="最新更新日時：">
                </asp:Label>
            </td>
            <td>
                <asp:Label ID="lblSaisinKousinNitijiValue" runat="server">
                </asp:Label>
            </td>
        </tr>
    </table>
    <table style="width: 960px;" cellspacing="0" cellpadding="1" class="mainTable paddinNarrow">
        <col width="85px;" />
        <col width="77px;" />
        <col width="70px;" />
        <col width="250px;" />
        <col width="45px;" />
        <col width="25px;" />
        <col width="115px;" />
        <col width="60px;" />
        <col width="30px;" />
        <col width="" />
        <tr>
            <td style="border-color: #ccffff;">
            </td>
            <td style="border-color: #ccffff;">
            </td>
            <td style="border-color: #ccffff;">
            </td>
            <td style="border-color: #ccffff;">
            </td>
            <td style="border-color: #ccffff;">
            </td>
            <td style="border-color: #ccffff;">
            </td>
            <td style="border-color: #ccffff;">
            </td>
            <td style="border-color: #ccffff;">
            </td>
            <td style="border-color: #ccffff;">
            </td>
            <td style="border-color: #ccffff;">
            </td>
        </tr>
        <tr>
            <th class="tableTitle TitleUndLine" style="height: 20px;" colspan="10">
                基本情報
            </th>
        </tr>
        <tr>
            <td class="koumokuMei">
                区分
            </td>
            <td colspan="3">
                <asp:TextBox ID="tbxKbn" runat="server" Width="50px" CssClass="readOnly" ReadOnly="true"></asp:TextBox>
                <asp:Label ID="lblKbn" runat="server" Width="280px"></asp:Label>
            </td>
            <td class="koumokuMei">
                取消
            </td>
            <td colspan="2">
                &nbsp;<asp:Label ID="lblTorikesi" runat="server" Width="110px"></asp:Label>
            </td>
            <td class="koumokuMei" colspan="2">
                発注停止フラグ
            </td>
            <td>
                &nbsp;<asp:Label ID="lblHaltutyuuTeisiFlg" runat="server" Width="155px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="koumokuMei">
                加盟店コード
            </td>
            <td>
                <asp:TextBox ID="tbxKameitenCd" runat="server" Width="50px" CssClass="readOnly" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="koumokuMei" style="">
                加盟店名
            </td>
            <td>
                <%--<asp:Label ID="lblKmeitenMei" runat="server" Width="240px"></asp:Label>--%>
                <asp:TextBox ID="lblKmeitenMei" runat="server" CssClass="readOnlyStyle" TabIndex="-1"
                    Width="240px" Style="border-bottom: none;"></asp:TextBox>
            </td>
            <td colspan="2" class="koumokuMei" style="">
                都道府県
            </td>
            <td colspan="4" style="">
                &nbsp;<asp:Label ID="lblTodouhuke" runat="server" Width="335px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="koumokuMei" style="">
                営業区分
            </td>
            <td colspan="3">
                <asp:TextBox ID="tbxEigyouKbn" runat="server" Width="50px" CssClass="readOnly" ReadOnly="true"></asp:TextBox>
                <asp:Label ID="lblEigyouKbn" runat="server" Width="250px"></asp:Label>
            </td>
            <td colspan="2" class="koumokuMei">
                営業担当者
            </td>
            <td colspan="2">
                &nbsp;<asp:TextBox ID="tbxEigyouSya" runat="server" Width="90px" CssClass="readOnly"
                    ReadOnly="true"></asp:TextBox>
                <%--<asp:Label ID="lblEigyouSya" runat="server" Width="75px" Text="１２３４ ５６７"></asp:Label>--%>
                <asp:TextBox ID="lblEigyouSya" runat="server" CssClass="readOnlyStyle" TabIndex="-1"
                    Width="65px" Style="border-bottom: none;"></asp:TextBox>
            </td>
            <td class="koumokuMei" style="">
                所属
            </td>
            <td>
                <%-- <asp:Label ID="lblSyozoku" runat="server" Width="160px" Text ="営業担当者営業担当者営業担当者"></asp:Label>--%>
                &nbsp;<asp:TextBox ID="lblSyozoku" runat="server" CssClass="readOnlyStyle" TabIndex="-1"
                    Width="155px" Style="border-bottom: none;"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="koumokuMei" style="">
                系列コード
            </td>
            <td colspan="3">
                <asp:TextBox ID="tbxKeiretuCd" runat="server" Width="50px" CssClass="readOnly" ReadOnly="true"></asp:TextBox>
                <asp:Label ID="lblKeiretuCd" runat="server" Width="250px"></asp:Label>
            </td>
            <td colspan="2" class="koumokuMei">
                管轄支店名
            </td>
            <td colspan="4">
                &nbsp;<asp:Label ID="lblKankatuSitemei" runat="server" Width="335px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="koumokuMei" style="">
                営業所コード
            </td>
            <td colspan="3">
                <asp:TextBox ID="tbxEigyousyoCd" runat="server" Width="50px" CssClass="readOnly"
                    ReadOnly="true"></asp:TextBox>
                <asp:Label ID="lblEigyousyoCd" runat="server" Width="250px"></asp:Label>
            </td>
            <td colspan="2" class="koumokuMei">
                年間棟数
            </td>
            <td>
                &nbsp;<asp:Label ID="lblNenken" runat="server" Width="85px"></asp:Label>
            </td>
            <td class="koumokuMei" colspan="2">
                計画値有無
            </td>
            <td>
                &nbsp;<asp:Label ID="lblKeikakuTi" runat="server" Width="155px"></asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <div id="divAll" style="overflow: auto; height: 294px; width: 977px; margin-top: 10px;
        border-width: 0px; border-style: solid;" onscroll="fncSaveAllPageScroll();">
        <!--##############    計画管理用_加盟店情報    ##############-->
        <uc1:KeikakuKanriKameitenInquiry ID="KeikakuKanriKameitenInquiry" runat="server"></uc1:KeikakuKanriKameitenInquiry>
        <br />
        <!--##############    計画管理用_加盟店備考情報    ##############-->
        <uc2:KeikaikuKanriKameitenBikouInquiry ID="KeikaikuKanriKameitenBikouInquiry1" runat="server" />
    </div>
    <asp:HiddenField runat="server" ID="hidAllPageScroll" />
</asp:Content>

<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SitenTukibetuKeikakuchiList.ascx.vb" Inherits="CommonControl_SitenTukibetuKeikakuchiList" %>
<link href="../App_Themes/CSS/JHS_EKKS.css" rel="stylesheet" type="text/css" />

<table border="0" cellpadding="0" cellspacing="0" class="" runat="server" id="tblTitle" width="40px;" style="margin-top:0px;">
    <tr style="text-align:center; height:15px;">
        <td class="">
            &nbsp;</td>
    </tr>
    <tr style="text-align:center; height:15px">
        <td style="" class="">
            &nbsp;</td>
    </tr>
    <tr style="text-align:center; height:15px">
        <td class="">
        &nbsp;</td>
    </tr>
    <tr style="text-align:center;height:15px">
        <td class="tr_border td_title K010_td_backcolor7">
        営業</td>
    </tr>
    <tr style="text-align:center;height:15px">
        <td class="tr_border td_title K010_td_backcolor7">
        特販</td>
    </tr>
    <tr style="text-align:center;height:15px">
        <td class="tr_border td_title K010_td_backcolor7">
        ＦＣ</td>
    </tr>
    <tr style="text-align:center;height:15px">
        <td class="tr_borderLast td_title K010_td_backcolor6">
        合計</td>
    </tr>
</table>

<table border="0" cellpadding="0" cellspacing="0" class="TableBorder" runat="server" id="tblId" style="margin-left:40px;margin-top:-110px; width:589px;">
    <tr style="text-align:center ;">
        <td  style="" colspan="3" class="TdBorder td_title K010_td_backcolor1">
            <asp:Label ID="lblTitle" runat="server" Text="7月"></asp:Label></td>
    </tr>
    <tr style="text-align:center ;">
        <td  style="" class="TdBorder td_title K010_td_backcolor2">
            <asp:Label ID="lblKensuu" runat="server" Text="計画 調査件数"></asp:Label></td>
        <td  style="" class="TdBorder td_title K010_td_backcolor2">
            <asp:Label ID="lblUriKingaku" runat="server" Text="計画 売上金額"></asp:Label></td>
        <td  style="" class="TdBorder td_title K010_td_backcolor2">
            <asp:Label ID="lblArari" runat="server" Text="計画 粗利金額"></asp:Label></td>
    </tr>
    <tr style="text-align:center;">
        <td class="">
            <table border="0" cellpadding="0" cellspacing="0" class="">
                <tr >
                    <td style="width:97px;" class="TdBorder td_title K010_td_backcolor3">
                        <asp:Label ID="lblKensuuBefore" runat="server" Text="前年"></asp:Label></td>
                    <td  style="width:97px;" class="TdBorder td_title K010_td_backcolor4">
                        <asp:Label ID="lblKeikakuKensuu" runat="server" Text="計画"></asp:Label></td>
                </tr>
                <tr style="height:15px;">
                    <td  style="" class="TdBorder td_item K010_td_backcolor3">
                       &nbsp;<asp:Label ID="lblEigyouKensuuBefore" runat="server" Text="123,456,789,012"></asp:Label></td>
                    <td  style="" class="TdBorder td_item K010_td_backcolor5" id="tdEigyouKeikakuKensuu" runat="server" >
                       &nbsp;<asp:Label ID="lblEigyouKeikakuKensuu" runat="server" Text="123,456,789,012"></asp:Label></td>
                </tr>
                <tr style="height:15px;">
                    <td  style="" class="TdBorder td_item K010_td_backcolor3">
                        &nbsp;<asp:Label ID="lblTokuhanKensuuBefore" runat="server" Text="123,456,789,012"></asp:Label></td>
                    <td  style="" class="TdBorder td_item K010_td_backcolor5" id="tdTokuhanKeikakuKensuu" runat="server">
                        &nbsp;<asp:Label ID="lblTokuhanKeikakuKensuu" runat="server" Text="123,456,789,012"></asp:Label></td>
                </tr>
                <tr style="height:15px;">
                    <td  style="" class="TdBorder td_item K010_td_backcolor3">
                        &nbsp;<asp:Label ID="lblFCKensuuBefore" runat="server" Text="123,456,789,012"></asp:Label></td>
                    <td  style="" class="TdBorder td_item K010_td_backcolor5" id="tdFCKeikakuKensuu" runat="server">
                        &nbsp;<asp:Label ID="lblFCKeikakuKensuu" runat="server" Text="123,456,789,012"></asp:Label></td>
                </tr>
                <tr style="height:15px;">
                    <td  style="" class="TdBorder td_item K010_td_backcolor3">
                        &nbsp;<asp:Label ID="lblGoukeiKensuuBefore" runat="server" Text="123,456,789,012"></asp:Label></td>
                    <td  style="" class="TdBorder td_item K010_td_backcolor6">
                        &nbsp;<asp:Label ID="lblGoukeiKeikakuKensuu" runat="server" Text="123,456,789,012"></asp:Label></td>
                </tr>
            </table>
        </td>
        <td class="">
            <table border="0" cellpadding="0" cellspacing="0" class="">
                <tr >
                    <td  style="width:97px;" class="TdBorder td_title K010_td_backcolor3">
                        <asp:Label ID="lblUriKingakuBefore" runat="server" Text="前年"></asp:Label></td>
                    <td  style="width:97px;" class="TdBorder td_title K010_td_backcolor4">
                        <asp:Label ID="lblKeikakuUriKingaku" runat="server" Text="計画"></asp:Label></td>
                </tr>
                <tr style="height:15px;">
                    <td  style="" class="TdBorder td_item K010_td_backcolor3">
                        &nbsp;<asp:Label ID="lblEigyouUriKingakuBefore" runat="server" Text="123,456,789,012"></asp:Label></td>
                    <td  style="" class="TdBorder td_item K010_td_backcolor5" id="tdEigyouKeikakuUriKingaku" runat="server">
                        &nbsp;<asp:Label ID="lblEigyouKeikakuUriKingaku" runat="server" Text="123,456,789,012"></asp:Label></td>
                </tr>
                <tr style="height:15px;">
                    <td  style="" class="TdBorder td_item K010_td_backcolor3">
                        &nbsp;<asp:Label ID="lblTokuhanUriKingakuBefore" runat="server" Text="123,456,789,012"></asp:Label></td>
                    <td  style="" class="TdBorder td_item K010_td_backcolor5" id="tdTokuhanKeikakuUriKingaku" runat="server">
                        &nbsp;<asp:Label ID="lblTokuhanKeikakuUriKingaku" runat="server" Text="123,456,789,012"></asp:Label></td>
                </tr>
                <tr style="height:15px;">
                    <td  style="" class="TdBorder td_item K010_td_backcolor3">
                        &nbsp;<asp:Label ID="lblFCUriKingakuBefore" runat="server" Text="123,456,789,012"></asp:Label></td>
                    <td  style="" class="TdBorder td_item K010_td_backcolor5" id="tdFCKeikakuUriKingaku" runat="server">
                        &nbsp;<asp:Label ID="lblFCKeikakuUriKingaku" runat="server" Text="123,456,789,012"></asp:Label></td>
                </tr>
                <tr style="height:15px;">
                    <td  style="" class="TdBorder td_item K010_td_backcolor3">
                        &nbsp;<asp:Label ID="lblGoukeiUriKingakuBefore" runat="server" Text="123,456,789,012"></asp:Label></td>
                    <td  style="" class="TdBorder td_item K010_td_backcolor6">
                        &nbsp;<asp:Label ID="lblGoukeiKeikakuUriKingaku" runat="server" Text="123,456,789,012"></asp:Label></td>
                </tr>
            </table>
        </td>
        <td class="">
            <table border="0" cellpadding="0" cellspacing="0" class="">
                <tr >
                    <td  style="width:97px;" class="TdBorder td_title K010_td_backcolor3">
                        <asp:Label ID="lblArariBefore" runat="server" Text="前年"></asp:Label></td>
                    <td  style="width:97px;" class="TdBorder td_title K010_td_backcolor4">
                        <asp:Label ID="lblKeikakuArari" runat="server" Text="計画"></asp:Label></td>
                </tr>
                <tr style="height:15px;">
                    <td  style="" class="TdBorder td_item K010_td_backcolor3">
                        &nbsp;<asp:Label ID="lblEigyouArariBefore" runat="server" Text="123,456,789,012"></asp:Label></td>
                    <td  style="" class="TdBorder td_item K010_td_backcolor5" id="tdEigyouKeikakuArari" runat="server">
                        &nbsp;<asp:Label ID="lblEigyouKeikakuArari" runat="server" Text="123,456,789,012"></asp:Label></td>
                </tr>
                <tr style="height:15px;">
                    <td  style="" class="TdBorder td_item K010_td_backcolor3">
                        &nbsp;<asp:Label ID="lblTokuhanArariBefore" runat="server" Text="123,456,789,012"></asp:Label></td>
                    <td  style="" class="TdBorder td_item K010_td_backcolor5" id="tdTokuhanKeikakuArari" runat="server">
                        &nbsp;<asp:Label ID="lblTokuhanKeikakuArari" runat="server" Text="123,456,789,012"></asp:Label></td>
                </tr>
                <tr style="height:15px;">
                    <td  style="" class="TdBorder td_item K010_td_backcolor3">
                        &nbsp;<asp:Label ID="lblFCArariBefore" runat="server" Text="123,456,789,012"></asp:Label></td>
                    <td  style="" class="TdBorder td_item K010_td_backcolor5" id="tdFCKeikakuArari" runat="server">
                        &nbsp;<asp:Label ID="lblFCKeikakuArari" runat="server" Text="123,456,789,012"></asp:Label></td>
                </tr>
                <tr style="height:15px;">
                    <td  style="" class="TdBorder td_item K010_td_backcolor3">
                        &nbsp;<asp:Label ID="lblGoukeiArariBefore" runat="server" Text="123,456,789,012"></asp:Label></td>
                    <td  style="" class="TdBorder td_item K010_td_backcolor6">
                        &nbsp;<asp:Label ID="lblGoukeiKeikakuArari" runat="server" Text="123,456,789,012"></asp:Label></td>
                </tr>
            </table>
        </td>
    </tr>
</table>

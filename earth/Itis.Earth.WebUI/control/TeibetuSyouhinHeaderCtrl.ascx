<%@ Control Language="vb" AutoEventWireup="false" Codebehind="TeibetuSyouhinHeaderCtrl.ascx.vb"
    Inherits="Itis.Earth.WebUI.TeibetuSyouhinHeaderCtrl" %>
<table class="innerTable itemTableNarrow" cellpadding="0" cellspacing="0" style="width: 100%;
    height: 100%; border-top: 0px; table-layout: fixed;">
    <tr class="shouhinTableTitle">
        <td id="TdSyouhinCdTitle" style="width: 249px; border-top: 0px; border-left: 0px;"
            runat="server">
            商品コード</td>
        <td id="TdSyoudakusyoTitle" style="border-top: 0px; border-left: 3px; width: 77px;"
            runat="server">
            承諾書<br />
            金額</td>
        <td id="TdDenpyouSiireNengappiTitleDisplay" style="border-top: 0px; width: 77px;"
            runat="server">
            伝票仕入<br />
            年月日</td>
        <td class="itemMei_small" style="border-top: 0px; border-left: 3px; width: 77px;">
            工務店請求<br />
            税抜金額</td>
        <td style="border-top: 0px; width: 77px;">
            消費税</td>
        <td style="border-top: 0px; width: 75px;">
            伝票売上<br />
            年月日</td>
        <td style="border-top: 0px; width: 114px;">
            売上年月日</td>
        <td style="border-top: 0px; border-left: 3px; width: 75px;">
            請求書<br />
            発行日</td>
        <td style="border-top: 0px; border-left: 3px; width: 50px;">
            発注書<br />
            金額</td>
        <td style="border-top: 0px; width: 100%;">
            発注書<br />
            確定</td>
        <td id="TdSpacer" runat="server" style="border-top: 0px; border-left: 3px; width: 150px; display: none;"
            rowspan="2">
            &nbsp;</td>
    </tr>
    <!-- 2行目 -->
    <tr class="shouhinTableTitle">
        <td id="TdSyouhinNmTitle" style="border-left: 0px;" runat="server">
            商品名</td>
        <td id="TdSiireSyouhizeiGakuTitle" runat="server" style="border-left: 3px;">
            仕入<br />
            消費税額</td>
        <td id="TdDenpyouSiireNengappiTitle" runat="server">
            伝票仕入<br />
            年月日修正</td>
        <td class="itemMei_small" style="border-left: 3px;">
            実請求<br />
            税抜金額</td>
        <td>
            税込金額</td>
        <td>
            伝票売上<br />
            年月日修正</td>
        <td id="TdUriageSyoriTitle" runat="server">
            売上処理</td>
        <td style="border-left: 3px;">
            <span id="SpanSeikyuuUmu" runat="server">請求<br />
                有無</span></td>
        <td colspan="2" style="border-left: 3px;">
            発注書<br />
            確認日</td>
    </tr>
</table>

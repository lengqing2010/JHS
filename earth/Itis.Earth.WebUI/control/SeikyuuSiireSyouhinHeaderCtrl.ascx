<%@ Control Language="vb" AutoEventWireup="false" Codebehind="SeikyuuSiireSyouhinHeaderCtrl.ascx.vb"
    Inherits="Itis.Earth.WebUI.SeikyuuSiireSyouhinHeaderCtrl" %>
<table class="innerTable itemTableNarrow" cellpadding="0" cellspacing="0" style="width: 100%;
    height: 100%; border-top: 0px; table-layout: fixed;">
    <tr class="shouhinTableTitle" style="height:20px;">
        <td id="TdSyouhinCdTitle" style="width: 80px; border-top: 0px; border-left: 0px;"
            runat="server">
            商品コード</td>
        <td class="itemMei_small" style="border-top: 0px; width: 65px;">
            分類コード</td>
        <td style="border-top: 0px; width: 60px;">
            表示番号</td>
        <td style="border-top: 0px; width: 672px;">
            請求先</td>
    </tr>
    <!-- 2行目 -->
    <tr class="shouhinTableTitle" style="height:20px;">
        <td style="border-left: 0px;" colspan="3">
            商品名</td>
        <td style="width: 672px;">
            仕入先</td>
    </tr>
</table>

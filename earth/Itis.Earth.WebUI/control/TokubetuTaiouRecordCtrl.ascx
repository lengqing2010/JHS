<%@ Control Language="vb" AutoEventWireup="false" Codebehind="TokubetuTaiouRecordCtrl.ascx.vb"
    Inherits="Itis.Earth.WebUI.TokubetuTaiouRecordCtrl" %>
<tr id="Tr1" runat="server">
    <td style="width: 287px;">
        <!-- 以下の項目は順番を変更しないこと -->
        <input type="checkbox" id="CheckBoxTokubetuTaiou" runat="server" />
        <span id="SpanTokubetuTaiouMei" runat="server"></span>
        <input type="hidden" id="HiddenTokubetuTaiouMei" runat="server" />
        <input type="hidden" id="HiddenUpdDatetime" runat="server" />
        <input type="hidden" id="HiddenDisp" runat="server" />
        <input type="hidden" id="HiddenTokubetuTaiouCd" runat="server" />
        <input type="hidden" id="HiddenTokubetuTaiouCheckedMst" runat="server" />
        <input type="hidden" id="HiddenUriKeijyou" runat="server"/>
        <input type="hidden" id="HiddenKkkSyoriFlg" runat="server" />
        <input type="hidden" id="HiddenUpdFlg" runat="server" />
        <input type="hidden" id="HiddenHattyuuKingaku" runat="server" value="0" />
        <input type="button" id="ButtonChkTokubetuTaiou" runat="server" style="display: none" value="特別対応チェックイベント"
            onserverclick="ButtonCheckBoxTokubetuTaiou_ServerClick" />
        <input type="hidden" id="HiddenChkJykyOld" runat="server" />
    </td>
    <td style="width: 74px; text-align:center;">
        <input type="text" id="TextSetteiSaki" class="textCenter readOnlyStyle" readonly="readonly" style="border-style: none; width: 50px;"
            tabindex="-1" runat="server" /><input type="hidden" id="HiddenBunruiCd" runat="server" /><input type="hidden"
            id="HiddenGamenHyoujiNo" runat="server" /><input type="hidden" id="HiddenSetteiSakiStyle" runat="server" />
    </td>
    <td style="width: 42px; text-align:center;">
        <input type="text" id="TextUriKeijyou" class="textCenter readOnlyStyle" readonly="readonly" tabindex="-1"
            style="border-style: none; width: 25px;" runat="server" />
    </td>
    <td style="width: 62px; text-align:center;">
        <input type="text" id="TextKasanSyouhinCd" class="textCenter readOnlyStyle" readonly="readonly" tabindex="-1"
            style="border-style: none; width: 40px;" runat="server" />
        <input type="hidden" id="HiddenKasanSyouhinCdOld" runat="server" />
        <input type="hidden" id="HiddenKasanSyouhinCdMst" runat="server" />
        <input type="hidden" id="HiddenKasanSyouhinCdNew" runat="server" />
        <input type="hidden" id="HiddenKasanSyouhinCdDispOld" runat="server" />
    </td>
    <td style="width: 227px;">
        <input type="text" id="TextKasanSyouhinMei" class="readOnlyStyle" readonly="readonly" tabindex="-1" 
            style="border-style: none; width: 215px;" runat="server" />
        <input type="hidden" id="HiddenKasanSyouhinMeiOld" runat="server" />
        <input type="hidden" id="HiddenKasanSyouhinMeiMst" runat="server" />
        <input type="hidden" id="HiddenKasanSyouhinMeiNew" runat="server" />
        <input type="hidden" id="HiddenKasanSyouhinMeiDispOld" runat="server" />
    </td>
    <td style="width: 74px; text-align:right;">
        <input type="text" id="TextKoumutenSeikyuKasanKingaku" class="kingaku readOnlyStyle" readonly="readonly" tabindex="-1" 
            style="border-style: none; width: 65px;" runat="server" />
        <input type="hidden" id="HiddenKoumutenSeikyuKasanKingakuOld" runat="server" />
        <input type="hidden" id="HiddenKoumutenSeikyuKasanKingakuMst" runat="server" />
        <input type="hidden" id="HiddenKoumutenSeikyuKasanKingakuNew" runat="server" />
        <input type="hidden" id="HiddenKoumutenSeikyuKasanKingakuDispOld" runat="server" />
    </td>
    <td style="width: 74px; text-align:right;">
        <input type="text" id="TextJituSeikyuKasanKingaku"  class="kingaku readOnlyStyle" readonly="readonly" tabindex="-1"
            style="border-style: none; width: 65px;" runat="server" />
        <input type="hidden" id="HiddenJituSeikyuKasanKingakuOld" runat="server" />
        <input type="hidden" id="HiddenJituSeikyuKasanKingakuMst" runat="server" />
        <input type="hidden" id="HiddenJituSeikyuKasanKingakuNew" runat="server" />
        <input type="hidden" id="HiddenJituSeikyuKasanKingakuDispOld" runat="server" />
    </td>
    <td style="width: 49px; text-align:center;">
        <input type="text" id="TextKkkSyoriFlg" class="textCenter readOnlyStyle" readonly="readonly" tabindex="-1"
            style="border-style: none; width: 25px;" runat="server" />
    </td>
</tr>

<%@ Control Language="vb" AutoEventWireup="false" Codebehind="HansokuRecordCtrl.ascx.vb"
    Inherits="Itis.Earth.WebUI.HansokuRecordCtrl" %>
<tr runat="server" id="TrHansokuRecord">
    <td id="TdHassouDate" runat="server">
        <input type="text" class="date" id="TextHassouDate" runat="server" />
        <input id="buttonChgHassouDate" type="button" value="発送日変更時アクション" runat="server"
            style="display: none" />
    </td>
    <td>
        <input type="text" class="itemCd" id="TextSyouhinCd" runat="server" />
        <input id="buttonChgSyouhinCd" type="button" runat="server" style="display: none" />
        <input type="button" id="buttonKensaku" runat="server" value="索" class="itemSearchBtn" />
    </td>
    <td>
        <input type="text" readonly="readonly" class="itemNm readOnlyStyle" id="TextSyouhinMei"
            style="width: 146px;" runat="server" tabindex="-1" />
    </td>
    <td id="TdKoumutenSeikyuu" runat="server">
        <input type="text" class="kingaku" id="TextKoumutenSeikyuuTanka" runat="server" />
        <input id="buttonChgKoumu" type="button" value="工務店請求変更時アクション" runat="server" style="display: none" />
    </td>
    <td>
        <input type="text" class="kingaku" id="TextJituSeikyuuTanka" runat="server" />
        <input id="buttonChgJitu" type="button" value="実請求単価変更時アクション" runat="server" style="display: none" />
    </td>
    <td>
        <input type="text" class="kingaku" id="TextSuuryou" runat="server" style="width: 34px;" />
        <input id="buttonChgSuuryou" type="button" value="数量変更時アクション" runat="server" style="display: none" />
    </td>
    <td id="TdZeinukiKingaku" runat="server">
        <input type="text" readonly="readonly" class="kingaku readOnlyStyle" id="TextZeinukiKingaku"
            runat="server" tabindex="-1" />
    </td>
    <td>
        <input type="text" class="kingaku" id="TextSyouhizeiGaku" runat="server" />
        <input id="buttonChgSyouhiZei" type="button" value="消費税変更時アクション" runat="server" style="display: none" />
    </td>
    <td>
        <input type="text" readonly="readonly" class="kingaku readOnlyStyle" id="TextZeikomiKingaku"
            runat="server" tabindex="-1" />
    </td>
    <td>
        <input type="text" class="date" id="TextSeikyuusyoHakkouBi" runat="server" onblur="checkDate(this);" />
    </td>
    <td>
        <input type="text" class="date" id="TextUriageNengappi" runat="server" />
        <input id="buttonChgUriDate" type="button" value="売上年月日変更時アクション" runat="server" style="display: none" />
    </td>
    <td>
        <input type="text" readonly="readonly" class="date readOnlyStyle" id="TextDenpyouUriDateDisplay"
            runat="server" tabindex="-1" />
    </td>
    <td id="tdDenUriDate" runat="server">
        <input type="text" class="date" id="TextDenpyouUriDate" runat="server" />
        <input id="buttonChgDenUriDate" type="button" value="伝票売上年月日変更時アクション" runat="server"
            style="display: none" />
    </td>
    <td>
        <asp:Label ID="lblSeikyuuType" runat="server" Text="Label"></asp:Label>
    </td>
    <td id="TdGyouSyori" runat="server">
        <input type="button" id="buttonGyouSakujoCall" runat="server" value="削除" />
        <input type="button" id="buttonGyouSakujo" runat="server" value="削除ボタン押下時アクション" style="display: none" />
        <asp:HiddenField ID="hiddenSqlTypeFlg" runat="server" />
        <asp:HiddenField ID="hiddenTyousaSeikyuuSaki" runat="server" />
        <asp:HiddenField ID="hiddenHansokuHinSeikyuuSaki" runat="server" />
        <asp:HiddenField ID="hiddenMiseCd" runat="server" />
        <asp:HiddenField ID="hiddenKeiretuCd" runat="server" />
        <asp:HiddenField ID="hiddenNyuuryokuDate" runat="server" />
        <asp:HiddenField ID="hiddenNyuuryokuDateNo" runat="server" />
        <asp:HiddenField ID="hiddenTxtChgCtrlHansoku" runat="server" />
        <asp:HiddenField ID="hiddenZeiKbn" runat="server" />
        <asp:HiddenField ID="hiddenSoukoCd" runat="server" />
        <asp:HiddenField ID="hiddenUpdateTime" runat="server" />
        <asp:HiddenField ID="hiddenIsFc" runat="server" />
        <asp:HiddenField ID="hiddenOldSyouhinCd" runat="server" />
        <asp:HiddenField ID="hiddenZeiritu" runat="server" />
        <asp:HiddenField ID="HiddenOpenValues" runat="server" />
        <asp:HiddenField ID="HiddenUriKeijyouFlg" runat="server" />
    </td>
</tr>

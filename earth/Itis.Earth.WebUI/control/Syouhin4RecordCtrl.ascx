<%@ Control Language="vb" AutoEventWireup="false" Codebehind="Syouhin4RecordCtrl.ascx.vb"
    Inherits="Itis.Earth.WebUI.Syouhin4RecordCtrl" %>
<%@ Register Src="SeikyuuSiireLinkCtrl.ascx" TagName="SeikyuuSiireLinkCtrl" TagPrefix="uc2" %>
<tr id="TrSyouhin4Record" runat="server">
    <td style="width: 235px; border-left: 0px; border-top: 0px; white-space: nowrap"
        class="itemNm">
        <input type="hidden" id="HiddenKubun" runat="server" /><%-- ãÊï™ --%>
        <input type="hidden" id="HiddenBangou" runat="server" /><%-- î‘çÜ --%>
        <input type="hidden" id="HiddenBunruiCd" runat="server" /><%-- ï™óﬁÉRÅ[Éh --%>
        <input type="hidden" id="HiddenGamenHyoujiNo" runat="server" /><%-- âÊñ ï\é¶NO --%>
        <input type="hidden" id="HiddenLoginUserId" runat="server" /><%-- ÉçÉOÉCÉìÉÜÅ[ÉUÅ[ÇhÇc --%>
        <input type="hidden" id="HiddenUpdDatetime" runat="server" />
        <%-- å†å¿ --%>
        <input type="hidden" id="HiddenIraiGyoumuKengen" runat="server" />
        <input type="hidden" id="HiddenHattyuusyoKanriKengen" runat="server" />
        <input type="hidden" id="HiddenKeiriGyoumuKengen" runat="server" />
        <input type="hidden" id="HiddenKameitenCd" runat="server" />
        <input type="hidden" id="HiddenKingakuFlg" runat="server" />
        <input type="hidden" id="HiddenUriageJyoukyou" runat="server" />
        <input type="hidden" id="HiddenUriageKeijyouDate" runat="server" />
        <input type="hidden" id="HiddenBikou" runat="server" />
        <input type="hidden" id="HiddenIkkatuNyuukinFlg" runat="server" />
        <input type="hidden" id="HiddenSeikyuuType" runat="server" />
        <input type="hidden" id="HiddenKeiretu" runat="server" />
        <input type="hidden" id="HiddenHattyuusyoKakuteiOld" runat="server" />
        <input type="hidden" id="HiddenHattyuusyoFlgOld" runat="server" />
        <input type="hidden" id="HiddenHattyuusyoKingakuOld" runat="server" />
        <input type="hidden" id="HiddenSyouhinKbn3" runat="server" />
        <input type="hidden" id="HiddenJibanTysKaisyaCd" runat="server" />
        <input type="hidden" id="HiddenSyouhinCdOld" runat="server" />
        <input type="hidden" id="HiddenSyouhin4SearchType" runat="server" />
        <input type="hidden" id="HiddenZeikubun" runat="server" />
        <input type="hidden" id="HiddenZeiritu" runat="server" />
        <input type="hidden" id="HiddenKakuteiOld" runat="server" />
        <asp:HiddenField ID="HiddenOpenValuesUriage" runat="server" />
        <asp:HiddenField ID="HiddenOpenValuesSiire" runat="server" />
       <%-- è§ïiÉRÅ[Éh --%>
        <asp:TextBox ID="TextSyouhinCd" runat="server" CssClass="itemCd" MaxLength="8" Width="60px"></asp:TextBox>
        <%-- è§ïiåüçıÉ{É^Éì --%>
        <input type="button" id="ButtonSyouhinKensaku" value="åüçı" class="gyoumuSearchBtn"
            runat="server" onserverclick="ButtonSyouhinKensaku_ServerClick" />
        <input id="ButtonHiddenSyouhinKensaku" runat="server" style="display: none" type="button"
            value="åüçı(îÒï\é¶)" />
        <%-- ämíËãÊï™ --%>
        <asp:DropDownList ID="SelectKakutei" runat="server" AutoPostBack="true" OnSelectedIndexChanged="SelectKakutei_SelectedIndexChanged">
            <asp:ListItem Value="0">ñ¢ämíË</asp:ListItem>
            <asp:ListItem Value="1">ämíË</asp:ListItem>
        </asp:DropDownList><span id="SpanKakutei" runat="server"></span>
        <%-- êøãÅêÊ/édì¸êÊÉäÉìÉN --%>
        <uc2:SeikyuuSiireLinkCtrl ID="ucSeikyuuSiireLink" runat="server" />
        <br />
        <%-- è§ïiñº --%>
        <span id="SpanSyouhinMei" class="itemNm" runat="server" style="width: 200px"></span>
    </td>
    <td id="Td1" runat="server" style="border-left: solid 3px gray; border-top: 0px;
        width: 73px;">
        <%-- è≥ë¯èëã‡äz --%>
        <asp:TextBox ID="TextSyoudakusyoKingaku" runat="server" CssClass="kingaku" MaxLength="7"
            Width="65px">
        </asp:TextBox><br />
        <%-- édì¸è¡îÔê≈äz --%>
        <asp:TextBox ID="TextSiireSyouhizeiGaku" runat="server" CssClass="kingaku" Width="65px"
            MaxLength="7"></asp:TextBox>
    </td>
    <td style="border-top: 0px; width: 73px;" id="TdDenpyouSiireNengappi" runat="server">
        <%-- ì`ï[édì¸îNåéì˙(éQè∆) --%>
        <asp:TextBox ID="TextDenpyouSiireNengappiDisplay" runat="server" CssClass="date readOnlyStyle2"
            ReadOnly="True" Width="65px"></asp:TextBox><br />
        <%-- ì`ï[édì¸îNåéì˙(èCê≥) --%>
        <asp:TextBox ID="TextDenpyouSiireNengappi" runat="server" CssClass="date" MaxLength="10"
            Width="65px" AutoPostBack="False"></asp:TextBox>
    </td>
    <td style="border-top: 0px; border-left: solid 3px gray; width: 73px;">
        <%-- çHñ±ìXêøãÅã‡äz --%>
        <asp:TextBox ID="TextKoumutenSeikyuuGaku" runat="server" CssClass="kingaku" MaxLength="7"
            Width="65px"></asp:TextBox><br />
        <%-- é¿êøãÅã‡äz --%>
        <asp:TextBox ID="TextJituSeikyuuGaku" runat="server" CssClass="kingaku" Width="65px"
            MaxLength="7"></asp:TextBox>
        <%-- å©êœèëçÏê¨ì˙(îÒï\é¶) --%>
        <asp:TextBox ID="TextMitumorisyoSakuseiDate" runat="server" CssClass="date" MaxLength="10"
            Visible="false" Width="65px"></asp:TextBox>
    </td>
    <td style="border-top: 0px; width: 73px;">
        <%-- è¡îÔê≈äz --%>
        <asp:TextBox ID="TextSyouhizeiGaku" runat="server" CssClass="kingaku" MaxLength="7"
            Width="65px"></asp:TextBox><br />
        <%-- ê≈çûã‡äz --%>
        <asp:TextBox ID="TextZeikomiKingaku" runat="server" BorderStyle="None" CssClass="kingaku readOnlyStyle2"
            ReadOnly="True" Width="65px"></asp:TextBox>
    </td>
    <td style="border-top: 0px; width: 71px;">
        <%-- ì`ï[îÑè„îNåéì˙(éQè∆) --%>
        <asp:TextBox ID="TextDenpyouUriageNengappiDisplay" runat="server" CssClass="date"
            MaxLength="10" Width="65px"></asp:TextBox>
        <%-- ì`ï[îÑè„îNåéì˙(èCê≥) --%>
        <asp:TextBox ID="TextDenpyouUriageNengappi" runat="server" CssClass="date readOnlyStyle2"
            ReadOnly="True" Width="65px">
        </asp:TextBox><br />
    </td>
    <td style="border-top: 0px; width: 115px;">
        <%-- îÑè„îNåéì˙ --%>
        <asp:TextBox ID="TextUriageNengappi" runat="server" CssClass="date" MaxLength="10"
            Width="65px"></asp:TextBox><br />
        <%-- îÑè„èàóù --%>
        <asp:DropDownList ID="SelectUriageSyori" runat="server" AutoPostBack="true" Style="font-size: 10px;
            width: 36px;" OnSelectedIndexChanged="SelectUriageSyori_SelectedIndexChanged">
            <asp:ListItem Selected="True" Value="0">ñ¢</asp:ListItem>
            <asp:ListItem Value="1">çœ</asp:ListItem>
        </asp:DropDownList><span id="SpanUriageSyori" runat="server"></span>
        <%-- îÑè„ì˙ --%>
        <asp:TextBox ID="TextUriageDate" runat="server" CssClass="date readOnlyStyle2" MaxLength="10"
            BorderStyle="None" ReadOnly="True"></asp:TextBox>
    </td>
    <td colspan="2" style="border-top: 0px; border-left: solid 3px gray; width: 76px;">
        <%-- êøãÅèëî≠çsì˙ --%>
        <asp:TextBox ID="TextSeikyuusyoHakkouDate" runat="server" CssClass="date" MaxLength="10"
            Width="65px"></asp:TextBox>
        <br />
        <%-- êøãÅóLñ≥ --%>
        <asp:DropDownList ID="SelectSeikyuuUmu" runat="server" AutoPostBack="true" Style="font-size: 10px;
            width: 36px;" OnSelectedIndexChanged="SelectSeikyuuUmu_SelectedIndexChanged">
            <asp:ListItem Selected="True" Value="1">óL</asp:ListItem>
            <asp:ListItem Value="0">ñ≥</asp:ListItem>
        </asp:DropDownList><span id="SpanSeikyuuUmu" runat="server"></span>
    </td>
    <td style="text-align: center; border-top: 0px; border-left: solid 3px gray; width: 100%;">
        <%-- î≠íçèëã‡äz --%>
        <asp:TextBox ID="TextHattyuusyoKingaku" Font-Size="Small" runat="server" CssClass="kingaku"
            MaxLength="7" Width="58px"></asp:TextBox>
        <%-- î≠íçèëämíË --%>
        <asp:DropDownList ID="SelectHattyuusyoKakutei" runat="server" AutoPostBack="true"
            Style="font-size: 10px; width: 36px;" OnSelectedIndexChanged="SelectHattyuusyoKakutei_SelectedIndexChanged">
            <asp:ListItem Selected="True" Value="0">ñ¢</asp:ListItem>
            <asp:ListItem Value="1">äm</asp:ListItem>
        </asp:DropDownList><span id="SpanHattyuusyoKakutei" runat="server"></span>
        <br />
        <%-- î≠íçèëämîFì˙ --%>
        <asp:TextBox ID="TextHattyuusyoKakuninDate" runat="server" CssClass="date" MaxLength="10"
            Width="65px"></asp:TextBox>
        <asp:HiddenField ID="HiddenTargetId" runat="server" />
    </td>
</tr>

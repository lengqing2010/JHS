<%@ Control Language="vb" AutoEventWireup="false" Codebehind="IkkatuHenkouTysSyouhin2RecordCtrl.ascx.vb"
    Inherits="Itis.Earth.WebUI.IkkatuHenkouTysSyouhin2RecordCtrl" %>
<%@ Register Src="TokubetuTaiouToolTipCtrl.ascx" TagName="TokubetuTaiouToolTipCtrl" TagPrefix="uc" %>
<tr id="TrTysSyouhin_2_1" runat="server">
    <td>
        <%--4行共通HiddenField--%>
        <asp:HiddenField runat="server" ID="HiddenKameitenCode" />
        <asp:HiddenField runat="server" ID="HiddenTysKaisyaCd" />
        <asp:HiddenField runat="server" ID="HiddenTysKaisyaJigyousyoCd" />
        <asp:HiddenField runat="server" ID="HiddenKeiretuCd" />
        <asp:HiddenField runat="server" ID="HiddenTorikeshi" />
        <asp:HiddenField runat="server" ID="HiddenUriDateItem1" />
        <%--1行ごとのHiddenField--%>
        <asp:HiddenField runat="server" ID="HiddenDbValue_2_1" />
        <asp:HiddenField runat="server" ID="HiddenChgValue_2_1" />
        <asp:HiddenField runat="server" ID="HiddenDbKingaku_2_1" />
        <asp:HiddenField runat="server" ID="HiddenChgKingaku_2_1" />
        <asp:HiddenField runat="server" ID="HiddenKbn_2_1" />
        <asp:HiddenField runat="server" ID="HiddenHosyousyoNo_2_1" />
        <asp:HiddenField runat="Server" ID="HiddenBunruiCd_2_1" />
        <asp:HiddenField runat="server" ID="HiddenZeiKbn_2_1" />
        <asp:HiddenField runat="server" ID="HiddenUriDate_2_1" />
        <asp:HiddenField runat="server" ID="HiddenHattyuusyoKakuteiFlg_2_1" />
        <asp:HiddenField runat="server" ID="HiddenHattyuusyoGaku_2_1" />
        <asp:HiddenField runat="server" ID="HiddenAutoKingakuFlg_2_1" />
        <asp:HiddenField runat="server" ID="HiddenManualKingakuFlg_2_1" />
        <asp:HiddenField runat="server" ID="HiddenBothKingakuChgFlg_2_1" />
        <asp:HiddenField runat="server" ID="HiddenTysSeikyuuSaki_2_1" />
        <asp:HiddenField runat="server" ID="HiddenSeikyuuSakiCd_2_1" />
        <%--自動計算チェックボックス--%>
        <asp:CheckBox runat="server" ID="CheckAutoCalc_2_1" /><span id="SPAN_Check_2_1" runat="server"></span></td>
    <td>
        <%-- JavaScript(rowDelete)のchildNodesで取得する為、このTD内の順番は変更しないこと --%>
        <asp:TextBox runat="server" ID="TextKokyakuBangou_2_1" Style="width: 80px" CssClass="readOnlyStyle"
            ReadOnly="true" TabIndex="-1" />
        <asp:HiddenField runat="server" ID="HiddenGamenHyoujiNo_2_1" />
        <asp:HiddenField runat="server" ID="HiddenRowDisplay_2_1" />
    </td>
    <td style="text-align: center;">
        <asp:TextBox runat="server" ID="TextNo_2_1" Style="width: 7px; text-align: center;"
            ReadOnly="true" TabIndex="-1" />
    </td>
    <%-- --%>
    <td>
        <asp:DropDownList runat="server" ID="SelectSyouhin_2_1" Style="width: 280px" CssClass="hissu">
        </asp:DropDownList><span id="SPAN_Syouhin_2_1" runat="server"></span>
        <asp:HiddenField runat="server" ID="HiddenSelectSyouhin_2_1" />
        <asp:HiddenField runat="server" ID="HiddenSyouhinKingaku_2_1" /><uc:TokubetuTaiouToolTipCtrl
        ID="ucTokubetuTaiouToolTipCtrl_2_1" runat="server" />
    </td>
    <td class="textCenter">
        <asp:TextBox runat="server" ID="TextUriageSyori_2_1" Style="width: 40px; color: red;
            font-weight: bold;" CssClass="readOnlyStyle textCenter" ReadOnly="true" TabIndex="-1" /></td>
    <td>
        <asp:TextBox runat="server" ID="TextKoumutenKingaku_2_1" Style="width: 70px" CssClass="kingaku"
            MaxLength="7" />
    </td>
    <td>
        <asp:TextBox runat="server" ID="TextJituSeikyuuKingaku_2_1" Style="width: 70px" CssClass="kingaku"
            MaxLength="7" />
    </td>
    <td>
        <asp:TextBox runat="server" ID="TextSyoudakusyoKingaku_2_1" Style="width: 70px" CssClass="kingaku"
            MaxLength="7" />
    </td>
    <td style="text-align: center;">
        <asp:DropDownList runat="server" ID="SelectSeikyuuUmu_2_1" Style="width: 38px">
            <asp:ListItem Value="1" Text="有" />
            <asp:ListItem Value="0" Text="無" />
        </asp:DropDownList><span id="SPAN_Seikyuu_2_1" runat="server"></span>
    </td>
    <td style="text-align: center;">
        <input type="button" id="ButtonAdd_2_1" runat="server" class="gyoumuSearchBtn" value="追加" />
    </td>
    <td style="text-align: center;">
        <input type="button" id="ButtonDelete_2_1" runat="server" class="gyoumuSearchBtn"
            value="削除" />
    </td>
</tr>
<tr id="TrTysSyouhin_2_2" runat="server">
    <td>
        <asp:HiddenField runat="server" ID="HiddenDbValue_2_2" />
        <asp:HiddenField runat="server" ID="HiddenChgValue_2_2" />
        <asp:HiddenField runat="server" ID="HiddenDbKingaku_2_2" />
        <asp:HiddenField runat="server" ID="HiddenChgKingaku_2_2" />
        <asp:HiddenField runat="server" ID="HiddenKbn_2_2" />
        <asp:HiddenField runat="server" ID="HiddenHosyousyoNo_2_2" />
        <asp:HiddenField runat="Server" ID="HiddenBunruiCd_2_2" />
        <asp:HiddenField runat="server" ID="HiddenZeiKbn_2_2" />
        <asp:HiddenField runat="server" ID="HiddenUriDate_2_2" />
        <asp:HiddenField runat="server" ID="HiddenHattyuusyoKakuteiFlg_2_2" />
        <asp:HiddenField runat="server" ID="HiddenHattyuusyoGaku_2_2" />
        <asp:HiddenField runat="server" ID="HiddenAutoKingakuFlg_2_2" />
        <asp:HiddenField runat="server" ID="HiddenManualKingakuFlg_2_2" />
        <asp:HiddenField runat="server" ID="HiddenBothKingakuChgFlg_2_2" />
        <asp:HiddenField runat="server" ID="HiddenTysSeikyuuSaki_2_2" />
        <asp:HiddenField runat="server" ID="HiddenSeikyuuSakiCd_2_2" />
        <%--自動計算チェックボックス--%>
        <asp:CheckBox runat="server" ID="CheckAutoCalc_2_2" /><span id="SPAN_Check_2_2" runat="server"></span></td>
    <td>
        <%-- JavaScript(rowDelete)のchildNodesで取得する為、このTD内の順番は変更しないこと --%>
        <asp:TextBox runat="server" ID="TextKokyakuBangou_2_2" Style="width: 80px" CssClass="readOnlyStyle"
            ReadOnly="true" TabIndex="-1" />
        <asp:HiddenField runat="server" ID="HiddenGamenHyoujiNo_2_2" />
        <asp:HiddenField runat="server" ID="HiddenRowDisplay_2_2" />
    </td>
    <td style="width: 10px; text-align: center;">
        <asp:TextBox runat="server" ID="TextNo_2_2" Style="width: 7px; text-align: center;" />
    </td>
    <%-- --%>
    <td>
        <asp:DropDownList runat="server" ID="SelectSyouhin_2_2" Style="width: 280px" CssClass="hissu">
        </asp:DropDownList><span id="SPAN_Syouhin_2_2" runat="server"></span>
        <asp:HiddenField runat="server" ID="HiddenSelectSyouhin_2_2" />
        <asp:HiddenField runat="server" ID="HiddenSyouhinKingaku_2_2" /><uc:TokubetuTaiouToolTipCtrl
        ID="ucTokubetuTaiouToolTipCtrl_2_2" runat="server" Visible="false" />
    </td>
    <td class="textCenter">
        <asp:TextBox runat="server" ID="TextUriageSyori_2_2" Style="width: 40px; color: red;
            font-weight: bold;" CssClass="readOnlyStyle textCenter" ReadOnly="true" TabIndex="-1" /></td>
    <td>
        <asp:TextBox runat="server" ID="TextKoumutenKingaku_2_2" Style="width: 70px" CssClass="kingaku"
            MaxLength="7" />
    </td>
    <td>
        <asp:TextBox runat="server" ID="TextJituSeikyuuKingaku_2_2" Style="width: 70px" CssClass="kingaku"
            MaxLength="7" />
    </td>
    <td>
        <asp:TextBox runat="server" ID="TextSyoudakusyoKingaku_2_2" Style="width: 70px" CssClass="kingaku"
            MaxLength="7" />
    </td>
    <td style="text-align: center;">
        <asp:DropDownList runat="server" ID="SelectSeikyuuUmu_2_2" Style="width: 38px">
            <asp:ListItem Value="1" Text="有" />
            <asp:ListItem Value="0" Text="無" />
        </asp:DropDownList><span id="SPAN_Seikyuu_2_2" runat="server"></span>
    </td>
    <td style="text-align: center;">
        <input type="button" id="ButtonAdd_2_2" runat="server" class="gyoumuSearchBtn" value="追加" />
    </td>
    <td style="text-align: center;">
        <input type="button" id="ButtonDelete_2_2" runat="server" class="gyoumuSearchBtn"
            value="削除" />
    </td>
</tr>
<tr id="TrTysSyouhin_2_3" runat="server">
    <td>
        <asp:HiddenField runat="server" ID="HiddenDbValue_2_3" />
        <asp:HiddenField runat="server" ID="HiddenChgValue_2_3" />
        <asp:HiddenField runat="server" ID="HiddenDbKingaku_2_3" />
        <asp:HiddenField runat="server" ID="HiddenChgKingaku_2_3" />
        <asp:HiddenField runat="server" ID="HiddenKbn_2_3" />
        <asp:HiddenField runat="server" ID="HiddenHosyousyoNo_2_3" />
        <asp:HiddenField runat="Server" ID="HiddenBunruiCd_2_3" />
        <asp:HiddenField runat="server" ID="HiddenZeiKbn_2_3" />
        <asp:HiddenField runat="server" ID="HiddenUriDate_2_3" />
        <asp:HiddenField runat="server" ID="HiddenHattyuusyoKakuteiFlg_2_3" />
        <asp:HiddenField runat="server" ID="HiddenHattyuusyoGaku_2_3" />
        <asp:HiddenField runat="server" ID="HiddenAutoKingakuFlg_2_3" />
        <asp:HiddenField runat="server" ID="HiddenManualKingakuFlg_2_3" />
        <asp:HiddenField runat="server" ID="HiddenBothKingakuChgFlg_2_3" />
        <asp:HiddenField runat="server" ID="HiddenTysSeikyuuSaki_2_3" />
        <asp:HiddenField runat="server" ID="HiddenSeikyuuSakiCd_2_3" />
        <%--自動計算チェックボックス--%>
        <asp:CheckBox runat="server" ID="CheckAutoCalc_2_3" /><span id="SPAN_Check_2_3" runat="server"></span></td>
    <td>
        <%-- JavaScript(rowDelete)のchildNodesで取得する為、このTD内の順番は変更しないこと --%>
        <asp:TextBox runat="server" ID="TextKokyakuBangou_2_3" Style="width: 80px" CssClass="readOnlyStyle"
            ReadOnly="true" TabIndex="-1" />
        <asp:HiddenField runat="server" ID="HiddenGamenHyoujiNo_2_3" />
        <asp:HiddenField runat="server" ID="HiddenRowDisplay_2_3" />
    </td>
    <td style="width: 10px; text-align: center;">
        <asp:TextBox runat="server" ID="TextNo_2_3" Style="width: 7px; text-align: center;" />
    </td>
    <%-- --%>
    <td>
        <asp:DropDownList runat="server" ID="SelectSyouhin_2_3" Style="width: 280px" CssClass="hissu">
        </asp:DropDownList><span id="SPAN_Syouhin_2_3" runat="server"></span>
        <asp:HiddenField runat="server" ID="HiddenSelectSyouhin_2_3" />
        <asp:HiddenField runat="server" ID="HiddenSyouhinKingaku_2_3" /><uc:TokubetuTaiouToolTipCtrl
        ID="ucTokubetuTaiouToolTipCtrl_2_3" runat="server" Visible="false" />
    </td>
    <td class="textCenter">
        <asp:TextBox runat="server" ID="TextUriageSyori_2_3" Style="width: 40px; color: red;
            font-weight: bold;" CssClass="readOnlyStyle textCenter" ReadOnly="true" TabIndex="-1" /></td>
    <td>
        <asp:TextBox runat="server" ID="TextKoumutenKingaku_2_3" Style="width: 70px" CssClass="kingaku"
            MaxLength="7" />
    </td>
    <td>
        <asp:TextBox runat="server" ID="TextJituSeikyuuKingaku_2_3" Style="width: 70px" CssClass="kingaku"
            MaxLength="7" />
    </td>
    <td>
        <asp:TextBox runat="server" ID="TextSyoudakusyoKingaku_2_3" Style="width: 70px" CssClass="kingaku"
            MaxLength="7" />
    </td>
    <td style="text-align: center;">
        <asp:DropDownList runat="server" ID="SelectSeikyuuUmu_2_3" Style="width: 38px">
            <asp:ListItem Value="1" Text="有" />
            <asp:ListItem Value="0" Text="無" />
        </asp:DropDownList><span id="SPAN_Seikyuu_2_3" runat="server"></span>
    </td>
    <td style="text-align: center;">
        <input type="button" id="ButtonAdd_2_3" runat="server" class="gyoumuSearchBtn" value="追加" />
    </td>
    <td style="text-align: center;">
        <input type="button" id="ButtonDelete_2_3" runat="server" class="gyoumuSearchBtn"
            value="削除" />
    </td>
</tr>
<tr id="TrTysSyouhin_2_4" runat="server">
    <td>
        <asp:HiddenField runat="server" ID="HiddenDbValue_2_4" />
        <asp:HiddenField runat="server" ID="HiddenChgValue_2_4" />
        <asp:HiddenField runat="server" ID="HiddenDbKingaku_2_4" />
        <asp:HiddenField runat="server" ID="HiddenChgKingaku_2_4" />
        <asp:HiddenField runat="server" ID="HiddenKbn_2_4" />
        <asp:HiddenField runat="server" ID="HiddenHosyousyoNo_2_4" />
        <asp:HiddenField runat="Server" ID="HiddenBunruiCd_2_4" />
        <asp:HiddenField runat="server" ID="HiddenZeiKbn_2_4" />
        <asp:HiddenField runat="server" ID="HiddenUriDate_2_4" />
        <asp:HiddenField runat="server" ID="HiddenHattyuusyoKakuteiFlg_2_4" />
        <asp:HiddenField runat="server" ID="HiddenHattyuusyoGaku_2_4" />
        <asp:HiddenField runat="server" ID="HiddenAutoKingakuFlg_2_4" />
        <asp:HiddenField runat="server" ID="HiddenManualKingakuFlg_2_4" />
        <asp:HiddenField runat="server" ID="HiddenBothKingakuChgFlg_2_4" />
        <asp:HiddenField runat="server" ID="HiddenTysSeikyuuSaki_2_4" />
        <asp:HiddenField runat="server" ID="HiddenSeikyuuSakiCd_2_4" />
        <%--自動計算チェックボックス--%>
        <asp:CheckBox runat="server" ID="CheckAutoCalc_2_4" /><span id="SPAN_Check_2_4" runat="server"></span></td>
    <td>
        <%-- JavaScript(rowDelete)のchildNodesで取得する為、このTD内の順番は変更しないこと --%>
        <asp:TextBox runat="server" ID="TextKokyakuBangou_2_4" Style="width: 80px" CssClass="readOnlyStyle"
            ReadOnly="true" TabIndex="-1" />
        <asp:HiddenField runat="server" ID="HiddenGamenHyoujiNo_2_4" />
        <asp:HiddenField runat="server" ID="HiddenRowDisplay_2_4" />
    </td>
    <td style="width: 10px; text-align: center;">
        <asp:TextBox runat="server" ID="TextNo_2_4" Style="width: 7px; text-align: center;" />
    </td>
    <%-- --%>
    <td>
        <asp:DropDownList runat="server" ID="SelectSyouhin_2_4" Style="width: 280px" CssClass="hissu">
        </asp:DropDownList><span id="SPAN_Syouhin_2_4" runat="server"></span>
        <asp:HiddenField runat="server" ID="HiddenSelectSyouhin_2_4" />
        <asp:HiddenField runat="server" ID="HiddenSyouhinKingaku_2_4" /><uc:TokubetuTaiouToolTipCtrl
        ID="ucTokubetuTaiouToolTipCtrl_2_4" runat="server" Visible="false" />
    </td>
    <td class="textCenter">
        <asp:TextBox runat="server" ID="TextUriageSyori_2_4" Style="width: 40px; color: red;
            font-weight: bold;" CssClass="readOnlyStyle textCenter" ReadOnly="true" TabIndex="-1" /></td>
    <td>
        <asp:TextBox runat="server" ID="TextKoumutenKingaku_2_4" Style="width: 70px" CssClass="kingaku"
            MaxLength="7" />
    </td>
    <td>
        <asp:TextBox runat="server" ID="TextJituSeikyuuKingaku_2_4" Style="width: 70px" CssClass="kingaku"
            MaxLength="7" />
    </td>
    <td>
        <asp:TextBox runat="server" ID="TextSyoudakusyoKingaku_2_4" Style="width: 70px" CssClass="kingaku"
            MaxLength="7" />
    </td>
    <td style="text-align: center;">
        <asp:DropDownList runat="server" ID="SelectSeikyuuUmu_2_4" Style="width: 38px">
            <asp:ListItem Value="1" Text="有" />
            <asp:ListItem Value="0" Text="無" />
        </asp:DropDownList><span id="SPAN_Seikyuu_2_4" runat="server"></span>
    </td>
    <td style="text-align: center;">
        <input type="button" id="ButtonAdd_2_4" runat="server" class="gyoumuSearchBtn" value="追加" />
    </td>
    <td style="text-align: center;">
        <input type="button" id="ButtonDelete_2_4" runat="server" class="gyoumuSearchBtn"
            value="削除" />
    </td>
</tr>

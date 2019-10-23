<%@ Control Language="vb" AutoEventWireup="false" Codebehind="IkkatuHenkouTysSyouhin1RecordCtrl.ascx.vb"
    Inherits="Itis.Earth.WebUI.IkkatuHenkouTysSyouhin1RecordCtrl" %>
<%@ Register Src="TokubetuTaiouToolTipCtrl.ascx" TagName="TokubetuTaiouToolTipCtrl" TagPrefix="uc" %>
<tr id="TrTysSyouhin_1_1" runat="server">
    <td rowspan="2" style="width: 20px;">
        <asp:HiddenField runat="server" ID="HiddenDbValue" />
        <asp:HiddenField runat="server" ID="HiddenOpenValuesGenka" />
        <asp:HiddenField runat="server" ID="HiddenOpenValuesHanbai" />
        <asp:HiddenField runat="server" ID="HiddenOpenValuesSyouhinKkk" />
        <asp:HiddenField runat="server" ID="HiddenChgValue" />
        <asp:HiddenField runat="server" ID="HiddenDbKingaku" />
        <asp:HiddenField runat="server" ID="HiddenChgKingaku" />
        <asp:HiddenField runat="server" ID="HiddenKbn" />
        <asp:HiddenField runat="server" ID="HiddenHosyousyoNo" />
        <asp:HiddenField runat="server" ID="HiddenBunruiCd" />
        <asp:HiddenField runat="server" ID="HiddenGamenHyoujiNo" />
        <asp:HiddenField runat="server" ID="HiddenSyouhinKbn" />
        <asp:HiddenField runat="server" ID="HiddenTorikeshi" />
        <asp:HiddenField runat="server" ID="HiddenKeiretuCd" />
        <asp:HiddenField runat="server" ID="HiddenEigyousyoCd" />
        <asp:HiddenField runat="server" ID="HiddenTysKaisyaCd" />
        <asp:HiddenField runat="server" ID="HiddenTysKaisyaJigyousyoCd" />
        <asp:HiddenField runat="server" ID="HiddenTysSeikyuuSaki" />
        <asp:HiddenField runat="server" ID="HiddenSeikyuuSakiCd" />
        <asp:HiddenField runat="server" ID="HiddenTysGaiyou" />
        <asp:HiddenField runat="server" ID="HiddenKakakuSetteiBasyo" />
        <asp:HiddenField runat="server" ID="HiddenIraiTousuu" />
        <asp:HiddenField runat="server" ID="HiddenTatemonoYoutoNo" />
        <asp:HiddenField runat="server" ID="HiddenStatusIf" />
        <asp:HiddenField runat="server" ID="HiddenHattyuusyoGaku" />
        <asp:HiddenField runat="server" ID="HiddenUriDate" />
        <asp:HiddenField runat="server" ID="HiddenAutoKingakuFlg" />
        <asp:HiddenField runat="server" ID="HiddenManualKingakuFlg" />
        <asp:HiddenField runat="server" ID="HiddenBothKingakuChgFlg" />
        <asp:HiddenField runat="server" ID="HiddenAddDatetimeJiban" />
        <asp:HiddenField runat="server" ID="HiddenUpdDatetimeJiban" />
        <asp:HiddenField runat="server" ID="HiddenSdsHenkouKahi" />
        <asp:HiddenField runat="server" ID="HiddenKmtnHenkouKahi" />
        <asp:HiddenField runat="server" ID="HiddenJituGakuHenkouKahi" />
        <%--自動計算チェックボックス--%>
        <asp:CheckBox runat="server" ID="CheckAutoCalc" /><span id="SPAN_Check" runat="server"></span></td>
    <td colspan="1" style="width: 80px">
        <asp:TextBox runat="server" ID="TextKokyakuBangou" Style="width: 80px" CssClass="readOnlyStyle"
            ReadOnly="true" TabIndex="-1" />
    </td>
    <td colspan="4" style="width: 248px;">
        <nobr><asp:TextBox runat="server" ID="TextTysHouhouCode" CssClass="pullCd" MaxLength="2" /><asp:DropDownList
            runat="server" ID="SelectTysHouhou" Style="width: 212px" CssClass="hissu">
        </asp:DropDownList><span id="SPAN_TysHouhou" runat="server"></span>
        <asp:HiddenField runat="server" ID="HiddenTysHouhouCode" /></nobr>
    </td>
    <td id="TdTyousakaisya" runat="server" colspan="10" style="width: 323px">
        <asp:TextBox runat="server" ID="TextTyousakaisyaCode" Style="width: 60px" CssClass="codeNumber hissu"
            MaxLength="7" /><input type="button" runat="server" id="ButtonSearchTyousakaisya"
                class="gyoumuSearchBtn" value="検索" />
        <asp:HiddenField ID="HiddenTyousaKaishaCdOld" runat="server" />
        <asp:HiddenField ID="tyousakaisyaSearchType" runat="server" />
        <asp:TextBox runat="server" ID="TextTyousakaisyaName" Style="width: 200px" CssClass="readOnlyStyle"
            ReadOnly="true" TabIndex="-1" />
    </td>
    <td colspan="5">
        <asp:TextBox runat="server" ID="TextKameitenCode" Style="width: 38px" CssClass="readOnlyStyle"
            ReadOnly="true" TabIndex="-1" />
        <asp:TextBox runat="server" ID="TextKameitenName" Style="width: 105px" CssClass="readOnlyStyle"
            ReadOnly="true" TabIndex="-1" />
        <asp:TextBox runat="server" ID="TextTorikesiRiyuu" Style="width: 60px" CssClass="readOnlyStyle"
            ReadOnly="true" TabIndex="-1" />
    </td>
</tr>
<tr id="TrTysSyouhin_1_2" runat="server">
    <td colspan="4">
        <asp:TextBox runat="server" ID="TextSesyuName" Style="width: 270px" MaxLength="50"
            CssClass="readOnlyStyle" ReadOnly="true" TabIndex="-1" />
    </td>
    <td colspan="1" class="textCenter">
        <asp:TextBox runat="server" ID="TextUriageSyori" Style="color: red; font-weight: bold;
            width: 45px;" CssClass="readOnlyStyle textCenter" ReadOnly="true" TabIndex="-1" />
    </td>
    <td colspan="7">
        <asp:DropDownList runat="server" ID="SelectSyouhin1" Style="width: 220px" CssClass="hissu">
        </asp:DropDownList><span id="SPAN_Syouhin1" runat="server"></span>
        <asp:HiddenField runat="server" ID="HiddenSyouhin1Code" /><uc:TokubetuTaiouToolTipCtrl
            ID="ucTokubetuTaiouToolTipCtrl" runat="server" />
    </td>
    <td colspan="3" class="textCenter">
        <asp:TextBox runat="server" ID="TextKoumutenKingaku" Style="width: 65px" CssClass="kingaku"
            MaxLength="7" />
    </td>
    <td colspan="2" class="textCenter">
        <asp:TextBox runat="server" ID="TextJituSeikyuuKingaku" Style="width: 66px" CssClass="kingaku"
            MaxLength="7" />
    </td>
    <td colspan="2" class="textCenter">
        <asp:TextBox runat="server" ID="TextSyoudakusyoKingaku" Style="width: 66px" CssClass="kingaku"
            MaxLength="7" />
    </td>
    <td colspan="1" style="text-align: center; padding-left: 0px;">
        <asp:DropDownList runat="server" ID="SelectSeikyuuUmu" Style="width: 38px; margin-left: 4px;">
            <asp:ListItem Value="1" Text="有" />
            <asp:ListItem Value="0" Text="無" />
        </asp:DropDownList><span id="SPAN_Seikyuu" runat="server"></span>
    </td>
</tr>

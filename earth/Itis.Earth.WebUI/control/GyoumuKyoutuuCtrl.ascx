<%@ Control Language="vb" AutoEventWireup="false" Codebehind="GyoumuKyoutuuCtrl.ascx.vb"
    Inherits="Itis.Earth.WebUI.GyoumuKyoutuuCtrl" %>
<%@ Import Namespace="Itis.Earth.BizLogic" %>
<%@ Import Namespace="Itis.Earth.Utilities" %>

<script language="javascript" type="text/javascript">

    //jüíÊÏXÌ
    function changeHaki() {
        var objBtnChangeHaki= objEBI("<%= changeHaki.clientID %>");
        objBtnChangeHaki.click();
    }

    //Á¿XõðÄÑo·
    function callKameitenSearch(obj){
        objEBI("<%= kameitenSearchType.clientID %>").value = "";
        if(obj.value == ""){
            objEBI("<%= kameitenSearchType.clientID %>").value = "1";
            objEBI("<%= ButtonKameitenSearch.clientID %>").click();
        }
    }

</script>

<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
</asp:ScriptManagerProxy>
<asp:UpdatePanel ID="irai1MainUpdatePanel" UpdateMode="Conditional" runat="server"
    RenderMode="Inline">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="changeHaki" />
    </Triggers>
    <ContentTemplate>
        <!-- hiddenÚ-->
        <input type="hidden" id="HiddenKubun" runat="server" />
        <input type="hidden" id="HiddenUpdDatetime" runat="server" />
        <input type="hidden" id="iraiSessionKey" runat="server" />
        <input type="hidden" id="actMode" runat="server" />
        <input type="hidden" id="updateDateTime" runat="server" />
        <input type="hidden" id="lastUpdateUserId" runat="server" />
        <input type="hidden" id="lastUpdateUserNm" runat="server" />
        <input type="hidden" id="lastUpdateDateTime" runat="server" />
        <input type="hidden" id="errorData" runat="server" />
        <input type="hidden" id="kakuninOpenFlg" runat="server" />
        <input type="hidden" id="dateYesterday" runat="server" />
        <input type="hidden" id="HiddenTyousaKaisyaCd" runat="server" />
        <input type="hidden" id="HiddenTyousaKaisyaCd1" runat="server" />
        <input type="hidden" id="HiddenTyousaKaisyaCd2" runat="server" />
        <input type="hidden" id="HiddenTyousaKaisyaMei" runat="server" />
        <input type="hidden" id="HiddenTyousaHouhouCd" runat="server" />
        <input type="hidden" id="HiddenTyousaHouhouMei" runat="server" />
        <input type="hidden" id="HiddenKoujiKaisyaCd" runat="server" />
        <input type="hidden" id="HiddenKouzou" runat="server" />
        <input type="hidden" id="HiddenKouzouMei" runat="server" />
        <input type="hidden" id="HiddenTyousaGaiyou" runat="server" />
        <input type="hidden" id="HiddenHanteiCd1" runat="server" />
        <input type="hidden" id="HiddenHanteiCd2" runat="server" />
        <input type="hidden" id="HiddenKojGaisyaCd" runat="server" />
        <input type="hidden" id="HiddenTKojKaisyaCd" runat="server" />
        <!-- ¤Êîñ -->
        <table style="text-align: left; width: 100%;" id="TableKyoutuu" class="mainTable"
            cellpadding="1">
            <thead>
                <tr>
                    <th class="tableTitle" colspan="4">
                        <a id="LinkKyoutuuInfo" runat="server">¤Êîñ</a> <span id="TitleInfobar" style="display: none;"
                            runat="server"></span>
                        <input type="hidden" id="HiddenKyoutuuInfoStyle" runat="server" value="inline" />
                    </th>
                    <th class="tableTitle" style="text-align: right" colspan="5">
                        <input type="button" id="ButtonTokubetuTaiou" runat="server" value="ÁÊÎ" />
                        <input type="button" id="ButtonBukkenRireki" runat="server" value="¨ð" />
                        <input type="button" id="ButtonKousinRireki" runat="server" value="XVð" />
                        <input type="button" id="ButtonTyosaMitumori" runat="server" value="²¸©Ï" />
                        <input type="button" id="ButtonHosyousyoDB" runat="server" value="ÛØDB" class="openHosyousyoDB" />
                        <input type="button" id="ButtonRJHS" runat="server" value="R-JHS" class="openRJHS" />
                    </th>
                </tr>
            </thead>
            <tbody id="kyoutuuInfo" style="display: inline;" runat="server">
                <tr>
                    <td style="width: 80px" class="koumokuMei">
                        æª
                    </td>
                    <td>
                        <input type="text" id="TextKubun" style="ime-mode: disabled; border-style: none;"
                            readonly="readonly" class="readOnlyStyle" tabindex="-1" runat="server" />
                    </td>
                    <td class="koumokuMei">
                        Ô
                    </td>
                    <td>
                        <input type="text" id="TextBangou" style="width: 100px; ime-mode: disabled; border-style: none;"
                            readonly="readonly" class="readOnlyStyle" tabindex="-1" runat="server" />
                    </td>
                    <td class="koumokuMei">
                        f[^jüíÊ
                    </td>
                    <td id="TdDataHakiSyubetu" runat="server">
                        <asp:DropDownList ID="SelectDataHaki" runat="server" Width="110px">
                        </asp:DropDownList><span id="SpanDataHaki" style="display: none;" runat="server"></span><input
                            type="button" id="changeHaki" style="display: none" runat="server" onserverclick="changeHaki_ServerClick" />
                    </td>
                    <td style="" class="koumokuMei">
                        <span id="SpanDataHakiDate" runat="server">f[^jüú</span>
                    </td>
                    <td id="TdDataHakiDate" runat="server">
                        <input type="text" id="TextDataHakiDate" maxlength="10" class="date" style="width: 80px"
                            onblur="checkDate(this);" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="koumokuMei">
                        {å¼
                    </td>
                    <td colspan="7">
                        <input type="text" id="TextSesyuMei" maxlength="50" class="hissu" style="width: 320px;"
                            runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="koumokuMei">
                        ¨Z
                    </td>
                    <td colspan="7">
                        PF<input type="text" id="TextBukkenJyuusyo1" maxlength="32" class="hissu" style="width: 210px;"
                            runat="server" />
                        QF<input type="text" id="TextBukkenJyuusyo2" maxlength="32" class="" style="width: 210px;"
                            runat="server" />
                        <br />
                        RF<input type="text" id="TextBukkenJyuusyo3" style="width: 380px;" maxlength="54"
                            class="" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="koumokuMei">
                        õl</td>
                    <td colspan="7" rowspan="1">
                        <textarea id="TextAreaBikou" runat="server" cols="80" onfocus="this.select();" rows="3"
                            style="ime-mode: active; font-family: Sans-Serif"></textarea>
                        <textarea id="TextAreaBikou2" runat="server" cols="60" onfocus="this.select();" rows="4"
                            style="display: none; ime-mode: active; font-family: Sans-Serif"></textarea>
                    </td>
                </tr>
                <tr>
                    <td class="koumokuMei">
                        oR</td>
                    <td>
                        <input type="text" id="TextKeiyu" style="ime-mode: disabled; border-style: none;"
                            readonly="readonly" class="readOnlyStyle" tabindex="-1" runat="server" />
                    </td>
                    <td class="koumokuMei">
                        ¨¸
                    </td>
                    <td colspan="5">
                        <input type="text" id="TextTatemonoKensa" style="ime-mode: disabled; border-style: none;"
                            readonly="readonly" class="readOnlyStyle" tabindex="-1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="koumokuMei">
                        ²¸Aæ</td>
                    <td colspan="7">
                        AæF<input type="text" id="TextTyousaRenrakusaki" style="width: 150px;" maxlength="20"
                            readonly="readonly" class="readOnlyStyle2" tabindex="-1" runat="server" />
                        TelF<input type="text" id="TextTyousaTel" style="width: 125px;" class="tel readOnlyStyle2"
                            maxlength="20" readonly="readonly" tabindex="-1" runat="server" />
                        FaxF<input type="text" id="TextTyousaFax" style="width: 125px;" class="fax readOnlyStyle2"
                            maxlength="20" readonly="readonly" tabindex="-1" runat="server" /><br />
                        XSÒF<input type="text" id="TextTyousaMiseTantousya" style="width: 130px;" maxlength="20"
                            readonly="readonly" class="readOnlyStyle2" tabindex="-1" runat="server" />
                        mailF<input type="text" id="TextTyousaMail" style="width: 460px;" maxlength="64"
                            readonly="readonly" class="readOnlyStyle2" tabindex="-1" runat="server" /></td>
                </tr>
                <tr>
                    <td colspan="8" class="tableSpacer">
                    </td>
                </tr>
                <tr>
                    <td class="koumokuMei">
                        Á¿X
                    </td>
                    <td colspan="7">
                        <table id="TableKameiten" class="subTable paddinNarrow" style="font-weight: bold;">
                            <tr>
                                <td colspan="2" id="TdKameiten" runat="server">
                                    <input type="text" id="TextKameitenCd" maxlength="5" class="codeNumber hissu" style="width: 40px;"
                                        runat="server" /><input type="hidden" id="HiddenKameitenCdTextOld" runat="server" /><input
                                            type="hidden" id="HiddenKameitenCdTextMae" runat="server" /><input type="hidden"
                                                id="HiddenKameitenTyuuiJikou" runat="server" />
                                    <input id="ButtonKameitenSearch" runat="server" type="button" value="õ" class="GyoumuSearchBtn"
                                        onserverclick="ButtonKameitenSearch_ServerClick" /><input type="hidden" id="kameitenSearchType"
                                            runat="server" />
                                    <input type="text" id="TextKameitenMei" readonly="readonly" style="width: 14em;"
                                        class="readOnlyStyle" tabindex="-1" runat="server" />
                                    <asp:TextBox ID="TextTorikesiRiyuu" runat="server" Width="80px"></asp:TextBox>
                                    <input id="ButtonKameitenTyuuijouhou" runat="server" class="btnKameitenTyuuijouhou"
                                        type="button" value="Óîñ" />&nbsp; TelF
                                    <input type="text" id="TextKameitenTel" class="readOnlyStyle" readonly="readonly"
                                        style="width: 80px;" tabindex="-1" runat="server" />&nbsp; FaxF
                                    <input type="text" id="TextKameitenFax" class="readOnlyStyle" readonly="readonly"
                                        style="width: 80px;" tabindex="-1" runat="server" />
                                    <input type="hidden" id="HiddenKameitenMail" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 280px">
                                    r_[NOF<input type="text" id="TextBuilderNo" maxlength="5" readonly="readonly" style="width: 60px;"
                                        class=" readOnlyStyle" tabindex="-1" runat="server" />
                                    <input id="ButtonBuilderCheck" runat="server" style="width: 6.5em" type="button"
                                        value="r_[îñ" />
                                </td>
                                <td>
                                    nñF<input type="text" id="TextKeiretu" readonly="readonly" style="width: 25em;" class="readOnlyStyle"
                                        tabindex="-1" runat="server" /><input type="hidden" id="HiddenKeiretuCd" runat="server" />
                                    <br />
                                    cÆ/@l¼F<input type="text" id="TextEigyousyoMei" readonly="readonly" style="width: 25em;"
                                        class="readOnlyStyle" tabindex="-1" runat="server" /><input type="hidden" id="HiddenEigyousyoCd"
                                            runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <input type="text" id="TextMitsumoriHituyou" style="font-weight: bold; width: 177px;
                                        color: red; border-style: none;" class=" readOnlyStyle" readonly="readonly" tabindex="-1"
                                        runat="server" />
                                    &nbsp;
                                    <input type="text" id="TextHattyuusyoHituyou" style="font-weight: bold; width: 201px;
                                        color: red; border-style: none;" class=" readOnlyStyle" readonly="readonly" tabindex="-1"
                                        runat="server" />
                                    <asp:UpdatePanel ID="UpdatePanelNG" UpdateMode="always" runat="server" RenderMode="Inline">
                                        <ContentTemplate>
                                            <input type="text" id="TextTyousaKaisyaNG" style="font-weight: bold; width: 80px;
                                                color: red; border-style: none;" class=" readOnlyStyle" readonly="readonly" tabindex="-1"
                                                runat="server" />&nbsp;
                                            <input type="text" id="TextHanteiNG" style="font-weight: bold; width: 50px; color: red;
                                                border-style: none;" class=" readOnlyStyle" readonly="readonly" tabindex="-1"
                                                runat="server" />&nbsp;
                                            <input type="text" id="TextKoujiKaisyaNG" style="font-weight: bold; width: 80px;
                                                color: red; border-style: none;" class=" readOnlyStyle" readonly="readonly" tabindex="-1"
                                                runat="server" />&nbsp;
                                            <input id="TextJioSakiFlg" runat="server" style="font-weight: bold; width: 50px;
                                                color: red; border-style: none;" class="readOnlyStyle" readonly="readOnly" tabindex="-1" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    &nbsp; FCXF<input type="text" id="TextFcTenMei" style="width: 200px;" class="readOnlyStyle" readonly="readonly" tabindex="-1"
                                        runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tableSpacer" colspan="8">
                    </td>
                </tr>
                <tr>
                    <td class="koumokuMei">
                        &nbsp;
                    </td>
                    <td>
                        <input type="text" id="TextSyouhinKbn" class="readOnlyStyle" tabindex="-1" style="border-style: none;"
                            readonly="readonly" runat="server" />
                    </td>
                    <td class="koumokuMei">
                        ¯Ë
                    </td>
                    <td>
                        <input type="text" id="TextDoujiIraiTousuu" maxlength="4" readonly="readonly" class="number readOnlyStyle2"
                            tabindex="-1" style="width: 30px;" runat="server" />
                    </td>
                    <td class="koumokuMei">
                        ¨pr
                    </td>
                    <td>
                        <input type="text" id="TextTatemonoYouto" class="readOnlyStyle2" tabindex="-1" readonly="readonly"
                            runat="server" />&nbsp;
                    </td>
                    <td class="koumokuMei">
                        Ë
                    </td>
                    <td>
                        <input type="text" id="TextKosuu" maxlength="4" class="number readOnlyStyle2" tabindex="-1"
                            style="width: 40px;" readonly="readonly" runat="server" />Ë
                    </td>
                </tr>
            </tbody>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>

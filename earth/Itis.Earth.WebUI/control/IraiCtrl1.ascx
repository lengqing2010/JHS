<%@ Control Language="vb" AutoEventWireup="false" Codebehind="IraiCtrl1.ascx.vb"
    Inherits="Itis.Earth.WebUI.IraiCtrl1" %>
<%@ Register Assembly="Itis.Earth.WebUI" Namespace="Itis.Earth.WebUI" TagPrefix="cc1" %>
<%@ Import Namespace="Itis.Earth.BizLogic" %>
<%@ Import Namespace="Itis.Earth.Utilities" %>

<script language="javascript" type="text/javascript">
history.forward();

//重複チェック処理呼び出し
function choufukuCheck(objThis){
    objEBI("<%= choufukuKakuninTargetId.clientID %>").value = objThis.id;
    objEBI("<%= btnChoufukuCheck.clientID %>").click();
}

//破棄種別変更時の処理
function changeHaki() {
    var objBtnChangeHaki= objEBI("<%= changeHaki.clientID %>");
    objBtnChangeHaki.click();
}

//住所転記ボタン押下時の処理
function juushoTenki_onclick() {
    var objJuusho3 = objEBI("<%= bukkenJyuusho3.clientID %>");
    var objBikou = objEBI("<%= bikou.clientID %>");
    if(objJuusho3.value != ""){
        if(objBikou.value != ""){
            objBikou.value += " ";
        }
        objBikou.value += "住所続き：" + objJuusho3.value;
    }
}

//受注物件名に施主名をセットする処理
function setJyutyuuBukkenMei(objJyutyuuBukkenMei,objSesyuMei){
    if(objJyutyuuBukkenMei.className.indexOf("readOnlyStyle") == -1 && objJyutyuuBukkenMei.value == ""){
      objJyutyuuBukkenMei.value = objSesyuMei.value;
    }
}

//保証書NO採番状況チェック
function checkSaiban(){
    var objSaibanButton = objEBI("<%= btnHoshoushoNoSaiban.clientID %>");
    var objKubun = objEBI("<%= cboKubun.clientID %>");
    
    objSaibanButton.disabled = true;
    //区分選択チェック
    if(objKubun.value == ""){
        alert("<%= Messages.MSG004E %>");
        objKubun.focus();
        objSaibanButton.disabled = false;
        return false;
    }
    //保証書NO発行済みチェック
    if(objEBI("<%= hoshouNo.clientID %>").value != ""){
        if(!confirm("<%= Messages.MSG005C %>")){
            objKubun.focus();
            objSaibanButton.disabled = false;
            return false;
        }
    }
    
    //連棟物件数のチェック
    var strRentouSuu = "";//連棟物件数
    var strMsg = "<%= Messages.MSG062E %>";
    strMsg = strMsg.replace('@PARAM1','連棟物件数');
    strRentouSuu = prompt("\r\n" + strMsg,"");
    if(strRentouSuu == null){ //キャンセルボタン押下時
        objSaibanButton.disabled = false;
        objSaibanButton.focus();
        return false;
    }
    strRentouSuu = strRentouSuu.z2hDigit();
    if(strRentouSuu == "" || strRentouSuu == undefined){
        strMsg = "<%= Messages.MSG013E %>";
        strMsg = strMsg.replace('@PARAM1','連棟物件数');
        alert(strMsg);
        objSaibanButton.disabled = false;
        objSaibanButton.focus();
        return false;
    }
    if(strRentouSuu <= 0 || strRentouSuu > 999){
        strMsg = "<%= Messages.MSG111E %>";
        strMsg = strMsg.replace('@PARAM1','連棟物件数');
        strMsg = strMsg.replace('@PARAM2','1');
        strMsg = strMsg.replace('@PARAM3','999');
        alert(strMsg);
        objSaibanButton.disabled = false;
        objSaibanButton.focus();
        return false;
    }
    if(strRentouSuu >= 21){
        strMsg = "<%= Messages.MSG112C %>";
        strMsg = strMsg.replace('@PARAM1',strRentouSuu + '棟');
        if(confirm(strMsg) == false){
            objSaibanButton.disabled = false;
            return false;
        }
    }
    
    //連棟物件数をhiddenにセット
    objEBI("<%=HiddenRentouBukkenSuu.clientID %>").value = strRentouSuu;
    
    //採番処理を実行
    objEBI("<%= hoshoushoNoSaiban.clientID %>").click();
}

//プルダウンの値が特定値の場合、指定のエレメントのReadOnlyを外し、項目名ラベルの表示状態を切替える
function checkSonota(flgVal, targetId, labelId){
    if(flgVal){
        objEBI(targetId).style.visibility = "visible";
        objEBI(labelId).style.visibility = "visible";
    }else{
        objEBI(targetId).value = "";
        objEBI(targetId).style.visibility = "hidden";
        objEBI(labelId).style.visibility = "hidden";
    }
}

//入力チェック
function checkInputValue(){
    var objKubun = objEBI("<%=cboKubun.clientID %>");
    var objHosyousyoNo = objEBI("<%=hoshouNo.clientID %>");
    var objSesyuMei = objEBI("<%=seshuName.clientID %>");
}

</script>

<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
</asp:ScriptManagerProxy>
<input id="btn_irai1_act" runat="server" style="display: none" type="button" value="編集呼び出し" />
<asp:UpdatePanel ID="irai1MainUpdatePanel" UpdateMode="Conditional" runat="server"
    RenderMode="Inline">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="hoshoushoNoSaiban" />
        <asp:AsyncPostBackTrigger ControlID="changeHaki" />
        <asp:AsyncPostBackTrigger ControlID="CheckBunjouCdSaiban" />
    </Triggers>
    <ContentTemplate>
        <input type="hidden" id="iraiSessionKey" runat="server" />
        <input type="hidden" id="actMode" runat="server" />
        <input type="hidden" id="updateDateTime" runat="server" />
        <input type="hidden" id="lastUpdateUserId" runat="server" />
        <input type="hidden" id="lastUpdateUserNm" runat="server" />
        <input type="hidden" id="lastUpdateDateTime" runat="server" />
        <input type="hidden" id="errorData" runat="server" />
        <input type="hidden" id="kakuninOpenFlg" runat="server" />
        <input type="hidden" id="dateYesterday" runat="server" />
        <input type="hidden" id="HiddenSesyumeiOld" runat="server" />
        <input type="hidden" id="HiddenJuusyo1" runat="server" />
        <input type="hidden" id="HiddenSyoriKensuu" runat="server" value="0" />
        <input type="hidden" id="HiddenRentouBukkenSuu" runat="server" value="1" />
        <input type="hidden" id="HiddenInsTokubetuTaiouFlg" runat="server" value=""/>
        <input type="hidden" id="HiddenSinkiTourokuMotoKbn" runat="server" />
        <table style="text-align: left; width: 100%;" id="mainTableIrai1" class="mainTable"
            cellpadding="1">
            <thead>
                <tr>
                    <th class="tableTitle" colspan="2">
                        <a id="irai1DispLink" runat="server">共通情報</a>
                        <input id="btn_irai1" runat="server" class="btnEdit" style="height: 20px" type="button"
                            value="編集" />
                        <span id="irai1TitleInfobar" style="display: none;" runat="server"></span>
                    </th>
                    <th class="tableTitle" style="text-align: right" colspan="7">
                        <input type="button" id="ButtonSearchFcMousikomi" runat="server" value="FC申込情報" />
                        <input type="button" id="ButtonSearchMousikomi" runat="server" value="申込情報" />
                        <input type="button" id="ButtonBukkenRireki" runat="server" value="物件履歴" />
                        <input type="button" id="ButtonKousinRireki" runat="server" value="更新履歴" />
                        <input type="button" id="ButtonTyousaMitsumorisyo" runat="server" value="調査見積書" class="openTyousaMitsumorisyo" />
                        <input type="button" id="ButtonHosyousyoDB" runat="server" value="保証書DB" class="openHosyousyoDB" />
                        <input type="button" id="ButtonRJHS" runat="server" value="R-JHS" class="openRJHS" />
                    </th>
                </tr>
            </thead>
            <tbody id="irai1TBody" runat="server">
                <tr>
                    <td colspan="9" class="tableSpacer">
                    </td>
                </tr>
                <tr>
                    <td class="koumokuMei">
                        区分</td>
                    <td colspan="2">
                        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server" RenderMode="Inline">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="hoshoushoNoSaiban" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:DropDownList ID="cboKubun" runat="server" CssClass="hissu">
                                </asp:DropDownList><span id="spanKubun" runat="server"></span>
                                <input id="btnHoshoushoNoSaiban" style="width: 37px;" type="button" value="採番" onclick="checkSaiban();"
                                    runat="server" class="gyoumuSearchBtn" />
                                <input id="hoshoushoNoSaiban" style="display: none" type="button" value="採番hid" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="koumokuMei">
                        番号</td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel_irai2" UpdateMode="Conditional" runat="server" RenderMode="Inline">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="hoshoushoNoSaiban" />
                            </Triggers>
                            <ContentTemplate>
                                <input id="hoshouNo" style="width: 100px; ime-mode: disabled;" readonly="readonly"
                                    runat="server" class="readOnlyStyle hissu" tabindex="-1" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="koumokuMei">
                        データ破棄種別<span id="spanHakiDate" runat="server"><br />
                            データ破棄日</span></td>
                    <td colspan="3" style="padding-top: 1px; padding-bottom: 1px;">
                        <asp:DropDownList ID="cboDataHaki" runat="server" Width="110px">
                        </asp:DropDownList><span id="spanDataHaki" runat="server"></span><input type="button"
                            id="changeHaki" style="display: none" runat="server" onserverclick="changeHaki_ServerClick" /><br />
                        <input maxlength="10" class="date" id="hakiDate" style="width: 80px" runat="server"
                            onblur="checkDate(this);" /></td>
                </tr>
                <tr>
                    <td class="koumokuMei">
                        物件名</td>
                    <td colspan="3">
                        <input type="text" id="seshuName" style="width: 25em" maxlength="50" class="hissu"
                            onchange="choufukuCheck(this)" runat="server" />
                    </td>
                    <td class="koumokuMei">
                        施主名</td>
                    <td>
                        <input type="radio" name="RadioSesyumei" id="RadioSesyumei1" runat="server"
                             class="hissu"/><span id="SpanSesyumei1" runat="server">有</span>
                        <input type="radio" name="RadioSesyumei" id="RadioSesyumei0" runat="server"
                             class="hissu"/><span id="SpanSesyumei0" runat="server">無</span>
                    </td>
                    <td colspan="3">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="koumokuMei">
                        受注物件名</td>
                    <td colspan="8">
                        <input type="text" id="TextJyutyuuBukkenMei" style="width: 20em" maxlength="50" class="hissu"
                            runat="server" />
                    </td>     
                </tr>
                <tr>
                    <td class="koumokuMei">
                        物件住所</td>
                    <td colspan="8">
                        １：<input id="bukkenJyuusho1" style="width: 17em" runat="server" onchange="choufukuCheck(this)"
                            class="hissu" maxlength="32" />
                        ２：<input id="bukkenJyuusho2" style="width: 17em" runat="server" onchange="choufukuCheck(this)"
                            maxlength="32" />
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" RenderMode="Inline">
                            <ContentTemplate>
                                <input id="choufukuCheck" style="width: 7em;" type="button" value="重複物件あり" runat="server"
                                    onserverclick="choufukuCheck_ServerClick" size="" />
                                <input id="btnChoufukuCheck" style="display: none" type="button" value="重複チェック呼び出し"
                                    runat="server" onserverclick="btnChoufukuCheck_ServerClick" />
                                <input type="hidden" style="display: none" id="choufukuKakuninFlg1" runat="server" />
                                <input type="hidden" style="display: none" id="choufukuKakuninFlg2" runat="server" />
                                <input type="hidden" style="display: none" id="choufukuKakuninTargetId" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br />
                        ３：<input id="bukkenJyuusho3" style="width: 30em" runat="server" maxlength="54" />
                        <input id="juushoTenki" style="width: 9em" type="button" value="住所３を備考に転記" onclick="return juushoTenki_onclick()"
                            runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="koumokuMei" rowspan="2">
                        備考</td>
                    <td rowspan="2" colspan="4">
                        <textarea cols="80" rows="3" id="bikou" runat="server" style="font-family: Sans-Serif;
                            ime-mode: active;" onfocus="this.select();"></textarea><textarea cols="60" rows="4"
                                id="bikou2" runat="server" style="display: none; font-family: Sans-Serif; ime-mode: active;"
                                onfocus="this.select();"></textarea></td>
                    <td class="koumokuMei">
                        分譲コード</td>
                    <td colspan="3">
                        <input type="text" id="TextBunjouCd" style="width: 90px; ime-mode: disabled;" runat="server"
                            class="number" maxlength="10" /><input type="checkbox" id="CheckBunjouCdSaiban" runat="server" /><span
                                id="SpanBunjouCdSaiban" runat="server">採番</span>
                        <input type="button" id="ButtonBunjouSaiban" runat="server" style="display: none"
                            value="分譲採番" onserverclick="ButtonBunjouSaiban_ServerClick" />
                        <input type="hidden" id="HiddenDbUpdDate" runat="server" />
                        <input type="hidden" id="HiddenBunjouCd" runat="server" />
                        <input type="hidden" id="HiddenSaibanNo" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="koumokuMei">
                        物件名寄コード
                    </td>
                    <td colspan="3">
                        <input type="text" id="TextBukkenNayoseCd" style="width: 90px; ime-mode: disabled;"
                            runat="server" class="codeNumber" maxlength="11" />
                    </td>
                </tr>
                <tr>
                    <td class="koumokuMei">
                        経由</td>
                    <td colspan="2">
                        <select id="keiyu" runat="server">
                            <option value="0">0:無し</option>
                            <option value="1">1:有り</option>
                        </select>
                        <span id="SPAN1" runat="server"></span>
                    </td>
                    <td class="koumokuMei">
                        建物検査</td>
                    <td colspan="5">
                        <select id="kashi" runat="server">
                            <option value="0">0:無し</option>
                            <option value="1">1:有り</option>
                        </select>
                        <span id="SPAN2" runat="server"></span>
                    </td>
                </tr>
                <tr>
                    <td class="koumokuMei">
                        調査連絡先</td>
                    <td colspan="8">
                        連絡先：<input style="width: 150px" id="tyousakaisyaRenraku" runat="server" maxlength="60" />
                        Tel：<input style="width: 125px; ime-mode: disabled;" class="tel" id="tyousakaisyaTel"
                            runat="server" maxlength="20" />
                        Fax：<input style="width: 125px; ime-mode: disabled;" class="fax" id="tyousakaisyaFax"
                            runat="server" maxlength="20" /><br />
                        店担当者：<input style="width: 130px" id="tyousakaisyaTantou" runat="server" maxlength="20" />
                        mail：<input style="width: 460px; ime-mode: disabled;" id="tyousakaisyaMailAddr" runat="server"
                            maxlength="64" /></td>
                </tr>
                <tr>
                    <td colspan="9" class="tableSpacer">
                    </td>
                </tr>
                <tr>
                    <td class="koumokuMei">
                        依頼日</td>
                    <td>
                        <input class="date hissu" maxlength="10" id="iraiDate" runat="server" onblur="checkDate(this);" />
                        <input type="button" id="setIraiDate" style="width: 37px" runat="server" value="前日"
                            class="gyoumuSearchBtn" /></td>
                    <td class="koumokuMei">
                        契約NO</td>
                    <td colspan="6">
                        <input id="keiyakuNo" style="width: 150px; ime-mode: inactive;" runat="server" maxlength="20"
                            class="codeNumber" /></td>
                </tr>
                <tr>
                    <td class="koumokuMei">
                        調査希望日</td>
                    <td>
                        <input id="chousaKibouDate" runat="server" class="date hissu" maxlength="10" onblur="checkDate(this);" />
                        <input type="checkbox" id="CheckYoyakuZumi" runat="server" /><span id="SpanCheckYoyakuZumi"
                            runat="server">予定書手動</span></td>
                    <td class="koumokuMei">
                        調査希望時間</td>
                    <td colspan="6" rowspan="1">
                        <input id="chousaKibouTime" runat="server" size="40" maxlength="26" /></td>
                </tr>
                <tr>
                    <td class="koumokuMei">
                        立会有無</td>
                    <td>
                        <input id="tatiai_cd" runat="server" maxlength="1" class="pullCd" />&nbsp;<asp:DropDownList
                            ID="tatiai" runat="server">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="1">有り</asp:ListItem>
                        </asp:DropDownList><span id="SPAN17" runat="server"></span>
                    </td>
                    <td class="koumokuMei">
                        <label id="tatiaisya" runat="server">
                            立会者</label></td>
                    <td colspan="6" rowspan="1">
                        <div id="tatiaiDiv" runat="server">
                            <input id="tatiaiSha_1" runat="server" type="checkbox" value="1" /><span id="Span3"
                                runat="server">施主様</span>
                            <input id="tatiaiSha_2" runat="server" type="checkbox" value="2" /><span id="Span6"
                                runat="server">担当者</span>
                            <input id="tatiaiSha_3" runat="server" type="checkbox" value="4" /><span id="Span7"
                                runat="server">その他</span></div>
                    </td>
                </tr>
                <tr>
                    <td class="koumokuMei">
                        構造</td>
                    <td>
                        <input id="kouzou_cd" runat="server" maxlength="1" class="pullCd" />&nbsp;<asp:DropDownList
                            ID="cboKouzou" runat="server" Width="116px">
                        </asp:DropDownList><span id="SPAN12" runat="server"></span></td>
                    <td class="koumokuMei">
                        <label id="lblKouzouSonota" runat="server">
                            構造その他</label></td>
                    <td colspan="6">
                        <input style="width: 300px" id="kouzouSonota" runat="server" maxlength="80" /></td>
                </tr>
                <tr>
                    <td class="koumokuMei">
                        階層</td>
                    <td>
                        <input id="kaisou_cd" runat="server" maxlength="2" class="pullCd" />&nbsp;<asp:DropDownList
                            ID="cboKaisou" runat="server" Width="115px">
                        </asp:DropDownList><span id="SPAN9" runat="server"></span></td>
                    <td class="koumokuMei">
                        新築/建替</td>
                    <td colspan="6">
                        <input id="sintikuTatekae_cd" runat="server" maxlength="1" class="pullCd" />&nbsp;<asp:DropDownList
                            ID="cboShintikuTatekae" runat="server" Width="110px">
                        </asp:DropDownList><span id="SPAN10" runat="server"></span></td>
                </tr>
                <tr>
                    <td class="koumokuMei">
                        設計許容支持力</td>
                    <td>
                        <input id="sijiryoku" runat="server" class="number" style="width: 60px;" onblur="checkFewNumberAddFig(this);"
                            onfocus="removeFig(this);" maxlength="6" />kN/㎡</td>
                    <td class="koumokuMei">
                        依頼予定棟数</td>
                    <td colspan="6">
                        <input id="iraiYoteiTousu" runat="server" class="number" style="width: 60px;" onblur="checkNumberAddFig(this);"
                            onfocus="removeFig(this);" maxlength="4" />棟</td>
                </tr>
                <tr>
                    <td colspan="9" class="tableSpacer">
                    </td>
                </tr>
                <tr>
                    <td class="koumokuMei">
                        根切り深さ</td>
                    <td>
                        <input maxlength="13" id="negiri" runat="server" class="number" style="width: 90px;"
                            onblur="checkFewNumberAddFig(this);" onfocus="removeFig(this);" />mm</td>
                    <td class="koumokuMei">
                        予定盛土厚さ</td>
                    <td colspan="6">
                        <input maxlength="13" id="morituti" runat="server" class="number" style="width: 90px;"
                            onblur="checkFewNumberAddFig(this);" onfocus="removeFig(this);" />mm</td>
                </tr>
                <tr>
                    <td class="koumokuMei">
                        予定基礎</td>
                    <td>
                        <input id="yoteiKiso_cd" runat="server" maxlength="1" class="pullCd" />&nbsp;<asp:DropDownList
                            ID="cboYoteiKiso" runat="server" Width="142px">
                        </asp:DropDownList><span id="SPAN14" runat="server"></span></td>
                    <td class="koumokuMei">
                        <label id="lblYoteiKisoSonota" runat="server">
                            予定基礎その他</label></td>
                    <td colspan="6">
                        <input style="width: 300px" id="yoteiKisoSonota" runat="server" maxlength="80" /></td>
                </tr>
                <tr>
                    <td class="koumokuMei">
                        車庫</td>
                    <td>
                        <asp:DropDownList ID="cboSyako" runat="server">
                        </asp:DropDownList><span id="SPAN13" runat="server"></span></td>
                    <td class="koumokuMei">
                        承諾書</td>
                    <td colspan="2">
                        <input id="shoudakuUmu" runat="server" type="checkbox" value="1" /><span id="Span8"
                            runat="server">あり</span></td>
                    <td class="koumokuMei">
                        承諾書調査日</td>
                    <td colspan="3">
                        <input id="shoudakuChousaDate" runat="server" class="date" maxlength="10" onblur="checkDate(this);" /></td>
                </tr>
                <tr>
                    <td class="koumokuMei">
                        添付書類</td>
                    <td colspan="8">
                        <span style="font-weight: bold;">必須：</span>
                        <input type="checkbox" id="CheckAnnaiZu" runat="server" /><span id="SpanCheckAnnaiZu"
                            runat="server">案内図（区割図・測量図など）</span>
                        <input type="checkbox" id="CheckHaitiZu" runat="server" /><span id="SpanCheckHaitiZu"
                            runat="server">配置図</span>
                        <input type="checkbox" id="CheckKakukaiHeimenZu" runat="server" /><span id="SpanCheckKakukaiHeimenZu"
                            runat="server">各階平面図</span>&nbsp;&nbsp;&nbsp;&nbsp;<span style="font-weight: bold;">任意：</span>
                        <input type="checkbox" id="CheckKisoHuseZu" runat="server" /><span id="SpanCheckKisoHuseZu"
                            runat="server">基礎伏図</span>
                        <input type="checkbox" id="CheckKisoDanmenZu" runat="server" /><span id="SpanCheckKisoDanmenZu"
                            runat="server">基礎断面図</span>
                        <input type="checkbox" id="CheckZouseiKeikakuZu" runat="server" /><span id="SpanCheckZouseiKeikakuZu"
                            runat="server">立面図</span>
                    </td>
                </tr>
                <tr>
                    <td class="koumokuMei">
                        基礎着工予定日</td>
                    <td colspan="8">
                        <input id="TextKisoTyakkouYoteiDateFrom" runat="server" maxlength="10" class="date"
                            onblur="checkDate(this);" />～<input id="TextKisoTyakkouYoteiDateTo" runat="server"
                                maxlength="10" class="date" onblur="checkDate(this);" />
                    </td>
                </tr>
            </tbody>
        </table>
        <input type="hidden" id="iraiST" runat="server" />
        <input type="hidden" id="Irai1DataStr" runat="server" />
        <input type="hidden" id="Irai2DataStr" runat="server" />
        <input type="hidden" id="Irai2DdlDataStr" runat="server" />
        <input type="hidden" id="irai1Mode" runat="server" />
        <input type="hidden" id="irai2Mode" runat="server" />
        <input type="hidden" id="exeMode" runat="server" />
        <input type="hidden" id="jibanData" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>

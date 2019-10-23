<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="Hosyou.aspx.vb" Inherits="Itis.Earth.WebUI.Hosyou" Title="EARTH 保証" %>

<%@ Register Src="control/GyoumuKyoutuuCtrl.ascx" TagName="GyoumuKyoutuuCtrl" TagPrefix="uc1" %>
<%@ Register Src="control/SeikyuuSiireLinkCtrl.ascx" TagName="SeikyuuSiireLinkCtrl"
    TagPrefix="uc2" %>
<%@ Import Namespace="Itis.Earth.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script type="text/javascript">
        //画面起動時にウィンドウサイズをディスプレイに合わせる
        window.moveTo(0, 0);
        window.resizeTo(window.screen.availWidth, window.screen.availHeight);

        _d = document;
        
        var tmpConfirmResultButtonId = "";
                
        /****************************************
         * onload 時の追加処理
         * @param objTarget
         * @return
         ****************************************/
        function funcAfterOnload() {
            var objChkKaisiDate = objEBI("<%= HiddenChkKaisiDate.clientID %>");
            if (objChkKaisiDate.value == "1") {
               changeDisplay("<%= TbodyHakkouIraiInfo.clientID %>");
               SetDisplayStyle("<%= HiddenHakkouIraiInfoStyle.ClientID %>", "<%= TbodyHakkouIraiInfo.ClientID %>");
            }
        }

        //変更前コントロールの値を退避して、該当コントロール(Hidden)に保持する
        function SetChangeMaeValue(strTaihiID, strTargetID){
           document.getElementById(strTaihiID).value = document.getElementById(strTargetID).value;
        }
        
        //登録ボタン押下時の登録許可確認を行なう。
        function CheckTouroku(){
            //Chk03 保証書発行日＜地盤.調査実施日の場合
            var objHosyousyoHakkouDate = objEBI("<%= TextHosyousyoHakkouDate.clientID %>");
            var objTyousaJissiDate = objEBI("<%= HiddenTyousaJissiDateOld.clientID %>");
            if(objHosyousyoHakkouDate.value != "" && objTyousaJissiDate.value != ""){
                if(Number(removeSlash(objHosyousyoHakkouDate.value)) < Number(removeSlash(objTyousaJissiDate.value))){
                    if(objEBI("<%= HiddenHosyousyoHakkouDateMsg03.clientID %>").value != "1" && objEBI("<%= HiddenChk03.clientID %>").value != "1"){
                        if(confirm("<%= Messages.MSG099C %>")){
                            objEBI("<%= HiddenChk03.clientID %>").value = "1";
                        }else{
                            return false;
                        }
                    }
                }
            }

            //Chk04 保証書発行日＝入力、地盤.調査実施日＝未入力の場合
            var objHosyousyoHakkouDate = objEBI("<%= TextHosyousyoHakkouDate.clientID %>");
            var objTyousaJissiDate = objEBI("<%= HiddenTyousaJissiDateOld.clientID %>");
            if(objHosyousyoHakkouDate.value != "" && objTyousaJissiDate.value == ""){
                if(document.getElementById("<%= HiddenHosyousyoHakkouDateMsg04.clientID %>").value != "1" && objEBI("<%= HiddenChk04.clientID %>").value != "1"){
                    if(confirm("<%= Messages.MSG098C %>")){
                        objEBI("<%= HiddenChk04.clientID %>").value = "1";
                    }else{
                        return false;
                    }
                }
            }

            //Chk21 保証期間に変更がある場合、警告メッセージ表示
            var varMsg205 = "<%= Messages.MSG205S %>";
            var objHosyouKikanOld = objEBI("<%= HiddenHosyouKikanOld.clientID %>");
            var objHosyouKikanNew = objEBI("<%= TextHosyouKikan.clientID %>");
            if(objHosyouKikanOld.value != objHosyouKikanNew.value){
                varMsg205 = varMsg205.replace('@PARAM1',objHosyouKikanOld.value);
                varMsg205 = varMsg205.replace('@PARAM2',objHosyouKikanNew.value);
                alert(varMsg205);
            }
            
            return true;　//チェック完了
        }
                
        //付保証明書FLG変更時処理(Cancel時PostBack)
        function callFuhoSyoumeisyoFlgCancel(strMsg ,btnId){
            if(confirm(strMsg)){
            }else{
                tmpConfirmResultButtonId = btnId;
            }
        }
        
        //IME処理制御用フォーカスバウンダー（再発行理由用
        function setFocusSaihakkouRiyuu(){
            objEBI("<%=TextSaihakkouRiyuu.ClientId %>").focus();
        }

        //Ajax実行時処理
        Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(InitializeRequestHandler);
            function InitializeRequestHandler(sender, args){
                objEBI("<%=HiddenAjaxFlg.ClientID %>").value=1;
        }
        //Ajaxロード後処理
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args){
            if(tmpConfirmResultButtonId != ""){
                objEBI(tmpConfirmResultButtonId).click();
                tmpConfirmResultButtonId = "";
            }
            objEBI("<%=HiddenAjaxFlg.ClientID %>").value=0;
        }
        
        //プルダウンの値が特定値の場合、指定のエレメントのReadOnlyを外し、項目名ラベルの表示状態を切替える
        function checkPullDown(){
            var objTarget = objEBI("<%= TextHosyouNasiRiyuu.clientID %>");
            var objPulldown = objEBI("<%= SelectHosyouNasiRiyuu.clientID %>");
            var objHdnPulldown = objEBI("<%= SelectHannyouNo.clientID %>");
            var index = 0;
            var text = "0";
            
            //選択項目の汎用NOを取得
            index = objPulldown.selectedIndex;
            if(index == -1){
                //非活性化、値クリア
                objTarget.value = "";
                objTarget.disabled = true;
                objTarget.style.backgroundColor = "<%= CSS_COLOR_GRAY %>";            
            }else{
                objHdnPulldown.selectedIndex = index;
                text = objHdnPulldown.options[index].text;
                
                if(objPulldown.style.display != "none"){
                    if(text == "1"){
                        //活性化
                        objTarget.disabled = false;
                        objTarget.style.backgroundColor = "<%= EarthConst.STYLE_COLOR_WHITE %>";
                    }else{
                        //非活性化、値クリア
                        objTarget.value = "";
                        objTarget.disabled = true;
                        objTarget.style.backgroundColor = "<%= CSS_COLOR_GRAY %>";
                    }    
                }
            }
        }
        
        //加盟店検索処理を呼び出す
        function callYoteiKameitenSearch(obj){
            objEBI("<%= yoteiKameitenSearchType.clientID %>").value = "";
            if(obj.value == ""){
                objEBI("<%= yoteiKameitenSearchType.clientID %>").value = "1";
                objEBI("<%= ButtonYoteiKameitenSearch.clientID %>").click();
            }
        }        
        
        // 発行依頼受付セットのチェック
        function CheckIraiUketuke(){
            // Chk27 各項目（施主名と物件名称、住所１+2+3と物件所在地1+2+3、保証情報.お引渡し日とお引渡し日）を比較し、
            //       異なるものがひとつでもある場合はメッセージ表示

            var chkWarnFlg = 0;

            var objTextSesyuMei = objEBI("<%= ucGyoumuKyoutuu.AccSesyuMei.clientID %>");
            var objTextbukkenuMei = objEBI("<%= TextbukkenuMei.clientID %>");
            var objTextBukkenJyuusyo1 = objEBI("<%= ucGyoumuKyoutuu.AccBukkenJyuusyo1.clientID %>");
            var objTextBukkenJyuusyo2 = objEBI("<%= ucGyoumuKyoutuu.AccBukkenJyuusyo2.clientID %>");
            var objTextBukkenJyuusyo3 = objEBI("<%= ucGyoumuKyoutuu.AccBukkenJyuusyo3.clientID %>");
            var objTextBukkenuSyozai1 = objEBI("<%= TextBukkenuSyozai1.clientID %>");
            var objTextBukkenuSyozai2 = objEBI("<%= TextBukkenuSyozai2.clientID %>");
            var objTextBukkenuSyozai3 = objEBI("<%= TextBukkenuSyozai3.clientID %>");
            var objTextHosyouKaisiDate = objEBI("<%= TextHosyouKaisiDate.clientID %>");
            var objTextHikiwatasiDate = objEBI("<%= TextHikiwatasiDate.clientID %>");

            // 施主名と物件名称
            if (objTextSesyuMei.value != objTextbukkenuMei.value) {
               chkWarnFlg = 1;
            }

            // 住所１+2+3と物件所在地1+2+3
            if (objTextBukkenJyuusyo1.value + objTextBukkenJyuusyo2.value + objTextBukkenJyuusyo3.value  != objTextBukkenuSyozai1.value + objTextBukkenuSyozai2.value + objTextBukkenuSyozai3.value) {
               chkWarnFlg = 1;
            }

            // 保証情報.お引渡し日（保証開始日）と発行依頼情報.お引渡し日
            var nKaisi = 0;
            if (objTextHosyouKaisiDate.value != "") {
               nKaisi = Number(removeSlash(objTextHosyouKaisiDate.value));
            } 
            var nHikiwatasi = 0;
            if (objTextHikiwatasiDate.value != "") {
               nHikiwatasi = Number(removeSlash(objTextHikiwatasiDate.value));
            }
            if (nKaisi < nHikiwatasi) {
               chkWarnFlg = 1;
            }
            
            if (chkWarnFlg == 1) {
	            if(confirm("<%= Messages.MSG212C %>")){
	                objEBI("<%= HiddenChk27.clientID %>").value = "1";
	            }else{
	                return false;
	            }
            }

            // Chk28 （保証書再発行日、保証書発行日）＞＝システム日付
            chkWarnFlg = 0;
            var nHakko = 0;
            var objTextSaihakkouDate = objEBI("<%= TextSaihakkouDate.clientID %>");
            var objTextHosyousyoHakkouDate = objEBI("<%= TextHosyousyoHakkouDate.clientID %>");
            
            if (objTextSaihakkouDate.value != "") {
                nHakko = Number(removeSlash(objTextSaihakkouDate.value));
            } else if (objTextHosyousyoHakkouDate.value != "") {
                nHakko = Number(removeSlash(objTextHosyousyoHakkouDate.value));
            }
            
            if (nHakko >= Number(removeSlash(getToday()))) {
               chkWarnFlg = 1;
            }

            if (chkWarnFlg == 1) {
	            if(confirm("<%= Messages.MSG213C %>")){
	                objEBI("<%= HiddenChk28.clientID %>").value = "1";
	            }else{
	                return false;
	            }
            }
            
            // 画面．保証書発行日が空白の場合...
            var chkConfirmFlg = 0;
            // 再発行商品コード
            var objTextShSyouhinCd = objEBI("<%= TextShSyouhinCd.clientID %>");

            // 保証書発行日が空白
            if (objTextHosyousyoHakkouDate.value == "") {
                // 画面．保証書発行日に画面．セット発行日をセット
                objEBI("<%= HiddenHakkouSetTo.clientID %>").value = "1";

            // 保証書再発行日が空白
            } else if (objTextSaihakkouDate.value == "") {
                // 画面．保証書再発行日に画面．セット発行日をセット
                objEBI("<%= HiddenHakkouSetTo.clientID %>").value = "2";

            // 画面．再発行商品コードが空白
            } else if (objTextShSyouhinCd.value == "") {
                objEBI("<%= HiddenHakkouSetTo.clientID %>").value = "3";

            } else {
	            if(confirm("<%= Messages.MSG214C %>")){
                    objEBI("<%= HiddenHakkouSetTo.clientID %>").value = "4";
	            }else{
	                return false;
	            }
            }

            // 処理継続 
            return true;

        }

        // 保証情報.お引渡し日（保証開始日）へ転記
        function CheckHosyouKaisiDate(){ 
            // お引渡し日がシステム日付の3年前～3年後の範囲外の場合メッセージ表示
            var objHikiwatasiDate = objEBI("<%= TextHikiwatasiDate.clientID %>");

            var objToday = new Date();
            var objPastDay = new Date(objToday.getFullYear() - 3, objToday.getMonth(), objToday.getDate());
            var objFutureDay = new Date(objToday.getFullYear() + 3, objToday.getMonth(), objToday.getDate());
            
            var objHikiwatasiDay = new Date(Date.parse(objHikiwatasiDate.value));
            
            if((objHikiwatasiDate.value != "") &&
              (( objHikiwatasiDay <= objPastDay) || ( objHikiwatasiDay >= objFutureDay)))
            {
	            if(confirm("<%= Messages.MSG217C %>")){
	                objEBI("<%= HiddenChkKaisiDate.clientID %>").value = "1";
	            }else{
	                return false;
	            }
            }
            // 処理継続 
            return true;
        }
    </script>

    <!-- 画面上部・ヘッダ -->
    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tbody>
            <tr>
                <th style="text-align: left; width: 150px;">
                    保証
                </th>
                <th style="text-align: left;">
                    <input type="button" id="ButtonTouroku1" value="登録 / 修正 実行" style="font-weight: bold;
                        font-size: 18px; width: 155px; color: black; height: 30px; background-color: fuchsia"
                        runat="server" />&nbsp;&nbsp;&nbsp;
                </th>
                <th style="text-align: right; font-size: 11px;">
                    最終更新者：<asp:TextBox ID="TextSaisyuuKousinsya" CssClass="readOnlyStyle" Style="width: 120px"
                        ReadOnly="true" Text="" runat="server" TabIndex="-1" /><br />
                    最終更新日時：<asp:TextBox ID="TextSaisyuuKousinDate" CssClass="readOnlyStyle" Style="width: 100px"
                        ReadOnly="true" Text="" runat="server" TabIndex="-1" />
                    <asp:HiddenField ID="HiddenJibanUpdateDateTime" runat="server" />
                </th>
            </tr>
            <tr>
                <td colspan="2" rowspan="1" style="height: 10px">
                </td>
                <td colspan="1" rowspan="1" style="height: 10px">
                </td>
            </tr>
        </tbody>
    </table>
    <asp:UpdatePanel ID="UpdatePanelHosyou" UpdateMode="conditional" runat="server" RenderMode="Inline">
        <ContentTemplate>
            <uc1:GyoumuKyoutuuCtrl ID="ucGyoumuKyoutuu" runat="server" DispMode="HOSYOU" />
            <br />
            <asp:UpdatePanel ID="UpdatePanelHall" UpdateMode="conditional" runat="server" RenderMode="inline"
                ChildrenAsTriggers="true">
                <Triggers>
                    <%--業務共通タブ--%>
                    <asp:AsyncPostBackTrigger ControlID="ucGyoumuKyoutuu" />
                </Triggers>
                <ContentTemplate>
                    <input type="hidden" id="HiddenAjaxFlg" runat="server" /><%--Ajax処理中フラグ--%>
                    <input type="hidden" id="HiddenHantei1CdOld" runat="server" value="" /><%--判定1コードOld--%>
                    <input type="hidden" id="HiddenTyousaJissiDateOld" runat="server" value="" /><%--調査実施日Old--%>
                    <input type="hidden" id="HiddenHosyousyoHakkouDateMae" runat="server" value="" /><%--保証書発行日変更前--%>
                    <input type="hidden" id="HiddenSaihakkouDateMae" runat="server" value="" /><%--再発行日変更前--%>
                    <input type="hidden" id="HiddenSaihakkouDateOld" runat="server" /><%--保証書再発行日Old--%>
                    <input type="hidden" id="HiddenShSeikyuuUmuMae" runat="server" value="" /><%--請求有無変更前--%>
                    <input type="hidden" id="HiddenShZeiritu" runat="server" value="" /><%--売上・税率--%>
                    <input type="hidden" id="HiddenKyZeiritu" runat="server" value="" /><%--解約払戻・税率--%>
                    <input type="hidden" id="HiddenShJituseikyuu1Flg" value="" runat="server" /><%--実請求税抜金額1フラグ--%>
                    <input type="hidden" id="HiddenSyouhin1SeikyuuHakkouDate" value="" runat="server" /><%--<商品コード1>請求書発行日--%>
                    <input type="hidden" id="HiddenHosyousyoHakkouDateMsg03" runat="server" value="" /><%--保証書発行日変更Chk03--%>
                    <input type="hidden" id="HiddenHosyousyoHakkouDateMsg04" runat="server" value="" /><%--保証書発行日変更Chk03--%>
                    <input type="hidden" id="HiddenChk03" runat="server" value="" /><%--Chk03--%>
                    <input type="hidden" id="HiddenChk04" runat="server" value="" /><%--Chk04--%>
                    <input type="hidden" id="HiddenChk27" runat="server" value="" /><%--Chk27--%>
                    <input type="hidden" id="HiddenChk28" runat="server" value="" /><%--Chk28--%>
                    <input type="hidden" id="HiddenChkKaisiDate" runat="server" value="" /><%--HiddenChkKaisiDate--%>
                    <input type="hidden" id="HiddenHakkouSetTo" runat="server" value="" />
                    <input type="hidden" id="HiddenKyKaiyakuHaraimodosiSinseiUmuMae" runat="server" value="" /><%--解約払戻申請有無変更前--%>
                    <input type="hidden" id="HiddenTyousaHattyuusyoKakuninDateFlg" runat="server" value="" /><%--調査発注書確認日フラグ--%>
                    <input type="hidden" id="HiddenKoujiHattyuusyoKakuninDateFlg" runat="server" value="" /><%--工事発注書確認日フラグ--%>
                    <input type="hidden" id="HiddenFuhoSyoumeisyoFlgMae" runat="server" value="" /><%--付保証明書FLG変更前--%>
                    <input type="hidden" id="HiddenDefaultSiireSakiCdForLink" runat="server" value="" /><%--調査会社コード（連結）--%>
                    <input type="hidden" id="HiddenKameitenCd" runat="server" /><%--加盟店コード--%>
                    <input type="hidden" id="HiddenHosyouSyouhinUmu" runat="server" /><%--保証商品有無--%>
                    <input type="hidden" id="HiddenHosyousyoHakJyKyMae" runat="server" /><%--保証書発行状況変更前--%>
                    <input type="hidden" id="HiddenSetFuhoSyoumeisyoFlg" runat="server" value="0" /><%--付保証明書FLG自動設定フラグ--%>
                    <input type="hidden" id="HiddenHkUpdDatetime" runat="server" /><%--保証書管理テーブル更新日時--%>
                    <input type="hidden" id="HiddenHosyouKikanOld" runat="server" /><%--保証期間Old--%>
                    <input type="hidden" id="HiddenHakIraiUkeDatetimeOld" runat="server" /><%--発行依頼受付日時Old(地盤)--%>
                    <input type="hidden" id="HiddenHakIraiCanDatetimeOld" runat="server" /><%--発行依頼キャンセル日時Old(地盤)--%>
                    <input type="hidden" id="HiddenShSyouhinCdOld" runat="server" /><%--再発行商品コードOld--%>
                    <input type="hidden" id="HiddenHakIraiUketukeFlg" runat="server" /><%--保証書発行受付フラグ--%>
                    <input type="hidden" id="HiddenHakIraiCancelFlg" runat="server" /><%--保証書発行キャンセルフラグ--%>
                    <input type="hidden" id="HiddenHakIraiTime" runat="server" /><%--発行依頼日--%>
                    <input type="hidden" id="HiddenHakIraiUkeDatetimeR" runat="server" /><%--発行依頼受付日時(進捗)--%>
                    <input type="hidden" id="HiddenHakIraiCanDatetimeR" runat="server" /><%--発行依頼キャンセル日時(進捗)--%>

                    <div id="divHakkouIrai" runat="server">
                        <!-- 画面中央部・発行依頼情報 -->
                        <table style="text-align: left; width: 100%;" id="Table6" class="mainTable" cellpadding="0"
                            cellspacing="0" border="1">
                            <!-- ヘッダ部 -->
                            <thead>
                                <tr>
                                    <th class="tableTitle" colspan="8">
                                        <a id="AncHakkouIraiInfo" runat="server">発行ご依頼情報</a>
                                        <input type="hidden" id="HiddenHakkouIraiInfoStyle" runat="server" value="inline" />
                                    </th>
                                    <th id="Th2" class="tableTitle" style="text-align: right" colspan="2" runat="server">
                                        <asp:TextBox ID="TextHakIraiTime" CssClass="readOnlyStyle2"
                                             readonly="True" tabindex="-1" runat="server" />
                                    </th>
                            <thead>
                            <tbody id="TbodyHakkouIraiInfo" class="scrolltablestyle" runat="server">
                                <tr>
                                    <td colspan="10" class="tableSpacer">
                                    </td>
                                </tr>
                                <!-- 1行目 -->
                                <tr>
                                    <td class="koumokuMei">
                                        物件名称
                                    </td>
                                    <td colspan="7" style="border-right-style:none">
                                        <asp:TextBox ID="TextbukkenuMei" CssClass="readOnlyStyle2"
                                             readonly="True" tabindex="-1" runat="server" style="width:90%"/>
                                    </td>
                                    <td style="border-left-style:none; border-right-style:none">
                                       <input id="ButtonBukkenTenki" runat="server" type="button" value="施主名へ転記" class="BukkenTenkiBtn" style="width:110px"
                                        onserverclick="ButtonBukkenTenki_ServerClick" /><input type="hidden" id="BukkenTenkiType"
                                            runat="server" />
                                    </td>
                                    <td style="border-left-style:none">
                                       <input id="ButtonHakkouCancel" runat="server" type="button" value="キャンセル"  style="font-weight: bold;background-color: #faebd7;width:80px" class="HakkouCancelBtn"
                                        onserverclick="ButtonHakkouCancel_ServerClick" /><input type="hidden" id="HakkouCancelType"
                                            runat="server" />
                                    </td>
                                </tr>
                                <!-- 2行目 -->
                                <tr>
                                    <td class="koumokuMei" rowspan="2">
                                        物件所在地
                                    </td>
                                    <td id="TdBukkenSyozai1" runat="server" colspan="4" style="border-right-style:none; border-bottom-style:none">１：
                                        <asp:TextBox ID="TextBukkenuSyozai1" CssClass="readOnlyStyle2" style="width:100%;font-size:11px"
                                             readonly="True" tabindex="-1" runat="server"/>
                                    </td>
                                    <td id="TdBukkenSyozai2" runat="server" colspan="2" style="border-left-style:none; border-bottom-style:none; padding-left:20px">２：
                                        <asp:TextBox ID="TextBukkenuSyozai2" CssClass="readOnlyStyle2" style="width:80%;font-size:11px"
                                             readonly="True" tabindex="-1" runat="server"/>
                                    </td>
                                    <td class="koumokuMei">
                                        セット発行日
                                    </td>
                                    <td id="TdSetHakkouDate" style="border-right-style:none" runat="server">
                                        <asp:TextBox ID="TextSetHakkouDate" CssClass="date" MaxLength="10" Text=""
                                            runat="server" />
                                    </td>
                                    <td style="border-left-style:none">
                                       <input id="ButtonHakkouSet" runat="server" type="button" value="受付セット" style="font-weight: bold;background-color: #ffd700;width:80px" />
                                       <input type="hidden" id="HakkouSetType"
                                            runat="server" />
                                    </td>
                                </tr>
                                <!-- 3行目 -->
                                <tr>
                                    <td id="TdBukkenSyozai3" runat="server" colspan="5" style="border-right-style:none; border-top-style:none">３：
                                        <asp:TextBox ID="TextBukkenuSyozai3" CssClass="readOnlyStyle2"
                                             readonly="True" tabindex="-1" runat="server" style="width:100%;font-size:11px"/>&nbsp;
                                    </td>
                                    <td id="TdButtonJyuusyoTenki" runat="server" style="border-left-style:none; border-top-style:none">
                                       <input id="ButtonJyuusyoTenki" runat="server" type="button" value="物件住所へ転記" class="JyuusyoTenkiBtn" style="width:100px"
                                        onserverclick="ButtonJyuusyoTenki_ServerClick"/><input type="hidden" id="JyuusyoTenkiType"
                                            runat="server" />
                                    </td>
                                    <td class="koumokuMei">
                                        お引渡し日
                                    </td>
                                    <td id="TdHikiwatasiDate" runat="server" colspan="2">
                                        <asp:TextBox ID="TextHikiwatasiDate" CssClass="readOnlyStyle2"
                                             readonly="True" tabindex="-1" runat="server" />
                                       <input id="ButtonHosyouKaisiDateTenki" runat="server" type="button" value="お引渡し日へ転記" class="HosyouKaisiDateTenkiBtn"  style="width:120px"
                                        onserverclick="ButtonHosyouKaisiDateTenki_ServerClick" /><input type="hidden" id="HosyouKaisiDateTenkiType"
                                            runat="server" />
                                    </td>
                                </tr>
                                <!-- 4行目 -->
                                <tr>
                                    <td class="koumokuMei">
                                        担当者
                                    </td>
                                    <td id="TdTantouSya" runat="server" colspan="3">
                                        <asp:TextBox ID="TextTantouSya" CssClass="readOnlyStyle2"
                                             readonly="True" tabindex="-1" runat="server" style="font-size:11px"/>
                                    </td>
                                    <td class="koumokuMei">
                                        連絡先
                                    </td>
                                    <td id="TdRenrakuSaki" runat="server" colspan="1">
                                        <asp:TextBox ID="TextRenrakuSaki" CssClass="readOnlyStyle2"
                                             readonly="True" tabindex="-1" runat="server" style="font-size:11px"/>
                                    </td>
                                    <td></td>
                                    <td class="koumokuMei">
                                        入力ID
                                    </td>
                                    <td id="TdNyuuryokuID" runat="server" colspan="2">
                                        <asp:TextBox ID="TextNyuuryokuID" CssClass="readOnlyStyle2"
                                             readonly="True" tabindex="-1" runat="server"  style="width:90%;font-size:11px"/>
                                    </td>
                                </tr>
                                <!-- 5行目 -->
                                <tr>
                                    <td class="koumokuMei">
                                        その他情報
                                    </td>
                                    <td id="TdSonota" runat="server" colspan="9">
                                        <asp:textbox ID="TextIraiSonota" CssClass="readOnlyStyle2" textmode="MultiLine" rows="3"
                                             readonly="True" tabindex="-1" runat="server" style="width:90%"/>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <br />
                    <div id="divHosyou" runat="server">
                        <!-- 画面中央部・保証情報 -->
                        <table style="text-align: left; width: 100%;" id="Table2" class="mainTable" cellpadding="0"
                            cellspacing="0" border="1">
                            <!-- ヘッダ部 -->
                            <thead>
                                <tr>
                                    <th class="tableTitle" style="padding: 0px; height: 26px" colspan="7">
                                        <a id="AncHosyouInfo" runat="server">保証情報</a>
                                        <input type="hidden" id="HiddenHosyouInfoStyle" runat="server" value="inline" />
                                    </th>
                                    <th id="Th1" class="tableTitle" style="padding: 0px; text-align: right;  height: 26px" runat="server">
                                        <input type="button" id="ButtonBukkenJyokyou" value="物件進捗状況" style="width: 100px"
                                            class="button_copy" runat="server" />&nbsp;
                                    </th>
                                </tr>
                            </thead>
                            <!-- ボディ部 -->
                            <tbody id="TbodyHoshoInfo" class="scrolltablestyle" runat="server">
                                <tr>
                                    <td colspan="8" class="tableSpacer">
                                    </td>
                                </tr>
                                <!-- 1行目 -->
                                <tr>
                                    <td style="width: 80px" class="koumokuMei">
                                        契約NO
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextKeiyakuNo" Style="width: 80px; ime-mode: inactive;" CssClass=""
                                            runat="server" MaxLength="20" />
                                    </td>
                                    <td class="koumokuMei">
                                        調査実施日
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextTyousaJissiDate" CssClass="date readOnlyStyle" Text="" MaxLength="10"
                                            Style="width: 70px" ReadOnly="true" TabIndex="-1" runat="server" />
                                    </td>
                                    <td class="koumokuMei">
                                        計画書作成日
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextKeikakusyoSakuseiDate" CssClass="date readOnlyStyle" Text=""
                                            MaxLength="10" Style="width: 70px" ReadOnly="true" TabIndex="-1" runat="server" />
                                    </td>
                                    <td class="koumokuMei">
                                        入金確認条件
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextNyuukinKakuninJyouken" Style="width: 150px" CssClass="readOnlyStyle"
                                            Text="" ReadOnly="true" TabIndex="-1" runat="server" />
                                    </td>
                                </tr>
                                <!-- 2行目 -->
                                <tr>
                                    <td class="koumokuMei">
                                        基礎報告書
                                    </td>
                                    <td id="TdKisoHoukokusyo" runat="server">
                                        <asp:DropDownList ID="SelectKisoHoukokusyo" CssClass="" Style="display: inline;"
                                            runat="server" AutoPostBack="true" OnSelectedIndexChanged="SelectKisoHoukokusyo_SelectedIndexChanged">
                                            <asp:ListItem Value="" Text="" Selected="true"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="有"></asp:ListItem>
                                        </asp:DropDownList><span id="SpanKisoHoukokusyo" runat="server"></span>
                                    </td>
                                    <td class="koumokuMei">
                                        基礎報告書着日
                                    </td>
                                    <td id="TdKisoHoukokusyoTyakuDate" runat="server">
                                        <asp:TextBox ID="TextKisoHoukokusyoTyakuDate" CssClass="date" MaxLength="10" Text=""
                                            runat="server" />
                                    </td>
                                    <td class="koumokuMei">
                                        変更予定加盟店
                                    </td>
                                    <td colspan="3">
                                        <input type="text" id="TextYoteiKameitenCd" maxlength="5" class="codeNumber" style="width: 40px;" 
                                            runat="server" /><input type="hidden" id="HiddenYoteiKameitenCdTextOld" runat="server" /><input
											type="hidden" id="HiddenYoteiKameitenCdTextMae" runat="server" />
                                        <input id="ButtonYoteiKameitenSearch" runat="server" type="button" value="検索" class="GyoumuSearchBtn"
                                             /><input type="hidden" id="yoteiKameitenSearchType" runat="server" />
                                        <input type="text" id="TextYoteiKameitenMei" readonly="readonly" style="width:200px;"
                                            class="readOnlyStyle" tabindex="-1" runat="server" />
                                        <input id="ButtonYoteiKameitenTyuuijouhou" runat="server" class="btnKameitenTyuuijouhou"
                                            type="button" value="注意情報" />
                                    </td>
                                </tr>
                                <!-- 3行目 -->
                                <tr>
                                    <td class="koumokuMei">
                                        発行依頼書
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="SelectHakkouIraisyo" CssClass="" Style="display: inline;" runat="server"
                                            AutoPostBack="true" OnSelectedIndexChanged="SelectHakkouIraisyo_SelectedIndexChanged">
                                            <asp:ListItem Value="" Text="" Selected="true"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="有"></asp:ListItem>
                                        </asp:DropDownList><span id="SpanHakkouIraisyo" runat="server"></span>
                                    </td>
                                    <td class="koumokuMei">
                                        発行依頼書着日
                                    </td>
                                    <td id="TdHakkouIraiTyakuDate" runat="server">
                                        <asp:TextBox ID="TextHakkouIraiTyakuDate" CssClass="date" MaxLength="10" Text=""
                                            runat="server" />
                                    </td>
                                    <td class="koumokuMei">
                                        業務完了日
                                    </td>
                                    <td id="TdGyoumuKanryouDate" runat="server">
                                        <asp:TextBox ID="TextGyoumuKanryouDate" CssClass="date readOnlyStyle" MaxLength="10" Text=""
                                            Style="width: 70px" ReadOnly="true" TabIndex="-1" runat="server" />
                                    </td>
                                    <!-- 業務完了内容 -->
                                    <td colspan="3" id="TdGyoumuKaisiNaiyou" runat="server">
                                        <asp:TextBox ID="TextGyoumuKaisiNaiyou" CssClass="readOnlyStyle"
                                            ReadOnly="true" TabIndex="-1" runat="server" />
                                    </td>
                                </tr>
                                <!-- 4行目 -->
                                <tr>
                                    <td class="koumokuMei">
                                        保証書発行状況
                                    </td>
                                    <td colspan="3" id="TdHosyousyoHakkouJyoukyou" runat="server">
                                        <asp:DropDownList ID="SelectHosyousyoHakkouJyoukyou" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="SelectHosyousyoHakkouJyoukyou_SelectedIndexChanged">
                                        </asp:DropDownList><span id="SpanHosyousyoHakkouJyoukyou" runat="server"></span>
                                        <span id="SpanHosyousyoHakJykyHosyouUmu" runat="server" style="font-weight:bold;font-size:13px;"></span>
                                    </td>
                                    <td class="koumokuMei" colspan="2">
                                        保証書発行状況設定日
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="TextHosyousyoHakkouJyoukyouSetteiDate" CssClass="date readOnlyStyle"
                                            Text="" MaxLength="10" Style="width: 70px" ReadOnly="true" TabIndex="-1" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <!-- 5行目 -->
                                <tr>
                                    <td class="koumokuMei">
                                        保証書発行日
                                    </td>
                                    <td id="TdHosyousyoHakkouDate" runat="server">
                                        <asp:TextBox ID="TextHosyousyoHakkouDate" CssClass="date" MaxLength="10" Text=""
                                            runat="server" OnTextChanged="TextHosyousyoHakkouDate_TextChanged" />
                                    </td>
                                    <td class="koumokuMei">
                                        物件状況&nbsp;<input type="button" id="ButtonHkKousin" value="更新" runat="server" style="height:20px;"/>
                                    </td>
                                    <td id="TdBukkenJyky" runat="server">
                                        <span id="SpanBukkenJyky" runat="server" style="font-size:13px"></span><input type="hidden"
                                            id="HiddenBukkenJyky" runat="server" />
                                    </td>
                                    <td class="koumokuMei">
                                        付保証明書FLG
                                    </td>
                                    <td id="TdFuhoSyoumeisyoFlg" runat="server">
                                        <asp:DropDownList ID="SelectFuhoSyoumeisyoFlg" CssClass="" Style="display: inline;"
                                            runat="server" AutoPostBack="true" OnSelectedIndexChanged="SelectFuhoSyoumeisyoFlg_SelectedIndexChanged">
                                            <asp:ListItem Value="" Text="" Selected="true"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="有り"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="なし"></asp:ListItem>
                                        </asp:DropDownList><span id="SpanFuhoSyoumeisyoFlg" runat="server"></span>
                                        <input type="button" id="ButtonFuhoSyoumeisyoFlg" runat="server" style="display: none;"
                                            onserverclick="ButtonFuhoSyoumeisyoFlgCancel_ServerClick" />
                                    </td>
                                    <td class="koumokuMei">
                                        付保証明書発送日
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextFuhoSyoumeisyoHassouDate" CssClass="date readOnlyStyle" MaxLength="10"
                                            Text="" runat="server" ReadOnly="true" TabIndex="-1" />
                                    </td>
                                </tr>
                                <!-- 6行目 -->
                                <tr>
                                    <td class="koumokuMei">
                                        保証書発送日
                                    </td>
                                    <td id="TdHosyousyoHassouDate" runat="server">
                                        <asp:TextBox ID="TextHosyousyoHassouDate" CssClass="date readOnlyStyle" MaxLength="10" 
                                        Text="" runat="server" ReadOnly="true" TabIndex="-1"/>
                                    </td>
                                    <td class="koumokuMei">
                                        発行依頼方法
                                    </td>
                                    <td colspan="5">
                                        <span id="spanHosyousyoHakHouhou" runat="server" />
                                    </td>
                                </tr>
                                <!-- 7行目 -->
                                <tr>
                                    <!-- 旧項目名「保障開始日」 -->
                                    <td class="koumokuMei">
                                        お引渡し日
                                    </td>
                                    <td id="TdHosyouKaisiDate" runat="server">
                                        <asp:TextBox ID="TextHosyouKaisiDate" CssClass="date" MaxLength="10" Text="" runat="server" />
                                    </td>
                                    <td class="koumokuMei">
                                        商品設定状況
                                    </td>
                                    <td>
                                        <span id="SpanHosyouSyouhinUmu" runat="server" style="font-weight:bold;font-size:13px;"></span>
                                    </td>
                                    <td class="koumokuMei" style="display:none">
                                        保証有無
                                    </td>
                                    <td id="TdHosyouUmu" runat="server" style="display:none">
                                        <asp:DropDownList ID="SelectHosyouUmu" CssClass="" Style="display: inline;" runat="server">
                                            <asp:ListItem Value="" Text="" Selected="true"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="有"></asp:ListItem>
                                        </asp:DropDownList><span id="SpanHosyouUmu" runat="server"></span>
                                    </td>
                                    <td class="koumokuMei">
                                        保証期間
                                    </td>
                                    <td id="TdHosyouKikan" runat="server">
                                        <asp:TextBox ID="TextHosyouKikan" Style="width: 40px" CssClass="number"
                                            Text="" MaxLength="2" runat="server" TabIndex="10" />年
                                    </td>
                                    <td class="koumokuMei">
                                        保証書タイプ
                                    </td>
                                    <td id="TdHosyousyoType" runat="server">
                                        <asp:TextBox ID="TextHosyousyoType" CssClass="readOnlyStyle" Style="width: 60px"
                                            Text="" runat="server" ReadOnly="true" TabIndex="-1" />
                                    </td>
                                </tr>
                                <!-- 8行目 -->
                                <tr>
                                    <td class="koumokuMei">
                                        保証なし理由
                                    </td>
                                    <td colspan="5" runat="server">
                                        <asp:DropDownList ID="SelectHosyouNasiRiyuu" Style="width: 300px" runat="server">
                                        </asp:DropDownList><span id="SpanHosyouNasiRiyuu" runat="server"></span>
                                        <asp:DropDownList ID="SelectHannyouNo" Style="display: none;" runat="server" TabIndex="-1">
                                        </asp:DropDownList><asp:TextBox ID="TextHosyouNasiRiyuu" Style="width: 200px; ime-mode: active;" runat="server"
                                            MaxLength="40"/>
                                    </td>
                                    <td class="koumokuMei">
                                        (調査)請求書発行日
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextTyousaSeikyuusyoHakkouDate" CssClass="date readOnlyStyle" Text=""
                                            MaxLength="10" Style="width: 70px" ReadOnly="true" TabIndex="-1" runat="server" />
                                    </td>
                                </tr>
                                <!-- 9行目 -->
                                <tr>
                                    <td class="koumokuMei">
                                        保険会社
                                    </td>
                                    <td colspan="3">
                                        <span id="SpanHkHokenGaisya" runat="server" style="font-size:13px"></span>
                                    </td>
                                    <td class="koumokuMei">
                                        引渡し前保険
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextHwMaeHkn" CssClass="readOnlyStyle" Style="width: 20px"
                                            Text="" MaxLength="1" runat="server" ReadOnly="true" TabIndex="-1" />
                                    </td>
                                    <td class="koumokuMei">
                                        引渡し後保険
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextHwAtohkn" CssClass="readOnlyStyle" Style="width: 20px"
                                            Text="" MaxLength="1" runat="server" ReadOnly="true" TabIndex="-1" />
                                    </td>
                                </tr>
                                <!-- 10行目 -->
                                <tr>
                                    <td colspan="8" style="padding: 0px;">
                                        <table class="innerTable" cellpadding="0" cellspacing="0">
                                            <tr class="firstRow">
                                                <td class="koumokuMei firstCol" style="width: 80px">
                                                    金額
                                                </td>
                                                <td class="shouhinTableTitleSum">
                                                    調査発注書合計金額
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextTyousaHattyuusyoGoukeiKingaku" CssClass="kingaku readOnlyStyle"
                                                        MaxLength="7" Text="0" Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                                <td class="shouhinTableTitleSum">
                                                    調査合計入金額(税込)
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextTyousaGoukeiNyuukingaku" CssClass="kingaku readOnlyStyle" MaxLength="7"
                                                        Text="0" Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                                <td class="shouhinTableTitleNyuukin">
                                                    残額
                                                </td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="TextTyousaZangaku" CssClass="kingaku readOnlyStyle" MaxLength="7"
                                                        Text="0" Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <!-- 11行目 -->
                                <tr class="tableSpacer">
                                    <td colspan="8">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="koumokuMei">
                                        置換工事
                                    </td>
                                    <td colspan="7" style="padding: 0px">
                                        <table class="subTable" style="font-weight: bold;">
                                            <tr>
                                                <td>
                                                    写真受理：
                                                    <asp:TextBox ID="TextSyasinJuri" runat="server" CssClass="readOnlyStyle" Style="width: 80px"
                                                        Text="" ReadOnly="true" TabIndex="-1" />
                                                </td>
                                                <td>
                                                    写真コメント：
                                                    <asp:TextBox ID="TextSyasinComment" runat="server" CssClass="readOnlyStyle" Style="width: 500px"
                                                        Text="" ReadOnly="true" TabIndex="-1" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr class="tableSpacer">
                                    <td colspan="8">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="koumokuMei">
                                        商品１
                                    </td>
                                    <td colspan="7">
                                        <asp:TextBox ID="TextSyouhin1A" CssClass="itemCd readOnlyStyle" ReadOnly="true" Text=""
                                            runat="server" TabIndex="-1" />
                                        <asp:TextBox ID="TextSyouhin1B" CssClass="itemNm readOnlyStyle" ReadOnly="true" Text=""
                                            runat="server" Style="width: 300px" TabIndex="-1" />
                                    </td>
                                </tr>
                                <tr id="TrSyouhin21" runat="server">
                                    <td class="koumokuMei">
                                        商品２_１
                                    </td>
                                    <td colspan="7">
                                        <asp:TextBox ID="TextSyouhin21A" CssClass="itemCd readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" TabIndex="-1" />
                                        <asp:TextBox ID="TextSyouhin21B" CssClass="itemNm readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" Style="width: 300px" TabIndex="-1" />
                                    </td>
                                </tr>
                                <tr id="TrSyouhin22" runat="server">
                                    <td class="koumokuMei">
                                        商品２_２
                                    </td>
                                    <td colspan="7">
                                        <asp:TextBox ID="TextSyouhin22A" CssClass="itemCd readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" TabIndex="-1" />
                                        <asp:TextBox ID="TextSyouhin22B" CssClass="itemNm readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" Style="width: 300px" TabIndex="-1" />
                                    </td>
                                </tr>
                                <tr id="TrSyouhin23" runat="server">
                                    <td class="koumokuMei">
                                        商品２_３
                                    </td>
                                    <td colspan="7">
                                        <asp:TextBox ID="TextSyouhin23A" CssClass="itemCd readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" TabIndex="-1" />
                                        <asp:TextBox ID="TextSyouhin23B" CssClass="itemNm readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" Style="width: 300px" TabIndex="-1" />
                                    </td>
                                </tr>
                                <tr id="TrSyouhin24" runat="server">
                                    <td class="koumokuMei">
                                        商品２_４
                                    </td>
                                    <td colspan="7">
                                        <asp:TextBox ID="TextSyouhin24A" CssClass="itemCd readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" TabIndex="-1" />
                                        <asp:TextBox ID="TextSyouhin24B" CssClass="itemNm readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" Style="width: 300px" TabIndex="-1" />
                                    </td>
                                </tr>
                                <tr id="TrSyouhin31" runat="server">
                                    <td class="koumokuMei">
                                        商品３_１
                                    </td>
                                    <td colspan="7">
                                        <asp:TextBox ID="TextSyouhin31A" CssClass="itemCd readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" TabIndex="-1" />
                                        <asp:TextBox ID="TextSyouhin31B" CssClass="itemNm readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" Style="width: 300px" TabIndex="-1" />
                                    </td>
                                </tr>
                                <tr id="TrSyouhin32" runat="server">
                                    <td class="koumokuMei">
                                        商品３_２
                                    </td>
                                    <td colspan="7">
                                        <asp:TextBox ID="TextSyouhin32A" CssClass="itemCd readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" TabIndex="-1" />
                                        <asp:TextBox ID="TextSyouhin32B" CssClass="itemNm readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" Style="width: 300px" TabIndex="-1" />
                                    </td>
                                </tr>
                                <tr id="TrSyouhin33" runat="server">
                                    <td class="koumokuMei">
                                        商品３_３
                                    </td>
                                    <td colspan="7">
                                        <asp:TextBox ID="TextSyouhin33A" CssClass="itemCd readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" TabIndex="-1" />
                                        <asp:TextBox ID="TextSyouhin33B" CssClass="itemNm readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" Style="width: 300px" TabIndex="-1" />
                                    </td>
                                </tr>
                                <tr id="TrSyouhin34" runat="server">
                                    <td class="koumokuMei">
                                        商品３_４
                                    </td>
                                    <td colspan="7">
                                        <asp:TextBox ID="TextSyouhin34A" CssClass="itemCd readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" TabIndex="-1" />
                                        <asp:TextBox ID="TextSyouhin34B" CssClass="itemNm readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" Style="width: 300px" TabIndex="-1" />
                                    </td>
                                </tr>
                                <tr id="TrSyouhin35" runat="server">
                                    <td class="koumokuMei">
                                        商品３_５
                                    </td>
                                    <td colspan="7">
                                        <asp:TextBox ID="TextSyouhin35A" CssClass="itemCd readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" TabIndex="-1" />
                                        <asp:TextBox ID="TextSyouhin35B" CssClass="itemNm readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" Style="width: 300px" TabIndex="-1" />
                                    </td>
                                </tr>
                                <tr id="TrSyouhin36" runat="server">
                                    <td class="koumokuMei">
                                        商品３_６
                                    </td>
                                    <td colspan="7">
                                        <asp:TextBox ID="TextSyouhin36A" CssClass="itemCd readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" TabIndex="-1" />
                                        <asp:TextBox ID="TextSyouhin36B" CssClass="itemNm readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" Style="width: 300px" TabIndex="-1" />
                                    </td>
                                </tr>
                                <tr id="TrSyouhin37" runat="server">
                                    <td class="koumokuMei">
                                        商品３_７
                                    </td>
                                    <td colspan="7">
                                        <asp:TextBox ID="TextSyouhin37A" CssClass="itemCd readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" TabIndex="-1" />
                                        <asp:TextBox ID="TextSyouhin37B" CssClass="itemNm readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" Style="width: 300px" TabIndex="-1" />
                                    </td>
                                </tr>
                                <tr id="TrSyouhin38" runat="server">
                                    <td class="koumokuMei">
                                        商品３_８
                                    </td>
                                    <td colspan="7">
                                        <asp:TextBox ID="TextSyouhin38A" CssClass="itemCd readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" TabIndex="-1" />
                                        <asp:TextBox ID="TextSyouhin38B" CssClass="itemNm readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" Style="width: 300px" TabIndex="-1" />
                                    </td>
                                </tr>
                                <tr id="TrSyouhin39" runat="server">
                                    <td class="koumokuMei">
                                        商品３_９
                                    </td>
                                    <td colspan="7">
                                        <asp:TextBox ID="TextSyouhin39A" CssClass="itemCd readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" TabIndex="-1" />
                                        <asp:TextBox ID="TextSyouhin39B" CssClass="itemNm readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" Style="width: 300px" TabIndex="-1" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <br />
                    <div id="divKairyouKouji" runat="server">
                        <!-- 画面中央部・改良工事 -->
                        <table style="text-align: left; width: 100%;" id="Table1" class="mainTable" cellpadding="0"
                            cellspacing="0" border="1">
                            <!-- ヘッダ部 -->
                            <thead>
                                <tr>
                                    <th class="tableTitle" style="padding: 0px" colspan="8">
                                        <a id="AncKairyouKouji" runat="server">改良工事</a>
                                        <input type="hidden" id="HiddenKairyouKoujiStyle" runat="server" value="inline" />
                                    </th>
                                </tr>
                            </thead>
                            <tbody id="TbodyKairyoKouji" runat="server">
                                <!-- 1行目 -->
                                <tr>
                                    <td class="koumokuMei" style="width: 60px;">
                                        判定者
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextHanteisya" Style="width: 150px" CssClass="readOnlyStyle" Text=""
                                            ReadOnly="true" TabIndex="-1" runat="server" />
                                    </td>
                                    <td class="koumokuMei" style="width: 70px;">
                                        判定種別
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextHanteiSyubetu" Style="width: 150px" CssClass="readOnlyStyle"
                                            Text="" ReadOnly="true" TabIndex="-1" runat="server" />
                                    </td>
                                    <td class="koumokuMei" style="width: 60px;">
                                        判定
                                    </td>
                                    <td colspan="3" style="width: 350px">
                                        <span id="SpanHantei1" runat="server" style="width: 150px" class="readOnlyStyle"></span>
                                        <span id="SpanHanteiSetuzokuMoji" runat="server" style="width: 50px" class="readOnlyStyle">
                                        </span><span id="SpanHantei2" runat="server" style="width: 150px" class="readOnlyStyle">
                                        </span>
                                    </td>
                                </tr>
                                <!-- 2行目 -->
                                <tr>
                                    <td colspan="8" style="padding: 0px;">
                                        <table class="innerTable" cellpadding="0" cellspacing="0">
                                            <tr class="firstRow">
                                                <!-- 1行目 -->
                                                <td style="width: 80px" class="koumokuMei firstCol">
                                                    工事会社
                                                </td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="TextKoujiGaisya" Style="width: 100px" CssClass="readOnlyStyle" Text=""
                                                        ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                                <td class="koumokuMei">
                                                    改良工事種別
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="TextKairyouKoujiSyubetu" CssClass="readOnlyStyle" Style="width: 150px;
                                                        size: 60;" Text="" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                            </tr>
                                            <!-- 2行目 -->
                                            <tr>
                                                <td class="koumokuMei firstCol">
                                                    改良工事日
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextKairyouKoujiDate" CssClass="date readOnlyStyle" MaxLength="10"
                                                        Text="" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                                <td class="koumokuMei">
                                                    (工事)請求書発行日
                                                </td>
                                                <td colspan="4" style="width: 300px">
                                                    <asp:TextBox ID="TextKoujiSeikyusyoHakkouDate" CssClass="date readOnlyStyle" MaxLength="10"
                                                        Text="" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                            </tr>
                                            <!-- 3行目 -->
                                            <tr>
                                                <td style="width: 120px" class="koumokuMei firstCol">
                                                    工事報告書受理
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextKoujiHoukokusyoJuri" Style="width: 90%" CssClass="readOnlyStyle"
                                                        Text="" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                                <td class="koumokuMei">
                                                    工事報告書受理日
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextKoujiHoukokusyoJuriDate" CssClass="date readOnlyStyle" MaxLength="10"
                                                        Text="" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                                <td class="koumokuMei">
                                                    工事報告書発送日
                                                </td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="TextKoujiHoukokusyoHassouDate" CssClass="date readOnlyStyle" MaxLength="10"
                                                        Text="" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <!-- 4行目 -->
                                <tr>
                                    <td colspan="8" style="padding: 0px;">
                                        <table class="innerTable" cellpadding="0" cellspacing="0">
                                            <tr class="firstRow">
                                                <td class="koumokuMei firstCol" style="width: 80px">
                                                    金額
                                                </td>
                                                <td class="shouhinTableTitleSum">
                                                    工事発注書合計金額
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextKoujiHattyuusyoGoukeiKingaku" CssClass="kingaku readOnlyStyle"
                                                        MaxLength="7" Text="0" Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                                <td class="shouhinTableTitleSum">
                                                    工事合計入金額(税込)
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextKoujiGoukeiNyuukingaku" CssClass="kingaku readOnlyStyle" MaxLength="7"
                                                        Text="0" Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                                <td class="shouhinTableTitleNyuukin">
                                                    残額
                                                </td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="TextKoujiZangaku" CssClass="kingaku readOnlyStyle" MaxLength="7"
                                                        Text="0" Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr class="tableSpacer">
                                    <td colspan="8">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="koumokuMei">
                                        工事商品
                                    </td>
                                    <td colspan="7">
                                        <asp:TextBox ID="TextKoujiSyouhinCd" CssClass="itemCd readOnlyStyle" ReadOnly="true"
                                            TabIndex="-1" Text="" runat="server" />
                                        <asp:TextBox ID="TextKoujiSyouhinMei" CssClass="itemNm readOnlyStyle" ReadOnly="true"
                                            TabIndex="-1" Text="" runat="server" Style="width: 300px" />
                                        &nbsp;&nbsp;&nbsp; <span id="SpanKoujiGaisyaSeikyuu" style="color: red; font-weight: bold;"
                                            runat="server"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="koumokuMei">
                                        追加工事商品
                                    </td>
                                    <td colspan="7">
                                        <asp:TextBox ID="TextTuikaKoujiSyouhinCd" CssClass="itemCd readOnlyStyle" ReadOnly="true"
                                            TabIndex="-1" Text="" runat="server" />
                                        <asp:TextBox ID="TextTuikaKoujiSyouhinMei" CssClass="itemNm readOnlyStyle" ReadOnly="true"
                                            TabIndex="-1" Text="" runat="server" Style="width: 300px" />
                                        &nbsp;&nbsp;&nbsp; <span id="SpanTuikaKoujiKaisyaSeikyuu" style="color: red; font-weight: bold;"
                                            runat="server"></span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <br />
                    <div id="divSaihakkou" runat="server">
                        <!-- 画面下部・再発行 -->
                        <table style="text-align: left; width: 100%;" id="Table3" class="mainTable" cellpadding="0"
                            cellspacing="0">
                            <!-- ヘッダ部 -->
                            <thead>
                                <tr>
                                    <th class="tableTitle" style="padding: 0px" colspan="6">
                                        <a id="AncSaiHakkou" runat="server">再発行</a>
                                        <input type="hidden" id="HiddenSaiHakkouStyle" runat="server" value="inline" />
                                    </th>
                                </tr>
                            </thead>
                            <!-- ボディ部 -->
                            <tbody id="TbodySaiHakkou" runat="server">
                                <!-- 1行目 -->
                                <tr id="TrShSaihakkou" runat="server">
                                    <td class="koumokuMei">
                                        再発行日
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextSaihakkouDate" CssClass="date" MaxLength="10" Text="" runat="server"
                                            OnTextChanged="TextSaihakkouDate_TextChanged" />
                                    </td>
                                    <td class="koumokuMei">
                                        再発行理由
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="TextSaihakkouRiyuu" Style="width: 300px" CssClass="" Text="" runat="server"
                                            MaxLength="40" />
                                        <input type="text" id="TextFocusBounderSaihakkouRiyuu" runat="server" tabindex="-1"
                                            style="width: 0px;" onfocus="setFocusSaihakkouRiyuu();" />
                                    </td>
                                </tr>
                                <tr class="tableSpacer">
                                    <td colspan="6">
                                    </td>
                                </tr>
                                <!-- 2行目[Table3] -->
                                <tr>
                                    <td colspan="6" style="border: 0px solid red; padding: 0px;">
                                        <table cellpadding="1" class="itemTable innerTable">
                                            <tbody>
                                                <tr class="firstRow">
                                                    <td colspan="8" class="firstCol" style="text-align: left">
                                                        <span id="SpanShUriageSyorizumi" style="color: red; font-weight: bold;" runat="server">
                                                        </span>
                                                        <input type="hidden" id="HiddenShUriageKeijyouDate" runat="server" />
                                                        <uc2:SeikyuuSiireLinkCtrl ID="ucSeikyuuSiireLinkSai" runat="server" />
                                                    </td>
                                                    <td style="text-align: center;" class="shouhinTableTitle">
                                                        請求先
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextShSeikyuusaki" Style="width: 80px" CssClass="readOnlyStyle"
                                                            TabIndex="-1" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr class="shouhinTableTitle">
                                                    <td class="firstCol" rowspan="2">
                                                        請求
                                                    </td>
                                                    <td>
                                                        商品コード
                                                    </td>
                                                    <td rowspan="2">
                                                        工務店請求<br />
                                                        税抜金額
                                                    </td>
                                                    <td rowspan="2">
                                                        実請求<br />
                                                        税抜金額
                                                    </td>
                                                    <td>
                                                        消費税
                                                    </td>
                                                    <td rowspan="2">
                                                        請求書<br />
                                                        発行日
                                                    </td>
                                                    <td rowspan="2">
                                                        売上<br />
                                                        年月日
                                                    </td>
                                                    <td rowspan="2">
                                                        発注書<br />
                                                        確定
                                                    </td>
                                                    <td rowspan="2">
                                                        発注書<br />
                                                        金額
                                                    </td>
                                                    <td rowspan="2">
                                                        発注書<br />
                                                        確認日
                                                    </td>
                                                </tr>
                                                <tr class="shouhinTableTitle">
                                                    <td>
                                                        商品名
                                                    </td>
                                                    <td>
                                                        税込金額
                                                    </td>
                                                </tr>
                                                <tr id="TrShSyouhin" runat="server">
                                                    <td class="firstCol" style="width: 40px">
                                                        <asp:DropDownList ID="SelectShSeikyuuUmu" CssClass="" Style="display: inline;" runat="server"
                                                            AutoPostBack="true" OnSelectedIndexChanged="SelectShSeikyuu_SelectedIndexChanged">
                                                            <asp:ListItem Value="" Text="" Selected="true"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="有"></asp:ListItem>
                                                            <asp:ListItem Value="0" Text="無"></asp:ListItem>
                                                        </asp:DropDownList><span id="SpanShSeikyuuUmu" runat="server"></span>
                                                    </td>
                                                    <td class="itemNm" style="width: 150px">
                                                        <asp:TextBox ID="TextShSyouhinCd" CssClass="itemCd readOnlyStyle" Style="width: 40px"
                                                            ReadOnly="true" Text="" TabIndex="-1" runat="server" />
                                                        <br />
                                                        <span id="SpanShSyouhinMei" class="itemNm" runat="server"></span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextShKoumutenSeikyuuKingaku" CssClass="kingaku" MaxLength="7" Text=""
                                                            Style="width: 60px" runat="server" OnTextChanged="TextShKoumutenSeikyuuKingaku_TextChanged" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextShJituseikyuuKingaku" CssClass="kingaku" MaxLength="7" Text=""
                                                            Style="width: 60px" runat="server" OnTextChanged="TextShJituseikyuuKingaku_TextChanged" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextShSyouhizei" CssClass="kingaku readOnlyStyle" MaxLength="7"
                                                            Text="" Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" /><asp:HiddenField
                                                                ID="HiddenShZeiKbn" runat="server" />
                                                            <asp:HiddenField ID="HiddenShSiireGaku" runat="server" />
                                                            <asp:HiddenField ID="HiddenShSiireSyouhiZei" runat="server" />
                                                        <br />
                                                        <asp:TextBox ID="TextShZeikomiKingaku" CssClass="kingaku readOnlyStyle" MaxLength="7"
                                                            Text="" Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextShSeikyuusyoHakkouDate" CssClass="date" MaxLength="10" Text=""
                                                            runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextShUriageNengappi" CssClass="date readOnlyStyle" MaxLength="10"
                                                            Text="" ReadOnly="true" TabIndex="-1" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextShHattyuusyoKakutei" Style="width: 60px" CssClass="readOnlyStyle"
                                                            Text="" ReadOnly="true" TabIndex="-1" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextShHattyuusyoKingaku" CssClass="kingaku readOnlyStyle" MaxLength="7"
                                                            Text="" Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextShHattyuusyoKakuninDate" CssClass="date readOnlyStyle" MaxLength="10"
                                                            Text="" ReadOnly="true" TabIndex="-1" runat="server" /><asp:HiddenField ID="HiddenShUpdDateTime"
                                                                runat="server" />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <!-- 14行目[Table4] -->
                                <tr align="center">
                                    <td colspan="6" style="padding: 3px;">
                                        <table class="miniTable" cellpadding="0" cellspacing="0">
                                            <tr class="">
                                                <td colspan="2" class="shouhinTableTitleNyuukin" style="width: 150px;">
                                                    入金額(税込)
                                                </td>
                                                <td colspan="2" class="shouhinTableTitleNyuukin" style="width: 150px;">
                                                    残額
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="">
                                                    <asp:TextBox ID="TextShNyuukingaku" CssClass="kingaku readOnlyStyle" MaxLength="7"
                                                        Text="0" Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="TextShZangaku" CssClass="kingaku readOnlyStyle" MaxLength="7" Text="0"
                                                        Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr class="tableSpacer">
                                    <td colspan="6">
                                    </td>
                                </tr>
                                <!-- 15行目[Table5] -->
                                <tr>
                                    <td colspan="6" style="padding: 0px;">
                                        <table cellpadding="0" cellspacing="0" class="itemTable innerTable">
                                            <tbody>
                                                <tr class="firstRow">
                                                    <td colspan="9" class="firstCol" style="text-align: left">
                                                        <span id="SpanKyKaiyakuUriageSyorizumi" style="color: red; font-weight: bold;" runat="server">
                                                        </span>
                                                        <input type="hidden" id="HiddenKyKaiyakuUriageKeijyouDate" runat="server" />
                                                        <uc2:SeikyuuSiireLinkCtrl ID="ucSeikyuuSiireLinkKai" runat="server" />
                                                    </td>
                                                    <td style="text-align: center;" class="shouhinTableTitle">
                                                        請求先
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextKySeikyuusaki" Style="width: 80px" CssClass="readOnlyStyle"
                                                            TabIndex="-1" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr class="shouhinTableTitle">
                                                    <td colspan="2" rowspan="2" class="firstCol">
                                                        解約払戻<br />
                                                        申請有無
                                                    </td>
                                                    <td rowspan="2">
                                                        商品コード
                                                    </td>
                                                    <td rowspan="2">
                                                        工務店請求<br />
                                                        税抜金額
                                                    </td>
                                                    <td rowspan="2">
                                                        実請求<br />
                                                        税抜金額
                                                    </td>
                                                    <td>
                                                        消費税
                                                    </td>
                                                    <td rowspan="2">
                                                        請求書<br />
                                                        発行日
                                                    </td>
                                                    <td rowspan="2">
                                                        売上<br />
                                                        年月日
                                                    </td>
                                                    <td rowspan="2">
                                                        発注書<br />
                                                        確定
                                                    </td>
                                                    <td rowspan="2">
                                                        発注書<br />
                                                        金額
                                                    </td>
                                                    <td rowspan="2">
                                                        発注書<br />
                                                        確認日
                                                    </td>
                                                </tr>
                                                <tr class="shouhinTableTitle">
                                                    <td>
                                                        税込金額
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" class="firstCol" id="TdKyKaiyakuHaraimodosiSinseiUmu" runat="server">
                                                        <asp:DropDownList ID="SelectKyKaiyakuHaraimodosiSinseiUmu" CssClass="" Style="display: inline;"
                                                            runat="server" AutoPostBack="true" OnSelectedIndexChanged="SelectKyKaiyakuHaraimodosiSinseiUmu_SelectedIndexChanged">
                                                            <asp:ListItem Value="" Selected="true"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="有"></asp:ListItem>
                                                        </asp:DropDownList><span id="SpanKyKaiyakuHaraimodosiSinseiUmu" runat="server"></span>
                                                    </td>
                                                    <td class="itemNm">
                                                        <asp:TextBox ID="TextKySyouhinCd" CssClass="itemCd readOnlyStyle" ReadOnly="true"
                                                            Text="" Style="width: 40px" TabIndex="-1" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextKyKoumutenSeikyuuKingaku" CssClass="kingaku readOnlyStyle" MaxLength="7"
                                                            Text="" Style="width: 60px" runat="server" ReadOnly="true" TabIndex="-1" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextKyJituseikyuuKingaku" CssClass="kingaku readOnlyStyle" MaxLength="7"
                                                            Text="" Style="width: 60px" runat="server" ReadOnly="true" TabIndex="-1" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextKySyouhizei" CssClass="kingaku readOnlyStyle" MaxLength="7"
                                                            Text="" Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" /><asp:HiddenField
                                                                ID="HiddenKyZeiKbn" runat="server" />
                                                            <asp:HiddenField ID="HiddenKySiireGaku" runat="server" />
                                                            <asp:HiddenField ID="HiddenKySiireSyouhiZei" runat="server" />
                                                        <br />
                                                        <asp:TextBox ID="TextKyZeikomiKingaku" CssClass="kingaku readOnlyStyle" MaxLength="7"
                                                            Text="" Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" />
                                                    </td>
                                                    <td id="TdKySeikyuusyoHakkouDate" runat="server">
                                                        <asp:TextBox ID="TextKySeikyuusyoHakkouDate" CssClass="date" MaxLength="10" Text=""
                                                            runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextKyUriageNengappi" CssClass="date readOnlyStyle" MaxLength="10"
                                                            Text="" ReadOnly="true" TabIndex="-1" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextKyHattyuusyoKakutei" Style="width: 60px" CssClass="readOnlyStyle"
                                                            Text="" ReadOnly="true" TabIndex="-1" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextKyHattyuusyoKingaku" CssClass="kingaku readOnlyStyle" MaxLength="7"
                                                            Text="" Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextKyHattyuusyoKakuninDate" CssClass="date readOnlyStyle" MaxLength="10"
                                                            Text="" ReadOnly="true" TabIndex="-1" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="11" class="firstCol" style="text-align: left">
                                                        <span id="SpanKyHenkinSyorizumi" style="color: blue; font-weight: bold;" runat="server">
                                                        </span>&nbsp;&nbsp; <span id="SpanKyHenkinSyoriDate" style="color: blue; font-weight: bold;"
                                                            runat="server"></span>
                                                        <asp:HiddenField ID="HiddenKyUpdDateTime" runat="server" />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <!-- 画面下部・ボタン -->
    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tbody>
            <tr>
                <th style="text-align: center;">
                    <input type="button" id="ButtonTouroku2" value="登録 / 修正 実行" style="font-weight: bold;
                        font-size: 18px; width: 155px; color: black; height: 30px; background-color: fuchsia"
                        onclick="return ButtonTourokuSyuuseiJikkou2_onclick()" runat="server" />&nbsp;
                </th>
            </tr>
        </tbody>
    </table>
</asp:Content>

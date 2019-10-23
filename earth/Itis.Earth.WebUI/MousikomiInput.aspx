<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="MousikomiInput.aspx.vb" Inherits="Itis.Earth.WebUI.MousikomiInput"
    Title="EARTH 申込入力" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script type="text/javascript">
        history.forward();
    
        //画面起動時にウィンドウサイズをディスプレイに合わせる
        window.moveTo(0, 0);
        window.resizeTo(window.screen.availWidth, window.screen.availHeight);

        //加盟店検索処理を呼び出す
        function callKameitenSearch(obj){
            objEBI("<%= kameitenSearchType.clientID %>").value = "";
            if(obj.value == ""){
                objEBI("<%= kameitenSearchType.clientID %>").value = "1";
                objEBI("<%= ButtonKITourokuBangou.clientID %>").click();
            }
        }

        //加盟店検索処理を商品1から呼び出す
        function callKameitenSearchFromSyouhin1(obj){
            var objBangou = objEBI("<%= TextKITourokuBangou.clientID %>"); //登録番号(加盟店コード)
            var objSyouhin1 = objEBI("<%= choSyouhin1.clientID %>"); //商品1
            objEBI("<%= kameitenSearchType.clientID %>").value = "";
            if(objSyouhin1.value == ""){
                objEBI("<%= kameitenSearchType.clientID %>").value = "1";
                objEBI("<%= ButtonKITourokuBangou.clientID %>").click();
            }else{
                if(objBangou.value != "" && objSyouhin1.value != ""){
                    objEBI("<%= ButtonKITourokuBangou.clientID %>").click();
                 }
            }
        }

        //重複物件チェック処理
        function ChgTyoufukuBukken(objThis){
            objEBI("<%= HiddenTyoufukuKakuninTargetId.clientID %>").value = objThis.id;
            objEBI("<%= ButtonExeTyoufukuCheck.clientID %>").click();
        }

        //調査会社検索処理を呼び出す
        function callTyousakaisyaSearch(obj){
            objEBI("<%= tyousakaisyaSearchType.clientID %>").value = "";
            if(obj.value == ""){
                objEBI("<%= tyousakaisyaSearchType.clientID %>").value = "1";
                objEBI("<%= ButtonSITysGaisya.clientID %>").click();
            }
        }

        //住所転記ボタン押下時の処理
        function juushoTenki_onclick() {
            var objJuusho3 = objEBI("<%= TextBukkenJyuusyo3.clientID %>");
            var objBikou = objEBI("<%= TextSIBikou.clientID %>");
            if(objJuusho3.value != ""){
                if(objBikou.value != ""){
                    objBikou.value += " ";
                }
                objBikou.value += "住所続き：" + objJuusho3.value;
            }
        }
        
        //プルダウンの値が特定値の場合、指定のエレメントのReadOnlyを外し、項目名ラベルの表示状態を切替える
        function checkSonota(flgVal, targetId){
            if(flgVal){
                objEBI(targetId).style.visibility = "visible";

            }else{
                objEBI(targetId).value = "";
                objEBI(targetId).style.visibility = "hidden";
            }
        }
        
        //調査立会者の有無によってチェックボックスの表示切替を行なう
        function ChgDispTatiaisya(){       
            var objAri = objEBI("<%= RadioTysTatiaisya1.clientID %>");            
            var objSesyusama = objEBI("<%= CheckTysTatiaisyaSesyuSama.clientID %>");
            var objTantousya = objEBI("<%= CheckTysTatiaisyaTantousya.clientID %>");
            var objSonota = objEBI("<%= CheckTysTatiaisyaSonota.clientID %>");

            if(objAri.checked){
                //活性化
                objSesyusama.disabled = false;
                objTantousya.disabled = false;
                objSonota.disabled = false;
            }else{
                //チェックをはずす
                objSesyusama.checked = false;
                objTantousya.checked = false;
                objSonota.checked = false;
                //非活性化
                objSesyusama.disabled = true;
                objTantousya.disabled = true;
                objSonota.disabled = true;               
            }
        }
        
        //区分変更時処理
        function ChgSelectKbn(){           
            var objSelKbn = objEBI("<%= SelectKIKubun.clientID %>"); //区分
            var objBangou = objEBI("<%= TextKITourokuBangou.clientID %>"); //登録番号(加盟店コード)
            
            if(objBangou.value == ""){
                var objBukkenNm = objEBI("<%= TextBukkenMeisyou.clientID %>"); //物件名称
                var objBukkenJyuusyo1 = objEBI("<%= TextBukkenJyuusyo1.clientID %>"); //物件住所１
                var objBukkenJyuusyo2 = objEBI("<%= TextBukkenJyuusyo2.clientID %>"); //物件住所２
                if(objBukkenNm.value + objBukkenJyuusyo1.value + objBukkenJyuusyo2 != ""){
                    //物件名称、住所に入力がある場合、重複チェック実行
                    ChgTyoufukuBukken(objSelKbn);
               }
               return false;
            }
            
            //確認MSG表示
            var strMSG = "<%= Messages.MSG119C %>";
            strMSG = strMSG.replace('@PARAM1','区分');
            strMSG = strMSG.replace('@PARAM2','加盟店');
            if(confirm(strMSG)){
                //前値をセット
                objEBI("<%= HiddenKIKbnMae.clientID %>").value = objSelKbn.value;
                //加盟店クリア処理実行
                objEBI("<%= ButtonKIKameitenClear.clientID %>").click();
            }else{
                objSelKbn.value = objEBI("<%= HiddenKIKbnMae.clientID %>").value;
            }           
        }
        
        //調査概要設定処理呼び出し
        function callSetTysGaiyou(objThis){
            objEBI("<%= actCtrlId.clientID %>").value = objThis.id; //実行トリガーオブジェクトID
            //ボタン押下
            objEBI("<%= btnSetTysGaiyou.clientID %>").click();
        }
        
        //登録処理前チェック
        function CheckTouroku(){
            //基礎着工予定日 大小チェック
            if(!checkDaiSyou(objEBI("<%= TextKsTyakkouYoteiDateFrom.clientID %>") ,objEBI("<%= TextKsTyakkouYoteiDateTo.clientID %>"),"基礎着工予定日"))return false;
            
            return true;
        }
        
        /**
         * 大小チェック
         * @return true/false
         */
        function checkDaiSyou(objFrom,objTo,mess){
            if(objFrom.value != "" && objTo.value != ""){
                if(Number(removeSlash(objFrom.value)) > Number(removeSlash(objTo.value))){
                    alert("<%= Messages.MSG022E %>".replace("@PARAM1",mess));
                    objFrom.select();
                    return false;
                }
            }
            return true;
        }
        
        //変更前コントロールの値を退避して、該当コントロール(Hidden)に保持する
        function SetChangeMaeValue(strTaihiID, strTargetID){
           document.getElementById(strTaihiID).value = document.getElementById(strTargetID).value;
        }
        
        //保証書NO採番状況チェック
        function checkSaiban(){
            
            //調査立会者表示処理
            ChgDispTatiaisya();
        
            var objSaibanButton = objEBI("<%= ButtonTouroku1.clientID %>");
            var objKubun = objEBI("<%= SelectKIKubun.clientID %>");
            
            objSaibanButton.disabled = true;
            //区分選択チェック
            if(objKubun.value == ""){
                alert("<%= Messages.MSG004E %>");
                objKubun.focus();
                objSaibanButton.disabled = false;
                return false;
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
            
            objSaibanButton.disabled = false;
                
            //連棟物件数をhiddenにセット
            objEBI("<%=HiddenRentouBukkenSuu.clientID %>").value = strRentouSuu;
            
            return true;       
        }
        
        //初期非表示エリアの折り畳み処理
        function Oritatami(){
            changeDisplay("<%=TrHJ.clientID %>");
            changeDisplay("<%=TrTS.clientID %>");
            changeDisplay("<%=TrSZ.clientID %>");
            changeDisplay("<%=TrTZ.clientID %>");
            changeDisplay("<%=TrSJ.clientID %>");
             
            //表示切替(+ ⇔ -)
            if(objEBI("<%=TrHJ.clientID %>").style.display == "inline"){
                objEBI('AOritatami').innerHTML = '-';
            }else{
                objEBI('AOritatami').innerHTML = '+';
            }
        }
        
        //onload後処理
        function funcAfterOnload(){
            _d = document;
            
            //分類プルダウンの値表示を切り替え
            ChgDispSelectBunrui(1);
            ChgDispSelectBunrui(2);
            ChgDispSelectBunrui(3);
            
            //連棟処理
            var callRentouNextFlg = objEBI("<%=HiddenCallRentouNextFlg.clientID %>").value;
            
            //入力チェック処理
            var inputChk = objEBI("<%=HiddenInputChk.clientID %>").value;
            
            //連棟続行FLGがたっている場合、処理続行
            if(callRentouNextFlg){
                objEBI("<%=actBtnId.value %>").click();
            }else if(inputChk){
                actClickButton(objEBI("<%=ButtonTouroku1.clientID %>"));
            }
        }
        
        //各種実行ボタン押下時の処理
        function actClickButton(obj){
            var objCallRentouNextFlg = objEBI("<%=HiddenCallRentouNextFlg.clientID %>");
            var objHiddenActButtonId = objEBI("<%=actBtnId.clientID %>");
            var syorikensuuButton = objEBI("<%=ButtonDisplaySyoriKensuu.clientID %>");
            var objAccRentouBukkenSuu = objEBI("<%= HiddenRentouBukkenSuu.clientID %>");
            var objAccSyorikenSuu = objEBI("<%= HiddenSyoriKensuu.clientID %>");

            //入力チェック処理
            var objInputChk = objEBI("<%=HiddenInputChk.clientID %>");
            
            if(objCallRentouNextFlg.value){
                if(objAccRentouBukkenSuu.value > 1){
                    syorikensuuButton.value = "連棟物件登録処理中....  [ " + objAccSyorikenSuu.value + " / " + objAccRentouBukkenSuu.value + " ] 件 完了";
                }
                objHiddenActButtonId.value=obj.id;
                setWindowOverlay(obj,syorikensuuButton);
                objCallRentouNextFlg.value = "";
                //更新処理を実行
                objEBI("<%=ButtonHiddenUpdate.clientID %>").click();
            }else if(objInputChk.value){
                objInputChk.value = "";
                
	            if(checkSaiban()){
                    //採番処理を実行
                    objEBI("<%=ButtonHiddenSaiban.clientID %>").click();
	            }else{
	                return false;
	            }
                
                objHiddenActButtonId.value=obj.id;
                setWindowOverlay(obj);
            }else if(confirm("<%=Messages.MSG017C %>")){
                objInputChk.value = "";
                
                //登録前チェック
	            if(CheckTouroku()){
	                //採番処理を実行
                    objEBI("<%=ButtonHiddenInputChk.clientID %>").click();
	            }else{
                    return false;
                }
            }else{
                objHiddenActButtonId.value='';
                return false;
            }
        }
        
        //******************************
        
        //コントロール接頭辞
        var gVarOyaSettouji = "ctl00_CPH1_";   
        var gVarSelectTmpSyubetu = "SelectSyubetu_";      
        
        // 分類プルダウンの値表示を切り替え
        function ChgDispSelectBunrui(prmCtrlFlg){
            var objSelSyubetu = null;
            var objSelBunrui = null;
            
            //物件履歴の対象コントロールを判断
            switch (prmCtrlFlg){
              case 1:
                objSelSyubetu = objEBI("<%=SelectBRSyubetu1.clientID %>");
                objSelBunrui = objEBI("SelectBRBunrui1");
                break;
              case 2:
                objSelSyubetu = objEBI("<%=SelectBRSyubetu2.clientID %>");
                objSelBunrui = objEBI("SelectBRBunrui2");
                break;
              case 3:
                objSelSyubetu = objEBI("<%=SelectBRSyubetu3.clientID %>");
                objSelBunrui = objEBI("SelectBRBunrui3");
                break;
              default:
                break;
            }
            
            //分類ドロップダウン非活性化
            if(objSelSyubetu.disabled){
                objSelBunrui.disabled = true;
            }else{
                objSelBunrui.disabled = false;
            }
                        
            //オプション全削除
            SelectOptionDelete(objSelBunrui);
            
            if(objSelSyubetu.value == '') return false;
            
            //分類の存在チェック
            if(ChkExitSelectCode(objSelSyubetu.value) == false){
                var strMSG = "<%= Messages.MSG113E %>";
                strMSG = strMSG.replace('@PARAM1','分類');
                alert(strMSG);
                return false;
            }

            //オプション挿入
            SelectOptionInsert(objSelBunrui,objSelSyubetu.value);
            
            //オプションセット
            SelectOptionSet(objSelBunrui,prmCtrlFlg);
                          
        }

        //SelectCodeのオプションを全削除する
        function SelectOptionDelete(objSel){
	        while(objSel.lastChild){
	            objSel.removeChild(objSel.lastChild);
	        }
        }
        
        //種別に対応するコードが存在するかチェックする
        function ChkExitSelectCode(intFlg){           
            if(intFlg == '') return false;
            var varMoto = gVarOyaSettouji + gVarSelectTmpSyubetu + intFlg;
	        var objMoto = objEBI(varMoto);
            var len = objMoto.length;            
            if(len <= 0) return false;
            return true;            
        }
        
        //SelectCodeのオプションを追加する
        function SelectOptionInsert(objSel,intFlg){      
            if(intFlg == '') return false;
            var varMoto = gVarOyaSettouji + gVarSelectTmpSyubetu + intFlg;
	        var objMoto = objEBI(varMoto);
            var len = objMoto.length;
            var varCnt = 1;
            var varIndex = 1;         
            for(varCnt=1; varCnt<len; varCnt++){
                varValue = objMoto.options[varCnt].value;
                varText = objMoto.options[varCnt].text;
                objSel.options[varIndex] = new Option(varText,varValue);
                varIndex++;
            }           
        }
        
        //SelectCodeのオプションをセットする
        function SelectOptionSet(objSel,prmCtrlFlg){
            var objHdnBunrui = null;
            if(objSel == undefined) return false;
            
            //物件履歴の対象コントロールを判断
            switch (prmCtrlFlg){
              case 1:
                objHdnBunrui = objEBI("<%=HiddenBRBunrui1.clientID %>");
                break;
              case 2:
                objHdnBunrui = objEBI("<%=HiddenBRBunrui2.clientID %>");
                break;
              case 3:
                objHdnBunrui = objEBI("<%=HiddenBRBunrui3.clientID %>");
                break;
              default:
                break;
            }
            if(objHdnBunrui == undefined) return false;
            
            //Hiddenの取得およびセット
            objSel.value = objHdnBunrui.value;
        }
        
        //分類ドロップダウンリスト変更時Hidden分類を更新する
        function UpdHiddenBunrui(objSel,prmCtrlFlg){        
            var objHdnBunrui = null;           
            if(objSel == undefined) return false;
            
            //物件履歴の対象コントロールを判断
            switch (prmCtrlFlg){
              case 1:
                objHdnBunrui = objEBI("<%=HiddenBRBunrui1.clientID %>");
                break;
              case 2:
                objHdnBunrui = objEBI("<%=HiddenBRBunrui2.clientID %>");
                break;
              case 3:
                objHdnBunrui = objEBI("<%=HiddenBRBunrui3.clientID %>");
                break;
              default:
                break;
            }
            
            if(objHdnBunrui == undefined) return false;
            
            if(objSel.value == ''){
                objHdnBunrui.value = '';
            }else{
                objHdnBunrui.value = objSel.value;
            }
        }
        
        //種別変更時、分類の中身を作り変える
        function SelectSyubetuOnChg(objSel,prmCtrlFlg){
            var objHdnBunrui = null;
            var objSelSyubetu = null;
            var objSelBunrui = null;
            
            //物件履歴の対象コントロールを判断
            switch (prmCtrlFlg){
              case 1:
                objHdnBunrui = objEBI("<%=HiddenBRBunrui1.clientID %>");
                objSelSyubetu = objEBI("<%=SelectBRSyubetu1.clientID %>");
                objSelBunrui = objEBI("SelectBRBunrui1");
                break;
              case 2:
                objHdnBunrui = objEBI("<%=HiddenBRBunrui2.clientID %>");
                objSelSyubetu = objEBI("<%=SelectBRSyubetu2.clientID %>");
                objSelBunrui = objEBI("SelectBRBunrui2");
                break;
              case 3:
                objHdnBunrui = objEBI("<%=HiddenBRBunrui3.clientID %>");
                objSelSyubetu = objEBI("<%=SelectBRSyubetu3.clientID %>");
                objSelBunrui = objEBI("SelectBRBunrui3");
                break;
              default:
                break;
            }
                     
            //Hidden分類の初期化
            if(objHdnBunrui == undefined) return false;
            objHdnBunrui.value = '';
            
            //オブジェクトの取得
            if(objSelSyubetu == undefined) return false;
            
            //オブジェクトの取得
            if(objSelBunrui == undefined) return false;
            
            //オプション全削除
            SelectOptionDelete(objSelBunrui);
            
            if(objSelSyubetu.value == '') return false;
            //分類の存在チェック
            if(ChkExitSelectCode(objSelSyubetu.value) == false){
                var strMSG = "<%= Messages.MSG113E %>";
                strMSG = strMSG.replace('@PARAM1','分類');
                alert(strMSG);
                return false;
            }
            
            //オプション挿入
            SelectOptionInsert(objSelBunrui,objSelSyubetu.value);
        }
        
    </script>

    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <input type="hidden" id="gamenId" value="shinki" runat="server" />
    <input type="hidden" id="actBtnId" runat="server" />
    <input type="hidden" id="st" runat="server" />
    <input type="hidden" id="tourokuKanryouFlg" runat="server" />
    <input type="hidden" id="callModalFlg" runat="server" />
    <input type="hidden" id="HiddenCallRentouNextFlg" runat="server" />
    <input type="button" id="ButtonDisplaySyoriKensuu" class="SyoriKensuuMessageButton"
        runat="server" tabindex="-1" onfocus="window.focus();" style="display: none;"
        value="処理中・・・" />
    <input type="hidden" id="HiddenSyoriKensuu" runat="server" value="0" />
    <input type="hidden" id="HiddenInputChk" runat="server" />
    <!-- 調査概要設定処理呼び出し -->
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server" RenderMode="Inline">
        <ContentTemplate>
            <input type="button" id="btnSetTysGaiyou" value="調査概要設定" style="display: none" runat="server" />
            <input type="hidden" id="actCtrlId" runat="server" />
            <input type="hidden" id="actPreVal" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <div>
        <input type="hidden" id="HiddenRentouBukkenSuu" runat="server" /><%-- 連棟物件数 --%>
        <input type="hidden" id="HiddenSentouBangou" runat="server" /><%-- 先頭番号 --%>
        <div id="divSelect" runat="server">
        </div>
        <asp:DropDownList runat="server" ID="SelectTmpCode" Style="display: none;">
        </asp:DropDownList>
        <!-- 申込書 -->
        <!-- 画面タイトル -->
        <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="1"
            class="titleTable">
            <tbody>
                <tr>
                    <th>
                        申込入力</th>
                    <th style="text-align: left;">
                        <asp:TextBox ID="TextBangou" Style="width: 260px" CssClass="readOnlyStyle" runat="server"
                            ReadOnly="true" TabIndex="-1" />&nbsp;
                        <input type="button" id="ButtonTouroku1" value="登録 実行" style="font-weight: bold;
                            font-size: 18px; width: 155px; color: black; height: 30px; background-color: fuchsia"
                            runat="server" tabindex="10" />
                        <input id="ButtonHiddenInputChk" style="display: none;" type="button" value="入力チェックhid"
                            runat="server" />
                        <input id="ButtonHiddenSaiban" style="display: none;" type="button" value="採番hid"
                            runat="server" />
                        <input id="ButtonHiddenUpdate" style="display: none;" type="button" value="地盤T更新hid"
                            runat="server" />
                        <input id="ButtonSinkiHikitugi" value="新規(引継)申込" type="button" runat="server" style="font-weight: bold;
                            font-size: 18px; width: 180px; color: black; height: 30px; background-color: fuchsia"
                            tabindex="10" />&nbsp;
                        <input id="ButtonSinki" value="新規申込" type="button" runat="server" style="font-weight: bold;
                            font-size: 18px; width: 100px; color: black; height: 30px; background-color: fuchsia"
                            tabindex="10" />
                    </th>
                </tr>
            </tbody>
        </table>
        <asp:UpdatePanel ID="UpdatePanelHoll" UpdateMode="conditional" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <input type="hidden" runat="server" id="HiddenDateYesterday" /><%--前日--%>
                <input type="hidden" runat="server" id="HiddenDateToday" /><%--当日--%>
                <input type="hidden" id="HiddenAjaxFlg" runat="server" /><%--Ajax処理中フラグ--%>
                <table>
                    <tr>
                        <td>
                            <!-- 画面上部 -->
                            <table style="text-align: left; width: 100%;" id="" class="" cellpadding="1">
                                <tr>
                                    <td style="vertical-align: bottom;">
                                        <!-- 画面左上部 -->
                                        <table style="text-align: left; width: 100%;" id="TableIraiDate" class="mainTable"
                                            cellpadding="0">
                                            <tr>
                                                <td class="koumokuMei" style="width: 100px">
                                                    依頼日
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="TextIraiDate" CssClass="hissu date" MaxLength="10" runat="server"
                                                        Style="width: 70px" TabIndex="10" />
                                                    <input type="button" id="ButtonIraiDateYestarday" style="width: 37px" value="前日"
                                                        class="gyoumuSearchBtn" runat="server" tabindex="10" />
                                                    <input type="button" id="ButtonIraiDateToday" style="width: 37px" value="当日" class="gyoumuSearchBtn"
                                                        runat="server" tabindex="10" />
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <!-- 画面左上部[接頭辞:Tys] -->
                                        <table style="text-align: left; width: 100%;" id="TableTys" class="mainTable" cellpadding="1">
                                            <thead>
                                                <tr>
                                                    <th class="tableTitle" colspan="4" style="width: 100%; text-align: center;">
                                                        調査連絡先
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td class="koumokuMei">
                                                        実務担当会社
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="TextTysJitumuTantouGaisya" CssClass="readOnlyStyle" ReadOnly="true"
                                                            TabIndex="-1" runat="server" Style="width: 250px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="koumokuMei">
                                                        住所
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="TextTysJyuusyo" runat="server" Style="width: 250px" MaxLength="60"
                                                            TabIndex="10" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="koumokuMei">
                                                        TEL
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextTysTel" runat="server" Style="width: 125px" CssClass="codeNumber"
                                                            MaxLength="20" TabIndex="10" />
                                                    </td>
                                                    <td class="koumokuMei" style="width: 40px">
                                                        FAX
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextTysFax" runat="server" Style="width: 125px" CssClass="codeNumber"
                                                            MaxLength="20" TabIndex="10" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="koumokuMei">
                                                        担当者
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextTysTantousya" runat="server" Style="width: 120px" CssClass="readOnlyStyle"
                                                            ReadOnly="true" TabIndex="-1" />
                                                    </td>
                                                    <td class="koumokuMei" style="width: 40px">
                                                        携帯
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextTysKeitai" runat="server" Style="width: 100px" CssClass="readOnlyStyle number"
                                                            ReadOnly="true" TabIndex="-1" />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                    <td style="text-align: right">
                                        <table style="text-align: left; width: 230px; border-bottom-style: none;" id="TableKeiyakuNo"
                                            class="mainTable" cellpadding="1">
                                            <tr>
                                                <td class="koumokuMei" style="width: 60px; border-bottom-style: none;">
                                                    契約NO</td>
                                                <td style="border-bottom-style: none;">
                                                    <asp:TextBox ID="TextKeiyakuNo" Style="width: 150px; ime-mode: inactive;" runat="server"
                                                        CssClass="codeNumber" MaxLength="20" TabIndex="10" />
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:UpdatePanel ID="UpdatePanelKameiten" UpdateMode="conditional" runat="server"
                                            RenderMode="Inline" ChildrenAsTriggers="true">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="TextKITourokuBangou" />
                                                <asp:AsyncPostBackTrigger ControlID="ButtonKITourokuBangou" />
                                                <asp:AsyncPostBackTrigger ControlID="ButtonKIKameitenClear" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <input type="hidden" runat="server" id="HiddenKIKbnMae" /><%--区分・変更前--%>
                                                <input type="hidden" id="HiddenKITourokuBangouMae" runat="server" /><%--加盟店コード・変更前--%>
                                                <input type="hidden" runat="server" id="HiddenKameitenClearFlg" /><%--加盟店クリアフラグ--%>
                                                <input type="hidden" runat="server" id="HiddenHosyousyoHakkouJyoukyou" /><%--保証書発行状況--%>
                                                <input type="hidden" runat="server" id="HiddenHosyousyoHakkouJyoukyouSetteiDate" /><%--保証書発行状況設定日--%>
                                                <input type="hidden" runat="server" id="HiddenHosyouKikan" /><%--保証期間--%>
                                                <input type="hidden" runat="server" id="HiddenKjGaisyaSeikyuuUmu" /><%--工事会社請求有無--%>
                                                <input type="hidden" runat="server" id="HiddenKameitenTyuuiJikou" /><%--加盟店注意事項--%>
                                                <!-- 画面右上部・加盟店情報[接頭辞:KI] -->
                                                <table style="text-align: left; width: 100%;" id="TableKameiten" class="mainTable"
                                                    cellpadding="1">
                                                    <tr>
                                                        <td class="koumokuMei" style="width: 100px">
                                                            区分
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:DropDownList ID="SelectKIKubun" runat="server" CssClass="hissu" TabIndex="10">
                                                            </asp:DropDownList><span id="SpanKIKubun" runat="server"></span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="koumokuMei" style="width: 100px">
                                                            登録番号
                                                        </td>
                                                        <td>
                                                            <asp:HiddenField ID="saveCdOrderStop" runat="server" />
                                                            <asp:TextBox ID="TextKITourokuBangou" Style="width: 40px" CssClass="hissu codeNumber"
                                                                runat="server" MaxLength="5" TabIndex="10" />
                                                            <input type="button" id="ButtonKITourokuBangou" value="検索" class="gyoumuSearchBtn"
                                                                runat="server" tabindex="10" />
                                                            <input type="hidden" id="kameitenSearchType" runat="server" />
                                                            <input type="button" id="ButtonKIKameitenClear" value="クリア" class="" style="display: none;"
                                                                runat="server" onserverclick="ButtonKIKameitenClear_ServerClick" />
                                                            <span id="SpanKIJioSaki" runat="server" style="color: Red; font-weight: bold;"></span>
                                                        </td>
                                                        <td class="koumokuMei" style="width: 60px">
                                                            担当者
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextKITantousya" Style="width: 80px" runat="server" MaxLength="10"
                                                                TabIndex="10" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="koumokuMei">
                                                            社名
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="TextKISyamei" Style="width: 250px" CssClass="readOnlyStyle" runat="server"
                                                                ReadOnly="true" TabIndex="-1" />
                                                            <input type="button" id="ButtonKIKameitenTyuuijouhou" value="注意情報" class="btnKameitenTyuuijouhou"
                                                                runat="server" tabindex="10" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="koumokuMei">
                                                            住所
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="TextKIJyuusyo" Style="width: 330px" CssClass="readOnlyStyle" runat="server"
                                                                ReadOnly="true" TabIndex="-1" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="koumokuMei">
                                                            TEL
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextKITel" Style="width: 100px" CssClass="readOnlyStyle codeNumber"
                                                                runat="server" ReadOnly="true" TabIndex="-1" />
                                                        </td>
                                                        <td class="koumokuMei" style="width: 40px">
                                                            FAX
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextKIFax" Style="width: 100px" CssClass="readOnlyStyle codeNumber"
                                                                runat="server" ReadOnly="true" TabIndex="-1" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="koumokuMei">
                                                            担当者携帯
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="TextKITantousyaKeitai" Style="width: 100px" CssClass="readOnlyStyle codeNumber"
                                                                runat="server" ReadOnly="true" TabIndex="-1" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <!-- 画面メイン -->
                            <table style="text-align: left; width: 100%;" id="TableMain" class="mainTable" cellpadding="1">
                                <tr>
                                    <td class="koumokuMei" style="width: 100px">
                                        物件名称</td>
                                    <td>
                                        <asp:TextBox ID="TextBukkenMeisyou" Style="width: 27em" runat="server" CssClass="hissu"
                                            MaxLength="50" TabIndex="10" />様 邸
                                    </td>
                                    <td class="koumokuMei">
                                        施主名
                                    </td>
                                    <td>
                                        <input type="radio" name="RadioSesyumei" id="RadioSesyumei1" runat="server"
                                            tabindex="10" class="hissu"/>有<span id="SpanSesyumei1" runat="server"></span>
                                        <input type="radio" name="RadioSesyumei" id="RadioSesyumei0" runat="server"
                                            tabindex="10" class="hissu"/>無<span id="SpanSesyumei0" runat="server"></span>
                                    </td>
                                    <td class="koumokuMei">
                                        今回同時依頼棟数</td>
                                    <td>
                                        <asp:TextBox ID="TextDoujiIraiTousuu" Style="width: 40px" runat="server" CssClass="hissu number"
                                            MaxLength="4" TabIndex="10" />棟
                                    </td>
                                </tr>
                                <tr>
                                    <td class="koumokuMei">
                                        物件住所</td>
                                    <td colspan="5">
                                        １：<asp:TextBox ID="TextBukkenJyuusyo1" Style="width: 17em" runat="server" CssClass="hissu"
                                            MaxLength="32" TabIndex="10" />
                                        ２：<asp:TextBox ID="TextBukkenJyuusyo2" Style="width: 17em" runat="server" CssClass=""
                                            MaxLength="32" TabIndex="10" />
                                        <asp:UpdatePanel ID="UpdatePanelTyoufukuCheck" UpdateMode="conditional" runat="server"
                                            RenderMode="Inline">
                                            <Triggers>
                                            </Triggers>
                                            <ContentTemplate>
                                                <input type="button" id="ButtonTyoufukuCheck" value="重複物件なし" class="" runat="server"
                                                    tabindex="10" />
                                                <input id="ButtonExeTyoufukuCheck" style="display: none" type="button" value="重複チェック呼び出し"
                                                    runat="server" onserverclick="ButtonExeTyoufukuCheck_ServerClick" />
                                                <input type="hidden" id="HiddenTyoufukuKakuninFlg1" value="" runat="server" />
                                                <input type="hidden" id="HiddenTyoufukuKakuninFlg2" value="" runat="server" />
                                                <input type="hidden" id="HiddenTyoufukuKakuninTargetId" runat="server" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <br />
                                        ３：<asp:TextBox ID="TextBukkenJyuusyo3" Style="width: 30em" runat="server" CssClass=""
                                            MaxLength="54" TabIndex="10" />
                                        <input type="button" id="ButtonJyuusyoTenki" value="住所３を備考に転記" style="width: 9em"
                                            runat="server" tabindex="10" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="koumokuMei">
                                        調査希望日</td>
                                    <td colspan="5" style="text-align: center">
                                        <asp:TextBox ID="TextTyousaKibouDate" Style="width: 150px" runat="server" CssClass="hissu hizuke"
                                            MaxLength="10" TabIndex="10" />
                                        (<asp:TextBox ID="TextTyousaKibouJikan" Style="width: 260px" runat="server" CssClass=""
                                            MaxLength="26" TabIndex="10" />) &nbsp;&nbsp;
                                        <asp:CheckBox ID="CheckYoyakuZumi" runat="server" /><span id="SpanYoyakuZumi" style="font-size: 17px;
                                            font-weight: bold;" runat="server">予定書手動</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="koumokuMei">
                                        調査 立会者</td>
                                    <td colspan="5">
                                        <input type="radio" name="RadioTysTatiaisya" id="RadioTysTatiaisya1" runat="server"
                                            tabindex="10" />有<span id="SpanTysTatiaisya1" runat="server"></span> &nbsp;
                                        (
                                        <input type="checkbox" id="CheckTysTatiaisyaSesyuSama" runat="server" value="1" disabled="disabled"
                                            tabindex="10" />施主様<span id="SpanTysTatiaisyaSesyuSama" runat="server"></span>
                                        <input type="checkbox" id="CheckTysTatiaisyaTantousya" runat="server" value="2" disabled="disabled"
                                            tabindex="10" />担当者<span id="SpanTysTatiaisyaTantousya" runat="server"></span>
                                        <input type="checkbox" id="CheckTysTatiaisyaSonota" runat="server" value="4" disabled="disabled"
                                            tabindex="10" />その他<span id="SpanTysTatiaisyaSonota" runat="server"></span>
                                        ) &nbsp;
                                        <input type="radio" name="RadioTysTatiaisya" id="RadioTysTatiaisya0" runat="server"
                                            tabindex="10" />無<span id="SpanTysTatiaisya0" runat="server"></span>
                                        <input type="radio" name="RadioTysTatiaisya" id="RadioTysDummy" runat="server" style="display: none;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="koumokuMei" style="text-align: left; width: 136px;">
                                        建物概要
                                    </td>
                                    <td colspan="5" style="padding: 0px">
                                        <table style="text-align: left; width: 100%;" id="TableTatemonoGaiyou" class="innerTable"
                                            cellpadding="0" cellspacing="0">
                                            <tr class="firstRow">
                                                <td colspan="2" rowspan="2" class="firstCol">
                                                    <span class="koumokuMei">構造種別：</span>
                                                    <asp:TextBox ID="TextKouzouSyubetuCd" runat="server" CssClass="pullCd" MaxLength="1"
                                                        TabIndex="20" />
                                                    <asp:DropDownList ID="SelectKouzouSyubetu" runat="server" Style="width: 120px;" TabIndex="21">
                                                    </asp:DropDownList><span id="SpanKouzouSyubetu" runat="server"></span><br />
                                                    <div style="width: 100%; text-align: right;">
                                                        <asp:TextBox ID="TextKouzouSyubetuSonota" Style="width: 300px" runat="server" MaxLength="80"
                                                            TabIndex="22" /></div>
                                                </td>
                                                <td colspan="2">
                                                    <span class="koumokuMei">新築立替：</span>
                                                    <asp:TextBox ID="TextSintikuTatekaeCd" runat="server" CssClass="pullCd" MaxLength="1"
                                                        TabIndex="25" />
                                                    <asp:DropDownList ID="SelectSintikuTatekae" runat="server" Style="width: 120px;"
                                                        TabIndex="26">
                                                    </asp:DropDownList><span id="SpanSintikuTatekae" runat="server"></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <span class="koumokuMei">延べ床面積：</span>
                                                    <asp:TextBox ID="TextNobeyukaMenseki" Style="width: 100px" runat="server" CssClass="readOnlyStyle number"
                                                        ReadOnly="true" TabIndex="-1" />㎡
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="firstCol">
                                                    <span class="koumokuMei">階層：</span>
                                                    <asp:TextBox ID="TextKaisouCd" runat="server" CssClass="pullCd" MaxLength="2" TabIndex="23" />
                                                    <asp:DropDownList ID="SelectKaisou" runat="server" Style="width: 120px;" TabIndex="24">
                                                    </asp:DropDownList><span id="SpanKaisou" runat="server"></span>
                                                </td>
                                                <td colspan="2">
                                                    <span class="koumokuMei">建築面積：</span>
                                                    <asp:TextBox ID="TextKentikuMenseki" Style="width: 100px" runat="server" CssClass="readOnlyStyle number"
                                                        ReadOnly="true" TabIndex="-1" />
                                                ㎡
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="firstCol">
                                                    <span class="koumokuMei">建物用途：</span>
                                                    <asp:TextBox ID="TextTatemonoYoutoCd" runat="server" CssClass="pullCd" MaxLength="1"
                                                        TabIndex="30" />
                                                    <asp:DropDownList ID="SelectTatemonoYouto" runat="server" Style="width: 100px;" TabIndex="30">
                                                    </asp:DropDownList><span id="SpanTatemonoYouto" runat="server"></span>
                                                </td>
                                                <td colspan="2" style="text-align: right; border-left: none;">
                                                    用途 &nbsp; (<asp:TextBox ID="TextYouto" Style="width: 250px" runat="server" CssClass="readOnlyStyle"
                                                        ReadOnly="true" TabIndex="-1" />)
                                                    <br />
                                                    (<asp:TextBox ID="TextTatemonoYoutoSonota" Style="width: 250px" runat="server" CssClass="readOnlyStyle"
                                                        ReadOnly="true" TabIndex="-1" />)
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" class="firstCol">
                                                    設計許容支持力(<asp:TextBox ID="TextSekkeiKyoyouSijiryoku" Style="width: 60px" runat="server"
                                                        CssClass="number" MaxLength="6" TabIndex="30" />)kN/㎡
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="koumokuMei">
                                        依頼棟数について
                                    </td>
                                    <td colspan="5">
                                        <span>依頼予定棟数</span> &nbsp;
                                        <asp:TextBox ID="TextIraiYoteiTousuu" Style="width: 60px" runat="server" CssClass="number"
                                            MaxLength="4" TabIndex="30" />棟
                                    </td>
                                </tr>
                                <tr>
                                    <td class="koumokuMei">
                                        地業および<br />
                                        予定基礎状況<br />
                                        <br />
                                        <a href="JavaScript:Oritatami();" id="AOritatami" style="text-decoration: none;"
                                            tabindex="30">+</a>
                                    </td>
                                    <td colspan="5">
                                        根切り深さ (<asp:TextBox ID="TextNegiriHukasa" Style="width: 90px" runat="server" CssClass="number"
                                            MaxLength="13" TabIndex="30" />mm) &nbsp; 予定盛土厚さ (<asp:TextBox ID="TextYoteiMoritutiAtusa"
                                                Style="width: 90px" runat="server" CssClass="number" MaxLength="13" TabIndex="30" />mm)
                                        <br />
                                        &nbsp;
                                        <asp:TextBox ID="TextYoteiKisoCd" runat="server" CssClass="pullCd" MaxLength="1"
                                            TabIndex="30" />
                                        <asp:DropDownList ID="SelectYoteiKiso" runat="server" Style="width: 142px;" TabIndex="30">
                                        </asp:DropDownList><span id="SpanYoteiKiso" runat="server"></span> &nbsp; ﾍﾞｰｽW(<asp:TextBox
                                            ID="TextBaseW" Style="width: 100px" runat="server" CssClass="readOnlyStyle number"
                                            ReadOnly="true" TabIndex="-1" />mm) &nbsp;
                                        <asp:TextBox ID="TextYoteiKisoSonota" Style="width: 300px;" runat="server" MaxLength="80"
                                            TabIndex="30" />
                                        <br />
                                        基礎立ち上がり高さ (ＧＬ＋<asp:TextBox ID="TextKisoTatiagariTakasa" Style="width: 50px" runat="server"
                                            CssClass="readOnlyStyle number" ReadOnly="true" TabIndex="-1" />mm)
                                    </td>
                                </tr>
                                <!-- 搬入条件[接頭辞:HJ] -->
                                <tr id="TrHJ" style="display: none" runat="server">
                                    <td class="koumokuMei">
                                        搬入条件
                                    </td>
                                    <td colspan="5">
                                        敷地に面する道路幅(<asp:TextBox ID="TextHJDouroHaba" Style="width: 30px" runat="server" CssClass="readOnlyStyle"
                                            ReadOnly="true" TabIndex="-1" />m) (<input type="radio" name="RadioHJDouroHaba" value="2"
                                                id="RadioHJDouroHaba0" disabled="disabled" />2t車
                                        <input type="radio" name="RadioHJDouroHaba" value="4" id="RadioHJDouroHaba1" disabled="disabled" />4t車)以上の通行不可
                                        &nbsp; 道路規制 (<input type="radio" name="RadioHJDouroKisei" value="0" id="RadioHJDouroKisei0"
                                            disabled="disabled" />無
                                        <input type="radio" name="RadioHJDouroKisei" value="1" id="RadioHJDouroKisei1" disabled="disabled" />有)
                                        <br />
                                        スロープ
                                        <input type="radio" name="RadioHJSlope" value="0" id="RadioHJSlope0" disabled="disabled" />無
                                        <input type="radio" name="RadioHJSlope" value="1" id="RadioHJSlope1" disabled="disabled" />有
                                        (間口
                                        <asp:TextBox ID="TextMaguti" Style="width: 50px" runat="server" CssClass="readOnlyStyle"
                                            ReadOnly="true" TabIndex="-1" />m) &nbsp; 階段
                                        <input type="radio" name="RadioHJKaidan" value="0" id="RadioHJKaidan0" disabled="disabled" />無
                                        <input type="radio" name="RadioHJKaidan" value="1" id="RadioHJKaidan1" disabled="disabled" />有
                                        &nbsp;
                                        <input type="checkbox" id="CheckHJSonota" disabled="disabled" />その他(<asp:TextBox
                                            ID="TextHJSonota" Style="width: 120px" runat="server" CssClass="readOnlyStyle"
                                            ReadOnly="true" TabIndex="-1" />)
                                    </td>
                                </tr>
                                <!-- 高さ障害[接頭辞:TS] -->
                                <tr id="TrTS" style="display: none" runat="server">
                                    <td class="koumokuMei">
                                        高さ障害
                                    </td>
                                    <td colspan="5">
                                        <input type="radio" name="RadioTSTakasaSyougai" value="0" id="RadioTSTakasaSyougai0"
                                            disabled="disabled" />無
                                        <input type="radio" name="RadioTSTakasaSyougai" value="1" id="RadioTSTakasaSyougai1"
                                            disabled="disabled" />有 (
                                        <input type="checkbox" id="CheckTSDensen" disabled="disabled" />電線
                                        <input type="checkbox" id="CheckTSChannel" disabled="disabled" />トンネル
                                        <input type="checkbox" id="ChecTSkSikitinaiKouteisa" disabled="disabled" />敷地内高低差
                                        (<asp:TextBox ID="TextTSSikitinaiKouteisa" Style="width: 30px" runat="server" CssClass="readOnlyStyle"
                                            ReadOnly="true" TabIndex="-1" />m)
                                        <input type="checkbox" id="CheckTSSonota" value="1" disabled="disabled" />
                                        その他(<asp:TextBox ID="TextTSSonota" Style="width: 120px" runat="server" CssClass="readOnlyStyle"
                                            ReadOnly="true" TabIndex="-1" />m))
                                    </td>
                                </tr>
                                <!-- 敷地の前歴[接頭辞:SZ] -->
                                <tr id="TrSZ" style="display: none" runat="server">
                                    <td class="koumokuMei">
                                        敷地の前歴
                                    </td>
                                    <td colspan="5">
                                        <input type="checkbox" id="CheckSZTahata" disabled="disabled" />田畑
                                        <input type="checkbox" id="CheckSZTakuti" disabled="disabled" />宅地
                                        <input type="checkbox" id="CheckSZSyokujuBatake" disabled="disabled" />植樹畑
                                        <input type="checkbox" id="CheckSZZoukiBayasi" disabled="disabled" />雑木林
                                        <input type="checkbox" id="CheckSZKantakuti" disabled="disabled" />干拓地
                                        <input type="checkbox" id="CheckSZKoujouAto" disabled="disabled" />工場跡
                                        <input type="checkbox" id="CheckSZSonota" disabled="disabled" />その他 (<asp:TextBox
                                            ID="TextSZSonota" Style="width: 120px" runat="server" CssClass="readOnlyStyle"
                                            ReadOnly="true" TabIndex="-1" />)
                                    </td>
                                </tr>
                                <!-- 宅地造成に関して[接頭辞:TZ] -->
                                <tr id="TrTZ" style="display: none" runat="server">
                                    <td class="koumokuMei">
                                        宅地造成に<br />
                                        関して
                                    </td>
                                    <td colspan="5">
                                        <input type="checkbox" id="CheckTZKankoutyou" disabled="disabled" />官公庁造成
                                        <input type="checkbox" id="CheckTZMinkan" disabled="disabled" />民間造成 造成後(
                                        <asp:TextBox ID="TextTZZouseiAto" Style="width: 30px" runat="server" CssClass="readOnlyStyle"
                                            ReadOnly="true" TabIndex="-1" />)ヶ月
                                        <input type="checkbox" id="CheckTZKirituti" disabled="disabled" />切土
                                        <input type="checkbox" id="CheckTZMorituti" disabled="disabled" />盛土 (
                                        <asp:TextBox ID="TextTZMorituti" Style="width: 50px" runat="server" CssClass="readOnlyStyle"
                                            ReadOnly="true" TabIndex="-1" />)m
                                    </td>
                                </tr>
                                <!-- 敷地の状況[接頭辞:SJ] -->
                                <tr id="TrSJ" style="display: none" runat="server">
                                    <td class="koumokuMei">
                                        敷地の状況
                                    </td>
                                    <td colspan="5">
                                        既存建物(
                                        <input type="radio" name="RadioSJKizonTatemono" value="0" id="RadioSJKizonTatemono0"
                                            disabled="disabled" />無
                                        <input type="radio" name="RadioSJKizonTatemono" value="1" id="RadioSJKizonTatemono1"
                                            disabled="disabled" />有 ) &nbsp; 飼犬(
                                        <input type="radio" name="RadioSJKaiinu" value="0" id="RadioSJKaiinu0" disabled="disabled" />無
                                        <input type="radio" name="RadioSJKaiinu" value="1" id="RadioSJKaiinu1" disabled="disabled" />有
                                        ) &nbsp; 井戸(
                                        <input type="radio" name="RadioSJIdo" value="0" id="RadioSJIdo0" disabled="disabled" />無
                                        <input type="radio" name="RadioSJIdo" value="1" id="RadioSJIdo1" disabled="disabled" />有
                                        )
                                        <br />
                                        擁壁(現況：
                                        <input type="radio" name="RadioSJYouhekiG" value="0" id="RadioSJYouhekiG0" disabled="disabled" />無
                                        <input type="radio" name="RadioSJYouhekiG" value="1" id="RadioSJYouhekiG1" disabled="disabled" />有
                                        &nbsp; 予定：
                                        <input type="radio" name="RadioSJYouhekiY" value="0" id="RadioSJYouhekiY0" disabled="disabled" />無
                                        <input type="radio" name="RadioSJYouhekiY" value="1" id="RadioSJYouhekiY1" disabled="disabled" />有
                                        ) &nbsp; 浄化槽(現況：
                                        <input type="radio" name="RadioSJJoukasouG" value="0" id="RadioSJJoukasouG0" disabled="disabled" />無
                                        <input type="radio" name="RadioSJJoukasouG" value="1" id="RadioSJJoukasouG1" disabled="disabled" />有
                                        &nbsp; 予定：
                                        <input type="radio" name="RadioSJJoukasouY" value="0" id="RadioSJJoukasouY0" disabled="disabled" />無
                                        <input type="radio" name="RadioSJJoukasouY" value="1" id="RadioSJJoukasouY1" disabled="disabled" />有
                                        )
                                        <br />
                                        <input type="checkbox" id="CheckSJTahata" disabled="disabled" />田畑
                                        <input type="checkbox" id="CheckSJTyuusyajou" disabled="disabled" />駐車場
                                        <input type="checkbox" id="CheckSJSonota" disabled="disabled" />その他 (<asp:TextBox
                                            ID="TextSJSonota" Style="width: 120px" runat="server" CssClass="readOnlyStyle"
                                            ReadOnly="true" TabIndex="-1" />)
                                    </td>
                                </tr>
                                <tr>
                                    <td class="koumokuMei">
                                        地下車庫計画
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="TextTikaSyakoKeikakuCd" runat="server" CssClass="pullCd" MaxLength="1"
                                            TabIndex="30" />
                                        <asp:DropDownList ID="SelectTikaSyakoKeikaku" runat="server" Style="width: 160px"
                                            TabIndex="30">
                                        </asp:DropDownList><span id="SpanTikaSyakoKeikaku" runat="server"></span>
                                    </td>
                                    <td style="text-align: center">
                                        地域特性</td>
                                    <td colspan="2">
                                        積雪量 (最大<asp:TextBox ID="TextSekisetuRyou" Style="width: 30px" runat="server" CssClass="readOnlyStyle"
                                            ReadOnly="true" TabIndex="-1" />cm)
                                    </td>
                                </tr>
                                <!-- 添付資料[接頭辞:TP] -->
                                <tr>
                                    <td class="koumokuMei">
                                        添付資料
                                    </td>
                                    <td colspan="5">
                                        <span id="SpanHissu" class="koumokuMei">必須：</span>
                                        <asp:CheckBox ID="CheckTPAnnaiZu" runat="server" TabIndex="30" />案内図（区割図・測量図など）<span
                                            id="SpanTPAnnaiZu" runat="server"></span>
                                        <asp:CheckBox ID="CheckTPHaitiZu" runat="server" TabIndex="30" />配置図<span id="SpanTPHaitiZu"
                                            runat="server"></span>
                                        <asp:CheckBox ID="CheckTPKakukaiHeimenZu" runat="server" TabIndex="30" />各階平面図 &nbsp;<span
                                            id="SpanTPKakukaiHeimenZu" runat="server"></span> <span id="SpanNinni" class="koumokuMei">
                                                任意：</span>
                                        <asp:CheckBox ID="CheckTPKsFuseZu" runat="server" TabIndex="30" />基礎伏図<span id="SpanTPKsFuseZu"
                                            runat="server"></span>
                                        <asp:CheckBox ID="CheckTPKsDanmenZu" runat="server" TabIndex="30" />基礎断面図<span id="SpanTPKsDanmenZu"
                                            runat="server"></span>
                                        <asp:CheckBox ID="CheckTPZouseiKeikakuZu" runat="server" TabIndex="30" />立面図<span
                                            id="SpanTPZouseiKeikakuZu" runat="server"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="koumokuMei">
                                        基礎着工予定日
                                    </td>
                                    <td style="text-align: center" colspan="5">
                                        <span class="hizuke">
                                            <asp:TextBox ID="TextKsTyakkouYoteiDateFrom" Style="width: 150px" runat="server"
                                                CssClass="hizuke" MaxLength="10" TabIndex="30" />
                                            ～
                                            <asp:TextBox ID="TextKsTyakkouYoteiDateTo" Style="width: 150px" runat="server" CssClass="hizuke"
                                                MaxLength="10" TabIndex="30" />
                                            頃 </span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <!-- 画面下部・その他情報[接頭辞:SI] -->
                    <tr>
                        <td colspan="4" style="height: 2px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:UpdatePanel ID="UpdatePanelSonota" UpdateMode="conditional" runat="server" RenderMode="inline"
                                ChildrenAsTriggers="true">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ButtonKITourokuBangou" />
                                </Triggers>
                                <ContentTemplate>
                                    <input type="hidden" id="HiddenSITysGaisyaMae" runat="server" /><%--調査会社コード・変更前--%>
                                    <table style="text-align: left; width: 100%;" id="Table6" class="mainTable" cellpadding="1">
                                        <thead>
                                            <tr>
                                                <th class="tableTitle" colspan="9" style="width: 100%">
                                                    <a id="ASI">その他情報</a>
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody id="TbodySI">
                                            <tr>
                                                <td colspan="9" class="tableSpacer">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="koumokuMei" style="width: 80px" rowspan="2">
                                                    備考</td>
                                                <td rowspan="2" colspan="4">
                                                    <textarea id="TextSIBikou" cols="80" rows="3" style="font-family: Sans-Serif; ime-mode: active;"
                                                        onfocus="this.select();" runat="server" tabindex="30"></textarea><textarea id="TextSIBikou2"
                                                            cols="60" rows="4" style="display: none; font-family: Sans-Serif; ime-mode: active;"
                                                            onfocus="this.select();" runat="server" tabindex="30"></textarea></td>
                                                <td class="koumokuMei">
                                                    &nbsp;</td>
                                                <td colspan="3">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="koumokuMei">
                                                    物件名寄コード
                                                </td>
                                                <td colspan="3">
                                                    <input type="text" id="TextBukkenNayoseCd" style="width: 90px; ime-mode: disabled;"
                                                        runat="server" class="codeNumber" maxlength="11" tabindex="30"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="koumokuMei">
                                                    経由</td>
                                                <td colspan="1">
                                                    <asp:DropDownList ID="SelectSIKeiyu" runat="server" TabIndex="30">
                                                    </asp:DropDownList><span id="SpanSIKeiyu" runat="server"></span>
                                                </td>
                                                <td class="koumokuMei">
                                                    建物検査</td>
                                                <td colspan="2">
                                                    <asp:DropDownList ID="SelectSITatemonoKensa" runat="server" TabIndex="30">
                                                        <asp:ListItem Value="0" Text="0:無し" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="1:有り"></asp:ListItem>
                                                    </asp:DropDownList><span id="SpanSITatemonoKensa" runat="server"></span>
                                                </td>
                                                <td class="koumokuMei">
                                                    戸数</td>
                                                <td colspan="4">
                                                    <asp:TextBox ID="TextSIKosuu" Style="width: 40px" runat="server" CssClass="number"
                                                        MaxLength="4" TabIndex="30" />戸</td>
                                            </tr>
                                            <tr style="display: none;">
                                                <td class="koumokuMei">
                                                    &nbsp;</td>
                                                <td colspan="3">
                                                    <input type="radio" name="RadioSISyouhinKbn" value="1" id="RadioSISyouhinKbn1" runat="server"
                                                        style="display: none" disabled="disabled" tabindex="30" /><span id="Span1" runat="server"></span>
                                                    <span id="SpanSISyouhinKbn1" style="display: none;" runat="server">60年保証</span>
                                                    <input type="radio" name="RadioSISyouhinKbn" value="2" id="RadioSISyouhinKbn2" runat="server"
                                                        tabindex="30" style="display: none" disabled="disabled" /><span id="Span2" runat="server"></span>
                                                    <span id="SpanSISyouhinKbn2" runat="server">土地販売</span>
                                                    <input type="radio" name="RadioSISyouhinKbn" value="3" id="RadioSISyouhinKbn3" runat="server"
                                                        tabindex="30" style="display: none" disabled="disabled" /><span id="Span3" runat="server"></span>
                                                    <span id="SpanSISyouhinKbn3" runat="server">リフォーム</span>
                                                    <input type="radio" name="RadioSISyouhinKbn" value="9" id="RadioSISyouhinKbn9" runat="server"
                                                        style="display: none" disabled="disabled" tabindex="30" /><span id="Span9" runat="server"></span>
                                                    <span id="SpanSISyouhinKbn9" style="display: none;" runat="server">&nbsp;</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="koumokuMei">
                                                    調査会社</td>
                                                <td colspan="8">
                                                    <span id="SpanSITysGaisya">
                                                        <asp:TextBox ID="TextSITysGaisyaCd" Style="width: 60px" runat="server" CssClass="codeNumber"
                                                            MaxLength="7" TabIndex="30" />
                                                        <input type="button" id="ButtonSITysGaisya" value="検索" class="gyoumuSearchBtn" runat="server"
                                                            onmouseup="JStyousakaisyaSearchType=9;" onkeydown="if(event.keyCode==13||event.keyCode==32)JStyousakaisyaSearchType=9;"
                                                            onserverclick="ButtonSITysGaisya_ServerClick" tabindex="30" />
                                                        <input type="hidden" id="tyousakaisyaSearchType" runat="server" value="" />
                                                        <asp:TextBox ID="TextSITysGaisyaMei" Style="width: 25em;" runat="server" CssClass="readOnlyStyle"
                                                            ReadOnly="true" TabIndex="-1" />
                                                    </span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="koumokuMei">
                                                    商品1</td>
                                                <td colspan="4">
                                                    <asp:UpdatePanel ID="UpdatePanelSyouhin1" UpdateMode="Conditional" runat="server"
                                                        RenderMode="Inline">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnSetTysGaiyou" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="choSyouhin1" runat="server" Width="310px" CssClass="hissu"
                                                                TabIndex="30">
                                                            </asp:DropDownList><span id="SpanSISyouhin1" runat="server"></span><input type="hidden"
                                                                id="HiddenSyouhin1Pre" runat="server" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td class="koumokuMei">
                                                    保証商品</td>
                                                <td colspan="3">
                                                    <asp:UpdatePanel ID="UpdatePanelHosyouSyouhinUmu" UpdateMode="Conditional" runat="server"
                                                        RenderMode="Inline">
                                                        <ContentTemplate>
                                                            <input id="TextHosyouSyouhinUmu" runat="server" class="readOnlyStyle" style="width: 30px"
                                                                tabindex="-1" readonly="readOnly" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td class="koumokuMei" colspan="1" style="display: none;">
                                                    保証有無</td>
                                                <td colspan="2" style="display: none;">
                                                    <asp:DropDownList ID="SelectSIHosyouUmu" runat="server" TabIndex="30">
                                                        <asp:ListItem Value="" Text=""></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="1:有り"></asp:ListItem>
                                                    </asp:DropDownList><span id="SpanSIHosyouUmu" runat="server"></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="koumokuMei">
                                                    調査方法</td>
                                                <td colspan="1">
                                                    <asp:UpdatePanel ID="UpdateTysHouhou" UpdateMode="Conditional" runat="server" RenderMode="Inline">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnSetTysGaiyou" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="TextSITysHouhouCd" runat="server" CssClass="pullCd" MaxLength="2"
                                                                TabIndex="30" />
                                                            <asp:DropDownList ID="SelectSITysHouhou" runat="server" Style="width: 210px;" TabIndex="30">
                                                            </asp:DropDownList>
                                                            <span id="SpanSITysHouhou" runat="server"></span>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td class="koumokuMei" colspan="1">
                                                    調査概要</td>
                                                <td colspan="6">
                                                    <asp:UpdatePanel ID="UpdatePanelTysGaiyou" UpdateMode="Conditional" runat="server"
                                                        RenderMode="Inline">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnSetTysGaiyou" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="SelectSITysGaiyou" runat="server" Style="width: 266px;" TabIndex="30">
                                                            </asp:DropDownList><span id="SpanSITysGaiyou" runat="server"></span>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <!-- 画面下部・物件履歴[接頭辞:BR] -->
                    <tr>
                        <td colspan="4" style="height: 2px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table style="text-align: left; width: 100%;" id="tblBRInfo" class="mainTable" cellpadding="1">
                                <thead>
                                    <tr>
                                        <th class="tableTitle" colspan="9" style="width: 100%">
                                            <a id="AncBRInfo" runat="server" tabindex="40">物件履歴情報</a>
                                            <input type="hidden" id="HiddenBRInfoStyle" runat="server" value="none" />
                                        </th>
                                    </tr>
                                </thead>
                                <tbody id="TBodyBRInfo" runat="server">
                                    <!-- 物件履歴項目1-->
                                    <tr>
                                        <td class="koumokuMei">
                                            種別</td>
                                        <td colspan="3" rowspan="1">
                                            <asp:DropDownList runat="server" ID="SelectBRSyubetu1" Width="300px" CssClass="" tabindex="40">
                                            </asp:DropDownList>
                                        </td>
                                        <td rowspan="3">
                                             <textarea id="TextAreaBRNaiyou1" runat="server" cols="70" onfocus="this.select();" rows="5" style="ime-mode: active; font-family: Sans-Serif" tabindex="45"></textarea>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="koumokuMei">
                                            分類</td>
                                        <td colspan="3" rowspan="1">
                                            <select id="SelectBRBunrui1" style="width: 300px" onchange="UpdHiddenBunrui(this,1)" class="" tabindex="40" >
                                            </select>
                                            <input type="hidden" id="HiddenBRBunrui1" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="koumokuMei">
                                            汎用コード</td>
                                        <td rowspan="1">
                                            <asp:TextBox ID="TextBRHanyouCd1" MaxLength="20" runat="server" Style="width: 200px" tabindex="40" CssClass="codeNumber"/>
                                        </td>
                                        <td class="koumokuMei">
                                            日付</td>
                                        <td rowspan="1">
                                            <asp:TextBox ID="TextBRHizuke1" MaxLength="10" runat="server" Style="width: 70px" tabindex="40"  CssClass="date"/>
                                        </td>
                                    </tr>
                                    <!-- 物件履歴項目2-->
                                    <tr style="background-color: LightCyan;">
                                        <td class="koumokuMei">
                                            種別</td>
                                        <td colspan="3" rowspan="1">
                                            <asp:DropDownList runat="server" ID="SelectBRSyubetu2" Width="300px" CssClass="" tabindex="50">
                                            </asp:DropDownList>
                                        </td>
                                        <td rowspan="3">
                                             <textarea id="TextAreaBRNaiyou2" runat="server" cols="70" onfocus="this.select();" rows="5" style="ime-mode: active; font-family: Sans-Serif" tabindex="55"></textarea>
                                        </td>
                                    </tr>
                                    <tr style="background-color: LightCyan;">
                                        <td class="koumokuMei">
                                            分類</td>
                                        <td colspan="3" rowspan="1">
                                            <select id="SelectBRBunrui2" style="width: 300px" onchange="UpdHiddenBunrui(this,2)" class="" tabindex="50" >
                                            </select>
                                            <input type="hidden" id="HiddenBRBunrui2" runat="server" />
                                        </td>
                                    </tr>
                                    <tr style="background-color: LightCyan;">
                                        <td class="koumokuMei">
                                            汎用コード</td>
                                        <td rowspan="1">
                                            <asp:TextBox ID="TextBRHanyouCd2" MaxLength="20" runat="server" Style="width: 200px" tabindex="50" CssClass="codeNumber"/>
                                        </td>
                                        <td class="koumokuMei">
                                            日付</td>
                                        <td rowspan="1">
                                            <asp:TextBox ID="TextBRHizuke2" MaxLength="10" runat="server" Style="width: 70px" tabindex="50"  CssClass="date"/>
                                        </td>
                                    </tr>
                                    <!-- 物件履歴項目3-->
                                    <tr>
                                        <td class="koumokuMei">
                                            種別</td>
                                        <td colspan="3" rowspan="1">
                                            <asp:DropDownList runat="server" ID="SelectBRSyubetu3" Width="300px" CssClass="" tabindex="60">
                                            </asp:DropDownList>
                                        </td>
                                        <td rowspan="3">
                                             <textarea id="TextAreaBRNaiyou3" runat="server" cols="70" onfocus="this.select();" rows="5" style="ime-mode: active; font-family: Sans-Serif" tabindex="65"></textarea>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="koumokuMei">
                                            分類</td>
                                        <td colspan="3" rowspan="1">
                                            <select id="SelectBRBunrui3" style="width: 300px" onchange="UpdHiddenBunrui(this,3)" class="" tabindex="60" >
                                            </select>
                                            <input type="hidden" id="HiddenBRBunrui3" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="koumokuMei">
                                            汎用コード</td>
                                        <td rowspan="1">
                                            <asp:TextBox ID="TextBRHanyouCd3" MaxLength="20" runat="server" Style="width: 200px" tabindex="60" CssClass="codeNumber"/>
                                        </td>
                                        <td class="koumokuMei">
                                            日付</td>
                                        <td rowspan="1">
                                            <asp:TextBox ID="TextBRHizuke3" MaxLength="10" runat="server" Style="width: 70px" tabindex="60"  CssClass="date"/>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>                               
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="1"
            class="titleTable">
            <tbody>
                <tr>
                    <th style="text-align: right; padding: 10px;">
                        <input type="button" id="ButtonTouroku2" value="登録 実行" style="font-weight: bold;
                            font-size: 18px; width: 155px; color: black; height: 30px; background-color: fuchsia"
                            runat="server" tabindex="30" />
                    </th>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>

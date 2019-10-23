<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="IkkatuHenkouTysSyouhin.aspx.vb" Inherits="Itis.Earth.WebUI.IkkatuHenkouTysSyouhin"
    Title="EARTH 一括変更【調査商品情報】" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script type="text/javascript">
        // 画面起動時にウィンドウサイズをディスプレイに合わせる
        window.moveTo(0, 0);
        window.resizeTo(window.screen.availWidth, window.screen.availHeight);
        var gstrClientId = "<%= ME_CLIENT_ID %>"
        var gVarSettouJi_1 = gstrClientId + "<%= TYS_SYOUHIN1_CTRL_NAME %>"; // コントロール接頭辞
        var gVarSettouJi_2 = gstrClientId + "<%= TYS_SYOUHIN2_CTRL_NAME %>"; // コントロール接頭辞
        var gintCallPopUp = 0;   // 調査会社呼出し用フラグ
       
        /****************************************
         * onload時の追加処理
         ****************************************/
        function funcAfterOnload() {
        
            // 商品2テーブル 各種レイアウト設定
            settingItem2Table();

        }
        
        // 商品2テーブル 各種レイアウト設定
        function settingItem2Table(){
            var item2Data = objEBI("<%=tblMeisaiSyouhin2.clientID %>");
            setItem2Color(item2Data);
        }
        
                
        /**
         * 顧客番号ごとに背景色を変更（実質は4行ごとに変更）
         * 
         * @param objGridTBody:対象とするtableのtbodyエレメント
         * @return
         */
        function setItem2Color(objGridTBody) {
            var countTr = 0;
            var arrTr = objGridTBody.rows;
            // 明細行の数だけループ
            for (var i = 0; i < arrTr.length; i = i + 4) {
                var objTr = arrTr[i];
                var k = i;
                var blnChg = false;
                // ４行ずつループし、全て非表示かどうか判断
                for (var j = 0; j < 4; j++){
                    k = i + j;
                    objWkTr = arrTr[k];
                    if (objWkTr.style.display != "none"){
                        blnChg = true;
                    }
                }
                // 1行でも表示されている行があったら、背景色を変更
                if (blnChg == true){
                    for (var j = 0; j < 4; j++){
                        k = i + j;
                        objWkTr = arrTr[k];
                        if (countTr % 2 == 0) {
                            objWkTr.className  = "odd";
                        } else {
                            objWkTr.className  = "even";
                        }
                    }
                    // 背景色を変更した時のみカウントアップ
                    countTr++;
                }
            }
            return true;
        } 
        
        // 調査会社検索処理を呼び出す
        function callTyousakaisyaSearch(obj){
            objEBI("<%= tyousakaisyaSearchType.clientID %>").value = "";
            if(obj.value == ""){
                objEBI("<%= tyousakaisyaSearchType.clientID %>").value = "1";
                objEBI("<%= ButtonTysGaisya.clientID %>").click();
            }
        }
        
        // 隠しドロップダウンリストから調査会社名称を取得し、セットする
        function getTysKaisyaName(objBtn, code, nameObj, oldObj){
            objEBI("<%= SelectTysKaisya.clientID %>").value = code;
            var index = objEBI("<%= SelectTysKaisya.clientID %>").selectedIndex;
            if(index == -1){
                gintCallPopUp = 1;
                objBtn.onclick();
            }else{
                //表示されているテキスト
                var strText = objEBI("<%= SelectTysKaisya.clientID %>").options[index].text;
                nameObj.value = strText;
                oldObj.value = code;
            }
        }                
        
        // 画面上部・コピーボタン押下処理(共通)
        function setCopyValue(objId){
            var varTarget1 = ''; // 対象コントロール
            var varTarget2 = ''; // 対象コントロール
            var varTarget3 = ''; // 対象コントロール
            var varTarget4 = ''; // 対象コントロール
            var varChkBoxId = '_CheckAutoCalc'; // チェックボックスID
            var setVal1 = ''; // セットする値
            var setVal2 = ''; // セットする値
            var setColor = '';
            var intCnt;
            var strCnt;
            
            if(objId.indexOf("ButtonCopyTysHouhou") != -1){ // 調査方法
                varTarget1 = "_TextTysHouhouCode";
                varTarget2 = "_SelectTysHouhou";
                setVal1 = objEBI("<%= TextTysHouhouCode.clientID %>").value;
                setVal2 = objEBI("<%= SelectTysHouhou.clientID %>").value;
                setColor = objEBI("<%= TextTysHouhouCode.clientID %>").style.color
            }else if(objId.indexOf("ButtonCopyTysGaisya") != -1){ // 調査会社
                varTarget1 = "_TextTyousakaisyaCode";
                varTarget2 = "_TextTyousakaisyaName";
                varTarget3 = "_HiddenTyousaKaishaCdOld"
                
                setVal1 = objEBI("<%= TextTysGaisyaCode.clientID %>").value;
                setVal2 = objEBI("<%= TextTysGaisyaName.clientID %>").value;
                setVal3 = objEBI("<%= HiddenTyousaKaishaCdOld.clientID %>").value;
                setColor = objEBI("<%= TextTysGaisyaCode.clientID %>").style.color
                if (setVal1 != setVal3 || (setVal1 != "" && setVal2 == "")){
                    alert('<%= Messages.MSG030E.replace("@PARAM1","調査会社コード") %>');
                    objEBI("<%= ButtonTysGaisya.clientID %>").focus();
                    return false;
                }
            }else if(objId.indexOf("ButtonCopySyouhin1") != -1){ //商品1
                varTarget1 = "_SelectSyouhin1";
                varTarget2 = "_SelectSyouhin1"; // ダミー
                varTarget3 = "_ucTokubetuTaiouToolTipCtrl_hiddenDisplay"; //特別対応Displayコード
                varTarget4 = "_TextKokyakuBangou"; //顧客番号
                setVal1 = objEBI("<%= SelectSyouhin1.clientID %>").value;
                setVal2 = objEBI("<%= SelectSyouhin1.clientID %>").value; // ダミー
            }else{
                return false;
            }
            
            //コピー内容(調査方法コード)が空白の場合            
            if(setVal1 == ''){
                alert('<%= String.Format(Messages.MSG126E, "コピー内容") %>');
                return false;      
            }            
            //コピー内容(調査方法名称)が空白の場合            
            if(setVal2 == ''){
                alert('<%= String.Format(Messages.MSG126E, "コピー内容") %>');
                return false;      
            }            
            
            var VarCnt = objEBI("<%= HiddenLineCnt.clientID %>").value; // 明細行数
            var intCopyFlg = 0;
            var intDisalbedFlg = 0;
            for(intCnt = 1; intCnt <= VarCnt; intCnt++){
                strCnt = "" + intCnt;
                var varTargetUriage = objEBI(gVarSettouJi_1 + strCnt + "_TextUriageSyori").value;
                var varTargetTorikeshi = objEBI(gVarSettouJi_1 + strCnt + "_HiddenTorikeshi").value;
                if (varTargetUriage == "" && varTargetTorikeshi != "1"){
                    var varTmpId1 = gVarSettouJi_1 + strCnt + varTarget1;
                    var varTmpId2 = gVarSettouJi_1 + strCnt + varTarget2;
                    var varTmpId3 = '';
                    var varTmpId4 = gVarSettouJi_1 + strCnt + varChkBoxId;
                    var varTmpId5 = '';
                    
                    if(objId.indexOf("ButtonCopyTysHouhou") != -1){ // 調査方法
                        objEBI(varTmpId1).value = setVal1;
                        objEBI(varTmpId1).style.color = setColor
                        objEBI(varTmpId2).value = setVal2;
                        objEBI(varTmpId2).style.color = setColor
                        objEBI(varTmpId4).checked = true;
                        intCopyFlg = intCopyFlg + 1;
                    }else if(objId.indexOf("ButtonCopyTysGaisya") != -1){ // 調査会社
                        if((objEBI(varTmpId1).readOnly != true) && (objEBI(objId).style.display != "none")){
                            objEBI(varTmpId1).value = setVal1;
                            objEBI(varTmpId1).style.color = setColor
                            objEBI(varTmpId2).value = setVal2;
                            objEBI(varTmpId2).style.color = setColor
                            
                            varTmpId3 = gVarSettouJi_1 + strCnt + varTarget3;
                            objEBI(varTmpId3).value = setVal3;
                            objEBI(varTmpId4).checked = true;
                            intCopyFlg = intCopyFlg + 1;
                        }
                    }else if(objId.indexOf("ButtonCopySyouhin1") != -1){ //商品1
 
                        varTmpId3 = gVarSettouJi_1 + strCnt + varTarget3;
                        varTmpId5 = gVarSettouJi_1 + strCnt + varTarget4;
                        var strId = objEBI(varTmpId3).id //特別対応Displayコード
                        var strVal = objEBI(varTmpId5).value //顧客番号

                        //特別対応価格対応(商品1を変更するか)
                        if(ChkTokubetuTaiou(strId, strVal) == true){                    
                            objEBI(varTmpId1).value = setVal1;
                            objEBI(varTmpId4).checked = true;
                        }
                        //キャンセルした場合もカウントアップする
                        intCopyFlg = intCopyFlg + 1;
                    }
                }else{
                    intDisalbedFlg = intDisalbedFlg + 1;    
                }
            }
            if(objId.indexOf("ButtonCopyTysHouhou") != -1){ // 調査方法
                if (intCopyFlg == 0){
                    alert('<%= String.Format(Messages.MSG133E, "全て", "調査方法") %>');
                }else if(intDisalbedFlg > 0){
                    alert('<%= String.Format(Messages.MSG133E, "一部", "調査方法") %>');
                }
            }else if(objId.indexOf("ButtonCopyTysGaisya") != -1){ // 調査会社
                if (intCopyFlg == 0){
                    alert('<%= String.Format(Messages.MSG133E, "全て", "調査会社") %>');
                }else if(intDisalbedFlg > 0){
                    alert('<%= String.Format(Messages.MSG133E, "一部", "調査会社") %>');
                }
            }else{ // 商品1
                if (intCopyFlg == 0){
                    alert('<%= String.Format(Messages.MSG133E, "全て", "商品1") %>');
                }else if(intDisalbedFlg > 0){
                    alert('<%= String.Format(Messages.MSG133E, "一部", "商品1") %>');
                }
            }
        }   

        // 画面下部・一括補助の金額テキストボックスをクリアする
        function clearKingakuText(){
            var objKoumu = objEBI("<%= TextKoumutenSeikyuuGaku.clientID %>");
            var objJitu = objEBI("<%= TextJituSeikyuuKinGaku.clientID %>");
            var objSyoudaku = objEBI("<%= TextSyoudakusyoKingaku.clientID %>");
            
            objKoumu.value = "";
            objJitu.value = "";
            objSyoudaku.value = "";
        }

        //  画面下部・明細のの金額テキストボックスをクリアする
        function clearMeisaiKingakuText(chgRowCtrlId, intRowNo){
            var objKoumu = objEBI(chgRowCtrlId + "_TextKoumutenKingaku_2_" + intRowNo);
            var objJitu = objEBI(chgRowCtrlId + "_TextJituSeikyuuKingaku_2_" + intRowNo);
            var objSyoudaku = objEBI(chgRowCtrlId + "_TextSyoudakusyoKingaku_2_" + intRowNo);

            objKoumu.value = "";
            objJitu.value = "";
            objSyoudaku.value = "";
        }
        
        // 金額テキストボックスを活性化する
        function activateKingakuText(obj){
            obj.className = "kingaku";
            obj.readOnly = false;
            obj.tabIndex = "";
            obj.style.borderStyle = "";                            
        }
        
        // 請求有無ドロップダウンリストを活性化する
        function activateCbo(objCbo, objSpan){
            objCbo.style.display = "";
            objSpan.innerHTML = "";
        }
        
        // 画面下部・商品2追加ボタン押下処理
        function setAddValue(){
            var setVal1 = objEBI("<%= SelectSyouhin2.clientID %>").value;
            var setVal2 = objEBI("<%= TextKoumutenSeikyuuGaku.clientID %>").value;
            var setVal3 = objEBI("<%= TextJituSeikyuuKinGaku.clientID %>").value;
            var setVal4 = objEBI("<%= TextSyoudakusyoKingaku.clientID %>").value;
            var setVal5 = objEBI("<%= SelectSeikyuuUmu.clientID %>").value;
            var setVal6 = objEBI("<%= HiddenBunruiCd.clientID %>").value;
            
            var varTarget1 = "_SelectSyouhin_2_"
            var varTarget2 = "_TextKoumutenKingaku_2_"
            var varTarget3 = "_TextJituSeikyuuKingaku_2_"
            var varTarget4 = "_TextSyoudakusyoKingaku_2_"
            var varTarget5 = "_SelectSeikyuuUmu_2_"
            var varTarget6 = "_HiddenRowDisplay_2_"
            var varTarget7 = "_CheckAutoCalc_2_"
            var varTarget8 = "_SPAN_Seikyuu_2_"
            var varTarget9 = "_HiddenDbValue_2_"
            var varTarget10 = "_HiddenBunruiCd_2_"
            
            var varTargetRow;
            var varTargetUriage;
            var intCntX;
            var strCntX;
            var intCntY;
            var strCntY;

            //追加内容が空白の場合            
            if(setVal1 == ''){
                alert('<%= String.Format(Messages.MSG126E, "商品2") %>');
                return false;      
            }
            
            if (setVal2 == ''){
                setVal2 = '0'
            }
            if (setVal3 == ''){
                setVal3 = '0'
            }
            if (setVal4 == ''){
                setVal4 = '0'
            }
            
            var VarCnt = objEBI("<%= HiddenLineCnt.clientID %>").value; // 明細行数
            var intCopyFlg = 0;
            var intDisalbedFlg = 0;
            
            for(intCntX = 1; intCntX <= VarCnt; intCntX++){
                strCntX = "" + intCntX;
                var varTargetUriage = objEBI(gVarSettouJi_1 + strCntX + "_TextUriageSyori").value;
                var varTargetTorkeshi = objEBI(gVarSettouJi_1 + strCntX + "_HiddenTorikeshi").value;
                if (varTargetUriage == "" && varTargetTorkeshi != "1"){
                    for (intCntY = 1; intCntY <= 4; intCntY++){
                        strCntY = "" + intCntY;
                        varTargetRow = (gVarSettouJi_2 + strCntX + "_TrTysSyouhin_2_" + intCntY);
                        if (objEBI(varTargetRow).style.display == "none"){
                            var varTmpId1 = gVarSettouJi_2 + strCntX + varTarget1 + strCntY;
                            var varTmpId2 = gVarSettouJi_2 + strCntX + varTarget2 + strCntY;
                            var varTmpId3 = gVarSettouJi_2 + strCntX + varTarget3 + strCntY;
                            var varTmpId4 = gVarSettouJi_2 + strCntX + varTarget4 + strCntY;
                            var varTmpId5 = gVarSettouJi_2 + strCntX + varTarget5 + strCntY;
                            var varTmpId6 = gVarSettouJi_2 + strCntX + varTarget6 + strCntY;
                            var varTmpId7 = gVarSettouJi_2 + strCntX + varTarget7 + strCntY;
                            var varTmpId8 = gVarSettouJi_2 + strCntX + varTarget8 + strCntY;
                            var varTmpId9 = gVarSettouJi_2 + strCntX + varTarget9 + strCntY;
                            var varTmpId10 = gVarSettouJi_2 + strCntX + varTarget10 + strCntY;
                            
                            objEBI(varTargetRow).style.display = "inline";
                            objEBI(varTmpId1).value = setVal1;
                            activateKingakuText(objEBI(varTmpId2));
                            if (objEBI(varTmpId2).readOnly){
                                setVal2 = '0';
                            }
                            objEBI(varTmpId2).value = setVal2;
                            
                            activateKingakuText(objEBI(varTmpId3));
                            if (objEBI(varTmpId3).readOnly){
                                setVal3 = '0';
                            }
                            objEBI(varTmpId3).value = setVal3;
                            
                            activateKingakuText(objEBI(varTmpId4));
                            if (objEBI(varTmpId4).readOnly){
                                setVal4 = '0';
                            }
                            objEBI(varTmpId4).value = setVal4;
                            
                            if (objEBI(varTmpId8).innerHTML == "" || objEBI(varTmpId9).value == ""){
                                activateCbo(objEBI(varTmpId5),objEBI(varTmpId8))
                                objEBI(varTmpId5).value = setVal5;
                            }
                            objEBI(varTmpId6).value = "inline";
                            objEBI(varTmpId7).checked = true;
                            
                            objEBI(varTmpId10).value = setVal6;
                            
                            break;
                        }
                    }
                    intCopyFlg = intCopyFlg + 1;
                }else{
                    intDisalbedFlg = intDisalbedFlg + 1;    
                }
            }
            if (intCopyFlg == 0){
                alert('<%= String.Format(Messages.MSG134E, "全て") %>');
            }else if(intDisalbedFlg > 0){
                alert('<%= String.Format(Messages.MSG134E, "一部") %>');
            }
            // 商品2テーブル 各種レイアウト設定
            settingItem2Table();
            return true;   
        }

        // 値変更時に計算チェックボックスをONにする処理(商品1)
        function rowChgItem1(chgRowCtrlId){
            objEBI(chgRowCtrlId + "_CheckAutoCalc").checked = true;
            
            return true;
        }

        // 値変更時に計算チェックボックスをONにする処理(商品2)
        function rowChgItem2(chgRowCtrlId, intRowNo){
            objEBI(chgRowCtrlId + "_CheckAutoCalc_2_" + intRowNo).checked = true;
            
            return true;
        }
        
        // 金額再計算用フラグ再設定処理の呼び出し
        function setKingakuItem2(chgObj, chgRowCtrlId, intRowNo){
            var autoObjId = chgRowCtrlId + "_HiddenAutoKingakuFlg_2_" + intRowNo;
            var manualObjId = chgRowCtrlId + "_HiddenManualKingakuFlg_2_" + intRowNo;
            var bothjId = chgRowCtrlId + "_HiddenBothKingakuChgFlg_2_" + intRowNo;

            setKingaku(chgObj, autoObjId, manualObjId, bothjId);
        }
        
        // 工務店請求金額、及び実請求金額変更時に金額の再計算をするフラグを設定
        function setKingaku(chgObj, autoObjId, manualObjId, bothObjId){
            if (objEBI(bothObjId).value != ""){
                if (objEBI(bothObjId).value != chgObj.id){
                    objEBI(bothObjId).value = "1";
                }
            }else{
                objEBI(bothObjId).value = chgObj.id;
            }
            if (objEBI(autoObjId).value == ""){
                if (chgObj.id.indexOf("TextKoumutenKingaku", 0) >= 0){
                    objEBI(autoObjId).value = "1"
                }else{
                    objEBI(autoObjId).value = "2"
                }
            }
            if (objEBI(manualObjId).value == ""){
                if (chgObj.id.indexOf("TextKoumutenKingaku", 0) >= 0){
                    objEBI(manualObjId).value = "1"
                }else{
                    objEBI(manualObjId).value = "2"
                }
            }else{
                if (chgObj.id.indexOf("TextKoumutenKingaku", 0) >= 0){
                    if (objEBI(autoObjId).value == "1"){
                        objEBI(manualObjId).value = "1"
                    }
                }else{
                    if (objEBI(autoObjId).value == "2"){
                        objEBI(manualObjId).value = "2"
                    }
                }
            }
        }
        
        // 親のコントロールを取得
        function getParent(el, pTagName) {
	        if (el == null) {
		        return null;
	        } else if (el.nodeType == 1 && el.tagName.toLowerCase() == pTagName.toLowerCase()) {
		        return el;
	        } else {
		        return getParent(el.parentNode, pTagName);
	        }
        }
        // 追加ボタン押下時の処理
        function rowAdd(obj){
            var objTd = obj.parentNode;
            var objTr = objTd.parentNode;
            var objTable = getParent(objTd, 'TABLE');
            var objArr = new Object();
            var strKokyakuNo = objTr.cells[1].childNodes[0].value;
            var bukkenArr = [];
            var wkTr;
            
            // 商品２情報明細テーブルをループする
            for (i = 0; i < objTable.tBodies[0].rows.length; i = i + 4){ 
                var rowArr = [];
                // 4行1セットの配列を作成する
                for (j = 0; j < 4; j++){
                    rowArr[j] = objTable.tBodies[0].rows[i+j];
                }
                // 4行1セットの配列を顧客番号をKeyにした連想配列に登録する
                var strKey = objTable.tBodies[0].rows[i].cells[1].childNodes[0].value;
                objArr[strKey] = rowArr;
            }
            // 処理対象物件を取得する
            bukkenArr = objArr[strKokyakuNo];
            
            var blnAddFlg = false
            // 処理対象物件からクリックした行を抜いた配列を作成する
            for (i = 0; i < bukkenArr.length; i++){
                wkTr = bukkenArr[i];
                if (wkTr.style.display == "none"){
                    // 非表示から表示に変更
                    wkTr.style.display = "inline";
                    wkTr.cells[1].childNodes[4].value = "inline";
                    // 請求有無活性化用変数
                    var objHdh;
                    var objCbo;
                    var objSpan;
                    
                    // 計算チェックボックスにチェックをつける, 金額項目を活性化させる
                    for (j = 0; j < wkTr.cells[0].childNodes.length; j++){
                        if (wkTr.cells[0].childNodes[j].id != null && wkTr.cells[0].childNodes[j].id != "undefined"){
                            var objTarget = wkTr.cells[0].childNodes[j];
                            switch (objTarget.type) {
                                case "hidden":
                                    if (objTarget.id.indexOf("HiddenDbValue", 0) > 0){
                                        objHdh = objTarget;
                                    }
                                    break;
                                case "text":
                                    break;
                                case "select-one":
                                    break;
                                case "checkbox":
                                    if (objTarget.childNodes[j].id.indexOf("CheckAutoCalc", 0) > 0){
                                        objTarget.childNodes[j].checked = true;
                                    }
                                    break;
                                default:
                                    for (k = 0; k < objTarget.childNodes.length; k++){
                                        if (objTarget.childNodes[k].id.indexOf("CheckAutoCalc", 0) > 0){
                                            objTarget.childNodes[k].checked = true;
                                        }
                                    break;
                                    }
                            }
                        }
                    }
                    // 金額項目を活性化させる(工務店請求額)
                    for (j = 0; j < wkTr.cells[5].childNodes.length; j++){
                        if (wkTr.cells[5].childNodes[j].id != null && wkTr.cells[5].childNodes[j].id != "undefined"){
                            var objTarget = wkTr.cells[5].childNodes[j];
                            switch (objTarget.type) {
                                case "text":
                                    if (objTarget.id.indexOf("TextKoumutenKingaku", 0) > 0){
                                        activateKingakuText(objTarget);
                                        objTarget.value = "";
                                        break;
                                    }
                                default:
                                    break;
                            }
                        }
                    }
                    // 金額項目を活性化させる(実請求金額)
                    for (j = 0; j < wkTr.cells[6].childNodes.length; j++){
                        if (wkTr.cells[6].childNodes[j].id != null && wkTr.cells[6].childNodes[j].id != "undefined"){
                            var objTarget = wkTr.cells[6].childNodes[j];
                            switch (objTarget.type) {
                                case "text":
                                    if (objTarget.id.indexOf("TextJituSeikyuuKingaku", 0) > 0){
                                        activateKingakuText(objTarget);
                                        objTarget.value = "";
                                        break;
                                    }
                                default:
                                    break;
                            }
                        }
                    }
                    // 金額項目を活性化させる(承諾書金額)
                    for (j = 0; j < wkTr.cells[7].childNodes.length; j++){
                        if (wkTr.cells[7].childNodes[j].id != null && wkTr.cells[7].childNodes[j].id != "undefined"){
                            var objTarget = wkTr.cells[7].childNodes[j];
                            switch (objTarget.type) {
                                case "text":
                                    if (objTarget.id.indexOf("TextSyoudakusyoKingaku", 0) > 0){
                                        activateKingakuText(objTarget);
                                        objTarget.value = "";
                                        break;
                                    }
                                default:
                                    break;
                            }
                        }
                    }
                    // 請求有無をブランクに設定
                    for (j = 0; j < wkTr.cells[8].childNodes.length; j++){
                        if (wkTr.cells[8].childNodes[j].id != null && wkTr.cells[8].childNodes[j].id != "undefined"){
                            var objTarget = wkTr.cells[8].childNodes[j];
                            switch (objTarget.type) {
                                case "select-one":
                                    if (objTarget.id.indexOf("SelectSeikyuuUmu", 0) > 0){
                                        objCbo = objTarget;
                                        break;
                                    }
                                default:
                                    if (objTarget.id.indexOf("SPAN_Seikyuu", 0) > 0){
                                        objSpan = objTarget;
                                        break;
                                    }
                                    break;
                            }
                        }
                    }
                    if (objSpan.innerHTML == "" || objHdh.value == ""){
                        activateCbo(objCbo, objSpan);
                    }
                                        
                    wkTr.cells[3].childNodes[0].focus();
                    blnAddFlg = true;
                    break;
                }
            }            
            if (blnAddFlg == false){
                alert('<%= Messages.MSG128E %>');
            }
        }
        
        //　削除ボタン押下時の処理
        function rowDelete(obj){
            var objTd = obj.parentNode;
            var objTr = objTd.parentNode;

            // 処理対象行の値をクリアする
            for (i = 0; i < objTr.cells.length; i++){
                var objCell = objTr.cells[i]
                var intSeikyuuFlg = 0;
                var objCbo;
                var objSpan;
                for (j = 0; j < objCell.childNodes.length; j++){
                    var clearObj = objCell.childNodes[j];
                    switch (clearObj.type) {
                        case "hidden":
                            if(clearObj.id.indexOf("HiddenSeikyuuSakiCd",0) > 0){  
                                clearObj.value = "";
                            }
                            switch (true){
                                case clearObj.id.indexOf("HiddenDbValue", 0) < 0:
                                    break;
                                case clearObj.id.indexOf("HiddenGamenHyoujiNo",0) < 0:
                                    break;
                                case clearObj.id.indexOf("HiddenKameitenCode",0) < 0:
                                    break;
                                case clearObj.id.indexOf("HiddenKeiretuCd",0) < 0:
                                    break;
                                case clearObj.id.indexOf("HiddenTysSeikyuuSaki",0) < 0:
                                    break;
                                default:
                                    clearObj.value = "";
                                    break;
                            }
                            break;
                        case "text":
                            if (clearObj.id.indexOf("TextKokyakuBangou", 0) < 0 && (clearObj.id.indexOf("TextNo",0) < 0)){
                                clearObj.value = "";
                            }
                            break;
                        case "select-one":
                            if (clearObj.id.indexOf("SelectSeikyuuUmu",0) < 0){
                                clearObj.value = "";
                            }else{
                                intSeikyuuFlg = 1;
                                objCbo = clearObj;
                            }
                            break;
                        case "checkbox":
                            clearObj.checked = false;
                            break;
                        default:
                            for (k = 0; k < clearObj.childNodes.length; k++){
                                if (clearObj.childNodes[k].type == "checkbox"){
                                    clearObj.childNodes[k].checked = false;
                                }
                            }
                            if (clearObj.id != undefined){
                                if (clearObj.id.indexOf("SPAN_Seikyuu",0) > 0){
                                    objSpan = clearObj;
                                }
                            }
                            break;
                    }
                }
                if (intSeikyuuFlg == 1 && objSpan.innerHTML == ""){
                    objCbo.value = "";
                }
            }

            objTr.style.display = "none";
            objTr.cells[1].childNodes[4].value = "none";
            
            // 商品2テーブル 各種レイアウト設定
            settingItem2Table();
        
        }

        //調査会社検索処理を呼び出す(明細行用)
        var JStyousakaisyaSearchType = 0;
        function callTyousakaisyaSearchMeisai(objText, strRowCtlrId){
            objEBI(strRowCtlrId + "_tyousakaisyaSearchType").value = "";
            if(objText == ""){
                objEBI(strRowCtlrId + "_tyousakaisyaSearchType").value = "1";
                objEBI(strRowCtlrId + "_ButtonSearchTyousakaisya").click();
            }
        }    
        
        //プラス値不許可チェック対象か調べる
        function checkPlusTaisyou(){     
            var objSyouhinCd = objEBI("<%= SelectSyouhin2.clientID %>");
            var objArrSyouhinCd = objEBI("<%= HiddenArrSyouhinCd.clientID %>");
            var objArrBunruiCd = objEBI("<%= HiddenArrBunruiCd.clientID %>");
            var objBunruiCd = objEBI("<%= HiddenBunruiCd.clientID %>");
           
            if(objArrSyouhinCd.value == "" || objArrBunruiCd.value == "")return false;
                       
            //Hiddenの値を配列に格納
            var arrSyouhinCd = objArrSyouhinCd.value.split(sepStr);
            var arrSoukoCd = objArrBunruiCd.value.split(sepStr);
               
            if(arrSyouhinCd.length != arrSoukoCd.length)return false;
                   
            //商品2の分類コードを検索
            for(var i = 0; i < arrSyouhinCd.length; i++) {
                if(objSyouhinCd.value == arrSyouhinCd[i]){
                    //取得した分類コードをHiddenに格納
                    objBunruiCd.value = arrSoukoCd[i];
                    return true;
                }
            }
            return false;
        }
        
        /**
         * 商品1が特別対応価格反映されているかチェック
         * @param ツールチップのDisplayCd(ClientID)
         * @param 顧客番号
         * @return 商品1を変更するか否か(Boolean)
         */
        function ChkTokubetuTaiou(strId, strVal){
            var objDisplayCd = objEBI(strId);   //ツールチップDisplayコード
            var strMsg = "<%= Messages.MSG202C %>";
            
            if(strVal != ""){
                strMsg = strMsg + "\r\n顧客番号:" + strVal;
            }
            
            //価格反映されている場合、確定メッセージ表示
            if(objDisplayCd.value != ""){
                if(confirm(strMsg)){
                    return true;
                }
                return false;
            }
            return true;
        }

        /**
         * 商品コードをセットする
         * @param コピー元商品1(ClientID)、コピー先商品1(ClientID)
         * @return
         */
        function SetSyouhin1(Syouhin1FromId, Syouhin1ToId){
            var objSyouhin1From = objEBI(Syouhin1FromId);
            var objSyouhin1To = objEBI(Syouhin1ToId);

            objSyouhin1To.value = objSyouhin1From.value;

        }  

    </script>

    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <div>
        <input type="hidden" id="HiddenLineCnt" runat="server" value="0" /><%-- 物件数 --%>
        <asp:DropDownList ID="SelectTysKaisya" runat="server" Style="display: none;">
        </asp:DropDownList>
        <table>
            <tr>
                <td>
                    <%-- 画面タイトル --%>
                    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
                        class="titleTable">
                        <tr>
                            <th style="text-align: left; width: 200px;">
                                一括変更【調査商品情報】</th>
                            <th style="text-align: right">
                                <input type="button" runat="server" id="ButtonNaiyouChk" value="内容チェック" class="selectedStyleG"
                                    style="font-weight: bold; font-size: 18px; width: 120px; color: black; height: 30px;" />
                                <input type="button" runat="server" id="ButtonIkkatuHenkou" value="一括変更" style="font-weight: bold;
                                    font-size: 18px; width: 120px; color: black; height: 30px; background-color: fuchsia" />
                            </th>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <%-- 画面上部1 --%>
                    <table style="text-align: left; width: 100%;">
                        <tr>
                            <td>
                                <span style="font-weight: bold; font-size: 15px;">一括変更補助</span>
                            </td>
                            <td style="text-align: right;">
                                <asp:Label ID="LabelNgTysKaisya" runat="server" Style="color: red; font-size: 16px;"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <%-- 画面上部[一括変更補助] --%>
                    <table>
                        <tr>
                            <td>
                                <table style="text-align: left; width: 100%;" class="mainTable" cellpadding="1">
                                    <tr>
                                        <td class="koumokuMei2">
                                            <input type="button" id="ButtonCopyTysHouhou" class="button_copy" style="padding-top: 2px;
                                                width: 60px; height: 25px;" value="調査方法" onclick="setCopyValue(this.id)" />
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="TextTysHouhouCode" Style="width: 18px" CssClass="pullCd" MaxLength="2"/><asp:DropDownList
                                                runat="server" ID="SelectTysHouhou" Style="width: 220px">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="koumokuMei2" style="width: 60px">
                                            <input type="button" id="ButtonCopyTysGaisya" class="button_copy" style="padding-top: 2px;
                                                width: 60px; height: 25px;" value="調査会社" onclick="setCopyValue(this.id)" />
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="TextTysGaisyaCode" Style="width: 48px" CssClass="codeNumber"
                                                MaxLength="7" />
                                            <input type="button" id="ButtonTysGaisya" value="検索" class="gyoumuSearchBtn" runat="server" />
                                            <asp:HiddenField ID="HiddenTyousaKaishaCdOld" runat="server" />
                                            <input type="hidden" id="tyousakaisyaSearchType" runat="server" value="" />
                                            <asp:TextBox runat="server" ID="TextTysGaisyaName" Style="width: 240px" CssClass="readOnlyStyle"
                                                ReadOnly="true" TabIndex="-1" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="koumokuMei2">
                                            <input type="button" id="ButtonCopySyouhin1" class="button_copy" style="padding-top: 2px;
                                                width: 60px; height: 25px;" value="商品1" onclick="setCopyValue(this.id)" />
                                        </td>
                                        <td colspan="3">
                                            <asp:DropDownList runat="server" ID="SelectSyouhin1">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <%-- 画面上部メイン --%>
                    <table cellpadding="0" cellspacing="0" style="border-bottom: solid 2px gray; border-left: solid 2px gray;">
                        <tr>
                            <td>
                                <table class="mainTable" style="width: 930px; border-bottom: none; border-left: solid 0px gray;
                                    table-layout: fixed;" id="Table2" cellpadding="0" cellspacing="0">
                                    <%-- ヘッダ部 --%>
                                    <tr>
                                        <td class="koumokuMei2" style="width: 20px;" rowspan="2">
                                            計算</td>
                                        <td class="koumokuMei2" style="width: 80px" colspan="1">
                                            顧客番号</td>
                                        <td class="koumokuMei2" style="width: 248px" colspan="4">
                                            調査方法</td>
                                        <td class="koumokuMei2" style="width: 323px" colspan="10">
                                            調査会社</td>
                                        <td class="koumokuMei2" colspan="5">
                                            加盟店</td>
                                    </tr>
                                    <tr>
                                        <td class="koumokuMei2" style="font-size: 13px; padding-top: 5px; padding-left: 1px" colspan="4">
                                            施主名</td>
                                        <td class="koumokuMei2" style="font-size: 13px; padding-top: 5px; padding-left: 1px"
                                            colspan="1">
                                            売上処理</td>
                                        <td class="koumokuMei2" style="font-size: 13px; padding-top: 5px; padding-left: 1px" colspan="7">
                                            商品1</td>
                                        <td class="koumokuMei2" style="font-size: 13px; padding-top: 5px; padding-left: 1px" colspan="3">
                                            工務店請求額</td>
                                        <td class="koumokuMei2" style="font-size: 13px; padding-top: 5px; padding-left: 1px" colspan="2">
                                            実請求金額</td>
                                        <td class="koumokuMei2" style="font-size: 13px; padding-top: 5px; padding-left: 1px" colspan="2">
                                            承諾書金額</td>
                                        <td class="koumokuMei2" style="font-size: 13px; padding-top: 5px; padding-left: 1px" colspan="1">
                                            請求</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="height: 200px; overflow-y: scroll; width: 946px; border-top: none; border-left: solid 0px gray;">
                                    <table class="mainTable" cellpadding="0" cellspacing="0" style="width: 930px; table-layout: fixed;
                                        border-top: none; border-left: solid 0px gray;">
                                        <%-- データ部 --%>
                                        <tbody id="tblMeisaiSyouhin1" runat="server">
                                        </tbody>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="height: 10px">
            </tr>
            <tr>
                <td>
                    <%-- 画面下部 --%>
                    <table>
                        <tr>
                            <td>
                                <span style="font-weight: bold; font-size: 15px;">一括変更補助</span>
                            </td>
                        </tr>
                    </table>
                    <%-- 画面下部[一括変更補助] --%>
                    <table>
                        <tr>
                            <td>
                                <table style="text-align: left; width: 100%;" id="Table3" class="mainTable" cellpadding="1">
                                    <tr>
                                        <td class="koumokuMei2" rowspan="2">
                                            <input type="button" runat="server" id="ButtonSyouhin2Add" class="button_copy" value="商品2追加"
                                                onclick="setAddValue()" />
                                        </td>
                                        <td class="koumokuMei2">
                                            商品2
                                        </td>
                                        <td class="koumokuMei2">
                                            工務店請求額
                                        </td>
                                        <td class="koumokuMei2">
                                            実請求金額
                                        </td>
                                        <td class="koumokuMei2">
                                            承諾書金額
                                        </td>
                                        <td class="koumokuMei2">
                                            請求
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList runat="server" ID="SelectSyouhin2" Style="width: 300px">
                                            </asp:DropDownList><input type="hidden" id ="HiddenBunruiCd" runat="server" /><input type="hidden"
                                                id ="HiddenArrSyouhinCd" runat="server" /><input type="hidden" id ="HiddenArrBunruiCd" runat="server" />
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="TextKoumutenSeikyuuGaku" Style="width: 90px" CssClass="kingaku"
                                                MaxLength="7" />
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="TextJituSeikyuuKinGaku" Style="width: 90px" CssClass="kingaku"
                                                MaxLength="7" />
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="TextSyoudakusyoKingaku" Style="width: 90px" CssClass="kingaku"
                                                MaxLength="7" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="SelectSeikyuuUmu" runat="server" Style="width: 40px">
                                                <asp:ListItem Value="1" Text="有" />
                                                <asp:ListItem Value="0" Text="無" />
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                    </table>
                    <br />
                    <%-- 画面下部メイン --%>
                    <table cellpadding="0" cellspacing="0" style="border-bottom: solid 2px gray; border-left: solid 2px gray;">
                        <tr>
                            <td>
                                <table class="mainTable" style="width: 930px; border-bottom: none; border-left: solid 0px gray;"
                                    id="TableSearch" cellpadding="0" cellspacing="0">
                                    <%-- ヘッダ部 --%>
                                    <tr id="trHeadSyouhin2" runat="server">
                                        <td class="koumokuMei2" style="width: 20px">
                                            計算</td>
                                        <td class="koumokuMei2" style="width: 88px">
                                            顧客番号</td>
                                        <td class="koumokuMei2" style="width: 20px">
                                            表示順</td>
                                        <td class="koumokuMei2" style="width: 290px">
                                            商品2</td>
                                        <td class="koumokuMei2" style="width: 65px">
                                            売上処理</td>
                                        <td class="koumokuMei2" style="width: 80px">
                                            工務店<br />
                                            請求額</td>
                                        <td class="koumokuMei2" style="width: 79px">
                                            実請求<br />
                                            金額</td>
                                        <td class="koumokuMei2" style="width: 79px">
                                            承諾書<br />
                                            金額</td>
                                        <td class="koumokuMei2" style="width: 54px">
                                            請求</td>
                                        <td class="koumokuMei2" style="width: 38px">
                                            追加</td>
                                        <td class="koumokuMei2" style="width: 38px">
                                            削除</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <%-- 明細部 --%>
                                <div style="width: 946px; height: 150px; overflow-y: scroll; border-top: none; border-left: solid 0px gray;">
                                    <table class="mainTable" cellpadding="0" cellspacing="0" style="width: 930px; border-top: none;
                                        border-left: solid 0px gray;">
                                        <%-- データ部 --%>
                                        <tbody id="tblMeisaiSyouhin2" runat="server">
                                        </tbody>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

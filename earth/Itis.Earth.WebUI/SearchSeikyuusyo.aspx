<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="SearchSeikyuusyo.aspx.vb" Inherits="Itis.Earth.WebUI.SearchSeikyuusyo"
    Title="EARTH 請求書一覧" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script src="js/sortable_sync_ja.js" type="text/javascript">
    </script>

    <script type="text/javascript">

        //ウィンドウサイズ変更
        try{
            if(window.name != "<%=EarthConst.MAIN_WINDOW_NAME %>") window.resizeTo(1024,768);
        }catch(e){
            //アクセスが拒否されましたのエラーが出たら何もしない。
            if(e.number == 2147024891) throw e;
        }

        _d = document;
        /*====================================
         *グローバル変数宣言（画面部品）
         ====================================*/
        var gVarSettouJi = "ctl00_CPH1_"; 
        var gVarTr1 = "DataTable_resultTr1_";
        var gVarTr2 = "DataTable_resultTr2_";
        var gVarTdSentou = "DataTable_Sentou_Td_";
        var gVarHdnSeikyuusyoNo = "HdnSeikyuusyoNo_";
        var gVarChkTaisyou = "ChkTaisyou_";
        
        //画面表示部品
        var objSeikyuuDateFrom = null;
        var objSeikyuuDateTo = null;
        var objSeikyuuSakiKbn = null;
        var objSeikyuuSakiCd = null;
        var objSeikyuuSakiBrc = null;   
        var objSeikyuuSimeDate = null;
        var objSeikyuuSyosiki = null;
        var objMeisaiKensuuFrom = null;
        var objMeisaiKensuuTo = null;
        var objTorikesiTaisyou = null;
        var objInjiTaisyou = null;
        
        //Hidden
        var objHdnPrmSeikyuuDateTo = null;
        var objCsvOutPutFlg = null;
        var objCsvMaxCntFlg = null;

        //検索実行用
        var objMaxSearchCount = null;
        var objSearch = null;
        //ダミーボタン
        var objCsv = null;
        
        //画面遷移用
        var objSendVal_SeikyuusyoNo = null;
        var objSendVal_SeikyuusyoNoPrint = null;
        var objSendVal_UpdDatetime = null;
        var objSendVal_ChkedTaisyou = null;
        var objSendVal_PrintTaisyougai = null;
        var objSendVal_TorikesiTaisyougai = null;
        var objSendVal_SyosikiTaisyougai = null;
        
        var objSendVal_GamenMode = null;
        
        var gVarPdfFlg = null; //Pdf出力判断フラグ
        var gVarExcelFlg = null; //Excel出力判断フラグ
        
        //アクション実行ボタン(検索実行,CSV出力,請求書印刷,請求書取消)
        var gBtnSearch = null;
        var gBtnCsv = null;
        var gBtnPrint = null;
        var gBtnExcel = null;
        var gBtnTorikesi = null;
                
        //該当データなしメッセージ制御用フラグ
        var gNoDataMsgFlg = null;
        
        //CSV出力処理時メッセージ
        var gVarCfmMsg = null;
        
        /*************************************
         * onload時の追加処理
         *************************************/
        function funcAfterOnload() {
            //画面表示部品セット
            setGlobalObj();
            
            /*検索結果テーブル ソート設定*/
            sortables_init();
            
            /*検索結果テーブル 各種レイアウト設定*/
            settingResultTable();
            
            /* 各種ボタン押下時処理を行なう */
            exeButtonJikkou();
        }
        
        /*********************************************
        * 各種ボタン押下時処理を行なう
        *********************************************/
        function exeButtonJikkou(){
            // CSV出力を行なう
            if (objCsvOutPutFlg.value == "1"){
            
                // CSV出力フラグをクリア
                objCsvOutPutFlg.value = "";
                
                //チェック状態を戻す
                setCheckedReturn();
                
                // CSV出力上限の確認
                if(objCsvMaxCntFlg.value == "1"){
                   gVarCfmMsg = "<%= Messages.MSG017C %>".replace("処理","CSV出力処理") 
                             + "<%= Messages.MSG162C %>".replace("@PARAM1","<%=EarthConst.MAX_CSV_OUTPUT %>").replace("@PARAM2","<%=EarthConst.MAX_CSV_OUTPUT %>")
                }else{
                   gVarCfmMsg = "<%= Messages.MSG017C %>".replace("処理","CSV出力処理")
                }
                
                // CSV出力上限フラグをクリア
                objCsvMaxCntFlg.value = "";
                
                //処理確認
                if(!confirm(gVarCfmMsg)){
                    // CSV出力フラグをクリア
                    objCsvOutPutFlg.value = "";
                    return false;
                }
                //CSV実行
                objCsv.click();
                return true;
            }
            // Pdf出力
            if(gVarPdfFlg != null){
                gVarPdfFlg = null; //初期化
                
                var varMsg = "";
                
                //印刷日は登録済みだが、印刷する請求書が無い場合
                if(objSendVal_SeikyuusyoNoPrint.value == ""){
                    varMsg = "<%= Messages.MSG173C %>";
                    alert(varMsg);
                    return false;
                }
                
                //PDF出力
                PdfOutput();
            }
            
        }
        
       /*********************************************
        * 戻り値がない為、同メソッドをオーバーライド
        *********************************************/
        function returnSelectValue(){
            return false;
        }
        
       /*********************************************
        * 値をチェックし、対象をクリアする
        *********************************************/
        function clrName(obj,targetId){
            if(obj.value == "") objEBI(targetId).value="";
        }
        
       /*********************************************
        * 画面表示部品オブジェクトをグローバル変数化
        *********************************************/
        function setGlobalObj() {
            //画面表示部品
            objHdnPrmSeikyuuDateTo = objEBI("<%= HiddenPrmSeikyuusyoHakDateTo.clientID %>");
            objSeikyuuDateFrom = objEBI("<%= TextSekyuusyoHakDateFrom.clientID %>");
            objSeikyuuDateTo = objEBI("<%= TextSekyuusyoHakDateTo.clientID %>");
            objSeikyuuSakiKbn = objEBI("<%= SelectSeikyuuSakiKbn.clientID %>");
            objSeikyuuSakiCd = objEBI("<%= TextSeikyuuSakiCd.clientID %>");
            objSeikyuuSakiBrc = objEBI("<%= TextSeikyuuSakiBrc.clientID %>");
            objSeikyuuSimeDate = objEBI("<%= TextSeikyuuSimeDate.clientID %>");
            objSeikyuuSyosiki = objEBI("<%= SelectSeikyuuSyousiki.clientID %>");
            objMeisaiKensuuFrom = objEBI("<%= TextMeisaiKensuuFrom.clientID %>");
            objMeisaiKensuuTo = objEBI("<%= TextMeisaiKensuuTo.clientID %>");
            objTorikesiTaisyou = objEBI("<%= CheckTorikesiTaisyou.clientID %>");
            objInjiTaisyou = objEBI("<%= CheckInjiTaisyou.clientID %>");
            
            //検索実行用
            objMaxSearchCount = objEBI("<%= maxSearchCount.clientID %>");
            objSearch = objEBI("<%= search.clientID %>");
            
            // CSV出力用
            objCsv = objEBI("<%= ButtonHiddenCsv.clientID %>");
            objCsvOutPutFlg = objEBI("<%= HiddenCsvOutPut.clientID %>");
            objCsvMaxCntFlg = objEBI("<%= HiddenCsvMaxCnt.clientID %>");
            
            //画面遷移用
            objSendVal_SeikyuusyoNo = objEBI("<%= HiddenSendValSeikyuusyoNo.clientID %>");
            objSendVal_SeikyuusyoNoPrint = objEBI("<%= HiddenSendValSeikyuusyoNoPrint.clientID %>");
            objSendVal_UpdDatetime = objEBI("<%= HiddenSendValUpdDatetime.clientID %>");
            objSendVal_ChkedTaisyou = objEBI("<%= HiddenChkedTaisyou.clientID %>");
            objSendVal_PrintTaisyougai = objEBI("<%= HiddenPrintTaisyougai.clientID %>");
            objSendVal_TorikesiTaisyougai = objEBI("<%= HiddenTorikesiTaisyougai.clientID %>");
            objSendVal_SyosikiTaisyougai = objEBI("<%= HiddenSyosikiTaisyougai.clientID %>");           
                        
            objSendVal_GamenMode = objEBI("<%= HiddenGamenMode.clientID %>");
            
            //アクション実行ボタン
            gBtnSearch = "<%= EarthEnum.emSearchSeikyuusyoBtnType.Search %>";
            gBtnCsv = "<%= EarthEnum.emSearchSeikyuusyoBtnType.CsvOutput %>";
            gBtnPrint = "<%= EarthEnum.emSearchSeikyuusyoBtnType.Print %>";
            gBtnExcel = "Excel";
            gBtnTorikesi = "<%= EarthEnum.emSearchSeikyuusyoBtnType.Torikesi %>";
        }

        /**
         * 明細行をダブルクリックした際の処理
         * @param objSelectedTr
         * @return
         */
        function returnSelectValue(objSelectedTr) {
            var varTrId = objSelectedTr.id;
            var varRow = varTrId.replace(gVarSettouJi,"");
            varRow = varRow.replace(gVarTr1,"");
            varRow = varRow.replace(gVarTr2,"");
            varRow = varRow.replace(gVarTdSentou,"");
            
            var varHdn = _d.getElementById(gVarSettouJi + gVarHdnSeikyuusyoNo + varRow);
           
            PopupSyuusei(varHdn.value);
        }
        
        /*
        * 検索結果テーブル 各種レイアウト設定
        */
        function settingResultTable(type){
            var tableTitle1 = objEBI("<%=TableTitleTable1.clientID %>");    // ヘッダ部左テーブル
            var tableTitle2 = objEBI("<%=TableTitleTable2.clientID %>");    // ヘッダ部右テーブル
            var tableData1 = objEBI("<%=TableDataTable1.clientID %>");      // データ部左テーブル
            var tableData2 = objEBI("<%=TableDataTable2.clientID %>");      // データ部右テーブル
            var divTitle1 = objEBI("<%=DivLeftTitle.clientID %>");          // ヘッダ部左DIV
            var divTitle2 = objEBI("<%=DivRightTitle.clientID %>");         // ヘッダ部右DIV
            var divData1 = objEBI("<%=DivLeftData.clientID %>");            // データ部左DIV
            var divData2 = objEBI("<%=DivRightData.clientID %>");           // データ部右DIV
            var minHeight = 100;                                            // ウィンドウリサイズ時の検索結果テーブルに設定する最低高さ
            var adjustHeight = 50;                                          // 調整高さ(大きい程、検索結果テーブルが低くなる)
            var adjustWidth = 615;                                          // 調整幅(大きい程、検索結果テーブルが狭くなる)

            //固定列有りの検索結果テーブル用レイアウト等設定処理
            settingResultTableForColumnFix(type
                                            , tableTitle1
                                            , tableTitle2
                                            , tableData1
                                            , tableData2
                                            , divTitle1
                                            , divTitle2
                                            , divData1
                                            , divData2
                                            , minHeight
                                            , adjustHeight
                                            , adjustWidth)
        }
        
        /*******************************************
         * Allクリア処理後に実行されるファンクション
         *******************************************/
        function funcAfterAllClear(obj){
            objSeikyuuDateTo.value = objHdnPrmSeikyuuDateTo.value;
            objMaxSearchCount.selectedIndex = 1;
            objSeikyuuDateFrom.focus();
            objTorikesiTaisyou.click();
            if(objSendVal_GamenMode.value == "<%= EarthEnum.emSeikyuuSearchType.KakoSearchSeikyuusyo %>"){ //過去請求書一覧
                objInjiTaisyou.click();
            }
        }
        
        /***********************************
         * 「検索実行」押下時のチェック処理
         ***********************************/
        function checkJikkou(varAction){    
                        
            //画面表示部品セット
            setGlobalObj();
            
            var varMsg = "";
                        
            //請求書発行日Toが未入力 必須チェック
            if(objSeikyuuDateTo.value.Trim() == ""){
                varMsg = "<%= Messages.MSG013E %>";
                varMsg = varMsg.replace("@PARAM1","請求書発行日To");
                alert(varMsg);
                return false;
            }
            
            if(objSendVal_GamenMode.value == "<%= EarthEnum.emSeikyuuSearchType.SearchSeikyuusyo %>"){ //請求書一覧

                //明細件数 大小チェック
                if(!checkDaiSyou(objMeisaiKensuuFrom,objMeisaiKensuuTo,"明細件数")){return false};   
                       
            }
            
            //請求書式＆請求締め日 or 請求先コード　必須チェック                
            //請求先コードが未入力
            if(objSeikyuuSakiCd.value.Trim() == ""){
                
                //請求書式が未入力または請求締め日が未入力
                if(objSeikyuuSyosiki.value.Trim() == "" || objSeikyuuSimeDate.value.Trim() == "" ){
                     varMsg = "<%= Messages.MSG026E %>";
                     varMsg = varMsg.replace("@PARAM1","「請求書式かつ請求締め日」");
                     varMsg = varMsg.replace("@PARAM2","「請求先コード」");
                     alert(varMsg);
                     objSeikyuuSakiCd.focus();
                return false;
                }   
            }       
            
            //請求書発行日 大小チェック
            if(!checkDaiSyou(objSeikyuuDateFrom,objSeikyuuDateTo,"請求書発行日")){return false};
            
            if(varAction == gBtnSearch){
                //表示件数「500件」チェック
                if(objMaxSearchCount.value == "500"){
                    if(!confirm(("<%= Messages.MSG007C %>").replace("無制限","500件"))){return false};
                }
                //検索実行
                objSearch.click();
                
            }else if(varAction == gBtnCsv){
                if(ChkTaisyou(varAction) == false){return false;}
            
                objCsvOutPutFlg.value = "1";
                
                //検索実行
                objSearch.click();
            }
            return true;
        }
        
        /*********************
         * 大小チェック
         *********************/
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
        
        /**
         * スクロール同期
         * @return 
         */
        function syncScroll(type,obj){
            if(type==1){
                //左側スクロール時
                objEBI("<%=DivLeftTitle.clientID %>").scrollLeft=obj.scrollLeft;
                objEBI("<%=DivRightData.clientID %>").scrollTop=obj.scrollTop;
            }else if(type==2){
                //右側スクロール時
                objEBI("<%=DivRightTitle.clientID %>").scrollLeft=obj.scrollLeft;
                objEBI("<%=DivLeftData.clientID %>").scrollTop=obj.scrollTop;
            }
        }
        
        /**
         * 対象ALLチェックボックス・一括チェック処理
         * @return
         */
        function setCheckedAll(objChk){
            var tableData1 = objEBI("<%=TableDataTable1.clientID %>");
            var arrSakiTr = tableData1.tBodies[0].rows;
            var objTd = null;
            var arrInput = null;
            
            var objTmpId = null;
            var objTmpChk = null;
                        
            for ( var tri = 0; tri < arrSakiTr.length; tri++) {
                objTd = arrSakiTr[tri].cells[0];
                
                arrInput = getChildArr(objTd,"INPUT");
                for ( var ar = 0; ar < arrInput.length; ar++) {
                    if(arrInput[ar].type == "checkbox"){
                        objTmpId = arrInput[ar].id;
                        objTmpChk = objEBI(objTmpId);
                        objTmpChk.checked = objChk.checked;
                        if(tri == 0 && objChk.checked == true){ //先頭行かつ対象ALL=チェックの場合
                            selectedLineColor(objTd); //先頭行を選択
                        }
                    }
                }
            }
            return true;
        }
                
        /**
         * 対象チェックボックス・チェック状況戻し処理(CSV出力ボタン押下時処理)
         * @return
         */
        function setCheckedReturn(){
            //画面表示部品セット
            setGlobalObj();
               
            //該当データのチェックをする
            setCmnChecked(objSendVal_ChkedTaisyou.value,true);
        }
        
        /**
         * チェックボックス・共通チェック処理
         * (第一引数：ClientIDのセパレータ含む、第二引数：チェック有無)
         * @return
         */
        function setCmnChecked(StrClientID,blnChked){           
            var objTmpId = null;
            var objTmpChk = null;
                        
            if(StrClientID == ""){return false;}
            
            var arrChked = StrClientID.split(sepStr);
            
            for ( var tri = 0; tri < arrChked.length; tri++) {
                if(arrChked[tri] == "") continue;
                objTmpId = arrChked[tri];
                objTmpChk = objEBI(objTmpId);
                if(objTmpChk == null) continue;
                objTmpChk.checked = blnChked;
            }
        }
        
        /**
         * 対象チェックボックス・入力チェック処理
         * チェックがある場合、対象の請求書NO,更新日時,請求書用紙汎用コードフラグをHiddenに格納する
         * @return
         */
        function ChkTaisyou(varAction){
            //画面表示部品セット
            setGlobalObj();
            
            var tableData1 = objEBI("<%=TableDataTable1.clientID %>");
            var tableData2 = objEBI("<%=TableDataTable2.clientID %>");
            var arrSakiTr1 = tableData1.tBodies[0].rows;
            var arrSakiTr2 = tableData2.tBodies[0].rows;
            var objTd = null;
            var objTdSeikyuusyoNo = null;
            var arrInput = null;
            var bukkenCount = 0;
            var meisaiSumCnt = 0;
            var objTmpId = null;
            var objTmpSeiNo = null;
            var objPrintBtn = objEBI("<%=ButtonSeikyuusyoPrint.clientID %>");
            var objGonyuukinGaku = null;
            
            var ErrMsg = "";
            
            //Key情報をクリア
            ClearKeyInfo();

            for ( var tri = 0; tri < arrSakiTr1.length; tri++) {
                objTd = arrSakiTr1[tri].cells[0];
                
                //御入金額の取得
                objGonyuukinGaku = arrSakiTr2[tri].cells[4].innerHTML.replace(",","");
                
                arrInput = getChildArr(objTd,"INPUT");               
                for ( var ar = 0; ar < arrInput.length; ar++) {
                    if(arrInput[ar].type == "checkbox" && arrInput[ar].checked){
                        objTmpId = arrInput[ar].id; //checkbox対象
                        objSendVal_ChkedTaisyou.value += objTmpId + sepStr;
                        
                        objTmpId = arrInput[0].id; //hidden請求書NO
                        objTmpVal = objEBI(objTmpId);
                        objTmpSeiNo = objEBI(objTmpId);
                        objSendVal_SeikyuusyoNo.value += objTmpVal.value + sepStr;
                        
                        objTmpId = arrInput[1].id; //hidden更新日時
                        objTmpVal = objEBI(objTmpId);
                        objSendVal_UpdDatetime.value += objTmpVal.value + sepStr;
                        
                        objTmpId = arrInput[2].id; //hiddenCSV出力対象外                       
                        objTmpVal = objEBI(objTmpId);
                        if(objTmpVal.value == "1"){ //フラグがたっている場合
                            objTmpId = arrInput[ar].id; //checkbox対象
                            objSendVal_PrintTaisyougai.value += objTmpId + sepStr; 
                        }
                        
                        objTmpId = arrInput[3].id; //hidden取消フラグ
                        objTmpVal = objEBI(objTmpId);
                        if(objTmpVal.value == "1"){ //フラグがたっている場合
                            objTmpId = arrInput[ar].id; //checkbox対象
                            objSendVal_TorikesiTaisyougai.value += objTmpId + sepStr; 
                        }
                        
                        objTmpId = arrInput[4].id; //hidden書式フラグ
                        objTmpVal = objEBI(objTmpId);
                        if(objTmpVal.value == "<%= EarthConst.ISNULL %>"){ //未設定の場合
                            objTmpId = arrInput[ar].id; //checkbox対象
                            objSendVal_SyosikiTaisyougai.value += objTmpId + sepStr; 
                        }
                        
                        objTmpId = arrInput[5].id; //hidden明細件数
                        objTmpVal = objEBI(objTmpId);
                        meisaiSumCnt += eval(objTmpVal.value);
                        
                        //請求書印刷対象設定
                        if(objPrintBtn.value == "<%= EarthConst.BUTTON_SEIKYUUSYO_PRINT %>"){
                            //請求書一覧画面では明細件数0件の場合、印刷を行わない
                            if(objTmpVal.value != 0){
                                objSendVal_SeikyuusyoNoPrint.value += objTmpSeiNo.value + sepStr;
                            }
                        }else if(objPrintBtn.value == "<%= EarthConst.BUTTON_SEIKYUUSYO_RE_PRINT %>"){
                            //過去請求書一覧画面では明細件数0件でも入金額が0じゃなければ印刷を行う
                            if(objTmpVal.value != 0){
                                //明細がある場合
                                objSendVal_SeikyuusyoNoPrint.value += objTmpSeiNo.value + sepStr;
                            }else{
                                //明細がない場合
                                if(objGonyuukinGaku != 0){
                                    objSendVal_SeikyuusyoNoPrint.value += objTmpSeiNo.value + sepStr;
                                }
                            }
                        }
                        
                        bukkenCount++;
                    }
                }
            }
            //一つも選択されていなかった場合、エラー
            if(1 > bukkenCount){
                alert("<%= Messages.MSG140E %>");
                ClearKeyInfo();
                return false;
            }
            
            //CSV出力時
            if(varAction == gBtnCsv){
                //上限件数を超えていた場合、エラー
                if(bukkenCount > "<%= EarthConst.MAX_CSV_SELECT %>"){
                    alert("<%= Messages.MSG047E %>".replace("{0}",bukkenCount).replace("{1}","CSV出力").replace("{2}","<%= EarthConst.MAX_CSV_SELECT %>"));
                    ClearKeyInfo();
                    return false;
                }
                //CSV出力上限を超えていた場合メッセージを付与
                if(meisaiSumCnt > "<%= EarthConst.MAX_CSV_OUTPUT %>"){
                    objCsvMaxCntFlg.value = "1"
                }
            }
            
            if(objSendVal_GamenMode.value == "<%= EarthEnum.emSeikyuuSearchType.SearchSeikyuusyo %>"){ //請求書一覧
                //請求書印刷ボタン押下時
                if(varAction == gBtnPrint){
                    //取消対象外のチェック処理
                    ErrMsg = "<%= Messages.MSG160C %>".replace("@PARAM1","取消");
                    ErrMsg = ErrMsg.replace("@PARAM2","請求書NO");
                    ErrMsg = ErrMsg.replace("@PARAM2","請求書NO");
                    if(ChkSeikyuusyoPrint(objSendVal_TorikesiTaisyougai.value,ErrMsg) == false){
                        ClearKeyInfo();
                        return false;
                    }
                    //印刷対象外のチェック処理
                    ErrMsg = "<%= Messages.MSG160C %>".replace("@PARAM1","印刷対象外");
                    ErrMsg = ErrMsg.replace("@PARAM2","請求書NO");
                    ErrMsg = ErrMsg.replace("@PARAM2","請求書NO");
                    if(ChkSeikyuusyoPrint(objSendVal_PrintTaisyougai.value,ErrMsg) == false){
                        ClearKeyInfo();
                        return false;
                    }
                    //書式対象外のチェック処理
                    ErrMsg = "<%= Messages.MSG160C %>".replace("@PARAM1","請求書式未設定");
                    ErrMsg = ErrMsg.replace("@PARAM2","請求書NO");                    
                    ErrMsg = ErrMsg.replace("@PARAM2","請求書NO");
                    if(ChkSeikyuusyoPrint(objSendVal_SyosikiTaisyougai.value,ErrMsg) == false){
                        ClearKeyInfo();
                        return false;
                    }
                }
            }
            //EXCEL出力時
            if(varAction == gBtnExcel){
                if(objSendVal_SeikyuusyoNo.value == ""){
                    ClearKeyInfo();
                    return false;
                }
            }
            
            //末尾のセパレータを除去
            RemoveSepStr();     
            return true;
        }
        
        /**
         * 請求書印刷時のチェック共通処理
         * @return
         */
        function ChkSeikyuusyoPrint(StrClientID,varMsg){
            if(StrClientID == ""){return true;}
            
            var objTmpId = null;
            var objTmpChk = null;
            var arrChked = StrClientID.split(sepStr);
            var strDispNo = "";
            var strSep = "";
            
            //請求書NOを取得
            for ( var tri = 0; tri < arrChked.length; tri++) {
                if(arrChked[tri] == "") continue;
                objTmpId = arrChked[tri];
                
                //区切り文字
                if(tri == 0){
                    strSep = ",";
                }else if( (tri + 1) % 5 == 0){
                    strSep = "\n";
                }else{
                    strSep = ",";
                }
                strDispNo += objTmpId.replace(gVarSettouJi + gVarChkTaisyou,"") + strSep;
            }
            if(confirm(varMsg + strDispNo)){
                //該当データのチェックをはずす
                setCmnChecked(StrClientID,false);
            }else{
                objEBI(arrChked[0]).focus(); //フォーカス
            }
            return false;
        }
        
        /**
         * KEY情報をクリア処理
         * (請求書NO,更新日時,対象チェックボックス,CSV出力対象外)
         * @return
         */
        function ClearKeyInfo(){
            objSendVal_SeikyuusyoNo.value = "";
            objSendVal_SeikyuusyoNoPrint.value = "";
            objSendVal_UpdDatetime.value = "";
            objSendVal_ChkedTaisyou.value = "";
            objSendVal_PrintTaisyougai.value = "";
            objSendVal_TorikesiTaisyougai.value = "";
            objSendVal_SyosikiTaisyougai.value = "";
        }
        
        /**
         * KEY情報の末尾のセパレータ文字列を除去する処理
         * @return
         */
        function RemoveSepStr(){
            objSendVal_SeikyuusyoNo.value = objSendVal_SeikyuusyoNo.value.replace(/\$\$\$$/, "");
            objSendVal_SeikyuusyoNoPrint.value = objSendVal_SeikyuusyoNoPrint.value.replace(/\$\$\$$/, "");
            objSendVal_UpdDatetime.value = objSendVal_UpdDatetime.value.replace(/\$\$\$$/, "");
            objSendVal_ChkedTaisyou.value = objSendVal_ChkedTaisyou.value.replace(/\$\$\$$/, "");
            objSendVal_PrintTaisyougai.value = objSendVal_PrintTaisyougai.value.replace(/\$\$\$$/, "");
            objSendVal_TorikesiTaisyougai.value = objSendVal_TorikesiTaisyougai.value.replace(/\$\$\$$/, "");
            objSendVal_SyosikiTaisyougai.value = objSendVal_SyosikiTaisyougai.value.replace(/\$\$\$$/, "");
        }
        
        //子画面呼出処理
        function PopupSyuusei(strUniqueNo){    
            //オブジェクトの再読込(Ajax再描画対応)
            objSendTargetWin = objEBI("<%= sendTargetWin.clientID %>");
            if(objSendTargetWin.value == "")objSendTargetWin.value="<%=EarthConst.MAIN_WINDOW_NAME %>";
            
            //オープン対象の画面を指定
            varAction = "<%=UrlConst.SEIKYUUSYO_SYUUSEI %>";
            
            //<!-- 画面引渡し情報 -->
            objSendForm = objEBI("searchForm");
            //起動画面 + 請求書NO
            var objSendVal_SearchTerms = objEBI("sendSearchTerms");
            var strGamen = objSendVal_GamenMode.value + "<%=EarthConst.SEP_STRING %>";
            objSendVal_SearchTerms.value = strGamen + strUniqueNo;
                        
            var varWindowName = "SeikyuusyoSyuusei";
            objSrchWin = window.open("about:Blank", varWindowName, "menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes");
                        
            //値セット
            objSendTargetWin.value = varWindowName;
            objSendForm.target=objSendTargetWin.value;
                            
            objSendForm.action = varAction;
            objSendForm.submit();
        }
        
        //PDF出力呼出処理
        function PdfOutput(){               
            //オブジェクトの再読込(Ajax再描画対応)
            objSendTargetWin = objEBI("<%= sendTargetWin.clientID %>");
            if(objSendTargetWin.value == "")objSendTargetWin.value="<%=EarthConst.MAIN_WINDOW_NAME %>";
            
            //オープン対象の画面を指定
            varAction = "<%=UrlConst.EARTH2_SEIKYUSYO_FCW_OUTPUT %>";
            
            //<!-- 画面引渡し情報 -->
            objSendForm = objEBI("searchForm");
            //起動画面 + 請求書NO
            var objSendVal_SeiNo = objEBI("seino");
            objSendVal_SeiNo.value = objSendVal_SeikyuusyoNoPrint.value;
                        
            var varWindowName = "SeikyusyoFcwOutput";
            objSrchWin = window.open("about:Blank", varWindowName);
                        
            //値セット
            objSendTargetWin.value = varWindowName;
            objSendForm.target=objSendTargetWin.value;
                            
            objSendForm.action = varAction;
            objSendForm.submit();
        }

        //EXCEL出力呼出処理
        function ExcelOutput(){
            //対象チェックボックスのチェック処理
            if(ChkTaisyou(gBtnExcel) == false) return false;
            
            var tmpSeiNo = objSendVal_SeikyuusyoNo.value.split(sepStr);           
            if(tmpSeiNo.length >= 1){
                tmpSeiNo = tmpSeiNo[0];
            }else{
                return false;
            }
            var varUrl = "<%=UrlConst.EARTH2_SEIKYUUSYO_EXCEL_OUTPUT_TESTPAGE %>";
            var varPrm = "?seino=" + tmpSeiNo;
            window.open(varUrl + varPrm);
        }
    </script>

    <input type="hidden" id="HiddenCsvOutPut" runat="server" /><%--CSV出力判断--%>
    <input type="hidden" id="HiddenCsvMaxCnt" runat="server" /><%--CSV出力上限件数フラグ--%>
    <input type="hidden" id="HiddenPrmSeikyuusyoHakDateTo" runat="server" /><%--パラメータ請求書発行日To--%>
    <input type="hidden" id="HiddenSendValSeikyuusyoNo" runat="server" /><%--請求書No--%>
    <input type="hidden" id="HiddenSendValSeikyuusyoNoPrint" runat="server" /><%--請求書No印刷用--%>
    <input type="hidden" id="HiddenSendValUpdDatetime" runat="server" /><%--更新日時--%>
    <input type="hidden" id="HiddenChkedTaisyou" runat="server" /><%--チェック済み対象チェックボックス--%>
    <input type="hidden" id="HiddenPrintTaisyougai" runat="server" /><%--印刷対象外の請求書NO--%>
    <input type="hidden" id="HiddenTorikesiTaisyougai" runat="server" /><%--取消による対象外の請求書NO--%>
    <input type="hidden" id="HiddenSyosikiTaisyougai" runat="server" /><%--請求書式未設定による対象外の請求書NO--%>
    <input type="hidden" id="HiddenGamenMode" runat="server" />
    <%-- 画面タイトル --%>
    <table>
        <tr>
            <td>
                <table style="text-align: left;" border="0" cellpadding="0" cellspacing="2" class="titleTable">
                    <tr>
                        <th style="text-align: left; width: 160px;">
                            <span id="SpanTitle" runat="server"></span>
                        </th>
                        <th>
                            <input type="button" id="ButtonReturn" value="戻る" runat="server" style="width: 100px;" tabindex="10" />&nbsp;
                        </th>
                        <th>
                            <input type="button" id="ButtonSeikyuusyoPrint" runat="server" style="background-color: #ffff69; width: 240px;"
                                tabindex="10" /><input type="button" id="ButtonHiddenPrint" value="印刷実行btn" style="display: none;"
                                    runat="server" />&nbsp;
                        </th>
                        <th style="width: 160px">
                            <input  type="button" id="ButtonMiinsatu" value="未印刷一覧" runat="server"
                                style="background-color: #ee5522; color: White; font-weight: bold; width: 150px;" tabindex="10"/>&nbsp;
                        </th>
                        <th style="width: 160px">
                            <input type="button" id="ButtonExcelOutput" value="Excel出力" style="width: 100px;" tabindex="10" onclick="ExcelOutput()"/>&nbsp;
                        </th>     
                        <th style="width: 270px;" align="right">
                            <input type="button" id="ButtonSeikyuusyoTorikesi" value="請求書取消" runat="server"
                                style="background-color: fuchsia; width: 100px;" tabindex="10" /><input type="button" id="ButtonHiddenTorikesi"
                                    value="取消実行btn" style="display: none" runat="server" />&nbsp;
                        </th>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <table style="text-align: left;" class="mainTable" cellpadding="2">
        <thead>
            <tr>
                <th class="tableTitle" colspan="6" rowspan="1">
                    検索条件
                    <input id="btnClearWin" value="クリア" type="reset" class="button" tabindex="20" /></th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td class="koumokuMei">
                    請求書発行日</td>
                <td colspan="5">
                    <input id="TextSekyuusyoHakDateFrom" runat="server" maxlength="10" class="date" onblur="checkDate(this);"
                        tabindex="20" />&nbsp;～&nbsp;<input id="TextSekyuusyoHakDateTo" runat="server" maxlength="10"
                            onblur="checkDate(this);" class="date readOnlyStyle" readonly="readonly" tabindex="-1" /></td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    請求先</td>
                <td colspan="3">
                    <asp:UpdatePanel ID="UpdatePanelSeikyuusaki" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <ContentTemplate>
                            <asp:DropDownList ID="SelectSeikyuuSakiKbn" runat="server" TabIndex="30">
                            </asp:DropDownList>
                            <input id="TextSeikyuuSakiCd" runat="server" maxlength="5" style="width: 35px;" class="codeNumber"
                                tabindex="30" />&nbsp;-
                            <input id="TextSeikyuuSakiBrc" runat="server" maxlength="2" style="width: 15px;"
                                class="codeNumber" tabindex="30" />
                            <input id="btnSeikyuuSakiSearch" runat="server" type="button" value="検索" class="gyoumuSearchBtn"
                                tabindex="30" onserverclick="btnSeikyuuSakiSearch_ServerClick" />&nbsp;
                            <input id="TextSeikyuuSakiMei" runat="server" class="readOnlyStyle" readonly="readonly"
                                style="width: 190px" tabindex="-1" />&nbsp;
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="koumokuMei">
                    請求先名カナ</td>
                <td class="codeNumber">
                    <input id="TextSeikyuuSakiMeiKana" runat="server" maxlength="40" style="ime-mode: active;
                        width: 320px;" tabindex="30" />
                </td>
            </tr>
            <tr id="TrSearchArea" runat="server">
                <td class="koumokuMei">
                    請求書式</td>
                <td id="TdSelectSeikyuuSyousiki">
                    <asp:DropDownList ID="SelectSeikyuuSyousiki" runat="server" TabIndex="40">
                    </asp:DropDownList>
                </td>
                <td class="koumokuMei" id="KoumokumeiSeikyuuSimeDate">
                    請求締め日</td>
                <td id="TdSeikyuuSimeDate">
                    <input id="TextSeikyuuSimeDate" runat="server" maxlength="2" class="date" style="width: 30px"
                        onblur="checkDateDD(this);" tabindex="40" /></td>
                <td id="KoumokumeiSearchMeisaiKensuu" class="koumokuMei">
                    明細件数</td>
                <td id="TdTextMeisaiKensuu">
                    <input id="TextMeisaiKensuuFrom" runat="server" maxlength="4" class="number" style="width: 40px;"
                        tabindex="40" onblur="checkNumberAddFig(this);checkMinus(this);" />&nbsp;～&nbsp;<input
                            id="TextMeisaiKensuuTo" runat="server" maxlength="4" class="number" style="width: 40px;"
                            tabindex="40" onblur="checkNumberAddFig(this);checkMinus(this);" />
                </td>
            </tr>
            <tr>
                <td style="text-align: center;" colspan="6" rowspan="1">
                    検索上限件数<select id="maxSearchCount" runat="server" tabindex="50">
                        <option value="10">10件</option>
                        <option value="100" selected="selected">100件</option>
                        <option value="500">500件</option>
                    </select>
                    <input id="btnSearch" value="検索実行" type="button" runat="server" tabindex="50" style="padding-top: 2px;" />
                    <input type="button" id="search" value="検索実行btn" style="display: none" runat="server"
                        tabindex="-1" />
                    <input type="button" id="ButtonCsv" value="CSV出力" runat="server" tabindex="50" />
                    <input type="button" id="ButtonHiddenCsv" value="CSV実行btn" style="display: none"
                        runat="server" tabindex="-1" />
                    <input id="CheckTorikesiTaisyou" value="0" type="checkbox" runat="server" checked="checked"
                        tabindex="50" />取消は検索対象外 <span id="SpanInjiYousi" runat="server">
                            <input id="CheckInjiTaisyou" value="0" type="checkbox" runat="server" checked="checked"
                                tabindex="50" />印字対象外の用紙は検索対象外 </span>
                </td>
            </tr>
        </tbody>
    </table>
    <table style="height: 30px">
        <tr id="TrMihakkou" runat="server">
            <td>
                未発行件数：
            </td>
            <td id="TdMihakkou" runat="server">
            </td>
            <td>
                件
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                検索結果：
            </td>
            <td id="resultCount" runat="server">
            </td>
            <td>
                件
            </td>
            <td id="TdMsgArea" runat="server" style="color: red; font-weight: bold;">
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0">
        <thead>
            <tr>
                <th style="text-align: left;">
                    <div id="DivLeftTitle" runat="server" class="scrollDivLeftTitleStyle2">
                        <table cellpadding="0" cellspacing="0" id="TableTitleTable1" runat="server" class="scrolltablestyle2 sortableTitle"
                            style="border-top: 1px solid #999999; border-left: 1px solid #999999;">
                            <thead>
                                <tr id="meisaiTableHeaderTr" runat="server" style="height: 20px;">
                                    <th style="width: 50px;" class="unsortable">
                                        対象<input id="CheckAll" type="checkbox" runat="server" onclick="setCheckedAll(this);"
                                            onfocus="this.select()" tabindex="50" /></th>
                                    <th style="width: 120px;">
                                        請求書NO</th>
                                    <th style="width: 40px;">
                                        取消</th>
                                    <th style="width: 90px;">
                                        請求先コード</th>
                                    <th style="width: 140px;">
                                        請求先名</th>
                                    <th style="width: 140px;">
                                        請求先名２</th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </th>
                <th style="text-align: left;">
                    <div id="DivRightTitle" runat="server" class="scrollDivRightTitleStyle2" style="border-right: 1px solid #999999;">
                        <table cellpadding="0" cellspacing="0" id="TableTitleTable2" runat="server" class="scrolltablestyle2 sortableTitle"
                            style="border-top: 1px solid #999999;">
                            <thead>
                                <tr style="height: 20px;">
                                    <th style="width: 90px;">
                                        請求書発行日</th>
                                    <th style="width: 75px;">
                                        請求締め日</th>
                                    <th style="width: 90px;">
                                        入金予定日</th>
                                    <th style="width: 90px;">
                                        前回御請求額</th>
                                    <th style="width: 90px;">
                                        御入金額</th>
                                    <th style="width: 90px;">
                                        前回繰越残高</th>
                                    <th style="width: 110px;">
                                        今回御請求額</th>
                                    <th style="width: 100px;">
                                        繰越残高</th>
                                    <th style="width: 70px;">
                                        郵便番号</th>
                                    <th style="width: 90px;">
                                        電話番号</th>
                                    <th style="width: 160px;">
                                        住所1</th>
                                    <th style="width: 160px;">
                                        住所2</th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td style="vertical-align: top;">
                    <div id="DivLeftData" runat="server" class="scrollLeftDivStyle2" onscroll="syncScroll(1,this);"
                        onmousewheel="wheel(event,this);">
                        <table cellpadding="0" cellspacing="0" id="TableDataTable1" runat="server" class="scrolltablestyle2 sortableData">
                        </table>
                    </div>
                </td>
                <td style="vertical-align: top;">
                    <div id="DivRightData" runat="server" class="scrollDivStyle2" onscroll="syncScroll(2,this);"
                        style="border-right: 1px solid #999999;">
                        <table cellpadding="0" cellspacing="0" id="TableDataTable2" runat="server" class="scrolltablestyle2 sortableData">
                        </table>
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
    <input type="hidden" id="sendTargetWin" runat="server" />
</asp:Content>

<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    CodeBehind="SearchMousikomi.aspx.vb" Inherits="Itis.Earth.WebUI.SearchMousikomi" 
    title="EARTH 申込検索" %>
    
<%@ Import Namespace="Itis.Earth.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script src="js/sortable_sync_ja.js" type="text/javascript">
    </script>

    <script type="text/javascript">
        history.forward();
       
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
        //コントロール接頭辞
        var gVarSettouJi = "ctl00_CPH1_"; 
        var gVarTr1 = "DataTable_resultTr1_";
        var gVarTr2 = "DataTable_resultTr2_";
        var gVarTdSentou = "DataTable_Sentou_Td_";
        var gVarHdnMousikomiNo = "HdnMousikomiNo_";
        var gVarChkTaisyou = "ChkTaisyou_";
        
        //画面表示部品
        var objMousikomiNoFrom = null;
        var objMousikomiNoTo = null;
        var objMousikomiDateFrom = null;
        var objMousikomiDateTo = null;
        var objKameitenCd = null;
        var objIraiDateFrom = null;
        var objIraiDateTo = null;
        var objBukkenMeisyou = null;
        var objDoujiIraiTousuuFrom = null;
        var objDoujiIraiTousuuTo = null;
        var objKubun = null;
        var objHosyousyoNoFrom = null;
        var objHosyousyoNoTo = null;
        var objStatus = null;
        var objTysKaisyaSearchTaisyou = null;

        //検索実行用
        var objMaxSearchCount = null;
        var objSearch = null;

        //画面遷移用
        var objSendVal_MousikomiNo = null;
        var objSendVal_UpdDatetime = null;
        var objSendVal_ChkedTaisyou = null;
　      var objSendVal_JutyuuZumi = null;
　      var objSendVal_DoujiIraiTousuu = null;
        var objSendVal_TyoufukuAri = null;
     
        //アクション実行ボタン(検索実行,新規受注)
        var gBtnSearch = null;
        var gBtnJutyuu = null;

        //該当データなしメッセージ制御用フラグ
        var gNoDataMsgFlg = null;

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
            
            /* 画面特有の表示切替処理 */
            ps_chgDisp();
        }
        
        /*********************************************
        * 画面特有の表示切替処理
        *********************************************/
        function ps_chgDisp(){
            /* 調査会社未決定チェックボックス表示切替 */
            if(objStatus.value == '200'){
                objTysKaisyaSearchTaisyou.disabled = false;
            }else{
                objTysKaisyaSearchTaisyou.disabled = true;
            }        
        }
        
        /*********************************************
        * 戻り値がない為、同メソッドをオーバーライド
        *********************************************/
        function returnSelectValue(){
            return false;
        }
        
        /*********************************************
        * 画面表示部品オブジェクトをグローバル変数化
        *********************************************/
        function setGlobalObj() {
            //画面表示部品
            objMousikomiNoFrom = objEBI("<%= TextMousikomiNoFrom.clientID %>");
            objMousikomiNoTo = objEBI("<%= TextMousikomiNoTo.clientID %>");
            objMousikomiDateFrom = objEBI("<%= TextMousikomiDateFrom.clientID %>");
            objMousikomiDateTo = objEBI("<%= TextMousikomiDateTo.clientID %>");
            objKameitenCd = objEBI("<%= TextKameitenCd.clientID %>");
            objIraiDateFrom = objEBI("<%= TextIraiDateFrom.clientID %>");
            objIraiDateTo = objEBI("<%= TextIraiDateTo.clientID %>");
            objBukkenMeisyou = objEBI("<%= TextBukkenMeisyou.clientID %>");
            objDoujiIraiTousuuFrom = objEBI("<%= TextDoujiIraiTousuuFrom.clientID %>");
            objDoujiIraiTousuuTo = objEBI("<%= TextDoujiIraiTousuuTo.clientID %>");
            objKubun = objEBI("<%= SelectKbn.clientID %>");
            objHosyousyoNoFrom = objEBI("<%= TextHosyousyoNoFrom.clientID %>");
            objHosyousyoNoTo = objEBI("<%= TextHosyousyoNoTo.clientID %>");
            objStatus = objEBI("<%= SelectStatus.clientID %>");
            objTysKaisyaSearchTaisyou = objEBI("<%= CheckTysKaisyaSearchTaisyou.clientID %>");
            
            //検索実行用
            objMaxSearchCount = objEBI("<%= maxSearchCount.clientID %>");
            objSearch = objEBI("<%= search.clientID %>");
            
            //画面遷移用
            objSendVal_MousikomiNo = objEBI("<%= HiddenSendValMousikomiNo.clientID %>");
            objSendVal_UpdDatetime = objEBI("<%= HiddenSendValUpdDatetime.clientID %>");
            objSendVal_ChkedTaisyou = objEBI("<%= HiddenChkedTaisyou.clientID %>");
            objSendVal_JutyuuZumi = objEBI("<%= HiddenJutyuuZumi.clientID %>");
            objSendVal_DoujiIraiTousuu = objEBI("<%= HiddenDoujiIraiTousuu.clientID %>");
            objSendVal_TyoufukuAri = objEBI("<%= HiddenTyoufukuAri.clientID %>");
            
            //アクション実行ボタン
            gBtnSearch = "<%= EarthEnum.emSearchMousikomiBtnType.Search %>";
            gBtnJutyuu = "<%= EarthEnum.emSearchMousikomiBtnType.SinkiJutyuu %>";

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
            
            var varHdn = _d.getElementById(gVarSettouJi + gVarHdnMousikomiNo + varRow);
           
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
            var minHeight = 120;                                            // ウィンドウリサイズ時の検索結果テーブルに設定する最低高さ
            var adjustHeight = 50;                                          // 調整高さ(大きい程、検索結果テーブルが低くなる)
            var adjustWidth = 580;                                          // 調整幅(大きい程、検索結果テーブルが狭くなる)

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
            objStatus.selectedIndex = 0;
            objMaxSearchCount.selectedIndex = 1;
            objTysKaisyaSearchTaisyou.checked = false;          
            objMousikomiNoFrom.focus();
            
            ps_chgDisp();
        }
        
        /***********************************
        * 「検索実行」押下時のチェック処理
        ***********************************/
        function checkJikkou(varAction){    
                        
            //画面表示部品セット
            setGlobalObj();
            
            var varMsg = "";
            
            //申込NO 大小チェック
            if(!checkDaiSyou(objMousikomiNoFrom,objMousikomiNoTo,"申込NO")){return false};   
           
            //申込日 大小チェック
            if(!checkDaiSyou(objMousikomiDateFrom,objMousikomiDateTo,"申込日")){return false};   

            //依頼日 大小チェック
            if(!checkDaiSyou(objIraiDateFrom,objIraiDateTo,"依頼日")){return false};   
            
            //同時依頼棟数 大小チェック
            if(!checkDaiSyou(objDoujiIraiTousuuFrom,objDoujiIraiTousuuTo,"同時依頼棟数")){return false};   

            //番号 大小チェック
            if(!checkDaiSyou(objHosyousyoNoFrom,objHosyousyoNoTo,"番号")){return false};   

            if(varAction == gBtnSearch){
                //表示件数「無制限」チェック
                if(objMaxSearchCount.value == "max"){
                    if(!confirm(("<%= Messages.MSG007C %>")))return false;
                }
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
        * 対象チェックボックス・チェック状況戻し処理(新規受注ボタン押下時処理)
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
        * チェックがある場合、対象の申込NO,更新日時をHiddenに格納する
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
            var objTdMousikomiNo = null;
            var arrInput = null;
            var bukkenCount = 0;
            var objTmpId = null;
            var objTmpMouNo = null;
            
            var ErrMsg = "";
      
            //Key情報をクリア
            ClearKeyInfo();

            for ( var tri = 0; tri < arrSakiTr1.length; tri++) {
                objTd = arrSakiTr1[tri].cells[0];

                arrInput = getChildArr(objTd,"INPUT");  
           
                for ( var ar = 0; ar < arrInput.length; ar++) {
                    if(arrInput[ar].type == "checkbox" && arrInput[ar].checked){
                        objTmpId = arrInput[ar].id; //checkbox対象
                        objSendVal_ChkedTaisyou.value += objTmpId + sepStr;
                        
                        objTmpId = arrInput[0].id; //hidden申込NO
                        objTmpVal = objEBI(objTmpId);
                        objTmpMouNo = objEBI(objTmpId);
                        objSendVal_MousikomiNo.value += objTmpVal.value + sepStr;
                        
                        objTmpId = arrInput[1].id; //hidden更新日時
                        objTmpVal = objEBI(objTmpId);
                        objSendVal_UpdDatetime.value += objTmpVal.value + sepStr;
                        
                        objTmpId = arrInput[2].id; //hidden受注済
                        objTmpVal = objEBI(objTmpId);
                        if(objTmpVal.value > "<%= EarthConst.MOUSIKOMI_STATUS_MI_JUTYUU %>"){ //受注済の場合
                            objTmpId = arrInput[ar].id; //checkbox対象
                            objSendVal_JutyuuZumi.value += objTmpId + sepStr; 
                        }
                        
                        objTmpId = arrInput[3].id; //hidden同時依頼棟数
                        objTmpVal = objEBI(objTmpId);

                        if(objTmpVal.value > "<%= EarthConst.MOUSIKOMI_DOUJI_IRAI_1_TOU %>"){ //同時依頼棟数が1棟より大きい場合(null以外)
                            if(objTmpVal.value != "isnull"){
                                objTmpId = arrInput[ar].id; //checkbox対象
                                objSendVal_DoujiIraiTousuu.value += objTmpId + sepStr; 
                            }
                        }

                        objTmpId = arrInput[4].id; //hidden重複物件                       
                        objTmpVal = objEBI(objTmpId);
                        if(objTmpVal.value == "<%= EarthConst.TYOUFUKU_ARI %>"){ //重複物件がある場合
                                objTmpId = arrInput[ar].id; //checkbox対象
                                objSendVal_TyoufukuAri.value += objTmpId + sepStr; 
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
            //新規受注時
            if(varAction == gBtnJutyuu){
　              //上限物件数を超えていた場合、エラー
                if(bukkenCount > objEBI("<%=HiddenSinkiJutyuuMax.clientID %>").value){
                    alert("<%= Messages.MSG124E %>".replace("{0}",bukkenCount).replace("{1}",objEBI("<%=HiddenSinkiJutyuuMax.clientID %>").value).replace("一括変更","新規受注"));
                    ClearKeyInfo();
                    return false;
                }
                //受注済のチェック処理
                ErrMsg = "<%= Messages.MSG160C %>".replace("@PARAM1","受注済");
                ErrMsg = ErrMsg.replace("@PARAM2","申込NO");
                ErrMsg = ErrMsg.replace("@PARAM2","申込NO");
                if(ChkSinkiJutyuu(objSendVal_JutyuuZumi.value,ErrMsg) == false){
                    ClearKeyInfo();
                    return false;
                }
                //同時依頼棟数のチェック処理
                ErrMsg = "<%= Messages.MSG225C %>".replace("@PARAM1","同時依頼棟数が2棟以上");
                ErrMsg = ErrMsg.replace("@PARAM2","申込NO");
                ErrMsg = ErrMsg.replace("@PARAM2","申込NO");
                if(CfmSinkiJutyuu(objSendVal_DoujiIraiTousuu.value,ErrMsg) == false){
                    ClearKeyInfo();
                    return false;
                }
                //重複物件のチェック処理
                ErrMsg = "<%= Messages.MSG160C %>".replace("@PARAM1","重複物件あり");
                ErrMsg = ErrMsg.replace("@PARAM2","申込NO");
                ErrMsg = ErrMsg.replace("@PARAM2","申込NO");
                if(ChkSinkiJutyuu(objSendVal_TyoufukuAri.value,ErrMsg) == false){
                    ClearKeyInfo();
                    return false;
                }
                
            }
             //末尾のセパレータを除去
            RemoveSepString();     
            return true;
        }
        
         /**
         * 新規受注時のチェック処理
         * @return
         */
        function ChkSinkiJutyuu(StrClientID,varMsg){
            if(StrClientID == ""){return true;}
            
            var objTmpId = null;
            var objTmpChk = null;
            var arrChked = StrClientID.split(sepStr);
            var strDispNo = "";
            var strSep = "";
            
            //申込NOを取得
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
         * 新規受注時の確認処理
         * @return
         */
        function CfmSinkiJutyuu(StrClientID,varMsg){
            if(StrClientID == ""){return true;}
            
            var objTmpId = null;
            var objTmpChk = null;
            var arrChked = StrClientID.split(sepStr);
            var strDispNo = "";
            var strSep = "";
            
            //申込NOを取得
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
                // 処理継続
                // setCmnChecked(StrClientID,false);
                return true;
            }else{
                objEBI(arrChked[0]).focus(); //フォーカス
            }
            return false;
        }
       
         /**
         * KEY情報をクリア処理
         * (申込NO,更新日時,対象チェックボックス)
         * @return
         */
        function ClearKeyInfo(){
            objSendVal_MousikomiNo.value = "";
            objSendVal_UpdDatetime.value = "";
            objSendVal_ChkedTaisyou.value = "";
            objSendVal_JutyuuZumi.value = "";
            objSendVal_DoujiIraiTousuu.value = "";
            objSendVal_TyoufukuAri.value = "";   
        }
        
         /**
         * KEY情報の末尾のセパレータ文字列を除去する処理
         * @return
         */
        function RemoveSepString(){
            objSendVal_MousikomiNo.value = RemoveSepStr(objSendVal_MousikomiNo.value);
            objSendVal_UpdDatetime.value = RemoveSepStr(objSendVal_UpdDatetime.value);
            objSendVal_ChkedTaisyou.value = RemoveSepStr(objSendVal_ChkedTaisyou.value.value);
            objSendVal_JutyuuZumi.value = RemoveSepStr(objSendVal_JutyuuZumi.value);
            objSendVal_DoujiIraiTousuu.value = RemoveSepStr(objSendVal_DoujiIraiTousuu.value);
            objSendVal_TyoufukuAri.value = RemoveSepStr(objSendVal_TyoufukuAri.value);
        }

        //子画面呼出処理
        function PopupSyuusei(strUniqueNo){    
            //オブジェクトの再読込(Ajax再描画対応)
            objSendTargetWin = objEBI("<%= sendTargetWin.clientID %>");
            if(objSendTargetWin.value == "")objSendTargetWin.value="<%=EarthConst.MAIN_WINDOW_NAME %>";
            
            //オープン対象の画面を指定
            varAction = "<%=UrlConst.MOUSIKOMI_SYUUSEI %>";
            
            //<!-- 画面引渡し情報 -->
            objSendForm = objEBI("searchForm");
            //申込NO
            var objSendVal_SearchTerms = objEBI("sendSearchTerms");
            objSendVal_SearchTerms.value = strUniqueNo;
                        
            var varWindowName = "MousikomiSyuusei";
            objSrchWin = window.open("about:Blank", varWindowName, "menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes");
                        
            //値セット
            objSendTargetWin.value = varWindowName;
            objSendForm.target=objSendTargetWin.value;
                            
            objSendForm.action = varAction;
            objSendForm.submit();
        }
                
        /**
        * 申込NO,番号,同時依頼棟数 To自動セット
        * @return true/false
        */
        function setFromTo(obj){
            if(obj.id == objMousikomiNoFrom.id && objMousikomiNoTo.value == ""){
                objMousikomiNoTo.value = obj.value;
                objMousikomiNoTo.select();
            }else if(obj.id == objHosyousyoNoFrom.id && objHosyousyoNoTo.value == ""){
                objHosyousyoNoTo.value = obj.value;
                objHosyousyoNoTo.select();
            }else if(obj.id == objDoujiIraiTousuuFrom.id && objDoujiIraiTousuuTo.value == ""){
                objDoujiIraiTousuuTo.value = obj.value;
                objDoujiIraiTousuuTo.select();
            }
            return true;
        }
        
        /*********************
         * 加盟店情報クリア
         *********************/
        function clrKameitenInfo(obj){
            if(obj.value == ""){
                //値のクリア
                objEBI("<%= TextKameitenMei.clientID %>").value = "";
                objEBI("<%= TextTorikesiRiyuu.clientID %>").value = "";
                //色をデフォルトへ
                objEBI("<%= TextKameitenCd.clientID %>").style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>"
            }
        }
                
    </script>

    <input type="hidden" id="HiddenSendValMousikomiNo" runat="server" /><%--申込No--%>
    <input type="hidden" id="HiddenSendValUpdDatetime" runat="server" /><%--更新日時--%>
    <input type="hidden" id="HiddenChkedTaisyou" runat="server" /><%--チェック済み対象チェックボックス--%>   
    <input type="hidden" id="HiddenJutyuuZumi" runat="server" /><%--受注済の申込NO--%>   
    <input type="hidden" id="HiddenDoujiIraiTousuu" runat="server" /><%--同時依頼棟数が1以外の申込NO--%>   
    <input type="hidden" id="HiddenTyoufukuAri" runat="server" /><%--重複物件有の申込NO--%>   
    <%-- 画面タイトル --%>
    <table style="text-align: left;" border="0" cellpadding="0" cellspacing="2" class="titleTable">
        <tr>
            <th style="text-align: left; width: 700px;">
                申込検索</th>
            <th>
                <input type="checkbox" id="CheckTysKaisyaTaisyou" runat="server" tabindex="10" />調査会社未決定
            </th>
            <th>
                <input type="hidden" id="HiddenSinkiJutyuuMax" runat="server" />
                <input type="button" id="ButtonSinkiJutyuu" value="新規受注" runat="server" style="font-weight: bold; 
                    font-size: 18px; width: 100px; color: black; height: 30px; background-color: fuchsia;"
                    tabindex="10" />
            </th>
        </tr>
    </table>
    <br />
    <table style="text-align: left;" class="mainTable" cellpadding="2">
        <thead>
            <tr>
                <th class="tableTitle" colspan="12" rowspan="1">
                    検索条件
                    <input id="btnClearWin" value="クリア" type="reset" class="button" tabindex="20" /></th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td class="koumokuMei">
                    申込NO</td>
                <td colspan="3">
                    <input id="TextMousikomiNoFrom" runat="server" maxlength="15" style="width: 110px;" class="codeNumber"
                        onblur="checkNumber(this);" onchange="if(checkNumber(this))setFromTo(this);"
                        tabindex="20" />&nbsp;～&nbsp;<input id="TextMousikomiNoTo" runat="server" maxlength="15" style="width: 110px;"
                        class="codeNumber" onblur="checkNumber(this);" tabindex="20" /></td>
                <td class="koumokuMei" style="width: 70px;">
                    申込日</td>
                <td colspan="3">
                    <input id="TextMousikomiDateFrom" runat="server" maxlength="10" style="width: 70px;" class="date" onblur="checkDate(this);"
                        tabindex="20" />&nbsp;～&nbsp;<input id="TextMousikomiDateTo" runat="server" maxlength="10" style="width: 70px;"
                        class="date" onblur="checkDate(this);" tabindex="20" /></td>
                <td class="koumokuMei">
                    依頼日</td>
                <td colspan="3">
                    <input id="TextIraiDateFrom" runat="server" maxlength="10" style="width: 70px;" class="date" onblur="checkDate(this);"
                        tabindex="20" />&nbsp;～&nbsp;<input id="TextIraiDateTo" runat="server" maxlength="10" style="width: 70px;"
                        class="date" onblur="checkDate(this);" tabindex="20" /></td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    加盟店</td>
                <td colspan="3">
                    <asp:UpdatePanel ID="UpdatePanel_kameiten" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <ContentTemplate>
                            <input id="TextKameitenCd" runat="server" maxlength="5" style="width: 40px;" class="codeNumber"
                                tabindex="30" />
                            <input id="ButtonKameitenSearch" runat="server" type="button" value="検索" class="gyoumuSearchBtn"
                                 tabindex="30" />&nbsp;
                            <input id="TextKameitenMei" runat="server" class="readOnlyStyle" readonly="readonly"
                                style="width: 235px" tabindex="-1" />&nbsp;
                            <input type="hidden" id="Hidden1" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="koumokuMei">
                    取消</td>
                <td colspan="7">
                    <asp:UpdatePanel ID="UpdatePanel_kameiToikesi" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ButtonKameitenSearch" />
                        </Triggers>
                        <ContentTemplate>
                            <input id="TextTorikesiRiyuu" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    物件名称</td>
                <td colspan="3">
                    <input id="TextBukkenMeisyou" runat="server" maxlength="50" style="width: 333px;" tabindex="30"/></td>
                <td class="koumokuMei">
                    同時依頼棟数</td>
                <td colspan="3">
                    <input id="TextDoujiIraiTousuuFrom" runat="server" maxlength="2" style="width: 50px;" class="number" onblur="checkNumber(this);"
                        onchange="if(checkNumber(this))setFromTo(this);" tabindex="30" />&nbsp;～&nbsp;<input id="TextDoujiIraiTousuuTo" runat="server"
                        maxlength="2" style="width: 50px;" class="number" onblur="checkNumber(this);" tabindex="30" />棟</td>
                <td class="koumokuMei">
                    番号</td>
                <td colspan="3">
                    <input id="TextHosyousyoNoFrom" runat="server" maxlength="10" style="width: 75px;" class="codeNumber"
                        onblur="checkNumber(this);" onchange="if(checkNumber(this))setFromTo(this);"
                        tabindex="40" />&nbsp;～&nbsp;<input id="TextHosyousyoNoTo" runat="server" maxlength="10" style="width: 75px;"
                        class="codeNumber" onblur="checkNumber(this);" tabindex="40" /></td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    区分</td>
                <td colspan="11">
                    <asp:DropDownList ID="SelectKbn" runat="server" TabIndex="40">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: center;" colspan="12" rowspan="1">
                    受注状況<select id="SelectStatus" runat="server" tabindex="50" onchange="ps_chgDisp()">
                        <option value="100" selected="selected">未受注</option>
                        <option value="150">保留</option>
                        <option value="200">受注済</option>
                    </select>
                    検索上限件数<select id="maxSearchCount" runat="server" tabindex="50">
                        <option value="10">10件</option>
                        <option value="100" selected="selected">100件</option>
                        <option value="max">無制限</option>
                    </select>
                    <input id="btnSearch" value="検索実行" type="button" runat="server" tabindex="50" style="padding-top: 2px;" />
                    <input type="button" id="search" value="検索実行btn" style="display: none" runat="server"
                        tabindex="-1" />
                    <input id="CheckTysKaisyaSearchTaisyou" type="checkbox" runat="server" tabindex="50" />調査会社未決定
                </td>
            </tr>
        </tbody>
     </table>
     <table style="height: 30px">
        <tr>
            <td>
                検索結果：
            </td>
            <td id="resultCount" runat="server">
            </td>
            <td>
                件
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
                                    <th style="width: 110px;">
                                        申込NO</th>
                                    <th style="width: 70px;">
                                        受注状況</th>
                                    <th style="width: 40px;">
                                        区分</th>
                                    <th style="width: 70px;">
                                        番号</th>
                                    <th style="width: 200px;">
                                        物件名称</th>
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
                                    <th style="width: 210px;">
                                        住所1</th>
                                    <th style="width: 210px;">
                                        住所2</th>
                                    <th style="width: 90px;">
                                        加盟店コード</th>
                                    <th style="width: 180px;">
                                        加盟店名</th>
                                    <th style="width: 140px;">
                                        依頼日</th>
                                    <th style="width: 80px;">
                                        商品コード</th>
                                    <th style="width: 180px;">
                                        商品名</th>
                                    <th style="width: 90px;">
                                        同時依頼棟数</th>
                                    <th style="width: 60px;">
                                        施主名</th>
                                    <th style="width: 110px;">
                                        調査方法</th>
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

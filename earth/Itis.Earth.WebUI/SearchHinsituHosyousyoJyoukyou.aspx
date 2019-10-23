<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="SearchHinsituHosyousyoJyoukyou.aspx.vb" Inherits="Itis.Earth.WebUI.SearchHinsituHosyousyoJyoukyou" Title="EARTH 品質保証書状況検索" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script src="js/sortable_sync_ja.js" type="text/javascript">
    </script>

    <script type="text/javascript">
        
        /*====================================
         *グローバル変数宣言（画面部品）
         ====================================*/
        //画面表示部品
        var objKubun1 = null;
        var objKubunAll = null;
        var objHoshouNoFrom = null;
        var objHoshouNoTo = null;
        var objKeiyakuNo = null;        
        
        var objSeshuName = null;
        var objBukkenJyuusho12 = null;        
        var objBikou = null;
        var objTyousakaisyaCd = null;

        var objchkHakkouStatus1 = null;
        var objchkHakkouStatus2 = null;
        var objchkHakkouStatus3 = null;
        var objchkHakkouStatus4 = null;
        var objchkHakkouStatus5 = null;
        var objchkHakkouStatus6 = null;

        var objKameitenCd = null;
        var objKameitenTorikesiRiyuu = null;
        var objKameitenMei = null;
        var objKameitenKana = null;
        var objHakkouTiming = null;

        //依頼書着日
        var objchkIraisyoTykDateBlank = null;
        var objIraisyoTykDateFrom = null;
        var objIraisyoTykDateTo = null;

        //発行日
        var objchkHakkouDateBlank = null;
        var objHakkouDateFrom = null;
        var objHakkouDateTo = null;

        //再発行日
        var objSaihakkouDateFrom = null;
        var objSaihakkouDateTo = null;

        //発行依頼日
        var objchkHakkouIraiDateBlank = null;
        var objHakkouIraiDateFrom = null;
        var objHakkouIraiDateTo = null;

        //物件依頼日
        var objBukkenIraiDateFrom = null;
        var objBukkenIraiDateTo = null;

        //保証期間
        var objHosyouKikanKameiten = null;
        var objHosyouKikanBukken = null;
        
        //データ破棄対象
        var objHakiTaisyou = null;

        //一括セット発行日
        var objIkkatuHakkouDate = null;

        //hidden
        var objKubunVal = null;
        var objHdnKensakuDisp = null;
        var objHdnKensakuStatus = null;
        var objHdnIkkatuHakkouDate = null;

        //検索実行用
        var objMaxSearchCount = null;
        var objSearch = null;

        //画面遷移用
        var objSendBtn = null;
        var objSendKubun = null;
        var objSendHoshoushoNo = null;
        var objSendTargetWin = null;
        
        // CSV出力用
        var objCsv = null;
        
        //選択物件一括受付用
        var objIkkatuUketuke = null;
        var objHiddenSendKbn = null;
        var objHiddenSendHosyousyoNo = null;
        
        /****************************************
         * onload時の追加処理
         * @param objTarget
         * @return
         ****************************************/
        function funcAfterOnload() {
            //画面表示部品セット
            setGlobalObj();
            
            //区分の状態をセッティング
            setKubunVal()
            
            //一括セット発行日をセット
            setIkkatuHakkouDate()
            
            //検索結果が1件のみの場合、値を戻す処理を実行
            if (objEBI("<%= firstSend.clientID %>").value != ""){
                returnSelectValue(objEBI(objEBI("<%= firstSend.clientID %>").value));
            }
            
            /*検索結果テーブル ソート設定*/
            sortables_init();
            
            /*検索結果テーブル 各種レイアウト設定*/
            settingResultTable();

            //後処理ボタンID
            objEBI("afterEventBtnId").value = "<%= search.clientID %>";
            
            // CSV出力を行なう
            if (objCsvOutPutFlg.value == "1"){
            
                // CSV出力時の確認メッセージ
                var objCfmMsg = null;
                
                // CSV出力上限の確認
                if(objEBI("<%= HiddenCsvMaxCnt.clientID %>").value == "1"){
                   objCfmMsg = "<%= Messages.MSG017C %>".replace("処理","CSV出力処理") 
                             + "<%= Messages.MSG162C %>".replace("@PARAM1","<%=EarthConst.MAX_CSV_OUTPUT %>").replace("@PARAM2","<%=EarthConst.MAX_CSV_OUTPUT %>")
                }else{
                   objCfmMsg = "<%= Messages.MSG017C %>".replace("処理","CSV出力処理")
                }
                
                //処理確認
                if(!confirm(objCfmMsg)){
                    // CSV出力フラグをクリア
                    objCsvOutPutFlg.value = "";
                    return false;
                }
                //CSV実行
                objCsv.click();
            }
            // CSV出力フラグをクリア
            objCsvOutPutFlg.value = "";

        }

        /*
        * 画面表示部品オブジェクトをグローバル変数化
        */
        function setGlobalObj() {
            //画面表示部品
            objKubun1 = objEBI("<%= cmbKubun_1.clientID %>");
            objKubunAll = objEBI("<%= kubun_all.clientID %>");
            objHoshouNoFrom = objEBI("<%= hoshouNo_from.clientID %>");
            objHoshouNoTo = objEBI("<%= hoshouNo_to.clientID %>");
            objKeiyakuNo = objEBI("<%= TextKeiyakuNo.clientID %>");

            objSeshuName = objEBI("<%= BukkenName.clientID %>");
            objBukkenJyuusho12 = objEBI("<%= bukkenJyuusho12.clientID %>");
            objBikou = objEBI("<%= TextBikou.clientID %>");
            objTyousakaisyaCd = objEBI("<%= tyousakaisyaCd.clientID %>");

            objchkHakkouStatus1 = objEBI("<%= chkHakkouStatus1.clientID %>");
            objchkHakkouStatus2 = objEBI("<%= chkHakkouStatus2.clientID %>");
            objchkHakkouStatus3 = objEBI("<%= chkHakkouStatus3.clientID %>");
            objchkHakkouStatus4 = objEBI("<%= chkHakkouStatus4.clientID %>");
            objchkHakkouStatus5 = objEBI("<%= chkHakkouStatus5.clientID %>");
            objchkHakkouStatus6 = objEBI("<%= chkHakkouStatus6.clientID %>");

            objKameitenCd = objEBI("<%= kameitenCd.clientID %>");
            objKameitenTorikesiRiyuu = objEBI("<%= TextTorikesiRiyuu.clientID %>");            
            objKameitenMei = objEBI("<%= kameitenNm.clientID %>");
            objKameitenKana = objEBI("<%= kameitenKana.clientID %>");
            objHakkouTiming = objEBI("<%= cmbHakkouTiming.clientID %>");

            //依頼書着日
            objchkIraisyoTykDateBlank = objEBI("<%= chkIraisyoTykDateBlank.clientID %>");
            objIraisyoTykDateFrom = objEBI("<%= TextIraisyoTykDateFrom.clientID %>");
            objIraisyoTykDateTo = objEBI("<%= TextIraisyoTykDateTo.clientID %>");

            //発行日
            objchkHakkouDateBlank = objEBI("<%= chkHakkouDateBlank.clientID %>");
            objHakkouDateFrom = objEBI("<%= TextHakkouDateFrom.clientID %>");
            objHakkouDateTo = objEBI("<%= TextHakkouDateTo.clientID %>");

            //再発行日
            objSaihakkouDateFrom = objEBI("<%= TextSaihakkouDateFrom.clientID %>");
            objSaihakkouDateTo = objEBI("<%= TextSaihakkouDateTo.clientID %>");

            //発行依頼日
            objchkHakkouIraiDateBlank = objEBI("<%= chkHakkouIraiDateBlank.clientID %>");
            objHakkouIraiDateFrom = objEBI("<%= TextHakkouIraiDateFrom.clientID %>");
            objHakkouIraiDateTo = objEBI("<%= TextHakkouIraiDateTo.clientID %>");

            //物件依頼日
            objBukkenIraiDateFrom = objEBI("<%= TextBukkenIraiDateFrom.clientID %>");
            objBukkenIraiDateTo = objEBI("<%= TextBukkenIraiDateTo.clientID %>");

            //保証期間
            objHosyouKikanKameiten = objEBI("<%= TextHosyouKikanKameiten.clientID %>");
            objHosyouKikanBukken = objEBI("<%= TextHosyouKikanBukken.clientID %>");

            //データ破棄
            objHakiTaisyou = objEBI("<%= CheckHakiTaisyou.clientID %>");
            
            //一括セット発行日
            objIkkatuHakkouDate = objEBI("<%= TextIkkatuHakkouDate.clientID %>");

            //hidden
            objKubunVal = objEBI("<%= kubunVal.clientID %>");
            objHdnKensakuInfo = objEBI("<%= kensakuInfo.clientID %>");
            objHdnKensakuStatus = objEBI("<%= HiddenKensakuInfoStyle.clientID %>");
            objHdnIkkatuHakkouDate = objEBI("<%= HiddenIkkatuHakkouDate.clientID %>");

            //検索実行用
            objMaxSearchCount = objEBI("<%= maxSearchCount.clientID %>");
            objSearch = objEBI("<%= search.clientID %>");
            
            //画面遷移用
            objSendBtn = objEBI("<%= btnSend.clientID %>");
            objSendKubun = objEBI("<%= sendKubun.clientID %>");
            objSendHoshoushoNo = objEBI("<%= sendHoshoushoNo.clientID %>");
            objSendTargetWin = objEBI("<%= sendTargetWin.clientID %>");
            
            // CSV出力用
            objCsv = objEBI("<%= ButtonHiddenCsv.clientID %>");
            objCsvOutPutFlg = objEBI("<%= HiddenCsvOutPut.clientID %>");
            
            //選択物件一括受付用
             objIkkatuUketuke = objEBI("<%= ikkatuUketuke.clientID %>");
             objHiddenSendKbn = objEBI("<%= HiddenSendKbn.clientID %>");
             objHiddenSendHosyousyoNo = objEBI("<%= HiddenSendHosyousyoNo.clientID %>");

        }
        
        /*
        * 検索結果テーブル 各種レイアウト設定
        */
        function settingResultTable(type){
            var tableTitle1 = objEBI("<%=TableTitleTable1.clientID %>");
            var tableTitle2 = objEBI("<%=TableTitleTable2.clientID %>");
            var tableData1 = objEBI("<%=TableDataTable1.clientID %>");
            var tableData2 = objEBI("<%=TableDataTable2.clientID %>");
            var divTitle1 = objEBI("<%=DivLeftTitle.clientID %>");
            var divTitle2 = objEBI("<%=DivRightTitle.clientID %>");
            var divData1 = objEBI("<%=DivLeftData.clientID %>");
            var divData2 = objEBI("<%=DivRightData.clientID %>");
            var minHeight = 120;
            var adjustHeight = 40;
            var adjustWidth = 370;

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
        
        /**
         * 明細行をダブルクリックした際の処理
         * @param objSelectedTr
         * @param intGamen[1:受,2:報,3:工,4:保]
         * @return
         */
        function returnSelectValue(objSelectedTr,intGamen) {
            if(objSelectedTr.tagName == "TR"){
                objSendTargetWin.value = "_blank";
                sendGyoumuGamen(objSelectedTr,4,2);
            }
            return;
        }
        
        /**
         * 物件確認画面表示処理
         * @param objSelectedTr
         * @param intGamen[1:受,2:報,3:工,4:保]
         * @param intEvent[1:別ウィンドウ,2:ダブルクリック]
         * @return
         */
        function sendGyoumuGamen(objSelectedTr,intGamen,intEvent){
            var varAction = '';
            
            //オブジェクトの再読込(Ajax再描画対応)
            objSendTargetWin = objEBI("<%= sendTargetWin.clientID %>");
            if(objSendTargetWin.value == "")objSendTargetWin.value="<%=EarthConst.MAIN_WINDOW_NAME %>";

            //戻り値郡配列(行の先頭セルの先頭オブジェクトから取得)
            var objSelRet = getChildArr(getChildArr(objSelectedTr,"TD")[0],"INPUT")[0];

            //<!-- 画面引渡し情報 -->
            objSendForm = objEBI("openPageForm");
            objSendVal_st = objEBI("st");
            objSendVal_Kubun = objEBI("sendPage_kubun");
            objSendVal_HosyousyoNo = objEBI("sendPage_hosyoushoNo");
            objSendVal_kbn = objEBI("kbn");
            objSendVal_no = objEBI("no");
            
            //値セット   
            objSendForm.target=objSendTargetWin.value;
            objSendVal_st.value="<%=EarthConst.MODE_VIEW %>";
            if(objSelRet != undefined){
                var arrReturnValue = objSelRet.value.split(sepStr);  
                objSendVal_Kubun.value=arrReturnValue[0];
                objSendVal_HosyousyoNo.value=arrReturnValue[1];
                objSendVal_kbn.value=arrReturnValue[0];
                objSendVal_no.value=arrReturnValue[1];
            }
                    
            //intEvent=1 : 別ウィンドウクリック(対象画面一つのみ)
            if(intEvent == 1){
                //オープン対象の業務画面を指定
                switch(intGamen){
                     case 1://
                        varAction = "<%=UrlConst.IRAI_KAKUNIN %>";
                        break;
                     case 2://
                        varAction = "<%=UrlConst.HOUKOKUSYO %>";
                        break;
                     case 3://
                        varAction = "<%=UrlConst.KAIRYOU_KOUJI %>";
                        break;
                     case 4://
                        varAction = "<%=UrlConst.HOSYOU %>";
                        break;
                     case 5://
                        varAction = "<%=UrlConst.IKKATU_HENKOU_KIHON %>";
                        if(!setCheckedIkkatu())return false;
                        break;
                     case 6://
                        varAction = "<%=UrlConst.IKKATU_HENKOU_TYS_SYOUHIN %>";
                        if(!setCheckedIkkatu())return false;
                        break;
                     default://
                        return false;
                        break;
                }
                objSendForm.action = varAction;
                objSendForm.submit();
                
            }else if(intEvent == 2){//intEvent=2 : ダブルクリック(チェックボックスのチェック対象画面を開く)
                //E:??
                if(flgEaster == 1){
                    flgEaster = null;
                    objSendForm.action="<%=UrlConst.POPUP_GAMEN_KIDOU %>";
                    objSendForm.submit();
                    return true;
                }
                //1:受
                 if(intGamen == 1){
                    objSendForm.action="<%=UrlConst.IRAI_KAKUNIN %>";
                    objSendForm.submit();
                }
                //2:報
                if(intGamen == 2){
                    objSendForm.action="<%=UrlConst.HOUKOKUSYO %>";
                    objSendForm.submit();
                }
                //3:工
                if(intGamen == 3){
                    objSendForm.action="<%=UrlConst.KAIRYOU_KOUJI %>";
                    objSendForm.submit();
                }
                //4:保
                if(intGamen == 4){
                    objSendForm.action="<%=UrlConst.HOSYOU %>";
                    objSendForm.submit();
                }
            }else{
                return false;
            }
        }

        /**
         * 選択物件一括受付ボタン処理前チェック
         * @return
         */
        function setCheckedIkkatuUketuke(){
        
            var tableData1 = objEBI("<%=TableDataTable1.clientID %>");
            var arrSakiTr = tableData1.tBodies[0].rows;
            var objTd = null;
            var arrInput = null;
            var bukkenCount = 0;

            objHiddenSendKbn.value = "";
            objHiddenSendHosyousyoNo.value = "";

            for ( var tri = 0; tri < arrSakiTr.length; tri++) {
                objTd = arrSakiTr[tri].cells[0];
                arrInput = getChildArr(objTd,"INPUT");
                for ( var ar = 0; ar < arrInput.length; ar++) {
                    if(arrInput[ar].type == "checkbox" && arrInput[ar].checked){
                        var arrVal = arrInput[ar].value.split(sepStr);
                        objHiddenSendKbn.value += arrVal[0] + sepStr;
                        objHiddenSendHosyousyoNo.value += arrVal[1] + sepStr;
                        bukkenCount++;
                    }
                }
            }
            //一つも選択されていなかった場合、エラー
            if(1 > bukkenCount){
                alert("<%= Messages.MSG125E %>");
                objSendVal_Kubun.value = "";
                objSendVal_HosyousyoNo.value = "";
                return false;
            }
            //上限物件数を超えていた場合、エラー
            if(bukkenCount >= objEBI("<%=HiddenIkkatuUketukeMax.clientID %>").value){
                alert("<%= Messages.MSG216E %>");
                objSendVal_Kubun.value = "";
                objSendVal_HosyousyoNo.value = "";
                return false;
            }
            //「一括セット発行日」が空白または過去日付の場合、エラー
            if((objIkkatuHakkouDate.value == "") || (!checkDaiSyouIkkatuHakkou(objHdnIkkatuHakkouDate,objIkkatuHakkouDate,"一括セット発行日"))){
                  alert("<%= Messages.MSG040E %>".replace("@PARAM1","一括セット発行日"));
//                objSendVal_Kubun.value = "";
//                objSendVal_HosyousyoNo.value = "";
                return false;
            }

            return true;
        }

        /**
         * 区分セレクトボックス＆チェックボックスの状態をチェックし、選択されている区分をまとめる
         * @return
         */
        function setKubunVal(){
            objKubunVal.value = ""; //初期化
            if(objKubunAll.checked == true){
                objKubun1.selectedIndex = 0;
                objKubun1.disabled = true;
                return;
            }else{
                objKubun1.disabled = false;
                if(objKubun1.value != ""){
                    objKubunVal.value = objKubun1.value;
                }
            }
        }

        /**
         * 一括セット発行日をセット
         * @return
         */
        function setIkkatuHakkouDate(){
            if(objIkkatuHakkouDate.value == ""){
                //一括セット発行日にすでに日付がセットされていない場合のみ、
                //システム日付をセットする
                objIkkatuHakkouDate.value = getToday();
                //Hiddenにセット(比較用)
                objHdnIkkatuHakkouDate.value = getToday();
                return;
            }
        }

        /**
         * 「検索実行」押下時のチェック処理
         * @return
         */
        function checkJikkou(varAction){

            if(!objKubunAll.checked && objKubunVal.value.Trim() == ""){
                alert("<%= Messages.MSG006E %>");
                objKubun1.focus();
                return false;
            }
            if(objKubunAll.checked && !checkInputJouken()){
                alert("<%= Messages.MSG008E %>");
                objKubunAll.focus();
                return false;
            }

            //番号 大小チェック
            if(!checkDaiSyou(objHoshouNoFrom,objHoshouNoTo,"番号"))return false;
            
            //依頼書着日 大小チェック
            if(!checkDaiSyou(objIraisyoTykDateFrom,objIraisyoTykDateTo,"依頼書着日"))return false;
            
            //発行日 大小チェック
            if(!checkDaiSyou(objHakkouDateFrom,objHakkouDateTo,"発行日"))return false;

            //再発行日 大小チェック
            if(!checkDaiSyou(objSaihakkouDateFrom,objSaihakkouDateTo,"再発行日"))return false;

            //発行依頼日 大小チェック
            if(!checkDaiSyou(objHakkouIraiDateFrom,objHakkouIraiDateTo,"発行依頼日"))return false;

            //物件依頼日 大小チェック
            if(!checkDaiSyou(objBukkenIraiDateFrom,objBukkenIraiDateTo,"物件依頼日"))return false;

            if(varAction == "0"){
                //表示件数チェック
                if(objMaxSearchCount.value == "max"){
                    if(!confirm(("<%= Messages.MSG007C %>")))return false;
                }
                //検索実行
                objSearch.click();
            }else if(varAction == "1"){
                    objCsvOutPutFlg.value = "1"
                    //検索実行
                    objSearch.click();
            }
        }
        
        /**
         * 区分、対象範囲以外の検索条件が入力されているかのチェック
         * @return true:一つでも入力がある/false:一つも入力が無い
         */
        function checkInputJouken(){
        
            if(objHoshouNoFrom.value!="")return true;
            if(objHoshouNoTo.value!="")return true;
            if(objKeiyakuNo.value!="")return true;
            
            if(objSeshuName.value!="")return true;
            if(objBukkenJyuusho12.value!="")return true;
            if(objBikou.value!="")return true;
            if(objTyousakaisyaCd.value!="")return true;            

            if(objchkHakkouStatus1.checked)return true;
            if(objchkHakkouStatus2.checked)return true;
            if(objchkHakkouStatus3.checked)return true;
            if(objchkHakkouStatus4.checked)return true;
            if(objchkHakkouStatus5.checked)return true;
            if(objchkHakkouStatus6.checked)return true;
 
            if(objKameitenCd.value!="")return true;
            if(objKameitenKana.value!="")return true;
            if(objHakkouTiming.value!="")return true;

            //依頼書着日
            if(objchkIraisyoTykDateBlank.checked)return true;
            if(objIraisyoTykDateFrom.value!="")return true;
            if(objIraisyoTykDateTo.value!="")return true;
            
            //発行日
            if(objchkHakkouDateBlank.checked)return true;
            if(objHakkouDateFrom.value!="")return true;
            if(objHakkouDateTo.value!="")return true;

            //再発行日
            if(objSaihakkouDateFrom.value!="")return true;
            if(objSaihakkouDateTo.value!="")return true;

            //発行依頼日
            if(objchkHakkouIraiDateBlank.checked)return true;
            if(objHakkouIraiDateFrom.value!="")return true;
            if(objHakkouIraiDateTo.value!="")return true;

            //物件依頼日
            if(objBukkenIraiDateFrom.value!="")return true;
            if(objBukkenIraiDateTo.value!="")return true;

            //保証期間setCheckedIkkatuUketuke
            if(objHosyouKikanKameiten.value!="")return true;
            if(objHosyouKikanBukken.value!="")return true;
                        
            return false;
        
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

        /**
         * 大小チェック(一括セット発行日※過去日付はエラー)
         * @return true/false
         */
        function checkDaiSyouIkkatuHakkou(objFrom,objTo,mess){
            if(objFrom.value != "" && objTo.value != ""){
                if(Number(removeSlash(objFrom.value)) > Number(removeSlash(objTo.value))){
                    // alert("<%= Messages.MSG040E %>".replace("@PARAM1",mess));
                    objFrom.select();
                    return false;
                }
            }
            return true;
        }

        /**
         * 保証書NO To自動セット
         * @return true/false
         */
        function setHosyouNoTo(obj){
            if(obj.id == objHoshouNoFrom.id && objHoshouNoTo.value == ""){
                objHoshouNoTo.value = obj.value;
                objHoshouNoTo.select();
            }
            return true;
        }

        /**
         * To自動セット
         * @return true/false
         */
        function setTo(obj){
            //依頼書着日
            if(obj.id == objIraisyoTykDateFrom.id && objIraisyoTykDateTo.value == ""){
                objIraisyoTykDateTo.value = obj.value;
                objIraisyoTykDateTo.select();
            //発行日
            }else if(obj.id == objHakkouDateFrom.id && objHakkouDateTo.value == ""){
                objHakkouDateTo.value = obj.value;
                objHakkouDateTo.select();
            //再発行日
            }else if(obj.id == objSaihakkouDateFrom.id && objSaihakkouDateTo.value == ""){
                objSaihakkouDateTo.value = obj.value;
                objSaihakkouDateTo.select();
            //発行依頼日
            }else if(obj.id == objHakkouIraiDateFrom.id && objHakkouIraiDateTo.value == ""){
                objHakkouIraiDateTo.value = obj.value;
                objHakkouIraiDateTo.select();
            //物件依頼日
            }else if(obj.id == objBukkenIraiDateFrom.id && objBukkenIraiDateTo.value == ""){
                objBukkenIraiDateTo.value = obj.value;
                objBukkenIraiDateTo.select();
            }
            return true;
        }

        /**
         * Allクリア処理後に実行されるファンクション
         * @return 
         */
        function funcAfterAllClear(obj){
            //全区分にチェック
            objKubunAll.click();

            objMaxSearchCount.selectedIndex = 1;
            //日付をセット
            objIkkatuHakkouDate.value = getToday();
            //検索条件 非表示→表示
            if(objHdnKensakuInfo.style.display == 'none'){
                objHdnKensakuInfo.style.display = 'inline';
                objHdnKensakuStatus.value = 'inline';
            }
            //発行進捗状況のデフォルトチェック
            objchkHakkouStatus4.checked = true;
            objKubun1.focus();
        }

        /**
         * 値をチェックし、対象をクリアする
         * @return 
         */
        function clrName(obj,targetId){
            if(obj.value == "") objEBI(targetId).value="";
        }

        /*********************
         * 加盟店情報クリア
         *********************/
        function clrKameitenInfo(obj){
            if(obj.value == ""){
                //値のクリア
                objKameitenCd.value = "";
                objKameitenTorikesiRiyuu.value = "";
                objKameitenMei.value = "";
                //色をデフォルトへ
                objKameitenCd.style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>"
                objKameitenTorikesiRiyuu.style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>"
                objKameitenMei.style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>"
            }
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


    //Ajax処理完了後処理
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args){
            //画面表示部品セット
            setGlobalObj();
        }

    /**
     * 「選択物件一括受付」押下時のチェック処理
     * @return
     */
    function checkIkkatuUketuke(){
        
        //チェック
        if(!setCheckedIkkatuUketuke())return false;
        
        //検索実行
        objIkkatuUketuke.click();
    }

    //変更前コントロールの値を退避して、該当コントロール(Hidden)に保持する
    function SetChangeMaeValue(strTaihiID, strTargetID){
       document.getElementById(strTaihiID).value = document.getElementById(strTargetID).value;
    }

    </script>

    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    
    <%--CSV出力判断--%>
    <asp:HiddenField ID="HiddenCsvOutPut" runat="server" />
    <%--CSV出力上限件数フラグ--%>
    <asp:HiddenField ID="HiddenCsvMaxCnt" runat="server" />
    <%--排他用日時--%>
    <input type="hidden" id="HiddenHaitaDate" runat="server" />
    <input type="hidden" id="HiddenKensakuKbn" runat="server" /><%--検索時区分--%>
    <input type="hidden" id="HiddenKensakuNoFrom" runat="server" /><%--検索時番号From--%>
    <input type="hidden" id="HiddenKensakuNoTo" runat="server" /><%--検索時番号To--%>
    <input type="hidden" id="HiddenKensakukeiyakuNo" runat="server" /><%--検索時契約NO--%>
    <input type="hidden" id="HiddenKensakuSesyuMei" runat="server" /><%--検索時物件名--%>
    <input type="hidden" id="HiddenKensakuBukkenjyuusyo" runat="server" /><%--検索時物件住所１＋２--%>
    <input type="hidden" id="HiddenKensakuBikou" runat="server" /><%--検索時備考--%>
    <input type="hidden" id="HiddenKensakuTysKaisya" runat="server" /><%--検索時調査会社コード・調査会社事業所コード--%>
    <input type="hidden" id="HiddenKensakuHakSts1" runat="server" /><%--検索時発行進捗状況1--%>
    <input type="hidden" id="HiddenKensakuHakSts2" runat="server" /><%--検索時発行進捗状況2--%>
    <input type="hidden" id="HiddenKensakuHakSts3" runat="server" /><%--検索時発行進捗状況3--%>
    <input type="hidden" id="HiddenKensakuHakSts4" runat="server" /><%--検索時発行進捗状況4--%>
    <input type="hidden" id="HiddenKensakuHakSts5" runat="server" /><%--検索時発行進捗状況5--%>
    <input type="hidden" id="HiddenKensakuHakSts6" runat="server" /><%--検索時発行進捗状況6--%>
    <input type="hidden" id="HiddenKensakuKameitenCd" runat="server" /><%--検索時加盟店コード--%>
    <input type="hidden" id="HiddenKensakuTenmeiKana" runat="server" /><%--検索時加盟店カナ--%>
    <input type="hidden" id="HiddenKensakuHakTiming" runat="server" /><%--検索時発行タイミング（加盟店発行設定）--%>
    <input type="hidden" id="HiddenKensakuHakIraiTykChk" runat="server" /><%--検索時依頼書着日 空--%>
    <input type="hidden" id="HiddenKensakuHakIraiTykFrom" runat="server" /><%--検索時依頼書着日 From--%>
    <input type="hidden" id="HiddenKensakuHakIraiTykTo" runat="server" /><%--検索時依頼書着日 To--%>
    <input type="hidden" id="HiddenKensakuHakDtChk" runat="server" /><%--検索時発行日 空--%>
    <input type="hidden" id="HiddenKensakuHakDtFrom" runat="server" /><%--検索時発行日 From--%>
    <input type="hidden" id="HiddenKensakuHakDtTo" runat="server" /><%--検索時発行日 To--%>
    <input type="hidden" id="HiddenKensakuSaiHakDtFrom" runat="server" /><%--検索時再発行日 From--%>
    <input type="hidden" id="HiddenKensakuSaiHakDtTo" runat="server" /><%--検索時再発行日 To--%>
    <input type="hidden" id="HiddenKensakuHakIraiTimeChk" runat="server" /><%--検索時発行依頼日 空--%>
    <input type="hidden" id="HiddenKensakuHakIraiTimeFrom" runat="server" /><%--検索時発行依頼日 From--%>
    <input type="hidden" id="HiddenKensakuHakIraiTimeTo" runat="server" /><%--検索時発行依頼日 To--%>
    <input type="hidden" id="HiddenKensakuIraiDtFrom" runat="server" /><%--検索時物件依頼日 From--%>
    <input type="hidden" id="HiddenKensakuIraiDtTo" runat="server" /><%--検索時物件依頼日 To--%>
    <input type="hidden" id="HiddenKensakuHosyouKknMk" runat="server" /><%--検索時保証期間(加盟店)--%>
    <input type="hidden" id="HiddenKensakuHosyouKknTj" runat="server" /><%--検索時保証期間(物件)--%>
    <input type="hidden" id="HiddenKensakuHakiSyubetu" runat="server" /><%--検索時データ破棄種別--%>
    <%--選択物件一括受付用(チェック付のもの)--%>
    <input type="hidden" id="HiddenSendKbn" runat="server" />
    <input type="hidden" id="HiddenSendHosyousyoNo" runat="server" />
    <%--選択物件一括受付用(邸別請求登録用)--%>
    <input type="hidden" id="HiddenTextShSyouhinCd" runat="server" />
    <input type="hidden" id="HiddenShZeiritu" runat="server" />
    <input type="hidden" id="HiddenShZeiKbn" runat="server" />
    <input type="hidden" id="HiddenSimeDate" runat="server" />   
    <input type="hidden" id="HiddenTextShSeikyuusyoHakkouDate" runat="server" />      
    <input type="hidden" id="HiddenTextShUriageNengappi" runat="server" />      
    <input type="hidden" id="HiddenTextShHattyuusyoKakutei" runat="server" />  
    <input type="hidden" id="HiddenTextShSeikyuusaki" runat="server" />
    <input type="hidden" id="HiddenTextShJituseikyuuKingaku" runat="server" />
    <input type="hidden" id="HiddenTextShKoumutenSeikyuuKingaku" runat="server" />
    <input type="hidden" id="HiddenTextShSyouhizei" runat="server" />
    <%--選択物件一括受付処理済--%>
    <input type="hidden" id="HiddenIkkatuSyoriZumiKbn" runat="server" />
    <input type="hidden" id="HiddenIkkatuSyoriZumiNo" runat="server" />
    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tbody>
            <tr>
                <th style="text-align: left;">
                    品質保証書状況検索</th>
                <th >
                </th>
            </tr>
            <tr>
                <td colspan="2" rowspan="1">
                </td>
            </tr>
        </tbody>
    </table>
    <input type="hidden" id="gamenId" value="kensaku" runat="server" />
    <table style="text-align: left;" class="mainTable paddinNarrow">
        <thead>
            <tr>
                <th class="tableTitle" colspan="8" rowspan="1">
                    <a id="AKensakuInfo" tabindex="10" runat="server">検索条件</a>
                    <input id="btnClearWin" value="クリア" type="reset" class="button" tabindex="10" />
                    <input type="hidden" id="HiddenKensakuInfoStyle" runat="server" value="inline" /></th>
            </tr>
        </thead>
        <tbody id="kensakuInfo" style="display: inline;" runat="server">
            <tr>
                <td class="hissu" style="font-weight: bold">
                    区分</td>
                <td colspan="3" class="hissu">
                    <asp:DropDownList ID="cmbKubun_1" runat="server" TabIndex="10">
                    </asp:DropDownList>
                    &nbsp;全区分<input id="kubun_all" type="checkbox" runat="server" tabindex="10" />
                    <input type="hidden" id="kubunVal" runat="server" /></td>
                <td class="koumokuMei">
                    番号</td>
                <td colspan="1">
                    <input id="hoshouNo_from" runat="server" maxlength="10" style="width: 70px;" class="codeNumber"
                        onblur="checkNumber(this);" onchange="if(checkNumber(this))setHosyouNoTo(this);"
                        tabindex="10" />～<input id="hoshouNo_to" runat="server" maxlength="10" style="width: 70px;"
                            class="codeNumber" onblur="checkNumber(this);" tabindex="10" /></td>
                <td class="koumokuMei">
                    契約NO
                </td>
                <td colspan="1">
                    <input id="TextKeiyakuNo" style="width: 150px; ime-mode: inactive;" runat="server" maxlength="20"
                            class="codeNumber" tabindex="10"/>
                </td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    物件名</td>
                <td colspan="1">
                    <input id="BukkenName" runat="server" maxlength="50" style="width: 100px; ime-mode: active;"
                        tabindex="10" /></td>
                <td class="koumokuMei">
                    物件住所</td>
                <td colspan="1">
                    <input size="100" id="bukkenJyuusho12" runat="server" maxlength="64" style="width: 100px;
                        ime-mode: active;" tabindex="10" /></td>
                <td class="koumokuMei">
                    備考</td>
                <td colspan="1">
                    <input id="TextBikou" runat="server" maxlength="256" style="width: 100px; ime-mode: active;"
                        tabindex="10" /></td>
                <td class="koumokuMei">
                    調査会社</td>
                <td colspan="1">
                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server" RenderMode="Inline">
                        <ContentTemplate>
                            <input id="tyousakaisyaCd" runat="server" maxlength="7" style="width: 45px;" class="codeNumber"
                                tabindex="10" />
                            <input id="tyousakaisyaSearch" runat="server" type="button" value="検索" class="gyoumuSearchBtn"
                                tabindex="10"  style="width:3em;" />&nbsp;
                            <input id="tyousakaisyaNm" runat="server" class="readOnlyStyle" style="width: 5em"
                                readonly="readOnly" tabindex="-1" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>                                                                        
            </tr>
            <tr>
                <td class="koumokuMei">
                    発行進捗状況
                </td>
                <td colspan="7" rowspan="1" style="text-align: left">
                    <input id="chkHakkouStatus1" name="chkHakkouStatus1" value="1" type="checkbox" runat="server"
                         tabindex="10" />対象外
                    <input id="chkHakkouStatus2" name="chkHakkouStatus2" value="1" type="checkbox" runat="server"
                        tabindex="10" />発行不可
                    <input id="chkHakkouStatus3" name="chkHakkouStatus3" value="1" type="checkbox" runat="server"
                        tabindex="10" />発行可未依頼
                    <input id="chkHakkouStatus4" name="chkHakkouStatus4" value="1" type="checkbox" runat="server"
                        checked="checked" tabindex="10" />モール依頼(再発行含)済・未受付
                    <input id="chkHakkouStatus5" name="chkHakkouStatus5" value="1" type="checkbox" runat="server"
                         tabindex="10" />モール依頼(再発行含)処理済
                    <input id="chkHakkouStatus6" name="chkHakkouStatus6" value="1" type="checkbox" runat="server"
                        tabindex="10" />初回発行済
                 </td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    加盟店</td>
                <td colspan="2">
                    <asp:UpdatePanel ID="UpdatePanel_irai1" UpdateMode="Conditional" runat="server" RenderMode="Inline">
                        <ContentTemplate>
                            <input id="kameitenCd" runat="server" maxlength="5" style="width: 40px;" class="codeNumber"
                                tabindex="10" />
                            <input id="kameitenSearch" runat="server" type="button" value="検索" class="gyoumuSearchBtn"
                                tabindex="10" style="width:3em;" />&nbsp;
                            <input id="kameitenNm" runat="server" class="readOnlyStyle" readonly="readonly" style="width: 5em;"
                                tabindex="-1" />&nbsp;
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td colspan="1">
                    <asp:UpdatePanel ID="UpdatePanel_KtTorikesi" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="kameitenSearch" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:TextBox ID="TextTorikesiRiyuu" runat="server" Width="100px"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="koumokuMei">
                    加盟店カナ</td>
                <td colspan="1">
                    <input id="kameitenKana" runat="server" maxlength="40" style="width: 150px; ime-mode: active;"
                        tabindex="10" /></td>
                <td class="koumokuMei" >
                    初回発行方法</td>
                <td colspan="1">
                    <asp:DropDownList ID="cmbHakkouTiming" runat="server" TabIndex="10">
                        <asp:ListItem Value="" Text="" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="0" Text="依頼書"></asp:ListItem>
                        <asp:ListItem Value="1" Text="自動発行"></asp:ListItem>
                        <asp:ListItem Value="2" Text="地盤モール"></asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    依頼書着日
                    <input id="chkIraisyoTykDateBlank" value="1" type="checkbox" runat="server" tabindex="10" />空</td>
                <td class="date" colspan="3">
                    <input id="TextIraisyoTykDateFrom" runat="server" maxlength="10" class="date" onblur="checkDate(this);" onchange="if(checkDate(this))setTo(this);"
                        tabindex="10" />～<input id="TextIraisyoTykDateTo" runat="server" maxlength="10"
                            class="date" onblur="checkDate(this);" tabindex="10" /></td>
                <td class="koumokuMei">
                    発行日
                    <input id="chkHakkouDateBlank" value="1" type="checkbox" runat="server" tabindex="10" />空</td>
                <td class="date" colspan="1">
                    <input id="TextHakkouDateFrom" runat="server" maxlength="10" class="date" onblur="checkDate(this);" onchange="if(checkDate(this))setTo(this);"
                        tabindex="10" />～<input id="TextHakkouDateTo" runat="server" maxlength="10"
                            class="date" onblur="checkDate(this);" tabindex="10" /></td>
                <td class="koumokuMei">
                    再発行日</td>
                <td class="date" colspan="1">
                    <input id="TextSaihakkouDateFrom" runat="server" maxlength="10" class="date" onblur="checkDate(this);" onchange="if(checkDate(this))setTo(this);"
                        tabindex="10" />～<input id="TextSaihakkouDateTo" runat="server" maxlength="10"
                            class="date" onblur="checkDate(this);" tabindex="10" /></td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    発行依頼日
                    <input id="chkHakkouIraiDateBlank" value="1" type="checkbox" runat="server" tabindex="10" />空</td>
                <td class="date" colspan="3">
                    <input id="TextHakkouIraiDateFrom" runat="server" maxlength="10" class="date" onblur="checkDate(this);" onchange="if(checkDate(this))setTo(this);"
                        tabindex="10" />～<input id="TextHakkouIraiDateTo" runat="server" maxlength="10"
                            class="date" onblur="checkDate(this);" tabindex="10" /></td>
                <td class="koumokuMei">
                    物件依頼日
                </td>
                <td class="date">
                    <input id="TextBukkenIraiDateFrom" runat="server" maxlength="10" class="date" onblur="checkDate(this);" onchange="if(checkDate(this))setTo(this);"
                        tabindex="10" />～<input id="TextBukkenIraiDateTo" runat="server" maxlength="10"
                            class="date" onblur="checkDate(this);" tabindex="10" /></td>            
                <td class="koumokuMei">
                    保証期間
                </td>
                <td colspan="1">
                    加盟店<input id="TextHosyouKikanKameiten" type="text" runat="server" maxlength="2" style="width: 20px;
                        ime-mode: disabled;" class="codeNumber" onblur="checkNumber(this);" tabindex="10" />年&nbsp;&nbsp;&nbsp;
                    物件<input id="TextHosyouKikanBukken" type="text" runat="server" maxlength="2" style="width: 20px;
                        ime-mode: disabled;" class="codeNumber" onblur="checkNumber(this);" tabindex="10"/>年
                </td>
            </tr>
            <tr class="tableSpacer">
                <td colspan="8">
                </td>
            </tr>
        </tbody>
        <tbody id="kensakuJikkou" style="display: inline;">
            <tr>
                <td colspan="8" rowspan="1" style="text-align: center">
                    <img src="images/spacer.gif" alt="" style="width: 30px; height: 0px;" />
                    検索上限件数
                    <select id="maxSearchCount" runat="server" tabindex="10">
                        <option value="10">10件</option>
                        <option value="100" selected="selected">100件</option>
                        <option value="max">無制限</option>
                    </select>
                    <input type="button" id="btnSearch" value="検索実行" runat="server" tabindex="10" />
                    <input type="button" id="search" value="検索実行btn" style="display: none" runat="server" />
                    <img src="images/spacer.gif" alt="" style="width: 30px; height: 0px;" />
                    <input id="CheckHakiTaisyou" value="0" type="checkbox" runat="server"
                        tabindex="10" />データ破棄種別有りは検索対象外
                </td>
            </tr>
        </tbody>
    </table>
    <input type="hidden" id="afterEventBtnId" runat="server" />
    <table style="text-align: left; height: 20px; width: 100px;" border="0" cellpadding="0" cellspacing="0">
    <tr><td>&nbsp;</td></tr>
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
            <td colspan="10"></td>
            <td>一括セット発行日</td>
            <td class="date">
                <input id="TextIkkatuHakkouDate" runat="server" maxlength="10" class="date" onblur="checkDate(this);" tabindex="10" />
                <input type="hidden" id="HiddenIkkatuHakkouDate" runat="server" />
                <input type="hidden" id="HiddenIkkatuUketukeMax" runat="server" />
                <input type="button" id="ButtonIkkatuUketuke" runat="server" value="選択物件一括受付" style="width: 160px;"
                    onclick="" tabindex="10" />
                <input type="button" id="ikkatuUketuke" value="選択物件一括受付実行btn" style="display: none" runat="server" />
                <input type="button" id="ButtonCsv" runat="server" value="CSV出力"
                    style="width: 160px;" onclick="" tabindex="10" />
                    <input type="button" id="ButtonHiddenCsv" value="CSV実行btn" style="display: none"
                        runat="server" tabindex="-1" />
            </td>
        </tr>
    </table>

    <table style="text-align: left; width: 100px;" cellpadding="0" cellspacing="0">
        <thead>
            <tr>
                <th style="text-align: left;">
                    <div id="DivLeftTitle" runat="server" class="scrollDivLeftTitleStyle2">
                        <table cellpadding="0" cellspacing="0" id="TableTitleTable1" runat="server" class="scrolltablestyle2 sortableTitle"
                            style="border-top: 1px solid #999999; border-left: 1px solid #999999;">
                            <thead>
                                <tr>
                                    <th style="width: 40px" class="unsortable">
                                        一括</th>
                                    <th style="width: 40px;">
                                        区分</th>
                                    <th style="width: 70px;">
                                        番号</th>
                                    <th style="width: 200px;">
                                        施主名</th>
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
                                <tr>
                                    <th style="width: 200px">
                                        物件住所</th>
                                    <th style="width: 82px;">
                                        加盟店コード</th>
                                    <th style="width: 180px;">
                                        加盟店名</th>
                                    <th style="width: 140px">
                                        依頼日時</th>
                                    <th style="width: 80px">
                                        依頼書着日</th>
                                    <th style="width: 80px">
                                        発行日</th>
                                    <th style="width: 80px">
                                        保証開始日</th>
                                    <th style="width: 120px">
                                        判定</th>
                                    <th style="width: 80px">
                                        工報受理日</th>
                                    <th style="width: 150px">
                                        保証なし理由</th>
                                    <th style="width: 80px">
                                        営業担当者</th>
                                    <th style="width: 100px">
                                        初回発行方法</th>
                                    <th style="width: 80px">
                                        加/保証期間</th>
                                    <th style="width: 80px">
                                        物/保証期間</th>
                                    <th style="width: 80px">
                                        物件依頼日</th>
                                    <th style="width: 320px">
                                        備考</th>
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

    <script type="text/javascript">
    </script>

    <input type="hidden" id="firstSend" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server" RenderMode="Inline">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSend" />
        </Triggers>
        <ContentTemplate>
            <input type="hidden" id="sendKubun" runat="server" />
            <input type="hidden" id="sendHoshoushoNo" runat="server" />
            <input type="hidden" id="sendTargetWin" runat="server" />
            <!-- 受注確認画面遷移ボタン（非表示） -->
            <input type="button" id="btnSend" value="確認画面遷移" style="display: none" runat="server"
                onserverclick="btnSend_ServerClick" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="SearchBukken.aspx.vb" Inherits="Itis.Earth.WebUI.SearchBukken" Title="EARTH 物件検索" %>

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
        var objKubun2 = null;
        var objKubun3 = null;
        var objKubunAll = null;
        var objRdoHanni0 = null;
        var objRdoHanni1 = null;
        var objHoshouNoFrom = null;
        var objHoshouNoTo = null;
        var objKameitenCd = null;
        var objKameitenTorikesiRiyuu = null;
        var objKameitenMei = null;
        var objKameitenKana = null;
        var objKeiretuCd = null;
        var objTyousakaisyaCd = null;
        var objKoujiKaishaCd = null;
        var objKoujiUriageDateFrom = null;
        var objKoujiUriageDateTo = null;
        var objKoujiKanryouYoteiDateFrom = null;
        var objKoujiKanryouYoteiDateTo = null;
        var objSeshuName = null;
        var objBukkenJyuusho12 = null;
        var objIraiDateFrom = null;
        var objIraiDateTo = null;
        var objTyousaKibouDateFrom = null;
        var objTyousaKibouDateTo = null;
        var objBikou = null;
        var objTyousaJissiDateFrom = null;
        var objTyousaJissiDateTo = null;
        var objHosyousyoHakkouDateFrom = null;
        var objHosyousyoHakkouDateTo = null;
        var objSyoudakusyoTyousaDateFrom = null;
        var objSyoudakusyoTyousaDateTo = null;
        var objKeikakusyoSakuseiDateFrom = null;
        var objKeikakusyoSakuseiDateTo = null;
        var objHosyousyoHakkouIraisyoTyakuDateFrom = null;
        var objHosyousyoHakkouIraisyoTyakuDateTo = null;
        var objHakiTaisyou = null;
        var objYoyakuZumi = null;
        
        var objchkHyoujiGamen1 = null;
        var objchkHyoujiGamen2 = null;
        var objchkHyoujiGamen3 = null;
        var objchkHyoujiGamen4 = null;

        //hidden
        var objKubunVal = null;
        var objHdnKensakuDisp = null;
        var objHdnKensakuStatus = null;

        //検索実行用
        var objMaxSearchCount = null;
        var objSearch = null;

        //画面遷移用
        var objSendBtn = null;
        var objSendKubun = null;
        var objSendHoshoushoNo = null;
        var objSendTargetWin = null;
        
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
            
            //検索結果が1件のみの場合、値を戻す処理を実行
            if (objEBI("<%= firstSend.clientID %>").value != ""){
                returnSelectValue(objEBI(objEBI("<%= firstSend.clientID %>").value));
            }
            
            /*検索結果テーブル ソート設定*/
            sortables_init();
            
            /*検索結果テーブル 各種レイアウト設定*/
            settingResultTable();

        }

        /*
        * 画面表示部品オブジェクトをグローバル変数化
        */
        function setGlobalObj() {
            //画面表示部品
            objKubun1 = objEBI("<%= cmbKubun_1.clientID %>");
            objKubun2 = objEBI("<%= cmbKubun_2.clientID %>");
            objKubun3 = objEBI("<%= cmbKubun_3.clientID %>");
            objKubunAll = objEBI("<%= kubun_all.clientID %>");
            objRdoHanni0 = objEBI("<%= rdo_hanni0.clientID %>");
            objRdoHanni1 = objEBI("<%= rdo_hanni1.clientID %>");
            objHoshouNoFrom = objEBI("<%= hoshouNo_from.clientID %>");
            objHoshouNoTo = objEBI("<%= hoshouNo_to.clientID %>");
            objKameitenCd = objEBI("<%= kameitenCd.clientID %>");
            objKameitenTorikesiRiyuu = objEBI("<%= TextTorikesiRiyuu.clientID %>");
            objKameitenMei = objEBI("<%= kameitenNm.clientID %>");
            objKameitenKana = objEBI("<%= kameitenKana.clientID %>");
            objKeiretuCd = objEBI("<%= keiretuCd.clientID %>");
            objTyousakaisyaCd = objEBI("<%= tyousakaisyaCd.clientID %>");
            objKoujiKaishaCd = objEBI("<%= kojiKaishaCd.clientID %>");
            objKoujiUriageDateFrom = objEBI("<%= TextKoujiUriageDateFrom.clientID %>");
            objKoujiUriageDateTo = objEBI("<%= TextKoujiUriageDateTo.clientID %>");
            objKoujiKanryouYoteiDateFrom = objEBI("<%= TextKoujiKanryouYoteiDateFrom.clientID %>");
            objKoujiKanryouYoteiDateTo = objEBI("<%= TextKoujiKanryouYoteiDateTo.clientID %>");
            objSeshuName = objEBI("<%= seshuName.clientID %>");
            objBukkenJyuusho12 = objEBI("<%= bukkenJyuusho12.clientID %>");
            objIraiDateFrom = objEBI("<%= TextIraiDateFrom.clientID %>");;
            objIraiDateTo = objEBI("<%= TextIraiDateTo.clientID %>");;
            objTyousaKibouDateFrom = objEBI("<%= TextTyousaKibouDateFrom.clientID %>");;
            objTyousaKibouDateTo = objEBI("<%= TextTyousaKibouDateTo.clientID %>");;
            objBikou = objEBI("<%= TextBikou.clientID %>");
            objTyousaJissiDateFrom = objEBI("<%= TextTyousaJissiDateFrom.clientID %>");
            objTyousaJissiDateTo = objEBI("<%= TextTyousaJissiDateTo.clientID %>");
            objHosyousyoHakkouDateFrom = objEBI("<%= TextHosyousyoHakkouDateFrom.clientID %>");
            objHosyousyoHakkouDateTo = objEBI("<%= TextHosyousyoHakkouDateTo.clientID %>");
            objSyoudakusyoTyousaDateFrom = objEBI("<%= TextSyoudakusyoTyousaDateFrom.clientID %>");
            objSyoudakusyoTyousaDateTo = objEBI("<%= TextSyoudakusyoTyousaDateTo.clientID %>");
            objKeikakusyoSakuseiDateFrom = objEBI("<%= TextKeikakusyoSakuseiDateFrom.clientID %>");
            objKeikakusyoSakuseiDateTo = objEBI("<%= TextKeikakusyoSakuseiDateTo.clientID %>");
            objHosyousyoHakkouIraisyoTyakuDateFrom = objEBI("<%= TextHosyousyoHakkouIraisyoTyakuDateFrom.clientID %>");
            objHosyousyoHakkouIraisyoTyakuDateTo = objEBI("<%= TextHosyousyoHakkouIraisyoTyakuDateTo.clientID %>");
            objHakiTaisyou = objEBI("<%= CheckHakiTaisyou.clientID %>");
            objYoyakuZumi = objEBI("<%= CheckYoyakuZumi.clientID %>");
            
            objchkHyoujiGamen1 = objEBI("<%= chkHyoujiGamen1.clientID %>");
            objchkHyoujiGamen2 = objEBI("<%= chkHyoujiGamen2.clientID %>");
            objchkHyoujiGamen3 = objEBI("<%= chkHyoujiGamen3.clientID %>");
            objchkHyoujiGamen4 = objEBI("<%= chkHyoujiGamen4.clientID %>");
            
            //hidden
            objKubunVal = objEBI("<%= kubunVal.clientID %>");
            objHdnKensakuInfo = objEBI("<%= kensakuInfo.clientID %>");
            objHdnKensakuStatus = objEBI("<%= HiddenKensakuInfoStyle.clientID %>");

            //検索実行用
            objMaxSearchCount = objEBI("<%= maxSearchCount.clientID %>");
            objSearch = objEBI("<%= search.clientID %>");
            
            //画面遷移用
            objSendBtn = objEBI("<%= btnSend.clientID %>");
            objSendKubun = objEBI("<%= sendKubun.clientID %>");
            objSendHoshoushoNo = objEBI("<%= sendHoshoushoNo.clientID %>");
            objSendTargetWin = objEBI("<%= sendTargetWin.clientID %>");
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
            var adjustWidth = 530;

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
                sendGyoumuGamen(objSelectedTr,intGamen,2);
            }
            return;
        }
        
        /**
         * 別ウィンドウアイコンをクリックした際の処理
         * @param objOnCklick
         * @param intGamen[1:受,2:報,3:工,4:保]
         * @return
         */
        function returnSelectValueOtherWin(objOnCklick,intGamen){
            objSendTargetWin.value = "_blank";
            sendGyoumuGamen(objOnCklick.parentNode.parentNode,intGamen,1);
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
                if(objchkHyoujiGamen1.checked == true){
                    objSendForm.action="<%=UrlConst.IRAI_KAKUNIN %>";
                    objSendForm.submit();
                }
                //2:報
                if(objchkHyoujiGamen2.checked == true){
                    objSendForm.action="<%=UrlConst.HOUKOKUSYO %>";
                    objSendForm.submit();
                }
                //3:工
                if(objchkHyoujiGamen3.checked == true){
                    objSendForm.action="<%=UrlConst.KAIRYOU_KOUJI %>";
                    objSendForm.submit();
                }
                //4:保
                if(objchkHyoujiGamen4.checked == true){
                    objSendForm.action="<%=UrlConst.HOSYOU %>";
                    objSendForm.submit();
                }
            }else{
                return false;
            }
        }
        
        /**
         * 一括変更画面起動パラメータ生成
         * @return
         */
        function setCheckedIkkatu(){
        
            var tableData1 = objEBI("<%=TableDataTable1.clientID %>");
            var arrSakiTr = tableData1.tBodies[0].rows;
            var objTd = null;
            var arrInput = null;
            var bukkenCount = 0;

            objSendVal_Kubun.value = "";
            objSendVal_HosyousyoNo.value = "";

            for ( var tri = 0; tri < arrSakiTr.length; tri++) {
                objTd = arrSakiTr[tri].cells[1];
                arrInput = getChildArr(objTd,"INPUT");
                for ( var ar = 0; ar < arrInput.length; ar++) {
                    if(arrInput[ar].type == "checkbox" && arrInput[ar].checked){
                        var arrVal = arrInput[ar].value.split(sepStr);
                        objSendVal_Kubun.value += arrVal[0] + sepStr;
                        objSendVal_HosyousyoNo.value += arrVal[1] + sepStr;
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
            if(bukkenCount > objEBI("<%=HiddenIkkatuKidouMax.clientID %>").value){
                alert("<%= Messages.MSG124E %>".replace("{0}",bukkenCount).replace("{1}",objEBI("<%=HiddenIkkatuKidouMax.clientID %>").value));
                objSendVal_Kubun.value = "";
                objSendVal_HosyousyoNo.value = "";
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
                objKubun2.selectedIndex = 0;
                objKubun3.selectedIndex = 0;
                objKubun1.disabled = true;
                objKubun2.disabled = true;
                objKubun3.disabled = true;
                return;
            }else{
                objKubun1.disabled = false;
                objKubun2.disabled = false;
                objKubun3.disabled = false;
                
                if(objKubun1.value != ""){
                    objKubunVal.value = objKubun1.value;
                }
                if(objKubun2.value != ""){
                    if(objKubunVal.value == ""){
                        objKubunVal.value = objKubun2.value;
                    }else{
                        objKubunVal.value += "," + objKubun2.value;
                    }
                }
                if(objKubun3.value != ""){
                    if(objKubunVal.value == ""){
                        objKubunVal.value = objKubun3.value;
                    }else{
                        objKubunVal.value += "," + objKubun3.value;
                    }
                }
            }
        }

        /**
         * 「検索実行」押下時のチェック処理
         * @return
         */
        function checkJikkou(){

            if(!objKubunAll.checked && objKubunVal.value.Trim() == ""){
                alert("<%= Messages.MSG006E %>");
                objKubun1.focus();
                return false;
            }
            if(objKubunAll.checked && objRdoHanni0.checked && !checkInputJouken()){
                alert("<%= Messages.MSG008E %>");
                objKubunAll.focus();
                return false;
            }
            //番号 大小チェック
            if(!checkDaiSyou(objHoshouNoFrom,objHoshouNoTo,"番号"))return false;
            
            //工事売上年月日 大小チェック
            if(!checkDaiSyou(objKoujiUriageDateFrom,objKoujiUriageDateTo,"工事売上年月日"))return false;
            
            //工事完了予定日 大小チェック
            if(!checkDaiSyou(objKoujiKanryouYoteiDateFrom,objKoujiKanryouYoteiDateTo,"工事完了予定日"))return false;
            
            //依頼日 大小チェック
            if(!checkDaiSyou(objIraiDateFrom,objIraiDateTo,"依頼日"))return false;
            
            //調査希望日 大小チェック
            if(!checkDaiSyou(objTyousaKibouDateFrom,objTyousaKibouDateTo,"調査希望日"))return false;

            //調査希望日 大小チェック
            if(!checkDaiSyou(objTyousaKibouDateFrom,objTyousaKibouDateTo,"調査希望日"))return false;

            //調査実施日 大小チェック
            if(!checkDaiSyou(objTyousaJissiDateFrom,objTyousaJissiDateTo,"調査実施日"))return false;

            //保証書発行日 大小チェック
            if(!checkDaiSyou(objHosyousyoHakkouDateFrom,objHosyousyoHakkouDateTo,"保証書発行日"))return false;

            //承諾書調査日 大小チェック
            if(!checkDaiSyou(objSyoudakusyoTyousaDateFrom,objSyoudakusyoTyousaDateTo,"承諾書調査日"))return false;

            //計画書作成日 大小チェック
            if(!checkDaiSyou(objKeikakusyoSakuseiDateFrom,objKeikakusyoSakuseiDateTo,"計画書作成日"))return false;

            //保証書発行依頼書着日 大小チェック
            if(!checkDaiSyou(objHosyousyoHakkouIraisyoTyakuDateFrom,objHosyousyoHakkouIraisyoTyakuDateTo,"保証書発行依頼書着日"))return false;

            if(objMaxSearchCount.value == "max"){
                if(!confirm(("<%= Messages.MSG007C %>")))return false;
            }
            
            //検索実行
            objSearch.click();
        }
        
        /**
         * 区分、対象範囲以外の検索条件が入力されているかのチェック
         * @return true:一つでも入力がある/false:一つも入力が無い
         */
        function checkInputJouken(){
        
            if(objHoshouNoFrom.value!="")return true;
            if(objHoshouNoTo.value!="")return true;
            if(objKameitenCd.value!="")return true;
            if(objKameitenKana.value!="")return true;
            if(objKeiretuCd.value!="")return true;
            if(objTyousakaisyaCd.value!="")return true;
            if(objSeshuName.value!="")return true;
            if(objBukkenJyuusho12.value!="")return true;
            if(objKoujiKaishaCd.value!="")return true;
            if(objKoujiUriageDateFrom.value!="")return true;
            if(objKoujiUriageDateTo.value!="")return true;
            if(objKoujiKanryouYoteiDateFrom.value!="")return true;
            if(objKoujiKanryouYoteiDateTo.value!="")return true;
            if(objIraiDateFrom.value!="")return true;
            if(objIraiDateTo.value!="")return true;
            if(objYoyakuZumi.checked!=false)return true;
            if(objTyousaKibouDateFrom.value!="")return true;
            if(objTyousaKibouDateTo.value!="")return true;
            if(objBikou.value!="")return true;
            if(objTyousaJissiDateFrom.value!="")return true;
            if(objTyousaJissiDateTo.value!="")return true;
            if(objHosyousyoHakkouDateFrom.value!="")return true;
            if(objHosyousyoHakkouDateTo.value!="")return true;
            if(objSyoudakusyoTyousaDateFrom.value!="")return true;
            if(objSyoudakusyoTyousaDateTo.value!="")return true;
            if(objKeikakusyoSakuseiDateFrom.value!="")return true;
            if(objKeikakusyoSakuseiDateTo.value!="")return true;
            if(objHosyousyoHakkouIraisyoTyakuDateFrom.value!="")return true;
            if(objHosyousyoHakkouIraisyoTyakuDateTo.value!="")return true;
            
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
         * 対象範囲ラジオボタンクリック時処理
         * @return true/false
         */
        function actNoHaniRadio(obj){
            //番号範囲指定をクリア
            objHoshouNoFrom.value = "";
            objHoshouNoTo.value = "";
            return true;
        }

        /**
         * 番号自動設定ラジオボタンクリック時処理
         * @return true/false
         */
        function actNoAutoRadio(obj,fromDate,toDate){
            //番号範囲指定を設定
            if(obj.checked){
                objHoshouNoFrom.value = fromDate;
                objHoshouNoTo.value = toDate;
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
         * Allクリア処理後に実行されるファンクション
         * @return 
         */
        function funcAfterAllClear(obj){
            objKubunAll.click();
            objRdoHanni1.click();
            //objHakiTaisyou.click();
            objMaxSearchCount.selectedIndex = 1;
            //検索条件 非表示→表示
            if(objHdnKensakuInfo.style.display == 'none'){
                objHdnKensakuInfo.style.display = 'inline';
                objHdnKensakuStatus.value = 'inline';
            }
            objKubunAll.focus();
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
        
    </script>

    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tbody>
            <tr>
                <th>
                    物件検索</th>
                <th style="text-align: right;">
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
                <td colspan="5" class="hissu">
                    <asp:DropDownList ID="cmbKubun_1" runat="server" TabIndex="10">
                    </asp:DropDownList>
                    <asp:DropDownList ID="cmbKubun_2" runat="server" TabIndex="10">
                    </asp:DropDownList>
                    <asp:DropDownList ID="cmbKubun_3" runat="server" TabIndex="10">
                    </asp:DropDownList>
                    &nbsp;全区分<input id="kubun_all" type="checkbox" runat="server" tabindex="10" />
                    <input type="hidden" id="kubunVal" runat="server" /></td>
                <td class="koumokuMei">
                    東西</td>
                <td colspan="">
                    <input name="rdo_SetTouzaiFlg" value="0" type="radio" id="rdo_TouzaiFlg_0" runat="server"
                        tabindex="10" />東日本
                    <input name="rdo_SetTouzaiFlg" value="1" type="radio" id="rdo_TouzaiFlg_1" runat="server"
                        tabindex="10" />西日本
                    <input name="rdo_SetTouzaiFlg" value="NULL" type="radio" id="rdo_TouzaiFlg_dummy" runat="server"
                        style="display: none;" />
                </td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    対象範囲</td>
                <td>
                    <input name="rdo_SetHoshouNo" id="rdo_hanni0" value="all" type="radio" runat="server"
                        tabindex="10" />全て
                    <input name="rdo_SetHoshouNo" id="rdo_hanni1" value="all2" type="radio" runat="server"
                        checked tabindex="10" /><label id="haniYear" runat="server"></label>年以降</td>
                <td class="koumokuMei">
                    番号自動設定</td>
                <td colspan="5">
                    <input name="rdo_SetHoshouNo" value="12" type="radio" id="hoshouNoSet_12" runat="server"
                        tabindex="10" />過去12ヶ月
                    <input name="rdo_SetHoshouNo" value="6" type="radio" id="hoshouNoSet_6" runat="server"
                        tabindex="10" />過去6ヶ月
                    <input name="rdo_SetHoshouNo" value="3" type="radio" id="hoshouNoSet_3" runat="server"
                        tabindex="10" />過去3ヶ月
                    <input name="rdo_SetHoshouNo" value="2" type="radio" id="hoshouNoSet_2" runat="server"
                        tabindex="10" />過去2ヶ月
                    <input name="rdo_SetHoshouNo" value="0" type="radio" id="hoshouNoSet_0" runat="server"
                        tabindex="10" />当月</td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    番号</td>
                <td>
                    <input id="hoshouNo_from" runat="server" maxlength="10" style="width: 70px;" class="codeNumber"
                        onblur="checkNumber(this);" onchange="if(checkNumber(this))setHosyouNoTo(this);"
                        tabindex="10" />～<input id="hoshouNo_to" runat="server" maxlength="10" style="width: 70px;"
                            class="codeNumber" onblur="checkNumber(this);" tabindex="10" /></td>
                <td class="koumokuMei">
                    分譲コード
                </td>
                <td>
                    <input id="TextBunjouCd" type="text" runat="server" maxlength="10" style="width: 70px;
                        ime-mode: disabled;" />
                </td>
                <td class="koumokuMei">
                    物件名寄コード
                </td>
                <td>
                    <input id="TextNayoseCd" type="text" runat="server" maxlength="11" style="width: 90px;
                        ime-mode: disabled;" />
                </td>
                <td class="koumokuMei">
                    営業所</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Conditional" runat="server" RenderMode="Inline">
                        <ContentTemplate>
                            <input id="eigyousyoCd" runat="server" maxlength="5" style="width: 40px;" class="codeNumber"
                                tabindex="10" /><input id="eigyousyoSearch" runat="server" type="button" value="検索"
                                    class="gyoumuSearchBtn" tabindex="10" onserverclick="eigyousyoSearch_ServerClick" />
                            <input id="eigyousyoNm" runat="server" class="readOnlyStyle" readonly="readonly"
                                style="width: 5em" tabindex="-1" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
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
                                tabindex="10" />&nbsp;
                            <input id="kameitenNm" runat="server" class="readOnlyStyle" readonly="readonly" style="width: 12em"
                                tabindex="-1" />&nbsp;
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel_KtTorikesi" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="kameitenSearch" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:TextBox ID="TextTorikesiRiyuu" runat="server" Width="80px"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="koumokuMei">
                    加盟店カナ</td>
                <td colspan="">
                    <input id="kameitenKana" runat="server" maxlength="40" style="width: 150px; ime-mode: active;"
                        tabindex="10" /></td>
                <td class="koumokuMei">
                    系列</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server" RenderMode="Inline">
                        <ContentTemplate>
                            <input id="keiretuCd" runat="server" maxlength="5" style="width: 40px;" class="codeNumber"
                                tabindex="10" /><input id="keiretuSearch" runat="server" type="button" value="検索"
                                    class="gyoumuSearchBtn" tabindex="10" />
                            <input id="keiretuNm" runat="server" class="readOnlyStyle" readonly="readonly" style="width: 5em"
                                tabindex="-1" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    施主名</td>
                <td colspan="3">
                    <input id="seshuName" runat="server" maxlength="50" style="width: 25em; ime-mode: active;"
                        tabindex="10" /></td>
                <td class="koumokuMei">
                    備考</td>
                <td colspan="3">
                    <input id="TextBikou" runat="server" maxlength="256" style="width: 25em; ime-mode: active;"
                        tabindex="10" /></td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    物件住所１+２</td>
                <td colspan="3">
                    <input size="100" id="bukkenJyuusho12" runat="server" maxlength="64" style="width: 25em;
                        ime-mode: active;" tabindex="10" /></td>
                <td class="koumokuMei">
                    依頼日</td>
                <td>
                    <input id="TextIraiDateFrom" runat="server" maxlength="10" class="date" onblur="checkDate(this);"
                        tabindex="10" />～<input id="TextIraiDateTo" runat="server" maxlength="10" class="date"
                            onblur="checkDate(this);" tabindex="10" /></td>
                <td class="koumokuMei">
                    予約済</td>
                <td>
                    <input id="CheckYoyakuZumi" value="1" type="checkbox" runat="server" tabindex="10" />予約済のみ</td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    調査希望日</td>
                <td class="date">
                    <input id="TextTyousaKibouDateFrom" runat="server" maxlength="10" class="date" onblur="checkDate(this);"
                        tabindex="21" />～<input id="TextTyousaKibouDateTo" runat="server" maxlength="10"
                            class="date" onblur="checkDate(this);" tabindex="22" /></td>
                <td class="koumokuMei">
                    調査実施日</td>
                <td class="date" colspan="2">
                    <input id="TextTyousaJissiDateFrom" runat="server" maxlength="10" class="date" onblur="checkDate(this);"
                        tabindex="25" />～<input id="TextTyousaJissiDateTo" runat="server" maxlength="10"
                            class="date" onblur="checkDate(this);" tabindex="26" /></td>
                <td class="koumokuMei" colspan="">
                    保証書発行日</td>
                <td class="date" colspan="2">
                    <input id="TextHosyousyoHakkouDateFrom" runat="server" maxlength="10" class="date"
                        onblur="checkDate(this);" tabindex="29" />～<input id="TextHosyousyoHakkouDateTo"
                            runat="server" maxlength="10" class="date" onblur="checkDate(this);" tabindex="30" /></td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    承諾書調査日</td>
                <td class="date">
                    <input id="TextSyoudakusyoTyousaDateFrom" runat="server" maxlength="10" class="date"
                        onblur="checkDate(this);" tabindex="23" />～<input id="TextSyoudakusyoTyousaDateTo"
                            runat="server" maxlength="10" class="date" onblur="checkDate(this);" tabindex="24" /></td>
                <td class="koumokuMei">
                    計画書作成日</td>
                <td class="date" colspan="2">
                    <input id="TextKeikakusyoSakuseiDateFrom" runat="server" maxlength="10" class="date"
                        onblur="checkDate(this);" tabindex="27" />～<input id="TextKeikakusyoSakuseiDateTo"
                            runat="server" maxlength="10" class="date" onblur="checkDate(this);" tabindex="28" /></td>
                <td class="koumokuMei" colspan="">
                    保証書発行依頼書着日</td>
                <td class="date" colspan="2">
                    <input id="TextHosyousyoHakkouIraisyoTyakuDateFrom" runat="server" maxlength="10"
                        class="date" onblur="checkDate(this);" tabindex="31" />～<input id="TextHosyousyoHakkouIraisyoTyakuDateTo"
                            runat="server" maxlength="10" class="date" onblur="checkDate(this);" tabindex="32" /></td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    調査会社</td>
                <td colspan="3">
                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server" RenderMode="Inline">
                        <ContentTemplate>
                            <input id="tyousakaisyaCd" runat="server" maxlength="7" style="width: 45px;" class="codeNumber"
                                tabindex="40" />
                            <input id="tyousakaisyaSearch" runat="server" type="button" value="検索" class="gyoumuSearchBtn"
                                tabindex="40" />&nbsp;
                            <input id="tyousakaisyaNm" runat="server" class="readOnlyStyle" style="width: 15em"
                                readonly="readOnly" tabindex="-1" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="koumokuMei">
                    工事会社</td>
                <td colspan="3">
                    <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server" RenderMode="Inline">
                        <ContentTemplate>
                            <input id="kojiKaishaCd" runat="server" maxlength="7" style="width: 45px;" class="codeNumber"
                                tabindex="40" />
                            <input id="kojiKaishaSearch" runat="server" type="button" value="検索" class="gyoumuSearchBtn"
                                tabindex="40" />&nbsp;
                            <input id="kojiKaishaNm" runat="server" class="readOnlyStyle" style="width: 15em"
                                readonly="readOnly" tabindex="-1" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    工事売上年月日</td>
                <td colspan="2" style="border-right: none;">
                    <input id="TextKoujiUriageDateFrom" runat="server" maxlength="10" class="date" onblur="checkDate(this);"
                        tabindex="40" />～<input id="TextKoujiUriageDateTo" runat="server" maxlength="10"
                            class="date" onblur="checkDate(this);" tabindex="40" /></td>
                <td style="border-left: none;">
                    <img src="images/spacer.gif" alt="" style="width: 70px; height: 0px;" /></td>
                <td class="koumokuMei">
                    工事完了予定日</td>
                <td class="date">
                    <input id="TextKoujiKanryouYoteiDateFrom" runat="server" maxlength="10" class="date"
                        onblur="checkDate(this);" tabindex="40" />～<input id="TextKoujiKanryouYoteiDateTo"
                            runat="server" maxlength="10" class="date" onblur="checkDate(this);" tabindex="40" /></td>
                <td class="koumokuMei">
                    契約NO
                </td>
                <td>
                    <input id="TextKeiyakuNo" style="width: 150px; ime-mode: inactive;" runat="server" maxlength="20"
                            class="codeNumber" tabindex="40"/>
                </td>
            </tr>
        </tbody>
        <tbody id="kensakuJikkou" style="display: inline;">
            <tr class="tableSpacer">
                <td colspan="8">
                </td>
            </tr>
            <tr>
                <td colspan="8" rowspan="1" style="text-align: center">
                    <input id="chkHyoujiGamen1" name="chkHyoujiGamen1" value="1" type="checkbox" runat="server"
                        checked="checked" tabindex="40" />受注
                    <input id="chkHyoujiGamen2" name="chkHyoujiGamen2" value="2" type="checkbox" runat="server"
                        tabindex="40" />報告
                    <input id="chkHyoujiGamen3" name="chkHyoujiGamen3" value="3" type="checkbox" runat="server"
                        tabindex="40" />工事
                    <input id="chkHyoujiGamen4" name="chkHyoujiGamen4" value="4" type="checkbox" runat="server"
                        tabindex="40" />保証
                    <img src="images/spacer.gif" alt="" style="width: 30px; height: 0px;" />
                    検索上限件数
                    <select id="maxSearchCount" runat="server" tabindex="40">
                        <option value="10">10件</option>
                        <option value="100" selected="selected">100件</option>
                        <option value="max">無制限</option>
                    </select>
                    <input type="button" id="btnSearch" value="検索実行" runat="server" tabindex="40" />
                    <input type="button" id="search" value="検索実行btn" style="display: none" runat="server" />
                    <img src="images/spacer.gif" alt="" style="width: 30px; height: 0px;" />
                    <input id="CheckHakiTaisyou" value="0" type="checkbox" runat="server"
                        tabindex="40" />データ破棄種別有りは検索対象外
                </td>
            </tr>
        </tbody>
    </table>
    <table style="text-align: left; width: 100px;" border="0" cellpadding="0" cellspacing="0">
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
            <td>
                <img src="images/spacer.gif" alt="" style="width: 70px; height: 0px;" />
                <input type="hidden" id="HiddenIkkatuKidouMax" runat="server" />
                <input type="button" id="ButtonIkkatuKihon" runat="server" value="一括変更【物件基本情報】" style="width: 160px;"
                    onclick="returnSelectValueOtherWin(this,5)" />
                <input type="button" id="ButtonIkkatuTysSyouhin" runat="server" value="一括変更【調査商品情報】"
                    style="width: 160px;" onclick="returnSelectValueOtherWin(this,6)" />
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
                                <tr>
                                    <th style="width: 80px" class="unsortable">
                                        画面選択</th>
                                    <th style="width: 40px" class="unsortable">
                                        一括</th>
                                    <th style="width: 65px">
                                        破棄種別</th>
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
                                        住所</th>
                                    <th style="width: 82px;">
                                        加盟店コード</th>
                                    <th style="width: 82px;">
                                        加盟店取消</th>
                                    <th style="width: 180px;">
                                        加盟店名</th>
                                    <th style="width: 80px">
                                        依頼担当者</th>
                                    <th style="width: 80px">
                                        依頼日</th>
                                    <th style="width: 80px">
                                        調査希望日</th>
                                    <th style="width: 60px">
                                        予約済</th>
                                    <th style="width: 120px">
                                        調査会社コード</th>
                                    <th style="width: 140px">
                                        調査会社事業所コード</th>
                                    <th style="width: 150px">
                                        調査会社名</th>
                                    <th style="width: 200px">
                                        調査方法</th>
                                    <th style="width: 100px">
                                        承諾書調査日</th>
                                    <th style="width: 120px">
                                        調査工務店請求額</th>
                                    <th style="width: 120px">
                                        調査実請求額</th>
                                    <th style="width: 120px">
                                        調査承諾書金額</th>
                                    <th style="width: 80px">
                                        担当者名</th>
                                    <th style="width: 80px">
                                        承認者名</th>
                                    <th style="width: 80px">
                                        調査実施日</th>
                                    <th style="width: 200px">
                                        調査結果</th>
                                    <th style="width: 100px">
                                        計画書作成日</th>
                                    <th style="width: 100px">
                                        保証書発行日</th>
                                    <th style="width: 450px">
                                        備考</th>
                                    <th style="width: 80px">
                                        営業担当者</th>
                                    <th style="width: 110px">
                                        工事売上年月日</th>
                                    <th style="width: 110px">
                                        分譲コード</th>
                                    <th style="width: 110px">
                                        物件名寄コード</th>
                                    <th style="width: 80px">
                                        契約NO</th>
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

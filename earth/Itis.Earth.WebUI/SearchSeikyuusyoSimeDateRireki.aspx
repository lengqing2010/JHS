<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="SearchSeikyuusyoSimeDateRireki.aspx.vb" Inherits="Itis.Earth.WebUI.SearchSeikyuusyoSimeDateRireki"
    Title="EARTH 請求書締め日履歴照会" %>

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
        var gVarOyaSettouji = "ctl00_CPH1_";
        var gVarTr1 = "DataTable_resultTr1_";
        var gVarTr2 = "DataTable_resultTr2_";
        var gVarTdSentou = "DataTable_Sentou_Td_";
        var gVarChkTaisyou = "ChkTaisyou_";

        //画面表示部品
        var objSeikyuuSakiKbn = null;
        var objSeikyuuSakiCd = null;
        var objSeikyuuSakiBrc = null;   
        var objSeikyuuDateFrom = null;
        var objSeikyuuDateTo = null;        
        var objTorikesiTaisyou = null;
        var objSaisinSeikyuuSimeDate = null;
        var objSeikyuusyoNoFrom = null;
        var objSeikyuusyoNoTo = null;
        var objCheckAll = null;        

        //検索実行用
        var objMaxSearchCount = null;
        var objSearch = null;
        
        //画面遷移用
        var objSendVal_SimeDateRirekiPk = null;
        var objSendVal_UpdDatetime = null;
        var objSendVal_ChkedTaisyou = null;
        var objSendVal_KagamiUpdDatetime = null;        

        //アクション実行ボタン(検索実行,履歴取消)
        var gBtnSearch = null;
        var gBtnTorikesi = null;

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
            objSeikyuuSakiKbn = objEBI("<%= SelectSeikyuuSakiKbn.clientID %>");
            objSeikyuuSakiCd = objEBI("<%= TextSeikyuuSakiCd.clientID %>");
            objSeikyuuSakiBrc = objEBI("<%= TextSeikyuuSakiBrc.clientID %>");
            objSeikyuuDateFrom = objEBI("<%= TextSeikyuusyoHakkouDateFrom.clientID %>");
            objSeikyuuDateTo = objEBI("<%= TextSeikyuusyoHakkouDateTo.clientID %>");            
            objTorikesiTaisyou = objEBI("<%= CheckTorikesiTaisyou.clientID %>");
            objSaisinSeikyuuSimeDate = objEBI("<%= CheckSaisinSeikyuuSimeDate.clientID %>");
            objSeikyuusyoNoFrom = objEBI("<%= TextSeikyuusyoNoFrom.clientID %>"); 
            objSeikyuusyoNoTo = objEBI("<%= TextSeikyuusyoNoTo.clientID %>");
            objCheckAll = objEBI("<%= CheckAll.clientID %>");                        
   
            //検索実行用
            objMaxSearchCount = objEBI("<%= maxSearchCount.clientID %>");
            objSearch = objEBI("<%= search.clientID %>");
        
            //画面遷移用
            objSendVal_SimeDateRirekiPk = objEBI("<%= HiddenSimeDateRirekiPk.clientID %>");     
            objSendVal_UpdDatetime = objEBI("<%= HiddenUpdDatetime.clientID %>");
            objSendVal_ChkedTaisyou = objEBI("<%= HiddenChkedTaisyou.clientID %>");
            objSendVal_KagamiUpdDatetime = objEBI("<%= HiddenKagamiUpdDatetime.clientID %>");            

            //アクション実行ボタン
            gBtnSearch = "<%= EarthEnum.emSearchSeikyuuSimeDateRirekiBtnType.Search %>";
            gBtnTorikesi = "<%= EarthEnum.emSearchSeikyuuSimeDateRirekiBtnType.Torikesi %>";
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
            var adjustWidth = 290;                                          // 調整幅(大きい程、検索結果テーブルが狭くなる)

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

        
       /***********************************
        * 「検索実行」押下時のチェック処理
        ***********************************/
        function checkJikkou(varAction){    
                        
            //画面表示部品セット
            setGlobalObj();
            
            var varMsg = "";  
             
            //請求年月日 大小チェック
            if(!checkDaiSyou(objSeikyuuDateFrom,objSeikyuuDateTo,"請求年月日")){return false};
            
            //請求書NO 大小チェック
            if(!checkDaiSyou(objSeikyuusyoNoFrom,objSeikyuusyoNoTo,"請求書NO")){return false};   

            if(varAction == gBtnSearch){

                //表示件数「1000件」チェック
　              if(objMaxSearchCount.value == "1000"){
                    if(!confirm(("<%= Messages.MSG007C %>").replace("無制限","1000件")))return false;                    
                } 
                //検索実行
                objSearch.click();
            }
            return true;
        }
        
        /*********************************************
        * 戻り値がない為、同メソッドをオーバーライド
        *********************************************/
        function returnSelectValue(){
            return false;
        } 
        
        /*******************************************
        * Allクリア処理後に実行されるファンクション
        *******************************************/
        function funcAfterAllClear(obj){ 
            objMaxSearchCount.selectedIndex = 1;
            objTorikesiTaisyou.checked = true;
            objSaisinSeikyuuSimeDate.checked = true;
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
            var arrInput = null;
            var bukkenCount = 0;
            var objTmpId = null;
            var objTmpSeiNo = null;
            
            var ErrMsg = "";
            
            //Key情報をクリア
            ClearKeyInfo();

            for ( var tri = 0; tri < arrSakiTr1.length; tri++) {
                objTd = arrSakiTr1[tri].cells[0];
                          
                arrInput = getChildArr(objTd,"INPUT");               
                for ( var ar = 0; ar < arrInput.length; ar++) {
                    if(arrInput[ar].type == "checkbox" && arrInput[ar].checked){
                        objTmpId = arrInput[ar].id; //checkbox対象
                        objSendVal_ChkedTaisyou.value += objTmpId + sepStr + sepStr;
                        
                        objTmpId = arrInput[0].id; //hidden請求書締め日履歴テーブルPK
                        objTmpVal = objEBI(objTmpId);
                        objSendVal_SimeDateRirekiPk.value += objTmpVal.value + sepStr + sepStr;

                        objTmpId = arrInput[1].id; //hidden更新日時
                        objTmpVal = objEBI(objTmpId);
                        objSendVal_UpdDatetime.value += objTmpVal.value + sepStr + sepStr;                        
                        
                        objTmpId = arrInput[2].id; //hidden請求鑑更新日時
                        objTmpVal = objEBI(objTmpId);
                        objSendVal_KagamiUpdDatetime.value += objTmpVal.value + sepStr + sepStr;

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
                     
            //末尾のセパレータを除去
            RemoveSepStr();     
            return true;
        }
        
         /**
         * KEY情報をクリア処理
         * (請求書NO,更新日時,対象チェックボックス,CSV出力対象外)
         * @return
         */
        function ClearKeyInfo(){
            objSendVal_SimeDateRirekiPk.value = "";
            objSendVal_UpdDatetime.value = "";
            objSendVal_ChkedTaisyou.value = "";
            objSendVal_KagamiUpdDatetime.value = "";
        }
        
        /**
         * KEY情報の末尾のセパレータ文字列を除去する処理
         * @return
         */
        function RemoveSepStr(){
            objSendVal_SimeDateRirekiPk.value = objSendVal_SimeDateRirekiPk.value.replace(/\$\$\$\$\$\$$/, "");
            objSendVal_UpdDatetime.value = objSendVal_UpdDatetime.value.replace(/\$\$\$\$\$\$$/, "");
            objSendVal_ChkedTaisyou.value = objSendVal_ChkedTaisyou.value.replace(/\$\$\$\$\$\$$/, "");
            objSendVal_KagamiUpdDatetime.value = objSendVal_KagamiUpdDatetime.value.replace(/\$\$\$\$\$\$$/, "");
        }
               
        /**
        * 請求書NO To自動セット
        * @return true/false
        */
        function setFromTo(obj){    
            var strval = obj.value;
            obj.value = paddingStr(strval,15,'0'); 
                    
            if(obj.id == objSeikyuusyoNoFrom.id && objSeikyuusyoNoTo.value == ""){                          
                objSeikyuusyoNoTo.value = obj.value;
            }
            return true;
        }
        
    </script>
    <input type="hidden" id="HiddenSimeDateRirekiPk" runat="server" /><%--請求書締め日履歴テーブルのPK--%>
    <input type="hidden" id="HiddenUpdDatetime" runat="server" /><%--更新日時--%>
    <input type="hidden" id="HiddenChkedTaisyou" runat="server" /><%--チェック済み対象チェックボックス--%>
    <input type="hidden" id="HiddenKagamiUpdDatetime" runat="server" /><%--請求鑑レコードの更新日時--%>
    <%-- 画面タイトル --%>
    <table style="text-align: left;" border="0" cellpadding="0" cellspacing="2" class="titleTable">
        <tr>
            <th style="text-align: left; width: 200px;">
                請求書締め日履歴照会</th>
            <th>
                <input type="button" id="ButtonReturn" value="戻る" runat="server" style="width: 100px;"
                    tabindex="10" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                  
                <input type="button" id="ButtonRirekiTorikesi" value="履歴取消" runat="server" style="background-color: fuchsia;
                    width: 100px;" tabindex="10" /><input type="button" id="ButtonHiddenTorikesi" value="取消実行btn"
                        style="display: none" runat="server" />&nbsp;
            </th>
        </tr>
    </table>
    <br />
    <table style="text-align: left;" class="mainTable" cellpadding="2">
        <thead>
            <tr>
                <th class="tableTitle" colspan="8" rowspan="1">
                    検索条件
                    <input id="btnClearWin" value="クリア" type="reset" class="button" tabindex="20" /></th>
            </tr>
        </thead>
        <tbody>
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
                                tabindex="30" />&nbsp;
                            <input id="TextSeikyuuSakiMei" runat="server" class="readOnlyStyle" readonly="readonly"
                                style="width: 190px" tabindex="-1" />&nbsp;
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="koumokuMei">
                    請求先名カナ</td>
                <td class="codeNumber">
                    <input id="TextSeikyuuSakiMeiKana" runat="server" maxlength="40" style="ime-mode: active;
                        width: 244px;" tabindex="30" />
                </td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    請求書発行日</td>
                <td colspan="3">
                    <input id="TextSeikyuusyoHakkouDateFrom" runat="server" maxlength="10" style="width: 75px;"
                        onblur="checkDate(this);" class="codeNumber" tabindex="40" />&nbsp;～
                    <input id="TextSeikyuusyoHakkouDateTo" runat="server" maxlength="10" style="width: 75px;"
                        onblur="checkDate(this);" class="codeNumber" tabindex="40" /></td>                
                <td class="koumokuMei">
                    請求書NO</td>
                <td id="TdSeikyuusyoNo">                
                    <input id="TextSeikyuusyoNoFrom" runat="server" maxlength="15" style="width: 105px;" 
                        class="codeNumber" tabindex="40" />&nbsp;～
                    <input id="TextSeikyuusyoNoTo" runat="server" maxlength="15" style="width: 105px;" 
                        class="codeNumber" tabindex="40" /></td> 
            </tr>
            <tr>
                <td style="text-align: center;" colspan="6" rowspan="1">
                    検索上限件数<select id="maxSearchCount" runat="server" tabindex="50">
                        <option value="10">10件</option>
                        <option value="100" selected="selected">100件</option>
                        <option value="1000">1000件</option>
                    </select>
                    <input id="btnSearch" value="検索実行" type="button" runat="server" tabindex="50" style="padding-top: 2px;" />
                    <input type="button" id="search" value="検索実行btn" style="display: none" runat="server"
                        tabindex="-1" />
                    <input id="CheckTorikesiTaisyou" value="0" type="checkbox" runat="server" checked="checked"
                        tabindex="50" />取消は検索対象外
                    <input id="CheckSaisinSeikyuuSimeDate" value="0" type="checkbox" runat="server" checked="checked" 
                        tabindex="50" />最終請求書のみ表示
                </td>
            </tr>
        </tbody>
    </table>
    <table style="height:30px">
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
    <table cellpadding="0" cellspacing="0" >
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
                                    <th style="width: 70px;">
                                        履歴NO</th>
                                    <th style="width: 40px;">
                                        取消</th>
                                    <th style="width: 95px;">
                                        請求先コード</th>
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
                                    <th style="width: 187px;">
                                        請求先名</th>
                                    <th style="width: 187px;">
                                        請求先名2</th>
                                    <th style="width: 94px;">
                                        請求書発行日</th>
                                    <th style="width: 74px;">
                                        請求金額</th>
                                    <th style="width: 120px;">
                                        請求書NO</th>
                                    <th style="width: 88px;">
                                        全対象フラグ</th>
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

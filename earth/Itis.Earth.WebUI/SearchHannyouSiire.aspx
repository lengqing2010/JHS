<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="SearchHannyouSiire.aspx.vb" Inherits="Itis.Earth.WebUI.SearchHannyouSiire"
    Title="EARTH 汎用仕入データ照会" %>

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
            if(window.name != "<%=EarthConst.MAIN_WINDOW_NAME %>") window.resizeTo(1010,800);
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
        var gVarHdnSeikyuusyoNo = "HdnUniNo_";
        var gVarChkTaisyou = "CheckTaisyou_";
        
        //画面遷移用
        var objSendBtn = null;
        var objSendTargetWin = null;
        
        var objSelectedTr = null;
        var objSendVal_NyuukinNo = null;
        
        var varAction = null;
        
        //画面表示部品
        var objSyouhinCd = null;
        var objAddDateFrom = null;
        var objAddDateTo = null;
        var objSiireDateFrom = null;
        var objSiireDateTo = null;
        var objDenpyouSiireDateFrom = null;
        var objDenpyouSiireDateTo = null;
        var objTorikesiTaisyou = null;
        //検索実行用
        var objMaxSearchCount = null;
        var objSearch = null;


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
            
            //検索結果が1件のみの場合、値を戻す処理を実行
            if (objEBI("<%= firstSend.clientID %>").value != ""){
                returnSelectValue(objEBI(objEBI("<%= firstSend.clientID %>").value));
            }          
        }
        
       /*********************************************
        * 戻り値がない為、同メソッドをオーバーライド
        *********************************************/
        function returnSelectValue(objSelectedTr){
            var varTrId = objSelectedTr.id;
            var varRow = varTrId.replace(gVarOyaSettouji,"");
            varRow = varRow.replace(gVarTr1,"");
            varRow = varRow.replace(gVarTr2,"");
            varRow = varRow.replace(gVarTdSentou,"");
            
            var varHdn = _d.getElementById(gVarOyaSettouji + gVarHdnSeikyuusyoNo + varRow);
           
            PopupSyuusei(varHdn.value);
        }

        //子画面呼出処理
        function PopupSyuusei(strUniqueNo){    
            //オブジェクトの再読込(Ajax再描画対応)
            objSendTargetWin = objEBI("<%= sendTargetWin.clientID %>");
            if(objSendTargetWin.value == "")objSendTargetWin.value="<%=EarthConst.MAIN_WINDOW_NAME %>";
            
            //オープン対象の画面を指定
            varAction = "<%=UrlConst.HANNYOU_SIIRE_SYUUSEI %>";
            
            //<!-- 画面引渡し情報 -->
            objSendForm = objEBI("searchForm");
            //区分+番号+入力NO
            var objSendVal_SearchTerms = objEBI("sendSearchTerms");
            objSendVal_SearchTerms.value = strUniqueNo;
                        
            var varWindowName = "HannyouSiireSyuusei";
            objSrchWin = window.open("about:Blank", varWindowName, "menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes");
                        
            //値セット
            objSendTargetWin.value = varWindowName;
            objSendForm.target=objSendTargetWin.value;
                            
            objSendForm.action = varAction;
            objSendForm.submit();
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
            objSyouhinCd = objEBI("<%= TextSyouhinCd.clientID %>");
            objAddDateFrom = objEBI("<%= TextAddDateFrom.clientID %>");
            objAddDateTo = objEBI("<%= TextAddDateTo.clientID %>");
            objSiireDateFrom = objEBI("<%= TextSiireDateFrom.clientID %>");
            objSiireDateTo = objEBI("<%= TextSiireDateTo.clientID %>");
            objDenpyouSiireDateFrom = objEBI("<%= TextDenpyouSiireDateFrom.clientID %>");
            objDenpyouSiireDateTo = objEBI("<%= TextDenpyouSiireDateTo.clientID %>");
            objTorikesiTaisyou = objEBI("<%= CheckTorikesiTaisyou.clientID %>");
            //検索実行用
            objMaxSearchCount = objEBI("<%= maxSearchCount.clientID %>");
            objSearch = objEBI("<%= search.clientID %>");
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
            var minHeight = 110;                                            // ウィンドウリサイズ時の検索結果テーブルに設定する最低高さ
            var adjustHeight = 39;                                          // 調整高さ(大きい程、検索結果テーブルが低くなる)
            var adjustWidth = 631;                                          // 調整幅(大きい程、検索結果テーブルが狭くなる)

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


        /*******************************************
         * Allクリア処理後に実行されるファンクション
         *******************************************/
        function funcAfterAllClear(obj){
            objTorikesiTaisyou.click();
            objMaxSearchCount.selectedIndex = 1;
            objSyouhinCd.focus();
        }


        /***********************************
         * 「検索実行」押下時のチェック処理
         ***********************************/
        function checkJikkou(){
                        
            //登録年月日 大小チェック
            if(!checkDaiSyou(objAddDateFrom,objAddDateTo,"登録年月日"))return false;
            
            //仕入年月日 大小チェック
            if(!checkDaiSyou(objSiireDateFrom,objSiireDateTo,"仕入年月日"))return false;
            
            //伝票仕入年月日 大小チェック
            if(!checkDaiSyou(objDenpyouSiireDateFrom,objDenpyouSiireDateTo,"伝票仕入年月日"))return false;

            //表示件数「無制限」チェック
            if(objMaxSearchCount.value == "max"){
                if(!confirm(("<%= Messages.MSG007C %>")))return false;
            }

            //検索実行
            objSearch.click();
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
    </script>

    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tbody>
            <tr>
                <th style="text-align: left; width: 160px;">
                    汎用仕入データ照会</th>
                <th>
                    <input id="ButtonSinki" value="新規登録" type="button" runat="server" onclick="PopupSyuusei(0)" tabindex="10" />&nbsp;&nbsp;
                    <input id="BtnCsvInput" value="CSV取込" type="button" runat="server" tabindex="10" />
                </th>
            </tr>
        </tbody>
    </table>
    <br />
    <table style="text-align: left;" class="mainTable" cellpadding="2">
        <thead>
            <tr>
                <th class="tableTitle" colspan="6" rowspan="1">
                    検索条件
                    <input id="btnClearWin" value="クリア" type="reset" class="button" tabindex="10" /></th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td class="koumokuMei">
                    商品</td>
                <td colspan="2">
                    <asp:UpdatePanel ID="UpdatePanel_syouhin" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <ContentTemplate>
                            <input id="TextSyouhinCd" runat="server" maxlength="8" style="width: 60px;" class="codeNumber" tabindex="20" />
                            <input id="btnSyouhinSearch" runat="server" type="button" value="検索" class="gyoumuSearchBtn"
                                onserverclick="btnSyouhinSearch_ServerClick" tabindex="20" />&nbsp;
                            <input id="TextHinmei" runat="server" class="readOnlyStyle" readonly="readonly" style="width: 228px"
                                tabindex="-1" />&nbsp;
                            <input type="hidden" id="hdnSyouhinType" runat="server" tabindex="20" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="koumokuMei">
                    登録年月日</td>
                <td colspan="2" class="date">
                    <input id="TextAddDateFrom" runat="server" maxlength="10" class="date" onblur="checkDate(this);" tabindex="20" />&nbsp;～&nbsp;<input
                        id="TextAddDateTo" runat="server" maxlength="10" class="date" onblur="checkDate(this);" tabindex="20" /></td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    調査会社</td>
                <td colspan="2" class="codeNumber">
                    <asp:UpdatePanel ID="UpdatePanel_TysKaisya" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <ContentTemplate>
                            <input id="TextTysKaisyaCd" runat="server" maxlength="7" style="width: 45px;" class="codeNumber" tabindex="30" />
                            <input id="ButtonTysKaisyaSearch" runat="server" type="button" value="検索" class="gyoumuSearchBtn"
                                onserverclick="ButtonTysKaisyaSearch_ServerClick" tabindex="30" />&nbsp;
                            <input id="TextTysKaisyaMei" runat="server" class="readOnlyStyle" style="width: 15em"
                                readonly="readOnly" tabindex="-1" />
                            <input type="hidden" id="HiddenKameitenCd" runat="server" tabindex="30" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="koumokuMei">
                    調査会社名カナ</td>
                <td colspan="2" class="codeNumber">
                    <input id="TextTysKaisyaMeiKana" runat="server" maxlength="40" style="ime-mode: active;
                        width: 175px;" tabindex="30" />
                </td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    仕入年月日</td>
                <td colspan="2" class="date">
                    <input id="TextSiireDateFrom" runat="server" maxlength="10" class="date" onblur="checkDate(this);" tabindex="40" />&nbsp;～&nbsp;<input
                        id="TextSiireDateTo" runat="server" maxlength="10" class="date" onblur="checkDate(this);" tabindex="40" /></td>
                <td class="koumokuMei">
                    伝票仕入年月日</td>
                <td colspan="2" class="date">
                    <input id="TextDenpyouSiireDateFrom" runat="server" maxlength="10" class="date" onblur="checkDate(this);" tabindex="40" />&nbsp;～&nbsp;<input
                        id="TextDenpyouSiireDateTo" runat="server" maxlength="10" class="date" onblur="checkDate(this);" tabindex="40" /></td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    区分</td>
                <td colspan="2">
                    <asp:TextBox ID="TextKbn" runat="server" MaxLength="1" Style="width: 20px;" CssClass="codeNumber" tabindex="50" />
                </td>
                <td class="koumokuMei">
                    番号</td>
                <td colspan="3">
                    <asp:TextBox ID="TextHosyousyoNo" runat="server" MaxLength="10" Style="width: 72px;" CssClass="codeNumber" tabindex="50" />
                </td>
                </tr>
            <tr>
            <tr>
                <td style="text-align: center;" colspan="7" rowspan="1">
                    検索上限件数
                    <select id="maxSearchCount" runat="server" tabindex="60" >
                        <option value="10">10件</option>
                        <option value="100" selected="selected">100件</option>
                        <option value="max">無制限</option>
                    </select>
                    <input id="btnSearch" value="検索実行" type="button" runat="server" tabindex="60" />
                    <input type="button" id="search" value="検索実行btn" style="display: none" runat="server" tabindex="60" />
                    <input id="CheckTorikesiTaisyou" value="0" type="checkbox" runat="server" checked="checked" tabindex="60" />取消は検索対象外
                </td>
            </tr>
        </tbody>
    </table>
    <input type="hidden" id="returnTargetIds" runat="server" />
    <input type="hidden" id="afterEventBtnId" runat="server" />
    <input type="hidden" id="firstSend" runat="server" />
    <input id="search_shouhin23" runat="server" type="hidden" />
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
                                <tr>
                                    <th style="width: 50px;">
                                        仕入NO</th>
                                    <th style="width: 90px;">
                                        調査会社コード</th>
                                    <th style="width: 195px;">
                                        調査会社名</th>
                                    <th style="width: 70px;">
                                        商品コード</th>
                                    <th style="width: 195px;">
                                        商品名</th>
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
                                    <th style="width: 95px;">
                                        税込仕入金額</th>
                                    <th style="width: 90px;">
                                        仕入年月日</th>
                                    <th style="width: 100px;">
                                        伝票仕入年月日</th>
                                    <th style="width: 35px;">
                                        区分</th>
                                    <th style="width: 80px;">
                                        番号</th>
                                    <th style="width: 140px;">
                                        施主名</th>  
                                    <th style="width: 180px;">
                                        摘要</th>
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


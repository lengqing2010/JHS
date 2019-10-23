<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="SearchSiharaiData.aspx.vb" Inherits="Itis.Earth.WebUI.SearchSiharaiData"
    Title="EARTH 支払伝票照会" %>

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
        
        /*====================================
         *グローバル変数宣言（画面部品）
         ====================================*/
             
        //画面表示部品
        var objShriDateFrom = null;
        var objShriDataTo = null;
        var objDenNoFrom = null;
        var objDenNoTo = null;
        var objTysKaisyaCd = null;       
        var objSkkJigyousyoCd = null;
        var objSkkShriSakiCd = null;
        var objSaisinDenpyou = null;
        //検索実行用
        var objMaxSearchCount = null;
        var objSearch = null;
        var objCsv = null;

        /*************************************
         * onload時の追加処理
         *************************************/
        function funcAfterOnload() {
            //画面表示部品セット
            setGlobalObj();
            
            //検索結果テーブル ソート設定
            sortables_init(); 

            //検索結果テーブル 各種レイアウト設定
            settingResultTable();
                         
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
            objCsvOutPutFlg.value = ""
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
        
       /*============================================
        * 画面表示部品オブジェクトをグローバル変数化
        ============================================*/
        function setGlobalObj() {
            //画面表示部品
            objShriDateFrom = objEBI("<%= TextShriDateFrom.clientID %>");
            objShriDataTo = objEBI("<%= TextShriDateTo.clientID %>");
            objDenNoFrom = objEBI("<%= TextDenNoFrom.clientID %>");
            objDenNoTo = objEBI("<%= TextDenNoTo.clientID %>");
            objTysKaisyaCd = objEBI("<%= TextTysKaisyaCd.clientID %>");
            objSkkJigyousyoCd = objEBI("<%= TextSkkJigyousyoCd.clientID %>");
            objSkkShriSakiCd = objEBI("<%= TextSkkShriSakiCd.clientID %>");
            objSaisinDenpyou = objEBI("<%= CheckSaisinDenpyou.clientID %>");
            //検索実行用
            objMaxSearchCount = objEBI("<%= maxSearchCount.clientID %>");
            objSearch = objEBI("<%= search.clientID %>");
            // CSV出力用
            objCsv = objEBI("<%= BtnHiddenCsv.clientID %>");
            objCsvOutPutFlg = objEBI("<%= HiddenCsvOutPut.clientID %>");
        }

        /**************************************
         * 検索結果テーブル 各種レイアウト設定
         **************************************/
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
            var adjustHeight = 40;                                          // 調整高さ(大きい程、検索結果テーブルが低くなる)
            var adjustWidth = 539;                                          // 調整幅(大きい程、検索結果テーブルが狭くなる)

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
            objSaisinDenpyou.click();
            objMaxSearchCount.selectedIndex = 1;
            objShriDateFrom.focus();
        }


        /***********************************
         * 「検索実行」押下時のチェック処理
         ***********************************/
        function checkJikkou(varAction){
            var varErrMsg = '';

            //支払年月日　必須チェック
            if(objShriDateFrom.value == "" && objShriDataTo.value == ""){
                varErrMsg = "<%= Messages.MSG013E %>";
                varErrMsg = varErrMsg.replace("@PARAM1","支払年月日From、支払年月日To");
                alert(varErrMsg);
                objShriDateFrom.focus();
                return false;
                
            }else if(objShriDateFrom.value == "" || objShriDataTo.value == ""){
                if(objShriDateFrom.value == ""){
                    varErrMsg = "<%= Messages.MSG013E %>";
                    varErrMsg = varErrMsg.replace("@PARAM1","支払年月日From");
                    alert(varErrMsg);
                    objShriDateFrom.focus();
                    return false;
                }
                if(objShriDataTo.value == ""){
                    varErrMsg = "<%= Messages.MSG013E %>";
                    varErrMsg = varErrMsg.replace("@PARAM1","支払年月日To");
                    alert(varErrMsg);
                    objShriDataTo.focus();
                    return false;
                }
            }            

            
            //支払年月日 大小チェック
            if(!checkDaiSyou(objShriDateFrom,objShriDataTo,"支払年月日")){return false};
            
            //伝票番号 大小チェック
            if(!checkDaiSyou(objDenNoFrom,objDenNoTo,"伝票番号")){return false};
            
            if(varAction == "0"){
                //表示件数「500件」チェック
                if(objMaxSearchCount.value == "500"){
                    if(!confirm(("<%= Messages.MSG007C %>").replace("無制限","500件"))){return false};
                }
                //検索実行
                objSearch.click();
            
            }else if(varAction == "1"){
                objCsvOutPutFlg.value = "1"
                //検索実行
                objSearch.click();
            }
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
        
        /*********************
         * スクロール同期
         * @return 
         *********************/
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

    </script>
    
    <%--CSV出力判断--%>
    <asp:HiddenField ID="HiddenCsvOutPut" runat="server" />
    <%--CSV出力上限件数フラグ--%>
    <asp:HiddenField id="HiddenCsvMaxCnt" runat="server" />
 
    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tbody>
            <tr>
                <th>
                    支払伝票照会</th>
                <th style="text-align: right;">
                </th>
            </tr>
        </tbody>
    </table>
    <br />
    <table style="text-align: left;" class="mainTable" cellpadding="2">
        <thead>
            <tr>
                <th class="tableTitle" colspan="9" rowspan="1">
                    検索条件
                    <input id="btnClearWin" value="クリア" type="reset" class="button" tabindex="10" /></th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td class="koumokuMei">
                    支払年月日</td>
                <td colspan="2">
                    <input id="TextShriDateFrom" runat="server" maxlength="10" class="date hissu" tabindex="10" />&nbsp;～
                    <input id="TextShriDateTo" runat="server" maxlength="10" class="date hissu"  tabindex="10" /></td>
                <td class="koumokuMei">
                    調査会社</td>
                <td colspan="5" class="codeNumber">
                    <asp:UpdatePanel ID="UpdatePanel_TysKaisya" UpdateMode="Conditional" runat="server" RenderMode="Inline">
                        <ContentTemplate>
                            <input id="TextTysKaisyaCd" runat="server" maxlength="7" style="width: 60px;" class="codeNumber"
                                tabindex="10" />
                            <input id="BtnTysKaisyaSearch" runat="server" type="button" value="検索" class="gyoumuSearchBtn"
                                tabindex="10" />&nbsp;
                            <input id="TextTysKaisyaMei" runat="server" class="readOnlyStyle" style="width: 240px"
                                readonly="readOnly" tabindex="-1" />
                            <input type="hidden" id="HiddenTyskaisyaCd" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    伝票番号</td>
                <td colspan="2">
                    <input id="TextDenNoFrom" runat="server" maxlength="5" style="width: 72px;" class="codeNumber"
                        tabindex="10" />&nbsp;～&nbsp;<input id="TextDenNoTo" runat="server" maxlength="5"
                        style="width: 72px;" class="codeNumber" tabindex="10" />
                </td>
                <td class="koumokuMei">
                    新会計支払先</td>
                <td colspan="5" class="codeNumber">
                    <asp:UpdatePanel ID="UpdatePanel_SkkShriSaki" UpdateMode="Conditional" runat="server" RenderMode="Inline">
                        <ContentTemplate>
                            <input id="TextSkkJigyousyoCd" runat="server" maxlength="10" style="width: 80px;" class="codeNumber"
                                tabindex="10" />&nbsp;-
                            <input id="TextSkkShriSakiCd" runat="server" maxlength="10" style="width: 80px;" class="codeNumber"
                                tabindex="10" />
                            <input type="hidden" id="HiddenSkkShriSakiCd" runat="server" />
                            <input id="BtnSkkShriSakiSearch" runat="server"  type="button" value="検索" class="gyoumuSearchBtn"
                                tabindex="10" />&nbsp;
                            <input id="TextShriSakiMei" runat="server" class="readOnlyStyle" readonly="readonly"
                                style="width: 260px" tabindex="-1" />&nbsp;
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>               
            </tr>
            <tr>
                <td style="text-align: center;" colspan="9" rowspan="1">
                    検索上限件数
                    <select id="maxSearchCount" runat="server" tabindex="10">
                        <option value="10">10件</option>
                        <option value="100" selected="selected">100件</option>
                        <option value="500">500件</option>
                    </select>
                    <input id="BtnSearch" value="検索実行" type="button" runat="server" tabindex="10" />
                    <input type="button" id="search" value="検索実行btn" style="display: none" runat="server" tabindex="-1" />
                    <input id="BtnCsv" value="CSV出力" type="button" runat="server" tabindex="10" />
                    <input type="button" id="BtnHiddenCsv" value="CSV実行btn" style="display: none" runat="server"
                        tabindex="-1" />
                    <input id="CheckSaisinDenpyou" value="0" type="checkbox" runat="server" checked="checked"
                        tabindex="10" />最新伝票のみ表示
                </td>
            </tr>
        </tbody>
    </table>
    <input type="hidden" id="returnTargetIds" runat="server" />
    <input type="hidden" id="afterEventBtnId" runat="server" />
    <table style="height:30px;">
        <tr>
            <td>
                検索結果：</td>
            <td id="resultCount" runat="server">
            </td>
            <td>
                件</td>
            <td style="width:10px">
            </td>
            <td>
                支払合計： \
            </td>
            <td id='TdTotalKingaku' runat='server'>
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0">
        <!-- ヘッダー部 -->   
        <thead>
            <tr>
                <th style="text-align: left;">
                    <div id="DivLeftTitle" runat="server" class="scrollDivLeftTitleStyle2">
                        <table cellpadding="0" cellspacing="0" id="TableTitleTable1" runat="server" class="scrolltablestyle2 sortableTitle"
                            style="border-top: 1px solid #999999; border-left: 1px solid #999999;">
                            <thead>
                                <tr>
                                    <th style="width: 63px;">
                                        SEQ NO</th>
                                    <th style="width: 63px;">
                                        伝票番号</th>
                                    <th style="width: 95px;">
                                        調査会社コード</th>
                                    <th style="width: 85px;">
                                        新会計コード</th>
                                    <th style="width: 200px;">
                                        支払先名</th>
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
                                    <th style="width: 70px;">
                                        振込額</th>
                                    <th style="width: 70px;">
                                        相殺額</th>
                                    <th style="width: 80px;">
                                        支払年月日</th>
                                    <th style="width: 650px;">
                                        摘要</th>                                   
                                </tr>
                            </thead>
                        </table>
                    </div>
                </th>
            </tr>
        </thead>
        <!-- データ部 -->
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
</asp:Content>
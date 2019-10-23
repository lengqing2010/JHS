<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="SearchNyuukinData.aspx.vb" Inherits="Itis.Earth.WebUI.SearchNyuukinData"
    Title="EARTH 入金伝票照会" %>

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
        var objDenBangouFrom = null;
        var objDenBangouTo = null;
        var objAddDateFrom = null;
        var objAddDateTo = null;
        var objNyuukinDateFrom = null;
        var objNyuukinDateTo = null;
        var objSaisinDenpyou = null;
        //検索実行用
        var objMaxSearchCount = null;
        var objSearch = null;
        //CSV出力用
        var objCsv = null;
        var objCsvOutPutFlg = null;

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
            objDenBangouFrom = objEBI("<%= TextDenpyouBangouFrom.clientID %>");
            objDenBangouTo = objEBI("<%= TextDenpyouBangouTo.clientID %>");
            objAddDateFrom = objEBI("<%= TextAddDateFrom.clientID %>");
            objAddDateTo = objEBI("<%= TextAddDateTo.clientID %>");
            objNyuukinDateFrom = objEBI("<%= TextNyuukinDateFrom.clientID %>");
            objNyuukinDateTo = objEBI("<%= TextNyuukinDateTo.clientID %>");
            objSaisinDenpyou = objEBI("<%= CheckSaisinDenpyou.clientID %>");
            //検索実行用
            objMaxSearchCount = objEBI("<%= maxSearchCount.clientID %>");
            objSearch = objEBI("<%= search.clientID %>");
            // CSV出力用
            objCsv = objEBI("<%= ButtonHiddenCsv.clientID %>");
            objCsvOutPutFlg = objEBI("<%= HiddenCsvOutPut.clientID %>");
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
            var minHeight = 90;                                             // ウィンドウリサイズ時の検索結果テーブルに設定する最低高さ
            var adjustHeight = 58;                                          // 調整高さ(大きい程、検索結果テーブルが低くなる)
            var adjustWidth = 545;                                          // 調整幅(大きい程、検索結果テーブルが狭くなる)

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
        }
        
        
        /***********************************
         * 実行ボタン押下時のチェック処理
         ***********************************/
        function checkJikkou(varAction){
            
            //伝票番号 大小チェック
            if(!checkDaiSyou(objDenBangouFrom,objDenBangouTo,"伝票番号"))return false;
            
            //伝票作成日 大小チェック
            if(!checkDaiSyou(objAddDateFrom,objAddDateTo,"伝票作成日"))return false;
            
            //入金年月日 大小チェック
            if(!checkDaiSyou(objNyuukinDateFrom,objNyuukinDateTo,"入金年月日"))return false;

            if(varAction == "0"){
                //表示件数「500件」チェック
                if(objMaxSearchCount.value == "500"){
                    if(!confirm(("<%= Messages.MSG007C %>").replace("無制限","500件"))){return false};
                }
                //検索実行
                objSearch.click();
                
            }else if(varAction == "1"){
                    objCsvOutPutFlg.value = "1";
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
        
        /*********************
         * 請求先情報クリア
         *********************/
        function clrSeikyuuInfo(obj){
            if(obj.value == ""){
                //値のクリア
                objEBI("<%= TextSeikyuuSakiMei.clientID %>").value = "";
                objEBI("<%= TextTorikesiRiyuu.clientID %>").value = "";
                //色をデフォルトへ
                objEBI("<%= TextSeikyuuSakiCd.clientID %>").style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>"
                objEBI("<%= TextSeikyuuSakiBrc.clientID %>").style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>"
                objEBI("<%= SelectSeikyuuSakiKbn.clientID %>").style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>"
            }
        }
        
    </script>
    <%--CSV出力判断--%>
    <asp:HiddenField ID="HiddenCsvOutPut" runat="server" />
    <asp:HiddenField ID="HiddenCsvMaxCnt" runat="server" />
    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tbody>
            <tr>
                <th>
                    入金伝票照会</th>
                <th style="text-align: right;">
                </th>
            </tr>
        </tbody>
    </table>
    <br />
    <table style="text-align: left; width: 950px; table-layout: fixed;" class="mainTable"
        cellpadding="2">
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
                    伝票番号</td>
                <td colspan="2">
                    <input id="TextDenpyouBangouFrom" runat="server" maxlength="5" style="width: 40px;"
                        class="codeNumber" tabindex="20" />&nbsp;～&nbsp;<input id="TextDenpyouBangouTo" runat="server"
                            maxlength="5" style="width: 40px;" class="codeNumber" tabindex="20" /></td>
                <td class="koumokuMei">
                    伝票作成日</td>
                <td colspan="2">
                    <input id="TextAddDateFrom" runat="server" maxlength="10" class="date" onblur="checkDate(this);"
                        tabindex="20" />&nbsp;～&nbsp;<input id="TextAddDateTo" runat="server" maxlength="10"
                            class="date" onblur="checkDate(this);" tabindex="20" /></td>
                <td class="koumokuMei">
                    入金年月日</td>
                <td colspan="2">
                    <input id="TextNyuukinDateFrom" runat="server" maxlength="10" class="date" onblur="checkDate(this);"
                        tabindex="20" />&nbsp;～&nbsp;<input id="TextNyuukinDateTo" runat="server" maxlength="10"
                            class="date" onblur="checkDate(this);" tabindex="20" /></td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    請求先</td>
                <td colspan="5">
                    <asp:UpdatePanel ID="UpdatePanel_seikyuu" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <ContentTemplate>
                            <asp:DropDownList ID="SelectSeikyuuSakiKbn" runat="server" TabIndex="30">
                            </asp:DropDownList>
                            <input id="TextSeikyuuSakiCd" runat="server" maxlength="5" style="width: 40px;" class="codeNumber"
                                tabindex="30" />&nbsp;-
                            <input id="TextSeikyuuSakiBrc" runat="server" maxlength="2" style="width: 20px;"
                                class="codeNumber" tabindex="30" />
                            <input id="btnSeikyuuSakiSearch" runat="server" type="button" value="検索" class="gyoumuSearchBtn"
                                tabindex="30" onserverclick="btnSeikyuuSakiSearch_ServerClick" />&nbsp;
                            <input id="TextSeikyuuSakiMei" runat="server" class="readOnlyStyle" readonly="readonly"
                                style="width: 270px;" tabindex="-1" />&nbsp;
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="koumokuMei">
                    取消</td>
                <td colspan="2">
                    <asp:UpdatePanel ID="UpdatePanel_SeikyuusakiToikesi" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSeikyuuSakiSearch" />
                        </Triggers>
                        <ContentTemplate>
                            <input id="TextTorikesiRiyuu" runat="server" style="width:175px;" tabindex="30" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    請求先名カナ</td>
                <td colspan="5">
                    <input id="TextSeikyuuSakiMeiKana" runat="server" maxlength="30" style="ime-mode: active;
                        width: 490px;" tabindex="35" />
                </td>
            </tr>
            <tr>
                <td style="text-align: center;" colspan="9" rowspan="1">
                    検索上限件数<select id="maxSearchCount" runat="server" tabindex="40">
                        <option value="10">10件</option>
                        <option value="100" selected="selected">100件</option>
                        <option value="500">500件</option>
                    </select>
                    <input id="btnSearch" value="検索実行" type="button" runat="server" tabindex="40" />
                    <input type="button" id="ButtonCsv" value="CSV出力" runat="server" tabindex="40" />
                    <input type="button" id="ButtonHiddenCsv" value="CSV実行btn" style="display: none"
                        runat="server" />
                    <input type="button" id="search" value="検索実行btn" style="display: none" runat="server" />
                    <!-- 最新伝票のみ表示 (非表示、未チェック設定)-->
                    <input id="CheckSaisinDenpyou" value="0" type="checkbox" runat="server" style="display: none"
                        tabindex="40" />
                </td>
            </tr>
        </tbody>
    </table>
    <input type="hidden" id="returnTargetIds" runat="server" />
    <input type="hidden" id="afterEventBtnId" runat="server" />
    <table style="height: 30px;">
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
                                    <th style="width: 63px;">
                                        SEQ NO</th>
                                    <th style="width: 40px;">
                                        伝票<br />
                                        種別</th>
                                    <th style="width: 80px;">
                                        請求先コード</th>
                                    <th style="width: 254px;">
                                        請求先名</th>
                                    <th style="width: 75px;">
                                        入金年月日</th>
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
                                    <th style="width: 80px;">
                                        伝票<br />
                                        合計金額</th>
                                    <th style="width: 60px;">
                                        現金</th>
                                    <th style="width: 60px;">
                                        小切手</th>
                                    <th style="width: 60px;">
                                        口座振替</th>
                                    <th style="width: 60px;">
                                        振込</th>
                                    <th style="width: 60px;">
                                        手形</th>
                                    <th style="width: 60px;">
                                        協力会費</th>
                                    <th style="width: 60px;">
                                        振込<br />
                                        手数料</th>
                                    <th style="width: 60px;">
                                        相殺</th>
                                    <th style="width: 60px;">
                                        値引</th>
                                    <th style="width: 60px;">
                                        その他</th>
                                    <th style="width: 75px;">
                                        手形期日</th>
                                    <th style="width: 160px;">
                                        摘要名</th>
                                    <th style="width: 60px;">
                                        入金取込<br />
                                        NO</th>
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
</asp:Content>

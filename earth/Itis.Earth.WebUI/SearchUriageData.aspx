<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="SearchUriageData.aspx.vb" Inherits="Itis.Earth.WebUI.SearchUriageData"
    Title="EARTH 売上伝票照会" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script src="js/sortable_sync_ja.js" type="text/javascript">
    </script>

    <script type="text/javascript">

        //ウィンドウサイズ変更
        try{
            if(window.name != "<%=EarthConst.MAIN_WINDOW_NAME %>") window.resizeTo(1010,800);
        }catch(e){
            //アクセスが拒否されましたのエラーが出たら何もしない。
            if(e.number == 2147024891) throw e;
        }
        
        /*====================================
         *グローバル変数宣言（画面部品）
         ====================================*/
        //画面表示部品
        var objKubun = null;
        var objKubunAll = null;
        var objBangouFrom = null;
        var objBangouTo = null;
        var objDenBangouFrom = null;
        var objDenBangouTo = null;
        var objAddDateFrom = null;
        var objAddDateTo = null;
        var objSeikyuuDateFrom = null;
        var objSeikyuuDateTo = null;
        var objUriDateFrom = null;
        var objUriDateTo = null;
        var objDenUriDateFrom = null;
        var objDenUriDateTo = null;
        var objSaisinDenpyou = null;
        //検索実行用
        var objMaxSearchCount = null;
        var objSearch = null;
        var objCsv = null;
        //hidden
        var objKubunVal = null;
        //変更ポップアップ用
        var objSelectedTr = null;


        /*************************************
         * onload時の追加処理
         *************************************/
        function funcAfterOnload() {
            //画面表示部品セット
            setGlobalObj();
            
            //区分の状態をセッティング
            setKubunVal()
            
            /*検索結果テーブル ソート設定*/
            sortables_init();
            
            /*検索結果テーブル 各種レイアウト設定*/
            settingResultTable();
            
            //検索結果が1件のみの場合、値を戻す処理を実行
            if (objEBI("<%= firstSend.clientID %>").value != ""){
                returnSelectValue(objEBI(objEBI("<%= firstSend.clientID %>").value));
            }
            
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
            objCsvOutPutFlg.value = ""
        }
        
       /*********************************************
        * 戻り値がない為、同メソッドをオーバーライド
        *********************************************/
        function returnSelectValue(objSelectedTr){
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
            objKubun = objEBI("<%= selectKbn.clientID %>");
            objKubunAll = objEBI("<%= kubun_all.clientID %>");
            objKubunVal = objEBI("<%= kubunVal.clientID %>");       //hidden
            objBangouFrom = objEBI("<%= TextBangouFrom.clientID %>");
            objBangouTo = objEBI("<%= TextBangouTo.clientID %>");
            objDenBangouFrom = objEBI("<%= TextDenpyouBangouFrom.clientID %>");
            objDenBangouTo = objEBI("<%= TextDenpyouBangouTo.clientID %>");
            objAddDateFrom = objEBI("<%= TextAddDateFrom.clientID %>");
            objAddDateTo = objEBI("<%= TextAddDateTo.clientID %>");
            objSeikyuuDateFrom = objEBI("<%= TextSeikyuuDateFrom.clientID %>");
            objSeikyuuDateTo = objEBI("<%= TextSeikyuuDateTo.clientID %>");
            objUriDateFrom = objEBI("<%= TextUriageDateFrom.clientID %>");
            objUriDateTo = objEBI("<%= TextUriageDateTo.clientID %>");
            objDenUriDateFrom = objEBI("<%= TextDenpyouUriageDateFrom.clientID %>");
            objDenUriDateTo = objEBI("<%= TextDenpyouUriageDateTo.clientID %>");
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
            var minHeight = 120;                                            // ウィンドウリサイズ時の検索結果テーブルに設定する最低高さ
            var adjustHeight = 39;                                          // 調整高さ(大きい程、検索結果テーブルが低くなる)
            var adjustWidth = 698;                                          // 調整幅(大きい程、検索結果テーブルが狭くなる)

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
            objKubunAll.click();
            objSaisinDenpyou.click();
            objMaxSearchCount.selectedIndex = 1;
            objKubunAll.focus();
        }


        /****************************************************************************************
         * 区分セレクトボックス＆チェックボックスの状態をチェック
         ****************************************************************************************/
        function setKubunVal(){

            objKubunVal.value = ""; //初期化
            
            if(objKubunAll.checked == true){
                objKubun.selectedIndex = 0;
                objKubun.disabled = true;
                return;
            }else{
                objKubun.disabled = false;
                if(objKubun.value != ""){
                    objKubunVal.value = objKubun.value;
                }
            }
        }
        
        
        /***********************************
         * 「検索実行」押下時のチェック処理
         ***********************************/
        function checkJikkou(varAction){

            if(!objKubunAll.checked && objKubunVal.value.Trim() == ""){
                alert("<%= Messages.MSG006E %>");
                objKubun.focus();
                return false;
            }

            //番号 大小チェック
            if(!checkDaiSyou(objBangouFrom,objBangouTo,"番号")){return false};
            
            //伝票番号 大小チェック
            if(!checkDaiSyou(objDenBangouFrom,objDenBangouTo,"伝票番号")){return false};
            
            //登録年月日 大小チェック
            if(!checkDaiSyou(objAddDateFrom,objAddDateTo,"伝票作成日")){return false};
            
            //請求年月日 大小チェック
            if(!checkDaiSyou(objSeikyuuDateFrom,objSeikyuuDateTo,"請求年月日")){return false};
            
            //売上年月日 大小チェック
            if(!checkDaiSyou(objUriDateFrom,objUriDateTo,"売上年月日")){return false};

            //売上年月日 大小チェック
            if(!checkDaiSyou(objDenUriDateFrom,objDenUriDateTo,"伝票売上年月日")){return false};

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
        
        /***********************************
         * 請求年月日変更モーダル画面呼出 ＆ 呼出後処理
         ***********************************/
        function openModalSeikyuuDate(strUrl, strUniqueNo, strSeikyuuDate){
            var wx = 260;
            var wy = 160;
            var x = (screen.width  - wx) / 2;
            var y = (screen.height - wy) / 2;
            var retVal;
            retVal =window.showModalDialog(strUrl + "?DenUnqNo=" + strUniqueNo
                                                  + "&SeikyuuDate=" + strSeikyuuDate
                                               ,window 
                                               ,'dialogLeft:' + x + ';dialogTop:' + y + ';dialogWidth:260px;dialogHeight:160px;menubar:no;toolbar:no;location:no;status:no;resizable:yes;scrollbars:yes;');
            if (retVal != undefined){
                objEBI("<%= ButtonModalRefresh.clientID %>").click();
            }
        }
        
        /***********************************
         * 伝票売上年月日変更モーダル画面呼出 ＆ 呼出後処理
         ***********************************/
         function openModalDenUriDate(strUrl, strUniqueNo, strDenUriDate){
            var wx = 300;
            var wy = 160;
            var x = (screen.width  - wx) / 2;
            var y = (screen.height - wy) / 2;
            var retVal;
            retVal =window.showModalDialog(strUrl + "?DenUnqNo=" + strUniqueNo
                                                  + "&DenUriDate=" + strDenUriDate
                                               ,window 
                                               ,'dialogLeft:' + x + ';dialogTop:' + y + ';dialogWidth:300px;dialogHeight:160px;menubar:no;toolbar:no;location:no;status:no;resizable:yes;scrollbars:yes;');
            if (retVal != undefined){
                objEBI("<%= ButtonModalRefresh.clientID %>").click();
            }
         }
        
        /***********************************
         * 検索実行処理
         ***********************************/
        function exeSearch(){
            objSearch = objEBI("<%= search.clientID %>");
            //画面グレイアウト
            setWindowOverlay(objSearch);
            objSearch.click();
        }
        
        /**
        * 保証書NO To自動セット
        * @return true/false
        */
        function setHosyouNoTo(obj){
            if(obj.id == objBangouFrom.id && objBangouTo.value == ""){
                objBangouTo.value = obj.value;
                objBangouTo.select();
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
                objEBI("<%= TextKameiTorikesiRiyuu.clientID %>").value = "";
            }
        }
        
        /*********************
         * 請求先情報クリア
         *********************/
        function clrSeikyuuInfo(obj){
            if(obj.value == ""){
                //値のクリア
                objEBI("<%= TextSeikyuuSakiMei.clientID %>").value = "";
                objEBI("<%= TextSeikyuuKameiTorikesiRiyuu.clientID %>").value = "";
                //色をデフォルトへ
                objEBI("<%= TextSeikyuuSakiCd.clientID %>").style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>"
                objEBI("<%= TextSeikyuuSakiBrc.clientID %>").style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>"
                objEBI("<%= SelectSeikyuuKbn.clientID %>").style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>"
            }
        }


    </script>

    <asp:UpdatePanel ID="UpdPnlModalRefresh" UpdateMode="Conditional" runat="server"
        RenderMode="Inline">
        <ContentTemplate>
            <asp:Button ID="ButtonModalRefresh" runat="server" Text="モーダル処理後リフレッシュ" OnClick="ButtonModalRefresh_Click"
                Style="display: none;" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--CSV出力判断--%>
    <asp:HiddenField ID="HiddenCsvOutPut" runat="server" />
    <asp:HiddenField ID="HiddenCsvMaxCnt" runat="server" />
    <%--CSV出力上限件数フラグ--%>
    <%-- どの商品を表すか（非表示） --%>
    <asp:HiddenField ID="HiddenTargetId" runat="server" />
    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tbody>
            <tr>
                <th>
                    売上伝票照会</th>
                <th style="text-align: right;">
                </th>
            </tr>
        </tbody>
    </table>
    <br />
    <table style="text-align: left;" class="mainTable" cellpadding="2">
        <thead>
            <tr>
                <th class="tableTitle" colspan="7" rowspan="1">
                    検索条件
                    <input id="btnClearWin" value="クリア" type="reset" class="button" tabindex="10" /></th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td class="hissu" style="font-weight: bold">
                    区分</td>
                <td colspan="2" class="hissu">
                    <asp:DropDownList ID="selectKbn" runat="server" TabIndex="10">
                    </asp:DropDownList>
                    &nbsp;全区分<input id="kubun_all" type="checkbox" runat="server" tabindex="10" />
                    <input type="hidden" id="kubunVal" runat="server" /></td>
                <td class="koumokuMei">
                    番号</td>
                <td class="codeNumber" colspan="3">
                    <input id="TextBangouFrom" runat="server" maxlength="10" style="width: 72px;" class="codeNumber"
                        onblur="checkNumber(this);" onchange="if(checkNumber(this))setHosyouNoTo(this);"
                        tabindex="10" />&nbsp;～&nbsp;<input id="TextBangouTo" runat="server" maxlength="10"
                        style="width: 72px;" class="codeNumber" onblur="checkNumber(this);" tabindex="10" /></td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    伝票番号</td>
                <td colspan="2" class="codeNumber">
                    <input id="TextDenpyouBangouFrom" runat="server" maxlength="5" style="width: 40px;"
                        class="codeNumber" tabindex="10" />&nbsp;～&nbsp;<input id="TextDenpyouBangouTo" runat="server"
                            maxlength="5" style="width: 40px;" class="codeNumber" tabindex="10" /></td>
                <td class="koumokuMei">
                    伝票作成日</td>
                <td colspan="3" class="date">
                    <input id="TextAddDateFrom" runat="server" maxlength="10" class="date" onblur="checkDate(this);"
                        tabindex="10" />&nbsp;～&nbsp;<input id="TextAddDateTo" runat="server" maxlength="10"
                            class="date" onblur="checkDate(this);" tabindex="10" /></td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    商品</td>
                <td colspan="2">
                    <asp:UpdatePanel ID="UpdatePanel_syouhin" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <ContentTemplate>
                            <input id="TextSyouhinCd" runat="server" maxlength="8" style="width: 60px;" class="codeNumber"
                                tabindex="10" />
                            <input id="btnSyouhinSearch" runat="server" type="button" value="検索" class="gyoumuSearchBtn"
                                tabindex="10" />&nbsp;
                            <input id="TextHinmei" runat="server" class="readOnlyStyle" readonly="readonly" style="width: 155px"
                                tabindex="-1" />&nbsp;
                            <input type="hidden" id="hdnSyouhinType" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="koumokuMei">
                    請求先</td>
                <td colspan="3" class="codeNumber">
                    <asp:UpdatePanel ID="UpdatePanel_seikyuu" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <ContentTemplate>
                            <asp:DropDownList ID="SelectSeikyuuKbn" runat="server" TabIndex="10">
                            </asp:DropDownList>
                            <input id="TextSeikyuuSakiCd" runat="server" maxlength="5" style="width: 35px;" class="codeNumber"
                                tabindex="10" />&nbsp;-
                            <input id="TextSeikyuuSakiBrc" runat="server" maxlength="2" style="width: 15px;"
                                class="codeNumber" tabindex="10" />
                            <input id="btnSeikyuuSakiSearch" runat="server" type="button" value="検索" class="gyoumuSearchBtn"
                                tabindex="10" />&nbsp;
                            <input id="TextSeikyuuSakiMei" runat="server" class="readOnlyStyle" readonly="readonly"
                                style="width: 155px" tabindex="-1" />&nbsp;
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    加盟店</td>
                <td colspan="2">
                    <asp:UpdatePanel ID="UpdatePanel_kameiten" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <ContentTemplate>
                            <input id="TextKameitenCd" runat="server" maxlength="8" style="width: 60px;" class="codeNumber"
                                tabindex="10" />
                            <input id="btnKameitenSearch" runat="server" type="button" value="検索" class="gyoumuSearchBtn"
                                onserverclick="btnKameitenSearch_ServerClick" tabindex="10" />&nbsp;
                            <input id="TextKameitenMei" runat="server" class="readOnlyStyle" readonly="readonly"
                                style="width: 155px" tabindex="-1" />&nbsp;
                            <input type="hidden" id="Hidden1" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="koumokuMei">
                    請求先名カナ</td>
                <td colspan="3" class="codeNumber">
                    <input id="TextSeikyuuSakiMeiKana" runat="server" maxlength="40" style="ime-mode: active;
                        width: 340px;" tabindex="10" />
                </td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    加盟店取消</td>
                <td colspan="2">
                    <asp:UpdatePanel ID="UpdatePanel_KameiToikesi" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnKameitenSearch" />
                        </Triggers>
                        <ContentTemplate>
                            <input id="TextKameiTorikesiRiyuu" runat="server" style="width: 100px;" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td style="font-size: 14px;" class="koumokuMei">
                    請求加盟店取消</td>
                <td colspan="3">
                    <asp:UpdatePanel ID="UpdatePanel_SeikyuusakiToikesi" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSeikyuuSakiSearch" />
                        </Triggers>
                        <ContentTemplate>
                            <input id="TextSeikyuuKameiTorikesiRiyuu" runat="server" style="width: 100px;" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    伝票売上年月日</td>
                <td class="date">
                    <input id="TextDenpyouUriageDateFrom" runat="server" maxlength="10" class="date"
                        onblur="checkDate(this);" tabindex="10" />&nbsp;～&nbsp;<input id="TextDenpyouUriageDateTo"
                            runat="server" maxlength="10" class="date" onblur="checkDate(this);" tabindex="10" /></td>
                <td class="koumokuMei">
                    請求年月日</td>
                <td class="date" style="border-right-style: none;">
                    <input id="TextSeikyuuDateFrom" runat="server" maxlength="10" class="date" onblur="checkDate(this);"
                        tabindex="10" />&nbsp;&nbsp;～&nbsp;</td>
                <td class="date" style="border-left-style: none;">
                    <input id="TextSeikyuuDateTo" runat="server" maxlength="10"
                            class="date" onblur="checkDate(this);" tabindex="10" /></td>
                <td class="koumokuMei">
                    売上年月日</td>
                <td class="date">
                    <input id="TextUriageDateFrom" runat="server" maxlength="10" class="date" onblur="checkDate(this);"
                        tabindex="10" />&nbsp;～&nbsp;<input id="TextUriageDateTo" runat="server" maxlength="10"
                            class="date" onblur="checkDate(this);" tabindex="10" /></td>
            </tr>
            <tr>
                <td style="text-align: center;" colspan="7" rowspan="1">
                    検索上限件数<select id="maxSearchCount" runat="server" tabindex="10">
                        <option value="10">10件</option>
                        <option value="100" selected="selected">100件</option>
                        <option value="500">500件</option>
                    </select>
                    <input type="button" id="btnSearch" value="検索実行" runat="server" style="padding-top: 2px;"
                        tabindex="10" />
                    <input type="button" id="search" value="検索実行btn" style="display: none" runat="server"
                        tabindex="-1" />
                    <input type="button" id="ButtonCsv" value="CSV出力" runat="server" tabindex="10" />
                    <input type="button" id="ButtonHiddenCsv" value="CSV実行btn" style="display: none"
                        runat="server" tabindex="-1" />
                    <input id="CheckSaisinDenpyou" value="0" type="checkbox" runat="server" checked="checked"
                        tabindex="10" />最新伝票のみ表示&nbsp;
                    <input id="CheckKeijyouFlg" value="0" type="checkbox" runat="server" checked="checked"
                        tabindex="10" />計上済みのみ対象&nbsp;
                    <input id="CheckMinusDenpyou" value="0" type="checkbox" runat="server" tabindex="10" />マイナス伝票のみ表示&nbsp;
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
                                    <th style="width: 63px;">
                                        SEQ NO</th>
                                    <th style="width: 62px;">
                                        伝票種別</th>
                                    <th style="width: 62px;">
                                        伝票番号</th>
                                    <th style="width: 85px;">
                                        請求先コード</th>
                                    <th style="width: 140px;">
                                        請求先名</th>
                                    <th style="width: 36px;">
                                        区分</th>
                                    <th style="width: 70px;">
                                        番号</th>
                                    <th style="width: 140px;">
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
                                    <th style="width: 66px;">
                                        商品コード</th>
                                    <th style="width: 140px;">
                                        品名</th>
                                    <th style="width: 62px;">
                                        売上金額</th>
                                    <th style="width: 36px;">
                                        数量</th>
                                    <th style="width: 75px;">
                                        売上年月日</th>
                                    <th style="width: 101px;">
                                        伝票売上年月日</th>
                                    <th style="width: 67px;">
                                        売上処理</th>
                                    <th style="width: 75px;">
                                        請求年月日</th>
                                    <th style="width: 80px;">
                                        加盟店コード</th>
                                    <th style="width: 140px;">
                                        加盟店名</th>
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

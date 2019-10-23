<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="TenbetuSyuusei.aspx.vb" Inherits="Itis.Earth.WebUI.tenbetu_syuusei"
    Title="EARTH 店別データ修正" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script type="text/javascript">
    
    
        //ウィンドウリサイズ時のテーブルサイズ調整
        window.onresize = function(){
            changeTableSize("dataGridContent",200,100); 
        }
        
        //onloadイベント追加ファンクション
        function funcAfterOnload(){
        
            changeTableSize("dataGridContent",200,100);
        
            //登録完了時に次の物件へ遷移するポップアップを表示する
            if(objEBI("<%=callModalFlg.clientID %>").value == "<%=Boolean.TrueString %>"){
                objEBI("<%=callModalFlg.clientID %>").value = "";
                var kbn = objEBI("<%=hiddenKubun.clientID %>").value;
                var tenmd = objEBI("<%=hiddenDispMode.clientID %>").value;
                var isfc = objEBI("<%=hiddenIsFc.clientID %>").value;
                var callType = "";
                if(tenmd == 1){
                    callType = "tenbetuA";
                }else{
                    callType = "hansokuA";
                }
                var rtnArg = callModalMise("<%=UrlConst.POPUP_MISE_SITEI %>","<%=UrlConst.TENBETU_SYUUSEI %>",callType,true,kbn,tenmd,isfc);

                //ポップアップで閉じるボタン押下時
                if(rtnArg == "null" && window.name != "<%=EarthConst.MAIN_WINDOW_NAME %>"){
                    window.close();
                }
            }
            
        }
        
        //行ダブルクリック時のダミーファンクション
        function returnSelectValue(objSelectedTr,intGamen) {
            return;
        }
        
        function funcOnChgChk(objCtrl,objClickCtrl){
            changeTableSize("dataGridContent",200,100);
            if (objEBI('<%= hiddenOnBlurValChk.clientID %>').value != objCtrl.value){
                objClickCtrl.click();
            }
        }
        //各種ボタン押下時の処理
        function deleteConfirm(objCtrl,objSyouhinId){
            changeTableSize("dataGridContent",200,100);
            if(objSyouhinId.value != ""){
                if(!confirm('行を削除します。\r\n宜しいですか？')){
                    return false;
                }
            }
            objCtrl.click();
        }
        
        //各種ボタン押下時の処理
        function executeConfirm(objCtrl){
            if(flgAjaxRunning){
                //Ajax処理中は、待つ
                setTimeout(function(){executeConfirm(objCtrl)},100);
            }else{
                if(objCtrl == objEBI("<%= buttonTourokuCall.clientID %>")){
                    if(!confirm('<%= Messages.MSG017C %>')){
                        return false;
                    }
                    setWindowOverlay(objCtrl);
                    objEBI("<%= buttonTouroku.clientID %>").click();
                }
                if(objCtrl == objEBI("<%= buttonTourokuHansokuCall.clientID %>")){
                    if(!confirm('<%= Messages.MSG017C %>')){
                        return false;
                    }
                    setWindowOverlay(objCtrl);
                    objEBI("<%= buttonTourokuHansoku.clientID %>").click();
                }
            }
        }

        function openViewPage(){
            var objViewWindow;
            var viewForm;
            var strKameitenCd;
            var strIsFc;
            
            objViewWindow = window.open("about:Blank", "viewWindow", "menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes");
            strKameitenCd = objEBI('<%= hiddenMiseCd.clientID %>').value;
            strIsFc = objEBI('<%= hiddenIsFc.clientID %>').value;
            
            viewForm = objEBI("openPageForm");
            viewForm.reset();
            objEBI("no").value = strKameitenCd;
            objEBI("sendPageHidden1").value = strIsFc;
            viewForm.method = "post";
            viewForm.action = "TenbetuSyuusei.aspx";
            viewForm.target = "viewWindow";
            viewForm.submit();
            objViewWindow.focus();            
        }

        var flgAjaxRunning = false;

        //Ajax処理開始時のfunction
        Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(InitializeRequestHandler);
            function InitializeRequestHandler(sender, args){
                var mng = Sys.WebForms.PageRequestManager.getInstance();

                flgAjaxRunning = true;

                if (mng.get_isInAsyncPostBack()){
                    alert("<%= Messages.MSG104E %>");
                    args.set_cancel(true);
                    return false;
                }
                if (objEBI("<%= buttonAddRow.clientID %>") != null && objEBI("<%= buttonAddRow.clientID %>") != undefined){
                    objEBI("<%= buttonAddRow.clientID %>").disabled = true;
                }

                var objGrid = objEBI("<%= searchGrid.clientID %>");
                var inputItems = objGrid.getElementsByTagName("input");
                for (i = 0; i < inputItems.length; i++){
                    if (inputItems[i].type == "button"){
                        inputItems[i].disabled = true;
                    }
                } 
            }
            
        //Ajax処理完了後のfunction
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args){
                initPage(); //画面初期化
                if (objEBI("<%= buttonAddRow.clientID %>") != null && objEBI("<%= buttonAddRow.clientID %>") != undefined){
                    objEBI("<%= buttonAddRow.clientID %>").disabled = false;
                }
                flgAjaxRunning = false;
            }
            
    </script>

    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tbody>
            <tr>
                <th id="PageTitleTh" runat="server">
                    店別データ修正</th>
                <th style="text-align: right;">
                </th>
            </tr>
            <tr>
                <td colspan="2" rowspan="1">
                </td>
            </tr>
        </tbody>
    </table>
    <div id="divDisplayReadOnly" runat="Server">
        <input type="hidden" id="gamenId" value="syuusei_keiri" runat="server" />
        <asp:HiddenField ID="hiddenOnBlurValChk" runat="server" />
        <asp:HiddenField ID="hiddenKubun" runat="server" />
        <asp:HiddenField ID="hiddenMiseCd" runat="server" />
        <table style="text-align: left; width: 100%;" id="mainTableIrai1" class="mainTable"
            cellpadding="1">
            <thead>
                <tr>
                    <th class="tableTitle" colspan="4">
                        <a id="seikyuuSakiDispLink" href="javascript:void(0);" runat="server">請求先情報</a>
                        <span style="display: none;">【】 【】 【】 【】</span>
                    </th>
                </tr>
            </thead>
            <tbody id="seikyuuSakiTbody" runat="server">
                <tr>
                    <td colspan="4" class="tableSpacer">
                    </td>
                </tr>
                <tr id="KubunRow" runat="server">
                    <td class="koumokuMei">
                        区分</td>
                    <td colspan="3">
                        <asp:TextBox ID="TextKubun" Style="width: 15em;" CssClass="readOnlyStyle2" ReadOnly="true"
                            Text="" TabIndex="-1" runat="server" />
                    </td>
                </tr>
                <tr id="KameitenRow" runat="server">
                    <td class="koumokuMei">
                        加盟店</td>
                    <td id="TdKameiten" runat="server">
                        <asp:TextBox ID="TextKameitenCd" Style="width: 3em" CssClass="readOnlyStyle2" ReadOnly="true"
                            Text="" TabIndex="-1" runat="server" />
                        <asp:TextBox ID="TextKameitenMei" Style="width: 18em" CssClass="readOnlyStyle2" ReadOnly="true"
                            TabIndex="-1" Text="" runat="server" />
                    </td>
                    <td class="koumokuMei">
                        取消</td>
                    <td id="TdTorikesiRiyuu">
                        <asp:TextBox ID="TextTorikesiRiyuu" runat="server" style="width: 10em;"/> 
                    </td>
                </tr>
                <tr>
                    <td class="koumokuMei">
                        営業所</td>
                    <td id="TdEigyousyo" runat="server">
                        <asp:TextBox ID="TextEigyousyoCd" Style="width: 3em;" CssClass="readOnlyStyle2" ReadOnly="true"
                            Text="" TabIndex="-1" runat="server" />
                        <asp:TextBox ID="TextEigyousyoMei" Style="width: 18em;" CssClass="readOnlyStyle2"
                            ReadOnly="true" Text="" TabIndex="-1" runat="server" />
                    </td>
                    <td style="width: 100px;" class="koumokuMei" id="TdKeiretuHead" runat="server">
                        <span id="keiretuTitle" runat="server">系列</span></td>
                    <td id="TdKeiretu" runat="server">
                        <asp:TextBox ID="TextKeiretuCd" Style="width: 3em" CssClass="readOnlyStyle2" ReadOnly="true"
                            Text="" TabIndex="-1" runat="server" />
                        <asp:TextBox ID="TextKeiretuMei" Style="width: 18em" CssClass="readOnlyStyle2" ReadOnly="true"
                            Text="" TabIndex="-1" runat="server" />
                    </td>
                </tr>
                <tr style="height: 22px;" runat="server" id="SeikyuusakiRow">
                    <td valign="middle" class="koumokuMei">
                        請求先</td>
                    <td valign="middle" colspan="3">
                        <asp:TextBox ID="TextSeikyuusaki" Style="width: 25em;" CssClass="readOnlyStyle2"
                            ReadOnly="true" Text="" TabIndex="-1" runat="server" />
                    </td>
                </tr>
                <tr style="display: none;">
                    <td colspan="4" class="tableSpacer">
                    </td>
                </tr>
                <tr id="UriageZumiRow" runat="server">
                    <td align="center" colspan="4">
                        <input type="button" id="buttonUriageSyorizumi" value="売上処理済の参照" class="button" runat="server"
                            onclick="openViewPage();" />
                    </td>
                </tr>
            </tbody>
        </table>
        <br />
        <br />
        <div id="divTourokuryou" runat="server">
            <asp:HiddenField ID="HiddenOpenTourokuRyouValues" runat="server" />
            <asp:UpdatePanel ID="UpdatePanelTouroku" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                <ContentTemplate>
                    <table style="width: 100%;" id="Table1" class="mainTable" cellpadding="1">
                        <thead>
                            <tr>
                                <th class="tableTitle" colspan="10" id="tourokuTitle" style="text-align: left;">
                                    <a id="tourokuRyouDispLink" href="javascript:void(0);" runat="server">登録料</a><img
                                        src="images/spacer.gif" alt="" style="width: 129px; height: 1px;" /><b> <span id="SpanUriageSyorizumi"
                                            style="display: inline; color: red;" runat="server">売上処理済</span></b>
                                </th>
                            </tr>
                        </thead>
                        <tbody id="tourokuRyouTbody" runat="server">
                            <tr>
                                <td colspan="10" class="tableSpacer">
                                </td>
                            </tr>
                            <!-- 1行目 -->
                            <tr class="koumokuMei">
                                <td id="TourokubiHead" runat="server" style="text-align: center">
                                    登録日
                                </td>
                                <td style="text-align: center">
                                    請求
                                </td>
                                <td style="text-align: center">
                                    商品名
                                </td>
                                <td style="text-align: center">
                                    実請求<br />
                                    税抜金額
                                </td>
                                <td style="text-align: center">
                                    消費税
                                </td>
                                <td style="text-align: center">
                                    税込金額
                                </td>
                                <td style="text-align: center">
                                    工務店<br />
                                    税抜金額
                                </td>
                                <td style="text-align: center">
                                    請求書<br />
                                    発行日
                                </td>
                                <td style="text-align: center">
                                    売上<br />
                                    年月日
                                </td>
                                <td style="text-align: center">
                                    伝票売上<br />
                                    年月日
                                </td>
                                <td style="text-align: center">
                                    伝票売上<br />
                                    年月日修正
                                </td>
                            </tr>
                            <!-- 2行目 -->
                            <tr>
                                <td id="tourokubiData" runat="server">
                                    <asp:TextBox ID="TextTourokuDate" CssClass="date" Text="" runat="server" />
                                </td>
                                <td>
                                    <select id="SelectSeikyuu" runat="server">
                                        <option value="0">無</option>
                                        <option value="1" selected="selected">有</option>
                                    </select>
                                    <span id="Span1" runat="server"></span>
                                    <input id="buttonChgTourokuSeikyuu" type="button" value="請求有無変更時アクション" runat="server"
                                        style="display: none" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="SelectSyouhinTourokuRyou" runat="server">
                                    </asp:DropDownList><span id="Span2" runat="server"></span><input id="buttonChgTourokuSyouhin"
                                        type="button" value="商品登録料変更時アクション" runat="server" style="display: none" />
                                </td>
                                <td>
                                    <asp:TextBox ID="TextJituseikyuuZeinukikingaku" CssClass="kingaku" Text="" runat="server" /><input
                                        id="buttonChgTourokuJituGaku" type="button" value="実請求税抜き額変更時アクション" runat="server"
                                        style="display: none" />
                                </td>
                                <td>
                                    <asp:TextBox ID="TextSyouhizei" Text="" CssClass="kingaku" runat="server" />
                                    <asp:HiddenField ID="hiddenZeiKbn" runat="server" />
                                    <input id="buttonChgSyouhiZei" type="button" value="消費税額変更時アクション" runat="server"
                                        style="display: none;" onserverclick="buttonChgSyouhiZei_ServerClick" />
                                </td>
                                <td>
                                    <asp:TextBox ID="TextZeikomiKingaku" ReadOnly="true" Text="" CssClass="kingaku readOnlyStyle2"
                                        TabIndex="-1" runat="server" />
                                </td>
                                <td id="TdKoumutenZeinukiKingaku" runat="server">
                                    <asp:TextBox ID="TextKoumutenZeinukiKingaku" CssClass="kingaku" Text="" runat="server" /><input
                                        id="buttonChgTourokuKoumu" type="button" value="工務店税抜き金額変更時アクション" runat="server"
                                        style="display: none" />
                                </td>
                                <td>
                                    <asp:TextBox ID="TextSeikyuusyoHakkouDate" CssClass="date" Text="" runat="server"
                                        onblur="checkDate(this);" />
                                </td>
                                <td>
                                    <asp:TextBox ID="TextUriageNengappi" CssClass="date" Text="" runat="server" /><input
                                        id="buttonChgUriDate" type="button" value="売上年月日変更時アクション" runat="server" style="display: none"
                                        onserverclick="buttonChgUriDate_ServerClick" />
                                </td>
                                <td>
                                    <asp:TextBox ID="TextDenpyouUriDateDisplay" ReadOnly="true" Text="" CssClass="date readOnlyStyle2"
                                        TabIndex="-1" runat="server" />
                                </td>
                                <td>
                                    <asp:TextBox ID="TextDenpyouUriDate" CssClass="date" Text="" runat="server" /><input
                                        id="buttonChgDenUriDate" type="button" value="伝票売上年月日変更時アクション" runat="server"
                                        style="display: none" onserverclick="buttonChgDenUriDate_ServerClick" />
                                </td>
                            </tr>
                            <!-- 3行目 -->
                            <tr id="tourokuBikou" runat="server">
                                <td colspan="1" class="koumokuMei">
                                    備考
                                </td>
                                <td colspan="8">
                                    <asp:TextBox ID="TextTourokuryouBikou" MaxLength="40" Style="width: 23em;" Text=""
                                        runat="server" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:HiddenField ID="hiddenTxtChgCtrlTouroku" runat="server" />
                    <asp:HiddenField ID="hiddenTourokuUpdateTime" runat="server" />
                    <asp:HiddenField ID="hiddenTourokuUriGaku" runat="server" />
                    <br />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="divHansokuSyoki" runat="server">
            <asp:HiddenField ID="HiddenOpenToolRyouValues" runat="server" />
            <asp:UpdatePanel ID="UpdatePanelTool" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                <ContentTemplate>
                    <table style="width: 100%;" class="mainTable" cellpadding="1">
                        <thead>
                            <tr>
                                <th class="tableTitle" colspan="10" style="text-align: left;">
                                    <a id="hansokuhinSyokiDispLink" href="javascript:void(0);" runat="server">販促品初期ツール料</a><img
                                        src="images/spacer.gif" alt="" style="width: 50px; height: 1px;" /><b> <span id="SpanUriagesyorizumiTool"
                                            style="display: inline; color: red;" runat="server">売上処理済</span></b>
                                </th>
                            </tr>
                        </thead>
                        <tbody id="hansokuhinSyokiTbody" runat="server">
                            <tr>
                                <td colspan="10" class="tableSpacer">
                                </td>
                            </tr>
                            <!-- 1行目 -->
                            <tr class="koumokuMei">
                                <td id="HaisoubiHead" runat="server" style="text-align: center">
                                    配送日
                                </td>
                                <td style="text-align: center">
                                    請求
                                </td>
                                <td style="text-align: center">
                                    商品名
                                </td>
                                <td style="text-align: center">
                                    実請求<br />
                                    税抜金額
                                </td>
                                <td style="text-align: center">
                                    消費税
                                </td>
                                <td style="text-align: center">
                                    税込金額
                                </td>
                                <td style="text-align: center">
                                    請求書<br />
                                    発行日
                                </td>
                                <td style="text-align: center">
                                    売上<br />
                                    年月日
                                </td>
                                <td style="text-align: center">
                                    伝票売上<br />
                                    年月日
                                </td>
                                <td style="text-align: center">
                                    伝票売上<br />
                                    年月日修正
                                </td>
                            </tr>
                            <!-- 2行目 -->
                            <tr>
                                <td id="HaisoubiData" runat="server">
                                    <asp:TextBox ID="TextHttourokuDate" CssClass="date" Text="" runat="server" />
                                </td>
                                <td>
                                    <select id="SelectSeikyuuTool" runat="server">
                                        <option value="0">無</option>
                                        <option value="1" selected="selected">有</option>
                                    </select>
                                    <span id="Span3" runat="server"></span>
                                    <input id="buttonChgToolSeikyuu" type="button" value="初期ツール料請求有無変更時アクション" runat="server"
                                        style="display: none" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="SelectSyouhinToolRyou" runat="server">
                                    </asp:DropDownList><span id="Span4" runat="server"></span>
                                    <input id="buttonChgToolSyouhin" type="button" value="初期ツール料変更時アクション" runat="server"
                                        style="display: none" />
                                </td>
                                <td>
                                    <asp:TextBox ID="TextJituseikyuuZeinukikingakuTool" CssClass="kingaku" Text="" runat="server" />
                                    <input id="buttonChgToolJituGaku" type="button" value="初期ツール料実請求税抜き金額変更時アクション" runat="server"
                                        style="display: none" onserverclick="buttonChgToolJituGaku_ServerClick" />
                                </td>
                                <td>
                                    <asp:TextBox ID="TextSyouhizeiTool" Text="" CssClass="kingaku" runat="server" />
                                    <asp:HiddenField ID="hiddenZeiKbnTool" runat="server" />
                                    <input id="ButtonChgSyouhizeiTool" type="button" value="消費税額変更時アクション" runat="server"
                                        style="display: none;" onserverclick="ButtonChgSyouhizeiTool_ServerClick" />
                                </td>
                                <td>
                                    <asp:TextBox ID="TextZeikomiKingakuTool" ReadOnly="true" Text="" CssClass="kingaku readOnlyStyle2"
                                        TabIndex="-1" runat="server" />
                                </td>
                                <td>
                                    <asp:TextBox ID="TextSeikyuusyoHakkouDateTool" CssClass="date" Text="" runat="server"
                                        onblur="checkDate(this);" />
                                </td>
                                <td>
                                    <asp:TextBox ID="TextUriageNengappiTool" CssClass="date" Text="" runat="server" />
                                    <input id="buttonChgUriDateTool" type="button" value="売上年月日変更時アクション" runat="server"
                                        style="display: none" onserverclick="buttonChgUriDateTool_ServerClick" />
                                </td>
                                <td>
                                    <asp:TextBox ID="TextDenpyouUriDateDisplayTool" ReadOnly="true" Text="" CssClass="date readOnlyStyle2"
                                        TabIndex="-1" runat="server" />
                                </td>
                                <td>
                                    <asp:TextBox ID="TextDenpyouUriDateTool" CssClass="date" Text="" runat="server" /><input
                                        id="buttonChgDenUriDateTool" type="button" value="伝票売上年月日変更時アクション" runat="server"
                                        style="display: none" onserverclick="buttonChgDenUriDateTool_ServerClick" />
                                </td>
                            </tr>
                            <!-- 3行目 -->
                            <tr id="hansokuSyokiBikou" runat="server">
                                <td colspan="1" class="koumokuMei">
                                    備考
                                </td>
                                <td colspan="8">
                                    <asp:TextBox ID="TextHtBikou" MaxLength="40" Style="width: 23em;" Text="" runat="server" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:HiddenField ID="hiddenToolUpdateTime" runat="server" />
                    <asp:HiddenField ID="hiddenToolUriGaku" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </div>
        <div id="divHansoku" runat="server">
            <asp:UpdatePanel ID="UpdatePanelHansoku" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                <ContentTemplate>
                    <input type="hidden" id="hiddenHansokuLineCount" runat="server" />
                    <!-- 販促品ヘッダ部 -->
                    <table style="width: 100%;" class="mainTable" cellpadding="1">
                        <thead>
                            <tr>
                                <th class="tableTitle" colspan="9">
                                    <a id="hansokuhinDispLink" href="javascript:void(0);" runat="server">販促品</a><img
                                        src="images/spacer.gif" alt="" style="width: 30px; height: 1px;" />
                                    <asp:DropDownList ID="selectHansokuSyouhin" runat="server" Style="width: 300px">
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList><span id="Span5" runat="server"></span>
                                    <input type="button" value="新規行追加" id="buttonAddRow" class="button" runat="server"
                                        onserverclick="addRow_ServerClick" />
                                    税抜合計&nbsp;
                                    <asp:TextBox ID="TextHsZeinukiGoukei" CssClass="kingaku readOnlyStyle2" Style="width: 80px"
                                        ReadOnly="true" Text="" TabIndex="-1" runat="server" />
                                    &nbsp; 消費税&nbsp;
                                    <asp:TextBox ID="TextHsSyouhizei" CssClass="kingaku readOnlyStyle2" Style="width: 80px"
                                        ReadOnly="true" Text="" TabIndex="-1" runat="server" />
                                    &nbsp; 税込金額&nbsp;
                                    <asp:TextBox ID="TextHsZeikomiKingaku" CssClass="kingaku readOnlyStyle2" Style="width: 80px"
                                        ReadOnly="true" Text="" TabIndex="-1" runat="server" />
                                    <asp:HiddenField ID="hiddenKoumuTenSumBefor" runat="server" />
                                    <asp:HiddenField ID="hiddenKoumuTenSumAfter" runat="server" />
                                    <asp:HiddenField ID="hiddenHsZeikomiGoukeiBefor" runat="server" />
                                </th>
                            </tr>
                        </thead>
                        <tbody id="hansokuhinTbody" runat="server">
                            <tr>
                                <td style="padding: 0px;">
                                    <!-- 販促品ボディ部 -->
                                    <div class="dataGridHeader" id="dataGridContent" style="width: 100%; border: none;">
                                        <table class="scrolltablestyle itemTableNarrow" cellpadding="0" cellspacing="0" style="border-collapse: collapse;">
                                            <thead>
                                                <tr id="meisaiTableHeaderTr" style="text-align: center; position: relative; top: expression(this.offsetParent.scrollTop);">
                                                    <th id="hansokuHead" runat="server">
                                                        発送日</th>
                                                    <th>
                                                        商品コード</th>
                                                    <th>
                                                        商品名</th>
                                                    <th id="fcKoumutenHead" runat="server">
                                                        工務店<br />
                                                        請求単価</th>
                                                    <th id="JituseikyuuHead" runat="server">
                                                        実請求<br />
                                                        単価</th>
                                                    <th>
                                                        数量</th>
                                                    <th id="ZeinukiHead" runat="server">
                                                        税抜金額</th>
                                                    <th>
                                                        消費税</th>
                                                    <th>
                                                        税込金額</th>
                                                    <th>
                                                        請求書<br />
                                                        発行日</th>
                                                    <th>
                                                        売上<br />
                                                        年月日</th>
                                                    <th>
                                                        伝票売上<br />
                                                        年月日</th>
                                                    <th id="thDenpyouUriDate" runat="server">
                                                        伝票売上<br />
                                                        年月日修正</th>
                                                    <th>
                                                        請求先</th>
                                                    <th id="lineActHead" runat="server">
                                                        行処理</th>
                                                </tr>
                                            </thead>
                                            <tbody id="searchGrid" runat="server">
                                            </tbody>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <br />
        </div>
    </div>
    <table style="width: 100%;" cellpadding="8" cellspacing="0">
        <tr>
            <td class="tableFooter" id="tdTouroku1" runat="server">
                <input type="button" id="buttonTourokuCall" value="登録（登録料・販促初期ツール料）" style="width: 250px"
                    runat="server" onclick="executeConfirm(this)" />
                <input type="button" id="buttonTouroku" value="登録（登録料・販促初期ツール料）" style="width: 250px;
                    display: none" runat="server" />
            </td>
            <td class="tableFooter" id="tdTouroku2" runat="server">
                <input type="button" id="buttonTourokuHansokuCall" value="登録（販促品）" style="width: 250px"
                    runat="server" onclick="executeConfirm(this)" />
                <input type="button" id="buttonTourokuHansoku" value="登録（販促品）" style="width: 250px;
                    display: none" runat="server" />
            </td>
            <td class="tableFooter" id="tdClose" runat="server">
                <input type="button" id="buttonClose" value="閉じる" style="width: 100px" runat="server"
                    onclick="window.close();" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hiddenTyousaSeikyuuSaki" runat="server" />
    <asp:HiddenField ID="hiddenHansokuHinSeikyuuSaki" runat="server" />
    <asp:HiddenField ID="callModalFlg" runat="server" />
    <asp:HiddenField ID="hiddenIsFc" runat="server" />
    <asp:HiddenField ID="hiddenDispMode" runat="server" />
</asp:Content>

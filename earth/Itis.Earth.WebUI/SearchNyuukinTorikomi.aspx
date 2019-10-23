<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="SearchNyuukinTorikomi.aspx.vb" Inherits="Itis.Earth.WebUI.SearchNyuukinTorikomi"
    Title="EARTH 入金取込データ照会" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script src="js/sortable_ja.js" type="text/javascript">
    </script>

    <script type="text/javascript">
        //画面起動時にウィンドウサイズをディスプレイに合わせる
        window.resizeTo(1024,768);
        
        _d = document;
        /*====================================
         *グローバル変数宣言（画面部品）
         ====================================*/
         //コントロール接頭辞
        var gVarOyaSettouji = "ctl00_CPH1_";
        var gVarTr = "resultTr_";
        
        //画面遷移用
        var objSendBtn = null;
        var objSendTargetWin = null;
        
        var objSelectedTr = null;
        var objSendVal_NyuukinNo = null;
        
        var varAction = null;

        //画面表示部品
        var objNkTorikomiNoFrom = null;
        var objNkTorikomiNoTo = null;
        var objTorikomiDenpyouNoFrom = null;
        var objTorikomiDenpyouNoTo = null;
        var objNyuukinDateFrom = null;
        var objNyuukinDateTo = null;
        var objChkTorikesi = null;
        //検索実行用
        var objSearch = null;
        var objMaxSearchCount = null;
        
        /*************************************
         * onload時の追加処理
         *************************************/
        function funcAfterOnload() {
            //画面表示部品セット
            setGlobalObj();
            
            /*検索結果テーブル ソート設定*/
            sortables_init();   
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
            objNkTorikomiNoFrom = objEBI("<%= TextNyuukinNoFrom.clientID %>");
            objNkTorikomiNoTo = objEBI("<%= TextNyuukinNoTo.clientID %>");
            objTorikomiDenpyouNoFrom = objEBI("<%= TextTorikomiDenpyouNoFrom.clientID %>");
            objTorikomiDenpyouNoTo = objEBI("<%= TextTorikomiDenpyouNoTo.clientID %>");
            objNyuukinDateFrom = objEBI("<%= TextNyuukinDateFrom.clientID %>");
            objNyuukinDateTo = objEBI("<%= TextNyuukinDateTo.clientID %>");
            objSearch = objEBI("<%= ButtonHiddenSearch.clientID %>");
            objMaxSearchCount = objEBI("<%= maxSearchCount.clientID %>");
            objChkTorikesi = objEBI("<%= CheckBoxTorikesiTaisyou.clientID %>");
        }
        
        /**
         * 明細行をダブルクリックした際の処理
         * @param objSelectedTr
         * @param intGamen[1:入金取込修正]
         * @return
         */
        function returnSelectValue(objSelectedTr) {
            var varTrId = objSelectedTr.id;
            var varRow = varTrId.replace(gVarOyaSettouji + gVarTr,"");
            var varHdn = _d.getElementById(gVarOyaSettouji + "HiddenNkTorikomiNo_" + varRow);
           
            PopupSyuusei(varHdn.value);
        }
        
        //子画面呼出処理
        function PopupSyuusei(strUniqueNo){    
            //オブジェクトの再読込(Ajax再描画対応)
            objSendTargetWin = objEBI("<%= sendTargetWin.clientID %>");
            if(objSendTargetWin.value == "")objSendTargetWin.value="<%=EarthConst.MAIN_WINDOW_NAME %>";
            
            //オープン対象の画面を指定
            varAction = "<%=UrlConst.NYUUKIN_TORIKOMI_SYUUSEI %>";
            
            //<!-- 画面引渡し情報 -->
            objSendForm = objEBI("searchForm");
            //区分+番号+入力NO
            var objSendVal_SearchTerms = objEBI("sendSearchTerms");
            objSendVal_SearchTerms.value = strUniqueNo;
                        
            var varWindowName = "NyuukinTorikomiSyuusei";
            objSrchWin = window.open("about:Blank", varWindowName, "menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes");
                        
            //値セット
            objSendTargetWin.value = varWindowName;
            objSendForm.target=objSendTargetWin.value;
                            
            objSendForm.action = varAction;
            objSendForm.submit();
        }
        
        /***********************************
         * 「検索実行」押下時のチェック処理
         ***********************************/
        function checkJikkou(){
           
            //入金取込NO 大小チェック
            if(!checkDaiSyou(objNkTorikomiNoFrom,objNkTorikomiNoTo,"入金取込NO"))return false;
            
            //入金取込NO_FROM 最大値チェック
            if(Number(removeFigure(objNkTorikomiNoFrom.value)) > 2147483647){
                alert("<%= Messages.MSG154E %>");
                objNkTorikomiNoFrom.focus();
                return false;
            }
            //入金取込NO_TO 最大値チェック
            if(Number(removeFigure(objNkTorikomiNoTo.value)) > 2147483647){
                alert("<%= Messages.MSG154E %>");
                objNkTorikomiNoTo.focus();
                return false;
            }
            
            //取込伝票番号 大小チェック
            if(!checkDaiSyou(objTorikomiDenpyouNoFrom,objTorikomiDenpyouNoTo,"取込伝票番号"))return false;
            
            //入金日 大小チェック
            if(!checkDaiSyou(objNyuukinDateFrom,objNyuukinDateTo,"入金日"))return false;
            
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

        /*******************************************
         * Allクリア処理後に実行されるファンクション
         *******************************************/
        function funcAfterAllClear(obj){
            objMaxSearchCount.selectedIndex = 1;
            objChkTorikesi.checked = true;
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
                objEBI("<%= SelectSeikyuuKbn.clientID %>").style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>"
            }
        }
        
    </script>

    <asp:ScriptManagerProxy ID="SMP1" runat="server">
    </asp:ScriptManagerProxy>
    <div>
        <%-- 画面タイトル --%>
        <table>
            <tr>
                <td>
                    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
                        class="titleTable">
                        <tr>
                            <th style="text-align: left; width: 160px;">
                                入金取込データ照会</th>
                            <th style="width: 80px">
                                <input type="button" id="ButtonSinki" value="新規登録" style="width: 80px;" runat="server"
                                    onclick="PopupSyuusei(0)" />
                            </th>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <%-- 画面上部[検索条件] --%>
        <table cellpadding="0" cellspacing="0" style="border-bottom: solid 2px gray; border-left: solid 2px gray;"
            class="mainTable">
            <thead>
                <tr>
                    <th class="tableTitle" colspan="6" rowspan="1">
                        検索条件
                        <input id="btnClearWin" value="クリア" type="reset" class="button" /></th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td class="koumokuMei" style="width: 100px">
                        入金取込NO</td>
                    <td>
                        <input id="TextNyuukinNoFrom" runat="server" style="width: 70px;" class="number"
                            maxlength="10" />～<input id="TextNyuukinNoTo" runat="server" style="width: 70px;"
                                class="number" maxlength="10" />
                    </td>
                    <td class="koumokuMei">
                        取込伝票番号</td>
                    <td colspan="3">
                        <input id="TextTorikomiDenpyouNoFrom" runat="server" maxlength="6" style="width: 70px;"
                            class="codeNumber" />～<input id="TextTorikomiDenpyouNoTo" runat="server" maxlength="6"
                                style="width: 70px;" class="codeNumber" />
                    </td>
                </tr>
                <tr>
                    <td class="koumokuMei">
                        入金日</td>
                    <td>
                        <input id="TextNyuukinDateFrom" runat="server" maxlength="10" class="date" />～<input
                            id="TextNyuukinDateTo" runat="server" maxlength="10" class="date" /></td>
                    <td class="koumokuMei">
                        EDI情報作成日
                    </td>
                    <td style="border-right-style: none;">
                        <input id="TextEdiJouhouSakuseiDate" runat="server" class="" style="width: 280px"
                            maxlength="40" />
                    </td>
                </tr>
                <tr>
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
                                style="width: 250px" tabindex="-1" />&nbsp;
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    </td>
                    <td class="koumokuMei">
                        取消</td>
                    <td>
                    <asp:UpdatePanel ID="UpdatePanel_SeikyuusakiToikesi" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSeikyuuSakiSearch" />
                        </Triggers>
                        <ContentTemplate>
                            <input id="TextTorikesiRiyuu" runat="server" style="width: 80px;" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    </td>
                </tr>
                <tr class="tableSpacer">
                    <td colspan="6">
                    </td>
                </tr>
                <tr>
                    <td colspan="6" rowspan="1" style="text-align: center">
                        <img src="images/spacer.gif" alt="" style="width: 30px; height: 0px;" />
                        検索上限件数
                        <select id="maxSearchCount" runat="server">
                            <option value="10">10件</option>
                            <option value="100" selected="selected">100件</option>
                            <option value="max">無制限</option>
                        </select>
                        <input type="button" id="ButtonSearch" value="検索実行" runat="server" onclick="checkJikkou()" />
                        <input type="button" id="ButtonHiddenSearch" value="検索実行btn" style="display: none"
                            runat="server" />
                        <img src="images/spacer.gif" alt="" style="width: 30px; height: 0px;" />
                        <input id="CheckBoxTorikesiTaisyou" value="0" type="checkbox" runat="server" checked="checked" />取消は検索対象外
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
                <td style="width: 10px">
                </td>
                <td>
                    総合計金額：
                </td>
                <td id="TdTotalKingaku" runat="server">
                </td>
            </tr>
        </table>
        <%-- 画面上部[入金取込データ情報] --%>
        <div class="dataGridHeader" id="dataGridContent" style="width: 870px">
            <table class="scrolltablestyle2 sortableTitle" id="meisaiTable" cellpadding="0" cellspacing="0">
                <%-- ヘッダ部 --%>
                <thead>
                    <tr id="meisaiTableHeaderTr" runat="server" style="position: relative; top: expression(this.offsetParent.scrollTop)">
                        <th style="width: 64px">
                            入金取込NO</th>
                        <th style="width: 100px">
                            EDI情報作成日</th>
                        <th style="width: 30px">
                            取消</th>
                        <th style="width: 90px">
                            入金日</th>
                        <th style="width: 80px">
                            取込伝票番号</th>
                        <th style="width: 80px">
                            請求先コード</th>
                        <th style="width: 160px">
                            請求先名</th>
                        <th style="width: 80px">
                            照合口座No.</th>
                        <th style="width: 80px">
                            伝票金額合計</th>
                    </tr>
                </thead>
                <!-- データ部 -->
                <tbody id="searchGrid" runat="server">
                </tbody>
            </table>
        </div>
    </div>
    <input type="hidden" id="sendTargetWin" runat="server" />
</asp:Content>

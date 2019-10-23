<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="SeikyuusyoDataSakusei.aspx.vb" Inherits="Itis.Earth.WebUI.SeikyuusyoDataSakusei"
    Title="EARTH 請求書データ作成" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script type="text/javascript">
    /****************************************
     * onload時の追加処理
     ****************************************/
    function funcAfterOnload() {
    
        if (objEBI('<%=Me.HiddenMovePageType.ClientID %>').value != ""){
            moveNextPage();
        }
    }
    
    /*********************************************
     * 請求書データ作成時処理
     *********************************************/  
    function executeConfirm(objCtrl){
        var varMsg = "";
        var objExeBtn = null;
        var objMinDate = "<%= EarthConst.Instance.MIN_DATE %>";
        var objMaxDate = "<%= EarthConst.Instance.MAX_DATE %>";
        var objSeikyuusyoHakDate = objEBI("<%= TextSeikyuusyoHakDate.clientID %>");
        
        //請求書発行日 必須チェック
        if(objSeikyuusyoHakDate.value.Trim() == ""){
            varMsg = "<%= Messages.MSG013E %>";
            varMsg = varMsg.replace("@PARAM1","請求書発行日");
            alert(varMsg);
            objEBI("<%= TextSeikyuusyoHakDate.clientID %>").focus();
            return false;
        }
        
        //請求書発行日 日付チェック
        if(objSeikyuusyoHakDate.value.Trim() > objMaxDate || objSeikyuusyoHakDate.value.Trim() < objMinDate){
            varMsg = "<%= Messages.MSG088E %>";
            varMsg = varMsg.replace("@PARAM1","請求書発行日");
            alert(varMsg);
            objEBI("<%= TextSeikyuusyoHakDate.clientID %>").focus();
            return false;
        }

        //請求書データ作成ボタン
        if(objCtrl == objEBI("<%= BtnSeikyuusyoDataSakuseiCall.clientID %>")){
        
            //請求書データ作成ボタンを実行
            objExeBtn = objEBI("<%= ButtonSeikyuusyoDataSakusei.clientID %>");
        
            //通常の確認メッセージ
            if(!confirm('<%= Messages.MSG042C.Replace("@PARAM1", "請求書データ作成") %>')){
                return false;
            }
            
            //全締日、全請求先を対象チェックボックス
            var objChk = objEBI("<%= CheckAllSakusei.clientID %>");
            
            //請求先指定とチェックがあれば再度確認MSG
            if((getSeikyuuInfo() == true) && (objChk.checked == true)){
                if(!confirm('<%= Messages.MSG190C.Replace("@PARAM1", "請求書データ作成") %>')){
                    return false;
                }  
            }
        }else if(objCtrl == objEBI("<%= btnGetDateCall.clientID %>")){
            //日付取得ボタン
            objExeBtn = objEBI("<%= btnGetDate.clientID %>");
        }
        
        //画面グレイアウト
        setWindowOverlay(objCtrl);
        //実行ボタン押下
        objExeBtn.click();
    }
    
    /*********************************************
     * ボタン実行処理
     *********************************************/  
    function executeBtn(objCtrl){
        var objExeBtn = null;
        
        //日付取得ボタン
        if(objCtrl == objEBI("<%= btnGetDateCall.clientID %>")){
            objExeBtn = objEBI("<%= btnGetDate.clientID %>");
        }
        
        //画面グレイアウト
        setWindowOverlay(objCtrl);
        //実行ボタン押下
        objExeBtn.click();
    }
    
    /*********************************************
     * 画面遷移ボタン押下時チェック処理
     *********************************************/
    function checkJikkou(varBtn){
        var objMoveType = objEBI("<%= HiddenMovePageType.clientID %>");
        
        //請求書一覧ボタン押下処理
        if(varBtn.id == objEBI("<%= ButtonSyuturyokuSeikyuusyo.clientID %>").id){
            objMoveType.value = "<%= EarthEnum.emSeikyuuSearchType.SearchSeikyuusyo %>";
        }

        //過去請求書一覧ボタン押下処理
        if(varBtn.id == objEBI("<%= ButtonKakoSeikyuusyo.clientID %>").id){
            objMoveType.value = ("<%= EarthEnum.emSeikyuuSearchType.KakoSearchSeikyuusyo %>");
        }
        
        //請求書締め日履歴ボタン押下処理
        if(varBtn.id == objEBI("<%= ButtonSeikyuusyoSimeDateRireki.clientID %>").id){
            objMoveType.value = ("<%= EarthEnum.emSeikyuuSearchType.SeikyuusyoSimeDateRireki %>");            
        }

        moveNextPage(varBtn);
    }

    /*********************************************
     * 画面遷移ボタン押下時チェク処理
     *********************************************/    
    function moveNextPage(varBtn){
        if(varBtn.id == objEBI("<%= ButtonSyuturyokuSeikyuusyo.clientID %>").id | varBtn.id == objEBI("<%= ButtonKakoSeikyuusyo.clientID %>").id){
            var viewForm;
            viewForm = objEBI('openPageForm');
            viewForm.reset();
            objEBI('st').value = objEBI('<%=Me.HiddenMovePageType.ClientID %>').value;
            viewForm.method = 'post';
            viewForm.action = '<%=UrlConst.SEARCH_SEIKYUUSYO %>';
            viewForm.submit();
        }
        else
        if(varBtn.id == objEBI("<%= ButtonSeikyuusyoSimeDateRireki.clientID %>").id){
            var viewForm;
            viewForm = objEBI('openPageForm');
            viewForm.reset();
            objEBI('st').value = objEBI('<%=Me.HiddenMovePageType.ClientID %>').value;
            viewForm.method = 'post';
            viewForm.action = '<%=UrlConst.SEARCH_SEIKYUUSYO_SIME_DATE_RIREKI %>';
            viewForm.submit();
        }
    } 
    
    /*********************************************
     * 値をチェックし、対象をクリアする
     *********************************************/
     function clrName(obj,targetId){
         if(obj.value == "") objEBI(targetId).value="";
     }
     
    /*********************************************
     * 請求先情報のクリアをする(請求書発行日以外)
     *********************************************/
     function clearSeikyuuInfo(){
        var objSeikyuusyoHakDate;
        objSeikyuusyoHakDate = objEBI('<%=Me.TextSeikyuusyoHakDate.ClientID %>').value;
        
        allClear(false);
        
        objEBI('<%=Me.TextSeikyuusyoHakDate.ClientID %>').value = objSeikyuusyoHakDate;
        
     }
     
    /*********************************************
     * 請求先情報を取得する
     *********************************************/
     function getSeikyuuInfo(){

        var arrInfo = new Array();
        
        //請求先コード
        arrInfo.push(objEBI("<%= TextSeikyuuSakiCd_1.clientID %>").value);
        arrInfo.push(objEBI("<%= TextSeikyuuSakiCd_2.clientID %>").value);
        arrInfo.push(objEBI("<%= TextSeikyuuSakiCd_3.clientID %>").value);
        arrInfo.push(objEBI("<%= TextSeikyuuSakiCd_4.clientID %>").value);
        arrInfo.push(objEBI("<%= TextSeikyuuSakiCd_5.clientID %>").value);
        arrInfo.push(objEBI("<%= TextSeikyuuSakiCd_6.clientID %>").value);
        arrInfo.push(objEBI("<%= TextSeikyuuSakiCd_7.clientID %>").value);
        arrInfo.push(objEBI("<%= TextSeikyuuSakiCd_8.clientID %>").value);
        arrInfo.push(objEBI("<%= TextSeikyuuSakiCd_9.clientID %>").value);
        arrInfo.push(objEBI("<%= TextSeikyuuSakiCd_10.clientID %>").value);
        //請求先枝番
        arrInfo.push(objEBI("<%= TextSeikyuuSakiBrc_1.clientID %>").value);
        arrInfo.push(objEBI("<%= TextSeikyuuSakiBrc_2.clientID %>").value);
        arrInfo.push(objEBI("<%= TextSeikyuuSakiBrc_3.clientID %>").value);
        arrInfo.push(objEBI("<%= TextSeikyuuSakiBrc_4.clientID %>").value);
        arrInfo.push(objEBI("<%= TextSeikyuuSakiBrc_5.clientID %>").value);
        arrInfo.push(objEBI("<%= TextSeikyuuSakiBrc_6.clientID %>").value);
        arrInfo.push(objEBI("<%= TextSeikyuuSakiBrc_7.clientID %>").value);
        arrInfo.push(objEBI("<%= TextSeikyuuSakiBrc_8.clientID %>").value);
        arrInfo.push(objEBI("<%= TextSeikyuuSakiBrc_9.clientID %>").value);
        arrInfo.push(objEBI("<%= TextSeikyuuSakiBrc_10.clientID %>").value);
        //請求先区分
        arrInfo.push(objEBI("<%= SelectSeikyuuSakiKbn_1.clientID %>").value);
        arrInfo.push(objEBI("<%= SelectSeikyuuSakiKbn_2.clientID %>").value);
        arrInfo.push(objEBI("<%= SelectSeikyuuSakiKbn_3.clientID %>").value);
        arrInfo.push(objEBI("<%= SelectSeikyuuSakiKbn_4.clientID %>").value);
        arrInfo.push(objEBI("<%= SelectSeikyuuSakiKbn_5.clientID %>").value);
        arrInfo.push(objEBI("<%= SelectSeikyuuSakiKbn_6.clientID %>").value);
        arrInfo.push(objEBI("<%= SelectSeikyuuSakiKbn_7.clientID %>").value);
        arrInfo.push(objEBI("<%= SelectSeikyuuSakiKbn_8.clientID %>").value);
        arrInfo.push(objEBI("<%= SelectSeikyuuSakiKbn_9.clientID %>").value);
        arrInfo.push(objEBI("<%= SelectSeikyuuSakiKbn_10.clientID %>").value);
     
        //配列を文字列へ連結
        var strInfo = arrInfo.join('');
     
        /* 請求先指定存在チェック */
        if(strInfo != ""){
            return true;
        }
        return false;
     }
        
    </script>

    <!-- 画面タイトル -->
    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tbody>
            <tr>
                <th style="text-align: left">
                    請求書データ作成
                </th>
            </tr>
            <tr>
                <td style="height: 10px">
                </td>
            </tr>
        </tbody>
    </table>
    <!-- ●請求書発行 -->
    <!-- 画面上部・ヘッダ -->
    <table style="font-size: 12pt; width: 548px; height: 20px; text-align: center;　background-color: lemonchiffon;
        padding-top: 6px; border: 2px solid gray; border-bottom: 0px; font-weight: bold; margin-top:5px;" >
        <tr>
            <td style ="text-align: right; font-size: 12pt;">請求書発行 　　　　　　　　           
                <input type="button" id="ButtonSeikyuusyoSimeDateRireki" runat="server" value="請求書締め日履歴" style="font-size: 10px;
                   width: 120px; color: black; height: 20px;" />  
            </td>
        </tr>   
    </table>
    <table style="text-align: left; width: 548px; border-bottom: solid 0px gray; border-left: solid 2px gray;
        border-right: solid 2px gray; border-top: solid 2px gray;" id="topTable" class="mainTable"
        cellpadding="0" cellspacing="0">
        <!-- 1行目 -->
        <tr>
            <td style="width: 100px; height: 30px;" class="koumokuMei">
                請求書発行日
            </td>
            <td style="width: 75px;">
                <input type="text" name="SeikyuusyoHakDate" id="TextSeikyuusyoHakDate" runat="server"
                    class="date" maxlength="10" style="width: 67px;" />
            </td>
            <td style="width: 80px;">
                <input id="btnGetDateCall" value="日付取得" type="button" runat="server" class="button" onclick="executeBtn(this);" />
                <input id="btnGetDate" value="日付取得" type="button" runat="server" class="button" style="display: none;" />
            </td>
            <td style="width: 25px; border-right: 0px;">
                &nbsp;<input id="CheckAllSakusei" value="0" type="checkbox" runat="server" />
            </td>
            <td style="width: 245px; border-left: 0px;">
                全締日、全請求先を作成対象</td>
        </tr>
    </table>
    <table style="text-align: left; width: 548px; border-bottom: solid 2px gray; border-left: solid 2px gray;
        border-right: solid 2px gray; border-top: solid 0px gray;" id="seikyuuTable"
        class="mainTable" cellpadding="0" cellspacing="0">
        <thead>
            <tr>
                <th class="tableTitle" colspan="6" style="height: 23px; border-top: solid 1px gray;">
                    <a id="SeikyuuDispLink" runat="server">請求先指定</a>
                    <input type="hidden" id="HiddenDispStyle" runat="server" value="none" />
                    <span id="SeikyuuTitleInfobar" style="display: inline;" runat="server"></span>
                    <input id="btnClearWin" value="クリア" type="button" class="button" onclick="clearSeikyuuInfo()"/>
                </th>
            </tr>
        </thead>
        <!-- 2行目 -->
        <tbody id="TBodySeikyuuInfo" runat="server">
            <tr id="TrSeikyuu_1" runat="server">
                <td style="width: 96px; height: 20px;" class="koumokuMei">
                    請求先 1</td>
                <td colspan="2">
                    <asp:UpdatePanel ID="UpdatePanelSeikyuusaki_1" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <ContentTemplate>
                            <asp:DropDownList ID="SelectSeikyuuSakiKbn_1" runat="server" OnSelectedIndexChanged="SelectSeikyuuSakiKbn_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:TextBox ID="TextSeikyuuSakiCd_1" runat="server" MaxLength="5" Style="width: 35px;"
                                CssClass="codeNumber" OnTextChanged="TextSeikyuuSakiCd_TextChanged" />&nbsp;-
                            <asp:TextBox ID="TextSeikyuuSakiBrc_1" runat="server" MaxLength="2" Style="width: 15px;"
                                CssClass="codeNumber" OnTextChanged="TextSeikyuuSakiBrc_TextChanged" />
                            <input id="btnSeikyuuSakiSearch_1" runat="server" type="button" value="検索" class="gyoumuSearchBtn"
                                 onserverclick="btnSeikyuuSakiSearch_ServerClick" />&nbsp;
                            <input id="TextSeikyuuSakiMei_1" runat="server" class="readOnlyStyle" readonly="readonly"
                                style="width: 190px" tabindex="-1" />
                            <asp:TextBox ID="TextSeikyuuSakiMeiHdn_1" runat="server" Style="display: none" TabIndex="-1" />&nbsp;
                            <input type="hidden" id="HiddenSeikyuuSakiCdOld_1" runat="server" />
                            <input type="hidden" id="HiddenSeikyuuSakiBrcOld_1" runat="server" />
                            <input type="hidden" id="HiddenSeikyuuSakiKbnOld_1" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr id="TrSeikyuu_2" runat="server">
                <td style="width: 96px; height: 20px;" class="koumokuMei">
                    請求先 2</td>
                <td colspan="2">
                    <asp:UpdatePanel ID="UpdatePanelSeikyuusaki_2" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <ContentTemplate>
                            <asp:DropDownList ID="SelectSeikyuuSakiKbn_2" runat="server" OnSelectedIndexChanged="SelectSeikyuuSakiKbn_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:TextBox ID="TextSeikyuuSakiCd_2" runat="server" MaxLength="5" Style="width: 35px;"
                                CssClass="codeNumber" OnTextChanged="TextSeikyuuSakiCd_TextChanged" />&nbsp;-
                            <asp:TextBox ID="TextSeikyuuSakiBrc_2" runat="server" MaxLength="2" Style="width: 15px;"
                                CssClass="codeNumber" OnTextChanged="TextSeikyuuSakiBrc_TextChanged" />
                            <input id="btnSeikyuuSakiSearch_2" runat="server" type="button" value="検索" class="gyoumuSearchBtn"
                                 onserverclick="btnSeikyuuSakiSearch_ServerClick" />&nbsp;
                            <input id="TextSeikyuuSakiMei_2" runat="server" class="readOnlyStyle" readonly="readonly"
                                style="width: 190px" tabindex="-1" />
                            <asp:TextBox ID="TextSeikyuuSakiMeiHdn_2" runat="server" Style="display: none" TabIndex="-1" />&nbsp;
                            <input type="hidden" id="HiddenSeikyuuSakiCdOld_2" runat="server" />
                            <input type="hidden" id="HiddenSeikyuuSakiBrcOld_2" runat="server" />
                            <input type="hidden" id="HiddenSeikyuuSakiKbnOld_2" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr id="TrSeikyuu_3" runat="server">
                <td style="width: 96px; height: 20px;" class="koumokuMei">
                    請求先 3</td>
                <td colspan="2">
                    <asp:UpdatePanel ID="UpdatePanelSeikyuusaki_3" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <ContentTemplate>
                            <asp:DropDownList ID="SelectSeikyuuSakiKbn_3" runat="server"  OnSelectedIndexChanged="SelectSeikyuuSakiKbn_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:TextBox ID="TextSeikyuuSakiCd_3" runat="server" MaxLength="5" Style="width: 35px;"
                                CssClass="codeNumber" OnTextChanged="TextSeikyuuSakiCd_TextChanged" />&nbsp;-
                            <asp:TextBox ID="TextSeikyuuSakiBrc_3" runat="server" MaxLength="2" Style="width: 15px;"
                                CssClass="codeNumber" OnTextChanged="TextSeikyuuSakiBrc_TextChanged" />
                            <input id="btnSeikyuuSakiSearch_3" runat="server" type="button" value="検索" class="gyoumuSearchBtn"
                                 onserverclick="btnSeikyuuSakiSearch_ServerClick" />&nbsp;
                            <input id="TextSeikyuuSakiMei_3" runat="server" class="readOnlyStyle" readonly="readonly"
                                style="width: 190px" tabindex="-1" />
                            <asp:TextBox ID="TextSeikyuuSakiMeiHdn_3" runat="server" Style="display: none" TabIndex="-1" />&nbsp;
                            <input type="hidden" id="HiddenSeikyuuSakiCdOld_3" runat="server" />
                            <input type="hidden" id="HiddenSeikyuuSakiBrcOld_3" runat="server" />
                            <input type="hidden" id="HiddenSeikyuuSakiKbnOld_3" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr id="TrSeikyuu_4" runat="server">
                <td style="width: 96px; height: 20px;" class="koumokuMei">
                    請求先 4</td>
                <td colspan="2">
                    <asp:UpdatePanel ID="UpdatePanelSeikyuusaki_4" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <ContentTemplate>
                            <asp:DropDownList ID="SelectSeikyuuSakiKbn_4" runat="server"  OnSelectedIndexChanged="SelectSeikyuuSakiKbn_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:TextBox ID="TextSeikyuuSakiCd_4" runat="server" MaxLength="5" Style="width: 35px;"
                                CssClass="codeNumber" OnTextChanged="TextSeikyuuSakiCd_TextChanged" />&nbsp;-
                            <asp:TextBox ID="TextSeikyuuSakiBrc_4" runat="server" MaxLength="2" Style="width: 15px;"
                                CssClass="codeNumber" OnTextChanged="TextSeikyuuSakiBrc_TextChanged" />
                            <input id="btnSeikyuuSakiSearch_4" runat="server" type="button" value="検索" class="gyoumuSearchBtn"
                                 onserverclick="btnSeikyuuSakiSearch_ServerClick" />&nbsp;
                            <input id="TextSeikyuuSakiMei_4" runat="server" class="readOnlyStyle" readonly="readonly"
                                style="width: 190px" tabindex="-1" />
                            <asp:TextBox ID="TextSeikyuuSakiMeiHdn_4" runat="server" Style="display: none" TabIndex="-1" />&nbsp;
                            <input type="hidden" id="HiddenSeikyuuSakiCdOld_4" runat="server" />
                            <input type="hidden" id="HiddenSeikyuuSakiBrcOld_4" runat="server" />
                            <input type="hidden" id="HiddenSeikyuuSakiKbnOld_4" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr id="TrSeikyuu_5" runat="server">
                <td style="width: 96px; height: 20px;" class="koumokuMei">
                    請求先 5</td>
                <td colspan="2">
                    <asp:UpdatePanel ID="UpdatePanelSeikyuusaki_5" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <ContentTemplate>
                            <asp:DropDownList ID="SelectSeikyuuSakiKbn_5" runat="server"  OnSelectedIndexChanged="SelectSeikyuuSakiKbn_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:TextBox ID="TextSeikyuuSakiCd_5" runat="server" MaxLength="5" Style="width: 35px;"
                                CssClass="codeNumber" OnTextChanged="TextSeikyuuSakiCd_TextChanged" />&nbsp;-
                            <asp:TextBox ID="TextSeikyuuSakiBrc_5" runat="server" MaxLength="2" Style="width: 15px;"
                                CssClass="codeNumber" OnTextChanged="TextSeikyuuSakiBrc_TextChanged" />
                            <input id="btnSeikyuuSakiSearch_5" runat="server" type="button" value="検索" class="gyoumuSearchBtn"
                                 onserverclick="btnSeikyuuSakiSearch_ServerClick" />&nbsp;
                            <input id="TextSeikyuuSakiMei_5" runat="server" class="readOnlyStyle" readonly="readonly"
                                style="width: 190px" tabindex="-1" />
                            <asp:TextBox ID="TextSeikyuuSakiMeiHdn_5" runat="server" Style="display: none" TabIndex="-1" />&nbsp;
                            <input type="hidden" id="HiddenSeikyuuSakiCdOld_5" runat="server" />
                            <input type="hidden" id="HiddenSeikyuuSakiBrcOld_5" runat="server" />
                            <input type="hidden" id="HiddenSeikyuuSakiKbnOld_5" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr id="TrSeikyuu_6" runat="server">
                <td style="width: 96px; height: 20px;" class="koumokuMei">
                    請求先 6</td>
                <td colspan="2">
                    <asp:UpdatePanel ID="UpdatePanelSeikyuusaki_6" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <ContentTemplate>
                            <asp:DropDownList ID="SelectSeikyuuSakiKbn_6" runat="server"  OnSelectedIndexChanged="SelectSeikyuuSakiKbn_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:TextBox ID="TextSeikyuuSakiCd_6" runat="server" MaxLength="5" Style="width: 35px;"
                                CssClass="codeNumber" OnTextChanged="TextSeikyuuSakiCd_TextChanged" />&nbsp;-
                            <asp:TextBox ID="TextSeikyuuSakiBrc_6" runat="server" MaxLength="2" Style="width: 15px;"
                                CssClass="codeNumber" OnTextChanged="TextSeikyuuSakiBrc_TextChanged" />
                            <input id="btnSeikyuuSakiSearch_6" runat="server" type="button" value="検索" class="gyoumuSearchBtn"
                                 onserverclick="btnSeikyuuSakiSearch_ServerClick" />&nbsp;
                            <input id="TextSeikyuuSakiMei_6" runat="server" class="readOnlyStyle" readonly="readonly"
                                style="width: 190px" tabindex="-1" />
                            <asp:TextBox ID="TextSeikyuuSakiMeiHdn_6" runat="server" Style="display: none" TabIndex="-1" />&nbsp;
                            <input type="hidden" id="HiddenSeikyuuSakiCdOld_6" runat="server" />
                            <input type="hidden" id="HiddenSeikyuuSakiBrcOld_6" runat="server" />
                            <input type="hidden" id="HiddenSeikyuuSakiKbnOld_6" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr id="TrSeikyuu_7" runat="server">
                <td style="width: 96px; height: 20px;" class="koumokuMei">
                    請求先 7</td>
                <td colspan="2">
                    <asp:UpdatePanel ID="UpdatePanelSeikyuusaki_7" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <ContentTemplate>
                            <asp:DropDownList ID="SelectSeikyuuSakiKbn_7" runat="server"  OnSelectedIndexChanged="SelectSeikyuuSakiKbn_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:TextBox ID="TextSeikyuuSakiCd_7" runat="server" MaxLength="5" Style="width: 35px;"
                                CssClass="codeNumber" OnTextChanged="TextSeikyuuSakiCd_TextChanged" />&nbsp;-
                            <asp:TextBox ID="TextSeikyuuSakiBrc_7" runat="server" MaxLength="2" Style="width: 15px;"
                                CssClass="codeNumber" OnTextChanged="TextSeikyuuSakiBrc_TextChanged" />
                            <input id="btnSeikyuuSakiSearch_7" runat="server" type="button" value="検索" class="gyoumuSearchBtn"
                                 onserverclick="btnSeikyuuSakiSearch_ServerClick" />&nbsp;
                            <input id="TextSeikyuuSakiMei_7" runat="server" class="readOnlyStyle" readonly="readonly"
                                style="width: 190px" tabindex="-1" />
                            <asp:TextBox ID="TextSeikyuuSakiMeiHdn_7" runat="server" Style="display: none" TabIndex="-1" />&nbsp;
                            <input type="hidden" id="HiddenSeikyuuSakiCdOld_7" runat="server" />
                            <input type="hidden" id="HiddenSeikyuuSakiBrcOld_7" runat="server" />
                            <input type="hidden" id="HiddenSeikyuuSakiKbnOld_7" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr id="TrSeikyuu_8" runat="server">
                <td style="width: 96px; height: 20px;" class="koumokuMei">
                    請求先 8</td>
                <td colspan="2">
                    <asp:UpdatePanel ID="UpdatePanelSeikyuusaki_8" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <ContentTemplate>
                            <asp:DropDownList ID="SelectSeikyuuSakiKbn_8" runat="server"  OnSelectedIndexChanged="SelectSeikyuuSakiKbn_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:TextBox ID="TextSeikyuuSakiCd_8" runat="server" MaxLength="5" Style="width: 35px;"
                                CssClass="codeNumber" OnTextChanged="TextSeikyuuSakiCd_TextChanged" />&nbsp;-
                            <asp:TextBox ID="TextSeikyuuSakiBrc_8" runat="server" MaxLength="2" Style="width: 15px;"
                                CssClass="codeNumber" OnTextChanged="TextSeikyuuSakiBrc_TextChanged" />
                            <input id="btnSeikyuuSakiSearch_8" runat="server" type="button" value="検索" class="gyoumuSearchBtn"
                                 onserverclick="btnSeikyuuSakiSearch_ServerClick" />&nbsp;
                            <input id="TextSeikyuuSakiMei_8" runat="server" class="readOnlyStyle" readonly="readonly"
                                style="width: 190px" tabindex="-1" />
                            <asp:TextBox ID="TextSeikyuuSakiMeiHdn_8" runat="server" Style="display: none" TabIndex="-1" />&nbsp;
                            <input type="hidden" id="HiddenSeikyuuSakiCdOld_8" runat="server" />
                            <input type="hidden" id="HiddenSeikyuuSakiBrcOld_8" runat="server" />
                            <input type="hidden" id="HiddenSeikyuuSakiKbnOld_8" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr id="TrSeikyuu_9" runat="server">
                <td style="width: 96px; height: 20px;" class="koumokuMei">
                    請求先 9</td>
                <td colspan="2">
                    <asp:UpdatePanel ID="UpdatePanelSeikyuusaki_9" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <ContentTemplate>
                            <asp:DropDownList ID="SelectSeikyuuSakiKbn_9" runat="server"  OnSelectedIndexChanged="SelectSeikyuuSakiKbn_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:TextBox ID="TextSeikyuuSakiCd_9" runat="server" MaxLength="5" Style="width: 35px;"
                                CssClass="codeNumber" OnTextChanged="TextSeikyuuSakiCd_TextChanged" />&nbsp;-
                            <asp:TextBox ID="TextSeikyuuSakiBrc_9" runat="server" MaxLength="2" Style="width: 15px;"
                                CssClass="codeNumber" OnTextChanged="TextSeikyuuSakiBrc_TextChanged" />
                            <input id="btnSeikyuuSakiSearch_9" runat="server" type="button" value="検索" class="gyoumuSearchBtn"
                                 onserverclick="btnSeikyuuSakiSearch_ServerClick" />&nbsp;
                            <input id="TextSeikyuuSakiMei_9" runat="server" class="readOnlyStyle" readonly="readonly"
                                style="width: 190px" tabindex="-1" />
                            <asp:TextBox ID="TextSeikyuuSakiMeiHdn_9" runat="server" Style="display: none" TabIndex="-1" />&nbsp;
                            <input type="hidden" id="HiddenSeikyuuSakiCdOld_9" runat="server" />
                            <input type="hidden" id="HiddenSeikyuuSakiBrcOld_9" runat="server" />
                            <input type="hidden" id="HiddenSeikyuuSakiKbnOld_9" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr id="TrSeikyuu_10" runat="server">
                <td style="width: 96px; height: 20px;" class="koumokuMei">
                    請求先 10</td>
                <td colspan="2">
                    <asp:UpdatePanel ID="UpdatePanelSeikyuusaki_10" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <ContentTemplate>
                            <asp:DropDownList ID="SelectSeikyuuSakiKbn_10" runat="server"  OnSelectedIndexChanged="SelectSeikyuuSakiKbn_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:TextBox ID="TextSeikyuuSakiCd_10" runat="server" MaxLength="5" Style="width: 35px;"
                                CssClass="codeNumber" OnTextChanged="TextSeikyuuSakiCd_TextChanged" />&nbsp;-
                            <asp:TextBox ID="TextSeikyuuSakiBrc_10" runat="server" MaxLength="2" Style="width: 15px;"
                                CssClass="codeNumber" OnTextChanged="TextSeikyuuSakiBrc_TextChanged" />
                            <input id="btnSeikyuuSakiSearch_10" runat="server" type="button" value="検索" class="gyoumuSearchBtn"
                                 onserverclick="btnSeikyuuSakiSearch_ServerClick" />&nbsp;
                            <input id="TextSeikyuuSakiMei_10" runat="server" class="readOnlyStyle" readonly="readonly"
                                style="width: 190px" tabindex="-1" />
                            <asp:TextBox ID="TextSeikyuuSakiMeiHdn_10" runat="server" Style="display: none" TabIndex="-1" />&nbsp;
                            <input type="hidden" id="HiddenSeikyuuSakiCdOld_10" runat="server" />
                            <input type="hidden" id="HiddenSeikyuuSakiBrcOld_10" runat="server" />
                            <input type="hidden" id="HiddenSeikyuuSakiKbnOld_10" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </tbody>
    </table>
    <table frame="void" rules="none" style="text-align: left; width: 548px; border-bottom: solid 2px gray;
        border-left: solid 2px gray; border-right: solid 2px gray; border-top: solid 0px gray;
        table-layout: fixed;" id="Table1" class="mainTableNoneFrame" cellpadding="0"
        cellspacing="0">
        <!-- 3行目 -->
        <tr>
            <td style="border-bottom: solid 1px gray; width: 200px;" align="center" valign="top"
                class="yohaku">
                <input type="button" id="BtnSeikyuusyoDataSakuseiCall" runat="server" value="請求書データ作成"
                    style="font-size: 12px; width: 200px; color: black; height: 30px;" onclick="executeConfirm(this);" />
                <input type="button" id="ButtonSeikyuusyoDataSakusei" runat="server" value="請求書データ作成"
                    style="display: none;" />
            </td>
            <td style="border-bottom: solid 1px gray;" class="yohaku">
                <div class="InfoText">
                    締日毎の、売上データの請求書発行日が<br />
                    指定した日付以前で、未作成分の<br />
                    請求書データを作成します。<br />
                    （全締日をチェックした場合は、全請求先を<br />
                    &emsp;対象とします。）
                </div>
            </td>
        </tr>
        <!-- 4行目 -->
        <tr>
            <td align="center" valign="top" class="yohaku">
                <input type="button" id="ButtonSyuturyokuSeikyuusyo" runat="server" value="請求書一覧"
                    style="font-size: 12px; width: 200px; color: black; height: 30px;" />
            </td>
            <td class="yohaku">
                <div class="InfoText">
                    上のボタンで作成した<br />
                    請求書データを一覧表示します。<br />
                </div>
            </td>
        </tr>
    </table>
    <br />
    <!-- ●請求書発行 -->
    <!-- 画面上部・ヘッダ -->
    <div style="font-size: 12pt; width: 544px; height: 20px; text-align: center; background-color: lemonchiffon;
        padding-top: 6px; border: 2px solid gray; border-bottom: 0px; font-weight: bold;">
        請求書再発行
    </div>
    <table style="text-align: left; width: 548px; border-bottom: solid 2px gray; border-left: solid 2px gray;
        border-right: solid 2px gray; border-top: solid 2px gray;" id="bottomTable" class="mainTableNoneFrame"
        cellpadding="1" cellspacing="1">
        <!-- 1行目 -->
        <tr>
            <td align="center" valign="top" class="yohaku" style="width: 200px;">
                <input type="button" id="ButtonKakoSeikyuusyo" runat="server" value="過去請求書一覧" style="font-size: 12px;
                    width: 197px; color: black; height: 30px;" />
            </td>
            <td class="yohaku">
                <div class="InfoText">
                    過去に請求書を発行した<br />
                    請求書データを一覧表示します。<br />
                </div>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HiddenMovePageType" runat="server" />
    <%-- 日付取得ボタン押下時_請求（非表示） --%>
    <asp:HiddenField ID="HiddenPushValuesSeikyuu" runat="server" />
</asp:Content>

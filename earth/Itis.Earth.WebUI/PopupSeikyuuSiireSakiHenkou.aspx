<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopupSeikyuuSiireSakiHenkou.aspx.vb"
    Inherits="Itis.Earth.WebUI.PopupSeikyuuSiireSakiHenkou" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
    <meta http-equiv="Expires" content="-1" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>EARTH 請求先・仕入先変更</title>
</head>

<script type="text/javascript" src="js/jhsearth.js">       
</script>

<script type="text/javascript">
    var viewMode;
    /****************************************
     * onload時の追加処理
     ****************************************/
    function funcAfterOnload() {
        // 請求先・仕入先デフォルト相違チェック処理
        funcChkDefault();
        
        if(viewMode=='1'){
            funcViewMode();
        }
    }

    // 請求先・仕入先デフォルト相違チェック処理
    function funcChkDefault(){
        if (objEBI('<%= Me.HiddenChkSeikyuuSaki.ClientID %>').value != ""){
            objEBI('<%= Me.tdSeikyuuSaki.ClientID %>').style.background  = '#D4FAD5';
            objEBI('<%= Me.tdSeikyuuSakiTori.ClientID %>').style.background  = '#D4FAD5';
        }else{
            objEBI('<%= Me.tdSeikyuuSaki.ClientID %>').style.background  = '#E6E6E6';
            objEBI('<%= Me.tdSeikyuuSakiTori.ClientID %>').style.background  = '#E6E6E6';
        }
        if (objEBI('<%= Me.HiddenChkSiireSaki.ClientID %>').value != ""){
            objEBI('<%= Me.tdSiireSaki.ClientID %>').style.background  = '#D4FAD5';
        }else{
            objEBI('<%= Me.tdSiireSaki.ClientID %>').style.background  = '#E6E6E6';
        }
    }
    
    // 基本請求先をセット
    function setDefaultSeikyuuSaki(){
        var strSeikyuuSakiKbn = objEBI('<%= Me.HiddenSeikyuuSakiKbn.ClientID %>').value;
        var strSeikyuuSakiCd = objEBI('<%= Me.HiddenSeikyuuSakiCd.ClientID %>').value;
        var strSeikyuuSakiBrc = objEBI('<%= Me.HiddenSeikyuuSakiBrc.ClientID %>').value;
        
        objEBI('<%= Me.SelectSeikyuuSaki.ClientID %>').value = strSeikyuuSakiKbn;
        objEBI('<%= Me.TextSeikyuuSakiCd.ClientID %>').value = strSeikyuuSakiCd;
        objEBI('<%= Me.TextSeikyuuSakiBrc.ClientID %>').value = strSeikyuuSakiBrc;
        objEBI('<%= Me.TextSeikyuuSakiMei.ClientID %>').value = objEBI('<%= Me.TextDefaultSeikyuuSakiMei.ClientID %>').value;
        objEBI('<%= Me.HiddenSeikyuuSakiCdOld.ClientID %>').value = strSeikyuuSakiKbn + "<%= EarthConst.SEP_STRING %>" 
                                                                    + strSeikyuuSakiCd + "<%= EarthConst.SEP_STRING %>"
                                                                    + strSeikyuuSakiBrc;
        if (strSeikyuuSakiKbn == ""  && strSeikyuuSakiCd == "" && strSeikyuuSakiBrc == ""){
            objEBI('<%= Me.HiddenSeikyuuSakiCdOld.ClientID %>').value = "";
        }
        objEBI('<%= Me.tdSeikyuuSaki.ClientID %>').style.background  = '#E6E6E6';
        objEBI('<%= Me.tdSeikyuuSakiTori.ClientID %>').style.background  = '#E6E6E6';
    }
    // 基本仕入先をセット
    function setDefaultSiireSaki(){
        var strSiireSakiKbn = objEBI('<%= Me.HiddenKaisyaCd.ClientID %>').value;
        var strSiireSakiCd = objEBI('<%= Me.HiddenJigyousyoCd.ClientID %>').value;
        objEBI('<%= Me.TextSiireSakiCd.ClientID %>').value = strSiireSakiKbn;
        objEBI('<%= Me.TextSiireSakiBrc.ClientID %>').value = strSiireSakiCd;
        objEBI('<%= Me.TextSiireSakiMei.ClientID %>').value = objEBI('<%= Me.TextDefaultSiireSakiMei.ClientID %>').value;
        objEBI('<%= Me.HiddenSiireSakiCdOld.ClientID %>').value = strSiireSakiKbn + "<%= EarthConst.SEP_STRING %>"
                                                                  + strSiireSakiCd;
        if (strSiireSakiKbn == ""  && strSiireSakiCd == "" ){
            objEBI('<%= Me.HiddenSiireSakiCdOld.ClientID %>').value = "";
        }
        objEBI('<%= Me.tdSiireSaki.ClientID %>').style.background  = '#E6E6E6';
    }

    // 請求先・仕入先変更ボタン押下処理
    function funcReturn(){
        var arrReturn = new Array();
        arrReturn["SeikyuuSakiKbn"] = objEBI('<%= Me.SelectSeikyuuSaki.ClientID %>').value
        arrReturn["SeikyuuSakiCd"] = objEBI('<%= Me.TextSeikyuuSakiCd.ClientID %>').value
        arrReturn["SeikyuuSakiBrc"] = objEBI('<%= Me.TextSeikyuuSakiBrc.ClientID %>').value
        arrReturn["SiireSakiCd"] = objEBI('<%= Me.TextSiireSakiCd.ClientID %>').value
        arrReturn["SiireSakiBrc"] = objEBI('<%= Me.TextSiireSakiBrc.ClientID %>').value
        
        var chkSeikyuuSaki = arrReturn["SeikyuuSakiKbn"] + "<%= EarthConst.SEP_STRING %>"
                            +  arrReturn["SeikyuuSakiCd"] + "<%= EarthConst.SEP_STRING %>"
                            +  arrReturn["SeikyuuSakiBrc"]
        if (arrReturn["SeikyuuSakiKbn"] == "" && arrReturn["SeikyuuSakiCd"] == "" & arrReturn["SeikyuuSakiBrc"]==""){  
            chkSeikyuuSaki = ""
        }
        
        var chkSiireSaki = arrReturn["SiireSakiCd"] + "<%= EarthConst.SEP_STRING %>"
                        +  arrReturn["SiireSakiBrc"]                            
        if (arrReturn["SiireSakiCd"] == "" & arrReturn["SiireSakiBrc"]==""){  
            chkSiireSaki = ""
        }
        
        var blnErr = false;
        var strErr = "";
        if (chkSeikyuuSaki != objEBI('<%= Me.HiddenSeikyuuSakiCdOld.ClientID %>').value){
            strErr = strErr + '<%= Messages.MSG030E.replace("@PARAM1","請求先") %>' + "\r\n"
            blnErr = true;
        }
        
        if (chkSiireSaki != objEBI('<%= Me.HiddenSiireSakiCdOld.ClientID %>').value){
            strErr = strErr + '<%= Messages.MSG030E.replace("@PARAM1","仕入先") %>' + "\r\n"
            blnErr = true;
        }
        
        if (blnErr){
            alert(strErr);
            return false;
        }
        
        window.returnValue = arrReturn;
        
        alert('<%= Messages.MSG148W %>');
        window.close();
    }
    
    // キャンセルボタン押下処理
    function funcCancel(){
        window.returnValue = undefined;
        window.close();
    }

    function funcViewMode(){
        document.getElementById("ButtonSetDefaultSeikyuuSaki").disabled = true;
        document.getElementById("ButtonSetDefaultSiireSaki").disabled = true;
    }
    
    /*********************
     * 請求先情報クリア
     *********************/
    function clrSeikyuuInfo(){
        //値のクリア
        objEBI("<%= TextSeikyuuSakiMei.clientID %>").value = "";
        objEBI("<%= TextTorikesiRiyuuTouroku.clientID %>").value = "";
        //色をデフォルトへ
        objEBI("<%= TextSeikyuuSakiCd.clientID %>").style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>"
        objEBI("<%= TextSeikyuuSakiBrc.clientID %>").style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>"
        objEBI("<%= SelectSeikyuuSaki.clientID %>").style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>"
    }
        
</script>

<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="AjaxScriptManager" runat="server">
        </asp:ScriptManager>

        <script type="text/javascript">
            // Ajax処理完了後処理
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
                function EndRequestHandler(sender, args){
                    // 請求先・仕入先デフォルト相違チェック処理
                    funcChkDefault();
                }
        </script>

        <asp:HiddenField ID="HiddenSeikyuuSakiKbn" runat="server" />
        <asp:HiddenField ID="HiddenSeikyuuSakiCd" runat="server" />
        <asp:HiddenField ID="HiddenSeikyuuSakiBrc" runat="server" />
        <asp:HiddenField ID="HiddenKaisyaCd" runat="server" />
        <asp:HiddenField ID="HiddenJigyousyoCd" runat="server" />
        <asp:HiddenField ID="HiddenKameitenCd" runat="server" />
        <asp:HiddenField ID="HiddenSyouhinCd" runat="server" />
        <asp:HiddenField ID="HiddenKojKaisyaSeikyuu" runat="server" />
        <asp:HiddenField ID="HiddenKojKaisyaCd" runat="server" />
        <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
            class="titleTable">
            <tr>
                <th style="text-align: left; width: 200px;">
                    請求先・仕入先変更</th>
            </tr>
        </table>
        <br />
        <div style="padding-left: 10px">
            <b>【基本情報】</b>
            <table class="mainTable" style="width: 750px; table-layout: fixed; text-align: center;"
                id="tblDefaultSeikyuuSaki" cellpadding="1">
                <tr>
                    <td class="koumokuMei" style="width: 110px;">
                        基本請求先
                    </td>
                    <td style="width: 90px; height: 30px;">
                        <asp:TextBox ID="TextDefaultSeikyuuSakiCd" runat="server" Style="width: 80px;" CssClass="readOnlyStyle2 textCenter"
                            ReadOnly="true" TabIndex="-1"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="TextDefaultSeikyuuSakiMei" runat="server" Style="width: 360px;"
                            CssClass="readOnlyStyle2" ReadOnly="true" TabIndex="-1"></asp:TextBox>
                    </td>
                    <td class="koumokuMei" style="width: 40px;">
                        取消</td>
                    <td style="width: 110px;">
                        <asp:TextBox ID="TextTorikesiRiyuuKihon" runat="server" TabIndex="-1" Width="100px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="koumokuMei" style="width: 110px;">
                        基本仕入先
                    </td>
                    <td style="width: 90px; height: 30px;">
                        <asp:TextBox ID="TextDefaultSiireSakiCd" runat="server" Style="width: 80px;" CssClass="readOnlyStyle2 textCenter"
                            ReadOnly="true" TabIndex="-1"></asp:TextBox>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="TextDefaultSiireSakiMei" runat="server" Style="width: 520px;" CssClass="readOnlyStyle2"
                            ReadOnly="true" TabIndex="-1"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            <div style="height: 26px;">
                <input id="ButtonSetDefaultSeikyuuSaki" type="button" value="基本請求先をセット" class="button_copy"
                    style="height: 22px;" onclick="setDefaultSeikyuuSaki()" />
                <input id="ButtonSetDefaultSiireSaki" type="button" value="基本仕入先をセット" class="button_copy"
                    style="height: 22px;" onclick="setDefaultSiireSaki()" />
            </div>
            <table class="mainTable" style="width: 750px; table-layout: fixed; text-align: center;"
                id="Table1" cellpadding="1">
                <tr>
                    <td class="koumokuMei" style="width: 110px;">
                        登録請求先
                    </td>
                    <td style="height: 30px;" id="tdSeikyuuSaki" runat="server">
                        <asp:UpdatePanel ID="UpdatePanelSeikyuuSaki" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:DropDownList ID="SelectSeikyuuSaki" runat="server" Style="width: 80px;">
                                </asp:DropDownList>
                                <span id="SpanSeikyuuSaki" runat="server"></span>
                                <asp:TextBox ID="TextSeikyuuSakiCd" runat="server" Style="width: 40px;" MaxLength="5"
                                    CssClass="codeNumber"></asp:TextBox>
                                －
                                <asp:TextBox ID="TextSeikyuuSakiBrc" runat="server" Style="width: 15px;" MaxLength="2"
                                    CssClass="codeNumber"></asp:TextBox>
                                <input type="button" id="ButtonSeikyuuSaki" runat="server" value="検索" onserverclick="ButtonSeikyuuSaki_Click" />
                                <asp:TextBox ID="TextSeikyuuSakiMei" runat="server" Style="width: 230px;" CssClass="readOnlyStyle"
                                    ReadOnly="true" TabIndex="-1"></asp:TextBox>
                                <asp:HiddenField ID="HiddenSeikyuuSakiCdOld" runat="server" />
                                <asp:HiddenField ID="HiddenSeikyuuSakiCall" runat="server" />
                                <asp:HiddenField ID="HiddenChkSeikyuuSaki" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="koumokuMei" style="width: 40px;">
                        取消</td>
                    <td style="width: 110px;" id="tdSeikyuuSakiTori" runat="server">
                        <asp:UpdatePanel ID="UpdatePanel_SeikyuusakiToikesiTouroku" UpdateMode="Conditional"
                            runat="server" RenderMode="Inline">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ButtonSeikyuuSaki" />
                                <asp:AsyncPostBackTrigger ControlID="SelectSeikyuuSaki" />
                                <asp:AsyncPostBackTrigger ControlID="TextSeikyuuSakiCd" />
                                <asp:AsyncPostBackTrigger ControlID="TextSeikyuuSakiBrc" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:TextBox ID="TextTorikesiRiyuuTouroku" runat="server" TabIndex="-1" Width="100px"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="koumokuMei" style="width: 110px;">
                        登録仕入先
                    </td>
                    <td style="height: 30px; width: 620px;" id="tdSiireSaki" runat="server" colspan="3">
                        <asp:UpdatePanel ID="UpdatePanelSiireSaki" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:TextBox ID="TextSiireSakiCd" runat="server" Style="width: 40px;" MaxLength="5"
                                    CssClass="codeNumber"></asp:TextBox>
                                －
                                <asp:TextBox ID="TextSiireSakiBrc" runat="server" Style="width: 15px;" MaxLength="2"
                                    CssClass="codeNumber"></asp:TextBox>
                                <input type="button" id="ButtonSiireSaki" runat="server" value="検索" onserverclick="ButtonSiireSaki_Click" />
                                <asp:TextBox ID="TextSiireSakiMei" runat="server" Style="width: 470px;" CssClass="readOnlyStyle"
                                    ReadOnly="true" TabIndex="-1"></asp:TextBox>
                                <asp:HiddenField ID="HiddenSiireSakiCdNew" runat="server" />
                                <asp:HiddenField ID="HiddenSiireSakiCdOld" runat="server" />
                                <asp:HiddenField ID="HiddenKakushuNG" runat="server" />
                                <asp:HiddenField ID="HiddenTysKensakuType" runat="server" />
                                <asp:HiddenField ID="HiddenSiireSakiCall" runat="server" />
                                <asp:HiddenField ID="HiddenChkSiireSaki" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            <br />
            <input id="ButtonSubmit" runat="server" type="button" value="請求先・仕入先変更" />
            <input id="ButtonCancel" runat="server" type="button" value="キャンセル" />
        </div>
    </form>
    <!-- 検索画面遷移用フォーム -->
    <form id="searchForm" method="post" action="">
        <!-- 検索条件値格納用 -->
        <input type="hidden" id="sendSearchTerms" name="sendSearchTerms" />
        <!-- 検索結果セット先ID格納用 -->
        <input type="hidden" id="returnTargetIds" name="returnTargetIds" />
        <!-- 結果セット後実行ボタンID格納用 -->
        <input type="hidden" id="afterEventBtnId" name="afterEventBtnId" />
    </form>
</body>
</html>

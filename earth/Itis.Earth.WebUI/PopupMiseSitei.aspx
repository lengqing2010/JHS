<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopupMiseSitei.aspx.vb"
    Inherits="Itis.Earth.WebUI.PopupMiseSitei" %>
<%@ Import Namespace="Itis.Earth.Utilities" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
    <meta http-equiv="Expires" content="-1" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>店指定</title>
</head>

<script type="text/javascript" src="js/jhsearth.js">
</script>

<script type="text/javascript">
    _d = document;
    
    function funcAfterOnload(){

        init();

        //画面遷移処理呼び出し
        if(objEBI("<%=HiddenOkFlg.clientID %>").value == "<%=Boolean.TrueString %>"){
            //遷移OKフラグがtrueの場合
            funcMove();
        }

    }

    //画面遷移処理
    function funcMove(){
        //<!-- 画面引渡し情報 -->
        var arrArg = window.dialogArguments;
        var objSendForm = objEBI("openPageForm");
        var objSendVal_kbn = objEBI("kbn");
        var objSendVal_tenmd = objEBI("tenmd");
        var objSendVal_isfc = objEBI("isfc");
        var objSendVal_kameicd = objEBI("kameicd");

        objSendForm.action = arrArg["url"];
        
        switch (arrArg["type"]) {
        case "tenbetu":     // 「店別」画面
            break;
        case "tenbetuA":    // 「店別」画面
            break;
        case "hansoku":     // 「販促」画面
            break;
        case "hansokuA":    // 「販促」画面
            break;
        default:
            break;
        }

        objSendVal_tenmd.value = arrArg["tenmd"]        
        if(objEBI("<%=RadioKameitenSitei.clientID %>").checked){
            objSendVal_isfc.value = 0;
            objSendVal_kameicd.value = objEBI("<%=TextKameitenCd.clientID %>").value;
        }else{
            objSendVal_isfc.value = 1;
            objSendVal_kameicd.value = objEBI("<%=TextEigyousyoCd.clientID %>").value;
        }
        objSendVal_kbn.value = objEBI("<%=SelectKubun.clientID %>").value;
        objSendForm.target=arrArg["window"];
        objSendForm.submit();
        window.close();
    }
    
    //ボタン押下時の処理
    function buttonAction(objBtn){
        var kameitenSitei = objEBI("<%=RadioKameitenSitei.clientID %>").checked;
        var eigyousyoSitei = objEBI("<%=RadioEigyousyoSitei.clientID %>").checked;
        var kameitenCd = objEBI("<%=TextKameitenCd.clientID %>").value.Trim();
        var eigyousyoCd = objEBI("<%=TextEigyousyoCd.clientID %>").value.Trim();
        var kameitenCdOld = objEBI("<%=HiddenKameitenCdOld.clientID %>").value.Trim();
        var eigyousyoCdOld = objEBI("<%=HiddenEigyousyoCdOld.clientID %>").value.Trim();

        //ボタン別チェック
        if(objBtn.id == "<%=ButtonClose.clientID %>"){
            //閉じるボタン
            window.returnValue="null";
            window.close();
        }else if(objBtn.id == "<%=ButtonNext.clientID %>"){
            //遷移実行ボタン
            //入力チェック
            if((!kameitenSitei && !eigyousyoSitei) || (kameitenSitei && eigyousyoSitei)){
                alert("加盟店、営業所のどちらかを選択してください。");
                objEBI("<%=RadioKameitenSitei.clientID %>").focus();
                return false;
            }
            if(kameitenSitei && kameitenCd==""){
                alert("加盟店コードは必須入力です。");
                objEBI("<%=TextKameitenCd.clientID %>").focus();
                return false;
            }else if(kameitenCd != kameitenCdOld){
                alert("加盟店コードが変更されております。検索ボタンを押してください。");
                objEBI("<%=ButtonKameitenKennsaku.clientID %>").focus();
                return false;
            }
            if(eigyousyoSitei && eigyousyoCd==""){
                alert("営業所コードは必須入力です。");
                objEBI("<%=TextEigyousyoCd.clientID %>").focus();
                return false;
            }else if(eigyousyoCd != eigyousyoCdOld){
                alert("営業所コードが変更されております。検索ボタンを押してください。");
                objEBI("<%=ButtonEigyousyoSearch.clientID %>").focus();
                return false;
            }
        
            //登録料・販促品修正へボタン
            funcMove();
        }
    }
    
    //画面をxボタンで閉じた場合の処理
    window.onunload=funcUnload;
    function funcUnload(){

    }

	function init(){

	}
	
	function clrName(obj,target,flg){
        if(obj.value==""){
            flg.value="1";
            target.click();
        }else{
            flg.value="";
        }
	}
	
    /*********************
     * 加盟店情報クリア
     *********************/
    function clrKameitenInfo(obj,target,flg){
        if(obj.value == ""){
            //値のクリア
            objEBI("<%= TextKameitenCd.clientID %>").value = "";
            objEBI("<%= TextKameitenMei.clientID %>").value = "";
            objEBI("<%= TextTorikesiRiyuu.clientID %>").value = "";
            objEBI("<%= TextKeiretu.clientID %>").value = "";
            objEBI("<%= TextKameiEigyousyoMei.clientID %>").value = "";
            //色をデフォルトへ
            objEBI("<%= TextKameitenCd.clientID %>").style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>";
            objEBI("<%= TextKameitenMei.clientID %>").style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>";
            objEBI("<%= TextTorikesiRiyuu.clientID %>").style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>";
            objEBI("<%= TextKeiretu.clientID %>").style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>";
            objEBI("<%= TextKameiEigyousyoMei.clientID %>").style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>";
            
            flg.value="1";
            target.click();
            
        }else{
            flg.value="";
        }
    }
        
</script>

<body>
    <div style="text-align: center">
        <form id="form1" runat="server">
            <asp:ScriptManager ID="AjaxScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <input id="HiddenOkFlg" runat="server" type="hidden" />
                    <input id="HiddenKidouType" runat="server" type="hidden" />
                    <input id="HiddenClearFlg" runat="server" type="hidden" />
                    <br />
                    <span id="SpanPopupMess1" runat="server"></span>
                    <br />
                    <span id="SpanPopupMess2" runat="server"></span>
                    <br />
                    <br />
                    <table style="text-align: left;" class="subTable">
                        <tr>
                            <td colspan="3">
                                <input type="radio" name="radioIfFc" id="RadioKameitenSitei" value="0" runat="server" />
                                加盟店指定
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30px">
                                &nbsp;
                            </td>
                            <td>
                                区分
                            </td>
                            <td>
                                <asp:DropDownList ID="SelectKubun" Style="width: 180px;" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30px">
                                &nbsp;
                            </td>
                            <td>
                                加盟店コード</td>
                            <td>
                                <asp:TextBox ID="TextKameitenCd" Style="width: 50px" CssClass="codeNumber" MaxLength="5"
                                    runat="server" /><input type="hidden" id="HiddenKameitenCdOld" runat="server" />
                                <input type="button" value="検索" id="ButtonKameitenKennsaku" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30px">
                                &nbsp;</td>
                            <td>
                                加盟店名</td>
                            <td>
                                <asp:TextBox ID="TextKameitenMei" Style="width: 200px" CssClass="readOnlyStyle" ReadOnly="true"
                                    TabIndex="-1" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30px;">
                                &nbsp;</td>
                            <td>
                                取消</td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel_kameiToikesi" UpdateMode="Conditional" runat="server"
                                    RenderMode="Inline">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ButtonKameitenKennsaku" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:TextBox ID="TextTorikesiRiyuu" runat="server" style="width: 200px;" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30px">
                                &nbsp;</td>
                            <td>
                                系列</td>
                            <td>
                                <asp:TextBox ID="TextKeiretu" Style="width: 200px" CssClass="readOnlyStyle" ReadOnly="true"
                                    TabIndex="-1" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30px">
                                &nbsp;</td>
                            <td>
                                営業所名</td>
                            <td>
                                <asp:TextBox ID="TextKameiEigyousyoMei" Style="width: 200px" CssClass="readOnlyStyle"
                                    TabIndex="-1" ReadOnly="true" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <input type="radio" name="radioIfFc" id="RadioEigyousyoSitei" value="1" runat="server" />
                                営業所指定
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30px">
                                &nbsp;</td>
                            <td>
                                営業所コード</td>
                            <td>
                                <asp:TextBox ID="TextEigyousyoCd" Style="width: 50px" CssClass="codeNumber" MaxLength="5"
                                    runat="server" /><input type="hidden" id="HiddenEigyousyoCdOld" runat="server" />
                                <input type="button" value="検索" id="ButtonEigyousyoSearch" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30px">
                                &nbsp;</td>
                            <td>
                                営業所名</td>
                            <td>
                                <asp:TextBox Style="width: 200px" CssClass="readOnlyStyle" ReadOnly="true" ID="TextEigyousyoMei"
                                    TabIndex="-1" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <input type="button" id="ButtonChangeSitei" runat="server" value="指定ラジオ選択変更" style="display:none" onserverclick="ButtonChangeSitei_ServerClick" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <table style="text-align: center">
                <tr>
                    <td>
                        <input type="button" value="登録料・販促品修正へ" style="width: 140px" id="ButtonNext" runat="server"
                            onclick="buttonAction(this);" />
                    </td>
                    <td>
                        <input type="button" value="閉じる" style="width: 80px" onclick="buttonAction(this);"
                            id="ButtonClose" runat="server" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
    <!-- 検索画面遷移用フォーム -->
    <form id="searchForm" method="post" action="">
        <!-- 検索条件値格納用 -->
        <input type="hidden" id="sendSearchTerms" name="sendSearchTerms" />
        <!-- 検索結果セット先ID格納用 -->
        <input type="hidden" id="returnTargetIds" name="returnTargetIds" />
        <!-- 結果セット後実行ボタンID格納用 -->
        <input type="hidden" id="afterEventBtnId" name="afterEventBtnId" />
    </form>
    <!-- 画面遷移用フォーム -->
    <form id="openPageForm" method="post" action="">
        <!-- 画面引渡し情報 -->
        <input type="hidden" id="kbn" name="kbn" />
        <input type="hidden" id="kameicd" name="kameicd" />
        <input type="hidden" id="tenmd" name="tenmd" />
        <input type="hidden" id="isfc" name="isfc" />
    </form>
</body>
</html>

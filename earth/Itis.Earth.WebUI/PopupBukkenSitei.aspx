<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopupBukkenSitei.aspx.vb"
    Inherits="Itis.Earth.WebUI.PopupBukkenSitei" %>
<%@ Import Namespace="Itis.Earth.Utilities" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <meta http-equiv="Expires" content="-1" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>EARTH 物件指定</title>
</head>

<script type="text/javascript" src="js/jhsearth.js">
</script>

<script type="text/javascript">
    function funcAfterOnload(){
        
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
        var objSendVal_st = objEBI("st");
        var objSendVal_Kubun = objEBI("sendPage_kubun");
        var objSendVal_HosyousyoNo = objEBI("sendPage_hosyoushoNo");
        var objSendVal_kbn = objEBI("kbn");
        var objSendVal_no = objEBI("no");

        //コピー情報初期化
        objEBI("copy").value = "";
        objEBI("motokubun").value = "";
        objEBI("motono").value = "";

        objSendForm.action = arrArg["url"];
        
        switch (arrArg["type"]) {
        case "irai":    // 「依頼」画面
            objSendVal_st.value = "<%=EarthConst.MODE_VIEW %>";
            break;
        case "teibetu": // 「邸別」画面
            break;
        case "teinyuu": // 「邸別入金」画面
            break;
        case "koujiA": // 「改良工事」画面
            //コピー処理を実装
            objEBI("copy").value = objEBI("<%=HiddenCopyFlg.clientID %>").value;
            objEBI("motokubun").value = objEBI("<%=HiddenOldKubun.clientID %>").value;
            objEBI("motono").value = objEBI("<%=HiddenOldNo.clientID %>").value;
            break;
        default:
            break;
        }
        
        objSendForm.target=arrArg["window"];
        objSendVal_Kubun.value=objEBI("<%=SelectKubun.clientID %>").value;
        objSendVal_HosyousyoNo.value=objEBI("<%=TextNo.clientID %>").value;
        objSendVal_kbn.value=objEBI("<%=SelectKubun.clientID %>").value;
        objSendVal_no.value=objEBI("<%=TextNo.clientID %>").value;
        objSendForm.submit();
        window.close();
    }
    
    //ボタン押下時の処理
    function buttonAction(objBtn){
        //コピーフラグをクリア
        objEBI("<%=HiddenCopyFlg.clientID %>").value = "";
        //ボタン別チェック
        if(objBtn.id == "<%=ButtonClose.clientID %>"){
            //閉じるボタン
            window.returnValue="null";
            window.close();
        }else if(objBtn.id == "<%=ButtonBukkenOpen.clientID %>"){
            //上記物件を開くボタン
            if(objEBI("<%=SelectKubun.clientID %>").value != "" && 
               objEBI("<%=TextNo.clientID %>").value != ""){
                //必須チェックOK
                if(objEBI("<%=SelectKubun.clientID %>").value == objEBI("<%=HiddenOldKubun.clientID %>").value && 
                   objEBI("<%=TextNo.clientID %>").value == objEBI("<%=HiddenOldNo.clientID %>").value){
                    //同じ物件の場合、確認メッセージを表示
                    if(!confirm("<%=Messages.MSG087C %>")){
                        //キャンセルの場合、区分にフォーカスを当てて戻る
                        objEBI("<%=SelectKubun.clientID %>").focus();
                        return false;
                    }else{
                        //必須チェックOKで、元画面と同じ物件でもOKの場合、実行ボタンをクリック
                        objEBI("<%=ButtonBukkenOpenExe.clientID %>").click();
                    }
                }else{
                    //必須チェックOKで、元画面と異なる物件の場合、実行ボタンをクリック
                    objEBI("<%=ButtonBukkenOpenExe.clientID %>").click();
                }
            }else{
                //必須チェックNGの場合、空項目にフォーカスセット
                alert("<%=Messages.MSG031E %>");
                if(objEBI("<%=SelectKubun.clientID %>").value == ""){
                    objEBI("<%=SelectKubun.clientID %>").focus();
                }else{
                    objEBI("<%=TextNo.clientID %>").focus();
                }
            }
        }else if(objBtn.id == "<%=ButtonCopyOpen.clientID %>"){
            if(objEBI("<%=SelectKubun.clientID %>").value == objEBI("<%=HiddenOldKubun.clientID %>").value && 
               objEBI("<%=TextNo.clientID %>").value == objEBI("<%=HiddenOldNo.clientID %>").value){
                //同じ物件の場合、エラーを表示し、区分にフォーカス
                alert("<%=Messages.MSG068E %>");
                objEBI("<%=SelectKubun.clientID %>").focus();
            }else{
                //チェックOKの場合、コピーチェック処理へ進む
                objEBI("<%=HiddenCopyFlg.clientID %>").value = 1;
                objEBI("<%=ButtonCopyOpenExe.clientID %>").click();
            }
        }
    }
    
    //画面をxボタンで閉じた場合の処理
    window.onunload=funcUnload;
    function funcUnload(){

    }

</script>

<body>
    <form id="form1" runat="server">
        <div style="text-align: center">
            <br />
            <span id="SpanPopupMess1" runat="server"></span><br />
            <span id="SpanPopupMess2" runat="server"></span>
            <br />
            <br />
            <table border="0" cellpadding="0" cellspacing="2" style="text-align: left">
                <tr>
                    <td>
                        区分
                    </td>
                    <td>
                        <asp:DropDownList ID="SelectKubun" runat="server" CssClass="hissu" Style="font-size: 15px;">
                        </asp:DropDownList><span id="SpanKubun" runat="server"></span>
                    </td>
                </tr>
                <tr>
                    <td>
                        番号
                    </td>
                    <td>
                        <input type="text" id="TextNo" runat="server" maxlength="10" style="width: 85px; font-size: 15px;" class="codeNumber hissu" />
                    </td>
                </tr>
            </table>
            <br />
            <table border="0" cellpadding="0" cellspacing="2">
                <tr>
                    <td>
                        <input type="button" id="ButtonBukkenOpen" runat="server" value="上記物件を開く" onclick="buttonAction(this);" />
                        <input type="button" id="ButtonBukkenOpenExe" runat="server" value="上記物件を開く_実行" />
                        <input type="button" id="ButtonClose" runat="server" value="閉じる" onclick="buttonAction(this);" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">
                        <input type="button" id="ButtonCopyOpen" runat="server" value="内容をコピーして開く" onclick="buttonAction(this);" />
                        <input type="button" id="ButtonCopyOpenExe" runat="server" value="内容をコピーして開く_実行"/>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="HiddenOkFlg" runat="server" />
            <asp:HiddenField ID="HiddenCopyFlg" runat="server" />
            <asp:HiddenField ID="HiddenKidouType" runat="server" />
            <asp:HiddenField ID="HiddenOldKubun" runat="server" />
            <asp:HiddenField ID="HiddenOldNo" runat="server" />
        </div>
    </form>
    <!-- 画面遷移用フォーム -->
    <form id="openPageForm" method="post" action="">
        <!-- 画面モード指定 -->
        <input type="hidden" id="st" name="st" />
        <!-- 画面引渡し情報 -->
        <input type="hidden" id="sendPage_kubun" name="sendPage_kubun" />
        <input type="hidden" id="sendPage_hosyoushoNo" name="sendPage_hosyoushoNo" />
        <input type="hidden" id="kbn" name="kbn" />
        <input type="hidden" id="no" name="no" />
        <input type="hidden" id="copy" name="copy" />
        <input type="hidden" id="motokubun" name="motokubun" />
        <input type="hidden" id="motono" name="motono" />
        <input type="hidden" id="sendPageHidden4" name="sendPageHidden4" />
        <input type="hidden" id="sendPageHidden5" name="sendPageHidden5" />
    </form>
</body>
</html>

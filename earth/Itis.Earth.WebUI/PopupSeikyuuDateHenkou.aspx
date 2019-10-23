<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopupSeikyuuDateHenkou.aspx.vb"
    Inherits="Itis.Earth.WebUI.PopupSeikyuuDateHenkou" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
    <meta http-equiv="Expires" content="-1" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>請求年月日変更</title>
</head>

<script type="text/javascript" src="js/jhsearth.js"> 
</script>

<script type="text/javascript">
    // 請求年月日確定処理処理
    function funcSubmit(){
        window.returnValue = true;
        window.close();
    }
    // キャンセルボタン押下処理
    function funcCancel(){
        window.returnValue = undefined;
        window.close();
    }
    
    // 請求年月日変更ボタン押下処理
    function funcChkInput(){
        if (objEBI("<%=TextSeikyuuDate.clientID %>").value == ""){
            alert("<%=Messages.MSG013E %>".replace("@PARAM1", "請求年月日"));
            return false;
        }else{
            objEBI("<%=ButtonSubmit.clientID %>").click();
        }
    }    
</script>

<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="AjaxScriptManager" runat="server">
        </asp:ScriptManager>
        <%--隠し項目--%>
        <asp:HiddenField ID="HiddenDenpyouUnqNo" runat="server" />
        <asp:HiddenField ID="HiddenRegUpdDate" runat="server" />
        <asp:HiddenField ID="HiddenDefaultSeikyuuDate" runat="server" />
        <%--画面タイトル--%>
        <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
            class="titleTable">
            <tr>
                <th style="text-align: left; width: 200px;">
                    請求先年月日変更</th>
            </tr>
        </table>
        <br />
        <%--画面項目--%>
        <div style="padding-left: 10px">
            <table class="mainTable" style="width: 200px; table-layout: fixed; text-align: center;"
                id="Table1" cellpadding="1">
                <tr>
                    <td class="koumokuMei" style="width: 100px;">
                        請求年月日
                    </td>
                    <td style="height: 30px; width: 100px" id="tdSeikyuuDate" runat="server">
                        <input id="TextSeikyuuDate" runat="server" maxlength="10" class="date" onblur="checkDate(this);" />
                    </td>
                </tr>
            </table>
            <br />
            <input id="ButtonSubMitDisp" runat="server" type="button" value="請求年月日変更" onclick="funcChkInput();" />
            <input id="ButtonSubmit" runat="server" type="button" value="請求年月日変更" style="display: none;" />
            <input id="ButtonCancel" type="button" value="キャンセル" onclick="funcCancel();" />
            <asp:HiddenField ID="HiddenDenNo" runat="server" />
            <asp:HiddenField ID="HiddenDenUriDate" runat="server" />
        </div>
    </form>
</body>
</html>

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopupGamenKidou.aspx.vb"
    Inherits="Itis.Earth.WebUI.PopupGamenKidou" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
    <meta http-equiv="Expires" content="-1" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>EARTH 画面起動</title>
</head>

<script type="text/javascript" src="js/jhsearth.js">
</script>

<script type="text/javascript">
    //Sys.WebForms.PageRequestManager
    var ajaxPRM = null;
    //Ajax処理中かを判別
    var isAjaxRunning = function(){
        if(ajaxPRM != null && ajaxPRM.get_isInAsyncPostBack()) return true;
        return false;
    }
    
    //body.onload後実行処理
    function funcAfterOnload(){
        try{
            window.resizeTo(480, 740);
        }catch(e){
            //アクセスが拒否されましたのエラーが出たら何もしない。
            if(e.number == 2147024891){
                throw e;
            }
        }
    }

    //画面起動処理
    function funcGamenKidou(strUrl,flgCloseWindow){
        if(isAjaxRunning()){
            //Ajax処理中は、待つ
            setTimeout(function(){funcGamenKidou(strUrl,flgCloseWindow)},100);
        }else{
            objOpenPageForm = objEBI("openPageForm");
            objSendPageKubun = objEBI("sendPage_kubun");
            objSendPageHosyoushoNo = objEBI("sendPage_hosyoushoNo");
            objKbn = objEBI("kbn");
            objNo = objEBI("no");
            objSt = objEBI("st");
            
            objSelectKubun = objEBI("<%=SelectKubun.ClientID %>");
            objTextNo = objEBI("<%=TextNo.ClientID %>");
            
            //入力チェック1
            if(objSelectKubun.value == "" || objTextNo.value == ""){
                //必須チェックNGの場合、空項目にフォーカスセット
                alert("<%=Messages.MSG031E %>");
                if(objSelectKubun.value == ""){
                    objSelectKubun.focus();
                }else{
                    objTextNo.focus();
                }
                return false;
            }
            
            //入力チェック2
            if(checkProc() == false || 
                (objEBI("<%=HiddenIsJibanData.ClientID %>").value != objEBI("<%=HiddenIsJibanDataOld.ClientID %>").value)){
                alert("<%=Messages.MSG020E %>");
                objSelectKubun.focus();
                return false;
            }
            
            objSendPageKubun.value = objSelectKubun.value;
            objKbn.value = objSelectKubun.value;
            objSendPageHosyoushoNo.value = objTextNo.value;
            objNo.value = objTextNo.value;
            objSt.value = "<%=EarthConst.MODE_VIEW %>"
            
            objOpenPageForm.action = strUrl;
            objOpenPageForm.target = Math.random();
            objOpenPageForm.submit();

            if(flgCloseWindow == 1)window.close();
        }
    }

    //地盤データ存在チェック
    function checkJibanData(obj){
        var objSelectKubun = objEBI("<%=SelectKubun.ClientID %>");
        var objTextNo = objEBI("<%=TextNo.ClientID %>");
        var strNoHankaku = objTextNo.value.z2hDigit();
        if(objSelectKubun.value != "" && strNoHankaku.length >= 10){
            //全角数値が含まれる場合、半角数値に変換
            if(objTextNo.value != strNoHankaku){
                objTextNo.value = strNoHankaku;
                if(obj == null)obj = objSelectKubun;
                obj.focus();
            }
            if(!isNaN(strNoHankaku) && objEBI("<%=HiddenIsJibanDataOld.ClientID %>").value != (objSelectKubun.value + strNoHankaku)){
                objEBI("<%=HiddenIsJibanData.ClientID %>").value = "";
                objEBI("<%=HiddenIsJibanDataOld.ClientID %>").value = objSelectKubun.value + strNoHankaku;
                objEBI("<%=ButtonCheckIsJibanData.ClientID %>").click();
            }
        }
    }
    
    //入力チェック
    function checkProc(){
        var objSelectKubun = objEBI("<%=SelectKubun.ClientID %>");
        var objTextNo = objEBI("<%=TextNo.ClientID %>");
        var strNoHankaku = objTextNo.value.z2hDigit();

        if(objSelectKubun.value != "" && strNoHankaku.length >= 10){
            return true;
        }   
        return false;
    }

    //画面をxボタンで閉じた場合の処理
    window.onbeforeunload=funcUnload;
    function funcUnload(){
        //ウィンドウサイズを広げておく
        window.resizeTo(1010, 720);
    }

</script>

<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="AjaxScriptManager1" runat="server" AllowCustomErrorsRedirect="false" />

        <script type="text/javascript">
            ajaxPRM = Sys.WebForms.PageRequestManager.getInstance();
            ajaxPRM.add_initializeRequest(InitializeRequestHandler);
                function InitializeRequestHandler(sender, args){
                    window.status = "Checking.. Jiban Data";
                }
            ajaxPRM.add_endRequest(EndRequestHandler);
                function EndRequestHandler(sender, args){
                    window.status = "Check Finish " + objEBI("<%=HiddenIsJibanData.ClientID %>").value;
                }
        </script>

        <div style="text-align: center">
            <br />
            <span id="SpanPopupMess1" runat="server" style="text-align: center">起動する画面のボタンを押下してください。</span>
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
                        <input type="text" id="TextNo" runat="server" maxlength="10" style="width: 85px;
                            font-size: 15px;" class="codeNumber hissu" />
                    </td>
                </tr>
            </table>
            <input type="button" id="ButtonCheckIsJibanData" runat="server" value="地盤データ存在チェック実行"
                style="display: none;" />
            <asp:UpdatePanel ID="UpdatePanel_BukkenKidou" UpdateMode="Conditional" runat="server"
                RenderMode="Inline">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ButtonCheckIsJibanData" />
                </Triggers>
                <ContentTemplate>
                    <input type="hidden" id="HiddenIsJibanData" runat="server" />
                    <input type="hidden" id="HiddenIsJibanDataOld" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <input type="button" id="ButtonJutyuu" runat="server" class="buttonKidou" value="受注【確認】" />
            <a target="searchWindowB" id="AncJutyuuOW" runat="server">
                <img alt="別ウィンドウで開く" src="images/otherWin.gif" style="width: 24px; height: 24px" /></a>
            <br />
            <br />
            <input type="button" id="ButtonHoukokusyo" runat="server" class="buttonKidou" value="報告書" />
            <a target="searchWindowB" id="AncHoukokusyoOW" runat="server">
                <img alt="別ウィンドウで開く" src="images/otherWin.gif" style="width: 24px; height: 24px" /></a>
            <br />
            <br />
            <input type="button" id="ButtonKairyouKouji" runat="server" class="buttonKidou" value="改良工事" />
            <a target="searchWindowB" id="AncKairyouKoujiOW" runat="server">
                <img alt="別ウィンドウで開く" src="images/otherWin.gif" style="width: 24px; height: 24px" /></a>
            <br />
            <br />
            <input type="button" id="ButtonHosyou" runat="server" class="buttonKidou" value="保証" />
            <a target="searchWindowB" id="AncHosyouOW" runat="server">
                <img alt="別ウィンドウで開く" src="images/otherWin.gif" style="width: 24px; height: 24px" /></a>
            <br />
            <br />
            <input type="button" id="ButtonTeibetuSyuusei" runat="server" class="buttonKidou"
                value="邸別データ修正" />
            <a target="searchWindowB" id="AncTeibetuSyuuseiOW" runat="server">
                <img alt="別ウィンドウで開く" src="images/otherWin.gif" style="width: 24px; height: 24px" /></a>
            <br />
            <br />
            <input type="button" id="ButtonTeibetuNyuukinSyuusei" runat="server" class="buttonKidou"
                value="邸別入金修正" />
            <a target="searchWindowB" id="AncTeibetuNyuukinSyuuseiOW" runat="server">
                <img alt="別ウィンドウで開く" src="images/otherWin.gif" style="width: 24px; height: 24px" /></a>
            <br />
            <br />
            <input type="button" id="ButtonPopupBukkenRireki" runat="server" class="buttonKidou"
                value="物件履歴" />
            <a target="searchWindowB" id="AncButtonPopupBukkenRirekiOW" runat="server">
                <img alt="別ウィンドウで開く" src="images/otherWin.gif" style="width: 24px; height: 24px" /></a>
            <br />
            <br />
            <input type="button" id="ButtonPopupTokubetuTaiou" runat="server" class="buttonKidou"
                value="特別対応" />
            <a target="searchWindowB" id="AncButtonPopupTokubetuTaiouOW" runat="server">
                <img alt="別ウィンドウで開く" src="images/otherWin.gif" style="width: 24px; height: 24px" /></a>
            <br />
            <br />
            <input type="button" id="ButtonSearchMousikomi" runat="server" class="buttonKidou"
                value="申込情報" />
            <a target="searchWindowB" id="AncButtonSearchMousikomiOW" runat="server">
                <img alt="別ウィンドウで開く" src="images/otherWin.gif" style="width: 24px; height: 24px" /></a>
            <br />
            <br />
            <input type="button" id="ButtonSearchFcMousikomi" runat="server" class="buttonKidou"
                value="FC申込情報" />
            <a target="searchWindowB" id="AncButtonSearchFcMousikomiOW" runat="server">
                <img alt="別ウィンドウで開く" src="images/otherWin.gif" style="width: 24px; height: 24px" /></a>
            <br />
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

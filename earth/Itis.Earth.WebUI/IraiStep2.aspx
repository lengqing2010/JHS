<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="IraiStep2.aspx.vb" Inherits="Itis.Earth.WebUI.IraiStep2" Title="EARTH 受注 【Step2 依頼内容】" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<%@ Register Src="control/IraiCtrl1.ascx" TagName="IraiCtrl1" TagPrefix="uc1" %>
<%@ Register Src="control/IraiCtrl2.ascx" TagName="IraiCtrl2" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script type="text/javascript">        
        var flgAjaxRunning = false;
        var autoExeButtonId = null;
        
        function checkAjax(){
            if(objEBI("<%=HiddenAjaxFlg.ClientID %>").value=="1"){
                //Ajax処理中は画面遷移しない
                alert("<%= Messages.MSG104E %>");
                return false;
            }
            return true;
        } 

        //「確認へ」ボタン押下時の画面チェック処理
        function nextBeforeCheck(){
            if(!checkAjax()){
                return false;
            }
            //商品区分：リフォーム選択時の確認メッセージ表示処理
            var objSyouhinKbn3 = objEBI("<%= IraiCtrl2_1.AccItemKb_3.ClientID %>");
            if(objSyouhinKbn3.checked){
                if(!confirm("<%= Messages.MSG029C %>")){
                    return;
                }
            }
            objEBI("<%= irai2_next.clientID %>").click();
        }
    
        //onload後処理
        function funcAfterOnload(){
            //自動実行ボタン押下
            if(autoExeButtonId != null){
              objEBI(autoExeButtonId).click();
              return;
            }
        }

    Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(InitializeRequestHandler);
        function InitializeRequestHandler(sender, args){
            var mng = Sys.WebForms.PageRequestManager.getInstance();
            if (mng.get_isInAsyncPostBack()){
                //非同期ポストバックが既に実行中の場合、新たにリクエストされたポストバック処理をキャンセルする。
                if(args.get_postBackElement().id != "<%=IraiCtrl2_1.AccBtnSetSyouhin1.ClientID %>"){
                    //商品１設定処理以外
                    args.set_cancel(true);
                    return false;
                }
            }
            objEBI("<%=HiddenAjaxFlg.ClientID %>").value=1;
            flgAjaxRunning = true;
            var arrCtrl = new Array();
            arrCtrl[0] = objEBI("<%= IraiCtrl2_1.AccBtnIrainaiyouKakutei.clientID %>");
            arrCtrl[1] = objEBI("<%= IraiCtrl2_1.AccBtnIrainaiyouKaijo.clientID %>");
            arrCtrl[2] = objEBI("<%= IraiCtrl1_1.AccBtn_irai1.clientID %>");
            arrCtrl[3] = objEBI("<%= irai2_next_before.clientID %>");
            arrCtrl[4] = objEBI("<%= irai2_exeDirectTouroku.clientID %>");
            for(ci=0;ci<arrCtrl.length;ci++){
                if(arrCtrl[ci]!=null)arrCtrl[ci].disabled = true;
            }
        }
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args){
            var hdnHanei = objEBI("<%= IraiCtrl2_1.AccHiddenTokutaiKkkHaneiFlg.clientID %>");
            var arrCtrl = new Array();
            arrCtrl[0] = objEBI("<%= IraiCtrl2_1.AccBtnIrainaiyouKakutei.clientID %>");
            arrCtrl[1] = objEBI("<%= IraiCtrl2_1.AccBtnIrainaiyouKaijo.clientID %>");
            arrCtrl[2] = objEBI("<%= IraiCtrl1_1.AccBtn_irai1.clientID %>");
            arrCtrl[3] = objEBI("<%= irai2_next_before.clientID %>");
            arrCtrl[4] = objEBI("<%= irai2_exeDirectTouroku.clientID %>");
            for(ci=0;ci<arrCtrl.length;ci++){
                if(arrCtrl[ci]!=null){
                    if(ci == 4){
                        if(hdnHanei == "")arrCtrl[ci].disabled = false;
                    }else{
                        arrCtrl[ci].disabled = false;
                    }
                }
            }
            objEBI("<%=HiddenAjaxFlg.ClientID %>").value=0;
            flgAjaxRunning = false;
        }
    </script>

    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tbody>
            <tr>
                <th>
                    受注 【Step2 依頼内容】</th>
                <th style="text-align: right;">
                </th>
            </tr>
            <tr>
                <td colspan="2" rowspan="1" style="height: 10px">
                </td>
            </tr>
        </tbody>
    </table>
    <input type="hidden" id="gamenId" value="shinki" runat="server" />
    <input type="hidden" id="HiddenAjaxFlg" runat="server" />
    <uc1:IraiCtrl1 ID="IraiCtrl1_1" runat="server" />
    <br />
    <uc2:IraiCtrl2 ID="IraiCtrl2_1" runat="server" />
    <br />
    <table style="width: 100%;" cellpadding="8" cellspacing="0">
        <tr>
            <td class="tableFooter">
                <input id="irai2_next" type="button" value="【確認】へ実体" runat="server" style="width: 180px;
                    display: none;" />
                <input id="irai2_next_before" type="button" value="【確認】へ" runat="server" style="width: 180px;"
                    onclick="nextBeforeCheck();" />
                &nbsp; &nbsp;
                <asp:UpdatePanel ID="UpdatePanelStep2RegBtn" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <input id="irai2_exeDirectTouroku" type="button" value="登録 / 修正 実行" runat="server"
                            style="width: 180px; font-weight: bold; background-color: fuchsia;" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <input id="irai2_exeDirectTouroku_exe" type="button" value="登録/修正実行" runat="server"
                    style="display: none;" />
            </td>
        </tr>
    </table>
</asp:Content>

<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="IraiStep1.aspx.vb" Inherits="Itis.Earth.WebUI.IraiStep1" Title="EARTH �� �yStep1 ��{���z" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<%@ Register Src="control/IraiCtrl1.ascx" TagName="IraiCtrl1" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script type="text/javascript">
        var flgAjaxRunning = false;
        var autoExeButtonId = null;
        
        function checkAjax(){
            if(objEBI("<%=HiddenAjaxFlg.ClientID %>").value=="1"){
                //Ajax�������͉�ʑJ�ڂ��Ȃ�
                alert("<%= Messages.MSG104E %>");
                return false;
            }
            return true;
        } 
    
    Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(InitializeRequestHandler);
        function InitializeRequestHandler(sender, args){
            var mng = Sys.WebForms.PageRequestManager.getInstance();
            if (mng.get_isInAsyncPostBack()){
                //�񓯊��|�X�g�o�b�N�����Ɏ��s���̏ꍇ�A�V���Ƀ��N�G�X�g���ꂽ�|�X�g�o�b�N�������L�����Z������B
                if(args.get_postBackElement().id != "<%=IraiCtrl1_1.AccChkBunjou.ClientID %>"){
                    //���i�P�ݒ菈���ȊO
                    args.set_cancel(true);
                    return false;
                }
            }
            objEBI("<%=HiddenAjaxFlg.ClientID %>").value=1;
            flgAjaxRunning = true;
            var arrCtrl = new Array();
            arrCtrl[0] = objEBI("<%= irai1_next.clientID %>");
            arrCtrl[1] = objEBI("<%= irai1_kakunin.clientID %>");
            arrCtrl[2] = objEBI("<%= irai1_exeDirectTouroku.clientID %>");
            arrCtrl[3] = objEBI("<%= irai1_exeDirectTouroku_exe.clientID %>");
            for(ci=0;ci<arrCtrl.length;ci++){
                if(arrCtrl[ci]!=null)arrCtrl[ci].disabled = true;
            }
        }
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args){
            var arrCtrl = new Array();
            arrCtrl[0] = objEBI("<%= irai1_next.clientID %>");
            arrCtrl[1] = objEBI("<%= irai1_kakunin.clientID %>");
            arrCtrl[2] = objEBI("<%= irai1_exeDirectTouroku.clientID %>");
            arrCtrl[3] = objEBI("<%= irai1_exeDirectTouroku_exe.clientID %>");
            for(ci=0;ci<arrCtrl.length;ci++){
                if(arrCtrl[ci]!=null)arrCtrl[ci].disabled = false
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
                    �� �yStep1 ��{���z</th>
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
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td style="height: 10px">
            </td>
        </tr>
    </table>
    <table style="width: 100%;" cellpadding="8" cellspacing="0">
        <tr>
            <td class="tableFooter">
                <input id="irai1_next" type="button" value="�yStep2 �˗����z��" runat="server" style="width: 180px" />
                &nbsp; &nbsp;
                <input id="irai1_kakunin" type="button" value="�y�m�F�z��" runat="server" style="width: 180px" />
                &nbsp; &nbsp;
                <input id="irai1_exeDirectTouroku" type="button" value="�o�^ / �C�� ���s" runat="server"
                    style="width: 180px; font-weight: bold; background-color: fuchsia;" />
                <input id="irai1_exeDirectTouroku_exe" type="button" value="�o�^/�C�����s" runat="server"
                    style="display: none;" />
            </td>
        </tr>
    </table>
</asp:Content>

<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="TeibetuSyuusei.aspx.vb" Inherits="Itis.Earth.WebUI.TeibetuSyuusei"
    Title="EARTH �@�ʃf�[�^�C��" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<%@ Register Src="control/TeibetuSyouhinRecordCtrl.ascx" TagName="TeibetuSyouhinRecordCtrl"
    TagPrefix="uc1" %>
<%@ Register Src="control/TeibetuKoujiRecordCtrl.ascx" TagName="TeibetuKoujiRecordCtrl"
    TagPrefix="uc2" %>
<%@ Register Src="control/TeibetuKyoutuuCtrl.ascx" TagName="TeibetuKyoutuuCtrl" TagPrefix="uc3" %>
<%@ Register Src="control/TeibetuIraiNaiyouCtrl.ascx" TagName="TeibetuIraiNaiyouCtrl"
    TagPrefix="uc4" %>
<%@ Register Src="control/TeibetuSyouhinHeaderCtrl.ascx" TagName="TeibetuSyouhinHeaderCtrl"
    TagPrefix="uc5" %>
<%@ Register Src="control/NyuukinZangakuCtrl.ascx" TagName="NyuukinZangakuCtrl" TagPrefix="uc6" %>
<%@ Register Src="control/TeibetuSyouhin2Ctrl.ascx" TagName="TeibetuSyouhin2Ctrl"
    TagPrefix="uc7" %>
<%@ Register Src="control/TeibetuSyouhin3Ctrl.ascx" TagName="TeibetuSyouhin3Ctrl"
    TagPrefix="uc8" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script type="text/javascript">
        //�������s�{�^��ID
        var autoExeButtonId = null;
                
        //onload�㏈��
        function funcAfterOnload(){
            //�o�^�������Ɏ��̕����֑J�ڂ���|�b�v�A�b�v��\������
            if(objEBI("<%=callModalFlg.clientID %>").value == "<%=Boolean.TrueString %>"){
                objEBI("<%=callModalFlg.clientID %>").value = ""
                rtnArg = callModalBukken("<%=UrlConst.POPUP_BUKKEN_SITEI %>","<%=UrlConst.TEIBETU_SYUUSEI %>","teibetuA",true,"<%=HiddenKubun.Value %>","<%=HiddenBangou.Value %>");
                if(rtnArg == "null" && window.name != "<%=EarthConst.MAIN_WINDOW_NAME %>"){
                    window.close();
                    return;
                }
            }
            
            //�������s�{�^������
            if(autoExeButtonId != null){
              objEBI(autoExeButtonId).click();
              return;
            }
        }
        

    //Ajax�|�X�g�o�b�N�O����
    Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(InitializeRequestHandler);
        function InitializeRequestHandler(sender, args){
            var mng = Sys.WebForms.PageRequestManager.getInstance();
            if (mng.get_isInAsyncPostBack()){
                //�񓯊��|�X�g�o�b�N�����Ɏ��s���̏ꍇ�A���s���̃|�X�g�o�b�N�����𒆎~����B
                mng.abortPostBack();
            }
            objEBI("<%=HiddenAjaxFlg.ClientID %>").value=1;
        }

    //Ajax���[�h�㏈��
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args){
            objEBI("<%=HiddenAjaxFlg.ClientID %>").value=0;
        }
        
    
    </script>

    <!-- ��ʏ㕔�E�w�b�_[Table1] -->
    <table style="text-align: left; width: 960px;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tbody>
            <tr>
                <th style="text-align: left">
                    �@�ʃf�[�^�C��
                </th>
                <th style="text-align: left;">
                    <input name="" type="button" id="ButtonTouroku1" value="�o�^ / �C�� ���s" style="font-weight: bold;
                        font-size: 18px; width: 155px; color: black; height: 30px; background-color: fuchsia"
                        runat="server" />&nbsp;&nbsp;&nbsp;
                    <input id="ButtonTourokuExe" runat="server" style="display: none" type="button" value="�o�^ / �C�� ���s_���s" />
                </th>
                <th style="text-align: right; font-size: 11px;">
                    �ŏI�X�V�ҁF
                    <input name="" type="text" id="TextSaisyuuKousinsya" style="width: 120px" class="readOnlyStyle"
                        tabindex="-1" runat="server" /><br />
                    �ŏI�X�V�����F
                    <input name="" type="text" id="TextSaisyuuKousinDateTime" style="width: 100px" class="readOnlyStyle"
                        tabindex="-1" runat="server" />
                </th>
            </tr>
            <tr>
                <td colspan="2" rowspan="1" style="height: 10px">
                </td>
                <td colspan="1" rowspan="1" style="height: 10px">
                </td>
            </tr>
        </tbody>
    </table>
    <asp:HiddenField ID="HiddenAjaxFlg" runat="server" />
    <asp:HiddenField ID="HiddenKubun" runat="server" />
    <asp:HiddenField ID="HiddenBangou" runat="server" />
    <asp:HiddenField ID="HiddenDenpyouNGList" runat="server" />
    <asp:HiddenField ID="HiddenSeikyuuUmuCheck" runat="server" />
    <asp:HiddenField ID="HiddenChgValChk" runat="server" />
    <input type="hidden" id="callModalFlg" runat="server" />
    <!-- ****************************** -->
    <!-- ���ʏ��                       -->
    <!-- ****************************** -->
    <uc3:TeibetuKyoutuuCtrl ID="Kyoutuu" runat="server" />
    <br />
    <!-- ****************************** -->
    <!-- �ꊇ�W�J�{�^��                       -->
    <!-- ****************************** -->
    <input type="button" id="ButtonDisp" value="�ꊇ�W�J" onclick="openDisplay('<%= IraiNaiyou.IraiTBody.clientID %>,<%= TBodySyouhin12.clientID %>,<%= TBodySyouhin3.clientID %>,<%= TBodyKairyou.clientID %>,<%= TBodyHoukokusyo.clientID %>,<%= TBodyHosyou.clientID %>','<%= IraiNaiyou.IraiTitleInfobarID %>');" />&nbsp;
    <br />
    <br />
    <!-- ****************************** -->
    <!-- �˗����e                       -->
    <!-- ****************************** -->
    <uc4:TeibetuIraiNaiyouCtrl ID="IraiNaiyou" runat="server" />
    &nbsp;
    <br />
    <!-- ****************************** -->
    <!-- �˗����e�i���i�P�^�Q�j         -->
    <!-- ****************************** -->
    <table style="text-align: left; width: 960px;" id="Table2" class="mainTable" cellpadding="0"
        cellspacing="0">
        <thead>
            <tr>
                <th class="tableTitle" style="width: 300px">
                    <a href="JavaScript:changeDisplay('<%= TBodySyouhin12.clientID %>');">���i�P �^ ���i�Q&nbsp;&nbsp;</a>
                </th>
                <th class="shouhinTableTitleNyuukin">
                    <!-- �����z�E�c�z -->
                    <uc6:NyuukinZangakuCtrl ID="NyuukinZangakuCtrlSyouhin1" isNyuukingaku="True" runat="server" />
                </th>
                <th style="background-color: #ccffff; text-align: right;">
                    <input type="button" id="ButtonSyouhin4" runat="server" value="���i�S" class="openHosyousyoDB" />
                    <asp:UpdatePanel ID="UpdatePanelTokubetuTaiou" runat="server" RenderMode="Inline"
                        UpdateMode="Conditional">
                        <ContentTemplate>
                            <input type="button" id="ButtonTokubetuTaiou" runat="server" value="���ʑΉ�" class="" />
                            <!-- �˗����e�m�莞���ێ�_���ʑΉ�(��\��) -->
                            <input type="hidden" id="HiddenKakuteiValuesTokubetu" runat="server" />
                            </th>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </th>
            </tr>
        </thead>
        <tbody id="TBodySyouhin12" style="display: none;" runat="server">
            <tr>
                <td style="padding: 0px; border-left: 0px;" colspan="3">
                    <!-- ���i�R�[�h1 -->
                    <!-- �w�b�_�[���R�[�h -->
                    <uc5:TeibetuSyouhinHeaderCtrl ID="Syouhin1Header" DispMode="SYOUHIN1" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="padding: 0px; border-left: 0px;" colspan="3">
                    <!-- ���׃��R�[�h -->
                    <uc1:TeibetuSyouhinRecordCtrl ID="Syouhin1Record01" DispMode="SYOUHIN1" CssName="odd"
                        IsRowSpacer="true" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="padding: 0px; border-left: 0px;" colspan="3">
                    <!-- ���i�R�[�h2 -->
                    <uc7:TeibetuSyouhin2Ctrl ID="CtrlTeibetuSyouhin2" runat="server" />
                </td>
            </tr>
        </tbody>
    </table>
    <!-- ****************************** -->
    <!-- �˗����e�i���i�R�j         -->
    <!-- ****************************** -->
    <table style="text-align: left; width: 960px; border-top: none;" id="Table3" class="mainTable"
        cellpadding="0" cellspacing="0">
        <thead>
            <tr>
                <th class="tableTitle" style="width: 300px">
                    <a href="JavaScript:changeDisplay('<%= TBodySyouhin3.clientID %>');">���i�R&nbsp;&nbsp;
                    </a>
                </th>
                <th class="shouhinTableTitleNyuukin">
                    <!-- �����z�E�c�z -->
                    <uc6:NyuukinZangakuCtrl ID="NyuukinZangakuCtrlSyouhin3" isNyuukingaku="True" runat="server" />
                </th>
            </tr>
        </thead>
        <tbody id="TBodySyouhin3" style="display: none;" runat="server">
            <tr>
                <td style="padding: 0px; border-left: 0px;" colspan="2">
                    <uc8:TeibetuSyouhin3Ctrl ID="CtrlTeibetuSyouhin3" runat="server" />
                </td>
            </tr>
        </tbody>
    </table>
    <br />
    <!-- ****************************** -->
    <!-- ���ǍH��                       -->
    <!-- ****************************** -->
    <table style="text-align: left; width: 960px;" id="" class="mainTable" cellpadding="0"
        cellspacing="0">
        <!-- �w�b�_�� -->
        <thead>
            <tr>
                <th class="tableTitle" colspan="11">
                    <a href="JavaScript:changeDisplay('<%= TBodyKairyou.clientID %>');">���ǍH��</a>
                </th>
            </tr>
        </thead>
        <!-- �{�f�B�� -->
        <!-- �H����� -->
        <tbody id="TBodyKairyou" style="display: none;" runat="server">
            <!-- ���ǍH�� -->
            <uc2:TeibetuKoujiRecordCtrl ID="Kairyoukouji" runat="server" DispMode="KAIRYOU" />
            <!-- �ǉ����ǍH�� -->
            <uc2:TeibetuKoujiRecordCtrl ID="Tuikakouji" runat="server" DispMode="TUIKA" />
        </tbody>
    </table>
    <br />
    <!-- ****************************** -->
    <!-- �񍐏�                         -->
    <!-- ****************************** -->
    <table style="text-align: left; width: 960px;" id="Table1" class="mainTable" cellpadding="0"
        cellspacing="0" border="1">
        <!-- �w�b�_�� -->
        <thead>
            <tr>
                <th class="tableTitle" colspan="2">
                    <a href="JavaScript:changeDisplay('<%= TBodyHoukokusyo.clientID %>');">�񍐏�</a>
                </th>
            </tr>
        </thead>
        <!-- �{�f�B�� -->
        <tbody id="TBodyHoukokusyo" style="display: none;" runat="server">
            <!-- ****************************** -->
            <!-- �񍐏�(����)                   -->
            <!-- ****************************** -->
            <tr>
                <td class="syoBunrui" style="border-right: none; width: 300px;">
                    (����)</td>
                <td class="syoBunrui" style="border-left: none;">
                    <!-- �����z�E�c�z -->
                    <uc6:NyuukinZangakuCtrl ID="NyuukinZangakuCtrlHoukokusyoTyousa" isNyuukingaku="True"
                        runat="server" />
                </td>
            </tr>
            <tr>
                <td style="padding: 0px; border-left: 0px;" colspan="2">
                    <!-- �w�b�_�[���R�[�h -->
                    <uc5:TeibetuSyouhinHeaderCtrl ID="HoukokusyoTyousaHeader" DispMode="HOUKOKUSYO" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="padding: 0px; border-left: 0px;" colspan="2">
                    <!-- ���׃��R�[�h -->
                    <uc1:TeibetuSyouhinRecordCtrl ID="HoukokusyoTyousa" DispMode="HOUKOKUSYO" CssName="odd"
                        IsRowSpacer="true" runat="server" />
                </td>
            </tr>
            <!-- ****************************** -->
            <!-- �񍐏�(�H��)                   -->
            <!-- ****************************** -->
            <tr>
                <td class="syoBunrui" style="border-right: none; width: 300px;">
                    (�H��)</td>
                <td class="syoBunrui" style="border-left: none;">
                    <!-- �����z�E�c�z -->
                    <uc6:NyuukinZangakuCtrl ID="NyuukinZangakuCtrlHoukokusyoKouji" isNyuukingaku="True"
                        runat="server" />
                </td>
            </tr>
            <tr>
                <td style="padding: 0px; border-left: 0px;" colspan="2">
                    <!-- �w�b�_�[���R�[�h -->
                    <uc5:TeibetuSyouhinHeaderCtrl ID="HoukokusyoKoujiHeader" DispMode="HOUKOKUSYO" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="padding: 0px; border-left: 0px;" colspan="2">
                    <!-- ���׃��R�[�h -->
                    <uc1:TeibetuSyouhinRecordCtrl ID="HoukokusyoKouji" DispMode="HOUKOKUSYO" CssName="odd"
                        runat="server" />
                </td>
            </tr>
        </tbody>
    </table>
    <br />
    <!-- ****************************** -->
    <!-- �ۏ�                           -->
    <!-- ****************************** -->
    <table style="text-align: left; width: 960px;" id="Table4" class="mainTable" cellpadding="0"
        cellspacing="0" border="1">
        <!-- �w�b�_�� -->
        <thead>
            <tr>
                <th class="tableTitle" colspan="2">
                    <a href="JavaScript:changeDisplay('<%= TBodyHosyou.clientID %>');">�ۏ�</a>
                </th>
            </tr>
        </thead>
        <!-- �{�f�B�� -->
        <tbody id="TBodyHosyou" style="display: none;" runat="server">
            <!-- ****************************** -->
            <!-- �ۏ�(�ۏ؏�)                   -->
            <!-- ****************************** -->
            <tr>
                <td class="syoBunrui" style="border-right: none; width: 300px;">
                    (�ۏ؏�)</td>
                <td class="syoBunrui" style="border-left: none;">
                    <uc6:NyuukinZangakuCtrl ID="NyuukinZangakuCtrlHosyou" isNyuukingaku="True" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="padding: 0px; border-left: 0px;" colspan="2">
                    <!-- �w�b�_�[���R�[�h -->
                    <uc5:TeibetuSyouhinHeaderCtrl ID="HosyouHeader" DispMode="HOSYOU" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="padding: 0px; border-left: 0px;" colspan="2">
                    <!-- ���׃��R�[�h -->
                    <uc1:TeibetuSyouhinRecordCtrl ID="HosyouHosyousyo" DispMode="HOSYOU" CssName="odd"
                        IsRowSpacer="true" runat="server" />
                </td>
            </tr>
            <!-- ****************************** -->
            <!-- �ۏ�(��񕥖�)                   -->
            <!-- ****************************** -->
            <tr>
                <td class="syoBunrui" style="border-right: none; width: 300px;">
                    (��񕥖�)</td>
                <td class="syoBunrui" style="border-left: none;">
                    <asp:Label ID="LabelKaiyakuMessage" runat="server" Font-Bold="True" ForeColor="Red"
                        Width="121px"></asp:Label></td>
            </tr>
            <tr>
                <td style="padding: 0px; border-left: 0px;" colspan="2">
                    <!-- �w�b�_�[���R�[�h -->
                    <uc5:TeibetuSyouhinHeaderCtrl ID="KaiyakuHeader" DispMode="KAIYAKU" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="padding: 0px; border-left: 0px;" colspan="2">
                    <!-- ���׃��R�[�h -->
                    <uc1:TeibetuSyouhinRecordCtrl ID="HosyouKaiyaku" DispMode="KAIYAKU" CssName="odd"
                        runat="server" />
                </td>
            </tr>
        </tbody>
    </table>
    <br />
    <!-- ��ʉ����E�{�^�� -->
    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tbody>
            <tr>
                <th style="text-align: center;">
                    <input name="" type="button" id="ButtonTouroku2" value="�o�^ / �C�� ���s" style="font-weight: bold;
                        font-size: 18px; width: 155px; height: 30px; color: black; background-color: fuchsia"
                        runat="server" />&nbsp;
                </th>
            </tr>
        </tbody>
    </table>
    <asp:UpdatePanel ID="UpdatePanelHosyou" runat="server" RenderMode="Inline" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="HiddenUpdDatetime" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

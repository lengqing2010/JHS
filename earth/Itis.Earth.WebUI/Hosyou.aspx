<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="Hosyou.aspx.vb" Inherits="Itis.Earth.WebUI.Hosyou" Title="EARTH �ۏ�" %>

<%@ Register Src="control/GyoumuKyoutuuCtrl.ascx" TagName="GyoumuKyoutuuCtrl" TagPrefix="uc1" %>
<%@ Register Src="control/SeikyuuSiireLinkCtrl.ascx" TagName="SeikyuuSiireLinkCtrl"
    TagPrefix="uc2" %>
<%@ Import Namespace="Itis.Earth.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script type="text/javascript">
        //��ʋN�����ɃE�B���h�E�T�C�Y���f�B�X�v���C�ɍ��킹��
        window.moveTo(0, 0);
        window.resizeTo(window.screen.availWidth, window.screen.availHeight);

        _d = document;
        
        var tmpConfirmResultButtonId = "";
                
        /****************************************
         * onload ���̒ǉ�����
         * @param objTarget
         * @return
         ****************************************/
        function funcAfterOnload() {
            var objChkKaisiDate = objEBI("<%= HiddenChkKaisiDate.clientID %>");
            if (objChkKaisiDate.value == "1") {
               changeDisplay("<%= TbodyHakkouIraiInfo.clientID %>");
               SetDisplayStyle("<%= HiddenHakkouIraiInfoStyle.ClientID %>", "<%= TbodyHakkouIraiInfo.ClientID %>");
            }
        }

        //�ύX�O�R���g���[���̒l��ޔ����āA�Y���R���g���[��(Hidden)�ɕێ�����
        function SetChangeMaeValue(strTaihiID, strTargetID){
           document.getElementById(strTaihiID).value = document.getElementById(strTargetID).value;
        }
        
        //�o�^�{�^���������̓o�^���m�F���s�Ȃ��B
        function CheckTouroku(){
            //Chk03 �ۏ؏����s�����n��.�������{���̏ꍇ
            var objHosyousyoHakkouDate = objEBI("<%= TextHosyousyoHakkouDate.clientID %>");
            var objTyousaJissiDate = objEBI("<%= HiddenTyousaJissiDateOld.clientID %>");
            if(objHosyousyoHakkouDate.value != "" && objTyousaJissiDate.value != ""){
                if(Number(removeSlash(objHosyousyoHakkouDate.value)) < Number(removeSlash(objTyousaJissiDate.value))){
                    if(objEBI("<%= HiddenHosyousyoHakkouDateMsg03.clientID %>").value != "1" && objEBI("<%= HiddenChk03.clientID %>").value != "1"){
                        if(confirm("<%= Messages.MSG099C %>")){
                            objEBI("<%= HiddenChk03.clientID %>").value = "1";
                        }else{
                            return false;
                        }
                    }
                }
            }

            //Chk04 �ۏ؏����s�������́A�n��.�������{���������͂̏ꍇ
            var objHosyousyoHakkouDate = objEBI("<%= TextHosyousyoHakkouDate.clientID %>");
            var objTyousaJissiDate = objEBI("<%= HiddenTyousaJissiDateOld.clientID %>");
            if(objHosyousyoHakkouDate.value != "" && objTyousaJissiDate.value == ""){
                if(document.getElementById("<%= HiddenHosyousyoHakkouDateMsg04.clientID %>").value != "1" && objEBI("<%= HiddenChk04.clientID %>").value != "1"){
                    if(confirm("<%= Messages.MSG098C %>")){
                        objEBI("<%= HiddenChk04.clientID %>").value = "1";
                    }else{
                        return false;
                    }
                }
            }

            //Chk21 �ۏ؊��ԂɕύX������ꍇ�A�x�����b�Z�[�W�\��
            var varMsg205 = "<%= Messages.MSG205S %>";
            var objHosyouKikanOld = objEBI("<%= HiddenHosyouKikanOld.clientID %>");
            var objHosyouKikanNew = objEBI("<%= TextHosyouKikan.clientID %>");
            if(objHosyouKikanOld.value != objHosyouKikanNew.value){
                varMsg205 = varMsg205.replace('@PARAM1',objHosyouKikanOld.value);
                varMsg205 = varMsg205.replace('@PARAM2',objHosyouKikanNew.value);
                alert(varMsg205);
            }
            
            return true;�@//�`�F�b�N����
        }
                
        //�t�ۏؖ���FLG�ύX������(Cancel��PostBack)
        function callFuhoSyoumeisyoFlgCancel(strMsg ,btnId){
            if(confirm(strMsg)){
            }else{
                tmpConfirmResultButtonId = btnId;
            }
        }
        
        //IME��������p�t�H�[�J�X�o�E���_�[�i�Ĕ��s���R�p
        function setFocusSaihakkouRiyuu(){
            objEBI("<%=TextSaihakkouRiyuu.ClientId %>").focus();
        }

        //Ajax���s������
        Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(InitializeRequestHandler);
            function InitializeRequestHandler(sender, args){
                objEBI("<%=HiddenAjaxFlg.ClientID %>").value=1;
        }
        //Ajax���[�h�㏈��
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args){
            if(tmpConfirmResultButtonId != ""){
                objEBI(tmpConfirmResultButtonId).click();
                tmpConfirmResultButtonId = "";
            }
            objEBI("<%=HiddenAjaxFlg.ClientID %>").value=0;
        }
        
        //�v���_�E���̒l������l�̏ꍇ�A�w��̃G�������g��ReadOnly���O���A���ږ����x���̕\����Ԃ�ؑւ���
        function checkPullDown(){
            var objTarget = objEBI("<%= TextHosyouNasiRiyuu.clientID %>");
            var objPulldown = objEBI("<%= SelectHosyouNasiRiyuu.clientID %>");
            var objHdnPulldown = objEBI("<%= SelectHannyouNo.clientID %>");
            var index = 0;
            var text = "0";
            
            //�I�����ڂ̔ėpNO���擾
            index = objPulldown.selectedIndex;
            if(index == -1){
                //�񊈐����A�l�N���A
                objTarget.value = "";
                objTarget.disabled = true;
                objTarget.style.backgroundColor = "<%= CSS_COLOR_GRAY %>";            
            }else{
                objHdnPulldown.selectedIndex = index;
                text = objHdnPulldown.options[index].text;
                
                if(objPulldown.style.display != "none"){
                    if(text == "1"){
                        //������
                        objTarget.disabled = false;
                        objTarget.style.backgroundColor = "<%= EarthConst.STYLE_COLOR_WHITE %>";
                    }else{
                        //�񊈐����A�l�N���A
                        objTarget.value = "";
                        objTarget.disabled = true;
                        objTarget.style.backgroundColor = "<%= CSS_COLOR_GRAY %>";
                    }    
                }
            }
        }
        
        //�����X�����������Ăяo��
        function callYoteiKameitenSearch(obj){
            objEBI("<%= yoteiKameitenSearchType.clientID %>").value = "";
            if(obj.value == ""){
                objEBI("<%= yoteiKameitenSearchType.clientID %>").value = "1";
                objEBI("<%= ButtonYoteiKameitenSearch.clientID %>").click();
            }
        }        
        
        // ���s�˗���t�Z�b�g�̃`�F�b�N
        function CheckIraiUketuke(){
            // Chk27 �e���ځi�{�喼�ƕ������́A�Z���P+2+3�ƕ������ݒn1+2+3�A�ۏ؏��.�����n�����Ƃ����n�����j���r���A
            //       �قȂ���̂��ЂƂł�����ꍇ�̓��b�Z�[�W�\��

            var chkWarnFlg = 0;

            var objTextSesyuMei = objEBI("<%= ucGyoumuKyoutuu.AccSesyuMei.clientID %>");
            var objTextbukkenuMei = objEBI("<%= TextbukkenuMei.clientID %>");
            var objTextBukkenJyuusyo1 = objEBI("<%= ucGyoumuKyoutuu.AccBukkenJyuusyo1.clientID %>");
            var objTextBukkenJyuusyo2 = objEBI("<%= ucGyoumuKyoutuu.AccBukkenJyuusyo2.clientID %>");
            var objTextBukkenJyuusyo3 = objEBI("<%= ucGyoumuKyoutuu.AccBukkenJyuusyo3.clientID %>");
            var objTextBukkenuSyozai1 = objEBI("<%= TextBukkenuSyozai1.clientID %>");
            var objTextBukkenuSyozai2 = objEBI("<%= TextBukkenuSyozai2.clientID %>");
            var objTextBukkenuSyozai3 = objEBI("<%= TextBukkenuSyozai3.clientID %>");
            var objTextHosyouKaisiDate = objEBI("<%= TextHosyouKaisiDate.clientID %>");
            var objTextHikiwatasiDate = objEBI("<%= TextHikiwatasiDate.clientID %>");

            // �{�喼�ƕ�������
            if (objTextSesyuMei.value != objTextbukkenuMei.value) {
               chkWarnFlg = 1;
            }

            // �Z���P+2+3�ƕ������ݒn1+2+3
            if (objTextBukkenJyuusyo1.value + objTextBukkenJyuusyo2.value + objTextBukkenJyuusyo3.value  != objTextBukkenuSyozai1.value + objTextBukkenuSyozai2.value + objTextBukkenuSyozai3.value) {
               chkWarnFlg = 1;
            }

            // �ۏ؏��.�����n�����i�ۏ؊J�n���j�Ɣ��s�˗����.�����n����
            var nKaisi = 0;
            if (objTextHosyouKaisiDate.value != "") {
               nKaisi = Number(removeSlash(objTextHosyouKaisiDate.value));
            } 
            var nHikiwatasi = 0;
            if (objTextHikiwatasiDate.value != "") {
               nHikiwatasi = Number(removeSlash(objTextHikiwatasiDate.value));
            }
            if (nKaisi < nHikiwatasi) {
               chkWarnFlg = 1;
            }
            
            if (chkWarnFlg == 1) {
	            if(confirm("<%= Messages.MSG212C %>")){
	                objEBI("<%= HiddenChk27.clientID %>").value = "1";
	            }else{
	                return false;
	            }
            }

            // Chk28 �i�ۏ؏��Ĕ��s���A�ۏ؏����s���j�����V�X�e�����t
            chkWarnFlg = 0;
            var nHakko = 0;
            var objTextSaihakkouDate = objEBI("<%= TextSaihakkouDate.clientID %>");
            var objTextHosyousyoHakkouDate = objEBI("<%= TextHosyousyoHakkouDate.clientID %>");
            
            if (objTextSaihakkouDate.value != "") {
                nHakko = Number(removeSlash(objTextSaihakkouDate.value));
            } else if (objTextHosyousyoHakkouDate.value != "") {
                nHakko = Number(removeSlash(objTextHosyousyoHakkouDate.value));
            }
            
            if (nHakko >= Number(removeSlash(getToday()))) {
               chkWarnFlg = 1;
            }

            if (chkWarnFlg == 1) {
	            if(confirm("<%= Messages.MSG213C %>")){
	                objEBI("<%= HiddenChk28.clientID %>").value = "1";
	            }else{
	                return false;
	            }
            }
            
            // ��ʁD�ۏ؏����s�����󔒂̏ꍇ...
            var chkConfirmFlg = 0;
            // �Ĕ��s���i�R�[�h
            var objTextShSyouhinCd = objEBI("<%= TextShSyouhinCd.clientID %>");

            // �ۏ؏����s������
            if (objTextHosyousyoHakkouDate.value == "") {
                // ��ʁD�ۏ؏����s���ɉ�ʁD�Z�b�g���s�����Z�b�g
                objEBI("<%= HiddenHakkouSetTo.clientID %>").value = "1";

            // �ۏ؏��Ĕ��s������
            } else if (objTextSaihakkouDate.value == "") {
                // ��ʁD�ۏ؏��Ĕ��s���ɉ�ʁD�Z�b�g���s�����Z�b�g
                objEBI("<%= HiddenHakkouSetTo.clientID %>").value = "2";

            // ��ʁD�Ĕ��s���i�R�[�h����
            } else if (objTextShSyouhinCd.value == "") {
                objEBI("<%= HiddenHakkouSetTo.clientID %>").value = "3";

            } else {
	            if(confirm("<%= Messages.MSG214C %>")){
                    objEBI("<%= HiddenHakkouSetTo.clientID %>").value = "4";
	            }else{
	                return false;
	            }
            }

            // �����p�� 
            return true;

        }

        // �ۏ؏��.�����n�����i�ۏ؊J�n���j�֓]�L
        function CheckHosyouKaisiDate(){ 
            // �����n�������V�X�e�����t��3�N�O�`3�N��͈̔͊O�̏ꍇ���b�Z�[�W�\��
            var objHikiwatasiDate = objEBI("<%= TextHikiwatasiDate.clientID %>");

            var objToday = new Date();
            var objPastDay = new Date(objToday.getFullYear() - 3, objToday.getMonth(), objToday.getDate());
            var objFutureDay = new Date(objToday.getFullYear() + 3, objToday.getMonth(), objToday.getDate());
            
            var objHikiwatasiDay = new Date(Date.parse(objHikiwatasiDate.value));
            
            if((objHikiwatasiDate.value != "") &&
              (( objHikiwatasiDay <= objPastDay) || ( objHikiwatasiDay >= objFutureDay)))
            {
	            if(confirm("<%= Messages.MSG217C %>")){
	                objEBI("<%= HiddenChkKaisiDate.clientID %>").value = "1";
	            }else{
	                return false;
	            }
            }
            // �����p�� 
            return true;
        }
    </script>

    <!-- ��ʏ㕔�E�w�b�_ -->
    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tbody>
            <tr>
                <th style="text-align: left; width: 150px;">
                    �ۏ�
                </th>
                <th style="text-align: left;">
                    <input type="button" id="ButtonTouroku1" value="�o�^ / �C�� ���s" style="font-weight: bold;
                        font-size: 18px; width: 155px; color: black; height: 30px; background-color: fuchsia"
                        runat="server" />&nbsp;&nbsp;&nbsp;
                </th>
                <th style="text-align: right; font-size: 11px;">
                    �ŏI�X�V�ҁF<asp:TextBox ID="TextSaisyuuKousinsya" CssClass="readOnlyStyle" Style="width: 120px"
                        ReadOnly="true" Text="" runat="server" TabIndex="-1" /><br />
                    �ŏI�X�V�����F<asp:TextBox ID="TextSaisyuuKousinDate" CssClass="readOnlyStyle" Style="width: 100px"
                        ReadOnly="true" Text="" runat="server" TabIndex="-1" />
                    <asp:HiddenField ID="HiddenJibanUpdateDateTime" runat="server" />
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
    <asp:UpdatePanel ID="UpdatePanelHosyou" UpdateMode="conditional" runat="server" RenderMode="Inline">
        <ContentTemplate>
            <uc1:GyoumuKyoutuuCtrl ID="ucGyoumuKyoutuu" runat="server" DispMode="HOSYOU" />
            <br />
            <asp:UpdatePanel ID="UpdatePanelHall" UpdateMode="conditional" runat="server" RenderMode="inline"
                ChildrenAsTriggers="true">
                <Triggers>
                    <%--�Ɩ����ʃ^�u--%>
                    <asp:AsyncPostBackTrigger ControlID="ucGyoumuKyoutuu" />
                </Triggers>
                <ContentTemplate>
                    <input type="hidden" id="HiddenAjaxFlg" runat="server" /><%--Ajax�������t���O--%>
                    <input type="hidden" id="HiddenHantei1CdOld" runat="server" value="" /><%--����1�R�[�hOld--%>
                    <input type="hidden" id="HiddenTyousaJissiDateOld" runat="server" value="" /><%--�������{��Old--%>
                    <input type="hidden" id="HiddenHosyousyoHakkouDateMae" runat="server" value="" /><%--�ۏ؏����s���ύX�O--%>
                    <input type="hidden" id="HiddenSaihakkouDateMae" runat="server" value="" /><%--�Ĕ��s���ύX�O--%>
                    <input type="hidden" id="HiddenSaihakkouDateOld" runat="server" /><%--�ۏ؏��Ĕ��s��Old--%>
                    <input type="hidden" id="HiddenShSeikyuuUmuMae" runat="server" value="" /><%--�����L���ύX�O--%>
                    <input type="hidden" id="HiddenShZeiritu" runat="server" value="" /><%--����E�ŗ�--%>
                    <input type="hidden" id="HiddenKyZeiritu" runat="server" value="" /><%--��񕥖߁E�ŗ�--%>
                    <input type="hidden" id="HiddenShJituseikyuu1Flg" value="" runat="server" /><%--�������Ŕ����z1�t���O--%>
                    <input type="hidden" id="HiddenSyouhin1SeikyuuHakkouDate" value="" runat="server" /><%--<���i�R�[�h1>���������s��--%>
                    <input type="hidden" id="HiddenHosyousyoHakkouDateMsg03" runat="server" value="" /><%--�ۏ؏����s���ύXChk03--%>
                    <input type="hidden" id="HiddenHosyousyoHakkouDateMsg04" runat="server" value="" /><%--�ۏ؏����s���ύXChk03--%>
                    <input type="hidden" id="HiddenChk03" runat="server" value="" /><%--Chk03--%>
                    <input type="hidden" id="HiddenChk04" runat="server" value="" /><%--Chk04--%>
                    <input type="hidden" id="HiddenChk27" runat="server" value="" /><%--Chk27--%>
                    <input type="hidden" id="HiddenChk28" runat="server" value="" /><%--Chk28--%>
                    <input type="hidden" id="HiddenChkKaisiDate" runat="server" value="" /><%--HiddenChkKaisiDate--%>
                    <input type="hidden" id="HiddenHakkouSetTo" runat="server" value="" />
                    <input type="hidden" id="HiddenKyKaiyakuHaraimodosiSinseiUmuMae" runat="server" value="" /><%--��񕥖ߐ\���L���ύX�O--%>
                    <input type="hidden" id="HiddenTyousaHattyuusyoKakuninDateFlg" runat="server" value="" /><%--�����������m�F���t���O--%>
                    <input type="hidden" id="HiddenKoujiHattyuusyoKakuninDateFlg" runat="server" value="" /><%--�H���������m�F���t���O--%>
                    <input type="hidden" id="HiddenFuhoSyoumeisyoFlgMae" runat="server" value="" /><%--�t�ۏؖ���FLG�ύX�O--%>
                    <input type="hidden" id="HiddenDefaultSiireSakiCdForLink" runat="server" value="" /><%--������ЃR�[�h�i�A���j--%>
                    <input type="hidden" id="HiddenKameitenCd" runat="server" /><%--�����X�R�[�h--%>
                    <input type="hidden" id="HiddenHosyouSyouhinUmu" runat="server" /><%--�ۏ؏��i�L��--%>
                    <input type="hidden" id="HiddenHosyousyoHakJyKyMae" runat="server" /><%--�ۏ؏����s�󋵕ύX�O--%>
                    <input type="hidden" id="HiddenSetFuhoSyoumeisyoFlg" runat="server" value="0" /><%--�t�ۏؖ���FLG�����ݒ�t���O--%>
                    <input type="hidden" id="HiddenHkUpdDatetime" runat="server" /><%--�ۏ؏��Ǘ��e�[�u���X�V����--%>
                    <input type="hidden" id="HiddenHosyouKikanOld" runat="server" /><%--�ۏ؊���Old--%>
                    <input type="hidden" id="HiddenHakIraiUkeDatetimeOld" runat="server" /><%--���s�˗���t����Old(�n��)--%>
                    <input type="hidden" id="HiddenHakIraiCanDatetimeOld" runat="server" /><%--���s�˗��L�����Z������Old(�n��)--%>
                    <input type="hidden" id="HiddenShSyouhinCdOld" runat="server" /><%--�Ĕ��s���i�R�[�hOld--%>
                    <input type="hidden" id="HiddenHakIraiUketukeFlg" runat="server" /><%--�ۏ؏����s��t�t���O--%>
                    <input type="hidden" id="HiddenHakIraiCancelFlg" runat="server" /><%--�ۏ؏����s�L�����Z���t���O--%>
                    <input type="hidden" id="HiddenHakIraiTime" runat="server" /><%--���s�˗���--%>
                    <input type="hidden" id="HiddenHakIraiUkeDatetimeR" runat="server" /><%--���s�˗���t����(�i��)--%>
                    <input type="hidden" id="HiddenHakIraiCanDatetimeR" runat="server" /><%--���s�˗��L�����Z������(�i��)--%>

                    <div id="divHakkouIrai" runat="server">
                        <!-- ��ʒ������E���s�˗���� -->
                        <table style="text-align: left; width: 100%;" id="Table6" class="mainTable" cellpadding="0"
                            cellspacing="0" border="1">
                            <!-- �w�b�_�� -->
                            <thead>
                                <tr>
                                    <th class="tableTitle" colspan="8">
                                        <a id="AncHakkouIraiInfo" runat="server">���s���˗����</a>
                                        <input type="hidden" id="HiddenHakkouIraiInfoStyle" runat="server" value="inline" />
                                    </th>
                                    <th id="Th2" class="tableTitle" style="text-align: right" colspan="2" runat="server">
                                        <asp:TextBox ID="TextHakIraiTime" CssClass="readOnlyStyle2"
                                             readonly="True" tabindex="-1" runat="server" />
                                    </th>
                            <thead>
                            <tbody id="TbodyHakkouIraiInfo" class="scrolltablestyle" runat="server">
                                <tr>
                                    <td colspan="10" class="tableSpacer">
                                    </td>
                                </tr>
                                <!-- 1�s�� -->
                                <tr>
                                    <td class="koumokuMei">
                                        ��������
                                    </td>
                                    <td colspan="7" style="border-right-style:none">
                                        <asp:TextBox ID="TextbukkenuMei" CssClass="readOnlyStyle2"
                                             readonly="True" tabindex="-1" runat="server" style="width:90%"/>
                                    </td>
                                    <td style="border-left-style:none; border-right-style:none">
                                       <input id="ButtonBukkenTenki" runat="server" type="button" value="�{�喼�֓]�L" class="BukkenTenkiBtn" style="width:110px"
                                        onserverclick="ButtonBukkenTenki_ServerClick" /><input type="hidden" id="BukkenTenkiType"
                                            runat="server" />
                                    </td>
                                    <td style="border-left-style:none">
                                       <input id="ButtonHakkouCancel" runat="server" type="button" value="�L�����Z��"  style="font-weight: bold;background-color: #faebd7;width:80px" class="HakkouCancelBtn"
                                        onserverclick="ButtonHakkouCancel_ServerClick" /><input type="hidden" id="HakkouCancelType"
                                            runat="server" />
                                    </td>
                                </tr>
                                <!-- 2�s�� -->
                                <tr>
                                    <td class="koumokuMei" rowspan="2">
                                        �������ݒn
                                    </td>
                                    <td id="TdBukkenSyozai1" runat="server" colspan="4" style="border-right-style:none; border-bottom-style:none">�P�F
                                        <asp:TextBox ID="TextBukkenuSyozai1" CssClass="readOnlyStyle2" style="width:100%;font-size:11px"
                                             readonly="True" tabindex="-1" runat="server"/>
                                    </td>
                                    <td id="TdBukkenSyozai2" runat="server" colspan="2" style="border-left-style:none; border-bottom-style:none; padding-left:20px">�Q�F
                                        <asp:TextBox ID="TextBukkenuSyozai2" CssClass="readOnlyStyle2" style="width:80%;font-size:11px"
                                             readonly="True" tabindex="-1" runat="server"/>
                                    </td>
                                    <td class="koumokuMei">
                                        �Z�b�g���s��
                                    </td>
                                    <td id="TdSetHakkouDate" style="border-right-style:none" runat="server">
                                        <asp:TextBox ID="TextSetHakkouDate" CssClass="date" MaxLength="10" Text=""
                                            runat="server" />
                                    </td>
                                    <td style="border-left-style:none">
                                       <input id="ButtonHakkouSet" runat="server" type="button" value="��t�Z�b�g" style="font-weight: bold;background-color: #ffd700;width:80px" />
                                       <input type="hidden" id="HakkouSetType"
                                            runat="server" />
                                    </td>
                                </tr>
                                <!-- 3�s�� -->
                                <tr>
                                    <td id="TdBukkenSyozai3" runat="server" colspan="5" style="border-right-style:none; border-top-style:none">�R�F
                                        <asp:TextBox ID="TextBukkenuSyozai3" CssClass="readOnlyStyle2"
                                             readonly="True" tabindex="-1" runat="server" style="width:100%;font-size:11px"/>&nbsp;
                                    </td>
                                    <td id="TdButtonJyuusyoTenki" runat="server" style="border-left-style:none; border-top-style:none">
                                       <input id="ButtonJyuusyoTenki" runat="server" type="button" value="�����Z���֓]�L" class="JyuusyoTenkiBtn" style="width:100px"
                                        onserverclick="ButtonJyuusyoTenki_ServerClick"/><input type="hidden" id="JyuusyoTenkiType"
                                            runat="server" />
                                    </td>
                                    <td class="koumokuMei">
                                        �����n����
                                    </td>
                                    <td id="TdHikiwatasiDate" runat="server" colspan="2">
                                        <asp:TextBox ID="TextHikiwatasiDate" CssClass="readOnlyStyle2"
                                             readonly="True" tabindex="-1" runat="server" />
                                       <input id="ButtonHosyouKaisiDateTenki" runat="server" type="button" value="�����n�����֓]�L" class="HosyouKaisiDateTenkiBtn"  style="width:120px"
                                        onserverclick="ButtonHosyouKaisiDateTenki_ServerClick" /><input type="hidden" id="HosyouKaisiDateTenkiType"
                                            runat="server" />
                                    </td>
                                </tr>
                                <!-- 4�s�� -->
                                <tr>
                                    <td class="koumokuMei">
                                        �S����
                                    </td>
                                    <td id="TdTantouSya" runat="server" colspan="3">
                                        <asp:TextBox ID="TextTantouSya" CssClass="readOnlyStyle2"
                                             readonly="True" tabindex="-1" runat="server" style="font-size:11px"/>
                                    </td>
                                    <td class="koumokuMei">
                                        �A����
                                    </td>
                                    <td id="TdRenrakuSaki" runat="server" colspan="1">
                                        <asp:TextBox ID="TextRenrakuSaki" CssClass="readOnlyStyle2"
                                             readonly="True" tabindex="-1" runat="server" style="font-size:11px"/>
                                    </td>
                                    <td></td>
                                    <td class="koumokuMei">
                                        ����ID
                                    </td>
                                    <td id="TdNyuuryokuID" runat="server" colspan="2">
                                        <asp:TextBox ID="TextNyuuryokuID" CssClass="readOnlyStyle2"
                                             readonly="True" tabindex="-1" runat="server"  style="width:90%;font-size:11px"/>
                                    </td>
                                </tr>
                                <!-- 5�s�� -->
                                <tr>
                                    <td class="koumokuMei">
                                        ���̑����
                                    </td>
                                    <td id="TdSonota" runat="server" colspan="9">
                                        <asp:textbox ID="TextIraiSonota" CssClass="readOnlyStyle2" textmode="MultiLine" rows="3"
                                             readonly="True" tabindex="-1" runat="server" style="width:90%"/>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <br />
                    <div id="divHosyou" runat="server">
                        <!-- ��ʒ������E�ۏ؏�� -->
                        <table style="text-align: left; width: 100%;" id="Table2" class="mainTable" cellpadding="0"
                            cellspacing="0" border="1">
                            <!-- �w�b�_�� -->
                            <thead>
                                <tr>
                                    <th class="tableTitle" style="padding: 0px; height: 26px" colspan="7">
                                        <a id="AncHosyouInfo" runat="server">�ۏ؏��</a>
                                        <input type="hidden" id="HiddenHosyouInfoStyle" runat="server" value="inline" />
                                    </th>
                                    <th id="Th1" class="tableTitle" style="padding: 0px; text-align: right;  height: 26px" runat="server">
                                        <input type="button" id="ButtonBukkenJyokyou" value="�����i����" style="width: 100px"
                                            class="button_copy" runat="server" />&nbsp;
                                    </th>
                                </tr>
                            </thead>
                            <!-- �{�f�B�� -->
                            <tbody id="TbodyHoshoInfo" class="scrolltablestyle" runat="server">
                                <tr>
                                    <td colspan="8" class="tableSpacer">
                                    </td>
                                </tr>
                                <!-- 1�s�� -->
                                <tr>
                                    <td style="width: 80px" class="koumokuMei">
                                        �_��NO
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextKeiyakuNo" Style="width: 80px; ime-mode: inactive;" CssClass=""
                                            runat="server" MaxLength="20" />
                                    </td>
                                    <td class="koumokuMei">
                                        �������{��
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextTyousaJissiDate" CssClass="date readOnlyStyle" Text="" MaxLength="10"
                                            Style="width: 70px" ReadOnly="true" TabIndex="-1" runat="server" />
                                    </td>
                                    <td class="koumokuMei">
                                        �v�揑�쐬��
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextKeikakusyoSakuseiDate" CssClass="date readOnlyStyle" Text=""
                                            MaxLength="10" Style="width: 70px" ReadOnly="true" TabIndex="-1" runat="server" />
                                    </td>
                                    <td class="koumokuMei">
                                        �����m�F����
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextNyuukinKakuninJyouken" Style="width: 150px" CssClass="readOnlyStyle"
                                            Text="" ReadOnly="true" TabIndex="-1" runat="server" />
                                    </td>
                                </tr>
                                <!-- 2�s�� -->
                                <tr>
                                    <td class="koumokuMei">
                                        ��b�񍐏�
                                    </td>
                                    <td id="TdKisoHoukokusyo" runat="server">
                                        <asp:DropDownList ID="SelectKisoHoukokusyo" CssClass="" Style="display: inline;"
                                            runat="server" AutoPostBack="true" OnSelectedIndexChanged="SelectKisoHoukokusyo_SelectedIndexChanged">
                                            <asp:ListItem Value="" Text="" Selected="true"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="�L"></asp:ListItem>
                                        </asp:DropDownList><span id="SpanKisoHoukokusyo" runat="server"></span>
                                    </td>
                                    <td class="koumokuMei">
                                        ��b�񍐏�����
                                    </td>
                                    <td id="TdKisoHoukokusyoTyakuDate" runat="server">
                                        <asp:TextBox ID="TextKisoHoukokusyoTyakuDate" CssClass="date" MaxLength="10" Text=""
                                            runat="server" />
                                    </td>
                                    <td class="koumokuMei">
                                        �ύX�\������X
                                    </td>
                                    <td colspan="3">
                                        <input type="text" id="TextYoteiKameitenCd" maxlength="5" class="codeNumber" style="width: 40px;" 
                                            runat="server" /><input type="hidden" id="HiddenYoteiKameitenCdTextOld" runat="server" /><input
											type="hidden" id="HiddenYoteiKameitenCdTextMae" runat="server" />
                                        <input id="ButtonYoteiKameitenSearch" runat="server" type="button" value="����" class="GyoumuSearchBtn"
                                             /><input type="hidden" id="yoteiKameitenSearchType" runat="server" />
                                        <input type="text" id="TextYoteiKameitenMei" readonly="readonly" style="width:200px;"
                                            class="readOnlyStyle" tabindex="-1" runat="server" />
                                        <input id="ButtonYoteiKameitenTyuuijouhou" runat="server" class="btnKameitenTyuuijouhou"
                                            type="button" value="���ӏ��" />
                                    </td>
                                </tr>
                                <!-- 3�s�� -->
                                <tr>
                                    <td class="koumokuMei">
                                        ���s�˗���
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="SelectHakkouIraisyo" CssClass="" Style="display: inline;" runat="server"
                                            AutoPostBack="true" OnSelectedIndexChanged="SelectHakkouIraisyo_SelectedIndexChanged">
                                            <asp:ListItem Value="" Text="" Selected="true"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="�L"></asp:ListItem>
                                        </asp:DropDownList><span id="SpanHakkouIraisyo" runat="server"></span>
                                    </td>
                                    <td class="koumokuMei">
                                        ���s�˗�������
                                    </td>
                                    <td id="TdHakkouIraiTyakuDate" runat="server">
                                        <asp:TextBox ID="TextHakkouIraiTyakuDate" CssClass="date" MaxLength="10" Text=""
                                            runat="server" />
                                    </td>
                                    <td class="koumokuMei">
                                        �Ɩ�������
                                    </td>
                                    <td id="TdGyoumuKanryouDate" runat="server">
                                        <asp:TextBox ID="TextGyoumuKanryouDate" CssClass="date readOnlyStyle" MaxLength="10" Text=""
                                            Style="width: 70px" ReadOnly="true" TabIndex="-1" runat="server" />
                                    </td>
                                    <!-- �Ɩ��������e -->
                                    <td colspan="3" id="TdGyoumuKaisiNaiyou" runat="server">
                                        <asp:TextBox ID="TextGyoumuKaisiNaiyou" CssClass="readOnlyStyle"
                                            ReadOnly="true" TabIndex="-1" runat="server" />
                                    </td>
                                </tr>
                                <!-- 4�s�� -->
                                <tr>
                                    <td class="koumokuMei">
                                        �ۏ؏����s��
                                    </td>
                                    <td colspan="3" id="TdHosyousyoHakkouJyoukyou" runat="server">
                                        <asp:DropDownList ID="SelectHosyousyoHakkouJyoukyou" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="SelectHosyousyoHakkouJyoukyou_SelectedIndexChanged">
                                        </asp:DropDownList><span id="SpanHosyousyoHakkouJyoukyou" runat="server"></span>
                                        <span id="SpanHosyousyoHakJykyHosyouUmu" runat="server" style="font-weight:bold;font-size:13px;"></span>
                                    </td>
                                    <td class="koumokuMei" colspan="2">
                                        �ۏ؏����s�󋵐ݒ��
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="TextHosyousyoHakkouJyoukyouSetteiDate" CssClass="date readOnlyStyle"
                                            Text="" MaxLength="10" Style="width: 70px" ReadOnly="true" TabIndex="-1" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <!-- 5�s�� -->
                                <tr>
                                    <td class="koumokuMei">
                                        �ۏ؏����s��
                                    </td>
                                    <td id="TdHosyousyoHakkouDate" runat="server">
                                        <asp:TextBox ID="TextHosyousyoHakkouDate" CssClass="date" MaxLength="10" Text=""
                                            runat="server" OnTextChanged="TextHosyousyoHakkouDate_TextChanged" />
                                    </td>
                                    <td class="koumokuMei">
                                        ������&nbsp;<input type="button" id="ButtonHkKousin" value="�X�V" runat="server" style="height:20px;"/>
                                    </td>
                                    <td id="TdBukkenJyky" runat="server">
                                        <span id="SpanBukkenJyky" runat="server" style="font-size:13px"></span><input type="hidden"
                                            id="HiddenBukkenJyky" runat="server" />
                                    </td>
                                    <td class="koumokuMei">
                                        �t�ۏؖ���FLG
                                    </td>
                                    <td id="TdFuhoSyoumeisyoFlg" runat="server">
                                        <asp:DropDownList ID="SelectFuhoSyoumeisyoFlg" CssClass="" Style="display: inline;"
                                            runat="server" AutoPostBack="true" OnSelectedIndexChanged="SelectFuhoSyoumeisyoFlg_SelectedIndexChanged">
                                            <asp:ListItem Value="" Text="" Selected="true"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="�L��"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="�Ȃ�"></asp:ListItem>
                                        </asp:DropDownList><span id="SpanFuhoSyoumeisyoFlg" runat="server"></span>
                                        <input type="button" id="ButtonFuhoSyoumeisyoFlg" runat="server" style="display: none;"
                                            onserverclick="ButtonFuhoSyoumeisyoFlgCancel_ServerClick" />
                                    </td>
                                    <td class="koumokuMei">
                                        �t�ۏؖ���������
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextFuhoSyoumeisyoHassouDate" CssClass="date readOnlyStyle" MaxLength="10"
                                            Text="" runat="server" ReadOnly="true" TabIndex="-1" />
                                    </td>
                                </tr>
                                <!-- 6�s�� -->
                                <tr>
                                    <td class="koumokuMei">
                                        �ۏ؏�������
                                    </td>
                                    <td id="TdHosyousyoHassouDate" runat="server">
                                        <asp:TextBox ID="TextHosyousyoHassouDate" CssClass="date readOnlyStyle" MaxLength="10" 
                                        Text="" runat="server" ReadOnly="true" TabIndex="-1"/>
                                    </td>
                                    <td class="koumokuMei">
                                        ���s�˗����@
                                    </td>
                                    <td colspan="5">
                                        <span id="spanHosyousyoHakHouhou" runat="server" />
                                    </td>
                                </tr>
                                <!-- 7�s�� -->
                                <tr>
                                    <!-- �����ږ��u�ۏ�J�n���v -->
                                    <td class="koumokuMei">
                                        �����n����
                                    </td>
                                    <td id="TdHosyouKaisiDate" runat="server">
                                        <asp:TextBox ID="TextHosyouKaisiDate" CssClass="date" MaxLength="10" Text="" runat="server" />
                                    </td>
                                    <td class="koumokuMei">
                                        ���i�ݒ��
                                    </td>
                                    <td>
                                        <span id="SpanHosyouSyouhinUmu" runat="server" style="font-weight:bold;font-size:13px;"></span>
                                    </td>
                                    <td class="koumokuMei" style="display:none">
                                        �ۏؗL��
                                    </td>
                                    <td id="TdHosyouUmu" runat="server" style="display:none">
                                        <asp:DropDownList ID="SelectHosyouUmu" CssClass="" Style="display: inline;" runat="server">
                                            <asp:ListItem Value="" Text="" Selected="true"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="�L"></asp:ListItem>
                                        </asp:DropDownList><span id="SpanHosyouUmu" runat="server"></span>
                                    </td>
                                    <td class="koumokuMei">
                                        �ۏ؊���
                                    </td>
                                    <td id="TdHosyouKikan" runat="server">
                                        <asp:TextBox ID="TextHosyouKikan" Style="width: 40px" CssClass="number"
                                            Text="" MaxLength="2" runat="server" TabIndex="10" />�N
                                    </td>
                                    <td class="koumokuMei">
                                        �ۏ؏��^�C�v
                                    </td>
                                    <td id="TdHosyousyoType" runat="server">
                                        <asp:TextBox ID="TextHosyousyoType" CssClass="readOnlyStyle" Style="width: 60px"
                                            Text="" runat="server" ReadOnly="true" TabIndex="-1" />
                                    </td>
                                </tr>
                                <!-- 8�s�� -->
                                <tr>
                                    <td class="koumokuMei">
                                        �ۏ؂Ȃ����R
                                    </td>
                                    <td colspan="5" runat="server">
                                        <asp:DropDownList ID="SelectHosyouNasiRiyuu" Style="width: 300px" runat="server">
                                        </asp:DropDownList><span id="SpanHosyouNasiRiyuu" runat="server"></span>
                                        <asp:DropDownList ID="SelectHannyouNo" Style="display: none;" runat="server" TabIndex="-1">
                                        </asp:DropDownList><asp:TextBox ID="TextHosyouNasiRiyuu" Style="width: 200px; ime-mode: active;" runat="server"
                                            MaxLength="40"/>
                                    </td>
                                    <td class="koumokuMei">
                                        (����)���������s��
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextTyousaSeikyuusyoHakkouDate" CssClass="date readOnlyStyle" Text=""
                                            MaxLength="10" Style="width: 70px" ReadOnly="true" TabIndex="-1" runat="server" />
                                    </td>
                                </tr>
                                <!-- 9�s�� -->
                                <tr>
                                    <td class="koumokuMei">
                                        �ی����
                                    </td>
                                    <td colspan="3">
                                        <span id="SpanHkHokenGaisya" runat="server" style="font-size:13px"></span>
                                    </td>
                                    <td class="koumokuMei">
                                        ���n���O�ی�
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextHwMaeHkn" CssClass="readOnlyStyle" Style="width: 20px"
                                            Text="" MaxLength="1" runat="server" ReadOnly="true" TabIndex="-1" />
                                    </td>
                                    <td class="koumokuMei">
                                        ���n����ی�
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextHwAtohkn" CssClass="readOnlyStyle" Style="width: 20px"
                                            Text="" MaxLength="1" runat="server" ReadOnly="true" TabIndex="-1" />
                                    </td>
                                </tr>
                                <!-- 10�s�� -->
                                <tr>
                                    <td colspan="8" style="padding: 0px;">
                                        <table class="innerTable" cellpadding="0" cellspacing="0">
                                            <tr class="firstRow">
                                                <td class="koumokuMei firstCol" style="width: 80px">
                                                    ���z
                                                </td>
                                                <td class="shouhinTableTitleSum">
                                                    �������������v���z
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextTyousaHattyuusyoGoukeiKingaku" CssClass="kingaku readOnlyStyle"
                                                        MaxLength="7" Text="0" Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                                <td class="shouhinTableTitleSum">
                                                    �������v�����z(�ō�)
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextTyousaGoukeiNyuukingaku" CssClass="kingaku readOnlyStyle" MaxLength="7"
                                                        Text="0" Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                                <td class="shouhinTableTitleNyuukin">
                                                    �c�z
                                                </td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="TextTyousaZangaku" CssClass="kingaku readOnlyStyle" MaxLength="7"
                                                        Text="0" Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <!-- 11�s�� -->
                                <tr class="tableSpacer">
                                    <td colspan="8">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="koumokuMei">
                                        �u���H��
                                    </td>
                                    <td colspan="7" style="padding: 0px">
                                        <table class="subTable" style="font-weight: bold;">
                                            <tr>
                                                <td>
                                                    �ʐ^�󗝁F
                                                    <asp:TextBox ID="TextSyasinJuri" runat="server" CssClass="readOnlyStyle" Style="width: 80px"
                                                        Text="" ReadOnly="true" TabIndex="-1" />
                                                </td>
                                                <td>
                                                    �ʐ^�R�����g�F
                                                    <asp:TextBox ID="TextSyasinComment" runat="server" CssClass="readOnlyStyle" Style="width: 500px"
                                                        Text="" ReadOnly="true" TabIndex="-1" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr class="tableSpacer">
                                    <td colspan="8">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="koumokuMei">
                                        ���i�P
                                    </td>
                                    <td colspan="7">
                                        <asp:TextBox ID="TextSyouhin1A" CssClass="itemCd readOnlyStyle" ReadOnly="true" Text=""
                                            runat="server" TabIndex="-1" />
                                        <asp:TextBox ID="TextSyouhin1B" CssClass="itemNm readOnlyStyle" ReadOnly="true" Text=""
                                            runat="server" Style="width: 300px" TabIndex="-1" />
                                    </td>
                                </tr>
                                <tr id="TrSyouhin21" runat="server">
                                    <td class="koumokuMei">
                                        ���i�Q_�P
                                    </td>
                                    <td colspan="7">
                                        <asp:TextBox ID="TextSyouhin21A" CssClass="itemCd readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" TabIndex="-1" />
                                        <asp:TextBox ID="TextSyouhin21B" CssClass="itemNm readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" Style="width: 300px" TabIndex="-1" />
                                    </td>
                                </tr>
                                <tr id="TrSyouhin22" runat="server">
                                    <td class="koumokuMei">
                                        ���i�Q_�Q
                                    </td>
                                    <td colspan="7">
                                        <asp:TextBox ID="TextSyouhin22A" CssClass="itemCd readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" TabIndex="-1" />
                                        <asp:TextBox ID="TextSyouhin22B" CssClass="itemNm readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" Style="width: 300px" TabIndex="-1" />
                                    </td>
                                </tr>
                                <tr id="TrSyouhin23" runat="server">
                                    <td class="koumokuMei">
                                        ���i�Q_�R
                                    </td>
                                    <td colspan="7">
                                        <asp:TextBox ID="TextSyouhin23A" CssClass="itemCd readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" TabIndex="-1" />
                                        <asp:TextBox ID="TextSyouhin23B" CssClass="itemNm readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" Style="width: 300px" TabIndex="-1" />
                                    </td>
                                </tr>
                                <tr id="TrSyouhin24" runat="server">
                                    <td class="koumokuMei">
                                        ���i�Q_�S
                                    </td>
                                    <td colspan="7">
                                        <asp:TextBox ID="TextSyouhin24A" CssClass="itemCd readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" TabIndex="-1" />
                                        <asp:TextBox ID="TextSyouhin24B" CssClass="itemNm readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" Style="width: 300px" TabIndex="-1" />
                                    </td>
                                </tr>
                                <tr id="TrSyouhin31" runat="server">
                                    <td class="koumokuMei">
                                        ���i�R_�P
                                    </td>
                                    <td colspan="7">
                                        <asp:TextBox ID="TextSyouhin31A" CssClass="itemCd readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" TabIndex="-1" />
                                        <asp:TextBox ID="TextSyouhin31B" CssClass="itemNm readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" Style="width: 300px" TabIndex="-1" />
                                    </td>
                                </tr>
                                <tr id="TrSyouhin32" runat="server">
                                    <td class="koumokuMei">
                                        ���i�R_�Q
                                    </td>
                                    <td colspan="7">
                                        <asp:TextBox ID="TextSyouhin32A" CssClass="itemCd readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" TabIndex="-1" />
                                        <asp:TextBox ID="TextSyouhin32B" CssClass="itemNm readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" Style="width: 300px" TabIndex="-1" />
                                    </td>
                                </tr>
                                <tr id="TrSyouhin33" runat="server">
                                    <td class="koumokuMei">
                                        ���i�R_�R
                                    </td>
                                    <td colspan="7">
                                        <asp:TextBox ID="TextSyouhin33A" CssClass="itemCd readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" TabIndex="-1" />
                                        <asp:TextBox ID="TextSyouhin33B" CssClass="itemNm readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" Style="width: 300px" TabIndex="-1" />
                                    </td>
                                </tr>
                                <tr id="TrSyouhin34" runat="server">
                                    <td class="koumokuMei">
                                        ���i�R_�S
                                    </td>
                                    <td colspan="7">
                                        <asp:TextBox ID="TextSyouhin34A" CssClass="itemCd readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" TabIndex="-1" />
                                        <asp:TextBox ID="TextSyouhin34B" CssClass="itemNm readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" Style="width: 300px" TabIndex="-1" />
                                    </td>
                                </tr>
                                <tr id="TrSyouhin35" runat="server">
                                    <td class="koumokuMei">
                                        ���i�R_�T
                                    </td>
                                    <td colspan="7">
                                        <asp:TextBox ID="TextSyouhin35A" CssClass="itemCd readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" TabIndex="-1" />
                                        <asp:TextBox ID="TextSyouhin35B" CssClass="itemNm readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" Style="width: 300px" TabIndex="-1" />
                                    </td>
                                </tr>
                                <tr id="TrSyouhin36" runat="server">
                                    <td class="koumokuMei">
                                        ���i�R_�U
                                    </td>
                                    <td colspan="7">
                                        <asp:TextBox ID="TextSyouhin36A" CssClass="itemCd readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" TabIndex="-1" />
                                        <asp:TextBox ID="TextSyouhin36B" CssClass="itemNm readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" Style="width: 300px" TabIndex="-1" />
                                    </td>
                                </tr>
                                <tr id="TrSyouhin37" runat="server">
                                    <td class="koumokuMei">
                                        ���i�R_�V
                                    </td>
                                    <td colspan="7">
                                        <asp:TextBox ID="TextSyouhin37A" CssClass="itemCd readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" TabIndex="-1" />
                                        <asp:TextBox ID="TextSyouhin37B" CssClass="itemNm readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" Style="width: 300px" TabIndex="-1" />
                                    </td>
                                </tr>
                                <tr id="TrSyouhin38" runat="server">
                                    <td class="koumokuMei">
                                        ���i�R_�W
                                    </td>
                                    <td colspan="7">
                                        <asp:TextBox ID="TextSyouhin38A" CssClass="itemCd readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" TabIndex="-1" />
                                        <asp:TextBox ID="TextSyouhin38B" CssClass="itemNm readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" Style="width: 300px" TabIndex="-1" />
                                    </td>
                                </tr>
                                <tr id="TrSyouhin39" runat="server">
                                    <td class="koumokuMei">
                                        ���i�R_�X
                                    </td>
                                    <td colspan="7">
                                        <asp:TextBox ID="TextSyouhin39A" CssClass="itemCd readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" TabIndex="-1" />
                                        <asp:TextBox ID="TextSyouhin39B" CssClass="itemNm readOnlyStyle" ReadOnly="true"
                                            Text="" runat="server" Style="width: 300px" TabIndex="-1" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <br />
                    <div id="divKairyouKouji" runat="server">
                        <!-- ��ʒ������E���ǍH�� -->
                        <table style="text-align: left; width: 100%;" id="Table1" class="mainTable" cellpadding="0"
                            cellspacing="0" border="1">
                            <!-- �w�b�_�� -->
                            <thead>
                                <tr>
                                    <th class="tableTitle" style="padding: 0px" colspan="8">
                                        <a id="AncKairyouKouji" runat="server">���ǍH��</a>
                                        <input type="hidden" id="HiddenKairyouKoujiStyle" runat="server" value="inline" />
                                    </th>
                                </tr>
                            </thead>
                            <tbody id="TbodyKairyoKouji" runat="server">
                                <!-- 1�s�� -->
                                <tr>
                                    <td class="koumokuMei" style="width: 60px;">
                                        �����
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextHanteisya" Style="width: 150px" CssClass="readOnlyStyle" Text=""
                                            ReadOnly="true" TabIndex="-1" runat="server" />
                                    </td>
                                    <td class="koumokuMei" style="width: 70px;">
                                        ������
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextHanteiSyubetu" Style="width: 150px" CssClass="readOnlyStyle"
                                            Text="" ReadOnly="true" TabIndex="-1" runat="server" />
                                    </td>
                                    <td class="koumokuMei" style="width: 60px;">
                                        ����
                                    </td>
                                    <td colspan="3" style="width: 350px">
                                        <span id="SpanHantei1" runat="server" style="width: 150px" class="readOnlyStyle"></span>
                                        <span id="SpanHanteiSetuzokuMoji" runat="server" style="width: 50px" class="readOnlyStyle">
                                        </span><span id="SpanHantei2" runat="server" style="width: 150px" class="readOnlyStyle">
                                        </span>
                                    </td>
                                </tr>
                                <!-- 2�s�� -->
                                <tr>
                                    <td colspan="8" style="padding: 0px;">
                                        <table class="innerTable" cellpadding="0" cellspacing="0">
                                            <tr class="firstRow">
                                                <!-- 1�s�� -->
                                                <td style="width: 80px" class="koumokuMei firstCol">
                                                    �H�����
                                                </td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="TextKoujiGaisya" Style="width: 100px" CssClass="readOnlyStyle" Text=""
                                                        ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                                <td class="koumokuMei">
                                                    ���ǍH�����
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="TextKairyouKoujiSyubetu" CssClass="readOnlyStyle" Style="width: 150px;
                                                        size: 60;" Text="" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                            </tr>
                                            <!-- 2�s�� -->
                                            <tr>
                                                <td class="koumokuMei firstCol">
                                                    ���ǍH����
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextKairyouKoujiDate" CssClass="date readOnlyStyle" MaxLength="10"
                                                        Text="" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                                <td class="koumokuMei">
                                                    (�H��)���������s��
                                                </td>
                                                <td colspan="4" style="width: 300px">
                                                    <asp:TextBox ID="TextKoujiSeikyusyoHakkouDate" CssClass="date readOnlyStyle" MaxLength="10"
                                                        Text="" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                            </tr>
                                            <!-- 3�s�� -->
                                            <tr>
                                                <td style="width: 120px" class="koumokuMei firstCol">
                                                    �H���񍐏���
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextKoujiHoukokusyoJuri" Style="width: 90%" CssClass="readOnlyStyle"
                                                        Text="" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                                <td class="koumokuMei">
                                                    �H���񍐏��󗝓�
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextKoujiHoukokusyoJuriDate" CssClass="date readOnlyStyle" MaxLength="10"
                                                        Text="" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                                <td class="koumokuMei">
                                                    �H���񍐏�������
                                                </td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="TextKoujiHoukokusyoHassouDate" CssClass="date readOnlyStyle" MaxLength="10"
                                                        Text="" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <!-- 4�s�� -->
                                <tr>
                                    <td colspan="8" style="padding: 0px;">
                                        <table class="innerTable" cellpadding="0" cellspacing="0">
                                            <tr class="firstRow">
                                                <td class="koumokuMei firstCol" style="width: 80px">
                                                    ���z
                                                </td>
                                                <td class="shouhinTableTitleSum">
                                                    �H�����������v���z
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextKoujiHattyuusyoGoukeiKingaku" CssClass="kingaku readOnlyStyle"
                                                        MaxLength="7" Text="0" Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                                <td class="shouhinTableTitleSum">
                                                    �H�����v�����z(�ō�)
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextKoujiGoukeiNyuukingaku" CssClass="kingaku readOnlyStyle" MaxLength="7"
                                                        Text="0" Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                                <td class="shouhinTableTitleNyuukin">
                                                    �c�z
                                                </td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="TextKoujiZangaku" CssClass="kingaku readOnlyStyle" MaxLength="7"
                                                        Text="0" Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr class="tableSpacer">
                                    <td colspan="8">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="koumokuMei">
                                        �H�����i
                                    </td>
                                    <td colspan="7">
                                        <asp:TextBox ID="TextKoujiSyouhinCd" CssClass="itemCd readOnlyStyle" ReadOnly="true"
                                            TabIndex="-1" Text="" runat="server" />
                                        <asp:TextBox ID="TextKoujiSyouhinMei" CssClass="itemNm readOnlyStyle" ReadOnly="true"
                                            TabIndex="-1" Text="" runat="server" Style="width: 300px" />
                                        &nbsp;&nbsp;&nbsp; <span id="SpanKoujiGaisyaSeikyuu" style="color: red; font-weight: bold;"
                                            runat="server"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="koumokuMei">
                                        �ǉ��H�����i
                                    </td>
                                    <td colspan="7">
                                        <asp:TextBox ID="TextTuikaKoujiSyouhinCd" CssClass="itemCd readOnlyStyle" ReadOnly="true"
                                            TabIndex="-1" Text="" runat="server" />
                                        <asp:TextBox ID="TextTuikaKoujiSyouhinMei" CssClass="itemNm readOnlyStyle" ReadOnly="true"
                                            TabIndex="-1" Text="" runat="server" Style="width: 300px" />
                                        &nbsp;&nbsp;&nbsp; <span id="SpanTuikaKoujiKaisyaSeikyuu" style="color: red; font-weight: bold;"
                                            runat="server"></span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <br />
                    <div id="divSaihakkou" runat="server">
                        <!-- ��ʉ����E�Ĕ��s -->
                        <table style="text-align: left; width: 100%;" id="Table3" class="mainTable" cellpadding="0"
                            cellspacing="0">
                            <!-- �w�b�_�� -->
                            <thead>
                                <tr>
                                    <th class="tableTitle" style="padding: 0px" colspan="6">
                                        <a id="AncSaiHakkou" runat="server">�Ĕ��s</a>
                                        <input type="hidden" id="HiddenSaiHakkouStyle" runat="server" value="inline" />
                                    </th>
                                </tr>
                            </thead>
                            <!-- �{�f�B�� -->
                            <tbody id="TbodySaiHakkou" runat="server">
                                <!-- 1�s�� -->
                                <tr id="TrShSaihakkou" runat="server">
                                    <td class="koumokuMei">
                                        �Ĕ��s��
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextSaihakkouDate" CssClass="date" MaxLength="10" Text="" runat="server"
                                            OnTextChanged="TextSaihakkouDate_TextChanged" />
                                    </td>
                                    <td class="koumokuMei">
                                        �Ĕ��s���R
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="TextSaihakkouRiyuu" Style="width: 300px" CssClass="" Text="" runat="server"
                                            MaxLength="40" />
                                        <input type="text" id="TextFocusBounderSaihakkouRiyuu" runat="server" tabindex="-1"
                                            style="width: 0px;" onfocus="setFocusSaihakkouRiyuu();" />
                                    </td>
                                </tr>
                                <tr class="tableSpacer">
                                    <td colspan="6">
                                    </td>
                                </tr>
                                <!-- 2�s��[Table3] -->
                                <tr>
                                    <td colspan="6" style="border: 0px solid red; padding: 0px;">
                                        <table cellpadding="1" class="itemTable innerTable">
                                            <tbody>
                                                <tr class="firstRow">
                                                    <td colspan="8" class="firstCol" style="text-align: left">
                                                        <span id="SpanShUriageSyorizumi" style="color: red; font-weight: bold;" runat="server">
                                                        </span>
                                                        <input type="hidden" id="HiddenShUriageKeijyouDate" runat="server" />
                                                        <uc2:SeikyuuSiireLinkCtrl ID="ucSeikyuuSiireLinkSai" runat="server" />
                                                    </td>
                                                    <td style="text-align: center;" class="shouhinTableTitle">
                                                        ������
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextShSeikyuusaki" Style="width: 80px" CssClass="readOnlyStyle"
                                                            TabIndex="-1" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr class="shouhinTableTitle">
                                                    <td class="firstCol" rowspan="2">
                                                        ����
                                                    </td>
                                                    <td>
                                                        ���i�R�[�h
                                                    </td>
                                                    <td rowspan="2">
                                                        �H���X����<br />
                                                        �Ŕ����z
                                                    </td>
                                                    <td rowspan="2">
                                                        ������<br />
                                                        �Ŕ����z
                                                    </td>
                                                    <td>
                                                        �����
                                                    </td>
                                                    <td rowspan="2">
                                                        ������<br />
                                                        ���s��
                                                    </td>
                                                    <td rowspan="2">
                                                        ����<br />
                                                        �N����
                                                    </td>
                                                    <td rowspan="2">
                                                        ������<br />
                                                        �m��
                                                    </td>
                                                    <td rowspan="2">
                                                        ������<br />
                                                        ���z
                                                    </td>
                                                    <td rowspan="2">
                                                        ������<br />
                                                        �m�F��
                                                    </td>
                                                </tr>
                                                <tr class="shouhinTableTitle">
                                                    <td>
                                                        ���i��
                                                    </td>
                                                    <td>
                                                        �ō����z
                                                    </td>
                                                </tr>
                                                <tr id="TrShSyouhin" runat="server">
                                                    <td class="firstCol" style="width: 40px">
                                                        <asp:DropDownList ID="SelectShSeikyuuUmu" CssClass="" Style="display: inline;" runat="server"
                                                            AutoPostBack="true" OnSelectedIndexChanged="SelectShSeikyuu_SelectedIndexChanged">
                                                            <asp:ListItem Value="" Text="" Selected="true"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="�L"></asp:ListItem>
                                                            <asp:ListItem Value="0" Text="��"></asp:ListItem>
                                                        </asp:DropDownList><span id="SpanShSeikyuuUmu" runat="server"></span>
                                                    </td>
                                                    <td class="itemNm" style="width: 150px">
                                                        <asp:TextBox ID="TextShSyouhinCd" CssClass="itemCd readOnlyStyle" Style="width: 40px"
                                                            ReadOnly="true" Text="" TabIndex="-1" runat="server" />
                                                        <br />
                                                        <span id="SpanShSyouhinMei" class="itemNm" runat="server"></span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextShKoumutenSeikyuuKingaku" CssClass="kingaku" MaxLength="7" Text=""
                                                            Style="width: 60px" runat="server" OnTextChanged="TextShKoumutenSeikyuuKingaku_TextChanged" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextShJituseikyuuKingaku" CssClass="kingaku" MaxLength="7" Text=""
                                                            Style="width: 60px" runat="server" OnTextChanged="TextShJituseikyuuKingaku_TextChanged" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextShSyouhizei" CssClass="kingaku readOnlyStyle" MaxLength="7"
                                                            Text="" Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" /><asp:HiddenField
                                                                ID="HiddenShZeiKbn" runat="server" />
                                                            <asp:HiddenField ID="HiddenShSiireGaku" runat="server" />
                                                            <asp:HiddenField ID="HiddenShSiireSyouhiZei" runat="server" />
                                                        <br />
                                                        <asp:TextBox ID="TextShZeikomiKingaku" CssClass="kingaku readOnlyStyle" MaxLength="7"
                                                            Text="" Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextShSeikyuusyoHakkouDate" CssClass="date" MaxLength="10" Text=""
                                                            runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextShUriageNengappi" CssClass="date readOnlyStyle" MaxLength="10"
                                                            Text="" ReadOnly="true" TabIndex="-1" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextShHattyuusyoKakutei" Style="width: 60px" CssClass="readOnlyStyle"
                                                            Text="" ReadOnly="true" TabIndex="-1" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextShHattyuusyoKingaku" CssClass="kingaku readOnlyStyle" MaxLength="7"
                                                            Text="" Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextShHattyuusyoKakuninDate" CssClass="date readOnlyStyle" MaxLength="10"
                                                            Text="" ReadOnly="true" TabIndex="-1" runat="server" /><asp:HiddenField ID="HiddenShUpdDateTime"
                                                                runat="server" />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <!-- 14�s��[Table4] -->
                                <tr align="center">
                                    <td colspan="6" style="padding: 3px;">
                                        <table class="miniTable" cellpadding="0" cellspacing="0">
                                            <tr class="">
                                                <td colspan="2" class="shouhinTableTitleNyuukin" style="width: 150px;">
                                                    �����z(�ō�)
                                                </td>
                                                <td colspan="2" class="shouhinTableTitleNyuukin" style="width: 150px;">
                                                    �c�z
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="">
                                                    <asp:TextBox ID="TextShNyuukingaku" CssClass="kingaku readOnlyStyle" MaxLength="7"
                                                        Text="0" Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="TextShZangaku" CssClass="kingaku readOnlyStyle" MaxLength="7" Text="0"
                                                        Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr class="tableSpacer">
                                    <td colspan="6">
                                    </td>
                                </tr>
                                <!-- 15�s��[Table5] -->
                                <tr>
                                    <td colspan="6" style="padding: 0px;">
                                        <table cellpadding="0" cellspacing="0" class="itemTable innerTable">
                                            <tbody>
                                                <tr class="firstRow">
                                                    <td colspan="9" class="firstCol" style="text-align: left">
                                                        <span id="SpanKyKaiyakuUriageSyorizumi" style="color: red; font-weight: bold;" runat="server">
                                                        </span>
                                                        <input type="hidden" id="HiddenKyKaiyakuUriageKeijyouDate" runat="server" />
                                                        <uc2:SeikyuuSiireLinkCtrl ID="ucSeikyuuSiireLinkKai" runat="server" />
                                                    </td>
                                                    <td style="text-align: center;" class="shouhinTableTitle">
                                                        ������
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextKySeikyuusaki" Style="width: 80px" CssClass="readOnlyStyle"
                                                            TabIndex="-1" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr class="shouhinTableTitle">
                                                    <td colspan="2" rowspan="2" class="firstCol">
                                                        ��񕥖�<br />
                                                        �\���L��
                                                    </td>
                                                    <td rowspan="2">
                                                        ���i�R�[�h
                                                    </td>
                                                    <td rowspan="2">
                                                        �H���X����<br />
                                                        �Ŕ����z
                                                    </td>
                                                    <td rowspan="2">
                                                        ������<br />
                                                        �Ŕ����z
                                                    </td>
                                                    <td>
                                                        �����
                                                    </td>
                                                    <td rowspan="2">
                                                        ������<br />
                                                        ���s��
                                                    </td>
                                                    <td rowspan="2">
                                                        ����<br />
                                                        �N����
                                                    </td>
                                                    <td rowspan="2">
                                                        ������<br />
                                                        �m��
                                                    </td>
                                                    <td rowspan="2">
                                                        ������<br />
                                                        ���z
                                                    </td>
                                                    <td rowspan="2">
                                                        ������<br />
                                                        �m�F��
                                                    </td>
                                                </tr>
                                                <tr class="shouhinTableTitle">
                                                    <td>
                                                        �ō����z
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" class="firstCol" id="TdKyKaiyakuHaraimodosiSinseiUmu" runat="server">
                                                        <asp:DropDownList ID="SelectKyKaiyakuHaraimodosiSinseiUmu" CssClass="" Style="display: inline;"
                                                            runat="server" AutoPostBack="true" OnSelectedIndexChanged="SelectKyKaiyakuHaraimodosiSinseiUmu_SelectedIndexChanged">
                                                            <asp:ListItem Value="" Selected="true"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="�L"></asp:ListItem>
                                                        </asp:DropDownList><span id="SpanKyKaiyakuHaraimodosiSinseiUmu" runat="server"></span>
                                                    </td>
                                                    <td class="itemNm">
                                                        <asp:TextBox ID="TextKySyouhinCd" CssClass="itemCd readOnlyStyle" ReadOnly="true"
                                                            Text="" Style="width: 40px" TabIndex="-1" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextKyKoumutenSeikyuuKingaku" CssClass="kingaku readOnlyStyle" MaxLength="7"
                                                            Text="" Style="width: 60px" runat="server" ReadOnly="true" TabIndex="-1" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextKyJituseikyuuKingaku" CssClass="kingaku readOnlyStyle" MaxLength="7"
                                                            Text="" Style="width: 60px" runat="server" ReadOnly="true" TabIndex="-1" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextKySyouhizei" CssClass="kingaku readOnlyStyle" MaxLength="7"
                                                            Text="" Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" /><asp:HiddenField
                                                                ID="HiddenKyZeiKbn" runat="server" />
                                                            <asp:HiddenField ID="HiddenKySiireGaku" runat="server" />
                                                            <asp:HiddenField ID="HiddenKySiireSyouhiZei" runat="server" />
                                                        <br />
                                                        <asp:TextBox ID="TextKyZeikomiKingaku" CssClass="kingaku readOnlyStyle" MaxLength="7"
                                                            Text="" Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" />
                                                    </td>
                                                    <td id="TdKySeikyuusyoHakkouDate" runat="server">
                                                        <asp:TextBox ID="TextKySeikyuusyoHakkouDate" CssClass="date" MaxLength="10" Text=""
                                                            runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextKyUriageNengappi" CssClass="date readOnlyStyle" MaxLength="10"
                                                            Text="" ReadOnly="true" TabIndex="-1" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextKyHattyuusyoKakutei" Style="width: 60px" CssClass="readOnlyStyle"
                                                            Text="" ReadOnly="true" TabIndex="-1" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextKyHattyuusyoKingaku" CssClass="kingaku readOnlyStyle" MaxLength="7"
                                                            Text="" Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextKyHattyuusyoKakuninDate" CssClass="date readOnlyStyle" MaxLength="10"
                                                            Text="" ReadOnly="true" TabIndex="-1" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="11" class="firstCol" style="text-align: left">
                                                        <span id="SpanKyHenkinSyorizumi" style="color: blue; font-weight: bold;" runat="server">
                                                        </span>&nbsp;&nbsp; <span id="SpanKyHenkinSyoriDate" style="color: blue; font-weight: bold;"
                                                            runat="server"></span>
                                                        <asp:HiddenField ID="HiddenKyUpdDateTime" runat="server" />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <!-- ��ʉ����E�{�^�� -->
    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tbody>
            <tr>
                <th style="text-align: center;">
                    <input type="button" id="ButtonTouroku2" value="�o�^ / �C�� ���s" style="font-weight: bold;
                        font-size: 18px; width: 155px; color: black; height: 30px; background-color: fuchsia"
                        onclick="return ButtonTourokuSyuuseiJikkou2_onclick()" runat="server" />&nbsp;
                </th>
            </tr>
        </tbody>
    </table>
</asp:Content>

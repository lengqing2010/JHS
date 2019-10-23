<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="MousikomiInput.aspx.vb" Inherits="Itis.Earth.WebUI.MousikomiInput"
    Title="EARTH �\������" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script type="text/javascript">
        history.forward();
    
        //��ʋN�����ɃE�B���h�E�T�C�Y���f�B�X�v���C�ɍ��킹��
        window.moveTo(0, 0);
        window.resizeTo(window.screen.availWidth, window.screen.availHeight);

        //�����X�����������Ăяo��
        function callKameitenSearch(obj){
            objEBI("<%= kameitenSearchType.clientID %>").value = "";
            if(obj.value == ""){
                objEBI("<%= kameitenSearchType.clientID %>").value = "1";
                objEBI("<%= ButtonKITourokuBangou.clientID %>").click();
            }
        }

        //�����X�������������i1����Ăяo��
        function callKameitenSearchFromSyouhin1(obj){
            var objBangou = objEBI("<%= TextKITourokuBangou.clientID %>"); //�o�^�ԍ�(�����X�R�[�h)
            var objSyouhin1 = objEBI("<%= choSyouhin1.clientID %>"); //���i1
            objEBI("<%= kameitenSearchType.clientID %>").value = "";
            if(objSyouhin1.value == ""){
                objEBI("<%= kameitenSearchType.clientID %>").value = "1";
                objEBI("<%= ButtonKITourokuBangou.clientID %>").click();
            }else{
                if(objBangou.value != "" && objSyouhin1.value != ""){
                    objEBI("<%= ButtonKITourokuBangou.clientID %>").click();
                 }
            }
        }

        //�d�������`�F�b�N����
        function ChgTyoufukuBukken(objThis){
            objEBI("<%= HiddenTyoufukuKakuninTargetId.clientID %>").value = objThis.id;
            objEBI("<%= ButtonExeTyoufukuCheck.clientID %>").click();
        }

        //������Ќ����������Ăяo��
        function callTyousakaisyaSearch(obj){
            objEBI("<%= tyousakaisyaSearchType.clientID %>").value = "";
            if(obj.value == ""){
                objEBI("<%= tyousakaisyaSearchType.clientID %>").value = "1";
                objEBI("<%= ButtonSITysGaisya.clientID %>").click();
            }
        }

        //�Z���]�L�{�^���������̏���
        function juushoTenki_onclick() {
            var objJuusho3 = objEBI("<%= TextBukkenJyuusyo3.clientID %>");
            var objBikou = objEBI("<%= TextSIBikou.clientID %>");
            if(objJuusho3.value != ""){
                if(objBikou.value != ""){
                    objBikou.value += " ";
                }
                objBikou.value += "�Z�������F" + objJuusho3.value;
            }
        }
        
        //�v���_�E���̒l������l�̏ꍇ�A�w��̃G�������g��ReadOnly���O���A���ږ����x���̕\����Ԃ�ؑւ���
        function checkSonota(flgVal, targetId){
            if(flgVal){
                objEBI(targetId).style.visibility = "visible";

            }else{
                objEBI(targetId).value = "";
                objEBI(targetId).style.visibility = "hidden";
            }
        }
        
        //��������҂̗L���ɂ���ă`�F�b�N�{�b�N�X�̕\���ؑւ��s�Ȃ�
        function ChgDispTatiaisya(){       
            var objAri = objEBI("<%= RadioTysTatiaisya1.clientID %>");            
            var objSesyusama = objEBI("<%= CheckTysTatiaisyaSesyuSama.clientID %>");
            var objTantousya = objEBI("<%= CheckTysTatiaisyaTantousya.clientID %>");
            var objSonota = objEBI("<%= CheckTysTatiaisyaSonota.clientID %>");

            if(objAri.checked){
                //������
                objSesyusama.disabled = false;
                objTantousya.disabled = false;
                objSonota.disabled = false;
            }else{
                //�`�F�b�N���͂���
                objSesyusama.checked = false;
                objTantousya.checked = false;
                objSonota.checked = false;
                //�񊈐���
                objSesyusama.disabled = true;
                objTantousya.disabled = true;
                objSonota.disabled = true;               
            }
        }
        
        //�敪�ύX������
        function ChgSelectKbn(){           
            var objSelKbn = objEBI("<%= SelectKIKubun.clientID %>"); //�敪
            var objBangou = objEBI("<%= TextKITourokuBangou.clientID %>"); //�o�^�ԍ�(�����X�R�[�h)
            
            if(objBangou.value == ""){
                var objBukkenNm = objEBI("<%= TextBukkenMeisyou.clientID %>"); //��������
                var objBukkenJyuusyo1 = objEBI("<%= TextBukkenJyuusyo1.clientID %>"); //�����Z���P
                var objBukkenJyuusyo2 = objEBI("<%= TextBukkenJyuusyo2.clientID %>"); //�����Z���Q
                if(objBukkenNm.value + objBukkenJyuusyo1.value + objBukkenJyuusyo2 != ""){
                    //�������́A�Z���ɓ��͂�����ꍇ�A�d���`�F�b�N���s
                    ChgTyoufukuBukken(objSelKbn);
               }
               return false;
            }
            
            //�m�FMSG�\��
            var strMSG = "<%= Messages.MSG119C %>";
            strMSG = strMSG.replace('@PARAM1','�敪');
            strMSG = strMSG.replace('@PARAM2','�����X');
            if(confirm(strMSG)){
                //�O�l���Z�b�g
                objEBI("<%= HiddenKIKbnMae.clientID %>").value = objSelKbn.value;
                //�����X�N���A�������s
                objEBI("<%= ButtonKIKameitenClear.clientID %>").click();
            }else{
                objSelKbn.value = objEBI("<%= HiddenKIKbnMae.clientID %>").value;
            }           
        }
        
        //�����T�v�ݒ菈���Ăяo��
        function callSetTysGaiyou(objThis){
            objEBI("<%= actCtrlId.clientID %>").value = objThis.id; //���s�g���K�[�I�u�W�F�N�gID
            //�{�^������
            objEBI("<%= btnSetTysGaiyou.clientID %>").click();
        }
        
        //�o�^�����O�`�F�b�N
        function CheckTouroku(){
            //��b���H�\��� �召�`�F�b�N
            if(!checkDaiSyou(objEBI("<%= TextKsTyakkouYoteiDateFrom.clientID %>") ,objEBI("<%= TextKsTyakkouYoteiDateTo.clientID %>"),"��b���H�\���"))return false;
            
            return true;
        }
        
        /**
         * �召�`�F�b�N
         * @return true/false
         */
        function checkDaiSyou(objFrom,objTo,mess){
            if(objFrom.value != "" && objTo.value != ""){
                if(Number(removeSlash(objFrom.value)) > Number(removeSlash(objTo.value))){
                    alert("<%= Messages.MSG022E %>".replace("@PARAM1",mess));
                    objFrom.select();
                    return false;
                }
            }
            return true;
        }
        
        //�ύX�O�R���g���[���̒l��ޔ����āA�Y���R���g���[��(Hidden)�ɕێ�����
        function SetChangeMaeValue(strTaihiID, strTargetID){
           document.getElementById(strTaihiID).value = document.getElementById(strTargetID).value;
        }
        
        //�ۏ؏�NO�̔ԏ󋵃`�F�b�N
        function checkSaiban(){
            
            //��������ҕ\������
            ChgDispTatiaisya();
        
            var objSaibanButton = objEBI("<%= ButtonTouroku1.clientID %>");
            var objKubun = objEBI("<%= SelectKIKubun.clientID %>");
            
            objSaibanButton.disabled = true;
            //�敪�I���`�F�b�N
            if(objKubun.value == ""){
                alert("<%= Messages.MSG004E %>");
                objKubun.focus();
                objSaibanButton.disabled = false;
                return false;
            }
            
            //�A���������̃`�F�b�N
            var strRentouSuu = "";//�A��������
            var strMsg = "<%= Messages.MSG062E %>";
            strMsg = strMsg.replace('@PARAM1','�A��������');
            strRentouSuu = prompt("\r\n" + strMsg,"");
            if(strRentouSuu == null){ //�L�����Z���{�^��������
                objSaibanButton.disabled = false;
                objSaibanButton.focus();
                return false;
            }
            strRentouSuu = strRentouSuu.z2hDigit();
            if(strRentouSuu == "" || strRentouSuu == undefined){
                strMsg = "<%= Messages.MSG013E %>";
                strMsg = strMsg.replace('@PARAM1','�A��������');
                alert(strMsg);
                objSaibanButton.disabled = false;
                objSaibanButton.focus();
                return false;
            }
            if(strRentouSuu <= 0 || strRentouSuu > 999){
                strMsg = "<%= Messages.MSG111E %>";
                strMsg = strMsg.replace('@PARAM1','�A��������');
                strMsg = strMsg.replace('@PARAM2','1');
                strMsg = strMsg.replace('@PARAM3','999');
                alert(strMsg);
                objSaibanButton.disabled = false;
                objSaibanButton.focus();
                return false;
            }
            if(strRentouSuu >= 21){
                strMsg = "<%= Messages.MSG112C %>";
                strMsg = strMsg.replace('@PARAM1',strRentouSuu + '��');
                if(confirm(strMsg) == false){
                    objSaibanButton.disabled = false;
                    return false;
                }
            }
            
            objSaibanButton.disabled = false;
                
            //�A����������hidden�ɃZ�b�g
            objEBI("<%=HiddenRentouBukkenSuu.clientID %>").value = strRentouSuu;
            
            return true;       
        }
        
        //������\���G���A�̐܂��ݏ���
        function Oritatami(){
            changeDisplay("<%=TrHJ.clientID %>");
            changeDisplay("<%=TrTS.clientID %>");
            changeDisplay("<%=TrSZ.clientID %>");
            changeDisplay("<%=TrTZ.clientID %>");
            changeDisplay("<%=TrSJ.clientID %>");
             
            //�\���ؑ�(+ �� -)
            if(objEBI("<%=TrHJ.clientID %>").style.display == "inline"){
                objEBI('AOritatami').innerHTML = '-';
            }else{
                objEBI('AOritatami').innerHTML = '+';
            }
        }
        
        //onload�㏈��
        function funcAfterOnload(){
            _d = document;
            
            //���ރv���_�E���̒l�\����؂�ւ�
            ChgDispSelectBunrui(1);
            ChgDispSelectBunrui(2);
            ChgDispSelectBunrui(3);
            
            //�A������
            var callRentouNextFlg = objEBI("<%=HiddenCallRentouNextFlg.clientID %>").value;
            
            //���̓`�F�b�N����
            var inputChk = objEBI("<%=HiddenInputChk.clientID %>").value;
            
            //�A�����sFLG�������Ă���ꍇ�A�������s
            if(callRentouNextFlg){
                objEBI("<%=actBtnId.value %>").click();
            }else if(inputChk){
                actClickButton(objEBI("<%=ButtonTouroku1.clientID %>"));
            }
        }
        
        //�e����s�{�^���������̏���
        function actClickButton(obj){
            var objCallRentouNextFlg = objEBI("<%=HiddenCallRentouNextFlg.clientID %>");
            var objHiddenActButtonId = objEBI("<%=actBtnId.clientID %>");
            var syorikensuuButton = objEBI("<%=ButtonDisplaySyoriKensuu.clientID %>");
            var objAccRentouBukkenSuu = objEBI("<%= HiddenRentouBukkenSuu.clientID %>");
            var objAccSyorikenSuu = objEBI("<%= HiddenSyoriKensuu.clientID %>");

            //���̓`�F�b�N����
            var objInputChk = objEBI("<%=HiddenInputChk.clientID %>");
            
            if(objCallRentouNextFlg.value){
                if(objAccRentouBukkenSuu.value > 1){
                    syorikensuuButton.value = "�A�������o�^������....  [ " + objAccSyorikenSuu.value + " / " + objAccRentouBukkenSuu.value + " ] �� ����";
                }
                objHiddenActButtonId.value=obj.id;
                setWindowOverlay(obj,syorikensuuButton);
                objCallRentouNextFlg.value = "";
                //�X�V���������s
                objEBI("<%=ButtonHiddenUpdate.clientID %>").click();
            }else if(objInputChk.value){
                objInputChk.value = "";
                
	            if(checkSaiban()){
                    //�̔ԏ��������s
                    objEBI("<%=ButtonHiddenSaiban.clientID %>").click();
	            }else{
	                return false;
	            }
                
                objHiddenActButtonId.value=obj.id;
                setWindowOverlay(obj);
            }else if(confirm("<%=Messages.MSG017C %>")){
                objInputChk.value = "";
                
                //�o�^�O�`�F�b�N
	            if(CheckTouroku()){
	                //�̔ԏ��������s
                    objEBI("<%=ButtonHiddenInputChk.clientID %>").click();
	            }else{
                    return false;
                }
            }else{
                objHiddenActButtonId.value='';
                return false;
            }
        }
        
        //******************************
        
        //�R���g���[���ړ���
        var gVarOyaSettouji = "ctl00_CPH1_";   
        var gVarSelectTmpSyubetu = "SelectSyubetu_";      
        
        // ���ރv���_�E���̒l�\����؂�ւ�
        function ChgDispSelectBunrui(prmCtrlFlg){
            var objSelSyubetu = null;
            var objSelBunrui = null;
            
            //���������̑ΏۃR���g���[���𔻒f
            switch (prmCtrlFlg){
              case 1:
                objSelSyubetu = objEBI("<%=SelectBRSyubetu1.clientID %>");
                objSelBunrui = objEBI("SelectBRBunrui1");
                break;
              case 2:
                objSelSyubetu = objEBI("<%=SelectBRSyubetu2.clientID %>");
                objSelBunrui = objEBI("SelectBRBunrui2");
                break;
              case 3:
                objSelSyubetu = objEBI("<%=SelectBRSyubetu3.clientID %>");
                objSelBunrui = objEBI("SelectBRBunrui3");
                break;
              default:
                break;
            }
            
            //���ރh���b�v�_�E���񊈐���
            if(objSelSyubetu.disabled){
                objSelBunrui.disabled = true;
            }else{
                objSelBunrui.disabled = false;
            }
                        
            //�I�v�V�����S�폜
            SelectOptionDelete(objSelBunrui);
            
            if(objSelSyubetu.value == '') return false;
            
            //���ނ̑��݃`�F�b�N
            if(ChkExitSelectCode(objSelSyubetu.value) == false){
                var strMSG = "<%= Messages.MSG113E %>";
                strMSG = strMSG.replace('@PARAM1','����');
                alert(strMSG);
                return false;
            }

            //�I�v�V�����}��
            SelectOptionInsert(objSelBunrui,objSelSyubetu.value);
            
            //�I�v�V�����Z�b�g
            SelectOptionSet(objSelBunrui,prmCtrlFlg);
                          
        }

        //SelectCode�̃I�v�V������S�폜����
        function SelectOptionDelete(objSel){
	        while(objSel.lastChild){
	            objSel.removeChild(objSel.lastChild);
	        }
        }
        
        //��ʂɑΉ�����R�[�h�����݂��邩�`�F�b�N����
        function ChkExitSelectCode(intFlg){           
            if(intFlg == '') return false;
            var varMoto = gVarOyaSettouji + gVarSelectTmpSyubetu + intFlg;
	        var objMoto = objEBI(varMoto);
            var len = objMoto.length;            
            if(len <= 0) return false;
            return true;            
        }
        
        //SelectCode�̃I�v�V������ǉ�����
        function SelectOptionInsert(objSel,intFlg){      
            if(intFlg == '') return false;
            var varMoto = gVarOyaSettouji + gVarSelectTmpSyubetu + intFlg;
	        var objMoto = objEBI(varMoto);
            var len = objMoto.length;
            var varCnt = 1;
            var varIndex = 1;         
            for(varCnt=1; varCnt<len; varCnt++){
                varValue = objMoto.options[varCnt].value;
                varText = objMoto.options[varCnt].text;
                objSel.options[varIndex] = new Option(varText,varValue);
                varIndex++;
            }           
        }
        
        //SelectCode�̃I�v�V�������Z�b�g����
        function SelectOptionSet(objSel,prmCtrlFlg){
            var objHdnBunrui = null;
            if(objSel == undefined) return false;
            
            //���������̑ΏۃR���g���[���𔻒f
            switch (prmCtrlFlg){
              case 1:
                objHdnBunrui = objEBI("<%=HiddenBRBunrui1.clientID %>");
                break;
              case 2:
                objHdnBunrui = objEBI("<%=HiddenBRBunrui2.clientID %>");
                break;
              case 3:
                objHdnBunrui = objEBI("<%=HiddenBRBunrui3.clientID %>");
                break;
              default:
                break;
            }
            if(objHdnBunrui == undefined) return false;
            
            //Hidden�̎擾����уZ�b�g
            objSel.value = objHdnBunrui.value;
        }
        
        //���ރh���b�v�_�E�����X�g�ύX��Hidden���ނ��X�V����
        function UpdHiddenBunrui(objSel,prmCtrlFlg){        
            var objHdnBunrui = null;           
            if(objSel == undefined) return false;
            
            //���������̑ΏۃR���g���[���𔻒f
            switch (prmCtrlFlg){
              case 1:
                objHdnBunrui = objEBI("<%=HiddenBRBunrui1.clientID %>");
                break;
              case 2:
                objHdnBunrui = objEBI("<%=HiddenBRBunrui2.clientID %>");
                break;
              case 3:
                objHdnBunrui = objEBI("<%=HiddenBRBunrui3.clientID %>");
                break;
              default:
                break;
            }
            
            if(objHdnBunrui == undefined) return false;
            
            if(objSel.value == ''){
                objHdnBunrui.value = '';
            }else{
                objHdnBunrui.value = objSel.value;
            }
        }
        
        //��ʕύX���A���ނ̒��g�����ς���
        function SelectSyubetuOnChg(objSel,prmCtrlFlg){
            var objHdnBunrui = null;
            var objSelSyubetu = null;
            var objSelBunrui = null;
            
            //���������̑ΏۃR���g���[���𔻒f
            switch (prmCtrlFlg){
              case 1:
                objHdnBunrui = objEBI("<%=HiddenBRBunrui1.clientID %>");
                objSelSyubetu = objEBI("<%=SelectBRSyubetu1.clientID %>");
                objSelBunrui = objEBI("SelectBRBunrui1");
                break;
              case 2:
                objHdnBunrui = objEBI("<%=HiddenBRBunrui2.clientID %>");
                objSelSyubetu = objEBI("<%=SelectBRSyubetu2.clientID %>");
                objSelBunrui = objEBI("SelectBRBunrui2");
                break;
              case 3:
                objHdnBunrui = objEBI("<%=HiddenBRBunrui3.clientID %>");
                objSelSyubetu = objEBI("<%=SelectBRSyubetu3.clientID %>");
                objSelBunrui = objEBI("SelectBRBunrui3");
                break;
              default:
                break;
            }
                     
            //Hidden���ނ̏�����
            if(objHdnBunrui == undefined) return false;
            objHdnBunrui.value = '';
            
            //�I�u�W�F�N�g�̎擾
            if(objSelSyubetu == undefined) return false;
            
            //�I�u�W�F�N�g�̎擾
            if(objSelBunrui == undefined) return false;
            
            //�I�v�V�����S�폜
            SelectOptionDelete(objSelBunrui);
            
            if(objSelSyubetu.value == '') return false;
            //���ނ̑��݃`�F�b�N
            if(ChkExitSelectCode(objSelSyubetu.value) == false){
                var strMSG = "<%= Messages.MSG113E %>";
                strMSG = strMSG.replace('@PARAM1','����');
                alert(strMSG);
                return false;
            }
            
            //�I�v�V�����}��
            SelectOptionInsert(objSelBunrui,objSelSyubetu.value);
        }
        
    </script>

    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <input type="hidden" id="gamenId" value="shinki" runat="server" />
    <input type="hidden" id="actBtnId" runat="server" />
    <input type="hidden" id="st" runat="server" />
    <input type="hidden" id="tourokuKanryouFlg" runat="server" />
    <input type="hidden" id="callModalFlg" runat="server" />
    <input type="hidden" id="HiddenCallRentouNextFlg" runat="server" />
    <input type="button" id="ButtonDisplaySyoriKensuu" class="SyoriKensuuMessageButton"
        runat="server" tabindex="-1" onfocus="window.focus();" style="display: none;"
        value="�������E�E�E" />
    <input type="hidden" id="HiddenSyoriKensuu" runat="server" value="0" />
    <input type="hidden" id="HiddenInputChk" runat="server" />
    <!-- �����T�v�ݒ菈���Ăяo�� -->
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server" RenderMode="Inline">
        <ContentTemplate>
            <input type="button" id="btnSetTysGaiyou" value="�����T�v�ݒ�" style="display: none" runat="server" />
            <input type="hidden" id="actCtrlId" runat="server" />
            <input type="hidden" id="actPreVal" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <div>
        <input type="hidden" id="HiddenRentouBukkenSuu" runat="server" /><%-- �A�������� --%>
        <input type="hidden" id="HiddenSentouBangou" runat="server" /><%-- �擪�ԍ� --%>
        <div id="divSelect" runat="server">
        </div>
        <asp:DropDownList runat="server" ID="SelectTmpCode" Style="display: none;">
        </asp:DropDownList>
        <!-- �\���� -->
        <!-- ��ʃ^�C�g�� -->
        <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="1"
            class="titleTable">
            <tbody>
                <tr>
                    <th>
                        �\������</th>
                    <th style="text-align: left;">
                        <asp:TextBox ID="TextBangou" Style="width: 260px" CssClass="readOnlyStyle" runat="server"
                            ReadOnly="true" TabIndex="-1" />&nbsp;
                        <input type="button" id="ButtonTouroku1" value="�o�^ ���s" style="font-weight: bold;
                            font-size: 18px; width: 155px; color: black; height: 30px; background-color: fuchsia"
                            runat="server" tabindex="10" />
                        <input id="ButtonHiddenInputChk" style="display: none;" type="button" value="���̓`�F�b�Nhid"
                            runat="server" />
                        <input id="ButtonHiddenSaiban" style="display: none;" type="button" value="�̔�hid"
                            runat="server" />
                        <input id="ButtonHiddenUpdate" style="display: none;" type="button" value="�n��T�X�Vhid"
                            runat="server" />
                        <input id="ButtonSinkiHikitugi" value="�V�K(���p)�\��" type="button" runat="server" style="font-weight: bold;
                            font-size: 18px; width: 180px; color: black; height: 30px; background-color: fuchsia"
                            tabindex="10" />&nbsp;
                        <input id="ButtonSinki" value="�V�K�\��" type="button" runat="server" style="font-weight: bold;
                            font-size: 18px; width: 100px; color: black; height: 30px; background-color: fuchsia"
                            tabindex="10" />
                    </th>
                </tr>
            </tbody>
        </table>
        <asp:UpdatePanel ID="UpdatePanelHoll" UpdateMode="conditional" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <input type="hidden" runat="server" id="HiddenDateYesterday" /><%--�O��--%>
                <input type="hidden" runat="server" id="HiddenDateToday" /><%--����--%>
                <input type="hidden" id="HiddenAjaxFlg" runat="server" /><%--Ajax�������t���O--%>
                <table>
                    <tr>
                        <td>
                            <!-- ��ʏ㕔 -->
                            <table style="text-align: left; width: 100%;" id="" class="" cellpadding="1">
                                <tr>
                                    <td style="vertical-align: bottom;">
                                        <!-- ��ʍ��㕔 -->
                                        <table style="text-align: left; width: 100%;" id="TableIraiDate" class="mainTable"
                                            cellpadding="0">
                                            <tr>
                                                <td class="koumokuMei" style="width: 100px">
                                                    �˗���
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="TextIraiDate" CssClass="hissu date" MaxLength="10" runat="server"
                                                        Style="width: 70px" TabIndex="10" />
                                                    <input type="button" id="ButtonIraiDateYestarday" style="width: 37px" value="�O��"
                                                        class="gyoumuSearchBtn" runat="server" tabindex="10" />
                                                    <input type="button" id="ButtonIraiDateToday" style="width: 37px" value="����" class="gyoumuSearchBtn"
                                                        runat="server" tabindex="10" />
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <!-- ��ʍ��㕔[�ړ���:Tys] -->
                                        <table style="text-align: left; width: 100%;" id="TableTys" class="mainTable" cellpadding="1">
                                            <thead>
                                                <tr>
                                                    <th class="tableTitle" colspan="4" style="width: 100%; text-align: center;">
                                                        �����A����
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td class="koumokuMei">
                                                        �����S�����
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="TextTysJitumuTantouGaisya" CssClass="readOnlyStyle" ReadOnly="true"
                                                            TabIndex="-1" runat="server" Style="width: 250px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="koumokuMei">
                                                        �Z��
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="TextTysJyuusyo" runat="server" Style="width: 250px" MaxLength="60"
                                                            TabIndex="10" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="koumokuMei">
                                                        TEL
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextTysTel" runat="server" Style="width: 125px" CssClass="codeNumber"
                                                            MaxLength="20" TabIndex="10" />
                                                    </td>
                                                    <td class="koumokuMei" style="width: 40px">
                                                        FAX
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextTysFax" runat="server" Style="width: 125px" CssClass="codeNumber"
                                                            MaxLength="20" TabIndex="10" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="koumokuMei">
                                                        �S����
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextTysTantousya" runat="server" Style="width: 120px" CssClass="readOnlyStyle"
                                                            ReadOnly="true" TabIndex="-1" />
                                                    </td>
                                                    <td class="koumokuMei" style="width: 40px">
                                                        �g��
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextTysKeitai" runat="server" Style="width: 100px" CssClass="readOnlyStyle number"
                                                            ReadOnly="true" TabIndex="-1" />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                    <td style="text-align: right">
                                        <table style="text-align: left; width: 230px; border-bottom-style: none;" id="TableKeiyakuNo"
                                            class="mainTable" cellpadding="1">
                                            <tr>
                                                <td class="koumokuMei" style="width: 60px; border-bottom-style: none;">
                                                    �_��NO</td>
                                                <td style="border-bottom-style: none;">
                                                    <asp:TextBox ID="TextKeiyakuNo" Style="width: 150px; ime-mode: inactive;" runat="server"
                                                        CssClass="codeNumber" MaxLength="20" TabIndex="10" />
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:UpdatePanel ID="UpdatePanelKameiten" UpdateMode="conditional" runat="server"
                                            RenderMode="Inline" ChildrenAsTriggers="true">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="TextKITourokuBangou" />
                                                <asp:AsyncPostBackTrigger ControlID="ButtonKITourokuBangou" />
                                                <asp:AsyncPostBackTrigger ControlID="ButtonKIKameitenClear" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <input type="hidden" runat="server" id="HiddenKIKbnMae" /><%--�敪�E�ύX�O--%>
                                                <input type="hidden" id="HiddenKITourokuBangouMae" runat="server" /><%--�����X�R�[�h�E�ύX�O--%>
                                                <input type="hidden" runat="server" id="HiddenKameitenClearFlg" /><%--�����X�N���A�t���O--%>
                                                <input type="hidden" runat="server" id="HiddenHosyousyoHakkouJyoukyou" /><%--�ۏ؏����s��--%>
                                                <input type="hidden" runat="server" id="HiddenHosyousyoHakkouJyoukyouSetteiDate" /><%--�ۏ؏����s�󋵐ݒ��--%>
                                                <input type="hidden" runat="server" id="HiddenHosyouKikan" /><%--�ۏ؊���--%>
                                                <input type="hidden" runat="server" id="HiddenKjGaisyaSeikyuuUmu" /><%--�H����А����L��--%>
                                                <input type="hidden" runat="server" id="HiddenKameitenTyuuiJikou" /><%--�����X���ӎ���--%>
                                                <!-- ��ʉE�㕔�E�����X���[�ړ���:KI] -->
                                                <table style="text-align: left; width: 100%;" id="TableKameiten" class="mainTable"
                                                    cellpadding="1">
                                                    <tr>
                                                        <td class="koumokuMei" style="width: 100px">
                                                            �敪
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:DropDownList ID="SelectKIKubun" runat="server" CssClass="hissu" TabIndex="10">
                                                            </asp:DropDownList><span id="SpanKIKubun" runat="server"></span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="koumokuMei" style="width: 100px">
                                                            �o�^�ԍ�
                                                        </td>
                                                        <td>
                                                            <asp:HiddenField ID="saveCdOrderStop" runat="server" />
                                                            <asp:TextBox ID="TextKITourokuBangou" Style="width: 40px" CssClass="hissu codeNumber"
                                                                runat="server" MaxLength="5" TabIndex="10" />
                                                            <input type="button" id="ButtonKITourokuBangou" value="����" class="gyoumuSearchBtn"
                                                                runat="server" tabindex="10" />
                                                            <input type="hidden" id="kameitenSearchType" runat="server" />
                                                            <input type="button" id="ButtonKIKameitenClear" value="�N���A" class="" style="display: none;"
                                                                runat="server" onserverclick="ButtonKIKameitenClear_ServerClick" />
                                                            <span id="SpanKIJioSaki" runat="server" style="color: Red; font-weight: bold;"></span>
                                                        </td>
                                                        <td class="koumokuMei" style="width: 60px">
                                                            �S����
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextKITantousya" Style="width: 80px" runat="server" MaxLength="10"
                                                                TabIndex="10" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="koumokuMei">
                                                            �Ж�
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="TextKISyamei" Style="width: 250px" CssClass="readOnlyStyle" runat="server"
                                                                ReadOnly="true" TabIndex="-1" />
                                                            <input type="button" id="ButtonKIKameitenTyuuijouhou" value="���ӏ��" class="btnKameitenTyuuijouhou"
                                                                runat="server" tabindex="10" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="koumokuMei">
                                                            �Z��
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="TextKIJyuusyo" Style="width: 330px" CssClass="readOnlyStyle" runat="server"
                                                                ReadOnly="true" TabIndex="-1" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="koumokuMei">
                                                            TEL
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextKITel" Style="width: 100px" CssClass="readOnlyStyle codeNumber"
                                                                runat="server" ReadOnly="true" TabIndex="-1" />
                                                        </td>
                                                        <td class="koumokuMei" style="width: 40px">
                                                            FAX
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextKIFax" Style="width: 100px" CssClass="readOnlyStyle codeNumber"
                                                                runat="server" ReadOnly="true" TabIndex="-1" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="koumokuMei">
                                                            �S���Ҍg��
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="TextKITantousyaKeitai" Style="width: 100px" CssClass="readOnlyStyle codeNumber"
                                                                runat="server" ReadOnly="true" TabIndex="-1" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <!-- ��ʃ��C�� -->
                            <table style="text-align: left; width: 100%;" id="TableMain" class="mainTable" cellpadding="1">
                                <tr>
                                    <td class="koumokuMei" style="width: 100px">
                                        ��������</td>
                                    <td>
                                        <asp:TextBox ID="TextBukkenMeisyou" Style="width: 27em" runat="server" CssClass="hissu"
                                            MaxLength="50" TabIndex="10" />�l �@
                                    </td>
                                    <td class="koumokuMei">
                                        �{�喼
                                    </td>
                                    <td>
                                        <input type="radio" name="RadioSesyumei" id="RadioSesyumei1" runat="server"
                                            tabindex="10" class="hissu"/>�L<span id="SpanSesyumei1" runat="server"></span>
                                        <input type="radio" name="RadioSesyumei" id="RadioSesyumei0" runat="server"
                                            tabindex="10" class="hissu"/>��<span id="SpanSesyumei0" runat="server"></span>
                                    </td>
                                    <td class="koumokuMei">
                                        ���񓯎��˗�����</td>
                                    <td>
                                        <asp:TextBox ID="TextDoujiIraiTousuu" Style="width: 40px" runat="server" CssClass="hissu number"
                                            MaxLength="4" TabIndex="10" />��
                                    </td>
                                </tr>
                                <tr>
                                    <td class="koumokuMei">
                                        �����Z��</td>
                                    <td colspan="5">
                                        �P�F<asp:TextBox ID="TextBukkenJyuusyo1" Style="width: 17em" runat="server" CssClass="hissu"
                                            MaxLength="32" TabIndex="10" />
                                        �Q�F<asp:TextBox ID="TextBukkenJyuusyo2" Style="width: 17em" runat="server" CssClass=""
                                            MaxLength="32" TabIndex="10" />
                                        <asp:UpdatePanel ID="UpdatePanelTyoufukuCheck" UpdateMode="conditional" runat="server"
                                            RenderMode="Inline">
                                            <Triggers>
                                            </Triggers>
                                            <ContentTemplate>
                                                <input type="button" id="ButtonTyoufukuCheck" value="�d�������Ȃ�" class="" runat="server"
                                                    tabindex="10" />
                                                <input id="ButtonExeTyoufukuCheck" style="display: none" type="button" value="�d���`�F�b�N�Ăяo��"
                                                    runat="server" onserverclick="ButtonExeTyoufukuCheck_ServerClick" />
                                                <input type="hidden" id="HiddenTyoufukuKakuninFlg1" value="" runat="server" />
                                                <input type="hidden" id="HiddenTyoufukuKakuninFlg2" value="" runat="server" />
                                                <input type="hidden" id="HiddenTyoufukuKakuninTargetId" runat="server" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <br />
                                        �R�F<asp:TextBox ID="TextBukkenJyuusyo3" Style="width: 30em" runat="server" CssClass=""
                                            MaxLength="54" TabIndex="10" />
                                        <input type="button" id="ButtonJyuusyoTenki" value="�Z���R����l�ɓ]�L" style="width: 9em"
                                            runat="server" tabindex="10" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="koumokuMei">
                                        ������]��</td>
                                    <td colspan="5" style="text-align: center">
                                        <asp:TextBox ID="TextTyousaKibouDate" Style="width: 150px" runat="server" CssClass="hissu hizuke"
                                            MaxLength="10" TabIndex="10" />
                                        (<asp:TextBox ID="TextTyousaKibouJikan" Style="width: 260px" runat="server" CssClass=""
                                            MaxLength="26" TabIndex="10" />) &nbsp;&nbsp;
                                        <asp:CheckBox ID="CheckYoyakuZumi" runat="server" /><span id="SpanYoyakuZumi" style="font-size: 17px;
                                            font-weight: bold;" runat="server">�\�菑�蓮</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="koumokuMei">
                                        ���� �����</td>
                                    <td colspan="5">
                                        <input type="radio" name="RadioTysTatiaisya" id="RadioTysTatiaisya1" runat="server"
                                            tabindex="10" />�L<span id="SpanTysTatiaisya1" runat="server"></span> &nbsp;
                                        (
                                        <input type="checkbox" id="CheckTysTatiaisyaSesyuSama" runat="server" value="1" disabled="disabled"
                                            tabindex="10" />�{��l<span id="SpanTysTatiaisyaSesyuSama" runat="server"></span>
                                        <input type="checkbox" id="CheckTysTatiaisyaTantousya" runat="server" value="2" disabled="disabled"
                                            tabindex="10" />�S����<span id="SpanTysTatiaisyaTantousya" runat="server"></span>
                                        <input type="checkbox" id="CheckTysTatiaisyaSonota" runat="server" value="4" disabled="disabled"
                                            tabindex="10" />���̑�<span id="SpanTysTatiaisyaSonota" runat="server"></span>
                                        ) &nbsp;
                                        <input type="radio" name="RadioTysTatiaisya" id="RadioTysTatiaisya0" runat="server"
                                            tabindex="10" />��<span id="SpanTysTatiaisya0" runat="server"></span>
                                        <input type="radio" name="RadioTysTatiaisya" id="RadioTysDummy" runat="server" style="display: none;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="koumokuMei" style="text-align: left; width: 136px;">
                                        �����T�v
                                    </td>
                                    <td colspan="5" style="padding: 0px">
                                        <table style="text-align: left; width: 100%;" id="TableTatemonoGaiyou" class="innerTable"
                                            cellpadding="0" cellspacing="0">
                                            <tr class="firstRow">
                                                <td colspan="2" rowspan="2" class="firstCol">
                                                    <span class="koumokuMei">�\����ʁF</span>
                                                    <asp:TextBox ID="TextKouzouSyubetuCd" runat="server" CssClass="pullCd" MaxLength="1"
                                                        TabIndex="20" />
                                                    <asp:DropDownList ID="SelectKouzouSyubetu" runat="server" Style="width: 120px;" TabIndex="21">
                                                    </asp:DropDownList><span id="SpanKouzouSyubetu" runat="server"></span><br />
                                                    <div style="width: 100%; text-align: right;">
                                                        <asp:TextBox ID="TextKouzouSyubetuSonota" Style="width: 300px" runat="server" MaxLength="80"
                                                            TabIndex="22" /></div>
                                                </td>
                                                <td colspan="2">
                                                    <span class="koumokuMei">�V�z���ցF</span>
                                                    <asp:TextBox ID="TextSintikuTatekaeCd" runat="server" CssClass="pullCd" MaxLength="1"
                                                        TabIndex="25" />
                                                    <asp:DropDownList ID="SelectSintikuTatekae" runat="server" Style="width: 120px;"
                                                        TabIndex="26">
                                                    </asp:DropDownList><span id="SpanSintikuTatekae" runat="server"></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <span class="koumokuMei">���׏��ʐρF</span>
                                                    <asp:TextBox ID="TextNobeyukaMenseki" Style="width: 100px" runat="server" CssClass="readOnlyStyle number"
                                                        ReadOnly="true" TabIndex="-1" />�u
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="firstCol">
                                                    <span class="koumokuMei">�K�w�F</span>
                                                    <asp:TextBox ID="TextKaisouCd" runat="server" CssClass="pullCd" MaxLength="2" TabIndex="23" />
                                                    <asp:DropDownList ID="SelectKaisou" runat="server" Style="width: 120px;" TabIndex="24">
                                                    </asp:DropDownList><span id="SpanKaisou" runat="server"></span>
                                                </td>
                                                <td colspan="2">
                                                    <span class="koumokuMei">���z�ʐρF</span>
                                                    <asp:TextBox ID="TextKentikuMenseki" Style="width: 100px" runat="server" CssClass="readOnlyStyle number"
                                                        ReadOnly="true" TabIndex="-1" />
                                                �u
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="firstCol">
                                                    <span class="koumokuMei">�����p�r�F</span>
                                                    <asp:TextBox ID="TextTatemonoYoutoCd" runat="server" CssClass="pullCd" MaxLength="1"
                                                        TabIndex="30" />
                                                    <asp:DropDownList ID="SelectTatemonoYouto" runat="server" Style="width: 100px;" TabIndex="30">
                                                    </asp:DropDownList><span id="SpanTatemonoYouto" runat="server"></span>
                                                </td>
                                                <td colspan="2" style="text-align: right; border-left: none;">
                                                    �p�r &nbsp; (<asp:TextBox ID="TextYouto" Style="width: 250px" runat="server" CssClass="readOnlyStyle"
                                                        ReadOnly="true" TabIndex="-1" />)
                                                    <br />
                                                    (<asp:TextBox ID="TextTatemonoYoutoSonota" Style="width: 250px" runat="server" CssClass="readOnlyStyle"
                                                        ReadOnly="true" TabIndex="-1" />)
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" class="firstCol">
                                                    �݌v���e�x����(<asp:TextBox ID="TextSekkeiKyoyouSijiryoku" Style="width: 60px" runat="server"
                                                        CssClass="number" MaxLength="6" TabIndex="30" />)kN/�u
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="koumokuMei">
                                        �˗������ɂ���
                                    </td>
                                    <td colspan="5">
                                        <span>�˗��\�蓏��</span> &nbsp;
                                        <asp:TextBox ID="TextIraiYoteiTousuu" Style="width: 60px" runat="server" CssClass="number"
                                            MaxLength="4" TabIndex="30" />��
                                    </td>
                                </tr>
                                <tr>
                                    <td class="koumokuMei">
                                        �n�Ƃ����<br />
                                        �\���b��<br />
                                        <br />
                                        <a href="JavaScript:Oritatami();" id="AOritatami" style="text-decoration: none;"
                                            tabindex="30">+</a>
                                    </td>
                                    <td colspan="5">
                                        ���؂�[�� (<asp:TextBox ID="TextNegiriHukasa" Style="width: 90px" runat="server" CssClass="number"
                                            MaxLength="13" TabIndex="30" />mm) &nbsp; �\�萷�y���� (<asp:TextBox ID="TextYoteiMoritutiAtusa"
                                                Style="width: 90px" runat="server" CssClass="number" MaxLength="13" TabIndex="30" />mm)
                                        <br />
                                        &nbsp;
                                        <asp:TextBox ID="TextYoteiKisoCd" runat="server" CssClass="pullCd" MaxLength="1"
                                            TabIndex="30" />
                                        <asp:DropDownList ID="SelectYoteiKiso" runat="server" Style="width: 142px;" TabIndex="30">
                                        </asp:DropDownList><span id="SpanYoteiKiso" runat="server"></span> &nbsp; �ް�W(<asp:TextBox
                                            ID="TextBaseW" Style="width: 100px" runat="server" CssClass="readOnlyStyle number"
                                            ReadOnly="true" TabIndex="-1" />mm) &nbsp;
                                        <asp:TextBox ID="TextYoteiKisoSonota" Style="width: 300px;" runat="server" MaxLength="80"
                                            TabIndex="30" />
                                        <br />
                                        ��b�����オ�荂�� (�f�k�{<asp:TextBox ID="TextKisoTatiagariTakasa" Style="width: 50px" runat="server"
                                            CssClass="readOnlyStyle number" ReadOnly="true" TabIndex="-1" />mm)
                                    </td>
                                </tr>
                                <!-- ��������[�ړ���:HJ] -->
                                <tr id="TrHJ" style="display: none" runat="server">
                                    <td class="koumokuMei">
                                        ��������
                                    </td>
                                    <td colspan="5">
                                        �~�n�ɖʂ��铹�H��(<asp:TextBox ID="TextHJDouroHaba" Style="width: 30px" runat="server" CssClass="readOnlyStyle"
                                            ReadOnly="true" TabIndex="-1" />m) (<input type="radio" name="RadioHJDouroHaba" value="2"
                                                id="RadioHJDouroHaba0" disabled="disabled" />2t��
                                        <input type="radio" name="RadioHJDouroHaba" value="4" id="RadioHJDouroHaba1" disabled="disabled" />4t��)�ȏ�̒ʍs�s��
                                        &nbsp; ���H�K�� (<input type="radio" name="RadioHJDouroKisei" value="0" id="RadioHJDouroKisei0"
                                            disabled="disabled" />��
                                        <input type="radio" name="RadioHJDouroKisei" value="1" id="RadioHJDouroKisei1" disabled="disabled" />�L)
                                        <br />
                                        �X���[�v
                                        <input type="radio" name="RadioHJSlope" value="0" id="RadioHJSlope0" disabled="disabled" />��
                                        <input type="radio" name="RadioHJSlope" value="1" id="RadioHJSlope1" disabled="disabled" />�L
                                        (�Ԍ�
                                        <asp:TextBox ID="TextMaguti" Style="width: 50px" runat="server" CssClass="readOnlyStyle"
                                            ReadOnly="true" TabIndex="-1" />m) &nbsp; �K�i
                                        <input type="radio" name="RadioHJKaidan" value="0" id="RadioHJKaidan0" disabled="disabled" />��
                                        <input type="radio" name="RadioHJKaidan" value="1" id="RadioHJKaidan1" disabled="disabled" />�L
                                        &nbsp;
                                        <input type="checkbox" id="CheckHJSonota" disabled="disabled" />���̑�(<asp:TextBox
                                            ID="TextHJSonota" Style="width: 120px" runat="server" CssClass="readOnlyStyle"
                                            ReadOnly="true" TabIndex="-1" />)
                                    </td>
                                </tr>
                                <!-- ������Q[�ړ���:TS] -->
                                <tr id="TrTS" style="display: none" runat="server">
                                    <td class="koumokuMei">
                                        ������Q
                                    </td>
                                    <td colspan="5">
                                        <input type="radio" name="RadioTSTakasaSyougai" value="0" id="RadioTSTakasaSyougai0"
                                            disabled="disabled" />��
                                        <input type="radio" name="RadioTSTakasaSyougai" value="1" id="RadioTSTakasaSyougai1"
                                            disabled="disabled" />�L (
                                        <input type="checkbox" id="CheckTSDensen" disabled="disabled" />�d��
                                        <input type="checkbox" id="CheckTSChannel" disabled="disabled" />�g���l��
                                        <input type="checkbox" id="ChecTSkSikitinaiKouteisa" disabled="disabled" />�~�n�����፷
                                        (<asp:TextBox ID="TextTSSikitinaiKouteisa" Style="width: 30px" runat="server" CssClass="readOnlyStyle"
                                            ReadOnly="true" TabIndex="-1" />m)
                                        <input type="checkbox" id="CheckTSSonota" value="1" disabled="disabled" />
                                        ���̑�(<asp:TextBox ID="TextTSSonota" Style="width: 120px" runat="server" CssClass="readOnlyStyle"
                                            ReadOnly="true" TabIndex="-1" />m))
                                    </td>
                                </tr>
                                <!-- �~�n�̑O��[�ړ���:SZ] -->
                                <tr id="TrSZ" style="display: none" runat="server">
                                    <td class="koumokuMei">
                                        �~�n�̑O��
                                    </td>
                                    <td colspan="5">
                                        <input type="checkbox" id="CheckSZTahata" disabled="disabled" />�c��
                                        <input type="checkbox" id="CheckSZTakuti" disabled="disabled" />��n
                                        <input type="checkbox" id="CheckSZSyokujuBatake" disabled="disabled" />�A����
                                        <input type="checkbox" id="CheckSZZoukiBayasi" disabled="disabled" />�G�ؗ�
                                        <input type="checkbox" id="CheckSZKantakuti" disabled="disabled" />����n
                                        <input type="checkbox" id="CheckSZKoujouAto" disabled="disabled" />�H���
                                        <input type="checkbox" id="CheckSZSonota" disabled="disabled" />���̑� (<asp:TextBox
                                            ID="TextSZSonota" Style="width: 120px" runat="server" CssClass="readOnlyStyle"
                                            ReadOnly="true" TabIndex="-1" />)
                                    </td>
                                </tr>
                                <!-- ��n�����Ɋւ���[�ړ���:TZ] -->
                                <tr id="TrTZ" style="display: none" runat="server">
                                    <td class="koumokuMei">
                                        ��n������<br />
                                        �ւ���
                                    </td>
                                    <td colspan="5">
                                        <input type="checkbox" id="CheckTZKankoutyou" disabled="disabled" />����������
                                        <input type="checkbox" id="CheckTZMinkan" disabled="disabled" />���ԑ��� ������(
                                        <asp:TextBox ID="TextTZZouseiAto" Style="width: 30px" runat="server" CssClass="readOnlyStyle"
                                            ReadOnly="true" TabIndex="-1" />)����
                                        <input type="checkbox" id="CheckTZKirituti" disabled="disabled" />�ؓy
                                        <input type="checkbox" id="CheckTZMorituti" disabled="disabled" />���y (
                                        <asp:TextBox ID="TextTZMorituti" Style="width: 50px" runat="server" CssClass="readOnlyStyle"
                                            ReadOnly="true" TabIndex="-1" />)m
                                    </td>
                                </tr>
                                <!-- �~�n�̏�[�ړ���:SJ] -->
                                <tr id="TrSJ" style="display: none" runat="server">
                                    <td class="koumokuMei">
                                        �~�n�̏�
                                    </td>
                                    <td colspan="5">
                                        ��������(
                                        <input type="radio" name="RadioSJKizonTatemono" value="0" id="RadioSJKizonTatemono0"
                                            disabled="disabled" />��
                                        <input type="radio" name="RadioSJKizonTatemono" value="1" id="RadioSJKizonTatemono1"
                                            disabled="disabled" />�L ) &nbsp; ����(
                                        <input type="radio" name="RadioSJKaiinu" value="0" id="RadioSJKaiinu0" disabled="disabled" />��
                                        <input type="radio" name="RadioSJKaiinu" value="1" id="RadioSJKaiinu1" disabled="disabled" />�L
                                        ) &nbsp; ���(
                                        <input type="radio" name="RadioSJIdo" value="0" id="RadioSJIdo0" disabled="disabled" />��
                                        <input type="radio" name="RadioSJIdo" value="1" id="RadioSJIdo1" disabled="disabled" />�L
                                        )
                                        <br />
                                        �i��(�����F
                                        <input type="radio" name="RadioSJYouhekiG" value="0" id="RadioSJYouhekiG0" disabled="disabled" />��
                                        <input type="radio" name="RadioSJYouhekiG" value="1" id="RadioSJYouhekiG1" disabled="disabled" />�L
                                        &nbsp; �\��F
                                        <input type="radio" name="RadioSJYouhekiY" value="0" id="RadioSJYouhekiY0" disabled="disabled" />��
                                        <input type="radio" name="RadioSJYouhekiY" value="1" id="RadioSJYouhekiY1" disabled="disabled" />�L
                                        ) &nbsp; �򉻑�(�����F
                                        <input type="radio" name="RadioSJJoukasouG" value="0" id="RadioSJJoukasouG0" disabled="disabled" />��
                                        <input type="radio" name="RadioSJJoukasouG" value="1" id="RadioSJJoukasouG1" disabled="disabled" />�L
                                        &nbsp; �\��F
                                        <input type="radio" name="RadioSJJoukasouY" value="0" id="RadioSJJoukasouY0" disabled="disabled" />��
                                        <input type="radio" name="RadioSJJoukasouY" value="1" id="RadioSJJoukasouY1" disabled="disabled" />�L
                                        )
                                        <br />
                                        <input type="checkbox" id="CheckSJTahata" disabled="disabled" />�c��
                                        <input type="checkbox" id="CheckSJTyuusyajou" disabled="disabled" />���ԏ�
                                        <input type="checkbox" id="CheckSJSonota" disabled="disabled" />���̑� (<asp:TextBox
                                            ID="TextSJSonota" Style="width: 120px" runat="server" CssClass="readOnlyStyle"
                                            ReadOnly="true" TabIndex="-1" />)
                                    </td>
                                </tr>
                                <tr>
                                    <td class="koumokuMei">
                                        �n���ԌɌv��
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="TextTikaSyakoKeikakuCd" runat="server" CssClass="pullCd" MaxLength="1"
                                            TabIndex="30" />
                                        <asp:DropDownList ID="SelectTikaSyakoKeikaku" runat="server" Style="width: 160px"
                                            TabIndex="30">
                                        </asp:DropDownList><span id="SpanTikaSyakoKeikaku" runat="server"></span>
                                    </td>
                                    <td style="text-align: center">
                                        �n�����</td>
                                    <td colspan="2">
                                        �ϐ�� (�ő�<asp:TextBox ID="TextSekisetuRyou" Style="width: 30px" runat="server" CssClass="readOnlyStyle"
                                            ReadOnly="true" TabIndex="-1" />cm)
                                    </td>
                                </tr>
                                <!-- �Y�t����[�ړ���:TP] -->
                                <tr>
                                    <td class="koumokuMei">
                                        �Y�t����
                                    </td>
                                    <td colspan="5">
                                        <span id="SpanHissu" class="koumokuMei">�K�{�F</span>
                                        <asp:CheckBox ID="CheckTPAnnaiZu" runat="server" TabIndex="30" />�ē��}�i�抄�}�E���ʐ}�Ȃǁj<span
                                            id="SpanTPAnnaiZu" runat="server"></span>
                                        <asp:CheckBox ID="CheckTPHaitiZu" runat="server" TabIndex="30" />�z�u�}<span id="SpanTPHaitiZu"
                                            runat="server"></span>
                                        <asp:CheckBox ID="CheckTPKakukaiHeimenZu" runat="server" TabIndex="30" />�e�K���ʐ} &nbsp;<span
                                            id="SpanTPKakukaiHeimenZu" runat="server"></span> <span id="SpanNinni" class="koumokuMei">
                                                �C�ӁF</span>
                                        <asp:CheckBox ID="CheckTPKsFuseZu" runat="server" TabIndex="30" />��b���}<span id="SpanTPKsFuseZu"
                                            runat="server"></span>
                                        <asp:CheckBox ID="CheckTPKsDanmenZu" runat="server" TabIndex="30" />��b�f�ʐ}<span id="SpanTPKsDanmenZu"
                                            runat="server"></span>
                                        <asp:CheckBox ID="CheckTPZouseiKeikakuZu" runat="server" TabIndex="30" />���ʐ}<span
                                            id="SpanTPZouseiKeikakuZu" runat="server"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="koumokuMei">
                                        ��b���H�\���
                                    </td>
                                    <td style="text-align: center" colspan="5">
                                        <span class="hizuke">
                                            <asp:TextBox ID="TextKsTyakkouYoteiDateFrom" Style="width: 150px" runat="server"
                                                CssClass="hizuke" MaxLength="10" TabIndex="30" />
                                            �`
                                            <asp:TextBox ID="TextKsTyakkouYoteiDateTo" Style="width: 150px" runat="server" CssClass="hizuke"
                                                MaxLength="10" TabIndex="30" />
                                            �� </span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <!-- ��ʉ����E���̑����[�ړ���:SI] -->
                    <tr>
                        <td colspan="4" style="height: 2px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:UpdatePanel ID="UpdatePanelSonota" UpdateMode="conditional" runat="server" RenderMode="inline"
                                ChildrenAsTriggers="true">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ButtonKITourokuBangou" />
                                </Triggers>
                                <ContentTemplate>
                                    <input type="hidden" id="HiddenSITysGaisyaMae" runat="server" /><%--������ЃR�[�h�E�ύX�O--%>
                                    <table style="text-align: left; width: 100%;" id="Table6" class="mainTable" cellpadding="1">
                                        <thead>
                                            <tr>
                                                <th class="tableTitle" colspan="9" style="width: 100%">
                                                    <a id="ASI">���̑����</a>
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody id="TbodySI">
                                            <tr>
                                                <td colspan="9" class="tableSpacer">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="koumokuMei" style="width: 80px" rowspan="2">
                                                    ���l</td>
                                                <td rowspan="2" colspan="4">
                                                    <textarea id="TextSIBikou" cols="80" rows="3" style="font-family: Sans-Serif; ime-mode: active;"
                                                        onfocus="this.select();" runat="server" tabindex="30"></textarea><textarea id="TextSIBikou2"
                                                            cols="60" rows="4" style="display: none; font-family: Sans-Serif; ime-mode: active;"
                                                            onfocus="this.select();" runat="server" tabindex="30"></textarea></td>
                                                <td class="koumokuMei">
                                                    &nbsp;</td>
                                                <td colspan="3">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="koumokuMei">
                                                    ��������R�[�h
                                                </td>
                                                <td colspan="3">
                                                    <input type="text" id="TextBukkenNayoseCd" style="width: 90px; ime-mode: disabled;"
                                                        runat="server" class="codeNumber" maxlength="11" tabindex="30"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="koumokuMei">
                                                    �o�R</td>
                                                <td colspan="1">
                                                    <asp:DropDownList ID="SelectSIKeiyu" runat="server" TabIndex="30">
                                                    </asp:DropDownList><span id="SpanSIKeiyu" runat="server"></span>
                                                </td>
                                                <td class="koumokuMei">
                                                    ��������</td>
                                                <td colspan="2">
                                                    <asp:DropDownList ID="SelectSITatemonoKensa" runat="server" TabIndex="30">
                                                        <asp:ListItem Value="0" Text="0:����" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="1:�L��"></asp:ListItem>
                                                    </asp:DropDownList><span id="SpanSITatemonoKensa" runat="server"></span>
                                                </td>
                                                <td class="koumokuMei">
                                                    �ː�</td>
                                                <td colspan="4">
                                                    <asp:TextBox ID="TextSIKosuu" Style="width: 40px" runat="server" CssClass="number"
                                                        MaxLength="4" TabIndex="30" />��</td>
                                            </tr>
                                            <tr style="display: none;">
                                                <td class="koumokuMei">
                                                    &nbsp;</td>
                                                <td colspan="3">
                                                    <input type="radio" name="RadioSISyouhinKbn" value="1" id="RadioSISyouhinKbn1" runat="server"
                                                        style="display: none" disabled="disabled" tabindex="30" /><span id="Span1" runat="server"></span>
                                                    <span id="SpanSISyouhinKbn1" style="display: none;" runat="server">60�N�ۏ�</span>
                                                    <input type="radio" name="RadioSISyouhinKbn" value="2" id="RadioSISyouhinKbn2" runat="server"
                                                        tabindex="30" style="display: none" disabled="disabled" /><span id="Span2" runat="server"></span>
                                                    <span id="SpanSISyouhinKbn2" runat="server">�y�n�̔�</span>
                                                    <input type="radio" name="RadioSISyouhinKbn" value="3" id="RadioSISyouhinKbn3" runat="server"
                                                        tabindex="30" style="display: none" disabled="disabled" /><span id="Span3" runat="server"></span>
                                                    <span id="SpanSISyouhinKbn3" runat="server">���t�H�[��</span>
                                                    <input type="radio" name="RadioSISyouhinKbn" value="9" id="RadioSISyouhinKbn9" runat="server"
                                                        style="display: none" disabled="disabled" tabindex="30" /><span id="Span9" runat="server"></span>
                                                    <span id="SpanSISyouhinKbn9" style="display: none;" runat="server">&nbsp;</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="koumokuMei">
                                                    �������</td>
                                                <td colspan="8">
                                                    <span id="SpanSITysGaisya">
                                                        <asp:TextBox ID="TextSITysGaisyaCd" Style="width: 60px" runat="server" CssClass="codeNumber"
                                                            MaxLength="7" TabIndex="30" />
                                                        <input type="button" id="ButtonSITysGaisya" value="����" class="gyoumuSearchBtn" runat="server"
                                                            onmouseup="JStyousakaisyaSearchType=9;" onkeydown="if(event.keyCode==13||event.keyCode==32)JStyousakaisyaSearchType=9;"
                                                            onserverclick="ButtonSITysGaisya_ServerClick" tabindex="30" />
                                                        <input type="hidden" id="tyousakaisyaSearchType" runat="server" value="" />
                                                        <asp:TextBox ID="TextSITysGaisyaMei" Style="width: 25em;" runat="server" CssClass="readOnlyStyle"
                                                            ReadOnly="true" TabIndex="-1" />
                                                    </span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="koumokuMei">
                                                    ���i1</td>
                                                <td colspan="4">
                                                    <asp:UpdatePanel ID="UpdatePanelSyouhin1" UpdateMode="Conditional" runat="server"
                                                        RenderMode="Inline">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnSetTysGaiyou" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="choSyouhin1" runat="server" Width="310px" CssClass="hissu"
                                                                TabIndex="30">
                                                            </asp:DropDownList><span id="SpanSISyouhin1" runat="server"></span><input type="hidden"
                                                                id="HiddenSyouhin1Pre" runat="server" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td class="koumokuMei">
                                                    �ۏ؏��i</td>
                                                <td colspan="3">
                                                    <asp:UpdatePanel ID="UpdatePanelHosyouSyouhinUmu" UpdateMode="Conditional" runat="server"
                                                        RenderMode="Inline">
                                                        <ContentTemplate>
                                                            <input id="TextHosyouSyouhinUmu" runat="server" class="readOnlyStyle" style="width: 30px"
                                                                tabindex="-1" readonly="readOnly" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td class="koumokuMei" colspan="1" style="display: none;">
                                                    �ۏؗL��</td>
                                                <td colspan="2" style="display: none;">
                                                    <asp:DropDownList ID="SelectSIHosyouUmu" runat="server" TabIndex="30">
                                                        <asp:ListItem Value="" Text=""></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="1:�L��"></asp:ListItem>
                                                    </asp:DropDownList><span id="SpanSIHosyouUmu" runat="server"></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="koumokuMei">
                                                    �������@</td>
                                                <td colspan="1">
                                                    <asp:UpdatePanel ID="UpdateTysHouhou" UpdateMode="Conditional" runat="server" RenderMode="Inline">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnSetTysGaiyou" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="TextSITysHouhouCd" runat="server" CssClass="pullCd" MaxLength="2"
                                                                TabIndex="30" />
                                                            <asp:DropDownList ID="SelectSITysHouhou" runat="server" Style="width: 210px;" TabIndex="30">
                                                            </asp:DropDownList>
                                                            <span id="SpanSITysHouhou" runat="server"></span>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td class="koumokuMei" colspan="1">
                                                    �����T�v</td>
                                                <td colspan="6">
                                                    <asp:UpdatePanel ID="UpdatePanelTysGaiyou" UpdateMode="Conditional" runat="server"
                                                        RenderMode="Inline">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnSetTysGaiyou" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="SelectSITysGaiyou" runat="server" Style="width: 266px;" TabIndex="30">
                                                            </asp:DropDownList><span id="SpanSITysGaiyou" runat="server"></span>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <!-- ��ʉ����E��������[�ړ���:BR] -->
                    <tr>
                        <td colspan="4" style="height: 2px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table style="text-align: left; width: 100%;" id="tblBRInfo" class="mainTable" cellpadding="1">
                                <thead>
                                    <tr>
                                        <th class="tableTitle" colspan="9" style="width: 100%">
                                            <a id="AncBRInfo" runat="server" tabindex="40">�����������</a>
                                            <input type="hidden" id="HiddenBRInfoStyle" runat="server" value="none" />
                                        </th>
                                    </tr>
                                </thead>
                                <tbody id="TBodyBRInfo" runat="server">
                                    <!-- ������������1-->
                                    <tr>
                                        <td class="koumokuMei">
                                            ���</td>
                                        <td colspan="3" rowspan="1">
                                            <asp:DropDownList runat="server" ID="SelectBRSyubetu1" Width="300px" CssClass="" tabindex="40">
                                            </asp:DropDownList>
                                        </td>
                                        <td rowspan="3">
                                             <textarea id="TextAreaBRNaiyou1" runat="server" cols="70" onfocus="this.select();" rows="5" style="ime-mode: active; font-family: Sans-Serif" tabindex="45"></textarea>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="koumokuMei">
                                            ����</td>
                                        <td colspan="3" rowspan="1">
                                            <select id="SelectBRBunrui1" style="width: 300px" onchange="UpdHiddenBunrui(this,1)" class="" tabindex="40" >
                                            </select>
                                            <input type="hidden" id="HiddenBRBunrui1" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="koumokuMei">
                                            �ėp�R�[�h</td>
                                        <td rowspan="1">
                                            <asp:TextBox ID="TextBRHanyouCd1" MaxLength="20" runat="server" Style="width: 200px" tabindex="40" CssClass="codeNumber"/>
                                        </td>
                                        <td class="koumokuMei">
                                            ���t</td>
                                        <td rowspan="1">
                                            <asp:TextBox ID="TextBRHizuke1" MaxLength="10" runat="server" Style="width: 70px" tabindex="40"  CssClass="date"/>
                                        </td>
                                    </tr>
                                    <!-- ������������2-->
                                    <tr style="background-color: LightCyan;">
                                        <td class="koumokuMei">
                                            ���</td>
                                        <td colspan="3" rowspan="1">
                                            <asp:DropDownList runat="server" ID="SelectBRSyubetu2" Width="300px" CssClass="" tabindex="50">
                                            </asp:DropDownList>
                                        </td>
                                        <td rowspan="3">
                                             <textarea id="TextAreaBRNaiyou2" runat="server" cols="70" onfocus="this.select();" rows="5" style="ime-mode: active; font-family: Sans-Serif" tabindex="55"></textarea>
                                        </td>
                                    </tr>
                                    <tr style="background-color: LightCyan;">
                                        <td class="koumokuMei">
                                            ����</td>
                                        <td colspan="3" rowspan="1">
                                            <select id="SelectBRBunrui2" style="width: 300px" onchange="UpdHiddenBunrui(this,2)" class="" tabindex="50" >
                                            </select>
                                            <input type="hidden" id="HiddenBRBunrui2" runat="server" />
                                        </td>
                                    </tr>
                                    <tr style="background-color: LightCyan;">
                                        <td class="koumokuMei">
                                            �ėp�R�[�h</td>
                                        <td rowspan="1">
                                            <asp:TextBox ID="TextBRHanyouCd2" MaxLength="20" runat="server" Style="width: 200px" tabindex="50" CssClass="codeNumber"/>
                                        </td>
                                        <td class="koumokuMei">
                                            ���t</td>
                                        <td rowspan="1">
                                            <asp:TextBox ID="TextBRHizuke2" MaxLength="10" runat="server" Style="width: 70px" tabindex="50"  CssClass="date"/>
                                        </td>
                                    </tr>
                                    <!-- ������������3-->
                                    <tr>
                                        <td class="koumokuMei">
                                            ���</td>
                                        <td colspan="3" rowspan="1">
                                            <asp:DropDownList runat="server" ID="SelectBRSyubetu3" Width="300px" CssClass="" tabindex="60">
                                            </asp:DropDownList>
                                        </td>
                                        <td rowspan="3">
                                             <textarea id="TextAreaBRNaiyou3" runat="server" cols="70" onfocus="this.select();" rows="5" style="ime-mode: active; font-family: Sans-Serif" tabindex="65"></textarea>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="koumokuMei">
                                            ����</td>
                                        <td colspan="3" rowspan="1">
                                            <select id="SelectBRBunrui3" style="width: 300px" onchange="UpdHiddenBunrui(this,3)" class="" tabindex="60" >
                                            </select>
                                            <input type="hidden" id="HiddenBRBunrui3" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="koumokuMei">
                                            �ėp�R�[�h</td>
                                        <td rowspan="1">
                                            <asp:TextBox ID="TextBRHanyouCd3" MaxLength="20" runat="server" Style="width: 200px" tabindex="60" CssClass="codeNumber"/>
                                        </td>
                                        <td class="koumokuMei">
                                            ���t</td>
                                        <td rowspan="1">
                                            <asp:TextBox ID="TextBRHizuke3" MaxLength="10" runat="server" Style="width: 70px" tabindex="60"  CssClass="date"/>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>                               
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="1"
            class="titleTable">
            <tbody>
                <tr>
                    <th style="text-align: right; padding: 10px;">
                        <input type="button" id="ButtonTouroku2" value="�o�^ ���s" style="font-weight: bold;
                            font-size: 18px; width: 155px; color: black; height: 30px; background-color: fuchsia"
                            runat="server" tabindex="30" />
                    </th>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>

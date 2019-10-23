<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="PopupBukkenRireki.aspx.vb" Inherits="Itis.Earth.WebUI.PopupBukkenRireki"
    Title="EARTH ��������" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script type="text/javascript">

        //�E�B���h�E�T�C�Y�ύX
        try{
            window.resizeTo(1024,768);
        }catch(e){
            //�A�N�Z�X�����ۂ���܂����̃G���[���o���牽�����Ȃ��B
            if(e.number == 2147024891) throw e;
        }
        
        //�R���g���[���ړ���
        var gVarOyaSettouji = "ctl00_CPH1_";
        
        var gVarSettouJi = gVarOyaSettouji + "CtrlBukkenRireki_"; 
        var gVarSelectSyubetu = "_SelectSyubetu";
        var gVarSpanBunrui = "_SpanBunrui";
        var gVarSpanTorikesi = "_SpanTorikesi";
        
        var gVarTr1 = "_Tr1";
        var gVarTr2 = "_Tr2";
        var gVarTdCode = "_TdCode"
        
        var gVarHiddenBunrui = "_HiddenBunrui";

        var gVarSelectTmpSyubetu = "SelectSyubetu_";
               
        _d = document;
                
        /****************************************
         * onload���̒ǉ�����
         ****************************************/
        function funcAfterOnload() {
            // �R�[�h�v���_�E���̒l�\����؂�ւ�
            ChgDispSelectCode();
            
            //����s�\���ݒ�
            ChgDispTorikesi();
        }
        
        //Index��Ԃ�
        function GetIndex(objID){
            var varTmp = objID;
            varTmp = varTmp.replace(gVarSelectSyubetu,'');
            varTmp = varTmp.replace(gVarSpanBunrui,'');
            varTmp = varTmp.replace(gVarSpanTorikesi,'');
            varTmp = varTmp.replace(gVarTr1,'');
            varTmp = varTmp.replace(gVarTr2,'');
            varTmp = varTmp.replace(gVarTdCode,'');
            varTmp = varTmp.replace(gVarHiddenBunrui,'');
            varTmp = varTmp.replace(gVarSelectTmpSyubetu,'');
            
            return varTmp;
        }
        
        // ���������e�[�u�� �e�탌�C�A�E�g�ݒ�
        function settingTable(){
            var rirekiData = objEBI("<%=tblMeisai.clientID %>");
            setRirekiColor(rirekiData);
        }
        
        /**
         * �������Ƃɔw�i�F��ύX�i������2�s���ƂɕύX�j
         * 
         * @param objGridTBody:�ΏۂƂ���table��tbody�G�������g
         * @return
         */
        function setRirekiColor(objGridTBody) {
            var countTr = 0;
            var arrTr = objGridTBody.rows;
            // ���׍s�̐��������[�v
            for (var i = 0; i < arrTr.length; i = i + 2) {
                var objTr = arrTr[i];
                var k = i;
                var blnChg = false;
                // �S�s�����[�v���A�S�Ĕ�\�����ǂ������f
                for (var j = 0; j < 2; j++){
                    k = i + j;
                    objWkTr = arrTr[k];
                    if (objWkTr.style.display != "none"){
                        blnChg = true;
                    }
                }
                // 1�s�ł��\������Ă���s����������A�w�i�F��ύX
                if (blnChg == true){
                    for (var j = 0; j < 2; j++){
                        k = i + j;
                        objWkTr = arrTr[k];
                        if (countTr % 2 == 0) {
                            objWkTr.className  = "odd";
                        } else {
                            objWkTr.className  = "even";
                        }
                    }
                    // �w�i�F��ύX�������̂݃J�E���g�A�b�v
                    countTr++;
                }
            }
            return true;
        }
        
        // �R�[�h�v���_�E���̒l�\����؂�ւ�
        function ChgDispSelectCode(){
            var objGridTBody = objEBI("<%=tblMeisai.clientID %>");
            var arrTr = objGridTBody.rows;
            var varLine = 0;
            var objSel = '';
            var varFlg = '';
                
            // ���׍s�̐��������[�v
            for (var i = 0; i < arrTr.length; i = i + 2) {
                varLine++ ;

                //�I�u�W�F�N�g�̎擾
                objSelSyubetu = RetObject(gVarSelectSyubetu,varLine);
                if(objSelSyubetu == undefined) continue;
                
                //�I�u�W�F�N�g�̎擾
                objTd = RetObject(gVarTdCode,varLine);
                if(objTd == undefined) continue;
                objSel = objTd.childNodes[0];
                if(objSel == undefined) continue;
                
                //�I�v�V�����S�폜
                SelectOptionDelete(objSel);
            
                //�I�v�V�����}��
                SelectOptionInsert(objSel,objSelSyubetu.value);

                //���ނ̑��݃`�F�b�N
                if(ChkExitSelectCode(objSelSyubetu.value) == false){
                    var strMSG = "<%= Messages.MSG113E %>";
                    strMSG = strMSG.replace('@PARAM1','����');
                    alert(strMSG);
                    return false;
                }
                                
                //�I�v�V�����Z�b�g
                SelectOptionSet(objSel,varLine);
                
            }
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
            varMoto = gVarOyaSettouji + gVarSelectTmpSyubetu + intFlg;
           
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
        function SelectOptionSet(objSel,varLine){
            //Hidden�̎擾����уZ�b�g
            objHdnCode = RetObject(gVarHiddenBunrui,varLine);
            objSel.value = objHdnCode.value; //Selected���w��
            //SPAN�̎擾����уZ�b�g
            objSpnBunrui = RetObject(gVarSpanBunrui,varLine);
            if(objHdnCode.value == ''){
                objSpnBunrui.innerHTML = '';
            }else{
                objSpnBunrui.innerHTML = objSel.options[objSel.selectedIndex].text; //�I��l���Z�b�g
            }
        }
        
        //���׍s�̊Y���R���g���[���I�u�W�F�N�g��Ԃ�
        function RetObject(varTarget,varLine){
            var varTmpId = gVarSettouJi + varLine + varTarget;
            return objEBI(varTmpId);
        }
        
        //����s�̕\���ؑ�
        //��0:��\��,1:�\��
        function TorikesiDisp(varFlg){           
            var objGridTBody = objEBI("<%=tblMeisai.clientID %>");
            var arrTr = objGridTBody.rows;
            var objTr = null;
            var objSyubetu = objEBI("<%=SelectSyubetu.clientID %>");
            var objTrTorikesi = null;
            var objTrSyubetu = null;
            var blnSyubetuFlg = true; //��ʕ\������t���O
            
            for ( var i = 1; i < arrTr.length; i++) {
                objTr = arrTr[i];
                objTorikesi = null;
                objTrSyubetu = null;
                blnSyubetuFlg = true;
                
                //��ʔ���
                if(objSyubetu.value == ''){ //���I��
                    //TR1�s��
                    objTr = RetObject(gVarTr1,i);
                    if(objTr == undefined){
                    }else{
                        objTr.style.display = "inline";
                    }
                    //TR2�s��
                    objTr = RetObject(gVarTr2,i);
                    if(objTr == undefined){
                    }else{
                        objTr.style.display = "inline";
                    }                           
                    
                }else{ //�I������               
                    //��ʏ㕔.��� = Tr.��ʂ��r�E����
                    //SELECT���                
                    objTrSyubetu = RetObject(gVarSelectSyubetu,i);
                    if(objTrSyubetu == undefined){
                    }else{
                        if(objSyubetu.value == objTrSyubetu.value){ //��v
                            //TR1�s��
                            objTr = RetObject(gVarTr1,i);
                            objTr.style.display = "inline";
                            //TR2�s��
                            objTr = RetObject(gVarTr2,i);
                            objTr.style.display = "inline";                           
                            
                        }else{ //�s��v
                            //TR1�s��
                            objTr = RetObject(gVarTr1,i);
                            objTr.style.display = "none";
                            //TR2�s��
                            objTr = RetObject(gVarTr2,i);
                            objTr.style.display = "none";
                            
                            blnSyubetuFlg = false;
                        }
                    }                

                }
                //�������
                //SPAN���                
                objTrTorikesi = RetObject(gVarSpanTorikesi,i);
                if(objTrTorikesi == undefined){
                }else{
                    if(objTrTorikesi.innerHTML == "���"){
                        if(varFlg == 0){ //��\��
                            //TR1�s��
                            objTr = RetObject(gVarTr1,i);
                            objTr.style.display = "none";
                            //TR2�s��
                            objTr = RetObject(gVarTr2,i);
                            objTr.style.display = "none";
                        }else{ //�\��
                            if(blnSyubetuFlg){
                                //TR1�s��
                                objTr = RetObject(gVarTr1,i);
                                objTr.style.display = "inline";
                                //TR2�s��
                                objTr = RetObject(gVarTr2,i);
                                objTr.style.display = "inline";                           
                            }else{
                                //TR1�s��
                                objTr = RetObject(gVarTr1,i);
                                objTr.style.display = "none";
                                //TR2�s��
                                objTr = RetObject(gVarTr2,i);
                                objTr.style.display = "none";
                            }
                            
                        } 
                    }
                }
            }
            
            // ���������e�[�u�� �e�탌�C�A�E�g�ݒ�
            settingTable();
            
            //�I�u�W�F�N�g�̎擾
            var objHdnInit = objEBI("<%=HiddenTorikesi.clientID %>");
            objHdnInit.value = varFlg;
        }
                
        //����s�\���ݒ�
        function ChgDispTorikesi(){
            var objHdnInit = objEBI("<%=HiddenTorikesi.clientID %>");
            TorikesiDisp(objHdnInit.value);
        }
        
        //SelectCode�ւ̃t�H�[�J�X����
        //���������ɂ́AHiddenCode.id���w�肷��
        function GetSelectCode(objID){
            //Index�̎擾
            var varLine = GetIndex(objID);
            //�I�u�W�F�N�g�̎擾
            var objTd = RetObject(gVarTdCode,varLine);
            if(objTd == undefined) return false;
            var objSel = objTd.childNodes[0];
            if(objSel == undefined) return false;
            return objSel;
        }
        
        //���������ڍ׉�ʌďo����
        function PopupSyousai(strNyuuryokuNo){    
            //�I�u�W�F�N�g�̍ēǍ�(Ajax�ĕ`��Ή�)
            objSendTargetWin = objEBI("<%= sendTargetWin.clientID %>");
            if(objSendTargetWin.value == "")objSendTargetWin.value="<%=EarthConst.MAIN_WINDOW_NAME %>";
            
            //�I�[�v���Ώۂ̉�ʂ��w��
            varAction = "<%=UrlConst.POPUP_BUKKEN_RIREKI_SYUUSEI %>";
            
            //<!-- ��ʈ��n����� -->
            objSendForm = objEBI("searchForm");
            //�敪+�ԍ�+����NO
            var objSendVal_SearchTerms = objEBI("sendSearchTerms");
            var strKbn = objEBI("<%= HiddenKbn.clientID %>").value + "<%=EarthConst.SEP_STRING %>";
            var strBangou = objEBI("<%= TextBangou.clientID %>").value + "<%=EarthConst.SEP_STRING %>";
            objSendVal_SearchTerms.value = strKbn + strBangou + strNyuuryokuNo;
            
            //�㏈���{�^��ID
            objEBI("afterEventBtnId").value = "<%= ButtonReload.clientID %>";
            
            var varWindowName = "PopupBukkenRirekiSyuusei";
            objSrchWin = window.open("about:Blank", varWindowName, "menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes");
                        
            //�l�Z�b�g
            objSendTargetWin.value = varWindowName;
            objSendForm.target=objSendTargetWin.value;
                            
            objSendForm.action = varAction;
            objSendForm.submit();
        }

        //��ʂ̕\���ؑ�
        function SyubetuDisp(){
            var objTmp = null;
            
            //��ʏ㕔�E����s�\��
            objTmp = objEBI("<%=RadioTorikesiDisp.clientID %>");
            if(objTmp.checked){
                TorikesiDisp(1); //�\��
            }else{
                TorikesiDisp(0); //��\��
            }
        }
         
    </script>

    <div>
        <input type="hidden" id="HiddenLineCnt" runat="server" value="0"/>
        <input type="hidden" id="HiddenTorikesi" runat="server" value="0"/>
        <input type="hidden" id="HiddenKengen" runat="server" value="0"/>
        <div id="divSelect" runat="server" >
        </div>
        <asp:DropDownList runat="server" ID="SelectTmpCode" Style="display: none;">
        </asp:DropDownList>
        <%-- ��ʃ^�C�g�� --%>
        <table>
            <tr>
                <td>
                    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
                        class="titleTable">
                        <tr>
                            <th style="text-align: left; width: 100px;">
                                ��������</th>
                            <th style="text-align: left;">
                                <input type="button" id="ButtonClose" runat="server" value="����" onclick="window.close();" />
                            </th>
                            <th style="width:20px">
                                &nbsp;</th>
                            <th>
                                ����s:
                                <input type="radio" id="RadioTorikesiDisp" name="RadioTorikesiDisp" runat="server" value="1" onclick="TorikesiDisp(this.value)"/>�\��
                                <input type="radio" id="RadioTorikesiDispNone" name="RadioTorikesiDisp" runat="server" value="0" onclick="TorikesiDisp(this.value)"/>��\��
                                <input type="button" id="ButtonTorikesiDisp" value="����s���\������" style="width: 140px;display:none;"
                                    onclick="TorikesiDisp(1)" tabindex="-1"/>
                                <input type="button" id="ButtonTorikesiDispNone" value="����s��\��" style="width: 120px;display:none;"
                                    onclick="TorikesiDisp(0)" tabindex="-1"/>
                            </th>
                            <th style="width: 20px">
                                &nbsp;</th>
                            <th>
                                ��ʍi��
                                <asp:DropDownList runat="server" ID="SelectSyubetu" Width="300px">
                                </asp:DropDownList><span id="SpanSyubetu" runat="server"></span>
                            </th>
                            <th style="width: 90px">
                                &nbsp;</th>
                            <th style="text-align:right">
                                <input type="button" id="ButtonReload" runat="server" value="�ēǍ�" style="width: 80px;display:none;" />
                                <input type="button" id="ButtonSinki" runat="server" value="�V�K" style="width: 80px;" onclick="PopupSyousai(0)" />
                            </th>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <%-- ��ʏ㕔[������{���] --%>
        <table cellpadding="0" cellspacing="0" style="width:800px; border-bottom: solid 2px gray; border-left: solid 2px gray;"
            class="mainTable">
            <tr>
                <td class="koumokuMei" style="width: 40px">
                    �敪</td>
                <td>
                    <asp:TextBox runat="server" ID="TextKbn" Style="ime-mode: disabled; border-style: none;"
                        ReadOnly="true" CssClass="readOnlyStyle" TabIndex="-1" />
                    <input type="hidden" id="HiddenKbn" runat="server" />
                </td>
                <td class="koumokuMei" style="width: 40px">
                    �ԍ�</td>
                <td>
                    <asp:TextBox runat="server" ID="TextBangou" Style="width: 100px;" ReadOnly="true"
                        CssClass="readOnlyStyle" TabIndex="-1" />
                </td>
                <td class="koumokuMei" style="width: 60px">
                    �{�喼</td>
                <td colspan="8">
                    <asp:TextBox runat="server" ID="TextSesyuMei" Style="width: 27em;" ReadOnly="true"
                        CssClass="readOnlyStyle" TabIndex="-1" MaxLength="50" />
                </td>
            </tr>
        </table>
        <br />
        <%-- ��ʏ㕔[�����������] --%>
        <table cellpadding="0" cellspacing="0" style="border-bottom: solid 2px gray; border-left: solid 2px gray;">
            <tr>
                <td>
                    <table class="mainTable" style="width: 900px; border-bottom: none; border-left: solid 0px gray;
                        table-layout: fixed;" id="Table2" cellpadding="0" cellspacing="0">
                        <%-- �w�b�_�� --%>
                        <tr>
                            <td class="koumokuMei2" style="width: 200px">
                                ���/���</td>
                            <td class="koumokuMei2" style="width: 300px">
                                ����/���t/�ėp�R�[�h</td>
                            <td class="koumokuMei2" style="width: 350px">
                                ���e</td>
                            <td class="koumokuMei2" style="width: 50px">
                                ����</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="width: 930px; height: 500px; overflow-y: scroll; border-top: none; border-left: solid 0px gray;">
                        <table class="mainTable" cellpadding="0" cellspacing="0" style="width: 930px; border-top: none;
                            border-left: solid 0px gray;">
                            <!-- �f�[�^�� -->
                            <tbody id="tblMeisai" runat="server">
                            </tbody>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <input type="hidden" id="sendTargetWin" runat="server" />
</asp:Content>

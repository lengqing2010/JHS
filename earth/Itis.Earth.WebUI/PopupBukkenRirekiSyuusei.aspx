<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="PopupBukkenRirekiSyuusei.aspx.vb" Inherits="Itis.Earth.WebUI.PopupBukkenRirekiSyuusei"
    Title="EARTH ���������C��" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script type="text/javascript">

        //�E�B���h�E�T�C�Y�ύX
        try{
            window.resizeTo(880,650);
        }catch(e){
            //�A�N�Z�X�����ۂ���܂����̃G���[���o���牽�����Ȃ��B
            if(e.number == 2147024891) throw e;
        }
        
        //�R���g���[���ړ���
        var gVarOyaSettouji = "ctl00_CPH1_";
        var gVarSelectSyubetu = "_SelectSyubetu";
        var gVarSelectCode = "SelectCode";
        var gVarTextHizuke = "_TextHizuke";
        var gVarTextHanyouCode = "_TextHanyouCode";
        var gVarTextAreaNaiyou = "_TextAreaNaiyou";
               
        var gVarHiddenBunrui = "_HiddenBunrui";

        var gVarSelectTmpSyubetu = "SelectSyubetu_";
         
        var gVarFocus = '';
        
        _d = document;
                
        /****************************************
         * onload���̒ǉ�����
         ****************************************/
        function funcAfterOnload() {
        
            // ���ރv���_�E���̒l�\����؂�ւ�
            ChgDispSelectBunrui();
           
            //�t�H�[�J�X����
            if(gVarFocus != ''){
                setFocus(gVarFocus);
                gVarFocus = '';
            }
            
        }
        
        //�t�H�[�J�X����
        //���T�[�o�[�R���g���[���ȊO�A���[�U�[�R���g���[������e�Ƀt�H�[�J�X������������ȏꍇ�Ɏg�p
        function setFocus(objID){
            var objTmp = objID;
            
            if(objID.indexOf(gVarSelectSyubetu) != -1){ //���
            }else if(objID.indexOf(gVarHiddenBunrui) != -1){ //����
                objTmp = objEBI("SelectBunrui");
                objTmp.selectedIndex = 0;
                objTmp.focus();
                return true;
            }else if(objID.indexOf(gVarTextHizuke) != -1){ //���t
            }else if(objID.indexOf(gVarTextHanyouCode) != -1){ //�ėp�R�[�h
            }else if(objID.indexOf(gVarTextAreaNaiyou) != -1){ //���e
            }else{
                return false;
            }
            objEBI(objTmp).focus();
        }
        
        
        // ���ރv���_�E���̒l�\����؂�ւ�
        function ChgDispSelectBunrui(){
            var objSelSyubetu = objEBI("<%=SelectSyubetu.clientID %>");
            var objSelBunrui = objEBI("SelectBunrui");
            
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
            SelectOptionSet(objSelBunrui);
                
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
        function SelectOptionSet(objSel){
            //Hidden�̎擾����уZ�b�g
            var objHdnBunrui = objEBI("<%=HiddenBunrui.clientID %>");
            objSel.value = objHdnBunrui.value;
        }
        
        //���ރh���b�v�_�E�����X�g�ύX��Hidden���X�V����
        function UpdHiddenBunrui(objSel){
            if(objSel == undefined) return false;
                        
            //Hidden���ނ̎擾
            var objHdnCode = objEBI("<%=HiddenBunrui.clientID %>");
            if(objHdnCode == undefined) return false;
            
            if(objSel.value == ''){
                objHdnCode.value = '';
            }else{
                objHdnCode.value = objSel.value;
            }
        }
        
        //��ʕύX���A���ނ̒��g�����ς���
        function SelectSyubetuOnChg(objID){            
            //Hidden���ނ̏�����
            var objHdnBunrui = objEBI("<%=HiddenBunrui.clientID %>");
            if(objHdnBunrui == undefined) return false;
            objHdnBunrui.value = '';
            
            //�I�u�W�F�N�g�̎擾
            var objSelSyubetu = objEBI("<%=SelectSyubetu.clientID %>");
            if(objSelSyubetu == undefined) return false;
            
            //�I�u�W�F�N�g�̎擾
            var objSelBunrui = objEBI("SelectBunrui");
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
        
        //�e��ʃ����[�h����
        function OyaReload(){
            //�e�E�B���h�E�����Ă��Ȃ����̃`�F�b�N
            if(window.opener == null || window.opener.closed){
                alert("�Ăяo������ʂ�����ꂽ�ׁA�����[�h�ł��܂���ł����B");
                return false;
            }else{               
                var _wod = window.opener.document; //�e�E�B���h�E�̃h�L�������g�I�u�W�F�N�g
                var afterEventBtnId = _wod.getElementById("afterEventBtnId").value;  //�l�Z�b�g��ɉ�������A�e��ʂ̃{�^��ID
                
                //�l�Z�b�g��ɐe��ʂ̃{�^��������
                if(afterEventBtnId != undefined && afterEventBtnId != "" && _wod.getElementById(afterEventBtnId) != undefined){
                  _wod.getElementById(afterEventBtnId).fireEvent("onclick");
                }
                
                //�e�E�B���h�E�փt�H�[�J�X
                window.opener.focus();
                
                //���g�����
                window.close();
                
            }
        }
        
    </script>

    <div>
        <input type="hidden" id="HiddenUpdDatetime" runat="server" />
        <input type="hidden" id="HiddenNyuuryokuNo" runat="server" />
        <input type="hidden" id="HiddenGamenMode" runat="server" />
        <div id="divSelect" runat="server">
        </div>
        <asp:DropDownList runat="server" ID="SelectTmpCode" Style="display: none;">
        </asp:DropDownList>
        <%-- ��ʃ^�C�g�� --%>
        <table>
            <tr>
                <td>
                    <table style="text-align: left; width: 800px;" border="0" cellpadding="0" cellspacing="2"
                        class="titleTable">
                        <tr>
                            <th style="text-align: left; width: 150px;">
                                <span id="SpanTitle" runat="server"></span>
                            </th>
                            <th>
                                <input type="button" id="ButtonClose" runat="server" value="����" onclick="window.close();" />
                            </th>
                            <th style="width: 600px">
                                &nbsp;</th>
                            <th style="text-align: right">
                                <input type="button" id="ButtonUpdate"  runat="server" style="width: 80px;" />
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
        <%-- ��ʃ��C��[�����������] --%>
        <table cellpadding="0" cellspacing="0" style="width:800px; border-bottom: solid 2px gray; border-left: solid 2px gray;"
            class="mainTable">
            <tr>
                <td class="koumokuMei" style="">
                    ���</td>
                <td>
                    <asp:DropDownList runat="server" ID="SelectSyubetu" Width="300px" CssClass="hissu">
                    </asp:DropDownList><span id="SpanSyubetu" runat="server"></span>
                    <input type="hidden" id="HiddenSyubetu" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="koumokuMei" style="">
                    ����</td>
                <td>
                    <select id="SelectBunrui" style="width: 300px" onchange="UpdHiddenBunrui(this)" class="hissu">
                    </select>
                    <input type="hidden" id="HiddenBunrui" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="koumokuMei" style="">
                    ���t</td>
                <td>
                    <asp:TextBox runat="server" ID="TextHizuke" MaxLength="10" CssClass="date" />
                </td>
            </tr>
            <tr>
                <td class="koumokuMei" style="">
                    �ėp�R�[�h</td>
                <td>
                    <asp:TextBox runat="server" ID="TextHanyouCode" Style="width: 200px;" CssClass="codeNumber"
                        MaxLength="20" />
                </td>
            </tr>
            <tr>
                <td class="koumokuMei" style="">
                    ���e</td>
                <td>
                    <textarea id="TextAreaNaiyou" runat="server" cols="100" onfocus="this.select();" rows="7"
                        style="ime-mode: active; font-family: Sans-Serif"></textarea>
                </td>
            </tr>
            <tr>
                <td class="koumokuMei" style="">
                    �o�^��</td>
                <td>
                    <asp:TextBox runat="server" ID="TextTourokuSya" Style="width: 100px;" ReadOnly="true"
                        CssClass="readOnlyStyle" TabIndex="-1" />
                </td>
            </tr>
            <tr>
                <td class="koumokuMei" style="">
                    �o�^����</td>
                <td>
                    <asp:TextBox runat="server" ID="TextTourokuDate" Style="width: 100px;" ReadOnly="true"
                        CssClass="readOnlyStyle" TabIndex="-1" />
                </td>
            </tr>
            <tr>
                <td class="koumokuMei" style="">
                    �ŏI�X�V��</td>
                <td>
                    <asp:TextBox runat="server" ID="TextKousinSya" Style="width: 100px;" ReadOnly="true"
                        CssClass="readOnlyStyle" TabIndex="-1" />
                </td>
            </tr>
            <tr>
                <td class="koumokuMei" style="">
                    �ŏI�X�V�ғ���</td>
                <td>
                    <asp:TextBox runat="server" ID="TextKousinDate" Style="width: 100px;" ReadOnly="true"
                        CssClass="readOnlyStyle" TabIndex="-1" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

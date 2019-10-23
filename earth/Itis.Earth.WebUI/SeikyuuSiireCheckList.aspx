<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="SeikyuuSiireCheckList.aspx.vb" Inherits="Itis.Earth.WebUI.SeikyuuSiireCheckList"
    Title="EARTH �����E�d���f�[�^�`�F�b�N���X�g" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script type="text/javascript">
    <!--
    //���W�I�{�^���I��ʐ���
    function js_ChgRadioControl(strCTLID){
    	
    	//�I���Z���̔w�i�F��ύX
    	setSelected(strCTLID);
    	
    	//��ʒ������E�\���ؑ�
    	js_ChgDisplay(strCTLID);

    }
    
    //�I���Z���̔w�i�F��ύX
    var objBeforSelectedTd = null;	//�O��I������Ă������W�I�{�^��
    var strBeforSelectedTdClass = "hissu";	//�f�t�H���g��class
    function setSelected(strCTLID){
    	var objSelectedTd = document.getElementById(strCTLID).parentNode;
    	var varCtlNo = strCTLID.replace('rdMS','');
    	if(varCtlNo < 6){
	    	objSelectedTd.className = "selectedStyleG";
    	}else{
        	objSelectedTd.className = "selectedStyleB";
    	}
    	if(objBeforSelectedTd != objSelectedTd && objBeforSelectedTd != null)objBeforSelectedTd.className = "hissu";
    	objBeforSelectedTd = objSelectedTd;
    }

    //��ʒ������E�\���ؑ�
    function js_ChgDisplay(strCTLID){
    	var varCtlNo = strCTLID.replace('rdMS','');
    	var varSpn = 'spnMS';
    	var varTxt = 'txtMS';
    	
    	switch(varCtlNo){
		    case '1': //[�����f�[�^]����
		    case '2': //[�����f�[�^]�H��
		    case '3': //[�����f�[�^]�o�^��
		    case '4': //[�����f�[�^]�̑��i
		    case '5': //[�����f�[�^]�ۏ؏��Ĕ��s
		    	gJs_SetInnerCTLToValue('spnData','�����f�[�^:');
		    	document.getElementById('spnData').style.color = 'green';
		    	document.getElementById('spnRadio').style.color = 'green';
		        break;
		    case '6': //[�d���f�[�^]����
		    case '7': //[�d���f�[�^]�H��
		    	gJs_SetInnerCTLToValue('spnData','�d���f�[�^:');
		    	document.getElementById('spnData').style.color = 'blue';
		    	document.getElementById('spnRadio').style.color = 'blue';
		        break;
		    default:
		        break;
		}
		//�l�Z�b�g
		gJs_SetInnerCTLtoInnerCTL('spnRadio',varSpn + varCtlNo);
    }
    
    //������InnerHTML
    function gJs_SetInnerCTLToValue(strID1,strValue){
    	document.getElementById(strID1).innerHTML = strValue;
    }
    //CTL��InnerHTML
    function gJs_SetInnerCTLtoCTL(strID1,strID2){
    	document.getElementById(strID1).innerHTML = document.getElementById(strID2).value;
    	document.getElementById(strID1).style.color = document.getElementById(strID2).style.color;
    }
    //InnerHTML��InnerHTML
    function gJs_SetInnerCTLtoInnerCTL(strID1,strID2){
    	document.getElementById(strID1).innerHTML = document.getElementById(strID2).innerHTML;
    }
    
    function getNowString(plusHour){
		var nowdate = new Date();
		var year = nowdate.getFullYear(); // �N
		var mon  = addZero(nowdate.getMonth() + 1); // ��
		var date = addZero(nowdate.getDate()); // ��
		var week = nowdate.getDay(); // �j��
		var hour = addZero(nowdate.getHours() + plusHour); // ��
		var min  = addZero(nowdate.getMinutes()); // ��
		var sec  = addZero(nowdate.getSeconds()); // �b
		var msec = addZero(nowdate.getMilliseconds()); // �~���b 
		
		var retStr = year + "/" + mon + "/" + date + " " + hour + ":" + min + ":" + sec;
		return retStr;
    }
    
    function addZero(parm){
    	if(parm < 10)parm = "0" + String(parm);
    	return parm;
    }
    
	// -->
    </script>

    <!-- ��ʏ㕔�E�w�b�_ -->
    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tbody>
            <tr>
                <th style="text-align: left; width: 250px;">
                    �����E�d���f�[�^�`�F�b�N���X�g
                </th>
            </tr>
            <tr>
                <td style="height: 10px">
                </td>
            </tr>
        </tbody>
    </table>
    <!-- ��ʏ㕔 -->
    <table style="text-align: left;" id="" class="mainTable" cellpadding="1" cellspacing="1"
        border="0">
        <!-- �{�f�B�� -->
        <tbody id="Tbody1" class="scrolltablestyle">
            <!-- 1�s�� -->
            <tr>
                <td style="width: 80px" class="koumokuMei">
                    �����f�[�^
                </td>
                <td class="hissu" style="width: 120px">
                    <input type="radio" name="rdMS" id="rdMS1" value="1" onclick="js_ChgRadioControl(this.id)" />
                    <span id="spnMS1" style="display: inline;">����</span>
                </td>
                <td class="hissu" style="width: 120px">
                    <input type="radio" name="rdMS" id="rdMS2" value="2" onclick="js_ChgRadioControl(this.id)" />
                    <span id="spnMS2" style="display: inline;">�H��</span>
                </td>
                <td class="hissu" style="width: 120px">
                    <input type="radio" name="rdMS" id="rdMS3" value="3" onclick="js_ChgRadioControl(this.id)" />
                    <span id="spnMS3" style="display: inline;">�o�^��</span>
                </td>
                <td class="hissu" style="width: 120px">
                    <input type="radio" name="rdMS" id="rdMS4" value="4" onclick="js_ChgRadioControl(this.id)" />
                    <span id="spnMS4" style="display: inline;">�̑��i</span>
                </td>
                <td class="hissu" style="width: 120px">
                    <input type="radio" name="rdMS" id="rdMS5" value="5" onclick="js_ChgRadioControl(this.id)" />
                    <span id="spnMS5" style="display: inline;">�ۏ؏��Ĕ��s</span>
                </td>
            </tr>
            <!-- 2�s�� -->
            <tr>
                <td style="width: 80px" class="koumokuMei">
                    �d���f�[�^
                </td>
                <td class="hissu">
                    <input type="radio" name="rdMS" id="rdMS6" value="1" onclick="js_ChgRadioControl(this.id)" />
                    <span id="spnMS6" style="display: inline;">����</span>
                </td>
                <td class="hissu">
                    <input type="radio" name="rdMS" id="rdMS7" value="2" onclick="js_ChgRadioControl(this.id)" />
                    <span id="spnMS7" style="display: inline;">�H��</span>
                </td>
                <td colspan="3" rowspan="2">
                </td>
            </tr>
            <!-- 3�s�� -->
            <tr>
                <td colspan="6" style="text-align: center; height: 60px;">
                    <span id="spnData" style="font-size: x-large;"></span><span id="spnRadio" style="font-size: x-large;">
                    </span>&nbsp;&nbsp; <span id="spnLastProcDate" style="font-size: x-large;"></span>
                </td>
            </tr>
            <!-- 4�s�� -->
            <tr>
                <td style="width: 80px" class="koumokuMei">
                    ����N����
                </td>
                <td colspan="5">
                    <input type="text" name="txtUriStart" id="txtUriStart" value="" maxlength="10" class="date"
                        tabindex="" style="" />
                    &nbsp;&nbsp;�`&nbsp;&nbsp;
                    <input type="text" name="txtUriEnd" id="Text1" value="" maxlength="10" class="date"
                        tabindex="" style="" />
                </td>
            </tr>
            <!-- 5�s�� -->
            <tr>
                <td colspan="6" class="tableFooter" style="padding: 0px;">
                    <!-- ��ʉ����E�{�^�� -->
                    <table cellpadding="5" cellspacing="0" class="subTable" style="width: 100%" border="0">
                        <tr>
                            <td>
                                <input type="button" name="btnFunc1" id="btnFunc1" value="�`�F�b�N���X�g�쐬" style="width: 120px;
                                    height: 30px" />&nbsp;
                                <input type="button" name="btnFunc6" id="Button1" value="�o��" style="width: 120px;
                                    height: 30px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>

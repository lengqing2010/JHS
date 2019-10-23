<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="SeikyuuSiireCheckList.aspx.vb" Inherits="Itis.Earth.WebUI.SeikyuuSiireCheckList"
    Title="EARTH 請求・仕入データチェックリスト" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script type="text/javascript">
    <!--
    //ラジオボタン選択別制御
    function js_ChgRadioControl(strCTLID){
    	
    	//選択セルの背景色を変更
    	setSelected(strCTLID);
    	
    	//画面中央部・表示切替
    	js_ChgDisplay(strCTLID);

    }
    
    //選択セルの背景色を変更
    var objBeforSelectedTd = null;	//前回選択されていたラジオボタン
    var strBeforSelectedTdClass = "hissu";	//デフォルトのclass
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

    //画面中央部・表示切替
    function js_ChgDisplay(strCTLID){
    	var varCtlNo = strCTLID.replace('rdMS','');
    	var varSpn = 'spnMS';
    	var varTxt = 'txtMS';
    	
    	switch(varCtlNo){
		    case '1': //[請求データ]調査
		    case '2': //[請求データ]工事
		    case '3': //[請求データ]登録料
		    case '4': //[請求データ]販促品
		    case '5': //[請求データ]保証書再発行
		    	gJs_SetInnerCTLToValue('spnData','請求データ:');
		    	document.getElementById('spnData').style.color = 'green';
		    	document.getElementById('spnRadio').style.color = 'green';
		        break;
		    case '6': //[仕入データ]調査
		    case '7': //[仕入データ]工事
		    	gJs_SetInnerCTLToValue('spnData','仕入データ:');
		    	document.getElementById('spnData').style.color = 'blue';
		    	document.getElementById('spnRadio').style.color = 'blue';
		        break;
		    default:
		        break;
		}
		//値セット
		gJs_SetInnerCTLtoInnerCTL('spnRadio',varSpn + varCtlNo);
    }
    
    //引数⇒InnerHTML
    function gJs_SetInnerCTLToValue(strID1,strValue){
    	document.getElementById(strID1).innerHTML = strValue;
    }
    //CTL⇒InnerHTML
    function gJs_SetInnerCTLtoCTL(strID1,strID2){
    	document.getElementById(strID1).innerHTML = document.getElementById(strID2).value;
    	document.getElementById(strID1).style.color = document.getElementById(strID2).style.color;
    }
    //InnerHTML⇒InnerHTML
    function gJs_SetInnerCTLtoInnerCTL(strID1,strID2){
    	document.getElementById(strID1).innerHTML = document.getElementById(strID2).innerHTML;
    }
    
    function getNowString(plusHour){
		var nowdate = new Date();
		var year = nowdate.getFullYear(); // 年
		var mon  = addZero(nowdate.getMonth() + 1); // 月
		var date = addZero(nowdate.getDate()); // 日
		var week = nowdate.getDay(); // 曜日
		var hour = addZero(nowdate.getHours() + plusHour); // 時
		var min  = addZero(nowdate.getMinutes()); // 分
		var sec  = addZero(nowdate.getSeconds()); // 秒
		var msec = addZero(nowdate.getMilliseconds()); // ミリ秒 
		
		var retStr = year + "/" + mon + "/" + date + " " + hour + ":" + min + ":" + sec;
		return retStr;
    }
    
    function addZero(parm){
    	if(parm < 10)parm = "0" + String(parm);
    	return parm;
    }
    
	// -->
    </script>

    <!-- 画面上部・ヘッダ -->
    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tbody>
            <tr>
                <th style="text-align: left; width: 250px;">
                    請求・仕入データチェックリスト
                </th>
            </tr>
            <tr>
                <td style="height: 10px">
                </td>
            </tr>
        </tbody>
    </table>
    <!-- 画面上部 -->
    <table style="text-align: left;" id="" class="mainTable" cellpadding="1" cellspacing="1"
        border="0">
        <!-- ボディ部 -->
        <tbody id="Tbody1" class="scrolltablestyle">
            <!-- 1行目 -->
            <tr>
                <td style="width: 80px" class="koumokuMei">
                    請求データ
                </td>
                <td class="hissu" style="width: 120px">
                    <input type="radio" name="rdMS" id="rdMS1" value="1" onclick="js_ChgRadioControl(this.id)" />
                    <span id="spnMS1" style="display: inline;">調査</span>
                </td>
                <td class="hissu" style="width: 120px">
                    <input type="radio" name="rdMS" id="rdMS2" value="2" onclick="js_ChgRadioControl(this.id)" />
                    <span id="spnMS2" style="display: inline;">工事</span>
                </td>
                <td class="hissu" style="width: 120px">
                    <input type="radio" name="rdMS" id="rdMS3" value="3" onclick="js_ChgRadioControl(this.id)" />
                    <span id="spnMS3" style="display: inline;">登録料</span>
                </td>
                <td class="hissu" style="width: 120px">
                    <input type="radio" name="rdMS" id="rdMS4" value="4" onclick="js_ChgRadioControl(this.id)" />
                    <span id="spnMS4" style="display: inline;">販促品</span>
                </td>
                <td class="hissu" style="width: 120px">
                    <input type="radio" name="rdMS" id="rdMS5" value="5" onclick="js_ChgRadioControl(this.id)" />
                    <span id="spnMS5" style="display: inline;">保証書再発行</span>
                </td>
            </tr>
            <!-- 2行目 -->
            <tr>
                <td style="width: 80px" class="koumokuMei">
                    仕入データ
                </td>
                <td class="hissu">
                    <input type="radio" name="rdMS" id="rdMS6" value="1" onclick="js_ChgRadioControl(this.id)" />
                    <span id="spnMS6" style="display: inline;">調査</span>
                </td>
                <td class="hissu">
                    <input type="radio" name="rdMS" id="rdMS7" value="2" onclick="js_ChgRadioControl(this.id)" />
                    <span id="spnMS7" style="display: inline;">工事</span>
                </td>
                <td colspan="3" rowspan="2">
                </td>
            </tr>
            <!-- 3行目 -->
            <tr>
                <td colspan="6" style="text-align: center; height: 60px;">
                    <span id="spnData" style="font-size: x-large;"></span><span id="spnRadio" style="font-size: x-large;">
                    </span>&nbsp;&nbsp; <span id="spnLastProcDate" style="font-size: x-large;"></span>
                </td>
            </tr>
            <!-- 4行目 -->
            <tr>
                <td style="width: 80px" class="koumokuMei">
                    売上年月日
                </td>
                <td colspan="5">
                    <input type="text" name="txtUriStart" id="txtUriStart" value="" maxlength="10" class="date"
                        tabindex="" style="" />
                    &nbsp;&nbsp;～&nbsp;&nbsp;
                    <input type="text" name="txtUriEnd" id="Text1" value="" maxlength="10" class="date"
                        tabindex="" style="" />
                </td>
            </tr>
            <!-- 5行目 -->
            <tr>
                <td colspan="6" class="tableFooter" style="padding: 0px;">
                    <!-- 画面下部・ボタン -->
                    <table cellpadding="5" cellspacing="0" class="subTable" style="width: 100%" border="0">
                        <tr>
                            <td>
                                <input type="button" name="btnFunc1" id="btnFunc1" value="チェックリスト作成" style="width: 120px;
                                    height: 30px" />&nbsp;
                                <input type="button" name="btnFunc6" id="Button1" value="出力" style="width: 120px;
                                    height: 30px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>

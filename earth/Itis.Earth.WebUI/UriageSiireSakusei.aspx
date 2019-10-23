<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="UriageSiireSakusei.aspx.vb" Inherits="Itis.Earth.WebUI.UriageSiireSakusei"
    Title="EARTH 売上・仕入データ確定" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script type="text/javascript">
    <!--
    
        //funcAfterOnload
        function funcAfterOnload(){
            //
            var objSelectedRadioId = objEBI("<%=hiddenSelectedRadioCID.clientID %>").value;
            if(objSelectedRadioId != ""){
                var objSelectedRadio = objEBI(objSelectedRadioId);
                if(objSelectedRadio != undefined)objSelectedRadio.click();
            }
        }
        
        //ラジオボタン選択別制御
        function js_ChgRadioControl(strRadioID, strTextID, intCellNo){
    	    objEBI(strRadioID).checked = true;
        	
    	    //選択セルの背景色を変更
    	    setSelected(strRadioID, intCellNo);
        	
    	    //画面中央部・表示切替
    	    js_ChgDisplay(strRadioID, strTextID, intCellNo);
        	
    	    //画面下部ボタン・表示切替
    	    js_BtnVisible(strRadioID);
        	
    	    js_ChgDenpyou(strRadioID);
            
            //押下されたラジオボタンのIDを取得
            js_GetSelectedRadioCID(intCellNo);
            
    	    //伝票NOクリアボタンの活性化
    	    objEBI('<%= buttonClearDenpyouNoCall.clientID %>').disabled = false;
        }
        
        //選択セルの背景色を変更
        var objBeforSelectedTd = null;	//前回選択されていたラジオボタン
        var strBeforSelectedTdClass = "hissu";	//デフォルトのclass
        function setSelected(strRadioID, intCellNo){
    	    var objSelectedTd = objEBI(strRadioID).parentNode;
    	    var varCtlNo = intCellNo;
    	    if(varCtlNo < 5 || varCtlNo == 7){
	    	    objSelectedTd.className = "selectedStyleG";
    	    }else{
        	    objSelectedTd.className = "selectedStyleB";
    	    }
    	    if(objBeforSelectedTd != objSelectedTd && objBeforSelectedTd != null)objBeforSelectedTd.className = "hissu";
    	    objBeforSelectedTd = objSelectedTd;
        }

        //画面中央部・表示切替
        function js_ChgDisplay(strRadioID, strTextID, intCellNo){
    	    var varCtlNo = intCellNo;
    	    var varSpn = 'spnMS';
        	    	
    	    objEBI('<%= SpanGetujiMessage.clientID %>').style.display = 'none';
    	    switch(varCtlNo){
		        case 2: //[売上データ]邸別(工事)
		            objEBI('<%= SpanGetujiMessage.clientID %>').style.display = 'inline';
		        case 1: //[売上データ]邸別(調査)
		        case 3: //[売上データ]邸別(その他)
		        case 4: //[売上データ]店別
		        case 7: //[売上データ]汎用
		    	    gJs_SetInnerCTLToValue('spnData','売上データ:');
		    	    objEBI('spnData').style.color = 'green';
		    	    objEBI('spnRadio').style.color = 'green';
		    	    // ボタンの活性化
		            objEBI('<%= buttonMakeData.clientID %>').disabled = false;
		            objEBI('<%= buttonMakeDataCall.clientID %>').disabled = false;
		            objEBI('<%= buttonReDownLoad.clientID %>').disabled = false;
		            objEBI('<%= buttonReDownLoadCall.clientID %>').disabled = false;
		            objEBI('<%= buttonMakeDataGetuGaku.clientID %>').disabled = false;
		            objEBI('<%= buttonMakeDataGetuGakuCall.clientID %>').disabled = false;
		            objEBI('<%= buttonReDownLoadGetuGaku.clientID %>').disabled = false;
		            objEBI('<%= buttonReDownLoadGetuGakuCall.clientID %>').disabled = false;
		            break;
		        case 5: //[仕入データ]邸別(調査)
		        case 6: //[仕入データ]邸別(工事)
		        case 8: //[仕入データ]汎用
		    	    gJs_SetInnerCTLToValue('spnData','仕入データ:');
		    	    objEBI('spnData').style.color = 'blue';
		    	    objEBI('spnRadio').style.color = 'blue';
		    	    // ボタンの活性化
		            objEBI('<%= buttonMakeData.clientID %>').disabled = false;
		            objEBI('<%= buttonMakeDataCall.clientID %>').disabled = false;
		            objEBI('<%= buttonReDownLoad.clientID %>').disabled = false;
		            objEBI('<%= buttonReDownLoadCall.clientID %>').disabled = false;
		            break;
		        default:
		            break;
		    }
		    //値セット
		    gJs_SetInnerCTLtoInnerCTL('spnRadio', varSpn + varCtlNo);
		    gJs_SetInnerCTLtoCTL('spnLastProcDate', strTextID);

		    objEBI('<%= SpanKessanMessage.clientID %>').style.display = 'inline';
    		
        }
        
        //押下されたラジオボタンのIDを取得
        function js_GetSelectedRadioCID(intCellNo){
            switch(intCellNo){
                case 1:
		            objEBI("<%=hiddenSelectedRadioCID.clientID %>").value = '<%= radioUriageTyousa.clientID %>'
                    break;
                case 2:
		            objEBI("<%=hiddenSelectedRadioCID.clientID %>").value = '<%= radioUriageKouji.clientID %>'
                    break;
                case 3:
		            objEBI("<%=hiddenSelectedRadioCID.clientID %>").value = '<%= radioUriageHoka.clientID %>'
                    break;
                case 4:
		            objEBI("<%=hiddenSelectedRadioCID.clientID %>").value = '<%= radioUriageTenbetu.clientID %>'
                    break;
                case 5:
		            objEBI("<%=hiddenSelectedRadioCID.clientID %>").value = '<%= radioSiireTyousa.clientID %>'
                    break;
                case 6:
		            objEBI("<%=hiddenSelectedRadioCID.clientID %>").value = '<%= radioSiireKouji.clientID %>'
                    break;
                default:
                    break;
            }
        }
        
        //画面下部ボタン・表示切替
        function js_BtnVisible(strRadioID){
    	    switch(strRadioID){
		        case '<%= radioUriageTyousa.clientID %>': //[売上データ]邸別(調査)
		        case '<%= radioUriageKouji.clientID %>': //[売上データ]邸別(工事)
		        case '<%= radioUriageHoka.clientID %>': //[売上データ]邸別(その他)
		        case '<%= radioSiireTyousa.clientID %>': //[仕入データ]邸別(調査)
		        case '<%= radioSiireKouji.clientID %>': //[仕入データ]邸別(工事)
		    	    //非表示
		    	    objEBI('tdPad').style.display = 'none';
		            break;
		        case '<%= radioUriageTenbetu.clientID %>': //[売上データ]店別
		    	    //常に非表示とする
		    	    objEBI('tdPad').style.display = 'none';
		            break;
		        default:
		            break;
			    }
        }
        
        //伝票番号変更（イメージ）
         function js_ChgDenpyou(strRadioID){
    	    switch(strRadioID){
		        case '<%= radioUriageTyousa.clientID %>': //[売上データ]邸別(調査)
		    	    objEBI('<%= textDenpyou.clientID %>').value = objEBI('<%= hiddenDenpyouNoUriageTyousa.clientID %>').value;
		            break;
		        case '<%= radioUriageKouji.clientID %>': //[売上データ]邸別(工事)
		    	    objEBI('<%= textDenpyou.clientID %>').value = objEBI('<%= hiddenDenpyouNoUriageKouji.clientID %>').value;
		            break;
		        case '<%= radioUriageHoka.clientID %>': //[売上データ]邸別(その他)
		    	    objEBI('<%= textDenpyou.clientID %>').value = objEBI('<%= hiddenDenpyouNoUriageHoka.clientID %>').value;
		            break;
		        case '<%= radioUriageTenbetu.clientID %>': //[売上データ]店別
		    	    objEBI('<%= textDenpyou.clientID %>').value = objEBI('<%= hiddenDenpyouNoUriageTenbetu.clientID %>').value;
		            break;
		        case '<%= radioSiireTyousa.clientID %>': //[仕入データ]邸別(調査)
		    	    objEBI('<%= textDenpyou.clientID %>').value = objEBI('<%= hiddenDenpyouNoSiireTyousa.clientID %>').value;
		            break;
		        case '<%= radioSiireKouji.clientID %>': //[仕入データ]邸別(工事)
		    	    objEBI('<%= textDenpyou.clientID %>').value = objEBI('<%= hiddenDenpyouNoSiireKouji.clientID %>').value;
		            break;
		        default:
		    	    objEBI('<%= textDenpyou.clientID %>').value = '';
		            break;
			    }
        }
        
        //引数⇒InnerHTML
        function gJs_SetInnerCTLToValue(strSpanDataID,strValue){
    	    objEBI(strSpanDataID).innerHTML = strValue;
        }
        //CTL⇒InnerHTML
        function gJs_SetInnerCTLtoCTL(strSpanDateID,strTextID){
    	    objEBI(strSpanDateID).innerHTML = objEBI(strTextID).value;
    	    objEBI(strSpanDateID).style.color = objEBI(strTextID).style.color;
        }
        //InnerHTML⇒InnerHTML
        function gJs_SetInnerCTLtoInnerCTL(strSpanRadioID,strSpanID){
    	    objEBI(strSpanRadioID).innerHTML = objEBI(strSpanID).innerHTML;
        }
        
        //各種ボタン押下時の処理
        function executeConfirm(objCtrl){
            if(objCtrl == objEBI("<%= buttonClearDenpyouNoCall.clientID %>")){
                if(!confirm('<%= Messages.MSG074C %>')){
                    return false;
                }
                setWindowOverlay(objCtrl);
                objEBI("<%= buttonClearDenpyouNo.clientID %>").click();
            }
            if(objCtrl == objEBI("<%= buttonMakeDataCall.clientID %>")){
                setWindowOverlay(objCtrl,objEBI("<%= buttonRelease.clientID %>"));
                objEBI("<%= buttonMakeData.clientID %>").click();
                objEBI('<%= buttonRelease.clientID %>').style.display = 'inline';
            }
            if(objCtrl == objEBI("<%= buttonMakeDataGetuGakuCall.clientID %>")){
                setWindowOverlay(objCtrl,objEBI("<%= buttonRelease.clientID %>"));
                objEBI("<%= buttonMakeDataGetuGaku.clientID %>").click();
                objEBI('<%= buttonRelease.clientID %>').style.display = 'inline';
            }
            if(objCtrl == objEBI("<%= buttonReDownLoadCall.clientID %>")){
                setWindowOverlay(objCtrl,objEBI("<%= buttonRelease.clientID %>"));
                objEBI("<%= buttonReDownLoad.clientID %>").click();
                objEBI('<%= buttonRelease.clientID %>').style.display = 'inline';
            }
            if(objCtrl == objEBI("<%= buttonReDownLoadGetuGakuCall.clientID %>")){
                setWindowOverlay(objCtrl,objEBI("<%= buttonRelease.clientID %>"));
                objEBI("<%= buttonReDownLoadGetuGaku.clientID %>").click();
                objEBI('<%= buttonRelease.clientID %>').style.display = 'inline';
            }
        }    
	// -->
    </script>

    <asp:HiddenField ID="hiddenDenpyouNoUriageTyousa" runat="server" />
    <asp:HiddenField ID="hiddenDenpyouNoUriageKouji" runat="server" />
    <asp:HiddenField ID="hiddenDenpyouNoUriageHoka" runat="server" />
    <asp:HiddenField ID="hiddenDenpyouNoUriageTenbetu" runat="server" />
    <asp:HiddenField ID="hiddenDenpyouNoSiireTyousa" runat="server" />
    <asp:HiddenField ID="hiddenDenpyouNoSiireKouji" runat="server" />
    <asp:HiddenField ID="hiddenSelectedRadioCID" runat="server" />
    <!-- 画面上部・ヘッダ -->
    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tbody>
            <tr>
                <th style="text-align: left; width: 180px;">
                    売上・仕入データ確定
                </th>
                <td>
                    <input type="button" id="buttonUriage" value="売上" runat="server" style="width: 100px;" />
                    <input type="button" id="buttonSiire" value="仕入" runat="server" style="width: 100px;" />
                    <input type="button" id="buttonNyuukin" value="入金" runat="server" style="width: 100px;" />
                    ←押下時点で確定済のデータが統合会計送信予約されます（何度でも可）
                </td>
            </tr>
            <tr>
                <td style="height: 10px">
                </td>
            </tr>
        </tbody>
    </table>
    <!-- 画面上部 -->
    <table style="text-align: left; width: 930px;" id="" class="mainTable" cellpadding="1"
        cellspacing="1" border="0">
        <!-- ヘッダ部 -->
        <thead>
            <tr>
                <th class="tableTitle" style="padding: 0px; background-color: #ffffce;" colspan="5">
                    データ作成
                </th>
            </tr>
        </thead>
        <!-- ボディ部 -->
        <tbody id="Tbody1" class="scrolltablestyle">
            <!-- 1行目 -->
            <tr style="height: 50px;">
                <td style="width: 100px" class="koumokuMei">
                    売上データ
                </td>
                <td class="hissu" id="tdUriageTyousa" runat="server">
                    <input type="radio" id="radioUriageTyousa" runat="server" value="1" />
                    <span id="spnMS1" style="display: inline;">邸別(調査)</span><br />
                    <input type="text" id="textUriageTyousa" class="hissu" style="width: 150px; border-style: none;"
                        value="2009/05/01 11:11:11" readonly="readOnly" runat="server" />
                </td>
                <td class="hissu" id="tdUriageKouji" runat="server">
                    <input type="radio" id="radioUriageKouji" runat="server" value="2" />
                    <span id="spnMS2" style="display: inline;">邸別(工事)</span><br />
                    <input type="text" id="textUriageKouji" class="hissu" style="width: 150px; border-style: none;"
                        value="YYYY/MM/DD hh:mm:ss" readonly="readOnly" runat="server" />
                </td>
                <td class="hissu" id="tdUriageHoka" runat="server">
                    <input type="radio" id="radioUriageHoka" runat="server" value="3" />
                    <span id="spnMS3" style="display: inline;">邸別(その他)</span><br />
                    <input type="text" id="textUriageHoka" class="hissu" style="width: 150px; border-style: none;"
                        value="YYYY/MM/DD hh:mm:ss" readonly="readOnly" runat="server" />
                </td>
                <td class="hissu" id="tdUriageTenbetu" runat="server">
                    <input type="radio" id="radioUriageTenbetu" runat="server" value="4" />
                    <span id="spnMS4" style="display: inline;">店別</span><br />
                    <input type="text" id="textUriageTenbetu" class="hissu" style="width: 150px; border-style: none;"
                        value="YYYY/MM/DD hh:mm:ss" readonly="readOnly" runat="server" />
                </td>
            </tr>
            <!-- 2行目 -->
            <tr style="height: 50px;">
                <td style="width: 100px" class="koumokuMei">
                    仕入データ
                </td>
                <td class="hissu" id="tdSiireTyousa" runat="server">
                    <input type="radio" id="radioSiireTyousa" runat="server" value="5" />
                    <span id="spnMS5" style="display: inline;">邸別(調査)</span><br />
                    <input type="text" id="textSiireTyousa" class="hissu" style="width: 150px; border-style: none;"
                        value="YYYY/MM/DD hh:mm:ss" readonly="readOnly" runat="server" />
                </td>
                <td class="hissu" id="tdSiireKouji" runat="server">
                    <input type="radio" id="radioSiireKouji" runat="server" value="6" />
                    <span id="spnMS6" style="display: inline;">邸別(工事)</span><br />
                    <input type="text" id="textSiireKouji" class="hissu" style="width: 150px; border-style: none;"
                        value="YYYY/MM/DD hh:mm:ss" readonly="readOnly" runat="server" />
                </td>
                <td colspan="2" rowspan="1">
                </td>
            </tr>
            <!-- 5行目 -->
            <tr style="height: 100px;">
                <td colspan="5" style="text-align: center; height: 60px;">
                    <span id="spnData" style="font-size: x-large;"></span><span id="spnRadio" style="font-size: x-large;">
                    </span>&nbsp;&nbsp; <span id="spnLastProcDate" style="font-size: x-large;"></span>
                    <br />
                    <br />
                    <span id="SpanGetujiMessage" runat="server" style="font-size: 18px; color: red; font-weight: bold;
                        display: none;">データ更新前に月次処理を忘れずに </span>&nbsp;&nbsp;&nbsp;&nbsp; <span id="SpanKessanMessage"
                            runat="server" style="font-size: 18px; color: #FF00FF; font-weight: bold; display: none;">
                            データ更新前に決算月処理を忘れずに </span>
                </td>
            </tr>
            <!-- 7行目 -->
            <tr>
                <td style="width: 100px" class="koumokuMei">
                    売上年月日
                </td>
                <td colspan="4">
                    <input type="text" name="textUriFrom" id="textUriFrom" value="" maxlength="10" class="date"
                        tabindex="" style="" runat="server" onblur="checkDate(this);" />
                    &nbsp;&nbsp;～&nbsp;&nbsp;
                    <input type="text" name="textUriTo" id="textUriTo" value="" maxlength="10" class="date"
                        tabindex="" style="" runat="server" onblur="checkDate(this);" />
                </td>
            </tr>
            <!-- 8行目 -->
            <!-- 9行目 -->
            <tr>
                <td style="text-align: center; height: 60px;" colspan="5">
                    現在の伝票NO：&nbsp;
                    <input type="text" id="textDenpyou" runat="server" class="readOnlyStyle" style="width: 7em;
                        text-align: center;" value="000001" readonly="readonly" />&nbsp;&nbsp;
                    <input type="button" id="buttonClearDenpyouNoCall" runat="server" class="" style=""
                        value="伝票NOのクリア" onclick="executeConfirm(this)" />
                    <input type="button" id="buttonClearDenpyouNo" runat="server" class="" style="display: none"
                        value="伝票NOのクリア" />
                </td>
            </tr>
            <!-- 10行目 -->
            <tr>
                <td colspan="5" class="tableFooter" style="padding: 0px;">
                    <!-- 画面下部・ボタン -->
                    <table cellpadding="5" cellspacing="0" class="subTable" style="width: 100%; height: 110px;"
                        border="0">
                        <tr style="height: 80px;">
                            <td>
                                <input type="button" id="buttonMakeDataCall" runat="server" value="データ作成" style="width: 180px;
                                    height: 25px" onclick="executeConfirm(this)" />
                                <input type="button" id="buttonMakeData" runat="server" value="データ作成" style="width: 180px;
                                    height: 25px; display: none;" /><br />
                                <br />
                                <input type="button" id="buttonReDownLoadCall" runat="server" value="再ダウンロード" style="width: 180px;
                                    height: 25px" onclick="executeConfirm(this)" />
                                <input type="button" id="buttonReDownLoad" runat="server" value="再ダウンロード" style="width: 180px;
                                    height: 25px; display: none;" />
                            </td>
                            <td id="tdPad" style="display: none; border-left: 1px solid gray;">
                                <fieldset id="fsBtn" style="padding: 10px;">
                                    <legend>月額実績割引</legend>
                                    <input type="button" id="buttonMakeDataGetuGakuCall" runat="server" value="データ作成"
                                        style="width: 180px; height: 25px" onclick="executeConfirm(this)" />
                                    <input type="button" id="buttonMakeDataGetuGaku" runat="server" value="データ作成" style="width: 180px;
                                        height: 25px; display: none;" /><br />
                                    <br />
                                    <input type="button" id="buttonReDownLoadGetuGakuCall" runat="server" value="再ダウンロード"
                                        style="width: 180px; height: 25px" onclick="executeConfirm(this)" />
                                    <input type="button" id="buttonReDownLoadGetuGaku" runat="server" value="再ダウンロード"
                                        style="width: 180px; height: 25px; display: none;" />
                                </fieldset>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
    <input id="buttonRelease" type="button" value="画面制御解除" runat="server" class="GamenSeigyoKaijoButton"
        onclick="this.disabled = true;" />
</asp:Content>

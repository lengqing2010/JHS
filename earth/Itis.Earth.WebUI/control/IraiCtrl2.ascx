<%@ Control Language="vb" AutoEventWireup="false" Codebehind="IraiCtrl2.ascx.vb"
    Inherits="Itis.Earth.WebUI.IraiCtrl2" %>
<%@ Import Namespace="Itis.Earth.BizLogic" %>
<%@ Import Namespace="Itis.Earth.Utilities" %>
<%@ Register Src="SeikyuuSiireLinkCtrl.ascx" TagName="SeikyuuSiireLinkCtrl" TagPrefix="uc" %>
<%@ Register Src="TokubetuTaiouToolTipCtrl.ascx" TagName="TokubetuTaiouToolTipCtrl" TagPrefix="uc" %>

<script language="javascript" type="text/javascript">
history.forward();

/*
function pageLoad() {
  var dropdowns = document.getElementsByTagName("select");
  if (dropdowns)
  for (i=0; i < dropdowns.length; i++){
    dropdowns[i].style.display = "inline";
  }
}
*/

var _gStrDoujiIrai = null; //同時依頼棟数

/**
 * ## onChange代替処理用(同時依頼棟数専用) ##
 *   テンポラリグローバル変数に、対象オブジェクトの値をセットする
 *   ※onfocusイベントハンドラからコール
 * @param obj:対象オブジェクト
 */
function setTempValueForOnBlur_DoujiIrai(obj){
  _gStrDoujiIrai = obj.value;
}

/**
 * ## onChange代替処理用(同時依頼棟数専用) ##
 *   テンポラリグローバル変数と、対象オブジェクトの値を比較する
 *     値がテンポラリと異なっている＝値が変更されている：True
 *     値がテンポラリと異なっていない＝値が変更されていない：False
 *   ※onblurイベントハンドラからコール
 * @param obj:対象オブジェクト
 * @return 変更されているか否か(Boolean)
 */
function checkTempValueForOnBlur_DoujiIrai(obj){
  return _gStrDoujiIrai != obj.value;
}

//商品検索処理呼び出し
function callSearchSyouhin(ln, strTargetIds, returnTargetIds, afterEventBtnId){
    objEBI("<%= targetLine_SearchSyouhin23.clientID %>").value = ln;                    //対象行番号
    objEBI("<%= strTargetIds_SearchSyouhin23.clientID %>").value = strTargetIds;        //画面引渡しオブジェクトID郡
    objEBI("<%= returnTargetIds_SearchSyouhin23.clientID %>").value = returnTargetIds;  //戻り値セット先オブジェクトID郡
    objEBI("<%= afterEventBtnId_SearchSyouhin23.clientID %>").value = afterEventBtnId;  //アフターイベントオブジェクトID
    //ボタン押下
    objEBI("<%= btnSearchSyouhin23.clientID %>").click();
}

//商品１設定処理呼び出し
var tmpAjValS1 = null;
function callSetSyouhin1(objThis){
    if(flgAjaxRunning){
        //Ajax処理中は、待つ
        if(tmpAjValS1==null)tmpAjValS1 = objThis.value;
        setTimeout(function(){callSetSyouhin1(objThis)},100);
    }else{
        if(tmpAjValS1!=null)objEBI(objThis.id).value = tmpAjValS1;
        
        if(objThis.id.indexOf('choSyouhin1') > 0){
            var objBangou = objEBI("<%= kameitenCd.clientID %>"); //加盟店コード
    		var objSyouhin1 = objEBI("<%= choSyouhin1.clientID %>"); //商品1
        	objEBI("<%= kameitenSearchType.clientID %>").value = "";
        	objEBI("<%= kameitenSearchFlg.clientID %>").value = "";
        	objEBI("<%= HiddenModeKakunin.clientID %>").value = "0";
        	if(objSyouhin1.value == ""){
        		objEBI("<%= kameitenSearchType.clientID %>").value = "1";
        		objEBI("<%= kameitenSearchFlg.clientID %>").value = "1";
        	}else{
        		if(objBangou.value != "" && objSyouhin1.value != ""){
        		    objEBI("<%= kameitenSearchFlg.clientID %>").value = "1";
            	}
        	}
        }
        
        objEBI("<%= actCtrlId.clientID %>").value = objThis.id; //実行トリガーオブジェクトID
        //ボタン押下
        tmpAjValS1 = null;
        objEBI("<%= btnSetSyouhin1.clientID %>").click();
    }
}

//調査概要設定処理呼び出し
var tmpAjValT1 = null;
function callSetTysGaiyou(objThis){
    if(flgAjaxRunning){
        //Ajax処理中は、待つ
        if(tmpAjValT1==null)tmpAjValT1 = objThis.value;
        setTimeout(function(){callSetTysGaiyou(objThis)},100);
    }else{
        if(tmpAjValT1!=null)objEBI(objThis.id).value = tmpAjValT1;
        
            objEBI("<%= kameitenSearchFlg.clientID %>").value = "";
            if(objThis.id.indexOf('choSyouhin1') > 0){
                var objBangou = objEBI("<%= kameitenCd.clientID %>"); //加盟店コード
    		    var objSyouhin1 = objEBI("<%= choSyouhin1.clientID %>"); //商品1
        	    objEBI("<%= kameitenSearchType.clientID %>").value = "";
        	    objEBI("<%= HiddenModeKakunin.clientID %>").value = "0";
        	    if(objSyouhin1.value == ""){
        		    objEBI("<%= kameitenSearchType.clientID %>").value = "1";
        		    objEBI("<%= kameitenSearchFlg.clientID %>").value = "1";
        	    }else{
        		    if(objBangou.value != "" && objSyouhin1.value != ""){
        		        objEBI("<%= kameitenSearchFlg.clientID %>").value = "1";
            	    }
        	    }
            }
        
        objEBI("<%= actCtrlIdTg.clientID %>").value = objThis.id; //実行トリガーオブジェクトID
        //ボタン押下
        tmpAjValT1 = null;
        objEBI("<%= btnSetTysGaiyou.clientID %>").click();
    }
}

//工務店請求税抜金額変更時処理呼び出し
function callKingakuHenkouKoumuten(objThis,ln){
    objEBI("<%= kingakuHenkouActCId.clientID %>").value = objThis.id; //実行トリガーオブジェクトID
    objEBI("<%= kingakuHenkouLineNo.clientID %>").value = ln; //実行対象行番号
    //ボタン押下
    objEBI("<%= kingakuHenkouKoumuten.clientID %>").click();
}

//実請求税抜金額変更時処理呼び出し
function callKingakuHenkouJituseikyu(objThis,ln){
    objEBI("<%= kingakuHenkouActCId.clientID %>").value = objThis.id; //実行トリガーオブジェクトID
    objEBI("<%= kingakuHenkouLineNo.clientID %>").value = ln; //実行対象行番号
    //ボタン押下
    objEBI("<%= kingakuHenkouJituseikyu.clientID %>").click();
}

//承諾書金額変更時処理呼び出し
function callKingakuHenkouSyoudakusyo(objThis,ln){
    objEBI("<%= kingakuHenkouActCId.clientID %>").value = objThis.id; //実行トリガーオブジェクトID
    objEBI("<%= kingakuHenkouLineNo.clientID %>").value = ln; //実行対象行番号
    //ボタン押下
    objEBI("<%= kingakuHenkouSyoudakusyo.clientID %>").click();
}

// 商品3確定コンボ変更時処理呼び出し
var tmpAjVal1 = null;
function callSyouhin3KakuteiHenkou(objThis,ln){
    if(flgAjaxRunning){
        //Ajax処理中は、待つ
        if(tmpAjVal1==null)tmpAjVal1 = objThis.value;
        setTimeout(function(){callSyouhin3KakuteiHenkou(objThis,ln)},100);
    }else{
        if(tmpAjVal1!=null)objEBI(objThis.id).value = tmpAjVal1;
        objEBI("<%= syouhin3KakuteiHenkouActCId.clientID %>").value = objThis.id; //実行トリガーオブジェクトID
        objEBI("<%= syouhin3KakuteiHenkouLineNo.clientID %>").value = ln; //実行対象行番号
        //ボタン押下
        tmpAjVal = null;
        objEBI("<%= syouhin3KakuteiHenkou.clientID %>").click();
    }
}

// 商品2,3請求有無コンボ変更時処理呼び出し
var tmpAjVal2 = null;
function callSyouhin23SeikyuuUmuHenkou(objThis,ln){
    if(flgAjaxRunning){
        //Ajax処理中は、待つ
        if(tmpAjVal2==null)tmpAjVal2 = objThis.value;
        setTimeout(function(){callSyouhin23SeikyuuUmuHenkou(objThis,ln)},100);
    }else{
        if(tmpAjVal2!=null)objEBI(objThis.id).value = tmpAjVal2;
        objEBI("<%= syouhin23SeikyuuUmuHenkouActCId.clientID %>").value = objThis.id; //実行トリガーオブジェクトID
        objEBI("<%= syouhin23SeikyuuUmuHenkouLineNo.clientID %>").value = ln; //実行対象行番号
        //ボタン押下
        tmpAjVal2 = null;
        objEBI("<%= syouhin23SeikyuuUmuHenkou.clientID %>").click();
    }
}

//発注書確定コンボ変更時処理呼び出し
var tmpAjVal3 = null;
function callHattyuusyoKakuteiHenkou(objThis,ln,ids){
    if(flgAjaxRunning){
        //Ajax処理中は、待つ
        if(tmpAjVal3==null)tmpAjVal3 = objThis.value;
        setTimeout(function(){callHattyuusyoKakuteiHenkou(objThis,ln,ids)},100);
    }else{
        if(tmpAjVal3!=null)objEBI(objThis.id).value = tmpAjVal3;
        objEBI("<%= hattyuusyoHenkouActCId.clientID %>").value = objThis.id; //実行トリガーオブジェクトID
        objEBI("<%= hattyuusyoHenkouLineNo.clientID %>").value = ln; //実行対象行番号
        
        //画面表示部品
        var arrDisId = ids.split(",");
        var objHattyuuKakutei = null;   // 発注書確定
        var objSeikyuuKingaku = null;   // 実請求金額
        var objHattyuuKingaku = null;   // 発注書金額
        
        objHattyuuKakutei = objEBI(objThis.id);
        objSeikyuuKingaku = objEBI(arrDisId[0]);
        objHattyuuKingaku = objEBI(arrDisId[1]);
        
        // 確定の状態
        if (objHattyuuKakutei.value == "1") {
            // 発注金額と実請求金額が異なる場合
            if (objHattyuuKingaku.value != objSeikyuuKingaku.value){
                // 確定して良いか確認する
                if(!confirm("<%= Messages.MSG011C %>")){
                    objHattyuuKakutei.value = "0"
                    //未確定にして反映させる(処理継続)
                }
            }
        }
       
        //ボタン押下
        tmpAjVal3 = null;
        objEBI("<%= hattyuusyoKakuteiHenkou.clientID %>").click();
    }
}

//発注書金額変更時処理呼び出し
function callHattyuusyoKingakuHenkou(objThis,ln,ids){
    objEBI("<%= hattyuusyoHenkouActCId.clientID %>").value = objThis.id; //実行トリガーオブジェクトID
    objEBI("<%= hattyuusyoHenkouLineNo.clientID %>").value = ln; //実行対象行番号
    
        //画面表示部品
    var arrDisId = ids.split(",");
    var objHattyuuKakutei = null;   // 発注書確定
    var objSeikyuuKingaku = null;   // 実請求金額
    var objHattyuuKingaku = null;   // 発注書金額
    var objHattyuuKingakuOld = null;   // 発注書金額
    var objUriageJyoukyou = null;   // 売上状況
    
    objHattyuuKingaku = objThis
    objSeikyuuKingaku = objEBI(arrDisId[0])
    objHattyuuKakutei = objEBI(arrDisId[1])
    objHattyuuKingakuOld = objEBI(arrDisId[2])
    objUriageJyoukyou = objEBI(arrDisId[3])
    
    // 確定の状態
    if (objUriageJyoukyou.value == "売上処理済") {
        // 発注金額OLDが空白
        if (objHattyuuKingakuOld.value == ""){
        
            var chk1 = removeFigure(objHattyuuKingaku.value);
            var chk2 = removeFigure(objSeikyuuKingaku.value);
        
            // 発注金額と実請求金額が同じ場合
            if (chk1 == chk2){
                // 確認無しで確定
                objHattyuuKakutei.value = "1";
            }else{
                objHattyuuKakutei.value = "0";
            }
        }else{
            var chk1 = removeFigure(objHattyuuKingaku.value);
            var chk2 = removeFigure(objSeikyuuKingaku.value);
        
            // 発注金額と実請求金額が同じ場合
            if (chk1 == chk2){
                // 確定して良いか確認する
                if(!confirm("<%= Messages.MSG012C %>")){
                    // このケースはサーバーアクセスしない
                    return false;
                }
            }else{
                objHattyuuKakutei.value = "0";
            }
        }
    }
    
    //ボタン押下
    objEBI("<%= hattyuusyoKingakuHenkou.clientID %>").click();
}

//加盟店検索処理を呼び出す
var JSkameitenSearchType = 0;
function callKameitenSearch(obj){
    objEBI("<%= kameitenSearchType.clientID %>").value = "";
    if(obj.value == ""){
        objEBI("<%= kameitenSearchType.clientID %>").value = "1";
        objEBI("<%= kameitenSearch.clientID %>").click();
    }
}

//調査会社検索処理を呼び出す
var JStyousakaisyaSearchType = 0;
function callTyousakaisyaSearch(obj){
    objEBI("<%= tyousakaisyaSearchType.clientID %>").value = "";
    if(obj.value == ""){
        objEBI("<%= tyousakaisyaSearchType.clientID %>").value = "1";
        objEBI("<%= tyousakaisyaSearch.clientID %>").click();
    }
}

//商品23検索処理を呼び出す
var JSsearchSyouhin23Type = 0;
function callSyouhinSearchOnChange(obj,ln){
    objEBI("<%= searchSyouhin23Type.clientID %>").value = "";
    if(obj.value == ""){
        objEBI("<%= searchSyouhin23Type.clientID %>").value = "1";
        objEBI("<%= ClientID & ClientIDSeparator.ToString %>shouhinSearch_" + ln).click();
    }
}


//変更前の値を保持する
function setPreVal(objThis,preId){
    var setVal = objEBI(preId).value;
    //直前値保持オブジェクトに値をセット
    objEBI("<%= actPreVal.clientID %>").value = setVal;
    objEBI(preId).value = objThis.value;
}

//調査概要と同時依頼棟数のチェック処理[共通]
//調査概要変更時、同時依頼棟数変更時に行なう
function callChkTysGaiyou(objThis){
    var objTysGaiyou = objEBI("<%= cboTyousaGaiyou.clientID %>");
    var strTysGaiyouPreVal = objEBI("<%= tyousaGaiyouPre.clientID %>").value;
    var objIraiTousuu = objEBI("<%= iraiTousuu.clientID %>");
    var strErrMsg = "<%= Messages.MSG145E %>";
    var intIraiTousuu = 1;
    var ReturnVal = ''; //戻り値
    if(objThis.id.indexOf('cboTyousaGaiyou') > 0){
        ReturnVal = strTysGaiyouPreVal;
    }else if(objThis.id.indexOf('iraiTousuu') > 0){
        ReturnVal = _gStrDoujiIrai;
    }
    
    //同時依頼棟数=未入力の場合、"1"として扱う
    if(objIraiTousuu.value == ""){
        intIraiTousuu = 1;
    }else{
        intIraiTousuu = Number(objIraiTousuu.value);
    }
    
    if(objTysGaiyou.value == "62" || objTysGaiyou.value == "63" || objTysGaiyou.value == "64" || objTysGaiyou.value == "65" ){
        if(intIraiTousuu < 10){ //9棟以下
            if(objTysGaiyou.value == "64" || objTysGaiyou.value == "65"){
                strErrMsg = strErrMsg.replace("@PARAM1", "同時依頼棟数");
                strErrMsg = strErrMsg.replace("@PARAM2", "9棟以下");
                strErrMsg = strErrMsg.replace("@PARAM3", "調査概要");
                alert(strErrMsg);
                objThis.value = ReturnVal;
                objThis.focus();
                return false;
            }
        }else{ //10棟以上
            if(objTysGaiyou.value == "62" || objTysGaiyou.value == "63"){
                strErrMsg = strErrMsg.replace("@PARAM1", "同時依頼棟数");
                strErrMsg = strErrMsg.replace("@PARAM2", "10棟以上");
                strErrMsg = strErrMsg.replace("@PARAM3", "調査概要");
                alert(strErrMsg);
                objThis.value = ReturnVal;
                objThis.focus();
                return false;
            }
        }
    }
    return true;
}

//調査概要とビルダー注意事項のチェック処理[共通]
//調査概要変更時に行なう
function callChkBuilder(objThis){
    var objTysGaiyou = objEBI("<%= cboTyousaGaiyou.clientID %>");
    var strTysGaiyouPreVal = objEBI("<%= tyousaGaiyouPre.clientID %>").value;
    var objBuilderFlg = objEBI("<%= HiddenKameitenTyuuiJikou.clientID %>");
    var strErrMsg = "<%= Messages.MSG146E %>";
    var ReturnVal = ''; //戻り値
    
    if(objThis.id.indexOf('cboTyousaGaiyou') > 0){
        ReturnVal = strTysGaiyouPreVal;
    }
    
    if(objTysGaiyou.value == "63" || objTysGaiyou.value == "65"){
        if(objBuilderFlg.value == 'False'){
            strErrMsg = strErrMsg.replace("@PARAM1", "加盟店");
            strErrMsg = strErrMsg.replace("@PARAM2", "地盤診断支払代行不可");
            alert(strErrMsg);
            objThis.value = ReturnVal;
            objThis.focus();
            return false;
        }
    }
    return true;
}

//商品1が特別対応価格反映されているかチェック
function ChkTokubetuTaiou(strId){
    var objDisplayCd = objEBI(strId);   //ツールチップDisplayコード
    var strMsg = "<%= Messages.MSG202C %>";
    
    //ツールチップが無い場合は、商品1設定
    if(objDisplayCd == null){
        return true;
    }
    
    //価格反映されている場合、確定メッセージ表示
    if(objDisplayCd.value != ""){
        if(confirm(strMsg)){
            return true;
        }
        return false;
    }
    return true;
}

// Value値をコピーする
// @param コピー元商品1(ClientID)、コピー先商品1(ClientID)
function CopyItemValue(Syouhin1FromId, Syouhin1ToId){
    var objItemPre = objEBI(Syouhin1FromId);
    var objItemAfter = objEBI(Syouhin1ToId);

    objItemAfter.value = objItemPre.value;

}  

        
Sys.WebForms.PageRequestManager.getInstance().add_pageLoading(PageLoadingHandler);
    function PageLoadingHandler(sender, args){
    }
Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args){
    }



</script>

<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
</asp:ScriptManagerProxy>
<input type="hidden" id="iraiSessionKey" runat="server" />
<input type="hidden" id="actMode" style="display: none" runat="server" />
<input type="hidden" id="actModeIrai2" style="display: none" runat="server" />
<input type="hidden" id="nowFocusId" runat="server" />
<input type="hidden" id="HiddenSeikyuuUmuCheck" runat="server" />
<input type="hidden" id="HiddenHosyouSyouhinUmuOld" runat="server" />
<input type="hidden" id="HiddenHosyousyoHakDateOld" runat="server" />
<input type="hidden" id="HiddenKeikakusyoSakuseiDateOld" runat="server" />
<table id="mainTableIrai2" style="text-align: left; width: 100%;" class="mainTable"
    cellpadding="1">
    <thead>
        <tr>
            <th class="tableTitle" colspan="8">
                <a id="irai2DispLink" runat="server">依頼内容</a>
                <input id="btn_irai2" runat="server" class="btnEdit" type="button" value="編集" style="height: 20px" />
                <span id="irai2TitleInfobar" style="display: none;" runat="server"></span>
            </th>
        </tr>
    </thead>
    <tbody id="irai2TBody" runat="server">
        <tr>
            <td colspan="8" class="tableSpacer">
            </td>
        </tr>
        <tr>
            <td class="koumokuMei">
                加盟店</td>
            <td colspan="7" style="padding: 0px 0px 0px 0px;">
                <asp:UpdatePanel ID="UpdatePanel_irai2" UpdateMode="Conditional" runat="server" RenderMode="Inline">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="tyousakaisyaSearch" />
                        <asp:AsyncPostBackTrigger ControlID="btnSetSyouhin1" />
                    </Triggers>
                    <ContentTemplate>
                        <table class="subTable paddinNarrow" style="font-weight: bold;">
                            <tr>
                                <td colspan="2">
                                    <input id="kameitenCd" runat="server" maxlength="5" class="codeNumber hissu" style="width: 40px;" /><input
                                        type="hidden" id="kameitenCdOld" runat="server" /><input type="hidden" id="saveCdOrderStop"
                                            runat="server" />
                                    <input type="hidden" id="HiddenKameitenTyuuiJikou" runat="server" /><input id="kameitenSearch"
                                        type="button" value="検索" runat="server" onmouseup="JSkameitenSearchType=9;" onkeydown="if(event.keyCode==13||event.keyCode==32)JSkameitenSearchType=9;"
                                        onserverclick="kameitenSearch_ServerClick" class="gyoumuSearchBtn" />
                                    <input type="hidden" id="kameitenSearchType" runat="server" />
                                    <input type="hidden" id="kameitenSearchFlg" runat="server" />
                                    <input id="kameitenSearchAfter"
                                        type="button" value="検索後処理" runat="server" onserverclick="kameitenSearchAfter_ServerClick"
                                        style="display: none" /><input id="kameitenNm" runat="server" readonly="readonly"
                                            style="width: 18em" class="readOnlyStyle" tabindex="-1" />
                                            <input id="TextTorikesiRiyuu" runat="server" style="width: 7em;" tabindex="-1" />
                                    <input id="ButtonKameitenTyuuijouhou" class="btnKameitenTyuuijouhou" runat="server"
                                        type="button" value="注意情報" /><input type="text" class="readOnlyStyle2" style="width: 39px;"
                                            readonly="readonly" tabindex="-1" />
                                    Tel：
                                    <input id="kameitenTel" runat="server" class="readOnlyStyle" readonly="readonly"
                                        style="width: 6em; ime-mode: disabled;" tabindex="-1" />&nbsp; Fax：
                                    <input id="kameitenFax" runat="server" class="readOnlyStyle" readonly="readonly"
                                        style="width: 6em; ime-mode: disabled;" tabindex="-1" />
                                    <input id="kameitenMail" runat="server" type="hidden" /></td>
                            </tr>
                            <tr>
                                <td style="width: 230px">
                                    ビルダーNO：<input id="builderNo" runat="server" maxlength="5" readonly="readonly" style="width: 3em;
                                        ime-mode: disabled;" class="readOnlyStyle" tabindex="-1" />
                                    <input id="builderCheck" runat="server" style="width: 6.5em" type="button" value="ビルダー情報" />
                                </td>
                                <td>
                                    系列：<input id="keiretuNm" runat="server" readonly="readonly" style="width: 19.3em;"
                                        class="readOnlyStyle" tabindex="-1" />&nbsp; 住所：<input id="kameitenJuusyo" runat="server"
                                            readonly="readonly" style="width: 20em;" class="readOnlyStyle" tabindex="-1" />
                                    <br />
                                    営業所/法人名：<input id="EigyousyoMei" runat="server" readonly="readonly" style="width: 25em;"
                                        class="readOnlyStyle" tabindex="-1" />
                                    <input id="EigyousyoCd" runat="server" type="hidden" />
                                    <input id="keiretuCd" runat="server" type="hidden" />
                                    <input id="TysSeikyuuSaki" runat="server" type="hidden" />
                                    <input id="HosyousyoHakUmu" runat="server" type="hidden" />
                                    <input id="KojGaisyaSeikyuuUmu" runat="server" type="hidden" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <input id="mitsumoriHitsuyou" runat="server" style="font-weight: bold; width: 177px;
                                        color: red" class="readOnlyStyle" readonly="readOnly" tabindex="-1" />
                                    &nbsp;
                                    <input id="hacchuushoHituyou" runat="server" style="font-weight: bold; width: 201px;
                                        color: red" class="readOnlyStyle" readonly="readOnly" tabindex="-1" />
                                    <input id="kakushuNG" runat="server" style="font-weight: bold; width: 162px; color: red"
                                        class="readOnlyStyle" readonly="readOnly" tabindex="-1" />
                                    <input id="TextJioSakiFlg" runat="server" style="font-weight: bold; width: 50px;
                                        color: red; border-bottom-color: red;" class="readOnlyStyle" readonly="readOnly"
                                        tabindex="-1" />
                                    &nbsp; &nbsp; &nbsp;&nbsp; <span id="kojTantoSpan" runat="server">工事担当者：<input id="koujiTantouNm"
                                        runat="server" maxlength="10" style="width: 70px" /></span></td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="tableSpacer" colspan="8">
            </td>
        </tr>
        <tr>
            <td class="koumokuMei">
                &nbsp;</td>
            <td style="width: 110px">
                <asp:UpdatePanel ID="UpdatePanel11" UpdateMode="Conditional" runat="server" RenderMode="Inline">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSetSyouhin1" />
                    </Triggers>
                    <ContentTemplate>
                        <input id="itemKb_1" runat="server" name="itemKb" type="radio" value="1" style="display: none"
                            disabled="disabled" /><span id="itemKbSpan_1" style="display: none" runat="server">60年保証</span>
                        <input id="itemKb_2" runat="server" name="itemKb" type="radio" value="2" style="display: none"
                            disabled="disabled" /><span id="itemKbSpan_2" runat="server">土地販売</span>
                        <input id="itemKb_3" runat="server" name="itemKb" type="radio" value="3" style="display: none"
                            disabled="disabled" /><span id="itemKbSpan_3" runat="server">リフォーム</span>
                        <input id="itemKb_9" runat="server" name="itemKb" type="radio" value="9" style="display: none"
                            checked disabled="disabled" /><span id="itemKbSpan_9" style="display: none" runat="server">&nbsp;</span><input
                                type="hidden" id="itemKbPre" value="9" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="koumokuMei">
                同時依頼棟数</td>
            <td>
                <input id="iraiTousuu" runat="server" maxlength="4" class="number" style="width: 40px;"
                    value="1" />棟</td>
            <td class="koumokuMei">
                建物用途</td>
            <td>
                <input id="tatemonoYoutoCode" runat="server" maxlength="1" class="pullCd" />&nbsp;<asp:DropDownList
                    ID="cboTatemonoYouto" runat="server" Width="97px" CssClass="hissu">
                </asp:DropDownList><span id="SPAN9" runat="server"></span><input type="hidden" id="HiddenTatemonoYoutoPre"
                            runat="server" /></td>
            <td class="koumokuMei" style="width: 80px">
                戸数</td>
            <td>
                <input id="kosuu" runat="server" maxlength="4" class="number" style="width: 40px;"
                    onblur="checkNumberAddFig(this);" onfocus="removeFig(this);" />戸
            </td>
        </tr>
        <tr>
            <td class="koumokuMei">
                調査会社</td>
            <td colspan="5">
                <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server" RenderMode="Inline">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSetSyouhin1" />
                        <asp:AsyncPostBackTrigger ControlID="btnSetTysGaiyou" />
                        <asp:AsyncPostBackTrigger ControlID="kameitenSearch" />
                    </Triggers>
                    <ContentTemplate>
                        <input id="tyousakaisyaCd" runat="server" maxlength="7" style="width: 60px;" class="codeNumber hissu" />
                        <input type="hidden" id="tyousakaisyaCdOld" runat="server" /><input type="hidden"
                            id="tyousakaisyaNmOld" runat="server" /><input type="hidden" id="tyousakaisyaSearchType"
                                runat="server" /><input id="tyousakaisyaSearch" value="検索" type="button" runat="server"
                                    onmouseup="JStyousakaisyaSearchType=9;" onkeydown="if(event.keyCode==13||event.keyCode==32)JStyousakaisyaSearchType=9;"
                                    onserverclick="tyousakaisyaSearch_ServerClick" class="gyoumuSearchBtn" />
                        <input id="afterChouKaishaSet" value="調査会社設定後処理" type="button" runat="server" style="display: none" />
                        <input id="tyousakaisyaNm" style="width: 25em" runat="server" class="readOnlyStyle"
                            tabindex="-1" readonly="readOnly" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="koumokuMei">
                進捗ステータス</td>
            <td>
                <input id="TextStatusIf" runat="server" maxlength="3" class="readOnlyStyle" style="width: 100px;"
                    tabindex="-1" readonly="readOnly" />
                <input type="hidden" id="HiddenStatusIf" runat="server" /></td>
        </tr>
        <tr>
            <td class="koumokuMei">
                商品1</td>
            <td colspan="5">
                <asp:UpdatePanel ID="UpdatePanelSyouhin1" UpdateMode="Conditional" runat="server"
                    RenderMode="Inline">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSetSyouhin1" />
                        <asp:AsyncPostBackTrigger ControlID="btnSetTysGaiyou" />
                        <asp:AsyncPostBackTrigger ControlID="HiddenTokutaiKkkHaneiFlg" />
                        <asp:AsyncPostBackTrigger ControlID="HiddenTokutaiKkkHaneiFlgPu" />
                        <asp:AsyncPostBackTrigger ControlID="HiddenTokutaiPreMode" />
                        <asp:AsyncPostBackTrigger ControlID="cboTyousaHouhou" />
                        <asp:AsyncPostBackTrigger ControlID="kameitenSearch" />
                        <asp:AsyncPostBackTrigger ControlID="kameitenSearchAfter" />
                        <asp:AsyncPostBackTrigger ControlID="cboTatemonoYouto" />
                        <asp:AsyncPostBackTrigger ControlID="afterChouKaishaSet" />
                        <asp:AsyncPostBackTrigger ControlID="tyousakaisyaSearch" />
                        <asp:AsyncPostBackTrigger ControlID="seikyuUmu_1_1" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:DropDownList ID="choSyouhin1" runat="server" CssClass="hissu">
                        </asp:DropDownList><span id="SpanSISyouhin1" runat="server"></span><input type="hidden"
                            id="HiddenSyouhin1Pre" runat="server" />
                            <input type="hidden" id="HiddenTokutaiKkkHaneiFlg" runat="server" />
                            <input type="hidden" id="HiddenTokutaiKkkHaneiFlgPu" runat="server" />
                            <input type="hidden" id="HiddenTokutaiPreMode" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="koumokuMei">
                保証商品</td>
            <td colspan="1">
                <asp:UpdatePanel ID="UpdatePanelHosyouSyouhinUmu" UpdateMode="Conditional" runat="server"
                    RenderMode="Inline">
                    <ContentTemplate>
                        <input id="TextHosyouSyouhinUmu" runat="server" class="readOnlyStyle" style="width: 30px"
                            tabindex="-1" readonly="readOnly" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="koumokuMei" colspan="1" style="display: none;">
                保証有無</td>
            <td colspan="2" style="display: none;">
                <asp:UpdatePanel ID="UpdatePanel8" UpdateMode="Conditional" runat="server" RenderMode="Inline">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="kameitenSearch" />
                        <asp:AsyncPostBackTrigger ControlID="tyousakaisyaSearch" />
                        <asp:AsyncPostBackTrigger ControlID="kameitenSearchAfter" />
                    </Triggers>
                    <ContentTemplate>
                        <select id="cboHosyouUmu" runat="server" class="" name="" style="display: inline">
                            <option value=""></option>
                            <option value="1">1:有り</option>
                        </select>
                        <span id="spanHosyouUmu" runat="server"></span>
                        <input id="HosyouKikan" runat="server" type="hidden" />
                        <input type="hidden" runat="server" id="HiddenHosyousyoHakkouJyoukyouMoto" />
                        <input type="hidden" runat="server" id="HiddenHosyousyoHakkouJyoukyouSetteiDateMoto" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="koumokuMei">
                調査方法</td>
            <td colspan="3">
                <asp:UpdatePanel ID="UpdatePanel10" UpdateMode="Conditional" runat="server" RenderMode="Inline">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSetSyouhin1" />
                        <asp:AsyncPostBackTrigger ControlID="btnSetTysGaiyou" />
                        <asp:AsyncPostBackTrigger ControlID="kameitenSearch" />
                        <asp:AsyncPostBackTrigger ControlID="kameitenSearchAfter" />
                    </Triggers>
                    <ContentTemplate>
                        <input id="chousaHouhouCode" runat="server" maxlength="2" class="pullCd" />&nbsp;<asp:DropDownList
                            ID="cboTyousaHouhou" runat="server" CssClass="hissu">
                        </asp:DropDownList><span id="SPAN15" runat="server"></span><input type="hidden" id="HiddenTysHouhouPre"
                            runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="koumokuMei">
                調査概要</td>
            <td colspan="3">
                <asp:UpdatePanel ID="UpdatePanelIraiNaiyou" UpdateMode="Conditional" runat="server"
                    RenderMode="Inline">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSetSyouhin1" />
                        <asp:AsyncPostBackTrigger ControlID="btnSetTysGaiyou" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:DropDownList ID="cboTyousaGaiyou" runat="server">
                        </asp:DropDownList><span id="SPAN16" runat="server"></span><input type="hidden" id="tyousaGaiyouPre"
                            value="9" runat="server" /><input type="hidden" runat="server" id="HiddenTyousaGaiyouMoto" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </tbody>
</table>
<!-- 依頼内容確定/解除 -->
<table cellpadding="1" class="titleTable" id="iraiKakuteiTable" style="width: 100%;
    text-align: center" runat="server">
    <tr>
        <td>
            <input type="button" id="btnIrainaiyouKakutei" value="依頼内容確定" runat="server" />
            <input type="button" id="btnIrainaiyouKaijo" value="依頼内容確定解除" runat="server" />
            <input type="hidden" id="flgKakutei" value="0" runat="server" />
        </td>
    </tr>
</table>
<!-- 商品情報設定処理呼び出し -->
<asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server" RenderMode="Inline">
    <ContentTemplate>
        <input type="button" id="btnSetSyouhin1" value="商品1設定" style="display: none" runat="server" />
        <input type="hidden" id="actCtrlId" runat="server" />
        <input type="hidden" id="actPreVal" runat="server" />
        <input type="hidden" id="kakakuSetteiBasyo" runat="server" />
        <input id="tyousaJissibi" runat="server" style="width: 20px" type="hidden" />
    </ContentTemplate>
</asp:UpdatePanel>
<!-- 調査概要設定処理呼び出し -->
<asp:UpdatePanel ID="UpdatePanelTysGaiyou" UpdateMode="Conditional" runat="server" RenderMode="Inline">
    <ContentTemplate>
        <input type="button" id="btnSetTysGaiyou" value="調査概要設定" style="display: none" runat="server" />
        <input type="hidden" id="actCtrlIdTg" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>
<!-- 商品検索画面呼び出し -->
<asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server" RenderMode="Inline">
    <ContentTemplate>
        <input type="button" id="btnSearchSyouhin23" value="商品検索" style="display: none" runat="server"
            onserverclick="btnSearchSyouhin23_ServerClick" />
        <input type="button" id="afterSearchSyouhin23" value="商品検索後処理" style="display: none"
            runat="server" onserverclick="afterSearchSyouhin23_ServerClick" />
        <input type="hidden" id="targetLine_SearchSyouhin23" runat="server" />
        <input type="hidden" id="strTargetIds_SearchSyouhin23" runat="server" />
        <input type="hidden" id="returnTargetIds_SearchSyouhin23" runat="server" />
        <input type="hidden" id="afterEventBtnId_SearchSyouhin23" runat="server" />
        <input type="hidden" id="searchSyouhin23Type" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>
<!-- 金額を手入力で変更した場合の処理呼び出し -->
<asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server" RenderMode="Inline">
    <ContentTemplate>
        <input type="button" id="kingakuHenkouKoumuten" value="工務店請求税抜金額変更時処理" style="display: none"
            runat="server" onserverclick="kingakuHenkouKoumuten_ServerClick" />
        <input type="button" id="kingakuHenkouJituseikyu" value="実請求税抜金額変更時処理" style="display: none"
            runat="server" onserverclick="kingakuHenkouJituseikyu_ServerClick" />
        <input type="button" id="kingakuHenkouSyoudakusyo" value="承諾書金額変更時処理" style="display: none"
            runat="server" onserverclick="kingakuHenkouSyoudakusyo_ServerClick" />
        <input type="hidden" id="kingakuHenkouActCId" runat="server" />
        <input type="hidden" id="kingakuHenkouLineNo" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>
<!-- 発注書確定コンボ変更、発注書金額変更時の処理呼び出し -->
<asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Conditional" runat="server" RenderMode="Inline">
    <ContentTemplate>
        <input type="button" id="hattyuusyoKakuteiHenkou" value="発注書確定変更時処理" style="display: none"
            runat="server" onserverclick="hattyuusyoKakuteiHenkou_ServerClick" />
        <input type="button" id="hattyuusyoKingakuHenkou" value="発注書金額変更時処理" style="display: none"
            runat="server" onserverclick="hattyuusyoKingakuHenkou_ServerClick" />
        <input type="hidden" id="hattyuusyoHenkouActCId" runat="server" />
        <input type="hidden" id="hattyuusyoHenkouLineNo" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>
<!-- 商品３確定コンボ変更時の処理呼び出し -->
<asp:UpdatePanel ID="UpdatePanel6" UpdateMode="Conditional" runat="server" RenderMode="Inline">
    <ContentTemplate>
        <input type="button" id="syouhin3KakuteiHenkou" value="商品3確定変更時処理" style="display: none"
            runat="server" onserverclick="syouhin3KakuteiHenkou_ServerClick" />&nbsp;
        <input type="hidden" id="syouhin3KakuteiHenkouActCId" runat="server" />
        <input type="hidden" id="syouhin3KakuteiHenkouLineNo" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>
<!-- 商品２，３請求有無コンボ変更時の処理呼び出し -->
<asp:UpdatePanel ID="UpdatePanel7" UpdateMode="Conditional" runat="server" RenderMode="Inline">
    <ContentTemplate>
        <input type="button" id="syouhin23SeikyuuUmuHenkou" value="商品23請求有無変更時処理" style="display: none"
            runat="server" onserverclick="syouhin23SeikyuuUmuHenkou_ServerClick" />&nbsp;
        <input type="hidden" id="syouhin23SeikyuuUmuHenkouActCId" runat="server" />
        <input type="hidden" id="syouhin23SeikyuuUmuHenkouLineNo" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>
<!-- 商品情報入力エリア -->
<asp:UpdatePanel ID="updPanelSyouhin" UpdateMode="Conditional" runat="server" RenderMode="Inline">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="kameitenSearch" />
        <asp:AsyncPostBackTrigger ControlID="tyousakaisyaSearch" />
        <asp:AsyncPostBackTrigger ControlID="kameitenSearchAfter" />
        <asp:AsyncPostBackTrigger ControlID="afterChouKaishaSet" />
        <asp:AsyncPostBackTrigger ControlID="btnSetSyouhin1" />
        <asp:AsyncPostBackTrigger ControlID="btnSearchSyouhin23" />
        <asp:AsyncPostBackTrigger ControlID="afterSearchSyouhin23" />
        <asp:AsyncPostBackTrigger ControlID="kingakuHenkouKoumuten" />
        <asp:AsyncPostBackTrigger ControlID="kingakuHenkouJituseikyu" />
        <asp:AsyncPostBackTrigger ControlID="kingakuHenkouSyoudakusyo" />
        <asp:AsyncPostBackTrigger ControlID="hattyuusyoKakuteiHenkou" />
        <asp:AsyncPostBackTrigger ControlID="hattyuusyoKingakuHenkou" />
        <asp:AsyncPostBackTrigger ControlID="syouhin3KakuteiHenkou" />
        <asp:AsyncPostBackTrigger ControlID="syouhin23SeikyuuUmuHenkou" />
    </Triggers>
    <ContentTemplate>
        <input type="hidden" runat="server" id="HiddenHosyousyoHakkouJyoukyou" />
        <!-- 売上状況保持 -->
        <input type="hidden" id="uriageJyoukyou1" runat="server" />
        <!-- 依頼内容確定時情報保持_特別対応 (非表示) -->
        <input type="hidden" id="HiddenKakuteiValuesTokubetu" runat="server" />
        <input type="hidden" id="HiddenChgTokuCd" runat="server" />
        <input type="hidden" id="HiddenChgTokuUpdDatetime" runat="server" />
        &nbsp;
        <!-- 商品テーブル -->
        <table cellpadding="1" class="mainTable itemTableNarrow" style="width: 100%; text-align: left">
            <thead>
                <tr>
                    <th class="tableTitle" colspan="3">
                        <a id="irai2DispLink2" runat="server">商品１ ／ 商品２</a></th>
                    <th colspan="9" class="shouhinTableTitleNyuukin">
                        &nbsp;&nbsp; 入金額（税込）
                        <input id="nyuukinGaku_1" runat="server" class="kingaku readOnlyStyle" maxlength="25"
                            style="width: 120px" readonly="readOnly" tabindex="-1" />
                        &nbsp;&nbsp; 残額
                        <input id="nyuukinZanGaku_1" runat="server" class="kingaku readOnlyStyle" maxlength="25"
                            size="25" style="width: 120px" readonly="readOnly" tabindex="-1" />
                        <input id="kaiyakuHaraimodosi" runat="server" style="width: 39px" type="hidden" /></th>
                    <th style="background-color: #ccffff; text-align: right;">
                        <input type="button" id="ButtonSyouhin4" runat="server" value="商品４" class="openHosyousyoDB" /></th>
                </tr>
                <tr>
                    <th class="tableSpacer">
                        <!-- 商品名列幅設定用スペーサー -->
                        <img src="images/spacer.gif" alt="" style="width: 128px; height: 0px;" /></th>
                    <th colspan="13" class="tableSpacer">
                    </th>
                </tr>
            </thead>
            <tbody id="irai2TBody2" runat="server">
                <tr class="shouhinTableTitle">
                    <td>
                        商品コード１<br />
                        商品名</td>
                    <td>
                        工務店請求<br />
                        税抜金額</td>
                    <td>
                        実請求<br />
                        税抜金額</td>
                    <td>
                        消費税</td>
                    <td>
                        税込金額</td>
                    <td>
                        承諾書<br />
                        金額</td>
                    <td>
                        請求書<br />
                        発行日</td>
                    <td>
                        売上<br />
                        年月日</td>
                    <td>
                        請求</td>
                    <td>
                        見積<br />
                        作成日</td>
                    <td>
                        発注書<br />
                        確定</td>
                    <td>
                        発注書<br />
                        金額</td>
                    <td>
                        発注書<br />
                        確認日</td>
                </tr>
                <tr id="shouhinLine_1_1" runat="server">
                    <td class="itemNm">
                        <input type="hidden" id="bunruiCd_1_1" runat="server" />
                        <input type="hidden" id="zeikubun_1_1" runat="server" />
                        <input type="hidden" id="zeiritu_1_1" runat="server" />
                        <input type="hidden" id="kingakuFlg_1_1" runat="server" />
                        <input type="hidden" id="uriageKeijyouFlg_1_1" runat="server" />
                        <input type="hidden" id="uriageKeijyouBi_1_1" runat="server" />
                        <input type="hidden" id="bikou_1_1" runat="server" />
                        <input type="hidden" id="ikkatuNyuukinFlg_1_1" runat="server" />
                        <input type="hidden" id="HiddenUpdDatetime_1_1" runat="server" />
                        <input type="hidden" id="shouhinCdOld_1_1" runat="server" />
                        <input type="hidden" id="HidSyouhinSeikyuuSakiCd_1_1" runat="server" />
                        <input type="hidden" id="HidSiireSyouhizei_1_1" runat="server" />
                        <input id="shouhinCd_1_1" runat="server" maxlength="8" size="10" class="readOnlyStyle itemCd"
                            readonly="readOnly" tabindex="-1" />
                       <uc:SeikyuuSiireLinkCtrl ID="ucSeikyuuSiireLink_1_1" runat="server" /><uc:TokubetuTaiouToolTipCtrl
                            ID="ucTokubetuTaiouToolTipCtrl_1_1" runat="server" />
                        <br />
                        <input type="text" id="shouhinNmText_1_1" style="display: none" runat="server" />
                        <span id="shouhinNm_1_1" runat="server"></span>
                    </td>
                    <td>
                        <input id="koumutenSeikyuZeinukiGaku_1_1" runat="server" class="kingaku" maxlength="10"
                            size="10" />
                        <input id="HiddenKoumutenGakuHenkouKahi_1_1" type="hidden" runat="server" />
                    </td>
                    <td>
                        <input id="jituSeikyuZeinukiGaku_1_1" runat="server" class="kingaku" maxlength="10"
                            size="10" />
                        <input id="HiddenJituGakuHenkouKahi_1_1" type="hidden" runat="server" />
                    </td>
                    <td colspan="1" rowspan="1">
                        <input id="shouhiZei_1_1" runat="server" class="kingaku readOnlyStyle" maxlength="10"
                            size="10" readonly="readOnly" tabindex="-1" />
                    </td>
                    <td colspan="1">
                        <input id="zeikomiGaku_1_1" runat="server" class="kingaku readOnlyStyle" maxlength="10"
                            size="10" readonly="readOnly" tabindex="-1" />
                    </td>
                    <td>
                        <input id="shoudakuKingaku_1_1" runat="server" class="kingaku" maxlength="10" size="10" />
                        <input type="hidden" id="HiddenSyoudakuHenkouKahi_1_1" runat="server" />
                    </td>
                    <td>
                        <input id="seikyuuHakkouDate_1_1" runat="server" class="date readOnlyStyle" maxlength="10"
                            size="10" readonly="readOnly" tabindex="-1" />
                    </td>
                    <td>
                        <input id="uriageDate_1_1" runat="server" class="date readOnlyStyle" maxlength="10"
                            size="10" readonly="readOnly" tabindex="-1" />
                    </td>
                    <td>
                        <select id="seikyuUmu_1_1" runat="server">
                            <option selected="selected" value="1">有</option>
                            <option value="0">無</option>
                        </select>
                        <span id="seikyuUmuSpan_1_1" runat="server"></span>
                    </td>
                    <td>
                        <input id="mitumoriSakuseiDate_1_1" runat="server" class="date readOnlyStyle" maxlength="10"
                            size="10" readonly="readOnly" tabindex="-1" />
                    </td>
                    <td style="width: 65px">
                        <select id="hattyuuKakutei_1_1" runat="server">
                            <option selected="selected" value="0">未確</option>
                            <option value="1">確定</option>
                        </select>
                        <span id="hattyuuKakuteiSpan_1_1" runat="server"></span>
                        <input id="hattyuuKakuteiOld_1_1" runat="server" style="width: 63px" type="hidden" /></td>
                    <td>
                        <input id="hattyuuKingaku_1_1" runat="server" class="kingaku" maxlength="10" size="10" /><input
                            type="hidden" id="HiddenHattyuuKingakuOld_1_1" runat="server" />
                    </td>
                    <td>
                        <input id="hattyuuKakuninDate_1_1" runat="server" class="date readOnlyStyle" maxlength="10"
                            size="10" readonly="readOnly" tabindex="-1" />
                    </td>
                </tr>
                <tr class="shouhinTableTitle">
                    <td>
                        商品コード２<br />
                        商品名</td>
                    <td>
                        工務店請求<br />
                        税抜金額</td>
                    <td>
                        実請求<br />
                        税抜金額</td>
                    <td>
                        消費税</td>
                    <td>
                        税込金額</td>
                    <td>
                        承諾書<br />
                        金額</td>
                    <td>
                        請求書<br />
                        発行日</td>
                    <td>
                        売上<br />
                        年月日</td>
                    <td>
                        請求</td>
                    <td>
                        見積<br />
                        作成日</td>
                    <td>
                        発注書<br />
                        確定</td>
                    <td>
                        発注書<br />
                        金額</td>
                    <td>
                        発注書<br />
                        確認日</td>
                </tr>
                <tr id="shouhinLine_2_1" runat="server">
                    <td class="itemNm">
                        <input type="hidden" id="bunruiCd_2_1" runat="server" />
                        <input type="hidden" id="zeikubun_2_1" runat="server" />
                        <input type="hidden" id="zeiritu_2_1" runat="server" />
                        <input type="hidden" id="kingakuFlg_2_1" runat="server" />
                        <input type="hidden" id="uriageKeijyouFlg_2_1" runat="server" />
                        <input type="hidden" id="uriageKeijyouBi_2_1" runat="server" />
                        <input type="hidden" id="bikou_2_1" runat="server" />
                        <input type="hidden" id="ikkatuNyuukinFlg_2_1" runat="server" />
                        <input type="hidden" id="HiddenUpdDatetime_2_1" runat="server" />
                        <input type="hidden" id="shouhinCdOld_2_1" runat="server" />
                        <input type="hidden" id="HidSyouhinSeikyuuSakiCd_2_1" runat="server" />
                        <input type="hidden" id="HidSiireSyouhizei_2_1" runat="server" />
                        <input id="shouhinCd_2_1" runat="server" maxlength="8" size="10" class="itemCd" /><input
                            id="shouhinSearch_2_1" runat="server" class="itemSearchBtn" type="button" value="索" />
                        <uc:SeikyuuSiireLinkCtrl ID="ucSeikyuuSiireLink_2_1" runat="server" /><uc:TokubetuTaiouToolTipCtrl
                            ID="ucTokubetuTaiouToolTipCtrl_2_1" runat="server" />
                        <br />
                        <input type="text" id="shouhinNmText_2_1" style="display: none" runat="server" />
                        <span id="shouhinNm_2_1" runat="server"></span>
                    </td>
                    <td>
                        <input id="koumutenSeikyuZeinukiGaku_2_1" runat="server" class="kingaku" maxlength="10" size="10" />
                        <input id="koumutenSeikyuZeinukiGakuOld_2_1" type="hidden" runat="server" />
                        <input id="HiddenKoumutenGakuHenkouKahi_2_1" type="hidden" runat="server" /></td>
                    <td>
                        <input id="jituSeikyuZeinukiGaku_2_1" runat="server" class="kingaku" maxlength="10" size="10" />
                        <input id="jituSeikyuZeinukiGakuOld_2_1" type="hidden" runat="server" />
                        <input id="HiddenJituGakuHenkouKahi_2_1" type="hidden" runat="server" /></td>
                    <td colspan="1" rowspan="1">
                        <input id="shouhiZei_2_1" runat="server" class="kingaku readOnlyStyle" maxlength="10"
                            size="10" readonly="readOnly" tabindex="-1" /></td>
                    <td colspan="1">
                        <input id="zeikomiGaku_2_1" runat="server" class="kingaku readOnlyStyle" maxlength="10"
                            size="10" readonly="readOnly" tabindex="-1" /></td>
                    <td>
                        <input id="shoudakuKingaku_2_1" runat="server" class="kingaku" maxlength="10" size="10" />
                        <input id="shoudakuKingakuOld_2_1" type="hidden" runat="server" />
                        <input id="HiddenSyoudakuHenkouKahi_2_1" type="hidden" runat="server" />
                    </td>
                    <td>
                        <input id="seikyuuHakkouDate_2_1" runat="server" class="date readOnlyStyle" maxlength="10"
                            size="10" readonly="readOnly" tabindex="-1" /></td>
                    <td>
                        <input id="uriageDate_2_1" runat="server" class="date readOnlyStyle" maxlength="10"
                            size="10" readonly="readOnly" tabindex="-1" /></td>
                    <td>
                        <select id="seikyuUmu_2_1" runat="server">
                            <option selected="selected" value="1">有</option>
                            <option value="0">無</option>
                        </select>
                        <span id="seikyuUmuSpan_2_1" runat="server"></span>
                    </td>
                    <td>
                        <input id="mitumoriSakuseiDate_2_1" runat="server" class="date readOnlyStyle" maxlength="10"
                            size="10" readonly="readOnly" tabindex="-1" /></td>
                    <td style="width: 65px">
                        <select id="hattyuuKakutei_2_1" runat="server">
                            <option selected="selected" value="0">未確</option>
                            <option value="1">確定</option>
                        </select>
                        <span id="hattyuuKakuteiSpan_2_1" runat="server"></span>
                        <input id="hattyuuKakuteiOld_2_1" runat="server" style="width: 63px" type="hidden" /><span
                            id="pullSpan_4" runat="server"></span></td>
                    <td>
                        <input id="hattyuuKingaku_2_1" runat="server" class="kingaku" maxlength="10" size="10" />
                        <input id="hattyuuKingakuOld_2_1" type="hidden" runat="server" />
                        <input id="HiddenHattyuuKingakuOld_2_1" type="hidden" runat="server" />
                    </td>
                    <td>
                        <input id="hattyuuKakuninDate_2_1" runat="server" class="date readOnlyStyle" maxlength="10"
                            size="10" readonly="readOnly" tabindex="-1" /></td>
                </tr>
                <tr id="shouhinLine_2_2" runat="server">
                    <td class="itemNm">
                        <input type="hidden" id="bunruiCd_2_2" runat="server" />
                        <input type="hidden" id="zeikubun_2_2" runat="server" />
                        <input type="hidden" id="zeiritu_2_2" runat="server" />
                        <input type="hidden" id="kingakuFlg_2_2" runat="server" />
                        <input type="hidden" id="uriageKeijyouFlg_2_2" runat="server" />
                        <input type="hidden" id="uriageKeijyouBi_2_2" runat="server" />
                        <input type="hidden" id="bikou_2_2" runat="server" />
                        <input type="hidden" id="ikkatuNyuukinFlg_2_2" runat="server" />
                        <input type="hidden" id="HiddenUpdDatetime_2_2" runat="server" />
                        <input type="hidden" id="shouhinCdOld_2_2" runat="server" />
                        <input type="hidden" id="HidSyouhinSeikyuuSakiCd_2_2" runat="server" />
                        <input type="hidden" id="HidSiireSyouhizei_2_2" runat="server" />
                        <input id="shouhinCd_2_2" runat="server" maxlength="8" size="10" class="itemCd" /><input
                            id="shouhinSearch_2_2" runat="server" class="itemSearchBtn" type="button" value="索" />
                        <uc:SeikyuuSiireLinkCtrl ID="ucSeikyuuSiireLink_2_2" runat="server" /><uc:TokubetuTaiouToolTipCtrl
                            ID="ucTokubetuTaiouToolTipCtrl_2_2" runat="server" />
                        <br />
                        <input type="text" id="shouhinNmText_2_2" style="display: none" runat="server" />
                        <span id="shouhinNm_2_2" runat="server"></span>
                    </td>
                    <td>
                        <input id="koumutenSeikyuZeinukiGaku_2_2" runat="server" class="kingaku" maxlength="10" size="10" />
                        <input id="koumutenSeikyuZeinukiGakuOld_2_2" type="hidden" runat="server" />
                        <input id="HiddenKoumutenGakuHenkouKahi_2_2" type="hidden" runat="server" /></td>
                    <td>
                        <input id="jituSeikyuZeinukiGaku_2_2" runat="server" class="kingaku" maxlength="10" size="10" />
                        <input id="jituSeikyuZeinukiGakuOld_2_2" type="hidden" runat="server" />
                        <input id="HiddenJituGakuHenkouKahi_2_2" type="hidden" runat="server" /></td>
                    <td colspan="1" rowspan="1">
                        <input id="shouhiZei_2_2" runat="server" class="kingaku readOnlyStyle" maxlength="10"
                            size="10" readonly="readOnly" tabindex="-1" /></td>
                    <td colspan="1">
                        <input id="zeikomiGaku_2_2" runat="server" class="kingaku readOnlyStyle" maxlength="10"
                            size="10" readonly="readOnly" tabindex="-1" /></td>
                    <td>
                        <input id="shoudakuKingaku_2_2" runat="server" class="kingaku" maxlength="10" size="10" />
                        <input id="shoudakuKingakuOld_2_2" type="hidden" runat="server" />
                        <input id="HiddenSyoudakuHenkouKahi_2_2" type="hidden" runat="server" />
                    </td>
                    <td>
                        <input id="seikyuuHakkouDate_2_2" runat="server" class="date readOnlyStyle" maxlength="10"
                            size="10" readonly="readOnly" tabindex="-1" /></td>
                    <td>
                        <input id="uriageDate_2_2" runat="server" class="date readOnlyStyle" maxlength="10"
                            size="10" readonly="readOnly" tabindex="-1" /></td>
                    <td>
                        <select id="seikyuUmu_2_2" runat="server">
                            <option selected="selected" value="1">有</option>
                            <option value="0">無</option>
                        </select>
                        <span id="seikyuUmuSpan_2_2" runat="server"></span>
                    </td>
                    <td>
                        <input id="mitumoriSakuseiDate_2_2" runat="server" class="date readOnlyStyle" maxlength="10"
                            size="10" readonly="readOnly" tabindex="-1" /></td>
                    <td style="width: 65px">
                        <select id="hattyuuKakutei_2_2" runat="server">
                            <option selected="selected" value="0">未確</option>
                            <option value="1">確定</option>
                        </select>
                        <span id="hattyuuKakuteiSpan_2_2" runat="server"></span>
                        <input id="hattyuuKakuteiOld_2_2" runat="server" style="width: 63px" type="hidden" /><span
                            id="pullSpan_6" runat="server"></span></td>
                    <td>
                        <input id="hattyuuKingaku_2_2" runat="server" class="kingaku" maxlength="10" size="10" />
                        <input id="hattyuuKingakuOld_2_2" type="hidden" runat="server" />
                        <input id="HiddenHattyuuKingakuOld_2_2" type="hidden" runat="server" />
                    </td>
                    <td>
                        <input id="hattyuuKakuninDate_2_2" runat="server" class="date readOnlyStyle" maxlength="10"
                            size="10" readonly="readOnly" tabindex="-1" /></td>
                </tr>
                <tr id="shouhinLine_2_3" runat="server">
                    <td class="itemNm">
                        <input type="hidden" id="bunruiCd_2_3" runat="server" />
                        <input type="hidden" id="zeikubun_2_3" runat="server" />
                        <input type="hidden" id="zeiritu_2_3" runat="server" />
                        <input type="hidden" id="kingakuFlg_2_3" runat="server" />
                        <input type="hidden" id="uriageKeijyouFlg_2_3" runat="server" />
                        <input type="hidden" id="uriageKeijyouBi_2_3" runat="server" />
                        <input type="hidden" id="bikou_2_3" runat="server" />
                        <input type="hidden" id="ikkatuNyuukinFlg_2_3" runat="server" />
                        <input type="hidden" id="HiddenUpdDatetime_2_3" runat="server" />
                        <input type="hidden" id="shouhinCdOld_2_3" runat="server" />
                        <input type="hidden" id="HidSyouhinSeikyuuSakiCd_2_3" runat="server" />
                        <input type="hidden" id="HidSiireSyouhizei_2_3" runat="server" />
                        <input id="shouhinCd_2_3" runat="server" maxlength="8" size="10" class="itemCd" /><input
                            id="shouhinSearch_2_3" runat="server" class="itemSearchBtn" type="button" value="索" />
                        <uc:SeikyuuSiireLinkCtrl ID="ucSeikyuuSiireLink_2_3" runat="server" /><uc:TokubetuTaiouToolTipCtrl
                            ID="ucTokubetuTaiouToolTipCtrl_2_3" runat="server" />
                        <br />
                        <input type="text" id="shouhinNmText_2_3" style="display: none" runat="server" />
                        <span id="shouhinNm_2_3" runat="server"></span>
                    </td>
                    <td>
                        <input id="koumutenSeikyuZeinukiGaku_2_3" runat="server" class="kingaku" maxlength="10" size="10" />
                        <input id="koumutenSeikyuZeinukiGakuOld_2_3" type="hidden" runat="server" />
                        <input id="HiddenKoumutenGakuHenkouKahi_2_3" type="hidden" runat="server" /></td>
                    <td>
                        <input id="jituSeikyuZeinukiGaku_2_3" runat="server" class="kingaku" maxlength="10" size="10" />
                        <input id="jituSeikyuZeinukiGakuOld_2_3" type="hidden" runat="server" />
                        <input id="HiddenJituGakuHenkouKahi_2_3" type="hidden" runat="server" /></td>
                    <td colspan="1" rowspan="1">
                        <input id="shouhiZei_2_3" runat="server" class="kingaku readOnlyStyle" maxlength="10"
                            size="10" readonly="readOnly" tabindex="-1" /></td>
                    <td colspan="1">
                        <input id="zeikomiGaku_2_3" runat="server" class="kingaku readOnlyStyle" maxlength="10"
                            size="10" readonly="readOnly" tabindex="-1" /></td>
                    <td>
                        <input id="shoudakuKingaku_2_3" runat="server" class="kingaku" maxlength="10" size="10" />
                        <input id="shoudakuKingakuOld_2_3" type="hidden" runat="server" />
                        <input id="HiddenSyoudakuHenkouKahi_2_3" type="hidden" runat="server" />
                    </td>
                    <td>
                        <input id="seikyuuHakkouDate_2_3" runat="server" class="date readOnlyStyle" maxlength="10"
                            size="10" readonly="readOnly" tabindex="-1" /></td>
                    <td>
                        <input id="uriageDate_2_3" runat="server" class="date readOnlyStyle" maxlength="10"
                            size="10" readonly="readOnly" tabindex="-1" /></td>
                    <td>
                        <select id="seikyuUmu_2_3" runat="server">
                            <option selected="selected" value="1">有</option>
                            <option value="0">無</option>
                        </select>
                        <span id="seikyuUmuSpan_2_3" runat="server"></span>
                    </td>
                    <td>
                        <input id="mitumoriSakuseiDate_2_3" runat="server" class="date readOnlyStyle" maxlength="10"
                            size="10" readonly="readOnly" tabindex="-1" /></td>
                    <td style="width: 65px">
                        <select id="hattyuuKakutei_2_3" runat="server">
                            <option selected="selected" value="0">未確</option>
                            <option value="1">確定</option>
                        </select>
                        <span id="hattyuuKakuteiSpan_2_3" runat="server"></span>
                        <input id="hattyuuKakuteiOld_2_3" runat="server" style="width: 63px" type="hidden" /><span
                            id="pullSpan_8" runat="server"></span></td>
                    <td>
                        <input id="hattyuuKingaku_2_3" runat="server" class="kingaku" maxlength="10" size="10" />
                        <input id="hattyuuKingakuOld_2_3" type="hidden" runat="server" />
                        <input id="HiddenHattyuuKingakuOld_2_3" type="hidden" runat="server" />
                    </td>
                    <td>
                        <input id="hattyuuKakuninDate_2_3" runat="server" class="date readOnlyStyle" maxlength="10"
                            size="10" readonly="readOnly" tabindex="-1" /></td>
                </tr>
                <tr id="shouhinLine_2_4" runat="server">
                    <td class="itemNm">
                        <input type="hidden" id="bunruiCd_2_4" runat="server" />
                        <input type="hidden" id="zeikubun_2_4" runat="server" />
                        <input type="hidden" id="zeiritu_2_4" runat="server" />
                        <input type="hidden" id="kingakuFlg_2_4" runat="server" />
                        <input type="hidden" id="uriageKeijyouFlg_2_4" runat="server" />
                        <input type="hidden" id="uriageKeijyouBi_2_4" runat="server" />
                        <input type="hidden" id="bikou_2_4" runat="server" />
                        <input type="hidden" id="ikkatuNyuukinFlg_2_4" runat="server" />
                        <input type="hidden" id="HiddenUpdDatetime_2_4" runat="server" />
                        <input type="hidden" id="shouhinCdOld_2_4" runat="server" />
                        <input type="hidden" id="HidSyouhinSeikyuuSakiCd_2_4" runat="server" />
                        <input type="hidden" id="HidSiireSyouhizei_2_4" runat="server" />
                        <input id="shouhinCd_2_4" runat="server" maxlength="8" size="10" class="itemCd" /><input
                            id="shouhinSearch_2_4" runat="server" class="itemSearchBtn" type="button" value="索" />
                        <uc:SeikyuuSiireLinkCtrl ID="ucSeikyuuSiireLink_2_4" runat="server" /><uc:TokubetuTaiouToolTipCtrl
                            ID="ucTokubetuTaiouToolTipCtrl_2_4" runat="server" />
                        <br />
                        <input type="text" id="shouhinNmText_2_4" style="display: none" runat="server" />
                        <span id="shouhinNm_2_4" runat="server"></span>
                    </td>
                    <td>
                        <input id="koumutenSeikyuZeinukiGaku_2_4" runat="server" class="kingaku" maxlength="10" size="10" />
                        <input id="koumutenSeikyuZeinukiGakuOld_2_4" type="hidden" runat="server" />
                        <input id="HiddenKoumutenGakuHenkouKahi_2_4" type="hidden" runat="server" /></td>
                    <td>
                        <input id="jituSeikyuZeinukiGaku_2_4" runat="server" class="kingaku" maxlength="10" size="10" />
                        <input id="jituSeikyuZeinukiGakuOld_2_4" type="hidden" runat="server" />
                        <input id="HiddenJituGakuHenkouKahi_2_4" type="hidden" runat="server" /></td>
                    <td colspan="1" rowspan="1">
                        <input id="shouhiZei_2_4" runat="server" class="kingaku readOnlyStyle" maxlength="10"
                            size="10" readonly="readOnly" tabindex="-1" /></td>
                    <td colspan="1">
                        <input id="zeikomiGaku_2_4" runat="server" class="kingaku readOnlyStyle" maxlength="10"
                            size="10" readonly="readOnly" tabindex="-1" /></td>
                    <td>
                        <input id="shoudakuKingaku_2_4" runat="server" class="kingaku" maxlength="10" size="10" />
                        <input id="shoudakuKingakuOld_2_4" type="hidden" runat="server" />
                        <input type="hidden" id="HiddenSyoudakuHenkouKahi_2_4" runat="server" />
                    </td>
                    <td>
                        <input id="seikyuuHakkouDate_2_4" runat="server" class="date readOnlyStyle" maxlength="10"
                            size="10" readonly="readOnly" tabindex="-1" /></td>
                    <td>
                        <input id="uriageDate_2_4" runat="server" class="date readOnlyStyle" maxlength="10"
                            size="10" readonly="readOnly" tabindex="-1" /></td>
                    <td>
                        <select id="seikyuUmu_2_4" runat="server">
                            <option selected="selected" value="1">有</option>
                            <option value="0">無</option>
                        </select>
                        <span id="seikyuUmuSpan_2_4" runat="server"></span>
                    </td>
                    <td>
                        <input id="mitumoriSakuseiDate_2_4" runat="server" class="date readOnlyStyle" maxlength="10"
                            size="10" readonly="readOnly" tabindex="-1" /></td>
                    <td style="width: 65px">
                        <select id="hattyuuKakutei_2_4" runat="server">
                            <option selected="selected" value="0">未確</option>
                            <option value="1">確定</option>
                        </select>
                        <span id="hattyuuKakuteiSpan_2_4" runat="server"></span>
                        <input id="hattyuuKakuteiOld_2_4" runat="server" style="width: 63px" type="hidden" /><span
                            id="pullSpan_10" runat="server"></span></td>
                    <td>
                        <input id="hattyuuKingaku_2_4" runat="server" class="kingaku" maxlength="10" size="10" />
                        <input id="hattyuuKingakuOld_2_4" type="hidden" runat="server" />
                        <input id="HiddenHattyuuKingakuOld_2_4" type="hidden" runat="server" />
                    </td>
                    <td>
                        <input id="hattyuuKakuninDate_2_4" runat="server" class="date readOnlyStyle" maxlength="10"
                            size="10" readonly="readOnly" tabindex="-1" /></td>
                </tr>
                <tr align="center" style="display: none">
                    <td colspan="14" rowspan="1">
                        <br />
                        <table cellpadding="2" class="mainTable" style="text-align: left">
                            <tbody>
                                <tr class="shouhinTableTitleNyuukin">
                                    <td>
                                        入金額（税込）</td>
                                    <td>
                                        残額</td>
                                </tr>
                                <tr>
                                    <td>
                                        <input id="nyuukinGaku_2" runat="server" class="kingaku" maxlength="25" style="width: 120px"
                                            readonly="readOnly" tabindex="-1" /></td>
                                    <td>
                                        <input id="nyuukinZanGaku_2" runat="server" class="kingaku" maxlength="25" size="25"
                                            style="width: 120px" readonly="readOnly" tabindex="-1" /></td>
                                </tr>
                            </tbody>
                        </table>
                        <br />
                    </td>
                </tr>
                <tr class="tableSpacer">
                    <td colspan="14">
                    </td>
                </tr>
            </tbody>
            <tbody>
                <tr>
                    <th class="tableTitle" colspan="3" rowspan="1">
                        <a id="irai2DispLink3" runat="server">商品３</a><input type="hidden" id="syouhin3Display"
                            runat="server" /></th>
                    <th colspan="11" class="shouhinTableTitleNyuukin">
                        &nbsp;&nbsp; 入金額（税込）
                        <input id="nyuukinGaku_4" runat="server" class="kingaku readOnlyStyle" maxlength="25"
                            style="width: 120px" readonly="readOnly" tabindex="-1" />
                        &nbsp;&nbsp; 残額
                        <input id="nyuukinZanGaku_4" runat="server" class="kingaku readOnlyStyle" maxlength="25"
                            size="25" style="width: 120px" readonly="readOnly" tabindex="-1" /></th>
                </tr>
            </tbody>
            <tbody id="irai2TBody3" runat="server">
                <tr class="shouhinTableTitle">
                    <td>
                        商品コード３<br />
                        商品名</td>
                    <td>
                        工務店請求<br />
                        税抜金額</td>
                    <td>
                        実請求<br />
                        税抜金額</td>
                    <td>
                        消費税</td>
                    <td>
                        税込金額</td>
                    <td>
                        承諾書<br />
                        金額</td>
                    <td>
                        請求書<br />
                        発行日</td>
                    <td>
                        売上<br />
                        年月日</td>
                    <td>
                        請求</td>
                    <td>
                        見積<br />
                        作成日</td>
                    <td>
                        発注書<br />
                        確定</td>
                    <td>
                        発注書<br />
                        金額</td>
                    <td>
                        発注書<br />
                        確認日</td>
                </tr>
                <tr id="shouhinLine_3_1" runat="server">
                    <td class="itemNm">
                        <input type="hidden" id="bunruiCd_3_1" runat="server" />
                        <input type="hidden" id="zeikubun_3_1" runat="server" />
                        <input type="hidden" id="zeiritu_3_1" runat="server" />
                        <input type="hidden" id="kingakuFlg_3_1" runat="server" />
                        <input type="hidden" id="uriageJyoukyou_3_1" runat="server" />
                        <input type="hidden" id="uriageKeijyouFlg_3_1" runat="server" />
                        <input type="hidden" id="uriageKeijyouBi_3_1" runat="server" />
                        <input type="hidden" id="bikou_3_1" runat="server" />
                        <input type="hidden" id="ikkatuNyuukinFlg_3_1" runat="server" />
                        <input type="hidden" id="HiddenUpdDatetime_3_1" runat="server" />
                        <input type="hidden" id="HiddenKakuteiOld_3_1" runat="server" />
                        <input type="hidden" id="shouhinCdOld_3_1" runat="server" />
                        <input type="hidden" id="HidSyouhinSeikyuuSakiCd_3_1" runat="server" />
                        <input type="hidden" id="HidSiireSyouhizei_3_1" runat="server" />
                        <input maxlength="8" size="10" id="shouhinCd_3_1" runat="server" class="itemCd" /><input
                            id="shouhinSearch_3_1" value="索" type="button" class="itemSearchBtn" runat="server" />
                        <select id="kakutei_3_1" runat="server">
                            <option value="0">未確</option>
                            <option value="1">確定</option>
                        </select>
                        <span id="kakuteiSpan_3_1" runat="server"></span>
                        <br />
                        <uc:SeikyuuSiireLinkCtrl ID="ucSeikyuuSiireLink_3_1" runat="server" /><uc:TokubetuTaiouToolTipCtrl
                            ID="ucTokubetuTaiouToolTipCtrl_3_1" runat="server" />
                        <br />
                        <input type="text" id="shouhinNmText_3_1" style="display: none" runat="server" />
                        <span id="shouhinNm_3_1" runat="server"></span>
                    </td>
                    <td>
                        <input class="kingaku" maxlength="10" size="10" id="koumutenSeikyuZeinukiGaku_3_1"
                            runat="server" /><input id="HiddenKoumutenGakuHenkouKahi_3_1" type="hidden" runat="server" /></td>
                    <td>
                        <input class="kingaku" maxlength="10" size="10" id="jituSeikyuZeinukiGaku_3_1" runat="server" />
                        <input id="HiddenJituGakuHenkouKahi_3_1" type="hidden" runat="server" />
                    </td>
                    <td colspan="1" rowspan="1">
                        <input class="kingaku readOnlyStyle" maxlength="10" size="10" id="shouhiZei_3_1"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                    <td colspan="1">
                        <input class="kingaku readOnlyStyle" maxlength="10" size="10" id="zeikomiGaku_3_1"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                    <td>
                        <input class="kingaku" maxlength="10" size="10" id="shoudakuKingaku_3_1" runat="server" />
                        <input type="hidden" id="HiddenSyoudakuHenkouKahi_3_1" runat="server" />
                    </td>
                    <td>
                        <input class="date readOnlyStyle" maxlength="10" size="10" id="seikyuuHakkouDate_3_1"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                    <td>
                        <input class="date readOnlyStyle" maxlength="10" size="10" id="uriageDate_3_1" runat="server"
                            readonly="readOnly" tabindex="-1" /></td>
                    <td>
                        <select id="seikyuUmu_3_1" runat="server">
                            <option selected="selected" value="1">有</option>
                            <option value="0">無</option>
                        </select>
                        <span id="seikyuUmuSpan_3_1" runat="server"></span>
                    </td>
                    <td>
                        <input class="date readOnlyStyle" maxlength="10" size="10" id="mitumoriSakuseiDate_3_1"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                    <td style="width: 65px">
                        <select id="hattyuuKakutei_3_1" runat="server">
                            <option value="0">未確</option>
                            <option value="1">確定</option>
                        </select>
                        <span id="hattyuuKakuteiSpan_3_1" runat="server"></span>
                        <input id="hattyuuKakuteiOld_3_1" runat="server" style="width: 63px" type="hidden" /><span
                            id="pullSpan_12" runat="server"></span></td>
                    <td>
                        <input class="kingaku" maxlength="10" size="10" id="hattyuuKingaku_3_1" runat="server" /><input
                            type="hidden" id="HiddenHattyuuKingakuOld_3_1" runat="server" />
                    </td>
                    <td>
                        <input class="date readOnlyStyle" maxlength="10" size="10" id="hattyuuKakuninDate_3_1"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                </tr>
                <tr id="shouhinLine_3_2" runat="server">
                    <td class="itemNm">
                        <input type="hidden" id="bunruiCd_3_2" runat="server" />
                        <input type="hidden" id="zeikubun_3_2" runat="server" />
                        <input type="hidden" id="zeiritu_3_2" runat="server" />
                        <input type="hidden" id="kingakuFlg_3_2" runat="server" />
                        <input type="hidden" id="uriageJyoukyou_3_2" runat="server" />
                        <input type="hidden" id="uriageKeijyouFlg_3_2" runat="server" />
                        <input type="hidden" id="uriageKeijyouBi_3_2" runat="server" />
                        <input type="hidden" id="bikou_3_2" runat="server" />
                        <input type="hidden" id="ikkatuNyuukinFlg_3_2" runat="server" />
                        <input type="hidden" id="HiddenUpdDatetime_3_2" runat="server" />
                        <input type="hidden" id="HiddenKakuteiOld_3_2" runat="server" />
                        <input type="hidden" id="shouhinCdOld_3_2" runat="server" />
                        <input type="hidden" id="HidSyouhinSeikyuuSakiCd_3_2" runat="server" />
                        <input type="hidden" id="HidSiireSyouhizei_3_2" runat="server" />
                        <input maxlength="8" size="10" id="shouhinCd_3_2" runat="server" class="itemCd" /><input
                            id="shouhinSearch_3_2" value="索" type="button" class="itemSearchBtn" runat="server" />
                        <select id="kakutei_3_2" runat="server">
                            <option value="0">未確</option>
                            <option value="1">確定</option>
                        </select>
                        <span id="kakuteiSpan_3_2" runat="server"></span>
                        <br />
                        <uc:SeikyuuSiireLinkCtrl ID="ucSeikyuuSiireLink_3_2" runat="server" /><uc:TokubetuTaiouToolTipCtrl
                            ID="ucTokubetuTaiouToolTipCtrl_3_2" runat="server" />
                        <br />
                        <input type="text" id="shouhinNmText_3_2" style="display: none" runat="server" />
                        <span id="shouhinNm_3_2" runat="server"></span>
                    </td>
                    <td>
                        <input class="kingaku" maxlength="10" size="10" id="koumutenSeikyuZeinukiGaku_3_2"
                            runat="server" /><input id="HiddenKoumutenGakuHenkouKahi_3_2" type="hidden" runat="server" /></td>
                    <td>
                        <input class="kingaku" maxlength="10" size="10" id="jituSeikyuZeinukiGaku_3_2" runat="server" />
                        <input id="HiddenJituGakuHenkouKahi_3_2" type="hidden" runat="server" />
                    </td>
                    <td colspan="1" rowspan="1">
                        <input class="kingaku readOnlyStyle" maxlength="10" size="10" id="shouhiZei_3_2"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                    <td colspan="1">
                        <input class="kingaku readOnlyStyle" maxlength="10" size="10" id="zeikomiGaku_3_2"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                    <td>
                        <input class="kingaku" maxlength="10" size="10" id="shoudakuKingaku_3_2" runat="server" />
                        <input type="hidden" id="HiddenSyoudakuHenkouKahi_3_2" runat="server" />
                    </td>
                    <td>
                        <input class="date readOnlyStyle" maxlength="10" size="10" id="seikyuuHakkouDate_3_2"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                    <td>
                        <input class="date readOnlyStyle" maxlength="10" size="10" id="uriageDate_3_2" runat="server"
                            readonly="readOnly" tabindex="-1" /></td>
                    <td>
                        <select id="seikyuUmu_3_2" runat="server">
                            <option selected="selected" value="1">有</option>
                            <option value="0">無</option>
                        </select>
                        <span id="seikyuUmuSpan_3_2" runat="server"></span>
                    </td>
                    <td>
                        <input class="date readOnlyStyle" maxlength="10" size="10" id="mitumoriSakuseiDate_3_2"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                    <td style="width: 65px">
                        <select id="hattyuuKakutei_3_2" runat="server">
                            <option value="0">未確</option>
                            <option value="1">確定</option>
                        </select>
                        <span id="hattyuuKakuteiSpan_3_2" runat="server"></span>
                        <input id="hattyuuKakuteiOld_3_2" runat="server" style="width: 63px" type="hidden" /><span
                            id="pullSpan_14" runat="server"></span></td>
                    <td>
                        <input class="kingaku" maxlength="10" size="10" id="hattyuuKingaku_3_2" runat="server" /><input
                            type="hidden" id="HiddenHattyuuKingakuOld_3_2" runat="server" />
                    </td>
                    <td>
                        <input class="date readOnlyStyle" maxlength="10" size="10" id="hattyuuKakuninDate_3_2"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                </tr>
                <tr id="shouhinLine_3_3" runat="server">
                    <td class="itemNm">
                        <input type="hidden" id="bunruiCd_3_3" runat="server" />
                        <input type="hidden" id="zeikubun_3_3" runat="server" />
                        <input type="hidden" id="zeiritu_3_3" runat="server" />
                        <input type="hidden" id="kingakuFlg_3_3" runat="server" />
                        <input type="hidden" id="uriageJyoukyou_3_3" runat="server" />
                        <input type="hidden" id="uriageKeijyouFlg_3_3" runat="server" />
                        <input type="hidden" id="uriageKeijyouBi_3_3" runat="server" />
                        <input type="hidden" id="bikou_3_3" runat="server" />
                        <input type="hidden" id="ikkatuNyuukinFlg_3_3" runat="server" />
                        <input type="hidden" id="HiddenUpdDatetime_3_3" runat="server" />
                        <input type="hidden" id="HiddenKakuteiOld_3_3" runat="server" />
                        <input type="hidden" id="shouhinCdOld_3_3" runat="server" />
                        <input type="hidden" id="HidSyouhinSeikyuuSakiCd_3_3" runat="server" />
                        <input type="hidden" id="HidSiireSyouhizei_3_3" runat="server" />
                        <input maxlength="8" size="10" id="shouhinCd_3_3" runat="server" class="itemCd" /><input
                            id="shouhinSearch_3_3" value="索" type="button" class="itemSearchBtn" runat="server" />
                        <select id="kakutei_3_3" runat="server">
                            <option value="0">未確</option>
                            <option value="1">確定</option>
                        </select>
                        <span id="kakuteiSpan_3_3" runat="server"></span>
                        <br />
                        <uc:SeikyuuSiireLinkCtrl ID="ucSeikyuuSiireLink_3_3" runat="server" /><uc:TokubetuTaiouToolTipCtrl
                            ID="ucTokubetuTaiouToolTipCtrl_3_3" runat="server" />
                        <br />
                        <input type="text" id="shouhinNmText_3_3" style="display: none" runat="server" />
                        <span id="shouhinNm_3_3" runat="server"></span>
                    </td>
                    <td>
                        <input class="kingaku" maxlength="10" size="10" id="koumutenSeikyuZeinukiGaku_3_3"
                            runat="server" /><input id="HiddenKoumutenGakuHenkouKahi_3_3" type="hidden" runat="server" /></td>
                    <td>
                        <input class="kingaku" maxlength="10" size="10" id="jituSeikyuZeinukiGaku_3_3" runat="server" />
                        <input id="HiddenJituGakuHenkouKahi_3_3" type="hidden" runat="server" />
                    </td>
                    <td colspan="1" rowspan="1">
                        <input class="kingaku readOnlyStyle" maxlength="10" size="10" id="shouhiZei_3_3"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                    <td colspan="1">
                        <input class="kingaku readOnlyStyle" maxlength="10" size="10" id="zeikomiGaku_3_3"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                    <td>
                        <input class="kingaku" maxlength="10" size="10" id="shoudakuKingaku_3_3" runat="server" />
                        <input type="hidden" id="HiddenSyoudakuHenkouKahi_3_3" runat="server" />
                    </td>
                    <td>
                        <input class="date readOnlyStyle" maxlength="10" size="10" id="seikyuuHakkouDate_3_3"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                    <td>
                        <input class="date readOnlyStyle" maxlength="10" size="10" id="uriageDate_3_3" runat="server"
                            readonly="readOnly" tabindex="-1" /></td>
                    <td>
                        <select id="seikyuUmu_3_3" runat="server">
                            <option selected="selected" value="1">有</option>
                            <option value="0">無</option>
                        </select>
                        <span id="seikyuUmuSpan_3_3" runat="server"></span>
                    </td>
                    <td>
                        <input class="date readOnlyStyle" maxlength="10" size="10" id="mitumoriSakuseiDate_3_3"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                    <td style="width: 65px">
                        <select id="hattyuuKakutei_3_3" runat="server">
                            <option value="0">未確</option>
                            <option value="1">確定</option>
                        </select>
                        <span id="hattyuuKakuteiSpan_3_3" runat="server"></span>
                        <input id="hattyuuKakuteiOld_3_3" runat="server" style="width: 63px" type="hidden" /><span
                            id="pullSpan_16" runat="server"></span></td>
                    <td>
                        <input class="kingaku" maxlength="10" size="10" id="hattyuuKingaku_3_3" runat="server" /><input
                            type="hidden" id="HiddenHattyuuKingakuOld_3_3" runat="server" />
                    </td>
                    <td>
                        <input class="date readOnlyStyle" maxlength="10" size="10" id="hattyuuKakuninDate_3_3"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                </tr>
                <tr id="shouhinLine_3_4" runat="server">
                    <td class="itemNm">
                        <input type="hidden" id="bunruiCd_3_4" runat="server" />
                        <input type="hidden" id="zeikubun_3_4" runat="server" />
                        <input type="hidden" id="zeiritu_3_4" runat="server" />
                        <input type="hidden" id="kingakuFlg_3_4" runat="server" />
                        <input type="hidden" id="uriageJyoukyou_3_4" runat="server" />
                        <input type="hidden" id="uriageKeijyouFlg_3_4" runat="server" />
                        <input type="hidden" id="uriageKeijyouBi_3_4" runat="server" />
                        <input type="hidden" id="bikou_3_4" runat="server" />
                        <input type="hidden" id="ikkatuNyuukinFlg_3_4" runat="server" />
                        <input type="hidden" id="HiddenUpdDatetime_3_4" runat="server" />
                        <input type="hidden" id="HiddenKakuteiOld_3_4" runat="server" />
                        <input type="hidden" id="shouhinCdOld_3_4" runat="server" />
                        <input type="hidden" id="HidSyouhinSeikyuuSakiCd_3_4" runat="server" />
                        <input type="hidden" id="HidSiireSyouhizei_3_4" runat="server" />
                        <input maxlength="8" size="10" id="shouhinCd_3_4" runat="server" class="itemCd" /><input
                            id="shouhinSearch_3_4" value="索" type="button" class="itemSearchBtn" runat="server" />
                        <select id="kakutei_3_4" runat="server">
                            <option value="0">未確</option>
                            <option value="1">確定</option>
                        </select>
                        <span id="kakuteiSpan_3_4" runat="server"></span>
                        <br />
                        <uc:SeikyuuSiireLinkCtrl ID="ucSeikyuuSiireLink_3_4" runat="server" /><uc:TokubetuTaiouToolTipCtrl
                            ID="ucTokubetuTaiouToolTipCtrl_3_4" runat="server" />
                        <br />
                        <input type="text" id="shouhinNmText_3_4" style="display: none" runat="server" />
                        <span id="shouhinNm_3_4" runat="server"></span>
                    </td>
                    <td>
                        <input class="kingaku" maxlength="10" size="10" id="koumutenSeikyuZeinukiGaku_3_4"
                            runat="server" /><input id="HiddenKoumutenGakuHenkouKahi_3_4" type="hidden" runat="server" /></td>
                    <td>
                        <input class="kingaku" maxlength="10" size="10" id="jituSeikyuZeinukiGaku_3_4" runat="server" />
                        <input id="HiddenJituGakuHenkouKahi_3_4" type="hidden" runat="server" />
                    </td>
                    <td colspan="1" rowspan="1">
                        <input class="kingaku readOnlyStyle" maxlength="10" size="10" id="shouhiZei_3_4"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                    <td colspan="1">
                        <input class="kingaku readOnlyStyle" maxlength="10" size="10" id="zeikomiGaku_3_4"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                    <td>
                        <input class="kingaku" maxlength="10" size="10" id="shoudakuKingaku_3_4" runat="server" />
                        <input type="hidden" id="HiddenSyoudakuHenkouKahi_3_4" runat="server" />
                    </td>
                    <td>
                        <input class="date readOnlyStyle" maxlength="10" size="10" id="seikyuuHakkouDate_3_4"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                    <td>
                        <input class="date readOnlyStyle" maxlength="10" size="10" id="uriageDate_3_4" runat="server"
                            readonly="readOnly" tabindex="-1" /></td>
                    <td>
                        <select id="seikyuUmu_3_4" runat="server">
                            <option selected="selected" value="1">有</option>
                            <option value="0">無</option>
                        </select>
                        <span id="seikyuUmuSpan_3_4" runat="server"></span>
                    </td>
                    <td>
                        <input class="date readOnlyStyle" maxlength="10" size="10" id="mitumoriSakuseiDate_3_4"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                    <td style="width: 65px">
                        <select id="hattyuuKakutei_3_4" runat="server">
                            <option value="0">未確</option>
                            <option value="1">確定</option>
                        </select>
                        <span id="hattyuuKakuteiSpan_3_4" runat="server"></span>
                        <input id="hattyuuKakuteiOld_3_4" runat="server" style="width: 63px" type="hidden" /><span
                            id="pullSpan_18" runat="server"></span></td>
                    <td>
                        <input class="kingaku" maxlength="10" size="10" id="hattyuuKingaku_3_4" runat="server" /><input
                            type="hidden" id="HiddenHattyuuKingakuOld_3_4" runat="server" />
                    </td>
                    <td>
                        <input class="date readOnlyStyle" maxlength="10" size="10" id="hattyuuKakuninDate_3_4"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                </tr>
                <tr id="shouhinLine_3_5" runat="server">
                    <td class="itemNm">
                        <input type="hidden" id="bunruiCd_3_5" runat="server" />
                        <input type="hidden" id="zeikubun_3_5" runat="server" />
                        <input type="hidden" id="zeiritu_3_5" runat="server" />
                        <input type="hidden" id="kingakuFlg_3_5" runat="server" />
                        <input type="hidden" id="uriageJyoukyou_3_5" runat="server" />
                        <input type="hidden" id="uriageKeijyouFlg_3_5" runat="server" />
                        <input type="hidden" id="uriageKeijyouBi_3_5" runat="server" />
                        <input type="hidden" id="bikou_3_5" runat="server" />
                        <input type="hidden" id="ikkatuNyuukinFlg_3_5" runat="server" />
                        <input type="hidden" id="HiddenUpdDatetime_3_5" runat="server" />
                        <input type="hidden" id="HiddenKakuteiOld_3_5" runat="server" />
                        <input type="hidden" id="shouhinCdOld_3_5" runat="server" />
                        <input type="hidden" id="HidSyouhinSeikyuuSakiCd_3_5" runat="server" />
                        <input type="hidden" id="HidSiireSyouhizei_3_5" runat="server" />
                        <input maxlength="8" size="10" id="shouhinCd_3_5" runat="server" class="itemCd" /><input
                            id="shouhinSearch_3_5" value="索" type="button" class="itemSearchBtn" runat="server" />
                        <select id="kakutei_3_5" runat="server">
                            <option value="0">未確</option>
                            <option value="1">確定</option>
                        </select>
                        <span id="kakuteiSpan_3_5" runat="server"></span>
                        <br />
                        <uc:SeikyuuSiireLinkCtrl ID="ucSeikyuuSiireLink_3_5" runat="server" /><uc:TokubetuTaiouToolTipCtrl
                            ID="ucTokubetuTaiouToolTipCtrl_3_5" runat="server" />
                        <br />
                        <input type="text" id="shouhinNmText_3_5" style="display: none" runat="server" />
                        <span id="shouhinNm_3_5" runat="server"></span>
                    </td>
                    <td>
                        <input class="kingaku" maxlength="10" size="10" id="koumutenSeikyuZeinukiGaku_3_5"
                            runat="server" /><input id="HiddenKoumutenGakuHenkouKahi_3_5" type="hidden" runat="server" /></td>
                    <td>
                        <input class="kingaku" maxlength="10" size="10" id="jituSeikyuZeinukiGaku_3_5" runat="server" />
                        <input id="HiddenJituGakuHenkouKahi_3_5" type="hidden" runat="server" />
                    </td>
                    <td colspan="1" rowspan="1">
                        <input class="kingaku readOnlyStyle" maxlength="10" size="10" id="shouhiZei_3_5"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                    <td colspan="1">
                        <input class="kingaku readOnlyStyle" maxlength="10" size="10" id="zeikomiGaku_3_5"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                    <td>
                        <input class="kingaku" maxlength="10" size="10" id="shoudakuKingaku_3_5" runat="server" />
                        <input type="hidden" id="HiddenSyoudakuHenkouKahi_3_5" runat="server" />
                    </td>
                    <td>
                        <input class="date readOnlyStyle" maxlength="10" size="10" id="seikyuuHakkouDate_3_5"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                    <td>
                        <input class="date readOnlyStyle" maxlength="10" size="10" id="uriageDate_3_5" runat="server"
                            readonly="readOnly" tabindex="-1" /></td>
                    <td>
                        <select id="seikyuUmu_3_5" runat="server">
                            <option selected="selected" value="1">有</option>
                            <option value="0">無</option>
                        </select>
                        <span id="seikyuUmuSpan_3_5" runat="server"></span>
                    </td>
                    <td>
                        <input class="date readOnlyStyle" maxlength="10" size="10" id="mitumoriSakuseiDate_3_5"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                    <td style="width: 65px">
                        <select id="hattyuuKakutei_3_5" runat="server">
                            <option value="0">未確</option>
                            <option value="1">確定</option>
                        </select>
                        <span id="hattyuuKakuteiSpan_3_5" runat="server"></span>
                        <input id="hattyuuKakuteiOld_3_5" runat="server" style="width: 63px" type="hidden" /><span
                            id="pullSpan_20" runat="server"></span></td>
                    <td>
                        <input class="kingaku" maxlength="10" size="10" id="hattyuuKingaku_3_5" runat="server" /><input
                            type="hidden" id="HiddenHattyuuKingakuOld_3_5" runat="server" />
                    </td>
                    <td>
                        <input class="date readOnlyStyle" maxlength="10" size="10" id="hattyuuKakuninDate_3_5"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                </tr>
                <tr id="shouhinLine_3_6" runat="server">
                    <td class="itemNm">
                        <input type="hidden" id="bunruiCd_3_6" runat="server" />
                        <input type="hidden" id="zeikubun_3_6" runat="server" />
                        <input type="hidden" id="zeiritu_3_6" runat="server" />
                        <input type="hidden" id="kingakuFlg_3_6" runat="server" />
                        <input type="hidden" id="uriageJyoukyou_3_6" runat="server" />
                        <input type="hidden" id="uriageKeijyouFlg_3_6" runat="server" />
                        <input type="hidden" id="uriageKeijyouBi_3_6" runat="server" />
                        <input type="hidden" id="bikou_3_6" runat="server" />
                        <input type="hidden" id="ikkatuNyuukinFlg_3_6" runat="server" />
                        <input type="hidden" id="HiddenUpdDatetime_3_6" runat="server" />
                        <input type="hidden" id="HiddenKakuteiOld_3_6" runat="server" />
                        <input type="hidden" id="shouhinCdOld_3_6" runat="server" />
                        <input type="hidden" id="HidSyouhinSeikyuuSakiCd_3_6" runat="server" />
                        <input type="hidden" id="HidSiireSyouhizei_3_6" runat="server" />
                        <input maxlength="8" size="10" id="shouhinCd_3_6" runat="server" class="itemCd" /><input
                            id="shouhinSearch_3_6" value="索" type="button" class="itemSearchBtn" runat="server" />
                        <select id="kakutei_3_6" runat="server">
                            <option value="0">未確</option>
                            <option value="1">確定</option>
                        </select>
                        <span id="kakuteiSpan_3_6" runat="server"></span>
                        <br />
                        <uc:SeikyuuSiireLinkCtrl ID="ucSeikyuuSiireLink_3_6" runat="server" /><uc:TokubetuTaiouToolTipCtrl
                            ID="ucTokubetuTaiouToolTipCtrl_3_6" runat="server" />
                        <br />
                        <input type="text" id="shouhinNmText_3_6" style="display: none" runat="server" />
                        <span id="shouhinNm_3_6" runat="server"></span>
                    </td>
                    <td>
                        <input class="kingaku" maxlength="10" size="10" id="koumutenSeikyuZeinukiGaku_3_6"
                            runat="server" /><input id="HiddenKoumutenGakuHenkouKahi_3_6" type="hidden" runat="server" /></td>
                    <td>
                        <input class="kingaku" maxlength="10" size="10" id="jituSeikyuZeinukiGaku_3_6" runat="server" />
                        <input id="HiddenJituGakuHenkouKahi_3_6" type="hidden" runat="server" />
                    </td>
                    <td colspan="1" rowspan="1">
                        <input class="kingaku readOnlyStyle" maxlength="10" size="10" id="shouhiZei_3_6"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                    <td colspan="1">
                        <input class="kingaku readOnlyStyle" maxlength="10" size="10" id="zeikomiGaku_3_6"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                    <td>
                        <input class="kingaku" maxlength="10" size="10" id="shoudakuKingaku_3_6" runat="server" />
                        <input type="hidden" id="HiddenSyoudakuHenkouKahi_3_6" runat="server" />
                    </td>
                    <td>
                        <input class="date readOnlyStyle" maxlength="10" size="10" id="seikyuuHakkouDate_3_6"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                    <td>
                        <input class="date readOnlyStyle" maxlength="10" size="10" id="uriageDate_3_6" runat="server"
                            readonly="readOnly" tabindex="-1" /></td>
                    <td>
                        <select id="seikyuUmu_3_6" runat="server">
                            <option selected="selected" value="1">有</option>
                            <option value="0">無</option>
                        </select>
                        <span id="seikyuUmuSpan_3_6" runat="server"></span>
                    </td>
                    <td>
                        <input class="date readOnlyStyle" maxlength="10" size="10" id="mitumoriSakuseiDate_3_6"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                    <td style="width: 65px">
                        <select id="hattyuuKakutei_3_6" runat="server">
                            <option value="0">未確</option>
                            <option value="1">確定</option>
                        </select>
                        <span id="hattyuuKakuteiSpan_3_6" runat="server"></span>
                        <input id="hattyuuKakuteiOld_3_6" runat="server" style="width: 63px" type="hidden" /><span
                            id="pullSpan_22" runat="server"></span></td>
                    <td>
                        <input class="kingaku" maxlength="10" size="10" id="hattyuuKingaku_3_6" runat="server" /><input
                            type="hidden" id="HiddenHattyuuKingakuOld_3_6" runat="server" />
                    </td>
                    <td>
                        <input class="date readOnlyStyle" maxlength="10" size="10" id="hattyuuKakuninDate_3_6"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                </tr>
                <tr id="shouhinLine_3_7" runat="server">
                    <td class="itemNm">
                        <input type="hidden" id="bunruiCd_3_7" runat="server" />
                        <input type="hidden" id="zeikubun_3_7" runat="server" />
                        <input type="hidden" id="zeiritu_3_7" runat="server" />
                        <input type="hidden" id="kingakuFlg_3_7" runat="server" />
                        <input type="hidden" id="uriageJyoukyou_3_7" runat="server" />
                        <input type="hidden" id="uriageKeijyouFlg_3_7" runat="server" />
                        <input type="hidden" id="uriageKeijyouBi_3_7" runat="server" />
                        <input type="hidden" id="bikou_3_7" runat="server" />
                        <input type="hidden" id="ikkatuNyuukinFlg_3_7" runat="server" />
                        <input type="hidden" id="HiddenUpdDatetime_3_7" runat="server" />
                        <input type="hidden" id="HiddenKakuteiOld_3_7" runat="server" />
                        <input type="hidden" id="shouhinCdOld_3_7" runat="server" />
                        <input type="hidden" id="HidSyouhinSeikyuuSakiCd_3_7" runat="server" />
                        <input type="hidden" id="HidSiireSyouhizei_3_7" runat="server" />
                        <input maxlength="8" size="10" id="shouhinCd_3_7" runat="server" class="itemCd" /><input
                            id="shouhinSearch_3_7" value="索" type="button" class="itemSearchBtn" runat="server" />
                        <select id="kakutei_3_7" runat="server">
                            <option value="0">未確</option>
                            <option value="1">確定</option>
                        </select>
                        <span id="kakuteiSpan_3_7" runat="server"></span>
                        <br />
                        <uc:SeikyuuSiireLinkCtrl ID="ucSeikyuuSiireLink_3_7" runat="server" /><uc:TokubetuTaiouToolTipCtrl
                            ID="ucTokubetuTaiouToolTipCtrl_3_7" runat="server" />
                        <br />
                        <input type="text" id="shouhinNmText_3_7" style="display: none" runat="server" />
                        <span id="shouhinNm_3_7" runat="server"></span>
                    </td>
                    <td>
                        <input class="kingaku" maxlength="10" size="10" id="koumutenSeikyuZeinukiGaku_3_7"
                            runat="server" /><input id="HiddenKoumutenGakuHenkouKahi_3_7" type="hidden" runat="server" /></td>
                    <td>
                        <input class="kingaku" maxlength="10" size="10" id="jituSeikyuZeinukiGaku_3_7" runat="server" />
                        <input id="HiddenJituGakuHenkouKahi_3_7" type="hidden" runat="server" />
                    </td>
                    <td colspan="1" rowspan="1">
                        <input class="kingaku readOnlyStyle" maxlength="10" size="10" id="shouhiZei_3_7"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                    <td colspan="1">
                        <input class="kingaku readOnlyStyle" maxlength="10" size="10" id="zeikomiGaku_3_7"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                    <td>
                        <input class="kingaku" maxlength="10" size="10" id="shoudakuKingaku_3_7" runat="server" />
                        <input type="hidden" id="HiddenSyoudakuHenkouKahi_3_7" runat="server" />
                    </td>
                    <td>
                        <input class="date readOnlyStyle" maxlength="10" size="10" id="seikyuuHakkouDate_3_7"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                    <td>
                        <input class="date readOnlyStyle" maxlength="10" size="10" id="uriageDate_3_7" runat="server"
                            readonly="readOnly" tabindex="-1" /></td>
                    <td>
                        <select id="seikyuUmu_3_7" runat="server">
                            <option selected="selected" value="1">有</option>
                            <option value="0">無</option>
                        </select>
                        <span id="seikyuUmuSpan_3_7" runat="server"></span>
                    </td>
                    <td>
                        <input class="date readOnlyStyle" maxlength="10" size="10" id="mitumoriSakuseiDate_3_7"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                    <td style="width: 65px">
                        <select id="hattyuuKakutei_3_7" runat="server">
                            <option value="0">未確</option>
                            <option value="1">確定</option>
                        </select>
                        <span id="hattyuuKakuteiSpan_3_7" runat="server"></span>
                        <input id="hattyuuKakuteiOld_3_7" runat="server" style="width: 63px" type="hidden" /><span
                            id="pullSpan_24" runat="server"></span></td>
                    <td>
                        <input class="kingaku" maxlength="10" size="10" id="hattyuuKingaku_3_7" runat="server" /><input
                            type="hidden" id="HiddenHattyuuKingakuOld_3_7" runat="server" />
                    </td>
                    <td>
                        <input class="date readOnlyStyle" maxlength="10" size="10" id="hattyuuKakuninDate_3_7"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                </tr>
                <tr id="shouhinLine_3_8" runat="server">
                    <td class="itemNm">
                        <input type="hidden" id="bunruiCd_3_8" runat="server" />
                        <input type="hidden" id="zeikubun_3_8" runat="server" />
                        <input type="hidden" id="zeiritu_3_8" runat="server" />
                        <input type="hidden" id="kingakuFlg_3_8" runat="server" />
                        <input type="hidden" id="uriageJyoukyou_3_8" runat="server" />
                        <input type="hidden" id="uriageKeijyouFlg_3_8" runat="server" />
                        <input type="hidden" id="uriageKeijyouBi_3_8" runat="server" />
                        <input type="hidden" id="bikou_3_8" runat="server" />
                        <input type="hidden" id="ikkatuNyuukinFlg_3_8" runat="server" />
                        <input type="hidden" id="HiddenUpdDatetime_3_8" runat="server" />
                        <input type="hidden" id="HiddenKakuteiOld_3_8" runat="server" />
                        <input type="hidden" id="shouhinCdOld_3_8" runat="server" />
                        <input type="hidden" id="HidSyouhinSeikyuuSakiCd_3_8" runat="server" />
                        <input type="hidden" id="HidSiireSyouhizei_3_8" runat="server" />
                        <input maxlength="8" size="10" id="shouhinCd_3_8" runat="server" class="itemCd" /><input
                            id="shouhinSearch_3_8" value="索" type="button" class="itemSearchBtn" runat="server" />
                        <select id="kakutei_3_8" runat="server">
                            <option value="0">未確</option>
                            <option value="1">確定</option>
                        </select>
                        <span id="kakuteiSpan_3_8" runat="server"></span>
                        <br />
                        <uc:SeikyuuSiireLinkCtrl ID="ucSeikyuuSiireLink_3_8" runat="server" /><uc:TokubetuTaiouToolTipCtrl
                            ID="ucTokubetuTaiouToolTipCtrl_3_8" runat="server" />
                        <br />
                        <input type="text" id="shouhinNmText_3_8" style="display: none" runat="server" />
                        <span id="shouhinNm_3_8" runat="server"></span>
                    </td>
                    <td>
                        <input class="kingaku" maxlength="10" size="10" id="koumutenSeikyuZeinukiGaku_3_8"
                            runat="server" /><input id="HiddenKoumutenGakuHenkouKahi_3_8" type="hidden" runat="server" /></td>
                    <td>
                        <input class="kingaku" maxlength="10" size="10" id="jituSeikyuZeinukiGaku_3_8" runat="server" /><input
                            id="HiddenJituGakuHenkouKahi_3_8" type="hidden" runat="server" /></td>
                    <td colspan="1" rowspan="1">
                        <input class="kingaku readOnlyStyle" maxlength="10" size="10" id="shouhiZei_3_8"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                    <td colspan="1">
                        <input class="kingaku readOnlyStyle" maxlength="10" size="10" id="zeikomiGaku_3_8"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                    <td>
                        <input class="kingaku" maxlength="10" size="10" id="shoudakuKingaku_3_8" runat="server" />
                        <input type="hidden" id="HiddenSyoudakuHenkouKahi_3_8" runat="server" />
                    </td>
                    <td>
                        <input class="date readOnlyStyle" maxlength="10" size="10" id="seikyuuHakkouDate_3_8"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                    <td>
                        <input class="date readOnlyStyle" maxlength="10" size="10" id="uriageDate_3_8" runat="server"
                            readonly="readOnly" tabindex="-1" /></td>
                    <td>
                        <select id="seikyuUmu_3_8" runat="server">
                            <option selected="selected" value="1">有</option>
                            <option value="0">無</option>
                        </select>
                        <span id="seikyuUmuSpan_3_8" runat="server"></span>
                    </td>
                    <td>
                        <input class="date readOnlyStyle" maxlength="10" size="10" id="mitumoriSakuseiDate_3_8"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                    <td style="width: 65px">
                        <select id="hattyuuKakutei_3_8" runat="server">
                            <option value="0">未確</option>
                            <option value="1">確定</option>
                        </select>
                        <span id="hattyuuKakuteiSpan_3_8" runat="server"></span>
                        <input id="hattyuuKakuteiOld_3_8" runat="server" style="width: 63px" type="hidden" /><span
                            id="pullSpan_26" runat="server"></span></td>
                    <td>
                        <input class="kingaku" maxlength="10" size="10" id="hattyuuKingaku_3_8" runat="server" /><input
                            type="hidden" id="HiddenHattyuuKingakuOld_3_8" runat="server" />
                    </td>
                    <td>
                        <input class="date readOnlyStyle" maxlength="10" size="10" id="hattyuuKakuninDate_3_8"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                </tr>
                <tr id="shouhinLine_3_9" runat="server">
                    <td class="itemNm">
                        <input type="hidden" id="bunruiCd_3_9" runat="server" />
                        <input type="hidden" id="zeikubun_3_9" runat="server" />
                        <input type="hidden" id="zeiritu_3_9" runat="server" />
                        <input type="hidden" id="kingakuFlg_3_9" runat="server" />
                        <input type="hidden" id="uriageJyoukyou_3_9" runat="server" />
                        <input type="hidden" id="uriageKeijyouFlg_3_9" runat="server" />
                        <input type="hidden" id="uriageKeijyouBi_3_9" runat="server" />
                        <input type="hidden" id="bikou_3_9" runat="server" />
                        <input type="hidden" id="ikkatuNyuukinFlg_3_9" runat="server" />
                        <input type="hidden" id="HiddenUpdDatetime_3_9" runat="server" />
                        <input type="hidden" id="HiddenKakuteiOld_3_9" runat="server" />
                        <input type="hidden" id="shouhinCdOld_3_9" runat="server" />
                        <input type="hidden" id="HidSyouhinSeikyuuSakiCd_3_9" runat="server" />
                        <input type="hidden" id="HidSiireSyouhizei_3_9" runat="server" />
                        <input maxlength="8" size="10" id="shouhinCd_3_9" runat="server" class="itemCd" /><input
                            id="shouhinSearch_3_9" value="索" type="button" class="itemSearchBtn" runat="server" />
                        <select id="kakutei_3_9" runat="server">
                            <option value="0">未確</option>
                            <option value="1">確定</option>
                        </select>
                        <span id="kakuteiSpan_3_9" runat="server"></span>
                        <br />
                        <uc:SeikyuuSiireLinkCtrl ID="ucSeikyuuSiireLink_3_9" runat="server" /><uc:TokubetuTaiouToolTipCtrl
                            ID="ucTokubetuTaiouToolTipCtrl_3_9" runat="server" />
                        <br />
                        <input type="text" id="shouhinNmText_3_9" style="display: none" runat="server" />
                        <span id="shouhinNm_3_9" runat="server"></span>
                    </td>
                    <td>
                        <input class="kingaku" maxlength="10" size="10" id="koumutenSeikyuZeinukiGaku_3_9"
                            runat="server" /><input id="HiddenKoumutenGakuHenkouKahi_3_9" type="hidden" runat="server" /></td>
                    <td>
                        <input class="kingaku" maxlength="10" size="10" id="jituSeikyuZeinukiGaku_3_9" runat="server" />
                        <input id="HiddenJituGakuHenkouKahi_3_9" type="hidden" runat="server" /></td>
                    <td colspan="1" rowspan="1">
                        <input class="kingaku readOnlyStyle" maxlength="10" size="10" id="shouhiZei_3_9"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                    <td colspan="1">
                        <input class="kingaku readOnlyStyle" maxlength="10" size="10" id="zeikomiGaku_3_9"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                    <td>
                        <input class="kingaku" maxlength="10" size="10" id="shoudakuKingaku_3_9" runat="server" />
                        <input type="hidden" id="HiddenSyoudakuHenkouKahi_3_9" runat="server" />
                    </td>
                    <td>
                        <input class="date readOnlyStyle" maxlength="10" size="10" id="seikyuuHakkouDate_3_9"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                    <td>
                        <input class="date readOnlyStyle" maxlength="10" size="10" id="uriageDate_3_9" runat="server"
                            readonly="readOnly" tabindex="-1" /></td>
                    <td>
                        <select id="seikyuUmu_3_9" runat="server">
                            <option selected="selected" value="1">有</option>
                            <option value="0">無</option>
                        </select>
                        <span id="seikyuUmuSpan_3_9" runat="server"></span>
                    </td>
                    <td>
                        <input class="date readOnlyStyle" maxlength="10" size="10" id="mitumoriSakuseiDate_3_9"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                    <td style="width: 65px">
                        <select id="hattyuuKakutei_3_9" runat="server">
                            <option value="0">未確</option>
                            <option value="1">確定</option>
                        </select>
                        <span id="hattyuuKakuteiSpan_3_9" runat="server"></span>
                        <input id="hattyuuKakuteiOld_3_9" runat="server" style="width: 63px" type="hidden" /><span
                            id="pullSpan_28" runat="server"></span></td>
                    <td>
                        <input class="kingaku" maxlength="10" size="10" id="hattyuuKingaku_3_9" runat="server" /><input
                            type="hidden" id="HiddenHattyuuKingakuOld_3_9" runat="server" />
                    </td>
                    <td>
                        <input class="date readOnlyStyle" maxlength="10" size="10" id="hattyuuKakuninDate_3_9"
                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                </tr>
                <tr align="center" style="display: none">
                    <td colspan="14" rowspan="1">
                        <br />
                        <table style="text-align: left;" class="mainTable" cellpadding="2">
                            <tbody>
                                <tr class="shouhinTableTitleNyuukin">
                                    <td>
                                        入金額（税込）</td>
                                    <td>
                                        残額</td>
                                </tr>
                                <tr>
                                    <td>
                                        <input class="kingaku" maxlength="25" size="25" id="nyuukinGaku_3" style="width: 120px"
                                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                                    <td>
                                        <input class="kingaku" maxlength="25" size="25" id="nyuukinZanGaku_3" style="width: 120px"
                                            runat="server" readonly="readOnly" tabindex="-1" /></td>
                                </tr>
                            </tbody>
                        </table>
                        <br />
                    </td>
                </tr>
            </tbody>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
<!--商品検索画面呼び出し元ターゲット商品行番号-->
<input type="hidden" id="shouhinSearchTargetLineNo" runat="server" />
<!--Irai1情報保持-->
<input type="hidden" id="kubunVal" runat="server" />
<input type="hidden" id="hosyousyoNoVal" runat="server" />
<input type="hidden" id="hakiSyubetuVal" runat="server" />
<input type="hidden" id="kubunId" runat="server" />
<input type="hidden" id="hosyousyoNoId" runat="server" />
<input type="hidden" id="HiddenInsTokubetuTaiouFlg" runat="server" />
<input type="hidden" id="HiddenRentouBukkenSuu" runat="server" />
<!--参照地盤データ更新日付-->
<input type="hidden" id="updateDateTime" runat="server" />
<!-- 画面起動時情報保持_原価 (非表示) -->
<input type="hidden" id="HiddenOpenValuesGenka" runat="server" />
<!-- 画面起動時情報保持_販売価格 (非表示) -->
<input type="hidden" id="HiddenOpenValuesHanbai" runat="server" />
<!-- 画面起動時情報保持_販売価格 (非表示) -->
<input type="hidden" id="HiddenOpenValuesSyouhinKkk" runat="server" />
<!-- 画面遷移時情報保持_確認モード (非表示) -->
<input type="hidden" id="HiddenModeKakunin" runat="server" />

var $D  = document;
var $ID = function(id){return $D.getElementById(id);}

var strComReadOnlyBgColor = "#D0D0D0";      //使用不可の背景色
var strComDefaultBgColor = "white";         //平常の背景色

//フォーカス事件フラグ
var focusoutFlg = true;

function objEBI(strTargetId) {
  return window.document.getElementById(strTargetId);
}
function offsetTopDoc(e){ var t = 0; while(e){ t += e.offsetTop; e = e.offsetParent; } return t; }
function offsetLeftDoc(e){ var l = 0; while(e){ l += e.offsetLeft; e = e.offsetParent; } return l; }
function changeTableSize(targetId, minHeight, adjustHeight, adjustWidth){

  if(objEBI(targetId) == null || objEBI(targetId) == undefined) return false;

  //ウィンドウのサイズを取得
  var winHeight = document.documentElement.clientHeight;
  var winWidth = document.documentElement.clientWidth;
  
  //調整値が未指定の場合、デフォルト値をセット
  if(!adjustHeight || adjustHeight == undefined) adjustHeight = 20;
  if(!adjustWidth  || adjustWidth  == undefined) adjustWidth  = 20;
  if(!minHeight  || minHeight  == undefined) minHeight  = 50;
  
  //調整値を現在のオブジェクトの位置を考慮して設定
  adjustHeight = offsetTopDoc(objEBI(targetId)) + adjustHeight;
  adjustWidth = offsetLeftDoc(objEBI(targetId)) + adjustWidth;
  
  //高さ設定実行
  if(winHeight > adjustHeight){
    if((winHeight - adjustHeight) > minHeight){
      objEBI(targetId).style.height = (winHeight - adjustHeight) + "px";
    }else{
      //最低高以下に変更使用としている場合、最低高にセット
      objEBI(targetId).style.height = minHeight + "px";
    }
  }
  //幅設定実行
  if(winWidth > adjustWidth){
    objEBI(targetId).style.width = (winWidth - adjustWidth) + "px";
  }
}
function setMenuBgColor() {

  var menuId = null;
  switch (window.name) {
  case "MainMenu.aspx": // 「メイン」画面
    menuId = "menu_lnk_main";
    break;
  case "EigyouKeikakuKanriMenu.aspx": // 「営業計画管理」画面
    menuId = "menu_lnk_eigyou_keikaku";
    break;
  case "UriageYojituKanriMenu.aspx": // 「売上予実管理」画面
    menuId = "menu_lnk_uri_yojitu";
    break;
  case "KeikakuKanriKameitenKensakuSyoukaiInquiry.aspx": // 「計画管理_加盟店検索照会指示」画面
    menuId = "menu_lnk_keikaku_kanri_kameiten_kensaku_syoukai";
    break;
  default:
    break;
  }

  if (menuId != null) {
    objEBI(menuId).style.backgroundColor = "orange";
  }
}

function GetValueToNumber(objValue)
{
    var strValue;
    strValue = objValue.toString();
    if (strValue == "")
    {
        return 0;
    }
    strValue = strValue.replace(/[,]/g,'');
    strValue = strValue.replace('¥','');
    strValue = strValue.replace('%','');
    return Number(strValue);
}


/**
 * 数値変換(カンマを削除)
 * @param objControl:付与対象値
 */
function SetNumberFocusEnter(objControl) {
    var strValue;
    strValue = objControl.value;
    strValue = strValue.replace(/[,]/g,'');
    strValue = strValue.replace('¥','');
    strValue = strValue.replace('%','');
    objControl.value = strValue;
}

/**
 * 数値変換(カンマ区切り)
 * @param objControl:付与対象値
 * @param strName:付与対象名
 */
function SetNumberFocusOut(objControl,strName,strLeftFormat,strRightFormat) {
    var strValue;
    strValue = objControl.value;
    
    if (strValue == '') {
        return true;
    }
    
    if(isNaN(strValue)) {
        focusoutFlg = false;
        alert("半角数字で入力して下さい。");
        objControl.select();
        focusoutFlg = true;
        return false;
    }
    
    // 適切な数値に変換する 000や-00 -> 0
    strValue = Number(strValue)
    strValue = String(strValue)
       
    while(strValue != (strValue = strValue.replace(/^(-?\d+)(\d{3})/, "$1,$2"))); //3桁区切りでカンマ付与
  
    objControl.value = strLeftFormat + strValue + strRightFormat
    //objControl.value = strValue
    
    return true;
}

/**
 * 小数変換(カンマ区切り)
 * @param objControl:付与対象値
 * @param strName:付与対象名
 * @param strLeftFormat:左側の文字列
 * @param strRightFormat:右側の文字列
 * @param intDecimalLength:小数桁数
 */
function SetDecimalFocusOut(objControl,strName,strLeftFormat,strRightFormat,intDecimalLength) {
    var strValue;
    var intLength;
    var i;
    
    strValue = objControl.value;
    
    if (strValue == '') {
        return true;
    }
    
    if(isNaN(strValue)) {
        focusoutFlg = false;
        alert("半角数字で入力して下さい。");
        objControl.select();
        focusoutFlg = true;
        return false;
    }
    
    intLength = 1;
    for (i=1;i<=intDecimalLength;i++)
    {
        intLength = intLength * 10
    }
    
    // 適切な数値に変換する 000や-00 -> 0
    strValue = Math.round(Number(strValue) * intLength) / intLength
    strValue = String(strValue)

    while(strValue != (strValue = strValue.replace(/^(-?\d+)(\d{3})/, "$1,$2"))); //3桁区切りでカンマ付与
  
    objControl.value = strLeftFormat + strValue + strRightFormat
    //objControl.value = strValue
    
    return true;
}

/**
 * tableの選択行の背景色変更
 * 
 * @param objTarget:選択されたtdエレメントのID
 * @param objGridTBody:対象とするtableのtbodyエレメントのID
 * @return
 */
//グローバル変数
var tmpBeforSelectedTrObject = null;  //前に選択されていた行オブジェクト

var tmpBeforSelectedTrColor = null;   //前に選択されていた行オブジェクトの元の背景色
function selectedLineColor(objTarget) {

  //objTargetがTRで無い場合、親要素を検索する
  while(objTarget.tagName != "TR"){
    if(objTarget.parentNode == undefined) return;
    objTarget = objTarget.parentNode;
  }

  //前に選択されていた行を、元の背景色に戻す

  if(tmpBeforSelectedTrObject != null){
    tmpBeforSelectedTrObject.style.backgroundColor = tmpBeforSelectedTrColor;
  }
  //新しく選択された行のオブジェクトと背景色の状態を、グローバル変数に退避
  tmpBeforSelectedTrObject = objTarget;
  tmpBeforSelectedTrColor = tmpBeforSelectedTrObject.style.backgroundColor;

  //選択行の背景色を変更
  objTarget.style.backgroundColor = "pink";
  objTarget.focus();
}

 /**
* 入力値がその項目の最大桁数未満の入力の場合、入力値の前に桁数を満たす「0」を付加する
* strValue:対象文字列
* numCount:桁数
* (例えば: 101 -> 000101)
*/
function PadLeft(strValue,numCount){
    var i;
    var returnValue;
    
    if (strValue == '')
    {
        return '';
    }
                
    if (strValue.length >= numCount)
    {
        return strValue;
    }
    
    if (numCount <= 0)
    {
        return strValue;
    }
    
    returnValue = strValue;
    for (i=strValue.length; i<numCount; i++)
    {
        returnValue = '0' + returnValue;
    }
    
    return returnValue
}

function SetControlReadOnly(objControl,intFlg)
{
    if (intFlg == 1)
    {
        objControl.readOnly=true;
        objControl.tabIndex=-1;
        //objControl.style.color=ReadOnlyfontColor;
        objControl.style.backcolor=strComReadOnlyBgColor;
    }else
    {
        objControl.readOnly=false;
        objControl.tabIndex=0;
        //objControl.style.color=ReadOnlyfontColor;
        objControl.style.backcolor=strComDefaultBgColor;
    }
}


function ShowModal()
{
    try
    {
        var buyDiv=document.getElementById('ctl00_buySelName');
        var disable=document.getElementById('ctl00_disableDiv');
        if(buyDiv.style.display=='none')
        {
            buyDiv.style.display='';
            disable.style.display='';
            disable.focus();
        }
        else
        {
            buyDiv.style.display='none';
            disable.style.display='none';
        }
    }
    catch(e1)
    {}
   }


var tmpBeforSelectedTrObject = null;  //前に選択されていた行オブジェクト
var tmpBeforSelectedTrObject1 = null;  //前に選択されていた行オブジェクト
var tmpBeforSelectedTrColor = null;   //前に選択されていた行オブジェクトの元の背景色
var tmpBeforSelectedTrColor1 = null;   //前に選択されていた行オブジェクトの元の背景色
function setSelectedLineColor(objTarget,objTarget1) {

  //objTargetがTRで無い場合、親要素を検索する
  while(objTarget.tagName != "TR"){
    if(objTarget.parentNode == undefined) return;
    objTarget = objTarget.parentNode;
  }

  //前に選択されていた行を、元の背景色に戻す
  if(tmpBeforSelectedTrObject != null){
    tmpBeforSelectedTrObject.style.backgroundColor = tmpBeforSelectedTrColor;
  }
  if(tmpBeforSelectedTrObject1 != null){
    tmpBeforSelectedTrObject1.style.backgroundColor = tmpBeforSelectedTrColor1;
  }
  //新しく選択された行のオブジェクトと背景色の状態を、グローバル変数に退避
  tmpBeforSelectedTrObject = objTarget;
  tmpBeforSelectedTrColor = tmpBeforSelectedTrObject.style.backgroundColor;
  tmpBeforSelectedTrObject1 = objTarget1;
  tmpBeforSelectedTrColor1 = tmpBeforSelectedTrObject1.style.backgroundColor;

  //選択行の背景色を変更
  objTarget.style.backgroundColor = "pink";
  objTarget1.style.backgroundColor = "pink";
  objTarget.focus();
}

/**
 * String.Trim
 * 文字列のトリム処理

 * @return
 */
String.prototype.Trim = function(){return this.replace(/^\s+|\s+$/g, "");}



/**
 * 日付用スラッシュ付与
 * @param str:付与対象値
 * @return val:付与後値 or false
 */
function addSlash(str){
  var val = "";
  str = removeSlash(str); //スラッシュ除去
  if(str.length != 8) return false; //8桁で無ければfalse
  val = str.substring(0, 4) + "/" + str.substring(4, 6) + "/" + str.substring(6, 8);  //yyyy/mm/dd
  return(val);
}

/**
 * スラッシュ除去
 * @param val:除去対象値
 * @return num:除去後値
 */
function removeSlash(val) {
  return new String(val).replace(/\//g, "");  //カンマを削除
}

/**
 * 日付妥当性チェック関数
 * @param y:年
 * @param m:月
 * @param d:日
 * @return boolean
 */
function checkDateVali(y,m,d){
  var di = new Date(y,m-1,d);
  if(di.getFullYear() == y && di.getMonth() == m-1 && di.getDate() == d){
    return true;
  }
  return false;
}

/**
 * 子ノードの中から、指定タグを持つ要素を配列化する
 * @param obj:親オブジェクト
 * @param tag:指定タグ
 * @return object
 */
function getChildArr(obj,tag){
  var arrCn = obj.childNodes;
  var arrRet = new Array();
  var numRet = 0;
  for(ci=0;ci<arrCn.length;ci++){
    if(arrCn[ci].tagName == tag){
      arrRet[numRet] = arrCn[ci];
      numRet++;
    }
  }
  return arrRet;
}

function SetDateNoSign(value,sign){
 
var arr;
arr = value.split(sign);
 
var i;
 
for(i=0;i<=arr.length-1;i++){
    if(arr[i].length==1){
        arr[i] = "0" + arr[i];        
    }
}
 
return arr.join("");
 
} 

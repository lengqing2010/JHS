/**
 * グローバル変数宣言
 */
var _d = null; // document格納用
var _form1 = null; // メインform格納用
var objSrchWin = null; // 検索ウィンドウオブジェクト
var IE='\v'=='v';

/**
 * グローバル定数宣言
 */
var sepStr = "$$$";

/**
 * onloadイベント
 */
window.onload = initPage; 
 
/**
 * 初期設定
 * @return 
 * @throws 
 */
function initPage() {
  // window.focus();
  _d = window.document; // window.documentを変数化
  _form1 = _d.getElementsByTagName("form")[0]; // 一番目のformを変数化
  
  // イベントハンドラ設定
  // ボタン用
  var listInput = _d.getElementsByTagName("input");
  for ( var i = 0; i < listInput.length; i++) {
    if (listInput[i].type == "button") {
      switch (listInput[i].id) {
      case "btnCloseWin": // 「閉じる」ボタン
        listInput[i].onclick = function() {
          window.close();
        };
        break;
      }
    } else if (listInput[i].type == "reset") {
      // 「クリア」ボタン
      listInput[i].onclick = function() {
        allClear(false);
      };
    }
  }
  
  // グリッドテーブルの初期表示設定
  initGridTable(objEBI("searchGrid"));
  initGridTable(objEBI("ctl00_ContentPlaceHolder1_searchGrid"));
  
  // メニューバーのリンク背景色を画面ごとに変更
  setMenuBgColor();
  
  
  /*------------------------------------------------
  //各画面固有のonload時に実行させたい処理を実行
  ------------------------------------------------*/
  if(typeof funcAfterOnload == "function"){
      funcAfterOnload();
  }
  
}

/**
 * 「document.getElementById」の代替メソッド。document指定を、変数化された要素を使用することで、パフォーマンスを向上させている
 * @param strTargetId
 * @return
 */
function objEBI(strTargetId) {
  return _d.getElementById(strTargetId);
}


/**
 * 「offsetTop」「offsetLeft」の代替メソッド。documentからの相対座標を取得する。
 * @param e：座標取得対象オブジェクト
 * @return t/l：座標情報
 */
function offsetTopDoc(e){ var t = 0; while(e){ t += e.offsetTop; e = e.offsetParent; } return t; }
function offsetLeftDoc(e){ var l = 0; while(e){ l += e.offsetLeft; e = e.offsetParent; } return l; }

// /////////////////////////////////////////////////////////////////////////////////////
// 画面表示設定系
// /////////////////////////////////////////////////////////////////////////////////////

/**
 * 画面IDによって、メニューバーの背景色を変更する
 * 
 * @return
 */
function setMenuBgColor() {
  var objGamenId = objEBI("ctl00_ContentPlaceHolder1_gamenId");
  if (objGamenId == null) {
    return;
  }
  var gamenId = objGamenId.value;
  var menuId = null;
  switch (gamenId) {
  case "main": // 「メイン」画面
    menuId = "menu_lnk_main";
    break;
  case "shinki": // 「新規受注」画面
    menuId = "menu_lnk_sinki";
    break;
  case "kensaku": // 「物件検索」画面
    menuId = "menu_lnk_kensaku";
    break;
  default:
    break;
  }

  if (menuId != null) {
    objEBI(menuId).style.backgroundColor = "orange";
  }
}

/**
 * 表示中のエレメントを非表示にし、非表示中のエレメントを表示にする
 * 
 * @param strTargetId:表示切替対象エレメントのID
 * @param strTmpDispSettingId:display状態を保持するオブジェクトのID
 * @return
 */
function changeDisplay(strTargetId,strTmpDispSettingId) {
  var objTarget = objEBI(strTargetId);
  if (objTarget.style.display != "none") {
    objTarget.style.display = "none";
  } else {
    objTarget.style.display = "inline";
  }
  if(strTmpDispSettingId != undefined && strTmpDispSettingId != ""){
    objEBI(strTmpDispSettingId).value = objTarget.style.display;
  }
}

/**
 * 全ての入力エレメントを使用不可状態を切替える
 * 
 * @param setType:無効化する場合＝true / 有効化する場合＝false
 * @param strIDs:setTypeの逆に設定するエレメントのID郡(カンマ区切り)
 * @param strExIDs:常に有効化するエレメントのID郡(カンマ区切り)
 * @return
 */
function setDisabledAll(setType, strIDs, strExIDs) {
  if(_d == null){
    _d = document;
  }
  
  var arrInput = _d.getElementsByTagName("input");
  var arrSelect = _d.getElementsByTagName("select");
  var arrTextArea = _d.getElementsByTagName("textarea");
  
  for(var i=0;i<arrInput.length; i++){  //inputタグ
    arrInput[i].disabled = setType;
  }
  for(var i=0;i<arrSelect.length; i++){ //selectタグ
    arrSelect[i].disabled = setType;
  }
  for(var i=0;i<arrTextArea.length; i++){ //textareaタグ
    arrTextArea[i].disabled = setType;
  }
  
  //指定IDのdisabledをsetTypeの逆に設定しなおす
  var arrDisId = new Array();
  if(strIDs != undefined){
    arrDisId = strIDs.split(",");
    for(var i=0;i<arrDisId.length; i++){
      if(objEBI(arrDisId[i])){
        objEBI(arrDisId[i]).disabled = (setType == false);
      }
    }
  }

  //指定IDのdisabledをFalseに設定しなおす(常に有効化)
  var arrDisIdex = new Array();
  if(strExIDs != undefined){
    arrDisIdex = strExIDs.split(",");
    for(var i=0;i<arrDisIdex.length; i++){
      if(objEBI(arrDisIdex[i])){
        objEBI(arrDisIdex[i]).disabled = false;
      }
    }
  }

}

/**
 * 全ての入力エレメントを表示状態(visibility)を切替える
 * 
 * @param setType:非表示にする場合＝true / 表示する場合＝false
 * @param strIDs:setTypeの逆に設定するエレメントのID郡(カンマ区切り)
 * @param strExIDs:常に表示するエレメントのID郡(カンマ区切り)
 * @return
 */
function setVisibilityAll(setType, strIDs, strExIDs) {
  if(_d == null){
    _d = document;
  }
  
  var arrInput = _d.getElementsByTagName("input");
  var arrSelect = _d.getElementsByTagName("select");
  var arrTextArea = _d.getElementsByTagName("textarea");
  var visType = null;
  var visTypeR = null;
  
  if(setType){
    visType = "hidden";
    visTypeR = "visible";
  }else{
    visType = "visible";
    visTypeR = "hidden";
  }
  
  for(var i=0;i<arrInput.length; i++){  //inputタグ
    arrInput[i].style.visibility = visType;
  }
  for(var i=0;i<arrSelect.length; i++){ //selectタグ
    arrSelect[i].style.visibility = visType;
  }
  for(var i=0;i<arrTextArea.length; i++){ //textareaタグ
    arrTextArea[i].style.visibility = visType;
  }
  
  //指定IDのvisibilityをsetTypeの逆に設定しなおす
  var arrDisId = new Array();
  if(strIDs != undefined){
    arrDisId = strIDs.split(",");
    for(var i=0;i<arrDisId.length; i++){
      if(objEBI(arrDisId[i])){
        objEBI(arrDisId[i]).style.visibility = visTypeR;
      }
    }
  }

  //指定IDのvisibilityをvisibleに設定しなおす(常に表示)
  var arrDisIdex = new Array();
  if(strExIDs != undefined){
    arrDisIdex = strExIDs.split(",");
    for(var i=0;i<arrDisIdex.length; i++){
      if(objEBI(arrDisIdex[i])){
        objEBI(arrDisIdex[i]).style.visibility = "visible";
      }
    }
  }

}

/**
 * 特定の入力エレメントを表示状態(visibility)を切替える
 * 
 * @param setType:非表示にする場合＝true / 表示する場合＝false
 * @param strIDs:設定するエレメントのID郡(カンマ区切り)
 * @return
 */
function setVisibility(setType, strIDs) {
  if(_d == null){
    _d = document;
  }
  
  var visType = null;
  var visTypeR = null;
  
  if(setType){
    visType = "hidden";
  }else{
    visType = "visible";
  }
  
  //指定IDのdisabledを設定する
  var arrDisId = new Array();
  if(strIDs != undefined){
    arrDisId = strIDs.split(",");
  }
  for(var i=0;i<arrDisId.length; i++){
    if(objEBI(arrDisId[i])){
      objEBI(arrDisId[i]).style.visibility = visType;
    }
  }

}

/**
 * 指定されたエレメントの有効/無効を切替える
 * 
 * @param strDisIDs:無効化するエレメントのID郡(カンマ区切り)
 * @param strUnDisIDs:有効化するエレメントのID郡(カンマ区切り)
 * @return
 */
function changeDisabledById(strDisIDs,strUnDisIDs) {
  var arrDisId = strDisIDs.split(",");
  var arrUnDisId = strUnDisIDs.split(",");

  //指定IDのdisabledをtrueに設定する(=無効化する)
  for(var i=0;i<arrDisId.length; i++){
    var tmpObj = objEBI(arrDisId[i]);
    if(tmpObj){
      tmpObj.disabled = true;
    }
  }
  
  //指定IDのdisabledをfalseに設定する(=有効化する)
  for(var i=0;i<arrUnDisId.length; i++){
    var tmpObj = objEBI(arrUnDisId[i]);
    if(tmpObj){
      tmpObj.disabled = false;
    }
  }

}

// /////////////////////////////////////////////////////////////////////////////////////
// 画面項目値設定
// /////////////////////////////////////////////////////////////////////////////////////

/**
 * 全ての入力エレメントの入力値をクリアする
 * 
 * @param flgHidden:hidden項目をクリアする場合＝true / hidden項目は変更しない場合＝false
 * @return
 */
function allClear(flgHidden) {
  if(_d == null){
    _d = document;
  }
  
  var arrInput = _d.getElementsByTagName("input");
  var arrSelect = _d.getElementsByTagName("select");
  var arrTextArea = _d.getElementsByTagName("textarea");
  
  for(var i=0;i<arrInput.length; i++){  //inputタグ
    if(arrInput[i].type == "text" || arrInput[i].type == "hidden"){
      if(!flgHidden && arrInput[i].type == "hidden" ){
      }else{
        arrInput[i].value = "";
      }
    }
    if(arrInput[i].type == "checkbox" || arrInput[i].type == "radio"){
      arrInput[i].checked = false;
    }
  }
  for(var i=0;i<arrSelect.length; i++){ //selectタグ
    arrSelect[i].selectedIndex = 0;
  }
  for(var i=0;i<arrTextArea.length; i++){ //textareaタグ
    arrTextArea[i].value = "";
  }

  //以降のイベントをキャンセル
  event.returnValue = false;

  /*------------------------------------------------
  //各画面固有のAllクリア時に実行させたい処理を実行
  ------------------------------------------------*/
  if(typeof funcAfterAllClear == "function"){
      funcAfterAllClear();
  }
  
}

/**
 * コード入力値をプルダウンへ反映
 * 
 * @param this:コード入力項目オブジェクト
 * @param targetId:セット先エレメントID
 * @return
 */
function setCode2Pull(obj,targetId){
    if(event.keyCode != 9){  //タブキーの場合、アクションを起こさない
        var pull = objEBI(targetId);
        if(pull.value != obj.value){
          pull.value = obj.value;
          pull.fireEvent('onchange');
        }
    }
}

// /////////////////////////////////////////////////////////////////////////////////////
// テーブル操作系
// /////////////////////////////////////////////////////////////////////////////////////

/**
 * テーブルの表示、イベント設定
 * @return 
 * @throws 
 * @param objGridTBody :対象のテーブルTbodyオブジェクト
 */
function initGridTable(objGridTBody){
  //指定されたテーブルTbodyオブジェクトが存在する場合
  if (objGridTBody != null) {
    if(IE){
      objGridTBody.ondblclick = function() {
        returnSelectValue(event.srcElement.parentNode);      };
      objGridTBody.onmousedown = function() {
        selectedLineColor(event.srcElement.parentNode,objGridTBody);
      };
    }else{
      objGridTBody.ondblclick = function(event) {
        returnSelectValue(event.target.parentNode);
      };
      objGridTBody.onmousedown = function(event) {
        selectedLineColor(event.target.parentNode,objGridTBody);
      };
    }
    objGridTBody.onkeydown = function() {
      //alert(event.keyCode);
      if(event.keyCode == 40){  //カーソルキー(下)
        selectedLineMove(1);
      }
      if(event.keyCode == 38){  //カーソルキー(上)
        selectedLineMove(-1);
      }
      if(event.keyCode == 13){  //エンターキー
        getChildArr(tmpBeforSelectedTrObject,"TD")[0].fireEvent("ondblclick");
      }
    };
    
    //テーブルの行色設定
    setGridColor(objGridTBody);
    
    //テーブルサイズ調整実行
    changeTableSize("dataGridContent");
    //onresizeイベントハンドラにテーブルのサイズ調整を設定
    window.onresize = function(){
      changeTableSize("dataGridContent"); 
    }

    // テーブルのセル幅を揃える（タイトル行固定テーブル用 for not IE）
    /*if (!IE && objEBI("meisaiTableHeaderTr") != null && objEBI("meisaiTableTopTr") != null) {
      setTableWidth(objEBI("meisaiTableHeaderTr"), objEBI("meisaiTableTopTr"));
    }else if (objEBI("meisaiTableHeaderTr") != null) {
      setTableWidth2(objEBI("meisaiTableHeaderTr"));
    }*/

  }else{
    return false;
  }
}

/**
 * tableのタイトル行の幅とデータ行の幅を揃える
 * 
 * @param objMotoTr:タイトル行のtrオブジェクト(セルはth指定されている事)
 * @param objSakiTr:データ行のtrオブジェクト
 * @return
 */
function setTableWidth(objMotoTr, objSakiTr) {
  var arrMotoTh = getChildArr(objMotoTr,"TH");
  var arrSakiTd = getChildArr(objSakiTr,"TD");
  for ( var i = 0; i < arrMotoTh.length; i++) {
    arrSakiTd[i].style.width = arrMotoTh[i].style.width;
  }
}

/**
 * tableのタイトル行の幅とデータ行の幅を揃えるver2
 * 
 * @param objMotoTr:タイトル行のtrオブジェクト(セルはth指定されている事)
 * @param objSakiTr:データ行のtrオブジェクト
 * @return
 */
function setTableWidth2(objMotoTr) {
  var objTable = objMotoTr.parentNode.parentNode;
  var arrMotoTh = getChildArr(objMotoTr,"TH");
  var arrSakiTr = getChildArr(getChildArr(objTable,"TBODY")[0],"TR");
  for ( var tri = 0; tri < arrSakiTr.length; tri++) {
    var arrSakiTd = getChildArr(arrSakiTr[tri],"TD");
    for ( var i = 0; i < arrSakiTd.length; i++) {
      arrSakiTd[i].style.width = arrMotoTh[i].style.width;
    }
  }
}

/**
 * 奇数行、偶数行で背景色を変更
 * 
 * @param objGridTBody:対象とするtableのtbodyエレメントのID
 * @return
 */
function setGridColor(objGridTBody) {
  var countTr = 0;
  var arrTr = getChildArr(objGridTBody,"TR");
  for ( var i = 0; i < arrTr.length; i++) {
    var objTr = arrTr[i];
    if (countTr % 2 == 0) {
      objTr.style.backgroundColor = "white";
    } else {
      objTr.style.backgroundColor = "LightCyan";
    }
    countTr++;
  }
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
function selectedLineColor(objTarget, objGridTBody) {

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
 * カーソルキーでテーブル行を移動
 * @param move:移動行数(1 or -1)
 * @return
 */
function selectedLineMove(move){
  var tmpObjTr = tmpBeforSelectedTrObject;
  if(tmpObjTr && tmpObjTr.id != ""){
    var tmpArrStrId = tmpObjTr.id.split("_");
    var tmpLineNo = tmpArrStrId[tmpArrStrId.length-1];
    var re = new RegExp(tmpLineNo + "$"); 
    var tmpNextTr = objEBI(tmpObjTr.id.replace(re,"") + (Number(tmpLineNo) + move));
    if(tmpNextTr){
      var objDg = objEBI("dataGridContent");
      var objMt = objEBI("meisaiTableHeaderTr");
      moveTableScroll(objDg,objMt,tmpNextTr);
      getChildArr(tmpNextTr,"TD")[0].fireEvent("onmousedown");
      tmpNextTr.focus();
    }
  }
  //以降のイベントをキャンセル
  event.returnValue = false;
}

/**
 * テーブルのスクロール位置を調整
 * 
 * @param objDg:スクロールしているオブジェクト
 * @param tmpNextTr:スクロール位置を決める、行オブジェクト
 * @return
 */
function moveTableScroll(objDg,objMt,tmpNextTr){
  if(tmpNextTr.offsetTop >= objDg.scrollTop + (objDg.clientHeight - tmpNextTr.clientHeight)){
    objDg.scrollTop = tmpNextTr.offsetTop - (objDg.clientHeight - tmpNextTr.clientHeight);
  }
  if(tmpNextTr.offsetTop <= (objDg.scrollTop + objMt.clientHeight)){
    objDg.scrollTop = tmpNextTr.offsetTop - objMt.clientHeight;
  }
}

/**
 * テーブルのスクロール位置を最上段にセットする
 * 
 * @return
 */
function moveScrollTop(){
  objEBI("dataGridContent").scrollTop = 0;
}

/**
 * テーブルのheight/widthをウィンドウサイズに合わせて調整
 * 
 * @param targetId:サイズ変更対象オブジェクト
 * @param adjustHeight:調整高さ
 * @param adjustWidth:調整幅
 * @return
 */
function changeTableSize(targetId, adjustHeight, adjustWidth){
  //ウィンドウのサイズを取得
  var winHeight = document.documentElement.clientHeight;
  var winWidth = document.documentElement.clientWidth;
  
  //調整値が未指定の場合、デフォルト値をセット
  if(!adjustHeight || adjustHeight == undefined) adjustHeight = 20;
  if(!adjustWidth  || adjustWidth  == undefined) adjustWidth  = 20;
  
  //調整値を現在のオブジェクトの位置を考慮して設定
  adjustHeight = offsetTopDoc(objEBI(targetId)) + adjustHeight;
  adjustWidth = offsetLeftDoc(objEBI(targetId)) + adjustWidth;
  
  //高さ設定実行
  if(winHeight > adjustHeight){
    objEBI(targetId).style.height = winHeight - adjustHeight;
  }
  //幅設定実行
  if(winWidth > adjustWidth){
    objEBI(targetId).style.width = winWidth - adjustWidth;
  }
}

// /////////////////////////////////////////////////////////////////////////////////////
// ウィンドウ操作系
// /////////////////////////////////////////////////////////////////////////////////////

/**
 * 検索ウィンドウを開く
 * 
 * @param strTargetIds:検索画面に引き渡す値を保持しているエレメントのID郡（$$$区切り）
 * @param actionUrl:検索画面呼び出し先Url（formのactionに指定する）
 * @param returnTargetIds:検索画面で選択した値の戻し先エレメントのID郡（$$$区切り）
 * @param returnTargetIds:検索画面から親画面に値をセットした後に実行する、親画面のボタンオブジェクトのID(任意指定)
 * @return
 */
function callSearch(strTargetIds, actionUrl, returnTargetIds, afterEventBtnId) {
  if (!objSrchWin || objSrchWin.closed) {
    objSrchWin = window.open("about:Blank", "searchWindow", "menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes");
  }
  var sform = objEBI("searchForm");
  sform.reset();
  var arrTargetId = strTargetIds.split(sepStr);
  for (ti = 0; ti < arrTargetId.length; ti++) {
    objEBI("sendSearchTerms").value += objEBI(arrTargetId[ti]).value + sepStr;
  }

  objEBI("returnTargetIds").value = returnTargetIds;

  //後処理ボタンIDが指定されている場合、値をセット
  if(afterEventBtnId != undefined && afterEventBtnId != ""){
    objEBI("afterEventBtnId").value = afterEventBtnId;
  }
  
  sform.method = "post";
  sform.action = actionUrl;
  sform.target = "searchWindow";
  sform.submit();

  objSrchWin.focus();
}

/**
 * 検索ウィンドウから値を親ウィンドウにセットする
 * 
 * @param objSelectedTr:検索画面で選択された行オブジェクト
 * @return
 */
function returnSelectValue(objSelectedTr){
    //親ウィンドウが閉じていないかのチェック
    if(window.opener == null || window.opener.closed){
        alert("呼び出し元画面が閉じられた為、値をセットできません。");
        return false;
    }
    
    var _wod = window.opener.document; //親ウィンドウのドキュメントオブジェクト
    var objSelRet = getChildArr(getChildArr(objSelectedTr,"TD")[0],"INPUT")[0];
    var arrReturnValue = objSelRet.value.split(sepStr);  //戻り値郡配列
    var arrReturnTargetId = document.getElementById("returnTargetIds").value.split(sepStr);  //値のセット先ID郡配列
    var afterEventBtnId = document.getElementById("afterEventBtnId").value;  //値セット後に押下する、親画面のボタンID
    
    //親ウィンドウへの値セット処理
    for(var ri=0; ri<arrReturnTargetId.length; ri++){
      var tmpObj = _wod.getElementById(arrReturnTargetId[ri]);
      if(ri==0 && tmpObj.readOnly){
        alert("呼び出し元画面が閉じられた為、値をセットできません。");
        return false;
      }
      if(tmpObj.value == undefined){
        tmpObj.innerHTML = arrReturnValue[ri];
      }else{
        tmpObj.value = arrReturnValue[ri];
      }
    }
    
    //親ウィンドウの入力値項目(配列の先頭)の背景色をクリア
    //_wod.getElementById(arrReturnTargetId[0]).style.backgroundColor = "";
    
    //値セット後に親画面のボタンを押下
    if(afterEventBtnId != undefined && afterEventBtnId != "" && _wod.getElementById(afterEventBtnId) != undefined){
      _wod.getElementById(afterEventBtnId).fireEvent("onclick");
    }
    
    //親ウィンドウへフォーカス
    window.opener.focus();
    
    //自身を閉じる
    window.close();
}

/*#####################################################################
タイプ別入力チェック
#####################################################################*/
/**
 * 金額チェック3桁区切り
 * @param obj:処理対象オブジェクト
 * @return
 */
function checkKingaku(obj){
  obj.value = obj.value.Trim();
  if(obj.value == "")return;
  var ret = addFigure(obj.value);
  if(!ret){
    event.returnValue = false;
    obj.select();
    alert("数値以外が入力されています。");
    return false;
  }
  obj.value = ret;
  return true;
}
/**
 * 桁区切り除去
 * @param obj:処理対象オブジェクト
 * @return
 */
function removeFig(obj){
  obj.value = obj.value.Trim();
  if(obj.value == "")return;
  obj.value = removeFigure(obj.value);
  obj.select();
}
/**
 * 数値チェック3桁区切り有り
 * @param obj:処理対象オブジェクト
 * @return
 */
function checkNumberAddFig(obj){
  obj.value = obj.value.Trim();
  if(obj.value == "")return;
  return checkKingaku(obj);
}
/**
 * 数値チェック3桁区切り無し
 * @param obj:処理対象オブジェクト
 * @return
 */
function checkNumber(obj){
  //数値以外の場合、アラートを表示
  obj.value = obj.value.Trim();
  if(obj.value == "")return;
  if(isNaN(obj.value)){
    event.returnValue = false;
    obj.select();
    alert("数値以外が入力されています。");
    return false;
  }
  return true;
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

//指定バイト数以内チェック
function chkSiteinaiByte(strInputString,intByte){
	var intLengh;
    intLengh=strInputString.replace(/[^\x00-\xff]/g,"nn").length;
    if(intLengh > intByte){
    	return false; 
    }
    else{
    	return true; 
	}
}
 
//半角数字チェック			
function chkHankakuSuuji(strInputString){
	if(strInputString!=""){
	    if(strInputString.match(/[^0-9]/)!=null){
	    	return false; 
	    }
    }
	return true;
}

//年月日妥当性チェック
function chkDate(strYmd)
{	
    if(!chkHankakuSuuji(strYmd.replace(/\//g, '')))
    {
        return false;
    }

	if(strYmd.split('/').length != 3)
	{
	    return false;
	}
    var years  = strYmd.split('/')[0];
    var months = strYmd.split('/')[1];
    var days   = strYmd.split('/')[2];	
	
	if (parseInt(years) < 1900){
		return false;
	}
	if(months.length == 1)
	{
	    months = '0' + months;
	}
	if(months.length != 2)
	{
		return false;
	}
	if ((months < 1) || (months > 12)) {
		return false;
	}
	if ((months=="01") || (months=="03") || (months=="05") || (months=="07") || (months=="08") || (months=="10") || (months=="12")) {
		if ((days < 1) || (days > 31) ) {
			return false;
		}
	}
	if ((months=="04") || (months=="06") || (months=="09") || (months == "11")) {
		if ((days < 1) || (days > 30)) {
			return false;
		}
	}
	if (months=="02") {
 		if (((years % 4)==0 && (years % 100 ) != 0) || (years % 400 ) == 0 ){
			if ((days < 1) || (days > 29)) {
				return false;
			}
		} else {
			if ((days < 1) || (days > 28)) {
				return false;
			}
		}
	}
	return true;

}

function checkDate(obj){
  var checkFlg = true;
  obj.value = obj.value.Trim();
  var val = obj.value;
 
 
 val = SetDateNoSign(val,"/");
 val = SetDateNoSign(val,"-");
 
 
 
  
 
  if(val == "")return;  //空の場合、終了
  
  val = removeSlash(val); //スラッシュ除去
  val = val.replace(/\-/g, "");  //ハイフンを削除
 
  if(val.length == 6){  //6桁の場合
    if(val.substring(0, 2) > 70){
      val = "19" + val;
    }else{
      val = "20" + val;
    }
  }else if(val.length == 4){  //4桁の場合
    dd = new Date();
    year = dd.getFullYear();
    val = year + val;
  }
  
  if(val.length != 8){
    checkFlg = false; //この時点で8桁で無ければNG
  }else{  //8桁の場合
    val = addSlash(val);  //スラッシュ付与
    var arrD = val.split("/");
    if(checkDateVali(arrD[0],arrD[1],arrD[2]) == false){
      checkFlg = false; //日付妥当性チェックNG
    }
  }
  
  if(!checkFlg){
    event.returnValue = false;
    obj.select();
    alert("日付以外が入力されています。");
    return false;
  }else{
    obj.value = val;
    return true;
  }
}
/*#####################################################################
prototype定義
#####################################################################*/

/**
 * String.Trim
 * 文字列のトリム処理
 * @return
 */
String.prototype.Trim = function(){return this.replace(/^\s+|\s+$/g, "");}


/*#####################################################################
共通関数
#####################################################################*/

/**
 * カンマを付与
 * @param val:付与対象値
 * @return num
 */
function addFigure(val) {
  var num = removeFigure(val);  //カンマを削除
  if(isNaN(num))return false; //数値以外の場合、falseを戻して終了
  
  // 適切な数値に変換する 000や-00 -> 0
  num = Number(num)
  num = String(num)
  
  while(num != (num = num.replace(/^(-?\d+)(\d{3})/, "$1,$2"))); //3桁区切りでカンマ付与
  return num;
}

/**
 * カンマ除去
 * @param val:除去対象値
 * @return num:除去後値
 */
function removeFigure(val) {
  return new String(val).replace(/,/g, "");  //カンマを削除
}

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

/**
 * 区分セレクトボックス＆チェックボックスの状態をチェックし、選択されている区分をまとめる
 * @return
 */
function setKubunVal(strDdlID1,strDdlID2,strDdlID3,strDdlIDAll){

    var objKubun1 = document.getElementById(strDdlID1)
    var objKubun2 = document.getElementById(strDdlID2)
    var objKubun3 = document.getElementById(strDdlID3)
    var objKubunVal = document.getElementById(strDdlIDAll)

    objKubunVal.value = ""; //初期化
    
    if(objKubunVal.checked == true){
        objKubun1.selectedIndex = 0;
        objKubun2.selectedIndex = 0;
        objKubun3.selectedIndex = 0;
        objKubun1.disabled = true;
        objKubun2.disabled = true;
        objKubun3.disabled = true;
        return;
    }else{
        objKubun1.disabled = false;
        objKubun2.disabled = false;
        objKubun3.disabled = false;
        
        if(objKubun1.value != ""){
            objKubunVal.value = objKubun1.value;
        }
        if(objKubun2.value != ""){
            if(objKubunVal.value == ""){
                objKubunVal.value = objKubun2.value;
            }else{
                objKubunVal.value += "," + objKubun2.value;
            }
        }
        if(objKubun3.value != ""){
            if(objKubunVal.value == ""){
                objKubunVal.value = objKubun3.value;
            }else{
                objKubunVal.value += "," + objKubun3.value;
            }
        }
    }
}

/**
 * ボタンの繰り返しクリックを防止する
 * @return
 */
function disableAfterTimeout(){
    for (var i = 0;i < window.document.forms.length;i++) {
        c_form = window.document.forms[i];
        for (var j = 0;j < c_form.elements.length;j++) {
            c_form.elements[j].disabled = true;
        }
    }
}
function disableButton(){
    window.setTimeout('disableAfterTimeout()',0);
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
 * 物件指定用モーダルダイアログウィンドウを表示
 * @param siteURL:処理対象オブジェクト
 * @param callType:画面起動モード（業務を指定）
 * @param windowType:別ウィンドウで開くか否か(true:自Window false:新Window)
 * @return
 */
function callModalBukken(popupURL,targetURL,callType,windowType,kbn,no){
  var arrArg = new Array();
  var dialogWidth = "320px";
  var dialogHeight = "220px";
  if(windowType){
    if(window.name == "") window.name = Math.random();
    arrArg["window"] = window.name;
  }else{
    arrArg["window"] = Math.random();
  }
  arrArg["url"] = targetURL;
  arrArg["type"] = callType;
  arrArg["windowType"] = windowType;
  arrArg["kbn"] = kbn;
  arrArg["no"] = no;
  
  if(callType == "tenbetu" || callType == "hansoku" || callType == "tenbetuA" || callType == "hansokuA"){
    dialogWidth = "360px";
    dialogHeight = "400px";
  }
  
  var rtnArg = window.showModalDialog(popupURL + '?newwindow=true&kbn='+kbn+'&no='+no+'&type='+callType,arrArg,'dialogWidth:'+dialogWidth+'; dialogHeight:'+dialogHeight+'; status:no; help:no; edge:raised; unadorned:no');
  return rtnArg;
}
/**
 * 店指定用モーダルダイアログウィンドウを表示
 * @param siteURL:処理対象オブジェクト
 * @param callType:画面起動モード（業務を指定）
 * @param windowType:別ウィンドウで開くか否か(true:自Window false:新Window)
 * @return
 */
function callModalMise(popupURL,targetURL,callType,windowType,kbn,tenmd,isfc){
  var arrArg = new Array();
  var dialogWidth = "340px";
  var dialogHeight = "400px";
  if(windowType){
    if(window.name == "") window.name = Math.random();
    arrArg["window"] = window.name;
  }else{
    arrArg["window"] = Math.random();
  }
  arrArg["url"] = targetURL;
  arrArg["type"] = callType;
  arrArg["windowType"] = windowType;
  arrArg["kbn"] = kbn;
  arrArg["tenmd"] = tenmd;
  arrArg["isfc"] = isfc;
  
  var rtnArg = window.showModalDialog(popupURL + '?newwindow=true&kbn='+kbn+'&tenmd='+tenmd+'&isfc='+isfc+'&type='+callType,arrArg,'dialogWidth:'+dialogWidth+'; dialogHeight:'+dialogHeight+'; status:no; help:no; edge:raised; unadorned:no');
  return rtnArg;
}

/**
 * a->A
 */

function fncToUpper(e){
var strHan = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
var strZen = "abcdefghijklmnopqrstuvwxyz";
var i;
var str1;
var intIdx;
var strNew ="";
var strOld;
strOld = e.value;
for (i = 0; i < strOld.length; i++) {
    str1 = strOld.charAt(i).toUpperCase();
    intIdx = strZen.indexOf(str1,0);
    if (intIdx >= 0) {
        str1 = strHan.charAt(intIdx);
    }
    strNew += str1;
}
e.value = strNew;
}

/*
半角小文字に変換する。
*/
function fncTokomozi(frm) {
var strHan = 'ｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜｦﾝｧｨｩｪｫｯｬｭｮｰ0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZqwertyuiopasdfghjklzxcvbnmｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜｦﾝｧｨｩｪｫｯｬｭｮ~!@#$%^&*()_+`-={}|:<>?[];,./ ';
var strZen = 'あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめもやゆよらりるれろわをんぁぃぅぇぉっゃゅょー０１２３４５６７８９ＡＢＣＤＥＦＧＨＩＪＫＬＭＮＯＰＱＲＳＴＵＶＷＸＹＺｑｗｅｒｔｙｕｉｏｐａｓｄｆｇｈｊｋｌｚｘｃｖｂｎｍアイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミムメモヤユヨラリルレロワヲンァィゥェォッャュョ～！＠＃＄％＾＆＊（）＿＋‘－＝｛｝｜：＜＞？「」；、。・　';
var i;
var str1;
var intIdx;
var strNew = '';
var strin='';
strin=frm.value
strin=strin.replace(/ガ/g, 'ｶﾞ');

strin=strin.replace(/ギ/g, 'ｷﾞ');
strin=strin.replace(/グ/g, 'ｸﾞ');
strin=strin.replace(/ゲ/g, 'ｹﾞ');
strin=strin.replace(/ゴ/g, 'ｺﾞ');

strin=strin.replace(/ザ/g, 'ｻﾞ');
strin=strin.replace(/ジ/g, 'ｼﾞ');
strin=strin.replace(/ズ/g, 'ｽﾞ');
strin=strin.replace(/ゼ/g, 'ｾﾞ');
strin=strin.replace(/ゾ/g, 'ｿﾞ');

strin=strin.replace(/ダ/g, 'ﾀﾞ');
strin=strin.replace(/ヅ/g, 'ﾂﾞ');
strin=strin.replace(/デ/g, 'ﾃﾞ');
strin=strin.replace(/ド/g, 'ﾄﾞ');

strin=strin.replace(/バ/g, 'ﾊﾞ');
strin=strin.replace(/ビ/g, 'ﾋﾞ');
strin=strin.replace(/ブ/g, 'ﾌﾞ');
strin=strin.replace(/ベ/g, 'ﾍﾞ');
strin=strin.replace(/ボ/g, 'ﾎﾞ');

strin=strin.replace(/パ/g, 'ﾊﾟ');
strin=strin.replace(/ピ/g, 'ﾋﾟ');
strin=strin.replace(/プ/g, 'ﾌﾟ');
strin=strin.replace(/ペ/g, 'ﾍﾟ');
strin=strin.replace(/ポ/g, 'ﾎﾟ');


strin=strin.replace(/が/g, 'ｶﾞ');

strin=strin.replace(/ぎ/g, 'ｷﾞ');
strin=strin.replace(/ぐ/g, 'ｸﾞ');
strin=strin.replace(/げ/g, 'ｹﾞ');
strin=strin.replace(/ご/g, 'ｺﾞ');

strin=strin.replace(/ざ/g, 'ｻﾞ');
strin=strin.replace(/じ/g, 'ｼﾞ');
strin=strin.replace(/ず/g, 'ｽﾞ');
strin=strin.replace(/ぜ/g, 'ｾﾞ');
strin=strin.replace(/ぞ/g, 'ｿﾞ');

strin=strin.replace(/だ/g, 'ﾀﾞ');
strin=strin.replace(/づ/g, 'ﾂﾞ');
strin=strin.replace(/で/g, 'ﾃﾞ');
strin=strin.replace(/ど/g, 'ﾄﾞ');

strin=strin.replace(/ば/g, 'ﾊﾞ');
strin=strin.replace(/び/g, 'ﾋﾞ');
strin=strin.replace(/ぶ/g, 'ﾌﾞ');
strin=strin.replace(/べ/g, 'ﾍﾞ');
strin=strin.replace(/ぼ/g, 'ﾎﾞ');

strin=strin.replace(/ぱ/g, 'ﾊﾟ');
strin=strin.replace(/ぴ/g, 'ﾋﾟ');
strin=strin.replace(/ぷ/g, 'ﾌﾟ');
strin=strin.replace(/ぺ/g, 'ﾍﾟ');
strin=strin.replace(/ぽ/g, 'ﾎﾟ');

for (i = 0; i < strin.length; i++) {
str1 = strin.charAt(i);
intIdx = strZen.indexOf(str1,0);
if (intIdx >= 0) {
    str1 = strHan.charAt(intIdx);
}
strNew += str1;
}
strNew=strNew.replace(/(^\s*)|(\s*$)/g, '');
frm.value= strNew;
frm.value=frm.value.replace(/￥/g, '\\');
}

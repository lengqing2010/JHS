/**
 * グローバル変数宣言
 */
var _d = null; // document格納用
var _form1 = null; // メインform格納用
var _tempValForOnBlur = ""; // onBlurイベント時に値が変わっていないかをチェックするためのテンポラリ
var objSrchWin = null; // 検索ウィンドウオブジェクト
var IE='\v'=='v';
var flgEaster = null;

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
  var tmpObj = null;
  for ( var i = 0; i < listInput.length; i++) {
    tmpObj = listInput[i];
    if (tmpObj.type == "button") {
      switch (tmpObj.id) {
      case "btnCloseWin": // 「閉じる」ボタン
        tmpObj.onclick = function() {
          window.close();
        };
        break;
      }
    } else if (tmpObj.type == "reset") {
      // 「クリア」ボタン
      tmpObj.onclick = function() {
        allClear(false);
      };
    } else if (tmpObj.type == "text") {
      if(tmpObj.className == "date"){
        if(tmpObj.onkeydown == null){
          tmpObj.onkeydown = function() {
            //ctrl+[;]キー
            if(event.keyCode==187 && event.ctrlKey==true && !this.readOnly)this.value = getToday();
          };
        }
      }
    }
  }
  
  // グリッドテーブルの初期表示設定
  initGridTable();
  
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

/*#####################################################################
ホイール操作制御
#####################################################################*/
/**
 * ホイール操作制御
 * event：発生イベント
 * objDiv：制御先オブジェクト(Div)
 */
function wheel(event,objDiv){
   var delta = 0;
   if(!event)
       event = window.event;
   if (event.wheelDelta){
       delta = event.wheelDelta/120;
       if (window.opera)
           delta = -delta;
   } else if(event.detail){
       delta = -event.detail/3;
   }
   if (delta)
       handle(delta,objDiv);
}

/**
 * ホイール操作ハンドラ
 * event：発生イベント
 * objDiv：制御先オブジェクト(Div)
 */
function handle(delta,objDiv){
   var divVscroll=objDiv;
   if (delta < 0){
      divVscroll.scrollTop = divVscroll.scrollTop + 15;
   }else{
      divVscroll.scrollTop = divVscroll.scrollTop - 15;
   }
}

// /////////////////////////////////////////////////////////////////////////////////////
// 画面表示設定系
// /////////////////////////////////////////////////////////////////////////////////////

/**
 * 画面IDによって、メニューバーの背景色を変更する
 * 
 * @return
 */
function setMenuBgColor() {
  var objGamenId = objEBI("ctl00_CPH1_gamenId");
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
  case "syuusei_keiri": // 「修正・経理」画面
    menuId = "menu_lnk_syuusei_keiri";
    break;
  case "kameiten_bukken_syoukai": // 「加盟店・物件照会」画面
    menuId = "menu_lnk_kameiten_bukken_syoukai";
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
 * エレメントを一括表示する
 * 
 * @param strTargetId:表示切替対象エレメントのID郡(カンマ区切り)
 * @param strTmpDispSettingId:display状態を保持するオブジェクトのID
 * @return
 */
function openDisplay(strTargetIds,strTmpDispSettingId) {

  var arrDisId = new Array();
  if(strTargetIds != undefined){
    arrDisId = strTargetIds.split(",");
    
    for(var i=0;i<arrDisId.length; i++){
      if(objEBI(arrDisId[i])){
        objEBI(arrDisId[i]).style.display = "inline";
      }
    }
    
    if(strTmpDispSettingId != undefined && strTmpDispSettingId != ""){
      objEBI(strTmpDispSettingId).style.display = "none";
    }
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

/**
 * 画面操作を行えないように、画面全体にElementを被せる。
 * 同時に、全ての項目に「tabIndex=-1」をセットする。
 * 
 * @param obj:実行元オブジェクト
 * @param objOverTheOverlay:オーバーレイ上に表示するオブジェクト(解除ボタン等)
 * @param selectHiddenFlg:セレクトボックスを文字列化する方法を選択
 * @return
 */
function setWindowOverlay(obj,objOverTheOverlay,selectHiddenFlg){
  
  obj.blur(); //実行元オブジェクトからフォーカスを外す
  
  //全ての項目のtabIndexに-1をセットする。
  var arrInput = _d.getElementsByTagName("input");
  var arrSelect = _d.getElementsByTagName("select");
  var arrTextArea = _d.getElementsByTagName("textarea");
  var arrAnch = _d.getElementsByTagName("a");
  var arrTd = _d.getElementsByTagName("td");
  
  //selectタグ用処理オブジェクト郡
  var objSel = null;
  var objSpan = null;

  for(var i=0;i<arrInput.length; i++){  //inputタグ
    arrInput[i].tabIndex = "-1";
  }
  for(var i=0;i<arrSelect.length; i++){ //selectタグ
    //文字列に置き換え
    objSel = arrSelect[i];
    if(objSel.style.display != "none" && objSel.style.visibility != "hidden"){
      objSpan = _d.createElement("span");
      if(objSel.selectedIndex > -1){
        objSpan.innerText = " 【 " + objSel.options(objSel.selectedIndex).text + " 】 ";
      }else{
        objSpan.innerText = "";
      }
      if(selectHiddenFlg == 1){
        objSel.parentNode.appendChild(objSpan);
        objSel.tabIndex = "-1";
        objSel.style.display = "none";
      }else{
        objSpan.style.position = "absolute";
        objSpan.style.left = offsetLeftDoc(objSel);
        objSpan.style.top = offsetTopDoc(objSel)+3;
        objSel.parentNode.appendChild(objSpan);
        objSel.tabIndex = "-1";
        objSel.style.visibility = "hidden";
      }
    }
  }
  for(var i=0;i<arrTextArea.length; i++){ //textareaタグ
    arrTextArea[i].tabIndex = "-1";
  }
  for(var i=0;i<arrAnch.length; i++){  //aタグ
    arrAnch[i].tabIndex = "-1";
  }
  for(var i=0;i<arrTd.length; i++){  //aタグ
    arrTd[i].tabIndex = "-1";
  }
    
  //オーバーレイエレメントの高さを決定
  var eleHeight = 0;
  if(_d.documentElement.clientHeight < _d.getElementsByTagName("body")[0].clientHeight){
    eleHeight = _d.getElementsByTagName("body")[0].clientHeight + 100;
  }else{
    eleHeight = _d.documentElement.clientHeight;
  }

  //オーバーレイ表示用のエレメントを作成し、配置する。
  var objOverlay = _d.createElement("div");
  objOverlay.style.position = "absolute";
  objOverlay.style.backgroundColor = "gray";
  objOverlay.style.filter = "Alpha(opacity=20)";
  objOverlay.style.zIndex = "20000";
  objOverlay.style.left = "0";
  objOverlay.style.top = "0";
  objOverlay.style.width = _d.documentElement.clientWidth;
  objOverlay.style.height = eleHeight;
  document.getElementsByTagName("body")[0].appendChild(objOverlay);
  
  if(objOverTheOverlay != null){
    objOverTheOverlay.style.display = "inline";
    objOverTheOverlay.style.position = "absolute";
    objOverTheOverlay.style.zIndex = "20010";
    objOverTheOverlay.style.left = (_d.documentElement.clientWidth / 2) - (objOverTheOverlay.clientWidth / 2);
    objOverTheOverlay.style.top = (_d.documentElement.clientHeight / 2) - (objOverTheOverlay.clientHeight / 2);
  }
  
  return true;
}

//変更前コントロールのStyle(display状態)を退避して、該当コントロール(Hidden)に保持する
function SetDisplayStyle(strTaihiID, strTargetID){
   document.getElementById(strTaihiID).value = document.getElementById(strTargetID).style.display;
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
        arrInput[i].style.color = "black";
      }
    }
    if(arrInput[i].type == "checkbox" || arrInput[i].type == "radio"){
      arrInput[i].checked = false;
    }
  }
  for(var i=0;i<arrSelect.length; i++){ //selectタグ
    arrSelect[i].selectedIndex = 0;
    arrSelect[i].style.color = "black";
  }
  for(var i=0;i<arrTextArea.length; i++){ //textareaタグ
    arrTextArea[i].value = "";
    arrTextArea[i].style.color = "black";
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
 * @param leng:クリアする際の最低入力桁数(この数値以上入力されていない場合、クリアしないように)
 * @return
 */
function setCode2Pull(obj,targetId,leng){
  if(event.keyCode != 9){  //タブキーの場合、アクションを起こさない
    var pull = objEBI(targetId);
    var objVal = obj.value;
    if(pull.value != objVal){
      pull.value = objVal;
      if((objVal != "" && pull.value == "") && objVal.length < leng)return false;
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
  //対象Tbody検索
  if(objGridTBody == null){
    var tbodies = _d.getElementsByTagName("tbody");
	for (ti=0;ti<tbodies.length;ti++) {
		var tmpTbody = tbodies[ti];
		if (tmpTbody.id && tmpTbody.id.indexOf("searchGrid") != -1) {
			objGridTBody = tmpTbody;
		}
	}
  }

  //指定されたテーブルTbodyオブジェクトが存在する場合
  if (objGridTBody != null) {
    //グリッドテーブルの各行にイベントハンドらを設定
    setGridTbodyScript(objGridTBody);
    
    //テーブルの行色設定
    setGridColor(objGridTBody);
    
    //onresizeイベントに何も登録されていない場合
    if(window.onresize == null || window.onresize == ""){
      //テーブルサイズ調整実行
      changeTableSize("dataGridContent");
      //onresizeイベントハンドラにテーブルのサイズ調整を設定
      window.onresize = function(){
        changeTableSize("dataGridContent"); 
      }
    }

  }else{
    return false;
  }
}

/**
 * グリッドテーブルの各行にイベントハンドラを設定
 * 
 * @param objGridTBody:対象のTbodyオブジェクト
 * @return
 */
function setGridTbodyScript(objGridTBody){
    if(IE){
      objGridTBody.ondblclick = function() {
        returnSelectValue(event.srcElement.parentNode);      };
      objGridTBody.onmousedown = function() {
        selectedLineColor(event.srcElement.parentNode);
      };
    }else{
      objGridTBody.ondblclick = function(event) {
        returnSelectValue(event.target.parentNode);
      };
      objGridTBody.onmousedown = function(event) {
        selectedLineColor(event.target.parentNode);
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
        if(event.shiftKey==true && event.ctrlKey==true)flgEaster = 1;
        tmpBeforSelectedTrObject.cells[0].fireEvent("ondblclick");
      }
    };
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
  var arrSakiTd = objSakiTr.cells;
  var totalWidth = 0;
  for ( var i = 0; i < arrMotoTh.length; i++) {
    arrSakiTd[i].style.width = arrMotoTh[i].style.width;
    totalWidth += Number(arrMotoTh[i].style.width.replace(/px/, '')) + 1;
  }
  return totalWidth;
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
  var arrSakiTr = objTable.tBodies[0].rows;
  for ( var tri = 0; tri < arrSakiTr.length; tri++) {
    var arrSakiTd = arrSakiTr[tri].cells;
    for ( var i = 0; i < arrSakiTd.length; i++) {
      arrSakiTd[i].style.width = arrMotoTh[i].style.width;
    }
  }
}

/**
 * tableのタイトル行の幅とデータ行の幅を揃えるver3
 * 
 * @param objMotoTr:タイトル行のtrオブジェクト(セルはth指定されている事)
 * @param objSakiTr:データ行のtrオブジェクト
 * @return
 */
function setTableWidth3(objMotoTable,objSakiTable) {
  var objMotoTr = objMotoTable.rows[0];
  var objSakiTr = objSakiTable.rows[0];

  var arrMotoTh = objMotoTr.cells;
  var arrMotoThWidth = new Array;
  var totalWidth = 0;
  for ( var i = 0; i < arrMotoTh.length; i++) {
    arrMotoThWidth[i] = arrMotoTh[i].style.width;
    totalWidth += Number(arrMotoTh[i].style.width.replace(/px/, '')) + 3;
  }

  //テーブルのStyleに「table-layout: fixed;」を設定することにより、先頭行の幅のみを設定するだけでOK
  var arrSakiTd = null;
  var arrSakiTr = objSakiTable.tBodies[0].rows;
  if(arrSakiTr.length >= 1){
    arrSakiTd = arrSakiTr[0].cells;
    for ( var i = 0; i < arrMotoThWidth.length; i++) {
      arrSakiTd[i].style.width = arrMotoThWidth[i];
    }
  }

  objMotoTable.style.width = totalWidth + "px";
  objSakiTable.style.width = totalWidth + "px";
}

/**
 * 二つのテーブルの行高さを揃える
 * 
 * @param objMotoTable:元テーブル
 * @param objSakiTable:先テーブル
 * @return
 */
function setTableHeight(objMotoTable,objSakiTable) {
  
  //行オブジェクトを配列化
  var arrMotoTr = objMotoTable.rows;
  var arrSakiTr = objSakiTable.rows;
  //clientHeightとstyle.heightの差異を埋める調整値
  var adjustHeight = -7;
  if(!IE)adjustHeight = 0.2;
  //行背景色設定用カウンタ
  var countTr = 0;

  if(arrMotoTr.length==0) return true;

  //行高さ設定用オブジェクト郡
  var objMotoTr = null;
  var objSakiTr = null;
  var numMotoTrHeight = null;
  var numSakiTrHeight = null;

  for ( var tri = 0; tri < arrMotoTr.length; tri++) {
    objMotoTr = arrMotoTr[tri];
    objSakiTr = arrSakiTr[tri];
    numMotoTrHeight = Number(objMotoTr.clientHeight);
    numSakiTrHeight = Number(objSakiTr.clientHeight);
    if(numMotoTrHeight == numSakiTrHeight){
      //元テーブルの高さと、先テーブルの高さが同じ場合、処理なし
    }else if(numMotoTrHeight > numSakiTrHeight){
      //元テーブルの高さが、先テーブルの高さより高い場合
      objSakiTr.style.height = (numMotoTrHeight + adjustHeight ) + "px";
    }else if(numMotoTrHeight < numSakiTrHeight){
      //元テーブルの高さが、先テーブルの高さより低い場合
      objMotoTr.style.height = (numSakiTrHeight + adjustHeight ) + "px";
    }
    
    //ついでに行の背景色を変更
    if(objMotoTr.style.display != "none" && objSakiTr.style.display != "none"){
      if (countTr % 2 == 0) {
        objMotoTr.style.backgroundColor = "white";
        objSakiTr.style.backgroundColor = "white";
      } else {
        objMotoTr.style.backgroundColor = "LightCyan";
        objSakiTr.style.backgroundColor = "LightCyan";
      }
      countTr++;
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
  var arrTr = objGridTBody.rows;
  var objTr = null;
  for ( var i = 0; i < arrTr.length; i++) {
    objTr = arrTr[i];
    if(objTr.style.display != "none"){
      if (countTr % 2 == 0) {
        objTr.style.backgroundColor = "white";
      } else {
        objTr.style.backgroundColor = "LightCyan";
      }
      countTr++;
    }
  }
}

/**
 * tableの選択行の背景色変更
 * 
 * @param objTarget:選択されたtdエレメントのID
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

  var selectedColor = "pink";
  var flgSync = false;
  if (objTarget.id.indexOf("DataTable_resultTr") != -1) {
    flgSync = true;
  }
  
  //前に選択されていた行を、元の背景色に戻す
  if(tmpBeforSelectedTrObject != null){
    tmpBeforSelectedTrObject.style.backgroundColor = tmpBeforSelectedTrColor;
    if(flgSync){
      var tmpOldId1 = tmpBeforSelectedTrObject.id.replace(/DataTable_resultTr2/, "DataTable_resultTr1");
      var tmpOldId2 = tmpBeforSelectedTrObject.id.replace(/DataTable_resultTr1/, "DataTable_resultTr2");
      objEBI(tmpOldId1).style.backgroundColor = tmpBeforSelectedTrColor;
      objEBI(tmpOldId2).style.backgroundColor = tmpBeforSelectedTrColor;
    }
  }
  //新しく選択された行のオブジェクトと背景色の状態を、グローバル変数に退避
  tmpBeforSelectedTrObject = objTarget;
  tmpBeforSelectedTrColor = tmpBeforSelectedTrObject.style.backgroundColor;

  //選択行の背景色を変更
  objTarget.style.backgroundColor = selectedColor;
  if(flgSync){
    var tmpNewId1 = objTarget.id.replace(/DataTable_resultTr2/, "DataTable_resultTr1");
    var tmpNewId2 = objTarget.id.replace(/DataTable_resultTr1/, "DataTable_resultTr2");
    objEBI(tmpNewId1).style.backgroundColor = selectedColor;
    objEBI(tmpNewId2).style.backgroundColor = selectedColor;
  }
}

/**
 * カーソルキーでテーブル行を移動
 * @param move:移動行数(1 or -1)
 * @return
 */
function selectedLineMove(move){
  var tmpObjTr = tmpBeforSelectedTrObject;
  var actRowIndex = tmpObjTr.sectionRowIndex;
  if(tmpObjTr && tmpObjTr.id != ""){
    tmpNextTr = tmpObjTr.parentNode.rows[actRowIndex + move];
    if(tmpNextTr){
      var objDg = objEBI("dataGridContent");
      if(objDg == null)objDg = tmpNextTr.parentNode.parentNode.parentNode;
      var objMt = objEBI("meisaiTableHeaderTr");
      moveTableScroll(objDg,objMt,tmpNextTr);
      tmpNextTr.cells[0].fireEvent("onmousedown");
    }
  }
  //以降のイベントをキャンセル
  event.returnValue = false;
}

/**
 * テーブルのスクロール位置を調整
 * 
 * @param objDg:スクロールしているオブジェクト
 * @param objMt:スクロール対象のタイトル行（調整用）
 * @param tmpNextTr:スクロール位置を決める、行オブジェクト
 * @return
 */
function moveTableScroll(objDg,objMt,tmpNextTr){
  //タイトル行オブジェクトが存在する場合、調整値として保持する
  var mtHeight = 0;
  if(objMt != null)mtHeight = objMt.clientHeight;
  
  if(tmpNextTr.offsetTop >= objDg.scrollTop + (objDg.clientHeight - tmpNextTr.clientHeight)){
    objDg.scrollTop = tmpNextTr.offsetTop - (objDg.clientHeight - tmpNextTr.clientHeight);
  }
  if(tmpNextTr.offsetTop <= (objDg.scrollTop + mtHeight)){
    objDg.scrollTop = tmpNextTr.offsetTop - mtHeight;
  }
}

/**
 * テーブルのスクロール位置を最上段にセットする
 * 
 * @return
 */
function moveScrollTop(objScrollDiv){
  if(objScrollDiv != undefined && objScrollDiv != null){
    objScrollDiv.scrollTop = 0;
  }else{
    objEBI("dataGridContent").scrollTop = 0;
  }
}

/**
 * テーブルのheight/widthをウィンドウサイズに合わせて調整
 * 
 * @param targetId:サイズ変更対象オブジェクト
 * @param minHeight:最低高さ
 * @param adjustHeight:調整高さ
 * @param adjustWidth:調整幅
 * @return
 */
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

/**
 * テーブルのheight/widthをウィンドウサイズに合わせて調整(シンクロテーブル用)
 * 
 * @param targetId:サイズ変更対象オブジェクト
 * @param minHeight:最低高さ
 * @param adjustHeight:調整高さ
 * @param adjustWidth:調整幅
 * @return
 */
function changeTableSize2(objTitle1, objTitle2, objData1, objData2, minHeight, adjustHeight, adjustWidth){

  if(objTitle1 == null || objTitle1 == undefined) return false;

  //ウィンドウのサイズを取得
  var winHeight = document.documentElement.clientHeight;
  var winWidth = document.documentElement.clientWidth;

  //スクロールバーの幅を設定
  var scrollBarSize = 16;
  
  //調整値が未指定の場合、デフォルト値をセット
  if(!adjustHeight || adjustHeight == undefined) adjustHeight = 20;
  if(!adjustWidth  || adjustWidth  == undefined) adjustWidth  = 20;
  if(!minHeight  || minHeight  == undefined) minHeight  = 50;
  
  //調整値を現在のオブジェクトの位置を考慮して設定
  adjustHeight = offsetTopDoc(objTitle1) + adjustHeight;
  adjustWidth = offsetLeftDoc(objTitle1) + adjustWidth;
  
  //高さ設定実行
  if(winHeight > adjustHeight){
    if((winHeight - adjustHeight) > minHeight){
      objData1.style.height = (winHeight - adjustHeight - scrollBarSize) + "px";
      objData2.style.height = (winHeight - adjustHeight) + "px";
    }else{
      //最低高以下に変更使用としている場合、最低高にセット
      objData1.style.height = minHeight - scrollBarSize + "px";
      objData2.style.height = minHeight + "px";
    }
  }
  //幅設定実行
  if(winWidth > adjustWidth){
    objTitle2.style.width = (winWidth - adjustWidth - scrollBarSize) + "px";
    objData2.style.width = (winWidth - adjustWidth) + "px";
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
 * @param windowName:検索画面以外のウィンドウをポップアップする際のウィンドウ名付与用(任意指定)
 * @return
 */
function callSearch(strTargetIds, actionUrl, returnTargetIds, afterEventBtnId, windowName) {
  if (!objSrchWin || objSrchWin.closed) {
    //後処理ボタンIDが指定されている場合、値をセット
    if(windowName != undefined && afterEventBtnId != ""){
      windowName = "searchWindow";
    }
    objSrchWin = window.open("about:Blank", windowName, "menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes");
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
  sform.target = windowName;
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
    var objSelRet = getChildArr(objSelectedTr.cells[0],"INPUT")[0];
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


/**
 * 物件指定用モーダルダイアログウィンドウを表示
 * @param siteURL:処理対象オブジェクト
 * @param callType:画面起動モード（業務を指定）
 * @param windowType:別ウィンドウで開くか否か(true:自Window false:新Window)
 * @return
 */
function callModalBukken(popupURL,targetURL,callType,windowType,kbn,no){
  var arrArg = new Array();
  var dialogWidth = "350px";
  var dialogHeight = "250px";
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
  var dialogWidth = "350px";
  var dialogHeight = "430px";
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
 * 商品4モーダルダイアログウィンドウを表示
 * @param popupURL:処理対象オブジェクト
 * @param kbn:区分
 * @param no:保証書NO
 * @param kameitencd:加盟店コード
 * @return
 */
function callModalSyouhin4(popupURL
                           ,strKbnId
                           ,strNoId
                           ,strKameiCdId
                           ,strTysKaisyaCdId){
                           
  var dialogWidth = "1024px";
  var dialogHeight = "768px";
  var now = new Date();
  
  var rtnArg = window.showModalDialog(popupURL + '?kbn=' + objEBI(strKbnId).value
                                                 + '&no=' + objEBI(strNoId).value
                                                 + '&kameitencd=' + objEBI(strKameiCdId).value
                                                 + '&TysKaisyaCd=' + objEBI(strTysKaisyaCdId).value
                                                 + '&KEYID=' + Math.random()
                                                 , window ,'dialogWidth:'+dialogWidth+'; dialogHeight:'+dialogHeight+'; status:no; help:no; edge:raised; unadorned:no');
  return rtnArg;
}

/**
 * 特別対応モーダルダイアログウィンドウを表示
 * @param popupURL:処理対象オブジェクト
 * @param strKbnId:区分
 * @param strNoId:保証書NO
 * @param strKameiCdId:加盟店コード
 * @param strTysHouhouNoId:調査方法NO
 * @param strTysSyouhinCdId:商品コード
 * @return
 */
function callModalTokubetuTaiou(popupURL
                           ,strKbnId
                           ,strNoId
                           ,strKameiCdId
                           ,strTysHouhouNoId
                           ,strTysSyouhinCdId
                           ){
                           
  var dialogWidth = "960px";
  var dialogHeight = "680px";
  var now = new Date();
  
  var rtnArg = null;
  var varUrl = null;
  var varOptions = 'dialogWidth:'+dialogWidth+'; dialogHeight:'+dialogHeight+'; status:no; help:no; edge:raised; unadorned:no';
  
  //区分・番号
  varUrl = popupURL + '?kbn=' + objEBI(strKbnId).value
                    + '&no=' + objEBI(strNoId).value
                                     
  //加盟店コード・商品コード・調査方法NO
  if (strKameiCdId != "" && strTysHouhouNoId != "" && strTysSyouhinCdId != ""){  
        varUrl += '&kameitencd=' + objEBI(strKameiCdId).value
                    + '&TysHouhouNo=' + objEBI(strTysHouhouNoId).value
                    + '&SyouhinCd=' + objEBI(strTysSyouhinCdId).value
  }

  varUrl += '&KEYID=' + Math.random();

  var rtnArg = window.showModalDialog(varUrl, window ,varOptions);
  
  return rtnArg;
}

/**
 * 特別対応モーダルダイアログウィンドウを表示
 * @param popupURL:処理対象オブジェクト
 * @param strKbnId:区分
 * @param strNoId:保証書NO
 * @param strKameiCdId:加盟店コード
 * @param strTysHouhouNoId:調査方法NO
 * @param strTysSyouhinCdId:商品コード
 * @param strArrSyouhinCdId:商品123情報(商品コード)
 * @param strArrKeijouFlg:商品123情報(計上FLG)
 * @param strArrHattyuuKingaku:商品123情報(発注書金額)
 * @param strArrDisplayCd:商品123情報(特別対応ツールチップ_Displayコード)
 * @param strChgTokuCd:商品123情報(特別対応更新対象コード) 
 * @param emType:画面モード
 * @param strKkkHaneiFlg:特別対応価格反映用フラグ
 * @param strRentouBukkenSuu:連棟物件数
 * @param strHdnKakuteiValue:特別対応文字色比較用
 * @param strBtnTokubetu:特別対応ボタン
 * @param strBtnReloadId :再描画ボタン押下用
 * @return
 */
function callModalTokubetuTaiouJT(popupURL
                           ,strKbnId
                           ,strNoId
                           ,strKameiCdId
                           ,strTysHouhouNoId
                           ,strTysSyouhinCdId
                           ,strArrSyouhinCdId
                           ,strArrKeijouFlg
                           ,strArrHattyuuKingaku
                           ,strArrDisplayCd
                           ,strChgTokuCd
                           ,emType
                           ,strKkkHaneiFlg
                           ,strRentouBukkenSuu
                           ,strHdnKakuteiValue
                           ,strBtnTokubetu
                           ,strBtnReloadId
                           ){
                           
  var dialogWidth = "960px";
  var dialogHeight = "680px";
  var now = new Date();
  
  var retVal  = null;
  var varUrl = null;
  var varOptions = 'dialogWidth:'+dialogWidth+'; dialogHeight:'+dialogHeight+'; status:no; help:no; edge:raised; unadorned:no';
  
  //区分・番号
  varUrl = popupURL + '?kbn=' + objEBI(strKbnId).value
                    + '&no=' + objEBI(strNoId).value
                                     
  //加盟店コード・商品コード・調査方法NO
  if (strKameiCdId != "" && strTysHouhouNoId != "" && strTysSyouhinCdId != ""){  
        varUrl += '&kameitencd=' + objEBI(strKameiCdId).value
                    + '&TysHouhouNo=' + objEBI(strTysHouhouNoId).value
                    + '&SyouhinCd=' + objEBI(strTysSyouhinCdId).value
  }
  //商品123情報
  if (strArrSyouhinCdId != ""){
        varUrl += '&ArrSyouhinCd=' + strArrSyouhinCdId
  }
  //計上FLG
  if (strArrKeijouFlg != ""){
        varUrl += '&ArrKeijouFlg=' + strArrKeijouFlg
  }
  //発注書金額
  if (strArrHattyuuKingaku != ""){
        varUrl += '&ArrHattyuuKingaku=' + strArrHattyuuKingaku
  }
  //Displayコード
  if (strArrDisplayCd != ""){
        varUrl += '&ArrDisplayCd=' + strArrDisplayCd
  }
  //更新対象コード
  if (strChgTokuCd != ""){
        varUrl += '&ChgTokuCd=' + objEBI(strChgTokuCd).value
  }  
  //画面モード
  if (emType != ""){
        varUrl += '&GamenMode=' + emType
  }
  //特別対応価格反映用フラグ
  if (strKkkHaneiFlg != ""){
        varUrl += '&TokutaiKkkHaneiFlg=' + objEBI(strKkkHaneiFlg).value
  }
  //連棟物件数
  if (strRentouBukkenSuu != ""){
        varUrl += '&RentouBukkenSuu=' + objEBI(strRentouBukkenSuu).value
  }
 
  varUrl += '&KEYID=' + Math.random();
 
  var retVal = window.showModalDialog(varUrl, window ,varOptions);
    
    if (retVal == 9){
   
        //特別対応文字色比較用
        objEBI(strHdnKakuteiValue).value = objEBI(strKameiCdId).value + sepStr
                                         + objEBI(strTysSyouhinCdId).value + sepStr
                                         + objEBI(strTysHouhouNoId).value + sepStr;
    
        //スタイルを黒字・普通に
        objEBI(strBtnTokubetu).style.backgroundColor = "";
        objEBI(strBtnTokubetu).style.fontWeight = "normal";
        
        //受注画面の場合(IDの指定がある場合)には、再描画ボタンを押下
        if(strBtnReloadId.value != ""){
            objEBI(strBtnReloadId).click();
        }
           
    }
  
  return retVal;
}

/**
 * 請求先・仕入先変更モーダルダイアログウィンドウを表示
 * @param popupURL          :処理対象オブジェクト
 * @param SeikyuuSakiKbn    :請求先区分[邸別請求テーブル用]
 * @param SeikyuuSakiCd     :請求先コード[邸別請求テーブル用]
 * @param SeikyuuSakiBrc    :請求先枝番[邸別請求テーブル用]
 * @param SiireSakiCd       :仕入先コード[邸別請求テーブル用]
 * @param SiireSakiBrc      :仕入先枝番[邸別請求テーブル用]
 * @param kameitenCd        :加盟店コード
 * @param KaisyaCd          :会社コード(調査会社)[会社コード + 事業所コード]
 * @param SyouhinCd         :商品コード
 * @param KojKaisyaSeikyuu  :工事会社請求
 * @param KojKaisyaCd       :工事会社コード[会社コード + 事業所コード]
 * @param KeijouFlg         :売上計上済フラグ
 * @param ViewMode          :表示モード
 * @return
 */
function callModalSeikyuuSiireSakiHenkou(popupURL
                                        , SeikyuuSakiCd
                                        , SeikyuuSakiBrc
                                        , SeikyuuSakiKbn
                                        , SiireSakiCd
                                        , SiireSakiBrc
                                        , kameitenCd
                                        , KaisyaCd
                                        , SyouhinCd
                                        , KojKaisyaSeikyuu
                                        , KojKaisyaCd
                                        , KeijouFlg
                                        , ViewMode){
  var dialogWidth = "780px";
  var dialogHeight = "350px";

  var rtnArg = window.showModalDialog(popupURL + '?SeikyuuSakiCd=' + SeikyuuSakiCd
                                                + '&SeikyuuSakiBrc=' + SeikyuuSakiBrc
                                                + '&SeikyuuSakiKbn=' + SeikyuuSakiKbn
                                                + '&SiireSakiCd=' + SiireSakiCd
                                                + '&SiireSakiBrc=' + SiireSakiBrc
                                                + '&KameitenCd=' + kameitenCd
                                                + '&KaisyaCd=' + KaisyaCd
                                                + '&KojKaisyaSeikyuu=' + KojKaisyaSeikyuu
                                                + '&KojKaisyaCd=' + KojKaisyaCd
                                                + '&SyouhinCd=' + SyouhinCd
                                                + '&KeijouFlg=' + KeijouFlg
                                                + '&ViewMode=' + ViewMode, '', 'dialogWidth:'+dialogWidth+'; dialogHeight:'+dialogHeight+'; status:no; help:no; edge:raised; unadorned:no');
  return rtnArg;
}

/** 請求先・仕入先画面呼出処理
 * @param strUrl                    :請求先・仕入先画面URL
 * @param strSkCdId                 :請求先コード           (格納コントロールID)
 * @param strSkBrcId                :請求先枝番             (格納コントロールID)
 * @param strSkKbnId                :請求先区分             (格納コントロールID)
 * @param strTyCdId                 :調査会社コード         (格納コントロールID)
 * @param strTyjCdId                :調査会社事業所コード   (格納コントロールID)
 * @param strKameiCdId              :加盟店コード           (格納コントロールID)
 * @param strKaisyaCdId             :会社コード             (格納コントロールID)
 * @param strItemCdId               :商品コード             (格納コントロールID)
 * @param striKojKaisyaSeikyuuId    :工事会社コード         (格納コントロールID)
 * @param striKojKaisyaCdId         :工事会社コード         (格納コントロールID)
 * @param strFlgKeijoId             :売上処理済フラグ       (格納コントロールID)
 * @param strViewModeId             :表示モード             (格納コントロールID)
 * @param strUpdPnlCtrlId           :アップデートパネルID
 * @param strChkSeikyuuSakiChg      :請求先変更チェック (格納コントロールID)
 */
function callSeikyuuSiireSakiModal(strUrl
                                , strSkCdId
                                , strSkBrcId
                                , strSkKbnId
                                , strTyCdId
                                , strTyjCdId
                                , strKameiCdId
                                , strKaisyaCdId
                                , strItemCdId
                                , striKojKaisyaSeikyuuId
                                , striKojKaisyaCdId
                                , strFlgKeijoId
                                , strViewModeId
                                , strUpdPnlCtrlId
                                , strChkSeikyuuSakiChg){
    var rtnArg = new Array();
    rtnArg = callModalSeikyuuSiireSakiHenkou(strUrl
                                            , objEBI(strSkCdId).value
                                            , objEBI(strSkBrcId).value
                                            , objEBI(strSkKbnId).value
                                            , objEBI(strTyCdId).value
                                            , objEBI(strTyjCdId).value
                                            , objEBI(strKameiCdId).value
                                            , objEBI(strKaisyaCdId).value
                                            , objEBI(strItemCdId).value
                                            , objEBI(striKojKaisyaSeikyuuId).value
                                            , objEBI(striKojKaisyaCdId).value
                                            , objEBI(strFlgKeijoId).value
                                            , objEBI(strViewModeId).value);
    if (rtnArg != undefined){
        if (objEBI(strSkCdId).value != rtnArg["SeikyuuSakiCd"] ||
            objEBI(strSkBrcId).value != rtnArg["SeikyuuSakiBrc"] ||
            objEBI(strSkKbnId).value != rtnArg["SeikyuuSakiKbn"]){
                objEBI(strChkSeikyuuSakiChg).value = "1";
        }else{
                objEBI(strChkSeikyuuSakiChg).value = "0";
        }
    
        objEBI(strSkCdId).value = rtnArg["SeikyuuSakiCd"];
        objEBI(strSkBrcId).value = rtnArg["SeikyuuSakiBrc"];
        objEBI(strSkKbnId).value = rtnArg["SeikyuuSakiKbn"];
        objEBI(strTyCdId).value = rtnArg["SiireSakiCd"];
        objEBI(strTyjCdId).value = rtnArg["SiireSakiBrc"];
        
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm._doPostBack(strUpdPnlCtrlId, '');
    }
    return true;
}

/*#####################################################################
タイプ別入力チェック
#####################################################################*/
/**
 * 金額チェック3桁区切り
 * @param obj:処理対象オブジェクト
 * @param flgMinus:マイナス許可フラグ（True:マイナスOK、False:マイナス不可）
 * @return
 */
function checkKingaku(obj,flgMinus){
  obj.value = obj.value.Trim();
  if(obj.value == "")return true;
  var ret = addFigure(obj.value);
  if(!ret){
    event.returnValue = false;
    obj.select();
    alert("数値以外が入力されています。");
    return false;
  }
  if(flgMinus!=undefined && !flgMinus){
    if(!checkMinus(obj)){ //数値マイナスチェック
      return false;
    }
  }
  var retIndex = obj.value.indexOf(".",0);
  if(!(retIndex == -1)){
    event.returnValue = false;
    obj.select();
    alert("小数値が入力されています。");
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
  if(obj.value == "")return true;
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
  if(obj.value == "")return true;
  return checkKingaku(obj);
}
/**
 * 少数値チェック3桁区切り有り
 * @param obj:処理対象オブジェクト
 * @return
 */
function checkFewNumberAddFig(obj,flgMinus){
  obj.value = obj.value.Trim();
  if(obj.value == "")return true;
  var ret = addFigure(obj.value);
  if(!ret){
    event.returnValue = false;
    obj.select();
    alert("数値以外が入力されています。");
    return false;
  }
  if(flgMinus!=undefined && !flgMinus){
    if(!checkMinus(obj)){ //数値マイナスチェック
      return false;
    }
  }
  obj.value = ret;
  return true; 
}
/**
 * 数値チェック3桁区切り無し
 * @param obj:処理対象オブジェクト
 * @return
 */
function checkNumber(obj){
  //数値以外の場合、アラートを表示
  obj.value = obj.value.Trim();
  if(obj.value == "")return true;
  if(isNaN(obj.value)){
    event.returnValue = false;
    obj.select();
    alert("数値以外が入力されています。");
    return false;
  }
  return true;
}
/**
 * 数値マイナス値不許可チェック
 * @param obj:処理対象オブジェクト
 * @return
 */
function checkMinus(obj){
  if(obj.value < 0){
    obj.select();
    alert("マイナス値が入力されています。この項目はマイナス値を入力することはできません。");
    return false;
  }
  return true;
}
/**
 * 金額プラス値不許可チェック
 * @param obj:処理対象オブジェクト
 * @param blnReturn:変更前値戻しフラグ(true:戻す,以外:戻さない)
 * @return
 */
function checkPlus(obj,blnReturn){
    //入力有の場合
    if(obj.value != ""){
        if(obj.value > 0){
            if(blnReturn) obj.value = _tempValForOnBlur;
            obj.select();
            alert("プラス値が入力されています。この項目はプラス値を入力することはできません。");
            return false;
        }
    }
    return true;
}
/**
 * 金額プラス値不許可チェック（商品2用）
 * @param obj:処理対象オブジェクト
 * @return
 */
function checkPlusKingaku(objSoukoCd, objKingaku){
  
    if(objSoukoCd == undefined || objKingaku == undefined)return true;
    
    //倉庫コード=115の場合
    if(objSoukoCd.value == "115"){
        //桁区切り除去
        if(objKingaku.value == "")return true;
        objKingaku.value = removeFigure(objKingaku.value);
        
        if(checkPlus(objKingaku,true) == false){
            return false;
        }
            
        //桁区切り付与
        objKingaku.value = addFigure(objKingaku.value);
    }
    return true;
}
 
/**
 * 半角英数チェック
 * @param obj:処理対象オブジェクト
 * @return
 */
 function hankakuEisuu(obj) { 
   if(obj.value == "")return true; 
   if ( !/^[a-zA-Z0-9]+$/.test(obj.value) ) { 
     obj.select();
     alert("半角英数字のみ入力して下さい。");
     return false;
   } 
   return true;
 }
 
/**
 * 日付チェック
 * @param obj:処理対象オブジェクト
 * @return
 */
function checkDate(obj){
  var checkFlg = true;
  obj.value = obj.value.Trim();
  var val = obj.value;

  if(val == "")return true;  //空の場合、終了
  
  //m/d対応
  var arrMDval = val.split("/");
  if(arrMDval.length > 1){
    val = "";
    for(ti=0;ti<arrMDval.length;ti++){
      if(arrMDval[ti].length == 1)arrMDval[ti] = "0" + arrMDval[ti];
      val += arrMDval[ti];
    }
  }

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
  }
  return true;
}

/**
 * 日付チェック(年月)
 * @param obj:処理対象オブジェクト
 * @return
 */
function checkDateYm(obj){
  var checkFlg = true;
  obj.value = obj.value.Trim();
  var val = obj.value;

  if(val == "")return true;  //空の場合、終了
  
  val = removeSlash(val); //スラッシュ除去
  val = val.replace(/\-/g, "");  //ハイフンを削除
  
  val = val + "01"

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
    }else{
      // yyyy/MMにする
      val = arrD[0] + "/" + arrD[1]
    }
  }
  
  if(!checkFlg){
    event.returnValue = false;
    obj.select();
    alert("日付以外が入力されています。");
    return false;
  }else{
    obj.value = val;
  }
  return true;
}

/**
 * 日付チェック(日)
 * @param obj:処理対象オブジェクト
 * @return
 */
function checkDateDD(obj){
  var checkFlg = true;
  obj.value = obj.value.Trim();
  var val = obj.value;
  var numChk = null;

  if(val == "")return true;  //空の場合、終了
  
  val = removeSlash(val); //スラッシュ除去
  val = val.replace(/\-/g, "");  //ハイフンを削除
  
  if(isNaN(val)){
    checkFlg = false; //日付妥当性チェックNG
  }else{
      numChk = Number(val);
      if(numChk < 1 || numChk > 31){
        checkFlg = false; //日付妥当性チェックNG
      }else{   
          if(val.length == 2){//2桁の場合
          }else if(val.length == 1){//1桁の場合
            val = paddingStr(val, 2, '0');
          }else{
            checkFlg = false; //日付妥当性チェックNG
          }
      }
  }
  
  if(!checkFlg){
    event.returnValue = false;
    obj.select();
    alert("日付以外が入力されています。");
    return false;
  }else{
    obj.value = val;
  }
  return true;
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

/**
 * String.z2hDigit
 * 全角数値を半角数値に変換処理
 * @return
 */
String.prototype.z2hDigit = function() {
  var str = '';
  var len = this.length;
  for (var i = 0; i < len; i++) {
    var c = this.charCodeAt(i);
    if (c >= 65296 && c <= 65305)
      str += String.fromCharCode(c - 65248);
    else
      str += this.charAt(i);
  }
  return str;
};

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
  return new String(val).replace(/\//g, "");  //スラッシュを削除
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
 * 本日日付取得
 * @return YYYY/MM/DD
 */
function getToday(){
  dd = new Date();
  yy = dd.getYear();
  mm = dd.getMonth() + 1;
  dd = dd.getDate();
  if(yy < 2000)yy += 1900;
  if(mm < 10)mm = "0" + mm;
  if(dd < 10)dd = "0" + dd;
  return (yy + "/" + mm + "/" + dd);
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
 * ## onChange代替処理用 ##
 *   テンポラリグローバル変数に、対象オブジェクトの値をセットする
 *   ※onfocusイベントハンドラからコール
 * @param obj:対象オブジェクト
 */
function setTempValueForOnBlur(obj){
  _tempValForOnBlur = obj.value;
}

/**
 * ## onChange代替処理用 ##
 *   テンポラリグローバル変数と、対象オブジェクトの値を比較する
 *     値がテンポラリと異なっている＝値が変更されている：True
 *     値がテンポラリと異なっていない＝値が変更されていない：False
 *   ※onblurイベントハンドラからコール
 * @param obj:対象オブジェクト
 * @return 変更されているか否か(Boolean)
 */
function checkTempValueForOnBlur(obj){
  return _tempValForOnBlur != obj.value;
}


/**
 * パディング関数
 * 指定条件により文字列をパディングする
 * @param
 *    －strTarget       ：対象文字列
 *    －intDigit        ：パディングする桁数
 *    －strPaddingChar  ：パディングする文字
 *    －intFlg          ：パディングする方向（-1で右側にパディング。それ以外は左側。）
 * @return
 *    パディングされた文字列
 */ 
function paddingStr(strTarget, intDigit, strPaddingChar, intFlg){
    var temp = '';
    strTarget = strTarget.replace(" ","");
    if(strTarget.length == 0) return temp;
    
    if (intFlg == -1){
        temp = fnPaddingRight(strTarget, intDigit, strPaddingChar);
    }else{
        temp = fnPaddingLeft(strTarget, intDigit, strPaddingChar);
    }
    return temp;
}

/**
 * 左パディング
 * 指定した文字列の左側に、文字列長が指定した桁数になるまで、指定した文字を埋める
 */
function fnPaddingLeft(strTarget, intDigit, strPaddingChar){
    var temp = "" + strTarget;
        while(temp.length < intDigit){
            temp = strPaddingChar + temp;
        }
    return temp;
}

/**
 * 右パディング
 * 指定した文字列の右側に、文字列長が指定した桁数になるまで、指定した文字を埋める
 */
function fnPaddingRight(strTarget, intDigit, strPaddingChar){
    var temp = "" + strTarget;
        while(temp.length < intDigit){
            temp = temp + strPaddingChar;
        }
    return temp;
}

/**
 * テーブルの背景色を縞状にする
 * @param
 *    － objGridTBody : 対象とするtableのtbodyエレメント
 *    － intStep      : 縞状にする行数（何行ごとに縞状にするか）
 * @return
 */
function setTableStripes(objGridTBody, intStep) {
    var countTr = 0;
    var arrTr = objGridTBody.rows;
    // 明細行の数だけループ
    for (var i = 0; i < arrTr.length; i = i + intStep) {
        var objTr = arrTr[i];
        var k = i;
        var blnChg = false;
        // ４行ずつループし、全て非表示かどうか判断
        for (var j = 0; j < intStep; j++){
            k = i + j;
            objWkTr = arrTr[k];
            if (objWkTr.style.display != "none"){
                blnChg = true;
            }
        }
        // 1行でも表示されている行があったら、背景色を変更
        if (blnChg == true){
            for (var j = 0; j < intStep; j++){
                k = i + j;
                objWkTr = arrTr[k];
                if (countTr % 2 == 0) {
                    objWkTr.className  = "odd";
                } else {
                    objWkTr.className  = "even";
                }
            }
            // 背景色を変更した時のみカウントアップ
            countTr++;
        }
    }
    return true;
}

/**
 * 固定列有りのテーブルのレイアウト等を設定する
 * @param
 *    － type           :処理種別
 *    － tableTitle1    :ヘッダ部左テーブル
 *    － tableTitle2    :ヘッダ部右テーブル
 *    － tableData1     :データ部左テーブル
 *    － tableData2     :データ部右テーブル
 *    － divTitle1      :ヘッダ部左DIV
 *    － divTitle2      :ヘッダ部右DIV
 *    － divData1       :データ部左DIV
 *    － divData2       :データ部右DIV
 *    － minHeight      :ウィンドウリサイズ時のデータ部に設定する最低高さ
 *    － adjustHeight   :調整高さ(大きい程、データ部が低くなる)
 *    － adjustWidth    :調整幅(大きい程、データ部が狭くなる)
 */
function settingResultTableForColumnFix(type
                                        , tableTitle1
                                        , tableTitle2
                                        , tableData1
                                        , tableData2
                                        , divTitle1
                                        , divTitle2
                                        , divData1
                                        , divData2
                                        , minHeight
                                        , adjustHeight
                                        , adjustWidth){
    if(type != 1){
        /*検索結果テーブル 各種レイアウト設定*/
        setTableHeight(tableTitle2,tableTitle1);
        setTableHeight(tableData2,tableData1);
        setGridTbodyScript(tableData1.tBodies[0]);
        setGridTbodyScript(tableData2.tBodies[0]);
        
        /*検索結果テーブル 表示サイズ変更*/
        changeTableSize2(divTitle1,divTitle2,divData1,divData2,minHeight,adjustHeight,adjustWidth)
        window.onresize = function(){
            changeTableSize2(divTitle1,divTitle2,divData1,divData2,minHeight,adjustHeight,adjustWidth)
        }
        
    }else if(type==1){
        //ソート後用
        setTableWidth3(tableTitle1,tableData1);
        setTableWidth3(tableTitle2,tableData2);
        setGridColor(tableData1.tBodies[0]);
        setGridColor(tableData2.tBodies[0]);
    }
}

/**
 * 末尾のセパレータ文字列を除去する処理
 * @param
 *    － strTarget  :対象文字列
 * @return
 *    セパレータ文字列を除去した文字列
 */
function RemoveSepStr(strTarget){
    if(strTarget == undefined || strTarget == null){
        return "";
    }else{   
        strTarget = strTarget.replace(/\$\$\$$/, "");    
        return strTarget;
    }
}

/**
 * 対象項目のクリア処理
 * @param
 *    － targetId   :クリア対象
 * @return
 */
function clrName(targetId){
    //対象をクリア
    objEBI(targetId).value="";

    return true;
}
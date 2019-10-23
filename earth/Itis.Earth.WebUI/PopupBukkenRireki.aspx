<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="PopupBukkenRireki.aspx.vb" Inherits="Itis.Earth.WebUI.PopupBukkenRireki"
    Title="EARTH 物件履歴" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script type="text/javascript">

        //ウィンドウサイズ変更
        try{
            window.resizeTo(1024,768);
        }catch(e){
            //アクセスが拒否されましたのエラーが出たら何もしない。
            if(e.number == 2147024891) throw e;
        }
        
        //コントロール接頭辞
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
         * onload時の追加処理
         ****************************************/
        function funcAfterOnload() {
            // コードプルダウンの値表示を切り替え
            ChgDispSelectCode();
            
            //取消行表示設定
            ChgDispTorikesi();
        }
        
        //Indexを返す
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
        
        // 物件履歴テーブル 各種レイアウト設定
        function settingTable(){
            var rirekiData = objEBI("<%=tblMeisai.clientID %>");
            setRirekiColor(rirekiData);
        }
        
        /**
         * 物件ごとに背景色を変更（実質は2行ごとに変更）
         * 
         * @param objGridTBody:対象とするtableのtbodyエレメント
         * @return
         */
        function setRirekiColor(objGridTBody) {
            var countTr = 0;
            var arrTr = objGridTBody.rows;
            // 明細行の数だけループ
            for (var i = 0; i < arrTr.length; i = i + 2) {
                var objTr = arrTr[i];
                var k = i;
                var blnChg = false;
                // ４行ずつループし、全て非表示かどうか判断
                for (var j = 0; j < 2; j++){
                    k = i + j;
                    objWkTr = arrTr[k];
                    if (objWkTr.style.display != "none"){
                        blnChg = true;
                    }
                }
                // 1行でも表示されている行があったら、背景色を変更
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
                    // 背景色を変更した時のみカウントアップ
                    countTr++;
                }
            }
            return true;
        }
        
        // コードプルダウンの値表示を切り替え
        function ChgDispSelectCode(){
            var objGridTBody = objEBI("<%=tblMeisai.clientID %>");
            var arrTr = objGridTBody.rows;
            var varLine = 0;
            var objSel = '';
            var varFlg = '';
                
            // 明細行の数だけループ
            for (var i = 0; i < arrTr.length; i = i + 2) {
                varLine++ ;

                //オブジェクトの取得
                objSelSyubetu = RetObject(gVarSelectSyubetu,varLine);
                if(objSelSyubetu == undefined) continue;
                
                //オブジェクトの取得
                objTd = RetObject(gVarTdCode,varLine);
                if(objTd == undefined) continue;
                objSel = objTd.childNodes[0];
                if(objSel == undefined) continue;
                
                //オプション全削除
                SelectOptionDelete(objSel);
            
                //オプション挿入
                SelectOptionInsert(objSel,objSelSyubetu.value);

                //分類の存在チェック
                if(ChkExitSelectCode(objSelSyubetu.value) == false){
                    var strMSG = "<%= Messages.MSG113E %>";
                    strMSG = strMSG.replace('@PARAM1','分類');
                    alert(strMSG);
                    return false;
                }
                                
                //オプションセット
                SelectOptionSet(objSel,varLine);
                
            }
        }
        
        //SelectCodeのオプションを全削除する
        function SelectOptionDelete(objSel){
	        while(objSel.lastChild){
	            objSel.removeChild(objSel.lastChild);
	        }
        }
        
        //種別に対応するコードが存在するかチェックする
        function ChkExitSelectCode(intFlg){     
            if(intFlg == '') return false;
            var varMoto = gVarOyaSettouji + gVarSelectTmpSyubetu + intFlg;
	        var objMoto = objEBI(varMoto);
            var len = objMoto.length;
            if(len <= 0) return false;
            return true;            
        }
        
        //SelectCodeのオプションを追加する
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
        
        //SelectCodeのオプションをセットする
        function SelectOptionSet(objSel,varLine){
            //Hiddenの取得およびセット
            objHdnCode = RetObject(gVarHiddenBunrui,varLine);
            objSel.value = objHdnCode.value; //Selectedを指定
            //SPANの取得およびセット
            objSpnBunrui = RetObject(gVarSpanBunrui,varLine);
            if(objHdnCode.value == ''){
                objSpnBunrui.innerHTML = '';
            }else{
                objSpnBunrui.innerHTML = objSel.options[objSel.selectedIndex].text; //選択値をセット
            }
        }
        
        //明細行の該当コントロールオブジェクトを返す
        function RetObject(varTarget,varLine){
            var varTmpId = gVarSettouJi + varLine + varTarget;
            return objEBI(varTmpId);
        }
        
        //取消行の表示切替
        //※0:非表示,1:表示
        function TorikesiDisp(varFlg){           
            var objGridTBody = objEBI("<%=tblMeisai.clientID %>");
            var arrTr = objGridTBody.rows;
            var objTr = null;
            var objSyubetu = objEBI("<%=SelectSyubetu.clientID %>");
            var objTrTorikesi = null;
            var objTrSyubetu = null;
            var blnSyubetuFlg = true; //種別表示判定フラグ
            
            for ( var i = 1; i < arrTr.length; i++) {
                objTr = arrTr[i];
                objTorikesi = null;
                objTrSyubetu = null;
                blnSyubetuFlg = true;
                
                //種別判定
                if(objSyubetu.value == ''){ //未選択
                    //TR1行目
                    objTr = RetObject(gVarTr1,i);
                    if(objTr == undefined){
                    }else{
                        objTr.style.display = "inline";
                    }
                    //TR2行目
                    objTr = RetObject(gVarTr2,i);
                    if(objTr == undefined){
                    }else{
                        objTr.style.display = "inline";
                    }                           
                    
                }else{ //選択あり               
                    //画面上部.種別 = Tr.種別を比較・判定
                    //SELECT種別                
                    objTrSyubetu = RetObject(gVarSelectSyubetu,i);
                    if(objTrSyubetu == undefined){
                    }else{
                        if(objSyubetu.value == objTrSyubetu.value){ //一致
                            //TR1行目
                            objTr = RetObject(gVarTr1,i);
                            objTr.style.display = "inline";
                            //TR2行目
                            objTr = RetObject(gVarTr2,i);
                            objTr.style.display = "inline";                           
                            
                        }else{ //不一致
                            //TR1行目
                            objTr = RetObject(gVarTr1,i);
                            objTr.style.display = "none";
                            //TR2行目
                            objTr = RetObject(gVarTr2,i);
                            objTr.style.display = "none";
                            
                            blnSyubetuFlg = false;
                        }
                    }                

                }
                //取消判定
                //SPAN取消                
                objTrTorikesi = RetObject(gVarSpanTorikesi,i);
                if(objTrTorikesi == undefined){
                }else{
                    if(objTrTorikesi.innerHTML == "取消"){
                        if(varFlg == 0){ //非表示
                            //TR1行目
                            objTr = RetObject(gVarTr1,i);
                            objTr.style.display = "none";
                            //TR2行目
                            objTr = RetObject(gVarTr2,i);
                            objTr.style.display = "none";
                        }else{ //表示
                            if(blnSyubetuFlg){
                                //TR1行目
                                objTr = RetObject(gVarTr1,i);
                                objTr.style.display = "inline";
                                //TR2行目
                                objTr = RetObject(gVarTr2,i);
                                objTr.style.display = "inline";                           
                            }else{
                                //TR1行目
                                objTr = RetObject(gVarTr1,i);
                                objTr.style.display = "none";
                                //TR2行目
                                objTr = RetObject(gVarTr2,i);
                                objTr.style.display = "none";
                            }
                            
                        } 
                    }
                }
            }
            
            // 物件履歴テーブル 各種レイアウト設定
            settingTable();
            
            //オブジェクトの取得
            var objHdnInit = objEBI("<%=HiddenTorikesi.clientID %>");
            objHdnInit.value = varFlg;
        }
                
        //取消行表示設定
        function ChgDispTorikesi(){
            var objHdnInit = objEBI("<%=HiddenTorikesi.clientID %>");
            TorikesiDisp(objHdnInit.value);
        }
        
        //SelectCodeへのフォーカス処理
        //※第一引数には、HiddenCode.idを指定する
        function GetSelectCode(objID){
            //Indexの取得
            var varLine = GetIndex(objID);
            //オブジェクトの取得
            var objTd = RetObject(gVarTdCode,varLine);
            if(objTd == undefined) return false;
            var objSel = objTd.childNodes[0];
            if(objSel == undefined) return false;
            return objSel;
        }
        
        //物件履歴詳細画面呼出処理
        function PopupSyousai(strNyuuryokuNo){    
            //オブジェクトの再読込(Ajax再描画対応)
            objSendTargetWin = objEBI("<%= sendTargetWin.clientID %>");
            if(objSendTargetWin.value == "")objSendTargetWin.value="<%=EarthConst.MAIN_WINDOW_NAME %>";
            
            //オープン対象の画面を指定
            varAction = "<%=UrlConst.POPUP_BUKKEN_RIREKI_SYUUSEI %>";
            
            //<!-- 画面引渡し情報 -->
            objSendForm = objEBI("searchForm");
            //区分+番号+入力NO
            var objSendVal_SearchTerms = objEBI("sendSearchTerms");
            var strKbn = objEBI("<%= HiddenKbn.clientID %>").value + "<%=EarthConst.SEP_STRING %>";
            var strBangou = objEBI("<%= TextBangou.clientID %>").value + "<%=EarthConst.SEP_STRING %>";
            objSendVal_SearchTerms.value = strKbn + strBangou + strNyuuryokuNo;
            
            //後処理ボタンID
            objEBI("afterEventBtnId").value = "<%= ButtonReload.clientID %>";
            
            var varWindowName = "PopupBukkenRirekiSyuusei";
            objSrchWin = window.open("about:Blank", varWindowName, "menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes");
                        
            //値セット
            objSendTargetWin.value = varWindowName;
            objSendForm.target=objSendTargetWin.value;
                            
            objSendForm.action = varAction;
            objSendForm.submit();
        }

        //種別の表示切替
        function SyubetuDisp(){
            var objTmp = null;
            
            //画面上部・取消行表示
            objTmp = objEBI("<%=RadioTorikesiDisp.clientID %>");
            if(objTmp.checked){
                TorikesiDisp(1); //表示
            }else{
                TorikesiDisp(0); //非表示
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
        <%-- 画面タイトル --%>
        <table>
            <tr>
                <td>
                    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
                        class="titleTable">
                        <tr>
                            <th style="text-align: left; width: 100px;">
                                物件履歴</th>
                            <th style="text-align: left;">
                                <input type="button" id="ButtonClose" runat="server" value="閉じる" onclick="window.close();" />
                            </th>
                            <th style="width:20px">
                                &nbsp;</th>
                            <th>
                                取消行:
                                <input type="radio" id="RadioTorikesiDisp" name="RadioTorikesiDisp" runat="server" value="1" onclick="TorikesiDisp(this.value)"/>表示
                                <input type="radio" id="RadioTorikesiDispNone" name="RadioTorikesiDisp" runat="server" value="0" onclick="TorikesiDisp(this.value)"/>非表示
                                <input type="button" id="ButtonTorikesiDisp" value="取消行も表示する" style="width: 140px;display:none;"
                                    onclick="TorikesiDisp(1)" tabindex="-1"/>
                                <input type="button" id="ButtonTorikesiDispNone" value="取消行非表示" style="width: 120px;display:none;"
                                    onclick="TorikesiDisp(0)" tabindex="-1"/>
                            </th>
                            <th style="width: 20px">
                                &nbsp;</th>
                            <th>
                                種別絞込
                                <asp:DropDownList runat="server" ID="SelectSyubetu" Width="300px">
                                </asp:DropDownList><span id="SpanSyubetu" runat="server"></span>
                            </th>
                            <th style="width: 90px">
                                &nbsp;</th>
                            <th style="text-align:right">
                                <input type="button" id="ButtonReload" runat="server" value="再読込" style="width: 80px;display:none;" />
                                <input type="button" id="ButtonSinki" runat="server" value="新規" style="width: 80px;" onclick="PopupSyousai(0)" />
                            </th>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <%-- 画面上部[物件基本情報] --%>
        <table cellpadding="0" cellspacing="0" style="width:800px; border-bottom: solid 2px gray; border-left: solid 2px gray;"
            class="mainTable">
            <tr>
                <td class="koumokuMei" style="width: 40px">
                    区分</td>
                <td>
                    <asp:TextBox runat="server" ID="TextKbn" Style="ime-mode: disabled; border-style: none;"
                        ReadOnly="true" CssClass="readOnlyStyle" TabIndex="-1" />
                    <input type="hidden" id="HiddenKbn" runat="server" />
                </td>
                <td class="koumokuMei" style="width: 40px">
                    番号</td>
                <td>
                    <asp:TextBox runat="server" ID="TextBangou" Style="width: 100px;" ReadOnly="true"
                        CssClass="readOnlyStyle" TabIndex="-1" />
                </td>
                <td class="koumokuMei" style="width: 60px">
                    施主名</td>
                <td colspan="8">
                    <asp:TextBox runat="server" ID="TextSesyuMei" Style="width: 27em;" ReadOnly="true"
                        CssClass="readOnlyStyle" TabIndex="-1" MaxLength="50" />
                </td>
            </tr>
        </table>
        <br />
        <%-- 画面上部[物件履歴情報] --%>
        <table cellpadding="0" cellspacing="0" style="border-bottom: solid 2px gray; border-left: solid 2px gray;">
            <tr>
                <td>
                    <table class="mainTable" style="width: 900px; border-bottom: none; border-left: solid 0px gray;
                        table-layout: fixed;" id="Table2" cellpadding="0" cellspacing="0">
                        <%-- ヘッダ部 --%>
                        <tr>
                            <td class="koumokuMei2" style="width: 200px">
                                種別/取消</td>
                            <td class="koumokuMei2" style="width: 300px">
                                分類/日付/汎用コード</td>
                            <td class="koumokuMei2" style="width: 350px">
                                内容</td>
                            <td class="koumokuMei2" style="width: 50px">
                                処理</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="width: 930px; height: 500px; overflow-y: scroll; border-top: none; border-left: solid 0px gray;">
                        <table class="mainTable" cellpadding="0" cellspacing="0" style="width: 930px; border-top: none;
                            border-left: solid 0px gray;">
                            <!-- データ部 -->
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

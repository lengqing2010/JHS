<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="SearchKousinRireki.aspx.vb" Inherits="Itis.Earth.WebUI.SearchKousinRireki"
    Title="EARTH 更新履歴照会" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script src="js/sortable_ja.js" type="text/javascript">
    </script>

    <script type="text/javascript">
    //画面起動時にウィンドウサイズをディスプレイに合わせる
    window.resizeTo(1024,768);

    /*====================================
    *グローバル変数宣言（画面部品）
    ====================================*/
    //コントロール接頭辞
    var gVarOyaSettouji = "ctl00_CPH1_";
    var gVarTr = "resultTr_"; 
        
    //画面表示部品
    var objKubun = null;
    var objKousinKoumoku = null;
    var objKousinbiFrom = null;
    var objKousinbiTo = null;
    var objHosyousyoNo = null;
    
    var objKameiten = null;
    var objKameitenTorikesiRiyuu = null;
    var objKameitenMei = null;
    var objKameitenKana = null;
    var objKousinBeforeValue = null;
    var objKousinAfterValue = null;   
    
    var objchkHyoujiGamen1 = null;
    var objchkHyoujiGamen2 = null;
    var objchkHyoujiGamen3 = null;
    var objchkHyoujiGamen4 = null;    
    
    //検索実行用
    var objMaxSearchCount = null;
    var objSearch = null;
    
    //画面遷移用
    var objSendBtn = null;
    var objSendTargetWin = null;
    
    /*************************************
    * onload時の追加処理
    *************************************/
    function funcAfterOnload() {
        //画面表示部品セット
        setGlobalObj(); 
        
        /*検索結果テーブル ソート設定*/
        sortables_init();
                 
    }
    
    
    /*********************************************
    * 戻り値がない為、同メソッドをオーバーライド
    *********************************************/
    function returnSelectValue(){
        return false;
    }
        

    /*********************************************
    * 画面表示部品オブジェクトをグローバル変数化
    *********************************************/
    function setGlobalObj() {
        //画面表示部品
        objKubun = objEBI("<%= SelectKubun.clientID %>");
        objKousinKoumoku = objEBI("<%= SelectKousinKoumoku.clientID %>");
        objKousinbiFrom = objEBI("<%= TextKousinbiFrom.clientID %>");
        objKousinbiTo = objEBI("<%= TextKousinbiTo.clientID %>");
        objHosyousyoNo = objEBI("<%= TextHosyousyoNo.clientID %>");
        
        objKameiten = objEBI("<%= TextkameitenCd.clientID %>");
        objKameitenTorikesiRiyuu = objEBI("<%= TextTorikesiRiyuu.clientID %>");
        objKameitenMei = objEBI("<%= kameitenNm.clientID %>");
        objKameitenKana = objEBI("<%= TextkameitenKana.clientID %>");
        objKousinBeforeValue = objEBI("<%= TextKousinBeforeValue.clientID %>");
        objKousinAfterValue = objEBI("<%= TextKousinAfterValue.clientID %>");
        
        objchkHyoujiGamen1 = objEBI("<%= chkHyoujiGamen1.clientID %>");
        objchkHyoujiGamen2 = objEBI("<%= chkHyoujiGamen2.clientID %>");
        objchkHyoujiGamen3 = objEBI("<%= chkHyoujiGamen3.clientID %>");
        objchkHyoujiGamen4 = objEBI("<%= chkHyoujiGamen4.clientID %>");


        //検索実行用
        objMaxSearchCount = objEBI("<%= maxSearchCount.clientID %>");
        objSearch = objEBI("<%= search.clientID %>");
        
        //画面遷移用
        objSendBtn = objEBI("<%= btnSend.clientID %>");
        objSendTargetWin = objEBI("<%= sendTargetWin.clientID %>");        
    }
    

    /**
    * 更新日 To自動セット
    * @return true/false
    */
    function setKousinbiTo(obj){
        if(obj.id == objKousinbiFrom.id && objKousinbiTo.value == ""){
            objKousinbiTo.value = obj.value;
            objKousinbiTo.select();
        }
        return true;
    }
        

    /***********************************
    * 「検索実行」押下時のチェック処理
    ***********************************/
    function checkJikkou(){
    
        var varMsg = "";

        //if(区分 && 番号) || (更新項目 || 更新前値 || 更新後値 ) || (更新日FROM || 更新日TO){
        if( (objKubun.value.Trim() != "" && objHosyousyoNo.value.Trim() != "") || 
            (checkInputJoukenKousinKoumoku()) || 
            (objKousinbiFrom.value.Trim() != "" || objKousinbiTo.value.Trim() != "") ){
        
            //どれかTRUEの場合
            
            //更新項目、更新前値、更新後値のすべてが未入力の場合、更新項目チェックを行わない
            if(objKousinKoumoku.value.Trim() == "" && objKousinBeforeValue.value.Trim() == "" && objKousinAfterValue.value.Trim() == ""){
                //return true;
            }else{
            
                //更新項目チェック
                
                //更新項目が入力されている場合、更新前値 or 更新後値 必須
                if(objKousinKoumoku.value.Trim() != "" && (objKousinBeforeValue.value.Trim() == "" && objKousinAfterValue.value.Trim() == "")){
                
                    //更新前値＆更新後値が入力されている場合
                    
                    varMsg = "<%= Messages.MSG153E %>";
                    varMsg = varMsg.replace("@PARAM1","更新項目");
                    varMsg = varMsg.replace("@PARAM2","更新前値または更新後値");
                    alert(varMsg);
        
                    if(objKousinBeforeValue.value.Trim() == "" && objKousinAfterValue.value.Trim() == "" )
                         objKousinBeforeValue.focus();
                    else if(objKousinBeforeValue.value.Trim() == "" && objKousinAfterValue.value.Trim() != "" )
                        objKousinBeforeValue.focus();
                    else if(objKousinBeforeValue.value.Trim() != "" && objKousinAfterValue.value.Trim() == "" )
                        objKousinAfterValue.focus();                
                
                    return false;

                }else if(objKousinKoumoku.value.Trim() == "" ){ 
                    //更新前値 or 更新後値 が入力されている場合、更新項目 必須
                    varMsg = "<%= Messages.MSG153E %>";
                    varMsg = varMsg.replace("@PARAM1","更新前値または更新後値");
                    varMsg = varMsg.replace("@PARAM2","更新項目");
                    alert(varMsg);
                    
                    objKousinKoumoku.focus();
                    return false;  
                }else{
                    //return true;
                }              
            }
        }else{
            //FALSEの場合
            varMsg = "<%= Messages.MSG026E %>";
            varMsg = varMsg.replace("@PARAM1","区分+番号");
            varMsg = varMsg.replace("@PARAM2","更新項目、更新日(From及びToによる範囲指定)");
            alert(varMsg);
            //focus処理
            //一番最初の未入力にフォーカスする
            if(objKubun.value.Trim() == ""){
                objKubun.focus();
            }else if(objHosyousyoNo.value.Trim() == ""){
                objHosyousyoNo.focus();            
            }else if(objKousinKoumoku.value.Trim() == ""){
                objKousinKoumoku.focus();            
            }else if(objKousinBeforeValue.value.Trim() == ""){
                objKousinBeforeValue.focus();            
            }else if(objKousinAfterValue.value.Trim() == ""){
                objKousinAfterValue.focus();            
            }else if(objKousinbiFrom.value.Trim() == ""){
                objKousinbiFrom.focus();            
            }else if(objKousinbiTo.value.Trim() == ""){
                objKousinbiTo.focus();            
            }
            return false;
        }
        
        //更新日 大小チェック
        if(!checkDaiSyou(objKousinbiFrom,objKousinbiTo,"更新日"))return false;
    
        //検索実行
        objSearch.click();
    }

    /**
     * 入力チェック(更新項目、更新前値、更新後値)
     * @return true:一つでも入力がある/false:一つも入力が無い
     */
    function checkInputJoukenKousinKoumoku(){    
        if(objKousinKoumoku.value!="")return true;
        if(objKousinBeforeValue.value!="")return true;
        if(objKousinAfterValue.value!="")return true;
        return false;
    }
    
    /*********************
    * 大小チェック
    *********************/
    function checkDaiSyou(objFrom,objTo,mess){
        if(objFrom.value != "" && objTo.value != ""){
            if(Number(removeSlash(objFrom.value)) > Number(removeSlash(objTo.value))){
                alert("<%= Messages.MSG022E %>".replace("@PARAM1",mess));
                objFrom.select();
                return false;
            }
        }
        return true;
    }
    
    /*******************************************
    * Allクリア処理後に実行されるファンクション
    *******************************************/
    function funcAfterAllClear(obj){
        objKubun.focus();
    }
    
     /*********************
     * 加盟店情報クリア
      *********************/
     function clrKameitenInfo(obj){
        if(obj.value == ""){
            //値のクリア
            objKameiten.value = "";
            objKameitenTorikesiRiyuu.value = "";
            objKameitenMei.value = "";
            //色をデフォルトへ
            objKameiten.style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>"
            objKameitenTorikesiRiyuu.style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>"
            objKameitenMei.style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>"
        }
     }

        /**
        * 明細行をダブルクリックした際の処理
        * @param objSelectedTr
        * @param intGamen[1:受,2:報,3:工,4:保]
        * @return
        */
        function returnSelectValue(objSelectedTr,intGamen) {
            if(objSelectedTr.tagName == "TR"){
                objSendTargetWin.value = "_blank";
                sendGyoumuGamen(objSelectedTr,intGamen,2);
            }
            return;        
        }

        /**
         * 別ウィンドウアイコンをクリックした際の処理
         * @param objOnCklick
         * @param intGamen[1:受,2:報,3:工,4:保]
         * @return
         */
        function returnSelectValueOtherWin(objOnCklick,intGamen){
            objSendTargetWin.value = "_blank";
            sendGyoumuGamen(objOnCklick.parentNode.parentNode,intGamen,1);
        }

        //子画面呼出処理
        function sendGyoumuGamen(objSelectedTr,intGamen,intEvent){  
            var varAction = '';
              
            //オブジェクトの再読込(Ajax再描画対応)
            objSendTargetWin = objEBI("<%= sendTargetWin.clientID %>");
            if(objSendTargetWin.value == "")objSendTargetWin.value="<%=EarthConst.MAIN_WINDOW_NAME %>";

            //戻り値郡配列(行の先頭セルの先頭オブジェクトから取得)
            var objSelRet = getChildArr(getChildArr(objSelectedTr,"TD")[0],"INPUT")[0];
            //var objSelRet = getChildArr(objSelectedTr,"TD")[0];

            //<!-- 画面引渡し情報 -->
            objSendForm = objEBI("openPageForm");
            objSendVal_st = objEBI("st");
            objSendVal_Kubun = objEBI("sendPage_kubun");
            objSendVal_HosyousyoNo = objEBI("sendPage_hosyoushoNo");
            objSendVal_kbn = objEBI("kbn");
            objSendVal_no = objEBI("no");

            //値セット   
            objSendForm.target=objSendTargetWin.value;
            objSendVal_st.value="<%=EarthConst.MODE_VIEW %>";
            if(objSelRet != undefined){
                var arrReturnValue = objSelRet.value.split(sepStr);  
                objSendVal_Kubun.value=arrReturnValue[0];
                objSendVal_HosyousyoNo.value=arrReturnValue[1];
                objSendVal_kbn.value=arrReturnValue[0];
                objSendVal_no.value=arrReturnValue[1];
            }
            
            //オープン対象の画面を指定
            
            //intEvent=1 : 別ウィンドウクリック(対象画面一つのみ)
            if(intEvent == 1){
                //オープン対象の業務画面を指定
                switch(intGamen){
                     case 1://
                        varAction = "<%=UrlConst.IRAI_KAKUNIN %>";
                        break;
                     case 2://
                        varAction = "<%=UrlConst.HOUKOKUSYO %>";
                        break;
                     case 3://
                        varAction = "<%=UrlConst.KAIRYOU_KOUJI %>";
                        break;
                     case 4://
                        varAction = "<%=UrlConst.HOSYOU %>";
                        break;
                     default://
                        return false;
                        break;
                }
                objSendForm.action = varAction;
                objSendForm.submit();
            }else if(intEvent == 2){//intEvent=2 : ダブルクリック(チェックボックスのチェック対象画面を開く)
                //E:??
                if(flgEaster == 1){
                    flgEaster = null;
                    objSendForm.action="<%=UrlConst.POPUP_GAMEN_KIDOU %>";
                    objSendForm.submit();
                    return true;
                }
                //1:受
                if(objchkHyoujiGamen1.checked == true){
                    objSendForm.action="<%=UrlConst.IRAI_KAKUNIN %>";
                    objSendForm.submit();
                }
                //2:報
                if(objchkHyoujiGamen2.checked == true){
                    objSendForm.action="<%=UrlConst.HOUKOKUSYO %>";
                    objSendForm.submit();
                }
                //3:工
                if(objchkHyoujiGamen3.checked == true){
                    objSendForm.action="<%=UrlConst.KAIRYOU_KOUJI %>";
                    objSendForm.submit();
                }
                //4:保
                if(objchkHyoujiGamen4.checked == true){
                    objSendForm.action="<%=UrlConst.HOSYOU %>";
                    objSendForm.submit();
                }
            }else{
                return false;
            }         
        }

    </script>
    
    <asp:ScriptManagerProxy ID="SMP1" runat="server">
    </asp:ScriptManagerProxy>
    <div>

    <!-- 画面タイトル -->
    <table>
        <tr>
            <td>
                <table style="text-align: left;" border="0" cellpadding="0" cellspacing="2" class="titleTable">
                    <tr>
                        <th style="text-align: left">
                            更新履歴照会
                        </th>
                        <th style="text-align: left;">
                                <input type="button" id="ButtonClose" runat="server" value="閉じる" onclick="window.close();" />
                            </th>
                        <th style="text-align: left; width: 50px;">
                            <span id="SpanTitle" runat="server"></span>
                        </th>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <table style="text-align: left;" class="mainTable" cellpadding="2">
        <thead>
            <tr>
                <th class="tableTitle" colspan="66" rowspan="1">
                    検索条件
                    <input id="btnClearWin" value="クリア" type="reset" class="button"/></th>
            </tr>
        </thead>
        <tbody>
     
            <tr>
                <td colspan="1" class="koumokuMei">
                    区分</td>
                <td colspan="7" rowspan="1" style="" >
                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server" RenderMode="Inline">
                        <ContentTemplate>
                            <asp:DropDownList ID="SelectKubun" runat="server">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td colspan="2" class="koumokuMei">
                    番号</td>
                <td colspan="10" class="codeNumber">
                    <input id="TextHosyousyoNo" runat="server" maxlength="10" style="width:72px; ime-mode: disabled;" />
                </td>
                <td colspan="2" class="koumokuMei">
                    最新加盟店</td>
                <td colspan="10" >
                    <asp:UpdatePanel ID="UpdatePanel_irai1" UpdateMode="Conditional" runat="server" RenderMode="Inline">
                        <ContentTemplate>
                            <input id="TextKameitenCd" runat="server" maxlength="5" style="width: 40px;" class="codeNumber" />
                            <input id="kameitenSearch" runat="server" type="button" value="検索" class="gyoumuSearchBtn"/>&nbsp;
                            <input id="kameitenNm" runat="server" class="readOnlyStyle" readonly="readonly" style="width:70px" tabindex="-1"/>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td colspan="2" >
                    <asp:UpdatePanel ID="UpdatePanel_KtTorikesi" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="kameitenSearch" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:TextBox ID="TextTorikesiRiyuu" runat="server" Width="40px"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td colspan="2" class="koumokuMei">
                    最新加盟店カナ</td>
                <td colspan="30">
                    <input id="TextKameitenKana" runat="server" maxlength="40" style="width: 100px; ime-mode: active;" />
                </td>                
            </tr>
            <tr>
                <td colspan="2" class="koumokuMei">
                    更新項目</td>
                <td colspan="6" rowspan="1" style="">
                    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server" RenderMode="Inline">
                        <ContentTemplate>
                            <asp:DropDownList ID="SelectKousinKoumoku" runat="server">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                 <td colspan="2" rowspan="1" class="koumokuMei" style="">
                    更新前値</td>
                <td colspan="10">
                    <input id="TextKousinBeforeValue" runat="server" maxlength="512" style="width: 100px; ime-mode: active;" />
                </td>   
                <td colspan="2" rowspan="1" class="koumokuMei">
                    更新後値</td>
                <td colspan="10" >
                    <input id="TextKousinAfterValue" runat="server" maxlength="512" style="width: 100px; ime-mode: active;" />
                </td> 
                <td colspan="2" class="koumokuMei">
                    更新日</td>
                <td colspan="32" class="date">
                    <input id="TextKousinbiFrom" runat="server" maxlength="10" class="date" onblur="flag=checkDate(this);if(flag==true){setKousinbiTo(this);}"/>&nbsp;～&nbsp;<input
                        id="TextKousinbiTo" runat="server" maxlength="10" class="date" onblur="checkDate(this);"/>
                </td>
                <!--<td colspan="2" class="koumokuMei">
                    更新者ID</td>
                <td colspan="42" class="">
                    <input id="TextKousinsya" runat="server" maxlength="30" style="width:175px; ime-mode: disabled;"/>
                </td>-->
            </tr>
            <tr>
                <td style="text-align: center; height: 5px;" colspan="66" rowspan="1">
                
                    <input id="chkHyoujiGamen1" name="chkHyoujiGamen1" value="1" type="checkbox" runat="server"
                        checked="checked" tabindex="0" />受注
                    <input id="chkHyoujiGamen2" name="chkHyoujiGamen2" value="2" type="checkbox" runat="server"
                        tabindex="0" />報告
                    <input id="chkHyoujiGamen3" name="chkHyoujiGamen3" value="3" type="checkbox" runat="server"
                        tabindex="0" />工事
                    <input id="chkHyoujiGamen4" name="chkHyoujiGamen4" value="4" type="checkbox" runat="server"
                        tabindex="0" />保証
                    <img src="images/spacer.gif" alt="" style="width: 30px; height: 0px;" />
                                    
                    <!-- 検索上限件数 -->
                    <select id="maxSearchCount" runat="server" style="visibility: hidden;">
                        <option value="100" selected="selected">100件</option>
                    </select>
                    <input id="btnSearch" value="検索実行" type="button" runat="server"/>
                    <input type="button" id="search" value="検索実行btn" style="display: none" runat="server" />
                    <input id="shadow" runat="server" style="visibility: hidden; width: 60px;" />
                </td>
            </tr>
        </tbody>
    </table>
    <table style="height: 30px">
        <tr>
            <td>
                検索結果：
            </td>
            <td id="resultCount" runat="server">
            </td>
            <td>
                件
            </td>
        </tr>
    </table>
    <div class="dataGridHeader" id="dataGridContent">
        <table class="scrolltablestyle2 sortableTitle" id="meisaiTable" cellpadding="0" cellspacing="0">
            <!-- テーブルヘッダ -->
            <thead>
                <tr id="meisaiTableHeaderTr" runat="server" style="position: relative; top: expression(this.offsetParent.scrollTop)">
                    <th style="width: 80px;">
                        画面選択</th>
                    <th style="width: 120px;">
                        更新日時</th>
                    <th style="width: 20px;">
                        区分</th>
                    <th style="width: 65px;">
                        番号</th>
                    <th style="width: 60px;">
                        更新項目</th>
                    <th style="width: 240px;">
                        更新前値</th>
                    <th style="width: 240px;">
                        更新後値</th>
                    <th style="width: 65px;">
                        更新者ID</th>
                </tr>
            </thead>
            <!-- テーブル内容 -->
            <tbody id="searchGrid" runat="server">
            </tbody>
        </table>
    </div>
    </div>

    <input type="hidden" id="firstSend" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server" RenderMode="Inline">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSend" />
        </Triggers>
        <ContentTemplate>
            <input type="hidden" id="sendKubun" runat="server" />
            <input type="hidden" id="sendHoshoushoNo" runat="server" />
            <input type="hidden" id="sendTargetWin" runat="server" />
            <!-- 受注確認画面遷移ボタン（非表示） -->
            <input type="button" id="btnSend" value="確認画面遷移" style="display: none" runat="server"
                onserverclick="btnSend_ServerClick" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

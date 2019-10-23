<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
  Codebehind="SeikyuuDateIkkatuHenkou.aspx.vb" Inherits="Itis.Earth.WebUI.SeikyuuDateIkkatuHenkou"
  Title="請求年月日一括変更処理実行画面" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

  <script type="text/javascript" src="js/jhsearth.js">
  </script>

  <script type="text/javascript">
    history.forward();
  
    //ウィンドウサイズ変更
    try{
        if(window.name != "<%=EarthConst.MAIN_WINDOW_NAME %>") window.resizeTo(1024,768);
    }catch(e){
        //アクセスが拒否されましたのエラーが出たら何もしない。
        if(e.number == 2147024891) throw e;
    }
  
    _d = document;
    /*====================================
     *グローバル変数宣言（画面部品）
     ====================================*/
     //画面表示部品
     var objSeikyuuSakiKbn = null;
     var objSeikyuuSakiCd = null;
     var objSeikyuuSakiBrc = null;
     var objSeikyuuSimebi = null;   
     var objNewSeikyuuSimebi = null;

     //検索実行用
     var objSearch = null;
     
     //更新実行用
     var objExeBtn = null;
     var objSearchFlg = null;
     
     //アクション実行ボタン(検索実行,CSV出力,請求書印刷,請求書取消)
     var gBtnSearch = null;

     //該当データなしメッセージ制御用フラグ
     var gNoDataMsgFlg = null;

    /*************************************
     * onload時の追加処理
     *************************************/
     function funcAfterOnload() {
         //画面表示部品セット
         setGlobalObj();
         
         /*検索結果テーブル 各種レイアウト設定*/
         settingResultTable();

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
         objSeikyuuSakiKbn = objEBI("<%= SelectSeikyuuKbn.clientID %>");
         objSeikyuuSakiCd = objEBI("<%= TextSeikyuuSakiCd.clientID %>");
         objSeikyuuSakiBrc = objEBI("<%= TextSeikyuuSakiBrc.clientID %>");
         objSeikyuuSimebi = objEBI("<%= TextSeikyuuSimebi.clientID %>").value.replace("日","");
         objarrNewSeikyuubi = objEBI("<%= TextSeikyuuDate.clientID %>").value.split("/");
         
         objResultCnt = objEBI("<%=TableDataTable1.clientID %>");
         
         //検索実行用
         objSearch = objEBI("<%= search.clientID %>");
         
         //更新実行用
         objExeBtn = objEBI("<%= ButtonSeikyuuDateIkkatuHenkouExe.clientID %>");
         objSearchFlg = objEBI("<%= HiddenSearch.clientID %>")

         //アクション実行ボタン
         gBtnSearch = "<%= EarthEnum.emSearchSeikyuusyoBtnType.Search %>";
     }
     
    /*
     * 検索結果テーブル 各種レイアウト設定
     */
     function settingResultTable(type){
        var tableTitle1 = objEBI("<%=TableTitleTable1.clientID %>");    // ヘッダ部左テーブル
        var tableTitle2 = objEBI("<%=TableTitleTable2.clientID %>");    // ヘッダ部右テーブル
        var tableData1 = objEBI("<%=TableDataTable1.clientID %>");      // データ部左テーブル
        var tableData2 = objEBI("<%=TableDataTable2.clientID %>");      // データ部右テーブル
        var divTitle1 = objEBI("<%=DivLeftTitle.clientID %>");          // ヘッダ部左DIV
        var divTitle2 = objEBI("<%=DivRightTitle.clientID %>");         // ヘッダ部右DIV
        var divData1 = objEBI("<%=DivLeftData.clientID %>");            // データ部左DIV
        var divData2 = objEBI("<%=DivRightData.clientID %>");           // データ部右DIV
        var minHeight = 100;                                            // ウィンドウリサイズ時の検索結果テーブルに設定する最低高さ
        var adjustHeight = 180;                                         // 調整高さ(大きい程、検索結果テーブルが低くなる)
        var adjustWidth = 695;                                          // 調整幅(大きい程、検索結果テーブルが狭くなる)

        //固定列有りの検索結果テーブル用レイアウト等設定処理
        settingResultTableForColumnFix(type
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
                                        , adjustWidth)
    }
    
   /*******************************************
    * Allクリア処理後に実行されるファンクション
    *******************************************/
    function funcAfterAllClear(obj){
    
    }

   /***********************************
    * 「検索実行」押下時のチェック処理
    ***********************************/
    function checkJikkou(){    

        //画面表示部品セット
        setGlobalObj();

        var varMsg = "";
       
        //請求先コード・枝番・区分 必須チェック
        if(objSeikyuuSakiKbn.value.Trim() == "" || objSeikyuuSakiCd.value.Trim() == "" || objSeikyuuSakiBrc.value.Trim() == ""){
            varMsg = "<%= Messages.MSG013E %>";
            varMsg = varMsg.replace("@PARAM1","請求先区分、請求先コード、請求先枝番");
            alert(varMsg);
            return false;
        }

        //検索実行
        objSearch.click();

        return true;
    }
    
   /**
    * スクロール同期
    * @return 
    */
    function syncScroll(type,obj){
        if(type==1){
            //左側スクロール時
            objEBI("<%=DivLeftTitle.clientID %>").scrollLeft=obj.scrollLeft;
            objEBI("<%=DivRightData.clientID %>").scrollTop=obj.scrollTop;
        }else if(type==2){
            //右側スクロール時
            objEBI("<%=DivRightTitle.clientID %>").scrollLeft=obj.scrollLeft;
            objEBI("<%=DivLeftData.clientID %>").scrollTop=obj.scrollTop;
        }
    }
        
   /*******************************************
    * 「請求年月日一括変更処理実行」押下時処理
    *******************************************/
    function executeConfirm(objCtrl){

        //画面表示部品セット
        setGlobalObj();
            
        //必須チェック(請求先、請求書発行日)
        if (objSeikyuuSakiKbn.value.Trim() == "" || objSeikyuuSakiCd.value.Trim() == "" || objSeikyuuSakiBrc.value.Trim() == "" || objarrNewSeikyuubi.length != 3){
            alert('<%= Messages.MSG013E.Replace("@PARAM1", "請求先、請求書発行日(請求年月日)") %>');
            return false;
        }
        
        //締日、請求年月日比較
        if (objSeikyuuSimebi != objarrNewSeikyuubi[2]){
            if (!confirm('<%= Messages.MSG177C %>')) return false;
        }
        
        //対象データのユーザ確認(対象データが未存在あるいは、未検索の場合エラー)
        if (objResultCnt.rows.length == 0 || objSearchFlg.value == '<%= Me.BTN_SEARCH_FLG_NASI %>'){
            alert('<%= Messages.MSG189E %>');
            return false;
        }
                
        //実行
        if(objCtrl == objEBI("<%= ButtonSeikyuuDateIkkatuHenkou.clientID %>")){
            if(!confirm('<%= Messages.MSG042C.Replace("@PARAM1", "請求年月日一括変更処理") %>')){
                return false;
            }
        }

        //画面グレイアウト
        setWindowOverlay(objCtrl);
        //実行ボタン押下
        objExeBtn.click();
    }
    
   /*********************
    * 請求先情報クリア
    *********************/
    function clrSeikyuuInfo(obj){
        if(obj.value == ""){
            //値のクリア
            objEBI("<%= TextSeikyuuSakiMei.clientID %>").value = "";
            objEBI("<%= TextSeikyuuSimebi.clientID %>").value = "";
            objEBI("<%= TextTorikesiRiyuu.clientID %>").value = "";
            //色をデフォルトへ
            objEBI("<%= TextSeikyuuSakiCd.clientID %>").style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>"
            objEBI("<%= TextSeikyuuSakiBrc.clientID %>").style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>"
            objEBI("<%= SelectSeikyuuKbn.clientID %>").style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>"
        }
    }
      
  </script>

  <%-- 画面タイトル --%>
  <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
    class="titleTable">
        <tr>
            <th style="text-align: left">
                請求年月日一括変更処理実行
            </th>
        </tr>
  </table>
  <br />
  <table style="text-align: left;" class="mainTable" cellpadding="2">
    <thead>
        <tr>
            <th class="tableTitle" colspan="8" rowspan="1">
                検索条件
                <input id="btnClearWin" value="クリア" type="reset" class="button" tabindex="10" /></th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class="koumokuMei">
                請求先</td>
            <td class="codeNumber">
                <asp:UpdatePanel ID="UpdatePanel_seikyuu" UpdateMode="Conditional" runat="server"
                    RenderMode="Inline">
                    <ContentTemplate>
                        <asp:DropDownList ID="SelectSeikyuuKbn" runat="server" TabIndex="20">
                        </asp:DropDownList>
                        <input id="TextSeikyuuSakiCd" runat="server" maxlength="5" style="width: 35px;" class="codeNumber"
                            tabindex="20" />&nbsp;-
                        <input id="TextSeikyuuSakiBrc" runat="server" maxlength="2" style="width: 15px;"
                            class="codeNumber" tabindex="20" />
                        <input id="btnSeikyuuSakiSearch" runat="server" type="button" value="検索" class="gyoumuSearchBtn"
                            tabindex="20" />&nbsp;
                        <input id="TextSeikyuuSakiMei" runat="server" class="readOnlyStyle" readonly="readonly"
                            style="width: 190px" tabindex="-1" />&nbsp; 締日：<input id="TextSeikyuuSimebi" runat="server"
                            class="readOnlyStyle" readonly="readonly" style="width: 40px" tabindex="-1" />
                        <input type="hidden" id="HiddenSeikyuuSakiCdOld" runat="server" />
                        <input type="hidden" id="HiddenSeikyuuSakiBrcOld" runat="server" />
                        <input type="hidden" id="HiddenSeikyuuSakiKbnOld" runat="server" />
                        <input type="hidden" id="HiddenSearch" runat="server" /><%--検索ボタン押下有無判断--%>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="koumokuMei">
                取消</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel_SeikyuusakiToikesi" UpdateMode="Conditional" runat="server"
                    RenderMode="Inline">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSeikyuuSakiSearch" />
                    </Triggers>
                    <ContentTemplate>
                        <input id="TextTorikesiRiyuu" runat="server" style="width:80px;" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="text-align: center;" colspan="8" rowspan="1">
                <input id="btnSearch" value="検索実行" type="button" runat="server" tabindex="30" style="padding-top: 2px;" />
                <input type="button" id="search" value="検索実行btn" style="display: none" runat="server"
                    tabindex="-1" />
            </td>
        </tr>
    </tbody>
  </table>
  <%-- 検索結果 --%>
  <table style="height: 30px">
    <tr>
        <td>
            検索結果：</td>
        <td id="resultCount" runat="server">
        </td>
        <td>
            件
        </td>
        <td id="TdMsgArea" runat="server" style="color: red; font-weight: bold;">
        </td>
    </tr>
  </table>
  <table cellpadding="0" cellspacing="0">
    <thead>
        <tr>
            <th style="text-align: left;">
                <div id="DivLeftTitle" runat="server" class="scrollDivLeftTitleStyle2">
                    <table cellpadding="0" cellspacing="0" id="TableTitleTable1" runat="server" class="scrolltablestyle2 sortableTitle"
                        style="border-top: 1px solid #999999; border-left: 1px solid #999999;">
                        <thead>
                            <tr id="meisaiTableHeaderTr" runat="server" style="height: 20px;">
                                <th style="width: 40px;">
                                    区分</th>
                                <th style="width: 70px;">
                                    番号</th>
                                <th style="width: 210px;">
                                    施主名</th>
                                <th style="width: 90px;">
                                    加盟店コード</th>
                                <th style="width: 70px;">
                                    商品コード</th>
                                <th style="width: 180px;">
                                    商品名</th>
                            </tr>
                        </thead>  
                    </table>
                </div>
            </th>
            <th style="text-align: left;">
                <div id="DivRightTitle" runat="server" class="scrollDivRightTitleStyle2" style="border-right: 1px solid #999999;">
                    <table cellpadding="0" cellspacing="0" id="TableTitleTable2" runat="server" class="scrolltablestyle2 sortableTitle"
                        style="border-top: 1px solid #999999;">
                        <thead>
                            <tr style="height: 20px;">
                                <th style="width: 40px;">
                                    数量</th>
                                <th style="width: 80px;">
                                    売上金額</th>
                                <th style="width: 100px;">
                                    請求書発行日</th>
                                <th style="width: 90px;">
                                    売上年月日</th>
                                <th style="width: 100px;">
                                    伝票売上年月日</th>
                            </tr>
                        </thead>
                    </table>
                </div>    
            </th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td style="vertical-align: top;">
                <div id="DivLeftData" runat="server" class="scrollLeftDivStyle2" onscroll="syncScroll(1,this);"
                    onmousewheel="wheel(event,this);">
                    <table cellpadding="0" cellspacing="0" id="TableDataTable1" runat="server" class="scrolltablestyle2 sortableData">
                    </table>
                </div>
            </td>
            <td style="vertical-align: top;">
                <div id="DivRightData" runat="server" class="scrollDivStyle2" onscroll="syncScroll(2,this);"
                    style="border-right: 1px solid #999999;">
                    <table cellpadding="0" cellspacing="0" id="TableDataTable2" runat="server" class="scrolltablestyle2 sortableData">
                    </table>
                </div>
            </td>
        </tr>
    </tbody>
  </table>
  <br /> 
  <%-- 一括変更処理 --%>  
  <table style="width: 950px" cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <table style="text-align: left" id="TableIkkatuHenkou" class="mainTable" cellpadding="1" cellspacing="1">
                <tr>
                    <td colspan="2" style="text-align: center;">
                        <div class="InfoText">
                            上で指定した請求先の請求書未作成データの請求日を<br />
                            下の【変更した請求書発行日（請求年月日）】に変更します。<br />
                        </div>
                        <br />
                        変更後の請求書発行日(請求年月日)：<input type="text" id="TextSeikyuuDate" runat="server" class="date hissu" maxlength="10"
                            onblur="checkDate(this);" tabindex="50"/>
                        <input type="button" id="ButtonSeikyuuDateIkkatuHenkou" value="請求年月日一括変更処理実行" runat="server"
                            style="width: 230px; height: 30px; font-size: 15px; font-weight: bold; color: black; background-color: Fuchsia;"
                            onclick="executeConfirm(this);" tabindex="50"/><br />
                        <input type="button" id="ButtonSeikyuuDateIkkatuHenkouExe" value="請求年月日一括変更処理実行Exe"
                            runat="server" style="display: none;" />
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 10px">
        </td>
        <td>
            <table style="text-align: left; width: 350px" id="TableHenkouKekka" class="mainTable mainTablefont" cellpadding="1" cellspacing="1">
                <tr>
                    <td class="koumokuMei" style="text-align: center; font-size: 16px; width: 25px;">
                       処<br />
                       理<br />
                       結<br />
                       果
                     </td>
                     <td>
                       <table cellpadding="1" cellspacing="1" class="subTable">
                           <tr>
                               <td>
                                   邸別請求テーブル</td>
                               <td id="TdResult1" runat="server">
                               </td>
                           </tr>
                           <tr>
                               <td>
                                   店別請求テーブル</td>
                               <td id="TdResult2" runat="server">
                               </td>
                           </tr>
                           <tr>
                               <td>
                                   店別初期請求テーブル</td>
                               <td id="TdResult3" runat="server">
                               </td>
                           </tr>
                           <tr>
                               <td>
                                   汎用売上テーブル</td>
                               <td id="TdResult4" runat="server">
                               </td>
                           </tr>
                           <tr>
                               <td>
                                   売上データテーブル(元データ削除済)</td>
                               <td id="TdResult5" runat="server">
                               </td>
                           </tr>  
                       </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
  </table>
</asp:Content>

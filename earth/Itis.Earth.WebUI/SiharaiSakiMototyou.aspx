<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
  Codebehind="SiharaiSakiMototyou.aspx.vb" Inherits="Itis.Earth.WebUI.SiharaiSakiMototyou"
  Title="EARTH 支払先元帳表示・出力画面" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

  <script type="text/javascript" src="js/jhsearth.js">
  </script>

  <script type="text/javascript">
        history.forward();

        /*====================================
         *グローバル変数宣言（画面部品）
         ====================================*/
        //画面表示部品
        var objShriSakiMei = null;
        var objNengappiFrom = null;
        var objNengappiTo = null;
        //検索実行用
        var objSearch = null;
        var objCsv = null;
        //hidden
        var objKubunVal = null;


        /*************************************
         * onload時の追加処理
         *************************************/
        function funcAfterOnload() {
            //画面表示部品セット
            setGlobalObj();
                        
            /*検索結果テーブル 各種レイアウト設定*/
            settingResultTable();
            
            // CSV出力を行なう
            if (objCsvOutPutFlg.value == "1"){
                // CSV出力フラグをクリア
                objCsvOutPutFlg.value = ""
                //処理確認
                if(!confirm("<%= Messages.MSG017C %>".replace("処理","CSV出力処理"))){return false};
                //CSV実行
                objCsv.click();
            }
        }
        
       /*********************************************
        * 戻り値がない為、同メソッドをオーバーライド
        *********************************************/
        function returnSelectValue(){
            return false;
        }
        
       /*********************************************
        * 値をチェックし、対象をクリアする
        *********************************************/
        function clrName(obj,targetId){
            if(obj.value == "") objEBI(targetId).value="";
        }
        
       /*********************************************
        * 画面表示部品オブジェクトをグローバル変数化
        *********************************************/
        function setGlobalObj() {
            //画面表示部品
            objShriSakiMei = objEBI("<%= TextShriSakiMei.clientID %>")
            objNengappiFrom = objEBI("<%= TextNengappiFrom.clientID %>");
            objNengappiTo = objEBI("<%= TextNengappiTo.clientID %>");
            //検索実行用
            objSearch = objEBI("<%= ButtonHiddenDisplay.clientID %>");
            // CSV出力用
            objCsv = objEBI("<%= ButtonHiddenCsv.clientID %>");
            objCsvOutPutFlg = objEBI("<%= HiddenCsvOutPut.clientID %>");
            // 印刷出力用
            objPrint = objEBI("<%= ButtonHiddenPrint.clientID %>");
            objPrintOutPutFlg = objEBI("<%= HiddenPrintOutPut.clientID %>");
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
            var minHeight = 120;                                            // ウィンドウリサイズ時の検索結果テーブルに設定する最低高さ
            var adjustHeight = 55;                                          // 調整高さ(大きい程、検索結果テーブルが低くなる)
            var adjustWidth = 600;                                          // 調整幅(大きい程、検索結果テーブルが狭くなる)

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
        function checkJikkou(varAction){

            //年月日 大小チェック
            if(!checkDaiSyou(objNengappiFrom,objNengappiTo,"年月日")){return false};
            if(varAction == "0"){
                //検索実行
                objSearch.click();
                
            }else if(varAction == "1"){
                    objCsvOutPutFlg.value = "1"
                    //検索実行
                    objSearch.click();

            }else if(varAction == "2"){
                    //検索実行
                    objPrint.click();
            }
        }

        /*********************
         * 大小チェック
         *********************/
        function checkDaiSyou(objFrom,objTo,mess){
            //経理対応データ日チェック
            if(objFrom.value != "" ){
                if(Number(removeSlash(objFrom.value)) < Number(removeSlash("<%=EarthConst.KEIRI_DATA_MIN_DATE %>"))){
                    alert("<%= Messages.MSG179W %>");
                    objFrom.select();
                    return false;
                }
            }
            //From,Toの比較
            if(objFrom.value != "" && objTo.value != ""){
                if(Number(removeSlash(objFrom.value)) > Number(removeSlash(objTo.value))){
                    alert("<%= Messages.MSG022E %>".replace("@PARAM1",mess));
                    objFrom.select();
                    return false;
                }
            }
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

  </script>

  <%--CSV出力判断--%>
  <asp:HiddenField ID="HiddenCsvOutPut" runat="server" />
  <%--印刷出力判断--%>
  <asp:HiddenField ID="HiddenPrintOutPut" runat="server" />
  <%-- どの商品を表すか（非表示） --%>
  <asp:HiddenField ID="HiddenTargetId" runat="server" />
  <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
    class="titleTable">
    <tbody>
      <tr>
        <th>
          支払先元帳表示・出力画面</th>
        <th style="text-align: right;">
        </th>
      </tr>
    </tbody>
  </table>
  <br />
  <table style="text-align: left;" cellpadding="2">
    <tr>
      <td>
        <table style="text-align: left;" class="mainTable" cellpadding="2">
          <thead>
            <tr>
              <th class="tableTitle" colspan="4" rowspan="1">
                検索・出力条件
                <input id="btnClearWin" value="クリア" type="reset" class="button" /></th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td class="koumokuMei">
                調査会社(支払先)</td>
              <td class="codeNumber hissu">
                <asp:UpdatePanel ID="UpdatePanel_ShriSaki" UpdateMode="Conditional" runat="server"
                  RenderMode="Inline">
                  <ContentTemplate>
                    <input id="TextSiharaisakiCd" runat="server" maxlength="7" style="width: 80px;" class="codeNumber" />
                    <input id="btnShriSakiSearch" runat="server" type="button" value="検索" class="gyoumuSearchBtn"
                      onserverclick="btnShriSakiSearch_ServerClick" />&nbsp;
                    <input id="TextShriSakiMei" runat="server" class="readOnlyStyle" readonly="readonly"
                      style="width: 180px" tabindex="-1" />&nbsp;
                    <input type="hidden" id="HiddenTysKensakuType" runat="server" />
                    <input type="hidden" id="HiddenKameitenCd" runat="server" />
                    <input type="hidden" id="HiddenKakusyuNG" runat="server" />
                  </ContentTemplate>
                </asp:UpdatePanel>
              </td>
              <td class="koumokuMei hissu">
                年月日</td>
              <td class="date hissu">
                <input id="TextNengappiFrom" runat="server" maxlength="10" class="date" onblur="checkDate(this);" />&nbsp;～&nbsp;<input
                  id="TextNengappiTo" runat="server" maxlength="10" class="date" onblur="checkDate(this);" /></td>
            </tr>
            <tr>
              <td style="text-align: center;" colspan="4" rowspan="1">
                <input type="button" id="ButtonDisplay" value="元帳画面表示" runat="server" style="padding-top: 2px;" />
                <input type="button" id="ButtonHiddenDisplay" value="元帳画面表示btn" style="display: none"
                  runat="server" tabindex="-1" />
                <input type="button" id="ButtonCsv" value="元帳データ出力" runat="server" />
                <input type="button" id="ButtonHiddenCsv" value="元帳データ出力btn" style="display: none"
                  runat="server" tabindex="-1" />
                <input type="button" id="ButtonPrint" value="元帳印刷" runat="server" />
                <input type="button" id="ButtonHiddenPrint" value="元帳印刷btn" style="display: none"
                  runat="server" tabindex="-1" />
              </td>
            </tr>
          </tbody>
        </table>
      </td>
      <td style="vertical-align: bottom;">
        <table style="text-align: left;" class="mainTable" cellpadding="2">
          <tr>
            <td>
              ファクタリング開始年月
            </td>
            <td id="TdFactaringStNengetu" runat="server" style="width: 100px; text-align: right">
            </td>
          </tr>
          <tr>
            <td>
              最新繰越日
            </td>
            <td id="TdSaisinKurikosiDate" runat="server" style="width: 100px; text-align: right">
            </td>
          </tr>
          <tr>
            <td>
              登録残高
            </td>
            <td id="TdTourokuZandaka" runat="server" style="width: 100px; text-align: right">
            </td>
          </tr>
        </table>
      </td>
    </tr>
  </table>
  <input type="hidden" id="returnTargetIds" runat="server" />
  <input type="hidden" id="afterEventBtnId" runat="server" />
  <input type="hidden" id="firstSend" runat="server" />
  <input id="search_shouhin23" runat="server" type="hidden" />
  <table style="height: 30px">
    <tr>
      <td>
        結果：
      </td>
      <td id="TdResultCount" runat="server">
      </td>
      <td>
        件
      </td>
    </tr>
  </table>
  <table cellpadding="0" cellspacing="0">
    <thead>
      <tr>
        <th style="text-align: left;">
          <div id="DivLeftTitle" runat="server" class="scrollDivLeftTitleStyle2">
            <table cellpadding="0" cellspacing="0" id="TableTitleTable1" runat="server" class="scrolltablestyle2"
              style="border-top: 1px solid #999999; border-left: 1px solid #999999;">
              <thead>
                <tr style="vertical-align: bottom">
                  <th style="width: 75px;">
                    年月日</th>
                  <th style="width: 36px;">
                    科目</th>
                  <th style="width: 50px;">
                    商品<br />
                    コード</th>
                  <th style="width: 140px;">
                    商品名/<br />
                    支払種別など</th>
                  <th style="width: 80px;">
                    顧客番号</th>
                  <th style="width: 140px;">
                    物件名 / 摘要など</th>
                  <th style="width: 36px;">
                    数量</th>
                </tr>
              </thead>
            </table>
          </div>
        </th>
        <th style="text-align: left;">
          <div id="DivRightTitle" runat="server" class="scrollDivRightTitleStyle2" style="border-right: 1px solid #999999;">
            <table cellpadding="0" cellspacing="0" id="TableTitleTable2" runat="server" class="scrolltablestyle2"
              style="border-top: 1px solid #999999;">
              <thead>
                <tr style="vertical-align: bottom">
                  <th style="width: 66px;">
                    単価</th>
                  <th style="width: 66px;">
                    税抜金額</th>
                  <th style="width: 66px;">
                    消費税</th>
                  <th style="width: 66px;">
                    金額</th>
                  <th style="width: 66px;">
                    残高</th>
                  <th style="width: 62px;">
                    伝票番号</th>
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
            <table cellpadding="0" cellspacing="0" id="TableDataTable1" runat="server" class="scrolltablestyle2">
            </table>
          </div>
        </td>
        <td style="vertical-align: top;">
          <div id="DivRightData" runat="server" class="scrollDivStyle2" onscroll="syncScroll(2,this);"
            style="border-right: 1px solid #999999;">
            <table cellpadding="0" cellspacing="0" id="TableDataTable2" runat="server" class="scrolltablestyle2">
            </table>
          </div>
        </td>
      </tr>
    </tbody>
  </table>
</asp:Content>

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopupSyouhin4.aspx.vb"
    Inherits="Itis.Earth.WebUI.PopupSyouhin4" Title="EARTH 商品4" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
    <meta http-equiv="Expires" content="-1" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>EARTH 商品4</title>
</head>

<script type="text/javascript" src="js/jhsearth.js"> 
</script>

<script type="text/javascript">
        history.forward();
        
        _d = document;
        
        //コントロール接頭辞
        var gVarSettouJi = "<%= EarthConst.USR_CTRL_ID_ITEM4 %>";
        
        /****************************************
         * onload時の追加処理
         ****************************************/
        function funcAfterOnload() {
            //テーブルのレイアウト設定
            settingTable();
            
            //画面遷移情報
            funcMove();
        }
        
        // 画面遷移情報
        function funcMove(){
            var arrArg = window.dialogArguments;
            var objMotoURL = objEBI("motoURL");
        }   
        
        // 商品4テーブル 各種レイアウト設定
        function settingTable(){
            var syouhinData = objEBI("tblMeisai");
            setTableStripes(syouhinData,1);
        }
               
        //商品４設定処理呼び出し
        function callSetSyouhin4(obj){
        
            var strIndex = GetIndex(obj);
            strIndex = strIndex + "_";
            
            //alert("商品４設定処理呼び出し");
            var strTypeID = gVarSettouJi + strIndex + "HiddenSyouhin4SearchType";
            var strBtnID = gVarSettouJi + strIndex + "ButtonSyouhinKensaku";
            
            objEBI(strTypeID).value = "";
            if(obj.value == ""){
                objEBI(strTypeID).value = "1";
                objEBI(strBtnID).click();
            }            
        }
        
        //Indexを返す
        function GetIndex(obj){
            var varTmp = "" + obj.id;
            varTmp = varTmp.replace(gVarSettouJi,'');
            varTmp = varTmp.replace("_HiddenSyouhin4SearchType",'');
            varTmp = varTmp.replace("_ButtonSyouhinKensaku",'');
            varTmp = varTmp.replace("_TextSyouhinCd",'');
            
            return varTmp;
        }
       
</script>

<body style="margin: 15px 10px 15px 10px">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="AjaxScriptManager" runat="server">
        </asp:ScriptManager>
        <%-- 隠し項目 --%>
        <input type="hidden" id="HiddenIraiGyoumuKengen" runat="server" value="0" />
        <input type="hidden" id="HiddenHattyuusyoKanriKengen" runat="server" value="0" />
        <input type="hidden" id="HiddenKeiriGyoumuKengen" runat="server" value="0" />
        <input type="hidden" id="HiddenRegUpdDate" runat="server" />
        <input type="hidden" id="HiddenKubun" runat="server" />
        <input type="hidden" id="HiddenNo" runat="server" />
        <input type="hidden" id="HiddenKameitenCd" runat="server" />
        <input type="hidden" id="HiddenJibanTysKaisyaCd" runat="server" />
        <%-- 画面タイトル --%>
        <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
            class="titleTable">
            <tr>
                <th style="text-align: left; width: 60px;">
                    商品4</th>
                <th style="width: 80px;">
                    <input type="button" id="ButtonClose" runat="server" value="閉じる" onclick="window.close();" />
                </th>
                <th style="width: 645px;">
                    &nbsp;<input type="button" id="ButtonAddNewRow" runat="server" value="新規行追加" onclick="settingTable();" />
                </th>
                <th>
                    <input type="button" runat="server" id="ButtonTouroku1" value="登録 / 修正実行" style="font-weight: bold;
                        font-size: 18px; width: 140px; color: black; height: 30px; background-color: fuchsia" />
                </th>
            </tr>
        </table>
        <br />
        <%-- 画面キー項目（表示のみ） --%>
        <table cellpadding="0" cellspacing="0" style="width: 800px; border-bottom: solid 2px gray;
            border-left: solid 2px gray;" class="mainTable">
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
        <%-- メイン画面 --%>
        <table class="" cellpadding="0" cellspacing="0" style="border-bottom: solid 2px gray;
            table-layout: fixed; border-left: solid 2px gray; width: 979px;">
            <tr>
                <td>
                    <table class="mainTable itemTableNarrow" style="width: 960px; border-bottom: none;
                        table-layout: fixed; border-left: none;" id="Table2" cellpadding="1">
                        <%-- ヘッダ部 --%>
                        <!-- 1行目 -->
                        <tr id="TdSyouhinCdTitle" class="shouhinTableTitle">
                            <td style="width: 239px; border-top: 0px; border-left: 0px;">
                                商品コード４</td>
                            <td style="border-top: 0px; border-left: 3px; width: 77px;">
                                承諾書<br />
                                金額
                            </td>
                            <td style="border-top: 0px; width: 77px;">
                                伝票仕入<br />
                                年月日</td>
                            <td class="itemMei_small" style="border-top: 0px; border-left: 3px; width: 77px;">
                                工務店請求<br />
                                税抜金額</td>
                            <td style="border-top: 0px; width: 77px;">
                                消費税</td>
                            <td style="border-top: 0px; width: 75px;">
                                伝票売上<br />
                                年月日</td>
                            <td style="border-top: 0px; width: 119px;">
                                売上年月日</td>
                            <td style="border-top: 0px; border-left: 3px; width: 80px;">
                                請求書<br />
                                発行日</td>
                            <td style="border-top: 0px; border-left: 3px; width: 50px;">
                                発注書<br />
                                金額</td>
                            <td style="border-top: 0px; width: 100%;">
                                発注書<br />
                                確定</td>
                            <td id="TdSpacer" runat="server" style="border-top: 0px; border-left: 3px; width: 150px;
                                display: none;" rowspan="2">
                                &nbsp;</td>
                        </tr>
                        <!-- 2行目 -->
                        <tr class="shouhinTableTitle">
                            <td style="border-left: 0px;">
                                商品名</td>
                            <td style="border-left: 3px;">
                                仕入<br />
                                消費税額
                            </td>
                            <td>
                                伝票仕入<br />
                                年月日修正</td>
                            <td class="itemMei_small" style="border-left: 3px;">
                                実請求<br />
                                税抜金額</td>
                            <td>
                                税込金額</td>
                            <td>
                                伝票売上<br />
                                年月日修正</td>
                            <td id="TdUriageSyoriTitle" runat="server">
                                売上処理</td>
                            <td style="border-left: 3px;">
                                <span id="SpanSeikyuuUmu" runat="server">請求<br />
                                    有無</span></td>
                            <td colspan="2" style="border-left: 3px;">
                                発注書<br />
                                確認日</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanelSyouhin4" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div style="width: 976px; height: 525px; overflow-y: scroll; border-top: none; border-left: solid 0px gray;">
                                <table class="mainTable" cellpadding="0" cellspacing="0" style="width: 960px; border-top: none;
                                    table-layout: fixed; border-left: solid 0px gray;">
                                    <!-- データ部 -->
                                    <tbody id="tblMeisai" runat="server">
                                    </tbody>
                                </table>
                            </div>
                            <input type="hidden" id="HiddenLineCnt" runat="server" value="0" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ButtonAddNewRow" EventName="ServerClick" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </form>
    <!-- 検索画面遷移用フォーム -->
    <form id="searchForm" method="post" action="">
        <!-- 検索条件値格納用 -->
        <input type="hidden" id="sendSearchTerms" name="sendSearchTerms" />
        <!-- 検索結果セット先ID格納用 -->
        <input type="hidden" id="returnTargetIds" name="returnTargetIds" />
        <!-- 結果セット後実行ボタンID格納用 -->
        <input type="hidden" id="afterEventBtnId" name="afterEventBtnId" />
    </form>
</body>
</html>

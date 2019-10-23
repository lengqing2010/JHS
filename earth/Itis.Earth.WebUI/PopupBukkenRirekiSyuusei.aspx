<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="PopupBukkenRirekiSyuusei.aspx.vb" Inherits="Itis.Earth.WebUI.PopupBukkenRirekiSyuusei"
    Title="EARTH 物件履歴修正" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script type="text/javascript">

        //ウィンドウサイズ変更
        try{
            window.resizeTo(880,650);
        }catch(e){
            //アクセスが拒否されましたのエラーが出たら何もしない。
            if(e.number == 2147024891) throw e;
        }
        
        //コントロール接頭辞
        var gVarOyaSettouji = "ctl00_CPH1_";
        var gVarSelectSyubetu = "_SelectSyubetu";
        var gVarSelectCode = "SelectCode";
        var gVarTextHizuke = "_TextHizuke";
        var gVarTextHanyouCode = "_TextHanyouCode";
        var gVarTextAreaNaiyou = "_TextAreaNaiyou";
               
        var gVarHiddenBunrui = "_HiddenBunrui";

        var gVarSelectTmpSyubetu = "SelectSyubetu_";
         
        var gVarFocus = '';
        
        _d = document;
                
        /****************************************
         * onload時の追加処理
         ****************************************/
        function funcAfterOnload() {
        
            // 分類プルダウンの値表示を切り替え
            ChgDispSelectBunrui();
           
            //フォーカス処理
            if(gVarFocus != ''){
                setFocus(gVarFocus);
                gVarFocus = '';
            }
            
        }
        
        //フォーカス処理
        //※サーバーコントロール以外、ユーザーコントロールから親にフォーカスしたい等特殊な場合に使用
        function setFocus(objID){
            var objTmp = objID;
            
            if(objID.indexOf(gVarSelectSyubetu) != -1){ //種別
            }else if(objID.indexOf(gVarHiddenBunrui) != -1){ //分類
                objTmp = objEBI("SelectBunrui");
                objTmp.selectedIndex = 0;
                objTmp.focus();
                return true;
            }else if(objID.indexOf(gVarTextHizuke) != -1){ //日付
            }else if(objID.indexOf(gVarTextHanyouCode) != -1){ //汎用コード
            }else if(objID.indexOf(gVarTextAreaNaiyou) != -1){ //内容
            }else{
                return false;
            }
            objEBI(objTmp).focus();
        }
        
        
        // 分類プルダウンの値表示を切り替え
        function ChgDispSelectBunrui(){
            var objSelSyubetu = objEBI("<%=SelectSyubetu.clientID %>");
            var objSelBunrui = objEBI("SelectBunrui");
            
            //オプション全削除
            SelectOptionDelete(objSelBunrui);
            
            if(objSelSyubetu.value == '') return false;
            
            //分類の存在チェック
            if(ChkExitSelectCode(objSelSyubetu.value) == false){
                var strMSG = "<%= Messages.MSG113E %>";
                strMSG = strMSG.replace('@PARAM1','分類');
                alert(strMSG);
                return false;
            }

            //オプション挿入
            SelectOptionInsert(objSelBunrui,objSelSyubetu.value);
            
            //オプションセット
            SelectOptionSet(objSelBunrui);
                
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
            var varMoto = gVarOyaSettouji + gVarSelectTmpSyubetu + intFlg;
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
        function SelectOptionSet(objSel){
            //Hiddenの取得およびセット
            var objHdnBunrui = objEBI("<%=HiddenBunrui.clientID %>");
            objSel.value = objHdnBunrui.value;
        }
        
        //分類ドロップダウンリスト変更時Hiddenを更新する
        function UpdHiddenBunrui(objSel){
            if(objSel == undefined) return false;
                        
            //Hidden分類の取得
            var objHdnCode = objEBI("<%=HiddenBunrui.clientID %>");
            if(objHdnCode == undefined) return false;
            
            if(objSel.value == ''){
                objHdnCode.value = '';
            }else{
                objHdnCode.value = objSel.value;
            }
        }
        
        //種別変更時、分類の中身を作り変える
        function SelectSyubetuOnChg(objID){            
            //Hidden分類の初期化
            var objHdnBunrui = objEBI("<%=HiddenBunrui.clientID %>");
            if(objHdnBunrui == undefined) return false;
            objHdnBunrui.value = '';
            
            //オブジェクトの取得
            var objSelSyubetu = objEBI("<%=SelectSyubetu.clientID %>");
            if(objSelSyubetu == undefined) return false;
            
            //オブジェクトの取得
            var objSelBunrui = objEBI("SelectBunrui");
            if(objSelBunrui == undefined) return false;
            
            //オプション全削除
            SelectOptionDelete(objSelBunrui);
            
            if(objSelSyubetu.value == '') return false;
            //分類の存在チェック
            if(ChkExitSelectCode(objSelSyubetu.value) == false){
                var strMSG = "<%= Messages.MSG113E %>";
                strMSG = strMSG.replace('@PARAM1','分類');
                alert(strMSG);
                return false;
            }
            
            //オプション挿入
            SelectOptionInsert(objSelBunrui,objSelSyubetu.value);
        }
        
        //親画面リロード処理
        function OyaReload(){
            //親ウィンドウが閉じていないかのチェック
            if(window.opener == null || window.opener.closed){
                alert("呼び出し元画面が閉じられた為、リロードできませんでした。");
                return false;
            }else{               
                var _wod = window.opener.document; //親ウィンドウのドキュメントオブジェクト
                var afterEventBtnId = _wod.getElementById("afterEventBtnId").value;  //値セット後に押下する、親画面のボタンID
                
                //値セット後に親画面のボタンを押下
                if(afterEventBtnId != undefined && afterEventBtnId != "" && _wod.getElementById(afterEventBtnId) != undefined){
                  _wod.getElementById(afterEventBtnId).fireEvent("onclick");
                }
                
                //親ウィンドウへフォーカス
                window.opener.focus();
                
                //自身を閉じる
                window.close();
                
            }
        }
        
    </script>

    <div>
        <input type="hidden" id="HiddenUpdDatetime" runat="server" />
        <input type="hidden" id="HiddenNyuuryokuNo" runat="server" />
        <input type="hidden" id="HiddenGamenMode" runat="server" />
        <div id="divSelect" runat="server">
        </div>
        <asp:DropDownList runat="server" ID="SelectTmpCode" Style="display: none;">
        </asp:DropDownList>
        <%-- 画面タイトル --%>
        <table>
            <tr>
                <td>
                    <table style="text-align: left; width: 800px;" border="0" cellpadding="0" cellspacing="2"
                        class="titleTable">
                        <tr>
                            <th style="text-align: left; width: 150px;">
                                <span id="SpanTitle" runat="server"></span>
                            </th>
                            <th>
                                <input type="button" id="ButtonClose" runat="server" value="閉じる" onclick="window.close();" />
                            </th>
                            <th style="width: 600px">
                                &nbsp;</th>
                            <th style="text-align: right">
                                <input type="button" id="ButtonUpdate"  runat="server" style="width: 80px;" />
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
        <%-- 画面メイン[物件履歴情報] --%>
        <table cellpadding="0" cellspacing="0" style="width:800px; border-bottom: solid 2px gray; border-left: solid 2px gray;"
            class="mainTable">
            <tr>
                <td class="koumokuMei" style="">
                    種別</td>
                <td>
                    <asp:DropDownList runat="server" ID="SelectSyubetu" Width="300px" CssClass="hissu">
                    </asp:DropDownList><span id="SpanSyubetu" runat="server"></span>
                    <input type="hidden" id="HiddenSyubetu" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="koumokuMei" style="">
                    分類</td>
                <td>
                    <select id="SelectBunrui" style="width: 300px" onchange="UpdHiddenBunrui(this)" class="hissu">
                    </select>
                    <input type="hidden" id="HiddenBunrui" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="koumokuMei" style="">
                    日付</td>
                <td>
                    <asp:TextBox runat="server" ID="TextHizuke" MaxLength="10" CssClass="date" />
                </td>
            </tr>
            <tr>
                <td class="koumokuMei" style="">
                    汎用コード</td>
                <td>
                    <asp:TextBox runat="server" ID="TextHanyouCode" Style="width: 200px;" CssClass="codeNumber"
                        MaxLength="20" />
                </td>
            </tr>
            <tr>
                <td class="koumokuMei" style="">
                    内容</td>
                <td>
                    <textarea id="TextAreaNaiyou" runat="server" cols="100" onfocus="this.select();" rows="7"
                        style="ime-mode: active; font-family: Sans-Serif"></textarea>
                </td>
            </tr>
            <tr>
                <td class="koumokuMei" style="">
                    登録者</td>
                <td>
                    <asp:TextBox runat="server" ID="TextTourokuSya" Style="width: 100px;" ReadOnly="true"
                        CssClass="readOnlyStyle" TabIndex="-1" />
                </td>
            </tr>
            <tr>
                <td class="koumokuMei" style="">
                    登録日時</td>
                <td>
                    <asp:TextBox runat="server" ID="TextTourokuDate" Style="width: 100px;" ReadOnly="true"
                        CssClass="readOnlyStyle" TabIndex="-1" />
                </td>
            </tr>
            <tr>
                <td class="koumokuMei" style="">
                    最終更新者</td>
                <td>
                    <asp:TextBox runat="server" ID="TextKousinSya" Style="width: 100px;" ReadOnly="true"
                        CssClass="readOnlyStyle" TabIndex="-1" />
                </td>
            </tr>
            <tr>
                <td class="koumokuMei" style="">
                    最終更新者日時</td>
                <td>
                    <asp:TextBox runat="server" ID="TextKousinDate" Style="width: 100px;" ReadOnly="true"
                        CssClass="readOnlyStyle" TabIndex="-1" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

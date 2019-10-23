<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="IkkatuHenkouKihon.aspx.vb" Inherits="Itis.Earth.WebUI.IkkatuHenkouKihon"
    Title="EARTH 一括変更【物件基本情報】" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script type="text/javascript">
        //画面起動時にウィンドウサイズをディスプレイに合わせる
        window.moveTo(0, 0);
        window.resizeTo(window.screen.availWidth, window.screen.availHeight);
        var gstrClientId = "<%= ME_CLIENT_ID %>"
        var gVarSettouJi = gstrClientId + "<%= IKKATU_HENKOU1_CTRL_NAME %>"; //コントロール接頭辞

        //画面上部・先頭行ボタン押下処理
        function SetSentouGyou(){
            //明細行の一行目を取得し、画面上部にセット
            objEBI("<%= TextSesyuMei.clientID %>").value = RetValue("_TextSesyuMei");
            if(ChkKengen()){
                objEBI("<%= TextTyousaKibouDate.clientID %>").value = RetValue("_TextTyousaKibouDate");
                objEBI("<%= TextTyousaKibouJikan.clientID %>").value = RetValue("_TextTyousaKibouJikan");
            }
            objEBI("<%= TextBukkenJyuusyo1.clientID %>").value = RetValue("_TextBukkenJyuusyo1");
            objEBI("<%= TextBukkenJyuusyo2.clientID %>").value = RetValue("_TextBukkenJyuusyo2");
            objEBI("<%= TextBukkenJyuusyo3.clientID %>").value = RetValue("_TextBukkenJyuusyo3");
            objEBI("<%= TextBikou.clientID %>").value = RetValue("_TextBikou");
            objEBI("<%= TextBunjouCode.clientID %>").value = RetValue("_TextBunjouCode");
            objEBI("<%= TextNayoseCode.clientID %>").value = RetValue("_TextNayoseCode");
            objEBI("<%= TextBukkenMei.clientID %>").value = RetValue("_TextBukkenMei");
            objEBI("<%= SelectKeiyu.clientID %>").value = RetValue("_SelectKeiyu");
            
        }
             
        //明細行の一行目を返す
        function RetValue(varTarget){
            varLine = "1";
            varTmpId = gVarSettouJi + varLine + varTarget;
            return objEBI(varTmpId).value;
        }
        
        //権限チェック
        function ChkKengen(){
            var objTmp = objEBI("<%= ButtonCopyTyousaKibouDate.clientID %>");
            if( objTmp.disabled == true ){
                return false
            }
            return true;
        }
        
        //画面上部・コピーボタン押下処理(共通)
        function SetCopyValue(objId){
            var varTarget = ''; //対象コントロール
            var setVal = ''; //セットする値
            var setBlnkFlg = ''; //空白セット判断フラグ
            var varMsg = '';
            
            if(objId.indexOf("ButtonCopySesyuMei") != -1){ //施主名
                varTarget = "_TextSesyuMei";
                setVal = objEBI("<%= TextSesyuMei.clientID %>").value;
                
            }else if(objId.indexOf("ButtonCopyTyousaKibouDate") != -1){ //調査希望日
                varTarget = "_TextTyousaKibouDate";
                setVal = objEBI("<%= TextTyousaKibouDate.clientID %>").value;
                
            }else if(objId.indexOf("ButtonCopyTyousaKibouJikan") != -1){ //調査希望時間
                varTarget = "_TextTyousaKibouJikan";
                setVal = objEBI("<%= TextTyousaKibouJikan.clientID %>").value;
                setBlnkFlg = '1'; //フラグをたてる
                
            }else if(objId.indexOf("ButtonCopyBukkenJyuusyo1") != -1){ //物件住所1
                varTarget = "_TextBukkenJyuusyo1";
                setVal = objEBI("<%= TextBukkenJyuusyo1.clientID %>").value;
                
            }else if(objId.indexOf("ButtonCopyBukkenJyuusyo2") != -1){ //物件住所2
                varTarget = "_TextBukkenJyuusyo2";
                setVal = objEBI("<%= TextBukkenJyuusyo2.clientID %>").value;
                setBlnkFlg = '1'; //フラグをたてる
                
            }else if(objId.indexOf("ButtonCopyBukkenJyuusyo3") != -1){ //物件住所3
                varTarget = "_TextBukkenJyuusyo3";
                setVal = objEBI("<%= TextBukkenJyuusyo3.clientID %>").value;
                setBlnkFlg = '1'; //フラグをたてる
                
            }else if(objId.indexOf("ButtonCopyBikou") != -1){ //備考
                varTarget = "_TextBikou";
                setVal = objEBI("<%= TextBikou.clientID %>").value;
                setBlnkFlg = '1'; //フラグをたてる
                
            }else if(objId.indexOf("ButtonCopyBunjou") != -1){ //分譲コード
                varTarget = "_TextBunjouCode";
                setVal = objEBI("<%= TextBunjouCode.clientID %>").value;
                setBlnkFlg = '1'; //フラグをたてる
                
            }else if(objId.indexOf("ButtonCopyNayose") != -1){ //物件名寄コード
                varTarget = "_TextNayoseCode";
                setVal = objEBI("<%= TextNayoseCode.clientID %>").value;
                setBlnkFlg = '1'; //フラグをたてる
                
            }else if(objId.indexOf("ButtonCopyBukkenMei") != -1){ //受注物件名
                varTarget = "_TextBukkenMei";
                setVal = objEBI("<%= TextBukkenMei.clientID %>").value;
                
            }else if(objId.indexOf("ButtonCopyKeiyu") != -1){ //経由
                varTarget = "_SelectKeiyu";
                setVal = objEBI("<%= SelectKeiyu.clientID %>").value;
                setBlnkFlg = '1'; //フラグをたてる
                
            }else{
                return false;
            }
            
            //メッセージ生成
            varMsg = '<%= String.Format(Messages.MSG126E, "コピー内容") %>';
            
            if(setBlnkFlg == '1'){
                //コピー内容が空白の場合            
                if(setVal == ''){
                    varMsg = varMsg + "よろしいですか？";
                    if(confirm(varMsg) == false){
                        return false;
                    }
                }                
            }else{
                //コピー内容が空白の場合            
                if(setVal == ''){
                    alert(varMsg);
                    return false;      
                }                
            }
            
            var VarCnt = objEBI("<%= HiddenLineCnt.clientID %>").value; //明細行数
            
            for(intCnt = 1; intCnt <= VarCnt; intCnt++){
                strCnt = "" + intCnt;
                var varTmpId = gVarSettouJi + strCnt + varTarget;
                objEBI(varTmpId).value = setVal;
            }
        }
        
        //受注物件名に施主名をセットする処理
        function setJyutyuuBukkenMei(objJyutyuuBukkenMei,objSesyuMei){
            if(objJyutyuuBukkenMei.className.indexOf("readOnlyStyle") == -1 && objJyutyuuBukkenMei.value == ""){
            objJyutyuuBukkenMei.value = objSesyuMei.value;
            }
        }   
            
    </script>

    <div>
        <input type="hidden" id="HiddenLineCnt" runat="server" value="0" /><%-- 物件数 --%>
        <table>
            <tr>
                <td>
                    <!-- 画面タイトル -->
                    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
                        class="titleTable">
                        <tr>
                            <th style="text-align: left; width: 200px;">
                                一括変更【物件基本情報】</th>
                            <th style="text-align: right">
                                <input type="button" id="ButtonIkkatuHenkou" runat="server" value="一括変更" style="font-weight: bold;
                                    font-size: 18px; width: 120px; color: black; height: 30px; background-color: fuchsia" />
                            </th>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <!-- 画面上部 -->
                    <table>
                        <tr>
                            <td>
                                <span style="font-weight: bold; font-size: 15px;">一括変更補助</span>
                            </td>
                            <td style="width: 20px">
                                &nbsp;</td>
                            <td>
                                <input type="button" id="ButtonGetFirstRow" runat="server" class="button selectedStyleB"
                                    value="先頭行をこの欄にセット" onclick="SetSentouGyou()" />
                            </td>
                        </tr>
                    </table>
                    <!-- 画面上部メイン -->
                    <table style="text-align: left; width: 927px; table-layout: fixed;" id="" class="mainTable"
                        cellpadding="1">
                        <tr>
                            <td class="koumokuMei2">
                                <input type="button" id="ButtonCopySesyuMei" runat="server" class="button_copy" style="padding-top: 2px;
                                    width: 90px; height: 25px;" value="施主名" onclick="SetCopyValue(this.id)" />
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="TextSesyuMei" runat="server" Style="width: 333px" MaxLength="50" />
                            </td>
                            <td class="koumokuMei2">
                                <input type="button" id="ButtonCopyBukkenMei" runat="server" class="button_copy"
                                    style="padding-top: 2px; width: 90px; height: 25px;" value="受注物件名" onclick="SetCopyValue(this.id)" />
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="TextBukkenMei" runat="server" Style="width: 333px" MaxLength="50" />
                            </td>
                        </tr>
                        <tr>
                            <td class="koumokuMei2">
                                <input type="button" id="ButtonCopyBukkenJyuusyo1" runat="server" class="button_copy"
                                    style="padding-top: 0px; width: 90px; height: 25px;" value="物件住所1" onclick="SetCopyValue(this.id)" />
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="TextBukkenJyuusyo1" runat="server" Style="width: 230px;" MaxLength="32" />
                            </td>
                            <td class="koumokuMei2">
                                <input type="button" id="ButtonCopyBukkenJyuusyo2" runat="server" class="button_copy"
                                    style="padding-top: 0px; width: 90px; height: 25px;" value="物件住所2" onclick="SetCopyValue(this.id)" />
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="TextBukkenJyuusyo2" runat="server" Style="width: 230px;" MaxLength="32" />
                            </td>
                        </tr>
                        <tr>
                            <td class="koumokuMei2">
                                <input type="button" id="ButtonCopyBukkenJyuusyo3" runat="server" class="button_copy"
                                    style="padding-top: 0px; width: 90px; height: 25px;" value="物件住所3" onclick="SetCopyValue(this.id)" />
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="TextBukkenJyuusyo3" runat="server" Style="width: 333px" MaxLength="54" />
                            </td>
                            <td class="koumokuMei2">
                                <input type="button" id="ButtonCopyKeiyu" runat="server" class="button_copy" style="padding-top: 0px;
                                    width: 90px; height: 25px;" value="経由" onclick="SetCopyValue(this.id)" />
                            </td>
                            <td colspan="3">
                                <asp:DropDownList ID="SelectKeiyu" runat="server">
                                </asp:DropDownList><span id="SpanKeiyu" runat="server"></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="koumokuMei2">
                                <input type="button" id="ButtonCopyBikou" runat="server" class="button_copy" style="padding-top: 2px;
                                    width: 90px; height: 25px;" value="備考" onclick="SetCopyValue(this.id)" />
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="TextBikou" runat="server" Style="width: 333px; font-family: Sans-Serif;
                                    ime-mode: active;" MaxLength="256" />
                            </td>
                            <td class="koumokuMei2">
                                <input type="button" id="ButtonCopyBunjou" runat="server" class="button_copy" style="padding-top: 0px;
                                    width: 90px; height: 25px;" value="分譲コード" onclick="SetCopyValue(this.id)" />
                            </td>
                            <td>
                                <asp:TextBox ID="TextBunjouCode" CssClass="number" runat="server" Style="width: 90px;
                                    ime-mode: diabled;" MaxLength="10" />
                            </td>
                            <td class="koumokuMei2">
                                <input type="button" id="ButtonCopyNayose" runat="server" class="button_copy" style="padding-top: 0px;
                                    width: 90px; height: 25px;" value="物件名寄コード" onclick="SetCopyValue(this.id)" />
                            </td>
                            <td>
                                <asp:TextBox ID="TextNayoseCode" CssClass="codeNumber" runat="server" Style="width: 90px;
                                    ime-mode: diabled;" MaxLength="11" />
                            </td>
                        </tr>
                        <tr>
                            <td class="koumokuMei2">
                                <input type="button" id="ButtonCopyTyousaKibouDate" runat="server" class="button_copy"
                                    style="padding-top: 2px; width: 90px; height: 25px;" value="調査希望日" onclick="SetCopyValue(this.id)" />
                            </td>
                            <td colspan="3" id="TdTyousaKibouDate" runat="server">
                                <asp:TextBox ID="TextTyousaKibouDate" runat="server" Style="width: 70px" CssClass="date"
                                    MaxLength="10" />
                            </td>
                            <td class="koumokuMei2" colspan="1">
                                <input type="button" id="ButtonCopyTyousaKibouJikan" runat="server" class="button_copy"
                                    style="padding-top: 2px; width: 90px; height: 25px;" value="調査希望時間" onclick="SetCopyValue(this.id)" />
                            </td>
                            <td colspan="3" id="TdTyousaKibouJikan" runat="server">
                                <asp:TextBox ID="TextTyousaKibouJikan" runat="server" Style="width: 280px;" MaxLength="26" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td class="tableSpacer">
                </td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" style="border-bottom: solid 2px gray;">
            <tr>
                <td>
                    <!-- 画面中央部 -->
                    <table style="text-align: left; width: 930px; border-bottom-width:0px; table-layout: fixed;"
                        id="Table1" class="mainTable" cellpadding="0" cellspacing="0">
                        <!-- ヘッダ部 -->
                        <tr>
                            <td class="narrow" colspan="3">
                                顧客番号</td>
                            <td class="narrow"colspan="11">
                                施主名</td>
                            <td class="narrow"colspan="12">
                                受注物件名</td>
                        </tr>
                        <tr>
                            <td class="narrow" style="width:10px; border-bottom-width:0px;" />
                            <td class="narrow" colspan="6">
                                物件住所1</td>
                            <td class="narrow" colspan="7">
                                物件住所2</td>
                            <td class="narrow" colspan="12">
                                物件住所3</td>
                        </tr>
                        <tr>
                            <td class="narrow" style="width:10px; border-bottom-width:0px; border-top-width:0px;" />
                            <td class="narrow" colspan="25">
                                備考</td>
                        </tr>
                        <tr>
                            <td class="narrow" style="width:10px; border-top-width:0px;" />
                            <td class="narrow" colspan="3">
                                調査希望日</td>
                            <td class="narrow" colspan="9">
                                調査希望時間</td>
                            <td class="narrow" colspan="7">
                                経由</td>
                            <td class="narrow" colspan="3">
                                分譲コード</td>
                            <td class="narrow" colspan="3">
                                物件名寄コード</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="height: 270px; overflow-y: scroll; width: 930px; border-top: none;">
                        <table class="mainTable" cellpadding="0" cellspacing="0" style="width: 930px; table-layout: fixed;">
                            <!-- データ部 -->
                            <tbody id="tblMeisai" runat="server">
                            </tbody>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

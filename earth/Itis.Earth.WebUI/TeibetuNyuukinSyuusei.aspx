<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="TeibetuNyuukinSyuusei.aspx.vb" Inherits="Itis.Earth.WebUI.TeibetuNyuukinSyuusei"
    Title="EARTH 邸別入金データ修正" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script type="text/javascript">
            //onload後処理
        function funcAfterOnload(){
        
            //登録完了時に次の物件へ遷移するポップアップを表示する
            if(objEBI("<%=callModalFlg.clientID %>").value == "<%=Boolean.TrueString %>"){
                objEBI("<%=callModalFlg.clientID %>").value = ""
                rtnArg = callModalBukken("<%=UrlConst.POPUP_BUKKEN_SITEI %>","<%=UrlConst.TEIBETU_NYUUKIN_SYUUSEI %>","teinyuuA",true,"<%=TextKubun.Text %>","<%=TextBangou.Text %>");
                if(rtnArg == "null" && window.name != "<%=EarthConst.MAIN_WINDOW_NAME %>"){
                    window.close();
                }
            }
        
        }
        
    </script>

    <!-- 画面上部・ヘッダ -->
    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tbody>
            <tr>
                <th style="text-align: left">
                    邸別入金データ修正
                </th>
                <th style="text-align: left;">
                    <input type="button" id="ButtonTourokuSyuuseiJikkou1" value="登録 / 修正 実行" style="font-weight: bold;
                        font-size: 18px; width: 155px; color: black; height: 30px; background-color: fuchsia;"
                        runat="server" />
                    <input id="ButtonTourokuExe" runat="server" style="display: none" type="button" value="登録 / 修正 実行_実行" />
                </th>
                <th style="text-align: right; font-size: 11px;">
                    最終更新者：<asp:TextBox ID="TextSaisyuuKousinnsya" CssClass="readOnlyStyle" Style="width: 120px"
                        ReadOnly="true" TabIndex="-1" runat="server" /><br />
                    最終更新日時：<asp:TextBox ID="TextSaisyuuKousinNitiji" CssClass="readOnlyStyle" Style="width: 100px"
                        ReadOnly="true" TabIndex="-1" runat="server" />
                </th>
            </tr>
            <tr>
                <td colspan="2" rowspan="1" style="height: 10px">
                </td>
                <td colspan="1" rowspan="1" style="height: 10px">
                </td>
            </tr>
        </tbody>
    </table>
    <!-- 1行目[Table1] -->
    <table style="text-align: left; width: 100%;" id="Table1" class="mainTable" cellpadding="0"
        cellspacing="0">
        <tr>
            <td style="width: 60px" class="koumokuMei">
                区分
            </td>
            <td>
                <asp:TextBox ID="TextKubun" Style="width: 80px;" CssClass="readOnlyStyle2" ReadOnly="true"
                    TabIndex="-1" Text="" runat="server" />
            </td>
            <td style="width: 60px" class="koumokuMei">
                番号
            </td>
            <td style="border-right: none;">
                <asp:TextBox ID="TextBangou" Style="width: 80px;" CssClass="readOnlyStyle2" ReadOnly="true"
                    TabIndex="-1" Text="" runat="server" />
                <input type="hidden" id="callModalFlg" runat="server" />
                <asp:HiddenField ID="HiddenNextNo" runat="server" />
            </td>
            <td style="text-align: right; border-left: none;">
                <asp:HiddenField ID="HiddenUpdDatetime" runat="server" />
                <input type="button" id="ButtonTokubetuTaiou" runat="server" value="特別対応" class="" />
                <input type="button" id="ButtonBukkenRireki" runat="server" value="物件履歴" />
                <input id="ButtonHosyousyoDB" class="openHosyousyoDB" type="button" value="保証書DB"
                    runat="server" />
            </td>
        </tr>
    </table>
    <br />
    <!-- 2行目[Table2] -->
    <table style="text-align: left; width: 100%;" id="Table2" class="mainTable" cellpadding="0"
        cellspacing="0">
        <!-- 1行目 -->
        <tr>
            <td style="width: 60px" class="koumokuMei">
                施主名
            </td>
            <td>
                <asp:TextBox ID="TextSetunusiMei" ReadOnly="true" TabIndex="-1" CssClass="readOnlyStyle2"
                    Text="" Style="width: 23em" runat="server" />
            </td>
            <td class="koumokuMei">
                加盟店
            </td>
            <td>
                <asp:TextBox ID="TextKameitenCd" Style="width: 3em;" CssClass="readOnlyStyle2" ReadOnly="true"
                    TabIndex="-1" Text="" runat="server" />
                <asp:TextBox ID="TextKameitenMei" Style="width: 14em;" CssClass="readOnlyStyle2"
                    ReadOnly="true" TabIndex="-1" Text="" runat="server" />
                <input id="ButtonKameitenTyuuijouhou" class="btnKameitenTyuuijouhou" type="button"
                    value="注意情報" runat="server" />
            </td>
            <td class="koumokuMei">
                取消</td>
            <td id="TdTorikesiRiyuu">
                <asp:TextBox ID="TextTorikesiRiyuu" runat="server" Style="width: 8em"/> 
            </td>
        </tr>
        <!-- 2行目 -->
        <tr>
            <td rowspan="2" class="koumokuMei">
                物件住所
            </td>
            <td rowspan="2">
                １：<asp:TextBox ID="TextBukkenJyuusyo1" ReadOnly="true" TabIndex="-1" CssClass="readOnlyStyle2"
                    Text="" Style="width: 17em" runat="server" /><br />
                ２：<asp:TextBox ID="TextBukkenJyuusyo2" ReadOnly="true" TabIndex="-1" CssClass="readOnlyStyle2"
                    Text="" Style="width: 17em" runat="server" /><br />
                ３：<asp:TextBox ID="TextBukkenJyuusyo3" ReadOnly="true" TabIndex="-1" CssClass="readOnlyStyle2"
                    Text="" Style="width: 23em" runat="server" />
            </td>
            <td class="koumokuMei">
                系列
            </td>
            <td colspan="3">
                <asp:TextBox ID="TextKeiretuCd" CssClass="readOnlyStyle2" ReadOnly="true" TabIndex="-1"
                    Text="" Style="width: 3em;" runat="server" />
                <asp:TextBox ID="TextKeiretuMei" CssClass="readOnlyStyle2" ReadOnly="true" TabIndex="-1"
                    Text="" Style="width: 20em" runat="server" />
            </td>
        </tr>
        <!-- 3行目 -->
        <tr>
            <td class="koumokuMei">
                営業所
            </td>
            <td colspan="3">
                <asp:TextBox ID="TextEigyousyoCd" CssClass="readOnlyStyle2" ReadOnly="true" TabIndex="-1"
                    Text="" Style="width: 3em;" runat="server" />
                <asp:TextBox ID="TextEigyousyoMei" CssClass="readOnlyStyle2" ReadOnly="true" TabIndex="-1"
                    Text="" Style="width: 20em" runat="server" />
            </td>
        </tr>
    </table>
    <br />
    <!-- 3行目[Table3] -->
    <table style="text-align: left; width: 100%;" id="Table3" class="mainTable itemTable"
        cellpadding="0" cellspacing="0">
        <!-- 1行目 -->
        <tr class="shouhinTableTitle">
            <td style="width: 150px">
            </td>
            <td style="width: 100px">
                商品コード
            </td>
            <td>
                商品名
            </td>
            <td>
                請求金額
            </td>
            <td>
                入金金額
            </td>
            <td>
                返金額
            </td>
            <td>
                残額
            </td>
        </tr>
        <%-- ************************* --%>
        <%-- 解約払戻以外の商品        --%>
        <%-- ************************* --%>
        <tbody id="tblMeisai" runat="server">
        </tbody>
        <%-- ***************************** --%>
        <%-- 解約払戻                      --%>
        <%-- ***************************** --%>
        <tr id="TrKaiyakuHeader" runat="server">
            <td colspan="5" rowspan="2">
                <asp:HiddenField ID="HiddenKeiriGyoumuKengen" runat="server" />
            </td>
            <td style="white-space: nowrap;" class="koumokuMei">
                解約払戻返金
            </td>
            <td style="width: 80px" class="koumokuMei">
                返金処理日
            </td>
        </tr>
        <tr id="TrKaiyakuMeisai" runat="server">
            <td style="text-align: center;">
                <asp:UpdatePanel ID="UpdatePanelKaiyakuCheck" runat="server" RenderMode="Inline"
                    UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:CheckBox ID="CheckboxKaiyakuHaraimodosikin" runat="server" AutoPostBack="True" />
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanelKaiyaku" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:TextBox ID="TextHenkinSyoriDate" CssClass="date" MaxLength="10" Text="" runat="server" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="CheckboxKaiyakuHaraimodosikin" EventName="CheckedChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <br />
    <!-- 画面下部・ボタン -->
    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tbody>
            <tr>
                <th style="text-align: center;">
                    <input type="button" id="ButtonTourokuSyuuseiJikkou2" value="登録 / 修正 実行" style="font-weight: bold;
                        font-size: 18px; width: 155px; color: black; height: 30px; background-color: fuchsia;"
                        runat="server" />&nbsp;
                </th>
            </tr>
        </tbody>
    </table>
    <asp:HiddenField ID="HiddenSyouhin2Cnt" runat="server" />
    <asp:HiddenField ID="HiddenSyouhin3Cnt" runat="server" />
    <asp:HiddenField ID="HiddenSyouhin4Cnt" runat="server" />
    <asp:HiddenField ID="HiddenKaiyakuNashiFlg" runat="server" />
    <asp:HiddenField ID="HiddenKaiyakuSyori" runat="server" />
</asp:Content>

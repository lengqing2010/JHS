<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="TeibetuNyuukinSyuusei.aspx.vb" Inherits="Itis.Earth.WebUI.TeibetuNyuukinSyuusei"
    Title="EARTH @Êüàf[^C³" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script type="text/javascript">
            //onloadã
        function funcAfterOnload(){
        
            //o^®¹ÉÌ¨ÖJÚ·é|bvAbvð\¦·é
            if(objEBI("<%=callModalFlg.clientID %>").value == "<%=Boolean.TrueString %>"){
                objEBI("<%=callModalFlg.clientID %>").value = ""
                rtnArg = callModalBukken("<%=UrlConst.POPUP_BUKKEN_SITEI %>","<%=UrlConst.TEIBETU_NYUUKIN_SYUUSEI %>","teinyuuA",true,"<%=TextKubun.Text %>","<%=TextBangou.Text %>");
                if(rtnArg == "null" && window.name != "<%=EarthConst.MAIN_WINDOW_NAME %>"){
                    window.close();
                }
            }
        
        }
        
    </script>

    <!-- æÊãEwb_ -->
    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tbody>
            <tr>
                <th style="text-align: left">
                    @Êüàf[^C³
                </th>
                <th style="text-align: left;">
                    <input type="button" id="ButtonTourokuSyuuseiJikkou1" value="o^ / C³ Às" style="font-weight: bold;
                        font-size: 18px; width: 155px; color: black; height: 30px; background-color: fuchsia;"
                        runat="server" />
                    <input id="ButtonTourokuExe" runat="server" style="display: none" type="button" value="o^ / C³ Às_Às" />
                </th>
                <th style="text-align: right; font-size: 11px;">
                    ÅIXVÒF<asp:TextBox ID="TextSaisyuuKousinnsya" CssClass="readOnlyStyle" Style="width: 120px"
                        ReadOnly="true" TabIndex="-1" runat="server" /><br />
                    ÅIXVúF<asp:TextBox ID="TextSaisyuuKousinNitiji" CssClass="readOnlyStyle" Style="width: 100px"
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
    <!-- 1sÚ[Table1] -->
    <table style="text-align: left; width: 100%;" id="Table1" class="mainTable" cellpadding="0"
        cellspacing="0">
        <tr>
            <td style="width: 60px" class="koumokuMei">
                æª
            </td>
            <td>
                <asp:TextBox ID="TextKubun" Style="width: 80px;" CssClass="readOnlyStyle2" ReadOnly="true"
                    TabIndex="-1" Text="" runat="server" />
            </td>
            <td style="width: 60px" class="koumokuMei">
                Ô
            </td>
            <td style="border-right: none;">
                <asp:TextBox ID="TextBangou" Style="width: 80px;" CssClass="readOnlyStyle2" ReadOnly="true"
                    TabIndex="-1" Text="" runat="server" />
                <input type="hidden" id="callModalFlg" runat="server" />
                <asp:HiddenField ID="HiddenNextNo" runat="server" />
            </td>
            <td style="text-align: right; border-left: none;">
                <asp:HiddenField ID="HiddenUpdDatetime" runat="server" />
                <input type="button" id="ButtonTokubetuTaiou" runat="server" value="ÁÊÎ" class="" />
                <input type="button" id="ButtonBukkenRireki" runat="server" value="¨ð" />
                <input id="ButtonHosyousyoDB" class="openHosyousyoDB" type="button" value="ÛØDB"
                    runat="server" />
            </td>
        </tr>
    </table>
    <br />
    <!-- 2sÚ[Table2] -->
    <table style="text-align: left; width: 100%;" id="Table2" class="mainTable" cellpadding="0"
        cellspacing="0">
        <!-- 1sÚ -->
        <tr>
            <td style="width: 60px" class="koumokuMei">
                {å¼
            </td>
            <td>
                <asp:TextBox ID="TextSetunusiMei" ReadOnly="true" TabIndex="-1" CssClass="readOnlyStyle2"
                    Text="" Style="width: 23em" runat="server" />
            </td>
            <td class="koumokuMei">
                Á¿X
            </td>
            <td>
                <asp:TextBox ID="TextKameitenCd" Style="width: 3em;" CssClass="readOnlyStyle2" ReadOnly="true"
                    TabIndex="-1" Text="" runat="server" />
                <asp:TextBox ID="TextKameitenMei" Style="width: 14em;" CssClass="readOnlyStyle2"
                    ReadOnly="true" TabIndex="-1" Text="" runat="server" />
                <input id="ButtonKameitenTyuuijouhou" class="btnKameitenTyuuijouhou" type="button"
                    value="Óîñ" runat="server" />
            </td>
            <td class="koumokuMei">
                æÁ</td>
            <td id="TdTorikesiRiyuu">
                <asp:TextBox ID="TextTorikesiRiyuu" runat="server" Style="width: 8em"/> 
            </td>
        </tr>
        <!-- 2sÚ -->
        <tr>
            <td rowspan="2" class="koumokuMei">
                ¨Z
            </td>
            <td rowspan="2">
                PF<asp:TextBox ID="TextBukkenJyuusyo1" ReadOnly="true" TabIndex="-1" CssClass="readOnlyStyle2"
                    Text="" Style="width: 17em" runat="server" /><br />
                QF<asp:TextBox ID="TextBukkenJyuusyo2" ReadOnly="true" TabIndex="-1" CssClass="readOnlyStyle2"
                    Text="" Style="width: 17em" runat="server" /><br />
                RF<asp:TextBox ID="TextBukkenJyuusyo3" ReadOnly="true" TabIndex="-1" CssClass="readOnlyStyle2"
                    Text="" Style="width: 23em" runat="server" />
            </td>
            <td class="koumokuMei">
                nñ
            </td>
            <td colspan="3">
                <asp:TextBox ID="TextKeiretuCd" CssClass="readOnlyStyle2" ReadOnly="true" TabIndex="-1"
                    Text="" Style="width: 3em;" runat="server" />
                <asp:TextBox ID="TextKeiretuMei" CssClass="readOnlyStyle2" ReadOnly="true" TabIndex="-1"
                    Text="" Style="width: 20em" runat="server" />
            </td>
        </tr>
        <!-- 3sÚ -->
        <tr>
            <td class="koumokuMei">
                cÆ
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
    <!-- 3sÚ[Table3] -->
    <table style="text-align: left; width: 100%;" id="Table3" class="mainTable itemTable"
        cellpadding="0" cellspacing="0">
        <!-- 1sÚ -->
        <tr class="shouhinTableTitle">
            <td style="width: 150px">
            </td>
            <td style="width: 100px">
                ¤iR[h
            </td>
            <td>
                ¤i¼
            </td>
            <td>
                ¿àz
            </td>
            <td>
                üààz
            </td>
            <td>
                Ôàz
            </td>
            <td>
                cz
            </td>
        </tr>
        <%-- ************************* --%>
        <%-- ðñ¥ßÈOÌ¤i        --%>
        <%-- ************************* --%>
        <tbody id="tblMeisai" runat="server">
        </tbody>
        <%-- ***************************** --%>
        <%-- ðñ¥ß                      --%>
        <%-- ***************************** --%>
        <tr id="TrKaiyakuHeader" runat="server">
            <td colspan="5" rowspan="2">
                <asp:HiddenField ID="HiddenKeiriGyoumuKengen" runat="server" />
            </td>
            <td style="white-space: nowrap;" class="koumokuMei">
                ðñ¥ßÔà
            </td>
            <td style="width: 80px" class="koumokuMei">
                Ôàú
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
    <!-- æÊºE{^ -->
    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tbody>
            <tr>
                <th style="text-align: center;">
                    <input type="button" id="ButtonTourokuSyuuseiJikkou2" value="o^ / C³ Às" style="font-weight: bold;
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

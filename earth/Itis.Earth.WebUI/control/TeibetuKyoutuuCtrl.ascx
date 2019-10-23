<%@ Control Language="vb" AutoEventWireup="false" Codebehind="TeibetuKyoutuuCtrl.ascx.vb"
    Inherits="Itis.Earth.WebUI.TeibetuKyoutuuCtrl" %>
<table style="text-align: left; width: 100%;" id="Table4" class="mainTable" cellpadding="0"
    cellspacing="0" border="1">
    <thead>
        <tr>
            <th class="tableTitle" colspan="4">
                <a id="KyoutuuDispLink" runat="server">ã§í èÓïÒ</a> <span id="KyoutuuTitleInfobar" style="display: none;"
                    runat="server"></span>
            </th>
            <th class="tableTitle" style="text-align: right;">
                <input type="button" id="ButtonBukkenRireki" runat="server" value="ï®åèóöó" />
                <input type="button" id="ButtonKousinRireki" runat="server" value="çXêVóöó" />
                <input id="ButtonHosyousyoDB" runat="server" class="openHosyousyoDB" type="button"
                    value="ï€èÿèëDB" />
            </th>
        </tr>
    </thead>
    <tbody id="TBodyKyotuInfo" style="display: inline;" runat="server">
        <!-- 1çsñ⁄ -->
        <tr>
            <td class="koumokuMei" style="height: 20px; width: 60px;">
                ãÊï™
            </td>
            <td>
                <input type="text" id="TextKubun" style="width: 80px;" class="readOnlyStyle2" readonly="readonly"
                    tabindex="-1" runat="server" />
            </td>
            <td style="width: 60px" class="koumokuMei">
                î‘çÜ
            </td>
            <td colspan="2">
                <input type="text" id="TextBangou" style="width: 80px;" class="readOnlyStyle2" readonly="readonly"
                    tabindex="-1" size="10" runat="server" />
            </td>
        </tr>
        <!-- 2çsñ⁄ -->
        <tr>
            <td class="koumokuMei" style="width: 80px; height: 20px">
                é{éÂñº
            </td>
            <td colspan="4">
                <input type="text" id="TextSesyuMei" size="50" readonly="readonly" tabindex="-1"
                    class="readOnlyStyle2" runat="server" />
            </td>
        </tr>
        <!-- 3çsñ⁄ -->
        <tr>
            <td class="koumokuMei" style="height: 40px">
                ï®åèèZèä
            </td>
            <td colspan="4">
                &nbsp; ÇPÅF<input type="text" id="TextJyuusyo1" size="32" readonly="readonly" tabindex="-1"
                    class="readOnlyStyle2" runat="server" />
                ÇQÅF<input type="text" id="TextJyuusyo2" size="32" readonly="readonly" tabindex="-1"
                    class="readOnlyStyle2" runat="server" /><br />
                &nbsp; ÇRÅF<input type="text" id="TextJyuusyo3" size="54" readonly="readonly" tabindex="-1"
                    class="readOnlyStyle2" runat="server" />
            </td>
        </tr>
        <!-- 4çsñ⁄ -->
        <tr>
            <td class="koumokuMei">
                îıçl
            </td>
            <td colspan="4" rowspan="1">
                <textarea cols="80" rows="3" id="TextBikou1" runat="server" style="font-family: Sans-Serif;
                    ime-mode: active;" onfocus="this.select();"></textarea><textarea cols="60" rows="4"
                        id="TextBikou2" runat="server" style="display: none; font-family: Sans-Serif;
                        ime-mode: active;" onfocus="this.select();"></textarea>
            </td>
        </tr>
        <tr>
            <td class="tableSpacer" colspan="5">
            </td>
        </tr>
        <!-- 5çsñ⁄ -->
        <tr>
            <td colspan="5" style="padding: 0px;">
                <table class="innerTable" cellpadding="0" cellspacing="0">
                    <tr class="firstRow">
                        <td class="koumokuMei firstCol">
                            í≤ç∏é¿é{ì˙
                        </td>
                        <td>
                            <input type="text" id="TextTyousaJissibi" class="date readOnlyStyle2" maxlength="10"
                                tabindex="-1" readonly="readOnly" runat="server" /></td>
                        <td class="koumokuMei">
                            â¸ó«çHéñ
                        </td>
                        <td style="font-weight: bold;">
                            é¿é{ì˙:<input type="text" id="TextKairyouKoujiJissibi" class="date readOnlyStyle2" maxlength="10"
                                tabindex="-1" readonly="readOnly" runat="server" />
                            äÆçHë¨ïÒíÖì˙:<input type="text" id="TextKairyouKoujiKankou" class="date readOnlyStyle2"
                                maxlength="10" tabindex="-1" readonly="readOnly" runat="server" />
                        </td>
                        <td class="koumokuMei">
                            í«â¡çHéñ
                        </td>
                        <td style="font-weight: bold;">
                            é¿é{ì˙:<input type="text" id="TextTuikaKoujiJissibi" class="date readOnlyStyle2" maxlength="10"
                                tabindex="-1" readonly="readOnly" runat="server" />
                            äÆçHë¨ïÒíÖì˙:<input type="text" id="TextTuikaKoujiKankou" class="date readOnlyStyle2" maxlength="10"
                                tabindex="-1" readonly="readOnly" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="koumokuMei firstCol">
                            âêÕíSìñé“
                        </td>
                        <td colspan="3">
                            <input type="text" id="TextKaisekiTantouCd" maxlength="7" tabindex="-1" style="width: 30px;"
                                class="codeNumber readOnlyStyle2" readonly="readOnly" runat="server" />&nbsp;
                            <input type="text" id="TextKaisekiTantouMei" style="width: 150px" class="readOnlyStyle2"
                                readonly="readonly" tabindex="-1" runat="server" /></td>
                        <td class="koumokuMei" colspan="">
                            çHéñíSìñé“
                        </td>
                        <td colspan="">
                            <input type="text" id="TextKoujiTantouCd" maxlength="7" tabindex="-1" style="width: 30px;"
                                class="codeNumber readOnlyStyle2" readonly="readOnly" runat="server" />&nbsp;
                            <input type="text" id="TextKoujiTantouMei" style="width: 150px" class="readOnlyStyle2"
                                readonly="readonly" tabindex="-1" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="koumokuMei firstCol">
                            îªíË
                        </td>
                        <td colspan="12" style="width: 800px">
                            <span id="SpanHantei1" runat="server" style="width: 150px" class="readOnlyStyle"></span>
                            &nbsp; <span id="SpanHanteiSetuzoku" runat="server" style="width: 70px" class="readOnlyStyle">
                            </span>&nbsp; <span id="SpanHantei2" runat="server" style="width: 150px" class="readOnlyStyle">
                            </span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </tbody>
</table>
<asp:HiddenField ID="HiddenKeiriGyoumuKengen" runat="server" />
<asp:HiddenField runat="server" ID="HiddenOpenValue" />
<asp:HiddenField runat="server" ID="HiddenKeyValue" />
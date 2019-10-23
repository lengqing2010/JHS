<%@ Control Language="vb" AutoEventWireup="false" Codebehind="TeibetuKyoutuuCtrl.ascx.vb"
    Inherits="Itis.Earth.WebUI.TeibetuKyoutuuCtrl" %>
<table style="text-align: left; width: 100%;" id="Table4" class="mainTable" cellpadding="0"
    cellspacing="0" border="1">
    <thead>
        <tr>
            <th class="tableTitle" colspan="4">
                <a id="KyoutuuDispLink" runat="server">���ʏ��</a> <span id="KyoutuuTitleInfobar" style="display: none;"
                    runat="server"></span>
            </th>
            <th class="tableTitle" style="text-align: right;">
                <input type="button" id="ButtonBukkenRireki" runat="server" value="��������" />
                <input type="button" id="ButtonKousinRireki" runat="server" value="�X�V����" />
                <input id="ButtonHosyousyoDB" runat="server" class="openHosyousyoDB" type="button"
                    value="�ۏ؏�DB" />
            </th>
        </tr>
    </thead>
    <tbody id="TBodyKyotuInfo" style="display: inline;" runat="server">
        <!-- 1�s�� -->
        <tr>
            <td class="koumokuMei" style="height: 20px; width: 60px;">
                �敪
            </td>
            <td>
                <input type="text" id="TextKubun" style="width: 80px;" class="readOnlyStyle2" readonly="readonly"
                    tabindex="-1" runat="server" />
            </td>
            <td style="width: 60px" class="koumokuMei">
                �ԍ�
            </td>
            <td colspan="2">
                <input type="text" id="TextBangou" style="width: 80px;" class="readOnlyStyle2" readonly="readonly"
                    tabindex="-1" size="10" runat="server" />
            </td>
        </tr>
        <!-- 2�s�� -->
        <tr>
            <td class="koumokuMei" style="width: 80px; height: 20px">
                �{�喼
            </td>
            <td colspan="4">
                <input type="text" id="TextSesyuMei" size="50" readonly="readonly" tabindex="-1"
                    class="readOnlyStyle2" runat="server" />
            </td>
        </tr>
        <!-- 3�s�� -->
        <tr>
            <td class="koumokuMei" style="height: 40px">
                �����Z��
            </td>
            <td colspan="4">
                &nbsp; �P�F<input type="text" id="TextJyuusyo1" size="32" readonly="readonly" tabindex="-1"
                    class="readOnlyStyle2" runat="server" />
                �Q�F<input type="text" id="TextJyuusyo2" size="32" readonly="readonly" tabindex="-1"
                    class="readOnlyStyle2" runat="server" /><br />
                &nbsp; �R�F<input type="text" id="TextJyuusyo3" size="54" readonly="readonly" tabindex="-1"
                    class="readOnlyStyle2" runat="server" />
            </td>
        </tr>
        <!-- 4�s�� -->
        <tr>
            <td class="koumokuMei">
                ���l
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
        <!-- 5�s�� -->
        <tr>
            <td colspan="5" style="padding: 0px;">
                <table class="innerTable" cellpadding="0" cellspacing="0">
                    <tr class="firstRow">
                        <td class="koumokuMei firstCol">
                            �������{��
                        </td>
                        <td>
                            <input type="text" id="TextTyousaJissibi" class="date readOnlyStyle2" maxlength="10"
                                tabindex="-1" readonly="readOnly" runat="server" /></td>
                        <td class="koumokuMei">
                            ���ǍH��
                        </td>
                        <td style="font-weight: bold;">
                            ���{��:<input type="text" id="TextKairyouKoujiJissibi" class="date readOnlyStyle2" maxlength="10"
                                tabindex="-1" readonly="readOnly" runat="server" />
                            ���H���񒅓�:<input type="text" id="TextKairyouKoujiKankou" class="date readOnlyStyle2"
                                maxlength="10" tabindex="-1" readonly="readOnly" runat="server" />
                        </td>
                        <td class="koumokuMei">
                            �ǉ��H��
                        </td>
                        <td style="font-weight: bold;">
                            ���{��:<input type="text" id="TextTuikaKoujiJissibi" class="date readOnlyStyle2" maxlength="10"
                                tabindex="-1" readonly="readOnly" runat="server" />
                            ���H���񒅓�:<input type="text" id="TextTuikaKoujiKankou" class="date readOnlyStyle2" maxlength="10"
                                tabindex="-1" readonly="readOnly" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="koumokuMei firstCol">
                            ��͒S����
                        </td>
                        <td colspan="3">
                            <input type="text" id="TextKaisekiTantouCd" maxlength="7" tabindex="-1" style="width: 30px;"
                                class="codeNumber readOnlyStyle2" readonly="readOnly" runat="server" />&nbsp;
                            <input type="text" id="TextKaisekiTantouMei" style="width: 150px" class="readOnlyStyle2"
                                readonly="readonly" tabindex="-1" runat="server" /></td>
                        <td class="koumokuMei" colspan="">
                            �H���S����
                        </td>
                        <td colspan="">
                            <input type="text" id="TextKoujiTantouCd" maxlength="7" tabindex="-1" style="width: 30px;"
                                class="codeNumber readOnlyStyle2" readonly="readOnly" runat="server" />&nbsp;
                            <input type="text" id="TextKoujiTantouMei" style="width: 150px" class="readOnlyStyle2"
                                readonly="readonly" tabindex="-1" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="koumokuMei firstCol">
                            ����
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
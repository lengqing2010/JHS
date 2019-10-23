<%@ Control Language="vb" AutoEventWireup="false" Codebehind="BukkenRirekiRecordCtrl.ascx.vb"
    Inherits="Itis.Earth.WebUI.BukkenRirekiRecordCtrl" %>
<tr id="Tr1" runat="server">
    <td style="width: 200px;word-break:break-all;">
        <input type="hidden" id="HiddenKbn" runat="server" />
        <input type="hidden" id="HiddenBangou" runat="server" />
        <input type="hidden" id="HiddenUpdDatetime" runat="server" />
        <input type="hidden" id="HiddenNyuuryokuNo" runat="server" />
        <input type="hidden" id="HiddenLoginUser" runat="server" />
        <asp:DropDownList runat="server" ID="SelectSyubetu" Style="display: none;">
        </asp:DropDownList><span id="SpanSyubetu" runat="server"></span>
        <input type="hidden" id="HiddenSyubetu" runat="server" />
    </td>
    <td style="width: 300px;word-break:break-all;" colspan="2" id="TdCode" runat="server">
        <select id="SelectBunrui" style="width: px; display:none;">
        </select><span id="SpanBunrui" runat="server"></span>
        <input type="hidden" id="HiddenBunrui" runat="server" />
    </td>
    <td style="width: 350px;word-break:break-all;" rowspan="2">
        <span id="SpanNaiyou" runat="server"></span>
    </td>
    <td style="width: 50px;" rowspan="2">
        <input type="button" id="ButtonSyuusei" value="C³" style="width: 22px; height: 45px;
            writing-mode: tb-rl;" runat="server" />
        <input type="button" id="ButtonTorikesi" value="ŽæÁ" style="width: 22px; height: 45px;
            writing-mode: tb-rl;" runat="server" />
    </td>
</tr>
<tr id="Tr2" runat="server">
    <td>
        <span id="SpanTorikesi" runat="server" style="font-weight: bold; color: Red;"></span><span id="SpanHenkouFukaFlg" runat="server" style="display: none"></span>
    </td>
    <td style="width: 93px;">
        <span id="SpanHizuke" runat="server"></span>
    </td>
    <td style="width: 200px;">
        <span id="SpanHanyouCode" runat="server"></span>
    </td>
</tr>

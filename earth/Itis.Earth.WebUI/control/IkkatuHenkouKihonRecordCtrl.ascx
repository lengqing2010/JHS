<%@ Control Language="vb" AutoEventWireup="false" Codebehind="IkkatuHenkouKihonRecordCtrl.ascx.vb"
    Inherits="Itis.Earth.WebUI.IkkatuHenkouKihonRecordCtrl" %>
<tr id="TrIkkatuHenkouRec1" runat="server">
    <%--�ڋq�ԍ�--%>
    <td colspan="3">
        <asp:TextBox ID="TextKokyakuBangou" runat="server" Style="width: 80px" CssClass="readOnlyStyle"
            ReadOnly="true" TabIndex="-1" />
        <input type="hidden" id="HiddenKbn" runat="server" />
        <input type="hidden" id="HiddenNo" runat="server" />
        <input type="hidden" id="updateDateTime" runat="server" />
    </td>
    <%--�{�喼--%>
    <td colspan="11">
        <asp:TextBox ID="TextSesyuMei" runat="server" Style="width: 370px" MaxLength="50"
            CssClass="hissu" />
    </td>
    <%--�󒍕�����--%>
    <td colspan="12">
        <asp:TextBox ID="TextBukkenMei" runat="server" Style="width: 415px" MaxLength="50" CssClass="hissu" />
    </td>
</tr>
<tr id="TrIkkatuHenkouRec2" runat="server">
<td style="border-bottom-width:0px;" />
    <%--�����Z��1--%>
    <td colspan="6">
        <asp:TextBox ID="TextBukkenJyuusyo1" runat="server" Style="width: 195px" CssClass="hissu"
            MaxLength="32" />
    </td>
    <%--�����Z��2--%>
    <td colspan="7">
        <asp:TextBox ID="TextBukkenJyuusyo2" runat="server" Style="width: 230px" MaxLength="32" />
    </td>
    <%--�����Z��3--%>
    <td colspan="12">
        <asp:TextBox ID="TextBukkenJyuusyo3" runat="server" Style="width: 415px" MaxLength="54" />
    </td>
</tr>
<tr id="TrIkkatuHenkouRec3" runat="server">
    <td style="border-bottom-width:0px; border-top-width:0px;" />
    <%--���l--%>
    <td colspan="25">
        <asp:TextBox ID="TextBikou" runat="server" Style="width: 874px; font-family: Sans-Serif;
            ime-mode: active;" MaxLength="256" />
    </td>
</tr>
<tr id="TrIkkatuHenkouRec4" runat="server">
    <td style="border-top-width:0px;" />
    <%--������]��--%>
    <td id="TdTyousaKibouDate" runat="server" colspan="3">
        <asp:TextBox ID="TextTyousaKibouDate" runat="server" Style="width: 70px" CssClass="date hissu"
            MaxLength="10" />
    </td>
    <%--������]����--%>
    <td id="TdTyousaKibouJikan" runat="server" colspan="9">
        <asp:TextBox ID="TextTyousaKibouJikan" runat="server" Style="width: 280px" MaxLength="26" />
    </td>
    <%--�o�R--%>
    <td colspan="7">
        <asp:DropDownList ID="SelectKeiyu" runat="server">
        </asp:DropDownList><span id="SpanKeiyu" runat="server"></span>
    </td>
    <%--�����R�[�h--%>
    <td colspan="3">
        <asp:TextBox ID="TextBunjouCode" runat="server" CssClass="number" Style="width: 90px;
            font-family: Sans-Serif; ime-mode: diabled;" MaxLength="10" />
    </td>
    <%--��������R�[�h--%>
    <td colspan="3">
        <asp:TextBox ID="TextNayoseCode" runat="server" CssClass="codeNumber" Style="width: 90px;
            font-family: Sans-Serif; ime-mode: diabled;" MaxLength="11" />
    </td>
</tr>

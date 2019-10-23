<%@ Control Language="vb" AutoEventWireup="false" Codebehind="TeibetuNyuukinRecordCtrl.ascx.vb"
    Inherits="Itis.Earth.WebUI.TeibetuNyuukinRecordCtrl" %>
<tr runat="server" id="trRecord">
    <td>
        <asp:TextBox ID="TextSyouhinCd" Style="width: 50px" CssClass="itemCd readOnlyStyle"
            TabIndex="-1" Text="" runat="server" />
    </td>
    <td>
        <asp:TextBox ID="TextSyouhinMei" Style="width: 250px" CssClass="itemNm readOnlyStyle"
            TabIndex="-1" Text="" runat="server" />
    </td>
    <td>
        <asp:TextBox ID="TextSeikyuuKingaku" CssClass="kingaku readOnlyStyle" MaxLength="7"
            Text="" Style="width: 60px" TabIndex="-1" runat="server" />
    </td>
    <td>
        <asp:TextBox ID="TextNyuukinKingaku" CssClass="kingaku" MaxLength="7" Text="" Style="width: 60px"
            runat="server" />
    </td>
    <td>
        <asp:TextBox ID="TextHenkingaku" CssClass="kingaku" MaxLength="7" Text="" Style="width: 60px"
            runat="server" />
    </td>
    <td>
        <asp:UpdatePanel ID="UpdatePanelSyouhin" runat="server" RenderMode="Inline" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:TextBox ID="TextZangaku" CssClass="kingaku readOnlyStyle" MaxLength="7" Text=""
                    Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="TextNyuukinKingaku" EventName="TextChanged" />
                <asp:AsyncPostBackTrigger ControlID="TextHenkingaku" EventName="TextChanged" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:HiddenField ID="HiddenBunruiCd" runat="server" />
        <asp:HiddenField ID="HiddenGamenHyoujiNo" runat="server" />
        <asp:HiddenField ID="HiddenUpdDatetime" runat="server" />
    </td>
</tr>

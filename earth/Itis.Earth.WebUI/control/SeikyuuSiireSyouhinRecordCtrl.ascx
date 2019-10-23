<%@ Control Language="vb" AutoEventWireup="false" Codebehind="SeikyuuSiireSyouhinRecordCtrl.ascx.vb"
    Inherits="Itis.Earth.WebUI.SeikyuuSiireSyouhinRecordCtrl" %>
<table class="innerTable itemTableNarrow" cellpadding="0" cellspacing="0" style="width: 100%;
    height: 100%; border-top: 1px; table-layout: fixed;" id="mainTable" runat="server">
    <tr id="trSeikyuu" runat="server" style="height: 30px;">
        <td id="TdSyouhinCdTitle" style="width: 80px; border-top: 1px; border-left: 0px;
            text-align: center;" runat="server">
            <asp:TextBox ID="TextSyouhinCd" runat="server" CssClass="readOnlyStyle" MaxLength="8"
                Style="width: 60px;" TabIndex="-1"></asp:TextBox></td>
        <td class="itemMei_small" style="border-top: 1px; width: 65px; text-align: center;">
            <asp:TextBox ID="TextBunruiCd" runat="server" CssClass="readOnlyStyle" MaxLength="3"
                Style="width: 30px;" TabIndex="-1"></asp:TextBox></td>
        <td style="border-top: 1px; width: 60px;">
            <asp:TextBox ID="TextHyoujiNo" runat="server" CssClass="readOnlyStyle codeNumber"
                MaxLength="2" Style="width: 20px;" TabIndex="-1"></asp:TextBox></td>
        <td style="border-top: 1px; width: 672px; text-align: left; padding-left: 5px;">
            <input type="button" id="ButtonSetDefSeikyuu" runat="server" class="button_copy"
                style="height: 22px; width: 90px;" value="基本をセット" />
            <asp:UpdatePanel ID="UpdatePanelSeikyuuSaki" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:DropDownList ID="SelectSeikyuuSaki" runat="server" Style="width: 80px;">
                    </asp:DropDownList><span id="SpanSeikyuuSakiKbn" runat="server"></span>
                    <asp:TextBox ID="TextSeikyuuSakiCd" runat="server" Style="width: 40px;" MaxLength="5"
                        CssClass="codeNumber">
                    </asp:TextBox>
                    －
                    <asp:TextBox ID="TextSeikyuuSakiBrc" runat="server" Style="width: 15px;" MaxLength="2"
                        CssClass="codeNumber">
                    </asp:TextBox>
                    <input type="button" id="ButtonSeikyuuSaki" runat="server" value="検索" />
                    <asp:TextBox ID="TextSeikyuuSakiMei" runat="server" CssClass="readOnlyStyle" Style="width: 340px;
                        border-bottom: 1px solid black;" TabIndex="-1">
                    </asp:TextBox>
                    <asp:HiddenField ID="HiddenSeikyuuSakiCdOld" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <!-- 2行目 -->
    <tr id="trSiire" runat="server" style="height: 30px;">
        <td style="border-left: 0px;" colspan="3">
            <asp:TextBox ID="TextSyouhinMei" runat="server" CssClass="readOnlyStyle" MaxLength="2"
                Style="width: 250px;" TabIndex="-1"></asp:TextBox></td>
        <td style="width: 672px; text-align: left; padding-left: 5px;">
            <input type="button" id="ButtonSetDefSiire" runat="server" class="button_copy" style="height: 22px;
                width: 90px;" value="基本をセット" />
            <asp:UpdatePanel ID="UpdatePanelSiireSaki" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:TextBox ID="TextSiireSakiCd" runat="server" Style="width: 40px;" MaxLength="5"
                        CssClass="codeNumber"></asp:TextBox>
                    －
                    <asp:TextBox ID="TextSiireSakiBrc" runat="server" Style="width: 15px;" MaxLength="2"
                        CssClass="codeNumber"></asp:TextBox>
                    <input type="button" id="ButtonSiireSaki" runat="server" value="検索" />
                    <asp:TextBox ID="TextSiireSakiMei" runat="server" CssClass="readOnlyStyle" Style="width: 424px;
                        border-bottom: 1px solid black;" TabIndex="-1"></asp:TextBox>
                    <asp:HiddenField ID="HiddenSiireSakiCdOld" runat="server" />
                    <asp:HiddenField ID="HiddenSiireSakiCdNew" runat="server" />
                    <asp:HiddenField ID="HiddenTysKensakuType" runat="server" />
                    <asp:HiddenField ID="HiddenKakushuNG" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
<asp:HiddenField ID="HiddenKameitenCd" runat="server" />
<asp:HiddenField ID="HiddenKojKaisyaSeikyuuFlg" runat="server" />
<asp:HiddenField ID="HiddenKojKaisyaCd" runat="server" />
<asp:HiddenField ID="HiddenDefaultSeikyuuSakiCd" runat="server" />
<asp:HiddenField ID="HiddenDefaultSeikyuuSakiBrc" runat="server" />
<asp:HiddenField ID="HiddenDefaultSeikyuuSakiKbn" runat="server" />
<asp:HiddenField ID="HiddenDefaultSeikyuuSakiMei" runat="server" />
<asp:HiddenField ID="HiddenDefaultSiireSakiCd" runat="server" />
<asp:HiddenField ID="HiddenDefaultSiireSakiBrc" runat="server" />
<asp:HiddenField ID="HiddenDefaultSiireSakiMei" runat="server" />
<asp:HiddenField ID="HiddenUpdDateTime" runat="server" />

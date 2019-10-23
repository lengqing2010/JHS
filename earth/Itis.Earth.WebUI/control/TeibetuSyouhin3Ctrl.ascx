<%@ Control Language="vb" AutoEventWireup="false" Codebehind="TeibetuSyouhin3Ctrl.ascx.vb"
    Inherits="Itis.Earth.WebUI.TeibetuSyouhin3Ctrl" %>
<%@ Register Src="TeibetuSyouhinHeaderCtrl.ascx" TagName="TeibetuSyouhinHeaderCtrl"
    TagPrefix="uc1" %>
<%@ Register Src="TeibetuSyouhinRecordCtrl.ascx" TagName="TeibetuSyouhinRecordCtrl"
    TagPrefix="uc2" %>
<table class="innerTable" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%;
    border-top: 0px; border-left: 0px;">
    <tr>
        <td style="padding: 0px; border-top: 0px; border-left: 0px;">
            <!-- 商品コード3 -->
            <uc1:TeibetuSyouhinHeaderCtrl ID="Syouhin3Header" DispMode="SYOUHIN3" runat="server" />
        </td>
    </tr>
    <tr id="trSyouhin3Record01" runat="server">
        <td style="padding: 0px; border-left: 0px;">
            <!-- 明細レコード 1～9 -->
            <uc2:TeibetuSyouhinRecordCtrl ID="Syouhin3Record01" DispMode="SYOUHIN3" CssName="odd"
                IsRowSpacer="true" runat="server" />
        </td>
    </tr>
    <tr id="trSyouhin3Record02" runat="server">
        <td style="padding: 0px; border-left: 0px;">
            <uc2:TeibetuSyouhinRecordCtrl ID="Syouhin3Record02" DispMode="SYOUHIN3" CssName="even"
                IsRowSpacer="true" runat="server" />
        </td>
    </tr>
    <tr id="trSyouhin3Record03" runat="server">
        <td style="padding: 0px; border-left: 0px;">
            <uc2:TeibetuSyouhinRecordCtrl ID="Syouhin3Record03" DispMode="SYOUHIN3" CssName="odd"
                IsRowSpacer="true" runat="server" />
        </td>
    </tr>
    <tr id="trSyouhin3Record04" runat="server">
        <td style="padding: 0px; border-left: 0px;">
            <uc2:TeibetuSyouhinRecordCtrl ID="Syouhin3Record04" DispMode="SYOUHIN3" CssName="even"
                IsRowSpacer="true" runat="server" />
        </td>
    </tr>
    <tr id="trSyouhin3Record05" runat="server">
        <td style="padding: 0px; border-left: 0px;">
            <uc2:TeibetuSyouhinRecordCtrl ID="Syouhin3Record05" DispMode="SYOUHIN3" CssName="odd"
                IsRowSpacer="true" runat="server" />
        </td>
    </tr>
    <tr id="trSyouhin3Record06" runat="server">
        <td style="padding: 0px; border-left: 0px;">
            <uc2:TeibetuSyouhinRecordCtrl ID="Syouhin3Record06" DispMode="SYOUHIN3" CssName="even"
                IsRowSpacer="true" runat="server" />
        </td>
    </tr>
    <tr id="trSyouhin3Record07" runat="server">
        <td style="padding: 0px; border-left: 0px;">
            <uc2:TeibetuSyouhinRecordCtrl ID="Syouhin3Record07" DispMode="SYOUHIN3" CssName="odd"
                IsRowSpacer="true" runat="server" />
        </td>
    </tr>
    <tr id="trSyouhin3Record08" runat="server">
        <td style="padding: 0px; border-left: 0px;">
            <uc2:TeibetuSyouhinRecordCtrl ID="Syouhin3Record08" DispMode="SYOUHIN3" CssName="even"
                IsRowSpacer="true" runat="server" />
        </td>
    </tr>
    <tr id="trSyouhin3Record09" runat="server">
        <td style="padding: 0px; border-left: 0px;">
            <uc2:TeibetuSyouhinRecordCtrl ID="Syouhin3Record09" DispMode="SYOUHIN3" CssName="odd"
                runat="server" />
        </td>
    </tr>
</table>
<asp:HiddenField ID="HiddenKubun" runat="server" />
<asp:HiddenField ID="HiddenBangou" runat="server" />
<asp:HiddenField ID="HiddenHattyuusyoKanriKengen" runat="server" />
<asp:HiddenField ID="HiddenKeiriGyoumuKengen" runat="server" />
<asp:HiddenField ID="HiddenLoginUserId" runat="server" />
<asp:UpdatePanel ID="UpdatePanelIraiInfo" runat="server" RenderMode="Inline" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:HiddenField ID="HiddenSeikyuuType" runat="server" />
        <asp:HiddenField ID="HiddenKeiretuCd" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>

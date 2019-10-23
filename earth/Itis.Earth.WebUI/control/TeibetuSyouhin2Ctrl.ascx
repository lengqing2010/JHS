<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="TeibetuSyouhin2Ctrl.ascx.vb" Inherits="Itis.Earth.WebUI.TeibetuSyouhin2Ctrl" %>
<%@ Register Src="TeibetuSyouhinHeaderCtrl.ascx" TagName="TeibetuSyouhinHeaderCtrl"
    TagPrefix="uc1" %>
<%@ Register Src="TeibetuSyouhinRecordCtrl.ascx" TagName="TeibetuSyouhinRecordCtrl"
    TagPrefix="uc2" %>
                <table class="innerTable"  cellpadding="0" cellspacing="0" style="width:100%; height:100%; border-top:0px; border-left:0px;">
                    <tr>
                        <td style="padding:0px; border-top:0px; border-left:0px;">
                            <!-- ヘッダーレコード -->
                            <uc1:TeibetuSyouhinHeaderCtrl id="Syouhin2Header" DispMode="SYOUHIN2" runat="server"/>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding:0px; border-left:0px;">
                            <!-- 明細レコード 1～4 -->
                            <uc2:TeibetuSyouhinRecordCtrl id="Syouhin2Record01" DispMode="SYOUHIN2" CssName="odd" IsRowSpacer="true" runat="server"/>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding:0px; border-left:0px;">
                            <uc2:TeibetuSyouhinRecordCtrl id="Syouhin2Record02" DispMode="SYOUHIN2" CssName="even" IsRowSpacer="true" runat="server"/>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding:0px; border-left:0px;">
                            <uc2:TeibetuSyouhinRecordCtrl id="Syouhin2Record03" DispMode="SYOUHIN2" CssName="odd" IsRowSpacer="true" runat="server"/>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding:0px; border-left:0px;">
                            <uc2:TeibetuSyouhinRecordCtrl id="Syouhin2Record04" DispMode="SYOUHIN2" CssName="even" runat="server"/>
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
                        </td>
                    </tr>
                </table>
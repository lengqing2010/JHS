<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="NyuukinZangakuCtrl.ascx.vb" Inherits="Itis.Earth.WebUI.NyuukinZangakuCtrl" %>
<asp:UpdatePanel ID="UpdatePanelZangaku" runat="server" RenderMode="Inline" UpdateMode="Conditional">
    <ContentTemplate>
                    <span id="SpanNyuukinTitle" runat="server"> �����z�i�ō��j</span>
                    <input 
                        id="TextNyuukinGaku" 
                        runat="server" 
                        class="kingaku readOnlyStyle2" 
                        maxlength="25"
                        style="width: 120px" 
                        readonly="readOnly" 
                        tabindex="-1" 
                        value="0" />&nbsp;&nbsp; �c�z
                    <input 
                        id="TextZanGaku" 
                        runat="server" 
                        class="kingaku readOnlyStyle2" 
                        maxlength="25"
                        size="25" 
                        style="width: 120px" 
                        readonly="readOnly" 
                        tabindex="-1" 
                        value="0" />
    </ContentTemplate>
</asp:UpdatePanel>
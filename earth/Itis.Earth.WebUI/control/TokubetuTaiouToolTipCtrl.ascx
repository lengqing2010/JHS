<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="TokubetuTaiouToolTipCtrl.ascx.vb"
    Inherits="Itis.Earth.WebUI.TokubetuTaiouToolTipCtrl" %>
<%@ Import Namespace="Itis.Earth.Utilities" %>

<a id ="spanTokubetuTaiou" runat="server"> 
    <asp:Label ID="lblTokubetuTaiou" runat="server" Text="��" Style="padding-left: 0px; padding-right: 0px; width: 10px;"
        /><input type="hidden" id="hiddenDisplay" runat="server" /><input type="hidden" id="hiddenTaisyou" runat="server"
        /><input type="hidden" id="hiddenUpdDatetime" runat="server" /><input type="hidden" id="hiddenVisibleFlg" runat="server"
        />
</a>
<%-- ���s���󔒗ޕ����ɕϊ������ׁA���s�s�� --%>   
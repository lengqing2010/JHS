<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="TokubetuTaiouToolTipCtrl.ascx.vb"
    Inherits="Itis.Earth.WebUI.TokubetuTaiouToolTipCtrl" %>
<%@ Import Namespace="Itis.Earth.Utilities" %>

<a id ="spanTokubetuTaiou" runat="server"> 
    <asp:Label ID="lblTokubetuTaiou" runat="server" Text="特" Style="padding-left: 0px; padding-right: 0px; width: 10px;"
        /><input type="hidden" id="hiddenDisplay" runat="server" /><input type="hidden" id="hiddenTaisyou" runat="server"
        /><input type="hidden" id="hiddenUpdDatetime" runat="server" /><input type="hidden" id="hiddenVisibleFlg" runat="server"
        />
</a>
<%-- 改行が空白類文字に変換される為、改行不可 --%>   
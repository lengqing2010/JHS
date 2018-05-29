<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master"
    Codebehind="testPage.aspx.vb" Inherits="Itis.Earth.WebUI.testPage" Title="受注（確認）" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label runat="server" ID="lable2" Text="区&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;分："></asp:Label>
    <asp:TextBox runat="server" ID="tbxKbn"></asp:TextBox>
    <br />
    <asp:Label ID="Label1" runat="server" Text="物件番号："></asp:Label>
    <asp:TextBox runat="server" ID="tbxBukenNo"></asp:TextBox>
    <br />
    <br />
    <asp:Button runat="server" ID="btn1" Text="登録＆調査見積書作成" Style="background-color: #ff8812;
        height: 23px; width: 150px; font-weight: bold; border: none;" />
    <asp:Button runat="server" ID="btn2" Text="登録＆調査見積EXCEL指示" Style="background-color: #B9CDE5;
        height: 23px; width: 180px; font-weight: bold; border: none; margin-left:10px;" />
    <asp:Button runat="server" ID="btn3" Text="調査指示書" Style="background-color: #B9CDE5;
        height: 23px; width: 180px; font-weight: bold; border: none; margin-left:10px;" />
</asp:Content>

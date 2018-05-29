<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TestPopupDblClick.aspx.vb"
    Inherits="TestPopupDblClick" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>無題のページ</title>
    <link rel="stylesheet" href="App_Themes/css/JHS_EKKS.css" type="text/css" />

    <script language="javascript" type="text/javascript" src="Js/JHS_EKKS.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table cellpadding="0" cellspacing="0" border="1">
                <tr>
                    <td colspan="2" style="font-weight: bolder;">
                        ・系列ｺｰﾄﾞ検索POPUP
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #ccffff; width: 80px; text-align: center;">
                        <asp:Label ID="Label2" runat="server" Text="系列コード"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbxKeiretuCd" runat="server"></asp:TextBox>
                        <asp:TextBox ID="tbxKeiretuMei" runat="server"></asp:TextBox>
                        <asp:Button ID="Button1" runat="server" Text="検索" />
                    </td>
                </tr>
            </table>
            <br />
            <table cellpadding="0" cellspacing="0" border="1">
                <tr>
                    <td colspan="2" style="font-weight: bolder;">
                        ・営業所検索POPUP
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #ccffff; width: 80px; text-align: center;">
                        <asp:Label ID="Label1" runat="server" Text="営業所"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbxEigyousyo" runat="server"></asp:TextBox>
                        <asp:TextBox ID="tbxEigyousyoCd" runat="server"></asp:TextBox>
                        <asp:Button ID="Button2" runat="server" Text="検索" />
                    </td>
                </tr>
            </table>
            <br />
            <table cellpadding="0" cellspacing="0" border="1">
                <tr>
                    <td colspan="2" style="font-weight: bolder;">
                        ・登録事業者検索POPUP
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #ccffff; width: 80px; text-align: center;">
                        <asp:Label ID="Label3" runat="server" Text="登録事業者"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbxTourokusy" runat="server"></asp:TextBox>
                        <asp:TextBox ID="tbxTourokusyCd" runat="server"></asp:TextBox>
                        <asp:Button ID="Button3" runat="server" Text="検索" />
                    </td>
                </tr>
            </table>
            <br />
            <table cellpadding="0" cellspacing="0" border="1">
                <tr>
                    <td colspan="2" style="font-weight: bolder;">
                        ・都道府県検索POPUP
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #ccffff; width: 80px; text-align: center;">
                        <asp:Label ID="Label4" runat="server" Text="都道府県"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbxToudouhuken" runat="server"></asp:TextBox>
                        <asp:TextBox ID="tbxToudouhukenCd" runat="server"></asp:TextBox>
                        <asp:Button ID="Button4" runat="server" Text="検索" />
                    </td>
                </tr>
            </table>
            <br />
            <table cellpadding="0" cellspacing="0" border="1">
                <tr>
                    <td colspan="2" style="font-weight: bolder;">
                        ・支店 検索POPUP
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #ccffff; width: 80px; text-align: center;">
                        <asp:Label ID="Label5" runat="server" Text="支店"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbxCiten" runat="server"></asp:TextBox>
                        <asp:TextBox ID="tbxCitenCd" runat="server"></asp:TextBox>
                        <asp:Button ID="Button5" runat="server" Text="検索" />
                    </td>
                </tr>
            </table>
            <br />
            <table cellpadding="0" cellspacing="0" border="1">
                <tr>
                    <td colspan="2" style="font-weight: bolder;">
                        ・営業マン検索POPUP
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #ccffff; width: 80px; text-align: center;">
                        <asp:Label ID="Label6" runat="server" Text="営業マン"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbxEigyouMan" runat="server"></asp:TextBox>
                        <asp:TextBox ID="tbxEigyouManCd" runat="server"></asp:TextBox>
                        <asp:Button ID="Button6" runat="server" Text="検索" />
                    </td>
                </tr>
            </table>
            <br />
            <table cellpadding="0" cellspacing="0" border="1">
                <tr>
                    <td colspan="2" style="font-weight: bolder;">
                        ・計画管理_加盟店　検索POPUP
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #ccffff; width: 110px; text-align: center;">
                        <asp:Label ID="Label7" runat="server" Text="計画管理_加盟店"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbxKameitenCd" runat="server"></asp:TextBox>
                        <asp:TextBox ID="tbxKameitenMei" runat="server"></asp:TextBox>
                        <asp:Button ID="Button7" runat="server" Text="検索" />
                    </td>
                </tr>
            </table>
            <br />
            <table cellpadding="0" cellspacing="0" border="1">
                <tr>
                    <td colspan="2" style="font-weight: bolder;">
                        ・営業所検索POPUP(EARTHと同じ)
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #ccffff; width: 110px; text-align: center;">
                        <asp:Label ID="Label8" runat="server" Text="営業所検索"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbxEigyousyoCdEarth" runat="server"></asp:TextBox>
                        <asp:TextBox ID="tbxEigyousyoMeiEarth" runat="server"></asp:TextBox>
                        <asp:Button ID="Button8" runat="server" Text="検索" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

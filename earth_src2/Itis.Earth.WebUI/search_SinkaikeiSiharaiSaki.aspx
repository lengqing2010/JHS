<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="search_SinkaikeiSiharaiSaki.aspx.vb" Inherits="Itis.Earth.WebUI.search_SinkaikeiSiharaiSaki" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
     <title>新会計支払先検索</title>
    <link rel="stylesheet" href="css/jhsearth.css" type="text/css" />
</head>
<script type="text/javascript" src="js/jhsearth.js">
</script>
<script type="text/javascript">
    window.resizeTo(820,560);

</script>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="2" class="titleTable" style="width: 100%;
            text-align: left">
            <tbody>
                <tr>
                    <th>
                        <asp:Label ID="lblTitle" runat="server" Font-Size="15px"></asp:Label>
                    </th>
                    <th style="text-align: right">
                    </th>
                </tr>
                <tr>
                    <td colspan="2" rowspan="1">
                        <input id="btnCloseWin" runat="server" type="button" value="閉じる" />
                        <input id="clearWin" runat="server" type="button" value="クリア" /></td>
                </tr>
            </tbody>
        </table>
        <br />
        <table cellpadding="2" class="mainTable" style="text-align: left">
            <thead>
                <tr>
                    <th class="tableTitle" colspan="4" rowspan="1" style="height: 18px">
                        <asp:Label ID="lblKensaku" runat="server" Text="検索条件"></asp:Label></th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>
                        <asp:Label ID="lblCd" runat="server" Text=""></asp:Label>
                    </td>
                    <td style="width: 514px">
                        <asp:TextBox ID="search_Cd" runat="server" MaxLength="4" Style="ime-mode: disabled;" Width="48px">YMP8</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCd2" runat="server"></asp:Label></td>
                    <td style="width: 514px">
                        <asp:TextBox ID="search_Cd2" runat="server"  MaxLength="6" Style="ime-mode: disabled;
                            width: 100px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblMei" runat="server" Text=""></asp:Label>
                    </td>
                    <td style="width: 514px">
                        <asp:TextBox ID="search_Mei" runat="server" MaxLength="30"  Style="ime-mode: active;" Width="500px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblMei2" runat="server"></asp:Label></td>
                    <td style="width: 514px">
                        <asp:TextBox ID="search_Mei2" runat="server" MaxLength="40" Style="ime-mode: active"
                            Width="500px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2" rowspan="1" style="text-align: center">
                        検索上限件数<select id="maxSearchCount" runat="server">
                            <option selected="selected" value="100">100件</option>
                            <option value="max">無制限</option>
                        </select>
                        <asp:Button ID="search" runat="server" Text="検索実行" />
                        <asp:Button ID="Button" runat="server" Style="display: none" Text="Button" /></td>
                </tr>
            </tbody>
        </table>
        <table style="height: 30px">
            <tr>
                <td>
                    検索結果：
                </td>
                <td id="resultCount" runat="server" style="width: 3px">
                </td>
                <td>
                    件
                </td>
            </tr>
        </table>
        <asp:GridView ID="grdHead" runat="server" CellPadding="0" CellSpacing="0" CssClass="gridviewTableHeader"
            ShowHeader="False" Style="border-top: black 1px solid; border-left: black 1px solid">
        </asp:GridView>
        <div id="divMeisai" runat="server">
            <asp:GridView ID="grdBody" runat="server" BackColor="White" BorderColor="GrayText"
                BorderWidth="1px" CellPadding="0" CssClass="tableMeiSai" ShowHeader="False" Style="border-right: #999999 1px solid;
                border-top: black 1px solid">
                <SelectedRowStyle ForeColor="White" />
                <AlternatingRowStyle BackColor="LightCyan" />
                <RowStyle BorderColor="#999999" BorderWidth="1px" />
            </asp:GridView>
        </div>
        <asp:HiddenField ID="hidSort" runat="server" />
        <asp:HiddenField ID="hidColor" runat="server" />
    
    </div>
    </form>
</body>
</html>

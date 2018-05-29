<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="search_SiharaiTyousa.aspx.vb" Inherits="Itis.Earth.WebUI.search_SiharaiTyousa" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
     <title>検索</title>
    <link rel="stylesheet" href="css/jhsearth.css" type="text/css" />
</head>
<script type="text/javascript" src="js/jhsearth.js">
</script>
<script type="text/javascript">
    window.resizeTo(820,530);
</script>
<body>
    <form id="form1" runat="server">
    <div>

        
         <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
            class="titleTable">
            <tbody>
                <tr>
                    <th>
                        <asp:Label ID="lblTitle" runat="server" Font-Size="15px"></asp:Label>
                    </th>
                    <th style="text-align: right;">
                    </th>
                </tr>
                <tr>
                    <td colspan="2" rowspan="1">
                        <input id="btnCloseWin" value="閉じる" type="button" runat="server" />&nbsp;<input id="clearWin"
                            value="クリア" type="button" runat="server" /></td>
                </tr>
            </tbody>
        </table>
        <br />
         <table style="text-align: left;" class="mainTable" cellpadding="2">
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
                    <td>
                        <asp:TextBox ID="search_Cd" style="width: 100px; ime-mode: disabled;" runat="server"></asp:TextBox>
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 調査会社コード+調査会社事業所コードで検索</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblMei" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="search_Mei"  style="width: 500px; ime-mode: active;" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblMei2" runat="server"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="search_Mei2" runat="server" Style="ime-mode: active; width: 500px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: center;" colspan="2" rowspan="1">
                        検索上限件数<select id="maxSearchCount" runat="server">
                            <option value="100" selected="selected">100件</option>
                            <option value="max">無制限</option>
                        </select>
                        <asp:Button ID="search" runat="server" Text="検索実行" />
                        <asp:Button ID="Button" runat="server" Text="Button" style="display:none " /></td>
                </tr>
            </tbody>
        </table>
    </div>

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
    <asp:GridView ID="grdHead" runat="server" CssClass="gridviewTableHeader" style="border-left: 1px solid black;border-top: 1px solid black;"　ShowHeader="False" cellpadding="0" cellspacing="0">
    </asp:GridView>
    <div id="divMeisai" runat ="server"  >
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
    </form>
</body>
</html>
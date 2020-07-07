<%@ Page Language="vb" AutoEventWireup="false" Codebehind="search_SAPSiireSaki.aspx.vb"
    Inherits="Itis.Earth.WebUI.search_SAPSiireSaki" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>SAP仕入先検索</title>
    <link rel="stylesheet" href="css/jhsearth.css" type="text/css" />
</head>

<script type="text/javascript" src="js/jhsearth.js">
</script>

<script type="text/javascript">
    window.resizeTo(820,600);

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
                            <asp:Label ID="lblCd" runat="server" Text="勘定ｸﾞﾙｰﾌﾟ"></asp:Label>
                        </td>
                        <td style="width: 514px">
                            <asp:TextBox ID="tbxKdGroup" runat="server" MaxLength="4" Style="ime-mode: disabled;"
                                Width="48px" list="tbxKdGrouplist"></asp:TextBox>
                                <datalist id ="tbxKdGrouplist">
                                <option value="aaa">
                                <option value="bbb">
                                
                                </datalist>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCd2" runat="server" Text="仕入先ｺｰﾄﾞ"></asp:Label></td>
                        <td style="width: 514px">
                            <asp:TextBox ID="tbxSiireSakiCd" runat="server" MaxLength="10" Style="ime-mode: disabled;
                                width: 100px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblMei" runat="server" Text="仕入先名"></asp:Label>
                        </td>
                        <td style="width: 514px">
                            <asp:TextBox ID="tbxSiireSakiMei" runat="server" MaxLength="35" Style="ime-mode: active;"
                                Width="500px"></asp:TextBox>
                        </td>
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
            <table style="width: 600px; border: black 1px solid; border-collapse: collapse;
                background-color: #ffffd9; font-weight: bold; font-size: 13px;height:24px;
                text-align: center">
                <tr>
                    <td style="width: 100px; border: #ccc 1px solid; padding:2px;">
                        勘定ｸﾞﾙｰﾌﾟ</td>
                    <td style="width: 130px; border: #ccc 1px solid; padding:2px;">
                        仕入先ｺｰﾄﾞ</td>
                    <td>
                        仕入先名</td>
                </tr>
            </table>
            <div id="divMeisai" runat="server" style="width:620px; height:200px; overflow:auto;">
                <asp:GridView ID="grdBody" runat="server" BackColor="White" BorderColor="GrayText"
                    BorderWidth="1px" CellPadding="0" CssClass="tableMeiSai" ShowHeader="False" Style="border-right: #999999 1px solid;
                    border-top: black 1px solid" Width="600" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <%#Eval("a1_ktokk")%>
                            </ItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <%#Eval("a1_lifnr")%>
                            </ItemTemplate>
                            <ItemStyle Width="130px" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <%#Eval("a1_a_zz_sort")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
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

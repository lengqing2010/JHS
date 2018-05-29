<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SyubetuSearch.aspx.vb" Inherits="PopupSearch_SyubetuSearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>種別検索</title>
    <link rel="stylesheet" href="../App_Themes/css/JHS_EKKS.css" type="text/css" />

    <script language="javascript" type="text/javascript" src="../Js/JHS_EKKS.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <asp:Label ID="lblSyubetuKensaku" runat="server" Text="種別検索" CssClass="Title_fontBold">
                    </asp:Label>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" style="margin-top: 3px; margin-bottom: 20px;">
            <tr>
                <td style="padding-right: 3px;">
                    <asp:Button ID="btnTojiru" runat="server" Text="閉じる" TabIndex="5" />
                </td>
                <td>
                    <asp:Button ID="btnClear" runat="server" Text="クリア" TabIndex="6" />
                </td>
            </tr>
        </table>
        <table style="text-align: left;" class="mainTable" cellpadding="2" width="560px">
            <thead>
                <tr>
                    <th class="tableTitle" colspan="4" rowspan="1" style="height: 18px">
                        <asp:Label ID="lblKensaku" runat="server" Text="検索条件"></asp:Label></th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td style="width: 70px;">
                        <asp:Label ID="lblSyubetuCd" runat="server" Text="種別コード"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbxSyubetuCd" Style="width: 150px; ime-mode: disabled;" runat="server"
                            TabIndex="1" MaxLength="5">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 70px;">
                        <asp:Label ID="lblSyubetuMei" runat="server" Text="種別名"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbxSyubetuMei" Style="width: 460px; ime-mode: active;" runat="server"
                            TabIndex="2" MaxLength="82">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center;" colspan="2" rowspan="1">
                        検索上限件数
                        <asp:DropDownList runat="server" ID="ddlKensakuKensu" TabIndex="3">
                        </asp:DropDownList>
                        <asp:Button ID="btnKensakuJltukou" runat="server" Text="検索実行" Width="80px" Height="23px"
                            TabIndex="4" /></td>
                </tr>
            </tbody>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" style="margin-top: 10px; margin-bottom: 5px;">
            <tr>
                <td>
                    検索結果：
                    <asp:Label ID="lblKensakuKeltuka" runat="server" Width="10px"></asp:Label>&nbsp;件
                </td>
            </tr>
        </table>
        <table style="text-align: center; width: 583px;" border="0" cellpadding="0" cellspacing="0"
            class="TableBorder MeisaiHeader">
            <tr>
                <td class="TdBorder" style="width: 124px;">
                    <asp:Label ID="lblSyubetuCdGr" runat="server" Text="種別コード"></asp:Label>
                    <asp:LinkButton runat="server" ID="lnkBtnSyubetuCdUp" Text="▲" Style="text-decoration: none;"></asp:LinkButton>
                    <asp:LinkButton runat="server" ID="lnkBtnSyubetuCdDown" Text="▼" Style="text-decoration: none;
                        margin-left: -5px;"></asp:LinkButton>
                </td>
                <td class="TdBorder" style="">
                    <asp:Label ID="lblSyubetuMeiGr" runat="server" Text="種別名"></asp:Label>
                    <asp:LinkButton runat="server" ID="lnkBtnSyubetuMeiUp" Text="▲" Style="text-decoration: none;"></asp:LinkButton>
                    <asp:LinkButton runat="server" ID="lnkBtnSyubetuMeiDown" Text="▼" Style="text-decoration: none;
                        margin-left: -5px;"></asp:LinkButton>
                </td>
            </tr>
        </table>
        <div style="width: 600px; height: 190px; overflow-y: auto;">
            <asp:GridView ID="grdMeisai" runat="server" AutoGenerateColumns="False" ShowHeader="False"
                CellPadding="0" Width="583px" CssClass="gvTableBorder" Style="margin-top: -1px;">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            &nbsp;<asp:Label ID="lblSyubetuCdValue" runat="server" Width="105px" Text='<%#Eval("cd")%>'></asp:Label>
                            <asp:HiddenField ID="hidSyubetuCd" runat="server" Value='<%#Eval("cd")%>' />
                        </ItemTemplate>
                        <ItemStyle CssClass="gvMeisaiBorder" Width="130px" Height="18px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            &nbsp;<asp:Label ID="lblSyubetuMeiValue" runat="server" Width="295px" Style="padding-left: 10px;"
                                Text='<%#Eval("mei")%>' ToolTip='<%#Eval("mei")%>' CssClass="TextOverflow"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="gvMeisaiBorder" Height="18px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                </Columns>
                <AlternatingRowStyle BackColor="LightCyan" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>

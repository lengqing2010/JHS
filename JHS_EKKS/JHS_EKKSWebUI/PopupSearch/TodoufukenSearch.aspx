<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TodoufukenSearch.aspx.vb"
    Inherits="PopupSearch_TodoufukenSearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>都道府県検索</title>
    <link rel="stylesheet" href="../App_Themes/css/JHS_EKKS.css" type="text/css" />

    <script language="javascript" type="text/javascript" src="../Js/JHS_EKKS.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <asp:Label ID="lblTodouhukenKensaku" runat="server" Text="都道府県検索" CssClass="Title_fontBold">
                    </asp:Label>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" style="margin-top: 3px; margin-bottom: 20px;">
            <tr>
                <td style="padding-right: 3px;">
                    <asp:Button ID="btnTojiru" runat="server" Text="閉じる" TabIndex="4" />
                </td>
                <td>
                    <asp:Button ID="btnClear" runat="server" Text="クリア" TabIndex="5" />
                </td>
            </tr>
        </table>
        <table style="text-align: left;" class="mainTable" cellpadding="2" width="600px">
            <thead>
                <tr>
                    <th class="tableTitle" colspan="4" rowspan="1" style="height: 18px">
                        <asp:Label ID="lblKensaku" runat="server" Text="検索条件"></asp:Label></th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>
                        <asp:Label ID="lblTodouhukenMei" runat="server" Text="都道府県名"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbxTodouhukenMei" Style="width: 520px; ime-mode: active;" runat="server"
                            TabIndex="1" MaxLength="40">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center;" colspan="2" rowspan="1">
                        検索上限件数
                        <asp:DropDownList runat="server" ID="ddlKensakuKensu" TabIndex="2">
                        </asp:DropDownList>
                        <asp:Button ID="btnKensakuJltukou" runat="server" Text="検索実行" Width="80px" Height="23px"
                            TabIndex="3" /></td>
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
                <td class="TdBorder" style="width: 150px;">
                    <asp:Label ID="lblTodouhukenCdGr" runat="server" Text="都道府県コード"></asp:Label>
                    <asp:LinkButton runat="server" ID="lnkBtnTodouhukenCdUp" Text="▲" Style="text-decoration: none;"></asp:LinkButton>
                    <asp:LinkButton runat="server" ID="lnkBtnTodouhukenCdDown" Text="▼" Style="text-decoration: none;
                        margin-left: -5px;"></asp:LinkButton>
                </td>
                <td class="TdBorder" style="width: 433px;">
                    <asp:Label ID="lblTodouhukenMeiGr" runat="server" Text="都道府県名"></asp:Label>
                    <asp:LinkButton runat="server" ID="lnkBtnTodouhukenMeiUp" Text="▲" Style="text-decoration: none;"></asp:LinkButton>
                    <asp:LinkButton runat="server" ID="lnkBtnTodouhukenMeiDown" Text="▼" Style="text-decoration: none;
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
                            &nbsp;<asp:Label ID="lblTodouhukenCdValue" runat="server" Width="140px" Text='<%#Eval("todouhuken_cd")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="gvMeisaiBorder" Width="154px" Height="18px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            &nbsp;<asp:Label ID="lblTodouhukenMeiValue" runat="server" Width="420px" Text='<%#Eval("todouhuken_mei")%>'
                                ToolTip='<%#Eval("todouhuken_mei")%>' CssClass="TextOverflow"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="gvMeisaiBorder" Width="429px" Height="18px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                </Columns>
                <AlternatingRowStyle BackColor="LightCyan" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>

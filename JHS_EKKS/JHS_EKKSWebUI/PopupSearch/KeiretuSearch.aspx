<%@ Page Language="VB" AutoEventWireup="false" CodeFile="KeiretuSearch.aspx.vb" Inherits="KeiretuSearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>系列検索</title>
    <link rel="stylesheet" href="../App_Themes/css/JHS_EKKS.css" type="text/css" />

    <script language="javascript" type="text/javascript" src="../Js/JHS_EKKS.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <asp:Label ID="lblKeretuKensaku" runat="server" Text="系列検索" CssClass="Title_fontBold">
                    </asp:Label>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" style="margin-top: 3px; margin-bottom: 20px;">
            <tr>
                <td style="padding-right: 3px;">
                    <asp:Button ID="btnTojiru" runat="server" Text="閉じる" TabIndex="6" />
                </td>
                <td>
                    <asp:Button ID="btnClear" runat="server" Text="クリア" TabIndex="7" />
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
                        <asp:Label ID="lblKeiretuCd" runat="server" Text="系列コード"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbxKeiretuCd" Style="width: 100px; ime-mode: disabled;" runat="server"
                            TabIndex="1" MaxLength="5">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblKeiretuMei" runat="server" Text="系列名"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbxKeiretuMei" Style="width: 520px; ime-mode: active;" runat="server"
                            TabIndex="2" MaxLength="40">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center;" colspan="2" rowspan="1">
                        <asp:CheckBox ID="chkTorikesi" runat="server" Text="取消0のみ" Style="margin-right: 10px;"
                            TabIndex="3" />
                        検索上限件数
                        <asp:DropDownList runat="server" ID="ddlKensakuKensu" TabIndex="4">
                        </asp:DropDownList>
                        <asp:Button ID="btnKensakuJltukou" runat="server" Text="検索実行" Width="80px" Height="23px"
                            TabIndex="5" /></td>
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
        <table style="text-align: center; width: 599px;" border="0" cellpadding="0" cellspacing="0"
            class="TableBorder MeisaiHeader">
            <tr>
                <td class="TdBorder" style="width: 115px;">
                    <asp:Label ID="lblKeiretuCdGr" runat="server" Text="系列コード"></asp:Label>
                    <asp:LinkButton runat="server" ID="lnkBtnKeiretuCdUp" Text="▲" Style="text-decoration: none;"></asp:LinkButton>
                    <asp:LinkButton runat="server" ID="lnkBtnKeiretuCdDown" Text="▼" Style="text-decoration: none;
                        margin-left: -5px;"></asp:LinkButton>
                </td>
                <td class="TdBorder" style="width: 409px;">
                    <asp:Label ID="lblKeiretuMeiGr" runat="server" Text="系列名"></asp:Label>
                    <asp:LinkButton runat="server" ID="lnkBtnKeiretuMeiUp" Text="▲" Style="text-decoration: none;"></asp:LinkButton>
                    <asp:LinkButton runat="server" ID="lnkBtnKeiretuMeiDown" Text="▼" Style="text-decoration: none;
                        margin-left: -5px;"></asp:LinkButton>
                </td>
                <td class="TdBorder" style="width: 75px;">
                    <asp:Label ID="lblTorikesi" runat="server" Text="取消"></asp:Label>
                    <asp:LinkButton runat="server" ID="lnkBtnTorikesiUp" Text="▲" Style="text-decoration: none;"></asp:LinkButton>
                    <asp:LinkButton runat="server" ID="lnkBtnTorikesiDown" Text="▼" Style="text-decoration: none;
                        margin-left: -5px;"></asp:LinkButton>
                </td>
            </tr>
        </table>
        <div style="width: 616px; height: 190px; overflow-y: auto;">
            <asp:GridView ID="grdMeisai" runat="server" AutoGenerateColumns="False" ShowHeader="False"
                CellPadding="0" Width="599px" CssClass="gvTableBorder" Style="margin-top: -1px;">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            &nbsp;<asp:Label ID="lblKeiretuCdValue" runat="server" Width="110px" Text='<%#Eval("keiretu_cd")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="gvMeisaiBorder" Width="118px" Height="18px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            &nbsp;<asp:Label ID="lblKeiretuMeiValue" runat="server" Width="390px" Text='<%#Eval("keiretu_mei")%>'
                                ToolTip='<%#Eval("keiretu_mei")%>' CssClass="TextOverflow"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="gvMeisaiBorder" Width="401px" Height="18px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            &nbsp;<asp:Label ID="lblTorikesiValue" runat="server" Width="70px" Text='<%#Eval("Torikesi")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="gvMeisaiBorder" Width="80px" Height="18px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                </Columns>
                <AlternatingRowStyle BackColor="LightCyan" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>

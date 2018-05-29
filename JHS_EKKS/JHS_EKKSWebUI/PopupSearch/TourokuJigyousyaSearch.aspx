<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TourokuJigyousyaSearch.aspx.vb"
    Inherits="PopupSearch_TourokuJigyousyaSearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>登録事業者検索</title>
    <link rel="stylesheet" href="../App_Themes/css/JHS_EKKS.css" type="text/css" />

    <script language="javascript" type="text/javascript" src="../Js/JHS_EKKS.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <asp:Label ID="lblTourokuJigyousyaKensaku" runat="server" Text="登録事業者検索" CssClass="Title_fontBold">
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
                        <asp:Label ID="lblKameitenCd" runat="server" Text="加盟店コード"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbxKameitenCd" Style="width: 100px; ime-mode: disabled;" runat="server"
                            TabIndex="1" MaxLength="5">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblKameitenMei" runat="server" Text="加盟店名"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbxKameitenMei" Style="width: 520px; ime-mode: active;" runat="server"
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
        <table style="text-align: center; width: 780px;" border="0" cellpadding="0" cellspacing="0"
            class="TableBorder MeisaiHeader">
            <tr>
                <td class="TdBorder" style="width: 115px;">
                    <asp:Label ID="lblKameitenCdGr" runat="server" Text="加盟店コード"></asp:Label>
                    <asp:LinkButton runat="server" ID="lnkBtnKameitenCdUp" Text="▲" Style="text-decoration: none;"></asp:LinkButton>
                    <asp:LinkButton runat="server" ID="lnkBtnKameitenCdDown" Text="▼" Style="text-decoration: none;
                        margin-left: -5px;"></asp:LinkButton>
                </td>
                <td class="TdBorder" style="width: 300px;">
                    <asp:Label ID="lblKameitenMeiGr" runat="server" Text="加盟店名"></asp:Label>
                    <asp:LinkButton runat="server" ID="lnkBtnKameitenMeiUp" Text="▲" Style="text-decoration: none;"></asp:LinkButton>
                    <asp:LinkButton runat="server" ID="lnkBtnKameitenMeiDown" Text="▼" Style="text-decoration: none;
                        margin-left: -5px;"></asp:LinkButton>
                </td>
                <td class="TdBorder" style="width: 110px;">
                    <asp:Label ID="lblTodouhukenMeiGr" runat="server" Text="都道府県名"></asp:Label>
                    <asp:LinkButton runat="server" ID="lnkBtnTodouhukenMeiUp" Text="▲" Style="text-decoration: none;"></asp:LinkButton>
                    <asp:LinkButton runat="server" ID="lnkBtnTodouhukenMeiDown" Text="▼" Style="text-decoration: none;
                        margin-left: -5px;"></asp:LinkButton>
                </td>
                <td class="TdBorder" style="width: 180px;">
                    <asp:Label ID="lblKameitenKanaMeiGr" runat="server" Text="加盟店カナ名"></asp:Label>
                    <asp:LinkButton runat="server" ID="lnkBtnKameitenKanaMeiUp" Text="▲" Style="text-decoration: none;"></asp:LinkButton>
                    <asp:LinkButton runat="server" ID="lnkBtnKameitenKanaMeiDown" Text="▼" Style="text-decoration: none;
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
        <div style="width: 797px; height: 190px; overflow-y: auto;">
            <asp:GridView ID="grdMeisai" runat="server" AutoGenerateColumns="False" ShowHeader="False"
                CellPadding="0" Width="780px" CssClass="gvTableBorder" Style="margin-top: -1px;">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            &nbsp;<asp:Label ID="lblKameitenCdValue" runat="server" Width="108px" Text='<%#Eval("kameiten_cd")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="gvMeisaiBorder" Width="117px" Height="18px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            &nbsp;<asp:Label ID="lblKameitenMeiValue" runat="server" Width="285px" Text='<%#Eval("kameiten_mei1")%>'
                                ToolTip='<%#Eval("kameiten_mei1")%>' CssClass="TextOverflow"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="gvMeisaiBorder" Width="292px" Height="18px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            &nbsp;<asp:Label ID="lblTodouhukenMeiValue" runat="server" Width="100px" Text='<%#Eval("todouhuken_mei")%>'
                                ToolTip='<%#Eval("todouhuken_mei")%>' CssClass="TextOverflow"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="gvMeisaiBorder" Width="113px" Height="18px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            &nbsp;<asp:Label ID="lblKameitenKanaMeiValue" runat="server" Width="170px" Text='<%#Eval("tenmei_kana1")%>'
                                ToolTip='<%#Eval("tenmei_kana1")%>' CssClass="TextOverflow"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="gvMeisaiBorder" Width="180px" Height="18px" HorizontalAlign="Left" />
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

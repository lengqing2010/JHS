<%@ Page Language="VB" MasterPageFile="~/EKKSMaster.master" AutoEventWireup="false"
    CodeFile="SitenTukibetuKeikakuchiErrorDetails.aspx.vb" Inherits="SitenTukibetuKeikakuchiErrorDetails"
    Title="支店 月別計画値 EXCEL取込エラー" %>

<%@ MasterType VirtualPath="~/EKKSMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        //onload
        window.onload = function(){
            window.name = "<%=CommonConstBC.eigyouKeikakuKanri%>"
            setMenuBgColor();
        }
    </script>

    <table border="0" cellpadding="0" cellspacing="0" style="margin-top: 10px;">
        <tr>
            <td>
                <asp:Label ID="lblEigyousyoKensaku" runat="server" Text="支店 月別計画値 EXCEL取込エラー" CssClass="Title_fontBold">
                </asp:Label>
                <asp:Button ID="btnTojiru" runat="server" Text="閉じる" Style="margin-left: 20px; width: 50px;
                    height: 23px;" TabIndex="1" />
            </td>
        </tr>
    </table>
    <table style="width: 470px; text-align: left; margin-top: 10px;" class="mainTable paddinNarrow"
        cellpadding="1">
        <tr>
            <td style="width: 110px; height: 25px; background-color: #ccffff;">
                取込日時
            </td>
            <td>
                <asp:Label ID="lblSyoriDate" runat="server" Text="" Width="360px" Style="padding-left: 4px;"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 110px; height: 25px; background-color: #ccffff;">
                取込ファイル名
            </td>
            <td>
                <asp:Label ID="lblFileMei" runat="server" Text="" Width="360px" Style="padding-left: 4px;"
                    CssClass="TextOverflow"></asp:Label>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" style="margin-top: 10px;">
        <tr>
            <td style="width: 65px;">
                検索結果：&nbsp;
            </td>
            <td>
                <asp:Label runat="server" ID="lblCount"></asp:Label>
            </td>
            <td>
                &nbsp;件
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" class="TableBorder MeisaiHeader" style="margin-top: 5px;
        width: 900px;">
        <tr>
            <td class="TdBorder" style="width: 100px; text-align: center;">
                <asp:Label ID="lblGyouNo" runat="server" Text="行番号"></asp:Label>
            </td>
            <td class="TdBorder" style="width: 800px; text-align: left;">
                <asp:Label ID="lblErrorNaiyou" runat="server" Text="エラー内容"></asp:Label>
            </td>
        </tr>
    </table>
    <div style="overflow: auto; width: 917px; height: 285px;">
        <asp:GridView ID="grdErrorJyouhou" runat="server" AutoGenerateColumns="False" ShowHeader="False"
            CellPadding="0" Width="900px" CssClass="gvTableBorder" Style="margin-top: -1px;">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="lblGyouNoValue" runat="server" Width="90px" Text='<%#Eval("gyou_no")%>'
                            Style="padding-left: 5px" CssClass="TextOverflow"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle CssClass="gvMeisaiBorder" Width="109px" Height="18px" HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="lblErrorNaiyouValue" runat="server" Width="780px" Text='<%#Eval("error_naiyou")%>'
                            Style="padding-left: 5px" ToolTip='<%#Eval("error_naiyou")%>' CssClass="TextOverflow"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle CssClass="gvMeisaiBorder" Width="794px" Height="18px" HorizontalAlign="Left" />
                </asp:TemplateField>
            </Columns>
            <AlternatingRowStyle BackColor="LightCyan" />
        </asp:GridView>
    </div>
</asp:Content>

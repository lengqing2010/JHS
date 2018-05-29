<%@ Page Language="VB" MasterPageFile="~/EKKSMaster.master" AutoEventWireup="false"
    CodeFile="KeikakuKanriErrorDetails.aspx.vb" Inherits="KeikakuKanriErrorDetails"
    Title="計画管理 ＥＸＣＥＬ取込エラー" %>

<%@ MasterType VirtualPath="~/EKKSMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        //onload
        window.onload = function(){
            window.name = "<%=CommonConstBC.eigyouKeikakuKanri%>"
            setMenuBgColor();
        }
    </script>

    <table style="text-align: left; width: 960px;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tr>
            <th style="width: 350px;">
                計画管理 ＥＸＣＥＬ取込エラー
            </th>
            <th style="width: 70px;">
                <asp:Button ID="btnClose" runat="server" Text="閉じる" Style="height: 25px; padding-top: 2px;" />
            </th>
            <th>
                <%--<asp:Button ID="btnCsv" runat="server" Text="CSV出力" Style="height: 25px; padding-top: 2px;" />--%>
            </th>
        </tr>
        <tr>
            <td colspan="3" rowspan="1">
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" style="margin-left: 30px; border: 1px" class="mainTable paddinNarrow">
        <tr>
            <td class="" style="height: 25px; width: 100px; background-color: #ccffff;">
                取込日時
            </td>
            <td style="width: 350px;">
                &nbsp;<asp:Label ID="lblSyoriDatetime" runat="server" Text="" Width="280px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="" style="height: 25px; background-color: #ccffff;">
                取込ファイル名
            </td>
            <td>
                &nbsp;<asp:Label ID="lblFileName" runat="server" Text="" Width="280px" CssClass="TextOverflow"></asp:Label>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" style="margin-left: 30px; margin-top: 10px;">
        <tr>
            <td style="width: 65px; height: 14px;">
                検索結果：&nbsp;
            </td>
            <td>
                <asp:Label runat="server" ID="lblCount"></asp:Label>&nbsp;件
            </td>
        </tr>
    </table>
    <%--<br />--%>
    <table cellpadding="0" cellspacing="0" class="TableBorder" width="902px" style="margin-left: 30px;">
        <tr>
            <td class="TdBorder" style="width: 72px; height: 25px; background-color: #ffffd9;
                font-weight: bold;" align="center">
                行番号</td>
            <td class="TdBorder" style="background-color: #ffffd9; font-weight: bold;" align="left">
                &nbsp;エラー内容</td>
        </tr>
    </table>
    <div style="overflow: auto; width: 919px; height: 338px; margin-left: 30px;">
        <asp:GridView ID="grdErrorJyouhou" runat="server" AutoGenerateColumns="False" ShowHeader="False"
            Style="margin-top: -1px" Width="902" BorderWidth="1px" CellSpacing="0" CellPadding="0"
            CssClass="gvTableBorder">
            <Columns>
                <asp:BoundField DataField="gyou_no">
                    <ItemStyle Width="72px" Height="25px" CssClass="gvMeisaiBorder" HorizontalAlign="center" />
                </asp:BoundField>
                <asp:TemplateField>
                    <ItemStyle HorizontalAlign="Left" Width="" CssClass="gvMeisaiBorder Padding_Left" />
                    <ItemTemplate>
                        <asp:Label ID="lblErrorNaiyou" runat="server" Width="810px" Text='<%#Eval("error_naiyou").ToString%>'
                            ToolTip='<%#Eval("error_naiyou").ToString%>' CssClass="TextOverflow"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <AlternatingRowStyle BackColor="#CCFFFF" />
            <RowStyle Height="25px" BorderColor="#999999" />
        </asp:GridView>
    </div>
</asp:Content>

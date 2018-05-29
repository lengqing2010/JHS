<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master"
    Codebehind="TokubetuTaiouMasterErrorDetails.aspx.vb" Inherits="Itis.Earth.WebUI.TokubetuTaiouMasterErrorDetails"
    Title="加盟店商品調査方法特別対応マスタエラー確認" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js"></script>

    <script type="text/javascript">
        //window名付与
        var objWin = window;
        objWin.name = "earthMainWindow"
        initPage(); //画面初期設定
    </script>
    <table style="text-align: left; width: 960px;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tr>
        </tr>
        <tr>
            <th style="width: 400px;">
                加盟店商品調査方法特別対応マスタエラー確認
            </th>
            <th style="width: 70px;">
                <asp:Button ID="btnClose" runat="server" Text="閉じる" Style="height: 25px;" />
            </th>
            <th style="width: 70px;">
                <asp:Button ID="btnCsvOutput" runat="server" Text="CSV出力" Style="height: 25px;" />
            </th>
            <th>
            </th>
        </tr>
        <tr>
        <td colspan="5" rowspan="1">
            </td>
        </tr>
    </table>
    <table style="width: 400px; text-align: left;" class="mainTable paddinNarrow" cellpadding="1">
        <tr>
            <td style="width: 120px; height: 25px; background-color: #ccffff;">
                取込日時
            </td>
            <td style="height: 25px">
                <asp:Label ID="lblSyoriDate" runat="server" Text="" Width="280px" Style="padding-left: 4px;"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 120px; height: 25px; background-color: #ccffff;">
                取込ファイル名
            </td>
            <td style="height: 25px">
                 <asp:Label ID="lblFileMei" runat="server" Text="" Width="280px" Style="padding-left: 4px;
                    white-space: nowrap; overflow: hidden; text-overflow: ellipsis;"></asp:Label>
           </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" style="margin-top: 10px;">
        <tr>
            <td style="width: 65px; height: 14px;">
                検索結果：&nbsp;
            </td>
            <td style="height: 14px">
                <asp:Label runat="server" ID="lblCount"></asp:Label>&nbsp;件
            </td>
        </tr>
    </table>
    <br />
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <div id="divHeadLeft" runat="server" style="width: 615px; overflow-y: hidden; overflow-x: hidden; border-left: solid 1px black; ">
                    <table style="width: 615px; border-bottom: solid 1px black; text-align:left;" class="gridviewTableHeader" cellpadding="0" cellspacing="0" >
                        <tr>
                            <td style="width: 50px;">
                                <asp:Label ID="Label1" runat="server" Width="50px" Text="行番号"
                                        ToolTip="行番号" Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                            </td>
                            <td style="width: 80px">
                                <asp:Label ID="Label2" runat="server" Width="80px" Text="相手先種別"
                                        ToolTip="相手先種別" Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                            </td>
                            <td style="width: 80px">
                                <asp:Label ID="Label3" runat="server" Width="80px" Text="相手先コード"
                                        ToolTip="相手先コード" Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                            </td>
                            <td style="width: 65px">
                                <asp:Label ID="Label4" runat="server" Width="65px" Text="相手先名"
                                        ToolTip="相手先名" Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                            </td>
                            <td style="width: 65px">
                                <asp:Label ID="Label5" runat="server" Width="65px" Text="商品コード"
                                        ToolTip="商品コード" Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                            </td>
                            <td style="width: 60px">
                                <asp:Label ID="Label6" runat="server" Width="60px" Text="商品名"
                                        ToolTip="商品名" Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                            </td>
                            <td style="width: 65px;">
                                <asp:Label ID="Label7" runat="server" Width="65px" Text="調査方法"
                                        ToolTip="調査方法" Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                            </td>
                            <td style="border-right:solid 1px gray;">
                                <asp:Label ID="Label8" runat="server"  Text="特別対応コード"
                                        ToolTip="特別対応コード" Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td>
                <div id="divHeadRight" runat="server" style="width: 343px; overflow-y: hidden; overflow-x: hidden; border-right:solid 1px black;">
                    <table style="width: 850px; border-bottom: solid 1px black;" class="gridviewTableHeader" cellpadding="0" cellspacing="0" >
                        <tr>
                            <td style="width: 150px">
                                <asp:Label ID="Label9" runat="server" Width="150px" Text="特別対応名称"
                                        ToolTip="特別対応名称" Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                            </td>
                            <td style="width: 50px">
                                取消</td>
                            <td style="width:150px">
                                金額加算商品コード</td>
                            <td style="width:150px">
                                金額加算商品名</td>
                            <td style="width:50px">
                                初期値</td>
                            <td style="width:150px">
                                実請求加算金額</td>
                            <td style="width:150px">
                                工務店請求加算金額</td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; width: 615px;">
                <div id="divMeisaiLeft" runat="server" onmousewheel="wheel();" style="width: 615px; height: 221px; overflow-y: hidden;
                    overflow-x: hidden; border-left: solid 1px black; margin-top:-1px;
                    border-bottom: solid 1px black;">
                    <asp:GridView ID="grdMeisaiLeft" runat="server" ShowHeader="False" CssClass="tableMeiSai"
                         CellPadding="0" AutoGenerateColumns="False"
                        Width="615px">
                        <Columns>
                            <asp:TemplateField>
                                <ItemStyle Width="51px" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblGyou_no" runat="server" Width="51px" Text='<%#Eval("gyou_no").ToString%>'
                                        ToolTip='<%#Eval("gyou_no").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="82px" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblAitesaki_syubetu" runat="server" Width="82px" Text='<%#Eval("aitesaki_syubetu").ToString%>'
                                        ToolTip='<%#Eval("aitesaki_syubetu").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="82px" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblAitesaki_cd" runat="server" Width="82px" Text='<%#Eval("aitesaki_cd").ToString%>'
                                        ToolTip='<%#Eval("aitesaki_cd").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="67px" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblAitesaki_mei" runat="server" Width="67px" Text='<%#Eval("aitesaki_mei").ToString%>'
                                        ToolTip='<%#Eval("aitesaki_mei").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="67px" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblSyouhin_cd" runat="server" Width="67px" Text='<%#Eval("syouhin_cd").ToString%>'
                                        ToolTip='<%#Eval("syouhin_cd").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="62px" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="syouhin_mei" runat="server" Width="62px" Text='<%#Eval("syouhin_mei").ToString%>'
                                        ToolTip='<%#Eval("syouhin_mei").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="67px" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="tys_houhou" runat="server" Width="67px" Text='<%#Eval("tys_houhou").ToString%>'
                                        ToolTip='<%#Eval("tys_houhou").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle  BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="tokubetu_taiou_cd" runat="server"  Text='<%#Eval("tokubetu_taiou_cd").ToString%>'
                                        ToolTip='<%#Eval("tokubetu_taiou_cd").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle BackColor="#CCFFFF" />
                        <RowStyle Height="22px" BorderColor="#999999" />
                    </asp:GridView>
                </div>
            </td>
            <td style="vertical-align: top; width: 343px;">
                <div id="divMeisaiRight" runat="server" onmousewheel="wheel();" style="width: 343px; height: 221px; overflow-y: hidden;
                    overflow-x: hidden;  border-right: solid 1px black; margin-top:-1px;
                    border-bottom: solid 1px black;">
                    <asp:GridView ID="grdMeisaiRight" runat="server" ShowHeader="False" CssClass="tableMeiSai"
                         CellPadding="0" AutoGenerateColumns="False"
                        Width="850px">
                        <Columns>
                            <asp:TemplateField>
                                <ItemStyle Width="151px" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="tokubetu_taiou_meisyou" runat="server" Width="151px" Text='<%#Eval("tokubetu_taiou_meisyou").ToString%>'
                                        ToolTip='<%#Eval("tokubetu_taiou_meisyou").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="50px" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="torikesi" runat="server" Width="50px" Text='<%#Eval("torikesi").ToString%>'
                                        ToolTip='<%#Eval("torikesi").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="141px" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="kasan_syouhin_cd" runat="server" Width="141px" Text='<%#Eval("kasan_syouhin_cd").ToString%>'
                                        ToolTip='<%#Eval("kasan_syouhin_cd").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="141px" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="kasan_syouhin_mei" runat="server" Width="141px" Text='<%#Eval("kasan_syouhin_mei").ToString%>'
                                        ToolTip='<%#Eval("kasan_syouhin_mei").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="49px" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="syokiti" runat="server" Width="49px" Text='<%#Eval("syokiti").ToString%>'
                                        ToolTip='<%#Eval("syokiti").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="141px" HorizontalAlign="Right" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="uri_kasan_gaku" runat="server" Width="141px" Text='<%#Eval("uri_kasan_gaku").ToString%>'
                                        ToolTip='<%#Eval("uri_kasan_gaku").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle  BorderColor="#999999" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="koumuten_kasan_gaku" runat="server"  Text='<%#Eval("koumuten_kasan_gaku").ToString%>'
                                        ToolTip='<%#Eval("koumuten_kasan_gaku").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle BackColor="#CCFFFF" />
                        <RowStyle Height="22px" BorderColor="#999999" />
                    </asp:GridView>
                </div>
            </td>
            <td valign="top" style="width: 16px; height: 221px;">
                <div id="divHiddenMeisaiV" runat="server" style="overflow: auto; height: 221px; width: 30px;
                    margin-left: -14px;" onscroll="fncScrollV();">
                    <table height="<%=scrollHeight%>">
                        <tr>
                            <td>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td style="height: 16px;">
            </td>
            <td >
                <div style="overflow-x: hidden; height: 18px; width: 348px; margin-top: -1px;">
                    <div id="divHiddenMeisaiH" runat="server" style="overflow: auto; height: 18px; width: 348px;
                        margin-top: 0px;" onscroll="fncScrollH();">
                        <table style="width: 850px;">
                            <tr>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
            <td style="height: 16px;">
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hidCSVCount" runat="server" />
    <asp:HiddenField ID="hidCsvOut" runat="server" />
</asp:Content>

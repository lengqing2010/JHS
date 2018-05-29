<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master"
    Codebehind="KoujiKakakuMasterErrorDetails.aspx.vb" Inherits="Itis.Earth.WebUI.KoujiKakakuMasterErrorDetails"
    Title="工事価格マスタエラー確認" %>

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
            <th style="width: 210px;">
                工事価格マスタエラー確認
            </th>
            <th style="width: 70px;">
                <asp:Button ID="btnClose" runat="server" Text="閉じる" Style="height: 25px; padding-top: 2px;" />
            </th>
            <th style="width: 70px;">
                <asp:Button ID="btnCsvOutput" runat="server" Text="CSV出力" Style="height: 25px; padding-top: 2px;" />
            </th>
            <th>
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
            <td>
                <asp:Label ID="lblSyoriDate" runat="server" Text="" Width="280px" Style="padding-left: 4px;"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 120px; height: 25px; background-color: #ccffff;">
                取込ファイル名
            </td>
            <td>
                <asp:Label ID="lblFileMei" runat="server" Text="" Width="280px" Style="padding-left: 4px;
                    white-space: nowrap; overflow: hidden; text-overflow: ellipsis;"></asp:Label>
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
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <div id="divHeadLeft" style="width: 509px; overflow-y: hidden; overflow-x: hidden;">
                    <table id="tableHeadLeft" border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffd9;
                        font-weight: bold;">
                        <tr style="height: 24px;">
                            <td style="width: 54px; border-top: solid 1px black; border-bottom: solid 1px black;
                                border-left: solid 1px black; border-right: solid 1px gray; text-align: center;">
                                行番号
                            </td>
                            <td style="width: 70px; border-top: solid 1px black; border-bottom: solid 1px black;
                                border-right: solid 1px gray; text-align: center;">
                                先種別
                            </td>
                            <td style="width: 60px; border-top: solid 1px black; border-bottom: solid 1px black;
                                border-right: solid 1px gray; text-align: center;">
                                先コード
                            </td>
                            <td style="width: 124px; border-top: solid 1px black; border-bottom: solid 1px black;
                                border-right: solid 1px gray; text-align: center;">
                                相手先名
                            </td>
                            <td style="width: 70px; border-top: solid 1px black; border-bottom: solid 1px black;
                                border-right: solid 1px gray; text-align: center;">
                                商品
                            </td>
                            <td style="width: 124px; border-top: solid 1px black; border-bottom: solid 1px black;
                                border-right: solid 1px gray; text-align: center;">
                                商品名
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td>
                <div id="divHeadRight" runat="server" style="width: 422px; overflow-y: hidden; overflow-x: hidden;
                    border-right: solid 1px black;">
                    <table id="tableHeadRigth" border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffd9;
                        font-weight: bold; width: 772px;">
                        <tr style="height: 24px;">
                            <td style="width: 98px; border-top: solid 1px black; border-bottom: solid 1px black;
                                border-right: solid 1px gray; text-align: center;">
                                工事会社コード
                            </td>
                            <td style="width: 134px; border-top: solid 1px black; border-bottom: solid 1px black;
                                border-right: solid 1px gray; text-align: center;">
                                工事会社名
                            </td>
                            <td style="width: 65px; border-top: solid 1px black; border-bottom: solid 1px black;
                                border-right: solid 1px gray; text-align: center;">
                                取消
                            </td>
                            <td style="width: 166px; border-top: solid 1px black; border-bottom: solid 1px black;
                                border-right: solid 1px gray; text-align: center;">
                                売上金額
                            </td>
                            <td style="width: 149px; border-top: solid 1px black; border-bottom: solid 1px black;
                                border-right: solid 1px gray; text-align: center;">
                                工事会社請求有無
                            </td>
                            <td style="width: 136px; border-top: solid 1px black; border-bottom: solid 1px black;
                                border-right: solid 1px gray; text-align: center;">
                                請求有無
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <div id="divBodyLeft" runat="server" onmousewheel="wheel();" style="width: 508px;
                    height: 243px; overflow-y: hidden; overflow-x: hidden; margin-top: -1px; border-left: solid 1px black;
                    border-bottom: solid 1px black;">
                    <asp:GridView ID="grdBodyLeft" runat="server" AutoGenerateColumns="False" BackColor="White"
                        CssClass="tableMeiSai" ShowHeader="False" CellPadding="0">
                        <Columns>
                            <asp:BoundField DataField="gyou_no">
                                <ItemStyle Width="50px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="aitesaki">
                                <ItemStyle Width="66px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="aitesaki_cd">
                                <ItemStyle Width="56px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblAitesakiMei" Text='<%#Eval("aitesaki_mei").ToString%>'
                                        ToolTip='<%#Eval("aitesaki_mei").ToString%>' Width="118px" Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="120px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="syouhin_cd">
                                <ItemStyle Width="66px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblSyouhinMei" Text='<%#Eval("syouhin_mei").ToString%>'
                                        ToolTip='<%#Eval("syouhin_mei").ToString %>' Width="118px" Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="120px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:TemplateField>
                        </Columns>
                        <SelectedRowStyle ForeColor="White" />
                        <AlternatingRowStyle BackColor="LightCyan" />
                    </asp:GridView>
                </div>
            </td>
            <td>
                <div id="divBodyRight" runat="server" onmousewheel="wheel();" style="width: 422px;
                    height: 243px; overflow-x: hidden; overflow-y: hidden; margin-top: -1px; border-right: 1px solid black;
                    border-bottom: 1px solid black;">
                    <asp:GridView ID="grdBodyRight" runat="server" AutoGenerateColumns="False" BackColor="White"
                        CssClass="tableMeiSai" ShowHeader="False" CellPadding="0" Width="772px">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                   <asp:Label runat="server" ID="lblkojCd" Text='<%#Eval("koj_cd").ToString%>'
                                        ToolTip='<%#Eval("koj_cd").ToString %>' Width="91px" Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="95px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                   <asp:Label runat="server" ID="lbltyskaisyaMei" Text='<%#Eval("koj_gaisya_mei").ToString%>'
                                        ToolTip='<%#Eval("koj_gaisya_mei").ToString %>' Width="118px" Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="134px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="torikesi">
                                <ItemStyle Width="65px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>                           
                          <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblgaku" Text='<%#Eval("uri_gaku").ToString%>'
                                        ToolTip='<%#Eval("uri_gaku").ToString%>' Width="164px" Style="white-space: normal;
                                        overflow: hidden;"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="166px" Height="21px" HorizontalAlign="Right" BorderColor="#999999" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="koj_gaisya_seikyuu_umu">
                                <ItemStyle Width="154px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="seikyuu_umu">
                                <ItemStyle Width="139px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                        </Columns>
                        <SelectedRowStyle ForeColor="White" />
                        <AlternatingRowStyle BackColor="LightCyan" />
                    </asp:GridView>
                </div>
            </td>
            <td valign="top" style="width: 17px; height: 243px;">
                <div id="divHiddenMeisaiV" runat="server" style="overflow: auto; height: 243px; width: 30px;
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
            <td style="width: 422px">
                <div style="overflow-x: hidden; height: 18px; width: 422px; margin-top: -1px;">
                    <div id="divHiddenMeisaiH" runat="server" style="overflow: auto; height: 18px; width: 439px;
                        margin-top: 0px;" onscroll="fncScrollH();">
                        <table style="width: 771px;">
                            <tr>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
            <td style="height: 16px; width: 17px;">
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hidScroll" runat="server" />
    <asp:HiddenField ID="hidCsvCount" runat="server" Value="" />
</asp:Content>

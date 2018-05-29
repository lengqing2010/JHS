<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master"
    Codebehind="HanyouSiireErrorDetails.aspx.vb" Inherits="Itis.Earth.WebUI.HanyouSiireErrorDetails" Title="汎用仕入エラー確認" %>
    
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
                汎用仕入エラー確認
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
                <div id="divHeadLeft" style="width: 551px; overflow-y: hidden; overflow-x: hidden;">
                    <table id="tableHeadLeft" border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffd9;
                        font-weight: bold;width: 551px;">
                        <tr style="height: 24px;">
                            <td style="width: 60px; border-top: solid 1px black; border-bottom: solid 1px black;
                                border-left: solid 1px black; border-right: solid 1px gray; text-align: center;">
                                行番号
                            </td>
                            <td style="width: 50px; border-top: solid 1px black; border-bottom: solid 1px black;
                                border-right: solid 1px gray; text-align: center;">
                                取消
                            </td>
                            <td style="width: 100px; border-top: solid 1px black; border-bottom: solid 1px black;
                                border-right: solid 1px gray; text-align: center;">
                                加盟店コード
                            </td>
                            <td style="width: 100px; border-top: solid 1px black; border-bottom: solid 1px black;
                                border-right: solid 1px gray; text-align: center;">
                                調査会社コード
                            </td>
                            <td style="width: 140px; border-top: solid 1px black; border-bottom: solid 1px black;
                                border-right: solid 1px gray; text-align: center;">
                                調査会社事業所コード
                            </td>
                            <td style=" border-top: solid 1px black; border-bottom: solid 1px black;
                                border-right: solid 1px gray; text-align: center;">
                                商品コード
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td>
                <div id="divHeadRight" runat="server" style="width: 422px; overflow-y: hidden; overflow-x: hidden;
                    border-right: solid 1px black;">
                    <table id="tableHeadRigth" border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffd9;
                        font-weight: bold; width: 1300px;">
                        <tr style="height: 24px;">
                            <td style="width: 100px; border-top: solid 1px black; border-bottom: solid 1px black;
                                border-right: solid 1px gray; text-align: center;">
                                数量
                            </td>
                            <td style="width: 100px; border-top: solid 1px black; border-bottom: solid 1px black;
                                border-right: solid 1px gray; text-align: center;">
                                単価
                            </td>
                            <td style="width: 100px; border-top: solid 1px black; border-bottom: solid 1px black;
                                border-right: solid 1px gray; text-align: center;">
                                税区分
                            </td>
                            <td style="width: 100px; border-top: solid 1px black; border-bottom: solid 1px black;
                                border-right: solid 1px gray; text-align: center;">
                                消費税額
                            </td>
                            <td style="width: 100px; border-top: solid 1px black; border-bottom: solid 1px black;
                                border-right: solid 1px gray; text-align: center;">
                                仕入年月日
                            </td>
                            <td style="width: 100px; border-top: solid 1px black; border-bottom: solid 1px black;
                                border-right: solid 1px gray; text-align: center;">
                                伝票仕入年月日
                            </td>
                            <td style="width: 100px; border-top: solid 1px black; border-bottom: solid 1px black;
                                border-right: solid 1px gray; text-align: center;">
                                仕入処理FLG
                            </td>
                            <td style="width: 100px; border-top: solid 1px black; border-bottom: solid 1px black;
                                border-right: solid 1px gray; text-align: center;">
                                仕入処理日
                            </td>
                            <td style="width: 100px; border-top: solid 1px black; border-bottom: solid 1px black;
                                border-right: solid 1px gray; text-align: center;">
                                区分
                            </td>
                            <td style="width: 100px;border-top: solid 1px black; border-bottom: solid 1px black;
                                border-right: solid 1px gray; text-align: center;">
                                番号
                            </td>
                            <td style="width: 100px; border-top: solid 1px black; border-bottom: solid 1px black;
                                border-right: solid 1px gray; text-align: center;">
                                施主名
                            </td>
                            <td style="width: 200px;border-top: solid 1px black; border-bottom: solid 1px black;
                                border-right: solid 1px gray; text-align: center;">
                                摘要
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <div id="divBodyLeft" runat="server" onmousewheel="wheel();" style="width: 550px;
                    height: 243px;overflow-y: hidden; overflow-x: hidden;margin-top: -1px; border-left: solid 1px black;
                    border-bottom: solid 1px black;">
                    <asp:GridView ID="grdBodyLeft" runat="server" AutoGenerateColumns="False" BackColor="White"
                        CssClass="tableMeiSai" ShowHeader="False" CellPadding="0" Width="550px">
                        <Columns>
                            <asp:BoundField DataField="gyou_no">
                                <ItemStyle Width="55px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="torikesi">
                                <ItemStyle Width="46px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="kameiten_cd">
                                <ItemStyle Width="96px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tys_kaisya_cd">
                                <ItemStyle Width="96px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tys_kaisya_jigyousyo_cd">
                                <ItemStyle Width="136px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                             <asp:BoundField DataField="syouhin_cd">
                                <ItemStyle  Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                        </Columns>
                        <SelectedRowStyle ForeColor="White" />
                        <AlternatingRowStyle BackColor="LightCyan" />
                    </asp:GridView>
                </div>
            </td>
            <td>
                <div id="divBodyRight" runat="server" onmousewheel="wheel();" style="width: 422px;
                    height: 243px; overflow-y: hidden; overflow-x: hidden; margin-top: -1px; border-right: 1px solid black;
                    border-bottom: 1px solid black;">
                    <asp:GridView ID="grdBodyRight" runat="server" AutoGenerateColumns="False" BackColor="White"
                        CssClass="tableMeiSai" ShowHeader="False" CellPadding="0" Width="1300px">
                        <Columns>                           
                            <asp:TemplateField>
                                <ItemTemplate>
                                   <asp:Label runat="server" ID="lblsuu" Text='<%#Eval("suu").ToString%>'
                                        ToolTip='<%#Eval("suu").ToString %>' Width="95px" Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle  Height="21px" HorizontalAlign="right" BorderColor="#999999" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                   <asp:Label runat="server" ID="lbltanka" Text='<%#Eval("tanka").ToString%>'
                                        ToolTip='<%#Eval("tanka").ToString %>' Width="95px" Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle  Height="21px" HorizontalAlign="right" BorderColor="#999999" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                   <asp:Label runat="server" ID="lblzei_kbn" Text='<%#Eval("zei_kbn").ToString%>'
                                        ToolTip='<%#Eval("zei_kbn").ToString %>' Width="95px" Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle  Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                   <asp:Label runat="server" ID="lblsyouhizei_gaku" Text='<%#Eval("syouhizei_gaku").ToString%>'
                                        ToolTip='<%#Eval("syouhizei_gaku").ToString %>' Width="95px" Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle  Height="21px" HorizontalAlign="right" BorderColor="#999999" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                   <asp:Label runat="server" ID="lblsiire_date" Text='<%#Eval("siire_date").ToString%>'
                                        ToolTip='<%#Eval("siire_date").ToString %>' Width="95px" Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle  Height="21px" HorizontalAlign="right" BorderColor="#999999" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                   <asp:Label runat="server" ID="lbldenpyou_siire_date" Text='<%#Eval("denpyou_siire_date").ToString%>'
                                        ToolTip='<%#Eval("denpyou_siire_date").ToString %>' Width="95px" Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle  Height="21px" HorizontalAlign="right" BorderColor="#999999" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                   <asp:Label runat="server" ID="lblsiire_keijyou_flg" Text='<%#Eval("siire_keijyou_flg").ToString%>'
                                        ToolTip='<%#Eval("siire_keijyou_flg").ToString %>' Width="95px" Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle  Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                   <asp:Label runat="server" ID="lblsiire_keijyou_date" Text='<%#Eval("siire_keijyou_date").ToString%>'
                                        ToolTip='<%#Eval("siire_keijyou_date").ToString %>' Width="95px" Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle  Height="21px" HorizontalAlign="right" BorderColor="#999999" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                   <asp:Label runat="server" ID="lblkbn" Text='<%#Eval("kbn").ToString%>'
                                        ToolTip='<%#Eval("kbn").ToString %>' Width="95px" Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle  Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                   <asp:Label runat="server" ID="lblbangou" Text='<%#Eval("bangou").ToString%>'
                                        ToolTip='<%#Eval("bangou").ToString %>' Width="95px" Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle  Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                   <asp:Label runat="server" ID="lblsesyu_mei" Text='<%#Eval("sesyu_mei").ToString%>'
                                        ToolTip='<%#Eval("sesyu_mei").ToString %>' Width="95px" Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle  Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                   <asp:Label runat="server" ID="lbltekiyou" Text='<%#Eval("tekiyou").ToString%>'
                                        ToolTip='<%#Eval("tekiyou").ToString %>' Width="200px" Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle  Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:TemplateField>
                            
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
                        <table style="width: 1300px;">
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
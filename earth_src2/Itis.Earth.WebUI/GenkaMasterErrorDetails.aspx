<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master"
    Codebehind="GenkaMasterErrorDetails.aspx.vb" Inherits="Itis.Earth.WebUI.GenkaMasterErrorDetails"
    Title="原価マスタエラー確認" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js"></script>

    <script type="text/javascript">
        //window名付与
        var objWin = window;
        objWin.name = "earthMainWindow"
        initPage(); //画面初期設定
    </script>

    <div id="buySelName" runat="server" class="modalDiv" style="position: absolute; left: 300px;
        top: 140px; z-index: 2; display: none;">
    </div>
    <div id="disableDiv" runat="server" style="position: absolute; left: 0px; top: 0px;
        width: 1002px; height: 620px; z-index: 100; filter: alpha(opacity=70); background-color: #000000;
        display: none;">
        <iframe src="about:blank" id="hiddenIframe" width="100%" height="100%"></iframe>
    </div>
    <table style="text-align: left; width: 960px;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tr>
            <th style="width: 210px;">
                原価マスタエラー確認
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
            <td style="width:65px; text-align:left;">
                検索結果：
            </td>
            <td>
                <asp:Label runat="server" ID="lblCount" style="width:auto;"></asp:Label>
            </td>
            <td style="width:20px; text-align:right;">
                件
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <div id="divHeadLeft" style="width: 758px; border-top: solid 1px black; border-left: solid 1px black;
                    border-bottom: solid 1px black; overflow-y: hidden; overflow-x: hidden;">
                    <table id="table1" border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffd9;
                        font-weight: bold;">
                        <tr style="height: 24px;">
                            <td style="width: 54px; border-top: none; border-bottom: none; border-left: none;
                                border-right: solid 1px gray; text-align: center;">
                                行番号
                            </td>
                            <td style="width: 65px; border-top: none; border-bottom: none; border-left: none;
                                border-right: solid 1px gray; text-align: center;">
                                調コード
                            </td>
                            <td style="width: 120px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                調査会社名
                            </td>
                            <td style="width: 70px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                先種別
                            </td>
                            <td style="width: 62px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                先コード
                            </td>
                            <td style="width: 84px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                相手先名
                            </td>
                            <td style="width: 55px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                商品
                            </td>
                            <td style="width: 120px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                商品名
                            </td>
                            <td style="width: 120px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                調査方法
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td>
                <div id="divHeadRight" runat="server" style="width: 200px; border-top: solid 1px black;
                    border-bottom: solid 1px black; border-right: solid 1px black; overflow-y: hidden;
                    overflow-x: hidden;">
                    <table id="table2" border="0" cellpadding="0" cellspacing="0" style="width: 2860px;
                        background-color: #ffffd9; font-weight: bold;">
                        <tr style="height: 24px;">
                            <td style="width: 55px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                取消
                            </td>
                            <td style="width: 100px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                棟価格1
                            </td>
                            <td style="width: 80px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                価格変更
                            </td>
                            <td style="width: 100px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                棟価格2
                            </td>
                            <td style="width: 80px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                価格変更
                            </td>
                            <td style="width: 100px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                棟価格3
                            </td>
                            <td style="width: 80px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                価格変更
                            </td>
                            <td style="width: 100px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                棟価格4
                            </td>
                            <td style="width: 80px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                価格変更
                            </td>
                            <td style="width: 100px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                棟価格5
                            </td>
                            <td style="width: 80px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                価格変更
                            </td>
                            <td style="width: 100px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                棟価格6
                            </td>
                            <td style="width: 80px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                価格変更
                            </td>
                            <td style="width: 100px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                棟価格7
                            </td>
                            <td style="width: 80px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                価格変更
                            </td>
                            <td style="width: 100px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                棟価格8
                            </td>
                            <td style="width: 80px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                価格変更
                            </td>
                            <td style="width: 100px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                棟価格9
                            </td>
                            <td style="width: 80px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                価格変更
                            </td>
                            <td style="width: 100px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                棟価格10
                            </td>
                            <td style="width: 80px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                価格変更
                            </td>
                            <td style="width: 115px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                棟価格11～19
                            </td>
                            <td style="width: 80px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                価格変更
                            </td>
                            <td style="width: 115px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                棟価格20～29
                            </td>
                            <td style="width: 80px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                価格変更
                            </td>
                            <td style="width: 115px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                棟価格30～39
                            </td>
                            <td style="width: 80px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                価格変更
                            </td>
                            <td style="width: 115px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                棟価格40～49
                            </td>
                            <td style="width: 80px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                価格変更
                            </td>
                            <td style="width: 100px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                棟価格50～
                            </td>
                            <td style="border-top: none; border-bottom: none; border-right: solid 1px gray; text-align: center;">
                                価格変更
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <div id="divBodyLeft" runat="server" onmousewheel="wheel();" style="width: 758px;
                    height: 221px; border-left: solid 1px black; border-bottom: solid 1px black;
                    overflow-y: hidden; overflow-x: hidden; margin-top: -1px;">
                    <asp:GridView ID="grdBodyLeft" runat="server" AutoGenerateColumns="False" BackColor="White"
                        ShowHeader="False" CellPadding="0" CssClass="tableMeiSai" Style="width: 758px;">
                        <Columns>
                            <asp:BoundField DataField="gyou_no">
                                <ItemStyle Width="48px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemStyle Width="61px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTysKaisyaCd" runat="server" Width="56px" Text='<%#Eval("tys_kaisya_cd")%>'
                                        ToolTip='<%#Eval("tys_kaisya_cd")%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="116px" Height="21px" HorizontalAlign="Left" BorderColor="#999999"
                                    Font-Size="12px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTysKaisyaCd" runat="server" Width="111px" Text='<%#Eval("tys_kaisya_mei")%>'
                                        ToolTip='<%#Eval("tys_kaisya_mei")%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="66px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTysKaisyaCd" runat="server" Width="61px" Text='<%#Eval("aitesaki_syubetu")%>'
                                        ToolTip='<%#Eval("aitesaki_syubetu")%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="58px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTysKaisyaCd" runat="server" Width="53px" Text='<%#Eval("aitesaki_cd")%>'
                                        ToolTip='<%#Eval("aitesaki_cd")%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="80px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTysKaisyaCd" runat="server" Width="75px" Text='<%#Eval("aitesaki_mei")%>'
                                        ToolTip='<%#Eval("aitesaki_mei")%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="51px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTysKaisyaCd" runat="server" Width="46px" Text='<%#Eval("syouhin_cd")%>'
                                        ToolTip='<%#Eval("syouhin_cd")%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="116px" Height="21px" HorizontalAlign="Left" BorderColor="#999999"
                                    Font-Size="12px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTysKaisyaCd" runat="server" Width="111px" Text='<%#Eval("syouhin_mei")%>'
                                        ToolTip='<%#Eval("syouhin_mei")%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Height="21px" HorizontalAlign="Left" BorderColor="#999999" Font-Size="12px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTysKaisyaCd" runat="server" Width="111px" Text='<%#Eval("tys_houhou")%>'
                                        ToolTip='<%#Eval("tys_houhou")%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle BackColor="LightCyan" />
                    </asp:GridView>
                </div>
            </td>
            <td>
                <div id="divBodyRight" runat="server" onmousewheel="wheel();" style="width: 200px;
                    height: 221px; border-bottom: solid 1px black; border-right: solid 1px black;
                    overflow: scroll; margin-top: -1px; overflow-x: hidden; overflow-y: hidden;">
                    <asp:GridView ID="grdBodyRight" runat="server" AutoGenerateColumns="False" BackColor="White"
                        ShowHeader="False" CellPadding="0" CssClass="tableMeiSai" Style="width: 2861px;
                        border-right: solid 1px gray; padding-right: 4px; margin-left: -1px;">
                        <Columns>
                            <asp:BoundField DataField="torikesi">
                                <ItemStyle Width="47px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk1">
                                <ItemStyle Width="92px" Height="21px" HorizontalAlign="Right" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk_henkou_flg1">
                                <ItemStyle Width="72px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk2">
                                <ItemStyle Width="92px" Height="21px" HorizontalAlign="Right" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk_henkou_flg2">
                                <ItemStyle Width="72px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk3">
                                <ItemStyle Width="92px" Height="21px" HorizontalAlign="Right" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk_henkou_flg3">
                                <ItemStyle Width="72px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk4">
                                <ItemStyle Width="92px" Height="21px" HorizontalAlign="Right" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk_henkou_flg4">
                                <ItemStyle Width="72px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk5">
                                <ItemStyle Width="92px" Height="21px" HorizontalAlign="Right" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk_henkou_flg5">
                                <ItemStyle Width="72px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk6">
                                <ItemStyle Width="92px" Height="21px" HorizontalAlign="Right" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk_henkou_flg6">
                                <ItemStyle Width="72px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk7">
                                <ItemStyle Width="92px" Height="21px" HorizontalAlign="Right" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk_henkou_flg7">
                                <ItemStyle Width="72px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk8">
                                <ItemStyle Width="92px" Height="21px" HorizontalAlign="Right" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk_henkou_flg8">
                                <ItemStyle Width="72px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk9">
                                <ItemStyle Width="92px" Height="21px" HorizontalAlign="Right" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk_henkou_flg9">
                                <ItemStyle Width="72px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk10">
                                <ItemStyle Width="92px" Height="21px" HorizontalAlign="Right" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk_henkou_flg10">
                                <ItemStyle Width="72px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk11t19">
                                <ItemStyle Width="107px" Height="21px" HorizontalAlign="Right" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk_henkou_flg11t19">
                                <ItemStyle Width="72px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk20t29">
                                <ItemStyle Width="107px" Height="21px" HorizontalAlign="Right" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk_henkou_flg20t29">
                                <ItemStyle Width="72px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk30t39">
                                <ItemStyle Width="107px" Height="21px" HorizontalAlign="Right" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk_henkou_flg30t39">
                                <ItemStyle Width="72px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk40t49">
                                <ItemStyle Width="107px" Height="21px" HorizontalAlign="Right" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk_henkou_flg40t49">
                                <ItemStyle Width="72px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk50t">
                                <ItemStyle Width="92px" Height="21px" HorizontalAlign="Right" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk_henkou_flg50t">
                                <ItemStyle Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                        </Columns>
                        <AlternatingRowStyle BackColor="LightCyan" />
                    </asp:GridView>
                </div>
            </td>
            <td valign="top" style="width: 17px; height: 221px;">
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
            <td style="width: 201px;">
                <div style="overflow-x: hidden; height: 18px; width: 201px; margin-top: -1px;">
                    <div id="divHiddenMeisaiH" runat="server" style="overflow: auto; height: 18px; width: 218px;
                        margin-top: 0px;" onscroll="fncScrollH();">
                        <table style="width: 2860px;">
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
    <asp:HiddenField ID="hidCsvOut" runat="server" Value="" />
</asp:Content>

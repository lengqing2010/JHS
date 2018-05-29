<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master" CodeBehind="HanbaiKakakuMasterErrorDetails.aspx.vb" Inherits="Itis.Earth.WebUI.HanbaiKakakuMasterErrorDetails" 
    title="販売価格マスタエラー確認" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" src="js/jhsearth.js"></script>
    <script type="text/javascript">
        //window名付与
        var objWin = window;
        objWin.name = "earthMainWindow"
        initPage(); //画面初期設定
    </script>
    <table style="text-align:left; width:960px;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tr>
            <th style="width:210px;">
                販売価格マスタエラー確認
            </th>
            <th style="width:70px;">
                <asp:Button ID="btnClose" runat="server" Text="閉じる" Style="height:25px; padding-top:2px;" />
            </th>
            <th style="width:70px;">
                <asp:Button ID="btnCsvOutput" runat="server" Text="CSV出力" Style="height:25px; padding-top:2px;" />
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
    <table style="width:400px; text-align:left;" class="mainTable paddinNarrow" cellpadding="1" >
        <tr>
            <td style="width:120px; height:25px; background-color:#ccffff;">
                取込日時
            </td>
            <td>
                <asp:Label ID="lblSyoriDate" runat="server" Text="" Width="280px" style="padding-left:4px;"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width:120px; height:25px; background-color:#ccffff;">
                取込ファイル名
            </td>
            <td>
                <asp:Label ID="lblFileMei" runat="server" Text="" Width="280px" style="padding-left:4px; white-space:nowrap; overflow:hidden; text-overflow:ellipsis;"></asp:Label>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" style="margin-top:10px;">
        <tr>
            <td style="width:65px;">
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
                <div id="divHeadLeft" style="width:664px; overflow-y:hidden; overflow-x:hidden;">
                    <table id="tableHeadLeft" border="0" cellpadding="0" cellspacing="0" style="background-color:#ffffd9;
                        font-weight:bold;">
                        <tr style="height:24px;">
                            <td style="width:54px; border-top:solid 1px black; border-bottom:solid 1px black; border-left:solid 1px black;
                                border-right:solid 1px gray; text-align:center;">
                                行番号
                            </td>
                            <td style="width:70px; border-top:solid 1px black; border-bottom:solid 1px black; border-right:solid 1px gray;
                                text-align:center;">
                                先種別
                            </td>
                            <td style="width:60px; border-top:solid 1px black; border-bottom:solid 1px black; border-right:solid 1px gray;
                                text-align:center;">
                                先コード
                            </td>
                            <td style="width:124px; border-top:solid 1px black; border-bottom:solid 1px black; border-right:solid 1px gray;
                                text-align:center;">
                                相手先名
                            </td>
                            <td style="width:70px; border-top:solid 1px black; border-bottom:solid 1px black; border-right:solid 1px gray;
                                text-align:center;">
                                商品
                            </td>
                            <td style="width:124px; border-top:solid 1px black; border-bottom:solid 1px black; border-right:solid 1px gray;
                                text-align:center;">
                                商品名
                            </td>
                            <td style="width:154px; border-top:solid 1px black; border-bottom:solid 1px black; border-right:solid 1px gray;
                                text-align:center;">
                                調査方法
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td>
                <div id="divHeadRight" runat="server" style="width:267px; overflow-y:hidden; overflow-x:hidden; border-right:solid 1px black;">
                     <table id="tableHeadRigth" border="0" cellpadding="0" cellspacing="0" style="background-color:#ffffd9; 
                        font-weight:bold; width:537px;">
                        <tr style="height:24px;">
                           <td style="width:60px; border-top:solid 1px black; border-bottom:solid 1px black; border-right:solid 1px gray;
                                text-align:center;">
                                取消
                            </td>
                            <td style="width:124px; border-top:solid 1px black; border-bottom:solid 1px black; border-right:solid 1px gray;
                                text-align:center;">
                                工務店請求金額
                            </td>
                            <td style="width:80px; border-top:solid 1px black; border-bottom:solid 1px black; border-right:solid 1px gray;
                                text-align:center;">
                                金額変更
                            </td>
                           <td style="width:108px; border-top:solid 1px black; border-bottom:solid 1px black; border-right:solid 1px gray;
                                text-align:center;">
                                実請求金額
                            </td>
                            <td style="width:80px; border-top:solid 1px black; border-bottom:solid 1px black; border-right:solid 1px gray;
                                text-align:center;">
                                金額変更
                            </td>
                             <td style="width:78px; border-top:solid 1px black; border-bottom:solid 1px black; border-right:solid 1px gray;
                                text-align:center;">
                                公開フラグ
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td style="vertical-align:top;">
                <div id="divBodyLeft" runat="server" onmousewheel="wheel();" style="width:663px; height:243px; overflow-y:hidden; 
                overflow-x:hidden; margin-top:-1px; border-left:solid 1px black; border-bottom:solid 1px black;">
                    <asp:GridView ID="grdBodyLeft" runat="server" AutoGenerateColumns="False" BackColor="White" CssClass="tableMeiSai"
                        ShowHeader="False" CellPadding="0">
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
                                <ItemTemplate >
                                    <asp:Label runat="server" ID="lblAitesakiMei" Text='<%#Eval("aitesaki_mei").ToString%>' ToolTip='<%#Eval("aitesaki_mei").ToString%>' Width="118px" style="white-space:nowrap; overflow:hidden; text-overflow:ellipsis;"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="120px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="syouhin_cd">
                                <ItemStyle Width="66px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblSyouhinMei" Text='<%#Eval("syouhin_mei").ToString%>' ToolTip='<%#Eval("syouhin_mei").ToString %>' Width="118px" style="white-space:nowrap; overflow:hidden; text-overflow:ellipsis;"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="120px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblTysHouhou" Text='<%#Eval("tys_houhou").ToString%>' ToolTip='<%#Eval("tys_houhou").ToString %>' Width="148px"  style="white-space:nowrap; overflow:hidden; text-overflow:ellipsis;"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="150px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:TemplateField>
                        </Columns>
                        <SelectedRowStyle ForeColor="White" />
		                <AlternatingRowStyle BackColor="LightCyan" />
                    </asp:GridView>
               </div>
            </td>
            <td>
                <div id="divBodyRight" runat="server" onmousewheel="wheel();" style="width:267px; height:243px; overflow-x:hidden;
                    overflow-y:hidden; margin-top:-1px; border-right:1px solid black; border-bottom:1px solid black;">
                    <asp:GridView ID="grdBodyRight" runat="server" AutoGenerateColumns="False" BackColor="White"
                         CssClass="tableMeiSai" ShowHeader="False" CellPadding="0" Width="537px">
                        <Columns>
                            <asp:BoundField DataField="torikesi">
                                <ItemStyle Width="55px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="koumuten_seikyuu_gaku">
                                <ItemStyle Width="116px" Height="21px" HorizontalAlign="Right" BorderColor="#999999" CssClass="kingakuMeiSai" />
                            </asp:BoundField>
                            <asp:BoundField DataField="koumuten_seikyuu_gaku_henkou_flg">
                                <ItemStyle Width="76px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="jitu_seikyuu_gaku">
                                <ItemStyle Width="100px" Height="21px" HorizontalAlign="Right" BorderColor="#999999" CssClass="kingakuMeiSai" />
                            </asp:BoundField>
                            <asp:BoundField DataField="jitu_seikyuu_gaku_henkou_flg">
                                <ItemStyle Width="76px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="koukai_flg">
                                <ItemStyle Width="74px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                        </Columns>
                        <SelectedRowStyle ForeColor="White" />
	                    <AlternatingRowStyle BackColor="LightCyan" />
                    </asp:GridView>
                </div>
            </td>
            <td valign="top" style="width:17px; height:243px;">
                <div id="divHiddenMeisaiV" runat="server" style="overflow:auto; height:243px; width:30px; margin-left:-14px;" onscroll="fncScrollV();">
                    <table height="<%=scrollHeight%>">
                        <tr><td></td></tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td style="height:16px;">
            </td>
            <td style="width:267px">
                <div style="overflow-x:hidden; height:18px; width:267px; margin-top:-1px;">
                    <div id="divHiddenMeisaiH" runat="server" style="overflow:auto; height:18px; width:284px; margin-top:0px;" onscroll="fncScrollH();">
                        <table style="width:537px;">
                            <tr><td></td></tr>
                        </table>
                    </div>
                </div>
            </td>
            <td style="height:16px; width:17px;">
            </td>
        </tr>
    </table> 
    <asp:HiddenField ID="hidScroll" runat="server" />
    <asp:HiddenField ID="hidCsvCount" runat="server" Value=""  />
</asp:Content>

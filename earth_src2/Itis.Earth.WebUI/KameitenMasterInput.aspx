<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master" CodeBehind="KameitenMasterInput.aspx.vb" Inherits="Itis.Earth.WebUI.KameitenMasterInput" 
    title="加盟店情報一括取込" %>
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
            <th style="width: 350px;">
                加盟店情報一括取込
            </th>
            <th style="width: 70px;">
                <asp:Button ID="btnClose" runat="server" Text="閉じる" Style="height: 25px; padding-top: 2px;" />
            </th>
            <th>
            </th>
        </tr>
        <tr>
            <td colspan="3" rowspan="1">
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0">
        <tr style="vertical-align: top;">
            <td>
                <table cellpadding="0" cellspacing="0" style="width: 570px;">
                    <tr>
                        <td style="border: 2px solid gray; font-size: 14px; background-color: #ffffd9; font-weight: bold;
                            height: 20px; text-align: center;">
                            取込エラー確認
                        </td>
                    </tr>
                    <tr>
                        <td style="border: 2px solid gray; border-top: none; background-color: #e6e6e6; padding-left: 5px;">
                            <table border="0" cellpadding="0" cellspacing="0" style="margin-top: 10px;">
                                <tr>
                                    <td style="width: 65px;">
                                        検索結果：&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblCount"></asp:Label>&nbsp;件
                                    </td>
                                </tr>
                            </table>
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <table id="tableHeadLeft" border="0" cellpadding="0" cellspacing="0" style="width: 540px;
                                            background-color: #ffffd9; font-weight: bold;">
                                            <tr style="height: 22px;">
                                                <td style="width: 149px; border-top: solid 1px gray; border-bottom: solid 1px gray;
                                                    border-left: solid 1px gray; border-right: solid 1px gray; text-align: center;">
                                                    取込日時
                                                </td>
                                                <td style="width: 280px; border-top: solid 1px gray; border-bottom: solid 1px gray;
                                                    border-right: solid 1px gray; text-align: center;">
                                                    取込ファイル名
                                                </td>
                                                <td style="border-top: solid 1px gray; border-bottom: solid 1px gray; border-right: solid 1px gray;
                                                    text-align: center;">
                                                    取込エラー有無
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; width: 540px;">
                                        <div style="width: 557px; height: 308px; border-bottom: solid 1px gray; overflow: auto;
                                            margin-top: -1px;">
                                            <asp:GridView ID="grdInputKanri" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                ShowHeader="False" CellPadding="0" CssClass="tableMeiSai" Style="border-left: solid 1px gray;
                                                width: 540px;">
                                                <Columns>
                                                    <asp:BoundField DataField="syori_datetime">
                                                        <ItemStyle Width="145px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField>
                                                        <ItemStyle Width="276px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="nyuuryoku_file_mei" runat="server" Width="271px" Text='<%#Eval("nyuuryoku_file_mei")%>'
                                                                ToolTip='<%#Eval("nyuuryoku_file_mei")%>' Style="white-space: nowrap; overflow: hidden;
                                                                text-overflow: ellipsis;"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton1" runat="server" Text='<%#Eval("error_umu")%>' CommandName="LinkButton">LinkButton</asp:LinkButton>
                                                            <asp:HiddenField ID="hidEdi_Sakusei_bi" runat="server" Value='<%#Eval("edi_jouhou_sakusei_date") %>' />
                                                            <asp:HiddenField ID="hidTorikomibi" runat="server" Value='<%#Eval("torikomi_datetime") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle Height="21px" HorizontalAlign="Center" BorderColor="#999999" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <AlternatingRowStyle BackColor="LightCyan" />
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                                <tr style="height: 10px;">
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 10px;">
            </td>
            <td>
                <table cellpadding="0" cellspacing="0" style="width: 370px; text-align: left;">
                    <tr>
                        <td style="border: 2px solid gray; font-size: 14px; background-color: #ffffd9; font-weight: bold;
                            height: 20px; text-align: center;">
                            取込処理
                        </td>
                    </tr>
                    <tr>
                        <td style="border: 2px solid gray; border-top: none; background-color: #e6e6e6; text-align: center;">
                            <table border="0" cellpadding="0" cellspacing="0" style="margin-top: 20px;">
                                <tr>
                                    <td>
                                        <asp:FileUpload ID="fupCsvinput" runat="server" Style="width: 320px; ime-mode: disabled;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnCsvInput" runat="server" Text="CSV取込" Style="margin-top: 10px;" />
                                        <asp:Button ID="btnCsvCheck" runat="server"  Style="margin-top: 10px;display:none;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 15px;">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hidLineNo" runat="server"/>
    <%--<asp:HiddenField ID="hidExitsFlg" runat="server" />--%>
    <asp:HiddenField ID="hidInsLineNo" runat="server" />
</asp:Content>

﻿<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master"
    Codebehind="HanyouUriageInput.aspx.vb" Inherits="Itis.Earth.WebUI.HanyouUriageInput"
    Title="汎用売上データ取込" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js"></script>

    <script type="text/javascript">
        //window名付与
        var objWin = window;
        objWin.name = "earthMainWindow"
        initPage(); //画面初期設定
    </script>
<div id="buySelName" runat="server" class="modalDiv" style="position:absolute; left:300px; top:140px; z-index:2; display:none;">
    </div>
    <div id="disableDiv" runat="server" style="position:absolute; left:0px; top:0px; width:1002px; height:620px; z-index:100; FILTER:alpha(opacity=70); background-color:#000000; display:none;">
        <iframe src="about:blank" id="hiddenIframe" width="100%" height="100%"></iframe>
    </div>
    <table style="text-align: left; width: 960px;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tr>
            <th style="width: 180px;">
                汎用売上データ取込</th>
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
                                        <asp:Label runat="server" ID="lblCount" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;件
                                    </td>
                                </tr>
                            </table>
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <table id="tableHeadLeft" border="0" cellpadding="0" cellspacing="0" style="width: 540px;
                                            background-color: #ffffd9; font-weight: bold;">
                                            <tr style="height: 22px;">
                                                <td style="width: 150px; border-top: solid 1px gray; border-bottom: solid 1px gray;
                                                    border-left: solid 1px gray; border-right: solid 1px gray; text-align: center;">
                                                    取込日時
                                                </td>
                                                <td style="width: 280px; border-top: solid 1px gray; border-bottom: solid 1px gray;
                                                    border-right: solid 1px gray; text-align: center;">
                                                    取込ファイル名
                                                </td>
                                                <td style="width: 106px; border-top: solid 1px gray; border-bottom: solid 1px gray;
                                                    border-right: solid 1px gray; text-align: center;">
                                                    取込エラー有無
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="width: 557px; height: 308px; border-bottom: solid 1px gray; overflow: auto;
                                            margin-top: -1px;">
                                            <asp:GridView ID="grdUploadKanri" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                CssClass="tableMeiSai" ShowHeader="False" CellPadding="0" Style="border-left: solid 1px gray;
                                                width: 540px;">
                                                <Columns>
                                                    <asp:BoundField DataField="syori_datetime">
                                                        <ItemStyle Width="146px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Label ID='lblNyuuryokuFileMei' runat="server" Text='<%#Eval("nyuuryoku_file_mei").ToString%>'
                                                                ToolTip='<%#Eval("nyuuryoku_file_mei").ToString%>' Width="174px" Style="white-space: nowrap;
                                                                overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="276px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUmu" runat="server" Text='<%#Eval("error_umu").ToString%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="102px" Height="21px" HorizontalAlign="Center" BorderColor="#999999" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="torikomi_datetime">
                                                        <ItemStyle Width="0px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="edi_jouhou_sakusei_date">
                                                        <ItemStyle Width="0px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <SelectedRowStyle ForeColor="White" />
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
                                        <asp:FileUpload ID="fupCsvUpload" runat="server" Style="width: 320px;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnCsvUpload" runat="server" Text="CSV取込" Style="margin-top: 10px;" />
                                        <asp:Button ID="btnUpload" runat="server" Text="" Style="display:none;" />
                                        <asp:HiddenField ID="hidFile" runat="server" />
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
</asp:Content>

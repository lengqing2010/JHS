<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="EditConfig.aspx.vb" Inherits="Itis.Earth.WebUI.EditConfigForm1" Title="Edit Web.config" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">
    <table style="border: 1px solid gray; background-color: #EEEEEE; width: 840px;">
        <tr>
            <td>
                <asp:Label ID="LabelMessage" runat="server" Font-Bold="True" ForeColor="Red" Width="595px"></asp:Label></td>
        </tr>
    </table>
    <br />
    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tbody>
            <tr>
                <th style="text-align: left">
                    アプリケーション設定編集</th>
            </tr>
            <tr>
                <td style="height: 10px">
                </td>
            </tr>
        </tbody>
    </table>
    <table style="text-align: left; width: 780px;" id="" class="mainTable" cellpadding="1"
        cellspacing="1" border="0">
        <!-- ヘッダ部 -->
        <thead>
            <tr>
                <th class="tableTitle" style="padding: 0px; background-color: #ffffce;" colspan="2">
                    メール設定
                </th>
            </tr>
        </thead>
        <!-- ボディ部 -->
        <tbody id="Tbody1" class="scrolltablestyle">
            <!-- 1行目 -->
            <tr>
                <td style="width: 150px; height: 26px;" class="koumokuMei">
                    SMTPサーバー名
                </td>
                <td style="height: 26px">
                    &nbsp;<asp:TextBox ID="TextSmtpAddress" runat="server" Width="292px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 150px" class="koumokuMei">
                    送信者アドレス
                </td>
                <td>
                    &nbsp;<asp:TextBox ID="TextMailFromAddress" runat="server" Width="293px"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2" class="tableFooter" style="padding: 0px;">
                    <!-- 画面下部・ボタン -->
                    <table cellpadding="5" cellspacing="0" class="subTable" style="width: 100%; height: 50px;"
                        border="0">
                        <tr style="height: 50px;">
                            <td>
                                <br />
                                <input type="button" name="btnFunc6" id="ButtonReload" value="設定ファイル再読み込み" style="width: 180px;
                                    height: 30px" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
                                <input type="button" name="btnFunc6" id="ButtonUpdate" value="設定ファイル更新" style="width: 180px;
                                    height: 30px" runat="server" /><br />
                                <br />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
    <table style="text-align: left; width: 780px; display: none;" id="Table2" class="mainTable"
        cellpadding="1" cellspacing="1" border="0">
        <thead>
            <tr>
                <th class="tableTitle" style="padding: 0px; background-color: #ffffce;" colspan="2">
                    売上／仕入データ作成ファイル設定
                    <br />
                    <span style="font-size: 9px; color: Red;">※サーバー側の設定です．クライアント側の保存先ではありませんのでご注意下さい</span>
                </th>
            </tr>
        </thead>
        <!-- ボディ部 -->
        <tbody id="Tbody2" class="scrolltablestyle">
            <!-- 1行目 -->
            <tr>
                <td style="width: 150px" class="koumokuMei">
                    保存先フォルダパス
                </td>
                <td>
                    <input type="text" name="Setting01" id="TextOutputPath" style="width: 300px;" maxlength="256"
                        runat="server" />
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>

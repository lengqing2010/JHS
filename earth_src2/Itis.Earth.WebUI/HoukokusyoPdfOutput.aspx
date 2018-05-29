<%@ Page Language="vb" AutoEventWireup="false" Codebehind="HoukokusyoPdfOutput.aspx.vb"
    Inherits="Itis.Earth.WebUI.HoukokusyoPdfOutput" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>報告書出力</title>
</head>
<body>
    <form id="form1" runat="server" style="text-align: center;">
        <div id="divWord" style="text-align: center; width: 500px;">
            <font color="SteelBlue" size="6">
                <marquee scrollamount="20">
                    報告書PDF作成中です...
            </marquee>
            </font>
        </div>
        <div style="display: none;">
            1
            <iframe id="hiddenIframe" runat="server" title="modalDiv" width="100%" height="100%">
            </iframe>
            2
        </div>
        <div style="height: 30px; text-align: center; vertical-align: middle;">
            <div style="width: 500px; height: 20px; border-top: solid 1px gray; border-bottom: solid 1px gray;
                border-left: solid 1px gray; border-right: solid 1px gray; text-align: left;">
                <div id="divProgressBar" runat="server" style="width: 0%; height: 20px; background-color: #00ff00;">
                </div>
                <div style="position: relative; width: 500px; height: 20px; top: -20px; left: 0px;
                    text-align: center; vertical-align: middle;">
                    <asp:Label ID="lblProgress" runat="server" Font-Italic="true" Style="background-color: Transparent;"></asp:Label>
                </div>
            </div>
        </div>
        <div style="width: 500px; height: 400px; text-align: left; overflow: auto;">
            <asp:GridView ID="grdMsg" runat="server" AutoGenerateColumns="False" ShowHeader="False"
                BorderStyle="none" BorderWidth="0px" CellPadding="2" Style="font-size: 12px;
                font-family: ＭＳ ゴシック;">
                <Columns>
                    <asp:BoundField DataField="no">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="msg" />
                    <asp:BoundField DataField="err_flg" />
                </Columns>
            </asp:GridView>
        </div>
        <asp:Button ID="btnStart" runat="server" Text="Start" Style="display: none;" />
        <asp:Button ID="btnOpenFile" runat="server" Text="copy" Style="display: none;" />
        <asp:Button ID="btnCopy" runat="server" Text="copy" Style="display: none;" />
        <asp:HiddenField ID="hidKanriNo" runat="server" />
        <asp:HiddenField ID="hidIndex" runat="server" />
    </form>
</body>
</html>

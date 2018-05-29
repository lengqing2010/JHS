<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SoufujyouPdfOutput.aspx.vb"
    Inherits="Itis.Earth.WebUI.SoufujyouPdfOutput" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>送付状出力</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="divWord" style="text-align:center;">
            <font color="SteelBlue" size="6">
                <marquee  scrollamount="20">
                    送付状PDF作成中です...
            </marquee>
            </font>
        </div>
        <div style="display: none;">
            1
            <iframe id="hiddenIframe" runat="server" title="modalDiv" width="100%" height="100%">
            </iframe>
            2
        </div>
        <asp:Button ID="btnOpenFile" runat="server" Text="copy" Style="display: none;" />
        <asp:Button ID="btnCopy" runat="server" Text="copy" Style="display: none;" />
    </form>
</body>
</html>

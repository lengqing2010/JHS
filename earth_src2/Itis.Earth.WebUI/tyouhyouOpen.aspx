<%@ Page Language="vb" AutoEventWireup="false" Codebehind="tyouhyouOpen.aspx.vb"
    Inherits="Itis.Earth.WebUI.tyouhyouOpen" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>調査見積書</title>

    <script language="javascript" type="text/javascript">
    
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div style="display: none;">
            1
            <iframe id="hiddenIframe" runat="server" title="modalDiv" width="100%" height="100%">
            </iframe>
            2
        </div>
        <asp:Button ID="btnTest" runat="server" Text="copy" Style="display: none;" />
        <a id="file" href="" runat="server" style="display: none;">プレビュー</a>
    </form>
</body>
</html>

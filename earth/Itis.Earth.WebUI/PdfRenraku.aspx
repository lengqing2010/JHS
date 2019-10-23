<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PdfRenraku.aspx.vb" Inherits="Itis.Earth.WebUI.PdfRenraku" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>EARTH 調査予定連絡書表示</title>
</head>
<body>

    <script type="text/javascript">
       window.focus();
    </script>

    <form id="form1" runat="server">
        <div>
            <input id="kbn" runat="server" type="hidden" />
            <input id="hosyouno" runat="server" type="hidden" />
            <input id="accountno" runat="server" type="hidden" />
            <input id="stdate" runat="server" type="hidden" />
        </div>
    </form>
</body>
</html>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WaitMsg.aspx.vb" Inherits="Itis.Earth.WebUI.WaitMsg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>無題のページ</title>
</head>
<body bgcolor="LightBlue">
    <form id="form1" runat="server">
    <div>
        <font color="SteelBlue" size="6">
            <marquee scrollamount="15">
            ただいまダウンロード中です。しばらくお待ちください。
            </marquee>
        </font>
    </div>
    <iframe height="0" width="0" src="<%= htmlQuery %>">
    </iframe>
    </form>
</body>
</html>

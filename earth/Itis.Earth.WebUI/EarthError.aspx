<%@ Page Language="vb" AutoEventWireup="false" Codebehind="EarthError.aspx.vb" Inherits="Itis.Earth.WebUI.EarthError" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Expires" content="-1" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>EARTH エラー</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table id="mainMenuTable" style="width: 100%;" cellpadding="0" cellspacing="0">
                <tr class="masterHeader">
                    <td style="width: 220px; text-align: center;">
                        <img src="images/jhs_earth_logo1.gif" alt="logo" /></td>
                    <td>
                        <table style="width: 100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="text-align: right;" colspan="2">
                                    <table cellpadding="2" style="width: 481px">
                                        <tr>
                                            <td style="width: 62px; height: 6px;">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 5px">
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    <table class="menuLinkBar" cellpadding="2">
                                        <tr>
                                            <td id="menu_lnk_main">
                                                <a id="linkMain" runat="server" href="main.aspx">メイン</a></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <table>
                <tr>
                    <td>
                        <span id="SpanErrMess" runat="server" style="color: Red; font-size: 16px;">処理実行中に予期せぬ問題が発生しました。システム管理者にご連絡ください。</span>
                        <br />
                        <br />
                        <span id="SpanErrTime" runat="server" style="font-size: 16px;"></span>
                        <br />
                        <span id="SpanErrPath" runat="server" style="font-size: 16px;"></span>
                        <br />
                        <code id="CodeErrMess" runat="server" style="">
                            <pre id="PreErrMess" runat="server" style="font-size: 12px; font-family: Sans-Serif"></pre>
                        </code>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

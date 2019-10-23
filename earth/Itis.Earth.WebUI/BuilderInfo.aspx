<%@ Page Language="vb" AutoEventWireup="false" Codebehind="BuilderInfo.aspx.vb" Inherits="Itis.Earth.WebUI.BuilderInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>ビルダー情報</title>
</head>

<script type="text/javascript" src="js/jhsearth.js">
</script>

<script type="text/javascript">
    window.resizeTo(800,500);

    function returnSelectValue(){
        void(0);
    }
</script>

<body>
    <form id="form1" runat="server">
        <div>
            ビルダー情報
            <br />
            <input id="btnCloseWin" type="button" value="閉じる" />
            <input type="hidden" id="kameitenCd" runat="server" />
            <br />
            <br />
            <table style="height: 30px">
                <tr>
                    <td>
                        取得情報：
                    </td>
                    <td id="resultCount" runat="server">
                    </td>
                    <td>
                        件
                    </td>
                </tr>
            </table>
            <div class="dataGridHeader" id="dataGridContent">
                <table class="scrolltablestyle" cellpadding="0" cellspacing="0">
                    <thead>
                        <tr id="meisaiTableHeaderTr" style="position: relative; top: expression(this.offsetParent.scrollTop);">
                            <th class="searchReturnValues">
                            </th>
                            <th style="width: 100px;" nowrap>
                                注意事項種別</th>
                            <th style="width: 60px;">
                                入力日</th>
                            <th style="width: 100px;">
                                受付者</th>
                            <th style="width: 400px;">
                                内容</th>
                        </tr>
                    </thead>
                    <tbody id="searchGrid" runat="server">
                    </tbody>
                </table>
            </div>
        </div>
    </form>
</body>
</html>

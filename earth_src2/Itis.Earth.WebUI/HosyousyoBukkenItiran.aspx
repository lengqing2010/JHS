<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="HosyousyoBukkenItiran.aspx.vb" Inherits="Itis.Earth.WebUI.HosyousyoBukkenItiran" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><meta http-equiv="Expires" content="-1" /><meta http-equiv="Cache-Control" content="no-cache" /><meta http-equiv="Pragma" content="no-cache" /><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>保証書発行物件一覧</title>
    <link rel="stylesheet" href="css/jhsearth.css" type="text/css" />
</head>
<script type="text/javascript" src="js/jhsearth.js">
</script>

<script language="javascript" for="document" event="onkeydown"> 

  if(event.keyCode==13 && event.srcElement.type!="button" && event.srcElement.type!="submit" && event.srcElement.type!="reset" && event.srcElement.type!="textarea" && event.srcElement.type!="")
     event.keyCode=9; 
</script> 

<body>
    <form id="form1" runat="server">
  <div id="buySelName" runat="server" class="modalDiv" style="position:absolute;left:300px; top:140px;z-index:2;display:none;">
        </div>
        <div id="disableDiv" runat="server" style="position:absolute; left:0px; top:0px; width:830px; height:480px; z-index:100;  FILTER:alpha(opacity=70);background-color:#000000; display:none;">
        <iframe src="about:blank" id="hiddenIframe" width="100%" height="100%"></iframe>
        </div>
<%--    <asp:ScriptManager ID="AjaxScriptManager1" runat="server">
</asp:ScriptManager>--%>
        <div>
            <table border="0" cellpadding="0" cellspacing="2" class="titleTable" style="width: 100%;
                text-align: left">
                <tbody>
                    <tr>
                        <th>
                            <asp:Label ID="lblTitle" runat="server" Font-Size="15px">保証書発行物件一覧</asp:Label>
                        </th>
                        <th style="text-align: right">
                        </th>
                    </tr>
                </tbody>
            </table>
                        <table border="0" cellpadding="0" cellspacing="2" class="titleTable" style="width: 786px;
                text-align: left">
                <tbody>
                    <tr>
                        <th style="width: 360px; height: 35px;" align="right" valign="bottom">
                            &nbsp;<asp:Button ID="btnTougetu" runat="server" Text="当月" Width="56px" />
                            <asp:Button ID="btnZengetu" runat="server" Text="前月" Width="56px" /></th>
                        <th style="height: 35px; text-align: right; width: 242px;" valign="bottom">
                            <asp:Label ID="Label2" runat="server" Font-Size="X-Large" ForeColor="Red" Text="前月" Font-Bold="True"></asp:Label><asp:Label
                                ID="Label1" runat="server" ForeColor="Red" Text="分表示中"></asp:Label></th>
                        <th style="text-align: right; height: 35px;" valign="bottom">
                            &nbsp;<asp:Button ID="btnAll" runat="server" Text="全て[有り]をセット" /></th>
                    </tr>
                    <tr>
                        <th align="right" style="width: 360px; height: 4px" valign="bottom">
                        </th>
                        <th style="height: 4px; text-align: right; width: 242px;" valign="bottom">
                        </th>
                        <th style="height: 4px; text-align: right" valign="bottom">
                        </th>
                    </tr>
                </tbody>
            </table>
        </div>
        <div id="divHead" runat="server">
        <asp:GridView ID="grdHead" runat="server" CellPadding="0" CellSpacing="0" CssClass="gridviewTableHeader"
            ShowHeader="False" Style="border-top: black 1px solid; border-left: black 1px solid">
        </asp:GridView>
         
        <div id="divMeisai" runat="server">
   <%--         <asp:UpdatePanel ID="UpdatePanelA" runat="server" RenderMode="Inline" UpdateMode="Conditional">
        <ContentTemplate>--%>
            <asp:GridView ID="grdBody" runat="server" BackColor="White" BorderColor="GrayText"
                BorderWidth="1px" CellPadding="0" CssClass="tableMeiSai" ShowHeader="False" Style="border-right: #999999 1px solid;
                border-top: black 1px solid">
                <SelectedRowStyle ForeColor="White" />
                <AlternatingRowStyle BackColor="LightCyan" />
                <RowStyle BorderColor="#999999" BorderWidth="1px" />
            </asp:GridView>
            <asp:HiddenField ID="hidTop" runat="server" />
 <%--       </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnAll" />
            <asp:AsyncPostBackTrigger ControlID="btnTougetu" />
            <asp:AsyncPostBackTrigger ControlID="btnZengetu" />
            <asp:AsyncPostBackTrigger ControlID="btnClose" />
            <asp:AsyncPostBackTrigger ControlID="btnKousin" />
        </Triggers>
    </asp:UpdatePanel>--%>
            </div>
            </div>
            <br />
            <table border="0" cellpadding="0" cellspacing="2" class="titleTable" style="width: 786px;
                text-align: left">
                <tbody>
                    <tr>
                        <th style="width: 740px; height: 24px;" align="right">
                            &nbsp;
                            <asp:Button ID="btnClose" runat="server" Text="変更せずに閉じる" /></th>
                        <th style="text-align: right; height: 24px; width: 86px;">
                            <asp:Button ID="btnKousin" runat="server" Text="一括更新" style="background-color: fuchsia" /></th>
                    </tr>
                </tbody>
            </table>
        <asp:HiddenField ID="hidSort" runat="server" />
        <asp:HiddenField ID="hidColor" runat="server" />  
    </form>
</body>
</html>

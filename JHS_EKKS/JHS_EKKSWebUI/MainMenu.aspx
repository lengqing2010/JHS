<%@ Page Language="VB" MasterPageFile="~/EKKSMaster.master" AutoEventWireup="false"
    CodeFile="MainMenu.aspx.vb" Inherits="MainMenu" %>

<%@ Register Src="CommonControl/CommonButton.ascx" TagName="CommonButton" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/EKKSMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        //onload
        window.onload = function(){
            window.name = "<%=CommonConstBC.MAIN %>"
            changeTableSize("osiraseDiv",30,60);    //お知らせ表示領域のスクロール調整
            setMenuBgColor();
        }
        
        //onresize
        window.onresize = function(){
            changeTableSize("osiraseDiv",30,60);    //お知らせ表示領域のスクロール調整
        }



    </script>

    <table style="height: 100%; width: 100%">
        <tr>
            <td style="height: 10px" colspan="4">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 20px">
                &nbsp;</td>
            <td style="vertical-align: top; width: 320px">
                <table cellpadding="8" cellspacing="0" class="menuBar" style="width: 280px">
                    <tr>
                        <td colspan="2" class="menuDaiKoumoku">
                            メニュー</td>
                    </tr>
                    <tr>
                        <td class="menuKoumkuIcon">
                            <img alt="" src="images/menu_arrow.gif" /></td>
                        <td>
                            <a id="LinkEigyouKeikaku" runat="server">営業計画管理</a>
                        </td>
                    </tr>
                    <tr>
                        <td class="menuKoumkuIcon">
                            <img alt="" src="images/menu_arrow.gif" /></td>
                        <td>
                            <a id="LinkUriYojitu" runat="server">売上予実管理</a></td>
                    </tr>
                    <tr>
                        <td class="menuKoumkuIcon">
                            <img alt="" src="images/menu_arrow.gif" /></td>
                        <td>
                            <a id="LinkKeikakuKanriKameitenKensakuSyoukai" runat="server">計画用_加盟店情報照会</a></td>
                    </tr>
                    <tr>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top;">
                <table cellpadding="3" cellspacing="0" class="osiraseTable">
                    <tr>
                        <td>
                        </td>
                        <td colspan="2" style="font-size: 17px; font-weight: bold">
                            ～お知らせ～</td>
                    </tr>
                    <tr>
                        <td colspan="3" style="height: 15px;">
                        </td>
                    </tr>
                </table>
                <div id="osiraseDiv" style="overflow: auto; height: 400px;">
                    <table cellpadding="3" cellspacing="0" class="osiraseTable">
                        <thead>
                        </thead>
                        <tbody id="osiraseTbody" class="osiraseTbody" runat="server">
                        </tbody>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>

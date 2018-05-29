<%@ Page Language="VB" MasterPageFile="~/EKKSMaster.master" AutoEventWireup="false"
    CodeFile="UriageYojituKanriMenu.aspx.vb" Inherits="UriageYojituKanriMenu" Title="売上予実管理" %>

<%@ Register Src="CommonControl/CommonButton.ascx" TagName="CommonButton" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/EKKSMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        //onload
        window.onload = function(){
            window.name = "<%=CommonConstBC.uriageYojituKanri %>"
            setMenuBgColor();
        }
    </script>

    <table style="margin-left: 35px; margin-top: 25px;" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <asp:Label ID="lblGamenMei" runat="server" Text="売上予実管理" Style="font-size: 17px;
                    font-weight: bold"></asp:Label>
            </td>
        </tr>
    </table>
    <table style="margin-left: 35px; margin-top: 10px; margin-left: 50px; margin-bottom: 10px;"
        cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <asp:Label ID="lblSentaku" runat="server" Text="集計選択" Style="font-size: 15px; font-weight: bold;"></asp:Label>
            </td>
        </tr>
    </table>
    <div id="divSitenMei" style="height: 300px; width: 800px; border-right: 1px dotted;
        border-top: 1px dotted; border-left: 1px dotted; border-bottom: 1px dotted; margin-left: 60px;
        left: 16px; position: relative; top: 20px; visibility: visible; background-color: white;">
        <table cellpadding="0" cellspacing="0" border="0" style="margin-left: 30px; margin-top: 30px;">
            <tr>
                <td style="width: 300px;">
                    <uc1:CommonButton ID="btnZensya" runat="server" Text="全社 集計" Cssclass="button menuButton" />
                </td>
                <td style="width: 300px;">
                    <uc1:CommonButton ID="btnKakusyu" runat="server" Text="各種 集計" Cssclass="button menuButton"
                        Visible="false" />
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

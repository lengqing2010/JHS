<%@ Page Language="VB" MasterPageFile="~/EKKSMaster.master" AutoEventWireup="false"
    CodeFile="EigyouKeikakuKanriMenu.aspx.vb" Inherits="EigyouKeikakuKanriMenu" Title="営業計画管理"%>

<%@ Register Src="CommonControl/CommonButton.ascx" TagName="CommonButton" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/EKKSMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript">
        //onload
        window.onload = function(){
            window.name = "<%=CommonConstBC.eigyouKeikakuKanri%>"
            setMenuBgColor();
        }
</script>
    <table style="margin-left: 35px; margin-top: 25px;" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <asp:Label ID="lblGamenMei" runat="server" Text="営業計画管理 選択" style="font-size: 17px; font-weight: bold"></asp:Label>
            </td>
        </tr>
    </table>
    <div id="divSitenMei" style="height: 8px; width: 150px; border-right: 1px dotted;
        border-top: 1px dotted; border-left: 1px dotted; border-bottom: 1px dotted; margin-left: 40px;
        left: 16px; position: relative; top: 40px; visibility: visible; background-color: white;">
        <table cellpadding="0" cellspacing="0" border="0" style="margin-left: 40px;">
            <tr>
                <td>
                    <asp:Label ID="lblSitenbetu" runat="server" Text="会社・支店別" style="font-size: 15px; font-weight: bold"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div id="divMei" style="height: 80px; width: 700px; border-right: 1px dotted; border-top: 1px dotted;
        border-left: 1px dotted; border-bottom: 1px dotted; margin-left: 40px; margin-top: 30px;">
        <table cellpadding="0" cellspacing="0" border="0" style="margin-left: 30px; margin-top: 30px;">
            <tr>
                <td style="width: 300px; height: 24px;">
                <uc1:CommonButton ID="btnNendoSeltuTei" runat="server"  CssClass="button menuButton" Text ="年度計画値設定" />
                  
                </td>
                <td style="width: 300px; height: 24px;">
            <uc1:CommonButton ID="btnSiten" runat="server"  CssClass="button menuButton" Text ="支店 月別計画値設定"/>
                    
                </td>
            </tr>
        </table>
    </div>
    <div id="divKeikaku" style="height: 8px; width: 150px; border-right: 1px dotted;
        border-top: 1px dotted; border-left: 1px dotted; border-bottom: 1px dotted; margin-left: 40px;
        left: 16px; position: relative; top: 40px; visibility: visible; background-color: white;">
        <table cellpadding="0" cellspacing="0" border="0" style="margin-left: 40px;">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="計画管理" style="font-size: 15px; font-weight: bold"></asp:Label>
                    
                </td>
            </tr>
        </table>
    </div>
    
    <div id="divKeikakuBig" style="height: 80px; width: 700px; border-right: 1px dotted;
        border-top: 1px dotted; border-left: 1px dotted; border-bottom: 1px dotted; margin-left: 40px;
        margin-top: 30px;">
        <table cellpadding="0" cellspacing="0" border="0" style="margin-left: 30px; margin-top: 30px;">
            <tr>
                <td style="width: 300px; height: 24px;" >
               <uc1:CommonButton ID="btnTorokuKeikaku" runat="server"  CssClass="button menuButton" Text ="登録事業者別計画管理" />
                    
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

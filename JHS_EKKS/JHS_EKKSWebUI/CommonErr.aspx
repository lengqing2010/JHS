<%@ Page Language="VB" MasterPageFile="~/EKKSMaster.master" AutoEventWireup="false" CodeFile="CommonErr.aspx.vb" Inherits="CommonErr"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

      <div style ="text-align :center">

        <table id="table2" cellspacing="0" cellpadding="0" border="0" width="960px">
	        <tr>
		        <td style="width:90%;height:100px;">
                 <div style ="text-align :center">                         
                 <asp:label id="message" runat="server" ForeColor="red" Height="20px" Font-Size="12px" Width ="100%"></asp:label>
                 </div>
                 </td>
	        </tr>
        </table>
                <asp:Button ID="btnClose" runat="server" Text="閉じる" OnClientClick ="window.close();return false;" />
                <asp:Button ID="btnModoru" runat="server" Text="戻る" OnClick="btnModoru_Click"  />
</div> 
</asp:Content>


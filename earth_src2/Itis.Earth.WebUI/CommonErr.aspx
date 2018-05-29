<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master" CodeBehind="CommonErr.aspx.vb" Inherits="Itis.Earth.WebUI.CommonErr" 
    title="EARTH" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript" src="js/jhsearth.js"></script>
    <script type="text/javascript">
        //window名付与
        var objWin = window;
        objWin.name = "earthMainWindow"
        initPage(); //画面初期設定
    </script>
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
                <asp:Button ID="btnModoru" runat="server" Text="戻る"  />
</div> 
    
    
    
</asp:Content>

<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master" CodeBehind="SeikyusyoExcelOutputTestPage.aspx.vb" Inherits="Itis.Earth.WebUI.SeikyusyoExcelOutputTestPage" 
    title="無題のページ" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="buySelName" runat="server" class="modalDiv" style="position:absolute;left:300px; top:140px;z-index:2;display:none;">
    </div>
    <div id="disableDiv" runat="server" style="position:absolute; left:0px; top:0px; width:1002px; height:590px; z-index:100;  FILTER:alpha(opacity=70);background-color:#000000; display:none;">
        <iframe src="about:blank" id="hiddenIframe" width="100%" height="100%"></iframe>
    </div>

   <br/><br/> 請求書NO<br/>
    <asp:TextBox ID="tbx_seikyusyo_no" runat="server"></asp:TextBox><br/><br/>
    <asp:Button ID="btnExcel" runat="server" Text="Excel出力" />
    <asp:HiddenField ID="hidSeni" runat="server" />
</asp:Content>

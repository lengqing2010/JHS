<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master" CodeBehind="KeiretuMaster.aspx.vb" Inherits="Itis.Earth.WebUI.KeiretuMaster" 
    title="系列マスタメンテナンス" %>

<%@ Register Src="control/common_drop.ascx" TagName="common_drop" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" cellpadding="0" cellspacing="2" class="titleTable" style="width: 960px;
        text-align: left">
        <tbody>
            <tr>
                <th>
                    系列マスタメンテナンス 
                    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp;<asp:Button ID="btnBack" runat="server" CssClass="kyoutuubutton" Text="戻る" />
                </th>
                <th style="text-align: right">
                </th>
            </tr>
            <tr>
                <td colspan="2" rowspan="1" style="height: 13px">
                </td>
            </tr>
        </tbody>
    </table>
<script type="text/javascript" src="js/jhsearth.js"></script>
<script type="text/javascript">
    //window名付与
    var objWin = window;
    objWin.name = "earthMainWindow"
    initPage(); //画面初期設定  
</script>
<script language="javascript" for="document" event="onkeydown"> 
  if(event.keyCode==13 && event.srcElement.type!="button" && event.srcElement.type!="submit" && event.srcElement.type!="reset" && event.srcElement.type!="textarea" && event.srcElement.type!="")
     event.keyCode=9; 
</script> 

  <asp:UpdatePanel ID="UpdatePanelA" runat="server" UpdateMode="Conditional"  RenderMode="Inline"> 
    <Triggers>
                            <asp:AsyncPostBackTrigger ControlID ="btnSearch" />
                            <asp:AsyncPostBackTrigger ControlID ="btnClear" />
                            </Triggers>            
   <ContentTemplate>
    <table cellpadding="2" runat="server" id='TbHead' class="mainTable" style="text-align: left; width: 400px;">
        <tbody>
            <tr>
                <td class="koumokuMei" style="width: 90px" >
                  区分
                </td>
                <td>
                    <uc1:common_drop ID="Common_drop1" runat="server" CssClass="hissu" style="visibility:visible;" GetStyle='kubun'  />
                </td>
            </tr>
            <tr>
                <td class="koumokuMei" style="width: 90px" >
                    取消</td>
                <td>
                    <asp:DropDownList ID="ddlTorikesi" runat="server">
                    </asp:DropDownList></td>
            </tr>
            <tr align="left">
                <td colspan="2" rowspan="1">
                    <asp:Button ID="btnSearch" runat="server" Text="絞込 & 編集" Width="112px" />
                    <asp:Button ID="btnClear" runat="server" Text="クリア" /></td>
            </tr>
        </tbody>
    </table>

    <br />

         

    <asp:GridView ID="grdHead" runat="server" CellPadding="0" CellSpacing="0" CssClass="masterHeader"
        ShowHeader="False" Style="border-top: black 1px solid; border-left: black 1px solid;">
    </asp:GridView>
    <div id="divMeisai" runat ="server"  >
    <asp:GridView ID="grdBody" runat="server" BackColor="#E6E6E6" BorderColor="GrayText"
        BorderWidth="1px" CellPadding="0" ShowHeader="False" Style="border-right: #999999 1px solid;
        border-top: black 1px solid">
        <RowStyle BorderColor="#999999" BorderWidth="1px" />
        <SelectedRowStyle ForeColor="White" />
        <AlternatingRowStyle BackColor="#C0FFC0" />
    </asp:GridView>
    </div>
       <asp:HiddenField ID="hidUniqueID" runat="server" />
    <asp:Button ID="btn" runat="server"  Style="display: none;" Text="Button"/>

    <asp:HiddenField ID="hidBtn" runat="server" />
    <asp:HiddenField ID="hidKBN" runat="server" />
   
    <asp:HiddenField ID="hidKeiretuCd" runat="server" />
    <asp:HiddenField ID="hidKeiretuMei" runat="server" />
    <asp:HiddenField ID="hidTorikesi" runat="server" />
    <asp:HiddenField ID="hidUPDTime" runat="server" />
    <asp:HiddenField ID="hidRowIndex" runat="server" />
    
    <asp:HiddenField ID="hidBool" runat="server" />
    <asp:HiddenField ID="hidTop" runat="server" />
 </ContentTemplate> 
     <Triggers>
       <asp:AsyncPostBackTrigger ControlID ="btn" />
      </Triggers>   
                            
    </asp:UpdatePanel> 
 
</asp:Content>

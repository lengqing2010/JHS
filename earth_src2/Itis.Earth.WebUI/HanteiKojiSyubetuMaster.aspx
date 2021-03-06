﻿<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master" CodeBehind="HanteiKojiSyubetuMaster.aspx.vb" Inherits="Itis.Earth.WebUI.HanteiKojiSyubetuMaster" 
    title="判定工事種別設定マスタメンテナンス" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
 <div id="buySelName" runat="server" class="modalDiv" style="position:absolute;left:300px; top:140px;z-index:2;display:none;">
        </div>
        <div id="disableDiv" runat="server" style="position:absolute; left:0px; top:0px; width:1002px; height:610px; z-index:100;  FILTER:alpha(opacity=70);background-color:#000000; display:none;">
        <iframe src="about:blank" id="hiddenIframe" width="100%" height="100%"></iframe>
        </div>  
    <table border="0" cellpadding="0" cellspacing="2" class="titleTable" style="width: 960px;
        text-align: left">
        <tbody>
            <tr>
                <th>
                    判定工事種別設定マスタメンテナンス
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
    <asp:UpdatePanel ID="UpdatePanelA" runat="server" RenderMode="Inline" UpdateMode="Conditional">
        <ContentTemplate>
            <table id="TbHead" runat="server" cellpadding="2" class="mainTable" style="width: 472px;
                text-align: left">
                <tbody>
                    <tr>
                        <td class="koumokuMei" style="width: 113px">
                            基礎仕様NO
                        </td>
                        <td  > <asp:TextBox ID="tbxSiyouNo" runat="server"  CssClass="hissu"  MaxLength="7" Style="ime-mode: disabled"
                            Width="64px"></asp:TextBox>&nbsp;<asp:Button ID="btnSearchSiyou" runat="server"
                                Text="検索"  />
                        <asp:TextBox ID="tbxSiyou" runat="server" CssClass="readOnlyStyle" MaxLength="40"  
                            Width="208px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="koumokuMei" style="width: 113px">
                            改良工事種別NO</td>
                        <td >
                            <asp:DropDownList ID="ddlKojSyubetu" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    
                    <tr align="left">
                        <td colspan="2" rowspan="1">
                            <asp:Button ID="btnSearch" runat="server" Text="絞込 & 編集" Width="112px" OnClick="btnSearch_Click" />
                            <asp:Button ID="btnClear" runat="server" Text="クリア" /></td>
                    </tr>
                </tbody>
            </table>
        <br />

            <asp:GridView ID="grdHead" runat="server" CellPadding="0" CellSpacing="0" CssClass="masterHeader"
                ShowHeader="False" Style="border-top: black 1px solid; border-left: gray 1px solid">
            </asp:GridView>
            <div id="divMeisai" runat="server">
                <asp:GridView ID="grdBody" runat="server" BackColor="#E6E6E6" BorderColor="GrayText"
                    BorderWidth="1px" CellPadding="0" ShowHeader="False" Style="border-right: #999999 1px solid;
                    border-top: black 1px solid">
                    <RowStyle BorderColor="#999999" BorderWidth="1px" />
                    <SelectedRowStyle ForeColor="White" />
                    <AlternatingRowStyle BackColor="#C0FFC0" />
                </asp:GridView>
            </div>
            <asp:Button style="DISPLAY: none" id="btn" runat="server" Text="Button" OnClick="btn_Click"></asp:Button>
            <asp:HiddenField ID="hidTop" runat="server" />
            <asp:HiddenField ID="hidBtn" runat="server" />
            
            <asp:HiddenField ID="hidBool" runat="server" />
            <asp:Button ID="btnOpen" runat="server"  Style="display: none;" Text="Button"/>
            
            <asp:HiddenField ID="hidSiyouNo" runat="server" />
            <asp:HiddenField ID="hidSyubetuNo" runat="server" />

             <asp:HiddenField id="hidUPDTime" runat="server"/>
             <asp:HiddenField ID="hidRowIndex" runat="server" />
         </ContentTemplate>
        <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click"  />
            <asp:AsyncPostBackTrigger ControlID="btnClear" />
            <asp:AsyncPostBackTrigger ControlID="btn" />
            <asp:AsyncPostBackTrigger ControlID="btnOpen" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

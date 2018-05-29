<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master" CodeBehind="BukkenJyouhouList.aspx.vb" Inherits="Itis.Earth.WebUI.BukkenJyouhouList" 
    title="物件情報検索結果"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript" src="js/jhsearth.js"></script>
<%  If IsPostBack Then%>
    <script type="text/javascript">
        //window名付与
        var objWin = window;
        objWin.name = "earthMainWindow"
        initPage(); //画面初期設定
    </script>
    <%End If%>
    <!--明細-->
     <table runat ="server" visible ="false" id="tablhead2"  class="titleTable" border="0"  style="width: 940px; vertical-align: top;" cellpadding ="0" cellspacing ="0">
        <tr style="height: 20px;">
            <th rowspan="1" 
                style="width: 628px; 
                       text-align:left; vertical-align: top; ">物件情報検索結果 &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            </th>
            <td style="width: 64px; ">
            </td>
            <td style="width: 200px; ">
            </td>
        </tr>
         
    </table>
  
    
<table runat ="server" id="tablhead1" visible ="false"  class="gridviewTableHeader"  width = "960px" cellpadding="0" cellspacing="0" >
    <tr>
        <td  style="width: 799px; border-top: none;background-color: transparent; text-align: left;">
            検索結果：<asp:Label ID="lblSearch" runat="server" Text=""></asp:Label>件
        </td>
        <td style="width: 70px;border-left: 1px;">
            調査</td>
        <td>
            工事</td>
    </tr>
    </table> 
    <table runat ="server" id="tablhead" visible ="false"  class="gridviewTableHeader" width = "960px" cellpadding="0" cellspacing="0" >
    <tr>
    <td colspan="2" style=" border-left: 1px solid #999999;">
        物件NO<asp:LinkButton ID="Ahosyousyo_no" runat="server" Font-Underline="False" ForeColor="SkyBlue">▲</asp:LinkButton><asp:LinkButton ID="Dhosyousyo_no" runat="server" Font-Underline="False" ForeColor="SkyBlue">▼</asp:LinkButton></td>
        <td style="width: 86px">
            破棄<asp:LinkButton ID="Ahaki_syubetu" runat="server" Font-Underline="False" ForeColor="SkyBlue">▲</asp:LinkButton><asp:LinkButton
                ID="Dhaki_syubetu" runat="server" Font-Underline="False" ForeColor="SkyBlue">▼</asp:LinkButton></td>
        <td style="width: 100px">
            依頼日<asp:LinkButton ID="Airai_date" runat="server" Font-Underline="False" ForeColor="SkyBlue">▲</asp:LinkButton><asp:LinkButton
                ID="Dirai_date" runat="server" Font-Underline="False" ForeColor="SkyBlue">▼</asp:LinkButton></td>
        <td style="width: 127px">
            調査希望日<asp:LinkButton ID="Atys_kibou_date" runat="server" Font-Underline="False" ForeColor="SkyBlue">▲</asp:LinkButton><asp:LinkButton
                ID="Dtys_kibou_date" runat="server" Font-Underline="False" ForeColor="SkyBlue">▼</asp:LinkButton></td>
        <td style="width: 86px">
            方法<asp:LinkButton ID="Atys_houhou_mei_ryaku" runat="server" Font-Underline="False" ForeColor="SkyBlue">▲</asp:LinkButton><asp:LinkButton
                ID="Dtys_houhou_mei_ryaku" runat="server" Font-Underline="False" ForeColor="SkyBlue">▼</asp:LinkButton></td>
        <td colspan="3" rowspan="2">
            調査結果<asp:LinkButton ID="Aks_siyou" runat="server" Font-Underline="False" ForeColor="SkyBlue">▲</asp:LinkButton><asp:LinkButton
                ID="Dks_siyou" runat="server" Font-Underline="False" ForeColor="SkyBlue">▼</asp:LinkButton></td>
        <td style="width: 82px">
            予定日<asp:LinkButton ID="Asyoudakusyo_tys_date" runat="server" Font-Underline="False" ForeColor="SkyBlue">▲</asp:LinkButton><asp:LinkButton
                ID="Dsyoudakusyo_tys_date" runat="server" Font-Underline="False" ForeColor="SkyBlue">▼</asp:LinkButton></td>
        <td>
            予定日<asp:LinkButton ID="Akairy_koj_kanry_yotei_date" runat="server" Font-Underline="False" ForeColor="SkyBlue">▲</asp:LinkButton><asp:LinkButton
                ID="Dkairy_koj_kanry_yotei_date" runat="server" Font-Underline="False" ForeColor="SkyBlue">▼</asp:LinkButton></td>
    </tr>
        <tr>
            <td style="width: 24px; background-color: transparent; border-bottom-style: none;border-left-style: none;">
            　
            </td>
            <td colspan="5">
                施主名<asp:LinkButton ID="Asesyu_mei" runat="server" Font-Underline="False" ForeColor="SkyBlue">▲</asp:LinkButton><asp:LinkButton
                    ID="Dsesyu_mei" runat="server" Font-Underline="False" ForeColor="SkyBlue">▼</asp:LinkButton></td>
            <td style="width: 82px"  >
                実施日<asp:LinkButton ID="Atys_jissi_date" runat="server" Font-Underline="False" ForeColor="SkyBlue">▲</asp:LinkButton><asp:LinkButton
                    ID="Dtys_jissi_date" runat="server" Font-Underline="False" ForeColor="SkyBlue">▼</asp:LinkButton></td>
            <td>
                実施日<asp:LinkButton ID="Akairy_koj_date" runat="server" Font-Underline="False" ForeColor="SkyBlue">▲</asp:LinkButton><asp:LinkButton
                    ID="Dkairy_koj_date" runat="server" Font-Underline="False" ForeColor="SkyBlue">▼</asp:LinkButton></td>
        </tr>
        <tr>
            <td style="width: 24px;BACKGROUND-COLOR: transparent;border-bottom-style: none;border-left-style: none;border-top-style: none; ">
            　
            </td>
            <td style="border-bottom: 1px solid; width: 77px;">
                コード<asp:LinkButton ID="Akameiten_cd" runat="server" Font-Underline="False" ForeColor="SkyBlue">▲</asp:LinkButton><asp:LinkButton
                    ID="Dkameiten_cd" runat="server" Font-Underline="False" ForeColor="SkyBlue">▼</asp:LinkButton></td>
            <td colspan="3" style="border-bottom: 1px solid">
                加盟店名<asp:LinkButton ID="Akameiten_mei1" runat="server" Font-Underline="False" ForeColor="SkyBlue">▲</asp:LinkButton><asp:LinkButton
                    ID="Dkameiten_mei1" runat="server" Font-Underline="False" ForeColor="SkyBlue">▼</asp:LinkButton></td>
            <td colspan="1" style="border-bottom: 1px solid">
                取消</td>
            <td style="border-bottom: 1px solid; width: 95px;">
                担当者<asp:LinkButton ID="Airai_tantousya_mei" runat="server" Font-Underline="False" ForeColor="SkyBlue">▲</asp:LinkButton><asp:LinkButton
                    ID="Dirai_tantousya_mei" runat="server" Font-Underline="False" ForeColor="SkyBlue">▼</asp:LinkButton></td>
            <td style="border-bottom: 1px solid; width: 150px;">
                改良工事種別<asp:LinkButton ID="Akairy_koj_syubetu" runat="server" Font-Underline="False" ForeColor="SkyBlue">▲</asp:LinkButton><asp:LinkButton
                    ID="Dkairy_koj_syubetu" runat="server" Font-Underline="False" ForeColor="SkyBlue">▼</asp:LinkButton></td>
            <td style="border-bottom: 1px solid; width: 140px;">
                保証書発行日<asp:LinkButton ID="Ahosyousyo_hak_date" runat="server" Font-Underline="False" ForeColor="SkyBlue">▲</asp:LinkButton><asp:LinkButton
                    ID="Dhosyousyo_hak_date" runat="server" Font-Underline="False" ForeColor="SkyBlue">▼</asp:LinkButton></td>
            <td style="border-bottom: 1px solid; width: 82px;">
                売上日<asp:LinkButton ID="Auri_date" runat="server" Font-Underline="False" ForeColor="SkyBlue">▲</asp:LinkButton><asp:LinkButton
                    ID="Duri_date" runat="server" Font-Underline="False" ForeColor="SkyBlue">▼</asp:LinkButton></td>
            <td style="border-bottom: 1px solid">
                売上日<asp:LinkButton ID="Auri_date2" runat="server" Font-Underline="False" ForeColor="SkyBlue">▲</asp:LinkButton><asp:LinkButton
                    ID="Duri_date2" runat="server" Font-Underline="False" ForeColor="SkyBlue">▼</asp:LinkButton></td>
        </tr>
    </table> 
    <div id="divHiddenMeisaiH" runat = "server" style="overflow:auto;height:378px;width:977px;margin-top:0px;">
    <asp:GridView  ID="grdNaiyou" width = "960px" runat="server"  CellPadding="0" ShowHeader="False" Style="border-left-style:none;border-top:#999999 1px solid;border-bottom-style:none;border-right:#999999 1px solid; ">
    </asp:GridView>
    </div> 
    <table  width = "955px" cellpadding="0" cellspacing="0" >
        <tr>
            <td style="width: 118px; height: 14px">
            </td>
            <td style="width: 109px; height: 14px">
            </td>
            <td style="height: 14px">
            </td>
        </tr>
    <tr>
        <td style="width: 118px">
        </td>
        <td style="width: 109px">
            <asp:Button ID="btnClose" Visible ="false"  runat="server" Text="閉じる"  OnClientClick="window.close();" Width="78px" /></td>
    <td>
        <asp:Button ID="btnCsv" Visible ="false" runat="server" Text="CSV出力" Width="78px" /></td>
    </tr>
</table> 
    <asp:HiddenField ID="hidSort" runat="server" />
    <asp:HiddenField ID="hidkennsuu" runat="server" />
</asp:Content>

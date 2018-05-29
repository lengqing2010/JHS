<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master" CodeBehind="MasterMainteMenu.aspx.vb" Inherits="Itis.Earth.WebUI.MasterMainteMenu" 
    title="マスタメンテナンス" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript" src="js/jhsearth.js"></script>
<script type="text/javascript">
    //window名付与
    var objWin = window;
    objWin.name = "earthMainWindow"
    initPage(); //画面初期設定  
    
    //ポップアップ表示
    function ShowPopup(strPath)
    {
        window.open(strPath,"popup","menubar=yes,toolbar=yes,location=yes,status=yes,scrollbars=yes,resizable=yes");
    }
    
</script>
<table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
    class="titleTable">
    <tbody>
        <tr>
            <th>
                マスタメンテナンス
            </th>
            <th style="text-align: right;">
            </th>
        </tr>
        <tr>
            <td colspan="2" rowspan="1">
            </td>
        </tr>
    </tbody>
</table>
    <table style="width: 960px">
        <tr>
            <td align="center" style="height: 337px">
            <br />
            <table style="width:860px; height: 336px;" >
                <tr>
                    <td  align="center" ><asp:Button ID="btnKeiretuMaster" runat="server" Text="系列マスタ" Height="28px" Width="264px" Font-Size="12pt" Font-Bold="true" /></td>
                    <td  align="center"><asp:Button ID="btnSyouhinMaster" runat="server" Text="商品マスタ" Height="28px" Width="264px" Font-Size="12pt" Font-Bold="true" /></td>
                    <td  align="center"><asp:Button ID="btnKairyKojSyubetuMaster" runat="server" Text="改良工事種別マスタ" Height="28px" Width="264px" Font-Size="12pt" Font-Bold="true" /></td>
                </tr>
                <tr>
                    <td  align="center">
                        <asp:Button ID="btnEigyousyoMaster" runat="server" Text="営業所マスタ" Height="28px" Width="264px" Font-Size="12pt" Font-Bold="true" /></td>
                    <td  align="center"><asp:Button ID="btnKakakuMaster" runat="server" Text="販売価格マスタ" Height="28px" Width="264px" Font-Size="12pt" Font-Bold="true" /></td>
                    <td  align="center"><asp:Button ID="btnHanteiKojiSyubetuMaster" runat="server" Text="判定工事種別設定マスタ" Height="28px" Width="264px" Font-Size="12pt" Font-Bold="true" /></td>
                </tr>
                <tr>
                    <td align="center" >
                        <asp:Button ID="btnTyousaKaisyaMaster" runat="server" Text="調査会社マスタ" Height="28px" Width="264px" Font-Size="12pt" Font-Bold="true" /></td>
                    <td align="center" >
                        <asp:Button ID="btnSyouhinKakakusetteiMaster" runat="server" Text="商品価格設定マスタ" Height="28px" Width="264px" Font-Size="12pt" Font-Bold="true" /></td>
                    <td align="center" ><asp:Button ID="btnKoujiKakakuMaster" runat="server" Text="工事価格マスタ" Height="28px" Width="264px" Font-Size="12pt" Font-Bold="true" /></td>
                </tr>
                <tr>
                    <td align="center" >
                        <asp:Button ID="btnSiireMaster" runat="server" Text="原価マスタ" Height="28px" Width="264px" Font-Size="12pt" Font-Bold="true" /></td>
                    <td align="center" ><asp:Button ID="btnWaribikiMaster" runat="server" Text="多棟割引マスタ" Height="28px" Width="264px" Font-Size="12pt" Font-Bold="true" /></td>
                    <td align="center" ><asp:Button ID="btnTantousyaMaster" runat="server" Text="担当者マスタ" Height="28px" Width="264px" Font-Size="12pt" Font-Bold="true" /></td>
                </tr>
                <tr>
                    <td align="center" >
                        <asp:Button ID="btnSeikyuuSakiMaster" runat="server" Text="請求先マスタ" Height="28px" Width="264px" Font-Size="12pt" Font-Bold="true" /></td>
                    <td align="center" ><asp:Button ID="btnTokuteitenSyouhin2SetteiMaster" runat="server" Text="特定店商品２設定マスタ" Height="28px" Width="264px" Font-Size="12pt" Font-Bold="true" /></td>
                    <td align="center" >
                        </td>
                </tr>
                <tr>
                    <td align="center" >
                        <asp:Button ID="btnSeikyuuSakiHenkouMaster" runat="server" Text="請求先変更マスタ" Height="28px" Width="264px" Font-Size="12pt" Font-Bold="true" /></td>
                    <td align="center" >
                        <asp:Button ID="btnTokubetuTaiou" runat="server" Text="加盟店商品調査方法特別対応マスタ" Height="28px" Width="264px" Font-Size="11pt" Font-Bold="true" /></td>
                    <td align="center" >
                        <asp:Button ID="btnUserKanri" runat="server" Text="ユーザー管理" Height="28px" Width="264px" Font-Size="12pt" Font-Bold="true" /></td>
                </tr>
            </table>
            </td>
        </tr>
       
    </table>


<table class="buttonsTable" style="margin-top:100px;" >
	<tr>
		<td>
			<asp:Button ID="btnModoru" runat="server" CssClass="kyoutuubutton" Text="戻　る" />
            &nbsp;
	    </td>
		<td>
		</td>
	</tr>
</table>

</asp:Content>

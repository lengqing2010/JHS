<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    CodeBehind="KeiriMenu.aspx.vb" Inherits="Itis.Earth.WebUI.KeiriMenu" 
    title="経理メニュー" %>
    
<%@ Import Namespace="Itis.Earth.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

<script type="text/javascript" src="js/jhsearth.js">
</script>

<script type="text/javascript">

        var objBtnReturn =null;
        
        _d = document;
        
        function funcAfterOnload() {
            if(window.name != "<%=EarthConst.MAIN_WINDOW_NAME %>"){
                objBtnReturn = objEBI("<%= BtnModoru.clientID %>");
                objBtnReturn.style.display = "none";
            }
        }
        
        //ウィンドウサイズ変更
        try{
            if(window.name != "<%=EarthConst.MAIN_WINDOW_NAME %>"){
                window.resizeTo(1024,768);
            }
        }catch(e){
            //アクセスが拒否されましたのエラーが出たら何もしない。
            if(e.number == 2147024891) throw e;
        }
</script>

<table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
    class="titleTable">
    <tbody>
        <tr>
            <th>
                経理メニュー
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
        <td align="center" style="height: 371px">
             <table style="width:860px; height: 370px;" >
                <tr>
                    <td colspan="3" style="height:28px">
                    </td>
                </tr>
                <tr>
                    <td align="center" >
                        <asp:Button ID="BtnTeibetuSyuusei" runat="server" Text="邸別データ修正" Height="28px" Width="264px" Font-Size="12pt" Font-Bold="true" /></td>
                    <td align="center">
                        <asp:Button ID="BtnUriageSiireSakusei" runat="server" Text="売上計上／月額割引作成" Height="28px" Width="264px" Font-Size="12pt" Font-Bold="true" /></td>
                    <td align="center">
                        <asp:Button ID="BtnSearchUriageData" runat="server" Text="売上伝票照会／請求日変更" Height="28px" Width="264px" Font-Size="12pt" Font-Bold="true" /></td>
                </tr>
                <tr>
                    <td colspan="3" style="height:33px">
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="BtnTenbetuSyuusei" runat="server" Text="店別データ修正" Height="28px" Width="264px" Font-Size="12pt" Font-Bold="true" /></td>
                    <td align="center">
                        <asp:Button ID="BtnGetujiIkkatuSyuusei" runat="server" Text="月次データ一括修正" Height="28px" Width="264px" Font-Size="12pt" Font-Bold="true" /></td>
                    <td align="center">
                        <asp:Button ID="BtnSearchSiireData" runat="server" Text="仕入伝票照会" Height="28px" Width="264px" Font-Size="12pt" Font-Bold="true" /></td>
                </tr>
                <tr>
                    <td colspan="3" style="height:33px">
                    </td>
                </tr>
                <tr>
                    <td align="center" >
                        <asp:Button ID="BtnTeibetuNyuukinSyuusei" runat="server" Text="邸別入金修正" Height="28px" Width="264px" Font-Size="12pt" Font-Bold="true" /></td>
                    <td align="center" >
                    </td>
                    <td align="center" >
                        <asp:Button ID="BtnSearchNyuukinData" runat="server" Text="入金伝票照会" Height="28px" Width="264px" Font-Size="12pt" Font-Bold="true" /></td>
                </tr>
                <tr>
                    <td colspan="3" style="height:33px">
                    </td>                  
                </tr>
                <tr>
                    <td align="center" >
                        <asp:Button ID="BtnNyuukinSyori" runat="server" Text="入金取込処理" Height="28px" Width="264px" Font-Size="12pt" Font-Bold="true" /></td>
                    <td align="center" >
                        <asp:Button ID="BtnSeikyuusyoDataSakusei" runat="server" Text="請求書データ作成／出力" Height="28px" Width="264px" Font-Size="12pt" Font-Bold="true" /></td>
                    <td align="center" >
                        <asp:Button ID="BtnSearchSiharaiData" runat="server" Text="支払伝票照会" Height="28px" Width="264px" Font-Size="12pt" Font-Bold="true" /></td>
                </tr>
                <tr>
                    <td colspan="3" style="height:33px">
                    </td>
                </tr>
                <tr>
                    <td align="center" >
                        <asp:Button ID="BtnSearchNyuukinTorikomi" runat="server" Text="入金取込データ照会／登録／修正" Height="28px" Width="264px" Font-Size="12pt" Font-Bold="true" /></td>
                    <td align="center" >
                    </td>
                    <td align="center" >
                        <asp:Button ID="BtnSeikyuuSakiMototyou" runat="server" Text="請求先元帳" Height="28px" Width="264px" Font-Size="12pt" Font-Bold="true" /></td>
                </tr>
                <tr>
                    <td colspan="3" style="height:33px">
                    </td>
                </tr>
                <tr>
                    <td align="center" >
                        <asp:Button ID="BtnSearchHannyouUriage" runat="server" Text="汎用売上データ照会／登録／修正" Height="28px" Width="264px" Font-Size="12pt" Font-Bold="true" /></td>
                    <td align="center" >
                        <asp:Button ID="BtnSeikyuuDateIkkatuHenkou" runat="server" Text="請求年月日一括変更" Height="28px" Width="264px" Font-Size="12pt" Font-Bold="true" /></td>
                    <td align="center" >
                        <asp:Button ID="BtnSiharaiSakiMototyou" runat="server" Text="支払先元帳" Height="28px" Width="264px" Font-Size="12pt" Font-Bold="true" /></td>
                </tr>
                <tr>
                    <td colspan="3" style="height:33px">
                    </td>
                </tr>
                <tr>
                    <td align="center" >
                        <asp:Button ID="BtnSearchHannyouSiire" runat="server" Text="汎用仕入データ照会／登録／修正" Height="28px" Width="264px" Font-Size="12pt" Font-Bold="true" /></td>
                    <td align="center" >
                        <asp:Button ID="BtnTeibetuSiireIkkatuHenkou" runat="server" Text="邸別請求先・仕入先一括変更" Height="28px" Width="264px" Font-Size="12pt" Font-Bold="true" /></td>
                    <td align="center" >
                        <asp:Button ID="BtnKakusyuDataSyuturyoku" runat="server" Text="各種マスタ／データ出力" Height="28px" Width="264px" Font-Size="12pt" Font-Bold="true" /></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<table class="buttonsTable" style="margin-top:10px;" >
	<tr>
		<td>
			<asp:Button ID="BtnModoru" runat="server" Height="22px" Width="100px" Text="戻　る" />
	    </td>
		<td>
		</td>
	</tr>
</table>

</asp:Content>

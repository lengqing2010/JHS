<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master"
    Codebehind="KihonJyouhouInput.aspx.vb" Inherits="Itis.Earth.WebUI.KihonJyouhouInput"
    Title="加盟店基本情報新規登録" %>

<%@ Register Src="~/control/common_drop.ascx" TagName="common_drop" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js"></script>
    <script type="text/javascript" src="js/jquery-1.4.1.min.js"></script>

    <script type="text/javascript">
    //window名付与
    var objWin = window;
    objWin.name = "earthMainWindow"
    initPage(); //画面初期設定
      //画面遷移処理
    function funcMove(url,tenmd,isfc,kameicd){
        //<!-- 画面引渡し情報-->
        var arrArg = window.dialogArguments;
        var objSendForm = objEBI("openPageForm2");
        var objSendVal_tenmd = objEBI("tenmd");
        var objSendVal_isfc = objEBI("isfc");
        var objSendVal_kameicd = objEBI("kameicd");

        objSendForm.action =url
        objSendVal_tenmd.value = tenmd;
        objSendVal_isfc.value = isfc;
        objSendVal_kameicd.value = kameicd;  
        objSendForm.submit();
        return false;
    }
    function chkHankakuSuuji(strInputString){
	    if(strInputString!=""){
	        if(strInputString.match(/[^0-9]/)!=null){
	    	    return false; 
	        }
        }
	return true;
}
    </script>
    
    <script type="text/javascript">
        $(document).ready(function () {
        
        
        
        
        });
    </script>

    <table class="titleTable" border="0" style="width: 960px; vertical-align: top;" cellpadding="0"
        cellspacing="0">
        <tr style="height: 20px;">
            <!-- TITLEの字 -->
            <th rowspan="1" style="width: 718px; text-align: left; vertical-align: top;">
                加盟店基本情報新規登録 &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnClose" runat="server" Text="閉じる" OnClientClick="window.close();return false;" />
                <asp:Button ID="btnHansokuSina" runat="server" Text="販促品登録" ForeColor="Red" />
            </th>
            <th rowspan="1" style="vertical-align: top; width: 109px; text-align: left;">
            </th>
            <td style="width: 115px; height: 0px">
            </td>
            <td style="width: 149px; height: 0px">
            </td>
        </tr>
        <tr>
            <th rowspan="1" style="vertical-align: top; width: 718px; text-align: left">
            </th>
            <th rowspan="1" style="vertical-align: top; width: 109px; text-align: left">
            </th>
            <td style="width: 115px; height: 0px">
            </td>
            <td style="width: 149px; height: 0px">
            </td>
        </tr>
    </table>
    <br />
    <br />
    <table runat="server" id="tablekensaku" style="margin-bottom: 10px; text-align: left;
        width: 970px;" class="mainTable paddinNarrow" cellpadding="0">
        <tr style="background-color: mistyrose;">
            <td class="hissu" style="font-weight: bold; width: 162px;">
                空き番号検索</td>
            <td class="hissu" style="font-weight: bold; width: 55px;">
                区分</td>
            <td class="hissu" style="font-weight: bold; width: 205px">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnTyokusetuNyuuryoku" />
                        <asp:AsyncPostBackTrigger ControlID="btnTyokusetuNyuuryokuTyuusi" />
                        <asp:AsyncPostBackTrigger ControlID="btnKameitenSearch" />
                    </Triggers>
                    <ContentTemplate>
                        <uc1:common_drop ID="common_drop1" runat="server" GetStyle="kubun" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="hissu" style="font-weight: bold; width: 105px;">
                加盟店コード
            </td>
            <td class="hissu" style="font-weight: bold; width: 409px">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnTyokusetuNyuuryoku" />
                        <asp:AsyncPostBackTrigger ControlID="btnTyokusetuNyuuryokuTyuusi" />
                        <asp:AsyncPostBackTrigger ControlID="btnKameitenSearch" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:TextBox runat="server" ID="tbxKameitenCd" MaxLength="5" Style="width: 65px;"
                            CssClass="codeNumber"></asp:TextBox>
                        <asp:Button runat="server" ID="btnKameitenSearch" Text="検索" />
                        <asp:Button runat="server" ID="btnTyokusetuNyuuryoku" Text="直接入力" />
                        <asp:Button runat="server" ID="btnTyokusetuNyuuryokuTyuusi" Text="直接入力中止" Visible="false" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="text-align: center; height: 21px;" colspan="5">
                ※加盟店コードの最大空き番号を区分、加盟店コードにセットアップします。
            </td>
        </tr>
    </table>
    <table style="text-align: left; width: 970px" class="mainTable paddinNarrow" cellpadding="0">
        <thead>
            <tr>
                <th class="tableTitle" colspan="9" rowspan="1" style="height: 24px">
                    共通情報
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnTyokusetuNyuuryoku" />
                            <asp:AsyncPostBackTrigger ControlID="btnTyokusetuNyuuryokuTyuusi" />
                            <asp:AsyncPostBackTrigger ControlID="btnKameitenSearch" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Button ID="btnTouroku" runat="server" Text="登録" Enabled="false" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </th>
            </tr>
        </thead>
        <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional" RenderMode="Inline">
            <ContentTemplate>
                <tbody id="kyoutuTBody" runat="server" class="tableMeiSai" style="margin-top: 0px;
                    width: 950px;">
                    <tr style="height: 20px; background-color: mistyrose;">
                        <td class="hissu" style="font-weight: bold; width: 90px; height: 20px;">
                            区分
                        </td>
                        <td class="hissu" colspan="3" style="height: 20px">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnTyokusetuNyuuryoku" />
                                    <asp:AsyncPostBackTrigger ControlID="btnTyokusetuNyuuryokuTyuusi" />
                                    <asp:AsyncPostBackTrigger ControlID="btnKameitenSearch" />
                                </Triggers>
                                <ContentTemplate>
                                    <uc1:common_drop ID="comdrp" runat="server" GetStyle="kubun" Visible="false" />
                                    <asp:TextBox runat="server" ID="tbxKyoutuKubun" ReadOnly="true" TabIndex="-1" CssClass="readOnly"
                                        Width="20px"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="tbxKubunMei" Width="200px" BorderStyle="None" ReadOnly="true"
                                        TabIndex="-1" BackColor="Transparent"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="hissu" style="font-weight: bold; width: 85px; height: 20px;">
                            取消&nbsp;</td>
                        <td class="hissu" colspan="2" style="height: 20px; width: 319px;">
                            <asp:DropDownList ID="ddlTorikesi" runat="server" Width="105px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="height: 20px; background-color: mistyrose">
                        <td class="hissu" style="font-weight: bold; height: 20px;">
                            加盟店コード
                        </td>
                        <td class="hissu" style="width: 70px; height: 20px;">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnTyokusetuNyuuryoku" />
                                    <asp:AsyncPostBackTrigger ControlID="btnTyokusetuNyuuryokuTyuusi" />
                                    <asp:AsyncPostBackTrigger ControlID="btnKameitenSearch" />
                                </Triggers>
                                <ContentTemplate>
                                    <asp:TextBox ID="tbxKyoutuKameitenCd" MaxLength="5" ReadOnly="true" CssClass="readOnly"
                                        runat="server" Width="60px" TabIndex="-1"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="hissu" style="font-weight: bold; width: 75px; height: 20px;">
                            加盟店名１
                        </td>
                        <td class="hissu" style="width: 310px; height: 20px;">
                            <asp:TextBox ID="tbxKyoutuKameitenMei1" runat="server" MaxLength="40" Width="300px"></asp:TextBox>
                        </td>
                        <td class="hissu" style="font-weight: bold; height: 20px;">
                            店カナ名１
                        </td>
                        <td class="hissu" colspan="2" style="height: 20px;">
                            <asp:TextBox ID="tbxKyoutukakeMei1" runat="server" MaxLength="20" Width="300px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="height: 20px">
                        <td colspan="2">
                        </td>
                        <td style="font-weight: bold;" class="koumokuMei">
                            加盟店名２
                        </td>
                        <td style="width: 310px">
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnTyokusetuNyuuryoku" />
                                    <asp:AsyncPostBackTrigger ControlID="btnTyokusetuNyuuryokuTyuusi" />
                                    <asp:AsyncPostBackTrigger ControlID="btnKameitenSearch" />
                                    <asp:AsyncPostBackTrigger ControlID="comdrp" />
                                    <asp:AsyncPostBackTrigger ControlID="common_drop1" />
                                </Triggers>
                                <ContentTemplate>
                                     <asp:TextBox ID="tbxKyoutuKameitenMei2" runat="server" MaxLength="40" Width="300px"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        
                           
                        </td>
                        <td style="font-weight: bold;" class="koumokuMei">
                            店カナ名２
                        </td>
                        <td colspan="2" style="width: 311px">
                            <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnTyokusetuNyuuryoku" />
                                    <asp:AsyncPostBackTrigger ControlID="btnTyokusetuNyuuryokuTyuusi" />
                                    <asp:AsyncPostBackTrigger ControlID="btnKameitenSearch" />
                                    <asp:AsyncPostBackTrigger ControlID="comdrp" />
                                    <asp:AsyncPostBackTrigger ControlID="common_drop1" />
                                </Triggers>
                                <ContentTemplate>
                                     <asp:TextBox ID="tbxKyoutukakeMei2" runat="server" MaxLength="20" Width="300px"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        
                            
                        </td>
                    </tr> 
                    <tr style="height: 20px;">
                        <td style="font-weight: bold; height: 20px;" class="koumokuMei">
                            ビルダ－NO
                        </td>
                        <td colspan="3" style="height: 20px">
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnKameitenSearch" />
                                    <asp:AsyncPostBackTrigger ControlID="btnTyokusetuNyuuryoku" />
                                </Triggers>
                                <ContentTemplate>
                                    <asp:TextBox ID="tbxBirudaNo" runat="server" MaxLength="9" Width="67px" CssClass="codeNumber"></asp:TextBox>
                                    <asp:Button ID="btnBirudaNo" runat="server" Text="検索" OnClientClick="return fncKameitenSearch();" />
                                    <asp:TextBox ID="tbxBirudaMei" runat="server" Width="307px" BorderStyle="None" ReadOnly="True"
                                        TabIndex="-1" BackColor="transparent"></asp:TextBox>
                                        
                                        
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td style="font-weight: bold; height: 20px;" class="koumokuMei">
                            系列コード
                        </td>
                        <td style="height: 20px" colspan="2">
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnTouroku" />
                                     <asp:AsyncPostBackTrigger ControlID="comdrp" />
                                    <asp:AsyncPostBackTrigger ControlID="common_drop1" />
                                </Triggers>
                                <ContentTemplate>
                                    <asp:TextBox ID="tbxKeiretuCd" runat="server" MaxLength="5" Width="65px" CssClass="codeNumber"></asp:TextBox>
                                    <asp:Button ID="btnKeiretuCd" runat="server" Text="検索" OnClientClick="return fncKeiretuSearch();" />
                                    <asp:TextBox ID="tbxKeiretuMei" runat="server" BorderStyle="None" ReadOnly="True"
                                        Width="192px" TabIndex="-1" BackColor="transparent"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr style="height: 20px;">
                        <td style="font-weight: bold; height: 20px;" class="koumokuMei">
                            営業所コード</td>
                        <td colspan="3" style="height: 20px">
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnTouroku" />
                                </Triggers>
                                <ContentTemplate>
                                    <asp:TextBox ID="tbxEigyousyoCd" runat="server" MaxLength="5" Width="67px" CssClass="codeNumber"></asp:TextBox>
                                    <asp:Button ID="btnEigyousyoCd" runat="server" Text="検索" OnClientClick="return fncEigyousyoSearch();" />
                                    <asp:TextBox ID="tbxEigyousyoMei" runat="server" Width="307px" BorderStyle="None"
                                        ReadOnly="True" TabIndex="-1" BackColor="transparent"></asp:TextBox></td>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <td style="font-weight: bold; height: 20px;" class="koumokuMei">
                                TH瑕疵ｺｰﾄﾞ</td>
                            <td colspan="2" style="height: 20px">
                                <asp:TextBox ID="tbxThKasiCd" runat="server" MaxLength="7" Width="65px"></asp:TextBox>
                            </td>
                    </tr>
                </tbody>
            </ContentTemplate>
        </asp:UpdatePanel>
    </table>
    <asp:HiddenField runat="server" ID="hidTourokuFlg" />
</asp:Content>

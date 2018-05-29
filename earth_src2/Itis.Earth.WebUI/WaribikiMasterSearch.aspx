<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master" CodeBehind="WaribikiMasterSearch.aspx.vb" Inherits="Itis.Earth.WebUI.WaribikiMasterSearch" 
    title="多棟割引マスタメンテナンス" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript" src="js/jhsearth.js"></script>
<script type="text/javascript">
    //window名付与
    var objWin = window;
    objWin.name = "earthMainWindow"
    initPage(); //画面初期設定
</script>
<div id="buySelName" runat="server" class="modalDiv" style="position:absolute;left:300px; top:140px;z-index:2;display:none;">
</div>
<div id="disableDiv" runat="server" style="position:absolute; left:0px; top:0px; width:1002px; height:620px; z-index:100;  FILTER:alpha(opacity=70);background-color:#000000; display:none;">
<iframe src="about:blank" id="hiddenIframe" width="100%" height="100%"></iframe>
</div>  
    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tbody>
            <tr>
                <th>
                    多棟割引マスタメンテナンス
                </th>
            </tr>
        </tbody>
    </table>
    <table style="text-align: left; width:940px;" class="mainTable" cellpadding="1">
    <tr>
        <td class="koumokuMei">加盟店コード</td>
        <td>
            <asp:TextBox runat="server" ID="tbxKameitenCdFrom" maxlength="5" style="width: 70px;IME-MODE:disabled;" AutoCompleteType="disabled"/>
            <asp:Button runat="server" ID="btnKameitenSearch1" Text="検索" OnClientClick="fncKameitenSearch('1');return false;" />
            <asp:Label ID="lbl1" runat="server" Text="Label">～</asp:Label>
            <asp:TextBox runat="server" ID="tbxKameitenCdTo" maxlength="5" style="width: 70px;IME-MODE:disabled;" AutoCompleteType="disabled"/>
            <asp:Button runat="server" ID="btnKameitenSearch2" Text="検索" OnClientClick="fncKameitenSearch('2');return false;"/>
        </td>    
        <td class="koumokuMei">加盟店名</td>
        <td>
            <asp:TextBox runat="server" ID="tbxKameitenMei" maxlength="40" style="width: 280px;IME-MODE:active;" AutoCompleteType="disabled"/>
        </td>
    </tr>
    <tr>
        <td class="koumokuMei">加盟店ｶﾅ</td>
        <td>
            <asp:TextBox runat="server" ID="tbxKameitenKana" maxlength="20" style="width: 180px;IME-MODE:active;" AutoCompleteType="disabled"/>
        </td>   
        <td class="koumokuMei">系列コード</td>
        <td>
            <asp:TextBox runat="server" ID="tbxKeiretuCd" maxlength="5" style="width: 70px;IME-MODE:disabled;" AutoCompleteType="disabled"/>
            <asp:Button runat="server" ID="btnKeiretuSearch" Text="検索" OnClientClick="fncKeiretuSearch();return false;" />
            <asp:TextBox runat="Server" ID="tbxKeiretuMei" BorderStyle="None" Width="280px" BackColor="#E6E6E6" ReadOnly="true" TabIndex="-1"></asp:TextBox>
        </td>   
    </tr>
    <tr>
        <td class="koumokuMei">商品コード</td>
        <td colspan="3">
            <asp:TextBox ID="tbxSyouhin" runat="server" MaxLength="8" style="width: 110px;IME-MODE:disabled;" AutoCompleteType="disabled"></asp:TextBox>
            <asp:Button runat="server" ID="btnSyouhinSearch" Text="検索" OnClientClick="fncSyouhinSearch();return false;"/>
            <asp:TextBox runat="Server" ID="tbxSyouhinMei" BorderStyle="None" Width="280px" BackColor="#E6E6E6" ReadOnly="true" TabIndex="-1"></asp:TextBox>
        </td>   
    </tr>
    <tr>
        <td colspan="4" rowspan="1" style="text-align: center">
            検索上限件数
            <asp:DropDownList runat="server" ID="ddlSearchCount">
                <asp:ListItem Value="100" Text="100件" Selected="true"></asp:ListItem>
                <asp:ListItem Value="max" Text="無制限"></asp:ListItem>
            </asp:DropDownList>
            <asp:Button runat="server" ID="btnKensaku" Text="検索実行" />
            <asp:Button runat="server" ID="btnClearWin" Text="条件クリア" OnClientClick="fncClearWin();return false;" />
        </td>
    </tr>
</table> 
<table style="height: 30px">
    <tr>
        <td>
            検索結果：
        </td>
        <td>
            <asp:Label runat="server" ID="lblCount"></asp:Label>
        </td>
        <td>
            件
        </td>
    </tr>
</table>
<table cellpadding="0" cellspacing="0" >
<tr>
    <td style="width: 370px" >
        <div id = "divHeadLeft" style="width: 371px; " >
        <table class="gridviewTableHeader" cellpadding="0" cellspacing="0"  width="371px">
            <tr>
                <td colspan ="2" style="border-left:0; border-top:0; background-color :#FFFFFF; height: 23px;">
                </td> 
            </tr>
            <tr>
                <td style="width: 25%;border-bottom :1px solid black;border-left :1px solid black;">
                    加盟店コード
                </td>
                <td style="width: 70%;border-bottom :1px solid black;">
                    加盟店名
                </td>        
            </tr>
        </table>
        </div>
    </td>
    <td style="width: 589px">
        <div id = "divHeadRight"　runat = "server" style="width: 589px;overflow-y: hidden;overflow-x: hidden; border-left:1px solid black; border-right :1px solid black;" >  
            <table class="gridviewTableHeader" cellpadding="0" cellspacing="0" width ="1110px" >
                <tr>
                    <td colspan ="2">
                        棟数1　4棟～9棟
                    </td>
                    <td colspan ="2">
                        棟数2　10棟～19棟
                    </td>
                    <td colspan ="2">
                        棟数3　20棟以上
                    </td>     
                </tr>
                <tr>
                    <td style="width: 84px;border-bottom :1px solid black;">
                        商品コード
                    </td>
                    <td style="width: 289px;border-bottom :1px solid black;">
                        商品名
                    </td>
                    <td style="width: 85px;border-bottom :1px solid black;">
                        商品コード
                    </td>
                    <td style="width: 288px;border-bottom :1px solid black;">
                        商品名
                    </td>
                    <td style="width: 85px;border-bottom :1px solid black;">
                        商品コード
                    </td>
                    <td style="width: 290px;border-bottom :1px solid black;">
                        商品名
                    </td>
                </tr>
            </table>
       </div> 
       </td>
</tr> 
<tr >
    <td style="width: 370px">
        <div id = "divBodyLeft"  runat="server" style ="overflow-y: hidden; border-left :1px solid black;border-top :1px solid black;border-bottom :1px solid black;">
            <asp:GridView ID="grdBodyLeft" runat="server"  Width ="370px" BackColor="White" CssClass="tableMeiSai" ShowHeader="False" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="kameiten_cd">
                        <ItemStyle Width="92px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="kameiten_mei">
                        <ItemStyle Width="285px" />
                    </asp:BoundField>
                </Columns>
        <SelectedRowStyle ForeColor="White" />
		<AlternatingRowStyle BackColor="LightCyan" />
		<RowStyle Height="21px" />
            </asp:GridView>
        </div>
    </td>
    <td style="width: 590px">
       <div id = "divBodyRight"　runat = "server" style=" width: 590px; overflow-x: hidden ;overflow-y: hidden ; border-right :1px solid black;border-top :1px solid black;border-bottom :1px solid black;">       
     <asp:GridView ID="grdBodyRight" runat="server"  Width ="1110px" BackColor="White" CssClass="tableMeiSai" ShowHeader="False" 
         BorderWidth="1px"   CellPadding="0" AutoGenerateColumns="False" >
               <Columns>
                    <asp:BoundField DataField ="syouhin_cd1" NullDisplayText="&nbsp;&nbsp;" >
                          <ItemStyle Width="85px" HorizontalAlign="Left" />
                    </asp:BoundField>	
                    <asp:BoundField DataField ="syouhin_mei1" NullDisplayText="&nbsp;&nbsp;" >
                          <ItemStyle Width="290px" HorizontalAlign="Left" />
                    </asp:BoundField>	
                    <asp:BoundField DataField ="syouhin_cd2" NullDisplayText="&nbsp;&nbsp;" >
                          <ItemStyle Width="85px" HorizontalAlign="Left" />
                    </asp:BoundField>	
                    <asp:BoundField DataField ="syouhin_mei2" NullDisplayText="&nbsp;&nbsp;" >
                          <ItemStyle Width="290px" HorizontalAlign="Left" />
                    </asp:BoundField>	
                    <asp:BoundField DataField ="syouhin_cd3" NullDisplayText="&nbsp;&nbsp;" >
                          <ItemStyle Width="85px" HorizontalAlign="Left" />
                    </asp:BoundField>	
                    <asp:BoundField DataField ="syouhin_mei3" NullDisplayText="&nbsp;&nbsp;" >
                          <ItemStyle Width="290px" HorizontalAlign="Left" />
                    </asp:BoundField>
               </Columns>
        <SelectedRowStyle ForeColor="White" />
		<AlternatingRowStyle BackColor="LightCyan" />
		<RowStyle Height="21px" />
           </asp:GridView>      
       </div> 
    </td>
     <td valign = "top" style="width: 17px">
        <div id="divHiddenMeisaiV" runat = "server" style=" overflow:auto;height:254px;width:30px; margin-left:-14px;">
            <table height="<%=scrollHeight%>">
                <tr><td></td></tr>
            </table>
        </div>
     </td>
</tr>
<tr>
    <td style="height: 16px; width: 370px;">
    </td>
    <td style="width: 590px">
    <div style=" overflow-x:hidden;height:18px;width:590px;margin-top:-1px;">
            <div id="divHiddenMeisaiH" runat ="server" style="overflow:auto;height:18px;width:607px;margin-top:0px;">
                <table style="width: 1110px;">
                    <tr><td></td></tr>
                </table>
            </div>
    </div>
    </td>
    <td style="height: 16px; width: 17px;">
    </td>
</tr>
</table>
<table class="buttonsTable">
	<tr>
		<td>
			<asp:Button ID="btnBack" runat="server" CssClass="kyoutuubutton" Text="戻る" />
	    </td>		
	    <td>
			<asp:Button ID="btnCsvOut" runat="server"  CssClass="kyoutuubutton" Text="CSV出力" />
	    </td>		
	    <td>
			<asp:Button ID="btnZenkenCSV" runat="server" CssClass="kyoutuubutton" Text="全件出力" />
	    </td>
	</tr>
</table>
<input type="hidden" id="HidKameitenCd" value="" runat="server" />
<input type="hidden" id="HidSyouhinMei" value="" runat="server" />
<input type="hidden" id="HidKeiretuMei" value="" runat="server" />
</asp:Content>

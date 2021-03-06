<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master" CodeBehind="YosinTougetuInput.aspx.vb" Inherits="Itis.Earth.WebUI.YosinTougetuInput" 
    title="与信明細情報" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript" src="js/jhsearth.js"></script>
<script type="text/javascript">
    //window名付与
    var objWin = window;
    objWin.name = "earthMainWindow"
    initPage(); //画面初期設定
    
function divBodyRight_onclick() {

}

</script>
<table style="text-align:left; width:900px;" border="0" cellpadding="0" cellspacing="2" class="titleTable">
    <tbody>
        <tr>
            <th style="width:780px; height: 13px;">
                与信明細情報照会
            </th>
            <th rowspan="1" style="text-align:left; color:Red; height: 13px;" align="center" valign="middle">
            </th>
        </tr>
        <tr>
            <th style="width: 780px; height: 5px">
            </th>
            <th align="center" rowspan="1" style="color: red; height: 10px; text-align: left"
                valign="middle">
            </th>
        </tr>
    </tbody>
</table>
<table style="text-align:left; width:962px;" class="mainTable">
    <tr>
        <td class="koumokuMei" rowspan="2" style="font-weight: bold; width: 87px">
    名寄先コード</td>
        <td rowspan="2" style="width: 80px">
            <asp:TextBox runat="server" ID="tbxNayoseSakiCd" style="width:70px;" cssclass="readOnly" TabIndex="-1"/>
        </td>
        <td class="koumokuMei" rowspan="2" style="font-weight: bold; width: 74px">
    名寄先名１
        </td>
        <td rowspan="2" style="width: 262px">
            <asp:TextBox runat="server" ID="tbxNayoseSakiName1" style="width:250px;" cssclass="readOnly" TabIndex="-1"/></td>
        <td style="width: 259px">
        <asp:CheckBox ID="chkA" runat="server" Text="調査のみ" Checked="True" />
            &nbsp;
            <asp:CheckBox ID="chkB" runat="server" Text="工事のみ" Checked="True" />
            &nbsp;
            <asp:CheckBox ID="chkC" runat="server" Text="その他のみ" Checked="True" /></td>
        <td align="center" style="text-align: left; border-bottom-style: none">
        <asp:Button ID="Button2" runat="server" Text="絞込ボタン" Width="80px" /></td>
    </tr>
<tr >
    <td style="width: 259px">
        <asp:CheckBox ID="chk1" runat="server" Text="予定のみ" Checked="True" />&nbsp; &nbsp;<asp:CheckBox ID="chk2" runat="server" Text="実績のみ" Checked="True" /></td>
    <td align="center" style="border-top-style: none; text-align: left"><asp:Button ID="btnCHK" runat="server" Text="全チェック" Width="80px"/>
        <asp:Button ID="btnCLN" runat="server" Text="全クリア" /></td>

</tr> 
    <tr>
        <td class="koumokuMei" style="font-weight: bold; width: 87px">
            売上額合計</td>
        <td colspan="2">
            <asp:TextBox ID="tbxGoukei" style="text-align:right;" runat="server" Width="152px" ReadOnly  cssclass="readOnly"></asp:TextBox></td>
            <td colspan ="3">
                <asp:Label ID="Label1" runat="server" Text="←売上額合計はリアルタイム計算のため、夜間処理で確定する与信画面の当月売上額と差異のある場合があります"
                    Width="480px" ForeColor="Red"></asp:Label>
                <br />
                <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="(売上額合計が正確な数値となります）" Width="480px"></asp:Label></td>
    </tr>
</table>

 
<br>
<table cellpadding="0" cellspacing="0" border ="0" style="width: 962px">
    <tr>
        <td style=" width: 357px;">
            <div id = "divHeadLeft" style="width: 630px;overflow-y: hidden;overflow-x: hidden;" >
               <table runat ="server" id="headLeft" visible ="true"  style=" border-bottom: 1px solid #999999;border-left: 1px solid #999999; width: 630px; height: 47px;" class="gridviewTableHeader" cellpadding="0" cellspacing="0" >
                   <tr>
                        <td style="width: 66px; text-align: center;">
                            <table border ="0" cellpadding ="0" cellspacing ="0" >
                                <tr>
                                    <td rowspan="2"  style="border-width: 0px; text-align: left;" align="left">
                                        加<br />
                                        
                                        CODE</td>
                                    <td  style="border-width: 0px; vertical-align: bottom; padding-bottom: 0px; width: 21px;">
                                        <asp:LinkButton ID="Akameiten_cd" runat="server" Font-Underline="False" ForeColor="SkyBlue">▲</asp:LinkButton></td>
                                </tr>
                                <tr>
                                <td  style="border-width: 0px; vertical-align: top; padding-top: 0px; width: 21px;">
                                 <asp:LinkButton
                                ID="Dkameiten_cd" runat="server" Font-Underline="False" ForeColor="SkyBlue">▼</asp:LinkButton></td>
                         </tr>
                         </table>
                         
                         </td>
                        <td style="width: 104px"><table border ="0" cellpadding ="0" cellspacing ="0" >
                            <tr>
                                <td rowspan="2"  style="border-width: 0px;">
                            加盟店名
                                </td>
                                <td  style="border-width: 0px; vertical-align: bottom; padding-bottom: 0px;"><asp:LinkButton ID="Akameiten_mei1" runat="server" Font-Underline="False" ForeColor="SkyBlue">▲</asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td  style="border-width: 0px; vertical-align: top; padding-top: 0px;"><asp:LinkButton
                                ID="Dkameiten_mei1" runat="server" Font-Underline="False" ForeColor="SkyBlue">▼</asp:LinkButton></td>
                            </tr>
                        </table>
                        </td>
                        <td style="width: 50px"><table border ="0" cellpadding ="0" cellspacing ="0" >
                            <tr>
                                <td rowspan="2"  style="border-width: 0px;">
                            県
                                </td>
                                <td  style="border-width: 0px; vertical-align: bottom; padding-bottom: 0px;">
                                    <asp:LinkButton ID="Atodouhuken_cd" runat="server" Font-Underline="False" ForeColor="SkyBlue">▲</asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td  style="border-width: 0px; vertical-align: top; padding-top: 0px;">
                                    <asp:LinkButton ID="Dtodouhuken_cd" runat="server" Font-Underline="False" ForeColor="SkyBlue">▼</asp:LinkButton></td>
                            </tr>
                        </table>
                        </td>
                        <td style="width: 85px">
                            <table border ="0" cellpadding ="0" cellspacing ="0" >
                                <tr>
                                    <td rowspan="2"  style="border-width: 0px;">
                            物件NO
                                    </td>
                                    <td  style="border-width: 0px; vertical-align: bottom; padding-bottom: 0px;"><asp:LinkButton ID="Ahosyousyo_no" runat="server" Font-Underline="False" ForeColor="SkyBlue">▲</asp:LinkButton></td>
                                </tr>
                                <tr>
                                    <td  style="border-width: 0px; vertical-align: top; padding-top: 0px;"><asp:LinkButton
                                ID="Dhosyousyo_no" runat="server" Font-Underline="False" ForeColor="SkyBlue">▼</asp:LinkButton></td>
                                </tr>
                            </table>
                        </td>
                       <td style="width: 158px">
                           <table border ="0" cellpadding ="0" cellspacing ="0" >
                               <tr>
                                   <td rowspan="2"  style="border-width: 0px;">
                            施主名
                                   </td>
                                   <td  style="border-width: 0px; vertical-align: bottom; padding-bottom: 0px;"><asp:LinkButton ID="Asesyu_mei" runat="server" Font-Underline="False" ForeColor="SkyBlue">▲</asp:LinkButton></td>
                               </tr>
                               <tr>
                                   <td  style="border-width: 0px; vertical-align: top; padding-top: 0px;"><asp:LinkButton
                                ID="Dsesyu_mei" runat="server" Font-Underline="False" ForeColor="SkyBlue">▼</asp:LinkButton></td>
                               </tr>
                           </table>
                       </td>
                       <td style="width: 33px">
                           <table border ="0" cellpadding ="0" cellspacing ="0" >
                               <tr>
                                   <td rowspan="2"  style="border-width: 0px;">
                                       種<br />
                                       別
                                   </td>
                                   <td  style="border-width: 0px; vertical-align: bottom; padding-bottom: 0px;"><asp:LinkButton ID="Acol1" runat="server" Font-Underline="False" ForeColor="SkyBlue">▲</asp:LinkButton></td>
                               </tr>
                               <tr>
                                   <td  style="border-width: 0px; vertical-align: top; padding-top: 0px;"><asp:LinkButton
                                ID="Dcol1" runat="server" Font-Underline="False" ForeColor="SkyBlue">▼</asp:LinkButton></td>
                               </tr>
                           </table>
                       </td>
                       <td style="width: 33px">
                       <table border ="0" cellpadding ="0" cellspacing ="0" >
                           <tr>
                               <td rowspan="2"  style="border-width: 0px;">
                                   予<br />
                                   実
                               </td>
                               <td  style="border-width: 0px; vertical-align: bottom; padding-bottom: 0px;">
                                   <asp:LinkButton ID="Acol2" runat="server" Font-Underline="False" ForeColor="SkyBlue">▲</asp:LinkButton></td>
                           </tr>
                           <tr>
                               <td  style="border-width: 0px; vertical-align: top; padding-top: 0px;"><asp:LinkButton ID="Dcol2"
                                    runat="server" Font-Underline="False" ForeColor="SkyBlue">▼</asp:LinkButton></td>
                           </tr>
                       </table>
                       </td>
                       <td><table border ="0" cellpadding ="0" cellspacing ="0" >
                           <tr>
                               <td rowspan="2"  style="border-width: 0px;">
                            売上日
                               </td>
                               <td  style="border-width: 0px; vertical-align: bottom; padding-bottom: 0px;"><asp:LinkButton ID="Auri_date" runat="server" Font-Underline="False"
                                ForeColor="SkyBlue">▲</asp:LinkButton></td>
                           </tr>
                           <tr>
                               <td  style="border-width: 0px; vertical-align: top; padding-top: 0px;"><asp:LinkButton ID="Duri_date"
                                    runat="server" Font-Underline="False" ForeColor="SkyBlue">▼</asp:LinkButton></td>
                           </tr>
                       </table>
                       </td>
                                </tr>
               </table>
            </div>
        </td>
        <td style="width: 315px; ">
             <div id = "divHeadRight"　runat = "server" style="width:312px;overflow-y: hidden;overflow-x: hidden;border-right :1px solid black;" >  

               <table runat ="server" id="headRight" visible ="true"  style=" border-bottom: 1px solid #999999; border-left: 1px solid #999999;width:380px; height: 47px;" class="gridviewTableHeader" cellpadding="0" cellspacing="0">
                       <tr>
                       
                        <td style="width: 70px;">
                        <table border ="0" cellpadding ="0" cellspacing ="0" >
                            <tr>
                                <td rowspan="2"  style="border-width: 0px;">
                                    予定日
                                </td>
                                <td  style="border-width: 0px; vertical-align: bottom; padding-bottom: 0px;"><asp:LinkButton ID="Asyoudakusyo_tys_date" runat="server" Font-Underline="False" ForeColor="SkyBlue">▲</asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td  style="border-width: 0px; vertical-align: top; padding-top: 0px;"><asp:LinkButton
                                ID="Dsyoudakusyo_tys_date" runat="server" Font-Underline="False" ForeColor="SkyBlue">▼</asp:LinkButton></td>
                            </tr>
                        </table>
                        </td>
                        <td style="width: 78px;"><table border ="0" cellpadding ="0" cellspacing ="0" >
                            <tr>
                                <td rowspan="2"  style="border-width: 0px;">
                            売上金額
                                </td>
                                <td  style="border-width: 0px; vertical-align: bottom; padding-bottom: 0px;"><asp:LinkButton ID="Akin" runat="server" Font-Underline="False" ForeColor="SkyBlue">▲</asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td  style="border-width: 0px; vertical-align: top; padding-top: 0px;"><asp:LinkButton
                                ID="Dkin" runat="server" Font-Underline="False" ForeColor="SkyBlue">▼</asp:LinkButton></td>
                            </tr>
                        </table>
                        </td>
                        <td style="width: 128px;"><table border ="0" cellpadding ="0" cellspacing ="0" >
                            <tr>
                                <td rowspan="2"  style="border-width: 0px;">
                            商品名
                                </td>
                                <td  style="border-width: 0px; vertical-align: bottom; padding-bottom: 0px;"><asp:LinkButton ID="Asyouhin_mei" runat="server" Font-Underline="False" ForeColor="SkyBlue">▲</asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td  style="border-width: 0px; vertical-align: top; padding-top: 0px;"><asp:LinkButton
                                ID="Dsyouhin_mei" runat="server" Font-Underline="False" ForeColor="SkyBlue">▼</asp:LinkButton></td>
                            </tr>
                        </table>
                        </td>
                        <td >
                            <table border ="0" cellpadding ="0" cellspacing ="0" >
                            <tr>
                                <td rowspan="2"  style="border-width: 0px;">
                            依頼日
                                </td>
                                <td  style="border-width: 0px; vertical-align: bottom; padding-bottom: 0px;"><asp:LinkButton ID="Airai_date" runat="server" Font-Underline="False"
                                ForeColor="SkyBlue">▲</asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td  style="border-width: 0px; vertical-align: top; padding-top: 0px;"><asp:LinkButton ID="Dirai_date"
                                    runat="server" Font-Underline="False" ForeColor="SkyBlue">▼</asp:LinkButton></td>
                            </tr>
                        </table>
                        </td>
                    </tr>
                </table>
           </div> 
        </td>
        <td>
        </td>
    </tr> 
    <tr  >
        <td style="height: 176px; width: 357px;" valign="top" >
            <div id = "divBodyLeft"  runat="server" onmousewheel="wheel();" style="width:629px; height:296px; overflow-x:hidden;overflow-y: hidden; border-left :1px solid black;border-top :1px solid black;border-bottom :1px solid black;">
       
        <asp:GridView id="grdItiranLeft" runat="server"  BackColor="White" CssClass="tableMeiSai"
                BorderWidth="1px" ShowHeader="False" CellPadding="0" >
            <SelectedRowStyle ForeColor="White" />
		    <AlternatingRowStyle BackColor="LightCyan" />
            </asp:GridView>
            </div>
        </td>
        <td style="width: 315px; height: 176px;" valign="top" >
           <div id = "divBodyRight"　runat = "server" onmousewheel="wheel();" style="width:312px;height:296px; overflow-x: hidden ;overflow-y: hidden ;border-right :1px solid black;border-top :1px solid black;border-bottom :1px solid black;" onclick="return divBodyRight_onclick()">       
 
        <asp:GridView id="grdItiranRight" runat="server"  BackColor="White" CssClass="tableMeiSai"
                BorderWidth="1px" ShowHeader="False" CellPadding="0"  style=" width:380px;" >
            <SelectedRowStyle ForeColor="White" />
		    <AlternatingRowStyle BackColor="LightCyan" />
            </asp:GridView>
           </div> 
        </td>
         <td valign = "top" style="width: 17px; height: 176px;">
            <div id="divHiddenMeisaiV" runat = "server" style=" overflow:auto;height:298px;width:30px; margin-left:-14px;" onscroll="fncScrollV();">
                <table height="<%=scrollHeight%>">
                    <tr><td></td></tr>
                </table>
            </div>
         </td>
    </tr>
    <tr>
        <td style="height: 39px; width: 357px;">
        </td>
        <td style="width: 315px; height: 39px;" valign="top" >
            <div style=" overflow-x:hidden;height:18px;width:312px;margin-top:-1px;">
                <div id="divHiddenMeisaiH" runat ="server" style="overflow:auto;height:18px;width:328px;margin-top:0px;" onscroll="fncScrollH();">
                    <table style="width:380px;">
                        <tr><td></td></tr>
                    </table>
                </div>
            </div>
        </td>
        <td style="height: 39px; width: 17px;">

        </td>
    </tr>
    </table>
<table class="buttonsTable" style="margin-top:-25px;">
	<tr>
		<td style="width: 317px">
			<asp:Button ID="btnModoru" runat="server" CssClass="kyoutuubutton" Text="戻　る" />
            <asp:Button ID="btnCSV" runat="server" CssClass="kyoutuubutton" Text="CSV出力" /></td>
	</tr>
</table>
<asp:HiddenField runat="server" ID="hidScroll" />
<asp:HiddenField runat="server" ID="hidSort" />
</asp:Content>

<%@ Page Language="VB" MasterPageFile="~/EKKSMaster.master" AutoEventWireup="false"
    CodeFile="SitenTukibetuKeikakuchiSearchList.aspx.vb" Inherits="SitenTukibetuKeikakuchiSearchList"
    Title="éxìX åéï åvâÊílê›íË" %>
<%@ MasterType VirtualPath="~/EKKSMaster.master" %>
<%@ Register Src="CommonControl/CommonButton.ascx" TagName="CommonButton" TagPrefix="uc1" %>
<%@ Register Src="CommonControl/SitenTukibetuKeikakuchiList.ascx" TagName="SitenTukibetuKeikakuchiList" TagPrefix="uc2" %>
<%@ Register Assembly="Lixil.JHS_EKKS.Utilities" Namespace="Lixil.JHS_EKKS.Utilities"
    TagPrefix="cc1" %>     
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script language="javascript" type="text/javascript">
    //onload
    window.onload = function(){
        window.name = "<%=CommonConstBC.eigyouKeikakuKanri%>"
        setMenuBgColor();
    }      
</script>

<asp:HiddenField ID="hidSeni" runat="server" />
<%--<div id="buySelName" runat="server" class="modalDiv" style="position:absolute;left:300px; top:140px;z-index:2;display:none;">
</div>
<div id="disableDiv" runat="server" style="position:absolute; left:0px; top:0px; width:1002px; height:590px; z-index:100;  FILTER:alpha(opacity=70);background-color:#000000; display:none;">
    <iframe src="about:blank" id="hiddenIframe" width="100%" height="100%"></iframe>
</div>--%>
    <table cellpadding="0" cellspacing="0" width="980px">
        <tr style="padding-top: 10px;">
            <td colspan="6">
                <span style="font-size: 15px; font-weight: bold;">éxìX åéï åvâÊílê›íË</span></td>
        </tr>
        <tr style="height: 10px;">
            <td colspan="6"></td>
        </tr>
        <tr>
            <td style="width:10px"></td>
            <td style="font-weight: bold; width:120px;">&nbsp;&nbsp;îNìx ëIë</td>
            <td style="width:150px;">
                <cc1:CommonDropDownList ID="ddlNendo" runat="server" DataTextField="meisyou" DataValueField="code" IsAddNullRow="True">
                </cc1:CommonDropDownList></td>
            <td class="koumokuMei" style="text-align:center; border-bottom:1px solid #000000; border-left:1px solid #000000; border-top:1px solid #000000;width:120px;">
                éxìXñº</td>
            <td style="border:1px solid #000000;width:220px;">
                <asp:TextBox ID="tbxSiten" runat="server"></asp:TextBox>
                <asp:HiddenField ID="hidCitenCd" runat="server"></asp:HiddenField>
                <asp:Button ID="btnShitenKensaku" runat="server" Text="åüçı" /></td>
            <td style=" padding-left:10px;">
                <asp:Button ID="btnKensaku" runat="server" Text="åéï åvâÊï\é¶" /></td>
        </tr>
        <tr style="padding-top: 10px;">
            <td style="width:5px"></td>
            <td colspan="5">
<%--                <table cellpadding="0" cellspacing="0" style="border-width:1px; border-style:solid;">
                    <tr>
                        <td align="center" style="width:100px;" class="koumokuMei">åvâÊ í≤ç∏åèêî</td>
                        <td style="background-color: #e6e6e6;width:180px">
                            <cc1:CommonNumber ID="numKeikakuKensuu" MaxLength="12" runat="server" Width="120px"></cc1:CommonNumber>åè à»è„</td>
                        <td align="center" style="width:100px;" class="koumokuMei">åvâÊ îÑè„ã‡äz</td>
                        <td style="background-color: #e6e6e6;width:150px">
                            <cc1:CommonNumber ID="numKeikakuUriKingaku" MaxLength="12" runat="server" Width="120px" ObjName=""></cc1:CommonNumber>â~</td>
                        <td align="center" style="width:100px;" class="koumokuMei">åvâÊ ëeóòã‡äz</td>
                        <td style="background-color: #e6e6e6;">
                            <cc1:CommonNumber ID="numKeikakuArari" MaxLength="12" runat="server" Width="120px"></cc1:CommonNumber>â~&nbsp;&nbsp;</td>
                    </tr>
                </table>--%>
            </td>
        </tr>
        <tr >
            <td style="width:5px"></td>
            <td colspan="4">
                <uc1:CommonButton ID="btnSyuturyoku" runat="server" Text="ExcelèoóÕ"/>&nbsp;&nbsp;&nbsp;&nbsp;
                <uc1:CommonButton ID="btnTorikomi" runat="server" Text="CSVéÊçû" ButtonKegen="SitenbetuGetujiKeikakuTorikomi"/>
                <asp:Button ID="btnSyuturyoku1" runat="server" Text="ExcelèoóÕóp" style="visibility:hidden"/>
            </td>
            <td  style="text-align:right "> 
                 <asp:Label ID="lblSumi" runat="server" Font-Bold="true" Width="152px"></asp:Label>
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" width="980px" style="margin-top:3px;">
        <tr>
            <td style="width:5px; height: 383px">
            </td>
            <td style="vertical-align: top;">
                <div id="divMeisai" runat="server" style="overflow: auto; width: 980px; height: 383px;">
                <table cellpadding="0" cellspacing="0" style="width:2380px;">     
                    <tr>
                        <td>                   
                            <uc2:SitenTukibetuKeikakuchiList ID="SitenTukibetuKeikakuchiList4" runat="server" />
                            <uc2:SitenTukibetuKeikakuchiList ID="SitenTukibetuKeikakuchiList5" runat="server" MarginLeft="628px" MarginTop="-110px" TitleVisable="false" />                        
                            <uc2:SitenTukibetuKeikakuchiList ID="SitenTukibetuKeikakuchiList6" runat="server" MarginLeft="1216px" MarginTop="-110px" TitleVisable="false" />
                            <uc2:SitenTukibetuKeikakuchiList ID="SitenTukibetuKeikakuchiList456" runat="server" MarginLeft="1825px" MarginTop="-110px"  TitleVisable="false" backcolorWhite="true" />
                        </td>
                    </tr>
                    <tr style="height:18px;"></tr> 
                    <tr>
                        <td>                   
                            <uc2:SitenTukibetuKeikakuchiList ID="SitenTukibetuKeikakuchiList7" runat="server" />
                            <uc2:SitenTukibetuKeikakuchiList ID="SitenTukibetuKeikakuchiList8" runat="server" MarginLeft="628px" MarginTop="-110px" TitleVisable="false" />                        
                            <uc2:SitenTukibetuKeikakuchiList ID="SitenTukibetuKeikakuchiList9" runat="server" MarginLeft="1216px" MarginTop="-110px" TitleVisable="false" />
                            <uc2:SitenTukibetuKeikakuchiList ID="SitenTukibetuKeikakuchiList789" runat="server" MarginLeft="1825px" MarginTop="-110px"  TitleVisable="false" backcolorWhite="true" />
                        </td>
                    </tr>
                    <tr style="height:18px;"></tr> 
                    <tr>
                        <td>                   
                            <uc2:SitenTukibetuKeikakuchiList ID="SitenTukibetuKeikakuchiList10" runat="server" />
                            <uc2:SitenTukibetuKeikakuchiList ID="SitenTukibetuKeikakuchiList11" runat="server" MarginLeft="628px" MarginTop="-110px" TitleVisable="false" />                        
                            <uc2:SitenTukibetuKeikakuchiList ID="SitenTukibetuKeikakuchiList12" runat="server" MarginLeft="1216px" MarginTop="-110px" TitleVisable="false" />
                            <uc2:SitenTukibetuKeikakuchiList ID="SitenTukibetuKeikakuchiList101112" runat="server" MarginLeft="1825px" MarginTop="-110px"  TitleVisable="false" backcolorWhite="true" />
                        </td>
                    </tr>
                    <tr style="height:18px;"></tr> 
                    <tr>
                        <td>                   
                            <uc2:SitenTukibetuKeikakuchiList ID="SitenTukibetuKeikakuchiList1" runat="server" />
                            <uc2:SitenTukibetuKeikakuchiList ID="SitenTukibetuKeikakuchiList2" runat="server" MarginLeft="628px" MarginTop="-110px" TitleVisable="false" />                        
                            <uc2:SitenTukibetuKeikakuchiList ID="SitenTukibetuKeikakuchiList3" runat="server" MarginLeft="1216px" MarginTop="-110px" TitleVisable="false" />
                            <uc2:SitenTukibetuKeikakuchiList ID="SitenTukibetuKeikakuchiList123" runat="server" MarginLeft="1825px" MarginTop="-110px"  TitleVisable="false" backcolorWhite="true" />
                        </td>
                    </tr>
                </table>
                <br />
<%--                <table cellpadding="0" cellspacing="0" style="width:2280px;">                     
                    <tr>
                        <td>
                            <uc2:SitenTukibetuKeikakuchiList ID="SitenTukibetuKeikakuchiList1" runat="server" />
                        </td>
                        <td>
                            <uc2:SitenTukibetuKeikakuchiList ID="SitenTukibetuKeikakuchiList2" runat="server" />                        
                        </td>
                        <td>
                            <uc2:SitenTukibetuKeikakuchiList ID="SitenTukibetuKeikakuchiList3" runat="server" />
                        </td>
                        <td style="width:20px;">
                        </td>
                        <td >
                            <uc2:SitenTukibetuKeikakuchiList ID="SitenTukibetuKeikakuchiList4" runat="server" />
                        </td>
                    </tr>
                    <tr style="height:18px;"></tr> 
                    <tr>
                        <td>
                            <uc2:SitenTukibetuKeikakuchiList ID="SitenTukibetuKeikakuchiList5" runat="server" />
                        </td>
                        <td>
                            <uc2:SitenTukibetuKeikakuchiList ID="SitenTukibetuKeikakuchiList6" runat="server" />                        
                        </td>
                        <td>
                            <uc2:SitenTukibetuKeikakuchiList ID="SitenTukibetuKeikakuchiList7" runat="server" />
                        </td>
                        <td>
                        </td>
                        <td >
                            <uc2:SitenTukibetuKeikakuchiList ID="SitenTukibetuKeikakuchiList8" runat="server" />
                        </td>
                    </tr>
                    <tr style="height:18px;"></tr> 
                    <tr>
                        <td>
                            <uc2:SitenTukibetuKeikakuchiList ID="SitenTukibetuKeikakuchiList9" runat="server" />
                        </td>
                        <td>
                            <uc2:SitenTukibetuKeikakuchiList ID="SitenTukibetuKeikakuchiList10" runat="server" />                        
                        </td>
                        <td>
                            <uc2:SitenTukibetuKeikakuchiList ID="SitenTukibetuKeikakuchiList11" runat="server" />
                        </td>
                        <td>
                        </td>
                        <td >
                            <uc2:SitenTukibetuKeikakuchiList ID="SitenTukibetuKeikakuchiList12" runat="server" />
                        </td>
                    </tr>
                    <tr style="height:18px;"></tr> 
                    <tr>
                        <td>
                            <uc2:SitenTukibetuKeikakuchiList ID="SitenTukibetuKeikakuchiList13" runat="server" />
                        </td>
                        <td>
                            <uc2:SitenTukibetuKeikakuchiList ID="SitenTukibetuKeikakuchiList14" runat="server" />                        
                        </td>
                        <td>
                            <uc2:SitenTukibetuKeikakuchiList ID="SitenTukibetuKeikakuchiList15" runat="server" />
                        </td>
                        <td>
                        </td>
                        <td >
                            <uc2:SitenTukibetuKeikakuchiList ID="SitenTukibetuKeikakuchiList16" runat="server" />
                        </td>
                    </tr>
                    <tr style="height:18px;"></tr> 
                </table>--%>
                
                <table cellpadding="0" cellspacing="0" style="width:2380px;">
                    <tr>
                        <td>
                            <uc2:SitenTukibetuKeikakuchiList ID="SitenTukibetuKeikakuchiListKamiki" runat="server" backcolorWhite="true" />
                        </td>
                        <td>
                            <uc2:SitenTukibetuKeikakuchiList ID="SitenTukibetuKeikakuchiListSimoki" runat="server" backcolorWhite="true" />                        
                        </td>
                        <td>
                            <uc2:SitenTukibetuKeikakuchiList ID="SitenTukibetuKeikakuchiListNendo" runat="server" backcolorWhite="true" />
                        </td>
                    </tr>
                </table>

                </div>
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" width="980px">
        <tr>
            <td style="width:5px; ">
            </td>
            <td style="text-align:right;">
<%--                <uc1:CommonButton ID="CommonButton1" runat="server" Text="îNä‘åvâÊí≤ç∏åèêî çáåv  ∫Àﬂ∞" />&nbsp;&nbsp;&nbsp;&nbsp;
                <uc1:CommonButton ID="CommonButton2" runat="server" Text="îNä‘åvâÊîÑè„ã‡äz çáåv  ∫Àﬂ∞ " />&nbsp;&nbsp;&nbsp;&nbsp;
                <uc1:CommonButton ID="CommonButton3" runat="server" Text="îNä‘åvâÊëeóòã‡äz çáåv  ∫Àﬂ∞ " />&nbsp;&nbsp;&nbsp;&nbsp;--%>
            </td>
        </tr>
        <tr>
            <td style="width:5px; ">
            </td>
            <td style="text-align:right;">
                <uc1:CommonButton ID="btnKeikakuMinaosi" runat="server" Text="åvâÊå©íºÇµ" BackColor="gold" ButtonKegen="SitenbetuGetujiKeikakuMinaosi"/>&nbsp;&nbsp;&nbsp;&nbsp;
                <uc1:CommonButton ID="btnSitenbetuTukiConfirm" runat="server" Text="éxìXï  åéï åvâÊílämíË" BackColor="#FF00FE"Å@ButtonKegen="SitenbetuGetujiKeikakuKakutei"/>&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>

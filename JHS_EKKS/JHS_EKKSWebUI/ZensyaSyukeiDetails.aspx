<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ZensyaSyukeiDetails.aspx.vb" Inherits="ZensyaSyukeiDetails" title="Untitled Page" %>
<%@ Register Assembly="Lixil.JHS_EKKS.Utilities" Namespace="Lixil.JHS_EKKS.Utilities" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ëSé–èWåvè⁄ç◊</title>
    <link rel="stylesheet" href="App_Themes/css/JHS_EKKS.css" type="text/css" />
    <script language="javascript" type="text/javascript" src="Js/JHS_EKKS.js"></script>
</head>
<body>
<form id="form1" runat="server">
<script language="javascript" type="text/jscript" >
/*ÉOÉäÉbÉhÇÃscroll ê›íË*/
function fncSetScrollLeft(){
    var objDivBottom = document.getElementById("divBottom");
    
    document.getElementById("divHead").scrollLeft = objDivBottom.scrollLeft;
    
    document.getElementById("divEigyouMeisai1").scrollLeft = objDivBottom.scrollLeft;
    document.getElementById("divSinkiMeisai1").scrollLeft = objDivBottom.scrollLeft;
    document.getElementById("divTokuhanMeisai1").scrollLeft = objDivBottom.scrollLeft;
    document.getElementById("divFCMeisai1").scrollLeft = objDivBottom.scrollLeft;
    document.getElementById("divAllMeisai1").scrollLeft = objDivBottom.scrollLeft;
    document.getElementById("divAllNotFCMeisai1").scrollLeft = objDivBottom.scrollLeft;
    
    document.getElementById("divEigyouMeisai2").scrollLeft = objDivBottom.scrollLeft;
    document.getElementById("divSinkiMeisai2").scrollLeft = objDivBottom.scrollLeft;
    document.getElementById("divTokuhanMeisai2").scrollLeft = objDivBottom.scrollLeft;
    document.getElementById("divFCMeisai2").scrollLeft = objDivBottom.scrollLeft;
    document.getElementById("divAllMeisai2").scrollLeft = objDivBottom.scrollLeft;
    document.getElementById("divAllNotFCMeisai2").scrollLeft = objDivBottom.scrollLeft;
    
    document.getElementById("divEigyouMeisai3").scrollLeft = objDivBottom.scrollLeft;
    document.getElementById("divSinkiMeisai3").scrollLeft = objDivBottom.scrollLeft;
    document.getElementById("divTokuhanMeisai3").scrollLeft = objDivBottom.scrollLeft;
    document.getElementById("divFCMeisai3").scrollLeft = objDivBottom.scrollLeft;
    document.getElementById("divAllMeisai3").scrollLeft = objDivBottom.scrollLeft;
    document.getElementById("divAllNotFCMeisai3").scrollLeft = objDivBottom.scrollLeft;
    
    document.getElementById("divSum1").scrollLeft = objDivBottom.scrollLeft;
    document.getElementById("divSum2").scrollLeft = objDivBottom.scrollLeft;
    document.getElementById("divSum3").scrollLeft = objDivBottom.scrollLeft;
    document.getElementById("divSum4").scrollLeft = objDivBottom.scrollLeft;
    document.getElementById("divSum5").scrollLeft = objDivBottom.scrollLeft;
    document.getElementById("divSum6").scrollLeft = objDivBottom.scrollLeft;
}
</script>
<table>
    <tr>
        <td style="font-size:15px; font-weight: bold;">
            <asp:Label ID="lblNendo" Font-Size="18px" runat="server" Text="Label"></asp:Label>
        </td>
    </tr>
</table>
<table cellpadding="0" cellspacing="0" class="U009_table_border U009_td_font_bold" style="width:975px">
    <tr>
        <td rowspan="2" class="U009_td_border U009_td_backcolor1" style="width:50px;" align="center">ãÊï™</td>
        <td colspan="3" class="U009_td_border U009_td_backcolor2" style="width:342px;" align="center">ëOîNÉfÅ[É^</td>
        <td rowspan="2" class="U009_td_backcolor2">
        <div style="overflow:hidden;margin-top:-1px;width:559px" runat="server" id="divHead">
            <table cellpadding="0" cellspacing="0" class="U009_td_font_bold" width="1652px">
                <tr>
                    <td colspan="21" class="U009_td_border U009_td_backcolor4" align="center">ÅyèWåvè⁄ç◊ï\é¶Åz</td>
                </tr>
                <tr>
                    <td colspan="21" class="U009_td_border U009_td_backcolor3" align="center" >îÑè„</td>
                </tr>
                <tr>
                    <td colspan="3" class="U009_td_border U009_td_backcolor3" align="center" >ëOîNìØåé</td>
                    <td colspan="3" class="U009_td_border U009_td_backcolor3" align="center" >åvâÊ</td>
                    <td colspan="3" class="U009_td_border U009_td_backcolor3" align="center" >å©çû</td>
                    <td colspan="3" class="U009_td_border U009_td_backcolor3" align="center" >å©çû-åvâÊ</td>
                    <td colspan="3" class="U009_td_border U009_td_backcolor7 U009_td_font_color_white" align="center" >é¿ê—</td>
                    <td colspan="3" class="U009_td_border U009_td_backcolor8 U009_td_font_color_white" align="center" >åvâÊíBê¨ó¶</td>
                    <td colspan="3" class="U009_td_border U009_td_backcolor8 U009_td_font_color_white" align="center" >å©çûêiíªó¶</td>
                </tr>
                <tr>
                    <td class="U009_td_border U009_td_backcolor3" style="width:54px;" align="center">åèêî</td>
                    <td class="U009_td_border U009_td_backcolor3" style="width:104px;" align="center">ã‡äz</td>
                    <td class="U009_td_border U009_td_backcolor3" style="width:99px;" align="center">ëeóòäz</td>
                    <td class="U009_td_border U009_td_backcolor3" style="width:54px;" align="center">åèêî</td>
                    <td class="U009_td_border U009_td_backcolor3" style="width:104px;" align="center">ã‡äz</td>
                    <td class="U009_td_border U009_td_backcolor3" style="width:99px;" align="center">ëeóòäz</td>
                    <td class="U009_td_border U009_td_backcolor3" style="width:54px;" align="center">åèêî</td>
                    <td class="U009_td_border U009_td_backcolor3" style="width:104px;" align="center">ã‡äz</td>
                    <td class="U009_td_border U009_td_backcolor3" style="width:99px;" align="center">ëeóòäz</td>
                    <td class="U009_td_border U009_td_backcolor3" style="width:54px;" align="center">åèêî</td>
                    <td class="U009_td_border U009_td_backcolor3" style="width:104px;" align="center">ã‡äz</td>
                    <td class="U009_td_border U009_td_backcolor3" style="width:99px;" align="center">ëeóòäz</td>
                    <td class="U009_td_border U009_td_backcolor7 U009_td_font_color_white" style="width:54px;" align="center">åèêî</td>
                    <td class="U009_td_border U009_td_backcolor7 U009_td_font_color_white" style="width:104px;" align="center">ã‡äz</td>
                    <td class="U009_td_border U009_td_backcolor7 U009_td_font_color_white" style="width:99px;" align="center">ëeóòäz</td>
                    
                    <td class="U009_td_border U009_td_backcolor8 U009_td_font_color_white" style="width:54px;" align="center">åèêî</td>
                    <td class="U009_td_border U009_td_backcolor8 U009_td_font_color_white" style="width:54px;" align="center">ã‡äz</td>
                    <td class="U009_td_border U009_td_backcolor8 U009_td_font_color_white" style="width:54px;" align="center">ëeóòäz</td>
                    <td class="U009_td_border U009_td_backcolor8 U009_td_font_color_white" style="width:54px;" align="center">åèêî</td>
                    <td class="U009_td_border U009_td_backcolor8 U009_td_font_color_white" style="width:54px;" align="center">ã‡äz</td>
                    <td class="U009_td_border U009_td_backcolor8 U009_td_font_color_white" align="center">ëeóòäz</td>
                </tr>
            </table>
        
        </div>
        </td>
        
    </tr>
    <tr>
        <td class="U009_td_border U009_td_backcolor2" style="width:70px;"  align="center">çHéñî‰ó¶</td>
        <td class="U009_td_border U009_td_backcolor2" style="width:192px;" align="center">è§ïi</td>
        <td class="U009_td_border U009_td_backcolor2" style="width:94px;" align="center">ïΩãœíPâø</td>
    </tr>
</table>

<div class="Div_overflow_y" style="height:582px; margin-top:-1px;width:992px;" runat="server" id="divAllMeisai">
<table cellpadding="0" cellspacing="0" class="U009_table_border" style="width:975px">
    <tr>
        <td rowspan="4" class="U009_td_border U009_td_backcolor1 U009_td_font_bold" style="width:50px;">âcã∆</td>
        <td rowspan="2" class="U009_td_border U009_td_backcolor2" style="vertical-align:top;width:70px;">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr><td>çHéñîªíËó¶<br />
                <cc1:CommonDecimal ID="numKoj1_1" CssClass="U009_TextBox_border" RightFormat="%" Width="60px" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" Value="0" runat="server"></cc1:CommonDecimal><br /></td></tr>
                <tr><td style="border-bottom-style:solid; border-bottom-color:Black; border-bottom-width:2px;">&nbsp;</td></tr>
            </table>
            çHéñéÛíçó¶<br />
            <cc1:CommonDecimal ID="numKoj1_2" CssClass="U009_TextBox_border" RightFormat="%" Width="60px" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" Value="0" runat="server"></cc1:CommonDecimal></td>
        <td class="U009_td_border U009_td_backcolor2" style="width:38px;">í≤ç∏</td>
        <td class="U009_td_backcolor2" style="width:245px;">
            <asp:DataList ID="grdEigyouMeisai1" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="U009_td_border U009_td_backcolor2"><cc1:CommonText ID="txtSyouhinMei1_1" Text='<%#Eval("syouhin_mei")%>' Width="148px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" runat="server"></cc1:CommonText></td>
                        <td class="U009_td_border U009_td_backcolor2"><cc1:CommonDecimal ID="numTanka1_1" Value='<%#Eval("zennen_heikin_tanka")%>' Width="90px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" runat="server"></cc1:CommonDecimal></td>
                    </tr>
                </ItemTemplate>
            </asp:DataList>
        </td>
        <td class="U009_td_backcolor3">
            <div style="overflow:hidden;width:559px;" runat="server" id="divEigyouMeisai1">
                <asp:DataList ID="grdEigyouMeisai2" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_1" Value='<%#Eval("g_jittuseki_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_2" Value='<%#Eval("g_jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_3" Value='<%#Eval("g_jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_4" Value='<%#Eval("keikaku_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_5" Value='<%#Eval("keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_6" Value='<%#Eval("keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_7" Value='<%#Eval("mikomi_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_8" Value='<%#Eval("mikomi_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_9" Value='<%#Eval("mikomi_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber1" Value='<%#Eval("mikomi_keikaku_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber2" Value='<%#Eval("mikomi_keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber3" Value='<%#Eval("mikomi_keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_10" Value='<%#Eval("jittuseki_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_11" Value='<%#Eval("jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_12" Value='<%#Eval("jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_13" Value='<%#Eval("tasseiritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_14" Value='<%#Eval("tasseiritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_15" Value='<%#Eval("tasseiritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_16" Value='<%#Eval("sintyokuritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_17" Value='<%#Eval("sintyokuritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_18" Value='<%#Eval("sintyokuritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                    </tr>
                </ItemTemplate>
            </asp:DataList>
            </div>
        </td>
    </tr>
    <tr>
        <td class="U009_td_border U009_td_backcolor2">ÇªÇÃëº</td>
        <td class="U009_td_backcolor2" style="width:245px;">
            <asp:DataList ID="grdEigyouMeisai5" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="U009_td_border U009_td_backcolor2"><cc1:CommonText ID="txtSyouhinMei1_1" Text='<%#Eval("syouhin_mei")%>' Width="148px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" runat="server"></cc1:CommonText></td>
                        <td class="U009_td_border U009_td_backcolor2"><cc1:CommonDecimal ID="numTanka1_1" Value='<%#Eval("zennen_heikin_tanka")%>' Width="90px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" runat="server"></cc1:CommonDecimal></td>
                    </tr>
                </ItemTemplate>
            </asp:DataList>
        </td>
        <td class="U009_td_backcolor3">
            <div style="overflow:hidden;width:559px;" runat="server" id="divEigyouMeisai3">
                <asp:DataList ID="grdEigyouMeisai6" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_1" Value='<%#Eval("g_jittuseki_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_2" Value='<%#Eval("g_jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_3" Value='<%#Eval("g_jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_4" Value='<%#Eval("keikaku_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_5" Value='<%#Eval("keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_6" Value='<%#Eval("keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_7" Value='<%#Eval("mikomi_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_8" Value='<%#Eval("mikomi_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_9" Value='<%#Eval("mikomi_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber1" Value='<%#Eval("mikomi_keikaku_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber2" Value='<%#Eval("mikomi_keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber3" Value='<%#Eval("mikomi_keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_10" Value='<%#Eval("jittuseki_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_11" Value='<%#Eval("jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_12" Value='<%#Eval("jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_13" Value='<%#Eval("tasseiritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_14" Value='<%#Eval("tasseiritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_15" Value='<%#Eval("tasseiritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_16" Value='<%#Eval("sintyokuritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_17" Value='<%#Eval("sintyokuritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_18" Value='<%#Eval("sintyokuritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                    </tr>
                </ItemTemplate>
            </asp:DataList>
            </div>
        </td>
    </tr>
    
    <tr>
        <td class="U009_td_border U009_td_backcolor2" style="vertical-align:top;">
        íºçHéñó¶<br />
        <cc1:CommonDecimal ID="numKoj1_3" CssClass="U009_TextBox_border" RightFormat="%" Width="60px" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" Value="0" runat="server"></cc1:CommonDecimal></td>
        <td class="U009_td_border U009_td_backcolor2">çHéñ</td>
        <td class="U009_td_backcolor2">
            <asp:DataList ID="grdEigyouMeisai3" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="U009_td_border U009_td_backcolor2"><cc1:CommonText ID="txtSyouhinMei1_1" Text='<%#Eval("syouhin_mei")%>' Width="148px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" runat="server"></cc1:CommonText></td>
                        <td class="U009_td_border U009_td_backcolor2"><cc1:CommonDecimal ID="numTanka1_1" Value='<%#Eval("zennen_heikin_tanka")%>' Width="90px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" runat="server"></cc1:CommonDecimal></td>
                    </tr>
                </ItemTemplate>
            </asp:DataList>
        </td>
        <td class="U009_td_backcolor3">
            <div style="overflow:hidden;width:559px;" runat="server" id="divEigyouMeisai2">
                <asp:DataList ID="grdEigyouMeisai4" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_1" Value='<%#Eval("g_jittuseki_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_2" Value='<%#Eval("g_jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_3" Value='<%#Eval("g_jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_4" Value='<%#Eval("keikaku_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_5" Value='<%#Eval("keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_6" Value='<%#Eval("keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_7" Value='<%#Eval("mikomi_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_8" Value='<%#Eval("mikomi_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_9" Value='<%#Eval("mikomi_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber1" Value='<%#Eval("mikomi_keikaku_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber2" Value='<%#Eval("mikomi_keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber3" Value='<%#Eval("mikomi_keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_10" Value='<%#Eval("jittuseki_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_11" Value='<%#Eval("jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_12" Value='<%#Eval("jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_13" Value='<%#Eval("tasseiritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_14" Value='<%#Eval("tasseiritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_15" Value='<%#Eval("tasseiritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_16" Value='<%#Eval("sintyokuritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_17" Value='<%#Eval("sintyokuritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_18" Value='<%#Eval("sintyokuritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                    </tr>
                </ItemTemplate>
            </asp:DataList>
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="3" class="U009_td_border U009_td_backcolor2">&nbsp;</td>
        <td class="U009_td_backcolor3" style="border-bottom: black 2px solid;">
            <div style="overflow:hidden;margin-top:-1px;width:559px;" runat="server" id="divSum1">
                <asp:DataList ID="grdEigyouMeisaiSum" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="U009_td_backcolor6" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_1" Font-Bold="true" Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#847F83" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_2" Font-Bold="true" Value='<%#Eval("g_jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_3" Font-Bold="true" Value='<%#Eval("g_jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor6" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_4" Font-Bold="true" Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#847F83" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_5" Font-Bold="true" Value='<%#Eval("keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_6" Font-Bold="true" Value='<%#Eval("keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor6" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_7" Font-Bold="true" Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#847F83" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_8" Font-Bold="true" Value='<%#Eval("mikomi_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_9" Font-Bold="true" Value='<%#Eval("mikomi_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_backcolor6" style="border-right: black 2px solid;"><cc1:CommonNumber ID="CommonNumber4" Font-Bold="true" Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#847F83" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="CommonNumber5" Font-Bold="true" Value='<%#Eval("mikomi_keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="CommonNumber6" Font-Bold="true" Value='<%#Eval("mikomi_keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_backcolor6" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_10" Font-Bold="true" Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#847F83" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_11" Font-Bold="true" Value='<%#Eval("jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_12" Font-Bold="true" Value='<%#Eval("jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonDecimal ID="numUriage1_13" Font-Bold="true" Value='<%#Eval("tasseiritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonDecimal ID="numUriage1_14" Font-Bold="true" Value='<%#Eval("tasseiritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonDecimal ID="numUriage1_15" Font-Bold="true" Value='<%#Eval("tasseiritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonDecimal ID="numUriage1_16" Font-Bold="true" Value='<%#Eval("sintyokuritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonDecimal ID="numUriage1_17" Font-Bold="true" Value='<%#Eval("sintyokuritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonDecimal ID="numUriage1_18" Font-Bold="true" Value='<%#Eval("sintyokuritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonDecimal></td>
                    </tr>
                </ItemTemplate>
                </asp:DataList>
            </div>
        </td>
    </tr>
  
  <tr>
        <td rowspan="4" class="U009_td_border U009_td_backcolor1 U009_td_font_bold" style="width:50px;">êVãK</td>
        <td rowspan="2" class="U009_td_border U009_td_backcolor2" style="vertical-align:top;width:70px;">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr><td>çHéñîªíËó¶<br />
                <cc1:CommonDecimal ID="numKoj2_1" CssClass="U009_TextBox_border" RightFormat="%" Width="60px" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" Value="0" runat="server"></cc1:CommonDecimal><br /></td></tr>
                <tr><td style="border-bottom-style:solid; border-bottom-color:Black; border-bottom-width:2px;">&nbsp;</td></tr>
            </table>
            çHéñéÛíçó¶<br />
            <cc1:CommonDecimal ID="numKoj2_2" CssClass="U009_TextBox_border" RightFormat="%" Width="60px" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" Value="0" runat="server"></cc1:CommonDecimal></td>
        <td class="U009_td_border U009_td_backcolor2" style="width:28px;">í≤ç∏</td>
        <td class="U009_td_backcolor2" style="width:245px;">
            <asp:DataList ID="grdSinkiMeisai1" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="U009_td_border U009_td_backcolor2"><cc1:CommonText ID="txtSyouhinMei1_1" Text='<%#Eval("syouhin_mei")%>' Width="148px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" runat="server"></cc1:CommonText></td>
                        <td class="U009_td_border U009_td_backcolor2"><cc1:CommonDecimal ID="numTanka1_1" Value='<%#Eval("zennen_heikin_tanka")%>' Width="90px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" runat="server"></cc1:CommonDecimal></td>
                    </tr>
                </ItemTemplate>
            </asp:DataList>
        </td>
        <td class="U009_td_backcolor4">
            <div style="overflow:hidden;width:559px;" runat="server" id="divSinkiMeisai1">
                <asp:DataList ID="grdSinkiMeisai2" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="U009_td_border U009_td_backcolor4"><cc1:CommonNumber ID="numUriage1_1" Value='<%#Eval("g_jittuseki_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C0C0C0" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor4"><cc1:CommonNumber ID="numUriage1_2" Value='<%#Eval("g_jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C0C0C0" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor4"><cc1:CommonNumber ID="numUriage1_3" Value='<%#Eval("g_jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C0C0C0" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_4" Value='<%#Eval("keikaku_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_5" Value='<%#Eval("keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_6" Value='<%#Eval("keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_7" Value='<%#Eval("mikomi_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_8" Value='<%#Eval("mikomi_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_9" Value='<%#Eval("mikomi_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber1" Value='<%#Eval("mikomi_keikaku_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber2" Value='<%#Eval("mikomi_keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber3" Value='<%#Eval("mikomi_keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_10" Value='<%#Eval("jittuseki_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_11" Value='<%#Eval("jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_12" Value='<%#Eval("jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_13" Value='<%#Eval("tasseiritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_14" Value='<%#Eval("tasseiritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_15" Value='<%#Eval("tasseiritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_16" Value='<%#Eval("sintyokuritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_17" Value='<%#Eval("sintyokuritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_18" Value='<%#Eval("sintyokuritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                    </tr>
                </ItemTemplate>
            </asp:DataList>
            </div>
        </td>
    </tr>
    <tr>
        <td class="U009_td_border U009_td_backcolor2">ÇªÇÃëº</td>
        <td class="U009_td_backcolor2" style="width:245px;">
            <asp:DataList ID="grdSinkiMeisai5" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="U009_td_border U009_td_backcolor2"><cc1:CommonText ID="txtSyouhinMei1_1" Text='<%#Eval("syouhin_mei")%>' Width="148px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" runat="server"></cc1:CommonText></td>
                        <td class="U009_td_border U009_td_backcolor2"><cc1:CommonDecimal ID="numTanka1_1" Value='<%#Eval("zennen_heikin_tanka")%>' Width="90px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" runat="server"></cc1:CommonDecimal></td>
                    </tr>
                </ItemTemplate>
            </asp:DataList>
        </td>
        <td class="U009_td_backcolor4">
            <div style="overflow:hidden;width:559px;" runat="server" id="divSinkiMeisai3">
                <asp:DataList ID="grdSinkiMeisai6" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="U009_td_border U009_td_backcolor4"><cc1:CommonNumber ID="numUriage1_1" Value='<%#Eval("g_jittuseki_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C0C0C0" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor4"><cc1:CommonNumber ID="numUriage1_2" Value='<%#Eval("g_jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C0C0C0" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor4"><cc1:CommonNumber ID="numUriage1_3" Value='<%#Eval("g_jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C0C0C0" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_4" Value='<%#Eval("keikaku_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_5" Value='<%#Eval("keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_6" Value='<%#Eval("keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_7" Value='<%#Eval("mikomi_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_8" Value='<%#Eval("mikomi_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_9" Value='<%#Eval("mikomi_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber1" Value='<%#Eval("mikomi_keikaku_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber2" Value='<%#Eval("mikomi_keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber3" Value='<%#Eval("mikomi_keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_10" Value='<%#Eval("jittuseki_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_11" Value='<%#Eval("jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_12" Value='<%#Eval("jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_13" Value='<%#Eval("tasseiritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_14" Value='<%#Eval("tasseiritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_15" Value='<%#Eval("tasseiritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_16" Value='<%#Eval("sintyokuritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_17" Value='<%#Eval("sintyokuritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_18" Value='<%#Eval("sintyokuritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                    </tr>
                </ItemTemplate>
            </asp:DataList>
            </div>
        </td>
    </tr>
    
    <tr>
        <td class="U009_td_border U009_td_backcolor2" style="vertical-align:top;">
        íºçHéñó¶<br />
        <cc1:CommonDecimal ID="numKoj2_3" CssClass="U009_TextBox_border" RightFormat="%" Width="60px" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" Value="0" runat="server"></cc1:CommonDecimal></td>
        <td class="U009_td_border U009_td_backcolor2">çHéñ</td>
        <td class="U009_td_backcolor2">
            <asp:DataList ID="grdSinkiMeisai3" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="U009_td_border U009_td_backcolor2"><cc1:CommonText ID="txtSyouhinMei1_1" Text='<%#Eval("syouhin_mei")%>' Width="148px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" runat="server"></cc1:CommonText></td>
                        <td class="U009_td_border U009_td_backcolor2"><cc1:CommonDecimal ID="numTanka1_1" Value='<%#Eval("zennen_heikin_tanka")%>' Width="90px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" runat="server"></cc1:CommonDecimal></td>
                    </tr>
                </ItemTemplate>
            </asp:DataList>
        </td>
        <td class="U009_td_backcolor4">
            <div style="overflow:hidden;width:559px;" runat="server" id="divSinkiMeisai2">
                <asp:DataList ID="grdSinkiMeisai4" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="U009_td_border U009_td_backcolor4"><cc1:CommonNumber ID="numUriage1_1" Value='<%#Eval("g_jittuseki_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C0C0C0" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor4"><cc1:CommonNumber ID="numUriage1_2" Value='<%#Eval("g_jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C0C0C0" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor4"><cc1:CommonNumber ID="numUriage1_3" Value='<%#Eval("g_jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C0C0C0" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_4" Value='<%#Eval("keikaku_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_5" Value='<%#Eval("keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_6" Value='<%#Eval("keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_7" Value='<%#Eval("mikomi_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_8" Value='<%#Eval("mikomi_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_9" Value='<%#Eval("mikomi_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber1" Value='<%#Eval("mikomi_keikaku_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber2" Value='<%#Eval("mikomi_keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber3" Value='<%#Eval("mikomi_keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_10" Value='<%#Eval("jittuseki_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_11" Value='<%#Eval("jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_12" Value='<%#Eval("jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_13" Value='<%#Eval("tasseiritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_14" Value='<%#Eval("tasseiritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_15" Value='<%#Eval("tasseiritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_16" Value='<%#Eval("sintyokuritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_17" Value='<%#Eval("sintyokuritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_18" Value='<%#Eval("sintyokuritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                    </tr>
                </ItemTemplate>
            </asp:DataList>
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="3" class="U009_td_border U009_td_backcolor2">&nbsp;</td>
        <td class="U009_td_backcolor3" style="border-bottom: black 2px solid;">
            <div style="overflow:hidden;margin-top:-1px;width:559px;" runat="server" id="divSum2">
                <asp:DataList ID="grdSinkiMeisaiSum" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="U009_td_backcolor6" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_1" Font-Bold="true" Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#847F83" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor6" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_2" Font-Bold="true" Value='<%#Eval("g_jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#847F83" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor6" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_3" Font-Bold="true" Value='<%#Eval("g_jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#847F83" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor6" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_4" Font-Bold="true" Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#847F83" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_5" Font-Bold="true" Value='<%#Eval("keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_6" Font-Bold="true" Value='<%#Eval("keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor6" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_7" Font-Bold="true" Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#847F83" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_8" Font-Bold="true" Value='<%#Eval("mikomi_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_9" Font-Bold="true" Value='<%#Eval("mikomi_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_backcolor6" style="border-right: black 2px solid;"><cc1:CommonNumber ID="CommonNumber4" Font-Bold="true" Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#847F83" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="CommonNumber5" Font-Bold="true" Value='<%#Eval("mikomi_keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="CommonNumber6" Font-Bold="true" Value='<%#Eval("mikomi_keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_backcolor6" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_10" Font-Bold="true" Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#847F83" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_11" Font-Bold="true" Value='<%#Eval("jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_12" Font-Bold="true" Value='<%#Eval("jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonDecimal ID="numUriage1_13" Font-Bold="true" Value='<%#Eval("tasseiritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonDecimal ID="numUriage1_14" Font-Bold="true" Value='<%#Eval("tasseiritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonDecimal ID="numUriage1_15" Font-Bold="true" Value='<%#Eval("tasseiritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonDecimal ID="numUriage1_16" Font-Bold="true" Value='<%#Eval("sintyokuritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonDecimal ID="numUriage1_17" Font-Bold="true" Value='<%#Eval("sintyokuritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonDecimal ID="numUriage1_18" Font-Bold="true" Value='<%#Eval("sintyokuritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonDecimal></td>
                    </tr>
                </ItemTemplate>
                </asp:DataList>
            </div>
        </td>
    </tr>
    
    <tr>
        <td rowspan="4" class="U009_td_border U009_td_backcolor1 U009_td_font_bold" style="width:50px;">ì¡îÃ</td>
        <td rowspan="2" class="U009_td_border U009_td_backcolor2" style="vertical-align:top;width:70px;">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr><td>çHéñîªíËó¶<br />
                <cc1:CommonDecimal ID="numKoj3_1" CssClass="U009_TextBox_border" RightFormat="%" Width="60px" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" Value="0" runat="server"></cc1:CommonDecimal><br /></td></tr>
                <tr><td style="border-bottom-style:solid; border-bottom-color:Black; border-bottom-width:2px;">&nbsp;</td></tr>
            </table>
            çHéñéÛíçó¶<br />
            <cc1:CommonDecimal ID="numKoj3_2" CssClass="U009_TextBox_border" RightFormat="%" Width="60px" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" Value="0" runat="server"></cc1:CommonDecimal></td>
        <td class="U009_td_border U009_td_backcolor2" style="width:28px;">í≤ç∏</td>
        <td class="U009_td_backcolor2" style="width:245px;">
            <asp:DataList ID="grdTokuhanMeisai1" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="U009_td_border U009_td_backcolor2"><cc1:CommonText ID="txtSyouhinMei1_1" Text='<%#Eval("syouhin_mei")%>' Width="148px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" runat="server"></cc1:CommonText></td>
                        <td class="U009_td_border U009_td_backcolor2"><cc1:CommonDecimal ID="numTanka1_1" Value='<%#Eval("zennen_heikin_tanka")%>' Width="90px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" runat="server"></cc1:CommonDecimal></td>
                    </tr>
                </ItemTemplate>
            </asp:DataList>
        </td>
        <td class="U009_td_backcolor3">
            <div style="overflow:hidden;width:559px;" runat="server" id="divTokuhanMeisai1">
                <asp:DataList ID="grdTokuhanMeisai2" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_1" Value='<%#Eval("g_jittuseki_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_2" Value='<%#Eval("g_jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_3" Value='<%#Eval("g_jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_4" Value='<%#Eval("keikaku_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_5" Value='<%#Eval("keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_6" Value='<%#Eval("keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_7" Value='<%#Eval("mikomi_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_8" Value='<%#Eval("mikomi_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_9" Value='<%#Eval("mikomi_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber1" Value='<%#Eval("mikomi_keikaku_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber2" Value='<%#Eval("mikomi_keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber3" Value='<%#Eval("mikomi_keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_10" Value='<%#Eval("jittuseki_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_11" Value='<%#Eval("jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_12" Value='<%#Eval("jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_13" Value='<%#Eval("tasseiritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_14" Value='<%#Eval("tasseiritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_15" Value='<%#Eval("tasseiritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_16" Value='<%#Eval("sintyokuritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_17" Value='<%#Eval("sintyokuritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_18" Value='<%#Eval("sintyokuritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                    </tr>
                </ItemTemplate>
            </asp:DataList>
            </div>
        </td>
    </tr>
    <tr>
        <td class="U009_td_border U009_td_backcolor2">ÇªÇÃëº</td>
        <td class="U009_td_backcolor2" style="width:245px;">
            <asp:DataList ID="grdTokuhanMeisai5" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="U009_td_border U009_td_backcolor2"><cc1:CommonText ID="txtSyouhinMei1_1" Text='<%#Eval("syouhin_mei")%>' Width="148px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" runat="server"></cc1:CommonText></td>
                        <td class="U009_td_border U009_td_backcolor2"><cc1:CommonDecimal ID="numTanka1_1" Value='<%#Eval("zennen_heikin_tanka")%>' Width="90px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" runat="server"></cc1:CommonDecimal></td>
                    </tr>
                </ItemTemplate>
            </asp:DataList>
        </td>
        <td class="U009_td_backcolor3">
            <div style="overflow:hidden;width:559px;" runat="server" id="divTokuhanMeisai3">
                <asp:DataList ID="grdTokuhanMeisai6" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_1" Value='<%#Eval("g_jittuseki_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_2" Value='<%#Eval("g_jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_3" Value='<%#Eval("g_jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_4" Value='<%#Eval("keikaku_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_5" Value='<%#Eval("keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_6" Value='<%#Eval("keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_7" Value='<%#Eval("mikomi_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_8" Value='<%#Eval("mikomi_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_9" Value='<%#Eval("mikomi_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber1" Value='<%#Eval("mikomi_keikaku_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber2" Value='<%#Eval("mikomi_keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber3" Value='<%#Eval("mikomi_keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_10" Value='<%#Eval("jittuseki_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_11" Value='<%#Eval("jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_12" Value='<%#Eval("jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_13" Value='<%#Eval("tasseiritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_14" Value='<%#Eval("tasseiritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_15" Value='<%#Eval("tasseiritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_16" Value='<%#Eval("sintyokuritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_17" Value='<%#Eval("sintyokuritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_18" Value='<%#Eval("sintyokuritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                    </tr>
                </ItemTemplate>
            </asp:DataList>
            </div>
        </td>
    </tr>
    
    <tr>
        <td class="U009_td_border U009_td_backcolor2" style="vertical-align:top;">
        íºçHéñó¶<br />
        <cc1:CommonDecimal ID="numKoj3_3" CssClass="U009_TextBox_border" RightFormat="%" Width="60px" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" Value="0" runat="server"></cc1:CommonDecimal></td>
        <td class="U009_td_border U009_td_backcolor2">çHéñ</td>
        <td class="U009_td_backcolor2">
            <asp:DataList ID="grdTokuhanMeisai3" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="U009_td_border U009_td_backcolor2"><cc1:CommonText ID="txtSyouhinMei1_1" Text='<%#Eval("syouhin_mei")%>' Width="148px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" runat="server"></cc1:CommonText></td>
                        <td class="U009_td_border U009_td_backcolor2"><cc1:CommonDecimal ID="numTanka1_1" Value='<%#Eval("zennen_heikin_tanka")%>' Width="90px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" runat="server"></cc1:CommonDecimal></td>
                    </tr>
                </ItemTemplate>
            </asp:DataList>
        </td>
        <td class="U009_td_backcolor3">
            <div style="overflow:hidden;width:559px;" runat="server" id="divTokuhanMeisai2">
                <asp:DataList ID="grdTokuhanMeisai4" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_1" Value='<%#Eval("g_jittuseki_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_2" Value='<%#Eval("g_jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_3" Value='<%#Eval("g_jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_4" Value='<%#Eval("keikaku_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_5" Value='<%#Eval("keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_6" Value='<%#Eval("keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_7" Value='<%#Eval("mikomi_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_8" Value='<%#Eval("mikomi_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_9" Value='<%#Eval("mikomi_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber1" Value='<%#Eval("mikomi_keikaku_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber2" Value='<%#Eval("mikomi_keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber3" Value='<%#Eval("mikomi_keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_10" Value='<%#Eval("jittuseki_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_11" Value='<%#Eval("jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_12" Value='<%#Eval("jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_13" Value='<%#Eval("tasseiritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_14" Value='<%#Eval("tasseiritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_15" Value='<%#Eval("tasseiritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_16" Value='<%#Eval("sintyokuritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_17" Value='<%#Eval("sintyokuritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_18" Value='<%#Eval("sintyokuritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                    </tr>
                </ItemTemplate>
            </asp:DataList>
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="3" class="U009_td_border U009_td_backcolor2">&nbsp;</td>
        <td class="U009_td_backcolor3" style="border-bottom: black 2px solid;">
            <div style="overflow:hidden;margin-top:-1px;width:559px;" runat="server" id="divSum3">
                <asp:DataList ID="grdTokuhanMeisaiSum" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="U009_td_backcolor6" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_1" Font-Bold="true" Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#847F83" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_2" Font-Bold="true" Value='<%#Eval("g_jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_3" Font-Bold="true" Value='<%#Eval("g_jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor6" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_4" Font-Bold="true" Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#847F83" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_5" Font-Bold="true" Value='<%#Eval("keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_6" Font-Bold="true" Value='<%#Eval("keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor6" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_7" Font-Bold="true" Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#847F83" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_8" Font-Bold="true" Value='<%#Eval("mikomi_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_9" Font-Bold="true" Value='<%#Eval("mikomi_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_backcolor6" style="border-right: black 2px solid;"><cc1:CommonNumber ID="CommonNumber4" Font-Bold="true" Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#847F83" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="CommonNumber5" Font-Bold="true" Value='<%#Eval("mikomi_keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="CommonNumber6" Font-Bold="true" Value='<%#Eval("mikomi_keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_backcolor6" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_10" Font-Bold="true" Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#847F83" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_11" Font-Bold="true" Value='<%#Eval("jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_12" Font-Bold="true" Value='<%#Eval("jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonDecimal ID="numUriage1_13" Font-Bold="true" Value='<%#Eval("tasseiritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonDecimal ID="numUriage1_14" Font-Bold="true" Value='<%#Eval("tasseiritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonDecimal ID="numUriage1_15" Font-Bold="true" Value='<%#Eval("tasseiritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonDecimal ID="numUriage1_16" Font-Bold="true" Value='<%#Eval("sintyokuritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonDecimal ID="numUriage1_17" Font-Bold="true" Value='<%#Eval("sintyokuritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonDecimal ID="numUriage1_18" Font-Bold="true" Value='<%#Eval("sintyokuritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonDecimal></td>
                    </tr>
                </ItemTemplate>
                </asp:DataList>
            </div>
        </td>
    </tr>
    
    <tr>
        <td rowspan="4" class="U009_td_border U009_td_backcolor1 U009_td_font_bold" style="width:50px;">ÇeÇb</td>
        <td rowspan="2" class="U009_td_border U009_td_backcolor2" style="vertical-align:top;width:70px;">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr><td>çHéñîªíËó¶<br />
                <cc1:CommonDecimal ID="numKoj4_1" CssClass="U009_TextBox_border" RightFormat="%" Width="60px" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" Value="0" runat="server"></cc1:CommonDecimal><br /></td></tr>
                <tr><td style="border-bottom-style:solid; border-bottom-color:Black; border-bottom-width:2px;">&nbsp;</td></tr>
            </table>
            çHéñéÛíçó¶<br />
            <cc1:CommonDecimal ID="numKoj4_2" CssClass="U009_TextBox_border" RightFormat="%" Width="60px" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" Value="0" runat="server"></cc1:CommonDecimal></td>
        <td class="U009_td_border U009_td_backcolor2" style="width:28px;">í≤ç∏</td>
        <td class="U009_td_backcolor2" style="width:245px;">
            <asp:DataList ID="grdFCMeisai1" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="U009_td_border U009_td_backcolor2"><cc1:CommonText ID="txtSyouhinMei1_1" Text='<%#Eval("syouhin_mei")%>' Width="148px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" runat="server"></cc1:CommonText></td>
                        <td class="U009_td_border U009_td_backcolor2"><cc1:CommonDecimal ID="numTanka1_1" Value='<%#Eval("zennen_heikin_tanka")%>' Width="90px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" runat="server"></cc1:CommonDecimal></td>
                    </tr>
                </ItemTemplate>
            </asp:DataList>
        </td>
        <td class="U009_td_backcolor4">
            <div style="overflow:hidden;width:559px;" runat="server" id="divFCMeisai1">
                <asp:DataList ID="grdFCMeisai2" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="U009_td_border U009_td_backcolor4"><cc1:CommonNumber ID="numUriage1_1" Value='<%#Eval("g_jittuseki_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C0C0C0" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor4"><cc1:CommonNumber ID="numUriage1_2" Value='<%#Eval("g_jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C0C0C0" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor4"><cc1:CommonNumber ID="numUriage1_3" Value='<%#Eval("g_jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C0C0C0" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_4" Value='<%#Eval("keikaku_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_5" Value='<%#Eval("keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_6" Value='<%#Eval("keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_7" Value='<%#Eval("mikomi_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_8" Value='<%#Eval("mikomi_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_9" Value='<%#Eval("mikomi_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber1" Value='<%#Eval("mikomi_keikaku_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber2" Value='<%#Eval("mikomi_keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber3" Value='<%#Eval("mikomi_keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_10" Value='<%#Eval("jittuseki_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_11" Value='<%#Eval("jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_12" Value='<%#Eval("jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_13" Value='<%#Eval("tasseiritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_14" Value='<%#Eval("tasseiritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_15" Value='<%#Eval("tasseiritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_16" Value='<%#Eval("sintyokuritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_17" Value='<%#Eval("sintyokuritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_18" Value='<%#Eval("sintyokuritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                    </tr>
                </ItemTemplate>
            </asp:DataList>
            </div>
        </td>
    </tr>
    <tr>
        <td class="U009_td_border U009_td_backcolor2">ÇªÇÃëº</td>
        <td class="U009_td_backcolor2" style="width:245px;">
            <asp:DataList ID="grdFCMeisai5" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="U009_td_border U009_td_backcolor2"><cc1:CommonText ID="txtSyouhinMei1_1" Text='<%#Eval("syouhin_mei")%>' Width="148px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" runat="server"></cc1:CommonText></td>
                        <td class="U009_td_border U009_td_backcolor2"><cc1:CommonDecimal ID="numTanka1_1" Value='<%#Eval("zennen_heikin_tanka")%>' Width="90px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" runat="server"></cc1:CommonDecimal></td>
                    </tr>
                </ItemTemplate>
            </asp:DataList>
        </td>
        <td class="U009_td_backcolor4">
            <div style="overflow:hidden;width:559px;" runat="server" id="divFCMeisai3">
                <asp:DataList ID="grdFCMeisai6" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="U009_td_border U009_td_backcolor4"><cc1:CommonNumber ID="numUriage1_1" Value='<%#Eval("g_jittuseki_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C0C0C0" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor4"><cc1:CommonNumber ID="numUriage1_2" Value='<%#Eval("g_jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C0C0C0" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor4"><cc1:CommonNumber ID="numUriage1_3" Value='<%#Eval("g_jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C0C0C0" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_4" Value='<%#Eval("keikaku_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_5" Value='<%#Eval("keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_6" Value='<%#Eval("keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_7" Value='<%#Eval("mikomi_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_8" Value='<%#Eval("mikomi_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_9" Value='<%#Eval("mikomi_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber1" Value='<%#Eval("mikomi_keikaku_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber2" Value='<%#Eval("mikomi_keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber3" Value='<%#Eval("mikomi_keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_10" Value='<%#Eval("jittuseki_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_11" Value='<%#Eval("jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_12" Value='<%#Eval("jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_13" Value='<%#Eval("tasseiritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_14" Value='<%#Eval("tasseiritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_15" Value='<%#Eval("tasseiritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_16" Value='<%#Eval("sintyokuritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_17" Value='<%#Eval("sintyokuritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_18" Value='<%#Eval("sintyokuritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                    </tr>
                </ItemTemplate>
            </asp:DataList>
            </div>
        </td>
    </tr>
    
    <tr>
        <td class="U009_td_border U009_td_backcolor2" style="vertical-align:top;">
        íºçHéñó¶<br />
        <cc1:CommonDecimal ID="numKoj4_3" CssClass="U009_TextBox_border" RightFormat="%" Width="60px" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" Value="0" runat="server"></cc1:CommonDecimal></td>
        <td class="U009_td_border U009_td_backcolor2">çHéñ</td>
        <td class="U009_td_backcolor2">
            <asp:DataList ID="grdFCMeisai3" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="U009_td_border U009_td_backcolor2"><cc1:CommonText ID="txtSyouhinMei1_1" Text='<%#Eval("syouhin_mei")%>' Width="148px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" runat="server"></cc1:CommonText></td>
                        <td class="U009_td_border U009_td_backcolor2"><cc1:CommonDecimal ID="numTanka1_1" Value='<%#Eval("zennen_heikin_tanka")%>' Width="90px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" runat="server"></cc1:CommonDecimal></td>
                    </tr>
                </ItemTemplate>
            </asp:DataList>
        </td>
        <td class="U009_td_backcolor4">
            <div style="overflow:hidden;width:559px;" runat="server" id="divFCMeisai2">
                <asp:DataList ID="grdFCMeisai4" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="U009_td_border U009_td_backcolor4"><cc1:CommonNumber ID="numUriage1_1" Value='<%#Eval("g_jittuseki_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C0C0C0" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor4"><cc1:CommonNumber ID="numUriage1_2" Value='<%#Eval("g_jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C0C0C0" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor4"><cc1:CommonNumber ID="numUriage1_3" Value='<%#Eval("g_jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C0C0C0" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_4" Value='<%#Eval("keikaku_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_5" Value='<%#Eval("keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_6" Value='<%#Eval("keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_7" Value='<%#Eval("mikomi_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_8" Value='<%#Eval("mikomi_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_9" Value='<%#Eval("mikomi_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber1" Value='<%#Eval("mikomi_keikaku_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber2" Value='<%#Eval("mikomi_keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber3" Value='<%#Eval("mikomi_keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_10" Value='<%#Eval("jittuseki_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_11" Value='<%#Eval("jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_12" Value='<%#Eval("jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_13" Value='<%#Eval("tasseiritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_14" Value='<%#Eval("tasseiritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_15" Value='<%#Eval("tasseiritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_16" Value='<%#Eval("sintyokuritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_17" Value='<%#Eval("sintyokuritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_18" Value='<%#Eval("sintyokuritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                    </tr>
                </ItemTemplate>
            </asp:DataList>
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="3" class="U009_td_border U009_td_backcolor2">&nbsp;</td>
        <td class="U009_td_backcolor3" style="border-bottom: black 2px solid;">
            <div style="overflow:hidden;margin-top:-1px;width:559px;" runat="server" id="divSum4">
                <asp:DataList ID="grdFCMeisaiSum" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="U009_td_backcolor6" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_1" Font-Bold="true" Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#847F83" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor6" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_2" Font-Bold="true" Value='<%#Eval("g_jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#847F83" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor6" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_3" Font-Bold="true" Value='<%#Eval("g_jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#847F83" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor6" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_4" Font-Bold="true" Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#847F83" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_5" Font-Bold="true" Value='<%#Eval("keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_6" Font-Bold="true" Value='<%#Eval("keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor6" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_7" Font-Bold="true" Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#847F83" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_8" Font-Bold="true" Value='<%#Eval("mikomi_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_9" Font-Bold="true" Value='<%#Eval("mikomi_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_backcolor6" style="border-right: black 2px solid;"><cc1:CommonNumber ID="CommonNumber4" Font-Bold="true" Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#847F83" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="CommonNumber5" Font-Bold="true" Value='<%#Eval("mikomi_keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="CommonNumber6" Font-Bold="true" Value='<%#Eval("mikomi_keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_backcolor6" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_10" Font-Bold="true" Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#847F83" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_11" Font-Bold="true" Value='<%#Eval("jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_12" Font-Bold="true" Value='<%#Eval("jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonDecimal ID="numUriage1_13" Font-Bold="true" Value='<%#Eval("tasseiritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonDecimal ID="numUriage1_14" Font-Bold="true" Value='<%#Eval("tasseiritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonDecimal ID="numUriage1_15" Font-Bold="true" Value='<%#Eval("tasseiritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonDecimal ID="numUriage1_16" Font-Bold="true" Value='<%#Eval("sintyokuritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonDecimal ID="numUriage1_17" Font-Bold="true" Value='<%#Eval("sintyokuritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonDecimal ID="numUriage1_18" Font-Bold="true" Value='<%#Eval("sintyokuritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonDecimal></td>
                    </tr>
                </ItemTemplate>
                </asp:DataList>
            </div>
        </td>
    </tr>
</table>







<table cellpadding="10"><tr><td></td></tr></table>
<table cellpadding="0" cellspacing="0"  class="U009_table_border" style="width:975px">
    <tr>
        <td rowspan="4" class="U009_td_border U009_td_backcolor1 U009_td_font_bold" style="width:50px;">ëSëÃ</td>
        <td rowspan="2" class="U009_td_border U009_td_backcolor2" style="vertical-align:top;width:70px;">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr><td>çHéñîªíËó¶<br />
                <cc1:CommonDecimal ID="numKoj5_1" CssClass="U009_TextBox_border" RightFormat="%" Width="60px" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" Value="0" runat="server"></cc1:CommonDecimal><br /></td></tr>
                <tr><td style="border-bottom-style:solid; border-bottom-color:Black; border-bottom-width:2px;">&nbsp;</td></tr>
            </table>
            çHéñéÛíçó¶<br />
            <cc1:CommonDecimal ID="numKoj5_2" CssClass="U009_TextBox_border" RightFormat="%" Width="60px" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" Value="0" runat="server"></cc1:CommonDecimal></td>
        <td class="U009_td_border U009_td_backcolor2" style="width:28px;">í≤ç∏</td>
        <td class="U009_td_backcolor2" style="width:245px;">
            <asp:DataList ID="grdAllMeisai1" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="U009_td_border U009_td_backcolor2"><cc1:CommonText ID="txtSyouhinMei1_1" Text='<%#Eval("syouhin_mei")%>' Width="148px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" runat="server"></cc1:CommonText></td>
                        <td class="U009_td_border U009_td_backcolor2"><cc1:CommonDecimal ID="numTanka1_1" Value='<%#Eval("zennen_heikin_tanka")%>' Width="90px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" runat="server"></cc1:CommonDecimal></td>
                    </tr>
                </ItemTemplate>
            </asp:DataList>
        </td>
        <td class="U009_td_backcolor3">
            <div style="overflow:hidden;width:559px;" runat="server" id="divAllMeisai1">
                <asp:DataList ID="grdAllMeisai2" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_1" Value='<%#Eval("g_jittuseki_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_2" Value='<%#Eval("g_jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_3" Value='<%#Eval("g_jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_4" Value='<%#Eval("keikaku_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_5" Value='<%#Eval("keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_6" Value='<%#Eval("keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_7" Value='<%#Eval("mikomi_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_8" Value='<%#Eval("mikomi_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_9" Value='<%#Eval("mikomi_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber1" Value='<%#Eval("mikomi_keikaku_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber2" Value='<%#Eval("mikomi_keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber3" Value='<%#Eval("mikomi_keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_10" Value='<%#Eval("jittuseki_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_11" Value='<%#Eval("jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_12" Value='<%#Eval("jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_13" Value='<%#Eval("tasseiritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_14" Value='<%#Eval("tasseiritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_15" Value='<%#Eval("tasseiritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_16" Value='<%#Eval("sintyokuritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_17" Value='<%#Eval("sintyokuritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_18" Value='<%#Eval("sintyokuritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                    </tr>
                </ItemTemplate>
            </asp:DataList>
            </div>
        </td>
    </tr>
    <tr>
        <td class="U009_td_border U009_td_backcolor2">ÇªÇÃëº</td>
        <td class="U009_td_backcolor2" style="width:245px;">
            <asp:DataList ID="grdAllMeisai5" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="U009_td_border U009_td_backcolor2"><cc1:CommonText ID="txtSyouhinMei1_1" Text='<%#Eval("syouhin_mei")%>' Width="148px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" runat="server"></cc1:CommonText></td>
                        <td class="U009_td_border U009_td_backcolor2"><cc1:CommonDecimal ID="numTanka1_1" Value='<%#Eval("zennen_heikin_tanka")%>' Width="90px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" runat="server"></cc1:CommonDecimal></td>
                    </tr>
                </ItemTemplate>
            </asp:DataList>
        </td>
        <td class="U009_td_backcolor3">
            <div style="overflow:hidden;width:559px;" runat="server" id="divAllMeisai3">
                <asp:DataList ID="grdAllMeisai6" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_1" Value='<%#Eval("g_jittuseki_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_2" Value='<%#Eval("g_jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_3" Value='<%#Eval("g_jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_4" Value='<%#Eval("keikaku_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_5" Value='<%#Eval("keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_6" Value='<%#Eval("keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_7" Value='<%#Eval("mikomi_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_8" Value='<%#Eval("mikomi_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_9" Value='<%#Eval("mikomi_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber1" Value='<%#Eval("mikomi_keikaku_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber2" Value='<%#Eval("mikomi_keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber3" Value='<%#Eval("mikomi_keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_10" Value='<%#Eval("jittuseki_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_11" Value='<%#Eval("jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_12" Value='<%#Eval("jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_13" Value='<%#Eval("tasseiritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_14" Value='<%#Eval("tasseiritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_15" Value='<%#Eval("tasseiritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_16" Value='<%#Eval("sintyokuritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_17" Value='<%#Eval("sintyokuritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_18" Value='<%#Eval("sintyokuritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                    </tr>
                </ItemTemplate>
            </asp:DataList>
            </div>
        </td>
    </tr>
    
    <tr>
        <td class="U009_td_border U009_td_backcolor2" style="vertical-align:top;">
        íºçHéñó¶<br />
        <cc1:CommonDecimal ID="numKoj5_3" CssClass="U009_TextBox_border" RightFormat="%" Width="60px" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" Value="0" runat="server"></cc1:CommonDecimal></td>
        <td class="U009_td_border U009_td_backcolor2">çHéñ</td>
        <td class="U009_td_backcolor2">
            <asp:DataList ID="grdAllMeisai3" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="U009_td_border U009_td_backcolor2"><cc1:CommonText ID="txtSyouhinMei1_1" Text='<%#Eval("syouhin_mei")%>' Width="148px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" runat="server"></cc1:CommonText></td>
                        <td class="U009_td_border U009_td_backcolor2"><cc1:CommonDecimal ID="numTanka1_1" Value='<%#Eval("zennen_heikin_tanka")%>' Width="90px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" runat="server"></cc1:CommonDecimal></td>
                    </tr>
                </ItemTemplate>
            </asp:DataList>
        </td>
        <td class="U009_td_backcolor3">
            <div style="overflow:hidden;width:559px;" runat="server" id="divAllMeisai2">
                <asp:DataList ID="grdAllMeisai4" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_1" Value='<%#Eval("g_jittuseki_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_2" Value='<%#Eval("g_jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_3" Value='<%#Eval("g_jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_4" Value='<%#Eval("keikaku_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_5" Value='<%#Eval("keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_6" Value='<%#Eval("keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_7" Value='<%#Eval("mikomi_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_8" Value='<%#Eval("mikomi_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_9" Value='<%#Eval("mikomi_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber1" Value='<%#Eval("mikomi_keikaku_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber2" Value='<%#Eval("mikomi_keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber3" Value='<%#Eval("mikomi_keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_10" Value='<%#Eval("jittuseki_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_11" Value='<%#Eval("jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_12" Value='<%#Eval("jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_13" Value='<%#Eval("tasseiritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_14" Value='<%#Eval("tasseiritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_15" Value='<%#Eval("tasseiritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_16" Value='<%#Eval("sintyokuritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_17" Value='<%#Eval("sintyokuritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_18" Value='<%#Eval("sintyokuritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                    </tr>
                </ItemTemplate>
            </asp:DataList>
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="3" class="U009_td_border U009_td_backcolor2">&nbsp;</td>
        <td class="U009_td_backcolor3" style="border-bottom: black 2px solid;">
            <div style="overflow:hidden;margin-top:-1px;width:559px;" runat="server" id="divSum5">
                <asp:DataList ID="grdAllMeisaiSum" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="U009_td_backcolor6" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_1" Font-Bold="true" Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#847F83" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_2" Font-Bold="true" Value='<%#Eval("g_jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_3" Font-Bold="true" Value='<%#Eval("g_jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor6" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_4" Font-Bold="true" Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#847F83" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_5" Font-Bold="true" Value='<%#Eval("keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_6" Font-Bold="true" Value='<%#Eval("keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor6" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_7" Font-Bold="true" Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#847F83" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_8" Font-Bold="true" Value='<%#Eval("mikomi_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_9" Font-Bold="true" Value='<%#Eval("mikomi_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_backcolor6" style="border-right: black 2px solid;"><cc1:CommonNumber ID="CommonNumber4" Font-Bold="true" Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#847F83" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="CommonNumber5" Font-Bold="true" Value='<%#Eval("mikomi_keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="CommonNumber6" Font-Bold="true" Value='<%#Eval("mikomi_keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_backcolor6" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_10" Font-Bold="true" Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#847F83" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_11" Font-Bold="true" Value='<%#Eval("jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_12" Font-Bold="true" Value='<%#Eval("jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonDecimal ID="numUriage1_13" Font-Bold="true" Value='<%#Eval("tasseiritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonDecimal ID="numUriage1_14" Font-Bold="true" Value='<%#Eval("tasseiritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonDecimal ID="numUriage1_15" Font-Bold="true" Value='<%#Eval("tasseiritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonDecimal ID="numUriage1_16" Font-Bold="true" Value='<%#Eval("sintyokuritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonDecimal ID="numUriage1_17" Font-Bold="true" Value='<%#Eval("sintyokuritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonDecimal ID="numUriage1_18" Font-Bold="true" Value='<%#Eval("sintyokuritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonDecimal></td>
                    </tr>
                </ItemTemplate>
                </asp:DataList>
            </div>
        </td>
    </tr>
    
    <tr>
        <td rowspan="4" class="U009_td_border U009_td_backcolor1 U009_td_font_bold" style="width:50px;">ëSëÃ<br />ÅiFC<br />èúäOÅj</td>
        <td rowspan="2" class="U009_td_border U009_td_backcolor2" style="vertical-align:top;width:70px;">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr><td>çHéñîªíËó¶<br />
                <cc1:CommonDecimal ID="numKoj6_1" CssClass="U009_TextBox_border" RightFormat="%" Width="60px" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" Value="0" runat="server"></cc1:CommonDecimal><br /></td></tr>
                <tr><td style="border-bottom-style:solid; border-bottom-color:Black; border-bottom-width:2px;">&nbsp;</td></tr>
            </table>
            çHéñéÛíçó¶<br />
            <cc1:CommonDecimal ID="numKoj6_2" CssClass="U009_TextBox_border" RightFormat="%" Width="60px" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" Value="0" runat="server"></cc1:CommonDecimal></td>
        <td class="U009_td_border U009_td_backcolor2" style="width:28px;">í≤ç∏</td>
        <td class="U009_td_backcolor2" style="width:245px;">
            <asp:DataList ID="grdAllNotFCMeisai1" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="U009_td_border U009_td_backcolor2"><cc1:CommonText ID="txtSyouhinMei1_1" Text='<%#Eval("syouhin_mei")%>' Width="148px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" runat="server"></cc1:CommonText></td>
                        <td class="U009_td_border U009_td_backcolor2"><cc1:CommonDecimal ID="numTanka1_1" Value='<%#Eval("zennen_heikin_tanka")%>' Width="90px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" runat="server"></cc1:CommonDecimal></td>
                    </tr>
                </ItemTemplate>
            </asp:DataList>
        </td>
        <td class="U009_td_backcolor3">
            <div style="overflow:hidden;width:559px;" runat="server" id="divAllNotFCMeisai1">
                <asp:DataList ID="grdAllNotFCMeisai2" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_1" Value='<%#Eval("g_jittuseki_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_2" Value='<%#Eval("g_jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_3" Value='<%#Eval("g_jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_4" Value='<%#Eval("keikaku_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_5" Value='<%#Eval("keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_6" Value='<%#Eval("keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_7" Value='<%#Eval("mikomi_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_8" Value='<%#Eval("mikomi_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_9" Value='<%#Eval("mikomi_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber1" Value='<%#Eval("mikomi_keikaku_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber2" Value='<%#Eval("mikomi_keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber3" Value='<%#Eval("mikomi_keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_10" Value='<%#Eval("jittuseki_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_11" Value='<%#Eval("jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_12" Value='<%#Eval("jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_13" Value='<%#Eval("tasseiritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_14" Value='<%#Eval("tasseiritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_15" Value='<%#Eval("tasseiritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_16" Value='<%#Eval("sintyokuritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_17" Value='<%#Eval("sintyokuritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_18" Value='<%#Eval("sintyokuritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                    </tr>
                </ItemTemplate>
            </asp:DataList>
            </div>
        </td>
    </tr>
    <tr>
        <td class="U009_td_border U009_td_backcolor2">ÇªÇÃëº</td>
        <td class="U009_td_backcolor2" style="width:245px;">
            <asp:DataList ID="grdAllNotFCMeisai5" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="U009_td_border U009_td_backcolor2"><cc1:CommonText ID="txtSyouhinMei1_1" Text='<%#Eval("syouhin_mei")%>' Width="148px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" runat="server"></cc1:CommonText></td>
                        <td class="U009_td_border U009_td_backcolor2"><cc1:CommonDecimal ID="numTanka1_1" Value='<%#Eval("zennen_heikin_tanka")%>' Width="90px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" runat="server"></cc1:CommonDecimal></td>
                    </tr>
                </ItemTemplate>
            </asp:DataList>
        </td>
        <td class="U009_td_backcolor3">
            <div style="overflow:hidden;width:559px;" runat="server" id="divAllNotFCMeisai3">
                <asp:DataList ID="grdAllNotFCMeisai6" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_1" Value='<%#Eval("g_jittuseki_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_2" Value='<%#Eval("g_jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_3" Value='<%#Eval("g_jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_4" Value='<%#Eval("keikaku_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_5" Value='<%#Eval("keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_6" Value='<%#Eval("keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_7" Value='<%#Eval("mikomi_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_8" Value='<%#Eval("mikomi_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_9" Value='<%#Eval("mikomi_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber1" Value='<%#Eval("mikomi_keikaku_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber2" Value='<%#Eval("mikomi_keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber3" Value='<%#Eval("mikomi_keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_10" Value='<%#Eval("jittuseki_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_11" Value='<%#Eval("jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_12" Value='<%#Eval("jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_13" Value='<%#Eval("tasseiritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_14" Value='<%#Eval("tasseiritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_15" Value='<%#Eval("tasseiritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_16" Value='<%#Eval("sintyokuritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_17" Value='<%#Eval("sintyokuritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_18" Value='<%#Eval("sintyokuritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                    </tr>
                </ItemTemplate>
            </asp:DataList>
            </div>
        </td>
    </tr>
    
    <tr>
        <td class="U009_td_border U009_td_backcolor2" style="vertical-align:top;">
        íºçHéñó¶<br />
        <cc1:CommonDecimal ID="numKoj6_3" CssClass="U009_TextBox_border" RightFormat="%" Width="60px" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" Value="0" runat="server"></cc1:CommonDecimal></td>
        <td class="U009_td_border U009_td_backcolor2">çHéñ</td>
        <td class="U009_td_backcolor2">
            <asp:DataList ID="grdAllNotFCMeisai3" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="U009_td_border U009_td_backcolor2"><cc1:CommonText ID="txtSyouhinMei1_1" Text='<%#Eval("syouhin_mei")%>' Width="148px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" runat="server"></cc1:CommonText></td>
                        <td class="U009_td_border U009_td_backcolor2"><cc1:CommonDecimal ID="numTanka1_1" Value='<%#Eval("zennen_heikin_tanka")%>' Width="90px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#99CDFF" runat="server"></cc1:CommonDecimal></td>
                    </tr>
                </ItemTemplate>
            </asp:DataList>
        </td>
        <td class="U009_td_backcolor3">
            <div style="overflow:hidden;width:559px;" runat="server" id="divAllNotFCMeisai2">
                <asp:DataList ID="grdAllNotFCMeisai4" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_1" Value='<%#Eval("g_jittuseki_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_2" Value='<%#Eval("g_jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_3" Value='<%#Eval("g_jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_4" Value='<%#Eval("keikaku_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_5" Value='<%#Eval("keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_6" Value='<%#Eval("keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_7" Value='<%#Eval("mikomi_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_8" Value='<%#Eval("mikomi_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_9" Value='<%#Eval("mikomi_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber1" Value='<%#Eval("mikomi_keikaku_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber2" Value='<%#Eval("mikomi_keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="CommonNumber3" Value='<%#Eval("mikomi_keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_10" Value='<%#Eval("jittuseki_kensuu")%>' Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_11" Value='<%#Eval("jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonNumber ID="numUriage1_12" Value='<%#Eval("jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_13" Value='<%#Eval("tasseiritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_14" Value='<%#Eval("tasseiritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_15" Value='<%#Eval("tasseiritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_16" Value='<%#Eval("sintyokuritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_17" Value='<%#Eval("sintyokuritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_border U009_td_backcolor3"><cc1:CommonDecimal ID="numUriage1_18" Value='<%#Eval("sintyokuritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#CFFBFF" runat="server"></cc1:CommonDecimal></td>
                    </tr>
                </ItemTemplate>
            </asp:DataList>
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="3" class="U009_td_border U009_td_backcolor2">&nbsp;</td>
        <td class="U009_td_backcolor3" style="border-bottom: black 2px solid;">
            <div style="overflow:hidden;margin-top:-1px;width:559px;" runat="server" id="divSum6">
                <asp:DataList ID="grdAllNotFCMeisaiSum" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="U009_td_backcolor6" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_1" Font-Bold="true" Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#847F83" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_2" Font-Bold="true" Value='<%#Eval("g_jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_3" Font-Bold="true" Value='<%#Eval("g_jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor6" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_4" Font-Bold="true" Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#847F83" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_5" Font-Bold="true" Value='<%#Eval("keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_6" Font-Bold="true" Value='<%#Eval("keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor6" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_7" Font-Bold="true" Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#847F83" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_8" Font-Bold="true" Value='<%#Eval("mikomi_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_9" Font-Bold="true" Value='<%#Eval("mikomi_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_backcolor6" style="border-right: black 2px solid;"><cc1:CommonNumber ID="CommonNumber4" Font-Bold="true" Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#847F83" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="CommonNumber5" Font-Bold="true" Value='<%#Eval("mikomi_keikaku_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="CommonNumber6" Font-Bold="true" Value='<%#Eval("mikomi_keikaku_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_backcolor6" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_10" Font-Bold="true" Width="50px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#847F83" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_11" Font-Bold="true" Value='<%#Eval("jittuseki_kingaku")%>' Width="100px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonNumber ID="numUriage1_12" Font-Bold="true" Value='<%#Eval("jittuseki_arari")%>' Width="95px" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonNumber></td>
                        
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonDecimal ID="numUriage1_13" Font-Bold="true" Value='<%#Eval("tasseiritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonDecimal ID="numUriage1_14" Font-Bold="true" Value='<%#Eval("tasseiritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonDecimal ID="numUriage1_15" Font-Bold="true" Value='<%#Eval("tasseiritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonDecimal ID="numUriage1_16" Font-Bold="true" Value='<%#Eval("sintyokuritu_kensuu")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonDecimal ID="numUriage1_17" Font-Bold="true" Value='<%#Eval("sintyokuritu_kingaku")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonDecimal></td>
                        <td class="U009_td_backcolor5" style="border-right: black 2px solid;"><cc1:CommonDecimal ID="numUriage1_18" Font-Bold="true" Value='<%#Eval("sintyokuritu_arari")%>' Width="50px" RightFormat="%" CssClass="U009_TextBox_border" PageReadOnly="true" ReadOnlyBackColor="#C99BFC" runat="server"></cc1:CommonDecimal></td>
                    </tr>
                </ItemTemplate>
                </asp:DataList>
            </div>
        </td>
    </tr>
</table>
</div> 
<div>
<table cellpadding="0" cellspacing="0" style="width:993px">
<tr>
    <td style="width:413px;"></td>
    <td>
        <div style="overflow:auto;margin-top:-1px;width:580px;" onscroll="fncSetScrollLeft();" runat="server" id="divBottom">
            <table style="width:1660px;"><tr><td>&nbsp;</td></tr></table>
        </div>
    </td>
</tr>
</table>
</div>
</form>
</body>
</html>
<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master" CodeBehind="YosinJyouhouInput.aspx.vb" Inherits="Itis.Earth.WebUI.YosinJyouhouInput" 
    title="与信情報照会" %>
<%@ Register Src="~/control/common_drop.ascx" TagName="common_drop" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript" src="js/jhsearth.js"></script>
<script type="text/javascript">
    //window名付与
    var objWin = window;
    objWin.name = "earthMainWindow"
    initPage(); //画面初期設定
    
</script>
<table style="text-align:left; width:900px;" border="0" cellpadding="0" cellspacing="2" class="titleTable">
    <tbody>
        <tr>
            <th style="width:780px;">
                与信情報照会
            </th>
            <th rowspan="2" style="text-align:left; color:Red;" align="center" valign="middle">「社外秘」
            </th>
        </tr>
        <tr>
            <td>
            </td>
            <td style ="height:10px;">
            </td>
        </tr>
    </tbody>
</table>
<table style="text-align:left; width:850px;" cellpadding="0" class="mainTable">
    <tbody class="tableMeiSai">
        <tr style="height: 23px">
            <td style="width:160px;" class="koumokuMei">
                名寄先コード
            </td>
            <td style="width:640px;" colspan="3">
                <asp:TextBox runat="server" ID="tbxNayoseSakiCd" style="width:70px;" cssclass="readOnly" TabIndex="-1"/>
            </td>
        </tr>
        <tr style="height:23px">
            <td style="width:160px;" class="koumokuMei">
                名寄先名１
            </td>
            <td style="width:270px;">
                <asp:TextBox runat="server" ID="tbxNayoseSakiName1" style="width:250px;" cssclass="readOnly" TabIndex="-1"/>
            </td>
            <td style="width:120px;" class="koumokuMei">
                名寄先カナ１
            </td>
            <td style="width:250px;">
                <asp:TextBox runat="server" ID="tbxNayoseSakiKana1" style="width:180px;" cssclass="readOnly" TabIndex="-1"/>
            </td>
        </tr>
        <tr style="height:23px">
            <td style="width:160px;" class="koumokuMei">
                名寄先名２
            </td>
            <td style="width:270px;">
                <asp:TextBox runat="server" ID="tbxNayoseSakiName2" style="width:250px;" cssclass="readOnly" TabIndex="-1"/>
            </td>
            <td style="width:120px;" class="koumokuMei">
                名寄先カナ２
            </td>
            <td style="width:250px;">
                <asp:TextBox runat="server" ID="tbxNayoseSakiKana2" style="width:180px;" cssclass="readOnly" TabIndex="-1"/>
            </td>
        </tr>
    </tbody>
</table>
 <table style="margin-top:10px; text-align:left; width:850px;" class="mainTable" cellpadding="0">
    <thead>
        <tr>
            <th class="tableTitle" colspan="6" rowspan="1">
                <a id="yosinKihonJyouhou" runat="server">与信基本情報</a>  
                <span id="yosinKihonJyouhouTitleInfobar" style="display:none;" runat="server"></span>
            </th>
        </tr>
    </thead>
     <tbody id="yosinKihonJyouhouTBody" runat="server" class="tableMeiSai" style="margin-top:0px; width:850px;">
        <tr style="height: 23px">      
            <td style="width:163px;" class="koumokuMei">与信限度額
            </td>    
            <td style="width:120px;">
                <asp:TextBox runat="server" ID="tbxYosinGendoGaku" style="width:90px; text-align:right;" cssclass="readOnly" TabIndex="-1"/>
            </td>  
             <td style="width:130px;" class="koumokuMei">与信警告開始率
            </td>    
            <td style="width:110px;">
                <asp:TextBox runat="server" ID="tbxYosinKeikouKaisiritsu" style="width:40px; text-align:right;" cssclass="readOnly" TabIndex="-1"/>%
            </td>  
            <td style="width:120px" class="koumokuMei">帝国評点
             </td>
             <td style="width:160px;">
                <asp:TextBox runat="server" ID="tbxTeikokuHyouten" style="width:40px; text-align:right;" cssclass="readOnly" TabIndex="-1"/>
             </td>   
        </tr>
         <tr style="height:23px">        
             <td style="width:163px" class="koumokuMei">都道府県コード
             </td>
             <td style="width:120px;">
                <asp:TextBox runat="server" ID="tbxTodouhukenCd" style="width:40px; text-align:left;" cssclass="readOnly" TabIndex="-1"/>
                <asp:Label runat="server" ID="lblTodouhukenMei"/>
             </td>
             <td style="width:130px" class="koumokuMei">直工事FLG
             </td>
             <td style="width:110px;">
                <asp:TextBox runat="server" ID="tbxTyokuKojiFlg" style="width:40px; text-align:left;" cssclass="readOnly" TabIndex="-1"/>
                <asp:label runat ="server" ID ="lblTyokuKojiFlg" />
             </td>
             <td style="width:120px" class="koumokuMei">受注管理FLG
             </td>
             <td style="width:160px; ">
                 <asp:TextBox runat="server" ID="tbxJyutyuuKanriFlg" style="width:40px; text-align:left;" cssclass="readOnly" TabIndex="-1"/>
                 <asp:label runat ="server" ID ="lblJyutyuuKanriFlg" />
             </td>
         </tr>
     </tbody>       
</table>
 <table style="margin-top:10px; text-align:left; width:850px;" class="mainTable" cellpadding="0">
    <thead>
        <tr>
            <th class="tableTitle" colspan="6" rowspan="1">
                <a id="yosinKihonJyoukyou" runat="server">与信最新状況</a>  
                <span id="yosinKihonJyoukyouTitleInfobar" style="display:none;" runat="server"></span>
            </th>
        </tr>
    </thead>
     <tbody id="yosinKihonJyoukyouTBody" runat="server" class="tableMeiSai" style="margin-top:0px; width:700px; ">
        <tr style="height:23px;">
             <td style="width:392px" class="koumokuMei">与信警告状況
             </td>
             <td style="width:373px;">
                 <asp:TextBox runat="server" ID="tbxKeikokuJoukyou" style="width:110px; text-align:left;" cssclass="readOnly" TabIndex="-1"/>
             </td>
             <td style="width:383px" class="koumokuMei">前日工事状況FLG  
             </td>
             <td colspan="3">
                <asp:TextBox runat="server" ID="tbxZenjitsuKojiFlg" style="width:40px; text-align:left;" cssclass="readOnly" TabIndex="-1"/>
                <asp:label runat ="server" ID ="lblZenjitsuKojiFlg" />
             </td> 
        </tr>
        <tr style="height: 23px">
            <td style="width:392px;" class="koumokuMei">前月残高
            </td>    
            <td style="width:373px;">
                <asp:TextBox runat="server" ID="tbxZengetsuSaikenGaku" style="width:110px; text-align:right;" cssclass="readOnly" TabIndex="-1"/>
            </td> 
            <td style="width:383px;" class="koumokuMei">前月残高設定年月日
            </td>    
            <td colspan="3">
                <asp:TextBox runat="server" ID="tbxZengetsuSaikenSetDate" style="width:80px; text-align:left;" cssclass="readOnly" TabIndex="-1"/>
            </td>     
        </tr>
        <tr style="height:23px;">
            <td style="width:392px;" class="koumokuMei">当月入金額
            </td>    
            <td style="width:373px;">
                <asp:TextBox runat="server" ID="tbxRuisekiNyuukinGaku" style="width:110px; text-align:right;" cssclass="readOnly" TabIndex="-1"/>
            </td> 
            <td style="width:383px;" class="koumokuMei">当月入金額設定日
            </td>    
            <td colspan="3">
                <asp:TextBox runat="server" ID="tbxRuisekiNyuukinSetDateFrom" style="width:80px; text-align:left;" cssclass="readOnly" TabIndex="-1"/>
                &nbsp; ～ &nbsp;
                <asp:TextBox runat="server" ID="tbxRuisekiNyuukinSetDateTo" style="width:80px; text-align:left;" cssclass="readOnly" TabIndex="-1"/>
            </td>
        </tr>
        <tr style="height: 23px;">
            <td style="width:392px; height: 20px;" class="koumokuMei">
                当月売上額<br />
                (調査・工事・その他)
            </td>    
            <td style="width:373px; height: 20px;">
                <asp:TextBox runat="server" ID="tbxRuisekiJyutyuuGaku" style="width:110px; text-align:right;" cssclass="readOnly" TabIndex="-1"/>
                <asp:Button ID="btnMeisai" runat="server" Text="当月明細" /></td> 
            <td style="width:383px; height: 20px;" class="koumokuMei">
                当月売上額設定日<br />
                (調査・工事・その他)
            </td>    
            <td style="height: 20px;" colspan="3">
                <asp:TextBox runat="server" ID="tbxRuisekiJyutyuuSetDateFrom" style="width:80px; text-align:left;" cssclass="readOnly" TabIndex="-1" />
                &nbsp; ～ &nbsp;
                <asp:TextBox runat="server" ID="tbxRuisekiJyutyuuSetDateTo" style="width:80px; text-align:left;" cssclass="readOnly" TabIndex="-1"/>
            </td>
        </tr>
        <tr style="height:23px;">
            <td style="width:392px;" class="koumokuMei">
                当月売上額<br />
                (建物検査)
            </td>    
            <td style="width:373px;">
                <asp:TextBox runat="server" ID="tbxRuisekiKasiuriGaku" style="width:110px; text-align:right;" cssclass="readOnly" TabIndex="-1"/>
            </td> 
            <td style="width:383px;" class="koumokuMei">
                当月売上額設定日<br />
                (建物検査)
            </td>    
            <td colspan="3">
                <asp:TextBox runat="server" ID="tbxRuisekiKasiuriSetDateFrom" style="width:80px; text-align:left;" cssclass="readOnly" TabIndex="-1"/>
                &nbsp; ～ &nbsp;
                <asp:TextBox runat="server" ID="tbxRuisekiKasiuriSetDateTo" style="width:80px; text-align:left;" cssclass="readOnly" TabIndex="-1"/>
            </td>
        </tr> 
        <tr style="height:23px;">
            <td style="width:392px;" class="koumokuMei">当月売掛金合計額
            </td>    
            <td style="width:373px;" >
                <asp:TextBox runat="server" ID="tbxSaikengaku" style="width:110px; text-align:right;" cssclass="readOnly" TabIndex="-1"/>
            </td>
            <td style="width:383px;" class="koumokuMei">与信残額
            </td>
            <td style="width:225px;">
                <asp:TextBox runat="server" ID="tbxYosinZangaku" style="width:110px; text-align:right;" cssclass="readOnly" TabIndex="-1"/>&nbsp;
            </td> 
            <td style="width: 212px" class="koumokuMei">
                与信消化率
            </td>
            <td style="width: 85px">
                <asp:TextBox runat="server" ID="tbxYosinSyokaritu" style="width:40px; text-align:right;" cssclass="readOnly" TabIndex="-1"/>%</td>
        </tr> 
     </tbody>       
</table>
<table style="margin-top:10px; text-align:left; width:850px;" class="mainTable" cellpadding="0">
    <thead>
        <tr>
            <th class="tableTitle" colspan="4" rowspan="1">
                <a id="nyuukinYoteiLink" runat="server">入金予定情報</a>  
                <asp:Label ID="Label1" runat="server"  Width="90px"></asp:Label>
                <asp:Label ID="lblSign" runat="server"></asp:Label>
                <span id="nyuukinYoteiTitleInfobar" style="display:none;" runat="server"></span>
            </th>
        </tr>
    </thead>
     <tbody id="nyuukinYoteiTbody" runat="server" class="tableMeiSai" style="margin-top:0px; width:850px;">
        <tr style="height:23px;">
             <td style="width:146px;text-align:center;" class="koumokuMei">項目
             </td>
             <td style="width:128px;text-align:center;" class="koumokuMei">金額
             </td>
             <td style="width:97px;text-align:center;" class="koumokuMei">予定日
             </td>
             <td style="width:464px;text-align:center;" class="koumokuMei">備考
             </td>
        </tr>
        <tr id="nyuukinYoteiTr" runat="server">
            <td colspan ="4" style ="padding:0px; ">
                <asp:GridView ID="grdNyuukinYotei" runat="server" AutoGenerateColumns="False" 
                    BorderWidth="0px"  CellPadding="0" ShowHeader="False" Width="850px">
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="160px" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <asp:Label ID="lblNyuukinYoteiSyubetsu" runat="server" style="width:110px; text-align:center;" Text='<%# eval("meisyou") %>' Width="110px" TabIndex="-1"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="130px">
                            <ItemTemplate>
                                <asp:TextBox ID="tbxNyuukinYoteiGaku" runat="server" CssClass="readOnly" style="width:110px; text-align:right;" Text='<%# eval("nyuukinyotei_gaku") %>' ReadOnly="true" TabIndex="-1"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="100px">
                            <ItemTemplate>
                                <asp:TextBox ID="tbxNyuukinYoteiDate" runat="server" CssClass="readOnly" Text='<%# eval("nyuukinyotei_date") %>' Width="80px" ReadOnly="true" TabIndex="-1"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="460px">
                            <ItemTemplate>
                                <asp:TextBox ID="tbxBikou" runat="server" CssClass = "readOnly" Text='<%# eval("bikou") %>' Width="440px" ReadOnly="true" TabIndex="-1"></asp:TextBox>
                            </ItemTemplate>    
                        </asp:TemplateField>
                </Columns>
              </asp:GridView>
          </td>
     </tr>
</tbody>
</table>
<table class="buttonsTable" style="margin-top:15px;">
	<tr>
		<td>
			<asp:Button ID="btnModoru" runat="server" CssClass="kyoutuubutton" Text="戻　る" />
	    </td>
	</tr>
</table>
</asp:Content>

<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master" CodeBehind="YosinJyouhouInquiry.aspx.vb" Inherits="Itis.Earth.WebUI.YosinJyouhouInquiry_aspx" 
    title="加盟店与信情報照会" %>
<%@ Register Src="control/kyoutuu_jyouhou.ascx" TagName="kyoutuu_jyouhou" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript" src="js/jhsearth.js"></script>
<script type="text/javascript">
    //window名付与
    var objWin = window;
    objWin.name = "earthMainWindow"
    initPage(); //画面初期設定
    
</script>

    <uc1:kyoutuu_jyouhou id="Kyoutuu_jyouhou1" runat="server" GetStyle ="YosinJyouhou" ></uc1:kyoutuu_jyouhou>
    <table style="text-align: left;width: 100%; margin-top :10px;" border="0" cellpadding="0" cellspacing="2" class="titleTable">
        <tbody>
            <tr>
                <th>
                    与信情報
                </th>
            </tr>
        </tbody>
    </table>
<div style="overflow:auto ;height:277px; width:967px;border-width:0px; border-style:solid;">
    <table style="text-align: left;width: 950px;" class="mainTable" cellpadding="0">
        <thead>
            <tr>
                <th class="tableTitle" colspan="8" rowspan="1">
                    <a id="tyousaDispLink" runat="server">調査</a>
                    <span id="tyousaTitleInfobar" style="display: none;" runat="server"></span>
                </th>
            </tr>
        </thead>
        
        <!--調査情報明細-->
         <tbody id="kyoutuTBody" runat="server"  class="tableMeiSai" style="margin-top:0px;width: 950px; ">
            <tr>
                <td style="width: 165px; border-bottom-style :none ;" class="koumokuMei">調査請求先コード</td>
                <td style="width: 215px;border-bottom-style :none ;">
                    <asp:TextBox runat="server" ID="tbxTyousaSeikyuuCd" style="width: 40px;" CssClass ="readOnly"  readonly="true" Text=""  TabIndex="-1"/>
                    <asp:TextBox runat="server" ID="tbxTyousaSeikyuuBrc" style="width: 24px;" CssClass ="readOnly"  readonly="true" Text=""  TabIndex="-1"/>
                    <asp:TextBox runat="server" ID="lblTyousaSeikyuuKbn" style="width: 60px;" CssClass ="readOnly"  readonly="true" Text=""  TabIndex="-1"/>
                <td style="width: 130px" class="koumokuMei">調査請求先名1</td>
                <td style="width: 430px">
                    <asp:TextBox runat="server" ID="tbxTyousaSeikyuuName1" style="width: 280px;" CssClass ="readOnly"  readonly="true" TabIndex="-1"/>
                </td>
            </tr>
            <tr>
                <td style="width: 165px;border-top-style :none ; height: 20px;" class="koumokuMei"></td>
                <td style="width: 215px;border-top-style :none ; height: 20px;"></td> 
                <td style="width: 130px; height: 20px;" class="koumokuMei">調査請求先名2</td>
                <td style="width: 430px; height: 20px;">
                    <asp:TextBox runat="server" ID="tbxTyousaSeikyuuName2" style="width: 280px;" CssClass ="readOnly"  readonly="true" TabIndex="-1"/>
                </td>
            </tr>
            <tr>
                <td style="width: 165px;border-bottom-style :none ;" class="koumokuMei">調査名寄先コード</td>
                <td style="width: 215px;border-bottom-style :none ;">
                    <asp:TextBox runat="server" ID="tbxTyousaNayoseCd"   style="width: 70px;"  readonly="true" TabIndex="-1"/>
                </td>
                <td style="width: 130px" class="koumokuMei">調査名寄先名1</td>
                <td style="width: 430px">
                    <asp:TextBox runat="server" ID="tbxTyousaNayoseName1" style="width: 280px;" CssClass ="readOnly"  readonly="true" TabIndex="-1"/>
                </td>
            </tr>
            <tr>
                <td style="width: 165px;border-top-style :none ; height: 20px;"class="koumokuMei"></td>
                <td style="width: 215px;border-top-style :none ; height: 20px;">
                    <asp:Label ID="lblTyousaNayose" runat="server" Text="" TabIndex="-1" ForeColor ="Blue" ></asp:Label>
                </td> 
                <td style="width: 130px; height: 20px;" class="koumokuMei">調査名寄先名2</td>
                <td style="height: 20px; width: 430px;">
                    <asp:TextBox runat="server" ID="tbxTyousaNayoseName2" style="width: 280px;" CssClass ="readOnly"  readonly="true" TabIndex="-1"/>
                </td>
            </tr>
             
            <tr>
                <td style="width: 165px" class="koumokuMei">与信警告状況</td>
                <td style="width: 170px" colspan ="3">
                    <asp:TextBox runat="server" ID="tbxTyousaYoshinKeikokuJyoukyou" style="width: 70px;" CssClass ="readOnly"  readonly="true" TabIndex="-1"/>
                    <asp:Label ID="lblTyousaYoshinKeikokuJyoukyou" runat="server" Text=""  TabIndex="-1"></asp:Label>
                </td>
            </tr>   
         </tbody>       
    </table>    
    <table style="text-align: left;width: 950px;margin-top:5px;" class="mainTable" cellpadding="0">
        <thead>
            <tr>
                <th class="tableTitle" colspan="8" rowspan="1" style="width: 946px">
                    <a id="koujiDispLink" runat="server">工事</a>
                    <span id="koujiTitleInfobar" style="display: none;" runat="server"></span>
                </th>
            </tr>
        </thead>
        
        <!--工事情報明細-->
         <tbody id="koujiTBody" runat="server"  class="tableMeiSai" style="margin-top:10px;width: 950px; ">
            <tr>
                <td style="width: 165px; border-bottom-style :none ;" class="koumokuMei">工事請求先コード</td>
                <td style="width: 215px; border-bottom-style :none ;">
                    <asp:TextBox runat="server" ID="tbxKoujiSeikyuuCd" style="width: 40px;" CssClass ="readOnly"  readonly="true" TabIndex="-1"/>
                    <asp:TextBox runat="server" ID="tbxKoujiSeikyuuBrc" style="width: 24px;" CssClass ="readOnly"  readonly="true" Text=""  TabIndex="-1"/>
                    <asp:TextBox runat="server" ID="lblKoujiSeikyuuKbn" style="width: 60px;" CssClass ="readOnly"  readonly="true" Text=""  TabIndex="-1"/>
                </td>
                <td style="width: 130px;" class="koumokuMei">工事請求先名1</td>
                <td style="width: 430px">
                    <asp:TextBox runat="server" ID="tbxKoujiSeikyuuName1" style="width: 280px;" CssClass ="readOnly"  readonly="true" TabIndex="-1"/>
                </td>
            </tr>
            <tr>
                <td style="width: 165px;border-top-style :none ; height: 20px;" class="koumokuMei"></td>
                <td style="width: 215px;border-top-style :none ; height: 20px;"></td> 
                <td style="width: 130px; height: 20px;" class="koumokuMei">工事請求先名2</td>
                <td style="height: 20px; width: 430px;">
                    <asp:TextBox runat="server" ID="tbxKoujiSeikyuuName2" style="width: 280px;" CssClass ="readOnly"  readonly="true" TabIndex="-1"/>
                </td>
            </tr>
            <tr>
                <td style="width: 165px;border-bottom-style :none ;" class="koumokuMei">工事名寄先コード</td>
                <td style="width: 215px;border-bottom-style :none ;">
                    <asp:TextBox runat="server" ID="tbxKoujiNayoseCd"  style="width: 70px;" readonly="true" TabIndex="-1"/>
                </td>
                <td style="width: 130px" class="koumokuMei">工事名寄先名1</td>
                <td style="width: 430px">
                    <asp:TextBox runat="server" ID="tbxKoujiNayoseName1" style="width: 280px;" CssClass ="readOnly"  readonly="true" TabIndex="-1"/>
                </td>
            </tr>
            <tr>
                <td style="width: 165px;border-top-style :none ;" class="koumokuMei"></td>
                <td style="width: 215px;border-top-style :none ; height: 20px;">
                    <asp:Label ID="lblKoujiNayose" runat="server" Text="" TabIndex="-1" ForeColor ="Blue" ></asp:Label>
                </td> 
                <td style="width: 130px" class="koumokuMei">工事名寄先名2</td>
                <td style="width: 430px">
                    <asp:TextBox runat="server" ID="tbxKoujiNayoseName2" style="width: 280px;" CssClass ="readOnly"  readonly="true" TabIndex="-1"/>
                </td>
            </tr>
            <tr>
                <td style="width: 165px" class="koumokuMei">与信警告状況</td>
                <td style="width: 170px" colspan ="3">
                    <asp:TextBox runat="server" ID="tbxKoujiYoshinKeikokuJyoukyou" style="width: 70px;" CssClass ="readOnly"  readonly="true" TabIndex="-1"/>
                    <asp:Label ID="lblKoujiYoshinKeikokuJyoukyou" runat="server" Text=""  TabIndex="-1"></asp:Label>
                </td>
            </tr> 
         </tbody>       
    </table>  
    <table style="text-align: left;width: 950px;margin-top:5px;" class="mainTable" cellpadding="0">
        <thead>
            <tr>
                <th class="tableTitle" colspan="8" rowspan="1">
                    <a id="hansakuhinDispLink" runat="server">販促品</a>
                    <span id="hansakuhinTitleInfobar" style="display: none;" runat="server"></span>
                </th>
            </tr>
        </thead>
        
        <!--販促品情報明細-->
         <tbody id="hansakuhinTBody" runat="server"  class="tableMeiSai" style="margin-top:0px;width: 950px; ">
           <tr>
                <td style="width: 165px; border-bottom-style :none ;" class="koumokuMei">販促品請求先コード</td>
                <td style="width: 215px; border-bottom-style :none ;">
                    <asp:TextBox runat="server" ID="tbxHansokuhinSeikyuuCd" style="width: 40px;" CssClass ="readOnly"  readonly="true" TabIndex="-1"/>
                    <asp:TextBox runat="server" ID="tbxHansokuhinSeikyuuBrc" style="width: 24px;" CssClass ="readOnly"  readonly="true" Text=""  TabIndex="-1"/>
                    <asp:TextBox runat="server" ID="lblHansokuhinSeikyuuKbn" style="width: 60px;" CssClass ="readOnly"  readonly="true" Text=""  TabIndex="-1"/>
                </td>
                <td style="width: 130px; height: 23px" class="koumokuMei">販促品請求先名1</td>
                <td style="height: 23px; width: 430px;">
                    <asp:TextBox runat="server" ID="tbxHansokuhinSeikyuuName1" style="width: 280px;" CssClass ="readOnly"  readonly="true" TabIndex="-1"/>
                </td>
            </tr>
            <tr>
                <td style="width: 165px;border-top-style :none ;" class="koumokuMei"></td>
                <td style="width: 215px;border-top-style :none ;"></td> 
                <td style="width: 130px" class="koumokuMei">販促品請求先名2</td>
                <td style="width: 430px">
                    <asp:TextBox runat="server" ID="tbxHansokuhinSeikyuuName2" style="width: 280px;" CssClass ="readOnly"  readonly="true" TabIndex="-1"/>
                </td>
            </tr>
            <tr>
                <td style="width: 165px;border-bottom-style :none ;" class="koumokuMei">販促品名寄先コード</td>
                <td style="width: 215px;border-bottom-style :none ;">
                    <asp:TextBox runat="server" ID="tbxHansokuhinNayoseCd"   style="width: 70px;" readonly="true" TabIndex="-1"/>
                </td>
                <td style="width: 130px" class="koumokuMei">販促品名寄先名1</td>
                <td style="width: 430px">
                    <asp:TextBox runat="server" ID="tbxHansokuhinNayoseName1" style="width: 280px;" CssClass ="readOnly"  readonly="true" TabIndex="-1"/>
                </td>
            </tr>
            <tr>
                <td style="width: 165px;border-top-style :none ;" class="koumokuMei"></td>
                <td style="width: 215px;border-top-style :none ; height: 20px;">
                    <asp:Label ID="lblHansokuhinNayose" runat="server" Text="" TabIndex="-1" ForeColor ="Blue" ></asp:Label>
                </td> 
                <td style="width: 130px" class="koumokuMei">販促品名寄先名2</td>
                <td style="width: 430px">
                    <asp:TextBox runat="server" ID="tbxHansokuhinNayoseName2" style="width: 280px;" CssClass ="readOnly"  readonly="true" TabIndex="-1"/>
                </td>
            </tr>
            <tr>
                <td style="width: 165px" class="koumokuMei">与信警告状況</td>
                <td style="width: 170px" colspan ="3">
                    <asp:TextBox runat="server" ID="tbxHansokuhinYoshinKeikokuJyoukyou" style="width: 70px;" CssClass ="readOnly"  readonly="true" TabIndex="-1"/>
                    <asp:Label ID="lblHansokuhinYoshinKeikokuJyoukyou" runat="server" Text=""  TabIndex="-1"></asp:Label>
                </td>
            </tr>
         </tbody>
             </table>  
    <table style="text-align: left;width: 950px;margin-top:5px;" class="mainTable" cellpadding="0">
        <thead>
            <tr>
                <th class="tableTitle" colspan="8" rowspan="1">
                    <a id="tatemonoDispLink" runat="server">設計確認</a>
                    <span id="tatemonoTitleInfobar" style="display: none;" runat="server"></span>
                </th>
            </tr>
        </thead>
         <tbody id="tatemonoTBody" runat="server"  class="tableMeiSai" style="margin-top:0px;width: 950px; ">
           <tr>
                <td style="width: 165px; border-bottom-style :none ;" class="koumokuMei">設計確認請求先コード</td>
                <td style="width: 215px; border-bottom-style :none ;">
                    <asp:TextBox runat="server" ID="tbxTatemonoSeikyuuCd" style="width: 40px;" CssClass ="readOnly"  readonly="true" TabIndex="-1"/>
                    <asp:TextBox runat="server" ID="tbxTatemonoSeikyuuBrc" style="width: 24px;" CssClass ="readOnly"  readonly="true" Text=""  TabIndex="-1"/>
                    <asp:TextBox runat="server" ID="lblTatemonoSeikyuuKbn" style="width: 60px;" CssClass ="readOnly"  readonly="true" Text=""  TabIndex="-1"/>
                </td>
                <td style="width: 130px; height: 23px" class="koumokuMei">設計確認請求先名1</td>
                <td style="height: 23px; width: 430px;">
                    <asp:TextBox runat="server" ID="tbxTatemonoSeikyuuName1" style="width: 280px;" CssClass ="readOnly"  readonly="true" TabIndex="-1"/>
                </td>
            </tr>
            <tr>
                <td style="width: 165px;border-top-style :none ;" class="koumokuMei"></td>
                <td style="width: 215px;border-top-style :none ;"></td> 
                <td style="width: 130px" class="koumokuMei">設計確認請求先名2</td>
                <td style="width: 430px">
                    <asp:TextBox runat="server" ID="tbxTatemonoSeikyuuName2" style="width: 280px;" CssClass ="readOnly"  readonly="true" TabIndex="-1"/>
                </td>
            </tr>
            <tr>
                <td style="width: 165px;border-bottom-style :none ;" class="koumokuMei">設計確認名寄先コード</td>
                <td style="width: 215px;border-bottom-style :none ;">
                    <asp:TextBox runat="server" ID="tbxTatemonoNayoseCd"   style="width: 70px;" readonly="true" TabIndex="-1"/>
                </td>
                <td style="width: 130px" class="koumokuMei">設計確認名寄先名1</td>
                <td style="width: 430px">
                    <asp:TextBox runat="server" ID="tbxTatemonoNayoseName1" style="width: 280px;" CssClass ="readOnly"  readonly="true" TabIndex="-1"/>
                </td>
            </tr>
            <tr>
                <td style="width: 165px;border-top-style :none ;" class="koumokuMei"></td>
                <td style="width: 215px;border-top-style :none ; height: 20px;">
                    <asp:Label ID="lblTatemonoNayose" runat="server" Text="" TabIndex="-1" ForeColor ="Blue" ></asp:Label>
                </td> 
                <td style="width: 130px" class="koumokuMei">設計確認名寄先名2</td>
                <td style="width: 430px">
                    <asp:TextBox runat="server" ID="tbxTatemonoNayoseName2" style="width: 280px;" CssClass ="readOnly"  readonly="true" TabIndex="-1"/>
                </td>
            </tr>
            <tr>
                <td style="width: 165px" class="koumokuMei">与信警告状況</td>
                <td style="width: 170px" colspan ="3">
                    <asp:TextBox runat="server" ID="tbxTatemonoYoshinKeikokuJyoukyou" style="width: 70px;" CssClass ="readOnly"  readonly="true" TabIndex="-1"/>
                    <asp:Label ID="lblTatemonoYoshinKeikokuJyoukyou" runat="server" Text=""  TabIndex="-1"></asp:Label>
                </td>
            </tr>
         </tbody>        
    </table>  
    <asp:Button ID="btnTemp" runat="server" style =" display :none" />
    <asp:HiddenField ID="hidNayoseCd" runat="server" />
</div>
</asp:Content>
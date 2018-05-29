<%@ Page Language="VB" MasterPageFile="~/EKKSMaster.master" Title="登録事業者別 計画管理" AutoEventWireup="false"
    CodeFile="KeikakuKanriSearchList.aspx.vb" Inherits="KeikakuKanriSearchList" %>

<%@ Register Src="CommonControl/CommonButton.ascx" TagName="CommonButton" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/EKKSMaster.master" %>
<asp:Content ID="C1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        //onload
        window.onload = function(){
        window.name = "<%=CommonConstBC.eigyouKeikakuKanri%>"
        setMenuBgColor();
            if ("<%=gridviewRightId%>"!=''){
                objEBI("<%=tblRightId%>").style.height=objEBI("<%=gridviewRightId%>").offsetHeight;
                objEBI("<%=tblLeftId%>").style.width=objEBI("<%=gridviewRightId%>").offsetWidth;
            }
        }
    </script>

    <asp:HiddenField ID="hidSeni" runat="server" />
    <asp:HiddenField ID="hidDataTime" runat="server" />
    <%-- <div id="buySelName" runat="server" class="modalDiv" style="position:absolute;left:300px; top:140px;z-index:2;display:none;">
    </div>
    <div id="disableDiv" runat="server" style="position:absolute; left:0px; top:0px; width:1002px; height:590px; z-index:100;  FILTER:alpha(opacity=70);background-color:#000000; display:none;">
        <iframe src="about:blank" id="hiddenIframe" width="100%" height="100%"></iframe>
    </div>--%>
    <table border="0" cellpadding="0" cellspacing="2" class="titleTable" style="width: 960px;
        text-align: left; margin-top: -10px;">
        <tbody>
            <tr>
                <th style="width: 76px">
                    計画管理
                </th>
                <td style="width: 69px" class="koumokuMei">
                    計画年度
                </td>
                <th style="width: 610px">
                    <asp:DropDownList ID="ddlKeikaku" runat="server" Width="144px" AppendDataBoundItems="True">
                    </asp:DropDownList><uc1:CommonButton ID="btnClear" runat="server" Text="クリア" />
                </th>
                <th style="text-align: right">
                    <uc1:CommonButton ID="btnHouRenSou" runat="server" Text="報連相へ" />
                </th>
            </tr>
        </tbody>
    </table>
    <table border="0" cellpadding="0" cellspacing="2" class="titleTable" style="width: 960px;
        text-align: left">
        <tbody>
            <tr>
                <td style="width: 30px">
                </td>
                <td colspan="3">
                    <table cellpadding="2" class="mainTable2" style="width: 928px">
                        <tr>
                            <td class="koumokuMei" rowspan="4" style="width: 90px; text-align: center;">
                                条 件
                            </td>
                            <td class="koumokuMei" style="width: 52px">
                                支店名
                            </td>
                            <td style="width: 148px">
                                <asp:TextBox ID="tbxSiten" runat="server" Text="" Width="80px"></asp:TextBox>
                                <asp:Button ID="btnShiten" runat="server" Text="検索" />
                                <asp:HiddenField ID="hidSitenCd" runat="server" />
                            </td>
                            <td class="koumokuMei">
                                営業マン
                            </td>
                            <td style="width: 147px">
                                <asp:TextBox ID="tbxUser" runat="server" Width="80px"></asp:TextBox>
                                <asp:Button ID="btnUser" runat="server" Text="検索" />
                                <asp:HiddenField ID="hidUser" runat="server" />
                            </td>
                            <td class="koumokuMei">
                                登録事業者
                            </td>
                            <td style="width: 146px">
                                <asp:TextBox ID="tbxKameiten" runat="server" Width="80px"></asp:TextBox>
                                <asp:Button ID="btnKameiten" runat="server" Text="検索" />
                                <asp:HiddenField ID="hidKameiten" runat="server" />
                            </td>
                            <td class="koumokuMei">
                                営業所
                            </td>
                            <td style="width: 142px">
                                <asp:TextBox ID="tbxEigyou" runat="server" Width="80px"></asp:TextBox>
                                <asp:Button ID="btnEigyou" runat="server" Text="検索" />
                                <asp:HiddenField ID="hidEigyou" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="koumokuMei">
                                所属
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSyozoku" runat="server" Style="width: 120px;">
                                </asp:DropDownList>
                            </td>
                            <td class="koumokuMei">
                                都道府県
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlTodoufuken" runat="server" Style="width: 120px;">
                                </asp:DropDownList>
                            </td>
                            <td class="koumokuMei">
                                統一法人
                            </td>
                            <td>
                                <asp:TextBox ID="tbxTouituHoujin" runat="server" Width="80px"></asp:TextBox>
                                <asp:Button ID="btnTouituHoujin" runat="server" Text="検索" />
                                <asp:HiddenField ID="hidTouituHoujin" runat="server" />
                            </td>
                            <td class="koumokuMei">
                                法人
                            </td>
                            <td>
                                <asp:TextBox ID="tbxHoujin" runat="server" Width="80px"></asp:TextBox>
                                <asp:Button ID="btnHoujin" runat="server" Text="検索" />
                                <asp:HiddenField ID="hidHoujin" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="koumokuMei">
                                属性1
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlZokusei1" runat="server" Style="width: 120px;">
                                </asp:DropDownList>
                            </td>
                            <td class="koumokuMei">
                                属性2
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlZokusei2" runat="server" Style="width: 120px;">
                                </asp:DropDownList>
                            </td>
                            <td class="koumokuMei">
                                属性3
                            </td>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlZokusei3" runat="server" Style="width: 120px;">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="koumokuMei">
                                属性4
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlZokusei4" runat="server" Style="width: 120px;">
                                </asp:DropDownList>
                            </td>
                            <td class="koumokuMei">
                                属性5
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlZokusei5" runat="server" Style="width: 120px;">
                                </asp:DropDownList>
                            </td>
                            <td class="koumokuMei">
                                属性6
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="tbxZokusei6" runat="server" Width="100px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table cellpadding="2" class="mainTable2" style="width: 928px; border-top: 0px none;">
                        <tr>
                            <td class="koumokuMei" style="width: 90px; border-top-style: none;">
                                表示対象期間
                            </td>
                            <td style="border-top-style: none; width: 330px;">
                                <asp:CheckBox ID="chkKongetu" runat="server" Text="今月" Style="" />
                                <asp:CheckBox ID="chkSangetu" runat="server" Text="直近3ヶ月" Style="margin-left: 50px;" />
                                <asp:CheckBox ID="chkYogetu" runat="server" Text="先行4ヶ月" Style="margin-left: 50px;" />
                            </td>
                            <td class="koumokuMei" style="border-top-style: none;">
                                営業区分
                            </td>
                            <td style="border-top-style: none" colspan="2">
                                <asp:CheckBox ID="chkEigyou" runat="server" Text="営業" Style="" />
                                <asp:CheckBox ID="chkEigyouNew" runat="server" Text="営業(新規)" Style="margin-left: 8px;" />
                                <asp:CheckBox ID="chkTokuhan" runat="server" Text="特販" Style="margin-left: 8px;" />
                                <asp:CheckBox ID="chkTokuhanNew" runat="server" Text="特販(新規)" Style="margin-left: 8px;" />
                                <asp:CheckBox ID="chkFC" runat="server" Text="FC" Style="margin-left: 8px;" />
                                <asp:CheckBox ID="chkFCNew" runat="server" Text="FC(新規)" Enabled="false" Style="margin-left: 8px;" />
                                <asp:CheckBox ID="chkSinki" runat="server" Text="新規" Style="display: none;" />
                            </td>
                        </tr>
                    </table>
                    <table cellpadding="2" class="mainTable2" style="width: 928px; border-top-style: none;">
                        <tr>
                            <td class="koumokuMei" style="width: 90px; border-top-style: none;">
                                絞込み選択
                            </td>
                            <td style="width: 330px; border-top-style: none;">
                                <asp:CheckBox ID="chkKeikakuTi" runat="server" Text="計画値0" Style="" />
                                <asp:CheckBox ID="chkNenkanTouSuu" runat="server" Text="年間棟数順" Style="margin-left: 160px;" />
                                <asp:CheckBox ID="chkSinkiTouroku" runat="server" Text="新規登録事業者" Style="display: none;" />
                                <asp:CheckBox ID="chkBunjyou" runat="server" Text="分譲50社" Style="display: none;" />
                                <asp:CheckBox ID="chkTyumon" runat="server" Text="注文50社" Style="display: none;" />
                            </td>
                            <td class="koumokuMei" style="width: 85px; border-top-style: none;">
                                年間棟数範囲
                            </td>
                            <td style="width: 65px; border-top-style: none;">
                                <asp:CheckBox ID="chkKeikakuyou" runat="server" Text="計画用" />
                            </td>
                            <td colspan="2" style="border-top-style: none;">
                                <asp:TextBox ID="tbxNenkanTousuuFrom" runat="server" CssClass="codeNumber" Width="70px"></asp:TextBox>
                                <asp:Label ID="lblLine" runat="server" Text="～" Style="margin-left: 5px;"></asp:Label>
                                <asp:TextBox ID="tbxNenkanTousuuTo" runat="server" CssClass="codeNumber" Width="70px"
                                    Style="margin-left: 5px;"></asp:TextBox>
                            </td>
                            <td rowspan="2" style="vertical-align: middle; text-align: center; border-top-style: none;">
                                <uc1:CommonButton ID="btnKeikakuHyouji" runat="server" Text="計画表示" />
                            </td>
                        </tr>
                        <tr>
                            <td class="koumokuMei">
                                表示欄選択
                            </td>
                            <td colspan="3">
                                <asp:CheckBox ID="chkZennenDougetu" runat="server" Text="前年同月" />
                                <asp:CheckBox ID="chkKeikaku" runat="server" Text="計画" Style="margin-left: 20px;" />
                                <asp:CheckBox ID="chkMikomi" runat="server" Text="見込" Style="margin-left: 20px;"
                                    Enabled="false" />
                                <asp:CheckBox ID="chkJisseki" runat="server" Text="実績" Style="margin-left: 20px;" />
                                <asp:CheckBox ID="chkTassei" runat="server" Text="達成率" Style="margin-left: 20px;" />
                                <asp:CheckBox ID="chkSintyoku" runat="server" Text="進捗率" Style="margin-left: 20px;" />
                            </td>
                            <td class="koumokuMei" style="width: 65px;">
                                表示件数
                            </td>
                            <td style="width: 140px;">
                                <asp:DropDownList ID="ddlKensuu" runat="server" Width="130px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 30px; height: 0px">
                </td>
                <td colspan="3">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 928px;">
                        <tr>
                            <td>
                                <uc1:CommonButton ID="btnSyuturyoku" runat="server" Text="CSV出力" Height="25px" />
                                <uc1:CommonButton ID="btnTorikomi1" runat="server" Text="計画取込" Height="25px" />
                                <uc1:CommonButton ID="btnTorikomi2" runat="server" Text="見込取込" Height="25px" Enabled="false" />
                                <uc1:CommonButton ID="btnTorikomi3" runat="server" Text="FC用計画取込" Height="25px" />
                                <uc1:CommonButton ID="btnTorikomi4" runat="server" Text="FC用見込取込" Height="25px" Enabled="false" />
                                <asp:HyperLink ID="HyperLink2" runat="server">HyperLink</asp:HyperLink>
                            </td>
                            <td style="width: 250px;">
                                <asp:LinkButton ID="linMae" runat="server" Font-Bold="true" Text="前←"></asp:LinkButton>
                                <asp:Label ID="lblKensuu" runat="server" Font-Bold="true"></asp:Label>
                                <asp:LinkButton ID="lnkAto" Font-Bold="true" runat="server" Text="→次"></asp:LinkButton>
                            </td>
                            <td style="width: 80px; text-align: right;">
                                <asp:Label ID="lblSumi" runat="server" Font-Bold="true" Width="70px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 30px; height: 135px">
                </td>
                <td colspan="3" style="height: 135px; vertical-align: top;">
                    <!---------------------検索結果区----------------------->
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <div id="divHeadLeft" style="width: 473px; height: 81px; overflow-y: hidden; overflow-x: hidden;">
                                    <table runat="server" id="gridviewLeft" class="gridviewLeft" border="1" cellpadding="0"
                                        cellspacing="0">
                                        <tr style="height: 17px;">
                                            <td class="build" rowspan="4" style="width: 90px; text-align: center">
                                                <asp:Label ID="lblTorikesi" runat="server" Width="88px" Text='ビルダー情報' CssClass="lCss"></asp:Label>
                                            </td>
                                            <td class="build" rowspan="4" style="width: 36px; text-align: center">
                                                <asp:Label ID="Label9" runat="server" Width="34px" Text='区分' CssClass="lCss"></asp:Label>
                                            </td>
                                            <td class="zenen" style="text-align: center" colspan="4">
                                                前年データ</td>
                                        </tr>
                                        <tr style="height: 30px;">
                                            <td class="zenen" rowspan="3" style="text-align: center">
                                                <asp:Label ID="Label5" runat="server" Width="73px" Text='工事比率' CssClass="lCss"></asp:Label></td>
                                            <td class="zenen" rowspan="3" style="text-align: center">
                                                <asp:Label ID="Label6" runat="server" Width="50px" Text='分類' CssClass="lCss"></asp:Label></td>
                                            <td class="zenen" rowspan="3" style="text-align: center">
                                                <asp:Label ID="Label7" runat="server" Width="140px" Text='商品' CssClass="lCss"></asp:Label></td>
                                            <td class="zenen" rowspan="3" style="text-align: center">
                                                <asp:Label ID="Label8" runat="server" Width="65px" Text='平均単価' CssClass="lCss"></asp:Label></td>
                                        </tr>
                                        <tr style="height: 15px;">
                                        </tr>
                                        <tr style="height: 15px;">
                                        </tr>
                                    </table>
                                </div>
                            </td>
                            <td style="width: 480px">
                                <div id="divHeadRight" runat="server" style="width: 438px; height: 81px; overflow-y: hidden;
                                    overflow-x: hidden;">
                                    <!---------------------HeadRight----------------------->
                                    <asp:GridView ID="gridviewRight" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        CssClass="gridviewLeft" BorderWidth="1px" ShowHeader="False" CellPadding="0">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KH0" runat="server" Width="73px" Text='<%#Eval("KoujiHiritu0")%>'
                                                        ToolTip='<%#Eval("KoujiHiritu0")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="75px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="ZS0" runat="server" Width="38px" Text='<%#Eval("ZKensuu0")%>' ToolTip='<%#Eval("ZKensuu0")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="ZK0" runat="server" Width="75px" Text='<%#Eval("ZKinkaku0")%>' ToolTip='<%#Eval("ZKinkaku0")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="ZA0" runat="server" Width="138px" Text='<%#Eval("ZArari0")%>' ToolTip='<%#Eval("ZArari0")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KS0" runat="server" Width="38px" Text='<%#Eval("KKensuu0")%>' ToolTip='<%#Eval("KKensuu0")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KK0" runat="server" Width="75px" Text='<%#Eval("KKinkaku0")%>' ToolTip='<%#Eval("KKinkaku0")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KA0" runat="server" Width="138px" Text='<%#Eval("KArari0")%>' ToolTip='<%#Eval("KArari0")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MS0" runat="server" Width="38px" Text='<%#Eval("MKensuu0")%>' ToolTip='<%#Eval("MKensuu0")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MK0" runat="server" Width="75px" Text='<%#Eval("MKinkaku0")%>' ToolTip='<%#Eval("MKinkaku0")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MA0" runat="server" Width="138px" Text='<%#Eval("MArari0")%>' ToolTip='<%#Eval("MArari0")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JS0" runat="server" Width="38px" Text='<%#Eval("JKensuu0")%>' ToolTip='<%#Eval("JKensuu0")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JK0" runat="server" Width="75px" Text='<%#Eval("JKinkaku0")%>' ToolTip='<%#Eval("JKinkaku0")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JA0" runat="server" Width="138px" Text='<%#Eval("JArari0")%>' ToolTip='<%#Eval("JArari0")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JKS0" runat="server" Width="38px" Text='<%#Eval("JKKensuu0")%>' ToolTip='<%#Eval("JKKensuu0")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JKK0" runat="server" Width="75px" Text='<%#Eval("JKKinkaku0")%>' ToolTip='<%#Eval("JKKinkaku0")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JKA0" runat="server" Width="138px" Text='<%#Eval("JKArari0")%>' ToolTip='<%#Eval("JKArari0")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKS0" runat="server" Width="38px" Text='<%#Eval("MKKensuu0")%>' ToolTip='<%#Eval("MKKensuu0")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKK0" runat="server" Width="75px" Text='<%#Eval("MKKinkaku0")%>' ToolTip='<%#Eval("MKKinkaku0")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKA0" runat="server" Width="138px" Text='<%#Eval("MKArari0")%>' ToolTip='<%#Eval("MKArari0")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KH1" runat="server" Width="73px" Text='<%#Eval("KoujiHiritu1")%>'
                                                        ToolTip='<%#Eval("KoujiHiritu1")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="75px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="ZS1" runat="server" Width="38px" Text='<%#Eval("ZKensuu1")%>' ToolTip='<%#Eval("ZKensuu1")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="ZK1" runat="server" Width="75px" Text='<%#Eval("ZKinkaku1")%>' ToolTip='<%#Eval("ZKinkaku1")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="ZA1" runat="server" Width="138px" Text='<%#Eval("ZArari1")%>' ToolTip='<%#Eval("ZArari1")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KS1" runat="server" Width="38px" Text='<%#Eval("KKensuu1")%>' ToolTip='<%#Eval("KKensuu1")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KK1" runat="server" Width="75px" Text='<%#Eval("KKinkaku1")%>' ToolTip='<%#Eval("KKinkaku1")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KA1" runat="server" Width="138px" Text='<%#Eval("KArari1")%>' ToolTip='<%#Eval("KArari1")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MS1" runat="server" Width="38px" Text='<%#Eval("MKensuu1")%>' ToolTip='<%#Eval("MKensuu1")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MK1" runat="server" Width="75px" Text='<%#Eval("MKinkaku1")%>' ToolTip='<%#Eval("MKinkaku1")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MA1" runat="server" Width="138px" Text='<%#Eval("MArari1")%>' ToolTip='<%#Eval("MArari1")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JS1" runat="server" Width="38px" Text='<%#Eval("JKensuu1")%>' ToolTip='<%#Eval("JKensuu1")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JK1" runat="server" Width="75px" Text='<%#Eval("JKinkaku1")%>' ToolTip='<%#Eval("JKinkaku1")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JA1" runat="server" Width="138px" Text='<%#Eval("JArari1")%>' ToolTip='<%#Eval("JArari1")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JKS1" runat="server" Width="38px" Text='<%#Eval("JKKensuu1")%>' ToolTip='<%#Eval("JKKensuu1")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JKK1" runat="server" Width="75px" Text='<%#Eval("JKKinkaku1")%>' ToolTip='<%#Eval("JKKinkaku1")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JKA1" runat="server" Width="138px" Text='<%#Eval("JKArari1")%>' ToolTip='<%#Eval("JKArari1")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKS1" runat="server" Width="38px" Text='<%#Eval("MKKensuu1")%>' ToolTip='<%#Eval("MKKensuu1")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKK1" runat="server" Width="75px" Text='<%#Eval("MKKinkaku1")%>' ToolTip='<%#Eval("MKKinkaku1")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKA1" runat="server" Width="138px" Text='<%#Eval("MKArari1")%>' ToolTip='<%#Eval("MKArari1")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KH2" runat="server" Width="73px" Text='<%#Eval("KoujiHiritu2")%>'
                                                        ToolTip='<%#Eval("KoujiHiritu2")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="75px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="ZS2" runat="server" Width="38px" Text='<%#Eval("ZKensuu2")%>' ToolTip='<%#Eval("ZKensuu2")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="ZK2" runat="server" Width="75px" Text='<%#Eval("ZKinkaku2")%>' ToolTip='<%#Eval("ZKinkaku2")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="ZA2" runat="server" Width="138px" Text='<%#Eval("ZArari2")%>' ToolTip='<%#Eval("ZArari2")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KS2" runat="server" Width="38px" Text='<%#Eval("KKensuu2")%>' ToolTip='<%#Eval("KKensuu2")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KK2" runat="server" Width="75px" Text='<%#Eval("KKinkaku2")%>' ToolTip='<%#Eval("KKinkaku2")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KA2" runat="server" Width="138px" Text='<%#Eval("KArari2")%>' ToolTip='<%#Eval("KArari2")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MS2" runat="server" Width="38px" Text='<%#Eval("MKensuu2")%>' ToolTip='<%#Eval("MKensuu2")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MK2" runat="server" Width="75px" Text='<%#Eval("MKinkaku2")%>' ToolTip='<%#Eval("MKinkaku2")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MA2" runat="server" Width="138px" Text='<%#Eval("MArari2")%>' ToolTip='<%#Eval("MArari2")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JS2" runat="server" Width="38px" Text='<%#Eval("JKensuu2")%>' ToolTip='<%#Eval("JKensuu2")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JK2" runat="server" Width="75px" Text='<%#Eval("JKinkaku2")%>' ToolTip='<%#Eval("JKinkaku2")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JA2" runat="server" Width="138px" Text='<%#Eval("JArari2")%>' ToolTip='<%#Eval("JArari2")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JKS2" runat="server" Width="38px" Text='<%#Eval("JKKensuu2")%>' ToolTip='<%#Eval("JKKensuu2")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JKK2" runat="server" Width="75px" Text='<%#Eval("JKKinkaku2")%>' ToolTip='<%#Eval("JKKinkaku2")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JKA2" runat="server" Width="138px" Text='<%#Eval("JKArari2")%>' ToolTip='<%#Eval("JKArari2")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKS2" runat="server" Width="38px" Text='<%#Eval("MKKensuu2")%>' ToolTip='<%#Eval("MKKensuu2")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKK2" runat="server" Width="75px" Text='<%#Eval("MKKinkaku2")%>' ToolTip='<%#Eval("MKKinkaku2")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKA2" runat="server" Width="138px" Text='<%#Eval("MKArari2")%>' ToolTip='<%#Eval("MKArari2")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KH3" runat="server" Width="73px" Text='<%#Eval("KoujiHiritu3")%>'
                                                        ToolTip='<%#Eval("KoujiHiritu3")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="75px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="ZS3" runat="server" Width="38px" Text='<%#Eval("ZKensuu3")%>' ToolTip='<%#Eval("ZKensuu3")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="ZK3" runat="server" Width="75px" Text='<%#Eval("ZKinkaku3")%>' ToolTip='<%#Eval("ZKinkaku3")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="ZA3" runat="server" Width="138px" Text='<%#Eval("ZArari3")%>' ToolTip='<%#Eval("ZArari3")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KS3" runat="server" Width="38px" Text='<%#Eval("KKensuu3")%>' ToolTip='<%#Eval("KKensuu3")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KK3" runat="server" Width="75px" Text='<%#Eval("KKinkaku3")%>' ToolTip='<%#Eval("KKinkaku3")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KA3" runat="server" Width="138px" Text='<%#Eval("KArari3")%>' ToolTip='<%#Eval("KArari3")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MS3" runat="server" Width="38px" Text='<%#Eval("MKensuu3")%>' ToolTip='<%#Eval("MKensuu3")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MK3" runat="server" Width="75px" Text='<%#Eval("MKinkaku3")%>' ToolTip='<%#Eval("MKinkaku3")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MA3" runat="server" Width="138px" Text='<%#Eval("MArari3")%>' ToolTip='<%#Eval("MArari3")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JS3" runat="server" Width="38px" Text='<%#Eval("JKensuu3")%>' ToolTip='<%#Eval("JKensuu3")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JK3" runat="server" Width="75px" Text='<%#Eval("JKinkaku3")%>' ToolTip='<%#Eval("JKinkaku3")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JA3" runat="server" Width="138px" Text='<%#Eval("JArari3")%>' ToolTip='<%#Eval("JArari3")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JKS3" runat="server" Width="38px" Text='<%#Eval("JKKensuu3")%>' ToolTip='<%#Eval("JKKensuu3")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JKK3" runat="server" Width="75px" Text='<%#Eval("JKKinkaku3")%>' ToolTip='<%#Eval("JKKinkaku3")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JKA3" runat="server" Width="138px" Text='<%#Eval("JKArari3")%>' ToolTip='<%#Eval("JKArari3")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKS3" runat="server" Width="38px" Text='<%#Eval("MKKensuu3")%>' ToolTip='<%#Eval("MKKensuu3")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKK3" runat="server" Width="75px" Text='<%#Eval("MKKinkaku3")%>' ToolTip='<%#Eval("MKKinkaku3")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKA3" runat="server" Width="138px" Text='<%#Eval("MKArari3")%>' ToolTip='<%#Eval("MKArari3")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KH4" runat="server" Width="73px" Text='<%#Eval("KoujiHiritu4")%>'
                                                        ToolTip='<%#Eval("KoujiHiritu4")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="75px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="ZS4" runat="server" Width="38px" Text='<%#Eval("ZKensuu4")%>' ToolTip='<%#Eval("ZKensuu4")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="ZK4" runat="server" Width="75px" Text='<%#Eval("ZKinkaku4")%>' ToolTip='<%#Eval("ZKinkaku4")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="ZA4" runat="server" Width="138px" Text='<%#Eval("ZArari4")%>' ToolTip='<%#Eval("ZArari4")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KS4" runat="server" Width="38px" Text='<%#Eval("KKensuu4")%>' ToolTip='<%#Eval("KKensuu4")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KK4" runat="server" Width="75px" Text='<%#Eval("KKinkaku4")%>' ToolTip='<%#Eval("KKinkaku4")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KA4" runat="server" Width="138px" Text='<%#Eval("KArari4")%>' ToolTip='<%#Eval("KArari4")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MS4" runat="server" Width="38px" Text='<%#Eval("MKensuu4")%>' ToolTip='<%#Eval("MKensuu4")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MK4" runat="server" Width="75px" Text='<%#Eval("MKinkaku4")%>' ToolTip='<%#Eval("MKinkaku4")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MA4" runat="server" Width="138px" Text='<%#Eval("MArari4")%>' ToolTip='<%#Eval("MArari4")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JS4" runat="server" Width="38px" Text='<%#Eval("JKensuu4")%>' ToolTip='<%#Eval("JKensuu4")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JK4" runat="server" Width="75px" Text='<%#Eval("JKinkaku4")%>' ToolTip='<%#Eval("JKinkaku4")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JA4" runat="server" Width="138px" Text='<%#Eval("JArari4")%>' ToolTip='<%#Eval("JArari4")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JKS4" runat="server" Width="38px" Text='<%#Eval("JKKensuu4")%>' ToolTip='<%#Eval("JKKensuu4")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JKK4" runat="server" Width="75px" Text='<%#Eval("JKKinkaku4")%>' ToolTip='<%#Eval("JKKinkaku4")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JKA4" runat="server" Width="138px" Text='<%#Eval("JKArari4")%>' ToolTip='<%#Eval("JKArari4")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKS4" runat="server" Width="38px" Text='<%#Eval("MKKensuu4")%>' ToolTip='<%#Eval("MKKensuu4")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKK4" runat="server" Width="75px" Text='<%#Eval("MKKinkaku4")%>' ToolTip='<%#Eval("MKKinkaku4")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKA4" runat="server" Width="138px" Text='<%#Eval("MKArari4")%>' ToolTip='<%#Eval("MKArari4")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KH5" runat="server" Width="73px" Text='<%#Eval("KoujiHiritu5")%>'
                                                        ToolTip='<%#Eval("KoujiHiritu5")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="75px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="ZS5" runat="server" Width="38px" Text='<%#Eval("ZKensuu5")%>' ToolTip='<%#Eval("ZKensuu5")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="ZK5" runat="server" Width="75px" Text='<%#Eval("ZKinkaku5")%>' ToolTip='<%#Eval("ZKinkaku5")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="ZA5" runat="server" Width="138px" Text='<%#Eval("ZArari5")%>' ToolTip='<%#Eval("ZArari5")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KS5" runat="server" Width="38px" Text='<%#Eval("KKensuu5")%>' ToolTip='<%#Eval("KKensuu5")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KK5" runat="server" Width="75px" Text='<%#Eval("KKinkaku5")%>' ToolTip='<%#Eval("KKinkaku5")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KA5" runat="server" Width="138px" Text='<%#Eval("KArari5")%>' ToolTip='<%#Eval("KArari5")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MS5" runat="server" Width="38px" Text='<%#Eval("MKensuu5")%>' ToolTip='<%#Eval("MKensuu5")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MK5" runat="server" Width="75px" Text='<%#Eval("MKinkaku5")%>' ToolTip='<%#Eval("MKinkaku5")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MA5" runat="server" Width="138px" Text='<%#Eval("MArari5")%>' ToolTip='<%#Eval("MArari5")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JS5" runat="server" Width="38px" Text='<%#Eval("JKensuu5")%>' ToolTip='<%#Eval("JKensuu5")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JK5" runat="server" Width="75px" Text='<%#Eval("JKinkaku5")%>' ToolTip='<%#Eval("JKinkaku5")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JA5" runat="server" Width="138px" Text='<%#Eval("JArari5")%>' ToolTip='<%#Eval("JArari5")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JKS5" runat="server" Width="38px" Text='<%#Eval("JKKensuu5")%>' ToolTip='<%#Eval("JKKensuu5")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JKK5" runat="server" Width="75px" Text='<%#Eval("JKKinkaku5")%>' ToolTip='<%#Eval("JKKinkaku5")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JKA5" runat="server" Width="138px" Text='<%#Eval("JKArari5")%>' ToolTip='<%#Eval("JKArari5")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKS5" runat="server" Width="38px" Text='<%#Eval("MKKensuu5")%>' ToolTip='<%#Eval("MKKensuu5")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKK5" runat="server" Width="75px" Text='<%#Eval("MKKinkaku5")%>' ToolTip='<%#Eval("MKKinkaku5")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKA5" runat="server" Width="138px" Text='<%#Eval("MKArari5")%>' ToolTip='<%#Eval("MKArari5")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KH6" runat="server" Width="73px" Text='<%#Eval("KoujiHiritu6")%>'
                                                        ToolTip='<%#Eval("KoujiHiritu6")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="75px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="ZS6" runat="server" Width="38px" Text='<%#Eval("ZKensuu6")%>' ToolTip='<%#Eval("ZKensuu6")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="ZK6" runat="server" Width="75px" Text='<%#Eval("ZKinkaku6")%>' ToolTip='<%#Eval("ZKinkaku6")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="ZA6" runat="server" Width="138px" Text='<%#Eval("ZArari6")%>' ToolTip='<%#Eval("ZArari6")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KS6" runat="server" Width="38px" Text='<%#Eval("KKensuu6")%>' ToolTip='<%#Eval("KKensuu6")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KK6" runat="server" Width="75px" Text='<%#Eval("KKinkaku6")%>' ToolTip='<%#Eval("KKinkaku6")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KA6" runat="server" Width="138px" Text='<%#Eval("KArari6")%>' ToolTip='<%#Eval("KArari6")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MS6" runat="server" Width="38px" Text='<%#Eval("MKensuu6")%>' ToolTip='<%#Eval("MKensuu6")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MK6" runat="server" Width="75px" Text='<%#Eval("MKinkaku6")%>' ToolTip='<%#Eval("MKinkaku6")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MA6" runat="server" Width="138px" Text='<%#Eval("MArari6")%>' ToolTip='<%#Eval("MArari6")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JS6" runat="server" Width="38px" Text='<%#Eval("JKensuu6")%>' ToolTip='<%#Eval("JKensuu6")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JK6" runat="server" Width="75px" Text='<%#Eval("JKinkaku6")%>' ToolTip='<%#Eval("JKinkaku6")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JA6" runat="server" Width="138px" Text='<%#Eval("JArari6")%>' ToolTip='<%#Eval("JArari6")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JKS6" runat="server" Width="38px" Text='<%#Eval("JKKensuu6")%>' ToolTip='<%#Eval("JKKensuu6")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JKK6" runat="server" Width="75px" Text='<%#Eval("JKKinkaku6")%>' ToolTip='<%#Eval("JKKinkaku6")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JKA6" runat="server" Width="138px" Text='<%#Eval("JKArari6")%>' ToolTip='<%#Eval("JKArari6")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKS6" runat="server" Width="38px" Text='<%#Eval("MKKensuu6")%>' ToolTip='<%#Eval("MKKensuu6")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKK6" runat="server" Width="75px" Text='<%#Eval("MKKinkaku6")%>' ToolTip='<%#Eval("MKKinkaku6")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKA6" runat="server" Width="138px" Text='<%#Eval("MKArari6")%>' ToolTip='<%#Eval("MKArari6")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KH7" runat="server" Width="73px" Text='<%#Eval("KoujiHiritu7")%>'
                                                        ToolTip='<%#Eval("KoujiHiritu7")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="75px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="ZS7" runat="server" Width="38px" Text='<%#Eval("ZKensuu7")%>' ToolTip='<%#Eval("ZKensuu7")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="ZK7" runat="server" Width="75px" Text='<%#Eval("ZKinkaku7")%>' ToolTip='<%#Eval("ZKinkaku7")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="ZA7" runat="server" Width="138px" Text='<%#Eval("ZArari7")%>' ToolTip='<%#Eval("ZArari7")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KS7" runat="server" Width="38px" Text='<%#Eval("KKensuu7")%>' ToolTip='<%#Eval("KKensuu7")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KK7" runat="server" Width="75px" Text='<%#Eval("KKinkaku7")%>' ToolTip='<%#Eval("KKinkaku7")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KA7" runat="server" Width="138px" Text='<%#Eval("KArari7")%>' ToolTip='<%#Eval("KArari7")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MS7" runat="server" Width="38px" Text='<%#Eval("MKensuu7")%>' ToolTip='<%#Eval("MKensuu7")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MK7" runat="server" Width="75px" Text='<%#Eval("MKinkaku7")%>' ToolTip='<%#Eval("MKinkaku7")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MA7" runat="server" Width="138px" Text='<%#Eval("MArari7")%>' ToolTip='<%#Eval("MArari7")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JS7" runat="server" Width="38px" Text='<%#Eval("JKensuu7")%>' ToolTip='<%#Eval("JKensuu7")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JK7" runat="server" Width="75px" Text='<%#Eval("JKinkaku7")%>' ToolTip='<%#Eval("JKinkaku7")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JA7" runat="server" Width="138px" Text='<%#Eval("JArari7")%>' ToolTip='<%#Eval("JArari7")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JKS7" runat="server" Width="38px" Text='<%#Eval("JKKensuu7")%>' ToolTip='<%#Eval("JKKensuu7")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JKK7" runat="server" Width="75px" Text='<%#Eval("JKKinkaku7")%>' ToolTip='<%#Eval("JKKinkaku7")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JKA7" runat="server" Width="138px" Text='<%#Eval("JKArari7")%>' ToolTip='<%#Eval("JKArari7")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKS7" runat="server" Width="38px" Text='<%#Eval("MKKensuu7")%>' ToolTip='<%#Eval("MKKensuu7")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKK7" runat="server" Width="75px" Text='<%#Eval("MKKinkaku7")%>' ToolTip='<%#Eval("MKKinkaku7")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKA7" runat="server" Width="138px" Text='<%#Eval("MKArari7")%>' ToolTip='<%#Eval("MKArari7")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KH8" runat="server" Width="73px" Text='<%#Eval("KoujiHiritu8")%>'
                                                        ToolTip='<%#Eval("KoujiHiritu8")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="75px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="ZS8" runat="server" Width="38px" Text='<%#Eval("ZKensuu8")%>' ToolTip='<%#Eval("ZKensuu8")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="ZK8" runat="server" Width="75px" Text='<%#Eval("ZKinkaku8")%>' ToolTip='<%#Eval("ZKinkaku8")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="ZA8" runat="server" Width="138px" Text='<%#Eval("ZArari8")%>' ToolTip='<%#Eval("ZArari8")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KS8" runat="server" Width="38px" Text='<%#Eval("KKensuu8")%>' ToolTip='<%#Eval("KKensuu8")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KK8" runat="server" Width="75px" Text='<%#Eval("KKinkaku8")%>' ToolTip='<%#Eval("KKinkaku8")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KA8" runat="server" Width="138px" Text='<%#Eval("KArari8")%>' ToolTip='<%#Eval("KArari8")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MS8" runat="server" Width="38px" Text='<%#Eval("MKensuu8")%>' ToolTip='<%#Eval("MKensuu8")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MK8" runat="server" Width="75px" Text='<%#Eval("MKinkaku8")%>' ToolTip='<%#Eval("MKinkaku8")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MA8" runat="server" Width="138px" Text='<%#Eval("MArari8")%>' ToolTip='<%#Eval("MArari8")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JS8" runat="server" Width="38px" Text='<%#Eval("JKensuu8")%>' ToolTip='<%#Eval("JKensuu8")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JK8" runat="server" Width="75px" Text='<%#Eval("JKinkaku8")%>' ToolTip='<%#Eval("JKinkaku8")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JA8" runat="server" Width="138px" Text='<%#Eval("JArari8")%>' ToolTip='<%#Eval("JArari8")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JKS8" runat="server" Width="38px" Text='<%#Eval("JKKensuu8")%>' ToolTip='<%#Eval("JKKensuu8")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JKK8" runat="server" Width="75px" Text='<%#Eval("JKKinkaku8")%>' ToolTip='<%#Eval("JKKinkaku8")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JKA8" runat="server" Width="138px" Text='<%#Eval("JKArari8")%>' ToolTip='<%#Eval("JKArari8")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKS8" runat="server" Width="38px" Text='<%#Eval("MKKensuu8")%>' ToolTip='<%#Eval("MKKensuu8")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKK8" runat="server" Width="75px" Text='<%#Eval("MKKinkaku8")%>' ToolTip='<%#Eval("MKKinkaku8")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKA8" runat="server" Width="138px" Text='<%#Eval("MKArari8")%>' ToolTip='<%#Eval("MKArari8")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KH9" runat="server" Width="73px" Text='<%#Eval("KoujiHiritu9")%>'
                                                        ToolTip='<%#Eval("KoujiHiritu9")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="75px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="ZS9" runat="server" Width="38px" Text='<%#Eval("ZKensuu9")%>' ToolTip='<%#Eval("ZKensuu9")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="ZK9" runat="server" Width="75px" Text='<%#Eval("ZKinkaku9")%>' ToolTip='<%#Eval("ZKinkaku9")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="ZA9" runat="server" Width="138px" Text='<%#Eval("ZArari9")%>' ToolTip='<%#Eval("ZArari9")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KS9" runat="server" Width="38px" Text='<%#Eval("KKensuu9")%>' ToolTip='<%#Eval("KKensuu9")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KK9" runat="server" Width="75px" Text='<%#Eval("KKinkaku9")%>' ToolTip='<%#Eval("KKinkaku9")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KA9" runat="server" Width="138px" Text='<%#Eval("KArari9")%>' ToolTip='<%#Eval("KArari9")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MS9" runat="server" Width="38px" Text='<%#Eval("MKensuu9")%>' ToolTip='<%#Eval("MKensuu9")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MK9" runat="server" Width="75px" Text='<%#Eval("MKinkaku9")%>' ToolTip='<%#Eval("MKinkaku9")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MA9" runat="server" Width="138px" Text='<%#Eval("MArari9")%>' ToolTip='<%#Eval("MArari9")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JS9" runat="server" Width="38px" Text='<%#Eval("JKensuu9")%>' ToolTip='<%#Eval("JKensuu9")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JK9" runat="server" Width="75px" Text='<%#Eval("JKinkaku9")%>' ToolTip='<%#Eval("JKinkaku9")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JA9" runat="server" Width="138px" Text='<%#Eval("JArari9")%>' ToolTip='<%#Eval("JArari9")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JKS9" runat="server" Width="38px" Text='<%#Eval("JKKensuu9")%>' ToolTip='<%#Eval("JKKensuu9")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JKK9" runat="server" Width="75px" Text='<%#Eval("JKKinkaku9")%>' ToolTip='<%#Eval("JKKinkaku9")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JKA9" runat="server" Width="138px" Text='<%#Eval("JKArari9")%>' ToolTip='<%#Eval("JKArari9")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKS9" runat="server" Width="38px" Text='<%#Eval("MKKensuu9")%>' ToolTip='<%#Eval("MKKensuu9")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKK9" runat="server" Width="75px" Text='<%#Eval("MKKinkaku9")%>' ToolTip='<%#Eval("MKKinkaku9")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKA9" runat="server" Width="138px" Text='<%#Eval("MKArari9")%>' ToolTip='<%#Eval("MKArari9")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KH10" runat="server" Width="73px" Text='<%#Eval("KoujiHiritu10")%>'
                                                        ToolTip='<%#Eval("KoujiHiritu10")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="75px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="ZS10" runat="server" Width="38px" Text='<%#Eval("ZKensuu10")%>' ToolTip='<%#Eval("ZKensuu10")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="ZK10" runat="server" Width="75px" Text='<%#Eval("ZKinkaku10")%>' ToolTip='<%#Eval("ZKinkaku10")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="ZA10" runat="server" Width="138px" Text='<%#Eval("ZArari10")%>' ToolTip='<%#Eval("ZArari10")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KS10" runat="server" Width="38px" Text='<%#Eval("KKensuu10")%>' ToolTip='<%#Eval("KKensuu10")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KK10" runat="server" Width="75px" Text='<%#Eval("KKinkaku10")%>' ToolTip='<%#Eval("KKinkaku10")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KA10" runat="server" Width="138px" Text='<%#Eval("KArari10")%>' ToolTip='<%#Eval("KArari10")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MS10" runat="server" Width="38px" Text='<%#Eval("MKensuu10")%>' ToolTip='<%#Eval("MKensuu10")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MK10" runat="server" Width="75px" Text='<%#Eval("MKinkaku10")%>' ToolTip='<%#Eval("MKinkaku10")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MA10" runat="server" Width="138px" Text='<%#Eval("MArari10")%>' ToolTip='<%#Eval("MArari10")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JS10" runat="server" Width="38px" Text='<%#Eval("JKensuu10")%>' ToolTip='<%#Eval("JKensuu10")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JK10" runat="server" Width="75px" Text='<%#Eval("JKinkaku10")%>' ToolTip='<%#Eval("JKinkaku10")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JA10" runat="server" Width="138px" Text='<%#Eval("JArari10")%>' ToolTip='<%#Eval("JArari10")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JKS10" runat="server" Width="38px" Text='<%#Eval("JKKensuu10")%>'
                                                        ToolTip='<%#Eval("JKKensuu10")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JKK10" runat="server" Width="75px" Text='<%#Eval("JKKinkaku10")%>'
                                                        ToolTip='<%#Eval("JKKinkaku10")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JKA10" runat="server" Width="138px" Text='<%#Eval("JKArari10")%>'
                                                        ToolTip='<%#Eval("JKArari10")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKS10" runat="server" Width="38px" Text='<%#Eval("MKKensuu10")%>'
                                                        ToolTip='<%#Eval("MKKensuu10")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKK10" runat="server" Width="75px" Text='<%#Eval("MKKinkaku10")%>'
                                                        ToolTip='<%#Eval("MKKinkaku10")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKA10" runat="server" Width="138px" Text='<%#Eval("MKArari10")%>'
                                                        ToolTip='<%#Eval("MKArari10")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KH11" runat="server" Width="73px" Text='<%#Eval("KoujiHiritu11")%>'
                                                        ToolTip='<%#Eval("KoujiHiritu11")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="75px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="ZS11" runat="server" Width="38px" Text='<%#Eval("ZKensuu11")%>' ToolTip='<%#Eval("ZKensuu11")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="ZK11" runat="server" Width="75px" Text='<%#Eval("ZKinkaku11")%>' ToolTip='<%#Eval("ZKinkaku11")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="ZA11" runat="server" Width="138px" Text='<%#Eval("ZArari11")%>' ToolTip='<%#Eval("ZArari11")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KS11" runat="server" Width="38px" Text='<%#Eval("KKensuu11")%>' ToolTip='<%#Eval("KKensuu11")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KK11" runat="server" Width="75px" Text='<%#Eval("KKinkaku11")%>' ToolTip='<%#Eval("KKinkaku11")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KA11" runat="server" Width="138px" Text='<%#Eval("KArari11")%>' ToolTip='<%#Eval("KArari11")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MS11" runat="server" Width="38px" Text='<%#Eval("MKensuu11")%>' ToolTip='<%#Eval("MKensuu11")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MK11" runat="server" Width="75px" Text='<%#Eval("MKinkaku11")%>' ToolTip='<%#Eval("MKinkaku11")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MA11" runat="server" Width="138px" Text='<%#Eval("MArari11")%>' ToolTip='<%#Eval("MArari11")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JS11" runat="server" Width="38px" Text='<%#Eval("JKensuu11")%>' ToolTip='<%#Eval("JKensuu11")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JK11" runat="server" Width="75px" Text='<%#Eval("JKinkaku11")%>' ToolTip='<%#Eval("JKinkaku11")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JA11" runat="server" Width="138px" Text='<%#Eval("JArari11")%>' ToolTip='<%#Eval("JArari11")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JKS11" runat="server" Width="38px" Text='<%#Eval("JKKensuu11")%>'
                                                        ToolTip='<%#Eval("JKKensuu11")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JKK11" runat="server" Width="75px" Text='<%#Eval("JKKinkaku11")%>'
                                                        ToolTip='<%#Eval("JKKinkaku11")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JKA11" runat="server" Width="138px" Text='<%#Eval("JKArari11")%>'
                                                        ToolTip='<%#Eval("JKArari11")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKS11" runat="server" Width="38px" Text='<%#Eval("MKKensuu11")%>'
                                                        ToolTip='<%#Eval("MKKensuu11")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKK11" runat="server" Width="75px" Text='<%#Eval("MKKinkaku11")%>'
                                                        ToolTip='<%#Eval("MKKinkaku11")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKA11" runat="server" Width="138px" Text='<%#Eval("MKArari11")%>'
                                                        ToolTip='<%#Eval("MKArari11")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="ZS12" runat="server" Width="38px" Text='<%#Eval("ZKensuu12")%>' ToolTip='<%#Eval("ZKensuu12")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="ZK12" runat="server" Width="75px" Text='<%#Eval("ZKinkaku12")%>' ToolTip='<%#Eval("ZKinkaku12")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="ZA12" runat="server" Width="138px" Text='<%#Eval("ZArari12")%>' ToolTip='<%#Eval("ZArari12")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KS12" runat="server" Width="38px" Text='<%#Eval("KKensuu12")%>' ToolTip='<%#Eval("KKensuu12")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KK12" runat="server" Width="75px" Text='<%#Eval("KKinkaku12")%>' ToolTip='<%#Eval("KKinkaku12")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KA12" runat="server" Width="138px" Text='<%#Eval("KArari12")%>' ToolTip='<%#Eval("KArari12")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MS12" runat="server" Width="38px" Text='<%#Eval("MKensuu12")%>' ToolTip='<%#Eval("MKensuu12")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MK12" runat="server" Width="75px" Text='<%#Eval("MKinkaku12")%>' ToolTip='<%#Eval("MKinkaku12")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MA12" runat="server" Width="138px" Text='<%#Eval("MArari12")%>' ToolTip='<%#Eval("MArari12")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JS12" runat="server" Width="38px" Text='<%#Eval("JKensuu12")%>' ToolTip='<%#Eval("JKensuu12")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JK12" runat="server" Width="75px" Text='<%#Eval("JKinkaku12")%>' ToolTip='<%#Eval("JKinkaku12")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JA12" runat="server" Width="138px" Text='<%#Eval("JArari12")%>' ToolTip='<%#Eval("JArari12")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JKS12" runat="server" Width="38px" Text='<%#Eval("JKKensuu12")%>'
                                                        ToolTip='<%#Eval("JKKensuu12")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JKK12" runat="server" Width="75px" Text='<%#Eval("JKKinkaku12")%>'
                                                        ToolTip='<%#Eval("JKKinkaku12")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="JKA12" runat="server" Width="138px" Text='<%#Eval("JKArari12")%>'
                                                        ToolTip='<%#Eval("JKArari12")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKS12" runat="server" Width="38px" Text='<%#Eval("MKKensuu12")%>'
                                                        ToolTip='<%#Eval("MKKensuu12")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKK12" runat="server" Width="75px" Text='<%#Eval("MKKinkaku12")%>'
                                                        ToolTip='<%#Eval("MKKinkaku12")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKA12" runat="server" Width="138px" Text='<%#Eval("MKArari12")%>'
                                                        ToolTip='<%#Eval("MKArari12")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 125px; vertical-align: top;">
                                <div id="divBodyLeft" onmousewheel="wheel();" runat="server" style="width: 473px;
                                    height: 161px; overflow-y: hidden; overflow-x: hidden;">
                                    <asp:GridView ID="grdItiranLeft" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        CssClass="gridviewLeft" BorderWidth="1px" ShowHeader="False" CellPadding="0">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="BD" runat="server" Width="88px" Text='<%#Eval("Build")%>' ToolTip='<%#Eval("Build")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="90px" CssClass="build" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KB" runat="server" Width="34px" Text='<%#Eval("kbn")%>' ToolTip='<%#Eval("kbn")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="36px" CssClass="build" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="KH" runat="server" Width="71px" Text='<%#Eval("KoujiHiritu")%>' ToolTip='<%#Eval("KoujiHiritu")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="73px" HorizontalAlign="Left" CssClass="zenen" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="EK" runat="server" Width="48px" Text='<%#Eval("EigyouKbn")%>' ToolTip='<%#Eval("EigyouKbn")%>'
                                                        CssClass="lCss" Style="white-space: nowrap; overflow: hidden;"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="50px" HorizontalAlign="Left" CssClass="zenen" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="SH" runat="server" Width="138px" Text='<%#Eval("Syouhin")%>' ToolTip='<%#Eval("Syouhin")%>'
                                                        CssClass="lCss"></asp:Label><asp:HiddenField ID="hidT" Value='<%#Eval("DataType")%>'
                                                            runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="140px" HorizontalAlign="Left" CssClass="zenen" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="HT" runat="server" Width="63px" Text='<%#Eval("HeikinTanka")%>' ToolTip='<%#Eval("HeikinTanka")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="65px" HorizontalAlign="Left" CssClass="zenen" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                            <td style="height: 125px; vertical-align: top;">
                                <div id="divBodyRight" runat="server" onmousewheel="wheel();" style="width: 438px;
                                    height: 161px; overflow-y: hidden; overflow-x: hidden;">
                                    <!--------------------BodyRight----------------------->
                                    <asp:GridView ID="grdItiranRight" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        CssClass="gridviewLeft" BorderWidth="1px" ShowHeader="False" CellPadding="0">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKH0" runat="server" Width="73px" Text='<%#Eval("KoujiHiritu0")%>'
                                                        ToolTip='<%#Eval("KoujiHiritu0")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="75px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MZS0" runat="server" Width="38px" Text='<%#Eval("ZKensuu0")%>' ToolTip='<%#Eval("ZKensuu0")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MZK0" runat="server" Width="75px" Text='<%#Eval("ZKinkaku0")%>' ToolTip='<%#Eval("ZKinkaku0")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MZA0" runat="server" Width="138px" Text='<%#Eval("ZArari0")%>' ToolTip='<%#Eval("ZArari0")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKS0" runat="server" Width="38px" Text='<%#Eval("KKensuu0")%>' ToolTip='<%#Eval("KKensuu0")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKK0" runat="server" Width="75px" Text='<%#Eval("KKinkaku0")%>' ToolTip='<%#Eval("KKinkaku0")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKA0" runat="server" Width="138px" Text='<%#Eval("KArari0")%>' ToolTip='<%#Eval("KArari0")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMS0" runat="server" Width="38px" Text='<%#Eval("MKensuu0")%>' ToolTip='<%#Eval("MKensuu0")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMK0" runat="server" Width="75px" Text='<%#Eval("MKinkaku0")%>' ToolTip='<%#Eval("MKinkaku0")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMA0" runat="server" Width="138px" Text='<%#Eval("MArari0")%>' ToolTip='<%#Eval("MArari0")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJS0" runat="server" Width="38px" Text='<%#Eval("JKensuu0")%>' ToolTip='<%#Eval("JKensuu0")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJK0" runat="server" Width="75px" Text='<%#Eval("JKinkaku0")%>' ToolTip='<%#Eval("JKinkaku0")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJA0" runat="server" Width="138px" Text='<%#Eval("JArari0")%>' ToolTip='<%#Eval("JArari0")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJKS0" runat="server" Width="38px" Text='<%#Eval("JKKensuu0")%>' ToolTip='<%#Eval("JKKensuu0")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJKK0" runat="server" Width="75px" Text='<%#Eval("JKKinkaku0")%>'
                                                        ToolTip='<%#Eval("JKKinkaku0")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJKA0" runat="server" Width="138px" Text='<%#Eval("JKArari0")%>' ToolTip='<%#Eval("JKArari0")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMKS0" runat="server" Width="38px" Text='<%#Eval("MKKensuu0")%>' ToolTip='<%#Eval("MKKensuu0")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMKK0" runat="server" Width="75px" Text='<%#Eval("MKKinkaku0")%>'
                                                        ToolTip='<%#Eval("MKKinkaku0")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMKA0" runat="server" Width="138px" Text='<%#Eval("MKArari0")%>' ToolTip='<%#Eval("MKArari0")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKH1" runat="server" Width="73px" Text='<%#Eval("KoujiHiritu1")%>'
                                                        ToolTip='<%#Eval("KoujiHiritu1")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="75px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MZS1" runat="server" Width="38px" Text='<%#Eval("ZKensuu1")%>' ToolTip='<%#Eval("ZKensuu1")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MZK1" runat="server" Width="75px" Text='<%#Eval("ZKinkaku1")%>' ToolTip='<%#Eval("ZKinkaku1")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MZA1" runat="server" Width="138px" Text='<%#Eval("ZArari1")%>' ToolTip='<%#Eval("ZArari1")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKS1" runat="server" Width="38px" Text='<%#Eval("KKensuu1")%>' ToolTip='<%#Eval("KKensuu1")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKK1" runat="server" Width="75px" Text='<%#Eval("KKinkaku1")%>' ToolTip='<%#Eval("KKinkaku1")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKA1" runat="server" Width="138px" Text='<%#Eval("KArari1")%>' ToolTip='<%#Eval("KArari1")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMS1" runat="server" Width="38px" Text='<%#Eval("MKensuu1")%>' ToolTip='<%#Eval("MKensuu1")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMK1" runat="server" Width="75px" Text='<%#Eval("MKinkaku1")%>' ToolTip='<%#Eval("MKinkaku1")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMA1" runat="server" Width="138px" Text='<%#Eval("MArari1")%>' ToolTip='<%#Eval("MArari1")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJS1" runat="server" Width="38px" Text='<%#Eval("JKensuu1")%>' ToolTip='<%#Eval("JKensuu1")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJK1" runat="server" Width="75px" Text='<%#Eval("JKinkaku1")%>' ToolTip='<%#Eval("JKinkaku1")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJA1" runat="server" Width="138px" Text='<%#Eval("JArari1")%>' ToolTip='<%#Eval("JArari1")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJKS1" runat="server" Width="38px" Text='<%#Eval("JKKensuu1")%>' ToolTip='<%#Eval("JKKensuu1")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJKK1" runat="server" Width="75px" Text='<%#Eval("JKKinkaku1")%>'
                                                        ToolTip='<%#Eval("JKKinkaku1")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJKA1" runat="server" Width="138px" Text='<%#Eval("JKArari1")%>' ToolTip='<%#Eval("JKArari1")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMKS1" runat="server" Width="38px" Text='<%#Eval("MKKensuu1")%>' ToolTip='<%#Eval("MKKensuu1")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMKK1" runat="server" Width="75px" Text='<%#Eval("MKKinkaku1")%>'
                                                        ToolTip='<%#Eval("MKKinkaku1")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMKA1" runat="server" Width="138px" Text='<%#Eval("MKArari1")%>' ToolTip='<%#Eval("MKArari1")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKH2" runat="server" Width="73px" Text='<%#Eval("KoujiHiritu2")%>'
                                                        ToolTip='<%#Eval("KoujiHiritu2")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="75px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MZS2" runat="server" Width="38px" Text='<%#Eval("ZKensuu2")%>' ToolTip='<%#Eval("ZKensuu2")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MZK2" runat="server" Width="75px" Text='<%#Eval("ZKinkaku2")%>' ToolTip='<%#Eval("ZKinkaku2")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MZA2" runat="server" Width="138px" Text='<%#Eval("ZArari2")%>' ToolTip='<%#Eval("ZArari2")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKS2" runat="server" Width="38px" Text='<%#Eval("KKensuu2")%>' ToolTip='<%#Eval("KKensuu2")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKK2" runat="server" Width="75px" Text='<%#Eval("KKinkaku2")%>' ToolTip='<%#Eval("KKinkaku2")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKA2" runat="server" Width="138px" Text='<%#Eval("KArari2")%>' ToolTip='<%#Eval("KArari2")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMS2" runat="server" Width="38px" Text='<%#Eval("MKensuu2")%>' ToolTip='<%#Eval("MKensuu2")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMK2" runat="server" Width="75px" Text='<%#Eval("MKinkaku2")%>' ToolTip='<%#Eval("MKinkaku2")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMA2" runat="server" Width="138px" Text='<%#Eval("MArari2")%>' ToolTip='<%#Eval("MArari2")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJS2" runat="server" Width="38px" Text='<%#Eval("JKensuu2")%>' ToolTip='<%#Eval("JKensuu2")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJK2" runat="server" Width="75px" Text='<%#Eval("JKinkaku2")%>' ToolTip='<%#Eval("JKinkaku2")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJA2" runat="server" Width="138px" Text='<%#Eval("JArari2")%>' ToolTip='<%#Eval("JArari2")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJKS2" runat="server" Width="38px" Text='<%#Eval("JKKensuu2")%>' ToolTip='<%#Eval("JKKensuu2")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJKK2" runat="server" Width="75px" Text='<%#Eval("JKKinkaku2")%>'
                                                        ToolTip='<%#Eval("JKKinkaku2")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJKA2" runat="server" Width="138px" Text='<%#Eval("JKArari2")%>' ToolTip='<%#Eval("JKArari2")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMKS2" runat="server" Width="38px" Text='<%#Eval("MKKensuu2")%>' ToolTip='<%#Eval("MKKensuu2")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMKK2" runat="server" Width="75px" Text='<%#Eval("MKKinkaku2")%>'
                                                        ToolTip='<%#Eval("MKKinkaku2")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMKA2" runat="server" Width="138px" Text='<%#Eval("MKArari2")%>' ToolTip='<%#Eval("MKArari2")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKH3" runat="server" Width="73px" Text='<%#Eval("KoujiHiritu3")%>'
                                                        ToolTip='<%#Eval("KoujiHiritu3")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="75px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MZS3" runat="server" Width="38px" Text='<%#Eval("ZKensuu3")%>' ToolTip='<%#Eval("ZKensuu3")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MZK3" runat="server" Width="75px" Text='<%#Eval("ZKinkaku3")%>' ToolTip='<%#Eval("ZKinkaku3")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MZA3" runat="server" Width="138px" Text='<%#Eval("ZArari3")%>' ToolTip='<%#Eval("ZArari3")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKS3" runat="server" Width="38px" Text='<%#Eval("KKensuu3")%>' ToolTip='<%#Eval("KKensuu3")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKK3" runat="server" Width="75px" Text='<%#Eval("KKinkaku3")%>' ToolTip='<%#Eval("KKinkaku3")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKA3" runat="server" Width="138px" Text='<%#Eval("KArari3")%>' ToolTip='<%#Eval("KArari3")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMS3" runat="server" Width="38px" Text='<%#Eval("MKensuu3")%>' ToolTip='<%#Eval("MKensuu3")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMK3" runat="server" Width="75px" Text='<%#Eval("MKinkaku3")%>' ToolTip='<%#Eval("MKinkaku3")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMA3" runat="server" Width="138px" Text='<%#Eval("MArari3")%>' ToolTip='<%#Eval("MArari3")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJS3" runat="server" Width="38px" Text='<%#Eval("JKensuu3")%>' ToolTip='<%#Eval("JKensuu3")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJK3" runat="server" Width="75px" Text='<%#Eval("JKinkaku3")%>' ToolTip='<%#Eval("JKinkaku3")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJA3" runat="server" Width="138px" Text='<%#Eval("JArari3")%>' ToolTip='<%#Eval("JArari3")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJKS3" runat="server" Width="38px" Text='<%#Eval("JKKensuu3")%>' ToolTip='<%#Eval("JKKensuu3")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJKK3" runat="server" Width="75px" Text='<%#Eval("JKKinkaku3")%>'
                                                        ToolTip='<%#Eval("JKKinkaku3")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJKA3" runat="server" Width="138px" Text='<%#Eval("JKArari3")%>' ToolTip='<%#Eval("JKArari3")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMKS3" runat="server" Width="38px" Text='<%#Eval("MKKensuu3")%>' ToolTip='<%#Eval("MKKensuu3")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMKK3" runat="server" Width="75px" Text='<%#Eval("MKKinkaku3")%>'
                                                        ToolTip='<%#Eval("MKKinkaku3")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMKA3" runat="server" Width="138px" Text='<%#Eval("MKArari3")%>' ToolTip='<%#Eval("MKArari3")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKH4" runat="server" Width="73px" Text='<%#Eval("KoujiHiritu4")%>'
                                                        ToolTip='<%#Eval("KoujiHiritu4")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="75px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MZS4" runat="server" Width="38px" Text='<%#Eval("ZKensuu4")%>' ToolTip='<%#Eval("ZKensuu4")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MZK4" runat="server" Width="75px" Text='<%#Eval("ZKinkaku4")%>' ToolTip='<%#Eval("ZKinkaku4")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MZA4" runat="server" Width="138px" Text='<%#Eval("ZArari4")%>' ToolTip='<%#Eval("ZArari4")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKS4" runat="server" Width="38px" Text='<%#Eval("KKensuu4")%>' ToolTip='<%#Eval("KKensuu4")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKK4" runat="server" Width="75px" Text='<%#Eval("KKinkaku4")%>' ToolTip='<%#Eval("KKinkaku4")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKA4" runat="server" Width="138px" Text='<%#Eval("KArari4")%>' ToolTip='<%#Eval("KArari4")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMS4" runat="server" Width="38px" Text='<%#Eval("MKensuu4")%>' ToolTip='<%#Eval("MKensuu4")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMK4" runat="server" Width="75px" Text='<%#Eval("MKinkaku4")%>' ToolTip='<%#Eval("MKinkaku4")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMA4" runat="server" Width="138px" Text='<%#Eval("MArari4")%>' ToolTip='<%#Eval("MArari4")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJS4" runat="server" Width="38px" Text='<%#Eval("JKensuu4")%>' ToolTip='<%#Eval("JKensuu4")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJK4" runat="server" Width="75px" Text='<%#Eval("JKinkaku4")%>' ToolTip='<%#Eval("JKinkaku4")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJA4" runat="server" Width="138px" Text='<%#Eval("JArari4")%>' ToolTip='<%#Eval("JArari4")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJKS4" runat="server" Width="38px" Text='<%#Eval("JKKensuu4")%>' ToolTip='<%#Eval("JKKensuu4")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJKK4" runat="server" Width="75px" Text='<%#Eval("JKKinkaku4")%>'
                                                        ToolTip='<%#Eval("JKKinkaku4")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJKA4" runat="server" Width="138px" Text='<%#Eval("JKArari4")%>' ToolTip='<%#Eval("JKArari4")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMKS4" runat="server" Width="38px" Text='<%#Eval("MKKensuu4")%>' ToolTip='<%#Eval("MKKensuu4")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMKK4" runat="server" Width="75px" Text='<%#Eval("MKKinkaku4")%>'
                                                        ToolTip='<%#Eval("MKKinkaku4")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMKA4" runat="server" Width="138px" Text='<%#Eval("MKArari4")%>' ToolTip='<%#Eval("MKArari4")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKH5" runat="server" Width="73px" Text='<%#Eval("KoujiHiritu5")%>'
                                                        ToolTip='<%#Eval("KoujiHiritu5")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="75px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MZS5" runat="server" Width="38px" Text='<%#Eval("ZKensuu5")%>' ToolTip='<%#Eval("ZKensuu5")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MZK5" runat="server" Width="75px" Text='<%#Eval("ZKinkaku5")%>' ToolTip='<%#Eval("ZKinkaku5")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MZA5" runat="server" Width="138px" Text='<%#Eval("ZArari5")%>' ToolTip='<%#Eval("ZArari5")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKS5" runat="server" Width="38px" Text='<%#Eval("KKensuu5")%>' ToolTip='<%#Eval("KKensuu5")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKK5" runat="server" Width="75px" Text='<%#Eval("KKinkaku5")%>' ToolTip='<%#Eval("KKinkaku5")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKA5" runat="server" Width="138px" Text='<%#Eval("KArari5")%>' ToolTip='<%#Eval("KArari5")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMS5" runat="server" Width="38px" Text='<%#Eval("MKensuu5")%>' ToolTip='<%#Eval("MKensuu5")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMK5" runat="server" Width="75px" Text='<%#Eval("MKinkaku5")%>' ToolTip='<%#Eval("MKinkaku5")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMA5" runat="server" Width="138px" Text='<%#Eval("MArari5")%>' ToolTip='<%#Eval("MArari5")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJS5" runat="server" Width="38px" Text='<%#Eval("JKensuu5")%>' ToolTip='<%#Eval("JKensuu5")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJK5" runat="server" Width="75px" Text='<%#Eval("JKinkaku5")%>' ToolTip='<%#Eval("JKinkaku5")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJA5" runat="server" Width="138px" Text='<%#Eval("JArari5")%>' ToolTip='<%#Eval("JArari5")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJKS5" runat="server" Width="38px" Text='<%#Eval("JKKensuu5")%>' ToolTip='<%#Eval("JKKensuu5")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJKK5" runat="server" Width="75px" Text='<%#Eval("JKKinkaku5")%>'
                                                        ToolTip='<%#Eval("JKKinkaku5")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJKA5" runat="server" Width="138px" Text='<%#Eval("JKArari5")%>' ToolTip='<%#Eval("JKArari5")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMKS5" runat="server" Width="38px" Text='<%#Eval("MKKensuu5")%>' ToolTip='<%#Eval("MKKensuu5")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMKK5" runat="server" Width="75px" Text='<%#Eval("MKKinkaku5")%>'
                                                        ToolTip='<%#Eval("MKKinkaku5")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMKA5" runat="server" Width="138px" Text='<%#Eval("MKArari5")%>' ToolTip='<%#Eval("MKArari5")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKH6" runat="server" Width="73px" Text='<%#Eval("KoujiHiritu6")%>'
                                                        ToolTip='<%#Eval("KoujiHiritu6")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="75px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MZS6" runat="server" Width="38px" Text='<%#Eval("ZKensuu6")%>' ToolTip='<%#Eval("ZKensuu6")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MZK6" runat="server" Width="75px" Text='<%#Eval("ZKinkaku6")%>' ToolTip='<%#Eval("ZKinkaku6")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MZA6" runat="server" Width="138px" Text='<%#Eval("ZArari6")%>' ToolTip='<%#Eval("ZArari6")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKS6" runat="server" Width="38px" Text='<%#Eval("KKensuu6")%>' ToolTip='<%#Eval("KKensuu6")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKK6" runat="server" Width="75px" Text='<%#Eval("KKinkaku6")%>' ToolTip='<%#Eval("KKinkaku6")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKA6" runat="server" Width="138px" Text='<%#Eval("KArari6")%>' ToolTip='<%#Eval("KArari6")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMS6" runat="server" Width="38px" Text='<%#Eval("MKensuu6")%>' ToolTip='<%#Eval("MKensuu6")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMK6" runat="server" Width="75px" Text='<%#Eval("MKinkaku6")%>' ToolTip='<%#Eval("MKinkaku6")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMA6" runat="server" Width="138px" Text='<%#Eval("MArari6")%>' ToolTip='<%#Eval("MArari6")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJS6" runat="server" Width="38px" Text='<%#Eval("JKensuu6")%>' ToolTip='<%#Eval("JKensuu6")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJK6" runat="server" Width="75px" Text='<%#Eval("JKinkaku6")%>' ToolTip='<%#Eval("JKinkaku6")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJA6" runat="server" Width="138px" Text='<%#Eval("JArari6")%>' ToolTip='<%#Eval("JArari6")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJKS6" runat="server" Width="38px" Text='<%#Eval("JKKensuu6")%>' ToolTip='<%#Eval("JKKensuu6")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJKK6" runat="server" Width="75px" Text='<%#Eval("JKKinkaku6")%>'
                                                        ToolTip='<%#Eval("JKKinkaku6")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJKA6" runat="server" Width="138px" Text='<%#Eval("JKArari6")%>' ToolTip='<%#Eval("JKArari6")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMKS6" runat="server" Width="38px" Text='<%#Eval("MKKensuu6")%>' ToolTip='<%#Eval("MKKensuu6")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMKK6" runat="server" Width="75px" Text='<%#Eval("MKKinkaku6")%>'
                                                        ToolTip='<%#Eval("MKKinkaku6")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMKA6" runat="server" Width="138px" Text='<%#Eval("MKArari6")%>' ToolTip='<%#Eval("MKArari6")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKH7" runat="server" Width="73px" Text='<%#Eval("KoujiHiritu7")%>'
                                                        ToolTip='<%#Eval("KoujiHiritu7")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="75px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MZS7" runat="server" Width="38px" Text='<%#Eval("ZKensuu7")%>' ToolTip='<%#Eval("ZKensuu7")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MZK7" runat="server" Width="75px" Text='<%#Eval("ZKinkaku7")%>' ToolTip='<%#Eval("ZKinkaku7")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MZA7" runat="server" Width="138px" Text='<%#Eval("ZArari7")%>' ToolTip='<%#Eval("ZArari7")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKS7" runat="server" Width="38px" Text='<%#Eval("KKensuu7")%>' ToolTip='<%#Eval("KKensuu7")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKK7" runat="server" Width="75px" Text='<%#Eval("KKinkaku7")%>' ToolTip='<%#Eval("KKinkaku7")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKA7" runat="server" Width="138px" Text='<%#Eval("KArari7")%>' ToolTip='<%#Eval("KArari7")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMS7" runat="server" Width="38px" Text='<%#Eval("MKensuu7")%>' ToolTip='<%#Eval("MKensuu7")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMK7" runat="server" Width="75px" Text='<%#Eval("MKinkaku7")%>' ToolTip='<%#Eval("MKinkaku7")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMA7" runat="server" Width="138px" Text='<%#Eval("MArari7")%>' ToolTip='<%#Eval("MArari7")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJS7" runat="server" Width="38px" Text='<%#Eval("JKensuu7")%>' ToolTip='<%#Eval("JKensuu7")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJK7" runat="server" Width="75px" Text='<%#Eval("JKinkaku7")%>' ToolTip='<%#Eval("JKinkaku7")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJA7" runat="server" Width="138px" Text='<%#Eval("JArari7")%>' ToolTip='<%#Eval("JArari7")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJKS7" runat="server" Width="38px" Text='<%#Eval("JKKensuu7")%>' ToolTip='<%#Eval("JKKensuu7")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJKK7" runat="server" Width="75px" Text='<%#Eval("JKKinkaku7")%>'
                                                        ToolTip='<%#Eval("JKKinkaku7")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJKA7" runat="server" Width="138px" Text='<%#Eval("JKArari7")%>' ToolTip='<%#Eval("JKArari7")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMKS7" runat="server" Width="38px" Text='<%#Eval("MKKensuu7")%>' ToolTip='<%#Eval("MKKensuu7")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMKK7" runat="server" Width="75px" Text='<%#Eval("MKKinkaku7")%>'
                                                        ToolTip='<%#Eval("MKKinkaku7")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMKA7" runat="server" Width="138px" Text='<%#Eval("MKArari7")%>' ToolTip='<%#Eval("MKArari7")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKH8" runat="server" Width="73px" Text='<%#Eval("KoujiHiritu8")%>'
                                                        ToolTip='<%#Eval("KoujiHiritu8")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="75px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MZS8" runat="server" Width="38px" Text='<%#Eval("ZKensuu8")%>' ToolTip='<%#Eval("ZKensuu8")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MZK8" runat="server" Width="75px" Text='<%#Eval("ZKinkaku8")%>' ToolTip='<%#Eval("ZKinkaku8")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MZA8" runat="server" Width="138px" Text='<%#Eval("ZArari8")%>' ToolTip='<%#Eval("ZArari8")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKS8" runat="server" Width="38px" Text='<%#Eval("KKensuu8")%>' ToolTip='<%#Eval("KKensuu8")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKK8" runat="server" Width="75px" Text='<%#Eval("KKinkaku8")%>' ToolTip='<%#Eval("KKinkaku8")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKA8" runat="server" Width="138px" Text='<%#Eval("KArari8")%>' ToolTip='<%#Eval("KArari8")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMS8" runat="server" Width="38px" Text='<%#Eval("MKensuu8")%>' ToolTip='<%#Eval("MKensuu8")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMK8" runat="server" Width="75px" Text='<%#Eval("MKinkaku8")%>' ToolTip='<%#Eval("MKinkaku8")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMA8" runat="server" Width="138px" Text='<%#Eval("MArari8")%>' ToolTip='<%#Eval("MArari8")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJS8" runat="server" Width="38px" Text='<%#Eval("JKensuu8")%>' ToolTip='<%#Eval("JKensuu8")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJK8" runat="server" Width="75px" Text='<%#Eval("JKinkaku8")%>' ToolTip='<%#Eval("JKinkaku8")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJA8" runat="server" Width="138px" Text='<%#Eval("JArari8")%>' ToolTip='<%#Eval("JArari8")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJKS8" runat="server" Width="38px" Text='<%#Eval("JKKensuu8")%>' ToolTip='<%#Eval("JKKensuu8")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJKK8" runat="server" Width="75px" Text='<%#Eval("JKKinkaku8")%>'
                                                        ToolTip='<%#Eval("JKKinkaku8")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJKA8" runat="server" Width="138px" Text='<%#Eval("JKArari8")%>' ToolTip='<%#Eval("JKArari8")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMKS8" runat="server" Width="38px" Text='<%#Eval("MKKensuu8")%>' ToolTip='<%#Eval("MKKensuu8")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMKK8" runat="server" Width="75px" Text='<%#Eval("MKKinkaku8")%>'
                                                        ToolTip='<%#Eval("MKKinkaku8")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMKA8" runat="server" Width="138px" Text='<%#Eval("MKArari8")%>' ToolTip='<%#Eval("MKArari8")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKH9" runat="server" Width="73px" Text='<%#Eval("KoujiHiritu9")%>'
                                                        ToolTip='<%#Eval("KoujiHiritu9")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="75px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MZS9" runat="server" Width="38px" Text='<%#Eval("ZKensuu9")%>' ToolTip='<%#Eval("ZKensuu9")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MZK9" runat="server" Width="75px" Text='<%#Eval("ZKinkaku9")%>' ToolTip='<%#Eval("ZKinkaku9")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MZA9" runat="server" Width="138px" Text='<%#Eval("ZArari9")%>' ToolTip='<%#Eval("ZArari9")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKS9" runat="server" Width="38px" Text='<%#Eval("KKensuu9")%>' ToolTip='<%#Eval("KKensuu9")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKK9" runat="server" Width="75px" Text='<%#Eval("KKinkaku9")%>' ToolTip='<%#Eval("KKinkaku9")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKA9" runat="server" Width="138px" Text='<%#Eval("KArari9")%>' ToolTip='<%#Eval("KArari9")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMS9" runat="server" Width="38px" Text='<%#Eval("MKensuu9")%>' ToolTip='<%#Eval("MKensuu9")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMK9" runat="server" Width="75px" Text='<%#Eval("MKinkaku9")%>' ToolTip='<%#Eval("MKinkaku9")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMA9" runat="server" Width="138px" Text='<%#Eval("MArari9")%>' ToolTip='<%#Eval("MArari9")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJS9" runat="server" Width="38px" Text='<%#Eval("JKensuu9")%>' ToolTip='<%#Eval("JKensuu9")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJK9" runat="server" Width="75px" Text='<%#Eval("JKinkaku9")%>' ToolTip='<%#Eval("JKinkaku9")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJA9" runat="server" Width="138px" Text='<%#Eval("JArari9")%>' ToolTip='<%#Eval("JArari9")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJKS9" runat="server" Width="38px" Text='<%#Eval("JKKensuu9")%>' ToolTip='<%#Eval("JKKensuu9")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJKK9" runat="server" Width="75px" Text='<%#Eval("JKKinkaku9")%>'
                                                        ToolTip='<%#Eval("JKKinkaku9")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJKA9" runat="server" Width="138px" Text='<%#Eval("JKArari9")%>' ToolTip='<%#Eval("JKArari9")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMKS9" runat="server" Width="38px" Text='<%#Eval("MKKensuu9")%>' ToolTip='<%#Eval("MKKensuu9")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMKK9" runat="server" Width="75px" Text='<%#Eval("MKKinkaku9")%>'
                                                        ToolTip='<%#Eval("MKKinkaku9")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMKA9" runat="server" Width="138px" Text='<%#Eval("MKArari9")%>' ToolTip='<%#Eval("MKArari9")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKH10" runat="server" Width="73px" Text='<%#Eval("KoujiHiritu10")%>'
                                                        ToolTip='<%#Eval("KoujiHiritu10")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="75px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MZS10" runat="server" Width="38px" Text='<%#Eval("ZKensuu10")%>' ToolTip='<%#Eval("ZKensuu10")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MZK10" runat="server" Width="75px" Text='<%#Eval("ZKinkaku10")%>'
                                                        ToolTip='<%#Eval("ZKinkaku10")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MZA10" runat="server" Width="138px" Text='<%#Eval("ZArari10")%>' ToolTip='<%#Eval("ZArari10")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKS10" runat="server" Width="38px" Text='<%#Eval("KKensuu10")%>' ToolTip='<%#Eval("KKensuu10")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKK10" runat="server" Width="75px" Text='<%#Eval("KKinkaku10")%>'
                                                        ToolTip='<%#Eval("KKinkaku10")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKA10" runat="server" Width="138px" Text='<%#Eval("KArari10")%>' ToolTip='<%#Eval("KArari10")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMS10" runat="server" Width="38px" Text='<%#Eval("MKensuu10")%>' ToolTip='<%#Eval("MKensuu10")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMK10" runat="server" Width="75px" Text='<%#Eval("MKinkaku10")%>'
                                                        ToolTip='<%#Eval("MKinkaku10")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMA10" runat="server" Width="138px" Text='<%#Eval("MArari10")%>' ToolTip='<%#Eval("MArari10")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJS10" runat="server" Width="38px" Text='<%#Eval("JKensuu10")%>' ToolTip='<%#Eval("JKensuu10")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJK10" runat="server" Width="75px" Text='<%#Eval("JKinkaku10")%>'
                                                        ToolTip='<%#Eval("JKinkaku10")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJA10" runat="server" Width="138px" Text='<%#Eval("JArari10")%>' ToolTip='<%#Eval("JArari10")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJKS10" runat="server" Width="38px" Text='<%#Eval("JKKensuu10")%>'
                                                        ToolTip='<%#Eval("JKKensuu10")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJKK10" runat="server" Width="75px" Text='<%#Eval("JKKinkaku10")%>'
                                                        ToolTip='<%#Eval("JKKinkaku10")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJKA10" runat="server" Width="138px" Text='<%#Eval("JKArari10")%>'
                                                        ToolTip='<%#Eval("JKArari10")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMKS10" runat="server" Width="38px" Text='<%#Eval("MKKensuu10")%>'
                                                        ToolTip='<%#Eval("MKKensuu10")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMKK10" runat="server" Width="75px" Text='<%#Eval("MKKinkaku10")%>'
                                                        ToolTip='<%#Eval("MKKinkaku10")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMKA10" runat="server" Width="138px" Text='<%#Eval("MKArari10")%>'
                                                        ToolTip='<%#Eval("MKArari10")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKH11" runat="server" Width="73px" Text='<%#Eval("KoujiHiritu11")%>'
                                                        ToolTip='<%#Eval("KoujiHiritu11")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="75px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MZS11" runat="server" Width="38px" Text='<%#Eval("ZKensuu11")%>' ToolTip='<%#Eval("ZKensuu11")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MZK11" runat="server" Width="75px" Text='<%#Eval("ZKinkaku11")%>'
                                                        ToolTip='<%#Eval("ZKinkaku11")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MZA11" runat="server" Width="138px" Text='<%#Eval("ZArari11")%>' ToolTip='<%#Eval("ZArari11")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKS11" runat="server" Width="38px" Text='<%#Eval("KKensuu11")%>' ToolTip='<%#Eval("KKensuu11")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKK11" runat="server" Width="75px" Text='<%#Eval("KKinkaku11")%>'
                                                        ToolTip='<%#Eval("KKinkaku11")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKA11" runat="server" Width="138px" Text='<%#Eval("KArari11")%>' ToolTip='<%#Eval("KArari11")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMS11" runat="server" Width="38px" Text='<%#Eval("MKensuu11")%>' ToolTip='<%#Eval("MKensuu11")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMK11" runat="server" Width="75px" Text='<%#Eval("MKinkaku11")%>'
                                                        ToolTip='<%#Eval("MKinkaku11")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMA11" runat="server" Width="138px" Text='<%#Eval("MArari11")%>' ToolTip='<%#Eval("MArari11")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJS11" runat="server" Width="38px" Text='<%#Eval("JKensuu11")%>' ToolTip='<%#Eval("JKensuu11")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJK11" runat="server" Width="75px" Text='<%#Eval("JKinkaku11")%>'
                                                        ToolTip='<%#Eval("JKinkaku11")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJA11" runat="server" Width="138px" Text='<%#Eval("JArari11")%>' ToolTip='<%#Eval("JArari11")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJKS11" runat="server" Width="38px" Text='<%#Eval("JKKensuu11")%>'
                                                        ToolTip='<%#Eval("JKKensuu11")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJKK11" runat="server" Width="75px" Text='<%#Eval("JKKinkaku11")%>'
                                                        ToolTip='<%#Eval("JKKinkaku11")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJKA11" runat="server" Width="138px" Text='<%#Eval("JKArari11")%>'
                                                        ToolTip='<%#Eval("JKArari11")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMKS11" runat="server" Width="38px" Text='<%#Eval("MKKensuu11")%>'
                                                        ToolTip='<%#Eval("MKKensuu11")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMKK11" runat="server" Width="75px" Text='<%#Eval("MKKinkaku11")%>'
                                                        ToolTip='<%#Eval("MKKinkaku11")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMKA11" runat="server" Width="138px" Text='<%#Eval("MKArari11")%>'
                                                        ToolTip='<%#Eval("MKArari11")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MZS12" runat="server" Width="38px" Text='<%#Eval("ZKensuu12")%>' ToolTip='<%#Eval("ZKensuu12")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MZK12" runat="server" Width="75px" Text='<%#Eval("ZKinkaku12")%>'
                                                        ToolTip='<%#Eval("ZKinkaku12")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MZA12" runat="server" Width="138px" Text='<%#Eval("ZArari12")%>' ToolTip='<%#Eval("ZArari12")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKS12" runat="server" Width="38px" Text='<%#Eval("KKensuu12")%>' ToolTip='<%#Eval("KKensuu12")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKK12" runat="server" Width="75px" Text='<%#Eval("KKinkaku12")%>'
                                                        ToolTip='<%#Eval("KKinkaku12")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MKA12" runat="server" Width="138px" Text='<%#Eval("KArari12")%>' ToolTip='<%#Eval("KArari12")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMS12" runat="server" Width="38px" Text='<%#Eval("MKensuu12")%>' ToolTip='<%#Eval("MKensuu12")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMK12" runat="server" Width="75px" Text='<%#Eval("MKinkaku12")%>'
                                                        ToolTip='<%#Eval("MKinkaku12")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMA12" runat="server" Width="138px" Text='<%#Eval("MArari12")%>' ToolTip='<%#Eval("MArari12")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJS12" runat="server" Width="38px" Text='<%#Eval("JKensuu12")%>' ToolTip='<%#Eval("JKensuu12")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJK12" runat="server" Width="75px" Text='<%#Eval("JKinkaku12")%>'
                                                        ToolTip='<%#Eval("JKinkaku12")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJA12" runat="server" Width="138px" Text='<%#Eval("JArari12")%>' ToolTip='<%#Eval("JArari12")%>'
                                                        CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJKS12" runat="server" Width="38px" Text='<%#Eval("JKKensuu12")%>'
                                                        ToolTip='<%#Eval("JKKensuu12")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJKK12" runat="server" Width="75px" Text='<%#Eval("JKKinkaku12")%>'
                                                        ToolTip='<%#Eval("JKKinkaku12")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MJKA12" runat="server" Width="138px" Text='<%#Eval("JKArari12")%>'
                                                        ToolTip='<%#Eval("JKArari12")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMKS12" runat="server" Width="38px" Text='<%#Eval("MKKensuu12")%>'
                                                        ToolTip='<%#Eval("MKKensuu12")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="40px" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMKK12" runat="server" Width="75px" Text='<%#Eval("MKKinkaku12")%>'
                                                        ToolTip='<%#Eval("MKKinkaku12")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="MMKA12" runat="server" Width="138px" Text='<%#Eval("MKArari12")%>'
                                                        ToolTip='<%#Eval("MKArari12")%>' CssClass="lCss"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Height="17px" Width="77px" HorizontalAlign="Left" CssClass="rigthMeisai" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                            <td valign="top" style="width: 17px; height: 125px;">
                                <div id="divHiddenMeisaiV" runat="server" style="overflow: auto; height: 161px; width: 30px;
                                    margin-left: -14px;" onscroll="fncScrollV();">
                                    <table runat="server" border="1" id="scrollV">
                                        <tr>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 16px; width: 422px;">
                            </td>
                            <td colspan="2" style="height: 16px">
                                <div id="divHiddenMeisaiH" runat="server" style="overflow: auto; height: 18px; width: 455px;
                                    margin-top: -2px;" onscroll="fncScrollH();">
                                    <table runat="server" id="scrollH">
                                        <tr>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <!---------------------検索結果区----------------------->
                </td>
            </tr>
        </tbody>
    </table>
    <table border="0" cellpadding="0" cellspacing="2" class="titleTable" style="width: 958px;">
        <tr>
            <td style="width: 8px">
            </td>
            <td>
                <uc1:CommonButton ID="btnMinaosi" runat="server" Text="計画見直し" />
            </td>
            <td style="width: 130px; text-align: right;">
            </td>
            <td style="text-align: right">
                <uc1:CommonButton ID="btnKakunin" runat="server" Text="計画確定" />
                &nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
    <div style="position: absolute; vertical-align: top; margin-top: 100px;">
    </div>
</asp:Content>

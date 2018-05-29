<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master"
    Codebehind="KensakuSyoukaiInquiry.aspx.vb" Inherits="Itis.Earth.WebUI.KensakuSyoukaiInquiry"
    Title="加盟店検索照会指示" %>

<%@ Register Src="control/common_drop.ascx" TagName="common_drop" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js"></script>

    <script type="text/javascript">
    //window名付与
    var objWin = window;
    objWin.name = "earthMainWindow"
    initPage(); //画面初期設定
    
    function window.onload(){
        fncSetKubunVal();
    }
    </script>

    <div id="buySelName" runat="server" class="modalDiv" style="position: absolute; left: 300px;
        top: 140px; z-index: 2; display: none;">
    </div>
    <div id="disableDiv" runat="server" style="position: absolute; left: 0px; top: 0px;
        width: 1002px; height: 620px; z-index: 100; filter: alpha(opacity=70); background-color: #000000;
        display: none;">
        <iframe src="about:blank" id="hiddenIframe" width="100%" height="100%"></iframe>
    </div>
    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tr>
            <th>
                加盟店検索照会指示
            </th>
            <th style="text-align: right;">
            </th>
        </tr>
        <tr>
            <td colspan="2" rowspan="1">
            </td>
        </tr>
    </table>
    <table id="tblKensaku" style="width: 960px; text-align: left;" class="mainTable paddinNarrow"
        cellpadding="1">
        <tr>
            <th class="tableTitle" colspan="8" rowspan="1" style="height: 20px;">
                検索条件
            </th>
        </tr>
        <tr>
            <td class="hissu" style="font-weight: bold;">
                区分</td>
            <td colspan="5" class="hissu">
                <uc1:common_drop ID="Common_drop1" runat="server" GetStyle="kubun" />
                <uc1:common_drop ID="Common_drop2" runat="server" GetStyle="kubun" />
                <uc1:common_drop ID="Common_drop3" runat="server" GetStyle="kubun" />
                &nbsp;
                <asp:CheckBox runat="server" ID="chkKubunAll" Text="全区分" TextAlign="left" Checked="true" />
                <asp:CheckBox runat="server" ID="chkTaikai" Text="退会した加盟店を表示する" />
            </td>
        </tr>
        <tr>
            <td style="width: 95px;" class="koumokuMei">
                加盟店名</td>
            <td style="width: 370px;">
                <asp:TextBox runat="server" ID="tbxKameitenMei" MaxLength="40" Style="width: 300px;" />
            </td>
            <td style="width: 100px;" class="koumokuMei">
                加盟店コード</td>
            <td style="width: 125px; border-right-color: #E6E6E6;">
                <asp:TextBox runat="server" ID="tbxKameitenCd1" MaxLength="5" Style="width: 65px;"
                    CssClass="codeNumber"></asp:TextBox>
                <asp:Button runat="server" ID="btnKameitenSearch1" Text="検索" OnClientClick="return fncKameitenSearch('1');" />
            </td>
            <td style="width: 45px; border-left-color: #E6E6E6; border-right-color: #E6E6E6;">
                ～
            </td>
            <td style="width: 253px; border-left-color: #E6E6E6;">
                <asp:TextBox runat="server" ID="tbxKameitenCd2" MaxLength="5" Style="width: 65px;"
                    CssClass="codeNumber"></asp:TextBox>
                <asp:Button runat="server" ID="btnKameitenSearch2" Text="検索" OnClientClick="return fncKameitenSearch('2');" />
            </td>
        </tr>
        <tr>
            <td class="koumokuMei">
                加盟店名カナ</td>
            <td>
                <asp:TextBox runat="server" ID="tbxKameitenKana" MaxLength="20" Style="width: 300px;" />
            </td>
            <td class="koumokuMei">
                営業所コード</td>
            <td style="border-right-color: #E6E6E6;">
                <asp:TextBox runat="server" ID="tbxEigyousyoCd1" MaxLength="5" Style="width: 65px;"
                    CssClass="codeNumber"></asp:TextBox>
                <asp:Button runat="server" ID="btnEigyousyoSearch1" Text="検索" OnClientClick="return fncEigyousyoSearch('1');" />
            </td>
            <td style="border-left-color: #E6E6E6; border-right-color: #E6E6E6;">
                ～
            </td>
            <td style="border-left-color: #E6E6E6;">
                <asp:TextBox runat="server" ID="tbxEigyousyoCd2" MaxLength="5" Style="width: 65px;"
                    CssClass="codeNumber"></asp:TextBox>
                <asp:Button runat="server" ID="btnEigyousyoSearch2" Text="検索" OnClientClick="return fncEigyousyoSearch('2');" />
            </td>
        </tr>
        <tr>
            <td class="koumokuMei">
                電話番号</td>
            <td>
                <asp:TextBox runat="server" ID="tbxDenwaBangou" MaxLength="16" Style="width: 300px;"
                    CssClass="codeNumber" />
            </td>
            <td class="koumokuMei">
                登録年月</td>
            <td style="border-right-color: #E6E6E6;">
                <asp:TextBox runat="server" ID="tbxTourokuNengetuhi1" MaxLength="7" Style="width: 108px;"
                    CssClass="codeNumber"></asp:TextBox>
            </td>
            <td style="border-left-color: #E6E6E6; border-right-color: #E6E6E6;">
                ～
            </td>
            <td style="border-left-color: #E6E6E6;">
                <asp:TextBox runat="server" ID="tbxTourokuNengetuhi2" MaxLength="7" Style="width: 108px;"
                    CssClass="codeNumber"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="koumokuMei">
                都道府県</td>
            <td>
                <uc1:common_drop ID="Common_drop" runat="server" GetStyle="todoufuken" />
            </td>
            <td class="koumokuMei">
                系列コード</td>
            <td style="border-right-color: #E6E6E6;">
                <asp:TextBox runat="server" ID="tbxKeiretuCd" MaxLength="5" Style="width: 65px;"
                    CssClass="codeNumber"></asp:TextBox>
                <asp:Button runat="server" ID="btnKeiretuSearch" Text="検索" OnClientClick="return fncKeiretuSearch();" />
            </td>
            <td colspan="2">
                <asp:TextBox runat="Server" ID="tbxKeiretuMei" BorderStyle="None" Width="240px" ReadOnly="true"
                    TabIndex="-1" BackColor="#E6E6E6"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="koumokuMei">
                送付先選択
            </td>
            <td colspan="5">
                <asp:CheckBoxList ID="chkLitSoufusaki" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                    <asp:ListItem Text="登録住所　" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="請求書　"></asp:ListItem>
                    <asp:ListItem Text="保証書送付先　"></asp:ListItem>
                    <asp:ListItem Text="瑕疵保証書印字先　"></asp:ListItem>
                    <asp:ListItem Text="定期刊行物　"></asp:ListItem>
                    <asp:ListItem Text="調査報告書　"></asp:ListItem>
                    <asp:ListItem Text="工事報告書　"></asp:ListItem>
                    <asp:ListItem Text="検査報告書"></asp:ListItem>
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr class="tableSpacer">
            <td colspan="6">
            </td>
        </tr>
        <tr>
            <td colspan="6" rowspan="1" style="text-align: center">
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
    <table style="text-align: left; width: 100px;" border="0" cellpadding="0" cellspacing="0">
    </table>
    <table style="height: 25px">
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
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <div id="divHeadLeft" style="width: 320px; overflow-y: hidden; overflow-x: hidden;">
                    <table class="gridviewTableHeader" cellpadding="0" cellspacing="0" width="320px">
                        <tr>
                            <td style="width: 20px; border-left: 1px solid black; height: 28px;">
                                &nbsp;</td>
                            <td style="width: 75px; text-align: center;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2" style="border-color: #FFFFD9;">
                                            取消&nbsp;</td>
                                        <td style="height: 15px; border-color: #FFFFD9; padding: 0px; vertical-align: bottom;">
                                            <asp:LinkButton runat="server" ID="btnSortTorikesi1" Text="▲" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 14px; border-color: #FFFFD9; padding: 0px; vertical-align: top;">
                                            <asp:LinkButton runat="server" ID="btnSortTorikesi2" Text="▼" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 75px; text-align: center;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2" style="border-color: #FFFFD9;">
                                            区分&nbsp;</td>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: bottom;">
                                            <asp:LinkButton runat="server" ID="btnSortKubun1" Text="▲" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: top;">
                                            <asp:LinkButton runat="server" ID="btnSortKubun2" Text="▼" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 120px; text-align: center;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2" style="border-color: #FFFFD9;">
                                            加盟店コード&nbsp;</td>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: bottom;">
                                            <asp:LinkButton runat="server" ID="btnSortKameitenCd1" Text="▲" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: top;">
                                            <asp:LinkButton runat="server" ID="btnSortKameitenCd2" Text="▼" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td style="width: 532px">
                <div id="divHeadRight" runat="server" style="width: 638px; overflow-y: hidden; overflow-x: hidden;
                    border-right: 1px solid black;">
                    <table class="gridviewTableHeader" cellpadding="0" cellspacing="0" width="2109px">
                        <tr style="">
                            <td style="width: 272px; text-align: center; border-left: 1px solid #999999;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2" style="border-color: #FFFFD9;">
                                            加盟店名1&nbsp;</td>
                                        <td style="height: 15px; border-color: #FFFFD9; padding: 0px; vertical-align: bottom;">
                                            <asp:LinkButton runat="server" ID="btnSortKameitenMei1" Text="▲" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 14px; border-color: #FFFFD9; padding: 0px; vertical-align: top;">
                                            <asp:LinkButton runat="server" ID="btnSortKameitenMei2" Text="▼" TabIndex="-1" Height="14px"
                                                Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 162px; text-align: center;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2" style="border-color: #FFFFD9;">
                                            加盟店名カナ1&nbsp;</td>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: bottom;">
                                            <asp:LinkButton runat="server" ID="btnSortKameitenKana1" Text="▲" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: top;">
                                            <asp:LinkButton runat="server" ID="btnSortKameitenKana2" Text="▼" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 271px; text-align: center;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2" style="border-color: #FFFFD9;">
                                            加盟店名2&nbsp;</td>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: bottom;">
                                            <asp:LinkButton runat="server" ID="btnSortKameitenMei21" Text="▲" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: top;">
                                            <asp:LinkButton runat="server" ID="btnSortKameitenMei22" Text="▼" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 474px; text-align: center;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2" style="border-color: #FFFFD9;">
                                            住所&nbsp;</td>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: bottom;">
                                            <asp:LinkButton runat="server" ID="btnSortJyuusyo1" Text="▲" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: top;">
                                            <asp:LinkButton runat="server" ID="btnSortJyuusyo2" Text="▼" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 120px; text-align: center;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2" style="border-color: #FFFFD9;">
                                            都道府県名&nbsp;</td>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: bottom;">
                                            <asp:LinkButton runat="server" ID="btnSortTourofukenMei1" Text="▲" Height="14px"
                                                TabIndex="-1" Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: top;">
                                            <asp:LinkButton runat="server" ID="btnSortTourofukenMei2" Text="▼" Height="14px"
                                                TabIndex="-1" Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 110px; text-align: center;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2" style="border-color: #FFFFD9;">
                                            系列コード&nbsp;</td>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: bottom;">
                                            <asp:LinkButton runat="server" ID="btnSortKeiretu1" Text="▲" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: top;">
                                            <asp:LinkButton runat="server" ID="btnSortKeiretu2" Text="▼" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 120px; text-align: center;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2" style="border-color: #FFFFD9;">
                                            営業所コード&nbsp;</td>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: bottom;">
                                            <asp:LinkButton runat="server" ID="btnSortEigyousyoCd1" Text="▲" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: top;">
                                            <asp:LinkButton runat="server" ID="btnSortEigyousyoCd2" Text="▼" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 110px; text-align: center;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2" style="border-color: #FFFFD9;">
                                            ビルダーNO&nbsp;</td>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: bottom;">
                                            <asp:LinkButton runat="server" ID="btnSortBuilderNo1" Text="▲" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: top;">
                                            <asp:LinkButton runat="server" ID="btnSortBuilderNo2" Text="▼" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 160px; text-align: center;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2" style="border-color: #FFFFD9;">
                                            代表者名&nbsp;</td>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: bottom;">
                                            <asp:LinkButton runat="server" ID="btnSortDaihyousyaMei1" Text="▲" Height="14px"
                                                TabIndex="-1" Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: top;">
                                            <asp:LinkButton runat="server" ID="btnSortDaihyousyaMei2" Text="▼" Height="14px"
                                                TabIndex="-1" Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 150px; text-align: center;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2" style="border-color: #FFFFD9;">
                                            電話番号&nbsp;</td>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: bottom;">
                                            <asp:LinkButton runat="server" ID="btnSortTelNo1" Text="▲" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: top;">
                                            <asp:LinkButton runat="server" ID="btnSortTelNo2" Text="▼" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divBodyLeft" runat="server" onmousewheel="wheel();" style="width: 319px;
                    height: 154px; overflow-x: hidden; overflow-y: hidden; border-left: 1px solid black;
                    border-top: 1px solid black; border-bottom: 1px solid black;">
                    <asp:GridView ID="grdItiranLeft" runat="server" AutoGenerateColumns="False" BackColor="White"
                        CssClass="tableMeiSai" BorderWidth="1px" ShowHeader="False" CellPadding="0">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <input type="radio" name="rdo_SetHoshouNo" style="width: 20px;" onclick="fncSetLineColor(event.srcElement.parentNode.parentNode,event.srcElement.parentNode.parentNode.rowIndex);fncSetKameitenCd();funDisableButton();" />
                                </ItemTemplate>
                                <ItemStyle Width="22px" BorderColor="#999999" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblTorikesi" runat="server" Width="77px" Text='<%#Eval("torikesi")%>'
                                        ToolTip='<%#Eval("torikesi")%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Height="21px" Width="77px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="kbn">
                                <ItemStyle Width="77px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="kameiten_cd">
                                <ItemStyle Width="122px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                        </Columns>
                        <SelectedRowStyle ForeColor="White" />
                        <AlternatingRowStyle BackColor="LightCyan" />
                    </asp:GridView>
                </div>
            </td>
            <td style="width: 532px">
                <div id="divBodyRight" runat="server" onmousewheel="wheel();" style="width: 638px;
                    height: 154px; overflow-x: hidden; overflow-y: hidden; border-right: 1px solid black;
                    border-top: 1px solid black; border-bottom: 1px solid black;">
                    <asp:GridView ID="grdItiranRight" runat="server" AutoGenerateColumns="False" BackColor="White"
                        CssClass="tableMeiSai" Width="2109px" BorderWidth="1px" ShowHeader="False" CellPadding="0">
                        <Columns>
                            <asp:BoundField DataField="kameiten_mei1">
                                <ItemStyle Width="274px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tenmei_kana1">
                                <ItemStyle Width="164px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="kameiten_mei2">
                                <ItemStyle Width="273px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="jyuusyo1">
                                <ItemStyle Width="476px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="todouhuken_mei">
                                <ItemStyle Width="122px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="keiretu_cd">
                                <ItemStyle Width="112px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="eigyousyo_cd">
                                <ItemStyle Width="122px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="builder_no">
                                <ItemStyle Width="112px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="daihyousya_mei">
                                <ItemStyle Width="162px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tel_no">
                                <ItemStyle Width="152px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                        </Columns>
                        <SelectedRowStyle ForeColor="White" />
                        <AlternatingRowStyle BackColor="LightCyan" />
                    </asp:GridView>
                </div>
            </td>
            <td valign="top" style="width: 17px; height: 155px;">
                <div id="divHiddenMeisaiV" runat="server" style="overflow: auto; height: 155px; width: 30px;
                    margin-left: -14px;" onscroll="fncScrollV();">
                    <table height="<%=scrollHeight%>">
                        <tr>
                            <td>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td style="height: 16px;">
            </td>
            <td style="width: 532px">
                <div style="overflow-x: hidden; height: 18px; width: 639px; margin-top: -1px;">
                    <div id="divHiddenMeisaiH" runat="server" style="overflow: auto; height: 18px; width: 655px;
                        margin-top: 0px;" onscroll="fncScrollH();">
                        <table style="width: 2109px;">
                            <tr>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
            <td style="height: 16px; width: 17px;">
            </td>
        </tr>
    </table>
    <table cellspacing="0" cellpadding="0" border="0" style="margin-top: 10px;">
        <tr>
            <td style="padding-top: 0px; vertical-align: top; width: 130px;">
                <asp:Button runat="server" ID="btnTouroku" CssClass="bottombutton" Text="新規登録" OnClientClick="fncGamenSenni('0');return false;" />
            </td>
            <td style="width: 787px">
                <table style="width: 786px;" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td style="width: 76px; height: 24px; padding-left: 2px;">
                            情報照会</td>
                        <td style="border-right: 0px white; width: 70px;">
                            <asp:Button runat="server" ID="btnKihonJyouhou" Text="基本" OnClientClick="fncGamenSenni('1');return false;"
                                Width="60px" Height="22px" Disabled />
                        </td>
                        <td style="border-left: 0px white; width: 70px;">
                            <asp:Button runat="server" ID="btnTyuiJikou" Text="注意" OnClientClick="fncGamenSenni('2');return false;"
                                Width="60px" Height="22px" Disabled />
                        </td>
                        <td style="border-left: 0px white; width: 70px;">
                            <asp:Button runat="server" ID="btnBukkenJyouhou" Text="物件" OnClientClick="fncGamenSenni('3');return false;"
                                Width="60px" Height="22px" Disabled />
                        </td>
                        <td style="border-left: 0px white; width: 70px;">
                            <asp:Button runat="server" ID="btnYosinJyouhou" Text="与信" OnClientClick="fncGamenSenni('4');return false;"
                                Width="60px" Height="22px" Disabled />
                        </td>
                        <td style="border-left: 0px white; width: 70px;">
                            <asp:Button runat="server" ID="btnKakakuJyouhou" Text="価格" OnClientClick="fncGamenSenni('5');return false;"
                                Width="60px" Height="22px" Disabled />
                        </td>
                        <td style="border-left: 0px white; width: 120px;">
                            <asp:Button runat="server" ID="btnSiharaiTyousa" Text="支払条件（調査）" Width="110px" Height="22px"
                                Disabled OnClientClick="fncSetScroll();" />
                        </td>
                        <td style="border-left: 0px white; width: 120px;">
                            <asp:Button runat="server" ID="btnSiharaiKouji" Text="支払条件（工事）" Width="110px" Height="22px"
                                Disabled OnClientClick="fncSetScroll();" />
                        </td>
                        <td style="border-left: 0px white; width: 120px;">
                            <asp:Button runat="server" ID="btnHoukakusyo" Text="報告書・オプション" Width="110px" Height="22px"
                                Disabled OnClientClick="fncSetScroll();" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table cellspacing="0" cellpadding="0" border="0" style="margin-top: 5px;">
        <tr>
            <td style="padding-top: 0px; vertical-align: top; width: 130px;">
                <asp:Button runat="server" ID="btnIttukatu" CssClass="bottombutton" Text="一括取込" OnClientClick="fncGamenSenni('6');return false;" />
            </td>
            <td>
                <table cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td style="width: 75px; height: 24px; padding-left: 1px;">
                            CSV出力</td>
                        <td style="height: 24px; border-right: 0px white; width: 70px;">
                            <asp:Button runat="server" ID="btnKihonJyouhouCsv" Text="基本" Width="60px" Height="22px" />
                        </td>
                        <td style="height: 24px; border-left: 0px white; width: 70px;">
                            <asp:Button runat="server" ID="btnJyusyoJyouhouCsv" Text="住所" Width="60px" Height="22px" />
                        </td>
                        <td style="height: 24px; border-left: 0px white; width: 120px;">
                            <asp:Button runat="server" ID="btnKameitenInfoIttukatuCsv" Text="一括取込用情報" CssClass="bottombutton1"
                                Height="22px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table cellspacing="0" cellpadding="0" border="0" style="margin-left: 128px; margin-top: 5px;">
        <tr>
            <td style="width: 76px; height: 24px; padding-left: 2px;">
                過去データ</td>
            <td style="height: 24px; border-right: 0px white; width: 120px;">
                <asp:Button runat="server" ID="btnTorihukiJyoukenKakuninhyou" Text="取引条件確認表" Width="110px"
                    Height="22px" Disabled OnClientClick="fncSetScroll();" />
            </td>
            <td style="height: 24px; border-left: 0px white; width: 120px;">
                <asp:Button runat="server" ID="btnTyousaCard" Text="調査カード" Width="110px" Height="22px"
                    Disabled OnClientClick="fncSetScroll();" Style="display: none;" />
            </td>
        </tr>
    </table>
    <input type="hidden" id="hidKameitenCd" />
    <input type="hidden" id="hidKbnCd" />
    <asp:HiddenField runat="server" ID="hidKeiretuMei" />
    <asp:HiddenField runat="server" ID="hidScroll" />
    <asp:HiddenField runat="server" ID="hidSentakuKameitenCd" />
    <asp:HiddenField runat="server" ID="hidSentakuKbn" />
    <asp:HiddenField runat="server" ID="hidFile" />
    <asp:HiddenField runat="server" ID="hidSelectRowIndex" />
    <a id="file" style="display: none;" runat="server">取引条件確認表</a>
</asp:Content>

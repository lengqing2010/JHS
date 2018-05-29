<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master"
    Codebehind="BukkenJyouhouInquiry.aspx.vb" Inherits="Itis.Earth.WebUI.BukkenJyouhouInquiry"
    Title="加盟店物件情報照会" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js"></script>

    <script type="text/javascript">
    //window名付与
    var objWin = window;
    objWin.name = "earthMainWindow"
    initPage(); //画面初期設定   
    </script>

    <div style="height: 503px;">
        <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
            class="titleTable">
            <tbody>
                <tr>
                    <th style="width: 780px;">
                        加盟店物件情報照会
                    </th>
                    <th rowspan="2" style="text-align: left; color: Red;" align="center" valign="middle">
                        「社外秘」
                    </th>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td style="height: 10px;">
                    </td>
                </tr>
                <tr>
                </tr>
            </tbody>
        </table>
        <table border="0" cellpadding="1" cellspacing="0" style="text-align: left;">
            <tr>
                <td style="font-size: 13px;">
                    加盟店コード
                </td>
                <td>
                    <asp:TextBox runat="server" ID="tbxKameiTenCd" Style="width: 45px;" Text="" CssClass="readOnly"
                        TabIndex="-1" ReadOnly="true" />
                </td>
                <td style="font-size: 13px; padding-left: 5px;">
                    加盟店名
                </td>
                <td>
                    <asp:TextBox runat="server" ID="tbxKameiTenName" Style="width: 230px;" Text="" CssClass="readOnly"
                        TabIndex="-1" ReadOnly="true" />
                </td>
                <td style="font-size: 13px; padding-left: 5px;">
                    取消
                </td>
                <td>
                    <asp:TextBox runat="server" ID="tbxTorikesi" Style="width: 80px;" Text="" CssClass="readOnly"
                        TabIndex="-1" ReadOnly="true" />
                </td>
                <td style="font-size: 13px; padding-left: 5px;">
                    加盟店カナ名
                </td>
                <td>
                    <asp:TextBox runat="server" ID="tbxKameiTenKana" Style="width: 190px;" Text="" CssClass="readOnly"
                        TabIndex="-1" ReadOnly="true" />
                </td>
                <td style="font-size: 13px; padding-left: 5px;">
                    担当者
                </td>
                <td>
                    <asp:TextBox runat="server" ID="tbxTantou" Style="width: 90px;" Text="" CssClass="readOnly"
                        TabIndex="-1" ReadOnly="true" />
                </td>
            </tr>
        </table>
        <table id="Table1" style="border: solid 0px black; margin-top: 10px;" runat="server"
            cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <div id="divHeadLeft" style="width: 201px;">
                        <table class="gridviewTableHeader" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 200px; border-left: 1px solid black; border-top: 1px White; border-left: 1px White;
                                    background-color: White; border-right: 1px White; height: 24px;">
                                    &nbsp</td>
                            </tr>
                            <tr>
                                <td style="border-left: 1px solid black; border-top: 1px solid black; height: 24px;">
                                    物件NO
                                    <asp:LinkButton runat="server" ID="btnSortHosyousyoNo1" Text="▲" Height="14px" ForeColor="SkyBlue"
                                        Font-Underline="false" Visible="true" Style="margin-right: -3px" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="btnSortHosyousyoNo2" Text="▼" Height="14px" ForeColor="SkyBlue"
                                        Font-Underline="false" Visible="true" Style="margin-left: -3px" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 24px; border-left: 1px solid black; border-bottom: 1px solid black;">
                                    施主名
                                    <asp:LinkButton runat="server" ID="btnSortSesyuMei1" Text="▲" Height="14px" ForeColor="SkyBlue"
                                        Font-Underline="false" Visible="true" Style="margin-right: -3px" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="btnSortSesyuMei2" Text="▼" Height="14px" ForeColor="SkyBlue"
                                        Font-Underline="false" Visible="true" Style="margin-left: -3px" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td>
                    <div id="divHeadRight" runat="server" style="width: 760px; overflow: hidden;">
                        <table class="gridviewTableHeader" width="2164px" cellpadding="0" cellspacing="0">
                            <tr>
                                <td colspan="1" style="border-top: 1px White; background-color: White; border-right: 1px solid black;
                                    height: 24px;">
                                    &nbsp</td>
                                <td colspan="5" style="">
                                    調査</td>
                                <td colspan="4" style="">
                                    工事</td>
                                <td style="">
                                    保証書</td>
                                <td style="border-right: 1px solid black;">
                                    債権</td>
                            </tr>
                            <tr>
                                <td rowspan="2" style="width: 277px; border-left: 1px solid gray; border-bottom: 1px solid black;
                                    border-top: 1px solid black; height: 24px;">
                                    住所
                                    <asp:LinkButton runat="server" ID="btnSortBukkenJyuusyo1" Text="▲" Height="14px"
                                        ForeColor="SkyBlue" Font-Underline="false" Visible="true" Style="margin-right: -3px"
                                        OnClientClick="fncSetScroll();"></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="btnSortBukkenJyuusyo2" Text="▼" Height="14px"
                                        ForeColor="SkyBlue" Font-Underline="false" Visible="true" Style="margin-left: -3px"
                                        OnClientClick="fncSetScroll();"></asp:LinkButton>
                                </td>
                                <td style="width: 157px; height: 24px;">
                                    調査会社コード
                                    <asp:LinkButton runat="server" ID="btnKaisyaCd1" Text="▲" Height="14px" ForeColor="SkyBlue"
                                        Font-Underline="false" Visible="true" Style="margin-right: -3px" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="btnKaisyaCd2" Text="▼" Height="14px" ForeColor="SkyBlue"
                                        Font-Underline="false" Visible="true" Style="margin-left: -3px" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                </td>
                                <td style="width: 125px; height: 24px;">
                                    依頼日
                                    <asp:LinkButton runat="server" ID="btnSortIraiDate1" Text="▲" Height="14px" ForeColor="SkyBlue"
                                        Font-Underline="false" Visible="true" Style="margin-right: -3px" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="btnSortIraiDate2" Text="▼" Height="14px" ForeColor="SkyBlue"
                                        Font-Underline="false" Visible="true" Style="margin-left: -3px" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                </td>
                                <td style="width: 118px;">
                                    調査実施日
                                    <asp:LinkButton runat="server" ID="btnSortTysJissiDate1" Text="▲" Height="14px" ForeColor="SkyBlue"
                                        Font-Underline="false" Visible="true" Style="margin-right: -3px" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="btnSortTysJissiDate2" Text="▼" Height="14px" ForeColor="SkyBlue"
                                        Font-Underline="false" Visible="true" Style="margin-left: -3px" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                </td>
                                <td style="width: 205px;">
                                    報告書発送日
                                    <asp:LinkButton runat="server" ID="btnSortTysHkksHakDate1" Text="▲" Height="14px"
                                        ForeColor="SkyBlue" Font-Underline="false" Visible="true" Style="margin-right: -3px"
                                        OnClientClick="fncSetScroll();"></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="btnSortTysHkksHakDate2" Text="▼" Height="14px"
                                        ForeColor="SkyBlue" Font-Underline="false" Visible="true" Style="margin-left: -3px"
                                        OnClientClick="fncSetScroll();"></asp:LinkButton>
                                </td>
                                <td rowspan="2" style="width: 293px; border-bottom: 1px solid black;">
                                    判定結果
                                    <asp:LinkButton runat="server" ID="btnSortKsSiyou1" Text="▲" Height="14px" ForeColor="SkyBlue"
                                        Font-Underline="false" Visible="true" Style="margin-right: -3px" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="btnSortKsSiyou2" Text="▼" Height="14px" ForeColor="SkyBlue"
                                        Font-Underline="false" Visible="true" Style="margin-left: -3px" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                </td>
                                <td style="width: 157px; height: 14px;">
                                    工事会社コード
                                    <asp:LinkButton runat="server" ID="btnGaisyaCd1" Text="▲" Height="14px" ForeColor="SkyBlue"
                                        Font-Underline="false" Visible="true" Style="margin-right: -3px" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="btnGaisyaCd2" Text="▼" Height="14px" ForeColor="SkyBlue"
                                        Font-Underline="false" Visible="true" Style="margin-left: -3px" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                </td>
                                <td style="width: 143px;">
                                    工法
                                    <asp:LinkButton runat="server" ID="btnSortKouhou1" Text="▲" Height="14px" ForeColor="SkyBlue"
                                        Font-Underline="false" Visible="true" Style="margin-right: -3px" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="btnSortKouhou2" Text="▼" Height="14px" ForeColor="SkyBlue"
                                        Font-Underline="false" Visible="true" Style="margin-left: -3px" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                </td>
                                <td style="width: 123px;">
                                    改良工事日
                                    <asp:LinkButton runat="server" ID="btnSortKairy_koj_date1" Text="▲" Height="14px"
                                        ForeColor="SkyBlue" Font-Underline="false" Visible="true" Style="margin-right: -3px"
                                        OnClientClick="fncSetScroll();"></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="btnSortKairy_koj_date2" Text="▼" Height="14px"
                                        ForeColor="SkyBlue" Font-Underline="false" Visible="true" Style="margin-left: -3px"
                                        OnClientClick="fncSetScroll();"></asp:LinkButton>
                                </td>
                                <td style="width: 210px;">
                                    報告書発送日
                                    <asp:LinkButton runat="server" ID="btnSortKoj_hkks_hassou_date1" Text="▲" Height="14px"
                                        ForeColor="SkyBlue" Font-Underline="false" Visible="true" Style="margin-right: -3px"
                                        OnClientClick="fncSetScroll();"></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="btnSortKoj_hkks_hassou_date2" Text="▼" Height="14px"
                                        ForeColor="SkyBlue" Font-Underline="false" Visible="true" Style="margin-left: -3px"
                                        OnClientClick="fncSetScroll();"></asp:LinkButton>
                                </td>
                                <td style="width: 205px;">
                                    保証書発行状況
                                    <asp:LinkButton runat="server" ID="btnSortHosyousyo_hak_jyky1" Text="▲" Height="14px"
                                        ForeColor="SkyBlue" Font-Underline="false" Visible="true" Style="margin-right: -3px"
                                        OnClientClick="fncSetScroll();"></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="btnSortHosyousyo_hak_jyky2" Text="▼" Height="14px"
                                        ForeColor="SkyBlue" Font-Underline="false" Visible="true" Style="margin-left: -3px"
                                        OnClientClick="fncSetScroll();"></asp:LinkButton>
                                </td>
                                <td style="width: 151px; border-right: 1px solid black;">
                                    売上額
                                    <asp:LinkButton runat="server" ID="btnSortUriagegaku1" Text="▲" Height="14px" ForeColor="SkyBlue"
                                        Font-Underline="false" Visible="true" Style="margin-right: -3px" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="btnSortUriagegaku2" Text="▼" Height="14px" ForeColor="SkyBlue"
                                        Font-Underline="false" Visible="true" Style="margin-left: -3px" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td style="border-bottom: 1px solid black; height: 24px;">
                                    調査会社名
                                    <asp:LinkButton runat="server" ID="btnKaisyaMei1" Text="▲" Height="14px" ForeColor="SkyBlue"
                                        Font-Underline="false" Visible="true" Style="margin-right: -3px" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="btnKaisyaMei2" Text="▼" Height="14px" ForeColor="SkyBlue"
                                        Font-Underline="false" Visible="true" Style="margin-left: -3px" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                </td>
                                <td style="border-bottom: 1px solid black;">
                                    承諾書調査日
                                    <asp:LinkButton runat="server" ID="btnSortSyoudakusyo_tys_date1" Text="▲" Height="14px"
                                        ForeColor="SkyBlue" Font-Underline="false" Visible="true" Style="margin-right: -3px"
                                        OnClientClick="fncSetScroll();"></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="btnSortSyoudakusyo_tys_date2" Text="▼" Height="14px"
                                        ForeColor="SkyBlue" Font-Underline="false" Visible="true" Style="margin-left: -3px"
                                        OnClientClick="fncSetScroll();"></asp:LinkButton>
                                </td>
                                <td style="border-bottom: 1px solid black;">
                                    計画書作成日
                                    <asp:LinkButton runat="server" ID="btnSortKeikakusyo_sakusei_date1" Text="▲" Height="14px"
                                        ForeColor="SkyBlue" Font-Underline="false" Visible="true" Style="margin-right: -3px"
                                        OnClientClick="fncSetScroll();"></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="btnSortKeikakusyo_sakusei_date2" Text="▼" Height="14px"
                                        ForeColor="SkyBlue" Font-Underline="false" Visible="true" Style="margin-left: -3px"
                                        OnClientClick="fncSetScroll();"></asp:LinkButton>
                                </td>
                                <td style="border-bottom: 1px solid black;">
                                    解析担当者
                                    <asp:LinkButton runat="server" ID="btnSortTantousya_mei1" Text="▲" Height="14px"
                                        ForeColor="SkyBlue" Font-Underline="false" Visible="true" Style="margin-right: -3px"
                                        OnClientClick="fncSetScroll();"></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="btnSortTantousya_mei2" Text="▼" Height="14px"
                                        ForeColor="SkyBlue" Font-Underline="false" Visible="true" Style="margin-left: -3px"
                                        OnClientClick="fncSetScroll();"></asp:LinkButton>
                                </td>
                                <td style="border-bottom: 1px solid black;">
                                    工事会社名
                                    <asp:LinkButton runat="server" ID="btnGaisyaMei1" Text="▲" Height="14px" ForeColor="SkyBlue"
                                        Font-Underline="false" Visible="true" Style="margin-right: -3px" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="btnGaisyaMei2" Text="▼" Height="14px" ForeColor="SkyBlue"
                                        Font-Underline="false" Visible="true" Style="margin-left: -3px" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                </td>
                                <td style="border-bottom: 1px solid black;">
                                    改良工事予定日
                                    <asp:LinkButton runat="server" ID="btnSortKairy_koj_kanry_yotei_date1" Text="▲" Height="14px"
                                        ForeColor="SkyBlue" Font-Underline="false" Visible="true" Style="margin-right: -3px"
                                        OnClientClick="fncSetScroll();"></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="btnSortKairy_koj_kanry_yotei_date2" Text="▼" Height="14px"
                                        ForeColor="SkyBlue" Font-Underline="false" Visible="true" Style="margin-left: -3px"
                                        OnClientClick="fncSetScroll();"></asp:LinkButton>
                                </td>
                                <td style="border-bottom: 1px solid black;">
                                    完工速報着日
                                    <asp:LinkButton runat="server" ID="btnSortKairy_koj_sokuhou_tyk_date1" Text="▲" Height="14px"
                                        ForeColor="SkyBlue" Font-Underline="false" Visible="true" Style="margin-right: -3px"
                                        OnClientClick="fncSetScroll();"></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="btnSortKairy_koj_sokuhou_tyk_date2" Text="▼" Height="14px"
                                        ForeColor="SkyBlue" Font-Underline="false" Visible="true" Style="margin-left: -3px"
                                        OnClientClick="fncSetScroll();"></asp:LinkButton>
                                </td>
                                <td style="border-bottom: 1px solid black;">
                                    工事担当者
                                    <asp:LinkButton runat="server" ID="btnSortKouji_tantousya_mei1" Text="▲" Height="14px"
                                        ForeColor="SkyBlue" Font-Underline="false" Visible="true" Style="margin-right: -3px"
                                        OnClientClick="fncSetScroll();"></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="btnSortKouji_tantousya_mei2" Text="▼" Height="14px"
                                        ForeColor="SkyBlue" Font-Underline="false" Visible="true" Style="margin-left: -3px"
                                        OnClientClick="fncSetScroll();"></asp:LinkButton>
                                </td>
                                <td style="border-bottom: 1px solid black;">
                                    保証書発行日
                                    <asp:LinkButton runat="server" ID="btnSortHosyousyo_hak_date1" Text="▲" Height="14px"
                                        ForeColor="SkyBlue" Font-Underline="false" Visible="true" Style="margin-right: -3px"
                                        OnClientClick="fncSetScroll();"></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="btnSortHosyousyo_hak_date2" Text="▼" Height="14px"
                                        ForeColor="SkyBlue" Font-Underline="false" Visible="true" Style="margin-left: -3px"
                                        OnClientClick="fncSetScroll();"></asp:LinkButton>
                                </td>
                                <td style="border-bottom: 1px solid black; border-right: 1px solid black;">
                                    入金額
                                    <asp:LinkButton runat="server" ID="btnSortNyukingaku1" Text="▲" Height="14px" ForeColor="SkyBlue"
                                        Font-Underline="false" Visible="true" Style="margin-right: -3px" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="btnSortNyukingaku2" Text="▼" Height="14px" ForeColor="SkyBlue"
                                        Font-Underline="false" Visible="true" Style="margin-left: -3px" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td style="width: 17px">
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <div id="divBodyLeft" runat="server" style="width: 200px; border-left: 1px solid black;
                        border-bottom: 1px solid black; overflow: hidden;">
                        <asp:GridView ID="grdBodyLeft" runat="server" AutoGenerateColumns="False" BackColor="White"
                            CssClass="tableMeiSai" BorderWidth="1px" ShowHeader="False" CellPadding="0">
                            <Columns>
                                <asp:BoundField DataField="col0">
                                    <ItemStyle Width="200px" />
                                </asp:BoundField>
                            </Columns>
                            <SelectedRowStyle ForeColor="White" />
                            <RowStyle Height="30px" />
                        </asp:GridView>
                    </div>
                </td>
                <td valign="top">
                    <div id="divBodyRight" runat="server" style="border-right: 1px solid black; border-bottom: 1px solid black;
                        width: 759px; overflow: hidden;">
                        <asp:GridView ID="grdBodyRight" runat="server" Width="2164px" AutoGenerateColumns="False"
                            BackColor="White" CssClass="tableMeiSai" BorderWidth="1px" ShowHeader="False"
                            CellPadding="0">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemStyle Width="263px" />
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Width="244px" Text='<%# eval("col1") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemStyle Width="160px" />
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Width="150px" Text='<%# eval("col11") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemStyle Width="133px" />
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Width="120px" Text='<%# eval("col2") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemStyle Width="127px" />
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Width="120px" Text='<%# eval("col3") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemStyle Width="200px" />
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Width="195px" Text='<%# eval("col4") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemStyle Width="269px" />
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Width="270px" Text='<%# eval("col5") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemStyle Width="160px" />
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Width="150px" Text='<%# eval("col12") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemStyle Width="156px" />
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Width="130px" Text='<%# eval("col6") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemStyle Width="130px" />
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Width="120px" Text='<%# eval("col7") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemStyle Width="205px" />
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Width="195px" Text='<%# eval("col8") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemStyle Width="205px" />
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Width="195px" Text='<%# eval("col9") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemStyle Width="152px" />
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Width="142px" Text='<%# eval("col10") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <SelectedRowStyle ForeColor="White" />
                            <RowStyle Height="30px" />
                        </asp:GridView>
                    </div>
                </td>
                <td valign="top" style="width: 17px">
                    <div id="divHiddenMeisaiV" runat="server" style="overflow: auto; height: 301px; width: 30px;
                        margin-left: -14px;">
                        <table id="tblDivHMV" runat="server">
                            <tr>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="height: 16px">
                </td>
                <td style="height: 16px">
                    <div style="height: 18px; width: 760px; margin-top: -2px; overflow: hidden;">
                        <div id="divHiddenMeisaiH" runat="server" style="overflow: auto; height: 18px; width: 776px;
                            margin-top: 0px;">
                            <table id="tblDivHMH" runat="server">
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
    </div>
    <table id="tblButton" runat="server">
        <tr>
            <td>
                <asp:Button ID="btnTojiru" runat="server" CssClass="kyoutuubutton" Text="閉じる" />
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <input type="hidden" id="kameitenCd" value="" runat="server" />
    <asp:HiddenField runat="server" ID="hidScroll" />
</asp:Content>

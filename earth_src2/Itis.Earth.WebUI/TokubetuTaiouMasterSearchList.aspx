<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master"
    Codebehind="TokubetuTaiouMasterSearchList.aspx.vb" Inherits="Itis.Earth.WebUI.TokubetuTaiouMasterSearchList"
    Title="加盟店商品調査方法特別対応マスタ照会" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js"></script>

    <script type="text/javascript">
        //window名付与
        var objWin = window;
        objWin.name = "earthMainWindow"
        initPage(); //画面初期設定
    </script>

    <div id="buySelName" runat="server" class="modalDiv" style="position: absolute; left: 300px;
        top: 140px; z-index: 2; display: none;">
    </div>
    <div id="disableDiv" runat="server" style="position: absolute; left: 0px; top: 0px;
        width: 960px; height: 600px; z-index: 100; filter: alpha(opacity=70); background-color: #000000;
        display: none;">
        <iframe src="about:blank" id="hiddenIframe" width="100%" height="100%" style="width: 103%;
            height: 115%;"></iframe>
    </div>
    <div>
        <table style="width: 960px; text-align: left" class="TitleTable" border="0" cellpadding="0"
            cellspacing="2">
            <tr>
                <th style="width: 350px; font-size: 16px;">
                    加盟店商品調査方法特別対応マスタ照会
                </th>
                <th style="width: 70px; height: 25px;">
                    <asp:Button ID="btnClose" runat="server" Text="閉じる" Style="height: 25px; padding-top: 2px;" />
                </th>
                <th style="width: 70px; height: 25px;">
                    <asp:Button ID="btnCSVInput" runat="server" Text="CSV取込" Style="height: 25px; padding-top: 2px;" />
                </th>
                <th style="height: 25px">
                </th>
                <th style="height: 25px">
                </th>
            </tr>
            <tr>
                <td colspan="5" rowspan="1">
                </td>
            </tr>
        </table>
        <table style="width: 960px" class="mainTable" cellpadding="2" style="border-bottom: none;">
            <thead>
                <tr>
                    <th class="tableTitle" colspan="4" rowspan="1" style="height: 24px">
                        <asp:Label ID="lbljyouken" runat="server" Text="検索条件"></asp:Label>
                        <asp:Button ID="btnClear" runat="server" Text="クリア" Style="padding-top: 2px;" />
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td colspan="1" style="width: 100px; height: 24px; background-color: #ccffff;">
                        <asp:Label ID="lblKameiten" runat="server" Text="相手先コード"></asp:Label>
                    </td>
                    <td colspan="3" style="height: 24px; padding: 0px 0px 0px 0px;">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="border: none; padding: 0px 0px 0px 4px;">
                                    <div style="float: left; margin-right: 3px; margin-top: 2px;">
                                        <asp:DropDownList ID="ddlAitesakiSyubetu" runat="server" Width="80px">
                                        </asp:DropDownList>
                                    </div>
                                </td>
                                <td style="border: none; padding: 0px 0px 0px 0px;">
                                    <div id="divAitesakiSyubetu" runat="server" style="float: left;">
                                        <table border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="border: none; padding: 0px 0px 0px 0px;">
                                                    <asp:TextBox ID="tbxKameitenCdFrom" runat="server" Width="50px" MaxLength="5" Style="ime-mode: disabled;"></asp:TextBox>
                                                    <asp:Button ID="btnKameitenSearchFrom" runat="server" Text="検索" Style="padding-top: 2px;" />
                                                    <asp:TextBox ID="tbxKameitenMeiFrom" runat="server" CssClass="readOnlyStyle" Width="253px"
                                                        TabIndex="-1"></asp:TextBox>
                                                </td>
                                                <td style="border: none; padding: 0px 0px 0px 0px;">
                                                    <div id="divAitesakiTo" runat="server">
                                                        <asp:Label ID="lblFromTo" runat="server" Text="～" Style="margin-left: 5px;"></asp:Label>
                                                        <asp:TextBox ID="tbxKameitenCdTo" runat="server" Width="50px" MaxLength="5" Style="ime-mode: disabled;"></asp:TextBox>
                                                        <asp:Button ID="btnKameitenSearchTo" runat="server" Text="検索" Style="padding-top: 2px;" />
                                                        <asp:TextBox ID="tbxKameitenMeiTo" runat="server" CssClass="readOnlyStyle" Width="253px"
                                                            TabIndex="-1"></asp:TextBox>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
        <%--===========================================================================================--%>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 960px; border: solid 2px gray;
            border-top: none; border-bottom: none; background-color: #e6e6e6; padding-left: 4px;">
            <tr style="height: 30px;">
                <td style="width: 102px; background-color: #ccffff; border-right: solid 1px gray;
                    border-bottom: solid 1px gray;">
                    <asp:Label ID="lblSyouhin" runat="server" Text="商品コード"></asp:Label>
                </td>
                <td style="width: 382px; border-right: solid 1px gray; border-bottom: solid 1px gray;">
                    <asp:DropDownList ID="ddlSyouhinCd" runat="server" Width="320px">
                    </asp:DropDownList>
                </td>
                <td style="width: 102px; background-color: #ccffff; border-right: solid 1px gray;
                    border-bottom: solid 1px gray;">
                    <asp:Label ID="lblTyousa" runat="server" Text="調査方法"></asp:Label>
                </td>
                <td style="border-bottom: solid 1px gray;">
                    <asp:DropDownList ID="ddlTyousaHouhou" runat="server" Width="300px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="height: 30px;">
                <td style="background-color: #ccffff; border-right: solid 1px gray; border-bottom: solid 1px gray;">
                    <asp:Label ID="lblTokubetu" runat="server" Text="特別対応コード"></asp:Label>
                </td>
                <td colspan="3" style="border-bottom: solid 1px gray;">
                    <asp:TextBox ID="tbxTokubetuTaiouCd" runat="server" Width="50px" MaxLength="5" Style="ime-mode: disabled;"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="検索" Style="padding-top: 2px;" />
                    <asp:TextBox ID="tbxTokubetuTaiouMei" runat="server" CssClass="readOnlyStyle" Width="280px"
                        TabIndex="-1"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 960px; border-top: none;">
            <tr style="height: 30px; background-color: #e6e6e6;">
                <td style="border: solid 2px gray; border-top: none; text-align: center;">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="border: none; padding: 0px 0px 0px 0px;">
                                <asp:Label ID="lblKensakuKensuu" runat="server" Font-Bold="False" Text="検索上限件数"></asp:Label>
                                <asp:DropDownList ID="ddlKensuu" runat="server">
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="100">100</asp:ListItem>
                                    <asp:ListItem Value="max">無制限</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Button ID="btnKensaku" runat="server" Text="検索実行" Style="padding-top: 2px; height: 26px; width:60px;" />
                                <asp:CheckBox ID="chkTorikesi" runat="server" Font-Bold="False" Font-Size="11px" Text="取消は対象外" Checked="true" />
                                <asp:CheckBox ID="chkAitesakiTaisyouGai" runat="server" Font-Size="11px" Text="取消相手先は対象外" Checked="true" />
                                <asp:CheckBox ID="chk0TaisyouGai" runat="server" Font-Size="11px" Text="\0は対象外" Checked="false" />
                            </td>
                            <td style="border: none; vertical-align: bottom; padding: 0px 0px 0px 0px;">
                                <div id="divSitei" runat="server" style="display: none;">
                                    <asp:CheckBox ID="chkSiteiNasiTaisyou" runat="server" Font-Size="11px" Text="系列・営業所・指定無しも対象" Checked="true" />
                                </div>
                            </td>
                            <td style="border: none; padding: 0px 0px 0px 0px;">
                                <div id="divCsvOutput" runat="server">
                                    <asp:Button ID="btnCSVOutput" runat="server" Text="CSV出力" Style="width: 65px; padding-top: 2px;" />
                                </div>
                            </td>
                            <td style="border: none; vertical-align: bottom; padding: 0px 0px 0px 0px;">
                                <asp:CheckBox ID="chkMisetteimo" runat="server" Font-Size="11px" Text="未設定も含む" />
                                <asp:CheckBox ID="chkSyokiti" runat="server" Font-Size="11px" Text="初期値1のみ" Checked="true" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <%--===========================================================================================--%>
    </div>
    <table border="0" cellpadding="0" cellspacing="0" style="margin-top: 10px;">
        <tr>
            <td style="width: 65px; height: 14px;">
                検索結果：&nbsp;
            </td>
            <td style="height: 14px">
                <asp:Label runat="server" ID="lblCount"></asp:Label>&nbsp;件
            </td>
            <td style="height: 14px; padding-left: 20px; color: Red; display: none;" runat="server"
                id="tbHoukokusyo">
                報告書様式：
                <asp:Label runat="server" ID="lblHoukokusyo" Style="white-space: nowrap; overflow: hidden;
                    text-overflow: ellipsis;" Width ="720px"></asp:Label>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <div id="divHeadLeft" runat="server" style="width: 675px; border-left: solid 1px black;
                    overflow-y: hidden; overflow-x: hidden;">
                    <table class="gridviewTableHeader" cellpadding="0" cellspacing="0" style="width: 675px;
                        border-bottom: solid 1px black;">
                        <tr>
                            <td style="width: 65px; height: 16px;">
                                先種別<asp:LinkButton ID="btnSortAitesakiSyubetuUp" runat="server" TabIndex="-1" Font-Underline="false"
                                    Height="14px" Text="▲"></asp:LinkButton>
                                <asp:LinkButton ID="btnSortAitesakiSyubetuDown" runat="server" TabIndex="-1" Font-Underline="false"
                                    Height="14px" Style="margin-left: -10px" Text="▼"></asp:LinkButton></td>
                            <td style="width: 84px; height: 16px;">
                                相手先CD<asp:LinkButton ID="btnSortAitesakiCdUp" runat="server" TabIndex="-1" Font-Underline="false"
                                    Height="14px" Text="▲"></asp:LinkButton>
                                <asp:LinkButton ID="btnSortAitesakiCdDown" runat="server" TabIndex="-1" Font-Underline="false"
                                    Height="14px" Style="margin-left: -10px" Text="▼"></asp:LinkButton></td>
                            <td style="height: 16px; width: 95px;">
                                相手先名<asp:LinkButton ID="btnSortAitesakiMeiUp" runat="server" TabIndex="-1" Font-Underline="false"
                                    Height="14px" Text="▲"></asp:LinkButton>
                                <asp:LinkButton ID="btnSortAitesakiMeiDown" runat="server" TabIndex="-1" Font-Underline="false"
                                    Height="14px" Style="margin-left: -10px" Text="▼"></asp:LinkButton></td>
                            <td style="height: 16px">
                                商品<asp:LinkButton ID="btnSortSyouhinCdUp" runat="server" TabIndex="-1" Font-Underline="false"
                                    Height="14px" Text="▲"></asp:LinkButton>
                                <asp:LinkButton ID="btnSortSyouhinCdDown" runat="server" TabIndex="-1" Font-Underline="false"
                                    Height="14px" Style="margin-left: -10px" Text="▼"></asp:LinkButton></td>
                            <td style="width: 98px; height: 16px">
                                商品名<asp:LinkButton ID="btnSortSyouhinMeiUp" runat="server" TabIndex="-1" Font-Underline="false"
                                    Height="14px" Text="▲"></asp:LinkButton>
                                <asp:LinkButton ID="btnSortSyouhinMeiDown" runat="server" TabIndex="-1" Font-Underline="false"
                                    Height="14px" Style="margin-left: -10px" Text="▼"></asp:LinkButton></td>
                            <td style="width: 90px; height: 16px">
                                調査方法<asp:LinkButton ID="btnSortTyousaUp" runat="server" TabIndex="-1" Font-Underline="false"
                                    Height="14px" Text="▲"></asp:LinkButton>
                                <asp:LinkButton ID="btnSortTyousaDown" runat="server" TabIndex="-1" Font-Underline="false"
                                    Height="14px" Style="margin-left: -10px" Text="▼"></asp:LinkButton></td>
                            <%--<td style="height: 16px">
                                特別対応コード<asp:LinkButton ID="btnSortTokubetuCdUp" runat="server" TabIndex="-1" Font-Underline="false"
                                    Height="14px" Text="▲"></asp:LinkButton>
                                <asp:LinkButton ID="btnSortTokubetuCdDown" runat="server" TabIndex="-1" Font-Underline="false"
                                    Height="14px" Style="margin-left: -10px" Text="▼"></asp:LinkButton></td>--%>
                            <td style="width: 130px;height: 16px">
                                特別対応名称<asp:LinkButton ID="btnSortTokubetuMeiUp" runat="server" TabIndex="-1" Font-Underline="false"
                                    Height="14px" Text="▲"></asp:LinkButton>
                                <asp:LinkButton ID="btnSortTokubetuMeiDown" runat="server" TabIndex="-1" Font-Underline="false"
                                    Height="14px" Style="margin-left: -10px" Text="▼"></asp:LinkButton></td>
                        </tr>
                    </table>
                </div>
            </td>
            <td>
                <div id="divHeadRight" runat="server" style="width: 280px; border-right: solid 1px black;
                    overflow-y: hidden; overflow-x: hidden;">
                    <table class="gridviewTableHeader" cellpadding="0" cellspacing="0" style="width: 605px;
                        border-bottom: solid 1px black;">
                        <tr>
                            <td style="width: 121px; height: 16px">
                                実請求加算金額<asp:LinkButton ID="btnSortRequestAddKingakuUp" runat="server" TabIndex="-1"
                                    Font-Underline="false" Height="14px" Text="▲"></asp:LinkButton>
                                <asp:LinkButton ID="btnSortRequestAddKingakuDown" runat="server" TabIndex="-1" Font-Underline="false"
                                    Height="14px" Style="margin-left: -10px" Text="▼"></asp:LinkButton></td>
                            <td style="width: 149px; height: 16px">
                                工務店請求加算金額<asp:LinkButton ID="btnSortKoumuAddKingakuUp" runat="server" TabIndex="-1"
                                    Font-Underline="false" Height="14px" Text="▲"></asp:LinkButton>
                                <asp:LinkButton ID="btnSortKoumuAddKingakuDown" runat="server" TabIndex="-1" Font-Underline="false"
                                    Height="14px" Style="margin-left: -10px" Text="▼"></asp:LinkButton></td>
                            <td style="height: 16px">
                                取消<asp:LinkButton ID="btnSortTorikesiUp" runat="server" TabIndex="-1" Font-Underline="false"
                                    Height="14px" Text="▲"></asp:LinkButton>
                                <asp:LinkButton ID="btnSortTorikesiDown" runat="server" TabIndex="-1" Font-Underline="false"
                                    Height="14px" Style="margin-left: -10px" Text="▼"></asp:LinkButton></td>
                            <td style="width: 150px; height: 16px">
                                金額加算商品CD<asp:LinkButton ID="btnSortKingakuAddScdUp" runat="server" TabIndex="-1"
                                    Font-Underline="false" Height="14px" Text="▲"></asp:LinkButton>
                                <asp:LinkButton ID="btnSortKingakuAddScdDown" runat="server" TabIndex="-1" Font-Underline="false"
                                    Height="14px" Style="margin-left: -10px" Text="▼"></asp:LinkButton></td>
                            <td style="width: 80px; height: 16px">
                                初期値<asp:LinkButton ID="btnSortSyokiTiUp" runat="server" TabIndex="-1" Font-Underline="false"
                                    Height="14px" Text="▲"></asp:LinkButton>
                                <asp:LinkButton ID="btnSortSyokiTiDown" runat="server" TabIndex="-1" Font-Underline="false"
                                    Height="14px" Style="margin-left: -10px" Text="▼"></asp:LinkButton></td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; width: 675px;">
                <div id="divMeisaiLeft" runat="server" onmousewheel="wheel();" style="width: 675px;
                    height: 302px; overflow-y: hidden; overflow-x: hidden; border-left: solid 1px black;
                    margin-top: -1px; border-bottom: solid 1px black;">
                    <asp:GridView ID="grdMeisaiLeft" runat="server" AutoGenerateColumns="False" CellPadding="0"
                        Width="675px" ShowHeader="False" CssClass="tableMeiSai">
                        <Columns>
                            <asp:TemplateField>
                                <ItemStyle Width="66px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="aitesaki_syubetu_layout" runat="server" Width="66px" Text='<%#Eval("aitesaki_syubetu_layout")%>'
                                        ToolTip='<%#Eval("aitesaki_syubetu_layout")%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="86px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="aitesaki_cd" runat="server" Width="86px" Text='<%#Eval("aitesaki_cd")%>'
                                        ToolTip='<%#Eval("aitesaki_cd")%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="97px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="kameiten_mei1" runat="server" Width="97px" Text='<%#Eval("aitesaki_name")%>'
                                        ToolTip='<%#Eval("aitesaki_name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="syouhin_cd" runat="server" Text='<%#Eval("syouhin_cd")%>'
                                        ToolTip='<%#Eval("syouhin_cd")%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="100px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="syouhin_mei" runat="server" Width="100px" Text='<%#Eval("syouhin_mei")%>'
                                        ToolTip='<%#Eval("syouhin_mei")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="92px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="tys_houhou" runat="server" Width="92px" Text='<%#Eval("tys_houhou")%>'
                                        ToolTip='<%#Eval("tys_houhou")%>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField>
                                <ItemStyle HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="tokubetu_taiou_cd" runat="server" Text='<%#Eval("tokubetu_taiou_cd")%>'
                                        ToolTip='<%#Eval("tokubetu_taiou_cd")%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField>
                                <ItemStyle Width="132px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="tokubetu_taiou_meisyou" runat="server" Width="132px" Text='<%#Eval("tokubetu_taiou_meisyou")%>'
                                        ToolTip='<%#Eval("tokubetu_taiou_meisyou")%>' </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <%--<AlternatingRowStyle BackColor="#CCFFFF" />--%>
                        <RowStyle Height="43px" />
                    </asp:GridView>
                </div>
            </td>
            <td style="vertical-align: top; width: 280px;">
                <div id="divMeisaiRight" runat="server" onmousewheel="wheel();" style="width: 280px;
                    height: 302px; overflow-y: hidden; overflow-x: hidden; border-right: solid 1px black;
                    margin-top: -1px; border-bottom: solid 1px black;">
                    <asp:GridView ID="grdMeisaiRight" runat="server" AutoGenerateColumns="False" CellPadding="0"
                        Width="605px" ShowHeader="False" CssClass="tableMeiSai">
                        <Columns>
                            <asp:TemplateField>
                                <ItemStyle Width="122px" HorizontalAlign="Right" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="uri_kasan_gaku" runat="server" Width="122px" Text='<%#Eval("uri_kasan_gaku")%>'
                                        ToolTip='<%#Eval("uri_kasan_gaku")%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="151px" HorizontalAlign="Right" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="koumuten_kasan_gaku" runat="server" Width="148px" Text='<%#Eval("koumuten_kasan_gaku")%>'
                                        ToolTip='<%#Eval("koumuten_kasan_gaku")%>' Style="padding-right:3px;white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="torikesi" runat="server" Text='<%#Eval("torikesi")%>'
                                        ToolTip='<%#Eval("torikesi")%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="152px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="kasan_syouhin_cd" runat="server" Width="152px" Text='<%#Eval("kasan_syouhin_cd")%>'
                                        ToolTip='<%#Eval("kasan_syouhin_cd")%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="82px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="syokiti" runat="server" Width="82px" Text='<%#Eval("syokiti")%>' ToolTip='<%#Eval("syokiti")%>'
                                        Style="white-space: nowrap; overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <%--<AlternatingRowStyle BackColor="#CCFFFF" />--%>
                        <RowStyle Height="43px" />
                    </asp:GridView>
                </div>
            </td>
            <td valign="top" style="width: 16px; height: 287px;">
                <div id="divHiddenMeisaiV" runat="server" style="overflow: auto; height: 287px; width: 30px;
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
            <td>
                <div style="overflow-x: hidden; height: 18px; width: 275px; margin-top: -1px;">
                    <div id="divHiddenMeisaiH" runat="server" style="overflow: auto; height: 18px; width: 275px;
                        margin-top: 0px;" onscroll="fncScrollH();">
                        <table style="width: 770px;">
                            <tr>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
            <td style="height: 16px;">
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hidCSVFlg" runat="server" />
</asp:Content>

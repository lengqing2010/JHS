<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master"
    Codebehind="TyuiJyouhouInquiry.aspx.vb" Inherits="Itis.Earth.WebUI.TyuiJyouhouInquiry"
    Title="加盟店注意情報照会" %>

<%@ Register Src="control/kyoutuu_jyouhou.ascx" TagName="kyoutuu_jyouhou" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js"></script>

    <script type="text/javascript">
        //window名付与
        var objWin = window;
        objWin.name = "earthMainWindow2"
        initPage(); //画面初期設定
        
    </script>

    <!--基本情報明細-->
    <div id="buySelName" runat="server" class="modalDiv" style="position: absolute; left: 300px;
        top: 140px; z-index: 2; display: none;">
    </div>
    <div id="disableDiv" runat="server" style="position: absolute; left: 0px; top: 0px;
        width: 1002px; height: 596px; z-index: 100; filter: alpha(opacity=70); background-color: #000000;
        display: none;">
        <iframe src="about:blank" id="hiddenIframe" width="100%" height="100%"></iframe>
    </div>
    <table class="titleTable" border="0" style="width: 960px;" cellpadding="0" cellspacing="0">
        <tr>
            <!-- TITLEの字 -->
            <th rowspan="1" style="width: 730px; text-align: left; vertical-align: bottom;">
                加盟店注意情報照会&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                <asp:Button ID="btnClose" runat="server" Text="閉じる" Width="50px" Height="22px" />
                <%--<asp:Button ID="btnTyuiJyouhouInquiry" runat="server" Text="基本情報" />
                <asp:Button ID="btnEigyouJyouhouInquiry" runat="server" Text="営業情報" />
                <asp:Button ID="btnBukkenJyouhouInquiry" runat="server" Text="物件情報" />
                <asp:Button ID="btnYosinJyouhouDetails" runat="server" Text="与信情報" />
                <asp:Button ID="btnKakakuJyouhou" runat="server" Text="価格情報" OnClientClick="fncGamenSenni();return false;" />--%>
            </th>
            <td style="width: 64px;">
                参照日時：</td>
            <td style="width: 200px;">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                    <ContentTemplate>
                        <asp:Label ID="lblHi" runat="server"></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" style="margin-bottom: 4px; margin-top: 4px;">
        <tr>
            <td>
                <asp:Button ID="btnTyuiJyouhouInquiry" runat="server" Text="基本" Style="margin-right: 2px;"
                    Width="60px" Height="22px" />
                <%--<asp:Button ID="btnTyuiJyouhouInquiry" runat="server" Text="注意" Style="margin-right: 10px;" />--%>
                <asp:Button ID="btnEigyouJyouhouInquiry" runat="server" Text="営業" Style="margin-right: 2px;"
                    Width="60px" Height="22px" />
                <asp:Button ID="btnBukkenJyouhouInquiry" runat="server" Text="物件" Style="margin-right: 2px;"
                    Width="60px" Height="22px" />
                <asp:Button ID="btnYosinJyouhouDetails" runat="server" Text="与信" Style="margin-right: 2px;"
                    Width="60px" Height="22px" />
                <asp:Button ID="btnKakakuJyouhou" runat="server" Text="価格" Style="margin-right: 2px;"
                    Width="60px" Height="22px" OnClientClick="fncGamenSenni();return false;" />
                <asp:Button runat="server" ID="btnSiharaiTyousa" Text="支払条件（調査）" Width="110px" Height="22px"
                    Style="margin-right: 2px;" />
                <asp:Button runat="server" ID="btnSiharaiKouji" Text="支払条件（工事）" Width="110px" Height="22px"
                    Style="margin-right: 2px;" />
                <asp:Button runat="server" ID="btnHoukakusyo" Text="報告書・オプション" Width="110px" Height="22px"
                    Style="margin-right: 2px;" />
                <asp:Button runat="server" ID="btnTorihukiJyoukenKakuninhyou" Text="取引条件確認表" Width="110px"
                    Height="22px" Style="margin-right: 2px;" />
                <asp:Button runat="server" ID="btnTyousaCard" Text="調査カード" Width="100px" Height="22px"
                    Style="display: none;" />
            </td>
        </tr>
    </table>
    <!--##############
    共通情報
    ##############-->
    <table border="0" cellpadding="0" cellspacing="0" style="width: 959px; border: 2px solid gray;
        text-align: left;">
        <thead>
            <tr>
                <th class="tableTitle" colspan="10" rowspan="1">
                    <a id="A1" runat="server">共通情報</a> <span id="kyoutuTitleInfobar" style="display: none;"
                        runat="server"></span>
                </th>
            </tr>
        </thead>
        <!--共通情報明細-->
        <tbody id="Tbody1" runat="server">
            <tr>
                <td colspan="10">
                    <table border="0" cellpadding="3" cellspacing="0" style="width: 100%; background-color: #e6e6e6;
                        border-top: 1px solid gray;">
                        <tr style="height: 21px;">
                            <td class="koumokuMei" style="width: 80px; border-right: 1px solid gray;">
                                区分
                            </td>
                            <td style="width: 110px; border-right: 1px solid gray;">
                                <asp:Label ID="lblKyoutuKubun" runat="server" Width="100px"></asp:Label>
                            </td>
                            <td class="koumokuMei" style="width: 75px; border-right: 1px solid gray;">
                                区分名
                            </td>
                            <td style="width: 200px; border-right: 1px solid gray;">
                                <asp:Label ID="lblKubunMei" runat="server" Width="143px"></asp:Label>
                            </td>
                            <td class="koumokuMei" style="width: 50px; border-right: 1px solid gray;">
                                取消&nbsp;
                            </td>
                            <td style="width: 120px; border-right: 1px solid gray;">
                                <asp:Label ID="lblTorikesi" runat="server" Width="100px"></asp:Label>
                            </td>
                            <td class="koumokuMei" style="width: 95px; border-right: 1px solid gray;">
                                工事売上種別
                            </td>
                            <td>
                                <asp:Label ID="lblKoujiUriageSyuubetu" runat="server" Width="120px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="10">
                    <table border="0" cellpadding="3" cellspacing="0" style="width: 100%; background-color: #e6e6e6;
                        border-top: 1px solid gray;">
                        <tr style="height: 21px;">
                            <td class="koumokuMei" style="width: 80px; border-right: 1px solid gray;">
                                加盟店コード
                            </td>
                            <td style="width: 110px; border-right: 1px solid gray;">
                                <asp:Label ID="lblKyoutuKameitenCd" runat="server" Width="100px"></asp:Label>
                            </td>
                            <td class="koumokuMei" style="width: 75px; border-right: 1px solid gray;">
                                加盟店名１
                            </td>
                            <td style="width: 280px; border-right: 1px solid gray;">
                                <asp:Label ID="lblKyoutuKameitenMei1" runat="server" Width="271px"></asp:Label>
                            </td>
                            <td class="koumokuMei" style="width: 75px; border-right: 1px solid gray;">
                                加盟店名２&nbsp;
                            </td>
                            <td>
                                <asp:Label ID="lblKyoutuKameitenMei2" runat="server" Width="265px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
    <div style="overflow-y: auto; height: 386px; width: 977px; margin-top: 10px;">
        <%--優先注意事項 START--%>
        <table style="text-align: left; width: 960px; border: 2px solid gray;" class="mainTable2"
            cellpadding="1">
            <thead>
                <tr>
                    <th class="tableTitle" colspan="11" rowspan="1" style="height: 18px">
                        <a id="jikouLink" runat="server">優先注意事項</a> <span id="jikouSpan" style="display: none;"
                            runat="server"></span>
                    </th>
                </tr>
            </thead>
            <tbody id="jikouTbody" runat="server">
                <tr>
                    <td style="vertical-align: top;">
                        <table width="100%" class="gridviewTableHeader" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 150px; border-left: 1px solid black;">
                                    種別</td>
                                <%--<td style="width: 107px;">
                                種別名
                            </td>--%>
                                <td style="width: 69px;">
                                    入力日</td>
                                <td style="width: 137px;">
                                    受付者</td>
                                <td style="border-right: 1px solid black;">
                                    内容</td>
                            </tr>
                        </table>
                        <asp:UpdatePanel ID="UpdatePanelA" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                            <ContentTemplate>
                                <asp:Button ID="btnA" runat="server" Text="Button" Style="display: none" Width="35px" />
                                <asp:GridView ID="grdBodyA" runat="server" BackColor="White" Style="padding-left: 2px;
                                    border-right: 1px solid #999999; border-top: 1px solid black;" BorderWidth="1px"
                                    ShowHeader="False" CellPadding="0" BorderColor="GrayText">
                                    <SelectedRowStyle ForeColor="White" />
                                    <AlternatingRowStyle BackColor="LightCyan" />
                                    <RowStyle BorderColor="#999999" BorderWidth="1px" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </tbody>
        </table>
        <%--優先注意事項 END--%>

        <%--トラブル・クレーム情報 START--%>
        <table style="text-align: left; width: 960px; border: 2px solid gray; margin-top :10px;" class="mainTable2"
            cellpadding="1">
            <thead>
                <tr>
                    <th class="tableTitle" colspan="11" rowspan="1" style="height: 18px">
                        <a id="titleLink14" runat="server">トラブル・クレーム情報</a> <span id="toraburuSpan" style="display: none;"
                            runat="server"></span>
                    </th>
                </tr>
            </thead>
            <tbody id="toraburuTbody" runat="server">
                <tr>
                    <td style="vertical-align: top;">
                        <table width="100%" class="gridviewTableHeader" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 150px; border-left: 1px solid black;">
                                    種別</td>
                                <td style="width: 69px;">
                                    入力日</td>
                                <td style="width: 137px;">
                                    受付者</td>
                                <td style="border-right: 1px solid black;">
                                    内容</td>
                            </tr>
                        </table>
                        <asp:UpdatePanel ID="UpdatePanelTORA" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                            <ContentTemplate>
                                <asp:Button ID="btnTORA" runat="server" Text="Button" Style="display: none" Width="35px" />
                                <asp:GridView ID="grdbodyTORA" runat="server" BackColor="White" Style="padding-left: 2px;
                                    border-right: 1px solid #999999; border-top: 1px solid black;" BorderWidth="1px"
                                    ShowHeader="False" CellPadding="0" BorderColor="GrayText">
                                    <SelectedRowStyle ForeColor="White" />
                                    <AlternatingRowStyle BackColor="LightCyan" />
                                    <RowStyle BorderColor="#999999" BorderWidth="1px" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </tbody>
        </table>
        <%--トラブル・クレーム情報 END--%>
        
        <%--基本商品＆基本調査方法 START--%>

        <table style="text-align: left; width: 960px; border: 2px solid gray; margin-top :10px;" class="mainTable2"
            cellpadding="1">
            <thead>
                <tr>
                    <th class="tableTitle" colspan="11" rowspan="1" style="height: 18px">
                        <a id="titleLinkKihonSyouhin" runat="server">基本商品</a> <span id="KihonSyouhinSpan" style="display: none;"
                            runat="server"></span>
                    </th>
                </tr>
            </thead>
            <tbody id="kihonSyouhinTbody" runat="server">
                <tr>
                    <td>
                        <table width="100%" class="gridviewTableHeader" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 300px; border-left: 1px solid black;">
                                    基本商品</td>
                                <td style="border-right: 1px solid black;">
                                    基本商品注意文</td>
                            </tr>
                        </table>
                        <asp:UpdatePanel ID="UpdatePanelKihonSyouhin" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width:306px;">
                                            <asp:DropDownList ID="ddlKihonSyouhin" runat="server" Width="270px" ></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbxKihonSyouhinTyuuibun" runat="server" Width="570px" ></asp:TextBox>
                                            <asp:Button ID="btnKihonSyouhin" runat="server" Text="登録" Width="32px" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </tbody>
        </table> 

        <table style="text-align: left; width: 960px; border: 2px solid gray; margin-top :10px;" class="mainTable2"
            cellpadding="1">
            <thead>
                <tr>
                    <th class="tableTitle" colspan="11" rowspan="1" style="height: 18px">
                        <a id="titleLinkKihonTyousaHouhou" runat="server">基本調査方法</a> <span id="KihonTyousaHouhouSpan" style="display: none;"
                            runat="server"></span>
                    </th>
                </tr>
            </thead>
            <tbody id="kihonTyousaHouhouTbody" runat="server">
                <tr>
                    <td>
                        <table width="100%" class="gridviewTableHeader" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 300px; border-left: 1px solid black;">
                                    基本調査方法</td>
                                <td style="border-right: 1px solid black;">
                                    基本調査方法注意文</td>
                            </tr>
                        </table>
                        <asp:UpdatePanel ID="UpdatePanelKihonTyousaHouhou" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width:306px;">
                                            <asp:DropDownList ID="ddlKihonTyousaHouhou" runat="server" Width="270px" ></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbxKihonTyousaHouhouTyuuibun" runat="server" Width="570px" ></asp:TextBox>
                                            <asp:Button ID="btnKihonTyousaHouhou" runat="server" Text="登録" Width="32px" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </tbody>
        </table>

        <%--基本商品＆基本調査方法 END--%>
        
        <%--通常注意事項 START--%>
        <table cellpadding="0" cellspacing="0" style="border-top-width: 0px; border-left-width: 0px;
            border-bottom-width: 0px; vertical-align: top; width: 960px; border-right-width: 0px">
            <tr>
                <td valign="top" style="width: 319px">
                    <table cellpadding="1" class="mainTable2" style="border-right: gray 2px solid; border-top: gray 2px solid;
                        margin-top: 10px; border-left: gray 2px solid; width: 318px; border-bottom: gray 2px solid;
                        text-align: left">
                        <thead>
                            <tr>
                                <th class="tableTitle" colspan="11" rowspan="1" style="border-right: gray 1px solid;
                                    border-top: gray 1px solid; border-left: gray 1px solid; border-bottom: gray 1px solid;
                                    border-collapse: collapse; height: 18px; width: 318px;">
                                    <a id="titleLink1" runat="server">調査会社</a> <span id="titleSpan1" runat="server" style="display: none">
                                    </span>
                                </th>
                            </tr>
                        </thead>
                        <tbody id="naiyouTbody1" runat="server">
                            <tr>
                                <td style="vertical-align: top">
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td style="border-right: gray 0px solid; border-top: gray 0px solid; vertical-align: top;
                                                border-left: gray 0px solid; width: 318px; border-bottom: gray 0px solid">
                                                <table cellspacing="0" class="gridviewTableHeader">
                                                    <tr>
                                                        <td style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;
                                                            width: 309px; border-bottom: black 1px solid; text-align: left">
                                                            指定調査会社
                                                        </td>
                                                    </tr>
                                                </table>
                                                <asp:UpdatePanel ID="UpdatePanel11" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btn11" runat="server" Style="display: none" Text="Button" />
                                                        <asp:GridView ID="grdNaiyou11" runat="server" BackColor="White" BorderColor="GrayText"
                                                            BorderWidth="1px" CellPadding="0" ShowHeader="False" Style="border-right: #999999 1px solid;
                                                            border-top: black 1px solid; padding-left: 2px">
                                                            <SelectedRowStyle ForeColor="White" />
                                                            <AlternatingRowStyle BackColor="LightCyan" />
                                                            <RowStyle BorderColor="#999999" BorderWidth="1px" VerticalAlign="Middle" />
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <table cellspacing="0" class="gridviewTableHeader">
                                                    <tr>
                                                        <td style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;
                                                            width: 309px; border-bottom: black 1px solid; text-align: left">
                                                            優先調査会社
                                                        </td>
                                                    </tr>
                                                </table>
                                                <asp:UpdatePanel ID="UpdatePanel13" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btn13" runat="server" Style="display: none" Text="Button" />
                                                        <asp:GridView ID="grdNaiyou13" runat="server" BackColor="White" BorderColor="GrayText"
                                                            BorderWidth="1px" CellPadding="0" ShowHeader="False" Style="border-right: #999999 1px solid;
                                                            border-top: black 1px solid; padding-left: 2px">
                                                            <SelectedRowStyle ForeColor="White" />
                                                            <AlternatingRowStyle BackColor="LightCyan" />
                                                            <RowStyle BorderColor="#999999" BorderWidth="1px" VerticalAlign="Middle" />
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <table cellspacing="0" class="gridviewTableHeader">
                                                    <tr>
                                                        <td style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;
                                                            width: 308px; border-bottom: black 1px solid; text-align: left; color: red; background-color: lightpink;">
                                                            発注禁止(NG)調査会社
                                                        </td>
                                                    </tr>
                                                </table>
                                                <asp:UpdatePanel ID="UpdatePanel19" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btn19" runat="server" Style="display: none" Text="Button" />
                                                        <asp:GridView ID="grdNaiyou19" runat="server" BackColor="White" BorderColor="GrayText"
                                                            BorderWidth="1px" CellPadding="0" ShowHeader="False" Style="border-right: #999999 1px solid;
                                                            border-top: black 1px solid; padding-left: 2px">
                                                            <SelectedRowStyle ForeColor="White" />
                                                            <AlternatingRowStyle BackColor="LightCyan" />
                                                            <RowStyle BorderColor="#999999" BorderWidth="1px" />
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <%--指定 END--%>
                </td>
                <td valign="top" style="width: 319px">
                    <%--優先 START--%>
                    <table cellpadding="1" class="mainTable2" style="border-right: gray 2px solid; border-top: gray 2px solid;
                        margin-top: 10px; border-left: gray 2px solid; width: 318px; border-bottom: gray 2px solid;
                        text-align: left">
                        <thead>
                            <tr>
                                <th class="tableTitle" colspan="11" rowspan="1" style="border-right: gray 1px solid;
                                    border-top: gray 1px solid; border-left: gray 1px solid; border-bottom: gray 1px solid;
                                    border-collapse: collapse; height: 18px; width: 318px;">
                                    <a id="titleLink13" runat="server">判定</a> <span id="titleSpan13" runat="server" style="display: none">
                                    </span>
                                </th>
                            </tr>
                        </thead>
                        <tbody id="naiyouTbody13" runat="server">
                            <tr>
                                <td style="vertical-align: top;">
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td style="border-right: gray 0px solid; border-top: gray 0px solid; vertical-align: top;
                                                border-left: gray 0px solid; width: 318px; border-bottom: gray 0px solid; height: 15px">
                                                <table cellpadding="0" cellspacing="0" class="gridviewTableHeader">
                                                    <tr>
                                                        <td style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;
                                                            width: 309px; border-bottom: black 1px solid; text-align: left">
                                                            指定判定
                                                        </td>
                                                    </tr>
                                                </table>
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btn1" runat="server" Style="display: none" Text="Button" />
                                                        <asp:GridView ID="grdNaiyou1" runat="server" BackColor="White" BorderColor="GrayText"
                                                            BorderWidth="1px" CellPadding="0" ShowHeader="False" Style="border-right: #999999 1px solid;
                                                            border-top: black 1px solid; padding-left: 2px">
                                                            <SelectedRowStyle ForeColor="White" />
                                                            <AlternatingRowStyle BackColor="LightCyan" />
                                                            <RowStyle BorderColor="#999999" BorderWidth="1px" />
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <table cellpadding="0" cellspacing="0" class="gridviewTableHeader">
                                                    <tr>
                                                        <td style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;
                                                            width: 309px; border-bottom: black 1px solid; text-align: left">
                                                            優先判定
                                                        </td>
                                                    </tr>
                                                </table>
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btn3" runat="server" Style="display: none" Text="Button" />
                                                        <asp:GridView ID="grdNaiyou3" runat="server" BackColor="White" BorderColor="GrayText"
                                                            BorderWidth="1px" CellPadding="0" ShowHeader="False" Style="border-right: #999999 1px solid;
                                                            border-top: black 1px solid; padding-left: 2px">
                                                            <SelectedRowStyle ForeColor="White" />
                                                            <AlternatingRowStyle BackColor="LightCyan" />
                                                            <RowStyle BorderColor="#999999" BorderWidth="1px" />
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <table cellpadding="0" cellspacing="0" class="gridviewTableHeader">
                                                    <tr>
                                                        <td style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;
                                                            width: 309px; border-bottom: black 1px solid; text-align: left; color: red; background-color: lightpink;">
                                                            禁止判定
                                                        </td>
                                                    </tr>
                                                </table>
                                                <asp:UpdatePanel ID="UpdatePanel9" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btn2" runat="server" Style="display: none" Text="Button" />
                                                        <asp:GridView ID="grdNaiyou9" runat="server" BackColor="White" BorderColor="GrayText"
                                                            BorderWidth="1px" CellPadding="0" ShowHeader="False" Style="border-right: #999999 1px solid;
                                                            border-top: black 1px solid; padding-left: 2px">
                                                            <SelectedRowStyle ForeColor="White" />
                                                            <AlternatingRowStyle BackColor="LightCyan" />
                                                            <RowStyle BorderColor="#999999" BorderWidth="1px" />
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <%--優先 END--%>
                </td>
                <td valign="top" style="width: 319px">
                    <%--禁止 START--%>
                    <table cellpadding="1" class="mainTable2" style="border-right: gray 2px solid; border-top: gray 2px solid;
                        margin-top: 10px; border-left: gray 2px solid; width: 318px; border-bottom: gray 2px solid;
                        text-align: left">
                        <thead>
                            <tr>
                                <th class="tableTitle" colspan="11" rowspan="1" style="border-right: gray 1px solid;
                                    border-top: gray 1px solid; border-left: gray 1px solid; border-bottom: gray 1px solid;
                                    border-collapse: collapse; height: 18px;">
                                    <a id="titleLink2" runat="server">工事会社</a> <span id="titleSpan2" runat="server" style="display: none">
                                    </span>
                                </th>
                            </tr>
                        </thead>
                        <tbody id="naiyouTbody2" runat="server">
                            <tr>
                                <td style="vertical-align: top">
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td style="border-right: gray 0px solid; border-top: gray 0px solid; vertical-align: top;
                                                border-left: gray 0px solid; width: 316px; border-bottom: gray 0px solid">
                                                <table cellpadding="0" cellspacing="0" class="gridviewTableHeader" style="width: 312px">
                                                    <tr>
                                                        <td style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;
                                                            width: 309px; border-bottom: black 1px solid; text-align: left">
                                                            指定工事会社
                                                        </td>
                                                    </tr>
                                                </table>
                                                <asp:UpdatePanel ID="UpdatePanel21" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btn21" runat="server" Style="display: none" Text="Button" />
                                                        <asp:GridView ID="grdNaiyou21" runat="server" BackColor="White" BorderColor="GrayText"
                                                            BorderWidth="1px" CellPadding="0" ShowHeader="False" Style="border-right: #999999 1px solid;
                                                            border-top: black 1px solid; padding-left: 2px">
                                                            <SelectedRowStyle ForeColor="White" />
                                                            <AlternatingRowStyle BackColor="LightCyan" />
                                                            <RowStyle BorderColor="#999999" BorderWidth="1px" />
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <table cellpadding="0" cellspacing="0" class="gridviewTableHeader" style="width: 312px">
                                                    <tr>
                                                        <td style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;
                                                            width: 309px; border-bottom: black 1px solid; text-align: left">
                                                            優先工事会社
                                                        </td>
                                                    </tr>
                                                </table>
                                                <asp:UpdatePanel ID="UpdatePanel23" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btn23" runat="server" Style="display: none" Text="Button" />
                                                        <asp:GridView ID="grdNaiyou23" runat="server" BackColor="White" BorderColor="GrayText"
                                                            BorderWidth="1px" CellPadding="0" ShowHeader="False" Style="border-right: #999999 1px solid;
                                                            border-top: black 1px solid; padding-left: 2px">
                                                            <SelectedRowStyle ForeColor="White" />
                                                            <AlternatingRowStyle BackColor="LightCyan" />
                                                            <RowStyle BorderColor="#999999" BorderWidth="1px" />
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <table cellpadding="0" cellspacing="0" class="gridviewTableHeader" style="width: 312px">
                                                    <tr>
                                                        <td style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;
                                                            width: 309px; border-bottom: black 1px solid; text-align: left; color: red; background-color: lightpink;">
                                                            発注禁止(NG)工事会社
                                                        </td>
                                                    </tr>
                                                </table>
                                                <asp:UpdatePanel ID="UpdatePanel29" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btn29" runat="server" Style="display: none" Text="Button" />
                                                        <asp:GridView ID="grdNaiyou29" runat="server" BackColor="White" BorderColor="GrayText"
                                                            BorderWidth="1px" CellPadding="0" ShowHeader="False" Style="border-right: #999999 1px solid;
                                                            border-top: black 1px solid; padding-left: 2px">
                                                            <SelectedRowStyle ForeColor="White" />
                                                            <AlternatingRowStyle BackColor="LightCyan" />
                                                            <RowStyle BorderColor="#999999" BorderWidth="1px" />
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <%--禁止 END--%>
                </td>
            </tr>
        </table>
        <table style="margin-top: 10px; text-align: left; width: 960px; border: 2px solid gray;"
            class="mainTable2" cellpadding="1">
            <thead>
                <tr>
                    <th class="tableTitle" colspan="11" rowspan="1" style="height: 18px">
                        <a id="titleLink3" runat="server">通常注意事項</a> <span id="titleSpan3" style="display: none;"
                            runat="server"></span>
                    </th>
                </tr>
            </thead>
            <tbody id="naiyouTbody3" runat="server">
                <tr>
                    <td style="vertical-align: top;">
                        <table width="100%" class="gridviewTableHeader" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 150px; border-left: 1px solid black;">
                                    種別</td>
                                <%--<td style="width: 107px;">
                                種別名
                            </td>--%>
                                <td style="width: 69px;">
                                    入力日</td>
                                <td style="width: 137px;">
                                    受付者</td>
                                <td style="border-right: 1px solid black;">
                                    内容</td>
                            </tr>
                        </table>
                        <asp:UpdatePanel ID="UpdatePanelB" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnA" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:GridView ID="grdBodyB" runat="server" BackColor="White" Style="padding-left: 2px;
                                    border-right: 1px solid #999999; border-top: 1px solid black;" BorderWidth="1px"
                                    ShowHeader="False" CellPadding="0" BorderColor="GrayText">
                                    <SelectedRowStyle ForeColor="White" />
                                    <AlternatingRowStyle BackColor="LightCyan" />
                                    <RowStyle BorderColor="#999999" BorderWidth="1px" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </tbody>
        </table>
        <%--通常注意事項 END--%>
    </div>
    <asp:HiddenField ID="hidRow" runat="server" />
    <asp:HiddenField ID="hidBtn" runat="server" />
    <asp:HiddenField ID="hidKbn" runat="server" />
    <asp:HiddenField ID="hidNo" runat="server" />
    <asp:HiddenField ID="hidDdl" runat="server" />
    <asp:HiddenField ID="hidDLMei" runat="server" />
    <asp:HiddenField ID="hidDate" runat="server" />
    <asp:HiddenField ID="hidMei" runat="server" />
    <asp:HiddenField ID="hidNaiyou" runat="server" />
    <asp:HiddenField ID="hidRowTime" runat="server" />
    <asp:HiddenField ID="hidFirstNameKyoutu" runat="server" />
    <asp:HiddenField runat="server" ID="hidFile" />
    <a id="file" style="display: none;" runat="server">取引条件確認表</a>
    <%--    <asp:DropDownList ID="DropDownList1" runat="server" Height="58px" Width="312px" >
        <asp:ListItem Value="1"></asp:ListItem>
        <asp:ListItem Value="2"></asp:ListItem>
        <asp:ListItem Value="2"></asp:ListItem>
        <asp:ListItem Value="3"></asp:ListItem>
        <asp:ListItem Value="4"></asp:ListItem>
        <asp:ListItem Value="5"></asp:ListItem>
        <asp:ListItem Value="6"></asp:ListItem>
        <asp:ListItem Value="7"></asp:ListItem>
        <asp:ListItem Value="8"></asp:ListItem>
        <asp:ListItem Value="9"></asp:ListItem>
    </asp:DropDownList>--%>
</asp:Content>

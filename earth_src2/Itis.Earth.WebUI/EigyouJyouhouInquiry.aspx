<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master"
    Codebehind="EigyouJyouhouInquiry.aspx.vb" Inherits="Itis.Earth.WebUI.EigyouJyouhouInquiry"
    Title="加盟店営業情報検索" %>

<%@ Register Src="~/control/common_drop.ascx" TagName="common_drop" TagPrefix="uc1" %>
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
        width: 1002px; height: 616px; z-index: 100; filter: alpha(opacity=70); background-color: #000000;
        display: none;">
        <iframe src="about:blank" id="hiddenIframe" width="100%" height="100%"></iframe>
    </div>
    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tbody>
            <tr>
                <th>
                    加盟店営業情報検索
                </th>
                <th style="text-align: right;">
                </th>
            </tr>
            <tr>
                <td colspan="2" rowspan="1">
                </td>
            </tr>
        </tbody>
    </table>
    <table style="text-align: left;" class="mainTable paddinNarrow" cellpadding="1" width="960px">
        <thead>
            <tr>
                <th class="tableTitle" colspan="4" rowspan="1">
                    検索条件
                </th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td class="hissu" style="font-weight: bold;">
                    区分</td>
                <td colspan="3" class="hissu">
                    <uc1:common_drop ID="Common_drop1" runat="server" GetStyle="kubun" />
                    <uc1:common_drop ID="Common_drop2" runat="server" GetStyle="kubun" />
                    <uc1:common_drop ID="Common_drop3" runat="server" GetStyle="kubun" />
                    &nbsp;
                    <asp:CheckBox runat="server" ID="chkKubunAll" Text="全区分" TextAlign="left" Checked="true" />
                    <asp:CheckBox runat="server" ID="chkTaikai" Text="退会した加盟店を表示する" />
                </td>
            </tr>
            <tr>
                <td style="width: 90px;" class="koumokuMei">
                    加盟店コード</td>
                <td style="width: 270px">
                    <asp:TextBox runat="server" ID="tbxKameitenCd" MaxLength="5" Style="width: 93px;"
                        CssClass="codeNumber" />
                </td>
                <td style="width: 90px;" class="koumokuMei">
                    加盟店カナ</td>
                <td style="width: 270px; border-left-color: White; border-right-color: White;">
                    <asp:TextBox runat="server" ID="tbxKameitenKana" MaxLength="40" Style="width: 160px;" />
                </td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    系列コード</td>
                <td style="border-right-color: White; border-right-color: White;" colspan="3">
                    <asp:TextBox ID="tbxKeiretuCd" runat="server" MaxLength="5" Style="width: 93px;"
                        CssClass="codeNumber"></asp:TextBox>
                    <asp:Button ID="btnKeiretuSearch" runat="server" Text="検索" OnClientClick="return fncKeiretuSearch();" />
                    <asp:TextBox ID="tbxKeiretuMei" runat="server" BorderStyle="None" BackColor="#E6E6E6"
                        Width="438px" TabIndex="-1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    都道府県</td>
                <td>
                    <uc1:common_drop ID="Common_drop" runat="server" GetStyle="todoufuken" />
                </td>
                <td class="koumokuMei">
                    電話番号</td>
                <td style="border-right-color: White; border-right-color: White;">
                    <asp:TextBox runat="server" ID="tbxTel" MaxLength="16" Style="width: 160px;" CssClass="codeNumber" />
                </td>
            </tr>
            <tr runat="server" id="trDdl">
                <td style="color: Red;" class="koumokuMei">
                    組織レベル</td>
                <td>
                    <asp:DropDownList ID="ddlSosikiLevel" runat="server" Width="100px" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td style="color: Red;" class="koumokuMei">
                    部署コード</td>
                <td colspan="3">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlBusyoCd" runat="server" Width="166px">
                                <asp:ListItem Value="" Text="" Selected="true"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:CheckBox ID="chkBusyoCd" runat="server" />対象部署のみ抽出する
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlSosikiLevel" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td style="color: Red;" class="koumokuMei">
                    担当営業ID</td>
                <td colspan="3" style="border-right-color: White; border-right-color: White;">
                    <asp:TextBox runat="server" ID="tbxTantouEigyouID" MaxLength="64" Style="width: 93px;"
                        CssClass="codeNumber" Text="" />
                    <asp:Button runat="server" ID="btnTantouKensaku" Text="担当検索" OnClientClick="return fncUserSearch();" />
                    <asp:TextBox ID="tbxTantouEigyouSyaMei" runat="server" BorderStyle="None" BackColor="#E6E6E6"
                        Width="307px" TabIndex="-1"></asp:TextBox>
                </td>
            </tr>
            <tr class="tableSpacer">
                <td colspan="4">
                </td>
            </tr>
            <tr>
                <td colspan="4" rowspan="1" style="text-align: center; border-right-color: White;">
                    検索上限件数
                    <asp:DropDownList runat="server" ID="ddlSearchCount">
                        <asp:ListItem Value="100" Text="100件" Selected="true"></asp:ListItem>
                        <asp:ListItem Value="max" Text="無制限"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button runat="server" ID="btnKensaku" Text="検索実行" />
                    <asp:Button runat="server" ID="btnClearWin" Text="条件クリア" />
                </td>
            </tr>
        </tbody>
    </table>
    <table style="text-align: left; width: 100px;" border="0" cellpadding="0" cellspacing="0">
    </table>
    <table style="height: 30px">
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
                <div id="divHeadLeft" style="width: 398px; overflow-y: hidden; overflow-x: hidden;">
                    <table class="gridviewTableHeader" cellpadding="0" cellspacing="0">
                        <tr style="vertical-align: middle;">
                            <td style="width: 22px; border-left: 1px solid black;">
                                &nbsp;</td>
                            <td style="width: 98px;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2" style="border-color: #FFFFD9;">
                                            担当部署</td>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: bottom; width: 16px;">
                                            <asp:LinkButton runat="server" ID="busyo_mei1" Text="▲" Height="14px" TabIndex="-1"
                                                ForeColor="SkyBlue" Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: top; width: 16px;
                                            height: 11px;">
                                            <asp:LinkButton runat="server" ID="busyo_mei2" Text="▼" Height="14px" TabIndex="-1"
                                                ForeColor="SkyBlue" Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 98px;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2" style="border-color: #FFFFD9;">
                                            営業担当者</td>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: bottom; width: 16px;">
                                            <asp:LinkButton runat="server" ID="DisplayName1" Text="▲" Height="14px" TabIndex="-1"
                                                ForeColor="SkyBlue" Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: top; width: 16px;
                                            height: 11px;">
                                            <asp:LinkButton runat="server" ID="DisplayName2" Text="▼" Height="14px" TabIndex="-1"
                                                ForeColor="SkyBlue" Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 98px;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2" style="border-color: #FFFFD9;">
                                            加盟店ｺｰﾄﾞ</td>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: bottom; width: 16px;">
                                            <asp:LinkButton runat="server" ID="kameiten_cd1" Text="▲" Height="14px" TabIndex="-1"
                                                ForeColor="SkyBlue" Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: top; width: 16px;
                                            height: 11px;">
                                            <asp:LinkButton runat="server" ID="kameiten_cd2" Text="▼" Height="14px" TabIndex="-1"
                                                ForeColor="SkyBlue" Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 75px;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2" style="border-color: #FFFFD9;">
                                            取消</td>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: bottom; width: 16px;">
                                            <asp:LinkButton runat="server" ID="torikesi1" Text="▲" Height="14px" TabIndex="-1"
                                                ForeColor="SkyBlue" Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: top; width: 16px;
                                            height: 11px;">
                                            <asp:LinkButton runat="server" ID="torikesi2" Text="▼" Height="14px" TabIndex="-1"
                                                ForeColor="SkyBlue" Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <%------------------------------↓2013.03.09李宇修正する------------------------------%>
            <td style="width: 534px">
                <div id="divHeadRight" runat="server" style="width: 550px; overflow-y: hidden; overflow-x: hidden;
                    border-right: 1px solid black;">
                    <table class="gridviewTableHeader" cellpadding="0" cellspacing="0" width="1907px">
                        <tr style="">
                            <td style="width: 274px;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2" style="border-color: #FFFFD9;">
                                            加盟店名1</td>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: bottom; width: 16px;">
                                            <asp:LinkButton runat="server" ID="kameiten_mei1" Text="▲" Height="14px" TabIndex="-1"
                                                ForeColor="SkyBlue" Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: top; width: 16px;
                                            height: 11px;">
                                            <asp:LinkButton runat="server" ID="kameiten_mei2" Text="▼" Height="14px" TabIndex="-1"
                                                ForeColor="SkyBlue" Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 73px;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2" style="border-color: #FFFFD9;">
                                            都道府県</td>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: bottom; width: 16px;">
                                            <asp:LinkButton runat="server" ID="todouhuken_mei1" Text="▲" Height="14px" TabIndex="-1"
                                                ForeColor="SkyBlue" Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: top; width: 16px;
                                            height: 11px;">
                                            <asp:LinkButton runat="server" ID="todouhuken_mei2" Text="▼" Height="14px" TabIndex="-1"
                                                ForeColor="SkyBlue" Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 90px;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2" style="border-color: #FFFFD9;">
                                            調査</td>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: bottom; width: 16px;">
                                            <asp:LinkButton runat="server" ID="tyousa_cd1" Text="▲" Height="14px" TabIndex="-1"
                                                ForeColor="SkyBlue" Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: top; width: 16px;
                                            height: 11px;">
                                            <asp:LinkButton runat="server" ID="tyousa_cd2" Text="▼" Height="14px" TabIndex="-1"
                                                ForeColor="SkyBlue" Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 93px;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2" style="border-color: #FFFFD9;">
                                            工事</td>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: bottom; width: 16px;">
                                            <asp:LinkButton runat="server" ID="kouji_cd1" Text="▲" Height="14px" TabIndex="-1"
                                                ForeColor="SkyBlue" Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: top; width: 16px;
                                            height: 11px;">
                                            <asp:LinkButton runat="server" ID="kouji_cd2" Text="▼" Height="14px" TabIndex="-1"
                                                ForeColor="SkyBlue" Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 82px; border-right: 1px solid black;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2" style="border-color: #FFFFD9;">
                                            その他</td>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: bottom; width: 16px;">
                                            <asp:LinkButton runat="server" ID="hansokuhin_cd1" Text="▲" Height="14px" TabIndex="-1"
                                                ForeColor="SkyBlue" Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: top; width: 16px;
                                            height: 11px;">
                                            <asp:LinkButton runat="server" ID="hansokuhin_cd2" Text="▼" Height="14px" TabIndex="-1"
                                                ForeColor="SkyBlue" Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
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
                                            <asp:LinkButton runat="server" ID="kameiten_mei21" Text="▲" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: top;">
                                            <asp:LinkButton runat="server" ID="kameiten_mei22" Text="▼" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 374px; text-align: center;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2" style="border-color: #FFFFD9;">
                                            住所&nbsp;</td>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: bottom;">
                                            <asp:LinkButton runat="server" ID="jyuusyo11" Text="▲" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: top;">
                                            <asp:LinkButton runat="server" ID="jyuusyo12" Text="▼" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
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
                                            <asp:LinkButton runat="server" ID="keiretu_cd1" Text="▲" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: top;">
                                            <asp:LinkButton runat="server" ID="keiretu_cd2" Text="▼" Height="14px" TabIndex="-1"
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
                                            <asp:LinkButton runat="server" ID="eigyousyo_cd1" Text="▲" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: top;">
                                            <asp:LinkButton runat="server" ID="eigyousyo_cd2" Text="▼" Height="14px" TabIndex="-1"
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
                                            <asp:LinkButton runat="server" ID="builder_no1" Text="▲" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: top;">
                                            <asp:LinkButton runat="server" ID="builder_no2" Text="▼" Height="14px" TabIndex="-1"
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
                                            <asp:LinkButton runat="server" ID="daihyousya_mei1" Text="▲" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: top;">
                                            <asp:LinkButton runat="server" ID="daihyousya_mei2" Text="▼" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
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
                                            <asp:LinkButton runat="server" ID="tel_no1" Text="▲" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: top;">
                                            <asp:LinkButton runat="server" ID="tel_no2" Text="▼" Height="14px" TabIndex="-1"
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
            <td style="width: 397px">
                <div id="divBodyLeft" runat="server" onmousewheel="wheel();" style="width: 397px;
                    height: 154px; overflow-x: hidden; overflow-y: hidden; border-left: 1px solid black;
                    border-top: 1px solid black; border-bottom: 1px solid black; border-right: -1px solid gray;">
                    <asp:GridView ID="grdItiran" runat="server" AutoGenerateColumns="False" BackColor="White"
                        CssClass="tableMeiSai" BorderWidth="1px" ShowHeader="False" CellPadding="0" Width="397px">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <input type="radio" name="rdo_SetHoshouNo" style="width: 17px;" onclick="fncSetLineColor(event.srcElement.parentNode.parentNode,event.srcElement.parentNode.parentNode.rowIndex);fncSetKameitenCd();funDisableButton();" />
                                </ItemTemplate>
                                <ItemStyle Width="17px" BorderColor="#999999" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="busyo_mei">
                                <ItemStyle Height="21px" Width="95px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DisplayName">
                                <ItemStyle Width="102px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="kameiten_cd">
                                <ItemStyle Width="100px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblTorikesi" runat="server" Width="60px" Text='<%#Eval("torikesi_txt")%>'
                                        ToolTip='<%#Eval("torikesi_txt")%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                    <asp:HiddenField ID="hidKbn" Value='<%#Eval("kbn")%>' runat="server" />
                                </ItemTemplate>
                                <ItemStyle Width="67px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:TemplateField>
                        </Columns>
                        <SelectedRowStyle ForeColor="White" />
                        <AlternatingRowStyle BackColor="LightCyan" />
                    </asp:GridView>
                </div>
            </td>
            <td style="width: 550px">
                <div id="divBodyRight" runat="server" onmousewheel="wheel();" style="width: 550px;
                    height: 154px; overflow-x: hidden; overflow-y: hidden; border-right: 1px solid black;
                    border-top: 1px solid black; border-bottom: 1px solid black;">
                    <asp:GridView ID="grdItiranRight" runat="server" AutoGenerateColumns="False" BackColor="White"
                        CssClass="tableMeiSai" Width="1907px" BorderWidth="1px" ShowHeader="False" CellPadding="0">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblKameitenMei" runat="server" Width="250px" Text='<%#Eval("kameiten_mei")%>'
                                        ToolTip='<%#Eval("kameiten_mei")%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="258px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="todouhuken_mei">
                                <ItemStyle Width="82px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tyousa">
                                <ItemStyle Width="89px" HorizontalAlign="Left" BorderColor="#999999" Height="21px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="kouji">
                                <ItemStyle Width="92px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="hansokuhin">
                                <ItemStyle Width="83px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblKameitenMei2" runat="server" Width="250px" Text='<%#Eval("kameiten_mei2")%>'
                                        ToolTip='<%#Eval("kameiten_mei2")%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="256px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="jyuusyo1">
                                <ItemStyle Width="349px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="keiretu_cd">
                                <ItemStyle Width="111px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="eigyousyo_cd">
                                <ItemStyle Width="121px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="builder_no">
                                <ItemStyle Width="112px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="daihyousya_mei">
                                <ItemStyle Width="156px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tel_no">
                                <ItemStyle Width="147px" HorizontalAlign="Left" BorderColor="#999999" />
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
            <td style="width: 400px">
                <div style="overflow-x: hidden; height: 18px; width: 550px; margin-top: -1px;">
                    <div id="divHiddenMeisaiH" runat="server" style="overflow: auto; height: 18px; width: 566px;
                        margin-top: 0px;" onscroll="fncScrollH();">
                        <table style="width: 2190px;">
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
    <%------------------------------↑2013.03.09李宇修正する------------------------------%>
    <table style="width: 889px; margin-top: 15px;" cellspacing="0" cellpadding="0" border="0">
        <tr>
            <td style="height: 25px; border-left: 0px white; border-right: 0px white;">
                <asp:Button runat="server" ID="btnClose" Text="閉じる" OnClientClick="fncGamenSenni('0');return false;"
                    Width="85px" />
            </td>
            <td style="width: 76px; height: 24px; padding-left: 20px;">
                情報照会</td>
            <td style="height: 25px; border-right: 0px white; width: 70px;">
                <asp:Button runat="server" ID="btnKihonJyouhou" Text="基本" OnClientClick="fncGamenSenni('1');return false;"
                    Width="60px" Height="22px" Disabled />
            </td>
            <td style="height: 25px; border-right: 0px white; width: 70px;">
                <asp:Button runat="server" ID="btnTyuiJikou" Text="注意" OnClientClick="fncGamenSenni('2');return false;"
                    Width="60px" Height="22px" Disabled />
            </td>
            <td style="height: 25px; border-right: 0px white; width: 70px;">
                <asp:Button runat="server" ID="btnBukkenJyouhou" Text="物件" OnClientClick="fncGamenSenni('3');return false;"
                    Width="60px" Height="22px" Disabled />
            </td>
            <td style="height: 25px; border-right: 0px white; width: 70px;">
                <asp:Button runat="server" ID="btnYosinJyouhou" Text="与信" OnClientClick="fncGamenSenni('4');return false;"
                    Width="60px" Height="22px" Disabled />
            </td>
            <td style="height: 25px; border-right: 0px white; width: 70px;">
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
    <table cellspacing="0" cellpadding="0" border="0" style="margin-left: 101px; margin-top: 5px;">
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
    <asp:HiddenField ID="hidSentakuKameitenCd" runat="server" />
    <asp:HiddenField runat="server" ID="hidScroll" />
    <input type="hidden" id="hidKbnCd" />
    <asp:HiddenField runat="server" ID="hidFile" />
    <asp:HiddenField runat="server" ID="hidSentakuKbn" />
    <asp:HiddenField runat="server" ID="hidSelectRowIndex" />
    <a id="file" style="display: none;" runat="server">取引条件確認表</a>
</asp:Content>

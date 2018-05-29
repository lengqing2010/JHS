<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master"
    Codebehind="BukkenJyouhouSearch.aspx.vb" Inherits="Itis.Earth.WebUI.BukkenJyouhouSearch"
    Title="物件情報検索" %>

<%@ Register Src="control/common_drop.ascx" TagName="common_drop" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js"></script>

    <script type="text/javascript">
        //window名付与
        var objWin = window;
        objWin.name = "earthMainWindow4"
        initPage(); //画面初期設定
   function window.onload(){
        fncSetKubunVal();
    }
    </script>

    <div id="buySelName" runat="server" class="modalDiv" style="position: absolute; left: 300px;
        top: 140px; z-index: 2; display: none;">
    </div>
    <div id="disableDiv" runat="server" style="position: absolute; left: 0px; top: 0px;
        width: 1002px; height: 596px; z-index: 100; filter: alpha(opacity=70); background-color: #000000;
        display: none;">
        <iframe src="about:blank" id="hiddenIframe" width="100%" height="100%"></iframe>
    </div>
    <!--明細-->
    <table class="titleTable" border="0" style="width: 960px; vertical-align: top;" cellpadding="0"
        cellspacing="0">
        <tr style="height: 20px;">
            <!-- TITLEの字 -->
            <th rowspan="1" style="width: 628px; text-align: left; vertical-align: top;">
                物件情報検索 &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            </th>
            <td style="width: 64px;">
            </td>
            <td style="width: 200px;">
            </td>
        </tr>
    </table>
    <table id="Table1" style="width: 960px; text-align: left;" border="0px">
        <tr>
            <td style="width: 9px; height: 15px;">
            </td>
            <td style="height: 15px">
                【必須条件】</td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <table id="tblKensaku" style="width: 940px; text-align: left;" class="mainTable paddinNarrow"
                    cellpadding="1">
                    <tr style="height: 24px;">
                        <td class="hissu" style="font-weight: bold; width: 120px;">
                            区分</td>
                        <td colspan="5" class="hissu">
                            <uc1:common_drop ID="Common_drop1" runat="server" GetStyle="kubun" />
                            <uc1:common_drop ID="Common_drop2" runat="server" GetStyle="kubun" />
                            <uc1:common_drop ID="Common_drop3" runat="server" GetStyle="kubun" />
                            &nbsp;
                            <asp:CheckBox runat="server" ID="chkKubunAll" Text="全区分" TextAlign="left" Checked="true" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 14px">
            </td>
            <td style="height: 14px">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                【半必須条件（いずれか必須）】</td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <table id="Table2" style="width: 800px; text-align: left;" class="mainTable paddinNarrow"
                    cellpadding="1">
                    <tr class="shouhinTableTitle">
                        <td style="font-weight: bold; width: 120px; height: 24px; text-align: left;">
                            番号</td>
                        <td colspan="5" style="height: 24px; text-align: left;">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtBangouF" runat="server" Width="80px"></asp:TextBox>～<asp:TextBox
                                        ID="txtBangouT" runat="server" Width="80px"></asp:TextBox>
                                    &nbsp; &nbsp;
                                    <asp:Button ID="btn12" runat="server" Text="過去12ヶ月" Width="85px" />
                                    <asp:Button ID="btn6" runat="server" Text="過去6ヶ月" Width="85px" />
                                    <asp:Button ID="btn3" runat="server" Text="過去3ヶ月" Width="85px" />
                                    <asp:Button ID="btn2" runat="server" Text="過去2ヶ月" Width="85px" />
                                    <asp:Button ID="btn1" runat="server" Text="当月" Width="85px" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table id="Table15" style="margin-top: -2px; width: 960px; text-align: left;" border="0px">
        <tr>
            <td style="width: 9px;">
            </td>
            <td style="height: 15px; width: 383px;">
                <table id="Table3" style="width: 340px; text-align: left;" class="mainTable paddinNarrow"
                    cellpadding="1">
                    <tr class="shouhinTableTitle">
                        <td style="font-weight: bold; width: 120px; height: 24px; text-align: left;">
                            依頼日</td>
                        <td colspan="5" style="height: 24px; text-align: left;">
                            <asp:TextBox ID="tbxIraiF" runat="server" Width="80px"></asp:TextBox>～<asp:TextBox
                                ID="tbxIraiT" runat="server" Width="80px"></asp:TextBox></td>
                    </tr>
                </table>
            </td>
            <td>
                <table id="Table17" style="width: 340px; text-align: left;" class="mainTable paddinNarrow"
                    cellpadding="1">
                    <tr class="shouhinTableTitle">
                        <td style="font-weight: bold; width: 120px; height: 24px; text-align: left;">
                            計画書作成日</td>
                        <td colspan="5" style="height: 24px; text-align: left;">
                            <asp:TextBox ID="tbxKeikakusyoF" runat="server" Width="80px"></asp:TextBox>～<asp:TextBox
                                ID="tbxKeikakusyoT" runat="server" Width="80px"></asp:TextBox></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table id="Table16" style="margin-top: -2px; width: 960px; text-align: left;" border="0px">
        <tr>
            <td style="width: 9px; height: 15px;">
            </td>
            <td>
                <table id="Table4" style="width: 940px; text-align: left;" class="mainTable paddinNarrow"
                    cellpadding="1">
                    <tr class="shouhinTableTitle">
                        <td style="font-weight: bold; width: 120px; height: 24px; text-align: left;">
                            調査</td>
                        <td style="height: 24px; text-align: left; width: 260px;">
                            予定日<asp:TextBox ID="tbxYoteiF" runat="server" Width="80px" Style="vertical-align: middle;"></asp:TextBox>～<asp:TextBox
                                ID="tbxYoteiT" runat="server" Width="80px" Style="vertical-align: middle;"></asp:TextBox></td>
                        <td style="height: 24px; text-align: left; width: 260px;">
                            実施日<asp:TextBox ID="tbxJissiF" runat="server" Width="80px" Style="vertical-align: middle;"></asp:TextBox>～<asp:TextBox
                                ID="tbxJissiT" runat="server" Width="80px" Style="vertical-align: middle;"></asp:TextBox></td>
                        <td style="height: 24px; text-align: left;">
                            売上日<asp:TextBox ID="tbxUriageF" runat="server" Width="80px" Style="vertical-align: middle;"></asp:TextBox>～<asp:TextBox
                                ID="tbxUriageT" runat="server" Width="80px" Style="vertical-align: middle;"></asp:TextBox></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <table id="Table5" style="width: 940px; text-align: left;" class="mainTable paddinNarrow"
                    cellpadding="1">
                    <tr class="shouhinTableTitle">
                        <td style="font-weight: bold; width: 120px; height: 24px; text-align: left;">
                            工事（追加工事含）</td>
                        <td style="height: 24px; text-align: left; width: 260px; vertical-align: middle;"
                            valign="middle">
                            予定日<asp:TextBox ID="tbxKYoteiF" runat="server" Style="vertical-align: middle" Width="80px"></asp:TextBox>～<asp:TextBox
                                ID="tbxKYoteiT" runat="server" Style="vertical-align: middle" Width="80px"></asp:TextBox></td>
                        <td style="height: 24px; text-align: left; width: 260px;">
                            実施日<asp:TextBox ID="tbxKJissiF" runat="server" Style="vertical-align: middle" Width="80px"></asp:TextBox>～<asp:TextBox
                                ID="tbxKJissiT" runat="server" Style="vertical-align: middle" Width="80px"></asp:TextBox></td>
                        <td style="height: 24px; text-align: left;">
                            売上日<asp:TextBox ID="tbxKUriageF" runat="server" Style="vertical-align: middle" Width="80px"></asp:TextBox>～<asp:TextBox
                                ID="tbxKUriageT" runat="server" Style="vertical-align: middle" Width="80px"></asp:TextBox>※</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td style="text-align: right">
                <asp:CheckBox ID="chkKouji" runat="server" Text="※工事会社請求分は含まない" Font-Bold="True"
                    Checked="True" /></td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                【任意条件】
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <table id="Table6" style="width: 696px; text-align: left;" class="mainTable paddinNarrow"
                    cellpadding="1">
                    <tr class="shouhinTableTitle" style="background-color: #ccffff;">
                        <td style="font-weight: bold; width: 120px; height: 24px; text-align: left;">
                            加盟店</td>
                        <td style="height: 24px; text-align: left;">
                            <asp:TextBox ID="tbxKameitenCd1" runat="server" Width="80px"></asp:TextBox>
                            <asp:Button ID="btnKameitenSearch1" runat="server" Text="検索" Width="40px" OnClientClick="return fncKameitenSearch('1');" />
                            <asp:TextBox ID="tbxKameitenMei" runat="server" Width="280px" BackColor="Transparent"
                                CssClass="readOnlyStyle" TabIndex="-1"></asp:TextBox>
                        </td>
                        <td style="width: 35px;">
                            取消
                        </td>
                        <td style="background-color: #E0E0E0; width: 105px; text-align: left;">
                            <asp:TextBox ID="tbxTorikesi" runat="server" Width="100px" CssClass="readOnlyStyle"
                                TabIndex="-1" Style="border-bottom: none;"></asp:TextBox>
                            <asp:HiddenField ID="hidTorikesi" runat="server" />
                            <input type="button" id ="btnChangeColor" style="display:none;" onclick="fncChangeColor();" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <table id="Table7" style="width: 550px; text-align: left;" class="mainTable paddinNarrow"
                    cellpadding="1">
                    <tr class="shouhinTableTitle" style="background-color: #ccffff;">
                        <td style="font-weight: bold; width: 120px; height: 24px; text-align: left;">
                            都道府県</td>
                        <td style="height: 24px; text-align: left;">
                            <uc1:common_drop ID="Common_drop4" runat="server" GetStyle="todoufuken" Style="vertical-align: middle" />
                            &nbsp;OR &nbsp;<uc1:common_drop ID="Common_drop5" runat="server" GetStyle="todoufuken"
                                Style="vertical-align: middle" />
                            &nbsp;OR &nbsp;<uc1:common_drop ID="Common_drop6" runat="server" GetStyle="todoufuken"
                                Style="vertical-align: middle" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <table id="Table8" style="width: 550px; text-align: left;" class="mainTable paddinNarrow"
                    cellpadding="1">
                    <tr class="shouhinTableTitle" style="background-color: #ccffff;">
                        <td style="font-weight: bold; width: 120px; height: 24px; text-align: left;">
                            系列</td>
                        <td style="height: 24px; text-align: left;">
                            <asp:TextBox ID="tbxKeiretuCd" runat="server" Width="80px"></asp:TextBox><asp:Button
                                ID="btnKeiretuSearch" runat="server" Text="検索" Width="40px" OnClientClick="return fncKeiretuSearch();" />
                            <asp:TextBox ID="tbxKeiretuMei" runat="server" BackColor="Transparent" CssClass="readOnlyStyle"
                                TabIndex="-1" Width="280px"></asp:TextBox></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <table id="Table9" style="width: 550px; text-align: left;" class="mainTable paddinNarrow"
                    cellpadding="1">
                    <tr class="shouhinTableTitle" style="background-color: #ccffff;">
                        <td style="font-weight: bold; width: 120px; height: 24px; text-align: left;">
                            営業所（FC）</td>
                        <td style="height: 24px; text-align: left;">
                            <asp:TextBox ID="tbxEigyousyoCd" runat="server" Width="80px"></asp:TextBox><asp:Button
                                ID="btnEigyousyoSearch" runat="server" Text="検索" Width="40px" OnClientClick="return fncEigyousyoSearch('1');" />
                            <asp:TextBox ID="tbxEigyousyoMei" runat="server" BackColor="Transparent" TabIndex="-1"
                                CssClass="readOnlyStyle" Width="280px"></asp:TextBox></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table id="Table11" style="margin-top: -2px; width: 960px; text-align: left;" border="0px">
        <tr>
            <td style="width: 9px; height: 15px;">
            </td>
            <td style="width: 300px;">
                <table id="Table12" style="width: 260px; text-align: left;" class="mainTable paddinNarrow"
                    cellpadding="1">
                    <tr class="shouhinTableTitle" style="background-color: #ccffff;">
                        <td style="font-weight: bold; width: 120px; height: 24px; text-align: left; color: red;">
                            組織レベル</td>
                        <td style="height: 24px; text-align: left;">
                            <asp:DropDownList ID="ddlSosikiLevel" runat="server" AutoPostBack="true" Width="100px">
                            </asp:DropDownList></td>
                    </tr>
                </table>
            </td>
            <td>
                <table id="Table14" style="width: 460px; text-align: left;" class="mainTable paddinNarrow"
                    cellpadding="1">
                    <tr class="shouhinTableTitle" style="background-color: #ccffff;">
                        <td style="font-weight: bold; width: 120px; height: 24px; text-align: left; color: red;">
                            部署コード</td>
                        <td style="height: 24px; text-align: left;">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlBusyoCd" runat="server" Width="166px">
                                        <asp:ListItem Value="" Text="" Selected="true"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:CheckBox ID="chkBusyoCd" runat="server" Text="対象部署のみ抽出する" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlSosikiLevel" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table id="Table13" style="margin-top: -9px; width: 960px; text-align: left;" border="0px">
        <tr>
            <td style="width: 9px; height: 46px;">
            </td>
            <td style="height: 46px">
                <table id="Table10" style="width: 430px; text-align: left;" class="mainTable paddinNarrow"
                    cellpadding="1">
                    <tr class="shouhinTableTitle" style="background-color: #ccffff;">
                        <td style="font-weight: bold; width: 120px; height: 24px; text-align: left; color: red;">
                            担当営業ID</td>
                        <td style="height: 24px; text-align: left;">
                            <asp:TextBox ID="tbxTantouEigyouID" runat="server" Width="80px"></asp:TextBox><asp:Button
                                ID="Button9" runat="server" Text="検索" Width="40px" OnClientClick="return fncUserSearch();" />
                            <asp:TextBox ID="tbxTantouEigyouSyaMei" runat="server" BackColor="Transparent" TabIndex="-1"
                                CssClass="readOnlyStyle" Width="160px"></asp:TextBox></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                検索上限件数
                <asp:DropDownList ID="ddlSearchCount" runat="server">
                    <asp:ListItem Selected="true" Text="30件" Value="30"></asp:ListItem>
                    <asp:ListItem Text="100件" Value="100"></asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnKensaku" runat="server" Text="検索実行" />
                <asp:Button ID="btnClearWin" runat="server" Text="条件クリア" OnClientClick="fncClearWin();" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hidkennsuu" runat="server" />
</asp:Content>

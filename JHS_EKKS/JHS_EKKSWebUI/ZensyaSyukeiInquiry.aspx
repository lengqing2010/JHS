<%@ Page Language="VB" MasterPageFile="~/EKKSMaster.master" AutoEventWireup="false"
    CodeFile="ZensyaSyukeiInquiry.aspx.vb" Inherits="ZensyaSyukeiInquiry" Title="全社集計" %>

<%@ Register Src="CommonControl/CommonButton.ascx" TagName="CommonButton" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/EKKSMaster.master" %>
<%--<%@ Register Assembly="Lixil.JHS_EKKS.Utilities" Namespace="Lixil.JHS_EKKS.Utilities"
    TagPrefix="cc1" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        //onload
        window.onload = function(){
            window.name = "<%=CommonConstBC.uriageYojituKanri%>"
            setMenuBgColor();
        }
    </script>

    <table style="margin-left: 35px; margin-top: 25px;" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <asp:Label ID="lblGamenMei" runat="server" Text="全社 集計" Style="font-size: 17px; font-weight: bold"></asp:Label>
            </td>
        </tr>
    </table>
    <div id="divNendo" style="height: 125px; width: 860px; border-right: 2px solid; border-top: 2px solid;
        border-left: 2px solid; border-bottom: 2px solid; margin-left: 20px; left: 16px;
        position: relative; top: 10px; visibility: visible; background-color: white;">
        <table cellpadding="0" cellspacing="0" style="margin-top: 10px; margin-left: 20px"
            width="450px">
            <tr>
                <td style="width: 120px">
                    <asp:Label ID="lblNendo" runat="server" Text="年度別集計" Style="font-size: 15px; font-weight: bold"></asp:Label></td>
                <td style="width: 120px">
                    <asp:DropDownList ID="ddlNendoNendo" runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
                <td style="width: 90px">
                    <uc1:CommonButton ID="btnHyouji" runat="server" Text="表示" />
                </td>
                <td>
                    <uc1:CommonButton ID="btnSyosai" runat="server" Text="詳細" Visible="false" />
                    &nbsp;
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" style="border: solid 1px gray;
            margin-left: 20px; margin-top: 20px;" width="830px">
            <tr>
                <td align="center" style="border-right: solid 1px gray; background-color: #ccecff;
                    width: 100px; font-weight: bold; height: 22px;">
                    計画 調査件数</td>
                <td style="background-color: #e6e6e6; width: 180px; height: 22px; text-align: right;">
                    <asp:Label ID="lblKeikakuKensuu" runat="server" Width="120px" Style="margin-right: 5px;"></asp:Label>
                    <asp:Label ID="lblKeiKakuIjyou" runat="server" Text="件以上" Style="margin-right: 5px;"></asp:Label>
                </td>
                <td align="center" style="border-left: solid 1px gray; border-right: solid 1px gray;
                    background-color: #ccecff; width: 100px; font-weight: bold; height: 22px;">
                    計画 売上金額</td>
                <td style="background-color: #e6e6e6; width: 180px; height: 22px; text-align: right;">
                    <asp:Label ID="lblKeikakuUriKingaku" runat="server" Width="120px" Style="margin-right: 5px;"></asp:Label>
                    <asp:Label ID="lblKeikakuEn" runat="server" Style="margin-right: 5px;" Text="円"></asp:Label></td>
                <td align="center" style="border-left: solid 1px gray; border-right: solid 1px gray;
                    background-color: #ccecff; width: 100px; font-weight: bold; height: 22px;">
                    計画 粗利金額</td>
                <td style="background-color: #e6e6e6; height: 22px; text-align: right;">
                    <asp:Label ID="lblKeikakuArari" runat="server" Width="120px" Style="margin-right: 5px;"></asp:Label>
                    <asp:Label ID="lblNendoKinEn" runat="server" Style="margin-right: 5px;" Text="円"></asp:Label></td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" style="border: solid 1px gray;
            margin-left: 20px; margin-top: 10px;" width="830px">
            <tr>
                <td align="center" style="border-right: solid 1px gray; background-color: #ccecff;
                    width: 100px; font-weight: bold; height: 22px;">
                    実績 調査件数</td>
                <td style="background-color: #e6e6e6; width: 180px; height: 22px; text-align: right;">
                    <asp:Label ID="lblJissekiKensuu" runat="server" Width="120px" Style="margin-right: 5px;"></asp:Label>
                    <asp:Label ID="lblKen" runat="server" Text="件" Style="margin-right: 30px;"></asp:Label>
                </td>
                <td align="center" style="border-left: solid 1px gray; border-right: solid 1px gray;
                    background-color: #ccecff; width: 100px; font-weight: bold; height: 22px;">
                    実績 売上金額</td>
                <td style="background-color: #e6e6e6; width: 180px; height: 22px; text-align: right;">
                    <asp:Label ID="lblJissekiKingaku" runat="server" Width="120px" Style="margin-right: 5px;"></asp:Label>
                    <asp:Label ID="lblJissekiKingakuEn" runat="server" Style="margin-right: 5px;" Text="円"></asp:Label>
                </td>
                <td align="center" style="border-left: solid 1px gray; border-right: solid 1px gray;
                    background-color: #ccecff; width: 100px; font-weight: bold; height: 22px;">
                    実績 粗利金額</td>
                <td style="background-color: #e6e6e6; height: 22px; text-align: right;">
                    <asp:Label ID="lblJissekiSori" runat="server" Width="120px" Style="margin-right: 5px;"></asp:Label>
                    <asp:Label ID="lblJissekiSoriEn" runat="server" Style="margin-right: 5px;" Text="円"></asp:Label></td>
            </tr>
        </table>
    </div>
    <div id="divKikan" style="height: 125px; width: 860px; border-right: 2px solid; border-top: 2px solid;
        border-left: 2px solid; border-bottom: 2px solid; margin-left: 20px; margin-top: 10px;
        left: 16px; position: relative; top: 10px; visibility: visible; background-color: white;">
        <table cellpadding="0" cellspacing="0" style="margin-top: 10px; margin-left: 20px"
            width="600px">
            <tr>
                <td style="width: 120px">
                    <asp:Label ID="lblKikan" runat="server" Text="期間別集計" Style="font-size: 15px; font-weight: bold"></asp:Label></td>
                <td style="width: 120px">
                    <asp:DropDownList ID="ddlNendoKikan" runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
                <td style="width: 150px" align="left">
                    <asp:DropDownList ID="ddlKikan" runat="server" Width="140px">
                        <asp:ListItem Value="2">上期</asp:ListItem>
                        <asp:ListItem Value="3">下期</asp:ListItem>
                        <asp:ListItem Value="4">四半期(4,5,6月)</asp:ListItem>
                        <asp:ListItem Value="5">四半期(7,8,9月)</asp:ListItem>
                        <asp:ListItem Value="6">四半期(10,11,12月)</asp:ListItem>
                        <asp:ListItem Value="7">四半期(1,2,3月)</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width: 110px" align="center">
                    <uc1:CommonButton ID="btnKikanHyouji" runat="server" Text="表示" />
                </td>
                <td>
                    <uc1:CommonButton ID="btnKikanSyousai" runat="server" Text="詳細" Visible="false" />
                    &nbsp;
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" style="border: solid 1px gray;
            margin-left: 20px; margin-top: 10px;" width="830px">
            <tr>
                <td align="center" style="border-right: solid 1px gray; background-color: #ccecff;
                    width: 100px; font-weight: bold; height: 22px;">
                    計画 調査件数</td>
                <td style="background-color: #e6e6e6; width: 180px; height: 22px; text-align: right;">
                    <asp:Label ID="lblKikanKensuu" runat="server" Width="120px" Style="margin-right: 5px;"></asp:Label>
                    <asp:Label ID="lblKikanEjyou" runat="server" Text="件以上" Style="margin-right: 5px;"></asp:Label>
                </td>
                <td align="center" style="border-left: solid 1px gray; border-right: solid 1px gray;
                    background-color: #ccecff; width: 100px; font-weight: bold; height: 22px;">
                    計画 売上金額</td>
                <td style="background-color: #e6e6e6; width: 180px; height: 22px; text-align: right;">
                    <asp:Label ID="lblKikanUriKingaku" runat="server" Width="120px" Style="margin-right: 5px;"></asp:Label>
                    <asp:Label ID="lblEn" runat="server" Text="円" Style="margin-right: 5px;"></asp:Label>
                </td>
                <td align="center" style="border-left: solid 1px gray; border-right: solid 1px gray;
                    background-color: #ccecff; width: 100px; font-weight: bold; height: 22px;">
                    計画 粗利金額</td>
                <td style="background-color: #e6e6e6; height: 22px; text-align: right;">
                    <asp:Label ID="lblKikanArari" runat="server" Width="120px" Style="margin-right: 5px;"></asp:Label>
                    <asp:Label ID="lblKinEn" runat="server" Text="円" Style="margin-right: 5px;"></asp:Label>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" style="border: solid 1px gray;
            margin-left: 20px; margin-top: 10px;" width="830px">
            <tr>
                <td align="center" style="border-right: solid 1px gray; background-color: #ccecff;
                    width: 100px; font-weight: bold; height: 22px;">
                    実績 調査件数</td>
                <td style="background-color: #e6e6e6; width: 180px; height: 22px; text-align: right;">
                    <asp:Label ID="lblKikanJissekiKensuu" runat="server" Width="120px" Style="margin-right: 5px;"></asp:Label>
                    <asp:Label ID="Label1" runat="server" Text="件" Style="margin-right: 30px;"></asp:Label>
                </td>
                <td align="center" style="border-left: solid 1px gray; border-right: solid 1px gray;
                    background-color: #ccecff; width: 100px; font-weight: bold; height: 22px;">
                    実績 売上金額</td>
                <td style="background-color: #e6e6e6; width: 180px; height: 22px; text-align: right;">
                    <asp:Label ID="lblKikanJissekiKingaku" runat="server" Width="120px" Style="margin-right: 5px;"></asp:Label>
                    <asp:Label ID="lblJiEn" runat="server" Text="円" Style="margin-right: 5px;"></asp:Label>
                </td>
                <td align="center" style="border-left: solid 1px gray; border-right: solid 1px gray;
                    background-color: #ccecff; width: 100px; font-weight: bold; height: 22px;">
                    実績 粗利金額</td>
                <td style="background-color: #e6e6e6; height: 22px; text-align: right;">
                    <asp:Label ID="lblKikanJissekiSori" runat="server" Width="120px" Style="margin-right: 5px;"></asp:Label>
                    <asp:Label ID="lblKjEn" runat="server" Text="円" Style="margin-right: 5px;"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div id="divTuki" style="height: 125px; width: 860px; border-right: 2px solid; border-top: 2px solid;
        border-left: 2px solid; border-bottom: 2px solid; margin-left: 20px; margin-top: 10px;
        left: 16px; position: relative; top: 10px; visibility: visible; background-color: white;">
        <table cellpadding="0" cellspacing="0" style="margin-top: 10px; margin-left: 20px"
            width="650px">
            <tr>
                <td style="width: 120px">
                    <asp:Label ID="lblTuki" runat="server" Text="月別集計" Style="font-size: 15px; font-weight: bold;
                        width: 120;"></asp:Label></td>
                <td style="width: 120px;">
                    <asp:DropDownList ID="ddlNendoTuki" runat="server" Width="100px">
                    </asp:DropDownList></td>
                <td align="left" style="width: 70px">
                    <asp:DropDownList ID="ddlBeginTuki" runat="server" AutoPostBack="true" Width="70px">
                        <asp:ListItem Value="4">4月</asp:ListItem>
                        <asp:ListItem Value="5">5月</asp:ListItem>
                        <asp:ListItem Value="6">6月</asp:ListItem>
                        <asp:ListItem Value="7">7月</asp:ListItem>
                        <asp:ListItem Value="8">8月</asp:ListItem>
                        <asp:ListItem Value="9">9月</asp:ListItem>
                        <asp:ListItem Value="10">10月</asp:ListItem>
                        <asp:ListItem Value="11">11月</asp:ListItem>
                        <asp:ListItem Value="12">12月</asp:ListItem>
                        <asp:ListItem Value="13">1月</asp:ListItem>
                        <asp:ListItem Value="14">2月</asp:ListItem>
                        <asp:ListItem Value="15">3月</asp:ListItem>
                    </asp:DropDownList></td>
                <td align="center" style="width: 30px;">
                    <asp:Label ID="lblLine" runat="server" Text="～"></asp:Label></td>
                <td align="left" style="width: 90px">
                    <asp:DropDownList ID="ddlEndTuki" runat="server" AutoPostBack="true" Width="70px">
                        <asp:ListItem Value="4">4月</asp:ListItem>
                        <asp:ListItem Value="5">5月</asp:ListItem>
                        <asp:ListItem Value="6">6月</asp:ListItem>
                        <asp:ListItem Value="7">7月</asp:ListItem>
                        <asp:ListItem Value="8">8月</asp:ListItem>
                        <asp:ListItem Value="9">9月</asp:ListItem>
                        <asp:ListItem Value="10">10月</asp:ListItem>
                        <asp:ListItem Value="11">11月</asp:ListItem>
                        <asp:ListItem Value="12">12月</asp:ListItem>
                        <asp:ListItem Value="13">1月</asp:ListItem>
                        <asp:ListItem Value="14">2月</asp:ListItem>
                        <asp:ListItem Value="15">3月</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width: 90px;" align="left">
                    <uc1:CommonButton ID="btnTukiHyouji" runat="server" Text="表示" />
                </td>
                <td>
                    <uc1:CommonButton ID="btnTukiSyousai" runat="server" Text="詳細" Visible="false" />
                    &nbsp;
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" style="border: solid 1px gray;
            margin-left: 20px; margin-top: 10px;" width="830px">
            <tr>
                <td align="center" style="border-right: solid 1px gray; background-color: #ccecff;
                    width: 100px; font-weight: bold; height: 22px;">
                    計画 調査件数</td>
                <td style="background-color: #e6e6e6; width: 180px; height: 22px; text-align: right;">
                    <asp:Label ID="lblTukiKensuu" runat="server" Width="120px" Style="margin-right: 5px;"></asp:Label>
                    <asp:Label ID="lblTukiKenJou" runat="server" Text="件以上" Style="margin-right: 5px;"></asp:Label>
                </td>
                <td align="center" style="border-left: solid 1px gray; border-right: solid 1px gray;
                    background-color: #ccecff; width: 100px; font-weight: bold; height: 22px;">
                    計画 売上金額</td>
                <td style="background-color: #e6e6e6; width: 180px; height: 22px; text-align: right;">
                    <asp:Label ID="lblTukiUriKingaku" runat="server" Width="120px" Style="margin-right: 5px;"></asp:Label>
                    <asp:Label ID="lblKikakuEn" runat="server" Text="円" Style="margin-right: 5px;"></asp:Label>
                </td>
                <td align="center" style="border-left: solid 1px gray; border-right: solid 1px gray;
                    background-color: #ccecff; width: 100px; font-weight: bold; height: 22px;">
                    計画 粗利金額</td>
                <td style="background-color: #e6e6e6; height: 22px; text-align: right;">
                    <asp:Label ID="lblTukiArari" runat="server" Width="120px" Style="margin-right: 5px;"></asp:Label>
                    <asp:Label ID="lblArariEn" runat="server" Text="円" Style="margin-right: 5px;"></asp:Label>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" style="border: solid 1px gray;
            margin-left: 20px; margin-top: 10px;" width="830px">
            <tr>
                <td align="center" style="border-right: solid 1px gray; background-color: #ccecff;
                    width: 100px; font-weight: bold; height: 22px;">
                    実績 調査件数</td>
                <td style="background-color: #e6e6e6; width: 180px; height: 22px; text-align: right;">
                    <asp:Label ID="lblTukiJissekiKensuu" runat="server" Width="120px" Style="margin-right: 5px;"></asp:Label>
                    <asp:Label ID="Label2" runat="server" Text="件" Style="margin-right: 30px;"></asp:Label>
                </td>
                <td align="center" style="border-left: solid 1px gray; border-right: solid 1px gray;
                    background-color: #ccecff; width: 100px; font-weight: bold; height: 22px;">
                    実績 売上金額</td>
                <td style="background-color: #e6e6e6; width: 180px; height: 22px; text-align: right;">
                    <asp:Label ID="lblTukiJissekiKingaku" runat="server" Width="120px" Style="margin-right: 5px;"></asp:Label>
                    <asp:Label ID="lblJkingakuEn" runat="server" Text="円" Style="margin-right: 5px;"></asp:Label>
                </td>
                <td align="center" style="border-left: solid 1px gray; border-right: solid 1px gray;
                    background-color: #ccecff; width: 100px; font-weight: bold; height: 22px;">
                    実績 粗利金額</td>
                <td style="background-color: #e6e6e6; height: 22px; text-align: right;">
                    <asp:Label ID="lblTukiJissekiSori" runat="server" Width="120px" Style="margin-right: 5px;"></asp:Label>
                    <asp:Label ID="lblJissekiEn" runat="server" Text="円" Style="margin-right: 5px;"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

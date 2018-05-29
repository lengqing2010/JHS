<%@ Page Language="VB" MasterPageFile="~/EKKSMaster.master" AutoEventWireup="false"
    CodeFile="KeikakuKanriKameitenKensakuSyoukaiInquiry.aspx.vb" Inherits="KeikakuKanriKameitenKensakuSyoukaiInquiry"
    Title="計画管理_加盟店検索照会指示" %>

<%@ Register Src="CommonControl/CommonButton.ascx" TagName="CommonButton" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/EKKSMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        //onload
        window.onload = function()
        {
            window.name = "<%=CommonConstBC.keikakuKanriKameitenKensakuSyoukai%>"
            setMenuBgColor();
            fncSetKubunVal();
            if ($ID('<%=gridviewRightId%>')!=null)
            {
                objEBI("<%=tblRightId%>").style.height=objEBI("<%=gridviewRightId%>").offsetHeight;
                fncSetScroll();
                //fncSetSelectedRow();                
            }
            fncShowErrorMessage();
        }
    </script>

    <table border="0" cellpadding="0" cellspacing="0" class="titleTable">
        <tr>
            <th>
                計画管理_加盟店検索照会指示
            </th>
        </tr>
    </table>
    <table id="tblKensaku" style="width: 960px; text-align: left;" class="mainTable paddinNarrow"
        cellpadding="1">
        <tr>
            <th class="tableTitle" colspan="8" style="height: 20px;">
                検索条件
            </th>
        </tr>
        <tr>
            <td class="hissu" style="font-weight: bold;">
                対象年度
            </td>
            <td colspan="7" class="hissu">
                <asp:DropDownList ID="ddlYear" runat="server" Style="width: 115px;">
                </asp:DropDownList>
                <asp:HiddenField runat="server" ID="hidYear" />
            </td>
        </tr>
        <tr>
            <td class="koumokuMei" style="font-weight: bold;">
                区分
            </td>
            <td colspan="7">
                <asp:DropDownList ID="ddlKbn1" runat="server" Style="width: 185px;">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlKbn2" runat="server" Style="width: 185px;">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlKbn3" runat="server" Style="width: 185px;">
                </asp:DropDownList>
                <asp:CheckBox runat="server" ID="chkKubunAll" Text="全区分" TextAlign="left" Checked="true"
                    Style="margin-left: 5px;" />
                <asp:CheckBox runat="server" ID="chkTaikai" Text="退会した加盟店を表示する" />
            </td>
        </tr>
        <tr>
            <td class="koumokuMei" style="font-weight: bold;">
                加盟店名
            </td>
            <td colspan="3">
                <asp:TextBox runat="server" ID="tbxKameitenMei" MaxLength="40" Style="width: 300px;" />
            </td>
            <td class="koumokuMei" style="font-weight: bold;">
                加盟店コード
            </td>
            <td colspan="3">
                <asp:TextBox runat="server" ID="tbxKameitenCd1" MaxLength="16" Style="width: 75px;"
                    CssClass="codeNumber"></asp:TextBox>
                <asp:Button runat="server" ID="btnKameitenSearch1" Text="検索" />
                ～
                <asp:TextBox runat="server" ID="tbxKameitenCd2" MaxLength="16" Style="width: 75px;"
                    CssClass="codeNumber"></asp:TextBox>
                <asp:Button runat="server" ID="btnKameitenSearch2" Text="検索" />
            </td>
        </tr>
        <tr>
            <td class="koumokuMei" style="font-weight: bold;">
                管轄支店
            </td>
            <td>
                <asp:DropDownList ID="ddlKankatuSiten" runat="server" Style="width: 115px;">
                </asp:DropDownList>
            </td>
            <td class="koumokuMei" style="font-weight: bold;">
                都道府県
            </td>
            <td>
                <asp:DropDownList ID="ddlTodoufuken" runat="server" Style="width: 115px;">
                </asp:DropDownList>
            </td>
            <td class="koumokuMei" style="font-weight: bold;">
                営業所コード
            </td>
            <td colspan="3">
                <asp:TextBox runat="server" ID="tbxEigyousyoCd1" MaxLength="5" Style="width: 75px;"
                    CssClass="codeNumber"></asp:TextBox>
                <asp:Button runat="server" ID="btnEigyousyoCdSearch1" Text="検索" />
                ～
                <asp:TextBox runat="server" ID="tbxEigyousyoCd2" MaxLength="5" Style="width: 75px;"
                    CssClass="codeNumber"></asp:TextBox>
                <asp:Button runat="server" ID="btnEigyousyoCdSearch2" Text="検索" />
            </td>
        </tr>
        <tr>
            <td class="koumokuMei" style="width: 95px; font-weight: bold;">
                営業担当者
            </td>
            <td colspan="3">
                <asp:TextBox runat="server" ID="tbxEigyouTantousyaCd" MaxLength="30" CssClass="codeNumber"
                    Style="width: 85px;" />
                <asp:Button runat="server" ID="btnEigyouTantousyaSearch" Text="検索" />
                <asp:TextBox ID="tbxEigyouTantousyaMei" runat="server" CssClass="readOnlyStyle" TabIndex="-1"
                    Width="200px" Style="border-bottom: none;"></asp:TextBox>
            </td>
            <td class="koumokuMei" style="width: 95px; font-weight: bold;">
                系列コード
            </td>
            <td colspan="3">
                <asp:TextBox runat="server" ID="tbxKeiretuCd" MaxLength="5" Style="width: 75px;"
                    CssClass="codeNumber"></asp:TextBox>
                <asp:Button runat="server" ID="btnKeiretuSearch" Text="検索" />
                <asp:TextBox ID="tbxKeiretuMei" runat="server" CssClass="readOnlyStyle" TabIndex="-1"
                    Width="200px" Style="border-bottom: none;"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="koumokuMei" style="font-weight: bold;">
                営業区分
            </td>
            <td style="">
                <asp:DropDownList ID="ddlEigyouKbn" runat="server" Style="width: 115px;">
                </asp:DropDownList>
            </td>
            <td class="koumokuMei" style="font-weight: bold;">
                営業担当所属
            </td>
            <td style="">
                <asp:DropDownList ID="ddlEigyouTantouSyozaku" runat="server" Style="width: 115px;">
                </asp:DropDownList>
            </td>
            <td class="koumokuMei" style="font-weight: bold;">
                業態
            </td>
            <td style="">
                <asp:DropDownList ID="ddlGyoutai" runat="server" Style="width: 115px;">
                </asp:DropDownList>
            </td>
            <td class="koumokuMei" style="font-weight: bold;">
                年間棟数
            </td>
            <td>
                <asp:TextBox ID="tbxNenkanTousuu1" runat="server" MaxLength="5" CssClass="codeNumber"
                    Style="width: 55px;"></asp:TextBox>
                ～
                <asp:TextBox ID="tbxNenkanTousuu2" runat="server" MaxLength="5" CssClass="codeNumber"
                    Style="width: 55px;"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="koumokuMei" style="width: 95px; font-weight: bold;">
                加盟店属性1
            </td>
            <td style="width: 130px;">
                <asp:DropDownList ID="ddlKameitenZokusei1" runat="server" Style="width: 115px;">
                </asp:DropDownList>
            </td>
            <td class="koumokuMei" style="width: 95px; font-weight: bold;">
                加盟店属性2
            </td>
            <td style="width: 130px;">
                <asp:DropDownList ID="ddlKameitenZokusei2" runat="server" Style="width: 115px;">
                </asp:DropDownList>
            </td>
            <td class="koumokuMei" style="width: 95px; font-weight: bold;">
                加盟店属性3
            </td>
            <td style="width: 130px;">
                <asp:DropDownList ID="ddlKameitenZokusei3" runat="server" Style="width: 115px;">
                </asp:DropDownList>
            </td>
            <td class="koumokuMei" style="width: 110px; font-weight: bold;">
                計画用_年間棟数
            </td>
            <td>
                <asp:TextBox ID="tbxKeikakuyouNenkanTousuu1" runat="server" MaxLength="5" CssClass="codeNumber"
                    Style="width: 55px;"></asp:TextBox>
                ～
                <asp:TextBox ID="tbxKeikakuyouNenkanTousuu2" runat="server" MaxLength="5" CssClass="codeNumber"
                    Style="width: 55px;"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="koumokuMei" style="width: 95px; font-weight: bold;">
                加盟店属性4
            </td>
            <td style="width: 130px;">
                <asp:DropDownList ID="ddlKameitenZokusei4" runat="server" Style="width: 115px;">
                </asp:DropDownList>
            </td>
            <td class="koumokuMei" style="width: 95px; font-weight: bold;">
                加盟店属性5
            </td>
            <td style="width: 130px;">
                <asp:DropDownList ID="ddlKameitenZokusei5" runat="server" Style="width: 115px;">
                </asp:DropDownList>
            </td>
            <td class="koumokuMei" style="width: 95px; font-weight: bold;">
                加盟店属性6
            </td>
            <td style="width: 130px;">
                <asp:TextBox ID="tbxKameitenZokusei6" runat="server" MaxLength="40" Style="width: 120px;"></asp:TextBox>
            </td>
            <td class="koumokuMei" style="width: 110px; font-weight: bold;">
                計画値有無
            </td>
            <td>
                <asp:DropDownList ID="ddlKeisakutiUmu" runat="server" Style="width: 115px;">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="8" style="height: 28px; text-align: center;">
                <asp:Label ID="lblJyouken" runat="server" Text="検索上限件数" Style=""></asp:Label>
                <asp:DropDownList ID="ddlKensakuJyouken" runat="server" Style="width: 70px;">
                    <asp:ListItem Value="100" Text="100件" Selected="true"></asp:ListItem>
                    <asp:ListItem Value="200" Text="200件"></asp:ListItem>
                    <asp:ListItem Value="max" Text="無制限"></asp:ListItem>
                </asp:DropDownList>
                <uc1:CommonButton ID="btnKensakujiltukou" runat="server" Text="検索実行" OnClientClick="if(!fncKensakuCheck()){return false;};" />
                <asp:Button ID="btnKensakuClear" runat="server" Text="検索クリア" Style="padding-top: 2px;" />
            </td>
        </tr>
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
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <div id="divHeadLeft" runat="server" style="width: 320px; overflow: hidden;">
                    <table border="0" cellpadding="0" cellspacing="0" class="TableBorder" width="320px">
                        <tr style="background-color: #ffffd9;">
                            <td class="TdBorder" style="width: 20px; height: 28px;">
                                &nbsp;</td>
                            <td class="TdBorder" style="width: 75px; text-align: center;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2">
                                            取消&nbsp;</td>
                                        <td style="height: 15px; padding: 0px; vertical-align: bottom;">
                                            <asp:LinkButton runat="server" ID="btnTorikesiUp" Text="▲" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 14px; padding: 0px; vertical-align: top;">
                                            <asp:LinkButton runat="server" ID="btnTorikesiDown" Text="▼" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="TdBorder" style="width: 75px; text-align: center;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2">
                                            区分&nbsp;</td>
                                        <td style="padding: 0px; vertical-align: bottom;">
                                            <asp:LinkButton runat="server" ID="btnKbnUp" Text="▲" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 0px; vertical-align: top;">
                                            <asp:LinkButton runat="server" ID="btnKbnDown" Text="▼" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="TdBorder" style="width: 120px; text-align: center;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2" style="border-color: #FFFFD9;">
                                            加盟店コード&nbsp;</td>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: bottom;">
                                            <asp:LinkButton runat="server" ID="btnKameitenCdUp" Text="▲" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-color: #FFFFD9; padding: 0px; vertical-align: top;">
                                            <asp:LinkButton runat="server" ID="btnKameitenCdDown" Text="▼" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td>
                <div id="divHeadRight" runat="server" style="width: 626px; overflow: hidden; margin-left: -1px;">
                    <table class="TableBorder" cellpadding="0" cellspacing="0" width="2110px">
                        <tr style="background-color: #ffffd9;">
                            <td class="TdBorder" style="width: 272px; text-align: center;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2">
                                            加盟店名&nbsp;</td>
                                        <td style="height: 15px; padding: 0px; vertical-align: bottom;">
                                            <asp:LinkButton runat="server" ID="btnKameitenMeiUp" Text="▲" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 14px; padding: 0px; vertical-align: top;">
                                            <asp:LinkButton runat="server" ID="btnKameitenMeiDown" Text="▼" TabIndex="-1" Height="14px"
                                                Font-Underline="false" Visible="false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="TdBorder" style="width: 103px; text-align: center;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2">
                                            営業区分&nbsp;</td>
                                        <td style="height: 15px; padding: 0px; vertical-align: bottom;">
                                            <asp:LinkButton runat="server" ID="btnEigyouKbnUp" Text="▲" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 14px; padding: 0px; vertical-align: top;">
                                            <asp:LinkButton runat="server" ID="btnEigyouKbnDown" Text="▼" TabIndex="-1" Height="14px"
                                                Font-Underline="false" Visible="false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="TdBorder" style="width: 133px; text-align: center;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2">
                                            営業担当者名&nbsp;</td>
                                        <td style="height: 15px; padding: 0px; vertical-align: bottom;">
                                            <asp:LinkButton runat="server" ID="btnEigyouTantousyaUp" Text="▲" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 14px; padding: 0px; vertical-align: top;">
                                            <asp:LinkButton runat="server" ID="btnEigyouTantousyaDown" Text="▼" TabIndex="-1"
                                                Height="14px" Font-Underline="false" Visible="false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="TdBorder" style="width: 113px; text-align: center;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2">
                                            管轄支店&nbsp;</td>
                                        <td style="height: 15px; padding: 0px; vertical-align: bottom;">
                                            <asp:LinkButton runat="server" ID="btnKankatuSitenUp" Text="▲" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 14px; padding: 0px; vertical-align: top;">
                                            <asp:LinkButton runat="server" ID="btnKankatuSitenDown" Text="▼" TabIndex="-1" Height="14px"
                                                Font-Underline="false" Visible="false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="TdBorder" style="width: 123px; text-align: center;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2">
                                            都道府県&nbsp;</td>
                                        <td style="height: 15px; padding: 0px; vertical-align: bottom;">
                                            <asp:LinkButton runat="server" ID="btnTodoufukenUp" Text="▲" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 14px; padding: 0px; vertical-align: top;">
                                            <asp:LinkButton runat="server" ID="btnTodoufukenDown" Text="▼" TabIndex="-1" Height="14px"
                                                Font-Underline="false" Visible="false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="TdBorder" style="width: 103px; text-align: center;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2">
                                            営業所コード&nbsp;</td>
                                        <td style="height: 15px; padding: 0px; vertical-align: bottom;">
                                            <asp:LinkButton runat="server" ID="btnEigyousyoCdUp" Text="▲" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 14px; padding: 0px; vertical-align: top;">
                                            <asp:LinkButton runat="server" ID="btnEigyousyoCdDown" Text="▼" TabIndex="-1" Height="14px"
                                                Font-Underline="false" Visible="false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="TdBorder" style="width: 103px; text-align: center;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2">
                                            系列コード&nbsp;</td>
                                        <td style="height: 15px; padding: 0px; vertical-align: bottom;">
                                            <asp:LinkButton runat="server" ID="btnKeiretuCdUp" Text="▲" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 14px; padding: 0px; vertical-align: top;">
                                            <asp:LinkButton runat="server" ID="btnKeiretuCdDown" Text="▼" TabIndex="-1" Height="14px"
                                                Font-Underline="false" Visible="false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="TdBorder" style="width: 123px; text-align: center;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2">
                                            営業担当所属&nbsp;</td>
                                        <td style="height: 15px; padding: 0px; vertical-align: bottom;">
                                            <asp:LinkButton runat="server" ID="btnEigyouTantouSyozakuUp" Text="▲" Height="14px"
                                                TabIndex="-1" Font-Underline="false" Visible="false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 14px; padding: 0px; vertical-align: top;">
                                            <asp:LinkButton runat="server" ID="btnEigyouTantouSyozakuDown" Text="▼" TabIndex="-1"
                                                Height="14px" Font-Underline="false" Visible="false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="TdBorder" style="width: 103px; text-align: center;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2">
                                            業態&nbsp;</td>
                                        <td style="height: 15px; padding: 0px; vertical-align: bottom;">
                                            <asp:LinkButton runat="server" ID="btnGyoutaiUp" Text="▲" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 14px; padding: 0px; vertical-align: top;">
                                            <asp:LinkButton runat="server" ID="btnGyoutaiDown" Text="▼" TabIndex="-1" Height="14px"
                                                Font-Underline="false" Visible="false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="TdBorder" style="width: 103px; text-align: center;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2">
                                            年間棟数&nbsp;</td>
                                        <td style="height: 15px; padding: 0px; vertical-align: bottom;">
                                            <asp:LinkButton runat="server" ID="btnNenkanTousuuUp" Text="▲" Height="14px" TabIndex="-1"
                                                Font-Underline="false" Visible="false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 14px; padding: 0px; vertical-align: top;">
                                            <asp:LinkButton runat="server" ID="btnNenkanTousuuDown" Text="▼" TabIndex="-1" Height="14px"
                                                Font-Underline="false" Visible="false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="TdBorder" style="width: 133px; text-align: center;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2">
                                            計画用_年間棟数&nbsp;</td>
                                        <td style="height: 15px; padding: 0px; vertical-align: bottom;">
                                            <asp:LinkButton runat="server" ID="btnKeikakuyouNenkanTousuuUp" Text="▲" Height="14px"
                                                TabIndex="-1" Font-Underline="false" Visible="false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 14px; padding: 0px; vertical-align: top;">
                                            <asp:LinkButton runat="server" ID="btnKeikakuyouNenkanTousuuDown" Text="▼" TabIndex="-1"
                                                Height="14px" Font-Underline="false" Visible="false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="TdBorder" style="width: 113px; text-align: center;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2">
                                            加盟店属性1&nbsp;</td>
                                        <td style="height: 15px; padding: 0px; vertical-align: bottom;">
                                            <asp:LinkButton runat="server" ID="btnKameitenZokusei1Up" Text="▲" Height="14px"
                                                TabIndex="-1" Font-Underline="false" Visible="false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 14px; padding: 0px; vertical-align: top;">
                                            <asp:LinkButton runat="server" ID="btnKameitenZokusei1Down" Text="▼" TabIndex="-1"
                                                Height="14px" Font-Underline="false" Visible="false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="TdBorder" style="width: 113px; text-align: center;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2">
                                            加盟店属性2&nbsp;</td>
                                        <td style="height: 15px; padding: 0px; vertical-align: bottom;">
                                            <asp:LinkButton runat="server" ID="btnKameitenZokusei2Up" Text="▲" Height="14px"
                                                TabIndex="-1" Font-Underline="false" Visible="false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 14px; padding: 0px; vertical-align: top;">
                                            <asp:LinkButton runat="server" ID="btnKameitenZokusei2Down" Text="▼" TabIndex="-1"
                                                Height="14px" Font-Underline="false" Visible="false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="TdBorder" style="width: 113px; text-align: center;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2">
                                            加盟店属性3&nbsp;</td>
                                        <td style="height: 15px; padding: 0px; vertical-align: bottom;">
                                            <asp:LinkButton runat="server" ID="btnKameitenZokusei3Up" Text="▲" Height="14px"
                                                TabIndex="-1" Font-Underline="false" Visible="false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 14px; padding: 0px; vertical-align: top;">
                                            <asp:LinkButton runat="server" ID="btnKameitenZokusei3Down" Text="▼" TabIndex="-1"
                                                Height="14px" Font-Underline="false" Visible="false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="TdBorder" style="width: 113px; text-align: center;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2">
                                            加盟店属性4&nbsp;</td>
                                        <td style="height: 15px; padding: 0px; vertical-align: bottom;">
                                            <asp:LinkButton runat="server" ID="btnKameitenZokusei4Up" Text="▲" Height="14px"
                                                TabIndex="-1" Font-Underline="false" Visible="false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 14px; padding: 0px; vertical-align: top;">
                                            <asp:LinkButton runat="server" ID="btnKameitenZokusei4Down" Text="▼" TabIndex="-1"
                                                Height="14px" Font-Underline="false" Visible="false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="TdBorder" style="width: 113px; text-align: center;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2">
                                            加盟店属性5&nbsp;</td>
                                        <td style="height: 15px; padding: 0px; vertical-align: bottom;">
                                            <asp:LinkButton runat="server" ID="btnKameitenZokusei5Up" Text="▲" Height="14px"
                                                TabIndex="-1" Font-Underline="false" Visible="false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 14px; padding: 0px; vertical-align: top;">
                                            <asp:LinkButton runat="server" ID="btnKameitenZokusei5Down" Text="▼" TabIndex="-1"
                                                Height="14px" Font-Underline="false" Visible="false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="TdBorder" style="text-align: center;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="2">
                                            加盟店属性6&nbsp;</td>
                                        <td style="height: 15px; padding: 0px; vertical-align: bottom;">
                                            <asp:LinkButton runat="server" ID="btnKameitenZokusei6Up" Text="▲" Height="14px"
                                                TabIndex="-1" Font-Underline="false" Visible="false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 14px; padding: 0px; vertical-align: top;">
                                            <asp:LinkButton runat="server" ID="btnKameitenZokusei6Down" Text="▼" TabIndex="-1"
                                                Height="14px" Font-Underline="false" Visible="false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td style="width: 17px;">
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <div id="divBodyLeft" runat="server" style="width: 320px; height: 133px; overflow: hidden;
                    margin-top: -1px;" onmousewheel="wheel();">
                    <asp:GridView ID="grdItiranLeft" runat="server" AutoGenerateColumns="False" BackColor="White"
                        CssClass="gvTableBorder" Style="width: 320px; border-color: Black; padding-left: 3px;"
                        ShowHeader="False" CellPadding="0">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <input type="radio" name="rdo_SetHoshouNo" style="width: 20px;" onclick="fncSetLineColor(event.srcElement.parentNode.parentNode,event.srcElement.parentNode.parentNode.rowIndex);funDisableButton();" />
                                </ItemTemplate>
                                <ItemStyle Width="20px" CssClass="gvMeisaiBorder" BorderColor="black" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblTorikesi" runat="server" Width="77px" Text='<%#Eval("TorikesiText")%>'
                                        ToolTip='<%#Eval("TorikesiText")%>' CssClass="TextOverflow"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Height="21px" Width="78px" HorizontalAlign="Left" CssClass="gvMeisaiBorder"
                                    BorderColor="black" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Kbn">
                                <ItemStyle Width="78px" HorizontalAlign="Left" CssClass="gvMeisaiBorder" BorderColor="black" />
                            </asp:BoundField>
                            <asp:BoundField DataField="KameitenCd">
                                <ItemStyle HorizontalAlign="Left" CssClass="gvMeisaiBorder" BorderColor="black" />
                            </asp:BoundField>
                        </Columns>
                        <SelectedRowStyle ForeColor="White" />
                        <AlternatingRowStyle BackColor="LightCyan" />
                    </asp:GridView>
                </div>
            </td>
            <td style="vertical-align: top;">
                <div id="divBodyRight" runat="server" style="width: 626px; height: 133px; overflow: hidden;
                    - margin-top: -1px; margin-left: -1px;" onmousewheel="wheel();">
                    <asp:GridView ID="grdItiranRight" runat="server" AutoGenerateColumns="False" BackColor="White"
                        CssClass="gvTableBorder" Style="width: 2110px; border-color: Black; padding-left: 3px;"
                        ShowHeader="False" CellPadding="0">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblTorikesi" runat="server" Width="269px" Text='<%#Eval("KameitenMei")%>'
                                        ToolTip='<%#Eval("KameitenMei")%>' CssClass="TextOverflow"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Height="21px" Width="269px" HorizontalAlign="Left" CssClass="gvMeisaiBorder"
                                    BorderColor="black" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblTorikesi" runat="server" Width="100px" Text='<%#Eval("EigyouKbnText")%>'
                                        ToolTip='<%#Eval("EigyouKbnText")%>' CssClass="TextOverflow"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="100px" HorizontalAlign="Left" CssClass="gvMeisaiBorder" BorderColor="black" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblTorikesi" runat="server" Width="130px" Text='<%#Eval("EigyouTantousya")%>'
                                        ToolTip='<%#Eval("EigyouTantousya")%>' CssClass="TextOverflow"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="130px" HorizontalAlign="Left" CssClass="gvMeisaiBorder" BorderColor="black" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblTorikesi" runat="server" Width="110px" Text='<%#Eval("KankatuSiten")%>'
                                        ToolTip='<%#Eval("KankatuSiten")%>' CssClass="TextOverflow"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="110px" HorizontalAlign="Left" CssClass="gvMeisaiBorder" BorderColor="black" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblTorikesi" runat="server" Width="120px" Text='<%#Eval("Todoufuken")%>'
                                        ToolTip='<%#Eval("Todoufuken")%>' CssClass="TextOverflow"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="120px" HorizontalAlign="Left" CssClass="gvMeisaiBorder" BorderColor="black" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="EigyousyoCd">
                                <ItemStyle Width="100px" HorizontalAlign="Left" CssClass="gvMeisaiBorder" BorderColor="black" />
                            </asp:BoundField>
                            <asp:BoundField DataField="KeiretuCd">
                                <ItemStyle Width="100px" HorizontalAlign="Left" CssClass="gvMeisaiBorder" BorderColor="black" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblTorikesi" runat="server" Width="120px" Text='<%#Eval("EigyouTantouSyozaku")%>'
                                        ToolTip='<%#Eval("EigyouTantouSyozaku")%>' CssClass="TextOverflow"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="120px" HorizontalAlign="Left" CssClass="gvMeisaiBorder" BorderColor="black" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblTorikesi" runat="server" Width="100px" Text='<%#Eval("Gyoutai")%>'
                                        ToolTip='<%#Eval("Gyoutai")%>' CssClass="TextOverflow"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="100px" HorizontalAlign="Left" CssClass="gvMeisaiBorder" BorderColor="black" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblTorikesi" runat="server" Width="100px" Text='<%#Eval("NenkanTousuu")%>'
                                        ToolTip='<%#Eval("NenkanTousuu")%>' CssClass="TextOverflow"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="100px" HorizontalAlign="Left" CssClass="gvMeisaiBorder" BorderColor="black" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblTorikesi" runat="server" Width="130px" Text='<%#Eval("KeikakuyouNenkanTousuu")%>'
                                        ToolTip='<%#Eval("KeikakuyouNenkanTousuu")%>' CssClass="TextOverflow"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="130px" HorizontalAlign="Left" CssClass="gvMeisaiBorder" BorderColor="black" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblTorikesi" runat="server" Width="110px" Text='<%#Eval("KameitenZokusei1")%>'
                                        ToolTip='<%#Eval("KameitenZokusei1")%>' CssClass="TextOverflow"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="110px" HorizontalAlign="Left" CssClass="gvMeisaiBorder" BorderColor="black" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblTorikesi" runat="server" Width="110px" Text='<%#Eval("KameitenZokusei2")%>'
                                        ToolTip='<%#Eval("KameitenZokusei2")%>' CssClass="TextOverflow"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="110px" HorizontalAlign="Left" CssClass="gvMeisaiBorder" BorderColor="black" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblTorikesi" runat="server" Width="110px" Text='<%#Eval("KameitenZokusei3")%>'
                                        ToolTip='<%#Eval("KameitenZokusei3")%>' CssClass="TextOverflow"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="110px" HorizontalAlign="Left" CssClass="gvMeisaiBorder" BorderColor="black" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblTorikesi" runat="server" Width="110px" Text='<%#Eval("KameitenZokusei4")%>'
                                        ToolTip='<%#Eval("KameitenZokusei4")%>' CssClass="TextOverflow"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="110px" HorizontalAlign="Left" CssClass="gvMeisaiBorder" BorderColor="black" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblTorikesi" runat="server" Width="110px" Text='<%#Eval("KameitenZokusei5")%>'
                                        ToolTip='<%#Eval("KameitenZokusei5")%>' CssClass="TextOverflow"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="110px" HorizontalAlign="Left" CssClass="gvMeisaiBorder" BorderColor="black" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblTorikesi" runat="server" Width="110px" Text='<%#Eval("KameitenZokusei6")%>'
                                        ToolTip='<%#Eval("KameitenZokusei6")%>' CssClass="TextOverflow"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" CssClass="gvMeisaiBorder" BorderColor="black" />
                            </asp:TemplateField>
                        </Columns>
                        <SelectedRowStyle ForeColor="White" />
                        <AlternatingRowStyle BackColor="LightCyan" />
                    </asp:GridView>
                </div>
            </td>
            <td>
                <div style="height: 133px; overflow: hidden;">
                    <div id="divScrollV" runat="server" style="width: 17px; height: 150px; overflow: auto;"
                        onscroll="fncScrollV();fncSaveVScroll()">
                        <table id="tableScrollV" runat="server">
                            <tr>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td style="width: 320px;">
            </td>
            <td>
                <div style="width: 626px; overflow: hidden;">
                    <div id="divScrollH" runat="server" style="width: 643px; height: 17px; overflow: auto;
                        margin-left: -1px;" onscroll="fncScrollH();fncSaveHScroll();">
                        <table style="width: 2110px;">
                            <tr>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <asp:Button ID="btnKeisakuKameitenJyouhouSyoukai" runat="server" Text="計画_加盟店情報照会" />
    <asp:HiddenField runat="server" ID="hidSelectRowIndex" />
    <asp:HiddenField runat="server" ID="hidErrorMessage" />
    <asp:HiddenField runat="server" ID="hidErrorItemId" />
    <asp:HiddenField runat="server" ID="hidHScroll" />
    <asp:HiddenField runat="server" ID="hidVScroll" />
</asp:Content>

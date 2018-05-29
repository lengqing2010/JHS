<%@ Control Language="VB" AutoEventWireup="false" CodeFile="KeikakuKanriKameitenInquiry.ascx.vb"
    Inherits="CommonControl_KeikakuKanriKameitenInquiry" %>
<table style="width: 960px;" cellspacing="0" cellpadding="1" class="mainTable paddinNarrow">
    <col width="" />
    <col width="185px;" />
    <col width="25px;" />
    <col width="100px;" />
    <col width="60px;" />
    <col width="90px;" />
    <col width="60px;" />
    <col width="70px;" />
    <col width="45px;" />
    <col width="210px;" />
    <tr>
        <td style="border-color: #ccffff;">
        </td>
        <td style="border-color: #ccffff;">
        </td>
        <td style="border-color: #ccffff;">
        </td>
        <td style="border-color: #ccffff;">
        </td>
        <td style="border-color: #ccffff;">
        </td>
        <td style="border-color: #ccffff;">
        </td>
        <td style="border-color: #ccffff;">
        </td>
        <td style="border-color: #ccffff;">
        </td>
        <td style="border-color: #ccffff;">
        </td>
        <td style="border-color: #ccffff;">
        </td>
    </tr>
    <tr>
        <th class="tableTitle" colspan="10" rowspan="1" style="text-align: left;">
            <asp:LinkButton ID="lnkTitle" runat="server">計画管理用_加盟店情報</asp:LinkButton>
            <asp:Button ID="btnTouroku" runat="server" Text="登録" Width="40px" Height="23px" />
            <span id="titleInfobar1" runat="server"></span>
        </th>
    </tr>
    <tbody id="meisaiTbody1" runat="server" class="itemTable">
        <tr>
            <td class="koumokuMei">
                計画用_加盟店名
            </td>
            <td colspan="4">
                <asp:TextBox runat="server" ID="tbxKeikakuyoKameitenMei" Width="350px" MaxLength="80"></asp:TextBox>
            </td>
            <td class="koumokuMei">
                業態
            </td>
            <td colspan="4">
                <asp:DropDownList ID="ddlGyoutai" runat="server" Width="300px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="koumokuMei">
                統一法人ｺｰﾄﾞ
            </td>
            <td colspan="4">
                <asp:TextBox runat="server" ID="tbxTouitu" Width="60px" Style="ime-mode: disabled;"
                    MaxLength="8"></asp:TextBox>
                <asp:Button ID="btnKensakuTouitu" runat="server" Text="検索" Width="40px" Height="23px" />
                <asp:TextBox ID="lblTouitu" runat="server" CssClass="readOnlyStyle" Width="250px"
                    TabIndex="-1" Style="border-bottom: none;"></asp:TextBox>
                <%--<asp:Label ID="lblTouitu" runat="server" Width="260px"></asp:Label>--%>
            </td>
            <td class="koumokuMei">
                法人ｺｰﾄﾞ
            </td>
            <td colspan="4">
                <asp:TextBox runat="server" ID="tbxHoujin" Width="60px" Style="ime-mode: disabled;"
                    MaxLength="8"></asp:TextBox>
                <asp:Button ID="btnKensakuHoujin" runat="server" Text="検索" Width="40px" Height="23px" />
                <asp:TextBox ID="lblHoujin" runat="server" CssClass="readOnlyStyle" Width="250px"
                    TabIndex="-1" Style="border-bottom: none;"></asp:TextBox>
                <%--<asp:Label ID="lblHoujin" runat="server" Width="260px"></asp:Label>--%>
            </td>
        </tr>
        <tr>
            <td class="koumokuMei">
                計画名寄コード
            </td>
            <td colspan="9">
                <asp:TextBox runat="server" ID="tbxYoriCd" Width="60px" Style="ime-mode: disabled;"
                    MaxLength="8"></asp:TextBox>
                <asp:Button ID="btnKensakuYoriCd" runat="server" Text="検索" Width="40px" Height="23px" />
                <asp:TextBox ID="lblYoriCd" runat="server" CssClass="readOnlyStyle" Width="600px"
                    TabIndex="-1" Style="border-bottom: none;"></asp:TextBox>
                <%--<asp:Label ID="lblYoriCd" runat="server" Width="600px"></asp:Label>--%>
            </td>
        </tr>
        <tr>
            <td class="koumokuMei">
                計画用_年間棟数
            </td>
            <td>
                <asp:TextBox runat="server" ID="tbxNenKan" Width="170px" Style="ime-mode: disabled;"></asp:TextBox>
            </td>
            <td colspan="2" class="koumokuMei">
                計画時_営業担当者
            </td>
            <td colspan="4">
                <asp:TextBox runat="server" ID="tbxEigyouSya" Width="90px" MaxLength="30" Style="ime-mode: disabled;"></asp:TextBox>
                <asp:Button ID="btnKensakuEigyouSya" runat="server" Text="検索" Width="40px" Height="23px" />
                <asp:TextBox ID="lblKeikakuEigyouSya" runat="server" CssClass="readOnlyStyle" TabIndex="-1"
                    Width="120px" Style="border-bottom: none;"></asp:TextBox>
                <%--                <asp:Label ID="lblKeikakuEigyouSya" runat="server" Width="160px" Style="background-color: Red;"></asp:Label>
--%>
            </td>
            <td class="koumokuMei">
                所属
            </td>
            <td>
                <%--<asp:Label ID="lblSyozoku" runat="server" Width="180px"></asp:Label>--%>
                <asp:TextBox ID="lblSyozoku" runat="server" CssClass="readOnlyStyle" TabIndex="-1"
                    Width="200px" Style="border-bottom: none;"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="koumokuMei">
                計画用_営業区分
            </td>
            <td>
                <asp:DropDownList ID="ddlKeikauEigyouKbn" runat="server" Width="175px">
                </asp:DropDownList>
            </td>
            <td colspan="2" class="koumokuMei">
                計画用_管轄支店
            </td>
            <td colspan="3">
                <asp:DropDownList ID="ddlKamkatuSiten" runat="server" Width="200px">
                </asp:DropDownList>
            </td>
            <td colspan="2" class="koumokuMei">
                SDS開始年月
            </td>
            <td>
                <asp:TextBox runat="server" ID="tbxSDS" Width="170px" MaxLength="7" Style="ime-mode: disabled;"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="koumokuMei">
                加盟店属性1
            </td>
            <td colspan="2">
                <asp:DropDownList ID="ddlSokusei1" runat="server" Width="200px">
                </asp:DropDownList>
            </td>
            <td class="koumokuMei">
                加盟店属性2
            </td>
            <td colspan="3">
                <asp:DropDownList ID="ddlSokusei2" runat="server" Width="200px">
                </asp:DropDownList>
            </td>
            <td colspan="2" class="koumokuMei">
                加盟店属性3
            </td>
            <td>
                <asp:DropDownList ID="ddlSokusei3" runat="server" Width="175px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="koumokuMei">
                加盟店属性4
            </td>
            <td colspan="2">
                <asp:DropDownList ID="ddlSokusei4" runat="server" Width="200px">
                </asp:DropDownList>
            </td>
            <td class="koumokuMei">
                加盟店属性5
            </td>
            <td colspan="3">
                <asp:DropDownList ID="ddlSokusei5" runat="server" Width="200px">
                </asp:DropDownList>
            </td>
            <td colspan="2" class="koumokuMei">
                加盟店属性6
            </td>
            <td>
                <asp:TextBox runat="server" ID="tbxSokusei6" Width="170px" MaxLength="40"></asp:TextBox>
            </td>
        </tr>
    </tbody>
</table>
<asp:HiddenField ID="hidMaxDate" runat="server" />

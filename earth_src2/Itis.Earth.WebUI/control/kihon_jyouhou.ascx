<%@ Control Language="vb" AutoEventWireup="false" Codebehind="kihon_jyouhou.ascx.vb"
    Inherits="Itis.Earth.WebUI.kihon_jyouhou" %>

<%@ Register Src="common_drop.ascx" TagName="common_drop" TagPrefix="uc1" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" RenderMode="Inline">
    <ContentTemplate>
        <table cellpadding="1" class="mainTable" style="text-align: left ; width:968px; ">
            <thead>
                <tr>
                    <th class="tableTitle" colspan="9" rowspan="1" style="text-align: left;height:30px;">
                        <asp:LinkButton ID="lnkTitle" runat="server">基本情報</asp:LinkButton>
                        <asp:Button ID="btnTouroku" runat="server" Text="登録" />
                        <span id="titleInfobar1" runat="server"></span>
                    </th>
                </tr>
            </thead>
            <!--基本情報明細-->
            <tbody id="meisaiTbody1" runat="server" class="itemTable">
                <tr>
                    <td style="font-weight: bold;" class="hissu">
                        請求先名称</td>
                    <td class="hissu" colspan="6" style="font-weight: bold">
                        <asp:TextBox ID="tbxSeisikiMei" runat="server" Width="496px"></asp:TextBox>&nbsp;<asp:Button
                            ID="btnCopy1" runat="server" Text="加盟店名１コピー" Width="120px" />
                        <asp:Button ID="btnCopy2" runat="server" Text="加盟店名２コピー" Width="120px" /></td>
                </tr>
                <tr>
                    <td class="koumokuMei" style="font-weight: bold;">
                        請求先名称２</td>
                    <td colspan="4">
                        <asp:TextBox ID="tbxSeisikiKana" runat="server" Width="296px"></asp:TextBox>&nbsp;<asp:Button
                            ID="btnTenCopy1" runat="server" Text="加盟店名１コピー" Width="120px" />&nbsp;<asp:Button
                                ID="btnTenCopy2" runat="server" Text="加盟店名２コピー" Width="120px" /></td>
                    <td style="font-weight: bold;" class="hissu">
                        都道府県コード
                    </td>
                    <td class="hissu">
                        <uc1:common_drop ID="Common_drop1" runat="server" GetStyle="todoufuken" />
                    </td>
                </tr>
                <tr>
                    <td class="koumokuMei" >
                        年間棟数</td>
                    <td style="">
                        <asp:TextBox ID="tbxlblNenkanTousuu" runat="server" Style="ime-mode: disabled;"></asp:TextBox></td>
                    <td class="koumokuMei" style="">
                        付保証明書FLG</td>
                    <td style="">
                        <asp:DropDownList ID="ddlSyoumeisyo" runat="server" AutoPostBack="true">
                            <asp:ListItem Value="1">有り</asp:ListItem>
                            <asp:ListItem Value="0">なし</asp:ListItem>
                        </asp:DropDownList></td>
                    <td class="koumokuMei" style="">
                        付保証明書<br />
                        開始年月</td>
                    <td colspan="2">
                        <asp:TextBox ID="tbxAddNengetu" runat="server" CssClass="codeNumber" MaxLength="7"
                            Style="width: 72px"></asp:TextBox>
                        <asp:Button ID="btnFuho" runat="server" Text="付保証明有無状況" OnClick="btnFuho_Click" /></td>
                </tr>
                
                <tr>
                    <td class="koumokuMei" >
                        新築住宅引渡し<br />
                        （販売）件数
                        </td>
                    <td style="">
                        <asp:TextBox ID="tbxSintikuKensuu" runat="server" CssClass="codeNumber" Style="ime-mode: disabled;" MaxLength="10"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;棟/年
                    </td>
                    <td class="koumokuMei" style="">
                        不動産売買件数</td>
                    <td style="">
                        <asp:TextBox ID="tbxFudouKensuu" runat="server" CssClass="codeNumber" Style="ime-mode: disabled; width:74px;" MaxLength="8"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;件/年
                        </td>
                    <td class="koumokuMei" >
                        リフォーム<br />
                    前年度 請負金額
</td>
                    <td colspan="2">
                        <asp:TextBox ID="tbxZenNenUkeoiKin" runat="server" CssClass="codeNumber" Style="ime-mode: disabled; width:104px;" MaxLength="20"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;万円/年

                    </td>
                </tr> 
                
                
                
                
                <tr>
                    <td class="koumokuMei">
                        営業担当者</td>
                    <td style="">
                        <asp:TextBox ID="tbxEigyouCd" runat="server" Width="120px" Style="ime-mode: disabled;"></asp:TextBox><asp:Button
                            ID="btnKensaku" runat="server" Text="検索" /><asp:TextBox ID="tbxEigyouMei" runat="server"
                                TabIndex="-1" BorderStyle="None" ReadOnly="True" Width="92px" BackColor="Transparent"></asp:TextBox></td>
                    <td style="" class="koumokuMei">
                        引継完了日</td>
                    <td style="">
                        <asp:TextBox ID="tbxHKDate" runat="server" Width="74px" Style="ime-mode: disabled;"></asp:TextBox></td>
                    <td style="" class="koumokuMei">
                        旧営業担当者</td>
                    <td colspan="2">
                        <asp:TextBox ID="tbxKyuuEigyouCd" runat="server" Width="120px" Style="ime-mode: disabled;"></asp:TextBox><asp:Button
                            ID="btnKyuuKensaku" runat="server" Text="検索" /><asp:TextBox ID="tbxKyuuEigyouMei"
                                runat="server" TabIndex="-1" BorderStyle="None" ReadOnly="True" Width="92px"
                                BackColor="Transparent"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style=" height: 25px" class="koumokuMei">
                        地震補償FLG</td>
                    <td style=" height: 25px">
                        <asp:DropDownList ID="ddlJisinHosyou" runat="server">
                            <asp:ListItem Value="1">有り</asp:ListItem>
                            <asp:ListItem Value="0">なし</asp:ListItem>
                        </asp:DropDownList></td>
                    <td style=" height: 25px" class="koumokuMei">
                        地震補償登録日</td>
                    <td style="height: 25px">
                        <asp:TextBox ID="tbxJisinHosyou" runat="server" Width="74px" Style="ime-mode: disabled;"></asp:TextBox>
                    </td>
                    <td style=" height: 25px" class="koumokuMei">
                        ＳＤＳ<br>
                        自動設定情報</td>
                    <td colspan="2" style="height: 25px; ">
                        <asp:DropDownList ID="ddlSds" runat="server">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="1">1：SDS専用</asp:ListItem>
                            <asp:ListItem Value="2">2：SDS併用</asp:ListItem>
                            <asp:ListItem Value="3">3：SSのみ</asp:ListItem>
                            <asp:ListItem Value="4">4：その他</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="koumokuMei" style="">
                        工事売上種別</td>
                    <td style="">
                        <uc1:common_drop ID="Common_drop2" runat="server" GetStyle="syubetsu" />
                    </td>
                    <td class="koumokuMei" style="">
                        工事サポート<br />
                        システム</td>
                    <td style="">
                        <asp:DropDownList ID="ddlSystem" runat="server">
                            <asp:ListItem Value="1">利用する</asp:ListItem>
                            <asp:ListItem Value="">利用しない</asp:ListItem>
                        </asp:DropDownList></td>
                    <td class="koumokuMei" style="">
                        ＪＩＯ先フラグ</td>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlJio" runat="server">
                            <asp:ListItem Value="1">有り</asp:ListItem>
                            <asp:ListItem Value="">無し</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td class="koumokuMei">
                        対象商品FLG
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_taiou_syouhin_kbn" runat="server">
  
                        </asp:DropDownList>&nbsp; &nbsp; &nbsp; &nbsp;
                        <asp:Label ID="lbl_taiou_syouhin_kbn_set_date" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="koumokuMei">
                        土地レポ無料
                    </td>
                    <td>
                        <asp:Label ID="lbl_tochirepo_muryou_flg" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="koumokuMei">キャンペーン割
                    </td>
                    <td colspan="2">
                        <asp:DropDownList ID="ddl_campaign_waribiki_flg" runat="server">
                            <asp:ListItem Value="1">有り</asp:ListItem>
                            <asp:ListItem Value="">無し</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp; &nbsp; &nbsp; &nbsp;
                        <asp:Label ID="lbl_campaign_waribiki_flg_txt" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </tbody>
        </table>
        <asp:HiddenField ID="hidHi" runat="server" />
        <asp:HiddenField ID="hidHaita" runat="server" />

    </ContentTemplate>
</asp:UpdatePanel>

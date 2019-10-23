<%@ Control Language="vb" AutoEventWireup="false" Codebehind="TeibetuSyouhinRecordCtrl.ascx.vb"
    Inherits="Itis.Earth.WebUI.TeibetuSyouhinRecordCtrl" %>
<%@ Import Namespace="Itis.Earth.Utilities" %>
<%@ Register Src="SeikyuuSiireLinkCtrl.ascx" TagName="SeikyuuSiireLinkCtrl" TagPrefix="uc1" %>
<%@ Register Src="TokubetuTaiouToolTipCtrl.ascx" TagName="TokubetuTaiouToolTipCtrl" TagPrefix="uc" %>

<script type="text/javascript">
</script>

<asp:UpdatePanel ID="UpdatePanelSyouhinRec" runat="server" RenderMode="Inline" UpdateMode="Conditional">
    <ContentTemplate>
        <table class="innerTable" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%;
            border-top: 0px; table-layout: fixed;">
            <tr id="SyouhinRecord" runat="server">
                <td style="width: 245px; border-left: 0px; border-top: 0px; white-space: nowrap;"
                    class="itemNm">
                    <%-- 商品コード --%>
                    <asp:TextBox ID="TextSyouhinCd" runat="server" CssClass="itemCd" MaxLength="8" Width="60px"></asp:TextBox>
                    <%-- 商品検索ボタン --%>
                    <input type="button" name="" id="ButtonSyouhinKensaku" value="検索" class="gyoumuSearchBtn"
                        runat="server" onserverclick="ButtonSyouhinKensaku_ServerClick" />
                    <%-- 確定ドロップダウン（商品３のみ） --%>
                    <asp:DropDownList ID="SelectKakutei" runat="server">
                        <asp:ListItem Value="0">未確定</asp:ListItem>
                        <asp:ListItem Value="1">確定</asp:ListItem>
                    </asp:DropDownList>
                    <%-- 請求先/仕入先リンク --%>
                    <uc1:SeikyuuSiireLinkCtrl ID="SeikyuuSiireLinkCtrl" runat="server" />
                    <%-- 特別対応ツールチップ --%>
                    <uc:TokubetuTaiouToolTipCtrl ID="TokubetuTaiouToolTipCtrl" runat="server" />
                    <br />
                    <%-- 商品名 --%>
                    <asp:Label ID="SpanSyouhinName" runat="server" Style="width: 200px" />
                    <input id="ButtonHiddenSyouhinKensaku" runat="server" onserverclick="ButtonHiddenSyouhinKensaku_ServerClick"
                        style="display: none" type="button" value="検索(非表示)" /></td>
                <td style="border-left: solid 3px gray; border-top: 0px; width: 73px;" id="TdSyoudakusyoKingaku"
                    runat="server">
                    <%-- 承諾書金額 --%>
                    <asp:TextBox ID="TextSyoudakusyoKingaku" runat="server" CssClass="kingaku" MaxLength="7"
                        Width="65px" OnTextChanged="TextSyoudakusyoKingaku_TextChanged" AutoPostBack="False"></asp:TextBox><br />
                    <%-- 仕入消費税額 --%>
                    <asp:TextBox ID="TextSiireSyouhizeiGaku" runat="server" CssClass="kingaku" Width="65px"
                        TabIndex="-1" OnTextChanged="TextSiireSyouhizeiGaku_TextChanged" AutoPostBack="False"
                        MaxLength="7"></asp:TextBox>
                </td>
                <td style="border-top: 0px; width: 73px;" id="TdDenpyouSiireNengappi" runat="server">
                    <%-- 伝票仕入年月日 --%>
                    <asp:TextBox ID="TextDenpyouSiireNengappiDisplay" runat="server" CssClass="date readOnlyStyle2"
                        ReadOnly="True" TabIndex="-1" Width="65px"></asp:TextBox><br />
                    <%-- 伝票仕入年月日修正 --%>
                    <asp:TextBox ID="TextDenpyouSiireNengappi" runat="server" CssClass="date" MaxLength="10"
                        BackColor="#ddaaee" Width="65px" AutoPostBack="False"></asp:TextBox>
                </td>
                <td id="TdKoumutenSeikyuuGaku" runat="server" style="border-top: 0px; border-left: solid 3px gray;
                    width: 73px;">
                    <%-- 工務店請求金額 --%>
                    <asp:TextBox ID="TextKoumutenSeikyuuGaku" runat="server" CssClass="kingaku" MaxLength="7"
                        Width="65px" AutoPostBack="False" OnTextChanged="TextKoumutenSeikyuuGaku_TextChanded"></asp:TextBox><br />
                    <%-- 実請求金額 --%>
                    <asp:TextBox ID="TextJituSeikyuuGaku" runat="server" CssClass="kingaku" Width="65px"
                        OnTextChanged="TextJituSeikyuuGaku_TextChanged" AutoPostBack="False" MaxLength="7"></asp:TextBox>
                    <%-- 見積書作成日(非表示) --%>
                    <asp:TextBox ID="TextMitumorisyoSakuseibi" runat="server" CssClass="date" Visible="false"
                        MaxLength="10" Width="65px"></asp:TextBox>
                </td>
                <td style="border-top: 0px; width: 73px;">
                    <%-- 消費税額 --%>
                    <asp:TextBox ID="TextSyouhizeiGaku" runat="server" CssClass="kingaku" Width="65px"
                        OnTextChanged="TextSyouhizeiGaku_TextChanged" AutoPostBack="False" MaxLength="7"></asp:TextBox><br />
                    <%-- 税込金額 --%>
                    <asp:TextBox ID="TextZeikomiKingaku" runat="server" BorderStyle="None" CssClass="kingaku readOnlyStyle2"
                        ReadOnly="True" TabIndex="-1" Width="65px"></asp:TextBox>
                </td>
                <td style="border-top: 0px; width: 71px;">
                    <%-- 伝票売上年月日 --%>
                    <asp:TextBox ID="TextDenpyouUriageNengappiDisplay" runat="server" CssClass="date readOnlyStyle2"
                        ReadOnly="True" TabIndex="-1" Width="65px"></asp:TextBox><br />
                    <%-- 伝票売上年月日修正 --%>
                    <asp:TextBox ID="TextDenpyouUriageNengappi" runat="server" CssClass="date" MaxLength="10"
                        BackColor="#ddaaee" Width="65px" OnTextChanged="TextDenpyouUriageNengappi_TextChanged"
                        AutoPostBack="False"></asp:TextBox>
                </td>
                <td style="border-top: 0px; width: 110px;">
                    <%-- 売上年月日 --%>
                    <asp:TextBox ID="TextUriageNengappi" runat="server" CssClass="date" MaxLength="10"
                        TabIndex="-1" Width="65px" OnTextChanged="TextUriageNengappi_TextChanged" AutoPostBack="False"></asp:TextBox></br>
                    <%-- 売上処理 --%>
                    <asp:UpdatePanel ID="UpdatePanelUridate" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                        <ContentTemplate>
                            <%-- 売上処理プルダウン --%>
                            <asp:DropDownList ID="SelectUriageSyori" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SelectUriageSyori_SelectedIndexChanged"
                                Style="font-size: 10px; width: 36px;" TabIndex="-1">
                                <asp:ListItem Selected="True" Value="0">未</asp:ListItem>
                                <asp:ListItem Value="1">済</asp:ListItem>
                            </asp:DropDownList><%-- 売上日 --%><asp:TextBox ID="TextUriageBi" runat="server" CssClass="date readOnlyStyle2"
                                TabIndex="-1" BorderStyle="None" ReadOnly="True"></asp:TextBox></ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="SelectUriageSyori" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td colspan="2" style="border-top: 0px; border-left: solid 3px gray; width: 71px;">
                    <%-- 請求書発行日 --%>
                    <asp:TextBox ID="TextSeikyuusyoHakkoubi" runat="server" CssClass="date" MaxLength="10"
                        Width="65px"></asp:TextBox><br />
                    <%-- 請求有無 --%>
                    <asp:DropDownList ID="SelectSeikyuuUmu" runat="server" OnSelectedIndexChanged="SelectSeikyuuUmu_SelectedIndexChanged"
                        AutoPostBack="True" Style="font-size: 10px; width: 36px;" TabIndex="-1">
                        <asp:ListItem Selected="True" Value=""></asp:ListItem>
                        <asp:ListItem Value="1">有</asp:ListItem>
                        <asp:ListItem Value="0">無</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="text-align: center; border-top: 0px; border-left: solid 3px gray; width: 100%;">
                    <%-- 発注書金額 --%>
                    <asp:TextBox ID="TextHattyuusyoKingaku" runat="server" AutoPostBack="False" CssClass="kingaku"
                        MaxLength="7" Width="65px" OnTextChanged="TextHattyuusyoKingaku_TextChanged"></asp:TextBox><%-- 発注書確定 --%><asp:DropDownList
                            ID="SelectHattyuusyoKakutei" runat="server" AutoPostBack="True" Style="font-size: 10px;
                            width: 36px;" OnSelectedIndexChanged="SelectHattyuusyoKakutei_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Value="0">未</asp:ListItem>
                            <asp:ListItem Value="1">確</asp:ListItem>
                        </asp:DropDownList>
                    <br />
                    <%-- 発注書確認日 --%>
                    <asp:TextBox ID="TextHattyuusyoKakuninbi" runat="server" CssClass="date" Width="65px"
                        MaxLength="10"></asp:TextBox>
                </td>
                <td id="TdSpacer" runat="server" style="border-top: 0px; border-left: solid 3px gray;
                    width: 144px; display: none;">
                    <%-- ＊＊＊＊以下、非表示項目列挙＊＊＊＊ --%>
                    <%-- 売上処理（非表示） --%>
                    <asp:HiddenField ID="HiddenUriageSyori" runat="server" />
                    <%-- 発注書金額変更前（非表示） --%>
                    <asp:HiddenField ID="HiddenHattyuusyoKingakuOld" runat="server" />
                    <%-- 発注書フラグ変更前（非表示） --%>
                    <asp:HiddenField ID="HiddenHattyuusyoFlgOld" runat="server" />
                    <%-- 入金額（非表示） --%>
                    <asp:HiddenField ID="HiddenNyuukinGaku" runat="server" />
                    <%-- 税率（非表示） --%>
                    <asp:HiddenField ID="HiddenZeiritu" runat="server" />
                    <%-- 税区分（非表示） --%>
                    <asp:HiddenField ID="HiddenZeiKbn" runat="server" />
                    <%-- 表示モード（非表示） --%>
                    <asp:HiddenField ID="HiddenDispMode" runat="server" />
                    <%-- 請求タイプ（非表示） --%>
                    <asp:HiddenField ID="HiddenSeikyuuType" runat="server" />
                    <%-- 金額フラグ（非表示） --%>
                    <asp:HiddenField ID="HiddenKingakuFlg" runat="server" />
                    <%-- 系列コード（非表示） --%>
                    <asp:HiddenField ID="HiddenKeiretuCd" runat="server" />
                    <%-- 系列コード（非表示） --%>
                    <asp:HiddenField ID="HiddenKameitenCd" runat="server" />
                    <%-- どの商品を表すか（非表示） --%>
                    <asp:HiddenField ID="HiddenTargetId" runat="server" />
                    <%-- 発注書管理権限（非表示） --%>
                    <asp:HiddenField ID="HiddenHattyuusyoKanriKengen" runat="server" />
                    <%-- 経理業務権限（非表示） --%>
                    <asp:HiddenField ID="HiddenKeiriGyoumuKengen" runat="server" />
                    <%-- 更新日時（非表示） --%>
                    <asp:HiddenField ID="HiddenUpdDatetime" runat="server" />
                    <%-- ログインユーザーＩＤ（非表示） --%>
                    <asp:HiddenField ID="HiddenLoginUserId" runat="server" />
                    <asp:UpdatePanel ID="UpdatePanelHosyouMessage" runat="server" RenderMode="Inline"
                        UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:HiddenField ID="HiddenHosyouMessage" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <%-- 画面表示NO（非表示） --%>
                    <asp:HiddenField ID="HiddenGamenHyoujiNo" runat="server" />
                    <%-- 分類コード（非表示） --%>
                    <asp:HiddenField ID="HiddenBunruiCd" runat="server" />
                    <%-- 区分（非表示） --%>
                    <asp:HiddenField ID="HiddenKubun" runat="server" />
                    <%-- 番号（非表示） --%>
                    <asp:HiddenField ID="HiddenBangou" runat="server" />
                    <%-- 商品コード変更前（非表示） --%>
                    <asp:HiddenField ID="HiddenSyouhinCdOld" runat="server" />
                    <%-- 請求有無変更前（非表示） --%>
                    <asp:HiddenField ID="HiddenSeikyuuUmuOld" runat="server" />
                    <%-- 画面起動時情報保持_売上（非表示） --%>
                    <asp:HiddenField ID="HiddenOpenValuesUriage" runat="server" />
                    <%-- 画面起動時情報保持_仕入（非表示） --%>
                    <asp:HiddenField ID="HiddenOpenValuesSiire" runat="server" />
                    <asp:HiddenField runat="server" ID="HiddenOpenValue" />
                    <asp:HiddenField runat="server" ID="HiddenKeyValue" />
                    <%-- 画面起動時情報保持_売上（非表示） --%>
                    <asp:HiddenField ID="HiddenOpenValuesTokubetuTaiou" runat="server" />
                    <asp:HiddenField ID="HiddenTokubetuTaiouUpdFlg" runat="server" />
                </td>
            </tr>
            <tr id="TableSpacer" runat="server">
                <td class="tableSpacer" colspan="11">
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>

<%@ Control Language="vb" AutoEventWireup="false" Codebehind="TeibetuKoujiRecordCtrl.ascx.vb"
    Inherits="Itis.Earth.WebUI.TeibetuKoujiRecordCtrl" %>
<%@ Register Src="NyuukinZangakuCtrl.ascx" TagName="NyuukinZangakuCtrl" TagPrefix="uc1" %>
<%@ Register Src="SeikyuuSiireLinkCtrl.ascx" TagName="SeikyuuSiireLinkCtrl" TagPrefix="uc2" %>
<%@ Import Namespace="Itis.Earth.Utilities" %>
<tr>
    <td colspan="5" id="CtrlTitle" class="syoBunrui" style="border-right: none;" runat="server">
        (改良工事)</td>
    <td class="syoBunrui" colspan="6" style="border-left: none;">
        <uc1:NyuukinZangakuCtrl ID="NyuukinZangakuCtrlKouji" runat="server" isNyuukingaku="True" />
    </td>
</tr>
<tr>
    <td id="KoujigaisyaTitle" class="koumokuMei" runat="server">
        工事会社
    </td>
    <td colspan="6">
        <asp:UpdatePanel ID="UpdatePanelKoujiKaisya" runat="server" RenderMode="Inline" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:TextBox ID="TextKoujigaisyaCd" runat="server" MaxLength="7" Width="60px" CssClass="codeNumber"></asp:TextBox><input
                    type="button" id="ButtonKoujigaisyaKensaku" name="" value="検索" class="gyoumuSearchBtn"
                    onserverclick="ButtonKoujigaisyaKensaku_ServerClick" runat="server" />
                <asp:TextBox ID="TextKoujigaisyaMei" runat="server" CssClass="readOnlyStyle" MaxLength="20"
                    ReadOnly="True" TabIndex="-1" Width="250px"></asp:TextBox>&nbsp;
                <asp:HiddenField ID="HiddenKameitenCd" runat="server" />
                <asp:HiddenField ID="HiddenKeiretuCd" runat="server" />
                <asp:HiddenField ID="HiddenEigyousyoCd" runat="server" />
                <asp:HiddenField ID="HiddenNg" runat="server" />
                <asp:HiddenField ID="HiddenKoujigaisyaCdOld" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </td>
    <td colspan="4">
        <asp:UpdatePanel ID="UpdatePanelKoujigaisyaSeikyuu" runat="server" RenderMode="Inline"
            UpdateMode="Conditional">
            <ContentTemplate>
                <asp:CheckBox ID="CheckKoujigaisyaSeikyuu" runat="server" AutoPostBack="True" Text="工事会社請求 "
                    OnCheckedChanged="CheckKoujigaisyaSeikyuu_CheckedChanged" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </td>
</tr>
<tr>
    <td class="koumokuMei">
        商品コード</td>
    <td colspan="10" class="itemNm">
        <asp:UpdatePanel ID="UpdatePanelSyouhinCd" runat="server" RenderMode="Inline" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:DropDownList ID="SelectSyouhinCd" runat="server" Width="250px" AutoPostBack="True">
                </asp:DropDownList>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanelSeikyuuSiireLink" runat="server" RenderMode="inline"
            UpdateMode="conditional">
            <ContentTemplate>
                <%-- 請求先/仕入先リンク --%>
                <uc2:SeikyuuSiireLinkCtrl ID="SeikyuuSiireLinkCtrl" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:HiddenField ID="HiddenSyouhinCd" runat="server" />
    </td>
</tr>
<tr>
    <td colspan="11" style="padding: 0px;">
        <asp:UpdatePanel ID="UpdatePanelKoujiSyouhin" runat="server" RenderMode="Inline"
            UpdateMode="Conditional">
            <ContentTemplate>
                <table class="innerTable itemTableNarrow" cellpadding="0" cellspacing="0" style="width: 100%;
                    height: 100%; border-top: 0px;">
                    <tr class="shouhinTableTitle">
                        <td style="border-top: 0px; border-left: none;" rowspan="2">
                            承諾書金額</td>
                        <td style="border-top: 0px;">
                            仕入消費税額</td>
                        <td style="border-top: 0px;">
                            伝票仕入年月日</td>
                        <td style="border-top: 0px; border-left: solid 3px gray;" rowspan="2">
                            実請求金額</td>
                        <td style="border-top: 0px;">
                            消費税</td>
                        <td style="border-top: 0px;">
                            伝票売上年月日</td>
                        <td style="border-top: 0px;">
                            売上年月日</td>
                        <td style="border-top: 0px; border-left: solid 3px gray;">
                            請求書発行日</td>
                        <td style="border-top: 0px; border-left: solid 3px gray;">
                            発注書金額</td>
                        <td style="border-top: 0px;">
                            発注書確定</td>
                    </tr>
                    <tr class="shouhinTableTitle">
                        <td>
                            仕入税込金額</td>
                        <td>
                            伝票仕入年月日修正</td>
                        <td>
                            税込金額</td>
                        <td>
                            伝票売上年月日修正</td>
                        <td>
                            売上処理</td>
                        <td style="border-left: solid 3px gray;">
                            請求有無</td>
                        <td colspan="2" style="border-left: solid 3px gray;">
                            発注書確認日</td>
                    </tr>
                    <tr>
                        <td style="border-left: none;">
                            <asp:TextBox ID="TextSiireZeinukiKingaku" runat="server" CssClass="kingaku" MaxLength="7"
                                Width="70px" AutoPostBack="False" OnTextChanged="TextSiireZeinukiKingaku_TextChanged"></asp:TextBox>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanelSiireZei" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="TextSiireSyouhizeiGaku" runat="server" CssClass="kingaku" OnTextChanged="TextSiireSyouhizeiGaku_TextChanged"
                                        AutoPostBack="False" MaxLength="7" Width="60px"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="TextSiireZeinukiKingaku" EventName="TextChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <br />
                            <asp:UpdatePanel ID="UpdatePanelSiireZeikomi" runat="server" RenderMode="Inline"
                                UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="TextSiireZeikomiKingaku" runat="server" CssClass="kingaku readOnlyStyle2"
                                        MaxLength="8" ReadOnly="True" TabIndex="-1" Width="70px"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="TextSiireZeinukiKingaku" EventName="TextChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="TextSiireSyouhizeiGaku" EventName="TextChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <%-- 伝票仕入年月日 --%>
                            <asp:TextBox ID="TextDenpyouSiireNengappiDisplay" runat="server" CssClass="date readOnlyStyle2"
                                ReadOnly="True" TabIndex="-1" Width="65px"></asp:TextBox><br />
                            <%-- 伝票仕入年月日修正 --%>
                            <asp:TextBox ID="TextDenpyouSiireNengappi" runat="server" CssClass="date" MaxLength="10" BackColor="#ddaaee"
                                Width="65px" AutoPostBack="False"></asp:TextBox>
                        </td>
                        <td style="border-left: solid 3px gray;">
                            <asp:TextBox ID="TextUriageZeinukiKingaku" runat="server" CssClass="kingaku" MaxLength="7"
                                Width="70px" AutoPostBack="False" OnTextChanged="TextUriageZeinukiKingaku_TextChanged"></asp:TextBox>
                            <asp:HiddenField ID="HiddenBunruiCd" runat="server" />
                            <asp:HiddenField ID="HiddenZeiritu" runat="server" />
                            <asp:HiddenField ID="HiddenZeiKbn" runat="server" />
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanelUriZei" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="TextUriageSyouhizeiGaku" runat="server" CssClass="kingaku" MaxLength="10"
                                        Width="60px" OnTextChanged="TextUriageSyouhizeiGaku_TextChanged" AutoPostBack="False"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="TextUriageZeinukiKingaku" EventName="TextChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <br />
                            <asp:UpdatePanel ID="UpdatePanelZeikomi" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="TextUriageZeikomiKingaku" runat="server" BorderStyle="None" CssClass="kingaku readOnlyStyle2"
                                        MaxLength="10" ReadOnly="True" TabIndex="-1" Width="70px">
                                    </asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="TextUriageZeinukiKingaku" EventName="TextChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="TextUriageSyouhizeiGaku" EventName="TextChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:TextBox ID="TextDenpyouUriageNengappiDisplay" runat="server" CssClass="date readOnlyStyle2"
                                ReadOnly="true" AutoPostBack="False" TabIndex="-1"></asp:TextBox><br />
                            <asp:UpdatePanel ID="UpdatePanelDenpyouUriageNengappi" runat="server" RenderMode="Inline"
                                UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="TextDenpyouUriageNengappi" runat="server" CssClass="date" MaxLength="10" BackColor="#ddaaee"
                                        OnTextChanged="TextDenpyouUriageNengappi_TextChanged" AutoPostBack="False"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="TextUriageNengappi" EventName="TextChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:TextBox ID="TextUriageNengappi" runat="server" CssClass="date" MaxLength="10"
                                TabIndex="-1" OnTextChanged="TextUriageNengappi_TextChanged" AutoPostBack="False"></asp:TextBox>
                            <br />
                            <asp:UpdatePanel ID="UpdatePanelUridate" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:DropDownList ID="SelectUriageSyori" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SelectUriageSyori_SelectedIndexChanged"
                                        TabIndex="-1">
                                        <asp:ListItem Selected="True" Value="0">未</asp:ListItem>
                                        <asp:ListItem Value="1">済</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="TextUriagebi" runat="server" CssClass="date readOnlyStyle2" ReadOnly="True"
                                        BorderStyle="None" TabIndex="-1" MaxLength="10"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td style="border-left: solid 3px gray;">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="TextSeikyuusyoHakkoubi" runat="server" CssClass="date" MaxLength="10"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="TextDenpyouUriageNengappi" EventName="TextChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <br />
                            <asp:DropDownList ID="SelectSeikyuuUmu" runat="server" AutoPostBack="True" TabIndex="-1">
                                <asp:ListItem Selected="True" Value="1">有り</asp:ListItem>
                                <asp:ListItem Value="0">無し</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td colspan="2" style="border-left: solid 3px gray;">
                            <asp:TextBox ID="TextHattyuusyoKingaku" runat="server" CssClass="kingaku" MaxLength="7"
                                Width="70px" AutoPostBack="False" OnTextChanged="TextHattyuusyoKingaku_TextChanged"></asp:TextBox>
                            <asp:DropDownList ID="SelectHattyuusyoKakutei" runat="server" AutoPostBack="True"
                                OnSelectedIndexChanged="SelectHattyuusyoKakutei_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="0">未確定</asp:ListItem>
                                <asp:ListItem Value="1">確定</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <asp:TextBox ID="TextHattyuusyoKakuninbi" runat="server" CssClass="date" MaxLength="10"></asp:TextBox>
                            <%-- ＊＊＊＊以下、非表示項目列挙＊＊＊＊ --%>
                            <%-- 解約払い戻しボタン --%>
                            <asp:HiddenField ID="HiddenKubun" runat="server" />
                            <asp:HiddenField ID="HiddenBangou" runat="server" />
                            <asp:HiddenField ID="HiddenHattyuusyoKanriKengen" runat="server" />
                            <asp:HiddenField ID="HiddenKeiriGyoumuKengen" runat="server" />
                            <asp:HiddenField ID="HiddenHattyuusyoKingakuOld" runat="server" />
                            <asp:HiddenField ID="HiddenHattyuusyoFlgOld" runat="server" />
                            <asp:HiddenField ID="HiddenLoginUserId" runat="server" />
                            <asp:HiddenField ID="HiddenUpdDatetime" runat="server" />
                            <%-- 画面起動時情報保持_売上（非表示） --%>
                            <input type="hidden" id="HiddenOpenValuesUriage" runat="server" />
                            <%-- 画面起動時情報保持_仕入（非表示） --%>
                            <input type="hidden" id="HiddenOpenValuesSiire" runat="server" />
                            <asp:HiddenField runat="server" ID="HiddenOpenValue" />
                            <asp:HiddenField runat="server" ID="HiddenKeyValue" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="SelectSyouhinCd" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </td>
</tr>

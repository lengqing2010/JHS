<%@ Page Language="vb" AutoEventWireup="false" Codebehind="HannyouUriageSyuusei.aspx.vb"
    Inherits="Itis.Earth.WebUI.HannyouUriageSyuusei" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script type="text/javascript">
        history.forward();
  
        //ウィンドウサイズ変更
        try{
            window.resizeTo(840,560);
        }catch(e){
            //アクセスが拒否されましたのエラーが出たら何もしない。
            if(e.number == 2147024891) throw e;
        }
        
        _d = document;
        
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="SM1" runat="server">
            </asp:ScriptManager>
            <input type="hidden" id="HiddenUpdDatetime" runat="server" />
            <input type="hidden" id="HiddenUniqueNo" runat="server" />
            <input type="hidden" id="HiddenGamenMode" runat="server" />
            <%-- 施主名活性化制御用(初期読込判断用) --%>
            <input type="hidden" id="HiddenSesyuFlg" runat="server" />
            <%-- 画面起動時情報保持 --%>
            <asp:HiddenField ID="HiddenOpenValues" runat="server" />
            <%-- 画面タイトル --%>
            <table>
                <tr>
                    <td>
                        <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
                            class="titleTable">
                            <tr>
                                <th style="text-align: left; width: 160px;">
                                    <span id="SpanTitle" runat="server"></span>
                                </th>
                                <th style="width: 70px">
                                    <input type="button" id="ButtonClose" value="閉じる" onclick="window.close();" runat="server" />
                                </th>
                                <th style="text-align: right">
                                    <input type="button" id="ButtonUpdate" runat="server" value="" style="font-weight: bold;
                                        font-size: 18px; width: 100px; color: black; height: 30px; background-color: fuchsia" />
                                </th>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br />
            <%-- 画面上部[データ詳細] --%>
            <asp:UpdatePanel ID="UpdatePanelSyouhinInfo" UpdateMode="Conditional" runat="server"
                RenderMode="Inline">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ButtonSearchSyouhin" />
                    <asp:AsyncPostBackTrigger ControlID="SelectUriageTenKbn" />
                </Triggers>
                <ContentTemplate>
                    <table cellpadding="0" cellspacing="0" style="border-bottom: solid 2px gray; border-left: solid 2px gray;"
                        class="mainTable">
                        <tr>
                            <td class="koumokuMei" style="width: 100px;">
                                汎用売上NO</td>
                            <td colspan="2" style="width: 150px;">
                                <asp:TextBox ID="TextHanUriNo" runat="server" Style="width: 50px" CssClass="number readOnlyStyle"
                                    ReadOnly="true" TabIndex="-1" />
                            </td>
                            <td class="koumokuMei" style="width: 147px;">
                                取消</td>
                            <td colspan="4">
                                <asp:CheckBox ID="CheckTorikesi" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="koumokuMei">
                                商品</td>
                            <td colspan="7">
                                <asp:UpdatePanel ID="UpdatePanelSyouhin" UpdateMode="Conditional" runat="server"
                                    RenderMode="Inline">
                                    <ContentTemplate>
                                        <asp:TextBox ID="TextSyouhinCd" runat="server" CssClass="itemCd hissu" MaxLength="8"
                                            Width="60px"></asp:TextBox><input type="hidden" id="HiddenSyouhinCdOld" runat="server" /><input
                                                type="hidden" id="HiddenSyouhinSearchType" runat="server" /><input type="hidden"
                                                    id="HiddenTargetId" runat="server" /><input type="hidden" id="HiddenZeiritu" runat="server" />
                                        <input type="button" name="" id="ButtonSearchSyouhin" value="検索" class="gyoumuSearchBtn"
                                            runat="server" onserverclick="ButtonSearchSyouhin_ServerClick" />
                                        <asp:TextBox ID="TextSyouhinMei" Style="width: 300px;" runat="server" MaxLength="40"
                                            CssClass="hissu" />
                                        <span id="SpanUriageSyoriZumi" style="color: red; font-weight: bold;" runat="server">
                                        </span>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="koumokuMei">
                                売上店</td>
                            <td colspan="7">
                                <asp:UpdatePanel ID="UpdatePanelUriageTen" UpdateMode="Conditional" runat="server"
                                    RenderMode="Inline">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="SelectUriageTenKbn" runat="server" CssClass="" OnSelectedIndexChanged="SelectUriageTenKbn_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList><span id="SpanUriageTenKbn" runat="server"></span>
                                        <asp:TextBox ID="TextUriageTenCd" runat="server" MaxLength="7" Style="width: 60px;"
                                            CssClass="codeNumber" OnTextChanged="TextUriageTenCd_TextChanged" />
                                        <input id="ButtonSearchUriageTen" runat="server" type="button" value="検索" class="gyoumuSearchBtn"
                                            onserverclick="ButtonSearchUriageTen_ServerClick" />
                                        <asp:TextBox ID="TextUriageTenMei" Style="width: 300px;" runat="server" MaxLength="40"
                                            ReadOnly="true" CssClass="readOnlyStyle" TabIndex="-1" />
                                        <input type="hidden" id="HiddenUriageSearchType" runat="server" />
                                        <input type="hidden" id="HiddenKbn" runat="server" />
                                        <input type="hidden" id="HiddenKamentenCd" runat="server" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="koumokuMei">
                                請求先</td>
                            <td colspan="5" style="width:650px">
                                <asp:UpdatePanel ID="UpdatePanelSeikyuuSaki" UpdateMode="Conditional" runat="server"
                                    RenderMode="Inline">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ButtonSearchUriageTen" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:DropDownList ID="SelectSeikyuuSakiKbn" runat="server" CssClass="hissu" OnSelectedIndexChanged="SelectSeikyuuSakiKbn_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList><span id="SpanSeikyuuKbn" runat="server"></span>
                                        <asp:TextBox ID="TextSeikyuuSakiCd" runat="server" MaxLength="5" Style="width: 40px;"
                                            CssClass="codeNumber hissu" OnTextChanged="TextSeikyuuSakiCd_TextChanged" />&nbsp;-
                                        <asp:TextBox ID="TextSeikyuuSakiBrc" runat="server" MaxLength="2" Style="width: 20px;"
                                            CssClass="codeNumber hissu" OnTextChanged="TextSeikyuuSakiBrc_TextChanged" />
                                        <input id="ButtonSearchSeikyuuSaki" runat="server" type="button" value="検索" class="gyoumuSearchBtn" />
                                        <a id="LinkSeikyuuSakiMei" runat="server" style="width:190px"></a>
                                        <asp:TextBox ID="TextSeikyuuSakimeiHidden" runat="server" Style="display: none;"
                                            TabIndex="-1" />
                                        <input type="hidden" id="HiddenSeikyuuSakiCdOld" runat="server" />
                                        <input type="hidden" id="HiddenSeikyuuSakiBrcOld" runat="server" />
                                        <input type="hidden" id="HiddenSeikyuuSakiKbnOld" runat="server" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td class="koumokuMei">
                                取消</td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel_SeikyuusakiToikesi" UpdateMode="Conditional" runat="server"
                                    RenderMode="Inline">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ButtonSearchSeikyuuSaki" />
                                        <asp:AsyncPostBackTrigger ControlID="SelectSeikyuuSakiKbn" />
                                        <asp:AsyncPostBackTrigger ControlID="TextSeikyuuSakiCd" />
                                        <asp:AsyncPostBackTrigger ControlID="TextSeikyuuSakiBrc" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:TextBox ID="TextTorikesiRiyuu" runat="server" CssClass="readOnlyStyle" ReadOnly="True"
                                            TabIndex="-1" Width="80px"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="koumokuMei">
                                単価</td>
                            <td colspan="2">
                                <asp:TextBox ID="TextTanka" runat="server" Style="width: 80px" CssClass="kingaku hissu"
                                    MaxLength="7" OnTextChanged="TextTanka_TextChanged" />
                            </td>
                            <td class="koumokuMei" style="width: 147px;">
                                数量</td>
                            <td>
                                <asp:TextBox ID="TextSuu" runat="server" Style="width: 50px" CssClass="kingaku hissu"
                                    MaxLength="3" OnTextChanged="TextSuu_TextChanged" />
                            </td>
                            <td class="koumokuMei">
                                税区分</td>
                            <td colspan="2">
                                <asp:DropDownList runat="server" ID="SelectZeiKbn" Width="80px" CssClass="hissu"
                                    OnSelectedIndexChanged="SelectZeiKbn_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList><span id="SpanZeiKbn" runat="server"></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="koumokuMei">
                                税込売上金額</td>
                            <td colspan="2">
                                <asp:TextBox ID="TextUriGaku" runat="server" Style="width: 120px" CssClass="kingaku readOnlyStyle"
                                    ReadOnly="true" TabIndex="-1" />
                            </td>
                            <td class="koumokuMei" style="width: 147px;">
                                消費税額</td>
                            <td colspan="4">
                                <asp:TextBox ID="TextSyouhiZeiGaku" runat="server" Style="width: 100px" CssClass="kingaku hissu"
                                    MaxLength="10" OnTextChanged="TextSyouhiZeiGaku_TextChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td class="koumokuMei">
                                売上年月日</td>
                            <td colspan="2">
                                <asp:TextBox ID="TextUriDate" runat="server" Style="width: 70px" CssClass="date hissu"
                                 MaxLength="10" OnTextChanged="TextUriDate_TextChanged" AutoPostBack="False" />
                            </td>
                            <td class="koumokuMei" style="width: 147px;">
                                請求年月日</td>
                            <td colspan="4">
                                <asp:UpdatePanel ID="UpdatePanelSeikyuuDate" UpdateMode="Conditional" runat="server"
                                    RenderMode="Inline">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ButtonSearchUriageTen" />
                                        <asp:AsyncPostBackTrigger ControlID="ButtonSearchSeikyuuSaki" />
                                        <asp:AsyncPostBackTrigger ControlID="TextUriDate" />
                                        <asp:AsyncPostBackTrigger ControlID="TextDenpyouUriDate" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:TextBox ID="TextSeikyuuDate" runat="server" Style="width: 70px" CssClass="date hissu"
                                            MaxLength="10" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="koumokuMei">
                                伝票売上年月日</td>
                            <td colspan="2">
                                <asp:TextBox ID="TextDenpyouUriDateDisplay" runat="server" CssClass="date readOnlyStyle2"
                                    ReadOnly="True" TabIndex="-1" Width="70px"></asp:TextBox>
                            </td>
                            <td class="koumokuMei" style="width: 147px;">
                                伝票売上年月日修正</td>
                            <td colspan="4">
                                <asp:UpdatePanel ID="UpdatePanelDenpyouUriDateSyuusei" UpdateMode="Conditional" runat="server"
                                    RenderMode="Inline">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="TextUriDate" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:TextBox ID="TextDenpyouUriDate" runat="server" CssClass="date" MaxLength="10"
                                            BackColor="#ddaaee" Width="70px" OnTextChanged="TextDenpyouUriDate_TextChanged"
                                            AutoPostBack="False"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="koumokuMei">
                                区分</td>
                            <td>
                                <asp:TextBox ID="TextKbn" runat="server" MaxLength="1" Style="width: 20px;" CssClass="codeNumber"
                                    OnTextChanged="setSesyumeiControl" AutoPostBack="true" />
                                <input type="hidden" id="HiddenKubun" runat="server" />
                            </td>
                            <td class="koumokuMei">
                                番号</td>
                            <td colspan="5">
                                <asp:TextBox ID="TextHosyousyoNo" runat="server" MaxLength="10" Style="width: 75px;"
                                    CssClass="codeNumber" OnTextChanged="setSesyumeiControl" AutoPostBack="true" />
                                <input type="hidden" id="HiddenBangou" runat="server" />
                                <input id="ButtonSesyumeiSyutoku" runat="server" type="button" value="施主名取得" onserverclick="ButtonSearchSesyumei_ServerClick" />
                                <asp:UpdatePanel ID="UpdatePanelSesyumei" UpdateMode="Conditional" runat="server"
                                    RenderMode="Inline">
                                    <ContentTemplate>
                                        <asp:TextBox ID="TextSesyumei" runat="server" MaxLength="50" Style="width: 270px;" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="koumokuMei">
                                摘要</td>
                            <td colspan="7">
                                <textarea id="TextAreaTekiyou" runat="server" cols="100" onfocus="this.select();"
                                    rows="6" style="ime-mode: active; font-family: Sans-Serif"></textarea>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
    <!-- 検索画面遷移用フォーム -->
    <form id="searchForm" method="post" action="">
        <!-- 検索条件値格納用 -->
        <input type="hidden" id="sendSearchTerms" name="sendSearchTerms" />
        <!-- 検索結果セット先ID格納用 -->
        <input type="hidden" id="returnTargetIds" name="returnTargetIds" />
        <!-- 結果セット後実行ボタンID格納用 -->
        <input type="hidden" id="afterEventBtnId" name="afterEventBtnId" />
    </form>
</body>
</html>

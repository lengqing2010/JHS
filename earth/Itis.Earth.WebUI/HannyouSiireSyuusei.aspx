<%@ Page Language="vb" AutoEventWireup="false" Codebehind="HannyouSiireSyuusei.aspx.vb"
    Inherits="Itis.Earth.WebUI.HannyouSiireSyuusei" %>

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
            window.resizeTo(800,500);
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
                                        font-size: 18px; width: 120px; color: black; height: 30px; background-color: fuchsia" />
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
                <ContentTemplate>
                    <table cellpadding="0" cellspacing="0" style="border-bottom: solid 2px gray; border-left: solid 2px gray;"
                        class="mainTable">
                        <tr>
                            <td class="koumokuMei" style="width: 100px">
                                汎用仕入NO</td>
                            <td colspan="2">
                                <asp:TextBox ID="TextHanSiireNo" runat="server" Style="width: 50px" CssClass="number readOnlyStyle"
                                    ReadOnly="true" TabIndex="-1" />
                            </td>
                            <td class="koumokuMei" style="width: 100px">
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
                                            Width="60px" OnTextChanged="TextSyouhinCd_TextChanged"></asp:TextBox><input type="hidden"
                                                id="HiddenSyouhinCdOld" runat="server" /><input type="hidden" id="HiddenSyouhinSearchType"
                                                    runat="server" /><input type="hidden" id="HiddenTargetId" runat="server" /><input
                                                        type="hidden" id="HiddenZeiritu" runat="server" />
                                        <input type="button" name="" id="ButtonSearchSyouhin" value="検索" class="gyoumuSearchBtn"
                                            runat="server" onserverclick="ButtonSearchSyouhin_ServerClick" />
                                        <asp:TextBox ID="TextSyouhinMei" Style="width: 300px;" runat="server" MaxLength="40"
                                            ReadOnly="true" CssClass="readOnlyStyle" TabIndex="-1" />
                                        <span id="SpanSiireSyoriZumi" style="color: red; font-weight: bold;" runat="server">
                                        </span>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="koumokuMei">
                                加盟店</td>
                            <td colspan="5">
                                <asp:UpdatePanel ID="UpdatePanelKameiten" UpdateMode="Conditional" runat="server"
                                    RenderMode="Inline">
                                    <ContentTemplate>
                                        <asp:TextBox ID="TextKameitenCd" runat="server" MaxLength="5" Style="width: 45px;"
                                            CssClass="codeNumber" OnTextChanged="TextKameitenCd_TextChanged" />
                                        <input id="ButtonSearchKameiten" runat="server" type="button" value="検索" class="gyoumuSearchBtn"
                                            onserverclick="ButtonSearchKameiten_ServerClick" />
                                        <asp:TextBox ID="TextKameitenMei" Style="width: 300px;" runat="server" MaxLength="40"
                                            ReadOnly="true" CssClass="readOnlyStyle" TabIndex="-1" />
                                        <input type="hidden" id="HiddenKameitenSearchType" runat="server" />
                                        <input type="hidden" id="HiddenKbn" runat="server" />
                                        <input type="hidden" id="HiddenKamentenCdOld" runat="server" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td class="koumokuMei">
                                取消</td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel_KameiToikesi" UpdateMode="Conditional" runat="server"
                                    RenderMode="Inline">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ButtonSearchKameiten" />
                                        <asp:AsyncPostBackTrigger ControlID="TextKameitenCd" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:TextBox ID="TextTorikesiRiyuu" runat="server" Width="80px" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="koumokuMei">
                                調査会社</td>
                            <td colspan="7">
                                <asp:UpdatePanel ID="UpdatePanelTysKaisya" UpdateMode="Conditional" runat="server"
                                    RenderMode="Inline">
                                    <ContentTemplate>
                                        <asp:TextBox ID="TextTysKaisyaCd" runat="server" MaxLength="7" Style="width: 45px;"
                                            CssClass="codeNumber hissu" OnTextChanged="TextTysKaisyaCd_TextChanged" />
                                        <input id="ButtonSearchTysKaisya" runat="server" type="button" value="検索" class="gyoumuSearchBtn"
                                            onserverclick="ButtonSearchTysKaisya_ServerClick" />
                                        <asp:TextBox ID="TextTysKaisyaMei" Style="width: 300px;" runat="server" MaxLength="40"
                                            ReadOnly="true" CssClass="readOnlyStyle" TabIndex="-1" />
                                        <input type="hidden" id="HiddenTysKaisyaCdOld" runat="server" />
                                        <input type="hidden" id="HiddenKamentenCd" runat="server" />
                                        <input type="hidden" id="HiddenTysKaisyaSearchType" runat="server" />
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
                            <td class="koumokuMei">
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
                                税込仕入金額</td>
                            <td colspan="2">
                                <asp:TextBox ID="TextUriGaku" runat="server" Style="width: 120px" CssClass="kingaku readOnlyStyle"
                                    ReadOnly="true" TabIndex="-1" />
                            </td>
                            <td class="koumokuMei">
                                消費税額</td>
                            <td colspan="4">
                                <asp:TextBox ID="TextSyouhiZeiGaku" runat="server" Style="width: 100px" CssClass="kingaku hissu"
                                    MaxLength="10" OnTextChanged="TextSyouhiZeiGaku_TextChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td class="koumokuMei">
                                仕入年月日</td>
                            <td colspan="2">
                                <asp:TextBox ID="TextSiireDate" runat="server" Style="width: 70px" CssClass="date hissu"
                                 MaxLength="10" OnTextChanged="TextSiireDate_TextChanged" AutoPostBack="False" />
                            </td>
                            <td class="koumokuMei">
                                伝票仕入年月日</td>
                            <td>
                                <asp:TextBox ID="TextDenpyouSiireDateDisplay" runat="server" CssClass="date readOnlyStyle2"
                                    ReadOnly="True" TabIndex="-1" Width="70px"></asp:TextBox>
                            </td>
                            <td class="koumokuMei">
                                伝票仕入年月日修正</td>
                            <td colspan="2">
                                <asp:UpdatePanel ID="UpdatePanelDenpyouSiireDateSyuusei" UpdateMode="Conditional" runat="server" RenderMode="Inline">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="TextSiireDate" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:TextBox ID="TextDenpyouSiireDate" runat="server" CssClass="date" MaxLength="10"
                                            BackColor="#ddaaee" Width="70px" AutoPostBack="False"></asp:TextBox>
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
                                    rows="5" style="ime-mode: active; font-family: Sans-Serif"></textarea>
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

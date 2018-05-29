<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master"
    Codebehind="SeikyuuSakiMaster.aspx.vb" Inherits="Itis.Earth.WebUI.SeikyuuSakiMaster"
    Title="請求先マスタメンテナンス" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js"></script>

    <script type="text/javascript">
    //window名付与
    var objWin = window;
    objWin.name = "earthMainWindow"
    initPage(); //画面初期設定  
    </script>

    <script language="javascript" for="document" event="onkeydown"> 
  if(event.keyCode==13 && event.srcElement.type!="button" && event.srcElement.type!="submit" && event.srcElement.type!="reset" && event.srcElement.type!="textarea" && event.srcElement.type!="")
     event.keyCode=9; 
    </script>

    <asp:UpdatePanel ID="UpdatePanelA" runat="server" UpdateMode="Conditional" RenderMode="Inline">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSearchSeikyuuSaki" />
            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
            <asp:AsyncPostBackTrigger ControlID="btnClear" />
            <asp:AsyncPostBackTrigger ControlID="btnSyuusei" />
            <asp:AsyncPostBackTrigger ControlID="btnTouroku" />
            <asp:AsyncPostBackTrigger ControlID="btnClearMeisai" />
            <asp:AsyncPostBackTrigger ControlID="btnKensakuSeikyuuSaki" />
            <asp:AsyncPostBackTrigger ControlID="btnSkkJigyousyo" />
            <asp:AsyncPostBackTrigger ControlID="btnKihonJyouhouSet" />
            <asp:AsyncPostBackTrigger ControlID="btnKensakuYuubinNo" />
            <asp:AsyncPostBackTrigger ControlID="btnKensakuNayose" />
            <asp:AsyncPostBackTrigger ControlID="btnOK" />
            <asp:PostBackTrigger ControlID="btnBack" />
        </Triggers>
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="2" class="titleTable" style="width: 960px;
                text-align: left">
                <tbody>
                    <tr>
                        <th>
                            請求先マスタメンテナンス &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnBack" runat="server"
                                CssClass="kyoutuubutton" />
                        </th>
                        <th style="text-align: right">
                        </th>
                    </tr>
                    <tr>
                        <td colspan="2" rowspan="1" style="height: 13px">
                            <asp:Label ID="lblTyousaKoujiKbn" runat="server" Font-Bold="true" ForeColor="red"></asp:Label>                        
                        </td>
                    </tr>
                </tbody>
            </table>
            <table cellpadding="2" class="mainTable" style="text-align: left; width: 596px;">
                <tbody>
                    <tr>
                        <td class="koumokuMei" style="width: 80px">
                            請求先&nbsp;</td>
                        <td>
                            <asp:DropDownList ID="ddlSeikyuuSaki_Kbn" runat="server" Width="100px" CssClass="hissu">
                            </asp:DropDownList>&nbsp;&nbsp;
                            <asp:TextBox ID="tbxSeikyuuSaki_Cd" runat="server" CssClass="hissu" MaxLength="5"
                                Style="ime-mode: disabled;" Width="50px"></asp:TextBox>&nbsp;&nbsp;
                            <asp:TextBox ID="tbxSeikyuuSaki_Brc" runat="server" CssClass="hissu" MaxLength="2"
                                Style="ime-mode: disabled;" Width="30px"></asp:TextBox>&nbsp;&nbsp;
                            <asp:Button ID="btnSearchSeikyuuSaki" runat="server" Text="検索" />
                            <asp:TextBox ID="tbxSeikyuuSaki_Mei" runat="server" CssClass="readOnlyStyle" TabIndex="-1"
                                Width="256px"></asp:TextBox></td>
                    </tr>
                    <tr align="left">
                        <td colspan="2" rowspan="1">
                            <asp:Button ID="btnSearch" runat="server" Text="選択 & 編集" />
                            <asp:Button ID="btnClear" runat="server" Text="クリア" />
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <table cellpadding="2" class="mainTable" style="text-align: left;">
                <tbody>
                    <tr align="left">
                        <td colspan="4">
                            <asp:Button ID="btnSyuusei" runat="server" Text="修正実行" Width="90px" />&nbsp; &nbsp;
                            <asp:Button ID="btnTouroku" runat="server" Text="新規登録" Width="90px" />
                            &nbsp; &nbsp;<asp:Button ID="btnClearMeisai" runat="server" Text="明細クリア" Width="90px" />
                            <asp:Button ID="btnTyuuijikou" runat="server" Text="注意情報" Width="90px" Style="text-align: center;
                                margin-left: 60px;" />
                        </td>
                        <td class="koumokuMei">
                            取消</td>
                        <td>
                            <asp:CheckBox ID="chkTorikesi" runat="server" /></td>
                    </tr>
                    <tr align="left">
                        <td class="koumokuMei">
                            請求先区分</td>
                        <td>
                            <asp:DropDownList ID="ddlSeikyuuSakiKbn" runat="server" Width="100px" CssClass="hissu">
                            </asp:DropDownList></td>
                        <td class="koumokuMei">
                            請求先</td>
                        <td colspan="4">
                            <asp:TextBox ID="tbxSeikyuuSakiCd" runat="server" CssClass="hissu" MaxLength="5"
                                Style="ime-mode: disabled;" Width="50px"></asp:TextBox>&nbsp;&nbsp;
                            <asp:TextBox ID="tbxSeikyuuSakiBrc" runat="server" CssClass="hissu" MaxLength="2"
                                Style="ime-mode: disabled;" Width="30px"></asp:TextBox>&nbsp;&nbsp;
                            <asp:Button ID="btnKensakuSeikyuuSaki" runat="server" Text="検索" />
                            <asp:TextBox ID="tbxSeikyuuSakiMei" runat="server" CssClass="readOnlyStyle" TabIndex="-1"
                                Width="256px"></asp:TextBox></td>
                    </tr>
                    <tr align="left">
                        <td class="koumokuMei1">
                            名寄先コード</td>
                        <td>
                            <asp:TextBox ID="tbxNayoseCd" runat="server" MaxLength="5" Style="ime-mode: disabled;"
                                Width="80px"></asp:TextBox>
                            &nbsp;<asp:Button ID="btnKensakuNayose" runat="server" Text="検索" /></td>
                        <td class="koumokuMei1">
                            名寄先名</td>
                        <td colspan="4" style="padding-bottom: 0px;">
                            <asp:TextBox ID="tbxNayoseMei" runat="server" CssClass="readOnlyStyle" TabIndex="-1"
                                Width="415px"></asp:TextBox></td>
                    </tr>
                    <tr align="left" style="height: 0px;">
                        <td style="width: 100px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            clip: rect(-1px auto auto auto); border-top-style: none;">
                        </td>
                        <td style="width: 135px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none;">
                        </td>
                        <td style="width: 100px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none;">
                        </td>
                        <td style="width: 272px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none; border-right-style: none;">
                        </td>
                        <td style="width: 72px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none; border-right-style: none; border-left-style: none;">
                        </td>
                        <td style="width: 100px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none; border-left-style: none;">
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <table cellpadding="2" class="mainTable" style="text-align: left;">
                <tbody>
                    <tr align="left">
                        <td class="koumokuMei">
                            郵便番号</td>
                        <td colspan="3">
                            <asp:TextBox ID="tbxYuubinNo" runat="server" MaxLength="8" Style="ime-mode: disabled;"
                                Width="80px"></asp:TextBox>
                            &nbsp;<asp:Button ID="btnKensakuYuubinNo" runat="server" Text="検索" /></td>
                    </tr>
                    <tr align="left">
                        <td class="koumokuMei">
                            住所１</td>
                        <td>
                            <asp:TextBox ID="tbxJyuusyo1" runat="server" MaxLength="40" Width="380px"></asp:TextBox></td>
                        <td class="koumokuMei">
                            電話番号</td>
                        <td>
                            <asp:TextBox ID="tbxTelNo" runat="server" MaxLength="16" Style="ime-mode: disabled;"
                                Width="130px"></asp:TextBox></td>
                    </tr>
                    <tr align="left">
                        <td class="koumokuMei">
                            住所２</td>
                        <td>
                            <asp:TextBox ID="tbxJyuusyo2" runat="server" MaxLength="30" Width="380px"></asp:TextBox></td>
                        <td class="koumokuMei">
                            FAX番号</td>
                        <td>
                            <asp:TextBox ID="tbxFaxNo" runat="server" MaxLength="16" Style="ime-mode: disabled;"
                                Width="130px"></asp:TextBox></td>
                    </tr>
                    <tr align="left" style="height: 0px;">
                        <td style="width: 100px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            clip: rect(-1px auto auto auto); border-top-style: none;">
                        </td>
                        <td style="width: 395px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none;">
                        </td>
                        <td style="width: 130px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none;">
                        </td>
                        <td style="width: 166px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none; border-right-style: none;">
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <table border="0" cellpadding="0" cellspacing="0" style="width: 822px;">
                <tr>
                    <td>
                        <table cellpadding="0px" cellspacing="0px">
                            <tr>
                                <td style="height: 24px">
                                    <asp:DropDownList ID="ddlSeikyuuSakiTourokuHinagata" runat="server" Width="150px">
                                    </asp:DropDownList>&nbsp;&nbsp;
                                    <asp:Button ID="btnKihonJyouhouSet" runat="server" Text="基本情報セット" /></td>
                            </tr>
                        </table>
                        <br />
                    </td>
                    <td style="text-align: right; vertical-align: bottom;">
                        <table cellpadding="2" class="mainTable" style="width: 387px; text-align: left; margin-bottom: -2px;">
                            <tr>
                                <td class="koumokuMei" style="width: 171px;">
                                    統合会計得意先ｺｰﾄﾞ
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxTougouKaikeiTokusakiCd" runat="server" CssClass="codeNumber"
                                        Width="100px" MaxLength="10"></asp:TextBox>
                                    <asp:Button ID="btnSaiban" runat="server" Width="40px" Text="採番" Style="margin-left: 10px;" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table cellpadding="2" class="mainTable" style="text-align: left;">
                <tbody>
                    <tr align="left">
                        <td class="koumokuMei">
                            担当者</td>
                        <td colspan="3">
                            <asp:TextBox ID="tbxTantousya" runat="server" MaxLength="40" Width="256px"></asp:TextBox></td>
                        <td class="koumokuMei">
                            請求書印字物件名</td>
                        <td>
                            <asp:DropDownList ID="ddlSeikyuuSyoInjiBukkenMei" runat="server" Width="150px">
                            </asp:DropDownList></td>
                    </tr>
                    <tr align="left">
                        <td class="koumokuMei">
                            新会計事業所</td>
                        <td colspan="5">
                            <asp:TextBox ID="tbxSkkJigyousyoCd" runat="server" MaxLength="10" Style="ime-mode: disabled;"
                                Width="80px"></asp:TextBox>&nbsp;&nbsp;
                            <asp:Button ID="btnSkkJigyousyo" runat="server" Text="検索" />&nbsp;&nbsp;
                            <asp:TextBox ID="tbxSkkJigyousyoMei" runat="server" CssClass="readOnlyStyle" TabIndex="-1"
                                Width="256px"></asp:TextBox>&nbsp;&nbsp; ※グループ会社以外は空白</td>
                    </tr>
                    <tr align="left" style="height: 0px;">
                        <td style="width: 100px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            clip: rect(-1px auto auto auto); border-top-style: none;">
                        </td>
                        <td style="width: 135px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none;">
                        </td>
                        <td style="width: 100px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none;">
                        </td>
                        <td style="width: 72px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none; border-right-style: none;">
                        </td>
                        <td style="width: 172px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none; border-right-style: none; border-left-style: none;">
                        </td>
                        <td style="width: 200px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none; border-left-style: none;">
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <table cellpadding="2" class="mainTable" style="text-align: left;">
                <tbody>
                    <tr align="left">
                        <td class="koumokuMei">
                            請求締め日</td>
                        <td>
                            <asp:TextBox ID="tbxSeikyuuSimeDate" runat="server" MaxLength="2" Style="ime-mode: disabled;"
                                Width="30px"></asp:TextBox></td>
                        <td class="koumokuMei">
                            先方請求締め日</td>
                        <td>
                            <asp:TextBox ID="tbxSenpouSeikyuuSimeDate" runat="server" MaxLength="2" Style="ime-mode: disabled;"
                                Width="30px"></asp:TextBox></td>
                        <td class="koumokuMei">
                            請求書必着日</td>
                        <td>
                            <asp:TextBox ID="tbxSeikyuusyoHittykDate" runat="server" MaxLength="2" Style="ime-mode: disabled;"
                                Width="30px"></asp:TextBox></td>
                        <td class="koumokuMei">
                            決算時二度締め</td>
                        <td>
                            <asp:DropDownList ID="ddlKessanjiNidosimeFlg" runat="server" Width="80px">
                                <asp:ListItem Value="0" Text="行う" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="1" Text="行わない"></asp:ListItem>
                            </asp:DropDownList>
                    </tr>
                    <tr align="left">
                        <td class="koumokuMei">
                            回収予定月数</td>
                        <td>
                            <asp:TextBox ID="tbxKaisyuuYoteiGessuu" runat="server" MaxLength="2" Style="ime-mode: disabled;"
                                Width="30px"></asp:TextBox></td>
                        <td class="koumokuMei">
                            回収予定日</td>
                        <td>
                            <asp:TextBox ID="tbxKaisyuuYoteiDate" runat="server" MaxLength="2" Style="ime-mode: disabled;"
                                Width="30px"></asp:TextBox></td>
                        <td class="koumokuMei">
                            銀行支店コード
                        </td>
                        <td >
                            <asp:DropDownList ID="ddlGinkouSitenCd" runat="server" Width="150px">
                            </asp:DropDownList>
                        </td>
                        <td class="koumokuMei">
                            入金口座番号</td>
                        <td>
                            <asp:TextBox ID="tbxNyuukinKouzaNo" runat="server" MaxLength="10" Style="ime-mode: disabled;"
                                Width="80px"></asp:TextBox></td>
                    </tr>
                    <tr align="left">
                        <td class="koumokuMei">
                            直工事請求タイミング
                        </td>
                        <td colspan="3">
                            <asp:DropDownList ID="ddlTykKojSeikyuuTimingFlg" runat="server" Width="220px">
                            </asp:DropDownList>
                        </td>
                        <td class="koumokuMei">
                            口振ＯＫフラグ
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlKutiburiOkFlg" runat="server" Width="106px">
                            </asp:DropDownList>
                        </td>
                        <td class="koumokuMei">
                            相殺フラグ</td>
                        <td>
                            <asp:CheckBox ID="chkSousaiFlg" runat="server" />
                        </td>
                    </tr>
                    <tr align="left">
                        <td class="koumokuMei">
                            安全協力会費
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="tbxAnzenKyouryokuKaihi1" runat="server" CssClass="codeNumber" Width="90px"
                                MaxLength="13" Style="text-align: right;"></asp:TextBox>
                            <asp:Label ID="lblEn" runat="server" Text="円"></asp:Label>
                            <asp:TextBox ID="tbxAnzenKyouryokuKaihi2" runat="server" CssClass="codeNumber" Width="70px"
                                MaxLength="7" Style="text-align: right; margin-left: 15px;"></asp:TextBox>
                            <asp:Label ID="lblRitu" runat="server" Text="％"></asp:Label>
                        </td>
                        <td class="koumokuMei">
                            備考
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="tbxBikou" runat="server" Width="310px" MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>
                    <tr align="left" style="height: 0px;">
                        <td style="width: 128px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            clip: rect(-1px auto auto auto); border-top-style: none;">
                        </td>
                        <td style="width: 40px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none;">
                        </td>
                        <td style="width: 100px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none;">
                        </td>
                        <td style="width: 40px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none;">
                        </td>
                        <td style="width: 90px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none;">
                        </td>
                        <td style="width: 120px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none; border-right-style: none;">
                        </td>
                        <td style="width: 100px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none; border-right-style: none; border-left-style: none;">
                        </td>
                        <td style="width: 85px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none; border-left-style: none;">
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <table cellpadding="2" class="mainTable" style="text-align: left;">
                <tbody>
                    <tr align="left">
                        <td class="koumokuMei">
                            回収1手形サイト月数</td>
                        <td>
                            <asp:TextBox ID="tbxKaisyuu1TegataSiteGessuu" runat="server" MaxLength="2" Style="ime-mode: disabled;"
                                Width="30px"></asp:TextBox></td>
                        <td class="koumokuMei">
                            回収1手形サイト日</td>
                        <td>
                            <asp:TextBox ID="tbxKaisyuu1TegataSiteDate" runat="server" MaxLength="2" Style="ime-mode: disabled;"
                                Width="30px"></asp:TextBox></td>
                        <td class="koumokuMei">
                            回収1請求書用紙</td>
                        <td>
                            <asp:DropDownList ID="ddlKaisyuu1SeikyuusyoYousi" runat="server" Width="180px">
                            </asp:DropDownList></td>
                    </tr>
                    <tr align="left">
                        <td class="koumokuMei">
                            回収1種別1</td>
                        <td>
                            <asp:DropDownList ID="ddlKaisyuu1Syubetu1" runat="server" Width="107px">
                            </asp:DropDownList></td>
                        <td class="koumokuMei">
                            回収1割合1</td>
                        <td colspan="3">
                            <asp:TextBox ID="tbxKaisyuu1Wariai1" runat="server" CssClass="number" MaxLength="3"
                                Style="ime-mode: disabled;" Width="30px"></asp:TextBox>
                            &nbsp;％</td>
                    </tr>
                    <tr align="left">
                        <td class="koumokuMei">
                            回収1種別2</td>
                        <td>
                            <asp:DropDownList ID="ddlKaisyuu1Syubetu2" runat="server" Width="107px">
                            </asp:DropDownList></td>
                        <td class="koumokuMei">
                            回収1割合2</td>
                        <td colspan="3">
                            <asp:TextBox ID="tbxKaisyuu1Wariai2" runat="server" CssClass="number" MaxLength="3"
                                Style="ime-mode: disabled;" Width="30px"></asp:TextBox>
                            &nbsp;％</td>
                    </tr>
                    <tr align="left">
                        <td class="koumokuMei">
                            回収1種別3</td>
                        <td>
                            <asp:DropDownList ID="ddlKaisyuu1Syubetu3" runat="server" Width="107px">
                            </asp:DropDownList></td>
                        <td class="koumokuMei">
                            回収1割合3</td>
                        <td colspan="3">
                            <asp:TextBox ID="tbxKaisyuu1Wariai3" runat="server" CssClass="number" MaxLength="3"
                                Style="ime-mode: disabled;" Width="30px"></asp:TextBox>
                            &nbsp;％</td>
                    </tr>
                    <tr align="left" style="height: 0px;">
                        <td style="width: 160px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            clip: rect(-1px auto auto auto); border-top-style: none;">
                        </td>
                        <td style="width: 120px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none;">
                        </td>
                        <td style="width: 140px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none;">
                        </td>
                        <td style="width: 60px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none; border-right-style: none;">
                        </td>
                        <td style="width: 115px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none; border-right-style: none; border-left-style: none;">
                        </td>
                        <td style="width: 185px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none; border-left-style: none;">
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <table cellpadding="2" class="mainTable" style="text-align: left;">
                <tbody>
                    <tr align="left">
                        <td class="koumokuMei">
                            回収境界額</td>
                        <td>
                            <asp:TextBox ID="tbxKaisyuuKyoukaigaku" CssClass="kingaku" runat="server" MaxLength="7"
                                Width="100px"></asp:TextBox></td>
                        <tr align="left" style="height: 0px;">
                            <td style="width: 160px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                                clip: rect(-1px auto auto auto); border-top-style: none;">
                            </td>
                            <td style="width: 120px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                                border-top-style: none;">
                            </td>
                        </tr>
                </tbody>
            </table>
            <br />
            <table cellpadding="2" class="mainTable" style="text-align: left;">
                <tbody>
                    <tr align="left">
                        <td class="koumokuMei">
                            回収2手形サイト月数</td>
                        <td>
                            <asp:TextBox ID="tbxKaisyuu2TegataSiteGessuu" runat="server" MaxLength="2" Style="ime-mode: disabled;"
                                Width="30px"></asp:TextBox></td>
                        <td class="koumokuMei">
                            回収2手形サイト日</td>
                        <td>
                            <asp:TextBox ID="tbxKaisyuu2TegataSiteDate" runat="server" MaxLength="2" Style="ime-mode: disabled;"
                                Width="30px"></asp:TextBox></td>
                        <td class="koumokuMei">
                            回収2請求書用紙</td>
                        <td>
                            <asp:DropDownList ID="ddlKaisyuu2SeikyuusyoYousi" runat="server" Width="180px">
                            </asp:DropDownList></td>
                    </tr>
                    <tr align="left">
                        <td class="koumokuMei">
                            回収2種別1</td>
                        <td>
                            <asp:DropDownList ID="ddlKaisyuu2Syubetu1" runat="server" Width="107px">
                            </asp:DropDownList></td>
                        <td class="koumokuMei">
                            回収2割合1</td>
                        <td colspan="3">
                            <asp:TextBox ID="tbxKaisyuu2Wariai1" runat="server" CssClass="number" MaxLength="3"
                                Style="ime-mode: disabled;" Width="30px"></asp:TextBox>
                            &nbsp;％</td>
                    </tr>
                    <tr align="left">
                        <td class="koumokuMei">
                            回収2種別2</td>
                        <td>
                            <asp:DropDownList ID="ddlKaisyuu2Syubetu2" runat="server" Width="107px">
                            </asp:DropDownList></td>
                        <td class="koumokuMei">
                            回収2割合2</td>
                        <td colspan="3">
                            <asp:TextBox ID="tbxKaisyuu2Wariai2" runat="server" CssClass="number" MaxLength="3"
                                Style="ime-mode: disabled;" Width="30px"></asp:TextBox>
                            &nbsp;％</td>
                    </tr>
                    <tr align="left">
                        <td class="koumokuMei">
                            回収2種別3</td>
                        <td>
                            <asp:DropDownList ID="ddlKaisyuu2Syubetu3" runat="server" Width="107px">
                            </asp:DropDownList></td>
                        <td class="koumokuMei">
                            回収2割合3</td>
                        <td colspan="3">
                            <asp:TextBox ID="tbxKaisyuu2Wariai3" runat="server" CssClass="number" MaxLength="3"
                                Style="ime-mode: disabled;" Width="30px"></asp:TextBox>
                            &nbsp;％</td>
                    </tr>
                    <tr align="left" style="height: 0px;">
                        <td style="width: 160px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            clip: rect(-1px auto auto auto); border-top-style: none;">
                        </td>
                        <td style="width: 120px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none;">
                        </td>
                        <td style="width: 140px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none;">
                        </td>
                        <td style="width: 60px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none; border-right-style: none;">
                        </td>
                        <td style="width: 115px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none; border-right-style: none; border-left-style: none;">
                        </td>
                        <td style="width: 185px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none; border-left-style: none;">
                        </td>
                    </tr>
                </tbody>
            </table>
            <asp:HiddenField ID="hidUPDTime" runat="server" />
            <asp:HiddenField ID="hidConfirm" runat="server" />
            <asp:HiddenField ID="hidSeikyuuKbn" runat="server" />
            <asp:Button ID="btnOK" Text="button" Style="display: none;" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

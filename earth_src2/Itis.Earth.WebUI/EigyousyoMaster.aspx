<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master"
    Codebehind="EigyousyoMaster.aspx.vb" Inherits="Itis.Earth.WebUI.EigyousyoMaster"
    Title="営業所マスタメンテナンス" %>

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
            <asp:AsyncPostBackTrigger ControlID="btnSearchEigyousyo" />
            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
            <asp:AsyncPostBackTrigger ControlID="btnClear" />
            <asp:AsyncPostBackTrigger ControlID="btnSyuusei" />
            <asp:AsyncPostBackTrigger ControlID="btnTouroku" />
            <asp:AsyncPostBackTrigger ControlID="btnClearMeisai" />
            <asp:AsyncPostBackTrigger ControlID="btnFcTenKousin" />
            <asp:AsyncPostBackTrigger ControlID="btnKensakuYuubinNo" />
            <asp:AsyncPostBackTrigger ControlID="btnKensakuSeikyuuSaki" />
            <asp:AsyncPostBackTrigger ControlID="btnKensakuSeikyuuSyousai" />
            <%--<asp:AsyncPostBackTrigger ControlID ="btnKensakuSeikyuuSoufuCopy" />
        <asp:AsyncPostBackTrigger ControlID ="btnKensakuSeikyuuSyo" />--%>
            <asp:AsyncPostBackTrigger ControlID="btnOK" />
            <asp:AsyncPostBackTrigger ControlID="btnNO" />
            <asp:AsyncPostBackTrigger ControlID="btnFC" />
            <asp:PostBackTrigger ControlID="btnBack" />
        </Triggers>
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="2" class="titleTable" style="width: 960px;
                text-align: left">
                <tbody>
                    <tr>
                        <th>
                            営業所マスタメンテナンス&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnBack" runat="server"
                                CssClass="kyoutuubutton" Text="戻る" />
                        </th>
                        <th style="text-align: right">
                        </th>
                    </tr>
                    <tr>
                        <td colspan="2" rowspan="1" style="height: 13px">
                        </td>
                    </tr>
                </tbody>
            </table>
            <table cellpadding="2" class="mainTable" style="text-align: left; width: 496px;">
                <tbody>
                    <tr>
                        <td class="koumokuMei" style="width: 90px">
                            営業所&nbsp;</td>
                        <td>
                            <asp:TextBox ID="tbxEigyousyo_Cd" runat="server" MaxLength="5" CssClass="hissu codeNumber"
                                Width="64px"></asp:TextBox>&nbsp; &nbsp;
                            <asp:Button ID="btnSearchEigyousyo" runat="server" Text="検索" />
                            <asp:TextBox ID="tbxEigyousyo_Mei" runat="server" CssClass="readOnlyStyle" TabIndex="-1"
                                MaxLength="40" Width="256px"></asp:TextBox></td>
                    </tr>
                    <tr align="left">
                        <td colspan="2" rowspan="1">
                            <asp:Button ID="btnSearch" runat="server" Text="選択 & 編集" />
                            <asp:Button ID="btnClear" runat="server" Text="クリア" />
                            <asp:Button ID="btnCsv" runat="server" Width="100px" Text="CSV出力" Style="margin-left: 90px;" />
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <table cellpadding="2" class="mainTable" style="text-align: left;">
                <tbody>
                    <tr align="left">
                        <td colspan="9">
                            <asp:Button ID="btnSyuusei" runat="server" Text="修正実行" Width="90px" />&nbsp; &nbsp;
                            <asp:Button ID="btnTouroku" runat="server" Text="新規登録" Width="90px" />
                            &nbsp; &nbsp;<asp:Button ID="btnClearMeisai" runat="server" Text="明細クリア" Width="90px" />
                            &nbsp; &nbsp;<asp:Button ID="btnFcTenKousin" runat="server" Text="FC店住所自動更新" Width="120px" /></td>
                        <td colspan="2" class="koumokuMei">
                            取消</td>
                        <td>
                            <asp:CheckBox ID="chkTorikesi" runat="server" /></td>
                    </tr>
                    <tr align="left">
                        <td class="koumokuMei">
                            営業所コード</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbxEigyousyoCd" runat="server" CssClass="hissu codeNumber" MaxLength="5"
                                Width="64px"></asp:TextBox></td>
                        <td colspan="2" class="koumokuMei">
                            営業所名印字有無</td>
                        <td colspan="2">
                            <asp:DropDownList ID="ddlEigyousyoMeiInjiUmu" runat="server" CssClass="hissu" Width="80px">
                            </asp:DropDownList></td>
                        <td colspan="3" class="koumokuMei">
                            集計FC用コード
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="tbxSyuukeiFcCd" runat="server" CssClass=" codeNumber" Width="64px"
                                MaxLength="5"></asp:TextBox>
                        </td>
                    </tr>
                    <tr align="left">
                        <td class="koumokuMei">
                            営業所名</td>
                        <td colspan="4">
                            <asp:TextBox ID="tbxEigyousyoMei" runat="server" CssClass="hissu" MaxLength="40"
                                Width="250px"></asp:TextBox></td>
                        <td colspan="2" class="koumokuMei">
                            営業所カナ</td>
                        <td colspan="5">
                            <asp:TextBox ID="tbxEigyousyoMeiKana" MaxLength="20" runat="server" Width="250px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="koumokuMei">
                            エリア
                        </td>
                        <td colspan="4">
                            <asp:DropDownList ID="ddlArea" Width="256px" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td colspan="2" class="koumokuMei">
                            ブロック
                        </td>
                        <td colspan="5">
                            <asp:DropDownList ID="ddlBlock" Width="256px" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="koumokuMei">
                            FC店区分
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="ddlFcTenKbn" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td colspan="2" class="koumokuMei">
                            FC入会年月
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="tbxFcNyuukaiYM" runat="server" CssClass="codeNumber" Width="80px"
                                MaxLength="7"></asp:TextBox>
                        </td>
                        <td colspan="2" class="koumokuMei">
                            FC退会年月
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="tbxFcTaikaiYM" runat="server" CssClass="codeNumber" Width="80px"
                                MaxLength="7"></asp:TextBox>
                        </td>
                    </tr>
                    <tr align="left" style="height: 0px;">
                        <td style="width: 100px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            clip: rect(-1px auto auto auto); border-top-style: none;">
                        </td>
                        <td style="width: 30px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none;">
                        </td>
                        <td style="width: 70px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none;">
                        </td>
                        <td style="width: 50px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none;">
                        </td>
                        <td style="width: 100px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none;">
                        </td>
                        <td style="width: 10px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none; border-right-style: none;">
                        </td>
                        <td style="width: 90px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none; border-right-style: none;">
                        </td>
                        <td style="width: 20px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none; border-right-style: none; border-left-style: none;">
                        </td>
                        <td style="width: 60px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none; border-right-style: none; border-left-style: none;">
                        </td>
                        <td style="width: 20px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none; border-left-style: none;">
                        </td>
                        <td style="width: 30px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none; border-left-style: none;">
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
                    <tr align="left" style="height: 25px;">
                        <td colspan="7" class="koumokuMei" style="color: Red; font-weight: bold;">
                            調査会社選択
                        </td>
                    </tr>
                    <tr align="left">
                        <td class="koumokuMei">
                            調査会社
                        </td>
                        <td colspan="6">
                            <asp:TextBox ID="tbxTyousaKaisyaCd" runat="server" CssClass="hissu codeNumber" Width="64px"
                                MaxLength="6"></asp:TextBox>
                            <asp:Button ID="btnTyousaKaisyaCd" runat="server" Text="検索" Style="margin-left: 50px;" />
                            <asp:Label ID="lblFcTenSentaku1" runat="server" Text="※FC店選択の場合、調査会社選択が必須となります" Style="color: Blue;
                                margin-left: 15px;"></asp:Label>
                            <asp:Label ID="lblFcTenSentaku2" runat="server" Text="※ 調査会社情報は本画面で編集できません" Style="color: Blue;
                                margin-left: 15px;"></asp:Label>
                            <asp:Button ID="btnTyousakaisya" runat="server" Style="display: none;" />
                        </td>
                    </tr>
                    <tr align="left">
                        <td class="koumokuMei">
                            調査会社名
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="tbxTyousaKaisyaMei" runat="server" Width="210px" TabIndex="-1"></asp:TextBox>
                        </td>
                        <td class="koumokuMei">
                            調査会社名カナ
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="tbxTyousaKaisyaMeiKana" runat="server" Width="210px" TabIndex="-1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr align="left">
                        <td class="koumokuMei">
                            代表者名
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="tbxDaihyousyaMei" runat="server" Width="210px" TabIndex="-1"></asp:TextBox>
                        </td>
                        <td class="koumokuMei">
                            役職名
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="tbxYasyokuMei" runat="server" Width="210px" TabIndex="-1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr align="left">
                        <td colspan="2" class="koumokuMei">
                            郵便番号
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="tbxTyousakaisyaYuubinNo" runat="server" TabIndex="-1" CssClass="codeNumber"
                                Width="80px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr align="left">
                        <td colspan="2" class="koumokuMei">
                            住所１</td>
                        <td colspan="3">
                            <asp:TextBox ID="tbxTyousakaisyaJyuusyo1" runat="server" TabIndex="-1" Width="380px"></asp:TextBox></td>
                        <td class="koumokuMei">
                            電話番号</td>
                        <td>
                            <asp:TextBox ID="tbxTyousakaisyaTelNo" runat="server" TabIndex="-1" CssClass="codeNumber"
                                Width="130px"></asp:TextBox></td>
                    </tr>
                    <tr align="left">
                        <td colspan="2" class="koumokuMei">
                            住所２</td>
                        <td colspan="3">
                            <asp:TextBox ID="tbxTyousakaisyaJyuusyo2" runat="server" TabIndex="-1" Width="380px"></asp:TextBox></td>
                        <td class="koumokuMei">
                            FAX番号</td>
                        <td>
                            <asp:TextBox ID="tbxTyousakaisyaFaxNo" runat="server" TabIndex="-1" CssClass="codeNumber"
                                Width="130px"></asp:TextBox></td>
                    </tr>
                    <tr align="left" style="height: 0px;">
                        <td style="width: 120px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            clip: rect(-1px auto auto auto); border-top-style: none;">
                        </td>
                        <td style="width: 23px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none;">
                        </td>
                        <td style="width: 195px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none;">
                        </td>
                        <td style="width: 120px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none;">
                        </td>
                        <td style="width: 66px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none;">
                        </td>
                        <td style="width: 130px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none;">
                        </td>
                        <td style="width: 190px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none; border-right-style: none;">
                        </td>
                    </tr>
                </tbody>
            </table>
            <table cellpadding="2" class="mainTable" style="text-align: left; margin-top: -3px;
                padding-top: 0px; border-top-style: none; clip: rect(-1px auto auto auto); border-top-style: none;">
                <tr align="left">
                    <td class="koumokuMei">
                        JAPAN会区分
                    </td>
                    <td>
                        <asp:TextBox ID="tbxJapanKaiKbn" runat="server" Width="100px" TabIndex="-1"></asp:TextBox>
                    </td>
                    <td class="koumokuMei">
                        JAPAN会入会年月
                    </td>
                    <td>
                        <asp:TextBox ID="tbxJapanKaiNyuukaiYM" runat="server" Width="80px" TabIndex="-1"></asp:TextBox>
                    </td>
                    <td class="koumokuMei">
                        JAPAN会退会年月
                    </td>
                    <td>
                        <asp:TextBox ID="tbxJapanKaiTaikaiYM" runat="server" Width="80px" TabIndex="-1"></asp:TextBox>
                    </td>
                </tr>
                <tr align="left">
                    <td class="koumokuMei">
                        宅地地盤調査主任資格
                    </td>
                    <td>
                        <asp:TextBox ID="tbxTyousaSyuninSikaku" runat="server" Width="100px" TabIndex="-1"></asp:TextBox>
                    </td>
                    <td class="koumokuMei">
                        ReportJHSトークン
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="tbxReportJHS" runat="server" Width="80px" TabIndex="-1"></asp:TextBox>
                    </td>
                </tr>
                <tr align="left" style="height: 0px;">
                    <td style="width: 150px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                        clip: rect(-1px auto auto auto); border-top-style: none;">
                    </td>
                    <td style="width: 120px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                        border-top-style: none;">
                    </td>
                    <td style="width: 150px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                        border-top-style: none;">
                    </td>
                    <td style="width: 120px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                        border-top-style: none;">
                    </td>
                    <td style="width: 150px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                        border-top-style: none;">
                    </td>
                    <td style="width: 120px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                        border-top-style: none; border-right-style: none;">
                    </td>
                </tr>
            </table>
            <br />
            <table cellpadding="2" class="mainTable" style="text-align: left;">
                <tbody>
                    <tr align="left">
                        <td class="koumokuMei">
                            郵便番号</td>
                        <td colspan="3">
                            <asp:TextBox ID="tbxYuubinNo" runat="server" MaxLength="8" CssClass="codeNumber"
                                Width="80px"></asp:TextBox>
                            &nbsp;<asp:Button ID="btnKensakuYuubinNo" runat="server" Text="検索" />
                            <asp:Button ID="btnTyousakaisyaCopy" runat="server" Width="190px" Text="調査会社 住所情報コピー"
                                Style="margin-left: 20px;" />
                        </td>
                    </tr>
                    <tr align="left">
                        <td class="koumokuMei">
                            住所１</td>
                        <td>
                            <asp:TextBox ID="tbxJyuusyo1" runat="server" MaxLength="40" Width="380px"></asp:TextBox></td>
                        <td class="koumokuMei">
                            電話番号</td>
                        <td>
                            <asp:TextBox ID="tbxTelNo" runat="server" MaxLength="16" CssClass="codeNumber" Width="130px"></asp:TextBox></td>
                    </tr>
                    <tr align="left">
                        <td class="koumokuMei">
                            住所２</td>
                        <td>
                            <asp:TextBox ID="tbxJyuusyo2" runat="server" MaxLength="30" Width="380px"></asp:TextBox></td>
                        <td class="koumokuMei">
                            FAX番号</td>
                        <td>
                            <asp:TextBox ID="tbxFaxNo" runat="server" MaxLength="16" CssClass="codeNumber" Width="130px"></asp:TextBox></td>
                    </tr>
                    <tr align="left" style="height: 0px;">
                        <td style="width: 150px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            clip: rect(-1px auto auto auto); border-top-style: none;">
                        </td>
                        <td style="width: 395px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none;">
                        </td>
                        <td style="width: 130px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none;">
                        </td>
                        <td style="width: 210px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none; border-right-style: none;">
                        </td>
                    </tr>
                </tbody>
            </table>
            <table cellpadding="2" class="mainTable" style="text-align: left; margin-top: -1px;
                padding-top: 0px; border-top-style: none; clip: rect(-1px auto auto auto); border-top-style: none;">
                <tr align="left">
                    <td class="koumokuMei">
                        部署名</td>
                    <td>
                        <asp:TextBox ID="tbxBusyoMei" runat="server" MaxLength="30" Width="380px"></asp:TextBox></td>
                </tr>
                <tr align="left" style="height: 0px;">
                    <td style="width: 150px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                        clip: rect(-1px auto auto auto); border-top-style: none;">
                    </td>
                    <td style="width: 395px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                        border-top-style: none;">
                    </td>
                </tr>
            </table>
            <br />
            <table cellpadding="0px" cellspacing="0px">
                <tr>
                    <td colspan="2">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="labSeikyuuSaki" ForeColor="blue">【請求先新規登録】</asp:Label></td>
                                <td>
                                    <asp:Label runat="server" ID="labHyouji"></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="2" class="mainTable" style="text-align: left;">
                            <tbody>
                                <tr align="left">
                                    <td class="koumokuMei">
                                        請求先</td>
                                    <td colspan="4">
                                        <asp:DropDownList ID="ddlSeikyuuSaki" runat="server" Width="100px">
                                        </asp:DropDownList>&nbsp;&nbsp;
                                        <asp:TextBox ID="tbxSeikyuuSakiCd" runat="server" MaxLength="5" CssClass="codeNumber"
                                            Width="64px"></asp:TextBox>&nbsp;&nbsp;
                                        <asp:TextBox ID="tbxSeikyuuSakiBrc" runat="server" MaxLength="2" CssClass="codeNumber"
                                            Width="64px"></asp:TextBox>&nbsp;&nbsp;
                                        <asp:Button ID="btnKensakuSeikyuuSaki" runat="server" Text="検索" />
                                        <asp:TextBox ID="tbxSeikyuuSakiMei" runat="server" CssClass="readOnlyStyle" TabIndex="-1"
                                            Width="260px"></asp:TextBox></td>
                                </tr>
                                <tr align="left" style="height: 0px;">
                                    <td style="width: 150px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                                        clip: rect(-1px auto auto auto); border-top-style: none;">
                                    </td>
                                    <td style="width: 130px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                                        border-top-style: none;">
                                    </td>
                                    <td style="width: 130px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                                        border-top-style: none;">
                                    </td>
                                    <td style="width: 135px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                                        border-top-style: none; border-right-style: none;">
                                    </td>
                                    <td style="width: 180px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                                        border-top-style: none; border-right-style: none; border-left-style: none;">
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                    <td>
                        &nbsp;&nbsp;<asp:Button ID="btnKensakuSeikyuuSyousai" runat="server" Text="詳細" /></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="Label1" ForeColor="blue">FCの場合のみ請求先を入力する</asp:Label></td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <div id="divKoteiTyaaji" runat="server">
                <br />
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <table cellpadding="2" class="mainTable" style="text-align: left;">
                                <tbody>
                                    <tr align="left">
                                        <td class="koumokuMei">
                                            固定チャージ
                                        </td>
                                        <td>
                                            <asp:Button ID="btnKoteiTyaaji" runat="server" Width="100px" Text="固定チャージ" Style="padding-top: 2px;" />
                                            <asp:Label ID="lblKoteiTyaaji" runat="server" Style="margin-left: 20px;"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="left" style="height: 0px;">
                                        <td style="width: 160px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                                            clip: rect(-1px auto auto auto); border-top-style: none;">
                                        </td>
                                        <td style="width: 720px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                                            border-top-style: none; border-right-style: none; border-left-style: none;">
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 25px; vertical-align: bottom;">
                            <asp:Label runat="server" ID="Label2" Text="※ 固定チャージ請求は１つの営業所コードに月1回の処理となります"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <%--<br />
    <table cellpadding="0px" cellspacing="0px"><tr><td><asp:Button ID="btnKensakuSeikyuuSoufuCopy" runat="server" Text="送付住所にコピー" /></td></tr></table>
    <br />
    <table cellpadding="2" class="mainTable" style="text-align: left;">
        <tbody>
            <tr align="left">
                <td class="koumokuMei" >
                    請求先名</td>
                <td >
                    <asp:TextBox ID="tbxSeikyuuSakiShriSakiMei" runat="server" MaxLength ="80" Width="290px"></asp:TextBox></td>
                <td class="koumokuMei" colspan ="2" >
                    請求先カナ</td>
                <td colspan ="2">
                    <asp:TextBox ID="tbxSeikyuuSakiShriSakiKana" runat="server" MaxLength ="40" Width="237px"></asp:TextBox></td>
            </tr>
            <tr align="left">
                <td class="koumokuMei" >
                    請求書送付先郵便番号</td>
                <td colspan="5">
                    <asp:TextBox ID="tbxSkysySoufuYuubinNo" runat="server"  MaxLength ="8" style="ime-mode:disabled;" Width="64px"></asp:TextBox>&nbsp;&nbsp;
                    <asp:Button ID="btnKensakuSeikyuuSyo" runat="server" Text="検索" /></td>
            </tr>
            <tr align="left">
                <td class="koumokuMei" >
                    請求書送付先住所１</td>
                <td colspan ="2">
                    <asp:TextBox ID="tbxSkysySoufuJyuusyo1" runat="server" MaxLength ="40" Width="380px"></asp:TextBox></td>
                <td colspan ="2" class="koumokuMei" >
                    請求書送付先電話番号</td>
                <td >
                    <asp:TextBox ID="tbxSkysySoufuTelNo" runat="server" MaxLength ="16" style="ime-mode:disabled;" Width="130px"></asp:TextBox></td>
            </tr>
            <tr align="left">
                <td class="koumokuMei" >
                    請求書送付先住所２</td>
                <td colspan ="2">
                    <asp:TextBox ID="tbxSkysySoufuJyuusyo2" runat="server" MaxLength ="40" Width="380px"></asp:TextBox></td>
                <td colspan ="2" class="koumokuMei" >
                    請求書送付先FAX番号</td>
                <td >
                    <asp:TextBox ID="tbxShriYouFaxNo" runat="server" MaxLength ="16" style="ime-mode:disabled;" Width="130px"></asp:TextBox></td>
            </tr>
            <tr align="left" style="height:0px;">
                <td style="width: 150px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none; clip: rect(-1px auto auto auto); border-top-style: none;" ></td>
                <td style="width: 295px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none; border-top-style: none;" ></td>
                <td style="width: 50px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none; border-top-style: none;" ></td>
                <td style="width: 80px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none; border-top-style: none;" ></td>
                <td style="width: 100px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none; border-top-style: none;"></td>
                <td style="width: 162px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none; border-top-style: none; border-right-style: none;"></td>
            </tr>
        </tbody>
    </table>--%>
            <asp:HiddenField ID="hidUPDTime" runat="server" />
            <!--請求先検索-->
            <asp:HiddenField ID="hidSeikyuuSakiCd" runat="server" />
            <asp:HiddenField ID="hidSeikyuuSakiBrc" runat="server" />
            <asp:HiddenField ID="hidSeikyuuSakiKbn" runat="server" />
            <!--【請求先新規登録】-->
            <asp:HiddenField ID="hidConfirm" runat="server" />
            <asp:HiddenField ID="hidConfirm1" runat="server" />
            <asp:HiddenField ID="hidChange" runat="server" />
            <asp:HiddenField ID="hidSyousai" runat="server" />
            <asp:HiddenField ID="hidChange1" runat="server" />
            <asp:HiddenField ID="hidChange2" runat="server" />
            <asp:HiddenField ID="hidChange3" runat="server" />
            <asp:HiddenField ID="hidChange4" runat="server" />
            <asp:HiddenField ID="hidChange5" runat="server" />
            <asp:HiddenField ID="hidConfirm2" runat="server" />
            <asp:HiddenField ID="hidKousin" runat="server" />
            <!--ボタン-->
            <asp:Button ID="btnOK" runat="server" Text="Button" Style="display: none;" />
            <asp:Button ID="btnNO" runat="server" Text="Button" Style="display: none;" />
            <asp:Button ID="btnFC" runat="server" Text="Button" Style="display: none;" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Button ID="btnCsvOutput" runat="server" Style="display: none;" />
</asp:Content>

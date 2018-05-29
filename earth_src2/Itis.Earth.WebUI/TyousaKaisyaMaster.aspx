<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master"
    Codebehind="TyousaKaisyaMaster.aspx.vb" Inherits="Itis.Earth.WebUI.TyousaKaisyaMaster"
    Title="調査会社マスタメンテナンス" %>

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
            <asp:AsyncPostBackTrigger ControlID="btnSearchTyousaKaisya" />
            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
            <asp:AsyncPostBackTrigger ControlID="btnClear" />
            <asp:AsyncPostBackTrigger ControlID="btnSyuusei" />
            <asp:AsyncPostBackTrigger ControlID="btnTouroku" />
            <asp:AsyncPostBackTrigger ControlID="btnClearMeisai" />
            <asp:AsyncPostBackTrigger ControlID="btnKensakuYuubinNo" />
            <asp:AsyncPostBackTrigger ControlID="btnKensakuFCTen" />
            <%--<asp:AsyncPostBackTrigger ControlID="btnKensakuKensaCenter" />--%>
            <asp:AsyncPostBackTrigger ControlID="btnKensakuSeikyuuSaki" />
            <asp:AsyncPostBackTrigger ControlID="btnKensakuSeikyuuSyousai" />
            <asp:AsyncPostBackTrigger ControlID="btnKensakuSeikyuuSoufuCopy" />
            <asp:AsyncPostBackTrigger ControlID="btnKensakuSeikyuuSyo" />
            <asp:AsyncPostBackTrigger ControlID="btnKensakuSkkShriSaki" />
            <asp:AsyncPostBackTrigger ControlID="btnKensakuShriJigyousyo" />
            <asp:AsyncPostBackTrigger ControlID="btnKensakuShriMeisaiJigyousyo" />
            <asp:AsyncPostBackTrigger ControlID="btnOK" />
            <asp:AsyncPostBackTrigger ControlID="btnNO" />
            <asp:PostBackTrigger ControlID="btnBack" />
        </Triggers>
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="2" class="titleTable" style="width: 960px;
                text-align: left">
                <tbody>
                    <tr>
                        <th>
                            調査会社マスタメンテナンス&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnBack" runat="server" CssClass="kyoutuubutton"
                                Text="戻る" />
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
                            調査会社&nbsp;</td>
                        <td>
                            <asp:TextBox ID="tbxTyousaKaisya_Cd" runat="server" MaxLength="7" Style="ime-mode: disabled;"
                                CssClass="hissu" Width="64px"></asp:TextBox>&nbsp; &nbsp;<asp:Button ID="btnSearchTyousaKaisya"
                                    runat="server" Text="検索" />
                            <asp:TextBox ID="tbxTyousaKaisya_Mei" runat="server" CssClass="readOnlyStyle" TabIndex="-1"
                                MaxLength="40" Width="256px"></asp:TextBox></td>
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
                            &nbsp; &nbsp;<asp:Button ID="btnClearMeisai" runat="server" Text="明細クリア" Width="90px" /></td>
                        <td class="koumokuMei">
                            取消</td>
                        <td>
                            <asp:CheckBox ID="chkTorikesi" runat="server" /></td>
                    </tr>
                    <tr align="left">
                        <td class="koumokuMei">
                            調査会社コード</td>
                        <td>
                            <asp:TextBox ID="tbxTyousaKaisyaCd" runat="server" CssClass="hissu" MaxLength="4"
                                Style="ime-mode: disabled;" Width="64px"></asp:TextBox></td>
                        <td class="koumokuMei">
                            事業所コード</td>
                        <td>
                            <asp:TextBox ID="tbxJigyousyoCd" MaxLength="2" runat="server" Style="ime-mode: disabled;"
                                CssClass="hissu" Width="32px"></asp:TextBox></td>
                        <td class="koumokuMei">
                            取消理由</td>
                        <td>
                            <asp:TextBox ID="tbxTorikesiRiyuu" MaxLength="20" runat="server" Width="256px"></asp:TextBox></td>
                    </tr>
                    <tr align="left">
                        <td class="koumokuMei">
                            調査会社名</td>
                        <td>
                            <asp:TextBox ID="tbxTyousaKaisyaMei" runat="server" MaxLength="40" Width="256px"></asp:TextBox></td>
                        <td class="koumokuMei">
                            調査会社名カナ</td>
                        <td colspan="3">
                            <asp:TextBox ID="tbxTyousaKaisyaMeiKana" MaxLength="20" runat="server" Width="256px"></asp:TextBox></td>
                    </tr>
                    <tr align="left">
                        <td class="koumokuMei">
                            代表者名</td>
                        <td>
                            <asp:TextBox ID="tbxDaihyousyaMei" runat="server" MaxLength="20" Width="256px"></asp:TextBox></td>
                        <td class="koumokuMei">
                            役職名</td>
                        <td colspan="3">
                            <asp:TextBox ID="tbxYasyokuMei" MaxLength="20" runat="server" Width="256px"></asp:TextBox></td>
                    </tr>
                    <tr align="left" style="height: 0px;">
                        <td style="width: 100px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            clip: rect(-1px auto auto auto); border-top-style: none;">
                        </td>
                        <td style="width: 265px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none;">
                        </td>
                        <td style="width: 100px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none;">
                        </td>
                        <td style="width: 72px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none; border-right-style: none;">
                        </td>
                        <td style="width: 72px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none; border-right-style: none; border-left-style: none;">
                        </td>
                        <td style="width: 265px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
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
            <br />
            <table cellpadding="2" class="mainTable" style="text-align: left;">
                <tbody>
                    <tr align="left">
                        <td class="koumokuMei">
                            SS基準価格</td>
                        <td colspan="1">
                            <asp:TextBox ID="tbxSSKijyunKkk" runat="server" MaxLength="7" CssClass="kingaku"
                                Style="ime-mode: disabled;" Width="80px"></asp:TextBox></td>
                        <td class="koumokuMei">
                            SDS保持情報</td>
                        <td colspan="0">
                            <asp:DropDownList ID="ddlSdsJyouhou" runat="server">
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="1">1：SDSを保持している</asp:ListItem>
                                <asp:ListItem Value="2">2：SDSを保持していない</asp:ListItem>
                            </asp:DropDownList></td>
                        <td class="koumokuMei">
                            SDS機器台数</td>
                        <td colspan="0" style="width: 156px;">
                            <asp:TextBox ID="tbxSdsKiki" runat="server" MaxLength="5" CssClass="kingaku" Style="ime-mode: disabled;"
                                Width="100px"></asp:TextBox></td>
                    </tr>
                    <tr align="left">
                        <td class="koumokuMei">
                            調査業務</td>
                        <td>
                            <asp:DropDownList ID="ddlTyousaGyoumu" runat="server">
                            </asp:DropDownList></td>
                        <td class="koumokuMei">
                            工事業務</td>
                        <td colspan="1">
                            <asp:DropDownList ID="ddlKoujiGyoumu" runat="server">
                            </asp:DropDownList></td>
                            
                        <td  class="koumokuMei"  >
                            実在ＦＬＧ
                        </td>
                        <td style="" >
                            <asp:DropDownList ID="ddlJituzaiFlg" runat="server">
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="1" Selected="true">1:実在する</asp:ListItem>
                            </asp:DropDownList>
                        </td> 
                            
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
                    </tr>
                </tbody>
            </table>
            <br />
            <table cellpadding="2" class="mainTable" style="text-align: left;">
                <tbody>
                    <tr align="left">
                        <td class="koumokuMei">
                            FC店</td>
                        <td colspan="5">
                            <asp:TextBox ID="tbxFCTen" runat="server" MaxLength="5" Style="ime-mode: disabled;"
                                Width="64px"></asp:TextBox>&nbsp; &nbsp;<asp:Button ID="btnKensakuFCTen" runat="server"
                                    Text="検索" />
                            <asp:TextBox ID="tbxFCTenMei" runat="server" CssClass="readOnlyStyle" TabIndex="-1"
                                Width="256px"></asp:TextBox></td>
                    </tr>
                    <tr align="left">
                        <td class="koumokuMei">
                            全住品 区分</td>
                        <td>
                            <asp:DropDownList ID="ddlJapanKbn" runat="server">
                            </asp:DropDownList></td>
                        <td class="koumokuMei">
                            全住品 入会年月</td>
                        <td>
                            <asp:TextBox ID="tbxJapanKaiNyuukaiDate" MaxLength="7" runat="server" Style="ime-mode: disabled;
                                text-align: right;" Width="80px"></asp:TextBox></td>
                        <td class="koumokuMei">
                            全住品 退会年月</td>
                        <td>
                            <asp:TextBox ID="tbxJapanKaiTaikaiDate" MaxLength="7" runat="server" Style="ime-mode: disabled;
                                text-align: right;" Width="80px"></asp:TextBox></td>
                    </tr>
                    <tr align="left">
                        <td class="koumokuMei">
                            全住品区分補足</td>
                        <td colspan="5">
                            <asp:TextBox ID="btxZenjyuhinHosoku" runat="server"  Width="631px" MaxLength="80"></asp:TextBox></td>
                    </tr>
                    <tr align="left">
                        <td class="koumokuMei">
                            宅地地盤調査主任資格</td>
                        <td>
                            <asp:DropDownList ID="ddlTktJbnTysSyuninSkkFlg" runat="server">
                            </asp:DropDownList></td>
                        <td class="koumokuMei">
                            Ｒ－ＪＨＳ連携</td>
                        <td colspan="3">
                            <asp:DropDownList ID="ddlRJhsTokenFlg" runat="server">
                            </asp:DropDownList></td>
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
                        <td style="width: 130px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none; border-right-style: none; border-left-style: none;">
                        </td>
                        <td style="width: 135px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
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
                            工事報告書直送</td>
                        <td>
                            <asp:DropDownList ID="ddlKojHkksTyokusouFlg" runat="server">
                            </asp:DropDownList></td>
                        <td class="koumokuMei">
                            工事報告書直送変更ログインユーザ</td>
                        <td>
                            <asp:TextBox ID="tbxKojHkksTyokusouUpdLoginUserId" runat="server" CssClass="readOnlyStyle"
                                TabIndex="-1" Width="150px"></asp:TextBox></td>
                        <td class="koumokuMei">
                            工事報告書直送変更日時</td>
                        <td>
                            <asp:TextBox ID="tbxKojHkksTyokusouUpdDatetime" runat="server" CssClass="readOnlyStyle"
                                TabIndex="-1" Width="150px"></asp:TextBox></td>
                    </tr>
                    <tr align="left" style="height: 0px;">
                        <td style="width: 100px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            clip: rect(-1px auto auto auto); border-top-style: none;">
                        </td>
                        <td style="width: 80px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none;">
                        </td>
                        <td style="width: 200px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none;">
                        </td>
                        <td style="width: 165px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none; border-right-style: none;">
                        </td>
                        <td style="width: 130px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none; border-right-style: none; border-left-style: none;">
                        </td>
                        <td style="width: 160px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none; border-left-style: none;">
                        </td>
                    </tr>
                </tbody>
            </table>
<%--            <br />
            <table cellpadding="2" class="mainTable" style="text-align: left;">
                <tbody>
                    <tr align="left">
                        <td class="koumokuMei">
                            建物検査センター</td>
                        <td colspan="4">
                            <asp:TextBox ID="tbxKensakuKensaCenter" runat="server" MaxLength="10" Style="ime-mode: disabled;"
                                Width="75px"></asp:TextBox>&nbsp; &nbsp;<asp:Button ID="btnKensakuKensaCenter" runat="server"
                                    Text="検査センター名取得" />
                            <asp:TextBox ID="tbxKensakuKensaCenterMei" runat="server" CssClass="readOnlyStyle"
                                Width="256px"></asp:TextBox></td>
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
                        <td style="width: 130px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none; border-right-style: none; border-left-style: none;">
                        </td>
                    </tr>
                </tbody>
            </table>--%>
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
                                        <asp:TextBox ID="tbxSeikyuuSakiCd" runat="server" MaxLength="5" Style="ime-mode: disabled;"
                                            Width="64px"></asp:TextBox>&nbsp;&nbsp;
                                        <asp:TextBox ID="tbxSeikyuuSakiBrc" runat="server" MaxLength="2" Style="ime-mode: disabled;"
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
            </table>
            <br />
            <table cellpadding="0px" cellspacing="0px">
                <tr>
                    <td>
                        <asp:Button ID="btnKensakuSeikyuuSoufuCopy" runat="server" Text="請求先名・送付住所にコピー" /></td>
                </tr>
            </table>
            <br />
            <table cellpadding="2" class="mainTable" style="text-align: left;">
                <tbody>
                    <tr align="left">
                        <td class="koumokuMei">
                            請求先支払先名</td>
                        <td>
                            <asp:TextBox ID="tbxSeikyuuSakiShriSakiMei" runat="server" MaxLength="80" Width="290px"></asp:TextBox></td>
                        <td class="koumokuMei" colspan="2">
                            請求先支払先名カナ</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbxSeikyuuSakiShriSakiKana" runat="server" MaxLength="40" Width="237px"></asp:TextBox></td>
                    </tr>
                    <tr align="left">
                        <td class="koumokuMei">
                            請求書送付先郵便番号</td>
                        <td colspan="5">
                            <asp:TextBox ID="tbxSkysySoufuYuubinNo" runat="server" MaxLength="8" Style="ime-mode: disabled;"
                                Width="64px"></asp:TextBox>&nbsp;&nbsp;
                            <asp:Button ID="btnKensakuSeikyuuSyo" runat="server" Text="検索" /></td>
                    </tr>
                    <tr align="left">
                        <td class="koumokuMei">
                            請求書送付先住所１</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbxSkysySoufuJyuusyo1" runat="server" MaxLength="40" Width="380px"></asp:TextBox></td>
                        <td colspan="2" class="koumokuMei">
                            請求書送付先電話番号</td>
                        <td>
                            <asp:TextBox ID="tbxSkysySoufuTelNo" runat="server" MaxLength="16" Style="ime-mode: disabled;"
                                Width="130px"></asp:TextBox></td>
                    </tr>
                    <tr align="left">
                        <td class="koumokuMei">
                            請求書送付先住所２</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbxSkysySoufuJyuusyo2" runat="server" MaxLength="40" Width="380px"></asp:TextBox></td>
                        <td colspan="2" class="koumokuMei">
                            支払用FAX番号</td>
                        <td>
                            <asp:TextBox ID="tbxShriYouFaxNo" runat="server" MaxLength="16" Style="ime-mode: disabled;"
                                Width="130px"></asp:TextBox></td>
                    </tr>
                    <tr align="left" style="height: 0px;">
                        <td style="width: 150px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            clip: rect(-1px auto auto auto); border-top-style: none;">
                        </td>
                        <td style="width: 295px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none;">
                        </td>
                        <td style="width: 50px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none;">
                        </td>
                        <td style="width: 80px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none;">
                        </td>
                        <td style="width: 100px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none;">
                        </td>
                        <td style="width: 162px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none; border-right-style: none;">
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <table cellpadding="2" class="mainTable" style="text-align: left;">
                <tbody>
                    <tr align="left">
                        <td class="koumokuMei">
                            新会計支払先</td>
                        <td colspan="4">
                            <asp:TextBox ID="tbxSkkJigyousyoCd" runat="server" MaxLength="10" Style="ime-mode: disabled;"
                                Width="64px"></asp:TextBox>
                            &nbsp;-&nbsp;
                            <asp:TextBox ID="tbxSkkShriSakiCd" runat="server" MaxLength="10" Style="ime-mode: disabled;"
                                Width="64px"></asp:TextBox>&nbsp;&nbsp;
                            <asp:Button ID="btnKensakuSkkShriSaki" runat="server" Text="検索" />
                            <asp:TextBox ID="tbxSkkShriSakiMei" runat="server" CssClass="readOnlyStyle" TabIndex="-1"
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
                        <td style="width: 335px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none; border-right-style: none; border-left-style: none;">
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <table cellpadding="2" class="mainTable" style="text-align: left;">
                <tbody>
                    <tr align="left">
                        <td class="koumokuMei">
                            支払締め日</td>
                        <td>
                            <asp:TextBox ID="tbxShriSimeDate" MaxLength="2" runat="server" Style="ime-mode: disabled;
                                text-align: right;" Width="60px"></asp:TextBox></td>
                        <td class="koumokuMei">
                            支払予定月数</td>
                        <td>
                            <asp:TextBox ID="tbxShriYoteiGessuu" MaxLength="2" runat="server" Style="ime-mode: disabled;
                                text-align: right;" Width="60px"></asp:TextBox></td>
                        <td class="koumokuMei">
                            ファクタリング開始年月</td>
                        <td>
                            <asp:TextBox ID="tbxFctringKaisiNengetu" MaxLength="7" runat="server" Style="ime-mode: disabled;
                                text-align: right;" Width="100px"></asp:TextBox></td>
                    </tr>
                    <tr align="left">
                        <td class="koumokuMei">
                            支払集計先事業所</td>
                        <td colspan="5">
                            <asp:TextBox ID="tbxTysKaisyaCd" runat="server" CssClass="readOnlyStyle" TabIndex="-1"
                                Width="80px"></asp:TextBox>
                            &nbsp;-&nbsp;
                            <asp:TextBox ID="tbxShriJigyousyoCd" runat="server" MaxLength="2" Style="ime-mode: disabled;"
                                Width="60px"></asp:TextBox>
                            <asp:Button ID="btnKensakuShriJigyousyo" runat="server" Text="検索" />
                            <asp:TextBox ID="tbxTysKaisyaMei" runat="server" CssClass="readOnlyStyle" TabIndex="-1"
                                Width="260px"></asp:TextBox></td>
                    </tr>
                    <tr align="left">
                        <td class="koumokuMei">
                            支払明細集計先事業所</td>
                        <td colspan="5">
                            <asp:TextBox ID="tbxTysMeisaiKaisyaCd" runat="server" CssClass="readOnlyStyle" TabIndex="-1"
                                Width="80px"></asp:TextBox>
                            &nbsp;-&nbsp;
                            <asp:TextBox ID="tbxShriMeisaiJigyousyoCd" runat="server" MaxLength="2" Style="ime-mode: disabled;"
                                Width="60px"></asp:TextBox>
                            <asp:Button ID="btnKensakuShriMeisaiJigyousyo" runat="server" Text="検索" />
                            <asp:TextBox ID="tbxTysMeisaiKaisyaMei" runat="server" CssClass="readOnlyStyle" TabIndex="-1"
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
                        <td style="width: 130px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none; border-right-style: none; border-left-style: none;">
                        </td>
                        <td style="width: 194px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none; border-left-style: none;">
                        </td>
                    </tr>
                </tbody>
            </table>
            <asp:HiddenField ID="hidUPDTime" runat="server" />
            <!--請求先検索-->
            <asp:HiddenField ID="hidSeikyuuSakiCd" runat="server" />
            <asp:HiddenField ID="hidSeikyuuSakiBrc" runat="server" />
            <asp:HiddenField ID="hidSeikyuuSakiKbn" runat="server" />
            <!--建物検査センター検索-->
            <asp:HiddenField ID="hidKensakuKensaCenter" runat="server" />
            <!--新会計支払先検索-->
            <asp:HiddenField ID="hidSkkJigyousyoCd" runat="server" />
            <asp:HiddenField ID="hidSkkShriSakiCd" runat="server" />
            <!--支払集計先事業所検索-->
            <asp:HiddenField ID="hidShriJigyousyoCd" runat="server" />
            <!--支払明細集計先事業所検索-->
            <asp:HiddenField ID="hidShriMeisaiJigyousyoCd" runat="server" />
            <!--工事報告書直送-->
            <asp:HiddenField ID="hidKojHkksTyokusouFlg" runat="server" />
            <!--【請求先新規登録】-->
            <asp:HiddenField ID="hidConfirm" runat="server" />
            <asp:HiddenField ID="hidConfirm1" runat="server" />
            <!--取消-->
            <asp:HiddenField ID="hidTorikesi" runat="server" />
            <asp:HiddenField ID="hidChange" runat="server" />
            <asp:HiddenField ID="hidSyousai" runat="server" />
            <asp:HiddenField ID="hidChange1" runat="server" />
            <asp:HiddenField ID="hidChange2" runat="server" />
            <asp:HiddenField ID="hidChange3" runat="server" />
            <asp:HiddenField ID="hidChange4" runat="server" />
            <asp:HiddenField ID="hidChange5" runat="server" />
            <asp:HiddenField ID="hidConfirm2" runat="server" />
            <!--ボタン-->
            <asp:Button ID="btnOK" runat="server" Text="Button" Style="display: none;" />
            <asp:Button ID="btnNO" runat="server" Text="Button" Style="display: none;" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

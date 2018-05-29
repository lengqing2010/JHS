<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master"
    Codebehind="SyouhinMaster.aspx.vb" Inherits="Itis.Earth.WebUI.SyouhinMaster"
    Title="商品マスタメンテナンス" %>

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

    <table border="0" cellpadding="0" cellspacing="2" class="titleTable" style="width: 960px;
        text-align: left">
        <tbody>
            <tr>
                <th>
                    商品マスタメンテナンス &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;<asp:Button ID="btnBack" runat="server"
                        CssClass="kyoutuubutton" Text="戻る" /></th>
                <th style="text-align: right">
                </th>
            </tr>
            <tr>
                <td colspan="2" rowspan="1" style="height: 12px">
                </td>
            </tr>
        </tbody>
    </table>
    <asp:UpdatePanel ID="UpdatePanelA" runat="server" UpdateMode="Conditional" RenderMode="Inline">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSearchSyouhin" />
            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
            <asp:AsyncPostBackTrigger ControlID="btnClear" />
            <asp:AsyncPostBackTrigger ControlID="btnSyuusei" />
            <asp:AsyncPostBackTrigger ControlID="btnTouroku" />
            <asp:AsyncPostBackTrigger ControlID="btnClearMeisai" />
        </Triggers>
        <ContentTemplate>
            <table cellpadding="2" class="mainTable" style="text-align: left;">
                <tbody>
                    <tr>
                        <td class="koumokuMei" style="width: 90px">
                            商品&nbsp;</td>
                        <td>
                            <asp:TextBox ID="tbxSyouhin_cd" runat="server" MaxLength="8" Style="ime-mode: disabled;"
                                CssClass="hissu" Width="64px"></asp:TextBox>&nbsp; &nbsp;<asp:Button ID="btnSearchSyouhin"
                                    runat="server" Text="検索" />
                            <asp:TextBox ID="tbxSyouhin_mei" runat="server" CssClass="readOnlyStyle" TabIndex="-1"
                                MaxLength="40" Width="288px"></asp:TextBox></td>
                    </tr>
                    <tr align="left">
                        <td colspan="2" rowspan="1">
                            <asp:Button ID="btnSearch" runat="server" Text="選択 ＆ 編集" Width="112px" />&nbsp;
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
                            <asp:Button ID="btnSyuusei" runat="server" Text="修正実行" Width="112px" />&nbsp;
                            <asp:Button ID="btnTouroku" runat="server" Text="新規登録" Width="112px" />&nbsp;
                            <asp:Button ID="btnClearMeisai" runat="server" Text="明細クリア" Width="112px" /></td>
                        <td class="koumokuMei" style="width: 72px">
                            取消</td>
                        <td style="width: 62px">
                            <asp:CheckBox ID="chkTorikesi" runat="server" /></td>
                    </tr>
                    <tr align="left">
                        <td class="koumokuMei" style="width: 106px">
                            商品コード</td>
                        <td style="width: 194px">
                            <asp:TextBox ID="tbxSyouhinCd" runat="server" CssClass="hissu" MaxLength="8" Style="ime-mode: disabled;"
                                Width="64px"></asp:TextBox></td>
                        <td class="koumokuMei" style="width: 98px">
                            商品名</td>
                        <td colspan="3" style="width: 216px">
                            <asp:TextBox ID="tbxSyouhinMei" MaxLength="40" runat="server" CssClass="hissu" Width="256px"></asp:TextBox></td>
                    </tr>
                    <tr align="left">
                        <td class="koumokuMei" style="width: 106px; clip: rect(auto auto -1px auto); border-bottom-style: none;
                            height: 23px;">
                            単位</td>
                        <td style="width: 194px; clip: rect(auto auto -1px auto); border-bottom-style: none;
                            height: 23px;">
                            <asp:TextBox ID="tbxTanni" MaxLength="4" runat="server" Width="64px"></asp:TextBox></td>
                        <td class="koumokuMei" style="width: 98px; clip: rect(auto auto -1px auto); border-bottom-style: none;
                            height: 23px;">
                            支払用商品名</td>
                        <td colspan="3" style="width: 216px; clip: rect(auto auto -1px auto); border-bottom-style: none;
                            height: 23px;">
                            <asp:TextBox ID="tbxShiharaiSyouhin" MaxLength="40" runat="server" Width="256px"></asp:TextBox></td>
                    </tr>
                    <tr align="left" style="height: 0px;">
                        <td style="width: 106px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            clip: rect(-1px auto auto auto); border-top-style: none;">
                        </td>
                        <td style="width: 183px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none;">
                        </td>
                        <td style="width: 95px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none;">
                        </td>
                        <td style="width: 72px; margin-bottom: -1px; padding-bottom: 0px; border-bottom-style: none;
                            border-top-style: none; border-right-style: none;">
                        </td>
                        <td style="margin-bottom: -1px; padding-bottom: 0px; width: 72px; border-bottom-style: none;
                            border-top-style: none; border-right-style: none; border-left-style: none;">
                        </td>
                        <td style="margin-bottom: -1px; padding-bottom: 0px; width: 113px; border-bottom-style: none;
                            border-top-style: none; border-left-style: none;">
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <table cellpadding="2" class="mainTable" style="text-align: left;">
                <tbody>
                    <tr>
                        <td class="koumokuMei" style="width: 106px">
                            税区分</td>
                        <td style="width: 194px">
                            <asp:DropDownList ID="ddlZeiKBN" runat="server">
                            </asp:DropDownList></td>
                        <td class="koumokuMei" style="width: 98px">
                            商品区分１</td>
                        <td style="width: 160px;">
                            <asp:DropDownList ID="ddlSyouhinKBN1" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="koumokuMei" style="width: 106px">
                            税込区分</td>
                        <td style="width: 194px">
                            <asp:DropDownList ID="ddlZeikomi" runat="server">
                            </asp:DropDownList></td>
                        <td class="koumokuMei" style="width: 98px">
                            商品区分２</td>
                        <td style="width: 160px;">
                            <asp:DropDownList ID="ddlSyouhinKBN2" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="koumokuMei" style="width: 106px">
                            商品タイプ</td>
                        <td style="width: 194px">
                            <asp:DropDownList ID="ddlKouji" runat="server" Width="192px">
                            </asp:DropDownList></td>
                        <td class="koumokuMei" style="width: 98px">
                            商品区分３</td>
                        <td style="width: 160px;">
                            <asp:DropDownList ID="ddlSyouhinKBN3" CssClass="hissu" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                </tbody>
            </table>
            <br />
            <table cellpadding="2" class="mainTable" style="text-align: left;">
                <tbody>
                    <tr>
                        <td class="koumokuMei" style="width: 106px">
                            保証有無</td>
                        <td style="width: 194px">
                            <asp:DropDownList CssClass="hissu" ID="ddlHosyou" runat="server">
                            </asp:DropDownList></td>
                        <td class="koumokuMei" style="width: 98px">
                            倉庫コード</td>
                        <td style="width: 160px">
                            <asp:DropDownList ID="ddlSoukoCd" runat="server">
                            </asp:DropDownList></td>
                        <td class="koumokuMei" style="width: 80px">
                            調査有無</td>
                        <td style="width: 160px">
                            <asp:DropDownList ID="ddlSyousa" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="koumokuMei" style="width: 106px">
                            標準価格</td>
                        <td style="width: 194px">
                            <asp:TextBox ID="tbxHyoujun" CssClass="kingaku" MaxLength="7" runat="server" Width="136px"></asp:TextBox></td>
                        <td class="koumokuMei" style="width: 98px">
                            仕入価格</td>
                        <td style="width: 160px">
                            <asp:TextBox ID="tbxSiire" CssClass="kingaku" MaxLength="7" runat="server" Width="136px"></asp:TextBox></td>
                        <td class="koumokuMei" style="width: 80px">
                            社内原価</td>
                        <td>
                            <asp:TextBox ID="tbxSyanai" CssClass="kingaku" MaxLength="7" runat="server" Width="136px"></asp:TextBox></td>
                    </tr>
                </tbody>
            </table>
            <br />
            <table cellpadding="2" class="mainTable" style="text-align: left;">
                <tbody>
                    <tr>
                        <td class="koumokuMei" style="width: 106px">
                            <asp:Label ID="lblSyouhinMeisyo1" runat="server" Text=""></asp:Label></td>
                        <td style="width: 194px">
                            <asp:DropDownList ID="ddlSyouhinSyubetu1" runat="server" Width="150px">
                            </asp:DropDownList></td>
                        <td class="koumokuMei" style="width: 98px">
                            <asp:Label ID="lblSyouhinMeisyo2" runat="server" Text=""></asp:Label></td>
                        <td style="width: 160px">
                            <asp:DropDownList ID="ddlSyouhinSyubetu2" runat="server" Width="150px">
                            </asp:DropDownList></td>
                        <td class="koumokuMei" style="width: 80px">
                           <asp:Label ID="lblSyouhinMeisyo3" runat="server" Text=""></asp:Label></td>
                        <td style="width: 160px">
                            <asp:DropDownList ID="ddlSyouhinSyubetu3" runat="server" Width="150px">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="koumokuMei" style="width: 106px">
                            <asp:Label ID="lblSyouhinMeisyo4" runat="server" Text=""></asp:Label></td>
                        <td style="width: 194px">
                            <asp:DropDownList ID="ddlSyouhinSyubetu4" runat="server" Width="150px">
                            </asp:DropDownList></td>
                        <td class="koumokuMei" style="width: 98px">
                            <asp:Label ID="lblSyouhinMeisyo5" runat="server" Text=""></asp:Label></td>
                        <td style="width: 160px">
                            <asp:DropDownList ID="ddlSyouhinSyubetu5" runat="server" Width="150px">
                            </asp:DropDownList></td>
                        <td class="koumokuMei">
                           <asp:Label ID="lblSyouhinMeisyo6" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSyouhinSyubetu6" runat="server" Width="150px">
                            </asp:DropDownList>
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="koumokuMei" style="width: 106px">
                            <asp:Label ID="lblSyouhinMeisyo7" runat="server" Text=""></asp:Label></td>
                        <td style="width: 194px">
                            <asp:DropDownList ID="ddlSyouhinSyubetu7" runat="server" Width="150px">
                            </asp:DropDownList></td>
                        <td class="koumokuMei" style="width: 98px">
                            <asp:Label ID="lblSyouhinMeisyo8" runat="server" Text=""></asp:Label></td>
                        <td style="width: 160px">
                            <asp:DropDownList ID="ddlSyouhinSyubetu8" runat="server" Width="150px">
                            </asp:DropDownList></td>
                        <td class="koumokuMei">
                            <asp:Label ID="lblSyouhinMeisyo9" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSyouhinSyubetu9" runat="server" Width="150px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="koumokuMei" style="width: 106px">
                           <asp:Label ID="lblSyouhinMeisyo10" runat="server" Text=""></asp:Label></td>
                        <td style="width: 194px">
                            <asp:DropDownList ID="ddlSyouhinSyubetu10" runat="server" Width="150px">
                            </asp:DropDownList></td>
                        <td class="koumokuMei" style="width: 98px">
                            <asp:Label ID="lblSyouhinMeisyo11" runat="server" Text=""></asp:Label></td>
                        <td style="width: 160px">
                            <asp:DropDownList ID="ddlSyouhinSyubetu11" runat="server" Width="150px">
                            </asp:DropDownList></td>
                        <td class="koumokuMei">
                            <asp:Label ID="lblSyouhinMeisyo12" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSyouhinSyubetu12" runat="server" Width="150px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="koumokuMei" style="width: 106px">
                            <asp:Label ID="lblSyouhinMeisyo13" runat="server" Text=""></asp:Label></td>
                        <td style="width: 194px">
                            <asp:DropDownList ID="ddlSyouhinSyubetu13" runat="server" Width="150px">
                            </asp:DropDownList></td>
                        <td class="koumokuMei" style="width: 98px">
                            <asp:Label ID="lblSyouhinMeisyo14" runat="server" Text=""></asp:Label></td>
                        <td style="width: 160px">
                            <asp:DropDownList ID="ddlSyouhinSyubetu14" runat="server" Width="150px">
                            </asp:DropDownList></td>
                        <td class="koumokuMei">
                            <asp:Label ID="lblSyouhinMeisyo15" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSyouhinSyubetu15" runat="server" Width="150px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="koumokuMei" style="width: 106px">
                            <asp:Label ID="lblSyouhinMeisyo16" runat="server" Text=""></asp:Label></td>
                        <td style="width: 194px">
                            <asp:DropDownList ID="ddlSyouhinSyubetu16" runat="server" Width="150px">
                            </asp:DropDownList></td>
                        <td class="koumokuMei" style="width: 98px">
                            <asp:Label ID="lblSyouhinMeisyo17" runat="server" Text=""></asp:Label></td>
                        <td style="width: 160px">
                            <asp:DropDownList ID="ddlSyouhinSyubetu17" runat="server" Width="150px">
                            </asp:DropDownList></td>
                        <td class="koumokuMei">
                            
                        </td>
                        <td>
                            
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <table cellpadding="2" class="mainTable" style="text-align: left;">
                <tbody>
                    <tr>
                        <td class="koumokuMei" style="width: 106px">
                            ＳＤＳ自動設定</td>
                        <td style="width: 194px">
                            <asp:DropDownList ID="ddlSdsSeltutei" runat="server" Width="150px">
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="1">1：SDS自動設定する</asp:ListItem>
                            </asp:DropDownList></td>
                </tbody>
            </table>
            <asp:HiddenField ID="hidUPDTime" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

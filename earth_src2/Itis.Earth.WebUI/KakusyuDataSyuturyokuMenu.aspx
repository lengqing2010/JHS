<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master" CodeBehind="KakusyuDataSyuturyokuMenu.aspx.vb" Inherits="Itis.Earth.WebUI.KakusyuDataSyuturyokuMenu" 
    title="各種データ出力" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript" src="js/jhsearth.js"></script>
<script type="text/javascript">
    //window名付与
    var objWin = window;
    objWin.name = "earthMainWindow"
    initPage(); //画面初期設定  
</script>
<asp:UpdatePanel ID="UpdatePanelA" runat="server" UpdateMode="Conditional"  RenderMode="Inline">
    <Triggers>
        <asp:PostBackTrigger ControlID ="btnSeikyuuSakiSyuturyoku" />
        <asp:PostBackTrigger ControlID ="btnTyousaKaisyaSyuturyoku" />
        <asp:PostBackTrigger ControlID ="btnSyouhinSyuturyoku" />
        <asp:PostBackTrigger ControlID ="btnGinkouSyuturyoku" />
        
        <asp:AsyncPostBackTrigger ControlID ="titleText_SeikyuuSaki" />
        <asp:AsyncPostBackTrigger ControlID ="btnKensakuSeikyuuSaki" />
        <asp:AsyncPostBackTrigger ControlID ="btnKensakuSeikyuuSaki2" />
        <asp:AsyncPostBackTrigger ControlID ="btnKensakuSeikyuuSaki3" />
        <asp:AsyncPostBackTrigger ControlID ="btnKensakuSeikyuuSaki4" />
        <asp:AsyncPostBackTrigger ControlID ="btnKensakuSeikyuuSaki5" />
        <asp:AsyncPostBackTrigger ControlID ="btnKensakuSeikyuuSaki6" />
        <asp:AsyncPostBackTrigger ControlID ="btnKensakuSeikyuuSaki7" />
        <asp:AsyncPostBackTrigger ControlID ="btnKensakuSeikyuuSaki8" />
        <asp:AsyncPostBackTrigger ControlID ="btnKensakuSeikyuuSaki9" />
        <asp:AsyncPostBackTrigger ControlID ="btnKensakuSeikyuuSaki10" />
        <asp:AsyncPostBackTrigger ControlID ="btnKensakuSeikyuuSaki11" />
        <asp:AsyncPostBackTrigger ControlID ="btnKensakuSeikyuuSaki12" />
        <asp:AsyncPostBackTrigger ControlID ="btnKensakuSeikyuuSaki13" />
        <asp:AsyncPostBackTrigger ControlID ="btnKensakuSeikyuuSaki14" />
        <asp:AsyncPostBackTrigger ControlID ="btnKensakuSeikyuuSaki15" />
        <asp:AsyncPostBackTrigger ControlID ="btnKensakuSeikyuuSaki16" />
        <asp:AsyncPostBackTrigger ControlID ="btnKensakuSeikyuuSaki17" />
        <asp:AsyncPostBackTrigger ControlID ="btnKensakuSeikyuuSaki18" />
        <asp:AsyncPostBackTrigger ControlID ="btnKensakuSeikyuuSaki19" />
        <asp:AsyncPostBackTrigger ControlID ="btnKensakuSeikyuuSaki20" />
        <asp:AsyncPostBackTrigger ControlID ="btnKensakuSiire" />
        <asp:AsyncPostBackTrigger ControlID ="btnKensakuSiharai" />
        
        <asp:PostBackTrigger ControlID ="btnHyouji" />
        <asp:PostBackTrigger ControlID ="btnDataSyuturyoku" />
    </Triggers> 
    <ContentTemplate >
        <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
            class="titleTable">
            <tbody>
                <tr>
                    <th>
                        各種データ出力
                    </th>
                    <th style="text-align: right;">
                    </th>
                </tr>
                <tr>
                    <td colspan="2" rowspan="1">
                    </td>
                </tr>
            </tbody>
        </table>
<br />
<br />
        <table style ="width :800px;" >
            <tr>
                <th style ="width :80px;font-size:15px;" align ="right" >マスタ</th>
                <th colspan="4" style="width :700px;"></th>
            </tr>
            <tr>
                <td  style ="height :60px;" ></td>
                <td  align="left" ><asp:Button ID="btnSeikyuuSakiSyuturyoku" runat="server" Text="請求先マスタ出力" Height="28px" Width="200px" Font-Size="10pt" Font-Bold="true" /></td>
                <td style ="width:30px;"></td>
                <td  align="left"><asp:Button ID="btnTyousaKaisyaSyuturyoku" runat="server" Text="調査会社マスタ出力" Height="28px" Width="200px" Font-Size="10pt" Font-Bold="true" /></td>
                <td style ="width:260px;"></td>
            </tr>

            <tr>
                <td ></td>
                <td  align="left"><asp:Button ID="btnSyouhinSyuturyoku" runat="server" Text="商品マスタ出力" Height="28px" Width="200px" Font-Size="10pt" Font-Bold="true" /></td>
                <td style ="width:30px;"></td>
                <td  align="left"><asp:Button ID="btnGinkouSyuturyoku" runat="server" Text="銀行マスタ出力" Height="28px" Width="200px" Font-Size="10pt" Font-Bold="true" /></td>
                <td ></td>
            </tr>
            <tr>
                <td colspan="5" style ="height :30px;"></td>
            </tr>
            <tr>
                <th style ="width :80px;font-size:15px;" align ="right">データ</th>
                <th colspan="4"></th>
            </tr>
            <tr>
                <td style ="height :60px;"></td>
                <td>
                    <asp:DropDownList ID="ddlUriageData" Width ="200px" runat="server" AutoPostBack="true" >
                    </asp:DropDownList></td>
                <td colspan ="4"></td>
            </tr>
            
            <tr>
                <td   ></td>
                <td colspan ="4">
                    <table cellpadding="2" class="mainTable" style="text-align: left;">
                        <tbody>
                            <tr align="left">
                                <td class="koumokuMei" style ="width :70px; background-color:#ccffff;" >
                                    抽出期間</td>
                                <td colspan ="4" >
                                    <asp:TextBox ID="tbxFrom" runat="server"  MaxLength ="10" style="ime-mode:disabled;text-align:right;" Width="120px"></asp:TextBox>
                                    &nbsp;&nbsp;～&nbsp;&nbsp;
                                    <asp:TextBox ID="tbxTo" runat="server"  MaxLength ="10" style="ime-mode:disabled;text-align:right;" Width="120px"></asp:TextBox>&nbsp;&nbsp;
                            </tr>
                        </tbody>
                    </table>
                </td> 
            </tr>
            <tr><td style ="height :15px;"></td></tr> 
            <tr>
                <td></td>
                <td colspan ="4">
                    <table runat ="server" id="tblSeikyuuSakiSitei" visible ="false" cellpadding="2" cellspacing="0" class="mainTable" style="text-align: left;">
                        <tbody>  
                            <tr runat ="server" id="tblSeikyuuSakiTitle">
                                <th class="tableTitle" rowspan="1" style="height: 27px; width:664px">
                                    <a id="titleText_SeikyuuSaki" runat="server">請求先指定</a> 
                                </th>
                            </tr>
                            <!--請求先指定明細-->
                            <tr runat ="server" id="tblSeikyuuSakiMeisai">
                                <td style="padding:0">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr align="left">
                                            <td class="koumokuMei" style ="width :70px;" >
                                                請求先&nbsp;1</td>
                                            <td colspan ="4" >
                                                <asp:DropDownList ID="ddlSeikyuuSaki1" runat="server" Width ="100px">
                                                </asp:DropDownList>&nbsp;&nbsp;
                                                <asp:TextBox ID="tbxSeikyuuSakiCd1" runat="server"  MaxLength ="5" style="ime-mode:disabled;" Width="64px"></asp:TextBox>&nbsp;&mdash;
                                                <asp:TextBox ID="tbxSeikyuuSakiBrc1" runat="server"  MaxLength ="2" style="ime-mode:disabled;" Width="32px"></asp:TextBox>&nbsp;&nbsp;
                                                <asp:Button ID="btnKensakuSeikyuuSaki1" runat="server" Text="検索" />&nbsp;&nbsp;
                                                <asp:TextBox ID="tbxSeikyuuSakiMei1" runat="server" CssClass="readOnlyStyle" TabIndex ="-1" Width="280px"></asp:TextBox></td>
                                        </tr>
                                        <tr align="left">
                                            <td class="koumokuMei" style ="width :70px;" >
                                                請求先&nbsp;2</td>
                                            <td colspan ="4" >
                                                <asp:DropDownList ID="ddlSeikyuuSaki2" runat="server" Width ="100px">
                                                </asp:DropDownList>&nbsp;&nbsp;
                                                <asp:TextBox ID="tbxSeikyuuSakiCd2" runat="server"  MaxLength ="5" style="ime-mode:disabled;" Width="64px"></asp:TextBox>&nbsp;&mdash;
                                                <asp:TextBox ID="tbxSeikyuuSakiBrc2" runat="server"  MaxLength ="2" style="ime-mode:disabled;" Width="32px"></asp:TextBox>&nbsp;&nbsp;
                                                <asp:Button ID="btnKensakuSeikyuuSaki2" runat="server" Text="検索" />&nbsp;&nbsp;
                                                <asp:TextBox ID="tbxSeikyuuSakiMei2" runat="server" CssClass="readOnlyStyle" TabIndex ="-1" Width="280px"></asp:TextBox></td>
                                        </tr>
                                        <tr align="left">
                                            <td class="koumokuMei" style ="width :70px;" >
                                                請求先&nbsp;3</td>
                                            <td colspan ="4" >
                                                <asp:DropDownList ID="ddlSeikyuuSaki3" runat="server" Width ="100px">
                                                </asp:DropDownList>&nbsp;&nbsp;
                                                <asp:TextBox ID="tbxSeikyuuSakiCd3" runat="server"  MaxLength ="5" style="ime-mode:disabled;" Width="64px"></asp:TextBox>&nbsp;&mdash;
                                                <asp:TextBox ID="tbxSeikyuuSakiBrc3" runat="server"  MaxLength ="2" style="ime-mode:disabled;" Width="32px"></asp:TextBox>&nbsp;&nbsp;
                                                <asp:Button ID="btnKensakuSeikyuuSaki3" runat="server" Text="検索" />&nbsp;&nbsp;
                                                <asp:TextBox ID="tbxSeikyuuSakiMei3" runat="server" CssClass="readOnlyStyle" TabIndex ="-1" Width="280px"></asp:TextBox></td>
                                        </tr>
                                        <tr align="left">
                                            <td class="koumokuMei" style ="width :70px;" >
                                                請求先&nbsp;4</td>
                                            <td colspan ="4" >
                                                <asp:DropDownList ID="ddlSeikyuuSaki4" runat="server" Width ="100px">
                                                </asp:DropDownList>&nbsp;&nbsp;
                                                <asp:TextBox ID="tbxSeikyuuSakiCd4" runat="server"  MaxLength ="5" style="ime-mode:disabled;" Width="64px"></asp:TextBox>&nbsp;&mdash;
                                                <asp:TextBox ID="tbxSeikyuuSakiBrc4" runat="server"  MaxLength ="2" style="ime-mode:disabled;" Width="32px"></asp:TextBox>&nbsp;&nbsp;
                                                <asp:Button ID="btnKensakuSeikyuuSaki4" runat="server" Text="検索" />&nbsp;&nbsp;
                                                <asp:TextBox ID="tbxSeikyuuSakiMei4" runat="server" CssClass="readOnlyStyle" TabIndex ="-1" Width="280px"></asp:TextBox></td>
                                        </tr>
                                        <tr align="left">
                                            <td class="koumokuMei" style ="width :70px;" >
                                                請求先&nbsp;5</td>
                                            <td colspan ="4" >
                                                <asp:DropDownList ID="ddlSeikyuuSaki5" runat="server" Width ="100px">
                                                </asp:DropDownList>&nbsp;&nbsp;
                                                <asp:TextBox ID="tbxSeikyuuSakiCd5" runat="server"  MaxLength ="5" style="ime-mode:disabled;" Width="64px"></asp:TextBox>&nbsp;&mdash;
                                                <asp:TextBox ID="tbxSeikyuuSakiBrc5" runat="server"  MaxLength ="2" style="ime-mode:disabled;" Width="32px"></asp:TextBox>&nbsp;&nbsp;
                                                <asp:Button ID="btnKensakuSeikyuuSaki5" runat="server" Text="検索" />&nbsp;&nbsp;
                                                <asp:TextBox ID="tbxSeikyuuSakiMei5" runat="server" CssClass="readOnlyStyle" TabIndex ="-1" Width="280px"></asp:TextBox></td>
                                        </tr>
                                        <tr align="left">
                                            <td class="koumokuMei" style ="width :70px;" >
                                                請求先&nbsp;6</td>
                                            <td colspan ="4" >
                                                <asp:DropDownList ID="ddlSeikyuuSaki6" runat="server" Width ="100px">
                                                </asp:DropDownList>&nbsp;&nbsp;
                                                <asp:TextBox ID="tbxSeikyuuSakiCd6" runat="server"  MaxLength ="5" style="ime-mode:disabled;" Width="64px"></asp:TextBox>&nbsp;&mdash;
                                                <asp:TextBox ID="tbxSeikyuuSakiBrc6" runat="server"  MaxLength ="2" style="ime-mode:disabled;" Width="32px"></asp:TextBox>&nbsp;&nbsp;
                                                <asp:Button ID="btnKensakuSeikyuuSaki6" runat="server" Text="検索" />&nbsp;&nbsp;
                                                <asp:TextBox ID="tbxSeikyuuSakiMei6" runat="server" CssClass="readOnlyStyle" TabIndex ="-1" Width="280px"></asp:TextBox></td>
                                        </tr>
                                        <tr align="left">
                                            <td class="koumokuMei" style ="width :70px;" >
                                                請求先&nbsp;7</td>
                                            <td colspan ="4" >
                                                <asp:DropDownList ID="ddlSeikyuuSaki7" runat="server" Width ="100px">
                                                </asp:DropDownList>&nbsp;&nbsp;
                                                <asp:TextBox ID="tbxSeikyuuSakiCd7" runat="server"  MaxLength ="5" style="ime-mode:disabled;" Width="64px"></asp:TextBox>&nbsp;&mdash;
                                                <asp:TextBox ID="tbxSeikyuuSakiBrc7" runat="server"  MaxLength ="2" style="ime-mode:disabled;" Width="32px"></asp:TextBox>&nbsp;&nbsp;
                                                <asp:Button ID="btnKensakuSeikyuuSaki7" runat="server" Text="検索" />&nbsp;&nbsp;
                                                <asp:TextBox ID="tbxSeikyuuSakiMei7" runat="server" CssClass="readOnlyStyle" TabIndex ="-1" Width="280px"></asp:TextBox></td>
                                        </tr>
                                        <tr align="left">
                                            <td class="koumokuMei" style ="width :70px;" >
                                                請求先&nbsp;8</td>
                                            <td colspan ="4" >
                                                <asp:DropDownList ID="ddlSeikyuuSaki8" runat="server" Width ="100px">
                                                </asp:DropDownList>&nbsp;&nbsp;
                                                <asp:TextBox ID="tbxSeikyuuSakiCd8" runat="server"  MaxLength ="5" style="ime-mode:disabled;" Width="64px"></asp:TextBox>&nbsp;&mdash;
                                                <asp:TextBox ID="tbxSeikyuuSakiBrc8" runat="server"  MaxLength ="2" style="ime-mode:disabled;" Width="32px"></asp:TextBox>&nbsp;&nbsp;
                                                <asp:Button ID="btnKensakuSeikyuuSaki8" runat="server" Text="検索" />&nbsp;&nbsp;
                                                <asp:TextBox ID="tbxSeikyuuSakiMei8" runat="server" CssClass="readOnlyStyle" TabIndex ="-1" Width="280px"></asp:TextBox></td>
                                        </tr>
                                        <tr align="left">
                                            <td class="koumokuMei" style ="width :70px;" >
                                                請求先&nbsp;9</td>
                                            <td colspan ="4" >
                                                <asp:DropDownList ID="ddlSeikyuuSaki9" runat="server" Width ="100px">
                                                </asp:DropDownList>&nbsp;&nbsp;
                                                <asp:TextBox ID="tbxSeikyuuSakiCd9" runat="server"  MaxLength ="5" style="ime-mode:disabled;" Width="64px"></asp:TextBox>&nbsp;&mdash;
                                                <asp:TextBox ID="tbxSeikyuuSakiBrc9" runat="server"  MaxLength ="2" style="ime-mode:disabled;" Width="32px"></asp:TextBox>&nbsp;&nbsp;
                                                <asp:Button ID="btnKensakuSeikyuuSaki9" runat="server" Text="検索" />&nbsp;&nbsp;
                                                <asp:TextBox ID="tbxSeikyuuSakiMei9" runat="server" CssClass="readOnlyStyle" TabIndex ="-1" Width="280px"></asp:TextBox></td>
                                        </tr>
                                        <tr align="left">
                                            <td class="koumokuMei" style ="width :70px;" >
                                                請求先&nbsp;10</td>
                                            <td colspan ="4" >
                                                <asp:DropDownList ID="ddlSeikyuuSaki10" runat="server" Width ="100px">
                                                </asp:DropDownList>&nbsp;&nbsp;
                                                <asp:TextBox ID="tbxSeikyuuSakiCd10" runat="server"  MaxLength ="5" style="ime-mode:disabled;" Width="64px"></asp:TextBox>&nbsp;&mdash;
                                                <asp:TextBox ID="tbxSeikyuuSakiBrc10" runat="server"  MaxLength ="2" style="ime-mode:disabled;" Width="32px"></asp:TextBox>&nbsp;&nbsp;
                                                <asp:Button ID="btnKensakuSeikyuuSaki10" runat="server" Text="検索" />&nbsp;&nbsp;
                                                <asp:TextBox ID="tbxSeikyuuSakiMei10" runat="server" CssClass="readOnlyStyle" TabIndex ="-1" Width="280px"></asp:TextBox></td>
                                        </tr>
                                        <tr align="left">
                                            <td class="koumokuMei" style ="width :70px;" >
                                                請求先&nbsp;11</td>
                                            <td colspan ="4" >
                                                <asp:DropDownList ID="ddlSeikyuuSaki11" runat="server" Width ="100px">
                                                </asp:DropDownList>&nbsp;&nbsp;
                                                <asp:TextBox ID="tbxSeikyuuSakiCd11" runat="server"  MaxLength ="5" style="ime-mode:disabled;" Width="64px"></asp:TextBox>&nbsp;&mdash;
                                                <asp:TextBox ID="tbxSeikyuuSakiBrc11" runat="server"  MaxLength ="2" style="ime-mode:disabled;" Width="32px"></asp:TextBox>&nbsp;&nbsp;
                                                <asp:Button ID="btnKensakuSeikyuuSaki11" runat="server" Text="検索" />&nbsp;&nbsp;
                                                <asp:TextBox ID="tbxSeikyuuSakiMei11" runat="server" CssClass="readOnlyStyle" TabIndex ="-1" Width="280px"></asp:TextBox></td>
                                        </tr>
                                        <tr align="left">
                                            <td class="koumokuMei" style ="width :70px;" >
                                                請求先&nbsp;12</td>
                                            <td colspan ="4" >
                                                <asp:DropDownList ID="ddlSeikyuuSaki12" runat="server" Width ="100px">
                                                </asp:DropDownList>&nbsp;&nbsp;
                                                <asp:TextBox ID="tbxSeikyuuSakiCd12" runat="server"  MaxLength ="5" style="ime-mode:disabled;" Width="64px"></asp:TextBox>&nbsp;&mdash;
                                                <asp:TextBox ID="tbxSeikyuuSakiBrc12" runat="server"  MaxLength ="2" style="ime-mode:disabled;" Width="32px"></asp:TextBox>&nbsp;&nbsp;
                                                <asp:Button ID="btnKensakuSeikyuuSaki12" runat="server" Text="検索" />&nbsp;&nbsp;
                                                <asp:TextBox ID="tbxSeikyuuSakiMei12" runat="server" CssClass="readOnlyStyle" TabIndex ="-1" Width="280px"></asp:TextBox></td>
                                        </tr>
                                        <tr align="left">
                                            <td class="koumokuMei" style ="width :70px;" >
                                                請求先&nbsp;13</td>
                                            <td colspan ="4" >
                                                <asp:DropDownList ID="ddlSeikyuuSaki13" runat="server" Width ="100px">
                                                </asp:DropDownList>&nbsp;&nbsp;
                                                <asp:TextBox ID="tbxSeikyuuSakiCd13" runat="server"  MaxLength ="5" style="ime-mode:disabled;" Width="64px"></asp:TextBox>&nbsp;&mdash;
                                                <asp:TextBox ID="tbxSeikyuuSakiBrc13" runat="server"  MaxLength ="2" style="ime-mode:disabled;" Width="32px"></asp:TextBox>&nbsp;&nbsp;
                                                <asp:Button ID="btnKensakuSeikyuuSaki13" runat="server" Text="検索" />&nbsp;&nbsp;
                                                <asp:TextBox ID="tbxSeikyuuSakiMei13" runat="server" CssClass="readOnlyStyle" TabIndex ="-1" Width="280px"></asp:TextBox></td>
                                        </tr>
                                        <tr align="left">
                                            <td class="koumokuMei" style ="width :70px;" >
                                                請求先&nbsp;14</td>
                                            <td colspan ="4" >
                                                <asp:DropDownList ID="ddlSeikyuuSaki14" runat="server" Width ="100px">
                                                </asp:DropDownList>&nbsp;&nbsp;
                                                <asp:TextBox ID="tbxSeikyuuSakiCd14" runat="server"  MaxLength ="5" style="ime-mode:disabled;" Width="64px"></asp:TextBox>&nbsp;&mdash;
                                                <asp:TextBox ID="tbxSeikyuuSakiBrc14" runat="server"  MaxLength ="2" style="ime-mode:disabled;" Width="32px"></asp:TextBox>&nbsp;&nbsp;
                                                <asp:Button ID="btnKensakuSeikyuuSaki14" runat="server" Text="検索" />&nbsp;&nbsp;
                                                <asp:TextBox ID="tbxSeikyuuSakiMei14" runat="server" CssClass="readOnlyStyle" TabIndex ="-1" Width="280px"></asp:TextBox></td>
                                        </tr>
                                        <tr align="left">
                                            <td class="koumokuMei" style ="width :70px;" >
                                                請求先&nbsp;15</td>
                                            <td colspan ="4" >
                                                <asp:DropDownList ID="ddlSeikyuuSaki15" runat="server" Width ="100px">
                                                </asp:DropDownList>&nbsp;&nbsp;
                                                <asp:TextBox ID="tbxSeikyuuSakiCd15" runat="server"  MaxLength ="5" style="ime-mode:disabled;" Width="64px"></asp:TextBox>&nbsp;&mdash;
                                                <asp:TextBox ID="tbxSeikyuuSakiBrc15" runat="server"  MaxLength ="2" style="ime-mode:disabled;" Width="32px"></asp:TextBox>&nbsp;&nbsp;
                                                <asp:Button ID="btnKensakuSeikyuuSaki15" runat="server" Text="検索" />&nbsp;&nbsp;
                                                <asp:TextBox ID="tbxSeikyuuSakiMei15" runat="server" CssClass="readOnlyStyle" TabIndex ="-1" Width="280px"></asp:TextBox></td>
                                        </tr>
                                        <tr align="left">
                                            <td class="koumokuMei" style ="width :70px;" >
                                                請求先&nbsp;16</td>
                                            <td colspan ="4" >
                                                <asp:DropDownList ID="ddlSeikyuuSaki16" runat="server" Width ="100px">
                                                </asp:DropDownList>&nbsp;&nbsp;
                                                <asp:TextBox ID="tbxSeikyuuSakiCd16" runat="server"  MaxLength ="5" style="ime-mode:disabled;" Width="64px"></asp:TextBox>&nbsp;&mdash;
                                                <asp:TextBox ID="tbxSeikyuuSakiBrc16" runat="server"  MaxLength ="2" style="ime-mode:disabled;" Width="32px"></asp:TextBox>&nbsp;&nbsp;
                                                <asp:Button ID="btnKensakuSeikyuuSaki16" runat="server" Text="検索" />&nbsp;&nbsp;
                                                <asp:TextBox ID="tbxSeikyuuSakiMei16" runat="server" CssClass="readOnlyStyle" TabIndex ="-1" Width="280px"></asp:TextBox></td>
                                        </tr>
                                        <tr align="left">
                                            <td class="koumokuMei" style ="width :70px;" >
                                                請求先&nbsp;17</td>
                                            <td colspan ="4" >
                                                <asp:DropDownList ID="ddlSeikyuuSaki17" runat="server" Width ="100px">
                                                </asp:DropDownList>&nbsp;&nbsp;
                                                <asp:TextBox ID="tbxSeikyuuSakiCd17" runat="server"  MaxLength ="5" style="ime-mode:disabled;" Width="64px"></asp:TextBox>&nbsp;&mdash;
                                                <asp:TextBox ID="tbxSeikyuuSakiBrc17" runat="server"  MaxLength ="2" style="ime-mode:disabled;" Width="32px"></asp:TextBox>&nbsp;&nbsp;
                                                <asp:Button ID="btnKensakuSeikyuuSaki17" runat="server" Text="検索" />&nbsp;&nbsp;
                                                <asp:TextBox ID="tbxSeikyuuSakiMei17" runat="server" CssClass="readOnlyStyle" TabIndex ="-1" Width="280px"></asp:TextBox></td>
                                        </tr>
                                        <tr align="left">
                                            <td class="koumokuMei" style ="width :70px;" >
                                                請求先&nbsp;18</td>
                                            <td colspan ="4" >
                                                <asp:DropDownList ID="ddlSeikyuuSaki18" runat="server" Width ="100px">
                                                </asp:DropDownList>&nbsp;&nbsp;
                                                <asp:TextBox ID="tbxSeikyuuSakiCd18" runat="server"  MaxLength ="5" style="ime-mode:disabled;" Width="64px"></asp:TextBox>&nbsp;&mdash;
                                                <asp:TextBox ID="tbxSeikyuuSakiBrc18" runat="server"  MaxLength ="2" style="ime-mode:disabled;" Width="32px"></asp:TextBox>&nbsp;&nbsp;
                                                <asp:Button ID="btnKensakuSeikyuuSaki18" runat="server" Text="検索" />&nbsp;&nbsp;
                                                <asp:TextBox ID="tbxSeikyuuSakiMei18" runat="server" CssClass="readOnlyStyle" TabIndex ="-1" Width="280px"></asp:TextBox></td>
                                        </tr>
                                        <tr align="left">
                                            <td class="koumokuMei" style ="width :70px;" >
                                                請求先&nbsp;19</td>
                                            <td colspan ="4" >
                                                <asp:DropDownList ID="ddlSeikyuuSaki19" runat="server" Width ="100px">
                                                </asp:DropDownList>&nbsp;&nbsp;
                                                <asp:TextBox ID="tbxSeikyuuSakiCd19" runat="server"  MaxLength ="5" style="ime-mode:disabled;" Width="64px"></asp:TextBox>&nbsp;&mdash;
                                                <asp:TextBox ID="tbxSeikyuuSakiBrc19" runat="server"  MaxLength ="2" style="ime-mode:disabled;" Width="32px"></asp:TextBox>&nbsp;&nbsp;
                                                <asp:Button ID="btnKensakuSeikyuuSaki19" runat="server" Text="検索" />&nbsp;&nbsp;
                                                <asp:TextBox ID="tbxSeikyuuSakiMei19" runat="server" CssClass="readOnlyStyle" TabIndex ="-1" Width="280px"></asp:TextBox></td>
                                        </tr>
                                        <tr align="left">
                                            <td class="koumokuMei" style ="width :70px;" >
                                                請求先&nbsp;20</td>
                                            <td colspan ="4" >
                                                <asp:DropDownList ID="ddlSeikyuuSaki20" runat="server" Width ="100px">
                                                </asp:DropDownList>&nbsp;&nbsp;
                                                <asp:TextBox ID="tbxSeikyuuSakiCd20" runat="server"  MaxLength ="5" style="ime-mode:disabled;" Width="64px"></asp:TextBox>&nbsp;&mdash;
                                                <asp:TextBox ID="tbxSeikyuuSakiBrc20" runat="server"  MaxLength ="2" style="ime-mode:disabled;" Width="32px"></asp:TextBox>&nbsp;&nbsp;
                                                <asp:Button ID="btnKensakuSeikyuuSaki20" runat="server" Text="検索" />&nbsp;&nbsp;
                                                <asp:TextBox ID="tbxSeikyuuSakiMei20" runat="server" CssClass="readOnlyStyle" TabIndex ="-1" Width="280px"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>             
                        </tbody>
                    </table>
                    <table runat ="server" id="tblSeikyuuSaki" visible ="false"  cellpadding="2" class="mainTable" style="text-align: left;">
                        <tbody>
                             <tr align="left">
                                 <td class="koumokuMei" style ="width :70px;background-color:#ccffff;" >
                                     請求先</td>
                                 <td colspan ="4" >
                                     <asp:DropDownList ID="ddlSeikyuuSaki" runat="server" Width ="100px">
                                     </asp:DropDownList>&nbsp;&nbsp;
                                     <asp:TextBox ID="tbxSeikyuuSakiCd" runat="server"  MaxLength ="5" style="ime-mode:disabled;" Width="64px"></asp:TextBox>&nbsp;&mdash;
                                     <asp:TextBox ID="tbxSeikyuuSakiBrc" runat="server"  MaxLength ="2" style="ime-mode:disabled;" Width="32px"></asp:TextBox>&nbsp;&nbsp;
                                     <asp:Button ID="btnKensakuSeikyuuSaki" runat="server" Text="検索" />&nbsp;&nbsp;
                                     <asp:TextBox ID="tbxSeikyuuSakiMei" runat="server" CssClass="readOnlyStyle" TabIndex ="-1" Width="280px"></asp:TextBox></td>
                             </tr>
                        </tbody>
                    </table>
                    <table runat ="server" id="tblSiireSaki" visible ="false"  cellpadding="2" class="mainTable" style="text-align: left;">
                        <tbody>
                            <tr align="left">
                                <td class="koumokuMei" style ="width :70px;background-color:#ccffff;" >
                                    仕入先</td>
                                <td colspan ="4" >
                                    <asp:TextBox ID="tbxSiireCd" runat="server"  MaxLength ="7" style="ime-mode:disabled;" Width="100px"></asp:TextBox>&nbsp;&nbsp;
                                    <asp:Button ID="btnKensakuSiire" runat="server" Text="検索" />&nbsp;&nbsp;
                                    <asp:TextBox ID="tbxSiireMei" runat="server" CssClass="readOnlyStyle" TabIndex ="-1"  Width="406px"></asp:TextBox></td>
                            </tr>
                        </tbody>
                    </table>
                    <table runat ="server" id="tblSiharaiSaki" visible ="false"  cellpadding="2" class="mainTable" style="text-align: left;">
                        <tbody>
                            <tr align="left">
                                <td class="koumokuMei" style ="width :70px;background-color:#ccffff;" >
                                    支払先</td>
                                <td colspan ="4" >
                                    <asp:TextBox ID="tbxSiharaiSakiCd" runat="server"  MaxLength ="7" style="ime-mode:disabled;" Width="100px"></asp:TextBox>&nbsp;&nbsp;
                                    <asp:Button ID="btnKensakuSiharai" runat="server" Text="検索" />&nbsp;&nbsp;
                                    <asp:TextBox ID="tbxSiharaiMei" runat="server" CssClass="readOnlyStyle" TabIndex ="-1"  Width="406px"></asp:TextBox></td>
                            </tr>
                        </tbody>
                    </table>
                    
                    <table runat ="server" id="tblH" visible ="false" ><tr><td style ="height :15px;"></td></tr></table>
                </td>
            </tr>
            
            <tr>
                <td  style ="height :30px;" ></td>
                <td  align="left" ><asp:Button ID="btnDataSyuturyoku" runat="server" Text="データ出力" Height="28px" Width="200px" Font-Size="10pt" Font-Bold="true" /><asp:Button ID="btnHyouji" runat="server" Visible ="false"  Text="表示" Height="28px" Width="200px" Font-Size="10pt" Font-Bold="true" /></td>
                <td colspan ="3"></td>
            </tr>
        </table>
        
        <asp:HiddenField ID="hidSeikyuuStatus" runat="server"/>
        <asp:HiddenField ID="hidConfirm" runat="server" />
        <asp:HiddenField ID="HiddenTysKensakuType" runat="server" />
        <asp:HiddenField ID="HiddenTysKensakuType1" runat="server" />
        <asp:HiddenField ID="tbxSiireBrc" runat="server" />
        <asp:HiddenField ID="HiddenKameitenCd" runat="server" />
    </ContentTemplate> 
</asp:UpdatePanel>
</asp:Content>

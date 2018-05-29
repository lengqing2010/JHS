<%@ Page Language="vb" AutoEventWireup="false" Codebehind="TyousaMitumorisyoSakuseiInquiry.aspx.vb"
    Inherits="Itis.Earth.WebUI.TyousaMitumorisyoSakuseiInquiry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>調査見積書作成指示</title>
    <link rel="stylesheet" href="css/jhsearth.css" type="text/css" />

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 550px;">
                <tr>
                    <td>
                        <asp:Label ID="lblTitle" runat="server" Text="調査見積書作成指示" Font-Bold="true" Font-Size="16px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="margin-top: 5px;">
                            <tr>
                                <td>
                                    <asp:Button ID="btnCloseWin" runat="server" Text="閉じる" Style="padding-top: 2px;"
                                        Height="23px" Width="50px" />
                                </td>
                                <td style="width: 10px;">
                                </td>
                                <td>
                                    <asp:Button ID="clearWin" runat="server" Text="クリア" Style="padding-top: 2px;" Height="23px"
                                        Width="50px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 520px; background-color: #e6e6e6;
                            margin-top: 15px; margin-left: 10px;">
                            <tr style="height: 25px;">
                                <td style="width: 65px; border: solid 1px black; font-weight: bold; text-align: center;
                                    background-color: #ccffff;">
                                    区分
                                </td>
                                <td style="width: 60px; border-top: solid 1px black; border-bottom: solid 1px black;">
                                    &nbsp;<asp:Label ID="lblKbn" runat="server" Font-Size="15px"></asp:Label>
                                </td>
                                <td style="width: 85px; border: solid 1px black; font-weight: bold; text-align: center;
                                    background-color: #ccffff;">
                                    物件番号
                                </td>
                                <td style="width: 125px; border-top: solid 1px black; border-bottom: solid 1px black;">
                                    &nbsp;<asp:Label ID="lblBukkenNo" runat="server" Font-Size="15px"></asp:Label>
                                </td>
                                <td style="width: 130px; border: solid 1px black; font-weight: bold; text-align: center;
                                    background-color: #ccffff;">
                                    見積書作成回数
                                </td>
                                <td style="border-top: solid 1px black; border-bottom: solid 1px black; border-right: solid 1px black;">
                                    &nbsp;<asp:Label ID="lblMitumorisyoSakuseiKaisuu" runat="server" Font-Size="15px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 471px; background-color: #e6e6e6;
                            text-align: center; margin-top: -1px; margin-left: 10px;">
                            <tr style="height: 25px;">
                                <td style="width: 130px; border: solid 1px black; font-weight: bold; background-color: #ccffff;
                                    text-align: left; padding-left: 20px;">
                                    消費税表示 選択
                                </td>
                                <td style="border-top: solid 1px black; border-bottom: solid 1px black; border-right: solid 1px black;
                                    width: 140px;">
                                    <asp:RadioButton ID="rdoZeinu" runat="server" Text="する" TextAlign="Left" GroupName="zei"
                                        Checked="true" Style="font-weight: bold;" /><!--税抜き-->
                                </td>
                                <td style="border-top: solid 1px black; border-bottom: solid 1px black; border-right: solid 1px black;">
                                    <asp:RadioButton ID="rdoZeikomi" runat="server" Text="しない" TextAlign="Left" GroupName="zei"
                                        Style="font-weight: bold;" /><!--税込み-->
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 471px; background-color: #e6e6e6;
                            text-align: center; margin-top: -1px; margin-left: 10px;">
                            <tr style="height: 25px;">
                                <td style="width: 130px; border: solid 1px black; font-weight: bold; background-color: #ccffff;
                                    text-align: left; padding-left: 20px;">
                                    モール展開 選択
                                </td>
                                <td style="border-top: solid 1px black; border-bottom: solid 1px black; border-right: solid 1px black;
                                    width: 140px;">
                                    <asp:RadioButton ID="rdoSuru" runat="server" Text="する" TextAlign="Left" GroupName="hyouji"
                                        Style="font-weight: bold;" />
                                </td>
                                <td style="border-top: solid 1px black; border-bottom: solid 1px black; border-right: solid 1px black;">
                                    <asp:RadioButton ID="rdoSinai" runat="server" Text="しない" TextAlign="Left" GroupName="hyouji"
                                        Checked="true" Style="font-weight: bold;" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 380px; background-color: #e6e6e6;
                            margin-left: 10px; margin-top: 15px;">
                            <tr>
                                <td style="width: 130px; border: solid 1px black; font-weight: bold; background-color: #ccffff;
                                    text-align: left; padding-left: 20px;">
                                    見積書作成日
                                </td>
                                <td style="border-top: solid 1px black; border-bottom: solid 1px black; border-right: solid 1px black;">
                                    <asp:TextBox ID="tbxSakuseiDate" runat="server" MaxLength="10" CssClass="codeNumber" Style="width: 100px;
                                        margin-left: 20px;"></asp:TextBox>
                                    <asp:Button ID="btnToday" runat="server" Text="当日" Style="width: 40px; margin-left: 10px;
                                        padding-top: 2px;" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="height: 14px; width: 520px; text-align: left; font-weight: bold; padding-top: 15px;">
                        &nbsp;&nbsp;※ 依頼担当者の入力は任意となります
                    </td>
                </tr>
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 380px; background-color: #e6e6e6;
                            margin-left: 10px; margin-top: 2px;">
                            <tr>
                                <td style="width: 130px; border: solid 1px black; font-weight: bold; background-color: #ccffff;
                                    text-align: left; padding-left: 20px;">
                                    依頼担当者 入力
                                </td>
                                <td style="border-top: solid 1px black; border-bottom: solid 1px black; border-right: solid 1px black;">
                                    <asp:TextBox ID="tbxIraiTantousya" runat="server" MaxLength="20" Style="width: 150px;
                                        margin-left: 20px;"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="height: 14px; width: 520px; text-align: left; font-weight: bold; padding-top: 15px;">
                        &nbsp;&nbsp;※ 帳票に表示する住所に該当する支店・部署を選択して下さい
                    </td>
                    <%--<td>
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 520px; margin-top: 5px;
                            margin-left: 10px; text-align: left; font-weight: bold;">
                            <tr>
                                <td style="height: 14px">
                                    ※ 帳票に表示する住所に該当する支店・部署を選択して下さい
                                </td>
                            </tr>
                        </table>
                    </td>--%>
                </tr>
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 380px; background-color: #e6e6e6;
                            margin-left: 10px; margin-top: 2px;">
                            <tr>
                                <td style="width: 130px; border: solid 1px black; font-weight: bold; background-color: #ccffff;
                                    text-align: left; padding-left: 20px;">
                                    表示住所 選択
                                </td>
                                <td style="border-top: solid 1px black; border-bottom: solid 1px black; border-right: solid 1px black;">
                                    <asp:DropDownList ID="ddlHyoujiJyuusyo" runat="server" Style="width: 150px; margin-left: 20px;">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="height: 14px; width: 520px; text-align: left; font-weight: bold; padding-top: 15px;">
                        &nbsp;&nbsp;※ 承認者を選択して下さい
                    </td>
                    <%--<td>
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 520px; margin-top: 5px;
                            margin-left: 10px; text-align: left; font-weight: bold;">
                            <tr>
                                <td style="height: 14px">
                                    ※ 承認者を選択して下さい
                                </td>
                            </tr>
                        </table>
                    </td>--%>
                </tr>
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 380px; background-color: #e6e6e6;
                            margin-left: 10px; margin-top: 2px;">
                            <tr>
                                <td style="width: 130px; border: solid 1px black; font-weight: bold; background-color: #ccffff;
                                    text-align: left; padding-left: 20px;">
                                    承認者 選択
                                </td>
                                <td style="border-top: solid 1px black; border-bottom: solid 1px black; border-right: solid 1px black;">
                                    <asp:DropDownList ID="ddlSyouniSya" runat="server" Style="width: 150px; margin-left: 20px;">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <asp:Button ID="btnMitumorisyoSakusei" runat="server" Text="見積書作成" Style="padding-top: 2px;
                            margin-top: 20px;" Height="23px" Width="80px" />
                    </td>
                </tr>
            </table>
            <asp:HiddenField runat="server" ID="hidMoru" />
        </div>
    </form>
</body>
</html>

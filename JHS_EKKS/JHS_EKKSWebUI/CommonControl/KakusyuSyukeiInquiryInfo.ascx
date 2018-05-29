<%@ Control Language="VB" AutoEventWireup="false" CodeFile="KakusyuSyukeiInquiryInfo.ascx.vb"
    Inherits="CommonControl_KakusyuSyukeiInquiryInfo" %>
<link rel="stylesheet" href="../App_Themes/CSS/JHS_EKKS.css" type="text/css" />
<table border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 80px; border: solid 1px black; border-right: none; background-color: #FECC99;">
            <asp:Label ID="lblTitle" runat="server" style="word-break:break-all;"></asp:Label>
        </td>
        <td style="border: solid 1px black; border-right: none; vertical-align: top; background-color: #99CCFE;">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 70px;">
                <tr style="height: 19px;">
                    <td>
                        工事判定率
                    </td>
                </tr>
                <tr style="height: 19px;">
                    <td style="text-align: right;">
                        <asp:Label ID="lblRitu1" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr style="height: 18px;">
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr style="height: 19px;">
                    <td style="border-top: solid 1px black;">
                        工事受注率
                    </td>
                </tr>
                <tr style="height: 19px;">
                    <td style="text-align: right;">
                        <asp:Label ID="lblRitu2" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr style="height: 18px;">
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr style="height: 19px;">
                    <td style="border-top: solid 1px black;">
                        直工事率
                    </td>
                </tr>
                <tr style="height: 19px;">
                    <td style="text-align: right;">
                        <asp:Label ID="lblRitu3" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
        <td style="vertical-align: top;">
            <asp:GridView ID="grdSyouhinInfo" runat="server" AutoGenerateColumns="False" ShowHeader="False"
                BackColor="#99CCFE" Style="width: 330px; border: solid 1px black; font-size: 12px;">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="lblBunbetu" runat="server" Text='<%#Eval("meisyou")%>' Width="45px"
                                CssClass="TextOverflow"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="50px" Height="16px" BorderColor="black" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            &nbsp;<asp:Label ID="lblSyouhinMei" runat="server" Text='<%#Eval("syouhin_mei")%>'
                                Width="180px" CssClass="TextOverflow"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="185px" Height="16px" BorderColor="black" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="lblTanka" runat="server" Width="85px" Text='<%#Eval("zennenHeikentanka")%>'
                                CssClass="TextOverflow"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Height="16px" BorderColor="black" HorizontalAlign="Right" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>

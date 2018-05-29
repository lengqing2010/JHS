<%@ Control Language="VB" AutoEventWireup="false" CodeFile="KeikaikuKanriKameitenBikouInquiry.ascx.vb"
    Inherits="CommonControl_KeikaikuKanriKameitenBikouInquiry" %>
<table border="0" cellspacing="0" cellpadding="1" style="width: 960px; border: solid 2px gray;
    font-size: 13px; text-align: left;">
    <tr>
        <th style="background-color: #ccffff; height: 26px;">
            <asp:LinkButton ID="lnkTitle" runat="server">計画管理用_加盟店備考情報</asp:LinkButton>
        </th>
    </tr>
</table>
<div id="divNaiyou" runat="server" style="margin-top: -3px;">
    <table border="0" cellspacing="0" cellpadding="1" style="width: 960px; border: solid 2px gray;
        font-size: 13px; text-align: left; border-top-width: 1px;">
        <tr>
            <td style="background-color: #e6e6e6;">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <table class="TableBorder" border="0" cellpadding="0" cellspacing="0" style="margin-top: 10px;
                                margin-left: 10px; font-weight: bold; text-align: center; padding: 1px 1px; height: 24px;">
                                <tr style="background-color: #ffffd9;">
                                    <td class="TdBorder" style="width: 120px;">
                                        種別
                                    </td>
                                    <td class="TdBorder" style="width: 260px">
                                        種別名</td>
                                    <td class="TdBorder" style="width: 393px">
                                        内容</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="divMeisai" runat="server" style="width: 800px; height: 244px; overflow: auto;
                                margin-bottom: 10px; margin-left: 10px; margin-top: -1px;" onscroll="fncSaveBikouScroll();">
                                <asp:GridView ID="grdBeikou" runat="server" AutoGenerateColumns="False" BackColor="White"
                                    CssClass="gvTableBorder" ShowHeader="False" Style="border-color: Black; padding-left: 3px;">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:TextBox ID="tbxBikousyubetu" runat="server" CssClass="codeNumber" Text='<%# eval("bikou_syubetu") %>'
                                                    Width="46px" AutoPostBack="true" OnTextChanged="grdBikousyubetuText_TextChanged"></asp:TextBox>
                                                <asp:Button ID="btnKensaku1" runat="server" Text="検索" Height="23px" />
                                                <asp:HiddenField ID="hidNyuuryokuNo" runat="server" Value='<%# eval("nyuuryoku_no") %>' />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="gvMeisaiBorder hissu" BorderColor="black" HorizontalAlign="Left"
                                                Width="120px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:TextBox ID="tbxBikousyubetuMei" runat="server" Text='<%# eval("meisyou") %>'
                                                    CssClass="readOnlyStyle" TabIndex="-1" Width="240" Style="border-bottom: none;"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="gvMeisaiBorder" BorderColor="black" HorizontalAlign="Left" Width="260px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:TextBox ID="tbxNaiyou" runat="server" Text='<%# eval("naiyou") %>' Width="250px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="gvMeisaiBorder" BorderColor="black" HorizontalAlign="Left" Width="270px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnTouroku" runat="server" Text="登録" Width="50px" Height="23px" OnClick="UpdBikou" />
                                                <asp:Button ID="btnSakujyo" runat="server" Text="削除" Width="50px" Height="23px" OnClick="DelBikou"
                                                    OnClientClick="if (!confirm('削除します。')){return false;}" />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="gvMeisaiBorder" BorderColor="black" HorizontalAlign="Left" Width="120px" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <table id="tableNewRow" runat="server" class="TableBorder" border="0" cellpadding="1"
                                    cellspacing="0" style="background-color: White; margin-top: -1px;">
                                    <tr>
                                        <td class="TdBorder hissu" style="width: 120px;">
                                            <asp:TextBox ID="tbxBikousyubetu" runat="server" CssClass="codeNumber" Width="46px"
                                                MaxLength="4" AutoPostBack="true"></asp:TextBox>
                                            <asp:Button ID="btnKensaku" runat="server" Text="検索" Height="23px" />
                                        </td>
                                        <td class="TdBorder" style="width: 260px;">
                                            <asp:TextBox ID="lblSyubetumei" runat="server" CssClass="readOnlyStyle" TabIndex="-1"
                                                Width="240" Style="border-bottom: none;"></asp:TextBox>
                                        </td>
                                        <td class="TdBorder" style="width: 270px;">
                                            <asp:TextBox ID="tbxNaiyou" runat="server" Width="250px"></asp:TextBox>
                                        </td>
                                        <td class="TdBorder" style="width: 120px;">
                                            <asp:Button ID="btnXinki" runat="server" Text="新規" Width="50px" Height="23px" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
<asp:HiddenField runat="server" ID="hidScroll" />
<asp:HiddenField ID="hidMaxDate" runat="server" />

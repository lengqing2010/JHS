<%@ Control Language="VB" AutoEventWireup="false" CodeFile="KakusyuSyukeiInquiryData.ascx.vb"
    Inherits="CommonControl_KakusyuSyukeiInquiryData" %>
<link rel="stylesheet" href="../App_Themes/CSS/JHS_EKKS.css" type="text/css" />
<asp:GridView ID="grdSyouhinData" runat="server" AutoGenerateColumns="False" ShowHeader="False"
    BackColor="#FEFF8F" Style="width: 990px; border: solid 1px black;">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Label ID="lblKeikakuKensuSyukei" CssClass="TextOverflow" runat="server" Text='<%#Eval("keikakuKensuSyukei")%>'
                    Width="118px"></asp:Label>
            </ItemTemplate>
            <ItemStyle Width="120px" Height="16px" BorderColor="black" HorizontalAlign="Right" />
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Label ID="lblJiltusekiKensuSyukei" CssClass="TextOverflow" runat="server" Text='<%#Eval("jiltusekiKensuSyukei")%>'
                    Width="118px"></asp:Label>
            </ItemTemplate>
            <ItemStyle Width="120px" Height="16px" BorderColor="black" HorizontalAlign="Right" />
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Label ID="lblData3" runat="server" Text='<%#Eval("data3")%>' CssClass="TextOverflow"
                    Width="58px"></asp:Label>
            </ItemTemplate>
            <ItemStyle Width="60px" Height="16px" BorderColor="black" HorizontalAlign="Right" />
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Label ID="lblKeikakuKingakuSyukei" CssClass="TextOverflow" runat="server" Text='<%#Eval("keikakuKingakuSyukei")%>'
                    Width="118px"></asp:Label>
            </ItemTemplate>
            <ItemStyle Width="120px" Height="16px" BorderColor="black" HorizontalAlign="Right" />
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Label ID="lblJiltusekiKingakuSyukei" CssClass="TextOverflow" runat="server"
                    Text='<%#Eval("jiltusekiKingakuSyukei")%>' Width="118px"></asp:Label>
            </ItemTemplate>
            <ItemStyle Width="120px" Height="16px" BorderColor="black" HorizontalAlign="Right" />
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Label ID="lblData6" runat="server" Text='<%#Eval("data6")%>' CssClass="TextOverflow"
                    Width="58px"></asp:Label>
            </ItemTemplate>
            <ItemStyle Width="60px" Height="16px" BorderColor="black" HorizontalAlign="Right" />
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Label ID="lblKeikakuSoriSyukei" CssClass="TextOverflow" runat="server" Text='<%#Eval("keikakuSoriSyukei")%>'
                    Width="118px"></asp:Label>
            </ItemTemplate>
            <ItemStyle Width="120px" Height="16px" BorderColor="black" HorizontalAlign="Right" />
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Label ID="lblJiltusekiSoriSyukei" CssClass="TextOverflow" runat="server" Text='<%#Eval("jiltusekiSoriSyukei")%>'
                    Width="118px"></asp:Label>
            </ItemTemplate>
            <ItemStyle Width="120px" Height="16px" BorderColor="black" HorizontalAlign="Right" />
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Label ID="lblData9" runat="server" Text='<%#Eval("data9")%>' CssClass="TextOverflow"
                    Width="58px"></asp:Label>
            </ItemTemplate>
            <ItemStyle Width="60px" Height="16px" BorderColor="black" HorizontalAlign="Right" />
        </asp:TemplateField>
    </Columns>
</asp:GridView>

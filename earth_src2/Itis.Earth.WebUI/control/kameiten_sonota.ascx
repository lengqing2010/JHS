<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="kameiten_sonota.ascx.vb" Inherits="Itis.Earth.WebUI.kameiten_sonota" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table cellpadding="1" class="mainTable" style="margin-top: 10px; width: 968px; text-align: left">
            <thead>
                <tr>
                    <th class="tableTitle" colspan="12" rowspan="1" style="text-align: left">
                             <asp:LinkButton id="lbtnSonota" runat="server" OnClick="lbtnSonota_Click" >ÇªÇÃëº</asp:LinkButton> &nbsp; 
                                                       <asp:Button ID="btnTouroku" runat="server" Text="ìoò^" OnClick="btnTouroku_Click" style="display:none " />                 
                  </span></th>
                </tr>
            </thead>
            <!--äÓñ{èÓïÒñæç◊-->
            <tbody id="meisaiTbody" runat="server"  style="display:none ">
                <tr>
                    <td style="width: 167px; height: 26px;" class="koumokuMei">
                        ífñ ê}1 ïWèÄ</td>
                    <td style="width: 150px; height: 26px;">
                        <asp:TextBox ID="tbxDanmenzu1" runat="server" MaxLength="24" Width="120px" CssClass = "codeNumber"></asp:TextBox>
                    </td>
                    <td style="width: 167px; height: 26px;" class="koumokuMei">
                        ífñ ê}2 ägí£</td>
                    <td style="width: 150px; height: 26px;">
                        <asp:TextBox ID="tbxDanmenzu2" runat="server" MaxLength="24" Width="120px" CssClass = "codeNumber"></asp:TextBox></td>
                    <td style="width: 169px; height: 26px;" class="koumokuMei">
                        ífñ ê}3 ÉxÉ^</td>
                    <td style="width: 150px; height: 26px;" >
                        <asp:TextBox ID="tbxDanmenzu3" runat="server" MaxLength="24" Width="120px" CssClass = "codeNumber"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 167px" class="koumokuMei">
                        ífñ ê}4 ï\ëw</td>
                    <td style="width: 150px">
                        <asp:TextBox ID="tbxDanmenzu4" runat="server" MaxLength="24" Width="120px" CssClass = "codeNumber"></asp:TextBox>
                    </td>
                    <td style="width: 167px" class="koumokuMei">
                        ífñ ê}5 íåèÛ</td>
                    <td style="width: 150px">
                        <asp:TextBox ID="tbxDanmenzu5" runat="server" MaxLength="24" Width="120px" CssClass = "codeNumber"></asp:TextBox></td>
                    <td style="width: 169px" class="koumokuMei">
                        ífñ ê}6 çY</td>
                    <td style="width: 150px">
                        <asp:TextBox ID="tbxDanmenzu6" runat="server" MaxLength="24" Width="120px" CssClass = "codeNumber"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 167px" class="koumokuMei">
                        ífñ ê}7 ÉxÉ^ÇQ</td>
                    <td style="width: 150px">
                        <asp:TextBox ID="tbxDanmenzu7" runat="server" MaxLength="24" Width="120px" CssClass = "codeNumber"></asp:TextBox>
                    </td>
                    <td colspan="4" style="width: 317px">
                    </td>
                </tr>
            </tbody>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>

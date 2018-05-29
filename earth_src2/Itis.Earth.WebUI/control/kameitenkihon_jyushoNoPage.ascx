<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="kameitenkihon_jyushoNoPage.ascx.vb" Inherits="Itis.Earth.WebUI.kameitenkihon_jyushoNoPage1" %>
<%@ Register Src="common_drop.ascx" TagName="common_drop" TagPrefix="uc1" %>
        <link rel="stylesheet" href="../css/jhsearth.css" type="text/css" />
        <table id="Table4" cellpadding="1" class="mainTable" style="margin-top: 10px; width: 916px;
            text-align: left">
            <thead>
                <tr>
                    <th class="tableTitle" colspan="10" rowspan="1" style="text-align: left">
                        <a id="titleText" runat="server"></a>
                        <asp:LinkButton ID="lbtnJyosho" runat="server">LinkButton</asp:LinkButton>
                        &nbsp;&nbsp;
                        <asp:Label ID="lblSign" runat="server"></asp:Label><span id="titleInfobar" runat="server"
                            style="display: none"></span>
                        <asp:Button ID="btnTouroku" style="display:none;" runat="server" Text="�o�^" />
                        <asp:Button ID="btnSakujyo" style="display:none;" runat="server" Text="�폜" OnClientClick ="if (!confirm('�폜���܂��B')){return false;}"  OnClick="btnSakujyo_Click" />
                        <asp:Button ID="btnCopy" runat="server" style="display:none;" Text="�c�Ə����R�s�[" /></th>
                </tr>
            </thead>
            <tbody id="meisaiTbody" runat="server" style="display:none;">
                <tr>
                    <td class="koumokuMei" style="width: 91px; height: 26px" >
                        �X�֔ԍ�</td>
                    <td colspan="3" style="width: 304px; height: 26px;">
                        <asp:TextBox ID="tbxYuubinNo1" runat="server" Style="width: 72px" CssClass = "codeNumber"></asp:TextBox>
                        <asp:Button ID="btnKensaku" runat="server" Text="����"  />
                    </td>

                    <td class="hissu" style="width: 91px;font-weight:bold; height: 26px;"  >
                        �Z���P</td>
                    <td colspan="3" class="hissu" style="height: 26px; width: 421px;" >
                        <asp:TextBox ID="tbxJyuusyo1" runat="server" Style="width: 296px" CssClass = "input"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="koumokuMei" style="width: 120px"  >
                        ���ݒn����</td>
                     <td colspan="3" style="width: 340px;">
                        <uc1:common_drop ID="ddlTodoufuken" runat="server" GetStyle="todoufuken" />
                      </td>
                    <td style="width: 91px" class="koumokuMei" >
                        �Z���Q</td>
                    <td colspan="3" style="width: 421px">
                        <asp:TextBox ID="tbxJyuusyo2" runat="server" Style="width: 296px" CssClass = "input"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="koumokuMei" style="width: 91px" >
                        ������</td>
                    <td colspan="3" style="width: 304px">
                        <asp:TextBox ID="tbxBusyoMei1" runat="server" Style="width: 296px" CssClass = "input"></asp:TextBox></td>
                    <td class="koumokuMei" style="width: 91px" >
                        ��\�Җ�</td>
                    <td colspan="3" style="width: 421px">
                        <asp:TextBox ID="tbxDaihyousyaMei1" runat="server" Style="width: 144px" CssClass = "input"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="koumokuMei" style="width: 91px" >
                        �d�b�ԍ�</td>
                    <td colspan="3" style="width: 304px">
                        <asp:TextBox ID="tbxTelNo1" runat="server" CssClass = "codeNumber" Style="width: 145px"></asp:TextBox></td>
                    <td style="width: 91px" class="koumokuMei" >
                        �e�`�]�ԍ�</td>
                    <td colspan="3" style="width: 421px">
                        <asp:TextBox ID="tbxFaxNo1" runat="server" CssClass = "codeNumber" Style="width: 100px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="koumokuMei" style="width: 91px" >
                        ���l</td>
                    <td colspan="7">
                        <asp:TextBox ID="tbxBikou11" runat="server" Width="256px" CssClass = "input"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="8">
                                                                         ���t��
                                                <table style="width: 640px" cellpadding="0" cellspacing="0" border="1" class="tinyTable">
                                                        <thead class = "gridviewTableHeader">
                                                          <tr>
                                                           <td>
                                                                ������</td>
                                                            <td>
                                                                �ۏ؏�</td>
                                                            <td>
                                                                �񍐏�</td>
                                                            <td>
                                                                ������s</td>
                                                            <td>
                                                                ���r�ۏ؏���</td>
                                                            <td style="width: 93px">
                                                                �H���񍐏�</td>
                                                            <td>
                                                                �����񍐏�</td>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                      


                 
                
           
                                                        <tr>
                                                            <td>
                                                                <asp:DropDownList ID="ddlSeikyuusyo" runat="server" Width="72px" Font-Names="�l�r �o�S�V�b�N">
                                                                    <asp:ListItem></asp:ListItem>
                                                                    <asp:ListItem>�Z</asp:ListItem>
                                                                </asp:DropDownList></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlHosyousyo" runat="server" Width="72px" Font-Names="�l�r �o�S�V�b�N">
                                                                    <asp:ListItem></asp:ListItem>
                                                                    <asp:ListItem>�Z</asp:ListItem>
                                                                </asp:DropDownList></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlhkks" runat="server" Width="72px" Font-Names="�l�r �o�S�V�b�N">
                                                                    <asp:ListItem></asp:ListItem>
                                                                    <asp:ListItem>�Z</asp:ListItem>
                                                                </asp:DropDownList></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlTeikiKankou" runat="server" Width="72px" Font-Names="�l�r �o�S�V�b�N">
                                                                    <asp:ListItem></asp:ListItem>
                                                                    <asp:ListItem>�Z</asp:ListItem>
                                                                </asp:DropDownList></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlKasihosyousyo" runat="server" Width="72px" Font-Names="�l�r �o�S�V�b�N">
                                                                    <asp:ListItem></asp:ListItem>
                                                                    <asp:ListItem>�Z</asp:ListItem>
                                                                </asp:DropDownList></td>
                                                            <td style="width: 93px">
                                                                <asp:DropDownList ID="ddlKojhkks" runat="server" Width="72px" Font-Names="�l�r �o�S�V�b�N">
                                                                    <asp:ListItem></asp:ListItem>
                                                                    <asp:ListItem>�Z</asp:ListItem>
                                                                </asp:DropDownList></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlkensahkks" runat="server" Width="72px" Font-Names="�l�r �o�S�V�b�N">
                                                                    <asp:ListItem></asp:ListItem>
                                                                    <asp:ListItem>�Z</asp:ListItem>
                                                                </asp:DropDownList></td>
                                                        </tr>
                                                     </tbody>
                                                </table>
                    </td>
                </tr>
            </tbody>
        </table>
        <asp:HiddenField ID="hidUpdTime" runat="server" />


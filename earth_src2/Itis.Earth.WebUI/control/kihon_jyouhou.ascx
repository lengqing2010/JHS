<%@ Control Language="vb" AutoEventWireup="false" Codebehind="kihon_jyouhou.ascx.vb"
    Inherits="Itis.Earth.WebUI.kihon_jyouhou" %>

<%@ Register Src="common_drop.ascx" TagName="common_drop" TagPrefix="uc1" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" RenderMode="Inline">
    <ContentTemplate>
        <table cellpadding="1" class="mainTable" style="text-align: left ; width:968px; ">
            <thead>
                <tr>
                    <th class="tableTitle" colspan="9" rowspan="1" style="text-align: left;height:30px;">
                        <asp:LinkButton ID="lnkTitle" runat="server">��{���</asp:LinkButton>
                        <asp:Button ID="btnTouroku" runat="server" Text="�o�^" />
                        <span id="titleInfobar1" runat="server"></span>
                    </th>
                </tr>
            </thead>
            <!--��{��񖾍�-->
            <tbody id="meisaiTbody1" runat="server" class="itemTable">
                <tr>
                    <td style="font-weight: bold;" class="hissu">
                        �����於��</td>
                    <td class="hissu" colspan="6" style="font-weight: bold">
                        <asp:TextBox ID="tbxSeisikiMei" runat="server" Width="496px"></asp:TextBox>&nbsp;<asp:Button
                            ID="btnCopy1" runat="server" Text="�����X���P�R�s�[" Width="120px" />
                        <asp:Button ID="btnCopy2" runat="server" Text="�����X���Q�R�s�[" Width="120px" /></td>
                </tr>
                <tr>
                    <td class="koumokuMei" style="font-weight: bold;">
                        �����於�̂Q</td>
                    <td colspan="4">
                        <asp:TextBox ID="tbxSeisikiKana" runat="server" Width="296px"></asp:TextBox>&nbsp;<asp:Button
                            ID="btnTenCopy1" runat="server" Text="�����X���P�R�s�[" Width="120px" />&nbsp;<asp:Button
                                ID="btnTenCopy2" runat="server" Text="�����X���Q�R�s�[" Width="120px" /></td>
                    <td style="font-weight: bold;" class="hissu">
                        �s���{���R�[�h
                    </td>
                    <td class="hissu">
                        <uc1:common_drop ID="Common_drop1" runat="server" GetStyle="todoufuken" />
                    </td>
                </tr>
                <tr>
                    <td class="koumokuMei" >
                        �N�ԓ���</td>
                    <td style="">
                        <asp:TextBox ID="tbxlblNenkanTousuu" runat="server" Style="ime-mode: disabled;"></asp:TextBox></td>
                    <td class="koumokuMei" style="">
                        �t�ۏؖ���FLG</td>
                    <td style="">
                        <asp:DropDownList ID="ddlSyoumeisyo" runat="server" AutoPostBack="true">
                            <asp:ListItem Value="1">�L��</asp:ListItem>
                            <asp:ListItem Value="0">�Ȃ�</asp:ListItem>
                        </asp:DropDownList></td>
                    <td class="koumokuMei" style="">
                        �t�ۏؖ���<br />
                        �J�n�N��</td>
                    <td colspan="2">
                        <asp:TextBox ID="tbxAddNengetu" runat="server" CssClass="codeNumber" MaxLength="7"
                            Style="width: 72px"></asp:TextBox>
                        <asp:Button ID="btnFuho" runat="server" Text="�t�ۏؖ��L����" OnClick="btnFuho_Click" /></td>
                </tr>
                
                <tr>
                    <td class="koumokuMei" >
                        �V�z�Z����n��<br />
                        �i�̔��j����
                        </td>
                    <td style="">
                        <asp:TextBox ID="tbxSintikuKensuu" runat="server" CssClass="codeNumber" Style="ime-mode: disabled;" MaxLength="10"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;��/�N
                    </td>
                    <td class="koumokuMei" style="">
                        �s���Y��������</td>
                    <td style="">
                        <asp:TextBox ID="tbxFudouKensuu" runat="server" CssClass="codeNumber" Style="ime-mode: disabled; width:74px;" MaxLength="8"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;��/�N
                        </td>
                    <td class="koumokuMei" >
                        ���t�H�[��<br />
                    �O�N�x �������z
</td>
                    <td colspan="2">
                        <asp:TextBox ID="tbxZenNenUkeoiKin" runat="server" CssClass="codeNumber" Style="ime-mode: disabled; width:104px;" MaxLength="20"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;���~/�N

                    </td>
                </tr> 
                
                
                
                
                <tr>
                    <td class="koumokuMei">
                        �c�ƒS����</td>
                    <td style="">
                        <asp:TextBox ID="tbxEigyouCd" runat="server" Width="120px" Style="ime-mode: disabled;"></asp:TextBox><asp:Button
                            ID="btnKensaku" runat="server" Text="����" /><asp:TextBox ID="tbxEigyouMei" runat="server"
                                TabIndex="-1" BorderStyle="None" ReadOnly="True" Width="92px" BackColor="Transparent"></asp:TextBox></td>
                    <td style="" class="koumokuMei">
                        ���p������</td>
                    <td style="">
                        <asp:TextBox ID="tbxHKDate" runat="server" Width="74px" Style="ime-mode: disabled;"></asp:TextBox></td>
                    <td style="" class="koumokuMei">
                        ���c�ƒS����</td>
                    <td colspan="2">
                        <asp:TextBox ID="tbxKyuuEigyouCd" runat="server" Width="120px" Style="ime-mode: disabled;"></asp:TextBox><asp:Button
                            ID="btnKyuuKensaku" runat="server" Text="����" /><asp:TextBox ID="tbxKyuuEigyouMei"
                                runat="server" TabIndex="-1" BorderStyle="None" ReadOnly="True" Width="92px"
                                BackColor="Transparent"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style=" height: 25px" class="koumokuMei">
                        �n�k�⏞FLG</td>
                    <td style=" height: 25px">
                        <asp:DropDownList ID="ddlJisinHosyou" runat="server">
                            <asp:ListItem Value="1">�L��</asp:ListItem>
                            <asp:ListItem Value="0">�Ȃ�</asp:ListItem>
                        </asp:DropDownList></td>
                    <td style=" height: 25px" class="koumokuMei">
                        �n�k�⏞�o�^��</td>
                    <td style="height: 25px">
                        <asp:TextBox ID="tbxJisinHosyou" runat="server" Width="74px" Style="ime-mode: disabled;"></asp:TextBox>
                    </td>
                    <td style=" height: 25px" class="koumokuMei">
                        �r�c�r<br>
                        �����ݒ���</td>
                    <td colspan="2" style="height: 25px; ">
                        <asp:DropDownList ID="ddlSds" runat="server">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="1">1�FSDS��p</asp:ListItem>
                            <asp:ListItem Value="2">2�FSDS���p</asp:ListItem>
                            <asp:ListItem Value="3">3�FSS�̂�</asp:ListItem>
                            <asp:ListItem Value="4">4�F���̑�</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="koumokuMei" style="">
                        �H��������</td>
                    <td style="">
                        <uc1:common_drop ID="Common_drop2" runat="server" GetStyle="syubetsu" />
                    </td>
                    <td class="koumokuMei" style="">
                        �H���T�|�[�g<br />
                        �V�X�e��</td>
                    <td style="">
                        <asp:DropDownList ID="ddlSystem" runat="server">
                            <asp:ListItem Value="1">���p����</asp:ListItem>
                            <asp:ListItem Value="">���p���Ȃ�</asp:ListItem>
                        </asp:DropDownList></td>
                    <td class="koumokuMei" style="">
                        �i�h�n��t���O</td>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlJio" runat="server">
                            <asp:ListItem Value="1">�L��</asp:ListItem>
                            <asp:ListItem Value="">����</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td class="koumokuMei">
                        �Ώۏ��iFLG
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_taiou_syouhin_kbn" runat="server">
  
                        </asp:DropDownList>&nbsp; &nbsp; &nbsp; &nbsp;
                        <asp:Label ID="lbl_taiou_syouhin_kbn_set_date" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="koumokuMei">
                        �y�n���|����
                    </td>
                    <td>
                        <asp:Label ID="lbl_tochirepo_muryou_flg" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="koumokuMei">�L�����y�[����
                    </td>
                    <td colspan="2">
                        <asp:DropDownList ID="ddl_campaign_waribiki_flg" runat="server">
                            <asp:ListItem Value="1">�L��</asp:ListItem>
                            <asp:ListItem Value="">����</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp; &nbsp; &nbsp; &nbsp;
                        <asp:Label ID="lbl_campaign_waribiki_flg_txt" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </tbody>
        </table>
        <asp:HiddenField ID="hidHi" runat="server" />
        <asp:HiddenField ID="hidHaita" runat="server" />

    </ContentTemplate>
</asp:UpdatePanel>

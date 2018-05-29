<%@ Control Language="vb" AutoEventWireup="false" Codebehind="SinnseiKbn_jyouhou.ascx.vb"
    Inherits="Itis.Earth.WebUI.SinnseiKbn_jyouhou" %>
<%@ Register Src="common_drop.ascx" TagName="common_drop" TagPrefix="uc1" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" RenderMode="Inline">
    <ContentTemplate>
        <table cellpadding="1" class="mainTable" style="width: 968px; text-align: left">
            <thead>
                <tr>
                    <th class="tableTitle" colspan="7" style="text-align: left">
                        <asp:LinkButton ID="lnkTitle" runat="server">�\���敪���</asp:LinkButton>
                        <asp:Button ID="btnTouroku" runat="server" Text="�o�^" />
                        <span id="titleInfobar1" runat="server"></span>
                    </th>
                </tr>
            </thead>
            <!--��{��񖾍�-->
            <tbody id="meisaiTbody1" runat="server" class="itemTable">
                <tr>
                    <td style="width: 106px; font-weight: bold;" class="koumokuMei">
                        �\������
                    </td>
                    <td class="hissu" colspan="6" style="font-weight: bold;width: 850px;">
                         <asp:DropDownList ID="ddl_shinsei_syoshiki" runat="server" AutoPostBack="true">
             
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="font-weight: bold;" class="koumokuMei" rowspan="2">
                        �\���敪
                    </td>
                    <td style="font-weight: bold;" class="shouhinTableTitle">
                        <asp:CheckBox ID="cbshinsei_kbn_shinki" runat="server" Text="&nbsp;&nbsp;&nbsp;�V�K" />
                    </td>
                    <td  colspan="5">
                        <strong>
                        ���ƌ`��</strong> &nbsp; &nbsp; &nbsp; &nbsp;
                        <asp:CheckBox ID="cbshinsei_kbn_jig_shinki" runat="server" Text="�V�z���� " />
                        &nbsp; &nbsp;
                        <asp:CheckBox ID="cbshinsei_kbn_jig_fudousan" runat="server" Text="�s���Y����" />
                        &nbsp; &nbsp;
                        <asp:CheckBox ID="cbshinsei_kbn_jig_reform" runat="server" Text="���t�H�[������" />
                        &nbsp; &nbsp;
                        <asp:CheckBox ID="cbshinsei_kbn_jig_sonota" runat="server" Text="���̑�" AutoPostBack="true" OnCheckedChanged="cbshinsei_kbn_jig_sonota_CheckedChanged" />
                        �i<asp:TextBox runat="server" ID="tbxShinsei_kbn_jig_sonota_hosoku"  Enabled="false" MaxLength="25" Style="width: 165px;"
                        ></asp:TextBox>�j
                     </td>
                </tr>
                <tr>
                    <td style="font-weight: bold;"  class="shouhinTableTitle">
                        <asp:CheckBox ID="cbshinsei_kbn_sonota" runat="server" Text="���̑�" />
                    </td>
                    <td colspan="5">
                        <strong>
                        �����p���޽</strong>
                        <asp:CheckBox ID="cbshinsei_kbn_ser_jibantyousa" runat="server" Text="�n�Ւ����֘A���޽" />
                        <asp:CheckBox ID="cbshinsei_kbn_ser_tatemonokensa" runat="server" Text="���������֘A���޽" />
                        <asp:CheckBox ID="cbshinsei_kbn_ser_sonota" runat="server" Text="���̑�" AutoPostBack="true" OnCheckedChanged="cbshinsei_kbn_ser_sonota_CheckedChanged" />
                        �i<asp:TextBox runat="server" ID="tbxShinsei_kbn_ser_sonota_hosoku" Enabled="false" MaxLength="25" Style="width: 165px;"
                        ></asp:TextBox>�j
                    </td>
                </tr>
                
            </tbody>
        </table>
        <asp:HiddenField ID="hidHi" runat="server" />
        <asp:HiddenField ID="hidHaita" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>

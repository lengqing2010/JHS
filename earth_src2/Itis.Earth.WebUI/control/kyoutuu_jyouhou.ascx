<%@ Control Language="vb" AutoEventWireup="false" Codebehind="kyoutuu_jyouhou.ascx.vb"
    Inherits="Itis.Earth.WebUI.kyoutuu_jyouhou" %>
<%@ Register Src="common_drop.ascx" TagName="common_drop" TagPrefix="uc1" %>

<script type="text/javascript">
function fncClick(obj){
obj.click();
return false;
}
   //��ʑJ�ڏ���
    function funcMove(url,tenmd,isfc,kameicd){
        //<!-- ��ʈ��n����� -->
        var arrArg = window.dialogArguments;
        var objSendForm = objEBI("openPageForm2");
        var objSendVal_tenmd = objEBI("tenmd");
        var objSendVal_isfc = objEBI("isfc");
        var objSendVal_kameicd = objEBI("kameicd");

        objSendForm.action =url
        objSendVal_tenmd.value = tenmd;       
        objSendVal_isfc.value = isfc;  
        objSendVal_kameicd.value = kameicd;  
        objSendForm.submit();
        return false;
        
    }
    //��ʑJ��
    function fncGamenSenni(strKameitenCd){
        window.open('HanbaiKakakuMasterSearchList.aspx?sendSearchTerms='+'1'+'$$$'+strKameitenCd);
    }
    
</script>

<!--##############
    TITLE
    ##############-->
<table class="titleTable" border="0" style="width: 960px; vertical-align: top; height: 40px;"
    cellpadding="0" cellspacing="0">
    <tr style="height: 20px;">
        <!-- TITLE�̎� -->
        <th rowspan="1" style="width: 628px; text-align: left; vertical-align: top;">
            <asp:Label ID="lblTitle" runat="server" Text="�����X��{���Ɖ�" Font-Size="16px"></asp:Label>
            &nbsp; &nbsp; &nbsp; &nbsp;
            <asp:Button ID="btnClose" runat="server" Text="����" OnClientClick="window.close();return false;" />
        </th>
        <th rowspan="1" style="vertical-align: top; width: 109px; text-align: left;">
            <asp:Button ID="btnHansokuSina" runat="server" Text="�̑��i�o�^" ForeColor="Red" /></th>
        <td style="width: 115px; height: 0px">
            <asp:Label ID="lblKousin1" runat="server"></asp:Label></td>
        <td style="width: 149px; height: 0px">
            <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                <ContentTemplate>
                    <asp:Label ID="lblKousinSya" runat="server"></asp:Label>&nbsp;
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr style="height: 20px;">
        <th rowspan="1" colspan="2" style="vertical-align: top; text-align: left; height: 17px;">
            <table>
                <tr>
                    <td style="border-left: 0px white; width: 100%;">
                        <asp:Button ID="btnKihonJyouhouInquiry" runat="server" Text="��{" Width="60px" Height="22px"
                            Style="margin-right: 2px;" />
                        <asp:Button ID="btnTyuiJyouhouInquiry" runat="server" Text="����" Width="60px" Height="22px"
                            Style="margin-right: 2px;" />
                        <asp:Button ID="btnEigyouJyouhouInquiry" runat="server" Text="�c��" Width="60px" Height="22px"
                            Style="margin-right: 2px;" />
                        <asp:Button ID="btnBukkenJyouhouInquiry" runat="server" Text="����" Width="60px" Height="22px"
                            Style="margin-right: 2px;" />
                        <asp:Button ID="btnYosinJyouhouDetails" runat="server" Text="�^�M" Width="60px" Height="22px"
                            Style="margin-right: 2px;" />
                        <asp:Button ID="btnKakakuJyouhou" runat="server" Text="���i" Width="60px" Height="22px"
                            Style="margin-right: 2px;" />
                        <asp:Button ID="btnSiharaiTyousa" runat="server" Text="�x�������i�����j" Width="110px" Height="22px"
                            Style="margin-right: 2px;" />
                        <asp:Button ID="btnSiharaiKouji" runat="server" Text="�x�������i�H���j" Width="110px" Height="22px"
                            Style="margin-right: 2px;" />
                        <asp:Button ID="btnHoukakusyo" runat="server" Text="�񍐏��E�I�v�V����" Width="110px" Height="22px"
                            Style="margin-right: 2px;" />
                        <asp:Button runat="server" ID="btnTorihukiJyoukenKakuninhyou" Text="��������m�F�\" Width="110px"
                            Height="22px" Style="margin-right: 2px;" />
                        <asp:Button runat="server" ID="btnTyousaCard" Text="�����J�[�h" Width="100px" Height="22px"
                            Style="display: none;" />
                    </td>
                </tr>
            </table>
        </th>
        <td style="width: 115px; height: 17px;">
            <asp:Label ID="lblKousin2" runat="server"></asp:Label></td>
        <td style="width: 149px; height: 17px">
            <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                <ContentTemplate>
                    <asp:Label ID="lblKousinHi" runat="server">
                    </asp:Label>
                    <asp:HiddenField ID="hidHaita" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>

<table runat="server" id="tablekensaku" style="margin-bottom: 10px; text-align: left;"
    class="mainTable" cellpadding="0">
    <tr>
        <td style="width: 186px;">
            �󂫔ԍ�����</td>
        <td style="width: 243px">
            �敪
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnTyokusetuNyuuryoku" />
                    <asp:AsyncPostBackTrigger ControlID="btnTyokusetuNyuuryokuTyuusi" />
                </Triggers>
                <ContentTemplate>
                    <uc1:common_drop ID="Common_drop1" runat="server" GetStyle="kubun" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td style="width: 509px">
            �����X�R�[�h
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnTyokusetuNyuuryoku" />
                    <asp:AsyncPostBackTrigger ControlID="btnTyokusetuNyuuryokuTyuusi" />
                </Triggers>
                <ContentTemplate>
                    <asp:TextBox runat="server" ID="tbxKameitenCd" MaxLength="5" Style="width: 65px;"
                        CssClass="codeNumber"></asp:TextBox>
                    <asp:Button runat="server" ID="btnKameitenSearch1" Text="����" />
                    <asp:Button runat="server" ID="btnTyokusetuNyuuryoku" Text="���ړ���" />
                    <asp:Button runat="server" ID="btnTyokusetuNyuuryokuTyuusi" Text="���ړ��͒��~" Visible="false" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td style="text-align: center; height: 21px;" colspan="3">
            �������X�R�[�h�̍ő�󂫔ԍ����敪�A�����X�R�[�h�ɃZ�b�g�A�b�v���܂��B
        </td>
    </tr>
</table>
<!--##############
    ���ʏ��
    ##############-->
<asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional" RenderMode="Inline">
    <ContentTemplate>
        <table style="text-align: left; width: 968px" class="mainTable" cellpadding="0">
            <thead>
                <tr>
                    <th class="tableTitle" colspan="10" rowspan="1" style="height: 24px">
                        <a id="kyoutuDispLink" runat="server">���ʏ��</a>
                        <asp:Button ID="btnTouroku" runat="server" Text="�o�^" /><span id="kyoutuTitleInfobar"
                            style="display: none;" runat="server"></span></th>
                </tr>
            </thead>
            <!--���ʏ�񖾍�-->
            <tbody id="kyoutuTBody" runat="server" class="tableMeiSai" style="margin-top: 0px;
                width: 950px;">
                <tr style="height: 20px;">
                    <td class="koumokuMei">
                        �敪
                    </td>
                    <td colspan="3" style="height: 17px;">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnTyokusetuNyuuryoku" />
                                <asp:AsyncPostBackTrigger ControlID="btnTyokusetuNyuuryokuTyuusi" />
                            </Triggers>
                            <ContentTemplate>
                                <uc1:common_drop ID="comdrp" runat="server" Visible="false" GetStyle="kubun" />
                                <asp:TextBox runat="server" ID="tbxKyoutuKubun" TabIndex="-1" CssClass="readOnly"
                                    Width="50px"></asp:TextBox>
                                <asp:Label ID="lblKubunMei" runat="server"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="<%=strCss%>" style="font-weight: bold;">
                        ���&nbsp;
                    </td>
                    <td colspan="3" style="height: 17px; width: 109px;" class="<%=strCss2%>">
                        <asp:TextBox ID="tbxTorikesi" runat="server" Width="100px" Style="ime-mode: disabled;"></asp:TextBox>
                        <asp:DropDownList ID="ddlTorikesi" runat="server" Width="105px">
                        </asp:DropDownList>
                    </td>
                    <td colspan="1" class="koumokuMei" style="width: 155px; height: 17px; font-weight: bold;">
                        ������~�t���O</td>
                    <td colspan="1" style="width: 143px; height: 17px">
                        <uc1:common_drop ID="Common_drop2" GetWidth="128" runat="server" GetStyle="haccyu_teisi" />
                    </td>
                </tr>
                <tr style="height: 20px;">
                    <td class="koumokuMei">
                        �����X�R�[�h
                    </td>
                    <td style="width: 70px; height: 20px;">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnTyokusetuNyuuryoku" />
                                <asp:AsyncPostBackTrigger ControlID="btnTyokusetuNyuuryokuTyuusi" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:TextBox ID="tbxKyoutuKameitenCd" ReadOnly="true" TabIndex="-1" CssClass="readOnly"
                                    runat="server" Width="60px"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="<%=strCss%>" style="font-weight: bold;">
                        �����X���P
                    </td>
                    <td style="width: 310px; height: 20px; font-weight: bold;" class="<%=strCss2%>">
                        <asp:TextBox ID="tbxKyoutuKameitenMei1" runat="server" Width="300px"></asp:TextBox>
                    </td>
                    <td colspan="2" class="<%=strCss%>" style="font-weight: bold;">
                        �X�J�i���P
                    </td>
                    <td class="<%=strCss2%>" colspan="4" style="height: 20px">
                        <asp:TextBox ID="tbxKyoutukakeMei1" runat="server" Width="300px"></asp:TextBox>
                    </td>
                </tr>
                <tr style="height: 20px">
                    <td colspan="2" style="height: 18px">
                    </td>
                    <td class="koumokuMei" style="font-weight: bold; height: 18px;">
                        �����X���Q
                    </td>
                    <td style="width: 310px; height: 18px;">
                        <asp:TextBox ID="tbxKyoutuKameitenMei2" runat="server" Width="300px"></asp:TextBox>
                    </td>
                    <td colspan="2" class="koumokuMei" style="font-weight: bold; height: 18px;">
                        �X�J�i���Q
                    </td>
                    <td colspan="4" style="height: 18px">
                        <asp:TextBox ID="tbxKyoutukakeMei2" runat="server" Width="300px"></asp:TextBox>
                    </td>
                </tr>
                <tr style="height: 20px;">
                    <td class="koumokuMei" style="height: 15px">
                        �r���_�[NO
                    </td>
                    <td colspan="3" style="height: 15px">
                        <asp:TextBox ID="tbxBirudaNo" runat="server" Width="60px" Style="ime-mode: disabled;"></asp:TextBox>
                        <asp:Button ID="btnBirudaNo" runat="server" Text="����" />
                        <asp:TextBox ID="lblBirudaMei" TabIndex="-1" runat="server" Width="307px" BorderStyle="None"
                            ReadOnly="True" BackColor="Transparent"></asp:TextBox>
                    </td>
                    <td colspan="2" class="koumokuMei" style="height: 15px">
                        �n��R�[�h</td>
                    <td colspan="4" style="height: 15px">
                        <asp:TextBox ID="tbxKeiretuCd" runat="server" Width="65px" Style="ime-mode: disabled;"></asp:TextBox>
                        <asp:Button ID="btnKeiretuCd" runat="server" Text="����" />
                        <asp:TextBox ID="lblKeiretuMei" TabIndex="-1" runat="server" BorderStyle="None" ReadOnly="True"
                            Width="192px" BackColor="Transparent"></asp:TextBox>
                    </td>
                </tr>
                <tr style="height: 20px;">
                    <td class="koumokuMei">
                        �c�Ə��R�[�h</td>
                    <td colspan="3" style="height: 20px">
                        <asp:TextBox ID="tbxEigyousyoCd" runat="server" Width="60px" Style="ime-mode: disabled;"></asp:TextBox>
                        <asp:Button ID="btnEigyousyoCd" runat="server" Text="����" />
                        <asp:TextBox ID="lblEigyousyoMei" TabIndex="-1" runat="server" Width="150px" BorderStyle="None"
                            BackColor="Transparent"></asp:TextBox>
                        <asp:TextBox ID="tbxFcTyousaKaisya" TabIndex="-1" runat="server" Width="170px" BorderStyle="None"
                            BackColor="Transparent" Style="margin-left: 10px;">
                        </asp:TextBox>
                        <asp:Button ID="btnFcTyousaKaisya" runat="server" Style="display: none;" />
                    </td>
                    <td colspan="2" class="koumokuMei">
                        TH���r�R�[�h</td>
                    <td colspan="4" style="height: 20px">
                        <asp:TextBox ID="tbxThKasiCd" runat="server" Width="65px"></asp:TextBox>
                    </td>
                </tr>
            </tbody>
        </table>
        <asp:HiddenField ID="hidTorikesi" runat="server" />
        <asp:HiddenField runat="server" ID="hidFile" />
        <a id="file" style="display: none;" runat="server">��������m�F�\</a>
    </ContentTemplate>
</asp:UpdatePanel>

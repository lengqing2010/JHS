<%@ Control Language="vb" AutoEventWireup="false" Codebehind="TeibetuSyouhinRecordCtrl.ascx.vb"
    Inherits="Itis.Earth.WebUI.TeibetuSyouhinRecordCtrl" %>
<%@ Import Namespace="Itis.Earth.Utilities" %>
<%@ Register Src="SeikyuuSiireLinkCtrl.ascx" TagName="SeikyuuSiireLinkCtrl" TagPrefix="uc1" %>
<%@ Register Src="TokubetuTaiouToolTipCtrl.ascx" TagName="TokubetuTaiouToolTipCtrl" TagPrefix="uc" %>

<script type="text/javascript">
</script>

<asp:UpdatePanel ID="UpdatePanelSyouhinRec" runat="server" RenderMode="Inline" UpdateMode="Conditional">
    <ContentTemplate>
        <table class="innerTable" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%;
            border-top: 0px; table-layout: fixed;">
            <tr id="SyouhinRecord" runat="server">
                <td style="width: 245px; border-left: 0px; border-top: 0px; white-space: nowrap;"
                    class="itemNm">
                    <%-- ���i�R�[�h --%>
                    <asp:TextBox ID="TextSyouhinCd" runat="server" CssClass="itemCd" MaxLength="8" Width="60px"></asp:TextBox>
                    <%-- ���i�����{�^�� --%>
                    <input type="button" name="" id="ButtonSyouhinKensaku" value="����" class="gyoumuSearchBtn"
                        runat="server" onserverclick="ButtonSyouhinKensaku_ServerClick" />
                    <%-- �m��h���b�v�_�E���i���i�R�̂݁j --%>
                    <asp:DropDownList ID="SelectKakutei" runat="server">
                        <asp:ListItem Value="0">���m��</asp:ListItem>
                        <asp:ListItem Value="1">�m��</asp:ListItem>
                    </asp:DropDownList>
                    <%-- ������/�d���惊���N --%>
                    <uc1:SeikyuuSiireLinkCtrl ID="SeikyuuSiireLinkCtrl" runat="server" />
                    <%-- ���ʑΉ��c�[���`�b�v --%>
                    <uc:TokubetuTaiouToolTipCtrl ID="TokubetuTaiouToolTipCtrl" runat="server" />
                    <br />
                    <%-- ���i�� --%>
                    <asp:Label ID="SpanSyouhinName" runat="server" Style="width: 200px" />
                    <input id="ButtonHiddenSyouhinKensaku" runat="server" onserverclick="ButtonHiddenSyouhinKensaku_ServerClick"
                        style="display: none" type="button" value="����(��\��)" /></td>
                <td style="border-left: solid 3px gray; border-top: 0px; width: 73px;" id="TdSyoudakusyoKingaku"
                    runat="server">
                    <%-- ���������z --%>
                    <asp:TextBox ID="TextSyoudakusyoKingaku" runat="server" CssClass="kingaku" MaxLength="7"
                        Width="65px" OnTextChanged="TextSyoudakusyoKingaku_TextChanged" AutoPostBack="False"></asp:TextBox><br />
                    <%-- �d������Ŋz --%>
                    <asp:TextBox ID="TextSiireSyouhizeiGaku" runat="server" CssClass="kingaku" Width="65px"
                        TabIndex="-1" OnTextChanged="TextSiireSyouhizeiGaku_TextChanged" AutoPostBack="False"
                        MaxLength="7"></asp:TextBox>
                </td>
                <td style="border-top: 0px; width: 73px;" id="TdDenpyouSiireNengappi" runat="server">
                    <%-- �`�[�d���N���� --%>
                    <asp:TextBox ID="TextDenpyouSiireNengappiDisplay" runat="server" CssClass="date readOnlyStyle2"
                        ReadOnly="True" TabIndex="-1" Width="65px"></asp:TextBox><br />
                    <%-- �`�[�d���N�����C�� --%>
                    <asp:TextBox ID="TextDenpyouSiireNengappi" runat="server" CssClass="date" MaxLength="10"
                        BackColor="#ddaaee" Width="65px" AutoPostBack="False"></asp:TextBox>
                </td>
                <td id="TdKoumutenSeikyuuGaku" runat="server" style="border-top: 0px; border-left: solid 3px gray;
                    width: 73px;">
                    <%-- �H���X�������z --%>
                    <asp:TextBox ID="TextKoumutenSeikyuuGaku" runat="server" CssClass="kingaku" MaxLength="7"
                        Width="65px" AutoPostBack="False" OnTextChanged="TextKoumutenSeikyuuGaku_TextChanded"></asp:TextBox><br />
                    <%-- ���������z --%>
                    <asp:TextBox ID="TextJituSeikyuuGaku" runat="server" CssClass="kingaku" Width="65px"
                        OnTextChanged="TextJituSeikyuuGaku_TextChanged" AutoPostBack="False" MaxLength="7"></asp:TextBox>
                    <%-- ���Ϗ��쐬��(��\��) --%>
                    <asp:TextBox ID="TextMitumorisyoSakuseibi" runat="server" CssClass="date" Visible="false"
                        MaxLength="10" Width="65px"></asp:TextBox>
                </td>
                <td style="border-top: 0px; width: 73px;">
                    <%-- ����Ŋz --%>
                    <asp:TextBox ID="TextSyouhizeiGaku" runat="server" CssClass="kingaku" Width="65px"
                        OnTextChanged="TextSyouhizeiGaku_TextChanged" AutoPostBack="False" MaxLength="7"></asp:TextBox><br />
                    <%-- �ō����z --%>
                    <asp:TextBox ID="TextZeikomiKingaku" runat="server" BorderStyle="None" CssClass="kingaku readOnlyStyle2"
                        ReadOnly="True" TabIndex="-1" Width="65px"></asp:TextBox>
                </td>
                <td style="border-top: 0px; width: 71px;">
                    <%-- �`�[����N���� --%>
                    <asp:TextBox ID="TextDenpyouUriageNengappiDisplay" runat="server" CssClass="date readOnlyStyle2"
                        ReadOnly="True" TabIndex="-1" Width="65px"></asp:TextBox><br />
                    <%-- �`�[����N�����C�� --%>
                    <asp:TextBox ID="TextDenpyouUriageNengappi" runat="server" CssClass="date" MaxLength="10"
                        BackColor="#ddaaee" Width="65px" OnTextChanged="TextDenpyouUriageNengappi_TextChanged"
                        AutoPostBack="False"></asp:TextBox>
                </td>
                <td style="border-top: 0px; width: 110px;">
                    <%-- ����N���� --%>
                    <asp:TextBox ID="TextUriageNengappi" runat="server" CssClass="date" MaxLength="10"
                        TabIndex="-1" Width="65px" OnTextChanged="TextUriageNengappi_TextChanged" AutoPostBack="False"></asp:TextBox></br>
                    <%-- ���㏈�� --%>
                    <asp:UpdatePanel ID="UpdatePanelUridate" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                        <ContentTemplate>
                            <%-- ���㏈���v���_�E�� --%>
                            <asp:DropDownList ID="SelectUriageSyori" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SelectUriageSyori_SelectedIndexChanged"
                                Style="font-size: 10px; width: 36px;" TabIndex="-1">
                                <asp:ListItem Selected="True" Value="0">��</asp:ListItem>
                                <asp:ListItem Value="1">��</asp:ListItem>
                            </asp:DropDownList><%-- ����� --%><asp:TextBox ID="TextUriageBi" runat="server" CssClass="date readOnlyStyle2"
                                TabIndex="-1" BorderStyle="None" ReadOnly="True"></asp:TextBox></ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="SelectUriageSyori" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td colspan="2" style="border-top: 0px; border-left: solid 3px gray; width: 71px;">
                    <%-- ���������s�� --%>
                    <asp:TextBox ID="TextSeikyuusyoHakkoubi" runat="server" CssClass="date" MaxLength="10"
                        Width="65px"></asp:TextBox><br />
                    <%-- �����L�� --%>
                    <asp:DropDownList ID="SelectSeikyuuUmu" runat="server" OnSelectedIndexChanged="SelectSeikyuuUmu_SelectedIndexChanged"
                        AutoPostBack="True" Style="font-size: 10px; width: 36px;" TabIndex="-1">
                        <asp:ListItem Selected="True" Value=""></asp:ListItem>
                        <asp:ListItem Value="1">�L</asp:ListItem>
                        <asp:ListItem Value="0">��</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="text-align: center; border-top: 0px; border-left: solid 3px gray; width: 100%;">
                    <%-- ���������z --%>
                    <asp:TextBox ID="TextHattyuusyoKingaku" runat="server" AutoPostBack="False" CssClass="kingaku"
                        MaxLength="7" Width="65px" OnTextChanged="TextHattyuusyoKingaku_TextChanged"></asp:TextBox><%-- �������m�� --%><asp:DropDownList
                            ID="SelectHattyuusyoKakutei" runat="server" AutoPostBack="True" Style="font-size: 10px;
                            width: 36px;" OnSelectedIndexChanged="SelectHattyuusyoKakutei_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Value="0">��</asp:ListItem>
                            <asp:ListItem Value="1">�m</asp:ListItem>
                        </asp:DropDownList>
                    <br />
                    <%-- �������m�F�� --%>
                    <asp:TextBox ID="TextHattyuusyoKakuninbi" runat="server" CssClass="date" Width="65px"
                        MaxLength="10"></asp:TextBox>
                </td>
                <td id="TdSpacer" runat="server" style="border-top: 0px; border-left: solid 3px gray;
                    width: 144px; display: none;">
                    <%-- ���������ȉ��A��\�����ڗ񋓁������� --%>
                    <%-- ���㏈���i��\���j --%>
                    <asp:HiddenField ID="HiddenUriageSyori" runat="server" />
                    <%-- ���������z�ύX�O�i��\���j --%>
                    <asp:HiddenField ID="HiddenHattyuusyoKingakuOld" runat="server" />
                    <%-- �������t���O�ύX�O�i��\���j --%>
                    <asp:HiddenField ID="HiddenHattyuusyoFlgOld" runat="server" />
                    <%-- �����z�i��\���j --%>
                    <asp:HiddenField ID="HiddenNyuukinGaku" runat="server" />
                    <%-- �ŗ��i��\���j --%>
                    <asp:HiddenField ID="HiddenZeiritu" runat="server" />
                    <%-- �ŋ敪�i��\���j --%>
                    <asp:HiddenField ID="HiddenZeiKbn" runat="server" />
                    <%-- �\�����[�h�i��\���j --%>
                    <asp:HiddenField ID="HiddenDispMode" runat="server" />
                    <%-- �����^�C�v�i��\���j --%>
                    <asp:HiddenField ID="HiddenSeikyuuType" runat="server" />
                    <%-- ���z�t���O�i��\���j --%>
                    <asp:HiddenField ID="HiddenKingakuFlg" runat="server" />
                    <%-- �n��R�[�h�i��\���j --%>
                    <asp:HiddenField ID="HiddenKeiretuCd" runat="server" />
                    <%-- �n��R�[�h�i��\���j --%>
                    <asp:HiddenField ID="HiddenKameitenCd" runat="server" />
                    <%-- �ǂ̏��i��\�����i��\���j --%>
                    <asp:HiddenField ID="HiddenTargetId" runat="server" />
                    <%-- �������Ǘ������i��\���j --%>
                    <asp:HiddenField ID="HiddenHattyuusyoKanriKengen" runat="server" />
                    <%-- �o���Ɩ������i��\���j --%>
                    <asp:HiddenField ID="HiddenKeiriGyoumuKengen" runat="server" />
                    <%-- �X�V�����i��\���j --%>
                    <asp:HiddenField ID="HiddenUpdDatetime" runat="server" />
                    <%-- ���O�C�����[�U�[�h�c�i��\���j --%>
                    <asp:HiddenField ID="HiddenLoginUserId" runat="server" />
                    <asp:UpdatePanel ID="UpdatePanelHosyouMessage" runat="server" RenderMode="Inline"
                        UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:HiddenField ID="HiddenHosyouMessage" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <%-- ��ʕ\��NO�i��\���j --%>
                    <asp:HiddenField ID="HiddenGamenHyoujiNo" runat="server" />
                    <%-- ���ރR�[�h�i��\���j --%>
                    <asp:HiddenField ID="HiddenBunruiCd" runat="server" />
                    <%-- �敪�i��\���j --%>
                    <asp:HiddenField ID="HiddenKubun" runat="server" />
                    <%-- �ԍ��i��\���j --%>
                    <asp:HiddenField ID="HiddenBangou" runat="server" />
                    <%-- ���i�R�[�h�ύX�O�i��\���j --%>
                    <asp:HiddenField ID="HiddenSyouhinCdOld" runat="server" />
                    <%-- �����L���ύX�O�i��\���j --%>
                    <asp:HiddenField ID="HiddenSeikyuuUmuOld" runat="server" />
                    <%-- ��ʋN�������ێ�_����i��\���j --%>
                    <asp:HiddenField ID="HiddenOpenValuesUriage" runat="server" />
                    <%-- ��ʋN�������ێ�_�d���i��\���j --%>
                    <asp:HiddenField ID="HiddenOpenValuesSiire" runat="server" />
                    <asp:HiddenField runat="server" ID="HiddenOpenValue" />
                    <asp:HiddenField runat="server" ID="HiddenKeyValue" />
                    <%-- ��ʋN�������ێ�_����i��\���j --%>
                    <asp:HiddenField ID="HiddenOpenValuesTokubetuTaiou" runat="server" />
                    <asp:HiddenField ID="HiddenTokubetuTaiouUpdFlg" runat="server" />
                </td>
            </tr>
            <tr id="TableSpacer" runat="server">
                <td class="tableSpacer" colspan="11">
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>

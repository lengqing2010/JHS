<%@ Control Language="vb" AutoEventWireup="false" Codebehind="SeikyuuSiireLinkCtrl.ascx.vb"
    Inherits="Itis.Earth.WebUI.SeikyuuSiireLinkCtrl" %>
<%@ Import Namespace="Itis.Earth.Utilities" %> 
<asp:UpdatePanel ID="UpdatePanelSeikyuuSiireLink" runat="server" RenderMode="inline"
    UpdateMode="conditional">
    <ContentTemplate>
        <%-- ������/�d���惊���N --%>
        <a id="LinkSeikyuuSiireHenkou" style="display: none;" runat="server" href="JavaScript:void(0);">
            <asp:Label ID="lblLinkSeikyuuStr" runat="server" Text="��" Style="padding-left: 0px;
                padding-right: 0px; width: 10px;" />/<asp:Label ID="lblLinkSiireStr" runat="server"
                    Text="�d" /></a
         ><%-- ������R�[�h --%><input type="hidden" id="HiddenSeikyuuSakiCd" runat="server" style="display:block; float:left;"
        /><%-- ������}�� --%><input type="hidden" id="HiddenSeikyuuSakiBrc" runat="server" style="display:block; float:left;" 
        /><%-- ������敪 --%><input type="hidden" id="HiddenSeikyuuSakiKbn" runat="server" style="display:block; float:left;"
        /><%-- ������ЃR�[�h --%><input type="hidden" id="HiddenTysKaisyaCd" runat="server" style="display:block; float:left;"
        /><%-- ������Ў��Ə��R�[�h --%><input type="hidden" id="HiddenTysKaisyaJigyousyoCd" runat="server" style="display:block; float:left;" />
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel ID="UpdatePanelSeikyuuSiireInfo" runat="server" RenderMode="Inline"
    UpdateMode="Conditional">
    <ContentTemplate>
          <%-- �����X�R�[�h --%><input type="hidden" id="HiddenKameitenCd" runat="server" style="display:block; float:left;"
        /><%-- ���i�R�[�h --%><input type="hidden" id="HiddenSyouhinCd" runat="server" style="display:block; float:left;"
        /><%-- ��{������R�[�h --%><asp:HiddenField ID="HiddenDefaultSeikyuuSaki" runat="server"
        /><%-- ��{�d����R�[�h --%><asp:HiddenField ID="HiddenDefaultSiireSaki" runat="server"
        /><%-- �H����А����`�F�b�N --%><input type="hidden" id="HiddenKojKaisyaSeikyuu" runat="server" style="display:block; float:left;"
        /><%-- �H����ЃR�[�h --%><input type="hidden" id="HiddenKojKaisyaCd" runat="server" style="display:block; float:left;"
        /><%-- ��{�����於 --%><asp:HiddenField ID="HiddenDefaultSeikyuuSakiMei" runat="server"
        /><%-- ���㏈���� --%><input type="hidden" id="HiddenUriageSyorizumi" runat="server" style="display:block; float:left;"
        /><%-- �`�[����N���� --%><input type="hidden" id="HiddenDenUriDate" runat="server" style="display:block; float:left;"
        /><%-- �\�����[�h --%><input type="hidden" id="HiddenViewMode" runat="server" style="display:block; float:left;"
        /><%-- �������� --%><input type="hidden" id="HiddenSimeDate" runat="server" style="display:block; float:left;"
        /><%-- ������ύX�`�F�b�N--%><input type="hidden" id="HiddenChkSeikyuuSakiChg" runat="server" style="display:block; float:left;" />
    </ContentTemplate>
</asp:UpdatePanel>
<%-- ���s���󔒗ޕ����ɕϊ������ׁA���s�s�� --%>  
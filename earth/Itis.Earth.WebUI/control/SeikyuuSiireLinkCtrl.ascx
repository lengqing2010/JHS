<%@ Control Language="vb" AutoEventWireup="false" Codebehind="SeikyuuSiireLinkCtrl.ascx.vb"
    Inherits="Itis.Earth.WebUI.SeikyuuSiireLinkCtrl" %>
<%@ Import Namespace="Itis.Earth.Utilities" %> 
<asp:UpdatePanel ID="UpdatePanelSeikyuuSiireLink" runat="server" RenderMode="inline"
    UpdateMode="conditional">
    <ContentTemplate>
        <%-- 請求先/仕入先リンク --%>
        <a id="LinkSeikyuuSiireHenkou" style="display: none;" runat="server" href="JavaScript:void(0);">
            <asp:Label ID="lblLinkSeikyuuStr" runat="server" Text="請" Style="padding-left: 0px;
                padding-right: 0px; width: 10px;" />/<asp:Label ID="lblLinkSiireStr" runat="server"
                    Text="仕" /></a
         ><%-- 請求先コード --%><input type="hidden" id="HiddenSeikyuuSakiCd" runat="server" style="display:block; float:left;"
        /><%-- 請求先枝番 --%><input type="hidden" id="HiddenSeikyuuSakiBrc" runat="server" style="display:block; float:left;" 
        /><%-- 請求先区分 --%><input type="hidden" id="HiddenSeikyuuSakiKbn" runat="server" style="display:block; float:left;"
        /><%-- 調査会社コード --%><input type="hidden" id="HiddenTysKaisyaCd" runat="server" style="display:block; float:left;"
        /><%-- 調査会社事業所コード --%><input type="hidden" id="HiddenTysKaisyaJigyousyoCd" runat="server" style="display:block; float:left;" />
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel ID="UpdatePanelSeikyuuSiireInfo" runat="server" RenderMode="Inline"
    UpdateMode="Conditional">
    <ContentTemplate>
          <%-- 加盟店コード --%><input type="hidden" id="HiddenKameitenCd" runat="server" style="display:block; float:left;"
        /><%-- 商品コード --%><input type="hidden" id="HiddenSyouhinCd" runat="server" style="display:block; float:left;"
        /><%-- 基本請求先コード --%><asp:HiddenField ID="HiddenDefaultSeikyuuSaki" runat="server"
        /><%-- 基本仕入先コード --%><asp:HiddenField ID="HiddenDefaultSiireSaki" runat="server"
        /><%-- 工事会社請求チェック --%><input type="hidden" id="HiddenKojKaisyaSeikyuu" runat="server" style="display:block; float:left;"
        /><%-- 工事会社コード --%><input type="hidden" id="HiddenKojKaisyaCd" runat="server" style="display:block; float:left;"
        /><%-- 基本請求先名 --%><asp:HiddenField ID="HiddenDefaultSeikyuuSakiMei" runat="server"
        /><%-- 売上処理済 --%><input type="hidden" id="HiddenUriageSyorizumi" runat="server" style="display:block; float:left;"
        /><%-- 伝票売上年月日 --%><input type="hidden" id="HiddenDenUriDate" runat="server" style="display:block; float:left;"
        /><%-- 表示モード --%><input type="hidden" id="HiddenViewMode" runat="server" style="display:block; float:left;"
        /><%-- 請求締日 --%><input type="hidden" id="HiddenSimeDate" runat="server" style="display:block; float:left;"
        /><%-- 請求先変更チェック--%><input type="hidden" id="HiddenChkSeikyuuSakiChg" runat="server" style="display:block; float:left;" />
    </ContentTemplate>
</asp:UpdatePanel>
<%-- 改行が空白類文字に変換される為、改行不可 --%>  
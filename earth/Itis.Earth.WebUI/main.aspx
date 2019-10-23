<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
  Codebehind="main.aspx.vb" Inherits="Itis.Earth.WebUI.main" Title="EARTH メインページ" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<%@ OutputCache Location="None" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

  <script type="text/javascript" src="js/jhsearth.js">
  </script>

  <script type="text/javascript">
        //window名付与
        var objWin = window;
        objWin.name = "<%=EarthConst.MAIN_WINDOW_NAME %>"
        objWin.focus();
        
        //onload
        window.onload = function(){
            initPage(); //画面初期設定
            changeTableSize("osiraseDiv",30,60);    //お知らせ表示領域のスクロール調整
        }
        
        //onresize
        window.onresize = function(){
            changeTableSize("osiraseDiv",30,60);    //お知らせ表示領域のスクロール調整
        }
                
  </script>

  <input type="hidden" id="gamenId" value="main" runat="server"/>
  <table style="height: 100%;">
    <tr>
      <td style="height: 10px" colspan="4">
        &nbsp;</td>
    </tr>
    <tr>
      <td style="width: 20px">
        &nbsp;</td>
      <td style="vertical-align: top; width: 320px">
        <table cellpadding="8" cellspacing="0" class="menuBar">
          <tr>
            <td colspan="2" class="menuDaiKoumoku">
              業務メニュー</td>
          </tr>
          <tr>
            <td class="menuKoumkuIcon">
              <img alt="" src="images/menu_arrow.gif" /></td>
            <td>
              <a id="LinkSinkiJutyuu" runat="server">新規受注</a>
              <br />
              <ul id="UlSinkiJutyuuList" style="display: none; font-size: 11pt; margin: 0px;" runat="server">
                <li><a id="LinkMousikomiNyuuryoku" runat="server">申込入力</a></li>
                <li><a id="LinkJutyuuTouroku" runat="server">受注登録</a></li>
                <li><a id="LinkMousikomiKensaku" runat="server">申込検索</a> <a target="searchWindowB" id="LinkMousikomiKensakuOW"
                runat="server"><img alt="別ウィンドウで開く" src="images/otherWin.gif" /></a></li>
                <li><a id="LinkFcMousikomiKensaku" runat="server">FC申込検索</a> <a target="searchWindowB" id="LinkFcMousikomiKensakuOW"
                runat="server"><img alt="別ウィンドウで開く" src="images/otherWin.gif" /></a></li>
              </ul>
            </td>
          </tr>
          <tr>
            <td class="menuKoumkuIcon">
              <img alt="" src="images/menu_arrow.gif" /></td>
            <td>
              <a id="LinkMenuBukkenKensaku" runat="server">物件検索</a>
              <br />
              <ul id="UlBukkenKensakuList" style="display: none; font-size: 11pt; margin: 0px;" runat="server">
                <li><a id="LinkBukkenKensaku" runat="server">物件検索</a>&nbsp;
                <a target="searchWindowB" id="LinkBukkenKensakuOW" runat="server"><img alt="別ウィンドウで開く" src="images/otherWin.gif" /></a></li>
                <li><a id="LinkHinsituHosyousyoJyoukyouKensaku" runat="server">品質保証書状況検索</a>&nbsp;
                <a target="searchWindowB" id="LinkHinsituHosyousyoJyoukyouKensakuOW" runat="server"><img alt="別ウィンドウで開く" src="images/otherWin.gif" /></a></li>
              </ul>
            </td>
          </tr>
          <tr>
            <td class="menuKoumkuIcon">
              <img alt="" src="images/menu_arrow.gif" /></td>
            <td>
              <a id="LinkBukkenDirect" runat="server" target="BukkenDirectWin">物件ダイレクト</a></td>
          </tr>
          <tr>
            <td class="menuKoumkuIcon">
              <img alt="" src="images/menu_arrow.gif" /></td>
            <td>
              <a id="LinkSearchKousinRireki" runat="server">更新履歴照会</a> <a target="searchWindowB" id="LinkSearchKousinRirekiOW"
                runat="server">
                <img alt="別ウィンドウで開く" src="images/otherWin.gif" /></a></td>
          </tr>
          <tr>
            <td class="menuKoumkuIcon">
              <img alt="" src="images/menu_arrow.gif" /></td>
            <td><a id="LinkHansokuhinSeikyuu" runat="server">販促品請求</a><a target="searchWindowB"
                  id="LinkHansokuhinSeikyuuOW" runat="server"><img alt="別ウィンドウで開く" src="images/otherWin.gif" /></a></td>
          </tr>
          <tr>
            <td class="menuKoumkuIcon">
              <img alt="" src="images/menu_arrow.gif" /></td>
            <td>
              <a id="LinkKeiriMenu" runat="server">経理メニュー</a><a target="searchWindowB"
                  id="LinkKeiriMenuOW" runat="server"><img alt="別ウィンドウで開く" src="images/otherWin.gif" /></a></td>
          </tr>
          <tr>
            <td class="menuKoumkuIcon">
              <img alt="" src="images/menu_arrow.gif" /></td>
            <td>
              <a id="LinkTyousaMitumoriYouDataSyuturyoku" runat="server">調査見積書作成</a> <a target="searchWindowB" id="LinkTyousaMitumoriYouDataSyuturyokuOW"
                runat="server">
                <img alt="別ウィンドウで開く" src="images/otherWin.gif" /></a></td>
          </tr>
          <tr>
            <td colspan="2" style="padding: 0px; font-size: 1px; height: none;">
              &nbsp;</td>
          </tr>
          <tr>
            <td colspan="2" class="menuDaiKoumoku">
              登録・照会メニュー</td>
          </tr>
          <tr>
          </tr>
          <tr>
            <td class="menuKoumkuIcon">
              <img alt="" src="images/menu_arrow.gif" /></td>
            <td>
              <a id="LinkKameitenSyoukaiTouroku" runat="server">加盟店照会・登録</a> <a target="searchWindowB"
                id="LinkKameitenSyoukaiTourokuOW" runat="server">
                <img alt="別ウィンドウで開く" src="images/otherWin.gif" /></a>
            </td>
          </tr>
          <tr>
            <td class="menuKoumkuIcon">
              <img alt="" src="images/menu_arrow.gif" /></td>
            <td>
              <a id="LinkKameitenTyuuijouhouDirect" runat="server" target="searchWindowB">加盟店注意情報ダイレクト</a>
              <a target="searchWindowB" id="LinkKameitenTyuuijouhouDirectOW" runat="server" style="display: none;">
                <img alt="別ウィンドウで開く" src="images/otherWin.gif" /></a>
            </td>
          </tr>
          <tr>
            <td colspan="2" style="padding: 0px; font-size: 1px; height: none;">
              &nbsp;</td>
          </tr>
          <tr>
            <td colspan="2" class="menuDaiKoumoku">
              営業向けメニュー</td>
          </tr>
          <tr>
            <td class="menuKoumkuIcon">
              <img alt="" src="images/menu_arrow.gif" /></td>
            <td>
              <a id="LinkKameitenSyoukai" runat="server">加盟店照会</a> <a target="searchWindowB" id="LinkKameitenSyoukaiOW"
                runat="server">
                <img alt="別ウィンドウで開く" src="images/otherWin.gif" /></a>
            </td>
          </tr>
          <tr>
            <td class="menuKoumkuIcon">
              <img alt="" src="images/menu_arrow.gif" /></td>
            <td>
              <a id="LinkBukkenSyoukai" runat="server">物件照会</a> <a target="searchWindowB" id="LinkBukkenSyoukaiOW"
                runat="server">
                <img alt="別ウィンドウで開く" src="images/otherWin.gif" /></a>
            </td>
          </tr>
          <tr>
            <td colspan="2">
              <fieldset id="kanriMenuFS">
                <ul id="kanriLinksList" style="padding-top: 15px">
                  <li><a id="LinkHidukeMaster" runat="server">日付マスタ編集</a></li>
                  <li><a id="LinkMasterMaintenance" runat="server">マスタメンテナンス</a></li>
                </ul>
              </fieldset>
            </td>
          </tr>
        </table>
      </td>
      <td style="vertical-align: top;">
        <table cellpadding="3" cellspacing="0" class="osiraseTable">
          <tr>
            <td>
            </td>
            <td colspan="2" style="font-size: 17px; font-weight: bold">
              ～お知らせ～</td>
          </tr>
          <tr>
            <td colspan="3" style="height: 15px;">
            </td>
          </tr>
        </table>
        <div id="osiraseDiv" style="overflow: auto; height: 300px; width: 300px;">
          <table cellpadding="3" cellspacing="0" class="osiraseTable">
            <thead>
            </thead>
            <tbody id="osiraseTbody" class="osiraseTbody" runat="server">
            </tbody>
          </table>
        </div>
      </td>
    </tr>
  </table>
</asp:Content>

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SearchTyousakaisya.aspx.vb"
  Inherits="Itis.Earth.WebUI.SearchTyousakaisya" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <title>EARTH 調査会社マスタ検索</title>
</head>

<script type="text/javascript" src="js/jhsearth.js">
</script>

<script type="text/javascript">
    try{
        window.resizeTo(800,500);
    }catch(e){
        //アクセスが拒否されましたのエラーが出たら何もしない。
        if(e.number == 2147024891){
            throw e;
        }
    }

    //onload追加処理
    function funcAfterOnload(){
        //検索結果が1件のみの場合、値を戻す処理を実行
        if (objEBI("<%= firstSend.clientID %>").value != ""){
            returnSelectValue(objEBI(objEBI("<%= firstSend.clientID %>").value));
        }
    }
    
    /**
     * Allクリア処理後に実行されるファンクション
     * @return 
     */
    function funcAfterAllClear(obj){
        objEBI("<%= maxSearchCount.clientID %>").value = "100";
        objEBI("<%= tyousakaisyaCd.clientID %>").focus();
    }

</script>

<body>
  <form method="post" id="mstSearch" runat="server">
    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
      class="titleTable">
      <tbody>
        <tr>
          <th id="ThGamenTitle" runat="server">
            調査会社マスタ{0}検索</th>
          <th style="text-align: right;">
          </th>
        </tr>
        <tr>
          <td colspan="2" rowspan="1">
            <input id="btnCloseWin" value="閉じる" type="button" />&nbsp;<input id="clearWin" value="クリア"
              type="reset" /></td>
        </tr>
      </tbody>
    </table>
    <br />
    <table style="text-align: left;" class="mainTable" cellpadding="2">
      <thead>
        <tr>
          <th class="tableTitle" colspan="4" rowspan="1">
            検索条件</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td>
            調査会社コード</td>
          <td>
            <input size="100" id="tyousakaisyaCd" maxlength="7" style="width: 100px; ime-mode: disabled;"
              runat="server" /></td>
        </tr>
        <tr>
          <td>
            調査会社カナ名</td>
          <td>
            <input size="100" id="tyousakaisyaKanaNm" maxlength="20" style="width: 500px; ime-mode: active;"
              runat="server" /></td>
        </tr>
        <tr>
          <td style="text-align: center;" colspan="2" rowspan="1">
            検索上限件数<select id="maxSearchCount" runat="server">
              <option value="10">10件</option>
              <option value="100" selected="selected">100件</option>
            </select>
            <input id="search" value="検索実行" type="submit" runat="server" /></td>
        </tr>
      </tbody>
    </table>
    <input type="hidden" id="returnTargetIds" runat="server" />
    <input type="hidden" id="afterEventBtnId" runat="server" />
    <input type="hidden" id="firstSend" runat="server" />
    <input type="hidden" id="KameitenCd" runat="server" />
    <input type="hidden" id="HidGamenType" runat="server" />
  </form>
  <table style="height: 30px">
    <tr>
      <td>
        検索結果：
      </td>
      <td id="resultCount" runat="server">
      </td>
      <td>
        件
      </td>
    </tr>
  </table>
  <div class="dataGridHeader" id="dataGridContent">
    <table class="scrolltablestyle" cellpadding="0" cellspacing="0" style="width: 720px">
      <thead>
        <tr id="meisaiTableHeaderTr" style="position: relative; top: expression(this.offsetParent.scrollTop);">
          <th class="searchReturnValues">
          </th>
          <th style="width: 100px;">
            調査会社コード</th>
          <th style="width: 300px;">
            調査会社名</th>
          <th style="width: 300px;">
            調査会社カナ名&nbsp;</th>
        </tr>
      </thead>
      <tbody id="searchGrid" runat="server">
      </tbody>
    </table>
  </div>
</body>
</html>

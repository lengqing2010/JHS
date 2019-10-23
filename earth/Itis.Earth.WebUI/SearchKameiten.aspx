<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SearchKameiten.aspx.vb"
    Inherits="Itis.Earth.WebUI.SearchKameiten" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>EARTH 加盟店検索</title>
</head>

<script type="text/javascript" src="js/jhsearth.js">
</script>

<script type="text/javascript">
    try{
        window.resizeTo(800,600);
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
        objEBI("<%= kameitenCd.clientID %>").focus();
    }

</script>

<body>
    <form method="post" id="mstSearch" runat="server">
        <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
            class="titleTable">
            <tbody>
                <tr>
                    <th>
                        加盟店検索</th>
                    <th style="text-align: right;">
                    </th>
                </tr>
                <tr>
                    <td colspan="2" rowspan="1">
                        <input id="btnCloseWin" value="閉じる" type="button" runat="server" />&nbsp;<input id="clearWin"
                            value="クリア" type="reset" runat="server" /></td>
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
                        加盟店コード</td>
                    <td>
                        <input size="100" id="kameitenCd" maxlength="5" style="width: 100px; ime-mode: disabled;"
                            runat="server" />
                        <input type="hidden" id="kubun" runat="server" /></td>
                </tr>
                <tr>
                    <td>
                        加盟店カナ名</td>
                    <td>
                        <input size="100" id="kameitenKanaNm" maxlength="40" style="width: 500px; ime-mode: active;"
                            runat="server" /></td>
                </tr>
                <tr>
                    <td>
                        加盟店登録住所</td>
                    <td>
                        <input size="100" id="TextKameitenTourokuJyuusyo" maxlength="70" style="width: 500px; ime-mode: active;"
                            runat="server" /></td>
                </tr>
                <tr>
                    <td>
                        加盟店電話番号</td>
                    <td>
                        <input size="100" id="TextKameitenTelNo" maxlength="16" style="width: 200px; ime-mode: disabled;"
                            runat="server" /></td>
                </tr>
                <tr>
                    <td style="text-align: center;" colspan="2" rowspan="1">
                        検索上限件数<select id="maxSearchCount" runat="server">
                            <option value="10">10件</option>
                            <option value="100" selected="selected">100件</option>
                        </select>
                        <input id="search" value="検索実行" type="submit" runat="server" />&nbsp;</td>
                </tr>
            </tbody>
        </table>
        <input type="hidden" id="returnTargetIds" runat="server" />
        <input type="hidden" id="afterEventBtnId" runat="server" />
        <input type="hidden" id="firstSend" runat="server" />
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
        <table class="scrolltablestyle" cellpadding="0" cellspacing="0" style="width: 800px">
            <thead>
                <tr id="meisaiTableHeaderTr" style="position: relative; top: expression(this.offsetParent.scrollTop);">
                    <th class="searchReturnValues">
                    </th>
                    <th style="width: 80px;">
                        加盟店コード</th>
                    <th style="width: 320px;">
                        加盟店名</th>
                    <th style="width: 100px;">
                        都道府県名&nbsp;</th>
                    <th style="width: 300px;">
                        加盟店カナ名&nbsp;</th>
                </tr>
            </thead>
            <tbody id="searchGrid" runat="server">
            </tbody>
        </table>
    </div>
</body>
</html>

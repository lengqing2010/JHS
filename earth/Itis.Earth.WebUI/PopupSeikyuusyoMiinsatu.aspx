<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopupSeikyuusyoMiinsatu.aspx.vb"
    Inherits="Itis.Earth.WebUI.PopupSeikyuusyoMiinsatu" Title="EARTH 請求書未印刷一覧" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>

<script type="text/javascript" src="js/jhsearth.js">
</script>
    
<script src="js/sortable_ja.js" type="text/javascript">
</script>

<script type="text/javascript">
        history.forward();
      
        //ウィンドウサイズ変更
        try{
            window.resizeTo(816,354);
        }catch(e){
            //アクセスが拒否されましたのエラーが出たら何もしない。
            if(e.number == 2147024891) throw e;
        } 
                       
        _d = document;
        
        //該当データなしメッセージ制御用フラグ
        var gNoDataMsgFlg = null;
        
        /*********************************************
        * 戻り値がない為、同メソッドをオーバーライド
        *********************************************/
        function returnSelectValue(){
            return false;
        }


</script>

<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="SM1" runat="server">
        </asp:ScriptManager>        
        <div>
            <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
                class="titleTable">
                <tbody>
                    <tr>
                        <th style="text-align: left; width: 180px;">
                            請求書未印刷一覧</th>
                        <th>
                            <input type="button" id="BtnClose" value="閉じる"  onclick="window.close();"
                                runat="server" tabindex="10" />
                        </th>
                    </tr>
                </tbody>
            </table>
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
                <table class="scrolltablestyle2 sortableTitle" id="meisaiTable" cellpadding="0" cellspacing="0">
                    <thead>
                        <tr id="meisaiTableHeaderTr" runat="server" style="position: relative; top: expression(this.offsetParent.scrollTop)">
                            <th style="width: 100px;">
                                請求先コード</th>
                            <th style="width: 280px;">
                                請求先名</th>
                            <th style="width: 75px;">
                                請求締め日</th>
                            <th style="width: 92px;">
                                請求書発行日</th>
                            <th style="width: 180px;">
                                請求書式</th>
                        </tr>
                    </thead>
                    <tbody id="searchGrid" runat="server">
                    </tbody>
                </table>
            </div>
        </div>
        <input type="hidden" id="sendTargetWin" runat="server" />
    </form>
</body>    
</html>

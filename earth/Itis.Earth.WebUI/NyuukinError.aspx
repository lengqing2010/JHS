<%@ Page Language="vb" AutoEventWireup="false" Codebehind="NyuukinError.aspx.vb"
    Inherits="Itis.Earth.WebUI.NyuukinError" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>EARTH 入金エラー確認</title>

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script type="text/javascript">
        window.resizeTo(800,500);
        /**
         * テーブルの表示、イベント設定
         * @return 
         * @throws 
         * @param objGridTBody :対象のテーブルTbodyオブジェクト
         * ※"jhsearth.js"の"initGridTable"からダブルクリックイベントと
         *   エンターキー押下時のイベントを抜いたもの
         */
        function funcAfterOnload(){
          objGridTBody = objEBI("errorGrid");
          //指定されたテーブルTbodyオブジェクトが存在する場合
          if (objGridTBody != null) {
            if(IE){
              objGridTBody.onmousedown = function() {
                selectedLineColor(event.srcElement.parentNode,objGridTBody);
              };
            }else{
              objGridTBody.onmousedown = function(event) {
                selectedLineColor(event.target.parentNode,objGridTBody);
              };
            }
            objGridTBody.onkeydown = function() {
              if(event.keyCode == 40){  //カーソルキー(下)
                selectedLineMove(1);
              }
              if(event.keyCode == 38){  //カーソルキー(上)
                selectedLineMove(-1);
              }
            };
            
            //テーブルの行色設定
            setGridColor(objGridTBody);
            
            //onresizeイベントに何も登録されていない場合
            if(window.onresize == null || window.onresize == ""){
              //テーブルサイズ調整実行
              changeTableSize("dataGridContent");
              //onresizeイベントハンドラにテーブルのサイズ調整を設定
              window.onresize = function(){
                changeTableSize("dataGridContent"); 
              }
            }

          }else{
            return false;
          }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table class="titleTable" style="width: 100%; text-align: left" cellspacing="2" cellpadding="0"
            border="0">
            <tbody>
                <tr>
                    <th>
                        <asp:Label ID="lblTitle" runat="server" Text="入金エラー確認"></asp:Label></th>
                    <th style="text-align: right">
                    </th>
                </tr>
                <tr>
                </tr>
                <tr>
                    <td colspan="2">
                        <input id="btnCloseWin" type="button" value="閉じる" name="btnCloseWin" />&nbsp;
                    </td>
                </tr>
            </tbody>
        </table>
        
        <br />
        
        <table class="mainTable" style="text-align: left" cellpadding="2">
            <tbody>
                <tr>
                    <td class="koumokuMei">
                        取込日時</td>
                    <td>
                        <input name="" type="text" id="textTorikomiDate" runat="server" class="date readOnlyStyle"
                            value="2009/06/01 10:35:52" style="width: 120px;" readonly="readonly" tabindex="-1" />
                    </td>
                </tr>
                <tr>
                    <td class="koumokuMei">
                        取込ファイル名</td>
                    <td>
                        <input name="" type="text" id="textTorikomiFileName" runat="server" class="readOnlyStyle"
                            value="ZZZZ.csv" readonly="readonly" tabindex="-1" />
                    </td>
                </tr>
            </tbody>
        </table>
        
        <table style="height: 30px" class="subTable">
            <tr>
                <td>
                    検索結果：
                </td>
                <td id="resultCount" runat="server">
                    1
                </td>
                <td>
                    件
                </td>
            </tr>
        </table>
        
        <div class="dataGridHeader" id="dataGridContent">
            <table class="scrolltablestyle" style="width: 600px" cellspacing="0" cellpadding="0">
                <thead>
                    <tr id="meisaiTableHeaderTr" style="position: relative; top: expression(this.offsetParent.scrollTop);">
                        <th class="searchReturnValues">
                        </th>
                        <th>
                            行番号
                        </th>
                        <th>
                            グループコード</th>
                        <th>
                            顧客コード</th>
                        <th>
                            摘要</th>
                        <th>
                            入金額</th>
                        <th>
                            商品コード</th>
                    </tr>
                </thead>
                <tbody id="errorGrid" runat="server">
                </tbody>
            </table>
        </div>
    </form>
</body>
</html>

<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
  Codebehind="GetujiIkkatuSyuusei.aspx.vb" Inherits="Itis.Earth.WebUI.GetujiIkkatuSyuusei"
  Title="EARTH 月次データ一括修正" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

  <script type="text/javascript" src="js/jhsearth.js">
  </script>

  <script type="text/javascript">
    function executeConfirm(objCtrl){
        var objExeBtn = null;

        if(objCtrl == objEBI("<%= BtnGetujiSyoriCall.clientID %>")){
            if(!confirm('<%= Messages.MSG042C.Replace("@PARAM1", "月次処理") %>')){
                return false;
            }
            objExeBtn = objEBI("<%= ButtonGetujiSyori.clientID %>");

        }else if(objCtrl == objEBI("<%= BtnKessanSyoriCall.clientID %>")){
            if(!confirm('<%= Messages.MSG042C.Replace("@PARAM1", "決算月処理") %>')){
                return false;
            }
            objExeBtn = objEBI("<%= ButtonKessanSyori.clientID %>");

        }else if(objCtrl == objEBI("<%= ButtonKakuteiYoyaku.clientID %>")){
            if(!confirm('<%= Messages.MSG042C.Replace("@PARAM1", "月次確定処理の予約") %>')){
                return false;
            }
            objExeBtn = objEBI("<%= ButtonKakuteiYoyakuExe.clientID %>");

        }else if(objCtrl == objEBI("<%= ButtonKakuteiYoyakuKaijo.clientID %>")){
            if(!confirm('<%= Messages.MSG042C.Replace("@PARAM1", "月次確定処理の予約解除") %>')){
                return false;
            }
            objExeBtn = objEBI("<%= ButtonKakuteiYoyakuKaijoExe.clientID %>");
        }

        //画面グレイアウト
        setWindowOverlay(objCtrl);
        //実行ボタン押下
        objExeBtn.click();
    }
  </script>

  <!-- 画面中央部 -->
  <table>
    <tr style="vertical-align: top">
      <td>
        <!-- 画面上部・ヘッダ -->
        <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
          class="titleTable">
          <tbody>
            <tr>
              <th style="text-align: left">
                月次データ一括修正
              </th>
            </tr>
            <tr>
              <td style="height: 10px">
              </td>
            </tr>
          </tbody>
        </table>
        <table style="text-align: left; width: 450px; height: 120px;" id="" class="mainTable"
          cellpadding="1" cellspacing="1">
          <!-- 1行目 -->
          <tr>
            <td style="width: 150px; height: 30px;" class="koumokuMei">
              売上年月日
            </td>
            <td>
              <input name="" type="text" id="TextUriageFrom" maxlength="10" class="date" runat="server"
                onblur="checkDate(this);" />
              &nbsp;～&nbsp;
              <input name="" type="text" id="TextUriageTo" maxlength="10" class="date" runat="server"
                onblur="checkDate(this);" />
            </td>
          </tr>
          <!-- 2行目 -->
          <tr>
            <td style="width: 150px; height: 30px;" class="koumokuMei">
              請求書発行日
            </td>
            <td>
              <input name="" type="text" id="TextSeikyuuFrom" maxlength="10" class="date" runat="server"
                onblur="checkDate(this);" />
              &nbsp;～&nbsp;
              <input name="" type="text" id="TextSeikyuuTo" maxlength="10" class="date" runat="server"
                onblur="checkDate(this);" />
            </td>
          </tr>
          <!-- 3行目 -->
          <tr>
            <td colspan="2" style="text-align: center;">
              <div class="InfoText">
                未計上の工事・追加工事の売上年月日・請求書発行日<br />
                が指定期間内で、工事日年月より前の年月の売上年月<br />
                日を前月末に変更します<br />
                （月初に実施します・何度でも実行できます）<br />
              </div>
              <div>
                <table class="InfoArea">
                  <tr>
                    <td style="text-align: left;">
                      例）売上年月日&nbsp;&nbsp;&nbsp;2009/06/01～2009/06/03<br />
                      &nbsp;&nbsp;&nbsp;&nbsp;請求書発行日&nbsp;2009/06/30～2009/06/30<br />
                      &nbsp;&nbsp;&nbsp;&nbsp;と入力した場合<br />
                      &nbsp;&nbsp;&nbsp;&nbsp;売上年月日が6/1～3の物件は2009/05/31<br />
                      &nbsp;&nbsp;&nbsp;&nbsp;請求書発行日が6/30の物件は2009/05/31<br />
                      &nbsp;&nbsp;&nbsp;&nbsp;に変更になる。
                    </td>
                  </tr>
                </table>
              </div>
              <br />
              <input type="button" id="BtnGetujiSyoriCall" value="月次処理" runat="server" style="font-size: 15px;
                width: 200px; color: black; height: 30px;" onclick="executeConfirm(this);" /><br />
              <input type="button" id="ButtonGetujiSyori" value="月次処理" runat="server" style="display: none;" /><br />
              <br />
            </td>
          </tr>
        </table>
        <br />
        <!-- 画面下部・ボタン -->
        <table style="text-align: left; width: 450px;" border="0" cellpadding="0" cellspacing="2"
          class="titleTable">
          <tbody>
            <tr>
              <td style="text-align: center;">
                <div class="InfoText">
                  未計上で売上年月日が3/1～3/31または9/1～9/30で<br />
                  請求書発行日が4/1～4/30または10/1～10/30の売上<br />
                  年月日・請求書発行日を3/31または9/30に変更します<br />
                  （4月初・10月初に実施します・何度でも実行できます）<br />
                </div>
                <input name="btnKessan" type="button" id="BtnKessanSyoriCall" value="決算月処理" runat="server"
                  style="font-size: 15px; width: 200px; color: black; height: 30px;" disabled="disabled"
                  onclick="executeConfirm(this);" /><br />
                <input name="btnKessan" type="button" id="ButtonKessanSyori" value="決算月処理" runat="server"
                  style="display: none;" disabled="disabled" />
              </td>
            </tr>
          </tbody>
        </table>
      </td>
      <td>
        <img src="images/spacer.gif" alt="" style="width: 30px; height: 0px;" />
      </td>
      <td>
        <!-- 画面上部・ヘッダ -->
        <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
          class="titleTable">
          <tbody>
            <tr>
              <th style="text-align: left">
                月次確定処理予約
              </th>
            </tr>
            <tr>
              <td style="height: 10px">
              </td>
            </tr>
          </tbody>
        </table>
        <table style="text-align: left; width: 450px; height: 120px;" id="Table1" class="mainTable"
          cellpadding="1" cellspacing="1">
          <!-- 1行目 -->
          <tr>
            <td style="width: 150px; height: 30px;" class="koumokuMei">
              確定処理対象年月
            </td>
            <td>
              <input name="" type="text" id="TextKakuteiYM" maxlength="7" class="date readOnlyStyle" runat="server" readonly/>
            </td>
          </tr>
          <tr>
            <td style="width: 150px; height: 30px;" class="koumokuMei">
              現在の処理状況
            </td>
            <td>
              <input name="" type="text" id="TextKakuteiSyoriJoukyou" class="readOnlyStyle" style="width:80px" runat="server" readonly/>
            </td>
          </tr>
          <!-- 3行目 -->
          <tr>
            <td colspan="2" style="text-align: center;">
              <div class="InfoText">
                上記対象年月の売掛金、買掛金を確定し、各残高を<br />
                月単位に集計します。<br />
                「月次確定処理予約ボタン」押下により、処理の実行<br />
                が予約され、本日営業時間外(夜間)に実行されます。<br />
                処理実行前であれば、予約の解除が可能です。<br />
                (「月次確定処理予約解除ボタン」を押下してください。)<br />
              </div>
              <br />
              <input type="button" id="ButtonKakuteiYoyaku" value="月次確定処理予約" runat="server" style="font-size: 15px;
                width: 200px; color: black; height: 30px;" onclick="executeConfirm(this);" /><br />
              <input type="button" id="ButtonKakuteiYoyakuKaijo" value="月次確定処理予約解除" runat="server"
                style="font-size: 15px; width: 200px; color: black; height: 30px;" onclick="executeConfirm(this);" /><br />
              <input type="button" id="ButtonKakuteiYoyakuExe" value="月次確定処理予約Exe" runat="server"
                style="display: none;" />
              <input type="button" id="ButtonKakuteiYoyakuKaijoExe" value="月次確定処理解除Exe" runat="server"
                style="display: none;" />
              <br />
            </td>
          </tr>
        </table>
      </td>
    </tr>
  </table>
</asp:Content>

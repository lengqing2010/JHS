<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
  Codebehind="GetujiIkkatuSyuusei.aspx.vb" Inherits="Itis.Earth.WebUI.GetujiIkkatuSyuusei"
  Title="EARTH �����f�[�^�ꊇ�C��" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

  <script type="text/javascript" src="js/jhsearth.js">
  </script>

  <script type="text/javascript">
    function executeConfirm(objCtrl){
        var objExeBtn = null;

        if(objCtrl == objEBI("<%= BtnGetujiSyoriCall.clientID %>")){
            if(!confirm('<%= Messages.MSG042C.Replace("@PARAM1", "��������") %>')){
                return false;
            }
            objExeBtn = objEBI("<%= ButtonGetujiSyori.clientID %>");

        }else if(objCtrl == objEBI("<%= BtnKessanSyoriCall.clientID %>")){
            if(!confirm('<%= Messages.MSG042C.Replace("@PARAM1", "���Z������") %>')){
                return false;
            }
            objExeBtn = objEBI("<%= ButtonKessanSyori.clientID %>");

        }else if(objCtrl == objEBI("<%= ButtonKakuteiYoyaku.clientID %>")){
            if(!confirm('<%= Messages.MSG042C.Replace("@PARAM1", "�����m�菈���̗\��") %>')){
                return false;
            }
            objExeBtn = objEBI("<%= ButtonKakuteiYoyakuExe.clientID %>");

        }else if(objCtrl == objEBI("<%= ButtonKakuteiYoyakuKaijo.clientID %>")){
            if(!confirm('<%= Messages.MSG042C.Replace("@PARAM1", "�����m�菈���̗\�����") %>')){
                return false;
            }
            objExeBtn = objEBI("<%= ButtonKakuteiYoyakuKaijoExe.clientID %>");
        }

        //��ʃO���C�A�E�g
        setWindowOverlay(objCtrl);
        //���s�{�^������
        objExeBtn.click();
    }
  </script>

  <!-- ��ʒ����� -->
  <table>
    <tr style="vertical-align: top">
      <td>
        <!-- ��ʏ㕔�E�w�b�_ -->
        <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
          class="titleTable">
          <tbody>
            <tr>
              <th style="text-align: left">
                �����f�[�^�ꊇ�C��
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
          <!-- 1�s�� -->
          <tr>
            <td style="width: 150px; height: 30px;" class="koumokuMei">
              ����N����
            </td>
            <td>
              <input name="" type="text" id="TextUriageFrom" maxlength="10" class="date" runat="server"
                onblur="checkDate(this);" />
              &nbsp;�`&nbsp;
              <input name="" type="text" id="TextUriageTo" maxlength="10" class="date" runat="server"
                onblur="checkDate(this);" />
            </td>
          </tr>
          <!-- 2�s�� -->
          <tr>
            <td style="width: 150px; height: 30px;" class="koumokuMei">
              ���������s��
            </td>
            <td>
              <input name="" type="text" id="TextSeikyuuFrom" maxlength="10" class="date" runat="server"
                onblur="checkDate(this);" />
              &nbsp;�`&nbsp;
              <input name="" type="text" id="TextSeikyuuTo" maxlength="10" class="date" runat="server"
                onblur="checkDate(this);" />
            </td>
          </tr>
          <!-- 3�s�� -->
          <tr>
            <td colspan="2" style="text-align: center;">
              <div class="InfoText">
                ���v��̍H���E�ǉ��H���̔���N�����E���������s��<br />
                ���w����ԓ��ŁA�H�����N�����O�̔N���̔���N��<br />
                ����O�����ɕύX���܂�<br />
                �i�����Ɏ��{���܂��E���x�ł����s�ł��܂��j<br />
              </div>
              <div>
                <table class="InfoArea">
                  <tr>
                    <td style="text-align: left;">
                      ��j����N����&nbsp;&nbsp;&nbsp;2009/06/01�`2009/06/03<br />
                      &nbsp;&nbsp;&nbsp;&nbsp;���������s��&nbsp;2009/06/30�`2009/06/30<br />
                      &nbsp;&nbsp;&nbsp;&nbsp;�Ɠ��͂����ꍇ<br />
                      &nbsp;&nbsp;&nbsp;&nbsp;����N������6/1�`3�̕�����2009/05/31<br />
                      &nbsp;&nbsp;&nbsp;&nbsp;���������s����6/30�̕�����2009/05/31<br />
                      &nbsp;&nbsp;&nbsp;&nbsp;�ɕύX�ɂȂ�B
                    </td>
                  </tr>
                </table>
              </div>
              <br />
              <input type="button" id="BtnGetujiSyoriCall" value="��������" runat="server" style="font-size: 15px;
                width: 200px; color: black; height: 30px;" onclick="executeConfirm(this);" /><br />
              <input type="button" id="ButtonGetujiSyori" value="��������" runat="server" style="display: none;" /><br />
              <br />
            </td>
          </tr>
        </table>
        <br />
        <!-- ��ʉ����E�{�^�� -->
        <table style="text-align: left; width: 450px;" border="0" cellpadding="0" cellspacing="2"
          class="titleTable">
          <tbody>
            <tr>
              <td style="text-align: center;">
                <div class="InfoText">
                  ���v��Ŕ���N������3/1�`3/31�܂���9/1�`9/30��<br />
                  ���������s����4/1�`4/30�܂���10/1�`10/30�̔���<br />
                  �N�����E���������s����3/31�܂���9/30�ɕύX���܂�<br />
                  �i4�����E10�����Ɏ��{���܂��E���x�ł����s�ł��܂��j<br />
                </div>
                <input name="btnKessan" type="button" id="BtnKessanSyoriCall" value="���Z������" runat="server"
                  style="font-size: 15px; width: 200px; color: black; height: 30px;" disabled="disabled"
                  onclick="executeConfirm(this);" /><br />
                <input name="btnKessan" type="button" id="ButtonKessanSyori" value="���Z������" runat="server"
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
        <!-- ��ʏ㕔�E�w�b�_ -->
        <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
          class="titleTable">
          <tbody>
            <tr>
              <th style="text-align: left">
                �����m�菈���\��
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
          <!-- 1�s�� -->
          <tr>
            <td style="width: 150px; height: 30px;" class="koumokuMei">
              �m�菈���Ώ۔N��
            </td>
            <td>
              <input name="" type="text" id="TextKakuteiYM" maxlength="7" class="date readOnlyStyle" runat="server" readonly/>
            </td>
          </tr>
          <tr>
            <td style="width: 150px; height: 30px;" class="koumokuMei">
              ���݂̏�����
            </td>
            <td>
              <input name="" type="text" id="TextKakuteiSyoriJoukyou" class="readOnlyStyle" style="width:80px" runat="server" readonly/>
            </td>
          </tr>
          <!-- 3�s�� -->
          <tr>
            <td colspan="2" style="text-align: center;">
              <div class="InfoText">
                ��L�Ώ۔N���̔��|���A���|�����m�肵�A�e�c����<br />
                ���P�ʂɏW�v���܂��B<br />
                �u�����m�菈���\��{�^���v�����ɂ��A�����̎��s<br />
                ���\�񂳂�A�{���c�Ǝ��ԊO(���)�Ɏ��s����܂��B<br />
                �������s�O�ł���΁A�\��̉������\�ł��B<br />
                (�u�����m�菈���\������{�^���v���������Ă��������B)<br />
              </div>
              <br />
              <input type="button" id="ButtonKakuteiYoyaku" value="�����m�菈���\��" runat="server" style="font-size: 15px;
                width: 200px; color: black; height: 30px;" onclick="executeConfirm(this);" /><br />
              <input type="button" id="ButtonKakuteiYoyakuKaijo" value="�����m�菈���\�����" runat="server"
                style="font-size: 15px; width: 200px; color: black; height: 30px;" onclick="executeConfirm(this);" /><br />
              <input type="button" id="ButtonKakuteiYoyakuExe" value="�����m�菈���\��Exe" runat="server"
                style="display: none;" />
              <input type="button" id="ButtonKakuteiYoyakuKaijoExe" value="�����m�菈������Exe" runat="server"
                style="display: none;" />
              <br />
            </td>
          </tr>
        </table>
      </td>
    </tr>
  </table>
</asp:Content>

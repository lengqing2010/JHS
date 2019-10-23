<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
  Codebehind="SiharaiSakiMototyou.aspx.vb" Inherits="Itis.Earth.WebUI.SiharaiSakiMototyou"
  Title="EARTH �x���挳���\���E�o�͉��" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

  <script type="text/javascript" src="js/jhsearth.js">
  </script>

  <script type="text/javascript">
        history.forward();

        /*====================================
         *�O���[�o���ϐ��錾�i��ʕ��i�j
         ====================================*/
        //��ʕ\�����i
        var objShriSakiMei = null;
        var objNengappiFrom = null;
        var objNengappiTo = null;
        //�������s�p
        var objSearch = null;
        var objCsv = null;
        //hidden
        var objKubunVal = null;


        /*************************************
         * onload���̒ǉ�����
         *************************************/
        function funcAfterOnload() {
            //��ʕ\�����i�Z�b�g
            setGlobalObj();
                        
            /*�������ʃe�[�u�� �e�탌�C�A�E�g�ݒ�*/
            settingResultTable();
            
            // CSV�o�͂��s�Ȃ�
            if (objCsvOutPutFlg.value == "1"){
                // CSV�o�̓t���O���N���A
                objCsvOutPutFlg.value = ""
                //�����m�F
                if(!confirm("<%= Messages.MSG017C %>".replace("����","CSV�o�͏���"))){return false};
                //CSV���s
                objCsv.click();
            }
        }
        
       /*********************************************
        * �߂�l���Ȃ��ׁA�����\�b�h���I�[�o�[���C�h
        *********************************************/
        function returnSelectValue(){
            return false;
        }
        
       /*********************************************
        * �l���`�F�b�N���A�Ώۂ��N���A����
        *********************************************/
        function clrName(obj,targetId){
            if(obj.value == "") objEBI(targetId).value="";
        }
        
       /*********************************************
        * ��ʕ\�����i�I�u�W�F�N�g���O���[�o���ϐ���
        *********************************************/
        function setGlobalObj() {
            //��ʕ\�����i
            objShriSakiMei = objEBI("<%= TextShriSakiMei.clientID %>")
            objNengappiFrom = objEBI("<%= TextNengappiFrom.clientID %>");
            objNengappiTo = objEBI("<%= TextNengappiTo.clientID %>");
            //�������s�p
            objSearch = objEBI("<%= ButtonHiddenDisplay.clientID %>");
            // CSV�o�͗p
            objCsv = objEBI("<%= ButtonHiddenCsv.clientID %>");
            objCsvOutPutFlg = objEBI("<%= HiddenCsvOutPut.clientID %>");
            // ����o�͗p
            objPrint = objEBI("<%= ButtonHiddenPrint.clientID %>");
            objPrintOutPutFlg = objEBI("<%= HiddenPrintOutPut.clientID %>");
        }

        /*
        * �������ʃe�[�u�� �e�탌�C�A�E�g�ݒ�
        */
        function settingResultTable(type){
            var tableTitle1 = objEBI("<%=TableTitleTable1.clientID %>");    // �w�b�_�����e�[�u��
            var tableTitle2 = objEBI("<%=TableTitleTable2.clientID %>");    // �w�b�_���E�e�[�u��
            var tableData1 = objEBI("<%=TableDataTable1.clientID %>");      // �f�[�^�����e�[�u��
            var tableData2 = objEBI("<%=TableDataTable2.clientID %>");      // �f�[�^���E�e�[�u��
            var divTitle1 = objEBI("<%=DivLeftTitle.clientID %>");          // �w�b�_����DIV
            var divTitle2 = objEBI("<%=DivRightTitle.clientID %>");         // �w�b�_���EDIV
            var divData1 = objEBI("<%=DivLeftData.clientID %>");            // �f�[�^����DIV
            var divData2 = objEBI("<%=DivRightData.clientID %>");           // �f�[�^���EDIV
            var minHeight = 120;                                            // �E�B���h�E���T�C�Y���̌������ʃe�[�u���ɐݒ肷��Œፂ��
            var adjustHeight = 55;                                          // ��������(�傫�����A�������ʃe�[�u�����Ⴍ�Ȃ�)
            var adjustWidth = 600;                                          // ������(�傫�����A�������ʃe�[�u���������Ȃ�)

            //�Œ��L��̌������ʃe�[�u���p���C�A�E�g���ݒ菈��
            settingResultTableForColumnFix(type
                                            , tableTitle1
                                            , tableTitle2
                                            , tableData1
                                            , tableData2
                                            , divTitle1
                                            , divTitle2
                                            , divData1
                                            , divData2
                                            , minHeight
                                            , adjustHeight
                                            , adjustWidth)
        }

        /*******************************************
         * All�N���A������Ɏ��s�����t�@���N�V����
         *******************************************/
        function funcAfterAllClear(obj){

        }
        
        
        /***********************************
         * �u�������s�v�������̃`�F�b�N����
         ***********************************/
        function checkJikkou(varAction){

            //�N���� �召�`�F�b�N
            if(!checkDaiSyou(objNengappiFrom,objNengappiTo,"�N����")){return false};
            if(varAction == "0"){
                //�������s
                objSearch.click();
                
            }else if(varAction == "1"){
                    objCsvOutPutFlg.value = "1"
                    //�������s
                    objSearch.click();

            }else if(varAction == "2"){
                    //�������s
                    objPrint.click();
            }
        }

        /*********************
         * �召�`�F�b�N
         *********************/
        function checkDaiSyou(objFrom,objTo,mess){
            //�o���Ή��f�[�^���`�F�b�N
            if(objFrom.value != "" ){
                if(Number(removeSlash(objFrom.value)) < Number(removeSlash("<%=EarthConst.KEIRI_DATA_MIN_DATE %>"))){
                    alert("<%= Messages.MSG179W %>");
                    objFrom.select();
                    return false;
                }
            }
            //From,To�̔�r
            if(objFrom.value != "" && objTo.value != ""){
                if(Number(removeSlash(objFrom.value)) > Number(removeSlash(objTo.value))){
                    alert("<%= Messages.MSG022E %>".replace("@PARAM1",mess));
                    objFrom.select();
                    return false;
                }
            }
            return true;
        }
        
        /**
         * �X�N���[������
         * @return 
         */
        function syncScroll(type,obj){
            if(type==1){
                //�����X�N���[����
                objEBI("<%=DivLeftTitle.clientID %>").scrollLeft=obj.scrollLeft;
                objEBI("<%=DivRightData.clientID %>").scrollTop=obj.scrollTop;
            }else if(type==2){
                //�E���X�N���[����
                objEBI("<%=DivRightTitle.clientID %>").scrollLeft=obj.scrollLeft;
                objEBI("<%=DivLeftData.clientID %>").scrollTop=obj.scrollTop;
            }
        }

  </script>

  <%--CSV�o�͔��f--%>
  <asp:HiddenField ID="HiddenCsvOutPut" runat="server" />
  <%--����o�͔��f--%>
  <asp:HiddenField ID="HiddenPrintOutPut" runat="server" />
  <%-- �ǂ̏��i��\�����i��\���j --%>
  <asp:HiddenField ID="HiddenTargetId" runat="server" />
  <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
    class="titleTable">
    <tbody>
      <tr>
        <th>
          �x���挳���\���E�o�͉��</th>
        <th style="text-align: right;">
        </th>
      </tr>
    </tbody>
  </table>
  <br />
  <table style="text-align: left;" cellpadding="2">
    <tr>
      <td>
        <table style="text-align: left;" class="mainTable" cellpadding="2">
          <thead>
            <tr>
              <th class="tableTitle" colspan="4" rowspan="1">
                �����E�o�͏���
                <input id="btnClearWin" value="�N���A" type="reset" class="button" /></th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td class="koumokuMei">
                �������(�x����)</td>
              <td class="codeNumber hissu">
                <asp:UpdatePanel ID="UpdatePanel_ShriSaki" UpdateMode="Conditional" runat="server"
                  RenderMode="Inline">
                  <ContentTemplate>
                    <input id="TextSiharaisakiCd" runat="server" maxlength="7" style="width: 80px;" class="codeNumber" />
                    <input id="btnShriSakiSearch" runat="server" type="button" value="����" class="gyoumuSearchBtn"
                      onserverclick="btnShriSakiSearch_ServerClick" />&nbsp;
                    <input id="TextShriSakiMei" runat="server" class="readOnlyStyle" readonly="readonly"
                      style="width: 180px" tabindex="-1" />&nbsp;
                    <input type="hidden" id="HiddenTysKensakuType" runat="server" />
                    <input type="hidden" id="HiddenKameitenCd" runat="server" />
                    <input type="hidden" id="HiddenKakusyuNG" runat="server" />
                  </ContentTemplate>
                </asp:UpdatePanel>
              </td>
              <td class="koumokuMei hissu">
                �N����</td>
              <td class="date hissu">
                <input id="TextNengappiFrom" runat="server" maxlength="10" class="date" onblur="checkDate(this);" />&nbsp;�`&nbsp;<input
                  id="TextNengappiTo" runat="server" maxlength="10" class="date" onblur="checkDate(this);" /></td>
            </tr>
            <tr>
              <td style="text-align: center;" colspan="4" rowspan="1">
                <input type="button" id="ButtonDisplay" value="������ʕ\��" runat="server" style="padding-top: 2px;" />
                <input type="button" id="ButtonHiddenDisplay" value="������ʕ\��btn" style="display: none"
                  runat="server" tabindex="-1" />
                <input type="button" id="ButtonCsv" value="�����f�[�^�o��" runat="server" />
                <input type="button" id="ButtonHiddenCsv" value="�����f�[�^�o��btn" style="display: none"
                  runat="server" tabindex="-1" />
                <input type="button" id="ButtonPrint" value="�������" runat="server" />
                <input type="button" id="ButtonHiddenPrint" value="�������btn" style="display: none"
                  runat="server" tabindex="-1" />
              </td>
            </tr>
          </tbody>
        </table>
      </td>
      <td style="vertical-align: bottom;">
        <table style="text-align: left;" class="mainTable" cellpadding="2">
          <tr>
            <td>
              �t�@�N�^�����O�J�n�N��
            </td>
            <td id="TdFactaringStNengetu" runat="server" style="width: 100px; text-align: right">
            </td>
          </tr>
          <tr>
            <td>
              �ŐV�J�z��
            </td>
            <td id="TdSaisinKurikosiDate" runat="server" style="width: 100px; text-align: right">
            </td>
          </tr>
          <tr>
            <td>
              �o�^�c��
            </td>
            <td id="TdTourokuZandaka" runat="server" style="width: 100px; text-align: right">
            </td>
          </tr>
        </table>
      </td>
    </tr>
  </table>
  <input type="hidden" id="returnTargetIds" runat="server" />
  <input type="hidden" id="afterEventBtnId" runat="server" />
  <input type="hidden" id="firstSend" runat="server" />
  <input id="search_shouhin23" runat="server" type="hidden" />
  <table style="height: 30px">
    <tr>
      <td>
        ���ʁF
      </td>
      <td id="TdResultCount" runat="server">
      </td>
      <td>
        ��
      </td>
    </tr>
  </table>
  <table cellpadding="0" cellspacing="0">
    <thead>
      <tr>
        <th style="text-align: left;">
          <div id="DivLeftTitle" runat="server" class="scrollDivLeftTitleStyle2">
            <table cellpadding="0" cellspacing="0" id="TableTitleTable1" runat="server" class="scrolltablestyle2"
              style="border-top: 1px solid #999999; border-left: 1px solid #999999;">
              <thead>
                <tr style="vertical-align: bottom">
                  <th style="width: 75px;">
                    �N����</th>
                  <th style="width: 36px;">
                    �Ȗ�</th>
                  <th style="width: 50px;">
                    ���i<br />
                    �R�[�h</th>
                  <th style="width: 140px;">
                    ���i��/<br />
                    �x����ʂȂ�</th>
                  <th style="width: 80px;">
                    �ڋq�ԍ�</th>
                  <th style="width: 140px;">
                    ������ / �E�v�Ȃ�</th>
                  <th style="width: 36px;">
                    ����</th>
                </tr>
              </thead>
            </table>
          </div>
        </th>
        <th style="text-align: left;">
          <div id="DivRightTitle" runat="server" class="scrollDivRightTitleStyle2" style="border-right: 1px solid #999999;">
            <table cellpadding="0" cellspacing="0" id="TableTitleTable2" runat="server" class="scrolltablestyle2"
              style="border-top: 1px solid #999999;">
              <thead>
                <tr style="vertical-align: bottom">
                  <th style="width: 66px;">
                    �P��</th>
                  <th style="width: 66px;">
                    �Ŕ����z</th>
                  <th style="width: 66px;">
                    �����</th>
                  <th style="width: 66px;">
                    ���z</th>
                  <th style="width: 66px;">
                    �c��</th>
                  <th style="width: 62px;">
                    �`�[�ԍ�</th>
                </tr>
              </thead>
            </table>
          </div>
        </th>
      </tr>
    </thead>
    <tbody>
      <tr>
        <td style="vertical-align: top;">
          <div id="DivLeftData" runat="server" class="scrollLeftDivStyle2" onscroll="syncScroll(1,this);"
            onmousewheel="wheel(event,this);">
            <table cellpadding="0" cellspacing="0" id="TableDataTable1" runat="server" class="scrolltablestyle2">
            </table>
          </div>
        </td>
        <td style="vertical-align: top;">
          <div id="DivRightData" runat="server" class="scrollDivStyle2" onscroll="syncScroll(2,this);"
            style="border-right: 1px solid #999999;">
            <table cellpadding="0" cellspacing="0" id="TableDataTable2" runat="server" class="scrolltablestyle2">
            </table>
          </div>
        </td>
      </tr>
    </tbody>
  </table>
</asp:Content>

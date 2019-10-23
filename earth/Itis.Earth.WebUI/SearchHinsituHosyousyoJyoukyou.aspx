<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="SearchHinsituHosyousyoJyoukyou.aspx.vb" Inherits="Itis.Earth.WebUI.SearchHinsituHosyousyoJyoukyou" Title="EARTH �i���ۏ؏��󋵌���" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script src="js/sortable_sync_ja.js" type="text/javascript">
    </script>

    <script type="text/javascript">
        
        /*====================================
         *�O���[�o���ϐ��錾�i��ʕ��i�j
         ====================================*/
        //��ʕ\�����i
        var objKubun1 = null;
        var objKubunAll = null;
        var objHoshouNoFrom = null;
        var objHoshouNoTo = null;
        var objKeiyakuNo = null;        
        
        var objSeshuName = null;
        var objBukkenJyuusho12 = null;        
        var objBikou = null;
        var objTyousakaisyaCd = null;

        var objchkHakkouStatus1 = null;
        var objchkHakkouStatus2 = null;
        var objchkHakkouStatus3 = null;
        var objchkHakkouStatus4 = null;
        var objchkHakkouStatus5 = null;
        var objchkHakkouStatus6 = null;

        var objKameitenCd = null;
        var objKameitenTorikesiRiyuu = null;
        var objKameitenMei = null;
        var objKameitenKana = null;
        var objHakkouTiming = null;

        //�˗�������
        var objchkIraisyoTykDateBlank = null;
        var objIraisyoTykDateFrom = null;
        var objIraisyoTykDateTo = null;

        //���s��
        var objchkHakkouDateBlank = null;
        var objHakkouDateFrom = null;
        var objHakkouDateTo = null;

        //�Ĕ��s��
        var objSaihakkouDateFrom = null;
        var objSaihakkouDateTo = null;

        //���s�˗���
        var objchkHakkouIraiDateBlank = null;
        var objHakkouIraiDateFrom = null;
        var objHakkouIraiDateTo = null;

        //�����˗���
        var objBukkenIraiDateFrom = null;
        var objBukkenIraiDateTo = null;

        //�ۏ؊���
        var objHosyouKikanKameiten = null;
        var objHosyouKikanBukken = null;
        
        //�f�[�^�j���Ώ�
        var objHakiTaisyou = null;

        //�ꊇ�Z�b�g���s��
        var objIkkatuHakkouDate = null;

        //hidden
        var objKubunVal = null;
        var objHdnKensakuDisp = null;
        var objHdnKensakuStatus = null;
        var objHdnIkkatuHakkouDate = null;

        //�������s�p
        var objMaxSearchCount = null;
        var objSearch = null;

        //��ʑJ�ڗp
        var objSendBtn = null;
        var objSendKubun = null;
        var objSendHoshoushoNo = null;
        var objSendTargetWin = null;
        
        // CSV�o�͗p
        var objCsv = null;
        
        //�I�𕨌��ꊇ��t�p
        var objIkkatuUketuke = null;
        var objHiddenSendKbn = null;
        var objHiddenSendHosyousyoNo = null;
        
        /****************************************
         * onload���̒ǉ�����
         * @param objTarget
         * @return
         ****************************************/
        function funcAfterOnload() {
            //��ʕ\�����i�Z�b�g
            setGlobalObj();
            
            //�敪�̏�Ԃ��Z�b�e�B���O
            setKubunVal()
            
            //�ꊇ�Z�b�g���s�����Z�b�g
            setIkkatuHakkouDate()
            
            //�������ʂ�1���݂̂̏ꍇ�A�l��߂����������s
            if (objEBI("<%= firstSend.clientID %>").value != ""){
                returnSelectValue(objEBI(objEBI("<%= firstSend.clientID %>").value));
            }
            
            /*�������ʃe�[�u�� �\�[�g�ݒ�*/
            sortables_init();
            
            /*�������ʃe�[�u�� �e�탌�C�A�E�g�ݒ�*/
            settingResultTable();

            //�㏈���{�^��ID
            objEBI("afterEventBtnId").value = "<%= search.clientID %>";
            
            // CSV�o�͂��s�Ȃ�
            if (objCsvOutPutFlg.value == "1"){
            
                // CSV�o�͎��̊m�F���b�Z�[�W
                var objCfmMsg = null;
                
                // CSV�o�͏���̊m�F
                if(objEBI("<%= HiddenCsvMaxCnt.clientID %>").value == "1"){
                   objCfmMsg = "<%= Messages.MSG017C %>".replace("����","CSV�o�͏���") 
                             + "<%= Messages.MSG162C %>".replace("@PARAM1","<%=EarthConst.MAX_CSV_OUTPUT %>").replace("@PARAM2","<%=EarthConst.MAX_CSV_OUTPUT %>")
                }else{
                   objCfmMsg = "<%= Messages.MSG017C %>".replace("����","CSV�o�͏���")
                }
                
                //�����m�F
                if(!confirm(objCfmMsg)){
                    // CSV�o�̓t���O���N���A
                    objCsvOutPutFlg.value = "";
                    return false;
                }
                //CSV���s
                objCsv.click();
            }
            // CSV�o�̓t���O���N���A
            objCsvOutPutFlg.value = "";

        }

        /*
        * ��ʕ\�����i�I�u�W�F�N�g���O���[�o���ϐ���
        */
        function setGlobalObj() {
            //��ʕ\�����i
            objKubun1 = objEBI("<%= cmbKubun_1.clientID %>");
            objKubunAll = objEBI("<%= kubun_all.clientID %>");
            objHoshouNoFrom = objEBI("<%= hoshouNo_from.clientID %>");
            objHoshouNoTo = objEBI("<%= hoshouNo_to.clientID %>");
            objKeiyakuNo = objEBI("<%= TextKeiyakuNo.clientID %>");

            objSeshuName = objEBI("<%= BukkenName.clientID %>");
            objBukkenJyuusho12 = objEBI("<%= bukkenJyuusho12.clientID %>");
            objBikou = objEBI("<%= TextBikou.clientID %>");
            objTyousakaisyaCd = objEBI("<%= tyousakaisyaCd.clientID %>");

            objchkHakkouStatus1 = objEBI("<%= chkHakkouStatus1.clientID %>");
            objchkHakkouStatus2 = objEBI("<%= chkHakkouStatus2.clientID %>");
            objchkHakkouStatus3 = objEBI("<%= chkHakkouStatus3.clientID %>");
            objchkHakkouStatus4 = objEBI("<%= chkHakkouStatus4.clientID %>");
            objchkHakkouStatus5 = objEBI("<%= chkHakkouStatus5.clientID %>");
            objchkHakkouStatus6 = objEBI("<%= chkHakkouStatus6.clientID %>");

            objKameitenCd = objEBI("<%= kameitenCd.clientID %>");
            objKameitenTorikesiRiyuu = objEBI("<%= TextTorikesiRiyuu.clientID %>");            
            objKameitenMei = objEBI("<%= kameitenNm.clientID %>");
            objKameitenKana = objEBI("<%= kameitenKana.clientID %>");
            objHakkouTiming = objEBI("<%= cmbHakkouTiming.clientID %>");

            //�˗�������
            objchkIraisyoTykDateBlank = objEBI("<%= chkIraisyoTykDateBlank.clientID %>");
            objIraisyoTykDateFrom = objEBI("<%= TextIraisyoTykDateFrom.clientID %>");
            objIraisyoTykDateTo = objEBI("<%= TextIraisyoTykDateTo.clientID %>");

            //���s��
            objchkHakkouDateBlank = objEBI("<%= chkHakkouDateBlank.clientID %>");
            objHakkouDateFrom = objEBI("<%= TextHakkouDateFrom.clientID %>");
            objHakkouDateTo = objEBI("<%= TextHakkouDateTo.clientID %>");

            //�Ĕ��s��
            objSaihakkouDateFrom = objEBI("<%= TextSaihakkouDateFrom.clientID %>");
            objSaihakkouDateTo = objEBI("<%= TextSaihakkouDateTo.clientID %>");

            //���s�˗���
            objchkHakkouIraiDateBlank = objEBI("<%= chkHakkouIraiDateBlank.clientID %>");
            objHakkouIraiDateFrom = objEBI("<%= TextHakkouIraiDateFrom.clientID %>");
            objHakkouIraiDateTo = objEBI("<%= TextHakkouIraiDateTo.clientID %>");

            //�����˗���
            objBukkenIraiDateFrom = objEBI("<%= TextBukkenIraiDateFrom.clientID %>");
            objBukkenIraiDateTo = objEBI("<%= TextBukkenIraiDateTo.clientID %>");

            //�ۏ؊���
            objHosyouKikanKameiten = objEBI("<%= TextHosyouKikanKameiten.clientID %>");
            objHosyouKikanBukken = objEBI("<%= TextHosyouKikanBukken.clientID %>");

            //�f�[�^�j��
            objHakiTaisyou = objEBI("<%= CheckHakiTaisyou.clientID %>");
            
            //�ꊇ�Z�b�g���s��
            objIkkatuHakkouDate = objEBI("<%= TextIkkatuHakkouDate.clientID %>");

            //hidden
            objKubunVal = objEBI("<%= kubunVal.clientID %>");
            objHdnKensakuInfo = objEBI("<%= kensakuInfo.clientID %>");
            objHdnKensakuStatus = objEBI("<%= HiddenKensakuInfoStyle.clientID %>");
            objHdnIkkatuHakkouDate = objEBI("<%= HiddenIkkatuHakkouDate.clientID %>");

            //�������s�p
            objMaxSearchCount = objEBI("<%= maxSearchCount.clientID %>");
            objSearch = objEBI("<%= search.clientID %>");
            
            //��ʑJ�ڗp
            objSendBtn = objEBI("<%= btnSend.clientID %>");
            objSendKubun = objEBI("<%= sendKubun.clientID %>");
            objSendHoshoushoNo = objEBI("<%= sendHoshoushoNo.clientID %>");
            objSendTargetWin = objEBI("<%= sendTargetWin.clientID %>");
            
            // CSV�o�͗p
            objCsv = objEBI("<%= ButtonHiddenCsv.clientID %>");
            objCsvOutPutFlg = objEBI("<%= HiddenCsvOutPut.clientID %>");
            
            //�I�𕨌��ꊇ��t�p
             objIkkatuUketuke = objEBI("<%= ikkatuUketuke.clientID %>");
             objHiddenSendKbn = objEBI("<%= HiddenSendKbn.clientID %>");
             objHiddenSendHosyousyoNo = objEBI("<%= HiddenSendHosyousyoNo.clientID %>");

        }
        
        /*
        * �������ʃe�[�u�� �e�탌�C�A�E�g�ݒ�
        */
        function settingResultTable(type){
            var tableTitle1 = objEBI("<%=TableTitleTable1.clientID %>");
            var tableTitle2 = objEBI("<%=TableTitleTable2.clientID %>");
            var tableData1 = objEBI("<%=TableDataTable1.clientID %>");
            var tableData2 = objEBI("<%=TableDataTable2.clientID %>");
            var divTitle1 = objEBI("<%=DivLeftTitle.clientID %>");
            var divTitle2 = objEBI("<%=DivRightTitle.clientID %>");
            var divData1 = objEBI("<%=DivLeftData.clientID %>");
            var divData2 = objEBI("<%=DivRightData.clientID %>");
            var minHeight = 120;
            var adjustHeight = 40;
            var adjustWidth = 370;

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
        
        /**
         * ���׍s���_�u���N���b�N�����ۂ̏���
         * @param objSelectedTr
         * @param intGamen[1:��,2:��,3:�H,4:��]
         * @return
         */
        function returnSelectValue(objSelectedTr,intGamen) {
            if(objSelectedTr.tagName == "TR"){
                objSendTargetWin.value = "_blank";
                sendGyoumuGamen(objSelectedTr,4,2);
            }
            return;
        }
        
        /**
         * �����m�F��ʕ\������
         * @param objSelectedTr
         * @param intGamen[1:��,2:��,3:�H,4:��]
         * @param intEvent[1:�ʃE�B���h�E,2:�_�u���N���b�N]
         * @return
         */
        function sendGyoumuGamen(objSelectedTr,intGamen,intEvent){
            var varAction = '';
            
            //�I�u�W�F�N�g�̍ēǍ�(Ajax�ĕ`��Ή�)
            objSendTargetWin = objEBI("<%= sendTargetWin.clientID %>");
            if(objSendTargetWin.value == "")objSendTargetWin.value="<%=EarthConst.MAIN_WINDOW_NAME %>";

            //�߂�l�S�z��(�s�̐擪�Z���̐擪�I�u�W�F�N�g����擾)
            var objSelRet = getChildArr(getChildArr(objSelectedTr,"TD")[0],"INPUT")[0];

            //<!-- ��ʈ��n����� -->
            objSendForm = objEBI("openPageForm");
            objSendVal_st = objEBI("st");
            objSendVal_Kubun = objEBI("sendPage_kubun");
            objSendVal_HosyousyoNo = objEBI("sendPage_hosyoushoNo");
            objSendVal_kbn = objEBI("kbn");
            objSendVal_no = objEBI("no");
            
            //�l�Z�b�g   
            objSendForm.target=objSendTargetWin.value;
            objSendVal_st.value="<%=EarthConst.MODE_VIEW %>";
            if(objSelRet != undefined){
                var arrReturnValue = objSelRet.value.split(sepStr);  
                objSendVal_Kubun.value=arrReturnValue[0];
                objSendVal_HosyousyoNo.value=arrReturnValue[1];
                objSendVal_kbn.value=arrReturnValue[0];
                objSendVal_no.value=arrReturnValue[1];
            }
                    
            //intEvent=1 : �ʃE�B���h�E�N���b�N(�Ώۉ�ʈ�̂�)
            if(intEvent == 1){
                //�I�[�v���Ώۂ̋Ɩ���ʂ��w��
                switch(intGamen){
                     case 1://
                        varAction = "<%=UrlConst.IRAI_KAKUNIN %>";
                        break;
                     case 2://
                        varAction = "<%=UrlConst.HOUKOKUSYO %>";
                        break;
                     case 3://
                        varAction = "<%=UrlConst.KAIRYOU_KOUJI %>";
                        break;
                     case 4://
                        varAction = "<%=UrlConst.HOSYOU %>";
                        break;
                     case 5://
                        varAction = "<%=UrlConst.IKKATU_HENKOU_KIHON %>";
                        if(!setCheckedIkkatu())return false;
                        break;
                     case 6://
                        varAction = "<%=UrlConst.IKKATU_HENKOU_TYS_SYOUHIN %>";
                        if(!setCheckedIkkatu())return false;
                        break;
                     default://
                        return false;
                        break;
                }
                objSendForm.action = varAction;
                objSendForm.submit();
                
            }else if(intEvent == 2){//intEvent=2 : �_�u���N���b�N(�`�F�b�N�{�b�N�X�̃`�F�b�N�Ώۉ�ʂ��J��)
                //E:??
                if(flgEaster == 1){
                    flgEaster = null;
                    objSendForm.action="<%=UrlConst.POPUP_GAMEN_KIDOU %>";
                    objSendForm.submit();
                    return true;
                }
                //1:��
                 if(intGamen == 1){
                    objSendForm.action="<%=UrlConst.IRAI_KAKUNIN %>";
                    objSendForm.submit();
                }
                //2:��
                if(intGamen == 2){
                    objSendForm.action="<%=UrlConst.HOUKOKUSYO %>";
                    objSendForm.submit();
                }
                //3:�H
                if(intGamen == 3){
                    objSendForm.action="<%=UrlConst.KAIRYOU_KOUJI %>";
                    objSendForm.submit();
                }
                //4:��
                if(intGamen == 4){
                    objSendForm.action="<%=UrlConst.HOSYOU %>";
                    objSendForm.submit();
                }
            }else{
                return false;
            }
        }

        /**
         * �I�𕨌��ꊇ��t�{�^�������O�`�F�b�N
         * @return
         */
        function setCheckedIkkatuUketuke(){
        
            var tableData1 = objEBI("<%=TableDataTable1.clientID %>");
            var arrSakiTr = tableData1.tBodies[0].rows;
            var objTd = null;
            var arrInput = null;
            var bukkenCount = 0;

            objHiddenSendKbn.value = "";
            objHiddenSendHosyousyoNo.value = "";

            for ( var tri = 0; tri < arrSakiTr.length; tri++) {
                objTd = arrSakiTr[tri].cells[0];
                arrInput = getChildArr(objTd,"INPUT");
                for ( var ar = 0; ar < arrInput.length; ar++) {
                    if(arrInput[ar].type == "checkbox" && arrInput[ar].checked){
                        var arrVal = arrInput[ar].value.split(sepStr);
                        objHiddenSendKbn.value += arrVal[0] + sepStr;
                        objHiddenSendHosyousyoNo.value += arrVal[1] + sepStr;
                        bukkenCount++;
                    }
                }
            }
            //����I������Ă��Ȃ������ꍇ�A�G���[
            if(1 > bukkenCount){
                alert("<%= Messages.MSG125E %>");
                objSendVal_Kubun.value = "";
                objSendVal_HosyousyoNo.value = "";
                return false;
            }
            //����������𒴂��Ă����ꍇ�A�G���[
            if(bukkenCount >= objEBI("<%=HiddenIkkatuUketukeMax.clientID %>").value){
                alert("<%= Messages.MSG216E %>");
                objSendVal_Kubun.value = "";
                objSendVal_HosyousyoNo.value = "";
                return false;
            }
            //�u�ꊇ�Z�b�g���s���v���󔒂܂��͉ߋ����t�̏ꍇ�A�G���[
            if((objIkkatuHakkouDate.value == "") || (!checkDaiSyouIkkatuHakkou(objHdnIkkatuHakkouDate,objIkkatuHakkouDate,"�ꊇ�Z�b�g���s��"))){
                  alert("<%= Messages.MSG040E %>".replace("@PARAM1","�ꊇ�Z�b�g���s��"));
//                objSendVal_Kubun.value = "";
//                objSendVal_HosyousyoNo.value = "";
                return false;
            }

            return true;
        }

        /**
         * �敪�Z���N�g�{�b�N�X���`�F�b�N�{�b�N�X�̏�Ԃ��`�F�b�N���A�I������Ă���敪���܂Ƃ߂�
         * @return
         */
        function setKubunVal(){
            objKubunVal.value = ""; //������
            if(objKubunAll.checked == true){
                objKubun1.selectedIndex = 0;
                objKubun1.disabled = true;
                return;
            }else{
                objKubun1.disabled = false;
                if(objKubun1.value != ""){
                    objKubunVal.value = objKubun1.value;
                }
            }
        }

        /**
         * �ꊇ�Z�b�g���s�����Z�b�g
         * @return
         */
        function setIkkatuHakkouDate(){
            if(objIkkatuHakkouDate.value == ""){
                //�ꊇ�Z�b�g���s���ɂ��łɓ��t���Z�b�g����Ă��Ȃ��ꍇ�̂݁A
                //�V�X�e�����t���Z�b�g����
                objIkkatuHakkouDate.value = getToday();
                //Hidden�ɃZ�b�g(��r�p)
                objHdnIkkatuHakkouDate.value = getToday();
                return;
            }
        }

        /**
         * �u�������s�v�������̃`�F�b�N����
         * @return
         */
        function checkJikkou(varAction){

            if(!objKubunAll.checked && objKubunVal.value.Trim() == ""){
                alert("<%= Messages.MSG006E %>");
                objKubun1.focus();
                return false;
            }
            if(objKubunAll.checked && !checkInputJouken()){
                alert("<%= Messages.MSG008E %>");
                objKubunAll.focus();
                return false;
            }

            //�ԍ� �召�`�F�b�N
            if(!checkDaiSyou(objHoshouNoFrom,objHoshouNoTo,"�ԍ�"))return false;
            
            //�˗������� �召�`�F�b�N
            if(!checkDaiSyou(objIraisyoTykDateFrom,objIraisyoTykDateTo,"�˗�������"))return false;
            
            //���s�� �召�`�F�b�N
            if(!checkDaiSyou(objHakkouDateFrom,objHakkouDateTo,"���s��"))return false;

            //�Ĕ��s�� �召�`�F�b�N
            if(!checkDaiSyou(objSaihakkouDateFrom,objSaihakkouDateTo,"�Ĕ��s��"))return false;

            //���s�˗��� �召�`�F�b�N
            if(!checkDaiSyou(objHakkouIraiDateFrom,objHakkouIraiDateTo,"���s�˗���"))return false;

            //�����˗��� �召�`�F�b�N
            if(!checkDaiSyou(objBukkenIraiDateFrom,objBukkenIraiDateTo,"�����˗���"))return false;

            if(varAction == "0"){
                //�\�������`�F�b�N
                if(objMaxSearchCount.value == "max"){
                    if(!confirm(("<%= Messages.MSG007C %>")))return false;
                }
                //�������s
                objSearch.click();
            }else if(varAction == "1"){
                    objCsvOutPutFlg.value = "1"
                    //�������s
                    objSearch.click();
            }
        }
        
        /**
         * �敪�A�Ώ۔͈͈ȊO�̌������������͂���Ă��邩�̃`�F�b�N
         * @return true:��ł����͂�����/false:������͂�����
         */
        function checkInputJouken(){
        
            if(objHoshouNoFrom.value!="")return true;
            if(objHoshouNoTo.value!="")return true;
            if(objKeiyakuNo.value!="")return true;
            
            if(objSeshuName.value!="")return true;
            if(objBukkenJyuusho12.value!="")return true;
            if(objBikou.value!="")return true;
            if(objTyousakaisyaCd.value!="")return true;            

            if(objchkHakkouStatus1.checked)return true;
            if(objchkHakkouStatus2.checked)return true;
            if(objchkHakkouStatus3.checked)return true;
            if(objchkHakkouStatus4.checked)return true;
            if(objchkHakkouStatus5.checked)return true;
            if(objchkHakkouStatus6.checked)return true;
 
            if(objKameitenCd.value!="")return true;
            if(objKameitenKana.value!="")return true;
            if(objHakkouTiming.value!="")return true;

            //�˗�������
            if(objchkIraisyoTykDateBlank.checked)return true;
            if(objIraisyoTykDateFrom.value!="")return true;
            if(objIraisyoTykDateTo.value!="")return true;
            
            //���s��
            if(objchkHakkouDateBlank.checked)return true;
            if(objHakkouDateFrom.value!="")return true;
            if(objHakkouDateTo.value!="")return true;

            //�Ĕ��s��
            if(objSaihakkouDateFrom.value!="")return true;
            if(objSaihakkouDateTo.value!="")return true;

            //���s�˗���
            if(objchkHakkouIraiDateBlank.checked)return true;
            if(objHakkouIraiDateFrom.value!="")return true;
            if(objHakkouIraiDateTo.value!="")return true;

            //�����˗���
            if(objBukkenIraiDateFrom.value!="")return true;
            if(objBukkenIraiDateTo.value!="")return true;

            //�ۏ؊���setCheckedIkkatuUketuke
            if(objHosyouKikanKameiten.value!="")return true;
            if(objHosyouKikanBukken.value!="")return true;
                        
            return false;
        
        }
        
        /**
         * �召�`�F�b�N
         * @return true/false
         */
        function checkDaiSyou(objFrom,objTo,mess){
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
         * �召�`�F�b�N(�ꊇ�Z�b�g���s�����ߋ����t�̓G���[)
         * @return true/false
         */
        function checkDaiSyouIkkatuHakkou(objFrom,objTo,mess){
            if(objFrom.value != "" && objTo.value != ""){
                if(Number(removeSlash(objFrom.value)) > Number(removeSlash(objTo.value))){
                    // alert("<%= Messages.MSG040E %>".replace("@PARAM1",mess));
                    objFrom.select();
                    return false;
                }
            }
            return true;
        }

        /**
         * �ۏ؏�NO To�����Z�b�g
         * @return true/false
         */
        function setHosyouNoTo(obj){
            if(obj.id == objHoshouNoFrom.id && objHoshouNoTo.value == ""){
                objHoshouNoTo.value = obj.value;
                objHoshouNoTo.select();
            }
            return true;
        }

        /**
         * To�����Z�b�g
         * @return true/false
         */
        function setTo(obj){
            //�˗�������
            if(obj.id == objIraisyoTykDateFrom.id && objIraisyoTykDateTo.value == ""){
                objIraisyoTykDateTo.value = obj.value;
                objIraisyoTykDateTo.select();
            //���s��
            }else if(obj.id == objHakkouDateFrom.id && objHakkouDateTo.value == ""){
                objHakkouDateTo.value = obj.value;
                objHakkouDateTo.select();
            //�Ĕ��s��
            }else if(obj.id == objSaihakkouDateFrom.id && objSaihakkouDateTo.value == ""){
                objSaihakkouDateTo.value = obj.value;
                objSaihakkouDateTo.select();
            //���s�˗���
            }else if(obj.id == objHakkouIraiDateFrom.id && objHakkouIraiDateTo.value == ""){
                objHakkouIraiDateTo.value = obj.value;
                objHakkouIraiDateTo.select();
            //�����˗���
            }else if(obj.id == objBukkenIraiDateFrom.id && objBukkenIraiDateTo.value == ""){
                objBukkenIraiDateTo.value = obj.value;
                objBukkenIraiDateTo.select();
            }
            return true;
        }

        /**
         * All�N���A������Ɏ��s�����t�@���N�V����
         * @return 
         */
        function funcAfterAllClear(obj){
            //�S�敪�Ƀ`�F�b�N
            objKubunAll.click();

            objMaxSearchCount.selectedIndex = 1;
            //���t���Z�b�g
            objIkkatuHakkouDate.value = getToday();
            //�������� ��\�����\��
            if(objHdnKensakuInfo.style.display == 'none'){
                objHdnKensakuInfo.style.display = 'inline';
                objHdnKensakuStatus.value = 'inline';
            }
            //���s�i���󋵂̃f�t�H���g�`�F�b�N
            objchkHakkouStatus4.checked = true;
            objKubun1.focus();
        }

        /**
         * �l���`�F�b�N���A�Ώۂ��N���A����
         * @return 
         */
        function clrName(obj,targetId){
            if(obj.value == "") objEBI(targetId).value="";
        }

        /*********************
         * �����X���N���A
         *********************/
        function clrKameitenInfo(obj){
            if(obj.value == ""){
                //�l�̃N���A
                objKameitenCd.value = "";
                objKameitenTorikesiRiyuu.value = "";
                objKameitenMei.value = "";
                //�F���f�t�H���g��
                objKameitenCd.style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>"
                objKameitenTorikesiRiyuu.style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>"
                objKameitenMei.style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>"
            }
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


    //Ajax���������㏈��
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args){
            //��ʕ\�����i�Z�b�g
            setGlobalObj();
        }

    /**
     * �u�I�𕨌��ꊇ��t�v�������̃`�F�b�N����
     * @return
     */
    function checkIkkatuUketuke(){
        
        //�`�F�b�N
        if(!setCheckedIkkatuUketuke())return false;
        
        //�������s
        objIkkatuUketuke.click();
    }

    //�ύX�O�R���g���[���̒l��ޔ����āA�Y���R���g���[��(Hidden)�ɕێ�����
    function SetChangeMaeValue(strTaihiID, strTargetID){
       document.getElementById(strTaihiID).value = document.getElementById(strTargetID).value;
    }

    </script>

    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    
    <%--CSV�o�͔��f--%>
    <asp:HiddenField ID="HiddenCsvOutPut" runat="server" />
    <%--CSV�o�͏�������t���O--%>
    <asp:HiddenField ID="HiddenCsvMaxCnt" runat="server" />
    <%--�r���p����--%>
    <input type="hidden" id="HiddenHaitaDate" runat="server" />
    <input type="hidden" id="HiddenKensakuKbn" runat="server" /><%--�������敪--%>
    <input type="hidden" id="HiddenKensakuNoFrom" runat="server" /><%--�������ԍ�From--%>
    <input type="hidden" id="HiddenKensakuNoTo" runat="server" /><%--�������ԍ�To--%>
    <input type="hidden" id="HiddenKensakukeiyakuNo" runat="server" /><%--�������_��NO--%>
    <input type="hidden" id="HiddenKensakuSesyuMei" runat="server" /><%--������������--%>
    <input type="hidden" id="HiddenKensakuBukkenjyuusyo" runat="server" /><%--�����������Z���P�{�Q--%>
    <input type="hidden" id="HiddenKensakuBikou" runat="server" /><%--���������l--%>
    <input type="hidden" id="HiddenKensakuTysKaisya" runat="server" /><%--������������ЃR�[�h�E������Ў��Ə��R�[�h--%>
    <input type="hidden" id="HiddenKensakuHakSts1" runat="server" /><%--���������s�i����1--%>
    <input type="hidden" id="HiddenKensakuHakSts2" runat="server" /><%--���������s�i����2--%>
    <input type="hidden" id="HiddenKensakuHakSts3" runat="server" /><%--���������s�i����3--%>
    <input type="hidden" id="HiddenKensakuHakSts4" runat="server" /><%--���������s�i����4--%>
    <input type="hidden" id="HiddenKensakuHakSts5" runat="server" /><%--���������s�i����5--%>
    <input type="hidden" id="HiddenKensakuHakSts6" runat="server" /><%--���������s�i����6--%>
    <input type="hidden" id="HiddenKensakuKameitenCd" runat="server" /><%--�����������X�R�[�h--%>
    <input type="hidden" id="HiddenKensakuTenmeiKana" runat="server" /><%--�����������X�J�i--%>
    <input type="hidden" id="HiddenKensakuHakTiming" runat="server" /><%--���������s�^�C�~���O�i�����X���s�ݒ�j--%>
    <input type="hidden" id="HiddenKensakuHakIraiTykChk" runat="server" /><%--�������˗������� ��--%>
    <input type="hidden" id="HiddenKensakuHakIraiTykFrom" runat="server" /><%--�������˗������� From--%>
    <input type="hidden" id="HiddenKensakuHakIraiTykTo" runat="server" /><%--�������˗������� To--%>
    <input type="hidden" id="HiddenKensakuHakDtChk" runat="server" /><%--���������s�� ��--%>
    <input type="hidden" id="HiddenKensakuHakDtFrom" runat="server" /><%--���������s�� From--%>
    <input type="hidden" id="HiddenKensakuHakDtTo" runat="server" /><%--���������s�� To--%>
    <input type="hidden" id="HiddenKensakuSaiHakDtFrom" runat="server" /><%--�������Ĕ��s�� From--%>
    <input type="hidden" id="HiddenKensakuSaiHakDtTo" runat="server" /><%--�������Ĕ��s�� To--%>
    <input type="hidden" id="HiddenKensakuHakIraiTimeChk" runat="server" /><%--���������s�˗��� ��--%>
    <input type="hidden" id="HiddenKensakuHakIraiTimeFrom" runat="server" /><%--���������s�˗��� From--%>
    <input type="hidden" id="HiddenKensakuHakIraiTimeTo" runat="server" /><%--���������s�˗��� To--%>
    <input type="hidden" id="HiddenKensakuIraiDtFrom" runat="server" /><%--�����������˗��� From--%>
    <input type="hidden" id="HiddenKensakuIraiDtTo" runat="server" /><%--�����������˗��� To--%>
    <input type="hidden" id="HiddenKensakuHosyouKknMk" runat="server" /><%--�������ۏ؊���(�����X)--%>
    <input type="hidden" id="HiddenKensakuHosyouKknTj" runat="server" /><%--�������ۏ؊���(����)--%>
    <input type="hidden" id="HiddenKensakuHakiSyubetu" runat="server" /><%--�������f�[�^�j�����--%>
    <%--�I�𕨌��ꊇ��t�p(�`�F�b�N�t�̂���)--%>
    <input type="hidden" id="HiddenSendKbn" runat="server" />
    <input type="hidden" id="HiddenSendHosyousyoNo" runat="server" />
    <%--�I�𕨌��ꊇ��t�p(�@�ʐ����o�^�p)--%>
    <input type="hidden" id="HiddenTextShSyouhinCd" runat="server" />
    <input type="hidden" id="HiddenShZeiritu" runat="server" />
    <input type="hidden" id="HiddenShZeiKbn" runat="server" />
    <input type="hidden" id="HiddenSimeDate" runat="server" />   
    <input type="hidden" id="HiddenTextShSeikyuusyoHakkouDate" runat="server" />      
    <input type="hidden" id="HiddenTextShUriageNengappi" runat="server" />      
    <input type="hidden" id="HiddenTextShHattyuusyoKakutei" runat="server" />  
    <input type="hidden" id="HiddenTextShSeikyuusaki" runat="server" />
    <input type="hidden" id="HiddenTextShJituseikyuuKingaku" runat="server" />
    <input type="hidden" id="HiddenTextShKoumutenSeikyuuKingaku" runat="server" />
    <input type="hidden" id="HiddenTextShSyouhizei" runat="server" />
    <%--�I�𕨌��ꊇ��t������--%>
    <input type="hidden" id="HiddenIkkatuSyoriZumiKbn" runat="server" />
    <input type="hidden" id="HiddenIkkatuSyoriZumiNo" runat="server" />
    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tbody>
            <tr>
                <th style="text-align: left;">
                    �i���ۏ؏��󋵌���</th>
                <th >
                </th>
            </tr>
            <tr>
                <td colspan="2" rowspan="1">
                </td>
            </tr>
        </tbody>
    </table>
    <input type="hidden" id="gamenId" value="kensaku" runat="server" />
    <table style="text-align: left;" class="mainTable paddinNarrow">
        <thead>
            <tr>
                <th class="tableTitle" colspan="8" rowspan="1">
                    <a id="AKensakuInfo" tabindex="10" runat="server">��������</a>
                    <input id="btnClearWin" value="�N���A" type="reset" class="button" tabindex="10" />
                    <input type="hidden" id="HiddenKensakuInfoStyle" runat="server" value="inline" /></th>
            </tr>
        </thead>
        <tbody id="kensakuInfo" style="display: inline;" runat="server">
            <tr>
                <td class="hissu" style="font-weight: bold">
                    �敪</td>
                <td colspan="3" class="hissu">
                    <asp:DropDownList ID="cmbKubun_1" runat="server" TabIndex="10">
                    </asp:DropDownList>
                    &nbsp;�S�敪<input id="kubun_all" type="checkbox" runat="server" tabindex="10" />
                    <input type="hidden" id="kubunVal" runat="server" /></td>
                <td class="koumokuMei">
                    �ԍ�</td>
                <td colspan="1">
                    <input id="hoshouNo_from" runat="server" maxlength="10" style="width: 70px;" class="codeNumber"
                        onblur="checkNumber(this);" onchange="if(checkNumber(this))setHosyouNoTo(this);"
                        tabindex="10" />�`<input id="hoshouNo_to" runat="server" maxlength="10" style="width: 70px;"
                            class="codeNumber" onblur="checkNumber(this);" tabindex="10" /></td>
                <td class="koumokuMei">
                    �_��NO
                </td>
                <td colspan="1">
                    <input id="TextKeiyakuNo" style="width: 150px; ime-mode: inactive;" runat="server" maxlength="20"
                            class="codeNumber" tabindex="10"/>
                </td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    ������</td>
                <td colspan="1">
                    <input id="BukkenName" runat="server" maxlength="50" style="width: 100px; ime-mode: active;"
                        tabindex="10" /></td>
                <td class="koumokuMei">
                    �����Z��</td>
                <td colspan="1">
                    <input size="100" id="bukkenJyuusho12" runat="server" maxlength="64" style="width: 100px;
                        ime-mode: active;" tabindex="10" /></td>
                <td class="koumokuMei">
                    ���l</td>
                <td colspan="1">
                    <input id="TextBikou" runat="server" maxlength="256" style="width: 100px; ime-mode: active;"
                        tabindex="10" /></td>
                <td class="koumokuMei">
                    �������</td>
                <td colspan="1">
                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server" RenderMode="Inline">
                        <ContentTemplate>
                            <input id="tyousakaisyaCd" runat="server" maxlength="7" style="width: 45px;" class="codeNumber"
                                tabindex="10" />
                            <input id="tyousakaisyaSearch" runat="server" type="button" value="����" class="gyoumuSearchBtn"
                                tabindex="10"  style="width:3em;" />&nbsp;
                            <input id="tyousakaisyaNm" runat="server" class="readOnlyStyle" style="width: 5em"
                                readonly="readOnly" tabindex="-1" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>                                                                        
            </tr>
            <tr>
                <td class="koumokuMei">
                    ���s�i����
                </td>
                <td colspan="7" rowspan="1" style="text-align: left">
                    <input id="chkHakkouStatus1" name="chkHakkouStatus1" value="1" type="checkbox" runat="server"
                         tabindex="10" />�ΏۊO
                    <input id="chkHakkouStatus2" name="chkHakkouStatus2" value="1" type="checkbox" runat="server"
                        tabindex="10" />���s�s��
                    <input id="chkHakkouStatus3" name="chkHakkouStatus3" value="1" type="checkbox" runat="server"
                        tabindex="10" />���s���˗�
                    <input id="chkHakkouStatus4" name="chkHakkouStatus4" value="1" type="checkbox" runat="server"
                        checked="checked" tabindex="10" />���[���˗�(�Ĕ��s��)�ρE����t
                    <input id="chkHakkouStatus5" name="chkHakkouStatus5" value="1" type="checkbox" runat="server"
                         tabindex="10" />���[���˗�(�Ĕ��s��)������
                    <input id="chkHakkouStatus6" name="chkHakkouStatus6" value="1" type="checkbox" runat="server"
                        tabindex="10" />���񔭍s��
                 </td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    �����X</td>
                <td colspan="2">
                    <asp:UpdatePanel ID="UpdatePanel_irai1" UpdateMode="Conditional" runat="server" RenderMode="Inline">
                        <ContentTemplate>
                            <input id="kameitenCd" runat="server" maxlength="5" style="width: 40px;" class="codeNumber"
                                tabindex="10" />
                            <input id="kameitenSearch" runat="server" type="button" value="����" class="gyoumuSearchBtn"
                                tabindex="10" style="width:3em;" />&nbsp;
                            <input id="kameitenNm" runat="server" class="readOnlyStyle" readonly="readonly" style="width: 5em;"
                                tabindex="-1" />&nbsp;
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td colspan="1">
                    <asp:UpdatePanel ID="UpdatePanel_KtTorikesi" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="kameitenSearch" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:TextBox ID="TextTorikesiRiyuu" runat="server" Width="100px"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="koumokuMei">
                    �����X�J�i</td>
                <td colspan="1">
                    <input id="kameitenKana" runat="server" maxlength="40" style="width: 150px; ime-mode: active;"
                        tabindex="10" /></td>
                <td class="koumokuMei" >
                    ���񔭍s���@</td>
                <td colspan="1">
                    <asp:DropDownList ID="cmbHakkouTiming" runat="server" TabIndex="10">
                        <asp:ListItem Value="" Text="" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="0" Text="�˗���"></asp:ListItem>
                        <asp:ListItem Value="1" Text="�������s"></asp:ListItem>
                        <asp:ListItem Value="2" Text="�n�Ճ��[��"></asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    �˗�������
                    <input id="chkIraisyoTykDateBlank" value="1" type="checkbox" runat="server" tabindex="10" />��</td>
                <td class="date" colspan="3">
                    <input id="TextIraisyoTykDateFrom" runat="server" maxlength="10" class="date" onblur="checkDate(this);" onchange="if(checkDate(this))setTo(this);"
                        tabindex="10" />�`<input id="TextIraisyoTykDateTo" runat="server" maxlength="10"
                            class="date" onblur="checkDate(this);" tabindex="10" /></td>
                <td class="koumokuMei">
                    ���s��
                    <input id="chkHakkouDateBlank" value="1" type="checkbox" runat="server" tabindex="10" />��</td>
                <td class="date" colspan="1">
                    <input id="TextHakkouDateFrom" runat="server" maxlength="10" class="date" onblur="checkDate(this);" onchange="if(checkDate(this))setTo(this);"
                        tabindex="10" />�`<input id="TextHakkouDateTo" runat="server" maxlength="10"
                            class="date" onblur="checkDate(this);" tabindex="10" /></td>
                <td class="koumokuMei">
                    �Ĕ��s��</td>
                <td class="date" colspan="1">
                    <input id="TextSaihakkouDateFrom" runat="server" maxlength="10" class="date" onblur="checkDate(this);" onchange="if(checkDate(this))setTo(this);"
                        tabindex="10" />�`<input id="TextSaihakkouDateTo" runat="server" maxlength="10"
                            class="date" onblur="checkDate(this);" tabindex="10" /></td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    ���s�˗���
                    <input id="chkHakkouIraiDateBlank" value="1" type="checkbox" runat="server" tabindex="10" />��</td>
                <td class="date" colspan="3">
                    <input id="TextHakkouIraiDateFrom" runat="server" maxlength="10" class="date" onblur="checkDate(this);" onchange="if(checkDate(this))setTo(this);"
                        tabindex="10" />�`<input id="TextHakkouIraiDateTo" runat="server" maxlength="10"
                            class="date" onblur="checkDate(this);" tabindex="10" /></td>
                <td class="koumokuMei">
                    �����˗���
                </td>
                <td class="date">
                    <input id="TextBukkenIraiDateFrom" runat="server" maxlength="10" class="date" onblur="checkDate(this);" onchange="if(checkDate(this))setTo(this);"
                        tabindex="10" />�`<input id="TextBukkenIraiDateTo" runat="server" maxlength="10"
                            class="date" onblur="checkDate(this);" tabindex="10" /></td>            
                <td class="koumokuMei">
                    �ۏ؊���
                </td>
                <td colspan="1">
                    �����X<input id="TextHosyouKikanKameiten" type="text" runat="server" maxlength="2" style="width: 20px;
                        ime-mode: disabled;" class="codeNumber" onblur="checkNumber(this);" tabindex="10" />�N&nbsp;&nbsp;&nbsp;
                    ����<input id="TextHosyouKikanBukken" type="text" runat="server" maxlength="2" style="width: 20px;
                        ime-mode: disabled;" class="codeNumber" onblur="checkNumber(this);" tabindex="10"/>�N
                </td>
            </tr>
            <tr class="tableSpacer">
                <td colspan="8">
                </td>
            </tr>
        </tbody>
        <tbody id="kensakuJikkou" style="display: inline;">
            <tr>
                <td colspan="8" rowspan="1" style="text-align: center">
                    <img src="images/spacer.gif" alt="" style="width: 30px; height: 0px;" />
                    �����������
                    <select id="maxSearchCount" runat="server" tabindex="10">
                        <option value="10">10��</option>
                        <option value="100" selected="selected">100��</option>
                        <option value="max">������</option>
                    </select>
                    <input type="button" id="btnSearch" value="�������s" runat="server" tabindex="10" />
                    <input type="button" id="search" value="�������sbtn" style="display: none" runat="server" />
                    <img src="images/spacer.gif" alt="" style="width: 30px; height: 0px;" />
                    <input id="CheckHakiTaisyou" value="0" type="checkbox" runat="server"
                        tabindex="10" />�f�[�^�j����ʗL��͌����ΏۊO
                </td>
            </tr>
        </tbody>
    </table>
    <input type="hidden" id="afterEventBtnId" runat="server" />
    <table style="text-align: left; height: 20px; width: 100px;" border="0" cellpadding="0" cellspacing="0">
    <tr><td>&nbsp;</td></tr>
    </table>
    <table style="height: 30px">
        <tr>
            <td>
                �������ʁF
            </td>
            <td id="resultCount" runat="server">
            </td>
            <td>
                ��
            </td>
            <td colspan="10"></td>
            <td>�ꊇ�Z�b�g���s��</td>
            <td class="date">
                <input id="TextIkkatuHakkouDate" runat="server" maxlength="10" class="date" onblur="checkDate(this);" tabindex="10" />
                <input type="hidden" id="HiddenIkkatuHakkouDate" runat="server" />
                <input type="hidden" id="HiddenIkkatuUketukeMax" runat="server" />
                <input type="button" id="ButtonIkkatuUketuke" runat="server" value="�I�𕨌��ꊇ��t" style="width: 160px;"
                    onclick="" tabindex="10" />
                <input type="button" id="ikkatuUketuke" value="�I�𕨌��ꊇ��t���sbtn" style="display: none" runat="server" />
                <input type="button" id="ButtonCsv" runat="server" value="CSV�o��"
                    style="width: 160px;" onclick="" tabindex="10" />
                    <input type="button" id="ButtonHiddenCsv" value="CSV���sbtn" style="display: none"
                        runat="server" tabindex="-1" />
            </td>
        </tr>
    </table>

    <table style="text-align: left; width: 100px;" cellpadding="0" cellspacing="0">
        <thead>
            <tr>
                <th style="text-align: left;">
                    <div id="DivLeftTitle" runat="server" class="scrollDivLeftTitleStyle2">
                        <table cellpadding="0" cellspacing="0" id="TableTitleTable1" runat="server" class="scrolltablestyle2 sortableTitle"
                            style="border-top: 1px solid #999999; border-left: 1px solid #999999;">
                            <thead>
                                <tr>
                                    <th style="width: 40px" class="unsortable">
                                        �ꊇ</th>
                                    <th style="width: 40px;">
                                        �敪</th>
                                    <th style="width: 70px;">
                                        �ԍ�</th>
                                    <th style="width: 200px;">
                                        �{�喼</th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </th>
                <th style="text-align: left;">
                    <div id="DivRightTitle" runat="server" class="scrollDivRightTitleStyle2" style="border-right: 1px solid #999999;">
                        <table cellpadding="0" cellspacing="0" id="TableTitleTable2" runat="server" class="scrolltablestyle2 sortableTitle"
                            style="border-top: 1px solid #999999;">
                            <thead>
                                <tr>
                                    <th style="width: 200px">
                                        �����Z��</th>
                                    <th style="width: 82px;">
                                        �����X�R�[�h</th>
                                    <th style="width: 180px;">
                                        �����X��</th>
                                    <th style="width: 140px">
                                        �˗�����</th>
                                    <th style="width: 80px">
                                        �˗�������</th>
                                    <th style="width: 80px">
                                        ���s��</th>
                                    <th style="width: 80px">
                                        �ۏ؊J�n��</th>
                                    <th style="width: 120px">
                                        ����</th>
                                    <th style="width: 80px">
                                        �H��󗝓�</th>
                                    <th style="width: 150px">
                                        �ۏ؂Ȃ����R</th>
                                    <th style="width: 80px">
                                        �c�ƒS����</th>
                                    <th style="width: 100px">
                                        ���񔭍s���@</th>
                                    <th style="width: 80px">
                                        ��/�ۏ؊���</th>
                                    <th style="width: 80px">
                                        ��/�ۏ؊���</th>
                                    <th style="width: 80px">
                                        �����˗���</th>
                                    <th style="width: 320px">
                                        ���l</th>
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
                        <table cellpadding="0" cellspacing="0" id="TableDataTable1" runat="server" class="scrolltablestyle2 sortableData">
                        </table>
                    </div>
                </td>
                <td style="vertical-align: top;">
                    <div id="DivRightData" runat="server" class="scrollDivStyle2" onscroll="syncScroll(2,this);"
                        style="border-right: 1px solid #999999;">
                        <table cellpadding="0" cellspacing="0" id="TableDataTable2" runat="server" class="scrolltablestyle2 sortableData">
                        </table>
                    </div>
                </td>
            </tr>
        </tbody>
    </table>

    <script type="text/javascript">
    </script>

    <input type="hidden" id="firstSend" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server" RenderMode="Inline">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSend" />
        </Triggers>
        <ContentTemplate>
            <input type="hidden" id="sendKubun" runat="server" />
            <input type="hidden" id="sendHoshoushoNo" runat="server" />
            <input type="hidden" id="sendTargetWin" runat="server" />
            <!-- �󒍊m�F��ʑJ�ڃ{�^���i��\���j -->
            <input type="button" id="btnSend" value="�m�F��ʑJ��" style="display: none" runat="server"
                onserverclick="btnSend_ServerClick" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

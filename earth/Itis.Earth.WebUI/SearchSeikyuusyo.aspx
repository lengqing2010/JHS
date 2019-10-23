<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="SearchSeikyuusyo.aspx.vb" Inherits="Itis.Earth.WebUI.SearchSeikyuusyo"
    Title="EARTH �������ꗗ" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script src="js/sortable_sync_ja.js" type="text/javascript">
    </script>

    <script type="text/javascript">

        //�E�B���h�E�T�C�Y�ύX
        try{
            if(window.name != "<%=EarthConst.MAIN_WINDOW_NAME %>") window.resizeTo(1024,768);
        }catch(e){
            //�A�N�Z�X�����ۂ���܂����̃G���[���o���牽�����Ȃ��B
            if(e.number == 2147024891) throw e;
        }

        _d = document;
        /*====================================
         *�O���[�o���ϐ��錾�i��ʕ��i�j
         ====================================*/
        var gVarSettouJi = "ctl00_CPH1_"; 
        var gVarTr1 = "DataTable_resultTr1_";
        var gVarTr2 = "DataTable_resultTr2_";
        var gVarTdSentou = "DataTable_Sentou_Td_";
        var gVarHdnSeikyuusyoNo = "HdnSeikyuusyoNo_";
        var gVarChkTaisyou = "ChkTaisyou_";
        
        //��ʕ\�����i
        var objSeikyuuDateFrom = null;
        var objSeikyuuDateTo = null;
        var objSeikyuuSakiKbn = null;
        var objSeikyuuSakiCd = null;
        var objSeikyuuSakiBrc = null;   
        var objSeikyuuSimeDate = null;
        var objSeikyuuSyosiki = null;
        var objMeisaiKensuuFrom = null;
        var objMeisaiKensuuTo = null;
        var objTorikesiTaisyou = null;
        var objInjiTaisyou = null;
        
        //Hidden
        var objHdnPrmSeikyuuDateTo = null;
        var objCsvOutPutFlg = null;
        var objCsvMaxCntFlg = null;

        //�������s�p
        var objMaxSearchCount = null;
        var objSearch = null;
        //�_�~�[�{�^��
        var objCsv = null;
        
        //��ʑJ�ڗp
        var objSendVal_SeikyuusyoNo = null;
        var objSendVal_SeikyuusyoNoPrint = null;
        var objSendVal_UpdDatetime = null;
        var objSendVal_ChkedTaisyou = null;
        var objSendVal_PrintTaisyougai = null;
        var objSendVal_TorikesiTaisyougai = null;
        var objSendVal_SyosikiTaisyougai = null;
        
        var objSendVal_GamenMode = null;
        
        var gVarPdfFlg = null; //Pdf�o�͔��f�t���O
        var gVarExcelFlg = null; //Excel�o�͔��f�t���O
        
        //�A�N�V�������s�{�^��(�������s,CSV�o��,���������,���������)
        var gBtnSearch = null;
        var gBtnCsv = null;
        var gBtnPrint = null;
        var gBtnExcel = null;
        var gBtnTorikesi = null;
                
        //�Y���f�[�^�Ȃ����b�Z�[�W����p�t���O
        var gNoDataMsgFlg = null;
        
        //CSV�o�͏��������b�Z�[�W
        var gVarCfmMsg = null;
        
        /*************************************
         * onload���̒ǉ�����
         *************************************/
        function funcAfterOnload() {
            //��ʕ\�����i�Z�b�g
            setGlobalObj();
            
            /*�������ʃe�[�u�� �\�[�g�ݒ�*/
            sortables_init();
            
            /*�������ʃe�[�u�� �e�탌�C�A�E�g�ݒ�*/
            settingResultTable();
            
            /* �e��{�^���������������s�Ȃ� */
            exeButtonJikkou();
        }
        
        /*********************************************
        * �e��{�^���������������s�Ȃ�
        *********************************************/
        function exeButtonJikkou(){
            // CSV�o�͂��s�Ȃ�
            if (objCsvOutPutFlg.value == "1"){
            
                // CSV�o�̓t���O���N���A
                objCsvOutPutFlg.value = "";
                
                //�`�F�b�N��Ԃ�߂�
                setCheckedReturn();
                
                // CSV�o�͏���̊m�F
                if(objCsvMaxCntFlg.value == "1"){
                   gVarCfmMsg = "<%= Messages.MSG017C %>".replace("����","CSV�o�͏���") 
                             + "<%= Messages.MSG162C %>".replace("@PARAM1","<%=EarthConst.MAX_CSV_OUTPUT %>").replace("@PARAM2","<%=EarthConst.MAX_CSV_OUTPUT %>")
                }else{
                   gVarCfmMsg = "<%= Messages.MSG017C %>".replace("����","CSV�o�͏���")
                }
                
                // CSV�o�͏���t���O���N���A
                objCsvMaxCntFlg.value = "";
                
                //�����m�F
                if(!confirm(gVarCfmMsg)){
                    // CSV�o�̓t���O���N���A
                    objCsvOutPutFlg.value = "";
                    return false;
                }
                //CSV���s
                objCsv.click();
                return true;
            }
            // Pdf�o��
            if(gVarPdfFlg != null){
                gVarPdfFlg = null; //������
                
                var varMsg = "";
                
                //������͓o�^�ς݂����A������鐿�����������ꍇ
                if(objSendVal_SeikyuusyoNoPrint.value == ""){
                    varMsg = "<%= Messages.MSG173C %>";
                    alert(varMsg);
                    return false;
                }
                
                //PDF�o��
                PdfOutput();
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
            objHdnPrmSeikyuuDateTo = objEBI("<%= HiddenPrmSeikyuusyoHakDateTo.clientID %>");
            objSeikyuuDateFrom = objEBI("<%= TextSekyuusyoHakDateFrom.clientID %>");
            objSeikyuuDateTo = objEBI("<%= TextSekyuusyoHakDateTo.clientID %>");
            objSeikyuuSakiKbn = objEBI("<%= SelectSeikyuuSakiKbn.clientID %>");
            objSeikyuuSakiCd = objEBI("<%= TextSeikyuuSakiCd.clientID %>");
            objSeikyuuSakiBrc = objEBI("<%= TextSeikyuuSakiBrc.clientID %>");
            objSeikyuuSimeDate = objEBI("<%= TextSeikyuuSimeDate.clientID %>");
            objSeikyuuSyosiki = objEBI("<%= SelectSeikyuuSyousiki.clientID %>");
            objMeisaiKensuuFrom = objEBI("<%= TextMeisaiKensuuFrom.clientID %>");
            objMeisaiKensuuTo = objEBI("<%= TextMeisaiKensuuTo.clientID %>");
            objTorikesiTaisyou = objEBI("<%= CheckTorikesiTaisyou.clientID %>");
            objInjiTaisyou = objEBI("<%= CheckInjiTaisyou.clientID %>");
            
            //�������s�p
            objMaxSearchCount = objEBI("<%= maxSearchCount.clientID %>");
            objSearch = objEBI("<%= search.clientID %>");
            
            // CSV�o�͗p
            objCsv = objEBI("<%= ButtonHiddenCsv.clientID %>");
            objCsvOutPutFlg = objEBI("<%= HiddenCsvOutPut.clientID %>");
            objCsvMaxCntFlg = objEBI("<%= HiddenCsvMaxCnt.clientID %>");
            
            //��ʑJ�ڗp
            objSendVal_SeikyuusyoNo = objEBI("<%= HiddenSendValSeikyuusyoNo.clientID %>");
            objSendVal_SeikyuusyoNoPrint = objEBI("<%= HiddenSendValSeikyuusyoNoPrint.clientID %>");
            objSendVal_UpdDatetime = objEBI("<%= HiddenSendValUpdDatetime.clientID %>");
            objSendVal_ChkedTaisyou = objEBI("<%= HiddenChkedTaisyou.clientID %>");
            objSendVal_PrintTaisyougai = objEBI("<%= HiddenPrintTaisyougai.clientID %>");
            objSendVal_TorikesiTaisyougai = objEBI("<%= HiddenTorikesiTaisyougai.clientID %>");
            objSendVal_SyosikiTaisyougai = objEBI("<%= HiddenSyosikiTaisyougai.clientID %>");           
                        
            objSendVal_GamenMode = objEBI("<%= HiddenGamenMode.clientID %>");
            
            //�A�N�V�������s�{�^��
            gBtnSearch = "<%= EarthEnum.emSearchSeikyuusyoBtnType.Search %>";
            gBtnCsv = "<%= EarthEnum.emSearchSeikyuusyoBtnType.CsvOutput %>";
            gBtnPrint = "<%= EarthEnum.emSearchSeikyuusyoBtnType.Print %>";
            gBtnExcel = "Excel";
            gBtnTorikesi = "<%= EarthEnum.emSearchSeikyuusyoBtnType.Torikesi %>";
        }

        /**
         * ���׍s���_�u���N���b�N�����ۂ̏���
         * @param objSelectedTr
         * @return
         */
        function returnSelectValue(objSelectedTr) {
            var varTrId = objSelectedTr.id;
            var varRow = varTrId.replace(gVarSettouJi,"");
            varRow = varRow.replace(gVarTr1,"");
            varRow = varRow.replace(gVarTr2,"");
            varRow = varRow.replace(gVarTdSentou,"");
            
            var varHdn = _d.getElementById(gVarSettouJi + gVarHdnSeikyuusyoNo + varRow);
           
            PopupSyuusei(varHdn.value);
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
            var minHeight = 100;                                            // �E�B���h�E���T�C�Y���̌������ʃe�[�u���ɐݒ肷��Œፂ��
            var adjustHeight = 50;                                          // ��������(�傫�����A�������ʃe�[�u�����Ⴍ�Ȃ�)
            var adjustWidth = 615;                                          // ������(�傫�����A�������ʃe�[�u���������Ȃ�)

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
            objSeikyuuDateTo.value = objHdnPrmSeikyuuDateTo.value;
            objMaxSearchCount.selectedIndex = 1;
            objSeikyuuDateFrom.focus();
            objTorikesiTaisyou.click();
            if(objSendVal_GamenMode.value == "<%= EarthEnum.emSeikyuuSearchType.KakoSearchSeikyuusyo %>"){ //�ߋ��������ꗗ
                objInjiTaisyou.click();
            }
        }
        
        /***********************************
         * �u�������s�v�������̃`�F�b�N����
         ***********************************/
        function checkJikkou(varAction){    
                        
            //��ʕ\�����i�Z�b�g
            setGlobalObj();
            
            var varMsg = "";
                        
            //���������s��To�������� �K�{�`�F�b�N
            if(objSeikyuuDateTo.value.Trim() == ""){
                varMsg = "<%= Messages.MSG013E %>";
                varMsg = varMsg.replace("@PARAM1","���������s��To");
                alert(varMsg);
                return false;
            }
            
            if(objSendVal_GamenMode.value == "<%= EarthEnum.emSeikyuuSearchType.SearchSeikyuusyo %>"){ //�������ꗗ

                //���׌��� �召�`�F�b�N
                if(!checkDaiSyou(objMeisaiKensuuFrom,objMeisaiKensuuTo,"���׌���")){return false};   
                       
            }
            
            //�����������������ߓ� or ������R�[�h�@�K�{�`�F�b�N                
            //������R�[�h��������
            if(objSeikyuuSakiCd.value.Trim() == ""){
                
                //���������������͂܂��͐������ߓ���������
                if(objSeikyuuSyosiki.value.Trim() == "" || objSeikyuuSimeDate.value.Trim() == "" ){
                     varMsg = "<%= Messages.MSG026E %>";
                     varMsg = varMsg.replace("@PARAM1","�u�����������������ߓ��v");
                     varMsg = varMsg.replace("@PARAM2","�u������R�[�h�v");
                     alert(varMsg);
                     objSeikyuuSakiCd.focus();
                return false;
                }   
            }       
            
            //���������s�� �召�`�F�b�N
            if(!checkDaiSyou(objSeikyuuDateFrom,objSeikyuuDateTo,"���������s��")){return false};
            
            if(varAction == gBtnSearch){
                //�\�������u500���v�`�F�b�N
                if(objMaxSearchCount.value == "500"){
                    if(!confirm(("<%= Messages.MSG007C %>").replace("������","500��"))){return false};
                }
                //�������s
                objSearch.click();
                
            }else if(varAction == gBtnCsv){
                if(ChkTaisyou(varAction) == false){return false;}
            
                objCsvOutPutFlg.value = "1";
                
                //�������s
                objSearch.click();
            }
            return true;
        }
        
        /*********************
         * �召�`�F�b�N
         *********************/
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
        
        /**
         * �Ώ�ALL�`�F�b�N�{�b�N�X�E�ꊇ�`�F�b�N����
         * @return
         */
        function setCheckedAll(objChk){
            var tableData1 = objEBI("<%=TableDataTable1.clientID %>");
            var arrSakiTr = tableData1.tBodies[0].rows;
            var objTd = null;
            var arrInput = null;
            
            var objTmpId = null;
            var objTmpChk = null;
                        
            for ( var tri = 0; tri < arrSakiTr.length; tri++) {
                objTd = arrSakiTr[tri].cells[0];
                
                arrInput = getChildArr(objTd,"INPUT");
                for ( var ar = 0; ar < arrInput.length; ar++) {
                    if(arrInput[ar].type == "checkbox"){
                        objTmpId = arrInput[ar].id;
                        objTmpChk = objEBI(objTmpId);
                        objTmpChk.checked = objChk.checked;
                        if(tri == 0 && objChk.checked == true){ //�擪�s���Ώ�ALL=�`�F�b�N�̏ꍇ
                            selectedLineColor(objTd); //�擪�s��I��
                        }
                    }
                }
            }
            return true;
        }
                
        /**
         * �Ώۃ`�F�b�N�{�b�N�X�E�`�F�b�N�󋵖߂�����(CSV�o�̓{�^������������)
         * @return
         */
        function setCheckedReturn(){
            //��ʕ\�����i�Z�b�g
            setGlobalObj();
               
            //�Y���f�[�^�̃`�F�b�N������
            setCmnChecked(objSendVal_ChkedTaisyou.value,true);
        }
        
        /**
         * �`�F�b�N�{�b�N�X�E���ʃ`�F�b�N����
         * (�������FClientID�̃Z�p���[�^�܂ށA�������F�`�F�b�N�L��)
         * @return
         */
        function setCmnChecked(StrClientID,blnChked){           
            var objTmpId = null;
            var objTmpChk = null;
                        
            if(StrClientID == ""){return false;}
            
            var arrChked = StrClientID.split(sepStr);
            
            for ( var tri = 0; tri < arrChked.length; tri++) {
                if(arrChked[tri] == "") continue;
                objTmpId = arrChked[tri];
                objTmpChk = objEBI(objTmpId);
                if(objTmpChk == null) continue;
                objTmpChk.checked = blnChked;
            }
        }
        
        /**
         * �Ώۃ`�F�b�N�{�b�N�X�E���̓`�F�b�N����
         * �`�F�b�N������ꍇ�A�Ώۂ̐�����NO,�X�V����,�������p���ėp�R�[�h�t���O��Hidden�Ɋi�[����
         * @return
         */
        function ChkTaisyou(varAction){
            //��ʕ\�����i�Z�b�g
            setGlobalObj();
            
            var tableData1 = objEBI("<%=TableDataTable1.clientID %>");
            var tableData2 = objEBI("<%=TableDataTable2.clientID %>");
            var arrSakiTr1 = tableData1.tBodies[0].rows;
            var arrSakiTr2 = tableData2.tBodies[0].rows;
            var objTd = null;
            var objTdSeikyuusyoNo = null;
            var arrInput = null;
            var bukkenCount = 0;
            var meisaiSumCnt = 0;
            var objTmpId = null;
            var objTmpSeiNo = null;
            var objPrintBtn = objEBI("<%=ButtonSeikyuusyoPrint.clientID %>");
            var objGonyuukinGaku = null;
            
            var ErrMsg = "";
            
            //Key�����N���A
            ClearKeyInfo();

            for ( var tri = 0; tri < arrSakiTr1.length; tri++) {
                objTd = arrSakiTr1[tri].cells[0];
                
                //������z�̎擾
                objGonyuukinGaku = arrSakiTr2[tri].cells[4].innerHTML.replace(",","");
                
                arrInput = getChildArr(objTd,"INPUT");               
                for ( var ar = 0; ar < arrInput.length; ar++) {
                    if(arrInput[ar].type == "checkbox" && arrInput[ar].checked){
                        objTmpId = arrInput[ar].id; //checkbox�Ώ�
                        objSendVal_ChkedTaisyou.value += objTmpId + sepStr;
                        
                        objTmpId = arrInput[0].id; //hidden������NO
                        objTmpVal = objEBI(objTmpId);
                        objTmpSeiNo = objEBI(objTmpId);
                        objSendVal_SeikyuusyoNo.value += objTmpVal.value + sepStr;
                        
                        objTmpId = arrInput[1].id; //hidden�X�V����
                        objTmpVal = objEBI(objTmpId);
                        objSendVal_UpdDatetime.value += objTmpVal.value + sepStr;
                        
                        objTmpId = arrInput[2].id; //hiddenCSV�o�͑ΏۊO                       
                        objTmpVal = objEBI(objTmpId);
                        if(objTmpVal.value == "1"){ //�t���O�������Ă���ꍇ
                            objTmpId = arrInput[ar].id; //checkbox�Ώ�
                            objSendVal_PrintTaisyougai.value += objTmpId + sepStr; 
                        }
                        
                        objTmpId = arrInput[3].id; //hidden����t���O
                        objTmpVal = objEBI(objTmpId);
                        if(objTmpVal.value == "1"){ //�t���O�������Ă���ꍇ
                            objTmpId = arrInput[ar].id; //checkbox�Ώ�
                            objSendVal_TorikesiTaisyougai.value += objTmpId + sepStr; 
                        }
                        
                        objTmpId = arrInput[4].id; //hidden�����t���O
                        objTmpVal = objEBI(objTmpId);
                        if(objTmpVal.value == "<%= EarthConst.ISNULL %>"){ //���ݒ�̏ꍇ
                            objTmpId = arrInput[ar].id; //checkbox�Ώ�
                            objSendVal_SyosikiTaisyougai.value += objTmpId + sepStr; 
                        }
                        
                        objTmpId = arrInput[5].id; //hidden���׌���
                        objTmpVal = objEBI(objTmpId);
                        meisaiSumCnt += eval(objTmpVal.value);
                        
                        //����������Ώېݒ�
                        if(objPrintBtn.value == "<%= EarthConst.BUTTON_SEIKYUUSYO_PRINT %>"){
                            //�������ꗗ��ʂł͖��׌���0���̏ꍇ�A������s��Ȃ�
                            if(objTmpVal.value != 0){
                                objSendVal_SeikyuusyoNoPrint.value += objTmpSeiNo.value + sepStr;
                            }
                        }else if(objPrintBtn.value == "<%= EarthConst.BUTTON_SEIKYUUSYO_RE_PRINT %>"){
                            //�ߋ��������ꗗ��ʂł͖��׌���0���ł������z��0����Ȃ���Έ�����s��
                            if(objTmpVal.value != 0){
                                //���ׂ�����ꍇ
                                objSendVal_SeikyuusyoNoPrint.value += objTmpSeiNo.value + sepStr;
                            }else{
                                //���ׂ��Ȃ��ꍇ
                                if(objGonyuukinGaku != 0){
                                    objSendVal_SeikyuusyoNoPrint.value += objTmpSeiNo.value + sepStr;
                                }
                            }
                        }
                        
                        bukkenCount++;
                    }
                }
            }
            //����I������Ă��Ȃ������ꍇ�A�G���[
            if(1 > bukkenCount){
                alert("<%= Messages.MSG140E %>");
                ClearKeyInfo();
                return false;
            }
            
            //CSV�o�͎�
            if(varAction == gBtnCsv){
                //��������𒴂��Ă����ꍇ�A�G���[
                if(bukkenCount > "<%= EarthConst.MAX_CSV_SELECT %>"){
                    alert("<%= Messages.MSG047E %>".replace("{0}",bukkenCount).replace("{1}","CSV�o��").replace("{2}","<%= EarthConst.MAX_CSV_SELECT %>"));
                    ClearKeyInfo();
                    return false;
                }
                //CSV�o�͏���𒴂��Ă����ꍇ���b�Z�[�W��t�^
                if(meisaiSumCnt > "<%= EarthConst.MAX_CSV_OUTPUT %>"){
                    objCsvMaxCntFlg.value = "1"
                }
            }
            
            if(objSendVal_GamenMode.value == "<%= EarthEnum.emSeikyuuSearchType.SearchSeikyuusyo %>"){ //�������ꗗ
                //����������{�^��������
                if(varAction == gBtnPrint){
                    //����ΏۊO�̃`�F�b�N����
                    ErrMsg = "<%= Messages.MSG160C %>".replace("@PARAM1","���");
                    ErrMsg = ErrMsg.replace("@PARAM2","������NO");
                    ErrMsg = ErrMsg.replace("@PARAM2","������NO");
                    if(ChkSeikyuusyoPrint(objSendVal_TorikesiTaisyougai.value,ErrMsg) == false){
                        ClearKeyInfo();
                        return false;
                    }
                    //����ΏۊO�̃`�F�b�N����
                    ErrMsg = "<%= Messages.MSG160C %>".replace("@PARAM1","����ΏۊO");
                    ErrMsg = ErrMsg.replace("@PARAM2","������NO");
                    ErrMsg = ErrMsg.replace("@PARAM2","������NO");
                    if(ChkSeikyuusyoPrint(objSendVal_PrintTaisyougai.value,ErrMsg) == false){
                        ClearKeyInfo();
                        return false;
                    }
                    //�����ΏۊO�̃`�F�b�N����
                    ErrMsg = "<%= Messages.MSG160C %>".replace("@PARAM1","�����������ݒ�");
                    ErrMsg = ErrMsg.replace("@PARAM2","������NO");                    
                    ErrMsg = ErrMsg.replace("@PARAM2","������NO");
                    if(ChkSeikyuusyoPrint(objSendVal_SyosikiTaisyougai.value,ErrMsg) == false){
                        ClearKeyInfo();
                        return false;
                    }
                }
            }
            //EXCEL�o�͎�
            if(varAction == gBtnExcel){
                if(objSendVal_SeikyuusyoNo.value == ""){
                    ClearKeyInfo();
                    return false;
                }
            }
            
            //�����̃Z�p���[�^������
            RemoveSepStr();     
            return true;
        }
        
        /**
         * ������������̃`�F�b�N���ʏ���
         * @return
         */
        function ChkSeikyuusyoPrint(StrClientID,varMsg){
            if(StrClientID == ""){return true;}
            
            var objTmpId = null;
            var objTmpChk = null;
            var arrChked = StrClientID.split(sepStr);
            var strDispNo = "";
            var strSep = "";
            
            //������NO���擾
            for ( var tri = 0; tri < arrChked.length; tri++) {
                if(arrChked[tri] == "") continue;
                objTmpId = arrChked[tri];
                
                //��؂蕶��
                if(tri == 0){
                    strSep = ",";
                }else if( (tri + 1) % 5 == 0){
                    strSep = "\n";
                }else{
                    strSep = ",";
                }
                strDispNo += objTmpId.replace(gVarSettouJi + gVarChkTaisyou,"") + strSep;
            }
            if(confirm(varMsg + strDispNo)){
                //�Y���f�[�^�̃`�F�b�N���͂���
                setCmnChecked(StrClientID,false);
            }else{
                objEBI(arrChked[0]).focus(); //�t�H�[�J�X
            }
            return false;
        }
        
        /**
         * KEY�����N���A����
         * (������NO,�X�V����,�Ώۃ`�F�b�N�{�b�N�X,CSV�o�͑ΏۊO)
         * @return
         */
        function ClearKeyInfo(){
            objSendVal_SeikyuusyoNo.value = "";
            objSendVal_SeikyuusyoNoPrint.value = "";
            objSendVal_UpdDatetime.value = "";
            objSendVal_ChkedTaisyou.value = "";
            objSendVal_PrintTaisyougai.value = "";
            objSendVal_TorikesiTaisyougai.value = "";
            objSendVal_SyosikiTaisyougai.value = "";
        }
        
        /**
         * KEY���̖����̃Z�p���[�^��������������鏈��
         * @return
         */
        function RemoveSepStr(){
            objSendVal_SeikyuusyoNo.value = objSendVal_SeikyuusyoNo.value.replace(/\$\$\$$/, "");
            objSendVal_SeikyuusyoNoPrint.value = objSendVal_SeikyuusyoNoPrint.value.replace(/\$\$\$$/, "");
            objSendVal_UpdDatetime.value = objSendVal_UpdDatetime.value.replace(/\$\$\$$/, "");
            objSendVal_ChkedTaisyou.value = objSendVal_ChkedTaisyou.value.replace(/\$\$\$$/, "");
            objSendVal_PrintTaisyougai.value = objSendVal_PrintTaisyougai.value.replace(/\$\$\$$/, "");
            objSendVal_TorikesiTaisyougai.value = objSendVal_TorikesiTaisyougai.value.replace(/\$\$\$$/, "");
            objSendVal_SyosikiTaisyougai.value = objSendVal_SyosikiTaisyougai.value.replace(/\$\$\$$/, "");
        }
        
        //�q��ʌďo����
        function PopupSyuusei(strUniqueNo){    
            //�I�u�W�F�N�g�̍ēǍ�(Ajax�ĕ`��Ή�)
            objSendTargetWin = objEBI("<%= sendTargetWin.clientID %>");
            if(objSendTargetWin.value == "")objSendTargetWin.value="<%=EarthConst.MAIN_WINDOW_NAME %>";
            
            //�I�[�v���Ώۂ̉�ʂ��w��
            varAction = "<%=UrlConst.SEIKYUUSYO_SYUUSEI %>";
            
            //<!-- ��ʈ��n����� -->
            objSendForm = objEBI("searchForm");
            //�N����� + ������NO
            var objSendVal_SearchTerms = objEBI("sendSearchTerms");
            var strGamen = objSendVal_GamenMode.value + "<%=EarthConst.SEP_STRING %>";
            objSendVal_SearchTerms.value = strGamen + strUniqueNo;
                        
            var varWindowName = "SeikyuusyoSyuusei";
            objSrchWin = window.open("about:Blank", varWindowName, "menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes");
                        
            //�l�Z�b�g
            objSendTargetWin.value = varWindowName;
            objSendForm.target=objSendTargetWin.value;
                            
            objSendForm.action = varAction;
            objSendForm.submit();
        }
        
        //PDF�o�͌ďo����
        function PdfOutput(){               
            //�I�u�W�F�N�g�̍ēǍ�(Ajax�ĕ`��Ή�)
            objSendTargetWin = objEBI("<%= sendTargetWin.clientID %>");
            if(objSendTargetWin.value == "")objSendTargetWin.value="<%=EarthConst.MAIN_WINDOW_NAME %>";
            
            //�I�[�v���Ώۂ̉�ʂ��w��
            varAction = "<%=UrlConst.EARTH2_SEIKYUSYO_FCW_OUTPUT %>";
            
            //<!-- ��ʈ��n����� -->
            objSendForm = objEBI("searchForm");
            //�N����� + ������NO
            var objSendVal_SeiNo = objEBI("seino");
            objSendVal_SeiNo.value = objSendVal_SeikyuusyoNoPrint.value;
                        
            var varWindowName = "SeikyusyoFcwOutput";
            objSrchWin = window.open("about:Blank", varWindowName);
                        
            //�l�Z�b�g
            objSendTargetWin.value = varWindowName;
            objSendForm.target=objSendTargetWin.value;
                            
            objSendForm.action = varAction;
            objSendForm.submit();
        }

        //EXCEL�o�͌ďo����
        function ExcelOutput(){
            //�Ώۃ`�F�b�N�{�b�N�X�̃`�F�b�N����
            if(ChkTaisyou(gBtnExcel) == false) return false;
            
            var tmpSeiNo = objSendVal_SeikyuusyoNo.value.split(sepStr);           
            if(tmpSeiNo.length >= 1){
                tmpSeiNo = tmpSeiNo[0];
            }else{
                return false;
            }
            var varUrl = "<%=UrlConst.EARTH2_SEIKYUUSYO_EXCEL_OUTPUT_TESTPAGE %>";
            var varPrm = "?seino=" + tmpSeiNo;
            window.open(varUrl + varPrm);
        }
    </script>

    <input type="hidden" id="HiddenCsvOutPut" runat="server" /><%--CSV�o�͔��f--%>
    <input type="hidden" id="HiddenCsvMaxCnt" runat="server" /><%--CSV�o�͏�������t���O--%>
    <input type="hidden" id="HiddenPrmSeikyuusyoHakDateTo" runat="server" /><%--�p�����[�^���������s��To--%>
    <input type="hidden" id="HiddenSendValSeikyuusyoNo" runat="server" /><%--������No--%>
    <input type="hidden" id="HiddenSendValSeikyuusyoNoPrint" runat="server" /><%--������No����p--%>
    <input type="hidden" id="HiddenSendValUpdDatetime" runat="server" /><%--�X�V����--%>
    <input type="hidden" id="HiddenChkedTaisyou" runat="server" /><%--�`�F�b�N�ςݑΏۃ`�F�b�N�{�b�N�X--%>
    <input type="hidden" id="HiddenPrintTaisyougai" runat="server" /><%--����ΏۊO�̐�����NO--%>
    <input type="hidden" id="HiddenTorikesiTaisyougai" runat="server" /><%--����ɂ��ΏۊO�̐�����NO--%>
    <input type="hidden" id="HiddenSyosikiTaisyougai" runat="server" /><%--�����������ݒ�ɂ��ΏۊO�̐�����NO--%>
    <input type="hidden" id="HiddenGamenMode" runat="server" />
    <%-- ��ʃ^�C�g�� --%>
    <table>
        <tr>
            <td>
                <table style="text-align: left;" border="0" cellpadding="0" cellspacing="2" class="titleTable">
                    <tr>
                        <th style="text-align: left; width: 160px;">
                            <span id="SpanTitle" runat="server"></span>
                        </th>
                        <th>
                            <input type="button" id="ButtonReturn" value="�߂�" runat="server" style="width: 100px;" tabindex="10" />&nbsp;
                        </th>
                        <th>
                            <input type="button" id="ButtonSeikyuusyoPrint" runat="server" style="background-color: #ffff69; width: 240px;"
                                tabindex="10" /><input type="button" id="ButtonHiddenPrint" value="������sbtn" style="display: none;"
                                    runat="server" />&nbsp;
                        </th>
                        <th style="width: 160px">
                            <input  type="button" id="ButtonMiinsatu" value="������ꗗ" runat="server"
                                style="background-color: #ee5522; color: White; font-weight: bold; width: 150px;" tabindex="10"/>&nbsp;
                        </th>
                        <th style="width: 160px">
                            <input type="button" id="ButtonExcelOutput" value="Excel�o��" style="width: 100px;" tabindex="10" onclick="ExcelOutput()"/>&nbsp;
                        </th>     
                        <th style="width: 270px;" align="right">
                            <input type="button" id="ButtonSeikyuusyoTorikesi" value="���������" runat="server"
                                style="background-color: fuchsia; width: 100px;" tabindex="10" /><input type="button" id="ButtonHiddenTorikesi"
                                    value="������sbtn" style="display: none" runat="server" />&nbsp;
                        </th>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <table style="text-align: left;" class="mainTable" cellpadding="2">
        <thead>
            <tr>
                <th class="tableTitle" colspan="6" rowspan="1">
                    ��������
                    <input id="btnClearWin" value="�N���A" type="reset" class="button" tabindex="20" /></th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td class="koumokuMei">
                    ���������s��</td>
                <td colspan="5">
                    <input id="TextSekyuusyoHakDateFrom" runat="server" maxlength="10" class="date" onblur="checkDate(this);"
                        tabindex="20" />&nbsp;�`&nbsp;<input id="TextSekyuusyoHakDateTo" runat="server" maxlength="10"
                            onblur="checkDate(this);" class="date readOnlyStyle" readonly="readonly" tabindex="-1" /></td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    ������</td>
                <td colspan="3">
                    <asp:UpdatePanel ID="UpdatePanelSeikyuusaki" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <ContentTemplate>
                            <asp:DropDownList ID="SelectSeikyuuSakiKbn" runat="server" TabIndex="30">
                            </asp:DropDownList>
                            <input id="TextSeikyuuSakiCd" runat="server" maxlength="5" style="width: 35px;" class="codeNumber"
                                tabindex="30" />&nbsp;-
                            <input id="TextSeikyuuSakiBrc" runat="server" maxlength="2" style="width: 15px;"
                                class="codeNumber" tabindex="30" />
                            <input id="btnSeikyuuSakiSearch" runat="server" type="button" value="����" class="gyoumuSearchBtn"
                                tabindex="30" onserverclick="btnSeikyuuSakiSearch_ServerClick" />&nbsp;
                            <input id="TextSeikyuuSakiMei" runat="server" class="readOnlyStyle" readonly="readonly"
                                style="width: 190px" tabindex="-1" />&nbsp;
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="koumokuMei">
                    �����於�J�i</td>
                <td class="codeNumber">
                    <input id="TextSeikyuuSakiMeiKana" runat="server" maxlength="40" style="ime-mode: active;
                        width: 320px;" tabindex="30" />
                </td>
            </tr>
            <tr id="TrSearchArea" runat="server">
                <td class="koumokuMei">
                    ��������</td>
                <td id="TdSelectSeikyuuSyousiki">
                    <asp:DropDownList ID="SelectSeikyuuSyousiki" runat="server" TabIndex="40">
                    </asp:DropDownList>
                </td>
                <td class="koumokuMei" id="KoumokumeiSeikyuuSimeDate">
                    �������ߓ�</td>
                <td id="TdSeikyuuSimeDate">
                    <input id="TextSeikyuuSimeDate" runat="server" maxlength="2" class="date" style="width: 30px"
                        onblur="checkDateDD(this);" tabindex="40" /></td>
                <td id="KoumokumeiSearchMeisaiKensuu" class="koumokuMei">
                    ���׌���</td>
                <td id="TdTextMeisaiKensuu">
                    <input id="TextMeisaiKensuuFrom" runat="server" maxlength="4" class="number" style="width: 40px;"
                        tabindex="40" onblur="checkNumberAddFig(this);checkMinus(this);" />&nbsp;�`&nbsp;<input
                            id="TextMeisaiKensuuTo" runat="server" maxlength="4" class="number" style="width: 40px;"
                            tabindex="40" onblur="checkNumberAddFig(this);checkMinus(this);" />
                </td>
            </tr>
            <tr>
                <td style="text-align: center;" colspan="6" rowspan="1">
                    �����������<select id="maxSearchCount" runat="server" tabindex="50">
                        <option value="10">10��</option>
                        <option value="100" selected="selected">100��</option>
                        <option value="500">500��</option>
                    </select>
                    <input id="btnSearch" value="�������s" type="button" runat="server" tabindex="50" style="padding-top: 2px;" />
                    <input type="button" id="search" value="�������sbtn" style="display: none" runat="server"
                        tabindex="-1" />
                    <input type="button" id="ButtonCsv" value="CSV�o��" runat="server" tabindex="50" />
                    <input type="button" id="ButtonHiddenCsv" value="CSV���sbtn" style="display: none"
                        runat="server" tabindex="-1" />
                    <input id="CheckTorikesiTaisyou" value="0" type="checkbox" runat="server" checked="checked"
                        tabindex="50" />����͌����ΏۊO <span id="SpanInjiYousi" runat="server">
                            <input id="CheckInjiTaisyou" value="0" type="checkbox" runat="server" checked="checked"
                                tabindex="50" />�󎚑ΏۊO�̗p���͌����ΏۊO </span>
                </td>
            </tr>
        </tbody>
    </table>
    <table style="height: 30px">
        <tr id="TrMihakkou" runat="server">
            <td>
                �����s�����F
            </td>
            <td id="TdMihakkou" runat="server">
            </td>
            <td>
                ��
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                �������ʁF
            </td>
            <td id="resultCount" runat="server">
            </td>
            <td>
                ��
            </td>
            <td id="TdMsgArea" runat="server" style="color: red; font-weight: bold;">
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0">
        <thead>
            <tr>
                <th style="text-align: left;">
                    <div id="DivLeftTitle" runat="server" class="scrollDivLeftTitleStyle2">
                        <table cellpadding="0" cellspacing="0" id="TableTitleTable1" runat="server" class="scrolltablestyle2 sortableTitle"
                            style="border-top: 1px solid #999999; border-left: 1px solid #999999;">
                            <thead>
                                <tr id="meisaiTableHeaderTr" runat="server" style="height: 20px;">
                                    <th style="width: 50px;" class="unsortable">
                                        �Ώ�<input id="CheckAll" type="checkbox" runat="server" onclick="setCheckedAll(this);"
                                            onfocus="this.select()" tabindex="50" /></th>
                                    <th style="width: 120px;">
                                        ������NO</th>
                                    <th style="width: 40px;">
                                        ���</th>
                                    <th style="width: 90px;">
                                        ������R�[�h</th>
                                    <th style="width: 140px;">
                                        �����於</th>
                                    <th style="width: 140px;">
                                        �����於�Q</th>
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
                                <tr style="height: 20px;">
                                    <th style="width: 90px;">
                                        ���������s��</th>
                                    <th style="width: 75px;">
                                        �������ߓ�</th>
                                    <th style="width: 90px;">
                                        �����\���</th>
                                    <th style="width: 90px;">
                                        �O��䐿���z</th>
                                    <th style="width: 90px;">
                                        ������z</th>
                                    <th style="width: 90px;">
                                        �O��J�z�c��</th>
                                    <th style="width: 110px;">
                                        ����䐿���z</th>
                                    <th style="width: 100px;">
                                        �J�z�c��</th>
                                    <th style="width: 70px;">
                                        �X�֔ԍ�</th>
                                    <th style="width: 90px;">
                                        �d�b�ԍ�</th>
                                    <th style="width: 160px;">
                                        �Z��1</th>
                                    <th style="width: 160px;">
                                        �Z��2</th>
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
    <input type="hidden" id="sendTargetWin" runat="server" />
</asp:Content>

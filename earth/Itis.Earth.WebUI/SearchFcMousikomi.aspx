<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    CodeBehind="SearchFcMousikomi.aspx.vb" Inherits="Itis.Earth.WebUI.SearchFcMousikomi" 
    title="EARTH FC�\������" %>
    
<%@ Import Namespace="Itis.Earth.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script src="js/sortable_sync_ja.js" type="text/javascript">
    </script>

    <script type="text/javascript">
        history.forward();
       
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
        //�R���g���[���ړ���
        var gVarSettouJi = "ctl00_CPH1_"; 
        var gVarTr1 = "DataTable_resultTr1_";
        var gVarTr2 = "DataTable_resultTr2_";
        var gVarTdSentou = "DataTable_Sentou_Td_";
        var gVarHdnMousikomiNo = "HdnMousikomiNo_";
        var gVarChkTaisyou = "ChkTaisyou_";
        
        //��ʕ\�����i
        var objMousikomiNoFrom = null;
        var objMousikomiNoTo = null;
        var objMousikomiDateFrom = null;
        var objMousikomiDateTo = null;
        var objKameitenCd = null;
        var objIraiDateFrom = null;
        var objIraiDateTo = null;
        var objBukkenMeisyou = null;
        var objDoujiIraiTousuuFrom = null;
        var objDoujiIraiTousuuTo = null;
        var objKubun = null;
        var objHosyousyoNoFrom = null;
        var objHosyousyoNoTo = null;
        var objStatus = null;
        var objYouTyuuiUmu = null;

        //�������s�p
        var objMaxSearchCount = null;
        var objSearch = null;

        //��ʑJ�ڗp
        var objSendVal_MousikomiNo = null;
        var objSendVal_UpdDatetime = null;
        var objSendVal_ChkedTaisyou = null;
�@      var objSendVal_JutyuuZumi = null;
�@      var objSendVal_DoujiIraiTousuu = null;
        var objSendVal_TyoufukuAri = null;
     
        //�A�N�V�������s�{�^��(�������s,�V�K��)
        var gBtnSearch = null;
        var gBtnJutyuu = null;

        //�Y���f�[�^�Ȃ����b�Z�[�W����p�t���O
        var gNoDataMsgFlg = null;

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
            
        }
        
        /*********************************************
        * �߂�l���Ȃ��ׁA�����\�b�h���I�[�o�[���C�h
        *********************************************/
        function returnSelectValue(){
            return false;
        }
        
        /*********************
         * �����X���N���A
         *********************/
        function clrKameitenInfo(obj){
            if(obj.value == ""){
                //�l�̃N���A
                objEBI("<%= TextKameitenMei.clientID %>").value = "";
                objEBI("<%= TextTorikesiRiyuu.clientID %>").value = "";
                //�F���f�t�H���g��
                objEBI("<%= TextKameitenCd.clientID %>").style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>"
            }
        }

        /*********************************************
        * ��ʕ\�����i�I�u�W�F�N�g���O���[�o���ϐ���
        *********************************************/
        function setGlobalObj() {
            //��ʕ\�����i
            objMousikomiNoFrom = objEBI("<%= TextMousikomiNoFrom.clientID %>");
            objMousikomiNoTo = objEBI("<%= TextMousikomiNoTo.clientID %>");
            objMousikomiDateFrom = objEBI("<%= TextMousikomiDateFrom.clientID %>");
            objMousikomiDateTo = objEBI("<%= TextMousikomiDateTo.clientID %>");
            objKameitenCd = objEBI("<%= TextKameitenCd.clientID %>");
            objIraiDateFrom = objEBI("<%= TextIraiDateFrom.clientID %>");
            objIraiDateTo = objEBI("<%= TextIraiDateTo.clientID %>");
            objBukkenMeisyou = objEBI("<%= TextBukkenMeisyou.clientID %>");
            objDoujiIraiTousuuFrom = objEBI("<%= TextDoujiIraiTousuuFrom.clientID %>");
            objDoujiIraiTousuuTo = objEBI("<%= TextDoujiIraiTousuuTo.clientID %>");
            objKubun = objEBI("<%= SelectKbn.clientID %>");
            objHosyousyoNoFrom = objEBI("<%= TextHosyousyoNoFrom.clientID %>");
            objHosyousyoNoTo = objEBI("<%= TextHosyousyoNoTo.clientID %>");
            objStatus = objEBI("<%= SelectStatus.clientID %>");
            objYouTyuuiUmu = objEBI("<%= CheckYouTyuuiUmu.clientID %>");
            
            //�������s�p
            objMaxSearchCount = objEBI("<%= maxSearchCount.clientID %>");
            objSearch = objEBI("<%= search.clientID %>");
            
            //��ʑJ�ڗp
            objSendVal_MousikomiNo = objEBI("<%= HiddenSendValMousikomiNo.clientID %>");
            objSendVal_UpdDatetime = objEBI("<%= HiddenSendValUpdDatetime.clientID %>");
            objSendVal_ChkedTaisyou = objEBI("<%= HiddenChkedTaisyou.clientID %>");
            objSendVal_JutyuuZumi = objEBI("<%= HiddenJutyuuZumi.clientID %>");
            objSendVal_DoujiIraiTousuu = objEBI("<%= HiddenDoujiIraiTousuu.clientID %>");
            objSendVal_TyoufukuAri = objEBI("<%= HiddenTyoufukuAri.clientID %>");
            
            //�A�N�V�������s�{�^��
            gBtnSearch = "<%= EarthEnum.emSearchMousikomiBtnType.Search %>";
            gBtnJutyuu = "<%= EarthEnum.emSearchMousikomiBtnType.SinkiJutyuu %>";

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
            
            var varHdn = _d.getElementById(gVarSettouJi + gVarHdnMousikomiNo + varRow);
           
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
            var minHeight = 120;                                            // �E�B���h�E���T�C�Y���̌������ʃe�[�u���ɐݒ肷��Œፂ��
            var adjustHeight = 50;                                          // ��������(�傫�����A�������ʃe�[�u�����Ⴍ�Ȃ�)
            var adjustWidth = 495;                                          // ������(�傫�����A�������ʃe�[�u���������Ȃ�)

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
            objStatus.selectedIndex = 0;
            objMaxSearchCount.selectedIndex = 1;
            objYouTyuuiUmu.checked = false;        
            objMousikomiNoFrom.focus();
            
        }
        
        /***********************************
        * �u�������s�v�������̃`�F�b�N����
        ***********************************/
        function checkJikkou(varAction){    
                        
            //��ʕ\�����i�Z�b�g
            setGlobalObj();
            
            var varMsg = "";
            
            //�\��NO �召�`�F�b�N
            if(!checkDaiSyou(objMousikomiNoFrom,objMousikomiNoTo,"�\��NO")){return false};   
           
            //�\���� �召�`�F�b�N
            if(!checkDaiSyou(objMousikomiDateFrom,objMousikomiDateTo,"�\����")){return false};   

            //�˗��� �召�`�F�b�N
            if(!checkDaiSyou(objIraiDateFrom,objIraiDateTo,"�˗���")){return false};   
            
            //�����˗����� �召�`�F�b�N
            if(!checkDaiSyou(objDoujiIraiTousuuFrom,objDoujiIraiTousuuTo,"�����˗�����")){return false};   

            //�ԍ� �召�`�F�b�N
            if(!checkDaiSyou(objHosyousyoNoFrom,objHosyousyoNoTo,"�ԍ�")){return false};   

            if(varAction == gBtnSearch){
                //�\�������u�������v�`�F�b�N
                if(objMaxSearchCount.value == "max"){
                    if(!confirm(("<%= Messages.MSG007C %>")))return false;
                }
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
        * �Ώۃ`�F�b�N�{�b�N�X�E�`�F�b�N�󋵖߂�����(�V�K�󒍃{�^������������)
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
        * �`�F�b�N������ꍇ�A�Ώۂ̐\��NO,�X�V������Hidden�Ɋi�[����
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
            var objTdMousikomiNo = null;
            var arrInput = null;
            var bukkenCount = 0;
            var objTmpId = null;
            var objTmpMouNo = null;
            
            var ErrMsg = "";
      
            //Key�����N���A
            ClearKeyInfo();

            for ( var tri = 0; tri < arrSakiTr1.length; tri++) {
                objTd = arrSakiTr1[tri].cells[0];

                arrInput = getChildArr(objTd,"INPUT");  
           
                for ( var ar = 0; ar < arrInput.length; ar++) {
                    if(arrInput[ar].type == "checkbox" && arrInput[ar].checked){
                        objTmpId = arrInput[ar].id; //checkbox�Ώ�
                        objSendVal_ChkedTaisyou.value += objTmpId + sepStr;
                        
                        objTmpId = arrInput[0].id; //hidden�\��NO
                        objTmpVal = objEBI(objTmpId);
                        objTmpMouNo = objEBI(objTmpId);
                        objSendVal_MousikomiNo.value += objTmpVal.value + sepStr;
                        
                        objTmpId = arrInput[1].id; //hidden�X�V����
                        objTmpVal = objEBI(objTmpId);
                        objSendVal_UpdDatetime.value += objTmpVal.value + sepStr;
                        
                        objTmpId = arrInput[2].id; //hidden�󒍍�
                        objTmpVal = objEBI(objTmpId);
                        if(objTmpVal.value == "<%= EarthConst.MOUSIKOMI_STATUS_ZUMI_JUTYUU %>"){ //�󒍍ς̏ꍇ
                            objTmpId = arrInput[ar].id; //checkbox�Ώ�
                            objSendVal_JutyuuZumi.value += objTmpId + sepStr; 
                        }
                        
                        objTmpId = arrInput[3].id; //hidden�����˗�����
                        objTmpVal = objEBI(objTmpId);

                        if(objTmpVal.value > "<%= EarthConst.MOUSIKOMI_DOUJI_IRAI_1_TOU %>"){ //�����˗�������1�����傫���ꍇ(null�ȊO)
                            if(objTmpVal.value != "isnull"){
                                objTmpId = arrInput[ar].id; //checkbox�Ώ�
                                objSendVal_DoujiIraiTousuu.value += objTmpId + sepStr; 
                            }
                        }

                        objTmpId = arrInput[4].id; //hidden�d������                       
                        objTmpVal = objEBI(objTmpId);
                        if(objTmpVal.value == "<%= EarthConst.TYOUFUKU_ARI %>"){ //�d������������ꍇ
                                objTmpId = arrInput[ar].id; //checkbox�Ώ�
                                objSendVal_TyoufukuAri.value += objTmpId + sepStr; 
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
            //�V�K�󒍎�
            if(varAction == gBtnJutyuu){
�@              //����������𒴂��Ă����ꍇ�A�G���[
                if(bukkenCount > objEBI("<%=HiddenSinkiJutyuuMax.clientID %>").value){
                    alert("<%= Messages.MSG124E %>".replace("{0}",bukkenCount).replace("{1}",objEBI("<%=HiddenSinkiJutyuuMax.clientID %>").value).replace("�ꊇ�ύX","�V�K��"));
                    ClearKeyInfo();
                    return false;
                }
                //�󒍍ς̃`�F�b�N����
                ErrMsg = "<%= Messages.MSG160C %>".replace("@PARAM1","�󒍍�");
                ErrMsg = ErrMsg.replace("@PARAM2","�\��NO");
                ErrMsg = ErrMsg.replace("@PARAM2","�\��NO");
                if(ChkSinkiJutyuu(objSendVal_JutyuuZumi.value,ErrMsg) == false){
                    ClearKeyInfo();
                    return false;
                }
                //�����˗������̃`�F�b�N����
                ErrMsg = "<%= Messages.MSG225C %>".replace("@PARAM1","�����˗�������2���ȏ�");
                ErrMsg = ErrMsg.replace("@PARAM2","�\��NO");
                ErrMsg = ErrMsg.replace("@PARAM2","�\��NO");
                if(CfmSinkiJutyuu(objSendVal_DoujiIraiTousuu.value,ErrMsg) == false){
                    ClearKeyInfo();
                    return false;
                }
                //�d�������̃`�F�b�N����
                ErrMsg = "<%= Messages.MSG160C %>".replace("@PARAM1","�d����������");
                ErrMsg = ErrMsg.replace("@PARAM2","�\��NO");
                ErrMsg = ErrMsg.replace("@PARAM2","�\��NO");
                if(ChkSinkiJutyuu(objSendVal_TyoufukuAri.value,ErrMsg) == false){
                    ClearKeyInfo();
                    return false;
                }
                
            }
             //�����̃Z�p���[�^������
            RemoveSepStr();     
            return true;
        }
        
         /**
         * �V�K�󒍎��̃`�F�b�N����
         * @return
         */
        function ChkSinkiJutyuu(StrClientID,varMsg){
            if(StrClientID == ""){return true;}
            
            var objTmpId = null;
            var objTmpChk = null;
            var arrChked = StrClientID.split(sepStr);
            var strDispNo = "";
            var strSep = "";
            
            //�\��NO���擾
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
         * �V�K�󒍎��̊m�F����
         * @return
         */
        function CfmSinkiJutyuu(StrClientID,varMsg){
            if(StrClientID == ""){return true;}
            
            var objTmpId = null;
            var objTmpChk = null;
            var arrChked = StrClientID.split(sepStr);
            var strDispNo = "";
            var strSep = "";
            
            //�\��NO���擾
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
                // �����p��
                // setCmnChecked(StrClientID,false);
                return true;
            }else{
                objEBI(arrChked[0]).focus(); //�t�H�[�J�X
            }
            return false;
        }
       
         /**
         * KEY�����N���A����
         * (�\��NO,�X�V����,�Ώۃ`�F�b�N�{�b�N�X)
         * @return
         */
        function ClearKeyInfo(){
            objSendVal_MousikomiNo.value = "";
            objSendVal_UpdDatetime.value = "";
            objSendVal_ChkedTaisyou.value = "";
            objSendVal_JutyuuZumi.value = "";
            objSendVal_DoujiIraiTousuu.value = "";
            objSendVal_TyoufukuAri.value = "";   
        }
        
         /**
         * KEY���̖����̃Z�p���[�^��������������鏈��
         * @return
         */
        function RemoveSepStr(){
            objSendVal_MousikomiNo.value = objSendVal_MousikomiNo.value.replace(/\$\$\$$/, "");
            objSendVal_UpdDatetime.value = objSendVal_UpdDatetime.value.replace(/\$\$\$$/, "");
            objSendVal_ChkedTaisyou.value = objSendVal_ChkedTaisyou.value.replace(/\$\$\$$/, "");
            objSendVal_JutyuuZumi.value = objSendVal_JutyuuZumi.value.replace(/\$\$\$$/, "");
            objSendVal_DoujiIraiTousuu.value = objSendVal_DoujiIraiTousuu.value.replace(/\$\$\$$/, "");
            objSendVal_TyoufukuAri.value = objSendVal_TyoufukuAri.value.replace(/\$\$\$$/, "");
        }

        //�q��ʌďo����
        function PopupSyuusei(strUniqueNo){    
            //�I�u�W�F�N�g�̍ēǍ�(Ajax�ĕ`��Ή�)
            objSendTargetWin = objEBI("<%= sendTargetWin.clientID %>");
            if(objSendTargetWin.value == "")objSendTargetWin.value="<%=EarthConst.MAIN_WINDOW_NAME %>";
            
            //�I�[�v���Ώۂ̉�ʂ��w��
            varAction = "<%=UrlConst.FC_MOUSIKOMI_SYUUSEI %>";
            
            //<!-- ��ʈ��n����� -->
            objSendForm = objEBI("searchForm");
            //�\��NO
            var objSendVal_SearchTerms = objEBI("sendSearchTerms");
            objSendVal_SearchTerms.value = strUniqueNo;
                        
            var varWindowName = "MousikomiSyuusei";
            objSrchWin = window.open("about:Blank", varWindowName, "menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes");
                        
            //�l�Z�b�g
            objSendTargetWin.value = varWindowName;
            objSendForm.target=objSendTargetWin.value;
                            
            objSendForm.action = varAction;
            objSendForm.submit();
        }
                
        /**
        * �\��NO,�ԍ�,�����˗����� To�����Z�b�g
        * @return true/false
        */
        function setFromTo(obj){
            if(obj.id == objMousikomiNoFrom.id && objMousikomiNoTo.value == ""){
                objMousikomiNoTo.value = obj.value;
                objMousikomiNoTo.select();
            }else if(obj.id == objHosyousyoNoFrom.id && objHosyousyoNoTo.value == ""){
                objHosyousyoNoTo.value = obj.value;
                objHosyousyoNoTo.select();
            }else if(obj.id == objDoujiIraiTousuuFrom.id && objDoujiIraiTousuuTo.value == ""){
                objDoujiIraiTousuuTo.value = obj.value;
                objDoujiIraiTousuuTo.select();
            }
            return true;
        }
      
    </script>

    <input type="hidden" id="HiddenSendValMousikomiNo" runat="server" /><%--�\��No--%>
    <input type="hidden" id="HiddenSendValUpdDatetime" runat="server" /><%--�X�V����--%>
    <input type="hidden" id="HiddenChkedTaisyou" runat="server" /><%--�`�F�b�N�ςݑΏۃ`�F�b�N�{�b�N�X--%>   
    <input type="hidden" id="HiddenJutyuuZumi" runat="server" /><%--�󒍍ς̐\��NO--%>   
    <input type="hidden" id="HiddenDoujiIraiTousuu" runat="server" /><%--�����˗�������1�ȊO�̐\��NO--%>   
    <input type="hidden" id="HiddenTyoufukuAri" runat="server" /><%--�d�������L�̐\��NO--%>   
    <%-- ��ʃ^�C�g�� --%>
    <table style="text-align: left;" border="0" cellpadding="0" cellspacing="2" class="titleTable">
        <tr>
            <th style="text-align: left; width: 450px;">
                FC�\������</th>
            <th>
                <input type="hidden" id="HiddenSinkiJutyuuMax" runat="server" />
                <input type="button" id="ButtonSinkiJutyuu" value="�V�K��" runat="server" style="font-weight: bold; 
                    font-size: 18px; width: 100px; color: black; height: 30px; background-color: fuchsia;"
                    tabindex="10" />
            </th>
        </tr>
    </table>
    <br />
    <table style="text-align: left;" class="mainTable" cellpadding="2">
        <thead>
            <tr>
                <th class="tableTitle" colspan="12" rowspan="1">
                    ��������
                    <input id="btnClearWin" value="�N���A" type="reset" class="button" tabindex="20" /></th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td class="koumokuMei">
                    �\��NO</td>
                <td colspan="3">
                    <input id="TextMousikomiNoFrom" runat="server" maxlength="15" style="width: 110px;" class="codeNumber"
                        onblur="checkNumber(this);" onchange="if(checkNumber(this))setFromTo(this);"
                        tabindex="20" />&nbsp;�`&nbsp;<input id="TextMousikomiNoTo" runat="server" maxlength="15" style="width: 110px;"
                        class="codeNumber" onblur="checkNumber(this);" tabindex="20" /></td>
                <td class="koumokuMei" style="width: 70px;">
                    �\����</td>
                <td colspan="3">
                    <input id="TextMousikomiDateFrom" runat="server" maxlength="10" style="width: 70px;" class="date" onblur="checkDate(this);"
                        tabindex="20" />&nbsp;�`&nbsp;<input id="TextMousikomiDateTo" runat="server" maxlength="10" style="width: 70px;"
                        class="date" onblur="checkDate(this);" tabindex="20" /></td>
                <td class="koumokuMei">
                    �˗���</td>
                <td colspan="3">
                    <input id="TextIraiDateFrom" runat="server" maxlength="10" style="width: 70px;" class="date" onblur="checkDate(this);"
                        tabindex="30" />&nbsp;�`&nbsp;<input id="TextIraiDateTo" runat="server" maxlength="10" style="width: 70px;"
                        class="date" onblur="checkDate(this);" tabindex="30" /></td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    �����X</td>
                <td colspan="3">
                    <asp:UpdatePanel ID="UpdatePanel_kameiten" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <ContentTemplate>
                            <input id="TextKameitenCd" runat="server" maxlength="5" style="width: 40px;" class="codeNumber"
                                tabindex="30" />
                            <input id="ButtonKameitenSearch" runat="server" type="button" value="����" class="gyoumuSearchBtn"
                                 tabindex="30" />&nbsp;
                            <input id="TextKameitenMei" runat="server" class="readOnlyStyle" readonly="readonly"
                                style="width: 235px" tabindex="-1" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="koumokuMei">
                    ���</td>
                <td colspan="7">
                    <asp:UpdatePanel ID="UpdatePanel_kameiToikesi" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ButtonKameitenSearch" />
                        </Triggers>
                        <ContentTemplate>
                            <input id="TextTorikesiRiyuu" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    ��������</td>
                <td colspan="3">
                    <input id="TextBukkenMeisyou" runat="server" maxlength="50" style="width: 333px;" tabindex="30"/></td>
                <td class="koumokuMei">
                    �����˗�����</td>
                <td colspan="3">
                    <input id="TextDoujiIraiTousuuFrom" runat="server" maxlength="2" style="width: 50px;" class="number" onblur="checkNumber(this);"
                        onchange="if(checkNumber(this))setFromTo(this);" tabindex="30" />&nbsp;�`&nbsp;<input id="TextDoujiIraiTousuuTo" runat="server"
                        maxlength="2" style="width: 50px;" class="number" onblur="checkNumber(this);" tabindex="30" />��</td>
                <td class="koumokuMei">
                    �ԍ�</td>
                <td colspan="3">
                    <input id="TextHosyousyoNoFrom" runat="server" maxlength="10" style="width: 75px;" class="codeNumber"
                        onblur="checkNumber(this);" onchange="if(checkNumber(this))setFromTo(this);"
                        tabindex="40" />&nbsp;�`&nbsp;<input id="TextHosyousyoNoTo" runat="server" maxlength="10" style="width: 75px;"
                            class="codeNumber" onblur="checkNumber(this);" tabindex="40" /></td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    �敪</td>
                <td colspan="11">
                    <asp:DropDownList ID="SelectKbn" runat="server" TabIndex="40">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    �������</td>
                <td colspan="11">
                    <asp:UpdatePanel ID="UpdatePanel_tysKaisya" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <ContentTemplate>
                            <input id="TextTysKaisyaCd" runat="server" maxlength="7" style="width: 60px;" class="codeNumber"
                                tabindex="45" />
                            <input id="ButtonTysKaisyaSearch" runat="server" type="button" value="����" class="gyoumuSearchBtn"
                                 tabindex="45" />&nbsp;
                            <input id="TextTysKaisyaMei" runat="server" class="readOnlyStyle" readonly="readonly"
                                style="width: 190px" tabindex="-1" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td style="text-align: center;" colspan="12" rowspan="1">
                    �󒍏�<select id="SelectStatus" runat="server" tabindex="50">
                        <option value="100" selected="selected">����</option>
                        <option value="150">�ۗ�</option>
                        <option value="200">�󒍍�</option>
                    </select>
                    �����������<select id="maxSearchCount" runat="server" tabindex="50">
                        <option value="10">10��</option>
                        <option value="100" selected="selected">100��</option>
                        <option value="max">������</option>
                    </select>
                    <input id="btnSearch" value="�������s" type="button" runat="server" tabindex="50" style="padding-top: 2px;" />
                    <input type="button" id="search" value="�������sbtn" style="display: none" runat="server"
                        tabindex="-1" />
                    <input id="CheckYouTyuuiUmu" type="checkbox" runat="server" tabindex="50" />�v���ӗL
                </td>
            </tr>
        </tbody>
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
                                    <th style="width: 50px;">
                                        �v����</th>
                                    <th style="width: 65px;">
                                        �󒍏�</th>
                                    <th style="width: 40px;">
                                        �敪</th>
                                    <th style="width: 75px;">
                                        �ԍ�</th>
                                    <th style="width: 180px;">
                                        ��������</th>
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
                                    <th style="width: 75px;">
                                        �S_���R�[�h</th>
                                    <th style="width: 170px;">
                                        �S_����</th>
                                    <th style="width: 75px;">
                                        �\_���R�[�h</th>
                                    <th style="width: 170px;">
                                        �\_����</th>                       
                                    <th style="width: 210px;">
                                        �Z��1</th>
                                    <th style="width: 210px;">
                                        �Z��2</th>
                                    <th style="width: 90px;">
                                        �����X�R�[�h</th>
                                    <th style="width: 180px;">
                                        �����X��</th>
                                    <th style="width: 140px;">
                                        �˗���</th>
                                    <th style="width: 80px;">
                                        ���i�R�[�h</th>
                                    <th style="width: 180px;">
                                        ���i��</th>
                                    <th style="width: 90px;">
                                        �����˗�����</th>
                                    <th style="width: 60px;">
                                        �{�喼</th>
                                    <th style="width: 110px;">
                                        ��������R�[�h</th>
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

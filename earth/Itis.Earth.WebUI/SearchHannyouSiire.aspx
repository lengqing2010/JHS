<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="SearchHannyouSiire.aspx.vb" Inherits="Itis.Earth.WebUI.SearchHannyouSiire"
    Title="EARTH �ėp�d���f�[�^�Ɖ�" %>

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
            if(window.name != "<%=EarthConst.MAIN_WINDOW_NAME %>") window.resizeTo(1010,800);
        }catch(e){
            //�A�N�Z�X�����ۂ���܂����̃G���[���o���牽�����Ȃ��B
            if(e.number == 2147024891) throw e;
        }
        
        _d = document;
        /*====================================
         *�O���[�o���ϐ��錾�i��ʕ��i�j
         ====================================*/
        //�R���g���[���ړ���
        var gVarOyaSettouji = "ctl00_CPH1_";
        var gVarTr1 = "DataTable_resultTr1_";
        var gVarTr2 = "DataTable_resultTr2_";
        var gVarTdSentou = "DataTable_Sentou_Td_";
        var gVarHdnSeikyuusyoNo = "HdnUniNo_";
        var gVarChkTaisyou = "CheckTaisyou_";
        
        //��ʑJ�ڗp
        var objSendBtn = null;
        var objSendTargetWin = null;
        
        var objSelectedTr = null;
        var objSendVal_NyuukinNo = null;
        
        var varAction = null;
        
        //��ʕ\�����i
        var objSyouhinCd = null;
        var objAddDateFrom = null;
        var objAddDateTo = null;
        var objSiireDateFrom = null;
        var objSiireDateTo = null;
        var objDenpyouSiireDateFrom = null;
        var objDenpyouSiireDateTo = null;
        var objTorikesiTaisyou = null;
        //�������s�p
        var objMaxSearchCount = null;
        var objSearch = null;


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
            
            //�������ʂ�1���݂̂̏ꍇ�A�l��߂����������s
            if (objEBI("<%= firstSend.clientID %>").value != ""){
                returnSelectValue(objEBI(objEBI("<%= firstSend.clientID %>").value));
            }          
        }
        
       /*********************************************
        * �߂�l���Ȃ��ׁA�����\�b�h���I�[�o�[���C�h
        *********************************************/
        function returnSelectValue(objSelectedTr){
            var varTrId = objSelectedTr.id;
            var varRow = varTrId.replace(gVarOyaSettouji,"");
            varRow = varRow.replace(gVarTr1,"");
            varRow = varRow.replace(gVarTr2,"");
            varRow = varRow.replace(gVarTdSentou,"");
            
            var varHdn = _d.getElementById(gVarOyaSettouji + gVarHdnSeikyuusyoNo + varRow);
           
            PopupSyuusei(varHdn.value);
        }

        //�q��ʌďo����
        function PopupSyuusei(strUniqueNo){    
            //�I�u�W�F�N�g�̍ēǍ�(Ajax�ĕ`��Ή�)
            objSendTargetWin = objEBI("<%= sendTargetWin.clientID %>");
            if(objSendTargetWin.value == "")objSendTargetWin.value="<%=EarthConst.MAIN_WINDOW_NAME %>";
            
            //�I�[�v���Ώۂ̉�ʂ��w��
            varAction = "<%=UrlConst.HANNYOU_SIIRE_SYUUSEI %>";
            
            //<!-- ��ʈ��n����� -->
            objSendForm = objEBI("searchForm");
            //�敪+�ԍ�+����NO
            var objSendVal_SearchTerms = objEBI("sendSearchTerms");
            objSendVal_SearchTerms.value = strUniqueNo;
                        
            var varWindowName = "HannyouSiireSyuusei";
            objSrchWin = window.open("about:Blank", varWindowName, "menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes");
                        
            //�l�Z�b�g
            objSendTargetWin.value = varWindowName;
            objSendForm.target=objSendTargetWin.value;
                            
            objSendForm.action = varAction;
            objSendForm.submit();
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
            objSyouhinCd = objEBI("<%= TextSyouhinCd.clientID %>");
            objAddDateFrom = objEBI("<%= TextAddDateFrom.clientID %>");
            objAddDateTo = objEBI("<%= TextAddDateTo.clientID %>");
            objSiireDateFrom = objEBI("<%= TextSiireDateFrom.clientID %>");
            objSiireDateTo = objEBI("<%= TextSiireDateTo.clientID %>");
            objDenpyouSiireDateFrom = objEBI("<%= TextDenpyouSiireDateFrom.clientID %>");
            objDenpyouSiireDateTo = objEBI("<%= TextDenpyouSiireDateTo.clientID %>");
            objTorikesiTaisyou = objEBI("<%= CheckTorikesiTaisyou.clientID %>");
            //�������s�p
            objMaxSearchCount = objEBI("<%= maxSearchCount.clientID %>");
            objSearch = objEBI("<%= search.clientID %>");
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
            var minHeight = 110;                                            // �E�B���h�E���T�C�Y���̌������ʃe�[�u���ɐݒ肷��Œፂ��
            var adjustHeight = 39;                                          // ��������(�傫�����A�������ʃe�[�u�����Ⴍ�Ȃ�)
            var adjustWidth = 631;                                          // ������(�傫�����A�������ʃe�[�u���������Ȃ�)

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


        /*******************************************
         * All�N���A������Ɏ��s�����t�@���N�V����
         *******************************************/
        function funcAfterAllClear(obj){
            objTorikesiTaisyou.click();
            objMaxSearchCount.selectedIndex = 1;
            objSyouhinCd.focus();
        }


        /***********************************
         * �u�������s�v�������̃`�F�b�N����
         ***********************************/
        function checkJikkou(){
                        
            //�o�^�N���� �召�`�F�b�N
            if(!checkDaiSyou(objAddDateFrom,objAddDateTo,"�o�^�N����"))return false;
            
            //�d���N���� �召�`�F�b�N
            if(!checkDaiSyou(objSiireDateFrom,objSiireDateTo,"�d���N����"))return false;
            
            //�`�[�d���N���� �召�`�F�b�N
            if(!checkDaiSyou(objDenpyouSiireDateFrom,objDenpyouSiireDateTo,"�`�[�d���N����"))return false;

            //�\�������u�������v�`�F�b�N
            if(objMaxSearchCount.value == "max"){
                if(!confirm(("<%= Messages.MSG007C %>")))return false;
            }

            //�������s
            objSearch.click();
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
    </script>

    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tbody>
            <tr>
                <th style="text-align: left; width: 160px;">
                    �ėp�d���f�[�^�Ɖ�</th>
                <th>
                    <input id="ButtonSinki" value="�V�K�o�^" type="button" runat="server" onclick="PopupSyuusei(0)" tabindex="10" />&nbsp;&nbsp;
                    <input id="BtnCsvInput" value="CSV�捞" type="button" runat="server" tabindex="10" />
                </th>
            </tr>
        </tbody>
    </table>
    <br />
    <table style="text-align: left;" class="mainTable" cellpadding="2">
        <thead>
            <tr>
                <th class="tableTitle" colspan="6" rowspan="1">
                    ��������
                    <input id="btnClearWin" value="�N���A" type="reset" class="button" tabindex="10" /></th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td class="koumokuMei">
                    ���i</td>
                <td colspan="2">
                    <asp:UpdatePanel ID="UpdatePanel_syouhin" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <ContentTemplate>
                            <input id="TextSyouhinCd" runat="server" maxlength="8" style="width: 60px;" class="codeNumber" tabindex="20" />
                            <input id="btnSyouhinSearch" runat="server" type="button" value="����" class="gyoumuSearchBtn"
                                onserverclick="btnSyouhinSearch_ServerClick" tabindex="20" />&nbsp;
                            <input id="TextHinmei" runat="server" class="readOnlyStyle" readonly="readonly" style="width: 228px"
                                tabindex="-1" />&nbsp;
                            <input type="hidden" id="hdnSyouhinType" runat="server" tabindex="20" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="koumokuMei">
                    �o�^�N����</td>
                <td colspan="2" class="date">
                    <input id="TextAddDateFrom" runat="server" maxlength="10" class="date" onblur="checkDate(this);" tabindex="20" />&nbsp;�`&nbsp;<input
                        id="TextAddDateTo" runat="server" maxlength="10" class="date" onblur="checkDate(this);" tabindex="20" /></td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    �������</td>
                <td colspan="2" class="codeNumber">
                    <asp:UpdatePanel ID="UpdatePanel_TysKaisya" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <ContentTemplate>
                            <input id="TextTysKaisyaCd" runat="server" maxlength="7" style="width: 45px;" class="codeNumber" tabindex="30" />
                            <input id="ButtonTysKaisyaSearch" runat="server" type="button" value="����" class="gyoumuSearchBtn"
                                onserverclick="ButtonTysKaisyaSearch_ServerClick" tabindex="30" />&nbsp;
                            <input id="TextTysKaisyaMei" runat="server" class="readOnlyStyle" style="width: 15em"
                                readonly="readOnly" tabindex="-1" />
                            <input type="hidden" id="HiddenKameitenCd" runat="server" tabindex="30" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="koumokuMei">
                    ������Ж��J�i</td>
                <td colspan="2" class="codeNumber">
                    <input id="TextTysKaisyaMeiKana" runat="server" maxlength="40" style="ime-mode: active;
                        width: 175px;" tabindex="30" />
                </td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    �d���N����</td>
                <td colspan="2" class="date">
                    <input id="TextSiireDateFrom" runat="server" maxlength="10" class="date" onblur="checkDate(this);" tabindex="40" />&nbsp;�`&nbsp;<input
                        id="TextSiireDateTo" runat="server" maxlength="10" class="date" onblur="checkDate(this);" tabindex="40" /></td>
                <td class="koumokuMei">
                    �`�[�d���N����</td>
                <td colspan="2" class="date">
                    <input id="TextDenpyouSiireDateFrom" runat="server" maxlength="10" class="date" onblur="checkDate(this);" tabindex="40" />&nbsp;�`&nbsp;<input
                        id="TextDenpyouSiireDateTo" runat="server" maxlength="10" class="date" onblur="checkDate(this);" tabindex="40" /></td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    �敪</td>
                <td colspan="2">
                    <asp:TextBox ID="TextKbn" runat="server" MaxLength="1" Style="width: 20px;" CssClass="codeNumber" tabindex="50" />
                </td>
                <td class="koumokuMei">
                    �ԍ�</td>
                <td colspan="3">
                    <asp:TextBox ID="TextHosyousyoNo" runat="server" MaxLength="10" Style="width: 72px;" CssClass="codeNumber" tabindex="50" />
                </td>
                </tr>
            <tr>
            <tr>
                <td style="text-align: center;" colspan="7" rowspan="1">
                    �����������
                    <select id="maxSearchCount" runat="server" tabindex="60" >
                        <option value="10">10��</option>
                        <option value="100" selected="selected">100��</option>
                        <option value="max">������</option>
                    </select>
                    <input id="btnSearch" value="�������s" type="button" runat="server" tabindex="60" />
                    <input type="button" id="search" value="�������sbtn" style="display: none" runat="server" tabindex="60" />
                    <input id="CheckTorikesiTaisyou" value="0" type="checkbox" runat="server" checked="checked" tabindex="60" />����͌����ΏۊO
                </td>
            </tr>
        </tbody>
    </table>
    <input type="hidden" id="returnTargetIds" runat="server" />
    <input type="hidden" id="afterEventBtnId" runat="server" />
    <input type="hidden" id="firstSend" runat="server" />
    <input id="search_shouhin23" runat="server" type="hidden" />
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
                                <tr>
                                    <th style="width: 50px;">
                                        �d��NO</th>
                                    <th style="width: 90px;">
                                        ������ЃR�[�h</th>
                                    <th style="width: 195px;">
                                        ������Ж�</th>
                                    <th style="width: 70px;">
                                        ���i�R�[�h</th>
                                    <th style="width: 195px;">
                                        ���i��</th>
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
                                    <th style="width: 95px;">
                                        �ō��d�����z</th>
                                    <th style="width: 90px;">
                                        �d���N����</th>
                                    <th style="width: 100px;">
                                        �`�[�d���N����</th>
                                    <th style="width: 35px;">
                                        �敪</th>
                                    <th style="width: 80px;">
                                        �ԍ�</th>
                                    <th style="width: 140px;">
                                        �{�喼</th>  
                                    <th style="width: 180px;">
                                        �E�v</th>
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


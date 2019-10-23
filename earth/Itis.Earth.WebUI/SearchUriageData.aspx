<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="SearchUriageData.aspx.vb" Inherits="Itis.Earth.WebUI.SearchUriageData"
    Title="EARTH ����`�[�Ɖ�" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script src="js/sortable_sync_ja.js" type="text/javascript">
    </script>

    <script type="text/javascript">

        //�E�B���h�E�T�C�Y�ύX
        try{
            if(window.name != "<%=EarthConst.MAIN_WINDOW_NAME %>") window.resizeTo(1010,800);
        }catch(e){
            //�A�N�Z�X�����ۂ���܂����̃G���[���o���牽�����Ȃ��B
            if(e.number == 2147024891) throw e;
        }
        
        /*====================================
         *�O���[�o���ϐ��錾�i��ʕ��i�j
         ====================================*/
        //��ʕ\�����i
        var objKubun = null;
        var objKubunAll = null;
        var objBangouFrom = null;
        var objBangouTo = null;
        var objDenBangouFrom = null;
        var objDenBangouTo = null;
        var objAddDateFrom = null;
        var objAddDateTo = null;
        var objSeikyuuDateFrom = null;
        var objSeikyuuDateTo = null;
        var objUriDateFrom = null;
        var objUriDateTo = null;
        var objDenUriDateFrom = null;
        var objDenUriDateTo = null;
        var objSaisinDenpyou = null;
        //�������s�p
        var objMaxSearchCount = null;
        var objSearch = null;
        var objCsv = null;
        //hidden
        var objKubunVal = null;
        //�ύX�|�b�v�A�b�v�p
        var objSelectedTr = null;


        /*************************************
         * onload���̒ǉ�����
         *************************************/
        function funcAfterOnload() {
            //��ʕ\�����i�Z�b�g
            setGlobalObj();
            
            //�敪�̏�Ԃ��Z�b�e�B���O
            setKubunVal()
            
            /*�������ʃe�[�u�� �\�[�g�ݒ�*/
            sortables_init();
            
            /*�������ʃe�[�u�� �e�탌�C�A�E�g�ݒ�*/
            settingResultTable();
            
            //�������ʂ�1���݂̂̏ꍇ�A�l��߂����������s
            if (objEBI("<%= firstSend.clientID %>").value != ""){
                returnSelectValue(objEBI(objEBI("<%= firstSend.clientID %>").value));
            }
            
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
            objCsvOutPutFlg.value = ""
        }
        
       /*********************************************
        * �߂�l���Ȃ��ׁA�����\�b�h���I�[�o�[���C�h
        *********************************************/
        function returnSelectValue(objSelectedTr){
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
            objKubun = objEBI("<%= selectKbn.clientID %>");
            objKubunAll = objEBI("<%= kubun_all.clientID %>");
            objKubunVal = objEBI("<%= kubunVal.clientID %>");       //hidden
            objBangouFrom = objEBI("<%= TextBangouFrom.clientID %>");
            objBangouTo = objEBI("<%= TextBangouTo.clientID %>");
            objDenBangouFrom = objEBI("<%= TextDenpyouBangouFrom.clientID %>");
            objDenBangouTo = objEBI("<%= TextDenpyouBangouTo.clientID %>");
            objAddDateFrom = objEBI("<%= TextAddDateFrom.clientID %>");
            objAddDateTo = objEBI("<%= TextAddDateTo.clientID %>");
            objSeikyuuDateFrom = objEBI("<%= TextSeikyuuDateFrom.clientID %>");
            objSeikyuuDateTo = objEBI("<%= TextSeikyuuDateTo.clientID %>");
            objUriDateFrom = objEBI("<%= TextUriageDateFrom.clientID %>");
            objUriDateTo = objEBI("<%= TextUriageDateTo.clientID %>");
            objDenUriDateFrom = objEBI("<%= TextDenpyouUriageDateFrom.clientID %>");
            objDenUriDateTo = objEBI("<%= TextDenpyouUriageDateTo.clientID %>");
            objSaisinDenpyou = objEBI("<%= CheckSaisinDenpyou.clientID %>");
            //�������s�p
            objMaxSearchCount = objEBI("<%= maxSearchCount.clientID %>");
            objSearch = objEBI("<%= search.clientID %>");
            // CSV�o�͗p
            objCsv = objEBI("<%= ButtonHiddenCsv.clientID %>");
            objCsvOutPutFlg = objEBI("<%= HiddenCsvOutPut.clientID %>");
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
            var adjustHeight = 39;                                          // ��������(�傫�����A�������ʃe�[�u�����Ⴍ�Ȃ�)
            var adjustWidth = 698;                                          // ������(�傫�����A�������ʃe�[�u���������Ȃ�)

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
            objKubunAll.click();
            objSaisinDenpyou.click();
            objMaxSearchCount.selectedIndex = 1;
            objKubunAll.focus();
        }


        /****************************************************************************************
         * �敪�Z���N�g�{�b�N�X���`�F�b�N�{�b�N�X�̏�Ԃ��`�F�b�N
         ****************************************************************************************/
        function setKubunVal(){

            objKubunVal.value = ""; //������
            
            if(objKubunAll.checked == true){
                objKubun.selectedIndex = 0;
                objKubun.disabled = true;
                return;
            }else{
                objKubun.disabled = false;
                if(objKubun.value != ""){
                    objKubunVal.value = objKubun.value;
                }
            }
        }
        
        
        /***********************************
         * �u�������s�v�������̃`�F�b�N����
         ***********************************/
        function checkJikkou(varAction){

            if(!objKubunAll.checked && objKubunVal.value.Trim() == ""){
                alert("<%= Messages.MSG006E %>");
                objKubun.focus();
                return false;
            }

            //�ԍ� �召�`�F�b�N
            if(!checkDaiSyou(objBangouFrom,objBangouTo,"�ԍ�")){return false};
            
            //�`�[�ԍ� �召�`�F�b�N
            if(!checkDaiSyou(objDenBangouFrom,objDenBangouTo,"�`�[�ԍ�")){return false};
            
            //�o�^�N���� �召�`�F�b�N
            if(!checkDaiSyou(objAddDateFrom,objAddDateTo,"�`�[�쐬��")){return false};
            
            //�����N���� �召�`�F�b�N
            if(!checkDaiSyou(objSeikyuuDateFrom,objSeikyuuDateTo,"�����N����")){return false};
            
            //����N���� �召�`�F�b�N
            if(!checkDaiSyou(objUriDateFrom,objUriDateTo,"����N����")){return false};

            //����N���� �召�`�F�b�N
            if(!checkDaiSyou(objDenUriDateFrom,objDenUriDateTo,"�`�[����N����")){return false};

            if(varAction == "0"){
                //�\�������u500���v�`�F�b�N
                if(objMaxSearchCount.value == "500"){
                    if(!confirm(("<%= Messages.MSG007C %>").replace("������","500��"))){return false};
                }
                //�������s
                objSearch.click();
                
            }else if(varAction == "1"){
                    objCsvOutPutFlg.value = "1"
                    //�������s
                    objSearch.click();
            }
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
        
        /***********************************
         * �����N�����ύX���[�_����ʌďo �� �ďo�㏈��
         ***********************************/
        function openModalSeikyuuDate(strUrl, strUniqueNo, strSeikyuuDate){
            var wx = 260;
            var wy = 160;
            var x = (screen.width  - wx) / 2;
            var y = (screen.height - wy) / 2;
            var retVal;
            retVal =window.showModalDialog(strUrl + "?DenUnqNo=" + strUniqueNo
                                                  + "&SeikyuuDate=" + strSeikyuuDate
                                               ,window 
                                               ,'dialogLeft:' + x + ';dialogTop:' + y + ';dialogWidth:260px;dialogHeight:160px;menubar:no;toolbar:no;location:no;status:no;resizable:yes;scrollbars:yes;');
            if (retVal != undefined){
                objEBI("<%= ButtonModalRefresh.clientID %>").click();
            }
        }
        
        /***********************************
         * �`�[����N�����ύX���[�_����ʌďo �� �ďo�㏈��
         ***********************************/
         function openModalDenUriDate(strUrl, strUniqueNo, strDenUriDate){
            var wx = 300;
            var wy = 160;
            var x = (screen.width  - wx) / 2;
            var y = (screen.height - wy) / 2;
            var retVal;
            retVal =window.showModalDialog(strUrl + "?DenUnqNo=" + strUniqueNo
                                                  + "&DenUriDate=" + strDenUriDate
                                               ,window 
                                               ,'dialogLeft:' + x + ';dialogTop:' + y + ';dialogWidth:300px;dialogHeight:160px;menubar:no;toolbar:no;location:no;status:no;resizable:yes;scrollbars:yes;');
            if (retVal != undefined){
                objEBI("<%= ButtonModalRefresh.clientID %>").click();
            }
         }
        
        /***********************************
         * �������s����
         ***********************************/
        function exeSearch(){
            objSearch = objEBI("<%= search.clientID %>");
            //��ʃO���C�A�E�g
            setWindowOverlay(objSearch);
            objSearch.click();
        }
        
        /**
        * �ۏ؏�NO To�����Z�b�g
        * @return true/false
        */
        function setHosyouNoTo(obj){
            if(obj.id == objBangouFrom.id && objBangouTo.value == ""){
                objBangouTo.value = obj.value;
                objBangouTo.select();
            }
            return true;
        }
        
        /*********************
         * �����X���N���A
         *********************/
        function clrKameitenInfo(obj){
            if(obj.value == ""){
                //�l�̃N���A
                objEBI("<%= TextKameitenMei.clientID %>").value = "";
                objEBI("<%= TextKameiTorikesiRiyuu.clientID %>").value = "";
            }
        }
        
        /*********************
         * ��������N���A
         *********************/
        function clrSeikyuuInfo(obj){
            if(obj.value == ""){
                //�l�̃N���A
                objEBI("<%= TextSeikyuuSakiMei.clientID %>").value = "";
                objEBI("<%= TextSeikyuuKameiTorikesiRiyuu.clientID %>").value = "";
                //�F���f�t�H���g��
                objEBI("<%= TextSeikyuuSakiCd.clientID %>").style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>"
                objEBI("<%= TextSeikyuuSakiBrc.clientID %>").style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>"
                objEBI("<%= SelectSeikyuuKbn.clientID %>").style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>"
            }
        }


    </script>

    <asp:UpdatePanel ID="UpdPnlModalRefresh" UpdateMode="Conditional" runat="server"
        RenderMode="Inline">
        <ContentTemplate>
            <asp:Button ID="ButtonModalRefresh" runat="server" Text="���[�_�������ナ�t���b�V��" OnClick="ButtonModalRefresh_Click"
                Style="display: none;" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--CSV�o�͔��f--%>
    <asp:HiddenField ID="HiddenCsvOutPut" runat="server" />
    <asp:HiddenField ID="HiddenCsvMaxCnt" runat="server" />
    <%--CSV�o�͏�������t���O--%>
    <%-- �ǂ̏��i��\�����i��\���j --%>
    <asp:HiddenField ID="HiddenTargetId" runat="server" />
    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tbody>
            <tr>
                <th>
                    ����`�[�Ɖ�</th>
                <th style="text-align: right;">
                </th>
            </tr>
        </tbody>
    </table>
    <br />
    <table style="text-align: left;" class="mainTable" cellpadding="2">
        <thead>
            <tr>
                <th class="tableTitle" colspan="7" rowspan="1">
                    ��������
                    <input id="btnClearWin" value="�N���A" type="reset" class="button" tabindex="10" /></th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td class="hissu" style="font-weight: bold">
                    �敪</td>
                <td colspan="2" class="hissu">
                    <asp:DropDownList ID="selectKbn" runat="server" TabIndex="10">
                    </asp:DropDownList>
                    &nbsp;�S�敪<input id="kubun_all" type="checkbox" runat="server" tabindex="10" />
                    <input type="hidden" id="kubunVal" runat="server" /></td>
                <td class="koumokuMei">
                    �ԍ�</td>
                <td class="codeNumber" colspan="3">
                    <input id="TextBangouFrom" runat="server" maxlength="10" style="width: 72px;" class="codeNumber"
                        onblur="checkNumber(this);" onchange="if(checkNumber(this))setHosyouNoTo(this);"
                        tabindex="10" />&nbsp;�`&nbsp;<input id="TextBangouTo" runat="server" maxlength="10"
                        style="width: 72px;" class="codeNumber" onblur="checkNumber(this);" tabindex="10" /></td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    �`�[�ԍ�</td>
                <td colspan="2" class="codeNumber">
                    <input id="TextDenpyouBangouFrom" runat="server" maxlength="5" style="width: 40px;"
                        class="codeNumber" tabindex="10" />&nbsp;�`&nbsp;<input id="TextDenpyouBangouTo" runat="server"
                            maxlength="5" style="width: 40px;" class="codeNumber" tabindex="10" /></td>
                <td class="koumokuMei">
                    �`�[�쐬��</td>
                <td colspan="3" class="date">
                    <input id="TextAddDateFrom" runat="server" maxlength="10" class="date" onblur="checkDate(this);"
                        tabindex="10" />&nbsp;�`&nbsp;<input id="TextAddDateTo" runat="server" maxlength="10"
                            class="date" onblur="checkDate(this);" tabindex="10" /></td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    ���i</td>
                <td colspan="2">
                    <asp:UpdatePanel ID="UpdatePanel_syouhin" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <ContentTemplate>
                            <input id="TextSyouhinCd" runat="server" maxlength="8" style="width: 60px;" class="codeNumber"
                                tabindex="10" />
                            <input id="btnSyouhinSearch" runat="server" type="button" value="����" class="gyoumuSearchBtn"
                                tabindex="10" />&nbsp;
                            <input id="TextHinmei" runat="server" class="readOnlyStyle" readonly="readonly" style="width: 155px"
                                tabindex="-1" />&nbsp;
                            <input type="hidden" id="hdnSyouhinType" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="koumokuMei">
                    ������</td>
                <td colspan="3" class="codeNumber">
                    <asp:UpdatePanel ID="UpdatePanel_seikyuu" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <ContentTemplate>
                            <asp:DropDownList ID="SelectSeikyuuKbn" runat="server" TabIndex="10">
                            </asp:DropDownList>
                            <input id="TextSeikyuuSakiCd" runat="server" maxlength="5" style="width: 35px;" class="codeNumber"
                                tabindex="10" />&nbsp;-
                            <input id="TextSeikyuuSakiBrc" runat="server" maxlength="2" style="width: 15px;"
                                class="codeNumber" tabindex="10" />
                            <input id="btnSeikyuuSakiSearch" runat="server" type="button" value="����" class="gyoumuSearchBtn"
                                tabindex="10" />&nbsp;
                            <input id="TextSeikyuuSakiMei" runat="server" class="readOnlyStyle" readonly="readonly"
                                style="width: 155px" tabindex="-1" />&nbsp;
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    �����X</td>
                <td colspan="2">
                    <asp:UpdatePanel ID="UpdatePanel_kameiten" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <ContentTemplate>
                            <input id="TextKameitenCd" runat="server" maxlength="8" style="width: 60px;" class="codeNumber"
                                tabindex="10" />
                            <input id="btnKameitenSearch" runat="server" type="button" value="����" class="gyoumuSearchBtn"
                                onserverclick="btnKameitenSearch_ServerClick" tabindex="10" />&nbsp;
                            <input id="TextKameitenMei" runat="server" class="readOnlyStyle" readonly="readonly"
                                style="width: 155px" tabindex="-1" />&nbsp;
                            <input type="hidden" id="Hidden1" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="koumokuMei">
                    �����於�J�i</td>
                <td colspan="3" class="codeNumber">
                    <input id="TextSeikyuuSakiMeiKana" runat="server" maxlength="40" style="ime-mode: active;
                        width: 340px;" tabindex="10" />
                </td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    �����X���</td>
                <td colspan="2">
                    <asp:UpdatePanel ID="UpdatePanel_KameiToikesi" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnKameitenSearch" />
                        </Triggers>
                        <ContentTemplate>
                            <input id="TextKameiTorikesiRiyuu" runat="server" style="width: 100px;" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td style="font-size: 14px;" class="koumokuMei">
                    ���������X���</td>
                <td colspan="3">
                    <asp:UpdatePanel ID="UpdatePanel_SeikyuusakiToikesi" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSeikyuuSakiSearch" />
                        </Triggers>
                        <ContentTemplate>
                            <input id="TextSeikyuuKameiTorikesiRiyuu" runat="server" style="width: 100px;" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    �`�[����N����</td>
                <td class="date">
                    <input id="TextDenpyouUriageDateFrom" runat="server" maxlength="10" class="date"
                        onblur="checkDate(this);" tabindex="10" />&nbsp;�`&nbsp;<input id="TextDenpyouUriageDateTo"
                            runat="server" maxlength="10" class="date" onblur="checkDate(this);" tabindex="10" /></td>
                <td class="koumokuMei">
                    �����N����</td>
                <td class="date" style="border-right-style: none;">
                    <input id="TextSeikyuuDateFrom" runat="server" maxlength="10" class="date" onblur="checkDate(this);"
                        tabindex="10" />&nbsp;&nbsp;�`&nbsp;</td>
                <td class="date" style="border-left-style: none;">
                    <input id="TextSeikyuuDateTo" runat="server" maxlength="10"
                            class="date" onblur="checkDate(this);" tabindex="10" /></td>
                <td class="koumokuMei">
                    ����N����</td>
                <td class="date">
                    <input id="TextUriageDateFrom" runat="server" maxlength="10" class="date" onblur="checkDate(this);"
                        tabindex="10" />&nbsp;�`&nbsp;<input id="TextUriageDateTo" runat="server" maxlength="10"
                            class="date" onblur="checkDate(this);" tabindex="10" /></td>
            </tr>
            <tr>
                <td style="text-align: center;" colspan="7" rowspan="1">
                    �����������<select id="maxSearchCount" runat="server" tabindex="10">
                        <option value="10">10��</option>
                        <option value="100" selected="selected">100��</option>
                        <option value="500">500��</option>
                    </select>
                    <input type="button" id="btnSearch" value="�������s" runat="server" style="padding-top: 2px;"
                        tabindex="10" />
                    <input type="button" id="search" value="�������sbtn" style="display: none" runat="server"
                        tabindex="-1" />
                    <input type="button" id="ButtonCsv" value="CSV�o��" runat="server" tabindex="10" />
                    <input type="button" id="ButtonHiddenCsv" value="CSV���sbtn" style="display: none"
                        runat="server" tabindex="-1" />
                    <input id="CheckSaisinDenpyou" value="0" type="checkbox" runat="server" checked="checked"
                        tabindex="10" />�ŐV�`�[�̂ݕ\��&nbsp;
                    <input id="CheckKeijyouFlg" value="0" type="checkbox" runat="server" checked="checked"
                        tabindex="10" />�v��ς݂̂ݑΏ�&nbsp;
                    <input id="CheckMinusDenpyou" value="0" type="checkbox" runat="server" tabindex="10" />�}�C�i�X�`�[�̂ݕ\��&nbsp;
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
                                    <th style="width: 63px;">
                                        SEQ NO</th>
                                    <th style="width: 62px;">
                                        �`�[���</th>
                                    <th style="width: 62px;">
                                        �`�[�ԍ�</th>
                                    <th style="width: 85px;">
                                        ������R�[�h</th>
                                    <th style="width: 140px;">
                                        �����於</th>
                                    <th style="width: 36px;">
                                        �敪</th>
                                    <th style="width: 70px;">
                                        �ԍ�</th>
                                    <th style="width: 140px;">
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
                                    <th style="width: 66px;">
                                        ���i�R�[�h</th>
                                    <th style="width: 140px;">
                                        �i��</th>
                                    <th style="width: 62px;">
                                        ������z</th>
                                    <th style="width: 36px;">
                                        ����</th>
                                    <th style="width: 75px;">
                                        ����N����</th>
                                    <th style="width: 101px;">
                                        �`�[����N����</th>
                                    <th style="width: 67px;">
                                        ���㏈��</th>
                                    <th style="width: 75px;">
                                        �����N����</th>
                                    <th style="width: 80px;">
                                        �����X�R�[�h</th>
                                    <th style="width: 140px;">
                                        �����X��</th>
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
</asp:Content>

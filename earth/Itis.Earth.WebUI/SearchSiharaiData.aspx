<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="SearchSiharaiData.aspx.vb" Inherits="Itis.Earth.WebUI.SearchSiharaiData"
    Title="EARTH �x���`�[�Ɖ�" %>

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
        
        /*====================================
         *�O���[�o���ϐ��錾�i��ʕ��i�j
         ====================================*/
             
        //��ʕ\�����i
        var objShriDateFrom = null;
        var objShriDataTo = null;
        var objDenNoFrom = null;
        var objDenNoTo = null;
        var objTysKaisyaCd = null;       
        var objSkkJigyousyoCd = null;
        var objSkkShriSakiCd = null;
        var objSaisinDenpyou = null;
        //�������s�p
        var objMaxSearchCount = null;
        var objSearch = null;
        var objCsv = null;

        /*************************************
         * onload���̒ǉ�����
         *************************************/
        function funcAfterOnload() {
            //��ʕ\�����i�Z�b�g
            setGlobalObj();
            
            //�������ʃe�[�u�� �\�[�g�ݒ�
            sortables_init(); 

            //�������ʃe�[�u�� �e�탌�C�A�E�g�ݒ�
            settingResultTable();
                         
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
        function returnSelectValue(){
            return false;
        }
        
       /*********************************************
        * �l���`�F�b�N���A�Ώۂ��N���A����
        *********************************************/
        function clrName(obj,targetId){
            if(obj.value == "") objEBI(targetId).value="";
        }
        
       /*============================================
        * ��ʕ\�����i�I�u�W�F�N�g���O���[�o���ϐ���
        ============================================*/
        function setGlobalObj() {
            //��ʕ\�����i
            objShriDateFrom = objEBI("<%= TextShriDateFrom.clientID %>");
            objShriDataTo = objEBI("<%= TextShriDateTo.clientID %>");
            objDenNoFrom = objEBI("<%= TextDenNoFrom.clientID %>");
            objDenNoTo = objEBI("<%= TextDenNoTo.clientID %>");
            objTysKaisyaCd = objEBI("<%= TextTysKaisyaCd.clientID %>");
            objSkkJigyousyoCd = objEBI("<%= TextSkkJigyousyoCd.clientID %>");
            objSkkShriSakiCd = objEBI("<%= TextSkkShriSakiCd.clientID %>");
            objSaisinDenpyou = objEBI("<%= CheckSaisinDenpyou.clientID %>");
            //�������s�p
            objMaxSearchCount = objEBI("<%= maxSearchCount.clientID %>");
            objSearch = objEBI("<%= search.clientID %>");
            // CSV�o�͗p
            objCsv = objEBI("<%= BtnHiddenCsv.clientID %>");
            objCsvOutPutFlg = objEBI("<%= HiddenCsvOutPut.clientID %>");
        }

        /**************************************
         * �������ʃe�[�u�� �e�탌�C�A�E�g�ݒ�
         **************************************/
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
            var adjustHeight = 40;                                          // ��������(�傫�����A�������ʃe�[�u�����Ⴍ�Ȃ�)
            var adjustWidth = 539;                                          // ������(�傫�����A�������ʃe�[�u���������Ȃ�)

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
            objSaisinDenpyou.click();
            objMaxSearchCount.selectedIndex = 1;
            objShriDateFrom.focus();
        }


        /***********************************
         * �u�������s�v�������̃`�F�b�N����
         ***********************************/
        function checkJikkou(varAction){
            var varErrMsg = '';

            //�x���N�����@�K�{�`�F�b�N
            if(objShriDateFrom.value == "" && objShriDataTo.value == ""){
                varErrMsg = "<%= Messages.MSG013E %>";
                varErrMsg = varErrMsg.replace("@PARAM1","�x���N����From�A�x���N����To");
                alert(varErrMsg);
                objShriDateFrom.focus();
                return false;
                
            }else if(objShriDateFrom.value == "" || objShriDataTo.value == ""){
                if(objShriDateFrom.value == ""){
                    varErrMsg = "<%= Messages.MSG013E %>";
                    varErrMsg = varErrMsg.replace("@PARAM1","�x���N����From");
                    alert(varErrMsg);
                    objShriDateFrom.focus();
                    return false;
                }
                if(objShriDataTo.value == ""){
                    varErrMsg = "<%= Messages.MSG013E %>";
                    varErrMsg = varErrMsg.replace("@PARAM1","�x���N����To");
                    alert(varErrMsg);
                    objShriDataTo.focus();
                    return false;
                }
            }            

            
            //�x���N���� �召�`�F�b�N
            if(!checkDaiSyou(objShriDateFrom,objShriDataTo,"�x���N����")){return false};
            
            //�`�[�ԍ� �召�`�F�b�N
            if(!checkDaiSyou(objDenNoFrom,objDenNoTo,"�`�[�ԍ�")){return false};
            
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
        
        /*********************
         * �X�N���[������
         * @return 
         *********************/
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
    <%--CSV�o�͏�������t���O--%>
    <asp:HiddenField id="HiddenCsvMaxCnt" runat="server" />
 
    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tbody>
            <tr>
                <th>
                    �x���`�[�Ɖ�</th>
                <th style="text-align: right;">
                </th>
            </tr>
        </tbody>
    </table>
    <br />
    <table style="text-align: left;" class="mainTable" cellpadding="2">
        <thead>
            <tr>
                <th class="tableTitle" colspan="9" rowspan="1">
                    ��������
                    <input id="btnClearWin" value="�N���A" type="reset" class="button" tabindex="10" /></th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td class="koumokuMei">
                    �x���N����</td>
                <td colspan="2">
                    <input id="TextShriDateFrom" runat="server" maxlength="10" class="date hissu" tabindex="10" />&nbsp;�`
                    <input id="TextShriDateTo" runat="server" maxlength="10" class="date hissu"  tabindex="10" /></td>
                <td class="koumokuMei">
                    �������</td>
                <td colspan="5" class="codeNumber">
                    <asp:UpdatePanel ID="UpdatePanel_TysKaisya" UpdateMode="Conditional" runat="server" RenderMode="Inline">
                        <ContentTemplate>
                            <input id="TextTysKaisyaCd" runat="server" maxlength="7" style="width: 60px;" class="codeNumber"
                                tabindex="10" />
                            <input id="BtnTysKaisyaSearch" runat="server" type="button" value="����" class="gyoumuSearchBtn"
                                tabindex="10" />&nbsp;
                            <input id="TextTysKaisyaMei" runat="server" class="readOnlyStyle" style="width: 240px"
                                readonly="readOnly" tabindex="-1" />
                            <input type="hidden" id="HiddenTyskaisyaCd" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    �`�[�ԍ�</td>
                <td colspan="2">
                    <input id="TextDenNoFrom" runat="server" maxlength="5" style="width: 72px;" class="codeNumber"
                        tabindex="10" />&nbsp;�`&nbsp;<input id="TextDenNoTo" runat="server" maxlength="5"
                        style="width: 72px;" class="codeNumber" tabindex="10" />
                </td>
                <td class="koumokuMei">
                    �V��v�x����</td>
                <td colspan="5" class="codeNumber">
                    <asp:UpdatePanel ID="UpdatePanel_SkkShriSaki" UpdateMode="Conditional" runat="server" RenderMode="Inline">
                        <ContentTemplate>
                            <input id="TextSkkJigyousyoCd" runat="server" maxlength="10" style="width: 80px;" class="codeNumber"
                                tabindex="10" />&nbsp;-
                            <input id="TextSkkShriSakiCd" runat="server" maxlength="10" style="width: 80px;" class="codeNumber"
                                tabindex="10" />
                            <input type="hidden" id="HiddenSkkShriSakiCd" runat="server" />
                            <input id="BtnSkkShriSakiSearch" runat="server"  type="button" value="����" class="gyoumuSearchBtn"
                                tabindex="10" />&nbsp;
                            <input id="TextShriSakiMei" runat="server" class="readOnlyStyle" readonly="readonly"
                                style="width: 260px" tabindex="-1" />&nbsp;
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>               
            </tr>
            <tr>
                <td style="text-align: center;" colspan="9" rowspan="1">
                    �����������
                    <select id="maxSearchCount" runat="server" tabindex="10">
                        <option value="10">10��</option>
                        <option value="100" selected="selected">100��</option>
                        <option value="500">500��</option>
                    </select>
                    <input id="BtnSearch" value="�������s" type="button" runat="server" tabindex="10" />
                    <input type="button" id="search" value="�������sbtn" style="display: none" runat="server" tabindex="-1" />
                    <input id="BtnCsv" value="CSV�o��" type="button" runat="server" tabindex="10" />
                    <input type="button" id="BtnHiddenCsv" value="CSV���sbtn" style="display: none" runat="server"
                        tabindex="-1" />
                    <input id="CheckSaisinDenpyou" value="0" type="checkbox" runat="server" checked="checked"
                        tabindex="10" />�ŐV�`�[�̂ݕ\��
                </td>
            </tr>
        </tbody>
    </table>
    <input type="hidden" id="returnTargetIds" runat="server" />
    <input type="hidden" id="afterEventBtnId" runat="server" />
    <table style="height:30px;">
        <tr>
            <td>
                �������ʁF</td>
            <td id="resultCount" runat="server">
            </td>
            <td>
                ��</td>
            <td style="width:10px">
            </td>
            <td>
                �x�����v�F \
            </td>
            <td id='TdTotalKingaku' runat='server'>
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0">
        <!-- �w�b�_�[�� -->   
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
                                    <th style="width: 63px;">
                                        �`�[�ԍ�</th>
                                    <th style="width: 95px;">
                                        ������ЃR�[�h</th>
                                    <th style="width: 85px;">
                                        �V��v�R�[�h</th>
                                    <th style="width: 200px;">
                                        �x���於</th>
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
                                    <th style="width: 70px;">
                                        �U���z</th>
                                    <th style="width: 70px;">
                                        ���E�z</th>
                                    <th style="width: 80px;">
                                        �x���N����</th>
                                    <th style="width: 650px;">
                                        �E�v</th>                                   
                                </tr>
                            </thead>
                        </table>
                    </div>
                </th>
            </tr>
        </thead>
        <!-- �f�[�^�� -->
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
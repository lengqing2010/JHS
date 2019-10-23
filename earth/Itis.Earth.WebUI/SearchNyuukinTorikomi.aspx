<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="SearchNyuukinTorikomi.aspx.vb" Inherits="Itis.Earth.WebUI.SearchNyuukinTorikomi"
    Title="EARTH �����捞�f�[�^�Ɖ�" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script src="js/sortable_ja.js" type="text/javascript">
    </script>

    <script type="text/javascript">
        //��ʋN�����ɃE�B���h�E�T�C�Y���f�B�X�v���C�ɍ��킹��
        window.resizeTo(1024,768);
        
        _d = document;
        /*====================================
         *�O���[�o���ϐ��錾�i��ʕ��i�j
         ====================================*/
         //�R���g���[���ړ���
        var gVarOyaSettouji = "ctl00_CPH1_";
        var gVarTr = "resultTr_";
        
        //��ʑJ�ڗp
        var objSendBtn = null;
        var objSendTargetWin = null;
        
        var objSelectedTr = null;
        var objSendVal_NyuukinNo = null;
        
        var varAction = null;

        //��ʕ\�����i
        var objNkTorikomiNoFrom = null;
        var objNkTorikomiNoTo = null;
        var objTorikomiDenpyouNoFrom = null;
        var objTorikomiDenpyouNoTo = null;
        var objNyuukinDateFrom = null;
        var objNyuukinDateTo = null;
        var objChkTorikesi = null;
        //�������s�p
        var objSearch = null;
        var objMaxSearchCount = null;
        
        /*************************************
         * onload���̒ǉ�����
         *************************************/
        function funcAfterOnload() {
            //��ʕ\�����i�Z�b�g
            setGlobalObj();
            
            /*�������ʃe�[�u�� �\�[�g�ݒ�*/
            sortables_init();   
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
            objNkTorikomiNoFrom = objEBI("<%= TextNyuukinNoFrom.clientID %>");
            objNkTorikomiNoTo = objEBI("<%= TextNyuukinNoTo.clientID %>");
            objTorikomiDenpyouNoFrom = objEBI("<%= TextTorikomiDenpyouNoFrom.clientID %>");
            objTorikomiDenpyouNoTo = objEBI("<%= TextTorikomiDenpyouNoTo.clientID %>");
            objNyuukinDateFrom = objEBI("<%= TextNyuukinDateFrom.clientID %>");
            objNyuukinDateTo = objEBI("<%= TextNyuukinDateTo.clientID %>");
            objSearch = objEBI("<%= ButtonHiddenSearch.clientID %>");
            objMaxSearchCount = objEBI("<%= maxSearchCount.clientID %>");
            objChkTorikesi = objEBI("<%= CheckBoxTorikesiTaisyou.clientID %>");
        }
        
        /**
         * ���׍s���_�u���N���b�N�����ۂ̏���
         * @param objSelectedTr
         * @param intGamen[1:�����捞�C��]
         * @return
         */
        function returnSelectValue(objSelectedTr) {
            var varTrId = objSelectedTr.id;
            var varRow = varTrId.replace(gVarOyaSettouji + gVarTr,"");
            var varHdn = _d.getElementById(gVarOyaSettouji + "HiddenNkTorikomiNo_" + varRow);
           
            PopupSyuusei(varHdn.value);
        }
        
        //�q��ʌďo����
        function PopupSyuusei(strUniqueNo){    
            //�I�u�W�F�N�g�̍ēǍ�(Ajax�ĕ`��Ή�)
            objSendTargetWin = objEBI("<%= sendTargetWin.clientID %>");
            if(objSendTargetWin.value == "")objSendTargetWin.value="<%=EarthConst.MAIN_WINDOW_NAME %>";
            
            //�I�[�v���Ώۂ̉�ʂ��w��
            varAction = "<%=UrlConst.NYUUKIN_TORIKOMI_SYUUSEI %>";
            
            //<!-- ��ʈ��n����� -->
            objSendForm = objEBI("searchForm");
            //�敪+�ԍ�+����NO
            var objSendVal_SearchTerms = objEBI("sendSearchTerms");
            objSendVal_SearchTerms.value = strUniqueNo;
                        
            var varWindowName = "NyuukinTorikomiSyuusei";
            objSrchWin = window.open("about:Blank", varWindowName, "menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes");
                        
            //�l�Z�b�g
            objSendTargetWin.value = varWindowName;
            objSendForm.target=objSendTargetWin.value;
                            
            objSendForm.action = varAction;
            objSendForm.submit();
        }
        
        /***********************************
         * �u�������s�v�������̃`�F�b�N����
         ***********************************/
        function checkJikkou(){
           
            //�����捞NO �召�`�F�b�N
            if(!checkDaiSyou(objNkTorikomiNoFrom,objNkTorikomiNoTo,"�����捞NO"))return false;
            
            //�����捞NO_FROM �ő�l�`�F�b�N
            if(Number(removeFigure(objNkTorikomiNoFrom.value)) > 2147483647){
                alert("<%= Messages.MSG154E %>");
                objNkTorikomiNoFrom.focus();
                return false;
            }
            //�����捞NO_TO �ő�l�`�F�b�N
            if(Number(removeFigure(objNkTorikomiNoTo.value)) > 2147483647){
                alert("<%= Messages.MSG154E %>");
                objNkTorikomiNoTo.focus();
                return false;
            }
            
            //�捞�`�[�ԍ� �召�`�F�b�N
            if(!checkDaiSyou(objTorikomiDenpyouNoFrom,objTorikomiDenpyouNoTo,"�捞�`�[�ԍ�"))return false;
            
            //������ �召�`�F�b�N
            if(!checkDaiSyou(objNyuukinDateFrom,objNyuukinDateTo,"������"))return false;
            
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

        /*******************************************
         * All�N���A������Ɏ��s�����t�@���N�V����
         *******************************************/
        function funcAfterAllClear(obj){
            objMaxSearchCount.selectedIndex = 1;
            objChkTorikesi.checked = true;
        }
        
        /*********************
         * ��������N���A
         *********************/
        function clrSeikyuuInfo(obj){
            if(obj.value == ""){
                //�l�̃N���A
                objEBI("<%= TextSeikyuuSakiMei.clientID %>").value = "";
                objEBI("<%= TextTorikesiRiyuu.clientID %>").value = "";
                //�F���f�t�H���g��
                objEBI("<%= TextSeikyuuSakiCd.clientID %>").style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>"
                objEBI("<%= TextSeikyuuSakiBrc.clientID %>").style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>"
                objEBI("<%= SelectSeikyuuKbn.clientID %>").style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>"
            }
        }
        
    </script>

    <asp:ScriptManagerProxy ID="SMP1" runat="server">
    </asp:ScriptManagerProxy>
    <div>
        <%-- ��ʃ^�C�g�� --%>
        <table>
            <tr>
                <td>
                    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
                        class="titleTable">
                        <tr>
                            <th style="text-align: left; width: 160px;">
                                �����捞�f�[�^�Ɖ�</th>
                            <th style="width: 80px">
                                <input type="button" id="ButtonSinki" value="�V�K�o�^" style="width: 80px;" runat="server"
                                    onclick="PopupSyuusei(0)" />
                            </th>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <%-- ��ʏ㕔[��������] --%>
        <table cellpadding="0" cellspacing="0" style="border-bottom: solid 2px gray; border-left: solid 2px gray;"
            class="mainTable">
            <thead>
                <tr>
                    <th class="tableTitle" colspan="6" rowspan="1">
                        ��������
                        <input id="btnClearWin" value="�N���A" type="reset" class="button" /></th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td class="koumokuMei" style="width: 100px">
                        �����捞NO</td>
                    <td>
                        <input id="TextNyuukinNoFrom" runat="server" style="width: 70px;" class="number"
                            maxlength="10" />�`<input id="TextNyuukinNoTo" runat="server" style="width: 70px;"
                                class="number" maxlength="10" />
                    </td>
                    <td class="koumokuMei">
                        �捞�`�[�ԍ�</td>
                    <td colspan="3">
                        <input id="TextTorikomiDenpyouNoFrom" runat="server" maxlength="6" style="width: 70px;"
                            class="codeNumber" />�`<input id="TextTorikomiDenpyouNoTo" runat="server" maxlength="6"
                                style="width: 70px;" class="codeNumber" />
                    </td>
                </tr>
                <tr>
                    <td class="koumokuMei">
                        ������</td>
                    <td>
                        <input id="TextNyuukinDateFrom" runat="server" maxlength="10" class="date" />�`<input
                            id="TextNyuukinDateTo" runat="server" maxlength="10" class="date" /></td>
                    <td class="koumokuMei">
                        EDI���쐬��
                    </td>
                    <td style="border-right-style: none;">
                        <input id="TextEdiJouhouSakuseiDate" runat="server" class="" style="width: 280px"
                            maxlength="40" />
                    </td>
                </tr>
                <tr>
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
                                style="width: 250px" tabindex="-1" />&nbsp;
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    </td>
                    <td class="koumokuMei">
                        ���</td>
                    <td>
                    <asp:UpdatePanel ID="UpdatePanel_SeikyuusakiToikesi" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSeikyuuSakiSearch" />
                        </Triggers>
                        <ContentTemplate>
                            <input id="TextTorikesiRiyuu" runat="server" style="width: 80px;" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    </td>
                </tr>
                <tr class="tableSpacer">
                    <td colspan="6">
                    </td>
                </tr>
                <tr>
                    <td colspan="6" rowspan="1" style="text-align: center">
                        <img src="images/spacer.gif" alt="" style="width: 30px; height: 0px;" />
                        �����������
                        <select id="maxSearchCount" runat="server">
                            <option value="10">10��</option>
                            <option value="100" selected="selected">100��</option>
                            <option value="max">������</option>
                        </select>
                        <input type="button" id="ButtonSearch" value="�������s" runat="server" onclick="checkJikkou()" />
                        <input type="button" id="ButtonHiddenSearch" value="�������sbtn" style="display: none"
                            runat="server" />
                        <img src="images/spacer.gif" alt="" style="width: 30px; height: 0px;" />
                        <input id="CheckBoxTorikesiTaisyou" value="0" type="checkbox" runat="server" checked="checked" />����͌����ΏۊO
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
                <td style="width: 10px">
                </td>
                <td>
                    �����v���z�F
                </td>
                <td id="TdTotalKingaku" runat="server">
                </td>
            </tr>
        </table>
        <%-- ��ʏ㕔[�����捞�f�[�^���] --%>
        <div class="dataGridHeader" id="dataGridContent" style="width: 870px">
            <table class="scrolltablestyle2 sortableTitle" id="meisaiTable" cellpadding="0" cellspacing="0">
                <%-- �w�b�_�� --%>
                <thead>
                    <tr id="meisaiTableHeaderTr" runat="server" style="position: relative; top: expression(this.offsetParent.scrollTop)">
                        <th style="width: 64px">
                            �����捞NO</th>
                        <th style="width: 100px">
                            EDI���쐬��</th>
                        <th style="width: 30px">
                            ���</th>
                        <th style="width: 90px">
                            ������</th>
                        <th style="width: 80px">
                            �捞�`�[�ԍ�</th>
                        <th style="width: 80px">
                            ������R�[�h</th>
                        <th style="width: 160px">
                            �����於</th>
                        <th style="width: 80px">
                            �ƍ�����No.</th>
                        <th style="width: 80px">
                            �`�[���z���v</th>
                    </tr>
                </thead>
                <!-- �f�[�^�� -->
                <tbody id="searchGrid" runat="server">
                </tbody>
            </table>
        </div>
    </div>
    <input type="hidden" id="sendTargetWin" runat="server" />
</asp:Content>

<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="Houkokusyo.aspx.vb" Inherits="Itis.Earth.WebUI.Houkokusyo" Title="EARTH �񍐏�" %>

<%@ Register Src="control/GyoumuKyoutuuCtrl.ascx" TagName="GyoumuKyoutuuCtrl" TagPrefix="uc1" %>
<%@ Register Src="control/SeikyuuSiireLinkCtrl.ascx" TagName="SeikyuuSiireLinkCtrl" TagPrefix="uc2" %>
<%@ Import Namespace="Itis.Earth.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script type="text/javascript">
        //��ʋN�����ɃE�B���h�E�T�C�Y���f�B�X�v���C�ɍ��킹��
        window.moveTo(0, 0);
        window.resizeTo(window.screen.availWidth, window.screen.availHeight);
        
        //�ύX�O�R���g���[���̒l��ޔ����āA�Y���R���g���[��(Hidden)�ɕێ�����
        function SetChangeMaeValue(strTaihiID, strTargetID){
           document.getElementById(strTaihiID).value = document.getElementById(strTargetID).value;
        }

        //����1�R�[�h�����������Ăяo��
        function callHantei1Search(obj){
            objEBI("<%= HiddenHanteiSearchType1.clientID %>").value = "";
            if(obj.value == ""){
                objEBI("<%= HiddenHanteiSearchType1.clientID %>").value = "1";
                objEBI("<%= ButtonHantei1.clientID %>").click();
            }
        }
        
        //����2�R�[�h�����������Ăяo��
        function callHantei2Search(obj){
            objEBI("<%= HiddenHanteiSearchType2.clientID %>").value = "";
            if(obj.value == ""){
                objEBI("<%= HiddenHanteiSearchType2.clientID %>").value = "1";
                objEBI("<%= ButtonHantei2.clientID %>").click();
            }
        }
       
       //����1�L�����Z������
        var tmpConfirmResultButtonId = "";
        function callHantei1Cancel(strMsg, btnId1){
            if(confirm(strMsg)){
            }else{
                objEBI("<%= HiddenHanteiSearchType1.clientID %>").value = "1";
                objEBI("<%= TextHantei1Cd.ClientID %>").value = objEBI("<%= HiddenHantei1CdOld.ClientID %>").value;
                tmpConfirmResultButtonId = btnId1;
            }
        }
        
        //�o�^�����O�`�F�b�N
        function CheckTouroku(){           
            //����ύX�`�F�b�N
            if(ChkHantei() == false) return false;
            return true;
        }
        
        //����ύX������
        //����ύX���́A����ύX���R����͂���
        function ChkHantei(){
            var blnChkFlg = true;
            
            //Old
            var strHantei1CdOld = objEBI("<%= HiddenHantei1CdOld.clientID %>").value; //����1�R�[�hOld
            var strHantei2CdOld = objEBI("<%= HiddenHantei2CdOld.clientID %>").value; //����2�R�[�hOld
            var strHanteiSetuzokuOld = objEBI("<%= HiddenHanteiSetuzokuMojiOld.clientID %>").value; //����ڑ���Old
            //���
            var strHantei1Cd = objEBI("<%= TextHantei1Cd.clientID %>").value; //����1�R�[�hOld
            var strHantei2Cd = objEBI("<%= TextHantei2Cd.clientID %>").value; //����2�R�[�hOld
            var strHanteiSetuzoku = objEBI("<%= SelectHanteiSetuzokuMoji.clientID %>").value; //����ڑ���Old
            
            var objRiyuu = objEBI("<%= HiddenHanteiHenkouRiyuu.clientID %>");
            objRiyuu.value = ''; //������
            var retRiyuu = ''; //�ԋp�l(����ύX���R)
            
            //�ύX�`�F�b�N
            if(strHantei1CdOld == ''){ //����1=������
            }else{ //����1=���͍�
                if(CompVal(strHantei1CdOld,strHantei1Cd) == false){ //����1
                    blnChkFlg = false;
                }
                if(CompVal(strHantei2CdOld,strHantei2Cd) == false){ //����2
                    blnChkFlg = false;
                }
                if(CompVal(strHanteiSetuzokuOld,strHanteiSetuzoku) == false){ //����ڑ���
                    blnChkFlg = false;
                }            
            }
            
            if(blnChkFlg == false){
                //�m�FMSG�\��
                var strMSG = "<%= Messages.MSG062E %>";
                strMSG = strMSG.replace('@PARAM1','����ύX���R');
                retRiyuu = prompt("\r\n" + strMSG,"");
                if(retRiyuu == null){ //�L�����Z���{�^��������
                    return false;
                }
                if(retRiyuu.length == 0){ //OK�{�^��������
                    strMsg = "<%= Messages.MSG013E %>";
                    strMsg = strMsg.replace('@PARAM1','����ύX���R');
                    alert(strMsg);
                    return false;
                }

                //����ύX���R��Hidden�Z�b�g
                objRiyuu.value = retRiyuu;
            }
            return true;
        }
        
        //�������Ƒ������̒l���r���A����̏ꍇTrue��Ԃ��B
        function CompVal(strValOld, strVal){
            if(strValOld == strVal){
                return true;
            }else{
                return false;
            }
        }
        
       //Ajax���s������
        Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(InitializeRequestHandler);
            function InitializeRequestHandler(sender, args){
                objEBI("<%=HiddenAjaxFlg.ClientID %>").value=1;
        }
        //Ajax���[�h�㏈��
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args){
            if(tmpConfirmResultButtonId != ""){
                objEBI(tmpConfirmResultButtonId).click();
                tmpConfirmResultButtonId = "";
            }
            objEBI("<%=HiddenAjaxFlg.ClientID %>").value=0;
        }
          
    </script>

    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <!-- ��ʏ㕔�E�w�b�_ -->
    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tbody>
            <tr>
                <th style="text-align: left; width: 150px;">
                    �񍐏�
                </th>
                <th style="text-align: left;">
                    <input type="button" id="ButtonTouroku1" runat="server" value="�o�^ / �C�� ���s" style="font-weight: bold;
                        font-size: 18px; width: 155px; color: black; height: 30px; background-color: fuchsia" />&nbsp;&nbsp;&nbsp;
                </th>
                <th style="text-align: right; font-size: 11px;">
                    �ŏI�X�V�ҁF
                    <asp:TextBox ID="TextSaisyuuKousinSya" runat="server" CssClass="readOnlyStyle" Style="width: 120px"
                        Text="" ReadOnly="true" TabIndex="-1" />
                    <br />
                    �ŏI�X�V�����F
                    <asp:TextBox ID="TextSaisyuuKousinDate" runat="server" CssClass="readOnlyStyle" Style="width: 100px"
                        Text="" ReadOnly="true" TabIndex="-1" />
                </th>
            </tr>
            <tr>
                <td colspan="2" rowspan="1" style="height: 10px">
                </td>
                <td colspan="1" rowspan="1" style="height: 10px">
                </td>
            </tr>
        </tbody>
    </table>
    <asp:UpdatePanel ID="UpdatePanelHoll" UpdateMode="conditional" runat="server"
        RenderMode="Inline">
        <ContentTemplate>
            <uc1:GyoumuKyoutuuCtrl ID="ucGyoumuKyoutuu" runat="server" DispMode="HOUKOKUSYO" />
            <br />
            <div id="divTyousaHoukokusyo" runat="server">
                <asp:UpdatePanel ID="UpdatePanelHoukokusyo" UpdateMode="always" runat="server" RenderMode="inline"
                    ChildrenAsTriggers="true">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="SelectJuri" />
                        <asp:AsyncPostBackTrigger ControlID="TextHassouDate" />
                        <asp:AsyncPostBackTrigger ControlID="TextSaihakkouDate" />
                        <asp:AsyncPostBackTrigger ControlID="ButtonHantei1" />
                        <asp:AsyncPostBackTrigger ControlID="ButtonHantei2" />
                    </Triggers>
                    <ContentTemplate>
                        <!-- hidden����-->
                        <input type="hidden" id="HiddenAjaxFlg" runat="server" /><%--Ajax�������t���O--%>
                        <input type="hidden" id="HiddenUriageKeijouDate" value="" runat="server" />
                        <input type="hidden" id="HiddenZeikomiNyuukingaku" value="" runat="server" />
                        <input type="hidden" id="HiddenZeikomiHenkinkingaku" value="" runat="server" />
                        <input type="hidden" id="HiddenHassouDateMae" value="" runat="server" /><%--�������O--%>
                        <input type="hidden" id="HiddenSaihakkouDateMae" value="" runat="server" /><%--�Ĕ��s���O--%>
                        <input type="hidden" id="HiddenTyousaKaishaJigyousyoCd" value="" runat="server" />
                        <input type="hidden" id="HiddenHantei1CdOld" runat="server" value="" /><%--����1�R�[�hOld--%>
                        <input type="hidden" id="HiddenHantei2CdOld" runat="server" value="" /><%--����2�R�[�hOld--%>
                        <input type="hidden" id="HiddenHanteiSetuzokuMojiOld" runat="server" value="" /><%--����ڑ������R�[�hOld--%>
                        <input type="hidden" id="HiddenHantei1Old" runat="server" value="" /><%--����1�R�[�h��Old--%>
                        <input type="hidden" id="HiddenHantei1CdMae" runat="server" value="" /><%--����1�R�[�h�ύX�O--%>
                        <input type="hidden" id="HiddenHantei2CdMae" runat="server" value="" /><%--����2�R�[�h�ύX�O--%>
                        <input type="hidden" id="HiddenHosyousyoHakkouDate" runat="server" value="" /><%--�ۏ؏����s��--%>
                        <input type="hidden" id="HiddenUpdateTeibetuSeikyuuDatetime" runat="server" value="" /><%--�@�ʐ����f�[�^�p�X�V����--%>
                        <input type="hidden" id="HiddenTyousaKekkaTourokuDate" runat="server" value="" /><%--�n�Ճe�[�u���E�������ʓo�^����--%>
                        <input type="hidden" id="HiddenTyousaKekkaUpdateDate" runat="server" value="" /><%--�n�Ճe�[�u���E�������ʍX�V����--%>
                        <input type="hidden" id="HiddenHanteiHenkouRiyuu" runat="server" value="" /><%--���������e�[�u���E����ύX���R--%>
                        <input type="hidden" id="HiddenKojHanteiKekkaFlgOld" runat="server" /><%--�n�Ճe�[�u���E�H�����茋��FLGOld--%>
                        <input type="hidden" id="HiddenKameitenCd" runat="server" /><%--�����X�R�[�h--%>
                        <!-- ��ʒ������E�񍐏���� -->
                        <table style="text-align: left; width: 100%;" id="TableTyousaHoukokusyo" class="mainTable"
                            cellpadding="0" cellspacing="0">
                            <!-- �w�b�_�� -->
                            <thead>
                                <tr>
                                    <th class="tableTitle" style="" colspan="6">
                                        <a id="AncTysHoukokusyo" runat="server">�����񍐏����</a>
                                        <input type="hidden" id="HiddenHoukokusyoInfoStyle" runat="server" value="inline" />
                                    </th>
                                </tr>
                            </thead>
                            <!-- �{�f�B�� -->
                            <tbody id="TBodyHoukokushoInfo" class="scrolltablestyle" runat="server">
                                <!-- 1�s�� -->
                                <tr>
                                    <td style="width: 80px" class="koumokuMei">
                                        �˗��S����
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextIraiTantousya" runat="server" CssClass="readOnlyStyle" Style="width: 100px"
                                            Text="" ReadOnly="true" TabIndex="-1" />
                                    </td>
                                    <td class="koumokuMei">
                                        �������
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextTyousaKaisyaMei" runat="server" CssClass="readOnlyStyle" Style="width: 260px"
                                            Text="" ReadOnly="true" TabIndex="-1" />
                                        <input type="hidden" id="HiddenDefaultSiireSakiCdForLink" runat="server" value="" />
                                    </td>
                                    <td class="koumokuMei">
                                        �������@
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextTyousaHouhou" runat="server" CssClass="readOnlyStyle" Style="width: 260px"
                                            Text="" ReadOnly="true" TabIndex="-1" />
                                    </td>
                                </tr>
                                <!-- 2�s�� -->
                                <tr>
                                    <td class="koumokuMei">
                                        �������{��
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextTyousaJissiDate" runat="server" CssClass="date readOnlyStyle"
                                            Text="" ReadOnly="true" TabIndex="-1" MaxLength="10" />
                                    </td>
                                    <td class="koumokuMei">
                                        �v�揑�쐬��
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="TextKeikakusyoSakuseiDate" runat="server" CssClass="date readOnlyStyle"
                                            Style="" Text="" ReadOnly="true" TabIndex="-1" MaxLength="10" />
                                    </td>
                                </tr>
                                <!-- 3�s�� -->
                                <tr>
                                    <td class="koumokuMei">
                                        ��
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="SelectJuri" runat="server" AutoPostBack="true" OnSelectedIndexChanged="SelectJuri_SelectedIndexChanged">
                                        </asp:DropDownList><span id="SpanJuri" runat="server" style="display: none;"></span>
                                    </td>
                                    <td class="koumokuMei">
                                        �󗝏ڍ�
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="TextJuriSyousai" runat="server" CssClass="" Style="width: 300px"
                                            Text="" MaxLength="40" />
                                    </td>
                                </tr>
                                <!-- 4�s�� -->
                                <tr>
                                    <td class="koumokuMei">
                                        �󗝓�
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextJuriDate" runat="server" CssClass="date" Style="" Text="" MaxLength="10" />
                                    </td>
                                    <td class="koumokuMei">
                                        ������
                                    </td>
                                    <td colspan="3" runat="server" id="TdHassouDate">
                                        <asp:TextBox ID="TextHassouDate" runat="server" CssClass="date" Style="" Text=""
                                            MaxLength="10" OnTextChanged="TextHassouDate_ServerChange" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="koumokuMei">
                                        �u���H��
                                    </td>
                                    <td colspan="5" style="padding: 0px">
                                        <table class="subTable" style="font-weight: bold;">
                                            <tr>
                                                <td>
                                                    �ʐ^�󗝁F
                                                    <asp:DropDownList ID="SelectSyasinJuri" runat="server">
                                                    </asp:DropDownList><span id="SpanSyasinJuri" runat="server" style="display: none;"></span>
                                                </td>
                                                <td>
                                                    �ʐ^�R�����g�F
                                                    <asp:TextBox ID="TextSyasinComment" runat="server" CssClass="" Style="width: 500px"
                                                        Text="" MaxLength="100" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <!-- 10�s�� -->
                                <tr>
                                    <td class="koumokuMei">
                                        �Ĕ��s��
                                    </td>
                                    <td colspan="5" runat="server" id="TdSaihakkouDate">
                                        <asp:TextBox ID="TextSaihakkouDate" runat="server" CssClass="date" Style="" Text=""
                                            MaxLength="10" OnTextChanged="TextSaihakkouDate_TextChanged" />
                                        &nbsp;&nbsp;&nbsp; <span id="SpanUriageSyorizumi" style="color: red; font-weight: bold;"
                                            runat="server"></span>
                                            <input type="hidden" id="HiddenUriageKeijyouDate" runat="server" />
                                    </td>
                                </tr>
                                <!-- 11�s�� -->
                                <tr>
                                    <td colspan="6" style="padding: 0px;">
                                        <asp:UpdatePanel ID="UpdatePanelSyouhinInfo" UpdateMode="conditional" runat="server"
                                            RenderMode="inline" ChildrenAsTriggers="true">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="TextKoumutenSeikyuuKingaku" />
                                                <asp:AsyncPostBackTrigger ControlID="TextJituseikyuuKingaku" />
                                                <asp:AsyncPostBackTrigger ControlID="TextHattyuusyoKingaku" />
                                                <asp:AsyncPostBackTrigger ControlID="SelectSeikyuuUmu" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <!-- hidden����-->
                                                <input type="hidden" id="HiddenHosyousyoHakJykyOld" runat="server" /><%--�ۏ؏����s��Old--%>
                                                <input type="hidden" id="HiddenHosyousyoHakJyky" runat="server" /><%--�ۏ؏����s��--%>
                                                <input type="hidden" id="HiddenHosyousyoHakJykySetteiDate" runat="server" /><%--�ۏ؏����s�󋵐ݒ��--%>
                                                <input type="hidden" id="HiddenZeiKbn" runat="server" /><%--�ŋ敪--%>
                                                <input type="hidden" id="HiddenZeiritu" value="0" runat="server" /><%--�ŗ�--%>
                                                <input type="hidden" id="HiddenNyuukingakuOld" runat="server" /><%--�����z--%>
                                                <input type="hidden" id="HiddenKoumutenSeikyuuKingakuMae" runat="server" /><%--�H���X�����Ŕ��������z�O--%>
                                                <input type="hidden" id="HiddenJituseikyuuKingakuMae" runat="server" /><%--�������Ŕ��������z�O--%>
                                                <input type="hidden" id="HiddenSeikyuuUmuMae" runat="server" /><%--�����L���O--%>
                                                <input type="hidden" id="HiddenJituseikyuu1Flg" runat="server" /><%--�������Ŕ����z1�t���O--%>
                                                <table id="TableSyouhin" class="itemTable innerTable" cellpadding="0" cellspacing="0">
                                                    <tr class="shouhinTableTitle firstRow">
                                                        <td class="firstCol">
                                                            ���i�R�[�h
                                                        </td>
                                                        <td class="itemMei_small" rowspan="2">
                                                            �H���X����<br />
                                                            �Ŕ����z
                                                        </td>
                                                        <td class="itemMei_small" rowspan="2">
                                                            ������<br />
                                                            �Ŕ����z
                                                        </td>
                                                        <td>
                                                            �����
                                                        </td>
                                                        <td rowspan="2">
                                                            ������<br />
                                                            ���s��
                                                        </td>
                                                        <td rowspan="2">
                                                            ����<br />
                                                            �N����
                                                        </td>
                                                        <td>
                                                            ����
                                                        </td>
                                                        <td rowspan="2">
                                                            ������<br />
                                                            ���z
                                                        </td>
                                                        <td rowspan="2">
                                                            ������<br />
                                                            �m��
                                                        </td>
                                                        <td rowspan="2">
                                                            ������<br />
                                                            �m�F��
                                                        </td>
                                                    </tr>
                                                    <!-- 12�s�� -->
                                                    <tr class="shouhinTableTitle">
                                                        <td class="firstCol">
                                                            ���i��
                                                        </td>
                                                        <td>
                                                            �ō����z
                                                        </td>
                                                        <td>
                                                            ������
                                                        </td>
                                                    </tr>
                                                    <!-- 13�s�� -->
                                                    <tr runat="server" id="TrSyouhin">
                                                        <td style="width: 200px" class="itemNm firstCol">
                                                            <asp:TextBox ID="TextItemCd" runat="server" CssClass="itemCd readOnlyStyle" Style="size: 8"
                                                                Text="" ReadOnly="true" TabIndex="-1" />
                                                            <uc2:SeikyuuSiireLinkCtrl ID="ucSeikyuuSiireLink" runat="server" />
                                                            <br />
                                                            <span id="SpanItemMei" class="itemNm" runat="server"></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextKoumutenSeikyuuKingaku" runat="server" CssClass="kingaku" MaxLength="7"
                                                                Text="" OnTextChanged="TextKoumutenSeikyuuKingaku_TextChanged" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextJituseikyuuKingaku" runat="server" CssClass="kingaku" MaxLength="7"
                                                                Text="" OnTextChanged="TextJituseikyuuKingaku_TextChanged" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextSyouhizei" runat="server" CssClass="kingaku readOnlyStyle" ReadOnly="true"
                                                                MaxLength="7" Text="" Style="size: 9" TabIndex="-1" />
                                                            <br />
                                                            <asp:TextBox ID="TextZeikomiKingaku" runat="server" CssClass="kingaku readOnlyStyle"
                                                                ReadOnly="true" MaxLength="7" Text="" Style="size: 9" TabIndex="-1" />
                                                            <asp:HiddenField ID="HiddenSiireGaku" runat="server" />
                                                            <asp:HiddenField ID="HiddenSiireSyouhiZei" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextSeikyuusyoHakkouDate" runat="server" CssClass="date" Style=""
                                                                Text="" MaxLength="10" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextUriageNengappi" runat="server" CssClass="date readOnlyStyle"
                                                                Style="" Text="" ReadOnly="true" TabIndex="-1" MaxLength="10" />
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="SelectSeikyuuUmu" runat="server" AutoPostBack="true" OnSelectedIndexChanged="SelectSeikyuuUmu_SelectedIndexChanged">
                                                                <asp:ListItem Value="" Text="" Selected="true"></asp:ListItem>
                                                                <asp:ListItem Value="1" Text="�L"></asp:ListItem>
                                                                <asp:ListItem Value="0" Text="��"></asp:ListItem>
                                                            </asp:DropDownList><span id="SpanSeikyuUmu" runat="server"></span>
                                                            <br />
                                                            <asp:TextBox ID="TextSeikyuusaki" runat="server" CssClass="readOnlyStyle" Style="width: 60px"
                                                                Text="" ReadOnly="true" TabIndex="-1" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextHattyuusyoKingaku" runat="server" CssClass="kingaku readOnlyStyle"
                                                                MaxLength="7" Text="" ReadOnly="true" TabIndex="-1" Style="width: 60px" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextHattyuusyoKakutei" runat="server" CssClass="readOnlyStyle" Style="width: 40px"
                                                                TabIndex="-1" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextHattyuusyoKakuninDate" runat="server" CssClass="date readOnlyStyle"
                                                                Style="" Text="" ReadOnly="true" TabIndex="-1" MaxLength="10" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <!-- 14�s�� -->
                                <tr>
                                    <td colspan="6" style="padding: 0px;">
                                        <table id="Table5" class="innerTable" cellpadding="0" cellspacing="0">
                                            <tr class="firstRow">
                                                <td style="width: 90px" class="koumokuMei firstCol">
                                                    �Ĕ��s���R
                                                </td>
                                                <td id="TdSaihakkouRiyuu" runat="server">
                                                    <asp:TextBox ID="TextSaihakkouRiyuu" runat="server" CssClass="" Style="width: 300px"
                                                        Text="" MaxLength="40" />
                                                </td>
                                                <td class="shouhinTableTitleNyuukin">
                                                    �����z<br />
                                                    (�ō�)
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextNyuukingaku" runat="server" CssClass="kingaku readOnlyStyle"
                                                        Style="size: 9" Text="0" ReadOnly="true" TabIndex="-1" MaxLength="7" />
                                                </td>
                                                <td class="shouhinTableTitleNyuukin">
                                                    �c�z
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextZangaku" runat="server" CssClass="kingaku readOnlyStyle" Style="size: 9"
                                                        Text="0" ReadOnly="true" TabIndex="-1" MaxLength="7" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <!-- 14�s�� -->
                                <tr>
                                    <td colspan="6" style="padding: 0px;">
                                        <table id="Table1" class="innerTable" cellpadding="0" cellspacing="0">
                                            <tr class="firstRow">
                                                <td class="koumokuMei firstCol" style="width: 90px;">
                                                    ��͒S����
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextKaisekiTantousyaCd" runat="server" CssClass="codeNumber" Style="width: 30px"
                                                        Text="" MaxLength="3" />
                                                    <asp:DropDownList ID="SelectKaisekiTantousya" runat="server">
                                                    </asp:DropDownList><span id="SpanKaisekiTantousya" runat="server" style="display: none;"></span>
                                                </td>
                                                <td class="koumokuMei">
                                                    ��͏��F��
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextKaisekiSyouninsya" runat="server" CssClass="readOnlyStyle" Style="width: 150px"
                                                        Text="" ReadOnly="true" TabIndex="-1" />
                                                </td>
                                                <td class="koumokuMei">
                                                    �H���S����
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextKoujiTantousyaCd" runat="server" CssClass="codeNumber readOnlyStyle"
                                                        Style="width: 30px" Text="" ReadOnly="true" TabIndex="-1" MaxLength="7" />
                                                    <asp:TextBox ID="TextKoujiTantousya" runat="server" CssClass="readOnlyStyle" Style="width: 150px"
                                                        Text="" ReadOnly="true" TabIndex="-1" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="koumokuMei firstCol" style="width: 90px;">
                                                    ����
                                                </td>
                                                <td colspan="6" id="TdHantei" runat="server" style="width: 800px;">
                                                    <asp:TextBox ID="TextHantei1Cd" runat="server" CssClass="codeNumber" Style="width: 40px;"
                                                        MaxLength="5" Text="" /><input type="hidden" id="HiddenHanteiSearchType1" runat="server"
                                                            value="" />
                                                    <input type="button" id="ButtonHantei1" value="����" class="gyoumuSearchBtn" runat="server"
                                                        onserverclick="ButtonHantei1_ServerClick" />
                                                    <span id="SpanHantei1" runat="server" style="width: 200px" class="readOnlyStyle"></span>
                                                    <asp:DropDownList ID="SelectHanteiSetuzokuMoji" runat="server" Width="70px">
                                                    </asp:DropDownList><span id="SpanHanteiSetuzokuMoji" runat="server" style="display: none;"></span>
                                                    <asp:TextBox ID="TextHantei2Cd" runat="server" CssClass="codeNumber" Style="width: 40px;"
                                                        MaxLength="5" Text="" /><input type="hidden" id="HiddenHanteiSearchType2" runat="server"
                                                            value="" />
                                                    <input type="button" id="ButtonHantei2" value="����" class="gyoumuSearchBtn" runat="server"
                                                        onserverclick="ButtonHantei2_ServerClick" />
                                                    <span id="SpanHantei2" runat="server" style="width: 200px" class="readOnlyStyle"></span>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <!-- ��ʉ����E�{�^�� -->
    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tbody>
            <tr>
                <th style="text-align: center;">
                    <input type="button" id="ButtonTouroku2" runat="server" value="�o�^ / �C�� ���s" style="font-weight: bold;
                        font-size: 18px; width: 155px; color: black; height: 30px; background-color: fuchsia" />&nbsp;
                </th>
            </tr>
        </tbody>
    </table>
</asp:Content>

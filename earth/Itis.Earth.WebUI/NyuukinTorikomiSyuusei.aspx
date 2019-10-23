<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="NyuukinTorikomiSyuusei.aspx.vb" Inherits="Itis.Earth.WebUI.NyuukinTorikomiSyuusei"
    Title="EARTH �����捞�f�[�^�C��" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script type="text/javascript">
        history.forward();
        
        //�E�B���h�E�T�C�Y�ύX
        try{
            if(window.name != "<%=EarthConst.MAIN_WINDOW_NAME %>") window.resizeTo(720,724);
        }catch(e){
            //�A�N�Z�X�����ۂ���܂����̃G���[���o���牽�����Ȃ��B
            if(e.number == 2147024891) throw e;
        }
               
        _d = document;

        /*====================================
         *�O���[�o���ϐ��錾�i��ʕ��i�j
         ====================================*/        
        //��ʕ\�����i
        var objTotalGaku = null;
        var objGenkin = null;
        var objKogitte = null;
        var objKouzaFurikae = null;
        var objFurikomi = null;
        var objTegata = null;
        var objKyouryokuKaihi = null;
        var objFurikomiTesuuRyou = null;
        var objSousai = null;
        var objNebiki = null;
        var objSonota = null;

        /*************************************
         * onload���̒ǉ�����
         *************************************/
        function funcAfterOnload() {
            //��ʕ\�����i�Z�b�g
            setGlobalObj();
            
            //���v���z�̌v�Z
            CalcTotalGaku();      
        }
        
       /*********************************************
        * ��ʕ\�����i�I�u�W�F�N�g���O���[�o���ϐ���
        *********************************************/
        function setGlobalObj() {
            //�I�u�W�F�N�g�̎擾
            objTotalGaku = objEBI("<%= TextDenpyouGoukeiGaku.clientID %>");
            
            objGenkin = objEBI("<%= TextNkGenkin.clientID %>");
            objKogitte = objEBI("<%= TextNkKogitte.clientID %>");   
            objKouzaFurikae = objEBI("<%= TextNkKouzaFurikae.clientID %>");
            
            objFurikomi = objEBI("<%= TextNkFurikomi.clientID %>");
            objTegata = objEBI("<%= TextNkTegata.clientID %>");
            objKyouryokuKaihi = objEBI("<%= TextNkKyouryokuKaihi.clientID %>");
            
            objFurikomiTesuuRyou = objEBI("<%= TextNkFurikomiTesuuryou.clientID %>");
            objSousai = objEBI("<%= TextNkSousai.clientID %>");
            objNebiki = objEBI("<%= TextNkNebiki.clientID %>");
            
            objSonota = objEBI("<%= TextNkSonota.clientID %>");

        }
               
        //�����z���ύX������(�`�[���v���z�v�Z����)
        function CalcTotalGaku(){                   
            var varTmp = 0; //�`�[���v���z(��Ɨp)
                       
            var varNkGenkin = removeFigure(objGenkin.value);
            var varNkKogitte = removeFigure(objKogitte.value);
            var varNkKouzaFurikae = removeFigure(objKouzaFurikae.value);

            var varNkFurikomi = removeFigure(objFurikomi.value);
            var varNkTegata = removeFigure(objTegata.value);
            var varNkKyouryokuKaihi = removeFigure(objKyouryokuKaihi.value);
            
            var varNkFurikomiTesuuRyou = removeFigure(objFurikomiTesuuRyou.value);
            var varNkSousai = removeFigure(objSousai.value);
            var varNkNebiki = removeFigure(objNebiki.value);
            
            var varNkSonota = removeFigure(objSonota.value);
            
            //�`�[���v���z�̎Z�o
            varTmp = Number(varNkGenkin) + Number(varNkKogitte) + Number(varNkKouzaFurikae) 
                    + Number(varNkFurikomi) + Number(varNkTegata) + Number(varNkKyouryokuKaihi) 
                    + Number(varNkFurikomiTesuuRyou) + Number(varNkSousai) + Number(varNkNebiki) 
                    + Number(varNkSonota);  
                    
            //���z�̐ݒ聕�J���}�t�^
            objTotalGaku.value = addFigure(varTmp);           
        }
        
         /*********************
         * ��������N���A
         *********************/
        function clrSeikyuuSakiInfo(){
                //�l�̃N���A
                objEBI("<%= TextTorikesiRiyuu.clientID %>").value = "";
                //�F���f�t�H���g��
                objEBI("<%= SelectSeikyuuSakiKbn.clientID %>").style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>"
                objEBI("<%= SpanSeikyuuKbn.clientID %>").style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>"
                objEBI("<%= TextSeikyuuSakiCd.clientID %>").style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>"
                objEBI("<%= TextSeikyuuSakiBrc.clientID %>").style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>"
                objEBI("<%= TextSeikyuuSakiMei.clientID %>").style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>"
        }
        
    </script>

    <asp:ScriptManagerProxy ID="SMP1" runat="server">
    </asp:ScriptManagerProxy>
    <input type="hidden" id="HiddenUpdDatetime" runat="server" />
    <input type="hidden" id="HiddenGamenMode" runat="server" />
    <%-- ��ʋN�������ێ� --%>
    <asp:HiddenField ID="HiddenOpenValues" runat="server" />
    <div>
        <%-- ��ʃ^�C�g�� --%>
        <table>
            <tr>
                <td>
                    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
                        class="titleTable">
                        <tr>
                            <th style="text-align: left; width: 160px;">
                                <span id="SpanTitle" runat="server"></span>
                            </th>
                            <th style="width: 70px">
                                <input type="button" id="ButtonClose" value="����" onclick="window.close();" runat="server" />
                            </th>
                            <th style="text-align: right">
                                <input type="button" id="ButtonUpdate" runat="server" value="" style="font-weight: bold;
                                    font-size: 18px; width: 120px; color: black; height: 30px; background-color: fuchsia" />
                            </th>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <%-- ��ʏ㕔[�f�[�^�ڍ�] --%>
        <table cellpadding="0" cellspacing="0" style="border-bottom: solid 2px gray; border-left: solid 2px gray;"
            class="mainTable">
            <tr>
                <td class="koumokuMei" style="width: 110px" colspan="2">
                    �����捞NO</td>
                <td>
                    <asp:TextBox ID="TextNyuukinTorikomiNo" runat="server" Style="width: 50px" CssClass="number readOnlyStyle"
                        ReadOnly="true" TabIndex="-1" />
                </td>
                <td class="koumokuMei">
                    EDI���쐬��</td>
                <td colspan="2">
                    <asp:TextBox ID="TextEdiJouhouSakuseiDate" runat="server" Style="width: 280px" CssClass="date readOnlyStyle"
                        ReadOnly="true" TabIndex="-1" />
                </td>
            </tr>
            <tr>
                <td class="koumokuMei" colspan="2">
                    ���</td>
                <td style="width: 70px">
                    <asp:CheckBox ID="CheckTorikesi" runat="server" /><span id="SpanTorikesi" runat="server"></span></td>
                <td class="koumokuMei">
                    ������</td>
                <td colspan="2">
                    <asp:TextBox ID="TextNyuukinDate" runat="server" Style="width: 70px" CssClass="date hissu"
                        MaxLength="10" /></td>
            </tr>
            <tr>
                <td class="koumokuMei" colspan="2">
                    �捞�`�[�ԍ�</td>
                <td>
                    <asp:TextBox ID="TextTorikomiDenpyouNo" runat="server" Style="width: 60px" CssClass="codeNumber hissu"
                        MaxLength="6" />
                </td>
                <td class="koumokuMei">
                    �ƍ�����No.</td>
                <td colspan="2">
                    <asp:TextBox ID="TextSyougouKouzaNo" runat="server" Style="width: 90px" CssClass="codeNumber hissu"
                        MaxLength="10" />
                </td>
            </tr>
            <tr>
                <td class="koumokuMei" colspan="2" rowspan="2">
                    ������</td>
                <td colspan="2" style="width: 300px;">
                    <asp:UpdatePanel ID="UpdatePanel_seikyuu" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <ContentTemplate>
                            <asp:DropDownList ID="SelectSeikyuuSakiKbn" runat="server" CssClass="hissu">
                            </asp:DropDownList><span id="SpanSeikyuuKbn" runat="server"></span>
                            <asp:TextBox ID="TextSeikyuuSakiCd" runat="server" MaxLength="5" Style="width: 40px;"
                                CssClass="codeNumber hissu" />&nbsp;-
                            <asp:TextBox ID="TextSeikyuuSakiBrc" runat="server" MaxLength="2" Style="width: 20px;"
                                CssClass="codeNumber hissu" />
                            <input id="ButtonSearchSeikyuuSaki" runat="server" type="button" value="����" class="gyoumuSearchBtn" />
                            <input type="hidden" id="HiddenSeikyuuSakiCdOld" runat="server" />
                            <input type="hidden" id="HiddenSeikyuuSakiBrcOld" runat="server" />
                            <input type="hidden" id="HiddenSeikyuuSakiKbnOld" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="koumokuMei" style="width: 100px">
                    ���</td>
                <td id="TdTorikesiRiyuu">
                    <asp:UpdatePanel ID="UpdatePanel_SeikyuusakiToikesi" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ButtonSearchSeikyuuSaki" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:TextBox ID="TextTorikesiRiyuu" runat="server" Style="width: 10em;" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:UpdatePanel ID="UpdatePanel_SeikyuusakiMei" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ButtonSearchSeikyuuSaki" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:TextBox ID="TextSeikyuuSakiMei" Style="width: 420px;" runat="server" MaxLength="80" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="koumokuMei" rowspan="10">
                    �����z</td>
                <td class="shouhinTableTitleNyuukin" style="width: 110px">
                    ����</td>
                <td colspan="5">
                    <asp:TextBox ID="TextNkGenkin" runat="server" Style="width: 90px" CssClass="kingaku"
                        MaxLength="10" />
                </td>
            </tr>
            <tr>
                <td class="shouhinTableTitleNyuukin">
                    ���؎�</td>
                <td colspan="5">
                    <asp:TextBox ID="TextNkKogitte" runat="server" Style="width: 90px" CssClass="kingaku"
                        MaxLength="10" />
                </td>
            </tr>
            <tr>
                <td class="shouhinTableTitleNyuukin">
                    �����U��</td>
                <td colspan="5">
                    <asp:TextBox ID="TextNkKouzaFurikae" runat="server" Style="width: 90px" CssClass="kingaku"
                        MaxLength="10" />
                </td>
            </tr>
            <tr>
                <td class="shouhinTableTitleNyuukin">
                    �U��</td>
                <td colspan="5">
                    <asp:TextBox ID="TextNkFurikomi" runat="server" Style="width: 90px" CssClass="kingaku"
                        MaxLength="10" />
                </td>
            </tr>
            <tr>
                <td class="shouhinTableTitleNyuukin">
                    ��`</td>
                <td colspan="5">
                    <asp:TextBox ID="TextNkTegata" runat="server" Style="width: 90px" CssClass="kingaku"
                        MaxLength="10" />
                </td>
            </tr>
            <tr>
                <td class="shouhinTableTitleNyuukin">
                    ���͉��</td>
                <td colspan="5">
                    <asp:TextBox ID="TextNkKyouryokuKaihi" runat="server" Style="width: 90px" CssClass="kingaku"
                        MaxLength="10" />
                </td>
            </tr>
            <tr>
                <td class="shouhinTableTitleNyuukin">
                    �U��<br />
                    �萔��</td>
                <td colspan="5">
                    <asp:TextBox ID="TextNkFurikomiTesuuryou" runat="server" Style="width: 90px" CssClass="kingaku"
                        MaxLength="10" />
                </td>
            </tr>
            <tr>
                <td class="shouhinTableTitleNyuukin">
                    ���E</td>
                <td colspan="5">
                    <asp:TextBox ID="TextNkSousai" runat="server" Style="width: 90px" CssClass="kingaku"
                        MaxLength="10" />
                </td>
            </tr>
            <tr>
                <td class="shouhinTableTitleNyuukin">
                    �l��</td>
                <td colspan="5">
                    <asp:TextBox ID="TextNkNebiki" runat="server" Style="width: 90px" CssClass="kingaku"
                        MaxLength="10" />
                </td>
            </tr>
            <tr>
                <td class="shouhinTableTitleNyuukin">
                    ���̑�</td>
                <td>
                    <asp:TextBox ID="TextNkSonota" runat="server" Style="width: 90px" CssClass="kingaku"
                        MaxLength="10" />
                </td>
                <td class="shouhinTableTitleNyuukin" style="width: 120px">
                    �`�[���v���z</td>
                <td colspan="2">
                    <asp:TextBox ID="TextDenpyouGoukeiGaku" runat="server" Style="width: 120px" CssClass="kingaku readOnlyStyle"
                        ReadOnly="true" TabIndex="-1" />
                </td>
            </tr>
            <tr>
                <td class="koumokuMei" colspan="2">
                    ��`����</td>
                <td>
                    <asp:TextBox ID="TextTegataKijitu" runat="server" Style="width: 70px" CssClass="date"
                        MaxLength="10" />
                </td>
                <td class="koumokuMei">
                    ��`No.</td>
                <td colspan="2">
                    <asp:TextBox ID="TextTegataNo" runat="server" Style="width: 90px" CssClass="codeNumber"
                        MaxLength="10" />
                </td>
            </tr>
            <tr>
                <td class="koumokuMei" colspan="2">
                    �E�v��</td>
                <td colspan="5">
                    <textarea id="TextAreaTekiyou" runat="server" cols="80" onfocus="this.select();"
                        rows="4" style="ime-mode: active; font-family: Sans-Serif"></textarea>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

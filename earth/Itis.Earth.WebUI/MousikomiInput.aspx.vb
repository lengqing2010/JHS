
Partial Public Class MousikomiInput
    Inherits System.Web.UI.Page

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim userinfo As New LoginUserInfo
    Dim masterAjaxSM As New ScriptManager
    Dim cl As New CommonLogic
    Dim cbLogic As New CommonBizLogic

    ''' <summary>
    ''' ���b�Z�[�W�N���X
    ''' </summary>
    ''' <remarks></remarks>
    Dim MLogic As New MessageLogic
    Dim JLogic As New JibanLogic
    Dim sLogic As New StringLogic

    ''' <summary>
    ''' ���������R���g���[���^�C�v
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emBrCtrl
        ''' <summary>
        ''' ���������R���g���[��1
        ''' </summary>
        ''' <remarks></remarks>
        intCtrl1 = 1
        ''' <summary>
        ''' ���������R���g���[��2
        ''' </summary>
        ''' <remarks></remarks>
        intCtrl2 = 2
        ''' <summary>
        ''' ���������R���g���[��3
        ''' </summary>
        ''' <remarks></remarks>
        intCtrl3 = 3
    End Enum

#Region "���������E�s�R���g���[��ID�ړ���"
    Private Const BUKKEN_RIREKI_CTRL_NAME As String = "CtrlBukkenRireki_"
    Private Const SELECT_SYUBETU_CTRL_NAME As String = "SelectSyubetu_"
    Private Const BUKKEN_RIREKI_CTRL_CNT As Integer = 1
#End Region

    ''' <summary>
    ''' �y�[�W���[�h
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim jBn As New Jiban '�n�Չ�ʋ��ʃN���X
        Dim jSM As New JibanSessionManager

        '�}�X�^�[�y�[�W�����擾�iScriptManager�p�j
        Dim myMaster As EarthMasterPage = Page.Master
        masterAjaxSM = myMaster.AjaxScriptManager1

        Dim strKbn As String = ""
        Dim strBangou As String = ""

        ' ���[�U�[��{�F��
        jBn.UserAuth(userinfo)

        '�F�،��ʂɂ���ĉ�ʕ\����ؑւ���
        If userinfo Is Nothing OrElse userinfo.SinkiNyuuryokuKengen = 0 Then
            '���O�C����񂪖����ꍇ�A�V�K���͌������Ȃ��ꍇ�A���C����ʂɔ�΂�
            Response.Redirect(UrlConst.MAIN)
            Exit Sub
        End If

        '�e�e�[�u���̕\����Ԃ�؂�ւ���
        Me.TBodyBRInfo.Style("display") = Me.HiddenBRInfoStyle.Value

        If IsPostBack = False Then '�����N����

            '****************************************************************************
            ' �h���b�v�_�E�����X�g�ݒ�
            '****************************************************************************
            ' �R���{�ݒ�w���p�[�N���X�𐶐�
            Dim helper As New DropDownHelper
            Dim objDrpTmp As New DropDownList
            ' �敪�R���{�Ƀf�[�^���o�C���h����
            helper.SetDropDownList(SelectKIKubun, DropDownHelper.DropDownType.Kubun2, True)
            ' �\����ʃR���{�Ƀf�[�^���o�C���h����
            helper.SetDropDownList(SelectKouzouSyubetu, DropDownHelper.DropDownType.Kouzou, True, False)
            ' �K�w�R���{�Ƀf�[�^���o�C���h����
            helper.SetDropDownList(SelectKaisou, DropDownHelper.DropDownType.Kaisou, True, False)
            ' �V�z���փR���{�Ƀf�[�^���o�C���h����
            helper.SetDropDownList(SelectSintikuTatekae, DropDownHelper.DropDownType.ShintikuTatekae, True, False)
            ' �����p�r�R���{�Ƀf�[�^���o�C���h����
            helper.SetDropDownList(SelectTatemonoYouto, DropDownHelper.DropDownType.TatemonoYouto, True, False)
            ' �\���b�R���{�Ƀf�[�^���o�C���h����
            helper.SetDropDownList(SelectYoteiKiso, DropDownHelper.DropDownType.YoteiKiso, True, False)
            ' �n���ԌɌv��R���{�Ƀf�[�^���o�C���h����
            helper.SetDropDownList(SelectTikaSyakoKeikaku, DropDownHelper.DropDownType.Syako, True)
            ' �o�R�R���{�Ƀf�[�^���o�C���h����
            helper.SetDropDownList(SelectSIKeiyu, DropDownHelper.DropDownType.Keiyu, False, True)
            ' ���i1�R���{�Ƀf�[�^���o�C���h����
            helper.SetDropDownList(choSyouhin1, DropDownHelper.DropDownType.Syouhin1, True, True)
            '�������@
            helper.SetDropDownList(SelectSITysHouhou, DropDownHelper.DropDownType.TyousaHouhou, True, False)
            '�����T�v
            helper.SetDropDownList(SelectSITysGaiyou, DropDownHelper.DropDownType.TyousaGaiyou, True)

            ' ��ʃR���{�Ƀf�[�^���o�C���h����
            helper.SetMeisyouDropDownList(Me.SelectBRSyubetu1, EarthConst.emMeisyouType.BUKKEN_RIREKI_SYUBETU)
            helper.SetMeisyouDropDownList(Me.SelectBRSyubetu2, EarthConst.emMeisyouType.BUKKEN_RIREKI_SYUBETU)
            helper.SetMeisyouDropDownList(Me.SelectBRSyubetu3, EarthConst.emMeisyouType.BUKKEN_RIREKI_SYUBETU)

            '���_�~�[�R���{�ɃZ�b�g
            helper.SetMeisyouDropDownList(Me.SelectTmpCode, EarthConst.emMeisyouType.BUKKEN_RIREKI_SYUBETU, False, True)

            '�_�~�[�h���b�v�_�E�����X�g�̐���
            Me.CreateDropDownList(Me.SelectTmpCode)

            '****************************************************************************
            ' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
            '****************************************************************************
            SetDispAction()

        Else
            '�_�~�[�h���b�v�_�E�����X�g�̐���
            Me.CreateDropDownList(Me.SelectTmpCode)

        End If

        '�{�^�������C�x���g�̐ݒ�
        setBtnEvent()

        If ButtonTouroku1.Disabled = False Then
            ButtonTouroku1.Focus() '�o�^/�C���{�^���Ƀt�H�[�J�X
        End If

    End Sub

    ''' <summary>
    ''' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDispAction()
        Dim jBn As New Jiban '�n�Չ�ʃN���X

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' ���V�X�e���ւ̃����N�{�^���ݒ�
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '�����X���ӎ���
        cl.getKameitenTyuuijouhouPath(Me.TextKITourokuBangou.ClientID, Me.ButtonKIKameitenTyuuijouhou)

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' �敪�A�ۏ؏�NO�֘A
        '+++++++++++++++++++++++++++++++++++++++++++++++++++


        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' �d���`�F�b�N�֘A(���\���ݒ�)
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Me.CheckTyoufuku(Nothing)

        Me.TextBukkenMeisyou.Attributes("onchange") = "ChgTyoufukuBukken(this);"
        Me.TextBukkenJyuusyo1.Attributes("onchange") = "ChgTyoufukuBukken(this);"
        Me.TextBukkenJyuusyo2.Attributes("onchange") = "ChgTyoufukuBukken(this);"

        '�Z���R����l�ɓ]�L
        Me.ButtonJyuusyoTenki.Attributes("onclick") = "juushoTenki_onclick();"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' �v���_�E��/�R�[�h���͘A�g�ݒ�
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '�\�����
        jBn.SetPullCdScriptSrc(TextKouzouSyubetuCd, SelectKouzouSyubetu)
        '�V�z����
        jBn.SetPullCdScriptSrc(TextSintikuTatekaeCd, SelectSintikuTatekae)
        '�K�w
        jBn.SetPullCdScriptSrc(TextKaisouCd, SelectKaisou)
        '�����p�r
        jBn.SetPullCdScriptSrc(TextTatemonoYoutoCd, SelectTatemonoYouto)
        '�\���b
        jBn.SetPullCdScriptSrc(TextYoteiKisoCd, SelectYoteiKiso)
        '�n���ԌɌv��
        jBn.SetPullCdScriptSrc(TextTikaSyakoKeikakuCd, SelectTikaSyakoKeikaku)
        '�������@
        jBn.SetPullCdScriptSrc(TextSITysHouhouCd, SelectSITysHouhou)

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' �u�`���̑��v��u����L��v�̏ꍇ�̂ݕ\�����鍀��
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '�\���A�\���b�Ɋւ��ẮA�u9�F���̑��v�̏ꍇ�̂݁u�`���̑��v�̓��͍��ڂ��g�p�\�ɂ���X�N���v�g�𖄂ߍ���
        SelectKouzouSyubetu.Attributes("onchange") += "checkSonota(this.value==9,'" & TextKouzouSyubetuSonota.ClientID & "');"
        SelectYoteiKiso.Attributes("onchange") += "checkSonota(this.value==9,'" & TextYoteiKisoSonota.ClientID & "');"

        '�w��l�I�����̓���(��ʕ\�����p)
        checkSonota()

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' �O��/�������t�����֘A�Z�b�g
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '�O��
        Me.HiddenDateYesterday.Value = Format(Today.AddDays(-1), "yyyy/MM/dd")
        Dim strBefore As String = "if(objEBI('@IRAIDATA').value=='')objEBI('@IRAIDATA').value='@YESTERDAY';"
        strBefore = strBefore.Replace("@IRAIDATA", Me.TextIraiDate.ClientID)
        strBefore = strBefore.Replace("@YESTERDAY", Me.HiddenDateYesterday.Value)
        Me.ButtonIraiDateYestarday.Attributes("onclick") = strBefore

        '����
        Me.HiddenDateToday.Value = Format(Today, "yyyy/MM/dd")
        Dim strToday As String = "if(objEBI('@IRAIDATA').value=='')objEBI('@IRAIDATA').value='@TODAY';"
        strToday = strToday.Replace("@IRAIDATA", Me.TextIraiDate.ClientID)
        strToday = strToday.Replace("@TODAY", Me.HiddenDateToday.Value)
        Me.ButtonIraiDateToday.Attributes("onclick") = strToday

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' �����˗������A�����p�r�A�ː��֘A
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' �����˗������̓f�t�H���g�P
        If TextDoujiIraiTousuu.Text = String.Empty Then
            TextDoujiIraiTousuu.Text = "1"
        End If

        ' �����p�r�̓f�t�H���g�P
        If SelectTatemonoYouto.SelectedValue = String.Empty Then
            TextTatemonoYoutoCd.Text = "1"
            SelectTatemonoYouto.SelectedValue = "1"
        End If
        ' �ː��̓f�t�H���g�P
        If TextSIKosuu.Text = String.Empty Then
            TextSIKosuu.Text = "1"
        End If

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' Javascript�֘A�Z�b�g
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Dim onFocusPostBackScript As String = "removeFig(this);setTempValueForOnBlur(this);"
        Dim onBlurPostBackScript As String = "if(checkTempValueForOnBlur(this)){if(checkNumberAddFig(this)){__doPostBack(this.id,'');}}else{checkNumberAddFig(this);}"
        Dim onFocusScript As String = "removeFig(this);"
        Dim onBlurScript As String = "checkNumberAddFig(this);"
        Dim checkDate As String = "checkDate(this);"
        Dim checkKingaku As String = "checkKingaku(this);"
        Dim checkNumber As String = "checkNumber(this);"

        Dim disabledOnkeydown As String = "if (event.keyCode == 13){return false;};"

        '���t���ڗp
        Dim onFocusPostBackScriptDate As String = "setTempValueForOnBlur(this);"
        Dim onBlurPostBackScriptDate As String = "if(checkTempValueForOnBlur(this)){if(checkDate(this)){__doPostBack(this.id,'');}}else{checkDate(this);}"

        '*****************************
        '* ���t�n
        '*****************************
        '<��ʒ�����>
        '�˗���
        TextIraiDate.Attributes("onblur") = checkDate
        TextIraiDate.Attributes("onkeydown") = disabledOnkeydown
        '������]��
        TextTyousaKibouDate.Attributes("onblur") = checkDate
        TextTyousaKibouDate.Attributes("onkeydown") = disabledOnkeydown
        '��b���H�\���FROM
        TextKsTyakkouYoteiDateFrom.Attributes("onblur") = checkDate
        TextKsTyakkouYoteiDateFrom.Attributes("onkeydown") = disabledOnkeydown
        '��b���H�\���TO
        TextKsTyakkouYoteiDateTo.Attributes("onblur") = checkDate
        TextKsTyakkouYoteiDateTo.Attributes("onkeydown") = disabledOnkeydown
        '<�����������>
        '���t
        Me.TextBRHizuke1.Attributes("onblur") = checkDate
        Me.TextBRHizuke1.Attributes("onkeydown") = disabledOnkeydown
        '���t
        Me.TextBRHizuke2.Attributes("onblur") = checkDate
        Me.TextBRHizuke2.Attributes("onkeydown") = disabledOnkeydown
        '���t
        Me.TextBRHizuke3.Attributes("onblur") = checkDate
        Me.TextBRHizuke3.Attributes("onkeydown") = disabledOnkeydown

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ ���l�n
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '���񓯎��˗�����
        Me.TextDoujiIraiTousuu.Attributes("onfocus") = onFocusScript
        Me.TextDoujiIraiTousuu.Attributes("onblur") = onBlurScript
        '�\�����
        Me.TextKouzouSyubetuCd.Attributes("onblur") += checkNumber
        '�V�z����
        Me.TextSintikuTatekaeCd.Attributes("onblur") += checkNumber
        '�K�w
        Me.TextKaisouCd.Attributes("onblur") += checkNumber
        '�����p�r
        Me.TextTatemonoYoutoCd.Attributes("onblur") += checkNumber
        '�݌v���e�x����
        Me.TextSekkeiKyoyouSijiryoku.Attributes("onfocus") = onFocusScript
        Me.TextSekkeiKyoyouSijiryoku.Attributes("onblur") = onBlurScript
        '�˗��\�蓏��
        Me.TextIraiYoteiTousuu.Attributes("onfocus") = onFocusScript
        Me.TextIraiYoteiTousuu.Attributes("onblur") = onBlurScript
        '���؂�[��
        Me.TextNegiriHukasa.Attributes("onfocus") = onFocusScript
        Me.TextNegiriHukasa.Attributes("onblur") = onBlurScript
        '�\�萷�y����
        Me.TextYoteiMoritutiAtusa.Attributes("onfocus") = onFocusScript
        Me.TextYoteiMoritutiAtusa.Attributes("onblur") = onBlurScript
        '�\���b
        Me.TextYoteiKisoCd.Attributes("onblur") += checkNumber
        '�n���ԌɌv��
        Me.TextTikaSyakoKeikakuCd.Attributes("onblur") += checkNumber
        '�ː�
        Me.TextSIKosuu.Attributes("onfocus") = onFocusScript
        Me.TextSIKosuu.Attributes("onblur") = onBlurScript
        '�������@
        Me.TextSITysHouhouCd.Attributes("onblur") += checkNumber

        '*****************************
        '* �R�[�h����у|�b�v�A�b�v�{�^��
        '*****************************
        '�����X���.�o�^�ԍ�
        TextKITourokuBangou.Attributes("onblur") = "if(checkTempValueForOnBlur(this)){if(checkNumber(this))callKameitenSearch(this);}else{checkNumber(this);}"
        TextKITourokuBangou.Attributes("onfocus") = "setTempValueForOnBlur(this);"
        '���̑����.�������
        TextSITysGaisyaCd.Attributes("onblur") = "if(checkTempValueForOnBlur(this)){if(checkNumber(this))callTyousakaisyaSearch(this);}else{checkNumber(this);}"
        TextSITysGaisyaCd.Attributes("onfocus") = "setTempValueForOnBlur(this);"

        '�|�b�v�A�b�v�{�^��
        ButtonKITourokuBangou.Attributes("onclick") = "SetChangeMaeValue('" & HiddenKITourokuBangouMae.ClientID & "','" & TextKITourokuBangou.ClientID & "');"
        ButtonSITysGaisya.Attributes("onclick") = "SetChangeMaeValue('" & HiddenSITysGaisyaMae.ClientID & "','" & TextSITysGaisyaCd.ClientID & "');"

        '*****************************
        '* �v���_�E��
        '*****************************
        '<��ʉE�㕔>
        '�敪
        SelectKIKubun.Attributes("onfocus") = "SetChangeMaeValue('" & HiddenKIKbnMae.ClientID & "','" & SelectKIKubun.ClientID & "');"
        SelectKIKubun.Attributes("onchange") = "ChgSelectKbn();"
        '<�����������>
        '���
        Me.SelectBRSyubetu1.Attributes("onchange") = "SelectSyubetuOnChg('" & Me.SelectBRSyubetu1.ClientID & "',1)"
        '���
        Me.SelectBRSyubetu2.Attributes("onchange") = "SelectSyubetuOnChg('" & Me.SelectBRSyubetu2.ClientID & "',2)"
        '���
        Me.SelectBRSyubetu3.Attributes("onchange") = "SelectSyubetuOnChg('" & Me.SelectBRSyubetu3.ClientID & "',3)"

        '*****************************
        '* ���W�I�{�^��
        '*****************************
        '���i�敪�̕\���ؑ�
        checkSyouhinkubun()

        '��������҂̕\���ؑ�
        checkTysTatiaisya()

        '*****************************
        '* �����T�v�ݒ�
        '*****************************
        Dim setTysGaiyouScript As String = "callSetTysGaiyou(this);"
        Me.choSyouhin1.Attributes("onchange") = setTysGaiyouScript
        Me.SelectSITysHouhou.Attributes("onchange") += setTysGaiyouScript

        '���i1�ύX���ASDS�����ݒ�
        Dim setTysHouhouGaisyaScript As String = "callKameitenSearchFromSyouhin1(this);"
        Me.choSyouhin1.Attributes("onchange") += setTysHouhouGaisyaScript

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ �@�\�ʃe�[�u���̕\���ؑ�
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '<�����������>
        Me.AncBRInfo.HRef = "JavaScript:changeDisplay('" & Me.TBodyBRInfo.ClientID & "');SetDisplayStyle('" & Me.HiddenBRInfoStyle.ClientID & "','" & Me.TBodyBRInfo.ClientID & "');"

    End Sub

    ''' <summary>
    ''' Ajax���쎞�̃t�H�[�J�X�Z�b�g
    ''' </summary>
    ''' <param name="ctrl"></param>
    ''' <remarks></remarks>
    Private Sub setFocusAJ(ByVal ctrl As Control)
        '�t�H�[�J�X�Z�b�g
        masterAjaxSM.SetFocus(ctrl)
    End Sub

    ''' <summary>
    ''' �o�^/�C�����s�{�^���̐ݒ�
    ''' </summary>
    ''' <remarks>
    ''' DB�X�V�̊m�F���s�Ȃ��B<br/>
    ''' OK���FDB�X�V���s�Ȃ��B<br/>
    ''' �L�����Z�����FDB�X�V�𒆒f����B<br/>
    ''' </remarks>
    Private Sub setBtnEvent()

        '�\���ݒ�
        '�ԍ�
        Me.TextBangou.Visible = False

        '�V�K(���p)�\���{�^��
        Me.ButtonSinkiHikitugi.Visible = False
        '�V�K�\���{�^��
        Me.ButtonSinki.Visible = False

        '�C�x���g�n���h���o�^
        Dim tmpScript As String = "actClickButton(this)"
        Me.ButtonTouroku1.Attributes("onclick") = tmpScript
        Me.ButtonTouroku2.Attributes("onclick") = tmpScript

    End Sub

    ''' <summary>
    ''' �����T�v�ݒ�{�^��(��\��)�������̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSetTysGaiyou_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetTysGaiyou.ServerClick
        '�ݒ�E�擾�p ���i���i�ݒ背�R�[�h
        Dim recKakakuSettei As New KakakuSetteiRecord

        '���s���̃N���C�A���g�R���g���[��
        Dim strActCtrlId As String = actCtrlId.Value.Replace(ClientID & ClientIDSeparator.ToString, "")
        Dim actCtrl As Control = FindControl(strActCtrlId)

        '���i�敪
        If RadioSISyouhinKbn1.Checked Then '60�N�ۏ�
            recKakakuSettei.SyouhinKbn = Me.RadioSISyouhinKbn1.Value
        ElseIf RadioSISyouhinKbn2.Checked Then '�y�n�̔�
            recKakakuSettei.SyouhinKbn = Me.RadioSISyouhinKbn2.Value
        ElseIf RadioSISyouhinKbn3.Checked Then '���t�H�[��
            recKakakuSettei.SyouhinKbn = Me.RadioSISyouhinKbn3.Value
        Else '���̑�
            recKakakuSettei.SyouhinKbn = Me.RadioSISyouhinKbn9.Value
        End If
        '�������@
        cl.SetDisplayString(Me.TextSITysHouhouCd.Text, recKakakuSettei.TyousaHouhouNo)
        '���i�R�[�h
        cl.SetDisplayString(Me.choSyouhin1.SelectedValue, recKakakuSettei.SyouhinCd)

        '���i���i�ݒ�}�X�^����l�̎擾
        JLogic.GetTysGaiyou(recKakakuSettei)

        '�����T�v�̐ݒ�
        Me.SelectSITysGaiyou.SelectedValue = cl.GetDispNum(recKakakuSettei.TyousaGaiyou, "")

        '�t�H�[�J�X�Z�b�g
        If actCtrlId.Value = Me.choSyouhin1.ClientID Then
            masterAjaxSM.SetFocus(Me.choSyouhin1)
        ElseIf actCtrlId.Value = Me.SelectSITysHouhou.ClientID Then
            masterAjaxSM.SetFocus(Me.SelectSITysHouhou)
        End If


    End Sub

#Region "[�����X���]����"

    ''' <summary>
    ''' �����X���.�o�^�ԍ������{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonKITourokuBangou_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonKITourokuBangou.ServerClick
        '�������s�O�̋敪�l��ێ�
        Dim tmpOldKbn As String = Me.SelectKIKubun.SelectedValue

        If kameitenSearchType.Value <> "1" Then
            kameitenSearchSub(sender, e)
        Else
            '�R�[�honchange�ŌĂ΂ꂽ�ꍇ
            kameitenSearchSub(sender, e, False)
            kameitenSearchType.Value = String.Empty
        End If

        If tmpOldKbn <> Me.SelectKIKubun.SelectedValue Then
            '�����X�����̌��ʁA�敪���ύX����Ă���ꍇ�A�d���`�F�b�N���s��(�`�F�b�N���s�g���K�[�͋敪�Ƃ���)
            Me.HiddenTyoufukuKakuninTargetId.Value = Me.SelectKIKubun.ClientID
            Me.CheckTyoufuku(sender)
        End If
    End Sub

    ''' <summary>
    ''' �����X�����{�^���������̏���(����)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="callWindow">�����|�b�v�A�b�v���N�����邩�ۂ��̎w��</param>
    ''' <remarks></remarks>
    Private Sub kameitenSearchSub(ByVal sender As System.Object, ByVal e As System.EventArgs, Optional ByVal callWindow As Boolean = True)

        ' ���͂���Ă���R�[�h���L�[�ɁA�}�X�^���������A�f�[�^��1���̂ݎ擾�ł����ꍇ��
        ' ��ʏ����X�V����B�f�[�^�������ꍇ�́A������ʂ�\������
        Dim kameitenSearchLogic As New KameitenSearchLogic
        Dim dataArray As New List(Of KameitenSearchRecord)
        Dim blnTorikesi As Boolean = True
        Dim total_count As Integer
        Dim strTysGaisyaCd As String = String.Empty
        Dim blnRet As Boolean = False

        ' �擾�������i�荞�ޏꍇ�A������ǉ����Ă�������
        If TextKITourokuBangou.Text <> "" Then
            dataArray = kameitenSearchLogic.GetKameitenSearchResult(SelectKIKubun.SelectedValue, _
                                                                    TextKITourokuBangou.Text, _
                                                                    blnTorikesi, _
                                                                    total_count)
        End If

        If dataArray.Count = 1 Then

            '�����X�R�[�h����꒼��
            Me.TextKITourokuBangou.Text = dataArray(0).KameitenCd
            Me.HiddenKITourokuBangouMae.Value = Me.TextKITourokuBangou.Text

            '���r���_�[���ӎ����`�F�b�N
            If kameitenSearchLogic.ChkBuilderData13(Me.TextKITourokuBangou.Text) Then
                Me.HiddenKameitenTyuuiJikou.Value = Boolean.TrueString
            Else
                Me.HiddenKameitenTyuuiJikou.Value = Boolean.FalseString
            End If

            '���������@SDS�����ݒ�`�F�b�N
            '���.���i1�����͍ς݈ȊO�̏ꍇ�A�ȍ~�̏����͍s��Ȃ��B
            If (TextKITourokuBangou.Text <> "") AndAlso (Me.choSyouhin1.SelectedValue <> "") Then
                blnRet = cbLogic.ChkTysJidouSet(Me.choSyouhin1.SelectedValue, Me.TextKITourokuBangou.Text, strTysGaisyaCd)
            End If

            '������~�`�F�b�N
            cl.chkOrderStopFlg(sender, dataArray(0).OrderStopFLG, Me.TextKITourokuBangou.Text, Me.saveCdOrderStop.Value)

            ' �t�H�[�J�X�Z�b�g > ���i�P���͌�ɉ�ʂ��X�N���[�����Ă��܂��̂Ŕr��
            ' setFocusAJ(ButtonKITourokuBangou)
        Else
            '���r���_�[���ӎ����t���O������
            Me.HiddenKameitenTyuuiJikou.Value = String.Empty

            '������ʕ\���pJavaScript�wcallSearch�x�����s
            Dim tmpFocusScript = "objEBI('" & ButtonKITourokuBangou.ClientID & "').focus();"
            Dim tmpScript As String = "callSearch('" & SelectKIKubun.ClientID & EarthConst.SEP_STRING & TextKITourokuBangou.ClientID & "','" & UrlConst.SEARCH_KAMEITEN & "','" & _
                            TextKITourokuBangou.ClientID & EarthConst.SEP_STRING & TextKISyamei.ClientID & "','" & ButtonKITourokuBangou.ClientID & "');"

            '�����X���m��̂��ߖ��̃N���A
            '�N���A���s�Ȃ�
            Me.ClearKameitenInfo(False)

            '�|�b�v�A�b�v�\��
            If callWindow Then
                tmpScript = tmpFocusScript & tmpScript
                ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
                Exit Sub
            ElseIf kameitenSearchType.Value = "1" Then
                tmpScript = tmpFocusScript
                ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
            End If
        End If

        ' �����X�������s�㏈�����s
        kameitenSearchAfter_ServerClick(sender, e, blnRet, strTysGaisyaCd)

    End Sub

    ''' <summary>
    ''' �����X�������s�㏈��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="blnRet">SDS�����ݒ�`�F�b�N����</param>
    ''' <param name="strTysGaisyaCd">SDS�����ݒ蒲�����</param>
    ''' <remarks></remarks>
    Protected Sub kameitenSearchAfter_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal blnRet As Boolean, ByVal strTysGaisyaCd As String)

        '�o�^�ԍ�
        If Me.TextKITourokuBangou.Text <> "" Then '����
            '�����X�R�[�h��ޔ�(������~�����̏ꍇ�͍Č���)
            Me.saveCdOrderStop.Value = Me.TextKITourokuBangou.Text

            '�����X�֘A���ڂ̐ݒ���s�Ȃ�
            '�����X�������s�㏈��(�����X�ڍ׏��A�r���_�[���擾)
            Dim logic As New KameitenSearchLogic
            Dim blnTorikesi As Boolean = True
            Dim dataArray As New List(Of KameitenSearchRecord)
            Dim record As KameitenSearchRecord

            record = logic.GetKameitenSearchResult("", TextKITourokuBangou.Text, "", blnTorikesi)

            If Not record Is Nothing AndAlso record.KameitenCd <> String.Empty Then
                '�敪
                Me.SelectKIKubun.SelectedValue = cl.GetDisplayString(record.Kbn)
                '�Ж�
                Me.TextKISyamei.Text = cl.GetDisplayString(record.KameitenMei1)
                '�Z��
                Me.TextKIJyuusyo.Text = cl.GetDisplayString(record.Jyuusyo)
                'TEL
                Me.TextKITel.Text = cl.GetDisplayString(record.TelNo)
                'FAX
                Me.TextKIFax.Text = cl.GetDisplayString(record.FaxNo)

                'JIO��̕\���ݒ�
                If record.JioSakiFLG = 1 Then
                    Me.SpanKIJioSaki.InnerHtml = EarthConst.JIO_SAKI
                Else
                    Me.SpanKIJioSaki.InnerHtml = ""
                End If

                '�����X�ɂ�鎩���ݒ�
                '�ۏ؏����s�L���͂O�̏ꍇ�����A�ȊO�P�i�n�Վd�l�j
                Me.SelectSIHosyouUmu.SelectedValue = IIf(cl.GetDisplayString(record.HosyousyoHakUmu) = "1", "1", "")
                Me.HiddenHosyouKikan.Value = cl.GetDisplayString(record.HosyouKikan)
                ' �H����А����L���ݒ�
                Me.HiddenKjGaisyaSeikyuuUmu.Value = IIf(cl.GetDisplayString(record.KojGaisyaSeikyuuUmu) = "1", "1", "")

                '�������@SDS�����ݒ�
                If blnRet = True AndAlso strTysGaisyaCd <> String.Empty Then
                    '�������@
                    Me.TextSITysHouhouCd.Text = EarthConst.TYOUSA_HOUHOU_CD_15
                    Me.SelectSITysHouhou.SelectedValue = EarthConst.TYOUSA_HOUHOU_CD_15
                    '�������
                    Me.TextSITysGaisyaCd.Text = strTysGaisyaCd
                    tyousakaisyaSearchSub(sender, e, False)
                    '�����T�v
                    btnSetTysGaiyou_ServerClick(sender, e)
                End If
            Else
                '�N���A���s�Ȃ�
                ClearKameitenInfo()

            End If

        Else '������
            '�N���A���s�Ȃ�
            ClearKameitenInfo(False)

        End If

    End Sub

    ''' <summary>
    ''' �����X.�N���A�{�^��(��\��)����������
    ''' �������X�֘A�����N���A����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonKIKameitenClear_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        setFocusAJ(Me.SelectKIKubun)

        '�N���A���s�Ȃ�
        ClearKameitenInfo()

        '�敪
        Me.SelectKIKubun.SelectedValue = CStr(Me.HiddenKIKbnMae.Value)

        '�d���`�F�b�N���s��(�`�F�b�N���s�g���K�[�͋敪�Ƃ���)
        Me.HiddenTyoufukuKakuninTargetId.Value = Me.SelectKIKubun.ClientID
        Me.CheckTyoufuku(sender)

    End Sub

    ''' <summary>
    ''' �����X�����N���A����
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearKameitenInfo(Optional ByVal blnFlg As Boolean = True)

        If blnFlg Then
            '�敪
            Me.SelectKIKubun.SelectedValue = ""
            '�o�^�ԍ�
            Me.TextKITourokuBangou.Text = ""
            ' �S����
            Me.TextKITantousya.Text = ""
        End If

        '�����X�R�[�h���N���A(������~�����p)
        Me.saveCdOrderStop.Value = ""

        '�Ж�
        Me.TextKISyamei.Text = ""
        '�Z��
        Me.TextKIJyuusyo.Text = ""
        'TEL
        Me.TextKITel.Text = ""
        'FAX
        Me.TextKIFax.Text = ""

        'JIO��̕\���ݒ�
        Me.SpanKIJioSaki.InnerHtml = ""

        '�����X�ɂ�鎩���ݒ�
        '�ۏ؏����s�L���͂O�̏ꍇ�����A�ȊO�P�i�n�Վd�l�j
        Me.SelectSIHosyouUmu.SelectedValue = ""
        Me.HiddenHosyouKikan.Value = ""
        ' �H����А����L���ݒ�
        Me.HiddenKjGaisyaSeikyuuUmu.Value = ""

    End Sub

#End Region

#Region "[���̑����]����"

    ''' <summary>
    ''' ������Ќ����{�^���������̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonSITysGaisya_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If tyousakaisyaSearchType.Value <> "1" Then
            tyousakaisyaSearchSub(sender, e)
        Else
            '�R�[�honchange�ŌĂ΂ꂽ�ꍇ
            tyousakaisyaSearchSub(sender, e, False)
            tyousakaisyaSearchType.Value = String.Empty
        End If

    End Sub

    ''' <summary>
    ''' ������Ќ����{�^���������̏���(����)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tyousakaisyaSearchSub(ByVal sender As System.Object, ByVal e As System.EventArgs, Optional ByVal callWindow As Boolean = True)
        ' ���͂���Ă���R�[�h���L�[�ɁA�}�X�^���������A�f�[�^���擾�ł����ꍇ��
        ' ��ʏ����X�V����B�f�[�^�������ꍇ�A������ʂ�\������
        Dim tyousakaisyaSearchLogic As New TyousakaisyaSearchLogic
        Dim blnTorikesi As Boolean = True
        Dim dataArray As New List(Of TyousakaisyaSearchRecord)

        If TextSITysGaisyaCd.Text <> "" Then
            dataArray = tyousakaisyaSearchLogic.GetTyousakaisyaSearchResult(TextSITysGaisyaCd.Text, _
                                                                            "", _
                                                                            "", _
                                                                            "", _
                                                                            blnTorikesi, _
                                                                            TextKITourokuBangou.Text)
        End If

        If dataArray.Count = 1 Then
            Dim recData As TyousakaisyaSearchRecord = dataArray(0)
            TextSITysGaisyaCd.Text = recData.TysKaisyaCd & recData.JigyousyoCd
            TextSITysGaisyaMei.Text = recData.TysKaisyaMei

            Me.HiddenSITysGaisyaMae.Value = Me.TextSITysGaisyaCd.Text

            ' �������NG�ݒ�
            If recData.KahiKbn = 9 Then
                TextSITysGaisyaCd.Style("color") = "red"
                TextSITysGaisyaMei.Style("color") = "red"
            Else
                TextSITysGaisyaCd.Style("color") = "blue"
                TextSITysGaisyaMei.Style("color") = "blue"
            End If

            '�t�H�[�J�X�Z�b�g
            setFocusAJ(ButtonSITysGaisya)
        Else
            TextSITysGaisyaCd.Style("color") = "black"
            TextSITysGaisyaMei.Style("color") = "black"

            '������ЃR�[�h�����m��Ȃ̂ŃN���A
            Me.TextSITysGaisyaMei.Text = "" '������Ж�

            '������ʕ\���pJavaScript�wcallSearch�x�����s
            Dim tmpFocusScript = "objEBI('" & ButtonSITysGaisya.ClientID & "').focus();"
            Dim tmpScript = "callSearch('" & TextSITysGaisyaCd.ClientID & EarthConst.SEP_STRING & TextKITourokuBangou.ClientID & "','" & UrlConst.SEARCH_TYOUSAKAISYA & "','" & _
                                        TextSITysGaisyaCd.ClientID & EarthConst.SEP_STRING & _
                                        TextSITysGaisyaMei.ClientID & _
                                        "','" & ButtonSITysGaisya.ClientID & "');"
            If callWindow Then
                ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpFocusScript & tmpScript, True)
            ElseIf tyousakaisyaSearchType.Value = "1" Then
                ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpFocusScript, True)
            End If
        End If

    End Sub

    ''' <summary>
    ''' ���i�敪�̒l�ɂ���āA�\����؂�ւ���
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub checkSyouhinkubun()

        '���W�I�{�^���͔�\���ŏ��i�敪�͑S�ă��x����
        If Me.RadioSISyouhinKbn1.Checked Then
            ' 60�N�ۏ�
            Me.SpanSISyouhinKbn1.Style("display") = "inline"
            Me.SpanSISyouhinKbn2.Style("display") = "none"
            Me.SpanSISyouhinKbn3.Style("display") = "none"
            Me.SpanSISyouhinKbn9.Style("display") = "none"
        ElseIf Me.RadioSISyouhinKbn2.Checked Then
            ' �y�n�̔�
            Me.SpanSISyouhinKbn1.Style("display") = "none"
            Me.SpanSISyouhinKbn2.Style("display") = "inline"
            Me.SpanSISyouhinKbn3.Style("display") = "none"
            Me.SpanSISyouhinKbn9.Style("display") = "none"
        ElseIf Me.RadioSISyouhinKbn3.Checked Then
            ' ���t�H�[��
            Me.SpanSISyouhinKbn1.Style("display") = "none"
            Me.SpanSISyouhinKbn2.Style("display") = "none"
            Me.SpanSISyouhinKbn3.Style("display") = "inline"
            Me.SpanSISyouhinKbn9.Style("display") = "none"
        Else
            ' ���ݒ�͏��i�敪 9 
            Me.SpanSISyouhinKbn1.Style("display") = "none"
            Me.SpanSISyouhinKbn2.Style("display") = "none"
            Me.SpanSISyouhinKbn3.Style("display") = "none"
            Me.SpanSISyouhinKbn9.Style("display") = "inline"
        End If

    End Sub

#End Region

#Region "[�����������]����"

    ''' <summary>
    ''' �_�~�[�h���b�v�_�E�����X�g�̐���
    ''' </summary>
    ''' <param name="SelectTarget">�Ώی��h���b�v�_�E�����X�g</param>
    ''' <remarks>����M.���̎��=16�ɕR�t������M.�R�[�h=����M.���̎�ʂ̃_�~�[�h���b�v�_�E�����X�g�𐶐�����</remarks>
    Private Sub CreateDropDownList(ByRef SelectTarget As DropDownList)

        Dim helper As New DropDownHelper

        '���_�~�[�R���{�ɃZ�b�g
        Dim objDrpTmp As DropDownList

        Dim intCnt As Integer = 0
        Dim intValue As Integer

        If SelectTarget.Items.Count <= 0 Then
            Dim strMsg As String = Messages.MSG113E.Replace("@PARAM1", "���")
            Dim tmpScript As String = "alert('" & strMsg & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CreateDropDownList", tmpScript, True)

            Response.Redirect(UrlConst.MAIN)
            Exit Sub
        End If

        For intCnt = 0 To SelectTarget.Items.Count - 1
            intValue = SelectTarget.Items(intCnt).Value 'Value�l�擾

            objDrpTmp = New DropDownList
            objDrpTmp.ID = SELECT_SYUBETU_CTRL_NAME & intValue.ToString 'ID�t�^
            objDrpTmp.Style("display") = "none" '��\��
            helper.SetMeisyouDropDownList(objDrpTmp, intValue) '�l�Z�b�g

            Me.divSelect.Controls.Add(objDrpTmp) '�R���g���[���ǉ�
        Next

    End Sub

#End Region

#Region "���C��"

#Region "�{�^���C�x���g"

    ''' <summary>
    ''' �d�������m�F��ʌĂяo���{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonTyoufukuCheck_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonTyoufukuCheck.ServerClick

        '�d���m�F���ʃt���O���Z�b�g
        Me.HiddenTyoufukuKakuninFlg1.Value = Boolean.TrueString
        Me.HiddenTyoufukuKakuninFlg2.Value = Boolean.TrueString

        '�d�������m�F��ʌĂяo��
        Dim tmpFocusScript = "objEBI('" & ButtonTyoufukuCheck.ClientID & "').focus();"
        Dim tmpScript As String = "callSearch('" & Me.SelectKIKubun.ClientID & EarthConst.SEP_STRING & Me.TextKITourokuBangou.ClientID & _
                                                                EarthConst.SEP_STRING & Me.TextBukkenMeisyou.ClientID & _
                                                                EarthConst.SEP_STRING & Me.TextBukkenJyuusyo1.ClientID & EarthConst.SEP_STRING & _
                                                                Me.TextBukkenJyuusyo2.ClientID & "', '" & UrlConst.SEARCH_TYOUFUKU & "');"
        ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpFocusScript & tmpScript, True)
        Exit Sub

    End Sub

    ''' <summary>
    ''' �n��T�X�V�{�^������(�B���{�^��)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>�n��T�̍X�V������A�����������s�Ȃ�</remarks>
    Protected Sub ButtonHiddenUpdate_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonHiddenUpdate.ServerClick

        Dim tmpScript As String = ""

        '�������� >= �A��������(�S�����I��)
        If CInt(Me.HiddenSyoriKensuu.Value) >= CInt(HiddenRentouBukkenSuu.Value) Then

            '�A�������o�^�������ɂ́A���b�Z�[�W��\��
            tmpScript = "alert('" & Messages.MSG018S.Replace("@PARAM1", "�o�^") & "');"
            ScriptManager.RegisterStartupScript(sender, sender.GetType(), "ButtonHiddenUpdate_ServerClick1", tmpScript, True)

            '�{�^���̕\���ؑ�
            Me.ChgDispButton(sender)

        Else '�������� < �A��������(�������f�[�^����)

            ' ��ʂ̓��e��DB�ɔ��f����
            If SaveData() Then '�o�^����
                If actBtnId.Value = String.Empty Then
                    actBtnId.Value = Me.ButtonHiddenUpdate.ClientID
                End If

                '�A���o�^�p�t���O���Z�b�g
                HiddenCallRentouNextFlg.Value = Boolean.TrueString

            Else '�o�^���s
                tmpScript = "alert('" & Messages.MSG019E.Replace("@PARAM1", "�o�^") & "');"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonHiddenUpdate_ServerClick2", tmpScript, True)
                Exit Sub
            End If

        End If
    End Sub

    ''' <summary>
    ''' �ۏ؏�NO�����̔ԏ���(�B���{�^��)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>�̔�M���ŏI�ԍ����X�V�A�n��T�ɐV�K�ǉ����s�Ȃ��B
    ''' �G���[���Ȃ���΁A�����Ēn��T�̍X�V�������s�Ȃ�</remarks>
    Protected Sub ButtonHiddenSaiban_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonHiddenSaiban.ServerClick
        Dim kubun As String = Me.SelectKIKubun.SelectedValue
        Dim hosyousyo_no As String = ""
        Dim jBn As New Jiban '�n�Չ�ʃN���X

        Dim intRentouBukkenSuu As Integer = 1 '�A��������
        Dim strRentouBukkenSuu As String = ""
        Dim errMess As String = "" 'JS�p

        '�敪���邢�͘A���������������͂̏ꍇ�A�����𔲂���
        If kubun = "" Or HiddenRentouBukkenSuu.Value = "" Then
            '�G���[MSG
            errMess = Messages.MSG013E.Replace("@PARAM1", "�敪�A�A��������")
            MLogic.AlertMessage(sender, errMess)
            '�t�H�[�J�X�Z�b�g
            setFocusAJ(Me.ButtonTouroku1)
            Exit Sub
        End If

        '�A�����������擾
        strRentouBukkenSuu = CStr(HiddenRentouBukkenSuu.Value)
        strRentouBukkenSuu = StrConv(strRentouBukkenSuu, VbStrConv.Narrow) '�S�p�����p
        HiddenRentouBukkenSuu.Value = strRentouBukkenSuu '���p�œ��꒼��

        '���̓`�F�b�N(�A��������)
        '���o�C�g���`�F�b�N(��������̓t�B�[���h���Ώ�)
        If jBn.ByteCheckSJIS(strRentouBukkenSuu, "3") = False Then
            errMess += Messages.MSG092E.Replace("{0}", "�A��������").Replace("{1}", "3")
        End If
        '�����l�`�F�b�N
        If IsNumeric(strRentouBukkenSuu) = False Then
            errMess += Messages.MSG040E.Replace("@PARAM1", "�A��������")
        Else
            intRentouBukkenSuu = CInt(strRentouBukkenSuu)
            '�����l�͈̓`�F�b�N
            If intRentouBukkenSuu <= 0 Or intRentouBukkenSuu > 999 Then
                errMess += Messages.MSG111E.Replace("@PARAM1", "�A��������").Replace("@PARAM2", "1").Replace("@PARAM3", "999")
            End If
        End If

        '�G���[����������
        If errMess <> "" Then
            HiddenRentouBukkenSuu.Value = "" '������

            '�G���[���b�Z�[�W�\��
            MLogic.AlertMessage(sender, errMess)
            '�t�H�[�J�X�Z�b�g
            setFocusAJ(Me.ButtonTouroku1)
            Exit Sub
        End If

        '���̓`�F�b�N
        If Me.checkInput() = False Then Exit Sub

        '***************************************
        ' �����X�w�蒲����Ѓ`�F�b�N
        '***************************************
        Dim KameitenCd As String = Me.TextKITourokuBangou.Text '�����X�R�[�h
        Dim TysKaisyaCd As String = String.Empty               '������ЃR�[�h
        Dim TysKaisyaJigyousyoCd As String = String.Empty      '������Ў��Ə��R�[�h
        ' ������Г�
        Dim tmpTys As String = TextSITysGaisyaCd.Text
        If tmpTys.Length >= 6 Then   '����6�ȏ�K�{
            TysKaisyaCd = tmpTys.Substring(0, tmpTys.Length - 2) '������ЃR�[�h
            TysKaisyaJigyousyoCd = tmpTys.Substring(tmpTys.Length - 2, 2) '������Ў��Ə��R�[�h
        End If

        If Me.checkInputTysTehaiCenter(Me, KameitenCd, TysKaisyaCd, TysKaisyaJigyousyoCd) = False Then Exit Sub

        ' �n�Ճe�[�u�������o�^�����{
        If JLogic.InsertJibanData( _
                                    sender, _
                                    kubun, _
                                    hosyousyo_no, _
                                    userinfo.LoginUserId, _
                                    intRentouBukkenSuu, _
                                    EarthEnum.EnumSinkiTourokuMotoKbnType.EarthMousikomi _
                                    ) _
                                    = False Then

            '�̔Ԏ��s
            errMess = Messages.MSG019E.Replace("@PARAM1", "�̔�")
            MLogic.AlertMessage(sender, errMess)
            '�t�H�[�J�X�Z�b�g
            setFocusAJ(Me.ButtonTouroku1)
            Exit Sub
        End If

        '�ۏ؏�NO��ޔ�
        Me.HiddenSentouBangou.Value = hosyousyo_no

        '�n��T�X�V����
        actBtnId.Value = Me.ButtonTouroku1.ClientID

        ButtonHiddenUpdate_ServerClick(sender, e)

    End Sub

    ''' <summary>
    ''' ���̓`�F�b�N����(�B���{�^��)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonHiddenInputChk_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonHiddenInputChk.ServerClick

        '���̓`�F�b�N
        If checkInput() = False Then

            '�w��l�I�����̓���(��ʕ\�����p)
            checkSonota()

            '��������҂̕\���ؑ�
            checkTysTatiaisya()

            '���̓`�F�b�NNG
            Me.HiddenInputChk.Value = ""
        Else
            '���̓`�F�b�NOK
            Me.HiddenInputChk.Value = Boolean.TrueString
        End If

    End Sub

    ''' <summary>
    ''' �V�K(���p)�\��/�V�K�\�� �{�^��������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonSinki_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSinkiHikitugi.ServerClick _
                                                                                                                    , ButtonSinki.ServerClick

        '�{�^���̕\���ؑ�
        Me.ChgDispButton(sender)

    End Sub

    ''' <summary>
    ''' ��������҂̗L���ɂ���āA�\����؂�ւ���
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub checkTysTatiaisya()

        '��������҂ɑ΂���`�F�b�N�{�b�N�X�̕\���ؑ�
        Dim TatiaisyaScript As String = "ChgDispTatiaisya();"

        Me.RadioTysTatiaisya1.Attributes("onclick") = TatiaisyaScript
        Me.RadioTysTatiaisya0.Attributes("onclick") = TatiaisyaScript

        '��������҂̃��W�I�{�^�����_�u���N���b�N����ƁA�_�~�[�̃��W�I�{�^����I�����遁���`�F�b�N�����p
        Dim tmpScript As String = "objEBI('" & Me.RadioTysDummy.ClientID & "').click();"
        Me.RadioTysTatiaisya1.Attributes("ondblclick") = tmpScript & TatiaisyaScript
        Me.RadioTysTatiaisya0.Attributes("ondblclick") = tmpScript & TatiaisyaScript

        '���������
        If Me.RadioTysTatiaisya1.Checked Then '�L
            '������
            Me.CheckTysTatiaisyaSesyuSama.Disabled = False
            Me.CheckTysTatiaisyaTantousya.Disabled = False
            Me.CheckTysTatiaisyaSonota.Disabled = False
        Else '�񊈐���
            Me.CheckTysTatiaisyaSesyuSama.Disabled = True
            Me.CheckTysTatiaisyaTantousya.Disabled = True
            Me.CheckTysTatiaisyaSonota.Disabled = True
        End If

    End Sub

    ''' <summary>
    ''' �I��l���u���̑��v�̏ꍇ�A�\����؂�ւ���
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub checkSonota()
        '�w��l�I�����̓���(��ʕ\�����p)
        Dim tmpAL As New ArrayList
        tmpAL.Add(TextKouzouSyubetuSonota)
        cl.CheckVisible("9", SelectKouzouSyubetu, tmpAL)
        Dim tmpAL2 As New ArrayList
        tmpAL2.Add(TextYoteiKisoSonota)
        cl.CheckVisible("9", SelectYoteiKiso, tmpAL2)
    End Sub

#End Region

#Region "�v���C�x�[�g���\�b�h"

    ''' <summary>
    ''' ���͍��ڃ`�F�b�N
    ''' </summary>
    ''' <remarks>
    ''' �R�[�h���͒l�ύX�`�F�b�N<br/>
    ''' �K�{�`�F�b�N<br/>
    ''' �֑������`�F�b�N(��������̓t�B�[���h���Ώ�)<br/>
    ''' �o�C�g���`�F�b�N(��������̓t�B�[���h���Ώ�)<br/>
    ''' �����`�F�b�N<br/>
    ''' ���t�`�F�b�N<br/>
    ''' ���̑��`�F�b�N<br/>
    ''' </remarks>
    Public Function checkInput() As Boolean
        Dim e As New System.EventArgs

        '�n�Չ�ʋ��ʃN���X
        Dim jBn As New Jiban

        '�S�ẴR���g���[����L����(�j����ʃ`�F�b�N�p)
        Dim noTarget As New Hashtable
        jBn.ChangeDesabledAll(Me, False, noTarget)

        '�G���[���b�Z�[�W������
        Dim errMess As String = ""
        Dim arrFocusTargetCtrl As New List(Of Control)

        Dim strErrMsg As String = String.Empty '��Ɨp
        Dim blnKamentenFlg As Boolean = False
        Dim kameitenSearchLogic As New KameitenSearchLogic

        '���R�[�h���͒l�ύX�`�F�b�N
        '�o�^�ԍ�(�����X�R�[�h)
        If TextKITourokuBangou.Text <> HiddenKITourokuBangouMae.Value Or (TextKITourokuBangou.Text <> "" And Me.TextKISyamei.Text = "") Then
            errMess += Messages.MSG030E.Replace("@PARAM1", "�o�^�ԍ�")
            arrFocusTargetCtrl.Add(ButtonKITourokuBangou)
            blnKamentenFlg = True '�t���O�𗧂Ă�
        End If
        '������ЃR�[�h
        If TextSITysGaisyaCd.Text <> HiddenSITysGaisyaMae.Value Or (TextSITysGaisyaCd.Text <> "" And Me.TextSITysGaisyaMei.Text = "") Then
            errMess += Messages.MSG030E.Replace("@PARAM1", "������ЃR�[�h")
            arrFocusTargetCtrl.Add(ButtonSITysGaisya)
        End If

        '���K�{�`�F�b�N
        '<��ʍ��㕔>
        '�˗���
        If Me.TextIraiDate.Text = "" Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "�˗���")
            arrFocusTargetCtrl.Add(Me.TextIraiDate)
        End If
        '<��ʉE�㕔>
        '�敪
        If Me.SelectKIKubun.SelectedValue = "" Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "�敪")
            arrFocusTargetCtrl.Add(Me.SelectKIKubun)
        End If
        '�o�^�ԍ�(�����X�R�[�h)
        If Me.TextKITourokuBangou.Text = "" Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "�o�^�ԍ�")
            arrFocusTargetCtrl.Add(Me.TextKITourokuBangou)
            blnKamentenFlg = True '�t���O�𗧂Ă�
        End If
        '<��ʒ�����>
        '��������
        If Me.TextBukkenMeisyou.Text = "" Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "��������")
            arrFocusTargetCtrl.Add(Me.TextBukkenMeisyou)
        End If
        '�{�喼�L��
        If Me.RadioSesyumei0.Checked = False AndAlso Me.RadioSesyumei1.Checked = False Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "�{�喼�L��")
            arrFocusTargetCtrl.Add(Me.RadioSesyumei1)
        End If
        '���񓯎��˗�����
        If Me.TextDoujiIraiTousuu.Text = "" Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "���񓯎��˗�����")
            arrFocusTargetCtrl.Add(Me.TextDoujiIraiTousuu)
        End If
        '�����Z��1
        If Me.TextBukkenJyuusyo1.Text = "" Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "�����Z��1")
            arrFocusTargetCtrl.Add(Me.TextBukkenJyuusyo1)
        End If
        '������]��
        If Me.TextTyousaKibouDate.Text = "" Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "������]��")
            arrFocusTargetCtrl.Add(Me.TextTyousaKibouDate)
        End If
        '<��ʉ���>
        '���i1
        If Me.choSyouhin1.SelectedValue = "" Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "���i1")
            arrFocusTargetCtrl.Add(Me.choSyouhin1)
        End If

        '�����t�`�F�b�N
        '<��ʒ�����>
        '�˗���
        If Me.TextIraiDate.Text <> "" Then
            If cl.checkDateHanni(Me.TextIraiDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "�˗���")
                arrFocusTargetCtrl.Add(Me.TextIraiDate)
            End If
        End If
        '������]��
        If Me.TextTyousaKibouDate.Text <> "" Then
            If cl.checkDateHanni(Me.TextTyousaKibouDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "������]��")
                arrFocusTargetCtrl.Add(Me.TextTyousaKibouDate)
            End If
        End If
        '��b���H�\���FROM
        If Me.TextKsTyakkouYoteiDateFrom.Text <> "" Then
            If cl.checkDateHanni(Me.TextKsTyakkouYoteiDateFrom.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "��b���H�\���FROM")
                arrFocusTargetCtrl.Add(Me.TextKsTyakkouYoteiDateFrom)
            End If
        End If
        '��b���H�\���TO
        If Me.TextKsTyakkouYoteiDateTo.Text <> "" Then
            If cl.checkDateHanni(Me.TextKsTyakkouYoteiDateTo.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "��b���H�\���TO")
                arrFocusTargetCtrl.Add(Me.TextKsTyakkouYoteiDateTo)
            End If
        End If

        '���֑������`�F�b�N(��������̓t�B�[���h���Ώ�)
        '<��ʍ��㕔>
        '�����A����.�Z��
        If jBn.KinsiStrCheck(Me.TextTysJyuusyo.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "�����A����F�Z��")
            arrFocusTargetCtrl.Add(Me.TextTysJyuusyo)
        End If
        '�����A����.TEL
        If jBn.KinsiStrCheck(Me.TextTysTel.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "�����A����FTEL")
            arrFocusTargetCtrl.Add(Me.TextTysTel)
        End If
        '�����A����.FAX
        If jBn.KinsiStrCheck(Me.TextTysFax.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "�����A����FFAX")
            arrFocusTargetCtrl.Add(Me.TextTysFax)
        End If
        '<��ʉE�㕔>
        '�_��NO
        If jBn.KinsiStrCheck(Me.TextKeiyakuNo.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "�_��NO")
            arrFocusTargetCtrl.Add(Me.TextKeiyakuNo)
        End If
        '�����X.�S����
        If jBn.KinsiStrCheck(Me.TextKITantousya.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "�����X�F�S����")
            arrFocusTargetCtrl.Add(Me.TextKITantousya)
        End If
        '<��ʒ�����>
        '��������
        If jBn.KinsiStrCheck(Me.TextBukkenMeisyou.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "��������")
            arrFocusTargetCtrl.Add(Me.TextBukkenMeisyou)
        End If
        '�����Z��1
        If jBn.KinsiStrCheck(Me.TextBukkenJyuusyo1.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "�����Z��1")
            arrFocusTargetCtrl.Add(Me.TextBukkenJyuusyo1)
        End If
        '�����Z��2
        If jBn.KinsiStrCheck(Me.TextBukkenJyuusyo2.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "�����Z��2")
            arrFocusTargetCtrl.Add(Me.TextBukkenJyuusyo2)
        End If
        '�����Z��3
        If jBn.KinsiStrCheck(Me.TextBukkenJyuusyo3.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "�����Z��3")
            arrFocusTargetCtrl.Add(Me.TextBukkenJyuusyo3)
        End If
        '������]����
        If jBn.KinsiStrCheck(Me.TextTyousaKibouJikan.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "������]����")
            arrFocusTargetCtrl.Add(Me.TextTyousaKibouJikan)
        End If
        '�\�����̑�
        If jBn.KinsiStrCheck(Me.TextKouzouSyubetuSonota.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "�\�����̑�")
            arrFocusTargetCtrl.Add(Me.TextKouzouSyubetuSonota)
        End If
        '�\���b���̑�
        If jBn.KinsiStrCheck(Me.TextYoteiKisoSonota.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "�\���b���̑�")
            arrFocusTargetCtrl.Add(Me.TextYoteiKisoSonota)
        End If
        '<���̑����>
        '���l
        If jBn.KinsiStrCheck(Me.TextSIBikou.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "���l")
            arrFocusTargetCtrl.Add(Me.TextSIBikou)
        End If
        '���l2
        If jBn.KinsiStrCheck(Me.TextSIBikou2.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "���l2")
            arrFocusTargetCtrl.Add(Me.TextSIBikou2)
        End If
        '��������R�[�h
        If jBn.KinsiStrCheck(Me.TextBukkenNayoseCd.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "��������R�[�h")
            arrFocusTargetCtrl.Add(Me.TextBukkenNayoseCd)
        End If

        '���s�ϊ�
        '<���̑����>
        If Me.TextSIBikou.Value <> "" Then
            Me.TextSIBikou.Value = Me.TextSIBikou.Value.Replace(vbCrLf, " ")
        End If
        If Me.TextSIBikou2.Value <> "" Then
            Me.TextSIBikou2.Value = Me.TextSIBikou2.Value.Replace(vbCrLf, " ")
        End If

        '���o�C�g���`�F�b�N(��������̓t�B�[���h���Ώ�)
        '<��ʏ㕔>
        '�����A����F�Z��
        If jBn.ByteCheckSJIS(Me.TextTysJyuusyo.Text, Me.TextTysJyuusyo.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "�����A����F�Z��")
            arrFocusTargetCtrl.Add(Me.TextTysJyuusyo)
        End If
        '�����A����FTEL
        If jBn.ByteCheckSJIS(Me.TextTysTel.Text, Me.TextTysTel.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "�����A����FTEL")
            arrFocusTargetCtrl.Add(Me.TextTysTel)
        End If
        '�����A����FFAX
        If jBn.ByteCheckSJIS(Me.TextTysFax.Text, Me.TextTysFax.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "�����A����FFAX")
            arrFocusTargetCtrl.Add(Me.TextTysFax)
        End If
        '�_��NO
        If jBn.ByteCheckSJIS(Me.TextKeiyakuNo.Text, Me.TextKeiyakuNo.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "�_��NO")
            arrFocusTargetCtrl.Add(Me.TextKeiyakuNo)
        End If
        '�����X�F�S����
        If jBn.ByteCheckSJIS(Me.TextKITantousya.Text, Me.TextKITantousya.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "�����X�F�S����")
            arrFocusTargetCtrl.Add(Me.TextKITantousya)
        End If
        '<��ʒ�����>
        '��������
        If jBn.ByteCheckSJIS(Me.TextBukkenMeisyou.Text, Me.TextBukkenMeisyou.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "��������")
            arrFocusTargetCtrl.Add(Me.TextBukkenMeisyou)
        End If
        '�����Z��1
        If jBn.ByteCheckSJIS(Me.TextBukkenJyuusyo1.Text, Me.TextBukkenJyuusyo1.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "�����Z��1")
            arrFocusTargetCtrl.Add(Me.TextBukkenJyuusyo1)
        End If
        '�����Z��2
        If jBn.ByteCheckSJIS(Me.TextBukkenJyuusyo2.Text, Me.TextBukkenJyuusyo2.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "�����Z��2")
            arrFocusTargetCtrl.Add(Me.TextBukkenJyuusyo2)
        End If
        '�����Z��3
        If jBn.ByteCheckSJIS(Me.TextBukkenJyuusyo3.Text, Me.TextBukkenJyuusyo3.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "�����Z��3")
            arrFocusTargetCtrl.Add(Me.TextBukkenJyuusyo3)
        End If
        '������]����
        If jBn.ByteCheckSJIS(Me.TextTyousaKibouJikan.Text, Me.TextTyousaKibouJikan.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "������]����")
            arrFocusTargetCtrl.Add(Me.TextTyousaKibouJikan)
        End If
        '�\�����̑�
        If jBn.ByteCheckSJIS(Me.TextKouzouSyubetuSonota.Text, Me.TextKouzouSyubetuSonota.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "�\�����̑�")
            arrFocusTargetCtrl.Add(Me.TextKouzouSyubetuSonota)
        End If
        '�\���b���̑�
        If jBn.ByteCheckSJIS(Me.TextYoteiKisoSonota.Text, Me.TextYoteiKisoSonota.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "�\���b���̑�")
            arrFocusTargetCtrl.Add(Me.TextYoteiKisoSonota)
        End If
        '<���̑����>
        '���l
        If jBn.ByteCheckSJIS(Me.TextSIBikou.Value, 256) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "���l")
            arrFocusTargetCtrl.Add(Me.TextSIBikou)
        End If
        '���l2
        If jBn.ByteCheckSJIS(Me.TextSIBikou2.Value, 512) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "���l2")
            arrFocusTargetCtrl.Add(Me.TextSIBikou2)
        End If
        '��������R�[�h
        If jBn.ByteCheckSJIS(Me.TextBukkenNayoseCd.Value, Me.TextBukkenNayoseCd.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "��������R�[�h")
            arrFocusTargetCtrl.Add(Me.TextBukkenNayoseCd)
        End If

        '�������`�F�b�N
        '<��ʒ�����>
        '���񓯎��˗�����
        If jBn.SuutiStrCheck(Me.TextDoujiIraiTousuu.Text, 4, 0) = False Then
            errMess += Messages.MSG027E.Replace("@PARAM1", "���񓯎��˗�����").Replace("@PARAM2", "4").Replace("@PARAM3", "0")
            arrFocusTargetCtrl.Add(Me.TextDoujiIraiTousuu)
        End If
        '�݌v���e�x����
        If jBn.SuutiStrCheck(Me.TextSekkeiKyoyouSijiryoku.Text, 4, 1) = False Then
            errMess += Messages.MSG027E.Replace("@PARAM1", "�݌v���e�x����").Replace("@PARAM2", "4").Replace("@PARAM3", "1")
            arrFocusTargetCtrl.Add(Me.TextSekkeiKyoyouSijiryoku)
        End If
        '�˗��\�蓏��
        If jBn.SuutiStrCheck(Me.TextIraiYoteiTousuu.Text, 4, 0) = False Then
            errMess += Messages.MSG027E.Replace("@PARAM1", "�˗��\�蓏��").Replace("@PARAM2", "4").Replace("@PARAM3", "0")
            arrFocusTargetCtrl.Add(Me.TextIraiYoteiTousuu)
        End If
        '���؂�[��
        If jBn.SuutiStrCheck(Me.TextNegiriHukasa.Text, 8, 4) = False Then
            errMess += Messages.MSG027E.Replace("@PARAM1", "���؂�[��").Replace("@PARAM2", "8").Replace("@PARAM3", "4")
            arrFocusTargetCtrl.Add(Me.TextNegiriHukasa)
        End If
        '�\�萷�y����
        If jBn.SuutiStrCheck(Me.TextYoteiMoritutiAtusa.Text, 9, 3) = False Then
            errMess += Messages.MSG027E.Replace("@PARAM1", "�\�萷�y����").Replace("@PARAM2", "9").Replace("@PARAM3", "3")
            arrFocusTargetCtrl.Add(Me.TextYoteiMoritutiAtusa)
        End If
        '<��ʉ���>
        '�ː�
        If jBn.SuutiStrCheck(Me.TextSIKosuu.Text, 4, 0) = False Then
            errMess += Messages.MSG027E.Replace("@PARAM1", "�ː�").Replace("@PARAM2", "4").Replace("@PARAM3", "0")
            arrFocusTargetCtrl.Add(Me.TextSIKosuu)
        End If

        '�����̑��`�F�b�N
        '(Chk02:���t����.TO�̒l���A���t����.FROM�̒l���ȑO�̏ꍇ�A�G���[�Ƃ���B)
        '�o�^�������s�O�ɁAJS�Ń`�F�b�N�ς�
        '��b���H�\���FROM�ATO�`�F�b�N
        If Me.TextKsTyakkouYoteiDateFrom.Text <> String.Empty And Me.TextKsTyakkouYoteiDateTo.Text <> String.Empty Then
            If Me.TextKsTyakkouYoteiDateFrom.Text > Me.TextKsTyakkouYoteiDateTo.Text Then
                errMess += Messages.MSG022E.Replace("@PARAM1", "��b���H�\���")
                arrFocusTargetCtrl.Add(Me.TextKsTyakkouYoteiDateFrom)
            End If
        End If

        '(Chk03:���.�敪�Ɖ����XM.�敪���s��v�̏ꍇ�A�G���[�Ƃ���B(�L�[�F���.�敪�������XM.�敪 AND ���.�o�^�ԍ��������XM.�����X�R�[�h)
        '�敪�A�o�^�ԍ�(�����X�R�[�h)
        ' ���͂���Ă���R�[�h���L�[�ɁA�}�X�^���������A�f�[�^��1���̂ݎ擾�ł����ꍇ��
        ' ��ʏ����X�V����B�f�[�^�������ꍇ�́A������ʂ�\������
        Dim dataArray As New List(Of KameitenSearchRecord)
        Dim blnTorikesi As Boolean = False
        Dim total_count As Integer = 0

        ' �擾�������i�荞�ޏꍇ�A������ǉ����Ă�������
        If Me.SelectKIKubun.SelectedValue <> "" And Me.TextKITourokuBangou.Text <> "" Then
            dataArray = kameitenSearchLogic.GetKameitenSearchResult(SelectKIKubun.SelectedValue, _
                                                                    TextKITourokuBangou.Text, _
                                                                    blnTorikesi, _
                                                                    total_count)

            If total_count = 1 Then '����
            Else '�ُ�
                errMess += Messages.MSG120E.Replace("@PARAM1", "�敪").Replace("@PARAM2", "�����X�R�[�h")
                arrFocusTargetCtrl.Add(Me.SelectKIKubun)
            End If

        End If

        '�������T�v/�����˗������`�F�b�N
        strErrMsg = String.Empty
        If cbLogic.ChkErrTysGaiyou(Me.SelectSITysGaiyou.SelectedValue, Me.TextDoujiIraiTousuu.Text, strErrMsg) = False Then
            errMess += strErrMsg
            arrFocusTargetCtrl.Add(Me.TextDoujiIraiTousuu)
        End If
        '���r���_�[���ӎ����`�F�b�N(�����X�֘A�̃G���[���Ȃ��ꍇ�`�F�b�N����)
        strErrMsg = String.Empty
        If blnKamentenFlg = False Then
            If kameitenSearchLogic.ChkBuilderData13(Me.TextKITourokuBangou.Text) Then
                Me.HiddenKameitenTyuuiJikou.Value = Boolean.TrueString
            Else
                Me.HiddenKameitenTyuuiJikou.Value = Boolean.FalseString
            End If
            If cbLogic.ChkErrBuilderData(Me.SelectSITysGaiyou.SelectedValue, Me.TextKITourokuBangou.Text, Me.HiddenKameitenTyuuiJikou.Value, strErrMsg) = False Then
                errMess += strErrMsg
                arrFocusTargetCtrl.Add(Me.TextKITourokuBangou)
            End If
        End If

        '�d�������`�F�b�N
        '�d���������莞�A�d�������|�b�v�A�b�v�ɂĊm�F���Ă��Ȃ��ꍇ
        If Me.HiddenTyoufukuKakuninFlg1.Value <> Boolean.TrueString Or _
           Me.HiddenTyoufukuKakuninFlg2.Value <> Boolean.TrueString Then
            errMess += Messages.MSG017E
            arrFocusTargetCtrl.Add(Me.ButtonTyoufukuCheck)
        End If

        '�������������̓��̓`�F�b�N
        If Me.checkInputBR(errMess, arrFocusTargetCtrl, emBrCtrl.intCtrl1) = False Then
            ' �G���[���b�Z�[�W���ǉ����ꂽ�̂ŕ�����������W�J����
            Me.TBodyBRInfo.Attributes("style") = "display:inline;"
        End If
        If Me.checkInputBR(errMess, arrFocusTargetCtrl, emBrCtrl.intCtrl2) = False Then
            ' �G���[���b�Z�[�W���ǉ����ꂽ�̂ŕ�����������W�J����
            Me.TBodyBRInfo.Attributes("style") = "display:inline;"
        End If
        If Me.checkInputBR(errMess, arrFocusTargetCtrl, emBrCtrl.intCtrl3) = False Then
            ' �G���[���b�Z�[�W���ǉ����ꂽ�̂ŕ�����������W�J����
            Me.TBodyBRInfo.Attributes("style") = "display:inline;"
        End If

        '����NO���擾
        Dim strBukkenNo As String = String.Empty '����NO�͂��̎��_�Ŗ�����
        Dim strBukkenNayoseCd As String = Me.TextBukkenNayoseCd.Value.ToUpper
        Dim blnBukkenNoFlg As Boolean = True

        '��������R�[�h
        If strBukkenNayoseCd = String.Empty Then '������
            strBukkenNayoseCd = strBukkenNo '�����͂̏ꍇ�A������NO���Z�b�g
        End If

        '��������R�[�h(���͕s���`�F�b�N)
        If Me.TextBukkenNayoseCd.Value <> String.Empty Then
            '11���̃`�F�b�N
            If sLogic.GetStrByteSJIS(Me.TextBukkenNayoseCd.Value) = Me.TextBukkenNayoseCd.MaxLength Then
            Else
                blnBukkenNoFlg = False

                errMess += Messages.MSG040E.Replace("@PARAM1", "��������R�[�h") & "�y�敪+�ۏ؏�NO(�ԍ�)�z\r\n"
                arrFocusTargetCtrl.Add(Me.TextBukkenNayoseCd)
            End If
        End If

        If blnBukkenNoFlg Then
            '�����������w�肵�Ă���ꍇ�̂݃`�F�b�N
            If strBukkenNayoseCd <> String.Empty Then
                '����悪�e�������̃`�F�b�N
                If JLogic.ChkBukkenNayoseOyaBukken(strBukkenNo, strBukkenNayoseCd) = False Then
                    errMess += Messages.MSG167E.Replace("@PARAM1", "�����̕���").Replace("@PARAM2", "�q����").Replace("@PARAM3", "��������R�[�h")
                    arrFocusTargetCtrl.Add(Me.TextBukkenNayoseCd)
                End If

                '�������̖���󋵃`�F�b�N������ʂł͕s�v
            End If
        End If

        '�G���[�������͉�ʍĕ`��̂��߁A�R���g���[���̗L��������ؑւ���
        If errMess <> "" Then
            Dim tmpScript As String = ""

            '�t�H�[�J�X�Z�b�g
            If arrFocusTargetCtrl.Count > 0 Then
                arrFocusTargetCtrl(0).Focus()
            End If
            '�G���[���b�Z�[�W�\��
            tmpScript = "alert('" & errMess & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            Return False
        End If

        '�G���[�����̏ꍇ�ATrue��Ԃ�
        Return True

    End Function

    ''' <summary>
    ''' ���͍��ڃ`�F�b�N(�����������)
    ''' </summary>
    ''' <remarks>
    ''' �R�[�h���͒l�ύX�`�F�b�N<br/>
    ''' �K�{�`�F�b�N<br/>
    ''' �֑������`�F�b�N(��������̓t�B�[���h���Ώ�)<br/>
    ''' �o�C�g���`�F�b�N(��������̓t�B�[���h���Ώ�)<br/>
    ''' �����`�F�b�N<br/>
    ''' ���t�`�F�b�N<br/>
    ''' ���̑��`�F�b�N<br/>
    ''' </remarks>
    Public Function checkInputBR( _
                          ByRef errMess As String _
                          , ByRef arrFocusTargetCtrl As List(Of Control) _
                          , ByVal emBrCtrl As emBrCtrl _
                          ) As Boolean

        '�n�Չ�ʋ��ʃN���X
        Dim jBn As New Jiban

        '�G���[���b�Z�[�W������
        Dim strErrMsg As String = errMess '��r�p
        Dim strSettouji As String = String.Empty

        '�`�F�b�N��Ɨp
        Dim objSelectBRSyubetu As New DropDownList
        Dim objHiddenBRBunrui As New HtmlInputHidden
        Dim objTextBRHizuke As New TextBox
        Dim objTextBRHanyouCd As New TextBox
        Dim objTextAreaBRNaiyou As New HtmlTextArea

        Select Case emBrCtrl
            Case MousikomiInput.emBrCtrl.intCtrl1
                strSettouji = "���������P�F"

                objSelectBRSyubetu = Me.SelectBRSyubetu1
                objHiddenBRBunrui = Me.HiddenBRBunrui1
                objTextBRHizuke = Me.TextBRHizuke1
                objTextBRHanyouCd = Me.TextBRHanyouCd1
                objTextAreaBRNaiyou = Me.TextAreaBRNaiyou1

            Case MousikomiInput.emBrCtrl.intCtrl2
                strSettouji = "���������Q�F"

                objSelectBRSyubetu = Me.SelectBRSyubetu2
                objHiddenBRBunrui = Me.HiddenBRBunrui2
                objTextBRHizuke = Me.TextBRHizuke2
                objTextBRHanyouCd = Me.TextBRHanyouCd2
                objTextAreaBRNaiyou = Me.TextAreaBRNaiyou2

            Case MousikomiInput.emBrCtrl.intCtrl3
                strSettouji = "���������R�F"

                objSelectBRSyubetu = Me.SelectBRSyubetu3
                objHiddenBRBunrui = Me.HiddenBRBunrui3
                objTextBRHizuke = Me.TextBRHizuke3
                objTextBRHanyouCd = Me.TextBRHanyouCd3
                objTextAreaBRNaiyou = Me.TextAreaBRNaiyou3

            Case Else
                '�`�F�b�N�ΏۊO�Ƃ��ď����𔲂���
                Return True
        End Select

        '���R�[�h���͒l�ύX�`�F�b�N
        '�Ȃ�

        '���K�{�`�F�b�N
        '��ʁA����
        If objSelectBRSyubetu.SelectedValue <> String.Empty And objHiddenBRBunrui.Value = String.Empty _
            Or objSelectBRSyubetu.SelectedValue = String.Empty And objHiddenBRBunrui.Value <> String.Empty Then
            errMess += strSettouji & Messages.MSG013E.Replace("@PARAM1", "��ʂƕ���")
            arrFocusTargetCtrl.Add(objSelectBRSyubetu)
        End If
        '��ʁA���ށ������͂ł��A�ȊO�����͂̏ꍇ
        If objSelectBRSyubetu.SelectedValue = String.Empty And objHiddenBRBunrui.Value = String.Empty Then
            If objTextBRHizuke.Text <> String.Empty _
                Or objTextBRHanyouCd.Text <> String.Empty _
                    Or objTextAreaBRNaiyou.Value <> String.Empty Then

                errMess += strSettouji & Messages.MSG013E.Replace("@PARAM1", "��ʂƕ���")
                arrFocusTargetCtrl.Add(objSelectBRSyubetu)
            End If
        End If

        '�����t�`�F�b�N
        '(�ėp)���t
        If objTextBRHizuke.Text <> String.Empty Then
            If cl.checkDateHanni(objTextBRHizuke.Text) = False Then
                errMess += strSettouji & Messages.MSG014E.Replace("@PARAM1", "���t")
                arrFocusTargetCtrl.Add(objTextBRHizuke)
            End If
        End If

        '���֑������`�F�b�N(��������̓t�B�[���h���Ώ�)
        '�ėp�R�[�h
        If jBn.KinsiStrCheck(objTextBRHanyouCd.Text) = False Then
            errMess += strSettouji & Messages.MSG015E.Replace("@PARAM1", "�ėp�R�[�h")
            arrFocusTargetCtrl.Add(objTextBRHanyouCd)
        End If
        '���e
        If jBn.KinsiStrCheck(objTextAreaBRNaiyou.Value) = False Then
            errMess += strSettouji & Messages.MSG015E.Replace("@PARAM1", "���e")
            arrFocusTargetCtrl.Add(objTextAreaBRNaiyou)
        End If

        '���s�ϊ�
        If objTextAreaBRNaiyou.Value <> "" Then
            objTextAreaBRNaiyou.Value = objTextAreaBRNaiyou.Value.Replace(vbCrLf, " ")
        End If

        '���o�C�g���`�F�b�N(��������̓t�B�[���h���Ώ�)
        '�ėp�R�[�h
        If jBn.ByteCheckSJIS(objTextBRHanyouCd.Text, objTextBRHanyouCd.MaxLength) = False Then
            errMess += strSettouji & Messages.MSG016E.Replace("@PARAM1", "�ėp�R�[�h")
            arrFocusTargetCtrl.Add(objTextBRHanyouCd)
        End If
        '���e
        If jBn.ByteCheckSJIS(objTextAreaBRNaiyou.Value, 512) = False Then
            errMess += strSettouji & Messages.MSG016E.Replace("@PARAM1", "���e")
            arrFocusTargetCtrl.Add(objTextAreaBRNaiyou)
        End If

        '�������`�F�b�N
        '�Ȃ�

        '�����̑��`�F�b�N
        '�Ȃ�

        If errMess <> strErrMsg Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' ���͍��ڃ`�F�b�N(������z�Z���^�[)
    ''' </summary>
    ''' <remarks>
    ''' �R�[�h���͒l�ύX�`�F�b�N<br/>
    ''' �K�{�`�F�b�N<br/>
    ''' �֑������`�F�b�N(��������̓t�B�[���h���Ώ�)<br/>
    ''' �o�C�g���`�F�b�N(��������̓t�B�[���h���Ώ�)<br/>
    ''' �����`�F�b�N<br/>
    ''' ���t�`�F�b�N<br/>
    ''' ���̑��`�F�b�N<br/>
    ''' </remarks>
    Public Function checkInputTysTehaiCenter(ByVal sender As Object, ByVal KameitenCd As String, ByVal TysKaisyaCd As String, ByVal TysKaisyaJigyousyoCd As String) As Boolean
        Dim e As New System.EventArgs

        '�n�Չ�ʋ��ʃN���X
        Dim jBn As New Jiban
        Dim logic As New MousikomiInputLogic

        '�S�ẴR���g���[����L����(�j����ʃ`�F�b�N�p)
        Dim noTarget As New Hashtable
        jBn.ChangeDesabledAll(Me, False, noTarget)

        '�G���[���b�Z�[�W������
        Dim errMess As String = ""
        Dim arrFocusTargetCtrl As New List(Of Control)

        Dim strTysTehai As String = String.Empty

        strTysTehai = cbLogic.ChkExistKameitenTysTehaiCenter(KameitenCd, TysKaisyaCd & TysKaisyaJigyousyoCd)
        If strTysTehai <> String.Empty Then
            errMess += Messages.MSG206E.Replace("@PARAM1", strTysTehai)
            arrFocusTargetCtrl.Add(Me.TextSITysGaisyaCd)
        End If

        '�G���[�������͉�ʍĕ`��̂��߁A�R���g���[���̗L��������ؑւ���
        If errMess <> "" Then
            Dim tmpScript As String = ""

            '�t�H�[�J�X�Z�b�g
            If arrFocusTargetCtrl.Count > 0 Then
                arrFocusTargetCtrl(0).Focus()
            End If
            '�G���[���b�Z�[�W�\��
            tmpScript = "alert('" & errMess & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            Return False
        End If

        '�G���[�����̏ꍇ�ATrue��Ԃ�
        Return True

    End Function

    ''' <summary>
    ''' ��ʂ̓��e��DB�ɔ��f����
    ''' </summary>
    ''' <remarks></remarks>
    Protected Function SaveData() As Boolean

        ' ��ʓ��e���n�Ճ��R�[�h�𐶐�����
        Dim jibanRec As New JibanRecordMousikomiInput

        ' �@�ʃf�[�^�C���p�̃��W�b�N�N���X
        Dim logic As New MousikomiInputLogic

        Dim dtNow As DateTime = DateTime.Now
        '�G���[���b�Z�[�W������
        Dim errMess As String = ""

        '***************************************
        ' �n�Ճf�[�^
        '***************************************
        ' �敪
        cl.SetDisplayString(SelectKIKubun.SelectedValue, jibanRec.Kbn)
        ' �ԍ��i�ۏ؏�NO�j
        jibanRec.HosyousyoNo = CStr(Me.HiddenSentouBangou.Value)
        ' �{�喼
        cl.SetDisplayString(TextBukkenMeisyou.Text, jibanRec.SesyuMei)
        ' �󒍕�����
        cl.SetDisplayString(TextBukkenMeisyou.Text, jibanRec.JyutyuuBukkenMei)
        ' �����Z��1
        cl.SetDisplayString(TextBukkenJyuusyo1.Text, jibanRec.BukkenJyuusyo1)
        ' �����Z��2
        cl.SetDisplayString(TextBukkenJyuusyo2.Text, jibanRec.BukkenJyuusyo2)
        ' �����Z��3
        cl.SetDisplayString(TextBukkenJyuusyo3.Text, jibanRec.BukkenJyuusyo3)
        '��������R�[�h
        If Me.TextBukkenNayoseCd.Value = String.Empty Then
            jibanRec.BukkenNayoseCdFlg = True '���W�b�N�N���X�ŃZ�b�g
        Else
            jibanRec.BukkenNayoseCdFlg = False
            jibanRec.BukkenNayoseCd = Me.TextBukkenNayoseCd.Value.ToUpper  '���.��������R�[�h
        End If
        ' �����X�R�[�h
        cl.SetDisplayString(TextKITourokuBangou.Text, jibanRec.KameitenCd)
        ' ���i�敪
        If RadioSISyouhinKbn1.Checked Then '60�N�ۏ�
            jibanRec.SyouhinKbn = RadioSISyouhinKbn1.Value
        ElseIf RadioSISyouhinKbn2.Checked Then '�y�n�̔�
            jibanRec.SyouhinKbn = RadioSISyouhinKbn2.Value
        ElseIf RadioSISyouhinKbn3.Checked Then '���t�H�[��
            jibanRec.SyouhinKbn = RadioSISyouhinKbn3.Value
        Else '���̑�
            jibanRec.SyouhinKbn = RadioSISyouhinKbn9.Value
        End If
        ' ���l �����s�R�[�h�͕ϊ��ς݂̂���
        cl.SetDisplayString(TextSIBikou.Value, jibanRec.Bikou)
        ' ���l2 �����s�R�[�h�͕ϊ��ς݂̂���
        cl.SetDisplayString(TextSIBikou2.Value, jibanRec.Bikou2)
        ' �˗���
        cl.SetDisplayString(TextIraiDate.Text, jibanRec.IraiDate)
        ' �˗��S����
        cl.SetDisplayString(TextKITantousya.Text, jibanRec.IraiTantousyaMei)
        ' �_��NO
        cl.SetDisplayString(TextKeiyakuNo.Text, jibanRec.KeiyakuNo)
        ' �K�w
        cl.SetDisplayString(Me.SelectKaisou.SelectedValue, jibanRec.Kaisou)
        ' �V�z����
        cl.SetDisplayString(Me.SelectSintikuTatekae.SelectedValue, jibanRec.SintikuTatekae)
        ' �\�����
        cl.SetDisplayString(Me.SelectKouzouSyubetu.SelectedValue, jibanRec.Kouzou)
        ' �\�����MEMO(���̑�)
        cl.SetDisplayString(TextKouzouSyubetuSonota.Text, jibanRec.KouzouMemo)
        ' �Ԍ�(�n���ԌɌv��)
        cl.SetDisplayString(Me.SelectTikaSyakoKeikaku.SelectedValue, jibanRec.Syako)
        ' ���؂�[��
        cl.SetDisplayString(TextNegiriHukasa.Text, jibanRec.NegiriHukasa)
        ' �\�萷�y����
        If TextYoteiMoritutiAtusa.Text <> String.Empty Then
            cl.SetDisplayString(TextYoteiMoritutiAtusa.Text / 10, jibanRec.YoteiMoritutiAtusa)
        Else
            cl.SetDisplayString(TextYoteiMoritutiAtusa.Text, jibanRec.YoteiMoritutiAtusa)
        End If
        ' �\���b
        cl.SetDisplayString(Me.SelectYoteiKiso.SelectedValue, jibanRec.YoteiKs)
        ' �\���bMEMO(���̑�)
        cl.SetDisplayString(TextYoteiKisoSonota.Text, jibanRec.YoteiKsMemo)
        ' ������Г�
        Dim tmpTys As String = TextSITysGaisyaCd.Text
        If tmpTys.Length >= 6 Then   '����6�ȏ�K�{
            jibanRec.TysKaisyaCd = tmpTys.Substring(0, tmpTys.Length - 2) '������ЃR�[�h
            jibanRec.TysKaisyaJigyousyoCd = tmpTys.Substring(tmpTys.Length - 2, 2) '������Ў��Ə��R�[�h
        End If
        ' �������@
        cl.SetDisplayString(Me.SelectSITysHouhou.SelectedValue, jibanRec.TysHouhou)
        ' �����T�v
        cl.SetDisplayString(Me.SelectSITysGaiyou.SelectedValue, jibanRec.TysGaiyou)
        ' ���i�R�[�h
        cl.SetDisplayString(Me.choSyouhin1.SelectedValue, jibanRec.SyouhinCd1)
        ' ������]��
        cl.SetDisplayString(TextTyousaKibouDate.Text, jibanRec.TysKibouDate)
        ' ������]����
        cl.SetDisplayString(TextTyousaKibouJikan.Text, jibanRec.TysKibouJikan)
        ' ����L��
        If RadioTysTatiaisya0.Checked Then '��
            cl.SetDisplayString(0, jibanRec.TatiaiUmu)
        ElseIf RadioTysTatiaisya1.Checked Then '�L
            cl.SetDisplayString(1, jibanRec.TatiaiUmu)
        Else
            cl.SetDisplayString(Integer.MinValue, jibanRec.TatiaiUmu)
        End If
        ' ����҃R�[�h
        cl.SetDisplayString(cl.GetTatiaiCd(Me.CheckTysTatiaisyaSesyuSama, Me.CheckTysTatiaisyaTantousya, Me.CheckTysTatiaisyaSonota), jibanRec.TatiaisyaCd)
        '�ۏ؏����s��
        cl.SetDisplayString(Integer.MinValue, jibanRec.HosyousyoHakJyky)
        '�ۏ؏����s�󋵐ݒ��
        cl.SetDisplayString(DateTime.MinValue, jibanRec.HosyousyoHakJykySetteiDate)
        ' �ۏؗL��
        cl.SetDisplayString(SelectSIHosyouUmu.SelectedValue, jibanRec.HosyouUmu)
        ' �ۏ؊���
        cl.SetDisplayString(HiddenHosyouKikan.Value, jibanRec.HosyouKikan)
        ' �X�V����
        cl.SetDisplayString(dtNow, jibanRec.UpdDatetime)
        ' �����˗�����
        cl.SetDisplayString(TextDoujiIraiTousuu.Text, jibanRec.DoujiIraiTousuu)
        ' ���r�L��
        cl.SetDisplayString(SelectSITatemonoKensa.SelectedValue, jibanRec.KasiUmu)
        ' �H����А����L��
        Dim intTmp As Integer = IIf(HiddenKjGaisyaSeikyuuUmu.Value = "1", HiddenKjGaisyaSeikyuuUmu.Value, Integer.MinValue)
        cl.SetDisplayString(intTmp, jibanRec.KojGaisyaSeikyuuUmu)
        ' �o�R
        jibanRec.Keiyu = IIf(SelectSIKeiyu.SelectedValue = "", 0, SelectSIKeiyu.SelectedValue)
        ' �݌v���e�x����
        cl.SetDisplayString(TextSekkeiKyoyouSijiryoku.Text, jibanRec.SekkeiKyoyouSijiryoku)
        ' �˗��\�蓏��
        cl.SetDisplayString(TextIraiYoteiTousuu.Text, jibanRec.IraiYoteiTousuu)
        ' �����p�rNO
        cl.SetDisplayString(Me.SelectTatemonoYouto.SelectedValue, jibanRec.TatemonoYoutoNo)
        ' �ː�
        cl.SetDisplayString(TextSIKosuu.Text, jibanRec.Kosuu)
        ' �X�V��
        jibanRec.Kousinsya = cbLogic.GetKousinsya(userinfo.LoginUserId, dtNow)

        '******************
        '* �����A����
        '******************
        ' �����A����_����
        cl.SetDisplayString(TextTysJyuusyo.Text, jibanRec.TysRenrakusakiAtesakiMei)
        ' �����A����_TEL
        cl.SetDisplayString(TextTysTel.Text, jibanRec.TysRenrakusakiTel)
        ' �����A����_FAX
        cl.SetDisplayString(TextTysFax.Text, jibanRec.TysRenrakusakiFax)

        ' �X�V���O�C�����[�U�[ID
        cl.SetDisplayString(userinfo.LoginUserId, jibanRec.UpdLoginUserId)

        ' �\���FLG
        jibanRec.YoyakuZumiFlg = IIf(CheckYoyakuZumi.Checked, 1, 0)

        '******************
        '* �Y�t����
        '******************
        ' �ē��}
        jibanRec.AnnaiZu = IIf(CheckTPAnnaiZu.Checked, 1, 0)
        ' �z�u�}
        jibanRec.HaitiZu = IIf(CheckTPHaitiZu.Checked, 1, 0)
        ' �e�K���ʐ}
        jibanRec.KakukaiHeimenZu = IIf(CheckTPKakukaiHeimenZu.Checked, 1, 0)
        ' ��b���}
        jibanRec.KsHuseZu = IIf(CheckTPKsFuseZu.Checked, 1, 0)
        ' ��b�f�ʐ}
        jibanRec.KsDanmenZu = IIf(CheckTPKsDanmenZu.Checked, 1, 0)
        ' �����v��}
        jibanRec.ZouseiKeikakuZu = IIf(CheckTPZouseiKeikakuZu.Checked, 1, 0)

        '******************
        '* ��b���H�\���
        '******************
        ' ��b���H�\���FROM
        cl.SetDisplayString(TextKsTyakkouYoteiDateFrom.Text, jibanRec.KsTyakkouYoteiFromDate)
        ' ��b���H�\���TO
        cl.SetDisplayString(TextKsTyakkouYoteiDateTo.Text, jibanRec.KsTyakkouYoteiToDate)

        '******************
        '* �{�喼�L��
        '******************
        ' �{�喼�L��
        If RadioSesyumei0.Checked Then '��
            cl.SetDisplayString(0, jibanRec.SesyuMeiUmu)
        ElseIf RadioSesyumei1.Checked Then '�L
            cl.SetDisplayString(1, jibanRec.SesyuMeiUmu)
        Else
            cl.SetDisplayString(Integer.MinValue, jibanRec.SesyuMeiUmu)
        End If

        '***************************************
        ' ���i�ȊO�̎����ݒ�
        '***************************************
        If Me.ChkOtherAutoSetting(Me, jibanRec) = False Then
            Return False
        End If

        ''***************************************
        '' �����X�w�蒲����Ѓ`�F�b�N
        ''***************************************
        'If Me.checkInputTysTehaiCenter(Me, jibanRec) = False Then
        '    Return False
        'End If

        '***************************************
        ' ���i�̎����ݒ�(���i�P�`�Q)
        '***************************************
        '���i1�Ȃ����̓G���[��Ԃ�
        If Me.ChkSyouhin12AutoSetting(Me, jibanRec) = False Then
            Return False
        End If

        '***************************************
        ' �����������̐ݒ�
        '***************************************
        Dim listBr As New List(Of BukkenRirekiRecord)
        Me.SetBukkenRirekiInfo(Me, jibanRec, listBr)

        '***************************************
        ' ���̑�
        '***************************************
        '�A��������
        jibanRec.RentouBukkenSuu = CInt(Me.HiddenRentouBukkenSuu.Value)
        '��������
        jibanRec.SyoriKensuu = CInt(Me.HiddenSyoriKensuu.Value)

        Dim intGenzaiSyoriKensuu As Integer = CInt(Me.HiddenSyoriKensuu.Value)
        '===========================================================

        ' �f�[�^�̍X�V���s���܂�
        If logic.saveData(Me, jibanRec, intGenzaiSyoriKensuu, listBr) = False Then
            Return False
        End If
        '�����������i�[
        Me.HiddenSyoriKensuu.Value = CStr(intGenzaiSyoriKensuu)

        '�o�^�����n�Ճf�[�^����ʂɃZ�b�g
        SetDispJibanData(jibanRec)

        Return True
    End Function

    ''' <summary>
    ''' �d���`�F�b�N�{�^���������̏����i��\���{�^���j
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonExeTyoufukuCheck_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        '�n�Չ�ʋ��ʃN���X
        Dim jBn As New Jiban

        '�֑�������`�F�b�N
        Dim errMess As String = String.Empty
        Dim arrFocusTargetCtrl As New ArrayList

        '��������
        If jBn.KinsiStrCheck(Me.TextBukkenMeisyou.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "��������")
            arrFocusTargetCtrl.Add(Me.TextBukkenMeisyou)
        End If
        '�����Z��1
        If jBn.KinsiStrCheck(Me.TextBukkenJyuusyo1.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "�����Z��1")
            arrFocusTargetCtrl.Add(Me.TextBukkenJyuusyo1)
        End If
        '�����Z��2
        If jBn.KinsiStrCheck(Me.TextBukkenJyuusyo2.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "�����Z��2")
            arrFocusTargetCtrl.Add(Me.TextBukkenJyuusyo2)
        End If

        If errMess <> String.Empty Then
            '�t�H�[�J�X�Z�b�g
            If arrFocusTargetCtrl.Count > 0 Then
                SetFocus(arrFocusTargetCtrl(0))
            End If
            '�G���[���b�Z�[�W�\��
            Dim tmpScript = "alert('" & errMess & "');"
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "err", tmpScript, True)
        Else
            '���̓`�F�b�NOK�Ȃ�A�d���`�F�b�N���s
            Me.CheckTyoufuku(sender)
        End If

    End Sub

    ''' <summary>
    ''' �d���`�F�b�N����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <remarks></remarks>
    Private Sub CheckTyoufuku(ByVal sender As System.Object)
        Dim bolResult1 As Boolean = True
        Dim bolResult2 As Boolean = True

        ' �{�喼���󔒈ȊO�̏ꍇ�A�{�喼�̏d���`�F�b�N�����{
        If Me.TextBukkenMeisyou.Text.Trim <> "" Then
            If JLogic.ChkTyouhuku(Me.SelectKIKubun.SelectedValue, _
                                 String.Empty, _
                                 Me.TextBukkenMeisyou.Text.Trim) = True Then
                bolResult1 = False
            End If
        End If

        ' �����Z�����󔒈ȊO�̏ꍇ�A�����Z���̏d���`�F�b�N�����{
        If Me.TextBukkenJyuusyo1.Text.Trim() <> "" Or Me.TextBukkenJyuusyo2.Text.Trim() <> "" Then
            If JLogic.ChkTyouhuku(Me.SelectKIKubun.SelectedValue, _
                                 String.Empty, _
                                 Me.TextBukkenJyuusyo1.Text.Trim(), _
                                 Me.TextBukkenJyuusyo2.Text.Trim()) = True Then
                bolResult2 = False
            End If
        End If

        If sender IsNot Nothing Then
            '�`�F�b�N�����̃g���K�[�ɂ���āA�m�F�t���O���Z�b�g
            If Me.HiddenTyoufukuKakuninTargetId.Value = Me.TextBukkenMeisyou.ClientID Then
                '�{�喼�ύX���Ɏ��s���ꂽ�ꍇ
                Me.HiddenTyoufukuKakuninFlg1.Value = bolResult1.ToString
            ElseIf (Me.HiddenTyoufukuKakuninTargetId.Value = Me.TextBukkenJyuusyo1.ClientID Or _
                    Me.HiddenTyoufukuKakuninTargetId.Value = Me.TextBukkenJyuusyo2.ClientID) Then
                '�Z���ύX���Ɏ��s���ꂽ�ꍇ
                Me.HiddenTyoufukuKakuninFlg2.Value = bolResult2.ToString
            ElseIf Me.HiddenTyoufukuKakuninTargetId.Value = Me.SelectKIKubun.ClientID Then
                '�敪�ύX���Ɏ��s���ꂽ�ꍇ
                Me.HiddenTyoufukuKakuninFlg1.Value = bolResult1.ToString
                Me.HiddenTyoufukuKakuninFlg2.Value = bolResult2.ToString
            End If
        End If

        '�d�������݂���ꍇ�A�u�d����������v�{�^����L����
        If bolResult1 And bolResult2 Then
            Me.ButtonTyoufukuCheck.Disabled = True
            Me.ButtonTyoufukuCheck.Value = "�d�������Ȃ�"
            Me.HiddenTyoufukuKakuninFlg1.Value = Boolean.TrueString
            Me.HiddenTyoufukuKakuninFlg2.Value = Boolean.TrueString
        Else
            Me.ButtonTyoufukuCheck.Disabled = False
            Me.ButtonTyoufukuCheck.Value = "�d����������"
        End If

        ' �`�F�b�N�����̃g���K�[�ɂȂ���ID���i�[���Ă���R���g���[�����N���A
        Me.HiddenTyoufukuKakuninTargetId.Value = String.Empty

        UpdatePanelTyoufukuCheck.Update()

    End Sub

    ''' <summary>
    ''' �擪�ԍ��̕\���ؑ�
    ''' </summary>
    ''' <param name="blnDispFlg">�\�� or ��\��</param>
    ''' <remarks></remarks>
    Private Sub SetDispBangou(ByVal blnDispFlg As Boolean)

        If blnDispFlg Then '�\��
            '�ԍ�
            Me.TextBangou.Visible = True
            Me.TextBangou.Text = SetFormatBangou(CStr(Me.HiddenSentouBangou.Value), CStr(Me.HiddenRentouBukkenSuu.Value))

        Else '��\��
            '�ԍ�
            Me.TextBangou.Visible = False
            Me.TextBangou.Text = String.Empty

        End If

    End Sub

    ''' <summary>
    ''' �o�^������A�n�Ճf�[�^����ʂɕ\������
    ''' </summary>
    ''' <param name="recJibanData"></param>
    ''' <remarks></remarks>
    Private Sub SetDispJibanData(ByVal recJibanData As JibanRecordMousikomiInput)
        '�\���ؑ�
        Dim e As System.EventArgs = New System.EventArgs

        '�������� >= �A�������� �̏ꍇ�A�S�����I��
        If recJibanData.SyoriKensuu >= recJibanData.RentouBukkenSuu Then

            '�����̔Ԃ����ԍ�����ʂɃZ�b�g
            SetDispBangou(True)

            '�������
            Me.TextSITysGaisyaCd.Text = cl.GetDispStr(recJibanData.TysKaisyaCd + recJibanData.TysKaisyaJigyousyoCd)
            Me.tyousakaisyaSearchSub(Me, e, False)

            '�������@��DDL�\������
            cl.ps_SetSelectTextBoxTysHouhou(recJibanData.TysHouhou, Me.SelectSITysHouhou, False, Me.TextSITysHouhouCd)

            '�������@
            cl.SetDisplayString(recJibanData.TysHouhou, Me.TextSITysHouhouCd.Text)

            '�����T�v
            If recJibanData.TysGaiyou <> 0 Then
                Me.SelectSITysGaiyou.SelectedValue = cl.GetDispNum(recJibanData.TysGaiyou, "")
            End If

            Me.UpdatePanelSonota.Update()

            '�ۏ؏��i
            cl.ChgDispHosyouSyouhin(recJibanData.HosyouSyouhinUmu, Me.TextHosyouSyouhinUmu)
            Me.UpdatePanelHosyouSyouhinUmu.Update()

        End If
    End Sub

    ''' <summary>
    ''' �擪�ԍ��ƘA�����������擾���A�w��̃t�H�[�}�b�g�������Ԃ�
    ''' ���w��t�H�[�}�b�g
    ''' �y�ԍ��FXXXXXXXXXX �` XXXXXXXXXX�z
    ''' �������A�A��������=1���́A�y�ԍ��FXXXXXXXXXX�z
    ''' </summary>
    ''' <param name="strSentouBangou"></param>
    ''' <param name="strRentouSuu"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetFormatBangou(ByVal strSentouBangou As String, ByVal strRentouSuu As String) As String
        Dim strTmp As String = ""
        Dim strFomatL As String = "�y�����ԍ��F"
        Dim strFomatC As String = " �` "
        Dim strFomatR As String = "�z"
        Dim strKbn As String = Me.SelectKIKubun.SelectedValue

        If strSentouBangou = String.Empty Or strRentouSuu = String.Empty Then
            Return strTmp
            Exit Function
        End If

        If strRentouSuu = "1" Then
            strTmp = strFomatL & strKbn & strSentouBangou & strFomatR
        Else
            ' �ԍ�(�ۏ؏�NO) ��Ɨp
            Dim intLastBangou As Integer = CInt(strSentouBangou) + (CInt(strRentouSuu) - 1)
            Dim strLastBangou As String = Format(intLastBangou, "0000000000") '�t�H�[�}�b�g

            strTmp = strFomatL & strKbn & strSentouBangou & strFomatC & strKbn & strLastBangou & strFomatR
        End If

        Return strTmp
    End Function

    ''' <summary>
    ''' �R���g���[���̗L��������ؑւ���
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetEnableControl(ByVal blnFlg As Boolean)
        '�n�Չ�ʋ��ʃN���X
        Dim jBn As New Jiban

        '�L�����A�������̑ΏۊO�ɂ���R���g���[���S
        Dim noTarget As New Hashtable
        noTarget.Add(Me.ButtonSinkiHikitugi.ID, True) '�V�K(���p)�\���{�^��
        noTarget.Add(Me.ButtonSinki.ID, True) '�V�K�\���{�^��
        noTarget.Add(Me.TextBangou.ID, True) '�ԍ�

        If blnFlg Then
            '�S�ẴR���g���[���𖳌���
            jBn.ChangeDesabledAll(Me, True, noTarget)

            setFocusAJ(Me.ButtonSinkiHikitugi) '�t�H�[�J�X
        Else
            '�S�ẴR���g���[����L����
            jBn.ChangeDesabledAll(Me, False, noTarget)

            setFocusAJ(Me.ButtonTouroku1) '�t�H�[�J�X
        End If

    End Sub

    ''' <summary>
    ''' �o�^ ���s/�V�K(���p)�\��/�V�K�\�� �{�^���̕\���ؑւ��s�Ȃ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <remarks></remarks>
    Private Sub ChgDispButton(ByVal sender As System.Object)

        Dim objBtn As HtmlInputButton = CType(sender, HtmlInputButton)

        '��ʃR���g���[���̊�����������s�Ȃ�
        Select Case objBtn.ID  '���s�{�^�����擾
            Case Me.ButtonTouroku1.ID, Me.ButtonTouroku2.ID, Me.ButtonHiddenUpdate.ID  '�o�^���s�{�^��
                '�V�K�{�^���\��
                Me.ButtonSinkiHikitugi.Visible = True
                Me.ButtonSinki.Visible = True
                '�o�^�{�^����\��
                Me.ButtonTouroku1.Visible = False
                Me.ButtonTouroku2.Visible = False

                '�ԍ��̕\���ؑ�
                SetDispBangou(True)

                '�w��l�I�����̓���(��ʕ\�����p)
                checkSonota()

                '��������҂̕\���ؑ�
                checkTysTatiaisya()

                '��ʍ��ڔ񊈐���
                Me.SetEnableControl(True)

            Case Me.ButtonSinkiHikitugi.ID '�V�K(���p)�\���o�^�{�^��
                '�V�K�{�^����\��
                Me.ButtonSinkiHikitugi.Visible = False
                Me.ButtonSinki.Visible = False
                '�o�^�{�^���\��
                Me.ButtonTouroku1.Visible = True
                Me.ButtonTouroku2.Visible = True

                '��ʍ��ڊ�����
                Me.SetEnableControl(False)

                '�ԍ��̕\���ؑ�
                SetDispBangou(False)

                '�w��l�I�����̓���(��ʕ\�����p)
                checkSonota()

                '��������҂̕\���ؑ�
                checkTysTatiaisya()

                '�d���`�F�b�N���s��(�`�F�b�N���s�g���K�[�͋敪�Ƃ���)
                Me.HiddenTyoufukuKakuninTargetId.Value = Me.SelectKIKubun.ClientID
                Me.CheckTyoufuku(sender)

                '�擪�ԍ�
                Me.HiddenSentouBangou.Value = String.Empty
                '�A��������
                Me.HiddenRentouBukkenSuu.Value = "1"
                '��������
                Me.HiddenSyoriKensuu.Value = "0"
                '�A�����sFLG
                HiddenCallRentouNextFlg.Value = String.Empty
                '���̓`�F�b�NFLG
                Me.HiddenInputChk.Value = String.Empty

            Case Me.ButtonSinki.ID '�V�K�\���{�^��
                '��ʑJ�ځi�����[�h�j
                Server.Transfer(UrlConst.MOUSIKOMI_INPUT)

            Case Else
                '�G���[
                Exit Select
        End Select
    End Sub

#End Region

#End Region

#Region "���i�ȊO�̎����ݒ�"

    ''' <summary>
    ''' ���i�ȊO�̎����ݒ菈�����s�Ȃ��B
    ''' ��������ЁA�������@�A
    ''' �ۏ؊��ԁA�H����А����L��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="jibanrec"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChkOtherAutoSetting(ByVal sender As Object, ByRef jibanRec As JibanRecordMousikomiInput) As Boolean

        Dim logic As New MousikomiInputLogic
        Dim blnSiteiTysGaisya As Boolean = False
        Dim tmpTys As String = ""
        Dim blnTysGaiyou As Boolean = False

        '������Ђ̎����ݒ�
        '�������
        Dim strTysGaisya As String = Me.TextSITysGaisyaCd.Text

        If strTysGaisya = String.Empty Then '������

            '�w�蒲����Ђ̎擾����ѐݒ�
            blnSiteiTysGaisya = logic.ChkExistSiteiTysGaisya(Me.TextKITourokuBangou.Text, strTysGaisya)
            If blnSiteiTysGaisya Then
                ' �擾�����������
                tmpTys = strTysGaisya
            Else
                ' ���������
                tmpTys = EarthConst.KARI_TYOSA_KAISYA_CD
            End If

            If tmpTys.Length >= 6 Then   '����6�ȏ�K�{
                jibanRec.TysKaisyaCd = tmpTys.Substring(0, tmpTys.Length - 2) '������ЃR�[�h
                jibanRec.TysKaisyaJigyousyoCd = tmpTys.Substring(tmpTys.Length - 2, 2) '������Ў��Ə��R�[�h
            End If

        End If

        '�������@�̎����ݒ�
        '�������=�����͂ł��A�������@=�����͂̏ꍇ�ł��A�����ݒ�̒������<>��������Ђ̏ꍇ
        If Me.TextSITysGaisyaCd.Text = String.Empty _
            AndAlso Me.SelectSITysHouhou.SelectedValue = String.Empty _
                AndAlso tmpTys <> String.Empty _
                    AndAlso tmpTys <> EarthConst.KARI_TYOSA_KAISYA_CD Then

            jibanRec.TysHouhou = 1
            blnTysGaiyou = True
        Else
            If Me.SelectSITysHouhou.SelectedValue = String.Empty Then '������
                jibanRec.TysHouhou = 90
                blnTysGaiyou = True
            End If
        End If

        '�����X�ɂ�鎩���ݒ�
        '�ۏ؊���
        If Me.HiddenHosyouKikan.Value <> "" Then
            jibanRec.HosyouKikan = CInt(Me.HiddenHosyouKikan.Value)
        End If
        '�H����А����L��
        If Me.HiddenKjGaisyaSeikyuuUmu.Value <> "" Then
            jibanRec.KojGaisyaSeikyuuUmu = CInt(Me.HiddenKjGaisyaSeikyuuUmu.Value)
        End If

        '�����T�v�̎����ݒ�(�������@�������ݒ肳���ꍇ�ł��A�����T�v�����ݒ�̏ꍇ)
        If blnTysGaiyou Then
            With jibanRec
                '���ݒ�̏ꍇ
                If Me.SelectSITysGaiyou.SelectedValue = "9" Then
                    '�ݒ�E�擾�p ���i���i�ݒ背�R�[�h
                    Dim recKakakuSettei As New KakakuSetteiRecord

                    '���i�敪
                    cbLogic.SetDisplayString(.SyouhinKbn, recKakakuSettei.SyouhinKbn)
                    '�������@
                    cbLogic.SetDisplayString(.TysHouhou, recKakakuSettei.TyousaHouhouNo)
                    '���i�R�[�h
                    cbLogic.SetDisplayString(.SyouhinCd1, recKakakuSettei.SyouhinCd)

                    '���i���i�ݒ�}�X�^����l�̎擾
                    JLogic.GetTysGaiyou(recKakakuSettei)

                    '�����T�v�̐ݒ�
                    .TysGaiyou = cl.GetDispNum(recKakakuSettei.TyousaGaiyou, Integer.MinValue)
                End If
            End With
        End If

        Return True
    End Function

    ''' <summary>
    ''' ��ʂ���̕������������擾���A���X�g�Ƃ��ĕԋp����
    ''' </summary>
    ''' <param name="jibanRec">�n�Ճ��R�[�h</param>
    ''' <param name="listBr">�����������R�[�h�̃��X�g(��ʂ̏��)</param>
    ''' <remarks></remarks>
    Private Sub SetBukkenRirekiInfo(ByVal sender As Object, ByVal jibanRec As JibanRecordBase, ByRef listBr As List(Of BukkenRirekiRecord))
        Dim brRec As New BukkenRirekiRecord
        Dim intCnt As Integer = 0

        '��ʏ����擾���A���������f�[�^���Z�b�g����
        brRec = Me.MakeBukkenRirekiRec(jibanRec, emBrCtrl.intCtrl1)
        If Not brRec Is Nothing Then
            listBr.Add(brRec)
        End If

        brRec = Me.MakeBukkenRirekiRec(jibanRec, emBrCtrl.intCtrl2)
        If Not brRec Is Nothing Then
            listBr.Add(brRec)
        End If

        brRec = Me.MakeBukkenRirekiRec(jibanRec, emBrCtrl.intCtrl3)
        If Not brRec Is Nothing Then
            listBr.Add(brRec)
        End If

    End Sub


#Region "���ʑΉ�(�����������)"
    ''' <summary>
    ''' ���������f�[�^���쐬����[�\�����͉��]
    ''' </summary>
    ''' <param name="jibanRec">�n�Ճ��R�[�h</param>
    ''' <param name="emBrCtrl">���������R���g���[���^�C�v</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function MakeBukkenRirekiRec( _
                                        ByVal jibanRec As JibanRecordBase _
                                        , ByVal emBrCtrl As emBrCtrl _
                                        ) As BukkenRirekiRecord

        '���̓`�F�b�N
        Select Case emBrCtrl
            Case MousikomiInput.emBrCtrl.intCtrl1
                '��ʂ���ѕ���
                If Me.SelectBRSyubetu1.SelectedValue <> String.Empty And Me.HiddenBRBunrui1.Value <> String.Empty Then
                Else
                    Return Nothing
                End If

            Case MousikomiInput.emBrCtrl.intCtrl2
                '��ʂ���ѕ���
                If Me.SelectBRSyubetu2.SelectedValue <> String.Empty And Me.HiddenBRBunrui2.Value <> String.Empty Then
                Else
                    Return Nothing
                End If

            Case MousikomiInput.emBrCtrl.intCtrl3
                '��ʂ���ѕ���
                If Me.SelectBRSyubetu3.SelectedValue <> String.Empty And Me.HiddenBRBunrui3.Value <> String.Empty Then
                Else
                    Return Nothing
                End If

            Case Else
                Return Nothing
        End Select

        '���ȉ��A���R�[�h�𐶐�
        '��������o�^�p���R�[�h
        Dim record As New BukkenRirekiRecord

        '�敪
        record.Kbn = jibanRec.Kbn
        '�ۏ؏�NO
        record.HosyousyoNo = jibanRec.HosyousyoNo

        Select Case emBrCtrl
            Case MousikomiInput.emBrCtrl.intCtrl1
                '�������
                cl.SetDisplayString(Me.SelectBRSyubetu1.SelectedValue, record.RirekiSyubetu)
                '����NO
                cl.SetDisplayString(Me.HiddenBRBunrui1.Value, record.RirekiNo)
                '���e
                record.Naiyou = Me.TextAreaBRNaiyou1.Value
                '�ėp���t
                cl.SetDisplayString(Me.TextBRHizuke1.Text, record.HanyouDate)
                '�ėp�R�[�h
                cl.SetDisplayString(Me.TextBRHanyouCd1.Text, record.HanyouCd)

            Case MousikomiInput.emBrCtrl.intCtrl2
                '�������
                cl.SetDisplayString(Me.SelectBRSyubetu2.SelectedValue, record.RirekiSyubetu)
                '����NO
                cl.SetDisplayString(Me.HiddenBRBunrui2.Value, record.RirekiNo)
                '���e
                record.Naiyou = Me.TextAreaBRNaiyou2.Value
                '�ėp���t
                cl.SetDisplayString(Me.TextBRHizuke2.Text, record.HanyouDate)
                '�ėp�R�[�h
                cl.SetDisplayString(Me.TextBRHanyouCd2.Text, record.HanyouCd)

            Case MousikomiInput.emBrCtrl.intCtrl3
                '�������
                cl.SetDisplayString(Me.SelectBRSyubetu3.SelectedValue, record.RirekiSyubetu)
                '����NO
                cl.SetDisplayString(Me.HiddenBRBunrui3.Value, record.RirekiNo)
                '���e
                record.Naiyou = Me.TextAreaBRNaiyou3.Value
                '�ėp���t
                cl.SetDisplayString(Me.TextBRHizuke3.Text, record.HanyouDate)
                '�ėp�R�[�h
                cl.SetDisplayString(Me.TextBRHanyouCd3.Text, record.HanyouCd)

        End Select

        '����NO(Logic���ō̔�)
        record.NyuuryokuNo = Integer.MinValue
        '�Ǘ����t
        cl.SetDisplayString(String.Empty, record.KanriDate)
        '�Ǘ��R�[�h
        cl.SetDisplayString(String.Empty, record.KanriCd)
        '�ύX�ۃt���O
        record.HenkouKahiFlg = 0
        '���
        record.Torikesi = 0
        '�o�^(�X�V)���O�C�����[�UID
        record.UpdLoginUserId = jibanRec.UpdLoginUserId
        '�o�^(�X�V)����
        record.UpdDatetime = jibanRec.UpdDatetime

        Return record
    End Function

#End Region

#End Region

#Region "���i�P�A�Q�̎����ݒ�"

    ''' <summary>
    ''' ���i�P�A�Q�̎����ݒ�̃`�F�b�N�ƃZ�b�g���s�Ȃ��B
    ''' �����펞�͊Y�����i���Y���@�ʐ������R�[�h�ɃZ�b�g
    ''' ���G���[���͏������f
    ''' </summary>
    ''' <param name="jibanRec"></param>
    ''' <returns>True or False</returns>
    ''' <remarks></remarks>
    Public Function ChkSyouhin12AutoSetting(ByVal sender As Object, ByRef jibanRec As JibanRecordMousikomiInput) As Boolean

        '�G���[���b�Z�[�W������
        Dim tmpScript As String = ""
        Dim strCrlf As String = "\r\n"
        Dim strErrMsg As String = ""

        '���i�R�[�h�P�̐ݒ���s
        If JLogic.Syouhin1Set(sender, jibanRec) = False Then

        Else '���i1�Z�b�gOK

            '���i�P���������z�̐ݒ�
            JLogic.Syouhin1SyoudakuGakuSet(sender, jibanRec)

            '���i�Q���R�[�h�I�u�W�F�N�g�̐���
            JLogic.CreateSyouhin23Rec(sender, jibanRec)

            '��������X�̏��i����2�����ݒ�
            JLogic.TokuteitenSyouhin2Set(sender, jibanRec)

            '���i����2[����������]�̎����ݒ�
            JLogic.TatouwariSet(sender, jibanRec)

            '���������s���A����N�����̐ݒ���s
            JLogic.Syouhin1UriageSeikyuDateSet(sender, jibanRec, False)

            '���i�Q���R�[�h�I�u�W�F�N�g�̔j��
            JLogic.DeleteSyouhin23Rec(sender, jibanRec)

        End If

        '�G���[�������͉�ʍĕ`��̂��߁A�R���g���[���̗L��������ؑւ���
        If jibanRec.SyouhinKkk_ErrMsg <> "" Then
            '�G���[���b�Z�[�W�\��
            tmpScript = "alert('" & jibanRec.SyouhinKkk_ErrMsg & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ChkSyouhin12AutoSetting", tmpScript, True)
            Return False
        End If

        Return True
    End Function

#End Region

End Class
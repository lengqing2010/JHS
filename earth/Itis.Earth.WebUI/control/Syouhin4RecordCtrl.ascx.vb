Partial Public Class Syouhin4RecordCtrl
    Inherits System.Web.UI.UserControl

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim iraiSession As New IraiSession
    Dim userinfo As New LoginUserInfo
    Dim masterAjaxSM As New ScriptManager
    Dim jSM As New JibanSessionManager
    Dim cl As New CommonLogic
    Dim MLogic As New MessageLogic

#Region "�v���p�e�B"

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for Hidden�����X�R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHdnKameitenCd() As HtmlInputHidden
        Get
            Return Me.HiddenKameitenCd
        End Get
        Set(ByVal value As HtmlInputHidden)
            Me.HiddenKameitenCd = value
        End Set
    End Property

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for Hidden�敪
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHdnKbn() As HtmlInputHidden
        Get
            Return Me.HiddenKubun
        End Get
        Set(ByVal value As HtmlInputHidden)
            Me.HiddenKubun = value
        End Set
    End Property

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for Hidden�n��.�������
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHdnJibanTysKaisyaCd() As HtmlInputHidden
        Get
            Return Me.HiddenJibanTysKaisyaCd
        End Get
        Set(ByVal value As HtmlInputHidden)
            Me.HiddenJibanTysKaisyaCd = value
        End Set
    End Property

    ''' <summary>
    ''' ������E�d���惊���N�̊O���A�N�Z�X�p
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccSeikyuuSiireLink() As SeikyuuSiireLinkCtrl
        Get
            Return ucSeikyuuSiireLink
        End Get
        Set(ByVal value As SeikyuuSiireLinkCtrl)
            ucSeikyuuSiireLink = value
        End Set
    End Property

#Region "�����n"

#Region "�˗��Ɩ�����"
    ''' <summary>
    ''' �˗��Ɩ�����
    ''' </summary>
    ''' <value></value>


    Public Property IraiGyoumuKengen() As String
        Get
            Return Integer.Parse(HiddenIraiGyoumuKengen.Value)
        End Get
        Set(ByVal value As String)
            HiddenIraiGyoumuKengen.Value = value.ToString()
        End Set
    End Property
#End Region

#Region "�������Ǘ�����"
    ''' <summary>
    ''' �������Ǘ�����
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HattyuusyoKanriKengen() As String
        Get
            Return Integer.Parse(HiddenHattyuusyoKanriKengen.Value)
        End Get
        Set(ByVal value As String)
            HiddenHattyuusyoKanriKengen.Value = value.ToString()
        End Set
    End Property
#End Region

#Region "�o���Ɩ�����"
    ''' <summary>
    ''' �o���Ɩ�����
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KeiriGyoumuKengen() As String
        Get
            Return Integer.Parse(HiddenKeiriGyoumuKengen.Value)
        End Get
        Set(ByVal value As String)
            HiddenKeiriGyoumuKengen.Value = value.ToString()
        End Set
    End Property
#End Region

#End Region

#End Region

    ''' <summary>
    ''' �y�[�W���[�h
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '���i4��ʏ����擾�iScriptManager�p�j
        Dim myMaster As PopupSyouhin4 = Page.Page
        masterAjaxSM = myMaster.AjaxScriptManager

        '****************************************************************************
        ' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
        '****************************************************************************
        SetDispAction()

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' ���i�}�X�^�|�b�v�A�b�v��ʂ̕��ގw��
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Me.HiddenTargetId.Value = EarthEnum.EnumSyouhinKubun.Syouhin4

        If Not IsPostBack Then
            '��ʓǍ��ݎ��̒l��Hidden���ڂɑޔ�
            setOpenValuesUriage()
            setOpenValuesSiire()
        End If

    End Sub

    ''' <summary>
    ''' �y�[�W�`��O����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

        Dim strViewMode As String       '��ʃ��[�h

        '�o�������������ꍇ�͔���E�d����ύX��ʂ͎Q�ƃ��[�h�ŊJ��
        If HiddenKeiriGyoumuKengen.Value <> "-1" Then
            strViewMode = EarthConst.MODE_VIEW
        Else
            strViewMode = EarthConst.MODE_EDIT
        End If

        '������/�d������̃Z�b�g
        Me.ucSeikyuuSiireLink.SetVariableValueCtrlFromParent(Me.AccHdnKameitenCd.Value _
                                                            , Me.TextSyouhinCd.Text _
                                                            , Me.AccHdnJibanTysKaisyaCd.Value _
                                                            , Me.HiddenUriageJyoukyou.Value _
                                                            , _
                                                            , _
                                                            , Me.TextDenpyouUriageNengappi.Text _
                                                            , strViewMode)
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
    ''' �@�ʐ������R�[�h�����ʕ\�����ڂւ̒l�Z�b�g���s�Ȃ��B
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="recJiban">�n�Ճ��R�[�h</param>
    ''' <param name="recTeiSei">�@�ʐ������R�[�h</param>
    ''' <param name="intKengen">����(�L:1�A��:0)</param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromDataRec(ByVal sender As Object _
                                    , ByVal e As System.EventArgs _
                                    , ByVal recJiban As JibanRecord _
                                    , ByVal recTeiSei As TeibetuSeikyuuRecord _
                                    , Optional ByVal intKengen As Integer = 1 _
                                    )

        '���i�R�[�h���ݒ莞�A�����Z�b�g���Ȃ�
        If recTeiSei.SyouhinCd Is Nothing Then
            Exit Sub
        ElseIf recTeiSei.SyouhinCd = "" Then
            Exit Sub
        End If

        '******************************************
        '* �@�ʐ����f�[�^����ʃR���g���[���ɐݒ�
        '******************************************
        '�敪
        Me.HiddenKubun.Value = recTeiSei.Kbn
        '�ۏ؏�NO
        Me.HiddenBangou.Value = recTeiSei.HosyousyoNo
        '���ރR�[�h
        Me.HiddenBunruiCd.Value = recTeiSei.BunruiCd
        '��ʕ\��NO
        Me.HiddenGamenHyoujiNo.Value = recTeiSei.GamenHyoujiNo
        '�ŋ敪
        Me.HiddenZeikubun.Value = recTeiSei.ZeiKbn
        '�ŗ�
        Me.HiddenZeiritu.Value = recTeiSei.Zeiritu
        '���i�R�[�h
        Me.TextSyouhinCd.Text = recTeiSei.SyouhinCd
        Me.HiddenSyouhinCdOld.Value = recTeiSei.SyouhinCd
        '���m��敪
        Me.SelectKakutei.SelectedValue = recTeiSei.KakuteiKbn
        '���i��
        Me.SpanSyouhinMei.InnerHtml = recTeiSei.SyouhinMei
        '�H���X�����Ŕ����z
        Me.TextKoumutenSeikyuuGaku.Text = IIf(recTeiSei.KoumutenSeikyuuGaku = Integer.MinValue, _
                                              0, _
                                              recTeiSei.KoumutenSeikyuuGaku.ToString(EarthConst.FORMAT_KINGAKU_1))

        '�������Ŕ����z
        Me.TextJituSeikyuuGaku.Text = IIf(recTeiSei.UriGaku = Integer.MinValue, _
                                          0, _
                                          recTeiSei.UriGaku.ToString(EarthConst.FORMAT_KINGAKU_1))

        '�d������Ŋz
        Me.TextSiireSyouhizeiGaku.Text = recTeiSei.SiireSyouhiZeiGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
        '����Ŋz
        Me.TextSyouhizeiGaku.Text = recTeiSei.UriageSyouhiZeiGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
        '�ō����z
        Me.TextZeikomiKingaku.Text = recTeiSei.ZeikomiUriGaku.ToString(EarthConst.FORMAT_KINGAKU_1)

        '���������z
        Me.TextSyoudakusyoKingaku.Text = IIf(recTeiSei.SiireGaku = Integer.MinValue, _
                                             0, _
                                            (recTeiSei.SiireGaku).ToString(EarthConst.FORMAT_KINGAKU_1))
        '���Ϗ��쐬��
        If Not recTeiSei.TysMitsyoSakuseiDate = Nothing Then
            Me.TextMitumorisyoSakuseiDate.Text = IIf(recTeiSei.TysMitsyoSakuseiDate = DateTime.MinValue, _
                                                     "", _
                                                    cl.GetDisplayString(recTeiSei.TysMitsyoSakuseiDate))
        End If

        '����N����
        If Not recTeiSei.UriDate = Nothing Then
            Me.TextUriageNengappi.Text = IIf(recTeiSei.UriDate = DateTime.MinValue, _
                                             "", _
                                            cl.GetDisplayString(recTeiSei.UriDate))
        End If

        '�`�[����N����(�C����Q��)
        If Not recTeiSei.DenpyouUriDate = Nothing Then
            Me.TextDenpyouUriageNengappi.Text = IIf(recTeiSei.DenpyouUriDate = DateTime.MinValue, _
                                              "", _
                                              cl.GetDisplayString(recTeiSei.DenpyouUriDate))
            Me.TextDenpyouUriageNengappiDisplay.Text = IIf(recTeiSei.DenpyouUriDate = DateTime.MinValue, _
                                  "", _
                                  cl.GetDisplayString(recTeiSei.DenpyouUriDate))
        End If

        '�`�[�d���N����(�C����Q��)
        If Not recTeiSei.DenpyouSiireDate = Nothing Then
            Me.TextDenpyouSiireNengappi.Text = IIf(recTeiSei.DenpyouSiireDate = DateTime.MinValue, _
                                              "", _
                                              cl.GetDisplayString(recTeiSei.DenpyouSiireDate))
            Me.TextDenpyouSiireNengappiDisplay.Text = IIf(recTeiSei.DenpyouSiireDate = DateTime.MinValue, _
                                  "", _
                                  cl.GetDisplayString(recTeiSei.DenpyouSiireDate))
        End If

        '�������L��
        Me.SelectSeikyuuUmu.SelectedValue = recTeiSei.SeikyuuUmu

        '���������s��
        If Not recTeiSei.SeikyuusyoHakDate = Nothing Then
            Me.TextSeikyuusyoHakkouDate.Text = IIf(recTeiSei.SeikyuusyoHakDate = DateTime.MinValue, _
                                                   "", _
                                                   cl.GetDisplayString(recTeiSei.SeikyuusyoHakDate))
        End If

        '�����㏈��
        Me.SelectUriageSyori.SelectedValue = recTeiSei.UriKeijyouFlg
        '����v��FGL
        Me.HiddenUriageJyoukyou.Value = recTeiSei.UriKeijyouFlg
        '����v���
        Me.HiddenUriageKeijyouDate.Value = cl.GetDisplayString(recTeiSei.UriKeijyouDate)

        '���㏈����
        If Not recTeiSei.UriKeijyouDate = Nothing Then
            Me.TextUriageDate.Text = IIf(recTeiSei.UriKeijyouDate = DateTime.MinValue, _
                                         "", _
                                         cl.GetDisplayString(recTeiSei.UriKeijyouDate))
        End If

        '���������z
        Me.TextHattyuusyoKingaku.Text = IIf(recTeiSei.HattyuusyoGaku = Integer.MinValue, _
                                            "", _
                                            (recTeiSei.HattyuusyoGaku).ToString(EarthConst.FORMAT_KINGAKU_1))
        '���������m��
        Me.SelectHattyuusyoKakutei.SelectedValue = recTeiSei.HattyuusyoKakuteiFlg
        '�������m��FLG
        Me.HiddenHattyuusyoKakuteiOld.Value = recTeiSei.HattyuusyoKakuteiFlg
        Me.HiddenHattyuusyoFlgOld.Value = recTeiSei.HattyuusyoKakuteiFlg

        '�������m��ς݂̏ꍇ�A���������z��ҏW�s�ɐݒ�
        If SelectHattyuusyoKakutei.SelectedValue = "1" Then
            cl.chgVeiwMode(TextHattyuusyoKingaku)

            ' �������m��ς݂̏ꍇ�A�o�������������ꍇ�A�������m����񊈐���
            If HiddenKeiriGyoumuKengen.Value = "0" Then
                cl.chgVeiwMode(Me.SelectHattyuusyoKakutei, Me.SpanKakutei)
            End If
        End If

        '�������m�F��
        If Not recTeiSei.HattyuusyoKakuninDate = Nothing Then
            Me.TextHattyuusyoKakuninDate.Text = IIf(recTeiSei.HattyuusyoKakuninDate = DateTime.MinValue, _
                                                    "", _
                                                    cl.GetDisplayString(recTeiSei.HattyuusyoKakuninDate))
        End If

        '�X�V����
        If recTeiSei.UpdDatetime = DateTime.MinValue Then
            Me.HiddenUpdDatetime.Value = recTeiSei.AddDatetime.ToString(EarthConst.FORMAT_DATE_TIME_2)
        Else
            Me.HiddenUpdDatetime.Value = recTeiSei.UpdDatetime.ToString(EarthConst.FORMAT_DATE_TIME_2)
        End If

        '������/�d���惊���N�փZ�b�g
        Me.ucSeikyuuSiireLink.SetSeikyuuSiireLinkFromTeibetuRec(recTeiSei)

        '******************************************
        '* ���i���̎擾
        '******************************************
        Dim lstSyouhin23 As New List(Of Syouhin23Record)
        Dim recSyouhin23 As New Syouhin23Record
        Dim lgcJiban As New JibanLogic
        Dim intCnt As Integer = 0
        lstSyouhin23 = lgcJiban.GetSyouhin23(recTeiSei.SyouhinCd, _
                                             String.Empty, _
                                             EarthEnum.EnumSyouhinKubun.AllSyouhinTorikesi, _
                                             intCnt, _
                                             Integer.MinValue, _
                                             Me.HiddenKameitenCd.Value)
        If lstSyouhin23.Count > 0 Then
            recSyouhin23 = lstSyouhin23(0)
        End If

        '��������̐ݒ�
        GetSeikyuuInfo(recSyouhin23)

        '�y�����ɂ�銈������z
        SetEnableControl()

    End Sub

    ''' <summary>
    ''' ��ʏ��i�R���g���[������@�ʐ����f�[�^�ɒl���Z�b�g����i�o�^/�X�V�����p�j
    ''' </summary>
    ''' <returns>�@�ʐ������R�[�h</returns>
    ''' <remarks></remarks>
    Public Function setTeibetuToSyouhin() As TeibetuSeikyuuRecord

        '�@�ʐ������R�[�h
        Dim recTS As New TeibetuSeikyuuRecord

        '��ʕ\��NO
        cl.SetDisplayString(Me.HiddenGamenHyoujiNo.Value, recTS.GamenHyoujiNo)
        '���i�R�[�h
        cl.SetDisplayString(Me.TextSyouhinCd.Text, recTS.SyouhinCd)
        '���m��敪
        cl.SetDisplayString(Me.SelectKakutei.SelectedValue, recTS.KakuteiKbn)
        '�H���X�����Ŕ����z
        cl.SetDisplayString(Me.TextKoumutenSeikyuuGaku.Text, recTS.KoumutenSeikyuuGaku)
        '�������Ŕ����z�i������z�j
        cl.SetDisplayString(Me.TextJituSeikyuuGaku.Text, recTS.UriGaku)
        '���������z�i�d�����z�j
        cl.SetDisplayString(Me.TextSyoudakusyoKingaku.Text, recTS.SiireGaku)
        '���ŋ敪
        cl.SetDisplayString(Me.HiddenZeikubun.Value, recTS.ZeiKbn)
        '�ŗ�
        cl.SetDisplayString(Me.HiddenZeiritu.Value, recTS.Zeiritu)
        '����Ŋz
        cl.SetDisplayString(Me.TextSyouhizeiGaku.Text, recTS.UriageSyouhiZeiGaku)
        '�d������Ŋz
        cl.SetDisplayString(Me.TextSiireSyouhizeiGaku.Text, recTS.SiireSyouhiZeiGaku)
        '���Ϗ��쐬��
        cl.SetDisplayString(Me.TextMitumorisyoSakuseiDate.Text, recTS.TysMitsyoSakuseiDate)
        '����N����
        cl.SetDisplayString(Me.TextUriageNengappi.Text, recTS.UriDate)
        '�`�[����N����
        cl.SetDisplayString(Me.TextDenpyouUriageNengappi.Text, recTS.DenpyouUriDate)
        '�`�[�d���N����
        cl.SetDisplayString(Me.TextDenpyouSiireNengappi.Text, recTS.DenpyouSiireDate)
        '�������L��
        cl.SetDisplayString(Me.SelectSeikyuuUmu.SelectedValue, recTS.SeikyuuUmu)
        '���������s��
        cl.SetDisplayString(Me.TextSeikyuusyoHakkouDate.Text, recTS.SeikyuusyoHakDate)
        '�����㏈��
        cl.SetDisplayString(Me.SelectUriageSyori.SelectedValue, recTS.UriKeijyouFlg)
        '���㏈����
        cl.SetDisplayString(Me.TextUriageDate.Text, recTS.UriKeijyouDate)
        '���������z
        cl.SetDisplayString(Me.TextHattyuusyoKingaku.Text, recTS.HattyuusyoGaku)
        '���������m��
        cl.SetDisplayString(Me.SelectHattyuusyoKakutei.SelectedValue, recTS.HattyuusyoKakuteiFlg)
        '�������m�F��
        cl.SetDisplayString(Me.TextHattyuusyoKakuninDate.Text, recTS.HattyuusyoKakuninDate)

        '������/�d���惊���N����擾
        Me.ucSeikyuuSiireLink.SetTeibetuRecFromSeikyuuSiireLink(recTS)

        '�X�V����
        If Me.HiddenUpdDatetime.Value <> String.Empty Then
            recTS.UpdDatetime = DateTime.Parse(Me.HiddenUpdDatetime.Value)
        Else
            recTS.UpdDatetime = DateTime.MinValue
        End If
        '�X�V��ID
        recTS.UpdLoginUserId = Me.HiddenLoginUserId.Value

        Return recTS
    End Function

    ''' <summary>
    ''' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetDispAction()
        Dim jBn As New Jiban '�n�Չ�ʃN���X

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' �v���_�E��/�R�[�h���͘A�g�ݒ�
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '�Ȃ�

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

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ ���i�R�[�h����у{�^��
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Me.TextSyouhinCd.Attributes("onfocus") = "setTempValueForOnBlur(this);"
        Me.TextSyouhinCd.Attributes("onblur") = "if(checkTempValueForOnBlur(this)) callSetSyouhin4(this);"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ ���z�n
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '�H���X�����Ŕ����z
        Me.TextKoumutenSeikyuuGaku.Attributes("onfocus") = onFocusPostBackScript
        Me.TextKoumutenSeikyuuGaku.Attributes("onblur") = onBlurPostBackScript
        Me.TextKoumutenSeikyuuGaku.Attributes("onkeydown") = disabledOnkeydown
        '�������Ŕ����z
        Me.TextJituSeikyuuGaku.Attributes("onfocus") = onFocusPostBackScript
        Me.TextJituSeikyuuGaku.Attributes("onblur") = onBlurPostBackScript
        Me.TextJituSeikyuuGaku.Attributes("onkeydown") = disabledOnkeydown
        '����Ŋz
        Me.TextSyouhizeiGaku.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSyouhizeiGaku.Attributes("onblur") = onBlurPostBackScript
        Me.TextSyouhizeiGaku.Attributes("onkeydown") = disabledOnkeydown
        '�d������Ŋz
        Me.TextSiireSyouhizeiGaku.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSiireSyouhizeiGaku.Attributes("onblur") = onBlurPostBackScript
        Me.TextSiireSyouhizeiGaku.Attributes("onkeydown") = disabledOnkeydown
        '���������z
        Me.TextSyoudakusyoKingaku.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSyoudakusyoKingaku.Attributes("onblur") = onBlurPostBackScript
        Me.TextSyoudakusyoKingaku.Attributes("onkeydown") = disabledOnkeydown
        '���������z
        Me.TextHattyuusyoKingaku.Attributes("onfocus") = onFocusPostBackScript
        Me.TextHattyuusyoKingaku.Attributes("onblur") = onBlurPostBackScript
        Me.TextHattyuusyoKingaku.Attributes("onkeydown") = disabledOnkeydown

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ ���t�n
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '���Ϗ��쐬��
        Me.TextMitumorisyoSakuseiDate.Attributes("onblur") = checkDate
        Me.TextMitumorisyoSakuseiDate.Attributes("onkeydown") = disabledOnkeydown
        '����N����
        Me.TextUriageNengappi.Attributes("onblur") = onBlurPostBackScriptDate
        Me.TextUriageNengappi.Attributes("onfocus") = onFocusPostBackScriptDate
        Me.TextUriageNengappi.Attributes("onkeydown") = disabledOnkeydown
        '�`�[����N����
        Me.TextDenpyouUriageNengappi.Attributes("onblur") = onBlurPostBackScriptDate
        Me.TextDenpyouUriageNengappi.Attributes("onfocus") = onFocusPostBackScriptDate
        Me.TextDenpyouUriageNengappi.Attributes("onkeydown") = disabledOnkeydown
        '�`�[�d���N����
        Me.TextDenpyouSiireNengappi.Attributes("onblur") = checkDate
        Me.TextDenpyouSiireNengappi.Attributes("onkeydown") = disabledOnkeydown
        '���������s��
        Me.TextSeikyuusyoHakkouDate.Attributes("onblur") = checkDate
        Me.TextSeikyuusyoHakkouDate.Attributes("onkeydown") = disabledOnkeydown
        '�������m�F��
        Me.TextHattyuusyoKakuninDate.Attributes("onblur") = checkDate
        Me.TextHattyuusyoKakuninDate.Attributes("onkeydown") = disabledOnkeydown

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ �^�u�C���f�b�N�X
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '�d������Ŋz
        Me.TextSiireSyouhizeiGaku.TabIndex = "-1"
        '����N����
        Me.TextUriageNengappi.TabIndex = "-1"
        '���㏈��
        Me.SelectUriageSyori.TabIndex = "-1"
        '�����L��
        Me.SelectSeikyuuUmu.TabIndex = "-1"


    End Sub

    ''' <summary>
    ''' �������̐ݒ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetSeikyuuInfo(ByVal recSyouhin As Syouhin23Record)

        Dim lgcKameiten As New KameitenSearchLogic
        Dim recKameiten As New KameitenSearchRecord
        Dim intKeiretuFlg As Integer
        Dim strSeikyuuType As String = String.Empty
        Dim lgcJiban As New JibanLogic

        '�f�t�H���g�l�i�������E3�n��ȊO�j
        HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuNotKeiretu

        '�����X�f�[�^�̎擾
        recKameiten = lgcKameiten.GetKameitenSearchResult(Me.HiddenKubun.Value, Me.HiddenKameitenCd.Value, String.Empty)

        '�����X���擾�o���Ȃ��ꍇ�ɂ́A�������E3�n��ȊO����
        If recKameiten.KameitenCd Is Nothing Then
            Exit Sub
        End If

        '�����悪�ʂɎw�肳��Ă���ꍇ�A�f�t�H���g�̐�������㏑��
        If Me.ucSeikyuuSiireLink.AccSeikyuuSakiCd.Value <> String.Empty Then
            recSyouhin.SeikyuuSakiCd = Me.ucSeikyuuSiireLink.AccSeikyuuSakiCd.Value
            recSyouhin.SeikyuuSakiBrc = Me.ucSeikyuuSiireLink.AccSeikyuuSakiBrc.Value
            recSyouhin.SeikyuuSakiKbn = Me.ucSeikyuuSiireLink.AccSeikyuuSakiKbn.Value
        End If

        '�n��R�[�h�̐ݒ�
        Me.HiddenKeiretu.Value = recKameiten.KeiretuCd

        '3�n��̔���(1:3�n��A0:3�n��ȊO)
        intKeiretuFlg = lgcJiban.GetKeiretuFlg(recKameiten.KeiretuCd)

        '���i�������ꍇ�ɂ͌n��̂ݔ��f�i�f�t�H���g�������j
        If String.IsNullOrEmpty(recSyouhin.SyouhinCd) Then
            If intKeiretuFlg = 1 Then
                '�������E3�n��
                HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TaSeikyuuKeiretu
            Else
                '�������E3�n��ȊO
                HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TaSeikyuuNotKeiretu
            End If
            Exit Sub
        End If

        '�����XM�E���iM�ɂ�鐿���^�C�v�̔���
        If recSyouhin.SeikyuuSakiType = EarthConst.SEIKYU_TYOKUSETU Then
            '���ڐ���
            If intKeiretuFlg = 1 Then
                '3�n��
                HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuKeiretu
            Else
                '3�n��ȊO
                HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuNotKeiretu
            End If
        ElseIf recSyouhin.SeikyuuSakiType = EarthConst.SEIKYU_TASETU Then
            '������
            If intKeiretuFlg = 1 Then
                '3�n��
                HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TaSeikyuuKeiretu
            Else
                '3�n��ȊO
                HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TaSeikyuuNotKeiretu
            End If
        End If


    End Sub

#Region "��ʐ���"

    ''' <summary>
    ''' �R���g���[���̉�ʐ���/���i�S�i�󒍂��痈�����p�j
    ''' </summary>
    ''' <remarks>�R���g���[���̉�ʐ�����d�l���̗D�揇�ɋL�ڂ���</remarks>
    Public Sub SetEnableControl()
        Dim ht As New Hashtable
        Dim htNotTarget As New Hashtable '�ΏۊO

        jSM.Hash2Ctrl(TrSyouhin4Record, EarthConst.MODE_VIEW, ht, htNotTarget) 'Tr���i�S���R�[�h

        '���D�揇2
        '���㏈����(�@�ʐ����e�[�u��.����v��FLG��1)
        If Not String.IsNullOrEmpty(Me.HiddenUriageKeijyouDate.Value) Then
        Else
            '���D�揇3
            '���i�R�[�h����
            If Me.TextSyouhinCd.Text = String.Empty Then
                cl.chgDispSyouhinText(Me.TextSyouhinCd) '���i�R�[�h

                '���D�揇4
                '���i�R�[�h������
            Else
                cl.chgDispSyouhinText(Me.TextSyouhinCd) '���i�R�[�h

                cl.chgDispSyouhinPull(Me.SelectKakutei, Me.SpanKakutei)                     '�m���
                cl.chgDispSyouhinText(Me.TextSyoudakusyoKingaku)                            '���������z
                cl.chgDispSyouhinText(Me.TextSyouhizeiGaku)                                 '����Ŋz
                cl.chgDispSyouhinText(Me.TextSiireSyouhizeiGaku)                            '�d������Ŋz
                cl.chgDispSyouhinPull(Me.SelectSeikyuuUmu, Me.SpanSeikyuuUmu)               '�����L��
                cl.chgDispSyouhinPull(Me.SelectHattyuusyoKakutei, Me.SpanHattyuusyoKakutei) '�������m��

                '****************
                cl.chgDispSyouhinText(Me.TextMitumorisyoSakuseiDate)                        '���Ϗ��쐬��
                cl.chgDispSyouhinText(Me.TextUriageNengappi)                                '����N����
                cl.chgDispSyouhinText(Me.TextDenpyouUriageNengappi)                         '�`�[����N����
                cl.chgDispSyouhinText(Me.TextDenpyouSiireNengappi)                          '�`�[�d���N����
                cl.chgDispSyouhinText(Me.TextSeikyuusyoHakkouDate)                          '���������s��
                cl.chgDispSyouhinPull(Me.SelectUriageSyori, Me.SpanUriageSyori)             '���㏈��
                cl.chgDispSyouhinText(Me.TextHattyuusyoKakuninDate)                         '�������m�F��
                '****************

                '���D�揇5
                '�����L��=�L��A���A������=�������A���A�n��<>3�n��
                If Me.SelectSeikyuuUmu.SelectedValue = "1" Then '�L��
                    '������ and 3�n��ȊO
                    If Me.HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TaSeikyuuNotKeiretu Then
                        cl.chgDispSyouhinText(Me.TextJituSeikyuuGaku) '���������z

                        '���D�揇6
                        '�����L��=�L��A���A�D�揇5�ȊO
                    Else
                        cl.chgDispSyouhinText(Me.TextKoumutenSeikyuuGaku) '�H���X�������z
                        cl.chgDispSyouhinText(Me.TextJituSeikyuuGaku) '���������z
                    End If

                End If

            End If

        End If

        '���D�揇1
        '�������m��(�@�ʐ����e�[�u��.�������m��FLG��1)
        If Me.HiddenHattyuusyoKakuteiOld.Value = "1" Then '�m��
            '�������m��
            cl.chgVeiwMode(SelectHattyuusyoKakutei, SpanHattyuusyoKakutei)
        Else '���m��
            '���i�R�[�h
            If Me.TextSyouhinCd.Text = String.Empty Then '������
            Else '����
                cl.chgDispSyouhinPull(Me.SelectHattyuusyoKakutei, Me.SpanHattyuusyoKakutei) '�������m��

                '���D�揇7
                '�����L��
                If Me.SelectSeikyuuUmu.SelectedValue = "0" Then '����
                Else
                    '���D�揇8
                    '�������m��=1:�m��
                    If Me.SelectHattyuusyoKakutei.SelectedValue = "1" Then

                        '���D�揇9
                        '�D�揇8�ȊO
                    Else
                        cl.chgDispSyouhinText(Me.TextHattyuusyoKingaku) '���������z

                    End If
                End If
            End If
        End If

        '**********************************
        '�����ɂ��\���E���x�������������s
        '**********************************
        SetEnableControlKengen()


    End Sub

    ''' <summary>
    ''' �����ɂ���ʐ���
    ''' </summary>
    ''' <remarks>�o���Ɩ��E�˗��Ɩ��E�������Ǘ������ɂ�鐧����s��</remarks>
    Public Sub SetEnableControlKengen()

        '�e�����̎擾
        Dim Keiri As Integer = Me.HiddenKeiriGyoumuKengen.Value
        Dim Irai As Integer = Me.HiddenIraiGyoumuKengen.Value
        Dim Hattyuu As Integer = Me.HiddenHattyuusyoKanriKengen.Value

        ''�o�������������ꍇ�͔��㏈���E�`�[����N�����������x����
        'If HiddenKeiriGyoumuKengen.Value <> "-1" Then
        '    cl.chgVeiwMode(TextUriageNengappi)                              '����N����
        '    cl.chgVeiwMode(TextDenpyouUriageNengappi)                       '�`�[����N����
        '    cl.chgVeiwMode(TextDenpyouSiireNengappi)                        '�`�[�d���N����
        '    cl.chgVeiwMode(SelectUriageSyori, SpanUriageSyori)              '���㏈��
        '    cl.chgVeiwMode(TextSyouhizeiGaku)                               '����Ŋz
        '    cl.chgVeiwMode(TextSiireSyouhizeiGaku)                          '����Ŋz
        '    cl.chgVeiwMode(TextMitumorisyoSakuseiDate)                      '���Ϗ��쐬��
        '    cl.chgVeiwMode(TextSeikyuusyoHakkouDate)                        '���������s��
        '    cl.chgVeiwMode(TextHattyuusyoKakuninDate)                       '�������m�F��
        'ElseIf HiddenKeiriGyoumuKengen.Value = "-1" Then
        '    '�o������������ꍇ�ɂ͑S�Ďg�p��
        '    cl.chgDispSyouhinText(TextSyouhinCd)
        '    cl.chgDispSyouhinPull(SelectKakutei, SpanKakutei)
        '    cl.chgDispSyouhinText(TextKoumutenSeikyuuGaku)
        '    cl.chgDispSyouhinText(TextJituSeikyuuGaku)
        '    cl.chgDispSyouhinText(TextSyouhizeiGaku)
        '    cl.chgDispSyouhinText(TextSiireSyouhizeiGaku)
        '    cl.chgDispSyouhinText(TextSyoudakusyoKingaku)
        '    cl.chgDispSyouhinText(TextMitumorisyoSakuseiDate)
        '    cl.chgDispSyouhinText(TextUriageNengappi)
        '    cl.chgDispSyouhinText(TextDenpyouUriageNengappi)
        '    cl.chgDispSyouhinText(TextDenpyouSiireNengappi)
        '    cl.chgDispSyouhinPull(SelectSeikyuuUmu, SpanSeikyuuUmu)
        '    cl.chgDispSyouhinText(TextSeikyuusyoHakkouDate)
        '    cl.chgDispSyouhinPull(SelectUriageSyori, SpanUriageSyori)
        '    '�������֘A���g�p��
        '    cl.chgDispSyouhinText(TextHattyuusyoKingaku)
        '    cl.chgDispSyouhinPull(SelectHattyuusyoKakutei, SpanHattyuusyoKakutei)
        '    cl.chgDispSyouhinText(TextHattyuusyoKakuninDate)
        'End If

        ''�o�������y�ш˗��Ɩ������������ꍇ�A�������ȊO���x����
        'If (HiddenKeiriGyoumuKengen.Value <> "-1") And _
        '    (HiddenIraiGyoumuKengen.Value <> "-1") Then
        '    cl.chgVeiwMode(TextUriageNengappi)                              '����N����
        '    cl.chgVeiwMode(TextDenpyouUriageNengappi)                       '�`�[����N����
        '    cl.chgVeiwMode(TextDenpyouSiireNengappi)                        '�`�[�d���N����
        '    cl.chgVeiwMode(SelectUriageSyori, SpanUriageSyori)              '���㏈��
        '    cl.chgVeiwMode(SelectKakutei, SpanKakutei)                      '�m��敪
        '    cl.chgVeiwMode(TextKoumutenSeikyuuGaku)                         '�H���X�������z
        '    cl.chgVeiwMode(TextJituSeikyuuGaku)                             '�������Ŕ����z
        '    cl.chgVeiwMode(TextSyouhizeiGaku)                               '����Ŋz
        '    cl.chgVeiwMode(TextSiireSyouhizeiGaku)                          '�d������Ŋz
        '    cl.chgVeiwMode(TextSyoudakusyoKingaku)                          '���������z
        '    cl.chgVeiwMode(TextMitumorisyoSakuseiDate)                      '���Ϗ��쐬��
        '    cl.chgVeiwMode(SelectSeikyuuUmu, SpanSeikyuuUmu)                '�����L��
        '    cl.chgVeiwMode(TextSeikyuusyoHakkouDate)                        '���������s��
        '    cl.chgVeiwMode(TextHattyuusyoKakuninDate)                       '�������m�F��
        '    cl.chgVeiwMode(TextSyouhinCd)                                   '���i�R�[�h
        '    Me.ButtonSyouhinKensaku.Visible = False                         '���i�����{�^��
        'End If

        ''�o�������E�˗������E�����������������ꍇ�S�ă��x����
        'If (HiddenKeiriGyoumuKengen.Value <> "-1") And _
        '    (HiddenIraiGyoumuKengen.Value <> "-1") And _
        '    (HiddenHattyuusyoKanriKengen.Value <> "-1") Then
        '    cl.chgVeiwMode(TextUriageNengappi)                              '����N����
        '    cl.chgVeiwMode(TextDenpyouUriageNengappi)                       '�`�[����N����
        '    cl.chgVeiwMode(TextDenpyouSiireNengappi)                        '�`�[�d���N����
        '    cl.chgVeiwMode(SelectUriageSyori, SpanUriageSyori)              '���㏈��
        '    cl.chgVeiwMode(SelectKakutei, SpanKakutei)                      '�m��敪
        '    cl.chgVeiwMode(TextKoumutenSeikyuuGaku)                         '�H���X�������z
        '    cl.chgVeiwMode(TextJituSeikyuuGaku)                             '�������Ŕ����z
        '    cl.chgVeiwMode(TextSyouhizeiGaku)                               '����Ŋz
        '    cl.chgVeiwMode(TextSiireSyouhizeiGaku)                          '�d������Ŋz
        '    cl.chgVeiwMode(TextSyoudakusyoKingaku)                          '���������z
        '    cl.chgVeiwMode(TextMitumorisyoSakuseiDate)                      '���Ϗ��쐬��
        '    cl.chgVeiwMode(SelectSeikyuuUmu, SpanSeikyuuUmu)                '�����L��
        '    cl.chgVeiwMode(TextSeikyuusyoHakkouDate)                        '���������s��
        '    cl.chgVeiwMode(TextHattyuusyoKakuninDate)                       '�������m�F��
        '    cl.chgVeiwMode(TextSyouhinCd)                                   '���i�R�[�h
        '    Me.ButtonSyouhinKensaku.Visible = False                         '���i�����{�^��
        '    cl.chgVeiwMode(TextHattyuusyoKingaku)                           '���������z
        '    cl.chgVeiwMode(SelectHattyuusyoKakutei, SpanHattyuusyoKakutei)  '�������m��
        'End If

        If HiddenKeiriGyoumuKengen.Value = "-1" Then
            '�o������������ꍇ�ɂ͑S�Ďg�p��
            cl.chgDispSyouhinText(TextSyouhinCd)
            cl.chgDispSyouhinPull(SelectKakutei, SpanKakutei)
            cl.chgDispSyouhinText(TextKoumutenSeikyuuGaku)
            cl.chgDispSyouhinText(TextJituSeikyuuGaku)
            cl.chgDispSyouhinText(TextSyouhizeiGaku)
            cl.chgDispSyouhinText(TextSiireSyouhizeiGaku)
            cl.chgDispSyouhinText(TextSyoudakusyoKingaku)
            cl.chgDispSyouhinText(TextMitumorisyoSakuseiDate)
            cl.chgDispSyouhinText(TextUriageNengappi)
            cl.chgDispSyouhinText(TextDenpyouUriageNengappi)
            cl.chgDispSyouhinText(TextDenpyouSiireNengappi)
            cl.chgDispSyouhinPull(SelectSeikyuuUmu, SpanSeikyuuUmu)
            cl.chgDispSyouhinText(TextSeikyuusyoHakkouDate)
            cl.chgDispSyouhinPull(SelectUriageSyori, SpanUriageSyori)
            '�������֘A���g�p��
            cl.chgDispSyouhinText(TextHattyuusyoKingaku)
            cl.chgDispSyouhinPull(SelectHattyuusyoKakutei, SpanHattyuusyoKakutei)
            cl.chgDispSyouhinText(TextHattyuusyoKakuninDate)
        Else
            '�o�������������ꍇ�ɂ͑S�ă��x����
            cl.chgVeiwMode(TextUriageNengappi)                              '����N����
            cl.chgVeiwMode(TextDenpyouUriageNengappi)                       '�`�[����N����
            cl.chgVeiwMode(TextDenpyouSiireNengappi)                        '�`�[�d���N����
            cl.chgVeiwMode(SelectUriageSyori, SpanUriageSyori)              '���㏈��
            cl.chgVeiwMode(SelectKakutei, SpanKakutei)                      '�m��敪
            cl.chgVeiwMode(TextKoumutenSeikyuuGaku)                         '�H���X�������z
            cl.chgVeiwMode(TextJituSeikyuuGaku)                             '�������Ŕ����z
            cl.chgVeiwMode(TextSyouhizeiGaku)                               '����Ŋz
            cl.chgVeiwMode(TextSiireSyouhizeiGaku)                          '�d������Ŋz
            cl.chgVeiwMode(TextSyoudakusyoKingaku)                          '���������z
            cl.chgVeiwMode(TextMitumorisyoSakuseiDate)                      '���Ϗ��쐬��
            cl.chgVeiwMode(SelectSeikyuuUmu, SpanSeikyuuUmu)                '�����L��
            cl.chgVeiwMode(TextSeikyuusyoHakkouDate)                        '���������s��
            cl.chgVeiwMode(TextHattyuusyoKakuninDate)                       '�������m�F��
            cl.chgVeiwMode(TextSyouhinCd)                                   '���i�R�[�h
            Me.ButtonSyouhinKensaku.Visible = False                         '���i�����{�^��
            cl.chgVeiwMode(TextHattyuusyoKingaku)                           '���������z
            cl.chgVeiwMode(SelectHattyuusyoKakutei, SpanHattyuusyoKakutei)  '�������m��
            cl.chgVeiwMode(TextHattyuusyoKakuninDate)                       '�������m�F��
        End If

    End Sub

#End Region

#Region "�R���g���[���̊�������"
    ''' <summary>
    ''' �R���g���[���̏�����
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub initCtrl(ByVal enabled As Boolean)

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".initCtrl")

        If enabled = False Then
            '[���x����]
            Dim ht As New Hashtable
            Dim htNotTarget As New Hashtable '�ΏۊO
            htNotTarget.Add(Me.TextSyouhinCd.ID, Me.TextSyouhinCd)  '���i�R�[�h
            jSM.Hash2Ctrl(TrSyouhin4Record, EarthConst.MODE_VIEW, ht, htNotTarget) 'Tr���i�S���R�[�h

            '[�f�[�^�̃N���A]
            Me.TextSyouhinCd.Text = String.Empty                '���i�R�[�h
            Me.SpanKakutei.InnerHtml = String.Empty             '�m��敪
            Me.SpanSyouhinMei.InnerText = String.Empty          '���i��
            Me.TextKoumutenSeikyuuGaku.Text = String.Empty      '�H���X�����Ŕ����z
            Me.TextJituSeikyuuGaku.Text = String.Empty          '�������Ŕ����z
            Me.TextSyouhizeiGaku.Text = String.Empty            '����Ŋz
            Me.TextSiireSyouhizeiGaku.Text = String.Empty       '�d������Ŋz
            Me.TextZeikomiKingaku.Text = String.Empty           '�ō����z
            Me.TextSyoudakusyoKingaku.Text = String.Empty       '���������z
            Me.TextMitumorisyoSakuseiDate.Text = String.Empty   '���Ϗ��쐬��
            Me.TextUriageNengappi.Text = String.Empty           '����N����
            Me.TextDenpyouUriageNengappiDisplay.Text = String.Empty     '�`�[����N����(�Q��)
            Me.TextDenpyouUriageNengappi.Text = String.Empty    '�`�[����N����(�C��)
            Me.TextDenpyouSiireNengappiDisplay.Text = String.Empty      '�`�[�d���N����(�Q��)
            Me.TextDenpyouSiireNengappi.Text = String.Empty     '�`�[�d���N����(�C��)
            Me.SpanSeikyuuUmu.InnerHtml = String.Empty          '�����L��
            Me.TextSeikyuusyoHakkouDate.Text = String.Empty     '������������
            Me.SpanUriageSyori.InnerHtml = String.Empty         '���㏈��
            Me.TextUriageDate.Text = String.Empty               '���㏈����
            Me.TextHattyuusyoKingaku.Text = String.Empty        '���������z
            Me.SpanHattyuusyoKakutei.InnerHtml = String.Empty   '�������m��
            Me.TextHattyuusyoKakuninDate.Text = String.Empty    '�������m�F��
            Me.HiddenHattyuusyoKakuteiOld.Value = String.Empty  '�������m��FLG

        Else
            cl.chgDispSyouhinText(Me.TextKoumutenSeikyuuGaku)                           '�H���X�������z
            cl.chgDispSyouhinText(Me.TextJituSeikyuuGaku)                               '�������Ŕ����z
            cl.chgDispSyouhinText(Me.TextSyouhizeiGaku)                                 '����Ŋz
            cl.chgDispSyouhinText(Me.TextSiireSyouhizeiGaku)                            '�d������Ŋz
            cl.chgDispSyouhinText(Me.TextSyoudakusyoKingaku)                            '���������z
            cl.chgDispSyouhinText(Me.TextMitumorisyoSakuseiDate)                        '���Ϗ��쐬��
            cl.chgDispSyouhinText(Me.TextUriageNengappi)                                '����N����
            cl.chgDispSyouhinText(Me.TextDenpyouUriageNengappi)                         '�`�[����N����
            cl.chgDispSyouhinText(Me.TextDenpyouSiireNengappi)                          '�`�[�d���N����
            cl.chgDispSyouhinText(Me.TextSeikyuusyoHakkouDate)                          '���������s��
            cl.chgDispSyouhinText(Me.TextHattyuusyoKingaku)                             '���������z
            cl.chgDispSyouhinText(Me.TextHattyuusyoKakuninDate)                         '�������m�F��
            cl.chgDispSyouhinPull(Me.SelectKakutei, Me.SpanKakutei)                     '�m���
            cl.chgDispSyouhinPull(Me.SelectSeikyuuUmu, Me.SpanSeikyuuUmu)               '�����L��
            cl.chgDispSyouhinPull(Me.SelectUriageSyori, Me.SpanUriageSyori)             '���㏈��
            cl.chgDispSyouhinPull(Me.SelectHattyuusyoKakutei, Me.SpanHattyuusyoKakutei) '�������m��
        End If

    End Sub

#End Region

#Region "���i���n"

    ''' <summary>
    ''' ���i�����{�^���������̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonSyouhinKensaku_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSyouhinKensaku.ServerClick

        If HiddenSyouhin4SearchType.Value <> "1" Then
            TextSyouhinCd_ChangeSub(sender, e)
        Else
            '�R�[�honchange�ŌĂ΂ꂽ�ꍇ
            TextSyouhinCd_ChangeSub(sender, e, False)
            HiddenSyouhin4SearchType.Value = String.Empty
        End If


    End Sub

    ''' <summary>
    ''' ���i�����{�^���������̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TextSyouhinCd_ChangeSub(ByVal sender As System.Object, _
                                ByVal e As System.EventArgs, _
                                Optional ByVal ProcType As Boolean = True)

        ' ���͂���Ă���R�[�h���L�[�ɁA�}�X�^���������A�f�[�^���擾�ł����ꍇ��
        ' ��ʏ����X�V����B�f�[�^�������ꍇ�A������ʂ�\������
        Dim SyouhinSearchLogic As New SyouhinSearchLogic
        Dim total_row As Integer

        ' ���[�h�̎擾
        'Dim mode As Integer = Integer.Parse(HiddenDispMode.Value)

        '���i���������s
        Dim dataArray As List(Of SyouhinMeisaiRecord) = SyouhinSearchLogic.GetSyouhinInfo(TextSyouhinCd.Text, _
                                                                                      "", _
                                                                                      EarthEnum.EnumSyouhinKubun.Syouhin4, _
                                                                                      total_row)
        If dataArray.Count = 1 Then
            '���i������ʂɃZ�b�g
            Dim recData As SyouhinMeisaiRecord = dataArray(0)

            '�t�H�[�J�X�Z�b�g
            masterAjaxSM.SetFocus(ButtonSyouhinKensaku)
        ElseIf ProcType = True Then
            '�����|�b�v�A�b�v���N��

            Dim tmpFocusScript = "objEBI('" & ButtonSyouhinKensaku.ClientID & "').focus();"

            '������ʕ\���pJavaScript�wcallSearch�x�����s
            Dim tmpScript = "callSearch('" & TextSyouhinCd.ClientID & EarthConst.SEP_STRING & _
                                        HiddenTargetId.ClientID & _
                                        "','" & UrlConst.SEARCH_SYOUHIN & "','" & _
                                        TextSyouhinCd.ClientID & "','" & _
                                        ButtonSyouhinKensaku.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)

            Exit Sub

        End If

        '���i������ʂɐݒ�
        SetSyouhin(sender, e, ProcType)

        ' ���i�R�[�h��ۑ�
        HiddenSyouhinCdOld.Value = TextSyouhinCd.Text

    End Sub

    ''' <summary>
    ''' ���i���̐ݒ�i���i�R�[�h���m�肵�Ă����Ԃ�Call����j
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="ProcType"></param>
    ''' <remarks></remarks>
    Public Sub SetSyouhin(ByVal sender As Object, ByVal e As System.EventArgs, _
                                Optional ByVal ProcType As Boolean = True)

        Dim intCntTotal As Integer
        Dim logic As New JibanLogic
        Dim lgcSyouhin As New SyouhinSearchLogic
        Dim lstSyouhin23 As New List(Of Syouhin23Record)
        Dim recSyouhin23 As New Syouhin23Record
        Dim lgcJiban As New JibanLogic
        Dim strZeiKbn As String = String.Empty              '�ŋ敪
        Dim strZeiritu As String = String.Empty             '�ŗ�
        Dim strSyouhinCd As String = String.Empty           '���i�R�[�h
        ' �擾�p���W�b�N�N���X
        Dim cBizLogic As New CommonBizLogic

        If Trim(Me.TextSyouhinCd.Text) = String.Empty Then

            '���������z�̎擾
            Dim strHattyuuGaku As String = IIf(Me.TextHattyuusyoKingaku.Text = String.Empty, "0", Me.TextHattyuusyoKingaku.Text)

            If strHattyuuGaku <> "0" Then
                Me.TextSyouhinCd.Text = Me.HiddenSyouhinCdOld.Value
                '�N���A�o���Ȃ�MSG
                ScriptManager.RegisterClientScriptBlock(sender, _
                                        sender.GetType(), _
                                        "alert", _
                                        "alert('" & _
                                        Messages.MSG010E & _
                                        "')", True)
                setFocusAJ(Me.TextSyouhinCd)
                Exit Sub
            End If

            '�R���g���[���̏�����(�񊈐����j
            initCtrl(False)
            setFocusAJ(Me.ButtonSyouhinKensaku)
            Exit Sub

        End If

        '���i���̎擾
        lstSyouhin23 = logic.GetSyouhin23(Me.TextSyouhinCd.Text, _
                                        String.Empty, _
                                        EarthEnum.EnumSyouhinKubun.Syouhin4, _
                                        intCntTotal, _
                                        Integer.MinValue, _
                                        Me.HiddenKameitenCd.Value)
        If lstSyouhin23.Count > 0 Then
            recSyouhin23 = lstSyouhin23(0)
        Else
            initCtrl(False)
            Exit Sub
        End If

        '��������̐ݒ�
        GetSeikyuuInfo(recSyouhin23)

        '����N�����Ŕ��f���āA�������ŗ����擾����
        strSyouhinCd = Me.TextSyouhinCd.Text
        If cBizLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
            '�擾�����ŋ敪�E�ŗ����Z�b�g
            recSyouhin23.Zeiritu = strZeiritu
            recSyouhin23.ZeiKbn = strZeiKbn
        End If

        '�R�[�h�E���̂��Z�b�g
        Me.TextSyouhinCd.Text = recSyouhin23.SyouhinCd        '���i�R�[�h
        Me.HiddenBunruiCd.Value = recSyouhin23.SoukoCd        '���ރR�[�h�i���i4�j
        Me.SpanSyouhinMei.InnerHtml = recSyouhin23.SyouhinMei '���i��
        Me.HiddenZeiritu.Value = recSyouhin23.Zeiritu         '�ŗ�
        Me.HiddenZeikubun.Value = recSyouhin23.ZeiKbn         '�ŋ敪

        '�@�ʐ������̎擾
        ' ���擾�p�̃p�����[�^�N���X
        Dim syouhin23_info As New Syouhin23InfoRecord
        syouhin23_info.Syouhin2Rec = New Syouhin23Record

        '���i�E��ʏ������R�[�h�ɃZ�b�g
        syouhin23_info.Syouhin2Rec.SyouhinCd = recSyouhin23.SyouhinCd                                 '���i�R�[�h
        syouhin23_info.Syouhin2Rec.ZeiKbn = recSyouhin23.ZeiKbn                                       '�ŋ敪
        syouhin23_info.Syouhin2Rec.SoukoCd = recSyouhin23.SoukoCd                                     '�q�ɃR�[�h
        If Me.HiddenSyouhinCdOld.Value <> recSyouhin23.SyouhinCd Then ' �㑱�Ŏg�p���Ȃ��̂Ő�����^�d����ύX���͂��̂܂�
            syouhin23_info.Syouhin2Rec.SiireKkk = recSyouhin23.SiireKkk                                   '�d�����i(���������z)
        Else
            recSyouhin23.SiireKkk = TextSyoudakusyoKingaku.Text
            syouhin23_info.Syouhin2Rec.SiireKkk = recSyouhin23.SiireKkk
        End If
        syouhin23_info.Syouhin2Rec.HyoujunKkk = recSyouhin23.HyoujunKkk                               '�W�����i
        syouhin23_info.Syouhin2Rec.SiireKkk = recSyouhin23.SiireKkk                                   '�d�����i
        syouhin23_info.SeikyuuUmu = SelectSeikyuuUmu.SelectedValue                                  '�����L��
        syouhin23_info.HattyuusyoKakuteiFlg = Integer.Parse(SelectHattyuusyoKakutei.SelectedValue)  '�������m��t���O
        syouhin23_info.KeiretuCd = Me.HiddenKeiretu.Value                                        '�n��R�[�h
        syouhin23_info.KeiretuFlg = lgcJiban.GetKeiretuFlg(Me.HiddenKeiretu.Value)                        '�n��t���O(3�n��or�ȊO)
        If Me.HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuKeiretu Or _
            Me.HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuNotKeiretu Then
            syouhin23_info.Seikyuusaki = EarthConst.SEIKYU_TYOKUSETU                                '�����^�C�v(����or��)
        Else
            syouhin23_info.Seikyuusaki = EarthConst.SEIKYU_TASETU
        End If

        ' �������R�[�h�̎擾(�m���Ɍ��ʂ��L��)
        Dim recTeiSei As TeibetuSeikyuuRecord = logic.GetSyouhin23SeikyuuData(sender, syouhin23_info, 1)

        '���i�����Z�b�g
        TextKoumutenSeikyuuGaku.Text = recTeiSei.KoumutenSeikyuuGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
        TextJituSeikyuuGaku.Text = recTeiSei.UriGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
        TextSyouhizeiGaku.Text = (Fix(recTeiSei.UriGaku * recSyouhin23.Zeiritu)).ToString(EarthConst.FORMAT_KINGAKU_1)
        TextZeikomiKingaku.Text = recTeiSei.UriGaku + Fix(recTeiSei.UriGaku * recSyouhin23.Zeiritu).ToString(EarthConst.FORMAT_KINGAKU_1)
        TextSyoudakusyoKingaku.Text = recSyouhin23.SiireKkk.ToString(EarthConst.FORMAT_KINGAKU_1)

        ' �ŋ��z�Đݒ�
        SetZeigaku(e)

        ' �d������Ŋz�ݒ�
        SetSiireZeigaku()

        ' ���������s���E����N�����̐ݒ�
        If Me.HiddenKeiriGyoumuKengen.Value <> "-1" Then
            setHakkoubi()
        End If

        '�y�����ɂ�銈������z
        SetEnableControl()

        '�V�K�s�ǉ����ɉ�ʂ̓�����ݒ�
        SetDispAction()

    End Sub

#End Region

#Region "�h���b�v�_�E�����X�g�n"

    ''' <summary>
    ''' �m��h���b�v�_�E�����X�g�ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectKakutei_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        ' ���������s���A����N������ݒ肵�܂�
        If Me.HiddenKeiriGyoumuKengen.Value <> "-1" Then
            setHakkoubi()
        End If

        setFocusAJ(SelectKakutei)

    End Sub

    ''' <summary>
    ''' �����L���h���b�v�_�E�����X�g�ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectSeikyuuUmu_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If SelectSeikyuuUmu.SelectedValue = "1" Then

            '���i���̎擾
            Dim lstSyouhin23 As New List(Of Syouhin23Record)
            Dim recSyouhin23 As New Syouhin23Record
            Dim intCnt As Integer = 0
            Dim logic As New JibanLogic

            lstSyouhin23 = logic.GetSyouhin23(HiddenSyouhinCdOld.Value, _
                                              String.Empty, _
                                              EarthEnum.EnumSyouhinKubun.Syouhin4, _
                                              intCnt, _
                                              Integer.MinValue, _
                                              Me.HiddenKameitenCd.Value)
            If lstSyouhin23.Count > 0 Then
                recSyouhin23 = lstSyouhin23(0)
            Else
                initCtrl(False)
                Exit Sub
            End If

            '��������̐ݒ�
            GetSeikyuuInfo(recSyouhin23)

            '�@�ʐ������̎擾
            ' ���擾�p�̃p�����[�^�N���X
            Dim syouhin23_info As New Syouhin23InfoRecord
            syouhin23_info.Syouhin2Rec = New Syouhin23Record

            '�R���g���[���̐ݒ�i�������j
            initCtrl(True)

            '���i�E��ʏ������R�[�h�ɃZ�b�g
            syouhin23_info.Syouhin2Rec.SyouhinCd = recSyouhin23.SyouhinCd                                 '���i�R�[�h
            syouhin23_info.Syouhin2Rec.ZeiKbn = recSyouhin23.ZeiKbn                                       '�ŋ敪
            syouhin23_info.Syouhin2Rec.SoukoCd = recSyouhin23.SoukoCd                                     '�q�ɃR�[�h
            syouhin23_info.Syouhin2Rec.HyoujunKkk = recSyouhin23.HyoujunKkk                               '�W�����i
            syouhin23_info.Syouhin2Rec.SiireKkk = recSyouhin23.SiireKkk                                   '�d�����i
            syouhin23_info.SeikyuuUmu = SelectSeikyuuUmu.SelectedValue                                    '�����L��
            syouhin23_info.HattyuusyoKakuteiFlg = Integer.Parse(SelectHattyuusyoKakutei.SelectedValue)    '�������m��t���O
            syouhin23_info.KeiretuCd = Me.HiddenKeiretu.Value                                             '�n��R�[�h
            syouhin23_info.KeiretuFlg = logic.GetKeiretuFlg(Me.HiddenKeiretu.Value)                       '�n��t���O(3�n��or�ȊO)
            If HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuKeiretu Or _
                HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuNotKeiretu Then
                syouhin23_info.Seikyuusaki = EarthConst.SEIKYU_TYOKUSETU                                  '�����^�C�v(����or��)
            Else
                syouhin23_info.Seikyuusaki = EarthConst.SEIKYU_TASETU
            End If

            ' �������R�[�h�̎擾(�m���Ɍ��ʂ��L��)
            Dim recTeiSei As TeibetuSeikyuuRecord = logic.GetSyouhin23SeikyuuData(sender, syouhin23_info, 1)

            '���i�����Z�b�g
            TextKoumutenSeikyuuGaku.Text = recTeiSei.KoumutenSeikyuuGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
            TextJituSeikyuuGaku.Text = recTeiSei.UriGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
            TextSyouhizeiGaku.Text = (Fix(recTeiSei.UriGaku * recSyouhin23.Zeiritu)).ToString(EarthConst.FORMAT_KINGAKU_1)
            TextZeikomiKingaku.Text = recTeiSei.UriGaku + Fix(recTeiSei.UriGaku * recSyouhin23.Zeiritu).ToString(EarthConst.FORMAT_KINGAKU_1)

            ' �ŋ��z�Đݒ�
            SetZeigaku(e)

        Else
            ' ���������̏ꍇ
            TextKoumutenSeikyuuGaku.Text = "0"
            TextJituSeikyuuGaku.Text = "0"
            SetZeigaku(e)
            Me.TextSeikyuusyoHakkouDate.Text = String.Empty
        End If

        '�y��ʐ���z
        SetEnableControl()

        '���������s���E����N������ݒ�
        If Me.HiddenKeiriGyoumuKengen.Value <> "-1" Then
            setHakkoubi(True)
        End If

        '�t�H�[�J�X��ݒ�
        setFocusAJ(SelectSeikyuuUmu)

    End Sub

    ''' <summary>
    ''' ���㏈���h���b�v�_�E�����X�g�ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectUriageSyori_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If SelectUriageSyori.SelectedValue = "0" Then
            ' ���㏈�������N���A
            TextUriageDate.Text = String.Empty
        Else
            ' �V�X�e�����t���Z�b�g
            TextUriageDate.Text = Date.Now.ToString("yyyy/MM/dd")
        End If

        '�t�H�[�J�X�Z�b�g
        setFocusAJ(SelectUriageSyori)


    End Sub

    ''' <summary>
    ''' �������m��h���b�v�_�E�����X�g�ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectHattyuusyoKakutei_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' �����������m�菈��
        FunCheckHKakutei(1, sender)
        setFocusAJ(SelectHattyuusyoKakutei)

        ' Old�l�Ɍ��݂̒l���Z�b�g
        HiddenHattyuusyoFlgOld.Value = SelectHattyuusyoKakutei.SelectedValue

    End Sub

    ''' <summary>
    ''' �����N�����C����N������ݒ肵�܂�
    ''' </summary>
    ''' <param name="flgUriDateForce">����N�����������Ŏ����ݒ�FLG</param>
    ''' <remarks></remarks>
    Private Sub setHakkoubi(Optional ByVal flgUriDateForce As Boolean = False)

        ' ����E�����N�����擾�p���W�b�N�N���X
        Dim logic As New JibanLogic
        ' �l���擾����ׂ̓@�ʐ������R�[�h
        Dim teibetu_rec As New TeibetuSeikyuuRecord

        ' �m��敪
        cl.SetDisplayString(Me.SelectKakutei.SelectedIndex, teibetu_rec.KakuteiKbn)
        ' ���������s��
        cl.SetDisplayString(Me.TextSeikyuusyoHakkouDate.Text, teibetu_rec.SeikyuusyoHakDate)
        ' ����N����
        cl.SetDisplayString(Me.TextUriageNengappi.Text, teibetu_rec.UriDate)
        ' �����L��
        cl.SetDisplayString(Me.SelectSeikyuuUmu.SelectedValue, teibetu_rec.SeikyuuUmu)
        ' �敪
        teibetu_rec.Kbn = Me.HiddenKubun.Value
        ' ���i�R�[�h
        cl.SetDisplayString(Me.TextSyouhinCd.Text, teibetu_rec.SyouhinCd)

        '��������̃Z�b�g
        teibetu_rec.SeikyuuSakiCd = Me.ucSeikyuuSiireLink.AccSeikyuuSakiCd.Value
        teibetu_rec.SeikyuuSakiBrc = Me.ucSeikyuuSiireLink.AccSeikyuuSakiBrc.Value
        teibetu_rec.SeikyuuSakiKbn = Me.ucSeikyuuSiireLink.AccSeikyuuSakiKbn.Value

        ' ����E�����N�����擾
        logic.SubChangeKakutei(Me.HiddenKameitenCd.Value, teibetu_rec)

        ' ���������s���ݒ�
        TextSeikyuusyoHakkouDate.Text = cl.GetDisplayString(teibetu_rec.SeikyuusyoHakDate)
        ' ����N�����E�`�[����N�����ݒ�
        If flgUriDateForce And (Me.SelectKakutei.SelectedIndex = 1) Then
            TextUriageNengappi.Text = Date.Now.ToString("yyyy/MM/dd")
            TextDenpyouUriageNengappi.Text = Date.Now.ToString("yyyy/MM/dd")
        ElseIf Not flgUriDateForce Then
            TextUriageNengappi.Text = cl.GetDisplayString(teibetu_rec.UriDate)
            TextDenpyouUriageNengappi.Text = cl.GetDisplayString(teibetu_rec.UriDate)
        End If


    End Sub

#End Region

#Region "���z�n"

    ''' <summary>
    ''' �H���X�����Ŕ����z�ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextKoumutenSeikyuuGaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextKoumutenSeikyuuGaku.TextChanged

        ' ��ʂ̍H���X�������z�𐔒l�^�ɕϊ�
        Dim koumutengaku As Integer = 0

        If Not Me.TextKoumutenSeikyuuGaku.Text.Trim() = String.Empty Then
            koumutengaku = Integer.Parse(TextKoumutenSeikyuuGaku.Text.Replace(",", ""))
        End If

        TextKoumutenSeikyuuGaku.Text = koumutengaku.ToString(EarthConst.FORMAT_KINGAKU_1)

        ' ��������
        If HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuKeiretu Or _
           HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuNotKeiretu Then

            '**************************************************
            ' ���ڐ���
            '**************************************************
            ' ���H���X���z�����������z�ɐݒ�
            TextKoumutenSeikyuuGaku.Text = koumutengaku.ToString(EarthConst.FORMAT_KINGAKU_1)
            TextJituSeikyuuGaku.Text = koumutengaku.ToString(EarthConst.FORMAT_KINGAKU_1)
        Else
            '**************************************************
            ' �������i�n��j���n��ȊO�͓��͕s�ׁ̈A�֌W�Ȃ�
            '**************************************************
            If HiddenKingakuFlg.Value <> "2" Then

                Dim logic As New JibanLogic
                Dim zeinukiGaku As Integer = 0

                If logic.GetSeikyuuGaku(sender, _
                                        5, _
                                        HiddenKeiretu.Value, _
                                        TextSyouhinCd.Text, _
                                        koumutengaku, _
                                        zeinukiGaku) Then

                    ' ���������z�փZ�b�g
                    TextJituSeikyuuGaku.Text = zeinukiGaku.ToString(EarthConst.FORMAT_KINGAKU_1)

                    ' �H���X�������z�Ǝ������z�̎����ݒ萧�䔻��p�t���O�Z�b�g
                    HiddenKingakuFlg.Value = "1"

                End If
            End If
        End If

        SetZeigaku(e)

        '�t�H�[�J�X�Z�b�g
        setFocusAJ(TextJituSeikyuuGaku)

    End Sub

    ''' <summary>
    ''' �������Ŕ����z�ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextJituSeikyuuGaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextJituSeikyuuGaku.TextChanged

        '���������z�𐔒l�^�ɕϊ��A���z�t�H�[�}�b�g���w��
        Dim jitugaku As Integer = 0
        If Me.TextJituSeikyuuGaku.Text.Trim() <> String.Empty Then
            jitugaku = Integer.Parse(TextJituSeikyuuGaku.Text.Replace(",", ""))
            TextJituSeikyuuGaku.Text = jitugaku.ToString(EarthConst.FORMAT_KINGAKU_1)
        Else
            SetZeigaku(e)
            setFocusAJ(TextSyouhizeiGaku)
            Exit Sub
        End If

        ' ��������
        If HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuKeiretu Or _
           HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuNotKeiretu Then

            '**************************************************
            ' ���ڐ���
            '**************************************************
            ' �����������z���H���X���z�ɐݒ�
            TextJituSeikyuuGaku.Text = jitugaku.ToString(EarthConst.FORMAT_KINGAKU_1)
            TextKoumutenSeikyuuGaku.Text = jitugaku.ToString(EarthConst.FORMAT_KINGAKU_1)

        ElseIf HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TaSeikyuuKeiretu Then
            '**************************************************
            ' �������i�n��j���n��ȊO�͐ݒ�Ȃ�
            '**************************************************
            If HiddenKingakuFlg.Value <> "1" Then

                Dim koumutenGaku As Integer = 0
                Dim logic As New JibanLogic

                ' �����z�Z�o���\�b�h�ւ̈����ݒ�i���i�P�̏ꍇ�̂�6,����4�j
                Dim param As Integer = 4

                ' �����z���Z�o����
                If logic.GetSeikyuuGaku(sender, _
                                        param, _
                                        HiddenKeiretu.Value, _
                                        TextSyouhinCd.Text, _
                                        jitugaku, _
                                        koumutenGaku) Then

                    ' �H���X�������z�Z�b�g
                    TextKoumutenSeikyuuGaku.Text = koumutenGaku.ToString(EarthConst.FORMAT_KINGAKU_1)

                    ' �H���X�������z�Ǝ������z�̎����ݒ萧�䔻��p�t���O�Z�b�g
                    HiddenKingakuFlg.Value = "2"

                End If
            End If
        End If

        SetZeigaku(e)

        '�t�H�[�J�X�Z�b�g
        setFocusAJ(TextSyouhizeiGaku)

    End Sub

    ''' <summary>
    ''' ���������z�ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextSyoudakusyoKingaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextSyoudakusyoKingaku.TextChanged

        If sender.Text.Trim() = "" Then
            sender.Text = "0"
        End If

        Dim jitugaku As Integer = Integer.Parse(sender.Text.Replace(",", ""))
        Dim zeigaku As Integer = 0
        If IsNumeric(Me.HiddenZeiritu.Value) = False Then
            Me.HiddenZeiritu.Value = "0"
        End If
        zeigaku = Fix(jitugaku * Decimal.Parse(HiddenZeiritu.Value))

        TextSiireSyouhizeiGaku.Text = zeigaku.ToString(EarthConst.FORMAT_KINGAKU_1)

        '�t�H�[�J�X�Z�b�g
        setFocusAJ(TextDenpyouSiireNengappi)

    End Sub

    ''' <summary>
    ''' �d������Ŋz�ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextSiireSyouhizeiGaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextSiireSyouhizeiGaku.TextChanged

        If sender.Text.Trim() = "" Then
            sender.Text = "0"
        End If

        '�t�H�[�J�X�Z�b�g
        setFocusAJ(TextDenpyouSiireNengappi)

    End Sub

    ''' <summary>
    ''' ����Ŋz�z�ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextSyouhizeiGaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextSyouhizeiGaku.TextChanged

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".TextSyouhizeiGaku_TextChanged", sender)

        Dim intJitugaku As Integer = 0
        Dim intZeigaku As Integer = 0

        '�������Ŕ����z�̎擾
        If Me.TextJituSeikyuuGaku.Text.Trim() = String.Empty Or Me.TextJituSeikyuuGaku.Text.Trim() = "0" Then
            Me.TextSyouhizeiGaku.Text = "0"
            Me.TextZeikomiKingaku.Text = "0"
            Exit Sub
        Else
            intJitugaku = Integer.Parse(TextJituSeikyuuGaku.Text.Replace(",", ""))
        End If

        '����Ŋz�̎擾
        If Me.TextSyouhizeiGaku.Text.Trim() <> String.Empty Then
            intZeigaku = Integer.Parse(TextSyouhizeiGaku.Text.Replace(",", ""))
        Else
            Me.TextSyouhizeiGaku.Text = "0"
        End If

        '�ō����z�̍Čv�Z
        Me.TextZeikomiKingaku.Text = (intJitugaku + intZeigaku).ToString(EarthConst.FORMAT_KINGAKU_1)

        '�t�H�[�J�X�̐ݒ�
        setFocusAJ(TextDenpyouUriageNengappi)

    End Sub

    ''' <summary>
    ''' ���������z�ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextHattyuusyoKingaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextHattyuusyoKingaku.TextChanged
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".TextHattyuusyoKingaku_TextChanged", _
                                                    sender)

        Dim hattyuuKingaku As Integer = Integer.MinValue

        CommonLogic.Instance.SetDisplayString(TextHattyuusyoKingaku.Text, hattyuuKingaku)

        TextHattyuusyoKakuninDate.Text = PfunZ010SetHatyuuYMD(hattyuuKingaku)

        '�������m��`�F�b�N
        Select Case FunCheckHKakutei(2, sender)
            Case 1
                '
            Case 2
                SelectHattyuusyoKakutei.SelectedValue = "1"

                ' Old�l�Ɍ��݂̒l���Z�b�g
                HiddenHattyuusyoFlgOld.Value = SelectHattyuusyoKakutei.SelectedValue

                ' ���������z����͕s�ɐݒ�
                EnableTextBox(TextHattyuusyoKingaku, False)
        End Select

        '�X�V�㔭�������z��old�ɐݒ�
        HiddenHattyuusyoKingakuOld.Value = TextHattyuusyoKingaku.Text
        TextHattyuusyoKingaku.Text = IIf(hattyuuKingaku = Integer.MinValue, "", hattyuuKingaku.ToString(EarthConst.FORMAT_KINGAKU_1))

        setFocusAJ(SelectHattyuusyoKakutei)
    End Sub

    ''' <summary>
    ''' �������m�F���ݒ菈��
    ''' </summary>
    ''' <param name="rvntKingaku">���������z</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function PfunZ010SetHatyuuYMD(ByVal rvntKingaku As Integer) As String

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".PfunZ010SetHatyuuYMD", _
                                                    rvntKingaku)

        If rvntKingaku = 0 Or rvntKingaku = Integer.MinValue Then
            Return ""
        Else
            Return Date.Now.ToString("yyyy/MM/dd")
        End If
    End Function

    ''' <summary>
    ''' �������m��`�F�b�N
    ''' </summary>
    ''' <param name="rlngMode">1,�������m��ύX��.2,���������z�ύX��</param>
    ''' <param name="sender">�N���C�A���g �X�N���v�g �u���b�N��o�^����R���g���[��</param>
    ''' <returns>0:�����Ȃ��A1:�������m��փt�H�[�J�X�ڍs�A2:�����m��</returns>
    ''' <remarks></remarks>
    Public Function FunCheckHKakutei(ByVal rlngMode As Long, _
                                     ByVal sender As Object) As Long

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".FunCheckHKakutei", _
                                                    rlngMode, sender)

        FunCheckHKakutei = 0

        If SelectHattyuusyoKakutei.SelectedValue = "1" And _
           TextHattyuusyoKingaku.Text IsNot TextJituSeikyuuGaku.Text Then

            Dim hattyuuKingaku As Integer = Integer.MinValue
            CommonLogic.Instance.SetDisplayString(TextHattyuusyoKingaku.Text, hattyuuKingaku)
            ' �������m�F����ݒ�
            TextHattyuusyoKakuninDate.Text = PfunZ010SetHatyuuYMD(hattyuuKingaku)

        End If

        ' ��r������z�𐔒l�ϊ�����
        Dim chkVal1 As Integer = 0
        Dim chkVal2 As Integer = 0
        CommonLogic.Instance.SetDisplayString(TextHattyuusyoKingaku.Text, chkVal1)
        CommonLogic.Instance.SetDisplayString(TextJituSeikyuuGaku.Text, chkVal2)

        ' �����
        If rlngMode = 2 Then
            ' ���������z�ύX���̓e�L�X�g�ĕύX�チ�b�Z�[�W�\��
            If SelectUriageSyori.SelectedValue = EarthConst.URIAGE_ZUMI_CODE Then

                ' ��r���ċ��z�̔�r�ɂ�胁�b�Z�[�W�𕪂���
                If chkVal1 = chkVal2 Then
                    FunCheckHKakutei = 2
                    If HiddenHattyuusyoKingakuOld.Value <> "" And _
                       HiddenHattyuusyoFlgOld.Value <> "1" Then
                        ' �����������m��
                        ScriptManager.RegisterClientScriptBlock(sender, _
                                                                sender.GetType(), _
                                                                "alert", _
                                                                "alert('" & _
                                                                Messages.MSG045C & _
                                                                "')", True)
                    End If
                End If
            End If
        Else
            If SelectHattyuusyoKakutei.SelectedValue = "1" Then
                ' �������m��ύX���͋��z����Ń��b�Z�[�W�\��
                FunCheckHKakutei = 2
                If chkVal1 <> chkVal2 Then
                    ' �����������m��
                    ScriptManager.RegisterClientScriptBlock(sender, _
                                                            sender.GetType(), _
                                                            "alert", _
                                                            "alert('" & _
                                                            Messages.MSG046C & _
                                                            "')", True)
                End If
            End If
        End If

        '�������m��ς݂̏ꍇ�A���������z��ҏW�s�ɐݒ�
        If SelectHattyuusyoKakutei.SelectedValue = "1" Then
            cl.chgVeiwMode(TextHattyuusyoKingaku)
        Else
            If HiddenKeiriGyoumuKengen.Value = "-1" Or _
               HiddenHattyuusyoKanriKengen.Value = "-1" Or _
               HiddenKeiriGyoumuKengen.Value = "-1" Then
                ' �o���������A�˗��Ɩ����A�������Ǘ������L��ꍇ�A������
                cl.chgDispSyouhinText(TextHattyuusyoKingaku)
            End If
        End If

    End Function

    ''' <summary>
    ''' ����Ŋz�A�ō����z�̃Z�b�g
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetZeigaku(ByVal e As System.EventArgs)

        ' �������Ŕ����z���󔒂̏ꍇ�A����Ŋz�E�ō����z���󔒂ɂ���
        If TextJituSeikyuuGaku.Text.Trim() = String.Empty Then
            TextSyouhizeiGaku.Text = String.Empty
            TextZeikomiKingaku.Text = String.Empty
            Exit Sub
        End If

        Dim jitugaku As Integer = IIf(TextJituSeikyuuGaku.Text.Trim() = "", _
                                      0, _
                                      Integer.Parse(TextJituSeikyuuGaku.Text.Replace(",", "")))
        Dim zeigaku As Integer = Fix(jitugaku * Decimal.Parse(HiddenZeiritu.Value))
        Dim zeikomi As Integer = jitugaku + zeigaku

        '����Ŋz
        TextSyouhizeiGaku.Text = zeigaku.ToString(EarthConst.FORMAT_KINGAKU_1)
        '�ō����z
        TextZeikomiKingaku.Text = zeikomi.ToString(EarthConst.FORMAT_KINGAKU_1)

    End Sub

    ''' <summary>
    ''' �d������Ŋz�̃Z�b�g
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSiireZeigaku()
        Dim intSyouGaku As Integer = 0
        Dim intSiireZeiGaku As Integer = 0

        '���������z���󔒂̏ꍇ�A�d������Ŋz���󔒂ɂ���
        If Me.TextSyoudakusyoKingaku.Text.Trim() = String.Empty Then
            Me.TextSiireSyouhizeiGaku.Text = String.Empty
            Exit Sub
        ElseIf Me.TextSyoudakusyoKingaku.Text.Trim() = "0" Then
            Me.TextSiireSyouhizeiGaku.Text = "0"
            Exit Sub
        End If

        '�ŗ��̊m�F
        If IsNumeric(Me.HiddenZeiritu.Value) = False Then
            Me.HiddenZeiritu.Value = "0"
        End If

        '���������z�̎擾
        intSyouGaku = Integer.Parse(Me.TextSyoudakusyoKingaku.Text.Replace(",", ""))

        '�d������Ŋz�̌v�Z
        intSiireZeiGaku = Fix(intSyouGaku * Decimal.Parse(Me.HiddenZeiritu.Value))

        '��ʂ֐ݒ�
        Me.TextSiireSyouhizeiGaku.Text = intSiireZeiGaku.ToString(EarthConst.FORMAT_KINGAKU_1)

    End Sub

#End Region

#Region "���t�n�C�x���g"

    ''' <summary>
    ''' ����N�����ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextUriageNengappi_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextUriageNengappi.TextChanged
        Dim strZeiKbn As String = String.Empty              '�ŋ敪
        Dim strZeiritu As String = String.Empty             '�ŗ�
        Dim strSyouhinCd As String = String.Empty           '���i�R�[�h
        ' �擾�p���W�b�N�N���X
        Dim cBizLogic As New CommonBizLogic

        '����N����
        If Me.TextUriageNengappi.Text <> String.Empty Then
            '�󔒂̏ꍇ�̂ݓ`�[����N������ݒ�
            If Me.TextDenpyouUriageNengappi.Text.Trim() = String.Empty Then
                Me.TextDenpyouUriageNengappi.Text = Me.TextUriageNengappi.Text
                TextDenpyouUriageNengappi_TextChanged(sender, e)
            End If
            '�󔒂̏ꍇ�̂ݓ`�[�d���N������ݒ�
            If Me.TextDenpyouSiireNengappi.Text = String.Empty Then
                '����N�������Z�b�g
                Me.TextDenpyouSiireNengappi.Text = Me.TextUriageNengappi.Text
            End If

            '�ŋ敪�E�ŗ����Ď擾
            strSyouhinCd = Me.TextSyouhinCd.Text

            ' ���i�R�[�h�ύX���AOld�ɉ��.���i�R�[�h���Z�b�g
            If HiddenSyouhinCdOld.Value <> TextSyouhinCd.Text Then
                HiddenSyouhinCdOld.Value = TextSyouhinCd.Text
            End If

            If cBizLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
                '�擾�����ŋ敪�E�ŗ����Z�b�g
                Me.HiddenZeikubun.Value = strZeiKbn
                Me.HiddenZeiritu.Value = strZeiritu

                '�ŋ敪�E�ŗ����󔒂̏ꍇ�A�������Ŕ����z�Ə��������z�ɋ󔒂��Z�b�g����
                If Me.HiddenZeiritu.Value = String.Empty OrElse Me.HiddenZeikubun.Value = String.Empty Then
                    Me.TextJituSeikyuuGaku.Text = ""
                    Me.TextSyoudakusyoKingaku.Text = ""
                End If
                '���z�v�Z
                SetZeigaku(e)
                SetSiireZeigaku()
            End If
        Else
            '����N���������͂̏ꍇ

            '�ŋ敪�E�ŗ����Ď擾
            strSyouhinCd = Me.TextSyouhinCd.Text

            If cBizLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
                '�擾�����ŋ敪�E�ŗ����Z�b�g
                Me.HiddenZeikubun.Value = strZeiKbn
                Me.HiddenZeiritu.Value = strZeiritu

                '�ŋ敪�E�ŗ����󔒂̏ꍇ�A�������Ŕ����z�Ə��������z�ɋ󔒂��Z�b�g����
                If Me.HiddenZeiritu.Value = String.Empty OrElse Me.HiddenZeikubun.Value = String.Empty Then
                    Me.TextJituSeikyuuGaku.Text = ""
                    Me.TextSyoudakusyoKingaku.Text = ""
                End If
                '���z�v�Z
                SetZeigaku(e)
                SetSiireZeigaku()
            End If
        End If

        '�`�[����N�����Ƀt�H�[�J�X
        setFocusAJ(TextDenpyouUriageNengappi)

    End Sub

    ''' <summary>
    ''' �`�[����N�����ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextDenpyouUriageNengappi_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextDenpyouUriageNengappi.TextChanged
        ' �擾�p���W�b�N�N���X
        Dim cBizLogic As New CommonBizLogic

        '�`�[����N����
        If Me.TextDenpyouUriageNengappi.Text <> String.Empty Then
            '���ߓ��̐ݒ�
            Me.ucSeikyuuSiireLink.SetSeikyuuSimeDate()
            ' ���������s���擾
            Dim strHakDate As String = Me.ucSeikyuuSiireLink.GetSeikyuusyoHakkouDate(Me.TextDenpyouUriageNengappi.Text)
            Me.TextSeikyuusyoHakkouDate.Text = strHakDate
        Else
            Me.TextSeikyuusyoHakkouDate.Text = String.Empty
        End If

        '���������s���Ƀt�H�[�J�X
        setFocusAJ(Me.TextSeikyuusyoHakkouDate)

    End Sub

#End Region

#Region "���̓`�F�b�N"
    ''' <summary>
    ''' ���̓`�F�b�N
    ''' </summary>
    ''' <param name="errMess"></param>
    ''' <param name="arrFocusTargetCtrl"></param>
    ''' <remarks></remarks>
    Public Sub CheckInput(ByRef errMess As String, _
                          ByRef arrFocusTargetCtrl As List(Of Control), _
                          ByVal intLineCnt As Integer)

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CheckInput", _
                                                    errMess, _
                                                    arrFocusTargetCtrl)

        '�s�ԍ�������
        Dim strGyouInfo As String = "���i4_" & intLineCnt & "�F"
        '�����\��m�茎
        Dim dtGetujiKakuteiLastSyoriDate As DateTime = DateTime.MinValue

        '���i�R�[�h
        If Me.TextSyouhinCd.Text.Trim = String.Empty Then '������
            Exit Sub '���i�폜�̂��߁A�ΏۊO
        End If

        'DB�ǂݍ��ݎ��_�̒l���A���݉�ʂ̒l�Ɣ�r(�ύX�L���`�F�b�N)
        If HiddenKubun.Value <> String.Empty AndAlso (HiddenOpenValuesUriage.Value <> String.Empty Or HiddenOpenValuesSiire.Value <> String.Empty) Then
            'DB�ǂݍ��ݎ��_�̒l����̏ꍇ�͔�r�ΏۊO
            '��r���{(����)
            If HiddenOpenValuesUriage.Value <> getCtrlValuesStringUriage() Then
                '�����m��\��ς݂̏����N���u�ȑO�v�̓��t�œ`�[����N������ݒ肷��̂̓G���[
                If cl.chkGetujiKakuteiYoyakuzumi(Me.TextDenpyouUriageNengappi.Text, dtGetujiKakuteiLastSyoriDate) = False Then
                    errMess += String.Format(Messages.MSG161E, dtGetujiKakuteiLastSyoriDate.Month, strGyouInfo, dtGetujiKakuteiLastSyoriDate.AddMonths(1).Month)
                    arrFocusTargetCtrl.Add(Me.TextDenpyouUriageNengappi)
                End If
            End If

            '��r���{(�d��)
            If HiddenOpenValuesSiire.Value <> getCtrlValuesStringSiire() Then
                '�����m��\��ς݂̏����N���u�ȑO�v�̓��t�œ`�[�d���N������ݒ肷��̂̓G���[
                If cl.chkGetujiKakuteiYoyakuzumi(Me.TextDenpyouSiireNengappi.Text, dtGetujiKakuteiLastSyoriDate) = False Then
                    errMess += String.Format(Messages.MSG161E, dtGetujiKakuteiLastSyoriDate.Month, strGyouInfo, dtGetujiKakuteiLastSyoriDate.AddMonths(1).Month).Replace("�`�[����", "�`�[�d��")
                    arrFocusTargetCtrl.Add(Me.TextDenpyouSiireNengappi)
                End If
            End If

        End If
        '�V�K�o�^���̃`�F�b�N(�`�[����N����)
        If String.IsNullOrEmpty(HiddenOpenValuesUriage.Value) Then
            '�����m��\��ς݂̏����N���u�ȑO�v�̓��t�œ`�[����N������ݒ肷��̂̓G���[
            If cl.chkGetujiKakuteiYoyakuzumi(Me.TextDenpyouUriageNengappi.Text, dtGetujiKakuteiLastSyoriDate) = False Then
                errMess += String.Format(Messages.MSG161E, dtGetujiKakuteiLastSyoriDate.Month, strGyouInfo, dtGetujiKakuteiLastSyoriDate.AddMonths(1).Month)
                arrFocusTargetCtrl.Add(Me.TextDenpyouUriageNengappi)
            End If
        End If
        '�V�K�o�^���̃`�F�b�N(�`�[�d���N����)
        If String.IsNullOrEmpty(HiddenOpenValuesSiire.Value) Then
            '�����m��\��ς݂̏����N���u�ȑO�v�̓��t�œ`�[�d���N������ݒ肷��̂̓G���[
            If cl.chkGetujiKakuteiYoyakuzumi(Me.TextDenpyouSiireNengappi.Text, dtGetujiKakuteiLastSyoriDate) = False Then
                errMess += String.Format(Messages.MSG161E, dtGetujiKakuteiLastSyoriDate.Month, strGyouInfo, dtGetujiKakuteiLastSyoriDate.AddMonths(1).Month).Replace("�`�[����", "�`�[�d��")
                arrFocusTargetCtrl.Add(Me.TextDenpyouSiireNengappi)
            End If
        End If

        '���R�[�h���͒l�ύX�`�F�b�N
        '���i�R�[�h
        If Me.TextSyouhinCd.Text <> Me.HiddenSyouhinCdOld.Value Then
            errMess &= Messages.MSG030E.Replace("@PARAM1", "���i�R�[�h")
            arrFocusTargetCtrl.Add(ButtonSyouhinKensaku)
        End If

        '���K�{�`�F�b�N
        '��������(�����L���F�L��̏ꍇ�̂ݕK�{)
        If Me.SelectSeikyuuUmu.SelectedValue = "1" Then
            If Me.ucSeikyuuSiireLink.AccSeikyuuSakiCd.Value.Trim() = String.Empty _
                Or Me.ucSeikyuuSiireLink.AccSeikyuuSakiBrc.Value.Trim() = String.Empty _
                Or Me.ucSeikyuuSiireLink.AccSeikyuuSakiKbn.Value.Trim() = String.Empty Then

                errMess &= strGyouInfo & Messages.MSG151E.Replace("@PARAM1", "��������").Replace("@PARAM2", "������/�d����ύX���")
                arrFocusTargetCtrl.Add(ucSeikyuuSiireLink.AccLinkSeikyuuSiireHenkou)
            End If
        End If
        '�d������
        If Me.ucSeikyuuSiireLink.AccTysKaisyaCd.Value.Trim() = String.Empty _
            Or Me.ucSeikyuuSiireLink.AccTysKaisyaJigyousyoCd.Value.Trim() = String.Empty Then

            errMess &= strGyouInfo & Messages.MSG151E.Replace("@PARAM1", "�d������").Replace("@PARAM2", "������/�d����ύX���")
            arrFocusTargetCtrl.Add(ucSeikyuuSiireLink.AccLinkSeikyuuSiireHenkou)
        End If

        '����N�����Ɠ`�[����N����
        If Me.TextUriageNengappi.Text = String.Empty And Me.TextDenpyouUriageNengappi.Text <> String.Empty Then
            errMess &= strGyouInfo & Messages.MSG153E.Replace("@PARAM1", "�`�[����N����").Replace("@PARAM2", "����N����")
            arrFocusTargetCtrl.Add(TextUriageNengappi)
        End If

        '�����t�`�F�b�N
        '���Ϗ��쐬��
        If Trim(Me.TextMitumorisyoSakuseiDate.Text) <> "" Then
            If cl.checkDateHanni(Me.TextMitumorisyoSakuseiDate.Text) = False Then
                errMess &= strGyouInfo & Messages.MSG014E.Replace("@PARAM1", "���Ϗ��쐬��")
                arrFocusTargetCtrl.Add(TextMitumorisyoSakuseiDate)
            End If
        End If
        '����N����
        If Trim(Me.TextUriageNengappi.Text) <> "" Then
            If cl.checkDateHanni(Me.TextUriageNengappi.Text) = False Then
                errMess &= strGyouInfo & Messages.MSG014E.Replace("@PARAM1", "����N����")
                arrFocusTargetCtrl.Add(Me.TextUriageNengappi)
            End If
        End If
        '�`�[����N����
        If Trim(Me.TextDenpyouUriageNengappi.Text) <> "" Then
            If cl.checkDateHanni(Me.TextDenpyouUriageNengappi.Text) = False Then
                errMess &= strGyouInfo & Messages.MSG014E.Replace("@PARAM1", "�`�[����N����")
                arrFocusTargetCtrl.Add(Me.TextDenpyouUriageNengappi)
            End If
        End If
        '�`�[�d���N����
        If Trim(Me.TextDenpyouSiireNengappi.Text) <> "" Then
            If cl.checkDateHanni(Me.TextDenpyouSiireNengappi.Text) = False Then
                errMess &= strGyouInfo & Messages.MSG014E.Replace("@PARAM1", "�`�[�d���N����")
                arrFocusTargetCtrl.Add(Me.TextDenpyouSiireNengappi)
            End If
        End If
        '���������s��
        If Trim(Me.TextSeikyuusyoHakkouDate.Text) <> "" Then
            If cl.checkDateHanni(Me.TextSeikyuusyoHakkouDate.Text) = False Then
                errMess &= strGyouInfo & Messages.MSG014E.Replace("@PARAM1", "�������N����")
                arrFocusTargetCtrl.Add(Me.TextSeikyuusyoHakkouDate)
            End If
        End If
        '�������m�F��
        If Trim(Me.TextHattyuusyoKakuninDate.Text) <> "" Then
            If cl.checkDateHanni(Me.TextHattyuusyoKakuninDate.Text) = False Then
                errMess &= strGyouInfo & Messages.MSG014E.Replace("@PARAM1", "�������m�F��")
                arrFocusTargetCtrl.Add(Me.TextHattyuusyoKakuninDate)
            End If
        End If

    End Sub

    ''' <summary>
    ''' ��ʓǂݍ��ݎ��̒l��Hidden���ڂɑޔ�
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub setOpenValuesUriage()
        Me.HiddenOpenValuesUriage.Value = getCtrlValuesStringUriage()
    End Sub

    ''' <summary>
    ''' ��ʓǂݍ��ݎ��̒l��Hidden���ڂɑޔ�
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub setOpenValuesSiire()
        Me.HiddenOpenValuesSiire.Value = getCtrlValuesStringSiire()
    End Sub

    ''' <summary>
    ''' ��ʃR���g���[���̒l���������A�����񉻂���
    ''' </summary>
    ''' <returns>��ʃR���g���[���̒l����������������</returns>
    ''' <remarks></remarks>
    Public Function getCtrlValuesStringUriage() As String
        Dim sb As New StringBuilder

        sb.Append(TextSyouhinCd.Text & EarthConst.SEP_STRING)               '���i�R�[�h
        sb.Append(TextJituSeikyuuGaku.Text & EarthConst.SEP_STRING)         '������z(�������Ŕ����z)
        sb.Append(TextSyouhizeiGaku.Text & EarthConst.SEP_STRING)           '����Ŋz
        sb.Append(HiddenZeikubun.Value & EarthConst.SEP_STRING)             '�ŋ敪(��\������)
        sb.Append(TextDenpyouUriageNengappi.Text & EarthConst.SEP_STRING)   '�`�[����N����
        sb.Append(TextUriageNengappi.Text & EarthConst.SEP_STRING)          '����N����
        sb.Append(SelectUriageSyori.SelectedValue & EarthConst.SEP_STRING)  '����v��FLG
        sb.Append(TextSeikyuusyoHakkouDate.Text & EarthConst.SEP_STRING)    '���������s��

        sb.Append(ucSeikyuuSiireLink.AccSeikyuuSakiCd.Value & EarthConst.SEP_STRING)          '������R�[�h
        sb.Append(ucSeikyuuSiireLink.AccSeikyuuSakiBrc.Value & EarthConst.SEP_STRING)         '������}��
        sb.Append(ucSeikyuuSiireLink.AccSeikyuuSakiKbn.Value & EarthConst.SEP_STRING)         '������敪

        Return sb.ToString
    End Function

    ''' <summary>
    ''' ��ʃR���g���[���̒l���������A�����񉻂���
    ''' </summary>
    ''' <returns>��ʃR���g���[���̒l����������������</returns>
    ''' <remarks></remarks>
    Public Function getCtrlValuesStringSiire() As String
        Dim sb As New StringBuilder

        sb.Append(TextSyouhinCd.Text & EarthConst.SEP_STRING)               '���i�R�[�h
        sb.Append(TextSyoudakusyoKingaku.Text & EarthConst.SEP_STRING)      '�d�����z(���������z)
        sb.Append(TextSiireSyouhizeiGaku.Text & EarthConst.SEP_STRING)      '�d������Ŋz
        sb.Append(TextDenpyouSiireNengappi.Text & EarthConst.SEP_STRING)    '�`�[�d���N����
        sb.Append(HiddenZeikubun.Value & EarthConst.SEP_STRING)             '�ŋ敪(��\������)
        sb.Append(SelectUriageSyori.SelectedValue & EarthConst.SEP_STRING)  '����v��FLG

        sb.Append(ucSeikyuuSiireLink.AccTysKaisyaCd.Value & EarthConst.SEP_STRING)            '������ЃR�[�h
        sb.Append(ucSeikyuuSiireLink.AccTysKaisyaJigyousyoCd.Value & EarthConst.SEP_STRING)   '������Ў��Ə��R�[�h

        Return sb.ToString
    End Function

#End Region

#Region "�R���g���[���̊�������"
    ''' <summary>
    ''' �e�L�X�g�{�b�N�X�P�̂̊�������
    ''' </summary>
    ''' <param name="enabled"></param>
    ''' <remarks></remarks>
    Private Sub EnableTextBox(ByRef ctrl As TextBox, _
                              ByVal enabled As Boolean)

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EnableTextBox", _
                                                    ctrl, enabled)

        ctrl.ReadOnly = Not enabled

        If enabled Then
            ctrl.BackColor = Drawing.Color.White
            ctrl.BorderStyle = BorderStyle.NotSet
            ctrl.TabIndex = 0
        Else
            ctrl.BackColor = Drawing.Color.Transparent
            ctrl.BorderStyle = BorderStyle.None
            ctrl.TabIndex = -1
        End If

    End Sub



#End Region

End Class
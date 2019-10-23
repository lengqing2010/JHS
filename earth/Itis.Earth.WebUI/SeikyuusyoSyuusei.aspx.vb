Public Partial Class SeikyuusyoSyuusei
    Inherits System.Web.UI.Page

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Private userinfo As New LoginUserInfo
    Private masterAjaxSM As New ScriptManager

    '���ʃ��W�b�N
    Private cl As New CommonLogic
    '���b�Z�[�W�N���X
    Private MLogic As New MessageLogic
    '���W�b�N�N���X
    Private MyLogic As New SeikyuuDataSearchLogic
    '�n�ՃZ�b�V�����Ǘ��N���X
    Private jSM As New JibanSessionManager
    '�����f�[�^���R�[�h�N���X
    Private dtRec As New SeikyuuDataRecord

#Region "��ʌŗL�R���g���[���l"

    ''' <summary>
    ''' ����{�^���u����������v
    ''' </summary>
    Private Const BTN_TORIKESI As String = "���������"

    ''' <summary>
    ''' ����{�^���u��������������v
    ''' </summary>
    Private Const BTN_TORIKESI_KAIJO As String = "�������������"

#End Region

#Region "�v���p�e�B"

#Region "�p�����[�^/�������ꗗ���"

    ''' <summary>
    ''' �N�����
    ''' </summary>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Private _GamenMode As String = String.Empty
    ''' <summary>
    ''' �N�����
    ''' </summary>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Public Property pStrGamenMode() As String
        Get
            Return _GamenMode
        End Get
        Set(ByVal value As String)
            _GamenMode = value
        End Set
    End Property

    ''' <summary>
    ''' ������NO
    ''' </summary>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Private _SeikyuusyoNo As String = String.Empty
    ''' <summary>
    ''' ������NO
    ''' </summary>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Public Property pStrSeikyuusyoNo() As String
        Get
            Return _SeikyuusyoNo
        End Get
        Set(ByVal value As String)
            _SeikyuusyoNo = value
        End Set
    End Property
#End Region

#End Region

#Region "�v���C�x�[�g���\�b�h"

#Region "�����Ǎ��������n"

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

        '�X�N���v�g�}�l�[�W���[���擾�iScriptManager�p�j
        masterAjaxSM = Me.SM1

        ' ���[�U�[��{�F��
        jBn.UserAuth(userinfo)

        '�F�،��ʂɂ���ĉ�ʕ\����ؑւ���
        If userinfo Is Nothing Then
            '���O�C����񂪖����ꍇ
            cl.CloseWindow(Me)
            Me.BtnStyle(False)
            Exit Sub
        End If

        If IsPostBack = False Then
            Try
                '���p�����[�^�̃`�F�b�N
                Dim arrSearchTerm() As String = Split(Request("sendSearchTerms"), EarthConst.SEP_STRING)
                ' Key����ێ�
                pStrGamenMode = arrSearchTerm(0)
                pStrSeikyuusyoNo = arrSearchTerm(1)

                ' �p�����[�^�s�����͕���
                If pStrSeikyuusyoNo Is Nothing OrElse pStrSeikyuusyoNo = String.Empty Then
                    cl.CloseWindow(Me)
                    Me.BtnStyle(False)
                    Exit Sub
                End If
            Catch ex As Exception
                cl.CloseWindow(Me)
                Me.BtnStyle(False)
                Exit Sub
            End Try

            '�������̃`�F�b�N
            '�o���Ɩ�����
            If userinfo.KeiriGyoumuKengen = 0 Then
                cl.CloseWindow(Me)
                Me.BtnStyle(False)
            End If

            '****************************************************************************
            ' �h���b�v�_�E�����X�g�ݒ�
            '****************************************************************************
            '�Ȃ�

            '****************************************************************************
            ' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
            '****************************************************************************
            setDispAction()

            '�{�^�������C�x���g�̐ݒ�()
            setBtnEvent()

            '�t�H�[�J�X�ݒ�
            BtnClose.Focus()

            '****************************************************************************
            ' �������ꗗ�f�[�^�擾
            '****************************************************************************
            SetCtrlFromDataRec(sender, e)

        Else
            '��ʐݒ�()
            'Me.SetGamenMode(dtRec)

        End If
    End Sub

    ''' <summary>
    ''' ���o���R�[�h�N���X�����ʕ\�����ڂւ̒l�Z�b�g���s�Ȃ��B
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromDataRec(ByVal sender As Object, ByVal e As System.EventArgs)

        dtRec = MyLogic.GetSeikyuuDataRec(sender, pStrSeikyuusyoNo)

        '������NO
        Me.TextSeikyuusyoNo.Text = cl.GetDispNum(dtRec.SeikyuusyoNo, "")

        '������
        Me.TextSeikyuuSakiCd.Text = cl.GetDispSeikyuuSakiCd(dtRec.SeikyuuSakiKbn, dtRec.SeikyuuSakiCd, dtRec.SeikyuuSakiBrc, False)
        '������M.�����於
        Me.TextSeikyuuSakiMei.Text = cl.GetDisplayString(dtRec.SeikyuuSakiMeiView)
        '�����於
        Me.TextSeikyuuSakiMei1.Text = cl.GetDisplayString(dtRec.SeikyuuSakiMei)
        '�����於�Q
        Me.TextSeikyuuSakiMei2.Text = cl.GetDisplayString(dtRec.SeikyuuSakiMei2)
        '�X�֔ԍ�
        Me.TextYuubin.Text = cl.GetDisplayString(dtRec.YuubinNo)
        '�d�b�ԍ�
        Me.TextTellNo.Text = cl.GetDisplayString(dtRec.TelNo)
        '�Z���P
        Me.TextJuusyo1.Text = cl.GetDisplayString(dtRec.Jyuusyo1)
        '�Z���Q
        Me.TextJuusyo2.Text = cl.GetDisplayString(dtRec.Jyuusyo2)
        '���׌���
        Me.TextMeisai.Text = cl.GetDisplayString(dtRec.MeisaiKensuu)
        '�S����
        Me.TextTantousyaMei.Text = cl.GetDisplayString(dtRec.TantousyaMei)
        '�O��䐿���z
        Me.TextZenSeikyuuGaku.Text = IIf(dtRec.ZenkaiGoseikyuuGaku = 0, _
                                            "", _
                                            dtRec.ZenkaiGoseikyuuGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
        '����䐿���z
        Me.TextKonSeikyuuGaku.Text = IIf(dtRec.KonkaiGoseikyuuGaku = 0, _
                                            "", _
                                            dtRec.KonkaiGoseikyuuGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
        '������z
        Me.TextNyuukinGaku.Text = IIf(dtRec.GonyuukinGaku = 0, _
                                            "", _
                                            dtRec.GonyuukinGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
        '�O��J�z�c��
        Me.TextZenZandaka.Text = IIf(dtRec.KurikosiGaku = 0, _
                                            "", _
                                            dtRec.KurikosiGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
        '�J�z�c��
        Me.TextKonZandaka.Text = IIf(dtRec.KonkaiKurikosiGaku = 0, _
                                            "", _
                                            dtRec.KonkaiKurikosiGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
        '���E�z
        Me.TextSousaiGaku.Text = IIf(dtRec.SousaiGaku = 0, _
                                            "", _
                                            dtRec.SousaiGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
        '�����z
        Me.TextTyouseiGaku.Text = IIf(dtRec.TyouseiGaku = 0, _
                                            "", _
                                            dtRec.TyouseiGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
        '�����\���
        Me.TextNyuukinYoteiDate.Text = cl.GetDisplayString(dtRec.KonkaiKaisyuuYoteiDate)
        '�f�[�^�쐬�����ߓ�
        Me.TextDataSakuseijiSimeDate.Text = cl.GetDisplayString(dtRec.SeikyuuSimeDate)
        '���������s��
        Me.TextSeikyuusyoHkDate.Text = cl.GetDisplayString(dtRec.SeikyuusyoHakDate)
        '�����������
        Me.TextSeikyuusyoInsatuDate.Text = cl.GetDisplayString(dtRec.SeikyuusyoInsatuDate)

        '����ΏۊO�t���O(�������p���ėp�R�[�h)
        Me.HiddenPrintTaisyougaiFlg.Value = dtRec.PrintTaisyougaiFlg

        '************
        '* Hidden����
        '************
        '�X�V����
        Me.HiddenUpdDatetime.Value = IIf(dtRec.UpdDatetime = Date.MinValue, "", Format(dtRec.UpdDatetime, EarthConst.FORMAT_DATE_TIME_1))

        '��ʃ��[�h�ʐݒ�
        SetGamenMode(dtRec)

    End Sub

    ''' <summary>
    ''' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setDispAction()

        Dim checkDate As String = "checkDate(this);"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ ���t�n
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '�����\���
        Me.TextNyuukinYoteiDate.Attributes("onblur") = checkDate

    End Sub

    ''' <summary>
    ''' �e��{�^���̐ݒ�
    ''' </summary>
    ''' <remarks>
    ''' DB�X�V�̊m�F���s�Ȃ��B<br/>
    ''' OK���FDB�X�V���s�Ȃ��B<br/>
    ''' �L�����Z�����FDB�X�V�𒆒f����B<br/>
    ''' </remarks>
    Private Sub setBtnEvent()

        '�C�x���g�n���h���o�^
        Dim tmpScriptOverLay As String = "setWindowOverlay(this,null,1);"
        Dim tmpTouroku As String = "if(confirm('" & Messages.MSG017C & "')){" & tmpScriptOverLay & "}else{return false;}"
        Dim tmpTourokuKakunin As String = "if(confirm('" & Messages.MSG017C & "\r\n" & Messages.MSG159E & "')){" & tmpScriptOverLay & "}else{return false;}"

        '�o�^����MSG�m�F��AOK�̏ꍇDB�X�V�������s�Ȃ�
        Me.BtnSyusei.Attributes("onclick") = tmpTouroku     '�C���{�^��
        Me.BtnInsatu.Attributes("onclick") = tmpTourokuKakunin     '����{�^��
        Me.BtnTorikesi.Attributes("onclick") = tmpTourokuKakunin   '����{�^��

    End Sub

    ''' <summary>
    ''' �{�^���\������
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BtnStyle(ByVal flgDisp As Boolean)

        If flgDisp Then
            '�{�^����\��
            Me.BtnSyusei.Style("display") = "inline"
            Me.BtnInsatu.Style("display") = "inline"
            Me.BtnTorikesi.Style("display") = "inline"
            Me.BtnSeikyuuSakiCall.Style("display") = "inline"
        Else
            '�{�^�����\��
            Me.BtnSyusei.Style("display") = "none"
            Me.BtnInsatu.Style("display") = "none"
            Me.BtnTorikesi.Style("display") = "none"
            Me.BtnSeikyuuSakiCall.Style("display") = "none"
        End If


    End Sub

    ''' <summary>
    ''' ��ʂ̊���������
    ''' </summary>
    ''' <param name="enabled"></param>
    ''' <remarks></remarks>
    Private Sub setEnabledCtrl(ByVal enabled As Boolean)

        If enabled = True Then
            cl.chgDispSyouhinText(Me.TextSeikyuuSakiMei1)
            cl.chgDispSyouhinText(Me.TextSeikyuuSakiMei2)
            cl.chgDispSyouhinText(Me.TextTellNo)
            cl.chgDispSyouhinText(Me.TextYuubin)
            cl.chgDispSyouhinText(Me.TextJuusyo1)
            cl.chgDispSyouhinText(Me.TextJuusyo2)
            cl.chgDispSyouhinText(Me.TextTantousyaMei)
            cl.chgDispSyouhinText(Me.TextNyuukinYoteiDate)
        End If

    End Sub

    ''' <summary>
    ''' ��ʃ��[�h�ʂ̉�ʐݒ�
    ''' </summary>
    ''' <param name="dtRec">�����f�[�^���R�[�h</param>
    ''' <remarks></remarks>
    Private Sub SetGamenMode(ByVal dtRec As SeikyuuDataRecord)
        Dim ht As New Hashtable
        Dim htNotTarget As New Hashtable '�ΏۊO
        Dim tmpScriptOverLay As String = "setWindowOverlay(this,null,1);"
        Dim tmpConfirm As String = "if(confirm('" & Messages.MSG017C & "')){" & tmpScriptOverLay & "}else{return false;}"
        Dim tmpTorikesi As String = "if(confirm('" & Messages.MSG017C & "\r\n" & Messages.MSG159E & "')){" & tmpScriptOverLay & "}else{return false;}"

        'Hidden�ɑޔ�
        Me.HiddenGamenMode.Value = pStrGamenMode

        '������No�̑��݃`�F�b�N
        If cl.GetDisplayString(dtRec.SeikyuusyoNo) = String.Empty Then
            '�{�^����\��
            BtnStyle(False)
        Else
            '�{�^���\��
            BtnStyle(True)

            '������.����̔��f����
            If cl.GetDisplayString(dtRec.Torikesi) = "0" Then
                '����{�^���̐ݒ�
                Me.BtnTorikesi.Value = BTN_TORIKESI
                Me.BtnTorikesi.Style("background-color") = "fuchsia"
                Me.BtnTorikesi.Attributes("onclick") = tmpTorikesi
            Else
                '����{�^���̐ݒ�
                Me.BtnTorikesi.Value = BTN_TORIKESI_KAIJO
                Me.BtnTorikesi.Style("background-color") = "#ffff69"
                Me.BtnTorikesi.Attributes("onclick") = tmpConfirm
            End If

            '�J�ڌ���ʂɂ�锻�f����
            If pStrGamenMode = CStr(EarthEnum.emSeikyuuSearchType.SearchSeikyuusyo) Then
                '�������ꗗ���
                Me.BtnInsatu.Value = EarthConst.BUTTON_SEIKYUUSYO_PRINT
            ElseIf pStrGamenMode = CStr(EarthEnum.emSeikyuuSearchType.KakoSearchSeikyuusyo) Then
                '�ߋ��ꗗ���
                Me.BtnInsatu.Value = EarthConst.BUTTON_SEIKYUUSYO_RE_PRINT
            End If

            '�y��������z
            '����ςƎ���ɂ�銈�����f
            If dtRec.SeikyuusyoInsatuDate <> Date.MinValue Or cl.GetDisplayString(dtRec.Torikesi) <> "0" Then
                '����ς�or����̏ꍇ�񊈐���
                jSM.Hash2Ctrl(Me, EarthConst.MODE_VIEW, ht, htNotTarget)
                '�C���{�^���񊈐���
                Me.BtnSyusei.Disabled = True
                '����������̏ꍇ�̂ݔ񊈐���
                If Me.BtnInsatu.Value = EarthConst.BUTTON_SEIKYUUSYO_PRINT Then
                    Me.BtnInsatu.Disabled = True
                End If
            Else
                '�����and������̏ꍇ������
                setEnabledCtrl(True)
                Me.BtnSyusei.Disabled = False
                '��������������̏ꍇ�݈̂��������
                If Me.HiddenPrintTaisyougaiFlg.Value = 0 Then
                    Me.BtnInsatu.Disabled = False
                End If
            End If

            '��������������ΏۊO�E�擾�ł��Ȃ��ꍇ�A��Ɉ���񊈐���
            If Me.HiddenPrintTaisyougaiFlg.Value = 1 Or String.IsNullOrEmpty(dtRec.KaisyuuSeikyuusyoYousi) Then
                Me.BtnInsatu.Disabled = True
            End If

            '������}�X�^�{�^���Ƀ����N�𐶐�
            cl.getSeikyuuSakiMasterPath( _
                                    dtRec.SeikyuuSakiCd _
                                    , dtRec.SeikyuuSakiBrc _
                                    , dtRec.SeikyuuSakiKbn _
                                    , Me.BtnSeikyuuSaki)

            '�����F���]����(������.�������ߓ��Ɛ�����M.�������ߓ�)
            If dtRec.SeikyuuSimeDate <> dtRec.SeikyuuSimeDateMst Then
                '�X�^�C���ݒ�
                Me.TextDataSakuseijiSimeDate.Style("color") = "red"
                Me.TextDataSakuseijiSimeDate.Style("font-weight") = "bold"
            End If
        End If

    End Sub


#End Region

#Region "DB�X�V�����n"

    ''' <summary>
    ''' ���͍��ڃ`�F�b�N
    ''' </summary>
    ''' <remarks>
    ''' �K�{�`�F�b�N<br/>
    ''' �֑������`�F�b�N(��������̓t�B�[���h���Ώ�)<br/>
    ''' �o�C�g���`�F�b�N(��������̓t�B�[���h���Ώ�)<br/>
    ''' �����`�F�b�N<br/>
    ''' ���t�`�F�b�N<br/>
    ''' ���̑��`�F�b�N<br/>
    ''' </remarks>
    Public Function checkInput(ByVal prmActBtn As HtmlInputButton) As Boolean
        '�n�Չ�ʋ��ʃN���X
        Dim jBn As New Jiban

        '�S�ẴR���g���[����L����
        Dim noTarget As New Hashtable
        jBn.ChangeDesabledAll(Me, False, noTarget)

        '�G���[���b�Z�[�W������
        Dim errMess As String = String.Empty
        Dim arrFocusTargetCtrl As New ArrayList

        '���̓`�F�b�N�͏C���{�^���������̂�
        If prmActBtn.ID = Me.BtnInsatu.ID Then
            Return True
        ElseIf prmActBtn.ID = Me.BtnTorikesi.ID Then
            Return True
        ElseIf prmActBtn.ID = Me.BtnSyusei.ID Then

            '���R�[�h���͒l�ύX�`�F�b�N
            '�Ȃ�

            '���K�{�`�F�b�N
            '�����於
            If Me.TextSeikyuuSakiMei1.Text = String.Empty Then
                errMess += Messages.MSG013E.Replace("@PARAM1", "�����於")
                arrFocusTargetCtrl.Add(Me.TextSeikyuuSakiMei1)
            End If
            '�X�֔ԍ�
            If Me.TextYuubin.Text = String.Empty Then
                errMess += Messages.MSG013E.Replace("@PARAM1", "�X�֔ԍ�")
                arrFocusTargetCtrl.Add(Me.TextYuubin)
            End If
            '�Z���P
            If Me.TextJuusyo1.Text = String.Empty Then
                errMess += Messages.MSG013E.Replace("@PARAM1", "�Z���P")
                arrFocusTargetCtrl.Add(Me.TextJuusyo1)
            End If

            '�����t�`�F�b�N
            '�����\���
            If Me.TextNyuukinYoteiDate.Text <> String.Empty Then
                If cl.checkDateHanni(Me.TextNyuukinYoteiDate.Text) = False Then
                    errMess += Messages.MSG014E.Replace("@PARAM1", "�����\���")
                    arrFocusTargetCtrl.Add(Me.TextNyuukinYoteiDate)
                End If
            End If

            '���R�[�h�����`�F�b�N
            '�Ȃ�

            '���֑������`�F�b�N(��������̓t�B�[���h���Ώ�)
            '�����於
            If jBn.KinsiStrCheck(Me.TextSeikyuuSakiMei1.Text) = False Then
                errMess += Messages.MSG015E.Replace("@PARAM1", "�����於")
                arrFocusTargetCtrl.Add(Me.TextSeikyuuSakiMei1)
            End If
            '�����於�Q
            If jBn.KinsiStrCheck(Me.TextSeikyuuSakiMei2.Text) = False Then
                errMess += Messages.MSG015E.Replace("@PARAM1", "�����於�Q")
                arrFocusTargetCtrl.Add(Me.TextSeikyuuSakiMei2)
            End If
            '�X�֔ԍ�
            If jBn.KinsiStrCheck(Me.TextYuubin.Text) = False Then
                errMess += Messages.MSG015E.Replace("@PARAM1", "�X�֔ԍ�")
                arrFocusTargetCtrl.Add(Me.TextYuubin)
            End If
            '�d�b�ԍ�
            If jBn.KinsiStrCheck(Me.TextTellNo.Text) = False Then
                errMess += Messages.MSG015E.Replace("@PARAM1", "�d�b�ԍ�")
                arrFocusTargetCtrl.Add(Me.TextTellNo)
            End If
            '�Z���P
            If jBn.KinsiStrCheck(Me.TextJuusyo1.Text) = False Then
                errMess += Messages.MSG015E.Replace("@PARAM1", "�Z���P")
                arrFocusTargetCtrl.Add(Me.TextJuusyo1)
            End If
            '�Z���Q
            If jBn.KinsiStrCheck(Me.TextJuusyo2.Text) = False Then
                errMess += Messages.MSG015E.Replace("@PARAM1", "�Z���Q")
                arrFocusTargetCtrl.Add(Me.TextJuusyo2)
            End If
            '�S���Җ�
            If jBn.KinsiStrCheck(Me.TextTantousyaMei.Text) = False Then
                errMess += Messages.MSG015E.Replace("@PARAM1", "�S���Җ�")
                arrFocusTargetCtrl.Add(Me.TextTantousyaMei)
            End If

            '���o�C�g���`�F�b�N(��������̓t�B�[���h���Ώ�)
            '�����於
            If jBn.ByteCheckSJIS(Me.TextSeikyuuSakiMei1.Text, Me.TextSeikyuuSakiMei1.MaxLength) = False Then
                errMess += Messages.MSG016E.Replace("@PARAM1", "�����於")
                arrFocusTargetCtrl.Add(Me.TextSeikyuuSakiMei1)
            End If
            '�����於�Q
            If jBn.ByteCheckSJIS(Me.TextSeikyuuSakiMei2.Text, Me.TextSeikyuuSakiMei2.MaxLength) = False Then
                errMess += Messages.MSG016E.Replace("@PARAM1", "�����於�Q")
                arrFocusTargetCtrl.Add(Me.TextSeikyuuSakiMei2)
            End If
            '�X�֔ԍ�
            If jBn.ByteCheckSJIS(Me.TextYuubin.Text, Me.TextYuubin.MaxLength) = False Then
                errMess += Messages.MSG016E.Replace("@PARAM1", "�X�֔ԍ�")
                arrFocusTargetCtrl.Add(Me.TextYuubin)
            End If
            '�d�b�ԍ�
            If jBn.ByteCheckSJIS(Me.TextTellNo.Text, Me.TextTellNo.MaxLength) = False Then
                errMess += Messages.MSG016E.Replace("@PARAM1", "�d�b�ԍ�")
                arrFocusTargetCtrl.Add(Me.TextTellNo)
            End If
            '�Z���P
            If jBn.ByteCheckSJIS(Me.TextJuusyo1.Text, Me.TextJuusyo1.MaxLength) = False Then
                errMess += Messages.MSG016E.Replace("@PARAM1", "�Z���P")
                arrFocusTargetCtrl.Add(Me.TextJuusyo1)
            End If
            '�Z���Q
            If jBn.ByteCheckSJIS(Me.TextJuusyo2.Text, Me.TextJuusyo2.MaxLength) = False Then
                errMess += Messages.MSG016E.Replace("@PARAM1", "�Z���Q")
                arrFocusTargetCtrl.Add(Me.TextJuusyo2)
            End If
            '�S���Җ�
            If jBn.ByteCheckSJIS(Me.TextTantousyaMei.Text, Me.TextTantousyaMei.MaxLength) = False Then
                errMess += Messages.MSG016E.Replace("@PARAM1", "�S���Җ�")
                arrFocusTargetCtrl.Add(Me.TextTantousyaMei)
            End If

        End If

        '�G���[�������͉�ʍĕ`��̂��߁A�R���g���[���̗L��������ؑւ���
        If errMess <> "" Then
            '�t�H�[�J�X�Z�b�g
            If arrFocusTargetCtrl.Count > 0 Then
                arrFocusTargetCtrl(0).Focus()
            End If
            '�G���[���b�Z�[�W�\��
            MLogic.AlertMessage(Me, errMess)
            Return False
        End If

        '�G���[�����̏ꍇ�ATrue��Ԃ�
        Return True
    End Function

    ''' <summary>
    ''' ��ʂ̓��e��DB�ɔ��f����
    ''' </summary>
    ''' <remarks></remarks>
    Protected Function SaveData(ByVal emType As EarthEnum.emSeikyuusyoUpdType) As Boolean
        Dim MyLogic As New SeikyuuDataSearchLogic
        Dim listRec As New List(Of SeikyuuDataRecord)

        '��ʂ��烌�R�[�h�N���X�ɃZ�b�g
        listRec = Me.GetCtrlToDataList()

        ' �f�[�^�̍X�V���s���܂�
        If MyLogic.saveData(Me, listRec, emType) = False Then
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' ��ʂ̊e�������R�[�h�N���X�Ɏ擾���ADB�X�V�p���R�[�h�N���X��ԋp����
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetCtrlToDataList() As List(Of SeikyuuDataRecord)
        Dim listRec As New List(Of SeikyuuDataRecord)
        Dim dtRec As New SeikyuuDataRecord

        With dtRec
            '***************************************
            ' �f�[�^
            '***************************************
            ' ������NO
            cl.SetDisplayString(Me.TextSeikyuusyoNo.Text, .SeikyuusyoNo)
            ' ���
            If Me.BtnTorikesi.Value = BTN_TORIKESI Then
                .Torikesi = 1
            ElseIf Me.BtnTorikesi.Value = BTN_TORIKESI_KAIJO Then
                .Torikesi = 0
            End If
            ' �����於
            cl.SetDisplayString(Me.TextSeikyuuSakiMei1.Text, .SeikyuuSakiMei)
            ' �����於�Q
            cl.SetDisplayString(Me.TextSeikyuuSakiMei2.Text, .SeikyuuSakiMei2)
            ' �X�֔ԍ�
            cl.SetDisplayString(Me.TextYuubin.Text, .YuubinNo)
            ' �d�b�ԍ�
            cl.SetDisplayString(Me.TextTellNo.Text, .TelNo)
            ' �Z���P
            cl.SetDisplayString(Me.TextJuusyo1.Text, .Jyuusyo1)
            ' �Z���Q
            cl.SetDisplayString(Me.TextJuusyo2.Text, .Jyuusyo2)
            ' �S���Җ�
            cl.SetDisplayString(Me.TextTantousyaMei.Text, .TantousyaMei)
            ' �����\���
            cl.SetDisplayString(Me.TextNyuukinYoteiDate.Text, .KonkaiKaisyuuYoteiDate)
            ' �����������
            cl.SetDisplayString(DateTime.Now, .SeikyuusyoInsatuDate)
            ' �X�V�҃��[�U�[ID
            .UpdLoginUserId = userinfo.LoginUserId
            ' �X�V���� �ǂݍ��ݎ��̃^�C���X�^���v
            If Me.HiddenUpdDatetime.Value = "" Then
                .UpdDatetime = System.Data.SqlTypes.SqlDateTime.Null
            Else
                .UpdDatetime = DateTime.ParseExact(Me.HiddenUpdDatetime.Value, EarthConst.FORMAT_DATE_TIME_1, Nothing)
            End If
        End With

        listRec.Add(dtRec)

        Return listRec
    End Function


#End Region

#End Region

#Region "�{�^���C�x���g"

    ''' <summary>
    ''' �C���{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub BtnSyusei_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSyusei.ServerClick
        Dim tmpScript As String = ""
        Dim objActBtn As HtmlInputButton = Me.BtnSyusei

        ' ���̓`�F�b�N
        If Me.checkInput(objActBtn) = False Then Exit Sub

        ' ��ʂ̓��e��DB�ɔ��f����
        If Me.SaveData(EarthEnum.emSeikyuusyoUpdType.SeikyuusyoSyuusei) Then '�o�^����
            '��ʂ����
            cl.CloseWindow(Me)

        Else '�o�^���s
            tmpScript = "alert('" & Messages.MSG019E.Replace("@PARAM1", Me.BtnSyusei.Value) & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "BtnSyusei_ServerClick", tmpScript, True)
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' ����{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub BtnInsatu_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnInsatu.ServerClick
        Dim tmpScript As String = ""
        Dim objActBtn As HtmlInputButton = Me.BtnInsatu

        '����������{�^���������ADB�X�V���s�Ȃ�
        If objActBtn.Value = EarthConst.BUTTON_SEIKYUUSYO_PRINT Then

            ' ���̓`�F�b�N
            If Me.checkInput(objActBtn) = False Then Exit Sub

            ' ��ʂ̓��e��DB�ɔ��f����
            If Me.SaveData(EarthEnum.emSeikyuusyoUpdType.SeikyuusyoPrint) Then

                pStrSeikyuusyoNo = Me.TextSeikyuusyoNo.Text
                SetCtrlFromDataRec(sender, e)

            Else '�o�^���s
                tmpScript = "alert('" & Messages.MSG019E.Replace("@PARAM1", Me.BtnInsatu.Value) & "');"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "BtnInsatu_ServerClick", tmpScript, True)
                Exit Sub
            End If

        End If

        'PDF�o�͏���
        tmpScript = "window.open('" & UrlConst.EARTH2_SEIKYUSYO_FCW_OUTPUT & "?seino=" & Me.TextSeikyuusyoNo.Text & "');"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonHiddenPrint_ServerClick2", tmpScript, True)

    End Sub

    ''' <summary>
    ''' ����{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub BtnTorikesi_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnTorikesi.ServerClick
        Dim tmpScript As String = ""
        Dim objActBtn As HtmlInputButton = Me.BtnTorikesi
        Dim intExistCnt As Integer = 0

        ' ���̓`�F�b�N
        If Me.checkInput(objActBtn) = False Then Exit Sub

        ' ������No�̎擾
        pStrSeikyuusyoNo = Me.TextSeikyuusyoNo.Text

        ' ����������͖��ׁi�`�[���j�[�NNo�j���d�����Ă��Ȃ����`�F�b�N����
        If Me.BtnTorikesi.Value = BTN_TORIKESI_KAIJO Then
            intExistCnt = MyLogic.GetDenpyouExistsCnt(sender, pStrSeikyuusyoNo)
            If intExistCnt > 0 Then
                '���ׂɏd������
                tmpScript = "alert('" & Messages.MSG170E & "');"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "BtnTorikesi_ServerClick", tmpScript, True)
                Exit Sub
            End If
        End If

        ' ��ʂ̓��e��DB�ɔ��f����
        If Me.SaveData(EarthEnum.emSeikyuusyoUpdType.SeikyuusyoTorikesi) Then

            SetCtrlFromDataRec(sender, e)

        Else '�o�^���s
            tmpScript = "alert('" & Messages.MSG019E.Replace("@PARAM1", Me.BtnTorikesi.Value) & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "BtnTorikesi_ServerClick", tmpScript, True)
            Exit Sub
        End If

    End Sub

#End Region

End Class
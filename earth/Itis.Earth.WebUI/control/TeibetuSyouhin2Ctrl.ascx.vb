
Partial Public Class TeibetuSyouhin2Ctrl
    Inherits System.Web.UI.UserControl

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim cbLogic As New CommonBizLogic

#Region "�v���p�e�B"

#Region "���i�R�[�h�Q�̓@�ʐ������R�[�hDictionary"
    ''' <summary>
    ''' ���i�R�[�h�Q�̓@�ʐ������R�[�hDictionary
    ''' </summary>
    ''' <remarks>TeibetuSeikyuuRecord��Dictionary�ł��鎖</remarks>
    Private htbSyouhin2Records As Dictionary(Of Integer, TeibetuSeikyuuRecord)
    ''' <summary>
    ''' ���i�R�[�h�Q�̓@�ʐ���Dictionary
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���i�R�[�h�Q�̓@�ʐ������R�[�hDictionary</returns>
    ''' <remarks>��ʕ\��NO��Key�Ƃ���TeibetuSeikyuuRecord�̃��X�g�ł��鎖</remarks>
    Public Property Syouhin2Records() As Dictionary(Of Integer, TeibetuSeikyuuRecord)
        Get
            ' �X�V�p��Dictionary���Đ�������
            htbSyouhin2Records = New Dictionary(Of Integer, TeibetuSeikyuuRecord)

            ' ���i�Q�|�P
            If Not Syouhin2Record01.TeibetuSeikyuuRec Is Nothing Then
                htbSyouhin2Records.Add(1, Syouhin2Record01.TeibetuSeikyuuRec)
            End If

            ' ���i�Q�|�Q
            If Not Syouhin2Record02.TeibetuSeikyuuRec Is Nothing Then
                htbSyouhin2Records.Add(2, Syouhin2Record02.TeibetuSeikyuuRec)
            End If

            ' ���i�Q�|�R
            If Not Syouhin2Record03.TeibetuSeikyuuRec Is Nothing Then
                htbSyouhin2Records.Add(3, Syouhin2Record03.TeibetuSeikyuuRec)
            End If

            ' ���i�Q�|�S
            If Not Syouhin2Record04.TeibetuSeikyuuRec Is Nothing Then
                htbSyouhin2Records.Add(4, Syouhin2Record04.TeibetuSeikyuuRec)
            End If

            Return htbSyouhin2Records
        End Get
        Set(ByVal value As Dictionary(Of Integer, TeibetuSeikyuuRecord))
            htbSyouhin2Records = value
            ' �f�[�^���R���g���[���ɐݒ�
            SetCtrlData(htbSyouhin2Records)
        End Set
    End Property

#End Region

#Region "�@�ʃf�[�^���ʐݒ���"
    ''' <summary>
    ''' �@�ʃf�[�^���ʐݒ���
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SettingInfo() As TeibetuSettingInfoRecord
        Get
            Dim info As New TeibetuSettingInfoRecord
            info.Kubun = HiddenKubun.Value                                                  ' �敪
            info.Bangou = HiddenBangou.Value                                                ' �ԍ��i�ۏ؏�NO�j
            info.UpdLoginUserId = HiddenLoginUserId.Value                                   ' ���O�C�����[�U�[ID
            info.KeiriGyoumuKengen = Integer.Parse(HiddenKeiriGyoumuKengen.Value)           ' �o���Ɩ�����
            info.HattyuusyoKanriKengen = Integer.Parse(HiddenHattyuusyoKanriKengen.Value)   ' �������Ǘ�����
            Return info
        End Get
        Set(ByVal value As TeibetuSettingInfoRecord)
            HiddenKubun.Value = value.Kubun                                 ' �敪
            HiddenBangou.Value = value.Bangou                               ' �ԍ��i�ۏ؏�NO�j
            HiddenLoginUserId.Value = value.UpdLoginUserId                  ' ���O�C�����[�U�[ID
            ' �o���Ɩ�����
            HiddenKeiriGyoumuKengen.Value = value.KeiriGyoumuKengen.ToString()
            ' �������Ǘ�����
            HiddenHattyuusyoKanriKengen.Value = value.HattyuusyoKanriKengen.ToString()
        End Set
    End Property
#End Region

#Region "�����^�C�v"
    ''' <summary>
    ''' �����^�C�v
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SeikyuuType() As String
        Get
            Return HiddenSeikyuuType.Value
        End Get
        Set(ByVal value As String)
            HiddenSeikyuuType.Value = value
        End Set
    End Property
#End Region

#Region "�n��R�[�h"
    ''' <summary>
    ''' �n��R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KeiretuCd() As String
        Get
            Return HiddenKeiretuCd.Value
        End Get
        Set(ByVal value As String)
            HiddenKeiretuCd.Value = value
        End Set
    End Property
#End Region

#Region "��������UpdatePanel"
    ''' <summary>
    ''' ��������UpdatePanel
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UpdateSeikyuusakiPanel() As UpdatePanel
        Get
            Return UpdatePanelIraiInfo
        End Get
        Set(ByVal value As UpdatePanel)
            UpdatePanelIraiInfo = value
        End Set
    End Property
#End Region

#Region "�ō����z"
    ''' <summary>
    ''' �ō����z
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ZeikomiKingaku() As Integer
        Get
            Dim zeikomi As Integer = 0

            If Not Syouhin2Record01 Is Nothing Then
                zeikomi = Syouhin2Record01.ZeikomiKingaku
            End If
            If Not Syouhin2Record02 Is Nothing Then
                zeikomi = zeikomi + Syouhin2Record02.ZeikomiKingaku
            End If
            If Not Syouhin2Record03 Is Nothing Then
                zeikomi = zeikomi + Syouhin2Record03.ZeikomiKingaku
            End If
            If Not Syouhin2Record04 Is Nothing Then
                zeikomi = zeikomi + Syouhin2Record04.ZeikomiKingaku
            End If

            Return zeikomi
        End Get
    End Property
#End Region

#End Region

#Region "�C�x���g"
    ''' <summary>
    ''' �e��ʂ̏������s�p�J�X�^���C�x���g�i������E�d������ݒ�p�j
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId">���ID</param>
    ''' <remarks></remarks>
    Public Event SetSeikyuuSiireSakiAction(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal strId As String)
    ''' <summary>
    ''' �e��ʂ̏������s�p�J�X�^���C�x���g�i�����^�C�v�ݒ�j
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId">�J�X�^���C�x���g������ID</param>
    ''' <param name="strSeikyuuSakiTypeStr">������^�C�v�i���ڐ���/�������j</param>
    ''' <param name="strKeiretuCd">�n��R�[�h</param>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <remarks></remarks>
    Public Event SetSeikyuuTypeAction(ByVal sender As System.Object _
                                        , ByVal e As System.EventArgs _
                                        , ByVal strId As String _
                                        , ByVal strSeikyuuSakiTypeStr As String _
                                        , ByVal strKeiretuCd As String _
                                        , ByVal strKameitenCd As String)
    ''' <summary>
    ''' �y�[�W���[�h���̃C�x���g�n���h��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

#Region "��������ݒ�"
    ''' <summary>
    ''' ���i2-1���R�[�h�ɐ��������ݒ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ExecSeikyuuInfoSettei21(ByVal sender As System.Object, _
                                   ByVal e As System.EventArgs) Handles Syouhin2Record01.GetSeikyuuInfo
        ' ������������i�Q�R���g���[���֐ݒ肷��
        SetSeikyuuInfo("1")
    End Sub

    ''' <summary>
    ''' ���i2-2���R�[�h�ɐ��������ݒ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ExecSeikyuuInfoSettei22(ByVal sender As System.Object, _
                                   ByVal e As System.EventArgs) Handles Syouhin2Record02.GetSeikyuuInfo
        ' ������������i�Q�R���g���[���֐ݒ肷��
        SetSeikyuuInfo("2")
    End Sub

    ''' <summary>
    ''' ���i2-3���R�[�h�ɐ��������ݒ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ExecSeikyuuInfoSettei23(ByVal sender As System.Object, _
                                   ByVal e As System.EventArgs) Handles Syouhin2Record03.GetSeikyuuInfo
        ' ������������i�Q�R���g���[���֐ݒ肷��
        SetSeikyuuInfo("3")
    End Sub

    ''' <summary>
    ''' ���i2-4���R�[�h�ɐ��������ݒ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ExecSeikyuuInfoSettei24(ByVal sender As System.Object, _
                                   ByVal e As System.EventArgs) Handles Syouhin2Record04.GetSeikyuuInfo
        ' ������������i�Q�R���g���[���֐ݒ肷��
        SetSeikyuuInfo("4")
    End Sub
#End Region

    ''' <summary>
    ''' �ō����z�̕ύX��e�R���g���[���ɒʒm����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event ChangeZeikomiGaku(ByVal sender As System.Object, _
                                   ByVal e As System.EventArgs, _
                                   ByVal zeikomigaku As Integer)

#Region "�ō����z�ύX���̃C�x���g�Q"
    ''' <summary>
    ''' ���i�Q�|�P�ō����z�ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ChangeZeikomiSyouhin21(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs, _
                                      ByVal zeikomigaku As Integer) Handles Syouhin2Record01.ChangeZeikomiGaku

        RaiseEvent ChangeZeikomiGaku(Me, e, ZeikomiKingaku)
    End Sub

    ''' <summary>
    ''' ���i�Q�|�Q�ō����z�ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ChangeZeikomiSyouhin22(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs, _
                                      ByVal zeikomigaku As Integer) Handles Syouhin2Record02.ChangeZeikomiGaku
        RaiseEvent ChangeZeikomiGaku(Me, e, ZeikomiKingaku)
    End Sub

    ''' <summary>
    ''' ���i�Q�|�R�ō����z�ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ChangeZeikomiSyouhin23(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs, _
                                      ByVal zeikomigaku As Integer) Handles Syouhin2Record03.ChangeZeikomiGaku
        RaiseEvent ChangeZeikomiGaku(Me, e, ZeikomiKingaku)
    End Sub

    ''' <summary>
    ''' ���i�Q�|�S�ō����z�ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ChangeZeikomiSyouhin24(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs, _
                                      ByVal zeikomigaku As Integer) Handles Syouhin2Record04.ChangeZeikomiGaku
        RaiseEvent ChangeZeikomiGaku(Me, e, ZeikomiKingaku)
    End Sub

#End Region

#End Region

    ''' <summary>
    ''' ����N�����̕ύX��e�R���g���[���ɒʒm����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event ChangeUriageNengappi(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs, _
                                      ByVal uriagenengappi As String)



#Region "�v���C�x�[�g���\�b�h"

    ''' <summary>
    ''' ���������ݒ肷��
    ''' </summary>
    ''' <param name="rowNo"></param>
    ''' <remarks></remarks>
    Private Sub SetSeikyuuInfo(ByVal rowNo As String)
        Dim teibetuRecCtrl As TeibetuSyouhinRecordCtrl

        teibetuRecCtrl = FindControl("Syouhin2Record0" & rowNo)

        ' ������������i�R���g���[���֐ݒ肷��
        teibetuRecCtrl.SeikyuuType = SeikyuuType
        teibetuRecCtrl.KeiretuCd = KeiretuCd
        teibetuRecCtrl.UpdateSyouhinPanel.Update()

    End Sub

    ''' <summary>
    ''' �擾�f�[�^���R���g���[���ɐݒ肵�܂�
    ''' </summary>
    ''' <param name="dicTeibetuRec"></param>
    ''' <remarks></remarks>
    Private Sub SetCtrlData(ByVal dicTeibetuRec As Dictionary(Of Integer, TeibetuSeikyuuRecord))

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetCtrlData", _
                                            dicTeibetuRec)

        Dim i As Integer
        Dim teibetuRecCtrl As TeibetuSyouhinRecordCtrl

        ' �R���g���[���̐����������s��
        For i = 1 To 4
            teibetuRecCtrl = FindControl("Syouhin2Record0" & i.ToString())

            ' KEY�͐ݒ肷��
            teibetuRecCtrl.SettingInfo = Me.SettingInfo

            ' ��ʕ\��NO��ݒ�
            teibetuRecCtrl.GamenHyoujiNo = i.ToString()

            ' ���i�Q�ݒ�
            If dicTeibetuRec IsNot Nothing AndAlso dicTeibetuRec.ContainsKey(i) Then
                ' �f�[�^�𖾍׃R���g���[���ɃZ�b�g
                teibetuRecCtrl.TeibetuSeikyuuRec = dicTeibetuRec.Item(i)
            Else
                ' �f�[�^�̑��݂��Ȃ����R�[�h�͔񊈐��ɂ���
                teibetuRecCtrl.Enabled = False
            End If
        Next
    End Sub

    ''' <summary>
    ''' ���[�U�[�R���g���[���Őݒ肵��������E�d���������ʂ̉B�����ڂɔ��f����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId">���ID</param>
    ''' <remarks></remarks>
    Private Sub SetSeikyuuSiireInfo(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal strId As String) Handles Syouhin2Record01.SetSeikyuuSiireSakiAction _
                                                                                                                                , Syouhin2Record02.SetSeikyuuSiireSakiAction _
                                                                                                                                , Syouhin2Record03.SetSeikyuuSiireSakiAction _
                                                                                                                                , Syouhin2Record04.SetSeikyuuSiireSakiAction
        '�����d���p�J�X�^���C�x���g
        RaiseEvent SetSeikyuuSiireSakiAction(sender, e, strId)
    End Sub

    ''' <summary>
    ''' �����^�C�v�̐ݒ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId">�J�X�^���C�x���g������ID</param>
    ''' <param name="strSeikyuuSakiTypeStr">������^�C�v(���ڐ���/������)</param>
    ''' <param name="strKeiretuCd">�n��R�[�h</param>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <remarks></remarks>
    Private Sub SetSeikyuuType(ByVal sender As System.Object _
                                    , ByVal e As System.EventArgs _
                                    , ByVal strId As String _
                                    , ByVal strSeikyuuSakiTypeStr As String _
                                    , ByVal strKeiretuCd As String _
                                    , ByVal strKameitenCd As String) Handles Syouhin2Record01.SetSeikyuuTypeAction _
                                                                            , Syouhin2Record02.SetSeikyuuTypeAction _
                                                                            , Syouhin2Record03.SetSeikyuuTypeAction _
                                                                            , Syouhin2Record04.SetSeikyuuTypeAction _

        '�����^�C�v�̐ݒ�
        RaiseEvent SetSeikyuuTypeAction(sender _
                                        , e _
                                        , strId _
                                        , strSeikyuuSakiTypeStr _
                                        , strKeiretuCd _
                                        , strKameitenCd)
    End Sub

#End Region

#Region "�p�u���b�N���\�b�h"
    ''' <summary>
    ''' �G���[�`�F�b�N
    ''' </summary>
    ''' <param name="errMess"></param>
    ''' <param name="arrFocusTargetCtrl"></param>
    ''' <param name="typeWord"></param>
    ''' <param name="denpyouNgList"></param>
    ''' <param name="denpyouErrMess"></param>
    ''' <param name="seikyuuUmuErrMess"></param>
    ''' <param name="strChgPartMess"></param>
    ''' <remarks></remarks>
    Public Sub CheckInput(ByRef errMess As String, _
                          ByRef arrFocusTargetCtrl As List(Of Control), _
                          ByVal typeWord As String, _
                          ByVal denpyouNgList As String, _
                          ByRef denpyouErrMess As String, _
                          ByRef seikyuuUmuErrMess As String, _
                          ByRef strChgPartMess As String _
                          )

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CheckKinsoku", _
                                                    errMess, _
                                                    arrFocusTargetCtrl, _
                                                    typeWord, _
                                                    denpyouNgList, _
                                                    denpyouErrMess, _
                                                    seikyuuUmuErrMess, _
                                                    strChgPartMess _
                                                    )
        Dim setuzoku As String = typeWord
        If typeWord <> "" Then
            setuzoku = typeWord & "_"
        End If

        ' ���i�Q�|�P�̃G���[�`�F�b�N
        Syouhin2Record01.CheckInput(errMess, arrFocusTargetCtrl, setuzoku & "�P", denpyouNgList, denpyouErrMess, seikyuuUmuErrMess, strChgPartMess)

        ' ���i�Q�|�Q�̃G���[�`�F�b�N
        Syouhin2Record02.CheckInput(errMess, arrFocusTargetCtrl, setuzoku & "�Q", denpyouNgList, denpyouErrMess, seikyuuUmuErrMess, strChgPartMess)

        ' ���i�Q�|�R�̃G���[�`�F�b�N
        Syouhin2Record03.CheckInput(errMess, arrFocusTargetCtrl, setuzoku & "�R", denpyouNgList, denpyouErrMess, seikyuuUmuErrMess, strChgPartMess)

        ' ���i�Q�|�S�̃G���[�`�F�b�N
        Syouhin2Record04.CheckInput(errMess, arrFocusTargetCtrl, setuzoku & "�S", denpyouNgList, denpyouErrMess, seikyuuUmuErrMess, strChgPartMess)

    End Sub

    Private Const SYOUHIN2_RECORDS_ID = "Syouhin2Record0"
    ''' <summary>
    ''' ��{������E�d������̐ݒ�
    ''' </summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="strTysKaisyaCd">������ЃR�[�h</param>
    ''' <param name="strKeiretuCd">�n��R�[�h</param>
    ''' <param name="strCtrlId">�w�胆�[�U�[�R���g���[��ID</param>
    ''' <remarks></remarks>
    Public Sub SetDefaultSeikyuuSiireSakiInfo(ByVal strKameitenCd As String _
                                            , ByVal strTysKaisyaCd As String _
                                            , ByVal strKeiretuCd As String _
                                            , Optional ByVal strCtrlId As String = "")
        Dim syouhinCtrl() As TeibetuSyouhinRecordCtrl = {Syouhin2Record01, Syouhin2Record02, Syouhin2Record03, Syouhin2Record04}

        If strCtrlId <> String.Empty Then
            Dim intIndex As Integer

            intIndex = Integer.Parse(strCtrlId.Replace(Me.ClientID & Me.ClientIDSeparator & SYOUHIN2_RECORDS_ID, String.Empty)) - 1
            syouhinCtrl(intIndex).SetDefaultSeikyuuSiireSakiInfo(strKameitenCd, strTysKaisyaCd, strKeiretuCd)
        Else
            For intCnt As Integer = LBound(syouhinCtrl) To UBound(syouhinCtrl)
                syouhinCtrl(intCnt).SetDefaultSeikyuuSiireSakiInfo(strKameitenCd, strTysKaisyaCd, strKeiretuCd)
            Next
        End If
    End Sub

    ''' <summary>
    ''' �����^�C�v�̐ݒ�
    ''' </summary>
    ''' <param name="enSeikyuuType">�����^�C�v</param>
    ''' <param name="strKeiretuCd">�n��R�[�h</param>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <remarks></remarks>
    Public Sub SetSeikyuuType(ByVal enSeikyuuType As EarthEnum.EnumSeikyuuType _
                            , ByVal strKeiretuCd As String _
                            , ByVal strKameitenCd As String _
                            , Optional ByVal strCtrlId As String = "")
        Dim syouhinCtrl() As TeibetuSyouhinRecordCtrl = {Syouhin2Record01, Syouhin2Record02, Syouhin2Record03, Syouhin2Record04}

        If strCtrlId <> String.Empty Then
            Dim intIndex As Integer
            intIndex = Integer.Parse(strCtrlId.Replace(Me.ClientID & Me.ClientIDSeparator & SYOUHIN2_RECORDS_ID, String.Empty)) - 1
            syouhinCtrl(intIndex).SetSeikyuuType(enSeikyuuType, strKeiretuCd, strKameitenCd)
        Else
            For intCnt As Integer = LBound(syouhinCtrl) To UBound(syouhinCtrl)
                syouhinCtrl(intCnt).SetSeikyuuType(enSeikyuuType, strKeiretuCd, strKameitenCd)
            Next
        End If
    End Sub

    ''' <summary>
    '''���i���̐����E�d���悪�ύX����Ă��Ȃ������`�F�b�N���A
    '''�ύX����Ă���ꍇ���̍Ď擾
    ''' </summary>
    ''' <remarks></remarks>
    Public Function setSeikyuuSiireHenkou(ByVal sender As Object, ByVal e As System.EventArgs) As Boolean
        Dim syouhinCtrl() As TeibetuSyouhinRecordCtrl = {Syouhin2Record01, Syouhin2Record02, Syouhin2Record03, Syouhin2Record04}
        For intCnt As Integer = LBound(syouhinCtrl) To UBound(syouhinCtrl)
            If syouhinCtrl(intCnt).AccSeikyuuSiireLink.AccHiddenChkSeikyuuSakiChg.Value = "1" Then
                '���i�Q�C�R�����{�^���������̏��������s
                syouhinCtrl(intCnt).SetSyouhin23(sender, e)
                syouhinCtrl(intCnt).AccSeikyuuSiireLink.AccHiddenChkSeikyuuSakiChg.Value = String.Empty
                '�ύX���ꂽ���i���L�����ꍇ�A���[�v�I��(�����Ƃ��āA1���i�������ύX����Ȃ�����)
                Return True
                Exit Function
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' ���ʑΉ��c�[���`�b�v�ɓ��ʑΉ��R�[�h��ݒ肷��
    ''' </summary>
    ''' <param name="strTokubetuTaiouCd">���ʑΉ��R�[�h</param>
    ''' <param name="rowNo">��ʕ\��NO</param>
    ''' <param name="ttRec">���ʑΉ����R�[�h�N���X</param>
    ''' <remarks></remarks>
    Public Sub SetTokubetuTaiouToolTip(ByVal strTokubetuTaiouCd As String, ByVal rowNo As String, ByVal ttRec As TokubetuTaiouRecordBase)
        Dim teibetuRecCtrl As TeibetuSyouhinRecordCtrl
        Dim emType As EarthEnum.emToolTipType           '�c�[���`�b�v�\���^�C�v

        '�Ώۂ̏��i2�̍s���擾
        teibetuRecCtrl = FindControl("Syouhin2Record0" & rowNo)

        '�c�[���`�b�v�ݒ�Ώۂ��`�F�b�N
        emType = cbLogic.checkToolTipSetValue(Me, ttRec, teibetuRecCtrl.BunruiCd, teibetuRecCtrl.GamenHyoujiNo, teibetuRecCtrl.AccUriageSyori.SelectedValue)
        If emType <> EarthEnum.emToolTipType.NASI Then
            '�\���p
            teibetuRecCtrl.AccTokubetuTaiouToolTip.SetDisplayCd(strTokubetuTaiouCd)

            '���ʑΉ��R�[�h�Ώ�Hdn�Ɋi�[(�o�^�p)
            If teibetuRecCtrl.AccTokubetuTaiouToolTip.AccTaisyouCd.Value = String.Empty Then
                teibetuRecCtrl.AccTokubetuTaiouToolTip.AccTaisyouCd.Value = strTokubetuTaiouCd
            Else
                teibetuRecCtrl.AccTokubetuTaiouToolTip.AccTaisyouCd.Value &= EarthConst.SEP_STRING & strTokubetuTaiouCd
            End If

            If emType = EarthEnum.emToolTipType.SYUSEI Then
                teibetuRecCtrl.AccTokubetuTaiouToolTip.AcclblTokubetuTaiou.Text = EarthConst.SYUU_TOOL_TIP
            End If
        End If

    End Sub

    ''' <summary>
    ''' ���ʑΉ��f�[�^�X�V�t���O������������
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ClearTokubetuTaiouInfo()
        Dim teibetuRecCtrl As TeibetuSyouhinRecordCtrl
        Dim i As Integer

        ' �R���g���[���̐����������s��
        For i = 1 To EarthConst.SYOUHIN2_COUNT
            teibetuRecCtrl = FindControl("Syouhin2Record0" & i.ToString())
            teibetuRecCtrl.AccTokubetuTaiouUpdFlg.Value = EarthConst.NASI_VAL
            teibetuRecCtrl.AccTokubetuTaiouToolTip.AccTaisyouCd.Value = String.Empty
            teibetuRecCtrl.AccTokubetuTaiouToolTip.AccTaisyouCd.Value = String.Empty
        Next
    End Sub

    ''' <summary>
    ''' �c�[���`�b�v������ʑΉ��R�[�h���擾����
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetCdFromToolTip() As String
        Dim teibetuRecCtrl As TeibetuSyouhinRecordCtrl
        Dim i As Integer
        Dim strTmp As String
        Dim strTokubetuTaiouCd As String = String.Empty

        ' �R���g���[���̐����������s��
        For i = 1 To EarthConst.SYOUHIN2_COUNT
            teibetuRecCtrl = FindControl("Syouhin2Record0" & i.ToString())

            '���ʑΉ��f�[�^�X�V�t���O=1�̏ꍇ
            If teibetuRecCtrl.AccTokubetuTaiouUpdFlg.Value = EarthConst.ARI_VAL Then
                strTmp = teibetuRecCtrl.AccTokubetuTaiouToolTip.AccTaisyouCd.Value
                If strTmp <> String.Empty Then
                    If strTokubetuTaiouCd = String.Empty Then
                        strTokubetuTaiouCd = strTmp
                    Else
                        strTokubetuTaiouCd &= EarthConst.SEP_STRING & strTmp
                    End If
                End If
            End If
        Next

        Return strTokubetuTaiouCd
    End Function

#End Region
End Class
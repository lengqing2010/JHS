Public Partial Class PopupDenpyouSiireDateHenkou
    Inherits System.Web.UI.Page

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Private userinfo As New LoginUserInfo
    Private masterAjaxSM As New ScriptManager
    Private MLogic As New MessageLogic
    Private CLogic As New CommonLogic
    Private SLogic As New SiireDataSearchLogic
    Private recSiire As New SiireDataKeyRecord

    ''' <summary>
    ''' �y�[�W��������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim jBn As New Jiban
        Dim jSM As New JibanSessionManager
        Dim strDenNo As String = String.Empty
        Dim strDenSiireDate As String = String.Empty

        '�}�X�^�[�y�[�W�����擾(ScriptManager�p)
        masterAjaxSM = AjaxScriptManager

        ' ���[�U�[��{�F��
        jBn.UserAuth(userinfo)

        '�F�،��ʂɂ���ĉ�ʕ\����ؑւ���
        If userinfo Is Nothing Then
            '���O�C����񂪖����ꍇ
            CLogic.CloseWindow(Me)
            Exit Sub
        End If

        '�������̃`�F�b�N
        '�o���Ɩ�����
        If userinfo.KeiriGyoumuKengen = 0 Then
            CLogic.CloseWindow(Me)
            Exit Sub
        End If

        '����N����
        If IsPostBack = False Then

            '�p�����[�^�̎擾
            strDenNo = CLogic.GetDisplayString(Request("DenUnqNo"))             '�`�[���j�[�NNO�i�ꗗ��ʁj
            strDenSiireDate = CLogic.GetDisplayString(Request("DenSiireDate"))      '�`�[�d���N�����i�ꗗ��ʁj

            '�p�����[�^�s�����͉�ʂ����
            If String.IsNullOrEmpty(strDenNo) OrElse String.IsNullOrEmpty(strDenSiireDate) Then
                Me.TextDenSiireDate.Disabled = True
                Me.ButtonSubMitDisp.Disabled = True
                CLogic.CloseWindow(Me)
                Exit Sub
            End If

            '�p�����[�^�̐ݒ�
            Me.HiddenDenpyouUnqNo.Value = strDenNo
            Me.HiddenDefaultDenSiireDate.Value = strDenSiireDate

            '�C�x���g�n���h���̐ݒ�
            setBtnEvent()

            '����f�[�^�̎擾�ƕ\��
            SetCtrlFromSiireRec(sender, e, strDenNo)

        End If

    End Sub

    ''' <summary>
    ''' �d���f�[�^���R�[�h�����ʕ\�����ڂւ̒l�Z�b�g���s��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strDenNo"></param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromSiireRec(ByVal sender As Object, ByVal e As System.EventArgs, ByVal strDenNo As String)

        Dim end_count As Integer = EarthConst.MAX_RESULT_COUNT
        Dim total_count As Integer = 0
        Dim recResult As New SiireSearchResultRecord
        Dim lgcSiire As New SiireDataSearchLogic
        Dim resultArray As New List(Of SiireSearchResultRecord)
        Dim tmpScript As String = String.Empty

        '�擾�p�����[�^�̐ݒ�
        recSiire.DenUnqNo = strDenNo
        recSiire.NewDenpyouDisp = 1
        recSiire.KeijyouZumiDisp = 1

        '�f�[�^�̎擾
        resultArray = lgcSiire.GetSiireDataInfo(sender, recSiire, 1, end_count, total_count)

        ' �������ʃ[�����̏ꍇ�A���b�Z�[�W��\��
        If total_count = 0 Then
            ' �������ʃ[�����̏ꍇ�A���b�Z�[�W��\��
            MLogic.AlertMessage(sender, Messages.MSG020E, 0, "kensakuZero")
        ElseIf total_count = -1 Then
            ' �������ʌ�����-1�̏ꍇ�A�G���[�Ȃ̂ŁA�����I��
            Exit Sub
        End If

        '�f�[�^�̕\��
        If total_count = 1 Then
            recResult = resultArray(0)
            '�`�[�d���N����
            Me.TextDenSiireDate.Value = CLogic.GetDisplayString(recResult.DenpyouSiireDate)
            '�X�V����
            Me.HiddenRegUpdDate.Value = recResult.UpdDatetime.ToString(EarthConst.FORMAT_DATE_TIME_2)
        End If

        '�ꗗ�ƕύX��ʂō��ق�����ꍇ�i���ɍX�V������ꍇ�j�̂݃��b�Z�[�W��\��
        If Me.HiddenDefaultDenSiireDate.Value <> Me.TextDenSiireDate.Value Then
            tmpScript = "alert('" & Messages.MSG174W & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Page_Load", tmpScript, True)
        End If

    End Sub

    ''' <summary>
    ''' �{�^���C�x���g�̐ݒ�
    ''' </summary>
    ''' <remarks>
    ''' DB�X�V���A��ʂ̃O���C�A�E�g���s�Ȃ��B<br/>
    ''' </remarks>
    Private Sub setBtnEvent()

        '�C�x���g�n���h���o�^
        Dim tmpScript As String = "setWindowOverlay(this,null,1);"

        '�o�^����MSG�m�F��AOK�̏ꍇ�X�V�������s��
        Me.ButtonSubmit.Attributes("onclick") = tmpScript

    End Sub

    ''' <summary>
    ''' �`�[�d���N�����ύX�{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonSubmit_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSubmit.ServerClick
        Dim tmpScript As String = String.Empty

        '���̓`�F�b�N
        If checkInput() = False Then Exit Sub

        ' ��ʂ̓��e��DB�ɔ��f����
        If SaveData() Then '�o�^����
            '�e��ʃ����[�h����
            tmpScript = "funcSubmit();"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "BtnSubmit_ServerClick", tmpScript, True)
            Exit Sub
        Else '�o�^���s
            tmpScript = "alert('" & Messages.MSG019E.Replace("@PARAM1", "�`�[�d���N�����ύX") & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "BtnSubmit_ServerClick", tmpScript, True)
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' ���̓`�F�b�N
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function checkInput() As Boolean

        '�G���[���b�Z�[�W������
        Dim errMess As String = String.Empty
        Dim arrFocusTargetCtrl As New ArrayList
        '���������m��N��
        Dim dtGetujiKakuteiLastSyoriDate As DateTime = DateTime.MinValue

        '�`�[�d���N����
        If Me.TextDenSiireDate.Value <> String.Empty Then
            If CLogic.checkDateHanni(Me.TextDenSiireDate.Value) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "�`�[�d���N����")
                arrFocusTargetCtrl.Add(Me.TextDenSiireDate)
            End If
        Else
            errMess += Messages.MSG013E.Replace("@PARAM1", "�`�[�d���N����")
            arrFocusTargetCtrl.Add(Me.TextDenSiireDate)
        End If

        '�����m��\��ς݂̏����N���u�ȑO�v�̓��t�œ`�[�d���N������ݒ肷��̂̓G���[
        If CLogic.chkGetujiKakuteiYoyakuzumi(Me.TextDenSiireDate.Value, dtGetujiKakuteiLastSyoriDate) = False Then
            errMess += String.Format(Messages.MSG176E, dtGetujiKakuteiLastSyoriDate.Month, "�`�[�d���N����", dtGetujiKakuteiLastSyoriDate.AddMonths(1).Month)
            arrFocusTargetCtrl.Add(Me.TextDenSiireDate)
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
    ''' �X�V����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function SaveData() As Boolean
        Dim recData As New SiireDataRecord

        '��ʂ��烌�R�[�h�N���X�ɃZ�b�g
        recData = GetCtrlToRec()

        '�f�[�^�̍X�V���s���܂�
        If SLogic.SaveSiireData(Me, recData) = False Then
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' ��ʂ̊e�������R�[�h�N���X�Ɏ擾���ADB�X�V�p���R�[�h�N���X��ԋp����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCtrlToRec() As SiireDataRecord
        Dim dtRec As New SiireDataRecord

        '�`�[���j�[�NNO
        CLogic.SetDisplayString(Me.HiddenDenpyouUnqNo.Value, dtRec.DenpyouUniqueNo)
        '�`�[�ԍ�(�`�[�d���N�������ύX�����̂ŁA�̔Ԃ��������ߓ`�[�ԍ��̓N���A)
        dtRec.DenpyouNo = Nothing
        '�`�[�d���N����
        CLogic.SetDisplayString(Me.TextDenSiireDate.Value, dtRec.DenpyouSiireDate)
        '�X�V�҃��[�UID
        CLogic.SetDisplayString(userinfo.LoginUserId, dtRec.UpdLoginUserId)
        '�o�^�E�X�V����
        dtRec.UpdDatetime = Me.HiddenRegUpdDate.Value

        Return dtRec
    End Function

End Class
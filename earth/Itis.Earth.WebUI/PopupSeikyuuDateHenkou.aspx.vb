Public Partial Class PopupSeikyuuDateHenkou
    Inherits System.Web.UI.Page

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Private userinfo As New LoginUserInfo
    Private masterAjaxSM As New ScriptManager
    Private MLogic As New MessageLogic
    Private CLogic As New CommonLogic
    Private MyLogic As New UriageDataSearchLogic
    '����f�[�^�e�[�u�����R�[�h�N���X
    Private rec As New UriageDataKeyRecord

    ''' <summary>
    ''' �y�[�W��������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim jBn As New Jiban    '�n�Չ�ʋ��ʃN���X
        Dim jSM As New JibanSessionManager
        Dim strDenNo As String = String.Empty
        Dim strSeikyuuDate As String = String.Empty

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

            '�p�����[�^�s�����͉�ʂ����
            strDenNo = CLogic.GetDisplayString(Request("DenUnqNo"))             '�`�[���j�[�NNO�i�ꗗ��ʁj
            strSeikyuuDate = CLogic.GetDisplayString(Request("SeikyuuDate"))    '�����N�����i�ꗗ��ʁj
            If String.IsNullOrEmpty(strDenNo) OrElse String.IsNullOrEmpty(strSeikyuuDate) Then
                Me.TextSeikyuuDate.Disabled = True
                Me.ButtonSubMitDisp.Disabled = True
                CLogic.CloseWindow(Me)
                Exit Sub
            End If

            '�p�����[�^�̐ݒ�
            Me.HiddenDenpyouUnqNo.Value = strDenNo
            Me.HiddenDefaultSeikyuuDate.Value = strSeikyuuDate

            '�C�x���g�n���h���̐ݒ�
            setBtnEvent()

            '����f�[�^�̎擾�ƕ\��
            SetCtrlFromUriageRec(sender, e, strDenNo)

        End If

    End Sub

    ''' <summary>
    ''' ����f�[�^���R�[�h�����ʕ\�����ڂւ̒l�Z�b�g���s��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strDenNo"></param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromUriageRec(ByVal sender As Object, ByVal e As System.EventArgs, ByVal strDenNo As String)

        Dim end_count As Integer = EarthConst.MAX_RESULT_COUNT
        Dim total_count As Integer = 0
        Dim recResult As UriageSearchResultRecord
        Dim lgcUri As New UriageDataSearchLogic
        Dim resultArray As List(Of UriageSearchResultRecord)
        Dim tmpScript As String = String.Empty

        '�擾�p�����[�^�̐ݒ�
        rec.DenUnqNo = strDenNo
        rec.NewDenpyouDisp = 1
        rec.KeijyouZumiDisp = 1

        '�f�[�^�̎擾
        resultArray = lgcUri.GetUriageDataInfo(sender, rec, 1, end_count, total_count)

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
            '�����N����
            Me.TextSeikyuuDate.Value = CLogic.GetDisplayString(recResult.SeikyuuDate)
            '�`�[NO
            Me.HiddenDenNo.Value = CLogic.GetDisplayString(recResult.DenNo)
            '�`�[����N����
            Me.HiddenDenUriDate.Value = CLogic.GetDisplayString(recResult.DenUriDate)
            '�X�V����
            Me.HiddenRegUpdDate.Value = recResult.UpdDatetime.ToString(EarthConst.FORMAT_DATE_TIME_2)
        End If

        '�ꗗ�ƕύX��ʂō��ق�����ꍇ�i���ɍX�V������ꍇ�j�̂݃��b�Z�[�W��\��
        If Me.HiddenDefaultSeikyuuDate.Value <> Me.TextSeikyuuDate.Value Then
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
    ''' �����N�����ύX�{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonSubmit_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSubmit.ServerClick
        Dim tmpScript As String = String.Empty

        '���̓`�F�b�N
        If checkInput() = False Then Exit Sub

        ' ��ʂ̓��e��DB�ɔ��f����
        If SaveData(sender) Then '�o�^����
            '�e��ʃ����[�h����
            tmpScript = "funcSubmit();"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "BtnSubmit_ServerClick", tmpScript, True)
            Exit Sub

        Else '�o�^���s
            tmpScript = "alert('" & Messages.MSG019E.Replace("@PARAM1", "�����N�����ύX") & "');"
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

        '�����N����
        If Me.TextSeikyuuDate.Value <> String.Empty Then
            If CLogic.checkDateHanni(Me.TextSeikyuuDate.Value) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "�����N����")
                arrFocusTargetCtrl.Add(Me.TextSeikyuuDate)
            End If
        Else
            errMess += Messages.MSG013E.Replace("@PARAM1", "�����N����")
            arrFocusTargetCtrl.Add(Me.TextSeikyuuDate)
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
    Protected Function SaveData(ByVal sender As System.Object) As Boolean
        ' ����f�[�^���R�[�h
        Dim recUri As New UriageDataRecord
        ' �������������W�b�N
        Dim lgcSeikyuusyoSearch As New SeikyuuDataSearchLogic
        ' ������NO
        Dim strSeikyuusyoNo As String = String.Empty
        ' �X�V���ʃ��b�Z�[�W
        Dim strResultMsg As String = String.Empty
        ' �\���p���b�Z�[�W
        Dim tmpScript As String = String.Empty

        '��ʂ��烌�R�[�h�N���X�ɃZ�b�g
        recUri = GetCtrlToRec()

        '�����\���ƍX�V���ɁA�����N�����̒l���ύX����Ă��邩�`�F�b�N
        If Me.HiddenDefaultSeikyuuDate.Value.Trim() <> Me.TextSeikyuuDate.Value.Trim() Then
            strSeikyuusyoNo = lgcSeikyuusyoSearch.GetSeikyuusyoKagamiNo(sender, Me.HiddenDenpyouUnqNo.Value)
            '�Y���̐����������݂���ꍇ�́C���������
            If Not String.IsNullOrEmpty(strSeikyuusyoNo) Then
                strResultMsg = lgcSeikyuusyoSearch.UpdKagamiTorikesi(sender, strSeikyuusyoNo, userinfo.LoginUserId)
            End If
        End If
        '�������b�Z�[�W�̕\��
        If strResultMsg.Length > 0 Then
            tmpScript = "alert('" & strResultMsg & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Info", tmpScript, True)
            Return False
        End If

        '�f�[�^�̍X�V���s���܂�
        If MyLogic.SaveUriData(Me, recUri) = False Then
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' ��ʂ̊e�������R�[�h�N���X�Ɏ擾���ADB�X�V�p���R�[�h�N���X��ԋp����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCtrlToRec() As UriageDataRecord
        Dim dtRec As New UriageDataRecord

        '�`�[���j�[�NNO
        CLogic.SetDisplayString(Me.HiddenDenpyouUnqNo.Value, dtRec.DenUnqNo)
        '�����N����
        CLogic.SetDisplayString(Me.TextSeikyuuDate.Value, dtRec.SeikyuuDate)
        '�`�[NO
        CLogic.SetDisplayString(Me.HiddenDenNo.Value, dtRec.DenNo)
        '�`�[����N����
        CLogic.SetDisplayString(Me.HiddenDenUriDate.Value, dtRec.DenUriDate)
        '�X�V�҃��[�UID
        CLogic.SetDisplayString(userinfo.LoginUserId, dtRec.UpdLoginUserId)
        '�o�^�E�X�V����
        dtRec.UpdDatetime = Me.HiddenRegUpdDate.Value

        Return dtRec
    End Function

End Class
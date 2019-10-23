Partial Public Class GetujiIkkatuSyuusei
    Inherits System.Web.UI.Page
    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "�ϐ�"
    Private user_info As New LoginUserInfo
    Private masterAjaxSM As New ScriptManager
    '�n�ՃN���X
    Private jBn As New Jiban
    '�����ꊇ�X�V���W�b�N
    Dim clsUpdLogic As New GetujiIkkatuUpdateLogic
    '���b�Z�[�W���W�b�N
    Dim mLogic As New MessageLogic
#End Region

    ''' <summary>
    ''' �y�[�W���[�h���̃C�x���g�n���h��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' ���[�U�[��{�F��
        jBn.UserAuth(user_info)
        If user_info Is Nothing Then
            '���O�C����񂪖����ꍇ�A���C����ʂɔ�΂�
            Response.Redirect(UrlConst.MAIN)
            Exit Sub
        End If
        '��������
        If user_info.KeiriGyoumuKengen <> -1 Then
            '�����������ꍇ�A���C����ʂɔ�΂�
            Response.Redirect(UrlConst.MAIN)
            Exit Sub
        End If

        If IsPostBack = False Then

            Dim today As Date = Date.Now

            ' �����l�ݒ�
            TextUriageFrom.Value = today.Year.ToString() & _
                                   "/" & _
                                   today.Month.ToString("00") & _
                                   "/01"

            TextUriageTo.Value = today.Year.ToString() & _
                                 "/" & _
                                 today.Month.ToString("00") & _
                                 "/" & _
                                 today.Day.ToString("00")

            TextSeikyuuFrom.Value = today.Year.ToString() & _
                                    "/" & _
                                    today.Month.ToString("00") & _
                                    "/" & _
                                    DateTime.DaysInMonth(today.Year, today.Month).ToString("00")

            TextSeikyuuTo.Value = today.Year.ToString() & _
                                  "/" & _
                                  today.Month.ToString("00") & _
                                  "/" & _
                                  DateTime.DaysInMonth(today.Year, today.Month).ToString("00")

            ' ���Z�����\���̃`�F�b�N
            If Now.Month = 3 Or _
               Now.Month = 4 Or _
               Now.Month = 9 Or _
               Now.Month = 10 Then
                ' ���Z�������{�^����3,4,9,10���̂݉����\
                ButtonKessanSyori.Disabled = False
                BtnKessanSyoriCall.Disabled = False
            Else
                ' �����s��
                ButtonKessanSyori.Disabled = True
                BtnKessanSyoriCall.Disabled = True
            End If

            '�����m�菈���Ɋւ����ʕ��i�̏�Ԑݒ�
            GetujiKakuteiGamenSetting()

        End If
    End Sub

    ''' <summary>
    ''' �u���������v�{�^���������̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonGetujiSyori_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonGetujiSyori.ServerClick
        Dim strInfoMsg As String = ""
        Dim intResult As Integer = 0

        '���̓`�F�b�N
        If Not CheckInput() Then
            Exit Sub
        End If

        With clsUpdLogic
            '���O�C�����[�U�[�����N���X�փZ�b�g
            .LoginUserId = user_info.LoginUserId
            '��ʕ\�����e���N���X�փZ�b�g
            .AccUriageFrom = Me.TextUriageFrom.Value
            .AccUriageTo = Me.TextUriageTo.Value
            .AccSeikyuuFrom = Me.TextSeikyuuFrom.Value
            .AccSeikyuuTo = Me.TextSeikyuuTo.Value

            '(2) ���������{�^���������̏���                     
            intResult = .GetujiSyori(sender)
        End With

        '�����I�����b�Z�[�W�\��
        If intResult <> 0 Then
            If intResult = Integer.MinValue Then
                strInfoMsg += Messages.MSG019E.Replace("@PARAM1", "��������")
            Else
                strInfoMsg += Messages.MSG043S.Replace("@PARAM1", "��������")
            End If
        Else
            strInfoMsg += Messages.MSG044S
        End If

        mLogic.AlertMessage(Me, strInfoMsg, 0, "GetujiSyoriError")

    End Sub

    ''' <summary>
    ''' �u���Z�������v�{�^���������̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonKessanSyori_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonKessanSyori.ServerClick
        Dim strInfoMsg As String = ""
        Dim intResult As Integer = 0

        With clsUpdLogic
            '���O�C�����[�U�[�����N���X�փZ�b�g
            .LoginUserId = user_info.LoginUserId

            '(3) ���Z�������������̏���    
            intResult = .KessanSyori(sender)
        End With

        '�����I�����b�Z�[�W�\��
        If intResult <> 0 Then
            If intResult = Integer.MinValue Then
                strInfoMsg += Messages.MSG019E.Replace("@PARAM1", "���Z������")
            Else
                strInfoMsg += Messages.MSG043S.Replace("@PARAM1", "���Z������")
            End If
        Else
            strInfoMsg += Messages.MSG044S
        End If

        mLogic.AlertMessage(Me, strInfoMsg, 0, "KessanSyoriError")

    End Sub

    ''' <summary>
    ''' ���͍��ڃ`�F�b�N
    ''' </summary>
    ''' <remarks></remarks>
    Private Function CheckInput() As Boolean
        '�G���[���b�Z�[�W������
        Dim strErrMsg As String = ""
        Dim arrFocusTargetCtrl As New ArrayList
        Dim strSetFocus As String = ""

        '�K�{�`�F�b�N
        If TextUriageFrom.Value = "" Then
            strErrMsg += Messages.MSG013E.Replace("@PARAM1", "����N����FROM")
            arrFocusTargetCtrl.Add(TextUriageFrom)
        End If
        If TextSeikyuuFrom.Value = "" Then
            strErrMsg += Messages.MSG013E.Replace("@PARAM1", "���������s��FROM")
            arrFocusTargetCtrl.Add(TextSeikyuuFrom)
        End If

        'TO���ږ����͎����͕⊮
        If TextUriageTo.Value = "" Then
            TextUriageTo.Value = TextUriageFrom.Value
        End If
        If TextSeikyuuTo.Value = "" Then
            Me.TextSeikyuuTo.Value = Me.TextSeikyuuFrom.Value
        End If

        '���t�`�F�b�N
        If TextUriageFrom.Value <> "" Then
            If DateTime.Parse(TextUriageFrom.Value) > EarthConst.Instance.MAX_DATE Or DateTime.Parse(TextUriageFrom.Value) < EarthConst.Instance.MIN_DATE Then
                strErrMsg += Messages.MSG040E.Replace("@PARAM1", "����N����FROM")
                arrFocusTargetCtrl.Add(TextUriageFrom)
            End If
        End If
        If TextUriageTo.Value <> "" Then
            If DateTime.Parse(TextUriageTo.Value) > EarthConst.Instance.MAX_DATE Or DateTime.Parse(TextUriageTo.Value) < EarthConst.Instance.MIN_DATE Then
                strErrMsg += Messages.MSG040E.Replace("@PARAM1", "����N����TO")
                arrFocusTargetCtrl.Add(TextUriageTo)
            End If
        End If
        If TextSeikyuuFrom.Value <> "" Then
            If DateTime.Parse(TextSeikyuuFrom.Value) > EarthConst.Instance.MAX_DATE Or DateTime.Parse(TextSeikyuuFrom.Value) < EarthConst.Instance.MIN_DATE Then
                strErrMsg += Messages.MSG040E.Replace("@PARAM1", "���������s��FROM")
                arrFocusTargetCtrl.Add(TextSeikyuuFrom)
            End If
        End If
        If TextSeikyuuTo.Value <> "" Then
            If DateTime.Parse(TextSeikyuuTo.Value) > EarthConst.Instance.MAX_DATE Or DateTime.Parse(TextSeikyuuTo.Value) < EarthConst.Instance.MIN_DATE Then
                strErrMsg += Messages.MSG040E.Replace("@PARAM1", "���������s��TO")
                arrFocusTargetCtrl.Add(TextSeikyuuTo)
            End If
        End If
        '���t�̑召�`�F�b�N
        If TextUriageFrom.Value <> "" And TextUriageTo.Value <> "" Then
            If DateTime.Parse(TextUriageFrom.Value) > DateTime.Parse(TextUriageTo.Value) Then
                strErrMsg += Messages.MSG041E
                arrFocusTargetCtrl.Add(TextUriageFrom)
            End If
        End If
        If TextSeikyuuFrom.Value <> "" And TextSeikyuuTo.Value <> "" Then
            If DateTime.Parse(TextSeikyuuFrom.Value) > DateTime.Parse(TextSeikyuuTo.Value) Then
                strErrMsg += Messages.MSG041E
                arrFocusTargetCtrl.Add(TextSeikyuuFrom)
            End If
        End If

        '�G���[�������͉�ʍĕ`��̂��߁A�R���g���[���̗L��������ؑւ���
        If strErrMsg <> "" Then
            '�t�H�[�J�X�Z�b�g
            If arrFocusTargetCtrl.Count > 0 Then
                arrFocusTargetCtrl(0).Focus()
            End If
            '�G���[���b�Z�[�W�\��
            mLogic.AlertMessage(Me, strErrMsg, 0, "CheckInputError")
            Return False
        End If
        '�G���[�����̏ꍇ�ATrue��Ԃ�
        Return True
    End Function

    ''' <summary>
    ''' �����m�菈���Ɋւ����ʕ��i�̏�Ԑݒ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetujiKakuteiGamenSetting()

        '�����m�菈���֘A�����ݒ�
        TextKakuteiYM.Value = Today.AddMonths(-1).Year.ToString() & _
                              "/" & _
                              Today.AddMonths(-1).Month.ToString("00")

        Dim targetYM As Date = TextKakuteiYM.Value & "/01"

        '���݂̏����󋵂ɂ��{�^���̗L��/������ؑ�
        Dim syoriJoukyou As Object = clsUpdLogic.GetGetujiKakuteiYoyakuData(targetYM.AddMonths(1).AddDays(-1))
        ButtonKakuteiYoyaku.Disabled = True
        ButtonKakuteiYoyakuExe.Disabled = True
        ButtonKakuteiYoyakuKaijo.Disabled = True
        ButtonKakuteiYoyakuKaijoExe.Disabled = True
        If syoriJoukyou Is Nothing OrElse syoriJoukyou = 0 Then
            ButtonKakuteiYoyaku.Disabled = False
            ButtonKakuteiYoyakuExe.Disabled = False
        ElseIf syoriJoukyou = 1 Then
            ButtonKakuteiYoyakuKaijo.Disabled = False
            ButtonKakuteiYoyakuKaijoExe.Disabled = False
        End If

        '���݂̏����󋵂���ʂɃZ�b�g
        Dim strSyoriJoukyou As String = String.Empty
        TextKakuteiSyoriJoukyou.Style.Remove("color")
        If syoriJoukyou Is Nothing Then
            strSyoriJoukyou = "���\��"
        Else
            Select Case CInt(syoriJoukyou)
                Case 0
                    strSyoriJoukyou = "�\����"
                Case 1
                    strSyoriJoukyou = "�\��"
                Case 2
                    strSyoriJoukyou = "������"
                Case 9
                    strSyoriJoukyou = "�����ς�"
                    TextKakuteiSyoriJoukyou.Style("color") = "red"
                Case Else
                    strSyoriJoukyou = syoriJoukyou.ToString
            End Select
        End If
        TextKakuteiSyoriJoukyou.Value = strSyoriJoukyou

    End Sub

    ''' <summary>
    ''' �����m�菈���\��{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonKakuteiYoyakuExe_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonKakuteiYoyakuExe.ServerClick
        ExeGetujiKakuteiYoyaku(sender, 1)

    End Sub

    ''' <summary>
    ''' �����m�菈���\������{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonKakuteiYoyakuKaijoExe_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonKakuteiYoyakuKaijoExe.ServerClick
        ExeGetujiKakuteiYoyaku(sender, 2)

    End Sub

    ''' <summary>
    ''' �����m�菈���\���ԍX�V����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="exeType"></param>
    ''' <remarks></remarks>
    Private Sub ExeGetujiKakuteiYoyaku(ByVal sender As System.Object, ByVal exeType As Integer)

        Dim intResult As Integer
        Dim strInfoMsg As String = String.Empty
        Dim targetYM As Date
        Dim extTypeMess As String = "�����m�菈���̗\��"

        '���b�Z�[�W�ɕ\�����鏈���^�C�v�̕������ύX(���������̏ꍇ)
        If exeType = 2 Then
            extTypeMess &= "����"
        End If

        '�����N����ݒ�
        If TextKakuteiYM.Value <> String.Empty Then
            targetYM = TextKakuteiYM.Value & "/01"
            targetYM = targetYM.AddMonths(1).AddDays(-1)
        Else
            mLogic.AlertMessage(sender, Messages.MSG013E.Replace("@PARAM1", "�m�菈���Ώ۔N��"), 0, "CheckInputError")
            Exit Sub
        End If

        '���W�b�N�N���X�̃v���p�e�B�Ƀ��O�C�����[�U�[ID���Z�b�g
        clsUpdLogic.LoginUserId = user_info.LoginUserId

        '�������s
        intResult = clsUpdLogic.EditGetujiKakuteiYoyaku(sender, exeType, targetYM)

        '�����I�����b�Z�[�W�\��
        If intResult <> 0 Then
            If intResult = Integer.MinValue Then
                strInfoMsg += Messages.MSG155E.Replace("@PARAM1", extTypeMess)
            Else
                strInfoMsg += Messages.MSG043S.Replace("@PARAM1", extTypeMess & "����")
            End If
        Else
            strInfoMsg += Messages.MSG044S
        End If
        mLogic.AlertMessage(Me, strInfoMsg, 0, "KakuteiYoyakuError")

        '�����m�菈���Ɋւ����ʕ��i�̏�Ԑݒ�
        GetujiKakuteiGamenSetting()

    End Sub

End Class
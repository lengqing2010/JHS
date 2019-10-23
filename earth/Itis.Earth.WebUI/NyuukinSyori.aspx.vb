Partial Public Class NyuukinSyori
    Inherits System.Web.UI.Page

#Region "�ϐ�"
    Private user_info As New LoginUserInfo
    Private masterAjaxSM As New ScriptManager
#End Region

    ''' <summary>
    ''' �y�[�W���[�h
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim jBn As New Jiban '�n�Չ�ʋ��ʃN���X
        Dim clsLogic As New NyuukinSyoriLogic

        '�}�X�^�[�y�[�W�����擾�iScriptManager�p�j
        Dim myMaster As EarthMasterPage = Page.Master
        masterAjaxSM = myMaster.AjaxScriptManager1

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
            '****************************************************************************
            ' �h���b�v�_�E�����X�g�ݒ�
            '****************************************************************************
            ' �n��R�[�h�R���{�Ƀf�[�^���o�C���h����
            Dim helper As New DropDownHelper
            helper.SetDropDownList(Me.SelectKeiretuCode, DropDownHelper.DropDownType.KeiretuCd)
            '���������s���̃N���A
            Me.TextSeikyuusyoHakkoubiFrom.Value = ""
            Me.TextSeikyuusyoHakkoubiTo.Value = ""
        End If
    End Sub

    ''' <summary>
    ''' �y�[�W���[�h�R���v���[�g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Dim clsLogic As New NyuukinSyoriLogic
        Dim zenkaiTorikomiTable As DataTable
        Dim dtZenkaiTorikomiDate As Date
        Dim strJs As String

        '�O������f�[�^�捞���̎擾
        zenkaiTorikomiTable = clsLogic.GetZenkaiTorikomiData
        If zenkaiTorikomiTable.Rows.Count > 0 Then
            With zenkaiTorikomiTable.Rows(0)
                '�擾�f�[�^����ʂփZ�b�g
                '�O��捞����
                dtZenkaiTorikomiDate = .Item("syori_datetime")
                Me.TextZenkaiTorikomiNitiji.Value = dtZenkaiTorikomiDate.Year.ToString() & _
                                                    "/" & _
                                                    dtZenkaiTorikomiDate.Month.ToString("00") & _
                                                    "/" & _
                                                    dtZenkaiTorikomiDate.Day.ToString("00")
                Me.TextZenkaiTorikomiNitiji.Value = .Item("syori_datetime")
                '�O��捞�t�@�C����
                Me.TextZenkaiTorikomiFileMei.Value = .Item("nyuuryoku_file_mei")
                '�O��G���[�L��
                If .Item("error_umu") = 0 Then
                    Me.LinkZenkaiErrorUmu.NavigateUrl = ""
                    Me.LinkZenkaiErrorUmu.Text = "����"
                    Me.LinkZenkaiErrorUmu.Attributes.Add("onclick", "")
                    Me.LinkZenkaiErrorUmu.Style("color") = "black"
                Else
                    Me.LinkZenkaiErrorUmu.NavigateUrl = "javascript:void(0);"
                    Me.LinkZenkaiErrorUmu.Text = "�L��"
                    strJs = "window.open('" & UrlConst.NYUUKIN_ERROR & _
                            "?nkn_kbn=0" & _
                            "&edi=" & HttpUtility.UrlEncode(.Item("edi_jouhou_sakusei_date")) & _
                            "&file=" & HttpUtility.UrlEncode(Me.TextZenkaiTorikomiFileMei.Value) & _
                            "','errorWindow','menubar=no,toolbar=no,location=no,status=no,resizable=yes,scrollbars=yes')"
                    Me.LinkZenkaiErrorUmu.Attributes.Add("onclick", strJs)
                End If
            End With
        Else
            Me.TextZenkaiTorikomiNitiji.Value = ""
            Me.TextZenkaiTorikomiFileMei.Value = ""
            Me.LinkZenkaiErrorUmu.NavigateUrl = ""
            Me.LinkZenkaiErrorUmu.Text = ""
        End If

        '�O��JHS�����f�[�^�捞���̎擾
        Dim clsJhsLogic As New JhsNyuukinTorikomiLogic
        Dim recUpload As UploadKanriRecord

        recUpload = clsJhsLogic.GetZenkaiTorikomiData
        If recUpload IsNot Nothing Then

            Me.TextJhsZenkaiTorikomiNitiji.Value = recUpload.SyoriDatetime      '�O��捞����
            Me.TextJhsZenkaiTorikomiFileMei.Value = recUpload.NyuuryokuFileMei  '�O��捞�t�@�C����
            If recUpload.ErrorUmu = 0 Then
            Else
                'Me.LinkJhsZenkaiErrorUmu.NavigateUrl = "javascript:void(0);"
                'Me.LinkJhsZenkaiErrorUmu.Text = "�L��"
                'strJs = "window.open('" & UrlConst.NYUUKIN_ERROR & _
                '        "?nkn_kbn=1" & _
                '        "&edi=" & HttpUtility.UrlEncode(recUpload.EdiJouhouSakuseiDate) & _
                '        "&file=" & HttpUtility.UrlEncode(Me.TextJhsZenkaiTorikomiFileMei.Value) & _
                '        "','errorWindow','menubar=no,toolbar=no,location=no,status=no,resizable=yes,scrollbars=yes')"
                'Me.LinkJhsZenkaiErrorUmu.Attributes.Add("onclick", strJs)
            End If
        Else
            Me.TextJhsZenkaiTorikomiNitiji.Value = ""
            Me.TextJhsZenkaiTorikomiFileMei.Value = ""
            'Me.LinkJhsZenkaiErrorUmu.NavigateUrl = ""
            'Me.LinkJhsZenkaiErrorUmu.Text = ""
        End If
    End Sub

    ''' <summary>
    ''' �ꊇ���������{�^���������̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonIkkatuNyuukinSyori_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonIkkatuNyuukinSyori.ServerClick
        Dim clsLogic As New NyuukinSyoriLogic
        Dim teibetuSeikyuuTable As DataTable
        Dim strInfoMsg As String = ""
        Dim strZeikomiUriageGaku As String
        Dim tmpScript As String
        Dim MLogic As New MessageLogic

        '1.���̓`�F�b�N
        If Not CheckInput() Then
            Exit Sub
        End If

        '2.�@�ʐ����e�[�u���̒��o
        With clsLogic
            '���O�C�����[�U�[�����N���X�փZ�b�g
            .LoginUserId = user_info.LoginUserId
            '��ʕ\�����e���N���X�փZ�b�g
            .AccSeikyuuFrom = Me.TextSeikyuusyoHakkoubiFrom.Value
            .AccSeikyuuTo = Me.TextSeikyuusyoHakkoubiTo.Value
            .AccKeiretuCd = Me.SelectKeiretuCode.Value
            teibetuSeikyuuTable = .GetTeibetuSeikyuuData()
        End With

        '��O����
        If teibetuSeikyuuTable Is Nothing Then
            '�����Ώۃf�[�^�����݂��Ȃ������ꍇ
            strInfoMsg = Messages.MSG020E
            tmpScript = "alert('" & strInfoMsg & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            Exit Sub
        End If

        '3.���s�m�F���b�Z�[�W�̕\��
        '�������z�̎擾
        strZeikomiUriageGaku = clsLogic.CalcZeikomiUriageGaku(teibetuSeikyuuTable)
        If Not IsNumeric(strZeikomiUriageGaku) Then
            MLogic.AlertMessage(sender, strZeikomiUriageGaku)
            Exit Sub
        End If
        Me.HiddenChkUriageGaku.Value = strZeikomiUriageGaku

        strInfoMsg = Messages.MSG048C.Replace("@PARAM1", strZeikomiUriageGaku)
        tmpScript = "if(confirm('" & strInfoMsg & "')){" & vbCrLf
        tmpScript &= "    _d = window.document;"
        tmpScript &= "    setWindowOverlay(this);"
        tmpScript &= "    objEBI(""" & Me.ButtonIkkatuNyuukinSyoriNext.ClientID & """).click();" & vbCrLf
        tmpScript &= "}" & vbCrLf
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "confirm", tmpScript, True)

    End Sub

    ''' <summary>
    ''' �ꊇ���������{�^���������̏����̑���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonIkkatuNyuukinSyoriNext_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonIkkatuNyuukinSyoriNext.ServerClick
        Dim clsLogic As New NyuukinSyoriLogic
        Dim strResultMsg As String
        Dim strInfoMsg As String
        Dim tmpScript As String

        '�ꊇ��������
        With clsLogic
            '���O�C�����[�U�[�����N���X�փZ�b�g
            .LoginUserId = user_info.LoginUserId
            '��ʕ\�����e���N���X�փZ�b�g
            .AccSeikyuuFrom = Me.TextSeikyuusyoHakkoubiFrom.Value
            .AccSeikyuuTo = Me.TextSeikyuusyoHakkoubiTo.Value
            .AccKeiretuCd = Me.SelectKeiretuCode.Value
            strResultMsg = .IkkatuNyuukinSyori(Me.HiddenChkUriageGaku.Value)
        End With

        '8.�������b�Z�[�W�̕\��
        If strResultMsg.Length > 0 Then
            tmpScript = "alert('" & strResultMsg & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Info", tmpScript, True)
        Else
            strInfoMsg = Messages.MSG050S
            tmpScript = "alert('" & strInfoMsg & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Info", tmpScript, True)
        End If
    End Sub

    ''' <summary>
    ''' �����f�[�^��荞�݃{�^���������̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonNyuukinDataTorikomiNext_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonNyuukinDataTorikomiNext.ServerClick
        Dim clsLogic As New NyuukinSyoriLogic
        Dim strErrMsg As String
        Dim strResultMsg As String
        Dim tmpScript As String

        '���O�C�����[�U�[�����N���X�փZ�b�g
        clsLogic.LoginUserId = user_info.LoginUserId

        '1.�Q�ƃ{�^�������ɂ��w�肵���t�@�C���̃A�b�v���[�h���s��
        '�t�@�C���`�F�b�N
        strErrMsg = clsLogic.ChkFile(Me.FileNyuukinDataTorikomi)
        If strErrMsg.Length > 0 Then
            tmpScript = "alert('" & strErrMsg & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Info", tmpScript, True)
            Exit Sub
        End If

        strResultMsg = clsLogic.NyuukinDataTorikomi(Me.FileNyuukinDataTorikomi)

        '4.�������b�Z�[�W�̕\��
        If strResultMsg.Length > 0 Then
            tmpScript = "alert('" & strResultMsg & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Info", tmpScript, True)
        Else
            tmpScript = "alert('" & Messages.MSG060S & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Info", tmpScript, True)
        End If
    End Sub

    ''' <summary>
    ''' JHS�����f�[�^��荞�݃{�^���������̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonJhsNyuukinDataTorikomiNext_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonJhsNyuukinDataTorikomiNext.ServerClick
        Dim clsLogic As New NyuukinSyoriLogic
        Dim clsJhsLogic As New JhsNyuukinTorikomiLogic
        Dim strErrMsg As String
        Dim strResultMsg As String
        Dim tmpScript As String

        '���O�C�����[�U�[�����N���X�փZ�b�g
        clsJhsLogic.LoginUserId = user_info.LoginUserId

        '�t�@�C���`�F�b�N
        strErrMsg = clsLogic.ChkFile(Me.FileJhsNyuukinDataTorikomi)
        If strErrMsg.Length > 0 Then
            tmpScript = "alert('" & strErrMsg & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Info", tmpScript, True)
            Exit Sub
        End If

        strResultMsg = clsJhsLogic.NyuukinDataTorikomi(Me.FileJhsNyuukinDataTorikomi)

        '�������b�Z�[�W�̕\��()
        tmpScript = "alert('" & strResultMsg & "');" & vbCrLf
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Info", tmpScript, True)

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
        If Me.TextSeikyuusyoHakkoubiFrom.Value = "" Then
            strErrMsg += Messages.MSG013E.Replace("@PARAM1", "���������s��FROM")
            arrFocusTargetCtrl.Add(Me.TextSeikyuusyoHakkoubiFrom)
        End If
        If Me.SelectKeiretuCode.Value = "" Then
            strErrMsg += Messages.MSG013E.Replace("@PARAM1", "�n��R�[�h")
            arrFocusTargetCtrl.Add(Me.SelectKeiretuCode)
        End If
        'TO���ږ����͎����͕⊮
        If Me.TextSeikyuusyoHakkoubiTo.Value = "" Then
            Me.TextSeikyuusyoHakkoubiTo.Value = Me.TextSeikyuusyoHakkoubiFrom.Value
        End If
        '���t�`�F�b�N
        If TextSeikyuusyoHakkoubiFrom.Value <> "" Then
            If DateTime.Parse(TextSeikyuusyoHakkoubiFrom.Value) > EarthConst.Instance.MAX_DATE Or DateTime.Parse(TextSeikyuusyoHakkoubiFrom.Value) < EarthConst.Instance.MIN_DATE Then
                strErrMsg += Messages.MSG088E.Replace("@PARAM1", "���������s��FROM")
                arrFocusTargetCtrl.Add(TextSeikyuusyoHakkoubiFrom)
            End If
        End If
        If TextSeikyuusyoHakkoubiTo.Value <> "" Then
            If DateTime.Parse(TextSeikyuusyoHakkoubiTo.Value) > EarthConst.Instance.MAX_DATE Or DateTime.Parse(TextSeikyuusyoHakkoubiTo.Value) < EarthConst.Instance.MIN_DATE Then
                strErrMsg += Messages.MSG088E.Replace("@PARAM1", "���������s��TO")
                arrFocusTargetCtrl.Add(TextSeikyuusyoHakkoubiTo)
            End If
        End If
        '���t�̑召�`�F�b�N
        If Me.TextSeikyuusyoHakkoubiFrom.Value <> "" And Me.TextSeikyuusyoHakkoubiTo.Value <> "" Then
            If DateTime.Parse(Me.TextSeikyuusyoHakkoubiFrom.Value) > DateTime.Parse(Me.TextSeikyuusyoHakkoubiTo.Value) Then
                strErrMsg += Messages.MSG041E
                arrFocusTargetCtrl.Add(Me.TextSeikyuusyoHakkoubiFrom)
            End If
        End If
        '�G���[�������͉�ʍĕ`��̂��߁A�R���g���[���̗L��������ؑւ���
        If strErrMsg <> "" Then
            '�t�H�[�J�X�Z�b�g
            If arrFocusTargetCtrl.Count > 0 Then
                arrFocusTargetCtrl(0).Focus()
            End If
            '�G���[���b�Z�[�W�\��
            Dim tmpScript = "alert('" & strErrMsg & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            Return False
        End If
        '�G���[�����̏ꍇ�ATrue��Ԃ�
        Return True
    End Function

End Class
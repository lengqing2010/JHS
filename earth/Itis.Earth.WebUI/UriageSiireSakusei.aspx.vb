Partial Public Class UriageSiireSakusei
    Inherits System.Web.UI.Page

#Region "�ϐ�"
    Private user_info As New LoginUserInfo
    Private masterAjaxSM As New ScriptManager
    Private CLogic As New CommonLogic
    Private DLogic As New DataLogic
#End Region
#Region "�萔"
#Region "�`�[��ʒ萔"
    ''' <summary>
    ''' ����-�@�ʁi�����j
    ''' </summary>
    ''' <remarks></remarks>
    Private Const URIAGE_TYOUSA As String = "0"
    ''' <summary>
    ''' ����-�@�ʁi�H���j
    ''' </summary>
    ''' <remarks></remarks>
    Private Const URIAGE_KOUJI As String = "1"
    ''' <summary>
    ''' �d��-�@�ʁi�����j
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SIIRE_TYOUSA As String = "2"
    ''' <summary>
    ''' �d��-�@�ʁi�H���j
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SIIRE_KOUJI As String = "3"
    ''' <summary>
    ''' ����-�@�ʁi���̑��j
    ''' </summary>
    ''' <remarks></remarks>
    Private Const URIAGE_HOKA As String = "4"
    ''' <summary>
    ''' ����-�X��
    ''' </summary>
    ''' <remarks></remarks>
    Private Const URIAGE_TENBETU As String = "5"
#End Region
#End Region

#Region "�y�[�W����"
    ''' <summary>
    ''' �y�[�W������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'onclick�C�x���g�̐ݒ�
        Me.radioUriageTyousa.Attributes.Add("onclick", "js_ChgRadioControl(this.id,'" & Me.textUriageTyousa.ClientID & "',1)")
        Me.radioUriageKouji.Attributes.Add("onclick", "js_ChgRadioControl(this.id,'" & Me.textUriageKouji.ClientID & "',2)")
        Me.radioUriageHoka.Attributes.Add("onclick", "js_ChgRadioControl(this.id,'" & Me.textUriageHoka.ClientID & "',3)")
        Me.radioUriageTenbetu.Attributes.Add("onclick", "js_ChgRadioControl(this.id,'" & Me.textUriageTenbetu.ClientID & "',4)")
        Me.radioSiireTyousa.Attributes.Add("onclick", "js_ChgRadioControl(this.id,'" & Me.textSiireTyousa.ClientID & "',5)")
        Me.radioSiireKouji.Attributes.Add("onclick", "js_ChgRadioControl(this.id,'" & Me.textSiireKouji.ClientID & "',6)")
        Me.tdUriageTyousa.Attributes.Add("onclick", "js_ChgRadioControl('" & Me.radioUriageTyousa.ClientID & "','" & Me.textUriageTyousa.ClientID & "',1)")
        Me.tdUriageKouji.Attributes.Add("onclick", "js_ChgRadioControl('" & Me.radioUriageKouji.ClientID & "','" & Me.textUriageKouji.ClientID & "',2)")
        Me.tdUriageHoka.Attributes.Add("onclick", "js_ChgRadioControl('" & Me.radioUriageHoka.ClientID & "','" & Me.textUriageHoka.ClientID & "',3)")
        Me.tdUriageTenbetu.Attributes.Add("onclick", "js_ChgRadioControl('" & Me.radioUriageTenbetu.ClientID & "','" & Me.textUriageTenbetu.ClientID & "',4)")
        Me.tdSiireTyousa.Attributes.Add("onclick", "js_ChgRadioControl('" & Me.radioSiireTyousa.ClientID & "','" & Me.textSiireTyousa.ClientID & "',5)")
        Me.tdSiireKouji.Attributes.Add("onclick", "js_ChgRadioControl('" & Me.radioSiireKouji.ClientID & "','" & Me.textSiireKouji.ClientID & "',6)")
        '�{�^���̔񊈐���
        Me.buttonMakeData.Disabled = True
        Me.buttonMakeDataCall.Disabled = True
        Me.buttonReDownLoad.Disabled = True
        Me.buttonReDownLoadCall.Disabled = True
        Me.buttonMakeDataGetuGaku.Disabled = True
        Me.buttonMakeDataGetuGakuCall.Disabled = True
        Me.buttonReDownLoadGetuGaku.Disabled = True
        Me.buttonReDownLoadGetuGakuCall.Disabled = True
        Me.buttonClearDenpyouNoCall.Disabled = True
        '�{�^���̔�\����
        Me.buttonRelease.Style("display") = "none"
        '�`�[NO�̏�����
        Me.textDenpyou.Value = ""

    End Sub

    ''' <summary>
    ''' �y�[�W���[�h
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim jBn As New Jiban '�n�Չ�ʋ��ʃN���X

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
            '�{�^���̐ݒ�
            Me.setBtnEvent()
        End If

    End Sub

    ''' <summary>
    ''' �y�[�W���[�h�R���v���[�g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Dim clsLogic As New UriageSiireSakuseiLogic
        Dim intResult As Integer
        Dim mLogic As New MessageLogic

        '��ʍ��ڂ̃Z�b�e�B���O
        setDispAction()

        With clsLogic
            '���O�C�����[�U�[�����N���X�փZ�b�g
            .LoginUserId = user_info.LoginUserId
            .UpdDatetime = DateTime.Now
        End With

        '�����I�����b�Z�[�W�\��
        If intResult <> 0 Then
            If intResult = Integer.MinValue Then
                mLogic.AlertMessage(sender, Messages.MSG019E.Replace("@PARAM1", "���f�[�^�ޔ�"))
            End If
        End If

    End Sub
#End Region

#Region "�{�^����������"
    ''' <summary>
    ''' �`�[NO�̃N���A�{�^���������̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonClearDenpyouNo_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonClearDenpyouNo.ServerClick
        Dim clsLogic As New UriageSiireSakuseiLogic
        Dim intFileType As Integer
        Dim strResult As String
        Dim tmpScript As String

        '�t�@�C���o�̓^�C�v�ƃt�@�C����������
        Select Case Me.hiddenSelectedRadioCID.Value
            Case Me.radioUriageTyousa.ClientID
                'CHOUSA.txt
                intFileType = UriageSiireSakuseiLogic.enOutputFileType.UriageTyousa
            Case Me.radioUriageKouji.ClientID
                'KOUJI.txt
                intFileType = UriageSiireSakuseiLogic.enOutputFileType.UriageKouji
            Case Me.radioUriageHoka.ClientID
                'SONOTA.txt
                intFileType = UriageSiireSakuseiLogic.enOutputFileType.UriageSonota
            Case Me.radioUriageTenbetu.ClientID
                'MISEURI.txt
                intFileType = UriageSiireSakuseiLogic.enOutputFileType.UriageMiseuri
            Case Me.radioSiireTyousa.ClientID
                '�����d������.csv
                intFileType = UriageSiireSakuseiLogic.enOutputFileType.SiireTyousa
            Case Me.radioSiireKouji.ClientID
                '�H���d������.csv
                intFileType = UriageSiireSakuseiLogic.enOutputFileType.SiireKouji
            Case Else
                intFileType = -1
        End Select

        With clsLogic
            .AccFileType = intFileType
            If intFileType <> UriageSiireSakuseiLogic.enOutputFileType.UriageTyousa _
                And intFileType <> UriageSiireSakuseiLogic.enOutputFileType.SiireTyousa Then
                .AccDenpyouNo = Integer.Parse(Me.textDenpyou.Value.Substring(2)) - 1
            Else
                .AccDenpyouNo = Integer.Parse(Me.textDenpyou.Value.Substring(1)) - 60000 - 1
            End If
            .LoginUserId = user_info.LoginUserId
            .UpdDatetime = DateTime.Now
        End With

        strResult = clsLogic.ResetDenpyouNo(intFileType.ToString)

        '�G���[���b�Z�[�W��\��
        If strResult.Length > 0 Then
            tmpScript = "alert('" & strResult & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' �f�[�^�쐬�{�^���������̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonMakeData_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonMakeData.ServerClick
        Dim strInfoMsg As String
        Dim tmpScript As String
        Dim clsLogic As New UriageSiireSakuseiLogic
        Dim listCsvRec As New List(Of UriageDataCsvRecord)
        Dim strOutputString As String
        Dim httpRes As HttpResponse
        Dim strFileName As String
        Dim intFileType As Integer

        '���̓`�F�b�N
        If Not CheckInput() Then
            Exit Sub
        End If

        strFileName = ""
        '�t�@�C���o�̓^�C�v�ƃt�@�C����������
        Select Case Me.hiddenSelectedRadioCID.Value
            Case Me.radioUriageTyousa.ClientID
                'CHOUSA.txt
                intFileType = UriageSiireSakuseiLogic.enOutputFileType.UriageTyousa
                strFileName = ConfigurationManager.AppSettings("UriageTyousa")
            Case Me.radioUriageKouji.ClientID
                'KOUJI.txt
                intFileType = UriageSiireSakuseiLogic.enOutputFileType.UriageKouji
                strFileName = ConfigurationManager.AppSettings("UriageKouji")
            Case Me.radioUriageHoka.ClientID
                'SONOTA.txt
                intFileType = UriageSiireSakuseiLogic.enOutputFileType.UriageSonota
                strFileName = ConfigurationManager.AppSettings("UriageSonota")
            Case Me.radioUriageTenbetu.ClientID
                'MISEURI.txt
                intFileType = UriageSiireSakuseiLogic.enOutputFileType.UriageMiseuri
                strFileName = ConfigurationManager.AppSettings("UriageMiseuri")
            Case Me.radioSiireTyousa.ClientID
                '�����d������.csv
                intFileType = UriageSiireSakuseiLogic.enOutputFileType.SiireTyousa
                strFileName = ConfigurationManager.AppSettings("SiireTyousa")
            Case Me.radioSiireKouji.ClientID
                '�H���d������.csv
                intFileType = UriageSiireSakuseiLogic.enOutputFileType.SiireKouji
                strFileName = ConfigurationManager.AppSettings("SiireKouji")
        End Select

        With clsLogic
            '��ʕ\�����e���N���X�փZ�b�g
            .AccUriageFrom = Me.textUriFrom.Value
            .AccUriageTo = Me.textUriTo.Value
            If intFileType <> UriageSiireSakuseiLogic.enOutputFileType.UriageTyousa _
            And intFileType <> UriageSiireSakuseiLogic.enOutputFileType.SiireTyousa Then
                .AccDenpyouNo = Integer.Parse(Me.textDenpyou.Value.Substring(2)) - 1
            Else
                .AccDenpyouNo = Integer.Parse(Me.textDenpyou.Value.Substring(1)) - 1 - 60000
            End If
            .AccFileType = intFileType
            .LoginUserId = user_info.LoginUserId
            .UpdDatetime = DateTime.Now
            '����f�[�^CSV�t�@�C���o�͗p�̕�����𐶐�
            strInfoMsg = .MakeData()
            strOutputString = .OutPutString
        End With

        '�G���[���b�Z�[�W��\��
        If clsLogic.OutPutString = String.Empty And strInfoMsg.Length > 0 Then
            tmpScript = "alert('" & strInfoMsg & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "procInfoMsg", tmpScript, True)
            Exit Sub
        End If

        If strFileName <> String.Empty And clsLogic.OutPutString <> String.Empty Then
            '�t�@�C���̏o�͂��s��
            httpRes = HttpContext.Current.Response
            httpRes.Clear()
            httpRes.AddHeader("Content-Disposition", "attachment;filename=" & HttpUtility.UrlEncode(strFileName))
            httpRes.ContentType = "text/plain"
            httpRes.BinaryWrite(System.Text.Encoding.GetEncoding("Shift-JIS").GetBytes(strOutputString))
            httpRes.End()
        End If

    End Sub

    ''' <summary>
    ''' �f�[�^�쐬�i���ʊ����j�{�^���������̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonMakeDataGetuGaku_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonMakeDataGetuGaku.ServerClick
        'Dim strInfoMsg As String
        'Dim tmpScript As String
        'Dim clsLogic As New UriageSiireSakuseiLogic
        'Dim strOutputString As String
        'Dim httpRes As HttpResponse
        'Dim strFileName As String

        ''���̓`�F�b�N
        'If Not CheckInput() Then
        '    Exit Sub
        'End If

        ''�t�@�C������ݒ�
        'strFileName = ConfigurationManager.AppSettings("UriageWaribiki")

        'With clsLogic
        '    '��ʕ\�����e���N���X�փZ�b�g
        '    .AccUriageFrom = Me.textUriFrom.Value
        '    .AccUriageTo = Me.textUriTo.Value
        '    .AccDenpyouNo = Integer.Parse(Me.textDenpyou.Value.Substring(2)) - 1
        '    .AccFileType = UriageSiireSakuseiLogic.enOutputFileType.UriageMiseuri
        '    .LoginUserId = user_info.LoginUserId
        '    .UpdDatetime = DateTime.Now
        '    '����f�[�^CSV�t�@�C���o�͗p�̕�����𐶐�
        '    strInfoMsg = .MakeDataWaribiki
        '    strOutputString = .OutPutString
        'End With

        ''�G���[���b�Z�[�W��\��
        'If strInfoMsg.Length > 0 Then
        '    tmpScript = "alert('" & strInfoMsg & "');" & vbCrLf
        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
        '    Exit Sub
        'End If

        ''�t�@�C���̏o�͂��s��
        'httpRes = HttpContext.Current.Response
        'httpRes.Clear()
        'httpRes.AddHeader("Content-Disposition", "attachment;filename=" & HttpUtility.UrlEncode(strFileName))
        'httpRes.ContentType = "text/plain"
        'httpRes.BinaryWrite(System.Text.Encoding.GetEncoding("Shift-JIS").GetBytes(strOutputString))
        'httpRes.End()
    End Sub

    ''' <summary>
    ''' �ă_�E�����[�h�{�^���������̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonReDownLoad_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonReDownLoad.ServerClick
        Dim strInfoMsg As String
        Dim tmpScript As String
        Dim clsLogic As New UriageSiireSakuseiLogic
        Dim listCsvRec As New List(Of UriageDataCsvRecord)
        Dim strOutputString As String
        Dim httpRes As HttpResponse
        Dim strFileName As String
        Dim intFileType As Integer

        strFileName = ""
        '�t�@�C���o�̓^�C�v�ƃt�@�C������ݒ�
        Select Case Me.hiddenSelectedRadioCID.Value
            Case Me.radioUriageTyousa.ClientID
                'CHOUSA.txt
                intFileType = UriageSiireSakuseiLogic.enOutputFileType.UriageTyousa
                strFileName = ConfigurationManager.AppSettings("UriageTyousa")
            Case Me.radioUriageKouji.ClientID
                'KOUJI.txt
                intFileType = UriageSiireSakuseiLogic.enOutputFileType.UriageKouji
                strFileName = ConfigurationManager.AppSettings("UriageKouji")
            Case Me.radioUriageHoka.ClientID
                'SONOTA.txt
                intFileType = UriageSiireSakuseiLogic.enOutputFileType.UriageSonota
                strFileName = ConfigurationManager.AppSettings("UriageSonota")
            Case Me.radioUriageTenbetu.ClientID
                'MISEURI.txt
                intFileType = UriageSiireSakuseiLogic.enOutputFileType.UriageMiseuri
                strFileName = ConfigurationManager.AppSettings("UriageMiseuri")
            Case Me.radioSiireTyousa.ClientID
                '�����d������.csv
                intFileType = UriageSiireSakuseiLogic.enOutputFileType.SiireTyousa
                strFileName = ConfigurationManager.AppSettings("SiireTyousa")
            Case Me.radioSiireKouji.ClientID
                '�H���d������.csv
                intFileType = UriageSiireSakuseiLogic.enOutputFileType.SiireKouji
                strFileName = ConfigurationManager.AppSettings("SiireKouji")
        End Select

        With clsLogic
            '�t�@�C���^�C�v�̐ݒ�
            .AccFileType = intFileType
            '�o�͕�����̎擾
            strInfoMsg = .ReDownLoad()
            strOutputString = .OutPutString
        End With

        '�G���[���b�Z�[�W��\��
        If strInfoMsg.Length > 0 Then
            tmpScript = "alert('" & strInfoMsg & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            Exit Sub
        End If

        '�t�@�C���̏o�͂��s��
        httpRes = HttpContext.Current.Response
        httpRes.Clear()
        httpRes.AddHeader("Content-Disposition", "attachment;filename=" & HttpUtility.UrlEncode(strFileName))
        httpRes.ContentType = "text/plain"
        httpRes.BinaryWrite(System.Text.Encoding.GetEncoding("Shift-JIS").GetBytes(strOutputString))
        httpRes.End()

    End Sub

    ''' <summary>
    ''' �ă_�E�����[�h�{�^���i���ʊ����j�������̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonReDownLoadGetuGaku_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonReDownLoadGetuGaku.ServerClick
        Dim strInfoMsg As String
        Dim tmpScript As String
        Dim clsLogic As New UriageSiireSakuseiLogic
        Dim listCsvRec As New List(Of UriageDataCsvRecord)
        Dim strOutputString As String
        Dim httpRes As HttpResponse
        Dim strFileName As String
        Dim intFileType As Integer

        '�t�@�C���o�̓^�C�v�ƃt�@�C������ݒ�
        strFileName = ConfigurationManager.AppSettings("UriageWaribiki")
        intFileType = UriageSiireSakuseiLogic.enOutputFileType.UriageWaribiki

        With clsLogic
            '�t�@�C���^�C�v�̐ݒ�
            .AccFileType = intFileType
            '�o�͕�����̎擾
            strInfoMsg = .ReDownLoad()
            strOutputString = .OutPutString
        End With

        '�G���[���b�Z�[�W��\��
        If strInfoMsg.Length > 0 Then
            tmpScript = "alert('" & strInfoMsg & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            Exit Sub
        End If

        '�t�@�C���̏o�͂��s��
        httpRes = HttpContext.Current.Response
        httpRes.Clear()
        httpRes.AddHeader("Content-Disposition", "attachment;filename=" & HttpUtility.UrlEncode(strFileName))
        httpRes.ContentType = "text/plain"
        httpRes.BinaryWrite(System.Text.Encoding.GetEncoding("Shift-JIS").GetBytes(strOutputString))
        httpRes.End()

    End Sub

    ''' <summary>
    ''' ��ʐ�������{�^���������̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonRelease_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonRelease.ServerClick

    End Sub

    ''' <summary>
    ''' ����/�d��/�����{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>����/�d��/�����f�[�^T.������v���M�t���O���X�V����</remarks>
    Protected Sub ps_UpdTgkSouinFlg(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonUriage.ServerClick _
                                                                                                                , buttonSiire.ServerClick _
                                                                                                                , buttonNyuukin.ServerClick
        Dim clsLogic As New UriageSiireSakuseiLogic
        Dim intResult As Integer
        Dim tmpScript As String = String.Empty

        Dim emBtnType As EarthEnum.emExecBtnType
        Dim strMsg As String = String.Empty

        '���s���{�^��ID
        Dim strTargetId As String = CType(sender, HtmlInputButton).ID

        Select Case strTargetId
            Case buttonUriage.ID
                emBtnType = EarthEnum.emExecBtnType.BtnUriage
                strMsg &= "[����]" & EarthConst.CRLF_CODE & EarthConst.CRLF_CODE
            Case buttonSiire.ID
                emBtnType = EarthEnum.emExecBtnType.BtnSiire
                strMsg &= "[�d��]" & EarthConst.CRLF_CODE & EarthConst.CRLF_CODE
            Case buttonNyuukin.ID
                emBtnType = EarthEnum.emExecBtnType.BtnNyuukin
                strMsg &= "[����]" & EarthConst.CRLF_CODE & EarthConst.CRLF_CODE
            Case Else
                Exit Sub
        End Select

        '���̓`�F�b�N
        If Not CheckInput_Tgk() Then
            Exit Sub
        End If

        '�X�V����
        intResult = clsLogic.UpdTgkSouinFlg(sender, emBtnType, user_info.LoginUserId, Me.textUriTo.Value)

        '�������ʂ����b�Z�[�W�\��
        If intResult < 0 Then
            strMsg &= "�X�V�G���[�ł��B" & EarthConst.CRLF_CODE
        ElseIf intResult >= 0 Then
            strMsg &= "[" & intResult.ToString & "]���X�V���܂����B" & EarthConst.CRLF_CODE
        End If

        If strMsg <> String.Empty Then
            tmpScript = "alert('" & strMsg & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ps_UpdTgkSouinFlg", tmpScript, True)
        End If
    End Sub

#End Region

#Region "��ʕ\�����䏈��"
    ''' <summary>
    ''' ��ʍ��ڂ̓������Z�b�e�B���O���܂�
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setDispAction()
        Dim clsLogic As New UriageSiireSakuseiLogic
        Dim clsRec As DenpyouRecord
        Dim dtNow As DateTime
        Dim dtYesterday As DateTime
        Dim dtUriageTyousa As DateTime
        Dim dtUriageKouji As DateTime
        Dim dtUriageHoka As DateTime
        Dim dtUriageTenbetu As DateTime
        Dim dtSiireTyousa As DateTime
        Dim dtSiireKouji As DateTime
        Dim dicDenpyouInfo As New Dictionary(Of Integer, DenpyouRecord)

        '�f�[�^�쐬���Ɠ`�[NO�̎擾
        With clsLogic
            '����f�[�^�F�@�ʁi�����j
            clsRec = .GetLastUpdDate(URIAGE_TYOUSA)
            dicDenpyouInfo.Add(URIAGE_TYOUSA, clsRec)
            '����f�[�^�F�@�ʁi�H���j
            clsRec = .GetLastUpdDate(URIAGE_KOUJI)
            dicDenpyouInfo.Add(URIAGE_KOUJI, clsRec)
            '����f�[�^�F�@�ʁi���̑��j
            clsRec = .GetLastUpdDate(URIAGE_HOKA)
            dicDenpyouInfo.Add(URIAGE_HOKA, clsRec)
            '����f�[�^�F�X��
            clsRec = .GetLastUpdDate(URIAGE_TENBETU)
            dicDenpyouInfo.Add(URIAGE_TENBETU, clsRec)
            '�d���f�[�^�F�@�ʁi�����j
            clsRec = .GetLastUpdDate(SIIRE_TYOUSA)
            dicDenpyouInfo.Add(SIIRE_TYOUSA, clsRec)
            '�d���f�[�^�F�@�ʁi�H���j
            clsRec = .GetLastUpdDate(SIIRE_KOUJI)
            dicDenpyouInfo.Add(SIIRE_KOUJI, clsRec)
        End With

        '�f�[�^�쐬���̕\��
        dtUriageTyousa = dicDenpyouInfo(URIAGE_TYOUSA).LastSakuseiDateTime
        dtUriageKouji = dicDenpyouInfo(URIAGE_KOUJI).LastSakuseiDateTime
        dtUriageHoka = dicDenpyouInfo(URIAGE_HOKA).LastSakuseiDateTime
        dtUriageTenbetu = dicDenpyouInfo(URIAGE_TENBETU).LastSakuseiDateTime
        dtSiireTyousa = dicDenpyouInfo(SIIRE_TYOUSA).LastSakuseiDateTime
        dtSiireKouji = dicDenpyouInfo(SIIRE_KOUJI).LastSakuseiDateTime

        Me.textUriageTyousa.Value = DLogic.dtTime2Str(dtUriageTyousa, EarthConst.FORMAT_DATE_TIME_7)
        Me.textUriageKouji.Value = DLogic.dtTime2Str(dtUriageKouji, EarthConst.FORMAT_DATE_TIME_7)
        Me.textUriageHoka.Value = DLogic.dtTime2Str(dtUriageHoka, EarthConst.FORMAT_DATE_TIME_7)
        Me.textUriageTenbetu.Value = DLogic.dtTime2Str(dtUriageTenbetu, EarthConst.FORMAT_DATE_TIME_7)
        Me.textSiireTyousa.Value = DLogic.dtTime2Str(dtSiireTyousa, EarthConst.FORMAT_DATE_TIME_7)
        Me.textSiireKouji.Value = DLogic.dtTime2Str(dtSiireKouji, EarthConst.FORMAT_DATE_TIME_7)

        '�`�[NO�̑ޔ�
        Me.hiddenDenpyouNoUriageTyousa.Value = (dicDenpyouInfo(URIAGE_TYOUSA).DenpyouNo + 60001).ToString.PadLeft(6, "0"c)
        Me.hiddenDenpyouNoUriageKouji.Value = "01" & (dicDenpyouInfo(URIAGE_KOUJI).DenpyouNo + 1).ToString.PadLeft(4, "0"c)
        Me.hiddenDenpyouNoUriageHoka.Value = "02" & (dicDenpyouInfo(URIAGE_HOKA).DenpyouNo + 1).ToString.PadLeft(4, "0"c)
        Me.hiddenDenpyouNoUriageTenbetu.Value = "03" & ((dicDenpyouInfo(URIAGE_TENBETU).DenpyouNo) + 1).ToString.PadLeft(4, "0"c)
        Me.hiddenDenpyouNoSiireTyousa.Value = (dicDenpyouInfo(SIIRE_TYOUSA).DenpyouNo + 60001).ToString.PadLeft(6, "0"c)
        Me.hiddenDenpyouNoSiireKouji.Value = "01" & (dicDenpyouInfo(SIIRE_KOUJI).DenpyouNo + 1).ToString.PadLeft(4, "0"c)

        '�f�[�^�쐬���̐ԐF����
        dtNow = DateTime.Now
        dtYesterday = DateTime.Now.AddHours(-24)
        If dtUriageTyousa >= dtYesterday And dtUriageTyousa <= dtNow Then
            Me.textUriageTyousa.Style.Item("color") = "red"
        End If
        If dtUriageKouji >= dtYesterday And dtUriageKouji <= dtNow Then
            Me.textUriageKouji.Style.Item("color") = "red"
        End If
        If dtUriageHoka >= dtYesterday And dtUriageHoka <= dtNow Then
            Me.textUriageHoka.Style.Item("color") = "red"
        End If
        If dtUriageTenbetu > dtYesterday And dtUriageTenbetu <= dtNow Then
            Me.textUriageTenbetu.Style.Item("color") = "red"
        End If
        If dtSiireTyousa >= dtYesterday And dtSiireTyousa <= dtNow Then
            Me.textSiireTyousa.Style.Item("color") = "red"
        End If
        If dtSiireKouji >= dtYesterday And dtSiireKouji <= dtNow Then
            Me.textSiireKouji.Style.Item("color") = "red"
        End If

        '���������x�����b�Z�[�W�̐ݒ�
        Me.SpanGetujiMessage.InnerText = ""
        If dtUriageKouji.Month = DateTime.Now.AddMonths(-1).Month Then
            Me.SpanGetujiMessage.InnerText = Messages.MSG072W
        End If

        '���Z�������x�����b�Z�[�W�̐ݒ�
        Me.SpanKessanMessage.InnerText = ""
        If DateTime.Now.Month = 3 Or DateTime.Now.Month = 9 Then
            Me.SpanKessanMessage.InnerText = Messages.MSG073W
        End If
        If DateTime.Now.Month = 4 Or DateTime.Now.Month = 10 Then
            If _
                    (dtUriageTyousa.Month = 3 OrElse dtUriageTyousa.Month = 9) _
            AndAlso (dtUriageKouji.Month = 3 OrElse dtUriageKouji.Month = 9) _
            AndAlso (dtUriageHoka.Month = 3 OrElse dtUriageHoka.Month = 9) _
            AndAlso (dtUriageTenbetu.Month = 3 OrElse dtUriageTenbetu.Month = 9) _
            AndAlso (dtSiireTyousa.Month = 3 OrElse dtSiireTyousa.Month = 9) _
            AndAlso (dtSiireKouji.Month = 3 OrElse dtSiireKouji.Month = 9) Then
                Me.SpanKessanMessage.InnerText = Messages.MSG073W
            End If
        End If

    End Sub

    ''' <summary>
    ''' ���s�{�^���̐ݒ�
    ''' </summary>
    ''' <remarks>
    ''' DB�X�V�̊m�F���s�Ȃ��B<br/>
    ''' OK���FDB�X�V���s�Ȃ��B<br/>
    ''' �L�����Z�����FDB�X�V�𒆒f����B<br/>
    ''' </remarks>
    Private Sub setBtnEvent()
        '�C�x���g�n���h���o�^
        Dim tmpTouroku As String = "if(confirm('" & Messages.MSG017C & "')){setWindowOverlay(this);}else{return false;}"

        '�o�^����MSG�m�F��AOK�̏ꍇDB�X�V�������s�Ȃ�
        Me.buttonUriage.Attributes("onclick") = tmpTouroku
        Me.buttonSiire.Attributes("onclick") = tmpTouroku
        Me.buttonNyuukin.Attributes("onclick") = tmpTouroku
    End Sub

    ''' <summary>
    ''' ���͍��ڃ`�F�b�N(������v�A���Ή�[���M])
    ''' </summary>
    ''' <remarks></remarks>
    Private Function CheckInput_Tgk() As Boolean
        '�G���[���b�Z�[�W������
        Dim strErrMsg As String = ""
        Dim arrFocusTargetCtrl As New ArrayList
        Dim strSetFocus As String = ""

        '�K�{�`�F�b�N
        If Me.textUriTo.Value = "" Then
            strErrMsg += Messages.MSG013E.Replace("@PARAM1", "����N����TO")
            arrFocusTargetCtrl.Add(Me.textUriTo)
        End If
        '���t�̑召�`�F�b�N
        If Me.textUriFrom.Value <> "" And Me.textUriTo.Value <> "" Then
            If DateTime.Parse(Me.textUriFrom.Value) > DateTime.Parse(Me.textUriTo.Value) Then
                strErrMsg += Messages.MSG041E
                arrFocusTargetCtrl.Add(Me.textUriFrom)
            End If
        End If
        '���t�̕s���l�`�F�b�N
        If Me.textUriTo.Value <> "" Then
            If DateTime.Parse(Me.textUriTo.Value) > EarthConst.Instance.MAX_DATE Or DateTime.Parse(Me.textUriTo.Value) < EarthConst.Instance.MIN_DATE Then
                strErrMsg += Messages.MSG088E.Replace("@PARAM1", "����N����TO")
                arrFocusTargetCtrl.Add(Me.textUriTo)
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
        If Me.textUriFrom.Value = "" Then
            strErrMsg += Messages.MSG013E.Replace("@PARAM1", "����N����FROM")
            arrFocusTargetCtrl.Add(Me.textUriFrom)
        End If
        'TO���ږ����͎����͕⊮
        If Me.textUriTo.Value = "" Then
            Me.textUriTo.Value = Me.textUriFrom.Value
        End If
        '���t�̑召�`�F�b�N
        If Me.textUriFrom.Value <> "" And Me.textUriTo.Value <> "" Then
            If DateTime.Parse(Me.textUriFrom.Value) > DateTime.Parse(Me.textUriTo.Value) Then
                strErrMsg += Messages.MSG041E
                arrFocusTargetCtrl.Add(Me.textUriFrom)
            End If
        End If
        '���t�̕s���l�`�F�b�N
        If Me.textUriFrom.Value <> "" Then
            If DateTime.Parse(Me.textUriFrom.Value) > EarthConst.Instance.MAX_DATE Or DateTime.Parse(Me.textUriFrom.Value) < EarthConst.Instance.MIN_DATE Then
                strErrMsg += Messages.MSG088E.Replace("@PARAM1", "����N����FROM")
                arrFocusTargetCtrl.Add(Me.textUriFrom)
            End If
        End If
        If Me.textUriTo.Value <> "" Then
            If DateTime.Parse(Me.textUriTo.Value) > EarthConst.Instance.MAX_DATE Or DateTime.Parse(Me.textUriTo.Value) < EarthConst.Instance.MIN_DATE Then
                strErrMsg += Messages.MSG088E.Replace("@PARAM1", "����N����TO")
                arrFocusTargetCtrl.Add(Me.textUriTo)
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
#End Region

End Class
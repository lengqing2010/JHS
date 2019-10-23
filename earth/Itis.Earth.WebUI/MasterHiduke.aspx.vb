
Partial Public Class MasterHiduke
    Inherits System.Web.UI.Page

    Dim user_info As New LoginUserInfo
    Dim masterAjaxSM As New ScriptManager

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

        '�F�،��ʂɂ���ĉ�ʕ\����ؑւ���
        If user_info IsNot Nothing Then

            '�ۏ؏����s���ҏW��:  �ۏ؏�����
            If user_info.HosyouGyoumuKengen <> -1 Then
                TextHosyousyoHakkouHenkou.Style("display") = "none"
                ButtonHosyousyoHakkouHenkou.Disabled = True
            Else
                TextHosyousyoHakkouHenkou.Style("display") = "inline"
                ButtonHosyousyoHakkouHenkou.Disabled = False
            End If
            '�񍐏��������ҏW��:  �񍐏�����
            If user_info.HoukokusyoGyoumuKengen <> -1 Then
                TextHoukokusyoHassouHenkou.Style("display") = "none"
                ButtonHoukokusyoHassouHenkou.Disabled = True
            Else
                TextHoukokusyoHassouHenkou.Style("display") = "inline"
                ButtonHoukokusyoHassouHenkou.Disabled = False
            End If
            '�ۏ؏�NO�N���ҏW��: �V�K���͌���
            If user_info.SinkiNyuuryokuKengen <> -1 Then
                TextHosyousyoNoHenkou.Style("display") = "none"
                ButtonHosyousyoNoHenkou.Disabled = True
            Else
                TextHosyousyoNoHenkou.Style("display") = "inline"
                ButtonHosyousyoNoHenkou.Disabled = False
            End If

        Else
            Response.Redirect(UrlConst.MAIN)
        End If

        If IsPostBack = False Then

            '****************************************************************************
            ' �h���b�v�_�E�����X�g�ݒ�
            '****************************************************************************
            ' �敪�R���{�Ƀf�[�^���o�C���h����
            Dim helper As New DropDownHelper
            Dim kbn_logic As New KubunLogic
            helper.SetDropDownList(Me.SelectKubun, DropDownHelper.DropDownType.Kubun, False)

            ' �����lA�Œ�
            SelectKubun.SelectedValue = "A"

            SetHidukeData()

            '****************************************************************************
            ' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
            '****************************************************************************
            TextHosyousyoHakkouHenkou.Attributes("onblur") = "checkDate(this);"
            TextHoukokusyoHassouHenkou.Attributes("onblur") = "checkDate(this);"
            TextHosyousyoNoHenkou.Attributes("onblur") = "checkDateYm(this);"

            '�t�H�[�J�X�ݒ�
            Me.SelectKubun.Focus()

        Else

        End If

    End Sub

    ''' <summary>
    ''' �ۏ؏����s���{�^���������̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonHosyousyoHakkouHenkou_ServerClick(ByVal sender As System.Object, _
                                           ByVal e As System.EventArgs) _
                                           Handles ButtonHosyousyoHakkouHenkou.ServerClick
        Dim logic As New HidukeLogic
        Dim record As New HidukeSaveRecord

        If CheckInput() = False Then
            Exit Sub
        End If

        logic.SetDisplayString(SelectKubun.SelectedValue, record.Kbn)
        logic.SetDisplayString(TextHosyousyoHakkouHenkou.Text, record.HosyousyoHakDate)
        logic.SetDisplayString(TextHoukokusyoHassouDate.Text, record.HkksHassouDate)
        logic.SetDisplayString(TextHosyousyoNo.Text & "/01", record.HosyousyoNoNengetu)

        logic.SetDisplayString(user_info.LoginUserId, record.UpdLoginUserId)
        logic.SetDisplayString(HiddenUpdDateTime.Value, record.UpdDatetime)

        If logic.EditHidukeSaveRecord(sender, record) = True Then
            SetHidukeData()
        End If
    End Sub

    ''' <summary>
    ''' �񍐏��������{�^���������̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonHoukokusyoHassouHenkou_ServerClick(ByVal sender As System.Object, _
                                           ByVal e As System.EventArgs) _
                                           Handles ButtonHoukokusyoHassouHenkou.ServerClick

        Dim logic As New HidukeLogic
        Dim record As New HidukeSaveRecord

        If CheckInput() = False Then
            Exit Sub
        End If

        logic.SetDisplayString(SelectKubun.SelectedValue, record.Kbn)
        logic.SetDisplayString(TextHosyousyoHakkouDate.Text, record.HosyousyoHakDate)
        logic.SetDisplayString(TextHoukokusyoHassouHenkou.Text, record.HkksHassouDate)
        logic.SetDisplayString(TextHosyousyoNo.Text & "/01", record.HosyousyoNoNengetu)

        logic.SetDisplayString(user_info.LoginUserId, record.UpdLoginUserId)
        logic.SetDisplayString(HiddenUpdDateTime.Value, record.UpdDatetime)

        If logic.EditHidukeSaveRecord(sender, record) = True Then
            SetHidukeData()
        End If
    End Sub

    ''' <summary>
    ''' �ۏ؏�NO�N���{�^���������̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonHosyousyoNoHenkou_ServerClick(ByVal sender As System.Object, _
                                           ByVal e As System.EventArgs) _
                                           Handles ButtonHosyousyoNoHenkou.ServerClick

        Dim logic As New HidukeLogic
        Dim record As New HidukeSaveRecord

        If CheckInput() = False Then
            Exit Sub
        End If

        logic.SetDisplayString(SelectKubun.SelectedValue, record.Kbn)
        logic.SetDisplayString(TextHosyousyoHakkouDate.Text, record.HosyousyoHakDate)
        logic.SetDisplayString(TextHoukokusyoHassouDate.Text, record.HkksHassouDate)
        logic.SetDisplayString(TextHosyousyoNoHenkou.Text & "/01", record.HosyousyoNoNengetu)

        logic.SetDisplayString(user_info.LoginUserId, record.UpdLoginUserId)
        logic.SetDisplayString(HiddenUpdDateTime.Value, record.UpdDatetime)

        If logic.EditHidukeSaveRecord(sender, record) = True Then
            SetHidukeData()
        End If

    End Sub


    ''' <summary>
    ''' �敪�R���{�ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectKubun_SelectedIndexChanged(ByVal sender As System.Object, _
                                                   ByVal e As System.EventArgs) _
                                                   Handles SelectKubun.SelectedIndexChanged
        SetHidukeData()
    End Sub

    ''' <summary>
    ''' DB�����tSave�}�X�^�̏����擾���ݒ肷��
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetHidukeData()

        Dim logic As New HidukeLogic

        Dim record As HidukeSaveRecord = logic.GetHidukeRecord(SelectKubun.SelectedValue)

        If Not record Is Nothing Then
            '�ۏ؏����s��
            TextHosyousyoHakkouDate.Text = logic.GetDisplayString(record.HosyousyoHakDate)
            TextHosyousyoHakkouHenkou.Text = TextHosyousyoHakkouDate.Text
            '�񍐏�������
            TextHoukokusyoHassouDate.Text = logic.GetDisplayString(record.HkksHassouDate)
            TextHoukokusyoHassouHenkou.Text = TextHoukokusyoHassouDate.Text
            '�ۏ؏�NO�N��
            TextHosyousyoNo.Text = IIf(record.HosyousyoNoNengetu = DateTime.MinValue, "", record.HosyousyoNoNengetu.ToString("yyyy/MM"))
            TextHosyousyoNoHenkou.Text = TextHosyousyoNo.Text
            '�X�V����
            HiddenUpdDateTime.Value = record.UpdDatetime.ToString(EarthConst.FORMAT_DATE_TIME_2)
        Else
            '�ۏ؏����s��
            TextHosyousyoHakkouDate.Text = String.Empty
            TextHosyousyoHakkouHenkou.Text = String.Empty
            '�񍐏�������
            TextHoukokusyoHassouDate.Text = String.Empty
            TextHoukokusyoHassouHenkou.Text = String.Empty
            '�ۏ؏�NO�N��
            TextHosyousyoNo.Text = String.Empty
            TextHosyousyoNoHenkou.Text = String.Empty
            '�X�V����
            HiddenUpdDateTime.Value = String.Empty
        End If

    End Sub


    ''' <summary>
    ''' ���̓`�F�b�N
    ''' </summary>
    ''' <returns>�`�F�b�N���� True:OK False:NG</returns>
    ''' <remarks></remarks>
    Private Function CheckInput() As Boolean

        '�G���[���b�Z�[�W������
        Dim errMess As String = ""
        Dim arrFocusTargetCtrl As New List(Of Control)

        '���͒l�`�F�b�N
        If TextHosyousyoHakkouHenkou.Text <> "" Then
            If DateTime.Parse(TextHosyousyoHakkouHenkou.Text) > EarthConst.Instance.MAX_DATE Or _
               DateTime.Parse(TextHosyousyoHakkouHenkou.Text) < EarthConst.Instance.MIN_DATE Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "�ۏ؏����s��")
                arrFocusTargetCtrl.Add(TextHosyousyoHakkouHenkou)
            End If
        End If

        If TextHoukokusyoHassouHenkou.Text <> "" Then
            If DateTime.Parse(TextHoukokusyoHassouHenkou.Text) > EarthConst.Instance.MAX_DATE Or _
               DateTime.Parse(TextHoukokusyoHassouHenkou.Text) < EarthConst.Instance.MIN_DATE Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "�񍐏�������")
                arrFocusTargetCtrl.Add(TextHoukokusyoHassouHenkou)
            End If
        End If

        If TextHosyousyoNoHenkou.Text <> "" Then
            If DateTime.Parse(TextHosyousyoNoHenkou.Text) > EarthConst.Instance.MAX_DATE Or _
               DateTime.Parse(TextHosyousyoNoHenkou.Text) < EarthConst.Instance.MIN_DATE Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "�ۏ؏�NO�N��")
                arrFocusTargetCtrl.Add(TextHosyousyoNoHenkou)
            End If
        End If

        If errMess <> "" Then
            '�t�H�[�J�X�Z�b�g
            If arrFocusTargetCtrl.Count > 0 Then
                arrFocusTargetCtrl(0).Focus()
            End If
            '�G���[���b�Z�[�W�\��
            Dim tmpScript = "alert('" & errMess & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            Return False
        End If

        Return True

    End Function


End Class
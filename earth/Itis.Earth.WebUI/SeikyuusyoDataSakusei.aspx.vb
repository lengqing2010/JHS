Public Partial Class SeikyuusyoDataSakusei
    Inherits System.Web.UI.Page

#Region "���ʕϐ�"

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Private userinfo As New LoginUserInfo
    Private masterAjaxSM As New ScriptManager
    Private jSM As New JibanSessionManager
    Private jBn As New Jiban
    Private cl As New CommonLogic
    Private lgcMassage As New MessageLogic
    Private lgcSeiDataSaku As New SeikyuusyoDataSakuseiLogic

    ''' <summary>
    ''' ��������sNo
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emSsCtrl
        intCtrl1 = 1        '�s1
        intCtrl2 = 2        '�s2
        intCtrl3 = 3        '�s3
        intCtrl4 = 4        '�s4
        intCtrl5 = 5        '�s5
        intCtrl6 = 6        '�s6
        intCtrl7 = 7        '�s7
        intCtrl8 = 8        '�s8
        intCtrl9 = 9        '�s9
        intCtrl10 = 10      '�s10
    End Enum

#End Region

    ''' <summary>
    ''' �y�[�W���[�h
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '�}�X�^�[�y�[�W�����擾�iScriptManager�p�j
        Dim myMaster As EarthMasterPage = Page.Master
        masterAjaxSM = myMaster.AjaxScriptManager1

        ' ���[�U�[��{�F��
        jBn.UserAuth(userinfo)
        If userinfo Is Nothing Then
            '���O�C����񂪖����ꍇ�A���C����ʂɔ�΂�
            Response.Redirect(UrlConst.MAIN)
            Exit Sub
        End If

        ' ��������
        If userinfo.KeiriGyoumuKengen <> -1 Then
            '�����������ꍇ�A���C����ʂɔ�΂�
            Response.Redirect(UrlConst.MAIN)
            Exit Sub
        End If


        '�e�e�[�u���̕\����Ԃ�؂�ւ���
        Me.TBodySeikyuuInfo.Style("display") = Me.HiddenDispStyle.Value

        If IsPostBack = False Then

            ' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
            SetDispAction()

            ' �����t�H�[�J�X
            Me.TextSeikyuusyoHakDate.Focus()

            '��ʕ\�����_�̐��������Hidden�ɕێ�(�ύX�`�F�b�N�p)
            If Me.HiddenPushValuesSeikyuu.Value = String.Empty Then
                Me.HiddenPushValuesSeikyuu.Value = getCtrlValuesStringSeikyuu()
            End If

        End If

        Me.HiddenMovePageType.Value = String.Empty

    End Sub

    ''' <summary>
    ''' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetDispAction()

        'JavaScript�֘A��`
        Dim onFocusPostBackScript As String = "removeFig(this);setTempValueForOnBlur(this);"
        Dim onBlurPostBackScript As String = "if(checkTempValueForOnBlur(this)){__doPostBack(this.id,'');}"

        '****************************************************************************
        ' �h���b�v�_�E�����X�g�ݒ�
        '****************************************************************************
        Dim helper As New DropDownHelper
        ' ������敪�R���{�Ƀf�[�^���o�C���h����
        helper.SetKtMeisyouDropDownList(Me.SelectSeikyuuSakiKbn_1, EarthConst.emKtMeisyouType.SEIKYUUSAKI_KBN, True, False)
        helper.SetKtMeisyouDropDownList(Me.SelectSeikyuuSakiKbn_2, EarthConst.emKtMeisyouType.SEIKYUUSAKI_KBN, True, False)
        helper.SetKtMeisyouDropDownList(Me.SelectSeikyuuSakiKbn_3, EarthConst.emKtMeisyouType.SEIKYUUSAKI_KBN, True, False)
        helper.SetKtMeisyouDropDownList(Me.SelectSeikyuuSakiKbn_4, EarthConst.emKtMeisyouType.SEIKYUUSAKI_KBN, True, False)
        helper.SetKtMeisyouDropDownList(Me.SelectSeikyuuSakiKbn_5, EarthConst.emKtMeisyouType.SEIKYUUSAKI_KBN, True, False)
        helper.SetKtMeisyouDropDownList(Me.SelectSeikyuuSakiKbn_6, EarthConst.emKtMeisyouType.SEIKYUUSAKI_KBN, True, False)
        helper.SetKtMeisyouDropDownList(Me.SelectSeikyuuSakiKbn_7, EarthConst.emKtMeisyouType.SEIKYUUSAKI_KBN, True, False)
        helper.SetKtMeisyouDropDownList(Me.SelectSeikyuuSakiKbn_8, EarthConst.emKtMeisyouType.SEIKYUUSAKI_KBN, True, False)
        helper.SetKtMeisyouDropDownList(Me.SelectSeikyuuSakiKbn_9, EarthConst.emKtMeisyouType.SEIKYUUSAKI_KBN, True, False)
        helper.SetKtMeisyouDropDownList(Me.SelectSeikyuuSakiKbn_10, EarthConst.emKtMeisyouType.SEIKYUUSAKI_KBN, True, False)

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '�����挟���n�R�[�h���͍��ڃC�x���g�n���h���ݒ�
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '������R�[�h
        Me.TextSeikyuuSakiCd_1.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSeikyuuSakiCd_1.Attributes("onblur") = onBlurPostBackScript
        Me.TextSeikyuuSakiCd_2.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSeikyuuSakiCd_2.Attributes("onblur") = onBlurPostBackScript
        Me.TextSeikyuuSakiCd_3.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSeikyuuSakiCd_3.Attributes("onblur") = onBlurPostBackScript
        Me.TextSeikyuuSakiCd_4.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSeikyuuSakiCd_4.Attributes("onblur") = onBlurPostBackScript
        Me.TextSeikyuuSakiCd_5.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSeikyuuSakiCd_5.Attributes("onblur") = onBlurPostBackScript
        Me.TextSeikyuuSakiCd_6.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSeikyuuSakiCd_6.Attributes("onblur") = onBlurPostBackScript
        Me.TextSeikyuuSakiCd_7.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSeikyuuSakiCd_7.Attributes("onblur") = onBlurPostBackScript
        Me.TextSeikyuuSakiCd_8.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSeikyuuSakiCd_8.Attributes("onblur") = onBlurPostBackScript
        Me.TextSeikyuuSakiCd_9.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSeikyuuSakiCd_9.Attributes("onblur") = onBlurPostBackScript
        Me.TextSeikyuuSakiCd_10.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSeikyuuSakiCd_10.Attributes("onblur") = onBlurPostBackScript
        '������}��
        Me.TextSeikyuuSakiBrc_1.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSeikyuuSakiBrc_1.Attributes("onblur") = onBlurPostBackScript
        Me.TextSeikyuuSakiBrc_2.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSeikyuuSakiBrc_2.Attributes("onblur") = onBlurPostBackScript
        Me.TextSeikyuuSakiBrc_3.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSeikyuuSakiBrc_3.Attributes("onblur") = onBlurPostBackScript
        Me.TextSeikyuuSakiBrc_4.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSeikyuuSakiBrc_4.Attributes("onblur") = onBlurPostBackScript
        Me.TextSeikyuuSakiBrc_5.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSeikyuuSakiBrc_5.Attributes("onblur") = onBlurPostBackScript
        Me.TextSeikyuuSakiBrc_6.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSeikyuuSakiBrc_6.Attributes("onblur") = onBlurPostBackScript
        Me.TextSeikyuuSakiBrc_7.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSeikyuuSakiBrc_7.Attributes("onblur") = onBlurPostBackScript
        Me.TextSeikyuuSakiBrc_8.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSeikyuuSakiBrc_8.Attributes("onblur") = onBlurPostBackScript
        Me.TextSeikyuuSakiBrc_9.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSeikyuuSakiBrc_9.Attributes("onblur") = onBlurPostBackScript
        Me.TextSeikyuuSakiBrc_10.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSeikyuuSakiBrc_10.Attributes("onblur") = onBlurPostBackScript

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' Javascript�֘A�Z�b�g
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Dim checkDate As String = "checkDate(this);"
        Dim disabledOnkeydown As String = "if (event.keyCode == 13){return false;};"

        '���������s��
        Me.TextSeikyuusyoHakDate.Attributes("onblur") = checkDate
        Me.TextSeikyuusyoHakDate.Attributes("onkeydown") = disabledOnkeydown

        '�������ꗗ�{�^��
        Me.ButtonSyuturyokuSeikyuusyo.Attributes("onclick") = "checkJikkou(this);"
        '�ߋ��������ꗗ�{�^��
        Me.ButtonKakoSeikyuusyo.Attributes("onclick") = "checkJikkou(this);"

        '���������񃊃��N
        Me.SeikyuuDispLink.HRef = "JavaScript:changeDisplay('" & Me.TBodySeikyuuInfo.ClientID & "');SetDisplayStyle('" & Me.HiddenDispStyle.ClientID & "','" & Me.TBodySeikyuuInfo.ClientID & "');"

        '���������ߓ������{�^��
        Me.ButtonSeikyuusyoSimeDateRireki.Attributes("onclick") = "checkJikkou(this);"

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
        Dim blnMstChk As Boolean = True     '�}�X�^�ւ̑��݃`�F�b�N�t���O

        '���R�[�h���͒l�ύX�`�F�b�N
        CheckSeikyuuChg(strErrMsg, arrFocusTargetCtrl)

        '�K�{�`�F�b�N
        If Trim(Me.TextSeikyuusyoHakDate.Value) = String.Empty Then
            strErrMsg += Messages.MSG013E.Replace("@PARAM1", "���������s��")
            arrFocusTargetCtrl.Add(Me.TextSeikyuusyoHakDate)
        End If

        '���t�`�F�b�N
        If Me.TextSeikyuusyoHakDate.Value <> String.Empty Then
            If DateTime.Parse(Me.TextSeikyuusyoHakDate.Value) > EarthConst.Instance.MAX_DATE _
            Or DateTime.Parse(Me.TextSeikyuusyoHakDate.Value) < EarthConst.Instance.MIN_DATE Then
                strErrMsg += Messages.MSG088E.Replace("@PARAM1", "���������s��")
                arrFocusTargetCtrl.Add(TextSeikyuusyoHakDate)
            End If
        End If

        '�������r�`�F�b�N(�ύX�`�F�b�N)
        If Me.HiddenPushValuesSeikyuu.Value <> getCtrlValuesStringSeikyuu() Then
            strErrMsg += Messages.MSG188E
            arrFocusTargetCtrl.Add(Me.btnGetDateCall)
        End If

        '�G���[�������͉�ʍĕ`��̂��߁A�R���g���[���̗L��������ؑւ���
        If strErrMsg <> "" Then

            '������w���W�J����
            Me.TBodySeikyuuInfo.Style("display") = "inline"
            Me.HiddenDispStyle.Value = "inline"

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
    ''' ��������͒l�ύX�`�F�b�N
    ''' </summary>
    ''' <param name="strErrMsg"></param>
    ''' <param name="arrFocusTargetCtrl"></param>
    ''' <remarks></remarks>
    Private Sub CheckSeikyuuChg(ByRef strErrMsg As String, ByRef arrFocusTargetCtrl As ArrayList)

        '������R�[�h�E�}�ԁE�敪�̂ǂꂩ�ɓ��͂�����ꍇ�̂݃`�F�b�N
        If Not String.IsNullOrEmpty(Me.TextSeikyuuSakiCd_1.Text) Or _
           Not String.IsNullOrEmpty(Me.TextSeikyuuSakiBrc_1.Text) Or _
           Me.SelectSeikyuuSakiKbn_1.SelectedIndex <> 0 Then

            '�����{�^���������ƌ��݂ō��ق�����̂��`�F�b�N
            If Me.TextSeikyuuSakiCd_1.Text <> Me.HiddenSeikyuuSakiCdOld_1.Value _
                Or Me.TextSeikyuuSakiBrc_1.Text <> Me.HiddenSeikyuuSakiBrcOld_1.Value _
                Or Me.SelectSeikyuuSakiKbn_1.SelectedValue <> Me.HiddenSeikyuuSakiKbnOld_1.Value Then

                strErrMsg += Messages.MSG030E.Replace("@PARAM1", "������P")
                arrFocusTargetCtrl.Add(btnSeikyuuSakiSearch_1)
            End If

        End If

        '������R�[�h�E�}�ԁE�敪�̂ǂꂩ�ɓ��͂�����ꍇ�̂݃`�F�b�N
        If Not String.IsNullOrEmpty(Me.TextSeikyuuSakiCd_2.Text) Or _
           Not String.IsNullOrEmpty(Me.TextSeikyuuSakiBrc_2.Text) Or _
           Me.SelectSeikyuuSakiKbn_2.SelectedIndex <> 0 Then

            '�����{�^���������ƌ��݂ō��ق�����̂��`�F�b�N
            If Me.TextSeikyuuSakiCd_2.Text <> Me.HiddenSeikyuuSakiCdOld_2.Value _
                Or Me.TextSeikyuuSakiBrc_2.Text <> Me.HiddenSeikyuuSakiBrcOld_2.Value _
                Or Me.SelectSeikyuuSakiKbn_2.SelectedValue <> Me.HiddenSeikyuuSakiKbnOld_2.Value Then

                strErrMsg += Messages.MSG030E.Replace("@PARAM1", "������Q")
                arrFocusTargetCtrl.Add(btnSeikyuuSakiSearch_2)
            End If
        End If

        '������R�[�h�E�}�ԁE�敪�̂ǂꂩ�ɓ��͂�����ꍇ�̂݃`�F�b�N
        If Not String.IsNullOrEmpty(Me.TextSeikyuuSakiCd_3.Text) Or _
           Not String.IsNullOrEmpty(Me.TextSeikyuuSakiBrc_3.Text) Or _
           Me.SelectSeikyuuSakiKbn_2.SelectedIndex <> 0 Then

            '�����{�^���������ƌ��݂ō��ق�����̂��`�F�b�N
            If Me.TextSeikyuuSakiCd_3.Text <> Me.HiddenSeikyuuSakiCdOld_3.Value _
                Or Me.TextSeikyuuSakiBrc_3.Text <> Me.HiddenSeikyuuSakiBrcOld_3.Value _
                Or Me.SelectSeikyuuSakiKbn_3.SelectedValue <> Me.HiddenSeikyuuSakiKbnOld_3.Value Then

                strErrMsg += Messages.MSG030E.Replace("@PARAM1", "������R")
                arrFocusTargetCtrl.Add(btnSeikyuuSakiSearch_3)
            End If
        End If

        '������R�[�h�E�}�ԁE�敪�̂ǂꂩ�ɓ��͂�����ꍇ�̂݃`�F�b�N
        If Not String.IsNullOrEmpty(Me.TextSeikyuuSakiCd_4.Text) Or _
           Not String.IsNullOrEmpty(Me.TextSeikyuuSakiBrc_4.Text) Or _
           Me.SelectSeikyuuSakiKbn_4.SelectedIndex <> 0 Then

            '�����{�^���������ƌ��݂ō��ق�����̂��`�F�b�N
            If Me.TextSeikyuuSakiCd_4.Text <> Me.HiddenSeikyuuSakiCdOld_4.Value _
                Or Me.TextSeikyuuSakiBrc_4.Text <> Me.HiddenSeikyuuSakiBrcOld_4.Value _
                Or Me.SelectSeikyuuSakiKbn_4.SelectedValue <> Me.HiddenSeikyuuSakiKbnOld_4.Value Then

                strErrMsg += Messages.MSG030E.Replace("@PARAM1", "������S")
                arrFocusTargetCtrl.Add(btnSeikyuuSakiSearch_4)
            End If
        End If

        '������R�[�h�E�}�ԁE�敪�̂ǂꂩ�ɓ��͂�����ꍇ�̂݃`�F�b�N
        If Not String.IsNullOrEmpty(Me.TextSeikyuuSakiCd_5.Text) Or _
           Not String.IsNullOrEmpty(Me.TextSeikyuuSakiBrc_5.Text) Or _
           Me.SelectSeikyuuSakiKbn_5.SelectedIndex <> 0 Then

            '�����{�^���������ƌ��݂ō��ق�����̂��`�F�b�N
            If Me.TextSeikyuuSakiCd_5.Text <> Me.HiddenSeikyuuSakiCdOld_5.Value _
                Or Me.TextSeikyuuSakiBrc_5.Text <> Me.HiddenSeikyuuSakiBrcOld_5.Value _
                Or Me.SelectSeikyuuSakiKbn_5.SelectedValue <> Me.HiddenSeikyuuSakiKbnOld_5.Value Then

                strErrMsg += Messages.MSG030E.Replace("@PARAM1", "������T")
                arrFocusTargetCtrl.Add(btnSeikyuuSakiSearch_5)
            End If
        End If

        '������R�[�h�E�}�ԁE�敪�̂ǂꂩ�ɓ��͂�����ꍇ�̂݃`�F�b�N
        If Not String.IsNullOrEmpty(Me.TextSeikyuuSakiCd_6.Text) Or _
           Not String.IsNullOrEmpty(Me.TextSeikyuuSakiBrc_6.Text) Or _
           Me.SelectSeikyuuSakiKbn_6.SelectedIndex <> 0 Then

            '�����{�^���������ƌ��݂ō��ق�����̂��`�F�b�N
            If Me.TextSeikyuuSakiCd_6.Text <> Me.HiddenSeikyuuSakiCdOld_6.Value _
                Or Me.TextSeikyuuSakiBrc_6.Text <> Me.HiddenSeikyuuSakiBrcOld_6.Value _
                Or Me.SelectSeikyuuSakiKbn_6.SelectedValue <> Me.HiddenSeikyuuSakiKbnOld_6.Value Then

                strErrMsg += Messages.MSG030E.Replace("@PARAM1", "������U")
                arrFocusTargetCtrl.Add(btnSeikyuuSakiSearch_6)
            End If
        End If

        '������R�[�h�E�}�ԁE�敪�̂ǂꂩ�ɓ��͂�����ꍇ�̂݃`�F�b�N
        If Not String.IsNullOrEmpty(Me.TextSeikyuuSakiCd_7.Text) Or _
           Not String.IsNullOrEmpty(Me.TextSeikyuuSakiBrc_7.Text) Or _
           Me.SelectSeikyuuSakiKbn_7.SelectedIndex <> 0 Then

            '�����{�^���������ƌ��݂ō��ق�����̂��`�F�b�N
            If Me.TextSeikyuuSakiCd_7.Text <> Me.HiddenSeikyuuSakiCdOld_7.Value _
                Or Me.TextSeikyuuSakiBrc_7.Text <> Me.HiddenSeikyuuSakiBrcOld_7.Value _
                Or Me.SelectSeikyuuSakiKbn_7.SelectedValue <> Me.HiddenSeikyuuSakiKbnOld_7.Value Then

                strErrMsg += Messages.MSG030E.Replace("@PARAM1", "������V")
                arrFocusTargetCtrl.Add(btnSeikyuuSakiSearch_7)
            End If
        End If

        '������R�[�h�E�}�ԁE�敪�̂ǂꂩ�ɓ��͂�����ꍇ�̂݃`�F�b�N
        If Not String.IsNullOrEmpty(Me.TextSeikyuuSakiCd_8.Text) Or _
           Not String.IsNullOrEmpty(Me.TextSeikyuuSakiBrc_8.Text) Or _
           Me.SelectSeikyuuSakiKbn_8.SelectedIndex <> 0 Then

            '�����{�^���������ƌ��݂ō��ق�����̂��`�F�b�N
            If Me.TextSeikyuuSakiCd_8.Text <> Me.HiddenSeikyuuSakiCdOld_8.Value _
                Or Me.TextSeikyuuSakiBrc_8.Text <> Me.HiddenSeikyuuSakiBrcOld_8.Value _
                Or Me.SelectSeikyuuSakiKbn_8.SelectedValue <> Me.HiddenSeikyuuSakiKbnOld_8.Value Then

                strErrMsg += Messages.MSG030E.Replace("@PARAM1", "������W")
                arrFocusTargetCtrl.Add(btnSeikyuuSakiSearch_8)
            End If
        End If

        '������R�[�h�E�}�ԁE�敪�̂ǂꂩ�ɓ��͂�����ꍇ�̂݃`�F�b�N
        If Not String.IsNullOrEmpty(Me.TextSeikyuuSakiCd_9.Text) Or _
           Not String.IsNullOrEmpty(Me.TextSeikyuuSakiBrc_9.Text) Or _
           Me.SelectSeikyuuSakiKbn_9.SelectedIndex <> 0 Then

            '�����{�^���������ƌ��݂ō��ق�����̂��`�F�b�N
            If Me.TextSeikyuuSakiCd_9.Text <> Me.HiddenSeikyuuSakiCdOld_9.Value _
                Or Me.TextSeikyuuSakiBrc_9.Text <> Me.HiddenSeikyuuSakiBrcOld_9.Value _
                Or Me.SelectSeikyuuSakiKbn_9.SelectedValue <> Me.HiddenSeikyuuSakiKbnOld_9.Value Then

                strErrMsg += Messages.MSG030E.Replace("@PARAM1", "������X")
                arrFocusTargetCtrl.Add(btnSeikyuuSakiSearch_9)
            End If
        End If

        '������R�[�h�E�}�ԁE�敪�̂ǂꂩ�ɓ��͂�����ꍇ�̂݃`�F�b�N
        If Not String.IsNullOrEmpty(Me.TextSeikyuuSakiCd_10.Text) Or _
           Not String.IsNullOrEmpty(Me.TextSeikyuuSakiBrc_10.Text) Or _
           Me.SelectSeikyuuSakiKbn_10.SelectedIndex <> 0 Then

            '�����{�^���������ƌ��݂ō��ق�����̂��`�F�b�N
            If Me.TextSeikyuuSakiCd_10.Text <> Me.HiddenSeikyuuSakiCdOld_10.Value _
                Or Me.TextSeikyuuSakiBrc_10.Text <> Me.HiddenSeikyuuSakiBrcOld_10.Value _
                Or Me.SelectSeikyuuSakiKbn_10.SelectedValue <> Me.HiddenSeikyuuSakiKbnOld_10.Value Then

                strErrMsg += Messages.MSG030E.Replace("@PARAM1", "������P�O")
                arrFocusTargetCtrl.Add(btnSeikyuuSakiSearch_10)
            End If
        End If


    End Sub

    ''' <summary>
    ''' ���t�擾�{�^���������̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnGetDate_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetDate.ServerClick

        Dim strErrMsg As String = String.Empty
        Dim arrFocusTargetCtrl As New ArrayList
        Dim strSetFocus As String = String.Empty
        Dim listSsi As New List(Of SeikyuuSakiInfoRecord)
        Dim strResultDate As String = String.Empty
        Dim flgResult As Integer = Integer.MinValue
        Dim intErrFlg As EarthEnum.emHidukeSyutokuErr = EarthEnum.emHidukeSyutokuErr.OK

        '���R�[�h���͒l�ύX�`�F�b�N
        CheckSeikyuuChg(strErrMsg, arrFocusTargetCtrl)

        '�G���[�������͉�ʍĕ`��̂��߁A�R���g���[���̗L��������ؑւ���
        If strErrMsg <> "" Then

            '������w���W�J����
            Me.TBodySeikyuuInfo.Style("display") = "inline"
            Me.HiddenDispStyle.Value = "inline"

            '�t�H�[�J�X�Z�b�g
            If arrFocusTargetCtrl.Count > 0 Then
                arrFocusTargetCtrl(0).Focus()
            End If
            '�G���[���b�Z�[�W�\��
            Dim tmpScript = "alert('" & strErrMsg & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            Exit Sub
        End If

        '��������̎擾
        SetSeikyuuInfo(listSsi)

        '���������s���E�S�Ώ�FLG�̏����l�ݒ�(�ŏ��������擾�B�擾�ł��Ȃ������ꍇ�A�������t)
        lgcSeiDataSaku.getMinSeikyuuSimeDate(listSsi, strResultDate, flgResult, intErrFlg)

        '���t�擾�s�̏ꍇ�̓��b�Z�[�W�\��
        If intErrFlg > EarthEnum.emHidukeSyutokuErr.OK Then
            Select Case intErrFlg
                Case EarthEnum.emHidukeSyutokuErr.SyutokuErr
                    strErrMsg = Messages.MSG196W.Replace("@PARAM1", "���t���擾�o���Ȃ�����").Replace("@PARAM2", "���������s��").Replace("@PARAM3", "���ݓ���")
                Case EarthEnum.emHidukeSyutokuErr.SqlErr
                    strErrMsg = Messages.MSG196W.Replace("@PARAM1", "SQL���s�G���[��").Replace("@PARAM2", "���������s��").Replace("@PARAM3", "���ݓ���")
                Case EarthEnum.emHidukeSyutokuErr.HidukeErr
                    strErrMsg = Messages.MSG196W.Replace("@PARAM1", "���t�`���G���[��").Replace("@PARAM2", "���������s��").Replace("@PARAM3", "���ݓ���")
            End Select
            '�G���[���b�Z�[�W�\��
            Dim tmpScript = "alert('" & strErrMsg & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
        End If

        '���������s���Z�b�g
        Me.TextSeikyuusyoHakDate.Value = strResultDate
        '�S�����A�S��������쐬�Ώۃ`�F�b�N�{�b�N�X�Z�b�g
        If flgResult = "1" Then
            Me.CheckAllSakusei.Checked = True
        ElseIf flgResult = "0" Then
            Me.CheckAllSakusei.Checked = False
        End If

        '���t�擾�{�^���������ɒl��ޔ�
        Me.HiddenPushValuesSeikyuu.Value = getCtrlValuesStringSeikyuu()

    End Sub

    ''' <summary>
    ''' �������f�[�^�쐬�{�^���������̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonSeikyuusyoDataSakusei_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSeikyuusyoDataSakusei.ServerClick

        Dim intDataCnt As Integer = 0
        Dim tmpScript As String = String.Empty
        Dim strResultMsg As String = String.Empty
        Dim strInfoMsg As String = String.Empty
        Dim strSeikyuusyoHakDate As String = String.Empty
        Dim blnAllSakuseiFlg As Boolean = False
        Dim listSsi As New List(Of SeikyuuSakiInfoRecord)

        '���̓`�F�b�N
        If Not CheckInput() Then
            Exit Sub
        End If

        '��������̎擾
        SetSeikyuuInfo(listSsi)

        '�v���p�e�B�̐ݒ�
        lgcSeiDataSaku.LoginUserId = userinfo.LoginUserId

        '��ʍ��ڂ̎擾
        strSeikyuusyoHakDate = Me.TextSeikyuusyoHakDate.Value   '���������s��
        blnAllSakuseiFlg = Me.CheckAllSakusei.Checked     '�������쐬�S�Ώۃt���O

        '�������f�[�^�쐬����
        strResultMsg = lgcSeiDataSaku.SeikyuusyoDataSakuseiSyori(strSeikyuusyoHakDate, blnAllSakuseiFlg, listSsi)

        '�������b�Z�[�W�̕\��
        If strResultMsg.Length > 0 Then
            tmpScript = "alert('" & strResultMsg & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Info", tmpScript, True)
        Else
            strInfoMsg = Messages.MSG018S.Replace("@PARAM1", "�������f�[�^�쐬")
            tmpScript = "alert('" & strInfoMsg & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Info", tmpScript, True)
        End If

    End Sub

    ''' <summary>
    ''' �����挟���{�^���������̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSeikyuuSakiSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim blnResult As Boolean
        Dim strTargetId As String = CType(sender, HtmlInputButton).ID       '���s���{�^��ID
        Dim tmpSeikyuuSakicd As New TextBox                 '������R�[�h
        Dim tmpSeikyuuSakiBrc As New TextBox                '������}��
        Dim tmpSeikyuuSakiKbn As New DropDownList           '������敪
        Dim tmpSeikyuuSakiMei As New HtmlInputText          '�����於
        Dim tmpSeikyuuSakiMeiHdn As New TextBox             '�����於�iHidden�e�L�X�g�{�b�N�X�j
        Dim tmpSeikyuuSakiBtn As New HtmlInputButton        '������{�^��
        Dim hdnOldObj As HtmlInputHidden() = New HtmlInputHidden(2) {}

        '�{�^�������s�ɂ��ďo�Ώۂ�؂蕪��
        Select Case strTargetId
            Case Me.btnSeikyuuSakiSearch_1.ID
                tmpSeikyuuSakicd = Me.TextSeikyuuSakiCd_1
                tmpSeikyuuSakiBrc = Me.TextSeikyuuSakiBrc_1
                tmpSeikyuuSakiKbn = Me.SelectSeikyuuSakiKbn_1
                tmpSeikyuuSakiMei = Me.TextSeikyuuSakiMei_1
                tmpSeikyuuSakiMeiHdn = Me.TextSeikyuuSakiMeiHdn_1
                tmpSeikyuuSakiBtn = Me.btnSeikyuuSakiSearch_1
                hdnOldObj(0) = Me.HiddenSeikyuuSakiKbnOld_1         '�敪���R�[�h���}�Ԃ̏�
                hdnOldObj(1) = Me.HiddenSeikyuuSakiCdOld_1
                hdnOldObj(2) = Me.HiddenSeikyuuSakiBrcOld_1
            Case Me.btnSeikyuuSakiSearch_2.ID
                tmpSeikyuuSakicd = Me.TextSeikyuuSakiCd_2
                tmpSeikyuuSakiBrc = Me.TextSeikyuuSakiBrc_2
                tmpSeikyuuSakiKbn = Me.SelectSeikyuuSakiKbn_2
                tmpSeikyuuSakiMei = Me.TextSeikyuuSakiMei_2
                tmpSeikyuuSakiMeiHdn = Me.TextSeikyuuSakiMeiHdn_2
                tmpSeikyuuSakiBtn = Me.btnSeikyuuSakiSearch_2
                hdnOldObj(0) = Me.HiddenSeikyuuSakiKbnOld_2
                hdnOldObj(1) = Me.HiddenSeikyuuSakiCdOld_2
                hdnOldObj(2) = Me.HiddenSeikyuuSakiBrcOld_2
            Case Me.btnSeikyuuSakiSearch_3.ID
                tmpSeikyuuSakicd = Me.TextSeikyuuSakiCd_3
                tmpSeikyuuSakiBrc = Me.TextSeikyuuSakiBrc_3
                tmpSeikyuuSakiKbn = Me.SelectSeikyuuSakiKbn_3
                tmpSeikyuuSakiMei = Me.TextSeikyuuSakiMei_3
                tmpSeikyuuSakiMeiHdn = Me.TextSeikyuuSakiMeiHdn_3
                tmpSeikyuuSakiBtn = Me.btnSeikyuuSakiSearch_3
                hdnOldObj(0) = Me.HiddenSeikyuuSakiKbnOld_3
                hdnOldObj(1) = Me.HiddenSeikyuuSakiCdOld_3
                hdnOldObj(2) = Me.HiddenSeikyuuSakiBrcOld_3
            Case Me.btnSeikyuuSakiSearch_4.ID
                tmpSeikyuuSakicd = Me.TextSeikyuuSakiCd_4
                tmpSeikyuuSakiBrc = Me.TextSeikyuuSakiBrc_4
                tmpSeikyuuSakiKbn = Me.SelectSeikyuuSakiKbn_4
                tmpSeikyuuSakiMei = Me.TextSeikyuuSakiMei_4
                tmpSeikyuuSakiMeiHdn = Me.TextSeikyuuSakiMeiHdn_4
                tmpSeikyuuSakiBtn = Me.btnSeikyuuSakiSearch_4
                hdnOldObj(0) = Me.HiddenSeikyuuSakiKbnOld_4
                hdnOldObj(1) = Me.HiddenSeikyuuSakiCdOld_4
                hdnOldObj(2) = Me.HiddenSeikyuuSakiBrcOld_4
            Case Me.btnSeikyuuSakiSearch_5.ID
                tmpSeikyuuSakicd = Me.TextSeikyuuSakiCd_5
                tmpSeikyuuSakiBrc = Me.TextSeikyuuSakiBrc_5
                tmpSeikyuuSakiKbn = Me.SelectSeikyuuSakiKbn_5
                tmpSeikyuuSakiMei = Me.TextSeikyuuSakiMei_5
                tmpSeikyuuSakiMeiHdn = Me.TextSeikyuuSakiMeiHdn_5
                tmpSeikyuuSakiBtn = Me.btnSeikyuuSakiSearch_5
                hdnOldObj(0) = Me.HiddenSeikyuuSakiKbnOld_5
                hdnOldObj(1) = Me.HiddenSeikyuuSakiCdOld_5
                hdnOldObj(2) = Me.HiddenSeikyuuSakiBrcOld_5
            Case Me.btnSeikyuuSakiSearch_6.ID
                tmpSeikyuuSakicd = Me.TextSeikyuuSakiCd_6
                tmpSeikyuuSakiBrc = Me.TextSeikyuuSakiBrc_6
                tmpSeikyuuSakiKbn = Me.SelectSeikyuuSakiKbn_6
                tmpSeikyuuSakiMei = Me.TextSeikyuuSakiMei_6
                tmpSeikyuuSakiMeiHdn = Me.TextSeikyuuSakiMeiHdn_6
                tmpSeikyuuSakiBtn = Me.btnSeikyuuSakiSearch_6
                hdnOldObj(0) = Me.HiddenSeikyuuSakiKbnOld_6
                hdnOldObj(1) = Me.HiddenSeikyuuSakiCdOld_6
                hdnOldObj(2) = Me.HiddenSeikyuuSakiBrcOld_6
            Case Me.btnSeikyuuSakiSearch_7.ID
                tmpSeikyuuSakicd = Me.TextSeikyuuSakiCd_7
                tmpSeikyuuSakiBrc = Me.TextSeikyuuSakiBrc_7
                tmpSeikyuuSakiKbn = Me.SelectSeikyuuSakiKbn_7
                tmpSeikyuuSakiMei = Me.TextSeikyuuSakiMei_7
                tmpSeikyuuSakiMeiHdn = Me.TextSeikyuuSakiMeiHdn_7
                tmpSeikyuuSakiBtn = Me.btnSeikyuuSakiSearch_7
                hdnOldObj(0) = Me.HiddenSeikyuuSakiKbnOld_7
                hdnOldObj(1) = Me.HiddenSeikyuuSakiCdOld_7
                hdnOldObj(2) = Me.HiddenSeikyuuSakiBrcOld_7
            Case Me.btnSeikyuuSakiSearch_8.ID
                tmpSeikyuuSakicd = Me.TextSeikyuuSakiCd_8
                tmpSeikyuuSakiBrc = Me.TextSeikyuuSakiBrc_8
                tmpSeikyuuSakiKbn = Me.SelectSeikyuuSakiKbn_8
                tmpSeikyuuSakiMei = Me.TextSeikyuuSakiMei_8
                tmpSeikyuuSakiMeiHdn = Me.TextSeikyuuSakiMeiHdn_8
                tmpSeikyuuSakiBtn = Me.btnSeikyuuSakiSearch_8
                hdnOldObj(0) = Me.HiddenSeikyuuSakiKbnOld_8
                hdnOldObj(1) = Me.HiddenSeikyuuSakiCdOld_8
                hdnOldObj(2) = Me.HiddenSeikyuuSakiBrcOld_8
            Case Me.btnSeikyuuSakiSearch_9.ID
                tmpSeikyuuSakicd = Me.TextSeikyuuSakiCd_9
                tmpSeikyuuSakiBrc = Me.TextSeikyuuSakiBrc_9
                tmpSeikyuuSakiKbn = Me.SelectSeikyuuSakiKbn_9
                tmpSeikyuuSakiMei = Me.TextSeikyuuSakiMei_9
                tmpSeikyuuSakiMeiHdn = Me.TextSeikyuuSakiMeiHdn_9
                tmpSeikyuuSakiBtn = Me.btnSeikyuuSakiSearch_9
                hdnOldObj(0) = Me.HiddenSeikyuuSakiKbnOld_9
                hdnOldObj(1) = Me.HiddenSeikyuuSakiCdOld_9
                hdnOldObj(2) = Me.HiddenSeikyuuSakiBrcOld_9
            Case Me.btnSeikyuuSakiSearch_10.ID
                tmpSeikyuuSakicd = Me.TextSeikyuuSakiCd_10
                tmpSeikyuuSakiBrc = Me.TextSeikyuuSakiBrc_10
                tmpSeikyuuSakiKbn = Me.SelectSeikyuuSakiKbn_10
                tmpSeikyuuSakiMei = Me.TextSeikyuuSakiMei_10
                tmpSeikyuuSakiMeiHdn = Me.TextSeikyuuSakiMeiHdn_10
                tmpSeikyuuSakiBtn = Me.btnSeikyuuSakiSearch_10
                hdnOldObj(0) = Me.HiddenSeikyuuSakiKbnOld_10
                hdnOldObj(1) = Me.HiddenSeikyuuSakiCdOld_10
                hdnOldObj(2) = Me.HiddenSeikyuuSakiBrcOld_10
        End Select


        '�����挟����ʌďo
        blnResult = cl.CallSeikyuuSakiSearchWindow(sender, e, Me _
                                                   , tmpSeikyuuSakiKbn _
                                                   , tmpSeikyuuSakicd _
                                                   , tmpSeikyuuSakiBrc _
                                                   , tmpSeikyuuSakiMeiHdn _
                                                   , tmpSeikyuuSakiBtn _
                                                   , hdnOldObj)
        If blnResult Then
            '�t�H�[�J�X�Z�b�g
            masterAjaxSM.SetFocus(tmpSeikyuuSakiBtn)
            tmpSeikyuuSakiMei.Value = tmpSeikyuuSakiMeiHdn.Text
        End If

    End Sub

    ''' <summary>
    ''' ������敪�ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectSeikyuuSakiKbn_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim strTargetId As String = CType(sender, DropDownList).ID       '���s���R���{

        '�{�^�������s���ďo�Ώۂ�؂蕪��
        Select Case strTargetId
            Case Me.SelectSeikyuuSakiKbn_1.ID
                '������敪�ύX���ɖ��̂��N���A����
                Me.TextSeikyuuSakiMei_1.Value = String.Empty
                masterAjaxSM.SetFocus(SelectSeikyuuSakiKbn_1)
            Case Me.SelectSeikyuuSakiKbn_2.ID
                Me.TextSeikyuuSakiMei_2.Value = String.Empty
                masterAjaxSM.SetFocus(SelectSeikyuuSakiKbn_2)
            Case Me.SelectSeikyuuSakiKbn_3.ID
                Me.TextSeikyuuSakiMei_3.Value = String.Empty
                masterAjaxSM.SetFocus(SelectSeikyuuSakiKbn_3)
            Case Me.SelectSeikyuuSakiKbn_4.ID
                Me.TextSeikyuuSakiMei_4.Value = String.Empty
                masterAjaxSM.SetFocus(SelectSeikyuuSakiKbn_4)
            Case Me.SelectSeikyuuSakiKbn_5.ID
                Me.TextSeikyuuSakiMei_5.Value = String.Empty
                masterAjaxSM.SetFocus(SelectSeikyuuSakiKbn_5)
            Case Me.SelectSeikyuuSakiKbn_6.ID
                Me.TextSeikyuuSakiMei_6.Value = String.Empty
                masterAjaxSM.SetFocus(SelectSeikyuuSakiKbn_6)
            Case Me.SelectSeikyuuSakiKbn_7.ID
                Me.TextSeikyuuSakiMei_7.Value = String.Empty
                masterAjaxSM.SetFocus(SelectSeikyuuSakiKbn_7)
            Case Me.SelectSeikyuuSakiKbn_8.ID
                Me.TextSeikyuuSakiMei_8.Value = String.Empty
                masterAjaxSM.SetFocus(SelectSeikyuuSakiKbn_8)
            Case Me.SelectSeikyuuSakiKbn_9.ID
                Me.TextSeikyuuSakiMei_9.Value = String.Empty
                masterAjaxSM.SetFocus(SelectSeikyuuSakiKbn_9)
            Case Me.SelectSeikyuuSakiKbn_10.ID
                Me.TextSeikyuuSakiMei_10.Value = String.Empty
                masterAjaxSM.SetFocus(SelectSeikyuuSakiKbn_10)
        End Select

    End Sub

    ''' <summary>
    ''' ������R�[�h�ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextSeikyuuSakiCd_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim strTargetId As String = CType(sender, TextBox).ID       '���s���e�L�X�g

        '�{�^�������s���ďo�Ώۂ�؂蕪��
        Select Case strTargetId
            Case Me.TextSeikyuuSakiCd_1.ID
                '������敪�ύX���ɖ��̂��N���A����
                Me.TextSeikyuuSakiMei_1.Value = String.Empty
                masterAjaxSM.SetFocus(TextSeikyuuSakiBrc_1)
            Case Me.TextSeikyuuSakiCd_2.ID
                Me.TextSeikyuuSakiMei_2.Value = String.Empty
                masterAjaxSM.SetFocus(TextSeikyuuSakiBrc_2)
            Case Me.TextSeikyuuSakiCd_3.ID
                Me.TextSeikyuuSakiMei_3.Value = String.Empty
                masterAjaxSM.SetFocus(TextSeikyuuSakiBrc_3)
            Case Me.TextSeikyuuSakiCd_4.ID
                Me.TextSeikyuuSakiMei_4.Value = String.Empty
                masterAjaxSM.SetFocus(TextSeikyuuSakiBrc_4)
            Case Me.TextSeikyuuSakiCd_5.ID
                Me.TextSeikyuuSakiMei_5.Value = String.Empty
                masterAjaxSM.SetFocus(TextSeikyuuSakiBrc_5)
            Case Me.TextSeikyuuSakiCd_6.ID
                Me.TextSeikyuuSakiMei_6.Value = String.Empty
                masterAjaxSM.SetFocus(TextSeikyuuSakiBrc_6)
            Case Me.TextSeikyuuSakiCd_7.ID
                Me.TextSeikyuuSakiMei_7.Value = String.Empty
                masterAjaxSM.SetFocus(TextSeikyuuSakiBrc_7)
            Case Me.TextSeikyuuSakiCd_8.ID
                Me.TextSeikyuuSakiMei_8.Value = String.Empty
                masterAjaxSM.SetFocus(TextSeikyuuSakiBrc_8)
            Case Me.TextSeikyuuSakiCd_9.ID
                Me.TextSeikyuuSakiMei_9.Value = String.Empty
                masterAjaxSM.SetFocus(TextSeikyuuSakiBrc_9)
            Case Me.TextSeikyuuSakiCd_10.ID
                Me.TextSeikyuuSakiMei_10.Value = String.Empty
                masterAjaxSM.SetFocus(TextSeikyuuSakiBrc_10)
        End Select

    End Sub

    ''' <summary>
    ''' ������}�ԕύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextSeikyuuSakiBrc_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim strTargetId As String = CType(sender, TextBox).ID       '���s���e�L�X�g

        '�{�^�������s���ďo�Ώۂ�؂蕪��
        Select Case strTargetId
            Case Me.TextSeikyuuSakiBrc_1.ID
                '������敪�ύX���ɖ��̂��N���A����
                Me.TextSeikyuuSakiMei_1.Value = String.Empty
                masterAjaxSM.SetFocus(btnSeikyuuSakiSearch_1)
            Case Me.TextSeikyuuSakiBrc_2.ID
                Me.TextSeikyuuSakiMei_2.Value = String.Empty
                masterAjaxSM.SetFocus(btnSeikyuuSakiSearch_2)
            Case Me.TextSeikyuuSakiBrc_3.ID
                Me.TextSeikyuuSakiMei_3.Value = String.Empty
                masterAjaxSM.SetFocus(btnSeikyuuSakiSearch_3)
            Case Me.TextSeikyuuSakiBrc_4.ID
                Me.TextSeikyuuSakiMei_4.Value = String.Empty
                masterAjaxSM.SetFocus(btnSeikyuuSakiSearch_4)
            Case Me.TextSeikyuuSakiBrc_5.ID
                Me.TextSeikyuuSakiMei_5.Value = String.Empty
                masterAjaxSM.SetFocus(btnSeikyuuSakiSearch_5)
            Case Me.TextSeikyuuSakiBrc_6.ID
                Me.TextSeikyuuSakiMei_6.Value = String.Empty
                masterAjaxSM.SetFocus(btnSeikyuuSakiSearch_6)
            Case Me.TextSeikyuuSakiBrc_7.ID
                Me.TextSeikyuuSakiMei_7.Value = String.Empty
                masterAjaxSM.SetFocus(btnSeikyuuSakiSearch_7)
            Case Me.TextSeikyuuSakiBrc_8.ID
                Me.TextSeikyuuSakiMei_8.Value = String.Empty
                masterAjaxSM.SetFocus(btnSeikyuuSakiSearch_8)
            Case Me.TextSeikyuuSakiBrc_9.ID
                Me.TextSeikyuuSakiMei_9.Value = String.Empty
                masterAjaxSM.SetFocus(btnSeikyuuSakiSearch_9)
            Case Me.TextSeikyuuSakiBrc_10.ID
                Me.TextSeikyuuSakiMei_10.Value = String.Empty
                masterAjaxSM.SetFocus(btnSeikyuuSakiSearch_10)
        End Select

    End Sub

    ''' <summary>
    ''' ��ʂ��琿��������K�����A���X�g�ɃZ�b�g����
    ''' </summary>
    ''' <param name="listSsi"></param>
    ''' <remarks></remarks>
    Private Sub SetSeikyuuInfo(ByRef listSsi As List(Of SeikyuuSakiInfoRecord))
        Dim recSeikyuu As New SeikyuuSakiInfoRecord

        '���R�[�h�P�ʂŏK�����āA���X�g�ɃZ�b�g
        recSeikyuu = MakeSeikyuuInfoRec(emSsCtrl.intCtrl1)
        If Not recSeikyuu Is Nothing Then
            listSsi.Add(recSeikyuu)
        End If

        recSeikyuu = MakeSeikyuuInfoRec(emSsCtrl.intCtrl2)
        If Not recSeikyuu Is Nothing Then
            listSsi.Add(recSeikyuu)
        End If

        recSeikyuu = MakeSeikyuuInfoRec(emSsCtrl.intCtrl3)
        If Not recSeikyuu Is Nothing Then
            listSsi.Add(recSeikyuu)
        End If

        recSeikyuu = MakeSeikyuuInfoRec(emSsCtrl.intCtrl4)
        If Not recSeikyuu Is Nothing Then
            listSsi.Add(recSeikyuu)
        End If

        recSeikyuu = MakeSeikyuuInfoRec(emSsCtrl.intCtrl5)
        If Not recSeikyuu Is Nothing Then
            listSsi.Add(recSeikyuu)
        End If

        recSeikyuu = MakeSeikyuuInfoRec(emSsCtrl.intCtrl6)
        If Not recSeikyuu Is Nothing Then
            listSsi.Add(recSeikyuu)
        End If

        recSeikyuu = MakeSeikyuuInfoRec(emSsCtrl.intCtrl7)
        If Not recSeikyuu Is Nothing Then
            listSsi.Add(recSeikyuu)
        End If

        recSeikyuu = MakeSeikyuuInfoRec(emSsCtrl.intCtrl8)
        If Not recSeikyuu Is Nothing Then
            listSsi.Add(recSeikyuu)
        End If

        recSeikyuu = MakeSeikyuuInfoRec(emSsCtrl.intCtrl9)
        If Not recSeikyuu Is Nothing Then
            listSsi.Add(recSeikyuu)
        End If

        recSeikyuu = MakeSeikyuuInfoRec(emSsCtrl.intCtrl10)
        If Not recSeikyuu Is Nothing Then
            listSsi.Add(recSeikyuu)
        End If

    End Sub

    ''' <summary>
    ''' ��ʂ���s�P�ʂŐ���������擾����
    ''' </summary>
    ''' <param name="emSsCtrl"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function MakeSeikyuuInfoRec(ByVal emSsCtrl As emSsCtrl) As SeikyuuSakiInfoRecord
        Dim recTemp As New SeikyuuSakiInfoRecord

        Select Case emSsCtrl
            Case SeikyuusyoDataSakusei.emSsCtrl.intCtrl1
                '1�s��
                If String.IsNullOrEmpty(Me.TextSeikyuuSakiCd_1.Text) Or _
                    String.IsNullOrEmpty(Me.TextSeikyuuSakiBrc_1.Text) Or _
                    String.IsNullOrEmpty(Me.SelectSeikyuuSakiKbn_1.SelectedValue) Then
                    '�ǂꂩ��ł������ꍇ�͐ݒ肵�Ȃ�
                    Return Nothing
                Else
                    '������R�[�h
                    cl.SetDisplayString(Me.TextSeikyuuSakiCd_1.Text, recTemp.SeikyuuSakiCd)
                    '������}��
                    cl.SetDisplayString(Me.TextSeikyuuSakiBrc_1.Text, recTemp.SeikyuuSakiBrc)
                    '������敪
                    cl.SetDisplayString(Me.SelectSeikyuuSakiKbn_1.SelectedValue, recTemp.SeikyuuSakiKbn)
                End If
            Case SeikyuusyoDataSakusei.emSsCtrl.intCtrl2
                '2�s��
                If String.IsNullOrEmpty(Me.TextSeikyuuSakiCd_2.Text) Or _
                    String.IsNullOrEmpty(Me.TextSeikyuuSakiBrc_2.Text) Or _
                    String.IsNullOrEmpty(Me.SelectSeikyuuSakiKbn_2.SelectedValue) Then
                    '�ǂꂩ��ł������ꍇ�͐ݒ肵�Ȃ�
                    Return Nothing
                Else
                    '������R�[�h
                    cl.SetDisplayString(Me.TextSeikyuuSakiCd_2.Text, recTemp.SeikyuuSakiCd)
                    '������}��
                    cl.SetDisplayString(Me.TextSeikyuuSakiBrc_2.Text, recTemp.SeikyuuSakiBrc)
                    '������敪
                    cl.SetDisplayString(Me.SelectSeikyuuSakiKbn_2.SelectedValue, recTemp.SeikyuuSakiKbn)
                End If
            Case SeikyuusyoDataSakusei.emSsCtrl.intCtrl3
                '3�s��
                If String.IsNullOrEmpty(Me.TextSeikyuuSakiCd_3.Text) Or _
                    String.IsNullOrEmpty(Me.TextSeikyuuSakiBrc_3.Text) Or _
                    String.IsNullOrEmpty(Me.SelectSeikyuuSakiKbn_3.SelectedValue) Then
                    '�ǂꂩ��ł������ꍇ�͐ݒ肵�Ȃ�
                    Return Nothing
                Else
                    '������R�[�h
                    cl.SetDisplayString(Me.TextSeikyuuSakiCd_3.Text, recTemp.SeikyuuSakiCd)
                    '������}��
                    cl.SetDisplayString(Me.TextSeikyuuSakiBrc_3.Text, recTemp.SeikyuuSakiBrc)
                    '������敪
                    cl.SetDisplayString(Me.SelectSeikyuuSakiKbn_3.SelectedValue, recTemp.SeikyuuSakiKbn)
                End If
            Case SeikyuusyoDataSakusei.emSsCtrl.intCtrl4
                '4�s��
                If String.IsNullOrEmpty(Me.TextSeikyuuSakiCd_4.Text) Or _
                    String.IsNullOrEmpty(Me.TextSeikyuuSakiBrc_4.Text) Or _
                    String.IsNullOrEmpty(Me.SelectSeikyuuSakiKbn_4.SelectedValue) Then
                    '�ǂꂩ��ł������ꍇ�͐ݒ肵�Ȃ�
                    Return Nothing
                Else
                    '������R�[�h
                    cl.SetDisplayString(Me.TextSeikyuuSakiCd_4.Text, recTemp.SeikyuuSakiCd)
                    '������}��
                    cl.SetDisplayString(Me.TextSeikyuuSakiBrc_4.Text, recTemp.SeikyuuSakiBrc)
                    '������敪
                    cl.SetDisplayString(Me.SelectSeikyuuSakiKbn_4.SelectedValue, recTemp.SeikyuuSakiKbn)
                End If
            Case SeikyuusyoDataSakusei.emSsCtrl.intCtrl5
                '5�s��
                If String.IsNullOrEmpty(Me.TextSeikyuuSakiCd_5.Text) Or _
                    String.IsNullOrEmpty(Me.TextSeikyuuSakiBrc_5.Text) Or _
                    String.IsNullOrEmpty(Me.SelectSeikyuuSakiKbn_5.SelectedValue) Then
                    '�ǂꂩ��ł������ꍇ�͐ݒ肵�Ȃ�
                    Return Nothing
                Else
                    '������R�[�h
                    cl.SetDisplayString(Me.TextSeikyuuSakiCd_5.Text, recTemp.SeikyuuSakiCd)
                    '������}��
                    cl.SetDisplayString(Me.TextSeikyuuSakiBrc_5.Text, recTemp.SeikyuuSakiBrc)
                    '������敪
                    cl.SetDisplayString(Me.SelectSeikyuuSakiKbn_5.SelectedValue, recTemp.SeikyuuSakiKbn)
                End If
            Case SeikyuusyoDataSakusei.emSsCtrl.intCtrl6
                '6�s��
                If String.IsNullOrEmpty(Me.TextSeikyuuSakiCd_6.Text) Or _
                    String.IsNullOrEmpty(Me.TextSeikyuuSakiBrc_6.Text) Or _
                    String.IsNullOrEmpty(Me.SelectSeikyuuSakiKbn_6.SelectedValue) Then
                    '�ǂꂩ��ł������ꍇ�͐ݒ肵�Ȃ�
                    Return Nothing
                Else
                    '������R�[�h
                    cl.SetDisplayString(Me.TextSeikyuuSakiCd_6.Text, recTemp.SeikyuuSakiCd)
                    '������}��
                    cl.SetDisplayString(Me.TextSeikyuuSakiBrc_6.Text, recTemp.SeikyuuSakiBrc)
                    '������敪
                    cl.SetDisplayString(Me.SelectSeikyuuSakiKbn_6.SelectedValue, recTemp.SeikyuuSakiKbn)
                End If
            Case SeikyuusyoDataSakusei.emSsCtrl.intCtrl7
                '7�s��
                If String.IsNullOrEmpty(Me.TextSeikyuuSakiCd_7.Text) Or _
                    String.IsNullOrEmpty(Me.TextSeikyuuSakiBrc_7.Text) Or _
                    String.IsNullOrEmpty(Me.SelectSeikyuuSakiKbn_7.SelectedValue) Then
                    '�ǂꂩ��ł������ꍇ�͐ݒ肵�Ȃ�
                    Return Nothing
                Else
                    '������R�[�h
                    cl.SetDisplayString(Me.TextSeikyuuSakiCd_7.Text, recTemp.SeikyuuSakiCd)
                    '������}��
                    cl.SetDisplayString(Me.TextSeikyuuSakiBrc_7.Text, recTemp.SeikyuuSakiBrc)
                    '������敪
                    cl.SetDisplayString(Me.SelectSeikyuuSakiKbn_7.SelectedValue, recTemp.SeikyuuSakiKbn)
                End If
            Case SeikyuusyoDataSakusei.emSsCtrl.intCtrl8
                '8�s��
                If String.IsNullOrEmpty(Me.TextSeikyuuSakiCd_8.Text) Or _
                    String.IsNullOrEmpty(Me.TextSeikyuuSakiBrc_8.Text) Or _
                    String.IsNullOrEmpty(Me.SelectSeikyuuSakiKbn_8.SelectedValue) Then
                    '�ǂꂩ��ł������ꍇ�͐ݒ肵�Ȃ�
                    Return Nothing
                Else
                    '������R�[�h
                    cl.SetDisplayString(Me.TextSeikyuuSakiCd_8.Text, recTemp.SeikyuuSakiCd)
                    '������}��
                    cl.SetDisplayString(Me.TextSeikyuuSakiBrc_8.Text, recTemp.SeikyuuSakiBrc)
                    '������敪
                    cl.SetDisplayString(Me.SelectSeikyuuSakiKbn_8.SelectedValue, recTemp.SeikyuuSakiKbn)
                End If
            Case SeikyuusyoDataSakusei.emSsCtrl.intCtrl9
                '9�s��
                If String.IsNullOrEmpty(Me.TextSeikyuuSakiCd_9.Text) Or _
                    String.IsNullOrEmpty(Me.TextSeikyuuSakiBrc_9.Text) Or _
                    String.IsNullOrEmpty(Me.SelectSeikyuuSakiKbn_9.SelectedValue) Then
                    '�ǂꂩ��ł������ꍇ�͐ݒ肵�Ȃ�
                    Return Nothing
                Else
                    '������R�[�h
                    cl.SetDisplayString(Me.TextSeikyuuSakiCd_9.Text, recTemp.SeikyuuSakiCd)
                    '������}��
                    cl.SetDisplayString(Me.TextSeikyuuSakiBrc_9.Text, recTemp.SeikyuuSakiBrc)
                    '������敪
                    cl.SetDisplayString(Me.SelectSeikyuuSakiKbn_9.SelectedValue, recTemp.SeikyuuSakiKbn)
                End If
            Case SeikyuusyoDataSakusei.emSsCtrl.intCtrl10
                '10�s��
                If String.IsNullOrEmpty(Me.TextSeikyuuSakiCd_10.Text) Or _
                    String.IsNullOrEmpty(Me.TextSeikyuuSakiBrc_10.Text) Or _
                    String.IsNullOrEmpty(Me.SelectSeikyuuSakiKbn_10.SelectedValue) Then
                    '�ǂꂩ��ł������ꍇ�͐ݒ肵�Ȃ�
                    Return Nothing
                Else
                    '������R�[�h
                    cl.SetDisplayString(Me.TextSeikyuuSakiCd_10.Text, recTemp.SeikyuuSakiCd)
                    '������}��
                    cl.SetDisplayString(Me.TextSeikyuuSakiBrc_10.Text, recTemp.SeikyuuSakiBrc)
                    '������敪
                    cl.SetDisplayString(Me.SelectSeikyuuSakiKbn_10.SelectedValue, recTemp.SeikyuuSakiKbn)
                End If
        End Select

        Return recTemp
    End Function

    ''' <summary>
    ''' ��ʃR���g���[��(��������)�̒l���������A�����񉻂���
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getCtrlValuesStringSeikyuu() As String

        Dim sb As New StringBuilder

        sb.Append(Me.TextSeikyuuSakiCd_1.Text & EarthConst.SEP_STRING)                  '������R�[�h�P
        sb.Append(Me.TextSeikyuuSakiBrc_1.Text & EarthConst.SEP_STRING)                 '������}�ԂP
        sb.Append(Me.SelectSeikyuuSakiKbn_1.SelectedIndex & EarthConst.SEP_STRING)      '������敪�P
        sb.Append(Me.TextSeikyuuSakiCd_2.Text & EarthConst.SEP_STRING)
        sb.Append(Me.TextSeikyuuSakiBrc_2.Text & EarthConst.SEP_STRING)
        sb.Append(Me.SelectSeikyuuSakiKbn_2.SelectedIndex & EarthConst.SEP_STRING)
        sb.Append(Me.TextSeikyuuSakiCd_3.Text & EarthConst.SEP_STRING)
        sb.Append(Me.TextSeikyuuSakiBrc_3.Text & EarthConst.SEP_STRING)
        sb.Append(Me.SelectSeikyuuSakiKbn_3.SelectedIndex & EarthConst.SEP_STRING)
        sb.Append(Me.TextSeikyuuSakiCd_4.Text & EarthConst.SEP_STRING)
        sb.Append(Me.TextSeikyuuSakiBrc_4.Text & EarthConst.SEP_STRING)
        sb.Append(Me.SelectSeikyuuSakiKbn_4.SelectedIndex & EarthConst.SEP_STRING)
        sb.Append(Me.TextSeikyuuSakiCd_5.Text & EarthConst.SEP_STRING)
        sb.Append(Me.TextSeikyuuSakiBrc_5.Text & EarthConst.SEP_STRING)
        sb.Append(Me.SelectSeikyuuSakiKbn_5.SelectedIndex & EarthConst.SEP_STRING)
        sb.Append(Me.TextSeikyuuSakiCd_6.Text & EarthConst.SEP_STRING)
        sb.Append(Me.TextSeikyuuSakiBrc_6.Text & EarthConst.SEP_STRING)
        sb.Append(Me.SelectSeikyuuSakiKbn_6.SelectedIndex & EarthConst.SEP_STRING)
        sb.Append(Me.TextSeikyuuSakiCd_7.Text & EarthConst.SEP_STRING)
        sb.Append(Me.TextSeikyuuSakiBrc_7.Text & EarthConst.SEP_STRING)
        sb.Append(Me.SelectSeikyuuSakiKbn_7.SelectedIndex & EarthConst.SEP_STRING)
        sb.Append(Me.TextSeikyuuSakiCd_8.Text & EarthConst.SEP_STRING)
        sb.Append(Me.TextSeikyuuSakiBrc_8.Text & EarthConst.SEP_STRING)
        sb.Append(Me.SelectSeikyuuSakiKbn_8.SelectedIndex & EarthConst.SEP_STRING)
        sb.Append(Me.TextSeikyuuSakiCd_9.Text & EarthConst.SEP_STRING)
        sb.Append(Me.TextSeikyuuSakiBrc_9.Text & EarthConst.SEP_STRING)
        sb.Append(Me.SelectSeikyuuSakiKbn_9.SelectedIndex & EarthConst.SEP_STRING)
        sb.Append(Me.TextSeikyuuSakiCd_10.Text & EarthConst.SEP_STRING)
        sb.Append(Me.TextSeikyuuSakiBrc_10.Text & EarthConst.SEP_STRING)
        sb.Append(Me.SelectSeikyuuSakiKbn_10.SelectedIndex & EarthConst.SEP_STRING)

        Return (sb.ToString)

    End Function

End Class
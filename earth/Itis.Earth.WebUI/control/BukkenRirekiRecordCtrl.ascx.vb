
Partial Public Class BukkenRirekiRecordCtrl
    Inherits System.Web.UI.UserControl

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim iraiSession As New IraiSession
    Dim userinfo As New LoginUserInfo
    Dim masterAjaxSM As New ScriptManager
    Dim cl As New CommonLogic
    ''' <summary>
    ''' ���b�Z�[�W�N���X
    ''' </summary>
    ''' <remarks></remarks>
    Dim MLogic As New MessageLogic

#Region "�R���g���[���l"
    Private Const CTRL_VALUE_BUTTON_SYUUSEI As String = "�C��"
    Private Const CTRL_VALUE_BUTTON_KAKUNIN As String = "�m�F"
    Private Const CTRL_VALUE_SPACE As String = "&nbsp;"
#End Region

#Region "�J�X�^���C�x���g�錾"
    ''' <summary>
    ''' �e��ʂ̏������s�p�J�X�^���C�x���g
    ''' </summary>
    ''' <remarks></remarks>
    Public Event OyaGamenAction()

#End Region

#Region "���[�U�[�R���g���[���ւ̊O������̃A�N�Z�X�pGetter/Setter"

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for HiddenLoginUser
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccStrLoginUser() As String
        Get
            Return Me.HiddenLoginUser.Value
        End Get
        Set(ByVal value As String)
            Me.HiddenLoginUser.Value = value
        End Set
    End Property

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '****************************************************************************
        ' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
        '****************************************************************************
        '�{�^�������C�x���g�̐ݒ�
        Me.setBtnEvent()

    End Sub

    ''' <summary>
    ''' ����{�^���̐ݒ�
    ''' </summary>
    ''' <remarks>
    ''' DB�X�V�̊m�F���s�Ȃ��B<br/>
    ''' OK���FDB�X�V���s�Ȃ��B<br/>
    ''' �L�����Z�����FDB�X�V�𒆒f����B<br/>
    ''' </remarks>
    Private Sub setBtnEvent()

        '�C�x���g�n���h���o�^
        Dim tmpScriptOverLay As String = "setWindowOverlay(this,null,1);"
        Dim tmpScript As String = "if(confirm('" & Messages.MSG130C & "')){" & tmpScriptOverLay & "}else{return false;}"

        '�������MSG�m�F��AOK�̏ꍇDB�X�V�������s�Ȃ�
        ButtonTorikesi.Attributes("onclick") = tmpScript

    End Sub

#Region "�{�^���C�x���g"

    ''' <summary>
    ''' ��� �{�^���������̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonTorikesi_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonTorikesi.ServerClick

        Dim strScript As String = String.Empty
        Dim strFocusScript As String = String.Empty

        Dim objBtn As HtmlInputButton = CType(sender, HtmlInputButton)

        ' ��ʂ̓��e��DB�ɔ��f����
        If Me.SaveData(objBtn) Then '�o�^����

            '�o�^������A��ʂ������[�h���邽�߂ɁA�L�[���������n��
            Context.Items("sendSearchTerms") = Me.HiddenKbn.Value & EarthConst.SEP_STRING & Me.HiddenBangou.Value

            '��ʑJ�ځi�����[�h�j
            Server.Transfer(UrlConst.POPUP_BUKKEN_RIREKI)

        Else '�o�^���s
            strScript = "alert('" & Messages.MSG019E.Replace("@PARAM1", "���") & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonUpdate_ServerClick2", strScript, True)
            Exit Sub
        End If

    End Sub

#End Region

#Region "�v���C�x�[�g���\�b�h"

    ''' <summary>
    ''' ��ʂ̓��e��DB�ɔ��f����
    ''' </summary>
    ''' <remarks></remarks>
    Protected Function SaveData(ByVal objBtn As HtmlInputButton) As Boolean
        '*************************
        '���������f�[�^���X�V����
        '*************************
        Dim logic As New BukkenRirekiLogic
        Dim blnExe As Boolean = False

        '�e�s���Ƃɉ�ʂ��烌�R�[�h�N���X�ɓ��ꍞ��
        Dim dataRec As BukkenRirekiRecord = Nothing

        dataRec = Me.GetRowCtrlToDataRec()
        If dataRec Is Nothing Then
            Return False
        End If

        ' �f�[�^�̍X�V���s���܂�
        '�{�^�������ʏ���
        Select Case objBtn.ID
            Case Me.ButtonTorikesi.ID '���
                'UPDATE
                blnExe = True
            Case Else '����ȊO�̓G���[
                Return False
        End Select

        If logic.saveData(Me, dataRec, blnExe) = False Then
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' �����������R�[�h�����ʕ\�����ڂւ̒l�Z�b�g���s�Ȃ��B
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="br">�����������R�[�h</param>
    ''' <param name="intKengen">����(�L:1�A��:0)</param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromDataRec(ByVal sender As Object _
                                    , ByVal e As System.EventArgs _
                                    , ByVal br As BukkenRirekiRecord _
                                    , Optional ByVal intKengen As Integer = 1 _
                                    )

        Dim helper As New DropDownHelper

        ' ��ʃR���{�Ƀf�[�^���o�C���h����
        helper.SetMeisyouDropDownList(Me.SelectSyubetu, EarthConst.emMeisyouType.BUKKEN_RIREKI_SYUBETU)

        '******************************************
        '* ��ʃR���g���[���ɐݒ�
        '******************************************
        '(����)���
        '���݃`�F�b�N
        If cl.ChkDropDownList(Me.SelectSyubetu, cl.GetDispNum(br.RirekiSyubetu)) Then
            Me.SelectSyubetu.SelectedValue = cl.GetDisplayString(br.RirekiSyubetu, "")
            Me.SpanSyubetu.InnerHtml = Me.SelectSyubetu.SelectedItem.Text

            '�R�[�h(����NO)
            '���݃`�F�b�N
            If cl.ChkDropDownList(Me.SelectSyubetu, cl.GetDispNum(br.RirekiSyubetu)) Then
                Me.HiddenBunrui.Value = cl.GetDisplayString(br.RirekiNo, "")
            End If

        Else
            Me.SpanSyubetu.InnerHtml = CTRL_VALUE_SPACE
            Me.SpanBunrui.InnerHtml = CTRL_VALUE_SPACE
        End If
        '���
        If cl.GetDisplayString(br.Torikesi) = 0 Then
            Me.SpanTorikesi.InnerHtml = CTRL_VALUE_SPACE
        Else
            Me.SpanTorikesi.InnerHtml = EarthConst.TORIKESI
        End If
        '�ύX��
        If cl.GetDisplayString(br.HenkouKahiFlg) = 0 Then
            Me.SpanHenkouFukaFlg.InnerHtml = CTRL_VALUE_SPACE
        Else
            Me.SpanHenkouFukaFlg.InnerHtml = EarthConst.HENKOU_FUKA
        End If
        '(�ėp)���t
        Me.SpanHizuke.InnerHtml = cl.GetDisplayString(br.HanyouDate, CTRL_VALUE_SPACE)
        '�ėp�R�[�h
        Me.SpanHanyouCode.InnerHtml = cl.GetDisplayString(br.HanyouCd, CTRL_VALUE_SPACE)
        '���e
        Me.SpanNaiyou.InnerHtml = cl.GetDisplayString(br.Naiyou, CTRL_VALUE_SPACE)

        '****************************
        '* Hidden����
        '****************************
        '�敪
        Me.HiddenKbn.Value = cl.GetDisplayString(br.Kbn)
        '�ԍ�
        Me.HiddenBangou.Value = cl.GetDisplayString(br.HosyousyoNo)
        '����NO
        Me.HiddenNyuuryokuNo.Value = cl.GetDisplayString(br.NyuuryokuNo)
        '�X�V����
        Me.HiddenUpdDatetime.Value = IIf(br.UpdDatetime = Date.MinValue, "", Format(br.UpdDatetime, EarthConst.FORMAT_DATE_TIME_1))

        '****************************
        '* Javascript
        '****************************
        Me.ButtonSyuusei.Attributes("onclick") = "PopupSyousai('" & Me.HiddenNyuuryokuNo.Value & "')"

        '���׍s�̕\���ݒ�
        Me.SetDispControl(br, intKengen)

    End Sub

    ''' <summary>
    ''' ��ʂ̊e���׍s�������R�[�h�N���X�Ɏ擾���A�n�Ճ��R�[�h�N���X�̃��X�g��ԋp����
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetRowCtrlToDataRec() As BukkenRirekiRecord

        Dim intCnt As Integer = 0
        Dim ctrl As New BukkenRirekiRecordCtrl
        ' ��ʓ��e���n�Ճ��R�[�h�𐶐�����
        Dim dataRec As New BukkenRirekiRecord

        With ctrl

            '***************************************
            ' ���������f�[�^
            '***************************************
            ' �敪
            cl.SetDisplayString(Me.HiddenKbn.Value, dataRec.Kbn)
            ' �ԍ�
            cl.SetDisplayString(Me.HiddenBangou.Value, dataRec.HosyousyoNo)
            ' �������
            cl.SetDisplayString(Me.SelectSyubetu.SelectedValue, dataRec.RirekiSyubetu)
            ' ����NO
            cl.SetDisplayString(Me.HiddenBunrui.Value, dataRec.RirekiNo)
            ' ����NO
            cl.SetDisplayString(Me.HiddenNyuuryokuNo.Value, dataRec.NyuuryokuNo)
            ' ���e
            cl.SetDisplayString(Me.SpanNaiyou.InnerHtml, dataRec.Naiyou)
            ' (�ėp)���t
            cl.SetDisplayString(Me.SpanHizuke.InnerHtml, dataRec.HanyouDate)
            ' �ėp�R�[�h
            cl.SetDisplayString(Me.SpanHanyouCode.InnerHtml, dataRec.HanyouCd)
            ' �X�V�҃��[�U�[ID
            dataRec.UpdLoginUserId = Me.HiddenLoginUser.Value
            ' �X�V���� �ǂݍ��ݎ��̃^�C���X�^���v
            If Me.HiddenUpdDatetime.Value = "" Then
                dataRec.UpdDatetime = System.Data.SqlTypes.SqlDateTime.Null
            Else
                dataRec.UpdDatetime = DateTime.ParseExact(Me.HiddenUpdDatetime.Value, EarthConst.FORMAT_DATE_TIME_1, Nothing)
            End If

        End With

        Return dataRec
    End Function

#Region "��ʐ���"

    ''' <summary>
    ''' ���׍s�̕\���ݒ���s���B
    ''' </summary>
    ''' <param name="dataRec" >�����������R�[�h</param>
    ''' <param name="intKengen" >����(�L:1�A��:0)</param>
    ''' <remarks>�T�[�o�[���ł͎���s�͏�ɔ�\���B�\���ؑւ�JS�ɂĐ���</remarks>
    Public Sub SetDispControl(ByVal dataRec As BukkenRirekiRecord, ByVal intKengen As Integer)

        '����s�̏ꍇ�A��\��
        If Me.SpanTorikesi.InnerHtml = EarthConst.TORIKESI Then
            Me.Tr1.Style("display") = "none"
            Me.Tr2.Style("display") = "none"
        Else
            Me.Tr1.Style("display") = "inline"
            Me.Tr2.Style("display") = "inline"
        End If

        If intKengen = 0 Then '�����Ȃ���
            Me.ButtonSyuusei.Value = CTRL_VALUE_BUTTON_KAKUNIN '�m�F
            Me.ButtonTorikesi.Disabled = True '���

        Else
            '�o�^���t
            Dim strTmpAddDate As String = IIf(dataRec.AddDatetime = Date.MinValue, String.Empty, Format(dataRec.AddDatetime, EarthConst.FORMAT_DATE_TIME_3))
            '�V�X�e�����t
            Dim strNowDate As String = Format(Now.Date, EarthConst.FORMAT_DATE_TIME_3)

            '�V�K�o�^��
            If strTmpAddDate = String.Empty Then
                Me.ButtonSyuusei.Value = CTRL_VALUE_BUTTON_KAKUNIN '�m�F
                Me.ButtonTorikesi.Disabled = True '���

            ElseIf strTmpAddDate = strNowDate Then '�X�V���F�o�^���t=�V�X�e�����t

                '���or�ύX�s��
                If Me.SpanTorikesi.InnerHtml = EarthConst.TORIKESI Or Me.SpanHenkouFukaFlg.InnerHtml = EarthConst.HENKOU_FUKA Then
                    Me.ButtonSyuusei.Value = CTRL_VALUE_BUTTON_KAKUNIN '�m�F
                    Me.ButtonTorikesi.Disabled = True '���

                Else
                    Me.ButtonSyuusei.Value = CTRL_VALUE_BUTTON_SYUUSEI '�C��
                    Me.ButtonTorikesi.Disabled = False '���

                End If

            Else '�o�^���t<>�V�X�e�����t
                Me.ButtonSyuusei.Value = CTRL_VALUE_BUTTON_KAKUNIN '�m�F
                Me.ButtonTorikesi.Disabled = True '���

            End If

        End If

    End Sub

#End Region

#End Region

End Class
Partial Public Class TokubetuTaiouToolTipCtrl
    Inherits System.Web.UI.UserControl

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Private cl As New CommonLogic
    '���ʑΉ��}�X�^���W�b�N
    Dim mttLogic As New TokubetuTaiouMstLogic

#Region "�v���p�e�B"
    ''' <summary>
    ''' ���ʑΉ��R�[�h(�\���p)Hidden�ւ̊O���A�N�Z�X�p�v���p�e�B
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccDisplayCd() As HtmlInputHidden
        Get
            Return hiddenDisplay
        End Get
        Set(ByVal value As HtmlInputHidden)
            hiddenDisplay = value
        End Set
    End Property

    ''' <summary>
    ''' ���ʑΉ��R�[�h(�X�V�Ώ�)Hidden�ւ̊O���A�N�Z�X�p�v���p�e�B
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTaisyouCd() As HtmlInputHidden
        Get
            Return hiddenTaisyou
        End Get
        Set(ByVal value As HtmlInputHidden)
            hiddenTaisyou = value
        End Set
    End Property

    ''' <summary>
    ''' �X�V����Hidden�ւ̊O���A�N�Z�X�p�v���p�e�B
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccUpdDatetime() As HtmlInputHidden
        Get
            Return hiddenUpdDatetime
        End Get
        Set(ByVal value As HtmlInputHidden)
            hiddenUpdDatetime = value
        End Set
    End Property

    ''' <summary>
    ''' �\���ؑփt���OHidden�ւ̊O���A�N�Z�X�p�v���p�e�B
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccVisibleFlg() As HtmlInputHidden
        Get
            Return hiddenVisibleFlg
        End Get
        Set(ByVal value As HtmlInputHidden)
            hiddenVisibleFlg = value
        End Set
    End Property

    ''' <summary>
    ''' �\��Label�ւ̊O���A�N�Z�X�p�v���p�e�B
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AcclblTokubetuTaiou() As Label
        Get
            Return lblTokubetuTaiou
        End Get
        Set(ByVal value As Label)
            lblTokubetuTaiou = value
        End Set
    End Property
#End Region

    ''' <summary>
    ''' �y�[�W�`��O����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

        '���ʑΉ����x���̃X�^�C���ݒ�
        SetLabelStyle()

        '�c�[���`�b�v�̐ݒ�
        Dim strToolTip As String = GetToolTip()
        'Html�R���g���[���Ƀc�[���`�b�v��ݒ肷��
        cl.SetToolTipForCtrl(Me.spanTokubetuTaiou, strToolTip)

    End Sub

#Region "�v���C�x�[�g���\�b�h"
    ''' <summary>
    ''' ���ʑΉ����x���X�^�C���̐ݒ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetLabelStyle()
        '���ʑΉ��R�[�h(�\���p)
        Dim strTokubetuTaiou As String = Me.hiddenDisplay.Value

        '�\���ؑփt���O
        If Me.AccVisibleFlg.Value = String.Empty Then

            '�\���p�R�[�h
            If strTokubetuTaiou <> String.Empty Then
                '�Ԏ�����
                Me.lblTokubetuTaiou.Style(EarthConst.STYLE_FONT_COLOR) = EarthConst.STYLE_COLOR_RED
                Me.lblTokubetuTaiou.Style(EarthConst.STYLE_FONT_WEIGHT) = EarthConst.STYLE_WEIGHT_BOLD

                '�\��
                Me.lblTokubetuTaiou.Visible = True
            Else
                '��\��
                Me.lblTokubetuTaiou.Visible = False
            End If
        Else
            '��\��
            Me.lblTokubetuTaiou.Visible = False
        End If

    End Sub

    ''' <summary>
    ''' ���ʑΉ��̃c�[���`�b�v�ݒ�
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetToolTip() As String

        Dim arrTokubetuTaiouCd() As String = Nothing
        Dim strTokubetuTaiou As String = String.Empty
        Dim intCnt As Integer = 0
        Dim dtRec As New TokubetuTaiouMstRecord
        Dim listMtt As New List(Of TokubetuTaiouMstRecord)
        Dim listResult As New List(Of TokubetuTaiouMstRecord)

        If Me.hiddenDisplay.Value <> String.Empty Then
            arrTokubetuTaiouCd = Split(Me.hiddenDisplay.Value, EarthConst.SEP_STRING)
        End If

        If Not arrTokubetuTaiouCd Is Nothing Then
            For intCnt = 0 To arrTokubetuTaiouCd.Length - 1
                '***************************************
                ' ���ʑΉ��}�X�^
                '***************************************
                dtRec = New TokubetuTaiouMstRecord

                '���ʑΉ��R�[�h
                dtRec.TokubetuTaiouCd = arrTokubetuTaiouCd(intCnt)

                listMtt.Add(dtRec)
            Next
            listResult = mttLogic.GetTokubetuTaiouToolTip(Me, listMtt)

        End If

        '�擾�������ʑΉ����̂��c�[���`�b�v�p�ɕҏW
        If Not listResult Is Nothing AndAlso listResult.Count <> 0 Then
            For i As Integer = 0 To listResult.Count - 1
                If i > 0 Then
                    strTokubetuTaiou += "" & vbCrLf
                End If
                strTokubetuTaiou += listResult(i).TokubetuTaiouMeisyou
            Next
        End If

        Return strTokubetuTaiou
    End Function

    ''' <summary>
    ''' �\���p��Hidden�ɓ��ʑΉ��R�[�h��ݒ�
    ''' </summary>
    ''' <param name="strTokubetuTaiouCd">���ʑΉ��R�[�h</param>
    ''' <remarks></remarks>
    Public Sub SetDisplayCd(ByVal strTokubetuTaiouCd As String)
        '���ʑΉ��R�[�h
        If Me.hiddenDisplay.Value = String.Empty Then
            Me.hiddenDisplay.Value = strTokubetuTaiouCd
        Else
            Me.hiddenDisplay.Value &= EarthConst.SEP_STRING & strTokubetuTaiouCd
        End If
    End Sub

#End Region

End Class
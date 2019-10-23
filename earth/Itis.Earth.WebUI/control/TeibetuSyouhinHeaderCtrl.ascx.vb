
Partial Public Class TeibetuSyouhinHeaderCtrl
    Inherits System.Web.UI.UserControl

#Region "��"
    ''' <summary>
    ''' �R���g���[���̕\�����[�h
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum DisplayMode
        ''' <summary>
        ''' ���i�P
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN1 = 1
        ''' <summary>
        ''' ���i�Q
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN2 = 2
        ''' <summary>
        ''' ���i�R
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN3 = 3
        ''' <summary>
        ''' �񍐏�
        ''' </summary>
        ''' <remarks></remarks>
        HOUKOKUSYO = 4
        ''' <summary>
        ''' �ۏ�
        ''' </summary>
        ''' <remarks></remarks>
        HOSYOU = 5
        ''' <summary>
        ''' �ۏ�(��񕥖�)
        ''' </summary>
        ''' <remarks></remarks>
        KAIYAKU = 6
    End Enum
#End Region

#Region "�v���p�e�B"
    ''' <summary>
    ''' �R���g���[���̕\�����[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private mode As DisplayMode
    ''' <summary>
    ''' �R���g���[���̕\�����[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns>�R���g���[���̕\�����[�h</returns>
    ''' <remarks>���i�̎�ނɂ���ʂ̕\����ύX���܂�</remarks>
    Public Property DispMode() As DisplayMode
        Get
            Return mode
        End Get
        Set(ByVal value As DisplayMode)
            mode = value
        End Set
    End Property
#End Region

#Region "�C�x���g"
    ''' <summary>
    ''' �y�[�W���[�h���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            ' ��ʕ\���ݒ�
            Select Case Me.DispMode
                Case DisplayMode.SYOUHIN1
                    ' ���i�P�̏ꍇ
                    TdSyouhinCdTitle.InnerText = TdSyouhinCdTitle.InnerText & "1"
                Case DisplayMode.SYOUHIN2
                    ' ���i�Q�̏ꍇ
                    TdSyouhinCdTitle.InnerText = TdSyouhinCdTitle.InnerText & "2"
                Case DisplayMode.SYOUHIN3
                    ' ���i�R�̏ꍇ
                    TdSyouhinCdTitle.InnerText = TdSyouhinCdTitle.InnerText & "3"
                Case DisplayMode.HOUKOKUSYO, DisplayMode.HOSYOU
                    ' �񍐏�,�ۏ؂̏ꍇ
                    ' ���������z�A�d������Ŋz�A�`�[�d���N�������\��()
                    TdSyoudakusyoTitle.Style("display") = "none"
                    TdSiireSyouhizeiGakuTitle.Style("display") = "none"
                    TdDenpyouSiireNengappiTitleDisplay.Style("display") = "none"
                    TdDenpyouSiireNengappiTitle.Style("display") = "none"
                    TdSpacer.Style("display") = "inline"                    ' �E�[�X�y�[�T�[

                Case DisplayMode.KAIYAKU
                    ' �ۏ�(��񕥖�)�̏ꍇ
                    TdSyouhinCdTitle.Attributes("rowspan") = 2
                    TdSyouhinNmTitle.Style("display") = "none"
                    SpanSeikyuuUmu.InnerHtml = EarthConst.KAIYAKU_UMU
                    SpanSeikyuuUmu.Style("font-size") = "10px;"

                    ' ���������z�A�d������Ŋz�A�`�[�d���N�������\��()
                    TdSyoudakusyoTitle.Style("display") = "none"
                    TdSiireSyouhizeiGakuTitle.Style("display") = "none"
                    TdDenpyouSiireNengappiTitleDisplay.Style("display") = "none"
                    TdDenpyouSiireNengappiTitle.Style("display") = "none"

                    TdSpacer.Style("display") = "inline"                    ' �E�[�X�y�[�T�[

                Case Else
            End Select
        End If
    End Sub
#End Region

End Class
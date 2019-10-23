Public Partial Class NyuukinZangakuCtrl
    Inherits System.Web.UI.UserControl

#Region "�v���p�e�B"
    ''' <summary>
    ''' �����z�^�C�g������
    ''' </summary>
    ''' <remarks>true:�����z false:�ԋ��z</remarks>
    Private _isNyuukingaku As Boolean = True
    ''' <summary>
    ''' �����z�^�C�g������
    ''' </summary>
    ''' <value></value>
    ''' <returns>true:�����z false:�ԋ��z</returns>
    ''' <remarks></remarks>
    Public Property isNyuukingaku() As Boolean
        Get
            Return _isNyuukingaku
        End Get
        Set(ByVal value As Boolean)
            _isNyuukingaku = value
        End Set
    End Property

    ''' <summary>
    ''' �����z(�ԋ��z)�R���g���[���ւ̃A�N�Z�T
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NyuukinGaku() As String
        Get
            Return TextNyuukinGaku.Value
        End Get
        Set(ByVal value As String)
            TextNyuukinGaku.Value = value
        End Set
    End Property

    ''' <summary>
    ''' �c�z�R���g���[���ւ̃A�N�Z�T
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ZanGaku() As String
        Get
            Return TextZanGaku.Value
        End Get
        Set(ByVal value As String)
            TextZanGaku.Value = value
        End Set
    End Property

    ''' <summary>
    ''' UpdatePanel
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UpdateZangakuPanel() As UpdatePanel
        Get
            Return UpdatePanelZangaku
        End Get
        Set(ByVal value As UpdatePanel)
            UpdatePanelZangaku = value
        End Set
    End Property


#End Region

    ''' <summary>
    ''' �y�[�W���[�h���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            ' �����z�^�C�g���̐ݒ�
            If _isNyuukingaku = True Then
                ' �����z
                SpanNyuukinTitle.InnerText = Earth.Utilities.EarthConst.NYUUKINGAKU_ZEIKOMI
            Else
                ' �ԋ��z
                SpanNyuukinTitle.InnerText = Earth.Utilities.EarthConst.HENKINGAKU_ZEIKOMI
            End If
        End If
    End Sub

    ''' <summary>
    ''' �����z�E�c�z��\�����܂�
    ''' </summary>
    ''' <param name="uriageGoukeiGaku">�ō�������z���v</param>
    ''' <param name="nyuukinGaku">�����z</param>
    ''' <remarks></remarks>
    Public Sub CalcZangaku(ByVal uriageGoukeiGaku As Integer, _
                           ByVal nyuukinGaku As Integer)

        ' NULL�͂O�ɂ���
        uriageGoukeiGaku = IIf(uriageGoukeiGaku = Integer.MinValue, 0, uriageGoukeiGaku)
        nyuukinGaku = IIf(nyuukinGaku = Integer.MinValue, 0, nyuukinGaku)

        ' �����z
        TextNyuukinGaku.Value = nyuukinGaku.ToString(EarthConst.FORMAT_KINGAKU_1)

        ' �c�z
        TextZanGaku.Value = (uriageGoukeiGaku - nyuukinGaku).ToString(EarthConst.FORMAT_KINGAKU_1)

    End Sub

    ''' <summary>
    ''' �����z�E�c�z��\�����܂�<br/>
    ''' �ō�������z���v�̂ݕύX���A�Čv�Z���܂�
    ''' </summary>
    ''' <param name="uriageGoukeiGaku">�ō�������z���v</param>
    ''' <remarks></remarks>
    Public Sub CalcZangaku(ByVal uriageGoukeiGaku As Integer)

        Dim strNyuukinGaku As String = IIf(TextNyuukinGaku.Value.Replace(",", "").Trim() = "", _
                                        "0", _
                                        TextNyuukinGaku.Value.Replace(",", "").Trim())

        Dim nyuukinGaku As Integer = Integer.Parse(strNyuukinGaku)

        ' NULL�͂O�ɂ���
        uriageGoukeiGaku = IIf(uriageGoukeiGaku = Integer.MinValue, 0, uriageGoukeiGaku)

        ' �c�z
        TextZanGaku.Value = (uriageGoukeiGaku - nyuukinGaku).ToString(EarthConst.FORMAT_KINGAKU_1)

    End Sub


End Class
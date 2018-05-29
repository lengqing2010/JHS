''' <summary>
''' EARTHで使用する定数を管理するクラスです
''' 使用する場合は EarthConst.XXXX と指定(配列等はEarthConst.Instance.XXXX)
''' </summary>
''' <remarks>
''' </remarks>
Public Class CommonConstBC

    ''' <summary>
    ''' JavaScript連携用区切り文字列
    ''' </summary>
    ''' <remarks></remarks>
    Public Const sepStr As String = "$$$"

    ''' <summary>
    ''' 日付登録可能最小値
    ''' </summary>
    ''' <remarks></remarks>
    Public dateMin As Date = CType(System.Data.SqlTypes.SqlDateTime.MinValue, Date)
    ''' <summary>
    ''' 日付登録可能最大値
    ''' </summary>
    ''' <remarks></remarks>
    Public dateMax As Date = CType(System.Data.SqlTypes.SqlDateTime.MaxValue, Date)
    ''' <summary>
    ''' 使用禁止文字列配列
    ''' </summary>
    ''' <remarks></remarks>
    Public arrayKinsiStr() As String = New String() {vbCrLf, vbTab, """", ",", "'", "<", ">", "&", sepStr}


    Public Const main As String = "MainMenu.aspx"
    Public Const eigyouKeikakuKanri As String = "EigyouKeikakuKanriMenu.aspx"
    Public Const uriageYojituKanri As String = "UriageYojituKanriMenu.aspx"
    Public Const keikakuKanriKameitenKensakuSyoukai As String = "KeikakuKanriKameitenKensakuSyoukaiInquiry.aspx"

    '静的変数としてクラス型のインスタンスを生成
    Private Shared _instance As CommonConstBC = New CommonConstBC()

    '静的関数としてクラス型のインスタンスを返す関数を用意
    Public Shared ReadOnly Property Instance() As CommonConstBC
        Get
            '静的変数が解放されていた場合のみ、インスタンスを生成する
            If IsDBNull(_instance) Then
                _instance = New CommonConstBC()
            End If
            Return _instance
        End Get
    End Property

    'コンストラクタ（非公開：外部からのアクセスは不可）
    Private Sub New()
        ' 必要に応じて実装
    End Sub

End Class

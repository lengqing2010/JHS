''' <summary>
''' EARTH クラスのテンプレートです
''' EMAB 実装忘れ防止用
''' </summary>
''' <remarks></remarks>
Public Class EARTH_ClassTemplate1

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ' @@ 以下を引数を持つメソッドの始めに記述 @@
    'メソッド名、引数の情報の退避
    ' UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".[メソッド名]", _
    '                                                [引数1], _
    '                                                [引数2], _
    '                                                [引数2])


End Class

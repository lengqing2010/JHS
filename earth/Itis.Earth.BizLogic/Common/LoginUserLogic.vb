''' <summary>
''' ログインユーザー情報の取得クラスです
''' </summary>
''' <remarks></remarks>
Public Class LoginUserLogic

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' ログインユーザーIDよりEARTHのアカウント情報を取得します<br/>
    ''' アカウント情報の取得に失敗した場合、falseを返却します<br/>
    ''' </summary>
    ''' <param name="loginUserId">ログインユーザーID</param>
    ''' <param name="userInfo">EARTHのユーザー情報を保持するレコード</param>
    ''' <returns>取得結果 true:成功 false:失敗</returns>
    ''' <remarks>ユーザー情報の取得に失敗した場合、EARTH全機能の使用不可</remarks>
    Public Function MakeUserInfo(ByVal loginUserId As String, _
                                 ByRef userInfo As LoginUserInfo) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".MakeUserInfo", _
                                            loginUserId, _
                                            userInfo)

        ' アカウントマスタ検索用
        Dim dataAccess As New AccountDataAccess
        Dim dataTable As New AccountDataSet.AccountTableDataTable

        dataTable = dataAccess.GetAccountData(loginUserId)

        ' 指定したレコードクラスに結果を格納したList(Of LoginUserInfo)を取得します
        Dim list As List(Of LoginUserInfo) = DataMappingHelper.Instance.getMapArray(Of LoginUserInfo)(userInfo.GetType(), dataTable)

        ' 複数件あっても１件目を返す（運用ルール上1:1）
        If list.Count > 0 Then
            userInfo = list(0)
        Else
            ' 設定に失敗した場合、Falseを返す
            Return False
        End If

        Return True

    End Function


End Class

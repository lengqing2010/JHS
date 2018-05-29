Imports Itis.Earth.DataAccess

''' <summary>
''' ログインユーザー情報の取得クラスです
''' </summary>
''' <remarks></remarks>
Public Class LoginUserLogic

    ''' <summary>
    ''' ログインユーザーIDよりEARTHのアカウント情報を取得します<br/>
    ''' アカウント情報の取得に失敗した場合、falseを返却します<br/>
    ''' </summary>
    ''' <param name="login_user_id">ログインユーザーID</param>
    ''' <param name="user_info">EARTHのユーザー情報を保持するレコード</param>
    ''' <returns>取得結果 true:成功 false:失敗</returns>
    ''' <remarks>ユーザー情報の取得に失敗した場合、EARTH全機能の使用不可</remarks>
    Public Function makeUserInfo(ByVal login_user_id As String, _
                                 ByRef user_info As LoginUserInfo) As Boolean

        ' アカウントマスタ検索用
        Dim data_access As New AccountDataAccess
        Dim data_table As New AccountDataSet.AccountTableDataTable

        data_table = data_access.getAccountData(login_user_id)

        ' 指定したレコードクラスに結果を格納したArrayListを取得します
        Dim list As ArrayList = DataMappingHelper.Instance.getMapArray(user_info.GetType(), data_table)

        ' 複数件あっても１件目を返す（運用ルール上1:1）
        If list.Count > 0 Then
            user_info = list(0)
        Else
            ' 設定に失敗した場合、Falseを返す
            Return False
        End If

        Return True

    End Function


End Class

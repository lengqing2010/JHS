Imports Itis.Earth.BizLogic

''' <summary>
''' 地盤画面共通クラス
''' </summary>
''' <remarks>画面表示に関連する共通処理を管理</remarks>
Public Class Jiban

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()

        '必要に応じて実装

        'MsgBox("コンストラクタ")

    End Sub

    ''' <summary>
    ''' ユーザー認証
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub userAuth(ByRef user_info As LoginUserInfo)

        ' ユーザー認証
        Dim ninsyou As New Ninsyou()
        If (Not ninsyou.IsUserLogon()) Then
            '認証失敗

            ninsyou.EndResponseWithAccessDeny()
        End If

        'ユーザー権限によりリンク状態を切り替え

        Dim login_logic As New LoginUserLogic

        If login_logic.makeUserInfo(ninsyou.GetUserID(), user_info) Then

        Else
            ' ユーザーアカウント情報取得不可の場合Nothingをセット
            user_info = Nothing
            Debug.WriteLine("ログインユーザー情報の取得に失敗しました")
        End If

    End Sub


    ''' <summary>
    ''' バイト数チェック(SJIS)
    ''' </summary>
    ''' <param name="target">チェックする文字列</param>
    ''' <param name="max">最大OKバイト数</param>
    ''' <returns>true:チェックOK / false:チェックNG</returns>
    ''' <remarks></remarks>
    Public Function byteCheckSJIS(ByVal target As String, ByVal max As Integer) As Boolean

        Dim jL As New JibanLogic

        Return jL.getStrByteSJIS(target) <= max

    End Function

    ''' <summary>
    ''' 禁則文字チェック
    ''' </summary>
    ''' <param name="target">チェックする文字列</param>
    ''' <returns>true:チェックOK / false:チェックNG</returns>
    ''' <remarks></remarks>
    Public Function kinsiStrCheck(ByVal target As String) As Boolean

        For Each st As String In EarthConst.Instance.arrayKinsiStr
            If target.IndexOf(st) >= 0 Then
                Return False
            End If
        Next

        Return True

    End Function

End Class

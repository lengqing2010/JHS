Imports Itis.Earth.DataAccess
Imports Itis.Earth.BizLogic
Public Class KakakusetteiLogic
    Private KakakusetteiDA As New KakakusetteiDataAccess

    '============================2011/04/26 車龍 修正 開始↓=====================================
    'Public Function SelKakakuInfo(ByVal strKbn As String, ByVal strHouhou As String, ByVal strGaiyou As String) As DataTable
    '    Return KakakusetteiDA.SelKakakuInfo(strKbn, strHouhou, strGaiyou)
    'End Function
    ''' <summary>
    ''' 商品価格設定情報を取得する
    ''' </summary>
    ''' <param name="strKbn">商品区分</param>
    ''' <param name="strHouhou">調査方法</param>
    ''' <param name="strGaiyou">調査概要</param>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <returns>商品価格設定情報テーブル</returns>
    ''' <remarks>商品価格設定情報を取得する</remarks>
    ''' <history>2011/04/26　車龍(大連情報システム部)　新規作成</history>
    Public Function SelKakakuInfo(ByVal strKbn As String, ByVal strHouhou As String, ByVal strGaiyou As String, Optional ByVal strSyouhinCd As String = "") As DataTable
        Return KakakusetteiDA.SelKakakuInfo(strKbn, strHouhou, strGaiyou, strSyouhinCd)
    End Function
    '============================2011/04/26 車龍 修正 終了↑=====================================

    Public Function SelKakutyouInfo(ByVal strSyubetu As String, Optional ByVal strTable As String = "") As DataTable
        Return KakakusetteiDA.SelKakutyouInfo(strSyubetu, strTable)
    End Function
    Public Function SelHaita(ByVal strKbn As String, ByVal strHouhou As String, ByVal strGaiyou As String, ByVal strKousin As String) As DataTable

        Return KakakusetteiDA.SelHaita(strKbn, strHouhou, strGaiyou, strKousin)
    End Function

    Public Function SelJyuufuku(ByVal strKbn As String, ByVal strHouhou As String, ByVal strGaiyou As String) As Boolean

        If KakakusetteiDA.SelJyuufuku(strKbn, strHouhou, strGaiyou).Rows.Count = 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function InsKakakusettei(ByVal strKbn As String, _
                                 ByVal strHouhou As String, _
                                 ByVal strGaiyou As String, _
                                 ByVal strSyouhinCd As String, _
                                 ByVal strSetteiBasyo As String, _
                                 ByVal strUserId As String) As Integer

        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try
                KakakusetteiDA.InsKakakusettei(strKbn, _
                                    strHouhou, _
                                    strGaiyou, _
                                    strSyouhinCd, _
                                    strSetteiBasyo, _
                                    strUserId)
                scope.Complete()
                Return True
            Catch ex As Exception
                scope.Dispose()
                Return False
            End Try

        End Using

    End Function

    Public Function UpdKakakusettei(ByVal strKbn As String, _
                                 ByVal strHouhou As String, _
                                 ByVal strGaiyou As String, _
                                 ByVal strSyouhinCd As String, _
                                 ByVal strSetteiBasyo As String, _
                                 ByVal strUserId As String) As Integer

        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try
                If KakakusetteiDA.SelKakakuInfo(strKbn, strHouhou, strGaiyou).Rows.Count = 0 Then
                    scope.Dispose()
                    Return 2
                Else

                    KakakusetteiDA.UpdKakakusettei(strKbn, _
                                    strHouhou, _
                                    strGaiyou, _
                                    strSyouhinCd, _
                                    strSetteiBasyo, _
                                    strUserId)
                    scope.Complete()
                    Return 1
                End If

            Catch ex As Exception
                scope.Dispose()
                Return 0
            End Try

        End Using

    End Function

    Public Function DelKakakusettei(ByVal strKbn As String, ByVal strHouhou As String, ByVal strGaiyou As String) As Boolean

        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try

                KakakusetteiDA.DelKakakusettei(strKbn, strHouhou, strGaiyou)
                scope.Complete()
                Return True

            Catch ex As Exception
                scope.Dispose()
                Return False
            End Try

        End Using

    End Function

    '========================2011/04/26 車龍 追加 開始↓============================
    ''' <summary>
    ''' 重複チェック(商品区分、調査方法、商品ｺｰﾄﾞ)
    ''' </summary>
    ''' <param name="strKbn">商品区分</param>
    ''' <param name="strHouhou">調査方法</param>
    ''' <param name="strSyouhin">商品ｺｰﾄﾞ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2011/04/26　車龍(大連情報システム部)　新規作成</history>
    Public Function CheckJyuufukuSyouhin(ByVal strKbn As String, ByVal strHouhou As String, ByVal strSyouhin As String) As Boolean

        If KakakusetteiDA.SelJyuufukuSyouhin(strKbn, strHouhou, strSyouhin).Rows.Count = 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    '========================2011/04/26 車龍 追加 終了↑============================
End Class

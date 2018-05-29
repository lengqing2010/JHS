Imports Itis.Earth.DataAccess
Imports System.Transactions
Public Class KyoutuuJyouhouLogic
    ''' <summary> メインメニュークラスのインスタンス生成 </summary>
    Private KyoutuuJyouhouDataSet As New KyoutuuJyouhouDataAccess
    ''' <summary>加盟店マスト情報を取得する</summary>
    Public Function GetKameitenInfo(ByVal strKameitenCd As String) As KyoutuuJyouhouDataSet.KyoutuuJyouhouTableDataTable
        Return KyoutuuJyouhouDataSet.SelKyoutuuJyouhouInfo(strKameitenCd)
    End Function
    ''' <summary>加盟店マスト情報を更新する</summary>
    Public Function SetUpdKyoutuuJyouhouInfo(ByVal dtKyoutuuJyouhouData As KyoutuuJyouhouDataSet.KyoutuuJyouhouTableDataTable) As Boolean
        Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required)
            KyoutuuJyouhouDataSet.UpdKyoutuuJyouhouInfo(dtKyoutuuJyouhouData)
            KyoutuuJyouhouDataSet.UpdKyoutuuJyouhouRenkei(dtKyoutuuJyouhouData.Rows(0).Item("kameiten_cd"), dtKyoutuuJyouhouData.Rows(0).Item("simei"))
            scope.Complete()
            Return True
        End Using
    End Function
    Public Function SelKensuu(ByVal strKameitenCd As String, ByVal strKbn As String, ByVal strNengetu As String) As DataTable

        Return KyoutuuJyouhouDataSet.SelKensuu(strKameitenCd, strKbn, strNengetu)

    End Function
    Public Function SelHosyousyo(ByVal strKameitenCd As String, ByVal strNengetu As String, ByVal strFlg As String) As DataTable
        Return KyoutuuJyouhouDataSet.SelHosyousyo(strKameitenCd, strNengetu, strFlg)
    End Function

    Public Function SetUPDJiban(ByVal strParam As String, ByVal strUserId As String) As Boolean
        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try
                For i As Integer = 0 To Split(strParam, ",").Length - 1
                    Dim strkbn As String = Left(Split(Split(strParam, ",")(i), "|")(0), 1)
                    Dim strNo As String = Right(Split(Split(strParam, ",")(i), "|")(0), 10)
                    Dim strFlg As String = Split(Split(strParam, ",")(i), "|")(1)
                    KyoutuuJyouhouDataSet.UpdJiban(strkbn, strNo, strFlg, strUserId)
                    KyoutuuJyouhouDataSet.UpdJibanRenkei(strkbn, strNo, strUserId)
                Next

                scope.Complete()
                Return True
            Catch ex As Exception
                scope.Dispose()
                Return False
            End Try

        End Using
    End Function

    ''' <summary>
    ''' 「取消」ddlのデータを取得する
    ''' </summary>
    ''' <returns>「取消」ddlのデータテーブル</returns>
    ''' <remarks></remarks>
    ''' <history>2012/04/13 車龍 405738案件の対応 追加</history>
    Public Function GetTorikesiList(Optional ByVal strCd As String = "") As Data.DataTable

        '戻り値
        Return KyoutuuJyouhouDataSet.SelTorikesiList(strCd)

    End Function

    ''' <summary>
    ''' FC調査会社を取得する
    ''' </summary>
    ''' <returns>「取消」ddlのデータテーブル</returns>
    ''' <remarks></remarks>
    ''' <history>2012/03/27 車龍 405721案件の対応 追加</history>
    Public Function GetFcTyousaKaisya(ByVal strEigyousyoCd As String) As Data.DataTable

        '戻り値
        Return KyoutuuJyouhouDataSet.SelFcTyousaKaisya(strEigyousyoCd)

    End Function

End Class

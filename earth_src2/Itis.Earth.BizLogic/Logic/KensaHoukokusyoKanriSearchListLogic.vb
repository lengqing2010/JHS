Imports System.Transactions
Imports Itis.Earth.DataAccess

''' <summary>検査報告書管理</summary>
''' <remarks>検査報告書管理用機能を提供する</remarks>
''' <history>
''' <para>2015/12/04　高兵兵(大連情報システム部)　新規作成</para>
''' </history>
Public Class KensaHoukokusyoKanriSearchListLogic

    Private KensaHoukokusyoKanriSearchDA As New KensaHoukokusyoKanriSearchListDataAccess
    Private commonCheck As New CsvInputCheck
    Private ninsyou As New Ninsyou()
    Private userId As String = ninsyou.GetUserID()
    Private InputDate As String

    ''' <summary>
    ''' 検査報告書管理テーブルを取得する
    ''' </summary>
    ''' <param name="strKakunouDateFrom">格納日From</param>
    ''' <param name="strKakunouDateTo">格納日To</param>
    ''' <param name="strSendDateFrom">発送日From</param>
    ''' <param name="strSendDateTo">発送日To</param>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strNoFrom">番号From</param>
    ''' <param name="strNoTo">番号To</param>
    ''' <param name="strKameitenCdFrom">加盟店CD</param>
    ''' <param name="strKensakuJyouken">検索上限件数</param>
    ''' <param name="blnKensakuTaisyouGai">取消は検索対象外</param>
    ''' <param name="blnSendDateTaisyouGai">発送日セット済みは対象外</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/04　高兵兵(大連情報システム部)　新規作成</para>
    Public Function GetKensaHoukokusyoKanriSearch(ByVal strKakunouDateFrom As String, ByVal strKakunouDateTo As String, ByVal strSendDateFrom As String, _
                                                  ByVal strSendDateTo As String, ByVal strKbn As String, ByVal strNoFrom As String, _
                                                  ByVal strNoTo As String, ByVal strKameitenCdFrom As String, ByVal strKensakuJyouken As String, _
                                                  ByVal blnKensakuTaisyouGai As Boolean, ByVal blnSendDateTaisyouGai As Boolean) As DataTable

        Return KensaHoukokusyoKanriSearchDA.SelKensaHoukokusyoKanriSearch(strKakunouDateFrom, strKakunouDateTo, strSendDateFrom, strSendDateTo, strKbn, strNoFrom, _
                                                                        strNoTo, strKameitenCdFrom, strKensakuJyouken, blnKensakuTaisyouGai, blnSendDateTaisyouGai)

    End Function

    ''' <summary>
    ''' 検査報告書管理件数を取得する
    ''' </summary>
    ''' <param name="strKakunouDateFrom">格納日From</param>
    ''' <param name="strKakunouDateTo">格納日To</param>
    ''' <param name="strSendDateFrom">発送日From</param>
    ''' <param name="strSendDateTo">発送日To</param>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strNoFrom">番号From</param>
    ''' <param name="strNoTo">番号To</param>
    ''' <param name="strKameitenCdFrom">加盟店CD</param>
    ''' <param name="strKensakuJyouken">検索上限件数</param>
    ''' <param name="blnKensakuTaisyouGai">取消は検索対象外</param>
    ''' <param name="blnSendDateTaisyouGai">発送日セット済みは対象外</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/04　高兵兵(大連情報システム部)　新規作成</para>
    Public Function GetKensaHoukokusyoKanriSearchCount(ByVal strKakunouDateFrom As String, ByVal strKakunouDateTo As String, ByVal strSendDateFrom As String, _
                                                  ByVal strSendDateTo As String, ByVal strKbn As String, ByVal strNoFrom As String, _
                                                  ByVal strNoTo As String, ByVal strKameitenCdFrom As String, ByVal strKensakuJyouken As String, _
                                                  ByVal blnKensakuTaisyouGai As Boolean, ByVal blnSendDateTaisyouGai As Boolean) As Integer

        Return KensaHoukokusyoKanriSearchDA.SelKensaHoukokusyoKanriSearchCount(strKakunouDateFrom, strKakunouDateTo, strSendDateFrom, strSendDateTo, strKbn, strNoFrom, _
                                                                        strNoTo, strKameitenCdFrom, strKensakuJyouken, blnKensakuTaisyouGai, blnSendDateTaisyouGai)

    End Function

    ''' <summary>
    ''' <summary>検査報告書管理を更新する</summary>
    ''' </summary>
    ''' <param name="strHassoudate">発送日</param>
    ''' <param name="strSouhutantousya">送付担当者</param>
    ''' <param name="dtKensa">検査報告書管理テーブル</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/04　高兵兵(大連情報システム部)　新規作成</para>
    Public Function UpdKensahoukokusho(ByVal strHassoudate As String, ByVal strSouhutantousya As String, ByVal dtKensa As DataTable) As Boolean
        Using scope As New TransactionScope(TransactionScopeOption.RequiresNew)
            Try
                KensaHoukokusyoKanriSearchDA.UpdKensahoukokusho(strHassoudate, strSouhutantousya, userId, dtKensa)
                scope.Complete()

            Catch ex As Exception
                scope.Dispose()
                Return False
            End Try
        End Using
        Return True
    End Function

    ''' <summary>
    ''' <summary>検査報告書管理を更新する</summary>
    ''' </summary>
    ''' <param name="dtKensa">検査報告書管理テーブル</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/04　高兵兵(大連情報システム部)　新規作成</para>
    Public Function UpdKensahoukokushoTorikesi(ByVal dtKensa As DataTable) As Boolean
        Using scope As New TransactionScope(TransactionScopeOption.RequiresNew)
            Try

                KensaHoukokusyoKanriSearchDA.UpdKensahoukokushoTorikesi(userId, dtKensa)
                scope.Complete()
            Catch ex As Exception
                scope.Dispose()
                Return False
            End Try
        End Using
        Return True
    End Function

    ''' <summary>
    ''' 検査報告書管理テーブルを取得する
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店CD</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/04　高兵兵(大連情報システム部)　新規作成</para>
    Public Function SelMkameiten(ByVal strKameitenCd As String) As DataTable

        Return KensaHoukokusyoKanriSearchDA.SelMkameiten(strKameitenCd)

    End Function

End Class

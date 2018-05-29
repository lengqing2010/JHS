Imports System.Transactions
Imports Itis.Earth.DataAccess

''' <summary>検査報告書_各帳票出力画面</summary>
''' <remarks>検査報告書_各帳票出力画面用機能を提供する</remarks>
''' <history>
''' <para>2015/12/15　高兵兵(大連情報システム部)　新規作成</para>
''' </history>
Public Class KensaHoukokusyoOutputLogic

    Private KensaHoukokusyoOutputDA As New KensaHoukokusyoOutputDataAccess
    Private commonCheck As New CsvInputCheck
    Private ninsyou As New Ninsyou()
    Private userId As String = Ninsyou.GetUserID()
    Private InputDate As String

    ''' <summary>
    ''' 検査報告書管理テーブルを取得する
    ''' </summary>
    ''' <param name="strSendDateFrom">発送日From</param>
    ''' <param name="strSendDateTo">発送日To</param>
    ''' <param name="strKbn">区分</param>
    ''' <param name="tbxNoTo">番号From</param>
    ''' <param name="strNoTo">番号To</param>
    ''' <param name="strKameitenCdFrom">加盟店CD</param>
    ''' <param name="strKensakuJyouken">検索上限件数</param>
    ''' <param name="blnKensakuTaisyouGai">取消は検索対象外</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/15　高兵兵(大連情報システム部)　新規作成</para>
    Public Function GetKensaHoukokusyoKanriSearch(ByVal strSendDateFrom As String, ByVal strSendDateTo As String, ByVal strKbn As String, _
                                                  ByVal tbxNoTo As String, ByVal strNoTo As String, ByVal strKameitenCdFrom As String, _
                                                  ByVal strKensakuJyouken As String, ByVal blnKensakuTaisyouGai As Boolean) As Data.DataTable

        Return KensaHoukokusyoOutputDA.SelKensaHoukokusyoKanriSearch(strSendDateFrom, strSendDateTo, strKbn, tbxNoTo, strNoTo, strKameitenCdFrom, strKensakuJyouken, blnKensakuTaisyouGai)

    End Function

    ''' <summary>
    ''' 管理表EXCEL出力を取得する
    ''' </summary>
    ''' <param name="strKanriNo">管理No</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/15　高兵兵(大連情報システム部)　新規作成</para>
    Public Function GetKanrihyouExcelInfo(ByVal strKanriNo As String) As Data.DataTable

        Return KensaHoukokusyoOutputDA.SelKanrihyouExcelInfo(strKanriNo)
    End Function

    ''' <summary>
    ''' 送付状PDF出力を取得する
    ''' </summary>
    ''' <param name="strKanriNo">管理No</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/15　高兵兵(大連情報システム部)　新規作成</para>
    Public Function GetSyoufujyouPdfInfo(ByVal strKanriNo As String) As Data.DataTable

        Return KensaHoukokusyoOutputDA.SelSyoufujyouPdfInfo(strKanriNo)
    End Function

    ''' <summary>
    ''' 報告書PDF出力を取得する
    ''' </summary>
    ''' <param name="strKanriNo">管理No</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/15　高兵兵(大連情報システム部)　新規作成</para>
    Public Function GetHoukokusyoPdfInfo(ByVal strKanriNo As String) As Data.DataTable

        Return KensaHoukokusyoOutputDA.SelHoukokusyoPdfInfo(strKanriNo)
    End Function




    ''' <summary>
    ''' 検査報告書管理件数を取得する
    ''' </summary>
    ''' <param name="strSendDateFrom">発送日From</param>
    ''' <param name="strSendDateTo">発送日To</param>
    ''' <param name="strKbn">区分</param>
    ''' <param name="tbxNoTo">番号From</param>
    ''' <param name="strNoTo">番号To</param>
    ''' <param name="strKameitenCdFrom">加盟店CD</param>
    ''' <param name="strKensakuJyouken">検索上限件数</param>
    ''' <param name="blnKensakuTaisyouGai">取消は検索対象外</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/15　高兵兵(大連情報システム部)　新規作成</para>
    Public Function GetKensaHoukokusyoKanriSearchCount(ByVal strSendDateFrom As String, ByVal strSendDateTo As String, ByVal strKbn As String, _
                                                      ByVal tbxNoTo As String, ByVal strNoTo As String, ByVal strKameitenCdFrom As String, _
                                                      ByVal strKensakuJyouken As String, ByVal blnKensakuTaisyouGai As Boolean) As Integer

        Return KensaHoukokusyoOutputDA.SelKensaHoukokusyoKanriSearchCount(strSendDateFrom, strSendDateTo, strKbn, tbxNoTo, strNoTo, strKameitenCdFrom, strKensakuJyouken, blnKensakuTaisyouGai)

    End Function

    ''' <summary>
    ''' <summary>検査報告書管理を更新する</summary>
    ''' </summary>
    ''' <param name="dtKensa">検査報告書管理テーブル</param>
    ''' <param name="strFlg">ボタン区分(1:管理表EXCEL出力;2:送付状PDF出力;3:報告書PDF出力)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/15　高兵兵(大連情報システム部)　新規作成</para>
    Public Function UpdKensahoukokusho(ByVal dtKensa As DataTable, ByVal strFlg As String) As Boolean
        Using scope As New TransactionScope(TransactionScopeOption.RequiresNew)
            Try
                KensaHoukokusyoOutputDA.UpdKensahoukokusho(userId, dtKensa, strFlg)
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
    ''' <para>2015/12/15　高兵兵(大連情報システム部)　新規作成</para>
    Public Function SelMkameiten(ByVal strKameitenCd As String) As DataTable

        Return KensaHoukokusyoOutputDA.SelMkameiten(strKameitenCd)

    End Function

End Class


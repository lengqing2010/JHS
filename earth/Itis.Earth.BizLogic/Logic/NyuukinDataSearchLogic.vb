
Public Class NyuukinDataSearchLogic
    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    'UtilitiesのMessegeLogicクラス
    Dim mLogic As New MessageLogic

    'UtilitiesのStringLogicクラス
    Dim sLogic As New StringLogic

    ''' <summary>
    ''' 検索画面用入金データを取得します
    ''' </summary>
    ''' <param name="keyRec">入金データKeyレコード</param>
    ''' <param name="startRow">開始行</param>
    ''' <param name="endRow">最終行</param>
    ''' <param name="allCount">全件数</param>
    ''' <returns>入金データ検索用レコードのList(Of NyuukinDataRecord)</returns>
    ''' <remarks></remarks>
    Public Function GetNyuukinDataInfo(ByVal sender As Object, _
                                       ByVal keyRec As NyuukinDataKeyRecord, _
                                       ByVal startRow As Integer, _
                                       ByVal endRow As Integer, _
                                       ByRef allCount As Integer) As List(Of NyuukinDataRecord)
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetNyuukinDataInfo", _
                                            keyRec, _
                                            startRow, _
                                            endRow, _
                                            allCount)

        '検索実行クラス
        Dim dataAccess As New nyuukinDataAccess
        '取得データ格納用リスト
        Dim list As New List(Of NyuukinDataRecord)

        Try

            '検索処理の実行
            Dim table As DataTable = dataAccess.GetNyuukinDataInfo(keyRec)

            ' 総件数をセット
            allCount = table.Rows.Count

            ' 検索結果を格納用リストにセット
            list = DataMappingHelper.Instance.getMapArray(Of NyuukinDataRecord)(GetType(NyuukinDataRecord), table, startRow, endRow)

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = -2 Then  'エラーキャッチ：タイムアウト
                mLogic.AlertMessage(sender, Messages.MSG086E, 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            ' 総件数カウントに-1をセット
            allCount = -1
        End Try

        Return list

    End Function

    ''' <summary>
    ''' CSV出力用入金データを取得します
    ''' </summary>
    ''' <param name="keyRec">入金データKeyレコード</param>
    ''' <param name="allCount">全件数</param>
    ''' <returns>入金データCSV出力用データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetNyuukinDataCsv(ByVal sender As Object, _
                                       ByVal keyRec As NyuukinDataKeyRecord, _
                                       ByRef allCount As Integer) As DataTable
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetNyuukinDataCsv", _
                                            keyRec, _
                                            allCount)

        '検索実行クラス
        Dim dataAccess As New NyuukinDataAccess
        '取得データ格納用リスト
        Dim list As New List(Of NyuukinDataRecord)

        Dim dtRet As New DataTable

        Try
            '検索処理の実行
            dtRet = dataAccess.GetNyuukinDataCsv(keyRec)

            ' 総件数をセット
            allCount = dtRet.Rows.Count

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = -2 Then  'エラーキャッチ：タイムアウト
                mLogic.AlertMessage(sender, Messages.MSG086E, 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            ' 総件数カウントに-1をセット
            allCount = -1
        End Try

        Return dtRet
    End Function
End Class

Imports System.Data.SqlClient
Imports System.Web.UI

Public Class SeikyuuMiinsatuDataSearchLogic

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    'UtilitiesのMessegeLogicクラス
    Dim mLogic As New MessageLogic

    'UtilitiesのStringLogicクラス
    Dim sLogic As New StringLogic

    '更新日時保持用
    Dim pUpdDateTime As DateTime

    ''' <summary>
    ''' 請求書未印刷の請求データを取得します
    ''' </summary>
    ''' <param name="dtRec">請求データレコード</param>
    ''' <param name="startRow">開始行</param>
    ''' <param name="endRow">最終行</param>
    ''' <param name="allCount">全件数</param>
    ''' <returns>請求データ検索用レコードのList(Of SeikyuuDataRecord)</returns>
    ''' <remarks></remarks>

    Public Function GetSeikyuuMiinsatuData(ByVal sender As Object, _
                                   ByVal dtRec As SeikyuuDataRecord, _
                                   ByVal startRow As Integer, _
                                   ByVal endRow As Integer, _
                                   ByRef allCount As Integer) As List(Of SeikyuuDataRecord)


        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuuMiinsatuData", _
                                            dtRec, _
                                            startRow, _
                                            endRow, _
                                            allCount _
                                            )

        '検索実行クラス
        Dim dataAccess As New SeikyuuMiinsatuDataAccess

        '取得データ格納用リスト
        Dim list As New List(Of SeikyuuDataRecord)

        Try
            '検索処理の実行
            Dim table As DataTable = dataAccess.GetSearchSeikyuusyoTbl(dtRec)

            ' 総件数をセット
            allCount = table.Rows.Count

            ' 検索結果を格納用リストにセット
            list = DataMappingHelper.Instance.getMapArray(Of SeikyuuDataRecord)(GetType(SeikyuuDataRecord), table, startRow, endRow)

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

End Class

Imports System.Transactions
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI


Public Class KousinRirekiLogic

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    'UtilitiesのMessegeLogicクラス
    Dim mLogic As New MessageLogic



    ''' <summary>
    ''' 該当テーブルの情報を取得します
    ''' </summary>
    ''' <param name="recKey">検索条件</param>
    ''' <returns>該当レコードを格納したリスト ／ 0件の場合はNothing</returns>
    ''' <remarks>検索条件をKEYにして取得</remarks>
    Public Function getSearchKeyDataList(ByVal sender As Object _
                                                , ByVal recKey As KousinRirekiDataKeyRecord _
                                                , ByVal startRow As Integer _
                                                , ByVal endRow As Integer _
                                                , ByRef allCount As Integer) As List(Of KousinRirekiDataRecord)

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getSearchKeyDataTable", recKey _
                                                                                                , startRow _
                                                                                                , endRow _
                                                                                                , allCount _
                                                                                                )

        'データアクセスクラス
        Dim clsDataAcc As New KousinRirekiDataAccess
        '検索結果格納用データテーブル
        Dim dTblResult As New DataTable
        '検索結果格納用レコードリスト
        Dim listResult As New List(Of KousinRirekiDataRecord)

        Try
            dTblResult = clsDataAcc.getSearchTable(recKey)

            ' 総件数をセット
            allCount = dTblResult.Rows.Count

            If allCount > 0 Then
                ' 検索結果を格納用リストにセット
                listResult = DataMappingHelper.Instance.getMapArray(Of KousinRirekiDataRecord)(GetType(KousinRirekiDataRecord), dTblResult, startRow, endRow)
            End If

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

        Return listResult
    End Function

End Class

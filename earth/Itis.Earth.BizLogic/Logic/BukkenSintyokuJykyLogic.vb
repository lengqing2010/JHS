Imports System.Transactions

Public Class BukkenSintyokuJykyLogic

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    'UtilitiesのMessegeLogicクラス
    Dim mLogic As New MessageLogic


    ''' <summary>
    ''' 保証書管理テーブルの情報を取得します
    ''' </summary>
    ''' <param name="kbn">区分</param>
    ''' <param name="hosyousyoNo">保証書NO</param>
    ''' <returns>該当レコードを格納したリスト ／ 0件の場合はNothing</returns>
    ''' <remarks>検索条件をKEYにして取得</remarks>
    Public Function getSearchKeyDataRec(ByVal sender As Object _
                                                , ByVal kbn As String _
                                                , ByVal hosyousyoNo As String) As HosyousyoKanriRecord

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getSearchKeyDataRec", _
                                            kbn, _
                                            hosyousyoNo)

        'データアクセスクラス
        Dim clsDataAcc As New BukkenSintyokuJykyDataAccess
        '検索結果格納用データテーブル
        Dim dTblResult As New DataTable
        '検索結果格納用レコードリスト
        Dim recResult As New HosyousyoKanriRecord

        dTblResult = clsDataAcc.getSearchTable(kbn, hosyousyoNo)

        If dTblResult.Rows.Count > 0 Then
            ' 検索結果を格納用リストにセット
            recResult = DataMappingHelper.Instance.getMapArray(Of HosyousyoKanriRecord)(GetType(HosyousyoKanriRecord), dTblResult)(0)
        End If
        Return recResult

    End Function

    ''' <summary>
    ''' 保証書管理テーブル追加/更新処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="kbn">区分</param>
    ''' <param name="hosyousyoNo">保証書NO</param>
    ''' <param name="hkUpdDatetime">保証書管理テーブル更新日時</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function setHosyousyoKanriBukken(ByVal sender As Object _
                                                , ByVal kbn As String _
                                                , ByVal hosyousyoNo As String _
                                                , ByVal hkUpdDatetime As DateTime) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".setHosyousyoKanriBukken", _
                                                    kbn, _
                                                    hosyousyoNo, _
                                                    hkUpdDatetime)
        'データアクセスクラス
        Dim clsDataAcc As New BukkenSintyokuJykyDataAccess
        Dim hkRec As New HosyousyoKanriRecord
        Dim intResult As Integer = 0

        Const ERRMSG As String = "ストアドプロシージャ異常終了\r\nエラーコード：[{0}]\r\nエラーメッセージ：[{1}]"

        Try
            'トランザクション制御を行う
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                If kbn <> String.Empty OrElse Not kbn Is Nothing _
                    OrElse hosyousyoNo <> String.Empty OrElse Not hosyousyoNo Is Nothing Then

                    '該当のレコードを検索
                    hkRec = Me.getSearchKeyDataRec(sender, kbn, hosyousyoNo)

                    '排他チェック
                    If hkUpdDatetime <> hkRec.UpdDateTime Then
                        '排他チェックエラー
                        mLogic.CallHaitaErrorMessage(sender, hkRec.UpdLoginUserId, hkRec.UpdDateTime, "保証書管理テーブル")
                        Return False
                    End If

                    'ストアドプロシージャを呼出
                    intResult = clsDataAcc.setHosyousyoKanriData(kbn, hosyousyoNo)

                    If intResult = 0 Then
                        ' 更新に成功した場合、トランザクションをコミットする
                        scope.Complete()
                        Return True
                    Else
                        '処理失敗
                        Return False
                    End If
                End If

            End Using

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = 1205 Then    'エラーキャッチ：デッドロック
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            ElseIf exSqlException.Number = -2 Then  'エラーキャッチ：タイムアウト
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            Else
                mLogic.AlertMessage(sender, String.Format(ERRMSG, exSqlException.Number, exSqlException.Message), 0, "SqlException")
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            Return False
        Catch exTransactionException As System.Transactions.TransactionException
            'エラーキャッチ：トランザクションエラー
            UnTrappedExceptionManager.Publish(exTransactionException)
            mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exTransactionException.ToString), 0, "TransactionException")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 入金予定日を取得
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="kbn"></param>
    ''' <param name="hosyousyoNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function setNyuukinYoteiDate(ByVal sender As Object _
                                        , ByVal kbn As String _
                                        , ByVal hosyousyoNo As String _
                                        , ByVal dtHkUpdDatetime As DateTime) As String

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".setNyuukinYoteiDate", _
                                            kbn, _
                                            hosyousyoNo)
        'データアクセスクラス
        Dim clsDataAcc As New BukkenSintyokuJykyDataAccess
        Dim strData As String

        strData = clsDataAcc.setNyuukinYoteiDate(kbn, hosyousyoNo, dtHkUpdDatetime)

        Return strData
    End Function
End Class
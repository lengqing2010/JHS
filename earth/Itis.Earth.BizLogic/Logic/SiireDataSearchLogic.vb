Imports System.Transactions

Public Class SiireDataSearchLogic
    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    'UtilitiesのMessegeLogicクラス
    Dim mLogic As New MessageLogic

    'UtilitiesのStringLogicクラス
    Dim sLogic As New StringLogic

#Region "仕入データ取得"
    ''' <summary>
    ''' 検索画面用仕入データを取得します
    ''' </summary>
    ''' <param name="keyRec">仕入データKeyレコード</param>
    ''' <param name="startRow">開始行</param>
    ''' <param name="endRow">最終行</param>
    ''' <param name="allCount">全件数</param>
    ''' <returns>仕入データ検索用レコードのList(Of SiireDataRecord)</returns>
    ''' <remarks></remarks>
    Public Function GetSiireDataInfo(ByVal sender As Object, _
                                       ByVal keyRec As SiireDataKeyRecord, _
                                       ByVal startRow As Integer, _
                                       ByVal endRow As Integer, _
                                       ByRef allCount As Integer) As List(Of SiireSearchResultRecord)
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSiireDataInfo", _
                                            keyRec, _
                                            startRow, _
                                            endRow, _
                                            allCount)
        '検索実行クラス
        Dim dataAccess As New SiireDataAccess
        '取得データ格納用リスト
        Dim list As New List(Of SiireSearchResultRecord)

        Try
            '検索処理の実行
            Dim table As DataTable = dataAccess.GetSiireDataInfo(keyRec)

            ' 総件数をセット
            allCount = table.Rows.Count

            ' 検索結果を格納用リストにセット
            list = DataMappingHelper.Instance.getMapArray(Of SiireSearchResultRecord)(GetType(SiireSearchResultRecord), table, startRow, endRow)

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
    ''' 仕入データテーブルをPKで所得します
    ''' </summary>
    ''' <param name="strDenUnqNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSiireDataRec(ByVal sender As Object, ByVal strDenUnqNo As String) As SiireDataRecord
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSiireDataRec", strDenUnqNo)

        'データアクセスクラス
        Dim clsDataAcc As New SiireDataAccess
        '検索結果格納用データテーブル
        Dim dTblResult As New DataTable
        '検索結果格納用レコードリスト
        Dim recResult As New SiireDataRecord

        If strDenUnqNo = String.Empty Then
            Return recResult
        End If

        dTblResult = clsDataAcc.GetSiireDataRec(strDenUnqNo)

        If dTblResult.Rows.Count > 0 Then
            ' 検索結果を格納用リストにセット
            recResult = DataMappingHelper.Instance.getMapArray(Of SiireDataRecord)(GetType(SiireDataRecord), dTblResult)(0)
        End If
        Return recResult

    End Function

    ''' <summary>
    ''' CSV出力用仕入データを取得します
    ''' </summary>
    ''' <param name="keyRec">仕入データKeyレコード</param>
    ''' <param name="allCount">全件数</param>
    ''' <returns>仕入データCSV出力用データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetSiireDataCsv(ByVal sender As Object, _
                                       ByVal keyRec As SiireDataKeyRecord, _
                                       ByRef allCount As Integer) As DataTable
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSiireDataCsv", _
                                            keyRec, _
                                            allCount)

        '検索実行クラス
        Dim dataAccess As New SiireDataAccess
        '取得データ格納用リスト
        Dim list As New List(Of SiireDataRecord)

        Dim dtRet As New DataTable

        Try
            '検索処理の実行
            dtRet = dataAccess.GetSiireDataCsv(keyRec)

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
#End Region

#Region "仕入先情報取得"
    ''' <summary>
    ''' 仕入先コードの情報をSyouhinMeisaiRecordクラスのList(Of SyouhinMeisaiRecord)で取得します
    ''' </summary>
    ''' <param name="siireSakiCd">仕入先コード</param>
    ''' <param name="siireSakiBrc">仕入先枝番</param>
    ''' <param name="siireSakiMei">仕入先名</param>
    ''' <param name="siireSakiKana">仕入先カナ</param>
    ''' <returns>siireSakiInfoRecordクラスのList</returns>
    ''' <remarks></remarks>
    Public Function GetSiireSakiInfo(ByVal siireSakiCd As String, _
                                       ByVal siireSakiBrc As String, _
                                       ByVal siireSakiMei As String, _
                                       ByVal siireSakiKana As String, _
                                       ByRef allCount As Integer, _
                                       Optional ByVal startRow As Integer = 1, _
                                       Optional ByVal endRow As Integer = Integer.MaxValue) As List(Of SiireSakiInfoRecord)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSiireSakiInfo", _
                                                    siireSakiCd, _
                                                    siireSakiBrc, _
                                                    siireSakiMei, _
                                                    siireSakiKana, _
                                                    allCount, _
                                                    startRow, _
                                                    endRow)

        Dim dataAccess As New SiireDataAccess
        Dim list As List(Of SiireSakiInfoRecord)

        Dim table As DataTable = dataAccess.searchSiireSakiInfo(siireSakiCd, siireSakiBrc, siireSakiMei, siireSakiKana)

        ' 件数を設定
        allCount = table.Rows.Count

        ' データを取得し、List(Of SyouhinMeisaiRecord)に格納する
        list = DataMappingHelper.Instance.getMapArray(Of SiireSakiInfoRecord)(GetType(SiireSakiInfoRecord), _
                                                      table, _
                                                      startRow, _
                                                      endRow)

        Return list

    End Function

#End Region

#Region "仕入データ更新処理"
    ''' <summary>
    ''' 仕入レコードより仕入データテーブルを更新する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="recSiire"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveSiireData(ByVal sender As Object, ByVal recSiire As SiireDataRecord) As Boolean
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SaveSiireData", sender, recSiire)

        Dim clsJbDataAcc As New JibanDataAccess
        Dim recResult As New SiireDataRecord
        Dim updDateTime As DateTime = DateTime.Now
        Dim end_count As Integer = EarthConst.MAX_RESULT_COUNT
        Dim total_count As Integer = 0
        Dim IFsqlMaker As ISqlStringHelper = New UpdateStringHelper
        Dim strSqlString As String = String.Empty
        Dim listParam As New List(Of ParamRecord)
        Dim recType As Type = GetType(SiireDataRecord)

        Try
            'トランザクション制御を行う
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                ' 更新日時取得（システム日時）
                updDateTime = DateTime.Now

                If recSiire.DenpyouUniqueNo.ToString <> String.Empty Then

                    '更新対象のレコードを取得
                    recResult = GetSiireDataRec(sender, recSiire.DenpyouUniqueNo)

                    '排他チェック
                    If recResult.UpdDatetime <> recSiire.UpdDatetime Then
                        ' 排他チェックエラー
                        mLogic.CallHaitaErrorMessage(sender, recResult.UpdLoginUserId, recResult.UpdDatetime, "仕入データテーブル")
                        Return False
                    End If

                    '更新日時を設定
                    recSiire.UpdDatetime = updDateTime

                    '更新用文字列とパラメータの作成
                    strSqlString = IFsqlMaker.MakeUpdateInfo(recType, recSiire, listParam, GetType(SiireDataRecord))

                    'DB反映処理
                    If Not clsJbDataAcc.UpdateJibanData(strSqlString, listParam) Then
                        Return False
                        Exit Function
                    End If

                End If

                ' 更新に成功した場合、トランザクションをコミットする
                scope.Complete()

            End Using

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = 1205 Then    'エラーキャッチ：デッドロック
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            ElseIf exSqlException.Number = -2 Then  'エラーキャッチ：タイムアウト
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            Return False
        Catch exTransactionException As System.Transactions.TransactionException
            'エラーキャッチ：トランザクションエラー
            UnTrappedExceptionManager.Publish(exTransactionException)
            mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exTransactionException.ToString), 0, "TransactionException")
            Return False
        End Try

        Return True

    End Function
#End Region

End Class

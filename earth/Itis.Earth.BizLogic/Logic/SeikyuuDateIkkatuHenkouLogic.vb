Imports System.Transactions
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI

Public Class SeikyuuDateIkkatuHenkouLogic
    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    'メッセージロジック
    Dim mLogic As New MessageLogic

#Region "請求書発行日一括更新処理メイン"
    ''' <summary>
    ''' 請求書発行日一括更新処理メイン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="seiCd"></param>
    ''' <param name="seiBrc"></param>
    ''' <param name="seiKbn"></param>
    ''' <param name="updDate"></param>
    ''' <param name="loginUserId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SeikyuuDateIkkatuHenkou(ByVal sender As System.Object, _
                                            ByVal seiCd As String, _
                                            ByVal seiBrc As String, _
                                            ByVal seiKbn As String, _
                                            ByVal seiDate As Date, _
                                            ByVal updDate As DateTime, _
                                            ByVal loginUserId As String) As List(Of Integer)

        Dim listResult As New List(Of Integer)
        Dim intResult As Integer = 0
        listResult.Add(intResult)

        Try

            '各種テーブルと各種連携管理テーブルの同期を保つため、トランザクション制御を行う
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                Dim dataAccess As New SeikyuuDateIkkatuHenkouDataAccess(seiCd, seiBrc, seiKbn, seiDate, updDate, loginUserId)
                Dim renkeiDataAccess As New RenkeiKanriDataAccess

                '***********************************************
                ' 以下の更新順は画面側表示項目順と同期をとること
                '***********************************************

                '1.請求書発行日一括更新＠邸別請求テーブル
                intResult = DataAccess.updSeikyuuDateTeibetuSeikyuu()
                listResult(0) = intResult
                listResult.Add(intResult)

                '2.請求書発行日一括更新＠店別請求テーブル
                intResult = DataAccess.updSeikyuuDateTenbetuSeikyuu()
                listResult(0) = intResult
                listResult.Add(intResult)

                '3.請求書発行日一括更新＠店別初期請求テーブル
                intResult = DataAccess.updSeikyuuDateTenbetuSyokiSeikyuu()
                listResult(0) = intResult
                listResult.Add(intResult)

                '4.請求書発行日一括更新＠汎用売上テーブル
                intResult = DataAccess.updSeikyuuDateHannyouUriage()
                listResult(0) = intResult
                listResult.Add(intResult)

                '5.売上データテーブルの請求年月日をUPDATE(元テーブルが削除されたマイナス伝票を対象)
                intResult = DataAccess.updateUriDataSeikyuuDate()
                listResult(0) = intResult
                listResult.Add(intResult)

                '6.売上データテーブルの請求年月日をUPDATE(計上済でかつ請求書未作成のマイナス伝票を対象)
                intResult = dataAccess.updateTorikesiUriDataSeikyuuDate()
                listResult(0) = intResult
                listResult.Add(intResult)

                '7.邸別請求連携管理テーブルにUPDATE/INSERT
                intResult = renkeiDataAccess.setTeibetuSeikyuuRenkei(updDate, loginUserId)
                listResult(0) = intResult
                listResult.Add(intResult)

                '8.店別請求連携管理テーブルにUPDATE/INSERT
                intResult = renkeiDataAccess.setTenbetuSeikyuuRenkei(updDate, loginUserId)
                listResult(0) = intResult
                listResult.Add(intResult)

                '9.店別初期請求連携管理テーブルにUPDATE/INSERT
                intResult = renkeiDataAccess.setTenbetuSyokiSeikyuuRenkei(updDate, loginUserId)
                listResult(0) = intResult
                listResult.Add(intResult)

                'トランザクションスコープの確定(コミット)
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
            listResult(0) = Integer.MinValue
        Catch exTransactionException As System.Transactions.TransactionException
            'エラーキャッチ：トランザクションエラー
            UnTrappedExceptionManager.Publish(exTransactionException)
            mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exTransactionException.ToString), 0, "TransactionException")
            listResult(0) = Integer.MinValue
        End Try

        '結果戻し
        Return listResult

    End Function

#End Region

#Region "一括変更対象データ取得"
    ''' <summary>
    ''' 一括変更対象データを取得します
    ''' </summary>
    ''' <param name="seiCd">請求先コード</param>
    ''' <param name="seiBrc">請求先枝番</param>
    ''' <param name="seiKbn">請求先区分</param>
    ''' <param name="startRow">開始行</param>
    ''' <param name="endRow">最終行</param>
    ''' <param name="allCount">全件数</param>
    ''' <returns>一括変更対象データ用レコードのList(Of )</returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuuDateIkkatuHenkou(ByVal sender As System.Object, _
                                               ByVal seiCd As String, _
                                               ByVal seiBrc As String, _
                                               ByVal seiKbn As String, _
                                               ByVal startRow As Integer, _
                                               ByVal endRow As Integer, _
                                               ByRef allCount As Integer, _
                                               ByVal emType As EarthEnum.emIkkatuHenkouDataSearchType _
                                               ) As List(Of SeikyuuDateIkkatuHenkouRecord)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuuDateIkkatuHenkou", _
                                                    seiCd, _
                                                    seiBrc, _
                                                    seiKbn, _
                                                    startRow, _
                                                    endRow, _
                                                    allCount, _
                                                    emType)

        '検索実行クラス
        Dim dataAccess As New SeikyuuDateIkkatuHenkouDataAccess(seiCd, seiBrc, seiKbn)
        '取得データ格納用リスト
        Dim list As New List(Of SeikyuuDateIkkatuHenkouRecord)
        '取得データ格納用データテーブル
        Dim dtResult As DataTable

        Try
            '検索処理の実行
            Select Case emType

                Case EarthEnum.emIkkatuHenkouDataSearchType.TeibetuSeikyuu      '邸別請求テーブル
                    dtResult = dataAccess.GetTeibetuSeikyuuTbl

                Case EarthEnum.emIkkatuHenkouDataSearchType.TenbetuSeikyuu      '店別請求テーブル
                    dtResult = dataAccess.GetTenbetuSeikyuuTbl

                Case EarthEnum.emIkkatuHenkouDataSearchType.TenbetuSyoki        '店別初期請求テーブル
                    dtResult = dataAccess.GetTenbetuSyokiSeikyuuTbl

                Case EarthEnum.emIkkatuHenkouDataSearchType.HannyouUriage       '汎用売上テーブル
                    dtResult = dataAccess.GetHannyouUriageTbl

                Case EarthEnum.emIkkatuHenkouDataSearchType.UriageData          '売上データテーブル
                    dtResult = dataAccess.GetUriageDataTbl

                Case EarthEnum.emIkkatuHenkouDataSearchType.UriageDataTorikesiSeikyuuDate          '売上データテーブル
                    dtResult = dataAccess.GetUriageDataSeikyuuDateTbl

                Case Else
                    Return list
            End Select

            allCount = dtResult.Rows.Count
            list = DataMappingHelper.Instance.getMapArray(Of SeikyuuDateIkkatuHenkouRecord)(GetType(SeikyuuDateIkkatuHenkouRecord), dtResult, startRow, endRow)

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

#End Region
End Class

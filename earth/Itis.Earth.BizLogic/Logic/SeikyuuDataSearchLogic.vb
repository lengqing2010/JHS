Imports System.Transactions
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI

Public Class SeikyuuDataSearchLogic

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    'UtilitiesのMessegeLogicクラス
    Dim mLogic As New MessageLogic

    'UtilitiesのStringLogicクラス
    Dim sLogic As New StringLogic

    '更新日時保持用
    Dim pUpdDateTime As DateTime

#Region "請求データ取得"
    ''' <summary>
    ''' 検索画面用請求データを取得します
    ''' </summary>
    ''' <param name="keyRec">請求データKeyレコード</param>
    ''' <param name="startRow">開始行</param>
    ''' <param name="endRow">最終行</param>
    ''' <param name="allCount">全件数</param>
    ''' <param name="emType">請求データの検索タイプ</param>
    ''' <returns>請求データ検索用レコードのList(Of SeikyuuDataRecord)</returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuuDataInfo(ByVal sender As Object, _
                                       ByVal keyRec As SeikyuuDataKeyRecord, _
                                       ByVal startRow As Integer, _
                                       ByVal endRow As Integer, _
                                       ByRef allCount As Integer, _
                                       ByVal emType As EarthEnum.emSeikyuuSearchType _
                                       ) As List(Of SeikyuuDataRecord)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuuDataInfo", _
                                            keyRec, _
                                            startRow, _
                                            endRow, _
                                            allCount, _
                                            emType _
                                            )

        '検索実行クラス
        Dim dataAccess As New SeikyuuDataAccess
        '取得データ格納用リスト
        Dim list As New List(Of SeikyuuDataRecord)

        Try
            '検索処理の実行
            Dim table As DataTable = dataAccess.GetSearchSeikyuusyoTbl(keyRec, emType)

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

    ''' <summary>
    ''' 該当テーブルの情報をPKで取得します
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="strSeikyuusyoNo">請求書NO</param>
    ''' <returns>DB更新用レコードを格納したリスト ／ 0件の場合はNothing</returns>
    ''' <remarks>PKで該当テーブルの1レコードを取得</remarks>
    Public Function GetSeikyuuDataRec(ByVal sender As Object _
                                                , ByVal strSeikyuusyoNo As String _
                                                ) As SeikyuuDataRecord

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuuDataRec", sender, strSeikyuusyoNo)

        'データアクセスクラス
        Dim clsDataAcc As New SeikyuuDataAccess
        '検索結果格納用データテーブル
        Dim dTblResult As New DataTable
        '検索結果格納用レコードリスト
        Dim recResult As New SeikyuuDataRecord

        If strSeikyuusyoNo = String.Empty Then
            Return recResult
        End If

        dTblResult = clsDataAcc.GetSeikyuusyoSyuuseiRec(strSeikyuusyoNo)

        If dTblResult.Rows.Count > 0 Then
            ' 検索結果を格納用リストにセット
            recResult = DataMappingHelper.Instance.getMapArray(Of SeikyuuDataRecord)(GetType(SeikyuuDataRecord), dTblResult)(0)
        End If
        Return recResult
    End Function

    ''' <summary>
    ''' CSV出力用請求データを取得します
    ''' </summary>
    ''' <param name="keyRec">請求データKeyレコード</param>
    ''' <param name="allCount">全件数</param>
    ''' <param name="emType">請求データの検索タイプ</param>
    ''' <returns>請求データCSV出力用データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuusyoDataCsv(ByVal sender As Object, _
                                       ByVal keyRec As SeikyuuDataKeyRecord, _
                                       ByRef allCount As Integer, _
                                       ByVal emType As EarthEnum.emSeikyuuSearchType _
                                       ) As DataTable
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuusyoDataCsv", _
                                            keyRec, _
                                            allCount, _
                                            emType)

        '検索実行クラス
        Dim dataAccess As New SeikyuuDataAccess
        '取得データ格納用リスト
        Dim list As New List(Of SeikyuuDataRecord)

        Dim dtRet As New DataTable

        Try
            '検索処理の実行
            dtRet = dataAccess.GetSearchSeikyuuDataCsv(keyRec, emType)

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

    ''' <summary>
    ''' 請求鑑に紐付く請求明細データの未発行件数を取得します
    ''' ※請求鑑T.請求印刷日=NULLでかつ請求明細T.印字対象フラグ=1
    ''' </summary>
    ''' <param name="strAllCount">未発行データの総件数</param>
    ''' <remarks></remarks>
    Public Sub GetMihakkouCnt(ByVal sender As Object, ByRef strAllCount As String)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetMihakkouCnt", sender, strAllCount)

        '検索実行クラス
        Dim dataAccess As New SeikyuuDataAccess

        Try
            '検索処理の実行
            Dim table As DataTable = dataAccess.GetMihakkouCnt()

            ' 取得できなかった場合は空白を返却
            If table.Rows.Count > 0 Then
                Dim row As DataRow = table.Rows(0)
                ' 総件数をセット
                strAllCount = row("mihakkou_cnt").ToString()
            Else
                strAllCount = "0"
            End If

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = -2 Then  'エラーキャッチ：タイムアウト
                mLogic.AlertMessage(sender, Messages.MSG086E, 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            ' 総件数カウントに""(空白)をセット
            strAllCount = String.Empty
        End Try
    End Sub

    ''' <summary>
    ''' 請求鑑に紐付く請求明細の中で重複している伝票の件数を取得します
    ''' </summary>
    ''' <param name="strSeikyuusyoNo">請求書No</param>
    ''' <returns>請求明細取得件数</returns>
    ''' <remarks></remarks>
    Public Function GetDenpyouExistsCnt(ByVal sender As Object, ByVal strSeikyuusyoNo As String) As Integer
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDenpyouExistsCnt", strSeikyuusyoNo)

        '取得件数
        Dim intCnt As Integer = 0
        '検索実行クラス
        Dim dataAccess As New SeikyuuDataAccess
        '取得データ格納用テーブル
        Dim dtRet As New DataTable

        Try
            '検索処理の実行
            dtRet = dataAccess.GetDenpyouExistsCnt(strSeikyuusyoNo)

            ' 総件数をセット
            intCnt = dtRet.Rows.Count

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = -2 Then  'エラーキャッチ：タイムアウト
                mLogic.AlertMessage(sender, Messages.MSG086E, 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            ' 総件数カウントに-1をセット
            intCnt = -1
        End Try

        Return intCnt
    End Function

    ''' <summary>
    ''' 請求明細の伝票ユニークNOに紐付く、最新の請求鑑レコードの請求書NOを取得します
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="strDenUnqNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuusyoKagamiNo(ByVal sender As Object, ByVal strDenUnqNo As String) As String
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuusyoKagamiNo", strDenUnqNo)

        '請求書NO
        Dim strSeikyuusyoNo As String = String.Empty
        '検索実行クラス
        Dim clsDataAcc As New SeikyuuDataAccess
        '検索結果格納用データテーブル
        Dim dtResult As New DataTable
        '検索結果格納用レコード
        Dim recResult As New SeikyuuDataRecord

        Try
            dtResult = clsDataAcc.GetSeikyuusyoMaxRec(strDenUnqNo)

            If dtResult.Rows.Count > 0 Then
                ' 検索結果を格納用リストにセット
                recResult = DataMappingHelper.Instance.getMapArray(Of SeikyuuDataRecord)(GetType(SeikyuuDataRecord), dtResult)(0)
                ' 請求書NOを取得
                strSeikyuusyoNo = recResult.SeikyuusyoNo
            End If

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = -2 Then  'エラーキャッチ：タイムアウト
                mLogic.AlertMessage(sender, Messages.MSG086E, 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
        End Try

        Return strSeikyuusyoNo
    End Function

#End Region

#Region "請求データ更新"

    ''' <summary>
    ''' データ更新処理
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="listData">請求データのリスト</param>
    ''' <param name="emType">請求データの更新タイプ</param>
    ''' <returns>処理成否(Boolean)</returns>
    ''' <remarks>データ更新判断用列挙体の値で、処理を分岐する</remarks>
    Public Function saveData(ByVal sender As Object, ByRef listData As List(Of SeikyuuDataRecord), ByVal emType As EarthEnum.emSeikyuusyoUpdType) As Boolean
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".saveData", _
                                            sender, _
                                            listData, _
                                            emType)

        '排他用データアクセスクラス
        Dim clsJbDataAcc As New JibanDataAccess
        Dim recResult As New SeikyuuDataRecord 'レコードクラス
        Dim recTmp As New SeikyuuDataRecord '作業用
        'SQL文自動生成インターフェイス
        Dim IFsqlMaker As ISqlStringHelper = New UpdateStringHelper '更新のみ
        'SQL文
        Dim strSqlString As String = ""
        'パラメータレコードのリスト
        Dim listParam As New List(Of ParamRecord)

        Dim recType As Type = Nothing
        'レコード更新タイプを設定
        If emType = EarthEnum.emSeikyuusyoUpdType.SeikyuusyoTorikesi Then '取消
            recType = GetType(SeikyuuDataRecord)
        ElseIf emType = EarthEnum.emSeikyuusyoUpdType.SeikyuusyoPrint Then '請求書印刷
            recType = GetType(SeikyuuDataHakkouRecord)
        ElseIf emType = EarthEnum.emSeikyuusyoUpdType.SeikyuusyoSyuusei Then '請求書修正
            recType = GetType(SeikyuuDataSyuuseiRecord)
        Else
            Return False
            Exit Function
        End If

        Try
            'トランザクション制御を行う
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                ' 更新日時取得（システム日時）
                pUpdDateTime = DateTime.Now

                For Each recTmp In listData

                    If recTmp.SeikyuusyoNo <> String.Empty Then 'UPDATE

                        '更新対象のレコードを取得
                        recResult = Me.GetSeikyuuDataRec(sender, recTmp.SeikyuusyoNo)

                        '●排他チェック
                        If recResult.UpdDatetime <> recTmp.UpdDatetime Then
                            ' 排他チェックエラー
                            mLogic.CallHaitaErrorMessage(sender, recResult.UpdLoginUserId, recResult.UpdDatetime, "請求鑑テーブル")
                            Return False
                        End If

                        '請求書印刷
                        If emType = EarthEnum.emSeikyuusyoUpdType.SeikyuusyoPrint Then
                            '●請求書印刷日
                            If recResult.SeikyuusyoInsatuDate = DateTime.MinValue Then
                            Else
                                '請求書印刷日<>NULLで請求書印刷は行わないため、処理をとばす
                                Continue For
                            End If

                            '●取消
                            If recResult.Torikesi <> 0 Then
                                '取消で請求書印刷は行わないため、処理をとばす
                                Continue For
                            End If

                            '●請求書用紙汎用コード
                            If recResult.PrintTaisyougaiFlg = 1 Then
                                '印刷対象外で請求書印刷は行わないため、処理をとばす
                                Continue For
                            End If

                            '●請求書用紙
                            If recResult.KaisyuuSeikyuusyoYousi Is Nothing OrElse recResult.KaisyuuSeikyuusyoYousi = String.Empty Then
                                '請求書式未設定で請求書印刷は行わないため、処理をとばす
                                Continue For
                            End If

                        End If

                        '更新日時を設定
                        recTmp.UpdDatetime = pUpdDateTime

                        '更新用文字列とパラメータの作成
                        strSqlString = IFsqlMaker.MakeUpdateInfo(recType, recTmp, listParam, GetType(SeikyuuDataRecord))

                        'DB反映処理
                        If Not clsJbDataAcc.UpdateJibanData(strSqlString, listParam) Then
                            Return False
                            Exit Function
                        End If

                    End If
                Next

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

    ''' <summary>
    ''' 請求鑑レコードの取消をする
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="strSeikyuusyoNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdKagamiTorikesi(ByVal sender As System.Object, ByVal strSeikyuusyoNo As String, ByVal strLoginUserId As String) As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdKagamiTorikesi", sender, strSeikyuusyoNo, strLoginUserId)

        Dim strResultMsg As String = String.Empty
        Dim blnUpdResult As Boolean = False
        Dim dtAcc As New SeikyuuDataAccess

        Try
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                blnUpdResult = dtAcc.UpdKagamiTorikesi(strSeikyuusyoNo, strLoginUserId)

                If blnUpdResult = False Then
                    Return Messages.MSG147E.Replace("@PARAM1", "請求書取消")
                End If

                'トランザクションスコープ コンプリート
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

        Return String.Empty
    End Function

#End Region

End Class

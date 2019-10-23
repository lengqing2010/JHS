Imports System.Transactions
''' <summary>
''' 汎用仕入のロジッククラスです
''' </summary>
''' <remarks></remarks>
Public Class HannyouSiireLogic
    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    'UtilitiesのMessegeLogicクラス
    Dim mLogic As New MessageLogic

    '更新日時保持用
    Dim pUpdDateTime As DateTime

    ''' <summary>
    ''' 該当テーブルの情報を取得します
    ''' </summary>
    ''' <param name="recKey">検索条件</param>
    ''' <returns>該当レコードを格納したリスト ／ 0件の場合はNothing</returns>
    ''' <remarks>検索条件をKEYにして取得</remarks>
    Public Function getSearchKeyDataList(ByVal sender As Object _
                                                , ByVal recKey As HannyouSiireDataKeyRecord _
                                                , ByVal startRow As Integer _
                                                , ByVal endRow As Integer _
                                                , ByRef allCount As Integer) As List(Of HannyouSiireRecord)

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getSearchKeyDataTable", recKey _
                                                                                                , startRow _
                                                                                                , endRow _
                                                                                                , allCount _
                                                                                                )

        'データアクセスクラス
        Dim clsDataAcc As New HannyouSiireDataAccess  '汎用仕入
        '検索結果格納用データテーブル
        Dim dTblResult As New DataTable
        '検索結果格納用レコードリスト
        Dim listResult As New List(Of HannyouSiireRecord)

        Try
            dTblResult = clsDataAcc.getSearchTable(recKey)

            ' 総件数をセット
            allCount = dTblResult.Rows.Count

            If allCount > 0 Then
                ' 検索結果を格納用リストにセット
                listResult = DataMappingHelper.Instance.getMapArray(Of HannyouSiireRecord)(GetType(HannyouSiireRecord), dTblResult, startRow, endRow)
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

    ''' <summary>
    ''' 該当テーブルの情報をPKで取得します
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="strUniqueNo">汎用売上ユニークNO</param>
    ''' <returns>DB更新用レコードを格納したリスト ／ 0件の場合はNothing</returns>
    ''' <remarks>PKで該当テーブルの1レコードを取得</remarks>
    Public Function getSearchDataRec(ByVal sender As Object _
                                                , ByVal strUniqueNo As String _
                                                ) As HannyouSiireRecord

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getHannyouUriageData", sender, strUniqueNo)

        'データアクセスクラス
        Dim clsDataAcc As New HannyouSiireDataAccess  '汎用仕入
        '検索結果格納用データテーブル
        Dim dTblResult As New DataTable
        '検索結果格納用レコードリスト
        Dim recResult As New HannyouSiireRecord

        Dim intUniNo As Integer
        If strUniqueNo = String.Empty Then
            intUniNo = 0
        Else
            intUniNo = CInt(strUniqueNo)
        End If

        dTblResult = clsDataAcc.getSearchTable(intUniNo)

        If dTblResult.Rows.Count > 0 Then
            ' 検索結果を格納用リストにセット
            recResult = DataMappingHelper.Instance.getMapArray(Of HannyouSiireRecord)(GetType(HannyouSiireRecord), dTblResult)(0)
        End If
        Return recResult
    End Function

    ''' <summary>
    ''' データ更新処理
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="recData">汎用仕入レコード</param>
    ''' <returns>処理成否(Boolean)</returns>
    ''' <remarks>データ更新判断用列挙体の値で、処理を分岐する</remarks>
    Public Function saveData(ByVal sender As Object, ByRef recData As HannyouSiireRecord) As Boolean
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".saveData", _
                                            sender, _
                                            recData)

        '排他用データアクセスクラス
        Dim clsJbDataAcc As New JibanDataAccess
        Dim recResult As New HannyouSiireRecord 'レコードクラス
        'SQL文自動生成インターフェイス
        Dim IFsqlMaker As ISqlStringHelper
        'SQL文
        Dim strSqlString As String = ""
        'パラメータレコードのリスト
        Dim listParam As New List(Of ParamRecord)

        Try
            'トランザクション制御を行う
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                With recData
                    ' 更新日時取得（システム日時）
                    pUpdDateTime = DateTime.Now

                    If recData.HanSiireUnqNo <> Integer.MinValue Then 'UPDATE

                        '更新対象のレコードを取得
                        recResult = Me.getSearchDataRec(sender, recData.HanSiireUnqNo)

                        '●排他チェック
                        If recResult.UpdDatetime <> recData.UpdDatetime Then
                            ' 排他チェックエラー
                            mLogic.CallHaitaErrorMessage(sender, recResult.UpdLoginUserId, recResult.UpdDatetime, "汎用仕入テーブル")
                            Return False
                        End If

                        'SQL文自動生成インターフェイスを使用し、データを更新
                        IFsqlMaker = New UpdateStringHelper

                        '更新日時を設定
                        .UpdDatetime = pUpdDateTime

                    Else 'INSERT
                        'SQL文自動生成インターフェイスを使用し、データを登録
                        IFsqlMaker = New InsertStringHelper

                        '登録ログインユーザーIDを設定
                        .AddLoginUserId = recData.UpdLoginUserId
                        '登録ログインユーザー名を設定
                        .AddLoginUserName = recData.UpdLoginUserName
                        '登録日時を設定
                        .AddDatetime = pUpdDateTime

                    End If

                    '登録/更新用文字列とパラメータの作成
                    strSqlString = IFsqlMaker.MakeUpdateInfo(GetType(HannyouSiireRecord), recData, listParam, GetType(HannyouSiireRecord))

                    'DB反映処理
                    If Not clsJbDataAcc.UpdateJibanData(strSqlString, listParam) Then
                        Return False
                        Exit Function
                    End If

                End With

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

End Class

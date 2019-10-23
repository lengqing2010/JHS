Imports System.Transactions
''' <summary>
''' 入金取込(画面用)のロジッククラスです
''' </summary>
''' <remarks></remarks>
Public Class NyuukinTorikomiLogic
    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    'UtilitiesのMessegeLogicクラス
    Dim mLogic As New MessageLogic

    ''' <summary>
    ''' 入金ファイル取込テーブルの情報を取得します
    ''' </summary>
    ''' <param name="recKey">検索条件</param>
    ''' <returns>入金ファイル取込レコードを格納したリスト ／ 0件の場合はNothing</returns>
    ''' <remarks>検索条件をKEYにして取得</remarks>
    Public Function getNyuukinFileTorikomiList(ByVal sender As Object _
                                                , ByVal recKey As NyuukinFileTorikomiKeyRecord _
                                                , ByVal startRow As Integer _
                                                , ByVal endRow As Integer _
                                                , ByRef allCount As Integer) As List(Of NyuukinFileTorikomiRecord)

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getNyuukinFileTorikomiList", recKey)

        'データアクセスクラス
        Dim clsDataAcc As New JhsNyuukinTorikomiDataAccess  'JHS入金ファイル取込
        '物件履歴データテーブル
        Dim tblNkFileTorikomi As DataTable
        '物件履歴レコードリスト
        Dim listNkFileTorikomi As New List(Of NyuukinFileTorikomiRecord)

        Try
            tblNkFileTorikomi = clsDataAcc.getNkFileTorikomiTable(recKey)

            ' 総件数をセット
            allCount = tblNkFileTorikomi.Rows.Count

            If tblNkFileTorikomi.rows.Count > 0 Then
                ' 検索結果を格納用リストにセット
                listNkFileTorikomi = DataMappingHelper.Instance.getMapArray(Of NyuukinFileTorikomiRecord)(GetType(NyuukinFileTorikomiRecord), tblNkFileTorikomi, startRow, endRow)
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

        Return listNkFileTorikomi
    End Function

    ''' <summary>
    ''' 入金ファイル取込テーブルの情報をPKで取得します
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="strNyuukinTorikomiNo">入金取込ユニークNO</param>
    ''' <returns>入金ファイル取込レコードを格納したリスト ／ 0件の場合はNothing</returns>
    ''' <remarks>PKで入金取込テーブルの1レコードを取得</remarks>
    Public Function getNyuukinFileTorikomiList(ByVal sender As Object _
                                                , ByVal strNyuukinTorikomiNo As String _
                                                ) As NyuukinFileTorikomiRecord

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getNyuukinFileTorikomiList", sender, strNyuukinTorikomiNo)

        'データアクセスクラス
        Dim clsDataAcc As New JhsNyuukinTorikomiDataAccess  'JHS入金ファイル取込
        '物件履歴データテーブル
        Dim tblNkFileTorikomi As DataTable
        '入金ファイル取込レコード
        Dim recNkFileTorikomi As New NyuukinFileTorikomiRecord 'JHS入金ファイル取込レコードクラス

        '新規登録の場合、未入力
        If strNyuukinTorikomiNo = String.Empty Then
            Return recNkFileTorikomi
            Exit Function
        End If

        tblNkFileTorikomi = clsDataAcc.getNkFileTorikomiTable(strNyuukinTorikomiNo)

        If tblNkFileTorikomi.Rows.Count > 0 Then
            ' 検索結果を格納用リストにセット
            recNkFileTorikomi = DataMappingHelper.Instance.getMapArray(Of NyuukinFileTorikomiRecord)(GetType(NyuukinFileTorikomiRecord), tblNkFileTorikomi)(0)
        End If

        Return recNkFileTorikomi
    End Function

    ''' <summary>
    ''' データ更新処理
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="recData">入金ファイル取込レコード</param>
    ''' <returns>処理成否(Boolean)</returns>
    ''' <remarks>データ更新判断用列挙体の値で、処理を分岐する</remarks>
    Public Function saveData(ByVal sender As Object, ByRef recData As NyuukinFileTorikomiRecord) As Boolean
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".saveData", _
                                            sender, _
                                            recData)
        'データアクセスクラス
        Dim clsDataAcc As New JhsNyuukinTorikomiDataAccess
        '排他用入金取込レコード
        Dim recHaita As New NyuukinFileTorikomiHaitaRecord
        '排他制御用SQL文
        Dim sqlHaita As String
        '排他チェック用パラメータの情報を格納するList(Of ParamRecord)
        Dim listHaita As New List(Of ParamRecord)
        'Updateに必要なSQL情報を自動生成するクラス
        Dim clsUpdMaker As New UpdateStringHelper
        '排他用データアクセスクラス
        Dim clsJbDataAcc As New JibanDataAccess
        '排他結果リスト
        Dim listHaitaResult As List(Of NyuukinFileTorikomiHaitaRecord)
        'SQL文自動生成インターフェイス
        Dim IFsqlMaker As ISqlStringHelper
        'SQL文
        Dim strSqlString As String = ""
        'パラメータレコードのリスト
        Dim listParam As New List(Of ParamRecord)
        '更新キー情報
        Dim strUpdateKeyInfo As String = ""

        Try
            'トランザクション制御を行う
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                With recData

                    '更新対象レコードがあれば排他チェック
                    If recData.NyuukinTorikomiUniqueNo <> Integer.MinValue Then '更新

                        'SQL文自動生成インターフェイスを使用し、データを更新
                        IFsqlMaker = New UpdateStringHelper

                        '画面情報から排他レコードに複製
                        RecordMappingHelper.Instance.CopyRecordData(recData, recHaita)

                        '排他チェック用SQL文自動生成
                        sqlHaita = clsUpdMaker.MakeHaitaSQLInfo(GetType(NyuukinFileTorikomiHaitaRecord), recHaita, listHaita)

                        ' 排他チェック実施
                        listHaitaResult = DataMappingHelper.Instance.getMapArray(Of NyuukinFileTorikomiHaitaRecord)(GetType(NyuukinFileTorikomiHaitaRecord), _
                                    clsDataAcc.CheckHaita(sqlHaita, listHaita))

                        If listHaitaResult.Count > 0 Then
                            Dim recHaitaError As NyuukinFileTorikomiHaitaRecord = listHaitaResult(0)
                            ' 排他チェックエラー
                            mLogic.CallHaitaErrorMessage(sender, recHaitaError.UpdLoginUserId, recHaitaError.UpdDatetime, "入金ファイル取込テーブル")
                            Return False
                        End If

                    Else '新規登録

                        'SQL文自動生成インターフェイスを使用し、データを登録
                        IFsqlMaker = New InsertStringHelper

                        '登録ログインユーザーを設定
                        .AddLoginUserId = recData.UpdLoginUserId
                        '登録日時を設定
                        .AddDatetime = DateTime.Now

                    End If


                    '更新用文字列とパラメータの作成
                    strSqlString = IFsqlMaker.MakeUpdateInfo(GetType(NyuukinFileTorikomiRecord), recData, listParam, GetType(NyuukinFileTorikomiRecord))

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

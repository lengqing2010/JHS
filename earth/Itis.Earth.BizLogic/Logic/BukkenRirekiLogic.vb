Imports System.Transactions
''' <summary>
''' 物件履歴用のロジッククラスです
''' </summary>
''' <remarks></remarks>
Public Class BukkenRirekiLogic
    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    'UtilitiesのMessegeLogicクラス
    Dim mLogic As New MessageLogic

    ''' <summary>
    ''' 物件履歴テーブルの情報を取得します
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strHosyousyoNo">保証書NO</param>
    ''' <returns>物件履歴レコードを格納したリスト ／ 0件の場合はNothing</returns>
    ''' <remarks>区分と保証書NOをKEYにして取得</remarks>
    Public Function getBukkenRirekiList(ByVal sender As Object, ByVal strKbn As String, ByVal strHosyousyoNo As String) As List(Of BukkenRirekiRecord)
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getBukkenRirekiList" _
                                            , sender _
                                            , strKbn _
                                            , strHosyousyoNo)
        'データアクセスクラス
        Dim clsDataAcc As New BukkenRirekiDataAccess
        '物件履歴データテーブル
        Dim tblBukkenRireki As BukkenRirekiDataSet.BukkenRirekiTableDataTable
        '物件履歴レコード
        Dim recBukkenRireki As New BukkenRirekiRecord
        '物件履歴レコードリスト
        Dim listBukkenRireki As List(Of BukkenRirekiRecord)

        Try
            tblBukkenRireki = clsDataAcc.getBukkenRirekiTable(strKbn, strHosyousyoNo)

            If tblBukkenRireki.Count > 0 Then
                listBukkenRireki = DataMappingHelper.Instance.getMapArray(Of BukkenRirekiRecord)(GetType(BukkenRirekiRecord), tblBukkenRireki)
            Else
                listBukkenRireki = Nothing
            End If

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = -2 Then  'エラーキャッチ：タイムアウト
                mLogic.AlertMessage(sender, Messages.MSG086E, 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            listBukkenRireki = Nothing
        End Try

        Return listBukkenRireki
    End Function

    ''' <summary>
    ''' 物件履歴テーブルの情報をPKで取得します
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strHosyousyoNo">保証書NO</param>
    ''' <param name="strNyuuryokuNo">入力NO</param>
    ''' <returns>物件履歴レコード ／ 0件の場合はNothing</returns>
    ''' <remarks>PKで物件履歴テーブルの1レコードを取得</remarks>
    Public Function getBukkenRirekiRecord(ByVal strkbn As String, ByVal strHosyousyoNo As String, ByVal strNyuuryokuNo As String) As BukkenRirekiRecord
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getBukkenRirekiRecord" _
                                            , strkbn _
                                            , strHosyousyoNo _
                                            , strNyuuryokuNo)
        'データアクセスクラス
        Dim clsDataAcc As New BukkenRirekiDataAccess
        '物件履歴データテーブル
        Dim tblBukkenRireki As BukkenRirekiDataSet.BukkenRirekiTableDataTable
        '物件履歴レコード
        Dim recBukkenRireki As New BukkenRirekiRecord

        Dim intNyuuryokuNo As Integer = -1
        If strNyuuryokuNo <> String.Empty Then
            intNyuuryokuNo = Integer.Parse(strNyuuryokuNo)
        End If
        tblBukkenRireki = clsDataAcc.getBukkenRirekiTable(strkbn, strHosyousyoNo, intNyuuryokuNo)

        If tblBukkenRireki.Count > 0 Then
            recBukkenRireki = DataMappingHelper.Instance.getMapArray(Of BukkenRirekiRecord)(GetType(BukkenRirekiRecord), tblBukkenRireki)(0)
        End If

        Return recBukkenRireki
    End Function

    ''' <summary>
    ''' データ更新処理
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="recBukkenRireki">物件履歴レコード</param>
    ''' <param name="blnTorikesi">取消処理判断フラグ</param>
    ''' <returns>処理成否(Boolean)</returns>
    ''' <remarks>データ更新判断用列挙体の値で、処理を分岐する</remarks>
    Public Function saveData(ByVal sender As Object, ByRef recBukkenRireki As BukkenRirekiRecord, Optional ByVal blnTorikesi As Boolean = False) As Boolean
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".saveData", _
                                            sender, _
                                            recBukkenRireki, _
                                            blnTorikesi)

        '排他用物件履歴レコード
        Dim recHaita As New BukkenRirekiHaitaRecord
        '排他制御用SQL文
        Dim sqlHaita As String
        '排他チェック用パラメータの情報を格納するList(Of ParamRecord)
        Dim listHaita As New List(Of ParamRecord)
        'Updateに必要なSQL情報を自動生成するクラス
        Dim clsUpdMaker As New UpdateStringHelper
        '排他用データアクセスクラス
        Dim clsJbDataAcc As New JibanDataAccess
        '排他結果リスト
        Dim listHaitaResult As List(Of BukkenRirekiHaitaRecord)
        'SQL文自動生成インターフェイス
        Dim IFsqlMaker As ISqlStringHelper
        'SQL文
        Dim strSqlString As String = ""
        'パラメータレコードのリスト
        Dim listParam As New List(Of ParamRecord)

        Dim recTmp As New BukkenRirekiRecord

        Try
            'トランザクション制御を行う
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                With recBukkenRireki

                    '更新対象レコードがあれば更新、なければ登録
                    If .NyuuryokuNo <> Integer.MinValue Then

                        '画面情報から更新履歴レコードを複製
                        RecordMappingHelper.Instance.CopyRecordData(recBukkenRireki, recHaita)

                        '排他チェック用SQL文自動生成
                        sqlHaita = clsUpdMaker.MakeHaitaSQLInfo(GetType(BukkenRirekiHaitaRecord), recHaita, listHaita)

                        ' 排他チェック実施
                        listHaitaResult = DataMappingHelper.Instance.getMapArray(Of BukkenRirekiHaitaRecord)(GetType(BukkenRirekiHaitaRecord), _
                                    clsJbDataAcc.CheckHaita(sqlHaita, listHaita))

                        If listHaitaResult.Count > 0 Then
                            Dim recHaitaError As BukkenRirekiHaitaRecord = listHaitaResult(0)
                            ' 排他チェックエラー
                            mLogic.CallHaitaErrorMessage(sender, recHaitaError.UpdLoginUserId, recHaitaError.UpdDatetime, "物件履歴テーブル")
                            Return False
                        End If

                        '最新のDB値を取得
                        recTmp = Me.getBukkenRirekiRecord(.Kbn, .HosyousyoNo, .NyuuryokuNo)

                        '登録日付チェック
                        Dim strTmpAddDate As String = IIf(recTmp.AddDatetime = Date.MinValue, "", Format(recTmp.AddDatetime, EarthConst.FORMAT_DATE_TIME_3))
                        'システム日付
                        Dim strNowDate As String = Format(Now.Date, EarthConst.FORMAT_DATE_TIME_3)

                        Dim strSyoriMei As String = String.Empty
                        If blnTorikesi Then
                            strSyoriMei = "取消"
                        Else
                            strSyoriMei = "更新"
                        End If
                        Dim strErrMsg As String = Messages.MSG144E.Replace("@PARAM1", "システム日付").Replace("@PARAM2", "登録日付").Replace("@PARAM3", strSyoriMei)

                        If strTmpAddDate <> strNowDate Then '更新時：登録日付=システム日付
                            ' 日付チェックエラー
                            mLogic.AlertMessage(sender, strErrMsg)
                            Return False
                        End If

                        'SQL文自動生成インターフェイスを使用し、データを更新
                        IFsqlMaker = New UpdateStringHelper

                        '更新種類により使用するレコードクラスを変更
                        If blnTorikesi Then
                            '取消フラグを立てる
                            .Torikesi = 1
                            '更新用文字列とパラメータの作成
                            strSqlString = IFsqlMaker.MakeUpdateInfo(GetType(BukkenRirekiTorikesiRecord), recBukkenRireki, listParam, GetType(BukkenRirekiRecord))
                        Else
                            '更新用文字列とパラメータの作成
                            strSqlString = IFsqlMaker.MakeUpdateInfo(GetType(BukkenRirekiRecord), recBukkenRireki, listParam, GetType(BukkenRirekiRecord))
                        End If

                        'DB反映処理
                        If Not clsJbDataAcc.UpdateJibanData(strSqlString, listParam) Then
                            Return False
                            Exit Function
                        End If
                    Else
                        '新規追加用スクリプトおよび実行
                        If Me.InsertBukkenRireki(recBukkenRireki) = False Then
                            Return False
                            Exit Function
                        End If

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

    ''' <summary>
    ''' 物件履歴データの登録処理
    ''' </summary>
    ''' <remarks></remarks>
    Public Function InsertBukkenRireki(ByRef recBukkenRireki As BukkenRirekiRecord) As Boolean
        '入力NO採番用
        Dim intNyuuryokuNo As Integer
        'データアクセスクラス
        Dim clsDataAcc As New BukkenRirekiDataAccess
        '排他用データアクセスクラス
        Dim clsJbDataAcc As New JibanDataAccess
        'SQL文自動生成インターフェイス
        Dim IFsqlMaker As ISqlStringHelper
        'SQL文
        Dim strSqlString As String = ""
        'パラメータレコードのリスト
        Dim listParam As New List(Of ParamRecord)

        With recBukkenRireki

            If .Kbn = "" Or .HosyousyoNo = "" Then
                Return False
                Exit Function
            End If
            '入力NOの最大値を取得
            intNyuuryokuNo = clsDataAcc.getMaxNo(.Kbn, .HosyousyoNo)
            .NyuuryokuNo = intNyuuryokuNo + 1

            '登録ログインユーザーを設定
            .AddLoginUserId = recBukkenRireki.UpdLoginUserId
            '登録日時を設定
            .AddDatetime = DateTime.Now

            'SQL文自動生成インターフェイスを使用し、データを登録
            IFsqlMaker = New InsertStringHelper

            '更新用文字列とパラメータの作成
            strSqlString = IFsqlMaker.MakeUpdateInfo(GetType(BukkenRirekiRecord), recBukkenRireki, listParam, GetType(BukkenRirekiRecord))

            'DB反映処理
            If Not clsJbDataAcc.UpdateJibanData(strSqlString, listParam) Then
                Return False
                Exit Function
            End If

        End With

        Return True
    End Function
End Class

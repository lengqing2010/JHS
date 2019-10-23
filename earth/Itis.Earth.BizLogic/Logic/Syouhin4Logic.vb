Imports System.Transactions
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI

''' <summary>
''' 商品4Logicクラス
''' </summary>
''' <remarks></remarks>
Public Class Syouhin4Logic

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    'UtilitiesのMessegeLogicクラス
    Dim mLogic As New MessageLogic

#Region "地盤データ更新"
    ''' <summary>
    ''' 地盤データを更新します。関連する邸別請求データの更新も行われます
    ''' </summary>
    ''' <param name="sender">クライアント スクリプト ブロックを登録するコントロール</param>
    ''' <param name="jibanRec">地盤レコード</param>
    ''' <returns>True:更新成功 False:エラーによる更新失敗</returns>
    ''' <remarks></remarks>
    Public Function SaveJibanData(ByVal sender As Object, _
                                  ByVal jibanRec As JibanRecord, _
                                  ByVal recCnt As Integer) As Boolean
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SaveJibanData", _
                                                    sender, _
                                                    jibanRec)


        ' Updateに必要なSQL情報を自動生成するクラス
        Dim upadteMake As New UpdateStringHelper
        ' 排他制御用SQL文
        Dim sqlString As String = ""
        ' Update文
        Dim updateString As String = ""
        ' 排他チェック用パラメータの情報を格納するList(Of ParamRecord)
        Dim haitaList As New List(Of ParamRecord)
        ' 更新用パラメータの情報を格納するList(Of ParamRecord)
        Dim list As New List(Of ParamRecord)
        ' 排他チェック用レコード作成
        Dim jibanHaitaRec As New JibanHaitaRecord
        ' 地盤レコードの同一項目を複製
        RecordMappingHelper.Instance.CopyRecordData(jibanRec, jibanHaitaRec)
        ' 排他チェック用SQL文自動生成
        sqlString = upadteMake.MakeHaitaSQLInfo(GetType(JibanHaitaRecord), jibanHaitaRec, haitaList)
        ' 地盤データ用データアクセスクラス
        Dim dataAccess As JibanDataAccess = New JibanDataAccess
        ' 連携テーブルデータ登録用データの格納用
        Dim renkeiJibanRec As New JibanRenkeiRecord
        Dim renkeiTeibetuList As New List(Of TeibetuSeikyuuRenkeiRecord)
        '更新件数
        Dim intDBCnt As Integer = 0
        'UtilitiesのMessegeLogicクラス
        Dim mLogic As New MessageLogic
        'JibanLogicクラス
        Dim jibanLogic As New JibanLogic
        '邸別データ修正ロジッククラス
        Dim teiseiLogic As New TeibetuSyuuseiLogic

        Try
            ' 地盤テーブルと邸別請求の同期を保つため、トランザクション制御を行う
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                ' 排他チェック実施
                Dim haitaKekkaList As List(Of JibanHaitaRecord) = _
                            DataMappingHelper.Instance.getMapArray(Of JibanHaitaRecord)(GetType(JibanHaitaRecord), _
                            dataAccess.CheckHaita(sqlString, haitaList))

                If haitaKekkaList.Count > 0 Then
                    Dim haitaErrorData As JibanHaitaRecord = haitaKekkaList(0)

                    ' 排他チェックエラー
                    mLogic.CallHaitaErrorMessage(sender, _
                                                       haitaErrorData.UpdLoginUserId, _
                                                       haitaErrorData.UpdDatetime, _
                                                       "地盤テーブル")
                    Return False
                End If


                ' 【邸別請求データ】
                '現在の画面表示NOの最大値を取得
                Dim intMaxNo As Integer = Integer.Parse(dataAccess.GetTeibetuSeikyuuMaxNo(jibanRec.Kbn, _
                                                                                          jibanRec.HosyousyoNo, _
                                                                                          EarthConst.SOUKO_CD_SYOUHIN_4))
                ' 商品4
                Dim i As Integer
                Dim tmpTeibetuRec As New TeibetuSeikyuuRecord
                If Not jibanRec.Syouhin4Records Is Nothing Then
                    For i = 1 To recCnt '画面レコード分ループ
                        If jibanRec.Syouhin4Records.ContainsKey(i) = True Then
                            '作業用レコードクラスに格納
                            tmpTeibetuRec = jibanRec.Syouhin4Records.Item(i)

                            '商品コード
                            If tmpTeibetuRec.SyouhinCd = String.Empty Then '未入力
                                tmpTeibetuRec = Nothing '削除

                            Else '更新or追加

                                '追加
                                If tmpTeibetuRec.GamenHyoujiNo = Integer.MinValue Then
                                    '画面表示NOが存在しない（新規登録の場合）は取得最大値+1を新規採番
                                    tmpTeibetuRec.GamenHyoujiNo = (intMaxNo + 1)
                                    '次新規登録用に+1をしておく
                                    intMaxNo = intMaxNo + 1
                                End If
                            End If

                            ' 商品4の邸別請求データを更新します
                            If teiseiLogic.EditTeibetuRecord(sender, _
                                                            tmpTeibetuRec, _
                                                            jibanRec.Syouhin4Records.Item(i).BunruiCd, _
                                                            jibanRec.Syouhin4Records.Item(i).GamenHyoujiNo, _
                                                            jibanRec, _
                                                            renkeiTeibetuList _
                                                            ) = False Then
                                Return False
                            End If
                        End If
                    Next
                End If

                ' 邸別請求連携反映対象が存在する場合、反映を行う
                For Each renkeiTeibetuRec As TeibetuSeikyuuRenkeiRecord In renkeiTeibetuList
                    ' 連携用テーブルに反映する（邸別請求）
                    If dataAccess.EditTeibetuRenkeiData(renkeiTeibetuRec) <> 1 Then

                        ' 登録時エラー
                        mLogic.DbErrorMessage(sender, _
                                                    "登録", _
                                                    "邸別請求連携", _
                                                    String.Format(EarthConst.TEIBETU_KEY, _
                                                                 New String() {renkeiTeibetuRec.Kbn, _
                                                                               renkeiTeibetuRec.HosyousyoNo, _
                                                                               renkeiTeibetuRec.BunruiCd, _
                                                                               renkeiTeibetuRec.GamenHyoujiNo}))
                        ' 登録に失敗したので処理中断
                        Return False
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
#End Region

End Class
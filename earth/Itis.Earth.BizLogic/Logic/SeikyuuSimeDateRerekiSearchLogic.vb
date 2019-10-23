Imports System.Transactions

Public Class SeikyuuSimeDateRerekiSearchLogic

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    'UtilitiesのMessegeLogicクラス
    Dim mLogic As New MessageLogic

    '請求書Logic
    Private sLogic As New SeikyuuDataSearchLogic

    '更新日時保持用
    Dim pUpdDateTime As DateTime

    'エラーメッセージ格納変数
    Dim pErrMess As String = String.Empty

#Region "コントロール値"
    Private Const pStrInfoSeikyuusaki As String = "[請求先]"
    Private Const pStrInfoSeikyuusyoHakNengetu As String = "[請求年月日]"
    Private Const pStrInfoSimeDateRirekiContents As String = "【エラー内容】"
    Private Const pstrSeikyuusyoTorikesiErr As String = "■対象：[請求書締め日履歴テーブル]　■請求書NO：[@PARAM1]"
#End Region

#Region "請求書締め日履歴データ取得"
    ''' <summary>
    ''' 検索画面用請求書締め日履歴データを取得します
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="keyRec">請求書締め日履歴データKeyレコード</param>
    ''' <param name="startRow">開始行</param>
    ''' <param name="endRow">最終行</param>
    ''' <param name="allCount">全件数</param>
    ''' <returns>請求書締め日履歴データ検索用</returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuuSimeDateRirekiDataInfo(ByVal sender As Object, _
                                       ByVal keyRec As SeikyuuSimeDateRirekiKeyRecord, _
                                       ByVal startRow As Integer, _
                                       ByVal endRow As Integer, _
                                       ByRef allCount As Integer _
                                       ) As List(Of SeikyuuSimeDateRirekiRecord)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuuSimeDateRirekiDataInfo", _
                                            keyRec, _
                                            startRow, _
                                            endRow, _
                                            allCount _
                                            )

        '検索実行クラス
        Dim dataAccess As New SeikyuuSimeDateRerekiDataAccess
        '取得データ格納用リスト
        Dim list As New List(Of SeikyuuSimeDateRirekiRecord)

        Try
            '検索処理の実行
            Dim table As DataTable = dataAccess.GetSearchSeikyuuSimeDateRirekiTbl(keyRec)

            ' 総件数をセット
            allCount = table.Rows.Count

            ' 検索結果を格納用リストにセット
            list = DataMappingHelper.Instance.getMapArray(Of SeikyuuSimeDateRirekiRecord)(GetType(SeikyuuSimeDateRirekiRecord), table, startRow, endRow)

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
    ''' <param name="strSeikyuuSakiCd">請求先コード</param>
    ''' <param name="strSeikyuuSakiBrc">請求先枝番</param>
    ''' <param name="strSeikyuuSakiKbn">請求先区分</param>
    ''' <param name="dtSeikyuusyoHakNengetu">請求書発行年月</param>
    ''' <param name="strSeikyuuSimeDate">請求締め日</param>
    ''' <returns>DB更新用レコードを格納したリスト ／ 0件の場合はNothing</returns>
    ''' <remarks>請求先・請求年月日で該当テーブルのレコードを取得</remarks>
    Public Function GetSeikyuuSimeDateRirekiDataList(ByVal sender As Object, _
                                                     ByVal strSeikyuuSakiCd As String, _
                                                     ByVal strSeikyuuSakiBrc As String, _
                                                     ByVal strSeikyuuSakiKbn As String, _
                                                     ByVal dtSeikyuusyoHakNengetu As DateTime, _
                                                     ByVal strSeikyuuSimeDate As String _
                                                     ) As SeikyuuSimeDateRirekiRecord

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuuSimeDateRirekiDataRec", _
                                                    sender, _
                                                    strSeikyuuSakiCd, _
                                                    strSeikyuuSakiBrc, _
                                                    strSeikyuuSakiKbn, _
                                                    dtSeikyuusyoHakNengetu, _
                                                    strSeikyuuSimeDate _
                                                    )

        '検索実行クラス
        Dim dataAccess As New SeikyuuSimeDateRerekiDataAccess
        '検索結果格納用データテーブル
        Dim dTblResult As New DataTable
        '検索結果格納用レコード
        Dim recResult As New SeikyuuSimeDateRirekiRecord

        If strSeikyuuSakiCd = String.Empty _
            AndAlso strSeikyuuSakiBrc = String.Empty _
            AndAlso strSeikyuuSakiKbn = String.Empty _
            AndAlso dtSeikyuusyoHakNengetu = DateTime.MinValue Then
            'キー項目が不足している場合エラー
            Return recResult
        End If

        '検索処理の実行
        dTblResult = dataAccess.GetSeikyuuSimeDateRirekiSyuuseiList(strSeikyuuSakiCd, _
                                                                    strSeikyuuSakiBrc, _
                                                                    strSeikyuuSakiKbn, _
                                                                    dtSeikyuusyoHakNengetu, _
                                                                    strSeikyuuSimeDate _
                                                                   )
        If dTblResult.Rows.Count > 0 Then
            ' 検索結果を格納用リストにセット
            recResult = DataMappingHelper.Instance.getMapArray(Of SeikyuuSimeDateRirekiRecord)(GetType(SeikyuuSimeDateRirekiRecord), dTblResult)(0)
        End If

        Return recResult
    End Function

#End Region

#Region "請求書締日履歴データ更新"
    ''' <summary>
    ''' データ更新処理
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="listData">請求書締日履歴データのリスト</param>
    ''' <returns>処理可否(Boolean)</returns>
    ''' <remarks></remarks>
    Public Function saveData(ByVal sender As Object, _
                             ByRef listData As List(Of SeikyuuSimeDateRirekiRecord)) As Boolean
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".saveData", _
                                                    sender, _
                                                    listData _
                                                    )
        '排他用データアクセスクラス
        Dim clsJbDataAcc As New JibanDataAccess
        Dim recTmp As New SeikyuuSimeDateRirekiRecord           '作業用
        Dim recResult As New SeikyuuSimeDateRirekiRecord

        'SQL文自動生成インターフェイス
        Dim IFsqlMaker As ISqlStringHelper = New UpdateStringHelper '更新のみ
        'SQL文
        Dim strSqlString As String = ""
        'パラメータレコードのリスト
        Dim listParam As New List(Of ParamRecord)
        'レコード更新タイプを設定
        Dim recType As Type = GetType(SeikyuuSimeDateRirekiSyuuseiRecord)

        ' 請求鑑更新結果メッセージ
        Dim strResultMsg As String = String.Empty

        Try
            'トランザクション制御を行う
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                ' 更新日時取得（システム日時）
                pUpdDateTime = DateTime.Now

                For Each recTmp In listData

                    '*********************************
                    ' 請求書締め日履歴テーブルの取消
                    '*********************************
                    '更新対象レコードを取得
                    recResult = Me.GetSeikyuuSimeDateRirekiDataList(sender, _
                                                                     recTmp.SeikyuuSakiCd, _
                                                                     recTmp.SeikyuuSakiBrc, _
                                                                     recTmp.SeikyuuSakiKbn, _
                                                                     recTmp.SeikyuusyoHakNengetu, _
                                                                     recTmp.SeikyuuSimeDate _
                                                                     )

                    '最大履歴NOでない場合
                    If recResult.MaxRirekiNo <> 1 Then
                        mLogic.AlertMessage(sender, Me.SetErrMsg(recResult.SeikyuuSakiCd, _
                                                                 recResult.SeikyuuSakiBrc, _
                                                                 recResult.SeikyuuSakiKbn, _
                                                                 recResult.SeikyuusyoHakNengetu, _
                                                                 recResult.SeikyuuSimeDate, _
                                                                 Messages.MSG195E & Messages.MSG189E), 1)
                        Return False
                    End If

                    '更新対象レコードの請求書NOを取得
                    Dim strSeikyuusyoNo As String = recResult.SeikyuusyoNo

                    '●排他チェック
                    If recResult.UpdDatetime <> recTmp.UpdDatetime Then
                        ' 排他チェックエラー
                        mLogic.CallHaitaErrorMessage(sender, recResult.UpdLoginUserId, recResult.UpdDatetime, "請求書締め日履歴テーブル")
                        Return False
                    End If

                    '更新日時を設定
                    recTmp.UpdDatetime = pUpdDateTime

                    '更新用文字列とパラメータの作成
                    strSqlString = IFsqlMaker.MakeUpdateInfo(recType, recTmp, listParam, GetType(SeikyuuSimeDateRirekiRecord))

                    'DB反映処理
                    If Not clsJbDataAcc.UpdateJibanData(strSqlString, listParam) Then
                        Return False
                        Exit Function
                    End If

                    '*********************************
                    ' 請求鑑テーブルの取消
                    '*********************************
                    '請求締め日履歴.請求書NOがNULLでない場合
                    If strSeikyuusyoNo <> String.Empty Then

                        Dim recTmpKagami As SeikyuuDataRecord
                        '更新対象レコードを取得
                        recTmpKagami = sLogic.GetSeikyuuDataRec(sender, strSeikyuusyoNo)

                        '存在した場合
                        If recTmpKagami.SeikyuusyoNo <> String.Empty AndAlso Not recTmpKagami.SeikyuusyoNo Is Nothing Then
                            '●排他チェック
                            If recTmpKagami.UpdDatetime <> recTmp.SkUpdDatetime Then
                                ' 排他チェックエラー
                                mLogic.CallHaitaErrorMessage(sender, recTmpKagami.UpdLoginUserId, recTmpKagami.UpdDatetime, "請求鑑テーブル")
                                Return False
                            End If

                            strResultMsg = sLogic.UpdKagamiTorikesi(sender, strSeikyuusyoNo, recTmp.UpdLoginUserId)
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

#End Region

#Region "エラーメッセージ"
    ''' <summary>
    ''' エラー内容に記載するメッセージを生成する
    ''' </summary>
    ''' <param name="strSeikyuuSakiCd">請求先コード</param>
    ''' <param name="strSeikyuuSakiBrc">請求先枝番</param>
    ''' <param name="strSeikyuuSakiKbn">請求先区分</param>
    ''' <param name="dtSeikyuusyoHakNengetu">請求書発行年月</param>
    ''' <param name="strSeikyuuSimeDate">請求締め日</param>
    ''' <param name="strProcErrMsg">追加メッセージ</param>
    ''' <remarks></remarks>
    Public Function SetErrMsg(ByVal strSeikyuuSakiCd As String, _
                              ByVal strSeikyuuSakiBrc As String, _
                              ByVal strSeikyuuSakiKbn As String, _
                              ByVal dtSeikyuusyoHakNengetu As DateTime, _
                              ByVal strSeikyuuSimeDate As String, _
                              ByVal strProcErrMsg As String) As String
        Dim strErrMess As String = pErrMess '退避

        '請求先
        Dim strSeikyuuSaki As String = Me.GetDispSeikyuuSakiCd(strSeikyuuSakiKbn, strSeikyuuSakiCd, strSeikyuuSakiBrc, False)
        '請求年月日
        Dim strSeikyuusyoHakDate As String = Me.GetDisplayString(dtSeikyuusyoHakNengetu) & "/" & strSeikyuuSimeDate

        '[請求先]:該当の請求先<改行>
        pErrMess = pStrInfoSeikyuusaki & strSeikyuuSaki & EarthConst.CRLF_CODE
        '[請求年月日]:該当の請求年月日<改行><改行>
        pErrMess &= pStrInfoSeikyuusyoHakNengetu & strSeikyuusyoHakDate & EarthConst.CRLF_CODE & EarthConst.CRLF_CODE

        '【内容】<改行> + 処理メッセージ<改行>
        pErrMess &= pStrInfoSimeDateRirekiContents & EarthConst.CRLF_CODE & _
                        strProcErrMsg & EarthConst.CRLF_CODE

        Return pErrMess
    End Function

    ''' <summary>
    ''' エラー内容に記載するメッセージを生成する
    ''' </summary>
    ''' <param name="strSeikyuusyoNo">請求書NO</param>
    ''' <remarks></remarks>
    Public Function SetErrMsg(ByVal strSeikyuusyoNo As String) As String

        'エラーメッセージ
        Dim strErrMess As String

        '<改行>■対象：[請求書締め日履歴テーブル]　■請求書NO：該当の請求書NO<改行>
        strErrMess = EarthConst.CRLF_CODE & pstrSeikyuusyoTorikesiErr & EarthConst.CRLF_CODE
        strErrMess = strErrMess.Replace("@PARAM1", strSeikyuusyoNo)

        Return strErrMess
    End Function

    ''' <summary>
    ''' 請求先区分、請求先コード、請求先枝番の表記文字列を返す
    ''' </summary>
    ''' <param name="strSeikyuuSakiKbn">請求先区分</param>
    ''' <param name="strSakiCd">請求先コード</param>
    ''' <param name="strSakiBrc">請求先枝番</param>
    ''' <param name="blnBlank">ブランクとして表示するかフラグ(True：ブランク表示 False："＆nbsp;"表示)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDispSeikyuuSakiCd(ByVal strSeikyuuSakiKbn As String, ByVal strSakiCd As String, ByVal strSakiBrc As String, Optional ByVal blnBlank As Boolean = False) As String
        Const KAMEITEN As String = "加:"    '加盟店
        Const TYSKAISYA As String = "調:"   '調査会社
        Const HYPHEN As String = " - "      'ハイフン
        Const SPACE As String = EarthConst.HANKAKU_SPACE    '半角スペース

        Dim strRet As String = String.Empty

        '請求先区分
        If strSeikyuuSakiKbn = String.Empty Then
            strRet = String.Empty
        Else
            If strSeikyuuSakiKbn = "0" Then
                strRet = KAMEITEN
            ElseIf strSeikyuuSakiKbn = "1" Then
                strRet = TYSKAISYA
            Else
                strRet = String.Empty
            End If
        End If

        '請求先コード、請求先枝番
        If strSakiCd = String.Empty Or strSakiBrc = String.Empty Then
            If blnBlank = False Then
                strRet = SPACE
            Else
                strRet = String.Empty
            End If
        Else
            '連結
            strRet &= strSakiCd & HYPHEN & strSakiBrc
        End If

        Return strRet
    End Function

    ''' <summary>
    ''' 画面表示用にオブジェクトを文字列変換する
    ''' </summary>
    ''' <param name="obj">表示対象のデータ</param>
    ''' <param name="str">未設定時の初期値</param>
    ''' <returns>表示形式の文字列</returns>
    ''' <remarks>
    ''' DateTime : MinDateTimeを空白、それ以外は入力値を YYYY/MM/DD 形式のString型で返却<br/>
    ''' </remarks>
    Public Function GetDisplayString(ByVal obj As Object, _
                                     Optional ByVal str As String = "") As String

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(".GetDisplayString", _
                                                    obj, _
                                                    str)

        ' 戻り値となるStringデータ
        Dim ret As String = str

        If obj Is Nothing Then
            ' NULL は基本的に空白を返す
            Return ret
        ElseIf obj.GetType().ToString() = GetType(DateTime).ToString Then
            ' DateTimeの場合
            If obj = DateTime.MinValue Then
                Return ret
            Else
                ret = Format(obj, EarthConst.FORMAT_DATE_TIME_8)
            End If
        Else
            ret = obj.ToString()
        End If

        Return ret

    End Function

#End Region

End Class

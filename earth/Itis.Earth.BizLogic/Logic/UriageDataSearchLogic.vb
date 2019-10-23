Imports System.Transactions
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI

Public Class UriageDataSearchLogic

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    'UtilitiesのMessegeLogicクラス
    Dim mLogic As New MessageLogic

    'UtilitiesのStringLogicクラス
    Dim sLogic As New StringLogic

#Region "売上データ取得"
    ''' <summary>
    ''' 検索画面用売上データを取得します
    ''' </summary>
    ''' <param name="keyRec">売上データKeyレコード</param>
    ''' <param name="startRow">開始行</param>
    ''' <param name="endRow">最終行</param>
    ''' <param name="allCount">全件数</param>
    ''' <returns>売上データ検索用レコードのList(Of UriageDataRecord)</returns>
    ''' <remarks></remarks>
    Public Function GetUriageDataInfo(ByVal sender As Object, _
                                       ByVal keyRec As UriageDataKeyRecord, _
                                       ByVal startRow As Integer, _
                                       ByVal endRow As Integer, _
                                       ByRef allCount As Integer) As List(Of UriageSearchResultRecord)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetUriageDataInfo", _
                                            keyRec, _
                                            startRow, _
                                            endRow, _
                                            allCount)

        '検索実行クラス
        Dim dataAccess As New UriageDataAccess
        '取得データ格納用リスト
        Dim list As New List(Of UriageSearchResultRecord)

        Try
            '検索処理の実行
            Dim table As DataTable = dataAccess.GetUriageDataInfo(keyRec)

            ' 総件数をセット
            allCount = table.Rows.Count

            ' 検索結果を格納用リストにセット
            list = DataMappingHelper.Instance.getMapArray(Of UriageSearchResultRecord)(GetType(UriageSearchResultRecord), table, startRow, endRow)

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
    ''' 売上データテーブルの情報をPKで取得します
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="strDenpyouUnqNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUriageDataRec(ByVal sender As Object, ByVal strDenpyouUnqNo As String) As UriageDataRecord
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetUriageDataRec", sender, strDenpyouUnqNo)

        'データアクセスクラス
        Dim clsDataAcc As New UriageDataAccess
        '検索結果格納用データテーブル
        Dim dTblResult As New DataTable
        '検索結果格納用レコードリスト
        Dim recResult As New UriageDataRecord

        If strDenpyouUnqNo = String.Empty Then
            Return recResult
        End If

        dTblResult = clsDataAcc.GetUriageDataRec(strDenpyouUnqNo)

        If dTblResult.Rows.Count > 0 Then
            ' 検索結果を格納用リストにセット
            recResult = DataMappingHelper.Instance.getMapArray(Of UriageDataRecord)(GetType(UriageDataRecord), dTblResult)(0)
        End If
        Return recResult

    End Function

    ''' <summary>
    ''' CSV出力用売上データを取得します
    ''' </summary>
    ''' <param name="keyRec">売上データKeyレコード</param>
    ''' <param name="allCount">全件数</param>
    ''' <returns>売上データCSV出力用データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetUriageDataCsv(ByVal sender As Object, _
                                       ByVal keyRec As UriageDataKeyRecord, _
                                       ByRef allCount As Integer) As DataTable
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetUriageDataCsv", _
                                            keyRec, _
                                            allCount)

        '検索実行クラス
        Dim dataAccess As New UriageDataAccess
        '取得データ格納用リスト
        Dim list As New List(Of UriageDataRecord)

        Dim dtRet As New DataTable

        Try
            '検索処理の実行
            dtRet = dataAccess.GetUriageDataCsv(keyRec)

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

#Region "商品情報取得"
    ''' <summary>
    ''' 商品コードの情報をSyouhinMeisaiRecordクラスのList(Of SyouhinMeisaiRecord)で取得します
    ''' </summary>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <param name="strSyouhinNm">商品名</param>
    ''' <param name="allCount">検索時の取得全件数</param>
    ''' <param name="startRow">（任意）データ抽出時の開始行(1件目は1を指定)Default:1</param>
    ''' <param name="endRow">（任意）データ抽出時の終了行 Default:99999999</param>
    ''' <returns>SyouhinMeisaiRecordクラスのList(Of SyouhinMeisaiRecord)</returns>
    ''' <remarks></remarks>
    Public Function GetSyouhinInfo(ByVal strSyouhinCd As String, _
                                 ByVal strSyouhinNm As String, _
                                 ByRef allCount As Integer, _
                                 Optional ByVal startRow As Integer = 1, _
                                 Optional ByVal endRow As Integer = 99999999) As List(Of SyouhinMeisaiRecord)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSyouhinInfo", _
                                            strSyouhinCd, _
                                            strSyouhinNm, _
                                            allCount, _
                                            startRow, _
                                            endRow)

        Dim dataAccess As New SyouhinDataAccess
        Dim list As List(Of SyouhinMeisaiRecord)

        Dim table As DataTable = dataAccess.GetSyouhinInfo(strSyouhinCd, strSyouhinNm, EarthEnum.EnumSyouhinKubun.AllSyouhin)

        ' 件数を設定
        allCount = table.Rows.Count

        ' データを取得し、List(Of SyouhinMeisaiRecord)に格納する
        list = DataMappingHelper.Instance.getMapArray(Of SyouhinMeisaiRecord)(GetType(SyouhinMeisaiRecord), _
                                                      table, _
                                                      startRow, _
                                                      endRow)
        Return list

    End Function
#End Region

#Region "請求先情報取得"
    ''' <summary>
    ''' 請求先コードの情報をSeikyuuSakiInfoRecordクラスのList(Of SeikyuuSakiInfoRecord)で取得します
    ''' </summary>
    ''' <param name="seikyuuSakiCd">請求先コード</param>
    ''' <param name="seikyuuSakiBrc">請求先枝番</param>
    ''' <param name="seikyuuSakiKbn">請求先区分</param>
    ''' <param name="seikyuuSakiMei">請求先名</param>
    ''' <param name="seikyuuSakiKana">請求先カナ</param>
    ''' <returns>SeikyuuSakiInfoRecordクラスのList</returns>
    ''' <remarks>請求先検索画面用</remarks>
    Public Function GetSeikyuuSakiInfo(ByVal seikyuuSakiCd As String, _
                                       ByVal seikyuuSakiBrc As String, _
                                       ByVal seikyuuSakiKbn As String, _
                                       ByVal seikyuuSakiMei As String, _
                                       ByVal seikyuuSakiKana As String, _
                                       ByRef allCount As Integer, _
                                       Optional ByVal startRow As Integer = 1, _
                                       Optional ByVal endRow As Integer = 99999999, _
                                       Optional ByVal blnTorikesi As Boolean = False) As List(Of SeikyuuSakiInfoRecord)
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuuSakiInfo", _
                                                    seikyuuSakiCd, _
                                                    seikyuuSakiBrc, _
                                                    seikyuuSakiKbn, _
                                                    seikyuuSakiMei, _
                                                    seikyuuSakiKana, _
                                                    allCount, _
                                                    startRow, _
                                                    endRow, _
                                                    blnTorikesi)
        Dim dataAccess As New UriageDataAccess
        Dim list As List(Of SeikyuuSakiInfoRecord)

        Dim table As DataTable = dataAccess.searchSeikyuuSakiInfo(seikyuuSakiCd, seikyuuSakiBrc, seikyuuSakiKbn, seikyuuSakiMei, seikyuuSakiKana, blnTorikesi)

        ' 件数を設定
        allCount = table.Rows.Count

        ' データを取得し、List(Of SyouhinMeisaiRecord)に格納する
        list = DataMappingHelper.Instance.getMapArray(Of SeikyuuSakiInfoRecord)(GetType(SeikyuuSakiInfoRecord), _
                                                      table, _
                                                      startRow, _
                                                      endRow)

        Return list

    End Function

    ''' <summary>
    ''' 請求先コードの情報をSeikyuuSakiInfoRecordクラスのList(Of SeikyuuSakiInfoRecord)で取得します[PK]
    ''' </summary>
    ''' <param name="seikyuuSakiCd">請求先コード</param>
    ''' <param name="seikyuuSakiBrc">請求先枝番</param>
    ''' <param name="seikyuuSakiKbn">請求先区分</param>
    ''' <param name="blnTorikesi">取消フラグ</param>
    ''' <returns>SeikyuuSakiInfoRecordクラスのList</returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuuSakiInfo(ByVal seikyuuSakiCd As String, _
                                       ByVal seikyuuSakiBrc As String, _
                                       ByVal seikyuuSakiKbn As String, _
                                       Optional ByVal blnTorikesi As Boolean = False _
                                       ) As List(Of SeikyuuSakiInfoRecord)
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuuSakiInfo", _
                                                    seikyuuSakiCd, _
                                                    seikyuuSakiBrc, _
                                                    seikyuuSakiKbn, _
                                                    blnTorikesi)

        Dim dataAccess As New UriageDataAccess
        Dim list As List(Of SeikyuuSakiInfoRecord)

        Dim table As DataTable = dataAccess.searchSeikyuuSakiInfo(seikyuuSakiCd, seikyuuSakiBrc, seikyuuSakiKbn, blnTorikesi)

        ' データを取得し、List(Of SyouhinMeisaiRecord)に格納する
        list = DataMappingHelper.Instance.getMapArray(Of SeikyuuSakiInfoRecord)(GetType(SeikyuuSakiInfoRecord), table)

        Return list

    End Function

    ''' <summary>
    ''' 請求先マスタの検索結果件数を取得します
    ''' </summary>
    ''' <param name="seikyuuSakiCd">請求先コード</param>
    ''' <param name="seikyuuSakiBrc">請求先枝番</param>
    ''' <param name="seikyuuSakiKbn">請求先区分</param>
    ''' <param name="seikyuuSakiMei">請求先名</param>
    ''' <param name="seikyuuSakiKana">請求先カナ</param>
    ''' <returns>請求先マスタ検索結果件数</returns>
    ''' <remarks>請求先検索画面用</remarks>
    Public Function GetSeikyuuSakiCnt(ByVal seikyuuSakiCd As String, _
                                       ByVal seikyuuSakiBrc As String, _
                                       ByVal seikyuuSakiKbn As String, _
                                       ByVal seikyuuSakiMei As String, _
                                       ByVal seikyuuSakiKana As String, _
                                       Optional ByVal blnTorikesi As Boolean = False) As Integer
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuuSakiInfo", _
                                                    seikyuuSakiCd, _
                                                    seikyuuSakiBrc, _
                                                    seikyuuSakiKbn, _
                                                    seikyuuSakiMei, _
                                                    seikyuuSakiKana, _
                                                    blnTorikesi)
        Dim dataAccess As New UriageDataAccess
        Dim intCnt As Integer = dataAccess.searchSeikyuuSakiCnt(seikyuuSakiCd, seikyuuSakiBrc, seikyuuSakiKbn, seikyuuSakiMei, seikyuuSakiKana, blnTorikesi)

        Return intCnt

    End Function
#End Region

#Region "加盟店請求先情報取得"
    ''' <summary>
    ''' 請求先のKey情報を加盟店コードと商品コードから取得する
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <param name="blnTorikesi">取消フラグ</param>
    ''' <returns>KameitenSeikyuuSakiInfoRecordクラスのList</returns>
    ''' <remarks></remarks>
    Public Function GetKameitenSeikyuuSakiKey(ByVal strKameitenCd As String, _
                                       ByVal strSyouhinCd As String, _
                                       Optional ByVal blnTorikesi As Boolean = False _
                                       ) As List(Of KameitenSeikyuuSakiInfoRecord)
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKameitenSeikyuuSakiKey", _
                                                    strKameitenCd, _
                                                    strSyouhinCd, _
                                                    blnTorikesi _
                                                    )

        Dim dataAccess As New UriageDataAccess
        Dim list As List(Of KameitenSeikyuuSakiInfoRecord)

        Dim table As DataTable = dataAccess.searchKameitenSeikyuuSakiInfo(strKameitenCd, strSyouhinCd, blnTorikesi)

        ' データを取得し、List(Of KameitenSeikyuuSakiInfoRecord)に格納する
        list = DataMappingHelper.Instance.getMapArray(Of KameitenSeikyuuSakiInfoRecord)(GetType(KameitenSeikyuuSakiInfoRecord), table)

        Return list
    End Function

    ''' <summary>
    ''' 請求先の情報を加盟店コードと商品コードから取得する
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <param name="blnTorikesi">取消フラグ</param>
    ''' <returns>SeikyuuSakiInfoRecordクラスのList</returns>
    ''' <remarks></remarks>
    Public Function GetKameitenSeikyuuSakiInfo(ByVal strKameitenCd As String, _
                                                ByVal strSyouhinCd As String, _
                                                Optional ByVal blnTorikesi As Boolean = False _
                                                ) As List(Of SeikyuuSakiInfoRecord)
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKameitenSeikyuuSakiInfo", _
                                                    strKameitenCd, _
                                                    strSyouhinCd, _
                                                    blnTorikesi _
                                                    )
        Dim recKameiten As KameitenSeikyuuSakiInfoRecord
        Dim retList As List(Of SeikyuuSakiInfoRecord)
        Dim dataAccess As New UriageDataAccess
        Dim dtResult As DataTable

        dtResult = dataAccess.searchSeikyuuSakiFromKameiten(strKameitenCd, strSyouhinCd, blnTorikesi)
        If dtResult.Rows.Count = 1 Then
            recKameiten = DataMappingHelper.Instance.getMapArray(Of KameitenSeikyuuSakiInfoRecord)(GetType(KameitenSeikyuuSakiInfoRecord), dtResult)(0)
            '請求先情報の取得
            retList = GetSeikyuuSakiInfo(recKameiten.SeikyuuSakiCd, recKameiten.SeikyuuSakiBrc, recKameiten.SeikyuuSakiKbn, blnTorikesi)
        Else
            retList = Nothing
        End If

        Return retList
    End Function
#End Region

#Region "売上データ更新処理"
    ''' <summary>
    ''' 売上レコードより売上データテーブルを更新する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="recUri">売上データレコード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveUriData(ByVal sender As Object, ByVal recUri As UriageDataRecord) As Boolean
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SaveUriData", sender, recUri)

        '排他用データアクセスクラス
        Dim clsJbDataAcc As New JibanDataAccess
        Dim recResult As New UriageDataRecord 'レコードクラス
        Dim updDateTime As DateTime     '更新日時
        Dim end_count As Integer = EarthConst.MAX_RESULT_COUNT
        Dim total_count As Integer = 0
        'SQL文自動生成インターフェイス
        Dim IFsqlMaker As ISqlStringHelper = New UpdateStringHelper '更新のみ
        'SQL文
        Dim strSqlString As String = ""
        'パラメータレコードのリスト
        Dim listParam As New List(Of ParamRecord)
        Dim recType As Type = GetType(UriageDataRecord)

        Try
            'トランザクション制御を行う
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                ' 更新日時取得（システム日時）
                updDateTime = DateTime.Now

                If recUri.DenUnqNo.ToString <> String.Empty Then

                    '更新対象のレコードを取得
                    recResult = Me.GetUriageDataRec(sender, recUri.DenUnqNo)

                    '排他チェック
                    If recResult.UpdDatetime <> recUri.UpdDatetime Then
                        ' 排他チェックエラー
                        mLogic.CallHaitaErrorMessage(sender, recResult.UpdLoginUserId, recResult.UpdDatetime, "売上データテーブル")
                        Return False
                    End If

                    '更新日時を設定
                    recUri.UpdDatetime = updDateTime
                    '伝票売上年月日
                    If recUri.DenUriDate = DateTime.MinValue Then
                        recUri.DenUriDate = recResult.DenUriDate
                    End If
                    '請求年月日
                    If recUri.SeikyuuDate = DateTime.MinValue Then
                        recUri.SeikyuuDate = recResult.SeikyuuDate
                    End If
                    '伝票番号
                    If recUri.DenNo = String.Empty Then
                        recUri.DenNo = Nothing
                    End If

                    '更新用文字列とパラメータの作成
                    strSqlString = IFsqlMaker.MakeUpdateInfo(recType, recUri, listParam, GetType(UriageDataRecord))

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

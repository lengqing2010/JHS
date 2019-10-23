Imports System.Transactions
Public Class SeikyuuSiireSakiHenkouLogic
    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' エラータイプ
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ErrType
        SUCCESS = 0
        HAITA = 1
        KOUSIN = 2
        RENKEI = 3
        EXCEPTION = 9
    End Enum

    Private mLogic As New MessageLogic

    ''' <summary>
    ''' 加盟店と商品を元に請求先情報を取得
    ''' </summary>
    ''' <param name="dicParams">画面パラメータ情報</param>
    ''' <returns>データテーブル</returns>
    ''' <remarks></remarks>
    Public Function getSyouhinSeikyuuSakiInfo(ByVal dicParams As Dictionary(Of String, String)) As SeikyuuSiireSakiHenkouRecord
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getSyouhinSeikyuuSiireSakiInfo", _
                                                    dicParams)

        Dim uriageDataAcc As New UriageDataAccess
        Dim uriageLogic As New UriageDataSearchLogic
        Dim dTbl As DataTable
        Dim recData As SeikyuuSiireSakiHenkouRecord
        Dim seikyuuSakiList As List(Of SeikyuuSakiInfoRecord)
        Dim recSeikyuuSaki As SeikyuuSakiInfoRecord = Nothing


        '加盟店に紐付く請求先を取得
        dTbl = uriageDataAcc.searchKameitenSeikyuuSakiInfo(dicParams("KameitenCd"), dicParams("SyouhinCd"))
        If dTbl.Rows.Count > 0 Then
            recData = DataMappingHelper.Instance.getMapArray(Of SeikyuuSiireSakiHenkouRecord)(GetType(SeikyuuSiireSakiHenkouRecord), dTbl)(0)
            '請求先情報を取得
            seikyuuSakiList = uriageLogic.GetSeikyuuSakiInfo(recData.SeikyuuSakiCd, recData.SeikyuuSakiBrc, recData.SeikyuuSakiKbn)
            If seikyuuSakiList.Count > 0 Then
                recSeikyuuSaki = seikyuuSakiList(0)
            Else
                recSeikyuuSaki = Nothing
            End If
        Else
            recData = New SeikyuuSiireSakiHenkouRecord
        End If

        If recSeikyuuSaki IsNot Nothing Then
            recData.SeikyuuSakiMei = recSeikyuuSaki.SeikyuuSakiMei
        Else
            recData.SeikyuuSakiMei = String.Empty
        End If

        Return recData

    End Function

    ''' <summary>
    ''' 画面の情報でデータベースを更新する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="recJbn">画面からデータを取得した地盤レコード(排他制御用)</param>
    ''' <param name="listDispTeibetuRec">画面からデータを取得した邸別請求レコードクラスリスト</param>
    ''' <param name="recCnt"></param>
    ''' <param name="intErr">エラー種別</param>
    ''' <returns>True:更新成功 False:エラーによる更新失敗</returns>
    ''' <remarks></remarks>
    Public Function saveData(ByVal sender As Object _
                                    , ByVal recJbn As JibanRecordBase _
                                    , ByVal listDispTeibetuRec As List(Of TeibetuSeikyuuRecordSeikyuuSiireSakiHenkou) _
                                    , ByRef recCnt As Integer _
                                    , ByRef intErr As ErrType) As String()
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".saveTeibetuData" _
                                                    , sender _
                                                    , recJbn _
                                                    , listDispTeibetuRec _
                                                    , recCnt _
                                                    , intErr)
        '排他制御用
        Dim recJibanHaita As New JibanHaitaRecord
        Dim strSql As String
        Dim upadteMake As New UpdateStringHelper
        Dim haitaList As New List(Of ParamRecord)
        Dim jbnDtAcc As New JibanDataAccess
        '邸別請求レコード更新用
        Dim strKbn As String
        Dim strBangou As String
        Dim strBunruiCd As String
        Dim strHyoujiNo As String
        Dim recUpdTeibetu As TeibetuSeikyuuRecordSeikyuuSiireSakiHenkou
        Dim newUpdateDatetime As DateTime = DateTime.Now
        Dim newUpdLoginUserId As String
        Dim teibetuLogic As New TeibetuSyuuseiLogic
        '連携処理用
        Dim recRenkeiJiban As New JibanRenkeiRecord
        Dim listRenkeiTeibetuRec As New List(Of TeibetuSeikyuuRenkeiRecord)
        'エラー処理用
        Dim strErrKey() As String = Nothing

        Try
            'トランザクション制御
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))
                '**************************************************************************
                '* 地盤データ(加盟店、調査会社、工事会社請求有無、等)が更新されると
                '* 画面の情報とDBの情報で不整合が発生する為、地盤データの排他制御を行なう
                '**************************************************************************
                ' 地盤レコードの同一項目を複製
                RecordMappingHelper.Instance.CopyRecordData(recJbn, recJibanHaita)

                ' 排他チェック用SQL文自動生成
                strSql = upadteMake.MakeHaitaSQLInfo(GetType(JibanHaitaRecord), recJibanHaita, haitaList)

                ' 排他チェック実施
                Dim haitaKekkaList As List(Of JibanHaitaRecord) = _
                            DataMappingHelper.Instance.getMapArray(Of JibanHaitaRecord)(GetType(JibanHaitaRecord), _
                            jbnDtAcc.CheckHaita(strSql, haitaList))

                If haitaKekkaList.Count > 0 Then
                    Dim haitaErrorData As JibanHaitaRecord = haitaKekkaList(0)

                    ' 排他チェックエラー
                    mLogic.CallHaitaErrorMessage(sender, haitaErrorData.UpdLoginUserId, haitaErrorData.UpdDatetime, "地盤テーブル")

                    strErrKey = New String() {recJbn.Kbn _
                                            , recJbn.HosyousyoNo}

                    intErr = ErrType.HAITA
                    Return strErrKey
                    Exit Function
                End If

                For recCnt = 0 To listDispTeibetuRec.Count - 1
                    '更新対象の邸別請求レコードを取得
                    recUpdTeibetu = listDispTeibetuRec(recCnt)
                    '***************************************
                    '* 画面データの現在レコードをDBから取得
                    '***************************************
                    'KEYの取得
                    strKbn = recUpdTeibetu.Kbn
                    strBangou = recUpdTeibetu.HosyousyoNo
                    strBunruiCd = recUpdTeibetu.BunruiCd
                    strHyoujiNo = recUpdTeibetu.GamenHyoujiNo

                    '更新者
                    newUpdLoginUserId = recUpdTeibetu.UpdLoginUserId

                    If Not teibetuLogic.EditTeibetuRecord(sender _
                                                        , recUpdTeibetu _
                                                        , recUpdTeibetu.BunruiCd _
                                                        , recUpdTeibetu.GamenHyoujiNo _
                                                        , recJbn _
                                                        , listRenkeiTeibetuRec _
                                                        , GetType(TeibetuSeikyuuRecordSeikyuuSiireSakiHenkou)) Then
                        strErrKey = New String() {recJbn.Kbn _
                                                , recJbn.HosyousyoNo _
                                                , recUpdTeibetu.BunruiCd _
                                                , recUpdTeibetu.GamenHyoujiNo}
                        intErr = ErrType.KOUSIN
                        '登録に失敗したので処理中断
                        Return strErrKey
                        Exit Function
                    End If

                    '邸別請求連携反映対象が存在する場合、反映を行う
                    For Each recRenkeiTeibetu As TeibetuSeikyuuRenkeiRecord In listRenkeiTeibetuRec
                        '連携用テーブルに反映する（邸別請求）
                        If jbnDtAcc.EditTeibetuRenkeiData(recRenkeiTeibetu) <> 1 Then
                            strErrKey = New String() {recRenkeiTeibetu.Kbn _
                                                    , recRenkeiTeibetu.HosyousyoNo _
                                                    , recRenkeiTeibetu.BunruiCd _
                                                    , recRenkeiTeibetu.GamenHyoujiNo}
                            intErr = ErrType.RENKEI
                            '登録に失敗したので処理中断
                            Return strErrKey
                            Exit Function
                        End If
                    Next
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
            intErr = ErrType.EXCEPTION
            Return strErrKey
        Catch exTransactionException As System.Transactions.TransactionException
            'エラーキャッチ：トランザクションエラー
            UnTrappedExceptionManager.Publish(exTransactionException)
            mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exTransactionException.ToString), 0, "TransactionException")
            intErr = ErrType.EXCEPTION
            Return strErrKey
        End Try

        intErr = ErrType.SUCCESS
        Return strErrKey

    End Function

End Class

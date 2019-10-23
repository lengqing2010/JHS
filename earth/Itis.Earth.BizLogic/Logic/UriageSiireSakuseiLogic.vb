Imports System.Transactions
Imports System.text
Imports System.Web.UI
''' <summary>
''' 売上・仕入データ作成に関する処理を行うロジッククラスです
''' </summary>
''' <remarks></remarks>
Public Class UriageSiireSakuseiLogic
    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    'UtilitiesのStringLogicクラス
    Private sl As New StringLogic
    'メッセージロジック
    Private MLogic As New MessageLogic
    Private DLogic As New DataLogic
    Private renkeiDataAccess As New RenkeiKanriDataAccess


#Region "列挙体"
    ''' <summary>
    ''' 出力ファイルタイプ列挙体
    ''' </summary>
    ''' <remarks></remarks>
    Enum enOutputFileType
        ''' <summary>
        ''' 売上調査
        ''' </summary>
        ''' <remarks></remarks>
        UriageTyousa = 0
        ''' <summary>
        ''' 売上工事
        ''' </summary>
        ''' <remarks></remarks>
        UriageKouji = 1
        ''' <summary>
        ''' 仕入調査
        ''' </summary>
        ''' <remarks></remarks>
        SiireTyousa = 2
        ''' <summary>
        ''' 仕入工事
        ''' </summary>
        ''' <remarks></remarks>
        SiireKouji = 3
        ''' <summary>
        ''' 売上その他
        ''' </summary>
        ''' <remarks></remarks>
        UriageSonota = 4
        ''' <summary>
        '''売上店別
        ''' </summary>
        ''' <remarks></remarks>
        UriageMiseuri = 5
        ''' <summary>
        ''' 売上割引
        ''' </summary>
        ''' <remarks></remarks>
        UriageWaribiki = 9
        ''' <summary>
        ''' 汎用売上
        ''' </summary>
        ''' <remarks></remarks>
        UriageHannyou = 6
        ''' <summary>
        ''' 汎用仕入
        ''' </summary>
        ''' <remarks></remarks>
        SiireHannyou = 7

    End Enum
    ''' <summary>
    ''' 出力データ項目バイト数列挙体
    ''' </summary>
    ''' <remarks></remarks>
    Enum enOutPutDataByte
        ''' <summary>
        ''' 得意先コード
        ''' </summary>
        ''' <remarks></remarks>
        TokuiSakiCd = 5
        ''' <summary>
        ''' 得意先名
        ''' </summary>
        ''' <remarks></remarks>
        TokuiSakiName = 40
        ''' <summary>
        ''' 先方担当者名
        ''' </summary>
        ''' <remarks></remarks>
        SenpouTantousyaName = 28
        ''' <summary>
        ''' 摘要名
        ''' </summary>
        ''' <remarks></remarks>
        TekiyouName = 30
        ''' <summary>
        ''' 品名
        ''' </summary>
        ''' <remarks></remarks>
        HinName = 36
        ''' <summary>
        ''' 備考
        ''' </summary>
        ''' <remarks></remarks>
        Bikou = 20
        ''' <summary>
        ''' 仕入先コード
        ''' </summary>
        ''' <remarks></remarks>
        SiireSakiCd = 5
        ''' <summary>
        ''' 仕入先名
        ''' </summary>
        ''' <remarks></remarks>
        SiireSakiName = 40
    End Enum
#End Region

#Region "ファイル種別定数"
    ''' <summary>
    ''' 売上-邸別（調査）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const FT_URIAGE_TYOUSA As String = "00"
    ''' <summary>
    ''' 売上-邸別（工事）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const FT_URIAGE_KOUJI As String = "10"
    ''' <summary>
    ''' 仕入-邸別（調査）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const FT_SIIRE_TYOUSA As String = "20"
    ''' <summary>
    ''' 仕入-邸別（工事）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const FT_SIIRE_KOUJI As String = "30"
    ''' <summary>
    ''' 売上-邸別（その他）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const FT_URIAGE_HOKA As String = "40"
    ''' <summary>
    ''' 売上-店別
    ''' </summary>
    ''' <remarks></remarks>
    Private Const FT_URIAGE_TENBETU As String = "50"
    ''' <summary>
    ''' 売上-店別（割引）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const FT_URIAGE_TENBETU_WARIBIKI As String = "51"
#End Region

#Region "プロパティ"
#Region "ログインユーザーID"
    ''' <summary>
    ''' ログインユーザーID
    ''' </summary>
    ''' <remarks></remarks>
    Private strLoginUserId As String
    ''' <summary>
    ''' ログインユーザーID
    ''' </summary>
    ''' <value></value>
    ''' <returns> ログインユーザーID</returns>
    ''' <remarks></remarks>
    Public Property LoginUserId() As String
        Get
            Return strLoginUserId
        End Get
        Set(ByVal value As String)
            strLoginUserId = value
        End Set
    End Property
#End Region
#Region "更新日時"
    ''' <summary>
    ''' 更新日時
    ''' </summary>
    ''' <remarks></remarks>
    Private dateUpdDatetime As String
    ''' <summary>
    ''' 更新日時
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新日時</returns>
    ''' <remarks></remarks>
    Public Property UpdDatetime() As DateTime
        Get
            Return dateUpdDatetime
        End Get
        Set(ByVal value As DateTime)
            dateUpdDatetime = value
        End Set
    End Property
#End Region
#Region "画面情報の取得用Setterのみ"
#Region "売上年月日FROM"
    ''' <summary>
    ''' 売上年月日FROM
    ''' </summary>
    ''' <remarks></remarks>
    Private strUriageFrom As String
    ''' <summary>
    ''' 画面から取得用 for 売上年月日FROM
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property AccUriageFrom() As String
        Set(ByVal value As String)
            strUriageFrom = value
        End Set
    End Property
#End Region
#Region "売上年月日TO"
    ''' <summary>
    ''' 売上年月日TO
    ''' </summary>
    ''' <remarks></remarks>
    Private strUriageTo As String
    ''' <summary>
    ''' 画面から取得用 for 売上年月日TO
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property AccUriageTo() As String
        Set(ByVal value As String)
            strUriageTo = value
        End Set
    End Property
#End Region
#Region "伝票NO"
    ''' <summary>
    ''' 伝票NO
    ''' </summary>
    ''' <remarks></remarks>
    Private intDenpyouNo As Integer
    ''' <summary>
    ''' 画面から取得用 for 伝票NO
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property AccDenpyouNo() As Integer
        Set(ByVal value As Integer)
            intDenpyouNo = value
        End Set
    End Property
#End Region
#End Region
#Region "出力ファイルタイプ"
    ''' <summary>
    ''' 出力ファイルタイプ
    ''' </summary>
    ''' <remarks></remarks>
    Private enFileType As enOutputFileType
    ''' <summary>
    ''' 出力ファイルタイプ
    ''' </summary>
    ''' <value></value>
    ''' <returns>出力ファイルタイプ</returns>
    ''' <remarks></remarks>
    Public Property AccFileType() As enOutputFileType
        Get
            Return enFileType
        End Get
        Set(ByVal value As enOutputFileType)
            enFileType = value
        End Set
    End Property
#End Region
#Region "出力文字列"
    ''' <summary>
    ''' 出力文字列
    ''' </summary>
    ''' <remarks></remarks>
    Private strOutPutString As String
    ''' <summary>
    '''出力文字列
    ''' </summary>
    ''' <value></value>
    ''' <returns>出力文字列</returns>
    ''' <remarks></remarks>
    Public Property OutPutString() As String
        Get
            Return strOutPutString
        End Get
        Set(ByVal value As String)
            strOutPutString = value
        End Set
    End Property
#End Region
#End Region

#Region "初期処理"
    ''' <summary>
    ''' 最終更新情報を取得します
    ''' </summary>
    ''' <param name="intDenpyouType">伝票種別</param>
    ''' <returns>伝票レコード格納クラス</returns>
    ''' <remarks></remarks>
    Public Function GetLastUpdDate(ByVal intDenpyouType As Integer) As DenpyouRecord
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetLastUpdDate" _
                                                    , intDenpyouType)
        Dim clsRec As New DenpyouRecord
        Dim clsDataAccess As New UriageSiireSakuseiDataAccess
        Dim lastUpdDenpyouTable As DenpyouDataSet.LastUpdJouhouDataTable
        Dim lastUpdRow As DenpyouDataSet.LastUpdJouhouRow

        lastUpdDenpyouTable = clsDataAccess.GetLastUpdDateDenpyouNo(intDenpyouType)
        If lastUpdDenpyouTable.Rows.Count > 0 Then
            lastUpdRow = lastUpdDenpyouTable.Rows(0)
            clsRec = DataMappingHelper.Instance.getMapArray(Of DenpyouRecord)(GetType(DenpyouRecord), lastUpdDenpyouTable).Item(0)
        End If
        Return clsRec
    End Function
#End Region

#Region "データ作成処理"
    ''' <summary>
    ''' 指定された伝票種別の伝票NOを０に初期化します
    ''' </summary>
    ''' <param name="strDenpyouType">伝票種別</param>
    ''' <returns>更新件数</returns>
    ''' <remarks></remarks>
    Public Function ResetDenpyouNo(ByVal strDenpyouType As String) As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ResetDenpyouNo" _
                                                    , strDenpyouType)
        Dim clsDataAccess As New UriageSiireSakuseiDataAccess
        Dim intResult As Integer
        Dim lastUpdDenpyouTable As DenpyouDataSet.LastUpdJouhouDataTable
        Dim lastUpdRow As DenpyouDataSet.LastUpdJouhouRow
        Dim intLockDenpyouNo As Integer
        Dim strRetMsg As String

        '伝票NOの排他制御
        lastUpdDenpyouTable = clsDataAccess.GetLastUpdDateDenpyouNo(enFileType, True)
        If lastUpdDenpyouTable.Rows.Count > 0 Then
            lastUpdRow = lastUpdDenpyouTable.Rows(0)
            intLockDenpyouNo = lastUpdRow.saisyuu_denpyou_no
        End If
        If intDenpyouNo <> intLockDenpyouNo Then
            strRetMsg = Messages.MSG075E
            Return strRetMsg
            Exit Function
        End If

        intResult = clsDataAccess.ResetDenpyouNo(strDenpyouType, strLoginUserId, UpdDatetime)

        If intResult = 0 Then
            strRetMsg = String.Format(Messages.MSG150E, "伝票")
            Return strRetMsg
            Exit Function
        End If

        strRetMsg = ""
        Return strRetMsg
    End Function

    ''' <summary>
    ''' データ作成処理を行います
    ''' </summary>
    ''' <returns>処理結果メッセージ</returns>
    ''' <remarks></remarks>
    Public Function MakeData() As String
        Dim clsDataAccess As New UriageSiireSakuseiDataAccess
        Dim strRetMsg As String = String.Empty
        Dim listCsvRecUriage As New List(Of UriageDataCsvRecord)
        Dim listCsvRecSiire As New List(Of SiireDataCsvRecord)
        Dim lastUpdDenpyouTable As DenpyouDataSet.LastUpdJouhouDataTable
        Dim lastUpdRow As DenpyouDataSet.LastUpdJouhouRow
        Dim intLockDenpyouNo As Integer
        Dim cmdTextSb As New StringBuilder()
        Dim strFileType As String = String.Empty
        Dim intResult As Integer
        Dim intResultUpdate As Integer

        Try
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))
                '伝票NOの排他制御
                lastUpdDenpyouTable = clsDataAccess.GetLastUpdDateDenpyouNo(enFileType, True)
                If lastUpdDenpyouTable.Rows.Count > 0 Then
                    lastUpdRow = lastUpdDenpyouTable.Rows(0)
                    intLockDenpyouNo = lastUpdRow.saisyuu_denpyou_no
                End If
                If intDenpyouNo <> intLockDenpyouNo Then
                    strRetMsg = Messages.MSG075E
                    Return strRetMsg
                    Exit Function
                End If

                'ファイル種別判定
                Select Case enFileType
                    Case enOutputFileType.UriageTyousa
                        '売上調査
                        strFileType = FT_URIAGE_TYOUSA
                    Case enOutputFileType.UriageKouji
                        '売上工事
                        strFileType = FT_URIAGE_KOUJI
                    Case enOutputFileType.UriageSonota
                        '売上その他
                        strFileType = FT_URIAGE_HOKA
                    Case enOutputFileType.UriageMiseuri
                        '売上店別
                        strFileType = FT_URIAGE_TENBETU
                    Case enOutputFileType.SiireTyousa
                        '仕入調査
                        strFileType = FT_SIIRE_TYOUSA
                    Case enOutputFileType.SiireKouji
                        '仕入工事
                        strFileType = FT_SIIRE_KOUJI
                    Case Else
                        '処理対象データが存在しなかった場合
                        strRetMsg = Messages.MSG020E
                        Return strRetMsg
                        Exit Function
                End Select
                'ダウンロードデータテーブルを削除
                intResult = clsDataAccess.DeleteDownLoadTable(strFileType)
                'CSV出力用データを取得
                Select Case enFileType
                    Case enOutputFileType.UriageTyousa _
                    , enOutputFileType.UriageKouji _
                    , enOutputFileType.UriageSonota _
                    , enOutputFileType.UriageMiseuri
                        '売上CSV出力用データを取得
                        listCsvRecUriage = GetUriageOutPutData()
                        '売上固有処理
                        intResultUpdate = 0
                        strOutPutString = UriageUniqueSyori(listCsvRecUriage, strFileType, intResultUpdate)
                    Case enOutputFileType.SiireTyousa _
                    , enOutputFileType.SiireKouji
                        '仕入CSV出力用データを取得
                        listCsvRecSiire = GetSiireOutPutDate()
                        '仕入固有処理
                        strOutPutString = SiireUniqueSyori(listCsvRecSiire, strFileType, intResultUpdate)
                    Case Else
                        '処理対象データが存在しなかった場合
                        strRetMsg = Messages.MSG020E
                        Return strRetMsg
                        Exit Function
                End Select

                '伝票マスタの更新
                intResult = clsDataAccess.UpdateDenpyouNo(enFileType, intDenpyouNo, strLoginUserId, UpdDatetime)
                ' 更新に成功した場合、トランザクションをコミットする
                scope.Complete()
            End Using

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = -2 Then 'エラーキャッチ：タイムアウト
                strRetMsg = Messages.MSG086E & sl.RemoveSpecStr(exSqlException.Message)
            Else
                strRetMsg = Messages.MSG117E & sl.RemoveSpecStr(exSqlException.Message)
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            Return strRetMsg
            Exit Function
        Catch exTransactionException As System.Transactions.TransactionException
            strRetMsg = Messages.MSG117E & sl.RemoveSpecStr(exTransactionException.Message)
            UnTrappedExceptionManager.Publish(exTransactionException)
            Return strRetMsg
            Exit Function
        Catch ex As Exception
            strRetMsg = Messages.MSG118E & sl.RemoveSpecStr(ex.Message)
            UnTrappedExceptionManager.Publish(ex)
            Return strRetMsg
            Exit Function
        End Try

        If strOutPutString <> String.Empty Or intResultUpdate <> 0 Then
            strRetMsg = Messages.MSG018S.Replace("@PARAM1", "売上・仕入データ確定")
        Else
            '処理対象データが存在しなかった場合
            strRetMsg = Messages.MSG020E
        End If

        Return strRetMsg
    End Function

    ''' <summary>
    ''' 売上CSV出力用データを取得します
    ''' </summary>
    ''' <returns>売上CSV出力用データを格納したリスト</returns>
    ''' <remarks></remarks>
    Private Function GetUriageOutPutData() As List(Of UriageDataCsvRecord)
        Dim clsDataAccess As New UriageSiireSakuseiDataAccess
        Dim listCsvRec As New List(Of UriageDataCsvRecord)
        Dim csvRec As New UriageDataCsvRecord
        Dim csvTable As DataTable
        Dim dtFrom As DateTime
        Dim dtTo As DateTime

        dtFrom = DateTime.Parse(strUriageFrom)
        dtTo = DateTime.Parse(strUriageTo)
        'タイプによって出力内容を変更
        Select Case enFileType
            Case enOutputFileType.UriageTyousa
                '売上調査
                '対象データをロック
                Dim ret As Integer = clsDataAccess.GetUriageTyousaOutPutDataLock(dtFrom, dtTo)
                '参照用のスコープを用意(トランザクション外とする)
                Using subScope As TransactionScope = New TransactionScope(TransactionScopeOption.Suppress, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))
                    'CSV出力用データ取得
                    csvTable = clsDataAccess.GetUriageTyousaOutPutData(dtFrom, dtTo, intDenpyouNo)
                    'スコープを閉じる
                    subScope.Complete()
                End Using
            Case enOutputFileType.UriageKouji
                '売上工事
                '対象データをロック
                Dim ret As Integer = clsDataAccess.GetUriageKoujiOutPutDataLock(dtFrom, dtTo)
                '参照用のスコープを用意(トランザクション外とする)
                Using subScope As TransactionScope = New TransactionScope(TransactionScopeOption.Suppress, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))
                    'CSV出力用データ取得
                    csvTable = clsDataAccess.GetUriageKoujiOutPutData(dtFrom, dtTo, intDenpyouNo)
                    'スコープを閉じる
                    subScope.Complete()
                End Using
            Case enOutputFileType.UriageSonota
                '売上その他
                csvTable = clsDataAccess.GetUriageHokaOutPutData(dtFrom, dtTo, intDenpyouNo)
            Case enOutputFileType.UriageMiseuri
                '売上店別
                csvTable = clsDataAccess.GetUriageTenbetuOutPutData(dtFrom, dtTo, intDenpyouNo)
            Case Else
                csvTable = Nothing
        End Select

        listCsvRec = DataMappingHelper.Instance.getMapArray(Of UriageDataCsvRecord)(GetType(UriageDataCsvRecord), csvTable)

        Return listCsvRec
    End Function

    ''' <summary>
    ''' 仕入CSV出力用データを取得します
    ''' </summary>
    ''' <returns>仕入CSV出力用データを格納したリスト</returns>
    ''' <remarks></remarks>
    Private Function GetSiireOutPutDate() As List(Of SiireDataCsvRecord)
        Dim clsDataAccess As New UriageSiireSakuseiDataAccess
        Dim listCsvRec As New List(Of SiireDataCsvRecord)
        Dim csvRec As New SiireDataCsvRecord
        Dim csvTable As DataTable
        Dim dtFrom As DateTime
        Dim dtTo As DateTime

        dtFrom = DateTime.Parse(strUriageFrom)
        dtTo = DateTime.Parse(strUriageTo)
        'タイプによって出力内容を変更
        Select Case enFileType
            Case enOutputFileType.SiireTyousa
                '仕入調査
                csvTable = clsDataAccess.GetSiireTyousaOutPutData(dtFrom, dtTo, intDenpyouNo)
            Case enOutputFileType.SiireKouji
                '仕入工事
                csvTable = clsDataAccess.GetSiireKoujiOutPutdata(dtFrom, dtTo, intDenpyouNo)
            Case Else
                csvTable = Nothing
        End Select

        listCsvRec = DataMappingHelper.Instance.getMapArray(Of SiireDataCsvRecord)(GetType(SiireDataCsvRecord), csvTable)

        Return listCsvRec
    End Function

    ''' <summary>
    ''' 売上データの固有処理を行います
    ''' </summary>
    ''' <param name="listCsvRec">売上CSV出力用データ格納リスト</param>
    ''' <param name="strFileType">ファイル種別</param>
    ''' <returns>CSV出力文字列</returns>
    ''' <remarks></remarks>
    Private Function UriageUniqueSyori(ByVal listCsvRec As List(Of UriageDataCsvRecord) _
                                    , ByVal strFileType As String _
                                    , ByRef intResultUpdate As Integer) As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UriageUniqueSyori" _
                                                    , listCsvRec _
                                                    , strFileType)
        Dim strOutPutRecord As String
        Dim strOutPutString As String
        Dim intGyouNo As Integer
        Dim cmdTextSb As New StringBuilder()
        Dim csvRec As UriageDataCsvRecord
        Dim clsDataAccess As New UriageSiireSakuseiDataAccess
        Dim intResult As Integer
        Dim clsRenkeiLogic As New RenkeiKanriLogic
        Dim recTeibetuRenkei As New TeibetuSeikyuuRenkeiRecord
        Dim recTenbetuSyokiRenkei As New TenbetuSyokiSeikyuuRenkeiRecord
        Dim recTenbetuRenkei As New TenbetuSeikyuuRenkeiRecord
        Dim strDenpyouNo As String
        Dim intAutoFixCnt As Integer
        Dim dtFrom As DateTime
        Dim dtTo As DateTime
        Dim strBunruiCd As String
        Dim intUriageResult As Integer
        Dim intSiireResult As Integer

        'カウンタの初期化
        intGyouNo = 0
        intResult = 0

        If listCsvRec.Count = 0 Then
            '処理対象データが存在しなかった場合
            strOutPutString = ""
        End If
        '出力文字列の初期化
        cmdTextSb = New StringBuilder()
        For Each csvRec In listCsvRec
            'カウンタの初期化
            intAutoFixCnt = 0

            'カウンタのカウントアップ
            intDenpyouNo += 1
            If intDenpyouNo > 9999 AndAlso enFileType <> enOutputFileType.UriageTyousa Then
                intDenpyouNo = intDenpyouNo - 9999
            ElseIf intDenpyouNo > 39999 Then
                intDenpyouNo = intDenpyouNo - 39999
            End If
            intGyouNo += 1
            With csvRec
                '出力ファイルタイプ別処理分岐
                Select Case enFileType
                    Case enOutputFileType.UriageTyousa _
                        , enOutputFileType.UriageKouji _
                        , enOutputFileType.UriageSonota

                        strDenpyouNo = intDenpyouNo.ToString
                        '伝票NOの設定
                        Select Case enFileType
                            Case enOutputFileType.UriageTyousa
                                strDenpyouNo = "00" & intDenpyouNo.ToString.PadLeft(4, "0"c)
                            Case enOutputFileType.UriageKouji
                                strDenpyouNo = "01" & intDenpyouNo.ToString.PadLeft(4, "0"c)
                            Case enOutputFileType.UriageSonota
                                strDenpyouNo = "02" & intDenpyouNo.ToString.PadLeft(4, "0"c)
                        End Select
                        If intAutoFixCnt > 0 Then
                            '発注書自動確定情報テーブルの登録
                            intResult += clsDataAccess.JidouKakuteiSyori(strDenpyouNo _
                                                                        , .UpdateKey1 _
                                                                        , .UpdateKey2 _
                                                                        , .UpdateKey3 _
                                                                        , Integer.Parse(.UpdateKey4) _
                                                                        , strLoginUserId _
                                                                        , UpdDatetime)
                        End If
                    Case enOutputFileType.UriageMiseuri

                    Case Else
                        '処理対象データが存在しなかった場合
                        strOutPutString = ""
                        Return strOutPutString
                        Exit Function
                End Select
                '出力文字列の生成
                strOutPutRecord = GetUriageOutPutCsvString(csvRec, intDenpyouNo)
                cmdTextSb.Append(strOutPutRecord)
                'ダウンロードデータテーブルに出力レコードを登録する
                intResult += clsDataAccess.InsertDownLoadtable(strFileType, intGyouNo, strOutPutRecord, strLoginUserId, UpdDatetime)

            End With
        Next

        '売上年月日FromToの取得
        dtFrom = strUriageFrom
        dtTo = strUriageTo

        '売上確定処理・自動確定処理
        Select Case enFileType
            Case enOutputFileType.UriageTyousa _
                , enOutputFileType.UriageKouji _
                , enOutputFileType.UriageSonota
                Select Case enFileType
                    Case enOutputFileType.UriageTyousa
                        strBunruiCd = "'100','110','115','120','190'"
                    Case enOutputFileType.UriageKouji
                        strBunruiCd = "'130','140'"
                    Case enOutputFileType.UriageSonota
                        strBunruiCd = "'150','160','170','180'"
                    Case Else
                        strBunruiCd = ""
                End Select
                '邸別請求テーブルの更新（売上確定処理・自動確定処理）
                intResultUpdate += clsDataAccess.UriageJidouKakuteiForTeibetu(dtFrom, dtTo, strBunruiCd, strLoginUserId, UpdDatetime)

                '調査の場合
                If enFileType = enOutputFileType.UriageTyousa Then

                End If

            Case enOutputFileType.UriageMiseuri
                '店別初期請求テーブルの更新（売上確定処理）
                intResult += clsDataAccess.UriageKakuteiSyoriForTenbetuSyoki(dtFrom, dtTo, strLoginUserId, UpdDatetime)
                '店別請求テーブルの更新（売上確定処理）
                intResult += clsDataAccess.UriageKakuteiSyoriForTenbetu(dtFrom, dtTo, strLoginUserId, UpdDatetime)
                '汎用売上確定処理
                intUriageResult = UpdHannyouUriageKakutei()
                '汎用仕入確定処理
                intSiireResult = UpdHannyouSiireKakutei()
                intResultUpdate += intUriageResult + intSiireResult
            Case Else
                '処理対象データが存在しなかった場合
                strOutPutString = ""
                Return strOutPutString
                Exit Function
        End Select

        '邸別請求連携管理テーブルにUPDATE/INSERT
        intResult = renkeiDataAccess.setTeibetuSeikyuuRenkei(UpdDatetime, LoginUserId)
        '店別請求連携管理テーブルにUPDATE/INSERT
        intResult = renkeiDataAccess.setTenbetuSeikyuuRenkei(UpdDatetime, LoginUserId)
        '店別初期請求連携管理テーブルにUPDATE/INSERT
        intResult = renkeiDataAccess.setTenbetuSyokiSeikyuuRenkei(UpdDatetime, LoginUserId)

        strOutPutString = cmdTextSb.ToString
        Return strOutPutString
    End Function

    ''' <summary>
    ''' 仕入データの固有処理を行います
    ''' </summary>
    ''' <param name="listCsvRec">仕入CSV出力用データ格納リスト</param>
    ''' <returns>CSV出力文字列</returns>
    ''' <remarks></remarks>
    Private Function SiireUniqueSyori(ByVal listCsvRec As List(Of SiireDataCsvRecord) _
                                    , ByVal strFileType As String _
                                    , ByRef intResult As Integer) As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SiireUniqueSyori" _
                                                    , listCsvRec _
                                                    , strFileType)
        Dim strOutPutRecord As String
        Dim strOutPutString As String
        Dim intGyouNo As Integer
        Dim cmdTextSb As New StringBuilder()
        Dim csvRec As SiireDataCsvRecord
        Dim clsDataAccess As New UriageSiireSakuseiDataAccess
        Dim clsRenkeiLogic As New RenkeiKanriLogic
        Dim recTeibetuRenkei As New TeibetuSeikyuuRenkeiRecord
        Dim dtFrom As DateTime
        Dim dtTo As DateTime
        Dim strBunruiCd As String

        'カウンタの初期化
        intGyouNo = 0
        If listCsvRec.Count = 0 Then
            '処理対象データが存在しなかった場合
            strOutPutString = Messages.MSG020E
            Return ""
            Exit Function
        End If
        '出力文字列の初期化
        cmdTextSb = New StringBuilder()
        For Each csvRec In listCsvRec
            'カウンタのカウントアップ
            intDenpyouNo += 1
            If intDenpyouNo > 9999 AndAlso enFileType <> enOutputFileType.SiireTyousa Then
                intDenpyouNo = intDenpyouNo - 9999
            ElseIf intDenpyouNo > 39999 Then
                intDenpyouNo = intDenpyouNo - 39999
            End If
            intGyouNo += 1            '出力ファイルタイプ別処理分岐
            Select Case enFileType
                Case enOutputFileType.SiireTyousa _
                    , enOutputFileType.SiireKouji
                    '出力文字列の生成
                    strOutPutRecord = GetSiireOutPutCsvString(csvRec, intDenpyouNo)
                    cmdTextSb.Append(strOutPutRecord)
                    'ダウンロードデータテーブルに出力レコードを登録する
                    intResult += clsDataAccess.InsertDownLoadtable(strFileType, intGyouNo, strOutPutRecord, strLoginUserId, UpdDatetime)
                Case Else
                    '処理対象データが存在しなかった場合
                    strOutPutString = ""
                    Return strOutPutString
                    Exit Function
            End Select
        Next

        '売上年月日FromToの取得
        dtFrom = strUriageFrom
        dtTo = strUriageTo
        '売上確定処理
        Select Case enFileType
            Case enOutputFileType.SiireTyousa
                strBunruiCd = "'100','110','115','120','190'"
                '邸別請求テーブルの更新
                intResult += clsDataAccess.SiireUriageKakuteiSyoriForTyousa(dtFrom, dtTo, strLoginUserId, UpdDatetime)
            Case enOutputFileType.SiireKouji
                strBunruiCd = "'130','140'"
                '邸別請求テーブルの更新
                intResult += clsDataAccess.SiireUriageKakuteiSyoriForKouji(dtFrom, dtTo, strLoginUserId, UpdDatetime)
            Case Else
                '処理対象データが存在しなかった場合
                strOutPutString = ""
                Return strOutPutString
                Exit Function
        End Select

        '邸別請求連携管理テーブルにUPDATE/INSERT
        intResult = renkeiDataAccess.setTeibetuSeikyuuRenkei(UpdDatetime, LoginUserId)

        strOutPutString = cmdTextSb.ToString
        Return strOutPutString
    End Function

    ''' <summary>
    ''' 売上データCSVファイル出力用の文字列を取得します
    ''' </summary>
    ''' <param name="CsvRec">売上CSV出力用データ格納リスト</param>
    ''' <returns>売上データCSVファイル出力用の文字列</returns>
    ''' <remarks></remarks>
    Private Function GetUriageOutPutCsvString(ByVal CsvRec As UriageDataCsvRecord _
                                            , ByVal intDenpyouNo As Integer) As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetUriageOutPutCsvString" _
                                                    , CsvRec _
                                                    , intDenpyouNo)
        Dim strOutputString As String
        Dim cmdTextSb As New StringBuilder()

        With CsvRec
            cmdTextSb.Append("""" & .Denku & """,")
            If .UriDate = DateTime.MinValue Then
                cmdTextSb.Append("""" & "" & """,")
            Else
                cmdTextSb.Append("""" & Format(.UriDate, EarthConst.FORMAT_DATE_TIME_3) & """,")
            End If
            If .SeikyuusyoHakDate = DateTime.MinValue Then
                cmdTextSb.Append("""" & "" & """,")
            Else
                cmdTextSb.Append("""" & Format(.SeikyuusyoHakDate, EarthConst.FORMAT_DATE_TIME_3) & """,")
            End If
            Select Case enFileType
                Case enOutputFileType.UriageTyousa
                    cmdTextSb.Append("""" & (intDenpyouNo + 60000).ToString.PadLeft(6, "0"c) & """,")
                Case enOutputFileType.UriageKouji
                    cmdTextSb.Append("""" & "01" & intDenpyouNo.ToString.PadLeft(4, "0"c) & """,")
                Case enOutputFileType.UriageSonota
                    cmdTextSb.Append("""" & "02" & intDenpyouNo.ToString.PadLeft(4, "0"c) & """,")
                Case enOutputFileType.UriageMiseuri
                    cmdTextSb.Append("""" & "03" & intDenpyouNo.ToString.PadLeft(4, "0"c) & """,")
                Case Else
                    cmdTextSb.Append("""" & intDenpyouNo & """,")
            End Select
            cmdTextSb.Append("""" & sl.StringCutter(.TokuiSakiCd, enOutPutDataByte.TokuiSakiCd) & """,")
            cmdTextSb.Append("""" & sl.StringCutter(.TokuiSakiName, enOutPutDataByte.TokuiSakiName) & """,")
            cmdTextSb.Append("""" & .TyokusouSakiCd & """,")
            cmdTextSb.Append("""" & sl.StringCutter(.SenpouTantousyaName, enOutPutDataByte.SenpouTantousyaName) & """,")
            cmdTextSb.Append("""" & .BumonCd & """,")
            cmdTextSb.Append("""" & .TantousyaCd & """,")
            cmdTextSb.Append("""" & .TekiyouCd & """,")
            cmdTextSb.Append("""" & sl.StringCutter(.TekiyouName, enOutPutDataByte.TekiyouName) & """,")
            cmdTextSb.Append("""" & .BunruiCd & """,")
            cmdTextSb.Append("""" & .DenpyouKbn & """,")
            cmdTextSb.Append("""" & .SyouhinCd & """,")
            cmdTextSb.Append("""" & .MastaKbn & """,")
            cmdTextSb.Append("""" & sl.StringCutter(.HinMei, enOutPutDataByte.HinName) & """,")
            cmdTextSb.Append("""" & .Ku & """,")
            cmdTextSb.Append("""" & .SoukoCd & """,")
            cmdTextSb.Append("""" & .IriSuu & """,")
            cmdTextSb.Append("""" & .HakoSuu & """,")
            cmdTextSb.Append("""" & .Suuryou & """,")
            cmdTextSb.Append("""" & .Tani & """,")
            cmdTextSb.Append("""" & .Tanka & """,")
            cmdTextSb.Append("""" & .UriageKingaku & """,")
            cmdTextSb.Append("""" & .GenTanka & """,")
            cmdTextSb.Append("""" & .GenkaGaku & """,")
            cmdTextSb.Append("""" & .AraRieki & """,")
            cmdTextSb.Append("""" & .SotoZeiGaku & """,")
            cmdTextSb.Append("""" & .UtiZeiGaku & """,")
            cmdTextSb.Append("""" & .ZeiKbn & """,")
            cmdTextSb.Append("""" & .ZeikomiKbn & """,")
            cmdTextSb.Append("""" & sl.StringCutter(.Bikou, enOutPutDataByte.Bikou) & """,")
            cmdTextSb.Append("""" & .HyoujunKakaku & """,")
            cmdTextSb.Append("""" & .DoujiNyuukaKbn & """,")
            cmdTextSb.Append("""" & .BaiTanka & """,")
            cmdTextSb.Append("""" & .BaikaKingaku & """,")
            cmdTextSb.Append("""" & .KikakuKataBan & """,")
            cmdTextSb.Append("""" & .Color & """,")
            cmdTextSb.Append("""" & .Size & """")
            cmdTextSb.Append(vbCrLf)
        End With
        strOutputString = cmdTextSb.ToString

        Return strOutputString
    End Function

    ''' <summary>
    ''' 仕入データCSVファイル出力用の文字列を取得します
    ''' </summary>
    ''' <param name="CsvRec">仕入CSV出力用データ格納リスト</param>
    ''' <returns>仕入データCSVファイル出力用の文字列</returns>
    ''' <remarks></remarks>
    Private Function GetSiireOutPutCsvString(ByVal CsvRec As SiireDataCsvRecord _
                                            , ByVal intDenpyouNo As Integer) As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSiireOutPutCsvString" _
                                                    , CsvRec _
                                                    , intDenpyouNo)
        Dim strOutputString As String
        Dim cmdTextSb As New StringBuilder()

        With CsvRec
            cmdTextSb.Append("""" & .NyuukaHouhou & """,")
            cmdTextSb.Append("""" & .KamokuKbn & """,")
            cmdTextSb.Append("""" & .Denku & """,")
            If .NyuukaDate = DateTime.MinValue Then
                cmdTextSb.Append("""" & "" & """,")
            Else
                cmdTextSb.Append("""" & Format(.NyuukaDate, EarthConst.FORMAT_DATE_TIME_3) & """,")
            End If
            If .SeisanDate = DateTime.MinValue Then
                cmdTextSb.Append("""" & "" & """,")
            Else
                cmdTextSb.Append("""" & Format(.SeisanDate, EarthConst.FORMAT_DATE_TIME_3) & """,")
            End If
            Select Case enFileType
                Case enOutputFileType.SiireTyousa
                    cmdTextSb.Append("""" & (intDenpyouNo + 60000).ToString.PadLeft(6, "0"c) & """,")
                Case enOutputFileType.SiireKouji
                    cmdTextSb.Append("""" & "01" & intDenpyouNo.ToString.PadLeft(4, "0"c) & """,")
                Case Else
                    cmdTextSb.Append("""" & intDenpyouNo & """,")
            End Select
            cmdTextSb.Append("""" & sl.StringCutter(.SiireSaki, enOutPutDataByte.SiireSakiCd) & """,")
            cmdTextSb.Append("""" & sl.StringCutter(.SiireSakiName, enOutPutDataByte.SiireSakiName) & """,")
            cmdTextSb.Append("""" & sl.StringCutter(.SenpouTantousyaName, enOutPutDataByte.SenpouTantousyaName) & """,")
            cmdTextSb.Append("""" & .BumonCd & """,")
            cmdTextSb.Append("""" & .TantousyaCd & """,")
            cmdTextSb.Append("""" & .TekiyouCd & """,")
            cmdTextSb.Append("""" & sl.StringCutter(.TekiyouName, enOutPutDataByte.TekiyouName) & """,")
            cmdTextSb.Append("""" & .SyouhinCd & """,")
            cmdTextSb.Append("""" & .MastaKbn & """,")
            cmdTextSb.Append("""" & sl.StringCutter(.HinMei, enOutPutDataByte.HinName) & """,")
            cmdTextSb.Append("""" & .Ku & """,")
            cmdTextSb.Append("""" & .SoukoCd & """,")
            cmdTextSb.Append("""" & .IriSuu & """,")
            cmdTextSb.Append("""" & .HakoSuu & """,")
            cmdTextSb.Append("""" & .Suuryou & """,")
            cmdTextSb.Append("""" & .Tani & """,")
            cmdTextSb.Append("""" & .Tanka & """,")
            cmdTextSb.Append("""" & .Kingaku & """,")
            cmdTextSb.Append("""" & .SotoZeiGaku & """,")
            cmdTextSb.Append("""" & .UtiZeiGaku & """,")
            cmdTextSb.Append("""" & .ZeiKbn & """,")
            cmdTextSb.Append("""" & .ZeikomiKbn & """,")
            cmdTextSb.Append("""" & sl.StringCutter(.Bikou, enOutPutDataByte.Bikou) & """,")
            cmdTextSb.Append("""" & .KikakuKataBan & """,")
            cmdTextSb.Append("""" & .Color & """,")
            cmdTextSb.Append("""" & .Size & """")
            cmdTextSb.Append(vbCrLf)
        End With

        strOutputString = cmdTextSb.ToString

        Return strOutputString
    End Function

    ''' <summary>
    ''' 汎用売上確定処理
    ''' </summary>
    ''' <returns>処理結果メッセージ</returns>
    ''' <remarks></remarks>
    Private Function UpdHannyouUriageKakutei() As Integer
        Dim dtFrom As DateTime
        Dim dtTo As DateTime
        Dim dtAcc As New UriageSiireSakuseiDataAccess
        Dim intResult As Integer

        '売上年月日FromToの取得
        dtFrom = DLogic.str2DtTime(strUriageFrom)
        dtTo = DLogic.str2DtTime(strUriageTo)

        '売上確定処理
        intResult = dtAcc.UriageKakuteiSyoriForHannyou(dtFrom, dtTo, strLoginUserId, UpdDatetime)

        Return intResult
    End Function

    ''' <summary>
    ''' 汎用仕入確定処理
    ''' </summary>
    ''' <returns>処理結果メッセージ</returns>
    ''' <remarks></remarks>
    Private Function UpdHannyouSiireKakutei() As Integer
        Dim dtFrom As DateTime
        Dim dtTo As DateTime
        Dim dtAcc As New UriageSiireSakuseiDataAccess
        Dim intResult As Integer

        '売上年月日FromToの取得
        dtFrom = DLogic.str2DtTime(strUriageFrom)
        dtTo = DLogic.str2DtTime(strUriageTo)

        '仕入確定処理
        intResult = dtAcc.SiireKakuteiSyoriForHannyou(dtFrom, dtTo, strLoginUserId, UpdDatetime)

        Return intResult
    End Function

#End Region

#Region "統合会計連動対応"
    ''' <summary>
    ''' 売上/仕入/入金データTの統合会計送信フラグを更新する
    ''' </summary>
    ''' <returns>処理結果メッセージ</returns>
    ''' <remarks></remarks>
    Public Function UpdTgkSouinFlg(ByVal sender As Object, ByVal emBtnType As EarthEnum.emExecBtnType, ByVal strLoginUserId As String, ByVal dtUriDateTo As DateTime) As Integer
        Dim dtNow As DateTime = DateTime.Now
        Dim dtAcc As New UriageSiireSakuseiDataAccess
        Dim intResult As Integer = -1

        Try
            ' 地盤テーブルと邸別請求の同期を保つため、トランザクション制御を行う
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                intResult = dtAcc.UpdTgkSousinFlg(emBtnType, strLoginUserId, dtNow, dtUriDateTo)
                If intResult < 0 Then
                    Return False
                    Exit Function
                End If

                ' 更新に成功した場合、トランザクションをコミットする
                scope.Complete()

            End Using

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = 1205 Then    'エラーキャッチ：デッドロック
                MLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            ElseIf exSqlException.Number = -2 Then  'エラーキャッチ：タイムアウト
                MLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            Return False
        Catch exTransactionException As System.Transactions.TransactionException
            'エラーキャッチ：トランザクションエラー
            UnTrappedExceptionManager.Publish(exTransactionException)
            MLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exTransactionException.ToString), 0, "TransactionException")
            Return False
        End Try

        Return intResult
    End Function
#End Region

#Region "データ作成（月別割引）処理"
    ''' <summary>
    ''' データ作成（月別割引）処理を行います
    ''' </summary>
    ''' <returns>処理結果メッセージ</returns>
    ''' <remarks></remarks>
    Public Function MakeDataWaribiki() As String
        Dim clsDataAccess As New UriageSiireSakuseiDataAccess
        Dim strRetMsg As String
        Dim intResult As Integer
        Dim lastUpdDenpyouTable As DenpyouDataSet.LastUpdJouhouDataTable
        Dim lastUpdRow As DenpyouDataSet.LastUpdJouhouRow
        Dim intLockDenpyouNo As Integer
        Dim waribikiTable As UriageSiireCsvDataSet.WaribikiKensuuTableDataTable
        Dim waribikiRow As UriageSiireCsvDataSet.WaribikiKensuuTableRow
        Dim dtFrom As DateTime
        Dim dtTo As DateTime
        Dim dtFromStart As DateTime
        Dim dtToEnd As DateTime
        Dim dtFromTheEnd As DateTime
        Dim intTanka As Integer
        Dim clsRenkeiLogic As New RenkeiKanriLogic
        Dim recTenbetuRenkei As New TenbetuSeikyuuRenkeiRecord
        Dim listCsvRec As New List(Of UriageDataCsvRecord)
        Dim csvRec As New UriageDataCsvRecord
        Dim csvTable As DataTable
        Dim strFileType As String
        Dim strZeiKbn As String
        Dim decZeiritu As Decimal
        Dim tenbetuSeikyuuRenkeiTgtTable As TenbetuRenkeiDataSet.TenbetuRenkeiTargetDataTable
        Dim clsRenkei As New RenkeiKanriLogic

        '画面上の日付を取得
        dtFrom = DateTime.Parse(strUriageFrom)
        dtTo = DateTime.Parse(strUriageTo)
        dtFromStart = DateSerial(dtFrom.Year, dtFrom.Month, 1)
        dtToEnd = DateSerial(dtTo.Year, dtTo.Month + 1, 1).AddDays(-1)
        '売上年月日FROMの月末を取得
        dtFromTheEnd = DateSerial(dtFromStart.Year, dtFromStart.Month + 1, 1).AddDays(-1)

        Try
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))
                '伝票NOの排他制御
                lastUpdDenpyouTable = clsDataAccess.GetLastUpdDateDenpyouNo(enFileType, True)
                If lastUpdDenpyouTable.Rows.Count > 0 Then
                    lastUpdRow = lastUpdDenpyouTable.Rows(0)
                    intLockDenpyouNo = lastUpdRow.saisyuu_denpyou_no
                End If
                If intDenpyouNo <> intLockDenpyouNo Then
                    strRetMsg = Messages.MSG075E
                    Return strRetMsg
                    Exit Function
                End If
                'ファイル種別の設定
                strFileType = FT_URIAGE_TENBETU_WARIBIKI
                'ダウンロードデータテーブルを削除
                intResult += clsDataAccess.DeleteDownLoadTable(strFileType)
                'データ存在チェック時に削除する店別請求テーブルの連携管理用キーを取得
                tenbetuSeikyuuRenkeiTgtTable = clsDataAccess.GetWaribikiDataRenkeiTarget(dtFromTheEnd, strLoginUserId)
                'データ存在チェック
                intResult += clsDataAccess.DeleteWaribikiData(dtFromTheEnd, strLoginUserId)
                '店別請求テーブルから削除したデータを連携管理テーブルに更新
                intResult += clsRenkei.EditTenbetuSeikyuuRenkeiRecords(tenbetuSeikyuuRenkeiTgtTable, strLoginUserId, True, True)


                '集計先営業所コード単位の割引対象件数を取得
                waribikiTable = clsDataAccess.GetCountWaribikiTarget(dtFromStart, dtToEnd)

                '税区分の取得
                strZeiKbn = clsDataAccess.GetZeiKbn
                '税率の取得
                decZeiritu = clsDataAccess.GetZeiRitu

                '割引金額の設定
                For intCnt As Integer = 0 To waribikiTable.Count - 1
                    waribikiRow = waribikiTable.Rows(intCnt)
                    intTanka = 0
                    With waribikiRow
                        Select Case .waribiki_count
                            Case Is < 10
                                intTanka = 0
                            Case 10 To 19
                                intTanka = (.waribiki_count - 9) * 1000
                            Case 20 To 29
                                intTanka = (.waribiki_count - 19) * 2000 + 10000
                            Case 30 To 39
                                intTanka = (.waribiki_count - 29) * 3000 + 30000
                            Case 40 To 49
                                intTanka = (.waribiki_count - 39) * 4000 + 60000
                            Case Is > 50
                                intTanka = (.waribiki_count - 49) * 5000 + 100000
                        End Select
                        If intTanka > 0 Then
                            '店別請求テーブルへ月額実績割引データを登録
                            intResult += clsDataAccess.InsertWaribikiDataSyori(.syukeisaki_eigyousyo_cd _
                                                                                , intTanka _
                                                                                , dtFromTheEnd _
                                                                                , strZeiKbn _
                                                                                , decZeiritu _
                                                                                , strLoginUserId _
                                                                                , UpdDatetime)
                            '店別請求連携管理テーブルの登録
                            recTenbetuRenkei.MiseCd = .syukeisaki_eigyousyo_cd
                            recTenbetuRenkei.BunruiCd = "240"
                            recTenbetuRenkei.NyuuryokuDate = DateTime.Today
                            recTenbetuRenkei.NyuuryokuDateNo = 1
                            recTenbetuRenkei.RenkeiSijiCd = 1
                            recTenbetuRenkei.SousinJykyCd = 0
                            recTenbetuRenkei.UpdLoginUserId = strLoginUserId
                            recTenbetuRenkei.IsUpdate = False
                            intResult += clsRenkeiLogic.EditTenbetuSeikyuuRenkeiData(recTenbetuRenkei)
                        End If
                    End With
                Next

                '売上CSV出力用データを取得
                csvTable = clsDataAccess.GetWaribikiTenbetuOutPutData(dtFromStart, dtToEnd, intDenpyouNo)
                listCsvRec = DataMappingHelper.Instance.getMapArray(Of UriageDataCsvRecord)(GetType(UriageDataCsvRecord), csvTable)

                '割引固有処理
                strOutPutString = WaribikiUniqueSyori(listCsvRec, strFileType)
                If strOutPutString.Length = 0 Then
                    '処理対象データが存在しなかった場合
                    strRetMsg = Messages.MSG020E
                    Return strRetMsg
                    Exit Function
                End If

                '伝票マスタの更新
                intResult += clsDataAccess.UpdateDenpyouNo(enFileType, intDenpyouNo, strLoginUserId, UpdDatetime)

                ' 更新に成功した場合、トランザクションをコミットする
                scope.Complete()
            End Using

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = -2 Then 'エラーキャッチ：タイムアウト
                strRetMsg = Messages.MSG086E & sl.RemoveSpecStr(exSqlException.Message)
            Else
                strRetMsg = Messages.MSG117E & sl.RemoveSpecStr(exSqlException.Message)
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            Return strRetMsg
            Exit Function
        Catch exTransactionException As System.Transactions.TransactionException
            strRetMsg = Messages.MSG117E & sl.RemoveSpecStr(exTransactionException.Message)
            UnTrappedExceptionManager.Publish(exTransactionException)
            Return strRetMsg
            Exit Function
        Catch ex As Exception
            strRetMsg = Messages.MSG118E & sl.RemoveSpecStr(ex.Message)
            UnTrappedExceptionManager.Publish(ex)
            Return strRetMsg
            Exit Function
        End Try

        strRetMsg = ""
        Return strRetMsg
    End Function

    ''' <summary>
    ''' 売上（割引）データの固有処理を行います
    ''' </summary>
    ''' <param name="listCsvRec">売上（割引）CSV出力用データ格納リスト</param>
    ''' <param name="strFileType">ファイル種別</param>
    ''' <returns>CSV出力文字列</returns>
    ''' <remarks></remarks>
    Private Function WaribikiUniqueSyori(ByVal listCsvRec As List(Of UriageDataCsvRecord) _
                                    , ByVal strFileType As String) As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".WaribikiUniqueSyori" _
                                                    , listCsvRec _
                                                    , strFileType)
        Dim strOutPutRecord As String
        Dim strOutPutString As String
        Dim intGyouNo As Integer
        Dim cmdTextSb As New StringBuilder()
        Dim csvRec As UriageDataCsvRecord
        Dim clsDataAccess As New UriageSiireSakuseiDataAccess
        Dim intResult As Integer

        'カウンタの初期化
        intGyouNo = 0
        If listCsvRec.Count = 0 Then
            '処理対象データが存在しなかった場合
            strOutPutString = ""
            Return strOutPutString
            Exit Function
        End If

        '出力文字列の初期化
        cmdTextSb = New StringBuilder()
        For Each csvRec In listCsvRec
            'フラグの初期化
            'カウンタのカウントアップ
            intDenpyouNo += 1
            If intDenpyouNo > 9999 Then
                intDenpyouNo = intDenpyouNo - 9999
            End If
            intGyouNo += 1
            With csvRec
                '出力文字列の生成
                strOutPutRecord = GetUriageOutPutCsvString(csvRec, intDenpyouNo)
                cmdTextSb.Append(strOutPutRecord)
                'ダウンロードデータテーブルに出力レコードを登録する
                intResult += clsDataAccess.InsertDownLoadtable(strFileType, intGyouNo, strOutPutRecord, strLoginUserId, UpdDatetime)
            End With
        Next

        strOutPutString = cmdTextSb.ToString
        Return strOutPutString
    End Function
#End Region

#Region "再ダウンロード処理"
    ''' <summary>
    ''' 再ダウンロード処理を行います
    ''' </summary>
    ''' <returns>処理結果メッセージ</returns>
    ''' <remarks></remarks>
    Public Function ReDownLoad() As String
        Dim clsDataAccess As New UriageSiireSakuseiDataAccess
        Dim strRetMsg As String
        Dim DlTable As UriageSiireCsvDataSet.DownLoadTableDataTable
        Dim DlRow As UriageSiireCsvDataSet.DownLoadTableRow
        Dim cmdTextSb As New StringBuilder()
        Dim strFileType As String

        'ファイル種別判定
        Select Case enFileType
            Case enOutputFileType.UriageTyousa
                '売上調査
                strFileType = FT_URIAGE_TYOUSA
            Case enOutputFileType.UriageKouji
                '売上工事
                strFileType = FT_URIAGE_KOUJI
            Case enOutputFileType.UriageSonota
                '売上その他
                strFileType = FT_URIAGE_HOKA
            Case enOutputFileType.UriageMiseuri
                '売上店別
                strFileType = FT_URIAGE_TENBETU
            Case enOutputFileType.UriageWaribiki
                '売上店別(月別割引)
                strFileType = FT_URIAGE_TENBETU_WARIBIKI
            Case enOutputFileType.SiireTyousa
                '仕入調査
                strFileType = FT_SIIRE_TYOUSA
            Case enOutputFileType.SiireKouji
                '仕入工事
                strFileType = FT_SIIRE_KOUJI
            Case Else
                '処理対象データが存在しなかった場合
                strRetMsg = Messages.MSG020E
                Return strRetMsg
                Exit Function
        End Select

        'ダウンロードデータの取得
        DlTable = clsDataAccess.SelectDownLoadTable(strFileType)

        If DlTable.Rows.Count = 0 Then
            strRetMsg = Messages.MSG076E
            Return strRetMsg
            Exit Function
        End If

        '出力文字列の生成
        For intCnt As Integer = 0 To DlTable.Rows.Count - 1
            DlRow = DlTable.Rows(intCnt)
            cmdTextSb.Append(DlRow.gyou_data)
        Next
        strOutPutString = cmdTextSb.ToString

        strRetMsg = ""
        Return strRetMsg
    End Function
#End Region

End Class

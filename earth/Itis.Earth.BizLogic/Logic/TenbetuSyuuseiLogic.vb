Imports System.Transactions
Imports System.text
Imports System.Web.UI
Public Class TenbetuSyuuseiLogic
    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "画面情報プロパティ"
#Region "区分"
    ''' <summary>
    ''' 区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strKbn As String
    ''' <summary>
    ''' 区分
    ''' </summary>
    ''' <value></value>
    ''' <returns>区分</returns>
    ''' <remarks></remarks>
    Public Property Kbn() As String
        Get
            Return strKbn
        End Get
        Set(ByVal value As String)
            strKbn = value
        End Set
    End Property
#End Region
#Region "加盟店コード"
    ''' <summary>
    ''' 加盟店コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strMiseCd As String
    ''' <summary>
    ''' 加盟店コード
    ''' </summary>
    ''' <value></value>
    ''' <returns>加盟店コード</returns>
    ''' <remarks></remarks>
    Public Property MiseCd() As String
        Get
            Return strMiseCd
        End Get
        Set(ByVal value As String)
            strMiseCd = value
        End Set
    End Property
#End Region
#Region "モード"
    ''' <summary>
    ''' コントロールの表示モード
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum DisplayMode
        ''' <summary>
        ''' 店別データ修正
        ''' </summary>
        ''' <remarks></remarks>
        TENBETU = 1
        ''' <summary>
        ''' 販促品請求
        ''' </summary>
        ''' <remarks></remarks>
        HANSOKU = 2
        ''' <summary>
        ''' 売上処理済販促品参照
        ''' </summary>
        ''' <remarks></remarks>
        SANSYOU = 3
    End Enum
    ''' <summary>
    ''' コントロールの表示モード
    ''' </summary>
    ''' <remarks></remarks>
    Private enMode As DisplayMode
    ''' <summary>
    ''' コントロールの表示モード
    ''' </summary>
    ''' <value></value>
    ''' <returns>コントロールの表示モード</returns>
    ''' <remarks>商品の種類により画面の表示を変更します</remarks>
    Public Property DispMode() As DisplayMode
        Get
            Return enMode
        End Get
        Set(ByVal value As DisplayMode)
            enMode = value
        End Set
    End Property
    ''' <summary>
    ''' 加盟店モード
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum IsFcMode
        ''' <summary>
        ''' FC加盟店
        ''' </summary>
        ''' <remarks></remarks>
        FC = 1
        ''' <summary>
        ''' FC以外加盟店
        ''' </summary>
        ''' <remarks></remarks>
        NOT_FC = 0
    End Enum
    ''' <summary>
    ''' 加盟店モード
    ''' </summary>
    ''' <remarks></remarks>
    Private enIsFc As IsFcMode
    ''' <summary>
    ''' 加盟店モード
    ''' </summary>
    ''' <value></value>
    ''' <returns>FC加盟店:1 FC以外:0</returns>
    ''' <remarks></remarks>
    Public Property IsFC() As IsFcMode
        Get
            Return enIsFc
        End Get
        Set(ByVal value As IsFcMode)
            enIsFc = value
        End Set
    End Property
#End Region
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
#End Region
#Region "定数"
#Region "区分"
    Private Const JHSFC = "A"
#End Region
#Region "倉庫コード"
    Private SOUKO_CD_TOUROKU As String = "200"
    Private SOUKO_CD_TOOL As String = "210"
    Private SOUKO_CD_NOT_FC As String = "220"
    Private SOUKO_CD_FC As String = "230"
#End Region
#End Region
#Region "与信チェック用"
    ''' <summary>
    ''' 実請求単価のサマリ
    ''' </summary>
    ''' <remarks></remarks>
    Private lngSeikyuuGakuSum As Long
    ''' <summary>
    ''' 実請求単価のサマリ
    ''' </summary>
    ''' <value></value>
    ''' <returns>実請求単価のサマリ</returns>
    ''' <remarks></remarks>
    Public Property SeikyuuGakuSum() As Long
        Get
            Return lngSeikyuuGakuSum
        End Get
        Set(ByVal value As Long)
            lngSeikyuuGakuSum = value
        End Set
    End Property
    ''' <summary>
    ''' 工務店請求額のサマリ
    ''' </summary>
    ''' <remarks></remarks>
    Private lngKoumuGakuSum As Long
    ''' <summary>
    ''' 工務店請求額のサマリ
    ''' </summary>
    ''' <value></value>
    ''' <returns>工務店請求額のサマリ</returns>
    ''' <remarks></remarks>
    Public Property KoumuGakuSum() As Long
        Get
            Return lngKoumuGakuSum
        End Get
        Set(ByVal value As Long)
            lngKoumuGakuSum = value
        End Set
    End Property
#End Region

    ''' <summary>
    ''' 画面表示項目を取得します
    ''' </summary>
    ''' <param name="intUriFlg">売上計上FLG</param>
    ''' <returns>画面表示情報格納レコード</returns>
    ''' <remarks></remarks>
    Public Function GetDisplayInfo(Optional ByVal intUriFlg As Integer = -1) As TenbetuDataSyuuseiRecord
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDisplayInfo" _
                                            , intUriFlg)
        Dim clsDataAccess As New TenbetuSyuuseiDataAccess
        Dim recSeikyuuSakiRec As New TenbetuDataSyuuseiRecord

        Dim SeikyuuSakiTable As TenbetuSyuuseiDataSet.SeikyuuSakiTableDataTable
        Dim dTblTourokuRyou As DataTable
        Dim ToolRyouTable As DataTable
        Dim HansokuHinTable As DataTable

        '請求先情報の取得
        If enIsFc = IsFcMode.NOT_FC Then
            SeikyuuSakiTable = clsDataAccess.GetSeikyuuSaki(strMiseCd)
        Else
            SeikyuuSakiTable = clsDataAccess.GetFcSeikyuusaki(strMiseCd)
        End If
        If SeikyuuSakiTable.Rows.Count > 0 Then
            recSeikyuuSakiRec = DataMappingHelper.Instance.getMapArray(Of TenbetuDataSyuuseiRecord)(GetType(TenbetuDataSyuuseiRecord), SeikyuuSakiTable).Item(0)
        End If
        Select Case enMode
            Case DisplayMode.TENBETU
                '登録料情報の取得(FC以外の場合)
                If enIsFc = IsFcMode.NOT_FC Then
                    dTblTourokuRyou = clsDataAccess.GetTourokuRyou(strMiseCd)
                    If dTblTourokuRyou.Rows.Count > 0 Then
                        recSeikyuuSakiRec.TourokuRyouRecord = DataMappingHelper.Instance.getMapArray(Of TenbetuTourokuRyouRecord)(GetType(TenbetuTourokuRyouRecord), dTblTourokuRyou).Item(0)
                    End If
                End If
                '販促品初期ツール料情報の取得(FC以外かつ、区分＝Aの場合)
                If enIsFc = IsFcMode.NOT_FC And recSeikyuuSakiRec.Kbn = JHSFC Then
                    ToolRyouTable = clsDataAccess.GetToolRyou(strMiseCd)
                    If ToolRyouTable.Rows.Count > 0 Then
                        recSeikyuuSakiRec.ToolRyouRecord = DataMappingHelper.Instance.getMapArray(Of TenbetuToolRyouRecord)(GetType(TenbetuToolRyouRecord), ToolRyouTable).Item(0)
                    End If
                End If
                '販促品情報の取得(FC以外かつ、区分≠Aの場合）
                If enIsFc = IsFcMode.NOT_FC And recSeikyuuSakiRec.Kbn <> JHSFC Then
                    HansokuHinTable = clsDataAccess.GetHansokuHin(strMiseCd, SOUKO_CD_NOT_FC, intUriFlg)
                    If HansokuHinTable.Rows.Count > 0 Then
                        recSeikyuuSakiRec.HansokuHinRecords = DataMappingHelper.Instance.getMapArray(Of TenbetuHansokuHinRecord)(GetType(TenbetuHansokuHinRecord), HansokuHinTable)
                    End If
                End If
                '販促品情報の取得(FCの場合）
                If enIsFc = IsFcMode.FC Then
                    HansokuHinTable = clsDataAccess.GetHansokuHin(strMiseCd, SOUKO_CD_FC, intUriFlg)
                    If HansokuHinTable.Rows.Count > 0 Then
                        recSeikyuuSakiRec.HansokuHinRecords = DataMappingHelper.Instance.getMapArray(Of TenbetuHansokuHinRecord)(GetType(TenbetuHansokuHinRecord), HansokuHinTable)
                    End If
                End If
            Case DisplayMode.HANSOKU, DisplayMode.SANSYOU
                '販促品情報の取得
                If enIsFc = IsFcMode.NOT_FC Then
                    HansokuHinTable = clsDataAccess.GetHansokuHin(strMiseCd, SOUKO_CD_NOT_FC, intUriFlg)
                Else
                    HansokuHinTable = clsDataAccess.GetHansokuHin(strMiseCd, SOUKO_CD_FC, intUriFlg)
                End If
                If HansokuHinTable.Rows.Count > 0 Then
                    recSeikyuuSakiRec.HansokuHinRecords = DataMappingHelper.Instance.getMapArray(Of TenbetuHansokuHinRecord)(GetType(TenbetuHansokuHinRecord), HansokuHinTable)
                End If
        End Select

        Return recSeikyuuSakiRec
    End Function

    ''' <summary>
    ''' 標準マスタの標準価格を取得します
    ''' </summary>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <param name="strSoukoCd">倉庫コード</param>
    ''' <returns>商品明細レコード</returns>
    ''' <remarks></remarks>
    Public Function GetSyouhinMeisaiRec(ByVal strSyouhinCd As String _
                                    , ByVal strSoukoCd As String) As SyouhinMeisaiRecord
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSyouhinMeisaiRec" _
                                                    , strSyouhinCd _
                                                    , strSoukoCd)
        Dim clsDataAccess As New TenbetuSyuuseiDataAccess
        Dim syouhinMeisaiTable As TenbetuSyuuseiDataSet.SyouhinMeisaiDataTable
        Dim recSyouhinMeisai As New SyouhinMeisaiRecord

        syouhinMeisaiTable = clsDataAccess.GetSyouhinMeisai(strSyouhinCd, strSoukoCd)
        If syouhinMeisaiTable.Count > 0 Then
            recSyouhinMeisai = DataMappingHelper.Instance.getMapArray(Of SyouhinMeisaiRecord)(GetType(SyouhinMeisaiRecord), syouhinMeisaiTable).Item(0)
        Else
            recSyouhinMeisai = Nothing
        End If

        Return recSyouhinMeisai
    End Function

    ''' <summary>
    ''' 店別初期請求テーブルの更新を行います
    ''' </summary>
    ''' <param name="listUpdateDisplayInfo">画面情報格納レコードリスト</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateTenbetuSyokiSeikyuu(ByVal listUpdateDisplayInfo As List(Of TenbetuSyokiSeikyuuRecord), ByVal clsYosinOldRec As YosinTenbetuRecord) As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdateTenbetuSyokiSeikyuu" _
                                                    , listUpdateDisplayInfo _
                                                    , clsYosinOldRec)
        Dim clsDataAccess As New TenbetuSyuuseiDataAccess
        Dim TenbetuSyokiSeikyuuTable As TenbetuSyokiSeikyuuDataSet.TenbetuSyokiSeikyuuDataTable
        Dim recHaitaTargetInfo As New TenbetuSyokiSeikyuuRecord
        Dim strRetMsg As String
        Dim IFsqlMake As ISqlStringHelper
        Dim strSqlString As String
        Dim listParam As New List(Of ParamRecord)
        Dim clsJbAccess As New JibanDataAccess
        Dim strUpdateKeyInfo As String = ""
        Const UPDATE_TABLE_NAME As String = "店別初期請求テーブル"
        Const INSERT_PROCESS As String = "登録"
        Const UPDATE_PROCESS As String = "更新"
        Dim clsRenkeiLogic As New RenkeiKanriLogic
        Dim recTenbetuSyokiRenkei As New TenbetuSyokiSeikyuuRenkeiRecord
        Dim intResult As Integer
        Dim clsYosinNewRec As New YosinTenbetuRecord

        strRetMsg = ""

        Try
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                '与信チェック(FC外の場合のみ)
                If enIsFc = IsFcMode.NOT_FC Then
                    '画面上の合計額取得
                    With clsYosinNewRec
                        clsYosinNewRec.KameitenCd = listUpdateDisplayInfo(0).MiseCd
                        clsYosinNewRec.TourokuTesuuryouUriGaku = listUpdateDisplayInfo(0).ZeikomiGaku
                        If listUpdateDisplayInfo.Count > 1 Then
                            clsYosinNewRec.SyokiToolRyouUriGaku = listUpdateDisplayInfo(1).ZeikomiGaku
                        Else
                            clsYosinNewRec.SyokiToolRyouUriGaku = 0
                        End If
                        clsYosinNewRec.HansokuGoukei = 0
                        clsYosinNewRec.HansokuGoukeiKoumuten = 0
                        clsYosinNewRec.UpdLoginUserId = strLoginUserId
                        clsYosinNewRec.UpdDatetime = DateTime.Now
                    End With

                    '与信チェック処理
                    Dim YosinLogic As New YosinLogic
                    Dim intYosinResult As Integer = YosinLogic.YosinCheckTenbetu(6, clsYosinOldRec, clsYosinNewRec)
                    Select Case intYosinResult
                        Case EarthConst.YOSIN_ERROR_YOSIN_OK
                            'エラーなし
                        Case EarthConst.YOSIN_ERROR_YOSIN_NG
                            '与信限度額超過
                            strRetMsg = Messages.MSG089E
                            Return strRetMsg
                            Exit Function
                        Case Else
                            '6:与信管理情報取得エラー
                            '7:与信管理テーブル更新エラー
                            '8:メール送信処理エラー
                            '9:その他エラー
                            strRetMsg = String.Format(Messages.MSG090E, intYosinResult.ToString())
                            Return strRetMsg
                            Exit Function
                    End Select
                End If

                For Each recUpdateDisplayInfo As TenbetuSyokiSeikyuuRecord In listUpdateDisplayInfo
                    '更新対象のレコードを取得
                    With recUpdateDisplayInfo
                        TenbetuSyokiSeikyuuTable = clsDataAccess.GetUpdateTargetTenbetuSyoki(.MiseCd, .BunruiCd)
                    End With
                    '更新対象レコードがあれば更新、なければ登録
                    If TenbetuSyokiSeikyuuTable.Rows.Count > 0 Then
                        recHaitaTargetInfo = DataMappingHelper.Instance.getMapArray(Of TenbetuSyokiSeikyuuRecord)(GetType(TenbetuSyokiSeikyuuRecord), TenbetuSyokiSeikyuuTable).Item(0)
                        If recUpdateDisplayInfo.UpdDatetime <> recHaitaTargetInfo.UpdDatetime Then
                            strRetMsg = String.Format(Messages.MSG003E _
                                                    , recHaitaTargetInfo.UpdLoginUserId _
                                                    , Format(recHaitaTargetInfo.UpdDatetime, "yyyy/MM/dd HH:mm:ss") _
                                                    , UPDATE_TABLE_NAME)
                            Return strRetMsg
                            Exit Function
                        Else
                            '更新内容の設定
                            With recUpdateDisplayInfo
                                .AddDate = recHaitaTargetInfo.AddDate.Date
                                If .UriDate = DateTime.MinValue Then
                                    .UriKeijyouFlg = 0
                                    .UriKeijyouDate = DateTime.MinValue
                                Else
                                    .UriKeijyouFlg = recHaitaTargetInfo.UriKeijyouFlg
                                    .UriKeijyouDate = recHaitaTargetInfo.UriKeijyouDate
                                End If
                                .Bikou = recHaitaTargetInfo.Bikou
                                If .BunruiCd = SOUKO_CD_TOOL Then
                                    .KoumutenSeikyuuGaku = recHaitaTargetInfo.KoumutenSeikyuuGaku
                                End If
                            End With

                            'SQL情報を自動生成するInterfaceのインスタンス化
                            IFsqlMake = New UpdateStringHelper
                            '更新用文字列とパラメータの作成
                            strSqlString = IFsqlMake.MakeUpdateInfo(GetType(TenbetuSyokiSeikyuuRecord), recUpdateDisplayInfo, listParam)
                            'DB反映処理
                            If Not clsJbAccess.UpdateJibanData(strSqlString, listParam) Then
                                strUpdateKeyInfo = "店コード:" & recHaitaTargetInfo.MiseCd & " / " _
                                                & "分類コード:" & recHaitaTargetInfo.BunruiCd
                                strRetMsg = String.Format(Messages.MSG064E, UPDATE_PROCESS, UPDATE_TABLE_NAME, strUpdateKeyInfo)
                                Return strRetMsg
                                Exit Function
                            End If
                            '店別初期請求連携管理テーブルの更新
                            recTenbetuSyokiRenkei.MiseCd = recUpdateDisplayInfo.MiseCd
                            recTenbetuSyokiRenkei.BunruiCd = recUpdateDisplayInfo.BunruiCd
                            recTenbetuSyokiRenkei.RenkeiSijiCd = 2
                            recTenbetuSyokiRenkei.SousinJykyCd = 0
                            recTenbetuSyokiRenkei.UpdLoginUserId = recUpdateDisplayInfo.UpdLoginUserId
                            recTenbetuSyokiRenkei.IsUpdate = True
                            intResult += clsRenkeiLogic.EditTenbetuSyokiSeikyuuRenkeiData(recTenbetuSyokiRenkei)
                        End If
                    Else
                        '登録内容の設定
                        With recUpdateDisplayInfo
                            .AddDate = DateTime.Today
                            .UriKeijyouFlg = 0
                        End With
                        'SQL情報を自動生成するInterfaceのインスタンス化
                        IFsqlMake = New InsertStringHelper
                        '更新用文字列とパラメータの作成
                        strSqlString = IFsqlMake.MakeUpdateInfo(GetType(TenbetuSyokiSeikyuuRecord), recUpdateDisplayInfo, listParam)
                        'DB反映処理
                        If Not clsJbAccess.UpdateJibanData(strSqlString, listParam) Then
                            strUpdateKeyInfo = "\r\n店コード:" & recHaitaTargetInfo.MiseCd _
                                            & "\r\n分類コード:" & recHaitaTargetInfo.BunruiCd
                            strRetMsg = String.Format(Messages.MSG064E, INSERT_PROCESS, UPDATE_TABLE_NAME, strUpdateKeyInfo)
                            Return strRetMsg
                            Exit Function
                        End If
                        '店別初期請求連携管理テーブルの登録
                        recTenbetuSyokiRenkei.MiseCd = recUpdateDisplayInfo.MiseCd
                        recTenbetuSyokiRenkei.BunruiCd = recUpdateDisplayInfo.BunruiCd
                        recTenbetuSyokiRenkei.RenkeiSijiCd = 1
                        recTenbetuSyokiRenkei.SousinJykyCd = 0
                        recTenbetuSyokiRenkei.UpdLoginUserId = recUpdateDisplayInfo.UpdLoginUserId
                        recTenbetuSyokiRenkei.IsUpdate = False
                        intResult += clsRenkeiLogic.EditTenbetuSyokiSeikyuuRenkeiData(recTenbetuSyokiRenkei)
                    End If
                Next
                'トランザクションのコミット
                scope.Complete()
            End Using

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = 1205 Then    'エラーキャッチ：デッドロック
                strRetMsg = String.Format(Messages.MSG107E, exSqlException.Number)
            ElseIf exSqlException.Number = -2 Then  'エラーキャッチ：タイムアウト
                strRetMsg = String.Format(Messages.MSG107E, exSqlException.Number)
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
        Catch exTransactionException As System.Transactions.TransactionException
            'エラーキャッチ：トランザクションエラー
            UnTrappedExceptionManager.Publish(exTransactionException)
            strRetMsg = String.Format(Messages.MSG107E, exTransactionException.ToString)
        End Try

        Return strRetMsg
    End Function

    ''' <summary>
    ''' 店別請求テーブルの更新を行います
    ''' </summary>
    ''' <param name="listUpdateDisplayInfo">画面情報格納レコードリスト</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateTenbetuSeikyuu(ByVal listUpdateDisplayInfo As List(Of TenbetuSeikyuuRecord), ByVal clsYosinOldRec As YosinTenbetuRecord) As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdateTenbetuSeikyuu" _
                                                    , listUpdateDisplayInfo _
                                                    , clsYosinOldRec)
        Dim recUpdateDisplayInfo As TenbetuSeikyuuRecord
        Dim clsDataAccess As New TenbetuSyuuseiDataAccess
        Dim TenbetuSeikyuuTable As TenbetuSeikyuuDataSet.TenbetuSeikyuuDataTable
        Dim MaxNoTable As TenbetuSeikyuuDataSet.MaxNoDataTable
        Dim MaxNoRow As TenbetuSeikyuuDataSet.MaxNoRow
        Dim recHaitaTargetInfo As New TenbetuSeikyuuRecord
        Dim strRetMsg As String
        Dim IFsqlMake As ISqlStringHelper
        Dim strSqlString As String = ""
        Dim listParam As New List(Of ParamRecord)
        Dim clsJbAccess As New JibanDataAccess
        Dim strUpdateKeyInfo As String = ""
        Const UPDATE_TABLE_NAME As String = "店別請求テーブル"
        Dim strProcess As String
        Const INSERT_PROCESS As String = "登録"
        Const UPDATE_PROCESS As String = "更新"
        Const DELETE_PROCESS As String = "削除"
        Dim clsRenkeiLogic As New RenkeiKanriLogic
        Dim recTenbetuRenkei As New TenbetuSeikyuuRenkeiRecord
        Dim intResult As Integer
        Dim clsYosinNewRec As New YosinTenbetuRecord

        strRetMsg = ""

        Try
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                '与信チェック(FC外の場合のみ)
                If enIsFc = IsFcMode.NOT_FC And listUpdateDisplayInfo.Count > 0 Then
                    '画面上の合計額取得
                    With clsYosinNewRec
                        clsYosinNewRec.KameitenCd = listUpdateDisplayInfo(0).MiseCd
                        clsYosinNewRec.TourokuTesuuryouUriGaku = 0
                        clsYosinNewRec.SyokiToolRyouUriGaku = 0
                        clsYosinNewRec.HansokuGoukei = lngSeikyuuGakuSum
                        clsYosinNewRec.HansokuGoukeiKoumuten = lngKoumuGakuSum
                        clsYosinNewRec.UpdLoginUserId = strLoginUserId
                        clsYosinNewRec.UpdDatetime = DateTime.Now
                    End With

                    '与信チェック処理
                    Dim YosinLogic As New YosinLogic
                    Dim intYosinResult As Integer = YosinLogic.YosinCheckTenbetu(7, clsYosinOldRec, clsYosinNewRec)
                    Select Case intYosinResult
                        Case EarthConst.YOSIN_ERROR_YOSIN_OK
                            'エラーなし
                        Case EarthConst.YOSIN_ERROR_YOSIN_NG
                            '与信限度額超過
                            strRetMsg = Messages.MSG089E
                            Return strRetMsg
                            Exit Function
                        Case Else
                            '6:与信管理情報取得エラー
                            '7:与信管理テーブル更新エラー
                            '8:メール送信処理エラー
                            '9:その他エラー
                            strRetMsg = String.Format(Messages.MSG090E, intYosinResult.ToString())
                            Return strRetMsg
                            Exit Function
                    End Select

                End If

                For Each recUpdateDisplayInfo In listUpdateDisplayInfo
                    'データテーブルのインスタンス化
                    TenbetuSeikyuuTable = New TenbetuSeikyuuDataSet.TenbetuSeikyuuDataTable
                    '更新対象のレコードを取得
                    With recUpdateDisplayInfo
                        If .NyuuryokuDate <> DateTime.MinValue Then
                            TenbetuSeikyuuTable = clsDataAccess.GetUpdateTargetTenbetuSeikyuu(.MiseCd, .BunruiCd, .NyuuryokuDate, .NyuuryokuDateNo)
                        End If
                    End With
                    '更新対象レコードがあれば更新／削除、なければ登録
                    If TenbetuSeikyuuTable.Rows.Count > 0 Then
                        recHaitaTargetInfo = DataMappingHelper.Instance.getMapArray(Of TenbetuSeikyuuRecord)(GetType(TenbetuSeikyuuRecord), TenbetuSeikyuuTable).Item(0)
                        If recUpdateDisplayInfo.UpdDatetime <> recHaitaTargetInfo.UpdDatetime Then
                            strRetMsg = String.Format(Messages.MSG003E _
                                                    , recHaitaTargetInfo.UpdLoginUserId _
                                                    , recHaitaTargetInfo.UpdDatetime.ToString(EarthConst.FORMAT_DATE_TIME_2) _
                                                    , UPDATE_TABLE_NAME)
                            Return strRetMsg
                            Exit Function
                        Else
                            '更新内容の設定
                            With recUpdateDisplayInfo
                                If enMode <> DisplayMode.HANSOKU And enIsFc = IsFcMode.NOT_FC Then
                                    .HassouDate = recHaitaTargetInfo.HassouDate
                                Else
                                    .HassouDate = recUpdateDisplayInfo.HassouDate
                                End If
                                If .UriDate = DateTime.MinValue Then
                                    .UriKeijyouFlg = 0
                                    .UriKeijyouDate = DateTime.MinValue
                                Else
                                    .UriKeijyouFlg = recHaitaTargetInfo.UriKeijyouFlg
                                    .UriKeijyouDate = recHaitaTargetInfo.UriKeijyouDate
                                End If
                                If enIsFc = IsFcMode.FC Then
                                    .KoumutenSeikyuuTanka = 0
                                End If
                            End With
                            'SQL文字列を初期化()
                            strSqlString = ""
                            'SQL情報を自動生成するInterfaceのインスタンス化
                            Select Case recUpdateDisplayInfo.SqlTypeFlg
                                Case TenbetuSeikyuuRecord.enSqlTypeFlg.UPDATE
                                    IFsqlMake = New UpdateStringHelper
                                    strProcess = UPDATE_PROCESS
                                Case TenbetuSeikyuuRecord.enSqlTypeFlg.DELETE
                                    IFsqlMake = New DeleteStringHelper
                                    strProcess = DELETE_PROCESS
                                    If LoginUserId IsNot Nothing Then
                                        '削除処理時は、トリガ用ローカル一時テーブルを生成するSQLを追加
                                        strSqlString &= clsJbAccess.CreateUserInfoTempTableSQL(LoginUserId)
                                    End If
                                Case Else
                                    IFsqlMake = New InsertStringHelper
                                    strProcess = INSERT_PROCESS
                            End Select
                            '更新用文字列とパラメータの作成
                            strSqlString &= IFsqlMake.MakeUpdateInfo(GetType(TenbetuSeikyuuRecord), recUpdateDisplayInfo, listParam)
                            'DB反映処理
                            If Not clsJbAccess.UpdateJibanData(strSqlString, listParam) Then
                                strUpdateKeyInfo = "\r\n店コード:" & recHaitaTargetInfo.MiseCd _
                                                & "\r\n分類コード:" & recHaitaTargetInfo.BunruiCd _
                                                & "\r\n入力日:" & recHaitaTargetInfo.NyuuryokuDate _
                                                & "\r\n入力日NO:" & recHaitaTargetInfo.NyuuryokuDateNo
                                strRetMsg = String.Format(Messages.MSG064E, strProcess, UPDATE_TABLE_NAME, strUpdateKeyInfo)
                                Return strRetMsg
                                Exit Function
                            End If
                            '店別請求連携管理テーブルの更新
                            Select Case recUpdateDisplayInfo.SqlTypeFlg
                                Case TenbetuSeikyuuRecord.enSqlTypeFlg.UPDATE
                                    recTenbetuRenkei.RenkeiSijiCd = 2
                                    recTenbetuRenkei.SousinJykyCd = 0
                                    recTenbetuRenkei.IsUpdate = True
                                Case TenbetuSeikyuuRecord.enSqlTypeFlg.DELETE
                                    recTenbetuRenkei.RenkeiSijiCd = 9
                                    recTenbetuRenkei.SousinJykyCd = 0
                                    recTenbetuRenkei.IsUpdate = False
                                Case Else
                                    recTenbetuRenkei.RenkeiSijiCd = 1
                                    recTenbetuRenkei.SousinJykyCd = 0
                                    recTenbetuRenkei.IsUpdate = False
                            End Select
                            recTenbetuRenkei.MiseCd = recUpdateDisplayInfo.MiseCd
                            recTenbetuRenkei.BunruiCd = recUpdateDisplayInfo.BunruiCd
                            recTenbetuRenkei.NyuuryokuDate = recUpdateDisplayInfo.NyuuryokuDate
                            recTenbetuRenkei.NyuuryokuDateNo = recUpdateDisplayInfo.NyuuryokuDateNo
                            recTenbetuRenkei.UpdLoginUserId = recUpdateDisplayInfo.UpdLoginUserId
                            intResult += clsRenkeiLogic.EditTenbetuSeikyuuRenkeiData(recTenbetuRenkei)
                        End If
                    ElseIf recUpdateDisplayInfo.SqlTypeFlg <> EarthConst.enSqlTypeFlg.DELETE Then
                        '登録内容の設定
                        With recUpdateDisplayInfo
                            .NyuuryokuDate = DateTime.Today
                            MaxNoTable = clsDataAccess.GetUpdateTargetMaxNyuuryokuNo(.MiseCd, .BunruiCd, .NyuuryokuDate)
                            MaxNoRow = MaxNoTable.Rows(0)
                            .UriKeijyouFlg = 0
                            .NyuuryokuDateNo = MaxNoRow.max_no + 1
                        End With
                        '採番が100を超えたらエラー
                        If recHaitaTargetInfo.NyuuryokuDateNo > 100 Then
                            strUpdateKeyInfo = "\r\n店コード:" & recHaitaTargetInfo.MiseCd _
                                                                   & "\r\n分類コード:" & recHaitaTargetInfo.BunruiCd _
                                                                   & "\r\n入力日:" & recHaitaTargetInfo.NyuuryokuDate _
                                                                   & "\r\n入力日NO:" & recHaitaTargetInfo.NyuuryokuDateNo
                            strRetMsg = String.Format(Messages.MSG064E, INSERT_PROCESS, UPDATE_TABLE_NAME, strUpdateKeyInfo)
                            Return strRetMsg
                            Exit Function
                        End If
                        'SQL情報を自動生成するInterfaceのインスタンス化
                        IFsqlMake = New InsertStringHelper
                        '更新用文字列とパラメータの作成
                        strSqlString = IFsqlMake.MakeUpdateInfo(GetType(TenbetuSeikyuuRecord), recUpdateDisplayInfo, listParam)
                        'DB反映処理
                        If Not clsJbAccess.UpdateJibanData(strSqlString, listParam) Then
                            strUpdateKeyInfo = "\r\n店コード:" & recHaitaTargetInfo.MiseCd _
                                            & "\r\n分類コード:" & recHaitaTargetInfo.BunruiCd _
                                            & "\r\n入力日:" & recHaitaTargetInfo.NyuuryokuDate _
                                            & "\r\n入力日NO:" & recHaitaTargetInfo.NyuuryokuDateNo
                            strRetMsg = String.Format(Messages.MSG064E, INSERT_PROCESS, UPDATE_TABLE_NAME, strUpdateKeyInfo)
                            Return strRetMsg
                            Exit Function
                        End If
                        '店別請求連携管理テーブルの登録
                        recTenbetuRenkei.MiseCd = recUpdateDisplayInfo.MiseCd
                        recTenbetuRenkei.BunruiCd = recUpdateDisplayInfo.BunruiCd
                        recTenbetuRenkei.RenkeiSijiCd = 1
                        recTenbetuRenkei.SousinJykyCd = 0
                        recTenbetuRenkei.NyuuryokuDate = recUpdateDisplayInfo.NyuuryokuDate
                        recTenbetuRenkei.NyuuryokuDateNo = recUpdateDisplayInfo.NyuuryokuDateNo
                        recTenbetuRenkei.UpdLoginUserId = recUpdateDisplayInfo.UpdLoginUserId
                        recTenbetuRenkei.IsUpdate = False
                        intResult += clsRenkeiLogic.EditTenbetuSeikyuuRenkeiData(recTenbetuRenkei)
                    End If
                Next
                'トランザクションのコミット
                scope.Complete()
            End Using

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = 1205 Then    'エラーキャッチ：デッドロック
                strRetMsg = String.Format(Messages.MSG107E, exSqlException.Number)
            ElseIf exSqlException.Number = -2 Then  'エラーキャッチ：タイムアウト
                strRetMsg = String.Format(Messages.MSG107E, exSqlException.Number)
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
        Catch exTransactionException As System.Transactions.TransactionException
            'エラーキャッチ：トランザクションエラー
            UnTrappedExceptionManager.Publish(exTransactionException)
            strRetMsg = String.Format(Messages.MSG107E, exTransactionException.ToString)
        End Try

        Return strRetMsg
    End Function

    ''' <summary>
    ''' 最大入力日Noを取得します
    ''' </summary>
    ''' <param name="strMiseCd">店コード</param>
    ''' <param name="strBunruiCd">分類コード</param>
    ''' <param name="dtNyuuryokuDate">入力日</param>
    ''' <returns>最大入力日No</returns>
    ''' <remarks></remarks>
    Public Function GetMaxNyuuryokuDateNo(ByVal strMiseCd As String, ByVal strBunruiCd As String, ByVal dtNyuuryokuDate As Date) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetMaxNyuuryokuNo" _
                                                            , strMiseCd _
                                                            , strBunruiCd _
                                                            , dtNyuuryokuDate)
        Dim clsDataAccess As New TenbetuSyuuseiDataAccess
        Dim MaxNoTable As TenbetuSeikyuuDataSet.MaxNoDataTable
        Dim MaxNoRow As TenbetuSeikyuuDataSet.MaxNoRow
        Dim intMaxNyuuryokuDateNo As Integer

        MaxNoTable = clsDataAccess.GetUpdateTargetMaxNyuuryokuNo(strMiseCd, strBunruiCd, dtNyuuryokuDate)
        MaxNoRow = MaxNoTable.Rows(0)
        intMaxNyuuryokuDateNo = MaxNoRow.max_no

        Return intMaxNyuuryokuDateNo
    End Function

#Region "加盟店マスタ.販促品請求締め日"
    ''' <summary>
    ''' 加盟店の販促品請求書締め日を取得し、請求書発行日を算出します
    ''' </summary>
    ''' <param name="strMiseCd">店コード</param>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <param name="strCalcDate">算出基準日</param>
    ''' <returns>販促品請求書発行日</returns>
    ''' <remarks>
    ''' 加盟店マスタの販促品請求締め日より次回締め日を算出し返却します<br/>
    ''' 商品コードが指定されている場合は請求先マスタから算出します
    ''' <example>
    ''' 当日:2009/02/15 締め日:20 の場合    ⇒ 2009/02/20 <br/>
    ''' 当日:2009/02/25 締め日:20 の場合    ⇒ 2009/03/20 <br/>
    ''' 当日:2009/02/25 締め日:不正値の場合 ⇒ 2009/02/28 (当月末)<br/>
    ''' </example>
    ''' </remarks>
    Public Function GetHansokuhinSeikyuusyoHakkoudate(ByVal strMiseCd As String, ByVal strSyouhinCd As String, Optional ByVal strCalcDate As String = "") As DateTime
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHansokuhinSeikyuusyoHakkoudate", _
                                                    strMiseCd)
        ' 戻り値
        Dim editDate As Date
        Dim dataAccess As New TenbetuSyuuseiDataAccess
        Dim simeDate As String
        Dim cmnBizLogic As New CommonBizLogic

        ' 調査請求書締め日を取得
        If enIsFc = IsFcMode.NOT_FC Then
            simeDate = cmnBizLogic.GetSeikyuuSimeDateFromKameiten(String.Empty, String.Empty, String.Empty, strMiseCd, strSyouhinCd)
        Else
            simeDate = cmnBizLogic.GetSeikyuuSimeDateFromEigyousyo(String.Empty, String.Empty, String.Empty, strMiseCd)
        End If

        editDate = cmnBizLogic.CalcSeikyuusyoHakkouDate(simeDate, strCalcDate)

        ' 請求書発行日をセット
        Return editDate

    End Function
#End Region

End Class

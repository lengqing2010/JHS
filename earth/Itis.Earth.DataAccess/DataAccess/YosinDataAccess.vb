Imports System.text
Imports System.Data.SqlClient
Imports System.Configuration

Public Class YosinDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' コネクションストリング
    ''' </summary>
    ''' <remarks></remarks>
    Private connStr As String = ConnectionManager.Instance.ConnectionString

#Region "与信管理マスタ取得"
    ''' <summary>
    ''' 与信管理マスタのデータ取得を行う
    ''' </summary>
    ''' <param name="intSeikyusakiType">1:調査請求先、2:工事請求先、3:販促品請求先</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="blnDelete">取消対象フラグ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetYosinKanriData(ByVal intSeikyusakiType As Integer, _
                                        ByVal strKameitenCd As String, _
                                        ByVal blnDelete As Boolean) As YosinDataSet.YosinKanriTableDataTable

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetYosinKanriData", _
                                                    intSeikyusakiType, _
                                                    strKameitenCd, _
                                                    blnDelete)

        ' パラメータ
        Const strParamKameitenCd As String = "@KAMEITENCD"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("   YO.nayose_saki_cd ")
        commandTextSb.Append("   , YO.torikesi ")
        commandTextSb.Append("   , YO.nayose_saki_name1 ")
        commandTextSb.Append("   , YO.nayose_saki_name2 ")
        commandTextSb.Append("   , YO.nayose_saki_kana1 ")
        commandTextSb.Append("   , YO.nayose_saki_kana2 ")
        commandTextSb.Append("   , ISNULL(YO.yosin_gendo_gaku," & Integer.MinValue & ") AS yosin_gendo_gaku")
        commandTextSb.Append("   , YO.yosin_keikou_kaisiritsu ")
        commandTextSb.Append("   , YO.todouhuken_cd ")
        commandTextSb.Append("   , ISNULL(YO.keikoku_joukyou," & Integer.MinValue & ") AS keikoku_joukyou")
        commandTextSb.Append("   , YO.soushin_date ")
        commandTextSb.Append("   , YO.tyoku_koji_flg ")
        commandTextSb.Append("   , YO.zenjitsu_koji_flg ")
        commandTextSb.Append("   , YO.jyutyuu_kanri_flg ")
        commandTextSb.Append("   , YO.teikoku_hyouten ")
        commandTextSb.Append("   , YO.zengetsu_saiken_gaku ")
        commandTextSb.Append("   , YO.zengetsu_saiken_set_date ")
        commandTextSb.Append("   , YO.ruiseki_nyuukin_gaku ")
        commandTextSb.Append("   , YO.ruiseki_nyuukin_set_date ")
        commandTextSb.Append("   , YO.ruiseki_kasiuri_gaku ")
        commandTextSb.Append("   , YO.ruiseki_kasiuri_set_date ")
        commandTextSb.Append("   , YO.ruiseki_jyutyuu_gaku ")
        commandTextSb.Append("   , YO.ruiseki_jyutyuu_set_date ")
        commandTextSb.Append("   , YO.toujitsu_jyutyuu_gaku ")
        commandTextSb.Append("   , YO.tantou_busyo_cd01 ")
        commandTextSb.Append("   , YO.tantou_busyo_cd02 ")
        commandTextSb.Append("   , YO.tantou_busyo_cd03 ")
        commandTextSb.Append("   , YO.tantou_busyo_cd04 ")
        commandTextSb.Append("   , YO.tantou_busyo_cd05 ")
        commandTextSb.Append("   , YO.tantou_busyo_cd06 ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("   m_kameiten KA  ")
        commandTextSb.Append("   LEFT OUTER JOIN m_nayose NA  ")
        Select Case intSeikyusakiType
            Case 1
                commandTextSb.Append("     ON NA.seikyuu_saki_cd = KA.tys_seikyuu_saki  ")
            Case 2
                commandTextSb.Append("     ON NA.seikyuu_saki_cd = KA.koj_seikyuusaki  ")
            Case 3
                commandTextSb.Append("     ON NA.seikyuu_saki_cd = KA.hansokuhin_seikyuusaki  ")
        End Select
        commandTextSb.Append("   LEFT OUTER JOIN m_yosinkanri YO  ")
        commandTextSb.Append("     ON YO.nayose_saki_cd = NA.nayose_saki_cd  ")
        commandTextSb.Append(" WHERE ")
        commandTextSb.Append("   0 = 0 ")
        If blnDelete = True Then
            commandTextSb.Append("   AND ISNULL(KA.torikesi,0) = 0  ")
            commandTextSb.Append("   AND ISNULL(NA.torikesi,0) = 0  ")
            commandTextSb.Append("   AND ISNULL(YO.torikesi,0) = 0  ")
        End If
        commandTextSb.Append("   AND KA.kameiten_cd = " & strParamKameitenCd)

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.VarChar, 5, strKameitenCd)}

        ' データの取得
        Dim YosinDataSet As New YosinDataSet()

        ' 検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            YosinDataSet, YosinDataSet.YosinKanriTable.TableName, commandParameters)

        Dim YosinKanriTable As YosinDataSet.YosinKanriTableDataTable = _
                    YosinDataSet.YosinKanriTable

        Return YosinKanriTable

    End Function
#End Region

#Region "与信管理マスタ更新"
    ''' <summary>
    ''' 与信管理マスタ更新
    ''' </summary>
    ''' <param name="strNayoseSakiCode">名寄先コード</param>
    ''' <param name="intKeikokuJoukyou">警告状況</param>
    ''' <param name="dateSousinDate">送信日付</param>
    ''' <param name="intToujitsuJyutyuuGaku">当日受注額</param>
    ''' <returns>更新結果</returns>
    ''' <remarks></remarks>
    Public Function EditYosinKanri(ByVal strNayoseSakiCode As String, _
                                ByVal intKeikokuJoukyou As Integer, _
                                ByVal dateSousinDate As DateTime, _
                                ByVal intToujitsuJyutyuuGaku As Integer, _
                                ByVal strUpdUserId As String, _
                                ByVal dateUpdDate As DateTime) As Integer

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EditYosinKanri", _
                                                    strNayoseSakiCode, _
                                                    intKeikokuJoukyou, _
                                                    dateSousinDate, _
                                                    intToujitsuJyutyuuGaku, _
                                                    strUpdUserId, _
                                                    dateUpdDate)

        Dim intResult As Integer = 0
        Dim strCommandText As String = ""
        Dim cmdParams(6) As SqlClient.SqlParameter

        Const strParamNayoseSakiCode As String = "@NAYOSESAKICD"
        Const strParamKeikokuJoukyou As String = "@KEIKOKUJOUKYOU"
        Const strParamSousinDate As String = "@SOUSINDATE"
        Const strParamToujitsuJyutyuuGaku As String = "@TOUJITUJUTYUUGAKU"
        Const strParamUpdUserId As String = "@UPDUSERID"
        Const strParamUpdDate As String = "@UPDDATE"


        cmdParams(0) = MakeParam(strParamNayoseSakiCode, SqlDbType.VarChar, 5, strNayoseSakiCode)
        cmdParams(1) = MakeParam(strParamKeikokuJoukyou, SqlDbType.Int, 1, IIf(intKeikokuJoukyou = Integer.MinValue, DBNull.Value, intKeikokuJoukyou))
        cmdParams(2) = MakeParam(strParamSousinDate, SqlDbType.DateTime, 16, IIf(dateSousinDate = DateTime.MinValue, DBNull.Value, dateSousinDate))
        cmdParams(3) = MakeParam(strParamToujitsuJyutyuuGaku, SqlDbType.Int, 10, IIf(intToujitsuJyutyuuGaku = Integer.MinValue, DBNull.Value, intToujitsuJyutyuuGaku))
        cmdParams(4) = MakeParam(strParamUpdUserId, SqlDbType.VarChar, 30, strUpdUserId)
        cmdParams(5) = MakeParam(strParamUpdDate, SqlDbType.DateTime, 16, IIf(dateUpdDate = DateTime.MinValue, DBNull.Value, dateUpdDate))

        strCommandText = _
            " UPDATE m_yosinkanri SET " & _
            "     keikoku_joukyou = " & strParamKeikokuJoukyou & _
            "     , soushin_date = " & strParamSousinDate & _
            "     , toujitsu_jyutyuu_gaku = " & strParamToujitsuJyutyuuGaku & _
            "     , upd_login_user_id = " & strParamUpdUserId & _
            "     , upd_datetime = " & strParamUpdDate & _
            " WHERE nayose_saki_cd = " & strParamNayoseSakiCode

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    strCommandText, _
                                    cmdParams)

        Return intResult

    End Function
#End Region

#Region "チェック結果メール送信先取得"
    ''' <summary>
    ''' チェック結果メール送信先取得
    ''' </summary>
    ''' <param name="intSeikyusakiType">1:調査請求先、2:工事請求先、3:販促品請求先</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="blnDelete">取消対象フラグ</param>
    ''' <returns>メール送信先レコード</returns>
    ''' <remarks></remarks>
    Public Function GetYosinMailTarget(ByVal intSeikyusakiType As Integer, _
                                        ByVal strKameitenCd As String, _
                                        ByVal intYosinGendoResult As Integer, _
                                        ByVal blnDelete As Boolean) As YosinDataSet.YosinMailTargetTableDataTable

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetYosinMailTarget", _
                                                    intSeikyusakiType, _
                                                    strKameitenCd, _
                                                    intYosinGendoResult, _
                                                    blnDelete)

        ' パラメータ
        Const strParamKameitenCd As String = "@KAMEITENCD"

        Dim commandTextSb As New StringBuilder()
        Dim NotMailTargetBusyoLevelSQL As String = ConfigurationManager.AppSettings("NotMailTargetBusyoLevelSQL")


        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("   KA.kameiten_cd ")
        commandTextSb.Append("   , KA.kameiten_mei1 ")
        commandTextSb.Append("   , NA.nayose_saki_cd ")
        commandTextSb.Append("   , YO.nayose_saki_name1 ")
        commandTextSb.Append("   , YO.keikoku_joukyou ")
        commandTextSb.Append("   , MI.meisyou ")
        commandTextSb.Append("   , MB.PrimaryWindowsNTAccount ")
        commandTextSb.Append("   , MB.DisplayName ")
        commandTextSb.Append("   , MB.EmailAddresses ")
        commandTextSb.Append("   , BU.busyo_cd ")
        commandTextSb.Append("   , BU.busyo_mei ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("   m_kameiten KA  ")
        commandTextSb.Append("   LEFT OUTER JOIN m_nayose NA  ")
        Select Case intSeikyusakiType
            Case 1
                commandTextSb.Append("     ON NA.seikyuu_saki_cd = KA.tys_seikyuu_saki  ")
            Case 2
                commandTextSb.Append("     ON NA.seikyuu_saki_cd = KA.koj_seikyuusaki  ")
            Case 3
                commandTextSb.Append("     ON NA.seikyuu_saki_cd = KA.hansokuhin_seikyuusaki  ")
        End Select
        commandTextSb.Append("   LEFT OUTER JOIN m_yosinkanri YO  ")
        commandTextSb.Append("     ON YO.nayose_saki_cd = NA.nayose_saki_cd  ")
        commandTextSb.Append("   LEFT OUTER JOIN m_meisyou MI  ")
        commandTextSb.Append("     ON MI.meisyou_syubetu = '52'  ")
        If intYosinGendoResult = EarthConst.YOSIN_ERROR_YOSIN_NG Then
            commandTextSb.Append("     AND MI.code = " & EarthConst.YOSIN_KEIKOKU_OVER)
        Else
            commandTextSb.Append("     AND MI.code = YO.keikoku_joukyou  ")
        End If
        commandTextSb.Append("   LEFT OUTER JOIN m_busyo_kanri BU  ")
        commandTextSb.Append("     ON BU.busyo_cd IN (  ")
        commandTextSb.Append("       YO.tantou_busyo_cd01 ")
        commandTextSb.Append("       , YO.tantou_busyo_cd02 ")
        commandTextSb.Append("       , YO.tantou_busyo_cd03 ")
        commandTextSb.Append("       , YO.tantou_busyo_cd04 ")
        commandTextSb.Append("       , YO.tantou_busyo_cd05 ")
        commandTextSb.Append("       , YO.tantou_busyo_cd06 ")
        commandTextSb.Append("     )  ")
        commandTextSb.Append("   LEFT OUTER JOIN m_kanrityou KN  ")
        commandTextSb.Append("     ON KN.busyo_cd = BU.busyo_cd  ")
        commandTextSb.Append("   LEFT OUTER JOIN m_jhs_mailbox MB  ")
        commandTextSb.Append("     ON MB.PrimaryWindowsNTAccount = KN.login_user_id  ")
        commandTextSb.Append(" WHERE ")
        commandTextSb.Append("   0 = 0 ")
        commandTextSb.Append("   AND KA.kameiten_cd = " & strParamKameitenCd)
        commandTextSb.Append("   AND BU.sosiki_level NOT IN (" & NotMailTargetBusyoLevelSQL & ")")
        If blnDelete = True Then
            commandTextSb.Append("   AND ISNULL(KA.torikesi,0) = 0  ")
            commandTextSb.Append("   AND ISNULL(NA.torikesi,0) = 0  ")
            commandTextSb.Append("   AND ISNULL(YO.torikesi,0) = 0  ")
            commandTextSb.Append("   AND ISNULL(BU.torikesi,0) = 0  ")
            commandTextSb.Append("   AND ISNULL(KN.torikesi,0) = 0  ")
        End If

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.VarChar, 5, strKameitenCd)}

        ' データの取得
        Dim YosinDataSet As New YosinDataSet()

        ' 検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            YosinDataSet, YosinDataSet.YosinMailTargetTable.TableName, commandParameters)

        Dim YosinMailTargetTable As YosinDataSet.YosinMailTargetTableDataTable = _
                    YosinDataSet.YosinMailTargetTable

        Return YosinMailTargetTable

    End Function
#End Region

#Region "与信管理者マスタ取得"
    ''' <summary>
    ''' 与信管理者マスタのデータ取得を行う
    ''' </summary>
    ''' <param name="blnDelete">取消対象フラグ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetYosinKanrisyaData(ByVal blnDelete As Boolean) As YosinDataSet.YosinKanrisyaTableDataTable

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetYosinKanrisyaData", _
                                                    blnDelete)

        'SQL文
        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("   login_user_id ")
        commandTextSb.Append("   , torikesi ")
        commandTextSb.Append("   , add_login_user_id ")
        commandTextSb.Append("   , add_datetime ")
        commandTextSb.Append("   , upd_login_user_id ")
        commandTextSb.Append("   , upd_datetime ")
        commandTextSb.Append("   , EmailAddresses ")
        commandTextSb.Append("   , DisplayName ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("   m_yosin_kanrisya YK")
        commandTextSb.Append("   LEFT OUTER JOIN m_jhs_mailbox MB  ")
        commandTextSb.Append("     ON MB.PrimaryWindowsNTAccount = YK.login_user_id  ")
        commandTextSb.Append(" WHERE ")
        commandTextSb.Append("   0 = 0 ")
        commandTextSb.Append("   AND ISNULL(YK.torikesi,0) = 0  ")

        ' データの取得
        Dim YosinDataSet As New YosinDataSet()

        ' 検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            YosinDataSet, YosinDataSet.YosinKanrisyaTable.TableName)

        Dim YosinKanrisyaTable As YosinDataSet.YosinKanrisyaTableDataTable = _
                    YosinDataSet.YosinKanrisyaTable

        Return YosinKanrisyaTable

    End Function
#End Region

#Region "営業担当者メールアドレスの取得"
    ''' <summary>
    ''' 営業担当者メールアドレスの取得を行う
    ''' </summary>
    ''' <param name="blnDelete">取消対象フラグ</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetYosinEigyouTantousyaMailTarget(ByVal strKameitenCd As String, _
                                                    ByVal blnDelete As Boolean) As YosinDataSet.EigyouTantousyaMailTargetTableDataTable

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetYosinKanrisyaData", _
                                                    strKameitenCd, _
                                                    blnDelete)
        ' パラメータ
        Const strParamKameitenCd As String = "@KAMEITENCD"

        'SQL文
        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" select ")
        commandTextSb.Append("   KM.kameiten_cd ")
        commandTextSb.Append("   , KM.eigyou_tantousya_mei ")
        commandTextSb.Append("   , KM.kyuu_eigyou_tantousya_mei ")
        commandTextSb.Append("   , KM.hikitugi_kanryou_date ")
        commandTextSb.Append("   , MB.EmailAddresses ")
        commandTextSb.Append(" from ")
        commandTextSb.Append("   m_kameiten KM  ")
        commandTextSb.Append("   LEFT OUTER JOIN m_jhs_mailbox MB  ")
        commandTextSb.Append("     ON MB.PrimaryWindowsNTAccount = CASE  ")
        commandTextSb.Append("       WHEN KM.hikitugi_kanryou_date >= GETDATE()  ")
        commandTextSb.Append("       THEN KM.kyuu_eigyou_tantousya_mei  ")
        commandTextSb.Append("       ELSE KM.eigyou_tantousya_mei  ")
        commandTextSb.Append("       END  ")
        commandTextSb.Append(" where ")
        commandTextSb.Append("   0 = 0 ")
        commandTextSb.Append("   AND KM.kameiten_cd = " & strParamKameitenCd)
        If blnDelete = True Then
            commandTextSb.Append("   AND ISNULL(KM.torikesi, 0) = 0 ")
        End If

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.VarChar, 5, strKameitenCd)}

        ' データの取得
        Dim YosinDataSet As New YosinDataSet()

        ' 検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            YosinDataSet, YosinDataSet.EigyouTantousyaMailTargetTable.TableName, commandParameters)

        Dim EigyouTantousyaMailTargetTable As YosinDataSet.EigyouTantousyaMailTargetTableDataTable = _
                    YosinDataSet.EigyouTantousyaMailTargetTable

        Return EigyouTantousyaMailTargetTable

    End Function
#End Region

#Region "与信チェック対象管理テーブル削除"
    ''' <summary>
    ''' 与信チェック対象管理テーブル追加
    ''' </summary>
    ''' <returns>処理結果</returns>
    ''' <remarks></remarks>
    Public Function DeleteYosinCheckTaisyouKanri() As Integer

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".DeleteYosinCheckTaisyouKanri")

        Dim intResult As Integer = 0

        '30日以前のデータを削除
        Dim strCommandText As String = ""

        strCommandText = _
            "DELETE " & _
            "FROM " & _
            "  t_yosin_check_taisyou_kanri " & _
            "WHERE " & _
            "  upd_datetime < DATEADD(dd, - 30, GETDATE()) "

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    strCommandText)

        Return intResult

    End Function
#End Region

#Region "与信チェック対象管理テーブル追加"
    ''' <summary>
    ''' 与信チェック対象管理テーブル追加
    ''' </summary>
    ''' <param name="strSyoriId">処理ID(進捗データ連携バッチ処理ID)</param>
    ''' <param name="flgRecvSts">進捗テーブル受信ステータス 対象フラグ</param>
    ''' <returns>処理結果</returns>
    ''' <remarks></remarks>
    Public Function InsertYosinCheckTaisyouKanri(ByVal strSyoriId As String, _
                                                 ByVal flgRecvSts As Integer) As Integer

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".InsertYosinCheckTaisyouKanri", _
                                                    strSyoriId, _
                                                    flgRecvSts)

        Dim intResult As Integer = 0
        'データインサート処理
        Dim strCommandText As String = ""

        Const strParamSyoriId As String = "@SHORIID"

        strCommandText = _
            "INSERT " & _
            "INTO t_yosin_check_taisyou_kanri ( " & _
            "  syori_id" & _
            "  , kbn" & _
            "  , hosyousyo_no" & _
            "  , kameiten_cd" & _
            "  , nayose_saki_cd" & _
            "  , m_yosinkanri_upd_login_user_id" & _
            "  , m_yosinkanri_upd_datetime" & _
            "  , tys_uri_gaku_goukei" & _
            "  , yosin_check_syori_sts" & _
            "  , add_login_user_id" & _
            "  , add_datetime" & _
            "  , upd_login_user_id" & _
            "  , upd_datetime" & _
            ") " & _
            "SELECT" & _
            "  " & strParamSyoriId & " AS syori_id" & _
            "  , TJ.kbn" & _
            "  , TJ.hosyousyo_no" & _
            "  , TJ.kameiten_cd" & _
            "  , YO.nayose_saki_cd" & _
            "  , ISNULL(YO.upd_login_user_id, YO.add_login_user_id) AS m_yosinkanri_upd_login_user_id" & _
            "  , ISNULL(YO.upd_datetime, YO.add_datetime) AS m_yosinkanri_upd_datetime" & _
            "  , SUM( " & _
            "    CASE " & _
            "      WHEN TS.uri_date IS NOT NULL " & _
            "      THEN TS.uri_gaku " & _
            "      WHEN ISNULL( " & _
            "        TJ.syoudakusyo_tys_date" & _
            "        , ISNULL(TJ.tys_kibou_date, '')" & _
            "      ) BETWEEN DATEADD(dd, - 30, GETDATE()) AND DATEADD(dd, 30, GETDATE()) " & _
            "      THEN TS.uri_gaku " & _
            "      ELSE 0 " & _
            "      END" & _
            "  ) AS tys_uri_gaku_goukei" & _
            "  , 0 AS yosin_check_syori_status" & _
            "  , 'yosin_check' AS add_login_user_id" & _
            "  , GETDATE() AS add_datetime" & _
            "  , 'yosin_check' AS upd_login_user_id" & _
            "  , GETDATE() AS upd_datetime " & _
            "FROM" & _
            "  ReportIF RI WITH (READCOMMITTED)  " & _
            "  LEFT OUTER JOIN t_jiban TJ WITH (READCOMMITTED)  " & _
            "    ON SUBSTRING(RI.kokyaku_no, 1, 1) = TJ.kbn " & _
            "    AND SUBSTRING(RI.kokyaku_no, 2, 10) = TJ.hosyousyo_no " & _
            "  LEFT OUTER JOIN m_kameiten KA WITH (READCOMMITTED)  " & _
            "    ON TJ.kbn = KA.kbn " & _
            "    AND TJ.kameiten_cd = KA.kameiten_cd " & _
            "  LEFT OUTER JOIN m_nayose NA WITH (READCOMMITTED)  " & _
            "    ON NA.seikyuu_saki_cd = KA.tys_seikyuu_saki " & _
            "  LEFT OUTER JOIN m_yosinkanri YO WITH (READCOMMITTED)  " & _
            "    ON YO.nayose_saki_cd = NA.nayose_saki_cd " & _
            "  LEFT OUTER JOIN t_teibetu_seikyuu TS WITH (READCOMMITTED)  " & _
            "    ON TJ.kbn = TS.kbn " & _
            "    AND TJ.hosyousyo_no = TS.hosyousyo_no " & _
            "    AND TS.bunrui_cd IN ('100', '110', '115', '120', '180') " & _
            "WHERE" & _
            "  RI.recv_sts IN ('25', '30') " & _
            "GROUP BY" & _
            "  TJ.kbn" & _
            "  , TJ.hosyousyo_no" & _
            "  , TJ.kameiten_cd" & _
            "  , YO.nayose_saki_cd" & _
            "  , ISNULL(YO.upd_login_user_id, YO.add_login_user_id)" & _
            "  , ISNULL(YO.upd_datetime, YO.add_datetime)"

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamSyoriId, SqlDbType.VarChar, 30, strSyoriId)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    strCommandText, _
                                    commandParameters)

        Return intResult

    End Function
#End Region

#Region "与信チェック対象管理テーブル取得"
    ''' <summary>
    ''' 与信チェック対象管理テーブル取得を行う
    ''' </summary>
    ''' <param name="strSyoriId">処理ID(進捗データ連携バッチ処理ID)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetYosinCheckTaisyouKanriData(ByVal strSyoriId As String) As YosinDataSet.YosinCheckTaisyouKanriTableDataTable

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetYosinCheckTaisyouKanriData", _
                                                    strSyoriId)

        'パラメータ名設定
        Const strParamSyoriId As String = "@SHORIID"

        'SQL文
        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("   YC.syori_id ")
        commandTextSb.Append("   , YC.kbn ")
        commandTextSb.Append("   , YC.hosyousyo_no ")
        commandTextSb.Append("   , YC.kameiten_cd ")
        commandTextSb.Append("   , YC.nayose_saki_cd ")
        commandTextSb.Append("   , YC.m_yosinkanri_upd_login_user_id ")
        commandTextSb.Append("   , YC.m_yosinkanri_upd_datetime ")
        commandTextSb.Append("   , YC.tys_uri_gaku_goukei ")
        commandTextSb.Append("   , YC.yosin_check_syori_sts ")
        commandTextSb.Append("   , YC.add_login_user_id ")
        commandTextSb.Append("   , YC.add_datetime ")
        commandTextSb.Append("   , YC.upd_login_user_id ")
        commandTextSb.Append("   , YC.upd_datetime  ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("   t_yosin_check_taisyou_kanri YC  ")
        commandTextSb.Append("   INNER JOIN jhs_sys.m_yosinkanri YK WITH (UPDLOCK)  ")
        commandTextSb.Append("     ON YC.nayose_saki_cd = YK.nayose_saki_cd  ")
        commandTextSb.Append("     AND YC.m_yosinkanri_upd_datetime = ISNULL(YK.upd_datetime, YK.add_datetime)  ")
        commandTextSb.Append(" WHERE ")
        commandTextSb.Append("   syori_id = " & strParamSyoriId)

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamSyoriId, SqlDbType.VarChar, 30, strSyoriId)}

        ' データの取得
        Dim YosinDataSet As New YosinDataSet()

        ' 検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            YosinDataSet, YosinDataSet.YosinCheckTaisyouKanriTable.TableName, commandParameters)

        Dim YosinCheckTaisyouKanriTable As YosinDataSet.YosinCheckTaisyouKanriTableDataTable = _
                    YosinDataSet.YosinCheckTaisyouKanriTable

        Return YosinCheckTaisyouKanriTable

    End Function
#End Region

End Class

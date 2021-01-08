Option Explicit On
Option Strict On

Imports System.Data
Imports System.Text
Imports System.IO
Imports System.Transactions
Imports JHS.Batch.SqlExecutor
Imports JHS.Batch
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase

''' <summary>
''' 実績情報を実績管理ﾃｰﾌﾞﾙに格納する
''' </summary>
''' <remarks>計画管理表に表示させるための実績情報を実績管理ﾃｰﾌﾞﾙに格納させる。</remarks>
''' <history>
''' <para>2012/10/18 大連/王新 新規作成 P-44979</para>
''' </history>
Public Class S0004
#Region "定数"
    'バッチID
    Private Const CON_BATCH_ID As String = "bat_set4"
#End Region

#Region "変数"
    '各Event/Methodの動作時における、"EMAB障害対応情報の格納処理"向け、自クラス名
    Private ReadOnly mMyNamePeriod As String = MyClass.GetType.FullName
    'DB接続ストリング
    Private mDBconnectionEarth As String
    Private mDBconnectionJHS As String
    'DB接続
    Private mConnectionEarth As SqlExecutor
    Private mConnectionJHS As SqlExecutor
    'ログメッセージ
    Private mLogMsg As New StringBuilder()
    '新規件数
    Private mInsCount As Integer = 0
#End Region

#Region "Main処理"
    ''' <summary>
    ''' Main処理
    ''' </summary>
    ''' <param name="argv"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/01/15 大連/王新 新規作成 P-44979</para>
    ''' </history>
    Public Shared Function Main(ByVal argv As String()) As Integer
        Dim btProcess As S0004

        '初期化
        btProcess = New S0004()

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(btProcess.mMyNamePeriod & MyMethod.GetCurrentMethod.Name, argv)

        Try
            'DB接続ストリング
            btProcess.mDBconnectionEarth = Definition.GetConnectionStringEarth()
            btProcess.mDBconnectionJHS = Definition.GetConnectionStringJHS()

            'DB接続
            btProcess.mConnectionEarth = New SqlExecutor(btProcess.mDBconnectionEarth)
            btProcess.mConnectionJHS = New SqlExecutor(btProcess.mDBconnectionJHS)

            '主処理を呼び込む()
            Call btProcess.Main_Process()

            Return 0
        Catch ex As Exception
            Dim strErrorMsg As String = ""
            If ex.Data.Item("ERROR_LOG") IsNot Nothing Then
                strErrorMsg = Convert.ToString(ex.Data.Item("ERROR_LOG"))
                btProcess.mLogMsg.AppendLine(Format(Date.Now, "yyyy年MM月dd日 HH:mm:ss  ") & strErrorMsg)
            End If

            '異常を発生する場合、ログファイルに出力する
            btProcess.mLogMsg.AppendLine(ex.Message)

            btProcess.mLogMsg.AppendLine(Format(Date.Now, "yyyy年MM月dd日 HH:mm:ss  ") & "上記の処理をロールバックしました。")
            btProcess.mInsCount = 0
            Return 9
        Finally
            If btProcess.mConnectionEarth IsNot Nothing Then
                'DB接続のグルーズ
                btProcess.mConnectionEarth.Close()
                btProcess.mConnectionEarth.Dispose()
            End If

            If btProcess.mConnectionJHS IsNot Nothing Then
                'DB接続のグルーズ
                btProcess.mConnectionJHS.Close()
                btProcess.mConnectionJHS.Dispose()
            End If

            '新規件数をログファイルに出力する
            btProcess.mLogMsg.AppendLine(Format(Date.Now, "yyyy年MM月dd日 HH:mm:ss  ") & _
            "実績管理テーブルに" & _
            Convert.ToString(btProcess.mInsCount) & _
            "件データが挿入されました。")

            'ログ出力
            Console.WriteLine(btProcess.mLogMsg.ToString())
        End Try
    End Function

#End Region

#Region "主処理"
    ''' <summary>
    ''' 主処理
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/01/15 大連/王新 新規作成 P-44979</para>
    ''' </history>
    Private Sub Main_Process()
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(mMyNamePeriod & MyMethod.GetCurrentMethod.Name)

        Dim dtYear As DataTable                 '計画年度
        Dim strYear As String                   '計画年度
        Dim strBeginMonth As String             '開始月
        Dim strEndMonth As String               '結束月
        Dim dtEarthData As DataTable            'Ｅａｒｔｈデータ
        Dim dtZennenEarthData As DataTable      '前年Ｅａｒｔｈデータ
        Dim drData() As DataRow
        Dim options As New Transactions.TransactionOptions
        Dim i As Integer
        Dim j As Integer

        mLogMsg.AppendLine(Format(Date.Now, "yyyy年MM月dd日 HH:mm:ss  ") & _
                            "計画年度ＩＮＩファイル読取る処理を開始しました。")

        '計画年度ＣＴＬファイルを読込み、計画年度を取得する
        dtYear = Definition.GetKeikakuNendo("S0004")

        '正常に読取ることができなかった場合、終了する
        If dtYear.Rows.Count <= 0 Then
            Exit Sub
        End If

        mLogMsg.AppendLine(Format(Date.Now, "yyyy年MM月dd日 HH:mm:ss  ") & _
                            "Ｅａｒｔｈの実績管理データの取得処理を開始しました。")

        'DB接続のオープン
        mConnectionEarth.Open()
        mConnectionJHS.Open(True)

        Try
            For i = 0 To dtYear.Rows.Count - 1
                strYear = Convert.ToString(dtYear.Rows(i)("Year"))
                strBeginMonth = Convert.ToString(dtYear.Rows(i)("BeginMonth"))
                strEndMonth = Convert.ToString(dtYear.Rows(i)("EndMonth"))

                'Ｅａｒｔｈデータを取得する
                dtEarthData = SelEarthData(mConnectionEarth, strBeginMonth, strEndMonth)

                If dtEarthData IsNot Nothing Then
                    mLogMsg.AppendLine(Format(Date.Now, "yyyy年MM月dd日 HH:mm:ss  ") & _
                                        strYear & "年度にＥａｒｔｈの実績管理テーブルから" & _
                                        Convert.ToString(dtEarthData.Rows.Count) & _
                                        "件データが取得されました。")

                    '前年Ｅａｒｔｈデータを取得する
                    dtZennenEarthData = SelZennenEarthData(mConnectionEarth, strBeginMonth, strEndMonth)
                Else
                    Exit For
                End If

                '該当年度のデータを削除する
                DelJHSData(mConnectionJHS, strYear)

                dtEarthData.Columns("zennen_heikin_tanka").ReadOnly = False
                dtEarthData.Columns("zennen_siire_heikin_tanka").ReadOnly = False

                'Ｅａｒｔｈの実績管理データにより、ループする
                For j = 0 To dtEarthData.Rows.Count - 1
                    drData = dtZennenEarthData.Select("kameiten_cd = '" & Convert.ToString(dtEarthData.Rows(j)("kameiten_cd")) _
                                & "' AND syouhin_cd = '" & Convert.ToString(dtEarthData.Rows(j)("syouhin_cd")) & "'")

                    '前年データを設定する
                    If drData.Length > 0 Then
                        dtEarthData.Rows(j)("zennen_heikin_tanka") = drData(0)("heikin_tanka")
                        dtEarthData.Rows(j)("zennen_siire_heikin_tanka") = drData(0)("siire_heikin_tanka")
                    End If

                    '受注データワーク新規処理
                    mInsCount = mInsCount + InsJHSData(mConnectionJHS, strYear, dtEarthData.Rows(j))
                Next

                'データを解放する
                If dtEarthData IsNot Nothing Then
                    dtEarthData.Dispose()
                    dtEarthData = Nothing
                End If
            Next

            '成功の場合
            mConnectionJHS.Commit()
        Catch ex As Exception
            '失敗の場合
            mConnectionJHS.Rollback()
            Throw ex
        End Try
    End Sub
#End Region

#Region "SQL文"
    ''' <summary>
    ''' Ｅａｒｔｈのデータを取得する
    ''' </summary>
    ''' <param name="objConnection">DB接続</param>
    ''' <param name="strBeginMonth">開始月</param>
    ''' <param name="strEndMonth">結束月</param>
    ''' <returns>ＴＲＡＩＮ受注データ</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/01/15 大連/王新 新規作成 P-44979</para>
    ''' </history>
    Private Function SelEarthData(ByVal objConnection As SqlExecutor, _
                                  ByVal strBeginMonth As String, _
                                  ByVal strEndMonth As String) As Data.DataTable
        'SQLコメント
        Dim sqlBuffer As New System.Text.StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        '2013/10/23 李宇追加　↓
        '区分
        Dim strKubun As String
        strKubun = Definition.GetKubunName4
        '2013/10/23 李宇追加　↑

        Try
            'SQL文	
            'With sqlBuffer
            '    .AppendLine(" SELECT kameiten_mei1, ")
            '    .AppendLine(" kameiten_cd, ")
            '    .AppendLine(" syouhin_cd, ")
            '    .AppendLine(" syouhin_mei, ")
            '    .AppendLine(" SUM(uri_gaku4) AS uri_gaku4, ")
            '    .AppendLine(" SUM(uri_gaku5) AS uri_gaku5, ")
            '    .AppendLine(" SUM(uri_gaku6) AS uri_gaku6, ")
            '    .AppendLine(" SUM(uri_gaku7) AS uri_gaku7, ")
            '    .AppendLine(" SUM(uri_gaku8) AS uri_gaku8, ")
            '    .AppendLine(" SUM(uri_gaku9) AS uri_gaku9, ")
            '    .AppendLine(" SUM(uri_gaku10) AS uri_gaku10, ")
            '    .AppendLine(" SUM(uri_gaku11) AS uri_gaku11, ")
            '    .AppendLine(" SUM(uri_gaku12) AS uri_gaku12, ")
            '    .AppendLine(" SUM(uri_gaku1) AS uri_gaku1, ")
            '    .AppendLine(" SUM(uri_gaku2) AS uri_gaku2, ")
            '    .AppendLine(" SUM(uri_gaku3) AS uri_gaku3, ")
            '    .AppendLine(" SUM(uri_suu4) AS uri_suu4, ")
            '    .AppendLine(" SUM(uri_suu5) AS uri_suu5, ")
            '    .AppendLine(" SUM(uri_suu6) AS uri_suu6, ")
            '    .AppendLine(" SUM(uri_suu7) AS uri_suu7, ")
            '    .AppendLine(" SUM(uri_suu8) AS uri_suu8, ")
            '    .AppendLine(" SUM(uri_suu9) AS uri_suu9, ")
            '    .AppendLine(" SUM(uri_suu10) AS uri_suu10, ")
            '    .AppendLine(" SUM(uri_suu11) AS uri_suu11, ")
            '    .AppendLine(" SUM(uri_suu12) AS uri_suu12, ")
            '    .AppendLine(" SUM(uri_suu1) AS uri_suu1, ")
            '    .AppendLine(" SUM(uri_suu2) AS uri_suu2, ")
            '    .AppendLine(" SUM(uri_suu3) AS uri_suu3, ")
            '    .AppendLine(" SUM(siire_gaku4) AS siire_gaku4, ")
            '    .AppendLine(" SUM(siire_gaku5) AS siire_gaku5, ")
            '    .AppendLine(" SUM(siire_gaku6) AS siire_gaku6, ")
            '    .AppendLine(" SUM(siire_gaku7) AS siire_gaku7, ")
            '    .AppendLine(" SUM(siire_gaku8) AS siire_gaku8, ")
            '    .AppendLine(" SUM(siire_gaku9) AS siire_gaku9, ")
            '    .AppendLine(" SUM(siire_gaku10) AS siire_gaku10, ")
            '    .AppendLine(" SUM(siire_gaku11) AS siire_gaku11, ")
            '    .AppendLine(" SUM(siire_gaku12) AS siire_gaku12, ")
            '    .AppendLine(" SUM(siire_gaku1) AS siire_gaku1, ")
            '    .AppendLine(" SUM(siire_gaku2) AS siire_gaku2, ")
            '    .AppendLine(" SUM(siire_gaku3) AS siire_gaku3, ")
            '    .AppendLine(" SUM(siire_suu4) AS siire_suu4, ")
            '    .AppendLine(" SUM(siire_suu5) AS siire_suu5, ")
            '    .AppendLine(" SUM(siire_suu6) AS siire_suu6, ")
            '    .AppendLine(" SUM(siire_suu7) AS siire_suu7, ")
            '    .AppendLine(" SUM(siire_suu8) AS siire_suu8, ")
            '    .AppendLine(" SUM(siire_suu9) AS siire_suu9, ")
            '    .AppendLine(" SUM(siire_suu10) AS siire_suu10, ")
            '    .AppendLine(" SUM(siire_suu11) AS siire_suu11, ")
            '    .AppendLine(" SUM(siire_suu12) AS siire_suu12, ")
            '    .AppendLine(" SUM(siire_suu1) AS siire_suu1, ")
            '    .AppendLine(" SUM(siire_suu2) AS siire_suu2, ")
            '    .AppendLine(" SUM(siire_suu3) AS siire_suu3, ")
            '    .AppendLine(" ISNULL(SUM(uri_gaku4),0) - ISNULL(SUM(siire_gaku4),0) AS uri_arari4, ")
            '    .AppendLine(" ISNULL(SUM(uri_gaku5),0) - ISNULL(SUM(siire_gaku5),0) AS uri_arari5, ")
            '    .AppendLine(" ISNULL(SUM(uri_gaku6),0) - ISNULL(SUM(siire_gaku6),0) AS uri_arari6, ")
            '    .AppendLine(" ISNULL(SUM(uri_gaku7),0) - ISNULL(SUM(siire_gaku7),0) AS uri_arari7, ")
            '    .AppendLine(" ISNULL(SUM(uri_gaku8),0) - ISNULL(SUM(siire_gaku8),0) AS uri_arari8, ")
            '    .AppendLine(" ISNULL(SUM(uri_gaku9),0) - ISNULL(SUM(siire_gaku9),0) AS uri_arari9, ")
            '    .AppendLine(" ISNULL(SUM(uri_gaku10),0) - ISNULL(SUM(siire_gaku10),0) AS uri_arari10, ")
            '    .AppendLine(" ISNULL(SUM(uri_gaku11),0) - ISNULL(SUM(siire_gaku11),0) AS uri_arari11, ")
            '    .AppendLine(" ISNULL(SUM(uri_gaku12),0) - ISNULL(SUM(siire_gaku12),0) AS uri_arari12, ")
            '    .AppendLine(" ISNULL(SUM(uri_gaku1),0) - ISNULL(SUM(siire_gaku1),0) AS uri_arari1, ")
            '    .AppendLine(" ISNULL(SUM(uri_gaku2),0) - ISNULL(SUM(siire_gaku2),0) AS uri_arari2, ")
            '    .AppendLine(" ISNULL(SUM(uri_gaku3),0) - ISNULL(SUM(siire_gaku3),0) AS uri_arari3, ")
            '    .AppendLine(" 0 AS zennen_heikin_tanka, ")
            '    .AppendLine(" 0 AS zennen_siire_heikin_tanka ")
            '    .AppendLine(" FROM ( ")
            '    .AppendLine(" SELECT kameiten_mei1, ")
            '    .AppendLine(" kameiten_cd, ")
            '    .AppendLine(" syouhin_cd, ")
            '    .AppendLine(" syouhin_mei, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/4' THEN uri_gaku ELSE 0 END AS uri_gaku4, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/5' THEN uri_gaku ELSE 0 END AS uri_gaku5, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/6' THEN uri_gaku ELSE 0 END AS uri_gaku6, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/7' THEN uri_gaku ELSE 0 END AS uri_gaku7, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/8' THEN uri_gaku ELSE 0 END AS uri_gaku8, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/9' THEN uri_gaku ELSE 0 END AS uri_gaku9, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/10' THEN uri_gaku ELSE 0 END AS uri_gaku10, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/11' THEN uri_gaku ELSE 0 END AS uri_gaku11, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/12' THEN uri_gaku ELSE 0 END AS uri_gaku12, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/1' THEN uri_gaku ELSE 0 END AS uri_gaku1, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/2' THEN uri_gaku ELSE 0 END AS uri_gaku2, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/3' THEN uri_gaku ELSE 0 END AS uri_gaku3, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/4' THEN uri_suu ELSE 0 END AS uri_suu4, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/5' THEN uri_suu ELSE 0 END AS uri_suu5, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/6' THEN uri_suu ELSE 0 END AS uri_suu6, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/7' THEN uri_suu ELSE 0 END AS uri_suu7, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/8' THEN uri_suu ELSE 0 END AS uri_suu8, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/9' THEN uri_suu ELSE 0 END AS uri_suu9, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/10' THEN uri_suu ELSE 0 END AS uri_suu10, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/11' THEN uri_suu ELSE 0 END AS uri_suu11, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/12' THEN uri_suu ELSE 0 END AS uri_suu12, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/1' THEN uri_suu ELSE 0 END AS uri_suu1, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/2' THEN uri_suu ELSE 0 END AS uri_suu2, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/3' THEN uri_suu ELSE 0 END AS uri_suu3, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/4' THEN siire_gaku ELSE 0 END AS siire_gaku4, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/5' THEN siire_gaku ELSE 0 END AS siire_gaku5, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/6' THEN siire_gaku ELSE 0 END AS siire_gaku6, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/7' THEN siire_gaku ELSE 0 END AS siire_gaku7, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/8' THEN siire_gaku ELSE 0 END AS siire_gaku8, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/9' THEN siire_gaku ELSE 0 END AS siire_gaku9, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/10' THEN siire_gaku ELSE 0 END AS siire_gaku10, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/11' THEN siire_gaku ELSE 0 END AS siire_gaku11, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/12' THEN siire_gaku ELSE 0 END AS siire_gaku12, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/1' THEN siire_gaku ELSE 0 END AS siire_gaku1, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/2' THEN siire_gaku ELSE 0 END AS siire_gaku2, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/3' THEN siire_gaku ELSE 0 END AS siire_gaku3, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/4' THEN siire_suu ELSE 0 END AS siire_suu4, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/5' THEN siire_suu ELSE 0 END AS siire_suu5, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/6' THEN siire_suu ELSE 0 END AS siire_suu6, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/7' THEN siire_suu ELSE 0 END AS siire_suu7, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/8' THEN siire_suu ELSE 0 END AS siire_suu8, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/9' THEN siire_suu ELSE 0 END AS siire_suu9, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/10' THEN siire_suu ELSE 0 END AS siire_suu10, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/11' THEN siire_suu ELSE 0 END AS siire_suu11, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/12' THEN siire_suu ELSE 0 END AS siire_suu12, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/1' THEN siire_suu ELSE 0 END AS siire_suu1, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/2' THEN siire_suu ELSE 0 END AS siire_suu2, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/3' THEN siire_suu ELSE 0 END AS siire_suu3 ")
            '    .AppendLine(" FROM ( ")
            '    .AppendLine(" SELECT k.kameiten_mei1, ")
            '    .AppendLine(" k.kameiten_cd, ")
            '    .AppendLine(" '/'+CONVERT(VARCHAR,MONTH(denpyou_date)) AS denpyou_date, ")
            '    .AppendLine(" s.syouhin_syubetu1 AS syouhin_cd, ")
            '    .AppendLine(" km.meisyou AS syouhin_mei, ")
            '    .AppendLine(" SUM(urisiire.uri_gaku) AS uri_gaku, ")

            '    '------指摘No29の仕様変更--------修正(2013.03.05)-----------Begin
            '    '.AppendLine(" SUM(CASE s.souko_cd WHEN '140' THEN 0 ELSE uri_suu END) AS uri_suu, ")
            '    .AppendLine(" SUM(CASE WHEN (SUBSTRING(s.syouhin_syubetu1,1,3)='Ke1' AND s.syouhin_syubetu1<>'Ke1003' AND s.souko_cd<>'100') OR (s.souko_cd='140' AND t.kbn IS NOT NULL) THEN 0 ELSE uri_suu END) AS uri_suu, ")
            '    '------指摘No29の仕様変更--------修正(2013.03.05)-----------End

            '    .AppendLine(" SUM(urisiire.siire_gaku) AS siire_gaku, ")

            '    '------指摘No29の仕様変更--------修正(2013.03.05)-----------Begin
            '    '.AppendLine(" SUM(CASE s.souko_cd WHEN '140' THEN 0 ELSE siire_suu END) AS siire_suu ")
            '    .AppendLine(" SUM(CASE WHEN (SUBSTRING(s.syouhin_syubetu1,1,3)='Ke1' AND s.syouhin_syubetu1<>'Ke1003' AND s.souko_cd<>'100') OR (s.souko_cd='140' AND t.kbn IS NOT NULL) THEN 0 ELSE siire_suu END) AS siire_suu ")
            '    '------指摘No29の仕様変更--------修正(2013.03.05)-----------End

            '    .AppendLine(" FROM m_kameiten k WITH(READUNCOMMITTED) ")
            '    .AppendLine(" LEFT JOIN ( ")
            '    '紐付けテーブルタイプが1（邸別請求）は売上ﾃﾞｰﾀﾃｰﾌﾞﾙの加盟店ｺｰﾄﾞで集計
            '    .AppendLine("  SELECT kameiten_cd, ")
            '    .AppendLine("  u.syouhin_cd, ")
            '    .AppendLine("  denpyou_uri_date denpyou_date, ")
            '    .AppendLine("  uri_gaku, ")
            '    .AppendLine("  u.suu uri_suu, ")
            '    .AppendLine("  0 siire_gaku, ")
            '    .AppendLine("  0 siire_suu, ")

            '    '------指摘No29の仕様変更--------追加(2013.03.05)-----------Begin
            '    .AppendLine("  ISNULL(u.kbn,'')+ISNULL(u.bangou,'') AS bukken_bangou ")
            '    '------指摘No29の仕様変更--------追加(2013.03.05)-----------End

            '    .AppendLine("  FROM t_uriage_data u WITH(READUNCOMMITTED) ")
            '    .AppendLine("  WHERE u.denpyou_uri_date BETWEEN @beginYear AND @endYear ")
            '    .AppendLine("  AND u.himoduke_table_type=1 ")
            '    .AppendLine("  UNION ALL ")
            '    '紐付けテーブルタイプが2（店別請求）は売上ﾃﾞｰﾀﾃｰﾌﾞﾙの紐付けｺｰﾄﾞ先頭5桁（AFで始まるものは除く）で集計
            '    .AppendLine("  SELECT SUBSTRING(u.himoduke_cd,1,5), ")
            '    .AppendLine("  u.syouhin_cd, ")
            '    .AppendLine("  denpyou_uri_date denpyou_date, ")
            '    .AppendLine("  uri_gaku, ")
            '    .AppendLine("  u.suu uri_suu, ")
            '    .AppendLine("  0 siire_gaku, ")
            '    .AppendLine("  0 siire_suu, ")

            '    '------指摘No29の仕様変更--------追加(2013.03.05)-----------Begin
            '    .AppendLine("  '' AS bukken_bangou ")
            '    '------指摘No29の仕様変更--------追加(2013.03.05)-----------End

            '    .AppendLine("  FROM t_uriage_data u WITH(READUNCOMMITTED) ")
            '    .AppendLine("  WHERE u.denpyou_uri_date BETWEEN @beginYear AND @endYear ")
            '    .AppendLine("  AND u.himoduke_table_type=2  ")
            '    .AppendLine("  AND SUBSTRING(u.himoduke_cd,1,2)<>'AF' ")
            '    .AppendLine("  UNION ALL ")
            '    '紐付けテーブルタイプが3（店別初期請求）は売上ﾃﾞｰﾀﾃｰﾌﾞﾙの紐付けｺｰﾄﾞ先頭5桁で集計
            '    .AppendLine("  SELECT SUBSTRING(u.himoduke_cd,1,5), ")
            '    .AppendLine("  u.syouhin_cd, ")
            '    .AppendLine("  denpyou_uri_date denpyou_date, ")
            '    .AppendLine("  uri_gaku, ")
            '    .AppendLine("  u.suu uri_suu, ")
            '    .AppendLine("  0 siire_gaku, ")
            '    .AppendLine("  0 siire_suu, ")

            '    '------指摘No29の仕様変更--------追加(2013.03.05)-----------Begin
            '    .AppendLine("  '' AS bukken_bangou ")
            '    '------指摘No29の仕様変更--------追加(2013.03.05)-----------End

            '    .AppendLine("  FROM t_uriage_data u WITH(READUNCOMMITTED) ")
            '    .AppendLine("  INNER JOIN t_jiban j ")
            '    .AppendLine("  ON u.kbn=j.kbn ")
            '    .AppendLine("  AND u.bangou=j.hosyousyo_no ")
            '    .AppendLine("  WHERE u.denpyou_uri_date BETWEEN @beginYear AND @endYear ")
            '    .AppendLine("  AND u.himoduke_table_type=3 ")
            '    .AppendLine("  UNION ALL ")
            '    '紐付けテーブルタイプが9（汎用売上）は売上ﾃﾞｰﾀﾃｰﾌﾞﾙの加盟店ｺｰﾄﾞで集計
            '    .AppendLine("  SELECT j.kameiten_cd, ")
            '    .AppendLine("  u.syouhin_cd, ")
            '    .AppendLine("  denpyou_uri_date denpyou_date, ")
            '    .AppendLine("  uri_gaku, ")
            '    .AppendLine("  u.suu uri_suu, ")
            '    .AppendLine("  0 siire_gaku, ")
            '    .AppendLine("  0 siire_suu, ")

            '    '------指摘No29の仕様変更--------追加(2013.03.05)-----------Begin
            '    .AppendLine("  ISNULL(u.kbn,'')+ISNULL(u.bangou,'') AS bukken_bangou ")
            '    '------指摘No29の仕様変更--------追加(2013.03.05)-----------End

            '    .AppendLine("  FROM t_uriage_data u WITH(READUNCOMMITTED) ")
            '    .AppendLine("  INNER JOIN t_jiban j ")
            '    .AppendLine("  ON u.kbn=j.kbn ")
            '    .AppendLine("  AND u.bangou=j.hosyousyo_no ")
            '    .AppendLine("  WHERE u.denpyou_uri_date BETWEEN @beginYear AND @endYear ")
            '    .AppendLine("  AND u.himoduke_table_type=9 ")
            '    .AppendLine("  UNION ALL ")
            '    '紐付けテーブルタイプが1（邸別請求）は地盤テーブル加盟店ｺｰﾄﾞで集計
            '    .AppendLine("  SELECT j.kameiten_cd, ")
            '    .AppendLine("  s.syouhin_cd, ")
            '    .AppendLine("  s.denpyou_siire_date denpyou_date, ")
            '    .AppendLine("  0 uri_gaku, ")
            '    .AppendLine("  0 uri_suu, ")
            '    .AppendLine("  siire_gaku, ")
            '    .AppendLine("  s.suu siire_suu, ")

            '    '------指摘No29の仕様変更--------追加(2013.03.05)-----------Begin
            '    .AppendLine("  ISNULL(s.kbn,'')+ISNULL(s.bangou,'') AS bukken_bangou ")
            '    '------指摘No29の仕様変更--------追加(2013.03.05)-----------End

            '    .AppendLine("  FROM t_siire_data s WITH(READUNCOMMITTED) ")
            '    .AppendLine("  INNER JOIN t_jiban j WITH(READUNCOMMITTED) ")
            '    .AppendLine("  ON s.kbn=j.kbn ")
            '    .AppendLine("  AND s.bangou=j.hosyousyo_no ")
            '    .AppendLine("  WHERE s.denpyou_siire_date BETWEEN @beginYear AND @endYear ")
            '    .AppendLine("  AND s.himoduke_table_type=1 ")
            '    .AppendLine("  UNION ALL ")
            '    '紐付けテーブルタイプが9（汎用仕入）は汎用仕入テーブルの加盟店ｺｰﾄﾞか、空白だったら地盤テーブルの加盟店ｺｰﾄﾞで集計（取得できないものがでるが仕方なし）
            '    .AppendLine("  SELECT ISNULL(h.kameiten_cd,j.kameiten_cd), ")
            '    .AppendLine("  s.syouhin_cd, ")
            '    .AppendLine("  s.denpyou_siire_date denpyou_date, ")
            '    .AppendLine("  0 uri_gaku, ")
            '    .AppendLine("  0 uri_suu, ")
            '    .AppendLine("  siire_gaku, ")
            '    .AppendLine("  s.suu siire_suu, ")

            '    '------指摘No29の仕様変更--------追加(2013.03.05)-----------Begin
            '    .AppendLine("  ISNULL(s.kbn,'')+ISNULL(s.bangou,'') AS bukken_bangou ")
            '    '------指摘No29の仕様変更--------追加(2013.03.05)-----------End

            '    .AppendLine("  FROM t_siire_data s WITH(READUNCOMMITTED) ")
            '    .AppendLine("  INNER JOIN t_hannyou_siire h WITH(READUNCOMMITTED) ")
            '    .AppendLine("  ON s.himoduke_cd=h.han_siire_unique_no ")
            '    .AppendLine("  LEFT JOIN t_jiban j ")
            '    .AppendLine("  ON s.kbn=j.kbn ")
            '    .AppendLine("  and s.bangou=j.hosyousyo_no ")
            '    .AppendLine("  WHERE s.denpyou_siire_date BETWEEN @beginYear AND @endYear ")
            '    .AppendLine("  AND s.himoduke_table_type=9 ")
            '    .AppendLine(" ) urisiire ")
            '    .AppendLine(" ON k.kameiten_cd=urisiire.kameiten_cd ")
            '    .AppendLine(" INNER JOIN m_syouhin s WITH(READUNCOMMITTED) ")
            '    .AppendLine(" ON urisiire.syouhin_cd=s.syouhin_cd ")
            '    .AppendLine(" INNER JOIN m_kakutyou_meisyou km ")
            '    .AppendLine(" ON s.syouhin_syubetu1=km.code ")
            '    .AppendLine(" AND km.meisyou_syubetu='51' ")

            '    '------指摘No29の仕様変更--------追加(2013.03.05)-----------Begin
            '    .AppendLine(" LEFT JOIN t_teibetu_seikyuu t WITH(READUNCOMMITTED) ")
            '    .AppendLine(" ON urisiire.bukken_bangou=t.kbn+t.hosyousyo_no ")
            '    .AppendLine(" AND s.souko_cd='140' ")
            '    .AppendLine(" AND t.bunrui_cd='130' ")
            '    .AppendLine(" AND t.uri_gaku<>0 ")
            '    .AppendLine(" AND t.denpyou_uri_date IS NOT NULL ")
            '    '------指摘No29の仕様変更--------追加(2013.03.05)-----------End

            '    '特販がそれ以外の区分の系列の設定になったらこれではNG
            '    .AppendLine(" WHERE ISNULL(s.syouhin_syubetu1,'')<>'' ")

            '    '2013/10/23 李宇修正　↓
            '    .AppendLine(" AND k.kbn IN (" & strKubun & ") ")
            '    '2013/10/23 李宇修正　↑

            '    '加盟店・年月・商品種別1で集計
            '    .AppendLine(" GROUP BY k.kameiten_mei1, ")
            '    .AppendLine(" k.kameiten_cd, ")
            '    .AppendLine(" '/'+CONVERT(VARCHAR,MONTH(denpyou_date)), ")
            '    .AppendLine(" s.syouhin_syubetu1, ")
            '    .AppendLine(" km.meisyou ")
            '    .AppendLine(" ) AS SUB_MK ")
            '    .AppendLine(" ) AS MK ")
            '    .AppendLine(" GROUP BY kameiten_mei1, ")
            '    .AppendLine(" kameiten_cd, ")
            '    .AppendLine(" syouhin_cd, ")
            '    .AppendLine(" syouhin_mei ")
            '    .AppendLine(" ORDER BY  ")
            '    .AppendLine(" kameiten_cd, ")
            '    .AppendLine(" syouhin_cd ")
            'End With

            Dim strSql As String = String.Empty
            strSql = Definition.GetSqlString(Definition.SqlStringKbn.S0004_01)
            strSql = strSql.Replace("@strKubun", strKubun)
            sqlBuffer.AppendLine(strSql)

            'バラメタ
            paramList.Add(MakeParam("@beginYear", SqlDbType.VarChar, 10, strBeginMonth))         '開始年月
            paramList.Add(MakeParam("@endYear", SqlDbType.VarChar, 10, strEndMonth))             '終了年月

            'Ｅａｒｔｈのデータ
            Return objConnection.ExecuteDataTable(CommandType.Text, sqlBuffer.ToString(), paramList.ToArray())
        Catch ex As Exception
            If IsNothing(ex.Data.Item("ERROR_LOG")) Then
                ex.Data.Add("ERROR_LOG", "Ｅａｒｔｈのデータの取得処理が異常終了しました。")
            End If

            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 前年Ｅａｒｔｈのデータを取得する
    ''' </summary>
    ''' <param name="objConnection">DB接続</param>
    ''' <param name="strBeginMonth">開始月</param>
    ''' <param name="strEndMonth">結束月</param>
    ''' <returns>前年ＴＲＡＩＮ受注データ</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/01/15 大連/王新 新規作成 P-44979</para>
    ''' </history>
    Private Function SelZennenEarthData(ByVal objConnection As SqlExecutor, _
                                        ByVal strBeginMonth As String, _
                                        ByVal strEndMonth As String) As Data.DataTable
        'SQLコメント
        Dim sqlBuffer As New System.Text.StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        '2013/10/23 李宇追加　↓
        '区分
        Dim strKubun As String
        strKubun = Definition.GetKubunName4
        '2013/10/23 李宇追加　↑

        Try
            'SQL文	
            'With sqlBuffer
            '    .AppendLine(" SELECT ")
            '    .AppendLine(" k.kameiten_cd, ")                                                 '加盟店ｺｰﾄﾞ
            '    .AppendLine(" s.syouhin_syubetu1 AS syouhin_cd, ")                              '計画管理_商品コード
            '    '------指摘No29の仕様変更--------修正(2013.03.05)-----------Begin
            '    '.AppendLine(" CASE WHEN ISNULL(SUM(CASE s.souko_cd WHEN '140' THEN 0 ELSE uri_suu END),0) = 0 THEN 0 ELSE ")
            '    '.AppendLine(" ISNULL(SUM(uri_gaku),0) / SUM(CASE s.souko_cd WHEN '140' THEN 0 ELSE uri_suu END) END AS heikin_tanka, ")    '前年_平均単価
            '    '.AppendLine(" CASE WHEN ISNULL(SUM(CASE s.souko_cd WHEN '140' THEN 0 ELSE siire_suu END),0) = 0 THEN 0 ELSE ")
            '    '.AppendLine(" ISNULL(SUM(siire_gaku),0) / SUM(CASE s.souko_cd WHEN '140' THEN 0 ELSE siire_suu END) END AS siire_heikin_tanka ")   '前年_仕入平均単価
            '    .AppendLine(" CASE WHEN ISNULL(SUM(CASE WHEN (SUBSTRING(s.syouhin_syubetu1,1,3)='Ke1' AND s.syouhin_syubetu1<>'Ke1003' AND s.souko_cd<>'100') OR (s.souko_cd='140' AND t.kbn IS NOT NULL) THEN 0 ELSE uri_suu END),0) = 0 THEN 0 ELSE ")
            '    .AppendLine(" ISNULL(SUM(urisiire.uri_gaku),0) / SUM(CASE WHEN (SUBSTRING(s.syouhin_syubetu1,1,3)='Ke1' AND s.syouhin_syubetu1<>'Ke1003' AND s.souko_cd<>'100') OR (s.souko_cd='140' AND t.kbn IS NOT NULL) THEN 0 ELSE uri_suu END) END AS heikin_tanka, ")    '前年_平均単価
            '    .AppendLine(" CASE WHEN ISNULL(SUM(CASE WHEN (SUBSTRING(s.syouhin_syubetu1,1,3)='Ke1' AND s.syouhin_syubetu1<>'Ke1003' AND s.souko_cd<>'100') OR (s.souko_cd='140' AND t.kbn IS NOT NULL) THEN 0 ELSE siire_suu END),0) = 0 THEN 0 ELSE ")
            '    .AppendLine(" ISNULL(SUM(urisiire.siire_gaku),0) / SUM(CASE WHEN (SUBSTRING(s.syouhin_syubetu1,1,3)='Ke1' AND s.syouhin_syubetu1<>'Ke1003' AND s.souko_cd<>'100') OR (s.souko_cd='140' AND t.kbn IS NOT NULL) THEN 0 ELSE siire_suu END) END AS siire_heikin_tanka ")   '前年_仕入平均単価
            '    '------指摘No29の仕様変更--------修正(2013.03.05)-----------End
            '    .AppendLine(" FROM m_kameiten k WITH(READUNCOMMITTED) ")
            '    .AppendLine(" LEFT JOIN ( ")
            '    '紐付けテーブルタイプが1（邸別請求）は売上ﾃﾞｰﾀﾃｰﾌﾞﾙの加盟店ｺｰﾄﾞで集計
            '    .AppendLine("  SELECT kameiten_cd, ")
            '    .AppendLine("  u.syouhin_cd, ")
            '    .AppendLine("  uri_gaku, ")
            '    .AppendLine("  u.suu uri_suu, ")
            '    .AppendLine("  0 siire_gaku, ")
            '    .AppendLine("  0 siire_suu, ")

            '    '------指摘No29の仕様変更--------追加(2013.03.05)-----------Begin
            '    .AppendLine("  ISNULL(u.kbn,'')+ISNULL(u.bangou,'') AS bukken_bangou  ")
            '    '------指摘No29の仕様変更--------追加(2013.03.05)-----------End

            '    .AppendLine("  FROM t_uriage_data u WITH(READUNCOMMITTED) ")
            '    .AppendLine("  WHERE u.denpyou_uri_date BETWEEN DATEADD(YYYY,-1,@beginYear) AND DATEADD(YYYY,-1,@endYear) ")
            '    .AppendLine("  AND u.himoduke_table_type=1 ")
            '    .AppendLine("  UNION ALL ")
            '    '紐付けテーブルタイプが2（店別請求）は売上ﾃﾞｰﾀﾃｰﾌﾞﾙの紐付けｺｰﾄﾞ先頭5桁（AFで始まるものは除く）で集計
            '    .AppendLine("  SELECT SUBSTRING(u.himoduke_cd,1,5), ")
            '    .AppendLine("  u.syouhin_cd, ")
            '    .AppendLine("  uri_gaku, ")
            '    .AppendLine("  u.suu uri_suu, ")
            '    .AppendLine("  0 siire_gaku, ")
            '    .AppendLine("  0 siire_suu, ")

            '    '------指摘No29の仕様変更--------追加(2013.03.05)-----------Begin
            '    .AppendLine("  '' AS bukken_bangou  ")
            '    '------指摘No29の仕様変更--------追加(2013.03.05)-----------End

            '    .AppendLine("  FROM t_uriage_data u WITH(READUNCOMMITTED) ")
            '    .AppendLine("  WHERE u.denpyou_uri_date BETWEEN DATEADD(YYYY,-1,@beginYear) AND DATEADD(YYYY,-1,@endYear) ")
            '    .AppendLine("  AND u.himoduke_table_type=2  ")
            '    .AppendLine("  AND SUBSTRING(u.himoduke_cd,1,2)<>'AF' ")
            '    .AppendLine("  UNION ALL ")
            '    '紐付けテーブルタイプが3（店別初期請求）は売上ﾃﾞｰﾀﾃｰﾌﾞﾙの紐付けｺｰﾄﾞ先頭5桁で集計
            '    .AppendLine("  SELECT SUBSTRING(u.himoduke_cd,1,5), ")
            '    .AppendLine("  u.syouhin_cd, ")
            '    .AppendLine("  uri_gaku, ")
            '    .AppendLine("  u.suu uri_suu, ")
            '    .AppendLine("  0 siire_gaku, ")
            '    .AppendLine("  0 siire_suu, ")

            '    '------指摘No29の仕様変更--------追加(2013.03.05)-----------Begin
            '    .AppendLine("  '' AS bukken_bangou  ")
            '    '------指摘No29の仕様変更--------追加(2013.03.05)-----------End

            '    .AppendLine("  FROM t_uriage_data u WITH(READUNCOMMITTED) ")
            '    .AppendLine("  INNER JOIN t_jiban j  ")
            '    .AppendLine("  ON u.kbn=j.kbn  ")
            '    .AppendLine("  AND u.bangou=j.hosyousyo_no ")
            '    .AppendLine("  WHERE u.denpyou_uri_date BETWEEN DATEADD(YYYY,-1,@beginYear) AND DATEADD(YYYY,-1,@endYear) ")
            '    .AppendLine("  AND u.himoduke_table_type=3 ")
            '    .AppendLine("  UNION ALL ")
            '    '紐付けテーブルタイプが9（汎用売上）は売上ﾃﾞｰﾀﾃｰﾌﾞﾙの加盟店ｺｰﾄﾞで集計
            '    .AppendLine("  SELECT j.kameiten_cd, ")
            '    .AppendLine("  u.syouhin_cd, ")
            '    .AppendLine("  uri_gaku, ")
            '    .AppendLine("  u.suu uri_suu, ")
            '    .AppendLine("  0 siire_gaku, ")
            '    .AppendLine("  0 siire_suu, ")

            '    '------指摘No29の仕様変更--------追加(2013.03.05)-----------Begin
            '    .AppendLine("  ISNULL(u.kbn,'')+ISNULL(u.bangou,'') AS bukken_bangou  ")
            '    '------指摘No29の仕様変更--------追加(2013.03.05)-----------End

            '    .AppendLine("  FROM t_uriage_data u WITH(READUNCOMMITTED) ")
            '    .AppendLine("  INNER JOIN t_jiban j  ")
            '    .AppendLine("  ON u.kbn=j.kbn  ")
            '    .AppendLine("  AND u.bangou=j.hosyousyo_no ")
            '    .AppendLine("  WHERE u.denpyou_uri_date BETWEEN DATEADD(YYYY,-1,@beginYear) AND DATEADD(YYYY,-1,@endYear) ")
            '    .AppendLine("  AND u.himoduke_table_type=9 ")
            '    .AppendLine("  UNION ALL  ")
            '    '紐付けテーブルタイプが1（邸別請求）は地盤テーブル加盟店ｺｰﾄﾞで集計
            '    .AppendLine("  SELECT j.kameiten_cd, ")
            '    .AppendLine("  s.syouhin_cd, ")
            '    .AppendLine("  0 uri_gaku, ")
            '    .AppendLine("  0 uri_suu, ")
            '    .AppendLine("  siire_gaku, ")
            '    .AppendLine("  s.suu siire_suu, ")

            '    '------指摘No29の仕様変更--------追加(2013.03.05)-----------Begin
            '    .AppendLine("  ISNULL(s.kbn,'')+ISNULL(s.bangou,'') AS bukken_bangou  ")
            '    '------指摘No29の仕様変更--------追加(2013.03.05)-----------End

            '    .AppendLine("  FROM t_siire_data s WITH(READUNCOMMITTED) ")
            '    .AppendLine("  INNER JOIN t_jiban j WITH(READUNCOMMITTED)  ")
            '    .AppendLine("  ON s.kbn=j.kbn  ")
            '    .AppendLine("  AND s.bangou=j.hosyousyo_no ")
            '    .AppendLine("  WHERE s.denpyou_siire_date BETWEEN DATEADD(YYYY,-1,@beginYear) AND DATEADD(YYYY,-1,@endYear) ")
            '    .AppendLine("  AND s.himoduke_table_type=1 ")
            '    .AppendLine("  UNION ALL  ")
            '    '紐付けテーブルタイプが9（汎用仕入）は汎用仕入テーブルの加盟店ｺｰﾄﾞか、空白だったら地盤テーブルの加盟店ｺｰﾄﾞで集計（取得できないものがでるが仕方なし）
            '    .AppendLine("  SELECT ISNULL(h.kameiten_cd,j.kameiten_cd), ")
            '    .AppendLine("  s.syouhin_cd, ")
            '    .AppendLine("  0 uri_gaku, ")
            '    .AppendLine("  0 uri_suu, ")
            '    .AppendLine("  siire_gaku, ")
            '    .AppendLine("  s.suu siire_suu, ")

            '    '------指摘No29の仕様変更--------追加(2013.03.05)-----------Begin
            '    .AppendLine("  ISNULL(s.kbn,'')+ISNULL(s.bangou,'') AS bukken_bangou  ")
            '    '------指摘No29の仕様変更--------追加(2013.03.05)-----------End

            '    .AppendLine("  FROM t_siire_data s WITH(READUNCOMMITTED) ")
            '    .AppendLine("  INNER JOIN t_hannyou_siire h WITH(READUNCOMMITTED)  ")
            '    .AppendLine("  ON s.himoduke_cd=h.han_siire_unique_no ")
            '    .AppendLine("  LEFT JOIN t_jiban j  ")
            '    .AppendLine("  ON s.kbn=j.kbn  ")
            '    .AppendLine("  and s.bangou=j.hosyousyo_no ")
            '    .AppendLine("  WHERE s.denpyou_siire_date BETWEEN DATEADD(YYYY,-1,@beginYear) AND DATEADD(YYYY,-1,@endYear) ")
            '    .AppendLine("  AND s.himoduke_table_type=9 ")
            '    .AppendLine(" ) urisiire   ")
            '    .AppendLine(" ON k.kameiten_cd=urisiire.kameiten_cd ")
            '    .AppendLine(" INNER JOIN m_syouhin s WITH(READUNCOMMITTED)  ")
            '    .AppendLine(" ON urisiire.syouhin_cd=s.syouhin_cd ")
            '    .AppendLine(" INNER JOIN m_kakutyou_meisyou km  ")
            '    .AppendLine(" ON s.syouhin_syubetu1=km.code  ")
            '    .AppendLine(" AND km.meisyou_syubetu='51' ")

            '    '------指摘No29の仕様変更--------追加(2013.03.05)-----------Begin
            '    .AppendLine(" LEFT JOIN t_teibetu_seikyuu t WITH(READUNCOMMITTED) ")
            '    .AppendLine(" ON urisiire.bukken_bangou=t.kbn+t.hosyousyo_no ")
            '    .AppendLine(" AND s.souko_cd='140' ")
            '    .AppendLine(" AND t.bunrui_cd='130' ")
            '    .AppendLine(" AND t.uri_gaku<>0 ")
            '    .AppendLine(" AND t.denpyou_uri_date IS NOT NULL ")
            '    '------指摘No29の仕様変更--------追加(2013.03.05)-----------End

            '    '特販がそれ以外の区分の系列の設定になったらこれではNG
            '    .AppendLine(" WHERE ISNULL(s.syouhin_syubetu1,'')<>''  ")
            '    '2013/10/23 李宇修正　↓
            '    .AppendLine(" AND k.kbn IN (" & strKubun & ")  ")
            '    '2013/10/23 李宇修正　↑
            '    '加盟店・年月・商品種別1で集計
            '    .AppendLine(" GROUP BY ")
            '    .AppendLine(" k.kameiten_cd, ")
            '    .AppendLine(" s.syouhin_syubetu1 ")
            '    .AppendLine(" ORDER BY ")
            '    .AppendLine(" k.kameiten_cd, ")
            '    .AppendLine(" s.syouhin_syubetu1 ")
            'End With

            Dim strSql As String = String.Empty
            strSql = Definition.GetSqlString(Definition.SqlStringKbn.S0004_02)
            strSql = strSql.Replace("@strKubun", strKubun)
            sqlBuffer.AppendLine(strSql)

            'バラメタ
            paramList.Add(MakeParam("@beginYear", SqlDbType.VarChar, 10, strBeginMonth))         '開始年月
            paramList.Add(MakeParam("@endYear", SqlDbType.VarChar, 10, strEndMonth))             '終了年月

            'Ｅａｒｔｈのデータ
            Return objConnection.ExecuteDataTable(CommandType.Text, sqlBuffer.ToString(), paramList.ToArray())
        Catch ex As Exception
            If IsNothing(ex.Data.Item("ERROR_LOG")) Then
                ex.Data.Add("ERROR_LOG", "Ｅａｒｔｈの前年データの取得処理が異常終了しました。")
            End If

            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' JHSのデータを削除する
    ''' </summary>
    ''' <param name="objConnection">DB接続</param>
    ''' <param name="strYear">対象年度</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/01/15 大連/王新 新規作成 P-44979</para>
    ''' </history>
    Private Sub DelJHSData(ByVal objConnection As SqlExecutor, _
                           ByVal strYear As String)
        'SQLコメント
        Dim sqlBuffer As New System.Text.StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        Try
            'SQL文	
            'With sqlBuffer
            '    .AppendLine(" DELETE FROM t_jisseki_kanri ")
            '    .AppendLine(" WHERE keikaku_nendo = @keikaku_nendo ")
            'End With

            Dim strSql As String = String.Empty
            strSql = Definition.GetSqlString(Definition.SqlStringKbn.S0004_03)
            sqlBuffer.AppendLine(strSql)

            'バラメタ
            paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, strYear))      '計画_年度

            objConnection.ExecuteNonQuery(CommandType.Text, sqlBuffer.ToString(), paramList.ToArray())
        Catch ex As Exception
            If IsNothing(ex.Data.Item("ERROR_LOG")) Then
                ex.Data.Add("ERROR_LOG", "実績管理テーブルのデータの削除処理が異常終了しました。")
            End If

            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' JHSのデータを登録する
    ''' </summary>
    ''' <param name="objConnection">DB接続</param>
    ''' <param name="strYear">計画年度</param>
    ''' <param name="drData">登録データ</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/01/15 大連/王新 新規作成 P-44979</para>
    ''' </history>
    Private Function InsJHSData(ByVal objConnection As SqlExecutor, _
                                ByVal strYear As String, _
                                ByVal drData As DataRow) As Integer

        'SQLコメント
        Dim sqlBuffer As New System.Text.StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        Try
            'SQL文	
            'With sqlBuffer
            '    .AppendLine(" INSERT INTO t_jisseki_kanri ( ")
            '    .AppendLine(" [keikaku_nendo], ")
            '    .AppendLine(" [kameiten_cd], ")
            '    .AppendLine(" [keikaku_kanri_syouhin_cd], ")
            '    .AppendLine(" [kameiten_mei], ")
            '    .AppendLine(" [bunbetu_cd], ")
            '    .AppendLine(" [zennen_heikin_tanka], ")
            '    .AppendLine(" [zennen_siire_heikin_tanka], ")
            '    .AppendLine(" [4gatu_jisseki_kensuu], ")
            '    .AppendLine(" [4gatu_jisseki_kingaku], ")
            '    .AppendLine(" [4gatu_jisseki_arari], ")
            '    .AppendLine(" [5gatu_jisseki_kensuu], ")
            '    .AppendLine(" [5gatu_jisseki_kingaku], ")
            '    .AppendLine(" [5gatu_jisseki_arari], ")
            '    .AppendLine(" [6gatu_jisseki_kensuu], ")
            '    .AppendLine(" [6gatu_jisseki_kingaku], ")
            '    .AppendLine(" [6gatu_jisseki_arari], ")
            '    .AppendLine(" [7gatu_jisseki_kensuu], ")
            '    .AppendLine(" [7gatu_jisseki_kingaku], ")
            '    .AppendLine(" [7gatu_jisseki_arari], ")
            '    .AppendLine(" [8gatu_jisseki_kensuu], ")
            '    .AppendLine(" [8gatu_jisseki_kingaku], ")
            '    .AppendLine(" [8gatu_jisseki_arari], ")
            '    .AppendLine(" [9gatu_jisseki_kensuu], ")
            '    .AppendLine(" [9gatu_jisseki_kingaku], ")
            '    .AppendLine(" [9gatu_jisseki_arari], ")
            '    .AppendLine(" [10gatu_jisseki_kensuu], ")
            '    .AppendLine(" [10gatu_jisseki_kingaku], ")
            '    .AppendLine(" [10gatu_jisseki_arari], ")
            '    .AppendLine(" [11gatu_jisseki_kensuu], ")
            '    .AppendLine(" [11gatu_jisseki_kingaku], ")
            '    .AppendLine(" [11gatu_jisseki_arari], ")
            '    .AppendLine(" [12gatu_jisseki_kensuu], ")
            '    .AppendLine(" [12gatu_jisseki_kingaku], ")
            '    .AppendLine(" [12gatu_jisseki_arari], ")
            '    .AppendLine(" [1gatu_jisseki_kensuu], ")
            '    .AppendLine(" [1gatu_jisseki_kingaku], ")
            '    .AppendLine(" [1gatu_jisseki_arari], ")
            '    .AppendLine(" [2gatu_jisseki_kensuu], ")
            '    .AppendLine(" [2gatu_jisseki_kingaku], ")
            '    .AppendLine(" [2gatu_jisseki_arari], ")
            '    .AppendLine(" [3gatu_jisseki_kensuu], ")
            '    .AppendLine(" [3gatu_jisseki_kingaku], ")
            '    .AppendLine(" [3gatu_jisseki_arari], ")
            '    .AppendLine(" [add_login_user_id], ")
            '    .AppendLine(" [add_datetime], ")
            '    .AppendLine(" [upd_login_user_id], ")
            '    .AppendLine(" [upd_datetime] ")
            '    .AppendLine(" ) ")
            '    .AppendLine(" SELECT ")
            '    .AppendLine(" @keikaku_nendo, ")
            '    .AppendLine(" @kameiten_cd, ")
            '    .AppendLine(" @keikaku_kanri_syouhin_cd, ")
            '    .AppendLine(" @kameiten_mei, ")
            '    .AppendLine(" ISNULL((SELECT bunbetu_cd ")
            '    .AppendLine(" FROM m_keikaku_kanri_syouhin ")
            '    .AppendLine(" WHERE keikaku_nendo = @keikaku_nendo ")
            '    .AppendLine(" AND keikaku_kanri_syouhin_cd = @keikaku_kanri_syouhin_cd),''), ")
            '    .AppendLine(" @zennen_heikin_tanka, ")
            '    .AppendLine(" @zennen_siire_heikin_tanka, ")
            '    .AppendLine(" @gatu_jisseki_kensuu4, ")
            '    .AppendLine(" @gatu_jisseki_kingaku4, ")
            '    .AppendLine(" @gatu_jisseki_arari4, ")
            '    .AppendLine(" @gatu_jisseki_kensuu5, ")
            '    .AppendLine(" @gatu_jisseki_kingaku5, ")
            '    .AppendLine(" @gatu_jisseki_arari5, ")
            '    .AppendLine(" @gatu_jisseki_kensuu6, ")
            '    .AppendLine(" @gatu_jisseki_kingaku6, ")
            '    .AppendLine(" @gatu_jisseki_arari6, ")
            '    .AppendLine(" @gatu_jisseki_kensuu7, ")
            '    .AppendLine(" @gatu_jisseki_kingaku7, ")
            '    .AppendLine(" @gatu_jisseki_arari7, ")
            '    .AppendLine(" @gatu_jisseki_kensuu8, ")
            '    .AppendLine(" @gatu_jisseki_kingaku8, ")
            '    .AppendLine(" @gatu_jisseki_arari8, ")
            '    .AppendLine(" @gatu_jisseki_kensuu9, ")
            '    .AppendLine(" @gatu_jisseki_kingaku9, ")
            '    .AppendLine(" @gatu_jisseki_arari9, ")
            '    .AppendLine(" @gatu_jisseki_kensuu10, ")
            '    .AppendLine(" @gatu_jisseki_kingaku10, ")
            '    .AppendLine(" @gatu_jisseki_arari10, ")
            '    .AppendLine(" @gatu_jisseki_kensuu11, ")
            '    .AppendLine(" @gatu_jisseki_kingaku11, ")
            '    .AppendLine(" @gatu_jisseki_arari11, ")
            '    .AppendLine(" @gatu_jisseki_kensuu12, ")
            '    .AppendLine(" @gatu_jisseki_kingaku12, ")
            '    .AppendLine(" @gatu_jisseki_arari12, ")
            '    .AppendLine(" @gatu_jisseki_kensuu1, ")
            '    .AppendLine(" @gatu_jisseki_kingaku1, ")
            '    .AppendLine(" @gatu_jisseki_arari1, ")
            '    .AppendLine(" @gatu_jisseki_kensuu2, ")
            '    .AppendLine(" @gatu_jisseki_kingaku2, ")
            '    .AppendLine(" @gatu_jisseki_arari2, ")
            '    .AppendLine(" @gatu_jisseki_kensuu3, ")
            '    .AppendLine(" @gatu_jisseki_kingaku3, ")
            '    .AppendLine(" @gatu_jisseki_arari3, ")
            '    .AppendLine(" @add_login_user_id, ")
            '    .AppendLine(" GETDATE(), ")
            '    .AppendLine(" NULL, ")
            '    .AppendLine(" NULL ")
            'End With

            Dim strSql As String = String.Empty
            strSql = Definition.GetSqlString(Definition.SqlStringKbn.S0004_04)
            sqlBuffer.AppendLine(strSql)

            'バラメタ
            paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, strYear))                              '計画_年度
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 8, drData("kameiten_cd")))               '加盟店ｺｰﾄﾞ
            paramList.Add(MakeParam("@keikaku_kanri_syouhin_cd", SqlDbType.VarChar, 8, drData("syouhin_cd")))   '計画管理_商品コード
            paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 40, drData("kameiten_mei1")))           '加盟店名
            paramList.Add(MakeParam("@zennen_heikin_tanka", SqlDbType.BigInt, 12, drData("zennen_heikin_tanka")))   '前年_平均単価
            paramList.Add(MakeParam("@zennen_siire_heikin_tanka", SqlDbType.BigInt, 12, drData("zennen_siire_heikin_tanka"))) '前年_仕入平均単価
            paramList.Add(MakeParam("@gatu_jisseki_kensuu4", SqlDbType.BigInt, 12, drData("uri_suu4")))         '4月_実績件数
            paramList.Add(MakeParam("@gatu_jisseki_kingaku4", SqlDbType.BigInt, 12, drData("uri_gaku4")))       '4月_実績金額
            paramList.Add(MakeParam("@gatu_jisseki_arari4", SqlDbType.BigInt, 12, drData("uri_arari4")))        '4月_実績粗利
            paramList.Add(MakeParam("@gatu_jisseki_kensuu5", SqlDbType.BigInt, 12, drData("uri_suu5")))         '5月_実績件数
            paramList.Add(MakeParam("@gatu_jisseki_kingaku5", SqlDbType.BigInt, 12, drData("uri_gaku5")))       '5月_実績金額
            paramList.Add(MakeParam("@gatu_jisseki_arari5", SqlDbType.BigInt, 12, drData("uri_arari5")))        '5月_実績粗利
            paramList.Add(MakeParam("@gatu_jisseki_kensuu6", SqlDbType.BigInt, 12, drData("uri_suu6")))         '6月_実績件数
            paramList.Add(MakeParam("@gatu_jisseki_kingaku6", SqlDbType.BigInt, 12, drData("uri_gaku6")))       '6月_実績金額
            paramList.Add(MakeParam("@gatu_jisseki_arari6", SqlDbType.BigInt, 12, drData("uri_arari6")))        '6月_実績粗利
            paramList.Add(MakeParam("@gatu_jisseki_kensuu7", SqlDbType.BigInt, 12, drData("uri_suu7")))         '7月_実績件数
            paramList.Add(MakeParam("@gatu_jisseki_kingaku7", SqlDbType.BigInt, 12, drData("uri_gaku7")))       '7月_実績金額
            paramList.Add(MakeParam("@gatu_jisseki_arari7", SqlDbType.BigInt, 12, drData("uri_arari7")))        '7月_実績粗利
            paramList.Add(MakeParam("@gatu_jisseki_kensuu8", SqlDbType.BigInt, 12, drData("uri_suu8")))         '8月_実績件数
            paramList.Add(MakeParam("@gatu_jisseki_kingaku8", SqlDbType.BigInt, 12, drData("uri_gaku8")))       '8月_実績金額
            paramList.Add(MakeParam("@gatu_jisseki_arari8", SqlDbType.BigInt, 12, drData("uri_arari8")))        '8月_実績粗利
            paramList.Add(MakeParam("@gatu_jisseki_kensuu9", SqlDbType.BigInt, 12, drData("uri_suu9")))         '9月_実績件数
            paramList.Add(MakeParam("@gatu_jisseki_kingaku9", SqlDbType.BigInt, 12, drData("uri_gaku9")))       '9月_実績金額
            paramList.Add(MakeParam("@gatu_jisseki_arari9", SqlDbType.BigInt, 12, drData("uri_arari9")))        '9月_実績粗利
            paramList.Add(MakeParam("@gatu_jisseki_kensuu10", SqlDbType.BigInt, 12, drData("uri_suu10")))       '10月_実績件数
            paramList.Add(MakeParam("@gatu_jisseki_kingaku10", SqlDbType.BigInt, 12, drData("uri_gaku10")))     '10月_実績金額
            paramList.Add(MakeParam("@gatu_jisseki_arari10", SqlDbType.BigInt, 12, drData("uri_arari10")))      '10月_実績粗利
            paramList.Add(MakeParam("@gatu_jisseki_kensuu11", SqlDbType.BigInt, 12, drData("uri_suu11")))       '11月_実績件数
            paramList.Add(MakeParam("@gatu_jisseki_kingaku11", SqlDbType.BigInt, 12, drData("uri_gaku11")))     '11月_実績金額
            paramList.Add(MakeParam("@gatu_jisseki_arari11", SqlDbType.BigInt, 12, drData("uri_arari11")))      '11月_実績粗利
            paramList.Add(MakeParam("@gatu_jisseki_kensuu12", SqlDbType.BigInt, 12, drData("uri_suu12")))       '12月_実績件数
            paramList.Add(MakeParam("@gatu_jisseki_kingaku12", SqlDbType.BigInt, 12, drData("uri_gaku12")))     '12月_実績金額
            paramList.Add(MakeParam("@gatu_jisseki_arari12", SqlDbType.BigInt, 12, drData("uri_arari12")))      '12月_実績粗利
            paramList.Add(MakeParam("@gatu_jisseki_kensuu1", SqlDbType.BigInt, 12, drData("uri_suu1")))         '1月_実績件数
            paramList.Add(MakeParam("@gatu_jisseki_kingaku1", SqlDbType.BigInt, 12, drData("uri_gaku1")))       '1月_実績金額
            paramList.Add(MakeParam("@gatu_jisseki_arari1", SqlDbType.BigInt, 12, drData("uri_arari1")))        '1月_実績粗利
            paramList.Add(MakeParam("@gatu_jisseki_kensuu2", SqlDbType.BigInt, 12, drData("uri_suu2")))         '2月_実績件数
            paramList.Add(MakeParam("@gatu_jisseki_kingaku2", SqlDbType.BigInt, 12, drData("uri_gaku2")))       '2月_実績金額
            paramList.Add(MakeParam("@gatu_jisseki_arari2", SqlDbType.BigInt, 12, drData("uri_arari2")))        '2月_実績粗利
            paramList.Add(MakeParam("@gatu_jisseki_kensuu3", SqlDbType.BigInt, 12, drData("uri_suu3")))         '3月_実績件数
            paramList.Add(MakeParam("@gatu_jisseki_kingaku3", SqlDbType.BigInt, 12, drData("uri_gaku3")))       '3月_実績金額
            paramList.Add(MakeParam("@gatu_jisseki_arari3", SqlDbType.BigInt, 12, drData("uri_arari3")))        '3月_実績粗利
            paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, CON_BATCH_ID))                 '登録ログインユーザーID

            Return objConnection.ExecuteNonQuery(CommandType.Text, sqlBuffer.ToString(), paramList.ToArray())
        Catch ex As Exception
            If IsNothing(ex.Data.Item("ERROR_LOG")) Then
                ex.Data.Add("ERROR_LOG", "実績管理テーブルのデータの挿入処理が異常終了しました。")
            End If

            Throw ex
        End Try
    End Function
#End Region
End Class


Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper

Public Class TokubetuTaiouMasterDataAccess

    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>商品コードを取得する</summary>
    ''' <returns>商品コードデータテーブル</returns>
    ''' <history>2011/03/03　ジン登閣(大連情報システム部)　新規作成</history>
    Public Function SelSyouhinCd() As Data.DataTable

        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	syouhin_cd ")                       '商品コード
            .AppendLine("	,(syouhin_cd + '：' + syouhin_mei) AS syouhin_mei ") '商品名（商品コード：商品名）
            .AppendLine("FROM ")
            .AppendLine("	m_syouhin WITH(READCOMMITTED) ")                        '商品マスタ
            .AppendLine("WHERE ")
            .AppendLine("	torikesi = 0 ")                     '取消
            .AppendLine("	AND ")
            .AppendLine("	souko_cd = '100' ")                 '倉庫コード
        End With

        'SQLコメント実行、戻りデータセット実装
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtSyouhinCd")

        '戻りデータテーブル
        Return dsReturn.Tables(0)

    End Function

    ''' <summary>調査方法を取得する</summary>
    ''' <returns>調査方法データテーブル</returns>
    ''' <history>2011/03/03　ジン登閣(大連情報システム部)　新規作成</history>
    Public Function SelTyousaHouhou() As Data.DataTable

        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT  ")
            .AppendLine("	tys_houhou_no ")                                                            '調査方法NO
            .AppendLine("	,LTRIM(STR(tys_houhou_no) + '：' + tys_houhou_mei) AS tys_houhou_mei ")     '調査方法名称
            .AppendLine("FROM ")
            .AppendLine("	m_tyousahouhou WITH(READCOMMITTED) ")                                       '調査方法マスタ
        End With

        'SQLコメント実行、戻りデータセット実装
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtTyousahouhou")

        '戻りデータテーブル
        Return dsReturn.Tables(0)

    End Function

    '''' <summary>加盟店商品調査方法特別対応マスタ情報を取得</summary>
    'Public Function SelTokubetuTaiouJyouhou(ByVal dtParamList As Dictionary(Of String, String)) As Data.DataTable

    '    '戻りデータセット
    '    Dim dsReturn As New Data.DataSet

    '    'SQLコメント
    '    Dim commandTextSb As New StringBuilder

    '    'パラメータ格納
    '    Dim paramList As New List(Of SqlClient.SqlParameter)

    '    'SQL文
    '    With commandTextSb
    '        .AppendLine("SELECT ")
    '        If dtParamList.Item("kensuu").ToString <> "max" Then
    '            .AppendLine("   TOP " & dtParamList.Item("kensuu").ToString & "")
    '        End If
    '        .AppendLine("	MKSTTT.kameiten_cd ")   '--加盟店コード
    '        .AppendLine("	,MK.kameiten_mei1 ")     '--加盟店名称
    '        .AppendLine("	,MKSTTT.syouhin_cd ")    '--商品コード
    '        .AppendLine("	,MS.syouhin_mei ")       '--商品名称
    '        .AppendLine("	,(LTRIM(STR(MKSTTT.tys_houhou_no)) + '：' +MT.tys_houhou_mei) AS tys_houhou ") '--調査方法(調査方法NO:調査方法名称)
    '        .AppendLine("	,MKSTTT.tys_houhou_no ")    '--調査方法コード
    '        .AppendLine("	,MKSTTT.tokubetu_taiou_cd ")     '--特別対応コード
    '        .AppendLine("	,MTT.tokubetu_taiou_meisyou ")   '--特別対応名称
    '        .AppendLine("	,CASE ")
    '        .AppendLine("		WHEN MKSTTT.torikesi = 0 THEN ")
    '        .AppendLine("			'' ")
    '        .AppendLine("		ELSE ")
    '        .AppendLine("			'取消' ")
    '        .AppendLine("		END AS torikesi ")  '--取消
    '        .AppendLine("FROM")
    '        .AppendLine("	m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH (READCOMMITTED) ")    '--加盟店商品調査方法特別対応マスタ
    '        .AppendLine("	LEFT JOIN m_kameiten AS MK WITH (READCOMMITTED) ")      '--加盟店マスタ
    '        .AppendLine("	ON MKSTTT.kameiten_cd = MK.kameiten_cd ")       '--加盟店コード
    '        .AppendLine("	LEFT JOIN m_syouhin AS MS WITH (READCOMMITTED) ")       '--商品マスタ
    '        .AppendLine("	ON MKSTTT.syouhin_cd =	MS.syouhin_cd ")        '--商品コード
    '        .AppendLine("	AND MS.souko_cd = '100' ")      '--倉庫コード
    '        .AppendLine("	AND MS.torikesi = 0 ")        '--取消
    '        .AppendLine("	LEFT JOIN m_tyousahouhou AS MT WITH (READCOMMITTED) ")      '--調査方法マスタ
    '        .AppendLine("	ON MKSTTT.tys_houhou_no = MT.tys_houhou_no ")       '--調査方法NO
    '        .AppendLine("	LEFT JOIN m_tokubetu_taiou AS MTT WITH (READCOMMITTED) ")       '--特別対応マスタ
    '        .AppendLine("	ON MKSTTT.tokubetu_taiou_cd = MTT.tokubetu_taiou_cd ")      '--特別対応コード
    '        .AppendLine("WHERE  (1=1) ")
    '        If dtParamList.Item("KameitenCdFrom").ToString <> String.Empty And dtParamList.Item("KameitenCdTo").ToString <> String.Empty Then
    '            .AppendLine("   AND MKSTTT.kameiten_cd BETWEEN @KameitenCdFrom AND @KameitenCdTo ")
    '            paramList.Add(MakeParam("@KameitenCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("KameitenCdFrom").ToString))
    '            paramList.Add(MakeParam("@KameitenCdTo", SqlDbType.VarChar, 5, dtParamList.Item("KameitenCdTo").ToString))
    '        End If
    '        If dtParamList.Item("KameitenCdFrom").ToString <> String.Empty And dtParamList.Item("KameitenCdTo").ToString = String.Empty Then
    '            .AppendLine("   AND MKSTTT.kameiten_cd = @KameitenCdFrom ")
    '            paramList.Add(MakeParam("@KameitenCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("KameitenCdFrom").ToString))
    '        End If
    '        If dtParamList.Item("KameitenCdFrom").ToString = String.Empty And dtParamList.Item("KameitenCdTo").ToString <> String.Empty Then
    '            .AppendLine("   AND MKSTTT.kameiten_cd = @KameitenCdTo ")
    '            paramList.Add(MakeParam("@KameitenCdTo", SqlDbType.VarChar, 5, dtParamList.Item("KameitenCdTo").ToString))
    '        End If
    '        If dtParamList.Item("syouhin_cd").ToString <> String.Empty Then
    '            .AppendLine("   AND MKSTTT.syouhin_cd = @syouhin_cd ")
    '            paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtParamList.Item("syouhin_cd").ToString))
    '        End If
    '        If dtParamList.Item("tys_houhou_no").ToString <> String.Empty Then
    '            .AppendLine("   AND MKSTTT.tys_houhou_no = @tys_houhou_no ")
    '            paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 5, dtParamList.Item("tys_houhou_no")))
    '        End If
    '        If dtParamList.Item("tokubetu_taiou_cd").ToString <> String.Empty Then
    '            .AppendLine("   AND MKSTTT.tokubetu_taiou_cd = @tokubetu_taiou_cd ")
    '            paramList.Add(MakeParam("@tokubetu_taiou_cd", SqlDbType.Int, 5, dtParamList.Item("tokubetu_taiou_cd")))
    '        End If
    '        If dtParamList.Item("torikesiFlg").ToString = "1" Then
    '            .AppendLine("   AND MKSTTT.torikesi = 0 ")
    '        End If
    '        .AppendLine("ORDER BY")
    '        .AppendLine("   MKSTTT.kameiten_cd ")
    '        .AppendLine("   ,MKSTTT.syouhin_cd ")
    '        .AppendLine("   ,MKSTTT.tys_houhou_no  ")
    '    End With

    '    'SQLコメント実行、戻りデータセット実装
    '    FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "TokubetuTaiouJyouhou", paramList.ToArray)

    '    '戻りデータテーブル
    '    Return dsReturn.Tables(0)

    'End Function

    ''' <summary>加盟店商品調査方法特別対応マスタ情報を取得</summary>
    Public Function SelTokubetuTaiouJyouhou(ByVal dtParamList As Dictionary(Of String, String)) As Data.DataTable

        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            If dtParamList.Item("kensuu").ToString <> "max" Then
                .AppendLine("   TOP " & dtParamList.Item("kensuu").ToString & "")
            End If
            .AppendLine("	MKSTTT.aitesaki_syubetu ")              '相手先種別
            .AppendLine("	,(LTRIM(STR(MKSTTT.aitesaki_syubetu)) + '：' +MKM.meisyou) AS aitesaki_syubetu_layout ") '相手先種別(相手先種別:拡張名称)
            .AppendLine("	,MKSTTT.aitesaki_cd ")                  '相手先コード
            .AppendLine("	,MKM.meisyou ")                         '拡張名称
            .AppendLine("	,SUB.aitesaki_name ")                   '相手先名
            .AppendLine("	,MKSTTT.syouhin_cd ")                   '商品コード
            .AppendLine("	,MS.syouhin_mei ")                      '商品名称
            .AppendLine("	,MKSTTT.tys_houhou_no ")                '調査方法NO
            .AppendLine("	,MT.tys_houhou_mei ")                   '調査方法名称
            .AppendLine("	,(LTRIM(STR(MKSTTT.tys_houhou_no)) + '：' +MT.tys_houhou_mei) AS tys_houhou ") '調査方法(調査方法NO:調査方法名称)
            .AppendLine("	,MKSTTT.tokubetu_taiou_cd ")            '特別対応コード
            .AppendLine("	,MTT.tokubetu_taiou_meisyou ")          '特別対応名称
            .AppendLine("	,MKSTTT.torikesi ")                     '取消
            .AppendLine("	,MKSTTT.kasan_syouhin_cd ")             '金額加算商品コード
            .AppendLine("	,MKSTTT.syokiti ")                      '初期値
            .AppendLine("	,MKSTTT.uri_kasan_gaku ")               '実請求加算金額
            .AppendLine("	,MKSTTT.koumuten_kasan_gaku ")          '工務店請求加算金額
            .AppendLine("FROM")
            .AppendLine("	m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH (READCOMMITTED) ")    '加盟店商品調査方法特別対応マスタ
            .AppendLine("LEFT JOIN (")
            .AppendLine("	SELECT ")
            .AppendLine("	    '0' AS aitesaki_syubetu, ")
            .AppendLine("	    'ALL' AS aitesaki_cd, ")
            .AppendLine("	    '相手先なし' AS aitesaki_name, ")
            .AppendLine("	    '0' AS torikesi ")
            .AppendLine("	UNION ALL ")
            .AppendLine("	SELECT ")
            .AppendLine("	    '1' AS aitesaki_syubetu, ")
            .AppendLine("	    MK.kameiten_cd AS aitesaki_cd, ")
            .AppendLine("	    MK.kameiten_mei1 AS aitesaki_name, ")
            .AppendLine("	    MK.torikesi AS torikesi ")
            .AppendLine("	FROM ")
            .AppendLine("	    m_kameiten AS MK WITH (READCOMMITTED) ")
            .AppendLine("	UNION ALL ")
            .AppendLine("	SELECT ")
            .AppendLine("	    '5' AS aitesaki_syubetu, ")
            .AppendLine("	    ME.eigyousyo_cd AS aitesaki_cd, ")
            .AppendLine("	    ME.eigyousyo_mei AS aitesaki_name, ")
            .AppendLine("	    ME.torikesi AS torikesi ")
            .AppendLine("	FROM ")
            .AppendLine("	    m_eigyousyo AS ME WITH (READCOMMITTED) ")
            .AppendLine("	UNION ALL ")
            .AppendLine("	SELECT ")
            .AppendLine("	    '7' AS aitesaki_syubetu, ")
            .AppendLine("	    MKR.keiretu_cd AS aitesaki_cd, ")
            .AppendLine("	    MIN(MKR.keiretu_mei) AS aitesaki_name, ")
            .AppendLine("	    MIN(MKR.torikesi) AS torikesi ")
            .AppendLine("	FROM ")
            .AppendLine("	    m_keiretu AS MKR WITH (READCOMMITTED) ")
            .AppendLine("	GROUP BY MKR.keiretu_cd ")
            .AppendLine(") AS SUB")
            .AppendLine("ON")
            .AppendLine("	MKSTTT.aitesaki_syubetu = SUB.aitesaki_syubetu")
            .AppendLine("	AND MKSTTT.aitesaki_cd = SUB.aitesaki_cd")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_kakutyou_meisyou AS MKM WITH (READCOMMITTED)")
            .AppendLine("ON")
            .AppendLine("	MKSTTT.aitesaki_syubetu = MKM.code")
            .AppendLine("	AND MKM.meisyou_syubetu = '23'")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_syouhin AS MS WITH (READCOMMITTED)")
            .AppendLine("ON")
            .AppendLine("	MS.syouhin_cd = MKSTTT.syouhin_cd")
            .AppendLine("	AND MS.souko_cd = '100'")
            .AppendLine("	AND MS.torikesi = '0'")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_tyousahouhou AS MT WITH (READCOMMITTED)")
            .AppendLine("ON")
            .AppendLine("	MT.tys_houhou_no = MKSTTT.tys_houhou_no")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_tokubetu_taiou AS MTT WITH (READCOMMITTED)")
            .AppendLine("ON")
            .AppendLine("	MTT.tokubetu_taiou_cd = MKSTTT.tokubetu_taiou_cd")
            .AppendLine("WHERE  (1=1) ")
            If Not String.IsNullOrEmpty(dtParamList.Item("aitesakiSyubetu").ToString) Then
                .AppendLine("   AND MKSTTT.aitesaki_syubetu = @aitesaki_syubetu")
                paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 5, dtParamList.Item("aitesakiSyubetu").ToString))
            End If

            If Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                .AppendLine("   AND MKSTTT.aitesaki_cd BETWEEN @aitesakiCdFrom AND @aitesakiCdTo ")
                paramList.Add(MakeParam("@aitesakiCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdFrom").ToString))
                paramList.Add(MakeParam("@aitesakiCdTo", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdTo").ToString))
            End If

            If Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                .AppendLine("   AND MKSTTT.aitesaki_cd = @aitesakiCdFrom ")
                paramList.Add(MakeParam("@aitesakiCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdFrom").ToString))
            End If

            If String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                .AppendLine("   AND MKSTTT.aitesaki_cd = @aitesakiCdTo ")
                paramList.Add(MakeParam("@aitesakiCdTo", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdTo").ToString))
            End If

            If Not String.IsNullOrEmpty(dtParamList.Item("syouhin_cd").ToString) Then
                .AppendLine("   AND MKSTTT.syouhin_cd = @syouhin_cd ")
                paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtParamList.Item("syouhin_cd").ToString))
            End If

            If Not String.IsNullOrEmpty(dtParamList.Item("tys_houhou_no").ToString) Then
                .AppendLine("   AND MKSTTT.tys_houhou_no = @tys_houhou_no ")
                paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 5, dtParamList.Item("tys_houhou_no")))
            End If

            If Not String.IsNullOrEmpty(dtParamList.Item("tokubetu_taiou_cd").ToString) Then
                .AppendLine("   AND MKSTTT.tokubetu_taiou_cd = @tokubetu_taiou_cd ")
                paramList.Add(MakeParam("@tokubetu_taiou_cd", SqlDbType.Int, 5, dtParamList.Item("tokubetu_taiou_cd")))
            End If

            If dtParamList.Item("torikesiFlg").ToString.Equals("1") Then
                .AppendLine("   AND MKSTTT.torikesi = 0 ")
            End If

            '============2012/05/21 車龍 407553の対応 追加↓===============================
            '取消相手先は対象外=チェックの場合
            If dtParamList.Item("aitesakiTorikesiFlg").ToString.Equals("1") Then
                .AppendLine("   AND SUB.torikesi = 0 ")
            End If

            '\0は対象外=チェックの場合
            If dtParamList.Item("kingaku0TorikesiFlg").ToString.Equals("1") Then
                .AppendLine("   AND MKSTTT.uri_kasan_gaku <> 0 ")
            End If
            '============2012/05/21 車龍 407553の対応 追加↑===============================

            '---------------------------from 2013.03.09李宇追加-----------------------------
            '初期値1のみ=チェックの場合、サブ加盟店商品調査方法特別対応M.初期値　=　"1"
            If dtParamList.Item("Syokiti1Nomi").ToString.Equals("1") Then
                .AppendLine("   AND MKSTTT.syokiti = '1' ")
            End If
            '---------------------------to   2013.03.09李宇追加-----------------------------

            .AppendLine("ORDER BY")
            .AppendLine("   MKSTTT.aitesaki_syubetu ")
            .AppendLine("   ,MKSTTT.aitesaki_cd ")
            .AppendLine("   ,MKSTTT.syouhin_cd ")
            .AppendLine("   ,MKSTTT.tys_houhou_no  ")
            .AppendLine("   ,MKSTTT.tokubetu_taiou_cd  ")
        End With

        'SQLコメント実行、戻りデータセット実装
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "TokubetuTaiouJyouhou", paramList.ToArray)

        '戻りデータテーブル
        Return dsReturn.Tables(0)

    End Function

    ''' <summary>相手先種別情報を取得</summary>
    Public Function SelAitesakiSyubetuList() As Data.DataTable
        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        Dim strSql As String = "SELECT code+':'+meisyou AS name, code AS value FROM m_kakutyou_meisyou WHERE meisyou_syubetu = '23'"

        'SQLコメント実行、戻りデータセット実装
        FillDataset(connStr, CommandType.Text, strSql, dsReturn, "AitesakiSyubetuList")

        '戻りデータテーブル
        Return dsReturn.Tables(0)
    End Function

    '''' <summary>加盟店商品調査方法特別対応マスタ全部検索件数を取得</summary>
    'Public Function SelTokubetuTaiouCount(ByVal dtParamList As Dictionary(Of String, String)) As Integer

    '    '戻りデータセット
    '    Dim dsReturn As New Data.DataSet

    '    'SQLコメント
    '    Dim commandTextSb As New StringBuilder

    '    'パラメータ格納
    '    Dim paramList As New List(Of SqlClient.SqlParameter)

    '    'SQL文
    '    With commandTextSb
    '        .AppendLine("SELECT ")
    '        .AppendLine("	COUNT(*) AS kameiten_cd ")   '--全部加盟店レコード数
    '        .AppendLine("FROM")
    '        .AppendLine("	m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH (READCOMMITTED) ")    '--加盟店商品調査方法特別対応マスタ
    '        .AppendLine("	LEFT JOIN m_kameiten AS MK WITH (READCOMMITTED) ")      '--加盟店マスタ
    '        .AppendLine("	ON MKSTTT.kameiten_cd = MK.kameiten_cd ")       '--加盟店コード
    '        .AppendLine("	LEFT JOIN m_syouhin AS MS WITH (READCOMMITTED) ")       '--商品マスタ
    '        .AppendLine("	ON MKSTTT.syouhin_cd =	MS.syouhin_cd ")        '--商品コード
    '        .AppendLine("	AND MS.souko_cd = '100' ")      '--倉庫コード
    '        .AppendLine("	AND MS.torikesi = 0 ")        '--取消
    '        .AppendLine("	LEFT JOIN m_tyousahouhou AS MT WITH (READCOMMITTED) ")      '--調査方法マスタ
    '        .AppendLine("	ON MKSTTT.tys_houhou_no = MT.tys_houhou_no ")       '--調査方法NO
    '        .AppendLine("	LEFT JOIN m_tokubetu_taiou AS MTT WITH (READCOMMITTED) ")       '--特別対応マスタ
    '        .AppendLine("	ON MKSTTT.tokubetu_taiou_cd = MTT.tokubetu_taiou_cd ")      '--特別対応コード
    '        .AppendLine("WHERE  (1=1) ")
    '        If dtParamList.Item("KameitenCdFrom").ToString <> String.Empty And dtParamList.Item("KameitenCdTo").ToString <> String.Empty Then
    '            .AppendLine("   AND MKSTTT.kameiten_cd BETWEEN @KameitenCdFrom AND @KameitenCdTo ")
    '            paramList.Add(MakeParam("@KameitenCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("KameitenCdFrom").ToString))
    '            paramList.Add(MakeParam("@KameitenCdTo", SqlDbType.VarChar, 5, dtParamList.Item("KameitenCdTo").ToString))
    '        End If
    '        If dtParamList.Item("KameitenCdFrom").ToString <> String.Empty And dtParamList.Item("KameitenCdTo").ToString = String.Empty Then
    '            .AppendLine("   AND MKSTTT.kameiten_cd = @KameitenCdFrom ")
    '            paramList.Add(MakeParam("@KameitenCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("KameitenCdFrom").ToString))
    '        End If
    '        If dtParamList.Item("KameitenCdFrom").ToString = String.Empty And dtParamList.Item("KameitenCdTo").ToString <> String.Empty Then
    '            .AppendLine("   AND MKSTTT.kameiten_cd = @KameitenCdTo ")
    '            paramList.Add(MakeParam("@KameitenCdTo", SqlDbType.VarChar, 5, dtParamList.Item("KameitenCdTo").ToString))
    '        End If
    '        If dtParamList.Item("syouhin_cd").ToString <> String.Empty Then
    '            .AppendLine("   AND MKSTTT.syouhin_cd = @syouhin_cd ")
    '            paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtParamList.Item("syouhin_cd").ToString))
    '        End If
    '        If dtParamList.Item("tys_houhou_no").ToString <> String.Empty Then
    '            .AppendLine("   AND MKSTTT.tys_houhou_no = @tys_houhou_no ")
    '            paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 5, dtParamList.Item("tys_houhou_no")))
    '        End If
    '        If dtParamList.Item("tokubetu_taiou_cd").ToString <> String.Empty Then
    '            .AppendLine("   AND MKSTTT.tokubetu_taiou_cd = @tokubetu_taiou_cd ")
    '            paramList.Add(MakeParam("@tokubetu_taiou_cd", SqlDbType.Int, 5, dtParamList.Item("tokubetu_taiou_cd")))
    '        End If
    '        If dtParamList.Item("torikesiFlg").ToString = "1" Then
    '            .AppendLine("   AND MKSTTT.torikesi = 0 ")
    '        End If
    '    End With

    '    'SQLコメント実行、戻りデータセット実装
    '    FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "KensakuKensuu", paramList.ToArray)

    '    '戻りデータ
    '    Return dsReturn.Tables(0).Rows(0).Item(0)

    'End Function

    ''' <summary>加盟店商品調査方法特別対応マスタ全部検索件数を取得</summary>
    Public Function SelTokubetuTaiouCount(ByVal dtParamList As Dictionary(Of String, String)) As Integer

        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	COUNT(*) AS kameiten_cd ")                                              '全部レコード数
            .AppendLine("FROM")
            .AppendLine("	m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH (READCOMMITTED) ")    '加盟店商品調査方法特別対応マスタ
            .AppendLine("LEFT JOIN (")
            .AppendLine("	SELECT ")
            .AppendLine("	    '0' AS aitesaki_syubetu, ")
            .AppendLine("	    'ALL' AS aitesaki_cd, ")
            .AppendLine("	    '相手先なし' AS aitesaki_name, ")
            .AppendLine("	    '0' AS torikesi ")
            .AppendLine("	UNION ALL ")
            .AppendLine("	SELECT ")
            .AppendLine("	    '1' AS aitesaki_syubetu, ")
            .AppendLine("	    MK.kameiten_cd AS aitesaki_cd, ")
            .AppendLine("	    MK.kameiten_mei1 AS aitesaki_name, ")
            .AppendLine("	    MK.torikesi AS torikesi ")
            .AppendLine("	FROM ")
            .AppendLine("	    m_kameiten AS MK WITH (READCOMMITTED) ")
            .AppendLine("	UNION ALL ")
            .AppendLine("	SELECT ")
            .AppendLine("	    '5' AS aitesaki_syubetu, ")
            .AppendLine("	    ME.eigyousyo_cd AS aitesaki_cd, ")
            .AppendLine("	    ME.eigyousyo_mei AS aitesaki_name, ")
            .AppendLine("	    ME.torikesi AS torikesi ")
            .AppendLine("	FROM ")
            .AppendLine("	    m_eigyousyo AS ME WITH (READCOMMITTED) ")
            .AppendLine("	UNION ALL ")
            .AppendLine("	SELECT ")
            .AppendLine("	    '7' AS aitesaki_syubetu, ")
            .AppendLine("	    MKR.keiretu_cd AS aitesaki_cd, ")
            .AppendLine("	    MIN(MKR.keiretu_mei) AS aitesaki_name, ")
            .AppendLine("	    MIN(MKR.torikesi) AS torikesi ")
            .AppendLine("	FROM ")
            .AppendLine("	    m_keiretu AS MKR WITH (READCOMMITTED) ")
            .AppendLine("	GROUP BY MKR.keiretu_cd ")
            .AppendLine(") AS SUB")
            .AppendLine("ON")
            .AppendLine("	MKSTTT.aitesaki_syubetu = SUB.aitesaki_syubetu")
            .AppendLine("	AND MKSTTT.aitesaki_cd = SUB.aitesaki_cd")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_kakutyou_meisyou AS MKM WITH (READCOMMITTED)")
            .AppendLine("ON")
            .AppendLine("	MKSTTT.aitesaki_syubetu = MKM.code")
            .AppendLine("	AND MKM.meisyou_syubetu = '23'")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_syouhin AS MS WITH (READCOMMITTED)")
            .AppendLine("ON")
            .AppendLine("	MS.syouhin_cd = MKSTTT.syouhin_cd")
            .AppendLine("	AND MS.souko_cd = '100'")
            .AppendLine("	AND MS.torikesi = '0'")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_tyousahouhou AS MT WITH (READCOMMITTED)")
            .AppendLine("ON")
            .AppendLine("	MT.tys_houhou_no = MKSTTT.tys_houhou_no")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_tokubetu_taiou AS MTT WITH (READCOMMITTED)")
            .AppendLine("ON")
            .AppendLine("	MTT.tokubetu_taiou_cd = MKSTTT.tokubetu_taiou_cd")
            .AppendLine("WHERE  (1=1) ")
            If Not String.IsNullOrEmpty(dtParamList.Item("aitesakiSyubetu").ToString) Then
                .AppendLine("   AND MKSTTT.aitesaki_syubetu = @aitesaki_syubetu")
                paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 5, dtParamList.Item("aitesakiSyubetu").ToString))
            End If

            If Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                .AppendLine("   AND MKSTTT.aitesaki_cd BETWEEN @aitesakiCdFrom AND @aitesakiCdTo ")
                paramList.Add(MakeParam("@aitesakiCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdFrom").ToString))
                paramList.Add(MakeParam("@aitesakiCdTo", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdTo").ToString))
            End If

            If Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                .AppendLine("   AND MKSTTT.aitesaki_cd = @aitesakiCdFrom ")
                paramList.Add(MakeParam("@aitesakiCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdFrom").ToString))
            End If

            If String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                .AppendLine("   AND MKSTTT.aitesaki_cd = @aitesakiCdTo ")
                paramList.Add(MakeParam("@aitesakiCdTo", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdTo").ToString))
            End If

            If Not String.IsNullOrEmpty(dtParamList.Item("syouhin_cd").ToString) Then
                .AppendLine("   AND MKSTTT.syouhin_cd = @syouhin_cd ")
                paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtParamList.Item("syouhin_cd").ToString))
            End If

            If Not String.IsNullOrEmpty(dtParamList.Item("tys_houhou_no").ToString) Then
                .AppendLine("   AND MKSTTT.tys_houhou_no = @tys_houhou_no ")
                paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 5, dtParamList.Item("tys_houhou_no")))
            End If

            If Not String.IsNullOrEmpty(dtParamList.Item("tokubetu_taiou_cd").ToString) Then
                .AppendLine("   AND MKSTTT.tokubetu_taiou_cd = @tokubetu_taiou_cd ")
                paramList.Add(MakeParam("@tokubetu_taiou_cd", SqlDbType.Int, 5, dtParamList.Item("tokubetu_taiou_cd")))
            End If

            If dtParamList.Item("torikesiFlg").ToString.Equals("1") Then
                .AppendLine("   AND MKSTTT.torikesi = 0 ")
            End If

            '============2012/05/21 車龍 407553の対応 追加↓===============================
            '取消相手先は対象外=チェックの場合
            If dtParamList.Item("aitesakiTorikesiFlg").ToString.Equals("1") Then
                .AppendLine("   AND SUB.torikesi = 0 ")
            End If

            '\0は対象外=チェックの場合
            If dtParamList.Item("kingaku0TorikesiFlg").ToString.Equals("1") Then
                .AppendLine("   AND MKSTTT.uri_kasan_gaku <> 0 ")
            End If
            '============2012/05/21 車龍 407553の対応 追加↑===============================

            '---------------------------from 2013.03.09李宇追加-----------------------------
            '初期値1のみ=チェックの場合、サブ加盟店商品調査方法特別対応M.初期値　=　"1"
            If dtParamList.Item("Syokiti1Nomi").ToString.Equals("1") Then
                .AppendLine("   AND MKSTTT.syokiti = '1' ")
            End If
            '---------------------------to   2013.03.09李宇追加-----------------------------

        End With

        'SQLコメント実行、戻りデータセット実装
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "KensakuKensuu", paramList.ToArray)

        '戻りデータ
        Return dsReturn.Tables(0).Rows(0).Item(0)

    End Function

    '''' <summary>加盟店商品調査方法特別対応マスタCSV情報を取得</summary>
    'Public Function SelTokubetuTaiouJyouhouCSV(ByVal dtParamList As Dictionary(Of String, String)) As Data.DataTable

    '    '戻りデータセット
    '    Dim dsReturn As New Data.DataSet

    '    'SQLコメント
    '    Dim commandTextSb As New StringBuilder

    '    'パラメータ格納
    '    Dim paramList As New List(Of SqlClient.SqlParameter)

    '    'SQL文
    '    With commandTextSb
    '        .AppendLine("SELECT ")
    '        'EDI情報作成日
    '        .AppendLine("   RIGHT(CONVERT(varchar(10),GETDATE(),112),4)+REPLACE(CONVERT(varchar(8),GETDATE(),108),':','') AS edi_jouhou_sakusei_date ")
    '        .AppendLine("	,ISNULL(MKSTTT.kameiten_cd,'') ")   '--加盟店コード
    '        .AppendLine("	,ISNULL(MK.kameiten_mei1,'') ")     '--加盟店名称
    '        .AppendLine("	,ISNULL(MKSTTT.syouhin_cd,'') ")    '--商品コード
    '        .AppendLine("	,ISNULL(MS.syouhin_mei,'') ")       '--商品名称
    '        .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MKSTTT.tys_houhou_no),'') ") '--調査方法NO
    '        .AppendLine("	,ISNULL(MT.tys_houhou_mei,'') ")    '--調査方法名称
    '        .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MKSTTT.tokubetu_taiou_cd),'') ")     '--特別対応コード
    '        .AppendLine("	,ISNULL(MTT.tokubetu_taiou_meisyou,'') ")   '--特別対応名称
    '        .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MKSTTT.torikesi),'') ")      '--取消
    '        .AppendLine("FROM")
    '        .AppendLine("	m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH (READCOMMITTED) ")    '--加盟店商品調査方法特別対応マスタ
    '        .AppendLine("	LEFT JOIN m_kameiten AS MK WITH (READCOMMITTED) ")      '--加盟店マスタ
    '        .AppendLine("	ON MKSTTT.kameiten_cd = MK.kameiten_cd ")       '--加盟店コード
    '        .AppendLine("	LEFT JOIN m_syouhin AS MS WITH (READCOMMITTED) ")       '--商品マスタ
    '        .AppendLine("	ON MKSTTT.syouhin_cd =	MS.syouhin_cd ")        '--商品コード
    '        .AppendLine("	AND MS.souko_cd = '100' ")      '--倉庫コード
    '        .AppendLine("	AND MS.torikesi = 0 ")        '--取消
    '        .AppendLine("	LEFT JOIN m_tyousahouhou AS MT WITH (READCOMMITTED) ")      '--調査方法マスタ
    '        .AppendLine("	ON MKSTTT.tys_houhou_no = MT.tys_houhou_no ")       '--調査方法NO
    '        .AppendLine("	LEFT JOIN m_tokubetu_taiou AS MTT WITH (READCOMMITTED) ")       '--特別対応マスタ
    '        .AppendLine("	ON MKSTTT.tokubetu_taiou_cd = MTT.tokubetu_taiou_cd ")      '--特別対応コード
    '        .AppendLine("WHERE  (1=1) ")
    '        If dtParamList.Item("KameitenCdFrom").ToString <> String.Empty And dtParamList.Item("KameitenCdTo").ToString <> String.Empty Then
    '            .AppendLine("   AND MKSTTT.kameiten_cd BETWEEN @KameitenCdFrom AND @KameitenCdTo ")
    '            paramList.Add(MakeParam("@KameitenCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("KameitenCdFrom").ToString))
    '            paramList.Add(MakeParam("@KameitenCdTo", SqlDbType.VarChar, 5, dtParamList.Item("KameitenCdTo").ToString))
    '        End If
    '        If dtParamList.Item("KameitenCdFrom").ToString <> String.Empty And dtParamList.Item("KameitenCdTo").ToString = String.Empty Then
    '            .AppendLine("   AND MKSTTT.kameiten_cd = @KameitenCdFrom ")
    '            paramList.Add(MakeParam("@KameitenCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("KameitenCdFrom").ToString))
    '        End If
    '        If dtParamList.Item("KameitenCdFrom").ToString = String.Empty And dtParamList.Item("KameitenCdTo").ToString <> String.Empty Then
    '            .AppendLine("   AND MKSTTT.kameiten_cd = @KameitenCdTo ")
    '            paramList.Add(MakeParam("@KameitenCdTo", SqlDbType.VarChar, 5, dtParamList.Item("KameitenCdTo").ToString))
    '        End If
    '        If dtParamList.Item("syouhin_cd").ToString <> String.Empty Then
    '            .AppendLine("   AND MKSTTT.syouhin_cd = @syouhin_cd ")
    '            paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtParamList.Item("syouhin_cd").ToString))
    '        End If
    '        If dtParamList.Item("tys_houhou_no").ToString <> String.Empty Then
    '            .AppendLine("   AND MKSTTT.tys_houhou_no = @tys_houhou_no ")
    '            paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 5, dtParamList.Item("tys_houhou_no")))
    '        End If
    '        If dtParamList.Item("tokubetu_taiou_cd").ToString <> String.Empty Then
    '            .AppendLine("   AND MKSTTT.tokubetu_taiou_cd = @tokubetu_taiou_cd ")
    '            paramList.Add(MakeParam("@tokubetu_taiou_cd", SqlDbType.Int, 5, dtParamList.Item("tokubetu_taiou_cd")))
    '        End If
    '        If dtParamList.Item("torikesiFlg").ToString = "1" Then
    '            .AppendLine("   AND MKSTTT.torikesi = 0 ")
    '        End If
    '        .AppendLine("ORDER BY")
    '        .AppendLine("   MKSTTT.kameiten_cd ")
    '        .AppendLine("   ,MKSTTT.syouhin_cd ")
    '        .AppendLine("   ,MKSTTT.tys_houhou_no  ")
    '        .AppendLine("   ,MKSTTT.tokubetu_taiou_cd")
    '    End With

    '    'SQLコメント実行、戻りデータセット実装
    '    FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "TokubetuTaiouJyouhouCSV", paramList.ToArray)

    '    '戻りデータテーブル
    '    Return dsReturn.Tables(0)

    'End Function

    ''' <summary>加盟店商品調査方法特別対応マスタCSV情報を取得</summary>
    Public Function SelTokubetuTaiouJyouhouCSV(ByVal dtParamList As Dictionary(Of String, String)) As Data.DataTable

        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            'EDI情報作成日
            .AppendLine("   RIGHT(CONVERT(varchar(10),GETDATE(),112),4)+REPLACE(CONVERT(varchar(8),GETDATE(),108),':','') AS edi_jouhou_sakusei_date ")
            .AppendLine("	,ISNULL(MKSTTT.aitesaki_syubetu,'') ")   '--相手先種別
            .AppendLine("	,ISNULL(MKSTTT.aitesaki_cd,'') ")   '--相手先コード
            .AppendLine("	,ISNULL(SUB.aitesaki_name,'') ")     '--相手先名称
            .AppendLine("	,ISNULL(MKSTTT.syouhin_cd,'') ")    '--商品コード
            .AppendLine("	,ISNULL(MS.syouhin_mei,'') ")       '--商品名称
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MKSTTT.tys_houhou_no),'') ") '--調査方法NO
            .AppendLine("	,ISNULL(MT.tys_houhou_mei,'') ")    '--調査方法名称
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MKSTTT.tokubetu_taiou_cd),'') ")     '--特別対応コード
            .AppendLine("	,ISNULL(MTT.tokubetu_taiou_meisyou,'') ")   '--特別対応名称
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MKSTTT.torikesi),'') ")      '--取消
            .AppendLine("	,ISNULL(MKSTTT.kasan_syouhin_cd,'') ")                      '--金額加算商品コード
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MKSTTT.syokiti),'') ")          '--初期値
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MKSTTT.uri_kasan_gaku),'') ")      '--実請求加算金額
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MKSTTT.koumuten_kasan_gaku),'') ")      '--工務店請求加算金額
            .AppendLine("FROM")
            .AppendLine("	m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH (READCOMMITTED) ")    '加盟店商品調査方法特別対応マスタ
            .AppendLine("LEFT JOIN (")
            .AppendLine("	SELECT ")
            .AppendLine("	    '0' AS aitesaki_syubetu, ")
            .AppendLine("	    'ALL' AS aitesaki_cd, ")
            .AppendLine("	    '相手先なし' AS aitesaki_name, ")
            .AppendLine("	    '0' AS torikesi ")
            .AppendLine("	UNION ALL ")
            .AppendLine("	SELECT ")
            .AppendLine("	    '1' AS aitesaki_syubetu, ")
            .AppendLine("	    MK.kameiten_cd AS aitesaki_cd, ")
            .AppendLine("	    MK.kameiten_mei1 AS aitesaki_name, ")
            .AppendLine("	    MK.torikesi AS torikesi ")
            .AppendLine("	FROM ")
            .AppendLine("	    m_kameiten AS MK WITH (READCOMMITTED) ")
            .AppendLine("	UNION ALL ")
            .AppendLine("	SELECT ")
            .AppendLine("	    '5' AS aitesaki_syubetu, ")
            .AppendLine("	    ME.eigyousyo_cd AS aitesaki_cd, ")
            .AppendLine("	    ME.eigyousyo_mei AS aitesaki_name, ")
            .AppendLine("	    ME.torikesi AS torikesi ")
            .AppendLine("	FROM ")
            .AppendLine("	    m_eigyousyo AS ME WITH (READCOMMITTED) ")
            .AppendLine("	UNION ALL ")
            .AppendLine("	SELECT ")
            .AppendLine("	    '7' AS aitesaki_syubetu, ")
            .AppendLine("	    MKR.keiretu_cd AS aitesaki_cd, ")
            .AppendLine("	    MIN(MKR.keiretu_mei) AS aitesaki_name, ")
            .AppendLine("	    MIN(MKR.torikesi) AS torikesi ")
            .AppendLine("	FROM ")
            .AppendLine("	    m_keiretu AS MKR WITH (READCOMMITTED) ")
            .AppendLine("	GROUP BY MKR.keiretu_cd ")
            .AppendLine(") AS SUB")
            .AppendLine("ON")
            .AppendLine("	MKSTTT.aitesaki_syubetu = SUB.aitesaki_syubetu")
            .AppendLine("	AND MKSTTT.aitesaki_cd = SUB.aitesaki_cd")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_kakutyou_meisyou AS MKM WITH (READCOMMITTED)")
            .AppendLine("ON")
            .AppendLine("	MKSTTT.aitesaki_syubetu = MKM.code")
            .AppendLine("	AND MKM.meisyou_syubetu = '23'")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_syouhin AS MS WITH (READCOMMITTED)")
            .AppendLine("ON")
            .AppendLine("	MS.syouhin_cd = MKSTTT.syouhin_cd")
            .AppendLine("	AND MS.souko_cd = '100'")
            .AppendLine("	AND MS.torikesi = '0'")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_tyousahouhou AS MT WITH (READCOMMITTED)")
            .AppendLine("ON")
            .AppendLine("	MT.tys_houhou_no = MKSTTT.tys_houhou_no")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_tokubetu_taiou AS MTT WITH (READCOMMITTED)")
            .AppendLine("ON")
            .AppendLine("	MTT.tokubetu_taiou_cd = MKSTTT.tokubetu_taiou_cd")
            .AppendLine("WHERE  (1=1) ")
            If Not String.IsNullOrEmpty(dtParamList.Item("aitesakiSyubetu").ToString) Then
                .AppendLine("   AND MKSTTT.aitesaki_syubetu = @aitesaki_syubetu")
                paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 5, dtParamList.Item("aitesakiSyubetu").ToString))
            End If

            If Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                .AppendLine("   AND MKSTTT.aitesaki_cd BETWEEN @aitesakiCdFrom AND @aitesakiCdTo ")
                paramList.Add(MakeParam("@aitesakiCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdFrom").ToString))
                paramList.Add(MakeParam("@aitesakiCdTo", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdTo").ToString))
            End If

            If Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                .AppendLine("   AND MKSTTT.aitesaki_cd = @aitesakiCdFrom ")
                paramList.Add(MakeParam("@aitesakiCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdFrom").ToString))
            End If

            If String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                .AppendLine("   AND MKSTTT.aitesaki_cd = @aitesakiCdTo ")
                paramList.Add(MakeParam("@aitesakiCdTo", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdTo").ToString))
            End If

            If Not String.IsNullOrEmpty(dtParamList.Item("syouhin_cd").ToString) Then
                .AppendLine("   AND MKSTTT.syouhin_cd = @syouhin_cd ")
                paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtParamList.Item("syouhin_cd").ToString))
            End If

            If Not String.IsNullOrEmpty(dtParamList.Item("tys_houhou_no").ToString) Then
                .AppendLine("   AND MKSTTT.tys_houhou_no = @tys_houhou_no ")
                paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 5, dtParamList.Item("tys_houhou_no")))
            End If

            If Not String.IsNullOrEmpty(dtParamList.Item("tokubetu_taiou_cd").ToString) Then
                .AppendLine("   AND MKSTTT.tokubetu_taiou_cd = @tokubetu_taiou_cd ")
                paramList.Add(MakeParam("@tokubetu_taiou_cd", SqlDbType.Int, 5, dtParamList.Item("tokubetu_taiou_cd")))
            End If

            If dtParamList.Item("torikesiFlg").ToString.Equals("1") Then
                .AppendLine("   AND MKSTTT.torikesi = 0 ")
            End If
            '============2012/05/21 車龍 407553の対応 追加↓===============================
            '取消相手先は対象外=チェックの場合
            If dtParamList.Item("aitesakiTorikesiFlg").ToString.Equals("1") Then
                .AppendLine("   AND SUB.torikesi = 0 ")
            End If

            '\0は対象外=チェックの場合
            If dtParamList.Item("kingaku0TorikesiFlg").ToString.Equals("1") Then
                .AppendLine("   AND MKSTTT.uri_kasan_gaku <> 0 ")
            End If
            '============2012/05/21 車龍 407553の対応 追加↑===============================

            '------------------From 2013.03.09  李宇追加する-----------------
            '初期値1のみ=チェックの場合、加盟店商品調査方法特別対応M.初期値　=　"1"
            If dtParamList.Item("Syokiti1Nomi").ToString.Equals("1") Then
                .AppendLine("	AND ")
                .AppendLine("	MKSTTT.syokiti = '1'")
            End If
            '------------------To   2013.03.09  李宇追加する-----------------

            .AppendLine("ORDER BY")
            .AppendLine("   MKSTTT.aitesaki_syubetu ")
            .AppendLine("   ,MKSTTT.aitesaki_cd ")
            .AppendLine("   ,MKSTTT.syouhin_cd ")
            .AppendLine("   ,MKSTTT.tys_houhou_no  ")
            .AppendLine("   ,MKSTTT.tokubetu_taiou_cd  ")
        End With

        'SQLコメント実行、戻りデータセット実装
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "TokubetuTaiouJyouhouCSV", paramList.ToArray)

        '戻りデータテーブル
        Return dsReturn.Tables(0)

    End Function


    '''' <summary>未設定も含む加盟店商品調査方法特別対応マスタCSVデータを取得</summary>
    '''' <returns>未設定も含む加盟店商品調査方法特別対応マスタCSVテーブル</returns>
    'Public Function SelTokubetuTaiouCSV(ByVal dtParamList As Dictionary(Of String, String)) As Data.DataTable
    '    '戻りデータセット
    '    Dim dsReturn As New Data.DataSet

    '    'SQLコメント
    '    Dim commandTextSb As New StringBuilder

    '    'パラメータ格納
    '    Dim paramList As New List(Of SqlClient.SqlParameter)

    '    'SQL文
    '    With commandTextSb
    '        .AppendLine("SELECT ")
    '        'EDI情報作成日
    '        .AppendLine("   RIGHT(CONVERT(varchar(10),GETDATE(),112),4)+REPLACE(CONVERT(varchar(8),GETDATE(),108),':','') AS edi_jouhou_sakusei_date ")
    '        .AppendLine(" 	,ISNULL(SKM.kameiten_cd,'') ") '加盟店コード
    '        .AppendLine(" 	,ISNULL(SKM.kameiten_mei1,'') ")   '加盟店名称
    '        .AppendLine(" 	,ISNULL(SMS.syouhin_cd,'') ")  '商品コード
    '        .AppendLine(" 	,ISNULL(SMS.syouhin_mei,'') ") '商品名
    '        .AppendLine(" 	,ISNULL(CONVERT(VARCHAR(10),SMT.tys_houhou_no),'') ")   '調査方法NO
    '        .AppendLine(" 	,ISNULL(SMT.tys_houhou_mei,'') ")  '調査方法名
    '        .AppendLine(" 	,ISNULL(CONVERT(VARCHAR(10),SMTT.tokubetu_taiou_cd),'') ")  '特別対応コード
    '        .AppendLine(" 	,ISNULL(SMTT.tokubetu_taiou_meisyou,'') ") '特別対応名称
    '        .AppendLine(" 	,ISNULL(CONVERT(VARCHAR(10),MKSTTT.torikesi),'') ") '取消
    '        .AppendLine(" FROM ")
    '        .AppendLine(" 	(SELECT  ")
    '        .AppendLine(" 		tys_houhou_no ")
    '        .AppendLine(" 		,tys_houhou_mei ")
    '        .AppendLine(" 	 FROM ")
    '        .AppendLine(" 	  	m_tyousahouhou WITH(READCOMMITTED) ")   '調査方法マスタ
    '        .AppendLine(" 	 WHERE ")
    '        .AppendLine(" 	  	torikesi = 0 ")
    '        If dtParamList.Item("tys_houhou_no").ToString <> String.Empty Then
    '            .AppendLine(" 	AND tys_houhou_no = @tys_houhou_no  ")
    '            paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 5, dtParamList.Item("tys_houhou_no")))
    '        End If
    '        .AppendLine(" 	  )AS SMT ")    'サブ調査方法マスタ
    '        .AppendLine(" CROSS JOIN ")
    '        .AppendLine("  	 (SELECT ")
    '        .AppendLine("  	  	 syouhin_cd ")
    '        .AppendLine("  	  	 ,syouhin_mei ")
    '        .AppendLine("  	  FROM ")
    '        .AppendLine("  	  	  m_syouhin WITH(READCOMMITTED) ")  '商品マスタ
    '        .AppendLine(" 	  WHERE ")
    '        .AppendLine("  	  	  souko_cd = '100' AND torikesi = 0  ")
    '        If dtParamList.Item("syouhin_cd").ToString <> String.Empty Then
    '            .AppendLine(" 	  AND syouhin_cd =@syouhin_cd  ")
    '            paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtParamList.Item("syouhin_cd").ToString))
    '        End If
    '        .AppendLine("  	  )AS SMS ")    'サブ商品マスタ
    '        .AppendLine(" CROSS JOIN ")
    '        .AppendLine(" 	  (SELECT ")
    '        .AppendLine("  	  	  tokubetu_taiou_cd ")
    '        .AppendLine("  	  	  ,tokubetu_taiou_meisyou ")
    '        .AppendLine("  	   FROM ")
    '        .AppendLine("  	   	  m_tokubetu_taiou WITH(READCOMMITTED) ")   '特別対応マスタ
    '        .AppendLine("  	   WHERE ")
    '        .AppendLine(" 	      torikesi = 0  ")
    '        If dtParamList.Item("tokubetu_taiou_cd").ToString <> String.Empty Then
    '            .AppendLine("  	  AND tokubetu_taiou_cd = @tokubetu_taiou_cd ")
    '            paramList.Add(MakeParam("@tokubetu_taiou_cd", SqlDbType.Int, 5, dtParamList.Item("tokubetu_taiou_cd")))
    '        End If
    '        .AppendLine("  	   )AS SMTT ")  'サブ特別対応マスタ
    '        .AppendLine(" CROSS JOIN ")
    '        .AppendLine("  	   (SELECT ")
    '        .AppendLine("  	       kameiten_cd ")
    '        .AppendLine("  	       ,kameiten_mei1 ")
    '        .AppendLine("  	    FROM ")
    '        .AppendLine(" 	       m_kameiten WITH(READCOMMITTED) ")    '加盟店マスタ
    '        .AppendLine("       WHERE(1=1) ")
    '        If dtParamList.Item("KameitenCdFrom").ToString <> String.Empty And dtParamList.Item("KameitenCdTo").ToString <> String.Empty Then
    '            .AppendLine("   AND kameiten_cd BETWEEN @kameitenCdFrom AND @kameitenCdTo  ")
    '            paramList.Add(MakeParam("@KameitenCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("KameitenCdFrom").ToString))
    '            paramList.Add(MakeParam("@KameitenCdTo", SqlDbType.VarChar, 5, dtParamList.Item("KameitenCdTo").ToString))
    '        End If
    '        If dtParamList.Item("KameitenCdFrom").ToString <> String.Empty And dtParamList.Item("KameitenCdTo").ToString = String.Empty Then
    '            .AppendLine("   AND kameiten_cd = @kameitenCdFrom ")
    '            paramList.Add(MakeParam("@KameitenCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("KameitenCdFrom").ToString))
    '        End If
    '        If dtParamList.Item("KameitenCdFrom").ToString = String.Empty And dtParamList.Item("KameitenCdTo").ToString <> String.Empty Then
    '            .AppendLine("   AND kameiten_cd = @kameitenCdTo ")
    '            paramList.Add(MakeParam("@KameitenCdTo", SqlDbType.VarChar, 5, dtParamList.Item("KameitenCdTo").ToString))
    '        End If
    '        .AppendLine("  	   	)AS SKM ")  'サブ加盟店マスタ
    '        .AppendLine("  LEFT JOIN ")
    '        .AppendLine("  	    m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH(READCOMMITTED) ")
    '        .AppendLine("  ON  SMS.syouhin_cd = MKSTTT.syouhin_cd ")
    '        .AppendLine("  AND SMT.tys_houhou_no = MKSTTT.tys_houhou_no ")
    '        .AppendLine("  AND SMTT.tokubetu_taiou_cd = MKSTTT.tokubetu_taiou_cd ")
    '        .AppendLine("  AND SKM.kameiten_cd = MKSTTT.kameiten_cd ")
    '        .AppendLine(" ORDER BY ")
    '        .AppendLine(" 	 SKM.kameiten_cd ")
    '        .AppendLine(" 	,SMS.syouhin_cd ")
    '        .AppendLine(" 	,SMT.tys_houhou_no ")
    '    End With

    '    'SQLコメント実行、戻りデータセット実装
    '    FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "TokubetuTaiouCSV", paramList.ToArray)

    '    '戻りデータテーブル
    '    Return dsReturn.Tables(0)

    'End Function

    ''' <summary>未設定も含む加盟店商品調査方法特別対応マスタCSVデータを取得</summary>
    ''' <returns>未設定も含む加盟店商品調査方法特別対応マスタCSVテーブル</returns>
    Public Function SelTokubetuTaiouCSV(ByVal dtParamList As Dictionary(Of String, String)) As Data.DataTable
        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            'EDI情報作成日
            .AppendLine("   RIGHT(CONVERT(varchar(10),GETDATE(),112),4)+REPLACE(CONVERT(varchar(8),GETDATE(),108),':','') AS edi_jouhou_sakusei_date ")
            .AppendLine(" 	,ISNULL(SUB.aitesaki_syubetu,'') ")                             '相手先種別
            .AppendLine(" 	,ISNULL(SUB.aitesaki_cd,'') ")                                  '相手先コード
            .AppendLine(" 	,ISNULL(SUB.aitesaki_name,'') ")                                '相手先名称
            .AppendLine(" 	,ISNULL(SMS.syouhin_cd,'') ")                                   '商品コード
            .AppendLine(" 	,ISNULL(SMS.syouhin_mei,'') ")                                  '商品名
            .AppendLine(" 	,ISNULL(CONVERT(VARCHAR(10),SMT.tys_houhou_no),'') ")           '調査方法NO
            .AppendLine(" 	,ISNULL(SMT.tys_houhou_mei,'') ")                               '調査方法名
            .AppendLine(" 	,ISNULL(CONVERT(VARCHAR(10),SMTT.tokubetu_taiou_cd),'') ")      '特別対応コード
            .AppendLine(" 	,ISNULL(SMTT.tokubetu_taiou_meisyou,'') ")                      '特別対応名称
            .AppendLine(" 	,ISNULL(CONVERT(VARCHAR(10),MKSTTT.torikesi),'') ")             '取消
            .AppendLine("	,ISNULL(MKSTTT.kasan_syouhin_cd,'') ")                          '金額加算商品コード
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MKSTTT.syokiti),'') ")              '初期値
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MKSTTT.uri_kasan_gaku),'') ")       '実請求加算金額
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MKSTTT.koumuten_kasan_gaku),'') ")  '工務店請求加算金額
            .AppendLine(" FROM ")
            .AppendLine(" 	(SELECT  ")
            .AppendLine(" 		tys_houhou_no ")
            .AppendLine(" 		,tys_houhou_mei ")
            .AppendLine(" 	 FROM ")
            .AppendLine(" 	  	m_tyousahouhou WITH(READCOMMITTED) ")   '調査方法マスタ
            .AppendLine(" 	 WHERE ")
            .AppendLine(" 	  	torikesi = 0 ")
            If dtParamList.Item("tys_houhou_no").ToString <> String.Empty Then
                .AppendLine(" 	AND tys_houhou_no = @tys_houhou_no  ")
                paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 5, dtParamList.Item("tys_houhou_no")))
            End If
            .AppendLine(" 	  )AS SMT ")    'サブ調査方法マスタ
            .AppendLine(" CROSS JOIN ")
            .AppendLine("  	 (SELECT ")
            .AppendLine("  	  	 syouhin_cd ")
            .AppendLine("  	  	 ,syouhin_mei ")
            .AppendLine("  	  FROM ")
            .AppendLine("  	  	  m_syouhin WITH(READCOMMITTED) ")  '商品マスタ
            .AppendLine(" 	  WHERE ")
            .AppendLine("  	  	  souko_cd = '100' AND torikesi = 0  ")
            If dtParamList.Item("syouhin_cd").ToString <> String.Empty Then
                .AppendLine(" 	  AND syouhin_cd =@syouhin_cd  ")
                paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtParamList.Item("syouhin_cd").ToString))
            End If
            .AppendLine("  	  )AS SMS ")    'サブ商品マスタ
            .AppendLine(" CROSS JOIN ")
            .AppendLine(" 	  (SELECT ")
            .AppendLine("  	  	  tokubetu_taiou_cd ")
            .AppendLine("  	  	  ,tokubetu_taiou_meisyou ")
            .AppendLine("  	   FROM ")
            .AppendLine("  	   	  m_tokubetu_taiou WITH(READCOMMITTED) ")   '特別対応マスタ
            .AppendLine("  	   WHERE ")
            .AppendLine(" 	      torikesi = 0  ")
            If dtParamList.Item("tokubetu_taiou_cd").ToString <> String.Empty Then
                .AppendLine("  	  AND tokubetu_taiou_cd = @tokubetu_taiou_cd ")
                paramList.Add(MakeParam("@tokubetu_taiou_cd", SqlDbType.Int, 5, dtParamList.Item("tokubetu_taiou_cd")))
            End If
            .AppendLine("  	   )AS SMTT ")  'サブ特別対応マスタ
            .AppendLine(" CROSS JOIN ")
            If "0".Equals(dtParamList.Item("aitesakiSyubetu").ToString) Then
                .AppendLine("	(SELECT ")
                .AppendLine("	    '0' AS aitesaki_syubetu, ")
                .AppendLine("	    'ALL' AS aitesaki_cd, ")
                .AppendLine("	    '相手先なし' AS aitesaki_name ")
                .AppendLine("  	   )AS SUB ") 'サブ
            End If

            If "1".Equals(dtParamList.Item("aitesakiSyubetu").ToString) Then
                .AppendLine("	(SELECT ")
                .AppendLine("	    '1' AS aitesaki_syubetu, ")
                .AppendLine("	    MK.kameiten_cd AS aitesaki_cd, ")
                .AppendLine("	    MK.kameiten_mei1 AS aitesaki_name ")
                .AppendLine("	FROM ")
                .AppendLine("	    m_kameiten AS MK WITH (READCOMMITTED) ")
                .AppendLine("WHERE  (1=1) ")
                If Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                    .AppendLine("   AND MK.kameiten_cd BETWEEN @aitesakiCdFrom AND @aitesakiCdTo ")
                    paramList.Add(MakeParam("@aitesakiCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdFrom").ToString))
                    paramList.Add(MakeParam("@aitesakiCdTo", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdTo").ToString))
                End If

                If Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                    .AppendLine("   AND MK.kameiten_cd = @aitesakiCdFrom ")
                    paramList.Add(MakeParam("@aitesakiCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdFrom").ToString))
                End If

                If String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                    .AppendLine("   AND MK.kameiten_cd = @aitesakiCdTo ")
                    paramList.Add(MakeParam("@aitesakiCdTo", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdTo").ToString))
                End If
                If dtParamList.Item("torikesiFlg").ToString.Equals("1") Then
                    .AppendLine("   AND MK.torikesi = 0 ")
                End If
                .AppendLine("  	   )AS SUB ") 'サブ
            End If

            If "5".Equals(dtParamList.Item("aitesakiSyubetu").ToString) Then
                .AppendLine("	(SELECT ")
                .AppendLine("	    '5' AS aitesaki_syubetu, ")
                .AppendLine("	    ME.eigyousyo_cd AS aitesaki_cd, ")
                .AppendLine("	    ME.eigyousyo_mei AS aitesaki_name ")
                .AppendLine("	FROM ")
                .AppendLine("	    m_eigyousyo AS ME WITH (READCOMMITTED) ")
                .AppendLine("WHERE  (1=1) ")
                If Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                    .AppendLine("   AND ME.eigyousyo_cd BETWEEN @aitesakiCdFrom AND @aitesakiCdTo ")
                    paramList.Add(MakeParam("@aitesakiCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdFrom").ToString))
                    paramList.Add(MakeParam("@aitesakiCdTo", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdTo").ToString))
                End If

                If Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                    .AppendLine("   AND ME.eigyousyo_cd = @aitesakiCdFrom ")
                    paramList.Add(MakeParam("@aitesakiCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdFrom").ToString))
                End If

                If String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                    .AppendLine("   AND ME.eigyousyo_cd = @aitesakiCdTo ")
                    paramList.Add(MakeParam("@aitesakiCdTo", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdTo").ToString))
                End If
                If dtParamList.Item("torikesiFlg").ToString.Equals("1") Then
                    .AppendLine("   AND ME.torikesi = 0 ")
                End If
                .AppendLine("  	   )AS SUB ") 'サブ
            End If

            If "7".Equals(dtParamList.Item("aitesakiSyubetu").ToString) Then
                .AppendLine("	(SELECT ")
                .AppendLine("	    '7' AS aitesaki_syubetu, ")
                .AppendLine("	    MKR.keiretu_cd AS aitesaki_cd, ")
                .AppendLine("	    MIN(MKR.keiretu_mei) AS aitesaki_name ")
                .AppendLine("	FROM ")
                .AppendLine("	    m_keiretu AS MKR WITH (READCOMMITTED) ")
                .AppendLine("WHERE  (1=1) ")
                If Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                    .AppendLine("   AND MKR.keiretu_cd BETWEEN @aitesakiCdFrom AND @aitesakiCdTo ")
                    paramList.Add(MakeParam("@aitesakiCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdFrom").ToString))
                    paramList.Add(MakeParam("@aitesakiCdTo", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdTo").ToString))
                End If

                If Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                    .AppendLine("   AND MKR.keiretu_cd = @aitesakiCdFrom ")
                    paramList.Add(MakeParam("@aitesakiCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdFrom").ToString))
                End If

                If String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                    .AppendLine("   AND MKR.keiretu_cd = @aitesakiCdTo ")
                    paramList.Add(MakeParam("@aitesakiCdTo", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdTo").ToString))
                End If
                If dtParamList.Item("torikesiFlg").ToString.Equals("1") Then
                    .AppendLine("   AND MKR.torikesi = 0 ")
                End If
                .AppendLine("	GROUP BY MKR.keiretu_cd ")
                .AppendLine("  	   )AS SUB ") 'サブ
            End If

            .AppendLine("  LEFT JOIN ")
            .AppendLine("  	    m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH(READCOMMITTED) ")
            .AppendLine("  ON  SMS.syouhin_cd = MKSTTT.syouhin_cd ")
            .AppendLine("  AND SMT.tys_houhou_no = MKSTTT.tys_houhou_no ")
            .AppendLine("  AND SMTT.tokubetu_taiou_cd = MKSTTT.tokubetu_taiou_cd ")
            .AppendLine("  AND SUB.aitesaki_syubetu = MKSTTT.aitesaki_syubetu ")
            .AppendLine("  AND SUB.aitesaki_cd = MKSTTT.aitesaki_cd ")
            .AppendLine(" ORDER BY ")
            .AppendLine(" 	 MKSTTT.aitesaki_syubetu ")
            .AppendLine(" 	,MKSTTT.aitesaki_cd ")
            .AppendLine(" 	,SMS.syouhin_cd ")
            .AppendLine(" 	,SMT.tys_houhou_no ")
            .AppendLine(" 	,MKSTTT.tokubetu_taiou_cd ")
        End With

        'SQLコメント実行、戻りデータセット実装
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "TokubetuTaiouCSV", paramList.ToArray)

        '戻りデータテーブル
        Return dsReturn.Tables(0)

    End Function

    '''' <summary>未設定も含む加盟店商品調査方法特別対応マスタCSVデータ件数を取得</summary>
    '''' <returns>未設定も含む加盟店商品調査方法特別対応マスタCSVデータ件数</returns>
    'Public Function SelTokubetuTaiouCSVCount(ByVal dtParamList As Dictionary(Of String, String)) As Long

    '    '戻りデータセット
    '    Dim dsReturn As New Data.DataSet

    '    'SQLコメント
    '    Dim commandTextSb As New StringBuilder

    '    'パラメータ格納
    '    Dim paramList As New List(Of SqlClient.SqlParameter)

    '    'SQL文
    '    With commandTextSb
    '        .AppendLine("SELECT ")
    '        .AppendLine("   COUNT_BIG(*) AS CSVCount")
    '        .AppendLine("FROM")
    '        .AppendLine(" 	(SELECT  ")
    '        .AppendLine(" 		tys_houhou_no ")    '調査方法NO
    '        .AppendLine(" 		,tys_houhou_mei ")  '調査方法名称
    '        .AppendLine(" 	 FROM ")
    '        .AppendLine(" 	  	m_tyousahouhou WITH(READCOMMITTED) ")   '調査方法マスタ
    '        .AppendLine(" 	 WHERE ")
    '        .AppendLine(" 	  	ISNULL (genka_settei_fuyou_flg,0) = 0 ")
    '        If dtParamList.Item("tys_houhou_no").ToString <> String.Empty Then
    '            .AppendLine(" 	AND tys_houhou_no = @tys_houhou_no  ")
    '            paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 5, dtParamList.Item("tys_houhou_no")))
    '        End If
    '        .AppendLine(" 	  )AS SMT ")    'サブ調査方法マスタ
    '        .AppendLine(" CROSS JOIN ")
    '        .AppendLine("  	 (SELECT ")
    '        .AppendLine("  	  	 syouhin_cd ")  '-商品コード
    '        .AppendLine("  	  	 ,syouhin_mei ")    '商品名
    '        .AppendLine("  	  FROM ")
    '        .AppendLine("  	  	  m_syouhin WITH(READCOMMITTED) ")  '商品マスタ
    '        .AppendLine(" 	  WHERE ")
    '        .AppendLine("  	  	  souko_cd = '100' AND torikesi = 0  ")
    '        If dtParamList.Item("syouhin_cd").ToString <> String.Empty Then
    '            .AppendLine(" 	  AND syouhin_cd =@syouhin_cd  ")
    '            paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtParamList.Item("syouhin_cd").ToString))
    '        End If
    '        .AppendLine("  	  )AS SMS ")    'サブ商品マスタ
    '        .AppendLine(" CROSS JOIN ")
    '        .AppendLine(" 	  (SELECT ")
    '        .AppendLine("  	  	  tokubetu_taiou_cd ")  '特別対応コード
    '        .AppendLine("  	  	  ,tokubetu_taiou_meisyou ")    '-特別対応名称
    '        .AppendLine("  	   FROM ")
    '        .AppendLine("  	   	  m_tokubetu_taiou WITH(READCOMMITTED) ")   '特別対応マスタ
    '        .AppendLine("  	   WHERE ")
    '        .AppendLine(" 	      torikesi = 0  ")
    '        If dtParamList.Item("tokubetu_taiou_cd").ToString <> String.Empty Then
    '            .AppendLine("  	  AND tokubetu_taiou_cd = @tokubetu_taiou_cd ")
    '            paramList.Add(MakeParam("@tokubetu_taiou_cd", SqlDbType.Int, 5, dtParamList.Item("tokubetu_taiou_cd")))
    '        End If
    '        .AppendLine("  	   )AS SMTT ")  'サブ特別対応マスタ
    '        .AppendLine(" CROSS JOIN ")
    '        .AppendLine("  	   (SELECT ")
    '        .AppendLine("  	       kameiten_cd ")   '加盟店コード
    '        .AppendLine("  	       ,kameiten_mei1 ")    '加盟店名称1
    '        .AppendLine("  	    FROM ")
    '        .AppendLine(" 	       m_kameiten WITH(READCOMMITTED) ")    '加盟店マスタ
    '        .AppendLine("   WHERE(1=1) ")
    '        If dtParamList.Item("KameitenCdFrom").ToString <> String.Empty And dtParamList.Item("KameitenCdTo").ToString <> String.Empty Then
    '            .AppendLine("   AND kameiten_cd BETWEEN @kameitenCdFrom AND @kameitenCdTo  ")
    '            paramList.Add(MakeParam("@KameitenCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("KameitenCdFrom").ToString))
    '            paramList.Add(MakeParam("@KameitenCdTo", SqlDbType.VarChar, 5, dtParamList.Item("KameitenCdTo").ToString))
    '        End If
    '        If dtParamList.Item("KameitenCdFrom").ToString <> String.Empty And dtParamList.Item("KameitenCdTo").ToString = String.Empty Then
    '            .AppendLine("   AND kameiten_cd = @kameitenCdFrom ")
    '            paramList.Add(MakeParam("@KameitenCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("KameitenCdFrom").ToString))
    '        End If
    '        If dtParamList.Item("KameitenCdFrom").ToString = String.Empty And dtParamList.Item("KameitenCdTo").ToString <> String.Empty Then
    '            .AppendLine("   AND kameiten_cd = @kameitenCdTo ")
    '            paramList.Add(MakeParam("@KameitenCdTo", SqlDbType.VarChar, 5, dtParamList.Item("KameitenCdTo").ToString))
    '        End If
    '        .AppendLine("  	   	)AS SKM ")  'サブ加盟店マスタ
    '        .AppendLine("  LEFT JOIN ")
    '        .AppendLine("  	    m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH(READCOMMITTED) ")
    '        .AppendLine("  ON  SMS.syouhin_cd = MKSTTT.syouhin_cd ")
    '        .AppendLine("  AND SMT.tys_houhou_no = MKSTTT.tys_houhou_no ")
    '        .AppendLine("  AND SMTT.tokubetu_taiou_cd = MKSTTT.tokubetu_taiou_cd ")
    '        .AppendLine("  AND SKM.kameiten_cd = MKSTTT.kameiten_cd ")
    '    End With

    '    'SQLコメント実行、戻りデータセット実装
    '    FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "TokubetuTaiouCSV", paramList.ToArray)

    '    '戻りデータテーブル
    '    Return dsReturn.Tables(0).Rows(0).Item(0)
    'End Function

    ''' <summary>未設定も含む加盟店商品調査方法特別対応マスタCSVデータ件数を取得</summary>
    ''' <returns>未設定も含む加盟店商品調査方法特別対応マスタCSVデータ件数</returns>
    Public Function SelTokubetuTaiouCSVCount(ByVal dtParamList As Dictionary(Of String, String)) As Long

        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   COUNT_BIG(*) AS CSVCount")
            .AppendLine(" FROM ")
            .AppendLine(" 	(SELECT  ")
            .AppendLine(" 		tys_houhou_no ")
            .AppendLine(" 		,tys_houhou_mei ")
            .AppendLine(" 	 FROM ")
            .AppendLine(" 	  	m_tyousahouhou WITH(READCOMMITTED) ")   '調査方法マスタ
            .AppendLine(" 	 WHERE ")
            .AppendLine(" 	  	torikesi = 0 ")
            If dtParamList.Item("tys_houhou_no").ToString <> String.Empty Then
                .AppendLine(" 	AND tys_houhou_no = @tys_houhou_no  ")
                paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 5, dtParamList.Item("tys_houhou_no")))
            End If
            .AppendLine(" 	  )AS SMT ")    'サブ調査方法マスタ
            .AppendLine(" CROSS JOIN ")
            .AppendLine("  	 (SELECT ")
            .AppendLine("  	  	 syouhin_cd ")
            .AppendLine("  	  	 ,syouhin_mei ")
            .AppendLine("  	  FROM ")
            .AppendLine("  	  	  m_syouhin WITH(READCOMMITTED) ")  '商品マスタ
            .AppendLine(" 	  WHERE ")
            .AppendLine("  	  	  souko_cd = '100' AND torikesi = 0  ")
            If dtParamList.Item("syouhin_cd").ToString <> String.Empty Then
                .AppendLine(" 	  AND syouhin_cd =@syouhin_cd  ")
                paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtParamList.Item("syouhin_cd").ToString))
            End If
            .AppendLine("  	  )AS SMS ")    'サブ商品マスタ
            .AppendLine(" CROSS JOIN ")
            .AppendLine(" 	  (SELECT ")
            .AppendLine("  	  	  tokubetu_taiou_cd ")
            .AppendLine("  	  	  ,tokubetu_taiou_meisyou ")
            .AppendLine("  	   FROM ")
            .AppendLine("  	   	  m_tokubetu_taiou WITH(READCOMMITTED) ")   '特別対応マスタ
            .AppendLine("  	   WHERE ")
            .AppendLine(" 	      torikesi = 0  ")
            If dtParamList.Item("tokubetu_taiou_cd").ToString <> String.Empty Then
                .AppendLine("  	  AND tokubetu_taiou_cd = @tokubetu_taiou_cd ")
                paramList.Add(MakeParam("@tokubetu_taiou_cd", SqlDbType.Int, 5, dtParamList.Item("tokubetu_taiou_cd")))
            End If
            .AppendLine("  	   )AS SMTT ")  'サブ特別対応マスタ
            .AppendLine(" CROSS JOIN ")
            If "0".Equals(dtParamList.Item("aitesakiSyubetu").ToString) Then
                .AppendLine("	(SELECT ")
                .AppendLine("	    '0' AS aitesaki_syubetu, ")
                .AppendLine("	    'ALL' AS aitesaki_cd, ")
                .AppendLine("	    '相手先なし' AS aitesaki_name ")
                .AppendLine("  	   )AS SUB ") 'サブ
            End If

            If "1".Equals(dtParamList.Item("aitesakiSyubetu").ToString) Then
                .AppendLine("	(SELECT ")
                .AppendLine("	    '1' AS aitesaki_syubetu, ")
                .AppendLine("	    MK.kameiten_cd AS aitesaki_cd, ")
                .AppendLine("	    MK.kameiten_mei1 AS aitesaki_name ")
                .AppendLine("	FROM ")
                .AppendLine("	    m_kameiten AS MK WITH (READCOMMITTED) ")
                .AppendLine("WHERE  (1=1) ")
                If Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                    .AppendLine("   AND MK.kameiten_cd BETWEEN @aitesakiCdFrom AND @aitesakiCdTo ")
                    paramList.Add(MakeParam("@aitesakiCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdFrom").ToString))
                    paramList.Add(MakeParam("@aitesakiCdTo", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdTo").ToString))
                End If

                If Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                    .AppendLine("   AND MK.kameiten_cd = @aitesakiCdFrom ")
                    paramList.Add(MakeParam("@aitesakiCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdFrom").ToString))
                End If

                If String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                    .AppendLine("   AND MK.kameiten_cd = @aitesakiCdTo ")
                    paramList.Add(MakeParam("@aitesakiCdTo", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdTo").ToString))
                End If
                If dtParamList.Item("torikesiFlg").ToString.Equals("1") Then
                    .AppendLine("   AND MK.torikesi = 0 ")
                End If
                .AppendLine("  	   )AS SUB ") 'サブ
            End If

            If "5".Equals(dtParamList.Item("aitesakiSyubetu").ToString) Then
                .AppendLine("	(SELECT ")
                .AppendLine("	    '5' AS aitesaki_syubetu, ")
                .AppendLine("	    ME.eigyousyo_cd AS aitesaki_cd, ")
                .AppendLine("	    ME.eigyousyo_mei AS aitesaki_name ")
                .AppendLine("	FROM ")
                .AppendLine("	    m_eigyousyo AS ME WITH (READCOMMITTED) ")
                .AppendLine("WHERE  (1=1) ")
                If Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                    .AppendLine("   AND ME.eigyousyo_cd BETWEEN @aitesakiCdFrom AND @aitesakiCdTo ")
                    paramList.Add(MakeParam("@aitesakiCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdFrom").ToString))
                    paramList.Add(MakeParam("@aitesakiCdTo", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdTo").ToString))
                End If

                If Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                    .AppendLine("   AND ME.eigyousyo_cd = @aitesakiCdFrom ")
                    paramList.Add(MakeParam("@aitesakiCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdFrom").ToString))
                End If

                If String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                    .AppendLine("   AND ME.eigyousyo_cd = @aitesakiCdTo ")
                    paramList.Add(MakeParam("@aitesakiCdTo", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdTo").ToString))
                End If
                If dtParamList.Item("torikesiFlg").ToString.Equals("1") Then
                    .AppendLine("   AND ME.torikesi = 0 ")
                End If
                .AppendLine("  	   )AS SUB ") 'サブ
            End If

            If "7".Equals(dtParamList.Item("aitesakiSyubetu").ToString) Then
                .AppendLine("	(SELECT ")
                .AppendLine("	    '7' AS aitesaki_syubetu, ")
                .AppendLine("	    MKR.keiretu_cd AS aitesaki_cd, ")
                .AppendLine("	    MIN(MKR.keiretu_mei) AS aitesaki_name ")
                .AppendLine("	FROM ")
                .AppendLine("	    m_keiretu AS MKR WITH (READCOMMITTED) ")
                .AppendLine("WHERE  (1=1) ")
                If Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                    .AppendLine("   AND MKR.keiretu_cd BETWEEN @aitesakiCdFrom AND @aitesakiCdTo ")
                    paramList.Add(MakeParam("@aitesakiCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdFrom").ToString))
                    paramList.Add(MakeParam("@aitesakiCdTo", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdTo").ToString))
                End If

                If Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                    .AppendLine("   AND MKR.keiretu_cd = @aitesakiCdFrom ")
                    paramList.Add(MakeParam("@aitesakiCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdFrom").ToString))
                End If

                If String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                    .AppendLine("   AND MKR.keiretu_cd = @aitesakiCdTo ")
                    paramList.Add(MakeParam("@aitesakiCdTo", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdTo").ToString))
                End If
                If dtParamList.Item("torikesiFlg").ToString.Equals("1") Then
                    .AppendLine("   AND MKR.torikesi = 0 ")
                End If
                .AppendLine("	GROUP BY MKR.keiretu_cd ")
                .AppendLine("  	   )AS SUB ") 'サブ
            End If

            .AppendLine("  LEFT JOIN ")
            .AppendLine("  	    m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH(READCOMMITTED) ")
            .AppendLine("  ON  SMS.syouhin_cd = MKSTTT.syouhin_cd ")
            .AppendLine("  AND SMT.tys_houhou_no = MKSTTT.tys_houhou_no ")
            .AppendLine("  AND SMTT.tokubetu_taiou_cd = MKSTTT.tokubetu_taiou_cd ")
            .AppendLine("  AND SUB.aitesaki_syubetu = MKSTTT.aitesaki_syubetu ")
            .AppendLine("  AND SUB.aitesaki_cd = MKSTTT.aitesaki_cd ")

        End With

        'SQLコメント実行、戻りデータセット実装
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "TokubetuTaiouCSV", paramList.ToArray)

        '戻りデータテーブル
        Return dsReturn.Tables(0).Rows(0).Item(0)
    End Function

    ''' <summary>アップロード管理を取得する</summary>
    ''' <returns>アップロード管理データテーブル</returns>
    Public Function SelInputKanri() As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'SQL文
        With commandTextSb
            .AppendLine(" SELECT TOP 100 ")
            .AppendLine("	CONVERT(varchar(100),syori_datetime,111) + ' ' + CONVERT(varchar(100),syori_datetime,108) AS syori_datetime ") '処理日時
            .AppendLine("	,CONVERT(varchar(100),syori_datetime,112) + REPLACE(CONVERT(varchar(100),syori_datetime,114),':','') AS torikomi_datetime") '取込日時
            .AppendLine("	,nyuuryoku_file_mei ")      '入力ファイル名
            .AppendLine("	,CASE error_umu ")
            .AppendLine("    WHEN '1' THEN 'あり' ")
            .AppendLine("    WHEN '0' THEN 'なし' ")
            .AppendLine("    ELSE '' ")
            .AppendLine("    END AS error_umu ")            'エラー有無
            .AppendLine("    ,edi_jouhou_sakusei_date ")    'EDI情報作成日
            .AppendLine(" FROM ")
            .AppendLine("	t_upload_kanri WITH(READCOMMITTED) ")  'アップロード管理テーブル
            .AppendLine(" WHERE ")
            .AppendLine("	file_kbn = 4 ")         'ファイル区分(4：加盟店商品調査方法特別対応M用)
            .AppendLine(" ORDER BY ")
            .AppendLine("	syori_datetime DESC ")  '処理日時(降順)
        End With

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtInputKanri")

        Return dsReturn.Tables(0)

    End Function

    ''' <summary>アップロード管理の件数を取得する</summary>
    ''' <returns>アップロード管理の件数</returns>
    Public Function SelInputKanriCount() As Integer

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	COUNT(*) ")    '件数
            .AppendLine("FROM ")
            .AppendLine("	t_upload_kanri WITH(READCOMMITTED) ")           'アップロード管理テーブル
            .AppendLine("WHERE ")
            .AppendLine("	file_kbn = 4 ")             'ファイル区分
        End With

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtInputKanri")

        Return dsReturn.Tables(0).Rows(0).Item(0).ToString

    End Function

    '''' <summary>加盟店コードを取得する</summary>
    'Public Function SelKameitenCd(ByVal strKameitenCd As String) As Boolean

    '    'DataSetインスタンスの生成
    '    Dim dsReturn As New Data.DataSet

    '    'SQL文の生成
    '    Dim commandTextSb As New StringBuilder

    '    'パラメータ格納
    '    Dim paramList As New List(Of SqlClient.SqlParameter)

    '    'SQL文
    '    With commandTextSb
    '        .AppendLine("SELECT DISTINCT ")
    '        .AppendLine("   kameiten_cd ")
    '        .AppendLine("FROM ")
    '        .AppendLine("   m_kameiten WITH(READCOMMITTED) ")
    '        .AppendLine("WHERE ")
    '        .AppendLine("   kameiten_cd = @kameiten_cd")
    '    End With

    '    'パラメータ作成
    '    paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))

    '    ' 検索実行
    '    FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtSyouhinCd", paramList.ToArray)

    '    '戻り値
    '    If dsReturn.Tables(0).Rows.Count > 0 Then
    '        Return True
    '    Else
    '        Return False
    '    End If

    'End Function

    ''' <summary>相手先種別を取得する</summary>
    ''' <history>2012/02/07　陳琳(大連情報システム部)　新規作成</history>
    Public Function SelAitesakiSyubetuInput(ByVal intAitesakiSyubetu As Integer, ByVal AitesakiCd As String) As Boolean

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	aitesaki_syubetu ") '--相手先種別
            .AppendLine("	,aitesaki_cd ") '--相手先コード
            .AppendLine("FROM ")
            .AppendLine("	( ")
            .AppendLine("		SELECT ")
            .AppendLine("			0				AS aitesaki_syubetu ")  '--相手先種別
            .AppendLine("			,'ALL'			AS aitesaki_cd ")   '--相手先コード
            .AppendLine("			,'相手先なし'	AS aitesaki_mei ")  '--相手先名
            .AppendLine("			,0				AS torikesi ")  '--取消
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			1					AS aitesaki_syubetu ")
            .AppendLine("			,MKA.kameiten_cd	AS aitesaki_cd ")   '--加盟店コード
            .AppendLine("			,MKA.kameiten_mei1	AS aitesaki_mei ")  '--加盟店名1
            .AppendLine("			,MKA.torikesi		AS torikesi ")
            .AppendLine("		FROM ")
            .AppendLine("			m_kameiten AS MKA WITH(READCOMMITTED) ")    '--加盟店マスタ
            .AppendLine("	    UNION ALL ")
            .AppendLine("	    SELECT ")
            .AppendLine("	        '5' AS aitesaki_syubetu, ")
            .AppendLine("	        ME.eigyousyo_cd AS aitesaki_cd, ")
            .AppendLine("	        ME.eigyousyo_mei AS aitesaki_name, ")
            .AppendLine("	        ME.torikesi AS torikesi ")
            .AppendLine("	    FROM ")
            .AppendLine("	        m_eigyousyo AS ME WITH (READCOMMITTED) ")   '--営業所マスタ
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			7						AS aitesaki_syubetu ")
            .AppendLine("			,MKE.keiretu_cd			AS aitesaki_cd ")   '--系列コード
            .AppendLine("			,MIN(MKE.keiretu_mei)	AS aitesaki_mei ")  '--系列名
            .AppendLine("			,MIN(MKE.torikesi)		AS torikesi ")
            .AppendLine("		FROM ")
            .AppendLine("			m_keiretu AS MKE WITH(READCOMMITTED) ") '--系列マスタ
            .AppendLine("		GROUP BY ")
            .AppendLine("			keiretu_cd ")   '--系列コード
            .AppendLine("	) AS TA ")
            .AppendLine("WHERE ")
            .AppendLine("	aitesaki_syubetu = @aitesaki_syubetu ")
            .AppendLine("	AND ")
            .AppendLine("	aitesaki_cd = @aitesaki_cd ")
        End With

        paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 8, intAitesakiSyubetu))
        paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, AitesakiCd))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtAitesakiSyubetu", paramList.ToArray)

        '戻り値
        If dsReturn.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>商品コードを取得する</summary>
    Public Function SelSyouhinCd(ByVal strSyouhinCd As String) As Boolean

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT DISTINCT ")
            .AppendLine("	syouhin_cd ")
            .AppendLine("FROM  ")
            .AppendLine("	m_syouhin WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	syouhin_cd = @syouhin_cd ")
        End With

        'パラメータ作成
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtSyouhinCd", paramList.ToArray)

        '戻り値
        If dsReturn.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>調査方法NOを取得する</summary>
    Public Function SelTyousahouhouNo(ByVal intTyousahouhouNo As Integer) As Boolean

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT DISTINCT    ")
            .AppendLine("	tys_houhou_no ")
            .AppendLine("FROM  ")
            .AppendLine("	m_tyousahouhou WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	tys_houhou_no = @TyousahouhouNo ")
        End With

        'パラメータ作成
        paramList.Add(MakeParam("@TyousahouhouNo", SqlDbType.Int, 8, intTyousahouhouNo))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtTyousahouhouNo", paramList.ToArray)

        '戻り値
        If dsReturn.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>特別対応コードを取得する</summary>
    Public Function SelTokubetuCd(ByVal intTokubetuCd As Integer) As Boolean
        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT DISTINCT    ")
            .AppendLine("   tokubetu_taiou_cd ")
            .AppendLine("FROM   ")
            .AppendLine("   m_tokubetu_taiou WITH(READCOMMITTED)    ")
            .AppendLine("WHERE  ")
            .AppendLine("   tokubetu_taiou_cd = @tokubetu_taiou_cd  ")
        End With

        'パラメータ作成
        paramList.Add(MakeParam("@tokubetu_taiou_cd", SqlDbType.Int, 10, intTokubetuCd))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "dtTokubetuTaiou", paramList.ToArray)

        '戻り値
        If dsReturn.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    '''' <summary>加盟店商品調査方法特別対応データを取得</summary>
    'Public Function SelTokubetuTaiouJyouhou(ByVal strKameitenCd As String, ByVal strSyouhinCd As String, ByVal strTyshouhouNo As String, ByVal strTokubetuCd As String) As Boolean
    '    'DataSetインスタンスの生成
    '    Dim dsReturn As New Data.DataSet

    '    'SQL文の生成
    '    Dim commandTextSb As New StringBuilder

    '    'パラメータ格納
    '    Dim paramList As New List(Of SqlClient.SqlParameter)

    '    With commandTextSb
    '        .AppendLine(" SELECT DISTINCT ")
    '        .AppendLine("   kameiten_cd ")
    '        .AppendLine(" FROM ")
    '        .AppendLine("   m_kamei_syouhin_tys_tokubetu_taiou WITH(READCOMMITTED) ")
    '        .AppendLine(" WHERE ")
    '        .AppendLine("   kameiten_cd = @kameiten_cd ")
    '        .AppendLine(" AND syouhin_cd = @syouhin_cd ")
    '        .AppendLine(" AND tys_houhou_no = @tys_houhou_no ")
    '        .AppendLine(" AND tokubetu_taiou_cd = @tokubetu_taiou_cd ")
    '    End With

    '    'パラメータ作成
    '    paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
    '    paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))
    '    paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 5, strTyshouhouNo))
    '    paramList.Add(MakeParam("@tokubetu_taiou_cd", SqlDbType.Int, 5, strTokubetuCd))

    '    ' 検索実行
    '    FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "TokubetuTaiouJyouhou", paramList.ToArray)

    '    '戻り値
    '    If dsReturn.Tables(0).Rows.Count > 0 Then
    '        Return True
    '    Else
    '        Return False
    '    End If

    'End Function

    ''' <summary>加盟店商品調査方法特別対応データを取得</summary>
    Public Function SelTokubetuTaiouJyouhou(ByVal strAitesakiSyubetu As String, ByVal strAitesakiCd As String, ByVal strSyouhinCd As String, ByVal strTyshouhouNo As String, ByVal strTokubetuCd As String) As Boolean
        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine(" SELECT DISTINCT ")
            .AppendLine("   aitesaki_cd ")
            .AppendLine(" FROM ")
            .AppendLine("   m_kamei_syouhin_tys_tokubetu_taiou WITH(READCOMMITTED) ")
            .AppendLine(" WHERE ")
            .AppendLine("   aitesaki_syubetu = @aitesaki_syubetu ")
            .AppendLine(" AND aitesaki_cd = @aitesaki_cd ")
            .AppendLine(" AND syouhin_cd = @syouhin_cd ")
            .AppendLine(" AND tys_houhou_no = @tys_houhou_no ")
            .AppendLine(" AND tokubetu_taiou_cd = @tokubetu_taiou_cd ")
        End With

        'パラメータ作成
        paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 5, strAitesakiSyubetu))
        paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, strAitesakiCd))
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))
        paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 5, strTyshouhouNo))
        paramList.Add(MakeParam("@tokubetu_taiou_cd", SqlDbType.Int, 5, strTokubetuCd))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "TokubetuTaiouJyouhou", paramList.ToArray)

        '戻り値
        If dsReturn.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>エラー情報テーブルにエラー情報データを追加</summary>
    Public Function InsTokubetuTaiouError(ByVal dtError As Data.DataTable) As Boolean

        '戻り値
        Dim InsCount As Integer = 0

        'SQLコメント
        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("INSERT INTO ")
            .AppendLine("	m_kamei_syouhin_tys_tokubetu_taiou_error WITH(UPDLOCK) ")
            .AppendLine("	( ")
            .AppendLine("		edi_jouhou_sakusei_date ")
            .AppendLine("		,gyou_no ")
            .AppendLine("		,syori_datetime ")
            .AppendLine("		,aitesaki_syubetu ")
            .AppendLine("		,aitesaki_cd ")
            .AppendLine("		,aitesaki_mei ")
            .AppendLine("		,syouhin_cd ")
            .AppendLine("		,syouhin_mei ")
            .AppendLine("		,tys_houhou_no ")
            .AppendLine("		,tys_houhou ")
            .AppendLine("		,tokubetu_taiou_cd ")
            .AppendLine("		,tokubetu_taiou_meisyou ")
            .AppendLine("		,torikesi ")
            .AppendLine("		,kasan_syouhin_cd ")
            .AppendLine("		,kasan_syouhin_mei ")
            .AppendLine("		,syokiti ")
            .AppendLine("		,uri_kasan_gaku ")
            .AppendLine("		,koumuten_kasan_gaku ")
            .AppendLine("		,add_login_user_id ")
            .AppendLine("		,add_datetime ")
            .AppendLine("		,upd_login_user_id ")
            .AppendLine("		,upd_datetime ")
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("	( ")
            .AppendLine("		@edi_jouhou_sakusei_date ")
            .AppendLine("		,@gyou_no ")
            .AppendLine("		,@syori_datetime ")
            .AppendLine("		,@aitesaki_syubetu ")
            .AppendLine("		,@aitesaki_cd ")
            .AppendLine("		,@aitesaki_mei ")
            .AppendLine("		,@syouhin_cd ")
            .AppendLine("		,@syouhin_mei ")
            .AppendLine("		,@tys_houhou_no ")
            .AppendLine("		,@tys_houhou ")
            .AppendLine("		,@tokubetu_taiou_cd ")
            .AppendLine("		,@tokubetu_taiou_meisyou ")
            .AppendLine("		,@torikesi ")
            .AppendLine("		,@kasan_syouhin_cd ")
            .AppendLine("		,@kasan_syouhin_mei ")
            .AppendLine("		,@syokiti ")
            .AppendLine("		,@uri_kasan_gaku ")
            .AppendLine("		,@koumuten_kasan_gaku ")
            .AppendLine("		,@add_login_user_id ")
            .AppendLine("		,GETDATE() ")
            .AppendLine("		,NULL ")
            .AppendLine("		,NULL ")
            .AppendLine("	) ")
        End With

        For i As Integer = 0 To dtError.Rows.Count - 1
            'パラメータの設定
            paramList.Clear()
            paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, InsObj(dtError.Rows(i).Item(0).ToString.Trim)))
            paramList.Add(MakeParam("@gyou_no", SqlDbType.Int, 5, InsObj(dtError.Rows(i).Item(16).ToString.Trim)))
            paramList.Add(MakeParam("@syori_datetime", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(dtError.Rows(i).Item(17).ToString.Trim))))
            paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.VarChar, 1, InsObj(dtError.Rows(i).Item(1).ToString.Trim)))
            paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, InsObj(dtError.Rows(i).Item(2).ToString.Trim)))
            paramList.Add(MakeParam("@aitesaki_mei", SqlDbType.VarChar, 40, InsObj(dtError.Rows(i).Item(3).ToString.Trim)))
            paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, InsObj(dtError.Rows(i).Item(4).ToString.Trim)))
            paramList.Add(MakeParam("@syouhin_mei", SqlDbType.VarChar, 40, InsObj(dtError.Rows(i).Item(5).ToString.Trim)))
            paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.VarChar, 5, InsObj(dtError.Rows(i).Item(6).ToString.Trim)))
            paramList.Add(MakeParam("@tys_houhou", SqlDbType.VarChar, 32, InsObj(dtError.Rows(i).Item(7).ToString.Trim)))
            paramList.Add(MakeParam("@tokubetu_taiou_cd", SqlDbType.VarChar, 10, InsObj(dtError.Rows(i).Item(8).ToString.Trim)))
            paramList.Add(MakeParam("@tokubetu_taiou_meisyou", SqlDbType.VarChar, 40, InsObj(dtError.Rows(i).Item(9).ToString.Trim)))
            paramList.Add(MakeParam("@torikesi", SqlDbType.VarChar, 1, InsObj(dtError.Rows(i).Item(10).ToString.Trim)))
            paramList.Add(MakeParam("@kasan_syouhin_cd", SqlDbType.VarChar, 8, InsObj(dtError.Rows(i).Item(11).ToString.Trim)))
            paramList.Add(MakeParam("@kasan_syouhin_mei", SqlDbType.VarChar, 40, GetSyouhinMei(dtError.Rows(i).Item(11).ToString.Trim)))
            paramList.Add(MakeParam("@syokiti", SqlDbType.VarChar, 1, InsObj(dtError.Rows(i).Item(12).ToString.Trim)))
            paramList.Add(MakeParam("@uri_kasan_gaku", SqlDbType.VarChar, 10, InsObj(dtError.Rows(i).Item(13).ToString.Trim)))
            paramList.Add(MakeParam("@koumuten_kasan_gaku", SqlDbType.VarChar, 10, InsObj(dtError.Rows(i).Item(14).ToString.Trim)))
            paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, InsObj(dtError.Rows(i).Item(18))))

            '更新されたデータセットを DB へ書き込み
            Try
                InsCount = ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)
            Catch ex As Exception
                Return False
            End Try

            If Not (InsCount > 0) Then
                Return False
            End If
        Next

        Return True
    End Function

    Private Function GetSyouhinMei(ByVal strSyouhinCd As String) As String
        Dim strSyouhinMei As String

        Dim strSql As String = "SELECT syouhin_mei FROM m_syouhin WITH(READCOMMITTED) WHERE syouhin_cd = '" & strSyouhinCd & "'"

        strSyouhinMei = ExecuteScalar(connStr, CommandType.Text, strSql)

        If String.IsNullOrEmpty(strSyouhinMei) Then
            strSyouhinMei = String.Empty
        End If

        Return strSyouhinMei
    End Function

    '''' <summary>加盟店商品調査方法特別対応マスタにデータを追加と更新</summary>
    'Public Function InsUpdTokubetuTaiou(ByVal dtOk As Data.DataTable) As Boolean
    '    '戻り値
    '    Dim InsUpdCount As Integer = 0
    '    '追加用sql文
    '    Dim strSqlIns As New System.Text.StringBuilder
    '    '更新用sql文
    '    Dim strSqlUpd As New System.Text.StringBuilder

    '    'パラメータ格納
    '    Dim paramList As New List(Of SqlClient.SqlParameter)

    '    '追加用SQL文
    '    With strSqlIns
    '        .AppendLine("INSERT INTO ")
    '        .AppendLine("	m_kamei_syouhin_tys_tokubetu_taiou WITH(UPDLOCK) ")
    '        .AppendLine("	( ")
    '        .AppendLine("		kameiten_cd ")
    '        .AppendLine("		,syouhin_cd ")
    '        .AppendLine("		,tys_houhou_no ")
    '        .AppendLine("		,tokubetu_taiou_cd ")
    '        .AppendLine("		,torikesi ")
    '        .AppendLine("		,add_login_user_id ")
    '        .AppendLine("		,add_datetime ")
    '        .AppendLine("		,upd_login_user_id ")
    '        .AppendLine("		,upd_datetime ")
    '        .AppendLine("	) ")
    '        .AppendLine("VALUES ")
    '        .AppendLine("	( ")
    '        .AppendLine("		@kameiten_cd ")
    '        .AppendLine("		,@syouhin_cd ")
    '        .AppendLine("		,@tys_houhou_no ")
    '        .AppendLine("		,@tokubetu_taiou_cd ")
    '        .AppendLine("		,@torikesi ")
    '        .AppendLine("		,@add_login_user_id ")
    '        .AppendLine("		,GETDATE() ")
    '        .AppendLine("		,NULL ")
    '        .AppendLine("		,NULL ")
    '        .AppendLine("	) ")
    '    End With

    '    With strSqlUpd
    '        .AppendLine("UPDATE ")
    '        .AppendLine("	m_kamei_syouhin_tys_tokubetu_taiou WITH(UPDLOCK) ")
    '        .AppendLine("SET ")
    '        .AppendLine("	torikesi = @torikesi ")
    '        .AppendLine("	,upd_login_user_id = @upd_login_user_id ")
    '        .AppendLine("	,upd_datetime = GETDATE() ")
    '        .AppendLine("WHERE ")
    '        .AppendLine("	kameiten_cd = @kameiten_cd ")
    '        .AppendLine("   AND ")
    '        .AppendLine("	syouhin_cd = @syouhin_cd ")
    '        .AppendLine("   AND ")
    '        .AppendLine("	tys_houhou_no = @tys_houhou_no ")
    '        .AppendLine("   AND ")
    '        .AppendLine("	tokubetu_taiou_cd = @tokubetu_taiou_cd ")
    '    End With

    '    For i As Integer = 0 To dtOk.Rows.Count - 1
    '        'パラメータの設定
    '        paramList.Clear()
    '        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, dtOk.Rows(i).Item("kameiten_cd").ToString.Trim))
    '        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtOk.Rows(i).Item("syouhin_cd").ToString.Trim))
    '        paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 4, dtOk.Rows(i).Item("tys_houhou_no").ToString.Trim))
    '        paramList.Add(MakeParam("@tokubetu_taiou_cd", SqlDbType.Int, 4, dtOk.Rows(i).Item("tokubetu_taiou_cd").ToString.Trim))
    '        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 1, dtOk.Rows(i).Item("torikesi").ToString.Trim))
    '        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, dtOk.Rows(i).Item("add_login_user_id").ToString.Trim))
    '        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, dtOk.Rows(i).Item("upd_login_user_id").ToString.Trim))

    '        Try
    '            If dtOk.Rows(i).Item("ins_upd_flg").Equals("0") Then
    '                '追加
    '                InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlIns.ToString(), paramList.ToArray)
    '            Else
    '                '更新
    '                InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlUpd.ToString(), paramList.ToArray)
    '            End If

    '            If Not (InsUpdCount > 0) Then
    '                Return False
    '            End If

    '        Catch ex As Exception
    '            Return False
    '        End Try

    '    Next

    '    Return True
    'End Function

    ''' <summary>加盟店商品調査方法特別対応マスタにデータを追加と更新</summary>
    ''' <history>2012/02/07　陳琳(大連情報システム部)　新規作成</history>
    Public Function InsUpdTokubetuTaiou(ByVal dtOk As Data.DataTable) As Boolean
        '戻り値
        Dim InsUpdCount As Integer = 0
        '追加用sql文
        Dim strSqlIns As New System.Text.StringBuilder
        '更新用sql文
        Dim strSqlUpd As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        '追加用SQL文
        With strSqlIns
            .AppendLine("INSERT INTO ")
            .AppendLine("	m_kamei_syouhin_tys_tokubetu_taiou WITH(UPDLOCK) ")
            .AppendLine("	( ")
            .AppendLine("		aitesaki_syubetu ")
            .AppendLine("		,aitesaki_cd ")
            .AppendLine("		,syouhin_cd ")
            .AppendLine("		,tys_houhou_no ")
            .AppendLine("		,tokubetu_taiou_cd ")
            .AppendLine("		,torikesi ")
            .AppendLine("		,kasan_syouhin_cd ")
            .AppendLine("		,syokiti ")
            .AppendLine("		,uri_kasan_gaku ")
            .AppendLine("		,koumuten_kasan_gaku ")
            .AppendLine("		,add_login_user_id ")
            .AppendLine("		,add_datetime ")
            .AppendLine("		,upd_login_user_id ")
            .AppendLine("		,upd_datetime ")
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("	( ")
            .AppendLine("		@aitesaki_syubetu ")
            .AppendLine("		,@aitesaki_cd ")
            .AppendLine("		,@syouhin_cd ")
            .AppendLine("		,@tys_houhou_no ")
            .AppendLine("		,@tokubetu_taiou_cd ")
            .AppendLine("		,@torikesi ")
            .AppendLine("		,@kasan_syouhin_cd ")
            .AppendLine("		,@syokiti ")
            .AppendLine("		,@uri_kasan_gaku ")
            .AppendLine("		,@koumuten_kasan_gaku ")
            .AppendLine("		,@add_login_user_id ")
            .AppendLine("		,GETDATE() ")
            .AppendLine("		,NULL ")
            .AppendLine("		,NULL ")
            .AppendLine("	) ")
        End With

        With strSqlUpd
            .AppendLine("UPDATE ")
            .AppendLine("	m_kamei_syouhin_tys_tokubetu_taiou WITH(UPDLOCK) ")
            .AppendLine("SET ")
            .AppendLine("	torikesi = @torikesi ")
            .AppendLine("	,kasan_syouhin_cd = @kasan_syouhin_cd ")
            .AppendLine("	,syokiti = @syokiti ")
            .AppendLine("	,uri_kasan_gaku = @uri_kasan_gaku ")
            .AppendLine("	,koumuten_kasan_gaku = @koumuten_kasan_gaku ")
            .AppendLine("	,upd_login_user_id = @upd_login_user_id ")
            .AppendLine("	,upd_datetime = GETDATE() ")
            .AppendLine("WHERE ")
            .AppendLine("	aitesaki_syubetu = @aitesaki_syubetu ")
            .AppendLine("   AND ")
            .AppendLine("	aitesaki_cd = @aitesaki_cd ")
            .AppendLine("   AND ")
            .AppendLine("	syouhin_cd = @syouhin_cd ")
            .AppendLine("   AND ")
            .AppendLine("	tys_houhou_no = @tys_houhou_no ")
            .AppendLine("   AND ")
            .AppendLine("	tokubetu_taiou_cd = @tokubetu_taiou_cd ")
        End With

        For i As Integer = 0 To dtOk.Rows.Count - 1
            'パラメータの設定
            paramList.Clear()
            paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 5, InsObj(dtOk.Rows(i).Item("aitesaki_syubetu").ToString.Trim)))
            paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("aitesaki_cd").ToString.Trim)))
            paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, InsObj(dtOk.Rows(i).Item("syouhin_cd").ToString.Trim)))
            paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 4, InsObj(dtOk.Rows(i).Item("tys_houhou_no").ToString.Trim)))
            paramList.Add(MakeParam("@tokubetu_taiou_cd", SqlDbType.Int, 4, InsObj(dtOk.Rows(i).Item("tokubetu_taiou_cd").ToString.Trim)))
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 1, InsObj(dtOk.Rows(i).Item("torikesi").ToString.Trim)))
            paramList.Add(MakeParam("@kasan_syouhin_cd", SqlDbType.VarChar, 8, InsObj(dtOk.Rows(i).Item("kasan_syouhin_cd").ToString.Trim)))
            paramList.Add(MakeParam("@syokiti", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("syokiti").ToString.Trim)))
            paramList.Add(MakeParam("@uri_kasan_gaku", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("uri_kasan_gaku").ToString.Trim)))
            paramList.Add(MakeParam("@koumuten_kasan_gaku", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("koumuten_kasan_gaku").ToString.Trim)))
            paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("add_login_user_id").ToString.Trim)))
            paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("upd_login_user_id").ToString.Trim)))

            Try
                If dtOk.Rows(i).Item("ins_upd_flg").Equals("0") Then
                    '追加
                    InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlIns.ToString(), paramList.ToArray)
                Else
                    '更新
                    InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlUpd.ToString(), paramList.ToArray)
                End If

                If Not (InsUpdCount > 0) Then
                    Return False
                End If

            Catch ex As Exception
                Return False
            End Try

        Next

        Return True
    End Function

    Function InsObj(ByVal str As String) As Object
        If String.IsNullOrEmpty(str) Then
            Return DBNull.Value
        Else
            Return str
        End If
    End Function

    ''' <summary>アップロード管理テーブルを登録する</summary>
    Public Function InsInputKanri(ByVal strSyoriDatetime As String, ByVal strNyuuryokuFileMei As String, ByVal strEdiJouhouSakuseiDate As String, ByVal strErrorUmu As Integer, ByVal strAddLoginUserId As String) As Boolean

        '戻り値
        Dim InsCount As Integer = 0

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine("INSERT INTO  ")
            .AppendLine("	t_upload_kanri ")
            .AppendLine("	( ")
            .AppendLine("		syori_datetime ")
            .AppendLine("		,nyuuryoku_file_mei ")
            .AppendLine("		,edi_jouhou_sakusei_date ")
            .AppendLine("		,error_umu ")
            .AppendLine("		,file_kbn ")
            .AppendLine("		,add_login_user_id ")
            .AppendLine("		,add_datetime ")
            .AppendLine("		,upd_login_user_id ")
            .AppendLine("		,upd_datetime ")
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("( ")
            .AppendLine("	@syori_datetime ")
            .AppendLine("	,@nyuuryoku_file_mei ")
            .AppendLine("	,@edi_jouhou_sakusei_date ")
            .AppendLine("	,@error_umu ")
            .AppendLine("	,4 ")
            .AppendLine("	,@add_login_user_id ")
            .AppendLine("	,GETDATE() ")
            .AppendLine("	,NULL ")
            .AppendLine("	,NULL ")
            .AppendLine(") ")
        End With

        paramList.Add(MakeParam("@syori_datetime", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(strSyoriDatetime))))
        paramList.Add(MakeParam("@nyuuryoku_file_mei", SqlDbType.VarChar, 128, strNyuuryokuFileMei))
        paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdiJouhouSakuseiDate))
        paramList.Add(MakeParam("@error_umu", SqlDbType.Int, 8, strErrorUmu))
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strAddLoginUserId))

        '更新されたデータセットを DB へ書き込み
        Try
            InsCount = ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)
        Catch ex As Exception
            Return False
        End Try

        If InsCount > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    '''' <summary>加盟店商品調査方法特別対応エラー情報を取得する</summary>
    '''' <param name="strEdidate">EDI情報作成日</param>
    '''' <param name="strSyoridate">処理日時</param>
    '''' <returns>加盟店商品調査方法特別対応エラーデータテーブル</returns>
    'Public Function SelTokubetuTaiouError(ByVal strEdidate As String, ByVal strSyoridate As String) As Data.DataTable
    '    'DataSetインスタンスの生成
    '    Dim dsReturn As New Data.DataSet
    '    'SQLコメント
    '    Dim commandTextSb As New StringBuilder
    '    ''パラメータ格納
    '    Dim paramList As New List(Of SqlClient.SqlParameter)
    '    'SQL文
    '    With commandTextSb
    '        .AppendLine(" SELECT ")
    '        .AppendLine("   TOP 100 ")
    '        .AppendLine("    MKSTTTE.edi_jouhou_sakusei_date ")     'EDI情報作成日
    '        .AppendLine("    ,MKSTTTE.gyou_no ")    '行NO
    '        .AppendLine("    ,MKSTTTE.kameiten_cd ")    '加盟店コード
    '        .AppendLine("    ,MKSTTTE.kameiten_mei ")   '加盟店名
    '        .AppendLine("    ,MKSTTTE.syouhin_cd ")     '商品コード
    '        .AppendLine("    ,MKSTTTE.syouhin_mei ")    '商品名
    '        '調査方法（調査方法NO:調査方法名）
    '        .AppendLine("    ,CASE WHEN ISNULL(MKSTTTE.tys_houhou_no,'') = '' AND ISNULL(MKSTTTE.tys_houhou,'') = '' THEN '' ")
    '        .AppendLine("       ELSE MKSTTTE.tys_houhou_no + '：' + MKSTTTE.tys_houhou ")
    '        .AppendLine("     END AS tys_houhou ")
    '        '.AppendLine("    ,(MKSTTTE.tys_houhou_no + '：' + MKSTTTE.tys_houhou) AS tys_houhou ")   
    '        .AppendLine("    ,MKSTTTE.tokubetu_taiou_cd ")  '特別対応コード
    '        .AppendLine("    ,MKSTTTE.tokubetu_taiou_meisyou ")     '特別対応名称
    '        .AppendLine("    ,CASE MKSTTTE.torikesi ")
    '        .AppendLine("       WHEN '0' THEN '' ")
    '        .AppendLine("       ELSE '取消'    ")
    '        .AppendLine("     END AS torikesi ")     '取消
    '        .AppendLine("    ,MKSTTTE.add_login_user_id ")      '登録者ID
    '        .AppendLine("    ,MKSTTTE.add_datetime ")       '登録日時
    '        .AppendLine("    ,MKSTTTE.upd_login_user_id ")      '更新者ID
    '        .AppendLine("    ,MKSTTTE.upd_datetime ")       '更新日時
    '        .AppendLine(" FROM  ")
    '        .AppendLine("    m_kamei_syouhin_tys_tokubetu_taiou_error AS MKSTTTE WITH(READCOMMITTED) ")  '加盟店商品調査方法特別対応エラーマスタ
    '        .AppendLine(" WHERE ")
    '        .AppendLine("    MKSTTTE.edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date ")   'EDI情報作成日
    '        .AppendLine(" AND CONVERT(varchar(100),MKSTTTE.syori_datetime,112) + REPLACE(CONVERT(varchar(100),MKSTTTE.syori_datetime,114),':','') = @syori_datetime ")  '処理日時
    '        .AppendLine(" ORDER BY ")
    '        .AppendLine("    MKSTTTE.gyou_no ")
    '    End With
    '    'EDI情報作成日
    '    paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdidate))
    '    '処理日時
    '    paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 40, strSyoridate))
    '    '検索実行
    '    FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "TokubetuTaiouError", paramList.ToArray)

    '    Return dsReturn.Tables(0)

    'End Function

    ''' <summary>加盟店商品調査方法特別対応エラー情報を取得する</summary>
    ''' <param name="strEdidate">EDI情報作成日</param>
    ''' <param name="strSyoridate">処理日時</param>
    ''' <returns>加盟店商品調査方法特別対応エラーデータテーブル</returns>
    Public Function SelTokubetuTaiouError(ByVal strEdidate As String, ByVal strSyoridate As String) As Data.DataTable
        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet
        'SQLコメント
        Dim commandTextSb As New StringBuilder
        ''パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL文
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   TOP 100 ")
            .AppendLine("    MKSTTTE.edi_jouhou_sakusei_date ")     'EDI情報作成日
            .AppendLine("    ,MKSTTTE.gyou_no ")                    '行NO
            .AppendLine("    ,MKSTTTE.syori_datetime ")             '処理日時
            .AppendLine("    ,MKSTTTE.aitesaki_syubetu ")           '相手先種別
            .AppendLine("    ,MKSTTTE.aitesaki_cd ")                '相手先コード
            .AppendLine("    ,MKSTTTE.aitesaki_mei ")               '相手先名
            .AppendLine("    ,MKSTTTE.syouhin_cd ")                 '商品コード
            .AppendLine("    ,MKSTTTE.syouhin_mei ")                '商品名
            '調査方法（調査方法NO:調査方法名）
            .AppendLine("    ,CASE WHEN ISNULL(MKSTTTE.tys_houhou_no,'') = '' AND ISNULL(MKSTTTE.tys_houhou,'') = '' THEN '' ")
            .AppendLine("       ELSE MKSTTTE.tys_houhou_no + '：' + MKSTTTE.tys_houhou ")
            .AppendLine("     END AS tys_houhou ")
            '.AppendLine("    ,(MKSTTTE.tys_houhou_no + '：' + MKSTTTE.tys_houhou) AS tys_houhou ")   
            .AppendLine("    ,MKSTTTE.tokubetu_taiou_cd ")          '特別対応コード
            .AppendLine("    ,MKSTTTE.tokubetu_taiou_meisyou ")     '特別対応名称
            .AppendLine("    ,CASE MKSTTTE.torikesi ")
            .AppendLine("       WHEN '0' THEN '' ")
            .AppendLine("       ELSE '取消'    ")
            .AppendLine("     END AS torikesi ")                    '取消
            .AppendLine("    ,MKSTTTE.kasan_syouhin_cd ")           '金額加算商品コード
            .AppendLine("    ,MKSTTTE.kasan_syouhin_mei ")          '金額加算商品名
            .AppendLine("    ,MKSTTTE.syokiti ")                    '初期値
            .AppendLine("    ,MKSTTTE.uri_kasan_gaku ")             '実請求加算金額
            .AppendLine("    ,MKSTTTE.koumuten_kasan_gaku ")        '工務店請求加算金額
            .AppendLine("    ,MKSTTTE.add_login_user_id ")          '登録者ID
            .AppendLine("    ,MKSTTTE.add_datetime ")               '登録日時
            .AppendLine("    ,MKSTTTE.upd_login_user_id ")          '更新者ID
            .AppendLine("    ,MKSTTTE.upd_datetime ")               '更新日時
            .AppendLine(" FROM  ")
            .AppendLine("    m_kamei_syouhin_tys_tokubetu_taiou_error AS MKSTTTE WITH(READCOMMITTED) ")  '加盟店商品調査方法特別対応エラーマスタ
            .AppendLine(" WHERE ")
            .AppendLine("    MKSTTTE.edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date ")   'EDI情報作成日
            .AppendLine(" AND CONVERT(varchar(100),MKSTTTE.syori_datetime,112) + REPLACE(CONVERT(varchar(100),MKSTTTE.syori_datetime,114),':','') = @syori_datetime ")  '処理日時
            .AppendLine(" ORDER BY ")
            .AppendLine("    MKSTTTE.gyou_no ")
        End With
        'EDI情報作成日
        paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdidate))
        '処理日時
        paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 40, strSyoridate))
        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "TokubetuTaiouError", paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function

    ''' <summary>加盟店商品調査方法特別対応マスタエラーCSVを取得</summary>
    ''' <returns>加盟店商品調査方法特別対応マスタエラーCSVテーブル</returns>
    Public Function SelTokubetuTaiouErrorCSV(ByVal strEdidate As String, ByVal strSyoridate As String) As Data.DataTable
        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet
        'SQLコメント
        Dim commandTextSb As New StringBuilder
        ''パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL文
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   TOP 5000")
            .AppendLine("    MKSTTTE.edi_jouhou_sakusei_date ")     'EDI情報作成日
            .AppendLine("    ,MKSTTTE.gyou_no ")                    '行NO
            .AppendLine("    ,MKSTTTE.syori_datetime")              '処理日時
            .AppendLine("    ,MKSTTTE.aitesaki_syubetu ")           '相手先種別
            .AppendLine("    ,MKSTTTE.aitesaki_cd ")                '相手先コード
            .AppendLine("    ,MKSTTTE.aitesaki_mei ")               '相手先名
            .AppendLine("    ,MKSTTTE.syouhin_cd ")                 '商品コード
            .AppendLine("    ,MKSTTTE.syouhin_mei ")                '商品名
            .AppendLine("    ,MKSTTTE.tys_houhou_no")               '調査方法コード
            .AppendLine("    ,MKSTTTE.tys_houhou")                  '調査方法名称
            .AppendLine("    ,MKSTTTE.tokubetu_taiou_cd ")          '特別対応コード
            .AppendLine("    ,MKSTTTE.tokubetu_taiou_meisyou ")     '特別対応名称
            .AppendLine("    ,MKSTTTE.torikesi ")                   '取消
            .AppendLine("    ,MKSTTTE.kasan_syouhin_cd ")           '金額加算商品コード
            .AppendLine("    ,MKSTTTE.kasan_syouhin_mei ")          '金額加算商品名
            .AppendLine("    ,MKSTTTE.syokiti ")                    '初期値
            .AppendLine("    ,MKSTTTE.uri_kasan_gaku ")             '実請求加算金額
            .AppendLine("    ,MKSTTTE.koumuten_kasan_gaku ")        '工務店請求加算金額
            .AppendLine("    ,MKSTTTE.add_login_user_id ")          '登録者ID
            .AppendLine("    ,MKSTTTE.add_datetime ")               '登録日時
            .AppendLine("    ,MKSTTTE.upd_login_user_id ")          '更新者ID
            .AppendLine("    ,MKSTTTE.upd_datetime ")               '更新日時
            .AppendLine(" FROM  ")
            .AppendLine("    m_kamei_syouhin_tys_tokubetu_taiou_error AS MKSTTTE WITH(READCOMMITTED) ")  '加盟店商品調査方法特別対応エラーマスタ
            .AppendLine(" WHERE ")
            .AppendLine("    MKSTTTE.edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date ")   'EDI情報作成日
            .AppendLine(" AND CONVERT(varchar(100),MKSTTTE.syori_datetime,112) + REPLACE(CONVERT(varchar(100),MKSTTTE.syori_datetime,114),':','') = @syori_datetime ")  '処理日時
            .AppendLine(" ORDER BY ")
            .AppendLine("    MKSTTTE.gyou_no ")
        End With
        'EDI情報作成日
        paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdidate))
        '処理日時
        paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 40, strSyoridate))
        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "TokubetuTaiouError", paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function

    ''' <summary>加盟店商品調査特別対応マスタエラー件数を取得する</summary>
    ''' <param name="strEdidate">EDI情報作成日</param>
    ''' <returns>加盟店商品調査特別対応マスタエラー件数</returns>
    Public Function SelTokubetuTaiouErrorCount(ByVal strEdidate As String, ByVal strSyoridate As String) As Integer
        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   COUNT(*) AS Maxcount ")
            .AppendLine(" FROM ")
            .AppendLine("   m_kamei_syouhin_tys_tokubetu_taiou_error AS MKSTTTE WITH(READCOMMITTED) ")  '加盟店商品調査方法特別対応エラーマスタ
            .AppendLine(" WHERE ")
            .AppendLine("    MKSTTTE.edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date ")  'EDI情報作成日
            .AppendLine(" AND CONVERT(varchar(100),MKSTTTE.syori_datetime,112) + REPLACE(CONVERT(varchar(100),MKSTTTE.syori_datetime,114),':','') = @syori_datetime ")  '処理日時
        End With

        'EDI情報作成日
        paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdidate))
        '処理日時
        paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 40, strSyoridate))
        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "TokubetuTaiouErrorCount", paramList.ToArray)

        Return dsReturn.Tables(0).Rows(0).Item("Maxcount")

    End Function

    ''' <summary>相手先名を取得する</summary>
    ''' <returns>相手先名データテーブル</returns>
    ''' <history>2012/02/08　陳琳(大連情報システム部)　新規作成</history>
    Public Function SelAitesakiMei(ByVal intAitesakiSyubetu As Integer, ByVal AitesakiCd As String, ByVal strTorikesiAitesaki As String) As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT  ")
            .AppendLine("	aitesaki_syubetu ") '--相手先種別
            .AppendLine("	,aitesaki_cd ") '--相手先コード
            .AppendLine("	,aitesaki_mei ")    '--相手先名
            .AppendLine("FROM  ")
            .AppendLine("	(  ")
            .AppendLine("		SELECT  ")
            .AppendLine("			0				AS aitesaki_syubetu ")  '--相手先種別
            .AppendLine("			,'ALL'			AS aitesaki_cd ")   '--相手先コード
            .AppendLine("			,'相手先なし'	AS aitesaki_mei ")  '--相手先名
            .AppendLine("			,0				AS torikesi ")  '--取消
            .AppendLine("		UNION ALL  ")
            .AppendLine("		SELECT  ")
            .AppendLine("			1					AS aitesaki_syubetu  ")
            .AppendLine("			,MKA.kameiten_cd	AS aitesaki_cd ")   '--加盟店コード
            .AppendLine("			,MKA.kameiten_mei1	AS aitesaki_mei ")  '--加盟店名1
            .AppendLine("			,MKA.torikesi		AS torikesi  ")
            .AppendLine("		FROM  ")
            .AppendLine("			m_kameiten AS MKA WITH(READCOMMITTED) ")    '--加盟店マスタ
            .AppendLine("	    UNION ALL ")
            .AppendLine("	    SELECT ")
            .AppendLine("	        '5'                 AS aitesaki_syubetu, ")
            .AppendLine("	        ME.eigyousyo_cd     AS aitesaki_cd, ")
            .AppendLine("	        ME.eigyousyo_mei    AS aitesaki_name, ")
            .AppendLine("	        ME.torikesi         AS torikesi ")
            .AppendLine("	    FROM ")
            .AppendLine("	        m_eigyousyo AS ME WITH (READCOMMITTED) ")
            .AppendLine("		UNION ALL  ")
            .AppendLine("		SELECT  ")
            .AppendLine("			7						AS aitesaki_syubetu  ")
            .AppendLine("			,MKE.keiretu_cd			AS aitesaki_cd ")   '--系列コード
            .AppendLine("			,MIN(MKE.keiretu_mei)	AS aitesaki_mei ")  '--系列名
            .AppendLine("			,MIN(MKE.torikesi)		AS torikesi  ")
            .AppendLine("		FROM  ")
            .AppendLine("			m_keiretu AS MKE WITH(READCOMMITTED) ") '--系列マスタ
            .AppendLine("		GROUP BY  ")
            .AppendLine("			keiretu_cd ")   '--系列コード
            .AppendLine("	) AS TA  ")
            .AppendLine(" WHERE  ")
            .AppendLine(" 	aitesaki_syubetu = @aitesaki_syubetu ")
            .AppendLine(" 	AND  ")
            .AppendLine(" 	aitesaki_cd = @aitesaki_cd ")
        End With

        If strTorikesiAitesaki <> String.Empty Then
            '取消パラメータの設定
            commandTextSb.AppendLine(" AND torikesi = @torikesi ")
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 8, "0"))
        End If

        paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 8, intAitesakiSyubetu))
        paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, AitesakiCd))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtAitesakiCd", paramList.ToArray)

        '戻り値
        Return dsReturn.Tables(0)

    End Function

    ''' <summary>加盟店商品調査方法特別対応マスタ情報を取得(「系列・営業所・指定無しも対象チェックボックス」=チェックの場合)</summary>
    ''' <history>2012/05/23 車龍 407553の対応 追加</history>
    Public Function SelTokubetuTaiouNasiInfo(ByVal dtParamList As Dictionary(Of String, String)) As Data.DataTable

        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            If dtParamList.Item("kensuu").ToString <> "max" Then
                .AppendLine("   TOP " & dtParamList.Item("kensuu").ToString & "")
            End If
            .AppendLine("	SUB_MKSTTT.aitesaki_syubetu ") '--相手先種別 ")
            .AppendLine("	,(LTRIM(STR(SUB_MKSTTT.aitesaki_syubetu)) + '：' +MKM.meisyou) AS aitesaki_syubetu_layout ") '相手先種別(相手先種別:拡張名称)
            .AppendLine("	,SUB_MKSTTT.aitesaki_cd ") '--相手先コード ")
            .AppendLine("	,MKM.meisyou ")                         '拡張名称
            .AppendLine("	,SUB.aitesaki_name ")                   '相手先名
            .AppendLine("	,SUB_MKSTTT.syouhin_cd ") '--商品コード ")
            .AppendLine("	,MS.syouhin_mei ")                      '商品名称
            .AppendLine("	,SUB_MKSTTT.tys_houhou_no ") '--調査方法NO ")
            .AppendLine("	,MT.tys_houhou_mei ")                   '調査方法名称
            .AppendLine("	,(LTRIM(STR(SUB_MKSTTT.tys_houhou_no)) + '：' +MT.tys_houhou_mei) AS tys_houhou ") '調査方法(調査方法NO:調査方法名称)
            .AppendLine("	,SUB_MKSTTT.tokubetu_taiou_cd ") '--特別対応コード ")
            .AppendLine("	,MTT.tokubetu_taiou_meisyou ")          '特別対応名称
            .AppendLine("	,SUB_MKSTTT.torikesi ") '--取消 ")
            .AppendLine("	,SUB_MKSTTT.kasan_syouhin_cd ") '--金額加算商品コード ")
            .AppendLine("	,SUB_MKSTTT.syokiti ") '--初期値 ")
            .AppendLine("	,SUB_MKSTTT.uri_kasan_gaku ") '--実請求加算金額 ")
            .AppendLine("	,SUB_MKSTTT.koumuten_kasan_gaku ") '--工務店請求加算金額 ")
            .AppendLine("FROM ")
            .AppendLine("	( ")
            .AppendLine("		SELECT ")
            .AppendLine("			aitesaki_syubetu ") '--相手先種別 ")
            .AppendLine("			,aitesaki_cd ") '--相手先コード ")
            .AppendLine("			,syouhin_cd ") '--商品コード ")
            .AppendLine("			,tys_houhou_no ") '--調査方法NO ")
            .AppendLine("			,tokubetu_taiou_cd ") '--特別対応コード ")
            .AppendLine("			,torikesi ") '--取消 ")
            .AppendLine("			,kasan_syouhin_cd ") '--金額加算商品コード ")
            .AppendLine("			,syokiti ") '--初期値 ")
            .AppendLine("			,uri_kasan_gaku ") '--実請求加算金額 ")
            .AppendLine("			,koumuten_kasan_gaku ") '--工務店請求加算金額 ")
            .AppendLine("		FROM ")
            .AppendLine("			m_kamei_syouhin_tys_tokubetu_taiou WITH(READCOMMITTED) ") '--加盟店商品調査方法特別対応マスタ ")
            .AppendLine("		WHERE ")
            .AppendLine("			aitesaki_syubetu = @aitesaki_syubetu1 ") '--相手先種別 = 1 ")
            .AppendLine("			AND ")
            .AppendLine("			aitesaki_cd = @aitesaki_cd ") '--相手先コード ")
            .AppendLine("		UNION ALL ") '--==================================================================================================== ")
            .AppendLine("		SELECT ")
            .AppendLine("			MKSTTT.aitesaki_syubetu ") '--相手先種別 ")
            .AppendLine("			,MKSTTT.aitesaki_cd ") '--相手先コード ")
            .AppendLine("			,MKSTTT.syouhin_cd ") '--商品コード ")
            .AppendLine("			,MKSTTT.tys_houhou_no ") '--調査方法NO ")
            .AppendLine("			,MKSTTT.tokubetu_taiou_cd ") '--特別対応コード ")
            .AppendLine("			,MKSTTT.torikesi ") '--取消 ")
            .AppendLine("			,MKSTTT.kasan_syouhin_cd ") '--金額加算商品コード ")
            .AppendLine("			,MKSTTT.syokiti ") '--初期値 ")
            .AppendLine("			,MKSTTT.uri_kasan_gaku ") '--実請求加算金額 ")
            .AppendLine("			,MKSTTT.koumuten_kasan_gaku ") '--工務店請求加算金額 ")
            .AppendLine("		FROM ")
            .AppendLine("			m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH(READCOMMITTED) ") '--加盟店商品調査方法特別対応マスタ ")
            .AppendLine("			INNER JOIN ")
            .AppendLine("			m_kameiten AS MK WITH(READCOMMITTED) ") '--加盟店マスタ ")
            .AppendLine("			ON ")
            .AppendLine("				MKSTTT.aitesaki_syubetu = @aitesaki_syubetu5 ") '--相手先種別 = 5 ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.aitesaki_cd = MK.eigyousyo_cd ") '--加特M.相手先コード = 加M.営業所コード ")
            .AppendLine("			LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT ")
            .AppendLine("					aitesaki_syubetu ") '--相手先種別 ")
            .AppendLine("					,aitesaki_cd ") '--相手先コード ")
            .AppendLine("					,syouhin_cd ") '--商品コード ")
            .AppendLine("					,tys_houhou_no ") '--調査方法NO ")
            .AppendLine("					,tokubetu_taiou_cd ") '--特別対応コード ")
            .AppendLine("					,torikesi ") '--取消 ")
            .AppendLine("					,kasan_syouhin_cd ") '--金額加算商品コード ")
            .AppendLine("					,syokiti ") '--初期値 ")
            .AppendLine("					,uri_kasan_gaku ") '--実請求加算金額 ")
            .AppendLine("					,koumuten_kasan_gaku ") '--工務店請求加算金額 ")
            .AppendLine("				FROM ")
            .AppendLine("					m_kamei_syouhin_tys_tokubetu_taiou WITH(READCOMMITTED) ") '--加盟店商品調査方法特別対応マスタ ")
            .AppendLine("				WHERE ")
            .AppendLine("					aitesaki_syubetu = @aitesaki_syubetu1 ") '--相手先種別 = 1 ")
            .AppendLine("					AND ")
            .AppendLine("					aitesaki_cd = @aitesaki_cd ") '--相手先コード ")
            .AppendLine("					AND ")
            .AppendLine("					ISNULL(torikesi,0) = 0 ") '--取消 = 0 ")
            .AppendLine("			 ")
            .AppendLine("			) AS SUB_MKSTTT_1 ") '--加特サブ1 ")
            .AppendLine("			ON ")
            .AppendLine("				MKSTTT.syouhin_cd = SUB_MKSTTT_1.syouhin_cd ") '--加特M.商品コード = 加特サブ1.商品コード ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tys_houhou_no = SUB_MKSTTT_1.tys_houhou_no ") '--加特M.調査方法NO = 加特サブ1.調査方法NO ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tokubetu_taiou_cd = SUB_MKSTTT_1.tokubetu_taiou_cd ") '--加特M.特別対応コード = 加特サブ1.特別対応コード
            .AppendLine("		WHERE ")
            .AppendLine("			MK.kameiten_cd = @aitesaki_cd ") '--相手先コード ")
            .AppendLine("			AND ")
            .AppendLine("			SUB_MKSTTT_1.aitesaki_cd IS NULL ") '--加特サブ1.相手先コード IS NULL ")
            .AppendLine("		UNION ALL ") '--==================================================================================================== ")
            .AppendLine("		SELECT ")
            .AppendLine("			MKSTTT.aitesaki_syubetu ") '--相手先種別 ")
            .AppendLine("			,MKSTTT.aitesaki_cd ") '--相手先コード ")
            .AppendLine("			,MKSTTT.syouhin_cd ") '--商品コード ")
            .AppendLine("			,MKSTTT.tys_houhou_no ") '--調査方法NO ")
            .AppendLine("			,MKSTTT.tokubetu_taiou_cd ") '--特別対応コード ")
            .AppendLine("			,MKSTTT.torikesi ") '--取消 ")
            .AppendLine("			,MKSTTT.kasan_syouhin_cd ") '--金額加算商品コード ")
            .AppendLine("			,MKSTTT.syokiti ") '--初期値 ")
            .AppendLine("			,MKSTTT.uri_kasan_gaku ") '--実請求加算金額 ")
            .AppendLine("			,MKSTTT.koumuten_kasan_gaku ") '--工務店請求加算金額 ")
            .AppendLine("		FROM ")
            .AppendLine("			m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH(READCOMMITTED) ") '--加盟店商品調査方法特別対応マスタ ")
            .AppendLine("			INNER JOIN ")
            .AppendLine("			m_kameiten AS MK WITH(READCOMMITTED) ") '--加盟店マスタ ")
            .AppendLine("			ON ")
            .AppendLine("				MKSTTT.aitesaki_syubetu = @aitesaki_syubetu7 ") '--相手先種別 = 7 ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.aitesaki_cd = MK.keiretu_cd ") '--加特M.相手先コード = 加M.系列コード ")
            .AppendLine("			LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT ")
            .AppendLine("					aitesaki_syubetu ") '--相手先種別 ")
            .AppendLine("					,aitesaki_cd ") '--相手先コード ")
            .AppendLine("					,syouhin_cd ") '--商品コード ")
            .AppendLine("					,tys_houhou_no ") '--調査方法NO ")
            .AppendLine("					,tokubetu_taiou_cd ") '--特別対応コード ")
            .AppendLine("					,torikesi ") '--取消 ")
            .AppendLine("					,kasan_syouhin_cd ") '--金額加算商品コード ")
            .AppendLine("					,syokiti ") '--初期値 ")
            .AppendLine("					,uri_kasan_gaku ") '--実請求加算金額 ")
            .AppendLine("					,koumuten_kasan_gaku ") '--工務店請求加算金額 ")
            .AppendLine("				FROM ")
            .AppendLine("					m_kamei_syouhin_tys_tokubetu_taiou WITH(READCOMMITTED) ") '--加盟店商品調査方法特別対応マスタ ")
            .AppendLine("				WHERE ")
            .AppendLine("					aitesaki_syubetu = @aitesaki_syubetu1 ") '--相手先種別 = 1 ")
            .AppendLine("					AND ")
            .AppendLine("					aitesaki_cd = @aitesaki_cd ") '--相手先コード ")
            .AppendLine("					AND ")
            .AppendLine("					ISNULL(torikesi,0) = 0 ") '--取消 = 0 ")
            .AppendLine("			) AS SUB_MKSTTT_1 ") '--加特サブ1 ")
            .AppendLine("			ON ")
            .AppendLine("				MKSTTT.syouhin_cd = SUB_MKSTTT_1.syouhin_cd ") '--加特M.商品コード = 加特サブ1.商品コード ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tys_houhou_no = SUB_MKSTTT_1.tys_houhou_no ") '--加特M.調査方法NO = 加特サブ1.調査方法NO ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tokubetu_taiou_cd = SUB_MKSTTT_1.tokubetu_taiou_cd ") '--加特M.特別対応コード = 加特サブ1.特別対応コード
            .AppendLine("			LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT ")
            .AppendLine("					MKSTTT.aitesaki_syubetu ") '--相手先種別 ")
            .AppendLine("					,MKSTTT.aitesaki_cd ") '--相手先コード ")
            .AppendLine("					,MKSTTT.syouhin_cd ") '--商品コード ")
            .AppendLine("					,MKSTTT.tys_houhou_no ") '--調査方法NO ")
            .AppendLine("					,MKSTTT.tokubetu_taiou_cd ") '--特別対応コード ")
            .AppendLine("					,MKSTTT.torikesi ") '--取消 ")
            .AppendLine("					,MKSTTT.kasan_syouhin_cd ") '--金額加算商品コード ")
            .AppendLine("					,MKSTTT.syokiti ") '--初期値 ")
            .AppendLine("					,MKSTTT.uri_kasan_gaku ") '--実請求加算金額 ")
            .AppendLine("					,MKSTTT.koumuten_kasan_gaku ") '--工務店請求加算金額 ")
            .AppendLine("				FROM ")
            .AppendLine("					m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH(READCOMMITTED) ") '--加盟店商品調査方法特別対応マスタ ")
            .AppendLine("					INNER JOIN ")
            .AppendLine("					m_kameiten AS MK WITH(READCOMMITTED) ") '--加盟店マスタ ")
            .AppendLine("					ON ")
            .AppendLine("						MKSTTT.aitesaki_syubetu = @aitesaki_syubetu5 ") '--相手先種別 = 5 ")
            .AppendLine("						AND ")
            .AppendLine("						MKSTTT.aitesaki_cd = MK.eigyousyo_cd ") '--加特M.相手先コード = 加M.営業所コード ")
            .AppendLine("						AND ")
            .AppendLine("						MK.kameiten_cd = @aitesaki_cd ") '--相手先コード ")
            .AppendLine("						AND ")
            .AppendLine("						ISNULL(MKSTTT.torikesi,0) = 0 ") '--取消 = 0 ")
            .AppendLine("					 ")
            .AppendLine("			) AS SUB_MKSTTT_5 ") '--加特サブ5 ")
            .AppendLine("			ON ")
            .AppendLine("				MKSTTT.syouhin_cd = SUB_MKSTTT_5.syouhin_cd ") '--加特M.商品コード = 加特サブ5.商品コード ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tys_houhou_no = SUB_MKSTTT_5.tys_houhou_no ") '--加特M.調査方法NO = 加特サブ5.調査方法NO ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tokubetu_taiou_cd = SUB_MKSTTT_5.tokubetu_taiou_cd ") '--加特M.特別対応コード = 加特サブ5.特別対応コード
            .AppendLine("		WHERE ")
            .AppendLine("			MK.kameiten_cd = @aitesaki_cd ") '--相手先コード ")
            .AppendLine("			AND ")
            .AppendLine("			SUB_MKSTTT_1.aitesaki_cd IS NULL ") '--加特サブ1.相手先コード IS NULL ")
            .AppendLine("			AND ")
            .AppendLine("			SUB_MKSTTT_5.aitesaki_cd IS NULL ") '--加特サブ5.相手先コード IS NULL ")
            .AppendLine("		UNION ALL ") '--==================================================================================================== ")
            .AppendLine("		SELECT ")
            .AppendLine("			MKSTTT.aitesaki_syubetu ") '--相手先種別 ")
            .AppendLine("			,MKSTTT.aitesaki_cd ") '--相手先コード ")
            .AppendLine("			,MKSTTT.syouhin_cd ") '--商品コード ")
            .AppendLine("			,MKSTTT.tys_houhou_no ") '--調査方法NO ")
            .AppendLine("			,MKSTTT.tokubetu_taiou_cd ") '--特別対応コード ")
            .AppendLine("			,MKSTTT.torikesi ") '--取消 ")
            .AppendLine("			,MKSTTT.kasan_syouhin_cd ") '--金額加算商品コード ")
            .AppendLine("			,MKSTTT.syokiti ") '--初期値 ")
            .AppendLine("			,MKSTTT.uri_kasan_gaku ") '--実請求加算金額 ")
            .AppendLine("			,MKSTTT.koumuten_kasan_gaku ") '--工務店請求加算金額 ")
            .AppendLine("		FROM ")
            .AppendLine("			m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH(READCOMMITTED) ") '--加盟店商品調査方法特別対応マスタ ")
            .AppendLine("			LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT ")
            .AppendLine("					aitesaki_syubetu ") '--相手先種別 ")
            .AppendLine("					,aitesaki_cd ") '--相手先コード ")
            .AppendLine("					,syouhin_cd ") '--商品コード ")
            .AppendLine("					,tys_houhou_no ") '--調査方法NO ")
            .AppendLine("					,tokubetu_taiou_cd ") '--特別対応コード ")
            .AppendLine("					,torikesi ") '--取消 ")
            .AppendLine("					,kasan_syouhin_cd ") '--金額加算商品コード ")
            .AppendLine("					,syokiti ") '--初期値 ")
            .AppendLine("					,uri_kasan_gaku ") '--実請求加算金額 ")
            .AppendLine("					,koumuten_kasan_gaku ") '--工務店請求加算金額 ")
            .AppendLine("				FROM ")
            .AppendLine("					m_kamei_syouhin_tys_tokubetu_taiou WITH(READCOMMITTED) ") '--加盟店商品調査方法特別対応マスタ ")
            .AppendLine("				WHERE ")
            .AppendLine("					aitesaki_syubetu = @aitesaki_syubetu1 ") '--相手先種別 = 1 ")
            .AppendLine("					AND ")
            .AppendLine("					aitesaki_cd = @aitesaki_cd ") '--相手先コード ")
            .AppendLine("					AND ")
            .AppendLine("					ISNULL(torikesi,0) = 0 ") '--取消 = 0 ")
            .AppendLine("			) AS SUB_MKSTTT_1 ") '--加特サブ1 ")
            .AppendLine("			ON ")
            .AppendLine("				MKSTTT.syouhin_cd = SUB_MKSTTT_1.syouhin_cd ") '--加特M.商品コード = 加特サブ1.商品コード ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tys_houhou_no = SUB_MKSTTT_1.tys_houhou_no ") '--加特M.調査方法NO = 加特サブ1.調査方法NO ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tokubetu_taiou_cd = SUB_MKSTTT_1.tokubetu_taiou_cd ") '--加特M.特別対応コード = 加特サブ1.特別対応コード
            .AppendLine("			LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT ")
            .AppendLine("					MKSTTT.aitesaki_syubetu ") '--相手先種別 ")
            .AppendLine("					,MKSTTT.aitesaki_cd ") '--相手先コード ")
            .AppendLine("					,MKSTTT.syouhin_cd ") '--商品コード ")
            .AppendLine("					,MKSTTT.tys_houhou_no ") '--調査方法NO ")
            .AppendLine("					,MKSTTT.tokubetu_taiou_cd ") '--特別対応コード ")
            .AppendLine("					,MKSTTT.torikesi ") '--取消 ")
            .AppendLine("					,MKSTTT.kasan_syouhin_cd ") '--金額加算商品コード ")
            .AppendLine("					,MKSTTT.syokiti ") '--初期値 ")
            .AppendLine("					,MKSTTT.uri_kasan_gaku ") '--実請求加算金額 ")
            .AppendLine("					,MKSTTT.koumuten_kasan_gaku ") '--工務店請求加算金額 ")
            .AppendLine("				FROM ")
            .AppendLine("					m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH(READCOMMITTED) ") '--加盟店商品調査方法特別対応マスタ ")
            .AppendLine("					INNER JOIN ")
            .AppendLine("					m_kameiten AS MK WITH(READCOMMITTED) ") '--加盟店マスタ ")
            .AppendLine("					ON ")
            .AppendLine("						MKSTTT.aitesaki_syubetu = @aitesaki_syubetu5 ") '--相手先種別 = 5 ")
            .AppendLine("						AND ")
            .AppendLine("						MKSTTT.aitesaki_cd = MK.eigyousyo_cd ") '--加特M.相手先コード = 加M.営業所コード ")
            .AppendLine("						AND ")
            .AppendLine("						MK.kameiten_cd = @aitesaki_cd ") '--相手先コード ")
            .AppendLine("						AND ")
            .AppendLine("						ISNULL(MKSTTT.torikesi,0) = 0 ") '--取消 = 0 ")
            .AppendLine("			) AS SUB_MKSTTT_5 ") '--加特サブ5 ")
            .AppendLine("			ON ")
            .AppendLine("				MKSTTT.syouhin_cd = SUB_MKSTTT_5.syouhin_cd ") '--加特M.商品コード = 加特サブ5.商品コード ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tys_houhou_no = SUB_MKSTTT_5.tys_houhou_no ") '--加特M.調査方法NO = 加特サブ5.調査方法NO ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tokubetu_taiou_cd = SUB_MKSTTT_5.tokubetu_taiou_cd ") '--加特M.特別対応コード = 加特サブ5.特別対応コード
            .AppendLine("				 ")
            .AppendLine("			LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT ")
            .AppendLine("					MKSTTT.aitesaki_syubetu ") '--相手先種別 ")
            .AppendLine("					,MKSTTT.aitesaki_cd ") '--相手先コード ")
            .AppendLine("					,MKSTTT.syouhin_cd ") '--商品コード ")
            .AppendLine("					,MKSTTT.tys_houhou_no ") '--調査方法NO ")
            .AppendLine("					,MKSTTT.tokubetu_taiou_cd ") '--特別対応コード ")
            .AppendLine("					,MKSTTT.torikesi ") '--取消 ")
            .AppendLine("					,MKSTTT.kasan_syouhin_cd ") '--金額加算商品コード ")
            .AppendLine("					,MKSTTT.syokiti ") '--初期値 ")
            .AppendLine("					,MKSTTT.uri_kasan_gaku ") '--実請求加算金額 ")
            .AppendLine("					,MKSTTT.koumuten_kasan_gaku ") '--工務店請求加算金額 ")
            .AppendLine("				FROM ")
            .AppendLine("					m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH(READCOMMITTED) ") '--加盟店商品調査方法特別対応マスタ ")
            .AppendLine("					INNER JOIN ")
            .AppendLine("					m_kameiten AS MK WITH(READCOMMITTED) ") '--加盟店マスタ ")
            .AppendLine("					ON ")
            .AppendLine("						MKSTTT.aitesaki_syubetu = @aitesaki_syubetu7 ") '--相手先種別 = 7 ")
            .AppendLine("						AND ")
            .AppendLine("						MKSTTT.aitesaki_cd = MK.keiretu_cd ") '--加特M.相手先コード = 加M.系列コード ")
            .AppendLine("						AND ")
            .AppendLine("						MK.kameiten_cd = @aitesaki_cd ") '--相手先コード ")
            .AppendLine("						AND ")
            .AppendLine("						ISNULL(MKSTTT.torikesi,0) = 0 ") '--取消 = 0 ")
            .AppendLine("			) AS SUB_MKSTTT_7 ") '--加特サブ7 ")
            .AppendLine("			ON ")
            .AppendLine("				MKSTTT.syouhin_cd = SUB_MKSTTT_7.syouhin_cd ") '--加特M.商品コード = 加特サブ7.商品コード ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tys_houhou_no = SUB_MKSTTT_7.tys_houhou_no ") '--加特M.調査方法NO = 加特サブ7.調査方法NO ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tokubetu_taiou_cd = SUB_MKSTTT_7.tokubetu_taiou_cd ") '--加特M.特別対応コード = 加特サブ7.特別対応コード
            .AppendLine("		WHERE ")
            .AppendLine("			MKSTTT.aitesaki_syubetu = @aitesaki_syubetu0 ") '--加特M.相手先種別 = 0 ")
            .AppendLine("			AND ")
            .AppendLine("			SUB_MKSTTT_1.aitesaki_cd IS NULL ") '--加特サブ1.相手先コード IS NULL ")
            .AppendLine("			AND ")
            .AppendLine("			SUB_MKSTTT_5.aitesaki_cd IS NULL ") '--加特サブ5.相手先コード IS NULL ")
            .AppendLine("			AND ")
            .AppendLine("			SUB_MKSTTT_7.aitesaki_cd IS NULL ") '--加特サブ7.相手先コード IS NULL ")
            .AppendLine("	) AS SUB_MKSTTT ") '--サブ加盟店商品調査方法特別対応M ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	( ")
            .AppendLine("		SELECT ")
            .AppendLine("			'0' AS aitesaki_syubetu, ")
            .AppendLine("			'ALL' AS aitesaki_cd, ")
            .AppendLine("			'相手先なし' AS aitesaki_name, ")
            .AppendLine("			'0' AS torikesi ")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			'1' AS aitesaki_syubetu, ")
            .AppendLine("			MK.kameiten_cd AS aitesaki_cd, ")
            .AppendLine("			MK.kameiten_mei1 AS aitesaki_name, ")
            .AppendLine("			MK.torikesi AS torikesi ")
            .AppendLine("		FROM ")
            .AppendLine("			m_kameiten AS MK WITH (READCOMMITTED) ")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			'5' AS aitesaki_syubetu, ")
            .AppendLine("			ME.eigyousyo_cd AS aitesaki_cd, ")
            .AppendLine("			ME.eigyousyo_mei AS aitesaki_name, ")
            .AppendLine("			ME.torikesi AS torikesi ")
            .AppendLine("		FROM ")
            .AppendLine("			m_eigyousyo AS ME WITH (READCOMMITTED) ")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			'7' AS aitesaki_syubetu, ")
            .AppendLine("			MKR.keiretu_cd AS aitesaki_cd, ")
            .AppendLine("			MIN(MKR.keiretu_mei) AS aitesaki_name, ")
            .AppendLine("			MIN(MKR.torikesi) AS torikesi ")
            .AppendLine("		FROM ")
            .AppendLine("			m_keiretu AS MKR WITH (READCOMMITTED) ")
            .AppendLine("		GROUP BY MKR.keiretu_cd ")
            .AppendLine("	) AS SUB ")
            .AppendLine("	ON ")
            .AppendLine("		SUB_MKSTTT.aitesaki_syubetu = SUB.aitesaki_syubetu ") '--相手先種別 ")
            .AppendLine("		AND ")
            .AppendLine("		SUB_MKSTTT.aitesaki_cd = SUB.aitesaki_cd ") '--相手先種別 ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_syouhin AS MS WITH (READCOMMITTED) ") '--商品M ")
            .AppendLine("	ON ")
            .AppendLine("		SUB_MKSTTT.syouhin_cd = MS.syouhin_cd ") '--商品コード ")
            .AppendLine("		AND ")
            .AppendLine("		MS.souko_cd = @souko_cd ") '--倉庫コード = "100" ")
            .AppendLine("		AND ")
            .AppendLine("		MS.torikesi = @torikesi ") '--取消 = 0 ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_tyousahouhou AS MT WITH (READCOMMITTED) ") '--調査方法M ")
            .AppendLine("	ON ")
            .AppendLine("		SUB_MKSTTT.tys_houhou_no = MT.tys_houhou_no ") '--調査方法NO ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_tokubetu_taiou AS MTT WITH (READCOMMITTED) ") '--特別対応M ")
            .AppendLine("	ON ")
            .AppendLine("		SUB_MKSTTT.tokubetu_taiou_cd = MTT.tokubetu_taiou_cd ") '--特別対応コード ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_kakutyou_meisyou AS MKM WITH (READCOMMITTED) ") '--拡張名称M ")
            .AppendLine("	ON ")
            .AppendLine("		SUB_MKSTTT.aitesaki_syubetu = MKM.code ") '--相手先種別 ")
            .AppendLine("		AND ")
            .AppendLine("		MKM.meisyou_syubetu = @meisyou_syubetu ") '--名称種別 ")
            .AppendLine("WHERE ")
            .AppendLine("	1 = 1 ")
            '商品コード
            If Not String.IsNullOrEmpty(dtParamList.Item("syouhin_cd").ToString) Then
                .AppendLine("	AND ")
                .AppendLine("	SUB_MKSTTT.syouhin_cd = @syouhin_cd ") '--商品コード ")
                paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtParamList.Item("syouhin_cd").ToString))
            End If
            '調査方法NO
            If Not String.IsNullOrEmpty(dtParamList.Item("tys_houhou_no").ToString) Then
                .AppendLine("	AND ")
                .AppendLine("	SUB_MKSTTT.tys_houhou_no = @tys_houhou_no ") '--調査方法NO ")
                paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 5, dtParamList.Item("tys_houhou_no")))
            End If
            '特別対応コード
            If Not String.IsNullOrEmpty(dtParamList.Item("tokubetu_taiou_cd").ToString) Then
                .AppendLine("	AND ")
                .AppendLine("	SUB_MKSTTT.tokubetu_taiou_cd = @tokubetu_taiou_cd ") '--特別対応コード ")
                paramList.Add(MakeParam("@tokubetu_taiou_cd", SqlDbType.Int, 5, dtParamList.Item("tokubetu_taiou_cd")))
            End If
            '取消
            .AppendLine("	AND ")
            .AppendLine("	SUB_MKSTTT.torikesi = @torikesi ") '--取消 ")
            '取消相手先は対象外=チェックの場合
            If dtParamList.Item("aitesakiTorikesiFlg").ToString.Equals("1") Then
                .AppendLine("	AND ")
                .AppendLine("	SUB.torikesi = @torikesi ") '--取消相手先は対象外 ")
            End If
            '\0は対象外=チェックの場合
            If dtParamList.Item("kingaku0TorikesiFlg").ToString.Equals("1") Then
                .AppendLine("	AND ")
                .AppendLine("	SUB_MKSTTT.uri_kasan_gaku <> 0 ") '--\0は対象外 ")
            End If

            '------------------From 2013.03.09  李宇追加する-----------------
            '初期値1のみ=チェックの場合、加盟店商品調査方法特別対応M.初期値　=　"1"
            If dtParamList.Item("Syokiti1Nomi").ToString.Equals("1") Then
                .AppendLine("	AND ")
                .AppendLine("	SUB_MKSTTT.syokiti = '1'")
            End If
            '------------------To   2013.03.09  李宇追加する-----------------

            '順番
            .AppendLine("ORDER BY ")
            .AppendLine("	SUB_MKSTTT.syouhin_cd ASC ") '--商品コード ")
            .AppendLine("	,SUB_MKSTTT.tys_houhou_no ASC ") '--調査方法NO ")
            .AppendLine("	,SUB_MKSTTT.tokubetu_taiou_cd ASC ") '--特別対応コード ")
        End With

        paramList.Add(MakeParam("@aitesaki_syubetu0", SqlDbType.Int, 10, 0))
        paramList.Add(MakeParam("@aitesaki_syubetu1", SqlDbType.Int, 10, 1))
        paramList.Add(MakeParam("@aitesaki_syubetu5", SqlDbType.Int, 10, 5))
        paramList.Add(MakeParam("@aitesaki_syubetu7", SqlDbType.Int, 10, 7))

        paramList.Add(MakeParam("@souko_cd", SqlDbType.VarChar, 3, "100"))
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, 0))
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.Int, 10, 23))
        paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdFrom")))

        'SQLコメント実行、戻りデータセット実装
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "TokubetuTaiouJyouhouNasiInfo", paramList.ToArray)

        '戻りデータテーブル
        Return dsReturn.Tables("TokubetuTaiouJyouhouNasiInfo")

    End Function

    ''' <summary>加盟店商品調査方法特別対応マスタ件数を取得「系列・営業所・指定無しも対象チェックボックス」=チェックの場合</summary>
    ''' <history>2012/05/23 車龍 407553の対応 追加</history>
    Public Function SelTokubetuTaiouNasiCount(ByVal dtParamList As Dictionary(Of String, String)) As Integer

        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   COUNT(SUB_MKSTTT.aitesaki_syubetu) AS count ") '--件数 ")
            .AppendLine("FROM ")
            .AppendLine("	( ")
            .AppendLine("		SELECT ")
            .AppendLine("			aitesaki_syubetu ") '--相手先種別 ")
            .AppendLine("			,aitesaki_cd ") '--相手先コード ")
            .AppendLine("			,syouhin_cd ") '--商品コード ")
            .AppendLine("			,tys_houhou_no ") '--調査方法NO ")
            .AppendLine("			,tokubetu_taiou_cd ") '--特別対応コード ")
            .AppendLine("			,torikesi ") '--取消 ")
            .AppendLine("			,kasan_syouhin_cd ") '--金額加算商品コード ")
            .AppendLine("			,syokiti ") '--初期値 ")
            .AppendLine("			,uri_kasan_gaku ") '--実請求加算金額 ")
            .AppendLine("			,koumuten_kasan_gaku ") '--工務店請求加算金額 ")
            .AppendLine("		FROM ")
            .AppendLine("			m_kamei_syouhin_tys_tokubetu_taiou WITH(READCOMMITTED) ") '--加盟店商品調査方法特別対応マスタ ")
            .AppendLine("		WHERE ")
            .AppendLine("			aitesaki_syubetu = @aitesaki_syubetu1 ") '--相手先種別 = 1 ")
            .AppendLine("			AND ")
            .AppendLine("			aitesaki_cd = @aitesaki_cd ") '--相手先コード ")
            .AppendLine("		UNION ALL ") '--==================================================================================================== ")
            .AppendLine("		SELECT ")
            .AppendLine("			MKSTTT.aitesaki_syubetu ") '--相手先種別 ")
            .AppendLine("			,MKSTTT.aitesaki_cd ") '--相手先コード ")
            .AppendLine("			,MKSTTT.syouhin_cd ") '--商品コード ")
            .AppendLine("			,MKSTTT.tys_houhou_no ") '--調査方法NO ")
            .AppendLine("			,MKSTTT.tokubetu_taiou_cd ") '--特別対応コード ")
            .AppendLine("			,MKSTTT.torikesi ") '--取消 ")
            .AppendLine("			,MKSTTT.kasan_syouhin_cd ") '--金額加算商品コード ")
            .AppendLine("			,MKSTTT.syokiti ") '--初期値 ")
            .AppendLine("			,MKSTTT.uri_kasan_gaku ") '--実請求加算金額 ")
            .AppendLine("			,MKSTTT.koumuten_kasan_gaku ") '--工務店請求加算金額 ")
            .AppendLine("		FROM ")
            .AppendLine("			m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH(READCOMMITTED) ") '--加盟店商品調査方法特別対応マスタ ")
            .AppendLine("			INNER JOIN ")
            .AppendLine("			m_kameiten AS MK WITH(READCOMMITTED) ") '--加盟店マスタ ")
            .AppendLine("			ON ")
            .AppendLine("				MKSTTT.aitesaki_syubetu = @aitesaki_syubetu5 ") '--相手先種別 = 5 ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.aitesaki_cd = MK.eigyousyo_cd ") '--加特M.相手先コード = 加M.営業所コード ")
            .AppendLine("			LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT ")
            .AppendLine("					aitesaki_syubetu ") '--相手先種別 ")
            .AppendLine("					,aitesaki_cd ") '--相手先コード ")
            .AppendLine("					,syouhin_cd ") '--商品コード ")
            .AppendLine("					,tys_houhou_no ") '--調査方法NO ")
            .AppendLine("					,tokubetu_taiou_cd ") '--特別対応コード ")
            .AppendLine("					,torikesi ") '--取消 ")
            .AppendLine("					,kasan_syouhin_cd ") '--金額加算商品コード ")
            .AppendLine("					,syokiti ") '--初期値 ")
            .AppendLine("					,uri_kasan_gaku ") '--実請求加算金額 ")
            .AppendLine("					,koumuten_kasan_gaku ") '--工務店請求加算金額 ")
            .AppendLine("				FROM ")
            .AppendLine("					m_kamei_syouhin_tys_tokubetu_taiou WITH(READCOMMITTED) ") '--加盟店商品調査方法特別対応マスタ ")
            .AppendLine("				WHERE ")
            .AppendLine("					aitesaki_syubetu = @aitesaki_syubetu1 ") '--相手先種別 = 1 ")
            .AppendLine("					AND ")
            .AppendLine("					aitesaki_cd = @aitesaki_cd ") '--相手先コード ")
            .AppendLine("					AND ")
            .AppendLine("					ISNULL(torikesi,0) = 0 ") '--取消 = 0 ")
            .AppendLine("			 ")
            .AppendLine("			) AS SUB_MKSTTT_1 ") '--加特サブ1 ")
            .AppendLine("			ON ")
            .AppendLine("				MKSTTT.syouhin_cd = SUB_MKSTTT_1.syouhin_cd ") '--加特M.商品コード = 加特サブ1.商品コード ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tys_houhou_no = SUB_MKSTTT_1.tys_houhou_no ") '--加特M.調査方法NO = 加特サブ1.調査方法NO ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tokubetu_taiou_cd = SUB_MKSTTT_1.tokubetu_taiou_cd ") '--加特M.特別対応コード = 加特サブ1.特別対応コード
            .AppendLine("		WHERE ")
            .AppendLine("			MK.kameiten_cd = @aitesaki_cd ") '--相手先コード ")
            .AppendLine("			AND ")
            .AppendLine("			SUB_MKSTTT_1.aitesaki_cd IS NULL ") '--加特サブ1.相手先コード IS NULL ")
            .AppendLine("		UNION ALL ") '--==================================================================================================== ")
            .AppendLine("		SELECT ")
            .AppendLine("			MKSTTT.aitesaki_syubetu ") '--相手先種別 ")
            .AppendLine("			,MKSTTT.aitesaki_cd ") '--相手先コード ")
            .AppendLine("			,MKSTTT.syouhin_cd ") '--商品コード ")
            .AppendLine("			,MKSTTT.tys_houhou_no ") '--調査方法NO ")
            .AppendLine("			,MKSTTT.tokubetu_taiou_cd ") '--特別対応コード ")
            .AppendLine("			,MKSTTT.torikesi ") '--取消 ")
            .AppendLine("			,MKSTTT.kasan_syouhin_cd ") '--金額加算商品コード ")
            .AppendLine("			,MKSTTT.syokiti ") '--初期値 ")
            .AppendLine("			,MKSTTT.uri_kasan_gaku ") '--実請求加算金額 ")
            .AppendLine("			,MKSTTT.koumuten_kasan_gaku ") '--工務店請求加算金額 ")
            .AppendLine("		FROM ")
            .AppendLine("			m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH(READCOMMITTED) ") '--加盟店商品調査方法特別対応マスタ ")
            .AppendLine("			INNER JOIN ")
            .AppendLine("			m_kameiten AS MK WITH(READCOMMITTED) ") '--加盟店マスタ ")
            .AppendLine("			ON ")
            .AppendLine("				MKSTTT.aitesaki_syubetu = @aitesaki_syubetu7 ") '--相手先種別 = 7 ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.aitesaki_cd = MK.keiretu_cd ") '--加特M.相手先コード = 加M.系列コード ")
            .AppendLine("			LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT ")
            .AppendLine("					aitesaki_syubetu ") '--相手先種別 ")
            .AppendLine("					,aitesaki_cd ") '--相手先コード ")
            .AppendLine("					,syouhin_cd ") '--商品コード ")
            .AppendLine("					,tys_houhou_no ") '--調査方法NO ")
            .AppendLine("					,tokubetu_taiou_cd ") '--特別対応コード ")
            .AppendLine("					,torikesi ") '--取消 ")
            .AppendLine("					,kasan_syouhin_cd ") '--金額加算商品コード ")
            .AppendLine("					,syokiti ") '--初期値 ")
            .AppendLine("					,uri_kasan_gaku ") '--実請求加算金額 ")
            .AppendLine("					,koumuten_kasan_gaku ") '--工務店請求加算金額 ")
            .AppendLine("				FROM ")
            .AppendLine("					m_kamei_syouhin_tys_tokubetu_taiou WITH(READCOMMITTED) ") '--加盟店商品調査方法特別対応マスタ ")
            .AppendLine("				WHERE ")
            .AppendLine("					aitesaki_syubetu = @aitesaki_syubetu1 ") '--相手先種別 = 1 ")
            .AppendLine("					AND ")
            .AppendLine("					aitesaki_cd = @aitesaki_cd ") '--相手先コード ")
            .AppendLine("					AND ")
            .AppendLine("					ISNULL(torikesi,0) = 0 ") '--取消 = 0 ")
            .AppendLine("			) AS SUB_MKSTTT_1 ") '--加特サブ1 ")
            .AppendLine("			ON ")
            .AppendLine("				MKSTTT.syouhin_cd = SUB_MKSTTT_1.syouhin_cd ") '--加特M.商品コード = 加特サブ1.商品コード ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tys_houhou_no = SUB_MKSTTT_1.tys_houhou_no ") '--加特M.調査方法NO = 加特サブ1.調査方法NO ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tokubetu_taiou_cd = SUB_MKSTTT_1.tokubetu_taiou_cd ") '--加特M.特別対応コード = 加特サブ1.特別対応コード
            .AppendLine("			LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT ")
            .AppendLine("					MKSTTT.aitesaki_syubetu ") '--相手先種別 ")
            .AppendLine("					,MKSTTT.aitesaki_cd ") '--相手先コード ")
            .AppendLine("					,MKSTTT.syouhin_cd ") '--商品コード ")
            .AppendLine("					,MKSTTT.tys_houhou_no ") '--調査方法NO ")
            .AppendLine("					,MKSTTT.tokubetu_taiou_cd ") '--特別対応コード ")
            .AppendLine("					,MKSTTT.torikesi ") '--取消 ")
            .AppendLine("					,MKSTTT.kasan_syouhin_cd ") '--金額加算商品コード ")
            .AppendLine("					,MKSTTT.syokiti ") '--初期値 ")
            .AppendLine("					,MKSTTT.uri_kasan_gaku ") '--実請求加算金額 ")
            .AppendLine("					,MKSTTT.koumuten_kasan_gaku ") '--工務店請求加算金額 ")
            .AppendLine("				FROM ")
            .AppendLine("					m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH(READCOMMITTED) ") '--加盟店商品調査方法特別対応マスタ ")
            .AppendLine("					INNER JOIN ")
            .AppendLine("					m_kameiten AS MK WITH(READCOMMITTED) ") '--加盟店マスタ ")
            .AppendLine("					ON ")
            .AppendLine("						MKSTTT.aitesaki_syubetu = @aitesaki_syubetu5 ") '--相手先種別 = 5 ")
            .AppendLine("						AND ")
            .AppendLine("						MKSTTT.aitesaki_cd = MK.eigyousyo_cd ") '--加特M.相手先コード = 加M.営業所コード ")
            .AppendLine("						AND ")
            .AppendLine("						MK.kameiten_cd = @aitesaki_cd ") '--相手先コード ")
            .AppendLine("						AND ")
            .AppendLine("						ISNULL(MKSTTT.torikesi,0) = 0 ") '--取消 = 0 ")
            .AppendLine("					 ")
            .AppendLine("			) AS SUB_MKSTTT_5 ") '--加特サブ5 ")
            .AppendLine("			ON ")
            .AppendLine("				MKSTTT.syouhin_cd = SUB_MKSTTT_5.syouhin_cd ") '--加特M.商品コード = 加特サブ5.商品コード ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tys_houhou_no = SUB_MKSTTT_5.tys_houhou_no ") '--加特M.調査方法NO = 加特サブ5.調査方法NO ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tokubetu_taiou_cd = SUB_MKSTTT_5.tokubetu_taiou_cd ") '--加特M.特別対応コード = 加特サブ5.特別対応コード
            .AppendLine("		WHERE ")
            .AppendLine("			MK.kameiten_cd = @aitesaki_cd ") '--相手先コード ")
            .AppendLine("			AND ")
            .AppendLine("			SUB_MKSTTT_1.aitesaki_cd IS NULL ") '--加特サブ1.相手先コード IS NULL ")
            .AppendLine("			AND ")
            .AppendLine("			SUB_MKSTTT_5.aitesaki_cd IS NULL ") '--加特サブ5.相手先コード IS NULL ")
            .AppendLine("		UNION ALL ") '--==================================================================================================== ")
            .AppendLine("		SELECT ")
            .AppendLine("			MKSTTT.aitesaki_syubetu ") '--相手先種別 ")
            .AppendLine("			,MKSTTT.aitesaki_cd ") '--相手先コード ")
            .AppendLine("			,MKSTTT.syouhin_cd ") '--商品コード ")
            .AppendLine("			,MKSTTT.tys_houhou_no ") '--調査方法NO ")
            .AppendLine("			,MKSTTT.tokubetu_taiou_cd ") '--特別対応コード ")
            .AppendLine("			,MKSTTT.torikesi ") '--取消 ")
            .AppendLine("			,MKSTTT.kasan_syouhin_cd ") '--金額加算商品コード ")
            .AppendLine("			,MKSTTT.syokiti ") '--初期値 ")
            .AppendLine("			,MKSTTT.uri_kasan_gaku ") '--実請求加算金額 ")
            .AppendLine("			,MKSTTT.koumuten_kasan_gaku ") '--工務店請求加算金額 ")
            .AppendLine("		FROM ")
            .AppendLine("			m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH(READCOMMITTED) ") '--加盟店商品調査方法特別対応マスタ ")
            .AppendLine("			LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT ")
            .AppendLine("					aitesaki_syubetu ") '--相手先種別 ")
            .AppendLine("					,aitesaki_cd ") '--相手先コード ")
            .AppendLine("					,syouhin_cd ") '--商品コード ")
            .AppendLine("					,tys_houhou_no ") '--調査方法NO ")
            .AppendLine("					,tokubetu_taiou_cd ") '--特別対応コード ")
            .AppendLine("					,torikesi ") '--取消 ")
            .AppendLine("					,kasan_syouhin_cd ") '--金額加算商品コード ")
            .AppendLine("					,syokiti ") '--初期値 ")
            .AppendLine("					,uri_kasan_gaku ") '--実請求加算金額 ")
            .AppendLine("					,koumuten_kasan_gaku ") '--工務店請求加算金額 ")
            .AppendLine("				FROM ")
            .AppendLine("					m_kamei_syouhin_tys_tokubetu_taiou WITH(READCOMMITTED) ") '--加盟店商品調査方法特別対応マスタ ")
            .AppendLine("				WHERE ")
            .AppendLine("					aitesaki_syubetu = @aitesaki_syubetu1 ") '--相手先種別 = 1 ")
            .AppendLine("					AND ")
            .AppendLine("					aitesaki_cd = @aitesaki_cd ") '--相手先コード ")
            .AppendLine("					AND ")
            .AppendLine("					ISNULL(torikesi,0) = 0 ") '--取消 = 0 ")
            .AppendLine("			) AS SUB_MKSTTT_1 ") '--加特サブ1 ")
            .AppendLine("			ON ")
            .AppendLine("				MKSTTT.syouhin_cd = SUB_MKSTTT_1.syouhin_cd ") '--加特M.商品コード = 加特サブ1.商品コード ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tys_houhou_no = SUB_MKSTTT_1.tys_houhou_no ") '--加特M.調査方法NO = 加特サブ1.調査方法NO ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tokubetu_taiou_cd = SUB_MKSTTT_1.tokubetu_taiou_cd ") '--加特M.特別対応コード = 加特サブ1.特別対応コード
            .AppendLine("			LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT ")
            .AppendLine("					MKSTTT.aitesaki_syubetu ") '--相手先種別 ")
            .AppendLine("					,MKSTTT.aitesaki_cd ") '--相手先コード ")
            .AppendLine("					,MKSTTT.syouhin_cd ") '--商品コード ")
            .AppendLine("					,MKSTTT.tys_houhou_no ") '--調査方法NO ")
            .AppendLine("					,MKSTTT.tokubetu_taiou_cd ") '--特別対応コード ")
            .AppendLine("					,MKSTTT.torikesi ") '--取消 ")
            .AppendLine("					,MKSTTT.kasan_syouhin_cd ") '--金額加算商品コード ")
            .AppendLine("					,MKSTTT.syokiti ") '--初期値 ")
            .AppendLine("					,MKSTTT.uri_kasan_gaku ") '--実請求加算金額 ")
            .AppendLine("					,MKSTTT.koumuten_kasan_gaku ") '--工務店請求加算金額 ")
            .AppendLine("				FROM ")
            .AppendLine("					m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH(READCOMMITTED) ") '--加盟店商品調査方法特別対応マスタ ")
            .AppendLine("					INNER JOIN ")
            .AppendLine("					m_kameiten AS MK WITH(READCOMMITTED) ") '--加盟店マスタ ")
            .AppendLine("					ON ")
            .AppendLine("						MKSTTT.aitesaki_syubetu = @aitesaki_syubetu5 ") '--相手先種別 = 5 ")
            .AppendLine("						AND ")
            .AppendLine("						MKSTTT.aitesaki_cd = MK.eigyousyo_cd ") '--加特M.相手先コード = 加M.営業所コード ")
            .AppendLine("						AND ")
            .AppendLine("						MK.kameiten_cd = @aitesaki_cd ") '--相手先コード ")
            .AppendLine("						AND ")
            .AppendLine("						ISNULL(MKSTTT.torikesi,0) = 0 ") '--取消 = 0 ")
            .AppendLine("			) AS SUB_MKSTTT_5 ") '--加特サブ5 ")
            .AppendLine("			ON ")
            .AppendLine("				MKSTTT.syouhin_cd = SUB_MKSTTT_5.syouhin_cd ") '--加特M.商品コード = 加特サブ5.商品コード ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tys_houhou_no = SUB_MKSTTT_5.tys_houhou_no ") '--加特M.調査方法NO = 加特サブ5.調査方法NO ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tokubetu_taiou_cd = SUB_MKSTTT_5.tokubetu_taiou_cd ") '--加特M.特別対応コード = 加特サブ5.特別対応コード
            .AppendLine("				 ")
            .AppendLine("			LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT ")
            .AppendLine("					MKSTTT.aitesaki_syubetu ") '--相手先種別 ")
            .AppendLine("					,MKSTTT.aitesaki_cd ") '--相手先コード ")
            .AppendLine("					,MKSTTT.syouhin_cd ") '--商品コード ")
            .AppendLine("					,MKSTTT.tys_houhou_no ") '--調査方法NO ")
            .AppendLine("					,MKSTTT.tokubetu_taiou_cd ") '--特別対応コード ")
            .AppendLine("					,MKSTTT.torikesi ") '--取消 ")
            .AppendLine("					,MKSTTT.kasan_syouhin_cd ") '--金額加算商品コード ")
            .AppendLine("					,MKSTTT.syokiti ") '--初期値 ")
            .AppendLine("					,MKSTTT.uri_kasan_gaku ") '--実請求加算金額 ")
            .AppendLine("					,MKSTTT.koumuten_kasan_gaku ") '--工務店請求加算金額 ")
            .AppendLine("				FROM ")
            .AppendLine("					m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH(READCOMMITTED) ") '--加盟店商品調査方法特別対応マスタ ")
            .AppendLine("					INNER JOIN ")
            .AppendLine("					m_kameiten AS MK WITH(READCOMMITTED) ") '--加盟店マスタ ")
            .AppendLine("					ON ")
            .AppendLine("						MKSTTT.aitesaki_syubetu = @aitesaki_syubetu7 ") '--相手先種別 = 7 ")
            .AppendLine("						AND ")
            .AppendLine("						MKSTTT.aitesaki_cd = MK.keiretu_cd ") '--加特M.相手先コード = 加M.系列コード ")
            .AppendLine("						AND ")
            .AppendLine("						MK.kameiten_cd = @aitesaki_cd ") '--相手先コード ")
            .AppendLine("						AND ")
            .AppendLine("						ISNULL(MKSTTT.torikesi,0) = 0 ") '--取消 = 0 ")
            .AppendLine("			) AS SUB_MKSTTT_7 ") '--加特サブ7 ")
            .AppendLine("			ON ")
            .AppendLine("				MKSTTT.syouhin_cd = SUB_MKSTTT_7.syouhin_cd ") '--加特M.商品コード = 加特サブ7.商品コード ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tys_houhou_no = SUB_MKSTTT_7.tys_houhou_no ") '--加特M.調査方法NO = 加特サブ7.調査方法NO ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tokubetu_taiou_cd = SUB_MKSTTT_7.tokubetu_taiou_cd ") '--加特M.特別対応コード = 加特サブ7.特別対応コード
            .AppendLine("		WHERE ")
            .AppendLine("			MKSTTT.aitesaki_syubetu = @aitesaki_syubetu0 ") '--加特M.相手先種別 = 0 ")
            .AppendLine("			AND ")
            .AppendLine("			SUB_MKSTTT_1.aitesaki_cd IS NULL ") '--加特サブ1.相手先コード IS NULL ")
            .AppendLine("			AND ")
            .AppendLine("			SUB_MKSTTT_5.aitesaki_cd IS NULL ") '--加特サブ5.相手先コード IS NULL ")
            .AppendLine("			AND ")
            .AppendLine("			SUB_MKSTTT_7.aitesaki_cd IS NULL ") '--加特サブ7.相手先コード IS NULL ")
            .AppendLine("	) AS SUB_MKSTTT ") '--サブ加盟店商品調査方法特別対応M ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	( ")
            .AppendLine("		SELECT ")
            .AppendLine("			'0' AS aitesaki_syubetu, ")
            .AppendLine("			'ALL' AS aitesaki_cd, ")
            .AppendLine("			'相手先なし' AS aitesaki_name, ")
            .AppendLine("			'0' AS torikesi ")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			'1' AS aitesaki_syubetu, ")
            .AppendLine("			MK.kameiten_cd AS aitesaki_cd, ")
            .AppendLine("			MK.kameiten_mei1 AS aitesaki_name, ")
            .AppendLine("			MK.torikesi AS torikesi ")
            .AppendLine("		FROM ")
            .AppendLine("			m_kameiten AS MK WITH (READCOMMITTED) ")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			'5' AS aitesaki_syubetu, ")
            .AppendLine("			ME.eigyousyo_cd AS aitesaki_cd, ")
            .AppendLine("			ME.eigyousyo_mei AS aitesaki_name, ")
            .AppendLine("			ME.torikesi AS torikesi ")
            .AppendLine("		FROM ")
            .AppendLine("			m_eigyousyo AS ME WITH (READCOMMITTED) ")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			'7' AS aitesaki_syubetu, ")
            .AppendLine("			MKR.keiretu_cd AS aitesaki_cd, ")
            .AppendLine("			MIN(MKR.keiretu_mei) AS aitesaki_name, ")
            .AppendLine("			MIN(MKR.torikesi) AS torikesi ")
            .AppendLine("		FROM ")
            .AppendLine("			m_keiretu AS MKR WITH (READCOMMITTED) ")
            .AppendLine("		GROUP BY MKR.keiretu_cd ")
            .AppendLine("	) AS SUB ")
            .AppendLine("	ON ")
            .AppendLine("		SUB_MKSTTT.aitesaki_syubetu = SUB.aitesaki_syubetu ") '--相手先種別 ")
            .AppendLine("		AND ")
            .AppendLine("		SUB_MKSTTT.aitesaki_cd = SUB.aitesaki_cd ") '--相手先種別 ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_syouhin AS MS WITH (READCOMMITTED) ") '--商品M ")
            .AppendLine("	ON ")
            .AppendLine("		SUB_MKSTTT.syouhin_cd = MS.syouhin_cd ") '--商品コード ")
            .AppendLine("		AND ")
            .AppendLine("		MS.souko_cd = @souko_cd ") '--倉庫コード = "100" ")
            .AppendLine("		AND ")
            .AppendLine("		MS.torikesi = @torikesi ") '--取消 = 0 ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_tyousahouhou AS MT WITH (READCOMMITTED) ") '--調査方法M ")
            .AppendLine("	ON ")
            .AppendLine("		SUB_MKSTTT.tys_houhou_no = MT.tys_houhou_no ") '--調査方法NO ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_tokubetu_taiou AS MTT WITH (READCOMMITTED) ") '--特別対応M ")
            .AppendLine("	ON ")
            .AppendLine("		SUB_MKSTTT.tokubetu_taiou_cd = MTT.tokubetu_taiou_cd ") '--特別対応コード ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_kakutyou_meisyou AS MKM WITH (READCOMMITTED) ") '--拡張名称M ")
            .AppendLine("	ON ")
            .AppendLine("		SUB_MKSTTT.aitesaki_syubetu = MKM.code ") '--相手先種別 ")
            .AppendLine("		AND ")
            .AppendLine("		MKM.meisyou_syubetu = @meisyou_syubetu ") '--名称種別 ")
            .AppendLine("WHERE ")
            .AppendLine("	1 = 1 ")
            '商品コード
            If Not String.IsNullOrEmpty(dtParamList.Item("syouhin_cd").ToString) Then
                .AppendLine("	AND ")
                .AppendLine("	SUB_MKSTTT.syouhin_cd = @syouhin_cd ") '--商品コード ")
                paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtParamList.Item("syouhin_cd").ToString))
            End If
            '調査方法NO
            If Not String.IsNullOrEmpty(dtParamList.Item("tys_houhou_no").ToString) Then
                .AppendLine("	AND ")
                .AppendLine("	SUB_MKSTTT.tys_houhou_no = @tys_houhou_no ") '--調査方法NO ")
                paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 5, dtParamList.Item("tys_houhou_no")))
            End If
            '特別対応コード
            If Not String.IsNullOrEmpty(dtParamList.Item("tokubetu_taiou_cd").ToString) Then
                .AppendLine("	AND ")
                .AppendLine("	SUB_MKSTTT.tokubetu_taiou_cd = @tokubetu_taiou_cd ") '--特別対応コード ")
                paramList.Add(MakeParam("@tokubetu_taiou_cd", SqlDbType.Int, 5, dtParamList.Item("tokubetu_taiou_cd")))
            End If
            '取消
            .AppendLine("	AND ")
            .AppendLine("	SUB_MKSTTT.torikesi = @torikesi ") '--取消 ")
            '取消相手先は対象外=チェックの場合
            If dtParamList.Item("aitesakiTorikesiFlg").ToString.Equals("1") Then
                .AppendLine("	AND ")
                .AppendLine("	SUB.torikesi = @torikesi ") '--取消相手先は対象外 ")
            End If
            '\0は対象外=チェックの場合
            If dtParamList.Item("kingaku0TorikesiFlg").ToString.Equals("1") Then
                .AppendLine("	AND ")
                .AppendLine("	SUB_MKSTTT.uri_kasan_gaku <> 0 ") '--\0は対象外 ")
            End If

            '------------------From 2013.03.09  李宇追加する-----------------
            '初期値1のみ=チェックの場合、加盟店商品調査方法特別対応M.初期値　=　"1"
            If dtParamList.Item("Syokiti1Nomi").ToString.Equals("1") Then
                .AppendLine("	AND ")
                .AppendLine("	SUB_MKSTTT.syokiti = '1'")
            End If
            '------------------To   2013.03.09  李宇追加する-----------------

        End With

        paramList.Add(MakeParam("@aitesaki_syubetu0", SqlDbType.Int, 10, 0))
        paramList.Add(MakeParam("@aitesaki_syubetu1", SqlDbType.Int, 10, 1))
        paramList.Add(MakeParam("@aitesaki_syubetu5", SqlDbType.Int, 10, 5))
        paramList.Add(MakeParam("@aitesaki_syubetu7", SqlDbType.Int, 10, 7))

        paramList.Add(MakeParam("@souko_cd", SqlDbType.VarChar, 3, "100"))
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, 0))
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.Int, 10, 23))
        paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdFrom")))

        'SQLコメント実行、戻りデータセット実装
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "TokubetuTaiouJyouhouNasiCount", paramList.ToArray)

        '戻りデータテーブル
        Return Convert.ToInt32(dsReturn.Tables("TokubetuTaiouJyouhouNasiCount").Rows(0).Item(0))

    End Function

    ''' <summary>
    ''' 旧様式名称取得
    ''' </summary>
    ''' <returns>旧様式名称</returns>
    Public Function SelStyleMeisyou() As Object
        Try
            Dim strSql As String = "SELECT meisyou FROM m_meisyou  WITH(READCOMMITTED) WHERE meisyou_syubetu='80' and  code=0 "
            Dim obj As Object = ExecuteScalar(connStr, CommandType.Text, strSql)

            If obj Is Nothing Then
                Return String.Empty
            Else
                Return obj.ToString
            End If
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

End Class

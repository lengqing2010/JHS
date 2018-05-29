Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper

''' <summary>
''' 請求先注意事項
''' </summary>
''' <history>
''' <para>2011/06/13　車龍(大連情報システム部)　新規作成</para>
''' </history>
Public Class SeikyuuSakiTyuuijikouDataAccess

    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' 請求先名を取得する
    ''' </summary>
    ''' <history>
    ''' <para>2011/06/14　車龍(大連情報システム部)　新規作成</para>
    ''' </history>
    Public Function SelSeikyuusakiMei(ByVal strSeikyuusakiKbn As String, ByVal strSeikyuusakiCd As String, ByVal strSeikyuusakiBrc As String) As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	VSS.seikyuu_saki_mei ")
            .AppendLine("FROM ")
            .AppendLine("	v_seikyuu_saki_info AS VSS WITH(READCOMMITTED) ")
            .AppendLine("	INNER JOIN ")
            .AppendLine("	m_seikyuu_saki AS MSS WITH(READCOMMITTED) ")
            .AppendLine("	ON ")
            .AppendLine("		VSS.seikyuu_saki_cd = MSS.seikyuu_saki_cd ")
            .AppendLine("		AND ")
            .AppendLine("		VSS.seikyuu_saki_kbn = MSS.seikyuu_saki_kbn ")
            .AppendLine("		AND ")
            .AppendLine("		VSS.seikyuu_saki_brc = MSS.seikyuu_saki_brc ")
            .AppendLine("WHERE ")
            .AppendLine("	VSS.seikyuu_saki_kbn = @seikyuu_saki_kbn ")
            .AppendLine("	AND ")
            .AppendLine("	VSS.seikyuu_saki_cd = @seikyuu_saki_cd ")
            .AppendLine("	AND ")
            .AppendLine("	VSS.seikyuu_saki_brc = @seikyuu_saki_brc ")
        End With

        'パラメータ
        paramList.Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.Char, 1, strSeikyuusakiKbn))
        paramList.Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 5, strSeikyuusakiCd))
        paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 2, strSeikyuusakiBrc))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtSeikyuusakiMei", paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function

    ''' <summary>
    ''' 種別情報を取得する
    ''' </summary>
    ''' <history>
    ''' <para>2011/06/14　車龍(大連情報システム部)　新規作成</para>
    ''' </history>
    Public Function SelSyubetu() As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT  ")
            .AppendLine("	code ")
            .AppendLine("	,(LTRIM(STR(code)) + '：' + meisyou) AS meisyou ")
            .AppendLine("FROM  ")
            .AppendLine("	m_meisyou WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	meisyou_syubetu = @meisyou_syubetu ")
            .AppendLine("ORDER BY ")
            .AppendLine("	code ")
        End With

        'パラメータ
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.Char, 2, "56"))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtSyubetu", paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function








    ''' <summary>
    ''' 請求先注意事項情報を取得する
    ''' </summary>
    ''' <history>
    ''' <para>2011/06/14　車龍(大連情報システム部)　新規作成</para>
    ''' </history>
    Public Function SelSeikyuuSakiTyuuijikouInfo(ByVal Param As Dictionary(Of String, String)) As SeikyuuSakiTyuuijikouDataSet.SeikyuuSakiTyuuijikouInfoTableDataTable

        'DataSetインスタンスの生成
        Dim dsReturn As New SeikyuuSakiTyuuijikouDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT  ")
            '検索件数
            If Param("KensakuKensuu").Trim.Equals("10") Then
                .AppendLine("   TOP 10 ")
            ElseIf Param("KensakuKensuu").Trim.Equals("100") Then
                .AppendLine("   TOP 100 ")
            End If
            .AppendLine("	CASE ")
            .AppendLine("		WHEN MSST.seikyuu_saki_kbn = '0' THEN ")
            .AppendLine("			'加' ")
            .AppendLine("		WHEN MSST.seikyuu_saki_kbn = '1' THEN ")
            .AppendLine("			'調' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'' ")
            .AppendLine("	END + '：' + MSST.seikyuu_saki_cd + '-' + MSST.seikyuu_saki_brc AS _seikyuu_saki_cd ")  '--請CD ")
            .AppendLine("	,ISNULL(VSS.seikyuu_saki_mei,'') AS seikyuu_saki_mei ")  '--請求先名 ")
            .AppendLine("	,MSST.nyuuryoku_no ")  '--入力№ ")
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN MSST.torikesi = 0 THEN ")
            .AppendLine("			'' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'取消' ")
            .AppendLine("	END AS _torikesi ")  '--取消 ")
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(CONVERT(VARCHAR(10),MSST.syubetu_cd),'') + '：' + ISNULL(MM.meisyou,'') = '：' THEN ")
            .AppendLine("			'' ")
            .AppendLine("		ELSE ")
            .AppendLine("			ISNULL(CONVERT(VARCHAR(10),MSST.syubetu_cd),'') + '：' + ISNULL(MM.meisyou,'') ")
            .AppendLine("	END AS _syubetu_cd ")  '--種別コード ")
            .AppendLine("	,ISNULL(MSST.syousai,'') AS syousai ")  '--詳細 ")
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN MSST.jyuyodo = 2 THEN ")
            .AppendLine("			'高' ")
            .AppendLine("		WHEN MSST.jyuyodo = 1 THEN ")
            .AppendLine("			'中' ")
            .AppendLine("		WHEN MSST.jyuyodo = 0 THEN ")
            .AppendLine("			'低' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'' ")
            .AppendLine("	END AS _jyuyodo ")  '--重要度 ")
            .AppendLine("	,ISNULL(MSS.seikyuu_sime_date,'') AS _seikyuu_sime_date ")  '--請求締め日 ")
            .AppendLine("	,ISNULL(MSS.seikyuusyo_hittyk_date,'') AS _seikyuusyo_hittyk_date ")  '--請求書必着日 ")
            .AppendLine(" ")
            .AppendLine("	,MSST.seikyuu_saki_cd ")  '--請求先コード ")
            .AppendLine("	,MSST.seikyuu_saki_brc ")  '--請求先枝番 ")
            .AppendLine("	,MSST.seikyuu_saki_kbn ")  '--請求先区分 ")
            .AppendLine("	,MSST.torikesi ")  '--取消 ")
            .AppendLine("	,ISNULL(MSST.syubetu_cd,-1) AS syubetu_cd ")  '--種別コード ")
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MSST.jyuyodo,-1) = -1 THEN ")
            .AppendLine("			-1 ")
            .AppendLine("		WHEN MSST.jyuyodo > 2 THEN ")
            .AppendLine("			-1 ")
            .AppendLine("		ELSE ")
            .AppendLine("			MSST.jyuyodo ")
            .AppendLine("	END AS jyuyodo ")  '--重要度 ")
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN LTRIM(ISNULL(MSS.seikyuu_sime_date,'')) = '' THEN ")
            .AppendLine("			'-1' ")
            .AppendLine("		WHEN IsNumeric(MSS.seikyuu_sime_date) = 1  THEN ")
            .AppendLine("			RIGHT('00'+MSS.seikyuu_sime_date,2) ")
            .AppendLine("		ELSE ")
            .AppendLine("			'100' ")
            .AppendLine("	END AS seikyuu_sime_date ")  '--請求締め日 ")
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN LTRIM(ISNULL(MSS.seikyuusyo_hittyk_date,'')) = '' THEN ")
            .AppendLine("			'-1' ")
            .AppendLine("		WHEN IsNumeric(MSS.seikyuusyo_hittyk_date) = 1  THEN ")
            .AppendLine("			RIGHT('00'+MSS.seikyuusyo_hittyk_date,2) ")
            .AppendLine("		ELSE ")
            .AppendLine("			'100' ")
            .AppendLine("	END AS seikyuusyo_hittyk_date ")  '--請求書必着日 ")
            .AppendLine("		 ")
            .AppendLine("FROM  ")
            .AppendLine("	m_seikyuu_saki_tyuuijikou AS MSST WITH(READCOMMITTED) ")  '--請求先注意事項マスタ ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_seikyuu_saki AS MSS WITH(READCOMMITTED) ")  '--請求先マスタ ")
            .AppendLine("	ON ")
            .AppendLine("		MSST.seikyuu_saki_cd = MSS.seikyuu_saki_cd ")
            .AppendLine("		AND ")
            .AppendLine("		MSST.seikyuu_saki_brc = MSS.seikyuu_saki_brc ")
            .AppendLine("		AND ")
            .AppendLine("		MSST.seikyuu_saki_kbn = MSS.seikyuu_saki_kbn ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	v_seikyuu_saki_info AS VSS WITH(READCOMMITTED) ")  '--請求先インフォビュー ")
            .AppendLine("	ON ")
            .AppendLine("		MSST.seikyuu_saki_cd = VSS.seikyuu_saki_cd ")
            .AppendLine("		AND ")
            .AppendLine("		MSST.seikyuu_saki_brc = VSS.seikyuu_saki_brc ")
            .AppendLine("		AND ")
            .AppendLine("		MSST.seikyuu_saki_kbn = VSS.seikyuu_saki_kbn ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_meisyou AS MM WITH(READCOMMITTED) ")  '--名称マスタ ")
            .AppendLine("	ON ")
            .AppendLine("		MSST.syubetu_cd = MM.code ")
            .AppendLine("		AND ")
            .AppendLine("		MM.meisyou_syubetu = '56' ")
            .AppendLine("WHERE ")
            .AppendLine("	1 = 1 ")
            '請求先区分
            If Not Param("SeikyuusakiKbn").Trim.Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MSST.seikyuu_saki_kbn = @seikyuu_saki_kbn ")
                paramList.Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.Char, 1, Param("SeikyuusakiKbn").Trim))
            End If
            '請求先コード
            If Not Param("SeikyuusakiCd").Trim.Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MSST.seikyuu_saki_cd = @seikyuu_saki_cd ")
                paramList.Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 5, Param("SeikyuusakiCd").Trim))
            End If
            '請求先枝番
            If Not Param("SeikyuusakiBrc").Trim.Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MSST.seikyuu_saki_brc = @seikyuu_saki_brc ")
                paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 2, Param("SeikyuusakiBrc").Trim))
            End If
            '種別コード
            If Not Param("SyubetuCd").Trim.Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MSST.syubetu_cd = @syubetu_cd ")
                paramList.Add(MakeParam("@syubetu_cd", SqlDbType.Int, 8, CInt(Param("SyubetuCd").Trim)))
            End If
            '重要度
            If Not Param("Jyuuyoudo").Trim.Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MSST.jyuyodo = @jyuyodo ")
                paramList.Add(MakeParam("@jyuyodo", SqlDbType.Int, 8, CInt(Param("Jyuuyoudo").Trim)))
            End If
            '請求締め日
            If Not Param("SeikyuuSimeDate").Trim.Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MSS.seikyuu_sime_date = @seikyuu_sime_date ")
                paramList.Add(MakeParam("@seikyuu_sime_date", SqlDbType.VarChar, 2, Param("SeikyuuSimeDate").Trim))
            End If
            '請求書必着日
            If Not Param("SeikyuusyoHittykDate").Trim.Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MSS.seikyuusyo_hittyk_date = @seikyuusyo_hittyk_date ")
                paramList.Add(MakeParam("@seikyuusyo_hittyk_date", SqlDbType.VarChar, 2, Param("SeikyuusyoHittykDate").Trim))
            End If
            '取消は検索対象外
            If Not Param("KensakuTaisyouGai").Trim.Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MSST.torikesi = @torikesi ")
                paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 8, CInt(Param("KensakuTaisyouGai").Trim)))
            End If
            .AppendLine("ORDER BY  ")
            .AppendLine("	MSST.seikyuu_saki_kbn ASC ")
            .AppendLine("	,MSST.seikyuu_saki_cd ASC ")
            .AppendLine("	,MSST.seikyuu_saki_brc ASC ")
            .AppendLine("	,MSST.nyuuryoku_no ASC ")
        End With

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, dsReturn.SeikyuuSakiTyuuijikouInfoTable.TableName, paramList.ToArray)

        Return dsReturn.SeikyuuSakiTyuuijikouInfoTable
    End Function

    ''' <summary>
    ''' 請求先注意事項件数を取得する
    ''' </summary>
    ''' <history>
    ''' <para>2011/06/14　車龍(大連情報システム部)　新規作成</para>
    ''' </history>
    Public Function SelSeikyuuSakiTyuuijikouCount(ByVal Param As Dictionary(Of String, String)) As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT  ")
            .AppendLine("   COUNT(MSST.seikyuu_saki_cd) AS _count")
            .AppendLine("FROM  ")
            .AppendLine("	m_seikyuu_saki_tyuuijikou AS MSST WITH(READCOMMITTED) ")  '--請求先注意事項マスタ ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_seikyuu_saki AS MSS WITH(READCOMMITTED) ")  '--請求先マスタ ")
            .AppendLine("	ON ")
            .AppendLine("		MSST.seikyuu_saki_cd = MSS.seikyuu_saki_cd ")
            .AppendLine("		AND ")
            .AppendLine("		MSST.seikyuu_saki_brc = MSS.seikyuu_saki_brc ")
            .AppendLine("		AND ")
            .AppendLine("		MSST.seikyuu_saki_kbn = MSS.seikyuu_saki_kbn ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	v_seikyuu_saki_info AS VSS WITH(READCOMMITTED) ")  '--請求先インフォビュー ")
            .AppendLine("	ON ")
            .AppendLine("		MSST.seikyuu_saki_cd = VSS.seikyuu_saki_cd ")
            .AppendLine("		AND ")
            .AppendLine("		MSST.seikyuu_saki_brc = VSS.seikyuu_saki_brc ")
            .AppendLine("		AND ")
            .AppendLine("		MSST.seikyuu_saki_kbn = VSS.seikyuu_saki_kbn ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_meisyou AS MM WITH(READCOMMITTED) ")  '--名称マスタ ")
            .AppendLine("	ON ")
            .AppendLine("		MSST.syubetu_cd = MM.code ")
            .AppendLine("		AND ")
            .AppendLine("		MM.meisyou_syubetu = '56' ")
            .AppendLine("WHERE ")
            .AppendLine("	1 = 1 ")
            '請求先区分
            If Not Param("SeikyuusakiKbn").Trim.Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MSST.seikyuu_saki_kbn = @seikyuu_saki_kbn ")
                paramList.Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.Char, 1, Param("SeikyuusakiKbn").Trim))
            End If
            '請求先コード
            If Not Param("SeikyuusakiCd").Trim.Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MSST.seikyuu_saki_cd = @seikyuu_saki_cd ")
                paramList.Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 5, Param("SeikyuusakiCd").Trim))
            End If
            '請求先枝番
            If Not Param("SeikyuusakiBrc").Trim.Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MSST.seikyuu_saki_brc = @seikyuu_saki_brc ")
                paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 2, Param("SeikyuusakiBrc").Trim))
            End If
            '種別コード
            If Not Param("SyubetuCd").Trim.Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MSST.syubetu_cd = @syubetu_cd ")
                paramList.Add(MakeParam("@syubetu_cd", SqlDbType.Int, 8, Param("SyubetuCd").Trim))
            End If
            '重要度
            If Not Param("Jyuuyoudo").Trim.Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MSST.jyuyodo = @jyuyodo ")
                paramList.Add(MakeParam("@jyuyodo", SqlDbType.Int, 8, Param("Jyuuyoudo").Trim))
            End If
            '請求締め日
            If Not Param("SeikyuuSimeDate").Trim.Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MSS.seikyuu_sime_date = @seikyuu_sime_date ")
                paramList.Add(MakeParam("@seikyuu_sime_date", SqlDbType.VarChar, 2, Param("SeikyuuSimeDate").Trim))
            End If
            '請求書必着日
            If Not Param("SeikyuusyoHittykDate").Trim.Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MSS.seikyuusyo_hittyk_date = @seikyuusyo_hittyk_date ")
                paramList.Add(MakeParam("@seikyuusyo_hittyk_date", SqlDbType.VarChar, 2, Param("SeikyuusyoHittykDate").Trim))
            End If
            '取消は検索対象外
            If Not Param("KensakuTaisyouGai").Trim.Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MSST.torikesi = @torikesi ")
                paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 8, Param("KensakuTaisyouGai").Trim))
            End If
        End With

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtCount", paramList.ToArray)

        Return dsReturn.Tables(0)
    End Function

    ''' <summary>
    ''' 請求先注意事項CSVを取得する
    ''' </summary>
    ''' <history>
    ''' <para>2011/06/14　車龍(大連情報システム部)　新規作成</para>
    ''' </history>
    Public Function SelSeikyuuSakiTyuuijikouCSV(ByVal Param As Dictionary(Of String, String)) As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT  ")
            .AppendLine("	MSST.seikyuu_saki_cd ")  '--請求先コード ")
            .AppendLine("	,MSST.seikyuu_saki_brc ")  '--請求先枝番 ")
            .AppendLine("	,MSST.seikyuu_saki_kbn ")  '--請求先区分 ")
            .AppendLine("	,ISNULL(VSS.seikyuu_saki_mei,'') AS seikyuu_saki_mei ")  '--請求先名 ")
            .AppendLine("	,MSST.nyuuryoku_no ")  '--入力№ ")
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN MSST.torikesi = 0 THEN ")
            .AppendLine("			'' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'取消' ")
            .AppendLine("	END AS torikesi ")  '--取消 ")
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(CONVERT(VARCHAR(10),MSST.syubetu_cd),'') + '：' + ISNULL(MM.meisyou,'') = '：' THEN ")
            .AppendLine("			'' ")
            .AppendLine("		ELSE ")
            .AppendLine("			ISNULL(CONVERT(VARCHAR(10),MSST.syubetu_cd),'') + '：' + ISNULL(MM.meisyou,'') ")
            .AppendLine("	END AS _syubetu_cd")  '--種別コード ")
            .AppendLine("	,ISNULL(MSST.syousai,'') AS syousai ")  '--詳細 ")
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN MSST.jyuyodo = 2 THEN ")
            .AppendLine("			'高' ")
            .AppendLine("		WHEN MSST.jyuyodo = 1 THEN ")
            .AppendLine("			'中' ")
            .AppendLine("		WHEN MSST.jyuyodo = 0 THEN ")
            .AppendLine("			'低' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'' ")
            .AppendLine("	END AS jyuyodo ")  '--重要度 ")
            .AppendLine("	,ISNULL(MSS.seikyuu_sime_date,'') AS seikyuu_sime_date ")  '--請求締め日 ")
            .AppendLine("	,ISNULL(MSS.seikyuusyo_hittyk_date,'') AS seikyuusyo_hittyk_date ")  '--請求書必着日 ")
            .AppendLine("	,ISNULL(MSST.add_login_user_id,'') AS add_login_user_id ")  '--登録ID ")
            .AppendLine("	,MSST.add_datetime ")  '--登録日時 ")
            .AppendLine("	,ISNULL(MSST.upd_login_user_id,'') AS upd_login_user_id ")  '--更新ID ")
            .AppendLine("	,MSST.upd_datetime ")  '--更新日時 ")
            .AppendLine("FROM  ")
            .AppendLine("	m_seikyuu_saki_tyuuijikou AS MSST WITH(READCOMMITTED) ")  '--請求先注意事項マスタ ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_seikyuu_saki AS MSS WITH(READCOMMITTED) ")  '--請求先マスタ ")
            .AppendLine("	ON ")
            .AppendLine("		MSST.seikyuu_saki_cd = MSS.seikyuu_saki_cd ")
            .AppendLine("		AND ")
            .AppendLine("		MSST.seikyuu_saki_brc = MSS.seikyuu_saki_brc ")
            .AppendLine("		AND ")
            .AppendLine("		MSST.seikyuu_saki_kbn = MSS.seikyuu_saki_kbn ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	v_seikyuu_saki_info AS VSS WITH(READCOMMITTED) ")  '--請求先インフォビュー ")
            .AppendLine("	ON ")
            .AppendLine("		MSST.seikyuu_saki_cd = VSS.seikyuu_saki_cd ")
            .AppendLine("		AND ")
            .AppendLine("		MSST.seikyuu_saki_brc = VSS.seikyuu_saki_brc ")
            .AppendLine("		AND ")
            .AppendLine("		MSST.seikyuu_saki_kbn = VSS.seikyuu_saki_kbn ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_meisyou AS MM WITH(READCOMMITTED) ")  '--名称マスタ ")
            .AppendLine("	ON ")
            .AppendLine("		MSST.syubetu_cd = MM.code ")
            .AppendLine("		AND ")
            .AppendLine("		MM.meisyou_syubetu = '56' ")
            .AppendLine("WHERE ")
            .AppendLine("	1 = 1 ")
            '請求先区分
            If Not Param("SeikyuusakiKbn").Trim.Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MSST.seikyuu_saki_kbn = @seikyuu_saki_kbn ")
                paramList.Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.Char, 1, Param("SeikyuusakiKbn").Trim))
            End If
            '請求先コード
            If Not Param("SeikyuusakiCd").Trim.Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MSST.seikyuu_saki_cd = @seikyuu_saki_cd ")
                paramList.Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 5, Param("SeikyuusakiCd").Trim))
            End If
            '請求先枝番
            If Not Param("SeikyuusakiBrc").Trim.Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MSST.seikyuu_saki_brc = @seikyuu_saki_brc ")
                paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 2, Param("SeikyuusakiBrc").Trim))
            End If
            '種別コード
            If Not Param("SyubetuCd").Trim.Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MSST.syubetu_cd = @syubetu_cd ")
                paramList.Add(MakeParam("@syubetu_cd", SqlDbType.Int, 8, Param("SyubetuCd").Trim))
            End If
            '重要度
            If Not Param("Jyuuyoudo").Trim.Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MSST.jyuyodo = @jyuyodo ")
                paramList.Add(MakeParam("@jyuyodo", SqlDbType.Int, 8, Param("Jyuuyoudo").Trim))
            End If
            '請求締め日
            If Not Param("SeikyuuSimeDate").Trim.Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MSS.seikyuu_sime_date = @seikyuu_sime_date ")
                paramList.Add(MakeParam("@seikyuu_sime_date", SqlDbType.VarChar, 2, Param("SeikyuuSimeDate").Trim))
            End If
            '請求書必着日
            If Not Param("SeikyuusyoHittykDate").Trim.Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MSS.seikyuusyo_hittyk_date = @seikyuusyo_hittyk_date ")
                paramList.Add(MakeParam("@seikyuusyo_hittyk_date", SqlDbType.VarChar, 2, Param("SeikyuusyoHittykDate").Trim))
            End If
            '取消は検索対象外
            If Not Param("KensakuTaisyouGai").Trim.Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MSST.torikesi = @torikesi ")
                paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 8, Param("KensakuTaisyouGai").Trim))
            End If
        End With

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtCSV", paramList.ToArray)

        Return dsReturn.Tables(0)
    End Function

    ''' <summary>
    ''' 請求先情報を取得する
    ''' </summary>
    ''' <history>
    ''' <para>2011/06/14　車龍(大連情報システム部)　新規作成</para>
    ''' </history>
    Public Function SelSeikyuusakiInfo(ByVal strSeikyuusakiKbn As String, ByVal strSeikyuusakiCd As String, ByVal strSeikyuusakiBrc As String) As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	ISNULL(VSS.seikyuu_saki_mei ,'') AS seikyuu_saki_mei ")
            .AppendLine("	,ISNULL(MSS.seikyuu_sime_date ,'') AS seikyuu_sime_date ")
            .AppendLine("	,ISNULL(MSS.seikyuusyo_hittyk_date ,'') AS seikyuusyo_hittyk_date ")
            .AppendLine("FROM ")
            .AppendLine("	m_seikyuu_saki AS MSS WITH(READCOMMITTED) ")
            .AppendLine("	INNER JOIN ")
            .AppendLine("	v_seikyuu_saki_info AS VSS WITH(READCOMMITTED) ")
            .AppendLine("	ON ")
            .AppendLine("		VSS.seikyuu_saki_cd = MSS.seikyuu_saki_cd ")
            .AppendLine("		AND ")
            .AppendLine("		VSS.seikyuu_saki_kbn = MSS.seikyuu_saki_kbn ")
            .AppendLine("		AND ")
            .AppendLine("		VSS.seikyuu_saki_brc = MSS.seikyuu_saki_brc ")
            .AppendLine("WHERE ")
            .AppendLine("	VSS.seikyuu_saki_kbn = @seikyuu_saki_kbn ")
            .AppendLine("	AND ")
            .AppendLine("	VSS.seikyuu_saki_cd = @seikyuu_saki_cd ")
            .AppendLine("	AND ")
            .AppendLine("	VSS.seikyuu_saki_brc = @seikyuu_saki_brc ")
        End With

        'パラメータ
        paramList.Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.Char, 1, strSeikyuusakiKbn))
        paramList.Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 5, strSeikyuusakiCd))
        paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 2, strSeikyuusakiBrc))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtSeikyuusakiInfo", paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function

    ''' <summary>
    ''' 請求先注意事項を取得する
    ''' </summary>
    ''' <history>
    ''' <para>2011/06/14　車龍(大連情報システム部)　新規作成</para>
    ''' </history>
    Public Function SelSeikyuusakiTyuuijikou(ByVal strSeikyuusakiKbn As String, ByVal strSeikyuusakiCd As String, ByVal strSeikyuusakiBrc As String, ByVal strNo As String) As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	CASE torikesi ")
            .AppendLine("		WHEN 0 THEN ")
            .AppendLine("			0 ")
            .AppendLine("		ELSE ")
            .AppendLine("			1 ")
            .AppendLine("	END AS torikesi ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),syubetu_cd),'') AS syubetu_cd ")
            .AppendLine("	,ISNULL(syousai,'') AS syousai ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),jyuyodo),'') AS jyuyodo ")
            .AppendLine("FROM ")
            .AppendLine("	m_seikyuu_saki_tyuuijikou WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	seikyuu_saki_kbn = @seikyuu_saki_kbn  ")
            .AppendLine("	AND  ")
            .AppendLine("	seikyuu_saki_cd = @seikyuu_saki_cd  ")
            .AppendLine("	AND  ")
            .AppendLine("	seikyuu_saki_brc = @seikyuu_saki_brc  ")
            .AppendLine("	AND  ")
            .AppendLine("	nyuuryoku_no = @nyuuryoku_no ")
        End With

        'パラメータ
        paramList.Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.Char, 1, strSeikyuusakiKbn))
        paramList.Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 5, strSeikyuusakiCd))
        paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 2, strSeikyuusakiBrc))
        paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Int, 8, strNo))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtSeikyuusakiTyuuijikou", paramList.ToArray)

        Return dsReturn.Tables(0)
    End Function

    ''' <summary>
    ''' 請求先の存在チェック
    ''' </summary>
    ''' <history>
    ''' <para>2011/06/14　車龍(大連情報システム部)　新規作成</para>
    ''' </history>
    Public Function SelSeikyuusakiCheck(ByVal strSeikyuusakiKbn As String, ByVal strSeikyuusakiCd As String, ByVal strSeikyuusakiBrc As String) As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT  ")
            .AppendLine("	seikyuu_saki_cd ")
            .AppendLine("	,seikyuu_saki_brc ")
            .AppendLine("	,seikyuu_saki_kbn ")
            .AppendLine("FROM  ")
            .AppendLine("	m_seikyuu_saki WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	seikyuu_saki_kbn = @seikyuu_saki_kbn ")
            .AppendLine("	AND ")
            .AppendLine("	seikyuu_saki_cd = @seikyuu_saki_cd ")
            .AppendLine("	AND ")
            .AppendLine("	seikyuu_saki_brc = @seikyuu_saki_brc ")
        End With

        'パラメータ
        paramList.Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.Char, 1, strSeikyuusakiKbn))
        paramList.Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 5, strSeikyuusakiCd))
        paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 2, strSeikyuusakiBrc))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtSeikyuusakiCheck", paramList.ToArray)

        Return dsReturn.Tables(0)
    End Function

    ''' <summary>
    ''' 請求先注意事項の存在チェック
    ''' </summary>
    ''' <history>
    ''' <para>2011/06/14　車龍(大連情報システム部)　新規作成</para>
    ''' </history>
    Public Function SelSeikyuusakiTyuuijikouCheck(ByVal strSeikyuusakiKbn As String, ByVal strSeikyuusakiCd As String, ByVal strSeikyuusakiBrc As String, ByVal strNo As String) As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT  ")
            .AppendLine("	add_datetime ")
            .AppendLine("	,upd_datetime  ")
            .AppendLine("FROM  ")
            .AppendLine("	m_seikyuu_saki_tyuuijikou WITH(READCOMMITTED) ")
            .AppendLine(" WHERE ")
            .AppendLine(" 	seikyuu_saki_kbn = @seikyuu_saki_kbn ")
            .AppendLine(" 	AND ")
            .AppendLine(" 	seikyuu_saki_cd = @seikyuu_saki_cd ")
            .AppendLine(" 	AND ")
            .AppendLine(" 	seikyuu_saki_brc = @seikyuu_saki_brc ")
            .AppendLine(" 	AND ")
            .AppendLine(" 	nyuuryoku_no = @nyuuryoku_no ")
        End With

        'パラメータ
        paramList.Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.Char, 1, strSeikyuusakiKbn))
        paramList.Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 5, strSeikyuusakiCd))
        paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 2, strSeikyuusakiBrc))
        paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Int, 8, strNo))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtSeikyuusakiTyuuijikouCheck", paramList.ToArray)

        Return dsReturn.Tables(0)
    End Function

    ''' <summary>
    ''' DBの時間を取得する
    ''' </summary>
    ''' <history>
    ''' <para>2011/06/14　車龍(大連情報システム部)　新規作成</para>
    ''' </history>
    Public Function SelDbTime() As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder


        'SQL文
        With commandTextSb
            .AppendLine("SELECT  ")
            .AppendLine("	GETDATE() as search_time ")
        End With

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtDbTime")

        Return dsReturn.Tables(0)
    End Function

    ''' <summary>
    ''' 請求先注意事項の最大入力Noを取得する
    ''' </summary>
    ''' <history>
    ''' <para>2011/06/14　車龍(大連情報システム部)　新規作成</para>
    ''' </history>
    Public Function SelMaxNyuuryokuNo(ByVal strSeikyuusakiKbn As String, ByVal strSeikyuusakiCd As String, ByVal strSeikyuusakiBrc As String) As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT  ")
            .AppendLine("	ISNULL(MAX(nyuuryoku_no),0) AS nyuuryoku_no ")
            .AppendLine("FROM  ")
            .AppendLine("	m_seikyuu_saki_tyuuijikou WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	seikyuu_saki_kbn = @seikyuu_saki_kbn ")
            .AppendLine("	AND ")
            .AppendLine("	seikyuu_saki_cd = @seikyuu_saki_cd ")
            .AppendLine("	AND ")
            .AppendLine("	seikyuu_saki_brc = @seikyuu_saki_brc ")
        End With

        'パラメータ
        paramList.Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.Char, 1, strSeikyuusakiKbn))
        paramList.Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 5, strSeikyuusakiCd))
        paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 2, strSeikyuusakiBrc))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtMaxNyuuryokuNo", paramList.ToArray)

        Return dsReturn.Tables(0)
    End Function

    ''' <summary>請求先注意事項を登録する</summary>
    Public Function InsSeikyuusakiTyuuijikou(ByVal strSeikyuusakiKbn As String, ByVal strSeikyuusakiCd As String, ByVal strSeikyuusakiBrc As String, ByVal strNo As String, ByVal strTorikesi As String, ByVal strSyubetuCd As String, ByVal strSyousai As String, ByVal strJyuuyoudo As String, ByVal strUserId As String) As Boolean

        '戻り値
        Dim InsCount As Integer = 0

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("INSERT INTO  ")
            .AppendLine("	m_seikyuu_saki_tyuuijikou WITH(UPDLOCK) ")
            .AppendLine("	( ")
            .AppendLine("		seikyuu_saki_cd ")
            .AppendLine("		,seikyuu_saki_brc ")
            .AppendLine("		,seikyuu_saki_kbn ")
            .AppendLine("		,nyuuryoku_no ")
            .AppendLine("		,torikesi ")
            .AppendLine("		,syubetu_cd ")
            .AppendLine("		,syousai ")
            .AppendLine("		,jyuyodo ")
            .AppendLine("		,add_login_user_id ")
            .AppendLine("		,add_datetime ")
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("( ")
            .AppendLine("	@seikyuu_saki_cd ")
            .AppendLine("	,@seikyuu_saki_brc ")
            .AppendLine("	,@seikyuu_saki_kbn ")
            .AppendLine("	,@nyuuryoku_no ")
            .AppendLine("	,@torikesi ")
            .AppendLine("	,@syubetu_cd ")
            .AppendLine("	,@syousai ")
            .AppendLine("	,@jyuyodo ")
            .AppendLine("	,@add_login_user_id ")
            .AppendLine("	,GETDATE() ")
            .AppendLine(") ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.Char, 1, strSeikyuusakiKbn))
        paramList.Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 5, strSeikyuusakiCd))
        paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 2, strSeikyuusakiBrc))
        paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Int, 8, strNo))
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 8, strTorikesi))
        paramList.Add(MakeParam("@syubetu_cd", SqlDbType.Int, 8, strSyubetuCd))
        paramList.Add(MakeParam("@syousai", SqlDbType.VarChar, 256, strSyousai))
        paramList.Add(MakeParam("@jyuyodo", SqlDbType.Int, 8, strJyuuyoudo))
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strUserId))

        '更新されたデータセットを DB へ書き込み
        Try
            InsCount = ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)
        Catch ex As Exception
            Return False
        End Try

        If Not (InsCount > 0) Then
            Return False
        End If

        Return True
    End Function

    ''' <summary>請求先注意事項を更新する</summary>
    Public Function UpdSeikyuusakiTyuuijikou(ByVal strSeikyuusakiKbn As String, ByVal strSeikyuusakiCd As String, ByVal strSeikyuusakiBrc As String, ByVal strNo As String, ByVal strTorikesi As String, ByVal strSyubetuCd As String, ByVal strSyousai As String, ByVal strJyuuyoudo As String, ByVal strUserId As String) As Boolean

        '戻り値
        Dim UpdCount As Integer = 0

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("UPDATE  ")
            .AppendLine("	m_seikyuu_saki_tyuuijikou WITH(UPDLOCK) ")
            .AppendLine("SET  ")
            .AppendLine("	torikesi = @torikesi ")
            .AppendLine("	,syubetu_cd = @syubetu_cd ")
            .AppendLine("	,syousai = @syousai ")
            .AppendLine("	,jyuyodo = @jyuyodo ")
            .AppendLine("	,upd_login_user_id = @upd_login_user_id ")
            .AppendLine("	,upd_datetime  = GETDATE() ")
            .AppendLine("WHERE  ")
            .AppendLine("	seikyuu_saki_cd = @seikyuu_saki_cd ")
            .AppendLine("	AND ")
            .AppendLine("	seikyuu_saki_brc = @seikyuu_saki_brc ")
            .AppendLine("	AND ")
            .AppendLine("	seikyuu_saki_kbn = @seikyuu_saki_kbn ")
            .AppendLine("	AND ")
            .AppendLine("	nyuuryoku_no = @nyuuryoku_no ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.Char, 1, strSeikyuusakiKbn))
        paramList.Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 5, strSeikyuusakiCd))
        paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 2, strSeikyuusakiBrc))
        paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Int, 8, strNo))
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 8, strTorikesi))
        paramList.Add(MakeParam("@syubetu_cd", SqlDbType.Int, 8, strSyubetuCd))
        paramList.Add(MakeParam("@syousai", SqlDbType.VarChar, 256, strSyousai))
        paramList.Add(MakeParam("@jyuyodo", SqlDbType.Int, 8, strJyuuyoudo))
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, strUserId))

        '更新されたデータセットを DB へ書き込み
        Try
            UpdCount = ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)
        Catch ex As Exception
            Return False
        End Try

        If Not (UpdCount > 0) Then
            Return False
        End If

        Return True
    End Function

End Class

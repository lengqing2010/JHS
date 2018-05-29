Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper

''' <summary>原価マスタ</summary>
''' <remarks>原価マスタ用機能を提供する</remarks>
''' <history>
''' <para>2011/02/24　車龍(大連情報システム部)　新規作成</para>
''' </history>
Public Class GenkaMasterDataAccess

    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>調査会社名を取得する</summary>
    ''' <returns>調査会社名データテーブル</returns>
    ''' <history>2011/02/24　車龍(大連情報システム部)　新規作成</history>
    Public Function SelTyousaKaisyaMei(ByVal strTyousaKaisyaCd As String, ByVal strJigyousyoCd As String, ByVal strKensakuTaisyouGai As String) As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT  ")
            .AppendLine("	tys_kaisya_mei ")
            .AppendLine("FROM  ")
            .AppendLine("	m_tyousakaisya WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	tys_kaisya_cd = @tys_kaisya_cd ")
            .AppendLine("	AND ")
            .AppendLine("	jigyousyo_cd = @jigyousyo_cd ")

            If strKensakuTaisyouGai <> String.Empty Then
                '取消パラメータの設定
                .AppendLine("	AND ")
                .AppendLine("	torikesi = @torikesi ")
                paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 8, "0"))
            End If

        End With

        paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, strTyousaKaisyaCd))
        paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, strJigyousyoCd))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtTyousaKaisyaMei", paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function

    ''' <summary>相手先種別を取得する</summary>
    ''' <returns>相手先種別データテーブル</returns>
    ''' <history>2011/02/24　車龍(大連情報システム部)　新規作成</history>
    Public Function SelAiteSakiSyubetu() As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	code ")                                 'コード
            .AppendLine("	,(code + '：' + meisyou) AS meisyou ")  '名称
            .AppendLine("FROM ")
            .AppendLine("	m_kakutyou_meisyou WITH(READCOMMITTED) ")                   '拡張名称マスタ
            .AppendLine("WHERE ")
            .AppendLine("	meisyou_syubetu = 22 ")                 '名称種別
        End With

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtAiteSakiSyubetu")

        Return dsReturn.Tables(0)

    End Function

    ''' <summary>相手先名を取得する</summary>
    ''' <returns>相手先名データテーブル</returns>
    ''' <history>2011/03/07　車龍(大連情報システム部)　新規作成</history>
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

    ''' <summary>商品コードを取得する</summary>
    ''' <returns>商品コードデータテーブル</returns>
    ''' <history>2011/02/24　車龍(大連情報システム部)　新規作成</history>
    Public Function SelSyouhinCd() As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	syouhin_cd ")                       '商品コード
            .AppendLine("	,(syouhin_cd + '：' + syouhin_mei) AS syouhin_mei ") '商品名
            .AppendLine("FROM ")
            .AppendLine("	m_syouhin WITH(READCOMMITTED) ")                        '商品マスタ
            .AppendLine("WHERE ")
            .AppendLine("	torikesi = 0 ")                     '取消
            .AppendLine("	and ")
            .AppendLine("	souko_cd = '100' ")                 '倉庫コード
        End With

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtSyouhinCd")

        Return dsReturn.Tables(0)

    End Function

    ''' <summary>調査方法を取得する</summary>
    ''' <returns>調査方法データテーブル</returns>
    ''' <history>2011/02/24　車龍(大連情報システム部)　新規作成</history>
    Public Function SelTyousaHouhou() As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
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

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtTyousahouhou")

        Return dsReturn.Tables(0)

    End Function


    ''' <summary>原価情報を取得する</summary>
    ''' <returns>原価情報データテーブル</returns>
    ''' <history>2011/02/28　車龍(大連情報システム部)　新規作成</history>
    Public Function SelGenkaJyouhou(ByVal strKensakuCount As String, ByVal strTysKaisyaCd As String, ByVal strAtesakiSyubetu As String, ByVal strAtesakiCdFrom As String, ByVal strAtesakiCdTo As String, ByVal strSyouhinCd As String, ByVal strHouhouCd As String, ByVal blnKensakuTaisyouGai As Boolean, ByVal blnAitesakiTaisyouGai As Boolean) As GenkaMasterDataSet.GenkaInfoTableDataTable

        'DataSetインスタンスの生成
        Dim dsReturn As New GenkaMasterDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            '検索件数
            If strKensakuCount.Trim.Equals("10") Then
                .AppendLine("   TOP 10 ")
            End If
            If strKensakuCount.Trim.Equals("100") Then
                .AppendLine("   TOP 100 ")
            End If
            .AppendLine("	(MG.tys_kaisya_cd + '：' + MG.jigyousyo_cd) AS tys_kaisya_cd ")  '--調査会社コード(調査会社コード：事業所コード)
            .AppendLine("	,MTS.tys_kaisya_mei ")  '--調査会社名")
            .AppendLine("	,(LTRIM(STR(MG.aitesaki_syubetu)) + '：' + MKM.meisyou) AS aitesaki_syubetu ")   '--相手先種別	(相手先種別：名称)
            .AppendLine("	,MG.aitesaki_cd ")  '--相手先コード
            .AppendLine("	,SUB.aitesaki_mei ")    '--相手先名
            .AppendLine("	,MG.syouhin_cd ")   '--商品コード
            .AppendLine("	,MS.syouhin_mei ")  '--商品名
            .AppendLine("	,(LTRIM(STR(MG.tys_houhou_no)) + '：' + MTH.tys_houhou_mei) AS tys_houhou ") '--調査方法(調査方法NO：調査方法名称)
            .AppendLine("   ,MG.tys_houhou_no ")
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN MG.torikesi = 0 THEN ")
            .AppendLine("			'' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'取消' ")
            .AppendLine("		END AS torikesi ")  '--取消
            .AppendLine("	,MG.tou_kkk1 ")   '--棟価格1
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MG.tou_kkk_henkou_flg1,0) = 0 THEN ")
            .AppendLine("			'変更不可' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'変更可' ")
            .AppendLine("		END AS tou_kkk_henkou_flg1 ")   '--棟価格変更FLG1
            .AppendLine("	,MG.tou_kkk2 ")   '--棟価格2
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MG.tou_kkk_henkou_flg2,0) = 0 THEN ")
            .AppendLine("			'変更不可' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'変更可' ")
            .AppendLine("		END AS tou_kkk_henkou_flg2 ")   '--棟価格変更FLG2
            .AppendLine("	,MG.tou_kkk3 ")   '--棟価格3
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MG.tou_kkk_henkou_flg3,0) = 0 THEN ")
            .AppendLine("			'変更不可' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'変更可' ")
            .AppendLine("		END AS tou_kkk_henkou_flg3 ")   '--棟価格変更FLG3
            .AppendLine("	,MG.tou_kkk4 ")   '--棟価格4
            .AppendLine("	,CASE")
            .AppendLine("		WHEN ISNULL(MG.tou_kkk_henkou_flg4,0) = 0 THEN ")
            .AppendLine("			'変更不可' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'変更可' ")
            .AppendLine("		END AS tou_kkk_henkou_flg4 ")   '--棟価格変更FLG4
            .AppendLine("	,MG.tou_kkk5 ")   '--棟価格5
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MG.tou_kkk_henkou_flg5,0) = 0 THEN ")
            .AppendLine("			'変更不可' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'変更可' ")
            .AppendLine("		END AS tou_kkk_henkou_flg5 ")   '--棟価格変更FLG5
            .AppendLine("	,MG.tou_kkk6 ")   '--棟価格6
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MG.tou_kkk_henkou_flg6,0) = 0 THEN ")
            .AppendLine("			'変更不可' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'変更可' ")
            .AppendLine("		END AS tou_kkk_henkou_flg6 ")   '--棟価格変更FLG6
            .AppendLine("	,MG.tou_kkk7 ")   '--棟価格7
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MG.tou_kkk_henkou_flg7,0) = 0 THEN ")
            .AppendLine("			'変更不可' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'変更可' ")
            .AppendLine("		END AS tou_kkk_henkou_flg7 ")   '--棟価格変更FLG7
            .AppendLine("	,MG.tou_kkk8 ")   '--棟価格8
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MG.tou_kkk_henkou_flg8,0) = 0 THEN ")
            .AppendLine("			'変更不可' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'変更可' ")
            .AppendLine("		END AS tou_kkk_henkou_flg8 ")   '--棟価格変更FLG8
            .AppendLine("	,MG.tou_kkk9 ")   '--棟価格9
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MG.tou_kkk_henkou_flg9,0) = 0 THEN ")
            .AppendLine("			'変更不可' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'変更可' ")
            .AppendLine("		END AS tou_kkk_henkou_flg9 ")   '--棟価格変更FLG9
            .AppendLine("	,MG.tou_kkk10 ") '--棟価格10
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MG.tou_kkk_henkou_flg10,0) = 0 THEN ")
            .AppendLine("			'変更不可' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'変更可' ")
            .AppendLine("		END AS tou_kkk_henkou_flg10 ")  '--棟価格変更FLG10
            .AppendLine("	,MG.tou_kkk11t19 ")   '--棟価格11～19
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MG.tou_kkk_henkou_flg11t19,0) = 0 THEN ")
            .AppendLine("			'変更不可' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'変更可' ")
            .AppendLine("		END AS tou_kkk_henkou_flg11t19 ")   '--棟価格変更FLG11～19
            .AppendLine("	,MG.tou_kkk20t29 ")   '--棟価格20～29
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MG.tou_kkk_henkou_flg20t29,0) = 0 THEN ")
            .AppendLine("			'変更不可' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'変更可' ")
            .AppendLine("		END AS tou_kkk_henkou_flg20t29 ")   '--棟価格変更FLG20～29
            .AppendLine("	,MG.tou_kkk30t39 ")   '--棟価格30～39
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MG.tou_kkk_henkou_flg30t39,0) = 0 THEN ")
            .AppendLine("			'変更不可' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'変更可' ")
            .AppendLine("		END AS tou_kkk_henkou_flg30t39 ")   '--棟価格変更FLG30～39
            .AppendLine("	,MG.tou_kkk40t49 ")   '--棟価格40～49
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MG.tou_kkk_henkou_flg40t49,0) = 0 THEN ")
            .AppendLine("			'変更不可'")
            .AppendLine("		ELSE")
            .AppendLine("			'変更可'")
            .AppendLine("		END AS tou_kkk_henkou_flg40t49 ")   '--棟価格変更FLG40～49
            .AppendLine("	,MG.tou_kkk50t ")   '--棟価格50～
            .AppendLine("	,CASE")
            .AppendLine("		WHEN ISNULL(MG.tou_kkk_henkou_flg50t,0) = 0 THEN ")
            .AppendLine("			'変更不可' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'変更可' ")
            .AppendLine("		END AS tou_kkk_henkou_flg50t ") '--棟価格変更FLG50～
            .AppendLine("FROM ")
            .AppendLine("	m_genka AS MG WITH(READCOMMITTED) ")    '--原価M
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	( ")
            .AppendLine("		SELECT ")
            .AppendLine("			0			AS aitesaki_syubetu ")  '--相手先種別
            .AppendLine("			,'ALL'			AS aitesaki_cd ")   '--相手先コード
            .AppendLine("			,'相手先なし'		AS aitesaki_mei ")  '--相手先名
            .AppendLine("			,0			AS torikesi	 ") '--取消")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			1			AS aitesaki_syubetu ")
            .AppendLine("			,MKA.kameiten_cd	AS aitesaki_cd ")   '--加盟店コード
            .AppendLine("			,MKA.kameiten_mei1	AS aitesaki_mei ")  '--加盟店名1
            .AppendLine("			,MKA.torikesi		AS torikesi ")
            .AppendLine("		FROM ")
            .AppendLine("			m_kameiten AS MKA WITH(READCOMMITTED) ")    '--加盟店マスタ
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			3			AS aitesaki_syubetu ")
            .AppendLine("			,'JIO'	AS aitesaki_cd ")
            .AppendLine("			,'JIO先'	AS aitesaki_mei ")
            .AppendLine("			,'0'		AS torikesi ")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			7			        AS aitesaki_syubetu ")
            .AppendLine("			,MKE.keiretu_cd	    AS aitesaki_cd ")   '--系列コード
            .AppendLine("			,MIN(MKE.keiretu_mei)	AS aitesaki_mei ")  '--系列名
            .AppendLine("			,MIN(MKE.torikesi)		AS torikesi ")
            .AppendLine("		FROM ")
            .AppendLine("			m_keiretu AS MKE WITH(READCOMMITTED) ") '--系列マスタ
            .AppendLine("           GROUP BY ")
            .AppendLine("               MKE.keiretu_cd ")
            .AppendLine("	) AS SUB ")
            .AppendLine("	ON ")
            .AppendLine("		MG.aitesaki_syubetu = SUB.aitesaki_syubetu ")
            .AppendLine("		AND ")
            .AppendLine("		MG.aitesaki_cd = SUB.aitesaki_cd ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_tyousakaisya AS MTS WITH(READCOMMITTED) ")    '--調査会社マスタ
            .AppendLine("	ON ")
            .AppendLine("		MG.tys_kaisya_cd  =  MTS.tys_kaisya_cd ")
            .AppendLine("		AND ")
            .AppendLine("		MG.jigyousyo_cd  =  MTS.jigyousyo_cd ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_syouhin AS MS WITH(READCOMMITTED) ")  '--商品マスタ
            .AppendLine("	ON ")
            .AppendLine("		MG.syouhin_cd  =  MS.syouhin_cd ")
            .AppendLine("		AND ")
            .AppendLine("		MS.souko_cd  =  '100' ")    '--倉庫コード
            .AppendLine("		AND ")
            .AppendLine("		MS.torikesi = 0 ")
            .AppendLine("	LEFT JOIN")
            .AppendLine("	m_tyousahouhou AS MTH WITH(READCOMMITTED) ")    '--調査方法マスタ
            .AppendLine("		ON ")
            .AppendLine("		MG.tys_houhou_no  =  MTH.tys_houhou_no ")   '--調査方法NO
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_kakutyou_meisyou AS MKM WITH(READCOMMITTED) ")    '--拡張名称マスタ
            .AppendLine("	ON ")
            .AppendLine("		MG.aitesaki_syubetu=MKM.code ") '--ｺｰﾄﾞ
            .AppendLine("		AND ")
            .AppendLine("		MKM.meisyou_syubetu='22' ") '--名称種別
            .AppendLine("WHERE ")
            '｢調査会社コード｣
            If Not strTysKaisyaCd.Trim.Equals(String.Empty) Then
                .AppendLine("	MG.tys_kaisya_cd = @tys_kaisya_cd ")   '--調査会社コード
                .AppendLine("	AND")
                .AppendLine("	MG.jigyousyo_cd = @jigyousyo_cd ")   '--事業所コード
                .AppendLine("	AND ")
            End If
            '相手先種別
            If Not strAtesakiSyubetu.Trim.Equals(String.Empty) Then
                .AppendLine("	MG.aitesaki_syubetu = @aitesaki_syubetu ")   '--相手先種別
                .AppendLine("	AND")
                If (Not strAtesakiCdFrom.Trim.Equals(String.Empty)) AndAlso (Not strAtesakiCdTo.Trim.Equals(String.Empty)) Then
                    '相手先コードFROM、相手先コードTO両方が入力されている場合
                    .AppendLine("	MG.aitesaki_cd BETWEEN @aitesaki_cd_from AND @aitesaki_cd_to ")   '--相手先コード
                    .AppendLine("	AND ")
                Else
                    '相手先コードFROMのみあるいは、相手先コードTOが入力されている場合
                    If Not strAtesakiCdFrom.Trim.Equals(String.Empty) Then
                        .AppendLine("	MG.aitesaki_cd = @aitesaki_cd_from ")   '--相手先コード
                        .AppendLine("	AND ")
                    Else
                        If Not strAtesakiCdTo.Trim.Equals(String.Empty) Then
                            .AppendLine("	MG.aitesaki_cd = @aitesaki_cd_to ")   '--相手先コード
                            .AppendLine("	AND ")
                        End If
                    End If
                End If
            End If
            If Not strSyouhinCd.Trim.Equals(String.Empty) Then
                '商品コード=入力の場合
                .AppendLine("	MG.syouhin_cd = @syouhin_cd ")   '--商品コード
                .AppendLine("	AND")
            End If
            If Not strHouhouCd.Equals(String.Empty) Then
                '調査方法=入力の場合
                .AppendLine("	MG.tys_houhou_no = @tys_houhou_no ")   '--調査方法
                .AppendLine("	AND")
            End If
            If blnKensakuTaisyouGai.Equals(True) Then
                '取消は検索対象外=チェックの場合
                .AppendLine("	MG.torikesi = 0 ")   '--取消
                .AppendLine("	AND")
            End If
            If blnAitesakiTaisyouGai.Equals(True) Then
                '取消相手先は対象外=チェックの場合
                .AppendLine("	SUB.torikesi = 0 ")   '--取消
                .AppendLine("	AND")
            End If
            .AppendLine("	1 = 1 ")
            .AppendLine("ORDER BY ")
            .AppendLine("	MG.tys_kaisya_cd ") '--調査会社コード
            .AppendLine("	,MG.jigyousyo_cd ") '--事業所コード
            .AppendLine("	,MG.aitesaki_syubetu ") '--相手先種別
            .AppendLine("	,MG.aitesaki_cd ")  '--相手先コード
            .AppendLine("	,MG.syouhin_cd ")   '--商品コード
            .AppendLine("	,MG.tys_houhou_no ")    '--調査方法NO
        End With

        paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 4, Left(strTysKaisyaCd, 4)))
        paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, IIf(strTysKaisyaCd.Length > 4, Mid(strTysKaisyaCd, 5, 6), String.Empty)))
        If Not strAtesakiSyubetu.Trim.Equals(String.Empty) Then
            paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 8, CInt(strAtesakiSyubetu)))
        End If
        paramList.Add(MakeParam("@aitesaki_cd_from", SqlDbType.VarChar, 5, strAtesakiCdFrom))
        paramList.Add(MakeParam("@aitesaki_cd_to", SqlDbType.VarChar, 5, strAtesakiCdTo))
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))
        If Not strHouhouCd.Trim.Equals(String.Empty) Then
            paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 8, CInt(strHouhouCd)))
        End If

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, dsReturn.GenkaInfoTable.TableName, paramList.ToArray)

        Return dsReturn.GenkaInfoTable

    End Function





    ''' <summary>原価情報件数を取得する</summary>
    ''' <returns>原価情報データテーブル</returns>
    ''' <history>2011/02/28　車龍(大連情報システム部)　新規作成</history>
    Public Function SelGenkaJyouhouCount(ByVal strTysKaisyaCd As String, ByVal strAtesakiSyubetu As String, ByVal strAtesakiCdFrom As String, ByVal strAtesakiCdTo As String, ByVal strSyouhinCd As String, ByVal strHouhouCd As String, ByVal blnKensakuTaisyouGai As Boolean, ByVal blnAitesakiTaisyouGai As Boolean) As Integer

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	COUNT(MG.tys_kaisya_cd) ")  '--件数
            .AppendLine("FROM ")
            .AppendLine("	m_genka AS MG WITH(READCOMMITTED) ")    '--原価M
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	( ")
            .AppendLine("		SELECT ")
            .AppendLine("			0			AS aitesaki_syubetu ")  '--相手先種別
            .AppendLine("			,'ALL'			AS aitesaki_cd ")   '--相手先コード
            .AppendLine("			,'相手先なし'		AS aitesaki_mei ")  '--相手先名
            .AppendLine("			,0			AS torikesi	 ") '--取消")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			1			AS aitesaki_syubetu ")
            .AppendLine("			,MKA.kameiten_cd	AS aitesaki_cd ")   '--加盟店コード
            .AppendLine("			,MKA.kameiten_mei1	AS aitesaki_mei ")  '--加盟店名1
            .AppendLine("			,MKA.torikesi		AS torikesi ")
            .AppendLine("		FROM ")
            .AppendLine("			m_kameiten AS MKA WITH(READCOMMITTED) ")    '--加盟店マスタ
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			3			AS aitesaki_syubetu ")
            .AppendLine("			,'JIO'	AS aitesaki_cd ")
            .AppendLine("			,'JIO先'	AS aitesaki_mei ")
            .AppendLine("			,'0'		AS torikesi ")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			7			        AS aitesaki_syubetu ")
            .AppendLine("			,MKE.keiretu_cd	    AS aitesaki_cd ")   '--系列コード
            .AppendLine("			,MIN(MKE.keiretu_mei)	AS aitesaki_mei ")  '--系列名
            .AppendLine("			,MIN(MKE.torikesi)		AS torikesi ")
            .AppendLine("		FROM ")
            .AppendLine("			m_keiretu AS MKE WITH(READCOMMITTED) ") '--系列マスタ
            .AppendLine("           GROUP BY ")
            .AppendLine("               MKE.keiretu_cd ")
            .AppendLine("	) AS SUB ")
            .AppendLine("	ON ")
            .AppendLine("		MG.aitesaki_syubetu = SUB.aitesaki_syubetu ")
            .AppendLine("		AND ")
            .AppendLine("		MG.aitesaki_cd = SUB.aitesaki_cd ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_tyousakaisya AS MTS WITH(READCOMMITTED) ")    '--調査会社マスタ
            .AppendLine("	ON ")
            .AppendLine("		MG.tys_kaisya_cd  =  MTS.tys_kaisya_cd ")
            .AppendLine("		AND ")
            .AppendLine("		MG.jigyousyo_cd  =  MTS.jigyousyo_cd ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_syouhin AS MS WITH(READCOMMITTED) ")  '--商品マスタ
            .AppendLine("	ON ")
            .AppendLine("		MG.syouhin_cd  =  MS.syouhin_cd ")
            .AppendLine("		AND ")
            .AppendLine("		MS.souko_cd  =  '100' ")    '--倉庫コード
            .AppendLine("		AND ")
            .AppendLine("		MS.torikesi = 0 ")
            .AppendLine("	LEFT JOIN")
            .AppendLine("	m_tyousahouhou AS MTH WITH(READCOMMITTED) ")    '--調査方法マスタ
            .AppendLine("		ON ")
            .AppendLine("		MG.tys_houhou_no  =  MTH.tys_houhou_no ")   '--調査方法NO
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_kakutyou_meisyou AS MKM WITH(READCOMMITTED) ")    '--拡張名称マスタ
            .AppendLine("	ON ")
            .AppendLine("		MG.aitesaki_syubetu=MKM.code ") '--ｺｰﾄﾞ
            .AppendLine("		AND ")
            .AppendLine("		MKM.meisyou_syubetu='22' ") '--名称種別
            .AppendLine("WHERE ")
            '｢調査会社コード｣
            If Not strTysKaisyaCd.Trim.Equals(String.Empty) Then
                .AppendLine("	MG.tys_kaisya_cd = @tys_kaisya_cd ")   '--調査会社コード
                .AppendLine("	AND")
                .AppendLine("	MG.jigyousyo_cd = @jigyousyo_cd ")   '--事業所コード
                .AppendLine("	AND ")
            End If
            '相手先種別
            If Not strAtesakiSyubetu.Trim.Equals(String.Empty) Then
                .AppendLine("	MG.aitesaki_syubetu = @aitesaki_syubetu ")   '--相手先種別
                .AppendLine("	AND")
                If (Not strAtesakiCdFrom.Trim.Equals(String.Empty)) AndAlso (Not strAtesakiCdTo.Trim.Equals(String.Empty)) Then
                    '相手先コードFROM、相手先コードTO両方が入力されている場合
                    .AppendLine("	MG.aitesaki_cd BETWEEN @aitesaki_cd_from AND @aitesaki_cd_to ")   '--相手先コード
                    .AppendLine("	AND ")
                Else
                    '相手先コードFROMのみあるいは、相手先コードTOが入力されている場合
                    If Not strAtesakiCdFrom.Trim.Equals(String.Empty) Then
                        .AppendLine("	MG.aitesaki_cd = @aitesaki_cd_from ")   '--相手先コード
                        .AppendLine("	AND ")
                    Else
                        If Not strAtesakiCdTo.Trim.Equals(String.Empty) Then
                            .AppendLine("	MG.aitesaki_cd = @aitesaki_cd_to ")   '--相手先コード
                            .AppendLine("	AND ")
                        End If
                    End If
                End If
            End If
            If Not strSyouhinCd.Trim.Equals(String.Empty) Then
                '商品コード=入力の場合
                .AppendLine("	MG.syouhin_cd = @syouhin_cd ")   '--商品コード
                .AppendLine("	AND")
            End If
            If Not strHouhouCd.Equals(String.Empty) Then
                '調査方法=入力の場合
                .AppendLine("	MG.tys_houhou_no = @tys_houhou_no ")   '--調査方法
                .AppendLine("	AND")
            End If
            If blnKensakuTaisyouGai.Equals(True) Then
                '取消は検索対象外=チェックの場合
                .AppendLine("	MG.torikesi = 0 ")   '--取消
                .AppendLine("	AND")
            End If
            If blnAitesakiTaisyouGai.Equals(True) Then
                '取消相手先は対象外=チェックの場合
                .AppendLine("	SUB.torikesi = 0 ")   '--取消
                .AppendLine("	AND")
            End If
            .AppendLine("	1 = 1 ")
        End With

        paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 4, Left(strTysKaisyaCd, 4)))
        paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, IIf(strTysKaisyaCd.Length > 4, Mid(strTysKaisyaCd, 5, 6), String.Empty)))
        If Not strAtesakiSyubetu.Trim.Equals(String.Empty) Then
            paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 8, CInt(strAtesakiSyubetu)))
        End If
        paramList.Add(MakeParam("@aitesaki_cd_from", SqlDbType.VarChar, 5, strAtesakiCdFrom))
        paramList.Add(MakeParam("@aitesaki_cd_to", SqlDbType.VarChar, 5, strAtesakiCdTo))
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))
        If Not strHouhouCd.Trim.Equals(String.Empty) Then
            paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 8, CInt(strHouhouCd)))
        End If

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtGenkaJyouhouCount", paramList.ToArray)

        Return CInt(dsReturn.Tables(0).Rows(0).Item(0).ToString.Trim)

    End Function

    ''' <summary>加盟店件数を取得する</summary>
    ''' <returns>加盟店件数</returns>
    Public Function SelKameitenCount(ByVal strAitesakiCdFrom As String, ByVal strAitesakiCdTo As String, ByVal strTorikesiAitesaki As String) As Integer

        'DataSetインスタンスの生成
        Dim dsKameiten As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   COUNT(*) AS count ")
            .AppendLine(" FROM ")
            .AppendLine("   m_kameiten WITH(READCOMMITTED) ")   '加盟店マスタ
            .AppendLine(" WHERE ")

            '相手先コード
            .AppendLine(" kameiten_cd BETWEEN @aitesaki_cd_from AND @aitesaki_cd_to ")
            paramList.Add(MakeParam("@aitesaki_cd_from", SqlDbType.VarChar, 5, strAitesakiCdFrom))
            paramList.Add(MakeParam("@aitesaki_cd_to", SqlDbType.VarChar, 5, strAitesakiCdTo))

            '取消相手先
            If strTorikesiAitesaki <> String.Empty Then
                .AppendLine(" AND torikesi = @torikesi_aitesaki ")
                paramList.Add(MakeParam("@torikesi_aitesaki", SqlDbType.Int, 8, 0))
            End If

        End With

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsKameiten, _
                    "dtKameitenCount", paramList.ToArray)

        Return dsKameiten.Tables("dtKameitenCount").Rows(0).Item("count")

    End Function


    ''' <summary>未設定も含む原価CSVデータを取得する</summary>
    ''' <returns>未設定も含む原価CSVデータテーブル</returns>
    Public Function SelMiSeteiGenkaCSVInfo(ByVal strTysKaisyaCd As String, ByVal strAtesakiSyubetu As String, ByVal strAtesakiCdFrom As String, ByVal strAtesakiCdTo As String, ByVal strSyouhinCd As String, ByVal strHouhouCd As String, ByVal blnKensakuTaisyouGai As Boolean, ByVal blnAitesakiTaisyouGai As Boolean) As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT  ")
            .AppendLine("	RIGHT(CONVERT(varchar(10),GETDATE(),112),4) + REPLACE(CONVERT(varchar(8),GETDATE(),108),':','') AS edi_jouhou_sakusei_date  ")
            .AppendLine("	,ISNULL(SUB_MTK.tys_kaisya_cd,'')  ")
            .AppendLine("	,ISNULL(SUB_MTK.jigyousyo_cd,'')  ")
            .AppendLine("	,ISNULL(SUB_MTK.tys_kaisya_mei,'')  ")
            .AppendLine("	,ISNULL(SUB.aitesaki_syubetu,'')  ")
            .AppendLine("	,ISNULL(SUB.aitesaki_cd,'')  ")
            .AppendLine("	,ISNULL(SUB.aitesaki_mei,'')  ")
            .AppendLine("	,ISNULL(SUB_MSH.syouhin_cd,'')  ")
            .AppendLine("	,ISNULL(SUB_MSH.syouhin_mei,'')  ")
            .AppendLine("	,ISNULL(SUB_MTH.tys_houhou_no,'')  ")
            .AppendLine("	,ISNULL(SUB_MTH.tys_houhou_mei,'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.torikesi),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk1),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg1),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk2),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg2),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk3),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg3),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk4),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg4),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk5),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg5),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk6),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg6),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk7),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg7),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk8),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg8),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk9),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg9),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk10),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg10),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk11t19),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg11t19),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk20t29),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg20t29),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk30t39),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg30t39),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk40t49),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg40t49),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk50t),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg50t),'')  ")
            .AppendLine("FROM ")
            .AppendLine("	( ")
            .AppendLine("		SELECT ")
            .AppendLine("			tys_houhou_no ")
            .AppendLine("			,tys_houhou_mei ")
            .AppendLine("		FROM ")
            .AppendLine("			m_tyousahouhou WITH(READCOMMITTED) ")
            .AppendLine("		WHERE ")
            .AppendLine("			ISNULL(genka_settei_fuyou_flg,0) = 0 ") '--原価設定不要フラグ 
            '＜調査方法=入力の場合＞
            If Not strHouhouCd.Trim.Equals(String.Empty) Then
                .AppendLine("			AND ")
                .AppendLine("			tys_houhou_no = @tys_houhou_no ")
            End If
            .AppendLine("	) AS SUB_MTH ") '--サブ調査方法M 
            .AppendLine("	CROSS JOIN ")
            .AppendLine("	( ")
            .AppendLine("		SELECT ")
            .AppendLine("			syouhin_cd ")
            .AppendLine("			,syouhin_mei ")
            .AppendLine("		FROM ")
            .AppendLine("			m_syouhin WITH(READCOMMITTED) ")
            .AppendLine("		WHERE ")
            .AppendLine("			souko_cd = '100' ")
            .AppendLine("			AND ")
            .AppendLine("			torikesi = 0 ")
            '＜商品コード=入力の場合＞
            If Not strSyouhinCd.Trim.Equals(String.Empty) Then
                .AppendLine("			AND ")
                .AppendLine("			syouhin_cd = @syouhin_cd ")
            End If
            .AppendLine("	) AS SUB_MSH ") '--サブ商品M ")
            .AppendLine("	CROSS JOIN ")
            .AppendLine("	( ")
            If strAtesakiSyubetu.Trim.Equals(String.Empty) Then
                .AppendLine("		SELECT ")
                .AppendLine("			0			AS aitesaki_syubetu ")  '--相手先種別
                .AppendLine("			,'ALL'			AS aitesaki_cd ")   '--相手先コード
                .AppendLine("			,'相手先なし'		AS aitesaki_mei ")  '--相手先名
                .AppendLine("			,0			AS torikesi	 ") '--取消")
                .AppendLine("		UNION ALL ")
                .AppendLine("		SELECT ")
                .AppendLine("			1			AS aitesaki_syubetu ")
                .AppendLine("			,MKA.kameiten_cd	AS aitesaki_cd ")   '--加盟店コード
                .AppendLine("			,MKA.kameiten_mei1	AS aitesaki_mei ")  '--加盟店名1
                .AppendLine("			,MKA.torikesi		AS torikesi ")
                .AppendLine("		FROM ")
                .AppendLine("			m_kameiten AS MKA WITH(READCOMMITTED) ")    '--加盟店マスタ
                '＜取消相手先は対象外=チェックの場合＞
                If blnAitesakiTaisyouGai.Equals(True) Then
                    .AppendLine("       WHERE ")
                    .AppendLine("           AND ")
                    .AppendLine("	        MKA.torikesi = 0 ")   '--取消
                End If
                .AppendLine("		UNION ALL ")
                .AppendLine("		SELECT ")
                .AppendLine("			3			AS aitesaki_syubetu ")
                .AppendLine("			,'JIO'	AS aitesaki_cd ")
                .AppendLine("			,'JIO先'	AS aitesaki_mei ")
                .AppendLine("			,'0'		AS torikesi ")
                .AppendLine("		UNION ALL ")
                .AppendLine("		SELECT ")
                .AppendLine("			7			        AS aitesaki_syubetu ")
                .AppendLine("			,MKE.keiretu_cd	    AS aitesaki_cd ")   '--系列コード
                .AppendLine("			,MIN(MKE.keiretu_mei)	AS aitesaki_mei ")  '--系列名
                .AppendLine("			,MIN(MKE.torikesi)		AS torikesi ")
                .AppendLine("		FROM ")
                .AppendLine("			m_keiretu AS MKE WITH(READCOMMITTED) ") '--系列マスタ
                '＜取消相手先は対象外=チェックの場合＞
                If blnAitesakiTaisyouGai.Equals(True) Then
                    .AppendLine("       WHERE ")
                    .AppendLine("           AND ")
                    .AppendLine("	        MKA.torikesi = 0 ")   '--取消
                End If
                .AppendLine("           GROUP BY ")
                .AppendLine("               MKE.keiretu_cd ")
            Else
                Select Case strAtesakiSyubetu
                    Case "0"
                        .AppendLine("		SELECT ")
                        .AppendLine("			0			AS aitesaki_syubetu ")  '--相手先種別 
                        .AppendLine("			,'ALL'			AS aitesaki_cd ")   '--相手先コード 
                        .AppendLine("			,'相手先なし'		AS aitesaki_mei ")  '--相手先名 
                    Case "3"
                        .AppendLine("		SELECT ")
                        .AppendLine("			3			AS aitesaki_syubetu ")
                        .AppendLine("			,'JIO'	AS aitesaki_cd ")
                        .AppendLine("			,'JIO先'	AS aitesaki_mei ")
                    Case "1"
                        .AppendLine("		SELECT ")
                        .AppendLine("			1					AS aitesaki_syubetu ")
                        .AppendLine("			,MKA.kameiten_cd	AS aitesaki_cd ")   '--加盟店コード 
                        .AppendLine("			,MKA.kameiten_mei1	AS aitesaki_mei ")  '--加盟店名1 
                        .AppendLine("		FROM ")
                        .AppendLine("			m_kameiten AS MKA WITH(READCOMMITTED) ")    '--加盟店マスタ 
                        .AppendLine("		WHERE ")
                        .AppendLine("           1=1 ")
                        '＜相手先コードFROM、相手先コードTO両方が入力されている場合＞
                        If (Not strAtesakiCdFrom.Trim.Equals(String.Empty)) AndAlso (Not strAtesakiCdTo.Trim.Equals(String.Empty)) Then
                            .AppendLine("           AND ")
                            .AppendLine("			MKA.kameiten_cd BETWEEN @aitesaki_cd_from AND @aitesaki_cd_to ")
                        Else
                            '＜相手先コードFROMのみあるいは、相手先コードTOが入力されている場合＞
                            If Not strAtesakiCdFrom.Trim.Equals(String.Empty) Then
                                .AppendLine("           AND ")
                                .AppendLine("	        MKA.kameiten_cd = @aitesaki_cd_from ")   '--加盟店コード 
                            Else
                                If Not strAtesakiCdTo.Trim.Equals(String.Empty) Then
                                    .AppendLine("           AND ")
                                    .AppendLine("	        MKA.kameiten_cd = @aitesaki_cd_to ")   '--加盟店コード
                                End If
                            End If
                        End If
                        '＜取消相手先は対象外=チェックの場合＞
                        If blnAitesakiTaisyouGai.Equals(True) Then
                            .AppendLine("           AND ")
                            .AppendLine("	        MKA.torikesi = 0 ")   '--取消
                        End If
                    Case "7"
                        .AppendLine("			SELECT ")
                        .AppendLine("				7			 AS aitesaki_syubetu ")
                        .AppendLine("				,MKE.keiretu_cd	 AS aitesaki_cd ")  '--系列コード 
                        .AppendLine("				,MIN(MKE.keiretu_mei)	AS aitesaki_mei ")  '--系列名 
                        .AppendLine("			FROM ")
                        .AppendLine("				m_keiretu AS MKE WITH(READCOMMITTED) ") '--系列マスタ 
                        .AppendLine(" ")
                        .AppendLine("			WHERE ")
                        .AppendLine("           1=1 ")
                        '＜相手先コードFROM、相手先コードTO両方が入力されている場合＞
                        If (Not strAtesakiCdFrom.Trim.Equals(String.Empty)) AndAlso (Not strAtesakiCdTo.Trim.Equals(String.Empty)) Then
                            .AppendLine("           AND ")
                            .AppendLine("           MKE.keiretu_cd BETWEEN @aitesaki_cd_from AND @aitesaki_cd_to ")
                        Else
                            '＜相手先コードFROMのみあるいは、相手先コードTOが入力されている場合＞
                            If Not strAtesakiCdFrom.Trim.Equals(String.Empty) Then
                                .AppendLine("           AND ")
                                .AppendLine("	        MKE.keiretu_cd = @aitesaki_cd_from ")   '--系列コード
                            Else
                                If Not strAtesakiCdTo.Trim.Equals(String.Empty) Then
                                    .AppendLine("           AND ")
                                    .AppendLine("           MKE.keiretu_cd = @aitesaki_cd_to ")   '--系列コード
                                End If
                            End If
                        End If
                        '＜取消相手先は対象外=チェックの場合＞
                        If blnAitesakiTaisyouGai.Equals(True) Then
                            .AppendLine("           AND ")
                            .AppendLine("	        MKE.torikesi = 0 ")   '--取消
                        End If
                        .AppendLine("			GROUP BY ")
                        .AppendLine("				MKE.keiretu_cd ")
                End Select

            End If
            .AppendLine("	) AS SUB ")
            .AppendLine("	CROSS JOIN  ")
            .AppendLine("	( ")
            .AppendLine("		SELECT  ")
            .AppendLine("			tys_kaisya_cd ")
            .AppendLine("			,jigyousyo_cd ")
            .AppendLine("			,torikesi ")
            .AppendLine("			,tys_kaisya_mei  ")
            .AppendLine("		FROM  ")
            .AppendLine("			m_tyousakaisya WITH(READCOMMITTED)  ")
            .AppendLine("		WHERE ")
            .AppendLine("           1=1 ")
            '｢調査会社コード｣
            If Not strTysKaisyaCd.Trim.Equals(String.Empty) Then
                .AppendLine("           AND ")
                .AppendLine("			tys_kaisya_cd = @tys_kaisya_cd ") '--調査会社コード
                .AppendLine("			AND ")
                .AppendLine("			jigyousyo_cd = @jigyousyo_cd ") '--事業所コード
            End If
            .AppendLine("	) AS SUB_MTK ") '--サブ調査会社M ")
            .AppendLine("	LEFT OUTER JOIN  ")
            .AppendLine("	m_genka as MG WITH(READCOMMITTED)  ")
            .AppendLine("	ON  ")
            .AppendLine("		SUB_MSH.syouhin_cd = MG.syouhin_cd ")   '--サブ商品M.商品コード = 原価M.商品コード  ")
            .AppendLine("		AND  ")
            .AppendLine("		SUB_MTH.tys_houhou_no = MG.tys_houhou_no ")   '--サブ商品M.商品コード = 原価M.商品コード  ")
            .AppendLine("		AND  ")
            .AppendLine("		SUB.aitesaki_syubetu = MG.aitesaki_syubetu ")   '--サブ.相手先種別=原価M.相手先種別  ")
            .AppendLine("		AND  ")
            .AppendLine("		SUB.aitesaki_cd = MG.aitesaki_cd ") '--サブ.相手先コード=原価M.相手先コード  ")
            .AppendLine("		AND  ")
            .AppendLine("		MG.tys_kaisya_cd = SUB_MTK.tys_kaisya_cd ") '--原価M.調査会社コード = サブ調査会社M.調査会社コード  ")
            .AppendLine("		AND  ")
            .AppendLine("		MG.jigyousyo_cd = SUB_MTK.jigyousyo_cd ")   '--原価M.事業所コード = サブ調査会社M.事業所コード  ")
            .AppendLine("		  ")
            .AppendLine("ORDER BY  ")
            .AppendLine("  ")
            .AppendLine("SUB_MTK.tys_kaisya_cd ")    '--調査会社コード  ")
            .AppendLine(",SUB_MTK.jigyousyo_cd ")    '--事業所コード  ")
            .AppendLine(",SUB.aitesaki_syubetu ")    '--相手先種別  ")
            .AppendLine(",SUB.aitesaki_cd ") '--相手先コード  ")
            .AppendLine(",SUB_MSH.syouhin_cd ")  '--商品コード  ")
            .AppendLine(",SUB_MTH.tys_houhou_no ")   '--調査方法NO ")
        End With

        paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 4, Left(strTysKaisyaCd, 4)))
        paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, IIf(strTysKaisyaCd.Length > 4, Mid(strTysKaisyaCd, 5, 6), String.Empty)))
        If Not strAtesakiCdFrom.Trim.Equals(String.Empty) Then
            paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 8, CInt(strAtesakiSyubetu)))
        End If
        paramList.Add(MakeParam("@aitesaki_cd_from", SqlDbType.VarChar, 5, strAtesakiCdFrom))
        paramList.Add(MakeParam("@aitesaki_cd_to", SqlDbType.VarChar, 5, strAtesakiCdTo))
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))
        If Not strHouhouCd.Trim.Equals(String.Empty) Then
            paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 8, CInt(strHouhouCd)))
        End If

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtCSV", paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function

    ''' <summary>未設定も含む原価CSV件数を取得する</summary>
    ''' <returns>未設定も含む原価CSV件数</returns>
    Public Function SelMiSeteiGenkaCSVCount(ByVal strTysKaisyaCd As String, ByVal strAtesakiSyubetu As String, ByVal strAtesakiCdFrom As String, ByVal strAtesakiCdTo As String, ByVal strSyouhinCd As String, ByVal strHouhouCd As String, ByVal blnKensakuTaisyouGai As Boolean, ByVal blnAitesakiTaisyouGai As Boolean) As Long

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT  ")
            .AppendLine("   COUNT_BIG(*) ")
            .AppendLine("FROM ")
            .AppendLine("	( ")
            .AppendLine("		SELECT ")
            .AppendLine("			tys_houhou_no ")
            .AppendLine("			,tys_houhou_mei ")
            .AppendLine("		FROM ")
            .AppendLine("			m_tyousahouhou WITH(READCOMMITTED) ")
            .AppendLine("		WHERE ")
            .AppendLine("			ISNULL(genka_settei_fuyou_flg,0) = 0 ") '--原価設定不要フラグ 
            '＜調査方法=入力の場合＞
            If Not strHouhouCd.Trim.Equals(String.Empty) Then
                .AppendLine("			AND ")
                .AppendLine("			tys_houhou_no = @tys_houhou_no ")
            End If
            .AppendLine("	) AS SUB_MTH ") '--サブ調査方法M 
            .AppendLine("	CROSS JOIN ")
            .AppendLine("	( ")
            .AppendLine("		SELECT ")
            .AppendLine("			syouhin_cd ")
            .AppendLine("			,syouhin_mei ")
            .AppendLine("		FROM ")
            .AppendLine("			m_syouhin WITH(READCOMMITTED) ")
            .AppendLine("		WHERE ")
            .AppendLine("			souko_cd = '100' ")
            .AppendLine("			AND ")
            .AppendLine("			torikesi = 0 ")
            '＜商品コード=入力の場合＞
            If Not strSyouhinCd.Trim.Equals(String.Empty) Then
                .AppendLine("			AND ")
                .AppendLine("			syouhin_cd = @syouhin_cd ")
            End If
            .AppendLine("	) AS SUB_MSH ") '--サブ商品M ")
            .AppendLine("	CROSS JOIN ")
            .AppendLine("	( ")
            If strAtesakiSyubetu.Trim.Equals(String.Empty) Then
                .AppendLine("		SELECT ")
                .AppendLine("			0			AS aitesaki_syubetu ")  '--相手先種別
                .AppendLine("			,'ALL'			AS aitesaki_cd ")   '--相手先コード
                .AppendLine("			,'相手先なし'		AS aitesaki_mei ")  '--相手先名
                .AppendLine("			,0			AS torikesi	 ") '--取消")
                .AppendLine("		UNION ALL ")
                .AppendLine("		SELECT ")
                .AppendLine("			1			AS aitesaki_syubetu ")
                .AppendLine("			,MKA.kameiten_cd	AS aitesaki_cd ")   '--加盟店コード
                .AppendLine("			,MKA.kameiten_mei1	AS aitesaki_mei ")  '--加盟店名1
                .AppendLine("			,MKA.torikesi		AS torikesi ")
                .AppendLine("		FROM ")
                .AppendLine("			m_kameiten AS MKA WITH(READCOMMITTED) ")    '--加盟店マスタ
                '＜取消相手先は対象外=チェックの場合＞
                If blnAitesakiTaisyouGai.Equals(True) Then
                    .AppendLine("       WHERE ")
                    .AppendLine("	        MKA.torikesi = 0 ")   '--取消
                End If
                .AppendLine("		UNION ALL ")
                .AppendLine("		SELECT ")
                .AppendLine("			3			AS aitesaki_syubetu ")
                .AppendLine("			,'JIO'	AS aitesaki_cd ")
                .AppendLine("			,'JIO先'	AS aitesaki_mei ")
                .AppendLine("			,'0'		AS torikesi ")
                .AppendLine("		UNION ALL ")
                .AppendLine("		SELECT ")
                .AppendLine("			7			        AS aitesaki_syubetu ")
                .AppendLine("			,MKE.keiretu_cd	    AS aitesaki_cd ")   '--系列コード
                .AppendLine("			,MIN(MKE.keiretu_mei)	AS aitesaki_mei ")  '--系列名
                .AppendLine("			,MIN(MKE.torikesi)		AS torikesi ")
                .AppendLine("		FROM ")
                .AppendLine("			m_keiretu AS MKE WITH(READCOMMITTED) ") '--系列マスタ
                '＜取消相手先は対象外=チェックの場合＞
                If blnAitesakiTaisyouGai.Equals(True) Then
                    .AppendLine("       WHERE ")
                    .AppendLine("	        MKE.torikesi = 0 ")   '--取消
                End If
                .AppendLine("           GROUP BY ")
                .AppendLine("               MKE.keiretu_cd ")
            Else
                Select Case strAtesakiSyubetu
                    Case "0"
                        .AppendLine("		SELECT ")
                        .AppendLine("			0			AS aitesaki_syubetu ")  '--相手先種別 
                        .AppendLine("			,'ALL'			AS aitesaki_cd ")   '--相手先コード 
                        .AppendLine("			,'相手先なし'		AS aitesaki_mei ")  '--相手先名 
                    Case "3"
                        .AppendLine("		SELECT ")
                        .AppendLine("			3			AS aitesaki_syubetu ")
                        .AppendLine("			,'JIO'	AS aitesaki_cd ")
                        .AppendLine("			,'JIO先'	AS aitesaki_mei ")
                    Case "1"
                        .AppendLine("		SELECT ")
                        .AppendLine("			1					AS aitesaki_syubetu ")
                        .AppendLine("			,MKA.kameiten_cd	AS aitesaki_cd ")   '--加盟店コード 
                        .AppendLine("			,MKA.kameiten_mei1	AS aitesaki_mei ")  '--加盟店名1 
                        .AppendLine("		FROM ")
                        .AppendLine("			m_kameiten AS MKA WITH(READCOMMITTED) ")    '--加盟店マスタ 
                        .AppendLine("		WHERE ")
                        .AppendLine("           1=1 ")
                        '＜相手先コードFROM、相手先コードTO両方が入力されている場合＞
                        If (Not strAtesakiCdFrom.Trim.Equals(String.Empty)) AndAlso (Not strAtesakiCdTo.Trim.Equals(String.Empty)) Then
                            .AppendLine("           AND ")
                            .AppendLine("			MKA.kameiten_cd BETWEEN @aitesaki_cd_from AND @aitesaki_cd_to ")
                        Else
                            '＜相手先コードFROMのみあるいは、相手先コードTOが入力されている場合＞
                            If Not strAtesakiCdFrom.Trim.Equals(String.Empty) Then
                                .AppendLine("           AND ")
                                .AppendLine("	        MKA.kameiten_cd = @aitesaki_cd_from ")   '--加盟店コード 
                            Else
                                If Not strAtesakiCdTo.Trim.Equals(String.Empty) Then
                                    .AppendLine("           AND ")
                                    .AppendLine("	        MKA.kameiten_cd = @aitesaki_cd_to ")   '--加盟店コード
                                End If
                            End If
                        End If
                        '＜取消相手先は対象外=チェックの場合＞
                        If blnAitesakiTaisyouGai.Equals(True) Then
                            .AppendLine("           AND ")
                            .AppendLine("	        MKA.torikesi = 0 ")   '--取消
                        End If
                    Case "7"
                        .AppendLine("			SELECT ")
                        .AppendLine("				7			 AS aitesaki_syubetu ")
                        .AppendLine("				,MKE.keiretu_cd	 AS aitesaki_cd ")  '--系列コード 
                        .AppendLine("				,MIN(MKE.keiretu_mei)	AS aitesaki_mei ")  '--系列名 
                        .AppendLine("			FROM ")
                        .AppendLine("				m_keiretu AS MKE WITH(READCOMMITTED) ") '--系列マスタ 
                        .AppendLine(" ")
                        .AppendLine("			WHERE ")
                        .AppendLine("           1=1 ")
                        '＜相手先コードFROM、相手先コードTO両方が入力されている場合＞
                        If (Not strAtesakiCdFrom.Trim.Equals(String.Empty)) AndAlso (Not strAtesakiCdTo.Trim.Equals(String.Empty)) Then
                            .AppendLine("           AND ")
                            .AppendLine("           MKE.keiretu_cd BETWEEN @aitesaki_cd_from AND @aitesaki_cd_to ")
                        Else
                            '＜相手先コードFROMのみあるいは、相手先コードTOが入力されている場合＞
                            If Not strAtesakiCdFrom.Trim.Equals(String.Empty) Then
                                .AppendLine("           AND ")
                                .AppendLine("	        MKE.keiretu_cd = @aitesaki_cd_from ")   '--系列コード
                            Else
                                If Not strAtesakiCdTo.Trim.Equals(String.Empty) Then
                                    .AppendLine("           AND ")
                                    .AppendLine("           MKE.keiretu_cd = @aitesaki_cd_to ")   '--系列コード
                                End If
                            End If
                        End If
                        '＜取消相手先は対象外=チェックの場合＞
                        If blnAitesakiTaisyouGai.Equals(True) Then
                            .AppendLine("           AND ")
                            .AppendLine("	        MKE.torikesi = 0 ")   '--取消
                        End If
                        .AppendLine("			GROUP BY ")
                        .AppendLine("				MKE.keiretu_cd ")
                End Select
            End If
            .AppendLine("	) AS SUB ")
            .AppendLine("	CROSS JOIN  ")
            .AppendLine("	( ")
            .AppendLine("		SELECT  ")
            .AppendLine("			tys_kaisya_cd ")
            .AppendLine("			,jigyousyo_cd ")
            .AppendLine("			,torikesi ")
            .AppendLine("			,tys_kaisya_mei  ")
            .AppendLine("		FROM  ")
            .AppendLine("			m_tyousakaisya WITH(READCOMMITTED)  ")
            .AppendLine("		WHERE ")
            .AppendLine("           1=1 ")
            '｢調査会社コード｣
            If Not strTysKaisyaCd.Trim.Equals(String.Empty) Then
                .AppendLine("           AND ")
                .AppendLine("			tys_kaisya_cd = @tys_kaisya_cd ") '--調査会社コード
                .AppendLine("			AND ")
                .AppendLine("			jigyousyo_cd = @jigyousyo_cd ") '--事業所コード
            End If
            .AppendLine("	) AS SUB_MTK ") '--サブ調査会社M ")
            .AppendLine("	LEFT OUTER JOIN  ")
            .AppendLine("	m_genka as MG WITH(READCOMMITTED)  ")
            .AppendLine("	ON  ")
            .AppendLine("		SUB_MSH.syouhin_cd = MG.syouhin_cd ")   '--サブ商品M.商品コード = 原価M.商品コード  ")
            .AppendLine("		AND  ")
            .AppendLine("		SUB_MTH.tys_houhou_no = MG.tys_houhou_no ")   '--サブ商品M.商品コード = 原価M.商品コード  ")
            .AppendLine("		AND  ")
            .AppendLine("		SUB.aitesaki_syubetu = MG.aitesaki_syubetu ")   '--サブ.相手先種別=原価M.相手先種別  ")
            .AppendLine("		AND  ")
            .AppendLine("		SUB.aitesaki_cd = MG.aitesaki_cd ") '--サブ.相手先コード=原価M.相手先コード  ")
            .AppendLine("		AND  ")
            .AppendLine("		MG.tys_kaisya_cd = SUB_MTK.tys_kaisya_cd ") '--原価M.調査会社コード = サブ調査会社M.調査会社コード  ")
            .AppendLine("		AND  ")
            .AppendLine("		MG.jigyousyo_cd = SUB_MTK.jigyousyo_cd ")   '--原価M.事業所コード = サブ調査会社M.事業所コード  ")
        End With

        paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 4, Left(strTysKaisyaCd, 4)))
        paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, IIf(strTysKaisyaCd.Length > 4, Mid(strTysKaisyaCd, 5, 6), String.Empty)))
        If Not strAtesakiCdFrom.Trim.Equals(String.Empty) Then
            paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 8, CInt(strAtesakiSyubetu)))
        End If
        paramList.Add(MakeParam("@aitesaki_cd_from", SqlDbType.VarChar, 5, strAtesakiCdFrom))
        paramList.Add(MakeParam("@aitesaki_cd_to", SqlDbType.VarChar, 5, strAtesakiCdTo))
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))
        If Not strHouhouCd.Trim.Equals(String.Empty) Then
            paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 8, CInt(strHouhouCd)))
        End If

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtCSV", paramList.ToArray)

        Return CLng(dsReturn.Tables(0).Rows(0).Item(0))

    End Function


    ''' <summary>原価情報CSVを取得する</summary>
    ''' <returns>原価情報CSVデータテーブル</returns>
    ''' <history>2011/02/28　車龍(大連情報システム部)　新規作成</history>
    Public Function SelGenkaJyouhouCSV(ByVal strTysKaisyaCd As String, ByVal strAtesakiSyubetu As String, ByVal strAtesakiCdFrom As String, ByVal strAtesakiCdTo As String, ByVal strSyouhinCd As String, ByVal strHouhouCd As String, ByVal blnKensakuTaisyouGai As Boolean, ByVal blnAitesakiTaisyouGai As Boolean) As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	RIGHT(CONVERT(varchar(10),GETDATE(),112),4) + REPLACE(CONVERT(varchar(8),GETDATE(),108),':','') ")
            .AppendLine("	,ISNULL(MG.tys_kaisya_cd,'') ")
            .AppendLine("	,ISNULL(MG.jigyousyo_cd,'') ")
            .AppendLine("	,ISNULL(MTS.tys_kaisya_mei,'') ")
            .AppendLine("	,ISNULL(MG.aitesaki_syubetu,'') ")
            .AppendLine("	,ISNULL(MG.aitesaki_cd,'') ")
            .AppendLine("	,ISNULL(SUB.aitesaki_mei,'') ")
            .AppendLine("	,ISNULL(MG.syouhin_cd,'') ")
            .AppendLine("	,ISNULL(MS.syouhin_mei,'') ")
            .AppendLine("	,ISNULL(MG.tys_houhou_no,'') ")
            .AppendLine("	,ISNULL(MTH.tys_houhou_mei,'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.torikesi),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk1),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg1),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk2),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg2),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk3),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg3),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk4),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg4),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk5),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg5),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk6),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg6),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk7),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg7),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk8),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg8),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk9),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg9),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk10),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg10),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk11t19),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg11t19),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk20t29),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg20t29),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk30t39),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg30t39),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk40t49),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg40t49),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk50t),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg50t),'') ")
            .AppendLine("FROM ")
            .AppendLine("	m_genka AS MG WITH(READCOMMITTED) ")    '--原価M
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	( ")
            .AppendLine("		SELECT ")
            .AppendLine("			0			AS aitesaki_syubetu ")  '--相手先種別
            .AppendLine("			,'ALL'			AS aitesaki_cd ")   '--相手先コード
            .AppendLine("			,'相手先なし'		AS aitesaki_mei ")  '--相手先名
            .AppendLine("			,0			AS torikesi	 ") '--取消")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			3			AS aitesaki_syubetu ")
            .AppendLine("			,'JIO'	AS aitesaki_cd ")
            .AppendLine("			,'JIO先'	AS aitesaki_mei ")
            .AppendLine("			,0			AS torikesi	 ") '--取消")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			1			AS aitesaki_syubetu ")
            .AppendLine("			,MKA.kameiten_cd	AS aitesaki_cd ")   '--加盟店コード
            .AppendLine("			,MKA.kameiten_mei1	AS aitesaki_mei ")  '--加盟店名1
            .AppendLine("			,MKA.torikesi		AS torikesi ")
            .AppendLine("		FROM ")
            .AppendLine("			m_kameiten AS MKA WITH(READCOMMITTED) ")    '--加盟店マスタ
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			7			        AS aitesaki_syubetu ")
            .AppendLine("			,MKE.keiretu_cd	    AS aitesaki_cd ")   '--系列コード
            .AppendLine("			,MIN(MKE.keiretu_mei)	AS aitesaki_mei ")  '--系列名
            .AppendLine("			,MIN(MKE.torikesi)		AS torikesi ")
            .AppendLine("		FROM ")
            .AppendLine("			m_keiretu AS MKE WITH(READCOMMITTED) ") '--系列マスタ
            .AppendLine("           GROUP BY ")
            .AppendLine("               MKE.keiretu_cd ")
            .AppendLine("	) AS SUB ")
            .AppendLine("	ON ")
            .AppendLine("		MG.aitesaki_syubetu = SUB.aitesaki_syubetu ")
            .AppendLine("		AND ")
            .AppendLine("		MG.aitesaki_cd = SUB.aitesaki_cd ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_tyousakaisya AS MTS WITH(READCOMMITTED) ")    '--調査会社マスタ
            .AppendLine("	ON ")
            .AppendLine("		MG.tys_kaisya_cd  =  MTS.tys_kaisya_cd ")
            .AppendLine("		AND ")
            .AppendLine("		MG.jigyousyo_cd  =  MTS.jigyousyo_cd ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_syouhin AS MS WITH(READCOMMITTED) ")  '--商品マスタ
            .AppendLine("	ON ")
            .AppendLine("		MG.syouhin_cd  =  MS.syouhin_cd ")
            .AppendLine("		AND ")
            .AppendLine("		MS.souko_cd  =  '100' ")    '--倉庫コード
            .AppendLine("		AND ")
            .AppendLine("		MS.torikesi = 0 ")
            .AppendLine("	LEFT JOIN")
            .AppendLine("	m_tyousahouhou AS MTH WITH(READCOMMITTED) ")    '--調査方法マスタ
            .AppendLine("		ON ")
            .AppendLine("		MG.tys_houhou_no  =  MTH.tys_houhou_no ")   '--調査方法NO
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_kakutyou_meisyou AS MKM WITH(READCOMMITTED) ")    '--拡張名称マスタ
            .AppendLine("	ON ")
            .AppendLine("		MG.aitesaki_syubetu=MKM.code ") '--ｺｰﾄﾞ
            .AppendLine("		AND ")
            .AppendLine("		MKM.meisyou_syubetu='22' ") '--名称種別
            .AppendLine("WHERE ")
            '｢調査会社コード｣
            If Not strTysKaisyaCd.Trim.Equals(String.Empty) Then
                .AppendLine("	MG.tys_kaisya_cd = @tys_kaisya_cd ")   '--調査会社コード
                .AppendLine("	AND")
                .AppendLine("	MG.jigyousyo_cd = @jigyousyo_cd ")   '--事業所コード
                .AppendLine("	AND ")
            End If

            If Not strAtesakiSyubetu.Trim.Equals(String.Empty) Then
                .AppendLine("	MG.aitesaki_syubetu = @aitesaki_syubetu ")   '--相手先種別
                .AppendLine("	AND")
                If (Not strAtesakiCdFrom.Trim.Equals(String.Empty)) AndAlso (Not strAtesakiCdTo.Trim.Equals(String.Empty)) Then
                    '相手先コードFROM、相手先コードTO両方が入力されている場合
                    .AppendLine("	MG.aitesaki_cd BETWEEN @aitesaki_cd_from AND @aitesaki_cd_to ")   '--相手先コード
                    .AppendLine("	AND ")
                Else
                    '相手先コードFROMのみあるいは、相手先コードTOが入力されている場合
                    If Not strAtesakiCdFrom.Trim.Equals(String.Empty) Then
                        .AppendLine("	MG.aitesaki_cd = @aitesaki_cd_from ")   '--相手先コード
                        .AppendLine("	AND ")
                    Else
                        If Not strAtesakiCdTo.Trim.Equals(String.Empty) Then
                            .AppendLine("	MG.aitesaki_cd = @aitesaki_cd_to ")   '--相手先コード
                            .AppendLine("	AND ")
                        End If
                    End If
                End If
            End If

            If Not strSyouhinCd.Trim.Equals(String.Empty) Then
                '商品コード=入力の場合
                .AppendLine("	MG.syouhin_cd = @syouhin_cd ")   '--商品コード
                .AppendLine("	AND")
            End If
            If Not strHouhouCd.Equals(String.Empty) Then
                '調査方法=入力の場合
                .AppendLine("	MG.tys_houhou_no = @tys_houhou_no ")   '--調査方法
                .AppendLine("	AND")
            End If
            If blnKensakuTaisyouGai.Equals(True) Then
                '取消は検索対象外=チェックの場合
                .AppendLine("	MG.torikesi = 0 ")   '--取消
                .AppendLine("	AND")
            End If
            If blnAitesakiTaisyouGai.Equals(True) Then
                '取消相手先は対象外=チェックの場合
                .AppendLine("	SUB.torikesi = 0 ")   '--取消
                .AppendLine("	AND")
            End If
            .AppendLine("	1 = 1 ")
            .AppendLine("ORDER BY ")
            .AppendLine("	MG.tys_kaisya_cd ") '--調査会社コード
            .AppendLine("	,MG.jigyousyo_cd ") '--事業所コード
            .AppendLine("	,MG.aitesaki_syubetu ") '--相手先種別
            .AppendLine("	,MG.aitesaki_cd ")  '--相手先コード
            .AppendLine("	,MG.syouhin_cd ")   '--商品コード
            .AppendLine("	,MG.tys_houhou_no ")    '--調査方法NO
        End With

        paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 4, Left(strTysKaisyaCd, 4)))
        paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, IIf(strTysKaisyaCd.Length > 4, Mid(strTysKaisyaCd, 5, 6), String.Empty)))
        If Not strAtesakiSyubetu.Trim.Equals(String.Empty) Then
            paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 8, CInt(strAtesakiSyubetu)))
        End If
        paramList.Add(MakeParam("@aitesaki_cd_from", SqlDbType.VarChar, 5, strAtesakiCdFrom))
        paramList.Add(MakeParam("@aitesaki_cd_to", SqlDbType.VarChar, 5, strAtesakiCdTo))
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))
        If Not strHouhouCd.Trim.Equals(String.Empty) Then
            paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 8, CInt(strHouhouCd)))
        End If

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtCSV", paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function

    ''' <summary>アップロード管理を取得する</summary>
    ''' <returns>アップロード管理データテーブル</returns>
    ''' <history>2011/02/24　車龍(大連情報システム部)　新規作成</history>
    Public Function SelInputKanri() As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'SQL文
        With commandTextSb
            .AppendLine("SELECT TOP 100 ")
            .AppendLine("	CONVERT(varchar(100),syori_datetime,111) + ' ' + CONVERT(varchar(100),syori_datetime,108) AS syori_datetime ") '処理日時
            .AppendLine("	,CONVERT(varchar(100),syori_datetime,112) + REPLACE(CONVERT(varchar(100),syori_datetime,114),':','') AS torikomi_datetime") '取込日時
            .AppendLine("	,nyuuryoku_file_mei ")  '入力ファイル名
            .AppendLine("	,error_umu ")           'エラー有無
            .AppendLine("    ,edi_jouhou_sakusei_date ")    'EDI情報作成日
            .AppendLine("FROM ")
            .AppendLine("	t_upload_kanri WITH(READCOMMITTED) ")       'アップロード管理テーブル
            .AppendLine("WHERE ")
            .AppendLine("	file_kbn = 2 ")         'ファイル区分
            .AppendLine("ORDER BY ")
            .AppendLine("	syori_datetime DESC ")  '処理日時(降順)
        End With

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtInputKanri")

        Return dsReturn.Tables(0)

    End Function

    ''' <summary>アップロード管理の件数を取得する</summary>
    ''' <returns>アップロード管理の件数</returns>
    ''' <history>2011/02/24　車龍(大連情報システム部)　新規作成</history>
    Public Function SelInputKanriCount() As Integer

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	COUNT(syori_datetime) ")    '件数
            .AppendLine("FROM ")
            .AppendLine("	t_upload_kanri WITH(READCOMMITTED) ")           'アップロード管理テーブル
            .AppendLine("WHERE ")
            .AppendLine("	file_kbn = 2 ")             'ファイル区分
        End With

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtInputKanri")

        Return CInt(dsReturn.Tables(0).Rows(0).Item(0).ToString)

    End Function

    ''' <summary>調査会社名を取得する</summary>
    ''' <returns>調査会社名データテーブル</returns>
    ''' <history>2011/02/24　車龍(大連情報システム部)　新規作成</history>
    Public Function SelTyousaKaisyaMeiInput(ByVal strTyousaKaisyaCd As String, ByVal strJigyousyoCd As String) As Boolean

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT  ")
            .AppendLine("	tys_kaisya_mei ")
            .AppendLine("FROM  ")
            .AppendLine("	m_tyousakaisya WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	tys_kaisya_cd = @tys_kaisya_cd ")
            .AppendLine("	AND ")
            .AppendLine("	jigyousyo_cd = @jigyousyo_cd ")
        End With

        paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, strTyousaKaisyaCd))
        paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, strJigyousyoCd))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtTyousaKaisyaMei", paramList.ToArray)

        '戻り値
        If dsReturn.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>相手先種別を取得する</summary>
    ''' <history>2011/03/02　車龍(大連情報システム部)　新規作成</history>
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
            .AppendLine("			3			AS aitesaki_syubetu ")
            .AppendLine("			,'JIO'	AS aitesaki_cd ")
            .AppendLine("			,'JIO先'	AS aitesaki_mei ")
            .AppendLine("			,0			AS torikesi	 ") '--取消")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			1					AS aitesaki_syubetu ")
            .AppendLine("			,MKA.kameiten_cd	AS aitesaki_cd ")   '--加盟店コード
            .AppendLine("			,MKA.kameiten_mei1	AS aitesaki_mei ")  '--加盟店名1
            .AppendLine("			,MKA.torikesi		AS torikesi ")
            .AppendLine("		FROM ")
            .AppendLine("			m_kameiten AS MKA WITH(READCOMMITTED) ")    '--加盟店マスタ
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
    ''' <history>2011/03/02　車龍(大連情報システム部)　新規作成</history>
    Public Function SelSyouhinCdInput(ByVal strSyouhinCd As String) As Boolean

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
    ''' <history>2011/03/02　車龍(大連情報システム部)　新規作成</history>
    Public Function SelTyousahouhouNoInput(ByVal intTyousahouhouNo As Integer) As Boolean

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT  ")
            .AppendLine("	tys_houhou_no ")
            .AppendLine("FROM  ")
            .AppendLine("	m_tyousahouhou WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	tys_houhou_no = @TyousahouhouNo ")
        End With

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

    ''' <summary>原価データを取得する</summary>
    ''' <returns>原価データテーブル</returns>
    ''' <history>2011/03/02　車龍(大連情報システム部)　新規作成</history>
    Public Function SelGenkaInputJyouhou(ByVal strTyousaKaisyaCd As String, ByVal strJigyousyoCd As String, ByVal intAitesakiSyubetu As Integer, ByVal strAitesakiCd As String, ByVal strSyouhinCd As String, ByVal strTyousaHouhouNo As Integer) As Boolean

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT  DISTINCT ")
            .AppendLine("	tys_kaisya_cd ")
            .AppendLine("FROM ")
            .AppendLine("	m_genka WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	tys_kaisya_cd = @tys_kaisya_cd ")
            .AppendLine("	AND ")
            .AppendLine("	jigyousyo_cd = @jigyousyo_cd ")
            .AppendLine("	AND ")
            .AppendLine("	aitesaki_syubetu = @aitesaki_syubetu ")
            .AppendLine("	AND ")
            .AppendLine("	aitesaki_cd = @aitesaki_cd ")
            .AppendLine("	AND ")
            .AppendLine("	syouhin_cd = @syouhin_cd ")
            .AppendLine("	AND ")
            .AppendLine("	tys_houhou_no = @tys_houhou_no ")
        End With

        paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, strTyousaKaisyaCd))
        paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, strJigyousyoCd))
        paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 8, intAitesakiSyubetu))
        paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, strAitesakiCd))
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))
        paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 8, strTyousaHouhouNo))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtGenkaJyouhou", paramList.ToArray)

        '戻り値
        If dsReturn.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function


    ''' <summary>原価エラー情報テテーブルを登録する</summary>
    ''' <history>2011/03/02　車龍(大連情報システム部)　新規作成</history>
    Public Function InsGenkaError(ByVal dtError As Data.DataTable) As Boolean
        '戻り値
        Dim InsCount As Integer = 0

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine("INSERT INTO  ")
            .AppendLine("	m_genka_error WITH(UPDLOCK) ")
            .AppendLine("	( ")
            .AppendLine("		edi_jouhou_sakusei_date ")
            .AppendLine("		,gyou_no ")
            .AppendLine("		,syori_datetime ")
            .AppendLine("		,tys_kaisya_cd ")
            .AppendLine("		,jigyousyo_cd ")
            .AppendLine("		,tys_kaisya_mei ")
            .AppendLine("		,aitesaki_syubetu ")
            .AppendLine("		,aitesaki_cd ")
            .AppendLine("		,aitesaki_mei ")
            .AppendLine("		,syouhin_cd ")
            .AppendLine("		,syouhin_mei ")
            .AppendLine("		,tys_houhou_no ")
            .AppendLine("		,tys_houhou ")
            .AppendLine("		,torikesi ")
            .AppendLine("		,tou_kkk1 ")
            .AppendLine("		,tou_kkk_henkou_flg1 ")
            .AppendLine("		,tou_kkk2 ")
            .AppendLine("		,tou_kkk_henkou_flg2 ")
            .AppendLine("		,tou_kkk3 ")
            .AppendLine("		,tou_kkk_henkou_flg3 ")
            .AppendLine("		,tou_kkk4 ")
            .AppendLine("		,tou_kkk_henkou_flg4 ")
            .AppendLine("		,tou_kkk5 ")
            .AppendLine("		,tou_kkk_henkou_flg5 ")
            .AppendLine("		,tou_kkk6 ")
            .AppendLine("		,tou_kkk_henkou_flg6 ")
            .AppendLine("		,tou_kkk7 ")
            .AppendLine("		,tou_kkk_henkou_flg7 ")
            .AppendLine("		,tou_kkk8 ")
            .AppendLine("		,tou_kkk_henkou_flg8 ")
            .AppendLine("		,tou_kkk9 ")
            .AppendLine("		,tou_kkk_henkou_flg9 ")
            .AppendLine("		,tou_kkk10 ")
            .AppendLine("		,tou_kkk_henkou_flg10 ")
            .AppendLine("		,tou_kkk11t19 ")
            .AppendLine("		,tou_kkk_henkou_flg11t19 ")
            .AppendLine("		,tou_kkk20t29 ")
            .AppendLine("		,tou_kkk_henkou_flg20t29 ")
            .AppendLine("		,tou_kkk30t39 ")
            .AppendLine("		,tou_kkk_henkou_flg30t39 ")
            .AppendLine("		,tou_kkk40t49 ")
            .AppendLine("		,tou_kkk_henkou_flg40t49 ")
            .AppendLine("		,tou_kkk50t ")
            .AppendLine("		,tou_kkk_henkou_flg50t ")
            .AppendLine("		,add_login_user_id ")
            .AppendLine("		,add_datetime ")
            .AppendLine("		,upd_login_user_id ")
            .AppendLine("		,upd_datetime ")
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("( ")
            .AppendLine("	@edi_jouhou_sakusei_date ")
            .AppendLine("	,@gyou_no ")
            .AppendLine("	,@syori_datetime ")
            .AppendLine("	,@tys_kaisya_cd ")
            .AppendLine("	,@jigyousyo_cd ")
            .AppendLine("	,@tys_kaisya_mei ")
            .AppendLine("	,@aitesaki_syubetu ")
            .AppendLine("	,@aitesaki_cd ")
            .AppendLine("	,@aitesaki_mei ")
            .AppendLine("	,@syouhin_cd ")
            .AppendLine("	,@syouhin_mei ")
            .AppendLine("	,@tys_houhou_no ")
            .AppendLine("	,@tys_houhou ")
            .AppendLine("	,@torikesi ")
            .AppendLine("	,@tou_kkk1 ")
            .AppendLine("	,@tou_kkk_henkou_flg1 ")
            .AppendLine("	,@tou_kkk2 ")
            .AppendLine("	,@tou_kkk_henkou_flg2 ")
            .AppendLine("	,@tou_kkk3 ")
            .AppendLine("	,@tou_kkk_henkou_flg3 ")
            .AppendLine("	,@tou_kkk4 ")
            .AppendLine("	,@tou_kkk_henkou_flg4 ")
            .AppendLine("	,@tou_kkk5 ")
            .AppendLine("	,@tou_kkk_henkou_flg5 ")
            .AppendLine("	,@tou_kkk6 ")
            .AppendLine("	,@tou_kkk_henkou_flg6 ")
            .AppendLine("	,@tou_kkk7 ")
            .AppendLine("	,@tou_kkk_henkou_flg7 ")
            .AppendLine("	,@tou_kkk8 ")
            .AppendLine("	,@tou_kkk_henkou_flg8 ")
            .AppendLine("	,@tou_kkk9 ")
            .AppendLine("	,@tou_kkk_henkou_flg9 ")
            .AppendLine("	,@tou_kkk10 ")
            .AppendLine("	,@tou_kkk_henkou_flg10 ")
            .AppendLine("	,@tou_kkk11t19 ")
            .AppendLine("	,@tou_kkk_henkou_flg11t19 ")
            .AppendLine("	,@tou_kkk20t29 ")
            .AppendLine("	,@tou_kkk_henkou_flg20t29 ")
            .AppendLine("	,@tou_kkk30t39 ")
            .AppendLine("	,@tou_kkk_henkou_flg30t39 ")
            .AppendLine("	,@tou_kkk40t49 ")
            .AppendLine("	,@tou_kkk_henkou_flg40t49 ")
            .AppendLine("	,@tou_kkk50t ")
            .AppendLine("	,@tou_kkk_henkou_flg50t ")
            .AppendLine("	,@add_login_user_id ")
            .AppendLine("	,GETDATE() ")
            .AppendLine("	,NULL ")
            .AppendLine("	,NULL ")
            .AppendLine(") ")
        End With

        For i As Integer = 0 To dtError.Rows.Count - 1

            'パラメータの設定 
            paramList.Clear()
            paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, dtError.Rows(i).Item(0).ToString.Trim))
            paramList.Add(MakeParam("@gyou_no", SqlDbType.Int, 8, CInt(dtError.Rows(i).Item(42).Trim)))
            paramList.Add(MakeParam("@syori_datetime", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(dtError.Rows(i).Item(43).ToString.Trim))))
            paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, dtError.Rows(i).Item(1).ToString.Trim))
            paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, dtError.Rows(i).Item(2).ToString.Trim))
            paramList.Add(MakeParam("@tys_kaisya_mei", SqlDbType.VarChar, 40, dtError.Rows(i).Item(3).ToString.Trim))
            paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.VarChar, 5, dtError.Rows(i).Item(4).ToString.Trim))
            paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, dtError.Rows(i).Item(5).ToString.Trim))
            paramList.Add(MakeParam("@aitesaki_mei", SqlDbType.VarChar, 40, dtError.Rows(i).Item(6).ToString.Trim))
            paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtError.Rows(i).Item(7).ToString.Trim))
            paramList.Add(MakeParam("@syouhin_mei", SqlDbType.VarChar, 40, dtError.Rows(i).Item(8).ToString.Trim))
            paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.VarChar, 5, dtError.Rows(i).Item(9).ToString.Trim))
            paramList.Add(MakeParam("@tys_houhou", SqlDbType.VarChar, 32, dtError.Rows(i).Item(10).ToString.Trim))
            paramList.Add(MakeParam("@torikesi", SqlDbType.VarChar, 1, dtError.Rows(i).Item(11).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk1", SqlDbType.VarChar, 10, dtError.Rows(i).Item(12).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg1", SqlDbType.VarChar, 1, dtError.Rows(i).Item(13).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk2", SqlDbType.VarChar, 10, dtError.Rows(i).Item(14).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg2", SqlDbType.VarChar, 1, dtError.Rows(i).Item(15).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk3", SqlDbType.VarChar, 10, dtError.Rows(i).Item(16).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg3", SqlDbType.VarChar, 1, dtError.Rows(i).Item(17).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk4", SqlDbType.VarChar, 10, dtError.Rows(i).Item(18).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg4", SqlDbType.VarChar, 1, dtError.Rows(i).Item(19).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk5", SqlDbType.VarChar, 10, dtError.Rows(i).Item(20).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg5", SqlDbType.VarChar, 1, dtError.Rows(i).Item(21).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk6", SqlDbType.VarChar, 10, dtError.Rows(i).Item(22).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg6", SqlDbType.VarChar, 1, dtError.Rows(i).Item(23).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk7", SqlDbType.VarChar, 10, dtError.Rows(i).Item(24).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg7", SqlDbType.VarChar, 1, dtError.Rows(i).Item(25).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk8", SqlDbType.VarChar, 10, dtError.Rows(i).Item(26).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg8", SqlDbType.VarChar, 1, dtError.Rows(i).Item(27).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk9", SqlDbType.VarChar, 10, dtError.Rows(i).Item(28).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg9", SqlDbType.VarChar, 1, dtError.Rows(i).Item(29).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk10", SqlDbType.VarChar, 10, dtError.Rows(i).Item(30).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg10", SqlDbType.VarChar, 1, dtError.Rows(i).Item(31).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk11t19", SqlDbType.VarChar, 10, dtError.Rows(i).Item(32).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg11t19", SqlDbType.VarChar, 1, dtError.Rows(i).Item(33).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk20t29", SqlDbType.VarChar, 10, dtError.Rows(i).Item(34).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg20t29", SqlDbType.VarChar, 1, dtError.Rows(i).Item(35).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk30t39", SqlDbType.VarChar, 10, dtError.Rows(i).Item(36).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg30t39", SqlDbType.VarChar, 1, dtError.Rows(i).Item(37).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk40t49", SqlDbType.VarChar, 10, dtError.Rows(i).Item(38).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg40t49", SqlDbType.VarChar, 1, dtError.Rows(i).Item(39).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk50t", SqlDbType.VarChar, 10, dtError.Rows(i).Item(40).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg50t", SqlDbType.VarChar, 1, dtError.Rows(i).Item(41).ToString.Trim))
            paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, dtError.Rows(i).Item(44).ToString.Trim))

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

    ''' <summary>原価マスタを登録or更新する</summary>
    ''' <history>2011/03/03　車龍(大連情報システム部)　新規作成</history>
    Public Function InsUpdGenkaMaster(ByVal dtOk As Data.DataTable) As Boolean
        '戻り値
        Dim InsUpdCount As Integer = 0
        '追加用sql文
        Dim strSqlIns As New System.Text.StringBuilder
        '更新用sql文
        Dim strSqlUpd As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        '追加用sql文
        With strSqlIns
            .AppendLine("INSERT INTO  ")
            .AppendLine("	m_genka WITH(UPDLOCK) ")
            .AppendLine("	( ")
            .AppendLine("		tys_kaisya_cd ")
            .AppendLine("		,jigyousyo_cd ")
            .AppendLine("		,aitesaki_syubetu ")
            .AppendLine("		,aitesaki_cd ")
            .AppendLine("		,syouhin_cd ")
            .AppendLine("		,tys_houhou_no ")
            .AppendLine("		,torikesi ")
            .AppendLine("		,tou_kkk1 ")
            .AppendLine("		,tou_kkk_henkou_flg1 ")
            .AppendLine("		,tou_kkk2 ")
            .AppendLine("		,tou_kkk_henkou_flg2 ")
            .AppendLine("		,tou_kkk3 ")
            .AppendLine("		,tou_kkk_henkou_flg3 ")
            .AppendLine("		,tou_kkk4 ")
            .AppendLine("		,tou_kkk_henkou_flg4 ")
            .AppendLine("		,tou_kkk5 ")
            .AppendLine("		,tou_kkk_henkou_flg5 ")
            .AppendLine("		,tou_kkk6 ")
            .AppendLine("		,tou_kkk_henkou_flg6 ")
            .AppendLine("		,tou_kkk7 ")
            .AppendLine("		,tou_kkk_henkou_flg7 ")
            .AppendLine("		,tou_kkk8 ")
            .AppendLine("		,tou_kkk_henkou_flg8 ")
            .AppendLine("		,tou_kkk9 ")
            .AppendLine("		,tou_kkk_henkou_flg9 ")
            .AppendLine("		,tou_kkk10 ")
            .AppendLine("		,tou_kkk_henkou_flg10 ")
            .AppendLine("		,tou_kkk11t19 ")
            .AppendLine("		,tou_kkk_henkou_flg11t19 ")
            .AppendLine("		,tou_kkk20t29 ")
            .AppendLine("		,tou_kkk_henkou_flg20t29 ")
            .AppendLine("		,tou_kkk30t39 ")
            .AppendLine("		,tou_kkk_henkou_flg30t39 ")
            .AppendLine("		,tou_kkk40t49 ")
            .AppendLine("		,tou_kkk_henkou_flg40t49 ")
            .AppendLine("		,tou_kkk50t ")
            .AppendLine("		,tou_kkk_henkou_flg50t ")
            .AppendLine("		,add_login_user_id ")
            .AppendLine("		,add_datetime ")
            .AppendLine("		,upd_login_user_id ")
            .AppendLine("		,upd_datetime ")
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("( ")
            .AppendLine("	@tys_kaisya_cd ")
            .AppendLine("	,@jigyousyo_cd ")
            .AppendLine("	,@aitesaki_syubetu ")
            .AppendLine("	,@aitesaki_cd ")
            .AppendLine("	,@syouhin_cd ")
            .AppendLine("	,@tys_houhou_no ")
            .AppendLine("	,@torikesi ")
            .AppendLine("	,@tou_kkk1 ")
            .AppendLine("	,@tou_kkk_henkou_flg1 ")
            .AppendLine("	,@tou_kkk2 ")
            .AppendLine("	,@tou_kkk_henkou_flg2 ")
            .AppendLine("	,@tou_kkk3 ")
            .AppendLine("	,@tou_kkk_henkou_flg3 ")
            .AppendLine("	,@tou_kkk4 ")
            .AppendLine("	,@tou_kkk_henkou_flg4 ")
            .AppendLine("	,@tou_kkk5 ")
            .AppendLine("	,@tou_kkk_henkou_flg5 ")
            .AppendLine("	,@tou_kkk6 ")
            .AppendLine("	,@tou_kkk_henkou_flg6 ")
            .AppendLine("	,@tou_kkk7 ")
            .AppendLine("	,@tou_kkk_henkou_flg7 ")
            .AppendLine("	,@tou_kkk8 ")
            .AppendLine("	,@tou_kkk_henkou_flg8 ")
            .AppendLine("	,@tou_kkk9 ")
            .AppendLine("	,@tou_kkk_henkou_flg9 ")
            .AppendLine("	,@tou_kkk10 ")
            .AppendLine("	,@tou_kkk_henkou_flg10 ")
            .AppendLine("	,@tou_kkk11t19 ")
            .AppendLine("	,@tou_kkk_henkou_flg11t19 ")
            .AppendLine("	,@tou_kkk20t29 ")
            .AppendLine("	,@tou_kkk_henkou_flg20t29 ")
            .AppendLine("	,@tou_kkk30t39 ")
            .AppendLine("	,@tou_kkk_henkou_flg30t39 ")
            .AppendLine("	,@tou_kkk40t49 ")
            .AppendLine("	,@tou_kkk_henkou_flg40t49 ")
            .AppendLine("	,@tou_kkk50t ")
            .AppendLine("	,@tou_kkk_henkou_flg50t ")
            .AppendLine("	,@add_login_user_id ")
            .AppendLine("	,GETDATE() ")
            .AppendLine("	,NULL ")
            .AppendLine("	,NULL ")
            .AppendLine(") ")
        End With

        '更新用sql文
        With StrSqlUpd
            .AppendLine("UPDATE  ")
            .AppendLine("	m_genka WITH(UPDLOCK) ")
            .AppendLine("SET  ")
            .AppendLine("	torikesi = @torikesi ")
            .AppendLine("	,tou_kkk1 = @tou_kkk1 ")
            .AppendLine("	,tou_kkk_henkou_flg1 = @tou_kkk_henkou_flg1 ")
            .AppendLine("	,tou_kkk2 = @tou_kkk2 ")
            .AppendLine("	,tou_kkk_henkou_flg2 = @tou_kkk_henkou_flg2 ")
            .AppendLine("	,tou_kkk3 = @tou_kkk3 ")
            .AppendLine("	,tou_kkk_henkou_flg3 = @tou_kkk_henkou_flg3 ")
            .AppendLine("	,tou_kkk4 = @tou_kkk4 ")
            .AppendLine("	,tou_kkk_henkou_flg4 = @tou_kkk_henkou_flg4 ")
            .AppendLine("	,tou_kkk5 = @tou_kkk5 ")
            .AppendLine("	,tou_kkk_henkou_flg5 = @tou_kkk_henkou_flg5 ")
            .AppendLine("	,tou_kkk6 = @tou_kkk6 ")
            .AppendLine("	,tou_kkk_henkou_flg6 = @tou_kkk_henkou_flg6 ")
            .AppendLine("	,tou_kkk7 = @tou_kkk7 ")
            .AppendLine("	,tou_kkk_henkou_flg7 = @tou_kkk_henkou_flg7 ")
            .AppendLine("	,tou_kkk8 = @tou_kkk8 ")
            .AppendLine("	,tou_kkk_henkou_flg8 = @tou_kkk_henkou_flg8 ")
            .AppendLine("	,tou_kkk9 = @tou_kkk9 ")
            .AppendLine("	,tou_kkk_henkou_flg9 = @tou_kkk_henkou_flg9 ")
            .AppendLine("	,tou_kkk10 = @tou_kkk10 ")
            .AppendLine("	,tou_kkk_henkou_flg10 = @tou_kkk_henkou_flg10 ")
            .AppendLine("	,tou_kkk11t19 = @tou_kkk11t19 ")
            .AppendLine("	,tou_kkk_henkou_flg11t19 = @tou_kkk_henkou_flg11t19 ")
            .AppendLine("	,tou_kkk20t29 = @tou_kkk20t29 ")
            .AppendLine("	,tou_kkk_henkou_flg20t29 = @tou_kkk_henkou_flg20t29 ")
            .AppendLine("	,tou_kkk30t39 = @tou_kkk30t39 ")
            .AppendLine("	,tou_kkk_henkou_flg30t39 = @tou_kkk_henkou_flg30t39 ")
            .AppendLine("	,tou_kkk40t49 = @tou_kkk40t49 ")
            .AppendLine("	,tou_kkk_henkou_flg40t49 = @tou_kkk_henkou_flg40t49 ")
            .AppendLine("	,tou_kkk50t = @tou_kkk50t ")
            .AppendLine("	,tou_kkk_henkou_flg50t = @tou_kkk_henkou_flg50t ")
            .AppendLine("	,upd_login_user_id = @upd_login_user_id ")
            .AppendLine("	,upd_datetime = GETDATE() ")
            .AppendLine("WHERE ")
            .AppendLine("	tys_kaisya_cd = @tys_kaisya_cd ")
            .AppendLine("	AND ")
            .AppendLine("	jigyousyo_cd = @jigyousyo_cd ")
            .AppendLine("	AND ")
            .AppendLine("	aitesaki_syubetu = @aitesaki_syubetu ")
            .AppendLine("	AND ")
            .AppendLine("	aitesaki_cd = @aitesaki_cd ")
            .AppendLine("	AND ")
            .AppendLine("	syouhin_cd = @syouhin_cd ")
            .AppendLine("	AND ")
            .AppendLine("	tys_houhou_no = @tys_houhou_no ")
        End With

        For i As Integer = 0 To dtOk.Rows.Count - 1
            'パラメータの設定
            paramList.Clear()
            paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, dtOk.Rows(i).Item("tys_kaisya_cd").ToString.Trim))
            paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, dtOk.Rows(i).Item("jigyousyo_cd").ToString.Trim))
            paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 8, dtOk.Rows(i).Item("aitesaki_syubetu").ToString.Trim))
            paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, dtOk.Rows(i).Item("aitesaki_cd").ToString.Trim))
            paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtOk.Rows(i).Item("syouhin_cd").ToString.Trim))
            paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 10, dtOk.Rows(i).Item("tys_houhou_no").ToString.Trim))
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 1, dtOk.Rows(i).Item("torikesi").ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk1", SqlDbType.Int, 10, IIf(dtOk.Rows(i).Item("tou_kkk1").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk1").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg1", SqlDbType.Int, 1, IIf(dtOk.Rows(i).Item("tou_kkk_henkou_flg1").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk_henkou_flg1").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk2", SqlDbType.Int, 10, IIf(dtOk.Rows(i).Item("tou_kkk2").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk2").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg2", SqlDbType.Int, 1, IIf(dtOk.Rows(i).Item("tou_kkk_henkou_flg2").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk_henkou_flg2").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk3", SqlDbType.Int, 10, IIf(dtOk.Rows(i).Item("tou_kkk3").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk3").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg3", SqlDbType.Int, 1, IIf(dtOk.Rows(i).Item("tou_kkk_henkou_flg3").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk_henkou_flg3").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk4", SqlDbType.Int, 10, IIf(dtOk.Rows(i).Item("tou_kkk4").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk4").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg4", SqlDbType.Int, 1, IIf(dtOk.Rows(i).Item("tou_kkk_henkou_flg4").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk_henkou_flg4").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk5", SqlDbType.Int, 10, IIf(dtOk.Rows(i).Item("tou_kkk5").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk5").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg5", SqlDbType.Int, 1, IIf(dtOk.Rows(i).Item("tou_kkk_henkou_flg5").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk_henkou_flg5").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk6", SqlDbType.Int, 10, IIf(dtOk.Rows(i).Item("tou_kkk6").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk6").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg6", SqlDbType.Int, 1, IIf(dtOk.Rows(i).Item("tou_kkk_henkou_flg6").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk_henkou_flg6").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk7", SqlDbType.Int, 10, IIf(dtOk.Rows(i).Item("tou_kkk7").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk7").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg7", SqlDbType.Int, 1, IIf(dtOk.Rows(i).Item("tou_kkk_henkou_flg7").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk_henkou_flg7").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk8", SqlDbType.Int, 10, IIf(dtOk.Rows(i).Item("tou_kkk8").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk8").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg8", SqlDbType.Int, 1, IIf(dtOk.Rows(i).Item("tou_kkk_henkou_flg8").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk_henkou_flg8").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk9", SqlDbType.Int, 10, IIf(dtOk.Rows(i).Item("tou_kkk9").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk9").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg9", SqlDbType.Int, 1, IIf(dtOk.Rows(i).Item("tou_kkk_henkou_flg9").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk_henkou_flg9").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk10", SqlDbType.Int, 10, IIf(dtOk.Rows(i).Item("tou_kkk10").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk10").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg10", SqlDbType.Int, 1, IIf(dtOk.Rows(i).Item("tou_kkk_henkou_flg10").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk_henkou_flg10").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk11t19", SqlDbType.Int, 10, IIf(dtOk.Rows(i).Item("tou_kkk11t19").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk11t19").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg11t19", SqlDbType.Int, 1, IIf(dtOk.Rows(i).Item("tou_kkk_henkou_flg11t19").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk_henkou_flg11t19").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk20t29", SqlDbType.Int, 10, IIf(dtOk.Rows(i).Item("tou_kkk20t29").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk20t29").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg20t29", SqlDbType.Int, 1, IIf(dtOk.Rows(i).Item("tou_kkk_henkou_flg20t29").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk_henkou_flg20t29").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk30t39", SqlDbType.Int, 10, IIf(dtOk.Rows(i).Item("tou_kkk30t39").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk30t39").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg30t39", SqlDbType.Int, 1, IIf(dtOk.Rows(i).Item("tou_kkk_henkou_flg30t39").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk_henkou_flg30t39").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk40t49", SqlDbType.Int, 10, IIf(dtOk.Rows(i).Item("tou_kkk40t49").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk40t49").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg40t49", SqlDbType.Int, 1, IIf(dtOk.Rows(i).Item("tou_kkk_henkou_flg40t49").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk_henkou_flg40t49").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk50t", SqlDbType.Int, 10, IIf(dtOk.Rows(i).Item("tou_kkk50t").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk50t").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg50t", SqlDbType.Int, 1, IIf(dtOk.Rows(i).Item("tou_kkk_henkou_flg50t").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk_henkou_flg50t").ToString.Trim)))

            paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, dtOk.Rows(i).Item("add_login_user_id").ToString.Trim))
            paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, dtOk.Rows(i).Item("upd_login_user_id").ToString.Trim))
            '更新されたデータセットを DB へ書き込み

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

    ''' <summary>アップロード管理テーブルを登録する</summary>
    ''' <history>2011/03/02　車龍(大連情報システム部)　新規作成</history>
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
            .AppendLine("	,2 ")
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

    ''' <summary>原価エラー情報を取得する</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI情報作成日</param>
    ''' <returns>原価エラーデータテーブル</returns>
    Public Function SelGenkaErr(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoridate As String) As Data.DataTable
        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT   ")
            .AppendLine("	TOP 100   ")
            .AppendLine("	MGE.gyou_no ")
            .AppendLine("	,ISNULL(MGE.tys_kaisya_cd,'') + ISNULL(MGE.jigyousyo_cd,'') AS tys_kaisya_cd ")
            .AppendLine("	,MGE.tys_kaisya_mei  ")
            .AppendLine("   ,case ")
            .AppendLine("       WHEN (ISNULL(MGE.aitesaki_syubetu,'') + '：' + ISNULL(MKM.meisyou,'')) = '：' THEN ")
            .AppendLine("           '' ")
            .AppendLine("       ELSE ")
            .AppendLine("	        (ISNULL(MGE.aitesaki_syubetu,'') + '：' + ISNULL(MKM.meisyou,'')) ")
            .AppendLine("       END AS aitesaki_syubetu ")
            .AppendLine("	,MGE.aitesaki_cd  ")
            .AppendLine("	,MGE.aitesaki_mei  ")
            .AppendLine("	,MGE.syouhin_cd  ")
            .AppendLine("	,MGE.syouhin_mei  ")
            .AppendLine("   ,case ")
            .AppendLine("       WHEN (ISNULL(MGE.tys_houhou_no,'') + '：' + ISNULL(MGE.tys_houhou,'')) = '：' THEN ")
            .AppendLine("           '' ")
            .AppendLine("       ELSE ")
            .AppendLine("	        (ISNULL(MGE.tys_houhou_no,'') + '：' + ISNULL(MGE.tys_houhou,'')) ")
            .AppendLine("       END AS tys_houhou ")
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN MGE.torikesi = '0' THEN ")
            .AppendLine("			'' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'取消' ")
            .AppendLine("		END AS torikesi ")  '--取消
            .AppendLine("	,MGE.tou_kkk1 ")   '--棟価格1
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MGE.tou_kkk_henkou_flg1,'0') = '0' THEN ")
            .AppendLine("			'変更不可' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'変更可' ")
            .AppendLine("		END AS tou_kkk_henkou_flg1 ")   '--棟価格変更FLG1
            .AppendLine("	,MGE.tou_kkk2 ")   '--棟価格2
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MGE.tou_kkk_henkou_flg2,'0') = '0' THEN ")
            .AppendLine("			'変更不可' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'変更可' ")
            .AppendLine("		END AS tou_kkk_henkou_flg2 ")   '--棟価格変更FLG2
            .AppendLine("	,MGE.tou_kkk3 ")   '--棟価格3
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MGE.tou_kkk_henkou_flg3,'0') = '0' THEN ")
            .AppendLine("			'変更不可' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'変更可' ")
            .AppendLine("		END AS tou_kkk_henkou_flg3 ")   '--棟価格変更FLG3
            .AppendLine("	,MGE.tou_kkk4 ")   '--棟価格4
            .AppendLine("	,CASE")
            .AppendLine("		WHEN ISNULL(MGE.tou_kkk_henkou_flg4,'0') = '0' THEN ")
            .AppendLine("			'変更不可' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'変更可' ")
            .AppendLine("		END AS tou_kkk_henkou_flg4 ")   '--棟価格変更FLG4
            .AppendLine("	,MGE.tou_kkk5 ")   '--棟価格5
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MGE.tou_kkk_henkou_flg5,'0') = '0' THEN ")
            .AppendLine("			'変更不可' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'変更可' ")
            .AppendLine("		END AS tou_kkk_henkou_flg5 ")   '--棟価格変更FLG5
            .AppendLine("	,MGE.tou_kkk6 ")   '--棟価格6
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MGE.tou_kkk_henkou_flg6,'0') = '0' THEN ")
            .AppendLine("			'変更不可' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'変更可' ")
            .AppendLine("		END AS tou_kkk_henkou_flg6 ")   '--棟価格変更FLG6
            .AppendLine("	,MGE.tou_kkk7 ")   '--棟価格7
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MGE.tou_kkk_henkou_flg7,'0') = '0' THEN ")
            .AppendLine("			'変更不可' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'変更可' ")
            .AppendLine("		END AS tou_kkk_henkou_flg7 ")   '--棟価格変更FLG7
            .AppendLine("	,MGE.tou_kkk8 ")   '--棟価格8
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MGE.tou_kkk_henkou_flg8,'0') = '0' THEN ")
            .AppendLine("			'変更不可' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'変更可' ")
            .AppendLine("		END AS tou_kkk_henkou_flg8 ")   '--棟価格変更FLG8
            .AppendLine("	,MGE.tou_kkk9 ")   '--棟価格9
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MGE.tou_kkk_henkou_flg9,'0') = '0' THEN ")
            .AppendLine("			'変更不可' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'変更可' ")
            .AppendLine("		END AS tou_kkk_henkou_flg9 ")   '--棟価格変更FLG9
            .AppendLine("	,MGE.tou_kkk10 ") '--棟価格10
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MGE.tou_kkk_henkou_flg10,'0') = '0' THEN ")
            .AppendLine("			'変更不可' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'変更可' ")
            .AppendLine("		END AS tou_kkk_henkou_flg10 ")  '--棟価格変更FLG10
            .AppendLine("	,MGE.tou_kkk11t19 ")   '--棟価格11～19
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MGE.tou_kkk_henkou_flg11t19,'0') = '0' THEN ")
            .AppendLine("			'変更不可' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'変更可' ")
            .AppendLine("		END AS tou_kkk_henkou_flg11t19 ")   '--棟価格変更FLG11～19
            .AppendLine("	,MGE.tou_kkk20t29 ")   '--棟価格20～29
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MGE.tou_kkk_henkou_flg20t29,'0') = '0' THEN ")
            .AppendLine("			'変更不可' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'変更可' ")
            .AppendLine("		END AS tou_kkk_henkou_flg20t29 ")   '--棟価格変更FLG20～29
            .AppendLine("	,MGE.tou_kkk30t39 ")   '--棟価格30～39
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MGE.tou_kkk_henkou_flg30t39,'0') = '0' THEN ")
            .AppendLine("			'変更不可' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'変更可' ")
            .AppendLine("		END AS tou_kkk_henkou_flg30t39 ")   '--棟価格変更FLG30～39
            .AppendLine("	,MGE.tou_kkk40t49 ")   '--棟価格40～49
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MGE.tou_kkk_henkou_flg40t49,'0') = '0' THEN ")
            .AppendLine("			'変更不可'")
            .AppendLine("		ELSE")
            .AppendLine("			'変更可'")
            .AppendLine("		END AS tou_kkk_henkou_flg40t49 ")   '--棟価格変更FLG40～49
            .AppendLine("	,MGE.tou_kkk50t ")   '--棟価格50～
            .AppendLine("	,CASE")
            .AppendLine("		WHEN ISNULL(MGE.tou_kkk_henkou_flg50t,'0') = '0' THEN ")
            .AppendLine("			'変更不可' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'変更可' ")
            .AppendLine("		END AS tou_kkk_henkou_flg50t ") '--棟価格変更FLG50～
            .AppendLine("FROM  ")
            .AppendLine("	m_genka_error AS MGE WITH(READCOMMITTED)  ")
            .AppendLine("	LEFT OUTER JOIN  ")
            .AppendLine("	m_kakutyou_meisyou MKM WITH(READCOMMITTED)  ")
            .AppendLine("	ON ")
            .AppendLine("		MGE.aitesaki_syubetu = MKM.code  ")
            .AppendLine("		AND ")
            .AppendLine("		MKM.meisyou_syubetu = 22  ")
            .AppendLine("WHERE ")
            .AppendLine("	MGE.edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date ")
            '処理日時
            .AppendLine(" AND CONVERT(varchar(100),MGE.syori_datetime,112) + REPLACE(CONVERT(varchar(100),MGE.syori_datetime,114),':','') = @syori_datetime")
            .AppendLine("ORDER BY ")
            .AppendLine("	MGE.gyou_no ")
        End With

        paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdiJouhouSakuseiDate))
        paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 40, strSyoridate))

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtGenkaErr", paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function

    ''' <summary>原価エラー件数を取得する</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI情報作成日</param>
    ''' <returns>原価エラー件数</returns>
    Public Function SelGenkaErrCount(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoridate As String) As String

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   COUNT(*) AS count ")
            .AppendLine("FROM  ")
            .AppendLine("	m_genka_error AS MGE WITH(READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("	MGE.edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date ")
            '処理日時
            .AppendLine(" AND CONVERT(varchar(100),MGE.syori_datetime,112) + REPLACE(CONVERT(varchar(100),MGE.syori_datetime,114),':','') = @syori_datetime")
        End With

        paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdiJouhouSakuseiDate))
        paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 40, strSyoridate))

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtHanbaiKakakuErr", paramList.ToArray)

        Return CInt(dsReturn.Tables(0).Rows(0).Item(0))

    End Function

    ''' <summary>原価エラーCSV情報を取得する</summary>
    ''' <returns>原価エラーCSVデータテーブル</returns>
    Public Function SelGenkaErrCsv(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoridate As String) As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT   ")
            .AppendLine("	TOP 5000   ")
            .AppendLine("	MGE.edi_jouhou_sakusei_date ")
            .AppendLine("	,MGE.gyou_no ")
            .AppendLine("   ,MGE.syori_datetime ")
            .AppendLine("	,MGE.tys_kaisya_cd ")
            .AppendLine("	,MGE.jigyousyo_cd ")
            .AppendLine("	,MGE.tys_kaisya_mei ")
            .AppendLine("	,MGE.aitesaki_syubetu ")
            .AppendLine("	,MGE.aitesaki_cd  ")
            .AppendLine("	,MGE.aitesaki_mei  ")
            .AppendLine("	,MGE.syouhin_cd  ")
            .AppendLine("	,MGE.syouhin_mei  ")
            .AppendLine("	,MGE.tys_houhou_no  ")
            .AppendLine("	,MGE.tys_houhou  ")
            .AppendLine("	,MGE.torikesi  ")
            .AppendLine("	,MGE.tou_kkk1  ")
            .AppendLine("	,MGE.tou_kkk_henkou_flg1  ")
            .AppendLine("	,MGE.tou_kkk2  ")
            .AppendLine("	,MGE.tou_kkk_henkou_flg2  ")
            .AppendLine("	,MGE.tou_kkk3  ")
            .AppendLine("	,MGE.tou_kkk_henkou_flg3  ")
            .AppendLine("	,MGE.tou_kkk4  ")
            .AppendLine("	,MGE.tou_kkk_henkou_flg4  ")
            .AppendLine("	,MGE.tou_kkk5  ")
            .AppendLine("	,MGE.tou_kkk_henkou_flg5  ")
            .AppendLine("	,MGE.tou_kkk6  ")
            .AppendLine("	,MGE.tou_kkk_henkou_flg6  ")
            .AppendLine("	,MGE.tou_kkk7  ")
            .AppendLine("	,MGE.tou_kkk_henkou_flg7  ")
            .AppendLine("	,MGE.tou_kkk8  ")
            .AppendLine("	,MGE.tou_kkk_henkou_flg8  ")
            .AppendLine("	,MGE.tou_kkk9  ")
            .AppendLine("	,MGE.tou_kkk_henkou_flg9  ")
            .AppendLine("	,MGE.tou_kkk10  ")
            .AppendLine("	,MGE.tou_kkk_henkou_flg10  ")
            .AppendLine("	,MGE.tou_kkk11t19  ")
            .AppendLine("	,MGE.tou_kkk_henkou_flg11t19  ")
            .AppendLine("	,MGE.tou_kkk20t29  ")
            .AppendLine("	,MGE.tou_kkk_henkou_flg20t29  ")
            .AppendLine("	,MGE.tou_kkk30t39  ")
            .AppendLine("	,MGE.tou_kkk_henkou_flg30t39  ")
            .AppendLine("	,MGE.tou_kkk40t49  ")
            .AppendLine("	,MGE.tou_kkk_henkou_flg40t49  ")
            .AppendLine("	,MGE.tou_kkk50t  ")
            .AppendLine("	,MGE.tou_kkk_henkou_flg50t  ")
            .AppendLine("	,MGE.add_login_user_id  ")
            .AppendLine("	,MGE.add_datetime  ")
            .AppendLine("	,MGE.upd_login_user_id  ")
            .AppendLine("	,MGE.upd_datetime  ")
            .AppendLine("FROM  ")
            .AppendLine("	m_genka_error AS MGE WITH(READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("	MGE.edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date ")
            '処理日時
            .AppendLine(" AND CONVERT(varchar(100),MGE.syori_datetime,112) + REPLACE(CONVERT(varchar(100),MGE.syori_datetime,114),':','') = @syori_datetime")
            .AppendLine("ORDER BY ")
            .AppendLine("	MGE.gyou_no ")
        End With

        paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdiJouhouSakuseiDate))
        paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 40, strSyoridate))

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtGenkaErrCount", paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function

End Class

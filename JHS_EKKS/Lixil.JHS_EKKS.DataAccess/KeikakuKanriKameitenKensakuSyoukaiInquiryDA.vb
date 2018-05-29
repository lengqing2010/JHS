Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text

''' <summary>
''' 計画管理_加盟店検索照会指示
''' </summary>
''' <remarks></remarks>
Public Class KeikakuKanriKameitenKensakuSyoukaiInquiryDA

    ''' <summary>
    ''' 区分情報を取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/02　車龍(大連情報システム部)　新規作成</history>
    Public Function SelKbnInfo() As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)

        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   kbn AS cd ")
            .AppendLine("   ,(kbn + '：' + kbn_mei) AS mei ")
            .AppendLine("FROM ")
            .AppendLine("   m_data_kbn WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("   torikesi=@torikesi ")
            .AppendLine("ORDER BY ")
            .AppendLine("   kbn ASC ")
        End With

        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 5, "0"))

        ' 検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtKbnInfo", paramList.ToArray)

        Return dsReturn.Tables("dtKbnInfo")

    End Function

    ''' <summary>
    ''' 管轄支店情報を取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/02　車龍(大連情報システム部)　新規作成</history>
    Public Function SelSitenInfo() As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)

        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("    busyo_cd")
            .AppendLine("     ,(busyo_cd + '：' + busyo_mei) AS busyo_mei ")
            .AppendLine("FROM ")
            .AppendLine("   m_busyo_kanri WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("   sosiki_level = '4' ")
            .AppendLine("ORDER BY ")
            .AppendLine("   busyo_cd ")
        End With

        ' 検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtSitenInfo")

        Return dsReturn.Tables("dtSitenInfo")

    End Function

    ''' <summary>
    ''' 都道府県情報を取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/02　車龍(大連情報システム部)　新規作成</history>
    Public Function SelTodoufukenInfo() As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)

        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   todouhuken_cd AS cd ")
            .AppendLine("   ,(todouhuken_cd + '：' + todouhuken_mei) AS mei ")
            .AppendLine("FROM ")
            .AppendLine("   m_todoufuken WITH(READCOMMITTED) ")
            .AppendLine("ORDER BY ")
            .AppendLine("   todouhuken_cd ASC ")
        End With

        ' 検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtTodoufukenInfo")

        Return dsReturn.Tables("dtTodoufukenInfo")

    End Function

    ''' <summary>
    ''' 名称情報を取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/02　車龍(大連情報システム部)　新規作成</history>
    Public Function SelMeisyouInfo(ByVal strMeisyouSyubetu As String) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strMeisyouSyubetu)

        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("    code")
            .AppendLine("     ,(CONVERT(VARCHAR(10),code) + '：' + meisyou) AS meisyou ")
            .AppendLine("FROM ")
            .AppendLine("   m_keikakuyou_meisyou WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("   meisyou_syubetu = @meisyou_syubetu ")
            .AppendLine("ORDER BY ")
            .AppendLine("   hyouji_jyun ")
        End With

        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.VarChar, 2, strMeisyouSyubetu))

        ' 検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtMeisyouInfo", paramList.ToArray)

        Return dsReturn.Tables("dtMeisyouInfo")

    End Function

    ''' <summary>
    ''' 拡張名称情報を取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/02　車龍(大連情報システム部)　新規作成</history>
    Public Function SelKakutyouMeisyouInfo(ByVal strMeisyouSyubetu As String) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strMeisyouSyubetu)

        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("    code")
            .AppendLine("     ,(CONVERT(VARCHAR(10),code) + '：' + meisyou) AS meisyou ")
            .AppendLine("FROM ")
            .AppendLine("   m_keikakuyou_kakutyou_meisyou WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("   meisyou_syubetu = @meisyou_syubetu ")
            .AppendLine("ORDER BY ")
            .AppendLine("   hyouji_jyun ")
        End With

        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.VarChar, 2, strMeisyouSyubetu))

        ' 検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtKakutyouMeisyouInfo", paramList.ToArray)

        Return dsReturn.Tables("dtKakutyouMeisyouInfo")

    End Function

    ''' <summary>
    ''' 加盟店明細情報を取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/02　車龍(大連情報システム部)　新規作成</history>
    Public Function SelKameitenInfo(ByVal dicPrm As Dictionary(Of String, String)) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, dicPrm)

        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            If dicPrm("KensakuJyouken") = "100" Then
                .AppendLine("   TOP 100 ")
            ElseIf dicPrm("KensakuJyouken") = "200" Then
                .AppendLine("   TOP 200 ")
            End If
            .AppendLine("	ISNULL(MKK.torikesi,0) AS Torikesi ") '--取消 ")
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MKK.torikesi,0) = 0 THEN ")
            .AppendLine("			'' ")
            .AppendLine("		ELSE ")
            .AppendLine("			CONVERT(VARCHAR(10),MKK.torikesi) + '：' + ISNULL(MKKM_TORIKESI.meisyou,'') ")
            .AppendLine("		END AS TorikesiText ") '--取消名 ")
            .AppendLine("	,ISNULL(MKKI.kbn,'') AS Kbn ") '--区分 ")
            .AppendLine("	,ISNULL(MKK.kameiten_cd,'') AS KameitenCd ") '--加盟店コード ")
            .AppendLine("	,ISNULL(MKK.kameiten_mei,'') AS KameitenMei ") '--加盟店名 ")
            .AppendLine("	,ISNULL(MKK.eigyou_kbn,'') AS EigyouKbn ") '--営業区分 ")
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MKK.eigyou_kbn,'') = '' THEN ")
            .AppendLine("			'' ")
            .AppendLine("		ELSE ")
            .AppendLine("			ISNULL(MKK.eigyou_kbn,'') + '：' + ISNULL(MKM_EIGYOU_KBN.meisyou,'') ")
            .AppendLine("		END AS EigyouKbnText ") '--営業区分名 ")
            .AppendLine("	,ISNULL(MKK.eigyou_tantousya_mei,'') AS EigyouTantousya ") '--営業担当者名 ")
            .AppendLine("	,ISNULL(MKK.shiten_mei,'') AS KankatuSiten ") '--支店名 ")
            .AppendLine("	,ISNULL(MKK.todouhuken_mei,'') AS Todoufuken ") '--都道府県名 ")
            .AppendLine("	,ISNULL(MKK.eigyousyo_cd,'') AS EigyousyoCd ") '--営業所ｺｰﾄﾞ ")
            .AppendLine("	,ISNULL(MKK.keiretu_cd,'') AS KeiretuCd ") '--系列ｺｰﾄﾞ ")
            .AppendLine("	,ISNULL(METS.syozoku,'') AS EigyouTantouSyozaku ") '--所属 ")
            .AppendLine("	,MKKI.gyoutai AS GyoutaiCd ") '--業態コード ")
            .AppendLine("	,ISNULL(MKM_GYOUTAI.meisyou,'') AS Gyoutai ") '--業態名 ")
            .AppendLine("	,MKK.keikaku_nenkan_tousuu AS NenkanTousuu ") '--年間棟数 ")
            .AppendLine("	,MKKI.keikakuyou_nenkan_tousuu AS KeikakuyouNenkanTousuu ") '--計画用_年間棟数 ")
            .AppendLine("	,MKKI.kameiten_zokusei_1 ") '--加盟店属性1 ")
            .AppendLine("	,ISNULL(MKM_ZOKUSEI1.meisyou,'') AS KameitenZokusei1 ") '--加盟店属性1名 ")
            .AppendLine("	,MKKI.kameiten_zokusei_2 ") '--加盟店属性2 ")
            .AppendLine("	,ISNULL(MKM_ZOKUSEI2.meisyou,'') AS KameitenZokusei2 ") '--加盟店属性2名 ")
            .AppendLine("	,MKKI.kameiten_zokusei_3 ") '--加盟店属性3 ")
            .AppendLine("	,ISNULL(MKM_ZOKUSEI3.meisyou,'') AS KameitenZokusei3 ") '--加盟店属性3名 ")
            .AppendLine("	,MKKI.kameiten_zokusei_4 ") '--加盟店属性4 ")
            .AppendLine("	,ISNULL(MKKM_ZOKUSEI4.meisyou,'') AS KameitenZokusei4 ") '--加盟店属性4名 ")
            .AppendLine("	,MKKI.kameiten_zokusei_5 ") '--加盟店属性5 ")
            .AppendLine("	,ISNULL(MKKM_ZOKUSEI5.meisyou,'') AS KameitenZokusei5 ") '--加盟店属性5名 ")
            .AppendLine("	,ISNULL(MKKI.kameiten_zokusei_6,'') AS KameitenZokusei6 ") '--加盟店属性6 ")
            .AppendLine("FROM ")
            .AppendLine("	m_keikaku_kameiten AS MKK WITH(READCOMMITTED) ") '--計画管理_加盟店マスタ ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_keikaku_kameiten_info AS MKKI WITH(READCOMMITTED) ") '--計画管理_加盟店情報マスタ ")
            .AppendLine("	ON ")
            .AppendLine("		MKK.kameiten_cd = MKKI.kameiten_cd ") '--加盟店コード ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_eigyou_tantousya_syozoku_info AS METS WITH(READCOMMITTED) ") '--営業担当者_所属情報マスタ ")
            .AppendLine("	ON ")
            .AppendLine("		MKK.eigyou_tantousya_id = METS.eigyou_tantousya_id ") '--営業担当者名 ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_keikakuyou_kakutyou_meisyou AS MKKM_TORIKESI WITH(READCOMMITTED) ") '--拡張名称マスタ(取消) ")
            .AppendLine("	ON ")
            .AppendLine("		MKKM_TORIKESI.meisyou_syubetu = '56' ")
            .AppendLine("		AND ")
            .AppendLine("		MKKM_TORIKESI.code = MKK.torikesi ") '--取消 ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_keikakuyou_meisyou AS MKM_EIGYOU_KBN WITH(READCOMMITTED) ") '--名称マスタ(営業区分) ")
            .AppendLine("	ON ")
            .AppendLine("		MKM_EIGYOU_KBN.meisyou_syubetu = '05' ")
            .AppendLine("		AND ")
            .AppendLine("		MKM_EIGYOU_KBN.code = MKK.eigyou_kbn ") '--営業区分 ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_keikakuyou_meisyou AS MKM_GYOUTAI WITH(READCOMMITTED) ") '--名称マスタ(業態) ")
            .AppendLine("	ON ")
            .AppendLine("		MKM_GYOUTAI.meisyou_syubetu = '20' ")
            .AppendLine("		AND ")
            .AppendLine("		MKM_GYOUTAI.code = MKKI.gyoutai ") '--業態 ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_keikakuyou_meisyou AS MKM_ZOKUSEI1 WITH(READCOMMITTED) ") '--名称マスタ(加盟店属性1) ")
            .AppendLine("	ON ")
            .AppendLine("		MKM_ZOKUSEI1.meisyou_syubetu = '21' ")
            .AppendLine("		AND ")
            .AppendLine("		MKM_ZOKUSEI1.code = MKKI.kameiten_zokusei_1 ") '--加盟店属性1 ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_keikakuyou_meisyou AS MKM_ZOKUSEI2 WITH(READCOMMITTED) ") '--名称マスタ(加盟店属性2) ")
            .AppendLine("	ON ")
            .AppendLine("		MKM_ZOKUSEI2.meisyou_syubetu = '22' ")
            .AppendLine("		AND ")
            .AppendLine("		MKM_ZOKUSEI2.code = MKKI.kameiten_zokusei_2 ") '--加盟店属性2 ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_keikakuyou_meisyou AS MKM_ZOKUSEI3 WITH(READCOMMITTED) ") '--名称マスタ(加盟店属性3) ")
            .AppendLine("	ON ")
            .AppendLine("		MKM_ZOKUSEI3.meisyou_syubetu = '23' ")
            .AppendLine("		AND ")
            .AppendLine("		MKM_ZOKUSEI3.code = MKKI.kameiten_zokusei_3 ") '--加盟店属性3 ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_keikakuyou_kakutyou_meisyou AS MKKM_ZOKUSEI4 WITH(READCOMMITTED) ") '--拡張名称マスタ(加盟店属性4) ")
            .AppendLine("	ON ")
            .AppendLine("		MKKM_ZOKUSEI4.meisyou_syubetu = '21' ")
            .AppendLine("		AND ")
            .AppendLine("		MKKM_ZOKUSEI4.code = MKKI.kameiten_zokusei_4 ") '--加盟店属性4 ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_keikakuyou_kakutyou_meisyou AS MKKM_ZOKUSEI5 WITH(READCOMMITTED) ") '--拡張名称マスタ(加盟店属性5) ")
            .AppendLine("	ON ")
            .AppendLine("		MKKM_ZOKUSEI5.meisyou_syubetu = '22' ")
            .AppendLine("		AND ")
            .AppendLine("		MKKM_ZOKUSEI5.code = MKKI.kameiten_zokusei_5 ") '--加盟店属性5 ")
            .AppendLine("WHERE ")
            .AppendLine("	MKK.keikaku_nendo = @keikaku_nendo ") '--計画年度 ")
            paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.VarChar, 4, dicPrm("KeikakuNendo")))
            '区分
            If Not dicPrm("Kbn").Equals(String.Empty) Then
                Dim arrKbn() As String = dicPrm("Kbn").Split(CChar(","))
                .AppendLine("	AND ")
                .AppendLine("	MKKI.kbn IN ") '--区分 ")
                .AppendLine("	( ")
                For i As Integer = 0 To arrKbn.Length - 1
                    If i = 0 Then
                        .AppendLine("       @kbn" & i)
                    Else
                        .AppendLine("       ,@kbn" & i)
                    End If
                    paramList.Add(MakeParam("@kbn" & i, SqlDbType.VarChar, 1, arrKbn(i)))
                Next
                .AppendLine("   ) ")
            End If
            '取消
            If Not dicPrm("Torikesi").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.torikesi = @torikesi ") '--取消 ")
                paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, dicPrm("Torikesi")))
            End If
            '加盟店名
            If Not dicPrm("KameitenMei").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.kameiten_mei LIKE @kameiten_mei ") '--加盟店名 ")
                paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 41, dicPrm("KameitenMei") & "%"))
            End If
            '加盟店コード
            If (Not dicPrm("KameitenCd1").Equals(String.Empty)) AndAlso (Not dicPrm("KameitenCd2").Equals(String.Empty)) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.kameiten_cd BETWEEN @kameiten_cd_from AND @kameiten_cd_to ") '--加盟店コード ")
                paramList.Add(MakeParam("@kameiten_cd_from", SqlDbType.VarChar, 16, dicPrm("KameitenCd1")))
                paramList.Add(MakeParam("@kameiten_cd_to", SqlDbType.VarChar, 16, dicPrm("KameitenCd2")))
            Else
                If Not dicPrm("KameitenCd1").Equals(String.Empty) Then
                    .AppendLine("	AND ")
                    .AppendLine("	MKK.kameiten_cd = @kameiten_cd_from ") '--加盟店コード ")
                    paramList.Add(MakeParam("@kameiten_cd_from", SqlDbType.VarChar, 16, dicPrm("KameitenCd1")))
                End If
            End If
            '営業所コード
            If (Not dicPrm("EigyousyaCd1").Equals(String.Empty)) AndAlso (Not dicPrm("EigyousyaCd2").Equals(String.Empty)) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.eigyousyo_cd BETWEEN @eigyousyo_cd_from AND @eigyousyo_cd_to ") '--営業所コード ")
                paramList.Add(MakeParam("@eigyousyo_cd_from", SqlDbType.VarChar, 5, dicPrm("EigyousyaCd1")))
                paramList.Add(MakeParam("@eigyousyo_cd_to", SqlDbType.VarChar, 5, dicPrm("EigyousyaCd2")))
            Else
                If Not dicPrm("EigyousyaCd1").Equals(String.Empty) Then
                    .AppendLine("	AND ")
                    .AppendLine("	MKK.eigyousyo_cd = @eigyousyo_cd_from ") '--営業所コード ")
                    paramList.Add(MakeParam("@eigyousyo_cd_from", SqlDbType.VarChar, 5, dicPrm("EigyousyaCd1")))
                End If
            End If
            '系列コード
            If Not dicPrm("KeiretuCd").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.keiretu_cd = @keiretu_cd ") '--系列コード ")
                paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 5, dicPrm("KeiretuCd")))
            End If
            '管轄支店
            If Not dicPrm("Siten").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.busyo_cd = @busyo_cd ") '--管轄支店 ")
                paramList.Add(MakeParam("@busyo_cd", SqlDbType.VarChar, 16, dicPrm("Siten")))
            End If
            '都道府県コード
            If Not dicPrm("TodoufukenCd").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.todouhuken_cd = @todouhuken_cd ") '--都道府県コード ")
                paramList.Add(MakeParam("@todouhuken_cd", SqlDbType.VarChar, 2, dicPrm("TodoufukenCd")))
            End If
            '営業担当者
            If Not dicPrm("EigyouTantousya").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.eigyou_tantousya_id = @eigyou_tantousya_id ") '--営業担当者 ")
                paramList.Add(MakeParam("@eigyou_tantousya_id", SqlDbType.VarChar, 30, dicPrm("EigyouTantousya")))
            End If
            '営業区分
            If Not dicPrm("EigyouKbn").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.eigyou_kbn = @eigyou_kbn ") '--営業区分 ")
                paramList.Add(MakeParam("@eigyou_kbn", SqlDbType.VarChar, 1, dicPrm("EigyouKbn")))
            End If
            '営業担当所属
            If Not dicPrm("EigyouTantouSyozaku").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	METS.syozoku_cd = @syozoku_cd ") '--営業担当所属 ")
                paramList.Add(MakeParam("@syozoku_cd", SqlDbType.Int, 10, dicPrm("EigyouTantouSyozaku")))
            End If
            '業態
            If Not dicPrm("Gyoutai").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKKI.gyoutai = @gyoutai ") '--業態 ")
                paramList.Add(MakeParam("@gyoutai", SqlDbType.Int, 10, dicPrm("Gyoutai")))
            End If
            '年間棟数
            If (Not dicPrm("NenkanTousuu1").Equals(String.Empty)) AndAlso (Not dicPrm("NenkanTousuu2").Equals(String.Empty)) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.keikaku_nenkan_tousuu BETWEEN @keikaku_nenkan_tousuu_from AND @keikaku_nenkan_tousuu_to ") '--年間棟数 ")
                paramList.Add(MakeParam("@keikaku_nenkan_tousuu_from", SqlDbType.Int, 5, dicPrm("NenkanTousuu1")))
                paramList.Add(MakeParam("@keikaku_nenkan_tousuu_to", SqlDbType.Int, 5, dicPrm("NenkanTousuu2")))
            Else
                If Not dicPrm("NenkanTousuu1").Equals(String.Empty) Then
                    .AppendLine("	AND ")
                    .AppendLine("	MKK.keikaku_nenkan_tousuu = @keikaku_nenkan_tousuu_from ") '--年間棟数 ")
                    paramList.Add(MakeParam("@keikaku_nenkan_tousuu_from", SqlDbType.Int, 5, dicPrm("NenkanTousuu1")))
                End If
            End If
            '計画用_年間棟数
            If (Not dicPrm("KeikakuyouNenkanTousuu1").Equals(String.Empty)) AndAlso (Not dicPrm("KeikakuyouNenkanTousuu2").Equals(String.Empty)) Then
                .AppendLine("	AND ")
                .AppendLine("	MKKI.keikakuyou_nenkan_tousuu BETWEEN @keikakuyou_nenkan_tousuu_from AND @keikakuyou_nenkan_tousuu_to ") '--計画用_年間棟数 ")
                paramList.Add(MakeParam("@keikakuyou_nenkan_tousuu_from", SqlDbType.Int, 10, dicPrm("KeikakuyouNenkanTousuu1")))
                paramList.Add(MakeParam("@keikakuyou_nenkan_tousuu_to", SqlDbType.Int, 10, dicPrm("KeikakuyouNenkanTousuu2")))
            Else
                If Not dicPrm("KeikakuyouNenkanTousuu1").Equals(String.Empty) Then
                    .AppendLine("	AND ")
                    .AppendLine("	MKKI.keikakuyou_nenkan_tousuu = @keikakuyou_nenkan_tousuu_from ") '--計画用_年間棟数 ")
                    paramList.Add(MakeParam("@keikakuyou_nenkan_tousuu_from", SqlDbType.Int, 10, dicPrm("KeikakuyouNenkanTousuu1")))
                End If
            End If
            '加盟店属性1
            If Not dicPrm("KameitenZokusei1").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKKI.kameiten_zokusei_1 = @kameiten_zokusei_1 ") '--加盟店属性1 ")
                paramList.Add(MakeParam("@kameiten_zokusei_1", SqlDbType.Int, 2, dicPrm("KameitenZokusei1")))
            End If
            '加盟店属性2
            If Not dicPrm("KameitenZokusei2").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKKI.kameiten_zokusei_2 = @kameiten_zokusei_2 ") '--加盟店属性2 ")
                paramList.Add(MakeParam("@kameiten_zokusei_2", SqlDbType.Int, 2, dicPrm("KameitenZokusei2")))
            End If
            '加盟店属性3
            If Not dicPrm("KameitenZokusei3").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKKI.kameiten_zokusei_3 = @kameiten_zokusei_3 ") '--加盟店属性3 ")
                paramList.Add(MakeParam("@kameiten_zokusei_3", SqlDbType.Int, 2, dicPrm("KameitenZokusei3")))
            End If
            '加盟店属性4
            If Not dicPrm("KameitenZokusei4").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKKI.kameiten_zokusei_4 = @kameiten_zokusei_4 ") '--加盟店属性4 ")
                paramList.Add(MakeParam("@kameiten_zokusei_4", SqlDbType.Int, 2, dicPrm("KameitenZokusei4")))
            End If
            '加盟店属性5
            If Not dicPrm("KameitenZokusei5").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKKI.kameiten_zokusei_5 = @kameiten_zokusei_5 ") '--加盟店属性2 ")
                paramList.Add(MakeParam("@kameiten_zokusei_5", SqlDbType.Int, 2, dicPrm("KameitenZokusei5")))
            End If
            '加盟店属性6
            If Not dicPrm("KameitenZokusei6").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKKI.kameiten_zokusei_6 = @kameiten_zokusei_6 ") '--加盟店属性6 ")
                paramList.Add(MakeParam("@kameiten_zokusei_6", SqlDbType.VarChar, 40, dicPrm("KameitenZokusei6")))
            End If
            '計画値有無
            If Not dicPrm("Keikaku0Flg").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.keikaku0_flg = @keikaku0_flg ") '--計画値有無 ")
                paramList.Add(MakeParam("@keikaku0_flg", SqlDbType.Int, 1, dicPrm("Keikaku0Flg")))
            End If
            .AppendLine("ORDER BY ")
            .AppendLine("	MKKI.kbn ASC ") '--区分 ")
            .AppendLine("	,MKK.kameiten_cd ASC ") '--加盟店コード ")
        End With

        ' 検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtKameitenInfo", paramList.ToArray)

        Return dsReturn.Tables("dtKameitenInfo")

    End Function

    ''' <summary>
    ''' 加盟店明細件数を取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/02　車龍(大連情報システム部)　新規作成</history>
    Public Function SelKameitenCount(ByVal dicPrm As Dictionary(Of String, String)) As Integer

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, dicPrm)

        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	COUNT(MKK.kameiten_cd) AS CNT ")
            .AppendLine("FROM ")
            .AppendLine("	m_keikaku_kameiten AS MKK WITH(READCOMMITTED) ") '--計画管理_加盟店マスタ ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_keikaku_kameiten_info AS MKKI WITH(READCOMMITTED) ") '--計画管理_加盟店情報マスタ ")
            .AppendLine("	ON ")
            .AppendLine("		MKK.kameiten_cd = MKKI.kameiten_cd ") '--加盟店コード ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_eigyou_tantousya_syozoku_info AS METS WITH(READCOMMITTED) ") '--営業担当者_所属情報マスタ ")
            .AppendLine("	ON ")
            .AppendLine("		MKK.eigyou_tantousya_id = METS.eigyou_tantousya_id ") '--営業担当者名 ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_keikakuyou_kakutyou_meisyou AS MKKM_TORIKESI WITH(READCOMMITTED) ") '--拡張名称マスタ(取消) ")
            .AppendLine("	ON ")
            .AppendLine("		MKKM_TORIKESI.meisyou_syubetu = '56' ")
            .AppendLine("		AND ")
            .AppendLine("		MKKM_TORIKESI.code = MKK.torikesi ") '--取消 ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_keikakuyou_meisyou AS MKM_EIGYOU_KBN WITH(READCOMMITTED) ") '--名称マスタ(営業区分) ")
            .AppendLine("	ON ")
            .AppendLine("		MKM_EIGYOU_KBN.meisyou_syubetu = '05' ")
            .AppendLine("		AND ")
            .AppendLine("		MKM_EIGYOU_KBN.code = MKK.eigyou_kbn ") '--営業区分 ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_keikakuyou_meisyou AS MKM_GYOUTAI WITH(READCOMMITTED) ") '--名称マスタ(業態) ")
            .AppendLine("	ON ")
            .AppendLine("		MKM_GYOUTAI.meisyou_syubetu = '20' ")
            .AppendLine("		AND ")
            .AppendLine("		MKM_GYOUTAI.code = MKKI.gyoutai ") '--業態 ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_keikakuyou_meisyou AS MKM_ZOKUSEI1 WITH(READCOMMITTED) ") '--名称マスタ(加盟店属性1) ")
            .AppendLine("	ON ")
            .AppendLine("		MKM_ZOKUSEI1.meisyou_syubetu = '21' ")
            .AppendLine("		AND ")
            .AppendLine("		MKM_ZOKUSEI1.code = MKKI.kameiten_zokusei_1 ") '--加盟店属性1 ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_keikakuyou_meisyou AS MKM_ZOKUSEI2 WITH(READCOMMITTED) ") '--名称マスタ(加盟店属性2) ")
            .AppendLine("	ON ")
            .AppendLine("		MKM_ZOKUSEI2.meisyou_syubetu = '22' ")
            .AppendLine("		AND ")
            .AppendLine("		MKM_ZOKUSEI2.code = MKKI.kameiten_zokusei_2 ") '--加盟店属性2 ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_keikakuyou_meisyou AS MKM_ZOKUSEI3 WITH(READCOMMITTED) ") '--名称マスタ(加盟店属性3) ")
            .AppendLine("	ON ")
            .AppendLine("		MKM_ZOKUSEI3.meisyou_syubetu = '23' ")
            .AppendLine("		AND ")
            .AppendLine("		MKM_ZOKUSEI3.code = MKKI.kameiten_zokusei_3 ") '--加盟店属性3 ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_keikakuyou_kakutyou_meisyou AS MKKM_ZOKUSEI4 WITH(READCOMMITTED) ") '--拡張名称マスタ(加盟店属性4) ")
            .AppendLine("	ON ")
            .AppendLine("		MKKM_ZOKUSEI4.meisyou_syubetu = '21' ")
            .AppendLine("		AND ")
            .AppendLine("		MKKM_ZOKUSEI4.code = MKKI.kameiten_zokusei_4 ") '--加盟店属性4 ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_keikakuyou_kakutyou_meisyou AS MKKM_ZOKUSEI5 WITH(READCOMMITTED) ") '--拡張名称マスタ(加盟店属性5) ")
            .AppendLine("	ON ")
            .AppendLine("		MKKM_ZOKUSEI5.meisyou_syubetu = '22' ")
            .AppendLine("		AND ")
            .AppendLine("		MKKM_ZOKUSEI5.code = MKKI.kameiten_zokusei_5 ") '--加盟店属性5 ")
            .AppendLine("WHERE ")
            .AppendLine("	MKK.keikaku_nendo = @keikaku_nendo ") '--計画年度 ")
            paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.VarChar, 4, dicPrm("KeikakuNendo")))
            '区分
            If Not dicPrm("Kbn").Equals(String.Empty) Then
                Dim arrKbn() As String = dicPrm("Kbn").Split(CChar(","))
                .AppendLine("	AND ")
                .AppendLine("	MKKI.kbn IN ") '--区分 ")
                .AppendLine("	( ")
                For i As Integer = 0 To arrKbn.Length - 1
                    If i = 0 Then
                        .AppendLine("       @kbn" & i)
                    Else
                        .AppendLine("       ,@kbn" & i)
                    End If
                    paramList.Add(MakeParam("@kbn" & i, SqlDbType.VarChar, 1, arrKbn(i)))
                Next
                .AppendLine("   ) ")
            End If
            '取消
            If Not dicPrm("Torikesi").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.torikesi = @torikesi ") '--取消 ")
                paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, dicPrm("Torikesi")))
            End If
            '加盟店名
            If Not dicPrm("KameitenMei").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.kameiten_mei LIKE @kameiten_mei ") '--加盟店名 ")
                paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 41, dicPrm("KameitenMei") & "%"))
            End If
            '加盟店コード
            If (Not dicPrm("KameitenCd1").Equals(String.Empty)) AndAlso (Not dicPrm("KameitenCd2").Equals(String.Empty)) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.kameiten_cd BETWEEN @kameiten_cd_from AND @kameiten_cd_to ") '--加盟店コード ")
                paramList.Add(MakeParam("@kameiten_cd_from", SqlDbType.VarChar, 16, dicPrm("KameitenCd1")))
                paramList.Add(MakeParam("@kameiten_cd_to", SqlDbType.VarChar, 16, dicPrm("KameitenCd2")))
            Else
                If Not dicPrm("KameitenCd1").Equals(String.Empty) Then
                    .AppendLine("	AND ")
                    .AppendLine("	MKK.kameiten_cd = @kameiten_cd_from ") '--加盟店コード ")
                    paramList.Add(MakeParam("@kameiten_cd_from", SqlDbType.VarChar, 16, dicPrm("KameitenCd1")))
                End If
            End If
            '営業所コード
            If (Not dicPrm("EigyousyaCd1").Equals(String.Empty)) AndAlso (Not dicPrm("EigyousyaCd2").Equals(String.Empty)) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.eigyousyo_cd BETWEEN @eigyousyo_cd_from AND @eigyousyo_cd_to ") '--営業所コード ")
                paramList.Add(MakeParam("@eigyousyo_cd_from", SqlDbType.VarChar, 5, dicPrm("EigyousyaCd1")))
                paramList.Add(MakeParam("@eigyousyo_cd_to", SqlDbType.VarChar, 5, dicPrm("EigyousyaCd2")))
            Else
                If Not dicPrm("EigyousyaCd1").Equals(String.Empty) Then
                    .AppendLine("	AND ")
                    .AppendLine("	MKK.eigyousyo_cd = @eigyousyo_cd_from ") '--営業所コード ")
                    paramList.Add(MakeParam("@eigyousyo_cd_from", SqlDbType.VarChar, 5, dicPrm("EigyousyaCd1")))
                End If
            End If
            '系列コード
            If Not dicPrm("KeiretuCd").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.keiretu_cd = @keiretu_cd ") '--系列コード ")
                paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 5, dicPrm("KeiretuCd")))
            End If
            '管轄支店
            If Not dicPrm("Siten").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.busyo_cd = @busyo_cd ") '--管轄支店 ")
                paramList.Add(MakeParam("@busyo_cd", SqlDbType.VarChar, 16, dicPrm("Siten")))
            End If
            '都道府県コード
            If Not dicPrm("TodoufukenCd").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.todouhuken_cd = @todouhuken_cd ") '--都道府県コード ")
                paramList.Add(MakeParam("@todouhuken_cd", SqlDbType.VarChar, 2, dicPrm("TodoufukenCd")))
            End If
            '営業担当者
            If Not dicPrm("EigyouTantousya").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.eigyou_tantousya_id = @eigyou_tantousya_id ") '--営業担当者 ")
                paramList.Add(MakeParam("@eigyou_tantousya_id", SqlDbType.VarChar, 30, dicPrm("EigyouTantousya")))
            End If
            '営業区分
            If Not dicPrm("EigyouKbn").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.eigyou_kbn = @eigyou_kbn ") '--営業区分 ")
                paramList.Add(MakeParam("@eigyou_kbn", SqlDbType.VarChar, 1, dicPrm("EigyouKbn")))
            End If
            '営業担当所属
            If Not dicPrm("EigyouTantouSyozaku").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	METS.syozoku_cd = @syozoku_cd ") '--営業担当所属 ")
                paramList.Add(MakeParam("@syozoku_cd", SqlDbType.Int, 10, dicPrm("EigyouTantouSyozaku")))
            End If
            '業態
            If Not dicPrm("Gyoutai").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKKI.gyoutai = @gyoutai ") '--業態 ")
                paramList.Add(MakeParam("@gyoutai", SqlDbType.Int, 10, dicPrm("Gyoutai")))
            End If
            '年間棟数
            If (Not dicPrm("NenkanTousuu1").Equals(String.Empty)) AndAlso (Not dicPrm("NenkanTousuu2").Equals(String.Empty)) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.keikaku_nenkan_tousuu BETWEEN @keikaku_nenkan_tousuu_from AND @keikaku_nenkan_tousuu_to ") '--年間棟数 ")
                paramList.Add(MakeParam("@keikaku_nenkan_tousuu_from", SqlDbType.Int, 5, dicPrm("NenkanTousuu1")))
                paramList.Add(MakeParam("@keikaku_nenkan_tousuu_to", SqlDbType.Int, 5, dicPrm("NenkanTousuu2")))
            Else
                If Not dicPrm("NenkanTousuu1").Equals(String.Empty) Then
                    .AppendLine("	AND ")
                    .AppendLine("	MKK.keikaku_nenkan_tousuu = @keikaku_nenkan_tousuu_from ") '--年間棟数 ")
                    paramList.Add(MakeParam("@keikaku_nenkan_tousuu_from", SqlDbType.Int, 5, dicPrm("NenkanTousuu1")))
                End If
            End If
            '計画用_年間棟数
            If (Not dicPrm("KeikakuyouNenkanTousuu1").Equals(String.Empty)) AndAlso (Not dicPrm("KeikakuyouNenkanTousuu2").Equals(String.Empty)) Then
                .AppendLine("	AND ")
                .AppendLine("	MKKI.keikakuyou_nenkan_tousuu BETWEEN @keikakuyou_nenkan_tousuu_from AND @keikakuyou_nenkan_tousuu_to ") '--計画用_年間棟数 ")
                paramList.Add(MakeParam("@keikakuyou_nenkan_tousuu_from", SqlDbType.Int, 10, dicPrm("KeikakuyouNenkanTousuu1")))
                paramList.Add(MakeParam("@keikakuyou_nenkan_tousuu_to", SqlDbType.Int, 10, dicPrm("KeikakuyouNenkanTousuu2")))
            Else
                If Not dicPrm("KeikakuyouNenkanTousuu1").Equals(String.Empty) Then
                    .AppendLine("	AND ")
                    .AppendLine("	MKKI.keikakuyou_nenkan_tousuu = @keikakuyou_nenkan_tousuu_from ") '--計画用_年間棟数 ")
                    paramList.Add(MakeParam("@keikakuyou_nenkan_tousuu_from", SqlDbType.Int, 10, dicPrm("KeikakuyouNenkanTousuu1")))
                End If
            End If
            '加盟店属性1
            If Not dicPrm("KameitenZokusei1").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKKI.kameiten_zokusei_1 = @kameiten_zokusei_1 ") '--加盟店属性1 ")
                paramList.Add(MakeParam("@kameiten_zokusei_1", SqlDbType.Int, 2, dicPrm("KameitenZokusei1")))
            End If
            '加盟店属性2
            If Not dicPrm("KameitenZokusei2").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKKI.kameiten_zokusei_2 = @kameiten_zokusei_2 ") '--加盟店属性2 ")
                paramList.Add(MakeParam("@kameiten_zokusei_2", SqlDbType.Int, 2, dicPrm("KameitenZokusei2")))
            End If
            '加盟店属性3
            If Not dicPrm("KameitenZokusei3").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKKI.kameiten_zokusei_3 = @kameiten_zokusei_3 ") '--加盟店属性3 ")
                paramList.Add(MakeParam("@kameiten_zokusei_3", SqlDbType.Int, 2, dicPrm("KameitenZokusei3")))
            End If
            '加盟店属性4
            If Not dicPrm("KameitenZokusei4").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKKI.kameiten_zokusei_4 = @kameiten_zokusei_4 ") '--加盟店属性4 ")
                paramList.Add(MakeParam("@kameiten_zokusei_4", SqlDbType.Int, 2, dicPrm("KameitenZokusei4")))
            End If
            '加盟店属性5
            If Not dicPrm("KameitenZokusei5").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKKI.kameiten_zokusei_5 = @kameiten_zokusei_5 ") '--加盟店属性2 ")
                paramList.Add(MakeParam("@kameiten_zokusei_5", SqlDbType.Int, 2, dicPrm("KameitenZokusei5")))
            End If
            '加盟店属性6
            If Not dicPrm("KameitenZokusei6").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKKI.kameiten_zokusei_6 = @kameiten_zokusei_6 ") '--加盟店属性6 ")
                paramList.Add(MakeParam("@kameiten_zokusei_6", SqlDbType.VarChar, 40, dicPrm("KameitenZokusei6")))
            End If
            '計画値有無
            If Not dicPrm("Keikaku0Flg").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.keikaku0_flg = @keikaku0_flg ") '--計画値有無 ")
                paramList.Add(MakeParam("@keikaku0_flg", SqlDbType.Int, 1, dicPrm("Keikaku0Flg")))
            End If
        End With

        ' 検索実行
        Dim intCount As Integer
        intCount = Convert.ToInt32(SQLHelper.ExecuteScalar(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), paramList.ToArray))

        Return intCount

    End Function


End Class

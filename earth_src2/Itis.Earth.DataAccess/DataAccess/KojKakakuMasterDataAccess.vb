Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports System.Text

Public Class KojKakakuMasterDataAccess
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>工事価格データ件数を取得する</summary>
    ''' <param name="dtInfo">パラメータ</param>
    ''' <returns>工事価格データ件数</returns>
    Public Function SelKojKakakuInfoCount(ByVal dtInfo As DataTable) As Integer

        'DataSetインスタンスの生成
        Dim dsKojKakaku As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'SQL文
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   COUNT(*) AS count ")
            .AppendLine(ComSQL(dtInfo))

        End With
        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsKojKakaku, _
                    "dtKoiKakakuCount", ComParam(dtInfo).ToArray)

        Return dsKojKakaku.Tables("dtKoiKakakuCount").Rows(0).Item("count")

    End Function
    Private Function ComParam(ByVal dtInfo As DataTable) As List(Of SqlClient.SqlParameter)
        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        '相手先種別

        paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 4, dtInfo.Rows(0).Item("aitesaki_syubetu")))

        '相手先コード
        If dtInfo.Rows(0).Item("aitesaki_cd_from") <> String.Empty And dtInfo.Rows(0).Item("aitesaki_cd_to") <> String.Empty Then
            paramList.Add(MakeParam("@aitesaki_cd_from", SqlDbType.VarChar, 5, dtInfo.Rows(0).Item("aitesaki_cd_from")))
            paramList.Add(MakeParam("@aitesaki_cd_to", SqlDbType.VarChar, 5, dtInfo.Rows(0).Item("aitesaki_cd_to")))
        ElseIf dtInfo.Rows(0).Item("aitesaki_cd_from") <> String.Empty And dtInfo.Rows(0).Item("aitesaki_cd_to") = String.Empty Then
            paramList.Add(MakeParam("@aitesaki_cd_from", SqlDbType.VarChar, 5, dtInfo.Rows(0).Item("aitesaki_cd_from")))
        ElseIf dtInfo.Rows(0).Item("aitesaki_cd_from") = String.Empty And dtInfo.Rows(0).Item("aitesaki_cd_to") <> String.Empty Then
            paramList.Add(MakeParam("@aitesaki_cd_to", SqlDbType.VarChar, 5, dtInfo.Rows(0).Item("aitesaki_cd_to")))
        End If

        '商品コード
        If dtInfo.Rows(0).Item("syouhin_cd") <> String.Empty Then
            paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtInfo.Rows(0).Item("syouhin_cd")))
        End If

        '工事会社
        If dtInfo.Rows(0).Item("kojkaisya_cd") <> String.Empty Then
            paramList.Add(MakeParam("@kojkaisya_cd", SqlDbType.VarChar, 8, dtInfo.Rows(0).Item("kojkaisya_cd")))
        End If

        '取消
        If dtInfo.Rows(0).Item("torikesi") <> String.Empty Then
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, 0))
        End If

        '取消相手先
        If dtInfo.Rows(0).Item("torikesi_aitesaki") <> String.Empty Then
            paramList.Add(MakeParam("@torikesi_aitesaki", SqlDbType.Int, 4, 0))
        End If
        Return paramList
    End Function
    Private Function ComSQL(ByVal dtInfo As DataTable) As String
        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'SQL文
        With commandTextSb
            .AppendLine(" FROM ")
            .AppendLine("   m_koj_kakaku MHK WITH(READCOMMITTED) ")     '工事価格M
            .AppendLine(" LEFT OUTER JOIN ( ")
            .AppendLine("   SELECT ")
            .AppendLine("       0 AS aitesaki_syubetu ")
            .AppendLine("       ,'ALL' AS aitesaki_cd ")
            .AppendLine("       ,'相手先なし' AS aitesaki_mei ")
            .AppendLine("       ,0 AS torikesi ")
            .AppendLine("   UNION ALL ")
            .AppendLine("   SELECT ")
            .AppendLine("       1 AS aitesaki_syubetu ")
            .AppendLine("       ,kameiten_cd AS aitesaki_cd ")
            .AppendLine("       ,kameiten_mei1 AS aitesaki_mei ")
            .AppendLine("       ,torikesi ")
            .AppendLine("   FROM ")
            .AppendLine("       m_kameiten WITH(READCOMMITTED) ")   '加盟店マスタ
            .AppendLine("   UNION ALL ")
            .AppendLine("   SELECT ")
            .AppendLine("       5 AS aitesaki_syubetu ")
            .AppendLine("       ,eigyousyo_cd AS aitesaki_cd ")
            .AppendLine("       ,eigyousyo_mei AS aitesaki_mei ")
            .AppendLine("       ,torikesi ")
            .AppendLine("   FROM ")
            .AppendLine("       m_eigyousyo WITH(READCOMMITTED) ")   '営業所マスタ
            .AppendLine("   UNION ALL ")
            .AppendLine("   SELECT ")
            .AppendLine("       7 AS aitesaki_syubetu ")
            .AppendLine("       ,keiretu_cd AS aitesaki_cd ")
            .AppendLine("       ,MIN(keiretu_mei) AS aitesaki_mei ")
            .AppendLine("       ,MIN(torikesi) AS torikesi ")
            .AppendLine("   FROM ")
            .AppendLine("       m_keiretu WITH(READCOMMITTED) ")   '系列マスタ
            .AppendLine("   GROUP BY ")
            .AppendLine("       keiretu_cd ")
            .AppendLine("   ) SUB ")
            .AppendLine(" ON    MHK.aitesaki_syubetu = SUB.aitesaki_syubetu ")
            .AppendLine(" AND   MHK.aitesaki_cd = SUB.aitesaki_cd ")
            .AppendLine(" LEFT OUTER JOIN ")
            .AppendLine("   m_syouhin MS WITH(READCOMMITTED) ")     '商品マスタ
            .AppendLine(" ON    MHK.syouhin_cd = MS.syouhin_cd ")
            .AppendLine(" AND  (MS.souko_cd = '130' OR MS.souko_cd = '140')")
            .AppendLine(" AND   MS.torikesi = 0 ")
            .AppendLine(" AND   MS.koj_type LIKE '1%' ")
            .AppendLine(" LEFT OUTER JOIN (")
            .AppendLine("   SELECT ")
            .AppendLine("       'ALL' AS tys_kaisya_cd ")
            .AppendLine("       ,'AL' AS jigyousyo_cd ")
            .AppendLine("       ,'指定無し' AS tys_kaisya_mei ")
            .AppendLine("   UNION ALL ")
            .AppendLine("   SELECT ")
            .AppendLine("       tys_kaisya_cd AS tys_kaisya_cd ")
            .AppendLine("       ,jigyousyo_cd AS jigyousyo_cd ")
            .AppendLine("       ,tys_kaisya_mei AS tys_kaisya_mei ")
            .AppendLine("   FROM ")
            .AppendLine("       m_tyousakaisya WITH(READCOMMITTED) ")   '調査会社マスタ
            .AppendLine("   ) AS KKK ")
            .AppendLine(" ON    MHK.koj_gaisya_cd = KKK.tys_kaisya_cd ")
            .AppendLine(" AND    MHK.koj_gaisya_jigyousyo_cd = KKK.jigyousyo_cd ")
            .AppendLine(" LEFT OUTER JOIN ")
            .AppendLine("   m_kakutyou_meisyou MKM WITH(READCOMMITTED) ")  '拡張名称M
            .AppendLine(" ON    MHK.aitesaki_syubetu = MKM.code ")
            .AppendLine(" AND   MKM.meisyou_syubetu = 23 ")
            .AppendLine(" WHERE ")

            '相手先種別
            .AppendLine(" MHK.aitesaki_syubetu = @aitesaki_syubetu ")

            '相手先コード
            If dtInfo.Rows(0).Item("aitesaki_cd_from") <> String.Empty And dtInfo.Rows(0).Item("aitesaki_cd_to") <> String.Empty Then
                .AppendLine(" AND MHK.aitesaki_cd BETWEEN @aitesaki_cd_from AND @aitesaki_cd_to ")
            ElseIf dtInfo.Rows(0).Item("aitesaki_cd_from") <> String.Empty And dtInfo.Rows(0).Item("aitesaki_cd_to") = String.Empty Then
                .AppendLine(" AND MHK.aitesaki_cd = @aitesaki_cd_from ")
            ElseIf dtInfo.Rows(0).Item("aitesaki_cd_from") = String.Empty And dtInfo.Rows(0).Item("aitesaki_cd_to") <> String.Empty Then
                .AppendLine(" AND MHK.aitesaki_cd = @aitesaki_cd_to ")
            End If

            '商品コード
            If dtInfo.Rows(0).Item("syouhin_cd") <> String.Empty Then
                .AppendLine(" AND MHK.syouhin_cd = @syouhin_cd ")
            End If

            '工事会社
            If dtInfo.Rows(0).Item("kojkaisya_cd") <> String.Empty Then
                .AppendLine(" AND ISNULL(MHK.koj_gaisya_cd,'')+ISNULL(MHK.koj_gaisya_jigyousyo_cd,'') = @kojkaisya_cd ")
            End If

            '取消
            If dtInfo.Rows(0).Item("torikesi") <> String.Empty Then
                .AppendLine(" AND MHK.torikesi = @torikesi ")
            End If

            '取消相手先
            If dtInfo.Rows(0).Item("torikesi_aitesaki") <> String.Empty Then
                .AppendLine(" AND SUB.torikesi = @torikesi_aitesaki ")
            End If
        End With
        Return commandTextSb.ToString
    End Function
    ''' <summary>工事価格のデータを取得する</summary>
    ''' <param name="dtInfo">パラメータ</param>
    ''' <returns>工事価格データテーブル</returns>
    Public Function SelKojKakakuSeiteInfo(ByVal dtInfo As DataTable) As DataTable
        'DataSetインスタンスの生成
        Dim dsKojKakaku As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine(" SELECT ")
            If dtInfo.Rows(0).Item("kensaku_count") = "100" Then
                .AppendLine("      TOP 100 ")
            ElseIf dtInfo.Rows(0).Item("kensaku_count") = "10" Then
                .AppendLine("      TOP 10 ")
            End If
            '工事価格M.相手先種別:拡張名称M.名称
            .AppendLine("   CAST(MHK.aitesaki_syubetu AS VARCHAR) + '：' + ISNULL(MKM.meisyou,'') AS aitesaki ")
            .AppendLine("   ,MHK.aitesaki_cd ")                             '工事価格M.相手先コード
            .AppendLine("   ,SUB.aitesaki_mei ")                            'サブ.相手先名
            .AppendLine("   ,MHK.syouhin_cd ")                              '工事価格M.商品コード
            .AppendLine("   ,MS.syouhin_mei ")                              '商品M.商品名
            .AppendLine("   ,MHK.koj_gaisya_cd +MHK.koj_gaisya_jigyousyo_cd AS koj_cd") '工事価格M.工コード
            .AppendLine("   ,KKK.tys_kaisya_mei ")                          '工事会社M.工事会社名
            .AppendLine("   ,CASE MHK.torikesi ")
            .AppendLine("       WHEN 0 THEN '' ")
            .AppendLine("       ELSE '取消' ")
            .AppendLine("    END AS torikesi ")                             '工事価格M.取消
            .AppendLine("   ,MHK.uri_gaku ")                                '工事価格M.売上金額
            .AppendLine("   ,CASE MHK.koj_gaisya_seikyuu_umu ")
            .AppendLine("       WHEN 1 THEN '有' ")
            .AppendLine("       ELSE '無' ")
            .AppendLine("    END AS kojumu ")                             '工事価格M.工事会社請求有無
            .AppendLine("   ,CASE ISNULL(MHK.seikyuu_umu,2) ")
            .AppendLine("       WHEN 0 THEN '無' ")
            .AppendLine("       WHEN 1 THEN '有' ")
            .AppendLine("       ELSE '' ")
            .AppendLine("    END AS seikyuumu ")                             '工事価格M.請求有無

            .AppendLine(ComSQL(dtInfo))
            'ソート順
            .AppendLine(" ORDER BY ")
            .AppendLine("      MHK.aitesaki_syubetu ")
            .AppendLine("      ,MHK.aitesaki_cd ")
            .AppendLine("      ,MHK.syouhin_cd ")
            .AppendLine("      ,MHK.koj_gaisya_cd ")
            .AppendLine("      ,MHK.koj_gaisya_jigyousyo_cd ")
        End With

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsKojKakaku, _
                    "dsKojKakaku", ComParam(dtInfo).ToArray)

        Return dsKojKakaku.Tables("dsKojKakaku")

    End Function
    ''' <summary>工事価格CSVデータを取得する</summary>
    ''' <param name="dtInfo">パラメータ</param>
    ''' <returns>工事価格CSVデータテーブル</returns>
    Public Function SelKojKakakuCSVInfo(ByVal dtInfo As DataTable) As DataTable

        'DataSetインスタンスの生成
        Dim dsKojKakaku As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'SQL文
        With commandTextSb
            .AppendLine(" SELECT ")
            'EDI情報作成日
            .AppendLine("   RIGHT(CONVERT(varchar(10),GETDATE(),112),4) + REPLACE(CONVERT(varchar(8),GETDATE(),108),':','') AS edi_jouhou_sakusei_date ")
            .AppendLine("   ,ISNULL(MHK.aitesaki_syubetu,'') AS aitesaki_syubetu ")                         '工事価格M.相手先種別
            .AppendLine("   ,ISNULL(MHK.aitesaki_cd,'') AS  aitesaki_cd")                                   '工事価格M.相手先コード
            .AppendLine("   ,ISNULL(SUB.aitesaki_mei,'') AS  aitesaki_mei")                                 'サブ.相手先名
            .AppendLine("   ,ISNULL(MHK.syouhin_cd,'') AS  syouhin_cd")                                     '工事価格M.商品コード
            .AppendLine("   ,ISNULL(MS.syouhin_mei,'') AS  syouhin_mei")                                    '商品M.商品名
            .AppendLine("   ,ISNULL(MHK.koj_gaisya_cd,'') AS  koj_gaisya_cd")                               '工事価格M.工事会社コード
            .AppendLine("   ,ISNULL(MHK.koj_gaisya_jigyousyo_cd,'') AS  koj_gaisya_jigyousyo_cd")           '工事価格M.工事会社事業所コード
            .AppendLine("   ,ISNULL(KKK.tys_kaisya_mei,'') AS  tys_kaisya_mei")                             '工事会社M.工事会社名
            .AppendLine("   ,ISNULL(MHK.torikesi,'') AS  torikesi")                                         '工事価格M.取消
            .AppendLine("   ,ISNULL(MHK.uri_gaku,'') AS  uri_gaku")                                         '工事価格M.売上金額
            .AppendLine("   ,ISNULL(MHK.koj_gaisya_seikyuu_umu,'') AS  koj_gaisya_seikyuu_umu")             '工事価格M.工事会社請求有無
            .AppendLine("   ,ISNULL(MHK.seikyuu_umu,'') AS  seikyuu_umu")                                   '工事価格M.請求有無
            .AppendLine(ComSQL(dtInfo))
            'ソート順
            .AppendLine(" ORDER BY ")
            .AppendLine("      MHK.aitesaki_syubetu ")
            .AppendLine("      ,MHK.aitesaki_cd ")
            .AppendLine("      ,MHK.syouhin_cd ")
            .AppendLine("      ,MHK.koj_gaisya_cd ")
            .AppendLine("      ,MHK.koj_gaisya_jigyousyo_cd ")
        End With

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsKojKakaku, _
                    "dsKojKakaku", ComParam(dtInfo).ToArray)
        Return dsKojKakaku.Tables("dsKojKakaku")

    End Function
    ''' <summary>商品を取得する</summary>
    ''' <returns>商品データテーブル</returns>
    Public Function SelSyouhin(Optional ByVal syohuinCd As String = "") As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsSyouhin As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'SQL文
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   syouhin_cd ")  '商品コード
            .AppendLine("   ,syouhin_cd + '：' + ISNULL(syouhin_mei,'') AS syouhin ") '商品
            .AppendLine(" FROM ")
            .AppendLine("   m_syouhin WITH(READCOMMITTED) ") '商品マスタ
            .AppendLine(" WHERE  ")  '取消
            .AppendLine("  (souko_cd = '130' OR souko_cd = '140')")
            .AppendLine(" AND   torikesi = 0 ")
            .AppendLine(" AND   koj_type LIKE '1%' ")
            If syohuinCd <> "" Then
                .AppendLine(" AND   syouhin_cd ='" & syohuinCd & "'")
            End If
            .AppendLine(" ORDER BY ")
            .AppendLine("   syouhin_cd ")
        End With

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsSyouhin, "dtSyouhin")

        Return dsSyouhin.Tables("dtSyouhin")

    End Function
    ''' <summary>工事会社データを取得する</summary>
    Public Function SelKojKaisyaKensaku(ByVal strCd As String) As DataTable
        ' DataSetインスタンスの生成()
        Dim dsKojKaisya As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine("      ISNULL(tys_kaisya_cd,'')+ISNULL(jigyousyo_cd,'') AS cd,tys_kaisya_mei ")
        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine("      m_tyousakaisya  WITH(READCOMMITTED)  ")
        commandTextSb.AppendLine(" WHERE  ")
        commandTextSb.AppendLine("  ISNULL(tys_kaisya_cd,'')+ISNULL(jigyousyo_cd,'') = @Cd ")

        'パラメータの設定
        paramList.Add(MakeParam("@Cd", SqlDbType.VarChar, 7, strCd))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsKojKaisya, _
                    "dsKojKaisya", paramList.ToArray)
        Return dsKojKaisya.Tables(0)

    End Function

    ''' <summary>アップロード管理を取得する</summary>
    ''' <returns>アップロード管理データテーブル</returns>
    Public Function SelUploadKanri() As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsHanbaiKakaku As New Data.DataSet

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
            .AppendLine("	file_kbn = 5 ")         'ファイル区分
            .AppendLine(" ORDER BY ")
            .AppendLine("	syori_datetime DESC ")  '処理日時(降順)
        End With

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanbaiKakaku, "dtUploadKanri")

        Return dsHanbaiKakaku.Tables("dtUploadKanri")

    End Function

    ''' <summary>アップロード管理の件数を取得する</summary>
    ''' <returns>アップロード管理の件数</returns>
    Public Function SelUploadKanriCount() As Integer

        'DataSetインスタンスの生成
        Dim dsHanbaiKakaku As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'SQL文
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("	COUNT(*) AS count ")                    '件数
            .AppendLine(" FROM ")
            .AppendLine("	t_upload_kanri WITH(READCOMMITTED) ")   'アップロード管理テーブル
            .AppendLine(" WHERE ")
            .AppendLine("	file_kbn =5 ")                         'ファイル区分
        End With

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanbaiKakaku, "dtUploadKanri")

        Return CInt(dsHanbaiKakaku.Tables("dtUploadKanri").Rows(0).Item("count").ToString)

    End Function
    ''' <summary>工事価格データ存在チェック</summary>
    ''' <param name="strAitesakiSyubetu">相手先種別</param>
    ''' <param name="strAitesakiCd">相手先コード</param>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <param name="strKojCd">工事会社コード</param>
    ''' <returns>工事価格データ存在区分</returns>
    Public Function SelKojKakaku(ByVal strAitesakiSyubetu As String, ByVal strAitesakiCd As String, _
                                    ByVal strSyouhinCd As String, ByVal strKojCd As String) As Boolean

        'DataSetインスタンスの生成
        Dim dsKojKakaku As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   aitesaki_syubetu ")
            .AppendLine(" FROM ")
            .AppendLine("	m_koj_kakaku WITH(READCOMMITTED) ")
            .AppendLine(" WHERE ")
            .AppendLine("	aitesaki_syubetu = @aitesaki_syubetu ")
            .AppendLine(" AND ")
            .AppendLine("	aitesaki_cd = @aitesaki_cd ")
            .AppendLine(" AND ")
            .AppendLine("	syouhin_cd = @syouhin_cd ")
            .AppendLine(" AND ")
            .AppendLine("	ISNULL(koj_gaisya_cd,'')+ISNULL(koj_gaisya_jigyousyo_cd,'') = @koj_cd ")
        End With

        paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 4, strAitesakiSyubetu))
        paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, strAitesakiCd))
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))
        paramList.Add(MakeParam("@koj_cd", SqlDbType.VarChar, 7, strKojCd))

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsKojKakaku, "dsKojKakaku", paramList.ToArray)

        '戻り値
        If dsKojKakaku.Tables("dsKojKakaku").Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function
    ''' <summary>販売価格マスタを登録・更新する</summary>
    ''' <param name="dtKojKakakuOk">データ情報</param>
    ''' <returns>成功失敗区分</returns>
    Public Function InsUpdKojKakaku(ByVal dtKojKakakuOk As Data.DataTable, _
                                       ByVal strUserId As String) As Boolean

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
            .AppendLine(" INSERT INTO  ")
            .AppendLine("	m_koj_kakaku WITH(UPDLOCK) ")
            .AppendLine("	( ")
            .AppendLine("		aitesaki_syubetu ")                     '相手先種別
            .AppendLine("		,aitesaki_cd ")                         '相手先コード
            .AppendLine("		,syouhin_cd ")                          '商品コード
            .AppendLine("		,koj_gaisya_cd ")                       '工事会社コード
            .AppendLine("		,koj_gaisya_jigyousyo_cd ")             '工事会社事業所コード
            .AppendLine("		,torikesi ")                            '取消
            .AppendLine("		,uri_gaku ")                            '売上金額
            .AppendLine("		,koj_gaisya_seikyuu_umu ")              '工事会社請求有無
            .AppendLine("		,seikyuu_umu ")                         '請求有無
            .AppendLine("		,add_login_user_id ")                   '登録ログインユーザID
            .AppendLine("		,add_datetime ")                        '登録日時
            .AppendLine("		,upd_login_user_id ")                   '更新ログインユーザID
            .AppendLine("		,upd_datetime ")                        '更新日時
            .AppendLine("	) ")
            .AppendLine(" VALUES ")
            .AppendLine(" ( ")
            .AppendLine("	@aitesaki_syubetu ")
            .AppendLine("	,@aitesaki_cd ")
            .AppendLine("	,@syouhin_cd ")
            .AppendLine("	,@koj_gaisya_cd ")
            .AppendLine("	,@koj_gaisya_jigyousyo_cd ")
            .AppendLine("	,@torikesi ")
            .AppendLine("	,@uri_gaku ")
            .AppendLine("	,@koj_gaisya_seikyuu_umu ")
            .AppendLine("	,@seikyuu_umu ")
            .AppendLine("	,@add_login_user_id ")
            .AppendLine("	,GETDATE() ")
            .AppendLine("	,NULL ")
            .AppendLine("	,NULL ")
            .AppendLine(" ) ")
        End With

        '更新用sql文
        With strSqlUpd
            .AppendLine(" UPDATE ")
            .AppendLine("	m_koj_kakaku WITH(UPDLOCK) ")
            .AppendLine(" SET ")
            .AppendLine("	torikesi = @torikesi ")                             '取消
            .AppendLine("	,uri_gaku = @uri_gaku ")  '売上金額
            .AppendLine("	,koj_gaisya_seikyuu_umu = @koj_gaisya_seikyuu_umu ") '工事会社請求有無
            .AppendLine("	,seikyuu_umu = @seikyuu_umu ")                      '請求有無
            .AppendLine("	,upd_login_user_id = @upd_login_user_id ")      '更新ログインユーザID
            .AppendLine("	,upd_datetime = GETDATE() ")                    '更新日時
            .AppendLine(" WHERE aitesaki_syubetu = @aitesaki_syubetu ")     '相手先種別
            .AppendLine(" AND   aitesaki_cd = @aitesaki_cd ")               '相手先コード
            .AppendLine(" AND   syouhin_cd = @syouhin_cd ")                 '商品コード
            .AppendLine(" AND   ISNULL(koj_gaisya_cd,'')+ISNULL(koj_gaisya_jigyousyo_cd,'') = @koj_cd ")           '工事会社コード、工事会社事業所コード
        End With

        For i As Integer = 0 To dtKojKakakuOk.Rows.Count - 1
            'パラメータの設定
            paramList.Clear()
            paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 5, dtKojKakakuOk.Rows(i).Item("aitesaki_syubetu").ToString.Trim))
            paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, dtKojKakakuOk.Rows(i).Item("aitesaki_cd").ToString.Trim))
            paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtKojKakakuOk.Rows(i).Item("syouhin_cd").ToString.Trim))
            paramList.Add(MakeParam("@koj_gaisya_cd", SqlDbType.VarChar, 5, dtKojKakakuOk.Rows(i).Item("koj_gaisya_cd").ToString.Trim))
            paramList.Add(MakeParam("@koj_gaisya_jigyousyo_cd", SqlDbType.VarChar, 2, dtKojKakakuOk.Rows(i).Item("koj_gaisya_jigyousyo_cd").ToString.Trim))

            paramList.Add(MakeParam("@koj_cd", SqlDbType.VarChar, 7, dtKojKakakuOk.Rows(i).Item("koj_gaisya_cd").ToString.Trim & dtKojKakakuOk.Rows(i).Item("koj_gaisya_jigyousyo_cd").ToString.Trim))

            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, dtKojKakakuOk.Rows(i).Item("torikesi").ToString.Trim))
            If dtKojKakakuOk.Rows(i).Item("uri_gaku").ToString.Trim = String.Empty Then
                paramList.Add(MakeParam("@uri_gaku", SqlDbType.VarChar, 19, DBNull.Value))
            Else
                paramList.Add(MakeParam("@uri_gaku", SqlDbType.VarChar, 19, dtKojKakakuOk.Rows(i).Item("uri_gaku").ToString.Trim))
            End If
            If dtKojKakakuOk.Rows(i).Item("koj_gaisya_seikyuu_umu").ToString.Trim = String.Empty Then
                paramList.Add(MakeParam("@koj_gaisya_seikyuu_umu", SqlDbType.Int, 4, DBNull.Value))
            Else
                paramList.Add(MakeParam("@koj_gaisya_seikyuu_umu", SqlDbType.Int, 4, dtKojKakakuOk.Rows(i).Item("koj_gaisya_seikyuu_umu").ToString.Trim))
            End If
            If dtKojKakakuOk.Rows(i).Item("seikyuu_umu").ToString.Trim = String.Empty Then
                paramList.Add(MakeParam("@seikyuu_umu", SqlDbType.Int, 4, DBNull.Value))
            Else
                paramList.Add(MakeParam("@seikyuu_umu", SqlDbType.Int, 4, dtKojKakakuOk.Rows(i).Item("seikyuu_umu").ToString.Trim))
            End If
            paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strUserId))
            paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, strUserId))
            '更新されたデータセットを DB へ書き込み

            Try
                If dtKojKakakuOk.Rows(i).Item("ins_upd_flg").Equals("0") Then
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
    ''' <summary>販売価格エラー情報テーブルを登録する</summary>
    ''' <param name="dtKojKakakuError">エラーデータ情報</param>
    ''' <returns>成功失敗区分</returns>
    Public Function InsKojKakakuError(ByVal dtKojKakakuError As Data.DataTable, _
                                         ByVal strUploadDate As String, _
                                         ByVal strUserId As String) As Boolean

        '戻り値
        Dim InsCount As Integer = 0

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine(" INSERT INTO  ")
            .AppendLine("   m_koj_kakaku_error WITH(UPDLOCK) ")
            .AppendLine("   ( ")
            .AppendLine("		edi_jouhou_sakusei_date ")              'EDI情報作成日
            .AppendLine("		,gyou_no ")                             '行NO
            .AppendLine("		,syori_datetime ")                      '処理日時
            .AppendLine("		,aitesaki_syubetu ")                    '相手先種別
            .AppendLine("		,aitesaki_cd ")                         '相手先コード
            .AppendLine("		,aitesaki_mei ")                        '相手先名
            .AppendLine("		,syouhin_cd ")                          '商品コード
            .AppendLine("		,syouhin_mei ")                         '商品名
            .AppendLine("		,koj_gaisya_cd ")                       '工事会社コード
            .AppendLine("		,koj_gaisya_jigyousyo_cd ")             '工事会社事業所コード
            .AppendLine("		,koj_gaisya_mei ")                      '工事会社名
            .AppendLine("		,torikesi ")                            '取消
            .AppendLine("		,uri_gaku ")                            '売上金額
            .AppendLine("		,koj_gaisya_seikyuu_umu ")              '工事会社請求有無
            .AppendLine("		,seikyuu_umu ")                         '請求有無
            .AppendLine("		,add_login_user_id ")                   '登録ログインユーザID
            .AppendLine("		,add_datetime ")                        '登録日時
            .AppendLine("		,upd_login_user_id ")                   '更新ログインユーザID
            .AppendLine("		,upd_datetime ")                        '更新日時
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("( ")
            .AppendLine("	@edi_jouhou_sakusei_date ")
            .AppendLine("	,@gyou_no ")
            .AppendLine("	,@syori_datetime ")
            .AppendLine("	,@aitesaki_syubetu ")
            .AppendLine("	,@aitesaki_cd ")
            .AppendLine("	,@aitesaki_mei ")
            .AppendLine("	,@syouhin_cd ")
            .AppendLine("	,@syouhin_mei ")
            .AppendLine("	,@koj_gaisya_cd ")
            .AppendLine("	,@koj_gaisya_jigyousyo_cd ")
            .AppendLine("	,@koj_gaisya_mei ")
            .AppendLine("	,@torikesi ")
            .AppendLine("	,@uri_gaku ")
            .AppendLine("	,@koj_gaisya_seikyuu_umu ")
            .AppendLine("	,@seikyuu_umu ")
            .AppendLine("	,@add_login_user_id ")
            .AppendLine("	,GETDATE() ")
            .AppendLine("	,NULL ")
            .AppendLine("	,NULL ")
            .AppendLine(") ")
        End With

        For i As Integer = 0 To dtKojKakakuError.Rows.Count - 1

            'パラメータの設定
            paramList.Clear()
            paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, dtKojKakakuError.Rows(i).Item(0).ToString.Trim))
            paramList.Add(MakeParam("@gyou_no", SqlDbType.Int, 4, dtKojKakakuError.Rows(i).Item(12).ToString.Trim))
            paramList.Add(MakeParam("@syori_datetime", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(strUploadDate))))
            paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.VarChar, 5, dtKojKakakuError.Rows(i).Item(1).ToString.Trim))
            paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, dtKojKakakuError.Rows(i).Item(2).ToString.Trim))
            paramList.Add(MakeParam("@aitesaki_mei", SqlDbType.VarChar, 40, dtKojKakakuError.Rows(i).Item(3).ToString.Trim))
            paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtKojKakakuError.Rows(i).Item(4).ToString.Trim))
            paramList.Add(MakeParam("@syouhin_mei", SqlDbType.VarChar, 40, dtKojKakakuError.Rows(i).Item(5).ToString.Trim))

            paramList.Add(MakeParam("@koj_gaisya_cd", SqlDbType.VarChar, 5, Left(Right("       " & dtKojKakakuError.Rows(i).Item(6).ToString.Trim, 7), 5).Trim))
            paramList.Add(MakeParam("@koj_gaisya_jigyousyo_cd", SqlDbType.VarChar, 2, Right(dtKojKakakuError.Rows(i).Item(6).ToString.Trim, 2)))
            paramList.Add(MakeParam("@koj_gaisya_mei", SqlDbType.VarChar, 40, dtKojKakakuError.Rows(i).Item(7).ToString.Trim))


            paramList.Add(MakeParam("@torikesi", SqlDbType.VarChar, 1, dtKojKakakuError.Rows(i).Item(8).ToString.Trim))
            paramList.Add(MakeParam("@uri_gaku", SqlDbType.VarChar, 10, dtKojKakakuError.Rows(i).Item(9).ToString.Trim))
            If dtKojKakakuError.Rows(i).Item(10).ToString.Trim = String.Empty Then
                paramList.Add(MakeParam("@koj_gaisya_seikyuu_umu", SqlDbType.VarChar, 1, DBNull.Value))

            Else
                paramList.Add(MakeParam("@koj_gaisya_seikyuu_umu", SqlDbType.VarChar, 1, dtKojKakakuError.Rows(i).Item(10).ToString.Trim))

            End If
            If dtKojKakakuError.Rows(i).Item(11).ToString.Trim = String.Empty Then
                paramList.Add(MakeParam("@seikyuu_umu", SqlDbType.VarChar, 1, DBNull.Value))
            Else
                paramList.Add(MakeParam("@seikyuu_umu", SqlDbType.VarChar, 1, dtKojKakakuError.Rows(i).Item(11).ToString.Trim))

            End If

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
        Next

        Return True
    End Function
    ''' <summary>アップロード管理テーブルを登録する</summary>
    ''' <param name="strUploadDate">処理日時</param>
    ''' <param name="strNyuuryokuFileMei">入力ファイル名</param>
    ''' <param name="strEdiJouhouSakuseiDate">EDI情報作成日</param>
    ''' <param name="strErrorUmu">エラー有無</param>
    ''' <param name="strUserId">ユーザーID</param>
    ''' <returns>成功失敗区分</returns>
    Public Function InsUploadKanri(ByVal strUploadDate As String, _
                                   ByVal strNyuuryokuFileMei As String, _
                                   ByVal strEdiJouhouSakuseiDate As String, _
                                   ByVal strErrorUmu As Integer, _
                                   ByVal strUserId As String) As Boolean

        '戻り値
        Dim InsCount As Integer = 0

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine(" INSERT INTO  ")
            .AppendLine("	t_upload_kanri ")
            .AppendLine("	( ")
            .AppendLine("       syori_datetime ")
            .AppendLine("		,nyuuryoku_file_mei ")
            .AppendLine("		,edi_jouhou_sakusei_date ")
            .AppendLine("		,error_umu ")
            .AppendLine("		,file_kbn ")
            .AppendLine("		,add_login_user_id ")
            .AppendLine("		,add_datetime ")
            .AppendLine("		,upd_login_user_id ")
            .AppendLine("		,upd_datetime ")
            .AppendLine("	) ")
            .AppendLine(" VALUES ")
            .AppendLine(" ( ")
            .AppendLine("	@syori_datetime ")
            .AppendLine("	,@nyuuryoku_file_mei ")
            .AppendLine("	,@edi_jouhou_sakusei_date ")
            .AppendLine("	,@error_umu ")
            .AppendLine("	,5 ")
            .AppendLine("	,@add_login_user_id ")
            .AppendLine("	,GETDATE() ")
            .AppendLine("	,NULL ")
            .AppendLine("	,NULL ")
            .AppendLine(" ) ")
        End With

        paramList.Add(MakeParam("@syori_datetime", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(strUploadDate))))
        paramList.Add(MakeParam("@nyuuryoku_file_mei", SqlDbType.VarChar, 128, strNyuuryokuFileMei))
        paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdiJouhouSakuseiDate))
        paramList.Add(MakeParam("@error_umu", SqlDbType.Int, 4, strErrorUmu))
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strUserId))

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

    ''' <summary>工事価格エラー情報を取得する</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI情報作成日</param>
    ''' <param name="strSyoriDate">処理日時</param>
    ''' <returns>工事価格エラーデータテーブル</returns>
    Public Function SelKojKakakuErr(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoridate As String) _
                        As DataTable

        'DataSetインスタンスの生成
        Dim dsKojKakakuErr As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   TOP 100 ")
            .AppendLine("   MHKE.gyou_no ")                 '工事価格エラー情報T.行NO
            '工事価格エラー情報T.相手先種別：拡名M.名称
            .AppendLine("   ,CASE WHEN ISNULL(MHKE.aitesaki_syubetu,'') = '' THEN '' ")
            .AppendLine("         ELSE MHKE.aitesaki_syubetu + '：' + ISNULL(MKM.meisyou,'') ")
            .AppendLine("    END AS aitesaki ")
            .AppendLine("   ,MHKE.aitesaki_cd ")            '工事価格エラー情報T.相手先コード
            .AppendLine("   ,MHKE.aitesaki_mei ")           '工事価格エラー情報T.相手先名
            .AppendLine("   ,MHKE.syouhin_cd ")             '工事価格エラー情報T.商品コード
            .AppendLine("   ,MHKE.syouhin_mei ")            '工事価格エラー情報T.商品名
            .AppendLine("   ,ISNULL(MHKE.koj_gaisya_cd,'')+ISNULL(MHKE.koj_gaisya_jigyousyo_cd,'') AS koj_cd ")  '工事価格エラー情報T.工事会社コード
            .AppendLine("   ,MHKE.koj_gaisya_mei ") '工事価格エラー情報T.工事会社名
            .AppendLine("   ,CASE MHKE.torikesi ")
            .AppendLine("       WHEN '0' THEN '' ")
            .AppendLine("       ELSE '取消' ")
            .AppendLine("    END AS torikesi ")                 '工事価格エラー情報T.取消
            .AppendLine("   ,MHKE.uri_gaku ")      '工事価格エラー情報T.売上金額
            .AppendLine("   ,CASE ISNULL(MHKE.koj_gaisya_seikyuu_umu,'0') ")
            .AppendLine("       WHEN '0' THEN '工事会社請求無' ")
            .AppendLine("       ELSE '工事会社請求有' ")
            .AppendLine("    END AS koj_gaisya_seikyuu_umu ")     '工事価格エラー情報T.工事会社請求有無
            .AppendLine("   ,CASE ISNULL(MHKE.seikyuu_umu,'0') ")
            .AppendLine("       WHEN '0' THEN '無し' ")
            .AppendLine("       ELSE '有り' ")
            .AppendLine("    END AS seikyuu_umu ")     '工事価格エラー情報T.請求有無
            .AppendLine(" FROM ")
            .AppendLine("   m_koj_kakaku_error MHKE WITH(READCOMMITTED) ")   '工事価格エラー情報T
            .AppendLine(" LEFT OUTER JOIN ")
            .AppendLine("   m_kakutyou_meisyou MKM WITH(READCOMMITTED) ")   '拡張名称M
            .AppendLine(" ON    MHKE.aitesaki_syubetu = MKM.code ")
            .AppendLine(" AND   MKM.meisyou_syubetu = 23 ")
            .AppendLine(" WHERE ")

            'EDI情報作成日
            .AppendLine(" MHKE.edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date ")
            paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdiJouhouSakuseiDate))

            '処理日時
            .AppendLine(" AND CONVERT(varchar(100),MHKE.syori_datetime,112) + REPLACE(CONVERT(varchar(100),MHKE.syori_datetime,114),':','') = @syori_datetime")
            paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 40, strSyoridate))

            'ソート順
            .AppendLine(" ORDER BY ")
            .AppendLine("      MHKE.gyou_no ")
        End With

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsKojKakakuErr, _
                    "dsKojKakakuErr", paramList.ToArray)

        Return dsKojKakakuErr.Tables("dsKojKakakuErr")

    End Function
    ''' <summary>工事価格エラー件数を取得する</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI情報作成日</param>
    ''' <param name="strSyoriDate">処理日時</param>
    ''' <returns>工事価格エラー件数</returns>
    Public Function SelKojKakakuErrCount(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoriDate As String) As Integer

        'DataSetインスタンスの生成
        Dim dsKojKakakuErr As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   Count(edi_jouhou_sakusei_date) AS count ")
            .AppendLine(" FROM ")
            .AppendLine("   m_koj_kakaku_error WITH(READCOMMITTED) ")   '工事価格エラー情報T
            .AppendLine(" WHERE ")

            'EDI情報
            .AppendLine(" edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date ")
            paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdiJouhouSakuseiDate))

            '処理日時
            .AppendLine(" AND CONVERT(varchar(100),syori_datetime,112) + REPLACE(CONVERT(varchar(100),syori_datetime,114),':','') = @syori_datetime")
            paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 40, strSyoriDate))

        End With
        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsKojKakakuErr, _
                    "dsKojKakakuErr", paramList.ToArray)

        Return dsKojKakakuErr.Tables("dsKojKakakuErr").Rows(0).Item("count")
 


    End Function
    ''' <summary>工事価格エラーCSV情報を取得する</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI情報作成日</param>
    ''' <param name="strSyoriDate">処理日時</param>
    ''' <returns>工事価格エラーCSVデータテーブル</returns>
    Public Function SelKojKakakuErrCsv(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoriDate As String) _
                    As DataTable

        'DataSetインスタンスの生成
        Dim dsKojKakakuErr As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   TOP 5000 ")
            .AppendLine("   MHKE.edi_jouhou_sakusei_date ")             '工事価格エラー情報T.EDI情報作成日
            .AppendLine("   ,MHKE.gyou_no ")                            '工事価格エラー情報T.行NO
            .AppendLine("   ,MHKE.syori_datetime ")                     '工事価格エラー情報T.処理日時
            .AppendLine("   ,MHKE.aitesaki_syubetu ")                   '工事価格エラー情報T.相手先種別
            .AppendLine("   ,MHKE.aitesaki_cd ")                        '工事価格エラー情報T.相手先コード
            .AppendLine("   ,MHKE.aitesaki_mei ")                       '工事価格エラー情報T.相手先名
            .AppendLine("   ,MHKE.syouhin_cd ")                         '工事価格エラー情報T.商品コード
            .AppendLine("   ,MHKE.syouhin_mei ")                        '工事価格エラー情報T.商品名
            .AppendLine("   ,ISNULL(MHKE.koj_gaisya_cd,'')+ ISNULL(MHKE.koj_gaisya_jigyousyo_cd,'')  AS koj_gaisya_cd ")                      '工事価格エラー情報T.工事会社コード
            .AppendLine("   ,MHKE.koj_gaisya_mei ")                         '工事価格エラー情報T.工事会社名
            .AppendLine("   ,MHKE.torikesi ")                           '工事価格エラー情報T.取消
            .AppendLine("   ,MHKE.uri_gaku ")              '工事価格エラー情報T.売上金額
            .AppendLine("   ,MHKE.koj_gaisya_seikyuu_umu ")   '工事価格エラー情報T.工事会社請求有無
            .AppendLine("   ,MHKE.seikyuu_umu ")                  '工事価格エラー情報T.請求有無
            .AppendLine("   ,MHKE.add_login_user_id ")                  '工事価格エラー情報T.登録ログインユーザID
            .AppendLine("   ,MHKE.add_datetime ")                       '工事価格エラー情報T.登録日時
            .AppendLine("   ,MHKE.upd_login_user_id ")                  '工事価格エラー情報T.更新ログインユーザID
            .AppendLine("   ,MHKE.upd_datetime ")                       '工事価格エラー情報T.更新日時
            .AppendLine(" FROM ")
            .AppendLine("   m_koj_kakaku_error MHKE WITH(READCOMMITTED) ")   '工事価格エラー情報T
            .AppendLine(" WHERE ")

            'EDI情報作成日
            .AppendLine(" MHKE.edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date ")
            paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdiJouhouSakuseiDate))

            '処理日時
            .AppendLine(" AND CONVERT(varchar(100),MHKE.syori_datetime,112) + REPLACE(CONVERT(varchar(100),MHKE.syori_datetime,114),':','') = @syori_datetime")
            paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 40, strSyoriDate))

            'ソート順
            .AppendLine(" ORDER BY ")
            .AppendLine("      MHKE.edi_jouhou_sakusei_date ")      '工事価格エラー情報T.EDI情報作成日
            .AppendLine("      ,MHKE.gyou_no ")                     '工事価格エラー情報T.行NO
        End With

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsKojKakakuErr, _
                    "dsKojKakakuErr", paramList.ToArray)

        Return dsKojKakakuErr.Tables("dsKojKakakuErr")

    End Function
    ''' <summary>工事価格マスタ個別設定データを取得する</summary>
    Public Function SelKojKakakuKobeituSettei(ByVal dtInfo As DataTable) As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT  ")
            .AppendLine("	MHK.aitesaki_syubetu ")
            .AppendLine("	,MHK.aitesaki_cd ")
            .AppendLine("	,ISNULL(SUB.aitesaki_mei,'') AS aitesaki_mei ")
            .AppendLine("	,MHK.syouhin_cd ")
            .AppendLine("   ,ISNULL(MHK.koj_gaisya_cd,'') + '：' +ISNULL(MHK.koj_gaisya_jigyousyo_cd,'') AS koj_cd") '工事価格M.工コード
            .AppendLine("   ,KKK.tys_kaisya_mei ")                          '工事会社M.工事会社名
            .AppendLine("	,MHK.torikesi ")
            .AppendLine("	,ISNULL(ISNULL(MHK.uri_gaku,MS.hyoujun_kkk),'') AS uri_gaku ")
            .AppendLine("	,MHK.koj_gaisya_seikyuu_umu AS kojumu ")
            .AppendLine("	,MHK.seikyuu_umu AS seikyuumu ")
            .AppendLine(ComSQL(dtInfo))
        End With

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dsReturn", ComParam(dtInfo).ToArray)

        Return dsReturn.Tables(0)

    End Function
    ''' <summary>工事価格マスタ個別設定の存在チェツク</summary>
    Public Function CheckSonzai(ByVal strAiteSakiSyubetu As String, ByVal strAiteSakiCd As String, ByVal strSyouhinCd As String, ByVal strKojKaisyaCd As String) As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT  ")
            .AppendLine("	aitesaki_syubetu ")
            .AppendLine("FROM  ")
            .AppendLine("	m_koj_kakaku WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	aitesaki_syubetu = @aitesaki_syubetu ")
            .AppendLine("	AND ")
            .AppendLine("	aitesaki_cd = @aitesaki_cd ")
            .AppendLine("	AND ")
            .AppendLine("	syouhin_cd = @syouhin_cd ")
            .AppendLine("	AND ")
            .AppendLine("	ISNULL(koj_gaisya_cd,'')+ISNULL(koj_gaisya_jigyousyo_cd,'') = @koj_cd ")
        End With

        paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 4, strAiteSakiSyubetu))
        paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, strAiteSakiCd))
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))
        paramList.Add(MakeParam("@koj_cd", SqlDbType.VarChar, 7, strKojKaisyaCd))

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dsReturn", paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function
   

End Class

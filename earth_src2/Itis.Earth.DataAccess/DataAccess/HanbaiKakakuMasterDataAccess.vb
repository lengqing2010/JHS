Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports System.Text

Public Class HanbaiKakakuMasterDataAccess
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>相手先種別を取得する</summary>
    ''' <returns>相手先種別データテーブル</returns>
    Public Function SelAiteSakiSyubetu() As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsAiteSakiSyubetu As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'SQL文
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   code ") 'コード
            .AppendLine("   ,code + '：' + ISNULL(meisyou,'') AS aitesaki_syubetu ")  '相手先種別
            .AppendLine(" FROM ")
            .AppendLine("   m_kakutyou_meisyou WITH(READCOMMITTED) ") '拡張名称マスタ
            .AppendLine(" WHERE ")
            .AppendLine("	meisyou_syubetu = 23 ") '名称種別
            .AppendLine(" ORDER BY ")
            .AppendLine("   code ")
        End With

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsAiteSakiSyubetu, "dtAiteSakiSyubetu")

        Return dsAiteSakiSyubetu.Tables("dtAiteSakiSyubetu")

    End Function
    ''' <summary>商品を取得する</summary>
    ''' <returns>商品データテーブル</returns>
    Public Function SelSyouhin() As Data.DataTable

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
            .AppendLine(" WHERE torikesi = 0 ")  '取消
            .AppendLine(" AND souko_cd = '100' ") '倉庫コード
            .AppendLine(" ORDER BY ")
            .AppendLine("   syouhin_cd ")
        End With

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsSyouhin, "dtSyouhin")

        Return dsSyouhin.Tables("dtSyouhin")

    End Function
    ''' <summary>調査方法を取得する</summary>
    ''' <returns>調査方法データテーブル</returns>
    Public Function SelTyousaHouhou() As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsTyousaHouhou As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'SQL文
        With commandTextSb
            .AppendLine("SELECT  ")
            .AppendLine("	tys_houhou_no ") '調査方法NO
            .AppendLine("	,CAST(tys_houhou_no AS VARCHAR) + '：' + ISNULL(tys_houhou_mei,'') AS tys_houhou ")  '調査方法
            .AppendLine("FROM ")
            .AppendLine("	m_tyousahouhou WITH(READCOMMITTED) ") '調査方法マスタ
            .AppendLine(" ORDER BY ")
            .AppendLine("   tys_houhou_no ")
        End With

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsTyousaHouhou, "dtTyousahouhou")

        Return dsTyousaHouhou.Tables("dtTyousahouhou")

    End Function
    '''<summary>相手先情報を取得する</summary>
    ''' <param name="strAitesakiSyubetu">相手先種別</param>
    ''' <param name="strTorikesiAitesaki">相手先取消区分</param>
    ''' <param name="strAitesakiCd">相手先コード</param>
    ''' <returns>相手先情報データテーブル</returns>
    Public Function SelAiteSaki(ByVal strAitesakiSyubetu As String, _
                                ByVal strTorikesiAitesaki As String, _
                                ByVal strAitesakiCd As String) As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsHanbaiKakaku As New HanbaiKakakuMasterDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" SELECT ")
        Select Case strAitesakiSyubetu
            Case "1"
                commandTextSb.AppendLine("      kameiten_cd AS cd, ")
                commandTextSb.AppendLine("      kameiten_mei1 AS mei ")
                commandTextSb.AppendLine(" FROM ")
                commandTextSb.AppendLine("      m_kameiten WITH(READCOMMITTED) ")
                commandTextSb.AppendLine(" WHERE  kameiten_cd = @kameiten_cd")
                'パラメータの設定
                paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strAitesakiCd))

            Case "5"
                commandTextSb.AppendLine("      eigyousyo_cd AS cd, ")
                commandTextSb.AppendLine("      eigyousyo_mei AS mei ")
                commandTextSb.AppendLine(" FROM ")
                commandTextSb.AppendLine("      m_eigyousyo WITH(READCOMMITTED) ")
                commandTextSb.AppendLine(" WHERE eigyousyo_cd = @eigyousyo_cd ")
                'パラメータの設定
                paramList.Add(MakeParam("@eigyousyo_cd", SqlDbType.VarChar, 5, strAitesakiCd))
            Case "7"
                commandTextSb.AppendLine("      keiretu_cd AS cd, ")
                commandTextSb.AppendLine("      keiretu_mei AS mei ")
                commandTextSb.AppendLine(" FROM ")
                commandTextSb.AppendLine("      m_keiretu WITH(READCOMMITTED) ")
                commandTextSb.AppendLine(" WHERE keiretu_cd = @keiretu_cd ")
                'パラメータの設定
                paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 5, strAitesakiCd))

        End Select

        If strTorikesiAitesaki <> String.Empty Then
            '取消パラメータの設定
            commandTextSb.AppendLine(" AND torikesi = @torikesi ")
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
        End If

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanbaiKakaku, _
                    "dtAiteSaki", paramList.ToArray)

        Return dsHanbaiKakaku.Tables("dtAiteSaki")

    End Function
    ''' <summary>販売価格データを取得する</summary>
    ''' <param name="dtHanbaiKakakuInfo">パラメータ</param>
    ''' <returns>販売価格データテーブル</returns>
    Public Function SelHanbaiKakakuInfo(ByVal dtHanbaiKakakuInfo As HanbaiKakakuMasterDataSet.Param_HanbaiKakakuInfoDataTable) _
           As HanbaiKakakuMasterDataSet.HanbaiKakakuInfoTableDataTable

        'DataSetインスタンスの生成
        Dim dsHanbaiKakaku As New HanbaiKakakuMasterDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine(" SELECT ")
            If dtHanbaiKakakuInfo(0).kensaku_count = "100" Then
                .AppendLine("      TOP 100 ")
            ElseIf dtHanbaiKakakuInfo(0).kensaku_count = "10" Then
                .AppendLine("      TOP 10 ")
            End If
            '販売価格M.相手先種別:拡張名称M.名称
            .AppendLine("   CAST(MHK.aitesaki_syubetu AS VARCHAR) + '：' + ISNULL(MKM.meisyou,'') AS aitesaki ")
            .AppendLine("   ,MHK.aitesaki_cd ")                             '販売価格M.相手先コード
            .AppendLine("   ,SUB.aitesaki_mei ")                            'サブ.相手先名
            .AppendLine("   ,MHK.syouhin_cd ")                              '販売価格M.商品コード
            .AppendLine("   ,MS.syouhin_mei ")                              '商品M.商品名
            '販売価格M.調査方法NO:調査方法M.調査方法名称
            .AppendLine("   ,CAST(MHK.tys_houhou_no AS VARCHAR) + '：'+ISNULL(MT.tys_houhou_mei,'') AS tys_houhou ")
            .AppendLine("   ,CASE MHK.torikesi ")
            .AppendLine("       WHEN 0 THEN '' ")
            .AppendLine("       ELSE '取消' ")
            .AppendLine("    END AS torikesi ")                             '販売価格M.取消
            .AppendLine("   ,MHK.koumuten_seikyuu_gaku ")                   '販売価格M.工務店請求金額
            .AppendLine("   ,CASE ISNULL(MHK.koumuten_seikyuu_gaku_henkou_flg,0) ")
            .AppendLine("       WHEN 0 THEN '変更不可' ")
            .AppendLine("       ELSE '変更可' ")
            .AppendLine("    END AS koumuten_seikyuu_gaku_henkou_flg ")     '販売価格M.工務店請求金額変更FLG
            .AppendLine("   ,MHK.jitu_seikyuu_gaku ")                       '販売価格M.実請求金額
            .AppendLine("   ,CASE ISNULL(MHK.jitu_seikyuu_gaku_henkou_flg,0) ")
            .AppendLine("       WHEN 0 THEN '変更不可' ")
            .AppendLine("       ELSE '変更可' ")
            .AppendLine("    END AS jitu_seikyuu_gaku_henkou_flg ")         '販売価格M.実請求金額変更FLG
            .AppendLine("   ,CASE ISNULL(MHK.koukai_flg,0) ")
            .AppendLine("       WHEN 0 THEN '非公開' ")
            .AppendLine("       ELSE '公開' ")
            .AppendLine("    END AS koukai_flg ")                           '販売価格M.公開フラグ
            .AppendLine("   ,MHK.tys_houhou_no ")                           '販売価格M.調査方法NO
            .AppendLine(" FROM ")
            .AppendLine("   m_hanbai_kakaku MHK WITH(READCOMMITTED) ")      '販売価格M
            .AppendLine(" LEFT OUTER JOIN ")
            .AppendLine("   m_kakutyou_meisyou MKM WITH(READCOMMITTED) ")   '拡張名称M
            .AppendLine(" ON    MHK.aitesaki_syubetu = MKM.code ")
            .AppendLine(" AND   MKM.meisyou_syubetu = 23 ")
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
            .AppendLine("   m_syouhin MS WITH(READCOMMITTED) ")   '商品マスタ
            .AppendLine(" ON    MHK.syouhin_cd = MS.syouhin_cd ")
            .AppendLine(" AND   MS.souko_cd = '100' ")
            .AppendLine(" AND   MS.torikesi = 0 ")
            .AppendLine(" LEFT OUTER JOIN ")
            .AppendLine("   m_tyousahouhou MT WITH(READCOMMITTED) ")   '調査方法マスタ
            .AppendLine(" ON    MHK.tys_houhou_no = MT.tys_houhou_no ")
            .AppendLine(" WHERE ")

            '相手先種別
            .AppendLine(" MHK.aitesaki_syubetu = @aitesaki_syubetu ")
            paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 4, dtHanbaiKakakuInfo(0).aitesaki_syubetu))

            '相手先コード
            If dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                .AppendLine(" AND MHK.aitesaki_cd BETWEEN @aitesaki_cd_from AND @aitesaki_cd_to ")
                paramList.Add(MakeParam("@aitesaki_cd_from", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_from))
                paramList.Add(MakeParam("@aitesaki_cd_to", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_to))
            ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to = String.Empty Then
                .AppendLine(" AND MHK.aitesaki_cd = @aitesaki_cd_from ")
                paramList.Add(MakeParam("@aitesaki_cd_from", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_from))
            ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from = String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                .AppendLine(" AND MHK.aitesaki_cd = @aitesaki_cd_to ")
                paramList.Add(MakeParam("@aitesaki_cd_to", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_to))
            End If

            '商品コード
            If dtHanbaiKakakuInfo(0).syouhin_cd <> String.Empty Then
                .AppendLine(" AND MHK.syouhin_cd = @syouhin_cd ")
                paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtHanbaiKakakuInfo(0).syouhin_cd))
            End If

            '調査方法
            If dtHanbaiKakakuInfo(0).tys_houhou_no <> String.Empty Then
                .AppendLine(" AND MHK.tys_houhou_no = @tys_houhou_no ")
                paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 4, dtHanbaiKakakuInfo(0).tys_houhou_no))
            End If

            '取消
            If dtHanbaiKakakuInfo(0).torikesi <> String.Empty Then
                .AppendLine(" AND MHK.torikesi = @torikesi ")
                paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, 0))
            End If

            '取消相手先
            If dtHanbaiKakakuInfo(0).torikesi_aitesaki <> String.Empty Then
                .AppendLine(" AND SUB.torikesi = @torikesi_aitesaki ")
                paramList.Add(MakeParam("@torikesi_aitesaki", SqlDbType.Int, 4, 0))
            End If

            '=======================2011/05/18 車龍 仕様変更 追加 開始↓==========================
            '\0は対象外
            If dtHanbaiKakakuInfo(0).kingaku_0_taisyou_gai <> String.Empty Then
                .AppendLine(" AND MHK.jitu_seikyuu_gaku <> 0 ")
            End If
            '=======================2011/05/18 車龍 仕様変更 追加 終了↑==========================

            'ソート順
            .AppendLine(" ORDER BY ")
            .AppendLine("      MHK.aitesaki_syubetu ")
            .AppendLine("      ,MHK.aitesaki_cd ")
            .AppendLine("      ,MHK.syouhin_cd ")
            .AppendLine("      ,MHK.tys_houhou_no ")
        End With

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanbaiKakaku, _
                    dsHanbaiKakaku.HanbaiKakakuInfoTable.TableName, paramList.ToArray)

        Return dsHanbaiKakaku.HanbaiKakakuInfoTable

    End Function
    ''' <summary>販売価格「系列・営業所・指定なしチェックボックス」=チェックの場合のデータを取得する</summary>
    ''' <param name="dtHanbaiKakakuInfo">パラメータ</param>
    ''' <returns>販売価格データテーブル</returns>
    Public Function SelHanbaiKakakuSeiteNasiInfo(ByVal dtHanbaiKakakuInfo As HanbaiKakakuMasterDataSet.Param_HanbaiKakakuInfoDataTable) _
           As HanbaiKakakuMasterDataSet.HanbaiKakakuInfoTableDataTable

        'DataSetインスタンスの生成
        Dim dsHanbaiKakaku As New HanbaiKakakuMasterDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine(" SELECT ")
            If dtHanbaiKakakuInfo(0).kensaku_count = "100" Then
                .AppendLine("      TOP 100 ")
            ElseIf dtHanbaiKakakuInfo(0).kensaku_count = "10" Then
                .AppendLine("      TOP 10 ")
            End If
            'サブ販売価格M.相手先種別:拡張名称M.名称
            .AppendLine("	CAST(SUBMHK.aitesaki_syubetu AS VARCHAR) + '：' + ISNULL(MKM.meisyou,'') AS aitesaki ")
            .AppendLine("	,SUBMHK.aitesaki_cd ")  'サブ販売価格M.相手先コード
            .AppendLine("	,SUB.aitesaki_mei ")    'サブ.相手先名
            .AppendLine("	,SUBMHK.syouhin_cd ")   'サブ販売価格M.商品コード
            .AppendLine("	,MS.syouhin_mei ")      '商品M.商品名
            'サブ販売価格M.調査方法（調査方法No：調査方法名）
            .AppendLine("	,CAST(SUBMHK.tys_houhou_no AS VARCHAR) + '：' + ISNULL(MT.tys_houhou_mei,'') AS tys_houhou ")
            .AppendLine("	,CASE SUBMHK.torikesi  ")   '取消（0:""　0以外:取消）
            .AppendLine("		WHEN 0 THEN '' ")
            .AppendLine("		ELSE '取消' ")
            .AppendLine("	 END AS torikesi ")
            .AppendLine("	,SUBMHK.koumuten_seikyuu_gaku ")    'サブ販売価格M.工務店請求額
            .AppendLine("	,CASE ISNULL(SUBMHK.koumuten_seikyuu_gaku_henkou_flg,0) ")
            .AppendLine("		WHEN 0 THEN '変更不可' ")
            .AppendLine("		ELSE '変更可' ")
            .AppendLine("	 END AS koumuten_seikyuu_gaku_henkou_flg ")     '工務店請求額変更フラグ（0:変更不可,0以外:変更可）
            .AppendLine("	,SUBMHK.jitu_seikyuu_gaku ")        'サブ販売価格M.実請求額
            .AppendLine("	,CASE ISNULL(SUBMHK.jitu_seikyuu_gaku_henkou_flg,0) ")
            .AppendLine("		WHEN 0 THEN '変更不可' ")
            .AppendLine("		ELSE '変更可' ")
            .AppendLine("	 END AS jitu_seikyuu_gaku_henkou_flg ")     'サブ販売価格M.実請求額フラグ（0:変更不可,0以外：変更可）
            .AppendLine("	,CASE ISNULL(SUBMHK.koukai_flg,0) ")
            .AppendLine("		WHEN 0 THEN '非公開' ")
            .AppendLine("		ELSE '公開' ")
            .AppendLine("	 END AS koukai_flg ")       'サブ販売価格M.公開フラグ（0：非公開,0以外:公開）
            .AppendLine("	,SUBMHK.tys_houhou_no ")
            .AppendLine("FROM  ")
            .AppendLine("	( ")
            .AppendLine("		SELECT MHK.*  ")
            .AppendLine("		FROM m_hanbai_kakaku AS MHK WITH(READCOMMITTED)  ")     '販売価格マスタ
            .AppendLine("		WHERE  ")
            .AppendLine("			MHK.aitesaki_syubetu = 1  ")
            .AppendLine("			AND MHK.aitesaki_cd = @aitesaki_cd ")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT MHKM.*  ")
            .AppendLine("		FROM m_hanbai_kakaku AS MHKM WITH(READCOMMITTED) ")     '販売価格マスタ
            .AppendLine("		INNER JOIN m_kameiten AS MKM WITH(READCOMMITTED)  ")    '加盟店マスタ
            .AppendLine("		ON MHKM.aitesaki_syubetu = 5  ")
            .AppendLine("			AND MHKM.aitesaki_cd = MKM.eigyousyo_cd ")
            .AppendLine("		LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT MHK.*  ")
            .AppendLine("				FROM m_hanbai_kakaku MHK WITH(READCOMMITTED) ")     '販売価格マスタ
            .AppendLine("				WHERE  ")
            .AppendLine("					MHK.aitesaki_syubetu = 1  ")
            .AppendLine("					AND  MHK.aitesaki_cd = @aitesaki_cd ")
            '============================2011/04/26 車龍 追加 開始↓=====================================
            .AppendLine("					AND  ISNULL(MHK.torikesi,0) = 0 ")          'ISNULL(取消,0)=0
            '============================2011/04/26 車龍 追加 終了↑=====================================
            .AppendLine("			) AS SUBMHK1 ")
            .AppendLine("		ON MHKM.syouhin_cd = SUBMHK1.syouhin_cd  ")
            .AppendLine("			AND MHKM.tys_houhou_no = SUBMHK1.tys_houhou_no ")
            .AppendLine("		WHERE  ")
            .AppendLine("			MKM.kameiten_cd = @aitesaki_cd  ")
            .AppendLine("			AND SUBMHK1.aitesaki_cd IS NULL ")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT MHKM.*  ")
            .AppendLine("		FROM m_hanbai_kakaku AS MHKM WITH(READCOMMITTED) ")     '販売価格マスタ
            .AppendLine("		INNER JOIN m_kameiten AS MKM WITH(READCOMMITTED) ")     '加盟店マスタ
            .AppendLine("		ON MHKM.aitesaki_syubetu = 7  ")
            .AppendLine("			AND MHKM.aitesaki_cd = MKM.keiretu_cd ")
            .AppendLine("		LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT MHK.*  ")
            .AppendLine("				FROM m_hanbai_kakaku MHK WITH(READCOMMITTED) ")     '販売価格マスタ
            .AppendLine("				WHERE  ")
            .AppendLine("					MHK.aitesaki_syubetu = 1  ")
            .AppendLine("					AND  MHK.aitesaki_cd = @aitesaki_cd ")
            '============================2011/04/26 車龍 追加 開始↓=====================================
            .AppendLine("					AND  ISNULL(MHK.torikesi,0) = 0 ")          'ISNULL(取消,0)=0
            '============================2011/04/26 車龍 追加 終了↑=====================================
            .AppendLine("			) AS SUBMHK1 ")
            .AppendLine("		ON MHKM.syouhin_cd = SUBMHK1.syouhin_cd  ")
            .AppendLine("			AND MHKM.tys_houhou_no = SUBMHK1.tys_houhou_no ")
            .AppendLine("		LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT MHKM.*  ")
            .AppendLine("				FROM m_hanbai_kakaku AS MHKM WITH(READCOMMITTED)  ")    '販売価格マスタ
            .AppendLine("				INNER JOIN m_kameiten AS MKM WITH(READCOMMITTED) ")     '加盟店マスタ
            .AppendLine("				ON MHKM.aitesaki_syubetu = 5  ")
            .AppendLine("					AND MHKM.aitesaki_cd = MKM.eigyousyo_cd ")
            .AppendLine("					AND MKM.kameiten_cd = @aitesaki_cd ")
            '============================2011/04/26 車龍 追加 開始↓=====================================
            .AppendLine("					AND  ISNULL(MHKM.torikesi,0) = 0 ")          'ISNULL(取消,0)=0
            '============================2011/04/26 車龍 追加 終了↑=====================================
            .AppendLine("			) AS SUBMHK5 ")
            .AppendLine("		ON MHKM.syouhin_cd = SUBMHK5.syouhin_cd  ")
            .AppendLine("			AND MHKM.tys_houhou_no = SUBMHK5.tys_houhou_no ")
            .AppendLine("		WHERE  ")
            .AppendLine("			MKM.kameiten_cd = @aitesaki_cd  ")
            .AppendLine("			AND SUBMHK1.aitesaki_cd IS NULL ")
            .AppendLine("			AND SUBMHK5.aitesaki_cd IS NULL ")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT MHKM.*  ")
            .AppendLine("		FROM m_hanbai_kakaku AS MHKM WITH(READCOMMITTED) ")     '販売価格マスタ
            .AppendLine("		LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT MHK.*  ")
            .AppendLine("				FROM m_hanbai_kakaku AS MHK WITH(READCOMMITTED) ")      '販売価格マスタ
            .AppendLine("				WHERE  ")
            .AppendLine("					MHK.aitesaki_syubetu = 1  ")
            .AppendLine("					AND MHK.aitesaki_cd = @aitesaki_cd ")
            '============================2011/04/26 車龍 追加 開始↓=====================================
            .AppendLine("					AND  ISNULL(MHK.torikesi,0) = 0 ")          'ISNULL(取消,0)=0
            '============================2011/04/26 車龍 追加 終了↑=====================================
            .AppendLine("			) AS SUBMHK1 ")
            .AppendLine("		ON MHKM.syouhin_cd = SUBMHK1.syouhin_cd  ")
            .AppendLine("			AND MHKM.tys_houhou_no = SUBMHK1.tys_houhou_no ")
            .AppendLine("		LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT MHKM.*  ")
            .AppendLine("				FROM m_hanbai_kakaku AS MHKM WITH(READCOMMITTED)  ")        '販売価格マスタ
            .AppendLine("				INNER JOIN m_kameiten AS MKM WITH(READCOMMITTED) ")     '加盟店マスタ
            .AppendLine("				ON MHKM.aitesaki_syubetu = 5  ")
            .AppendLine("					AND MHKM.aitesaki_cd = MKM.eigyousyo_cd ")
            .AppendLine("					AND MKM.kameiten_cd = @aitesaki_cd ")
            '============================2011/04/26 車龍 追加 開始↓=====================================
            .AppendLine("					AND  ISNULL(MHKM.torikesi,0) = 0 ")          'ISNULL(取消,0)=0
            '============================2011/04/26 車龍 追加 終了↑=====================================
            .AppendLine("			) AS SUBMHK5 ")
            .AppendLine("		ON MHKM.syouhin_cd = SUBMHK5.syouhin_cd  ")
            .AppendLine("			AND MHKM.tys_houhou_no = SUBMHK5.tys_houhou_no ")
            .AppendLine("		LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT MHKM.*  ")
            .AppendLine("				FROM m_hanbai_kakaku AS MHKM WITH(READCOMMITTED) ")     '販売価格マスタ
            .AppendLine("				INNER JOIN m_kameiten AS MKM WITH(READCOMMITTED) ")     '加盟店マスタ
            .AppendLine("				ON MHKM.aitesaki_syubetu = 7  ")
            .AppendLine("					AND MHKM.aitesaki_cd = MKM.keiretu_cd ")
            .AppendLine("					AND MKM.kameiten_cd = @aitesaki_cd ")
            '============================2011/04/26 車龍 追加 開始↓=====================================
            .AppendLine("					AND  ISNULL(MHKM.torikesi,0) = 0 ")          'ISNULL(取消,0)=0
            '============================2011/04/26 車龍 追加 終了↑=====================================
            .AppendLine("			) AS SUBMHK7 ")
            .AppendLine("		ON MHKM.syouhin_cd = SUBMHK7.syouhin_cd  ")
            .AppendLine("			AND MHKM.tys_houhou_no = SUBMHK7.tys_houhou_no ")
            .AppendLine("		WHERE  ")
            .AppendLine("			MHKM.aitesaki_syubetu = 0  ")
            .AppendLine("			AND SUBMHK1.aitesaki_cd IS NULL ")
            .AppendLine("			AND SUBMHK5.aitesaki_cd IS NULL  ")
            .AppendLine("			AND SUBMHK7.aitesaki_cd IS NULL ")
            .AppendLine("	) AS SUBMHK ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("		( ")
            .AppendLine("			SELECT  ")
            .AppendLine("				0 AS aitesaki_syubetu,  ")
            .AppendLine("				'ALL' AS aitesaki_cd,  ")
            .AppendLine("				'相手先なし' AS aitesaki_mei,  ")
            .AppendLine("				0 AS torikesi ")
            .AppendLine("			UNION ALL ")
            .AppendLine("			SELECT  ")
            .AppendLine("				1 AS aitesaki_syubetu,  ")
            .AppendLine("				MK.kameiten_cd AS aitesaki_cd,  ")
            .AppendLine("				MK.kameiten_mei1 AS aitesaki_mei,  ")
            .AppendLine("				MK.torikesi AS torikesi ")
            .AppendLine("			FROM m_kameiten AS MK WITH(READCOMMITTED) ")        '加盟店マスタ
            .AppendLine("			UNION ALL ")
            .AppendLine("			SELECT  ")
            .AppendLine("				5 AS aitesaki_syubetu,  ")
            .AppendLine("				ME.eigyousyo_cd AS aitesaki_cd,  ")
            .AppendLine("				ME.eigyousyo_mei AS aitesaki_mei,  ")
            .AppendLine("				ME.torikesi AS torikesi ")
            .AppendLine("			FROM m_eigyousyo AS ME WITH(READCOMMITTED) ")       '営業所マスタ
            .AppendLine("			UNION ALL  ")
            .AppendLine("			SELECT  ")
            .AppendLine("				7 AS aitesaki_syubetu,  ")
            .AppendLine("				MK.keiretu_cd AS aitesaki_cd,  ")
            .AppendLine("				MIN(MK.keiretu_mei) AS aitesaki_mei,  ")
            .AppendLine("				MIN(MK.torikesi) AS torikesi ")
            .AppendLine("			FROM m_keiretu AS MK WITH(READCOMMITTED)  ")        '系列マスタ
            .AppendLine("			GROUP BY  ")
            .AppendLine("				MK.keiretu_cd ")
            .AppendLine("		) AS SUB ")
            .AppendLine("		ON SUBMHK.aitesaki_syubetu = SUB.aitesaki_syubetu  ")
            .AppendLine("			AND SUBMHK.aitesaki_cd = SUB.aitesaki_cd ")
            .AppendLine("	LEFT JOIN m_syouhin AS MS WITH(READCOMMITTED) ")        '商品マスタ
            .AppendLine("		ON SUBMHK.syouhin_cd = MS.syouhin_cd  ")
            .AppendLine("			AND MS.souko_cd = '100'  ")
            .AppendLine("			AND MS.torikesi = 0 ")
            .AppendLine("	LEFT JOIN m_tyousahouhou AS MT WITH(READCOMMITTED) ")       '調査方法マスタ
            .AppendLine("		ON SUBMHK.tys_houhou_no = MT.tys_houhou_no ")
            .AppendLine("	LEFT JOIN 	m_kakutyou_meisyou AS MKM WITH(READCOMMITTED) ")        '拡張名称マスタ
            .AppendLine("		ON SUBMHK.aitesaki_syubetu = MKM.code  ")
            .AppendLine("			AND MKM.meisyou_syubetu = '23' ")
            .AppendLine("WHERE(1=1)  ")
            '商品コード
            If dtHanbaiKakakuInfo(0).syouhin_cd <> String.Empty Then
                .AppendLine("AND SUBMHK.syouhin_cd = @syouhin_cd  ")
                paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtHanbaiKakakuInfo(0).syouhin_cd))
            End If

            '調査方法
            If dtHanbaiKakakuInfo(0).tys_houhou_no <> String.Empty Then
                .AppendLine("AND SUBMHK.tys_houhou_no = @tys_houhou_no ")
                paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 4, dtHanbaiKakakuInfo(0).tys_houhou_no))
            End If

            '取消
            '============================2011/04/26 車龍 削除 開始↓=====================================
            'If dtHanbaiKakakuInfo(0).torikesi <> String.Empty Then
            '============================2011/04/26 車龍 削除 終了↑=====================================
            .AppendLine("AND SUBMHK.torikesi = @torikesi ")
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, 0))
            '============================2011/04/26 車龍 削除 開始↓=====================================
            'End If
            '============================2011/04/26 車龍 削除 終了↑=====================================

            '取消相手先
            If dtHanbaiKakakuInfo(0).torikesi_aitesaki <> String.Empty Then
                .AppendLine("AND SUB.torikesi = @torikesi_aitesaki ")
                paramList.Add(MakeParam("@torikesi_aitesaki", SqlDbType.Int, 4, 0))
            End If

            '=======================2011/05/18 車龍 仕様変更 追加 開始↓==========================
            '\0は対象外
            If dtHanbaiKakakuInfo(0).kingaku_0_taisyou_gai <> String.Empty Then
                .AppendLine(" AND SUBMHK.jitu_seikyuu_gaku <> 0 ")
            End If
            '=======================2011/05/18 車龍 仕様変更 追加 終了↑==========================

            .AppendLine("ORDER BY  ")
            .AppendLine("SUBMHK.syouhin_cd  ")
            .AppendLine(",SUBMHK.tys_houhou_no ")
        End With

        paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_from))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanbaiKakaku, _
                    dsHanbaiKakaku.HanbaiKakakuInfoTable.TableName, paramList.ToArray)

        Return dsHanbaiKakaku.HanbaiKakakuInfoTable

    End Function
    ''' <summary>販売価格データ件数を取得する</summary>
    ''' <param name="dtHanbaiKakakuInfo">パラメータ</param>
    ''' <returns>販売価格データ件数</returns>
    Public Function SelHanbaiKakakuInfoCount(ByVal dtHanbaiKakakuInfo As HanbaiKakakuMasterDataSet.Param_HanbaiKakakuInfoDataTable) _
           As Integer

        'DataSetインスタンスの生成
        Dim dsHanbaiKakaku As New HanbaiKakakuMasterDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   COUNT(*) AS count ")
            .AppendLine(" FROM ")
            .AppendLine("   m_hanbai_kakaku MHK WITH(READCOMMITTED) ")     '販売価格M
            .AppendLine(" LEFT OUTER JOIN ")
            .AppendLine("   m_kakutyou_meisyou MKM WITH(READCOMMITTED) ")  '拡張名称M
            .AppendLine(" ON    MHK.aitesaki_syubetu = MKM.code ")
            .AppendLine(" AND   MKM.meisyou_syubetu = 23 ")
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
            .AppendLine(" AND   MS.souko_cd = '100' ")
            .AppendLine(" AND   MS.torikesi = 0 ")
            .AppendLine(" LEFT OUTER JOIN ")
            .AppendLine("   m_tyousahouhou MT WITH(READCOMMITTED) ")   '調査方法マスタ
            .AppendLine(" ON    MHK.tys_houhou_no = MT.tys_houhou_no ")
            .AppendLine(" WHERE ")

            '相手先種別
            .AppendLine(" MHK.aitesaki_syubetu = @aitesaki_syubetu ")
            paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 4, dtHanbaiKakakuInfo(0).aitesaki_syubetu))

            '相手先コード
            If dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                .AppendLine(" AND MHK.aitesaki_cd BETWEEN @aitesaki_cd_from AND @aitesaki_cd_to ")
                paramList.Add(MakeParam("@aitesaki_cd_from", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_from))
                paramList.Add(MakeParam("@aitesaki_cd_to", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_to))
            ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to = String.Empty Then
                .AppendLine(" AND MHK.aitesaki_cd = @aitesaki_cd_from ")
                paramList.Add(MakeParam("@aitesaki_cd_from", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_from))
            ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from = String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                .AppendLine(" AND MHK.aitesaki_cd = @aitesaki_cd_to ")
                paramList.Add(MakeParam("@aitesaki_cd_to", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_to))
            End If

            '商品コード
            If dtHanbaiKakakuInfo(0).syouhin_cd <> String.Empty Then
                .AppendLine(" AND MHK.syouhin_cd = @syouhin_cd ")
                paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtHanbaiKakakuInfo(0).syouhin_cd))
            End If

            '調査方法
            If dtHanbaiKakakuInfo(0).tys_houhou_no <> String.Empty Then
                .AppendLine(" AND MHK.tys_houhou_no = @tys_houhou_no ")
                paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 4, dtHanbaiKakakuInfo(0).tys_houhou_no))
            End If

            '取消
            If dtHanbaiKakakuInfo(0).torikesi <> String.Empty Then
                .AppendLine(" AND MHK.torikesi = @torikesi ")
                paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, 0))
            End If

            '取消相手先
            If dtHanbaiKakakuInfo(0).torikesi_aitesaki <> String.Empty Then
                .AppendLine(" AND SUB.torikesi = @torikesi_aitesaki ")
                paramList.Add(MakeParam("@torikesi_aitesaki", SqlDbType.Int, 4, 0))
            End If

            '=======================2011/05/18 車龍 仕様変更 追加 開始↓==========================
            '\0は対象外
            If dtHanbaiKakakuInfo(0).kingaku_0_taisyou_gai <> String.Empty Then
                .AppendLine(" AND MHK.jitu_seikyuu_gaku <> 0 ")
            End If
            '=======================2011/05/18 車龍 仕様変更 追加 終了↑==========================

        End With

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanbaiKakaku, _
                    "dtHanbaiKakakuCount", paramList.ToArray)

        Return dsHanbaiKakaku.Tables("dtHanbaiKakakuCount").Rows(0).Item("count")

    End Function
    ''' <summary>販売価格「系列・営業所・指定なしチェックボックス」=チェックの場合のデータの件数を取得する</summary>
    ''' <param name="dtHanbaiKakakuInfo">パラメータ</param>
    ''' <returns>販売価格データ件数</returns>
    Public Function SelHanbaiKakakuSeiteNasiinfoCount(ByVal dtHanbaiKakakuInfo As HanbaiKakakuMasterDataSet.Param_HanbaiKakakuInfoDataTable) _
           As Integer

        'DataSetインスタンスの生成
        Dim dsHanbaiKakaku As New HanbaiKakakuMasterDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   COUNT(*) AS count ")
            .AppendLine("FROM  ")
            .AppendLine("	( ")
            .AppendLine("		SELECT MHK.*  ")
            .AppendLine("		FROM m_hanbai_kakaku AS MHK WITH(READCOMMITTED)  ")     '販売価格マスタ
            .AppendLine("		WHERE  ")
            .AppendLine("			MHK.aitesaki_syubetu = 1  ")
            .AppendLine("			AND MHK.aitesaki_cd = @aitesaki_cd ")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT MHKM.*  ")
            .AppendLine("		FROM m_hanbai_kakaku AS MHKM WITH(READCOMMITTED) ")     '販売価格マスタ
            .AppendLine("		INNER JOIN m_kameiten AS MKM WITH(READCOMMITTED)  ")    '加盟店マスタ
            .AppendLine("		ON MHKM.aitesaki_syubetu = 5  ")
            .AppendLine("			AND MHKM.aitesaki_cd = MKM.eigyousyo_cd ")
            .AppendLine("		LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT MHK.*  ")
            .AppendLine("				FROM m_hanbai_kakaku MHK WITH(READCOMMITTED) ")     '販売価格マスタ
            .AppendLine("				WHERE  ")
            .AppendLine("					MHK.aitesaki_syubetu = 1  ")
            .AppendLine("					AND  MHK.aitesaki_cd = @aitesaki_cd ")
            '============================2011/04/26 車龍 追加 開始↓=====================================
            .AppendLine("					AND  ISNULL(MHK.torikesi,0) = 0 ")          'ISNULL(取消,0)=0
            '============================2011/04/26 車龍 追加 終了↑=====================================
            .AppendLine("			) AS SUBMHK1 ")
            .AppendLine("		ON MHKM.syouhin_cd = SUBMHK1.syouhin_cd  ")
            .AppendLine("			AND MHKM.tys_houhou_no = SUBMHK1.tys_houhou_no ")
            .AppendLine("		WHERE  ")
            .AppendLine("			MKM.kameiten_cd = @aitesaki_cd  ")
            .AppendLine("			AND SUBMHK1.aitesaki_cd IS NULL ")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT MHKM.*  ")
            .AppendLine("		FROM m_hanbai_kakaku AS MHKM WITH(READCOMMITTED) ")     '販売価格マスタ
            .AppendLine("		INNER JOIN m_kameiten AS MKM WITH(READCOMMITTED) ")     '加盟店マスタ
            .AppendLine("		ON MHKM.aitesaki_syubetu = 7  ")
            .AppendLine("			AND MHKM.aitesaki_cd = MKM.keiretu_cd ")
            .AppendLine("		LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT MHK.*  ")
            .AppendLine("				FROM m_hanbai_kakaku MHK WITH(READCOMMITTED) ")     '販売価格マスタ
            .AppendLine("				WHERE  ")
            .AppendLine("					MHK.aitesaki_syubetu = 1  ")
            .AppendLine("					AND  MHK.aitesaki_cd = @aitesaki_cd ")
            '============================2011/04/26 車龍 追加 開始↓=====================================
            .AppendLine("					AND  ISNULL(MHK.torikesi,0) = 0 ")          'ISNULL(取消,0)=0
            '============================2011/04/26 車龍 追加 終了↑=====================================
            .AppendLine("			) AS SUBMHK1 ")
            .AppendLine("		ON MHKM.syouhin_cd = SUBMHK1.syouhin_cd  ")
            .AppendLine("			AND MHKM.tys_houhou_no = SUBMHK1.tys_houhou_no ")
            .AppendLine("		LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT MHKM.*  ")
            .AppendLine("				FROM m_hanbai_kakaku AS MHKM WITH(READCOMMITTED)  ")    '販売価格マスタ
            .AppendLine("				INNER JOIN m_kameiten AS MKM WITH(READCOMMITTED) ")     '加盟店マスタ
            .AppendLine("				ON MHKM.aitesaki_syubetu = 5  ")
            .AppendLine("					AND MHKM.aitesaki_cd = MKM.eigyousyo_cd ")
            .AppendLine("					AND MKM.kameiten_cd = @aitesaki_cd ")
            '============================2011/04/26 車龍 追加 開始↓=====================================
            .AppendLine("					AND  ISNULL(MHKM.torikesi,0) = 0 ")          'ISNULL(取消,0)=0
            '============================2011/04/26 車龍 追加 終了↑=====================================
            .AppendLine("			) AS SUBMHK5 ")
            .AppendLine("		ON MHKM.syouhin_cd = SUBMHK5.syouhin_cd  ")
            .AppendLine("			AND MHKM.tys_houhou_no = SUBMHK5.tys_houhou_no ")
            .AppendLine("		WHERE  ")
            .AppendLine("			MKM.kameiten_cd = @aitesaki_cd  ")
            .AppendLine("			AND SUBMHK1.aitesaki_cd IS NULL ")
            .AppendLine("			AND SUBMHK5.aitesaki_cd IS NULL ")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT MHKM.*  ")
            .AppendLine("		FROM m_hanbai_kakaku AS MHKM WITH(READCOMMITTED) ")     '販売価格マスタ
            .AppendLine("		LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT MHK.*  ")
            .AppendLine("				FROM m_hanbai_kakaku AS MHK WITH(READCOMMITTED) ")      '販売価格マスタ
            .AppendLine("				WHERE  ")
            .AppendLine("					MHK.aitesaki_syubetu = 1  ")
            .AppendLine("					AND MHK.aitesaki_cd = @aitesaki_cd ")
            '============================2011/04/26 車龍 追加 開始↓=====================================
            .AppendLine("					AND  ISNULL(MHK.torikesi,0) = 0 ")          'ISNULL(取消,0)=0
            '============================2011/04/26 車龍 追加 終了↑=====================================
            .AppendLine("			) AS SUBMHK1 ")
            .AppendLine("		ON MHKM.syouhin_cd = SUBMHK1.syouhin_cd  ")
            .AppendLine("			AND MHKM.tys_houhou_no = SUBMHK1.tys_houhou_no ")
            .AppendLine("		LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT MHKM.*  ")
            .AppendLine("				FROM m_hanbai_kakaku AS MHKM WITH(READCOMMITTED)  ")        '販売価格マスタ
            .AppendLine("				INNER JOIN m_kameiten AS MKM WITH(READCOMMITTED) ")     '加盟店マスタ
            .AppendLine("				ON MHKM.aitesaki_syubetu = 5  ")
            .AppendLine("					AND MHKM.aitesaki_cd = MKM.eigyousyo_cd ")
            .AppendLine("					AND MKM.kameiten_cd = @aitesaki_cd ")
            '============================2011/04/26 車龍 追加 開始↓=====================================
            .AppendLine("					AND  ISNULL(MHKM.torikesi,0) = 0 ")          'ISNULL(取消,0)=0
            '============================2011/04/26 車龍 追加 終了↑=====================================
            .AppendLine("			) AS SUBMHK5 ")
            .AppendLine("		ON MHKM.syouhin_cd = SUBMHK5.syouhin_cd  ")
            .AppendLine("			AND MHKM.tys_houhou_no = SUBMHK5.tys_houhou_no ")
            .AppendLine("		LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT MHKM.*  ")
            .AppendLine("				FROM m_hanbai_kakaku AS MHKM WITH(READCOMMITTED) ")     '販売価格マスタ
            .AppendLine("				INNER JOIN m_kameiten AS MKM WITH(READCOMMITTED) ")     '加盟店マスタ
            .AppendLine("				ON MHKM.aitesaki_syubetu = 7  ")
            .AppendLine("					AND MHKM.aitesaki_cd = MKM.keiretu_cd ")
            .AppendLine("					AND MKM.kameiten_cd = @aitesaki_cd ")
            '============================2011/04/26 車龍 追加 開始↓=====================================
            .AppendLine("					AND  ISNULL(MHKM.torikesi,0) = 0 ")          'ISNULL(取消,0)=0
            '============================2011/04/26 車龍 追加 終了↑=====================================
            .AppendLine("			) AS SUBMHK7 ")
            .AppendLine("		ON MHKM.syouhin_cd = SUBMHK7.syouhin_cd  ")
            .AppendLine("			AND MHKM.tys_houhou_no = SUBMHK7.tys_houhou_no ")
            .AppendLine("		WHERE  ")
            .AppendLine("			MHKM.aitesaki_syubetu = 0  ")
            .AppendLine("			AND SUBMHK1.aitesaki_cd IS NULL ")
            .AppendLine("			AND SUBMHK5.aitesaki_cd IS NULL  ")
            .AppendLine("			AND SUBMHK7.aitesaki_cd IS NULL ")
            .AppendLine("	) AS SUBMHK ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("		( ")
            .AppendLine("			SELECT  ")
            .AppendLine("				0 AS aitesaki_syubetu,  ")
            .AppendLine("				'ALL' AS aitesaki_cd,  ")
            .AppendLine("				'相手先なし' AS aitesaki_mei,  ")
            .AppendLine("				0 AS torikesi ")
            .AppendLine("			UNION ALL ")
            .AppendLine("			SELECT  ")
            .AppendLine("				1 AS aitesaki_syubetu,  ")
            .AppendLine("				MK.kameiten_cd AS aitesaki_cd,  ")
            .AppendLine("				MK.kameiten_mei1 AS aitesaki_mei,  ")
            .AppendLine("				MK.torikesi AS torikesi ")
            .AppendLine("			FROM m_kameiten AS MK WITH(READCOMMITTED) ")        '加盟店マスタ
            .AppendLine("			UNION ALL ")
            .AppendLine("			SELECT  ")
            .AppendLine("				5 AS aitesaki_syubetu,  ")
            .AppendLine("				ME.eigyousyo_cd AS aitesaki_cd,  ")
            .AppendLine("				ME.eigyousyo_mei AS aitesaki_mei,  ")
            .AppendLine("				ME.torikesi AS torikesi ")
            .AppendLine("			FROM m_eigyousyo AS ME WITH(READCOMMITTED) ")       '営業所マスタ
            .AppendLine("			UNION ALL  ")
            .AppendLine("			SELECT  ")
            .AppendLine("				7 AS aitesaki_syubetu,  ")
            .AppendLine("				MK.keiretu_cd AS aitesaki_cd,  ")
            .AppendLine("				MIN(MK.keiretu_mei) AS aitesaki_mei,  ")
            .AppendLine("				MIN(MK.torikesi) AS torikesi ")
            .AppendLine("			FROM m_keiretu AS MK WITH(READCOMMITTED)  ")        '系列マスタ
            .AppendLine("			GROUP BY  ")
            .AppendLine("				MK.keiretu_cd ")
            .AppendLine("		) AS SUB ")
            .AppendLine("		ON SUBMHK.aitesaki_syubetu = SUB.aitesaki_syubetu  ")
            .AppendLine("			AND SUBMHK.aitesaki_cd = SUB.aitesaki_cd ")
            .AppendLine("	LEFT JOIN m_syouhin AS MS WITH(READCOMMITTED) ")        '商品マスタ
            .AppendLine("		ON SUBMHK.syouhin_cd = MS.syouhin_cd  ")
            .AppendLine("			AND MS.souko_cd = '100'  ")
            .AppendLine("			AND MS.torikesi = 0 ")
            .AppendLine("	LEFT JOIN m_tyousahouhou AS MT WITH(READCOMMITTED) ")       '調査方法マスタ
            .AppendLine("		ON SUBMHK.tys_houhou_no = MT.tys_houhou_no ")
            .AppendLine("	LEFT JOIN 	m_kakutyou_meisyou AS MKM WITH(READCOMMITTED) ")        '拡張名称マスタ
            .AppendLine("		ON SUBMHK.aitesaki_syubetu = MKM.code  ")
            .AppendLine("			AND MKM.meisyou_syubetu = '23' ")
            .AppendLine("WHERE(1=1)  ")
            '商品コード
            If dtHanbaiKakakuInfo(0).syouhin_cd <> String.Empty Then
                .AppendLine("AND SUBMHK.syouhin_cd = @syouhin_cd  ")
                paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtHanbaiKakakuInfo(0).syouhin_cd))
            End If

            '調査方法
            If dtHanbaiKakakuInfo(0).tys_houhou_no <> String.Empty Then
                .AppendLine("AND SUBMHK.tys_houhou_no = @tys_houhou_no ")
                paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 4, dtHanbaiKakakuInfo(0).tys_houhou_no))
            End If

            '取消
            '============================2011/04/26 車龍 削除 開始↓=====================================
            'If dtHanbaiKakakuInfo(0).torikesi <> String.Empty Then
            '============================2011/04/26 車龍 削除 終了↑=====================================
            .AppendLine("AND SUBMHK.torikesi = @torikesi ")
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, 0))
            '============================2011/04/26 車龍 削除 開始↓=====================================
            'End If
            '============================2011/04/26 車龍 削除 終了↑=====================================

            '取消相手先
            If dtHanbaiKakakuInfo(0).torikesi_aitesaki <> String.Empty Then
                .AppendLine("AND SUB.torikesi = @torikesi_aitesaki ")
                paramList.Add(MakeParam("@torikesi_aitesaki", SqlDbType.Int, 4, 0))
            End If

            '=======================2011/05/18 車龍 仕様変更 追加 開始↓==========================
            '\0は対象外
            If dtHanbaiKakakuInfo(0).kingaku_0_taisyou_gai <> String.Empty Then
                .AppendLine(" AND SUBMHK.jitu_seikyuu_gaku <> 0 ")
            End If
            '=======================2011/05/18 車龍 仕様変更 追加 終了↑==========================

        End With

        paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_from))

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanbaiKakaku, _
                    "dtHanbaiKakakuCount", paramList.ToArray)

        Return dsHanbaiKakaku.Tables("dtHanbaiKakakuCount").Rows(0).Item("count")

    End Function
    ''' <summary>未設定も含む販売価格CSVデータ件数を取得する</summary>
    ''' <param name="dtHanbaiKakakuInfo">パラメータ</param>
    ''' <returns>未設定も含む販売価格CSVデータ件数</returns>
    Public Function SelMiSeteiHanbaiKakakuCSVCount(ByVal dtHanbaiKakakuInfo As HanbaiKakakuMasterDataSet.Param_HanbaiKakakuInfoDataTable) _
           As Long

        'DataSetインスタンスの生成
        Dim dsHanbaiKakaku As New HanbaiKakakuMasterDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   COUNT_BIG(*) AS count ")
            .AppendLine(" FROM ")
            .AppendLine("   (SELECT ")
            .AppendLine("       tys_houhou_no ")
            .AppendLine("       ,tys_houhou_mei ")
            .AppendLine("    FROM m_tyousahouhou WITH(READCOMMITTED) ")     '調査方法マスタ
            .AppendLine("    WHERE (kakaku_settei_fuyou_flg IS NULL OR kakaku_settei_fuyou_flg = 0) ")
            '調査方法
            If dtHanbaiKakakuInfo(0).tys_houhou_no <> String.Empty Then
                .AppendLine(" AND tys_houhou_no = @tys_houhou_no ")
            End If
            .AppendLine("   ) MT ")
            .AppendLine(" CROSS JOIN ")
            .AppendLine("   (SELECT ")
            .AppendLine("       syouhin_cd ")
            .AppendLine("       ,syouhin_mei ")
            .AppendLine("    FROM m_syouhin WITH(READCOMMITTED) ")          '商品マスタ
            .AppendLine("    WHERE souko_cd = '100' ")
            .AppendLine("    AND torikesi = 0 ")
            '商品コード
            If dtHanbaiKakakuInfo(0).syouhin_cd <> String.Empty Then
                .AppendLine(" AND syouhin_cd = @syouhin_cd ")
            End If
            .AppendLine("   ) MS ")
            .AppendLine(" CROSS JOIN ( ")
            Select Case dtHanbaiKakakuInfo(0).aitesaki_syubetu
                Case 0
                    .AppendLine(" SELECT ")
                    .AppendLine("   0 AS aitesaki_syubetu ")
                    .AppendLine("   ,'ALL' AS aitesaki_cd ")
                    .AppendLine("   ,'相手先なし' AS aitesaki_mei ")
                Case 1
                    .AppendLine(" SELECT ")
                    .AppendLine("   1 AS aitesaki_syubetu ")
                    .AppendLine("   ,kameiten_cd AS aitesaki_cd ")
                    .AppendLine("   ,kameiten_mei1 AS aitesaki_mei ")
                    .AppendLine(" FROM ")
                    .AppendLine("   m_kameiten WITH(READCOMMITTED) ")   '加盟店マスタ
                    .AppendLine(" WHERE 1=1 ")
                    '相手先コード
                    If dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                        .AppendLine(" AND kameiten_cd BETWEEN @aitesaki_cd_from AND @aitesaki_cd_to ")
                    ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to = String.Empty Then
                        .AppendLine(" AND kameiten_cd = @aitesaki_cd_from ")
                    ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from = String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                        .AppendLine(" AND kameiten_cd = @aitesaki_cd_to ")
                    End If
                    '取消相手先
                    If dtHanbaiKakakuInfo(0).torikesi_aitesaki <> String.Empty Then
                        .AppendLine(" AND torikesi = 0 ")
                    End If
                Case 5
                    .AppendLine(" SELECT ")
                    .AppendLine("   5 AS aitesaki_syubetu ")
                    .AppendLine("   ,eigyousyo_cd AS aitesaki_cd ")
                    .AppendLine("   ,eigyousyo_mei AS aitesaki_mei ")
                    .AppendLine(" FROM ")
                    .AppendLine("   m_eigyousyo WITH(READCOMMITTED) ")   '営業所マスタ
                    .AppendLine(" WHERE 1=1 ")
                    '相手先コード
                    If dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                        .AppendLine(" AND eigyousyo_cd BETWEEN @aitesaki_cd_from AND @aitesaki_cd_to ")
                    ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to = String.Empty Then
                        .AppendLine(" AND eigyousyo_cd = @aitesaki_cd_from ")
                    ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from = String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                        .AppendLine(" AND eigyousyo_cd = @aitesaki_cd_to ")
                    End If
                    '取消相手先
                    If dtHanbaiKakakuInfo(0).torikesi_aitesaki <> String.Empty Then
                        .AppendLine(" AND torikesi = 0 ")
                    End If
                Case 7
                    .AppendLine(" SELECT ")
                    .AppendLine("   7 AS aitesaki_syubetu ")
                    .AppendLine("   ,keiretu_cd AS aitesaki_cd ")
                    .AppendLine("   ,MIN(keiretu_mei) AS aitesaki_mei ")
                    .AppendLine(" FROM ")
                    .AppendLine("   m_keiretu WITH(READCOMMITTED) ")   '系列マスタ
                    .AppendLine(" WHERE 1=1 ")
                    '相手先コード
                    If dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                        .AppendLine(" AND keiretu_cd BETWEEN @aitesaki_cd_from AND @aitesaki_cd_to ")
                    ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to = String.Empty Then
                        .AppendLine(" AND keiretu_cd = @aitesaki_cd_from ")
                    ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from = String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                        .AppendLine(" AND keiretu_cd = @aitesaki_cd_to ")
                    End If
                    '取消相手先
                    If dtHanbaiKakakuInfo(0).torikesi_aitesaki <> String.Empty Then
                        .AppendLine(" AND torikesi = 0 ")
                    End If
                    .AppendLine(" GROUP BY ")
                    .AppendLine("   keiretu_cd ")
            End Select
            .AppendLine(" ) SUB ")
            .AppendLine(" LEFT OUTER JOIN ")
            .AppendLine("   m_hanbai_kakaku MHK WITH(READCOMMITTED) ")     '販売価格M
            .AppendLine(" ON    MS.syouhin_cd = MHK.syouhin_cd ")
            .AppendLine(" AND   MT.tys_houhou_no = MHK.tys_houhou_no ")
            .AppendLine(" AND   SUB.aitesaki_syubetu = MHK.aitesaki_syubetu ")
            .AppendLine(" AND   SUB.aitesaki_cd = MHK.aitesaki_cd ")

            '相手先コード
            If dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                paramList.Add(MakeParam("@aitesaki_cd_from", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_from))
                paramList.Add(MakeParam("@aitesaki_cd_to", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_to))
            ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to = String.Empty Then
                paramList.Add(MakeParam("@aitesaki_cd_from", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_from))
            ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from = String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                paramList.Add(MakeParam("@aitesaki_cd_to", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_to))
            End If

            '商品コード
            If dtHanbaiKakakuInfo(0).syouhin_cd <> String.Empty Then
                paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtHanbaiKakakuInfo(0).syouhin_cd))
            End If

            '調査方法
            If dtHanbaiKakakuInfo(0).tys_houhou_no <> String.Empty Then
                paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 4, dtHanbaiKakakuInfo(0).tys_houhou_no))
            End If

        End With

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanbaiKakaku, _
                    "dtHanbaiKakakuCsvCount", paramList.ToArray)

        Return dsHanbaiKakaku.Tables("dtHanbaiKakakuCsvCount").Rows(0).Item("count")

    End Function
    ''' <summary>未設定も含む販売価格CSVデータを取得する</summary>
    ''' <param name="dtHanbaiKakakuInfo">パラメータ</param>
    ''' <returns>未設定も含む販売価格CSVデータテーブル</returns>
    Public Function SelMiSeteiHanbaiKakakuCSVInfo(ByVal dtHanbaiKakakuInfo As HanbaiKakakuMasterDataSet.Param_HanbaiKakakuInfoDataTable) _
           As HanbaiKakakuMasterDataSet.HanbaiKakakuCSVInfoTableDataTable

        'DataSetインスタンスの生成
        Dim dsHanbaiKakaku As New HanbaiKakakuMasterDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine(" SELECT ")
            'EDI情報作成日
            .AppendLine("   RIGHT(CONVERT(varchar(10),GETDATE(),112),4) + REPLACE(CONVERT(varchar(8),GETDATE(),108),':','') AS edi_jouhou_sakusei_date ")
            .AppendLine("   ,SUB.aitesaki_syubetu ")                        'サブ.相手先種別
            .AppendLine("   ,SUB.aitesaki_cd ")                             'サブ.相手先コード
            .AppendLine("   ,SUB.aitesaki_mei ")                            'サブ.相手先名
            .AppendLine("   ,MS.syouhin_cd ")                               '商品M.商品コード
            .AppendLine("   ,MS.syouhin_mei ")                              '商品M.商品名
            .AppendLine("   ,MT.tys_houhou_no ")                            '調査方法M.調査方法NO
            .AppendLine("   ,MT.tys_houhou_mei ")                           '調査方法M.調査方法
            .AppendLine("   ,MHK.torikesi ")                                '販売価格M.取消
            .AppendLine("   ,MHK.koumuten_seikyuu_gaku ")                   '販売価格M.工務店請求金額
            .AppendLine("   ,MHK.koumuten_seikyuu_gaku_henkou_flg ")        '販売価格M.工務店請求金額変更FLG
            .AppendLine("   ,MHK.jitu_seikyuu_gaku ")                       '販売価格M.実請求金額
            .AppendLine("   ,MHK.jitu_seikyuu_gaku_henkou_flg ")            '販売価格M.実請求金額変更FLG
            .AppendLine("   ,MHK.koukai_flg ")                              '販売価格M.公開フラグ
            .AppendLine(" FROM ")
            .AppendLine("   (SELECT ")
            .AppendLine("       tys_houhou_no ")
            .AppendLine("       ,tys_houhou_mei ")
            .AppendLine("    FROM m_tyousahouhou WITH(READCOMMITTED) ")     '調査方法マスタ
            .AppendLine("    WHERE (kakaku_settei_fuyou_flg IS NULL OR kakaku_settei_fuyou_flg = 0) ")
            '調査方法
            If dtHanbaiKakakuInfo(0).tys_houhou_no <> String.Empty Then
                .AppendLine(" AND tys_houhou_no = @tys_houhou_no ")
            End If
            .AppendLine("   ) MT ")
            .AppendLine(" CROSS JOIN ")
            .AppendLine("   (SELECT ")
            .AppendLine("       syouhin_cd ")
            .AppendLine("       ,syouhin_mei ")
            .AppendLine("    FROM m_syouhin WITH(READCOMMITTED) ")          '商品マスタ
            .AppendLine("    WHERE souko_cd = '100' ")
            .AppendLine("    AND torikesi = 0 ")
            '商品コード
            If dtHanbaiKakakuInfo(0).syouhin_cd <> String.Empty Then
                .AppendLine(" AND syouhin_cd = @syouhin_cd ")
            End If
            .AppendLine("   ) MS ")
            .AppendLine(" CROSS JOIN ( ")
            Select Case dtHanbaiKakakuInfo(0).aitesaki_syubetu
                Case 0
                    .AppendLine(" SELECT ")
                    .AppendLine("   0 AS aitesaki_syubetu ")
                    .AppendLine("   ,'ALL' AS aitesaki_cd ")
                    .AppendLine("   ,'相手先なし' AS aitesaki_mei ")
                Case 1
                    .AppendLine(" SELECT ")
                    .AppendLine("   1 AS aitesaki_syubetu ")
                    .AppendLine("   ,kameiten_cd AS aitesaki_cd ")
                    .AppendLine("   ,kameiten_mei1 AS aitesaki_mei ")
                    .AppendLine(" FROM ")
                    .AppendLine("   m_kameiten WITH(READCOMMITTED) ")   '加盟店マスタ
                    .AppendLine(" WHERE 1=1 ")
                    '相手先コード
                    If dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                        .AppendLine(" AND kameiten_cd BETWEEN @aitesaki_cd_from AND @aitesaki_cd_to ")
                    ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to = String.Empty Then
                        .AppendLine(" AND kameiten_cd = @aitesaki_cd_from ")
                    ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from = String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                        .AppendLine(" AND kameiten_cd = @aitesaki_cd_to ")
                    End If
                    '取消相手先
                    If dtHanbaiKakakuInfo(0).torikesi_aitesaki <> String.Empty Then
                        .AppendLine(" AND torikesi = 0 ")
                    End If
                Case 5
                    .AppendLine(" SELECT ")
                    .AppendLine("   5 AS aitesaki_syubetu ")
                    .AppendLine("   ,eigyousyo_cd AS aitesaki_cd ")
                    .AppendLine("   ,eigyousyo_mei AS aitesaki_mei ")
                    .AppendLine(" FROM ")
                    .AppendLine("   m_eigyousyo WITH(READCOMMITTED) ")   '営業所マスタ
                    .AppendLine(" WHERE 1=1 ")
                    '相手先コード
                    If dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                        .AppendLine(" AND eigyousyo_cd BETWEEN @aitesaki_cd_from AND @aitesaki_cd_to ")
                    ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to = String.Empty Then
                        .AppendLine(" AND eigyousyo_cd = @aitesaki_cd_from ")
                    ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from = String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                        .AppendLine(" AND eigyousyo_cd = @aitesaki_cd_to ")
                    End If
                    '取消相手先
                    If dtHanbaiKakakuInfo(0).torikesi_aitesaki <> String.Empty Then
                        .AppendLine(" AND torikesi = 0 ")
                    End If
                Case 7
                    .AppendLine(" SELECT ")
                    .AppendLine("   7 AS aitesaki_syubetu ")
                    .AppendLine("   ,keiretu_cd AS aitesaki_cd ")
                    .AppendLine("   ,MIN(keiretu_mei) AS aitesaki_mei ")
                    .AppendLine(" FROM ")
                    .AppendLine("   m_keiretu WITH(READCOMMITTED) ")   '系列マスタ
                    .AppendLine(" WHERE 1=1 ")
                    '相手先コード
                    If dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                        .AppendLine(" AND keiretu_cd BETWEEN @aitesaki_cd_from AND @aitesaki_cd_to ")
                    ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to = String.Empty Then
                        .AppendLine(" AND keiretu_cd = @aitesaki_cd_from ")
                    ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from = String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                        .AppendLine(" AND keiretu_cd = @aitesaki_cd_to ")
                    End If
                    '取消相手先
                    If dtHanbaiKakakuInfo(0).torikesi_aitesaki <> String.Empty Then
                        .AppendLine(" AND torikesi = 0 ")
                    End If
                    .AppendLine(" GROUP BY ")
                    .AppendLine("   keiretu_cd ")
            End Select
            .AppendLine(" ) SUB ")
            .AppendLine(" LEFT OUTER JOIN ")
            .AppendLine("   m_hanbai_kakaku MHK WITH(READCOMMITTED) ")     '販売価格M
            .AppendLine(" ON    MS.syouhin_cd = MHK.syouhin_cd ")
            .AppendLine(" AND   MT.tys_houhou_no = MHK.tys_houhou_no ")
            .AppendLine(" AND   SUB.aitesaki_syubetu = MHK.aitesaki_syubetu ")
            .AppendLine(" AND   SUB.aitesaki_cd = MHK.aitesaki_cd ")

            '相手先コード
            If dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                paramList.Add(MakeParam("@aitesaki_cd_from", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_from))
                paramList.Add(MakeParam("@aitesaki_cd_to", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_to))
            ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to = String.Empty Then
                paramList.Add(MakeParam("@aitesaki_cd_from", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_from))
            ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from = String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                paramList.Add(MakeParam("@aitesaki_cd_to", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_to))
            End If

            '商品コード
            If dtHanbaiKakakuInfo(0).syouhin_cd <> String.Empty Then
                paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtHanbaiKakakuInfo(0).syouhin_cd))
            End If

            '調査方法
            If dtHanbaiKakakuInfo(0).tys_houhou_no <> String.Empty Then
                paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 4, dtHanbaiKakakuInfo(0).tys_houhou_no))
            End If

            'ソート順
            .AppendLine(" ORDER BY ")
            .AppendLine("      SUB.aitesaki_syubetu ")
            .AppendLine("      ,SUB.aitesaki_cd ")
            .AppendLine("      ,MS.syouhin_cd ")
            .AppendLine("      ,MT.tys_houhou_no ")
        End With

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanbaiKakaku, _
                    dsHanbaiKakaku.HanbaiKakakuCSVInfoTable.TableName, paramList.ToArray)

        Return dsHanbaiKakaku.HanbaiKakakuCSVInfoTable

    End Function
    ''' <summary>販売価格CSVデータを取得する</summary>
    ''' <param name="dtHanbaiKakakuInfo">パラメータ</param>
    ''' <returns>販売価格CSVデータテーブル</returns>
    Public Function SelHanbaiKakakuCSVInfo(ByVal dtHanbaiKakakuInfo As HanbaiKakakuMasterDataSet.Param_HanbaiKakakuInfoDataTable) _
           As HanbaiKakakuMasterDataSet.HanbaiKakakuCSVInfoTableDataTable

        'DataSetインスタンスの生成
        Dim dsHanbaiKakaku As New HanbaiKakakuMasterDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine(" SELECT ")
            'EDI情報作成日
            .AppendLine("   RIGHT(CONVERT(varchar(10),GETDATE(),112),4) + REPLACE(CONVERT(varchar(8),GETDATE(),108),':','') AS edi_jouhou_sakusei_date ")
            .AppendLine("   ,MHK.aitesaki_syubetu ")                        '販売価格M.相手先種別
            .AppendLine("   ,MHK.aitesaki_cd ")                             '販売価格M.相手先コード
            .AppendLine("   ,SUB.aitesaki_mei ")                            'サブ.相手先名
            .AppendLine("   ,MHK.syouhin_cd ")                              '販売価格M.商品コード
            .AppendLine("   ,MS.syouhin_mei ")                              '商品M.商品名
            .AppendLine("   ,MHK.tys_houhou_no ")                           '販売価格M.調査方法NO
            .AppendLine("   ,MT.tys_houhou_mei ")                           '調査方法M.調査方法
            .AppendLine("   ,MHK.torikesi ")                                '販売価格M.取消
            .AppendLine("   ,MHK.koumuten_seikyuu_gaku ")                   '販売価格M.工務店請求金額
            .AppendLine("   ,MHK.koumuten_seikyuu_gaku_henkou_flg ")        '販売価格M.工務店請求金額変更FLG
            .AppendLine("   ,MHK.jitu_seikyuu_gaku ")                       '販売価格M.実請求金額
            .AppendLine("   ,MHK.jitu_seikyuu_gaku_henkou_flg ")            '販売価格M.実請求金額変更FLG
            .AppendLine("   ,MHK.koukai_flg ")                              '販売価格M.公開フラグ
            .AppendLine(" FROM ")
            .AppendLine("   m_hanbai_kakaku MHK WITH(READCOMMITTED) ")      '販売価格M
            .AppendLine(" LEFT OUTER JOIN ")
            .AppendLine("   m_kakutyou_meisyou MKM WITH(READCOMMITTED) ")   '拡張名称M
            .AppendLine(" ON    MHK.aitesaki_syubetu = MKM.code ")
            .AppendLine(" AND   MKM.meisyou_syubetu = 23 ")
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
            .AppendLine("   m_syouhin MS WITH(READCOMMITTED) ")   '商品マスタ
            .AppendLine(" ON    MHK.syouhin_cd = MS.syouhin_cd ")
            .AppendLine(" AND   MS.souko_cd = '100' ")
            .AppendLine(" AND   MS.torikesi = 0 ")
            .AppendLine(" LEFT OUTER JOIN ")
            .AppendLine("   m_tyousahouhou MT WITH(READCOMMITTED) ")   '調査方法マスタ
            .AppendLine(" ON    MHK.tys_houhou_no = MT.tys_houhou_no ")
            .AppendLine(" WHERE ")

            '相手先種別
            .AppendLine(" MHK.aitesaki_syubetu = @aitesaki_syubetu ")
            paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 4, dtHanbaiKakakuInfo(0).aitesaki_syubetu))

            '相手先コード
            If dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                .AppendLine(" AND MHK.aitesaki_cd BETWEEN @aitesaki_cd_from AND @aitesaki_cd_to ")
                paramList.Add(MakeParam("@aitesaki_cd_from", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_from))
                paramList.Add(MakeParam("@aitesaki_cd_to", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_to))
            ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to = String.Empty Then
                .AppendLine(" AND MHK.aitesaki_cd = @aitesaki_cd_from ")
                paramList.Add(MakeParam("@aitesaki_cd_from", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_from))
            ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from = String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                .AppendLine(" AND MHK.aitesaki_cd = @aitesaki_cd_to ")
                paramList.Add(MakeParam("@aitesaki_cd_to", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_to))
            End If

            '商品コード
            If dtHanbaiKakakuInfo(0).syouhin_cd <> String.Empty Then
                .AppendLine(" AND MHK.syouhin_cd = @syouhin_cd ")
                paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtHanbaiKakakuInfo(0).syouhin_cd))
            End If

            '調査方法
            If dtHanbaiKakakuInfo(0).tys_houhou_no <> String.Empty Then
                .AppendLine(" AND MHK.tys_houhou_no = @tys_houhou_no ")
                paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 4, dtHanbaiKakakuInfo(0).tys_houhou_no))
            End If

            '取消
            If dtHanbaiKakakuInfo(0).torikesi <> String.Empty Then
                .AppendLine(" AND MHK.torikesi = @torikesi ")
                paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, 0))
            End If

            '取消相手先
            If dtHanbaiKakakuInfo(0).torikesi_aitesaki <> String.Empty Then
                .AppendLine(" AND SUB.torikesi = @torikesi_aitesaki ")
                paramList.Add(MakeParam("@torikesi_aitesaki", SqlDbType.Int, 4, 0))
            End If

            '=======================2011/05/18 車龍 仕様変更 追加 開始↓==========================
            '\0は対象外
            If dtHanbaiKakakuInfo(0).kingaku_0_taisyou_gai <> String.Empty Then
                .AppendLine(" AND MHK.jitu_seikyuu_gaku <> 0 ")
            End If
            '=======================2011/05/18 車龍 仕様変更 追加 終了↑==========================

            'ソート順
            .AppendLine(" ORDER BY ")
            .AppendLine("      MHK.aitesaki_syubetu ")
            .AppendLine("      ,MHK.aitesaki_cd ")
            .AppendLine("      ,MHK.syouhin_cd ")
            .AppendLine("      ,MHK.tys_houhou_no ")

        End With

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanbaiKakaku, _
                    dsHanbaiKakaku.HanbaiKakakuCSVInfoTable.TableName, paramList.ToArray)

        Return dsHanbaiKakaku.HanbaiKakakuCSVInfoTable

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
            .AppendLine("	file_kbn = 3 ")         'ファイル区分
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
            .AppendLine("	file_kbn = 3 ")                         'ファイル区分
        End With

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanbaiKakaku, "dtUploadKanri")

        Return CInt(dsHanbaiKakaku.Tables("dtUploadKanri").Rows(0).Item("count").ToString)

    End Function
    ''' <summary>相手先（種別・コード）存在チェック</summary>
    ''' <param name="strAitesakiSyubetu">相手先種別</param>
    ''' <param name="strAitesakiCd">相手先コード</param>
    ''' <returns>相手先（種別・コード）存在区分</returns>
    Public Function SelAitesaki(ByVal strAitesakiSyubetu As String, ByVal strAitesakiCd As String) As Boolean

        'DataSetインスタンスの生成
        Dim dsAitesaki As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("	aitesaki_syubetu ") '相手先種別
            .AppendLine("	,aitesaki_cd ")     '相手先コード
            .AppendLine(" FROM ")
            .AppendLine("	( ")
            .AppendLine("		SELECT ")
            .AppendLine("			0				AS aitesaki_syubetu ")  '相手先種別
            .AppendLine("			,'ALL'			AS aitesaki_cd ")       '相手先コード
            .AppendLine("			,'相手先なし'	AS aitesaki_mei ")      '相手先名
            .AppendLine("			,0				AS torikesi ")          '取消
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			1				AS aitesaki_syubetu ")  '相手先種別
            .AppendLine("			,kameiten_cd	AS aitesaki_cd ")       '加盟店コード
            .AppendLine("			,kameiten_mei1	AS aitesaki_mei ")      '加盟店名1
            .AppendLine("			,torikesi		AS torikesi ")          '取消
            .AppendLine("		FROM ")
            .AppendLine("			m_kameiten WITH(READCOMMITTED) ")       '加盟店マスタ
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			5				AS aitesaki_syubetu ")  '相手先種別
            .AppendLine("			,eigyousyo_cd	AS aitesaki_cd ")       '営業所コード
            .AppendLine("			,eigyousyo_mei	AS aitesaki_mei ")      '営業所名
            .AppendLine("			,torikesi		AS torikesi ")          '取消
            .AppendLine("		FROM ")
            .AppendLine("			m_eigyousyo WITH(READCOMMITTED) ")      '営業所マスタ
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			7				AS aitesaki_syubetu ")  '相手先種別
            .AppendLine("			,keiretu_cd		AS aitesaki_cd ")       '系列コード
            .AppendLine("			,MIN(keiretu_mei)	AS aitesaki_mei ")  '系列名
            .AppendLine("			,MIN(torikesi)		AS torikesi ")      '取消
            .AppendLine("		FROM ")
            .AppendLine("			m_keiretu WITH(READCOMMITTED) ")        '系列マスタ
            .AppendLine("		GROUP BY ")
            .AppendLine("			keiretu_cd ")                           '系列コード
            .AppendLine("	) AS TA ")
            .AppendLine(" WHERE aitesaki_syubetu = @aitesaki_syubetu ")
            .AppendLine(" AND   aitesaki_cd = @aitesaki_cd ")
        End With

        paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 4, strAitesakiSyubetu))
        paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, strAitesakiCd))

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsAitesaki, "dtAitesaki", paramList.ToArray)

        '戻り値
        If dsAitesaki.Tables("dtAitesaki").Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function
    ''' <summary>商品コード存在チェック</summary>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <returns>商品コード存在区分</returns>
    Public Function SelSyouhinCd(ByVal strSyouhinCd As String) As Boolean

        'DataSetインスタンスの生成
        Dim dsSyouhin As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	syouhin_cd ")
            .AppendLine("FROM  ")
            .AppendLine("	m_syouhin WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	syouhin_cd = @syouhin_cd ")
        End With

        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsSyouhin, "dtSyouhinCd", paramList.ToArray)

        '戻り値
        If dsSyouhin.Tables("dtSyouhinCd").Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function
    ''' <summary>調査方法NO存在チェック</summary>
    ''' <param name="strTysHouhouNo">調査方法NO</param>
    ''' <returns>調査方法NO存在区分</returns>
    Public Function SelTysHouhou(ByVal strTysHouhouNo As String) As Boolean

        'DataSetインスタンスの生成
        Dim dsTysHouhou As New Data.DataSet

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

        paramList.Add(MakeParam("@TyousahouhouNo", SqlDbType.Int, 4, strTysHouhouNo))

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsTysHouhou, "dtTysHouhou", paramList.ToArray)

        '戻り値
        If dsTysHouhou.Tables("dtTysHouhou").Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function
    ''' <summary>販売価格データ存在チェック</summary>
    ''' <param name="strAitesakiSyubetu">相手先種別</param>
    ''' <param name="strAitesakiCd">相手先コード</param>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <param name="strTysHouhouNo">調査方法NO</param>
    ''' <returns>販売価格データ存在区分</returns>
    Public Function SelHanbaiKakaku(ByVal strAitesakiSyubetu As String, ByVal strAitesakiCd As String, _
                                    ByVal strSyouhinCd As String, ByVal strTysHouhouNo As Integer) As Boolean

        'DataSetインスタンスの生成
        Dim dsHanbaiKakaku As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   aitesaki_syubetu ")
            .AppendLine(" FROM ")
            .AppendLine("	m_hanbai_kakaku WITH(READCOMMITTED) ")
            .AppendLine(" WHERE ")
            .AppendLine("	aitesaki_syubetu = @aitesaki_syubetu ")
            .AppendLine(" AND ")
            .AppendLine("	aitesaki_cd = @aitesaki_cd ")
            .AppendLine(" AND ")
            .AppendLine("	syouhin_cd = @syouhin_cd ")
            .AppendLine(" AND ")
            .AppendLine("	tys_houhou_no = @tys_houhou_no ")
        End With

        paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 4, strAitesakiSyubetu))
        paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, strAitesakiCd))
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))
        paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 4, strTysHouhouNo))

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanbaiKakaku, "dtHanbaiKakaku", paramList.ToArray)

        '戻り値
        If dsHanbaiKakaku.Tables("dtHanbaiKakaku").Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function
    ''' <summary>販売価格エラー情報テーブルを登録する</summary>
    ''' <param name="dtHanbaiKakakuError">エラーデータ情報</param>
    ''' <returns>成功失敗区分</returns>
    Public Function InsHanbaiKakakuError(ByVal dtHanbaiKakakuError As Data.DataTable, _
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
            .AppendLine("   m_hanbai_kakaku_error WITH(UPDLOCK) ")
            .AppendLine("   ( ")
            .AppendLine("		edi_jouhou_sakusei_date ")              'EDI情報作成日
            .AppendLine("		,gyou_no ")                             '行NO
            .AppendLine("		,syori_datetime ")                      '処理日時
            .AppendLine("		,aitesaki_syubetu ")                    '相手先種別
            .AppendLine("		,aitesaki_cd ")                         '相手先コード
            .AppendLine("		,aitesaki_mei ")                        '相手先名
            .AppendLine("		,syouhin_cd ")                          '商品コード
            .AppendLine("		,syouhin_mei ")                         '商品名
            .AppendLine("		,tys_houhou_no ")                       '調査方法NO
            .AppendLine("		,tys_houhou ")                          '調査方法
            .AppendLine("		,torikesi ")                            '取消
            .AppendLine("		,koumuten_seikyuu_gaku ")               '工務店請求金額
            .AppendLine("		,koumuten_seikyuu_gaku_henkou_flg ")    '工務店請求金額変更FLG
            .AppendLine("		,jitu_seikyuu_gaku ")                   '実請求金額
            .AppendLine("		,jitu_seikyuu_gaku_henkou_flg ")        '実請求金額変更FLG
            .AppendLine("		,koukai_flg ")                          '公開フラグ
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
            .AppendLine("	,@tys_houhou_no ")
            .AppendLine("	,@tys_houhou ")
            .AppendLine("	,@torikesi ")
            .AppendLine("	,@koumuten_seikyuu_gaku ")
            .AppendLine("	,@koumuten_seikyuu_gaku_henkou_flg ")
            .AppendLine("	,@jitu_seikyuu_gaku ")
            .AppendLine("	,@jitu_seikyuu_gaku_henkou_flg ")
            .AppendLine("	,@koukai_flg ")
            .AppendLine("	,@add_login_user_id ")
            .AppendLine("	,GETDATE() ")
            .AppendLine("	,NULL ")
            .AppendLine("	,NULL ")
            .AppendLine(") ")
        End With

        For i As Integer = 0 To dtHanbaiKakakuError.Rows.Count - 1

            'パラメータの設定
            paramList.Clear()
            paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, dtHanbaiKakakuError.Rows(i).Item(0).ToString.Trim))
            paramList.Add(MakeParam("@gyou_no", SqlDbType.Int, 4, dtHanbaiKakakuError.Rows(i).Item(14).ToString.Trim))
            paramList.Add(MakeParam("@syori_datetime", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(strUploadDate))))
            paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.VarChar, 5, dtHanbaiKakakuError.Rows(i).Item(1).ToString.Trim))
            paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, dtHanbaiKakakuError.Rows(i).Item(2).ToString.Trim))
            paramList.Add(MakeParam("@aitesaki_mei", SqlDbType.VarChar, 40, dtHanbaiKakakuError.Rows(i).Item(3).ToString.Trim))
            paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtHanbaiKakakuError.Rows(i).Item(4).ToString.Trim))
            paramList.Add(MakeParam("@syouhin_mei", SqlDbType.VarChar, 40, dtHanbaiKakakuError.Rows(i).Item(5).ToString.Trim))
            paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.VarChar, 5, dtHanbaiKakakuError.Rows(i).Item(6).ToString.Trim))
            paramList.Add(MakeParam("@tys_houhou", SqlDbType.VarChar, 32, dtHanbaiKakakuError.Rows(i).Item(7).ToString.Trim))
            paramList.Add(MakeParam("@torikesi", SqlDbType.VarChar, 1, dtHanbaiKakakuError.Rows(i).Item(8).ToString.Trim))
            paramList.Add(MakeParam("@koumuten_seikyuu_gaku", SqlDbType.VarChar, 10, dtHanbaiKakakuError.Rows(i).Item(9).ToString.Trim))
            paramList.Add(MakeParam("@koumuten_seikyuu_gaku_henkou_flg", SqlDbType.VarChar, 1, dtHanbaiKakakuError.Rows(i).Item(10).ToString.Trim))
            paramList.Add(MakeParam("@jitu_seikyuu_gaku", SqlDbType.VarChar, 10, dtHanbaiKakakuError.Rows(i).Item(11).ToString.Trim))
            paramList.Add(MakeParam("@jitu_seikyuu_gaku_henkou_flg", SqlDbType.VarChar, 1, dtHanbaiKakakuError.Rows(i).Item(12).ToString.Trim))
            paramList.Add(MakeParam("@koukai_flg", SqlDbType.VarChar, 1, dtHanbaiKakakuError.Rows(i).Item(13).ToString.Trim))
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
    ''' <summary>販売価格マスタを登録・更新する</summary>
    ''' <param name="dtHanbaiKakakuOk">データ情報</param>
    ''' <returns>成功失敗区分</returns>
    Public Function InsUpdHanbaiKakaku(ByVal dtHanbaiKakakuOk As Data.DataTable, _
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
            .AppendLine("	m_hanbai_kakaku WITH(UPDLOCK) ")
            .AppendLine("	( ")
            .AppendLine("		aitesaki_syubetu ")                     '相手先種別
            .AppendLine("		,aitesaki_cd ")                         '相手先コード
            .AppendLine("		,syouhin_cd ")                          '商品コード
            .AppendLine("		,tys_houhou_no ")                       '調査方法NO
            .AppendLine("		,torikesi ")                            '取消
            .AppendLine("		,koumuten_seikyuu_gaku ")               '工務店請求金額
            .AppendLine("		,koumuten_seikyuu_gaku_henkou_flg ")    '工務店請求金額変更FLG
            .AppendLine("		,jitu_seikyuu_gaku ")                   '実請求金額
            .AppendLine("		,jitu_seikyuu_gaku_henkou_flg ")        '実請求金額変更FLG
            .AppendLine("		,koukai_flg ")                          '公開フラグ
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
            .AppendLine("	,@tys_houhou_no ")
            .AppendLine("	,@torikesi ")
            .AppendLine("	,@koumuten_seikyuu_gaku ")
            .AppendLine("	,@koumuten_seikyuu_gaku_henkou_flg ")
            .AppendLine("	,@jitu_seikyuu_gaku ")
            .AppendLine("	,@jitu_seikyuu_gaku_henkou_flg ")
            .AppendLine("	,@koukai_flg ")
            .AppendLine("	,@add_login_user_id ")
            .AppendLine("	,GETDATE() ")
            .AppendLine("	,NULL ")
            .AppendLine("	,NULL ")
            .AppendLine(" ) ")
        End With

        '更新用sql文
        With strSqlUpd
            .AppendLine(" UPDATE ")
            .AppendLine("	m_hanbai_kakaku WITH(UPDLOCK) ")
            .AppendLine(" SET ")
            .AppendLine("	torikesi = @torikesi ")                             '取消
            .AppendLine("	,koumuten_seikyuu_gaku = @koumuten_seikyuu_gaku ")  '工務店請求金額
            .AppendLine("	,koumuten_seikyuu_gaku_henkou_flg = @koumuten_seikyuu_gaku_henkou_flg ") '工務店請求金額変更FLG
            .AppendLine("	,jitu_seikyuu_gaku = @jitu_seikyuu_gaku ")          '実請求金額
            .AppendLine("	,jitu_seikyuu_gaku_henkou_flg = @jitu_seikyuu_gaku_henkou_flg ")    '実請求金額変更FLG
            .AppendLine("	,koukai_flg = @koukai_flg ")                    '公開フラグ
            .AppendLine("	,upd_login_user_id = @upd_login_user_id ")      '更新ログインユーザID
            .AppendLine("	,upd_datetime = GETDATE() ")                    '更新日時
            .AppendLine(" WHERE aitesaki_syubetu = @aitesaki_syubetu ")     '相手先種別
            .AppendLine(" AND   aitesaki_cd = @aitesaki_cd ")               '相手先コード
            .AppendLine(" AND   syouhin_cd = @syouhin_cd ")                 '商品コード
            .AppendLine(" AND   tys_houhou_no = @tys_houhou_no ")           '調査方法NO
        End With

        For i As Integer = 0 To dtHanbaiKakakuOk.Rows.Count - 1
            'パラメータの設定
            paramList.Clear()
            paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 4, dtHanbaiKakakuOk.Rows(i).Item("aitesaki_syubetu").ToString.Trim))
            paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, dtHanbaiKakakuOk.Rows(i).Item("aitesaki_cd").ToString.Trim))
            paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtHanbaiKakakuOk.Rows(i).Item("syouhin_cd").ToString.Trim))
            paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 4, dtHanbaiKakakuOk.Rows(i).Item("tys_houhou_no").ToString.Trim))
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, dtHanbaiKakakuOk.Rows(i).Item("torikesi").ToString.Trim))
            If dtHanbaiKakakuOk.Rows(i).Item("koumuten_seikyuu_gaku").ToString.Trim = String.Empty Then
                paramList.Add(MakeParam("@koumuten_seikyuu_gaku", SqlDbType.Int, 4, DBNull.Value))
            Else
                paramList.Add(MakeParam("@koumuten_seikyuu_gaku", SqlDbType.Int, 4, dtHanbaiKakakuOk.Rows(i).Item("koumuten_seikyuu_gaku").ToString.Trim))
            End If
            If dtHanbaiKakakuOk.Rows(i).Item("koumuten_seikyuu_gaku_henkou_flg").ToString.Trim = String.Empty Then
                paramList.Add(MakeParam("@koumuten_seikyuu_gaku_henkou_flg", SqlDbType.Int, 4, DBNull.Value))
            Else
                paramList.Add(MakeParam("@koumuten_seikyuu_gaku_henkou_flg", SqlDbType.Int, 4, dtHanbaiKakakuOk.Rows(i).Item("koumuten_seikyuu_gaku_henkou_flg").ToString.Trim))
            End If
            If dtHanbaiKakakuOk.Rows(i).Item("jitu_seikyuu_gaku").ToString.Trim = String.Empty Then
                paramList.Add(MakeParam("@jitu_seikyuu_gaku", SqlDbType.Int, 4, DBNull.Value))
            Else
                paramList.Add(MakeParam("@jitu_seikyuu_gaku", SqlDbType.Int, 4, dtHanbaiKakakuOk.Rows(i).Item("jitu_seikyuu_gaku").ToString.Trim))
            End If
            If dtHanbaiKakakuOk.Rows(i).Item("jitu_seikyuu_gaku_henkou_flg").ToString.Trim = String.Empty Then
                paramList.Add(MakeParam("@jitu_seikyuu_gaku_henkou_flg", SqlDbType.Int, 4, DBNull.Value))
            Else
                paramList.Add(MakeParam("@jitu_seikyuu_gaku_henkou_flg", SqlDbType.Int, 4, dtHanbaiKakakuOk.Rows(i).Item("jitu_seikyuu_gaku_henkou_flg").ToString.Trim))
            End If
            If dtHanbaiKakakuOk.Rows(i).Item("koukai_flg").ToString.Trim = String.Empty Then
                paramList.Add(MakeParam("@koukai_flg", SqlDbType.Int, 4, DBNull.Value))
            Else
                paramList.Add(MakeParam("@koukai_flg", SqlDbType.Int, 4, dtHanbaiKakakuOk.Rows(i).Item("koukai_flg").ToString.Trim))
            End If
            paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strUserId))
            paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, strUserId))
            '更新されたデータセットを DB へ書き込み

            Try
                If dtHanbaiKakakuOk.Rows(i).Item("ins_upd_flg").Equals("0") Then
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
            .AppendLine("	,3 ")
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
    ''' <summary>販売価格エラー情報を取得する</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI情報作成日</param>
    ''' <param name="strSyoriDate">処理日時</param>
    ''' <returns>販売価格エラーデータテーブル</returns>
    Public Function SelHanbaiKakakuErr(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoridate As String) _
                        As HanbaiKakakuMasterDataSet.HanbaiKakakuErrTableDataTable

        'DataSetインスタンスの生成
        Dim dsHanbaiKakakuErr As New HanbaiKakakuMasterDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   TOP 100 ")
            .AppendLine("   MHKE.gyou_no ")                 '販売価格エラー情報T.行NO
            '販売価格エラー情報T.相手先種別：拡名M.名称
            .AppendLine("   ,CASE WHEN ISNULL(MHKE.aitesaki_syubetu,'') = '' THEN '' ")
            .AppendLine("         ELSE MHKE.aitesaki_syubetu + '：' + ISNULL(MKM.meisyou,'') ")
            .AppendLine("    END AS aitesaki ")
            .AppendLine("   ,MHKE.aitesaki_cd ")            '販売価格エラー情報T.相手先コード
            .AppendLine("   ,MHKE.aitesaki_mei ")           '販売価格エラー情報T.相手先名
            .AppendLine("   ,MHKE.syouhin_cd ")             '販売価格エラー情報T.商品コード
            .AppendLine("   ,MHKE.syouhin_mei ")            '販売価格エラー情報T.商品名
            '販売価格エラー情報T.調査方法NO：販売価格エラー情報T.調査方法
            .AppendLine("   ,CASE WHEN ISNULL(MHKE.tys_houhou_no,'') = '' AND ISNULL(MHKE.tys_houhou,'') = '' THEN '' ")
            .AppendLine("         ELSE MHKE.tys_houhou_no + '：' + MHKE.tys_houhou ")
            .AppendLine("    END AS tys_houhou ")
            .AppendLine("   ,CASE MHKE.torikesi ")
            .AppendLine("       WHEN '0' THEN '' ")
            .AppendLine("       ELSE '取消' ")
            .AppendLine("    END AS torikesi ")                 '販売価格エラー情報T.取消
            .AppendLine("   ,MHKE.koumuten_seikyuu_gaku ")      '販売価格エラー情報T.工務店請求金額
            .AppendLine("   ,CASE ISNULL(MHKE.koumuten_seikyuu_gaku_henkou_flg,'0') ")
            .AppendLine("       WHEN '0' THEN '変更不可' ")
            .AppendLine("       ELSE '変更可' ")
            .AppendLine("    END AS koumuten_seikyuu_gaku_henkou_flg ")     '販売価格エラー情報T.工務店請求金額変更FLG
            .AppendLine("   ,MHKE.jitu_seikyuu_gaku ")     '販売価格エラー情報T.実請求金額
            .AppendLine("   ,CASE ISNULL(MHKE.jitu_seikyuu_gaku_henkou_flg,'0') ")
            .AppendLine("       WHEN '0' THEN '変更不可' ")
            .AppendLine("       ELSE '変更可' ")
            .AppendLine("    END AS jitu_seikyuu_gaku_henkou_flg ")     '販売価格エラー情報T.実請求金額変更FLG
            .AppendLine("   ,CASE ISNULL(MHKE.koukai_flg,'0') ")
            .AppendLine("       WHEN '0' THEN '非公開' ")
            .AppendLine("       ELSE '公開' ")
            .AppendLine("    END AS koukai_flg ")   '販売価格エラー情報T.公開フラグ
            .AppendLine(" FROM ")
            .AppendLine("   m_hanbai_kakaku_error MHKE WITH(READCOMMITTED) ")   '販売価格エラー情報T
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
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanbaiKakakuErr, _
                    dsHanbaiKakakuErr.HanbaiKakakuErrTable.TableName, paramList.ToArray)

        Return dsHanbaiKakakuErr.HanbaiKakakuErrTable

    End Function
    ''' <summary>販売価格エラー件数を取得する</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI情報作成日</param>
    ''' <param name="strSyoriDate">処理日時</param>
    ''' <returns>販売価格エラー件数</returns>
    Public Function SelHanbaiKakakuErrCount(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoriDate As String) As String

        'DataSetインスタンスの生成
        Dim dsHanbaiKakakuErr As New HanbaiKakakuMasterDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   COUNT(*) AS count ")
            .AppendLine(" FROM ")
            .AppendLine("   m_hanbai_kakaku_error WITH(READCOMMITTED) ")   '販売価格エラー情報T
            .AppendLine(" WHERE ")

            'EDI情報
            .AppendLine(" edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date ")
            paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdiJouhouSakuseiDate))

            '処理日時
            .AppendLine(" AND CONVERT(varchar(100),syori_datetime,112) + REPLACE(CONVERT(varchar(100),syori_datetime,114),':','') = @syori_datetime")
            paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 40, strSyoriDate))

        End With

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanbaiKakakuErr, _
                    "dtHanbaiKakakuErr", paramList.ToArray)

        Return dsHanbaiKakakuErr.Tables("dtHanbaiKakakuErr").Rows(0).Item("count")

    End Function
    ''' <summary>販売価格エラーCSV情報を取得する</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI情報作成日</param>
    ''' <param name="strSyoriDate">処理日時</param>
    ''' <returns>販売価格エラーCSVデータテーブル</returns>
    Public Function SelHanbaiKakakuErrCsv(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoriDate As String) _
                    As HanbaiKakakuMasterDataSet.HanbaiKakakuErrCSVTableDataTable

        'DataSetインスタンスの生成
        Dim dsHanbaiKakakuErr As New HanbaiKakakuMasterDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   TOP 5000 ")
            .AppendLine("   MHKE.edi_jouhou_sakusei_date ")             '販売価格エラー情報T.EDI情報作成日
            .AppendLine("   ,MHKE.gyou_no ")                            '販売価格エラー情報T.行NO
            .AppendLine("   ,MHKE.syori_datetime ")                     '販売価格エラー情報T.処理日時
            .AppendLine("   ,MHKE.aitesaki_syubetu ")                   '販売価格エラー情報T.相手先種別
            .AppendLine("   ,MHKE.aitesaki_cd ")                        '販売価格エラー情報T.相手先コード
            .AppendLine("   ,MHKE.aitesaki_mei ")                       '販売価格エラー情報T.相手先名
            .AppendLine("   ,MHKE.syouhin_cd ")                         '販売価格エラー情報T.商品コード
            .AppendLine("   ,MHKE.syouhin_mei ")                        '販売価格エラー情報T.商品名
            .AppendLine("   ,MHKE.tys_houhou_no ")                      '販売価格エラー情報T.調査方法NO
            .AppendLine("   ,MHKE.tys_houhou ")                         '販売価格エラー情報T.調査方法
            .AppendLine("   ,MHKE.torikesi ")                           '販売価格エラー情報T.取消
            .AppendLine("   ,MHKE.koumuten_seikyuu_gaku ")              '販売価格エラー情報T.工務店請求金額
            .AppendLine("   ,MHKE.koumuten_seikyuu_gaku_henkou_flg ")   '販売価格エラー情報T.工務店請求金額変更FLG
            .AppendLine("   ,MHKE.jitu_seikyuu_gaku ")                  '販売価格エラー情報T.実請求金額
            .AppendLine("   ,MHKE.jitu_seikyuu_gaku_henkou_flg ")       '販売価格エラー情報T.実請求金額変更FLG
            .AppendLine("   ,MHKE.koukai_flg ")                         '販売価格エラー情報T.公開フラグ
            .AppendLine("   ,MHKE.add_login_user_id ")                  '販売価格エラー情報T.登録ログインユーザID
            .AppendLine("   ,MHKE.add_datetime ")                       '販売価格エラー情報T.登録日時
            .AppendLine("   ,MHKE.upd_login_user_id ")                  '販売価格エラー情報T.更新ログインユーザID
            .AppendLine("   ,MHKE.upd_datetime ")                       '販売価格エラー情報T.更新日時
            .AppendLine(" FROM ")
            .AppendLine("   m_hanbai_kakaku_error MHKE WITH(READCOMMITTED) ")   '販売価格エラー情報T
            .AppendLine(" WHERE ")

            'EDI情報作成日
            .AppendLine(" MHKE.edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date ")
            paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdiJouhouSakuseiDate))

            '処理日時
            .AppendLine(" AND CONVERT(varchar(100),MHKE.syori_datetime,112) + REPLACE(CONVERT(varchar(100),MHKE.syori_datetime,114),':','') = @syori_datetime")
            paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 40, strSyoriDate))

            'ソート順
            .AppendLine(" ORDER BY ")
            .AppendLine("      MHKE.edi_jouhou_sakusei_date ")      '販売価格エラー情報T.EDI情報作成日
            .AppendLine("      ,MHKE.gyou_no ")                     '販売価格エラー情報T.行NO
        End With

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanbaiKakakuErr, _
                    dsHanbaiKakakuErr.HanbaiKakakuErrCSVTable.TableName, paramList.ToArray)

        Return dsHanbaiKakakuErr.HanbaiKakakuErrCSVTable

    End Function

    '====================2011/05/16 車龍 仕様変更 追加 開始↓===========================
    ''' <summary>販売価格マスタ個別設定データを取得する</summary>
    Public Function SelHanbaiKakakuKobeituSettei(ByVal strAiteSakiSyubetu As String, ByVal strAiteSakiCd As String, ByVal strSyouhinCd As String, ByVal strTyousaHouhouNo As String) As Data.DataTable

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
            .AppendLine("	,MHK.tys_houhou_no ")
            .AppendLine("	,MHK.torikesi ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MHK.koumuten_seikyuu_gaku),'') AS koumuten_seikyuu_gaku ")
            .AppendLine("	,ISNULL(MHK.koumuten_seikyuu_gaku_henkou_flg,0) AS koumuten_seikyuu_gaku_henkou_flg ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MHK.jitu_seikyuu_gaku),'') AS jitu_seikyuu_gaku ")
            .AppendLine("	,ISNULL(MHK.jitu_seikyuu_gaku_henkou_flg,0) AS jitu_seikyuu_gaku_henkou_flg ")
            .AppendLine("	,ISNULL(MHK.koukai_flg,0) AS koukai_flg ")
            .AppendLine("FROM  ")
            .AppendLine("	m_hanbai_kakaku AS MHK WITH(READCOMMITTED) ")
            .AppendLine("	LEFT OUTER JOIN  ")
            .AppendLine("	( ")
            .AppendLine("		SELECT  ")
            .AppendLine("		    0 AS aitesaki_syubetu  ")
            .AppendLine("		    ,'ALL' AS aitesaki_cd  ")
            .AppendLine("		    ,'相手先なし' AS aitesaki_mei  ")
            .AppendLine("		UNION ALL  ")
            .AppendLine("		SELECT  ")
            .AppendLine("		    1 AS aitesaki_syubetu  ")
            .AppendLine("		    ,kameiten_cd AS aitesaki_cd  ")
            .AppendLine("		    ,kameiten_mei1 AS aitesaki_mei  ")
            .AppendLine("		FROM  ")
            .AppendLine("		    m_kameiten WITH(READCOMMITTED)     ")
            .AppendLine("		UNION ALL  ")
            .AppendLine("		SELECT  ")
            .AppendLine("		    5 AS aitesaki_syubetu  ")
            .AppendLine("		    ,eigyousyo_cd AS aitesaki_cd  ")
            .AppendLine("		    ,eigyousyo_mei AS aitesaki_mei  ")
            .AppendLine("		FROM  ")
            .AppendLine("		    m_eigyousyo WITH(READCOMMITTED)     ")
            .AppendLine("		UNION ALL  ")
            .AppendLine("		SELECT  ")
            .AppendLine("		    7 AS aitesaki_syubetu  ")
            .AppendLine("		    ,keiretu_cd AS aitesaki_cd  ")
            .AppendLine("		    ,MIN(keiretu_mei) AS aitesaki_mei  ")
            .AppendLine("		FROM  ")
            .AppendLine("		    m_keiretu WITH(READCOMMITTED)     ")
            .AppendLine("		GROUP BY  ")
            .AppendLine("		    keiretu_cd  ")
            .AppendLine("	) SUB  ")
            .AppendLine("	ON ")
            .AppendLine("		MHK.aitesaki_syubetu = SUB.aitesaki_syubetu ")
            .AppendLine("		AND ")
            .AppendLine("		MHK.aitesaki_cd = SUB.aitesaki_cd ")
            .AppendLine("WHERE ")
            .AppendLine("	MHK.aitesaki_syubetu = @aitesaki_syubetu ")
            If Not strAiteSakiSyubetu.Trim.Equals("0") Then
                .AppendLine("	AND ")
                .AppendLine("	MHK.aitesaki_cd = @aitesaki_cd ")
            End If
            .AppendLine("	AND ")
            .AppendLine("	MHK.syouhin_cd = @syouhin_cd ")
            .AppendLine("	AND ")
            .AppendLine("	MHK.tys_houhou_no = @tys_houhou_no ")
        End With

        paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 4, strAiteSakiSyubetu))
        paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, strAiteSakiCd))
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))
        paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 4, strTyousaHouhouNo))

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtHanbaiKakakuKobeituSettei", paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function

    ''' <summary>販売価格マスタ個別設定の存在チェツク</summary>
    Public Function CheckSonzai(ByVal strAiteSakiSyubetu As String, ByVal strAiteSakiCd As String, ByVal strSyouhinCd As String, ByVal strTyousaHouhouNo As String) As Data.DataTable

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
            .AppendLine("	m_hanbai_kakaku WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	aitesaki_syubetu = @aitesaki_syubetu ")
            .AppendLine("	AND ")
            .AppendLine("	aitesaki_cd = @aitesaki_cd ")
            .AppendLine("	AND ")
            .AppendLine("	syouhin_cd = @syouhin_cd ")
            .AppendLine("	AND ")
            .AppendLine("	tys_houhou_no = @tys_houhou_no ")
        End With

        paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 4, strAiteSakiSyubetu))
        paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, strAiteSakiCd))
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))
        paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 4, strTyousaHouhouNo))

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtHanbaiKakakuKobeituSettei", paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function


    '====================2011/05/16 車龍 仕様変更 追加 終了↑===========================

End Class

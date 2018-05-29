Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper
''' <summary>加盟店物件情報照会する</summary>
''' <remarks>加盟店物件情報照会機能を提供する</remarks>
''' <history>
''' <para>2009/07/15　馬艶軍(大連情報システム部)　新規作成</para>
''' </history>
Public Class BukkenJyouhouInquiryDataAccess

    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary> 物件情報取得</summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <returns>物件情報のデータ</returns>
    Public Function SelBukkenJyouhouInfo(ByVal strKameitenCd As String) As BukkenJyouhouInquiryDataSet.BukkenJyouhouInquiryDataTableDataTable

        ' DataSetインスタンスの生成()
        Dim dsBukkenJyouhouInquiryDataSet As New BukkenJyouhouInquiryDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	MK.kameiten_cd, ")                              '加盟店ｺｰﾄﾞ	
            .AppendLine("	MK.kameiten_mei1, ")                            '加盟店名1	
            .AppendLine("	MK.tenmei_kana1, ")                             '店名ｶﾅ1	
            .AppendLine("	(MJM.DisplayName) AS eigyou_tantousya_mei, ")                     '営業担当者	
            .AppendLine("	(TJ.kbn + TJ.hosyousyo_no) AS hosyousyo_no, ")  '区分と保証書NO	
            .AppendLine("	TJ.sesyu_mei, ")                                '施主名	
            .AppendLine("	(ISNULL(TJ.bukken_jyuusyo1,'') + ISNULL(TJ.bukken_jyuusyo2,'') + ISNULL(TJ.bukken_jyuusyo3,'')) AS bukken_jyuusyo, ")    '物件住所123	
            .AppendLine("	CONVERT(VARCHAR(12),TJ.irai_date,111) AS irai_date, ")                                '依頼日	
            .AppendLine("	CONVERT(VARCHAR(12),TJ.syoudakusyo_tys_date,111) AS syoudakusyo_tys_date, ")                     '承諾書調査日	
            .AppendLine("	CONVERT(VARCHAR(12),TJ.tys_jissi_date,111) AS tys_jissi_date, ")                           '調査実施日	
            .AppendLine("	CONVERT(VARCHAR(12),TJ.keikakusyo_sakusei_date,111) AS keikakusyo_sakusei_date, ")                  '計画書作成日	
            .AppendLine("	CONVERT(VARCHAR(12),TJ.tys_hkks_hak_date,111) AS tys_hkks_hak_date, ")                        '調査報告書発送日	
            .AppendLine("	MT1.tantousya_mei, ")                           '担当者名	
            .AppendLine("	MKS.ks_siyou, ")                                '基礎仕様	
            .AppendLine("	kouhou = ")
            .AppendLine("	CASE WHEN koj_hantei_flg = '0' THEN '工事なし' ")
            .AppendLine("	ELSE MKKS.kairy_koj_syubetu ")
            .AppendLine("	END, ")                                         '改良工事種別	
            .AppendLine("	CONVERT(VARCHAR(12),TJ.kairy_koj_kanry_yotei_date,111) AS kairy_koj_kanry_yotei_date, ")               '改良工事完了予定日	
            .AppendLine("	CONVERT(VARCHAR(12),TJ.kairy_koj_date,111) AS kairy_koj_date, ")                           '改良工事日	
            .AppendLine("	CONVERT(VARCHAR(12),TJ.kairy_koj_sokuhou_tyk_date,111) AS kairy_koj_sokuhou_tyk_date, ")               '改良工事完工速報着日	
            .AppendLine("	CONVERT(VARCHAR(12),TJ.koj_hkks_hassou_date,111) AS koj_hkks_hassou_date, ")                     'K17報告書発送日	
            .AppendLine("	MT2.tantousya_mei AS kouji_tantousya_mei, ")    '担当者名	
            .AppendLine("	MHHJ.hosyousyo_hak_jyky, ")                     '保証書発行状況	
            .AppendLine("	CONVERT(VARCHAR(12),TJ.hosyousyo_hak_date,111) AS hosyousyo_hak_date, ")                       '保証書発行日	


            'isnull(TTS.uri_gaku,0)+isnull(TTS.syouhizei_gaku,0)

            '.AppendLine("	TBL1.uriagegaku, ")                             '売上額	
            .AppendLine("	isnull(TTS2.uri_gaku,0)+isnull(TTS2.syouhizei_gaku,0) as uriagegaku, ")

            .AppendLine("	TBL2.nyukingaku, ")                             '入金額
            .AppendLine("	(ISNULL(TJ.tys_kaisya_cd,'') + ISNULL(TJ.tys_kaisya_jigyousyo_cd,'')) AS tys_kaisya_cd, ")     '調査会社コード
            .AppendLine("	MTK1.tys_kaisya_mei AS tys_kaisya_mei, ")                          '調査会社名
            .AppendLine("	(ISNULL(TJ.koj_gaisya_cd,'') + ISNULL(TJ.koj_gaisya_jigyousyo_cd,'')) AS koj_gaisya_cd, ")     '工事会社コード
            .AppendLine("	MTK2.tys_kaisya_mei AS koj_gaisya_mei  ")                          '工事会社名
            .AppendLine("FROM ")
            .AppendLine("	m_kameiten MK WITH (READCOMMITTED) ")            '加盟店マスタ	
            .AppendLine("LEFT JOIN ")
            .AppendLine("	t_jiban TJ WITH (READCOMMITTED) ")               '地盤テーブル	
            .AppendLine("ON ")
            .AppendLine("	MK.kameiten_cd = TJ.kameiten_cd ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("  m_jhs_mailbox MJM WITH (READCOMMITTED) ")         '社員アカウント情報マスタ
            .AppendLine("ON ")
            .AppendLine("	MK.eigyou_tantousya_mei = MJM.PrimaryWindowsNTAccount ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_tantousya MT1 WITH (READCOMMITTED) ")          '担当者ﾏｽﾀ	
            .AppendLine("ON ")
            .AppendLine("	TJ.tantousya_cd = MT1.tantousya_cd ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_ks_siyou MKS WITH (READCOMMITTED) ")           '基礎仕様ﾏｽﾀ	
            .AppendLine("ON ")
            .AppendLine("	TJ.hantei_cd1 = MKS.ks_siyou_no ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_kairy_koj_syubetu MKKS WITH (READCOMMITTED) ") '改良工事種別ﾏｽﾀ	
            .AppendLine("ON ")
            .AppendLine("	TJ.kairy_koj_syubetu = MKKS.kairy_koj_syubetu_no ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_tantousya MT2 WITH (READCOMMITTED) ")          '担当者ﾏｽﾀ	
            .AppendLine("ON ")
            .AppendLine("	TJ.syouninsya_cd = MT2.tantousya_cd ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_hosyousyo_hak_jyky MHHJ WITH (READCOMMITTED) ")    '保証書発行状況ﾏｽﾀ	
            .AppendLine("ON ")
            .AppendLine("	TJ.hosyousyo_hak_jyky = MHHJ.hosyousyo_hak_jyky_no ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("    t_teibetu_seikyuu TTS2 ")
            .AppendLine("ON TJ.hosyousyo_no = TTS2.hosyousyo_no And TJ.kbn = TTS2.kbn")

            .AppendLine("LEFT JOIN ")
            .AppendLine("	(SELECT ")
            .AppendLine("		TJ1.kbn, ")
            .AppendLine("		TJ1.hosyousyo_no, ")
            .AppendLine("		TTS.seikyuu_umu, ")
            .AppendLine("		FLOOR(SUM(ISNULL(TTS.uri_gaku,0) * (ISNULL(MS.zeiritu,0)+1))) AS uriagegaku ")
            .AppendLine("	FROM ")
            .AppendLine("		t_jiban TJ1 WITH (READCOMMITTED) ")              '地盤テーブル	
            .AppendLine("	LEFT JOIN ")
            .AppendLine("		t_teibetu_seikyuu TTS WITH (READCOMMITTED) ")    '邸別請求テーブル	
            .AppendLine("	ON		 ")
            .AppendLine("		TJ1.hosyousyo_no = TTS.hosyousyo_no ")
            .AppendLine("	AND ")
            .AppendLine("		TJ1.kbn = TTS.kbn ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("		m_syouhizei MS WITH (READCOMMITTED) ")           '消費税マスタ	
            .AppendLine("	ON ")
            .AppendLine("		TTS.zei_kbn = MS.zei_kbn ")
            .AppendLine("	WHERE ")
            .AppendLine("		TTS.seikyuu_umu = '1' ")
            .AppendLine("	AND ")
            .AppendLine("		TJ1.kameiten_cd = @kameiten_cd ")
            .AppendLine("	GROUP BY ")
            .AppendLine("		TJ1.kbn, ")
            .AppendLine("		TJ1.hosyousyo_no, ")
            .AppendLine("		TTS.seikyuu_umu) TBL1 ")
            .AppendLine("ON ")
            .AppendLine("	TJ.hosyousyo_no = TBL1.hosyousyo_no ")
            .AppendLine("AND ")
            .AppendLine("	TJ.kbn = TBL1.kbn ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	(SELECT ")
            .AppendLine("		TJ2.kbn, ")
            .AppendLine("		TJ2.hosyousyo_no, ")
            .AppendLine("		FLOOR(SUM(ISNULL(TTN.zeikomi_nyuukin_gaku,0))) AS nyukingaku ")
            .AppendLine("	FROM ")
            .AppendLine("		t_jiban TJ2 WITH (READCOMMITTED) ")              '地盤テーブル	
            .AppendLine("	LEFT JOIN ")
            .AppendLine("		t_teibetu_nyuukin TTN WITH (READCOMMITTED) ")    '邸別入金テーブル	
            .AppendLine("	ON ")
            .AppendLine("		TJ2.hosyousyo_no = TTN.hosyousyo_no ")
            .AppendLine("	AND ")
            .AppendLine("		TJ2.kbn = TTN.kbn ")
            .AppendLine("	WHERE ")
            .AppendLine("		TJ2.kameiten_cd = @kameiten_cd ")
            .AppendLine("	GROUP BY ")
            .AppendLine("		TJ2.kbn, ")
            .AppendLine("		TJ2.hosyousyo_no) TBL2 ")
            .AppendLine("ON ")
            .AppendLine("	TJ.hosyousyo_no = TBL2.hosyousyo_no ")
            .AppendLine("AND ")
            .AppendLine("	TJ.kbn = TBL2.kbn ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_tyousakaisya MTK1 WITH (READCOMMITTED) ")           '調査会社マスタ
            .AppendLine("ON ")
            .AppendLine("	TJ.tys_kaisya_cd = MTK1.tys_kaisya_cd ")
            .AppendLine("AND ")
            .AppendLine("	TJ.tys_kaisya_jigyousyo_cd = MTK1.jigyousyo_cd ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_tyousakaisya MTK2 WITH (READCOMMITTED) ")           '調査会社マスタ
            .AppendLine("ON ")
            .AppendLine("	TJ.koj_gaisya_cd = MTK2.tys_kaisya_cd ")
            .AppendLine("AND ")
            .AppendLine("	TJ.koj_gaisya_jigyousyo_cd = MTK2.jigyousyo_cd ")
            .AppendLine("WHERE ")
            .AppendLine("	MK.kameiten_cd = @kameiten_cd ")
            .AppendLine("AND ")
            .AppendLine("   TJ.kameiten_cd = @kameiten_cd ")
            '邸別請求テーブル.請求有無=1
            .AppendLine("AND ")
            .AppendLine("   TBL1.seikyuu_umu = '1' ")
            '地盤テーブル.ﾃﾞｰﾀ破棄種別=0
            .AppendLine("AND ")
            .AppendLine("   TJ.data_haki_syubetu = '0' ")
            '地盤テーブル.債権対象外FLG=1以外
            .AppendLine("AND ")
            .AppendLine("	(TJ.saiken_taisyougai_flg <> '1' ")
            .AppendLine("       OR TJ.saiken_taisyougai_flg IS NULL) ")
            .AppendLine("AND ")
            '売上額>=入金額
            .AppendLine("	(((TBL1.uriagegaku > TBL2.nyukingaku) OR (TBL1.uriagegaku IS NULL AND TBL2.nyukingaku IS NULL)) ")
            .AppendLine("   OR ")
            '保証書発行日IS NULL
            .AppendLine("   TJ.hosyousyo_hak_date IS NULL) ")
            .AppendLine("ORDER BY ")
            .AppendLine("   irai_date DESC")

        End With

        'パラメータの設定
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsBukkenJyouhouInquiryDataSet, _
                    dsBukkenJyouhouInquiryDataSet.BukkenJyouhouInquiryDataTable.TableName, paramList.ToArray)

        Return dsBukkenJyouhouInquiryDataSet.BukkenJyouhouInquiryDataTable

    End Function

    ''' <summary>
    ''' 「取消」情報を取得する
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <returns>「取消」データ</returns>
    ''' <hidtory>2012/03/28 車龍 405721案件の対応 追加</hidtory>
    Public Function SelTorikesi(ByVal strKameitenCd As String) As Data.DataTable

        Dim dsReturn As New DataSet

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	MK.torikesi ")
            .AppendLine("	,ISNULL(MKM.meisyou,'') AS meisyou ")
            .AppendLine("FROM ")
            .AppendLine("	m_kameiten AS MK WITH(READCOMMITTED) ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_kakutyou_meisyou AS MKM WITH(READCOMMITTED) ")
            .AppendLine("	ON ")
            .AppendLine("		MKM.meisyou_syubetu = 56 ")
            .AppendLine("		AND ")
            .AppendLine("		MK.torikesi = MKM.code ")
            .AppendLine("WHERE ")
            .AppendLine("	MK.kameiten_cd = @kameiten_cd ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtTorikesi", paramList.ToArray)

        Return dsReturn.Tables("dtTorikesi")

    End Function

End Class

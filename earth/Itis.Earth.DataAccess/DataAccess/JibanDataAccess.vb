Imports System
Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' 地盤データの取得クラスです
''' </summary>
''' <remarks></remarks>
Public Class JibanDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "メンバ変数"
    Dim connStr As String = ConnectionManager.Instance.ConnectionString
    Dim strLogic As New StringLogic
    Dim cmnDtAcc As New CmnDataAccess
#End Region

#Region "列挙型"
    ''' <summary>
    ''' 更新対象アイテム名
    ''' </summary>
    ''' <remarks></remarks>
    Enum enumItemName
        ''' <summary>
        ''' 施主名
        ''' </summary>
        ''' <remarks></remarks>
        SesyuMei = 1
        ''' <summary>
        ''' 住所
        ''' </summary>
        ''' <remarks></remarks>
        Juusyo = 2
        ''' <summary>
        ''' 加盟店コード
        ''' </summary>
        ''' <remarks></remarks>
        KameitenCd = 3
        ''' <summary>
        ''' 調査会社
        ''' </summary>
        ''' <remarks></remarks>
        TyousaKaisya = 4
        ''' <summary>
        ''' 備考
        ''' </summary>
        ''' <remarks></remarks>
        Bikou = 5
        ''' <summary>
        ''' 備考2
        ''' </summary>
        ''' <remarks></remarks>
        Bikou2 = 6
    End Enum

#End Region

#Region "地盤データの取得"
    ''' <summary>
    ''' 地盤レコードを取得します
    ''' </summary>
    ''' <param name="kbn">区分</param>
    ''' <param name="hosyousyoNo">保証書NO</param>   
    ''' <param name="isNotDataHaki">データ破棄種別判断フラグ</param>
    ''' <returns>地盤データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetJibanData(ByVal kbn As String, _
                                 ByVal hosyousyoNo As String, _
                        Optional ByVal isNotDataHaki As Boolean = False) As JibanDataSet.JibanTableDataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetJibanData", _
                                            kbn, _
                                            hosyousyoNo, _
                                            isNotDataHaki)

        ' パラメータ
        Const strParamKbn As String = "@KBN"
        Const strParamHosyousyoNo As String = "@HOSYOUSYONO"

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKbn, SqlDbType.Char, 1, kbn), _
             SQLHelper.MakeParam(strParamHosyousyoNo, SqlDbType.VarChar, 10, hosyousyoNo)}

        Dim commandTextSb As New StringBuilder

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("   j.kbn, ")
        commandTextSb.Append("   j.hosyousyo_no, ")
        commandTextSb.Append("   j.data_haki_syubetu, ")
        commandTextSb.Append("   j.data_haki_date, ")
        commandTextSb.Append("   j.sesyu_mei, ")
        commandTextSb.Append("   j.jutyuu_bukken_mei, ")
        commandTextSb.Append("   j.bukken_jyuusyo1, ")
        commandTextSb.Append("   j.bukken_jyuusyo2, ")
        commandTextSb.Append("   j.bukken_jyuusyo3, ")
        commandTextSb.Append("   j.bunjou_cd, ")
        commandTextSb.Append("   j.bukken_nayose_cd, ")
        commandTextSb.Append("   j.kameiten_cd, ")
        commandTextSb.Append("   j.syouhin_kbn, ")
        commandTextSb.Append("   j.bikou, ")
        commandTextSb.Append("   j.irai_date, ")
        commandTextSb.Append("   j.irai_tantousya_mei, ")
        commandTextSb.Append("   j.keiyaku_no, ")
        commandTextSb.Append("   j.th_kasi_umu, ")
        commandTextSb.Append("   j.kaisou, ")
        commandTextSb.Append("   j.sintiku_tatekae, ")
        commandTextSb.Append("   j.kouzou, ")
        commandTextSb.Append("   j.kouzou_memo, ")
        commandTextSb.Append("   j.syako, ")
        commandTextSb.Append("   j.negiri_hukasa, ")
        commandTextSb.Append("   j.yotei_morituti_atusa, ")
        commandTextSb.Append("   j.yotei_ks, ")
        commandTextSb.Append("   j.yotei_ks_memo, ")
        commandTextSb.Append("   j.tys_kaisya_cd, ")
        commandTextSb.Append("   j.tys_kaisya_jigyousyo_cd, ")
        commandTextSb.Append("   j.tys_houhou, ")
        commandTextSb.Append("   j.tys_gaiyou, ")
        commandTextSb.Append("   j.fc_builder_hanbai_gaku, ")
        commandTextSb.Append("   j.tys_kibou_date, ")
        commandTextSb.Append("   j.tys_kibou_jikan, ")
        commandTextSb.Append("   j.tatiai_umu, ")
        commandTextSb.Append("   j.tatiaisya_cd, ")
        commandTextSb.Append("   j.tenpu_heimenzu, ")
        commandTextSb.Append("   j.tenpu_ritumenzu, ")
        commandTextSb.Append("   j.tenpu_ks_husezu, ")
        commandTextSb.Append("   j.tenpu_danmenzu, ")
        commandTextSb.Append("   j.tenpu_kukeizu, ")
        commandTextSb.Append("   j.syoudakusyo_tys_date, ")
        commandTextSb.Append("   j.tys_jissi_date, ")
        commandTextSb.Append("   j.dositu, ")
        commandTextSb.Append("   j.kyoyou_sijiryoku, ")
        commandTextSb.Append("   j.hantei_cd1, ")
        commandTextSb.Append("   j.hantei_cd2, ")
        commandTextSb.Append("   j.hantei_setuzoku_moji, ")
        commandTextSb.Append("   j.tantousya_cd, ")
        commandTextSb.Append("   t1.tantousya_mei As tantousya_mei, ")
        commandTextSb.Append("   j.syouninsya_cd, ")
        commandTextSb.Append("   t2.tantousya_mei As syouninsya_mei, ")
        commandTextSb.Append("   j.keikakusyo_sakusei_date, ")
        commandTextSb.Append("   j.ks_danmen_cd, ")
        commandTextSb.Append("   j.danmenzu_setumei, ")
        commandTextSb.Append("   j.kousatu, ")
        commandTextSb.Append("   j.ks_hkks_umu, ")
        commandTextSb.Append("   j.ks_koj_kanry_hkks_tyk_date, ")
        commandTextSb.Append("   j.hosyousyo_hak_jyky, ")
        commandTextSb.Append("   j.hosyousyo_hak_jyky_settei_date, ")
        commandTextSb.Append("   j.hosyousyo_hak_date, ")
        commandTextSb.Append("   j.insatu_hosyousyo_hak_date, ")
        commandTextSb.Append("   j.hosyou_umu, ")
        commandTextSb.Append("   j.hosyou_kaisi_date, ")
        commandTextSb.Append("   j.hosyou_kikan, ")
        commandTextSb.Append("   j.hosyou_nasi_riyuu, ")
        commandTextSb.Append("   j.hosyou_syouhin_umu, ")
        commandTextSb.Append("   j.hoken_kaisya, ")
        commandTextSb.Append("   j.hoken_sinsei_tuki, ")
        commandTextSb.Append("   j.hosyousyo_saihak_date, ")
        commandTextSb.Append("   j.tys_hkks_umu, ")
        commandTextSb.Append("   j.tys_hkks_jyuri_syousai, ")
        commandTextSb.Append("   j.tys_hkks_jyuri_date, ")
        commandTextSb.Append("   j.tys_hkks_hak_date, ")
        commandTextSb.Append("   j.tys_hkks_saihak_date, ")
        commandTextSb.Append("   j.koj_hkks_umu, ")
        commandTextSb.Append("   j.koj_hkks_juri_syousai, ")
        commandTextSb.Append("   j.koj_hkks_juri_date, ")
        commandTextSb.Append("   j.koj_hkks_hassou_date, ")
        commandTextSb.Append("   j.koj_hkks_saihak_date, ")
        commandTextSb.Append("   j.koj_gaisya_cd, ")
        commandTextSb.Append("   j.koj_gaisya_jigyousyo_cd, ")
        commandTextSb.Append("   j.kairy_koj_syubetu, ")
        commandTextSb.Append("   j.kairy_koj_kanry_yotei_date, ")
        commandTextSb.Append("   j.kairy_koj_date, ")
        commandTextSb.Append("   j.kairy_koj_sokuhou_tyk_date, ")
        commandTextSb.Append("   j.t_koj_kaisya_cd, ")
        commandTextSb.Append("   j.t_koj_kaisya_jigyousyo_cd, ")
        commandTextSb.Append("   j.t_koj_syubetu, ")
        commandTextSb.Append("   j.t_koj_kanry_yotei_date, ")
        commandTextSb.Append("   j.t_koj_date, ")
        commandTextSb.Append("   j.t_koj_sokuhou_tyk_date, ")
        commandTextSb.Append("   j.tys_kekka_add_datetime, ")
        commandTextSb.Append("   j.tys_kekka_upd_datetime, ")
        commandTextSb.Append("   j.douji_irai_tousuu, ")
        commandTextSb.Append("   j.hoken_sinsei_kbn, ")
        commandTextSb.Append("   j.kasi_umu, ")
        commandTextSb.Append("   j.koj_gaisya_seikyuu_umu, ")
        commandTextSb.Append("   j.henkin_syori_flg, ")
        commandTextSb.Append("   j.henkin_syori_date, ")
        commandTextSb.Append("   j.koj_tantousya_mei, ")
        commandTextSb.Append("   j.keiyu, ")
        commandTextSb.Append("   j.koj_siyou_kakunin, ")
        commandTextSb.Append("   j.koj_siyou_kakunin_date, ")
        commandTextSb.Append("   j.hosyousyo_hak_iraisyo_umu, ")
        commandTextSb.Append("   j.hosyousyo_hak_iraisyo_tyk_date, ")
        commandTextSb.Append("   j.sekkei_kyoyou_sijiryoku, ")
        commandTextSb.Append("   j.irai_yotei_tousuu, ")
        commandTextSb.Append("   j.tatemono_youto_no, ")
        commandTextSb.Append("   j.kosuu, ")
        commandTextSb.Append("   j.kousinsya, ")
        commandTextSb.Append("   j.tys_renrakusaki_atesaki_mei, ")
        commandTextSb.Append("   j.tys_renrakusaki_tel, ")
        commandTextSb.Append("   j.tys_renrakusaki_fax, ")
        commandTextSb.Append("   j.tys_renrakusaki_mail, ")
        commandTextSb.Append("   j.tys_renrakusaki_tantou_mei, ")
        commandTextSb.Append("   j.add_login_user_id, ")
        commandTextSb.Append("   j.add_datetime, ")
        commandTextSb.Append("   ISNULL(j.upd_login_user_id,j.add_login_user_id) AS upd_login_user_id , ")
        commandTextSb.Append("   ISNULL(j.upd_datetime,j.add_datetime) AS upd_datetime , ")
        commandTextSb.Append("   j.t_koj_kaisya_seikyuu_umu, ")
        commandTextSb.Append("   j.bikou2, ")
        commandTextSb.Append("   j.syasin_jyuri, ")
        commandTextSb.Append("   j.syasin_comment, ")
        commandTextSb.Append("   r.t_houkoku_syounin  AS t_houkoku_syounin,")
        commandTextSb.Append("   r.status  AS status,")
        commandTextSb.Append("   hs.hosyousyo_hak_date  AS hs_hosyousyo_hak_date,")
        commandTextSb.Append("   hs.hkks_hassou_date  AS hs_hkks_hassou_date, ")
        commandTextSb.Append("   m.meisyou As nyuukin_kakunin_jyouken_mei, ")
        commandTextSb.Append("   hj.hkks_jyuri_jyky,  ")
        commandTextSb.Append("   ks.kairy_koj_syubetu As kairy_koj_syubetu_mei, ")
        commandTextSb.Append("   k.hansokuhin_seikyuusaki, ")
        commandTextSb.Append("   k.tys_seikyuu_saki, ")
        commandTextSb.Append("   j.fuho_syoumeisyo_flg, ")
        commandTextSb.Append("   j.fuho_syoumeisyo_hassou_date, ")
        commandTextSb.Append("   j.yoyaku_zumi_flg, ")
        commandTextSb.Append("   j.annaizu, ")
        commandTextSb.Append("   j.haitizu, ")
        commandTextSb.Append("   j.kakukai_heimenzu, ")
        commandTextSb.Append("   j.ks_husezu, ")
        commandTextSb.Append("   j.ks_danmenzu, ")
        commandTextSb.Append("   j.zousei_keikakuzu, ")
        commandTextSb.Append("   j.ks_tyakkou_yotei_from_date, ")
        commandTextSb.Append("   j.ks_tyakkou_yotei_to_date, ")
        commandTextSb.Append("   j.sinki_touroku_moto_kbn, ")
        commandTextSb.Append("   j.koj_hantei_kekka_flg, ")
        commandTextSb.Append("   k.keiretu_cd, ")
        commandTextSb.Append("   k.eigyousyo_cd, ")
        commandTextSb.Append("   j.sesyu_mei_umu, ")
        commandTextSb.Append("   j.henkou_yotei_kameiten_cd, ")
        commandTextSb.Append("   j.hosyou_nasi_riyuu_cd, ")
        commandTextSb.Append("   j.hak_irai_uke_datetime, ")
        commandTextSb.Append("   j.hak_irai_can_datetime, ")
        commandTextSb.Append("   j.hosyousyo_hassou_date, ")
        commandTextSb.Append("   j.hosyousyo_hak_houhou ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("    t_jiban j  WITH (READCOMMITTED) ")
        commandTextSb.Append(" LEFT OUTER JOIN  ")
        commandTextSb.Append("    ReportIF r  WITH (READCOMMITTED) ")
        commandTextSb.Append(" ON  ")
        commandTextSb.Append("    j.kbn + j.hosyousyo_no = r.kokyaku_no ")
        commandTextSb.Append(" LEFT OUTER JOIN  ")
        commandTextSb.Append("    m_kameiten k  WITH (READCOMMITTED) ")
        commandTextSb.Append(" ON ")
        commandTextSb.Append("    j.kameiten_cd = k.kameiten_cd ")
        'commandTextSb.Append(" AND ")
        'commandTextSb.Append("    k.torikesi = 0 ")
        commandTextSb.Append(" LEFT OUTER JOIN ")
        commandTextSb.Append("    m_meisyou m  WITH (READCOMMITTED) ")
        commandTextSb.Append(" ON ")
        commandTextSb.Append("    m.meisyou_syubetu = '05' ")
        commandTextSb.Append(" AND ")
        commandTextSb.Append("    m.code = k.nyuukin_kakunin_jyouken ")
        commandTextSb.Append(" LEFT OUTER JOIN  ")
        commandTextSb.Append("    m_tantousya t1  WITH (READCOMMITTED) ")
        commandTextSb.Append(" ON  ")
        commandTextSb.Append("    j.tantousya_cd = t1.tantousya_cd ")
        commandTextSb.Append(" LEFT OUTER JOIN  ")
        commandTextSb.Append("    m_tantousya t2  WITH (READCOMMITTED) ")
        commandTextSb.Append(" ON  ")
        commandTextSb.Append("    j.syouninsya_cd = t2.tantousya_cd ")
        commandTextSb.Append(" LEFT OUTER JOIN  ")
        commandTextSb.Append("    m_hiduke_save hs  WITH (READCOMMITTED) ")
        commandTextSb.Append(" ON  ")
        commandTextSb.Append("    j.kbn = hs.kbn ")
        commandTextSb.Append(" LEFT OUTER JOIN  ")
        commandTextSb.Append("    m_hkks_juri hj  WITH (READCOMMITTED) ")
        commandTextSb.Append(" ON  ")
        commandTextSb.Append("    hj.hkks_jyuri_no = j.koj_hkks_umu ")
        commandTextSb.Append(" LEFT OUTER JOIN  ")
        commandTextSb.Append("    m_kairy_koj_syubetu ks  WITH (READCOMMITTED) ")
        commandTextSb.Append(" ON  ")
        commandTextSb.Append("    ks.kairy_koj_syubetu_no = j.kairy_koj_syubetu ")
        commandTextSb.Append(" WHERE ")
        commandTextSb.Append("    j.kbn = " & strParamKbn)
        commandTextSb.Append(" AND ")
        commandTextSb.Append("    j.hosyousyo_no = " & strParamHosyousyoNo)
        If isNotDataHaki Then
            'データ破棄種別が設定されていないものだけ取得する場合
            commandTextSb.Append(" AND")
            commandTextSb.Append("    j.data_haki_syubetu = 0")
        End If
        commandTextSb.Append(" ORDER BY r.kokyaku_brc DESC ")

        ' データの取得
        Dim JibanDataSet As New JibanDataSet()

        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString, _
            JibanDataSet, JibanDataSet.JibanTable.TableName, commandParameters)

        Dim JibanTable As JibanDataSet.JibanTableDataTable = JibanDataSet.JibanTable

        Return JibanTable

    End Function

#Region "物件検索用データの取得"

    ''' <summary>
    ''' 地盤レコードを取得します(物件検索用)
    ''' </summary>
    ''' <param name="keyRecAcc">地盤データ検索条件クラス</param>
    ''' <returns>地盤検索結果データテーブル</returns>
    ''' <remarks>検索条件にしたい情報のみ地盤データ検索条件クラスに設定し、渡して下さい</remarks>
    Public Function GetJibanSearchData(ByVal keyRecAcc As JibanSearchKeyRecord) As JibanDataSet.JibanSearchTableDataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetJibanData", _
                                            keyRecAcc)

        ' パラメータ設定用
        Dim commandParameters() As SqlParameter = Nothing

        ' SQL文
        Dim commandText As String = getSqlCondition(commandParameters, keyRecAcc)

        ' データの取得
        Dim JibanDataSet As New JibanDataSet()

        ' パラメータの有無により実行
        If commandParameters.Length > 0 Then
            SQLHelper.FillDataset(connStr, CommandType.Text, commandText, _
                JibanDataSet, JibanDataSet.JibanSearchTable.TableName, commandParameters)
        Else
            SQLHelper.FillDataset(connStr, CommandType.Text, commandText, _
                JibanDataSet, JibanDataSet.JibanSearchTable.TableName)
        End If

        Dim JibanSearchTable As JibanDataSet.JibanSearchTableDataTable = JibanDataSet.JibanSearchTable

        Return JibanSearchTable

    End Function

    ''' <summary>
    ''' 地盤レコード取得用のSQL文を生成します(物件検索用)
    ''' </summary>
    ''' <param name="sql_params">SQLパラメータ配列</param> 
    ''' <param name="keyRecAcc">地盤データ検索条件クラス</param>
    ''' <returns>検索用のSQL文</returns>
    ''' <remarks></remarks>
    Public Function getSqlCondition(ByRef sql_params As SqlParameter(), ByVal keyRecAcc As JibanSearchKeyRecord) As String

        Dim listParams As New List(Of ParamRecord)

        Dim sql As String = ""
        Dim sqlCondition As New StringBuilder
        Dim strKoujiSQL As String = "@KOUJISQL"

        ' 検索の基本となるSQL文

        Dim baseSql As String = " SELECT " & _
                                "   j.kbn " & _
                                "   , j.hosyousyo_no " & _
                                "   , h.haki_syubetu As data_haki_syubetu " & _
                                "   , j.sesyu_mei " & _
                                "   , j.bukken_jyuusyo1 " & _
                                "   , j.bukken_jyuusyo2 " & _
                                "   , j.bukken_jyuusyo3 " & _
                                "   , j.kameiten_cd " & _
                                "   , k.torikesi AS kt_torikesi " & _
                                "   , km.meisyou AS kt_torikesi_riyuu " & _
                                "   , k.kameiten_mei1 " & _
                                "   , j.tys_jissi_date " & _
                                "   , j.irai_tantousya_mei " & _
                                "   , j.irai_date " & _
                                "   , j.tys_kibou_date " & _
                                "   , j.yoyaku_zumi_flg " & _
                                "   , j.tys_kaisya_cd " & _
                                "   , j.tys_kaisya_jigyousyo_cd " & _
                                "   , tm.tys_kaisya_mei " & _
                                "   , thm.tys_houhou_mei " & _
                                "   , j.syoudakusyo_tys_date " & _
                                "   , t.koumuten_seikyuu_gaku " & _
                                "   , t.uri_gaku " & _
                                "   , t.siire_gaku " & _
                                "   , ttmi.tantousya_mei As tantousya_mei " & _
                                "   , ttmk.tantousya_mei As syouninsya_mei " & _
                                "   , ksm1.ks_siyou As hantei1 " & _
                                "   , kss.ks_siyou_setuzokusi As hantei_setuzoku_moji " & _
                                "   , ksm2.ks_siyou As hantei2 " & _
                                "   , j.keikakusyo_sakusei_date " & _
                                "   , j.hosyousyo_hak_date " & _
                                "   , j.bikou " & _
                                "   , jmm.DisplayName As eigyou_tantousya_mei " & _
                                "   , tk.uri_date AS koj_uri_date " & _
                                "   , j.bunjou_cd " & _
                                "   , j.bukken_nayose_cd " & _
                                "   , j.keiyaku_no " & _
                                " FROM " & _
                                "   t_jiban j  " & _
                                "   LEFT OUTER JOIN m_data_haki h  " & _
                                "     ON j.data_haki_syubetu = h.data_haki_no  " & _
                                "   LEFT OUTER JOIN m_kameiten k  " & _
                                "     ON j.kameiten_cd = k.kameiten_cd  " & _
                                "   LEFT OUTER JOIN m_jhs_mailbox jmm  " & _
                                "     ON k.eigyou_tantousya_mei = jmm.PrimaryWindowsNTAccount  " & _
                                "   LEFT OUTER JOIN m_tyousakaisya tm  " & _
                                "     ON j.tys_kaisya_cd = tm.tys_kaisya_cd  " & _
                                "     AND j.tys_kaisya_jigyousyo_cd = tm.jigyousyo_cd  " & _
                                "   LEFT OUTER JOIN m_tyousahouhou thm  " & _
                                "     ON j.tys_houhou = thm.tys_houhou_no  " & _
                                "   LEFT OUTER JOIN m_tantousya ttmi  " & _
                                "     ON j.tantousya_cd = ttmi.tantousya_cd  " & _
                                "   LEFT OUTER JOIN m_tantousya ttmk  " & _
                                "     ON j.syouninsya_cd = ttmk.tantousya_cd  " & _
                                "   LEFT OUTER JOIN m_ks_siyou ksm1  " & _
                                "     ON j.hantei_cd1 = ksm1.ks_siyou_no  " & _
                                "   LEFT OUTER JOIN m_ks_siyou ksm2  " & _
                                "     ON j.hantei_cd2 = ksm2.ks_siyou_no  " & _
                                "   LEFT OUTER JOIN m_ks_siyou_setuzokusi kss  " & _
                                "     ON j.hantei_setuzoku_moji = kss.ks_siyou_setuzokusi_no  " & _
                                "   LEFT OUTER JOIN (  " & _
                                "     SELECT " & _
                                "       kbn " & _
                                "       , hosyousyo_no " & _
                                "       , SUM(koumuten_seikyuu_gaku) As koumuten_seikyuu_gaku " & _
                                "       , SUM(uri_gaku) As uri_gaku " & _
                                "       , SUM(siire_gaku) As siire_gaku  " & _
                                "     FROM " & _
                                "       t_teibetu_seikyuu  " & _
                                "     WHERE " & _
                                "       bunrui_cd IN ('100', '110', '115', '120')  " & _
                                "       @TSEIKYUU1JOUKEN" & _
                                "     GROUP BY " & _
                                "       kbn " & _
                                "       , hosyousyo_no " & _
                                "   ) t  " & _
                                "     ON j.kbn = t.kbn  " & _
                                "     AND j.hosyousyo_no = t.hosyousyo_no  " & _
                                "   LEFT OUTER JOIN t_teibetu_seikyuu tk  " & _
                                "     ON j.kbn = tk.kbn  " & _
                                "     AND j.hosyousyo_no = tk.hosyousyo_no  " & _
                                "     AND tk.bunrui_cd = '130'  " & _
                                "   LEFT OUTER JOIN m_kakutyou_meisyou AS km  " & _
                                "     ON km.meisyou_syubetu = 56 " & _
                                "     AND km.code = CAST(k.torikesi AS VARCHAR(10))  " & _
                                "   LEFT OUTER JOIN m_todoufuken AS tf  " & _
                                "     ON tf.todouhuken_cd = k.todouhuken_cd " & _
                                " WHERE 0 = 0 " & _
                                " {0} " & _
                                " ORDER BY " & _
                                "   j.kbn " & _
                                "   , j.hosyousyo_no " & _
                                "   , h.haki_syubetu " & _
                                "   , k.kameiten_mei1 " & _
                                "   , j.sesyu_mei "

        '***********************************************************************
        ' 区分条件の設定
        '***********************************************************************
        '区分条件SQL格納用
        Dim tmpKbnNoJouken As String = String.Empty
        '区分指定条件格納リスト
        Dim listKbn As New List(Of String)

        ' 区分_1の判定
        If IIf(keyRecAcc.Kbn1 Is Nothing, String.Empty, keyRecAcc.Kbn1) IsNot String.Empty Then
            listKbn.Add(keyRecAcc.Kbn1)
        End If
        ' 区分_2の判定
        If IIf(keyRecAcc.Kbn2 Is Nothing, String.Empty, keyRecAcc.Kbn2) IsNot String.Empty Then
            listKbn.Add(keyRecAcc.Kbn2)
        End If
        ' 区分_3の判定
        If IIf(keyRecAcc.Kbn3 Is Nothing, String.Empty, keyRecAcc.Kbn3) IsNot String.Empty Then
            listKbn.Add(keyRecAcc.Kbn3)
        End If

        'SQL文生成
        tmpKbnNoJouken &= (" AND ")
        tmpKbnNoJouken &= (" j.kbn IN ( ")

        If listKbn.Count = 0 Then
            '条件が設定されていない場合、全区分を対象
            tmpKbnNoJouken &= (" SELECT kbn FROM m_data_kbn ")
        ElseIf listKbn.Count >= 1 Then
            '条件がひとつ以上設定されている場合
            If listKbn.Count = 1 Then
                '条件がひとつだけの場合、何故かパフォーマンスが低下するので、
                '必ずオマケの条件として同じ値を追加する
                listKbn.Add(listKbn(0))
            End If

            Dim strParamKbn As String = "@KUBUN"
            Dim ki As Integer = 0
            '条件の数だけパラメータ設定＆SQL生成のループ
            For ki = 0 To listKbn.Count - 1
                strParamKbn &= (ki + 1)
                tmpKbnNoJouken &= (strParamKbn)
                listParams.Add(getParamRecord(strParamKbn, SqlDbType.Char, 1, listKbn(ki)))

                If ki + 1 < listKbn.Count Then
                    '条件が最後の一つではない場合、結合符号「,」を追加
                    tmpKbnNoJouken &= (",")
                End If
            Next
        End If

        tmpKbnNoJouken &= (" ) ")

        '***********************************************************************
        ' 東_西日本フラグの設定
        '***********************************************************************
        ' 東_西日本フラグの判定
        If Not IIf(keyRecAcc.TouzaiFlg Is Nothing, String.Empty, keyRecAcc.TouzaiFlg) Is String.Empty Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@TOUZAIFLG", SqlDbType.VarChar, 1, keyRecAcc.TouzaiFlg))
            sqlCondition.Append(" tf.touzai_flg = @TOUZAIFLG ")
        End If

        Dim irai_date_flg As Boolean = False

        '***********************************************************************
        ' 保証書対象範囲条件（過去２年条件）
        '***********************************************************************
        ' 保証書対象範囲の判定
        If keyRecAcc.HosyousyoNoHani = 1 And _
          (keyRecAcc.HosyousyoNoFrom Is String.Empty And keyRecAcc.HosyousyoNoTo Is String.Empty) Then

            ' 現在日時の２年前をKeyとする
            Dim key As String = Date.Now.AddYears(-2).Year.ToString("0000") & "010000"

            tmpKbnNoJouken &= (" AND ")

            listParams.Add(getParamRecord("@HOSYOUSYONOHANI", SqlDbType.VarChar, 10, key))
            tmpKbnNoJouken &= (" j.hosyousyo_no >= @HOSYOUSYONOHANI ")
            tmpKbnNoJouken &= (" AND LEN(j.hosyousyo_no) = 10 ")
            irai_date_flg = True
        End If

        '***********************************************************************
        ' 保証書範囲条件の設定
        '***********************************************************************
        ' 一つでも設定されている場合、条件を作成
        If Not IIf(keyRecAcc.HosyousyoNoFrom Is Nothing, String.Empty, keyRecAcc.HosyousyoNoFrom) Is String.Empty Or _
           Not IIf(keyRecAcc.HosyousyoNoTo Is Nothing, String.Empty, keyRecAcc.HosyousyoNoTo) Is String.Empty Then

            tmpKbnNoJouken &= (" AND ")

            If Not keyRecAcc.HosyousyoNoFrom Is String.Empty And _
               Not keyRecAcc.HosyousyoNoTo Is String.Empty Then
                ' 両方指定有りはBETWEEN
                tmpKbnNoJouken &= (" j.hosyousyo_no BETWEEN @HOSYOUSYONOFROM AND @HOSYOUSYONOTO")
                listParams.Add(getParamRecord("@HOSYOUSYONOFROM", SqlDbType.VarChar, 10, keyRecAcc.HosyousyoNoFrom))
                listParams.Add(getParamRecord("@HOSYOUSYONOTO", SqlDbType.VarChar, 10, keyRecAcc.HosyousyoNoTo))
            Else
                If Not keyRecAcc.HosyousyoNoFrom Is String.Empty Then
                    ' 保証書Fromのみ
                    tmpKbnNoJouken &= (" j.hosyousyo_no >= @HOSYOUSYONOFROM ")
                    listParams.Add(getParamRecord("@HOSYOUSYONOFROM", SqlDbType.VarChar, 10, keyRecAcc.HosyousyoNoFrom))
                Else
                    ' 保証書Toのみ
                    tmpKbnNoJouken &= (" j.hosyousyo_no <= @HOSYOUSYONOTO ")
                    listParams.Add(getParamRecord("@HOSYOUSYONOTO", SqlDbType.VarChar, 10, keyRecAcc.HosyousyoNoTo))
                End If
            End If

            If irai_date_flg = False Then
                tmpKbnNoJouken &= (" AND LEN(j.hosyousyo_no) = 10 ")
                'sql_condition.Append(" AND j.irai_date IS NOT NULL ")
            End If
        End If

        '区分と番号のWHERE条件をセット
        sqlCondition.Append(tmpKbnNoJouken)
        '区分と番号のWHERE条件を、邸別請求テーブルの条件にもセット
        baseSql = baseSql.Replace("@TSEIKYUU1JOUKEN", tmpKbnNoJouken.Replace("j.", ""))

        '***********************************************************************
        ' 加盟店コードの設定
        '***********************************************************************
        ' 加盟店コードの判定
        If Not IIf(keyRecAcc.KameitenCd Is Nothing, String.Empty, keyRecAcc.KameitenCd) Is String.Empty Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@KAMEITENCD", SqlDbType.VarChar, 5, keyRecAcc.KameitenCd))
            sqlCondition.Append(" j.kameiten_cd = @KAMEITENCD ")
        End If

        '***********************************************************************
        ' 加盟店カナ１の設定
        '***********************************************************************
        ' 加盟店カナ１の判定
        If Not IIf(keyRecAcc.TenmeiKana1 Is Nothing, String.Empty, keyRecAcc.TenmeiKana1) Is String.Empty Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@TENMEIKANA1", SqlDbType.VarChar, 20, keyRecAcc.TenmeiKana1 & "%"))
            sqlCondition.Append(" (k.tenmei_kana1 like @TENMEIKANA1 OR k.tenmei_kana2 like @TENMEIKANA1)")
        End If

        '***********************************************************************
        ' 系列コードの設定
        '***********************************************************************
        ' 系列コードの判定
        If Not IIf(keyRecAcc.KeiretuCd Is Nothing, String.Empty, keyRecAcc.KeiretuCd) Is String.Empty Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@KEIRETU", SqlDbType.VarChar, 5, keyRecAcc.KeiretuCd))
            sqlCondition.Append(" k.keiretu_cd = @KEIRETU ")
        End If

        '***********************************************************************
        ' 営業所コードの設定
        '***********************************************************************
        ' 営業所コードの判定
        If Not IIf(keyRecAcc.EigyousyoCd Is Nothing, String.Empty, keyRecAcc.EigyousyoCd) Is String.Empty Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@EIGYOUSYO", SqlDbType.VarChar, 5, keyRecAcc.EigyousyoCd))
            sqlCondition.Append(" k.eigyousyo_cd = @EIGYOUSYO ")
        End If

        '***********************************************************************
        ' 調査会社コードの設定
        '***********************************************************************
        ' 調査会社コードの判定
        If Not IIf(keyRecAcc.TysKaisyaCd Is Nothing, String.Empty, keyRecAcc.TysKaisyaCd) Is String.Empty Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@TYSKAISYACD", SqlDbType.VarChar, 7, keyRecAcc.TysKaisyaCd))
            sqlCondition.Append(" j.tys_kaisya_cd + j.tys_kaisya_jigyousyo_cd = @TYSKAISYACD ")
        End If

        '***********************************************************************
        ' 工事会社コードの設定
        '***********************************************************************
        ' 工事会社コードの判定
        If Not IIf(keyRecAcc.KojGaisyaCd Is Nothing, String.Empty, keyRecAcc.KojGaisyaCd) Is String.Empty Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@KOUJIGAISYACD", SqlDbType.VarChar, 7, keyRecAcc.KojGaisyaCd))
            sqlCondition.Append(" j.koj_gaisya_cd + j.koj_gaisya_jigyousyo_cd = @KOUJIGAISYACD ")
        End If

        '***********************************************************************
        ' 工事売上年月日範囲条件の設定
        '***********************************************************************
        createDateHanniSql(sqlCondition, listParams, _
                           keyRecAcc.KojUriDateFrom, keyRecAcc.KojUriDateTo, _
                           "tk.uri_date", "@KOJURIDATE")

        '***********************************************************************
        ' 工事完了予定日範囲条件の設定
        '***********************************************************************
        createDateHanniSql(sqlCondition, listParams, _
                           keyRecAcc.KairyKojKanryYoteiDateFrom, keyRecAcc.KairyKojKanryYoteiDateTo, _
                           "j.kairy_koj_kanry_yotei_date", "@KOJKANRYOUYOTEIDATE")

        '***********************************************************************
        ' 施主名の設定
        '***********************************************************************
        ' 施主名の判定
        If Not IIf(keyRecAcc.SesyuMei Is Nothing, String.Empty, keyRecAcc.SesyuMei) Is String.Empty Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@SESYUMEI", SqlDbType.VarChar, 50 + 1, keyRecAcc.SesyuMei & "%"))
            sqlCondition.Append(" REPLACE(j.sesyu_mei, ' ', '') like REPLACE(@SESYUMEI, ' ', '') ")
        End If

        '***********************************************************************
        ' 物件住所1+2の設定
        '***********************************************************************
        ' 物件住所1+2の判定
        If Not IIf(keyRecAcc.BukkenJyuusyo12 Is Nothing, String.Empty, keyRecAcc.BukkenJyuusyo12) Is String.Empty Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@BUKKENJYUUSYO12", SqlDbType.VarChar, 64 + 1, keyRecAcc.BukkenJyuusyo12 & "%"))
            sqlCondition.Append(" isnull(j.bukken_jyuusyo1, '') + isnull(j.bukken_jyuusyo2, '') like @BUKKENJYUUSYO12 ")
        End If

        '***********************************************************************
        ' 備考の設定
        '***********************************************************************
        ' 備考の判定
        If Not IIf(keyRecAcc.Bikou Is Nothing, String.Empty, keyRecAcc.Bikou) Is String.Empty Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@BIKOU", SqlDbType.VarChar, 256 + 1, keyRecAcc.Bikou & "%"))
            sqlCondition.Append(" j.bikou like @BIKOU ")
        End If

        '***********************************************************************
        ' 依頼日範囲条件の設定
        '***********************************************************************
        createDateHanniSql(sqlCondition, listParams, _
                           keyRecAcc.IraiDateFrom, keyRecAcc.IraiDateTo, _
                           "j.irai_date", "@IRAIDATE")

        '***********************************************************************
        ' 調査希望日範囲条件の設定
        '***********************************************************************
        createDateHanniSql(sqlCondition, listParams, _
                           keyRecAcc.TyousaKibouDateFrom, keyRecAcc.TyousaKibouDateTo, _
                           "j.tys_kibou_date", "@TYOUSAKIBOUDATE")

        '***********************************************************************
        ' 調査実施日範囲条件の設定
        '***********************************************************************
        createDateHanniSql(sqlCondition, listParams, _
                           keyRecAcc.TyousaJissiDateFrom, keyRecAcc.TyousaJissiDateTo, _
                           "j.tys_jissi_date", "@TYOUSAJISSIDATE")

        '***********************************************************************
        ' 保証書発行日範囲条件の設定
        '***********************************************************************
        createDateHanniSql(sqlCondition, listParams, _
                           keyRecAcc.HosyousyoHakkouDateFrom, keyRecAcc.HosyousyoHakkouDateTo, _
                           "j.hosyousyo_hak_date", "@HOSYOUSYOHAKKOUDATE")

        '***********************************************************************
        ' 承諾書調査日範囲条件の設定
        '***********************************************************************
        createDateHanniSql(sqlCondition, listParams, _
                           keyRecAcc.SyoudakusyoTyousaDateFrom, keyRecAcc.SyoudakusyoTyousaDateTo, _
                           "j.syoudakusyo_tys_date", "@SYOUDAKUSYOTYOUSADATE")

        '***********************************************************************
        ' 計画書作成日範囲条件の設定
        '***********************************************************************
        createDateHanniSql(sqlCondition, listParams, _
                           keyRecAcc.KeikakusyoSakuseiDateFrom, keyRecAcc.KeikakusyoSakuseiDateTo, _
                           "j.keikakusyo_sakusei_date", "@KEIKAKUSYOSAKUSEIDATE")

        '***********************************************************************
        ' 保証書発行依頼書着日範囲条件の設定
        '***********************************************************************
        createDateHanniSql(sqlCondition, listParams, _
                           keyRecAcc.HosyousyoHakkouIraisyoTyakuDateFrom, keyRecAcc.HosyousyoHakkouIraisyoTyakuDateTo, _
                           "j.hosyousyo_hak_iraisyo_tyk_date", "@HOSYOUSYOHAKKOUIRAISYOTYAKUDATE")

        '***********************************************************************
        ' データ破棄種別
        '***********************************************************************
        ' データ破棄種別の判定
        If keyRecAcc.DataHakiSyubetu = 0 Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@DATAHAKISYUBETU", SqlDbType.Int, 10, keyRecAcc.DataHakiSyubetu))
            sqlCondition.Append(" ISNULL(j.data_haki_syubetu,0) = @DATAHAKISYUBETU ")

        End If

        '***********************************************************************
        ' 予約済FLG
        '***********************************************************************
        ' 予約済FLGの判定
        If keyRecAcc.YoyakuZumiFlg = 1 Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@YOYAKUZUMI", SqlDbType.Int, 10, keyRecAcc.YoyakuZumiFlg))
            sqlCondition.Append(" ISNULL(j.yoyaku_zumi_flg,0) = @YOYAKUZUMI ")

        End If

        '***********************************************************************
        ' 分譲コード
        '***********************************************************************
        ' 分譲コードの判定
        If keyRecAcc.BunjouCd <> Integer.MinValue Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@BUNJOUCD", SqlDbType.Int, 4, keyRecAcc.BunjouCd))
            sqlCondition.Append(" ISNULL(j.bunjou_cd, 0) = @BUNJOUCD")

        End If

        '***********************************************************************
        ' 物件名寄コード
        '***********************************************************************
        ' 物件名寄コードの判定
        If keyRecAcc.BukkenNayoseCd IsNot Nothing AndAlso keyRecAcc.BukkenNayoseCd <> String.Empty Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@BUKKENNNAYOSECD", SqlDbType.VarChar, 12, keyRecAcc.BukkenNayoseCd & "%"))
            sqlCondition.Append(" ISNULL(j.bukken_nayose_cd, 0) like @BUKKENNNAYOSECD")

        End If

        '***********************************************************************
        ' 契約NO
        '***********************************************************************
        ' 契約NOの判定
        If keyRecAcc.KeiyakuNo IsNot Nothing AndAlso keyRecAcc.KeiyakuNo <> String.Empty Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@KEIYAKUNO", SqlDbType.VarChar, 20, keyRecAcc.KeiyakuNo & "%"))
            sqlCondition.Append(" ISNULL(j.keiyaku_no, 0) like @KEIYAKUNO")

        End If

        ' パラメータの作成
        Dim i As Integer
        Dim cmdParams(listParams.Count - 1) As SqlParameter
        For i = 0 To listParams.Count - 1
            Dim rec As ParamRecord = listParams(i)
            ' 必要な情報をセット
            cmdParams(i) = SQLHelper.MakeParam(rec.Param, rec.DbType, rec.ParamLength, rec.SetData)
        Next

        ' 返却用パラメータのセット
        sql_params = cmdParams

        ' 最終的なSQL文の生成
        sql = String.Format(baseSql, sqlCondition.ToString())

        Return sql

    End Function

    ''' <summary>
    ''' パラメータレコードを作成します(物件検索用)
    ''' </summary>
    ''' <param name="paramName">パラメータ名</param>
    ''' <param name="dbType">DB属性</param>
    ''' <param name="length">長さ</param>
    ''' <param name="data">設定するデータ</param>
    ''' <returns>パラメータレコード</returns>
    ''' <remarks></remarks>
    Private Function getParamRecord(ByVal paramName As String, _
                                    ByVal dbType As SqlDbType, _
                                    ByVal length As Integer, _
                                    ByVal data As Object) As ParamRecord
        Dim paramRec As New ParamRecord

        paramRec.Param = paramName
        paramRec.DbType = dbType
        paramRec.ParamLength = length
        paramRec.SetData = data

        Return paramRec
    End Function

    ''' <summary>
    ''' 日付範囲検索条件SQL文生成(物件検索用)
    ''' </summary>
    ''' <param name="sqlCondition">SQL文を蓄積するStringBuilder</param>
    ''' <param name="arrParams">DBへのパラメータを蓄積するList</param>
    ''' <param name="dateFrom">FROM日付</param>
    ''' <param name="dateTo">TO日付</param>
    ''' <param name="strColumn">検索対象カラム文字列</param>
    ''' <param name="strTarget">パラメータ置換用文字列</param>
    ''' <remarks></remarks>
    Private Sub createDateHanniSql(ByRef sqlCondition As StringBuilder, _
                                   ByRef arrParams As List(Of ParamRecord), _
                                   ByVal dateFrom As Date, _
                                   ByVal dateTo As Date, _
                                   ByVal strColumn As String, _
                                   ByVal strTarget As String)

        ' 日付が一つでも設定されている場合、条件を作成
        If dateFrom <> DateTime.MinValue Or _
           dateTo <> DateTime.MinValue Then

            Dim strTargetFrom As String = strTarget & "FROM"
            Dim strTargetTo As String = strTarget & "TO"

            sqlCondition.Append(" AND ")

            If dateFrom <> DateTime.MinValue And _
               dateTo <> DateTime.MinValue Then
                ' 両方指定有りはBETWEEN
                sqlCondition.Append(String.Format(" {0} BETWEEN {1} AND {2} ", strColumn, strTargetFrom, strTargetTo))
                arrParams.Add(getParamRecord(strTargetFrom, SqlDbType.DateTime, 16, dateFrom))
                arrParams.Add(getParamRecord(strTargetTo, SqlDbType.DateTime, 16, dateTo))
            Else
                If dateFrom <> DateTime.MinValue Then
                    ' Fromのみ
                    sqlCondition.Append(String.Format(" {0} >= {1} ", strColumn, strTargetFrom))
                    arrParams.Add(getParamRecord(strTargetFrom, SqlDbType.DateTime, 16, dateFrom))
                Else
                    ' Toのみ
                    sqlCondition.Append(String.Format(" {0} <= {1} ", strColumn, strTargetTo))
                    arrParams.Add(getParamRecord(strTargetTo, SqlDbType.DateTime, 16, dateTo))
                End If
            End If

        End If
    End Sub

#End Region

    ''' <summary>
    ''' 地盤レコードを検索し初期登録の判定を行います
    ''' </summary>
    ''' <param name="kbn">区分</param>
    ''' <param name="hosyousyoNo">保証書NO</param>   
    ''' <returns>True:新規登録（初期登録状態）False:登録済</returns>
    ''' <remarks>新規採番済である事を前提とし、更新の有無をチェックします</remarks>
    Public Function ChkJibanNew(ByVal kbn As String, _
                                ByVal hosyousyoNo As String) As Boolean

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ChkJibanNew", _
                                    kbn, _
                                    hosyousyoNo)

        ' パラメータ
        Const strParamKbn As String = "@KBN"
        Const strParamHosyousyoNo As String = "@HOSYOUSYONO"

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKbn, SqlDbType.Char, 1, kbn), _
             SQLHelper.MakeParam(strParamHosyousyoNo, SqlDbType.VarChar, 10, hosyousyoNo)}

        Dim commandText As String = " SELECT " & _
                                    "   upd_datetime " & _
                                    " FROM " & _
                                    "    t_jiban WITH (READCOMMITTED) " & _
                                    " WHERE " & _
                                    "    kbn = " & strParamKbn & _
                                    " AND " & _
                                    "    hosyousyo_no = " & strParamHosyousyoNo

        ' データの取得
        Dim ret As Object = SQLHelper.ExecuteScalar(connStr, _
                                                    CommandType.Text, _
                                                    commandText, _
                                                    commandParameters)

        If ret Is System.DBNull.Value Then
            Return True
        End If

        Return False

    End Function

    ''' <summary>
    ''' 地盤レコードを検索し存在チェックを行います
    ''' </summary>
    ''' <param name="kbn">区分</param>
    ''' <param name="hosyousyoNo">保証書NO</param>   
    ''' <returns>True:存在する False:存在しない</returns>
    ''' <remarks>
    ''' 無用な検索を避ける為、パラメータの有無チェックは<br/>
    ''' ロジッククラスに実装してください(空白,Nothingの確認)</remarks>
    Public Function IsJibanData(ByVal kbn As String, _
                                ByVal hosyousyoNo As String) As Boolean

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ChkJibanNew", _
                                    kbn, _
                                    hosyousyoNo)

        ' パラメータ
        Const strParamKbn As String = "@KBN"
        Const strParamHosyousyoNo As String = "@HOSYOUSYONO"

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKbn, SqlDbType.Char, 1, kbn), _
             SQLHelper.MakeParam(strParamHosyousyoNo, SqlDbType.VarChar, 10, hosyousyoNo)}

        Dim commandText As String = " SELECT " & _
                                    "   kbn " & _
                                    " FROM " & _
                                    "    t_jiban WITH (READCOMMITTED) " & _
                                    " WHERE " & _
                                    "    kbn = " & strParamKbn & _
                                    " AND " & _
                                    "    hosyousyo_no = " & strParamHosyousyoNo

        ' データの取得
        Dim ret As Object = SQLHelper.ExecuteScalar(connStr, _
                                                    CommandType.Text, _
                                                    commandText, _
                                                    commandParameters)

        If ret Is Nothing Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 地盤レコードを検索し更新日時を取得します
    ''' </summary>
    ''' <param name="kbn">区分</param>
    ''' <param name="hosyousyoNo">保証書NO</param>   
    ''' <returns>更新日時</returns>
    ''' <remarks></remarks>
    Public Function GetJibanUpdDateTime(ByVal kbn As String, _
                                        ByVal hosyousyoNo As String) As DateTime

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ChkJibanNew", _
                                    kbn, _
                                    hosyousyoNo)

        ' パラメータ
        Const strParamKbn As String = "@KBN"
        Const strParamHosyousyoNo As String = "@HOSYOUSYONO"

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKbn, SqlDbType.Char, 1, kbn), _
             SQLHelper.MakeParam(strParamHosyousyoNo, SqlDbType.VarChar, 10, hosyousyoNo)}

        Dim commandText As String = " SELECT " & _
                                    "   upd_datetime " & _
                                    " FROM " & _
                                    "    t_jiban WITH (READCOMMITTED) " & _
                                    " WHERE " & _
                                    "    kbn = " & strParamKbn & _
                                    " AND " & _
                                    "    hosyousyo_no = " & strParamHosyousyoNo

        ' データの取得
        Dim ret As Object = SQLHelper.ExecuteScalar(connStr, _
                                                    CommandType.Text, _
                                                    commandText, _
                                                    commandParameters)

        If ret Is System.DBNull.Value Then
            Return DateTime.MinValue
        End If

        Return ret

    End Function

    ''' <summary>
    ''' 地盤データに設定されている分譲コードを取得します
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetBunjouCd(ByVal strBunjouCd As String) As Boolean

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetBunjouCd", strBunjouCd)

        Dim blnRes As Boolean = False

        Dim cmdParams() As SqlParameter = {}
        Dim commandTextSb As New StringBuilder
        Dim dTblRes As New DataTable

        'SQL生成
        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("   j.bunjou_cd ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("    t_jiban j  WITH (READCOMMITTED) ")
        commandTextSb.Append(" WHERE 1=1 ")
        'データ破棄種別が設定されていないものだけ取得
        commandTextSb.Append(" AND")
        commandTextSb.Append("    j.data_haki_syubetu = 0")
        commandTextSb.Append(" AND ")
        commandTextSb.Append("    j.bunjou_cd IS NOT NULL ")
        commandTextSb.Append(" AND ")
        commandTextSb.Append("    j.bunjou_cd = " & strBunjouCd)

        'データ取得＆返却
        dTblRes = cmnDtAcc.getDataTable(commandTextSb.ToString, cmdParams)
        If Not dTblRes Is Nothing AndAlso dTblRes.Rows.Count > 0 Then
            blnRes = True
        End If

        Return blnRes
    End Function

    ''' <summary>
    ''' 地盤レコードを走査し該当物件が他物件から名寄されているかのチェックを行います
    ''' </summary>
    ''' <param name="kbn">区分</param>
    ''' <param name="hosyousyoNo">保証書NO</param>
    ''' <param name="nayoseKbn">名寄先.区分</param>
    ''' <param name="nayoseHosyousyoNo">名寄先.保証書NO</param>
    ''' <returns>True:存在する False:存在しない</returns>
    ''' <remarks></remarks>
    Public Function ChkJibanDataNayoseNotChildren( _
                                                    ByVal kbn As String, _
                                                    ByVal hosyousyoNo As String, _
                                                    ByVal nayoseKbn As String, _
                                                    ByVal nayoseHosyousyoNo As String _
                                                ) As Boolean

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ChkJibanDataNayoseNotChildren", _
                                    kbn, hosyousyoNo, nayoseKbn, nayoseHosyousyoNo)

        ' パラメータ
        Const strPrmKbn1 As String = "@KBN1" '区分
        Const strPrmHosyousyoNo1 As String = "@HOSYOUSYONO1" '番号
        Const strPrmBukkenNo1 As String = "@BUKKENNO1" '物件NO
        ' パラメータ(名寄先)
        Const strPrmKbn2 As String = "@KBN2" '区分
        Const strPrmHosyousyoNo2 As String = "@HOSYOUSYONO2" '番号
        Const strPrmBukkenNo2 As String = "@BUKKENNO2" '物件NO

        If kbn = nayoseKbn And hosyousyoNo = nayoseHosyousyoNo Then '自物件に名寄する場合、データチェック不要
            Return True
        End If

        Dim cmdTextSb As New StringBuilder

        '抽出クエリ生成
        '自物件NOが(自物件以外の)他物件から名寄されていない場合OK
        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append("      TJ3.kbn ")
        cmdTextSb.Append("  FROM ")
        cmdTextSb.Append("      t_jiban TJ3 ")
        cmdTextSb.Append("  WHERE ")
        cmdTextSb.Append("      1=1 ")
        cmdTextSb.Append("  AND NOT ( ")
        cmdTextSb.Append("      TJ3.kbn = " & strPrmKbn1)
        cmdTextSb.Append("      AND TJ3.hosyousyo_no = " & strPrmHosyousyoNo1)
        cmdTextSb.Append("      ) ")
        cmdTextSb.Append("  AND TJ3.bukken_nayose_cd IS NOT NULL ")
        cmdTextSb.Append("  AND TJ3.bukken_nayose_cd = " & strPrmBukkenNo1)

        ' パラメータへ設定
        Dim cmdPrms() As SqlParameter = _
            {SQLHelper.MakeParam(strPrmKbn1, SqlDbType.Char, 1, kbn), _
             SQLHelper.MakeParam(strPrmHosyousyoNo1, SqlDbType.VarChar, 10, hosyousyoNo), _
             SQLHelper.MakeParam(strPrmBukkenNo1, SqlDbType.VarChar, 11, kbn & hosyousyoNo), _
             SQLHelper.MakeParam(strPrmKbn2, SqlDbType.Char, 1, nayoseKbn), _
             SQLHelper.MakeParam(strPrmHosyousyoNo2, SqlDbType.VarChar, 10, nayoseHosyousyoNo), _
             SQLHelper.MakeParam(strPrmBukkenNo2, SqlDbType.VarChar, 11, nayoseKbn & nayoseHosyousyoNo)}

        ' データの取得
        Dim ret As Object = SQLHelper.ExecuteScalar(connStr, _
                                                    CommandType.Text, _
                                                    cmdTextSb.ToString, _
                                                    cmdPrms)
        '値がとれる場合、エラー
        If ret Is Nothing Then
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' 地盤レコードを走査し、該当物件が親物件であるかどうかの判定を行なう
    ''' </summary>
    ''' <param name="kbn">区分</param>
    ''' <param name="hosyousyoNo">保証書NO</param>
    ''' <returns>True:存在する False:存在しない</returns>
    ''' <remarks></remarks>
    Public Function ChkJibanDataOyaBukken( _
                                            ByVal kbn As String, _
                                            ByVal hosyousyoNo As String, _
                                            ByVal nayoseKbn As String, _
                                            ByVal nayoseHosyousyoNo As String _
                                                ) As Boolean

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ChkJibanDataOyaBukken", _
                                    kbn, hosyousyoNo, nayoseKbn, nayoseHosyousyoNo)

        ' パラメータ
        Const strPrmKbn1 As String = "@KBN1" '区分
        Const strPrmHosyousyoNo1 As String = "@HOSYOUSYONO1" '番号
        Const strPrmBukkenNo1 As String = "@BUKKENNO1" '物件NO

        If kbn = nayoseKbn And hosyousyoNo = nayoseHosyousyoNo Then '自物件に名寄する場合、データチェック不要
            Return True
        End If

        Dim cmdTextSb As New StringBuilder

        '抽出クエリ生成
        '該当物件は親物件
        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append("      TJ3.kbn ")
        cmdTextSb.Append("  FROM ")
        cmdTextSb.Append("      t_jiban TJ3 ")
        cmdTextSb.Append("  WHERE ")
        cmdTextSb.Append("      1=1 ")
        cmdTextSb.Append("  AND TJ3.kbn = " & strPrmKbn1)
        cmdTextSb.Append("  AND TJ3.hosyousyo_no = " & strPrmHosyousyoNo1)
        cmdTextSb.Append("  AND TJ3.bukken_nayose_cd IS NOT NULL ")
        cmdTextSb.Append("  AND TJ3.bukken_nayose_cd = " & strPrmBukkenNo1)

        ' パラメータへ設定
        Dim cmdPrms() As SqlParameter = _
            {SQLHelper.MakeParam(strPrmKbn1, SqlDbType.Char, 1, nayoseKbn), _
             SQLHelper.MakeParam(strPrmHosyousyoNo1, SqlDbType.VarChar, 10, nayoseHosyousyoNo), _
             SQLHelper.MakeParam(strPrmBukkenNo1, SqlDbType.VarChar, 11, nayoseKbn & nayoseHosyousyoNo) _
             }

        ' データの取得
        Dim ret As Object = SQLHelper.ExecuteScalar(connStr, _
                                                    CommandType.Text, _
                                                    cmdTextSb.ToString, _
                                                    cmdPrms)

        '値がとれない場合、エラー
        If ret Is Nothing Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' 地盤レコードを走査し最新の物件名寄状況のチェックを行います
    ''' </summary>
    ''' <param name="kbn">区分</param>
    ''' <param name="hosyousyoNo">保証書NO</param>   
    ''' <returns>True:存在する False:存在しない</returns>
    ''' <remarks></remarks>
    Public Function ChkLatestJibanDataNayoseJyky( _
                                                    ByVal kbn As String, _
                                                    ByVal hosyousyoNo As String _
                                                ) As Boolean

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ChkLatestJibanDataNayoseJyky", _
                                    kbn, _
                                    hosyousyoNo)

        ' パラメータ
        Const strPrmKbn1 As String = "@KBN1" '区分
        Const strPrmHosyousyoNo1 As String = "@HOSYOUSYONO1" '番号
        Const strPrmBukkenNo1 As String = "@BUKKENNO1" '物件NO

        Dim cmdTextSb As New StringBuilder

        '抽出クエリ生成
        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append("      TJ1.kbn ")
        cmdTextSb.Append("    , TJ1.hosyousyo_no ")
        cmdTextSb.Append(" FROM ")
        cmdTextSb.Append("      t_jiban TJ1 ")
        cmdTextSb.Append(" WHERE ")
        cmdTextSb.Append("      1=1 ")
        cmdTextSb.Append("  AND TJ1.kbn = " & strPrmKbn1)
        cmdTextSb.Append("  AND TJ1.hosyousyo_no = " & strPrmHosyousyoNo1)
        cmdTextSb.Append("  AND EXISTS ")
        cmdTextSb.Append("     (SELECT ")
        cmdTextSb.Append("           * ")
        cmdTextSb.Append("      FROM ")
        cmdTextSb.Append("           t_jiban TJ3 ")
        cmdTextSb.Append("      WHERE ")
        cmdTextSb.Append("           1=1 ")
        cmdTextSb.Append("       AND NOT (TJ3.kbn = " & strPrmKbn1)
        cmdTextSb.Append("           AND TJ3.hosyousyo_no = " & strPrmHosyousyoNo1 & ")")
        cmdTextSb.Append("       AND TJ3.bukken_nayose_cd IS NOT NULL ")
        cmdTextSb.Append("       AND TJ3.bukken_nayose_cd = " & strPrmBukkenNo1)
        cmdTextSb.Append("     ) ")

        ' パラメータへ設定
        Dim cmdPrms() As SqlParameter = _
            {SQLHelper.MakeParam(strPrmKbn1, SqlDbType.Char, 1, kbn), _
             SQLHelper.MakeParam(strPrmHosyousyoNo1, SqlDbType.VarChar, 10, hosyousyoNo), _
             SQLHelper.MakeParam(strPrmBukkenNo1, SqlDbType.VarChar, 11, kbn & hosyousyoNo)}

        ' データの取得
        Dim ret As Object = SQLHelper.ExecuteScalar(connStr, _
                                                    CommandType.Text, _
                                                    cmdTextSb.ToString, _
                                                    cmdPrms)

        '存在しなければOK
        If ret Is Nothing Then
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="kbn"></param>
    ''' <param name="bangou"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBukkenNayoseNotEqualList( _
                                                ByVal kbn As String, _
                                                ByVal bangou As String _
                                            ) As String

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetBukkenNayoseNotEqualList", _
                                    kbn, bangou)

        ' パラメータ
        Const strPrmKbn1 As String = "@KBN1" '区分
        Const strPrmHosyousyoNo1 As String = "@HOSYOUSYONO1" '番号
        Const strPrmBukkenNo1 As String = "@BUKKENNO1" '物件NO

        Dim dTblRes As New DataTable
        Dim strRetBukkenNo As String = String.Empty
        Dim cmdTextSb As New StringBuilder

        '抽出クエリ生成
        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append("      TJ1.kbn AS kbn ")
        cmdTextSb.Append("    , TJ1.hosyousyo_no AS bangou ")
        cmdTextSb.Append("    , TJ1.bukken_nayose_cd AS bukken_nayose_cd ")
        cmdTextSb.Append(" FROM ")
        cmdTextSb.Append("      t_jiban TJ1 ")
        cmdTextSb.Append(" WHERE ")
        cmdTextSb.Append("      1=1 ")
        cmdTextSb.Append("  AND TJ1.kbn = " & strPrmKbn1)
        cmdTextSb.Append("  AND TJ1.hosyousyo_no = " & strPrmHosyousyoNo1)
        cmdTextSb.Append("  AND TJ1.bukken_nayose_cd IS NOT NULL ")
        cmdTextSb.Append("  AND TJ1.bukken_nayose_cd <> " & strPrmBukkenNo1)

        ' パラメータへ設定
        Dim cmdPrms() As SqlParameter = _
            {SQLHelper.MakeParam(strPrmKbn1, SqlDbType.Char, 1, kbn), _
             SQLHelper.MakeParam(strPrmHosyousyoNo1, SqlDbType.VarChar, 10, bangou), _
             SQLHelper.MakeParam(strPrmBukkenNo1, SqlDbType.VarChar, 11, kbn & bangou) _
        }

        'データ取得＆返却
        dTblRes = cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdPrms)
        If dTblRes.Rows.Count > 0 Then
            strRetBukkenNo = dTblRes.Rows(0)("kbn").ToString & dTblRes.Rows(0)("bangou").ToString
        End If
        Return strRetBukkenNo
    End Function

    ''' <summary>
    ''' 品質保証書状況検索用データを取得します
    ''' </summary>
    ''' <param name="keyRec">品質Keyレコード</param>
    ''' <returns>データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetJibanDataHinsitu(ByVal keyRec As HinsituHosyousyoJyoukyouRecord) As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetJibanDataHinsitu", _
                                                    keyRec)
        'SQL文の生成
        Dim sql As String = String.Empty
        Dim cmdParams() As SqlParameter = GetJibanDataHinsituSqlCmnParams(keyRec, sql)

        Dim sqlSb As New StringBuilder()
        sqlSb.AppendLine(" SELECT ")
        sqlSb.AppendLine("    j.kbn")
        sqlSb.AppendLine("    , j.hosyousyo_no ")
        sqlSb.AppendLine("    , h.haki_syubetu As data_haki_syubetu ")
        sqlSb.AppendLine("    , j.sesyu_mei ")
        sqlSb.AppendLine("    , j.bukken_jyuusyo1 ")
        sqlSb.AppendLine("    , j.bukken_jyuusyo2 ")
        sqlSb.AppendLine("    , j.bukken_jyuusyo3 ")
        sqlSb.AppendLine("    , j.kameiten_cd ")
        sqlSb.AppendLine("    , k.torikesi AS kt_torikesi ")
        sqlSb.AppendLine("    , km.meisyou AS kt_torikesi_riyuu ")
        sqlSb.AppendLine("    , k.kameiten_mei1 ")
        sqlSb.AppendLine("    , ri.hak_irai_time ")
        sqlSb.AppendLine("    , j.hosyousyo_hak_iraisyo_tyk_date ")
        sqlSb.AppendLine("    , j.hosyousyo_hak_date ")
        sqlSb.AppendLine("    , j.hosyou_kaisi_date ")
        sqlSb.AppendLine("    , ksm1.ks_siyou As ks_siyou_1 ")
        sqlSb.AppendLine("    , ksm2.ks_siyou As ks_siyou_2 ")
        sqlSb.AppendLine("    , kss.ks_siyou_setuzokusi ")
        sqlSb.AppendLine("    , j.koj_hkks_juri_date ")
        sqlSb.AppendLine("    , j.hosyou_nasi_riyuu ")
        sqlSb.AppendLine("    , jmm.DisplayName ")
        sqlSb.AppendLine("    , j.hosyousyo_hak_houhou AS hosyousyo_hak_umu")
        sqlSb.AppendLine("    , k.hosyou_kikan As hosyou_kikan_MK ")
        sqlSb.AppendLine("    , j.hosyou_kikan As hosyou_kikan_TJ ")
        sqlSb.AppendLine("    , j.irai_date ")
        sqlSb.AppendLine("    , j.bikou ")
        '以下チェック＆セット用
        sqlSb.AppendLine("    , j.upd_datetime ")
        sqlSb.AppendLine("    , ktm.tyuuijikou_syubetu ")
        sqlSb.AppendLine("    , ri.hak_irai_hw_date ")
        sqlSb.AppendLine("    , j.hosyousyo_saihak_date ")
        sqlSb.AppendLine("    , ri.hak_irai_bkn_name ")
        sqlSb.AppendLine("    , ri.hak_irai_bkn_adr1 ")
        sqlSb.AppendLine("    , ri.hak_irai_bkn_adr2 ")
        sqlSb.AppendLine("    , ri.hak_irai_bkn_adr3 ")
        sqlSb.AppendLine("    , ri.keiyu ")
        sqlSb.AppendLine("    , tk.hosyousyo_no AS hosyousyo_no_TK ")
        sqlSb.AppendLine("    , hkt.bukken_jyky ")
        sqlSb.AppendLine("    , tk.bikou As bikou_TK ")
        sqlSb.AppendLine("    , j.hosyousyo_hak_iraisyo_umu ")
        sqlSb.AppendLine("    , ri.hak_irai_uke_datetime ")
        sqlSb.AppendLine("    , ri.hak_irai_can_datetime ")

        sqlSb.AppendLine(sql)

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(sqlSb.ToString, cmdParams)
    End Function

    ''' <summary>
    ''' 品質保証書状況検索データ[CSV出力用]を取得します
    ''' </summary>
    ''' <param name="keyRec">品質Keyレコード</param>
    ''' <returns>データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetJibanDataHinsituCsv(ByVal keyRec As HinsituHosyousyoJyoukyouRecord) As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetJibanDataHinsituCsv", keyRec)

        'SQL文の生成
        Dim sql As String = String.Empty
        Dim cmdParams() As SqlParameter = GetJibanDataHinsituSqlCmnParams(keyRec, sql)

        Dim sqlSb As New StringBuilder
        sqlSb.AppendLine(" SELECT ")
        sqlSb.AppendLine("     CONVERT(VARCHAR,j.data_haki_syubetu) + '：' + h.haki_syubetu '破棄種別' ")
        sqlSb.AppendLine("    , j.kbn '区分' ")
        sqlSb.AppendLine("    , j.hosyousyo_no '番号' ")
        sqlSb.AppendLine("    , j.sesyu_mei '施主名' ")
        sqlSb.AppendLine("    , j.bukken_jyuusyo1 + j.bukken_jyuusyo2 + j.bukken_jyuusyo3 '物件住所' ")
        sqlSb.AppendLine("    , j.kameiten_cd '加盟店コード' ")
        sqlSb.AppendLine("    , CONVERT(VARCHAR,k.torikesi) + '：' + km.meisyou '加盟店取消' ")
        sqlSb.AppendLine("    , k.kameiten_mei1 '加盟店名' ")
        sqlSb.AppendLine("    , CONVERT(VARCHAR, ri.hak_irai_time, 111) '依頼日時' ")
        sqlSb.AppendLine("    , CONVERT(VARCHAR, j.hosyousyo_hak_iraisyo_tyk_date, 111) '依頼書着日' ")
        sqlSb.AppendLine("    , CONVERT(VARCHAR, j.hosyousyo_hak_date, 111) '発行日' ")
        sqlSb.AppendLine("    , CONVERT(VARCHAR, j.hosyou_kaisi_date, 111) '保証開始日' ")
        sqlSb.AppendLine("    , ksm1.ks_siyou + kss.ks_siyou_setuzokusi + ksm2.ks_siyou '判定' ")
        sqlSb.AppendLine("    , CONVERT(VARCHAR, j.koj_hkks_juri_date, 111) '工報受理日' ")
        sqlSb.AppendLine("    , j.hosyou_nasi_riyuu '保証なし理由' ")
        sqlSb.AppendLine("    , jmm.DisplayName '営業担当者' ")
        sqlSb.AppendLine("    , CASE WHEN j.hosyousyo_hak_houhou = '0' THEN '依頼書' WHEN j.hosyousyo_hak_houhou = '1' THEN '自動発行' WHEN j.hosyousyo_hak_houhou = '2' THEN '地盤モール' ELSE '' END '初回発行方法' ")
        sqlSb.AppendLine("    , k.hosyou_kikan '加／保証期間' ")
        sqlSb.AppendLine("    , j.hosyou_kikan '物／保証期間' ")
        sqlSb.AppendLine("    , CONVERT(VARCHAR, j.irai_date, 111) '物件依頼日' ")
        sqlSb.AppendLine("    , j.bikou '備考' ")
        sqlSb.AppendLine(sql)

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(sqlSb.ToString, cmdParams)
    End Function

    ''' <summary>
    ''' 品質保証書状況検索データ取得用の共通SQLクエリ・パラメータを取得
    ''' </summary>
    ''' <param name="keyRec">品質Keyレコード</param>
    ''' <param name="sql">SQLクエリ</param>
    ''' <returns>共通部分のSQL</returns>
    ''' <remarks></remarks>
    Private Function GetJibanDataHinsituSqlCmnParams(ByVal keyRec As HinsituHosyousyoJyoukyouRecord, _
                                                     ByRef sql As String) As SqlParameter()
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetJibanDataHinsituSqlCmnParams", keyRec, sql)

        'パラメータの生成
        Dim listParams As New List(Of ParamRecord)
        'SQL文の生成
        Dim sqlCondition As New StringBuilder

        Dim sqlSb As New StringBuilder()
        sqlSb.AppendLine("FROM")
        sqlSb.AppendLine("    jhs_sys.t_jiban j WITH (READCOMMITTED) ")
        '加盟店M
        sqlSb.AppendLine("    LEFT OUTER JOIN jhs_sys.m_kameiten k WITH (READCOMMITTED) ")
        sqlSb.AppendLine("        ON j.kameiten_cd = k.kameiten_cd ")
        '拡張名称M
        sqlSb.AppendLine("    LEFT OUTER JOIN jhs_sys.m_kakutyou_meisyou AS km WITH (READCOMMITTED) ")
        sqlSb.AppendLine("        ON km.meisyou_syubetu = 56 ")
        sqlSb.AppendLine("        AND km.code = CAST(k.torikesi AS VARCHAR(10)) ")
        'データ破棄M
        sqlSb.AppendLine("    LEFT OUTER JOIN jhs_sys.m_data_haki h WITH (READCOMMITTED) ")
        sqlSb.AppendLine("        ON j.data_haki_syubetu = h.data_haki_no ")
        '社員アカウント情報M
        sqlSb.AppendLine("    LEFT OUTER JOIN jhs_sys.m_jhs_mailbox jmm WITH (READCOMMITTED) ")
        sqlSb.AppendLine("        ON k.eigyou_tantousya_mei = jmm.PrimaryWindowsNTAccount ")
        '担当者M
        sqlSb.AppendLine("    LEFT OUTER JOIN jhs_sys.m_tantousya ttmi WITH (READCOMMITTED) ")
        sqlSb.AppendLine("        ON j.tantousya_cd = ttmi.tantousya_cd ")
        '調査会社M
        sqlSb.AppendLine("    LEFT OUTER JOIN jhs_sys.m_tyousakaisya tm WITH (READCOMMITTED) ")
        sqlSb.AppendLine("        ON j.tys_kaisya_cd = tm.tys_kaisya_cd ")
        sqlSb.AppendLine("        AND j.tys_kaisya_jigyousyo_cd = tm.jigyousyo_cd ")
        '基礎仕様M1
        sqlSb.AppendLine("    LEFT OUTER JOIN jhs_sys.m_ks_siyou ksm1 WITH (READCOMMITTED) ")
        sqlSb.AppendLine("        ON j.hantei_cd1 = ksm1.ks_siyou_no ")
        '基礎仕様M2
        sqlSb.AppendLine("    LEFT OUTER JOIN jhs_sys.m_ks_siyou ksm2 WITH (READCOMMITTED) ")
        sqlSb.AppendLine("        ON j.hantei_cd2 = ksm2.ks_siyou_no ")
        '基礎仕様接続詞M
        sqlSb.AppendLine("    LEFT OUTER JOIN jhs_sys.m_ks_siyou_setuzokusi kss WITH (READCOMMITTED) ")
        sqlSb.AppendLine("        ON j.hantei_setuzoku_moji = kss.ks_siyou_setuzokusi_no ")
        '進捗
        sqlSb.AppendLine("    LEFT OUTER JOIN ReportIF ri WITH (READCOMMITTED) ")
        sqlSb.AppendLine("        ON j.kbn + j.hosyousyo_no = ri.kokyaku_no ")
        sqlSb.AppendLine("        AND ri.kokyaku_brc = '0'                  ")
        '保証書管理T
        sqlSb.AppendLine("    LEFT OUTER JOIN jhs_sys.t_hosyousyo_kanri hkt WITH (READCOMMITTED) ")
        sqlSb.AppendLine("        ON j.kbn = hkt.kbn ")
        sqlSb.AppendLine("        AND j.hosyousyo_no = hkt.hosyousyo_no ")
        '以下チェック＆セット用
        '加盟店注意事項設定M
        sqlSb.AppendLine("        LEFT OUTER JOIN (")
        sqlSb.AppendLine("         SELECT mkt.kameiten_cd, mkt.tyuuijikou_syubetu")
        sqlSb.AppendLine("              , MAX(mkt.nyuuryoku_no) max_nyuuryoku_no ")
        sqlSb.AppendLine("           FROM m_kameiten_tyuuijikou mkt WITH (READCOMMITTED)  ")
        sqlSb.AppendLine("        GROUP BY mkt.kameiten_cd, mkt.tyuuijikou_syubetu ")
        sqlSb.AppendLine("        ) AS ktm ")
        sqlSb.AppendLine("        ON j.kameiten_cd = ktm.kameiten_cd")
        sqlSb.AppendLine("       AND ktm.tyuuijikou_syubetu = '55'  ")
        '邸別請求T
        sqlSb.AppendLine("    LEFT OUTER JOIN jhs_sys.t_teibetu_seikyuu AS tk WITH (READCOMMITTED) ")
        sqlSb.AppendLine("        ON j.kbn = tk.kbn ")
        sqlSb.AppendLine("        AND j.hosyousyo_no = tk.hosyousyo_no ")
        sqlSb.AppendLine("        AND tk.bunrui_cd = '170' ")

        sqlSb.AppendLine(" WHERE ")
        sqlSb.AppendLine("    0 = 0 ")
        sqlSb.AppendLine("    {0} ")
        sqlSb.AppendLine(" ORDER BY ")
        sqlSb.AppendLine("    j.kbn ")
        sqlSb.AppendLine("    , j.hosyousyo_no ")
        sqlSb.AppendLine("")

        '***********************************************************************
        ' 区分条件の設定
        '***********************************************************************
        '区分条件SQL格納用
        Dim tmpKbnNoJouken As String = String.Empty
        '区分指定条件格納リスト
        Dim listKbn As New List(Of String)

        ' 区分_1の判定
        If IIf(keyRec.Kbn1 Is Nothing, String.Empty, keyRec.Kbn1) IsNot String.Empty Then
            listKbn.Add(keyRec.Kbn1)
        End If

        'SQL文生成
        tmpKbnNoJouken &= (" AND ")
        tmpKbnNoJouken &= (" j.kbn IN ( ")

        If listKbn.Count = 0 Then
            '条件が設定されていない場合、全区分を対象
            tmpKbnNoJouken &= (" SELECT kbn FROM m_data_kbn ")
        ElseIf listKbn.Count >= 1 Then
            Dim strParamKbn As String = "@KUBUN"
            Dim ki As Integer = 0
            tmpKbnNoJouken &= (strParamKbn)
            listParams.Add(getParamRecord(strParamKbn, SqlDbType.Char, 1, listKbn(ki)))
        End If

        tmpKbnNoJouken &= (" ) ")

        '***********************************************************************
        ' 保証書範囲条件の設定
        '***********************************************************************
        ' 一つでも設定されている場合、条件を作成
        If Not IIf(keyRec.HosyousyoNoFrom Is Nothing, String.Empty, keyRec.HosyousyoNoFrom) Is String.Empty Or _
           Not IIf(keyRec.HosyousyoNoTo Is Nothing, String.Empty, keyRec.HosyousyoNoTo) Is String.Empty Then

            tmpKbnNoJouken &= (" AND ")

            If Not keyRec.HosyousyoNoFrom Is String.Empty And _
               Not keyRec.HosyousyoNoTo Is String.Empty Then
                ' 両方指定有りはBETWEEN
                tmpKbnNoJouken &= (" j.hosyousyo_no BETWEEN @HOSYOUSYONOFROM AND @HOSYOUSYONOTO")
                listParams.Add(getParamRecord("@HOSYOUSYONOFROM", SqlDbType.VarChar, 10, keyRec.HosyousyoNoFrom))
                listParams.Add(getParamRecord("@HOSYOUSYONOTO", SqlDbType.VarChar, 10, keyRec.HosyousyoNoTo))
            Else
                If Not keyRec.HosyousyoNoFrom Is String.Empty Then
                    ' 保証書Fromのみ
                    tmpKbnNoJouken &= (" j.hosyousyo_no >= @HOSYOUSYONOFROM ")
                    listParams.Add(getParamRecord("@HOSYOUSYONOFROM", SqlDbType.VarChar, 10, keyRec.HosyousyoNoFrom))
                Else
                    ' 保証書Toのみ
                    tmpKbnNoJouken &= (" j.hosyousyo_no <= @HOSYOUSYONOTO ")
                    listParams.Add(getParamRecord("@HOSYOUSYONOTO", SqlDbType.VarChar, 10, keyRec.HosyousyoNoTo))
                End If
            End If

        End If

        '区分と番号のWHERE条件をセット
        sqlCondition.Append(tmpKbnNoJouken)

        '***********************************************************************
        ' 契約NO
        '***********************************************************************
        ' 契約NOの判定
        If keyRec.KeiyakuNo IsNot Nothing AndAlso keyRec.KeiyakuNo <> String.Empty Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@KEIYAKUNO", SqlDbType.VarChar, 20, keyRec.KeiyakuNo & "%"))
            sqlCondition.Append(" ISNULL(j.keiyaku_no, 0) like @KEIYAKUNO")

        End If

        '***********************************************************************
        ' 施主名の設定
        '***********************************************************************
        ' 施主名の判定
        If Not IIf(keyRec.SesyuMei Is Nothing, String.Empty, keyRec.SesyuMei) Is String.Empty Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@SESYUMEI", SqlDbType.VarChar, 50 + 1, keyRec.SesyuMei & "%"))
            sqlCondition.Append(" REPLACE(j.sesyu_mei, ' ', '') like REPLACE (@SESYUMEI, ' ', '') ")
        End If

        '***********************************************************************
        ' 物件住所1+2の設定
        '***********************************************************************
        ' 物件住所1+2の判定
        If Not IIf(keyRec.BukkenJyuusyo12 Is Nothing, String.Empty, keyRec.BukkenJyuusyo12) Is String.Empty Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@BUKKENJYUUSYO12", SqlDbType.VarChar, 64 + 1, keyRec.BukkenJyuusyo12 & "%"))
            sqlCondition.Append(" isnull(j.bukken_jyuusyo1, '') + isnull(j.bukken_jyuusyo2, '') + isnull(j.bukken_jyuusyo3, '') like @BUKKENJYUUSYO12 ")
        End If

        '***********************************************************************
        ' 備考の設定
        '***********************************************************************
        ' 備考の判定
        If Not IIf(keyRec.Bikou Is Nothing, String.Empty, keyRec.Bikou) Is String.Empty Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@BIKOU", SqlDbType.VarChar, 256 + 1, keyRec.Bikou & "%"))
            sqlCondition.Append(" j.bikou like @BIKOU ")
        End If

        '***********************************************************************
        ' 調査会社コードの設定
        '***********************************************************************
        ' 調査会社コードの判定
        If Not IIf(keyRec.TysKaisyaCd Is Nothing, String.Empty, keyRec.TysKaisyaCd) Is String.Empty Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@TYSKAISYACD", SqlDbType.VarChar, 7, keyRec.TysKaisyaCd))
            sqlCondition.Append(" j.tys_kaisya_cd + j.tys_kaisya_jigyousyo_cd = @TYSKAISYACD ")
        End If

        '***********************************************************************
        ' 発行進捗状況の設定
        '***********************************************************************
        'SQL格納用
        Dim tmpHakkouStatusSql As String = String.Empty

        ' 発行進捗状況1の判定
        If keyRec.HakkouStatus1 = 1 Then
            tmpHakkouStatusSql &= (" AND ( ")
            tmpHakkouStatusSql &= (" ( hkt.bukken_jyky = '0' ) ")
        End If

        ' 発行進捗状況2の判定（発行不可）
        If keyRec.HakkouStatus2 = 1 Then
            If tmpHakkouStatusSql = String.Empty Then
                tmpHakkouStatusSql &= (" AND ( ")
            Else
                tmpHakkouStatusSql &= (" OR ")
            End If
            tmpHakkouStatusSql &= (" ( hkt.bukken_jyky = '2' ) ")
        End If

        ' 発行進捗状況3の判定（発行可未依頼）
        If keyRec.HakkouStatus3 = 1 Then
            If tmpHakkouStatusSql = String.Empty Then
                tmpHakkouStatusSql &= (" AND ( ")
            Else
                tmpHakkouStatusSql &= (" OR ")
            End If
            tmpHakkouStatusSql &= (" ( hkt.bukken_jyky = '1' ")
            tmpHakkouStatusSql &= (" AND ISNULL(j.hosyousyo_hak_iraisyo_umu, 0) = 0 ")
            tmpHakkouStatusSql &= (" AND ( ISNULL(ri.hak_irai_time, '') = '' OR ISNULL(ri.hak_irai_time, '') < ISNULL(ri.hak_irai_can_datetime, '') )) ")
        End If

        ' 発行進捗状況4の判定（モール依頼（再発行含）済・未受付）
        If keyRec.HakkouStatus4 = 1 Then
            If tmpHakkouStatusSql = String.Empty Then
                tmpHakkouStatusSql &= (" AND ( ")
            Else
                tmpHakkouStatusSql &= (" OR ")
            End If
            tmpHakkouStatusSql &= (" ( ISNULL(ri.hak_irai_time, '') <> '' ")
            tmpHakkouStatusSql &= (" AND ( ISNULL(ri.hak_irai_uke_datetime, '') = '' OR ISNULL(ri.hak_irai_time, '') > ISNULL(ri.hak_irai_uke_datetime, '') ) ")
            tmpHakkouStatusSql &= (" AND ( ISNULL(ri.hak_irai_can_datetime, '') = '' OR ISNULL(ri.hak_irai_time, '') > ISNULL(ri.hak_irai_can_datetime, '') )) ")
        End If

        ' 発行進捗状況5の判定（モール依頼（再発行含）処理済）
        If keyRec.HakkouStatus5 = 1 Then
            If tmpHakkouStatusSql = String.Empty Then
                tmpHakkouStatusSql &= (" AND ( ")
            Else
                tmpHakkouStatusSql &= (" OR ")
            End If
            tmpHakkouStatusSql &= (" ( ISNULL(ri.hak_irai_time, '') < ISNULL(ri.hak_irai_uke_datetime, '') OR ISNULL(ri.hak_irai_time, '') < ISNULL(ri.hak_irai_can_datetime, '') ) ")
        End If

        ' 発行進捗状況6の判定（初回発行済）
        If keyRec.HakkouStatus6 = 1 Then
            If tmpHakkouStatusSql = String.Empty Then
                tmpHakkouStatusSql &= (" AND ( ")
            Else
                tmpHakkouStatusSql &= (" OR ")
            End If
            tmpHakkouStatusSql &= (" ( hkt.bukken_jyky = '3' ) ")
        End If

        ' 終わりの括弧
        If tmpHakkouStatusSql <> String.Empty Then
            tmpHakkouStatusSql &= (" ) ")
        End If

        '発行進捗状況のWHERE条件をセット
        sqlCondition.Append(tmpHakkouStatusSql)

        '***********************************************************************
        ' 加盟店コードの設定
        '***********************************************************************
        ' 加盟店コードの判定
        If Not IIf(keyRec.KameitenCd Is Nothing, String.Empty, keyRec.KameitenCd) Is String.Empty Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@KAMEITENCD", SqlDbType.VarChar, 5, keyRec.KameitenCd))
            sqlCondition.Append(" j.kameiten_cd = @KAMEITENCD ")
        End If

        '***********************************************************************
        ' 加盟店カナ１の設定
        '***********************************************************************
        ' 加盟店カナ１の判定
        If Not IIf(keyRec.TenmeiKana1 Is Nothing, String.Empty, keyRec.TenmeiKana1) Is String.Empty Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@TENMEIKANA1", SqlDbType.VarChar, 20, keyRec.TenmeiKana1 & "%"))
            sqlCondition.Append(" (k.tenmei_kana1 like @TENMEIKANA1 OR k.tenmei_kana2 like @TENMEIKANA1)")
        End If

        '***********************************************************************
        ' 初回発行方法（発行タイミング）の設定
        '***********************************************************************
        ' 
        If keyRec.HakkouTiming = 0 Then
            ' 依頼書選択
            sqlCondition.Append(" AND ")
            sqlCondition.Append(" j.hosyousyo_hak_houhou = 0 ")
        ElseIf keyRec.HakkouTiming = 1 Then
            ' 自動発行選択
            sqlCondition.Append(" AND ")
            sqlCondition.Append(" j.hosyousyo_hak_houhou = 1 ")
        ElseIf keyRec.HakkouTiming = 2 Then
            ' 地盤モール選択
            sqlCondition.Append(" AND ")
            sqlCondition.Append(" j.hosyousyo_hak_houhou = 2 ")
        End If

        '***********************************************************************
        ' 依頼書着日範囲条件の設定
        '***********************************************************************
        ' 空チェック時
        If keyRec.HosyousyoHakkouIraisyoTyakuDateChk = 1 Then
            sqlCondition.Append(" AND ")
            sqlCondition.Append(" j.hosyousyo_hak_iraisyo_tyk_date IS NULL ")
        Else
            '範囲設定
            createDateHanniSql(sqlCondition, listParams, _
                   keyRec.HosyousyoHakkouIraisyoTyakuDateFrom, keyRec.HosyousyoHakkouIraisyoTyakuDateTo, _
                   "j.hosyousyo_hak_iraisyo_tyk_date", "@HOSYOUSYOHAKKOUIRAISYOTYAKUDATE")
        End If

        '***********************************************************************
        ' 発行日範囲条件の設定
        '***********************************************************************
        ' 空チェック時
        If keyRec.HosyousyoHakkouDateChk = 1 Then
            sqlCondition.Append(" AND ")
            sqlCondition.Append(" j.hosyousyo_hak_date IS NULL ")
        Else
            '範囲設定
            createDateHanniSql(sqlCondition, listParams, _
                   keyRec.HosyousyoHakkouDateFrom, keyRec.HosyousyoHakkouDateTo, _
                   "j.hosyousyo_hak_date", "@HOSYOUSYOHAKKOUDATE")
        End If

        '***********************************************************************
        ' 再発行日範囲条件の設定
        '***********************************************************************
        createDateHanniSql(sqlCondition, listParams, _
                           keyRec.HosyousyoSaihakDateFrom, keyRec.HosyousyoSaihakDateTo, _
                           "j.hosyousyo_saihak_date", "@HOSYOUSYOSAIHAKKOUDATE")

        '***********************************************************************
        ' 発行依頼日範囲条件の設定
        '***********************************************************************
        ' 空チェック時
        If keyRec.HakIraiTimeChk = 1 Then
            sqlCondition.Append(" AND ")
            sqlCondition.Append(" ri.hak_irai_time IS NULL ")
        Else
            '範囲設定
            createDateHanniSql(sqlCondition, listParams, _
                   keyRec.HakIraiTimeFrom, keyRec.HakIraiTimeTo, _
                   "ri.hak_irai_time", "@HAKKOUIRAITIME")
        End If

        '***********************************************************************
        ' 物件依頼日範囲条件の設定
        '***********************************************************************
        createDateHanniSql(sqlCondition, listParams, _
                           keyRec.IraiDateFrom, keyRec.IraiDateTo, _
                           "j.irai_date", "@IRAIDATE")

        '***********************************************************************
        ' 保証期間
        '***********************************************************************
        ' 保証期間の判定
        If keyRec.HosyouKikanMK <> Integer.MinValue OrElse keyRec.HosyouKikanTJ <> Integer.MinValue Then
            '加盟店・物件入力時
            If keyRec.HosyouKikanMK <> Integer.MinValue AndAlso keyRec.HosyouKikanTJ <> Integer.MinValue Then
                sqlCondition.Append(" AND ")

                listParams.Add(getParamRecord("@HOSYOUKIKANMK", SqlDbType.Int, 10, keyRec.HosyouKikanMK))
                listParams.Add(getParamRecord("@HOSYOUKIKANTJ", SqlDbType.Int, 10, keyRec.HosyouKikanTJ))
                sqlCondition.Append(" k.hosyou_kikan = @HOSYOUKIKANMK AND j.hosyou_kikan = @HOSYOUKIKANTJ ")

                ' 加盟店のみ入力時
            ElseIf keyRec.HosyouKikanMK <> Integer.MinValue OrElse keyRec.HosyouKikanTJ = Integer.MinValue Then
                sqlCondition.Append(" AND ")

                listParams.Add(getParamRecord("@HOSYOUKIKANMK", SqlDbType.Int, 10, keyRec.HosyouKikanMK))
                sqlCondition.Append(" k.hosyou_kikan = @HOSYOUKIKANMK ")

                ' 物件のみ入力時
            ElseIf keyRec.HosyouKikanMK = Integer.MinValue OrElse keyRec.HosyouKikanTJ <> Integer.MinValue Then
                sqlCondition.Append(" AND ")

                listParams.Add(getParamRecord("@HOSYOUKIKANTJ", SqlDbType.Int, 10, keyRec.HosyouKikanTJ))
                sqlCondition.Append(" j.hosyou_kikan = @HOSYOUKIKANTJ ")
            End If
        End If

        '***********************************************************************
        ' データ破棄種別
        '***********************************************************************
        ' データ破棄種別の判定
        If keyRec.DataHakiSyubetu = 0 Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@DATAHAKISYUBETU", SqlDbType.Int, 10, keyRec.DataHakiSyubetu))
            sqlCondition.Append(" ISNULL(j.data_haki_syubetu,0) = @DATAHAKISYUBETU ")

        End If

        '***********************************************************************
        ' パラメータの作成
        '***********************************************************************
        ' パラメータの作成
        Dim i As Integer
        Dim cmdParams(listParams.Count - 1) As SqlParameter
        For i = 0 To listParams.Count - 1
            Dim rec As ParamRecord = listParams(i)
            ' 必要な情報をセット
            cmdParams(i) = SQLHelper.MakeParam(rec.Param, rec.DbType, rec.ParamLength, rec.SetData)
        Next

        ' 最終的なSQL文の生成
        sql = String.Format(sqlSb.ToString, sqlCondition.ToString)

        Return cmdParams
    End Function

#End Region

#Region "邸別請求データの取得"
    ''' <summary>
    ''' 邸別請求レコードを取得します（区分・保証書NO単位に全て）
    ''' </summary>
    ''' <param name="kbn">区分</param>
    ''' <param name="hosyousyoNo">保証書NO</param>
    ''' <param name="bunruiCd">分類コード（任意）</param>
    ''' <returns>邸別請求データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetTeibetuSeikyuuData(ByVal kbn As String, _
                                          ByVal hosyousyoNo As String, _
                                          Optional ByVal bunruiCd As String = "") As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTeibetuSeikyuuData", _
                                                    kbn, _
                                                    hosyousyoNo, _
                                                    bunruiCd)

        ' パラメータ
        Const strParamKbn As String = "@KBN"
        Const strParamHosyousyoNo As String = "@HOSYOUSYONO"
        Const strParamBunruiCd As String = "@BUNRUICD"

        Dim sqlSb As New StringBuilder()
        sqlSb.AppendLine("SELECT")
        sqlSb.AppendLine("    t.kbn")
        sqlSb.AppendLine("    , t.hosyousyo_no")
        sqlSb.AppendLine("    , t.bunrui_cd")
        sqlSb.AppendLine("    , t.gamen_hyouji_no")
        sqlSb.AppendLine("    , t.syouhin_cd")
        sqlSb.AppendLine("    , s.syouhin_mei")
        sqlSb.AppendLine("    , t.uri_gaku")
        sqlSb.AppendLine("    , t.siire_gaku")
        sqlSb.AppendLine("    , t.zei_kbn")
        sqlSb.AppendLine("    , t.syouhizei_gaku")
        sqlSb.AppendLine("    , t.siire_syouhizei_gaku")
        sqlSb.AppendLine("    , z.zeiritu")
        sqlSb.AppendLine("    , t.seikyuusyo_hak_date")
        sqlSb.AppendLine("    , t.uri_date")
        sqlSb.AppendLine("    , t.denpyou_uri_date")
        sqlSb.AppendLine("    , t.denpyou_siire_date")
        sqlSb.AppendLine("    , t.seikyuu_umu")
        sqlSb.AppendLine("    , t.kakutei_kbn")
        sqlSb.AppendLine("    , t.uri_keijyou_flg")
        sqlSb.AppendLine("    , t.uri_keijyou_date")
        sqlSb.AppendLine("    , t.bikou")
        sqlSb.AppendLine("    , t.koumuten_seikyuu_gaku")
        sqlSb.AppendLine("    , t.hattyuusyo_gaku")
        sqlSb.AppendLine("    , t.hattyuusyo_kakunin_date")
        sqlSb.AppendLine("    , t.ikkatu_nyuukin_flg")
        sqlSb.AppendLine("    , t.tys_mitsyo_sakusei_date")
        sqlSb.AppendLine("    , t.hattyuusyo_kakutei_flg")
        sqlSb.AppendLine("    , t.seikyuu_saki_cd")
        sqlSb.AppendLine("    , t.seikyuu_saki_brc")
        sqlSb.AppendLine("    , t.seikyuu_saki_kbn")
        sqlSb.AppendLine("    , t.tys_kaisya_cd")
        sqlSb.AppendLine("    , t.tys_kaisya_jigyousyo_cd")
        sqlSb.AppendLine("    , t.add_login_user_id")
        sqlSb.AppendLine("    , t.add_datetime")
        sqlSb.AppendLine("    , ISNULL(t.upd_login_user_id,t.add_login_user_id) AS upd_login_user_id")
        sqlSb.AppendLine("    , ISNULL(t.upd_datetime,t.add_datetime) AS upd_datetime ")
        sqlSb.AppendLine("FROM")
        sqlSb.AppendLine("    jhs_sys.t_teibetu_seikyuu t WITH (READCOMMITTED) ")
        sqlSb.AppendLine("    LEFT OUTER JOIN jhs_sys.m_syouhin s WITH (READCOMMITTED) ")
        sqlSb.AppendLine("        ON s.syouhin_cd = t.syouhin_cd ")
        sqlSb.AppendLine("    LEFT OUTER JOIN jhs_sys.m_syouhizei z WITH (READCOMMITTED) ")
        sqlSb.AppendLine("        ON z.zei_kbn = t.zei_kbn ")
        sqlSb.AppendLine("WHERE")
        sqlSb.AppendLine("    t.kbn = " & strParamKbn)
        sqlSb.AppendLine("    AND t.hosyousyo_no = " & strParamHosyousyoNo)
        ' 分類コードが引数に存在する場合、条件に追加する
        If bunruiCd <> "" Then
            sqlSb.AppendLine("    AND t.bunrui_cd = " & strParamBunruiCd)
        End If
        sqlSb.AppendLine("ORDER BY")
        sqlSb.AppendLine("    t.bunrui_cd")
        sqlSb.AppendLine("    , t.gamen_hyouji_no")
        sqlSb.AppendLine("")

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
                    {SQLHelper.MakeParam(strParamKbn, SqlDbType.Char, 1, kbn), _
                     SQLHelper.MakeParam(strParamHosyousyoNo, SqlDbType.VarChar, 10, hosyousyoNo), _
                     SQLHelper.MakeParam(strParamBunruiCd, SqlDbType.VarChar, 3, bunruiCd)}

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(sqlSb.ToString, commandParameters)

    End Function

    ''' <summary>
    ''' 邸別請求レコードを取得します(１件のみ取得)
    ''' </summary>
    ''' <param name="kbn">区分</param>
    ''' <param name="hosyousyoNo">保証書NO</param>
    ''' <param name="bunruiCd">分類コード</param>
    ''' <param name="gamenHyoujiNo">画面表示NO</param>
    ''' <returns>邸別請求データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetTeibetuSeikyuuData(ByVal kbn As String, _
                                          ByVal hosyousyoNo As String, _
                                          ByVal bunruiCd As String, _
                                          ByVal gamenHyoujiNo As Integer) As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTeibetuSeikyuuData", _
                                            kbn, _
                                            hosyousyoNo, _
                                            bunruiCd, _
                                            gamenHyoujiNo)

        ' パラメータ
        Const strParamKbn As String = "@KBN"
        Const strParamHosyousyoNo As String = "@HOSYOUSYONO"
        Const strParamBunruiCd As String = "@BUNRUICD"
        Const strParamGamenHyoujiNo As String = "@GAMENHYOUJINO"

        Dim sqlSb As New StringBuilder()
        sqlSb.AppendLine("SELECT")
        sqlSb.AppendLine("    * ")
        sqlSb.AppendLine("FROM")
        sqlSb.AppendLine("    jhs_sys.t_teibetu_seikyuu ")
        sqlSb.AppendLine("WHERE")
        sqlSb.AppendLine("    kbn = " & strParamKbn)
        sqlSb.AppendLine("    AND hosyousyo_no = " & strParamHosyousyoNo)
        If bunruiCd = "110" Or bunruiCd = "115" Then
            sqlSb.AppendLine("    AND bunrui_cd IN ('110','115') ")
        Else
            sqlSb.AppendLine("    AND bunrui_cd = " & strParamBunruiCd)
        End If
        sqlSb.AppendLine("    AND gamen_hyouji_no =" & strParamGamenHyoujiNo)
        sqlSb.AppendLine("")

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKbn, SqlDbType.Char, 1, kbn), _
             SQLHelper.MakeParam(strParamHosyousyoNo, SqlDbType.VarChar, 10, hosyousyoNo), _
             SQLHelper.MakeParam(strParamBunruiCd, SqlDbType.VarChar, 3, bunruiCd), _
             SQLHelper.MakeParam(strParamGamenHyoujiNo, SqlDbType.Int, 1, gamenHyoujiNo)}

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(sqlSb.ToString, commandParameters)

    End Function

    ''' <summary>
    ''' 邸別請求レコードのKeyを取得します（区分・保証書NO単位に全て）
    ''' </summary>
    ''' <param name="kbn">区分</param>
    ''' <param name="hosyousyoNo">保証書NO</param>
    ''' <returns>邸別請求データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetTeibetuSeikyuuDataKey(ByVal kbn As String, _
                                             ByVal hosyousyoNo As String) As JibanDataSet.TeibetuSeikyuuKeyTableDataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTeibetuSeikyuuDataKey", _
                                                    kbn, _
                                                    hosyousyoNo)

        ' パラメータ
        Const strParamKbn As String = "@KBN"
        Const strParamHosyousyoNo As String = "@HOSYOUSYONO"

        Dim commandTextSb As New StringBuilder()
        commandTextSb.Append(" SELECT " & _
                                    "   t.kbn, " & _
                                    "   t.hosyousyo_no, " & _
                                    "   t.bunrui_cd, " & _
                                    "   t.gamen_hyouji_no " & _
                                    " FROM " & _
                                    "    t_teibetu_seikyuu t WITH (UPDLOCK) " & _
                                    " WHERE " & _
                                    "    t.kbn = " & strParamKbn & _
                                    " AND " & _
                                    "    t.hosyousyo_no = " & strParamHosyousyoNo)

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKbn, SqlDbType.Char, 1, kbn), _
             SQLHelper.MakeParam(strParamHosyousyoNo, SqlDbType.VarChar, 10, hosyousyoNo)}

        commandTextSb.Append(" ORDER BY t.bunrui_cd,t.gamen_hyouji_no ")

        ' データの取得
        Dim JibanDataSet As New JibanDataSet()

        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            JibanDataSet, JibanDataSet.TeibetuSeikyuuKeyTable.TableName, commandParameters)

        Dim teibetuTable As JibanDataSet.TeibetuSeikyuuKeyTableDataTable = JibanDataSet.TeibetuSeikyuuKeyTable

        Return teibetuTable

    End Function

    ''' <summary>
    ''' 邸別請求レコードの画面表示NOの最大値を取得します
    ''' </summary>
    ''' <param name="kbn">区分</param>
    ''' <param name="hosyousyoNo">保証書NO</param>
    ''' <param name="bunruiCd">分類コード</param>
    ''' <returns>邸別請求データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetTeibetuSeikyuuMaxNo(ByVal kbn As String, _
                                           ByVal hosyousyoNo As String, _
                                           ByVal bunruiCd As String) As Object

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTeibetuSeikyuuMaxNo", _
                                                    kbn, _
                                                    hosyousyoNo, _
                                                    bunruiCd)

        ' パラメータ
        Const strParamKbn As String = "@KBN"
        Const strParamHosyousyoNo As String = "@HOSYOUSYONO"
        Const strParamBunruiCd As String = "@BUNRUICD"

        Dim commandTextSb As New StringBuilder()
        commandTextSb.Append(" SELECT " & _
                                    "   MAX(t.gamen_hyouji_no) " & _
                                    " FROM " & _
                                    "    t_teibetu_seikyuu t WITH (UPDLOCK) " & _
                                    " WHERE " & _
                                    "    t.kbn = " & strParamKbn & _
                                    " AND " & _
                                    "    t.hosyousyo_no = " & strParamHosyousyoNo & _
                                    " AND " & _
                                    "    t.bunrui_cd = " & strParamBunruiCd)

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKbn, SqlDbType.Char, 1, kbn), _
             SQLHelper.MakeParam(strParamHosyousyoNo, SqlDbType.VarChar, 10, hosyousyoNo), _
             SQLHelper.MakeParam(strParamBunruiCd, SqlDbType.VarChar, 3, bunruiCd)}

        'データの取得
        Dim retValue As Object = SQLHelper.ExecuteScalar(connStr, CommandType.Text, commandTextSb.ToString(), commandParameters)

        If retValue Is DBNull.Value Then
            Return 0
        End If

        Return retValue

    End Function

    ''' <summary>
    ''' 該当物件情報に関連する邸別請求データ(商品1～3)の保証有無を取得し、いずれかが保証有の場合1を、以外の場合0を返す。
    ''' ※データが存在しない場合、""(空白)を返す。
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strBangou">番号</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetHosyouSyouhinUmu( _
                                    ByVal strKbn As String, _
                                    ByVal strBangou As String _
                                ) As String

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHosyouSyouhinUmu", strKbn, strBangou)

        Dim blnRes As Boolean = False

        Dim dTblRes As New DataTable
        Dim strRetHosyouUmu As String = String.Empty

        ' パラメータ
        Const strPrmKbn As String = "@KBN" '区分
        Const strPrmBangou As String = "@BANGOU" '番号

        Dim cmdTextSb As New StringBuilder

        'SQL生成
        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append("      TS.kbn ")
        cmdTextSb.Append("    , TS.hosyousyo_no ")
        cmdTextSb.Append("    , MAX( ")
        cmdTextSb.Append("           CASE ")
        cmdTextSb.Append("                WHEN ISNULL(MS.hosyou_umu, '') = '' ")
        cmdTextSb.Append("                THEN '0' ")
        cmdTextSb.Append("                WHEN MS.hosyou_umu = '0' ")
        cmdTextSb.Append("                THEN '0' ")
        cmdTextSb.Append("                WHEN MS.hosyou_umu = '1' ")
        cmdTextSb.Append("                THEN '1' ")
        cmdTextSb.Append("                ELSE '0' ")
        cmdTextSb.Append("           END) AS teibetu_hosyou_syouhin_umu ")
        cmdTextSb.Append(" FROM ")
        cmdTextSb.Append("      t_teibetu_seikyuu TS ")
        cmdTextSb.Append("           INNER JOIN m_syouhin MS ")
        cmdTextSb.Append("             ON TS.syouhin_cd = MS.syouhin_cd ")
        cmdTextSb.Append(" WHERE ")
        cmdTextSb.Append("      1=1 ")
        cmdTextSb.Append("  AND TS.kbn = " & strPrmKbn)
        cmdTextSb.Append("  AND TS.hosyousyo_no = " & strPrmBangou)
        cmdTextSb.Append("  AND TS.bunrui_cd IN('100', '110', '115', '120') ")
        cmdTextSb.Append(" GROUP BY ")
        cmdTextSb.Append("      TS.kbn ")
        cmdTextSb.Append("    , TS.hosyousyo_no ")

        ' パラメータへ設定
        Dim cmdPrms() As SqlParameter = _
            {SQLHelper.MakeParam(strPrmKbn, SqlDbType.Char, 1, strKbn), _
             SQLHelper.MakeParam(strPrmBangou, SqlDbType.VarChar, 10, strBangou)}

        'データ取得＆返却
        dTblRes = cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdPrms)
        If dTblRes.Rows.Count > 0 Then
            strRetHosyouUmu = dTblRes.Rows(0)("teibetu_hosyou_syouhin_umu").ToString
        End If

        Return strRetHosyouUmu
    End Function

#End Region

#Region "邸別入金データの取得"

    ''' <summary>
    ''' 邸別入金レコードを取得します
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strHosyousyoNo">保証書NO</param>
    ''' <returns>データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetTeibetuNyuukinDataKey(ByVal strKbn As String _
                                            , ByVal strHosyousyoNo As String) As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTeibetuNyuukinData", _
                                            strKbn, _
                                            strHosyousyoNo)

        'パラメータ
        Const strParamKbn As String = "@KBN"
        Const strParamHosyousyoNo As String = "@HOSYOUSYONO"

        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.Append(" SELECT")
        cmdTextSb.Append("      kbn")
        cmdTextSb.Append("    , hosyousyo_no")
        cmdTextSb.Append("    , bunrui_cd")
        cmdTextSb.Append("    , gamen_hyouji_no")
        cmdTextSb.Append("    , ISNULL(zeikomi_nyuukin_gaku, 0) - ISNULL(zeikomi_henkin_gaku, 0) AS nyuukin_gaku")
        cmdTextSb.Append("    , zeikomi_nyuukin_gaku")
        cmdTextSb.Append("    , zeikomi_henkin_gaku")
        cmdTextSb.Append("    , add_login_user_id")
        cmdTextSb.Append("    , add_datetime")
        cmdTextSb.Append("    , upd_login_user_id")
        cmdTextSb.Append("    , upd_datetime")
        cmdTextSb.Append(" FROM")
        cmdTextSb.Append("      t_teibetu_nyuukin")
        cmdTextSb.Append("      WITH (READCOMMITTED)")
        cmdTextSb.Append(" WHERE")
        cmdTextSb.Append("      kbn = " & strParamKbn)
        cmdTextSb.Append("  AND hosyousyo_no = " & strParamHosyousyoNo)

        ' パラメータへ設定
        Dim cmdParams() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKbn, SqlDbType.Char, 1, strKbn), _
             SQLHelper.MakeParam(strParamHosyousyoNo, SqlDbType.VarChar, 10, strHosyousyoNo)}

        cmdTextSb.Append(" ORDER BY bunrui_cd")
        cmdTextSb.Append("        , gamen_hyouji_no")

        Dim cmnDtAcc As New CmnDataAccess

        'データ取得＆返却 
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

    ''' <summary>
    ''' 邸別入金レコードを取得します
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strHosyousyoNo">保証書NO</param>
    ''' <param name="strBunruiCd">分類コード（任意）</param>
    ''' <param name="intGamenHyoujiNo">画面表示NO（任意）</param>
    ''' <returns>データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetTeibetuNyuukinDataKey(ByVal strKbn As String _
                                            , ByVal strHosyousyoNo As String _
                                            , ByVal strBunruiCd As String _
                                            , ByVal intGamenHyoujiNo As Integer) As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTeibetuNyuukinData", _
                                            strKbn, _
                                            strHosyousyoNo, _
                                            strBunruiCd, _
                                            intGamenHyoujiNo)

        'パラメータ
        Const strParamKbn As String = "@KBN"
        Const strParamHosyousyoNo As String = "@HOSYOUSYONO"
        Const strParamBunruiCd As String = "@BUNRUICD"
        Const strParamGamenHyoujiNo As String = "@GAMENHYOUJINO"

        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.Append(" SELECT")
        cmdTextSb.Append("      kbn")
        cmdTextSb.Append("    , hosyousyo_no")
        cmdTextSb.Append("    , bunrui_cd")
        cmdTextSb.Append("    , gamen_hyouji_no")
        cmdTextSb.Append("    , ISNULL(zeikomi_nyuukin_gaku, 0) - ISNULL(zeikomi_henkin_gaku, 0) AS nyuukin_gaku")
        cmdTextSb.Append("    , zeikomi_nyuukin_gaku")
        cmdTextSb.Append("    , zeikomi_henkin_gaku")
        cmdTextSb.Append("    , add_login_user_id")
        cmdTextSb.Append("    , add_datetime")
        cmdTextSb.Append("    , upd_login_user_id")
        cmdTextSb.Append("    , upd_datetime")
        cmdTextSb.Append(" FROM")
        cmdTextSb.Append("      t_teibetu_nyuukin")
        cmdTextSb.Append("      WITH (READCOMMITTED)")
        cmdTextSb.Append(" WHERE")
        cmdTextSb.Append("      kbn = " & strParamKbn)
        cmdTextSb.Append("  AND hosyousyo_no = " & strParamHosyousyoNo)
        cmdTextSb.Append(" AND LEFT(bunrui_cd, 2) = LEFT(" & strParamBunruiCd & ", 2)")
        cmdTextSb.Append(" AND gamen_hyouji_no = " & strParamGamenHyoujiNo)

        cmdTextSb.Append(" ORDER BY bunrui_cd")
        cmdTextSb.Append("        , gamen_hyouji_no")

        Dim cmdParams() As SqlParameter = _
        {SQLHelper.MakeParam(strParamKbn, SqlDbType.Char, 1, strKbn), _
         SQLHelper.MakeParam(strParamHosyousyoNo, SqlDbType.VarChar, 10, strHosyousyoNo), _
         SQLHelper.MakeParam(strParamBunruiCd, SqlDbType.VarChar, 3, strBunruiCd), _
         SQLHelper.MakeParam(strParamGamenHyoujiNo, SqlDbType.Int, 4, intGamenHyoujiNo)}

        Dim cmnDtAcc As New CmnDataAccess

        'データ取得＆返却 
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function


    ''' <summary>
    ''' 邸別入金レコードの分類ごとの入金額を取得します
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strHosyousyoNo">保証書NO</param>
    ''' <returns>データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetTeibetuNyuukinData(ByVal strKbn As String _
                                        , ByVal strHosyousyoNo As String) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTeibetuNyuukinData", _
                                                   strKbn, _
                                                   strHosyousyoNo)
        ' パラメータ
        Const strParamKbn As String = "@KBN"
        Const strParamHosyousyoNo As String = "@HOSYOUSYONO"
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.Append(" SELECT")
        cmdTextSb.Append("      kbn")
        cmdTextSb.Append("    , hosyousyo_no")
        cmdTextSb.Append("    , bunrui_cd")
        cmdTextSb.Append("    , SUM(ISNULL(zeikomi_nyuukin_gaku, 0) - ISNULL(zeikomi_henkin_gaku, 0)) nyuukin_gaku")
        cmdTextSb.Append(" FROM")
        cmdTextSb.Append("     (SELECT")
        cmdTextSb.Append("           kbn")
        cmdTextSb.Append("         , hosyousyo_no")
        cmdTextSb.Append("         ,")
        cmdTextSb.Append("           CASE")
        cmdTextSb.Append("                WHEN bunrui_cd in('100', '110', '115', '180')")
        cmdTextSb.Append("                THEN '100'")
        cmdTextSb.Append("                ELSE bunrui_cd")
        cmdTextSb.Append("           END bunrui_cd")
        cmdTextSb.Append("         , zeikomi_nyuukin_gaku")
        cmdTextSb.Append("         , zeikomi_henkin_gaku")
        cmdTextSb.Append("         , add_login_user_id")
        cmdTextSb.Append("         , add_datetime")
        cmdTextSb.Append("         , upd_login_user_id")
        cmdTextSb.Append("         , upd_datetime")
        cmdTextSb.Append("      FROM")
        cmdTextSb.Append("           t_teibetu_nyuukin WITH (READCOMMITTED)")
        cmdTextSb.Append("      WHERE")
        cmdTextSb.Append("           kbn = " & strParamKbn)
        cmdTextSb.Append("       AND hosyousyo_no = " & strParamHosyousyoNo)
        cmdTextSb.Append("     ) T")
        cmdTextSb.Append(" GROUP BY")
        cmdTextSb.Append("      kbn")
        cmdTextSb.Append("    , hosyousyo_no")
        cmdTextSb.Append("    , bunrui_cd")

        ' パラメータへ設定
        Dim cmdParams() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKbn, SqlDbType.Char, 1, strKbn), _
             SQLHelper.MakeParam(strParamHosyousyoNo, SqlDbType.VarChar, 10, strHosyousyoNo)}

        Dim cmnDtAcc As New CmnDataAccess

        'データ取得＆返却 
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)
    End Function

    ''' <summary>
    ''' 地盤と邸別請求に紐づく邸別入金データを取得（地盤にあって邸別請求にない邸別入金データも取得）
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strHosyousyoNo">保証書NO</param>
    ''' <returns>データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetTeibetuSeikyuuNyuukinData(ByVal strKbn As String _
                                                , ByVal strHosyousyoNo As String) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTeibetuSeikyuuNyuukinData", _
                                                   strKbn, _
                                                   strHosyousyoNo)
        ' パラメータ
        Const strParamKbn As String = "@KBN"
        Const strParamHosyousyoNo As String = "@HOSYOUSYONO"
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.Append(" SELECT")
        cmdTextSb.Append("      *")
        cmdTextSb.Append(" FROM")
        cmdTextSb.Append("     (SELECT")
        cmdTextSb.Append("           TJ.kbn")
        cmdTextSb.Append("         , TJ.hosyousyo_no")
        cmdTextSb.Append("         , ISNULL(TS.bunrui_cd, TN.bunrui_cd) bunrui_cd")
        cmdTextSb.Append("         , ISNULL(TS.gamen_hyouji_no, TN.bunrui_cd) gamen_hyouji_no")
        cmdTextSb.Append("         , ISNULL(TN.zeikomi_nyuukin_gaku, 0) - ISNULL(TN.zeikomi_henkin_gaku, 0) AS nyuukin_gaku")
        cmdTextSb.Append("         , TN.zeikomi_nyuukin_gaku")
        cmdTextSb.Append("         , TN.zeikomi_henkin_gaku")
        cmdTextSb.Append("         , TN.add_login_user_id")
        cmdTextSb.Append("         , TN.add_datetime")
        cmdTextSb.Append("         , TN.upd_login_user_id")
        cmdTextSb.Append("         , TN.upd_datetime")
        cmdTextSb.Append("      FROM")
        cmdTextSb.Append("           t_jiban TJ")
        cmdTextSb.Append("                LEFT OUTER JOIN t_teibetu_seikyuu TS")
        cmdTextSb.Append("                  ON TJ.kbn = TS.kbn")
        cmdTextSb.Append("                 AND TJ.hosyousyo_no = TS.hosyousyo_no")
        cmdTextSb.Append("                LEFT OUTER JOIN t_teibetu_nyuukin TN")
        cmdTextSb.Append("                  ON TJ.kbn = TN.kbn")
        cmdTextSb.Append("                 AND TJ.hosyousyo_no = TN.hosyousyo_no")
        cmdTextSb.Append("                 AND LEFT (TS.bunrui_cd, 2) = LEFT (TN.bunrui_cd, 2)")
        cmdTextSb.Append("                 AND TS.gamen_hyouji_no = TN.gamen_hyouji_no")
        cmdTextSb.Append("      WHERE")
        cmdTextSb.Append("            TJ.kbn = " & strParamKbn)
        cmdTextSb.Append("        AND TJ.hosyousyo_no = " & strParamHosyousyoNo)
        cmdTextSb.Append("      UNION")
        cmdTextSb.Append("      SELECT")
        cmdTextSb.Append("           TJ.kbn")
        cmdTextSb.Append("         , TJ.hosyousyo_no")
        cmdTextSb.Append("         , ISNULL(TS.bunrui_cd, TN.bunrui_cd) bunrui_cd")
        cmdTextSb.Append("         , ISNULL(TS.gamen_hyouji_no, TN.gamen_hyouji_no) gamen_hyouji_no")
        cmdTextSb.Append("         , ISNULL(TN.zeikomi_nyuukin_gaku, 0) - ISNULL(TN.zeikomi_henkin_gaku, 0) AS nyuukin_gaku")
        cmdTextSb.Append("         , TN.zeikomi_nyuukin_gaku")
        cmdTextSb.Append("         , TN.zeikomi_henkin_gaku")
        cmdTextSb.Append("         , TN.add_login_user_id")
        cmdTextSb.Append("         , TN.add_datetime")
        cmdTextSb.Append("         , TN.upd_login_user_id")
        cmdTextSb.Append("         , TN.upd_datetime")
        cmdTextSb.Append("      FROM")
        cmdTextSb.Append("           t_jiban TJ")
        cmdTextSb.Append("                LEFT OUTER JOIN t_teibetu_nyuukin TN")
        cmdTextSb.Append("                  ON TJ.kbn = TN.kbn")
        cmdTextSb.Append("                 AND TJ.hosyousyo_no = TN.hosyousyo_no")
        cmdTextSb.Append("                LEFT OUTER JOIN t_teibetu_seikyuu TS")
        cmdTextSb.Append("                  ON TJ.kbn = TS.kbn")
        cmdTextSb.Append("                 AND TJ.hosyousyo_no = TS.hosyousyo_no")
        cmdTextSb.Append("                 AND LEFT (TN.bunrui_cd, 2) = LEFT (TS.bunrui_cd, 2)")
        cmdTextSb.Append("                 AND TN.gamen_hyouji_no = TS.gamen_hyouji_no")
        cmdTextSb.Append("      WHERE")
        cmdTextSb.Append("            TJ.kbn = " & strParamKbn)
        cmdTextSb.Append("        AND TJ.hosyousyo_no = " & strParamHosyousyoNo)
        cmdTextSb.Append("     )")
        cmdTextSb.Append("      WK")
        cmdTextSb.Append(" ORDER BY")
        cmdTextSb.Append("      kbn")
        cmdTextSb.Append("    , hosyousyo_no")
        cmdTextSb.Append("    , LEFT (bunrui_cd, 2)")
        cmdTextSb.Append("    , gamen_hyouji_no")
        ' パラメータへ設定
        Dim cmdParams() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKbn, SqlDbType.Char, 1, strKbn), _
             SQLHelper.MakeParam(strParamHosyousyoNo, SqlDbType.VarChar, 10, strHosyousyoNo)}

        Dim cmnDtAcc As New CmnDataAccess

        'データ取得＆返却 
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function
#End Region

#Region "地盤データ登録"
    ''' <summary>
    ''' 地盤テーブルへデータを反映します
    ''' </summary>
    ''' <param name="kbn">区分</param>
    ''' <param name="hosyousyoNo">保証書NO</param>
    ''' <param name="addLoginUserId">登録者ID</param>
    ''' <returns>登録結果</returns>
    ''' <remarks></remarks>
    Public Function InsertJibanData(ByVal kbn As String, _
                                    ByVal hosyousyoNo As String, _
                                    ByVal addLoginUserId As String, _
                                    ByVal sinkiTourokuMotoKbnType As EarthEnum.EnumSinkiTourokuMotoKbnType _
                                    ) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".InsertJibanData", _
                                    kbn, _
                                    hosyousyoNo, _
                                    addLoginUserId, _
                                    sinkiTourokuMotoKbnType)

        Dim intResult As Integer = 0
        Dim commandTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        Dim strKousinsya As String = strLogic.GetKousinsya(addLoginUserId, DateTime.Now) '更新者

        ' 初期設定
        commandTextSb.Append(" INSERT INTO t_jiban ")
        commandTextSb.Append(" (   kbn, ")
        commandTextSb.Append("     hosyousyo_no, ")
        commandTextSb.Append("     data_haki_syubetu, ")
        commandTextSb.Append("     syouhin_kbn, ")
        commandTextSb.Append("     tys_gaiyou, ")
        commandTextSb.Append("     tenpu_heimenzu, ")
        commandTextSb.Append("     tenpu_ritumenzu, ")
        commandTextSb.Append("     tenpu_ks_husezu, ")
        commandTextSb.Append("     tenpu_danmenzu, ")
        commandTextSb.Append("     tenpu_kukeizu, ")
        commandTextSb.Append("     douji_irai_tousuu, ")
        commandTextSb.Append("     kasi_umu, ")
        commandTextSb.Append("     henkin_syori_flg, ")
        commandTextSb.Append("     keiyu, ")
        commandTextSb.Append("     add_login_user_id, ")
        commandTextSb.Append("     add_datetime, ")
        commandTextSb.Append("     kousinsya, ")
        commandTextSb.Append("     sinki_touroku_moto_kbn, ")
        commandTextSb.Append("     koj_hantei_kekka_flg ")
        commandTextSb.Append(" )VALUES( ")
        commandTextSb.Append("     @KBN, ")
        commandTextSb.Append("     @HOSYOUSYONO, ")
        commandTextSb.Append("     0, ")
        commandTextSb.Append("     9, ")
        commandTextSb.Append("     9, ")
        commandTextSb.Append("     0, ")
        commandTextSb.Append("     0, ")
        commandTextSb.Append("     0, ")
        commandTextSb.Append("     0, ")
        commandTextSb.Append("     0, ")
        commandTextSb.Append("     0, ")
        commandTextSb.Append("     0, ")
        commandTextSb.Append("     0, ")
        commandTextSb.Append("     0, ")
        commandTextSb.Append("     @ADDLOGINUSERID, ")
        commandTextSb.Append("     @ADDDATETIME, ")
        commandTextSb.Append("     @KOUSINSYA, ")
        commandTextSb.Append("     @TOUROKUMOTO, ")
        commandTextSb.Append("     0 ")
        commandTextSb.Append(" ) ")

        cmdParams = New SqlParameter() { _
                SQLHelper.MakeParam("@KBN", SqlDbType.Char, 1, kbn), _
                SQLHelper.MakeParam("@HOSYOUSYONO", SqlDbType.VarChar, 10, hosyousyoNo), _
                SQLHelper.MakeParam("@ADDLOGINUSERID", SqlDbType.VarChar, 30, addLoginUserId), _
                SQLHelper.MakeParam("@ADDDATETIME", SqlDbType.DateTime, 16, DateTime.Now), _
                SQLHelper.MakeParam("@KOUSINSYA", SqlDbType.VarChar, 30, strKousinsya), _
                SQLHelper.MakeParam("@TOUROKUMOTO", SqlDbType.Int, 10, sinkiTourokuMotoKbnType) _
                }

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    cmdParams)

        Return intResult

    End Function
#End Region

#Region "地盤データ更新"
    ''' <summary>
    ''' 地盤テーブルへデータを反映します
    ''' </summary>
    ''' <param name="sql">更新SQL</param>
    ''' <returns>更新に必要なパラメータ情報</returns>
    ''' <remarks>★本処理は固有のテーブルに依存しない為、各種テーブルの更新に使用可</remarks>
    Public Function UpdateJibanData(ByVal sql As String, _
                                    ByVal paramList As List(Of ParamRecord)) As Boolean

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdateJibanData", _
                            sql, _
                            paramList)

        Dim intResult As Integer = 0
        Dim cmdParams(paramList.Count - 1) As SqlClient.SqlParameter
        Dim i As Integer

        For i = 0 To paramList.Count - 1
            Dim rec As ParamRecord = paramList(i)
            ' 必要な情報をセット
            cmdParams(i) = SQLHelper.MakeParam(rec.Param, rec.DbType, rec.ParamLength, rec.SetData)
        Next

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    sql, _
                                    cmdParams)

        ' 更新に失敗した場合、False
        If intResult < 1 Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 地盤テーブルの排他チェックを行います
    ''' </summary>
    ''' <param name="sql">SQL</param>
    ''' <param name="paramList">パラメータのリスト</param>
    ''' <returns>地盤データ排他レコード（更新日付相違有りの場合取得されます）</returns>
    ''' <remarks></remarks>
    Public Function CheckHaita(ByVal sql As String, _
                               ByVal paramList As List(Of ParamRecord)) As JibanDataSet.JibanHaitaTableDataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CheckHaita", _
                                                    sql, _
                                                    paramList)

        Dim intResult As Integer = 0
        Dim cmdParams(paramList.Count - 1) As SqlClient.SqlParameter
        Dim i As Integer

        For i = 0 To paramList.Count - 1
            Dim rec As ParamRecord = paramList(i)
            ' 必要な情報をセット
            cmdParams(i) = SQLHelper.MakeParam(rec.Param, rec.DbType, rec.ParamLength, rec.SetData)
        Next

        ' データの取得
        Dim JibanDataSet As New JibanDataSet()

        SQLHelper.FillDataset(connStr, CommandType.Text, sql, _
            JibanDataSet, JibanDataSet.JibanHaitaTable.TableName, cmdParams)

        Dim haitaTable As JibanDataSet.JibanHaitaTableDataTable = JibanDataSet.JibanHaitaTable

        Return haitaTable

    End Function

    ''' <summary>
    ''' 経由を更新します
    ''' </summary>
    ''' <param name="kbn">区分</param>
    ''' <param name="hosyousyoNo">保証書NO</param>
    ''' <param name="addLoginUserId">ログインユーザーID</param>
    ''' <param name="keiyu">経由</param>
    ''' <returns>更新結果</returns>
    ''' <remarks></remarks>
    Public Function UpdateJibanKeiyu(ByVal kbn As String, _
                                   ByVal hosyousyoNo As String, _
                                   ByVal addLoginUserId As String, _
                                   ByVal keiyu As Integer, _
                                   Optional ByVal updateDatetime As DateTime = Nothing) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdateJibanKeiyu", _
                                            kbn, _
                                            hosyousyoNo, _
                                            addLoginUserId, _
                                            keiyu)

        Dim intResult As Integer = 0
        Dim commandTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        ' 初期設定
        commandTextSb.Append(" UPDATE t_jiban ")
        commandTextSb.Append(" SET keiyu = @KEIYU, ")
        commandTextSb.Append("     upd_login_user_id = @UPDLOGINUSERID, ")
        commandTextSb.Append("     upd_datetime     = @UPDDATETIME ")
        commandTextSb.Append(" WHERE kbn             = @KBN ")
        commandTextSb.Append(" AND   hosyousyo_no    = @HOSYOUSYONO ")

        If updateDatetime = Nothing Then
            updateDatetime = DateTime.Now
        End If

        cmdParams = New SqlParameter() { _
                SQLHelper.MakeParam("@KBN", SqlDbType.Char, 1, kbn), _
                SQLHelper.MakeParam("@HOSYOUSYONO", SqlDbType.VarChar, 10, hosyousyoNo), _
                SQLHelper.MakeParam("@KEIYU", SqlDbType.Int, 4, keiyu), _
                SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, addLoginUserId), _
                SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 16, updateDatetime)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    cmdParams)

        Return intResult

    End Function

    ''' <summary>
    ''' 発行依頼キャンセル日時を更新します
    ''' </summary>
    ''' <param name="kbn">区分</param>
    ''' <param name="hosyousyoNo">保証書NO</param>
    ''' <param name="addLoginUserId">ログインユーザーID</param>
    ''' <param name="cancelDatetime">発行依頼キャンセル日時</param>
    ''' <returns>更新結果</returns>
    ''' <remarks></remarks>
    Public Function UpdateJibanIraiCancel(ByVal kbn As String, _
                                   ByVal hosyousyoNo As String, _
                                   ByVal addLoginUserId As String, _
                                   ByVal cancelDatetime As DateTime, _
                                   Optional ByVal updateDatetime As DateTime = Nothing) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdateJibancancelDatetime", _
                                            kbn, _
                                            hosyousyoNo, _
                                            addLoginUserId, _
                                            cancelDatetime)

        Dim intResult As Integer = 0
        Dim commandTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        ' 初期設定
        commandTextSb.Append(" UPDATE t_jiban ")
        commandTextSb.Append(" SET hak_irai_can_datetime = @HAKIRAICANDATETIME, ")
        commandTextSb.Append("     upd_login_user_id     = @UPDLOGINUSERID, ")
        commandTextSb.Append("     upd_datetime          = @UPDDATETIME ")
        commandTextSb.Append(" WHERE kbn          = @KBN ")
        commandTextSb.Append(" AND   hosyousyo_no = @HOSYOUSYONO ")

        If updateDatetime = Nothing Then
            updateDatetime = DateTime.Now
        End If

        cmdParams = New SqlParameter() { _
                SQLHelper.MakeParam("@KBN", SqlDbType.Char, 1, kbn), _
                SQLHelper.MakeParam("@HOSYOUSYONO", SqlDbType.VarChar, 10, hosyousyoNo), _
                SQLHelper.MakeParam("@HAKIRAICANDATETIME", SqlDbType.DateTime, 16, cancelDatetime), _
                SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, addLoginUserId), _
                SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 16, updateDatetime)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    cmdParams)

        Return intResult

    End Function
#End Region

#Region "地盤データ・邸別データ削除"
    ''' <summary>
    ''' 地盤テーブルを削除します
    ''' </summary>
    ''' <param name="kbn">区分</param>
    ''' <param name="hosyousyoNo">保証書NO</param>
    ''' <param name="deleteTeibetu">邸別請求レコードを削除する場合：True</param>
    ''' <param name="loginUserId">更新ログインユーザーID(削除処理トリガ用)</param>
    ''' <returns>削除結果 True:成功-コミットしてください False:失敗</returns>
    ''' <remarks>地盤データと邸別請求の同期を保つため、呼び出し元でトランザクション制御を行って下さい</remarks>
    Public Function DeleteJibanData(ByVal kbn As String, _
                                    ByVal hosyousyoNo As String, _
                                    ByVal deleteTeibetu As Boolean, _
                                    ByVal loginUserId As String) As Boolean

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".DeleteJibanData", _
                                    kbn, _
                                    hosyousyoNo, _
                                    deleteTeibetu)

        Dim intResult As Integer = 0
        Dim commandTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter = { _
            SQLHelper.MakeParam("@KBN", SqlDbType.Char, 1, kbn), _
            SQLHelper.MakeParam("@HOSYOUSYONO", SqlDbType.VarChar, 10, hosyousyoNo)}

        '*****************************************************************
        ' 地盤テーブルの削除
        '*****************************************************************
        commandTextSb.Append(" DELETE FROM t_jiban ")
        commandTextSb.Append(" WHERE kbn = @KBN AND hosyousyo_no = @HOSYOUSYONO ")

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    cmdParams)

        ' 処理レコード数が0以下の場合、Falseで削除処理終了
        If intResult <= 0 Then
            Return False
        End If

        ' 邸別請求テーブルを同時削除する場合
        If deleteTeibetu = True Then
            '*****************************************************************
            ' 邸別請求テーブルの削除
            '*****************************************************************
            commandTextSb = New StringBuilder()
            commandTextSb.Append(CreateUserInfoTempTableSQL(loginUserId))   '削除トリガ用ローカル一時テーブル生成SQLを追加
            commandTextSb.Append(" DELETE FROM t_teibetu_seikyuu ")
            commandTextSb.Append(" WHERE kbn = @KBN AND hosyousyo_no = @HOSYOUSYONO ")

            ' クエリ実行
            intResult = ExecuteNonQuery(connStr, _
                                        CommandType.Text, _
                                        commandTextSb.ToString(), _
                                        cmdParams)

            ' 処理レコード数が0以下の場合、Falseで削除処理終了
            If intResult <= 0 Then
                Return False
            End If

        End If

        Return True

    End Function
#End Region

#Region "邸別請求受信データ削除"
    ''' <summary>
    ''' 邸別請求テーブルを削除します（区分・保証書NOで全て）
    ''' </summary>
    ''' <param name="kbn">区分</param>
    ''' <param name="hosyousyoNo">保証書NO</param>
    ''' <returns>削除結果 True:成功 False:失敗</returns>
    ''' <remarks></remarks>
    Public Function DeleteteibetuSeikyuuJyusinData(ByVal kbn As String, _
                                                   ByVal hosyousyoNo As String) As Boolean

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".DeleteteibetuSeikyuuJyusinData", _
                                            kbn, _
                                            hosyousyoNo)

        Dim intResult As Integer = 0
        Dim commandTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter = { _
            SQLHelper.MakeParam("@KBN", SqlDbType.Char, 1, kbn), _
            SQLHelper.MakeParam("@HOSYOUSYONO", SqlDbType.VarChar, 10, hosyousyoNo) _
            }

        '*****************************************************************
        ' 邸別請求テーブルの削除
        '*****************************************************************
        commandTextSb = New StringBuilder()
        commandTextSb.Append(" DELETE FROM t_teibetu_seikyuu_jyusin ")
        commandTextSb.Append(" WHERE kbn = @KBN  ")
        commandTextSb.Append(" AND   hosyousyo_no = @HOSYOUSYONO ")

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    cmdParams)
        Return True

    End Function
#End Region

#Region "連携データ登録・更新"
#Region "地盤連携"
    ''' <summary>
    ''' 地盤連携テーブルの内容を確認後データを反映します
    ''' </summary>
    ''' <param name="jibanRrec">地盤連携レコード</param>
    ''' <param name="isUpdate">地盤テーブルが更新される場合はTrue</param>
    ''' <returns>反映結果</returns>
    ''' <remarks></remarks>
    Public Function EditJibanRenkeiData(ByVal jibanRrec As JibanRenkeiRecord, _
                                        Optional ByVal isUpdate As Boolean = True) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EditJibanRenkeiData", _
                                                    jibanRrec, _
                                                    isUpdate)

        ' 地盤連携テーブルの存在チェック
        ' パラメータ
        Const paramKubun As String = "@KUBUN"
        Const paramHosyousyoNo As String = "@HOSYOUSYONO"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT sousin_jyky_cd ")
        commandTextSb.Append(" FROM t_jiban_renkei WITH(UPDLOCK) ")
        commandTextSb.Append(" WHERE kbn = " & paramKubun)
        commandTextSb.Append(" AND   hosyousyo_no = " & paramHosyousyoNo)

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(paramKubun, SqlDbType.Char, 1, jibanRrec.Kbn), _
             SQLHelper.MakeParam(paramHosyousyoNo, SqlDbType.VarChar, 10, jibanRrec.HosyousyoNo)}

        ' 送信状況コード
        'Dim sousin_jyky_cd As Integer
        Dim ret_value As Object = SQLHelper.ExecuteScalar(connStr, _
                                                          CommandType.Text, _
                                                          commandTextSb.ToString(), _
                                                          commandParameters)

        If ret_value Is Nothing Then
            ' 値が取得できない場合は新規でInsertする(新規は直接Insertメソッドを実行可)
            jibanRrec.SousinJykyCd = 0
            '削除の場合は送信状況コードを削除でInsert
            If jibanRrec.RenkeiSijiCd <> 9 Then
                If isUpdate = True Then
                    '既存地盤データの更新なので 2
                    jibanRrec.RenkeiSijiCd = 2
                Else
                    jibanRrec.RenkeiSijiCd = 1
                End If
            End If
            Return InsertJibanRenkeiData(jibanRrec)

        ElseIf ret_value = 0 Then
            ' 送信状況コードが未送信の場合、送信コード、連携指示コードを変更せずUPDATE
            Return UpdateJibanRenkeiData(jibanRrec, False)
        Else
            ' 送信状況コードが送信済の場合、送信コード：未送信、連携指示コード：更新でUPDATE
            jibanRrec.SousinJykyCd = 0
            jibanRrec.RenkeiSijiCd = 2
            Return UpdateJibanRenkeiData(jibanRrec, True)
        End If

    End Function

    ''' <summary>
    ''' 地盤連携テーブルへデータを登録します
    ''' </summary>
    ''' <param name="jibanRec">地盤連携レコード</param>
    ''' <returns>登録結果</returns>
    ''' <remarks></remarks>
    Public Function InsertJibanRenkeiData(ByVal jibanRec As JibanRenkeiRecord) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".InsertJibanRenkeiData", _
                                                    jibanRec)

        Dim intResult As Integer = 0
        Dim commandTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        ' 登録用SQL設定
        commandTextSb.Append(" INSERT INTO t_jiban_renkei ( ")
        commandTextSb.Append("     kbn, ")
        commandTextSb.Append("     hosyousyo_no, ")
        commandTextSb.Append("     renkei_siji_cd, ")
        commandTextSb.Append("     sousin_jyky_cd, ")
        commandTextSb.Append("     upd_login_user_id, ")
        commandTextSb.Append("     upd_datetime ")
        commandTextSb.Append(" ) VALUES ( ")
        commandTextSb.Append("     @KBN, ")
        commandTextSb.Append("     @HOSYOUSYONO, ")
        commandTextSb.Append("     @RENKEISIJICD, ")
        commandTextSb.Append("     @SOUSINJYKYCD, ")
        commandTextSb.Append("     @UPDLOGINUSERID, ")
        commandTextSb.Append("     @ADDDATETIME )")

        cmdParams = New SqlParameter() { _
                SQLHelper.MakeParam("@KBN", SqlDbType.Char, 1, jibanRec.Kbn), _
                SQLHelper.MakeParam("@HOSYOUSYONO", SqlDbType.VarChar, 10, jibanRec.HosyousyoNo), _
                SQLHelper.MakeParam("@RENKEISIJICD", SqlDbType.Int, 4, jibanRec.RenkeiSijiCd), _
                SQLHelper.MakeParam("@SOUSINJYKYCD", SqlDbType.Int, 4, jibanRec.SousinJykyCd), _
                SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, jibanRec.UpdLoginUserId), _
                SQLHelper.MakeParam("@ADDDATETIME", SqlDbType.DateTime, 16, DateTime.Now)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    cmdParams)

        Return intResult

    End Function

    ''' <summary>
    ''' 地盤連携テーブルのデータを更新します
    ''' </summary>
    ''' <param name="jibanRec">地盤連携レコード</param>
    ''' <param name="isAll">True:送信コード,連携指示コードも更新する</param>
    ''' <returns>更新結果</returns>
    ''' <remarks></remarks>
    Public Function UpdateJibanRenkeiData(ByVal jibanRec As JibanRenkeiRecord, _
                                          ByVal isAll As Boolean) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdateJibanRenkeiData", _
                                            jibanRec, _
                                            isAll)

        Dim intResult As Integer = 0
        Dim commandTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        ' 登録用SQL設定
        commandTextSb.Append(" UPDATE t_jiban_renkei ")
        commandTextSb.Append(" SET ")
        If isAll = True Then
            commandTextSb.Append("     renkei_siji_cd    = @RENKEISIJICD, ")
            commandTextSb.Append("     sousin_jyky_cd    = @SOUSINJYKYCD, ")
        End If
        commandTextSb.Append("     upd_login_user_id = @UPDLOGINUSERID, ")
        commandTextSb.Append("     upd_datetime     = @UPDDATETIME ")
        commandTextSb.Append(" WHERE kbn             = @KBN ")
        commandTextSb.Append(" AND   hosyousyo_no    = @HOSYOUSYONO ")

        cmdParams = New SqlParameter() { _
                SQLHelper.MakeParam("@KBN", SqlDbType.Char, 1, jibanRec.Kbn), _
                SQLHelper.MakeParam("@HOSYOUSYONO", SqlDbType.VarChar, 10, jibanRec.HosyousyoNo), _
                SQLHelper.MakeParam("@RENKEISIJICD", SqlDbType.Int, 4, jibanRec.RenkeiSijiCd), _
                SQLHelper.MakeParam("@SOUSINJYKYCD", SqlDbType.Int, 4, jibanRec.SousinJykyCd), _
                SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, jibanRec.UpdLoginUserId), _
                SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 16, DateTime.Now)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    cmdParams)

        Return intResult

    End Function

#End Region
#Region "邸別請求連携"
    ''' <summary>
    ''' 邸別請求連携テーブルの内容を確認後データを反映します
    ''' </summary>
    ''' <param name="teibetuRec">邸別請求連携レコード</param>
    ''' <returns>反映結果</returns>
    ''' <remarks></remarks>
    Public Function EditTeibetuRenkeiData(ByVal teibetuRec As TeibetuSeikyuuRenkeiRecord) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EditTeibetuRenkeiData", _
                                                    teibetuRec)

        ' 邸別請求連携テーブルの存在チェック
        ' パラメータ
        Const paramKubun As String = "@KUBUN"
        Const paramHosyousyoNo As String = "@HOSYOUSYONO"
        Const paramBunruiCd As String = "@BUNRUICD"
        Const paramGamenHyoujiNo As String = "@GAMENHYOUJINO"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT sousin_jyky_cd,renkei_siji_cd ")
        commandTextSb.Append(" FROM t_teibetu_seikyuu_renkei WITH(UPDLOCK) ")
        commandTextSb.Append(" WHERE kbn = " & paramKubun)
        commandTextSb.Append(" AND   hosyousyo_no = " & paramHosyousyoNo)
        commandTextSb.Append(" AND   SUBSTRING(bunrui_cd,1,2)         = SUBSTRING(" & paramBunruiCd & ",1,2)")
        commandTextSb.Append(" AND   gamen_hyouji_no   = " & paramGamenHyoujiNo)

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(paramKubun, SqlDbType.Char, 1, teibetuRec.Kbn), _
             SQLHelper.MakeParam(paramHosyousyoNo, SqlDbType.VarChar, 10, teibetuRec.HosyousyoNo), _
             SQLHelper.MakeParam(paramBunruiCd, SqlDbType.VarChar, 3, teibetuRec.BunruiCd), _
             SQLHelper.MakeParam(paramGamenHyoujiNo, SqlDbType.Int, 4, teibetuRec.GamenHyoujiNo)}

        ' データの取得
        Dim TeibetuRenkeiDataSet As New TeibetuRenkeiDataSet()

        ' 送信状況コード
        SQLHelper.FillDataset(connStr, _
                              CommandType.Text, _
                              commandTextSb.ToString(), _
                              TeibetuRenkeiDataSet, _
                              TeibetuRenkeiDataSet.RenkeiTable.TableName, _
                              commandParameters)

        Dim renkeiTable As TeibetuRenkeiDataSet.RenkeiTableDataTable = TeibetuRenkeiDataSet.RenkeiTable


        If renkeiTable.Rows.Count < 1 Then
            ' 値が取得できない場合は新規でInsertする(地盤ありは更新の為、邸別請求は新規でも更新)
            teibetuRec.SousinJykyCd = 0
            '削除の場合は送信状況コードを削除でInsert
            If teibetuRec.RenkeiSijiCd <> 9 Then
                If teibetuRec.IsUpdate = True Then
                    ' 既存データの更新時は2
                    teibetuRec.RenkeiSijiCd = 2
                Else
                    teibetuRec.RenkeiSijiCd = 1
                End If
            End If

            Return InsertTeibetuRenkeiData(teibetuRec)

        Else
            Dim row As TeibetuRenkeiDataSet.RenkeiTableRow = renkeiTable.Rows(0)

            ' 新規データが未送信且つ今回削除以外の場合、新規・未送信のままとする
            If row.sousin_jyky_cd = 0 And _
               row.renkei_siji_cd = 1 And _
               teibetuRec.RenkeiSijiCd <> 9 Then
                teibetuRec.SousinJykyCd = 0
                teibetuRec.RenkeiSijiCd = 1
            End If

            ' 削除データが未送信で今回新規の場合、更新・未送信に変更する
            If row.sousin_jyky_cd = 0 And _
               row.renkei_siji_cd = 9 And _
               teibetuRec.RenkeiSijiCd = 1 Then
                teibetuRec.SousinJykyCd = 0
                teibetuRec.RenkeiSijiCd = 2
            End If

            ' 送信状況コードが送信済の場合、送信コード：未送信、連携指示コード：更新でUPDATE
            Return UpdateTeibetuRenkeiData(teibetuRec, True)
        End If

    End Function

    ''' <summary>
    ''' 邸別請求連携テーブルへデータを登録します
    ''' </summary>
    ''' <param name="teibetuRec">邸別請求連携レコード</param>
    ''' <returns>登録結果</returns>
    ''' <remarks></remarks>
    Public Function InsertTeibetuRenkeiData(ByVal teibetuRec As TeibetuSeikyuuRenkeiRecord) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".InsertTeibetuRenkeiData", _
                                                    teibetuRec)

        Dim intResult As Integer = 0
        Dim commandTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        ' 登録用SQL設定
        commandTextSb.Append(" INSERT INTO t_teibetu_seikyuu_renkei ( ")
        commandTextSb.Append("     kbn, ")
        commandTextSb.Append("     hosyousyo_no, ")
        commandTextSb.Append("     bunrui_cd, ")
        commandTextSb.Append("     gamen_hyouji_no, ")
        commandTextSb.Append("     renkei_siji_cd, ")
        commandTextSb.Append("     sousin_jyky_cd, ")
        commandTextSb.Append("     upd_login_user_id, ")
        commandTextSb.Append("     upd_datetime ")
        commandTextSb.Append(" ) VALUES ( ")
        commandTextSb.Append("     @KBN, ")
        commandTextSb.Append("     @HOSYOUSYONO, ")
        commandTextSb.Append("     @BUNRUICD, ")
        commandTextSb.Append("     @GAMENHYOUJINO, ")
        commandTextSb.Append("     @RENKEISIJICD, ")
        commandTextSb.Append("     @SOUSINJYKYCD, ")
        commandTextSb.Append("     @UPDLOGINUSERID, ")
        commandTextSb.Append("     @ADDDATETIME )")

        cmdParams = New SqlParameter() { _
                SQLHelper.MakeParam("@KBN", SqlDbType.Char, 1, teibetuRec.Kbn), _
                SQLHelper.MakeParam("@HOSYOUSYONO", SqlDbType.VarChar, 10, teibetuRec.HosyousyoNo), _
                SQLHelper.MakeParam("@BUNRUICD", SqlDbType.VarChar, 3, teibetuRec.BunruiCd), _
                SQLHelper.MakeParam("@GAMENHYOUJINO", SqlDbType.Int, 4, teibetuRec.GamenHyoujiNo), _
                SQLHelper.MakeParam("@RENKEISIJICD", SqlDbType.Int, 4, teibetuRec.RenkeiSijiCd), _
                SQLHelper.MakeParam("@SOUSINJYKYCD", SqlDbType.Int, 4, teibetuRec.SousinJykyCd), _
                SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, teibetuRec.UpdLoginUserId), _
                SQLHelper.MakeParam("@ADDDATETIME", SqlDbType.DateTime, 16, DateTime.Now)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    cmdParams)

        Return intResult

    End Function

    ''' <summary>
    ''' 邸別請求連携テーブルのデータを更新します
    ''' </summary>
    ''' <param name="teibetuRec">邸別請求連携レコード</param>
    ''' <param name="isAll">True:送信コード,連携指示コードも更新する</param>
    ''' <returns>更新結果</returns>
    ''' <remarks></remarks>
    Public Function UpdateTeibetuRenkeiData(ByVal teibetuRec As TeibetuSeikyuuRenkeiRecord, _
                                            ByVal isAll As Boolean) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdateTeibetuRenkeiData", _
                                                    teibetuRec, _
                                                    isAll)

        Dim intResult As Integer = 0
        Dim commandTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        ' 登録用SQL設定
        commandTextSb.Append(" UPDATE t_teibetu_seikyuu_renkei ")
        commandTextSb.Append(" SET ")
        commandTextSb.Append("        bunrui_cd = @BUNRUICD, ")
        If isAll = True Then
            commandTextSb.Append("    renkei_siji_cd    = @RENKEISIJICD, ")
            commandTextSb.Append("    sousin_jyky_cd    = @SOUSINJYKYCD, ")
        End If
        commandTextSb.Append("        upd_login_user_id = @UPDLOGINUSERID, ")
        commandTextSb.Append("        upd_datetime     = @UPDDATETIME ")
        commandTextSb.Append(" WHERE  kbn               = @KBN ")
        commandTextSb.Append(" AND    hosyousyo_no      = @HOSYOUSYONO ")
        commandTextSb.Append(" AND    SUBSTRING(bunrui_cd,1,2)         = SUBSTRING(@BUNRUICD,1,2)")
        commandTextSb.Append(" AND    gamen_hyouji_no   = @GAMENHYOUJINO ")

        cmdParams = New SqlParameter() { _
                SQLHelper.MakeParam("@KBN", SqlDbType.Char, 1, teibetuRec.Kbn), _
                SQLHelper.MakeParam("@HOSYOUSYONO", SqlDbType.VarChar, 10, teibetuRec.HosyousyoNo), _
                SQLHelper.MakeParam("@BUNRUICD", SqlDbType.VarChar, 3, teibetuRec.BunruiCd), _
                SQLHelper.MakeParam("@GAMENHYOUJINO", SqlDbType.Int, 4, teibetuRec.GamenHyoujiNo), _
                SQLHelper.MakeParam("@RENKEISIJICD", SqlDbType.Int, 4, teibetuRec.RenkeiSijiCd), _
                SQLHelper.MakeParam("@SOUSINJYKYCD", SqlDbType.Int, 4, teibetuRec.SousinJykyCd), _
                SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, teibetuRec.UpdLoginUserId), _
                SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 16, DateTime.Now)}

        Dim debug As String = commandTextSb.ToString()

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    cmdParams)

        Return intResult

    End Function

#End Region
#Region "更新履歴連携"
    ''' <summary>
    ''' 更新履歴連携テーブルの内容を確認後データを反映します
    ''' </summary>
    ''' <param name="kousinRirekiRec">更新履歴連携レコード</param>
    ''' <param name="isUpdate">邸別テーブルが更新される場合はTrue</param>
    ''' <returns>反映結果</returns>
    ''' <remarks></remarks>
    Public Function EditKousinRirekiRenkeiData(ByVal kousinRirekiRec As KousinRirekiRenkeiRecord, _
                                               Optional ByVal isUpdate As Boolean = True) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EditKousinRirekiRenkeiData", _
                                                    kousinRirekiRec, _
                                                    isUpdate)

        ' 更新履歴連携テーブルの存在チェック
        ' パラメータ
        Const paramUpdDateTime As String = "@UPDDATETIME"
        Const paramKubun As String = "@KUBUN"
        Const paramHosyousyoNo As String = "@HOSYOUSYONO"
        Const paramUpdKoumoku As String = "@UPDKOUMOKU"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT sousin_jyky_cd ")
        commandTextSb.Append(" FROM t_kousin_rireki_renkei WITH(UPDLOCK) ")
        commandTextSb.Append(" WHERE kbn = " & paramKubun)
        commandTextSb.Append(" AND   hosyousyo_no = " & paramHosyousyoNo)
        commandTextSb.Append(" AND   upd_koumoku  = " & paramUpdKoumoku)
        commandTextSb.Append(" AND   upd_datetime_rireki   = " & paramUpdDateTime)

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(paramKubun, SqlDbType.Char, 1, kousinRirekiRec.Kbn), _
             SQLHelper.MakeParam(paramHosyousyoNo, SqlDbType.VarChar, 10, kousinRirekiRec.HosyousyoNo), _
             SQLHelper.MakeParam(paramUpdDateTime, SqlDbType.DateTime, 8, kousinRirekiRec.UpdDatetime), _
             SQLHelper.MakeParam(paramUpdKoumoku, SqlDbType.VarChar, 30, kousinRirekiRec.UpdKoumoku)}

        ' 送信状況コード
        Dim ret_value As Object = SQLHelper.ExecuteScalar(connStr, _
                                                          CommandType.Text, _
                                                          commandTextSb.ToString(), _
                                                          commandParameters)

        If ret_value Is Nothing Then
            ' 値が取得できない場合は新規でInsertする(新規は直接Insertメソッドを実行可)
            kousinRirekiRec.SousinJykyCd = 0
            kousinRirekiRec.RenkeiSijiCd = 1
            Return InsertKousinRirekiRenkeiData(kousinRirekiRec)
        Else
            ' 更新履歴テーブルはINSERTのみの為、存在する事は有り得ないが念の為更新ロジックを用意
            Return UpdateKousinRirekiRenkeiData(kousinRirekiRec, False)
        End If

    End Function

    ''' <summary>
    ''' 更新履歴連携テーブルへデータを登録します
    ''' </summary>
    ''' <param name="kousinRirekiRec">更新履歴連携レコード</param>
    ''' <returns>登録結果</returns>
    ''' <remarks></remarks>
    Public Function InsertKousinRirekiRenkeiData(ByVal kousinRirekiRec As KousinRirekiRenkeiRecord) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".InsertKousinRirekiRenkeiData", _
                                                    kousinRirekiRec)

        Dim intResult As Integer = 0
        Dim commandTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        ' 登録用SQL設定
        commandTextSb.Append(" INSERT INTO t_kousin_rireki_renkei ( ")
        commandTextSb.Append("     upd_datetime_rireki, ")
        commandTextSb.Append("     kbn, ")
        commandTextSb.Append("     hosyousyo_no, ")
        commandTextSb.Append("     upd_koumoku, ")
        commandTextSb.Append("     renkei_siji_cd, ")
        commandTextSb.Append("     sousin_jyky_cd, ")
        commandTextSb.Append("     upd_login_user_id, ")
        commandTextSb.Append("     upd_datetime ")
        commandTextSb.Append(" ) VALUES ( ")
        commandTextSb.Append("     @UPDDATETIME, ")
        commandTextSb.Append("     @KBN, ")
        commandTextSb.Append("     @HOSYOUSYONO, ")
        commandTextSb.Append("     @UPDKOUMOKU, ")
        commandTextSb.Append("     @RENKEISIJICD, ")
        commandTextSb.Append("     @SOUSINJYKYCD, ")
        commandTextSb.Append("     @UPDLOGINUSERID, ")
        commandTextSb.Append("     @ADDDATETIME )")

        cmdParams = New SqlParameter() { _
                SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 8, kousinRirekiRec.UpdDatetime), _
                SQLHelper.MakeParam("@KBN", SqlDbType.Char, 1, kousinRirekiRec.Kbn), _
                SQLHelper.MakeParam("@HOSYOUSYONO", SqlDbType.VarChar, 10, kousinRirekiRec.HosyousyoNo), _
                SQLHelper.MakeParam("@UPDKOUMOKU", SqlDbType.VarChar, 30, kousinRirekiRec.UpdKoumoku), _
                SQLHelper.MakeParam("@RENKEISIJICD", SqlDbType.Int, 4, kousinRirekiRec.RenkeiSijiCd), _
                SQLHelper.MakeParam("@SOUSINJYKYCD", SqlDbType.Int, 4, kousinRirekiRec.SousinJykyCd), _
                SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, kousinRirekiRec.UpdLoginUserId), _
                SQLHelper.MakeParam("@ADDDATETIME", SqlDbType.DateTime, 16, DateTime.Now)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    cmdParams)

        Return intResult

    End Function

    ''' <summary>
    ''' 更新履歴連携テーブルのデータを更新します
    ''' </summary>
    ''' <param name="kousinRirekiRec">更新履歴連携レコード</param>
    ''' <param name="isAll">True:送信コード,連携指示コードも更新する</param>
    ''' <returns>更新結果</returns>
    ''' <remarks></remarks>
    Public Function UpdateKousinRirekiRenkeiData(ByVal kousinRirekiRec As KousinRirekiRenkeiRecord, _
                                                 ByVal isAll As Boolean) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdateKousinRirekiRenkeiData", _
                                            kousinRirekiRec, _
                                            isAll)

        Dim intResult As Integer = 0
        Dim commandTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        ' 登録用SQL設定
        commandTextSb.Append(" UPDATE t_kousin_rireki_renkei  ")
        commandTextSb.Append(" SET ")
        If isAll = True Then
            commandTextSb.Append("     renkei_siji_cd = @RENKEISIJICD, ")
            commandTextSb.Append("     sousin_jyky_cd = @SOUSINJYKYCD, ")
        End If
        commandTextSb.Append("     upd_login_user_id = @UPDLOGINUSERID, ")
        commandTextSb.Append("     upd_datetime  = @UPDDATETIME ")

        commandTextSb.Append(" WHERE upd_datetime_rireki = @UPDDATETIME ")
        commandTextSb.Append(" AND   kbn          = @KBN  ")
        commandTextSb.Append(" AND   hosyousyo_no = @HOSYOUSYONO ")
        commandTextSb.Append(" AND   upd_koumoku  = @UPDKOUMOKU ")

        cmdParams = New SqlParameter() { _
                SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 8, kousinRirekiRec.UpdDatetime), _
                SQLHelper.MakeParam("@KBN", SqlDbType.Char, 1, kousinRirekiRec.Kbn), _
                SQLHelper.MakeParam("@HOSYOUSYONO", SqlDbType.VarChar, 10, kousinRirekiRec.HosyousyoNo), _
                SQLHelper.MakeParam("@UPDKOUMOKU", SqlDbType.VarChar, 30, kousinRirekiRec.UpdKoumoku), _
                SQLHelper.MakeParam("@RENKEISIJICD", SqlDbType.Int, 4, kousinRirekiRec.RenkeiSijiCd), _
                SQLHelper.MakeParam("@SOUSINJYKYCD", SqlDbType.Int, 4, kousinRirekiRec.SousinJykyCd), _
                SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, kousinRirekiRec.UpdLoginUserId), _
                SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 16, DateTime.Now)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    cmdParams)

        Return intResult

    End Function

#End Region
#End Region

#Region "更新履歴テーブル登録"
    ''' <summary>
    ''' 更新履歴テーブルへの登録を行います
    ''' </summary>
    ''' <param name="kbn">区分</param>
    ''' <param name="hosyousyoNo">保証書NO</param>
    ''' <param name="chkTaisyou">チェックする項目 1:施主名 2:物件住所 3:調査会社 4:備考</param>
    ''' <param name="chkTaisyouNm">チェック対象の日本語名</param>
    ''' <param name="chkData">チェックするデータ</param>
    ''' <param name="loginUserId">ログインユーザーID</param>
    ''' <remarks>チェック対象の項目が異なる場合のみ、更新履歴テーブルへ登録します</remarks>
    Public Function AddKoushinRireki(ByVal kbn As String, _
                                     ByVal hosyousyoNo As String, _
                                     ByVal chkTaisyou As Integer, _
                                     ByVal chkTaisyouNm As String, _
                                     ByVal chkData As String, _
                                     ByVal loginUserId As String) As Boolean

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".AddKoushinRireki", _
                                    kbn, _
                                    hosyousyoNo, _
                                    chkTaisyou, _
                                    chkTaisyouNm, _
                                    chkData, _
                                    loginUserId)

        Dim intResult As Integer = 0

        ' 連携テーブルと同期を取る為、更新日付を事前に取得する
        Dim nowDate As DateTime = DateTime.Now

        Dim commandTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        Dim updateItemName As String = ""
        Dim updateTableName As String = ""

        Select Case chkTaisyou
            Case enumItemName.SesyuMei
                updateItemName = "ISNULL(sesyu_mei,'')"
            Case enumItemName.Juusyo
                updateItemName = "ISNULL(bukken_jyuusyo1,'') + ISNULL(bukken_jyuusyo2,'')"
            Case enumItemName.KameitenCd
                updateItemName = "ISNULL(kameiten_cd,'')"
            Case enumItemName.TyousaKaisya
                updateItemName = "ISNULL(tys_kaisya_cd,'') + ISNULL(tys_kaisya_jigyousyo_cd,'')"
            Case enumItemName.Bikou
                updateItemName = "ISNULL(bikou,'')"
            Case enumItemName.Bikou2
                updateItemName = "ISNULL(bikou2,'')"
            Case Else
                Exit Function
        End Select

        commandTextSb.Append(" INSERT INTO t_kousin_rireki ")
        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("    @UPDDATETIME As upd_datetime, ")
        commandTextSb.Append("    kbn, ")
        commandTextSb.Append("    hosyousyo_no, ")
        commandTextSb.Append("    @UPDKOUMOKU As upd_koumoku, ")
        commandTextSb.Append("    {0} As upd_mae_atai, ")
        commandTextSb.Append("    @CHKDATA As upd_go_atai, ")
        commandTextSb.Append("    @USERID As kousinsya ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("     t_jiban WITH (READCOMMITTED) ")
        commandTextSb.Append(" WHERE ")
        commandTextSb.Append("     kbn = @KBN AND ")
        commandTextSb.Append("     hosyousyo_no = @HOSYOUSYONO AND ")
        commandTextSb.Append("     upd_datetime IS NOT NULL AND ")
        commandTextSb.Append("     {1} COLLATE Japanese_CS_AS_KS_WS  <> @CHKDATA COLLATE Japanese_CS_AS_KS_WS ")

        cmdParams = New SqlParameter() { _
                        SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 8, nowDate), _
                        SQLHelper.MakeParam("@KBN", SqlDbType.Char, 1, kbn), _
                        SQLHelper.MakeParam("@HOSYOUSYONO", SqlDbType.VarChar, 10, hosyousyoNo), _
                        SQLHelper.MakeParam("@UPDKOUMOKU", SqlDbType.VarChar, 30, chkTaisyouNm), _
                        SQLHelper.MakeParam("@CHKDATA", SqlDbType.VarChar, 512, chkData), _
                        SQLHelper.MakeParam("@USERID", SqlDbType.VarChar, 30, loginUserId)}

        Dim sql As String = commandTextSb.ToString()

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    String.Format(commandTextSb.ToString(), updateItemName, updateItemName), _
                                    cmdParams)

        If intResult >= 1 Then
            ' 変更があったので、連携テーブルにも登録する
            Dim kousinRirekiRec As New KousinRirekiRenkeiRecord

            kousinRirekiRec.UpdDatetime = nowDate
            kousinRirekiRec.Kbn = kbn
            kousinRirekiRec.HosyousyoNo = hosyousyoNo
            kousinRirekiRec.UpdKoumoku = chkTaisyouNm
            kousinRirekiRec.RenkeiSijiCd = 1
            kousinRirekiRec.SousinJykyCd = 0
            kousinRirekiRec.UpdDatetime1 = nowDate
            kousinRirekiRec.UpdLoginUserId = loginUserId

            intResult = EditKousinRirekiRenkeiData(kousinRirekiRec)

            ' 登録に失敗した場合False
            If intResult <= 0 Then
                Return False
            End If
        End If

        Return True

    End Function

#End Region

#Region "建物用途加算額取得"
    ''' <summary>
    ''' 建物用途加算額を取得します
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="intTatemonoYouto">建物用途コード</param>
    ''' <returns>建物用途加算額</returns>
    ''' <remarks>建物用途２～９までの加算額を取得します。取得制限は呼び出し元で行ってください。<br/>
    '''          （商品コードによる加算判断等）</remarks>
    Public Function GetTatemonoYoutoKasangaku(ByVal strKameitenCd As String, _
                                              ByVal intTatemonoYouto As Integer) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTatemonoYoutoKasangaku", _
                                                    strKameitenCd, _
                                                    intTatemonoYouto)

        ' 地盤連携テーブルの存在チェック
        ' パラメータ
        Const paramKameitenCd As String = "@KAMEITENCD"

        ' 建物用途が2～9の場合のみ、加算額を取得する
        If intTatemonoYouto < 2 Or intTatemonoYouto > 9 Then
            Return 0
        End If

        Dim commandTextSb As New StringBuilder()
        commandTextSb.Append(" SELECT ISNULL(kasangaku" & intTatemonoYouto.ToString() & ",0) As kasangaku ")
        commandTextSb.Append(" FROM m_tatemono_youto_kasangaku WITH (READCOMMITTED) ")
        commandTextSb.Append(" WHERE kameiten_cd = " & strKameitenCd)

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(paramKameitenCd, SqlDbType.VarChar, 5, strKameitenCd)}

        ' 建物用途加算額
        Dim retValue As Object = SQLHelper.ExecuteScalar(connStr, _
                                                          CommandType.Text, _
                                                          commandTextSb.ToString(), _
                                                          commandParameters)

        If retValue Is Nothing Then
            Return 0
        End If

        Return retValue

    End Function
#End Region

#Region "工事コピー処理チェック用"
    ''' <summary>
    ''' 工事コピー処理チェック用
    ''' </summary>
    ''' <param name="kbn">区分</param>
    ''' <param name="hosyousyoNo">保証書NO</param>   
    ''' <returns>工事コピー処理チェック用データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetKoujiCopyCheckData(ByVal kbn As String, _
                                 ByVal hosyousyoNo As String) As JibanDataSet.KoujiCopyCheckTableDataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetJibanData", _
                                            kbn, _
                                            hosyousyoNo)

        ' パラメータ
        Const strParamKbn As String = "@KBN"
        Const strParamHosyousyoNo As String = "@HOSYOUSYONO"

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKbn, SqlDbType.Char, 1, kbn), _
             SQLHelper.MakeParam(strParamHosyousyoNo, SqlDbType.VarChar, 10, hosyousyoNo)}

        Dim commandText As String = " select " & _
                                    "   j.kbn " & _
                                    "   , j.hosyousyo_no " & _
                                    "   , syouninsya_cd " & _
                                    "   , koj_siyou_kakunin " & _
                                    "   , koj_siyou_kakunin_date " & _
                                    "   , koj_gaisya_cd " & _
                                    "   , koj_gaisya_seikyuu_umu " & _
                                    "   , kairy_koj_syubetu " & _
                                    "   , kairy_koj_kanry_yotei_date " & _
                                    "   , kairy_koj_sokuhou_tyk_date " & _
                                    "   , t.syouhin_cd " & _
                                    "   , t.uri_gaku " & _
                                    "   , t.siire_gaku  " & _
                                    "   , t.seikyuu_umu " & _
                                    "   , t.uri_keijyou_date  " & _
                                    "   , t.hattyuusyo_gaku  " & _
                                    " from " & _
                                    "   t_jiban j  " & _
                                    "   left outer join t_teibetu_seikyuu t  " & _
                                    "     on t.kbn = j.kbn  " & _
                                    "     and t.hosyousyo_no = j.hosyousyo_no  " & _
                                    "     and t.bunrui_cd = '" & EarthConst.SOUKO_CD_KAIRYOU_KOUJI & "'  " & _
                                    " where " & _
                                    "   j.kbn = " & strParamKbn & _
                                    "   and j.hosyousyo_no = " & strParamHosyousyoNo

        ' データの取得
        Dim JibanDataSet As New JibanDataSet()

        SQLHelper.FillDataset(connStr, CommandType.Text, commandText, _
            JibanDataSet, JibanDataSet.KoujiCopyCheckTable.TableName, commandParameters)

        Dim KoujiCopyCheckTable As JibanDataSet.KoujiCopyCheckTableDataTable = JibanDataSet.KoujiCopyCheckTable

        Return KoujiCopyCheckTable

    End Function
#End Region

    ''' <summary>
    ''' ◆伝票発行対応◆
    ''' 削除処理トリガー用にユーザーIDをローカル一時テーブルに格納するためのSQL文を生成
    ''' </summary>
    ''' <param name="strUserId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CreateUserInfoTempTableSQL(ByVal strUserId As String) As String

        Dim commandTextSb As New StringBuilder()

        Const strParamTableName As String = "#TEMP_USER_INFO_FOR_TRIGGER"

        'テンポラリテーブルの作成
        commandTextSb.Append(" IF object_id('tempdb.." & strParamTableName & "') is null ")
        commandTextSb.Append(" BEGIN ")

        commandTextSb.Append("    CREATE TABLE ")
        commandTextSb.Append("           " & strParamTableName & " ")
        commandTextSb.Append("          (login_user_id  VARCHAR(30) ")
        commandTextSb.Append("          ,syori_datetime DATETIME ) ")

        commandTextSb.Append("    INSERT INTO " & strParamTableName & " ")
        commandTextSb.Append("          (login_user_id ")
        commandTextSb.Append("          ,syori_datetime ) ")
        commandTextSb.Append("    VALUES ")
        commandTextSb.Append("          ( '" & strUserId & "'")
        commandTextSb.Append("          ,GETDATE() ) ")

        commandTextSb.Append(" END ")

        Return commandTextSb.ToString()

    End Function

End Class

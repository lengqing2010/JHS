Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper
''' <summary>
''' 調査会社マスタ
''' </summary>
''' <history>
''' <para>2010/05/15　馬艶軍(大連)　新規作成</para>
''' </history>
Public Class TyousaKaisyaMasterDataAccess
    Private connStr As String = ConnectionManager.Instance.ConnectionString
    'Private connDbTable_m_fc As String = System.Configuration.ConfigurationManager.AppSettings("connDbTable_m_fc").ToString

    ''' <summary>
    ''' 拡張名称マスタ
    ''' </summary>
    ''' <param name="strSyubetu">名称種別</param>
    Public Function SelKakutyouInfo(ByVal strSyubetu As String) As DataTable

        ' DataSetインスタンスの生成
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine(" SELECT   ")
            .AppendLine("   code   ")
            .AppendLine("   ,meisyou   ")
            .AppendLine(" FROM  ")
            .AppendLine("   m_kakutyou_meisyou WITH (READCOMMITTED)  ")
            .AppendLine(" WHERE  meisyou_syubetu=@meisyou_syubetu  ")
            .AppendLine(" ORDER BY hyouji_jyun ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.VarChar, 3, strSyubetu))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        '戻る
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' 調査会社マスタ
    ''' </summary>
    ''' <param name="strTysKaisyaCd">調査会社コード</param>
    ''' <param name="strJigyousyoCd">事業所コード</param>
    Public Function SelMTyousaKaisyaInfo(ByVal strTyousaKaisya_Cd As String, _
                                         ByVal strTysKaisyaCd As String, _
                                         ByVal strJigyousyoCd As String, _
                                         ByVal btn As String) As TyousaKaisyaDataSet.m_tyousakaisyaDataTable

        ' DataSetインスタンスの生成
        Dim dsDataSet As New TyousaKaisyaDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   MTK.tys_kaisya_cd, ")       '調査会社コード
            .AppendLine("   MTK.jigyousyo_cd, ")        '事業所コード
            .AppendLine("   MTK.torikesi, ")        '取消
            .AppendLine("   MTK.tys_kaisya_mei, ")      '調査会社名
            .AppendLine("   MTK.tys_kaisya_mei_kana, ")     '調査会社名カナ
            .AppendLine("   MTK.seikyuu_saki_shri_saki_mei, ")      '請求先支払先名
            .AppendLine("   MTK.seikyuu_saki_shri_saki_kana, ")     '請求先支払先名カナ
            .AppendLine("   MTK.jyuusyo1, ")        '住所1
            .AppendLine("   MTK.jyuusyo2, ")        '住所2
            .AppendLine("   MTK.yuubin_no, ")       '郵便番号
            .AppendLine("   MTK.tel_no, ")      '電話番号
            .AppendLine("   MTK.fax_no, ")      'FAX番号
            .AppendLine("   MTK.pca_siiresaki_cd, ")        'PCA用仕入先コード
            .AppendLine("   MTK.pca_seikyuu_cd, ")      'PCA請求先コード
            .AppendLine("   MTK.seikyuu_saki_cd, ")     '請求先コード
            .AppendLine("   MTK.seikyuu_saki_brc, ")        '請求先枝番
            .AppendLine("   MTK.seikyuu_saki_kbn, ")        '請求先区分
            .AppendLine("   MTK.seikyuu_sime_date, ")       '請求締め日
            .AppendLine("   MTK.skysy_soufu_jyuusyo1, ")        '請求書送付先住所1
            .AppendLine("   MTK.skysy_soufu_jyuusyo2, ")        '請求書送付先住所2
            .AppendLine("   MTK.skysy_soufu_yuubin_no, ")       '請求書送付先郵便番号
            .AppendLine("   MTK.skysy_soufu_tel_no, ")      '請求書送付先電話番号
            .AppendLine("   MTK.skk_shri_saki_cd, ")        '新会計支払先コード
            .AppendLine("   MTK.skk_jigyousyo_cd, ")        '新会計事業所コード
            .AppendLine("   MTK.shri_meisai_jigyousyo_cd, ")        '支払明細集計先事業所コード
            .AppendLine("   MTK.shri_jigyousyo_cd, ")       '支払集計先事業所コード
            .AppendLine("   MTK.shri_sime_date, ")      '支払締め日
            .AppendLine("   MTK.shri_yotei_gessuu, ")       '支払予定月数
            .AppendLine("   MTK.fctring_kaisi_nengetu, ")       'ファクタリング開始年月
            .AppendLine("   MTK.shri_you_fax_no, ")     '支払用FAX番号
            .AppendLine("   MTK.ss_kijyun_kkk, ")       'SS基準価格
            .AppendLine("   MTK.fc_ten_cd, ")       'FC店コード
            .AppendLine("   MTK.kensa_center_cd, ")     '検査センターコード
            .AppendLine("   MTK.koj_hkks_tyokusou_flg, ")       '工事報告書直送
            .AppendLine("   MTK.koj_hkks_tyokusou_upd_login_user_id, ")     '工事報告書直送変更ログインユーザーID
            .AppendLine("   MTK.koj_hkks_tyokusou_upd_datetime, ")      '工事報告書直送変更日時
            .AppendLine("   MTK.tys_kaisya_flg, ")      '調査会社フラグ
            .AppendLine("   MTK.koj_kaisya_flg, ")      '工事会社フラグ
            .AppendLine("   MTK.japan_kai_kbn, ")       'JAPAN会区分
            .AppendLine("   MTK.japan_kai_nyuukai_date, ")      'JAPAN会入会年月
            .AppendLine("   MTK.japan_kai_taikai_date, ")       'JAPAN会退会年月
            .AppendLine("   MTK.zenjyuhin_hosoku, ")            '全住品区分補足


            .AppendLine("   MTK.fc_ten_kbn, ")      'FC店区分
            .AppendLine("   MTK.fc_nyuukai_date, ")     'FC入会年月
            .AppendLine("   MTK.fc_taikai_date, ")      'FC退会年月
            .AppendLine("   MTK.torikesi_riyuu, ")      '取消理由
            .AppendLine("   MTK.report_jhs_token_flg, ")        'ReportJHSトークン有無フラグ
            .AppendLine("   MTK.tkt_jbn_tys_syunin_skk_flg, ")      '宅地地盤調査主任資格有無フラグ
            .AppendLine("   MTK.add_login_user_id, ")       '登録ログインユーザーID
            .AppendLine("   MTK.add_datetime, ")        '登録日時
            .AppendLine("   MTK.upd_login_user_id, ")       '更新ログインユーザーID
            .AppendLine("   MTK.upd_datetime, ")        '更新日時
            .AppendLine("   MK.eigyousyo_mei AS keiretu_mei, ")        '系列名
            .AppendLine("   MSSS.shri_saki_mei_kanji, ")        '支払先名_漢字
            '.AppendLine("   MFC.fc_nm, ")        '支払先名_漢字
            .AppendLine("   VSSI.seikyuu_saki_mei, ")        '請求先名

            .AppendLine("   MTK1.tys_kaisya_mei AS shri_kaisya_mei, ")        '支払集計先事業所名
            .AppendLine("   MTK2.tys_kaisya_mei AS shri_meisai_kaisya_mei ")         '支払明細集計先事業所名

            '============2012/04/12 車龍 405721 追加↓==========================
            .AppendLine("   ,MTK.daihyousya_mei ")        '代表者名
            .AppendLine("   ,MTK.yakusyoku_mei ")        '役職名
            '============2012/04/12 車龍 405721 追加↑==========================

            '2013/11/04 李宇追加 ↓
            .AppendLine("   ,MTK.sds_hoji_info ")        'SDS保持情報
            .AppendLine("   ,MTK.sds_daisuu_info ")      'SDS台数情報
            .AppendLine("   ,MTK.jituzai_flg ")          '実在FLG
            '2013/11/04 李宇追加↑
            .AppendLine("   ,MTK.a1_lifnr ")          '実在FLG
            .AppendLine("   ,MSSK.a1_a_zz_sort ")          '実在FLG

            .AppendLine("FROM ")
            .AppendLine("   m_tyousakaisya MTK WITH (READCOMMITTED) ")      '調査会社マスタ
            .AppendLine("LEFT JOIN ")
            .AppendLine("   m_eigyousyo MK WITH (READCOMMITTED) ")            '営業所マスタ
            .AppendLine("ON ")
            .AppendLine("  MTK.fc_ten_cd = MK.eigyousyo_cd ")

            .AppendLine("LEFT JOIN ")
            .AppendLine("   m_sinkaikei_siharai_saki MSSS WITH (READCOMMITTED) ")       '新会計支払先マスタ
            .AppendLine("ON ")
            .AppendLine("  MTK.skk_jigyousyo_cd = MSSS.skk_jigyou_cd ")
            .AppendLine("AND ")
            .AppendLine("  MTK.skk_shri_saki_cd = MSSS.skk_shri_saki_cd ")
            '.AppendLine("LEFT JOIN ")
            '.AppendLine("   " & connDbTable_m_fc & " MFC WITH (READCOMMITTED) ")       '建物検査.FCマスタ
            '.AppendLine("ON ")
            '.AppendLine("  MTK.kensa_center_cd = MFC.fc_cd ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("   v_seikyuu_saki_info VSSI WITH (READCOMMITTED) ")       '請求先情報VIEW
            .AppendLine("ON ")
            .AppendLine("  MTK.seikyuu_saki_cd = VSSI.seikyuu_saki_cd ")
            .AppendLine("AND ")
            .AppendLine("  MTK.seikyuu_saki_brc = VSSI.seikyuu_saki_brc ")
            .AppendLine("AND ")
            .AppendLine("  MTK.seikyuu_saki_kbn = VSSI.seikyuu_saki_kbn ")

            .AppendLine("LEFT JOIN ")
            .AppendLine("   m_tyousakaisya MTK1 WITH (READCOMMITTED) ")      '調査会社マスタ")
            .AppendLine("ON ")
            .AppendLine("   MTK.tys_kaisya_cd = MTK1.tys_kaisya_cd ")
            .AppendLine("AND ")
            .AppendLine("   MTK.shri_jigyousyo_cd = MTK1.jigyousyo_cd ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("   m_tyousakaisya MTK2 WITH (READCOMMITTED) ")      '調査会社マスタ")
            .AppendLine("ON ")
            .AppendLine("   MTK.tys_kaisya_cd = MTK2.tys_kaisya_cd ")
            .AppendLine("AND ")
            .AppendLine("   MTK.shri_meisai_jigyousyo_cd = MTK2.jigyousyo_cd ")

            .AppendLine("LEFT JOIN m_sinkaikei_siire_saki MSSK")
            .AppendLine("  ON MTK.a1_lifnr = MSSK.a1_lifnr ")

            .AppendLine("WHERE ")
            .AppendLine("   MTK.tys_kaisya_cd IS NOT NULL ")
            If btn = "btnSearch" Then
                .AppendLine("AND ")
                .AppendLine("   MTK.tys_kaisya_cd + MTK.jigyousyo_cd = @tys_kaisya_cd ")
            Else
                .AppendLine("AND ")
                .AppendLine("   MTK.tys_kaisya_cd = @tys_kaisya_cd ")
                .AppendLine("AND ")
                .AppendLine("   MTK.jigyousyo_cd = @jigyousyo_cd ")
            End If

        End With

        'パラメータの設定
        If btn = "btnSearch" Then
            paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 7, strTyousaKaisya_Cd))
        Else
            paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, strTysKaisyaCd))
            paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, strJigyousyoCd))
        End If

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    dsDataSet.m_tyousakaisya.TableName, paramList.ToArray)

        '戻る
        Return dsDataSet.m_tyousakaisya
    End Function

    ''' <summary>
    ''' 排他チェック用
    ''' </summary>
    ''' <param name="strTysKaisyaCd">調査会社コード</param>
    ''' <param name="strJigyousyoCd">事業所コード</param>
    ''' <param name="strKousinDate">更新時間</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelHaita(ByVal strTysKaisyaCd As String, ByVal strJigyousyoCd As String, ByVal strKousinDate As String) As DataTable

        ' DataSetインスタンスの生成()
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("  upd_login_user_id ")
            .AppendLine("  ,upd_datetime ")
            .AppendLine("FROM ")
            .AppendLine("  m_tyousakaisya WITH (READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("  tys_kaisya_cd = @tys_kaisya_cd  ")
            .AppendLine("AND ")
            .AppendLine("  jigyousyo_cd = @jigyousyo_cd  ")
            .AppendLine("AND ")
            .AppendLine("   CONVERT(varchar(19),CONVERT(datetime,upd_datetime,21),21)<>CONVERT(varchar(19),CONVERT(datetime,@upd_datetime,21),21) ")
            .AppendLine("AND ")
            .AppendLine("   upd_datetime IS NOT NULL  ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, strTysKaisyaCd))
        paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, strJigyousyoCd))
        paramList.Add(MakeParam("@upd_datetime", SqlDbType.VarChar, 19, strKousinDate))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)
    End Function

    ''' <summary>
    ''' 調査会社マスタテーブルの修正
    ''' </summary>
    ''' <param name="dtTyousaKaisya">修正のデータ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdTyousaKaisya(ByVal dtTyousaKaisya As TyousaKaisyaDataSet.m_tyousakaisyaDataTable, ByVal strHenkou As String, ByVal strDisplayName As String) As Integer

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("UPDATE ")
            .AppendLine("   m_tyousakaisya WITH (UPDLOCK) ")
            .AppendLine("SET ")
            .AppendLine("   torikesi = @torikesi, ")    '取消
            .AppendLine("   tys_kaisya_mei = @tys_kaisya_mei, ")    '調査会社名
            .AppendLine("   tys_kaisya_mei_kana = @tys_kaisya_mei_kana, ")  '調査会社名カナ
            .AppendLine("   seikyuu_saki_shri_saki_mei = @seikyuu_saki_shri_saki_mei, ")    '請求先支払先名
            .AppendLine("   seikyuu_saki_shri_saki_kana = @seikyuu_saki_shri_saki_kana, ")  '請求先支払先名カナ
            .AppendLine("   jyuusyo1 = @jyuusyo1, ")    '住所1
            .AppendLine("   jyuusyo2 = @jyuusyo2, ")    '住所2
            .AppendLine("   yuubin_no = @yuubin_no, ")  '郵便番号
            .AppendLine("   tel_no = @tel_no, ")    '電話番号
            .AppendLine("   fax_no = @fax_no, ")    'FAX番号
            '.AppendLine("   pca_siiresaki_cd = @pca_siiresaki_cd, ")    'PCA用仕入先コード
            '.AppendLine("   pca_seikyuu_cd = @pca_seikyuu_cd, ")    'PCA請求先コード
            .AppendLine("   seikyuu_saki_cd = @seikyuu_saki_cd, ")  '請求先コード
            .AppendLine("   seikyuu_saki_brc = @seikyuu_saki_brc, ")    '請求先枝番
            .AppendLine("   seikyuu_saki_kbn = @seikyuu_saki_kbn, ")    '請求先区分
            '.AppendLine("   seikyuu_sime_date = @seikyuu_sime_date, ")  '請求締め日
            .AppendLine("   skysy_soufu_jyuusyo1 = @skysy_soufu_jyuusyo1, ")    '請求書送付先住所1
            .AppendLine("   skysy_soufu_jyuusyo2 = @skysy_soufu_jyuusyo2, ")    '請求書送付先住所2
            .AppendLine("   skysy_soufu_yuubin_no = @skysy_soufu_yuubin_no, ")  '請求書送付先郵便番号
            .AppendLine("   skysy_soufu_tel_no = @skysy_soufu_tel_no, ")    '請求書送付先電話番号
            .AppendLine("   skk_shri_saki_cd = @skk_shri_saki_cd, ")    '新会計支払先コード
            .AppendLine("   skk_jigyousyo_cd = @skk_jigyousyo_cd, ")    '新会計事業所コード
            .AppendLine("   shri_meisai_jigyousyo_cd = @shri_meisai_jigyousyo_cd, ")    '支払明細集計先事業所コード
            .AppendLine("   shri_jigyousyo_cd = @shri_jigyousyo_cd, ")  '支払集計先事業所コード
            .AppendLine("   shri_sime_date = @shri_sime_date, ")    '支払締め日
            .AppendLine("   shri_yotei_gessuu = @shri_yotei_gessuu, ")  '支払予定月数
            .AppendLine("   fctring_kaisi_nengetu = @fctring_kaisi_nengetu, ")  'ファクタリング開始年月
            .AppendLine("   shri_you_fax_no = @shri_you_fax_no, ")  '支払用FAX番号
            .AppendLine("   ss_kijyun_kkk = @ss_kijyun_kkk, ")  'SS基準価格
            .AppendLine("   fc_ten_cd = @fc_ten_cd, ")  'FC店コード
            .AppendLine("   kensa_center_cd = @kensa_center_cd, ")  '検査センターコード
            .AppendLine("   koj_hkks_tyokusou_flg = @koj_hkks_tyokusou_flg, ")  '工事報告書直送
            .AppendLine("   koj_hkks_tyokusou_upd_login_user_id = @koj_hkks_tyokusou_upd_login_user_id, ")  '工事報告書直送変更ログインユーザーID

            If strHenkou = "YES" Then
                .AppendLine("   koj_hkks_tyokusou_upd_datetime = GETDATE(), ")    '工事報告書直送変更日時
            Else
                .AppendLine("   koj_hkks_tyokusou_upd_datetime = @koj_hkks_tyokusou_upd_datetime, ")    '工事報告書直送変更日時
            End If

            .AppendLine("   tys_kaisya_flg = @tys_kaisya_flg, ")    '調査会社フラグ
            .AppendLine("   koj_kaisya_flg = @koj_kaisya_flg, ")    '工事会社フラグ
            .AppendLine("   japan_kai_kbn = @japan_kai_kbn, ")  'JAPAN会区分
            .AppendLine("   japan_kai_nyuukai_date = @japan_kai_nyuukai_date, ")    'JAPAN会入会年月
            .AppendLine("   japan_kai_taikai_date = @japan_kai_taikai_date, ")  'JAPAN会退会年月
            .AppendLine("   zenjyuhin_hosoku = @zenjyuhin_hosoku, ")

            .AppendLine("   fc_ten_kbn = @fc_ten_kbn, ")    'FC店区分
            .AppendLine("   fc_nyuukai_date = @fc_nyuukai_date, ")  'FC入会年月
            .AppendLine("   fc_taikai_date = @fc_taikai_date, ")    'FC退会年月
            .AppendLine("   torikesi_riyuu = @torikesi_riyuu, ")    '取消理由
            .AppendLine("   report_jhs_token_flg = @report_jhs_token_flg, ")    'ReportJHSトークン有無フラグ
            .AppendLine("   tkt_jbn_tys_syunin_skk_flg = @tkt_jbn_tys_syunin_skk_flg, ")    '宅地地盤調査主任資格有無フラグ
            '============2012/04/12 車龍 405721 追加↓==========================
            .AppendLine("   daihyousya_mei = @daihyousya_mei, ")        '代表者名
            .AppendLine("   yakusyoku_mei = @yakusyoku_mei, ")        '役職名
            '============2012/04/12 車龍 405721 追加↑==========================
            '.AppendLine("   add_login_user_id = @add_login_user_id, ")  '登録ログインユーザーID
            '.AppendLine("   add_datetime = @add_datetime, ")    '登録日時
            '2013/11/04 李宇追加 ↓
            .AppendLine("   sds_hoji_info = @sds_hoji_info, ")        'SDS保持情報
            .AppendLine("   sds_daisuu_info = @sds_daisuu_info, ")    'SDS台数情報
            '2013/11/04 李宇追加 ↑
            .AppendLine("   jituzai_flg = @jituzai_flg, ")  '実在FLG

            .AppendLine("   a1_lifnr = @a1_lifnr, ")  '実在FLG

            .AppendLine("   upd_login_user_id = @upd_login_user_id, ")  '更新ログインユーザーID
            .AppendLine("   upd_datetime = GETDATE() ")    '更新日時
            .AppendLine("WHERE ")
            .AppendLine("  tys_kaisya_cd = @tys_kaisya_cd ")
            .AppendLine("AND ")
            .AppendLine("  jigyousyo_cd = @jigyousyo_cd ")
        End With

        'パラメータの設定
        With paramList
            '調査会社コード
            .Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 255, dtTyousaKaisya(0).tys_kaisya_cd))
            '事業所コード
            .Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 255, dtTyousaKaisya(0).jigyousyo_cd))
            '取消
            .Add(MakeParam("@torikesi", SqlDbType.VarChar, 255, dtTyousaKaisya(0).torikesi))
            '調査会社名
            .Add(MakeParam("@tys_kaisya_mei", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).tys_kaisya_mei = "", DBNull.Value, dtTyousaKaisya(0).tys_kaisya_mei)))
            '調査会社名カナ
            .Add(MakeParam("@tys_kaisya_mei_kana", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).tys_kaisya_mei_kana = "", DBNull.Value, dtTyousaKaisya(0).tys_kaisya_mei_kana)))
            '請求先支払先名
            .Add(MakeParam("@seikyuu_saki_shri_saki_mei", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).seikyuu_saki_shri_saki_mei = "", DBNull.Value, dtTyousaKaisya(0).seikyuu_saki_shri_saki_mei)))
            '請求先支払先名カナ
            .Add(MakeParam("@seikyuu_saki_shri_saki_kana", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).seikyuu_saki_shri_saki_kana = "", DBNull.Value, dtTyousaKaisya(0).seikyuu_saki_shri_saki_kana)))
            '住所1
            .Add(MakeParam("@jyuusyo1", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).jyuusyo1 = "", DBNull.Value, dtTyousaKaisya(0).jyuusyo1)))
            '住所2
            .Add(MakeParam("@jyuusyo2", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).jyuusyo2 = "", DBNull.Value, dtTyousaKaisya(0).jyuusyo2)))
            '郵便番号
            .Add(MakeParam("@yuubin_no", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).yuubin_no = "", DBNull.Value, dtTyousaKaisya(0).yuubin_no)))
            '電話番号
            .Add(MakeParam("@tel_no", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).tel_no = "", DBNull.Value, dtTyousaKaisya(0).tel_no)))
            'FAX番号
            .Add(MakeParam("@fax_no", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).fax_no = "", DBNull.Value, dtTyousaKaisya(0).fax_no)))

            '請求先コード
            .Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).seikyuu_saki_cd = "", DBNull.Value, dtTyousaKaisya(0).seikyuu_saki_cd)))
            '請求先枝番
            .Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).seikyuu_saki_brc = "", DBNull.Value, dtTyousaKaisya(0).seikyuu_saki_brc)))
            '請求先区分
            .Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).seikyuu_saki_kbn = "", DBNull.Value, dtTyousaKaisya(0).seikyuu_saki_kbn)))

            '請求書送付先住所1
            .Add(MakeParam("@skysy_soufu_jyuusyo1", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).skysy_soufu_jyuusyo1 = "", DBNull.Value, dtTyousaKaisya(0).skysy_soufu_jyuusyo1)))
            '請求書送付先住所2
            .Add(MakeParam("@skysy_soufu_jyuusyo2", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).skysy_soufu_jyuusyo2 = "", DBNull.Value, dtTyousaKaisya(0).skysy_soufu_jyuusyo2)))
            '請求書送付先郵便番号
            .Add(MakeParam("@skysy_soufu_yuubin_no", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).skysy_soufu_yuubin_no = "", DBNull.Value, dtTyousaKaisya(0).skysy_soufu_yuubin_no)))
            '請求書送付先電話番号
            .Add(MakeParam("@skysy_soufu_tel_no", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).skysy_soufu_tel_no = "", DBNull.Value, dtTyousaKaisya(0).skysy_soufu_tel_no)))
            '新会計支払先コード
            .Add(MakeParam("@skk_shri_saki_cd", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).skk_shri_saki_cd = "", DBNull.Value, dtTyousaKaisya(0).skk_shri_saki_cd)))
            '新会計事業所コード
            .Add(MakeParam("@skk_jigyousyo_cd", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).skk_jigyousyo_cd = "", DBNull.Value, dtTyousaKaisya(0).skk_jigyousyo_cd)))
            '支払明細集計先事業所コード
            .Add(MakeParam("@shri_meisai_jigyousyo_cd", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).shri_meisai_jigyousyo_cd = "", DBNull.Value, dtTyousaKaisya(0).shri_meisai_jigyousyo_cd)))
            '支払集計先事業所コード
            .Add(MakeParam("@shri_jigyousyo_cd", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).shri_jigyousyo_cd = "", DBNull.Value, dtTyousaKaisya(0).shri_jigyousyo_cd)))
            '支払締め日
            .Add(MakeParam("@shri_sime_date", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).shri_sime_date = "", DBNull.Value, dtTyousaKaisya(0).shri_sime_date)))
            '支払予定月数
            .Add(MakeParam("@shri_yotei_gessuu", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).shri_yotei_gessuu = "", DBNull.Value, dtTyousaKaisya(0).shri_yotei_gessuu)))
            'ファクタリング開始年月
            .Add(MakeParam("@fctring_kaisi_nengetu", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).fctring_kaisi_nengetu = "", DBNull.Value, toYYYYMMDD(dtTyousaKaisya(0).fctring_kaisi_nengetu))))
            '支払用FAX番号
            .Add(MakeParam("@shri_you_fax_no", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).shri_you_fax_no = "", DBNull.Value, dtTyousaKaisya(0).shri_you_fax_no)))
            'SS基準価格
            .Add(MakeParam("@ss_kijyun_kkk", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).ss_kijyun_kkk = "", DBNull.Value, dtTyousaKaisya(0).ss_kijyun_kkk)))
            'FC店コード
            .Add(MakeParam("@fc_ten_cd", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).fc_ten_cd = "", DBNull.Value, dtTyousaKaisya(0).fc_ten_cd)))
            '検査センターコード
            .Add(MakeParam("@kensa_center_cd", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).kensa_center_cd = "", DBNull.Value, dtTyousaKaisya(0).kensa_center_cd)))

            If strHenkou = "YES" Then
                '工事報告書直送
                .Add(MakeParam("@koj_hkks_tyokusou_flg", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).koj_hkks_tyokusou_flg = "", DBNull.Value, dtTyousaKaisya(0).koj_hkks_tyokusou_flg)))
                '工事報告書直送変更ログインユーザーID
                .Add(MakeParam("@koj_hkks_tyokusou_upd_login_user_id", SqlDbType.VarChar, 255, IIf(strDisplayName = "", DBNull.Value, strDisplayName)))
            Else
                '工事報告書直送
                .Add(MakeParam("@koj_hkks_tyokusou_flg", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).koj_hkks_tyokusou_flg = "", DBNull.Value, dtTyousaKaisya(0).koj_hkks_tyokusou_flg)))
                '工事報告書直送変更ログインユーザーID
                .Add(MakeParam("@koj_hkks_tyokusou_upd_login_user_id", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).koj_hkks_tyokusou_upd_login_user_id = "", DBNull.Value, dtTyousaKaisya(0).koj_hkks_tyokusou_upd_login_user_id)))
                '工事報告書直送変更日時
                .Add(MakeParam("@koj_hkks_tyokusou_upd_datetime", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).koj_hkks_tyokusou_upd_datetime = "", DBNull.Value, toYYYYMMDDHHmmSS(dtTyousaKaisya(0).koj_hkks_tyokusou_upd_datetime))))
            End If


            '調査会社フラグ
            .Add(MakeParam("@tys_kaisya_flg", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).tys_kaisya_flg = "", DBNull.Value, dtTyousaKaisya(0).tys_kaisya_flg)))
            '工事会社フラグ
            .Add(MakeParam("@koj_kaisya_flg", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).koj_kaisya_flg = "", DBNull.Value, dtTyousaKaisya(0).koj_kaisya_flg)))
            'JAPAN会区分
            .Add(MakeParam("@japan_kai_kbn", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).japan_kai_kbn = "", DBNull.Value, dtTyousaKaisya(0).japan_kai_kbn)))
            'JAPAN会入会年月
            .Add(MakeParam("@japan_kai_nyuukai_date", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).japan_kai_nyuukai_date = "", DBNull.Value, toYYYYMMDD(dtTyousaKaisya(0).japan_kai_nyuukai_date))))
            'JAPAN会退会年月
            .Add(MakeParam("@japan_kai_taikai_date", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).japan_kai_taikai_date = "", DBNull.Value, toYYYYMMDD(dtTyousaKaisya(0).japan_kai_taikai_date))))

            '全住品区分補足
            .Add(MakeParam("@zenjyuhin_hosoku", SqlDbType.VarChar, 80, IIf(dtTyousaKaisya(0).zenjyuhin_hosoku = "", DBNull.Value, dtTyousaKaisya(0).zenjyuhin_hosoku)))

            'FC店区分
            .Add(MakeParam("@fc_ten_kbn", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).fc_ten_kbn = "", DBNull.Value, dtTyousaKaisya(0).fc_ten_kbn)))
            'FC入会年月
            .Add(MakeParam("@fc_nyuukai_date", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).fc_nyuukai_date = "", DBNull.Value, toYYYYMMDD(dtTyousaKaisya(0).fc_nyuukai_date))))
            'FC退会年月
            .Add(MakeParam("@fc_taikai_date", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).fc_taikai_date = "", DBNull.Value, toYYYYMMDD(dtTyousaKaisya(0).fc_taikai_date))))
            '取消理由
            .Add(MakeParam("@torikesi_riyuu", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).torikesi_riyuu = "", DBNull.Value, dtTyousaKaisya(0).torikesi_riyuu)))
            'ReportJHSトークン有無フラグ
            .Add(MakeParam("@report_jhs_token_flg", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).report_jhs_token_flg = "", DBNull.Value, dtTyousaKaisya(0).report_jhs_token_flg)))
            '宅地地盤調査主任資格有無フラグ
            .Add(MakeParam("@tkt_jbn_tys_syunin_skk_flg", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).tkt_jbn_tys_syunin_skk_flg = "", DBNull.Value, dtTyousaKaisya(0).tkt_jbn_tys_syunin_skk_flg)))
            '============2012/04/12 車龍 405721 追加↓==========================
            '代表者名
            .Add(MakeParam("@daihyousya_mei", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).daihyousya_mei.Equals(String.Empty), DBNull.Value, dtTyousaKaisya(0).daihyousya_mei)))
            '役職名
            .Add(MakeParam("@yakusyoku_mei", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).yakusyoku_mei.Equals(String.Empty), DBNull.Value, dtTyousaKaisya(0).yakusyoku_mei)))
            '============2012/04/12 車龍 405721 追加↑==========================

            '2013/11/04 李宇追加 ↓
            'SDS保持情報
            .Add(MakeParam("@sds_hoji_info", SqlDbType.Int, 10, IIf(dtTyousaKaisya(0).sds_hoji_info.Equals(String.Empty), DBNull.Value, dtTyousaKaisya(0).sds_hoji_info)))
            'SDS台数情報
            .Add(MakeParam("@sds_daisuu_info", SqlDbType.Int, 5, IIf(dtTyousaKaisya(0).sds_daisuu_info.Equals(String.Empty), DBNull.Value, dtTyousaKaisya(0).sds_daisuu_info)))
            '2013/11/04 李宇追加 ↑
            .Add(MakeParam("@jituzai_flg", SqlDbType.Int, 4, IIf(dtTyousaKaisya(0).jituzai_flg.Equals(String.Empty), DBNull.Value, dtTyousaKaisya(0).jituzai_flg)))

            .Add(MakeParam("@a1_lifnr", SqlDbType.VarChar, 10, IIf(dtTyousaKaisya(0).a1_lifnr.Equals(String.Empty), DBNull.Value, dtTyousaKaisya(0).a1_lifnr)))


            '更新ログインユーザーID
            .Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).upd_login_user_id = "", DBNull.Value, dtTyousaKaisya(0).upd_login_user_id)))

        End With

        ' クエリ実行
        Return ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

    End Function

    ''' <summary>
    ''' 重複チェック_請求先マスタ
    ''' </summary>
    ''' <param name="strSeikyuuSakiCd">請求先コード</param>
    ''' <param name="strSeikyuuSakiBrc">請求先枝番</param>
    ''' <param name="strSeikyuuSakiKbn">請求先区分</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelSeikyuuSaki(ByVal strSeikyuuSakiCd As String, ByVal strSeikyuuSakiBrc As String, ByVal strSeikyuuSakiKbn As String, Optional ByVal strTrue As Boolean = False) As DataTable

        ' DataSetインスタンスの生成()
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   seikyuu_saki_cd, ")
            .AppendLine("   seikyuu_saki_brc, ")
            .AppendLine("   seikyuu_saki_kbn ")
            .AppendLine("FROM ")
            .AppendLine("   m_seikyuu_saki WITH (READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("   seikyuu_saki_cd = @seikyuu_saki_cd ")
            .AppendLine("AND ")
            .AppendLine("   seikyuu_saki_brc = @seikyuu_saki_brc ")
            .AppendLine("AND ")
            .AppendLine("   seikyuu_saki_kbn = @seikyuu_saki_kbn ")
            'If strTrue = True Then
            '    .AppendLine("AND ")
            '    .AppendLine("   torikesi = @torikesi ")
            'End If
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 255, strSeikyuuSakiCd))
        paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 255, strSeikyuuSakiBrc))
        paramList.Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 255, strSeikyuuSakiKbn))
        'If strTrue = True Then
        '    paramList.Add(MakeParam("@torikesi", SqlDbType.VarChar, 1, "0"))
        'End If

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)
    End Function

    ''' <summary>
    ''' 重複チェック_請求先登録雛形マスタ
    ''' </summary>
    ''' <param name="strSeikyuuSakiBrc">請求先枝番</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelSeikyuuSakiTH(ByVal strSeikyuuSakiBrc As String) As DataTable

        ' DataSetインスタンスの生成()
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   seikyuu_saki_brc ")
            .AppendLine("FROM ")
            .AppendLine("   m_seikyuu_saki_touroku_hinagata WITH (READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("   seikyuu_saki_brc = @seikyuu_saki_brc ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 255, strSeikyuuSakiBrc))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)
    End Function

    ''' <summary>
    ''' 請求先マスタテーブルの登録
    ''' </summary>
    ''' <param name="strSeikyuuSakiCd">請求先コード</param>
    ''' <param name="strSeikyuuSakiBrc">請求先枝番</param>
    ''' <param name="strUserId">ログインユーザID</param>
    ''' <param name="blnFlg">存在フラグ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsSeikyuuSaki(ByVal strSeikyuuSakiCd As String, _
                                    ByVal strSeikyuuSakiBrc As String, _
                                    ByVal strUserId As String, _
                                    ByVal blnFlg As Boolean) As Integer

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("INSERT INTO ")
            .AppendLine("   m_seikyuu_saki WITH (UPDLOCK) ")
            .AppendLine("(")
            .AppendLine("   seikyuu_saki_cd, ") '請求先コード
            .AppendLine("   seikyuu_saki_brc, ")    '請求先枝番
            .AppendLine("   seikyuu_saki_kbn, ")    '請求先区分
            .AppendLine("   torikesi, ")    '取消
            .AppendLine("   add_login_user_id, ")   '登録ログインユーザーID
            .AppendLine("   add_datetime ")    '登録日時
            If blnFlg = True Then
                .AppendLine("  , ")
                .AppendLine("   tantousya_mei, ")   '担当者名
                .AppendLine("   seikyuusyo_inji_bukken_mei_flg, ")  '請求書印字物件名フラグ
                .AppendLine("   nyuukin_kouza_no, ")    '入金口座番号
                .AppendLine("   seikyuu_sime_date, ")   '請求締め日
                .AppendLine("   senpou_seikyuu_sime_date, ")    '先方請求締め日
                .AppendLine("   sousai_flg, ")  '相殺フラグ
                .AppendLine("   kaisyuu_yotei_gessuu, ")    '回収予定月数
                .AppendLine("   kaisyuu_yotei_date, ")  '回収予定日
                .AppendLine("   seikyuusyo_hittyk_date, ")  '請求書必着日
                .AppendLine("   kaisyuu1_syubetu1, ")   '回収1種別1
                .AppendLine("   kaisyuu1_wariai1, ")    '回収1割合1
                .AppendLine("   kaisyuu1_tegata_site_gessuu, ") '回収1手形サイト月数
                .AppendLine("   kaisyuu1_tegata_site_date, ")   '回収1手形サイト日
                .AppendLine("   kaisyuu1_seikyuusyo_yousi, ")   '回収1請求書用紙
                .AppendLine("   kaisyuu1_syubetu2, ")   '回収1種別2
                .AppendLine("   kaisyuu1_wariai2, ")    '回収1割合2
                .AppendLine("   kaisyuu1_syubetu3, ")   '回収1種別3
                .AppendLine("   kaisyuu1_wariai3, ")    '回収1割合3
                .AppendLine("   kaisyuu_kyoukaigaku, ") '回収境界額
                .AppendLine("   kaisyuu2_syubetu1, ")   '回収2種別1
                .AppendLine("   kaisyuu2_wariai1, ")    '回収2割合1
                .AppendLine("   kaisyuu2_tegata_site_gessuu, ") '回収2手形サイト月数
                .AppendLine("   kaisyuu2_tegata_site_date, ")   '回収2手形サイト日
                .AppendLine("   kaisyuu2_seikyuusyo_yousi, ")   '回収2請求書用紙
                .AppendLine("   kaisyuu2_syubetu2, ")   '回収2種別2
                .AppendLine("   kaisyuu2_wariai2, ")    '回収2割合2
                .AppendLine("   kaisyuu2_syubetu3, ")   '回収2種別3
                .AppendLine("   kaisyuu2_wariai3 ")     '回収2割合3
                '2013/5/29 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↓
                .AppendLine("   ,ginkou_siten_cd ")     '銀行支店コード
                '2013/5/29 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↑
            End If
            .AppendLine(")")
            .AppendLine("SELECT ")
            .AppendLine("   @seikyuu_saki_cd, ")    '請求先コード
            .AppendLine("   @seikyuu_saki_brc, ")   '請求先枝番
            .AppendLine("   @seikyuu_saki_kbn, ")   '請求先区分
            .AppendLine("   @torikesi, ")           '取消
            .AppendLine("   @add_login_user_id, ")  '登録ログインユーザーID
            .AppendLine("   GETDATE() ")            '登録日時
            If blnFlg = True Then
                .AppendLine("   ,")
                .AppendLine("   tantousya_mei, ")   '担当者名
                .AppendLine("   seikyuusyo_inji_bukken_mei_flg, ")  '請求書印字物件名フラグ
                .AppendLine("   nyuukin_kouza_no, ")    '入金口座番号
                .AppendLine("   seikyuu_sime_date, ")   '請求締め日
                .AppendLine("   senpou_seikyuu_sime_date, ")    '先方請求締め日
                .AppendLine("   sousai_flg, ")  '相殺フラグ
                .AppendLine("   kaisyuu_yotei_gessuu, ")    '回収予定月数
                .AppendLine("   kaisyuu_yotei_date, ")  '回収予定日
                .AppendLine("   seikyuusyo_hittyk_date, ")  '請求書必着日
                .AppendLine("   kaisyuu1_syubetu1, ")   '回収1種別1
                .AppendLine("   kaisyuu1_wariai1, ")    '回収1割合1
                .AppendLine("   kaisyuu1_tegata_site_gessuu, ") '回収1手形サイト月数
                .AppendLine("   kaisyuu1_tegata_site_date, ")   '回収1手形サイト日
                .AppendLine("   kaisyuu1_seikyuusyo_yousi, ")   '回収1請求書用紙
                .AppendLine("   kaisyuu1_syubetu2, ")   '回収1種別2
                .AppendLine("   kaisyuu1_wariai2, ")    '回収1割合2
                .AppendLine("   kaisyuu1_syubetu3, ")   '回収1種別3
                .AppendLine("   kaisyuu1_wariai3, ")    '回収1割合3
                .AppendLine("   kaisyuu_kyoukaigaku, ") '回収境界額
                .AppendLine("   kaisyuu2_syubetu1, ")   '回収2種別1
                .AppendLine("   kaisyuu2_wariai1, ")    '回収2割合1
                .AppendLine("   kaisyuu2_tegata_site_gessuu, ") '回収2手形サイト月数
                .AppendLine("   kaisyuu2_tegata_site_date, ")   '回収2手形サイト日
                .AppendLine("   kaisyuu2_seikyuusyo_yousi, ")   '回収2請求書用紙
                .AppendLine("   kaisyuu2_syubetu2, ")   '回収2種別2
                .AppendLine("   kaisyuu2_wariai2, ")    '回収2割合2
                .AppendLine("   kaisyuu2_syubetu3, ")   '回収2種別3
                .AppendLine("   kaisyuu2_wariai3 ")     '回収2割合3
                '2013/5/29 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↓
                .AppendLine("   ,ginkou_siten_cd ")     '銀行支店コード
                '2013/5/29 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↑
                .AppendLine("FROM ")
                .AppendLine("   m_seikyuu_saki_touroku_hinagata WITH (READCOMMITTED) ")
                .AppendLine("WHERE ")
                .AppendLine("   seikyuu_saki_brc = @seikyuu_saki_brc ")
            End If
        End With

        'パラメータの設定
        With paramList
            .Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 255, IIf(strSeikyuuSakiCd = "", DBNull.Value, strSeikyuuSakiCd.ToUpper)))
            .Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 255, IIf(strSeikyuuSakiBrc = "", DBNull.Value, strSeikyuuSakiBrc.ToUpper)))
            .Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 255, "1"))
            .Add(MakeParam("@torikesi", SqlDbType.VarChar, 255, "0"))
            .Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 255, IIf(strUserId = "", DBNull.Value, strUserId)))
        End With

        ' クエリ実行
        Return ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

    End Function

    ''' <summary>
    ''' 請求先.検索ボタン押下時処理_請求先登録雛形マスタチェック
    ''' </summary>
    ''' <param name="strSeikyuuSakiBrc">請求先枝番</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelSeikyuuSakiTouroku(ByVal strSeikyuuSakiBrc As String) As DataTable

        ' DataSetインスタンスの生成()
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   hyouji_naiyou ")
            .AppendLine("FROM ")
            .AppendLine("   m_seikyuu_saki_touroku_hinagata WITH (READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("   seikyuu_saki_brc = @seikyuu_saki_brc ")
            .AppendLine("AND ")
            .AppendLine("   torikesi = '0' ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 255, strSeikyuuSakiBrc))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)
    End Function

    Public Function InsTyousaKaisya(ByVal dtTyousaKaisya As TyousaKaisyaDataSet.m_tyousakaisyaDataTable, ByVal strHenkou As String, ByVal strDisplayName As String) As Integer

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        With commandTextSb
            .AppendLine("INSERT INTO ")
            .AppendLine("   m_tyousakaisya WITH (UPDLOCK) ")
            .AppendLine("( ")
            .AppendLine("   tys_kaisya_cd, ")   '調査会社コード
            .AppendLine("   jigyousyo_cd, ")    '事業所コード
            .AppendLine("   torikesi, ")    '取消
            .AppendLine("   tys_kaisya_mei, ")  '調査会社名
            .AppendLine("   tys_kaisya_mei_kana, ") '調査会社名カナ
            .AppendLine("   seikyuu_saki_shri_saki_mei, ")  '請求先支払先名
            .AppendLine("   seikyuu_saki_shri_saki_kana, ") '請求先支払先名カナ
            .AppendLine("   jyuusyo1, ")    '住所1
            .AppendLine("   jyuusyo2, ")    '住所2
            .AppendLine("   yuubin_no, ")   '郵便番号
            .AppendLine("   tel_no, ")  '電話番号
            .AppendLine("   fax_no, ")  'FAX番号
            .AppendLine("   seikyuu_saki_cd, ") '請求先コード
            .AppendLine("   seikyuu_saki_brc, ")    '請求先枝番
            .AppendLine("   seikyuu_saki_kbn, ")    '請求先区分
            '.AppendLine("   seikyuu_sime_date, ")   '請求締め日
            .AppendLine("   skysy_soufu_jyuusyo1, ")    '請求書送付先住所1
            .AppendLine("   skysy_soufu_jyuusyo2, ")    '請求書送付先住所2
            .AppendLine("   skysy_soufu_yuubin_no, ")   '請求書送付先郵便番号
            .AppendLine("   skysy_soufu_tel_no, ")  '請求書送付先電話番号
            .AppendLine("   skk_shri_saki_cd, ")    '新会計支払先コード
            .AppendLine("   skk_jigyousyo_cd, ")    '新会計事業所コード
            .AppendLine("   shri_meisai_jigyousyo_cd, ")    '支払明細集計先事業所コード
            .AppendLine("   shri_jigyousyo_cd, ")   '支払集計先事業所コード
            .AppendLine("   shri_sime_date, ")  '支払締め日
            .AppendLine("   shri_yotei_gessuu, ")   '支払予定月数
            .AppendLine("   fctring_kaisi_nengetu, ")   'ファクタリング開始年月
            .AppendLine("   shri_you_fax_no, ") '支払用FAX番号
            .AppendLine("   ss_kijyun_kkk, ")   'SS基準価格
            .AppendLine("   fc_ten_cd, ")   'FC店コード
            .AppendLine("   kensa_center_cd, ") '検査センターコード
            .AppendLine("   koj_hkks_tyokusou_flg, ")   '工事報告書直送
            .AppendLine("   koj_hkks_tyokusou_upd_login_user_id, ") '工事報告書直送変更ログインユーザーID
            .AppendLine("   koj_hkks_tyokusou_upd_datetime, ")  '工事報告書直送変更日時
            .AppendLine("   tys_kaisya_flg, ")  '調査会社フラグ
            .AppendLine("   koj_kaisya_flg, ")  '工事会社フラグ
            .AppendLine("   japan_kai_kbn, ")   'JAPAN会区分
            .AppendLine("   japan_kai_nyuukai_date, ")  'JAPAN会入会年月
            .AppendLine("   japan_kai_taikai_date, ")   'JAPAN会退会年月
            .AppendLine("   zenjyuhin_hosoku, ")   '全住品区分補足

            .AppendLine("   fc_ten_kbn, ")  'FC店区分
            .AppendLine("   fc_nyuukai_date, ") 'FC入会年月
            .AppendLine("   fc_taikai_date, ")  'FC退会年月
            .AppendLine("   torikesi_riyuu, ")  '取消理由
            .AppendLine("   report_jhs_token_flg, ")    'ReportJHSトークン有無フラグ
            .AppendLine("   tkt_jbn_tys_syunin_skk_flg, ")  '宅地地盤調査主任資格有無フラグ
            '============2012/04/12 車龍 405721 追加↓==========================
            .AppendLine("   daihyousya_mei, ")        '代表者名
            .AppendLine("   yakusyoku_mei, ")        '役職名
            '============2012/04/12 車龍 405721 追加↑==========================
            '2013/11/04 李宇追加 ↓
            .AppendLine("   sds_hoji_info, ")        'SDS保持情報
            .AppendLine("   sds_daisuu_info, ")      'SDS台数情報
            .AppendLine("   jituzai_flg, ")
            '2013/11/04 李宇追加 ↑

            .AppendLine("   a1_lifnr, ")

            .AppendLine("   add_login_user_id, ")   '登録ログインユーザーID
            .AppendLine("   add_datetime ")    '登録日時
            .AppendLine(") ")
            .AppendLine("SELECT ")
            .AppendLine("   @tys_kaisya_cd, ")  '調査会社コード
            .AppendLine("   @jigyousyo_cd, ")   '事業所コード
            .AppendLine("   @torikesi, ")   '取消
            .AppendLine("   @tys_kaisya_mei, ") '調査会社名
            .AppendLine("   @tys_kaisya_mei_kana, ")    '調査会社名カナ
            .AppendLine("   @seikyuu_saki_shri_saki_mei, ") '請求先支払先名
            .AppendLine("   @seikyuu_saki_shri_saki_kana, ")    '請求先支払先名カナ
            .AppendLine("   @jyuusyo1, ")   '住所1
            .AppendLine("   @jyuusyo2, ")   '住所2
            .AppendLine("   @yuubin_no, ")  '郵便番号
            .AppendLine("   @tel_no, ") '電話番号
            .AppendLine("   @fax_no, ") 'FAX番号
            .AppendLine("   @seikyuu_saki_cd, ")    '請求先コード
            .AppendLine("   @seikyuu_saki_brc, ")   '請求先枝番
            .AppendLine("   @seikyuu_saki_kbn, ")   '請求先区分
            '.AppendLine("   @seikyuu_sime_date, ")  '請求締め日
            .AppendLine("   @skysy_soufu_jyuusyo1, ")   '請求書送付先住所1
            .AppendLine("   @skysy_soufu_jyuusyo2, ")   '請求書送付先住所2
            .AppendLine("   @skysy_soufu_yuubin_no, ")  '請求書送付先郵便番号
            .AppendLine("   @skysy_soufu_tel_no, ") '請求書送付先電話番号
            .AppendLine("   @skk_shri_saki_cd, ")   '新会計支払先コード
            .AppendLine("   @skk_jigyousyo_cd, ")   '新会計事業所コード
            .AppendLine("   @shri_meisai_jigyousyo_cd, ")   '支払明細集計先事業所コード
            .AppendLine("   @shri_jigyousyo_cd, ")  '支払集計先事業所コード
            .AppendLine("   @shri_sime_date, ") '支払締め日
            .AppendLine("   @shri_yotei_gessuu, ")  '支払予定月数
            .AppendLine("   @fctring_kaisi_nengetu, ")  'ファクタリング開始年月
            .AppendLine("   @shri_you_fax_no, ")    '支払用FAX番号
            .AppendLine("   @ss_kijyun_kkk, ")  'SS基準価格
            .AppendLine("   @fc_ten_cd, ")  'FC店コード
            .AppendLine("   @kensa_center_cd, ")    '検査センターコード
            .AppendLine("   @koj_hkks_tyokusou_flg, ")  '工事報告書直送
            .AppendLine("   @koj_hkks_tyokusou_upd_login_user_id, ")    '工事報告書直送変更ログインユーザーID
            If strHenkou = "YES" Then
                .AppendLine("   GETDATE(), ")   '工事報告書直送変更日時
            Else
                .AppendLine("   @koj_hkks_tyokusou_upd_datetime, ") '工事報告書直送変更日時
            End If
            .AppendLine("   @tys_kaisya_flg, ") '調査会社フラグ
            .AppendLine("   @koj_kaisya_flg, ") '工事会社フラグ
            .AppendLine("   @japan_kai_kbn, ")  'JAPAN会区分
            .AppendLine("   @japan_kai_nyuukai_date, ") 'JAPAN会入会年月
            .AppendLine("   @japan_kai_taikai_date, ")  'JAPAN会退会年月
            .AppendLine("   @zenjyuhin_hosoku, ")   '全住品区分補足
            .AppendLine("   @fc_ten_kbn, ") 'FC店区分
            .AppendLine("   @fc_nyuukai_date, ")    'FC入会年月
            .AppendLine("   @fc_taikai_date, ") 'FC退会年月
            .AppendLine("   @torikesi_riyuu, ") '取消理由
            .AppendLine("   @report_jhs_token_flg, ")   'ReportJHSトークン有無フラグ
            .AppendLine("   @tkt_jbn_tys_syunin_skk_flg, ") '宅地地盤調査主任資格有無フラグ


            '============2012/04/12 車龍 405721 追加↓==========================
            .AppendLine("   @daihyousya_mei, ")        '代表者名
            .AppendLine("   @yakusyoku_mei, ")        '役職名
            '============2012/04/12 車龍 405721 追加↑==========================
            '2013/11/04 李宇追加 ↓
            .AppendLine("   @sds_hoji_info, ")        'SDS保持情報
            .AppendLine("   @sds_daisuu_info, ")      'SDS台数情報
            .AppendLine("   @jituzai_flg, ")

            .AppendLine("   @a1_lifnr, ")
            '2013/11/04 李宇追加 ↑
            .AppendLine("   @add_login_user_id, ")  '登録ログインユーザーID
            .AppendLine("   GETDATE() ")   '登録日時
        End With

        'パラメータの設定
        With paramList
            '調査会社コード
            .Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 255, dtTyousaKaisya(0).tys_kaisya_cd))
            '事業所コード
            .Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 255, dtTyousaKaisya(0).jigyousyo_cd))
            '取消
            .Add(MakeParam("@torikesi", SqlDbType.VarChar, 255, dtTyousaKaisya(0).torikesi))
            '調査会社名
            .Add(MakeParam("@tys_kaisya_mei", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).tys_kaisya_mei = "", DBNull.Value, dtTyousaKaisya(0).tys_kaisya_mei)))
            '調査会社名カナ
            .Add(MakeParam("@tys_kaisya_mei_kana", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).tys_kaisya_mei_kana = "", DBNull.Value, dtTyousaKaisya(0).tys_kaisya_mei_kana)))
            '請求先支払先名
            .Add(MakeParam("@seikyuu_saki_shri_saki_mei", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).seikyuu_saki_shri_saki_mei = "", DBNull.Value, dtTyousaKaisya(0).seikyuu_saki_shri_saki_mei)))
            '請求先支払先名カナ
            .Add(MakeParam("@seikyuu_saki_shri_saki_kana", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).seikyuu_saki_shri_saki_kana = "", DBNull.Value, dtTyousaKaisya(0).seikyuu_saki_shri_saki_kana)))
            '住所1
            .Add(MakeParam("@jyuusyo1", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).jyuusyo1 = "", DBNull.Value, dtTyousaKaisya(0).jyuusyo1)))
            '住所2
            .Add(MakeParam("@jyuusyo2", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).jyuusyo2 = "", DBNull.Value, dtTyousaKaisya(0).jyuusyo2)))
            '郵便番号
            .Add(MakeParam("@yuubin_no", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).yuubin_no = "", DBNull.Value, dtTyousaKaisya(0).yuubin_no)))
            '電話番号
            .Add(MakeParam("@tel_no", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).tel_no = "", DBNull.Value, dtTyousaKaisya(0).tel_no)))
            'FAX番号
            .Add(MakeParam("@fax_no", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).fax_no = "", DBNull.Value, dtTyousaKaisya(0).fax_no)))
            '請求先コード
            .Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).seikyuu_saki_cd = "", DBNull.Value, dtTyousaKaisya(0).seikyuu_saki_cd)))
            '請求先枝番
            .Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).seikyuu_saki_brc = "", DBNull.Value, dtTyousaKaisya(0).seikyuu_saki_brc)))
            '請求先区分
            .Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).seikyuu_saki_kbn = "", DBNull.Value, dtTyousaKaisya(0).seikyuu_saki_kbn)))
            '請求書送付先住所1
            .Add(MakeParam("@skysy_soufu_jyuusyo1", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).skysy_soufu_jyuusyo1 = "", DBNull.Value, dtTyousaKaisya(0).skysy_soufu_jyuusyo1)))
            '請求書送付先住所2
            .Add(MakeParam("@skysy_soufu_jyuusyo2", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).skysy_soufu_jyuusyo2 = "", DBNull.Value, dtTyousaKaisya(0).skysy_soufu_jyuusyo2)))
            '請求書送付先郵便番号
            .Add(MakeParam("@skysy_soufu_yuubin_no", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).skysy_soufu_yuubin_no = "", DBNull.Value, dtTyousaKaisya(0).skysy_soufu_yuubin_no)))
            '請求書送付先電話番号
            .Add(MakeParam("@skysy_soufu_tel_no", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).skysy_soufu_tel_no = "", DBNull.Value, dtTyousaKaisya(0).skysy_soufu_tel_no)))
            '新会計支払先コード
            .Add(MakeParam("@skk_shri_saki_cd", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).skk_shri_saki_cd = "", DBNull.Value, dtTyousaKaisya(0).skk_shri_saki_cd)))
            '新会計事業所コード
            .Add(MakeParam("@skk_jigyousyo_cd", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).skk_jigyousyo_cd = "", DBNull.Value, dtTyousaKaisya(0).skk_jigyousyo_cd)))
            '支払明細集計先事業所コード
            .Add(MakeParam("@shri_meisai_jigyousyo_cd", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).shri_meisai_jigyousyo_cd = "", DBNull.Value, dtTyousaKaisya(0).shri_meisai_jigyousyo_cd)))
            '支払集計先事業所コード
            .Add(MakeParam("@shri_jigyousyo_cd", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).shri_jigyousyo_cd = "", DBNull.Value, dtTyousaKaisya(0).shri_jigyousyo_cd)))
            '支払締め日
            .Add(MakeParam("@shri_sime_date", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).shri_sime_date = "", DBNull.Value, dtTyousaKaisya(0).shri_sime_date)))
            '支払予定月数
            .Add(MakeParam("@shri_yotei_gessuu", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).shri_yotei_gessuu = "", DBNull.Value, dtTyousaKaisya(0).shri_yotei_gessuu)))
            'ファクタリング開始年月
            .Add(MakeParam("@fctring_kaisi_nengetu", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).fctring_kaisi_nengetu = "", DBNull.Value, toYYYYMMDD(dtTyousaKaisya(0).fctring_kaisi_nengetu))))
            '支払用FAX番号
            .Add(MakeParam("@shri_you_fax_no", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).shri_you_fax_no = "", DBNull.Value, dtTyousaKaisya(0).shri_you_fax_no)))
            'SS基準価格
            .Add(MakeParam("@ss_kijyun_kkk", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).ss_kijyun_kkk = "", DBNull.Value, dtTyousaKaisya(0).ss_kijyun_kkk)))
            'FC店コード
            .Add(MakeParam("@fc_ten_cd", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).fc_ten_cd = "", DBNull.Value, dtTyousaKaisya(0).fc_ten_cd)))
            '検査センターコード
            .Add(MakeParam("@kensa_center_cd", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).kensa_center_cd = "", DBNull.Value, dtTyousaKaisya(0).kensa_center_cd)))
            '工事報告書直送
            .Add(MakeParam("@koj_hkks_tyokusou_flg", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).koj_hkks_tyokusou_flg = "", DBNull.Value, dtTyousaKaisya(0).koj_hkks_tyokusou_flg)))

            If strHenkou = "YES" Then
                '工事報告書直送変更ログインユーザーID
                .Add(MakeParam("@koj_hkks_tyokusou_upd_login_user_id", SqlDbType.VarChar, 255, IIf(strDisplayName = "", DBNull.Value, strDisplayName)))
            Else
                '工事報告書直送変更ログインユーザーID
                .Add(MakeParam("@koj_hkks_tyokusou_upd_login_user_id", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).koj_hkks_tyokusou_upd_login_user_id = "", DBNull.Value, dtTyousaKaisya(0).koj_hkks_tyokusou_upd_login_user_id)))
                '工事報告書直送変更日時
                .Add(MakeParam("@koj_hkks_tyokusou_upd_datetime", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).koj_hkks_tyokusou_upd_datetime = "", DBNull.Value, toYYYYMMDDHHmmSS(dtTyousaKaisya(0).koj_hkks_tyokusou_upd_datetime))))
            End If

            '調査会社フラグ
            .Add(MakeParam("@tys_kaisya_flg", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).tys_kaisya_flg = "", DBNull.Value, dtTyousaKaisya(0).tys_kaisya_flg)))
            '工事会社フラグ
            .Add(MakeParam("@koj_kaisya_flg", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).koj_kaisya_flg = "", DBNull.Value, dtTyousaKaisya(0).koj_kaisya_flg)))
            'JAPAN会区分
            .Add(MakeParam("@japan_kai_kbn", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).japan_kai_kbn = "", DBNull.Value, dtTyousaKaisya(0).japan_kai_kbn)))
            'JAPAN会入会年月
            .Add(MakeParam("@japan_kai_nyuukai_date", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).japan_kai_nyuukai_date = "", DBNull.Value, toYYYYMMDD(dtTyousaKaisya(0).japan_kai_nyuukai_date))))
            'JAPAN会退会年月
            .Add(MakeParam("@japan_kai_taikai_date", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).japan_kai_taikai_date = "", DBNull.Value, toYYYYMMDD(dtTyousaKaisya(0).japan_kai_taikai_date))))

            '全住品区分補足
            .Add(MakeParam("@zenjyuhin_hosoku", SqlDbType.VarChar, 80, IIf(dtTyousaKaisya(0).zenjyuhin_hosoku = "", DBNull.Value, dtTyousaKaisya(0).zenjyuhin_hosoku)))

            'FC店区分
            .Add(MakeParam("@fc_ten_kbn", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).fc_ten_kbn = "", DBNull.Value, dtTyousaKaisya(0).fc_ten_kbn)))
            'FC入会年月
            .Add(MakeParam("@fc_nyuukai_date", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).fc_nyuukai_date = "", DBNull.Value, toYYYYMMDD(dtTyousaKaisya(0).fc_nyuukai_date))))
            'FC退会年月
            .Add(MakeParam("@fc_taikai_date", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).fc_taikai_date = "", DBNull.Value, toYYYYMMDD(dtTyousaKaisya(0).fc_taikai_date))))
            '取消理由
            .Add(MakeParam("@torikesi_riyuu", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).torikesi_riyuu = "", DBNull.Value, dtTyousaKaisya(0).torikesi_riyuu)))
            'ReportJHSトークン有無フラグ
            .Add(MakeParam("@report_jhs_token_flg", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).report_jhs_token_flg = "", DBNull.Value, dtTyousaKaisya(0).report_jhs_token_flg)))
            '宅地地盤調査主任資格有無フラグ
            .Add(MakeParam("@tkt_jbn_tys_syunin_skk_flg", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).tkt_jbn_tys_syunin_skk_flg = "", DBNull.Value, dtTyousaKaisya(0).tkt_jbn_tys_syunin_skk_flg)))
            '============2012/04/12 車龍 405721 追加↓==========================
            '代表者名
            .Add(MakeParam("@daihyousya_mei", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).daihyousya_mei.Equals(String.Empty), DBNull.Value, dtTyousaKaisya(0).daihyousya_mei)))
            '役職名
            .Add(MakeParam("@yakusyoku_mei", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).yakusyoku_mei.Equals(String.Empty), DBNull.Value, dtTyousaKaisya(0).yakusyoku_mei)))
            '============2012/04/12 車龍 405721 追加↑==========================
            '2013/11/04 李宇追加 ↓
            'SDS保持情報
            .Add(MakeParam("@sds_hoji_info", SqlDbType.Int, 10, IIf(dtTyousaKaisya(0).sds_hoji_info.Equals(String.Empty), DBNull.Value, dtTyousaKaisya(0).sds_hoji_info)))
            'SDS台数情報
            .Add(MakeParam("@sds_daisuu_info", SqlDbType.Int, 5, IIf(dtTyousaKaisya(0).sds_daisuu_info.Equals(String.Empty), DBNull.Value, dtTyousaKaisya(0).sds_daisuu_info)))
            '2013/11/04 李宇追加 ↑
            .Add(MakeParam("@jituzai_flg", SqlDbType.Int, 4, IIf(dtTyousaKaisya(0).jituzai_flg.Equals(String.Empty), DBNull.Value, dtTyousaKaisya(0).jituzai_flg)))

            .Add(MakeParam("@a1_lifnr", SqlDbType.VarChar, 10, IIf(dtTyousaKaisya(0).a1_lifnr.Equals(String.Empty), DBNull.Value, dtTyousaKaisya(0).a1_lifnr)))

            '登録ログインユーザーID
            .Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).a1_lifnr = "", DBNull.Value, dtTyousaKaisya(0).a1_lifnr)))

        End With

        ' クエリ実行
        Return ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

    End Function

    '''' <summary>
    '''' ＦＣマスタ
    '''' </summary>
    '''' <param name="strFCCd">建物検査センターコード</param>
    'Public Function SelMfcInfo(ByVal strFCCd As String) As DataTable

    '    ' DataSetインスタンスの生成
    '    Dim dsDataSet As New DataSet

    '    'SQL文の生成
    '    Dim commandTextSb As New StringBuilder

    '    'パラメータ格納
    '    Dim paramList As New List(Of SqlClient.SqlParameter)

    '    'SQL文
    '    With commandTextSb
    '        .AppendLine(" SELECT   ")
    '        .AppendLine("   fc_cd   ")
    '        .AppendLine("   ,fc_nm   ")
    '        .AppendLine(" FROM  ")
    '        .AppendLine("   " & connDbTable_m_fc & " WITH (READCOMMITTED)  ")
    '        .AppendLine(" WHERE  fc_cd=@fc_cd ")
    '        .AppendLine(" ORDER BY fc_cd ")
    '    End With

    '    'パラメータの設定
    '    paramList.Add(MakeParam("@fc_cd", SqlDbType.VarChar, 10, strFCCd))

    '    ' 検索実行
    '    FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
    '                "dsDataSet", paramList.ToArray)

    '    '戻る
    '    Return dsDataSet.Tables(0)

    'End Function

    ''' <summary>
    ''' 工事報告書直送情報取得
    ''' </summary>
    ''' <param name="strUserId">ログインユーザ</param>
    Public Function SelKoujiInfo(ByVal strUserId As String) As DataTable

        ' DataSetインスタンスの生成
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT   ")
            .AppendLine("   MJM.DisplayName ")
            .AppendLine("FROM  ")
            .AppendLine("   m_jiban_ninsyou MJN WITH (READCOMMITTED)  ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("   m_jhs_mailbox MJM WITH (READCOMMITTED)  ")
            .AppendLine("ON ")
            .AppendLine("   MJN.login_user_id = MJM.PrimaryWindowsNTAccount")
            .AppendLine("WHERE ")
            .AppendLine("   MJN.login_user_id = @login_user_id ")

        End With

        'パラメータの設定
        paramList.Add(MakeParam("@login_user_id", SqlDbType.VarChar, 64, strUserId))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        '戻る
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' MailAddress　取得
    ''' </summary>
    ''' <param name="yuubin_no">郵便No</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMailAddress(ByVal yuubin_no As String) As DataSet

        ' DataSetインスタンスの生成
        Dim data As New DataSet

        ' SQL文の生成
        Dim sql As New StringBuilder

        ' パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        ' 加盟店マスト情報のSql 
        sql.AppendLine("SELECT (yuubin_no + ',' + isnull(todoufuken_mei,'')+     isnull(sikutyouson_mei,'')+      isnull(tiiki_mei,'')) as mei")
        sql.AppendLine("    from ")
        sql.AppendLine("    m_yuubin ")
        sql.AppendLine("    where ")
        sql.AppendLine("    yuubin_no like @yuubin_no order by yuubin_no")

        paramList.Add(MakeParam("@yuubin_no", SqlDbType.VarChar, 8, yuubin_no & "%"))
        ' 検索実行
        FillDataset(connStr, CommandType.Text, sql.ToString(), data, _
                   "mei", paramList.ToArray)

        Return data

    End Function

    ''' <summary>
    ''' 日付型変更処理
    ''' </summary>
    ''' <param name="ymd">年月日</param>
    ''' <returns></returns>
    ''' <remarks>2010/01/12 馬艶軍（大連） 新規作成</remarks>
    Public Function toYYYYMMDD(ByVal ymd As Object) As Object

        If ymd.Equals(String.Empty) Or ymd.Equals(DBNull.Value) Then
            Return ymd
        Else
            Return CDate(ymd).ToString("yyyy/MM/dd")
        End If

    End Function

    ''' <summary>
    ''' 日付型変更処理
    ''' </summary>
    ''' <param name="ymd">年月日</param>
    ''' <returns></returns>
    ''' <remarks>2010/01/12 馬艶軍（大連） 新規作成</remarks>
    Public Function toYYYYMMDDHHmmSS(ByVal ymd As Object) As Object

        If ymd.Equals(String.Empty) Or ymd.Equals(DBNull.Value) Then
            Return ymd
        Else
            Return CDate(ymd).ToString("yyyy/MM/dd hh:mm:ss")
        End If

    End Function

    ''' <summary>
    ''' FCコード存在チェック
    ''' </summary>
    Public Function SelFCTenInfo(ByVal strFcCd As String) As DataTable

        ' DataSetインスタンスの生成
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   keiretu_cd ")
            .AppendLine("FROM ")
            .AppendLine("   m_keiretu WITH (READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("   keiretu_cd = @keiretu_cd ")
            .AppendLine("AND ")
            .AppendLine("   kbn = @kbn ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 5, strFcCd))
        paramList.Add(MakeParam("@kbn", SqlDbType.VarChar, 1, "A"))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        '戻る
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' 郵便番号存在チェック
    ''' </summary>
    Public Function SelYuubinInfo(ByVal strBangou As String) As DataTable

        ' DataSetインスタンスの生成
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   yuubin_no ")
            .AppendLine("FROM ")
            .AppendLine("   m_yuubin WITH (READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("   yuubin_no = @yuubin_no ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@yuubin_no", SqlDbType.VarChar, 7, strBangou))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        '戻る
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' 調査会社マスタ
    ''' </summary>
    Public Function SelTyousaKaisya(ByVal strKaisyaCd As String, ByVal strJigyouCd As String, ByVal bloKbn As Boolean) As CommonSearchDataSet.tyousakaisyaTableDataTable

        ' DataSetインスタンスの生成
        Dim dsDataSet As New CommonSearchDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   tys_kaisya_cd, ")
            .AppendLine("   jigyousyo_cd, ")
            .AppendLine("   tys_kaisya_mei ")
            .AppendLine("FROM ")
            .AppendLine("   m_tyousakaisya WITH (READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("   tys_kaisya_cd + jigyousyo_cd = @strKaisyaCd ")
        End With
        If bloKbn = True Then
            commandTextSb.Append(" AND torikesi = @torikesi ")
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
        End If

        'パラメータの設定
        paramList.Add(MakeParam("@strKaisyaCd", SqlDbType.VarChar, 7, strKaisyaCd & strJigyouCd))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, dsDataSet.tyousakaisyaTable.TableName, paramList.ToArray)

        '戻る
        Return dsDataSet.tyousakaisyaTable

    End Function

    ''' <summary>
    ''' FC店マスタ
    ''' </summary>
    Public Function SelFCTen(ByVal strFCCd As String) As CommonSearchDataSet.KeiretuTableDataTable

        ' DataSetインスタンスの生成
        Dim dsDataSet As New CommonSearchDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   kbn, ")
            .AppendLine("   keiretu_cd, ")
            .AppendLine("   keiretu_mei ")
            .AppendLine("FROM ")
            .AppendLine("   m_keiretu WITH (READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("   torikesi = @torikesi ")
            .AppendLine("AND ")
            .AppendLine("   keiretu_cd = @keiretu_cd ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 1, "0"))
        paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 6, strFCCd))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, dsDataSet.KeiretuTable.TableName, paramList.ToArray)

        '戻る
        Return dsDataSet.KeiretuTable

    End Function

    ''' <summary>
    ''' 新会計支払先マスタ
    ''' </summary>
    Public Function SelSKK(ByVal strJigyouCd As String, ByVal strShriCd As String) As DataTable

        ' DataSetインスタンスの生成skk_jigyou_cd
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   skk_jigyou_cd, ")
            .AppendLine("   skk_shri_saki_cd, ")
            .AppendLine("   shri_saki_mei_kanji ")
            .AppendLine("FROM ")
            .AppendLine("   m_sinkaikei_siharai_saki WITH (READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("   skk_jigyou_cd = @skk_jigyou_cd ")
            .AppendLine("AND ")
            .AppendLine("   skk_shri_saki_cd = @skk_shri_saki_cd ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@skk_jigyou_cd", SqlDbType.VarChar, 10, strJigyouCd))
        paramList.Add(MakeParam("@skk_shri_saki_cd", SqlDbType.VarChar, 10, strShriCd))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        '戻る
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' 請求先マスタビュー
    ''' </summary>
    Public Function SelVSeikyuuSakiInfo(ByVal strKbn As String, ByVal strCd As String, ByVal strBrc As String, ByVal blnDelete As Boolean) As CommonSearchDataSet.SeikyuuSakiTable1DataTable

        ' DataSetインスタンスの生成
        Dim dsDataSet As New CommonSearchDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   seikyuu_saki_kbn, ")
            .AppendLine("   RTRIM(seikyuu_saki_cd) AS seikyuu_saki_cd, ")
            .AppendLine("   seikyuu_saki_brc, ")
            .AppendLine("   seikyuu_saki_mei, ")
            .AppendLine("   torikesi ")
            .AppendLine("FROM ")
            .AppendLine("   v_seikyuu_saki_info WITH(READCOMMITTED)  ")
            .AppendLine(" WHERE ")
        End With

        If blnDelete = True Then
            commandTextSb.Append(" torikesi = @torikesi ")
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 1, "0"))
        Else
            commandTextSb.Append("  0 = 0 ")
        End If

        If strCd.Trim <> "" Then
            commandTextSb.AppendLine(" AND seikyuu_saki_cd = @seikyuu_saki_cd ")
        End If
        If strBrc.Trim <> "" Then
            commandTextSb.AppendLine(" AND seikyuu_saki_brc = @seikyuu_saki_brc ")
        End If
        If strKbn.Trim <> "" Then
            commandTextSb.AppendLine(" AND seikyuu_saki_kbn = @seikyuu_saki_kbn ")
        End If

        'パラメータの設定
        'パラメータの設定
        paramList.Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 255, strKbn))
        paramList.Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 255, strCd))
        paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 255, strBrc))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, dsDataSet.SeikyuuSakiTable1.TableName, paramList.ToArray)

        '戻る
        Return dsDataSet.SeikyuuSakiTable1

    End Function

    ''' <summary>
    ''' 営業所マスタ
    ''' </summary>
    Public Function SelEigyousyo(ByVal strFCCd As String) As CommonSearchDataSet.EigyousyoTableDataTable

        ' DataSetインスタンスの生成
        Dim dsDataSet As New CommonSearchDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   eigyousyo_cd, ")
            .AppendLine("   eigyousyo_mei ")
            .AppendLine("FROM ")
            .AppendLine("   m_eigyousyo WITH (READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("   torikesi = @torikesi ")
            .AppendLine("AND ")
            .AppendLine("   eigyousyo_cd = @eigyousyo_cd ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 1, "0"))
        paramList.Add(MakeParam("@eigyousyo_cd", SqlDbType.VarChar, 6, strFCCd))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, dsDataSet.EigyousyoTable.TableName, paramList.ToArray)

        '戻る
        Return dsDataSet.EigyousyoTable

    End Function

    '==============2011/06/24 車龍 【連携調査会社マスタの登録・削除処理】の追加 開始↓======================
    ''' <summary>
    ''' 連携調査会社マスタを取得する
    ''' </summary>
    Public Function SelRenkeiTyousakaisyaMaster(ByVal strTyousakaisyaCd As String, ByVal strJigyousyoCd As String) As Data.DataTable

        ' DataSetインスタンスの生成
        Dim dsDataSet As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT  ")
            .AppendLine("	tys_kaisya_cd ")
            .AppendLine("	,jigyousyo_cd ")
            .AppendLine("FROM ")
            .AppendLine("	m_renkei_tys_kaisya WITH (READCOMMITTED) ")
            .AppendLine("where ")
            .AppendLine("	tys_kaisya_cd = @tys_kaisya_cd ")
            .AppendLine("	and ")
            .AppendLine("	jigyousyo_cd = @jigyousyo_cd ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, strTyousakaisyaCd))
        paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, strJigyousyoCd))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dtRenkeiTyousakaisyaMaster", paramList.ToArray)

        '戻る
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' 連携調査会社マスタを削除する
    ''' </summary>
    Public Function DelRenkeiTyousakaisyaMaster(ByVal strTyousakaisyaCd As String, ByVal strJigyousyoCd As String) As Boolean

        '削除件数
        Dim intDelCount As Integer

        ' DataSetインスタンスの生成
        Dim dsDataSet As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("DELETE  ")
            .AppendLine("FROM  ")
            .AppendLine("	m_renkei_tys_kaisya WITH (UPDLOCK) ")
            .AppendLine("WHERE  ")
            .AppendLine("	tys_kaisya_cd = @tys_kaisya_cd ")
            .AppendLine("	and ")
            .AppendLine("	jigyousyo_cd = @jigyousyo_cd ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, strTyousakaisyaCd))
        paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, strJigyousyoCd))

        '実行
        Try
            intDelCount = ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)
        Catch ex As Exception
            Return False
        End Try

        If intDelCount > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' 連携調査会社マスタを登録する
    ''' </summary>
    Public Function InsRenkeiTyousakaisyaMaster(ByVal strTyousakaisyaCd As String, ByVal strJigyousyoCd As String, ByVal strUserId As String) As Boolean
        '削除件数
        Dim intInsCount As Integer

        ' DataSetインスタンスの生成
        Dim dsDataSet As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("INSERT INTO  ")
            .AppendLine("	m_renkei_tys_kaisya WITH (UPDLOCK) ")
            .AppendLine("	( ")
            .AppendLine("		tys_kaisya_cd ")
            .AppendLine("		,jigyousyo_cd ")
            .AppendLine("		,add_login_user_id ")
            .AppendLine("		,add_datetime ")
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("( ")
            .AppendLine("	@tys_kaisya_cd ")
            .AppendLine("	,@jigyousyo_cd ")
            .AppendLine("	,@add_login_user_id ")
            .AppendLine("	,GETDATE() ")
            .AppendLine(") ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, strTyousakaisyaCd))
        paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, strJigyousyoCd))
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strUserId))

        '実行
        Try
            intInsCount = ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)
        Catch ex As Exception
            Return False
        End Try

        If intInsCount > 0 Then
            Return True
        Else
            Return False
        End If

    End Function
    '==============2011/06/24 車龍 【連携調査会社マスタの登録・削除処理】の追加 終了↑======================

End Class

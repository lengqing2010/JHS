Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper
''' <summary>
''' 請求先マスタ
''' </summary>
''' <history>
''' <para>2010/05/24　馬艶軍(大連)　新規作成</para>
''' </history>
Public Class SeikyuuSakiMasterDataAccess

    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' 請求先登録雛形マスタ
    ''' </summary>
    Public Function SelSeikyuuSakiTourokuHinagataInfo() As DataTable

        ' DataSetインスタンスの生成
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'SQL文
        With commandTextSb
            .AppendLine(" SELECT   ")
            .AppendLine("   seikyuu_saki_brc   ")
            .AppendLine("   ,hyouji_naiyou   ")
            .AppendLine(" FROM  ")
            .AppendLine("   m_seikyuu_saki_touroku_hinagata WITH (READCOMMITTED)  ")
            .AppendLine(" ORDER BY seikyuu_saki_brc ")
        End With

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet")

        '戻る
        Return dsDataSet.Tables(0)

    End Function

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
    ''' 請求先情報の取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelSeikyuuSakiInfo(ByVal strSeikyuuSakiCd As String, ByVal strSeikyuuSakiBrc As String, ByVal SeikyuuSakiKbn As String) As SeikyuuSakiDataSet.m_seikyuu_sakiDataTable

        ' DataSetインスタンスの生成
        Dim dsDataSet As New SeikyuuSakiDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   MSS.seikyuu_saki_cd, ")     '請求先コード
            .AppendLine("   MSS.seikyuu_saki_brc, ")        '請求先枝番
            .AppendLine("   MSS.seikyuu_saki_kbn, ")        '請求先区分
            .AppendLine("   MSS.torikesi, ")        '取消
            .AppendLine("   MSS.skk_jigyousyo_cd, ")        '新会計事業所コード
            .AppendLine("   MSS.kyuu_seikyuu_saki_cd, ")        '旧請求先コード

            .AppendLine("   MSS.skysy_soufu_jyuusyo1, ")        '請求書送付先住所1
            .AppendLine("   MSS.skysy_soufu_jyuusyo2, ")        '請求書送付先住所2
            .AppendLine("   MSS.skysy_soufu_yuubin_no, ")        '請求書送付先郵便番号
            .AppendLine("   MSS.skysy_soufu_tel_no, ")        '請求書送付先電話番号
            .AppendLine("   MSS.skysy_soufu_fax_no, ")        '請求書送付先FAX番号

            .AppendLine("   MSS.tantousya_mei, ")       '担当者名
            .AppendLine("   MSS.seikyuusyo_inji_bukken_mei_flg, ")      '請求書印字物件名フラグ
            .AppendLine("   MSS.nyuukin_kouza_no, ")        '入金口座番号
            .AppendLine("   MSS.seikyuu_sime_date, ")       '請求締め日
            .AppendLine("   MSS.senpou_seikyuu_sime_date, ")        '先方請求締め日
            .AppendLine("   MSS.tyk_koj_seikyuu_timing_flg, ")      '直工事請求タイミングフラグ
            .AppendLine("   MSS.sousai_flg, ")      '相殺フラグ
            .AppendLine("   MSS.kaisyuu_yotei_gessuu, ")        '回収予定月数
            .AppendLine("   MSS.kaisyuu_yotei_date, ")      '回収予定日
            .AppendLine("   MSS.seikyuusyo_hittyk_date, ")      '請求書必着日
            .AppendLine("   MSS.kaisyuu1_syubetu1, ")       '回収1種別1
            .AppendLine("   MSS.kaisyuu1_wariai1, ")        '回収1割合1
            .AppendLine("   MSS.kaisyuu1_tegata_site_gessuu, ")     '回収1手形サイト月数
            .AppendLine("   MSS.kaisyuu1_tegata_site_date, ")       '回収1手形サイト日
            .AppendLine("   MSS.kaisyuu1_seikyuusyo_yousi, ")       '回収1請求書用紙
            .AppendLine("   MSS.kaisyuu1_syubetu2, ")       '回収1種別2
            .AppendLine("   MSS.kaisyuu1_wariai2, ")        '回収1割合2
            .AppendLine("   MSS.kaisyuu1_syubetu3, ")       '回収1種別3
            .AppendLine("   MSS.kaisyuu1_wariai3, ")        '回収1割合3
            .AppendLine("   MSS.kaisyuu_kyoukaigaku, ")     '回収境界額
            .AppendLine("   MSS.kaisyuu2_syubetu1, ")       '回収2種別1
            .AppendLine("   MSS.kaisyuu2_wariai1, ")        '回収2割合1
            .AppendLine("   MSS.kaisyuu2_tegata_site_gessuu, ")     '回収2手形サイト月数
            .AppendLine("   MSS.kaisyuu2_tegata_site_date, ")       '回収2手形サイト日
            .AppendLine("   MSS.kaisyuu2_seikyuusyo_yousi, ")       '回収2請求書用紙
            .AppendLine("   MSS.kaisyuu2_syubetu2, ")       '回収2種別2
            .AppendLine("   MSS.kaisyuu2_wariai2, ")        '回収2割合2
            .AppendLine("   MSS.kaisyuu2_syubetu3, ")       '回収2種別3
            .AppendLine("   MSS.kaisyuu2_wariai3, ")        '回収2割合3
            .AppendLine("   MSS.add_login_user_id, ")       '登録ログインユーザーID
            .AppendLine("   MSS.add_datetime, ")        '登録日時
            .AppendLine("   MSS.upd_login_user_id, ")       '更新ログインユーザーID
            .AppendLine("   MSS.upd_datetime, ")        '更新日時
            .AppendLine("   MSJ.skk_jigyousyo_mei, ")        '事業所名
            .AppendLine("   VSSI.seikyuu_saki_mei ")        '請求先名

            '20100925　名寄先コード、名寄先名　追加　馬艶軍↓↓↓
            .AppendLine("   ,MSS.nayose_saki_cd ")        '名寄先コード
            .AppendLine("   ,MY.nayose_saki_name1 ")        '名寄先名
            '20100925　名寄先コード、名寄先名　追加　馬艶軍↑↑↑

            '==================2011/06/16 車龍  追加 開始↓========================== 
            .AppendLine("   ,MSS.kessanji_nidosime_flg ")   '決算時二度締めフラグ
            '==================2011/06/16 車龍  追加 終了↑==========================

            '==================2012/05/17 車龍 407553の対応 追加↓========================== 
            .AppendLine("   ,MSS.koufuri_ok_flg ") '口振OKフラグ
            .AppendLine("   ,MSS.tougou_tokuisaki_cd ") '統合会計得意先ｺｰﾄﾞ
            .AppendLine("   ,MSS.anzen_kaihi_en ") '安全協力会費_円
            .AppendLine("   ,MSS.anzen_kaihi_wari ") '安全協力会費_割合
            .AppendLine("   ,MSS.bikou ") '備考
            '==================2012/05/17 車龍 407553の対応 追加↑==========================
            '2013/5/29 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↓
            .AppendLine("   ,MSS.ginkou_siten_cd ") '銀行支店コード
            '2013/5/29 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↑

            .AppendLine("FROM ")
            .AppendLine("   m_seikyuu_saki MSS WITH (READCOMMITTED) ")      '請求先マスタ
            .AppendLine("LEFT JOIN ")
            .AppendLine("   m_sinkaikei_jigyousyo MSJ WITH (READCOMMITTED) ")   '新会計事業所マスタ
            .AppendLine("ON ")
            .AppendLine("   MSS.skk_jigyousyo_cd = MSJ.skk_jigyousyo_cd ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("   v_seikyuu_saki_info VSSI WITH (READCOMMITTED) ")   '請求先情報
            .AppendLine("ON ")
            .AppendLine("   MSS.seikyuu_saki_cd = VSSI.seikyuu_saki_cd ")
            .AppendLine("AND ")
            .AppendLine("   MSS.seikyuu_saki_brc = VSSI.seikyuu_saki_brc ")
            .AppendLine("AND ")
            .AppendLine("   MSS.seikyuu_saki_kbn = VSSI.seikyuu_saki_kbn ")

            '20100925　名寄先コード、名寄先名　追加　馬艶軍↓↓↓
            .AppendLine("LEFT JOIN ")
            .AppendLine("   m_yosinkanri MY WITH (READCOMMITTED) ")   '与信管理マスタ
            .AppendLine("ON ")
            .AppendLine("   MSS.nayose_saki_cd = MY.nayose_saki_cd ")
            '20100925　名寄先コード、名寄先名　追加　馬艶軍↑↑↑

            .AppendLine("WHERE ")
            .AppendLine("   MSS.seikyuu_saki_cd = @seikyuu_saki_cd ")
            .AppendLine("AND ")
            .AppendLine("   MSS.seikyuu_saki_brc = @seikyuu_saki_brc ")
            .AppendLine("AND ")
            .AppendLine("   MSS.seikyuu_saki_kbn = @seikyuu_saki_kbn ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 5, strSeikyuuSakiCd))
        paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 2, strSeikyuuSakiBrc))
        paramList.Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 1, SeikyuuSakiKbn))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet,dsDataSet.m_seikyuu_saki.TableName, paramList.ToArray)

        '戻る
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' 請求先マスタ登録
    ''' </summary>
    ''' <param name="dtSeikyuuSaki">登録データ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsSeikyuuSaki(ByVal dtSeikyuuSaki As SeikyuuSakiDataSet.m_seikyuu_sakiDataTable) As Boolean

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("INSERT INTO ")
            .AppendLine("   m_seikyuu_saki WITH (UPDLOCK) ")
            .AppendLine("( ")
            .AppendLine("   seikyuu_saki_cd, ") '請求先コード
            .AppendLine("   seikyuu_saki_brc, ")    '請求先枝番
            .AppendLine("   seikyuu_saki_kbn, ")    '請求先区分
            .AppendLine("   torikesi, ")    '取消
            .AppendLine("   skk_jigyousyo_cd, ")    '新会計事業所コード

            .AppendLine("   kyuu_seikyuu_saki_cd, ")    '旧請求先コード

            .AppendLine("   skysy_soufu_jyuusyo1, ")    '請求書送付先住所1
            .AppendLine("   skysy_soufu_jyuusyo2, ")    '請求書送付先住所2
            .AppendLine("   skysy_soufu_yuubin_no, ")    '請求書送付先郵便番号
            .AppendLine("   skysy_soufu_tel_no, ")    '請求書送付先電話番号
            .AppendLine("   skysy_soufu_fax_no, ")    '請求書送付先FAX番号

            .AppendLine("   tantousya_mei, ")   '担当者名
            .AppendLine("   seikyuusyo_inji_bukken_mei_flg, ")  '請求書印字物件名フラグ
            .AppendLine("   nyuukin_kouza_no, ")    '入金口座番号
            .AppendLine("   seikyuu_sime_date, ")   '請求締め日
            .AppendLine("   senpou_seikyuu_sime_date, ")    '先方請求締め日
            .AppendLine("   tyk_koj_seikyuu_timing_flg, ")  '直工事請求タイミングフラグ
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
            .AppendLine("   kaisyuu2_wariai3, ")    '回収2割合3

            .AppendLine("   add_login_user_id, ")   '登録ログインユーザーID
            .AppendLine("   add_datetime ")    '登録日時

            '20100925　名寄先コード、名寄先名　追加　馬艶軍↓↓↓
            .AppendLine("   ,nayose_saki_cd ")    '名寄先コード
            '20100925　名寄先コード、名寄先名　追加　馬艶軍↑↑↑

            '==================2011/06/16 車龍  追加 開始↓========================== 
            .AppendLine("   ,kessanji_nidosime_flg ")   '決算時二度締めフラグ
            '==================2011/06/16 車龍  追加 終了↑==========================

            '==================2012/05/17 車龍 407553の対応 追加↓========================== 
            .AppendLine("   ,koufuri_ok_flg ") '口振OKフラグ
            .AppendLine("   ,tougou_tokuisaki_cd ") '統合会計得意先ｺｰﾄﾞ
            .AppendLine("   ,anzen_kaihi_en ") '安全協力会費_円
            .AppendLine("   ,anzen_kaihi_wari ") '安全協力会費_割合
            .AppendLine("   ,bikou ") '備考
            '==================2012/05/17 車龍 407553の対応 追加↑==========================

            '2013/5/29 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↓
            .AppendLine("   ,ginkou_siten_cd ")           '銀行支店コード
            '2013/5/29 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↑

            .AppendLine(") ")
            .AppendLine("SELECT ")
            .AppendLine("   @seikyuu_saki_cd, ")    '請求先コード
            .AppendLine("   @seikyuu_saki_brc, ")   '請求先枝番
            .AppendLine("   @seikyuu_saki_kbn, ")   '請求先区分
            .AppendLine("   @torikesi, ")   '取消
            .AppendLine("   @skk_jigyousyo_cd, ")   '新会計事業所コード

            .AppendLine("   @kyuu_seikyuu_saki_cd, ")   '旧請求先コード

            .AppendLine("   @skysy_soufu_jyuusyo1, ")   '請求書送付先住所
            .AppendLine("   @skysy_soufu_jyuusyo2, ")   '請求書送付先住所
            .AppendLine("   @skysy_soufu_yuubin_no, ")   '請求書送付先郵便番号
            .AppendLine("   @skysy_soufu_tel_no, ")   '請求書送付先電話番号
            .AppendLine("   @skysy_soufu_fax_no, ")   '請求書送付先FAX番号

            .AppendLine("   @tantousya_mei, ")  '担当者名
            .AppendLine("   @seikyuusyo_inji_bukken_mei_flg, ") '請求書印字物件名フラグ
            .AppendLine("   @nyuukin_kouza_no, ")   '入金口座番号
            .AppendLine("   @seikyuu_sime_date, ")  '請求締め日
            .AppendLine("   @senpou_seikyuu_sime_date, ")   '先方請求締め日
            .AppendLine("   @tyk_koj_seikyuu_timing_flg, ") '直工事請求タイミングフラグ
            .AppendLine("   @sousai_flg, ") '相殺フラグ
            .AppendLine("   @kaisyuu_yotei_gessuu, ")   '回収予定月数
            .AppendLine("   @kaisyuu_yotei_date, ") '回収予定日
            .AppendLine("   @seikyuusyo_hittyk_date, ") '請求書必着日
            .AppendLine("   @kaisyuu1_syubetu1, ")  '回収1種別1
            .AppendLine("   @kaisyuu1_wariai1, ")   '回収1割合1
            .AppendLine("   @kaisyuu1_tegata_site_gessuu, ")    '回収1手形サイト月数
            .AppendLine("   @kaisyuu1_tegata_site_date, ")  '回収1手形サイト日
            .AppendLine("   @kaisyuu1_seikyuusyo_yousi, ")  '回収1請求書用紙
            .AppendLine("   @kaisyuu1_syubetu2, ")  '回収1種別2
            .AppendLine("   @kaisyuu1_wariai2, ")   '回収1割合2
            .AppendLine("   @kaisyuu1_syubetu3, ")  '回収1種別3
            .AppendLine("   @kaisyuu1_wariai3, ")   '回収1割合3
            .AppendLine("   @kaisyuu_kyoukaigaku, ")    '回収境界額
            .AppendLine("   @kaisyuu2_syubetu1, ")  '回収2種別1
            .AppendLine("   @kaisyuu2_wariai1, ")   '回収2割合1
            .AppendLine("   @kaisyuu2_tegata_site_gessuu, ")    '回収2手形サイト月数
            .AppendLine("   @kaisyuu2_tegata_site_date, ")  '回収2手形サイト日
            .AppendLine("   @kaisyuu2_seikyuusyo_yousi, ")  '回収2請求書用紙
            .AppendLine("   @kaisyuu2_syubetu2, ")  '回収2種別2
            .AppendLine("   @kaisyuu2_wariai2, ")   '回収2割合2
            .AppendLine("   @kaisyuu2_syubetu3, ")  '回収2種別3
            .AppendLine("   @kaisyuu2_wariai3, ")   '回収2割合3
            .AppendLine("   @add_login_user_id, ")  '登録ログインユーザーID
            .AppendLine("   GETDATE() ")   '登録日時
            '20100925　名寄先コード、名寄先名　追加　馬艶軍↓↓↓
            .AppendLine("   ,@nayose_saki_cd ")    '名寄先コード
            '20100925　名寄先コード、名寄先名　追加　馬艶軍↑↑↑

            '==================2011/06/16 車龍  追加 開始↓========================== 
            .AppendLine("   ,@kessanji_nidosime_flg ")   '決算時二度締めフラグ
            '==================2011/06/16 車龍  追加 終了↑==========================

            '==================2012/05/17 車龍 407553の対応 追加↓========================== 
            .AppendLine("   ,@koufuri_ok_flg ") '口振OKフラグ
            .AppendLine("   ,@tougou_tokuisaki_cd ") '統合会計得意先ｺｰﾄﾞ
            .AppendLine("   ,@anzen_kaihi_en ") '安全協力会費_円
            .AppendLine("   ,@anzen_kaihi_wari ") '安全協力会費_割合
            .AppendLine("   ,@bikou ") '備考
            '==================2012/05/17 車龍 407553の対応 追加↑==========================
            '2013/5/29 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↓
            .AppendLine("   ,@ginkou_siten_cd ")           '銀行支店コード
            '2013/5/29 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↑
        End With

        'パラメータの設定
        With paramList
            '請求先コード
            .Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).seikyuu_saki_cd = "", DBNull.Value, dtSeikyuuSaki(0).seikyuu_saki_cd)))
            '請求先枝番
            .Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).seikyuu_saki_brc = "", DBNull.Value, dtSeikyuuSaki(0).seikyuu_saki_brc)))
            '請求先区分
            .Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).seikyuu_saki_kbn = "", DBNull.Value, dtSeikyuuSaki(0).seikyuu_saki_kbn)))
            '取消
            .Add(MakeParam("@torikesi", SqlDbType.VarChar, 255, dtSeikyuuSaki(0).torikesi))
            '新会計事業所コード
            .Add(MakeParam("@skk_jigyousyo_cd", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).skk_jigyousyo_cd = "", DBNull.Value, dtSeikyuuSaki(0).skk_jigyousyo_cd)))

            '旧請求先コード
            .Add(MakeParam("@kyuu_seikyuu_saki_cd", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kyuu_seikyuu_saki_cd = "", DBNull.Value, dtSeikyuuSaki(0).kyuu_seikyuu_saki_cd)))

            '請求書送付先住所1
            .Add(MakeParam("@skysy_soufu_jyuusyo1", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).skysy_soufu_jyuusyo1 = "", DBNull.Value, dtSeikyuuSaki(0).skysy_soufu_jyuusyo1)))
            '請求書送付先住所2
            .Add(MakeParam("@skysy_soufu_jyuusyo2", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).skysy_soufu_jyuusyo2 = "", DBNull.Value, dtSeikyuuSaki(0).skysy_soufu_jyuusyo2)))
            '請求書送付先郵便番号
            .Add(MakeParam("@skysy_soufu_yuubin_no", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).skysy_soufu_yuubin_no = "", DBNull.Value, dtSeikyuuSaki(0).skysy_soufu_yuubin_no)))
            '請求書送付先電話番号
            .Add(MakeParam("@skysy_soufu_tel_no", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).skysy_soufu_tel_no = "", DBNull.Value, dtSeikyuuSaki(0).skysy_soufu_tel_no)))
            '請求書送付先FAX番号
            .Add(MakeParam("@skysy_soufu_fax_no", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).skysy_soufu_fax_no = "", DBNull.Value, dtSeikyuuSaki(0).skysy_soufu_fax_no)))

            '担当者名
            .Add(MakeParam("@tantousya_mei", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).tantousya_mei = "", DBNull.Value, dtSeikyuuSaki(0).tantousya_mei)))
            '請求書印字物件名フラグ
            .Add(MakeParam("@seikyuusyo_inji_bukken_mei_flg", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).seikyuusyo_inji_bukken_mei_flg = "", DBNull.Value, dtSeikyuuSaki(0).seikyuusyo_inji_bukken_mei_flg)))
            '入金口座番号
            .Add(MakeParam("@nyuukin_kouza_no", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).nyuukin_kouza_no = "", DBNull.Value, dtSeikyuuSaki(0).nyuukin_kouza_no)))
            '請求締め日
            .Add(MakeParam("@seikyuu_sime_date", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).seikyuu_sime_date = "", DBNull.Value, dtSeikyuuSaki(0).seikyuu_sime_date)))
            '先方請求締め日
            .Add(MakeParam("@senpou_seikyuu_sime_date", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).senpou_seikyuu_sime_date = "", DBNull.Value, dtSeikyuuSaki(0).senpou_seikyuu_sime_date)))
            '直工事請求タイミングフラグ
            .Add(MakeParam("@tyk_koj_seikyuu_timing_flg", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).tyk_koj_seikyuu_timing_flg = "", DBNull.Value, dtSeikyuuSaki(0).tyk_koj_seikyuu_timing_flg)))
            '相殺フラグ
            .Add(MakeParam("@sousai_flg", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).sousai_flg = "", DBNull.Value, dtSeikyuuSaki(0).sousai_flg)))
            '回収予定月数
            .Add(MakeParam("@kaisyuu_yotei_gessuu", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu_yotei_gessuu = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu_yotei_gessuu)))
            '回収予定日
            .Add(MakeParam("@kaisyuu_yotei_date", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu_yotei_date = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu_yotei_date)))
            '請求書必着日
            .Add(MakeParam("@seikyuusyo_hittyk_date", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).seikyuusyo_hittyk_date = "", DBNull.Value, dtSeikyuuSaki(0).seikyuusyo_hittyk_date)))
            '回収1種別1
            .Add(MakeParam("@kaisyuu1_syubetu1", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_syubetu1 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_syubetu1)))
            '回収1割合1
            .Add(MakeParam("@kaisyuu1_wariai1", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_wariai1 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_wariai1)))
            '回収1手形サイト月数
            .Add(MakeParam("@kaisyuu1_tegata_site_gessuu", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_tegata_site_gessuu = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_tegata_site_gessuu)))
            '回収1手形サイト日
            .Add(MakeParam("@kaisyuu1_tegata_site_date", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_tegata_site_date = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_tegata_site_date)))
            '回収1請求書用紙
            .Add(MakeParam("@kaisyuu1_seikyuusyo_yousi", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_seikyuusyo_yousi = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_seikyuusyo_yousi)))
            '回収1種別2
            .Add(MakeParam("@kaisyuu1_syubetu2", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_syubetu2 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_syubetu2)))
            '回収1割合2
            .Add(MakeParam("@kaisyuu1_wariai2", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_wariai2 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_wariai2)))
            '回収1種別3
            .Add(MakeParam("@kaisyuu1_syubetu3", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_syubetu3 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_syubetu3)))
            '回収1割合3
            .Add(MakeParam("@kaisyuu1_wariai3", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_wariai3 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_wariai3)))
            '回収境界額
            .Add(MakeParam("@kaisyuu_kyoukaigaku", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu_kyoukaigaku = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu_kyoukaigaku)))
            '回収2種別1
            .Add(MakeParam("@kaisyuu2_syubetu1", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_syubetu1 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_syubetu1)))
            '回収2割合1
            .Add(MakeParam("@kaisyuu2_wariai1", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_wariai1 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_wariai1)))
            '回収2手形サイト月数
            .Add(MakeParam("@kaisyuu2_tegata_site_gessuu", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_tegata_site_gessuu = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_tegata_site_gessuu)))
            '回収2手形サイト日
            .Add(MakeParam("@kaisyuu2_tegata_site_date", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_tegata_site_date = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_tegata_site_date)))
            '回収2請求書用紙
            .Add(MakeParam("@kaisyuu2_seikyuusyo_yousi", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_seikyuusyo_yousi = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_seikyuusyo_yousi)))
            '回収2種別2
            .Add(MakeParam("@kaisyuu2_syubetu2", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_syubetu2 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_syubetu2)))
            '回収2割合2
            .Add(MakeParam("@kaisyuu2_wariai2", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_wariai2 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_wariai2)))
            '回収2種別3
            .Add(MakeParam("@kaisyuu2_syubetu3", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_syubetu3 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_syubetu3)))
            '回収2割合3
            .Add(MakeParam("@kaisyuu2_wariai3", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_wariai3 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_wariai3)))
            '登録ログインユーザーID
            .Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).add_login_user_id = "", DBNull.Value, dtSeikyuuSaki(0).add_login_user_id)))

            '20100925　名寄先コード、名寄先名　追加　馬艶軍↓↓↓
            .Add(MakeParam("@nayose_saki_cd", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).nayose_saki_cd = "", DBNull.Value, dtSeikyuuSaki(0).nayose_saki_cd)))
            '20100925　名寄先コード、名寄先名　追加　馬艶軍↑↑↑

            '==================2011/06/16 車龍  追加 開始↓========================== 
            .Add(MakeParam("@kessanji_nidosime_flg", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kessanji_nidosime_flg = "", DBNull.Value, dtSeikyuuSaki(0).kessanji_nidosime_flg)))    '決算時二度締めフラグ
            '==================2011/06/16 車龍  追加 終了↑==========================

            '==================2012/05/17 車龍 407553の対応 追加↓========================== 
            '口振OKフラグ
            .Add(MakeParam("@koufuri_ok_flg", SqlDbType.Int, 10, IIf(dtSeikyuuSaki(0).koufuri_ok_flg = "", DBNull.Value, dtSeikyuuSaki(0).koufuri_ok_flg)))
            '統合会計得意先ｺｰﾄﾞ
            .Add(MakeParam("@tougou_tokuisaki_cd", SqlDbType.VarChar, 10, IIf(dtSeikyuuSaki(0).tougou_tokuisaki_cd = "", DBNull.Value, dtSeikyuuSaki(0).tougou_tokuisaki_cd)))
            '安全協力会費_円
            .Add(MakeParam("@anzen_kaihi_en", SqlDbType.Int, 10, IIf(dtSeikyuuSaki(0).anzen_kaihi_en = "", DBNull.Value, dtSeikyuuSaki(0).anzen_kaihi_en)))
            '安全協力会費_割合
            .Add(MakeParam("@anzen_kaihi_wari", SqlDbType.Decimal, 10, IIf(dtSeikyuuSaki(0).anzen_kaihi_wari = "", DBNull.Value, dtSeikyuuSaki(0).anzen_kaihi_wari)))
            '備考
            .Add(MakeParam("@bikou", SqlDbType.VarChar, 40, IIf(dtSeikyuuSaki(0).bikou = "", DBNull.Value, dtSeikyuuSaki(0).bikou)))
            '==================2012/05/17 車龍 407553の対応 追加↑==========================
            '2013/5/29 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↓
            .Add(MakeParam("@ginkou_siten_cd", SqlDbType.VarChar, 3, IIf(dtSeikyuuSaki(0).ginkou_siten_cd = "", DBNull.Value, dtSeikyuuSaki(0).ginkou_siten_cd))) '銀行支店コード
            '2013/5/29 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↑

        End With

        ' クエリ実行
        Return ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

    End Function

    ''' <summary>
    ''' 排他チェック用
    ''' </summary>
    ''' <param name="strSeikyuuSakiCd">請求先コード</param>
    ''' <param name="strSeikyuuSakiBrc">請求先</param>
    ''' <param name="strKousinDate">更新時間</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelHaita(ByVal strSeikyuuSakiCd As String, ByVal strSeikyuuSakiBrc As String, ByVal SeikyuuSakiKbn As String, ByVal strKousinDate As String) As DataTable

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
            .AppendLine("  m_seikyuu_saki WITH (READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("  seikyuu_saki_cd = @seikyuu_saki_cd  ")
            .AppendLine("AND ")
            .AppendLine("  seikyuu_saki_brc = @seikyuu_saki_brc  ")
            .AppendLine("AND ")
            .AppendLine("  seikyuu_saki_kbn = @seikyuu_saki_kbn  ")
            .AppendLine("AND ")
            .AppendLine("   CONVERT(varchar(19),CONVERT(datetime,upd_datetime,21),21)<>CONVERT(varchar(19),CONVERT(datetime,@upd_datetime,21),21) ")
            .AppendLine("AND ")
            .AppendLine("   upd_datetime IS NOT NULL  ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 5, strSeikyuuSakiCd))
        paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 2, strSeikyuuSakiBrc))
        paramList.Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 1, SeikyuuSakiKbn))
        paramList.Add(MakeParam("@upd_datetime", SqlDbType.VarChar, 19, strKousinDate))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)
    End Function

    ''' <summary>
    ''' 請求先マスタテーブルの修正
    ''' </summary>
    ''' <param name="dtSeikyuuSaki">修正のデータ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdSeikyuuSaki(ByVal dtSeikyuuSaki As SeikyuuSakiDataSet.m_seikyuu_sakiDataTable) As Integer

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("UPDATE ")
            .AppendLine("   m_seikyuu_saki WITH (UPDLOCK) ")
            .AppendLine("SET ")
            .AppendLine("   torikesi = @torikesi, ") '取消
            .AppendLine("   skk_jigyousyo_cd = @skk_jigyousyo_cd, ") '新会計事業所コード

            '==================2011/06/16 車龍  削除 開始↓==========================
            '.AppendLine("   kyuu_seikyuu_saki_cd = @kyuu_seikyuu_saki_cd, ") '旧請求先コード
            '==================2011/06/16 車龍  削除 終了↑==========================

            .AppendLine("   skysy_soufu_jyuusyo1 = @skysy_soufu_jyuusyo1, ")    '請求書送付先住所1
            .AppendLine("   skysy_soufu_jyuusyo2 = @skysy_soufu_jyuusyo2, ")    '請求書送付先住所2
            .AppendLine("   skysy_soufu_yuubin_no = @skysy_soufu_yuubin_no, ")    '請求書送付先郵便番号
            .AppendLine("   skysy_soufu_tel_no = @skysy_soufu_tel_no, ")    '請求書送付先電話番号
            .AppendLine("   skysy_soufu_fax_no = @skysy_soufu_fax_no, ")    '請求書送付先FAX番号

            .AppendLine("   tantousya_mei = @tantousya_mei, ") '担当者名
            .AppendLine("   seikyuusyo_inji_bukken_mei_flg = @seikyuusyo_inji_bukken_mei_flg, ") '請求書印字物件名フラグ
            .AppendLine("   nyuukin_kouza_no = @nyuukin_kouza_no, ") '入金口座番号
            .AppendLine("   seikyuu_sime_date = @seikyuu_sime_date, ") '請求締め日
            .AppendLine("   senpou_seikyuu_sime_date = @senpou_seikyuu_sime_date, ") '先方請求締め日
            .AppendLine("   tyk_koj_seikyuu_timing_flg = @tyk_koj_seikyuu_timing_flg, ") '直工事請求タイミングフラグ
            .AppendLine("   sousai_flg = @sousai_flg, ") '相殺フラグ
            .AppendLine("   kaisyuu_yotei_gessuu = @kaisyuu_yotei_gessuu, ") '回収予定月数
            .AppendLine("   kaisyuu_yotei_date = @kaisyuu_yotei_date, ") '回収予定日
            .AppendLine("   seikyuusyo_hittyk_date = @seikyuusyo_hittyk_date, ") '請求書必着日
            .AppendLine("   kaisyuu1_syubetu1 = @kaisyuu1_syubetu1, ") '回収1種別1
            .AppendLine("   kaisyuu1_wariai1 = @kaisyuu1_wariai1, ") '回収1割合1
            .AppendLine("   kaisyuu1_tegata_site_gessuu = @kaisyuu1_tegata_site_gessuu, ") '回収1手形サイト月数
            .AppendLine("   kaisyuu1_tegata_site_date = @kaisyuu1_tegata_site_date, ") '回収1手形サイト日
            .AppendLine("   kaisyuu1_seikyuusyo_yousi = @kaisyuu1_seikyuusyo_yousi, ") '回収1請求書用紙
            .AppendLine("   kaisyuu1_syubetu2 = @kaisyuu1_syubetu2, ") '回収1種別2
            .AppendLine("   kaisyuu1_wariai2 = @kaisyuu1_wariai2, ") '回収1割合2
            .AppendLine("   kaisyuu1_syubetu3 = @kaisyuu1_syubetu3, ") '回収1種別3
            .AppendLine("   kaisyuu1_wariai3 = @kaisyuu1_wariai3, ") '回収1割合3
            .AppendLine("   kaisyuu_kyoukaigaku = @kaisyuu_kyoukaigaku, ") '回収境界額
            .AppendLine("   kaisyuu2_syubetu1 = @kaisyuu2_syubetu1, ") '回収2種別1
            .AppendLine("   kaisyuu2_wariai1 = @kaisyuu2_wariai1, ") '回収2割合1
            .AppendLine("   kaisyuu2_tegata_site_gessuu = @kaisyuu2_tegata_site_gessuu, ") '回収2手形サイト月数
            .AppendLine("   kaisyuu2_tegata_site_date = @kaisyuu2_tegata_site_date, ") '回収2手形サイト日
            .AppendLine("   kaisyuu2_seikyuusyo_yousi = @kaisyuu2_seikyuusyo_yousi, ") '回収2請求書用紙
            .AppendLine("   kaisyuu2_syubetu2 = @kaisyuu2_syubetu2, ") '回収2種別2
            .AppendLine("   kaisyuu2_wariai2 = @kaisyuu2_wariai2, ") '回収2割合2
            .AppendLine("   kaisyuu2_syubetu3 = @kaisyuu2_syubetu3, ") '回収2種別3
            .AppendLine("   kaisyuu2_wariai3 = @kaisyuu2_wariai3, ") '回収2割合3
            .AppendLine("   upd_login_user_id = @upd_login_user_id, ") '更新ログインユーザーID
            .AppendLine("   upd_datetime = GETDATE() ") '更新日時
            '20100925　名寄先コード、名寄先名　追加　馬艶軍↓↓↓
            .AppendLine("   ,nayose_saki_cd = @nayose_saki_cd ")    '名寄先コード
            '20100925　名寄先コード、名寄先名　追加　馬艶軍↑↑↑

            '==================2011/06/16 車龍  追加 開始↓========================== 
            .AppendLine("   ,kessanji_nidosime_flg = @kessanji_nidosime_flg ")   '決算時二度締めフラグ
            '==================2011/06/16 車龍  追加 終了↑==========================

            '==================2012/05/17 車龍 407553の対応 追加↓========================== 
            .AppendLine("   ,koufuri_ok_flg = @koufuri_ok_flg ") '口振OKフラグ
            .AppendLine("   ,tougou_tokuisaki_cd = @tougou_tokuisaki_cd ") '統合会計得意先ｺｰﾄﾞ
            .AppendLine("   ,anzen_kaihi_en = @anzen_kaihi_en ") '安全協力会費_円
            .AppendLine("   ,anzen_kaihi_wari = @anzen_kaihi_wari ") '安全協力会費_割合
            .AppendLine("   ,bikou = @bikou ") '備考
            '==================2012/05/17 車龍 407553の対応 追加↑==========================
            '2013/5/29 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↓
            .AppendLine("   ,ginkou_siten_cd = @ginkou_siten_cd ") '銀行支店コード
            '2013/5/29 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↑
            .AppendLine("WHERE ")
            .AppendLine("  seikyuu_saki_cd = @seikyuu_saki_cd  ")
            .AppendLine("AND ")
            .AppendLine("  seikyuu_saki_brc = @seikyuu_saki_brc  ")
            .AppendLine("AND ")
            .AppendLine("  seikyuu_saki_kbn = @seikyuu_saki_kbn  ")
        End With

        'パラメータの設定
        With paramList
            '請求先コード
            .Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).seikyuu_saki_cd = "", DBNull.Value, dtSeikyuuSaki(0).seikyuu_saki_cd)))
            '請求先枝番
            .Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).seikyuu_saki_brc = "", DBNull.Value, dtSeikyuuSaki(0).seikyuu_saki_brc)))
            '請求先区分
            .Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).seikyuu_saki_kbn = "", DBNull.Value, dtSeikyuuSaki(0).seikyuu_saki_kbn)))
            '取消
            .Add(MakeParam("@torikesi", SqlDbType.VarChar, 255, dtSeikyuuSaki(0).torikesi))
            '新会計事業所コード
            .Add(MakeParam("@skk_jigyousyo_cd", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).skk_jigyousyo_cd = "", DBNull.Value, dtSeikyuuSaki(0).skk_jigyousyo_cd)))

            '==================2011/06/16 車龍  削除 開始↓==========================
            ''旧請求先コード
            '.Add(MakeParam("@kyuu_seikyuu_saki_cd", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kyuu_seikyuu_saki_cd = "", DBNull.Value, dtSeikyuuSaki(0).kyuu_seikyuu_saki_cd)))
            '==================2011/06/16 車龍  削除 終了↑==========================

            '請求書送付先住所1
            .Add(MakeParam("@skysy_soufu_jyuusyo1", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).skysy_soufu_jyuusyo1 = "", DBNull.Value, dtSeikyuuSaki(0).skysy_soufu_jyuusyo1)))
            '請求書送付先住所2
            .Add(MakeParam("@skysy_soufu_jyuusyo2", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).skysy_soufu_jyuusyo2 = "", DBNull.Value, dtSeikyuuSaki(0).skysy_soufu_jyuusyo2)))
            '請求書送付先郵便番号
            .Add(MakeParam("@skysy_soufu_yuubin_no", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).skysy_soufu_yuubin_no = "", DBNull.Value, dtSeikyuuSaki(0).skysy_soufu_yuubin_no)))
            '請求書送付先電話番号
            .Add(MakeParam("@skysy_soufu_tel_no", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).skysy_soufu_tel_no = "", DBNull.Value, dtSeikyuuSaki(0).skysy_soufu_tel_no)))
            '請求書送付先FAX番号
            .Add(MakeParam("@skysy_soufu_fax_no", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).skysy_soufu_fax_no = "", DBNull.Value, dtSeikyuuSaki(0).skysy_soufu_fax_no)))

            '担当者名
            .Add(MakeParam("@tantousya_mei", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).tantousya_mei = "", DBNull.Value, dtSeikyuuSaki(0).tantousya_mei)))
            '請求書印字物件名フラグ
            .Add(MakeParam("@seikyuusyo_inji_bukken_mei_flg", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).seikyuusyo_inji_bukken_mei_flg = "", DBNull.Value, dtSeikyuuSaki(0).seikyuusyo_inji_bukken_mei_flg)))
            '入金口座番号
            .Add(MakeParam("@nyuukin_kouza_no", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).nyuukin_kouza_no = "", DBNull.Value, dtSeikyuuSaki(0).nyuukin_kouza_no)))
            '請求締め日
            .Add(MakeParam("@seikyuu_sime_date", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).seikyuu_sime_date = "", DBNull.Value, dtSeikyuuSaki(0).seikyuu_sime_date)))
            '先方請求締め日
            .Add(MakeParam("@senpou_seikyuu_sime_date", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).senpou_seikyuu_sime_date = "", DBNull.Value, dtSeikyuuSaki(0).senpou_seikyuu_sime_date)))
            '直工事請求タイミングフラグ
            .Add(MakeParam("@tyk_koj_seikyuu_timing_flg", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).tyk_koj_seikyuu_timing_flg = "", DBNull.Value, dtSeikyuuSaki(0).tyk_koj_seikyuu_timing_flg)))
            '相殺フラグ
            .Add(MakeParam("@sousai_flg", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).sousai_flg = "", DBNull.Value, dtSeikyuuSaki(0).sousai_flg)))
            '回収予定月数
            .Add(MakeParam("@kaisyuu_yotei_gessuu", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu_yotei_gessuu = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu_yotei_gessuu)))
            '回収予定日
            .Add(MakeParam("@kaisyuu_yotei_date", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu_yotei_date = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu_yotei_date)))
            '請求書必着日
            .Add(MakeParam("@seikyuusyo_hittyk_date", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).seikyuusyo_hittyk_date = "", DBNull.Value, dtSeikyuuSaki(0).seikyuusyo_hittyk_date)))
            '回収1種別1
            .Add(MakeParam("@kaisyuu1_syubetu1", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_syubetu1 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_syubetu1)))
            '回収1割合1
            .Add(MakeParam("@kaisyuu1_wariai1", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_wariai1 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_wariai1)))
            '回収1手形サイト月数
            .Add(MakeParam("@kaisyuu1_tegata_site_gessuu", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_tegata_site_gessuu = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_tegata_site_gessuu)))
            '回収1手形サイト日
            .Add(MakeParam("@kaisyuu1_tegata_site_date", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_tegata_site_date = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_tegata_site_date)))
            '回収1請求書用紙
            .Add(MakeParam("@kaisyuu1_seikyuusyo_yousi", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_seikyuusyo_yousi = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_seikyuusyo_yousi)))
            '回収1種別2
            .Add(MakeParam("@kaisyuu1_syubetu2", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_syubetu2 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_syubetu2)))
            '回収1割合2
            .Add(MakeParam("@kaisyuu1_wariai2", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_wariai2 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_wariai2)))
            '回収1種別3
            .Add(MakeParam("@kaisyuu1_syubetu3", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_syubetu3 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_syubetu3)))
            '回収1割合3
            .Add(MakeParam("@kaisyuu1_wariai3", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_wariai3 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_wariai3)))
            '回収境界額
            .Add(MakeParam("@kaisyuu_kyoukaigaku", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu_kyoukaigaku = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu_kyoukaigaku)))
            '回収2種別1
            .Add(MakeParam("@kaisyuu2_syubetu1", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_syubetu1 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_syubetu1)))
            '回収2割合1
            .Add(MakeParam("@kaisyuu2_wariai1", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_wariai1 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_wariai1)))
            '回収2手形サイト月数
            .Add(MakeParam("@kaisyuu2_tegata_site_gessuu", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_tegata_site_gessuu = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_tegata_site_gessuu)))
            '回収2手形サイト日
            .Add(MakeParam("@kaisyuu2_tegata_site_date", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_tegata_site_date = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_tegata_site_date)))
            '回収2請求書用紙
            .Add(MakeParam("@kaisyuu2_seikyuusyo_yousi", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_seikyuusyo_yousi = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_seikyuusyo_yousi)))
            '回収2種別2
            .Add(MakeParam("@kaisyuu2_syubetu2", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_syubetu2 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_syubetu2)))
            '回収2割合2
            .Add(MakeParam("@kaisyuu2_wariai2", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_wariai2 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_wariai2)))
            '回収2種別3
            .Add(MakeParam("@kaisyuu2_syubetu3", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_syubetu3 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_syubetu3)))
            '回収2割合3
            .Add(MakeParam("@kaisyuu2_wariai3", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_wariai3 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_wariai3)))
            '更新ログインユーザーID
            .Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).upd_login_user_id = "", DBNull.Value, dtSeikyuuSaki(0).upd_login_user_id)))

            '20100925　名寄先コード、名寄先名　追加　馬艶軍↓↓↓
            .Add(MakeParam("@nayose_saki_cd", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).nayose_saki_cd = "", DBNull.Value, dtSeikyuuSaki(0).nayose_saki_cd)))
            '20100925　名寄先コード、名寄先名　追加　馬艶軍↑↑↑

            '==================2011/06/16 車龍  追加 開始↓========================== 
            .Add(MakeParam("@kessanji_nidosime_flg", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kessanji_nidosime_flg = "", DBNull.Value, dtSeikyuuSaki(0).kessanji_nidosime_flg)))    '決算時二度締めフラグ
            '==================2011/06/16 車龍  追加 終了↑==========================

            '==================2012/05/17 車龍 407553の対応 追加↓========================== 
            '口振OKフラグ
            .Add(MakeParam("@koufuri_ok_flg", SqlDbType.Int, 10, IIf(dtSeikyuuSaki(0).koufuri_ok_flg = "", DBNull.Value, dtSeikyuuSaki(0).koufuri_ok_flg)))
            '統合会計得意先ｺｰﾄﾞ
            .Add(MakeParam("@tougou_tokuisaki_cd", SqlDbType.VarChar, 10, IIf(dtSeikyuuSaki(0).tougou_tokuisaki_cd = "", DBNull.Value, dtSeikyuuSaki(0).tougou_tokuisaki_cd)))
            '安全協力会費_円
            .Add(MakeParam("@anzen_kaihi_en", SqlDbType.Int, 10, IIf(dtSeikyuuSaki(0).anzen_kaihi_en = "", DBNull.Value, dtSeikyuuSaki(0).anzen_kaihi_en)))
            '安全協力会費_割合
            .Add(MakeParam("@anzen_kaihi_wari", SqlDbType.Decimal, 10, IIf(dtSeikyuuSaki(0).anzen_kaihi_wari = "", DBNull.Value, dtSeikyuuSaki(0).anzen_kaihi_wari)))
            '備考
            .Add(MakeParam("@bikou", SqlDbType.VarChar, 40, IIf(dtSeikyuuSaki(0).bikou = "", DBNull.Value, dtSeikyuuSaki(0).bikou)))
            '==================2012/05/17 車龍 407553の対応 追加↑==========================
            '2013/5/29 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↓
            .Add(MakeParam("@ginkou_siten_cd", SqlDbType.VarChar, 3, IIf(dtSeikyuuSaki(0).ginkou_siten_cd = "", DBNull.Value, dtSeikyuuSaki(0).ginkou_siten_cd))) '銀行支店コード
            '2013/5/29 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↑
        End With

        ' クエリ実行
        Return ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

    End Function

    ''' <summary>
    ''' 請求先登録雛形マスタ情報の取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelSeikyuuSakiHinagataInfo(ByVal strSeikyuuSakiBrc As String) As SeikyuuSakiHinagataDataSet.m_seikyuu_saki_touroku_hinagataDataTable

        ' DataSetインスタンスの生成
        Dim dsDataSet As New SeikyuuSakiHinagataDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   skk_jigyousyo_cd, ") '新会計事業所コード
            .AppendLine("   tantousya_mei, ") '担当者名
            .AppendLine("   seikyuusyo_inji_bukken_mei_flg, ") '請求書印字物件名フラグ
            .AppendLine("   nyuukin_kouza_no, ") '入金口座番号
            .AppendLine("   seikyuu_sime_date, ") '請求締め日
            .AppendLine("   senpou_seikyuu_sime_date, ") '先方請求締め日
            .AppendLine("   sousai_flg, ") '相殺フラグ

            .AppendLine("   tyk_koj_seikyuu_timing_flg, ")  '直工事請求タイミングフラグ

            .AppendLine("   kaisyuu_yotei_gessuu, ") '回収予定月数
            .AppendLine("   kaisyuu_yotei_date, ") '回収予定日
            .AppendLine("   seikyuusyo_hittyk_date, ") '請求書必着日
            .AppendLine("   kaisyuu1_syubetu1, ") '回収1種別1
            .AppendLine("   kaisyuu1_wariai1, ") '回収1割合1
            .AppendLine("   kaisyuu1_tegata_site_gessuu, ") '回収1手形サイト月数
            .AppendLine("   kaisyuu1_tegata_site_date, ") '回収1手形サイト日
            .AppendLine("   kaisyuu1_seikyuusyo_yousi, ") '回収1請求書用紙
            .AppendLine("   kaisyuu1_syubetu2, ") '回収1種別2
            .AppendLine("   kaisyuu1_wariai2, ") '回収1割合2
            .AppendLine("   kaisyuu1_syubetu3, ") '回収1種別3
            .AppendLine("   kaisyuu1_wariai3, ") '回収1割合3
            .AppendLine("   kaisyuu_kyoukaigaku, ") '回収境界額
            .AppendLine("   kaisyuu2_syubetu1, ") '回収2種別1
            .AppendLine("   kaisyuu2_wariai1, ") '回収2割合1
            .AppendLine("   kaisyuu2_tegata_site_gessuu, ") '回収2手形サイト月数
            .AppendLine("   kaisyuu2_tegata_site_date, ") '回収2手形サイト日
            .AppendLine("   kaisyuu2_seikyuusyo_yousi, ") '回収2請求書用紙
            .AppendLine("   kaisyuu2_syubetu2, ") '回収2種別2
            .AppendLine("   kaisyuu2_wariai2, ") '回収2割合2
            .AppendLine("   kaisyuu2_syubetu3, ") '回収2種別3
            .AppendLine("   kaisyuu2_wariai3 ") '回収2割合3
            '2013/5/29 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↓
            .AppendLine("   ,ginkou_siten_cd ") '銀行支店コード
            '2013/5/29 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↑
            .AppendLine("FROM ")
            .AppendLine("   m_seikyuu_saki_touroku_hinagata WITH (READCOMMITTED) ")      '請求先登録雛形マスタ
            .AppendLine("WHERE ")
            .AppendLine("   seikyuu_saki_brc = @seikyuu_saki_brc ")

        End With

        'パラメータの設定
        paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 2, strSeikyuuSakiBrc))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, dsDataSet.m_seikyuu_saki_touroku_hinagata.TableName, paramList.ToArray)

        '戻る
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' 新会計事業所マスタ
    ''' </summary>
    Public Function SelSinkaikeiJigyousyoInfo(ByVal strSkkJigyousyoCd As String) As DataTable

        ' DataSetインスタンスの生成
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   skk_jigyousyo_cd, ")
            .AppendLine("   skk_jigyousyo_mei ")
            .AppendLine("FROM ")
            .AppendLine("   m_sinkaikei_jigyousyo WITH (READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("   skk_jigyousyo_cd = @skk_jigyousyo_cd ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@skk_jigyousyo_cd", SqlDbType.VarChar, 10, strSkkJigyousyoCd))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        '戻る
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' 名寄先コード（与信管理マスタ）
    ''' </summary>
    ''' <history>20100925　名寄先コード、名寄先名　追加　馬艶軍</history>
    Public Function SelNayoseSakiInfo(ByVal strNayoseSakiCd As String) As DataTable

        ' DataSetインスタンスの生成
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   nayose_saki_cd, ")
            .AppendLine("   nayose_saki_name1 ")
            .AppendLine("FROM ")
            .AppendLine("   m_yosinkanri WITH (READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("   nayose_saki_cd = @nayose_saki_cd ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@nayose_saki_cd", SqlDbType.VarChar, 5, strNayoseSakiCd))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        '戻る
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' 請求先マスタビュー
    ''' </summary>
    Public Function SelVSeikyuuSakiInfo(ByVal strKbn As String, ByVal strCd As String, ByVal strBrc As String, ByVal blnDelete As Boolean) As CommonSearchDataSet.SeikyuuSakiTableDataTable

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
            .AppendLine("   seikyuu_saki_mei ")
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
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, dsDataSet.SeikyuuSakiTable.TableName, paramList.ToArray)

        '戻る
        Return dsDataSet.SeikyuuSakiTable

    End Function

    ''' <summary>
    ''' その他マスタ存在チェック
    ''' </summary>
    Public Function SelSonzaiChk(ByVal strKbn As String, ByVal strCd As String, ByVal strBrc As String) As DataTable

        ' DataSetインスタンスの生成
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            Select Case strKbn
                Case "0"
                    .AppendLine("SELECT ")
                    .AppendLine("   kameiten_cd ")
                    .AppendLine("FROM ")
                    .AppendLine("   m_kameiten WITH (READCOMMITTED)  ")
                    .AppendLine("WHERE ")
                    .AppendLine("   kameiten_cd = @kameiten_cd ")
                    'パラメータの設定
                    paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strCd))
                Case "1"
                    .AppendLine("SELECT ")
                    .AppendLine("   tys_kaisya_cd ")
                    .AppendLine("FROM ")
                    .AppendLine("   m_tyousakaisya WITH (READCOMMITTED)  ")
                    .AppendLine("WHERE ")
                    .AppendLine("   tys_kaisya_cd = @tys_kaisya_cd ")
                    .AppendLine("AND ")
                    .AppendLine("   jigyousyo_cd = @jigyousyo_cd ")
                    'パラメータの設定
                    paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, strCd))
                    paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, strBrc))
                Case "2"
                    .AppendLine("SELECT ")
                    .AppendLine("   eigyousyo_cd ")
                    .AppendLine("FROM ")
                    .AppendLine("   m_eigyousyo WITH (READCOMMITTED)  ")
                    .AppendLine("WHERE ")
                    .AppendLine("   eigyousyo_cd = @eigyousyo_cd ")
                    'パラメータの設定
                    paramList.Add(MakeParam("@eigyousyo_cd", SqlDbType.VarChar, 5, strCd))
            End Select
        End With

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

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
    ''' 口振ＯＫフラグを取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2012/05/15 車龍 407553の対応 追加</history>
    Public Function SelKutiburiOkFlg() As Data.DataTable

        ' DataSetインスタンスの生成
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	code AS code ") '--コード ")
            .AppendLine("	,ISNULL(meisyou,'') AS meisyou ") '--名称 ")
            .AppendLine("FROM ")
            .AppendLine("	m_kakutyou_meisyou WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	meisyou_syubetu = @meisyou_syubetu ") '--名称種別 ")
            .AppendLine("ORDER BY ")
            .AppendLine("	hyouji_jyun ASC ") '--表示順 ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.Int, 10, "47"))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsKutiburiOkFlg", paramList.ToArray)

        '戻る
        Return dsDataSet.Tables("dsKutiburiOkFlg")

    End Function

    ''' <summary>
    ''' 銀行支店コードを取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/05/29 楊双 請求書の銀行支店コード増加に伴うEarth改修</history>
    Public Function SelGinkouSitenCd() As Data.DataTable

        ' DataSetインスタンスの生成
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	code AS code ") '--コード ")
            .AppendLine("	,code + ':' + ISNULL(meisyou,'') AS meisyou ") '--名称 ")
            .AppendLine("FROM ")
            .AppendLine("	m_kakutyou_meisyou WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	meisyou_syubetu = @meisyou_syubetu ") '--名称種別 ")
            .AppendLine("ORDER BY ")
            .AppendLine("	hyouji_jyun ASC ") '--表示順 ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.Int, 10, "48"))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsGinkouSitenCd", paramList.ToArray)

        '戻る
        Return dsDataSet.Tables("dsGinkouSitenCd")

    End Function

    ''' <summary>
    ''' Max口振ＯＫフラグを取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2012/05/15 車龍 407553の対応 追加</history>
    Public Function SelMaxKutiburiOkFlg() As Data.DataTable

        ' DataSetインスタンスの生成
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	MAX(CONVERT(INT,tougou_tokuisaki_cd)) AS tougou_tokuisaki_cd_max ") '--統合会計得意先ｺｰﾄﾞ ")
            .AppendLine("FROM ")
            .AppendLine("	m_seikyuu_saki ") '--請求先マスタ ")
        End With

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsMaxKutiburiOkFlg")

        '戻る
        Return dsDataSet.Tables("dsMaxKutiburiOkFlg")

    End Function

End Class

Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper
''' <summary>
''' 調査会社マスタ
''' </summary>
''' <history>
''' <para>2010/05/15　馬艶軍(大連)　新規作成</para>
''' </history>
Public Class EigyousyoMasterDataAccess
    Private connStr As String = ConnectionManager.Instance.ConnectionString

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
    ''' 営業所マスタ
    ''' </summary>
    ''' <param name="strEigyousyo_Cd">営業所コード</param>
    ''' <param name="strEigyousyoCd">営業所コード</param>
    Public Function SelEigyousyoInfo(ByVal strEigyousyo_Cd As String, _
                                         ByVal strEigyousyoCd As String, _
                                         ByVal btn As String) As EigyousyoDataSet.m_eigyousyoDataTable

        ' DataSetインスタンスの生成
        Dim dsDataSet As New EigyousyoDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   ME.eigyousyo_cd, ") '営業所コード
            .AppendLine("   ME.torikesi, ") '取消
            .AppendLine("   ME.eigyousyo_mei, ") '営業所名
            .AppendLine("   ME.eigyousyo_kana, ") '営業所カナ
            .AppendLine("   ME.eigyousyo_mei_inji_umu, ") '営業所名印字有無
            .AppendLine("   ME.seikyuu_saki_cd, ") '請求先コード
            .AppendLine("   ME.seikyuu_saki_brc, ") '請求先枝番
            .AppendLine("   ME.seikyuu_saki_kbn, ") '請求先区分
            .AppendLine("   ME.yuubin_no, ") '郵便番号
            .AppendLine("   ME.jyuusyo1, ") '住所1
            .AppendLine("   ME.jyuusyo2, ") '住所2
            .AppendLine("   ME.tel_no, ") '電話番号
            .AppendLine("   ME.fax_no, ") 'FAX番号
            .AppendLine("   ME.busyo_mei, ") '部署名
            .AppendLine("   ME.seikyuu_saki_mei, ") '請求先名
            .AppendLine("   ME.seikyuu_saki_kana, ") '請求先カナ
            .AppendLine("   ME.skysy_soufu_jyuusyo1, ") '請求書送付先住所1
            .AppendLine("   ME.skysy_soufu_jyuusyo2, ") '請求書送付先住所2
            .AppendLine("   ME.skysy_soufu_yuubin_no, ") '請求書送付先郵便番号
            .AppendLine("   ME.skysy_soufu_tel_no, ") '請求書送付先電話番号
            .AppendLine("   ME.skysy_soufu_fax_no, ") '請求書送付先FAX番号
            .AppendLine("   ME.add_login_user_id, ") '登録ログインユーザーID
            .AppendLine("   ME.add_datetime, ") '登録日時
            .AppendLine("   ME.upd_login_user_id, ") '更新ログインユーザーID
            .AppendLine("   ME.upd_datetime, ") '更新日時
            '=========2012/04/10 車龍 405738 追加↓================================
            .AppendLine("   ME.syuukei_fc_ten_cd, ") '集計FC用ｺｰﾄﾞ
            .AppendLine("   ME.eria_cd, ") 'エリアコード
            .AppendLine("   ME.block_cd, ") 'ブロックコード
            .AppendLine("   ME.fc_ten_kbn, ") 'FC店区分
            .AppendLine("	CASE ")
            .AppendLine("	    WHEN ME.fc_nyuukai_date IS NOT NULL THEN ")
            .AppendLine("	        LEFT(CONVERT(VARCHAR(10),ME.fc_nyuukai_date,111),7) ") '--FC入会年月 ")
            .AppendLine("	    ELSE ")
            .AppendLine("	        '' ")
            .AppendLine("	    END AS fc_nyuukai_date, ")
            .AppendLine("	CASE ")
            .AppendLine("	    WHEN ME.fc_taikai_date IS NOT NULL THEN ")
            .AppendLine("	        LEFT(CONVERT(VARCHAR(10),ME.fc_taikai_date,111),7) ") '--FC退会年月 ")
            .AppendLine("	    ELSE ")
            .AppendLine("	        '' ")
            .AppendLine("	    END AS fc_taikai_date, ")
            .AppendLine("   ME.fc_tys_kaisya_cd, ") '(FC)調査会社コード
            .AppendLine("   ME.fc_jigyousyo_cd, ") '(FC)事業所コード
            '=========2012/04/10 車龍 405738 追加↑================================
            .AppendLine("   VSSI.seikyuu_saki_mei AS seikyuu_saki_mei1 ")        '請求先名
            .AppendLine("FROM ")
            .AppendLine("   m_eigyousyo ME WITH (READCOMMITTED) ")      '営業所マスタ
            .AppendLine("LEFT JOIN ")
            .AppendLine("   v_seikyuu_saki_info VSSI WITH (READCOMMITTED) ")       '請求先情報VIEW
            .AppendLine("ON ")
            .AppendLine("  ME.seikyuu_saki_cd = VSSI.seikyuu_saki_cd ")
            .AppendLine("AND ")
            .AppendLine("  ME.seikyuu_saki_brc = VSSI.seikyuu_saki_brc ")
            .AppendLine("AND ")
            .AppendLine("  ME.seikyuu_saki_kbn = VSSI.seikyuu_saki_kbn ")
            .AppendLine("WHERE ")
            .AppendLine("   ME.eigyousyo_cd IS NOT NULL ")
            If btn = "btnSearch" Then
                .AppendLine("AND ")
                .AppendLine("   ME.eigyousyo_cd = @eigyousyo_cd ")
            Else
                .AppendLine("AND ")
                .AppendLine("   ME.eigyousyo_cd = @eigyousyocd ")
            End If
        End With

        'パラメータの設定
        If btn = "btnSearch" Then
            paramList.Add(MakeParam("@eigyousyo_cd", SqlDbType.VarChar, 5, strEigyousyo_Cd))
        Else
            paramList.Add(MakeParam("@eigyousyocd", SqlDbType.VarChar, 5, strEigyousyoCd))
        End If

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    dsDataSet.m_eigyousyo.TableName, paramList.ToArray)

        '戻る
        Return dsDataSet.m_eigyousyo
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
    ''' 排他チェック用
    ''' </summary>
    ''' <param name="strEigyousyoCd">営業所コード</param>
    ''' <param name="strKousinDate">更新時間</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelHaita(ByVal strEigyousyoCd As String, ByVal strKousinDate As String) As DataTable

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
            .AppendLine("  m_eigyousyo WITH (READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("  eigyousyo_cd = @eigyousyo_cd  ")
            .AppendLine("AND ")
            .AppendLine("   CONVERT(varchar(19),CONVERT(datetime,upd_datetime,21),21)<>CONVERT(varchar(19),CONVERT(datetime,@upd_datetime,21),21) ")
            .AppendLine("AND ")
            .AppendLine("   upd_datetime IS NOT NULL  ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@eigyousyo_cd", SqlDbType.VarChar, 5, strEigyousyoCd))
        paramList.Add(MakeParam("@upd_datetime", SqlDbType.VarChar, 19, strKousinDate))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)
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
            If strTrue = True Then
                .AppendLine("AND ")
                .AppendLine("   torikesi = @torikesi ")
            End If
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 255, strSeikyuuSakiCd))
        paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 255, strSeikyuuSakiBrc))
        paramList.Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 255, strSeikyuuSakiKbn))
        If strTrue = True Then
            paramList.Add(MakeParam("@torikesi", SqlDbType.VarChar, 1, "0"))
        End If

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
            .Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 255, "2"))
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

    ''' <summary>
    ''' 営業所マスタテーブルの修正
    ''' </summary>
    ''' <param name="dtEigyousyo">修正のデータ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdEigyousyo(ByVal dtEigyousyo As EigyousyoDataSet.m_eigyousyoDataTable) As Integer

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("UPDATE ")
            .AppendLine("   m_eigyousyo WITH (UPDLOCK) ")
            .AppendLine("SET ")

            .AppendLine("   torikesi = @torikesi, ")    '取消
            .AppendLine("   eigyousyo_mei = @eigyousyo_mei, ")    '営業所名
            .AppendLine("   eigyousyo_kana = @eigyousyo_kana, ")    '営業所カナ
            .AppendLine("   eigyousyo_mei_inji_umu = @eigyousyo_mei_inji_umu, ")    '営業所名印字有無
            '.AppendLine("   pca_seikyuu_cd = @pca_seikyuu_cd, ")    'PCA請求先コード
            .AppendLine("   seikyuu_saki_cd = @seikyuu_saki_cd, ")    '請求先コード
            .AppendLine("   seikyuu_saki_brc = @seikyuu_saki_brc, ")    '請求先枝番
            .AppendLine("   seikyuu_saki_kbn = @seikyuu_saki_kbn, ")    '請求先区分
            '.AppendLine("   hansokuhin_sime_date = @hansokuhin_sime_date, ")    '販促品締め日
            .AppendLine("   yuubin_no = @yuubin_no, ")    '郵便番号
            .AppendLine("   jyuusyo1 = @jyuusyo1, ")    '住所1
            .AppendLine("   jyuusyo2 = @jyuusyo2, ")    '住所2
            .AppendLine("   tel_no = @tel_no, ")    '電話番号
            .AppendLine("   fax_no = @fax_no, ")    'FAX番号
            .AppendLine("   busyo_mei = @busyo_mei, ")    '部署名
            .AppendLine("   seikyuu_saki_mei = @seikyuu_saki_mei, ")    '請求先名
            .AppendLine("   seikyuu_saki_kana = @seikyuu_saki_kana, ")    '請求先カナ
            .AppendLine("   skysy_soufu_jyuusyo1 = @skysy_soufu_jyuusyo1, ")    '請求書送付先住所1
            .AppendLine("   skysy_soufu_jyuusyo2 = @skysy_soufu_jyuusyo2, ")    '請求書送付先住所2
            .AppendLine("   skysy_soufu_yuubin_no = @skysy_soufu_yuubin_no, ")    '請求書送付先郵便番号
            .AppendLine("   skysy_soufu_tel_no = @skysy_soufu_tel_no, ")    '請求書送付先電話番号
            .AppendLine("   skysy_soufu_fax_no = @skysy_soufu_fax_no, ")    '請求書送付先FAX番号
            '=========2012/04/10 車龍 405738 追加↓================================
            .AppendLine("   syuukei_fc_ten_cd = @syuukei_fc_ten_cd, ")   '集計FC用ｺｰﾄﾞ
            .AppendLine("   eria_cd = @eria_cd, ")   'エリアコード
            .AppendLine("   block_cd = @block_cd, ")   'ブロックコード
            .AppendLine("   fc_ten_kbn = @fc_ten_kbn, ")   'FC店区分
            .AppendLine("   fc_nyuukai_date = @fc_nyuukai_date, ")   'FC入会年月
            .AppendLine("   fc_taikai_date = @fc_taikai_date, ")   'FC退会年月
            .AppendLine("   fc_tys_kaisya_cd = @fc_tys_kaisya_cd, ")   '(FC)調査会社コード
            .AppendLine("   fc_jigyousyo_cd = @fc_jigyousyo_cd, ")   '(FC)事業所コード
            '=========2012/04/10 車龍 405738 追加↑================================
            .AppendLine("   upd_login_user_id = @upd_login_user_id, ")    '更新ログインユーザーID
            .AppendLine("   upd_datetime = GETDATE() ")    '更新日時
            .AppendLine("WHERE ")
            .AppendLine("  eigyousyo_cd = @eigyousyo_cd ")
        End With

        'パラメータの設定
        With paramList
            '調査会社コード
            .Add(MakeParam("@eigyousyo_cd", SqlDbType.VarChar, 255, dtEigyousyo(0).eigyousyo_cd))
            '取消
            .Add(MakeParam("@torikesi", SqlDbType.VarChar, 255, dtEigyousyo(0).torikesi))
            '営業所名
            .Add(MakeParam("@eigyousyo_mei", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).eigyousyo_mei = "", DBNull.Value, dtEigyousyo(0).eigyousyo_mei)))
            '営業所カナ
            .Add(MakeParam("@eigyousyo_kana", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).eigyousyo_kana = "", DBNull.Value, dtEigyousyo(0).eigyousyo_kana)))
            '営業所名印字有無
            .Add(MakeParam("@eigyousyo_mei_inji_umu", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).eigyousyo_mei_inji_umu = "", DBNull.Value, dtEigyousyo(0).eigyousyo_mei_inji_umu)))
            '請求先コード
            .Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).seikyuu_saki_cd = "", DBNull.Value, dtEigyousyo(0).seikyuu_saki_cd)))
            '請求先枝番
            .Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).seikyuu_saki_brc = "", DBNull.Value, dtEigyousyo(0).seikyuu_saki_brc)))
            '請求先区分
            .Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).seikyuu_saki_kbn = "", DBNull.Value, dtEigyousyo(0).seikyuu_saki_kbn)))
            '住所1
            .Add(MakeParam("@jyuusyo1", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).jyuusyo1 = "", DBNull.Value, dtEigyousyo(0).jyuusyo1)))
            '住所2
            .Add(MakeParam("@jyuusyo2", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).jyuusyo2 = "", DBNull.Value, dtEigyousyo(0).jyuusyo2)))
            '郵便番号
            .Add(MakeParam("@yuubin_no", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).yuubin_no = "", DBNull.Value, dtEigyousyo(0).yuubin_no)))
            '電話番号
            .Add(MakeParam("@tel_no", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).tel_no = "", DBNull.Value, dtEigyousyo(0).tel_no)))
            'FAX番号
            .Add(MakeParam("@fax_no", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).fax_no = "", DBNull.Value, dtEigyousyo(0).fax_no)))
            '部署名
            .Add(MakeParam("@busyo_mei", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).busyo_mei = "", DBNull.Value, dtEigyousyo(0).busyo_mei)))
            '請求先名
            .Add(MakeParam("@seikyuu_saki_mei", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).seikyuu_saki_mei = "", DBNull.Value, dtEigyousyo(0).seikyuu_saki_mei)))
            '請求先カナ
            .Add(MakeParam("@seikyuu_saki_kana", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).seikyuu_saki_kana = "", DBNull.Value, dtEigyousyo(0).seikyuu_saki_kana)))
            '請求書送付先住所1
            .Add(MakeParam("@skysy_soufu_jyuusyo1", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).skysy_soufu_jyuusyo1 = "", DBNull.Value, dtEigyousyo(0).skysy_soufu_jyuusyo1)))
            '請求書送付先住所2
            .Add(MakeParam("@skysy_soufu_jyuusyo2", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).skysy_soufu_jyuusyo2 = "", DBNull.Value, dtEigyousyo(0).skysy_soufu_jyuusyo2)))
            '請求書送付先郵便番号
            .Add(MakeParam("@skysy_soufu_yuubin_no", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).skysy_soufu_yuubin_no = "", DBNull.Value, dtEigyousyo(0).skysy_soufu_yuubin_no)))
            '請求書送付先電話番号
            .Add(MakeParam("@skysy_soufu_tel_no", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).skysy_soufu_tel_no = "", DBNull.Value, dtEigyousyo(0).skysy_soufu_tel_no)))
            '請求書送付先FAX番号
            .Add(MakeParam("@skysy_soufu_fax_no", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).skysy_soufu_fax_no = "", DBNull.Value, dtEigyousyo(0).skysy_soufu_fax_no)))

            '=========2012/04/10 車龍 405738 追加↓================================
            '集計FC用ｺｰﾄﾞ
            .Add(MakeParam("@syuukei_fc_ten_cd", SqlDbType.VarChar, 5, IIf(dtEigyousyo(0).syuukei_fc_ten_cd.Trim.Equals(String.Empty), DBNull.Value, dtEigyousyo(0).syuukei_fc_ten_cd)))
            'エリアコード
            .Add(MakeParam("@eria_cd", SqlDbType.VarChar, 2, IIf(dtEigyousyo(0).eria_cd.Trim.Equals(String.Empty), DBNull.Value, dtEigyousyo(0).eria_cd)))
            'ブロックコード
            .Add(MakeParam("@block_cd", SqlDbType.VarChar, 2, IIf(dtEigyousyo(0).block_cd.Trim.Equals(String.Empty), DBNull.Value, dtEigyousyo(0).block_cd)))
            'FC店区分
            .Add(MakeParam("@fc_ten_kbn", SqlDbType.Int, 10, IIf(dtEigyousyo(0).fc_ten_kbn.Trim.Equals(String.Empty), DBNull.Value, dtEigyousyo(0).fc_ten_kbn)))
            'FC入会年月
            .Add(MakeParam("@fc_nyuukai_date", SqlDbType.DateTime, 20, IIf(dtEigyousyo(0).fc_nyuukai_date.Trim.Equals(String.Empty), DBNull.Value, dtEigyousyo(0).fc_nyuukai_date & "/01")))
            'FC退会年月
            .Add(MakeParam("@fc_taikai_date", SqlDbType.DateTime, 20, IIf(dtEigyousyo(0).fc_taikai_date.Trim.Equals(String.Empty), DBNull.Value, dtEigyousyo(0).fc_taikai_date & "/01")))
            '(FC)調査会社コード
            .Add(MakeParam("@fc_tys_kaisya_cd", SqlDbType.VarChar, 5, IIf(dtEigyousyo(0).fc_tys_kaisya_cd.Trim.Equals(String.Empty), DBNull.Value, dtEigyousyo(0).fc_tys_kaisya_cd)))
            '(FC)事業所コード
            .Add(MakeParam("@fc_jigyousyo_cd", SqlDbType.VarChar, 2, IIf(dtEigyousyo(0).fc_jigyousyo_cd.Trim.Equals(String.Empty), DBNull.Value, dtEigyousyo(0).fc_jigyousyo_cd)))
            '=========2012/04/10 車龍 405738 追加↑================================

            '更新ログインユーザーID
            .Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).upd_login_user_id = "", DBNull.Value, dtEigyousyo(0).upd_login_user_id)))
        End With

        ' クエリ実行
        Return ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

    End Function

    ''' <summary>
    ''' 登録処理
    ''' </summary>
    ''' <param name="dtEigyousyo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsEigyousyo(ByVal dtEigyousyo As EigyousyoDataSet.m_eigyousyoDataTable) As Integer

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        With commandTextSb
            .AppendLine("INSERT INTO ")
            .AppendLine("   m_eigyousyo WITH (UPDLOCK) ")
            .AppendLine("( ")
            .AppendLine("   eigyousyo_cd, ")   '営業所コード
            .AppendLine("   torikesi, ")   '取消
            .AppendLine("   eigyousyo_mei, ")   '営業所名
            .AppendLine("   eigyousyo_kana, ")   '営業所カナ
            .AppendLine("   eigyousyo_mei_inji_umu, ")   '営業所名印字有無
            '.AppendLine("   pca_seikyuu_cd, ")   'PCA請求先コード
            .AppendLine("   seikyuu_saki_cd, ")   '請求先コード
            .AppendLine("   seikyuu_saki_brc, ")   '請求先枝番
            .AppendLine("   seikyuu_saki_kbn, ")   '請求先区分
            '.AppendLine("   hansokuhin_sime_date, ")   '販促品締め日
            .AppendLine("   yuubin_no, ")   '郵便番号
            .AppendLine("   jyuusyo1, ")   '住所1
            .AppendLine("   jyuusyo2, ")   '住所2
            .AppendLine("   tel_no, ")   '電話番号
            .AppendLine("   fax_no, ")   'FAX番号
            .AppendLine("   busyo_mei, ")   '部署名
            .AppendLine("   seikyuu_saki_mei, ")   '請求先名
            .AppendLine("   seikyuu_saki_kana, ")   '請求先カナ
            .AppendLine("   skysy_soufu_jyuusyo1, ")   '請求書送付先住所1
            .AppendLine("   skysy_soufu_jyuusyo2, ")   '請求書送付先住所2
            .AppendLine("   skysy_soufu_yuubin_no, ")   '請求書送付先郵便番号
            .AppendLine("   skysy_soufu_tel_no, ")   '請求書送付先電話番号
            .AppendLine("   skysy_soufu_fax_no, ")   '請求書送付先FAX番号
            '=========2012/04/10 車龍 405738 追加↓================================
            .AppendLine("   syuukei_fc_ten_cd, ")   '集計FC用ｺｰﾄﾞ
            .AppendLine("   eria_cd, ")   'エリアコード
            .AppendLine("   block_cd, ")   'ブロックコード
            .AppendLine("   fc_ten_kbn, ")   'FC店区分
            .AppendLine("   fc_nyuukai_date, ")   'FC入会年月
            .AppendLine("   fc_taikai_date, ")   'FC退会年月
            .AppendLine("   fc_tys_kaisya_cd, ")   '(FC)調査会社コード
            .AppendLine("   fc_jigyousyo_cd, ")   '(FC)事業所コード
            '=========2012/04/10 車龍 405738 追加↑================================
            .AppendLine("   add_login_user_id, ")   '登録ログインユーザーID
            .AppendLine("   add_datetime ")   '登録日時
            .AppendLine(") ")
            .AppendLine("SELECT ")
            .AppendLine("   @eigyousyo_cd, ")   '営業所コード
            .AppendLine("   @torikesi, ")   '取消
            .AppendLine("   @eigyousyo_mei, ")   '営業所名
            .AppendLine("   @eigyousyo_kana, ")   '営業所カナ
            .AppendLine("   @eigyousyo_mei_inji_umu, ")   '営業所名印字有無
            '.AppendLine("   @pca_seikyuu_cd, ")   'PCA請求先コード
            .AppendLine("   @seikyuu_saki_cd, ")   '請求先コード
            .AppendLine("   @seikyuu_saki_brc, ")   '請求先枝番
            .AppendLine("   @seikyuu_saki_kbn, ")   '請求先区分
            '.AppendLine("   @hansokuhin_sime_date, ")   '販促品締め日
            .AppendLine("   @yuubin_no, ")   '郵便番号
            .AppendLine("   @jyuusyo1, ")   '住所1
            .AppendLine("   @jyuusyo2, ")   '住所2
            .AppendLine("   @tel_no, ")   '電話番号
            .AppendLine("   @fax_no, ")   'FAX番号
            .AppendLine("   @busyo_mei, ")   '部署名
            .AppendLine("   @seikyuu_saki_mei, ")   '請求先名
            .AppendLine("   @seikyuu_saki_kana, ")   '請求先カナ
            .AppendLine("   @skysy_soufu_jyuusyo1, ")   '請求書送付先住所1
            .AppendLine("   @skysy_soufu_jyuusyo2, ")   '請求書送付先住所2
            .AppendLine("   @skysy_soufu_yuubin_no, ")   '請求書送付先郵便番号
            .AppendLine("   @skysy_soufu_tel_no, ")   '請求書送付先電話番号
            .AppendLine("   @skysy_soufu_fax_no, ")   '請求書送付先FAX番号
            '=========2012/04/10 車龍 405738 追加↓================================
            .AppendLine("   @syuukei_fc_ten_cd, ")   '集計FC用ｺｰﾄﾞ
            .AppendLine("   @eria_cd, ")   'エリアコード
            .AppendLine("   @block_cd, ")   'ブロックコード
            .AppendLine("   @fc_ten_kbn, ")   'FC店区分
            .AppendLine("   @fc_nyuukai_date, ")   'FC入会年月
            .AppendLine("   @fc_taikai_date, ")   'FC退会年月
            .AppendLine("   @fc_tys_kaisya_cd, ")   '(FC)調査会社コード
            .AppendLine("   @fc_jigyousyo_cd, ")   '(FC)事業所コード
            '=========2012/04/10 車龍 405738 追加↑================================
            .AppendLine("   @add_login_user_id, ")   '登録ログインユーザーID
            .AppendLine("   GETDATE() ")   '登録日時
        End With

        'パラメータの設定
        With paramList
            '調査会社コード
            .Add(MakeParam("@eigyousyo_cd", SqlDbType.VarChar, 255, dtEigyousyo(0).eigyousyo_cd))
            '取消
            .Add(MakeParam("@torikesi", SqlDbType.VarChar, 255, dtEigyousyo(0).torikesi))
            '営業所名
            .Add(MakeParam("@eigyousyo_mei", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).eigyousyo_mei = "", DBNull.Value, dtEigyousyo(0).eigyousyo_mei)))
            '営業所カナ
            .Add(MakeParam("@eigyousyo_kana", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).eigyousyo_kana = "", DBNull.Value, dtEigyousyo(0).eigyousyo_kana)))
            '営業所名印字有無
            .Add(MakeParam("@eigyousyo_mei_inji_umu", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).eigyousyo_mei_inji_umu = "", DBNull.Value, dtEigyousyo(0).eigyousyo_mei_inji_umu)))
            '請求先コード
            .Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).seikyuu_saki_cd = "", DBNull.Value, dtEigyousyo(0).seikyuu_saki_cd)))
            '請求先枝番
            .Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).seikyuu_saki_brc = "", DBNull.Value, dtEigyousyo(0).seikyuu_saki_brc)))
            '請求先区分
            .Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).seikyuu_saki_kbn = "", DBNull.Value, dtEigyousyo(0).seikyuu_saki_kbn)))
            '住所1
            .Add(MakeParam("@jyuusyo1", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).jyuusyo1 = "", DBNull.Value, dtEigyousyo(0).jyuusyo1)))
            '住所2
            .Add(MakeParam("@jyuusyo2", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).jyuusyo2 = "", DBNull.Value, dtEigyousyo(0).jyuusyo2)))
            '郵便番号
            .Add(MakeParam("@yuubin_no", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).yuubin_no = "", DBNull.Value, dtEigyousyo(0).yuubin_no)))
            '電話番号
            .Add(MakeParam("@tel_no", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).tel_no = "", DBNull.Value, dtEigyousyo(0).tel_no)))
            'FAX番号
            .Add(MakeParam("@fax_no", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).fax_no = "", DBNull.Value, dtEigyousyo(0).fax_no)))
            '部署名
            .Add(MakeParam("@busyo_mei", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).busyo_mei = "", DBNull.Value, dtEigyousyo(0).busyo_mei)))
            '請求先名
            .Add(MakeParam("@seikyuu_saki_mei", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).seikyuu_saki_mei = "", DBNull.Value, dtEigyousyo(0).seikyuu_saki_mei)))
            '請求先カナ
            .Add(MakeParam("@seikyuu_saki_kana", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).seikyuu_saki_kana = "", DBNull.Value, dtEigyousyo(0).seikyuu_saki_kana)))
            '請求書送付先住所1
            .Add(MakeParam("@skysy_soufu_jyuusyo1", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).skysy_soufu_jyuusyo1 = "", DBNull.Value, dtEigyousyo(0).skysy_soufu_jyuusyo1)))
            '請求書送付先住所2
            .Add(MakeParam("@skysy_soufu_jyuusyo2", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).skysy_soufu_jyuusyo2 = "", DBNull.Value, dtEigyousyo(0).skysy_soufu_jyuusyo2)))
            '請求書送付先郵便番号
            .Add(MakeParam("@skysy_soufu_yuubin_no", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).skysy_soufu_yuubin_no = "", DBNull.Value, dtEigyousyo(0).skysy_soufu_yuubin_no)))
            '請求書送付先電話番号
            .Add(MakeParam("@skysy_soufu_tel_no", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).skysy_soufu_tel_no = "", DBNull.Value, dtEigyousyo(0).skysy_soufu_tel_no)))
            '請求書送付先FAX番号
            .Add(MakeParam("@skysy_soufu_fax_no", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).skysy_soufu_fax_no = "", DBNull.Value, dtEigyousyo(0).skysy_soufu_fax_no)))

            '=========2012/04/10 車龍 405738 追加↓================================
            '集計FC用ｺｰﾄﾞ
            .Add(MakeParam("@syuukei_fc_ten_cd", SqlDbType.VarChar, 5, IIf(dtEigyousyo(0).syuukei_fc_ten_cd.Trim.Equals(String.Empty), DBNull.Value, dtEigyousyo(0).syuukei_fc_ten_cd)))
            'エリアコード
            .Add(MakeParam("@eria_cd", SqlDbType.VarChar, 2, IIf(dtEigyousyo(0).eria_cd.Trim.Equals(String.Empty), DBNull.Value, dtEigyousyo(0).eria_cd)))
            'ブロックコード
            .Add(MakeParam("@block_cd", SqlDbType.VarChar, 2, IIf(dtEigyousyo(0).block_cd.Trim.Equals(String.Empty), DBNull.Value, dtEigyousyo(0).block_cd)))
            'FC店区分
            .Add(MakeParam("@fc_ten_kbn", SqlDbType.Int, 10, IIf(dtEigyousyo(0).fc_ten_kbn.Trim.Equals(String.Empty), DBNull.Value, dtEigyousyo(0).fc_ten_kbn)))
            'FC入会年月
            .Add(MakeParam("@fc_nyuukai_date", SqlDbType.DateTime, 20, IIf(dtEigyousyo(0).fc_nyuukai_date.Trim.Equals(String.Empty), DBNull.Value, dtEigyousyo(0).fc_nyuukai_date & "/01")))
            'FC退会年月
            .Add(MakeParam("@fc_taikai_date", SqlDbType.DateTime, 20, IIf(dtEigyousyo(0).fc_taikai_date.Trim.Equals(String.Empty), DBNull.Value, dtEigyousyo(0).fc_taikai_date & "/01")))
            '(FC)調査会社コード
            .Add(MakeParam("@fc_tys_kaisya_cd", SqlDbType.VarChar, 5, IIf(dtEigyousyo(0).fc_tys_kaisya_cd.Trim.Equals(String.Empty), DBNull.Value, dtEigyousyo(0).fc_tys_kaisya_cd)))
            '(FC)事業所コード
            .Add(MakeParam("@fc_jigyousyo_cd", SqlDbType.VarChar, 2, IIf(dtEigyousyo(0).fc_jigyousyo_cd.Trim.Equals(String.Empty), DBNull.Value, dtEigyousyo(0).fc_jigyousyo_cd)))
            '=========2012/04/10 車龍 405738 追加↑================================

            '登録ログインユーザーID
            .Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).add_login_user_id = "", DBNull.Value, dtEigyousyo(0).add_login_user_id)))
        End With

        ' クエリ実行
        Return ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

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
    ''' 加盟店マスタを取得する。
    ''' </summary>
    Public Function SelKameiten(ByVal strEigyousyoCd As String) As DataTable

        ' DataSetインスタンスの生成
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   kameiten_cd ")
            .AppendLine("FROM ")
            .AppendLine("   m_kameiten WITH (READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("   eigyousyo_cd = @eigyousyo_cd ")
            .AppendLine("AND ")
            .AppendLine("   (kbn = @kbn ")
            .AppendLine("   OR kbn = @kbn2 )")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@eigyousyo_cd", SqlDbType.VarChar, 5, strEigyousyoCd))
        paramList.Add(MakeParam("@kbn", SqlDbType.VarChar, 1, "A"))
        paramList.Add(MakeParam("@kbn2", SqlDbType.VarChar, 1, "C"))
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        '戻る
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' 加盟店住所マスタ
    ''' </summary>
    Public Function SelKameitenJyuusyo(ByVal strKameitenCd As String, Optional ByVal strFlg As String = "") As DataTable

        ' DataSetインスタンスの生成
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   kameiten_cd ")
            .AppendLine("FROM ")
            .AppendLine("   m_kameiten_jyuusyo WITH (READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("   kameiten_cd = @kameiten_cd ")
            If strFlg = "1" Then
                .AppendLine("AND ")
                .AppendLine("   (jyuusyo_no <> @jyuusyo_no OR jyuusyo_no IS NULL) ")
            Else
                .AppendLine("AND ")
                .AppendLine("   jyuusyo_no = @jyuusyo_no ")
            End If
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@jyuusyo_no", SqlDbType.VarChar, 1, "2"))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        '戻る
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' 加盟店住所マスタ更新と追加内容取得
    ''' </summary>
    Public Function SelNaiyou(ByVal strKameitenCd As String) As DataTable

        ' DataSetインスタンスの生成
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   ME.jyuusyo1, ")
            .AppendLine("   ME.jyuusyo2, ")
            .AppendLine("   ME.yuubin_no, ")
            .AppendLine("   ME.tel_no, ")
            .AppendLine("   ME.fax_no, ")
            .AppendLine("   ME.busyo_mei ")
            .AppendLine("FROM ")
            .AppendLine("   m_kameiten MK WITH (READCOMMITTED)  ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("   m_eigyousyo ME WITH (READCOMMITTED) ")
            .AppendLine("ON ")
            .AppendLine("   MK.eigyousyo_cd = ME.eigyousyo_cd ")
            .AppendLine("WHERE ")
            .AppendLine("   MK.kameiten_cd = @kameiten_cd ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        '戻る
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' 加盟店住所マスタ追加処理
    ''' </summary>
    ''' <param name="strKameitenCd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsKameitenJyuusyo(ByVal strKameitenCd As String, ByVal dtNaiyou As Data.DataTable, ByVal strFlg As String, ByVal strUserId As String) As Integer

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        With commandTextSb
            .AppendLine("INSERT INTO ")
            .AppendLine("   m_kameiten_jyuusyo WITH (UPDLOCK) ")
            .AppendLine("( ")
            .AppendLine("   kameiten_cd, ")   '加盟店コード
            .AppendLine("   jyuusyo_no, ")   '住所NO
            .AppendLine("   jyuusyo1, ")   '住所1
            .AppendLine("   jyuusyo2, ")   '住所2
            .AppendLine("   yuubin_no, ")   '郵便番号
            .AppendLine("   tel_no, ")   '電話番号
            .AppendLine("   fax_no, ")   'FAX番号
            .AppendLine("   busyo_mei, ")   '部署名
            .AppendLine("   daihyousya_mei, ")   '代表者名
            .AppendLine("   add_nengetu, ")   '登録年月
            .AppendLine("   seikyuusyo_flg, ")   '請求書FLG
            .AppendLine("   hosyousyo_flg, ")   '保証書FLG
            .AppendLine("   hkks_flg, ")   '報告書FLG
            .AppendLine("   teiki_kankou_flg, ")   '定期刊行FLG
            .AppendLine("   bikou1, ")   '備考1
            .AppendLine("   bikou2, ")   '備考2
            .AppendLine("   upd_date, ")   '更新日
            .AppendLine("   mail_address, ")   'メールアドレス
            .AppendLine("   kasi_hosyousyo_flg, ")   '瑕疵保証書FLG
            .AppendLine("   koj_hkks_flg, ")   '工事報告書FLG
            .AppendLine("   kensa_hkks_flg, ")   '検査報告書FLG
            .AppendLine("   add_login_user_id, ")   '登録ログインユーザーID
            .AppendLine("   add_datetime ")   '登録日時
            .AppendLine(") ")
            .AppendLine("SELECT ")
            .AppendLine("   @kameiten_cd, ")   '加盟店コード
            .AppendLine("   @jyuusyo_no, ")   '住所NO
            .AppendLine("   @jyuusyo1, ")   '住所1
            .AppendLine("   @jyuusyo2, ")   '住所2
            .AppendLine("   @yuubin_no, ")   '郵便番号
            .AppendLine("   @tel_no, ")   '電話番号
            .AppendLine("   @fax_no, ")   'FAX番号
            .AppendLine("   @busyo_mei, ")   '部署名
            .AppendLine("   @daihyousya_mei, ")   '代表者名
            .AppendLine("   @add_nengetu, ")   '登録年月
            .AppendLine("   @seikyuusyo_flg, ")   '請求書FLG
            .AppendLine("   @hosyousyo_flg, ")   '保証書FLG
            .AppendLine("   @hkks_flg, ")   '報告書FLG
            .AppendLine("   @teiki_kankou_flg, ")   '定期刊行FLG
            .AppendLine("   @bikou1, ")   '備考1
            .AppendLine("   @bikou2, ")   '備考2
            .AppendLine("   GETDATE(), ")   '更新日
            .AppendLine("   @mail_address, ")   'メールアドレス
            .AppendLine("   @kasi_hosyousyo_flg, ")   '瑕疵保証書FLG
            .AppendLine("   @koj_hkks_flg, ")   '工事報告書FLG
            .AppendLine("   @kensa_hkks_flg, ")   '検査報告書FLG
            .AppendLine("   @add_login_user_id, ")   '登録ログインユーザーID
            .AppendLine("   GETDATE() ")   '登録日時
        End With

        'パラメータの設定
        With paramList
            '加盟店コード
            .Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 255, strKameitenCd.ToUpper))
            '住所NO
            .Add(MakeParam("@jyuusyo_no", SqlDbType.VarChar, 255, "2"))
           
            '住所1
            .Add(MakeParam("@jyuusyo1", SqlDbType.VarChar, 255, IIf(TrimNull(dtNaiyou.Rows(0).Item("jyuusyo1")) = "", DBNull.Value, dtNaiyou.Rows(0).Item("jyuusyo1"))))
            '住所2
            .Add(MakeParam("@jyuusyo2", SqlDbType.VarChar, 255, IIf(TrimNull(dtNaiyou.Rows(0).Item("jyuusyo2")) = "", DBNull.Value, dtNaiyou.Rows(0).Item("jyuusyo2"))))
            '郵便番号
            .Add(MakeParam("@yuubin_no", SqlDbType.VarChar, 255, IIf(TrimNull(dtNaiyou.Rows(0).Item("yuubin_no")) = "", DBNull.Value, dtNaiyou.Rows(0).Item("yuubin_no"))))
            '電話番号
            .Add(MakeParam("@tel_no", SqlDbType.VarChar, 255, IIf(TrimNull(dtNaiyou.Rows(0).Item("tel_no")) = "", DBNull.Value, dtNaiyou.Rows(0).Item("tel_no"))))
            'FAX番号
            .Add(MakeParam("@fax_no", SqlDbType.VarChar, 255, IIf(TrimNull(dtNaiyou.Rows(0).Item("fax_no")) = "", DBNull.Value, dtNaiyou.Rows(0).Item("fax_no"))))
            '部署名
            .Add(MakeParam("@busyo_mei", SqlDbType.VarChar, 255, IIf(TrimNull(dtNaiyou.Rows(0).Item("busyo_mei")) = "", DBNull.Value, dtNaiyou.Rows(0).Item("busyo_mei"))))

            '代表者名
            .Add(MakeParam("@daihyousya_mei", SqlDbType.VarChar, 255, DBNull.Value))
            '登録年月
            .Add(MakeParam("@add_nengetu", SqlDbType.VarChar, 255, DBNull.Value))
            '請求書FLG
            .Add(MakeParam("@seikyuusyo_flg", SqlDbType.VarChar, 255, strFlg))
            '保証書FLG
            .Add(MakeParam("@hosyousyo_flg", SqlDbType.VarChar, 255, strFlg))
            '報告書FLG
            .Add(MakeParam("@hkks_flg", SqlDbType.VarChar, 255, strFlg))
            '定期刊行FLG
            .Add(MakeParam("@teiki_kankou_flg", SqlDbType.VarChar, 255, strFlg))

            '備考1
            .Add(MakeParam("@bikou1", SqlDbType.VarChar, 255, DBNull.Value))
            '備考2
            .Add(MakeParam("@bikou2", SqlDbType.VarChar, 255, DBNull.Value))
            'メールアドレス
            .Add(MakeParam("@mail_address", SqlDbType.VarChar, 255, DBNull.Value))

            '瑕疵保証書FLG
            .Add(MakeParam("@kasi_hosyousyo_flg", SqlDbType.VarChar, 255, strFlg))
            '工事報告書FLG
            .Add(MakeParam("@koj_hkks_flg", SqlDbType.VarChar, 255, strFlg))
            '検査報告書FLG
            .Add(MakeParam("@kensa_hkks_flg", SqlDbType.VarChar, 255, strFlg))

            '登録ログインユーザーID
            .Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 255, strUserId))
        End With

        ' クエリ実行
        Return ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

    End Function

    ''' <summary>
    ''' 加盟店住所マスタ更新処理
    ''' </summary>
    ''' <param name="strKameitenCd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdKameitenJyuusyo(ByVal strKameitenCd As String, ByVal dtNaiyou As Data.DataTable, ByVal strUserId As String) As Integer

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        With commandTextSb
            .AppendLine("UPDATE ")
            .AppendLine("   m_kameiten_jyuusyo WITH (UPDLOCK) ")
            .AppendLine("SET ")
            .AppendLine("   jyuusyo1 = @jyuusyo1, ")   '住所1
            .AppendLine("   jyuusyo2 = @jyuusyo2, ")   '住所2
            .AppendLine("   yuubin_no = @yuubin_no, ")   '郵便番号
            .AppendLine("   tel_no = @tel_no, ")   '電話番号
            .AppendLine("   fax_no = @fax_no, ")   'FAX番号
            .AppendLine("   busyo_mei = @busyo_mei, ")   '部署名
            .AppendLine("   upd_date = GETDATE(), ")   '更新日
            .AppendLine("   upd_login_user_id = @upd_login_user_id, ")   '更新ログインユーザーID
            .AppendLine("   upd_datetime = GETDATE() ")   '更新日時
            .AppendLine("WHERE ")
            .AppendLine("  kameiten_cd = @kameiten_cd ")
            .AppendLine("AND ")
            .AppendLine("  jyuusyo_no = @jyuusyo_no ")
        End With

        'パラメータの設定
        With paramList
            '加盟店コード
            .Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 255, strKameitenCd))

            '住所1
            .Add(MakeParam("@jyuusyo1", SqlDbType.VarChar, 255, IIf(TrimNull(dtNaiyou.Rows(0).Item("jyuusyo1")) = "", DBNull.Value, dtNaiyou.Rows(0).Item("jyuusyo1"))))
            '住所2
            .Add(MakeParam("@jyuusyo2", SqlDbType.VarChar, 255, IIf(TrimNull(dtNaiyou.Rows(0).Item("jyuusyo2")) = "", DBNull.Value, dtNaiyou.Rows(0).Item("jyuusyo2"))))
            '郵便番号
            .Add(MakeParam("@yuubin_no", SqlDbType.VarChar, 255, IIf(TrimNull(dtNaiyou.Rows(0).Item("yuubin_no")) = "", DBNull.Value, dtNaiyou.Rows(0).Item("yuubin_no"))))
            '電話番号
            .Add(MakeParam("@tel_no", SqlDbType.VarChar, 255, IIf(TrimNull(dtNaiyou.Rows(0).Item("tel_no")) = "", DBNull.Value, dtNaiyou.Rows(0).Item("tel_no"))))
            'FAX番号
            .Add(MakeParam("@fax_no", SqlDbType.VarChar, 255, IIf(TrimNull(dtNaiyou.Rows(0).Item("fax_no")) = "", DBNull.Value, dtNaiyou.Rows(0).Item("fax_no"))))
            '部署名
            .Add(MakeParam("@busyo_mei", SqlDbType.VarChar, 255, IIf(TrimNull(dtNaiyou.Rows(0).Item("busyo_mei")) = "", DBNull.Value, dtNaiyou.Rows(0).Item("busyo_mei"))))

            '住所No
            .Add(MakeParam("@jyuusyo_no", SqlDbType.VarChar, 255, "2"))

            '更新ログインユーザーID
            .Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 255, strUserId))
        End With

        ' クエリ実行
        Return ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

    End Function


    ''' <summary>
    ''' 加盟店住所マスタ連携管理テーブル
    ''' </summary>
    ''' <history>20101108 馬艶軍 加盟店住所マスタ連携管理テーブルも追加・更新する必要があります。</history>
    Public Function SelKameitenJyuusyoRenkei(ByVal strKameitenCd As String) As DataTable

        ' DataSetインスタンスの生成
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   kameiten_cd ")
            .AppendLine("FROM ")
            .AppendLine("   m_kameiten_jyuusyo_renkei WITH (READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("   kameiten_cd = @kameiten_cd ")
            .AppendLine("AND ")
            .AppendLine("   jyuusyo_no = @jyuusyo_no ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@jyuusyo_no", SqlDbType.VarChar, 1, "2"))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        '戻る
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' 加盟店住所マスタ連携管理テーブルの更新処理
    ''' </summary>
    ''' <history>20101108 馬艶軍 加盟店住所マスタ連携管理テーブルも追加・更新する必要があります。</history>
    Public Function UpdKameitenJyuusyoRenkei(ByVal strKameitenCd As String, ByVal strUserId As String) As Integer

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        With commandTextSb
            .AppendLine("UPDATE ")
            .AppendLine("   m_kameiten_jyuusyo_renkei WITH (UPDLOCK) ")
            .AppendLine("SET ")
            .AppendLine("   renkei_siji_cd = @renkei_siji_cd, ")   '連携指示コード
            .AppendLine("   sousin_jyky_cd = @sousin_jyky_cd, ")   '送信状況コード
            .AppendLine("   sousin_kanry_datetime = @sousin_kanry_datetime, ")   '送信完了日時
            .AppendLine("   upd_login_user_id = @upd_login_user_id, ")   '更新ログインユーザーID
            .AppendLine("   upd_datetime = GETDATE() ")   '更新日時
            .AppendLine("WHERE ")
            .AppendLine("  kameiten_cd = @kameiten_cd ")
            .AppendLine("AND ")
            .AppendLine("  jyuusyo_no = @jyuusyo_no ")
        End With

        'パラメータの設定
        With paramList
            '加盟店コード
            .Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 255, strKameitenCd))
            '住所No
            .Add(MakeParam("@jyuusyo_no", SqlDbType.VarChar, 255, "2"))

            '連携指示コード
            .Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 1, 2))
            '送信状況コード
            .Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 1, 0))
            '送信完了日時
            .Add(MakeParam("@sousin_kanry_datetime", SqlDbType.DateTime, 255, DBNull.Value))

            '更新ログインユーザーID
            .Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 255, strUserId))
        End With

        ' クエリ実行
        Return ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

    End Function

    ''' <summary>
    ''' 加盟店住所マスタ連携管理テーブルの登録処理
    ''' </summary>
    ''' <history>20101108 馬艶軍 加盟店住所マスタ連携管理テーブルも追加・更新する必要があります。</history>
    Public Function InsKameitenJyuusyoRenkei(ByVal strKameitenCd As String, ByVal strUserId As String) As Integer

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        With commandTextSb
            .AppendLine("INSERT INTO ")
            .AppendLine("   m_kameiten_jyuusyo_renkei WITH (UPDLOCK) ")
            .AppendLine("( ")
            .AppendLine("   kameiten_cd, ")   '加盟店コード
            .AppendLine("   jyuusyo_no, ")   '住所NO
            .AppendLine("   renkei_siji_cd, ")   '連携指示コード
            .AppendLine("   sousin_jyky_cd, ")   '送信状況コード
            .AppendLine("   upd_login_user_id, ")   '登録ログインユーザーID
            .AppendLine("   upd_datetime ")   '登録日時
            .AppendLine(") ")
            .AppendLine("SELECT ")
            .AppendLine("   @kameiten_cd, ")   '加盟店コード
            .AppendLine("   @jyuusyo_no, ")   '住所NO
            .AppendLine("   @renkei_siji_cd, ")   '連携指示コード
            .AppendLine("   @sousin_jyky_cd, ")   '送信状況コード
            .AppendLine("   @upd_login_user_id, ")   '登録ログインユーザーID
            .AppendLine("   GETDATE() ")   '登録日時
        End With

        'パラメータの設定
        With paramList
            '加盟店コード
            .Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 255, strKameitenCd.ToUpper))
            '住所NO
            .Add(MakeParam("@jyuusyo_no", SqlDbType.VarChar, 255, "2"))

            '連携指示コード
            .Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 1, 1))
            '送信状況コード
            .Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 1, 0))

            '登録ログインユーザーID
            .Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 255, strUserId))
        End With

        ' クエリ実行
        Return ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

    End Function

    ''' <summary>空白を削除</summary>
    Private Function TrimNull(ByVal objStr As Object) As String
        If IsDBNull(objStr) Then
            TrimNull = ""
        Else
            TrimNull = objStr.ToString.Trim
        End If
    End Function

    ''' <summary>
    ''' CSVデータを取得する
    ''' </summary>
    ''' <returns>CSVデータテーブル</returns>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 車龍</history>
    Public Function SelEigyousyoCsv() As Data.DataTable

        ' DataSetインスタンスの生成
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	ISNULL(MKM.meisyou,'') AS meisyou ") '--ブロック名 ")
            .AppendLine("	,ISNULL(ME.eigyousyo_cd,'') AS eigyousyo_cd ") '--営業所コード ")
            .AppendLine("	,'加入' AS fc_ten_kbn ") '--FC区分 ")
            .AppendLine("	,ISNULL(ME.fc_jigyousyo_cd,'') AS fc_jigyousyo_cd ") '--(FC)事業所コード ")
            .AppendLine("	,ISNULL(ME.eigyousyo_mei,'') AS eigyousyo_mei ") '--営業所名 ")
            .AppendLine("	,ISNULL(MT.tys_kaisya_mei,'') AS tys_kaisya_mei ") '--調査会社名 ")
            .AppendLine("	,ISNULL(MT.yakusyoku_mei,'') AS yakusyoku_mei ") '--役職名 ")
            .AppendLine("	,ISNULL(MT.daihyousya_mei,'') AS daihyousya_mei ") '--代表者名 ")
            .AppendLine("	,ISNULL(ME.yuubin_no,'') AS yuubin_no ") '--郵便番号 ")
            .AppendLine("	,ISNULL(ME.jyuusyo1,'') + ' ' + ISNULL(ME.jyuusyo2,'') AS jyuusyo ") '--住所1 + 住所2 ")
            .AppendLine("	,ISNULL(ME.tel_no,'') AS tel_no ") '--電話番号 ")
            .AppendLine("	,ISNULL(ME.fax_no,'') AS fax_no ") '--FAX番号 ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),SUB_TTS.nyuuryoku_date,111),'') AS nyuuryoku_date ") '--入力日 ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),SUB_TTS.seikyuusyo_hak_date,111),'') AS seikyuusyo_hak_date ") '--請求書発行日 ")
            .AppendLine("	,ISNULL(SUB_TTS.syouhin_cd,'') AS syouhin_cd ") '--商品ｺｰﾄﾞ ")
            .AppendLine("	,ISNULL(MS.syouhin_mei,'') AS syouhin_mei ") '--商品名 ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),SUB_TTS.koumuten_seikyuu_tanka),'') AS koumuten_seikyuu_tanka ") '--工務店請求税抜金額(工務店請求単価) ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),SUB_TTS.tanka),'') AS jituseikyuu_zeinu_kingaku ") '--実請求税抜金額(単価) ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),SUB_TTS.syouhizei_gaku),'') AS syouhizei_gaku ") '--消費税(消費税額) ")
            .AppendLine("	,(ISNULL(SUB_TTS.tanka,0) + ISNULL(SUB_TTS.syouhizei_gaku,0)) AS zeikomi_kingaku ") '--税込金額(単価＋消費税額) ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),SUB_TTS.tanka),'') AS tanka ") '--単価 ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),SUB_TTS.suu),'') AS suu ") '--数量 ")
            .AppendLine("	,ISNULL(ME.eigyousyo_cd,'') AS seijyuusaki_cd ") '--請求先ｺｰﾄﾞ(営業所コード) ")
            .AppendLine("	,ISNULL(ME.eigyousyo_mei,'') AS seijyuusaki_mei ") '--請求先ｺｰﾄﾞ(営業所名) ")
            .AppendLine("	 ")
            .AppendLine("FROM ")
            .AppendLine("	m_eigyousyo AS ME WITH(READCOMMITTED) ") '--営業所マスタ ")
            .AppendLine("	INNER JOIN ")
            .AppendLine("	m_kakutyou_meisyou AS MKM WITH(READCOMMITTED) ") '--拡張名称マスタ ")
            .AppendLine("	ON ")
            .AppendLine("		MKM.code = ME.block_cd ") '--拡張名称M.ｺｰﾄﾞ = 営業所M.ブロックコード ")
            .AppendLine("		AND ")
            .AppendLine("		MKM.meisyou_syubetu = @meisyou_syubetu ") '--拡張名称M.名称種別 = "61" ")
            .AppendLine("		AND ")
            .AppendLine("		ME.fc_ten_kbn = @fc_ten_kbn ") '--営業所M.FC区分 = "1"(加入) ")
            .AppendLine("		AND ")
            .AppendLine("		ME.torikesi = @torikesi ") '--営業所M.取消 = "0" ")
            .AppendLine("	INNER JOIN ")
            .AppendLine("	m_tyousakaisya AS MT WITH(READCOMMITTED) ") '--調査会社マスタ ")
            .AppendLine("	ON ")
            .AppendLine("		MT.tys_kaisya_cd = ME.fc_tys_kaisya_cd ") '--調査会社M.調査会社ｺｰﾄﾞ = 営業所M.(FC)調査会社ｺｰﾄﾞ ")
            .AppendLine("		AND ")
            .AppendLine("		MT.jigyousyo_cd = ME.fc_jigyousyo_cd ") '--調査会社M.事業所ｺｰﾄﾞ = 営業所M.(FC)事業所ｺｰﾄﾞ ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	( ")
            .AppendLine("		SELECT ")
            .AppendLine("			mise_cd ") '--店コード ")
            .AppendLine("			,nyuuryoku_date ") '--入力日 ")
            .AppendLine("			,seikyuusyo_hak_date ") '--請求書発行日 ")
            .AppendLine("			,syouhin_cd ") '--商品ｺｰﾄﾞ ")
            .AppendLine("			,koumuten_seikyuu_tanka ") '--工務店請求単価 ")
            .AppendLine("			,tanka ") '--単価 ")
            .AppendLine("			,syouhizei_gaku ") '--消費税額 ")
            .AppendLine("			,suu ") '--数量 ")
            .AppendLine("		FROM ")
            .AppendLine("			t_tenbetu_seikyuu WITH(READCOMMITTED) ") '--店別請求テーブル ")
            .AppendLine("		WHERE ")
            .AppendLine("		LEFT(CONVERT(VARCHAR(10),nyuuryoku_date,112),6) = LEFT(CONVERT(VARCHAR(10),GETDATE(),112),6) ") '--店別請求T.入力日 に システム年月 を含む ")
            .AppendLine("	) AS SUB_TTS	 ")
            .AppendLine("	ON ")
            .AppendLine("		SUB_TTS.mise_cd = ME.eigyousyo_cd ") '--店別請求マスタ.店コード = 営業所マスタ.営業所コード ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_syouhin AS MS WITH(READCOMMITTED) ") '--商品マスタ ")
            .AppendLine("	ON ")
            .AppendLine("		SUB_TTS.syouhin_cd = MS.syouhin_cd ") '--商品マスタ ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.Int, 10, 61))
        paramList.Add(MakeParam("@fc_ten_kbn", SqlDbType.Int, 10, 1))
        paramList.Add(MakeParam("@torikesi", SqlDbType.VarChar, 1, "0"))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dtEigyousyoCsv", paramList.ToArray)

        '戻る
        Return dsDataSet.Tables("dtEigyousyoCsv")

    End Function

    ''' <summary>
    ''' システム日付を取得する
    ''' </summary>
    ''' <returns>CSVデータテーブル</returns>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 車龍</history>
    Public Function SelSystemDateYMD() As Data.DataTable

        ' DataSetインスタンスの生成
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("CONVERT(VARCHAR(10),GETDATE(),112) as system_date	 ")
        End With

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dtSystemDateYMD")

        '戻る
        Return dsDataSet.Tables("dtSystemDateYMD")

    End Function

    ''' <summary>
    ''' ddlのデータを取得する
    ''' </summary>
    ''' <returns>ddlのデータテーブル</returns>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 車龍</history>
    Public Function SelDdlList(ByVal strMeisyouSyubetu As Integer) As Data.DataTable

        ' DataSetインスタンスの生成
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	code AS code ")
            .AppendLine("	,CONVERT(VARCHAR(10),code) + '：' + ISNULL(meisyou,'') AS meisyou ")
            .AppendLine("FROM ")
            .AppendLine("	m_kakutyou_meisyou WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	meisyou_syubetu = @meisyou_syubetu ")
            .AppendLine("ORDER BY ")
            .AppendLine("	hyouji_jyun ASC ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.Int, 10, strMeisyouSyubetu))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dtDdl", paramList.ToArray)

        '戻る
        Return dsDataSet.Tables("dtDdl")

    End Function

    ''' <summary>
    ''' 調査会社情報件数を取得する
    ''' </summary>
    ''' <returns>ddlのデータテーブル</returns>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 車龍</history>
    Public Function SelTyousaKaisyaCount(ByVal strTyousaKaisyaCd As String) As Data.DataTable

        ' DataSetインスタンスの生成
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	COUNT(tys_kaisya_cd) AS cnt ")
            .AppendLine("FROM ")
            .AppendLine("	m_tyousakaisya WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	tys_kaisya_cd + jigyousyo_cd LIKE @tys_kaisya_cd ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 8, strTyousaKaisyaCd & "%"))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dtTyousaKaisyaCount", paramList.ToArray)

        '戻る
        Return dsDataSet.Tables("dtTyousaKaisyaCount")

    End Function

    ''' <summary>
    ''' 調査会社情報を取得する
    ''' </summary>
    ''' <returns>ddlのデータテーブル</returns>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 車龍</history>
    Public Function SelTyousaKaisyaInfo(ByVal strTyousaKaisyaCd As String) As Data.DataTable

        ' DataSetインスタンスの生成
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	tys_kaisya_cd ") '--調査会社コード ")
            .AppendLine("	,jigyousyo_cd ") '--事業所コード ")
            .AppendLine("	,tys_kaisya_mei ") '--調査会社名 ")
            .AppendLine("	,tys_kaisya_mei_kana ") '--調査会社名カナ ")
            .AppendLine("	,daihyousya_mei ") '--代表者名 ")
            .AppendLine("	,yakusyoku_mei ") '--役職名 ")
            .AppendLine("	,jyuusyo1 ") '--住所1 ")
            .AppendLine("	,jyuusyo2 ") '--住所2 ")
            .AppendLine("	,yuubin_no ") '--郵便番号 ")
            .AppendLine("	,tel_no ") '--電話番号 ")
            .AppendLine("	,fax_no ") '--FAX番号 ")
            .AppendLine("	,japan_kai_kbn ") '--JAPAN会区分 ")
            .AppendLine("	,CASE ")
            .AppendLine("	    WHEN japan_kai_nyuukai_date IS NOT NULL THEN ")
            .AppendLine("	        LEFT(CONVERT(VARCHAR(10),japan_kai_nyuukai_date,111),7) ") '--JAPAN会入会年月 ")
            .AppendLine("	    ELSE ")
            .AppendLine("	        '' ")
            .AppendLine("	    END AS japan_kai_nyuukai_date ")
            .AppendLine("	,CASE ")
            .AppendLine("	    WHEN japan_kai_taikai_date IS NOT NULL THEN ")
            .AppendLine("	        LEFT(CONVERT(VARCHAR(10),japan_kai_taikai_date,111),7) ") '--JAPAN会退会年月 ")
            .AppendLine("	    ELSE ")
            .AppendLine("	        '' ")
            .AppendLine("	    END AS japan_kai_taikai_date ")
            .AppendLine("	,report_jhs_token_flg ") '--ReportJHSトークン有無フラグ ")
            .AppendLine("	,tkt_jbn_tys_syunin_skk_flg ") '--宅地地盤調査主任資格有無フラグ ")
            .AppendLine("FROM ")
            .AppendLine("	m_tyousakaisya WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	tys_kaisya_cd + jigyousyo_cd LIKE @tys_kaisya_cd ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 8, strTyousaKaisyaCd & "%"))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dtTyousaKaisyaInfo", paramList.ToArray)

        '戻る
        Return dsDataSet.Tables("dtTyousaKaisyaInfo")

    End Function

    ''' <summary>
    ''' 固定チャージの入力日を取得する
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 車龍</history>
    Public Function SelKoteiTyaaji(ByVal strEigyousyoCd As String, ByVal blnThisMonth As Boolean) As Data.DataTable

        ' DataSetインスタンスの生成
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	LEFT(CONVERT(VARCHAR(10),MAX(nyuuryoku_date),112),6) AS nyuuryoku_date ")
            .AppendLine("   ,MAX(nyuuryoku_date_no) as nyuuryoku_date_no ")
            .AppendLine("FROM ")
            .AppendLine("	t_tenbetu_seikyuu WITH(READCOMMITTED) ") '--店別請求テーブル ")
            .AppendLine("WHERE ")
            .AppendLine("	mise_cd = @mise_cd ") '--店コード ")
            .AppendLine("	AND ")
            .AppendLine("	bunrui_cd = @bunrui_cd ") '--分類コード ")
            .AppendLine("	AND ")
            .AppendLine("	syouhin_cd = @syouhin_cd ") '--商品コード ")
            If blnThisMonth Then
                .AppendLine("	AND ")
                .AppendLine("	LEFT(CONVERT(VARCHAR(10),nyuuryoku_date,112),6) = LEFT(CONVERT(VARCHAR(10),GETDATE(),112),6) ") '--入力日 ")
            End If
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@mise_cd", SqlDbType.VarChar, 5, strEigyousyoCd))
        paramList.Add(MakeParam("@bunrui_cd", SqlDbType.VarChar, 3, "230"))
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, "L2001"))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dtKoteiTyaajiYM", paramList.ToArray)

        '戻る
        Return dsDataSet.Tables("dtKoteiTyaajiYM")

    End Function

    ''' <summary>
    ''' 店別請求テーブルを登録する
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 車龍</history>
    Public Function InsTenbetuSeikyuu(ByVal strEigyousyoCd As String, ByVal strUserId As String) As Boolean

        Dim intInsCount As Integer

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("INSERT INTO ")
            .AppendLine("	t_tenbetu_seikyuu WITH(UPDLOCK) ") '--店別請求テーブル ")
            .AppendLine("	( ")
            .AppendLine("		mise_cd ") '--店コード ")
            .AppendLine("		,bunrui_cd ") '--分類コード ")
            .AppendLine("		,nyuuryoku_date ") '--入力日 ")
            .AppendLine("		,nyuuryoku_date_no ") '--入力日NO ")
            .AppendLine("		,hassou_date ") '--発送日 ")
            .AppendLine("		,seikyuusyo_hak_date ") '--請求書発行日 ")
            .AppendLine("		,uri_date ") '--売上年月日 ")
            .AppendLine("		,denpyou_uri_date ") '--伝票売上年月日 ")
            .AppendLine("		,uri_keijyou_flg ") '--売上処理FLG(売上計上FLG) ")
            .AppendLine("		,syouhin_cd ") '--商品コード ")
            .AppendLine("		,tanka ") '--単価 ")
            .AppendLine("		,suu ") '--数量 ")
            .AppendLine("		,zei_kbn ") '--税区分 ")
            .AppendLine("		,syouhizei_gaku ") '--消費税額 ")
            .AppendLine("		,koumuten_seikyuu_tanka ") '--工務店請求単価 ")
            .AppendLine("		,add_login_user_id ") '--登録ログインユーザーID ")
            .AppendLine("		,add_datetime ") '--登録日時 ")
            .AppendLine("	) ")
            .AppendLine("SELECT ")
            .AppendLine("	(@eigyousyo_cd + ' ') AS mise_cd ") '--店コード ")
            .AppendLine("	,@bunrui_cd AS bunrui_cd ") '--分類コード ")
            .AppendLine("	,CONVERT(DATETIME,CONVERT(VARCHAR(10),GETDATE(),111)) AS nyuuryoku_date ") '--入力日 ")
            '============2012/05/14 車龍 要望の対応 修正↓=========================
            .AppendLine("   ,@nyuuryoku_date_no AS nyuuryoku_date_no") '--入力日NO ")
            '.AppendLine("	,( ")
            '.AppendLine("		SELECT ")
            '.AppendLine("			CASE ")
            '.AppendLine("				WHEN MAX(nyuuryoku_date_no) IS NULL THEN ")
            '.AppendLine("					1 ")
            '.AppendLine("				ELSE ")
            '.AppendLine("					MAX(nyuuryoku_date_no) + 1 ")
            '.AppendLine("				END as nyuuryoku_date_no ")
            '.AppendLine("		FROM ")
            '.AppendLine("			t_tenbetu_seikyuu WITH(READCOMMITTED) ")
            '.AppendLine("		WHERE ")
            '.AppendLine("			mise_cd = @eigyousyo_cd ")
            '.AppendLine("			AND ")
            '.AppendLine("			bunrui_cd = @bunrui_cd ")
            '.AppendLine("			AND ")
            '.AppendLine("			syouhin_cd = @syouhin_cd ")
            '.AppendLine("			 ")
            '.AppendLine("	) AS nyuuryoku_date_no ") '--入力日NO ")
            '============2012/05/14 車龍 要望の対応 修正↑=========================
            .AppendLine("	,CONVERT(DATETIME,CONVERT(VARCHAR(10),GETDATE(),111)) AS hassou_date ") '--発送日 ")
            .AppendLine("	,( ")
            .AppendLine("		SELECT ")
            .AppendLine("			CASE ISNULL(MSS.seikyuu_sime_date,'') ")
            .AppendLine("				WHEN '' THEN ")
            .AppendLine("					DATEADD(DAY,-1,CONVERT(DATETIME,LEFT(CONVERT(VARCHAR(10),DATEADD(MONTH,1,GETDATE()),111),8) + '01')) ")
            .AppendLine("				WHEN '31' THEN ")
            .AppendLine("					DATEADD(DAY,-1,CONVERT(DATETIME,LEFT(CONVERT(VARCHAR(10),DATEADD(MONTH,1,GETDATE()),111),8) + '01')) ")
            .AppendLine("				ELSE ")
            .AppendLine("					CASE ISDATE(LEFT(CONVERT(VARCHAR(10),GETDATE(),111),8) + MSS.seikyuu_sime_date) ")
            .AppendLine("						WHEN 1 THEN ")
            .AppendLine("							CONVERT(DATETIME,LEFT(CONVERT(VARCHAR(10),GETDATE(),111),8) + MSS.seikyuu_sime_date) ")
            .AppendLine("						ELSE ")
            .AppendLine("							DATEADD(DAY,-1,CONVERT(DATETIME,LEFT(CONVERT(VARCHAR(10),DATEADD(MONTH,1,GETDATE()),111),8) + '01')) ")
            .AppendLine("						END				 ")
            .AppendLine("				END AS seikyuu_sime_date ")
            .AppendLine("		FROM ")
            .AppendLine("			m_seikyuu_saki AS MSS WITH(READCOMMITTED) ")
            .AppendLine("			INNER JOIN ")
            .AppendLine("			m_eigyousyo AS ME WITH(READCOMMITTED) ")
            .AppendLine("			ON ")
            .AppendLine("				MSS.seikyuu_saki_cd = ME.seikyuu_saki_cd ")
            .AppendLine("				AND ")
            .AppendLine("				MSS.seikyuu_saki_brc = ME.seikyuu_saki_brc ")
            .AppendLine("				AND ")
            .AppendLine("				MSS.seikyuu_saki_kbn = ME.seikyuu_saki_kbn ")
            .AppendLine("		WHERE ")
            .AppendLine("				ME.eigyousyo_cd = @eigyousyo_cd ")
            .AppendLine("	) AS seikyuusyo_hak_date ") '--請求書発行日 ")
            .AppendLine("	,CONVERT(DATETIME,CONVERT(VARCHAR(10),GETDATE(),111)) AS uri_date ") '--売上年月日 ")
            .AppendLine("	,CONVERT(DATETIME,CONVERT(VARCHAR(10),GETDATE(),111)) AS denpyou_uri_date ") '--伝票売上年月日 ")
            .AppendLine("	,@uri_keijyou_flg AS uri_keijyou_flg ") '--売上処理FLG(売上計上FLG) ")
            .AppendLine("	,@syouhin_cd AS syouhin_cd ") '--商品コード ")
            .AppendLine("	,MS.hyoujun_kkk AS tanka ") '--単価(商品マスタ.標準価格) ")
            .AppendLine("	,@suu AS suu ") '--数量 ")
            .AppendLine("	,MS.zei_kbn AS zei_kbn ") '--税区分(商品マスタ.税区分) ")
            .AppendLine("	,ISNULL(MS.hyoujun_kkk,0) * MSZ.zeiritu AS syouhizei_gaku ") '--消費税額(単価×消費税マスタ.税率) ")
            .AppendLine("	,@koumuten_seikyuu_tanka AS koumuten_seikyuu_tanka ") '--工務店請求単価 ")
            .AppendLine("	,@user_id AS add_login_user_id ") '--登録ログインユーザーID ")
            .AppendLine("	,GETDATE() AS add_datetime ") '--登録日時 ")
            .AppendLine("	 ")
            .AppendLine("FROM ")
            .AppendLine("	m_syouhin AS MS WITH(READCOMMITTED) ") '--商品マスタ ")
            .AppendLine("	INNER JOIN ")
            .AppendLine("	m_syouhizei AS MSZ WITH(READCOMMITTED) ") '--消費税マスタ ")
            .AppendLine("	ON ")
            .AppendLine("		MS.zei_kbn = MSZ.zei_kbn ") '--税区分 ")
            .AppendLine("WHERE ")
            .AppendLine("	MS.syouhin_cd = @syouhin_cd ") '--商品コード ")
        End With

        'パラメータの設定
        With paramList
            .Add(MakeParam("@eigyousyo_cd", SqlDbType.VarChar, 5, strEigyousyoCd))
            .Add(MakeParam("@bunrui_cd", SqlDbType.VarChar, 3, "230"))
            .Add(MakeParam("@nyuuryoku_date_no", SqlDbType.Int, 10, "1"))
            .Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, "L2001"))
            .Add(MakeParam("@uri_keijyou_flg", SqlDbType.Int, 10, 0))
            .Add(MakeParam("@suu", SqlDbType.Int, 10, 1))
            .Add(MakeParam("@koumuten_seikyuu_tanka", SqlDbType.Int, 10, 0))
            .Add(MakeParam("@user_id", SqlDbType.VarChar, 40, strUserId))

        End With

        Try
            ' クエリ実行
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

    ''' <summary>
    ''' 請求先の存在チェックする
    ''' </summary>
    ''' <returns>請求先の件数</returns>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 車龍</history>
    Public Function SelSeikyuusakiCheck(ByVal strEigyousyoCd As String) As Data.DataTable

        ' DataSetインスタンスの生成
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	COUNT(ME.eigyousyo_cd) AS cnt ")
            .AppendLine("FROM ")
            .AppendLine("	m_eigyousyo AS ME WITH(READCOMMITTED) ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_seikyuu_saki AS MSS WITH(READCOMMITTED) ")
            .AppendLine("	ON ")
            .AppendLine("		ME.seikyuu_saki_cd = MSS.seikyuu_saki_cd ")
            .AppendLine("		AND ")
            .AppendLine("		ME.seikyuu_saki_brc = MSS.seikyuu_saki_brc ")
            .AppendLine("		AND ")
            .AppendLine("		ME.seikyuu_saki_kbn = MSS.seikyuu_saki_kbn ")
            .AppendLine("WHERE ")
            .AppendLine("	MSS.seikyuu_saki_cd IS NOT NULL ")
            .AppendLine("	AND ")
            .AppendLine("	ME.eigyousyo_cd = @eigyousyo_cd ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@eigyousyo_cd", SqlDbType.VarChar, 5, strEigyousyoCd))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dtSeikyuusakiCheck", paramList.ToArray)

        '戻る
        Return dsDataSet.Tables("dtSeikyuusakiCheck")

    End Function


    ''' <summary>
    ''' 店別請求テーブル連携管理テーブルの存在チェックする
    ''' </summary>
    ''' <returns>店別請求テーブル連携管理テーブルの件数</returns>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 車龍</history>
    Public Function SelTenbetuSeikyuuRenkeiCount(ByVal strEigyousyoCd As String) As Data.DataTable

        ' DataSetインスタンスの生成
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	COUNT(mise_cd) AS cnt ")
            .AppendLine("FROM ")
            .AppendLine("	t_tenbetu_seikyuu_renkei WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	mise_cd = @mise_cd ")
            .AppendLine("	AND ")
            .AppendLine("	bunrui_cd = @bunrui_cd ")
            .AppendLine("	AND ")
            .AppendLine("	CONVERT(VARCHAR(10),nyuuryoku_date,111) = CONVERT(VARCHAR(10),GETDATE(),111) ")
            .AppendLine("	AND ")
            '.AppendLine("	nyuuryoku_date_no =  ")
            '.AppendLine("		( ")
            '.AppendLine("			SELECT ")
            '.AppendLine("				ISNULL(MAX(nyuuryoku_date_no),0) AS nyuuryoku_date_no ")
            '.AppendLine("			FROM ")
            '.AppendLine("				t_tenbetu_seikyuu WITH(READCOMMITTED) ")
            '.AppendLine("			WHERE ")
            '.AppendLine("				mise_cd = @mise_cd ")
            '.AppendLine("				AND ")
            '.AppendLine("				bunrui_cd = @bunrui_cd ")
            '.AppendLine("				AND ")
            '.AppendLine("				syouhin_cd = @syouhin_cd ")
            '.AppendLine("		) ")

            .AppendLine("	nyuuryoku_date_no = @nyuuryoku_date_no ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@mise_cd", SqlDbType.VarChar, 5, strEigyousyoCd))
        paramList.Add(MakeParam("@bunrui_cd", SqlDbType.VarChar, 3, "230"))
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, "L2001"))

        paramList.Add(MakeParam("@nyuuryoku_date_no", SqlDbType.Int, 10, "1"))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dtTenbetuSeikyuuRenkeiCount", paramList.ToArray)

        '戻る
        Return dsDataSet.Tables("dtTenbetuSeikyuuRenkeiCount")

    End Function

    ''' <summary>
    ''' 店別請求テーブル連携管理テーブルを登録する
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 車龍</history>
    Public Function InsTenbetuSeikyuuRenkei(ByVal strEigyousyoCd As String, ByVal strUserId As String) As Boolean

        Dim intInsCount As Integer

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("INSERT INTO ")
            .AppendLine("	t_tenbetu_seikyuu_renkei WITH(UPDLOCK) ")
            .AppendLine("	( ")
            .AppendLine("		mise_cd ")
            .AppendLine("		,bunrui_cd ")
            .AppendLine("		,nyuuryoku_date ")
            .AppendLine("		,nyuuryoku_date_no ")
            .AppendLine("		,renkei_siji_cd ")
            .AppendLine("		,sousin_jyky_cd ")
            .AppendLine("		,upd_login_user_id ")
            .AppendLine("		,upd_datetime ")
            .AppendLine("	) ")
            '.AppendLine("SELECT  ")
            '.AppendLine("	@mise_cd AS mise_cd ")
            '.AppendLine("	,@bunrui_cd AS bunrui_cd ")
            '.AppendLine("	,CONVERT(DATETIME,CONVERT(VARCHAR(10),GETDATE(),111)) AS nyuuryoku_date ")
            '.AppendLine("	,@nyuuryoku_date_no AS nyuuryoku_date_no  ")
            '.AppendLine("	,@renkei_siji_cd AS renkei_siji_cd ")
            '.AppendLine("	,@sousin_jyky_cd AS sousin_jyky_cd ")
            '.AppendLine("	,@user_id AS upd_login_user_id ")
            '.AppendLine("	,GETDATE() AS upd_datetime ")
            '.AppendLine("FROM  ")
            '.AppendLine("	t_tenbetu_seikyuu WITH(READCOMMITTED)  ")
            '.AppendLine("WHERE  ")
            '.AppendLine("	mise_cd = @mise_cd  ")
            '.AppendLine("	AND  ")
            '.AppendLine("	bunrui_cd = @bunrui_cd  ")
            '.AppendLine("	AND  ")
            '.AppendLine("	syouhin_cd = @syouhin_cd ")

            .AppendLine("VALUES ")
            .AppendLine("( ")
            .AppendLine("	@mise_cd ")
            .AppendLine("	,@bunrui_cd ")
            .AppendLine("	,CONVERT(DATETIME,CONVERT(VARCHAR(10),GETDATE(),111)) ")
            .AppendLine("	,@nyuuryoku_date_no ")
            .AppendLine("	,@renkei_siji_cd ")
            .AppendLine("	,@sousin_jyky_cd ")
            .AppendLine("	,@user_id ")
            .AppendLine("	,GETDATE() ")
            .AppendLine(") ")
        End With

        'パラメータの設定
        With paramList
            .Add(MakeParam("@mise_cd", SqlDbType.VarChar, 5, strEigyousyoCd))
            .Add(MakeParam("@bunrui_cd", SqlDbType.VarChar, 3, "230"))
            .Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, "L2001"))
            .Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 10, 1))
            .Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 10, 0))
            .Add(MakeParam("@user_id", SqlDbType.VarChar, 40, strUserId))

            .Add(MakeParam("@nyuuryoku_date_no", SqlDbType.Int, 10, "1"))
        End With

        Try
            ' クエリ実行
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

    ''' <summary>
    ''' 店別請求テーブル連携管理テーブルを更新する
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 車龍</history>
    Public Function UpdTenbetuSeikyuuRenkei(ByVal strEigyousyoCd As String, ByVal strUserId As String) As Boolean

        Dim UpdInsCount As Integer

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("UPDATE ")
            .AppendLine("	t_tenbetu_seikyuu_renkei WITH(UPDLOCK) ")
            .AppendLine("SET ")
            .AppendLine("	renkei_siji_cd = @renkei_siji_cd ")
            .AppendLine("	,sousin_jyky_cd = @sousin_jyky_cd ")
            .AppendLine("	,upd_login_user_id = @user_id ")
            .AppendLine("	,upd_datetime = GETDATE() ")
            .AppendLine("WHERE ")
            .AppendLine("	mise_cd = @mise_cd ")
            .AppendLine("	AND ")
            .AppendLine("	bunrui_cd = @bunrui_cd ")
            .AppendLine("	AND ")
            .AppendLine("	CONVERT(VARCHAR(10),nyuuryoku_date,111) = CONVERT(VARCHAR(10),GETDATE(),111) ")
            .AppendLine("	AND ")
            '.AppendLine("	nyuuryoku_date_no =  ")
            '.AppendLine("		( ")
            '.AppendLine("			SELECT ")
            '.AppendLine("				ISNULL(MAX(nyuuryoku_date_no),0) AS nyuuryoku_date_no ")
            '.AppendLine("			FROM ")
            '.AppendLine("				t_tenbetu_seikyuu WITH(READCOMMITTED) ")
            '.AppendLine("			WHERE ")
            '.AppendLine("				mise_cd = @mise_cd ")
            '.AppendLine("				AND ")
            '.AppendLine("				bunrui_cd = @bunrui_cd ")
            '.AppendLine("				AND ")
            '.AppendLine("				syouhin_cd = @syouhin_cd ")
            '.AppendLine("		) ")

            .AppendLine("	nyuuryoku_date_no = @nyuuryoku_date_no ")
        End With

        'パラメータの設定
        With paramList
            .Add(MakeParam("@mise_cd", SqlDbType.VarChar, 5, strEigyousyoCd))
            .Add(MakeParam("@bunrui_cd", SqlDbType.VarChar, 3, "230"))
            .Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, "L2001"))
            .Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 10, 2))
            .Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 10, 0))
            .Add(MakeParam("@user_id", SqlDbType.VarChar, 40, strUserId))

            paramList.Add(MakeParam("@nyuuryoku_date_no", SqlDbType.Int, 10, "1"))

        End With

        Try
            ' クエリ実行
            UpdInsCount = ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        Catch ex As Exception
            Return False
        End Try

        If UpdInsCount > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

End Class


